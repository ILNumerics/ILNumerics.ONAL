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
using ILNumerics.Core;
using ILNumerics.Core.MemoryLayer;
using System;
using System.Security;
using static ILNumerics.Core.Functions.Builtin.MathInternal;

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
        Double
    </source>
    <destination>double</destination>
    <destination>float</destination>
    <destination>float</destination>
</type>
<type>
    <source locate="here">
        dgesdd
    </source>
    <destination>zgesdd</destination>
    <destination>cgesdd</destination>
    <destination>sgesdd</destination>
</type>
<type>
    <source locate="nextline">
        complxConj
    </source>
    <destination><![CDATA[outV.a = conj(outV).T;]]></destination>
    <destination><![CDATA[outV.a = conj(outV).T;]]></destination>
    <destination><![CDATA[outV.a = outV.T;]]></destination>
</type>
</hycalper>
*/

namespace ILNumerics {

    public partial class ILMath {

        #region HYCALPER LOOPSTART
        /// <summary>
        /// Singular value decomposition. 
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Vector with min(M,N) singular values of <paramref name="A"/>, where A.S is [M,N].</returns>
        public static Array<Double> svd(InArray<double> A) {
            return svd(A, null, null, false, false);
        }
        /// <summary>
        /// Singular value decomposition. 
        /// </summary>
        /// <param name="A">Input matrix, size [M,N]</param>
        /// <param name="U">[Output, Optional] Returns left singular vectors of <paramref name="A"/> as columns. Default: (null) do not compute.</param>
        /// <returns>Singluar values as real diagonal matrix of same size and precision as <paramref name="A"/>.</returns>
        public static Array<Double> svd(InArray<double> A, OutArray<double> U = null) {
            return svd(A, U, null, false, false);
        }
        /// <summary>
        /// Singular value decomposition. 
        /// </summary>
        /// <param name="A">Input matrix. Size [M,N].</param>
        /// <param name="outU">[Optional, output] Returns left singular vectors of <paramref name="A"/> as columns. Default: (null) do not compute.</param>
        /// <param name="small">If true: return only first min(M,N) singular values. Reduces <paramref name="outU"/> to size [min(M,N),min(M,N)]. Default: false.</param>
        /// <returns>Singluar values as real diagonal matrix of same size and precision as <paramref name="A"/>.</returns>
        public static Array<Double> svd(InArray<double> A, OutArray<double> outU = null, bool small = false) {
            return svd(A, outU, null, small, false);
        }
        /// <summary>
        /// Singular value decomposition. 
        /// </summary>
        /// <param name="A">Input matrix. Size [M,N].</param>
        /// <param name="outU">[Output] Returns left singular vectors of <paramref name="A"/> as columns. Must be non-null on entry.</param>
        /// <param name="outV">[Output] Returns right singular vectors of <paramref name="A"/> as rows of matrix V. Must be non-null on entry and will be replaced with new values on return.</param>
        /// <returns>Singluar values as real diagonal matrix of same size and precision as <paramref name="A"/>.</returns>
        public static Array<Double> svd(InArray<double> A, OutArray<double> outU, OutArray<double> outV) {
            return svd(A, outU, outV, false, false);
        }
        /// <summary>
        /// Singular value decomposition. 
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <param name="outU">[Output] Left singular vectors of <paramref name="A"/> as columns. Provide null if this information is not required.</param>
        /// <param name="outV">[Output] Right singular vectors of <paramref name="A"/> as rows. Must be non-null on entry and will be replaced with new values on return.</param>
        /// <param name="small">If true: return only the first min(M,N) singlular values. Decreases the size of the array returned to [min(M,N),min(M,N)].</param>
        /// <param name="discardFiniteTest">true: the matrix given will not be checked for infinte or NaN values. If such elements 
        /// exist, convergence failure or an error might occur. Use with care! </param>
        /// <returns>Singluar values as real diagonal matrix of same size and precicion as <paramref name="A"/>.</returns>
        
