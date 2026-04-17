//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////

using ILNumerics.Core.Native;
using System;
using static ILNumerics.Core.Functions.Builtin.MathInternal;
using ILNumerics.Core.Functions.Builtin;

/*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        double
    </source>
    <destination>complex</destination>
    <destination>fcomplex</destination>
    <destination>float</destination>
</type>
<type>
    <source locate="here">
        dgemm
    </source>
    <destination>zgemm</destination>
    <destination>cgemm</destination>
    <destination>sgemm</destination>
</type>
</hycalper>
*/

namespace ILNumerics {

    public static partial class ILMath {

        #region HYCALPER LOOPSTART Matrix_multiply
        /// <summary>
        /// Matrix multiplication for 3 general matrices (2D). Performs A ** (B ** C). 
        /// </summary>
        /// <param name="A">Input matrix A.</param>
        /// <param name="B">Input matrix B.</param>
        /// <param name="C">Input matrix C.</param>
        /// <returns>Matrix with result of <paramref name="A"/> ** (<paramref name="B"/> ** <paramref name="C"/>), where ** denotes matrix multiplication.</returns>
        /// <remarks><para>All arrays must be matrices with matching dimension length. Watch the oder of computations: <paramref name="B"/> is 
        /// multiplied with <paramref name="C"/> first. The result is right multiplied with <paramref name="A"/>. </para>
        /// <para>The multiplication will be carried out inside optimized native code, if possible. Otherwise, an optimized, blocked, managed version is used. </para>
        /// <para>Be prepared that this function may alter the storage order (<see cref="Size.StorageOrder"/>) of any input parameter without notice.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If at least one of <paramref name="A"/>, <paramref name="B"/> or <paramref name="C"/> is not a matrix or does 
        /// not have a matching shape.</exception>
        public static Array<double> multiply(InArray<double> A, InArray<double> B, InArray<double> C) {
            return multiply(multiply(A, B), C);
        }
        /// <summary>
        /// Matrix multiplication for general matrices (2D). 
        /// </summary>
        /// <param name="A">Input matrix A.</param>
        /// <param name="B">Input matrix B.</param>
        /// <returns>Matrix of size A.S[0] x B.S[1] with result of multiplying the matrices <paramref name="A"/> and <paramref name="B"/>.</returns>
        /// <remarks><para>Both arrays must be matrices with matching dimension length. This is: the number of rows 
        /// of <paramref name="B"/> must equal the number of columns of <paramref name="A"/>. </para>
        /// The multiplication will be carried out inside optimized native code, if possible. Otherwise, an optimized, blocked, managed version is used.  
        /// <para>Be prepared that this function may alter the storage order (<see cref="Size.StorageOrder"/>) of <paramref name="A"/> and/or <paramref name="B"/> without notice.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If at least one of <paramref name="A"/> or <paramref name="B"/> is not a matrix or does 
        /// not have a matching shape.</exception>
        public static Array<double> multiply(InArray<double> A, InArray<double> B) {
            using (Scope.Enter(A, B)) {
                if (isnull(A) || A.Size.NumberOfElements != A.S[0] * A.S[1]) {
                    throw new ArgumentException("A must be a a non-null matrix in order to be multiplied by B.");
                }
                if (isnull(B) || B.Size.NumberOfElements != B.S[0] * B.S[1]) {
                    throw new ArgumentException("B must be a non-null matrix in order to be multiplied by A.");
                }
                if (A.Size[1] != B.Size[0])
                    throw new ArgumentException("Inner matrix dimensions must match for matrix multiply.");

                Array<double> _A = A;
                Array<double> _B = B; 
                unsafe {
                    if (Lapack != null && Lapack is LapackMKL10_0) {

                        // doing  ?GEMM in MKL
                        /* 
                         * We use the FORTRAN interface only. The natural storage order for the BLAS3 FORTRAN 
                         * interaface is ColumnMajor. But we can use it for row major matrices as well. All 
                         * what is required is to transpose the matrices (is performed virtually) and flip 
                         * arguments of the ?gemm call. This gives the transpose of the result which is 
                         * transposed again for the return value. 
                         * 
                         * In order to bring input into continous storage order we consider the larger of _A 
                         * or _B first. Checks are performed on both matrices appropriately. If no advantageous 
                         * storage order can be derived from the existing matrices, Settings:DefaultStorageOrder 
                         * is used. 
                         */

                        if ((_A.S.StorageOrder == StorageOrders.ColumnMajor || (_A.S.IsContinuous && _A.S.NonSingletonDimensions == 1))
                           && (_B.S.StorageOrder == StorageOrders.ColumnMajor || (_B.S.IsContinuous && _B.S.NonSingletonDimensions == 1))) {

                            Array<double> ret = zeros<double>(size(_A.Size[0], _B.Size[1]), StorageOrders.ColumnMajor);
                            double* pA = (double*)_A.GetHostPointerForRead();
                            double* pB = (double*)_B.GetHostPointerForRead();
                            double* pC = (double*)ret.GetHostPointerForWrite();

                            Lapack.dgemm('N', 'N', (int)_A.Size[0], (int)_B.Size[1], (int)_A.Size[1], (double)1.0, pA, (int)_A.Size[0], pB, (int)_B.Size[0], (double)1.0, pC, (int)_A.Size[0]);
                            return ret;

                        } else if ((_A.S.StorageOrder == StorageOrders.RowMajor || (_A.S.IsContinuous && _A.S.NonSingletonDimensions == 1))
                            && (_B.S.StorageOrder == StorageOrders.RowMajor || (_B.S.IsContinuous && _B.S.NonSingletonDimensions == 1))) {

                            return multiply(_B.T, _A.T).T;

                        }
                        #region find the order which requires minimal memory copies
                        if (_A.S.NumberOfElements > _B.S.NumberOfElements) {
                            checkEnsureStorageOrders4multiply<double>(_A, _B); 
                        } else {
                            checkEnsureStorageOrders4multiply<double>(_B, _A);
                        }
                        #endregion
                        // restart
                        return multiply(_A, _B);

                    } else {
                        //managed, blocked MatMult
                        #region storage orders
                        if (!_A.Size.IsContinuous) {
                            _A.Storage.EnsureStorageOrder(Settings.DefaultStorageOrder);
                        }
                        if (!_B.Size.IsContinuous) {
                            _B.Storage.EnsureStorageOrder(Settings.DefaultStorageOrder);
                        }
                        #endregion
                        Array<double> ret = zeros<double>(size(_A.Size[0], _B.Size[1]), StorageOrders.ColumnMajor);
                        var pA = (double*)_A.GetHostPointerForRead();
                        var pB = (double*)_B.GetHostPointerForRead();
                        var pC = (double*)ret.GetHostPointerForWrite();
                        ILNumerics.Core.LAPACK.Helper.MMultBlocked(
                            pA, pB, pC,
                            _A.S[0], _B.S[1], _A.S[1], Settings.ManagedMultiplyBlockSize,
                            _A.Size.GetStride(0), _A.Size.GetStride(1), _B.Size.GetStride(0), _B.Size.GetStride(1));
                        return ret;
                    }
                }
            }
        }

