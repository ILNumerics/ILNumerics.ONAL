using System;

#pragma warning disable 1591

namespace ILNumerics {

    /// <summary>
    /// Supported known storage orders for array elements.
    /// </summary>
    public enum StorageOrders : ulong {
        /// <summary>
        /// Column major, or first-to-last dimension ordering. (FORTRAN, Matlab, ILNumerics v4, Julia). 
        /// </summary>
        ColumnMajor = 1,
        /// <summary>
        /// Row major, or last-to-first dimension ordering. (C-style, Java, .NET multi-dim).
        /// </summary>
        RowMajor = 2,
        /// <summary>
        /// Other storage formats and any non-contiguous storages. 
        /// </summary>
        Other = 4
    }


    [Flags]
    public enum KernelFlags : ulong {
        Default = KernelFlags.SaturateIntegerBinaryOpsAuto,
        CLRSIMDDisabled = 1,
        SaturateIntegerBinaryOpsAuto = 2,
        SaturateIntegerBinaryOpsEnabled = 4,
        SaturateIntegerBinaryOpsDisabled = 8,
    }

    public enum PoolSizePolicy {
        /// <summary>
        /// Standard pool allocation handling: allocate. Default. 
        /// </summary>
        Regular = 0,
        /// <summary>
        /// Segments allocation policy. 
        /// </summary>
        Segment = 1
    }

    /// <summary>
    /// All device types supported by OpenCL. These enum values match the official constants values according to the OpenCL spec 2.1. 
    /// </summary>
    [Flags]
    public enum DeviceTypes : ulong {
        /// <summary>
        /// An OpenCL device that is the host processor. The host processor runs the OpenCL implementations and is a single or multi-core CPU. This value is part of the OpenCL specification.
        /// </summary>
        CPU = 1 << 1,
        /// <summary>\r\n HERE
        /// An OpenCL device that is a GPU. By this we mean that the device can also be used to accelerate a 3D API such as OpenGL or DirectX. This value is part of the OpenCL specification.
        /// </summary>
        GPU = 1 << 2,
        /// <summary>
        /// Dedicated OpenCL accelerators (for example the IBM CELL Blade). These devices communicate with the host processor using a peripheral interconnect such as PCIe. This value is part of the OpenCL specification.
        /// </summary>
        Accelerator = 1 << 3,
        /// <summary>
        /// Dedicated accelerators that do not support programs written in OpenCL C. This value is part of the OpenCL specification.
        /// </summary>
        Custom = 1 << 4,
        /// <summary>
        /// The default OpenCL device in the system. The default device cannot be a <see cref="DeviceTypes.Custom"/> device.  This value is part of the OpenCL specification.
        /// </summary>
        Default = 1 << 0,
        /// <summary>
        /// All OpenCL devices available in the system except <see cref="DeviceTypes.Custom"/> devices. This value is part of the OpenCL specification.
        /// </summary>
        ALL = unchecked((ulong)-1),
        /// <summary>
        /// A (non - OpenCL) device representing the host platform. ILNumerics uses the CLR (.NET Framework) as host in version 7. This value was added to the official OpenCL enum values by ILNumerics.
        /// </summary>
        Host = 1 << 16
    }

    /// <summary>
    /// [numpy API] Options for handling out-of-range indices in 'ILNumerics.numpy.put(A, B, C, mode)'.
    /// </summary>
    public enum PutModes {
        /// <summary>
        /// [Default] throw an <see cref="IndexOutOfRangeException"/>.
        /// </summary>
        Raise, 
        /// <summary>
        /// Indices are computed by applying the modulus of the allowed index range to the requested index. 
        /// </summary>
        Wrap, 
        /// <summary>
        /// Too large indices are clipped into the allowed range.
        /// </summary>
        Clip
    }

