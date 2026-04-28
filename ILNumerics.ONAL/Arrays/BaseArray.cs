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
using ILNumerics.Core.Misc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Xml;
using ILNumerics.Core.Arrays;

namespace ILNumerics {

    /// <summary>
    /// General base class for any array objects like <see cref="Array{T}"/> and similar arrays types. 
    /// </summary>
    /// <typeparam name="T">Element type</typeparam>
    public abstract partial class BaseArray<T> : BaseArray {

        /// <summary>
        /// Implicitly convert system array <paramref name="A"/> to a RetT array.
        /// </summary>
        /// <param name="A">System array of <see cref="System.ValueType"/> elements.</param>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        
        public static implicit operator BaseArray<T>(System.Array A) {

            var handle = Core.Functions.Builtin.MathInternal.New<T>((ulong)(object.Equals(A, null) ? 0 : A.LongLength));
            if (A is bool[,]) {
                var ret = LogicalStorage.Create();
                ret.Handles[0] = handle;
                ret.NumberTrues = LogicalStorage.MarshalConvert2Bool(A, handle, ret.Size);
                return ret.RetArray as BaseArray<T>;
            } else {
                // let marshal helper decide if T is struct
                var ret = Storage<T>.Create();
                ret.Handles[0] = handle;
                Storage<T>.MarshalCopy(A, handle, ret.Size);
                return ret.RetArray;
            }
        }



        /// <summary>
        /// Implicitly convert system scalar <paramref name="A"/> to a scalar array.
        /// </summary>
        /// <param name="A">Scalar value as <see cref="System.ValueType"/>.</param>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        public static implicit operator BaseArray<T>(T A) {
            if (A is bool) {
                return LogicalStorage.Create((bool)(object)A).RetArray as BaseArray<T>;
            } else {
                return Storage<T>.Create(A).RetArray;
            }
        }

        /// <summary>
        /// Convert arbitrary ILNumerics array to a scalar cell.
        /// </summary>
        /// <param name="A">Input array</param>
        /// <returns>Scalar cell storing a clone of <paramref name="A"/> as the only element.</returns>
        public static implicit operator Cell(BaseArray<T> A) {
            var ret = CellStorage.Create(A);
            ret.FromImplicitCast = true;
            return ret.RetArray;
        }
        /// <summary>
        /// Convert arbitrary ILNumerics array to a scalar (in)cell.
        /// </summary>
        /// <param name="A">Input array</param>
        /// <returns>Scalar cell storing a clone of <paramref name="A"/> as the only element.</returns>
        public static implicit operator InCell(BaseArray<T> A) {
            var ret = CellStorage.Create(A);
            ret.FromImplicitCast = true;

            return ret.GetInArray();
        }

