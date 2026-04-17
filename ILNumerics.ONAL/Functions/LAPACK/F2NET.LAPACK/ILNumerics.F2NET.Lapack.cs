//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
#pragma warning disable 1570, 1591
using ILNumerics.Core.MemoryLayer;
using ILNumerics.Core.Native;
using System;
using System.Security;
using static ILNumerics.Core.Functions.Builtin.MathInternal;

namespace ILNumerics.F2NET {
    /// <summary>
    /// Lapack interface implementation, specialized for the official netlib.org Lapack package, as a direct 1:1 translation utilizing ILNumerics.F2NET. 
    /// </summary>
    /// 
    public unsafe class ManagedLAPACK : ILapack {

        #region attributes
#pragma warning disable CS0169
        string m_version;
#pragma warning restore CS0168
        #endregion

        public ManagedLAPACK() {
            Init();
        }
        public static void Init() {
        }

        #region ILAPACK INTERFACE 

        #region service functions
        private int ILAENV(ref int ispec, string name, string opts, ref int n1, ref int n2, ref int n3, ref int n4) {
            return LAPACK._4mvd6e4d(ref ispec, name, opts, ref n1, ref n2, ref n3, ref n4);

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

            LAPACK._5nsxi69c(TransA, TransB, ref M, ref N, ref K, ref alpha, A, ref lda, B, ref ldb, ref beta, C, ref ldc);

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

            LAPACK._b8wa9454(TransA, TransB, ref M, ref N, ref K, ref alpha, A, ref lda, B, ref ldb, ref beta, C, ref ldc);

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
            LAPACK._5p0w9905(TransA, TransB, ref M, ref N, ref K, ref alpha, A, ref lda, B, ref ldb, ref beta, C, ref ldc);

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
        /// <param name="beta">multiplication factor for matrix B</param>
        /// <param name="C">pointer to predefined double array C of neccessary length</param>
        /// <param name="ldc">distance between first elements of each column for column based orientation or 
        /// distance between first elements of each row for row based orientation for matrix C</param>
        /// <remarks>All parameters except C are readonly. Only elements of matrix C will be altered. C must be a predefined 
        /// continous double array of size MxN</remarks>
        public void zgemm(char TransA, char TransB, int M, int N, int K, complex alpha, complex* A, int lda, complex* B, int ldb, complex beta, complex* C, int ldc) {
            LAPACK._xos1d1er(TransA, TransB, ref M, ref N, ref K, ref alpha, A, ref lda, B, ref ldb, ref beta, C, ref ldc);

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
        _ybtak27u
    </source>
    <destination>_q780vlia</destination>
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

                LAPACK._ybtak27u(jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, &work, ref lwork, (int*)iWorkHandle.Pointer, ref info);

                if (work > 0) {
                    workHandle = New<double>((ulong)work, 0);
                    lwork = (int)work;

                    LAPACK._ybtak27u(jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, (double*)workHandle.Pointer, ref lwork, (int*)iWorkHandle.Pointer, ref info);

                }


            } catch (OutOfMemoryException) {
                dgesvd(jobz, m, n, a, lda, s, u, ldu, vt, ldvt, ref info);
            } catch (Exception e) {
                var msg = $"Error in dgesdd: {e}";
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

                LAPACK._q780vlia(jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, &work, ref lwork, (int*)iWorkHandle.Pointer, ref info);

                if (work > 0) {
                    workHandle = New<float>((ulong)work, 0);
                    lwork = (int)work;

                    LAPACK._q780vlia(jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, (float*)workHandle.Pointer, ref lwork, (int*)iWorkHandle.Pointer, ref info);

                }


            } catch (OutOfMemoryException) {
                sgesvd(jobz, m, n, a, lda, s, u, ldu, vt, ldvt, ref info);
            } catch (Exception e) {
                var msg = $"Error in sgesdd: {e}";
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
                _6sr3m9vr
            </source>
            <destination>_1nuw98os</destination>
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

                LAPACK._6sr3m9vr(jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, &work, ref lwork, (double*)rworkHandle.Pointer, (int*)iworkHandle.Pointer, ref info);

                if (work != 0) {
                    workHandle = New<complex>((ulong)work.real);
                    lwork = (int)work.real;
                    LAPACK._6sr3m9vr(jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, (complex*)workHandle.Pointer, ref lwork, (double*)rworkHandle.Pointer, (int*)iworkHandle.Pointer, ref info);

                }
            } catch (OutOfMemoryException) {
                zgesvd(jobz, m, n, a, lda, s, u, ldu, vt, ldvt, ref info);
            } catch (Exception e) {
                throw new ArgumentException("Error in zgesdd. The inner exception may contain further details.", e);
            } finally {
                if (workHandle != null) {
                    free<double>(workHandle, 0);
                }
                if (iworkHandle != null) {
                    free<int>(iworkHandle, 0);
                }
                if (rworkHandle != null) {
                    free<int>(rworkHandle, 0);
                }
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

                LAPACK._1nuw98os(jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, &work, ref lwork, (float*)rworkHandle.Pointer, (int*)iworkHandle.Pointer, ref info);

                if (work != 0) {
                    workHandle = New<fcomplex>((ulong)work.real);
                    lwork = (int)work.real;
                    LAPACK._1nuw98os(jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, (fcomplex*)workHandle.Pointer, ref lwork, (float*)rworkHandle.Pointer, (int*)iworkHandle.Pointer, ref info);

                }
            } catch (OutOfMemoryException) {
                cgesvd(jobz, m, n, a, lda, s, u, ldu, vt, ldvt, ref info);
            } catch (Exception e) {
                throw new ArgumentException("Error in cgesdd. The inner exception may contain further details.", e);
            } finally {
                if (workHandle != null) {
                    free<float>(workHandle, 0);
                }
                if (iworkHandle != null) {
                    free<int>(iworkHandle, 0);
                }
                if (rworkHandle != null) {
                    free<int>(rworkHandle, 0);
                }
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

        #region GESVD

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
            LAPACK._honk69hl(jobz, jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, &work, ref lwork, ref info);

            if (work != 0) {
                var workHandle = New<double>((ulong)work);
                lwork = (int)work;
                LAPACK._honk69hl(jobz, jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, (double*)workHandle.Pointer, ref lwork, ref info);

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
            LAPACK._sxkpfn2l(jobz, jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, &work, ref lwork, ref info);

            if (work != 0) {
                var workHandle = New<float>((ulong)work);
                lwork = (int)work;
                LAPACK._sxkpfn2l(jobz, jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, (float*)workHandle.Pointer, ref lwork, ref info);

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
            var rworkHandle = New<int>((ulong)((m < n) ? m : n) * 8);
            LAPACK._lz14mjxc(jobz, jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, &work, ref lwork, (double*)rworkHandle.Pointer, ref info);

            if (work != 0) {
                var workHandle = New<complex>((ulong)work);
                lwork = (int)work;
                LAPACK._lz14mjxc(jobz, jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, (complex*)workHandle.Pointer, ref lwork, (double*)rworkHandle.Pointer, ref info);

                if (workHandle != null) {
                    free<complex>(workHandle, 0);
                }
            } else {
                throw new ArgumentException("Error in zgesvd: unable to determine working set size.");
            }
            if (rworkHandle != null) {
                free<int>(rworkHandle, 0);
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
            var rworkHandle = New<int>((ulong)((m < n) ? m : n) * 8);
            LAPACK._iq3tzxqu(jobz, jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, &work, ref lwork, (float*)rworkHandle.Pointer, ref info);

            if (work != 0) {
                var workHandle = New<fcomplex>((ulong)work);
                lwork = (int)work;
                LAPACK._iq3tzxqu(jobz, jobz, ref m, ref n, a, ref lda, s, u, ref ldu, vt, ref ldvt, (fcomplex*)workHandle.Pointer, ref lwork, (float*)rworkHandle.Pointer, ref info);

                if (workHandle != null) {
                    free<fcomplex>(workHandle, 0);
                }
            } else {
                throw new ArgumentException("Error in cgesvd: unable to determine working set size.");
            }
            if (rworkHandle != null) {
                free<int>(rworkHandle, 0);
            }

        }

        #endregion HYCALPER AUTO GENERATED CODE

        #region ?potrf ...
        public void dpotrf(char uplo, int n, double* A, int lda, ref int info) {
            LAPACK._yy3ifwba(uplo, ref n, A, ref lda, ref info);

        }
        public void spotrf(char uplo, int n, float* A, int lda, ref int info) {
            LAPACK._d0eywe7s(uplo, ref n, A, ref lda, ref info);

        }
        public void cpotrf(char uplo, int n, fcomplex* A, int lda, ref int info) {
            LAPACK._x1p8h8gy(uplo, ref n, A, ref lda, ref info);

        }
        public void zpotrf(char uplo, int n, complex* A, int lda, ref int info) {
            LAPACK._30im5i83(uplo, ref n, A, ref lda, ref info);

        }
        public void dpotri(char uplo, int n, double* A, int lda, ref int info) {
            LAPACK._2c8px66x(uplo, ref n, A, ref lda, ref info);

        }
        public void spotri(char uplo, int n, float* A, int lda, ref int info) {
            LAPACK._1pkpnxoc(uplo, ref n, A, ref lda, ref info);


        }
        public void cpotri(char uplo, int n, fcomplex* A, int lda, ref int info) {

            LAPACK._phzdjihf(uplo, ref n, A, ref lda, ref info);

        }
        public void zpotri(char uplo, int n, complex* A, int lda, ref int info) {
            LAPACK._0tb3k3c6(uplo, ref n, A, ref lda, ref info);

        }
        public void dgetrf(int M, int N, double* A, int LDA, int* IPIV, ref int info) {
            LAPACK._9b5ufle9(ref M, ref N, A, ref LDA, IPIV, ref info);

        }
        public void sgetrf(int M, int N, float* A, int LDA, int* IPIV, ref int info) {

            LAPACK._sprhud0f(ref M, ref N, A, ref LDA, IPIV, ref info);

        }
        public void cgetrf(int M, int N, fcomplex* A, int LDA, int* IPIV, ref int info) {
            LAPACK._ianoe0kp(ref M, ref N, A, ref LDA, IPIV, ref info);

        }
        public void zgetrf(int M, int N, complex* A, int LDA, int* IPIV, ref int info) {
            LAPACK._h9rrrkce(ref M, ref N, A, ref LDA, IPIV, ref info);

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
<type>
    <source locate="here">
        _4gq4e8s0
    </source>
    <destination>_4fupxqd8</destination>
    <destination>_3umidwpd</destination>
    <destination>_s7pkoikw</destination>
</type>
</hycalper>
*/
        public void dgetri(int N, double* A, int LDA, int* IPIV, ref int info) {
            double work = 1;
            int lwork = -1;
            MemoryHandle workHandle = null;
            try {

                LAPACK._4gq4e8s0(ref N, A, ref LDA, IPIV, &work, ref lwork, ref info);

                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<double>((ulong)lwork);

                    LAPACK._4gq4e8s0(ref N, A, ref LDA, IPIV, (double*)workHandle.Pointer, ref lwork, ref info);

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

                LAPACK._s7pkoikw(ref N, A, ref LDA, IPIV, &work, ref lwork, ref info);

                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<float>((ulong)lwork);

                    LAPACK._s7pkoikw(ref N, A, ref LDA, IPIV, (float*)workHandle.Pointer, ref lwork, ref info);

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

                LAPACK._3umidwpd(ref N, A, ref LDA, IPIV, &work, ref lwork, ref info);

                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<complex>((ulong)lwork);

                    LAPACK._3umidwpd(ref N, A, ref LDA, IPIV, (complex*)workHandle.Pointer, ref lwork, ref info);

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

                LAPACK._4fupxqd8(ref N, A, ref LDA, IPIV, &work, ref lwork, ref info);

                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<fcomplex>((ulong)lwork);

                    LAPACK._4fupxqd8(ref N, A, ref LDA, IPIV, (fcomplex*)workHandle.Pointer, ref lwork, ref info);

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
<type>
    <source locate="here">
        _ac2l6xc0
    </source>
    <destination>_2yle2tri</destination>
    <destination>_ljflx7wo</destination>
    <destination>_egefx4n9</destination>
</type>
</hycalper>
*/
        public void /*!HC:dllfunc*/ dgeqrf(int M, int N, double* A, int lda, double* tau, ref int info) {
            double work = 1;
            int lwork = -1;
            MemoryHandle workHandle = null;
            try {

                LAPACK._ac2l6xc0(ref M, ref N, A, ref lda, tau, &work, ref lwork, ref info);

                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<double>((ulong)lwork);

                    LAPACK._ac2l6xc0(ref M, ref N, A, ref lda, tau, (double*)workHandle.Pointer, ref lwork, ref info);

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

                LAPACK._egefx4n9(ref M, ref N, A, ref lda, tau, &work, ref lwork, ref info);

                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<float>((ulong)lwork);

                    LAPACK._egefx4n9(ref M, ref N, A, ref lda, tau, (float*)workHandle.Pointer, ref lwork, ref info);

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

                LAPACK._ljflx7wo(ref M, ref N, A, ref lda, tau, &work, ref lwork, ref info);

                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<complex>((ulong)lwork);

                    LAPACK._ljflx7wo(ref M, ref N, A, ref lda, tau, (complex*)workHandle.Pointer, ref lwork, ref info);

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

                LAPACK._2yle2tri(ref M, ref N, A, ref lda, tau, &work, ref lwork, ref info);

                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<fcomplex>((ulong)lwork);

                    LAPACK._2yle2tri(ref M, ref N, A, ref lda, tau, (fcomplex*)workHandle.Pointer, ref lwork, ref info);

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
<type>
    <source locate="here">
        _hxix712m
    </source>
    <destination>_hfwn2zbk</destination>
    <destination>_13b6etkp</destination>
    <destination>_mwcfh21x</destination>
</type>
</hycalper>
*/
        public void dorgqr(int M, int N, int K, double* A, int lda, double* tau, ref int info) {
            double work = 1;
            int lwork = -1;
            MemoryHandle workHandle = null;
            try {

                LAPACK._hxix712m(ref M, ref N, ref K, A, ref lda, tau, &work, ref lwork, ref info);

                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<double>((ulong)lwork);


                    LAPACK._hxix712m(ref M, ref N, ref K, A, ref lda, tau, (double*)workHandle.Pointer, ref lwork, ref info);

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

                LAPACK._mwcfh21x(ref M, ref N, ref K, A, ref lda, tau, &work, ref lwork, ref info);

                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<float>((ulong)lwork);


                    LAPACK._mwcfh21x(ref M, ref N, ref K, A, ref lda, tau, (float*)workHandle.Pointer, ref lwork, ref info);

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

                LAPACK._13b6etkp(ref M, ref N, ref K, A, ref lda, tau, &work, ref lwork, ref info);

                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<complex>((ulong)lwork);


                    LAPACK._13b6etkp(ref M, ref N, ref K, A, ref lda, tau, (complex*)workHandle.Pointer, ref lwork, ref info);

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

                LAPACK._hfwn2zbk(ref M, ref N, ref K, A, ref lda, tau, &work, ref lwork, ref info);

                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<fcomplex>((ulong)lwork);


                    LAPACK._hfwn2zbk(ref M, ref N, ref K, A, ref lda, tau, (fcomplex*)workHandle.Pointer, ref lwork, ref info);

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
    <source locate="here">
        _ygv24296
    </source>
    <destination>_er0i6q6o</destination>
    <destination>_s006yt38</destination>
    <destination>_dpoj3pi6</destination>
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

                LAPACK._ygv24296(ref M, ref N, A, ref LDA, JPVT, tau, &work, ref lwork, ref info);


#else
                rWorkHandle = New<Double>((ulong)(2 * N));
                
            LAPACK._ygv24296 (ref M, ref N, A, ref LDA, JPVT, tau, &work, ref lwork, (Double*)rWorkHandle.Pointer, ref info);

#endif

                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<double>((ulong)lwork);
                    /*!HC:cmplxRwork*/
#if !IS_COMPLEX

                    LAPACK._ygv24296(ref M, ref N, A, ref LDA, JPVT, tau, (double*)workHandle.Pointer, ref lwork, ref info);

#else
                    
                    LAPACK._ygv24296 (ref M, ref N, A, ref LDA, JPVT, tau, (double*)workHandle.Pointer, ref lwork, (Double*)rWorkHandle.Pointer, ref info);

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

                LAPACK._dpoj3pi6(ref M, ref N, A, ref LDA, JPVT, tau, &work, ref lwork, ref info);


#else
                rWorkHandle = New<float>((ulong)(2 * N));
                
            LAPACK._dpoj3pi6 (ref M, ref N, A, ref LDA, JPVT, tau, &work, ref lwork, (float*)rWorkHandle.Pointer, ref info);

#endif

                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<float>((ulong)lwork);
                    #if !IS_COMPLEX

                    LAPACK._dpoj3pi6(ref M, ref N, A, ref LDA, JPVT, tau, (float*)workHandle.Pointer, ref lwork, ref info);

#else
                    
                    LAPACK._dpoj3pi6 (ref M, ref N, A, ref LDA, JPVT, tau, (float*)workHandle.Pointer, ref lwork, (float*)rWorkHandle.Pointer, ref info);

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

                LAPACK._s006yt38(ref M, ref N, A, ref LDA, JPVT, tau, &work, ref lwork, ref info);


#else
                rWorkHandle = New<double>((ulong)(2 * N));
                
            LAPACK._s006yt38 (ref M, ref N, A, ref LDA, JPVT, tau, &work, ref lwork, (double*)rWorkHandle.Pointer, ref info);

#endif

                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<complex>((ulong)lwork);
                    #if IS_COMPLEX

                    LAPACK._s006yt38(ref M, ref N, A, ref LDA, JPVT, tau, (complex*)workHandle.Pointer, ref lwork, ref info);

#else
                    
                    LAPACK._s006yt38 (ref M, ref N, A, ref LDA, JPVT, tau, (complex*)workHandle.Pointer, ref lwork, (double*)rWorkHandle.Pointer, ref info);

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

                LAPACK._er0i6q6o(ref M, ref N, A, ref LDA, JPVT, tau, &work, ref lwork, ref info);


#else
                rWorkHandle = New<float>((ulong)(2 * N));
                
            LAPACK._er0i6q6o (ref M, ref N, A, ref LDA, JPVT, tau, &work, ref lwork, (float*)rWorkHandle.Pointer, ref info);

#endif

                lwork = (int)work;
                if (lwork > 0 && info == 0) {
                    workHandle = New<fcomplex>((ulong)lwork);
                    #if IS_COMPLEX

                    LAPACK._er0i6q6o(ref M, ref N, A, ref LDA, JPVT, tau, (fcomplex*)workHandle.Pointer, ref lwork, ref info);

#else
                    
                    LAPACK._er0i6q6o (ref M, ref N, A, ref LDA, JPVT, tau, (fcomplex*)workHandle.Pointer, ref lwork, (float*)rWorkHandle.Pointer, ref info);

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

            LAPACK._32vnbd7a(uplo, transA, diag, ref N, ref nrhs, A, ref LDA, B, ref LDB, ref info);

        }
        public void strtrs(char uplo, char transA, char diag, int N, int nrhs, float* A, int LDA, float* B, int LDB, ref int info) {

            LAPACK._6pzgz9zt(uplo, transA, diag, ref N, ref nrhs, A, ref LDA, B, ref LDB, ref info);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uplo"></param>
        /// <param name="transA"></param>
        /// <param name="diag"></param>
        /// <param name="N"></param>
        /// <param name="nrhs"></param>
        /// <param name="A"></param>
        /// <param name="LDA"></param>
        /// <param name="B"></param>
        /// <param name="LDB"></param>
        /// <param name="info"></param>
        public void ctrtrs(char uplo, char transA, char diag, int N, int nrhs, fcomplex* A, int LDA, fcomplex* B, int LDB, ref int info) {

            LAPACK._8jc4dph6(uplo, transA, diag, ref N, ref nrhs, A, ref LDA, B, ref LDB, ref info);

        }
        public void ztrtrs(char uplo, char transA, char diag, int N, int nrhs, complex* A, int LDA, complex* B, int LDB, ref int info) {

            LAPACK._pqn5b4dw(uplo, transA, diag, ref N, ref nrhs, A, ref LDA, B, ref LDB, ref info);

        }
        public void dgetrs(char trans, int N, int NRHS, double* A, int LDA, int* IPIV, double* B, int LDB, ref int info) {

            LAPACK._jmd58p6s(trans, ref N, ref NRHS, A, ref LDA, IPIV, B, ref LDB, ref info);

        }
        public void sgetrs(char trans, int N, int NRHS, float* A, int LDA, int* IPIV, float* B, int LDB, ref int info) {

            LAPACK._nepik37m(trans, ref N, ref NRHS, A, ref LDA, IPIV, B, ref LDB, ref info);

        }
        public void cgetrs(char trans, int N, int NRHS, fcomplex* A, int LDA, int* IPIV, fcomplex* B, int LDB, ref int info) {

            LAPACK._us183wko(trans, ref N, ref NRHS, A, ref LDA, IPIV, B, ref LDB, ref info);

        }
        public void zgetrs(char trans, int N, int NRHS, complex* A, int LDA, int* IPIV, complex* B, int LDB, ref int info) {

            LAPACK._uz7v0b9c(trans, ref N, ref NRHS, A, ref LDA, IPIV, B, ref LDB, ref info);

        }
        public void dpotrs(char uplo, int n, int nrhs, double* A, int lda, double* B, int ldb, ref int info) {

            LAPACK._jgz807xh(uplo, ref n, ref nrhs, A, ref lda, B, ref ldb, ref info);

        }
        public void spotrs(char uplo, int n, int nrhs, float* A, int lda, float* B, int ldb, ref int info) {

            LAPACK._9xem90jg(uplo, ref n, ref nrhs, A, ref lda, B, ref ldb, ref info);

        }
        public void cpotrs(char uplo, int n, int nrhs, fcomplex* A, int lda, fcomplex* B, int ldb, ref int info) {

            LAPACK._og4szj00(uplo, ref n, ref nrhs, A, ref lda, B, ref ldb, ref info);

        }
        public void zpotrs(char uplo, int n, int nrhs, complex* A, int lda, complex* B, int ldb, ref int info) {

            LAPACK._18nhpoga(uplo, ref n, ref nrhs, A, ref lda, B, ref ldb, ref info);

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
    <source locate="here">
        _mc7n64ut
    </source>
    <destination>_y4sjinuv</destination>
    <destination>_otp4ja5r</destination>
    <destination>_qknj33b9</destination>
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
            int iwork = 0;
            int lwork = -1;
            MemoryHandle iWorkHandle = null;
            MemoryHandle workHandle = null;
            MemoryHandle rWorkHandle = null;
            try {
                /*!HC:cmplxRwork*/
#if !IS_COMPLEX
                LAPACK._mc7n64ut(ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S, ref RCond, ref rank, &work, ref lwork, &iwork, ref info);

#else
                Double rwork = 0; 
                LAPACK._mc7n64ut (ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S,ref RCond, ref rank, &work, ref lwork, &rwork, &iwork, ref info);

#endif
                if (info != 0)
                    throw new ArgumentException("dgelsd: invalid parameter: #" + (-info).ToString());
                lwork = (int)work;
                if (lwork <= 0 || iwork <= 0)
                    throw new ArgumentException("dgelsd: unknown error determining work buffer size (lwork).");

                iWorkHandle = New<int>((ulong)iwork);
                workHandle = New<double>((ulong)lwork);

                /*!HC:cmplxRwork*/
#if !IS_COMPLEX
                LAPACK._mc7n64ut(ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S, ref RCond, ref rank, (double*)workHandle.Pointer, ref lwork, (int*)iWorkHandle.Pointer, ref info);

#else
                rWorkHandle = New<Double>((ulong)rwork);
                LAPACK._mc7n64ut (ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S,ref RCond, ref rank, (double*)workHandle.Pointer, ref lwork, (Double*)rWorkHandle.Pointer, (int*)iWorkHandle.Pointer, ref info);

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
            int iwork = 0;
            int lwork = -1;
            MemoryHandle iWorkHandle = null;
            MemoryHandle workHandle = null;
            MemoryHandle rWorkHandle = null;
            try {
                #if !IS_COMPLEX
                LAPACK._qknj33b9(ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S, ref RCond, ref rank, &work, ref lwork, &iwork, ref info);

#else
                float rwork = 0; 
                LAPACK._qknj33b9 (ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S,ref RCond, ref rank, &work, ref lwork, &rwork, &iwork, ref info);

#endif
                if (info != 0)
                    throw new ArgumentException("sgelsd : invalid parameter: #" + (-info).ToString());
                lwork = (int)work;
                if (lwork <= 0 || iwork <= 0)
                    throw new ArgumentException("sgelsd : unknown error determining work buffer size (lwork).");

                iWorkHandle = New<int>((ulong)iwork);
                workHandle = New<float>((ulong)lwork);

                #if !IS_COMPLEX
                LAPACK._qknj33b9(ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S, ref RCond, ref rank, (float*)workHandle.Pointer, ref lwork, (int*)iWorkHandle.Pointer, ref info);

#else
                rWorkHandle = New<float>((ulong)rwork);
                LAPACK._qknj33b9 (ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S,ref RCond, ref rank, (float*)workHandle.Pointer, ref lwork, (float*)rWorkHandle.Pointer, (int*)iWorkHandle.Pointer, ref info);

#endif
            } finally {
                if (iWorkHandle != null) free<int>(iWorkHandle, 0);
                if (workHandle != null) free<float>(workHandle, 0);
                if (rWorkHandle != null) free<float>(rWorkHandle, 0);
            }
        }
       
        public void zgelsd (int m, int n, int nrhs, complex* A, int lda, complex* B, int ldb, double* S, double RCond, ref int rank, ref int info) {
            complex work = 0;
            int iwork = 0;
            int lwork = -1;
            MemoryHandle iWorkHandle = null;
            MemoryHandle workHandle = null;
            MemoryHandle rWorkHandle = null;
            try {
                #if IS_COMPLEX
                LAPACK._otp4ja5r(ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S, ref RCond, ref rank, &work, ref lwork, &iwork, ref info);

#else
                double rwork = 0; 
                LAPACK._otp4ja5r (ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S,ref RCond, ref rank, &work, ref lwork, &rwork, &iwork, ref info);

#endif
                if (info != 0)
                    throw new ArgumentException("zgelsd : invalid parameter: #" + (-info).ToString());
                lwork = (int)work;
                if (lwork <= 0 || iwork <= 0)
                    throw new ArgumentException("zgelsd : unknown error determining work buffer size (lwork).");

                iWorkHandle = New<int>((ulong)iwork);
                workHandle = New<complex>((ulong)lwork);

                #if IS_COMPLEX
                LAPACK._otp4ja5r(ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S, ref RCond, ref rank, (complex*)workHandle.Pointer, ref lwork, (int*)iWorkHandle.Pointer, ref info);

#else
                rWorkHandle = New<double>((ulong)rwork);
                LAPACK._otp4ja5r (ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S,ref RCond, ref rank, (complex*)workHandle.Pointer, ref lwork, (double*)rWorkHandle.Pointer, (int*)iWorkHandle.Pointer, ref info);

#endif
            } finally {
                if (iWorkHandle != null) free<int>(iWorkHandle, 0);
                if (workHandle != null) free<complex>(workHandle, 0);
                if (rWorkHandle != null) free<double>(rWorkHandle, 0);
            }
        }
       
        public void cgelsd (int m, int n, int nrhs, fcomplex* A, int lda, fcomplex* B, int ldb, float* S, float RCond, ref int rank, ref int info) {
            fcomplex work = 0;
            int iwork = 0;
            int lwork = -1;
            MemoryHandle iWorkHandle = null;
            MemoryHandle workHandle = null;
            MemoryHandle rWorkHandle = null;
            try {
                #if IS_COMPLEX
                LAPACK._y4sjinuv(ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S, ref RCond, ref rank, &work, ref lwork, &iwork, ref info);

#else
                float rwork = 0; 
                LAPACK._y4sjinuv (ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S,ref RCond, ref rank, &work, ref lwork, &rwork, &iwork, ref info);

#endif
                if (info != 0)
                    throw new ArgumentException("cgelsd : invalid parameter: #" + (-info).ToString());
                lwork = (int)work;
                if (lwork <= 0 || iwork <= 0)
                    throw new ArgumentException("cgelsd : unknown error determining work buffer size (lwork).");

                iWorkHandle = New<int>((ulong)iwork);
                workHandle = New<fcomplex>((ulong)lwork);

                #if IS_COMPLEX
                LAPACK._y4sjinuv(ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S, ref RCond, ref rank, (fcomplex*)workHandle.Pointer, ref lwork, (int*)iWorkHandle.Pointer, ref info);

#else
                rWorkHandle = New<float>((ulong)rwork);
                LAPACK._y4sjinuv (ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, S,ref RCond, ref rank, (fcomplex*)workHandle.Pointer, ref lwork, (float*)rWorkHandle.Pointer, (int*)iWorkHandle.Pointer, ref info);

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
        _2h2fjxo9
    </source>
    <destination>_lsidb5bh</destination>
</type>
<type>
    <source locate="here">
        _ij0dqno0
    </source>
    <destination>_13mcp5d6</destination>
</type>
<type>
    <source locate="here">
        _61rqfkum
    </source>
    <destination>_c3m9iro8</destination>
</type>
<type>
    <source locate="here">
        _h1yivcup
    </source>
    <destination>_p46lqk1u</destination>
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
            LAPACK._2h2fjxo9(ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, JPVT0, ref RCond, ref rank, &work, ref lwork, ref info);

            if (info != 0)
                throw new ArgumentException("dgelsy: unable to determine optimal block size.");
            lwork = (int)work;
            var workHandle = New<double>((ulong)lwork);
            LAPACK._2h2fjxo9(ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, JPVT0, ref RCond, ref rank, (double*)workHandle.Pointer, ref lwork, ref info);

            free<double>(workHandle, 0);
        }
        public void dgeevx(char balance, char jobvl, char jobvr, char sense, int n, double* A, int lda, double* wr, double* wi, double* vl, int ldvl, double* vr, int ldvr, ref int ilo, ref int ihi, double* scale, ref double abnrm, double* rconde, double* rcondv, ref int info) {
            int lwork = -1;
            double work = 0;
            var iworkHandle = New<int>((ulong)(2 * n - 2));
            LAPACK._ij0dqno0(balance, jobvl, jobvr, sense, ref n, A, ref lda, wr, wi, vl, ref ldvl, vr, ref ldvr, ref ilo, ref ihi, scale, ref abnrm, rconde, rcondv, &work, ref lwork, (int*)iworkHandle.Pointer, ref info);

            if (info != 0)
                throw new ArgumentException("dgeevx: unable to determine working set size. (info: " + info + ")");
            lwork = (int)work;
            var workHandle = New<double>((ulong)lwork);
            LAPACK._ij0dqno0(balance, jobvl, jobvr, sense, ref n, A, ref lda, wr, wi, vl, ref ldvl, vr, ref ldvr, ref ilo, ref ihi, scale, ref abnrm, rconde, rcondv, (double*)workHandle.Pointer, ref lwork, (int*)iworkHandle.Pointer, ref info);

            free<double>(workHandle, 0);
            free<int>(iworkHandle, 0);
        }
        //?[he/sy]gv - generalized eigenproblem
        public void dsygv(int itype, char jobz, char uplo, int n, double* A, int lda, double* B, int ldb, double* w, ref int info) {
            int lwork = -1;
            double work = 0;
            LAPACK._61rqfkum(ref itype, jobz, uplo, ref n, A, ref lda, B, ref ldb, w, &work, ref lwork, ref info);

            if (info != 0 || work <= 0.0)
                throw new ArgumentException("dsygv: unable to determine working set size. (info: " + info + ")");

            lwork = (int)work;
            var workHandle = New<double>((ulong)lwork);
            LAPACK._61rqfkum(ref itype, jobz, uplo, ref n, A, ref lda, B, ref ldb, w, (double*)workHandle.Pointer, ref lwork, ref info);

            free<double>(workHandle, 0);
        }
        public void dsyevr(char jobz, char range, char uplo, int n, double* A, int lda, double vl, double vu, int il, int iu, double abstol, ref int m, double* w, double* z, int ldz, int* isuppz, ref int info) {
            int liwork = -1;
            int lwork = -1;
            double work = 0;
            int iwork = 0;
            //byte jz = (byte)jobz,rn = (byte) range,ul = (byte)uplo; 
            LAPACK._h1yivcup(jobz, range, uplo, ref n, A, ref lda, ref vl, ref vu, ref il, ref iu, ref abstol, ref m, w, z, ref ldz, isuppz, &work, ref lwork, &iwork, ref liwork, ref info);

            if (info != 0) {
                throw new ArgumentException("dsyevr: unable to determine working set size. (info: " + info + ")");
            }
            lwork = (int)work;
            var workHandle = New<double>((ulong)lwork);
            liwork = (int)iwork;
            var iworkHandle = New<int>((ulong)liwork);
            LAPACK._h1yivcup(jobz, range, uplo, ref n, A, ref lda, ref vl, ref vu, ref il, ref iu, ref abstol, ref m, w, z, ref ldz, isuppz, (double*)workHandle.Pointer, ref lwork, (int*)iworkHandle.Pointer, ref liwork, ref info);

            free<int>(iworkHandle, 0);
            free<double>(workHandle, 0);
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        public void sgelsy(int m, int n, int nrhs, float* A, int lda, float* B, int ldb, int* JPVT0, float RCond, ref int rank, ref int info) {
            int lwork = -1;
            float work = 0;
            LAPACK._lsidb5bh(ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, JPVT0, ref RCond, ref rank, &work, ref lwork, ref info);

            if (info != 0)
                throw new ArgumentException("sgelsy: unable to determine optimal block size.");
            lwork = (int)work;
            var workHandle = New<float>((ulong)lwork);
            LAPACK._lsidb5bh(ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, JPVT0, ref RCond, ref rank, (float*)workHandle.Pointer, ref lwork, ref info);

            free<float>(workHandle, 0);
        }
        public void sgeevx(char balance, char jobvl, char jobvr, char sense, int n, float* A, int lda, float* wr, float* wi, float* vl, int ldvl, float* vr, int ldvr, ref int ilo, ref int ihi, float* scale, ref float abnrm, float* rconde, float* rcondv, ref int info) {
            int lwork = -1;
            float work = 0;
            var iworkHandle = New<int>((ulong)(2 * n - 2));
            LAPACK._13mcp5d6(balance, jobvl, jobvr, sense, ref n, A, ref lda, wr, wi, vl, ref ldvl, vr, ref ldvr, ref ilo, ref ihi, scale, ref abnrm, rconde, rcondv, &work, ref lwork, (int*)iworkHandle.Pointer, ref info);

            if (info != 0)
                throw new ArgumentException("sgeevx: unable to determine working set size. (info: " + info + ")");
            lwork = (int)work;
            var workHandle = New<float>((ulong)lwork);
            LAPACK._13mcp5d6(balance, jobvl, jobvr, sense, ref n, A, ref lda, wr, wi, vl, ref ldvl, vr, ref ldvr, ref ilo, ref ihi, scale, ref abnrm, rconde, rcondv, (float*)workHandle.Pointer, ref lwork, (int*)iworkHandle.Pointer, ref info);

            free<float>(workHandle, 0);
            free<int>(iworkHandle, 0);
        }
        //?[he/sy]gv - generalized eigenproblem
        public void ssygv(int itype, char jobz, char uplo, int n, float* A, int lda, float* B, int ldb, float* w, ref int info) {
            int lwork = -1;
            float work = 0;
            LAPACK._c3m9iro8(ref itype, jobz, uplo, ref n, A, ref lda, B, ref ldb, w, &work, ref lwork, ref info);

            if (info != 0 || work <= 0.0)
                throw new ArgumentException("ssygv: unable to determine working set size. (info: " + info + ")");

            lwork = (int)work;
            var workHandle = New<float>((ulong)lwork);
            LAPACK._c3m9iro8(ref itype, jobz, uplo, ref n, A, ref lda, B, ref ldb, w, (float*)workHandle.Pointer, ref lwork, ref info);

            free<float>(workHandle, 0);
        }
        public void ssyevr(char jobz, char range, char uplo, int n, float* A, int lda, float vl, float vu, int il, int iu, float abstol, ref int m, float* w, float* z, int ldz, int* isuppz, ref int info) {
            int liwork = -1;
            int lwork = -1;
            float work = 0;
            int iwork = 0;
            //byte jz = (byte)jobz,rn = (byte) range,ul = (byte)uplo; 
            LAPACK._p46lqk1u(jobz, range, uplo, ref n, A, ref lda, ref vl, ref vu, ref il, ref iu, ref abstol, ref m, w, z, ref ldz, isuppz, &work, ref lwork, &iwork, ref liwork, ref info);

            if (info != 0) {
                throw new ArgumentException("ssyevr: unable to determine working set size. (info: " + info + ")");
            }
            lwork = (int)work;
            var workHandle = New<float>((ulong)lwork);
            liwork = (int)iwork;
            var iworkHandle = New<int>((ulong)liwork);
            LAPACK._p46lqk1u(jobz, range, uplo, ref n, A, ref lda, ref vl, ref vu, ref il, ref iu, ref abstol, ref m, w, z, ref ldz, isuppz, (float*)workHandle.Pointer, ref lwork, (int*)iworkHandle.Pointer, ref liwork, ref info);

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
<type>
    <source locate="here">
        _rsrplwoi
    </source>
    <destination>_qxri1eh7</destination>
</type>
<type>
    <source locate="here">
        _odkzrkpt
    </source>
    <destination>_oek2b4i2</destination>
</type>
<type>
    <source locate="here">
        _qdy0ve9d
    </source>
    <destination>_uc3yex4n</destination>
</type>
<type>
    <source locate="here">
        _xyu9aa4l
    </source>
    <destination>_rnk311bd</destination>
</type>
</hycalper>
*/
        public void zgelsy(int m, int n, int nrhs, complex* A, int lda, complex* B, int ldb, int* JPVT0, double RCond, ref int rank, ref int info) {
            int lwork = -1;
            complex work = 0;
            double rwork = 0;
            LAPACK._rsrplwoi(ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, JPVT0, ref RCond, ref rank, &work, ref lwork, &rwork, ref info);

            if (info != 0)
                throw new ArgumentException($"zgelsy: unable to determine working set size. (info: {info})");
            lwork = (int)work;
            var workHandle = New<complex>((ulong)lwork);
            var rworkHandle = New<double>((ulong)lwork);
            LAPACK._rsrplwoi(ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, JPVT0, ref RCond, ref rank, (complex*)workHandle.Pointer, ref lwork, (double*)rworkHandle.Pointer, ref info);

            free<complex>(workHandle, 0);
            free<double>(rworkHandle, 0);
        }
        public void zgeevx(char balance, char jobvl, char jobvr, char sense, int n, complex* A, int lda, complex* w, complex* vl, int ldvl, complex* vr, int ldvr, ref int ilo, ref int ihi, double* scale, ref double abnrm, double* rconde, double* rcondv, ref int info) {
            complex work = 0;
            double rwork = 0;
            int lwork = -1;
            LAPACK._odkzrkpt(balance, jobvl, jobvr, sense, ref n, A, ref lda, w, vl, ref ldvl, vr, ref ldvr, ref ilo, ref ihi, scale, ref abnrm, rconde, rcondv, &work, ref lwork, &rwork, ref info);

            if (info != 0)
                throw new ArgumentException($"zgeevx: unable to determine working set size. (info: {info})");
            lwork = (int)work;
            var workHandle = New<complex>((ulong)lwork);
            var rworkHandle = New<double>((ulong)Math.Max(1, 2 * n));
            LAPACK._odkzrkpt(balance, jobvl, jobvr, sense, ref n, A, ref lda, w, vl, ref ldvl, vr, ref ldvr, ref ilo, ref ihi, scale, ref abnrm, rconde, rcondv, (complex*)workHandle.Pointer, ref lwork, (double*)rworkHandle.Pointer, ref info);

            free<double>(rworkHandle, 0);
            free<complex>(workHandle, 0);
        }
        public void zheevr(char jobz, char range, char uplo, int n, complex* A, int lda, double vl, double vu, int il, int iu, double abstol, ref int m, double* w, complex* z, int ldz, int* isuppz, ref int info) {
            complex work = 0;
            double rwork = 0;
            int iwork = 0;
            int lrwork = -1, liwork = -1, lwork = -1;
            LAPACK._qdy0ve9d(jobz, range, uplo, ref n, A, ref lda, ref vl, ref vu, ref il, ref iu, ref abstol, ref m, w, z, ref ldz, isuppz, &work, ref lwork, &rwork, ref lrwork, &iwork, ref liwork, ref info);

            if (info != 0) {
                throw new ArgumentException($"zsyevr: unable to determine working set size. (info: {info})");
            }
            lrwork = (int)rwork;
            var rworkHandle = New<double>((ulong)lrwork);
            lwork = (int)work;
            var workHandle = New<complex>((ulong)lwork);
            liwork = (int)iwork;
            var iworkHandle = New<int>((ulong)liwork);
            LAPACK._qdy0ve9d(jobz, range, uplo, ref n, A, ref lda, ref vl, ref vu, ref il, ref iu, ref abstol, ref m, w, z, ref ldz, isuppz, (complex*)workHandle.Pointer, ref lwork, (double*)rworkHandle.Pointer, ref lrwork, (int*)iworkHandle.Pointer, ref liwork, ref info);

            free<int>(iworkHandle, 0);
            free<complex>(workHandle, 0);
            free<double>(rworkHandle, 0);
        }
        public void zhegv(int itype, char jobz, char uplo, int n, complex* A, int lda, complex* B, int ldb, double* w, ref int info) {
            complex work = 0;
            double rwork = 0;
            int lwork = -1;
            LAPACK._xyu9aa4l(ref itype, jobz, uplo, ref n, A, ref lda, B, ref ldb, w, &work, ref lwork, &rwork, ref info);

            if (info != 0 || work <= 0.0)
                throw new ArgumentException($"zhegv: unable to determine working set size. (info: {info})");

            // create temporary array(s)
            lwork = (int)work;
            var workHandle = New<complex>((ulong)lwork);
            var rworkHandle = New<double>((ulong)Math.Max(1, 3 * n - 2));
            LAPACK._xyu9aa4l(ref itype, jobz, uplo, ref n, A, ref lda, B, ref ldb, w, (complex*)workHandle.Pointer, ref lwork, (double*)rworkHandle.Pointer, ref info);

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
            LAPACK._qxri1eh7(ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, JPVT0, ref RCond, ref rank, &work, ref lwork, &rwork, ref info);

            if (info != 0)
                throw new ArgumentException($"cgelsy: unable to determine working set size. (info: {info})");
            lwork = (int)work;
            var workHandle = New<fcomplex>((ulong)lwork);
            var rworkHandle = New<float>((ulong)lwork);
            LAPACK._qxri1eh7(ref m, ref n, ref nrhs, A, ref lda, B, ref ldb, JPVT0, ref RCond, ref rank, (fcomplex*)workHandle.Pointer, ref lwork, (float*)rworkHandle.Pointer, ref info);

            free<fcomplex>(workHandle, 0);
            free<float>(rworkHandle, 0);
        }
        public void cgeevx(char balance, char jobvl, char jobvr, char sense, int n, fcomplex* A, int lda, fcomplex* w, fcomplex* vl, int ldvl, fcomplex* vr, int ldvr, ref int ilo, ref int ihi, float* scale, ref float abnrm, float* rconde, float* rcondv, ref int info) {
            fcomplex work = 0;
            float rwork = 0;
            int lwork = -1;
            LAPACK._oek2b4i2(balance, jobvl, jobvr, sense, ref n, A, ref lda, w, vl, ref ldvl, vr, ref ldvr, ref ilo, ref ihi, scale, ref abnrm, rconde, rcondv, &work, ref lwork, &rwork, ref info);

            if (info != 0)
                throw new ArgumentException($"cgeevx: unable to determine working set size. (info: {info})");
            lwork = (int)work;
            var workHandle = New<fcomplex>((ulong)lwork);
            var rworkHandle = New<float>((ulong)Math.Max(1, 2 * n));
            LAPACK._oek2b4i2(balance, jobvl, jobvr, sense, ref n, A, ref lda, w, vl, ref ldvl, vr, ref ldvr, ref ilo, ref ihi, scale, ref abnrm, rconde, rcondv, (fcomplex*)workHandle.Pointer, ref lwork, (float*)rworkHandle.Pointer, ref info);

            free<float>(rworkHandle, 0);
            free<fcomplex>(workHandle, 0);
        }
        public void cheevr(char jobz, char range, char uplo, int n, fcomplex* A, int lda, float vl, float vu, int il, int iu, float abstol, ref int m, float* w, fcomplex* z, int ldz, int* isuppz, ref int info) {
            fcomplex work = 0;
            float rwork = 0;
            int iwork = 0;
            int lrwork = -1, liwork = -1, lwork = -1;
            LAPACK._uc3yex4n(jobz, range, uplo, ref n, A, ref lda, ref vl, ref vu, ref il, ref iu, ref abstol, ref m, w, z, ref ldz, isuppz, &work, ref lwork, &rwork, ref lrwork, &iwork, ref liwork, ref info);

            if (info != 0) {
                throw new ArgumentException($"zsyevr: unable to determine working set size. (info: {info})");
            }
            lrwork = (int)rwork;
            var rworkHandle = New<float>((ulong)lrwork);
            lwork = (int)work;
            var workHandle = New<fcomplex>((ulong)lwork);
            liwork = (int)iwork;
            var iworkHandle = New<int>((ulong)liwork);
            LAPACK._uc3yex4n(jobz, range, uplo, ref n, A, ref lda, ref vl, ref vu, ref il, ref iu, ref abstol, ref m, w, z, ref ldz, isuppz, (fcomplex*)workHandle.Pointer, ref lwork, (float*)rworkHandle.Pointer, ref lrwork, (int*)iworkHandle.Pointer, ref liwork, ref info);

            free<int>(iworkHandle, 0);
            free<fcomplex>(workHandle, 0);
            free<float>(rworkHandle, 0);
        }
        public void chegv(int itype, char jobz, char uplo, int n, fcomplex* A, int lda, fcomplex* B, int ldb, float* w, ref int info) {
            fcomplex work = 0;
            float rwork = 0;
            int lwork = -1;
            LAPACK._rnk311bd(ref itype, jobz, uplo, ref n, A, ref lda, B, ref ldb, w, &work, ref lwork, &rwork, ref info);

            if (info != 0 || work <= 0.0)
                throw new ArgumentException($"chegv: unable to determine working set size. (info: {info})");

            // create temporary array(s)
            lwork = (int)work;
            var workHandle = New<fcomplex>((ulong)lwork);
            var rworkHandle = New<float>((ulong)Math.Max(1, 3 * n - 2));
            LAPACK._rnk311bd(ref itype, jobz, uplo, ref n, A, ref lda, B, ref ldb, w, (fcomplex*)workHandle.Pointer, ref lwork, (float*)rworkHandle.Pointer, ref info);

            free<float>(rworkHandle, 0);
            free<fcomplex>(workHandle, 0);
        }

#endregion HYCALPER AUTO GENERATED CODE

        #endregion
    }

}