        #endregion HYCALPER LOOPEND Matrix_multiply
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
        /// <summary>
        /// Matrix multiplication for 3 general matrices (2D). Performs A ** (B ** C). 
        /// </summary>
        /// <param name="A">Input matrix A.</param>
        /// <param name="B">Input matrix B.</param>
        /// <param name="C">Input matrix C.</param>
        /// <returns>Matrix with result of <paramref name="A"/> ** (<paramref name="B"/> ** <paramref name="C"/>), where ** denotes matrix multiplication.</returns>
        /// <remarks><para>All arrays must be matrices with matching dimension length. Watch the oder of computations: <paramref name="B"/> is 
        /// multiplied with <paramref name="C"/> first. The result is right multiplied with <paramref name="A"/>. </para>
        /// <para>The multiplication will be carried out inside optimized native code, if possible. Otherwise, an optimized, blocked, managed version is used. </para>
        /// <para>Be prepared that this function may alter the storage order (<see cref="Size.StorageOrder"/>) of any input parameter without notice.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If at least one of <paramref name="A"/>, <paramref name="B"/> or <paramref name="C"/> is not a matrix or does 
        /// not have a matching shape.</exception>
        public static Array<float> multiply(InArray<float> A, InArray<float> B, InArray<float> C) {
            return multiply(multiply(A, B), C);
        }
        /// <summary>
        /// Matrix multiplication for general matrices (2D). 
        /// </summary>
        /// <param name="A">Input matrix A.</param>
        /// <param name="B">Input matrix B.</param>
        /// <returns>Matrix of size A.S[0] x B.S[1] with result of multiplying the matrices <paramref name="A"/> and <paramref name="B"/>.</returns>
        /// <remarks><para>Both arrays must be matrices with matching dimension length. This is: the number of rows 
        /// of <paramref name="B"/> must equal the number of columns of <paramref name="A"/>. </para>
        /// The multiplication will be carried out inside optimized native code, if possible. Otherwise, an optimized, blocked, managed version is used.  
        /// <para>Be prepared that this function may alter the storage order (<see cref="Size.StorageOrder"/>) of <paramref name="A"/> and/or <paramref name="B"/> without notice.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If at least one of <paramref name="A"/> or <paramref name="B"/> is not a matrix or does 
        /// not have a matching shape.</exception>
        public static Array<float> multiply(InArray<float> A, InArray<float> B) {
            using (Scope.Enter(A, B)) {
                if (isnull(A) || A.Size.NumberOfElements != A.S[0] * A.S[1]) {
                    throw new ArgumentException("A must be a a non-null matrix in order to be multiplied by B.");
                }
                if (isnull(B) || B.Size.NumberOfElements != B.S[0] * B.S[1]) {
                    throw new ArgumentException("B must be a non-null matrix in order to be multiplied by A.");
                }
                if (A.Size[1] != B.Size[0])
                    throw new ArgumentException("Inner matrix dimensions must match for matrix multiply.");

                Array<float> _A = A;
                Array<float> _B = B; 
                unsafe {
                    if (Lapack != null && Lapack is LapackMKL10_0) {

                        // doing  ?GEMM in MKL
                        /* 
                         * We use the FORTRAN interface only. The natural storage order for the BLAS3 FORTRAN 
                         * interaface is ColumnMajor. But we can use it for row major matrices as well. All 
                         * what is required is to transpose the matrices (is performed virtually) and flip 
                         * arguments of the ?gemm call. This gives the transpose of the result which is 
                         * transposed again for the return value. 
                         * 
                         * In order to bring input into continous storage order we consider the larger of _A 
                         * or _B first. Checks are performed on both matrices appropriately. If no advantageous 
                         * storage order can be derived from the existing matrices, Settings:DefaultStorageOrder 
                         * is used. 
                         */

                        if ((_A.S.StorageOrder == StorageOrders.ColumnMajor || (_A.S.IsContinuous && _A.S.NonSingletonDimensions == 1))
                           && (_B.S.StorageOrder == StorageOrders.ColumnMajor || (_B.S.IsContinuous && _B.S.NonSingletonDimensions == 1))) {

                            Array<float> ret = zeros<float>(size(_A.Size[0], _B.Size[1]), StorageOrders.ColumnMajor);
                            float* pA = (float*)_A.GetHostPointerForRead();
                            float* pB = (float*)_B.GetHostPointerForRead();
                            float* pC = (float*)ret.GetHostPointerForWrite();

                            Lapack.sgemm('N', 'N', (int)_A.Size[0], (int)_B.Size[1], (int)_A.Size[1], (float)1.0, pA, (int)_A.Size[0], pB, (int)_B.Size[0], (float)1.0, pC, (int)_A.Size[0]);
                            return ret;

                        } else if ((_A.S.StorageOrder == StorageOrders.RowMajor || (_A.S.IsContinuous && _A.S.NonSingletonDimensions == 1))
                            && (_B.S.StorageOrder == StorageOrders.RowMajor || (_B.S.IsContinuous && _B.S.NonSingletonDimensions == 1))) {

                            return multiply(_B.T, _A.T).T;

                        }
                        #region find the order which requires minimal memory copies
                        if (_A.S.NumberOfElements > _B.S.NumberOfElements) {
                            checkEnsureStorageOrders4multiply<float>(_A, _B); 
                        } else {
                            checkEnsureStorageOrders4multiply<float>(_B, _A);
                        }
                        #endregion
                        // restart
                        return multiply(_A, _B);

                    } else {
                        //managed, blocked MatMult
                        #region storage orders
                        if (!_A.Size.IsContinuous) {
                            _A.Storage.EnsureStorageOrder(Settings.DefaultStorageOrder);
                        }
                        if (!_B.Size.IsContinuous) {
                            _B.Storage.EnsureStorageOrder(Settings.DefaultStorageOrder);
                        }
                        #endregion
                        Array<float> ret = zeros<float>(size(_A.Size[0], _B.Size[1]), StorageOrders.ColumnMajor);
                        var pA = (float*)_A.GetHostPointerForRead();
                        var pB = (float*)_B.GetHostPointerForRead();
                        var pC = (float*)ret.GetHostPointerForWrite();
                        ILNumerics.Core.LAPACK.Helper.MMultBlocked(
                            pA, pB, pC,
                            _A.S[0], _B.S[1], _A.S[1], Settings.ManagedMultiplyBlockSize,
                            _A.Size.GetStride(0), _A.Size.GetStride(1), _B.Size.GetStride(0), _B.Size.GetStride(1));
                        return ret;
                    }
                }
            }
        }

