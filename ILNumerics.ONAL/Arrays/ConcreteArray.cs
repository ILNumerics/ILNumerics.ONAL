using ILNumerics.Core.Global;
using ILNumerics.Core.MemoryLayer;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace ILNumerics.Core.Arrays
{

    /// <summary>
    /// Abstract base class for concrete array implementations of type <see cref="Array{T1}"/>, <see cref="OutArray{T}"/>, <see cref="InArray{T}"/> and their conterparts for <see cref="Logical"/> and <see cref="Cell"/>. This class is not intended to be used directly. Use the concrete derived array classes instead! 
    /// </summary>
    /// <typeparam name="T1">The element type.</typeparam>
    /// <typeparam name="LocalT">The type of the local arrays: one of <see cref="Array{T1}"/>, <see cref="Logical"/> or <see cref="Cell"/>.</typeparam>
    /// <typeparam name="InT">The type of the input arrays: <see cref="InArray{T1}"/>, <see cref="InLogical"/>, or <see cref="InCell"/> respectively.</typeparam>
    /// <typeparam name="OutT">The type of the output arrays: <see cref="OutArray{T1}"/>, <see cref="OutLogical"/>, or <see cref="OutCell"/> respectively.</typeparam>
    /// <typeparam name="RetT">The type of the return arrays: <see cref="Array{T1}"/>, <see cref="RetLogical"/>, or <see cref="RetCell"/> respectively.</typeparam>
    /// <typeparam name="StorageT">The concrete type of the internal storage object, determining all array types involved. This is derived from <see cref="BaseStorage{T, LocalT, InT, OutT, RetT, StorageT}"/>.</typeparam>
    /// <remarks>This class implements most of the abstract base class <see cref="BaseArray"/> public API. It also implements the public (strongly typed) API for 
    /// mutable arrays and the non-volatile input array types (<see cref="InArray{T1}"/>, ...).</remarks>
    /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}"/>
    [System.Diagnostics.DebuggerDisplay("{ShortInfo(),nq}")]
    [System.Diagnostics.DebuggerTypeProxy(typeof(Global.ArrayDebuggerProxy2<,,,,,>))]
    public abstract partial class ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>   // T1 must not read "T" to prevent from conflicts with the .T (transpose) property.
        : BaseArray<T1>, IEnumerable<T1>
        where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {

        #region attributes
        /// <summary>
        /// The internal storage reference.
        /// </summary>
        protected internal StorageT m_storage;

        /// <summary>
        /// Gives access to the storage object holding the actual data of this array. (Does not release RetArray.)
        /// </summary>
        /// <remarks><para>The following storage objects exist currently: <see cref="Storage{T}"/> 
        /// (used by <see cref="Array{T}"/> and Co.), <see cref="LogicalStorage"/> 
        /// (used by <see cref="Logical"/> and Co.), and <see cref="CellStorage"/> 
        /// (used by <see cref="Cell"/> and Co.).</para>
        /// <para>During normal use accessing the storage object of the array is commonly
        /// not neccessary. The direct access and utilization of these objects is left to the experienced 
        /// user !!</para>
        /// <para>For <see cref="RetArray{T}"/> this getter does not release the storage object!
        /// In fact, this is a rare (the only?) exception of a public function not releasing this array!</para>
        /// </remarks>
        internal StorageT Storage {
            get {
                System.Diagnostics.Debug.Assert(m_storage != null);
                // all public API (except some exception, ToString() etc.) access the storage over Storage. T
                // This does not block until the storage is completely configured, to enable access to pending 
                // storages, too. Users of a pending storage, however, cannot access 'Size' nor 'Handles' of
                // the storage via public access without waiting for them to becaome ready. 

                //while (!m_storage.IsReady) {
                //    Thread.Sleep(0); 
                //}
                return m_storage;
            }
        }
        internal static T1 ElementInstance = default(T1);

        internal override IStorage GetIStorage() { return m_storage; }
        /// <summary>
        /// Gets the number of bytes spanned by a single element of this array. 
        /// </summary>
        /// <remarks><para>This value corresponds to the value computed by <see cref="Marshal.SizeOf{T}(T)"/> with the following exceptions: 
        /// for <typeparamref name="T1"/> of 'bool' 1 is returned. Referencd types return <see cref="IntPtr.Size"/>.</para></remarks>
        public static readonly uint SizeOfT = BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>.ComputeSizeOfT();
        #endregion

        #region constructors
        internal ConcreteArray(StorageT storage) {
            m_storage = storage;
        }

        #endregion

        #region BaseArray interface implementation -> redirects to storage

        /// <summary>
        /// Gets the dimension descriptor for this array. [readonly]
        /// </summary>
        public override Size Size => m_storage.Size;

        /// <summary>
        /// Gets the dimension descriptor for this array (readonly). This is an alias for <see cref="Size"/>.
        /// </summary>
        public override Size S => m_storage.Size;  // <- Readonly delegation to 'Size' property getter.

        /// <summary>
        /// Determines whether this array represents a column vector.
        /// </summary>
        /// <remarks>An array is considered a column vector when all elements are stored in the 
        /// first dimension (dimension index 0).</remarks>
        public override bool IsColumnVector {
            get {
                using var _1 = ReaderLock.Create(this, out StorageT storage);

                return storage.Size[0] == storage.Size.NumberOfElements;
            }
        }

        /// <summary>
        /// Returns true if elements of this array store a real and an imaginary part each. 
        /// </summary>
        /// <remarks>Currently, the element types <see cref="ILNumerics.complex"/>, <see cref="ILNumerics.fcomplex"/>, 
        /// and <see cref="System.Numerics.Complex"/> are considered here .</remarks>
        public override bool IsComplex => ElementInstance is complex || ElementInstance is fcomplex || ElementInstance is System.Numerics.Complex;

        /// <summary>
        /// Determines, if this array is a cell array. Returns true for [In|Ret|Out|]Cell. False otherwise.
        /// </summary>
        public override bool IsCell => IsNumeric || ElementInstance is bool;  // IsNumeric releases RetT!

        /// <summary>
        /// Determines whether this array stores no elements. 
        /// </summary>
        public override bool IsEmpty {
            get {
                using var _1 = ReaderLock.Create(this, out StorageT storage);

                return storage.Size.NumberOfElements == 0;
            }
        }

        /// <summary>
        /// Determines whether this array is a matrix. 
        /// </summary>
        /// <remarks>A matrix has less than three dimensions.</remarks>
        public override bool IsMatrix {
            get {
                using var _1 = ReaderLock.Create(this, out StorageT storage);

                return storage.Size.NumberOfDimensions <= 2; // || m_storage.Size.NonSingletonDimensions == 2;
            }
        }

        /// <summary>
        /// Gets whether this array stores elements of a numeric type.
        /// </summary>
        /// <remarks>The list of such types considered 'numeric': <see cref="Double"/>, 
        /// <see cref="Single"/>
        /// <see cref="complex"/>
        /// <see cref="fcomplex"/>
        /// <see cref="System.Numerics.Complex"/>
        /// <see cref="Byte"/>
        /// <see cref="sbyte"/>
        /// <see cref="Char"/>
        /// <see cref="Int16"/>
        /// <see cref="Int32"/>
        /// <see cref="Int64"/>
        /// <see cref="UInt16"/>
        /// <see cref="UInt32"/>
        /// <see cref="UInt64"/>. All other types are considered 'non-numeric'.
        /// </remarks>
        public override bool IsNumeric {
            get {
                return (ElementInstance is double ||
                        ElementInstance is float ||
                        ElementInstance is complex ||
                        ElementInstance is fcomplex ||
                        ElementInstance is System.Numerics.Complex ||
                        ElementInstance is byte ||
                        ElementInstance is sbyte ||
                        ElementInstance is char ||
                        ElementInstance is Int16 ||
                        ElementInstance is Int32 ||
                        ElementInstance is Int64 ||
                        ElementInstance is UInt16 ||
                        ElementInstance is UInt32 ||
                        ElementInstance is UInt64);
            }
        }

        /// <summary>
        /// Tests if this base array stores elements of type <typeparamref name="ElementType"/>. 
        /// </summary>
        /// <typeparam name="ElementType">Type to test against the actual element type of this array.</typeparam>
        /// <returns>True if the elements of this array are of type <typeparamref name="ElementType"/>. False otherwise.</returns>
        public override bool IsOfType<ElementType>() {

            if (typeof(ElementType) == typeof(System.Boolean)) {
                return m_storage is LogicalStorage;
            }
            if (typeof(ElementType) == typeof(BaseArray)) {
                return m_storage is CellStorage;
            }
            return typeof(T1) == typeof(ElementType);
        }

        /// <summary>
        /// Gets whether this array represents a row vector [readonly].
        /// </summary>
        /// <remarks><para>An array is considered a row vector, if all of the following is true: 
        /// its first dimension (index 0) has lengths 1 and the array has more than one dimension
        /// and all elements are stored in the second dimension.</para>
        /// </remarks>
        /// <seealso cref="Length"/>
        /// <seealso cref="IsVector"/>
        /// <seealso cref="IsColumnVector"/>
        public override bool IsRowVector {
            get {
                using var _1 = ReaderLock.Create(this, out StorageT storage);
                return (storage.Size[0] == 1 && storage.Size.NumberOfDimensions > 1 && storage.S[1] == storage.Size.NumberOfElements);  // ho:WTF..!?? ->  || IsEmpty; 
            }
        }

        /// <summary>
        /// Gets whether this array has exactly one element.
        /// </summary>
        public override bool IsScalar {
            get {
                using var _1 = ReaderLock.Create(this, out StorageT storage);
                return storage.Size.NumberOfElements == 1;
            }
        }

        /// <summary>
        /// Determines if this array represents a vector. 
        /// </summary>
        /// <remarks>An array is considered a vector when one of the following is true: the array has exactly one dimension, 
        /// or the array has exactly one non-singleton dimension, or the 
        /// array has exactly 1 element.</remarks>
        public override bool IsVector {
            get {
                using var _1 = ReaderLock.Create(this, out StorageT storage);
                return storage.Size.NumberOfDimensions == 1 ||
                storage.Size.NonSingletonDimensions == 1 ||
                storage.Size.NumberOfElements == 1;
            }
        }

        /// <summary>
        /// Gets the number of elements stored in the longest dimension [readonly].
        /// </summary>
        public override long Length {
            get {
                using var _1 = ReaderLock.Create(this, out StorageT storage);
                return storage.Size.Longest;
            }
        }
        

        /// <summary>
        /// Gets a <see cref="System.Type"/> object describing the element type <typeparamref name="T1"/>.
        /// </summary>
        /// <returns>The element <see cref="Type"/>.</returns>
        public override Type GetElementType() {
            return typeof(T1); 
        }

        /// <summary>
        /// Gives detailed info about the current configuration of this storage. 
        /// </summary>
        /// <returns>String with description of this storage.</returns>
        public override string Info() {
            using var _1 = ReaderLock.Create(this, out StorageT storage);
            return storage.Info();
        }
        /// <summary>
        /// Converts this array into a short textual representation, displaying the type, shape and a few element values.
        /// </summary>
        /// <returns>A short textual representation of this array.</returns>
        /// <remarks>The type of elements and the size of the array are displayed. If the array
        /// is scalar, its value is displayed next to the type.
        /// <para>This method does not release the array's storage to aid debugging purposes.</para></remarks>
        public override string ShortInfo() {
            using var _1 = ReaderLock.Create(this, out StorageT storage, releaseRetT: false);
            return storage.ShortInfo();
        }
        /// <summary>
        /// Short textual summary of this instance, select individual info components. Releases this storage on return type arrays.
        /// </summary>
        /// <returns>String representation of type, size, values, storage order as requested.</returns>
        /// <remarks>The type of elements and the size of the array are displayed. If the array
        /// is scalar, its value is displayed next to the type.</remarks>
        public override string ShortInfo(bool includeType = true, bool includeSize = true, bool includeValues = true, bool includeStorageOrder = true, bool includeDevice = true) {
            using var _1 = ReaderLock.Create(this, out StorageT storage);
            return storage.ShortInfo(includeType, includeSize, includeValues, includeStorageOrder, includeDevices: includeDevice); 
        }

        internal bool IsSTA => m_storage?.IsSTA ?? false;

        /// <summary>
        /// [ExpertLevel] Gets the stale value of the id of the array component addressed by <paramref name="target"/> index. This function is part of the expert level interface and shall be used for testing only! 
        /// </summary>
        /// <param name="target">[Optional] The index of the target component. 0 (default): array component (currently not tracked), 1: storage, 2: buffer set, 3...(dev+3): buffers #0...max(#dev).</param>
        /// <returns>The current ID of the targeted component. Positive or 0 on success. Negative, if the component does not exist or does not maintain an ID.</returns>
        /// <remarks><para>This function is thread safe. It returns the indicated ID of the component _currently_ attached to this array. Note, that a mutable array may be changed by a concurrent thread at anytime without proper synchronization.</para></remarks>
        public IntPtr GetID(int target = 1) {
            var storage = m_storage; 
            if (storage == null) {
                return new IntPtr(-1); 
            }
            switch (target) {
                case 1:
                    return new IntPtr(storage.ID);
                case 2:
                    return new IntPtr(storage.Handles?.ID ?? -1);
                default:
                    return storage.Handles.GetID(target - 3); 
            }
        }
        /// <summary>
        /// Convert and store content of this array into a stream. This function is obsolete.
        /// </summary>
        /// <param name="outStream"></param>
        /// <param name="format"></param>
        /// <param name="method"></param>
        /// <remarks><para>This method is thread safe.</para></remarks>
        [Obsolete("Use the classes in ILNumerics.IO.HDF5 or ILNumerics.MatFile instead!")]
        public override void ToStream(Stream outStream, string format, ArrayStreamSerializationFlags method) {
            
            using var _1 = ReaderLock.Create(this, out StorageT storage);
            storage.ToStream(outStream, format, method);

        }

        /// <summary>
        /// Create textual description of this array. Limits the number of elements shown for very large arrays.
        /// </summary>
        /// <returns>String describing this array.</returns>
        /// <remarks><para>This function creates a textual representation of the array, describing the dimensionality, size and element values.</para>
        /// <para>Elements of this array are displayed in a tabular form, corresponding to slices/subarrays of the whole array. Each table contains the 
        /// elements along the first two dimension (<see cref="Settings.DefaultStorageOrder"/> equal to <see cref="StorageOrders.ColumnMajor"/>), or along 
        /// the last two dimensions (<see cref="Settings.DefaultStorageOrder"/> equal to <see cref="StorageOrders.RowMajor"/>) respectively. Note, that 
        /// a column major- or respective row major order representation is used according to the current setting of <see cref="Settings.ArrayStyle"/> - the 
        /// storage order of the array itself is not considered! </para>
        /// <para>Very large arrays are abreviated so that no more than a certain number of elements are displayed per dimension. 
        /// This is achieved by displaying elements at the beginning and at the end of a respective dimension only. Furthermore, a hard limit according to the value of <see cref="Settings.ToStringMaxNumberElements"/>
        /// applies. ILNumerics will try to respect this limit by limiting the number of elements displayed in any dimension. I.e. the value of 
        /// <see cref="Settings.ToStringMaxNumberElementsPerDimension"/> is adjusted as necessary.</para>
        /// <para>If this method is called on an array which is not yet fully configured (ILNumerics Virtual Processor) the function will halt and wait 
        /// until configuration is finished and until all values become available. Use <see cref="ToString(uint, uint, StorageOrders?, bool, bool, int?, bool, bool)"/> 
        /// to allow inspecting unfinished array contents.</para>
        /// <para>The display is optimized for common numeric element types.</para>
        /// <para>This method is thread safe.</para>
        /// </remarks>
        /// <seealso cref="ToString(uint, uint, StorageOrders?, bool, bool, int?, bool, bool)"/>
        /// <seealso cref="Settings.ToStringMaxNumberElements"/>
        /// <seealso cref="Settings.ToStringMaxNumberElementsPerDimension"/>
        public override string ToString() {

            using var _1 = ReaderLock.Create(this, out StorageT storage);
            return string.Join(
                Environment.NewLine,
                storage.ToString(Settings.ToStringMaxNumberElementsPerDimension, Settings.ToStringMaxNumberElements, Settings.DefaultStorageOrder, true, true, null)); 
        }


        /// <summary>
        /// Creates a textual representation of this array and allows to control display parameters.
        /// </summary>
        /// <param name="maxNumberElementsPerDimension">Maximum number of elements displayed for any dimension.</param>
        /// <param name="maxNumberElements">[Optional] Overall maximum number of elements displayed. Default: 10000. Use 0 to remove this limit.</param>
        /// <param name="style">[Optional] Show the elements in row major or column major order. Default: <see cref="Settings.DefaultStorageOrder"/> (i.e.: currently selected ArrayStyle).</param>
        /// <param name="showType">[Optional] Add a header info with the element type. Default: true.</param>
        /// <param name="showSize">[Optional] Add a header info with the arrays size. Default: true.</param>
        /// <param name="columnWidth">[Optional] Controls the width of element columns (number of characters). Default: (auto, depends on type).</param>
        /// <param name="scaleFP">[Optional] true: automatically scale element values up / down for prettier display (default). False: do not scale element values.</param>
        /// <param name="waitForCompletion">[Optional] true: block execution until the content of this array is completely configured (default). False: generate info for intermediate state of this array, displays only such details, already configured.</param>
        /// <returns>String with the content of this array converted according to the given parameters.</returns>
        /// <remarks><para>The function converts this array into a string for interactive display, for informal display and for utilization in interactive debug sessions.</para>
        /// <para>Large arrays will be converted into a shorter form to save resources. The number of elements per dimension is limited to the value <paramref name="maxNumberElementsPerDimension"/>. 
        /// Per default only 50 elements are shown from each dimension; 25 from the beginning and 25 from the end of each dimension. The value for <paramref name="maxNumberElementsPerDimension"/>
        /// may be adjusted to create smaller or larger output. 0 as value for <paramref name="maxNumberElementsPerDimension"/> removed the limit and shows all elements up to the number 
        /// of <see cref="uint.MaxValue"/>.</para>
        /// <para>The parameter <paramref name="maxNumberElements"/> limits the overall number of elements displayed, regardless of the actual dimension lengths. This parameter 
        /// takes precedence over <paramref name="maxNumberElementsPerDimension"/>: if the array has too many dimensions and displaying all elements would exceed the number given by <paramref name="maxNumberElements"/> 
        /// then the value of <paramref name="maxNumberElementsPerDimension"/> will automatically by adjusted (decreased) to keep the overall number of elements within the limits of <paramref name="maxNumberElements"/>.</para>
        /// <para>0 as value for <paramref name="maxNumberElements"/> removes the limit of overall number of elements for the output. In order to display all elements 
        /// of the array use <c>ToString(0,0)</c>. Caution: for very large arrays this may use a lot of resources and may take long!</para>
        /// <para>The elements of this array are displayed as "pages" or matrices. By default, and according to the current setting of <see cref="Settings.DefaultStorageOrder"/> these pages 
        /// visualize slices of the array along the first and second dimension (<see cref="StorageOrders.ColumnMajor"/>). Or the pages are created from the last two 
        /// dimensions (<see cref="StorageOrders.RowMajor"/>). The <paramref name="style"/> parameter allows to explicitly control if the display should follow row major or 
        /// column major semantics.</para>
        /// <para>The parameters <paramref name="showSize"/> and <paramref name="showType"/> - if true - add a short info about the arrays size and type to the header (beginning) of the output.</para>
        /// <para><paramref name="columnWidth"/> allows to control the number of characters each element column of the output pages created will span. By default, this width 
        /// is automatically adjusted. For most numeric element types this width is 12.</para>
        /// <para>The signature of this function has been adjusted in version 5.4. The first parameter has been made [required]. The reason is that in some 
        /// cases the overload resolution of the C# compiler has been observed to fail to prefer the parameterless version of <see cref="object.ToString()"/> over this function. </para>
        /// <para>This method is thread safe. If elements of this array exist on non-host storage only, they will transparently be copied to the host memory.</para>
        /// </remarks>
        /// <seealso cref="ToString()"/>
        /// <seealso cref="Settings.ToStringMaxNumberElements"></seealso>
        /// <seealso cref="Settings.ToStringMaxNumberElementsPerDimension"/>
        public override string ToString(uint maxNumberElementsPerDimension, uint maxNumberElements = 10000, StorageOrders? style = null, bool showType = true, bool showSize = true, int? columnWidth = null, bool scaleFP = true, bool waitForCompletion = true) {

            using var _1 = ReaderLock.Create(this, out StorageT storage);
            return string.Join(
                Environment.NewLine,
                storage.ToString(
                    maxNumberElementsPerDimension, 
                    maxNumberElements, 
                    style ?? Settings.DefaultStorageOrder,
                    showType, 
                    showSize,
                    columnWidth,
                    scaleFP, 
                    waitForCompletion)); 
        }

        internal override void Retain() {
            m_storage.Retain();
        }
        internal override void Release() {
            m_storage.Release();
        }

        /// <summary>
        /// Releases this array after use. Cleans up on <see cref="RetArray{T1}"/>, <see cref="RetCell"/> and <see cref="RetLogical"/> arrays which are not otherwise 'utilized'.
        /// </summary>
        /// <remarks>This method is used in rare situations where the return value from an ILNumerics method is not used or in the context of class attributes. Let's say: the user 
        /// called a method with <see cref="OutArray{T}"/> parameters and only the output parameter value is needed. If the 
        /// method has a regular return value (<see cref="RetArray{T1}"/>), too, this is not released automatically: return values are automatically 
        /// released after they are used for the _first time_. In order to release the array and to reclaim its storage one can call <see cref="Dispose()"/> on it.
        /// <para>Note: failing to call <see cref="Dispose"/> in such situations does not create a memory leak! But the array is only reclaimed 
        /// by the garbage collector and its memory is only freed by the finalization thread during the next GC collection. While this 
        /// is considered valid use, disposing the array manually may be profitable in situations where high performance execution and/or 
        /// low memory consumption is required. </para>
        /// <para>Calling <see cref="Dispose()"/> on local array variables which are subject of ILNumerics automatic memory management has no effect. If, for 
        /// example, an Array&lt;double&gt; A is enclosed into a local <see cref="Scope"/> calling <see cref="Dispose"/> on A within the scope body will have 
        /// no effect. The lifetime of such arrays are managed automatically by ILNumerics.</para>
        /// <para>Note further, that <see cref="Dispose"/> is <b>not</b> required in 'common' array uses. I.e. when dealing with <see cref="Array{T1}"/> 
        /// inside existing <see cref="Scope.Enter(BaseArray, ArrayStyles?)"/> scope blocks or when the return value is utilized in some way (for example by assignment to <see cref="Array{T1}"/>, 
        /// calling member functions on the return value or giving the return value to other functions as input parameter).</para>
        /// <para>Update, version 7.0: ILNumerics Accelerator automatically elides many unused return arrays in release mode (so that they are not computed to begin with).
        /// Alternatively, it disposes the return array automatically. Thus, there is even less need for manually disposing arrays, except in debug mode. 
        /// Eventually, this function will be deprecated in a future release.</para>
        /// <para>Note, that consequences of accessing an array variable or instance after <see cref="Dispose()"/> was called on it are undefined. The array ('s storage) may 
        /// immediately be reused for other arrays. It may be cleaned up, finalized, or reused in other computations. Hence, do not access the array after calling <see cref="Dispose()"/>!</para>
        /// </remarks>
        /// <example><code><![CDATA[
        /// Array<double> A = rand(10); 
        /// Array<long> I = 1; 
        /// sort(A, Indices: I).Dispose(); // <- cleaning up manually
        /// ]]></code>
        /// <para>Here, we only access the output parameter I of the sort function. The array returned 
        /// from sort(A, I) as the return value is not used, thus it is not released automatically. 
        /// In order to free its resources immediately, one can call Dispose() on the returned array. 
        /// Note, that no memory leak is produced without manually calling Dispose(). The array would 
        /// be cleaned up by the GC instead. For best performance - even in debug mode - Dispose() can be called here.</para>
        /// </example>
        public override void Dispose() {
            if (m_storage.m_scopeState == null) { // prevent multiple releases on local / input arrays 
                m_storage.Release(); 
            }
        }

        /// <summary>
        /// Gets the number of arrays currently sharing the storage with this array.
        /// </summary>
        /// <remarks>Commonly, the number of array references for a storage is 1. Note, that ILNumerics implements 
        /// a hierarchy of storage structures, each maintaining individual reference counters. Hence, a <see cref="ReferenceCount"/> 
        /// value of 1 does not guarantee or mean that no other arrays share the same memory. In fact, you should not 
        /// rely on this property nor on its value. It may change at anytime and becomes immediately stale! ILNumerics 
        /// manages references, disposal and copies automatically. Use this value for testing purposes only.</remarks>
        public override int ReferenceCount => m_storage.ReferenceCount;

        #endregion

        /// <summary>
        /// Enumerator returning elements as <typeparamref name="T1"/> in column major order. 
        /// </summary>
        /// <remarks><para>This standard iterator returns an <see cref="IEnumerator{T1}"/> and can be 
        /// used to iterate the elements of A along the rows. It is convenient for 
        /// using the <see cref="Array{T1}"/> in foreach loops.</para>
        /// <para>If you require more control over the order of iteration use one of the [Array{T}].Iterator() 
        /// extension methods instead.</para>
        /// <para>This method is thread safe.</para>
        /// </remarks>
        /// <seealso cref="ILNumerics.ExtensionMethods.Iterator(BaseArray{double}, StorageOrders?, bool)"/>
        /// <seealso href=""/>
        public IEnumerator<T1> GetEnumerator() {
            return m_storage.GetEnumerator(StorageOrders.ColumnMajor, dispose: false);
        }

        #region Typed public interface 

        /// <summary>
        /// (Expert API) Export (copy) values into System.Array.
        /// </summary>
        /// <param name="result">System.Array with a copy of all element values from this array.</param>
        /// <param name="layout">[Optional] The storage layout of the elements to write to <paramref name="result"/>.</param>
        /// <remarks>The System.Array can be predefined. If its length is sufficient, it will be used and 
        /// its leading elements will be overwritten when function returns. If <paramref name="result"/> is null or has too few elements, 
        /// it will be recreated from the _GC managed heap_.
        /// <para>Excessive use of this method can be a hint of suboptimal design! Especially if no array /no sufficiently long 
        /// array is provided in <paramref name="result"/> the returned array will be created on the managed heap. 
        /// This works around the ILNumerics memory management and may increases the pressure on the GC and can lead to 
        /// frequent garbage collections and bad performance! Make sure to reuse the array <paramref name="result"/>.</para>
        ///<para>If<paramref name="layout"/> is <c>null</c> or<see cref="StorageOrders.Other"/> the storage layout of this 
        /// array will be used to store the elements into <paramref name="result"/>.
        /// Make sure that the array <paramref name="result"/> is large enough, even if the current storage layout corresponds 
        /// to non-continously stored elements and may contain holes! Use <see cref="Size.GetElementSpan()"/> to compute 
        /// the absolute number of elements in <paramref name="result"/> necessary to hold all elements of this storage in its current
        /// storage order. Note further, that any potential holes in the element storage will not be cleared!</para>   
        /// <para>Exporting values of <see cref="Cell"/> arrays gives a clone of the individual cell elements. Special care must be 
        /// taken when working with arrays of type <see cref="BaseArray"/>. Its use is not recommended! Failing to handle BaseArray 
        /// instances properly may result in additional memory pressure, increased GC activation and unexpected <see cref="NullReferenceException"/>
        /// when accessing (automatically) disposed BaseArray objects.</para>
        /// <para>If - for some reasons - one needs to use <see cref="ExportValues(ref T1[], StorageOrders)"/> on <see cref="Cell"/>
        /// arrays nevertheless the following hints may serve as a rough guideline:</para>
        /// <list type="bullet">
        /// <item>Objects returned in <paramref name="result"/> are return type objects or null. Return type objects will dispose their
        /// content and itself after the first use. Make sure to <i>utilize</i> each object exactly once!</item>
        /// <item>Directly using an object in <paramref name="result"/> multiple times may give an error or unwanted side effects.</item>
        /// <item>In order to work with or to access a returned object multiple times it must be (immediately) converted to a local array of the 
        /// corresponding kind: <see cref="Array{T}"/>, <see cref="Logical"/>, or <see cref="Cell"/>. This, if course, requires knowledge 
        /// of the actual type of the object from within the cell.</item>
        /// <item>Failing to <i>use</i> the array at least once may leaves references to other data arrays (memory handles) open. 
        /// This may causes memory pressure on the GC and bad performance. However, in ILNumerics memory leaks (as for native languages) 
        /// cannot happen.</item>
        /// </list>
        /// </remarks>
        /// <seealso cref="IsOfType{ElementType}"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.IsTypeOf{CellT}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long)"/>
        /// <seealso cref="GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ExtensionMethods"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetArray{T}(ConcreteArray{BaseArray, Cell, InCell, OutCell, Cell, CellStorage}, long, long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{double}, long, long, long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{BaseArray}, long, long)"/>
        public void ExportValues(ref T1[] result, StorageOrders layout = StorageOrders.ColumnMajor) {
            using var _1 = ReaderLock.Create(this, out StorageT storage);

            if (result == null || result.Length < storage.Size.NumberOfElements)
                result = new T1[storage.Size.NumberOfElements];

            if (ElementInstance is System.ValueType) {

                GCHandle handle = GCHandle.Alloc(result, GCHandleType.Pinned);
                Size dummy = null;
                try {
                    Core.Functions.Builtin.CopyToOperators.CopyTo(storage.Handles[0].Pointer, storage.Size, handle.AddrOfPinnedObject(), dummy, layout, SizeOfT);
                } finally {
                    if (handle.IsAllocated) {
                        handle.Free();
                    }
                }
            } else if (storage is CellStorage) {

                var myArr = (storage.Handles[0] as ManagedHostHandle<IStorage>).HostArray;
                long p = 0;
                foreach (var ind in storage.S.Iterator(layout)) {

                    var tmp = myArr[ind]?.GetBaseArrayClone();
                    result[p++] = (T1)(object)tmp; 

                }

            } else {

                var myArr = (storage.Handles[0] as ManagedHostHandle<T1>).HostArray;
                long p = 0;
                foreach (var ind in storage.S.Iterator(layout)) {
                    result[p++] = myArr[ind];
                }

            }
        }

        /// <summary>
        /// Gets a copy of the array elements for <b>read access</b>.
        /// </summary>
        /// <param name="order">[Optional] The order used for copying the elements into the returned System array. Default: <see cref="StorageOrders.ColumnMajor"/>.</param>
        /// <returns>Managed System.Array with copies of the elements contained in this ILNumerics array.</returns>
        /// <remarks><para>This method is provided for experts only! Use this array only for reading! Changes 
        /// will not populate back to the internal array storage. Note 
        /// the default element storage format: column major. </para>
        /// <para> Keep in mind, the length of the array may exceeds the number of elements! </para>
        /// <para>The range of elements addresses by this ILNumerics array starts with index 0 of the <see cref="System.Array"/> 
        /// returned. Any <see cref="Size.BaseOffset"/> of this ILNumerics array is removed when copying the 
        /// elements to the managed array. The internal elements will be read in <paramref name="order"/>.</para>
        /// <para>The semantic of this method has changed with ILNumerics version 5. It now creates a 
        /// copy of the internal elements in column major element storage order layout. The copy uses 
        /// new memory from the regular, GC managed heap. This fact and the required memory copy 
        /// introduce a computational effort which disqualifies this method for high performance algorithms.</para>
        /// <para>The recommended way for experts to access internal elements directly, is now, to acquire a pointer 
        /// to the memory and to use the pointer in your unsafe code.</para>
        /// <para>In order to save the allocation of the managed array returned, consider using the method <see cref="ExportValues(ref T1[], StorageOrders)"/>.</para>
        /// <para>The <paramref name="order"/> parameter allows to control the order of the elements returned. Valid 
        /// values are <see cref="StorageOrders.ColumnMajor"/> and <see cref="StorageOrders.RowMajor"/>. The default value is <see cref="StorageOrders.ColumnMajor"/>.</para>
        /// <para>This method is now partly obsolete but it will remain here since it serves its purpose still: by copying 
        /// the elements one enables compatibility with other .NET APIs, at the same time it ensures that the 
        /// values returned will still be around and will not be affected by the cleanup mechanisms all internal 
        /// array storage is subject of. BUT: note the size limitation all <see cref="System.Array"/> in .NET undergo! 
        /// Attempts to acquire the elements of such arrays which exceed the maximum sizes of .NET objects will trigger an exception.</para>
        /// <para>The former versions (&lt; 5) method 'GetArrayForWrite()' has been replaced by <see cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</see>.</para>
        /// <para>This method is thread safe.</para>
        /// </remarks>
        /// <seealso cref="ExportValues(ref T1[], StorageOrders)"/>
        /// <seealso cref="Size.BaseOffset"/>
        /// <seealso cref="GetHostPointerForRead(StorageOrders?)"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <exception cref="ArgumentException">if <paramref name="order"/> is none of column major or row major.</exception>
        /// <exception cref="OutOfMemoryException">if this array is too large to fit into a managed array or if there is not enough memory currently available to allocate the return array.</exception>
        public virtual T1[] GetArrayForRead(StorageOrders order = StorageOrders.ColumnMajor) {
            if (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor) {
                throw new ArgumentException($"Only {nameof(StorageOrders.ColumnMajor)} and {nameof(StorageOrders.RowMajor)} are allowed as value for the '{nameof(order)}' argument. Found: {order}."); 
            }

            T1[] ret = null;  // allocating return array T1[] in ExportValues() saves a reader lock for its size here.
            ExportValues(ref ret, order);
            return ret;
        }

        /// <summary>
        /// [Expert API - UNSAFE!] Acquire a read pointer to the memory storing the first element of this array. 
        /// </summary>
        /// <param name="order">[Optional] Ensures that the array is in a specific storage layout before acquiring the pointer. Default: (null) don't change the storage layout.</param>
        /// <returns>Pointer to the first element of this array.</returns>
        /// <remarks>This function returns a pointer to the first element stored in this array. Any 
        /// base offset configured for the array is taken into account. The pointer points to the 
        /// memory used by this array directly - not to a copy of this memory!
        /// <para>Do not use this pointer for modifying the value of an element of this array! This 
        /// pointer is for <b>read purposes only!</b></para>
        /// <para>For empty arrays A (where A.<see cref="IsEmpty"/> is <c>true</c>) the value of the pointer returned is undefined.</para>
        /// <para>This pointer is valid only as long as this array is not modified or released! Do not 
        /// attempt to use this pointer after the array has been released, ran out of the current scope (meaning 
        /// the current function scope as well as <see cref="Scope.Enter(BaseArray, ArrayStyles?)"/>), or is modified!</para>
        /// <para>The order of elements in this array is determined by the size descriptor <see cref="Size"/>. 
        /// Use the strides, dimension lengths and the size of the <typeparamref name="T1"/> elements (<see cref="SizeOfT"/>)
        /// in order to compute the byte offset to individual elements relative to this pointer.</para>
        /// <para>The memory region addressed by the pointer returned exists on the <b>unmanaged</b> heap. 
        /// Hence, it does not need to be pinned and will not be moved by the GC. However, this memory 
        /// is subject of deterministic disposal, pooling and frequent reuse by other arrays. Do not use 
        /// the pointer returned after this array left the current function scope, was modified, reassigned or released!</para>
        /// <para>When <paramref name="order"/> is not null the storage order of the array is modified according 
        /// to <paramref name="order"/>. Valid values for <paramref name="order"/> are <see cref="StorageOrders.RowMajor"/>, 
        /// <see cref="StorageOrders.ColumnMajor"/>, and null. The <paramref name="order"/> parameter can be 
        /// convenient in order to access elements of non-continous array layouts. Instead of computing the address 
        /// of individual elements (<see cref="Size.GetSeqIndex(long, long, long, long)"/>, or <see cref="Size.GetStride(uint)"/>) 
        /// the array can be brought into a continous storage layout before acquiring the pointer. Afterwards, elements can 
        /// be accessed sequentially in memory, with unity strides. The price is the rearranging of the whole storage which 
        /// commonly requires a copy of all elements.</para>
        /// <para>This function may imply a copy operation from device specific memory onto host memory. This copy - if required - will
        /// be performed transparently to the user. The copy is done synchronously, i.e.: when the function returns the pointer
        /// returned will be valid. The user is not required to wait for asynchronous operations.</para>
        /// </remarks>
        /// <exception cref="InvalidOperationException"> if called on a return type array or if the elements are not of a <see cref="ValueType"/>.</exception>
        /// <seealso cref="GetArrayForRead"/>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        /// <seealso cref="Size.BaseOffset"/>

        public unsafe virtual IntPtr GetHostPointerForRead(StorageOrders? order = null) {

            // TODO: this should be in Mutable<T,..> &| in InArray<T> ?! 
            if (!typeof(T1).IsValueType) {
                throw new InvalidOperationException($"Cannot retrieve a pointer to an array with elements of reference type. T1 is: {typeof(T1).Name} but must be a value type.");
            }
            using var _1 = ReaderLock.Create(this, out StorageT storage);
            if (order != null && order != storage.S.StorageOrder) {
                var newStorage = storage.EnsureStorageOrder(order.GetValueOrDefault(), forceCopy: false, inplace: false);
                if (!ReferenceEquals(newStorage, storage)) {
                    _1.Update(ref storage, newStorage); 
                }
            }
            return (IntPtr)((byte*)storage.Handles[0].Pointer + storage.S.BaseOffset * SizeOfT);  
        }

        /// <summary>
        /// Create lazy, shallow clone of this array. 
        /// </summary>
        /// <returns>An array which acts as a lazy, shallow copy of this array.</returns>
        /// <remarks>The array returned is of the same size and type.
        /// <para>The copy is done in a lazy manner. This means, the new array shares memory 
        /// with this array. Only when attempting to <b>alter</b> the new array it will 
        /// be copied and use new memory then.</para></remarks>
        public virtual RetT C {
            get {
                // TODO: consider returning 'this' in case of RetT. But watch for side effects! Some test cases may depend on the returned storage being another storage. Otherwise, it would be allowed semantically! 
                using var _1 = ReaderLock.Create(this, out StorageT storage, releaseRetT: true); 
                return (storage.Clone() as BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>).m_retArray; // no retain here! Clone() gives refcount 1 or 2!
            }
        }

        /// <summary>
        /// Return transposed version of this array.
        /// </summary>
        /// <remarks>
        /// <para>For arrays having <see cref="Size.NumberOfDimensions"/> &lt; 2 <see cref="T"/> returns the same array.</para>
        /// <para>For matrices, <see cref="T"/> swaps columns with rows. </para>
        /// <para>For arrays and if <see cref="Settings.ArrayStyle"/> is <see cref="ArrayStyles.ILNumericsV4"/>, the 
        /// dimensions are shifted by one. For <see cref="Settings.ArrayStyle"/> <see cref="ArrayStyles.numpy"/> the 
        /// behavior is similar to (but more efficient than) calling <see cref="Size.SwapDimensions()"/> on a copy of this array.</para>
        /// <para>Note, for complex elements, <b>no</b> conjugate is created! Use conj(A.T) if this is intended.</para></remarks>
        public RetT T {
            get {
                return T_get(this);
            }
        }

        /// <summary>
        /// Test if this array equals another array.
        /// </summary>
        /// <param name="obj">The other array.</param>
        /// <returns>True if the element type, all element values and dimension sizes match, false otherwise.</returns>
        /// <remarks><para>The comparison returns true if both arrays are of the same type, have the same shape and if all element values match.</para>
        /// <para>Note, that the <see cref="Size.StorageOrder"/> of the arrays is not considered for the comparison. This way 
        /// it is perfectly legal for two arrays to return <c>true</c> while one array is stored in, let's say: 
        /// <see cref="StorageOrders.ColumnMajor"/> and the other array is ordered along the rows (<see cref="StorageOrders.RowMajor"/>). 
        /// However, all elements at matching sequencial positions / with matching indices in the flattened array must equal in order to return true.</para>
        /// <para>Singleton dimensions are ignored. As one consequence, <seealso cref="Equals(ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT})"/>  and 
        /// <see cref="Equals(object)"/> accept two vectors even if the orientations do not match. A row vector 
        /// with the same element values than another column vector of the same lengths are considered to be equal to each other.</para>
        /// <para><b>This function returns <c>true</c> for empty arrays of the same shape and element type. It always returns 
        /// <c>true</c> when used on an array with itself as parameter <paramref name="obj"/>.</b></para>
        /// <para>Comparing <see cref="System.ValueType"/> of type <typeparamref name="T1"/> as <paramref name="obj"/> returns <c>true</c>
        /// if this array is scalar (i.e.: it has exactly one element) and the value of the only element equals the value of 
        /// the provided scalar.</para>
        /// <para>For floating point element types <typeparamref name="T1"/> the comparison is made on the byte-level. Thus, only exact 
        /// matches are considered 'equal'. Therefore, this function is not intended to compare the results of such floating point
        /// operations which are subject of rounding errors. Consider computing the absolute distance of both floating point results 
        /// instead and compare this to the eps value of the floating point data type with appropriate precision and scaling.</para>
        /// <para><see cref="double.NaN"/> values are never equal to itself or any other value.</para>
        /// </remarks>
        /// <seealso cref="Equals(ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT})"/>
        /// <seealso cref="Size"/>
        /// <seealso cref="GetItem(long)"/>
        /// <seealso cref="Size.StorageOrder"/>
        public override bool Equals(object obj) {
            if (object.Equals(obj, null)) return false;
            if (obj is ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>) { 
                return Equals(obj as ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>);
            } else if (obj is T1) {
                // Cells?
                using var _1 = ReaderLock.Create(this, out StorageT myStor);
                return myStor.Size.NumberOfElements == 1 && obj.ToString() == myStor.GetValue(0).ToString();
            } else {
                return false; 
            }
        }
        /// <summary>
        /// Test if this array equals another array.
        /// </summary>
        /// <param name="A">The other array.</param>
        /// <returns>True if the element type, all element values and dimension sizes match, false otherwise.</returns>
        /// <remarks><para>The comparison returns true if both arrays are of the same type, have the same 
        /// shape and if all element values match.</para>
        /// <para>Note, that the <see cref="Size.StorageOrder"/> of the arrays is not considered for the comparison. This way 
        /// it is perfectly legal for two arrays to return <c>true</c> while one array is stored in, let's say: 
        /// <see cref="StorageOrders.ColumnMajor"/> and the other array is ordered along the rows (<see cref="StorageOrders.RowMajor"/>)
        /// storage layout. However, all elements at matching <i>sequencial</i> positions / with matching indices 
        /// in a <i>flattened</i> version of the array must equal in order to return <c>true</c>.</para>
        /// <para>Singleton dimensions are ignored! As one consequence, <seealso cref="Equals(ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT})"/> 
        /// and <see cref="Equals(object)"/> accept two vectors as input argument and can return <c>true</c> even 
        /// if the orientations do not match. A row vector with the same element values than another column vector 
        /// of the same lengths are considered equal to each other.</para>
        /// <para>Comparing <see cref="System.ValueType"/> of type <typeparamref name="T1"/> as <paramref name="A"/> returns <c>true</c>
        /// if this array is scalar (i.e.: it has exactly one element) and the value of the only element equals the value of 
        /// the provided scalar.</para>
        /// <para><b>This function returns <c>true</c> for empty arrays of the same shape and element type.</b></para>
        /// <para>For floating point element types <typeparamref name="T1"/> the comparison is made on the byte-level. Thus, only exact 
        /// matches are considered 'equal'. Therefore, this function is not intended to compare the results of such floating point
        /// operations which are subject of rounding errors. Consider computing the absolute distance of both floating point results 
        /// instead and compare this to the eps value of the floating point data type with appropriate precision and scaling.</para>
        /// <para><see cref="double.NaN"/> values are never equal to itself or any other value.</para>
        /// </remarks>
        /// <seealso cref="Equals(object)"/>
        /// <seealso cref="Size"/>
        /// <seealso cref="GetItem(long)"/>
        /// <seealso cref="Size.StorageOrder"/>
        public bool Equals(ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A) {
            if (object.Equals(A, null)) return false;
            using var _1 = ReaderLock.Create(this, out StorageT myStor);
            using var _2 = ReaderLock.Create(A, out StorageT aStor);

            // Caution! Don't release A! It might be a RetT!
            if (!aStor.Size.IsSameSize(myStor.Size))
                return false;
            long len = (long)myStor.Size.NumberOfElements;
            if (len == 0) return true;
            if (typeof(T1).IsValueType) {
                for (long i = 0; i < len; i++) {
                    if (!myStor.GetValue(i).Equals(aStor.GetValue(i)))
                        return false;
                }
            } else {
                // references may contain null element values! 
                for (long i = 0; i < len; i++) {
                    T1 t1 = (T1)myStor.GetValue(i);
                    if (t1 == null) {
                        if (!Equals(aStor.GetValue(i), null))
                            return false;
                    } else {
                        if (!t1.Equals(aStor.GetValue(i)))
                            return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Generate a hash code based on the current values.
        /// </summary>
        /// <returns>Hash code</returns>
        /// <remarks>The hashcode is created by taking the values currently stored in the array into account.
        /// Therefore, the function iterates over all elements in the array - which makes it somehow an expensive 
        /// operation. Take this into account if you consider using large arrays in collections like dictionaries 
        /// or hash tables, which make great use of such hash codes.
        /// <para>This method is thread safe.</para>
        /// </remarks>
        public override int GetHashCode() {
            return m_storage.GetHashCode();
        }

        /// <summary>
        /// Detaches the buffer set hosting the array elements from this array if they are shared with other storages. Afterwards, this array uses its own, dedicated memory.
        /// </summary>
        /// <remarks><para>This method is thread safe in that the buffer set _currently_ attached to this array is 
        /// detached from other storages, potentially also using the same buffer set. However, the storage might be 
        /// changed or even substituted concurrently by operations accessing the same array on other threads. Hence, 
        /// while the storage this function works on will be detached after the function returns it may no longer belongs
        /// to this array. Locking or other synchronization methods can be used to handle this case. </para>
        /// <para>Note further, that calling <see cref="Detach()"/> does not ensure exclusive access to the arrays memory. 
        /// The arrays' storage may be used / accessed from other threads concurrently and changes to it will be visible to
        /// such users immediately.</para></remarks>
        public void Detach() {

            using var _1 = ReaderLock.Create(this, out StorageT storage, releaseRetT: true);
            if (storage.Handles.ReferenceCount > 1) {
                storage.DetachBufferSetInplace();
            }
        }

        /// <summary>
        /// Returns a span providing access to this array's internal storage for reading. 
        /// </summary>
        /// <param name="order">[Optional] The order of elements returned. If order is null (default) the current value of <see cref="Settings.DefaultStorageOrder"/> is assumed, which depends on <see cref="Settings.ArrayStyle"/>.</param>
        /// <returns>A span with the exact length needed to hold all elements of this array, referencing the elements of this array, and having the same lifetime as this array.</returns>
        /// <remarks><para>Depending on the current internal storage layout this array is reordered internally as required
        /// before returning the span representing the elements in the requested storage order. </para>
        /// <para>Note, that for returning an array as span it must be ensured that the arrays elements are layed-out continously in memory. 
        /// Thus, the requested storage order is enforced in advance. This may or may not involve an internal copy of the elements of this array and a (transparent to the user) modification in its storage layout.</para>
        /// <para>The Span returned references the same memory as this array. No copy is made.</para>
        /// <para>The Span returned is only valid as long as this array is alive and before a subsequent modification is performed to this array.</para>
        /// </remarks>
        
        protected unsafe ReadOnlySpan<T1> AsReadOnlySpanInternal(StorageOrders? order = null) {
            if (order != null && (order != StorageOrders.ColumnMajor && order != StorageOrders.RowMajor)) {
                throw new ArgumentException($"Invalid value for 'order' argument. To give access to the elements of this array via a Span<T> the storage order must be continous (RowMajor or ColumnMajor). Found: '{order}'."); 
            }
            if (S.NumberOfElements > int.MaxValue) {
                throw new ArgumentOutOfRangeException($"The number of elements of this array ({S.NumberOfElements}) exceeds the maximum number of elements a Span<T> can contain ({int.MaxValue}).");
            }
            var p = GetHostPointerForRead(order ?? Settings.DefaultStorageOrder);  // performs reader lock and EnsureStorageOrder 
            var ret = new ReadOnlySpan<T1>((void*)p, (int)S.NumberOfElements);
            return ret; 
        }
        #endregion

        #region static helpers
        /// <summary>
        /// Returns transposed version of this array.
        /// </summary>
        /// <remarks>For matrices, this swaps columns with rows. For arrays, the dimensions are shifted by one.
        /// <para>Note, for complex elements, <b>no</b> conjugate is created! Use conj(A.T) if this is intended.</para>
        /// <para>The storage format of the array returned depends on the original storage format. Matrices stored in 
        /// column major format will produce row major matrices and vice versa. Note that the storage format for vectors
        /// (1 dimensional arrays) will not change since it is the same for both formats.</para>
        /// <para>This method is thread safe.</para>
        /// </remarks>
        internal unsafe static RetT T_get(ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A) {

            using var _ = (ReaderLock.Create(A, out StorageT storage));

            // Disabled -> RetT is not enough to check! Must check AsynchReferences, too! ... was: We reuse the handles and create a new storage only if required 
            var ret = (false && A is RetT) ? storage.RetArray : (storage.Clone() as StorageT).m_retArray;
            DoSize(storage, Settings.ArrayStyle, ret.Storage);
            DoStrides(storage, Settings.ArrayStyle, ret.Storage); 

            return ret;

            // Transpose the size descriptors, base meta data and dimension lengths. Strides are done later! 
            void DoSize(StorageT input, ArrayStyles style, StorageT output) {
                // if this gets ever updated to create a conjugate for complex data types 
                // -> do not forget to update quick reference ILNumerics4Matlab also!

                var inS = input.GetSizeUnsafe();
                var outS = output.GetSizeUnsafe();
                System.Diagnostics.Debug.Assert(!ReferenceEquals(inS, outS));
                int n = (int)inS.NumberOfDimensions;

                unsafe {
                    var inBSD = inS.GetBSD(false);
                    var outBSD = outS.GetBSD(true);

                    outBSD[0] = inBSD[0];
                    outBSD[1] = inBSD[1];
                    outBSD[2] = inBSD[2];

                    if (style == ArrayStyles.ILNumericsV4) {

                        // rotate dimensions (only!) 
                        // was: inS.GetShifted(1, outS.GetBSD(write: true)); // modifies ret Size, potentially inplace

                        for (int d = 0; d < n; d++) {
                            int id = (d + 1) % n;
                            outBSD[3 + d] = inBSD[3 + id];
                            // outBSD[3 + n + d] = inBSD[3 + n + id];
                        }

                    } else {

                        // numpy: transpose(). reverse dimensions 
                        // was: - but this would do strides as well. and it (temporarily) modifies dimensions, too. Hence, it cannot be used in DoValues.
                        //outS.SetAll(inS);  // does strides as well. Will be overwritten in DoValues
                        //outS.SwapDimensions();
                        // classic approach: 
                        for (int d = 0; d < n; d++) {
                            outBSD[3 + d] = inBSD[2 + n - d];
                            // outBSD[3 + n + d] = inBSD[3 + n + id];
                        }
                    }
                }
            }
            void DoStrides(StorageT input, ArrayStyles style, StorageT output) {

                var inS = input.GetSizeUnsafe();
                var outS = output.GetSizeUnsafe();
                System.Diagnostics.Debug.Assert(!ReferenceEquals(inS, outS));
                int n = (int)inS.NumberOfDimensions;

                unsafe {
                    var inBSD = inS.GetBSD(false);
                    var outBSD = outS.GetBSD(true);

                    if (style == ArrayStyles.ILNumericsV4) {

                        // rotate strides

                        for (int d = 0; d < n; d++) {
                            int id = (d + 1) % n;
                            //outBSD[3 + d] = inBSD[3 + id];  // completed in DoSize
                            outBSD[3 + n + d] = inBSD[3 + n + id];
                        }

                    } else {

                        // numpy: transpose(). reverse dimensions 
                        for (int d = 0; d < n; d++) {
                            // outBSD[3 + d] = inBSD[2 + n - d];
                            outBSD[3 + n + d] = inBSD[2 + 2 * n - d];
                        }
                    }
                }
#if DEBUG
                Size.CheckSizeBroadcastableStrides(outS); 
#endif
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        #endregion


    }
}