    /// <summary>
    /// Options of array semantics. This enum is used to switch the behavior of <see cref="Array{T}"/> and Co. between ILNumerics version 4 (Matlab, Julia, etc.) and numpy mode.
    /// </summary>
    public enum ArrayStyles : int {
        /// <summary>
        /// Default indexing and array styles. Handles subarray / indexing expressions in the tradional way, similar to Matlab(R), octave and ILNumerics prior version 5. All arrays exist as matrices (i.e.: have 2 or more dimensions, even vectors and scalars).
        /// </summary>
        /// <remarks><para>When this setting is selected, unspecified trailing dimensions are (virtually) substituted with '0', addressing the first element in the dimension (Matlab(R) style subarrays).</para>
        /// <para>All arrays are created as matrices having at least 2 dimensions.</para>
        /// <para>Addressing the last dimension with ":" or <see cref="Globals.full"/> in subarray expressions leads to a reshaping of the array and merging unspecified subsequent dimensions into the last dimension (linear index expansion).</para>
        /// <para>Arrays can be expanded, parts of arrays can be removed in subarray/ indexing expressions.</para>
        /// <para>The default storage order for new arrays is <see cref="StorageOrders.ColumnMajor"/>.</para>
        /// </remarks>
        ILNumericsV4 = 1,
        /// <summary>
        /// Array handling and subarray / indexing expressions are handled similar to numpy.
        /// </summary>
        /// <remarks><para>When this setting is selected, unspecified trailing dimensions are (virtually) substituted with ':', addressing all 
        /// of the trailing dimension ('ellipsis', numpy indexing).</para>
        /// <para>Arrays can have less than 2 dimensions. 1-dim vectors and 0-dim scalars are supported.</para>
        /// <para>Indexing parameters contribute to the dimensionality of the result. For scalars as index parameters this leads to a reduction of dimensions of the result.</para>
        /// <para>All indices provided must be in the range of valid indices for each dimension. No linear index expansion is supported!</para>
        /// <para>Arrays cannot be expanded, no parts of arrays can be removed in subarray/ indexing expressions.</para>
        /// <para>The default storage order for new arrays is <see cref="StorageOrders.RowMajor"/>.</para>
        /// </remarks>
        numpy = 2
    }

    /// <summary>
    /// Individual types of memory available to store arrays and parts thereof. 
    /// </summary>
    public enum MemoryTypes : int {

        /// <summary>
        /// The process' native virtual memory, managed by the OS.
        /// </summary>
        Process,
        /// <summary>
        /// Shared, virtual (native) memory.
        /// </summary>
        Shared, 
        /// <summary>
        /// Memory on the managed heap, subject to garbage collection.
        /// </summary>
        Managed,
        /// <summary>
        /// Device memory storage under control of a device specific interface, such as OpenCL. Ex: OpenCL device memory as GPU memory.  
        /// </summary>
        /// <remarks>Note, that <see cref="Process"/>, <see cref="Shared"/> and <see cref="Device"/> may refer to the 
        /// same memory addresses. The distinction is made by the interface used to access the memory.</remarks>
        Device
    }

    /// <summary>
    /// Specifies the type of eigen problem, as defined by Lapack. 
    /// </summary>
    /// <remarks>The enumeration lists possible problem definitions for generalized eigenproblems:
    /// <list type="bullet">
    /// <item>Ax_eq_lambBx: A*V = r*B*V</item>
    /// <item>ABx_eq_lambx: A*B*V = r*V</item>
    /// <item>BAx_eq_lambx: B*A*V = r*V</item>
    /// </list></remarks>
    public enum GenEigenType : int {
        /// <summary>
        /// A*V = r*B*V
        /// </summary>
        Ax_eq_lambBx = 1,
        /// <summary>
        /// A*B*V = r*V
        /// </summary>
        ABx_eq_lambx = 2,
        /// <summary>
        /// B*A*V = r*V
        /// </summary>
        BAx_eq_lambx = 3
    }

