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
//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
using ILNumerics.Core.Arrays;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static ILNumerics.Core.Functions.Builtin.MathInternal; 

namespace ILNumerics.Core.Arrays {

    /// <summary>
    /// General base class for array objects like <see cref="Array{T}"/> and similar arrays types. 
    /// </summary>
    public abstract partial class ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>   // T1 must not read "T" to prevent from conflicts with the .T (transpose) property.
        : BaseArray<T1>, IEnumerable<T1>
        where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {

        #region HYCALPER LOOPSTART

        /*!HC:TYPELIST:
        <hycalper>
        <type>
            <source locate="here">
                EqualTo 
            </source>
            <destination>LowerThan</destination>
            <destination>GreaterOrEqual</destination>
            <destination>LowerOrEqual</destination>
            <destination>GreaterThan</destination>
            <destination>UnEqualTo</destination>
        </type>
        <type>
            <source locate="comment">
                summary
            </source>
            <destination>Elementwise 'lower than' operator on two arrays.</destination>
            <destination>Elementwise 'greater or equal'  operator on two arrays.</destination>
            <destination>Elementwise 'lower or equal'  operator on two arrays.</destination>
            <destination>Elementwise 'greater than'  operator on two arrays.</destination>
            <destination>Elementwise 'unequal to'  operator on two arrays.</destination>
        </type>
        <type>
            <source locate="comment">
                returns
            </source>
            <destination><![CDATA[Logical array with the result of performing A &lt; B.]]></destination>
            <destination><![CDATA[Logical array with the result of performing A â‰¥ B.]]></destination>
            <destination><![CDATA[Logical array with the result of performing A â‰¤ B.]]></destination>
            <destination><![CDATA[Logical array with the result of performing A &gt; B.]]></destination>
            <destination><![CDATA[Logical array with the result of performing A != B.]]></destination>
        </type>
        <type>
            <source locate="here" endmark="(">
                ==
            </source>
            <destination><![CDATA[<]]></destination>
            <destination><![CDATA[>=]]></destination>
            <destination><![CDATA[<=]]></destination>
            <destination><![CDATA[>]]></destination>
            <destination><![CDATA[!=]]></destination>
        </type>
        <type>
            <source locate="here" endmark=";">
                A.Equals(B)
            </source>
            <destination><![CDATA[]]></destination>
            <destination><![CDATA[]]></destination>
            <destination><![CDATA[]]></destination>
            <destination><![CDATA[]]></destination>
            <destination><![CDATA[!A.Equals(B)]]></destination>
        </type>
        <type>
            <source locate="here">
                !NOLOGICAL
            </source>
            <destination><![CDATA[NOLOGICAL]]></destination>
            <destination><![CDATA[NOLOGICAL]]></destination>
            <destination><![CDATA[NOLOGICAL]]></destination>
            <destination><![CDATA[NOLOGICAL]]></destination>
            <destination><![CDATA[!NOLOGICAL]]></destination>
        </type>
        <type>
            <source locate="here">
                !NO_STRING
            </source>
            <destination><![CDATA[NO_STRING]]></destination>
            <destination><![CDATA[NO_STRING]]></destination>
            <destination><![CDATA[NO_STRING]]></destination>
            <destination><![CDATA[NO_STRING]]></destination>
            <destination><![CDATA[!NO_STRING]]></destination>
        </type>
        </hycalper>
        */

        /// <summary>
        /// Elementwise 'equals' comparison operator on two arrays.
        /// </summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The second array.</param>
        /// <returns>Logical array with result of A == B.</returns>
        /// <remarks><para>General notes on binary broadcasting operators: The operation is performed 
        /// elementwise on corresponding elements of <paramref name="A"/> and <paramref name="B"/>. The sizes of both 
        /// input arrays must be broadcastable and determine the size of the resulting array. Note, that 
        /// broadcasting is performed, regardless of the current setting of <see cref="Settings.ArrayStyle"/>. However, 
        /// the <see cref="Settings.ArrayStyle"/> configuration affects the operation in the following ways: 
        /// <list type="bullet">
        /// <item>If <paramref name="A"/> and <paramref name="B"/> have different number of dimensions, the array with 
        /// fewer dimensions is (virtually) padded with singleton dimensions, until the number of dimensions of both arrays match. In 
        /// <see cref="ArrayStyles.ILNumericsV4"/> the singleton dimensions are added to the end of dimensions. For <see cref="ArrayStyles.numpy"/> 
        /// singletons are padded to the start, such that here, broadcasting aligns the <i>last</i> dimensions.</item>
        /// <item><see cref="ArrayStyles.ILNumericsV4"/>: Result values of integer element types T are clamped to the 
        /// natural range of the interger type. I.e.: there will be no 'wrapping around' when exceeding the upper or lower 
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="double"/> precision 
        /// floating point arithmetics and by subsequently casting the result to the desired integer value. With <see cref="ArrayStyles.numpy"/> array style no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (just like in numpy itself).</item>
        /// </list>
        /// </para>
        /// <para>The input element type T1 must be a <i>numeric</i> value type, including <see cref="complex"/> and <see cref="fcomplex"/>. Some binary operations are also defined for string elements: equals '==' and not-equals '!='.</para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        public static Logical operator /*!HC:HCoperator*/ == (ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A, ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> B) {
            if (A is BaseArray<double>) {
                return Core.Functions.Builtin.InnerLoops.EqualTo.Double.Instance.operate(
                    A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>,
                    B as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>);
            } else if (A is BaseArray<float>) {
                return Core.Functions.Builtin.InnerLoops.EqualTo.Single.Instance.operate(
                    A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>,
                    B as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>);
                /*!HC:nocomplex*/
#if !NOCOMPLEX
            } else if (A is BaseArray<complex>) {
                return Core.Functions.Builtin.InnerLoops.EqualTo.Complex.Instance.operate(
                    A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>,
                    B as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>);
            } else if (A is BaseArray<fcomplex>) {
                return Core.Functions.Builtin.InnerLoops.EqualTo.FComplex.Instance.operate(
                    A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>,
                    B as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>);
#endif
            } else if (A is BaseArray<int>) {
                return Core.Functions.Builtin.InnerLoops.EqualTo.Int32.Instance.operate(
                    A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>,
                    B as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>);
            } else if (A is BaseArray<uint>) {
                return Core.Functions.Builtin.InnerLoops.EqualTo.UInt32.Instance.operate(
                A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>,
                B as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>);
            } else if (A is BaseArray<long>) {
                return Core.Functions.Builtin.InnerLoops.EqualTo.Int64.Instance.operate(
                A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>,
                B as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>);
#if !NOLOGICAL
            } else if (A is BaseArray<bool>) {
                return Core.Functions.Builtin.InnerLoops.EqualTo.Bool.Instance.operate(
                A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>,
                B as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>);
#endif
            } else if (A is BaseArray<ulong>) {
                return Core.Functions.Builtin.InnerLoops.EqualTo.UInt64.Instance.operate(
                A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>,
                B as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>);
            } else if (A is BaseArray<short>) {
                return Core.Functions.Builtin.InnerLoops.EqualTo.Int16.Instance.operate(
                A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>,
                B as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>);
            } else if (A is BaseArray<ushort>) {
                return Core.Functions.Builtin.InnerLoops.EqualTo.UInt16.Instance.operate(
                A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>,
                B as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>);
            } else if (A is BaseArray<sbyte>) {
                return Core.Functions.Builtin.InnerLoops.EqualTo.SByte.Instance.operate(
                A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>,
                B as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>);
            } else if (A is BaseArray<byte>) {
                return Core.Functions.Builtin.InnerLoops.EqualTo.Byte.Instance.operate(
                A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>,
                B as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>);
            } else if (object.Equals(A, null) || object.Equals(B, null)) {
                throw new ArgumentException($"The A == B operator is not defined for <null>-arrays. Found A: <{(Equals(A, null) ? "null" : "<" + typeof(T1).Name + ">")}>, B: {(Equals(B, null) ? "null" : "<" + typeof(T1).Name + ">")}");
#if !NO_STRING
            } else if (A is BaseArray<string>) {
                return A.Equals(B);
#endif
            } else {
                throw new ArgumentException($"The == operator is not defined for arrays of type <{typeof(T1).Name}>.");
            }
        }

        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

       

