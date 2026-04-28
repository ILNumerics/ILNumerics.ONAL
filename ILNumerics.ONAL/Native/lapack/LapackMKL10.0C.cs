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

#pragma warning disable 1570, 1591
using ILNumerics.Core.MemoryLayer;
using System;
using static ILNumerics.Core.Functions.Builtin.MathInternal;
using static ILNumerics.Core.Native.MKLImports;
using static ILNumerics.Core.Native.MKLValues;

namespace ILNumerics.Core.Native {
    /// <summary>
    /// LAPACK interface implementation, specialized for Intel's (R) MKL (R).
    /// </summary>
    /// 
    public unsafe class LapackMKL10_0 : ILapack {

        #region attributes
#pragma warning disable CS0169
        string m_version;
#pragma warning restore CS0168
        #endregion

        public LapackMKL10_0() {
            Init();
        }

        
        public static void Init() {
            try {
                SetNumThreads((int)Settings.MaxNumberThreads);
            } catch (System.IO.FileNotFoundException) { 
            } catch (System.BadImageFormatException) { }
        }

        #region ILAPACK INTERFACE 

        #region service functions
        
        private int ILAENV(int ispec, string name, string opts, int n1, int n2, int n3, int n4) {
            return mkl_ilaenv(ref ispec, ref name, ref opts, ref n1, ref n2, ref n3, ref n4);
        }
        
        private static void SetNumThreads(int numThreads) {
            try {

                mkl_set_num_threads(ref numThreads);
                int mask = MKL_FFT;
                mkl_domain_set_num_threads(ref numThreads, ref mask);

            } catch (DllNotFoundException) {
            } catch (System.IO.FileNotFoundException) {
            } catch (System.BadImageFormatException) {
            }
        }
        
        private static void SetDomainNumThreads(int numThreads) {
            int mklblas = MKL_BLAS;
            mkl_domain_set_num_threads(ref numThreads, ref mklblas);
            //omp_set_num_threads(numThreads); 
        }
        
        private static void SetDynamicThreads(bool dynamic) {
            int val = dynamic ? 1 : 0;
            mkl_set_dynamic(ref val);
        }
        
        public static int GetMaxThreads() {
            int dummy = MKL_BLAS;
            int ret = mkl_domain_get_max_threads(ref dummy);
            return ret;
        }
        /// <summary>
        /// Free all buffers from the MKL Fast Memory Management. Use sparingly and carefully! 
        /// </summary>
        
        public void FreeBuffers() {
            mkl_free_buffers();
        }
        #endregion

        #region ?gemm (fortran interface)
        /// <summary>
        /// Implement wrapper for BLAS GeneralMatrixMultiply
        /// </summary>
        /// <param name="TransA">Transposition state for matrix A: one of the constants in enum CBlas_Transpose</param>
        /// <param name="TransB">Transposition state for matrix B: one of the constants in enum CBlas_Transpose</param>
        /// <param name="M">Number of rows in A</param>
        /// <param name="N">Number of columns in B</param>
        /// <param name="K">Number of columns in A and number of rows in B</param>
        /// <param name="alpha">multiplicationi factor for A</param>
        /// <param name="A">pointer to double array A</param>
        /// <param name="lda">distance between first elements of each column for column based orientation or 
        /// distance between first elements of each row for row based orientation for matrix B</param>
        /// <param name="B">pointer to double array B</param>
        /// <param name="ldb">distance between first elements of each column for column based orientation or 
        /// distance between first elements of each row for row based orientation for matrix A</param>
        /// <param name="beta">multiplication faktor for matrix B</param>
        /// <param name="C">pointer to predefined double array C of neccessary length</param>
        /// <param name="ldc">distance between first elements of each column for column based orientation or 
        /// distance between first elements of each row for row based orientation for matrix C</param>
        /// <remarks>All parameters except C are readonly. Only elements of matrix C will be altered. C must be a predefined 
        /// continous double array of size MxN</remarks>
        
        public void dgemm(char TransA, char TransB, int M, int N, int K, double alpha, double* A, int lda, double* B, int ldb, double beta, double* C, int ldc) {
            mkl_dgemm(ref TransA, ref TransB, ref M, ref N, ref K, ref alpha, (IntPtr)A, ref lda, (IntPtr)B, ref ldb, ref beta, C, ref ldc);
        }
        /// <summary>
        /// Implement wrapper for BLAS GeneralMatrixMultiply
        /// </summary>
        /// <param name="TransA">Transposition state for matrix A: one of the constants in enum CBlas_Transpose</param>
        /// <param name="TransB">Transposition state for matrix B: one of the constants in enum CBlas_Transpose</param>
        /// <param name="M">Number of rows in A</param>
        /// <param name="N">Number of columns in B</param>
        /// <param name="K">Number of columns in A and number of rows in B</param>
        /// <param name="alpha">multiplicationi factor for A</param>
        /// <param name="A">pointer to double array A</param>
        /// <param name="lda">distance between first elements of each column for column based orientation or 
        /// distance between first elements of each row for row based orientation for matrix B</param>
        /// <param name="B">pointer to double array B</param>
        /// <param name="ldb">distance between first elements of each column for column based orientation or 
        /// distance between first elements of each row for row based orientation for matrix A</param>
        /// <param name="beta">multiplication faktor for matrix B</param>
        /// <param name="C">pointer to predefined double array C of neccessary length</param>
        /// <param name="ldc">distance between first elements of each column for column based orientation or 
        /// distance between first elements of each row for row based orientation for matrix C</param>
        /// <remarks>All parameters except C are readonly. Only elements of matrix C will be altered. C must be a predefined 
        /// continous double array of size MxN</remarks>
        
        public void sgemm(char TransA, char TransB, int M, int N, int K, float alpha, float* A, int lda, float* B, int ldb, float beta, float* C, int ldc) {
            mkl_sgemm(ref TransA, ref TransB, ref M, ref N, ref K, ref alpha, (IntPtr)A, ref lda, (IntPtr)B, ref ldb, ref beta, C, ref ldc);
        }
        /// <summary>
        /// Implement wrapper for BLAS GeneralMatrixMultiply
        /// </summary>
        /// <param name="TransA">Transposition state for matrix A: one of the constants in enum CBlas_Transpose</param>
        /// <param name="TransB">Transposition state for matrix B: one of the constants in enum CBlas_Transpose</param>
        /// <param name="M">Number of rows in A</param>
        /// <param name="N">Number of columns in B</param>
        /// <param name="K">Number of columns in A and number of rows in B</param>
        /// <param name="alpha">multiplicationi factor for A</param>
        /// <param name="A">pointer to double array A</param>
        /// <param name="lda">distance between first elements of each column for column based orientation or 
        /// distance between first elements of each row for row based orientation for matrix B</param>
        /// <param name="B">pointer to double array B</param>
        /// <param name="ldb">distance between first elements of each column for column based orientation or 
        /// distance between first elements of each row for row based orientation for matrix A</param>
        /// <param name="beta">multiplication faktor for matrix B</param>
        /// <param name="C">pointer to predefined double array C of neccessary length</param>
        /// <param name="ldc">distance between first elements of each column for column based orientation or 
        /// distance between first elements of each row for row based orientation for matrix C</param>
        /// <remarks>All parameters except C are readonly. Only elements of matrix C will be altered. C must be a predefined 
        /// continous double array of size MxN</remarks>
        
        public void cgemm(char TransA, char TransB, int M, int N, int K, fcomplex alpha, fcomplex* A, int lda, fcomplex* B, int ldb, fcomplex beta, fcomplex* C, int ldc) {

            mkl_cgemm(ref TransA, ref TransB, ref M, ref N, ref K, ref alpha, (IntPtr)A, ref lda, (IntPtr)B, ref ldb, ref beta, C, ref ldc);
        }
        /// <summary>
        /// Implement wrapper for BLAS GeneralMatrixMultiply
        /// </summary>
        /// <param name="TransA">Transposition state for matrix A: one of the constants in enum CBlas_Transpose</param>
        /// <param name="TransB">Transposition state for matrix B: one of the constants in enum CBlas_Transpose</param>
        /// <param name="M">Number of rows in A</param>
        /// <param name="N">Number of columns in B</param>
        /// <param name="K">Number of columns in A and number of rows in B</param>
        /// <param name="alpha">multiplicationi factor for A</param>
        /// <param name="A">pointer to double array A</param>
        /// <param name="lda">distance between first elements of each column for column based orientation or 
        /// distance between first elements of each row for row based orientation for matrix B</param>
        /// <param name="B">pointer to double array B</param>
        /// <param name="ldb">distance between first elements of each column for column based orientation or 
        /// distance between first elements of each row for row based orientation for matrix A</param>
        /// <param name="beta">multiplication faktor for matrix B</param>
        /// <param name="C">pointer to predefined double array C of neccessary length</param>
        /// <param name="ldc">distance between first elements of each column for column based orientation or 
        /// distance between first elements of each row for row based orientation for matrix C</param>
        /// <remarks>All parameters except C are readonly. Only elements of matrix C will be altered. C must be a predefined 
        /// continous double array of size MxN</remarks>
        
        public void zgemm(char TransA, char TransB, int M, int N, int K, complex alpha, complex* A, int lda, complex* B, int ldb, complex beta, complex* C, int ldc) {
            mkl_zgemm(ref TransA, ref TransB, ref M, ref N, ref K, ref alpha, (IntPtr)A, ref lda, (IntPtr)B, ref ldb, ref beta, C, ref ldc);
        }
        #endregion