        /// <summary>
        /// Matrix multiplication for 3 general matrices (2D). Performs A ** (B ** C). 
        /// </summary>
        /// <param name="A">Input matrix A.</param>
        /// <param name="B">Input matrix B.</param>
        /// <param name="C">Input matrix C.</param>
        /// <returns>Matrix with result of <paramref name="A"/> ** (<paramref name="B"/> ** <paramref name="C"/>), where ** denotes matrix multiplication.</returns>
        /// <remarks><para>All arrays must be matrices with matching dimension length. Watch the oder of computations: <paramref name="B"/> is 
        /// multiplied with <paramref name="C"/> first. The result is right multiplied with <paramref name="A"/>. </para>
        /// <para>The multiplication will be carried out inside optimized native code, if possible. Otherwise, an optimized, blocked, managed version is used. </para>
        /// <para>Be prepared that this function may alter the storage order (<see cref="Size.StorageOrder"/>) of any input parameter without notice.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If at least one of <paramref name="A"/>, <paramref name="B"/> or <paramref name="C"/> is not a matrix or does 
        /// not have a matching shape.</exception>
        public static Array<fcomplex> multiply(InArray<fcomplex> A, InArray<fcomplex> B, InArray<fcomplex> C) {
            return multiply(multiply(A, B), C);
        }
        /// <summary>
        /// Matrix multiplication for general matrices (2D). 
        /// </summary>
        /// <param name="A">Input matrix A.</param>
        /// <param name="B">Input matrix B.</param>
        /// <returns>Matrix of size A.S[0] x B.S[1] with result of multiplying the matrices <paramref name="A"/> and <paramref name="B"/>.</returns>
        /// <remarks><para>Both arrays must be matrices with matching dimension length. This is: the number of rows 
        /// of <paramref name="B"/> must equal the number of columns of <paramref name="A"/>. </para>
        /// The multiplication will be carried out inside optimized native code, if possible. Otherwise, an optimized, blocked, managed version is used.  
        /// <para>Be prepared that this function may alter the storage order (<see cref="Size.StorageOrder"/>) of <paramref name="A"/> and/or <paramref name="B"/> without notice.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If at least one of <paramref name="A"/> or <paramref name="B"/> is not a matrix or does 
        /// not have a matching shape.</exception>
        public static Array<fcomplex> multiply(InArray<fcomplex> A, InArray<fcomplex> B) {
            using (Scope.Enter(A, B)) {
                if (isnull(A) || A.Size.NumberOfElements != A.S[0] * A.S[1]) {
                    throw new ArgumentException("A must be a a non-null matrix in order to be multiplied by B.");
                }
                if (isnull(B) || B.Size.NumberOfElements != B.S[0] * B.S[1]) {
                    throw new ArgumentException("B must be a non-null matrix in order to be multiplied by A.");
                }
                if (A.Size[1] != B.Size[0])
                    throw new ArgumentException("Inner matrix dimensions must match for matrix multiply.");

                Array<fcomplex> _A = A;
                Array<fcomplex> _B = B; 
                unsafe {
                    if (Lapack != null && Lapack is LapackMKL10_0) {

                        // doing  ?GEMM in MKL
                        /* 
                         * We use the FORTRAN interface only. The natural storage order for the BLAS3 FORTRAN 
                         * interaface is ColumnMajor. But we can use it for row major matrices as well. All 
                         * what is required is to transpose the matrices (is performed virtually) and flip 
                         * arguments of the ?gemm call. This gives the transpose of the result which is 
                         * transposed again for the return value. 
                         * 
                         * In order to bring input into continous storage order we consider the larger of _A 
                         * or _B first. Checks are performed on both matrices appropriately. If no advantageous 
                         * storage order can be derived from the existing matrices, Settings:DefaultStorageOrder 
                         * is used. 
                         */

                        if ((_A.S.StorageOrder == StorageOrders.ColumnMajor || (_A.S.IsContinuous && _A.S.NonSingletonDimensions == 1))
                           && (_B.S.StorageOrder == StorageOrders.ColumnMajor || (_B.S.IsContinuous && _B.S.NonSingletonDimensions == 1))) {

                            Array<fcomplex> ret = zeros<fcomplex>(size(_A.Size[0], _B.Size[1]), StorageOrders.ColumnMajor);
                            fcomplex* pA = (fcomplex*)_A.GetHostPointerForRead();
                            fcomplex* pB = (fcomplex*)_B.GetHostPointerForRead();
                            fcomplex* pC = (fcomplex*)ret.GetHostPointerForWrite();

                            Lapack.cgemm('N', 'N', (int)_A.Size[0], (int)_B.Size[1], (int)_A.Size[1], (fcomplex)1.0, pA, (int)_A.Size[0], pB, (int)_B.Size[0], (fcomplex)1.0, pC, (int)_A.Size[0]);
                            return ret;

                        } else if ((_A.S.StorageOrder == StorageOrders.RowMajor || (_A.S.IsContinuous && _A.S.NonSingletonDimensions == 1))
                            && (_B.S.StorageOrder == StorageOrders.RowMajor || (_B.S.IsContinuous && _B.S.NonSingletonDimensions == 1))) {

                            return multiply(_B.T, _A.T).T;

                        }
                        #region find the order which requires minimal memory copies
                        if (_A.S.NumberOfElements > _B.S.NumberOfElements) {
                            checkEnsureStorageOrders4multiply<fcomplex>(_A, _B); 
                        } else {
                            checkEnsureStorageOrders4multiply<fcomplex>(_B, _A);
                        }
                        #endregion
                        // restart
                        return multiply(_A, _B);

                    } else {
                        //managed, blocked MatMult
                        #region storage orders
                        if (!_A.Size.IsContinuous) {
                            _A.Storage.EnsureStorageOrder(Settings.DefaultStorageOrder);
                        }
                        if (!_B.Size.IsContinuous) {
                            _B.Storage.EnsureStorageOrder(Settings.DefaultStorageOrder);
                        }
                        #endregion
                        Array<fcomplex> ret = zeros<fcomplex>(size(_A.Size[0], _B.Size[1]), StorageOrders.ColumnMajor);
                        var pA = (fcomplex*)_A.GetHostPointerForRead();
                        var pB = (fcomplex*)_B.GetHostPointerForRead();
                        var pC = (fcomplex*)ret.GetHostPointerForWrite();
                        ILNumerics.Core.LAPACK.Helper.MMultBlocked(
                            pA, pB, pC,
                            _A.S[0], _B.S[1], _A.S[1], Settings.ManagedMultiplyBlockSize,
                            _A.Size.GetStride(0), _A.Size.GetStride(1), _B.Size.GetStride(0), _B.Size.GetStride(1));
                        return ret;
                    }
                }
            }
        }