        public unsafe static Array<Double> svd(InArray<double> A, OutArray<double> outU,
                                           OutArray<double> outV, bool small, bool discardFiniteTest) {
            if (object.Equals(A, null))
                throw new ArgumentNullException(nameof(A));
            using (Scope.Enter()) {

                Array<double> _A = A;

                if (!_A.IsMatrix)
                    throw new ArgumentException("svd() is only defined for matrices.");
                // early exit for small matrices
                if (_A.Size[1] < 4 && _A.Size[0] == _A.Size[1]) {
                    switch (_A.Size[0]) {
                        case 1:
                            if (!Object.Equals(outU, null))
                                outU.a = (/*!HC:outArrU*/ double)1.0;
                            if (!Object.Equals(outV, null))
                                outV.a = (/*!HC:outArrV*/ double)1.0;
                            return abs(_A);
                            //case 2:
                            //    return -1; 
                            //case 3: 
                            //    return -1; 
                    }
                }
                if (!discardFiniteTest && !(bool)allall(isfinite(_A)))
                    throw new ArgumentException("svd(): input must have only finite elements.");
                if (Lapack == null)
                    throw new InvalidOperationException("svd() requires a LAPACK package. No LAPACK found.");
                // parameter evaluation
                long M = _A.Size[0];
                long N = _A.Size[1];
                long minMN = (M < N) ? M : N;
                int LDU = (int)M; int LDVT = (int)N;
                int LDA = (int)M, lenDU = 0, lenVT = 0;
                MemoryHandle  dS = New<Double>((ulong)minMN), dU = null, dVT = null;
                var pS = (Double*)dS.Pointer; 

                char jobz = (small) ? 'S' : 'A';

                int info = 0;
                try {
                    if (!Object.Equals(outU, null) || !Object.Equals(outV, null)) {
                        // need to return U and VT 
                        if (small) {
                            lenDU = (int)(M * minMN);
                            lenVT = (int)(N * minMN);
                            LDVT = (int)minMN;
                        } else {
                            lenDU = (int)(M * M);
                            lenVT = (int)(N * N);
                        }
                        dU = New<double>((ulong)lenDU);
                        dVT = New<double>((ulong)lenVT);
                    } else {
                        jobz = 'N';
                    }

                    // must create copy of input ! 
                    Array<double> Acopy = _A.C;

                    var dInput = (double*)Acopy.GetHostPointerForWrite(StorageOrders.ColumnMajor);

                    Lapack.dgesdd(jobz, (int)M, (int)N, dInput, LDA, pS,
                        dU != null ? (double*)dU.Pointer : null, LDU,
                        dVT != null ? (double*)dVT.Pointer : null, LDVT, ref info);
                    GC.KeepAlive(Acopy); 

                    if (info < 0)
                        throw new ArgumentException("Internal error: the " + (-info).ToString() + "-th argument to ?gesdd (LAPACK) was invalid. Please contact ILNumerics!");
                    if (info > 0)
                        throw new ArgumentException("svd() was not converging in LAPACK's ?gesdd.");

                    Array<Double> ret = null;
                    if (info == 0) {
                        // success
                        if (!Object.Equals(outU, null) || !Object.Equals(outV, null)) {
                            if (small) {
                                ret = zeros<Double>(minMN, minMN);
                            } else {
                                ret = zeros<Double>(M, N);
                            }
                            for (int i = 0; i < minMN; i++) {
                                ret.SetValue(pS[i], i, i);
                            }
                            if (!Object.Equals(outU, null)) {
                                var storage = Core.StorageLayer.Storage<double>.Create();
                                storage.S.SetAll(M, lenDU / M, StorageOrders.ColumnMajor); 
                                storage.Handles[0] = dU;
                                dU = null; 
                                // was: outU.a = array<double>(dU, M, lenDU / M);
                                lock (outU.SynchObj)
                                    outU.Storage.Assign(storage, true);
                                storage.Dispose(); 
                            }
                            if (!Object.Equals(outV, null)) {
                                var storage = Core.StorageLayer.Storage<double>.Create();
                                storage.S.SetAll(N, lenVT / N, StorageOrders.ColumnMajor);
                                storage.Handles[0] = dVT;
                                dVT = null; 
                                outV.Storage.Assign(storage, true);
                                storage.Dispose(); 
                                /*!HC:complxConj*/
                                outV.a = outV.T;
                            }
                        } else {
                            var storage = Core.StorageLayer.Storage<Double>.Create();
                            storage.S.SetAll(minMN, 1, StorageOrders.ColumnMajor);
                            storage.Handles[0] = dS;
                            ret = storage.RetArray;
                            dS = null; 
                        }
                    }
                    return ret;
                } finally {
                    if (dS != null) free<Double>(dS,0);
                    if (dU != null) free<double>(dU,0);
                    if (dVT != null) free<double>(dVT,0);
                }
            }
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
        /// <summary>
        /// Singular value decomposition. 
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Vector with min(M,N) singular values of <paramref name="A"/>, where A.S is [M,N].</returns>
        public static Array<float> svd(InArray<float> A) {
            return svd(A, null, null, false, false);
        }
        /// <summary>
        /// Singular value decomposition. 
        /// </summary>
        /// <param name="A">Input matrix, size [M,N]</param>
        /// <param name="U">[Output, Optional] Returns left singular vectors of <paramref name="A"/> as columns. Default: (null) do not compute.</param>
        /// <returns>Singluar values as real diagonal matrix of same size and precision as <paramref name="A"/>.</returns>
        public static Array<float> svd(InArray<float> A, OutArray<float> U = null) {
            return svd(A, U, null, false, false);
        }
        /// <summary>
        /// Singular value decomposition. 
        /// </summary>
        /// <param name="A">Input matrix. Size [M,N].</param>
        /// <param name="outU">[Optional, output] Returns left singular vectors of <paramref name="A"/> as columns. Default: (null) do not compute.</param>
        /// <param name="small">If true: return only first min(M,N) singular values. Reduces <paramref name="outU"/> to size [min(M,N),min(M,N)]. Default: false.</param>
        /// <returns>Singluar values as real diagonal matrix of same size and precision as <paramref name="A"/>.</returns>
        public static Array<float> svd(InArray<float> A, OutArray<float> outU = null, bool small = false) {
            return svd(A, outU, null, small, false);
        }
        /// <summary>
        /// Singular value decomposition. 
        /// </summary>
        /// <param name="A">Input matrix. Size [M,N].</param>
        /// <param name="outU">[Output] Returns left singular vectors of <paramref name="A"/> as columns. Must be non-null on entry.</param>
        /// <param name="outV">[Output] Returns right singular vectors of <paramref name="A"/> as rows of matrix V. Must be non-null on entry and will be replaced with new values on return.</param>
        /// <returns>Singluar values as real diagonal matrix of same size and precision as <paramref name="A"/>.</returns>
        public static Array<float> svd(InArray<float> A, OutArray<float> outU, OutArray<float> outV) {
            return svd(A, outU, outV, false, false);
        }
        /// <summary>
        /// Singular value decomposition. 
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <param name="outU">[Output] Left singular vectors of <paramref name="A"/> as columns. Provide null if this information is not required.</param>
        /// <param name="outV">[Output] Right singular vectors of <paramref name="A"/> as rows. Must be non-null on entry and will be replaced with new values on return.</param>
        /// <param name="small">If true: return only the first min(M,N) singlular values. Decreases the size of the array returned to [min(M,N),min(M,N)].</param>
        /// <param name="discardFiniteTest">true: the matrix given will not be checked for infinte or NaN values. If such elements 
        /// exist, convergence failure or an error might occur. Use with care! </param>
        /// <returns>Singluar values as real diagonal matrix of same size and precicion as <paramref name="A"/>.</returns>
        
        public unsafe static Array<float> svd(InArray<float> A, OutArray<float> outU,
                                           OutArray<float> outV, bool small, bool discardFiniteTest) {
            if (object.Equals(A, null))
                throw new ArgumentNullException(nameof(A));
            using (Scope.Enter()) {

                Array<float> _A = A;

                if (!_A.IsMatrix)
                    throw new ArgumentException("svd() is only defined for matrices.");
                // early exit for small matrices
                if (_A.Size[1] < 4 && _A.Size[0] == _A.Size[1]) {
                    switch (_A.Size[0]) {
                        case 1:
                            if (!Object.Equals(outU, null))
                                outU.a = ( float)1.0;
                            if (!Object.Equals(outV, null))
                                outV.a = ( float)1.0;
                            return abs(_A);
                            //case 2:
                            //    return -1; 
                            //case 3: 
                            //    return -1; 
                    }
                }
                if (!discardFiniteTest && !(bool)allall(isfinite(_A)))
                    throw new ArgumentException("svd(): input must have only finite elements.");
                if (Lapack == null)
                    throw new InvalidOperationException("svd() requires a LAPACK package. No LAPACK found.");
                // parameter evaluation
                long M = _A.Size[0];
                long N = _A.Size[1];
                long minMN = (M < N) ? M : N;
                int LDU = (int)M; int LDVT = (int)N;
                int LDA = (int)M, lenDU = 0, lenVT = 0;
                MemoryHandle  dS = New<float>((ulong)minMN), dU = null, dVT = null;
                var pS = (float*)dS.Pointer; 

                char jobz = (small) ? 'S' : 'A';

                int info = 0;
                try {
                    if (!Object.Equals(outU, null) || !Object.Equals(outV, null)) {
                        // need to return U and VT 
                        if (small) {
                            lenDU = (int)(M * minMN);
                            lenVT = (int)(N * minMN);
                            LDVT = (int)minMN;
                        } else {
                            lenDU = (int)(M * M);
                            lenVT = (int)(N * N);
                        }
                        dU = New<float>((ulong)lenDU);
                        dVT = New<float>((ulong)lenVT);
                    } else {
                        jobz = 'N';
                    }

                    // must create copy of input ! 
                    Array<float> Acopy = _A.C;

                    var dInput = (float*)Acopy.GetHostPointerForWrite(StorageOrders.ColumnMajor);

                    Lapack.sgesdd(jobz, (int)M, (int)N, dInput, LDA, pS,
                        dU != null ? (float*)dU.Pointer : null, LDU,
                        dVT != null ? (float*)dVT.Pointer : null, LDVT, ref info);
                    GC.KeepAlive(Acopy); 

                    if (info < 0)
                        throw new ArgumentException("Internal error: the " + (-info).ToString() + "-th argument to ?gesdd (LAPACK) was invalid. Please contact ILNumerics!");
                    if (info > 0)
                        throw new ArgumentException("svd() was not converging in LAPACK's ?gesdd.");

                    Array<float> ret = null;
                    if (info == 0) {
                        // success
                        if (!Object.Equals(outU, null) || !Object.Equals(outV, null)) {
                            if (small) {
                                ret = zeros<float>(minMN, minMN);
                            } else {
                                ret = zeros<float>(M, N);
                            }
                            for (int i = 0; i < minMN; i++) {
                                ret.SetValue(pS[i], i, i);
                            }
                            if (!Object.Equals(outU, null)) {
                                var storage = Core.StorageLayer.Storage<float>.Create();
                                storage.S.SetAll(M, lenDU / M, StorageOrders.ColumnMajor); 
                                storage.Handles[0] = dU;
                                dU = null; 
                                // was: outU.a = array<float>(dU, M, lenDU / M);
                                lock (outU.SynchObj)
                                    outU.Storage.Assign(storage, true);
                                storage.Dispose(); 
                            }
                            if (!Object.Equals(outV, null)) {
                                var storage = Core.StorageLayer.Storage<float>.Create();
                                storage.S.SetAll(N, lenVT / N, StorageOrders.ColumnMajor);
                                storage.Handles[0] = dVT;
                                dVT = null; 
                                outV.Storage.Assign(storage, true);
                                storage.Dispose(); 
                                outV.a = outV.T;
                            }
                        } else {
                            var storage = Core.StorageLayer.Storage<float>.Create();
                            storage.S.SetAll(minMN, 1, StorageOrders.ColumnMajor);
                            storage.Handles[0] = dS;
                            ret = storage.RetArray;
                            dS = null; 
                        }
                    }
                    return ret;
                } finally {
                    if (dS != null) free<float>(dS,0);
                    if (dU != null) free<float>(dU,0);
                    if (dVT != null) free<float>(dVT,0);
                }
            }
        }
        /// <summary>
        /// Singular value decomposition. 
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Vector with min(M,N) singular values of <paramref name="A"/>, where A.S is [M,N].</returns>
        public static Array<float> svd(InArray<fcomplex> A) {
            return svd(A, null, null, false, false);
        }
        /// <summary>
        /// Singular value decomposition. 
        /// </summary>
        /// <param name="A">Input matrix, size [M,N]</param>
        /// <param name="U">[Output, Optional] Returns left singular vectors of <paramref name="A"/> as columns. Default: (null) do not compute.</param>
        /// <returns>Singluar values as real diagonal matrix of same size and precision as <paramref name="A"/>.</returns>
        public static Array<float> svd(InArray<fcomplex> A, OutArray<fcomplex> U = null) {
            return svd(A, U, null, false, false);
        }
        /// <summary>
        /// Singular value decomposition. 
        /// </summary>
        /// <param name="A">Input matrix. Size [M,N].</param>
        /// <param name="outU">[Optional, output] Returns left singular vectors of <paramref name="A"/> as columns. Default: (null) do not compute.</param>
        /// <param name="small">If true: return only first min(M,N) singular values. Reduces <paramref name="outU"/> to size [min(M,N),min(M,N)]. Default: false.</param>
        /// <returns>Singluar values as real diagonal matrix of same size and precision as <paramref name="A"/>.</returns>
        public static Array<float> svd(InArray<fcomplex> A, OutArray<fcomplex> outU = null, bool small = false) {
            return svd(A, outU, null, small, false);
        }
        /// <summary>
        /// Singular value decomposition. 
        /// </summary>
        /// <param name="A">Input matrix. Size [M,N].</param>
        /// <param name="outU">[Output] Returns left singular vectors of <paramref name="A"/> as columns. Must be non-null on entry.</param>
        /// <param name="outV">[Output] Returns right singular vectors of <paramref name="A"/> as rows of matrix V. Must be non-null on entry and will be replaced with new values on return.</param>
        /// <returns>Singluar values as real diagonal matrix of same size and precision as <paramref name="A"/>.</returns>
        public static Array<float> svd(InArray<fcomplex> A, OutArray<fcomplex> outU, OutArray<fcomplex> outV) {
            return svd(A, outU, outV, false, false);
        }
        /// <summary>
        /// Singular value decomposition. 
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <param name="outU">[Output] Left singular vectors of <paramref name="A"/> as columns. Provide null if this information is not required.</param>
        /// <param name="outV">[Output] Right singular vectors of <paramref name="A"/> as rows. Must be non-null on entry and will be replaced with new values on return.</param>
        /// <param name="small">If true: return only the first min(M,N) singlular values. Decreases the size of the array returned to [min(M,N),min(M,N)].</param>
        /// <param name="discardFiniteTest">true: the matrix given will not be checked for infinte or NaN values. If such elements 
        /// exist, convergence failure or an error might occur. Use with care! </param>
        /// <returns>Singluar values as real diagonal matrix of same size and precicion as <paramref name="A"/>.</returns>
        
        public unsafe static Array<float> svd(InArray<fcomplex> A, OutArray<fcomplex> outU,
                                           OutArray<fcomplex> outV, bool small, bool discardFiniteTest) {
            if (object.Equals(A, null))
                throw new ArgumentNullException(nameof(A));
            using (Scope.Enter()) {

                Array<fcomplex> _A = A;

                if (!_A.IsMatrix)
                    throw new ArgumentException("svd() is only defined for matrices.");
                // early exit for small matrices
                if (_A.Size[1] < 4 && _A.Size[0] == _A.Size[1]) {
                    switch (_A.Size[0]) {
                        case 1:
                            if (!Object.Equals(outU, null))
                                outU.a = ( fcomplex)1.0;
                            if (!Object.Equals(outV, null))
                                outV.a = ( fcomplex)1.0;
                            return abs(_A);
                            //case 2:
                            //    return -1; 
                            //case 3: 
                            //    return -1; 
                    }
                }
                if (!discardFiniteTest && !(bool)allall(isfinite(_A)))
                    throw new ArgumentException("svd(): input must have only finite elements.");
                if (Lapack == null)
                    throw new InvalidOperationException("svd() requires a LAPACK package. No LAPACK found.");
                // parameter evaluation
                long M = _A.Size[0];
                long N = _A.Size[1];
                long minMN = (M < N) ? M : N;
                int LDU = (int)M; int LDVT = (int)N;
                int LDA = (int)M, lenDU = 0, lenVT = 0;
                MemoryHandle  dS = New<float>((ulong)minMN), dU = null, dVT = null;
                var pS = (float*)dS.Pointer; 

                char jobz = (small) ? 'S' : 'A';

                int info = 0;
                try {
                    if (!Object.Equals(outU, null) || !Object.Equals(outV, null)) {
                        // need to return U and VT 
                        if (small) {
                            lenDU = (int)(M * minMN);
                            lenVT = (int)(N * minMN);
                            LDVT = (int)minMN;
                        } else {
                            lenDU = (int)(M * M);
                            lenVT = (int)(N * N);
                        }
                        dU = New<fcomplex>((ulong)lenDU);
                        dVT = New<fcomplex>((ulong)lenVT);
                    } else {
                        jobz = 'N';
                    }

                    // must create copy of input ! 
                    Array<fcomplex> Acopy = _A.C;

                    var dInput = (fcomplex*)Acopy.GetHostPointerForWrite(StorageOrders.ColumnMajor);

                    Lapack.cgesdd(jobz, (int)M, (int)N, dInput, LDA, pS,
                        dU != null ? (fcomplex*)dU.Pointer : null, LDU,
                        dVT != null ? (fcomplex*)dVT.Pointer : null, LDVT, ref info);
                    GC.KeepAlive(Acopy); 

                    if (info < 0)
                        throw new ArgumentException("Internal error: the " + (-info).ToString() + "-th argument to ?gesdd (LAPACK) was invalid. Please contact ILNumerics!");
                    if (info > 0)
                        throw new ArgumentException("svd() was not converging in LAPACK's ?gesdd.");

                    Array<float> ret = null;
                    if (info == 0) {
                        // success
                        if (!Object.Equals(outU, null) || !Object.Equals(outV, null)) {
                            if (small) {
                                ret = zeros<float>(minMN, minMN);
                            } else {
                                ret = zeros<float>(M, N);
                            }
                            for (int i = 0; i < minMN; i++) {
                                ret.SetValue(pS[i], i, i);
                            }
                            if (!Object.Equals(outU, null)) {
                                var storage = Core.StorageLayer.Storage<fcomplex>.Create();
                                storage.S.SetAll(M, lenDU / M, StorageOrders.ColumnMajor); 
                                storage.Handles[0] = dU;
                                dU = null; 
                                // was: outU.a = array<fcomplex>(dU, M, lenDU / M);
                                lock (outU.SynchObj)
                                    outU.Storage.Assign(storage, true);
                                storage.Dispose(); 
                            }
                            if (!Object.Equals(outV, null)) {
                                var storage = Core.StorageLayer.Storage<fcomplex>.Create();
                                storage.S.SetAll(N, lenVT / N, StorageOrders.ColumnMajor);
                                storage.Handles[0] = dVT;
                                dVT = null; 
                                outV.Storage.Assign(storage, true);
                                storage.Dispose(); 
                                outV.a = conj(outV).T;
                            }
                        } else {
                            var storage = Core.StorageLayer.Storage<float>.Create();
                            storage.S.SetAll(minMN, 1, StorageOrders.ColumnMajor);
                            storage.Handles[0] = dS;
                            ret = storage.RetArray;
                            dS = null; 
                        }
                    }
                    return ret;
                } finally {
                    if (dS != null) free<float>(dS,0);
                    if (dU != null) free<fcomplex>(dU,0);
                    if (dVT != null) free<fcomplex>(dVT,0);
                }
            }
        }
        /// <summary>
        /// Singular value decomposition. 
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <returns>Vector with min(M,N) singular values of <paramref name="A"/>, where A.S is [M,N].</returns>
        public static Array<double> svd(InArray<complex> A) {
            return svd(A, null, null, false, false);
        }
        /// <summary>
        /// Singular value decomposition. 
        /// </summary>
        /// <param name="A">Input matrix, size [M,N]</param>
        /// <param name="U">[Output, Optional] Returns left singular vectors of <paramref name="A"/> as columns. Default: (null) do not compute.</param>
        /// <returns>Singluar values as real diagonal matrix of same size and precision as <paramref name="A"/>.</returns>
        public static Array<double> svd(InArray<complex> A, OutArray<complex> U = null) {
            return svd(A, U, null, false, false);
        }
        /// <summary>
        /// Singular value decomposition. 
        /// </summary>
        /// <param name="A">Input matrix. Size [M,N].</param>
        /// <param name="outU">[Optional, output] Returns left singular vectors of <paramref name="A"/> as columns. Default: (null) do not compute.</param>
        /// <param name="small">If true: return only first min(M,N) singular values. Reduces <paramref name="outU"/> to size [min(M,N),min(M,N)]. Default: false.</param>
        /// <returns>Singluar values as real diagonal matrix of same size and precision as <paramref name="A"/>.</returns>
        public static Array<double> svd(InArray<complex> A, OutArray<complex> outU = null, bool small = false) {
            return svd(A, outU, null, small, false);
        }
        /// <summary>
        /// Singular value decomposition. 
        /// </summary>
        /// <param name="A">Input matrix. Size [M,N].</param>
        /// <param name="outU">[Output] Returns left singular vectors of <paramref name="A"/> as columns. Must be non-null on entry.</param>
        /// <param name="outV">[Output] Returns right singular vectors of <paramref name="A"/> as rows of matrix V. Must be non-null on entry and will be replaced with new values on return.</param>
        /// <returns>Singluar values as real diagonal matrix of same size and precision as <paramref name="A"/>.</returns>
        public static Array<double> svd(InArray<complex> A, OutArray<complex> outU, OutArray<complex> outV) {
            return svd(A, outU, outV, false, false);
        }
        /// <summary>
        /// Singular value decomposition. 
        /// </summary>
        /// <param name="A">Input matrix.</param>
        /// <param name="outU">[Output] Left singular vectors of <paramref name="A"/> as columns. Provide null if this information is not required.</param>
        /// <param name="outV">[Output] Right singular vectors of <paramref name="A"/> as rows. Must be non-null on entry and will be replaced with new values on return.</param>
        /// <param name="small">If true: return only the first min(M,N) singlular values. Decreases the size of the array returned to [min(M,N),min(M,N)].</param>
        /// <param name="discardFiniteTest">true: the matrix given will not be checked for infinte or NaN values. If such elements 
        /// exist, convergence failure or an error might occur. Use with care! </param>
        /// <returns>Singluar values as real diagonal matrix of same size and precicion as <paramref name="A"/>.</returns>
        
        public unsafe static Array<double> svd(InArray<complex> A, OutArray<complex> outU,
                                           OutArray<complex> outV, bool small, bool discardFiniteTest) {
            if (object.Equals(A, null))
                throw new ArgumentNullException(nameof(A));
            using (Scope.Enter()) {

                Array<complex> _A = A;

                if (!_A.IsMatrix)
                    throw new ArgumentException("svd() is only defined for matrices.");
                // early exit for small matrices
                if (_A.Size[1] < 4 && _A.Size[0] == _A.Size[1]) {
                    switch (_A.Size[0]) {
                        case 1:
                            if (!Object.Equals(outU, null))
                                outU.a = ( complex)1.0;
                            if (!Object.Equals(outV, null))
                                outV.a = ( complex)1.0;
                            return abs(_A);
                            //case 2:
                            //    return -1; 
                            //case 3: 
                            //    return -1; 
                    }
                }
                if (!discardFiniteTest && !(bool)allall(isfinite(_A)))
                    throw new ArgumentException("svd(): input must have only finite elements.");
                if (Lapack == null)
                    throw new InvalidOperationException("svd() requires a LAPACK package. No LAPACK found.");
                // parameter evaluation
                long M = _A.Size[0];
                long N = _A.Size[1];
                long minMN = (M < N) ? M : N;
                int LDU = (int)M; int LDVT = (int)N;
                int LDA = (int)M, lenDU = 0, lenVT = 0;
                MemoryHandle  dS = New<double>((ulong)minMN), dU = null, dVT = null;
                var pS = (double*)dS.Pointer; 

                char jobz = (small) ? 'S' : 'A';

                int info = 0;
                try {
                    if (!Object.Equals(outU, null) || !Object.Equals(outV, null)) {
                        // need to return U and VT 
                        if (small) {
                            lenDU = (int)(M * minMN);
                            lenVT = (int)(N * minMN);
                            LDVT = (int)minMN;
                        } else {
                            lenDU = (int)(M * M);
                            lenVT = (int)(N * N);
                        }
                        dU = New<complex>((ulong)lenDU);
                        dVT = New<complex>((ulong)lenVT);
                    } else {
                        jobz = 'N';
                    }

                    // must create copy of input ! 
                    Array<complex> Acopy = _A.C;

                    var dInput = (complex*)Acopy.GetHostPointerForWrite(StorageOrders.ColumnMajor);

                    Lapack.zgesdd(jobz, (int)M, (int)N, dInput, LDA, pS,
                        dU != null ? (complex*)dU.Pointer : null, LDU,
                        dVT != null ? (complex*)dVT.Pointer : null, LDVT, ref info);
                    GC.KeepAlive(Acopy); 

                    if (info < 0)
                        throw new ArgumentException("Internal error: the " + (-info).ToString() + "-th argument to ?gesdd (LAPACK) was invalid. Please contact ILNumerics!");
                    if (info > 0)
                        throw new ArgumentException("svd() was not converging in LAPACK's ?gesdd.");

                    Array<double> ret = null;
                    if (info == 0) {
                        // success
                        if (!Object.Equals(outU, null) || !Object.Equals(outV, null)) {
                            if (small) {
                                ret = zeros<double>(minMN, minMN);
                            } else {
                                ret = zeros<double>(M, N);
                            }
                            for (int i = 0; i < minMN; i++) {
                                ret.SetValue(pS[i], i, i);
                            }
                            if (!Object.Equals(outU, null)) {
                                var storage = Core.StorageLayer.Storage<complex>.Create();
                                storage.S.SetAll(M, lenDU / M, StorageOrders.ColumnMajor); 
                                storage.Handles[0] = dU;
                                dU = null; 
                                // was: outU.a = array<complex>(dU, M, lenDU / M);
                                lock (outU.SynchObj)
                                    outU.Storage.Assign(storage, true);
                                storage.Dispose(); 
                            }
                            if (!Object.Equals(outV, null)) {
                                var storage = Core.StorageLayer.Storage<complex>.Create();
                                storage.S.SetAll(N, lenVT / N, StorageOrders.ColumnMajor);
                                storage.Handles[0] = dVT;
                                dVT = null; 
                                outV.Storage.Assign(storage, true);
                                storage.Dispose(); 
                                outV.a = conj(outV).T;
                            }
                        } else {
                            var storage = Core.StorageLayer.Storage<double>.Create();
                            storage.S.SetAll(minMN, 1, StorageOrders.ColumnMajor);
                            storage.Handles[0] = dS;
                            ret = storage.RetArray;
                            dS = null; 
                        }
                    }
                    return ret;
                } finally {
                    if (dS != null) free<double>(dS,0);
                    if (dU != null) free<complex>(dU,0);
                    if (dVT != null) free<complex>(dVT,0);
                }
            }
        }

#endregion HYCALPER AUTO GENERATED CODE
    }
}