        #region HYCALPER LOOPSTART GESDD
        /*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        double
    </source>
    <destination>float</destination>
</type>
<type>
    <source locate="after">
        outArrS
    </source>
    <destination>float</destination>
</type>
<type>
    <source locate="after">
        outArrU
    </source>
    <destination>float</destination>
</type>
<type>
    <source locate="here">
        dgesdd
    </source>
    <destination>sgesdd</destination>
</type>
<type>
    <source locate="here">
        dgesvd
    </source>
    <destination>sgesvd</destination>
</type>
</hycalper>
*/
        
        public unsafe void dgesdd(char jobz, int m, int n, double* a, int lda, double* s, double* u, int ldu, double* vt, int ldvt, ref int info) {
            double work = 1;
            MemoryHandle workHandle = null;
            MemoryHandle iWorkHandle = null;
            try {
                int lwork = -1;
                iWorkHandle = New<int>((ulong)Math.Max(1, ((m < n) ? m : n) * 8));

                mkl_dgesdd(ref jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, &work, ref lwork, (int*)iWorkHandle.Pointer, ref info);

                if (work > 0) {
                    workHandle = New<double>((ulong)work, 0);
                    lwork = (int)work;
                    mkl_dgesdd(ref jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, (double*)workHandle.Pointer, ref lwork, (int*)iWorkHandle.Pointer, ref info);
                }

            } catch (OutOfMemoryException) {
                dgesvd(jobz, m, n, a, lda, s, u, ldu, vt, ldvt, ref info);
            } catch (Exception e) {
                var msg = $"Error in dgesdd: " + e.ToString();
                System.Diagnostics.Trace.WriteLine(msg);
                throw new ArgumentException(msg, e);
            } finally {
                if (workHandle != null) {
                    free<double>(workHandle, 0);
                }
                if (iWorkHandle != null) {
                    free<int>(iWorkHandle, 0);
                }
            }

        }
        #endregion HYCALPER LOOPEND GESDD
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        
        public unsafe void sgesdd(char jobz, int m, int n, float* a, int lda, float* s, float* u, int ldu, float* vt, int ldvt, ref int info) {
            float work = 1;
            MemoryHandle workHandle = null;
            MemoryHandle iWorkHandle = null;
            try {
                int lwork = -1;
                iWorkHandle = New<int>((ulong)Math.Max(1, ((m < n) ? m : n) * 8));

                mkl_sgesdd(ref jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, &work, ref lwork, (int*)iWorkHandle.Pointer, ref info);

                if (work > 0) {
                    workHandle = New<float>((ulong)work, 0);
                    lwork = (int)work;
                    mkl_sgesdd(ref jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, (float*)workHandle.Pointer, ref lwork, (int*)iWorkHandle.Pointer, ref info);
                }

            } catch (OutOfMemoryException) {
                sgesvd(jobz, m, n, a, lda, s, u, ldu, vt, ldvt, ref info);
            } catch (Exception e) {
                var msg = $"Error in sgesdd: " + e.ToString();
                System.Diagnostics.Trace.WriteLine(msg);
                throw new ArgumentException(msg, e);
            } finally {
                if (workHandle != null) {
                    free<float>(workHandle, 0);
                }
                if (iWorkHandle != null) {
                    free<int>(iWorkHandle, 0);
                }
            }

        }

#endregion HYCALPER AUTO GENERATED CODE

        #region HYCALPER LOOPSTART Complex_SDD
        /*!HC:TYPELIST:
        <hycalper>
        <type>
            <source locate="here">
                complex
            </source>
            <destination>fcomplex</destination>
        </type>
        <type>
            <source locate="here">
                double
            </source>
            <destination>float</destination>
        </type>
        <type>
            <source locate="here">
                zgesdd
            </source>
            <destination>cgesdd</destination>
        </type>
        <type>
            <source locate="here">
                zgesvd
            </source>
            <destination>cgesvd</destination>
        </type>
        </hycalper>
        */
        