        /// <summary>
        /// Matrix multiplication for 3 general matrices (2D). Performs A ** (B ** C). 
        /// </summary>
        /// <param name="A">Input matrix A.</param>
        /// <param name="B">Input matrix B.</param>
        /// <param name="C">Input matrix C.</param>
        /// <returns>Matrix with result of <paramref name="A"/> ** (<paramref name="B"/> ** <paramref name="C"/>), where ** denotes matrix multiplication.</returns>
        /// <remarks><para>All arrays must be matrices with matching dimension length. Watch the oder of computations: <paramref name="B"/> is 
        /// multiplied with <paramref name="C"/> first. The result is right multiplied with <paramref name="A"/>. </para>
        /// <para>The multiplication will be carried out inside optimized native code, if possible. Otherwise, an optimized, blocked, managed version is used. </para>
        /// <para>Be prepared that this function may alter the storage order (<see cref="Size.StorageOrder"/>) of any input parameter without notice.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If at least one of <paramref name="A"/>, <paramref name="B"/> or <paramref name="C"/> is not a matrix or does 
        /// not have a matching shape.</exception>
        public static Array<complex> multiply(InArray<complex> A, InArray<complex> B, InArray<complex> C) {
            return multiply(multiply(A, B), C);
        }
        /// <summary>
        /// Matrix multiplication for general matrices (2D). 
        /// </summary>
        /// <param name="A">Input matrix A.</param>
        /// <param name="B">Input matrix B.</param>
        /// <returns>Matrix of size A.S[0] x B.S[1] with result of multiplying the matrices <paramref name="A"/> and <paramref name="B"/>.</returns>
        /// <remarks><para>Both arrays must be matrices with matching dimension length. This is: the number of rows 
        /// of <paramref name="B"/> must equal the number of columns of <paramref name="A"/>. </para>
        /// The multiplication will be carried out inside optimized native code, if possible. Otherwise, an optimized, blocked, managed version is used.  
        /// <para>Be prepared that this function may alter the storage order (<see cref="Size.StorageOrder"/>) of <paramref name="A"/> and/or <paramref name="B"/> without notice.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">If at least one of <paramref name="A"/> or <paramref name="B"/> is not a matrix or does 
        /// not have a matching shape.</exception>
        public static Array<complex> multiply(InArray<complex> A, InArray<complex> B) {
            using (Scope.Enter(A, B)) {
                if (isnull(A) || A.Size.NumberOfElements != A.S[0] * A.S[1]) {
                    throw new ArgumentException("A must be a a non-null matrix in order to be multiplied by B.");
                }
                if (isnull(B) || B.Size.NumberOfElements != B.S[0] * B.S[1]) {
                    throw new ArgumentException("B must be a non-null matrix in order to be multiplied by A.");
                }
                if (A.Size[1] != B.Size[0])
                    throw new ArgumentException("Inner matrix dimensions must match for matrix multiply.");

                Array<complex> _A = A;
                Array<complex> _B = B; 
                unsafe {
                    if (Lapack != null && Lapack is LapackMKL10_0) {

                        // doing  ?GEMM in MKL
                        /* 
                         * We use the FORTRAN interface only. The natural storage order for the BLAS3 FORTRAN 
                         * interaface is ColumnMajor. But we can use it for row major matrices as well. All 
                         * what is required is to transpose the matrices (is performed virtually) and flip 
                         * arguments of the ?gemm call. This gives the transpose of the result which is 
                         * transposed again for the return value. 
                         * 
                         * In order to bring input into continous storage order we consider the larger of _A 
                         * or _B first. Checks are performed on both matrices appropriately. If no advantageous 
                         * storage order can be derived from the existing matrices, Settings:DefaultStorageOrder 
                         * is used. 
                         */

                        if ((_A.S.StorageOrder == StorageOrders.ColumnMajor || (_A.S.IsContinuous && _A.S.NonSingletonDimensions == 1))
                           && (_B.S.StorageOrder == StorageOrders.ColumnMajor || (_B.S.IsContinuous && _B.S.NonSingletonDimensions == 1))) {

                            Array<complex> ret = zeros<complex>(size(_A.Size[0], _B.Size[1]), StorageOrders.ColumnMajor);
                            complex* pA = (complex*)_A.GetHostPointerForRead();
                            complex* pB = (complex*)_B.GetHostPointerForRead();
                            complex* pC = (complex*)ret.GetHostPointerForWrite();

                            Lapack.zgemm('N', 'N', (int)_A.Size[0], (int)_B.Size[1], (int)_A.Size[1], (complex)1.0, pA, (int)_A.Size[0], pB, (int)_B.Size[0], (complex)1.0, pC, (int)_A.Size[0]);
                            return ret;

                        } else if ((_A.S.StorageOrder == StorageOrders.RowMajor || (_A.S.IsContinuous && _A.S.NonSingletonDimensions == 1))
                            && (_B.S.StorageOrder == StorageOrders.RowMajor || (_B.S.IsContinuous && _B.S.NonSingletonDimensions == 1))) {

                            return multiply(_B.T, _A.T).T;

                        }
                        #region find the order which requires minimal memory copies
                        if (_A.S.NumberOfElements > _B.S.NumberOfElements) {
                            checkEnsureStorageOrders4multiply<complex>(_A, _B); 
                        } else {
                            checkEnsureStorageOrders4multiply<complex>(_B, _A);
                        }
                        #endregion
                        // restart
                        return multiply(_A, _B);

                    } else {
                        //managed, blocked MatMult
                        #region storage orders
                        if (!_A.Size.IsContinuous) {
                            _A.Storage.EnsureStorageOrder(Settings.DefaultStorageOrder);
                        }
                        if (!_B.Size.IsContinuous) {
                            _B.Storage.EnsureStorageOrder(Settings.DefaultStorageOrder);
                        }
                        #endregion
                        Array<complex> ret = zeros<complex>(size(_A.Size[0], _B.Size[1]), StorageOrders.ColumnMajor);
                        var pA = (complex*)_A.GetHostPointerForRead();
                        var pB = (complex*)_B.GetHostPointerForRead();
                        var pC = (complex*)ret.GetHostPointerForWrite();
                        ILNumerics.Core.LAPACK.Helper.MMultBlocked(
                            pA, pB, pC,
                            _A.S[0], _B.S[1], _A.S[1], Settings.ManagedMultiplyBlockSize,
                            _A.Size.GetStride(0), _A.Size.GetStride(1), _B.Size.GetStride(0), _B.Size.GetStride(1));
                        return ret;
                    }
                }
            }
        }


#endregion HYCALPER AUTO GENERATED CODE

