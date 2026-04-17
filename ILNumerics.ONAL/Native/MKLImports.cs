using ILNumerics.Core.MemoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Native {

    /// <summary>
    /// import functions (pinvoke)
    /// </summary>
    internal static unsafe class MKLImports {


        internal const string MKL_FILENAME = "mkl_custom";

        #region pinvoke definitions 

        [DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static extern int mkl_domain_set_num_threads(ref int num, ref int mask);
        [DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static extern int mkl_domain_get_max_threads(ref int mask);
        [DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static extern int mkl_get_max_threads();
        [DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static extern int omp_get_num_threads();

        [DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static extern void mkl_set_dynamic(ref int boolean_value);

        [DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static extern int DftiCreateDescriptor(ref IntPtr pDescriptor, int precision, int domain, int dimCount, int dim1);
        [DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static extern int DftiCreateDescriptor(ref IntPtr pDescriptor, int precision, int domain, int dimCount, int* dims);
        [DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static extern int DftiFreeDescriptor(ref IntPtr pDescriptor);
        [DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static extern int DftiCommitDescriptor(IntPtr pDescriptor);
        [DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static extern int DftiErrorClass(int status, int ERROR_CLASS);
        [DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static extern int DftiSetValue(IntPtr pDescriptor, int parameter, int value);
        [DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static extern int DftiSetValue(IntPtr pDescriptor, int parameter, double value);
        [DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static extern int DftiSetValue(IntPtr pDescriptor, int parameter, int* values);
        [DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static extern int DftiGetValue(IntPtr pDescriptor, int parameter, ref int value);
        [DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static extern int DftiGetValue(IntPtr pDescriptor, int parameter, ref double value);
        [DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static extern int DftiComputeForward(IntPtr pDescriptor, IntPtr pArray);
        [DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static extern int DftiComputeForward(IntPtr pDescriptor, IntPtr pInArray, IntPtr pOutArray);

        /** DFTI native DftiComputeForward declaration */
        [DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int DftiComputeForward(IntPtr desc, [In] double[] x_in, [Out] double[] x_out);

        [DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static extern int DftiComputeBackward(IntPtr pDescriptor, IntPtr pArray);
        [DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static extern int DftiComputeBackward(IntPtr pDescriptor, IntPtr pInArray, IntPtr pOutArray);

        /** DFTI native DftiComputeBackward declaration */
        [DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, SetLastError = false)]
        public static extern int DftiComputeBackward(IntPtr desc, [In] double[] x_in, [Out] double[] x_out);

        [DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static extern IntPtr DftiErrorMessage(int errID);

        #endregion

        #region dll imports for lapack functions
        
        [DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static extern void mkl_set_num_threads(ref int num);
        [DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static extern void omp_set_num_threads(ref int num);
        [DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static extern void mkl_free_buffers();
        //[DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical] 
        //public static extern void mkl_get_version_string(IntPtr buffer);
        //[DllImport(MKL_FILENAME, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical] 
        //public static extern void mkl_get_version(ref MKLVersion version);


        [DllImport(MKL_FILENAME, EntryPoint = "ilaenv", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern int mkl_ilaenv(ref int ispec, ref string name, ref string opts, ref int n1, ref int n2, ref int n3, ref int n4);

        ///////////////////////////   DOUBLE LAPACK /////////////////////////////////
        [DllImport(MKL_FILENAME, EntryPoint = "DGEMM", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_dgemm(ref char TransA, ref char TransB, ref int M, ref int N, ref int K, ref double alpha, IntPtr A, ref int lda, IntPtr B, ref int ldb, ref double beta, double* C, ref int ldc);
        [DllImport(MKL_FILENAME, EntryPoint = "SGEMM", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_sgemm(ref char TransA, ref char TransB, ref int M, ref int N, ref int K, ref float alpha, IntPtr A, ref int lda, IntPtr B, ref int ldb, ref float beta, float* C, ref int ldc);
        [DllImport(MKL_FILENAME, EntryPoint = "CGEMM", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_cgemm(ref char TransA, ref char TransB, ref int M, ref int N, ref int K, ref fcomplex alpha, IntPtr A, ref int lda, IntPtr B, ref int ldb, ref fcomplex beta, [In, Out] fcomplex* C, ref int ldc);
        [DllImport(MKL_FILENAME, EntryPoint = "ZGEMM", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_zgemm(ref char TransA, ref char TransB, ref int M, ref int N, ref int K, ref complex alpha, IntPtr A, ref int lda, IntPtr B, ref int ldb, ref complex beta, [In, Out] complex* C, ref int ldc);

        // C-interface. Supports row major storage also.
        [DllImport(MKL_FILENAME, EntryPoint = "cblas_dgemm", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_cblas_dgemm(ref int CBLAS_Layout, ref char TransA, ref char TransB, ref int M, ref int N, ref int K, ref double alpha, IntPtr A, ref int lda, IntPtr B, ref int ldb, ref double beta, double* C, ref int ldc);
        [DllImport(MKL_FILENAME, EntryPoint = "cblas_sgemm", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_cblas_sgemm(ref int CBLAS_Layout, ref char TransA, ref char TransB, ref int M, ref int N, ref int K, ref float alpha, IntPtr A, ref int lda, IntPtr B, ref int ldb, ref float beta, float* C, ref int ldc);
        [DllImport(MKL_FILENAME, EntryPoint = "cblas_cgemm", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_cblas_cgemm(ref int CBLAS_Layout, ref char TransA, ref char TransB, ref int M, ref int N, ref int K, ref fcomplex alpha, IntPtr A, ref int lda, IntPtr B, ref int ldb, ref fcomplex beta, [In, Out] fcomplex* C, ref int ldc);
        [DllImport(MKL_FILENAME, EntryPoint = "cblas_zgemm", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_cblas_zgemm(ref int CBLAS_Layout, ref char TransA, ref char TransB, ref int M, ref int N, ref int K, ref complex alpha, IntPtr A, ref int lda, IntPtr B, ref int ldb, ref complex beta, [In, Out] complex* C, ref int ldc);

        [DllImport(MKL_FILENAME, EntryPoint = "DGESDD", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_dgesdd(ref char jobz, ref int m, ref int n, double* a, ref int lda, double* s, double* u, ref int ldu, double* vt, ref int ldvt, double* work, ref int lwork, int* iwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "SGESDD", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_sgesdd(ref char jobz, ref int m, ref int n, float* a, ref int lda, float* s, float* u, ref int ldu, float* vt, ref int ldvt, float* work, ref int lwork, int* iwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "CGESDD", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_cgesdd(ref char jobz, ref int m, ref int n, [In, Out] fcomplex* a, ref int lda, float* s, [In, Out] fcomplex* u, ref int ldu, [In, Out]  fcomplex* vt, ref int ldvt, [In, Out] fcomplex* work, ref int lwork, [In, Out] float* rwork, int* iwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "ZGESDD", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_zgesdd(ref char jobz, ref int m, ref int n, [In, Out] complex* a, ref int lda, double* s, [In, Out] complex* u, ref int ldu, [In, Out] complex* vt, ref int ldvt, [In, Out] complex* work, ref int lwork, [In, Out] double* rwork, int* iwork, ref int info);

        [DllImport(MKL_FILENAME, EntryPoint = "DGESVD", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_dgesvd(ref char jobu, ref char jobvt, ref int m, ref int n, double* a, ref int lda, double* s, double* u, ref int ldu, double* vt, ref int ldvt, double* work, ref int lwork, int* iwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "SGESVD", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_sgesvd(ref char jobu, ref char jobvt, ref int m, ref int n, float* a, ref int lda, float* s, float* u, ref int ldu, float* vt, ref int ldvt, float* work, ref int lwork, int* iwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "CGESVD", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_cgesvd(ref char jobu, ref char jobvt, ref int m, ref int n, [In, Out] fcomplex* a, ref int lda, float* s, [In, Out] fcomplex* u, ref int ldu, [In, Out] fcomplex* vt, ref int ldvt, [In, Out]  fcomplex* work, ref int lwork, int* iwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "ZGESVD", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_zgesvd(ref char jobu, ref char jobvt, ref int m, ref int n, [In, Out] complex* a, ref int lda, double* s, [In, Out] complex* u, ref int ldu, [In, Out] complex* vt, ref int ldvt, [In, Out]  complex* work, ref int lwork, int* iwork, ref int info);

        [DllImport(MKL_FILENAME, EntryPoint = "DPOTRF", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_dpotrf(ref char uplo, ref int n, double* A, ref int lda, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "SPOTRF", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_spotrf(ref char uplo, ref int n, float* A, ref int lda, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "CPOTRF", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_cpotrf(ref char uplo, ref int n, [In, Out] fcomplex* A, ref int lda, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "ZPOTRF", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_zpotrf(ref char uplo, ref int n, [In, Out] complex* A, ref int lda, ref int info);

        [DllImport(MKL_FILENAME, EntryPoint = "DPOTRI", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_dpotri(ref char uplo, ref int n, double* A, ref int lda, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "SPOTRI", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_spotri(ref char uplo, ref int n, float* A, ref int lda, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "CPOTRI", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_cpotri(ref char uplo, ref int n, [In, Out] fcomplex* A, ref int lda, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "ZPOTRI", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_zpotri(ref char uplo, ref int n, [In, Out] complex* A, ref int lda, ref int info);

        [DllImport(MKL_FILENAME, EntryPoint = "DPOTRS", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_dpotrs(ref char uplo, ref int n, ref int NRHS, double* A, ref int lda, double* B, ref int ldb, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "SPOTRS", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_spotrs(ref char uplo, ref int n, ref int NRHS, float* A, ref int lda, float* B, ref int ldb, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "CPOTRS", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_cpotrs(ref char uplo, ref int n, ref int NRHS, [In, Out] fcomplex* A, ref int lda, [In, Out] fcomplex* B, ref int ldb, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "ZPOTRS", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_zpotrs(ref char uplo, ref int n, ref int NRHS, [In, Out] complex* A, ref int lda, [In, Out] complex* B, ref int ldb, ref int info);

        [DllImport(MKL_FILENAME, EntryPoint = "DGETRF", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_dgetrf(ref int M, ref int N, double* A, ref int LDA, int* IPIV, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "SGETRF", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_sgetrf(ref int M, ref int N, float* A, ref int LDA, int* IPIV, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "CGETRF", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_cgetrf(ref int M, ref int N, [In, Out] fcomplex* A, ref int LDA, int* IPIV, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "ZGETRF", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_zgetrf(ref int M, ref int N, [In, Out] complex* A, ref int LDA, int* IPIV, ref int info);

        [DllImport(MKL_FILENAME, EntryPoint = "DGETRI", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_dgetri(ref int N, double* A, ref int LDA, int* IPIV, double* work, ref int lwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "SGETRI", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_sgetri(ref int N, float* A, ref int LDA, int* IPIV, float* work, ref int lwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "CGETRI", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_cgetri(ref int N, [In, Out] fcomplex* A, ref int LDA, [In, Out] int* IPIV, [In, Out] fcomplex* work, ref int lwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "ZGETRI", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_zgetri(ref int N, [In, Out] complex* A, ref int LDA, int* IPIV, [In, Out] complex* work, ref int lwork, ref int info);

        [DllImport(MKL_FILENAME, EntryPoint = "DGEQRF", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_dgeqrf(ref int M, ref int N, double* A, ref int lda, double* tau, double* work, ref int lwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "SGEQRF", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_sgeqrf(ref int M, ref int N, float* A, ref int lda, float* tau, float* work, ref int lwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "CGEQRF", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_cgeqrf(ref int M, ref int N, [In, Out] fcomplex* A, ref int lda, [In, Out] fcomplex* tau, [In, Out] fcomplex* work, ref int lwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "ZGEQRF", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_zgeqrf(ref int M, ref int N, [In, Out] complex* A, ref int lda, [In, Out] complex* tau, [In, Out] complex* work, ref int lwork, ref int info);

        [DllImport(MKL_FILENAME, EntryPoint = "DGEQP3", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_dgeqp3(ref int M, ref int N, double* A, ref int LDA, int* JPVT, double* tau, double* work, ref int lwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "SGEQP3", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_sgeqp3(ref int M, ref int N, float* A, ref int LDA, int* JPVT, float* tau, float* work, ref int lwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "CGEQP3", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_cgeqp3(ref int M, ref int N, [In, Out] fcomplex* A, ref int LDA, [In, Out] int* JPVT, [In, Out] fcomplex* tau, [In, Out] fcomplex* work, ref int lwork, [In, Out] float* rwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "ZGEQP3", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_zgeqp3(ref int M, ref int N, [In, Out] complex* A, ref int LDA, [In, Out] int* JPVT, [In, Out] complex* tau, [In, Out] complex* work, ref int lwork, [In, Out] double* rwork, ref int info);

        [DllImport(MKL_FILENAME, EntryPoint = "DORMQR", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_dormqr(ref char side, ref char trans, ref int m, ref int n, ref int k, double* A, int lda, double* tau, double* C, ref int ldc, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "SORMQR", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_sormqr(ref char side, ref char trans, ref int m, ref int n, ref int k, float* A, ref int lda, float* tau, float* C, ref int ldc, ref int info);

        [DllImport(MKL_FILENAME, EntryPoint = "DORGQR", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_dorgqr(ref int m, ref int n, ref int k, double* A, ref int lda, double* tau, double* work, ref int lwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "SORGQR", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_sorgqr(ref int m, ref int n, ref int k, float* A, ref int lda, float* tau, float* work, ref int lwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "CUNGQR", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_cungqr(ref int m, ref int n, ref int k, [In, Out] fcomplex* A, ref int lda, [In, Out] fcomplex* tau, [In, Out] fcomplex* work, ref int lwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "ZUNGQR", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_zungqr(ref int m, ref int n, ref int k, [In, Out] complex* A, ref int lda, [In, Out] complex* tau, [In, Out] complex* work, ref int lwork, ref int info);

        [DllImport(MKL_FILENAME, EntryPoint = "DTRTRS", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_dtrtrs(ref char uplo, ref char transA, ref char diag, ref int N, ref int nrhs, double* A, ref int LDA, double* B, ref int LDB, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "STRTRS", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_strtrs(ref char uplo, ref char transA, ref char diag, ref int N, ref int nrhs, float* A, ref int LDA, float* B, ref int LDB, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "CTRTRS", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_ctrtrs(ref char uplo, ref char transA, ref char diag, ref int N, ref int nrhs, fcomplex* A, ref int LDA, fcomplex* B, ref int LDB, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "ZTRTRS", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_ztrtrs(ref char uplo, ref char transA, ref char diag, ref int N, ref int nrhs, complex* A, ref int LDA, complex* B, ref int LDB, ref int info);

        [DllImport(MKL_FILENAME, EntryPoint = "DGETRS", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_dgetrs(ref char trans, ref int N, ref int NRHS, double* A, ref int LDA, int* IPIV, double* B, ref int LDB, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "SGETRS", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_sgetrs(ref char trans, ref int N, ref int NRHS, float* A, ref int LDA, int* IPIV, float* B, ref int LDB, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "CGETRS", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_cgetrs(ref char trans, ref int N, ref int NRHS, [In, Out] fcomplex* A, ref int LDA, int* IPIV, [In, Out] fcomplex* B, ref int LDB, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "ZGETRS", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_zgetrs(ref char trans, ref int N, ref int NRHS, [In, Out] complex* A, ref int LDA, int* IPIV, [In, Out] complex* B, ref int LDB, ref int info);

        [DllImport(MKL_FILENAME, EntryPoint = "DGELSD", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_dgelsd(ref int m, ref int n, ref int nrhs, double* A, ref int lda, double* B, ref int ldb, double* S, ref double RCond, ref int rank, double* work, ref int lwork, int* iwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "SGELSD", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_sgelsd(ref int m, ref int n, ref int nrhs, float* A, ref int lda, float* B, ref int ldb, float* S, ref float RCond, ref int rank, float* work, ref int lwork, int* iwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "CGELSD", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_cgelsd(ref int m, ref int n, ref int nrhs, [In, Out] fcomplex* A, ref int lda, [In, Out] fcomplex* B, ref int ldb, float* S, ref float RCond, ref int rank, [In, Out] fcomplex* work, ref int lwork, float* rwork, int* iwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "ZGELSD", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_zgelsd(ref int m, ref int n, ref int nrhs, [In, Out] complex* A, ref int lda, [In, Out] complex* B, ref int ldb, double* S, ref double RCond, ref int rank, [In, Out]  complex* work, ref int lwork, double* rwork, int* iwork, ref int info);

        [DllImport(MKL_FILENAME, EntryPoint = "DGELSY", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_dgelsy(ref int m, ref int n, ref int nrhs, double* A, ref int lda, double* B, ref int ldb, int* JPVT0, ref double RCond, ref int rank, double* work, ref int lwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "SGELSY", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_sgelsy(ref int m, ref int n, ref int nrhs, float* A, ref int lda, float* B, ref int ldb, int* JPVT0, ref float RCond, ref int rank, float* work, ref int lwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "CGELSY", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_cgelsy(ref int m, ref int n, ref int nrhs, [In, Out] fcomplex* A, ref int lda, [In, Out] fcomplex* B, ref int ldb, int* JPVT0, ref float RCond, ref int rank, [In, Out] fcomplex* work, ref int lwork, float* rwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "ZGELSY", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_zgelsy(ref int m, ref int n, ref int nrhs, [In, Out] complex* A, ref int lda, [In, Out] complex* B, ref int ldb, int* JPVT0, ref double RCond, ref int rank, [In, Out]  complex* work, ref int lwork, double* rwork, ref int info);

        [DllImport(MKL_FILENAME, EntryPoint = "DGEEVX", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_dgeevx(ref char balance, ref char jobvl, ref char jobvr, ref char sense, ref int n, double* A, ref int lda, double* wr, double* wi, double* vl, ref int ldvl, double* vr, ref int ldvr, ref int ilo, ref int ihi, double* scale, ref double abnrm, double* rconde, double* rcondv, double* work, ref int lwork, int* iwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "SGEEVX", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_sgeevx(ref char balance, ref char jobvl, ref char jobvr, ref char sense, ref int n, float* A, ref int lda, float* wr, float* wi, float* vl, ref int ldvl, float* vr, ref int ldvr, ref int ilo, ref int ihi, float* scale, ref float abnrm, float* rconde, float* rcondv, float* work, ref int lwork, int* iwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "CGEEVX", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_cgeevx(ref char balance, ref char jobvl, ref char jobvr, ref char sense, ref int n, [In, Out] fcomplex* A, ref int lda, [In, Out] fcomplex* w, [In, Out] fcomplex* vl, ref int ldvl, [In, Out] fcomplex* vr, ref int ldvr, ref int ilo, ref int ihi, float* scale, ref float abnrm, float* rconde, float* rcondv, [In, Out] fcomplex* work, ref int lwork, float* rwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "ZGEEVX", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_zgeevx(ref char balance, ref char jobvl, ref char jobvr, ref char sense, ref int n, [In, Out]  complex* A, ref int lda, [In, Out] complex* w, [In, Out] complex* vl, ref int ldvl, [In, Out]  complex* vr, ref int ldvr, ref int ilo, ref int ihi, double* scale, ref double abnrm, double* rconde, double* rcondv, [In, Out] complex* work, ref int lwork, double* rwork, ref int info);

        [DllImport(MKL_FILENAME, EntryPoint = "DSYEVR", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_dsyevr(ref char jobz, ref char range, ref char uplo, ref int n, double* A, ref int lda, ref double vl, ref double vu, ref int il, ref int iu, ref double abstol, ref int m, double* w, double* z, ref int ldz, int* isuppz, double* work, ref int lwork, int* iwork, ref int liwork, ref int info);
        //[DllImport(MKL_FILENAME, EntryPoint = "DSYEVR",CallingConvention =CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        //internal static extern void mkl_dsyevr (ref char jobz, ref char range, ref char uplo, ref int n,          double  * A, ref int lda, ref double vl, ref double vu, ref int il, ref int iu, ref double abstol, ref int m, double* w,          double  * z, ref int ldz, int* isuppz, double* work, ref int lwork, int* iwork, ref int liwork, ref int info); 
        [DllImport(MKL_FILENAME, EntryPoint = "SSYEVR", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_ssyevr(ref char jobz, ref char range, ref char uplo, ref int n, float* A, ref int lda, ref float vl, ref float vu, ref int il, ref int iu, ref float abstol, ref int m, float* w, float* z, ref int ldz, int* isuppz, float* work, ref int lwork, int* iwork, ref int liwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "CHEEVR", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_cheevr(ref char jobz, ref char range, ref char uplo, ref int n, [In, Out] fcomplex* A, ref int lda, ref float vl, ref float vu, ref int il, ref int iu, ref float abstol, ref int m, float* w, [In, Out] fcomplex* z, ref int ldz, int* isuppz, [In, Out] fcomplex* work, ref int lwork, float* rwork, ref int lrwork, int* iwork, ref int liwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "ZHEEVR", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_zheevr(ref char jobz, ref char range, ref char uplo, ref int n, [In, Out] complex* A, ref int lda, ref double vl, ref double vu, ref int il, ref int iu, ref double abstol, ref int m, double* w, [In, Out] complex* z, ref int ldz, int* isuppz, [In, Out] complex* work, ref int lwork, double* rwork, ref int lrwork, int* iwork, ref int liwork, ref int info);

        [DllImport(MKL_FILENAME, EntryPoint = "DSYGV", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_dsygv(ref int itype, ref char jobz, ref char uplo, ref int n, double* A, ref int lda, double* B, ref int ldb, double* w, double* work, ref int lwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "SSYGV", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_ssygv(ref int itype, ref char jobz, ref char uplo, ref int n, float* A, ref int lda, float* B, ref int ldb, float* w, float* work, ref int lwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "CHEGV", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_chegv(ref int itype, ref char jobz, ref char uplo, ref int n, [In, Out] fcomplex* A, ref int lda, [In, Out] fcomplex* B, ref int ldb, float* w, [In, Out] fcomplex* work, ref int lwork, float* rwork, ref int info);
        [DllImport(MKL_FILENAME, EntryPoint = "ZHEGV", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecurityCritical]
        internal static extern void mkl_zhegv(ref int itype, ref char jobz, ref char uplo, ref int n, [In, Out] complex* A, ref int lda, [In, Out] complex* B, ref int ldb, double* w, [In, Out]  complex* work, ref int lwork, double* rwork, ref int info);
        
        #endregion DLL INCLUDES

    }

}