        /// <summary>
        /// Writes a XML string in XML file
        /// </summary>
        /// <param name="writer"></param>
        public override void ToXML(XmlWriter writer) {
            using (Scope.Enter()) {
                Array<T> me = this.ToArray<T>();
                writer.WriteAttributeString("element_type", typeof(T).Name);
                writer.WriteAttributeString("Size", String.Join(",", me.shape));
                writer.WriteStartElement("ColumnMajor");
                foreach (var val in me.Iterator<T>(StorageOrders.ColumnMajor))
                    writer.WriteString(val.ToString() + ",");
                writer.WriteEndElement();
            }
        }

    }

    /// <summary>
    /// General base class of any ILNumerics array. This class is abstract. 
    /// </summary>
    public abstract partial class BaseArray : IDisposable {

        #region abstract public interface

        internal abstract IStorage GetIStorage();

        /// <summary>
        /// Test if this array instance is a column vector
        /// </summary>
        public abstract bool IsColumnVector { get; }
        /// <summary>
        /// Determines if this array is of complex inner type.
        /// </summary>
        public abstract bool IsComplex { get; }

        /// <summary>
        /// Determines, if this array is a cell array. Returns true for [In|Ret|Out|]Cell. False otherwise.
        /// </summary>
        public abstract bool IsCell { get; }

        /// <summary>
        /// This flag indicates that an array is not to be used anymore. 
        /// </summary>
        /// <remarks>
        /// <para>Since version 5 arrays implement multiple layers of reference 
        /// counting and share their resources more frequently. Users shall not dispose arrays 
        /// manually nor should there be any need to check for the life state of an array.</para>
        /// <para>After an object was disposed (regardless of the reason, automatically or manually)
        /// it shall not be referenced anymore. A call to <see cref="IsDisposed"/>, thus, is only allowed 
        /// for life arrays. On such arrays, <see cref="IsDisposed"/> always returns false, which makes 
        /// this property a constant, semantically. </para>
        /// </remarks>
        /// <seealso cref="Release()"/>
        [Obsolete("This method can only be called on live arrays. When called on a disposed object the return value is undefined.")]
        public virtual bool IsDisposed { get { return ReferenceCount == 0; } }
        /// <summary>
        /// Test if this instance is an empty array (number of elements stored eqals 0).
        /// </summary>
        public abstract bool IsEmpty { get; }
        /// <summary>
        /// Test if this instance is a matrix.
        /// </summary>
        /// <remarks>In order for an array to be a matrix the number of <b>non singleton</b> 
        /// dimensions must be at most 2. This attribute is readonly.
        /// <para>Keep in mind that depending on the current <see cref="Settings.ArrayStyle"/> all 
        /// arrays may have at least 2 dimensions. Thus, vectors and 
        /// scalar arrays can also be considered a matrix of size [n,1] / [1,n] or [1,1].</para></remarks>
        public abstract bool IsMatrix { get; }
        /// <summary>
        /// Determine if this array is of numeric inner type.
        /// </summary>
        public abstract bool IsNumeric { get; }
        /// <summary>
        /// Test if this array instance is a row vector.
        /// </summary>
        public abstract bool IsRowVector { get; }
        /// <summary>
        /// Test if this instance is a scalar.
        /// </summary>
        /// <remarks>This attribute is readonly. It returns: Size.NumberOfElements == 1.</remarks>
        public abstract bool IsScalar { get; }

        /// <summary>
        /// Tests if this array stores elements of type <typeparamref name="ElementType"/>. 
        /// </summary>
        /// <typeparam name="ElementType">Type to test against the actual element type of this array.</typeparam>
        /// <returns>True if the elements of this array are of type <typeparamref name="ElementType"/>. 
        /// False otherwise.</returns>
        /// <remarks><para>Regular dense arrays can be tested for a specific type by providing the assumed 
        /// element type as generic type parameter <typeparamref name="ElementType"/>.</para>
        /// <para>To test if an array is a logical array, provide <see cref="System.Boolean"/> as 
        /// <typeparamref name="ElementType"/>.</para>
        /// <para>In order to test for cell types an <typeparamref name="ElementType"/> of <see cref="BaseArray"/>
        /// is to be provided.</para></remarks>
        public abstract bool IsOfType<ElementType>();

        /// <summary>
        /// Test if this array is a vector.
        /// </summary>
        /// <remarks>In order for an array to be a vector the number of <b>non singleton</b> 
        /// dimensions must equal 1. Keep in mind that all Arrays have at least 2 dimensions. Therefore 
        /// it is not sufficient to test for the number of dimensions, but to take the number of 
        /// <b>non singleton</b> dimensions into account. This attribute is readonly.</remarks>
        public abstract bool IsVector { get; }
        /// <summary>
        /// Length of the longest dimension of this instance
        /// </summary>
        /// <remarks>This property is readonly.</remarks>
        public abstract long Length { get; }
        /// <summary>
        /// Size descriptor determining the number, lengths and strides of the dimensions of this array. Alias to <see cref="Size"/>.
        /// </summary>
        public abstract Size S { get; }
        /// <summary>
        /// Size descriptor determining the number, lengths and strides of the dimensions of this array.
        /// </summary>
        public abstract Size Size { get; }

        /// <summary>
        /// Compare elements and shape of this array with another array.
        /// </summary>
        /// <param name="A">Other array</param>
        /// <returns>true if shape and element values of both arrays match, false otherwise</returns>
        /// <remarks><para>'Equals' accepts two vectors even if the orientations do not match. Therefore, a row vector 
        /// with the same element values than another column vector of the same lengths are considered to be equal to each other.</para></remarks>
        public abstract override bool Equals(object A);
        /// <summary>
        /// Default implementation of GetHashCode(). Concrete classes implement their own versions. 
        /// </summary>
        /// <returns>Default hash code of this instance. </returns>
        public override int GetHashCode() {
            return base.GetHashCode();
        }
        /// <summary>
        /// Gets the System.Type of the elements stored in this array.
        /// </summary>
        /// <returns>Sytem.Type of the generic argument of concreate subclass instances</returns>
        public abstract Type GetElementType();

        /// <summary>
        /// Short textual summary of this instance, used for debug output
        /// </summary>
        /// <returns>String representation of type and size</returns>
        /// <remarks>The type of elements and the size of the array are displayed. If the array
        /// is scalar, its value is displayed next to the type.</remarks>
        public abstract string ShortInfo();
        /// <summary>
        /// Short textual summary of this instance, select individual info components.
        /// </summary>
        /// <returns>String representation of type, size, values, storage order as requested.</returns>
        /// <remarks>The type of elements and the size of the array are displayed. If the array
        /// is scalar, its value is displayed next to the type.</remarks>
        public abstract string ShortInfo(bool includeType = true, bool includeSize = true, bool includeValues = true, bool includeStorageOrder = true, bool includeDevice = false);

        /// <summary>
        /// Gives detailed info about current configuration of this storage. Includes state, type, size, handles, reference counts, and buffer info. (Not threadsafe) 
        /// </summary>
        /// <returns>String describing the configuration of this storage.</returns>
        public abstract string Info();

        /// <summary>
        /// Creates a textual representation of a derived array and allows to control display parameters.
        /// </summary>
        /// <param name="maxNumberElementsPerDimension">[Optional] Maximum number of elements displayed in each dimension. Default: 50. Use 0 to remove this limit.</param>
        /// <param name="maxNumberElements">[Optional] Overall maximum number of elements displayed. Default: 1000. Use 0 to remove this limit.</param>
        /// <param name="style">[Optional] Show the elements in row major or column major order. Default: <see cref="Settings.DefaultStorageOrder"/> (i.e.: currently selected ArrayStyle).</param>
        /// <param name="showType">[Optional] Add a header info with the element type. Default: true.</param>
        /// <param name="showSize">[Optional] Add a header info with the arrays size. Default: true.</param>
        /// <param name="columnWidth">[Optional] Controls the width of element columns (number of characters). Default: (auto, depends on type).</param>
        /// <param name="scaleFP">[Optional] true: automatically scale element values up / down for prettier display (default). False: do not scale element values.</param>
        /// <param name="waitForCompletion">[Optional] true: wait until the array is completely configured. False: display result based on the current state. Default: true.</param>
        /// <returns>String with the content of this array converted according to the given parameters.</returns>
        public virtual string ToString(uint maxNumberElementsPerDimension = 50, uint maxNumberElements = 10000, StorageOrders? style = null, bool showType = true, bool showSize = true, int? columnWidth = null, bool scaleFP = true, bool waitForCompletion = true) {
            return (this as object).ToString();
        }

        /// <summary>
        /// Write values of this instance to a stream. 
        /// </summary>
        /// <param name="outStream">Stream to write the values into.</param>
        /// <param name="format">Format string to be used for output. See <see cref="System.String.Format(string,object)"/> for a specification
        /// of valid formating expressions. This flag is only used, when 'method' is set to 'Serial'.</param>
        /// <param name="method">A constant out of <see cref="ArrayStreamSerializationFlags"/>. Specifies the way 
        /// the values will be serialized.</param>
        /// <remarks><para>If method 'Formatted' is used, any occurences of NewLine character(s) 
        /// will be replaced from the format string before applying to the elements. This is done to 
        /// prevent the format from breaking the 'page' style for the output.</para>
        /// <para>If 'method' is set to 'Matlab', the array will be written as Matfile version 5.0. No compression will be used. The internal 'Name' property will be used as 
        /// the array name for writing. This array instance will be the only array in the mat file. If you want to write several arrays bundled into one mat file, use the MatFile class to
        /// create a collection of arrays and write the MatFile to stream. Or use the classes in the 
        /// ILNumerics.IO.HDF5 namespace to create a HDF5 format file (recommended).</para></remarks>
        [Obsolete("Consider using ILNumerics.IO.HDF5 or ILNumerics.MatFile instead!")]
        public abstract void ToStream(Stream outStream, string format, ArrayStreamSerializationFlags method);

        /// <summary>
        /// Increase the internal reference counter for this array object. 
        /// </summary>
        internal abstract void Retain();
        /// <summary>
        /// Decrease the internal reference counter for this array object. The array will be disposed once the reference counter reaches 0.
        /// </summary>
        internal abstract void Release();
        
        /// <summary>
        /// Disposes this array - no matter what. Derived types are free to release the memory to the OS or to the ILNumerics memory pool.
        /// </summary>
        public abstract void Dispose();
        /// <summary>
        /// Counts the number of arrays currently referencing the storage of this array. 
        /// </summary>
        public abstract int ReferenceCount {
            get;
        }

        #endregion

        #region implicit casts

        /// <summary>
        /// Implicit cast from <see cref="System.Double"/> scalar to <see cref="Array{T1}"/> of element type <see cref="double"/>.
        /// </summary>
        /// <param name="a">Input scalar.</param>
        /// <returns>A RetArray of same element type as <paramref name="a"/> and size 1x1.</returns>
        public static implicit operator BaseArray(double a) {
            return Storage<double>.Create(a).RetArray;
        }

        /* Uncommented ho, 2018-01-12: this would create many ambigous implicit cast warnings...
        ///// <summary>
        ///// Implicit cast from <see cref="System.Double"/> scalar to <see cref="RetArray{T1}"/> of element type <see cref="int"/>.
        ///// </summary>
        ///// <param name="a">Input scalar.</param>
        ///// <returns>A RetArray of same element type as <paramref name="a"/> and size 1x1.</returns>
        //public static implicit operator BaseArray(int a) {
        //    return Storage<int>.Create(a).RetArray;
        //}
        */

        /// <summary>
        /// Implicit cast from scalar of typeof(A) to RetArray&lt;typeof(A)&gt;
        /// </summary>
        /// <param name="a">Input scalar</param>
        /// <returns>A RetArray of same type as <paramref name="a"/> ans size 1x1</returns>
        public static implicit operator BaseArray(complex a) {
            return Storage<complex>.Create(a).RetArray;
        }
        /// <summary>
        /// Implicit cast from scalar of typeof(A) to RetArray&lt;typeof(A)&gt;
        /// </summary>
        /// <param name="a">Input scalar</param>
        /// <returns>A RetArray of same type as <paramref name="a"/> ans size 1x1</returns>
        public static implicit operator BaseArray(fcomplex a) {
            return Storage<fcomplex>.Create(a).RetArray;
        }
        /// <summary>
        /// Implicit cast from string to scalar RetArray&lt;string&gt;
        /// </summary>
        /// <param name="s">Input scalar</param>
        /// <returns>A RetArray of same type as <paramref name="s"/> and size 1x1</returns>
        public static implicit operator BaseArray(string s) {
            return Storage<string>.Create(s).RetArray;
        }

        /// <summary>
        /// Wraps an ILNumerics expression (i.e.: simple operation with 'end' specifier) into a scalar array.
        /// </summary>
        /// <param name="s">Input expression</param>
        /// <returns>A RetArray of same type as <paramref name="s"/> and size 1x1 (array scalar in numpy ArrayStyle).</returns>
        public static implicit operator BaseArray(ILExpression s) {
            return Storage<ILExpression>.Create(s).RetArray;
        }

        #endregion

        #region helpers

        /// <summary>
        /// Writes a XML string in XML file
        /// </summary>
        /// <param name="writer"></param>
        public abstract void ToXML(XmlWriter writer);

        #endregion
    }
}