        #region helpers

        /// <summary>
        /// Brings both matrices into the same storage order, as required for (native) GEMM.
        /// </summary>
        /// <typeparam name="T">Generic element type, not used.</typeparam>
        /// <param name="large">The larger of both matrices.</param>
        /// <param name="small">The smaller of both matrices.</param>
        /// <remarks>A copy is done in case that the storage's memory is shared with other storages. The number of arrays sharing the storage
        /// is not considered.</remarks>
        private static void checkEnsureStorageOrders4multiply<T>(OutArray<T> large, OutArray<T> small) {
            // no scoping required - private method, only called from multiply
            // No atomic mutation required either. We change local arrays in callers scope only (multiply()). 
            // They are not exposed to other threads.
            if (!large.S.IsContinuous) {
                if (small.S.IsContinuous) {
                    large.Storage.EnsureStorageOrder(small.S.StorageOrder, forceCopy: large.Storage.Handles.ReferenceCount > 1, inplace: true);
                } else {
                    large.Storage.EnsureStorageOrder(Settings.DefaultStorageOrder, forceCopy: large.Storage.Handles.ReferenceCount > 1, inplace: true);
                    small.Storage.EnsureStorageOrder(Settings.DefaultStorageOrder, forceCopy: small.Storage.Handles.ReferenceCount > 1, inplace: true);
                }
            } else {
                System.Diagnostics.Debug.Assert(small.S.StorageOrder != large.S.StorageOrder, "Two matrices with same storage order should have already been handled earlier?!");
                small.Storage.EnsureStorageOrder(large.S.StorageOrder, forceCopy: small.Storage.Handles.ReferenceCount > 1, inplace: true);
            }
        }

        #endregion
    }
}
