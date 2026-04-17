using System;
using static ILNumerics.Globals;

namespace ILNumerics {
    public static partial class ILMath {

        /// <summary>
        /// Copy upper triangle from PHYSICAL array A (used internally).
        /// </summary>
        /// <typeparam name="T">Arbitrary inner type </typeparam>
        /// <param name="A">PHYSICAL Array</param>
        /// <param name="m">Number of rows</param>
        /// <param name="n">Number of columns</param>
        /// <returns>Newly created physical array with the upper triangle of A</returns>
        /// <remarks>No checks are made for m,n fit inside A!</remarks>
        private static Array<T> copyUpperTriangle<T>(InArray<T> A, long m, long n) {
            using (Scope.Enter(A)) {
                Array<T> ret = zeros<T>(m, n, StorageOrders.ColumnMajor);
                if (m > 0) {
                    System.Diagnostics.Debug.Assert(m <= A.S[0] && n <= A.S[1]);
                    System.Diagnostics.Debug.Assert(m >= 1);
                    System.Diagnostics.Debug.Assert(A.S.NumberOfDimensions <= 2);

                    for (long c = 0; c < n; c++) {
                        var lastID = (long)Math.Min(m - 1, c);
                        ret[r(0, lastID), c] = A[r(0, lastID), c];
                    }
                }
#if DEBUG
                Size.CheckSizeBroadcastableStrides(ret.S);
#endif
                return ret;
            }
        }

        /// <summary>
        /// Copy lower triangle from array <paramref name="A"/>, set diagonal to val (used internally).
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="A">Input array.</param>
        /// <param name="m">Number of rows.</param>
        /// <param name="n">Number of columns.</param>
        /// <param name="val">Value for diagonal entries.</param>
        /// <returns>Newly created array with the lower triangle of <paramref name="A"/>.</returns>
        /// <remarks>No checks are made for m,n fitting inside <paramref name="A"/>!</remarks>
        private unsafe static Array<T> copyLowerTriangle<T>(InArray<T> A, long m, long n, T val) {

            using (Scope.Enter(A)) {
                Array<T> ret = zeros<T>(m, n, StorageOrders.ColumnMajor);

                for (long c = 0; c < n; c++) {
                    ret.SetValue(val, c, c);
                    if (c + 1 < m) {
                        ret[r(c + 1, end), c] = A[r(c + 1, end), c];
                    }
                }
                return ret;
            }
        }
        /// <summary>
        /// Copy lower triangle from PHYSICAL array A, set diagonal to val, permuted version
        /// </summary>
        /// <typeparam name="T">Arbitrary inner type </typeparam>
        /// <param name="A">PHYSICAL Array</param>
        /// <param name="m">Number of rows</param>
        /// <param name="n">Number of columns</param>
        /// <param name="perm">Mapping for rows, must be converted fom LAPACK version to single indices, 0-based. Length: <paramref name="m"/>.</param>
        /// <param name="val">Value for diagonal entries</param>
        /// <returns>Newly created physical array with the lower triangle of A</returns>
        /// <remarks>No checks are made for m,n fitting inside A!</remarks>
        private unsafe static Array<T> copyLowerTrianglePerm<T>(InArray<T> A, uint m, uint n, T val, int* perm) {

            using (Scope.Enter(A)) {
                Array<T> ret = zeros<T>(m, n, StorageOrders.ColumnMajor);
                if (m == 0 || n == 0) return ret; 

                uint trueRow;
                //ret.SetValue(val, 0, 0);
                for (uint r = 0; r < m; r++) {
                    trueRow = (uint)perm[r];
                    if (trueRow > 0) {
                        ret[r, slice(0, trueRow)] = A[trueRow, slice(0, trueRow)];
                    }
                    ret.SetValue(val, r, trueRow);
                }
                return ret;
            }
        }

        /// <summary>
        /// Relabel permutation indices from LAPACK ?getrf
        /// </summary>
        /// <param name="perm">Lapack pivoting permutation array</param>
        /// <param name="outPerm">[Output] Index mapping for direct addressing the rows </param>
        /// <param name="lenPerm">Minimal lengths of array <paramref name="perm"/>.</param>
        /// <param name="lenOutPerm">Minimal lengths of array <paramref name="outPerm"/>.</param>
        /// <remarks>Exchange the row labels in the same manner as LAPACK did for pivoting</remarks>
        private unsafe static void perm2indicesForward(int* perm, int* outPerm, uint lenPerm, uint lenOutPerm) {

            // buffer can be non-cleared
            for (int i = 0; i < lenOutPerm; i++) {
                outPerm[i] = i;
            }
            int tmp;
            for (int i = 0; i < lenPerm; i++) {
                if (perm[i] != i + 1) {
                    tmp = outPerm[perm[i] - 1];
                    outPerm[perm[i] - 1] = i;
                    outPerm[i] = tmp;
                }
            }
        }
        /// <summary>
        /// Relabel permutation indices from LAPACK ?getrf - backward version
        /// </summary>
        /// <param name="perm">Lapack pivoting permutation array</param>
        /// <param name="outPerm">[Output] Index mapping for direct addressing the rows</param>
        /// <param name="lenOutPerm">length of array <paramref name="outPerm"/>.</param>
        /// <param name="lenPerm">length of array <paramref name="perm"/>.</param>
        /// <remarks>Exchange the row labels in the same manner as LAPACK did for pivoting. Backward version.</remarks>
        private unsafe static void perm2indicesBackward(int* perm, int* outPerm, uint lenPerm, uint lenOutPerm) {
            if (lenOutPerm == 0) return; 
            // buffer can be non-cleared 
            for (uint i = 0; i < lenOutPerm; i++) {
                outPerm[i] = (int)i;
            }
            for (uint i = lenPerm; i-- > 0;) {
                if (perm[i] != i + 1) {
                    int tmp = outPerm[perm[i] - 1];
                    outPerm[perm[i] - 1] = (int)i;
                    outPerm[i] = tmp;
                }
            }
        }

    }
}
