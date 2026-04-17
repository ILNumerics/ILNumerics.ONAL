using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILNumerics.Core.Native {

    /// <summary>
    /// MKL configuration parameters (constant definitions)
    /// </summary>
    internal unsafe sealed class MKLParameter {
        /* Domain for forward transform. No default value */
        public static readonly int FORWARD_DOMAIN = 0;

        /* Dimensionality; or rank. No default value */
        public static readonly int DIMENSION = 1;

        /* Length(s) of transform. No default value */
        public static readonly int LENGTHS = 2;

        /* Floating point precision. No default value */
        public static readonly int PRECISION = 3;

        /* Scale factor for forward transform [1.0] */
        public static readonly int FORWARD_SCALE = 4;

        /* Scale factor for backward transform [1.0] */
        public static readonly int BACKWARD_SCALE = 5;

        /* Exponent sign for forward transform [NEGATIVE]  */
        /* FORWARD_SIGN = 6; ## NOT IMPLEMENTED */

        /* Number of data sets to be transformed [1] */
        public static readonly int NUMBER_OF_TRANSFORMS = 7;

        /* Storage of finite complex-valued sequences in complex domain
           [COMPLEX_COMPLEX] */
        public static readonly int COMPLEX_STORAGE = 8;

        /* Storage of finite real-valued sequences in real domain
           [REAL_REAL] */
        public static readonly int REAL_STORAGE = 9;

        /* Storage of finite complex-valued sequences in conjugate-even
           domain [COMPLEX_REAL] */
        public static readonly int CONJUGATE_EVEN_STORAGE = 10;

        /* Placement of result [INPLACE] */
        public static readonly int PLACEMENT = 11;

        /* Generalized strides for input data layout [tight; row-major for C] */
        public static readonly int INPUT_STRIDES = 12;

        /* Generalized strides for output data layout [tight; row-major
           for C] */
        public static readonly int OUTPUT_STRIDES = 13;

        /* Distance between first input elements for multiple transforms
           [0] */
        public static readonly int INPUT_DISTANCE = 14;

        /* Distance between first output elements for multiple transforms
           [0] */
        public static readonly int OUTPUT_DISTANCE = 15;

        /* Ordering of the result [ORDERED] */
        public static readonly int ORDERING = 18;

        /* Possible transposition of result [NONE] */
        public static readonly int TRANSPOSE = 19;

        /* User-settable descriptor name [""] */
        public static readonly int DESCRIPTOR_NAME = 20; /* DEPRECATED */

        /* Packing format for COMPLEX_REAL storage of finite
           conjugate-even sequences [CCS_FORMAT] */
        public static readonly int PACKED_FORMAT = 21;

        /* Commit status of the descriptor - R/O parameter */
        public static readonly int COMMIT_STATUS = 22;

        /* Version string for this DFTI implementation - R/O parameter */
        public static readonly int VERSION = 23;

        /* Number of user threads that share the descriptor [1] */
        public static readonly int NUMBER_OF_USER_THREADS = 26;
    }
    /// <summary>
    /// MKL configuration values (constant definitions) 
    /// </summary>
    internal unsafe sealed class MKLValues {
        public static readonly int MKL_ALL = 0;
        public static readonly int MKL_BLAS = 1;
        public static readonly int MKL_FFT = 2;
        public static readonly int MKL_VML = 3;
        /* Values of the descriptor configuration parameters */
        /* COMMIT_STATUS */
        public static readonly int COMMITTED = 30;
        public static readonly int UNCOMMITTED = 31;

        /* FORWARD_DOMAIN */
        public static readonly int COMPLEX = 32;
        public static readonly int REAL = 33;
        /* CONJUGATE_EVEN = 34;   ## NOT IMPLEMENTED */

        /* PRECISION */
        public static readonly int SINGLE = 35;
        public static readonly int DOUBLE = 36;

        /* COMPLEX_STORAGE and CONJUGATE_EVEN_STORAGE */
        public static readonly int COMPLEX_COMPLEX = 39;
        public static readonly int COMPLEX_REAL = 40;

        /* REAL_STORAGE */
        public static readonly int REAL_COMPLEX = 41;
        public static readonly int REAL_REAL = 42;

        /* PLACEMENT */
        public static readonly int INPLACE = 43;          /* Result overwrites input */
        public static readonly int NOT_INPLACE = 44;      /* Have another place for result */

        /* ORDERING */
        public static readonly int ORDERED = 48;
        public static readonly int BACKWARD_SCRAMBLED = 49;

        /* Allow/avoid certain usages */
        public static readonly int ALLOW = 51;            /* Allow transposition or workspace */
        public static readonly int NONE = 53;

        /* PACKED_FORMAT (for storing congugate-even finite sequence
           in real array) */
        public static readonly int CCS_FORMAT = 54;       /* Complex conjugate-symmetric */
        public static readonly int PACK_FORMAT = 55;      /* Pack format for real DFT */
        public static readonly int PERM_FORMAT = 56;      /* Perm format for real DFT */
        public static readonly int CCE_FORMAT = 57;       /* Complex conjugate-even */

        /* Error classes */
        public static readonly int NO_ERROR = 0;
        public static readonly int MEMORY_ERROR = 1;
        public static readonly int INVALID_CONFIGURATION = 2;
        public static readonly int INCONSISTENT_CONFIGURATION = 3;
        public static readonly int MULTITHREADED_ERROR = 4;
        public static readonly int BAD_DESCRIPTOR = 5;
        public static readonly int UNIMPLEMENTED = 6;
        public static readonly int MKL_INTERNAL_ERROR = 7;
        public static readonly int NUMBER_OF_THREADS_ERROR = 8;
        public static readonly int ONED_LENGTH_EXCEEDS_INT32 = 9;

        public static readonly int MAX_MESSAGE_LENGTH = 40; /* Maximum length of error string */
        public static readonly int MAX_NAME_LENGTH = 10;    /* Maximum length of descriptor name */
        public static readonly int VERSION_LENGTH = 198;    /* Maximum length of MKL version string */

        /* This is used to determine the storage layout of matrix arguments for cblas_?gemm. */
        public static int CblasRowMajor = 101;
        public static int CblasColMajor = 102;

    }
}