        /// <summary>Elementwise 'unequal to'  operator on two arrays.</summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The second array.</param>
        /// <returns>Logical array with the result of performing A != B.</returns>
        /// <remarks><para>General notes on binary broadcasting operators: The operation is performed 
        /// elementwise on corresponding elements of <paramref name="A"/> and <paramref name="B"/>. The sizes of both 
        /// input arrays must be broadcastable and determine the size of the resulting array. Note, that 
        /// broadcasting is performed, regardless of the current setting of <see cref="Settings.ArrayStyle"/>. However, 
        /// the <see cref="Settings.ArrayStyle"/> configuration affects the operation in the following ways: 
        /// <list type="bullet">
        /// <item>If <paramref name="A"/> and <paramref name="B"/> have different number of dimensions, the array with 
        /// fewer dimensions is (virtually) padded with singleton dimensions, until the number of dimensions of both arrays match. In 
        /// <see cref="ArrayStyles.ILNumericsV4"/> the singleton dimensions are added to the end of dimensions. For <see cref="ArrayStyles.numpy"/> 
        /// singletons are padded to the start, such that here, broadcasting aligns the <i>last</i> dimensions.</item>
        /// <item><see cref="ArrayStyles.ILNumericsV4"/>: Result values of integer element types T are clamped to the 
        /// natural range of the interger type. I.e.: there will be no 'wrapping around' when exceeding the upper or lower 
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="double"/> precision 
        /// floating point arithmetics and by subsequently casting the result to the desired integer value. With <see cref="ArrayStyles.numpy"/> array style no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (just like in numpy itself).</item>
        /// </list>
        /// </para>
        /// <para>The input element type T1 must be a <i>numeric</i> value type, including <see cref="complex"/> and <see cref="fcomplex"/>. Some binary operations are also defined for string elements: equals '!=' and not-equals '!='.</para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        public static Logical operator  != (ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A, ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> B) {
            if (A is BaseArray<double>) {
                return Core.Functions.Builtin.InnerLoops.UnEqualTo.Double.Instance.operate(
                    A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>,
                    B as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>);
            } else if (A is BaseArray<float>) {
                return Core.Functions.Builtin.InnerLoops.UnEqualTo.Single.Instance.operate(
                    A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>,
                    B as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>);
               
#if !NOCOMPLEX
            } else if (A is BaseArray<complex>) {
                return Core.Functions.Builtin.InnerLoops.UnEqualTo.Complex.Instance.operate(
                    A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>,
                    B as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>);
            } else if (A is BaseArray<fcomplex>) {
                return Core.Functions.Builtin.InnerLoops.UnEqualTo.FComplex.Instance.operate(
                    A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>,
                    B as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>);
#endif
            } else if (A is BaseArray<int>) {
                return Core.Functions.Builtin.InnerLoops.UnEqualTo.Int32.Instance.operate(
                    A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>,
                    B as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>);
            } else if (A is BaseArray<uint>) {
                return Core.Functions.Builtin.InnerLoops.UnEqualTo.UInt32.Instance.operate(
                A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>,
                B as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>);
            } else if (A is BaseArray<long>) {
                return Core.Functions.Builtin.InnerLoops.UnEqualTo.Int64.Instance.operate(
                A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>,
                B as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>);
#if !NOLOGICAL
            } else if (A is BaseArray<bool>) {
                return Core.Functions.Builtin.InnerLoops.UnEqualTo.Bool.Instance.operate(
                A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>,
                B as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>);
#endif
            } else if (A is BaseArray<ulong>) {
                return Core.Functions.Builtin.InnerLoops.UnEqualTo.UInt64.Instance.operate(
                A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>,
                B as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>);
            } else if (A is BaseArray<short>) {
                return Core.Functions.Builtin.InnerLoops.UnEqualTo.Int16.Instance.operate(
                A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>,
                B as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>);
            } else if (A is BaseArray<ushort>) {
                return Core.Functions.Builtin.InnerLoops.UnEqualTo.UInt16.Instance.operate(
                A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>,
                B as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>);
            } else if (A is BaseArray<sbyte>) {
                return Core.Functions.Builtin.InnerLoops.UnEqualTo.SByte.Instance.operate(
                A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>,
                B as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>);
            } else if (A is BaseArray<byte>) {
                return Core.Functions.Builtin.InnerLoops.UnEqualTo.Byte.Instance.operate(
                A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>,
                B as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>);
            } else if (object.Equals(A, null) || object.Equals(B, null)) {
                throw new ArgumentException($"The A != B operator is not defined for <null>-arrays. Found A: <{(Equals(A, null) ? "null" : "<" + typeof(T1).Name + ">")}>, B: {(Equals(B, null) ? "null" : "<" + typeof(T1).Name + ">")}");
#if !NO_STRING
            } else if (A is BaseArray<string>) {
                return !A.Equals(B);
#endif
            } else {
                throw new ArgumentException($"The != operator is not defined for arrays of type <{typeof(T1).Name}>.");
            }
        }


       

        /// <summary>Elementwise 'greater than'  operator on two arrays.</summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The second array.</param>
        /// <returns>Logical array with the result of performing A &gt; B.</returns>
        /// <remarks><para>General notes on binary broadcasting operators: The operation is performed 
        /// elementwise on corresponding elements of <paramref name="A"/> and <paramref name="B"/>. The sizes of both 
        /// input arrays must be broadcastable and determine the size of the resulting array. Note, that 
        /// broadcasting is performed, regardless of the current setting of <see cref="Settings.ArrayStyle"/>. However, 
        /// the <see cref="Settings.ArrayStyle"/> configuration affects the operation in the following ways: 
        /// <list type="bullet">
        /// <item>If <paramref name="A"/> and <paramref name="B"/> have different number of dimensions, the array with 
        /// fewer dimensions is (virtually) padded with singleton dimensions, until the number of dimensions of both arrays match. In 
        /// <see cref="ArrayStyles.ILNumericsV4"/> the singleton dimensions are added to the end of dimensions. For <see cref="ArrayStyles.numpy"/> 
        /// singletons are padded to the start, such that here, broadcasting aligns the <i>last</i> dimensions.</item>
        /// <item><see cref="ArrayStyles.ILNumericsV4"/>: Result values of integer element types T are clamped to the 
        /// natural range of the interger type. I.e.: there will be no 'wrapping around' when exceeding the upper or lower 
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="double"/> precision 
        /// floating point arithmetics and by subsequently casting the result to the desired integer value. With <see cref="ArrayStyles.numpy"/> array style no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (just like in numpy itself).</item>
        /// </list>
        /// </para>
        /// <para>The input element type T1 must be a <i>numeric</i> value type, including <see cref="complex"/> and <see cref="fcomplex"/>. Some binary operations are also defined for string elements: equals '>' and not-equals '!='.</para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        public static Logical operator  > (ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A, ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> B) {
            if (A is BaseArray<double>) {
                return Core.Functions.Builtin.InnerLoops.GreaterThan.Double.Instance.operate(
                    A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>,
                    B as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>);
            } else if (A is BaseArray<float>) {
                return Core.Functions.Builtin.InnerLoops.GreaterThan.Single.Instance.operate(
                    A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>,
                    B as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>);
               
#if !NOCOMPLEX
            } else if (A is BaseArray<complex>) {
                return Core.Functions.Builtin.InnerLoops.GreaterThan.Complex.Instance.operate(
                    A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>,
                    B as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>);
            } else if (A is BaseArray<fcomplex>) {
                return Core.Functions.Builtin.InnerLoops.GreaterThan.FComplex.Instance.operate(
                    A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>,
                    B as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>);
#endif
            } else if (A is BaseArray<int>) {
                return Core.Functions.Builtin.InnerLoops.GreaterThan.Int32.Instance.operate(
                    A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>,
                    B as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>);
            } else if (A is BaseArray<uint>) {
                return Core.Functions.Builtin.InnerLoops.GreaterThan.UInt32.Instance.operate(
                A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>,
                B as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>);
            } else if (A is BaseArray<long>) {
                return Core.Functions.Builtin.InnerLoops.GreaterThan.Int64.Instance.operate(
                A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>,
                B as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>);
#if NOLOGICAL
            } else if (A is BaseArray<bool>) {
                return Core.Functions.Builtin.InnerLoops.GreaterThan.Bool.Instance.operate(
                A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>,
                B as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>);
#endif
            } else if (A is BaseArray<ulong>) {
                return Core.Functions.Builtin.InnerLoops.GreaterThan.UInt64.Instance.operate(
                A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>,
                B as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>);
            } else if (A is BaseArray<short>) {
                return Core.Functions.Builtin.InnerLoops.GreaterThan.Int16.Instance.operate(
                A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>,
                B as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>);
            } else if (A is BaseArray<ushort>) {
                return Core.Functions.Builtin.InnerLoops.GreaterThan.UInt16.Instance.operate(
                A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>,
                B as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>);
            } else if (A is BaseArray<sbyte>) {
                return Core.Functions.Builtin.InnerLoops.GreaterThan.SByte.Instance.operate(
                A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>,
                B as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>);
            } else if (A is BaseArray<byte>) {
                return Core.Functions.Builtin.InnerLoops.GreaterThan.Byte.Instance.operate(
                A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>,
                B as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>);
            } else if (object.Equals(A, null) || object.Equals(B, null)) {
                throw new ArgumentException($"The A > B operator is not defined for <null>-arrays. Found A: <{(Equals(A, null) ? "null" : "<" + typeof(T1).Name + ">")}>, B: {(Equals(B, null) ? "null" : "<" + typeof(T1).Name + ">")}");
#if NO_STRING
            } else if (A is BaseArray<string>) {
                return ;
#endif
            } else {
                throw new ArgumentException($"The > operator is not defined for arrays of type <{typeof(T1).Name}>.");
            }
        }


       

        /// <summary>Elementwise 'lower or equal'  operator on two arrays.</summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The second array.</param>
        /// <returns>Logical array with the result of performing A â‰¤ B.</returns>
        /// <remarks><para>General notes on binary broadcasting operators: The operation is performed 
        /// elementwise on corresponding elements of <paramref name="A"/> and <paramref name="B"/>. The sizes of both 
        /// input arrays must be broadcastable and determine the size of the resulting array. Note, that 
        /// broadcasting is performed, regardless of the current setting of <see cref="Settings.ArrayStyle"/>. However, 
        /// the <see cref="Settings.ArrayStyle"/> configuration affects the operation in the following ways: 
        /// <list type="bullet">
        /// <item>If <paramref name="A"/> and <paramref name="B"/> have different number of dimensions, the array with 
        /// fewer dimensions is (virtually) padded with singleton dimensions, until the number of dimensions of both arrays match. In 
        /// <see cref="ArrayStyles.ILNumericsV4"/> the singleton dimensions are added to the end of dimensions. For <see cref="ArrayStyles.numpy"/> 
        /// singletons are padded to the start, such that here, broadcasting aligns the <i>last</i> dimensions.</item>
        /// <item><see cref="ArrayStyles.ILNumericsV4"/>: Result values of integer element types T are clamped to the 
        /// natural range of the interger type. I.e.: there will be no 'wrapping around' when exceeding the upper or lower 
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="double"/> precision 
        /// floating point arithmetics and by subsequently casting the result to the desired integer value. With <see cref="ArrayStyles.numpy"/> array style no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (just like in numpy itself).</item>
        /// </list>
        /// </para>
        /// <para>The input element type T1 must be a <i>numeric</i> value type, including <see cref="complex"/> and <see cref="fcomplex"/>. Some binary operations are also defined for string elements: equals '<=' and not-equals '!='.</para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        public static Logical operator  <= (ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A, ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> B) {
            if (A is BaseArray<double>) {
                return Core.Functions.Builtin.InnerLoops.LowerOrEqual.Double.Instance.operate(
                    A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>,
                    B as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>);
            } else if (A is BaseArray<float>) {
                return Core.Functions.Builtin.InnerLoops.LowerOrEqual.Single.Instance.operate(
                    A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>,
                    B as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>);
               
#if !NOCOMPLEX
            } else if (A is BaseArray<complex>) {
                return Core.Functions.Builtin.InnerLoops.LowerOrEqual.Complex.Instance.operate(
                    A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>,
                    B as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>);
            } else if (A is BaseArray<fcomplex>) {
                return Core.Functions.Builtin.InnerLoops.LowerOrEqual.FComplex.Instance.operate(
                    A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>,
                    B as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>);
#endif
            } else if (A is BaseArray<int>) {
                return Core.Functions.Builtin.InnerLoops.LowerOrEqual.Int32.Instance.operate(
                    A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>,
                    B as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>);
            } else if (A is BaseArray<uint>) {
                return Core.Functions.Builtin.InnerLoops.LowerOrEqual.UInt32.Instance.operate(
                A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>,
                B as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>);
            } else if (A is BaseArray<long>) {
                return Core.Functions.Builtin.InnerLoops.LowerOrEqual.Int64.Instance.operate(
                A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>,
                B as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>);
#if NOLOGICAL
            } else if (A is BaseArray<bool>) {
                return Core.Functions.Builtin.InnerLoops.LowerOrEqual.Bool.Instance.operate(
                A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>,
                B as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>);
#endif
            } else if (A is BaseArray<ulong>) {
                return Core.Functions.Builtin.InnerLoops.LowerOrEqual.UInt64.Instance.operate(
                A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>,
                B as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>);
            } else if (A is BaseArray<short>) {
                return Core.Functions.Builtin.InnerLoops.LowerOrEqual.Int16.Instance.operate(
                A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>,
                B as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>);
            } else if (A is BaseArray<ushort>) {
                return Core.Functions.Builtin.InnerLoops.LowerOrEqual.UInt16.Instance.operate(
                A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>,
                B as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>);
            } else if (A is BaseArray<sbyte>) {
                return Core.Functions.Builtin.InnerLoops.LowerOrEqual.SByte.Instance.operate(
                A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>,
                B as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>);
            } else if (A is BaseArray<byte>) {
                return Core.Functions.Builtin.InnerLoops.LowerOrEqual.Byte.Instance.operate(
                A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>,
                B as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>);
            } else if (object.Equals(A, null) || object.Equals(B, null)) {
                throw new ArgumentException($"The A <= B operator is not defined for <null>-arrays. Found A: <{(Equals(A, null) ? "null" : "<" + typeof(T1).Name + ">")}>, B: {(Equals(B, null) ? "null" : "<" + typeof(T1).Name + ">")}");
#if NO_STRING
            } else if (A is BaseArray<string>) {
                return ;
#endif
            } else {
                throw new ArgumentException($"The <= operator is not defined for arrays of type <{typeof(T1).Name}>.");
            }
        }


       

        /// <summary>Elementwise 'greater or equal'  operator on two arrays.</summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The second array.</param>
        /// <returns>Logical array with the result of performing A â‰¥ B.</returns>
        /// <remarks><para>General notes on binary broadcasting operators: The operation is performed 
        /// elementwise on corresponding elements of <paramref name="A"/> and <paramref name="B"/>. The sizes of both 
        /// input arrays must be broadcastable and determine the size of the resulting array. Note, that 
        /// broadcasting is performed, regardless of the current setting of <see cref="Settings.ArrayStyle"/>. However, 
        /// the <see cref="Settings.ArrayStyle"/> configuration affects the operation in the following ways: 
        /// <list type="bullet">
        /// <item>If <paramref name="A"/> and <paramref name="B"/> have different number of dimensions, the array with 
        /// fewer dimensions is (virtually) padded with singleton dimensions, until the number of dimensions of both arrays match. In 
        /// <see cref="ArrayStyles.ILNumericsV4"/> the singleton dimensions are added to the end of dimensions. For <see cref="ArrayStyles.numpy"/> 
        /// singletons are padded to the start, such that here, broadcasting aligns the <i>last</i> dimensions.</item>
        /// <item><see cref="ArrayStyles.ILNumericsV4"/>: Result values of integer element types T are clamped to the 
        /// natural range of the interger type. I.e.: there will be no 'wrapping around' when exceeding the upper or lower 
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="double"/> precision 
        /// floating point arithmetics and by subsequently casting the result to the desired integer value. With <see cref="ArrayStyles.numpy"/> array style no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (just like in numpy itself).</item>
        /// </list>
        /// </para>
        /// <para>The input element type T1 must be a <i>numeric</i> value type, including <see cref="complex"/> and <see cref="fcomplex"/>. Some binary operations are also defined for string elements: equals '>=' and not-equals '!='.</para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        public static Logical operator  >= (ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A, ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> B) {
            if (A is BaseArray<double>) {
                return Core.Functions.Builtin.InnerLoops.GreaterOrEqual.Double.Instance.operate(
                    A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>,
                    B as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>);
            } else if (A is BaseArray<float>) {
                return Core.Functions.Builtin.InnerLoops.GreaterOrEqual.Single.Instance.operate(
                    A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>,
                    B as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>);
               
#if !NOCOMPLEX
            } else if (A is BaseArray<complex>) {
                return Core.Functions.Builtin.InnerLoops.GreaterOrEqual.Complex.Instance.operate(
                    A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>,
                    B as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>);
            } else if (A is BaseArray<fcomplex>) {
                return Core.Functions.Builtin.InnerLoops.GreaterOrEqual.FComplex.Instance.operate(
                    A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>,
                    B as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>);
#endif
            } else if (A is BaseArray<int>) {
                return Core.Functions.Builtin.InnerLoops.GreaterOrEqual.Int32.Instance.operate(
                    A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>,
                    B as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>);
            } else if (A is BaseArray<uint>) {
                return Core.Functions.Builtin.InnerLoops.GreaterOrEqual.UInt32.Instance.operate(
                A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>,
                B as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>);
            } else if (A is BaseArray<long>) {
                return Core.Functions.Builtin.InnerLoops.GreaterOrEqual.Int64.Instance.operate(
                A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>,
                B as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>);
#if NOLOGICAL
            } else if (A is BaseArray<bool>) {
                return Core.Functions.Builtin.InnerLoops.GreaterOrEqual.Bool.Instance.operate(
                A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>,
                B as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>);
#endif
            } else if (A is BaseArray<ulong>) {
                return Core.Functions.Builtin.InnerLoops.GreaterOrEqual.UInt64.Instance.operate(
                A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>,
                B as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>);
            } else if (A is BaseArray<short>) {
                return Core.Functions.Builtin.InnerLoops.GreaterOrEqual.Int16.Instance.operate(
                A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>,
                B as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>);
            } else if (A is BaseArray<ushort>) {
                return Core.Functions.Builtin.InnerLoops.GreaterOrEqual.UInt16.Instance.operate(
                A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>,
                B as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>);
            } else if (A is BaseArray<sbyte>) {
                return Core.Functions.Builtin.InnerLoops.GreaterOrEqual.SByte.Instance.operate(
                A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>,
                B as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>);
            } else if (A is BaseArray<byte>) {
                return Core.Functions.Builtin.InnerLoops.GreaterOrEqual.Byte.Instance.operate(
                A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>,
                B as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>);
            } else if (object.Equals(A, null) || object.Equals(B, null)) {
                throw new ArgumentException($"The A >= B operator is not defined for <null>-arrays. Found A: <{(Equals(A, null) ? "null" : "<" + typeof(T1).Name + ">")}>, B: {(Equals(B, null) ? "null" : "<" + typeof(T1).Name + ">")}");
#if NO_STRING
            } else if (A is BaseArray<string>) {
                return ;
#endif
            } else {
                throw new ArgumentException($"The >= operator is not defined for arrays of type <{typeof(T1).Name}>.");
            }
        }


       

        /// <summary>Elementwise 'lower than' operator on two arrays.</summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The second array.</param>
        /// <returns>Logical array with the result of performing A &lt; B.</returns>
        /// <remarks><para>General notes on binary broadcasting operators: The operation is performed 
        /// elementwise on corresponding elements of <paramref name="A"/> and <paramref name="B"/>. The sizes of both 
        /// input arrays must be broadcastable and determine the size of the resulting array. Note, that 
        /// broadcasting is performed, regardless of the current setting of <see cref="Settings.ArrayStyle"/>. However, 
        /// the <see cref="Settings.ArrayStyle"/> configuration affects the operation in the following ways: 
        /// <list type="bullet">
        /// <item>If <paramref name="A"/> and <paramref name="B"/> have different number of dimensions, the array with 
        /// fewer dimensions is (virtually) padded with singleton dimensions, until the number of dimensions of both arrays match. In 
        /// <see cref="ArrayStyles.ILNumericsV4"/> the singleton dimensions are added to the end of dimensions. For <see cref="ArrayStyles.numpy"/> 
        /// singletons are padded to the start, such that here, broadcasting aligns the <i>last</i> dimensions.</item>
        /// <item><see cref="ArrayStyles.ILNumericsV4"/>: Result values of integer element types T are clamped to the 
        /// natural range of the interger type. I.e.: there will be no 'wrapping around' when exceeding the upper or lower 
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="double"/> precision 
        /// floating point arithmetics and by subsequently casting the result to the desired integer value. With <see cref="ArrayStyles.numpy"/> array style no such 
        /// precautions exist and the result of the binary operation is exactly the same as for operating the elements as System types (just like in numpy itself).</item>
        /// </list>
        /// </para>
        /// <para>The input element type T1 must be a <i>numeric</i> value type, including <see cref="complex"/> and <see cref="fcomplex"/>. Some binary operations are also defined for string elements: equals '<' and not-equals '!='.</para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        public static Logical operator  < (ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A, ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> B) {
            if (A is BaseArray<double>) {
                return Core.Functions.Builtin.InnerLoops.LowerThan.Double.Instance.operate(
                    A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>,
                    B as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>);
            } else if (A is BaseArray<float>) {
                return Core.Functions.Builtin.InnerLoops.LowerThan.Single.Instance.operate(
                    A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>,
                    B as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>);
               
#if !NOCOMPLEX
            } else if (A is BaseArray<complex>) {
                return Core.Functions.Builtin.InnerLoops.LowerThan.Complex.Instance.operate(
                    A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>,
                    B as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>);
            } else if (A is BaseArray<fcomplex>) {
                return Core.Functions.Builtin.InnerLoops.LowerThan.FComplex.Instance.operate(
                    A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>,
                    B as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>);
#endif
            } else if (A is BaseArray<int>) {
                return Core.Functions.Builtin.InnerLoops.LowerThan.Int32.Instance.operate(
                    A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>,
                    B as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>);
            } else if (A is BaseArray<uint>) {
                return Core.Functions.Builtin.InnerLoops.LowerThan.UInt32.Instance.operate(
                A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>,
                B as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>);
            } else if (A is BaseArray<long>) {
                return Core.Functions.Builtin.InnerLoops.LowerThan.Int64.Instance.operate(
                A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>,
                B as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>);
#if NOLOGICAL
            } else if (A is BaseArray<bool>) {
                return Core.Functions.Builtin.InnerLoops.LowerThan.Bool.Instance.operate(
                A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>,
                B as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>);
#endif
            } else if (A is BaseArray<ulong>) {
                return Core.Functions.Builtin.InnerLoops.LowerThan.UInt64.Instance.operate(
                A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>,
                B as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>);
            } else if (A is BaseArray<short>) {
                return Core.Functions.Builtin.InnerLoops.LowerThan.Int16.Instance.operate(
                A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>,
                B as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>);
            } else if (A is BaseArray<ushort>) {
                return Core.Functions.Builtin.InnerLoops.LowerThan.UInt16.Instance.operate(
                A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>,
                B as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>);
            } else if (A is BaseArray<sbyte>) {
                return Core.Functions.Builtin.InnerLoops.LowerThan.SByte.Instance.operate(
                A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>,
                B as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>);
            } else if (A is BaseArray<byte>) {
                return Core.Functions.Builtin.InnerLoops.LowerThan.Byte.Instance.operate(
                A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>,
                B as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>);
            } else if (object.Equals(A, null) || object.Equals(B, null)) {
                throw new ArgumentException($"The A < B operator is not defined for <null>-arrays. Found A: <{(Equals(A, null) ? "null" : "<" + typeof(T1).Name + ">")}>, B: {(Equals(B, null) ? "null" : "<" + typeof(T1).Name + ">")}");
#if NO_STRING
            } else if (A is BaseArray<string>) {
                return ;
#endif
            } else {
                throw new ArgumentException($"The < operator is not defined for arrays of type <{typeof(T1).Name}>.");
            }
        }


#endregion HYCALPER AUTO GENERATED CODE

    }
}
