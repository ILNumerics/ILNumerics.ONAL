// MIT License
// 
// Copyright (c) 2026 ILNumerics GmbH
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ILNumerics {

    /// <summary>
    /// An artificial scope class, used to define regions of bounded effect. 
    /// </summary>
    /// <remarks>A scope is defined by <see cref="Scope.Enter(ArrayStyles?)"/> for the body of a method. It configures the array style for its 
    /// lifetime and ensures to restore the old value when leaving the scope area.</remarks>
    public sealed class Scope : IDisposable {
        // This scope implementation was once used to track / release arrays within algorithms. In ILNumerics.ONAL scopes 
        // have a purpose limited to defining regions of defined ArrayStyle configuration value and safe restoring of the 
        // former value afterwards. 
        // Scopes are no longer used for memory management. 
        public class ScopeInfo {
            public int version;
            public WeakReference<IStorage> storage;
        }

        internal sealed class ThreadingContext {
            // 4.10,ho: this is an optimized version of the threading context. It replaces the old Stack<T> based implementation
            // in the hope for better performance. In 4.12  we gonna target .NET 4.5.2. This will allow us to 
            // use the generic WeakReference<T> instead of WeakReference, which should bring further improvements. 
            internal int CurArray = 0;
            /// <summary>
            /// Index of the currently active scope. 0: no scope yet. 
            /// </summary>
            internal int CurScope = 0;
            public ScopeInfo[] Arrays = new ScopeInfo[1000];
            public Scope[] Scopes = new Scope[100];

            /// <summary>
            /// Registers an ILNumerics array for disposal after the current scope was left. Increases the internal scope counter. For ILNumerics internal use.
            /// </summary>
            /// <param name="A">arbitrary ILNumerics array</param>
            /// <returns>True if this method was called .</returns>
            /// <remarks><para>This method is part of the memory managent of ILNumerics. You should not 
            /// call this method explicitely!</para>
            /// <para>This method registered an array in the current scope - no matter what! Regardless if the type of the array 
            /// is commonly registered or not. </para></remarks>
            public bool RegisterArray(BaseArray A) {
                if (CurScope > 0) { //A.EnterScope()
                    //ho: commented out, because otherwise common local arrays do return false and hence get not registered in implicit conversions here! 
                    // ho: 4.13: included in if() again. Array.EnterScope() now returns true. Therefore the next line is not needed anymore.
                    //A.EnterScope(); // <- must increment manually, since EnterScope() is not used... !
                    // ... and now removed again: StoreArray() has also BaseArray as parameter and checks EnterScope() also...
                    StoreArray(A);
                    return true; 
                }
                return false; 
            }

            private void expand<TArray>(ref TArray[] target) {
                var newArrays = new TArray[target.Length * 2];
                Array.Copy(target, newArrays, target.Length);
                target = newArrays;
            }

            /// <summary>
            /// Stores an array in the current scope. Does NOT modify the arrays internal data! (no m_scopeCounter increment!)
            /// </summary>
            /// <param name="A">Array to be registered.</param>
            /// <remarks>This function does not check for the existence of a current scope. Such check shall be done before calling this function!
            /// <para>Since v7.0 the scope does not store arrays but the storage _currently_ assigned to the array A. 
            /// This way we account for the new array renaming feature performed by Assign().</para></remarks>
            internal void StoreArray(BaseArray A) {
                System.Diagnostics.Debug.Assert(CurScope > 0, "StoreArray was called without a current scope! Check in the caller if such scope exists at all!");
                if (object.Equals(A, null)) { // disabled for version 5 scoping: || !A.EnterScope()) {
                    return;
                }
                if (++CurArray >= Arrays.Length) {
                    expand(ref Arrays);
                }
                var stor = A.GetIStorage(); 
                var versStor = Arrays[CurArray]; 
                if (versStor != null) {
                    versStor.storage.SetTarget(stor);
                    versStor.version = stor.Version; 
                } else {
                    versStor = new ScopeInfo() { storage = new WeakReference<IStorage>(stor), version = stor.Version };
                    Arrays[CurArray] = versStor; 
                }
                stor.EnterScope(versStor);  
                Scopes[CurScope].Count++;
            }
            private Scope ensureNewScope() {
                if (++CurScope >= Scopes.Length) {
                    expand(ref Scopes);
                }
                Scope ret = Scopes[CurScope];
                if (ret == null) {
                    ret = Scopes[CurScope] = new Scope();
                } else {
                    ret.Count = 0;  // reuse existing scope
                }
                return ret; 
            }
            internal IDisposable AddScope(ArrayStyles? arrayStyle) {
                ensureNewScope();
                Scopes[CurScope].ArrayStyle = arrayStyle;
                return Scopes[CurScope];
            }
        }

        [ThreadStatic]
        private static ThreadingContext s_threadContext;

        private ArrayStyles? m_oldArrayStyle;

        /// <summary>
        /// Begins an artificial scope block within a local function, determine the array style.
        /// </summary>
        /// <param name="arrayStyle">The <see cref="Settings.ArrayStyle"/> valid within the scope block. A non-null value will be used within the scope block as current array style.</param>
        /// <returns>The new scope object.</returns>
        /// <remarks><para>The <c>Scope</c> class plays an important role for the ILNumerics memory management. When writing functions in ILNumerics, 
        /// <c>Scope</c> is used to define blocks of artificial scopes for local function blocks. ILNumerics ensures that no memory 
        /// is left as garbage after the scope block is left. Furthermore, it garantees that input arrays are kept alive during 
        /// the execution of the block. By following these <a href="http://ilnumerics.net/$GeneralRules.html" target="Main">ILNumerics function rules</a> 
        /// ILNumerics is able to optimize the execution of the algorithm regarding execution speed and memory footprint.</para>
        /// <para>If a value other than null is given for <paramref name="arrayStyle"/> the value of <see cref="Settings.ArrayStyle"/>
        /// is modified for the operations within this scope block. After the scope is left, the old setting is reliably restored. 
        /// If null is provided the current setting of <see cref="Settings.ArrayStyle"/> is not touched.</para>
        /// <para>Several overloads exist allowing to specify one or multiple arrays together with the array style setting.</para>
        /// </remarks>
        /// <example><para>The examples demonstrates a custom function in ILNumerics. It demonstrates the use of an artificial scope 
        /// block to temporarily change the array style for the thread.</para>
        /// <code><![CDATA[
        /// [TestMethod]
        /// public void ScopeWithArrayStyle() {
        /// 
        ///     using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
        ///         // numpy style is valid here...
        ///         Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);
        /// 
        ///         using (Scope.Enter(ArrayStyles.ILNumericsV4)) {
        ///             // in the scope: array style is ILNumericsV4
        /// 
        ///             Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
        /// 
        ///             // the style may be changed again ... 
        ///             Settings.ArrayStyle = ArrayStyles.numpy;
        ///             Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);
        ///             Settings.ArrayStyle = ArrayStyles.ILNumericsV4;
        ///             Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
        ///             // ...
        ///         }
        ///         // array style which was valid immediately before the scope began is restored 
        ///         Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);
        /// 
        ///     }
        /// }]]></code>
        /// </example>
        /// <seealso cref="Enter(BaseArray, ArrayStyles?)"/>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <seealso cref="ArrayStyles"/>
        public static IDisposable Enter(ArrayStyles? arrayStyle) {
            return Context.AddScope(arrayStyle);
        }
        /// <summary>
        /// Begins an artificial scope block within a local function.
        /// </summary>
        /// <returns>The new scope object.</returns>
        /// <remarks><para>The <c>Scope</c> class plays an important role for the ILNumerics memory management. When writing functions in ILNumerics, 
        /// <c>Scope</c> is used to define blocks of artificial scopes for local function blocks. ILNumerics ensures that no memory 
        /// is left as garbage after the scope block is left. Furthermore, it garantees that input arrays are kept alive during 
        /// the execution of the block. By following these <a href="http://ilnumerics.net/$GeneralRules.html" target="Main">ILNumerics function rules</a> 
        /// ILNumerics is able to optimize the execution of the algorithm regarding execution speed and memory footprint.</para>
        /// <para>Several overloads exist allowing to specify one or multiple arrays together with the array style setting.</para>
        /// </remarks>
        /// <example><para>The examples demonstrates a custom function in ILNumerics. It demonstrates the use of an artificial scope 
        /// block to temporarily change the array style for the thread.</para>
        /// <code><![CDATA[
        /// [TestMethod]
        /// public void ScopeWithArrayStyle() {
        /// 
        ///     using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
        ///         // numpy style is valid here...
        ///         Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);
        /// 
        ///         using (Scope.Enter(ArrayStyles.ILNumericsV4)) {
        ///             // in the scope: array style is ILNumericsV4
        /// 
        ///             Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
        /// 
        ///             // the style may be changed again ... 
        ///             Settings.ArrayStyle = ArrayStyles.numpy;
        ///             Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);
        ///             Settings.ArrayStyle = ArrayStyles.ILNumericsV4;
        ///             Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
        ///             // ...
        ///         }
        ///         // array style which was valid immediately before the scope began is restored 
        ///         Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);
        /// 
        ///     }
        /// }]]></code>
        /// </example>
        /// <seealso cref="Enter(BaseArray, ArrayStyles?)"/>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <seealso cref="ArrayStyles"/>
        public static IDisposable Enter() {
            return Context.AddScope(null);
        }

        /// <summary>
        /// Begins an artificial scope block within a local function block.
        /// </summary>
        /// <param name="A">Any <b>input</b> array, given as parameter for the current function. Input arrays are: <see cref="InArray{ElementType}"/>, <see cref="InCell"/>, and <see cref="InLogical"/>.</param>
        /// <param name="arrayStyle">[Optional] The <see cref="Settings.ArrayStyle"/> valid within the scope block. A non-null value will be used within the scope block as current array style. Default: (null) the current <see cref="Settings.ArrayStyle"/>.</param>
        /// <returns>The new scope object.</returns>
        /// <remarks><para>The <c>Scope</c> class plays an important role for the ILNumerics memory management. When writing functions in ILNumerics, 
        /// <c>Scope</c> is used to define blocks of artificial scopes for local function blocks. ILNumerics ensures that no memory 
        /// is left as garbage after the scope block is left. Furthermore, it garantees that input arrays are kept alive during 
        /// the execution of the block. By following these <a href="http://ilnumerics.net/$GeneralRules.html" target="Main">ILNumerics function rules</a> 
        /// ILNumerics is able to optimize the execution of the algorithm regarding execution speed and memory footprint.</para>
        /// <para>If a value other than null is given for <paramref name="arrayStyle"/> the value of <see cref="Settings.ArrayStyle"/>
        /// is modified for the operations within this scope block. After the scope is left, the old setting is reliably restored. 
        /// If null is provided (default) the current setting of <see cref="Settings.ArrayStyle"/> is not touched.</para>
        /// </remarks>
        /// <example><para>The examples demonstrates a custom function in ILNumerics. It demonstrates the use of distinct array types in the function declaration and the use of 
        /// artificial scopes.</para>
        /// <code><![CDATA[RetArray<double> FreqPeaks(InArray<double> inData, OutArray<double> freq = null, double sampFreq = 44.1) { 
        ///
        ///    using (Scope.Enter(inData)) {    
        ///             
        ///        Array<double> Data = check(inData); 
        ///        Array<double> retLength = min(ceil(Data.Length / 2.0 + 1), 5.0);   
        ///        Array<double> Window = stdWindowFunc(Data.Length);  
        ///        Array<double> magnitudes = abs(fft(Data * Window));  
        ///        magnitudes = magnitudes[r(0,end / 2 + 1)];  
        /// 
        ///        Array<double> indices = empty();  
        ///        Array<double> sorted = sort(magnitudes, indices, descending:true);  
        ///        if (!isnull(freq)) 
        ///            freq.a = (sampFreq / 2.0 / magnitudes.Length * indices)[r(0,retLength-1)];  
        ///        return magnitudes[r(0,retLength-1)];  
        ///    } 
        ///}]]></code>
        /// </example>
        /// <seealso cref="Settings.ArrayStyle"/>
        /// <seealso cref="ArrayStyles"/>
        public static IDisposable Enter(BaseArray A, ArrayStyles? arrayStyle = null) {
            return Context.AddScope(arrayStyle);
        }

        /// <summary>
        /// Begins an artificial scope block within a local function block.
        /// </summary>
        /// <param name="A">An <b>input</b> array, parameter for the current function. Input arrays are: <see cref="InArray{ElementType}"/>, <see cref="InCell"/>, and <see cref="InLogical"/>.</param>
        /// <param name="B">A second <b>input</b> array, parameter for the current function. Input arrays are: <see cref="InArray{ElementType}"/>, <see cref="InCell"/>, and <see cref="InLogical"/>.</param>
        /// <param name="arrayStyle">[Optional] The <see cref="Settings.ArrayStyle"/> valid within the scope block. A non-null value will be used within the scope block as current array style. Default: (null) the current <see cref="Settings.ArrayStyle"/>.</param>
        /// <returns>The new scope object.</returns>
        /// <remarks><para>The <c>Scope</c> class plays an important role for the ILNumerics memory management. When writing functions in ILNumerics, 
        /// <c>Scope</c> is used to define blocks of artificial scopes for local function blocks. ILNumerics ensures, no memory is left as garbage, once 
        /// such a scope block was left. Furthermore, it garantees, input arrays are kept alive during the execution of the block. By following these 
        /// <a href="http://ilnumerics.net/$GeneralRules.html" target="Main">simple rules</a>, ILNumerics is able to optimize the execution of the algorithm regarding 
        /// execution speed and memory footprint.</para>
        /// <para>If a value other than null is given for <paramref name="arrayStyle"/> the value of <see cref="Settings.ArrayStyle"/>
        /// is modified for the operations within this scope block. After the scope is left, the old setting is reliably restored. 
        /// If null is provided (default) the current setting of <see cref="Settings.ArrayStyle"/> is not touched.</para>
        /// </remarks>
        /// <example><para>The examples demonstrates a custom function in ILNumerics. It demonstrates the use of distinct array types in the function declaration and the use of 
        /// artificial scopes.</para>
        /// <code><![CDATA[RetArray<double> FreqPeaks(InArray<double> inData, OutArray<double> freq = null, double sampFreq = 44.1) { 
        ///
        ///    using (Scope.Enter(inData)) {    
        ///             
        ///        Array<double> Data = check(inData); 
        ///        Array<double> retLength = min(ceil(Data.Length / 2.0 + 1), 5.0);   
        ///        Array<double> Window = stdWindowFunc(Data.Length);  
        ///        Array<double> magnitudes = abs(fft(Data * Window));  
        ///        magnitudes = magnitudes[r(0,end / 2 + 1)];  
        /// 
        ///        Array<double> indices = empty();  
        ///        Array<double> sorted = sort(magnitudes, indices, descending:true);  
        ///        if (!isnull(freq)) 
        ///            freq.a = (sampFreq / 2.0 / magnitudes.Length * indices)[r(0,retLength-1)];  
        ///        return magnitudes[r(0,retLength-1)];  
        ///    } 
        ///}]]></code>
        /// </example>
        public static IDisposable Enter(BaseArray A, BaseArray B, ArrayStyles? arrayStyle = null) {
            return Context.AddScope(arrayStyle);
        }

        /// <summary>
        /// Begins an artificial scope block within a local function block.
        /// </summary>
        /// <param name="A">An <b>input</b> array, parameter for the current function. Input arrays are: <see cref="InArray{ElementType}"/>, <see cref="InCell"/>, and <see cref="InLogical"/>.</param>
        /// <param name="B">A second <b>input</b> array, parameter for the current function. Input arrays are: <see cref="InArray{ElementType}"/>, <see cref="InCell"/>, and <see cref="InLogical"/>.</param>
        /// <param name="C">A third <b>input</b> array, parameter for the current function. Input arrays are: <see cref="InArray{ElementType}"/>, <see cref="InCell"/>, and <see cref="InLogical"/>.</param>
        /// <param name="arrayStyle">[Optional] The <see cref="Settings.ArrayStyle"/> valid within the scope block. A non-null value will be used within the scope block as current array style. Default: (null) the current <see cref="Settings.ArrayStyle"/>.</param>
        /// <returns>The new scope object.</returns>
        /// <remarks><para>The <c>Scope</c> class plays an important role for the ILNumerics memory management. When writing functions in ILNumerics, 
        /// <c>Scope</c> is used to define blocks of artificial scopes for local function blocks. ILNumerics ensures, no memory is left as garbage, once 
        /// such a scope block was left. Furthermore, it garantees, input arrays are kept alive during the execution of the block. By following these 
        /// <a href="http://ilnumerics.net/$GeneralRules.html" target="Main">simple rules</a>, ILNumerics is able to optimize the execution of the algorithm regarding 
        /// execution speed and memory footprint.</para>
        /// <para>If a value other than null is given for <paramref name="arrayStyle"/> the value of <see cref="Settings.ArrayStyle"/>
        /// is modified for the operations within this scope block. After the scope is left, the old setting is reliably restored. 
        /// If null is provided (default) the current setting of <see cref="Settings.ArrayStyle"/> is not touched.</para>
        /// </remarks>
        /// <example><para>The examples demonstrates a custom function in ILNumerics. It demonstrates the use of distinct array types in the function declaration and the use of 
        /// artificial scopes.</para>
        /// <code><![CDATA[RetArray<double> FreqPeaks(InArray<double> inData, OutArray<double> freq = null, double sampFreq = 44.1) { 
        ///
        ///    using (Scope.Enter(inData)) {    
        ///             
        ///        Array<double> Data = check(inData); 
        ///        Array<double> retLength = min(ceil(Data.Length / 2.0 + 1), 5.0);   
        ///        Array<double> Window = stdWindowFunc(Data.Length);  
        ///        Array<double> magnitudes = abs(fft(Data * Window));  
        ///        magnitudes = magnitudes[r(0,end / 2 + 1)];  
        /// 
        ///        Array<double> indices = empty();  
        ///        Array<double> sorted = sort(magnitudes, indices, descending:true);  
        ///        if (!isnull(freq)) 
        ///            freq.a = (sampFreq / 2.0 / magnitudes.Length * indices)[r(0,retLength-1)];  
        ///        return magnitudes[r(0,retLength-1)];  
        ///    } 
        ///}]]></code>
        /// </example>
        /// <returns>The new scope object.</returns>
        public static IDisposable Enter(BaseArray A, BaseArray B, BaseArray C, ArrayStyles? arrayStyle = null) {
            return Context.AddScope(arrayStyle);
        }

        /// <summary>
        /// Begins an artificial scope block within a local function block.
        /// </summary>
        /// <param name="A">An <b>input</b> array, parameter for the current function. Input arrays are: <see cref="InArray{ElementType}"/>, <see cref="InCell"/>, and <see cref="InLogical"/>.</param>
        /// <param name="B">A second <b>input</b> array, parameter for the current function. Input arrays are: <see cref="InArray{ElementType}"/>, <see cref="InCell"/>, and <see cref="InLogical"/>.</param>
        /// <param name="C">A third <b>input</b> array, parameter for the current function. Input arrays are: <see cref="InArray{ElementType}"/>, <see cref="InCell"/>, and <see cref="InLogical"/>.</param>
        /// <param name="D">A fourth <b>input</b> array, parameter for the current function. Input arrays are: <see cref="InArray{ElementType}"/>, <see cref="InCell"/>, and <see cref="InLogical"/>.</param>
        /// <param name="arrayStyle">[Optional] The <see cref="Settings.ArrayStyle"/> valid within the scope block. A non-null value will be used within the scope block as current array style. Default: (null) the current <see cref="Settings.ArrayStyle"/>.</param>
        /// <returns>The new scope object.</returns>
        /// <remarks><para>The <c>Scope</c> class plays an important role for the ILNumerics memory management. When writing functions in ILNumerics, 
        /// <c>Scope</c> is used to define blocks of artificial scopes for local function blocks. ILNumerics ensures, no memory is left as garbage, once 
        /// such a scope block was left. Furthermore, it garantees, input arrays are kept alive during the execution of the block. By following these 
        /// <a href="http://ilnumerics.net/$GeneralRules.html" target="Main">simple rules</a>, ILNumerics is able to optimize the execution of the algorithm regarding 
        /// execution speed and memory footprint.</para>
        /// <para>If a value other than null is given for <paramref name="arrayStyle"/> the value of <see cref="Settings.ArrayStyle"/>
        /// is modified for the operations within this scope block. After the scope is left, the old setting is reliably restored. 
        /// If null is provided (default) the current setting of <see cref="Settings.ArrayStyle"/> is not touched.</para>
        /// </remarks>
        /// <example><para>The examples demonstrates a custom function in ILNumerics. It demonstrates the use of distinct array types in the function declaration and the use of 
        /// artificial scopes.</para>
        /// <code><![CDATA[RetArray<double> FreqPeaks(InArray<double> inData, OutArray<double> freq = null, double sampFreq = 44.1) { 
        ///
        ///    using (Scope.Enter(inData)) {    
        ///             
        ///        Array<double> Data = check(inData); 
        ///        Array<double> retLength = min(ceil(Data.Length / 2.0 + 1), 5.0);   
        ///        Array<double> Window = stdWindowFunc(Data.Length);  
        ///        Array<double> magnitudes = abs(fft(Data * Window));  
        ///        magnitudes = magnitudes[r(0,end / 2 + 1)];  
        /// 
        ///        Array<double> indices = empty();  
        ///        Array<double> sorted = sort(magnitudes, indices, descending:true);  
        ///        if (!isnull(freq)) 
        ///            freq.a = (sampFreq / 2.0 / magnitudes.Length * indices)[r(0,retLength-1)];  
        ///        return magnitudes[r(0,retLength-1)];  
        ///    } 
        ///}]]></code>
        /// </example>
        public static IDisposable Enter(BaseArray A, BaseArray B, BaseArray C, BaseArray D, ArrayStyles? arrayStyle = null) {
            return Context.AddScope(arrayStyle);
        }
        /// <summary>
        /// Begins an artificial scope block within a local function block.
        /// </summary>
        /// <param name="A">An <b>input</b> array, parameter for the current function. Input arrays are: <see cref="InArray{ElementType}"/>, <see cref="InCell"/>, and <see cref="InLogical"/>.</param>
        /// <param name="B">A second <b>input</b> array, parameter for the current function. Input arrays are: <see cref="InArray{ElementType}"/>, <see cref="InCell"/>, and <see cref="InLogical"/>.</param>
        /// <param name="C">A third <b>input</b> array, parameter for the current function. Input arrays are: <see cref="InArray{ElementType}"/>, <see cref="InCell"/>, and <see cref="InLogical"/>.</param>
        /// <param name="D">A fourth <b>input</b> array, parameter for the current function. Input arrays are: <see cref="InArray{ElementType}"/>, <see cref="InCell"/>, and <see cref="InLogical"/>.</param>
        /// <param name="E">A fifth <b>input</b> array, parameter for the current function. Input arrays are: <see cref="InArray{ElementType}"/>, <see cref="InCell"/>, and <see cref="InLogical"/>.</param>
        /// <param name="arrayStyle">[Optional] The <see cref="Settings.ArrayStyle"/> valid within the scope block. A non-null value will be used within the scope block as current array style. Default: (null) the current <see cref="Settings.ArrayStyle"/>.</param>
        /// <returns>The new scope object.</returns>
        /// <remarks><para>The <c>Scope</c> class plays an important role for the ILNumerics memory management. When writing functions in ILNumerics, 
        /// <c>Scope</c> is used to define blocks of artificial scopes for local function blocks. ILNumerics ensures, no memory is left as garbage, once 
        /// such a scope block was left. Furthermore, it garantees, input arrays are kept alive during the execution of the block. By following these 
        /// <a href="http://ilnumerics.net/$GeneralRules.html" target="Main">simple rules</a>, ILNumerics is able to optimize the execution of the algorithm regarding 
        /// execution speed and memory footprint.</para>
        /// <para>If a value other than null is given for <paramref name="arrayStyle"/> the value of <see cref="Settings.ArrayStyle"/>
        /// is modified for the operations within this scope block. After the scope is left, the old setting is reliably restored. 
        /// If null is provided (default) the current setting of <see cref="Settings.ArrayStyle"/> is not touched.</para>
        /// </remarks>
        /// <example><para>The examples demonstrates a custom function in ILNumerics. It demonstrates the use of distinct array types in the function declaration and the use of 
        /// artificial scopes.</para>
        /// <code><![CDATA[RetArray<double> FreqPeaks(InArray<double> inData, OutArray<double> freq = null, double sampFreq = 44.1) { 
        ///
        ///    using (Scope.Enter(inData)) {    
        ///             
        ///        Array<double> Data = check(inData); 
        ///        Array<double> retLength = min(ceil(Data.Length / 2.0 + 1), 5.0);   
        ///        Array<double> Window = stdWindowFunc(Data.Length);  
        ///        Array<double> magnitudes = abs(fft(Data * Window));  
        ///        magnitudes = magnitudes[r(0,end / 2 + 1)];  
        /// 
        ///        Array<double> indices = empty();  
        ///        Array<double> sorted = sort(magnitudes, indices, descending:true);  
        ///        if (!isnull(freq)) 
        ///            freq.a = (sampFreq / 2.0 / magnitudes.Length * indices)[r(0,retLength-1)];  
        ///        return magnitudes[r(0,retLength-1)];  
        ///    } 
        ///}]]></code>
        /// </example>
        public static IDisposable Enter(BaseArray A, BaseArray B, BaseArray C, BaseArray D, BaseArray E, ArrayStyles? arrayStyle = null) {
            // version 5 scoping: no arrays enter the scope here. They are all registered in the calling scope, if any, or left for the GC.
            return Context.AddScope(arrayStyle);
        }

        /// <summary>
        /// The threading context - individual for each thread
        /// </summary>
        internal static ThreadingContext Context {
            get {
                ThreadingContext ret = s_threadContext;
                if (ret == null) {
                    s_threadContext = new ThreadingContext();
                    ret = s_threadContext; 
                }
                return ret; 
            }
        }

        /// <summary>
        /// Determines the array style to be used within the scope. This 
        /// must be called once during <see cref="Enter(BaseArray, ArrayStyles?)"/> or in related overloads.
        /// </summary>
        internal ArrayStyles? ArrayStyle {
            set {
                if (value != null) {
                    m_oldArrayStyle = Settings.ArrayStyle; 
                    Settings.ArrayStyle = value.GetValueOrDefault(); 
                } else {
                    m_oldArrayStyle = null; 
                }
            }
        }

        internal int Count = 0;
        /// <summary>
        /// Creates an empty scope. 
        /// </summary>
        private Scope() { }

#region IDisposable Members

        /// <summary>
        /// Release all arrays in this scope.
        /// </summary>
        public void Dispose() {
            var arr = Context.Arrays; 
            for (int i = Count; i-- > 0; ) {
                var sInfo = arr[Context.CurArray--];
                
                if (sInfo.storage.TryGetTarget(out var stor) && stor.Version == sInfo.version) {
                    stor.LeaveScope(sInfo); 
                } 
            }
#if DEBUG_VERBOSE
            System.Diagnostics.Debug.Assert(object.Equals(Context.Scopes.Peek(), this));
            System.Diagnostics.Debug.WriteLine ("Leaving scope {0} - disposing {1} arrays",Context.Scopes.Peek().GetHashCode(),Count); 
#endif
            if (m_oldArrayStyle != null && Settings.ArrayStyle != m_oldArrayStyle) {
                Settings.ArrayStyle = m_oldArrayStyle.GetValueOrDefault(); 
            }
            Context.CurScope--;
            
        }

#endregion

    }
}