        public void zgesdd(char jobz, int m, int n, complex* a, int lda, double* s, complex* u, int ldu, complex* vt, int ldvt, ref int info) {
            MemoryHandle workHandle = null;
            MemoryHandle rworkHandle = null;
            MemoryHandle iworkHandle = null;
            try {
                complex work = 0;
                int minMN = (m < n) ? m : n;
                if (jobz == 'N') {
                    rworkHandle = New<double>((ulong)(minMN * 7));
                } else {
                    rworkHandle = New<double>((ulong)(5 * minMN * minMN + 5 * minMN));
                }
                int lwork = -1;
                iworkHandle = New<int>((ulong)minMN * 8);

                mkl_zgesdd(ref jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, &work, ref lwork, (double*)rworkHandle.Pointer, (int*)iworkHandle.Pointer, ref info);
                if (work != 0) {
                    workHandle = New<complex>((ulong)work.real);
                    lwork = (int)work.real;

                    mkl_zgesdd(ref jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, (complex*)workHandle.Pointer, ref lwork, (double*)rworkHandle.Pointer, (int*)iworkHandle.Pointer, ref info);
                }
            } catch (OutOfMemoryException) {
                zgesvd(jobz, m, n, a, lda, s, u, ldu, vt, ldvt, ref info);
            } catch (Exception e) {
                throw new ArgumentException("Error in zgesdd. The inner exception may contain further details.", e);
            }
        }
        #endregion HYCALPER LOOPEND COMPLEX_SDD
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        
        public void cgesdd(char jobz, int m, int n, fcomplex* a, int lda, float* s, fcomplex* u, int ldu, fcomplex* vt, int ldvt, ref int info) {
            MemoryHandle workHandle = null;
            MemoryHandle rworkHandle = null;
            MemoryHandle iworkHandle = null;
            try {
                fcomplex work = 0;
                int minMN = (m < n) ? m : n;
                if (jobz == 'N') {
                    rworkHandle = New<float>((ulong)(minMN * 7));
                } else {
                    rworkHandle = New<float>((ulong)(5 * minMN * minMN + 5 * minMN));
                }
                int lwork = -1;
                iworkHandle = New<int>((ulong)minMN * 8);

                mkl_cgesdd(ref jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, &work, ref lwork, (float*)rworkHandle.Pointer, (int*)iworkHandle.Pointer, ref info);
                if (work != 0) {
                    workHandle = New<fcomplex>((ulong)work.real);
                    lwork = (int)work.real;

                    mkl_cgesdd(ref jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, (fcomplex*)workHandle.Pointer, ref lwork, (float*)rworkHandle.Pointer, (int*)iworkHandle.Pointer, ref info);
                }
            } catch (OutOfMemoryException) {
                cgesvd(jobz, m, n, a, lda, s, u, ldu, vt, ldvt, ref info);
            } catch (Exception e) {
                throw new ArgumentException("Error in cgesdd. The inner exception may contain further details.", e);
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

        #region HYCALPER LOOPSTART GESVD
        /*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        double
    </source>
    <destination>fcomplex</destination>
    <destination>complex</destination>
    <destination>float</destination>
</type>
<type>
    <source locate="here">
        Double
    </source>
    <destination>float</destination>
    <destination>double</destination>
    <destination>float</destination>
</type>
<type>
    <source locate="here">
        dgesvd
    </source>
    <destination>cgesvd</destination>
    <destination>zgesvd</destination>
    <destination>sgesvd</destination>
</type>
</hycalper>
*/

        /// <summary>
        /// singular value decomposition
        /// </summary>
        /// <param name="jobz"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="a"></param>
        /// <param name="lda"></param>
        /// <param name="s"></param>
        /// <param name="u"></param>
        /// <param name="ldu"></param>
        /// <param name="vt"></param>
        /// <param name="ldvt"></param>
        /// <param name="info"></param>
        
        public unsafe void dgesvd(char jobz, int m, int n, double* a, int lda,
                           Double* s, double* u, int ldu, double* vt, int ldvt, ref int info) {
            if (jobz != 'A' && jobz != 'S' && jobz != 'N')
                throw new ArgumentException("Argument jobz must be one of 'A','S' or 'N'");
            double work = 0;
            int lwork = -1;
            var iworkHandle = New<int>((ulong)((m < n) ? m : n) * 8);
            mkl_dgesvd(ref jobz, ref jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, &work, ref lwork, (int*)iworkHandle.Pointer, ref info);
            if (work != 0) {
                var workHandle = New<double>((ulong)work);
                lwork = (int)work;
                mkl_dgesvd(ref jobz, ref jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, (double*)workHandle.Pointer, ref lwork, (int*)iworkHandle.Pointer, ref info);
                if (workHandle != null) {
                    free<double>(workHandle, 0);
                }
            } else {
                throw new ArgumentException("Error in dgesvd: unable to determine working set size.");
            }
            if (iworkHandle != null) {
                free<int>(iworkHandle, 0);
            }

        }
        #endregion HYCALPER LOOPEND GESVD
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        /// <summary>
        /// singular value decomposition
        /// </summary>
        /// <param name="jobz"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="a"></param>
        /// <param name="lda"></param>
        /// <param name="s"></param>
        /// <param name="u"></param>
        /// <param name="ldu"></param>
        /// <param name="vt"></param>
        /// <param name="ldvt"></param>
        /// <param name="info"></param>
        
        public unsafe void sgesvd(char jobz, int m, int n, float* a, int lda,
                           float* s, float* u, int ldu, float* vt, int ldvt, ref int info) {
            if (jobz != 'A' && jobz != 'S' && jobz != 'N')
                throw new ArgumentException("Argument jobz must be one of 'A','S' or 'N'");
            float work = 0;
            int lwork = -1;
            var iworkHandle = New<int>((ulong)((m < n) ? m : n) * 8);
            mkl_sgesvd(ref jobz, ref jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, &work, ref lwork, (int*)iworkHandle.Pointer, ref info);
            if (work != 0) {
                var workHandle = New<float>((ulong)work);
                lwork = (int)work;
                mkl_sgesvd(ref jobz, ref jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, (float*)workHandle.Pointer, ref lwork, (int*)iworkHandle.Pointer, ref info);
                if (workHandle != null) {
                    free<float>(workHandle, 0);
                }
            } else {
                throw new ArgumentException("Error in sgesvd: unable to determine working set size.");
            }
            if (iworkHandle != null) {
                free<int>(iworkHandle, 0);
            }

        }
       

        /// <summary>
        /// singular value decomposition
        /// </summary>
        /// <param name="jobz"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="a"></param>
        /// <param name="lda"></param>
        /// <param name="s"></param>
        /// <param name="u"></param>
        /// <param name="ldu"></param>
        /// <param name="vt"></param>
        /// <param name="ldvt"></param>
        /// <param name="info"></param>
        
        public unsafe void zgesvd(char jobz, int m, int n, complex* a, int lda,
                           double* s, complex* u, int ldu, complex* vt, int ldvt, ref int info) {
            if (jobz != 'A' && jobz != 'S' && jobz != 'N')
                throw new ArgumentException("Argument jobz must be one of 'A','S' or 'N'");
            complex work = 0;
            int lwork = -1;
            var iworkHandle = New<int>((ulong)((m < n) ? m : n) * 8);
            mkl_zgesvd(ref jobz, ref jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, &work, ref lwork, (int*)iworkHandle.Pointer, ref info);
            if (work != 0) {
                var workHandle = New<complex>((ulong)work);
                lwork = (int)work;
                mkl_zgesvd(ref jobz, ref jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, (complex*)workHandle.Pointer, ref lwork, (int*)iworkHandle.Pointer, ref info);
                if (workHandle != null) {
                    free<complex>(workHandle, 0);
                }
            } else {
                throw new ArgumentException("Error in zgesvd: unable to determine working set size.");
            }
            if (iworkHandle != null) {
                free<int>(iworkHandle, 0);
            }

        }
       

        /// <summary>
        /// singular value decomposition
        /// </summary>
        /// <param name="jobz"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="a"></param>
        /// <param name="lda"></param>
        /// <param name="s"></param>
        /// <param name="u"></param>
        /// <param name="ldu"></param>
        /// <param name="vt"></param>
        /// <param name="ldvt"></param>
        /// <param name="info"></param>
        
        public unsafe void cgesvd(char jobz, int m, int n, fcomplex* a, int lda,
                           float* s, fcomplex* u, int ldu, fcomplex* vt, int ldvt, ref int info) {
            if (jobz != 'A' && jobz != 'S' && jobz != 'N')
                throw new ArgumentException("Argument jobz must be one of 'A','S' or 'N'");
            fcomplex work = 0;
            int lwork = -1;
            var iworkHandle = New<int>((ulong)((m < n) ? m : n) * 8);
            mkl_cgesvd(ref jobz, ref jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, &work, ref lwork, (int*)iworkHandle.Pointer, ref info);
            if (work != 0) {
                var workHandle = New<fcomplex>((ulong)work);
                lwork = (int)work;
                mkl_cgesvd(ref jobz, ref jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, (fcomplex*)workHandle.Pointer, ref lwork, (int*)iworkHandle.Pointer, ref info);
                if (workHandle != null) {
                    free<fcomplex>(workHandle, 0);
                }
            } else {
                throw new ArgumentException("Error in cgesvd: unable to determine working set size.");
            }
            if (iworkHandle != null) {
                free<int>(iworkHandle, 0);
            }

        }

#endregion HYCALPER AUTO GENERATED CODE

        #region ?potrf ...

        
        public void dpotrf(char uplo, int n, double* A, int lda, ref int info) {
            mkl_dpotrf(ref uplo, ref n, A, ref lda, ref info);
        }
        
        public void spotrf(char uplo, int n, float* A, int lda, ref int info) {
            mkl_spotrf(ref uplo, ref n, A, ref lda, ref info);
        }
        
        public void cpotrf(char uplo, int n, fcomplex* A, int lda, ref int info) {
            mkl_cpotrf(ref uplo, ref n, A, ref lda, ref info);
        }
        
        public void zpotrf(char uplo, int n, complex* A, int lda, ref int info) {
            mkl_zpotrf(ref uplo, ref n, A, ref lda, ref info);
        }


        
        public void dpotri(char uplo, int n, double* A, int lda, ref int info) {
            mkl_dpotri(ref uplo, ref n, A, ref lda, ref info);
        }
        
        public void spotri(char uplo, int n, float* A, int lda, ref int info) {
            mkl_spotri(ref uplo, ref n, A, ref lda, ref info);
        }
        
        public void cpotri(char uplo, int n, fcomplex* A, int lda, ref int info) {
            mkl_cpotri(ref uplo, ref n, A, ref lda, ref info);
        }
        
        public void zpotri(char uplo, int n, complex* A, int lda, ref int info) {
            mkl_zpotri(ref uplo, ref n, A, ref lda, ref info);
        }


        
        public void dgetrf(int M, int N, double* A, int LDA, int* IPIV, ref int info) {
            mkl_dgetrf(ref M, ref N, A, ref LDA, IPIV, ref info);
        }
        
        public void sgetrf(int M, int N, float* A, int LDA, int* IPIV, ref int info) {
            mkl_sgetrf(ref M, ref N, A, ref LDA, IPIV, ref info);
        }
        
        public void cgetrf(int M, int N, fcomplex* A, int LDA, int* IPIV, ref int info) {
            mkl_cgetrf(ref M, ref N, A, ref LDA, IPIV, ref info);
        }
        
        public void zgetrf(int M, int N, complex* A, int LDA, int* IPIV, ref int info) {
            mkl_zgetrf(ref M, ref N, A, ref LDA, IPIV, ref info);
        }
        #endregion

        #region HYCALPER LOOPSTART ?GETRI 
        /*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        double
    </source>
    <destination>fcomplex</destination>
    <destination>complex</destination>
    <destination>float</destination>
</type>
<type>
    <source locate="here">
        Double
    </source>
    <destination>float</destination>
    <destination>double</destination>
    <destination>float</destination>
</type>
<type>
    <source locate="here">
        dgetri
    </source>
    <destination>cgetri</destination>
    <destination>zgetri</destination>
    <destination>sgetri</destination>
</type>
</hycalper>
*/
        
        public void dgetri(int N, double* A, int LDA, int* IPIV, ref int info) {
            double work = 1;
            int lwork = -1;
            MemoryHandle workHandle = null;
            try {
                mkl_dgetri(ref N, A, ref LDA, IPIV, &work, ref lwork, ref info);
                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<double>((ulong)lwork);
                    mkl_dgetri(ref N, A, ref LDA, IPIV, (double*)workHandle.Pointer, ref lwork, ref info);
                } else {
                    throw new ArgumentException($"Error in dgetri. Unable to determine required working set size. info: {info}");
                }
            } catch (OutOfMemoryException e) {
                throw new ArgumentException($"Error in dgetri. Not enough memory! {lwork} double elements were required.", e);
            } finally {
                if (workHandle != null) {
                    free<double>(workHandle, 0);
                }
            }
        }
        #endregion HYCALPER LOOPEND ?GETRI
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        
        public void sgetri(int N, float* A, int LDA, int* IPIV, ref int info) {
            float work = 1;
            int lwork = -1;
            MemoryHandle workHandle = null;
            try {
                mkl_sgetri(ref N, A, ref LDA, IPIV, &work, ref lwork, ref info);
                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<float>((ulong)lwork);
                    mkl_sgetri(ref N, A, ref LDA, IPIV, (float*)workHandle.Pointer, ref lwork, ref info);
                } else {
                    throw new ArgumentException($"Error in sgetri. Unable to determine required working set size. info: {info}");
                }
            } catch (OutOfMemoryException e) {
                throw new ArgumentException($"Error in sgetri. Not enough memory! {lwork} float elements were required.", e);
            } finally {
                if (workHandle != null) {
                    free<float>(workHandle, 0);
                }
            }
        }
       
        
        public void zgetri(int N, complex* A, int LDA, int* IPIV, ref int info) {
            complex work = 1;
            int lwork = -1;
            MemoryHandle workHandle = null;
            try {
                mkl_zgetri(ref N, A, ref LDA, IPIV, &work, ref lwork, ref info);
                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<complex>((ulong)lwork);
                    mkl_zgetri(ref N, A, ref LDA, IPIV, (complex*)workHandle.Pointer, ref lwork, ref info);
                } else {
                    throw new ArgumentException($"Error in zgetri. Unable to determine required working set size. info: {info}");
                }
            } catch (OutOfMemoryException e) {
                throw new ArgumentException($"Error in zgetri. Not enough memory! {lwork} complex elements were required.", e);
            } finally {
                if (workHandle != null) {
                    free<complex>(workHandle, 0);
                }
            }
        }
       
        
        public void cgetri(int N, fcomplex* A, int LDA, int* IPIV, ref int info) {
            fcomplex work = 1;
            int lwork = -1;
            MemoryHandle workHandle = null;
            try {
                mkl_cgetri(ref N, A, ref LDA, IPIV, &work, ref lwork, ref info);
                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<fcomplex>((ulong)lwork);
                    mkl_cgetri(ref N, A, ref LDA, IPIV, (fcomplex*)workHandle.Pointer, ref lwork, ref info);
                } else {
                    throw new ArgumentException($"Error in cgetri. Unable to determine required working set size. info: {info}");
                }
            } catch (OutOfMemoryException e) {
                throw new ArgumentException($"Error in cgetri. Not enough memory! {lwork} fcomplex elements were required.", e);
            } finally {
                if (workHandle != null) {
                    free<fcomplex>(workHandle, 0);
                }
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

        #region HYCALPER LOOPSTART ?GEQRF 
        /*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        double
    </source>
    <destination>fcomplex</destination>
    <destination>complex</destination>
    <destination>float</destination>
</type>
<type>
    <source locate="here">
        dgeqrf
    </source>
    <destination>cgeqrf</destination>
    <destination>zgeqrf</destination>
    <destination>sgeqrf</destination>
</type>
</hycalper>
*/
        
        public void /*!HC:dllfunc*/ dgeqrf(int M, int N, double* A, int lda, double* tau, ref int info) {
            double work = 1;
            int lwork = -1;
            MemoryHandle workHandle = null;
            try {
                mkl_dgeqrf(ref M, ref N, A, ref lda, tau, &work, ref lwork, ref info);
                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<double>((ulong)lwork);
                    mkl_dgeqrf(ref M, ref N, A, ref lda, tau, (double*)workHandle.Pointer, ref lwork, ref info);
                } else {
                    throw new ArgumentException("Error in dgeqrf: Unable to determine required working set size.");
                }
            } catch (OutOfMemoryException e) {
                throw new ArgumentException($"Error in dgeqrf. Not enough memory! {lwork} double elements were required.", e);
            } finally {
                if (workHandle != null) {
                    free<double>(workHandle, 0);
                }
            }
        }
        #endregion HYCALPER LOOPEND GEQRF
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        
        public void  sgeqrf(int M, int N, float* A, int lda, float* tau, ref int info) {
            float work = 1;
            int lwork = -1;
            MemoryHandle workHandle = null;
            try {
                mkl_sgeqrf(ref M, ref N, A, ref lda, tau, &work, ref lwork, ref info);
                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<float>((ulong)lwork);
                    mkl_sgeqrf(ref M, ref N, A, ref lda, tau, (float*)workHandle.Pointer, ref lwork, ref info);
                } else {
                    throw new ArgumentException("Error in sgeqrf: Unable to determine required working set size.");
                }
            } catch (OutOfMemoryException e) {
                throw new ArgumentException($"Error in sgeqrf. Not enough memory! {lwork} float elements were required.", e);
            } finally {
                if (workHandle != null) {
                    free<float>(workHandle, 0);
                }
            }
        }
       
        
        public void  zgeqrf(int M, int N, complex* A, int lda, complex* tau, ref int info) {
            complex work = 1;
            int lwork = -1;
            MemoryHandle workHandle = null;
            try {
                mkl_zgeqrf(ref M, ref N, A, ref lda, tau, &work, ref lwork, ref info);
                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<complex>((ulong)lwork);
                    mkl_zgeqrf(ref M, ref N, A, ref lda, tau, (complex*)workHandle.Pointer, ref lwork, ref info);
                } else {
                    throw new ArgumentException("Error in zgeqrf: Unable to determine required working set size.");
                }
            } catch (OutOfMemoryException e) {
                throw new ArgumentException($"Error in zgeqrf. Not enough memory! {lwork} complex elements were required.", e);
            } finally {
                if (workHandle != null) {
                    free<complex>(workHandle, 0);
                }
            }
        }
       
        
        public void  cgeqrf(int M, int N, fcomplex* A, int lda, fcomplex* tau, ref int info) {
            fcomplex work = 1;
            int lwork = -1;
            MemoryHandle workHandle = null;
            try {
                mkl_cgeqrf(ref M, ref N, A, ref lda, tau, &work, ref lwork, ref info);
                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<fcomplex>((ulong)lwork);
                    mkl_cgeqrf(ref M, ref N, A, ref lda, tau, (fcomplex*)workHandle.Pointer, ref lwork, ref info);
                } else {
                    throw new ArgumentException("Error in cgeqrf: Unable to determine required working set size.");
                }
            } catch (OutOfMemoryException e) {
                throw new ArgumentException($"Error in cgeqrf. Not enough memory! {lwork} fcomplex elements were required.", e);
            } finally {
                if (workHandle != null) {
                    free<fcomplex>(workHandle, 0);
                }
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

        
        public void dormqr(char side, char trans, int m, int n, int k, double* A, int lda, double* tau, double* C, int LDC, ref int info) {
            throw new Exception("The method or operation is not implemented.");
        }
        
        public void sormqr(char side, char trans, int m, int n, int k, float* A, int lda, float* tau, float* C, int LDC, ref int info) {
            throw new Exception("The method or operation is not implemented.");
        }

        #region HYCALPER LOOPSTART ?ORGQR  / UNGQR 
        /*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        double
    </source>
    <destination>fcomplex</destination>
    <destination>complex</destination>
    <destination>float</destination>
</type>
<type>
    <source locate="here">
        dorgqr
    </source>
    <destination>cungqr</destination>
    <destination>zungqr</destination>
    <destination>sorgqr</destination>
</type>
</hycalper>
*/
        
        public void dorgqr(int M, int N, int K, double* A, int lda, double* tau, ref int info) {
            double work = 1;
            int lwork = -1;
            MemoryHandle workHandle = null;
            try {
                mkl_dorgqr(ref M, ref N, ref K, A, ref lda, tau, &work, ref lwork, ref info);
                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<double>((ulong)lwork);

                    mkl_dorgqr(ref M, ref N, ref K, A, ref lda, tau, (double*)workHandle.Pointer, ref lwork, ref info);
                } else {
                    throw new ArgumentException("Error in dorgqr: Unable to determine the required working set size.");
                }
            } catch (OutOfMemoryException e) {
                throw new ArgumentException($"Error in dorgqr. Not enough memory! {lwork} double elements were required.", e);
            } finally {
                if (workHandle != null) {
                    free<fcomplex>(workHandle, 0);
                }
            }
        }
        #endregion HYCALPER LOOPEND ?ORGQR / UNGQR
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        
        public void sorgqr(int M, int N, int K, float* A, int lda, float* tau, ref int info) {
            float work = 1;
            int lwork = -1;
            MemoryHandle workHandle = null;
            try {
                mkl_sorgqr(ref M, ref N, ref K, A, ref lda, tau, &work, ref lwork, ref info);
                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<float>((ulong)lwork);

                    mkl_sorgqr(ref M, ref N, ref K, A, ref lda, tau, (float*)workHandle.Pointer, ref lwork, ref info);
                } else {
                    throw new ArgumentException("Error in sorgqr: Unable to determine the required working set size.");
                }
            } catch (OutOfMemoryException e) {
                throw new ArgumentException($"Error in sorgqr. Not enough memory! {lwork} float elements were required.", e);
            } finally {
                if (workHandle != null) {
                    free<fcomplex>(workHandle, 0);
                }
            }
        }
       
        
        public void zungqr(int M, int N, int K, complex* A, int lda, complex* tau, ref int info) {
            complex work = 1;
            int lwork = -1;
            MemoryHandle workHandle = null;
            try {
                mkl_zungqr(ref M, ref N, ref K, A, ref lda, tau, &work, ref lwork, ref info);
                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<complex>((ulong)lwork);

                    mkl_zungqr(ref M, ref N, ref K, A, ref lda, tau, (complex*)workHandle.Pointer, ref lwork, ref info);
                } else {
                    throw new ArgumentException("Error in zungqr: Unable to determine the required working set size.");
                }
            } catch (OutOfMemoryException e) {
                throw new ArgumentException($"Error in zungqr. Not enough memory! {lwork} complex elements were required.", e);
            } finally {
                if (workHandle != null) {
                    free<fcomplex>(workHandle, 0);
                }
            }
        }
       
        
        public void cungqr(int M, int N, int K, fcomplex* A, int lda, fcomplex* tau, ref int info) {
            fcomplex work = 1;
            int lwork = -1;
            MemoryHandle workHandle = null;
            try {
                mkl_cungqr(ref M, ref N, ref K, A, ref lda, tau, &work, ref lwork, ref info);
                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<fcomplex>((ulong)lwork);

                    mkl_cungqr(ref M, ref N, ref K, A, ref lda, tau, (fcomplex*)workHandle.Pointer, ref lwork, ref info);
                } else {
                    throw new ArgumentException("Error in cungqr: Unable to determine the required working set size.");
                }
            } catch (OutOfMemoryException e) {
                throw new ArgumentException($"Error in cungqr. Not enough memory! {lwork} fcomplex elements were required.", e);
            } finally {
                if (workHandle != null) {
                    free<fcomplex>(workHandle, 0);
                }
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

        #region HYCALPER LOOPSTART ?GEQP3 
        /*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        double
    </source>
    <destination>fcomplex</destination>
    <destination>complex</destination>
    <destination>float</destination>
</type>
<type>
    <source locate="here">
        Double
    </source>
    <destination>float</destination>
    <destination>double</destination>
    <destination>float</destination>
</type>
<type>
    <source locate="here">
        dgeqp3
    </source>
    <destination>cgeqp3</destination>
    <destination>zgeqp3</destination>
    <destination>sgeqp3</destination>
</type>
<type>
    <source locate="nextline">
        cmplxRwork
    </source>
    <destination>#if IS_COMPLEX</destination>
    <destination>#if IS_COMPLEX</destination>
    <destination>#if !IS_COMPLEX</destination>
</type>
</hycalper>
*/
        
        public void /*!HC:dllfunc*/ dgeqp3(int M, int N, double* A, int LDA, int* JPVT, double* tau, ref int info) {
            double work = 0;
            int lwork = -1;
            MemoryHandle workHandle = null;
            MemoryHandle rWorkHandle = null;
            try {
                /*!HC:cmplxRwork*/
#if !IS_COMPLEX
                mkl_dgeqp3(ref M, ref N, A, ref LDA, JPVT, tau, &work, ref lwork, ref info);

#else
                rWorkHandle = New<Double>((ulong)(2 * N));
                mkl_dgeqp3 (ref M, ref N, A, ref LDA, JPVT, tau, &work, ref lwork, (Double*)rWorkHandle.Pointer, ref info);
#endif

                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<double>((ulong)lwork);
                    /*!HC:cmplxRwork*/
#if !IS_COMPLEX
                    mkl_dgeqp3(ref M, ref N, A, ref LDA, JPVT, tau, (double*)workHandle.Pointer, ref lwork, ref info);
#else
                    mkl_dgeqp3 (ref M, ref N, A, ref LDA, JPVT, tau, (double*)workHandle.Pointer, ref lwork, (Double*)rWorkHandle.Pointer, ref info);
#endif
                } else {
                    throw new ArgumentException("Error in dgeqp3. Unable to determine required working set size.");
                }
            } catch (OutOfMemoryException e) {
                throw new ArgumentException($"Error in dgeqp3. Not enough memory!{lwork} double elements were required.", e);
            } finally {
                if (workHandle != null) {
                    free<double>(workHandle, 0);
                }
                if (rWorkHandle != null) {
                    free<Double>(rWorkHandle, 0);
                }
            }
        }
        #endregion HYCALPER LOOPEND GEQP3
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        
        public void  sgeqp3(int M, int N, float* A, int LDA, int* JPVT, float* tau, ref int info) {
            float work = 0;
            int lwork = -1;
            MemoryHandle workHandle = null;
            MemoryHandle rWorkHandle = null;
            try {
                #if !IS_COMPLEX
                mkl_sgeqp3(ref M, ref N, A, ref LDA, JPVT, tau, &work, ref lwork, ref info);

#else
                rWorkHandle = New<float>((ulong)(2 * N));
                mkl_sgeqp3 (ref M, ref N, A, ref LDA, JPVT, tau, &work, ref lwork, (float*)rWorkHandle.Pointer, ref info);
#endif

                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<float>((ulong)lwork);
                    #if !IS_COMPLEX
                    mkl_sgeqp3(ref M, ref N, A, ref LDA, JPVT, tau, (float*)workHandle.Pointer, ref lwork, ref info);
#else
                    mkl_sgeqp3 (ref M, ref N, A, ref LDA, JPVT, tau, (float*)workHandle.Pointer, ref lwork, (float*)rWorkHandle.Pointer, ref info);
#endif
                } else {
                    throw new ArgumentException("Error in sgeqp3. Unable to determine required working set size.");
                }
            } catch (OutOfMemoryException e) {
                throw new ArgumentException($"Error in sgeqp3. Not enough memory!{lwork} float elements were required.", e);
            } finally {
                if (workHandle != null) {
                    free<float>(workHandle, 0);
                }
                if (rWorkHandle != null) {
                    free<float>(rWorkHandle, 0);
                }
            }
        }
       
        
        public void  zgeqp3(int M, int N, complex* A, int LDA, int* JPVT, complex* tau, ref int info) {
            complex work = 0;
            int lwork = -1;
            MemoryHandle workHandle = null;
            MemoryHandle rWorkHandle = null;
            try {
                #if IS_COMPLEX
                mkl_zgeqp3(ref M, ref N, A, ref LDA, JPVT, tau, &work, ref lwork, ref info);

#else
                rWorkHandle = New<double>((ulong)(2 * N));
                mkl_zgeqp3 (ref M, ref N, A, ref LDA, JPVT, tau, &work, ref lwork, (double*)rWorkHandle.Pointer, ref info);
#endif

                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<complex>((ulong)lwork);
                    #if IS_COMPLEX
                    mkl_zgeqp3(ref M, ref N, A, ref LDA, JPVT, tau, (complex*)workHandle.Pointer, ref lwork, ref info);
#else
                    mkl_zgeqp3 (ref M, ref N, A, ref LDA, JPVT, tau, (complex*)workHandle.Pointer, ref lwork, (double*)rWorkHandle.Pointer, ref info);
#endif
                } else {
                    throw new ArgumentException("Error in zgeqp3. Unable to determine required working set size.");
                }
            } catch (OutOfMemoryException e) {
                throw new ArgumentException($"Error in zgeqp3. Not enough memory!{lwork} complex elements were required.", e);
            } finally {
                if (workHandle != null) {
                    free<complex>(workHandle, 0);
                }
                if (rWorkHandle != null) {
                    free<double>(rWorkHandle, 0);
                }
            }
        }
       
        
        public void  cgeqp3(int M, int N, fcomplex* A, int LDA, int* JPVT, fcomplex* tau, ref int info) {
            fcomplex work = 0;
            int lwork = -1;
            MemoryHandle workHandle = null;
            MemoryHandle rWorkHandle = null;
            try {
                #if IS_COMPLEX
                mkl_cgeqp3(ref M, ref N, A, ref LDA, JPVT, tau, &work, ref lwork, ref info);

#else
                rWorkHandle = New<float>((ulong)(2 * N));
                mkl_cgeqp3 (ref M, ref N, A, ref LDA, JPVT, tau, &work, ref lwork, (float*)rWorkHandle.Pointer, ref info);
#endif

                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<fcomplex>((ulong)lwork);
                    #if IS_COMPLEX
                    mkl_cgeqp3(ref M, ref N, A, ref LDA, JPVT, tau, (fcomplex*)workHandle.Pointer, ref lwork, ref info);
#else
                    mkl_cgeqp3 (ref M, ref N, A, ref LDA, JPVT, tau, (fcomplex*)workHandle.Pointer, ref lwork, (float*)rWorkHandle.Pointer, ref info);
#endif
                } else {
                    throw new ArgumentException("Error in cgeqp3. Unable to determine required working set size.");
                }
            } catch (OutOfMemoryException e) {
                throw new ArgumentException($"Error in cgeqp3. Not enough memory!{lwork} fcomplex elements were required.", e);
            } finally {
                if (workHandle != null) {
                    free<fcomplex>(workHandle, 0);
                }
                if (rWorkHandle != null) {
                    free<float>(rWorkHandle, 0);
                }
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

        #region 
        
        public void dtrtrs(char uplo, char transA, char diag, int N, int nrhs, double* A, int LDA, double* B, int LDB, ref int info) {
            mkl_dtrtrs(ref uplo, ref transA, ref diag, ref N, ref nrhs, A, ref LDA, B, ref LDB, ref info);
        }
        
        public void strtrs(char uplo, char transA, char diag, int N, int nrhs, float* A, int LDA, float* B, int LDB, ref int info) {
            mkl_strtrs(ref uplo, ref transA, ref diag, ref N, ref nrhs, A, ref LDA, B, ref LDB, ref info);
        }
        
        public void ctrtrs(char uplo, char transA, char diag, int N, int nrhs, fcomplex* A, int LDA, fcomplex* B, int LDB, ref int info) {
            mkl_ctrtrs(ref uplo, ref transA, ref diag, ref N, ref nrhs, A, ref LDA, B, ref LDB, ref info);
        }
        
        public void ztrtrs(char uplo, char transA, char diag, int N, int nrhs, complex* A, int LDA, complex* B, int LDB, ref int info) {
            mkl_ztrtrs(ref uplo, ref transA, ref diag, ref N, ref nrhs, A, ref LDA, B, ref LDB, ref info);
        }

        
        public void dgetrs(char trans, int N, int NRHS, double* A, int LDA, int* IPIV, double* B, int LDB, ref int info) {
            mkl_dgetrs(ref trans, ref N, ref NRHS, A, ref LDA, IPIV, B, ref LDB, ref info);
        }

        
        public void sgetrs(char trans, int N, int NRHS, float* A, int LDA, int* IPIV, float* B, int LDB, ref int info) {
            mkl_sgetrs(ref trans, ref N, ref NRHS, A, ref LDA, IPIV, B, ref LDB, ref info);
        }

        
        public void cgetrs(char trans, int N, int NRHS, fcomplex* A, int LDA, int* IPIV, fcomplex* B, int LDB, ref int info) {
            mkl_cgetrs(ref trans, ref N, ref NRHS, A, ref LDA, IPIV, B, ref LDB, ref info);
        }

        
        public void zgetrs(char trans, int N, int NRHS, complex* A, int LDA, int* IPIV, complex* B, int LDB, ref int info) {
            mkl_zgetrs(ref trans, ref N, ref NRHS, A, ref LDA, IPIV, B, ref LDB, ref info);
        }

        
        public void dpotrs(char uplo, int n, int nrhs, double* A, int lda, double* B, int ldb, ref int info) {
            mkl_dpotrs(ref uplo, ref n, ref nrhs, A, ref lda, B, ref ldb, ref info);
        }

        
        public void spotrs(char uplo, int n, int nrhs, float* A, int lda, float* B, int ldb, ref int info) {
            mkl_spotrs(ref uplo, ref n, ref nrhs, A, ref lda, B, ref ldb, ref info);
        }

        
        public void cpotrs(char uplo, int n, int nrhs, fcomplex* A, int lda, fcomplex* B, int ldb, ref int info) {
            mkl_cpotrs(ref uplo, ref n, ref nrhs, A, ref lda, B, ref ldb, ref info);
        }

        
        public void zpotrs(char uplo, int n, int nrhs, complex* A, int lda, complex* B, int ldb, ref int info) {
            mkl_zpotrs(ref uplo, ref n, ref nrhs, A, ref lda, B, ref ldb, ref info);
        }
        #endregion

        #region HYCALPER LOOPSTART ?gelsd 
        /*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        double
    </source>
    <destination>fcomplex</destination>
    <destination>complex</destination>
    <destination>float</destination>
</type>
<type>
    <source locate="here">
        Double
    </source>
    <destination>float</destination>
    <destination>double</destination>
    <destination>float</destination>
</type>
<type>
    <source locate="here">
        dgelsd
    </source>
    <destination>cgelsd </destination>
    <destination>zgelsd </destination>
    <destination>sgelsd </destination>
</type>
<type>
    <source locate="nextline">
        cmplxRwork
    </source>
    <destination>#if IS_COMPLEX</destination>
    <destination>#if IS_COMPLEX</destination>
    <destination>#if !IS_COMPLEX</destination>
</type>
</hycalper>
*/
        
        public void dgelsd(int m, int n, int nrhs, double* A, int lda, double* B, int ldb, Double* S, Double RCond, ref int rank, ref int info) {
            double work = 0;
            int iwork = 1;
            int lwork = -1;
            MemoryHandle iWorkHandle = null;
            MemoryHandle workHandle = null;
            MemoryHandle rWorkHandle = null;
            try {
                /*!HC:cmplxRwork*/
#if !IS_COMPLEX
                mkl_dgelsd(ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S, ref RCond, ref rank, &work, ref lwork, &iwork, ref info);
#else
                Double rwork = 0; 
                mkl_dgelsd (ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S,ref RCond, ref rank, &work, ref lwork, &rwork, &iwork, ref info);
#endif
                if (info != 0)
                    throw new ArgumentException("dgelsd: invalid parameter: #" + (-info).ToString());
                lwork = (int)work;
                if (lwork <= 0)
                    throw new ArgumentException("dgelsd: unknown error determining working size lwork.");
                iWorkHandle = New<int>((ulong)(lwork * 1000));
                workHandle = New<double>((ulong)lwork);

                /*!HC:cmplxRwork*/
#if !IS_COMPLEX
                mkl_dgelsd(ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S, ref RCond, ref rank, (double*)workHandle.Pointer, ref lwork, (int*)iWorkHandle.Pointer, ref info);
#else
                rWorkHandle = New<Double>((ulong)rwork);
                mkl_dgelsd (ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S,ref RCond, ref rank, (double*)workHandle.Pointer, ref lwork, (Double*)rWorkHandle.Pointer, (int*)iWorkHandle.Pointer, ref info);
#endif
            } finally {
                if (iWorkHandle != null) free<int>(iWorkHandle, 0);
                if (workHandle != null) free<double>(workHandle, 0);
                if (rWorkHandle != null) free<Double>(rWorkHandle, 0);
            }
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        
        public void sgelsd (int m, int n, int nrhs, float* A, int lda, float* B, int ldb, float* S, float RCond, ref int rank, ref int info) {
            float work = 0;
            int iwork = 1;
            int lwork = -1;
            MemoryHandle iWorkHandle = null;
            MemoryHandle workHandle = null;
            MemoryHandle rWorkHandle = null;
            try {
                #if !IS_COMPLEX
                mkl_sgelsd (ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S, ref RCond, ref rank, &work, ref lwork, &iwork, ref info);
#else
                float rwork = 0; 
                mkl_sgelsd  (ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S,ref RCond, ref rank, &work, ref lwork, &rwork, &iwork, ref info);
#endif
                if (info != 0)
                    throw new ArgumentException("sgelsd : invalid parameter: #" + (-info).ToString());
                lwork = (int)work;
                if (lwork <= 0)
                    throw new ArgumentException("sgelsd : unknown error determining working size lwork.");
                iWorkHandle = New<int>((ulong)(lwork * 1000));
                workHandle = New<float>((ulong)lwork);

                #if !IS_COMPLEX
                mkl_sgelsd (ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S, ref RCond, ref rank, (float*)workHandle.Pointer, ref lwork, (int*)iWorkHandle.Pointer, ref info);
#else
                rWorkHandle = New<float>((ulong)rwork);
                mkl_sgelsd  (ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S,ref RCond, ref rank, (float*)workHandle.Pointer, ref lwork, (float*)rWorkHandle.Pointer, (int*)iWorkHandle.Pointer, ref info);
#endif
            } finally {
                if (iWorkHandle != null) free<int>(iWorkHandle, 0);
                if (workHandle != null) free<float>(workHandle, 0);
                if (rWorkHandle != null) free<float>(rWorkHandle, 0);
            }
        }
       
        
        public void zgelsd (int m, int n, int nrhs, complex* A, int lda, complex* B, int ldb, double* S, double RCond, ref int rank, ref int info) {
            complex work = 0;
            int iwork = 1;
            int lwork = -1;
            MemoryHandle iWorkHandle = null;
            MemoryHandle workHandle = null;
            MemoryHandle rWorkHandle = null;
            try {
                #if IS_COMPLEX
                mkl_zgelsd (ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S, ref RCond, ref rank, &work, ref lwork, &iwork, ref info);
#else
                double rwork = 0; 
                mkl_zgelsd  (ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S,ref RCond, ref rank, &work, ref lwork, &rwork, &iwork, ref info);
#endif
                if (info != 0)
                    throw new ArgumentException("zgelsd : invalid parameter: #" + (-info).ToString());
                lwork = (int)work;
                if (lwork <= 0)
                    throw new ArgumentException("zgelsd : unknown error determining working size lwork.");
                iWorkHandle = New<int>((ulong)(lwork * 1000));
                workHandle = New<complex>((ulong)lwork);

                #if IS_COMPLEX
                mkl_zgelsd (ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S, ref RCond, ref rank, (complex*)workHandle.Pointer, ref lwork, (int*)iWorkHandle.Pointer, ref info);
#else
                rWorkHandle = New<double>((ulong)rwork);
                mkl_zgelsd  (ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S,ref RCond, ref rank, (complex*)workHandle.Pointer, ref lwork, (double*)rWorkHandle.Pointer, (int*)iWorkHandle.Pointer, ref info);
#endif
            } finally {
                if (iWorkHandle != null) free<int>(iWorkHandle, 0);
                if (workHandle != null) free<complex>(workHandle, 0);
                if (rWorkHandle != null) free<double>(rWorkHandle, 0);
            }
        }
       
        
        public void cgelsd (int m, int n, int nrhs, fcomplex* A, int lda, fcomplex* B, int ldb, float* S, float RCond, ref int rank, ref int info) {
            fcomplex work = 0;
            int iwork = 1;
            int lwork = -1;
            MemoryHandle iWorkHandle = null;
            MemoryHandle workHandle = null;
            MemoryHandle rWorkHandle = null;
            try {
                #if IS_COMPLEX
                mkl_cgelsd (ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S, ref RCond, ref rank, &work, ref lwork, &iwork, ref info);
#else
                float rwork = 0; 
                mkl_cgelsd  (ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S,ref RCond, ref rank, &work, ref lwork, &rwork, &iwork, ref info);
#endif
                if (info != 0)
                    throw new ArgumentException("cgelsd : invalid parameter: #" + (-info).ToString());
                lwork = (int)work;
                if (lwork <= 0)
                    throw new ArgumentException("cgelsd : unknown error determining working size lwork.");
                iWorkHandle = New<int>((ulong)(lwork * 1000));
                workHandle = New<fcomplex>((ulong)lwork);

                #if IS_COMPLEX
                mkl_cgelsd (ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S, ref RCond, ref rank, (fcomplex*)workHandle.Pointer, ref lwork, (int*)iWorkHandle.Pointer, ref info);
#else
                rWorkHandle = New<float>((ulong)rwork);
                mkl_cgelsd  (ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S,ref RCond, ref rank, (fcomplex*)workHandle.Pointer, ref lwork, (float*)rWorkHandle.Pointer, (int*)iWorkHandle.Pointer, ref info);
#endif
            } finally {
                if (iWorkHandle != null) free<int>(iWorkHandle, 0);
                if (workHandle != null) free<fcomplex>(workHandle, 0);
                if (rWorkHandle != null) free<float>(rWorkHandle, 0);
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

        #region HYCALPER LOOPSTART ?gelsd 
        /*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        double
    </source>
    <destination>float</destination>
</type>
<type>
    <source locate="here">
        Double
    </source>
    <destination>float</destination>
</type>
<type>
    <source locate="here">
        dge
    </source>
    <destination>sge</destination>
</type>
<type>
    <source locate="here">
        dsy
    </source>
    <destination>ssy</destination>
</type>
</hycalper>
*/
        
        public void dgelsy(int m, int n, int nrhs, double* A, int lda, double* B, int ldb, int* JPVT0, double RCond, ref int rank, ref int info) {
            int lwork = -1;
            double work = 0;
            mkl_dgelsy(ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, JPVT0, ref RCond, ref rank, &work, ref lwork, ref info);
            if (info != 0)
                throw new ArgumentException("dgelsy: unable to determine optimal block size.");
            lwork = (int)work;
            var workHandle = New<double>((ulong)lwork);
            mkl_dgelsy(ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, JPVT0, ref RCond, ref rank, (double*)workHandle.Pointer, ref lwork, ref info);
            free<double>(workHandle, 0);
        }
        
        public void dgeevx(char balance, char jobvl, char jobvr, char sense, int n, double* A, int lda, double* wr, double* wi, double* vl, int ldvl, double* vr, int ldvr, ref int ilo, ref int ihi, double* scale, ref double abnrm, double* rconde, double* rcondv, ref int info) {
            int lwork = -1;
            double work = 0;
            var iworkHandle = New<int>((ulong)(2 * n - 2));
            mkl_dgeevx(ref balance, ref jobvl, ref jobvr, ref sense, ref n, A, ref lda, wr, wi, vl, ref ldvl, vr, ref ldvr, ref ilo, ref ihi, scale, ref abnrm, rconde, rcondv, &work, ref lwork, (int*)iworkHandle.Pointer, ref info);
            if (info != 0)
                throw new ArgumentException("dgeevx: unable to determine working set size. (info: " + info + ")");
            lwork = (int)work;
            var workHandle = New<double>((ulong)lwork);
            mkl_dgeevx(ref balance, ref jobvl, ref jobvr, ref sense, ref n, A, ref lda, wr, wi, vl, ref ldvl, vr, ref ldvr, ref ilo, ref ihi, scale, ref abnrm, rconde, rcondv, (double*)workHandle.Pointer, ref lwork, (int*)iworkHandle.Pointer, ref info);
            free<double>(workHandle, 0);
            free<int>(iworkHandle, 0);
        }
        //?[he/sy]gv - generalized eigenproblem
        
        public void dsygv(int itype, char jobz, char uplo, int n, double* A, int lda, double* B, int ldb, double* w, ref int info) {
            int lwork = -1;
            double work = 0;
            mkl_dsygv(ref itype, ref jobz, ref uplo, ref n, A, ref lda, B, ref ldb, w, &work, ref lwork, ref info);
            if (info != 0 || work <= 0.0)
                throw new ArgumentException("dsygv: unable to determine working set size. (info: " + info + ")");

            lwork = (int)work;
            var workHandle = New<double>((ulong)lwork);
            mkl_dsygv(ref itype, ref jobz, ref uplo, ref n, A, ref lda, B, ref ldb, w, (double*)workHandle.Pointer, ref lwork, ref info);
            free<double>(workHandle, 0);
        }
        
        public void dsyevr(char jobz, char range, char uplo, int n, double* A, int lda, double vl, double vu, int il, int iu, double abstol, ref int m, double* w, double* z, int ldz, int* isuppz, ref int info) {
            int liwork = -1;
            int lwork = -1;
            double work = 0;
            int iwork = 0;
            //byte jz = (byte)jobz,rn = (byte) range,ul = (byte)uplo; 
            mkl_dsyevr(ref jobz, ref range, ref uplo, ref n, A, ref lda, ref vl, ref vu, ref il, ref iu, ref abstol, ref m, w, z, ref ldz, isuppz, &work, ref lwork, &iwork, ref liwork, ref info);
            if (info != 0) {
                throw new ArgumentException("dsyevr: unable to determine working set size. (info: " + info + ")");
            }
            lwork = (int)work;
            var workHandle = New<double>((ulong)lwork);
            liwork = (int)iwork;
            var iworkHandle = New<int>((ulong)liwork);
            mkl_dsyevr(ref jobz, ref range, ref uplo, ref n, A, ref lda, ref vl, ref vu, ref il, ref iu, ref abstol, ref m, w, z, ref ldz, isuppz, (double*)workHandle.Pointer, ref lwork, (int*)iworkHandle.Pointer, ref liwork, ref info);
            free<int>(iworkHandle, 0);
            free<double>(workHandle, 0);
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        
        public void sgelsy(int m, int n, int nrhs, float* A, int lda, float* B, int ldb, int* JPVT0, float RCond, ref int rank, ref int info) {
            int lwork = -1;
            float work = 0;
            mkl_sgelsy(ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, JPVT0, ref RCond, ref rank, &work, ref lwork, ref info);
            if (info != 0)
                throw new ArgumentException("sgelsy: unable to determine optimal block size.");
            lwork = (int)work;
            var workHandle = New<float>((ulong)lwork);
            mkl_sgelsy(ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, JPVT0, ref RCond, ref rank, (float*)workHandle.Pointer, ref lwork, ref info);
            free<float>(workHandle, 0);
        }
        
        public void sgeevx(char balance, char jobvl, char jobvr, char sense, int n, float* A, int lda, float* wr, float* wi, float* vl, int ldvl, float* vr, int ldvr, ref int ilo, ref int ihi, float* scale, ref float abnrm, float* rconde, float* rcondv, ref int info) {
            int lwork = -1;
            float work = 0;
            var iworkHandle = New<int>((ulong)(2 * n - 2));
            mkl_sgeevx(ref balance, ref jobvl, ref jobvr, ref sense, ref n, A, ref lda, wr, wi, vl, ref ldvl, vr, ref ldvr, ref ilo, ref ihi, scale, ref abnrm, rconde, rcondv, &work, ref lwork, (int*)iworkHandle.Pointer, ref info);
            if (info != 0)
                throw new ArgumentException("sgeevx: unable to determine working set size. (info: " + info + ")");
            lwork = (int)work;
            var workHandle = New<float>((ulong)lwork);
            mkl_sgeevx(ref balance, ref jobvl, ref jobvr, ref sense, ref n, A, ref lda, wr, wi, vl, ref ldvl, vr, ref ldvr, ref ilo, ref ihi, scale, ref abnrm, rconde, rcondv, (float*)workHandle.Pointer, ref lwork, (int*)iworkHandle.Pointer, ref info);
            free<float>(workHandle, 0);
            free<int>(iworkHandle, 0);
        }
        //?[he/sy]gv - generalized eigenproblem
        
        public void ssygv(int itype, char jobz, char uplo, int n, float* A, int lda, float* B, int ldb, float* w, ref int info) {
            int lwork = -1;
            float work = 0;
            mkl_ssygv(ref itype, ref jobz, ref uplo, ref n, A, ref lda, B, ref ldb, w, &work, ref lwork, ref info);
            if (info != 0 || work <= 0.0)
                throw new ArgumentException("ssygv: unable to determine working set size. (info: " + info + ")");

            lwork = (int)work;
            var workHandle = New<float>((ulong)lwork);
            mkl_ssygv(ref itype, ref jobz, ref uplo, ref n, A, ref lda, B, ref ldb, w, (float*)workHandle.Pointer, ref lwork, ref info);
            free<float>(workHandle, 0);
        }
        
        public void ssyevr(char jobz, char range, char uplo, int n, float* A, int lda, float vl, float vu, int il, int iu, float abstol, ref int m, float* w, float* z, int ldz, int* isuppz, ref int info) {
            int liwork = -1;
            int lwork = -1;
            float work = 0;
            int iwork = 0;
            //byte jz = (byte)jobz,rn = (byte) range,ul = (byte)uplo; 
            mkl_ssyevr(ref jobz, ref range, ref uplo, ref n, A, ref lda, ref vl, ref vu, ref il, ref iu, ref abstol, ref m, w, z, ref ldz, isuppz, &work, ref lwork, &iwork, ref liwork, ref info);
            if (info != 0) {
                throw new ArgumentException("ssyevr: unable to determine working set size. (info: " + info + ")");
            }
            lwork = (int)work;
            var workHandle = New<float>((ulong)lwork);
            liwork = (int)iwork;
            var iworkHandle = New<int>((ulong)liwork);
            mkl_ssyevr(ref jobz, ref range, ref uplo, ref n, A, ref lda, ref vl, ref vu, ref il, ref iu, ref abstol, ref m, w, z, ref ldz, isuppz, (float*)workHandle.Pointer, ref lwork, (int*)iworkHandle.Pointer, ref liwork, ref info);
            free<int>(iworkHandle, 0);
            free<float>(workHandle, 0);
        }

#endregion HYCALPER AUTO GENERATED CODE

        #region HYCALPER LOOPSTART
        /*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        complex
    </source>
    <destination>fcomplex</destination>
</type>
<type>
    <source locate="here">
        double
    </source>
    <destination>float</destination>
</type>
<type>
    <source locate="here">
        zge
    </source>
    <destination>cge</destination>
</type>
<type>
    <source locate="here">
        zhe
    </source>
    <destination>che</destination>
</type>
</hycalper>
*/
        
        public void zgelsy(int m, int n, int nrhs, complex* A, int lda, complex* B, int ldb, int* JPVT0, double RCond, ref int rank, ref int info) {
            int lwork = -1;
            complex work = 0;
            double rwork = 0;
            mkl_zgelsy(ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, JPVT0, ref RCond, ref rank, &work, ref lwork, &rwork, ref info);
            if (info != 0)
                throw new ArgumentException($"zgelsy: unable to determine working set size. (info: {info})");
            lwork = (int)work;
            var workHandle = New<complex>((ulong)lwork);
            var rworkHandle = New<double>((ulong)lwork);
            mkl_zgelsy(ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, JPVT0, ref RCond, ref rank, (complex*)workHandle.Pointer, ref lwork, (double*)rworkHandle.Pointer, ref info);
            free<complex>(workHandle, 0);
            free<double>(rworkHandle, 0);
        }
        
        public void zgeevx(char balance, char jobvl, char jobvr, char sense, int n, complex* A, int lda, complex* w, complex* vl, int ldvl, complex* vr, int ldvr, ref int ilo, ref int ihi, double* scale, ref double abnrm, double* rconde, double* rcondv, ref int info) {
            complex work = 0;
            double rwork = 0;
            int lwork = -1;
            mkl_zgeevx(ref balance, ref jobvl, ref jobvr, ref sense, ref n, A, ref lda, w, vl, ref ldvl, vr, ref ldvr, ref ilo, ref ihi, scale, ref abnrm, rconde, rcondv, &work, ref lwork, &rwork, ref info);
            if (info != 0)
                throw new ArgumentException($"zgeevx: unable to determine working set size. (info: {info})");
            lwork = (int)work;
            var workHandle = New<complex>((ulong)lwork);
            var rworkHandle = New<double>((ulong)Math.Max(1, 2 * n));
            mkl_zgeevx(ref balance, ref jobvl, ref jobvr, ref sense, ref n, A, ref lda, w, vl, ref ldvl, vr, ref ldvr, ref ilo, ref ihi, scale, ref abnrm, rconde, rcondv, (complex*)workHandle.Pointer, ref lwork, (double*)rworkHandle.Pointer, ref info);
            free<double>(rworkHandle, 0);
            free<complex>(workHandle, 0);
        }
        
        public void zheevr(char jobz, char range, char uplo, int n, complex* A, int lda, double vl, double vu, int il, int iu, double abstol, ref int m, double* w, complex* z, int ldz, int* isuppz, ref int info) {
            complex work = 0;
            double rwork = 0;
            int iwork = 0;
            int lrwork = -1, liwork = -1, lwork = -1;
            mkl_zheevr(ref jobz, ref range, ref uplo, ref n, A, ref lda, ref vl, ref vu, ref il, ref iu, ref abstol, ref m, w, z, ref ldz, isuppz, &work, ref lwork, &rwork, ref lrwork, &iwork, ref liwork, ref info);
            if (info != 0) {
                throw new ArgumentException($"zsyevr: unable to determine working set size. (info: {info})");
            }
            lrwork = (int)rwork;
            var rworkHandle = New<double>((ulong)lrwork);
            lwork = (int)work;
            var workHandle = New<complex>((ulong)lwork);
            liwork = (int)iwork;
            var iworkHandle = New<int>((ulong)liwork);
            mkl_zheevr(ref jobz, ref range, ref uplo, ref n, A, ref lda, ref vl, ref vu, ref il, ref iu, ref abstol, ref m, w, z, ref ldz, isuppz, (complex*)workHandle.Pointer, ref lwork, (double*)rworkHandle.Pointer, ref lrwork, (int*)iworkHandle.Pointer, ref liwork, ref info);
            free<int>(iworkHandle, 0);
            free<complex>(workHandle, 0);
            free<double>(rworkHandle, 0);
        }
        
        public void zhegv(int itype, char jobz, char uplo, int n, complex* A, int lda, complex* B, int ldb, double* w, ref int info) {
            complex work = 0;
            double rwork = 0;
            int lwork = -1;
            mkl_zhegv(ref itype, ref jobz, ref uplo, ref n, A, ref lda, B, ref ldb, w, &work, ref lwork, &rwork, ref info);
            if (info != 0 || work <= 0.0)
                throw new ArgumentException($"zhegv: unable to determine working set size. (info: {info})");

            // create temporary array(s)
            lwork = (int)work;
            var workHandle = New<complex>((ulong)lwork);
            var rworkHandle = New<double>((ulong)Math.Max(1, 3 * n - 2));
            mkl_zhegv(ref itype, ref jobz, ref uplo, ref n, A, ref lda, B, ref ldb, w, (complex*)workHandle.Pointer, ref lwork, (double*)rworkHandle.Pointer, ref info);
            free<double>(rworkHandle, 0);
            free<complex>(workHandle, 0);
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        
        public void cgelsy(int m, int n, int nrhs, fcomplex* A, int lda, fcomplex* B, int ldb, int* JPVT0, float RCond, ref int rank, ref int info) {
            int lwork = -1;
            fcomplex work = 0;
            float rwork = 0;
            mkl_cgelsy(ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, JPVT0, ref RCond, ref rank, &work, ref lwork, &rwork, ref info);
            if (info != 0)
                throw new ArgumentException($"cgelsy: unable to determine working set size. (info: {info})");
            lwork = (int)work;
            var workHandle = New<fcomplex>((ulong)lwork);
            var rworkHandle = New<float>((ulong)lwork);
            mkl_cgelsy(ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, JPVT0, ref RCond, ref rank, (fcomplex*)workHandle.Pointer, ref lwork, (float*)rworkHandle.Pointer, ref info);
            free<fcomplex>(workHandle, 0);
            free<float>(rworkHandle, 0);
        }
        
        public void cgeevx(char balance, char jobvl, char jobvr, char sense, int n, fcomplex* A, int lda, fcomplex* w, fcomplex* vl, int ldvl, fcomplex* vr, int ldvr, ref int ilo, ref int ihi, float* scale, ref float abnrm, float* rconde, float* rcondv, ref int info) {
            fcomplex work = 0;
            float rwork = 0;
            int lwork = -1;
            mkl_cgeevx(ref balance, ref jobvl, ref jobvr, ref sense, ref n, A, ref lda, w, vl, ref ldvl, vr, ref ldvr, ref ilo, ref ihi, scale, ref abnrm, rconde, rcondv, &work, ref lwork, &rwork, ref info);
            if (info != 0)
                throw new ArgumentException($"cgeevx: unable to determine working set size. (info: {info})");
            lwork = (int)work;
            var workHandle = New<fcomplex>((ulong)lwork);
            var rworkHandle = New<float>((ulong)Math.Max(1, 2 * n));
            mkl_cgeevx(ref balance, ref jobvl, ref jobvr, ref sense, ref n, A, ref lda, w, vl, ref ldvl, vr, ref ldvr, ref ilo, ref ihi, scale, ref abnrm, rconde, rcondv, (fcomplex*)workHandle.Pointer, ref lwork, (float*)rworkHandle.Pointer, ref info);
            free<float>(rworkHandle, 0);
            free<fcomplex>(workHandle, 0);
        }
        
        public void cheevr(char jobz, char range, char uplo, int n, fcomplex* A, int lda, float vl, float vu, int il, int iu, float abstol, ref int m, float* w, fcomplex* z, int ldz, int* isuppz, ref int info) {
            fcomplex work = 0;
            float rwork = 0;
            int iwork = 0;
            int lrwork = -1, liwork = -1, lwork = -1;
            mkl_cheevr(ref jobz, ref range, ref uplo, ref n, A, ref lda, ref vl, ref vu, ref il, ref iu, ref abstol, ref m, w, z, ref ldz, isuppz, &work, ref lwork, &rwork, ref lrwork, &iwork, ref liwork, ref info);
            if (info != 0) {
                throw new ArgumentException($"zsyevr: unable to determine working set size. (info: {info})");
            }
            lrwork = (int)rwork;
            var rworkHandle = New<float>((ulong)lrwork);
            lwork = (int)work;
            var workHandle = New<fcomplex>((ulong)lwork);
            liwork = (int)iwork;
            var iworkHandle = New<int>((ulong)liwork);
            mkl_cheevr(ref jobz, ref range, ref uplo, ref n, A, ref lda, ref vl, ref vu, ref il, ref iu, ref abstol, ref m, w, z, ref ldz, isuppz, (fcomplex*)workHandle.Pointer, ref lwork, (float*)rworkHandle.Pointer, ref lrwork, (int*)iworkHandle.Pointer, ref liwork, ref info);
            free<int>(iworkHandle, 0);
            free<fcomplex>(workHandle, 0);
            free<float>(rworkHandle, 0);
        }
        
        public void chegv(int itype, char jobz, char uplo, int n, fcomplex* A, int lda, fcomplex* B, int ldb, float* w, ref int info) {
            fcomplex work = 0;
            float rwork = 0;
            int lwork = -1;
            mkl_chegv(ref itype, ref jobz, ref uplo, ref n, A, ref lda, B, ref ldb, w, &work, ref lwork, &rwork, ref info);
            if (info != 0 || work <= 0.0)
                throw new ArgumentException($"chegv: unable to determine working set size. (info: {info})");

            // create temporary array(s)
            lwork = (int)work;
            var workHandle = New<fcomplex>((ulong)lwork);
            var rworkHandle = New<float>((ulong)Math.Max(1, 3 * n - 2));
            mkl_chegv(ref itype, ref jobz, ref uplo, ref n, A, ref lda, B, ref ldb, w, (fcomplex*)workHandle.Pointer, ref lwork, (float*)rworkHandle.Pointer, ref info);
            free<float>(rworkHandle, 0);
            free<fcomplex>(workHandle, 0);
        }

#endregion HYCALPER AUTO GENERATED CODE

        #endregion
    }
}