    /// <summary>
    /// Numeric, value type names used as data array elements by ILNumerics. 
    /// </summary>
    public enum NumericType : int {
        /// <summary>
        /// Non-numeric element type.
        /// </summary>
        None = 0,
        /// <summary>
        /// Double precision (64 bit), floating point element type.
        /// </summary>
        Double = 1,
        /// <summary>
        /// Single precision (32 bit), floating point element type.
        /// </summary>
        Single = 2,
        /// <summary>
        /// Double precision complex element type.
        /// </summary>
        Complex = 3,
        /// <summary>
        /// Single precision complex element type.
        /// </summary>
        FComplex = 4,
        /// <summary>
        /// Unsigned, 8 bit integer type.
        /// </summary>
        Byte = 5,
        /// <summary>
        /// Signed, 8 bit integer type.
        /// </summary>
        SByte = 6,
        /// <summary>
        /// Signed, 16 bit integer type.
        /// </summary>
        Int16 = 7,
        /// <summary>
        /// unsigned, 16 bit integer type.
        /// </summary>
        UInt16 = 8,
        /// <summary>
        /// Signed, 32 bit integer type.
        /// </summary>
        Int32 = 9,
        /// <summary>
        /// Unsigned, 32 bit integer type.
        /// </summary>
        UInt32 = 10,
        /// <summary>
        /// Signed, 64 bit integer type.
        /// </summary>
        Int64 = 11,
        /// <summary>
        /// Unsigned, 64 bit integer type.
        /// </summary>
        UInt64 = 12,
        /// <summary>
        /// Boolean type. Not neccessarily numeric. Often implemented as 1 byte integer type.
        /// </summary>
        Boolean = 20,
    }

    /// <summary>
    /// Defines the way Arrays are serialized to stream.
    /// </summary>
    /// <seealso cref="BaseArray.ToStream(System.IO.Stream, string,ArrayStreamSerializationFlags)"/>
    public enum ArrayStreamSerializationFlags {
        /// <summary>
        /// Print values 'vectorized': one value after each other. The true dimension configuration 
        /// of the array will be lost in the result. 
        /// </summary>
        Serial,
        /// <summary>
        /// Print values 'matrixwise'. The real dimensions configuration for the array are kept 
        /// in the result. The array will be printed by pages, consisting out of the 1st and 2nd 
        /// leading dimnsion. A dimension tag will prefix each page. The format can be used as 
        /// fancier output version for human reading as well as human readable serialization 
        /// format. Array's are capable of constructing from streams containing this type of 
        /// output. 
        /// </summary>
        Formatted, 
        /// <summary>
        /// Export whole array instance to matlab 5.0 format
        /// </summary>
        Matlab
    }
    /// <summary>
    /// Possible properties for matrices 
    /// </summary>
    /// <remarks><para>These properties may be returned by function overloads receiving a MatrixProperties 
    /// parameter by reference. </para>
    /// <para><![CDATA[This enum is a bitflag'ed enum! You may query for any combination via the bitwise operators | and &. ]]></para></remarks>
    public enum MatrixProperties : int {
        /// <summary>
        /// Hermitian matrix 
        /// </summary>
        Hermitian = 1,
        /// <summary>
        /// Positive definite
        /// </summary>
        PositivDefinite = 2 ,
        /// <summary>
        /// Upper triangular matrix
        /// </summary>
        UpperTriangular = 4,
        /// <summary>
        /// Lower triangular matrix
        /// </summary>
        LowerTriangular = 8,
        /// <summary>
        /// Square matrix
        /// </summary>
        Square = 16,
        /// <summary>
        /// Diagonal matrix
        /// </summary>
        Diagonal = 32,
        /// <summary>
        /// The matrix is singular 
        /// </summary>
        Singular = 64,
        /// <summary>
        /// Hessenberg matrix
        /// </summary>
        Hessenberg = 128,
        /// <summary>
        /// Householder matrix
        /// </summary>
        Householder = 256,
        /// <summary>
        /// Unitary matrix
        /// </summary>
        Unitary = 512,
        /// <summary>
        /// Orthogonal matrix
        /// </summary>
        Orthogonal = 1024,
        /// <summary>
        /// Orthonormal matrix
        /// </summary>
        Orthonormal = 2048,
        /// <summary>
        /// The matrix has deficient rank
        /// </summary>
        RankDeficient = 4096,
        /// <summary>
        /// The matrix has no special properties
        /// </summary>
        None = 8192,
        /// <summary>
        /// No specific properties known (default)
        /// </summary>
        Unknown = 0
    }
}
