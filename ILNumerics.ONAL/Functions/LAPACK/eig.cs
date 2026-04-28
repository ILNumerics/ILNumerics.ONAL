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
using ILNumerics.Core.MemoryLayer;
using System;
using System.Security;
using static ILNumerics.Globals;
using static ILNumerics.Core.Functions.Builtin.MathInternal;
using ILNumerics.Core.Functions.Builtin;


/*!HC:TYPELIST:
<hycalper>
<type>
    <source locate="here">
        complex
    </source>
    <destination>complex</destination>
    <destination>fcomplex</destination>
    <destination>fcomplex</destination>
</type>
<type>
    <source locate="here">
        double
    </source>
    <destination>complex</destination>
    <destination>fcomplex</destination>
    <destination>float</destination>
</type>
<type>
    <source locate="nextline">
        HC_isreal
    </source>
    <destination>#if IS_CMPLX</destination>
    <destination>#if IS_CMPLX</destination>
    <destination>#if !IS_CMPLX</destination>
</type>
<type>
    <source locate="here">
        Double
    </source>
    <destination>Double</destination>
    <destination>Single</destination>
    <destination>Single</destination>
</type>
<type>
    <source locate="nextline">
        CreateRetEV
    </source>
    <destination>ret.a = wr;</destination>
    <destination>ret.a = wr;</destination>
    <destination>ret.a = ccomplex(wr,wi);</destination>
</type>
<type>
    <source locate="nextline">
        HCArrWI
    </source>
    <destination></destination>
    <destination></destination>
    <destination><![CDATA[float[] wi = MemoryPool.Pool.New< float>(n);]]></destination>
</type>
<type>
    <source locate="nextline">
        HCArrCleanUpWI
    </source>
    <destination></destination>
    <destination></destination>
    <destination>MemoryPool.Pool.Free(wi);</destination>
</type>
<type>
    <source locate="nextline">
        HCSortEVal
    </source>
    <destination>retArr[i] = wr[i]; </destination>
    <destination>retArr[i] = wr[i]; </destination>
    <destination>retArr[i].real = wr[i]; retArr[i].imag = wi[i];</destination>
</type>
<type>
    <source locate="here">
        dsyevr
    </source>
    <destination>zheevr</destination>
    <destination>cheevr</destination>
    <destination>ssyevr</destination>
</type>
<type>
    <source locate="nextline">
        HC?geevx
    </source>
    <destination>Lapack.zgeevx(bal,'N',jobvr,'N',(int)n,(complex*)tmpA.GetHostPointerForWrite(),(int)n,(complex*)wr.GetHostPointerForWrite(),   null,1,vr,ldvr,ref ilo,ref ihi,scale,ref abnrm,rconde,rcondv,ref info);   </destination>
    <destination>Lapack.cgeevx(bal,'N',jobvr,'N',(int)n,(fcomplex*)tmpA.GetHostPointerForWrite(),(int)n,(fcomplex*)wr.GetHostPointerForWrite(),   null,1,vr,ldvr,ref ilo,ref ihi,scale,ref abnrm,rconde,rcondv,ref info);   </destination>
    <destination>Lapack.sgeevx(bal,'N',jobvr,'N',(int)n,(float*)tmpA.GetHostPointerForWrite(),(int)n,(float*)wr.GetHostPointerForWrite(),(float*)wi.GetHostPointerForWrite(),null,1,vr,ldvr,ref ilo,ref ihi,scale,ref abnrm,rconde,rcondv,ref info);   </destination>
</type>
</hycalper>
*/

namespace ILNumerics {
    public static partial class ILMath {

        #region HYCALPER LOOPSTART standard eigenproblems
        /// <summary>
        /// Computes eigenvalues of general square matrix <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input matrix. Size [n x n].</param>
        /// <returns>Vector of eigenvalues of <paramref name="A"/>. Size [n x 1].</returns>
        /// <remarks><para>The eigenvalues of <paramref name="A"/> are found by use of the Lapack functions dgeevx, sgeevx, cgeevx and zgeevx. </para>
        /// <para>The vector returned is complex, since <paramref name="A"/> may be nonsymmetric. Use <see cref="eigSymm(InArray{double})"/> for computing real eigenvalues of symmetric matrices.</para>
        /// <para><paramref name="A"/> is internally balanced first. This includes permutations and scaling of <paramref name="A"/> in order to improve the conditioning of the eigenvalues.</para></remarks>
        /// <seealso cref="eig(InArray{double}, OutArray{complex})"/>
        /// <seealso cref="eig(InArray{double},OutArray{complex},ref MatrixProperties,bool)"/>
        public static Array<complex> eig(InArray<double> A) {
            MatrixProperties props = MatrixProperties.None;
            return eig(A, null, ref props, true);

        }
        /// <summary>
        /// Computes eigenvalues and eigenvectors of general square matrix <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input matrix. Size [n x n].</param>
        /// <param name="V">[Optional] Output, matrix of eigenvectors of <paramref name="A"/>. Size [n x n]. Default: (null) do not compute. If not null on entry <paramref name="V"/> returns the Eigenvectors.</param>
        /// <returns>Diagonal matrix with eigenvalues of <paramref name="A"/>. Size [n x n].</returns>
        /// <remarks><para>The eigenvalues of <paramref name="A"/> are found by use of the Lapack functions dgeevx, sgeevx, cgeevx and zgeevx. </para>
        /// <para>The matrices returned will be complex, since <paramref name="A"/> may be nonsymmetric. Use <see cref="eigSymm(InArray{double})"/> for computing real eigenvalues of symmetric matrices.</para>
        /// <para><paramref name="A"/> is internally balanced first. This includes permutations and scaling of <paramref name="A"/> in order to improve the conditioning of the eigenvalues.</para></remarks>
        /// <seealso cref="eig(InArray{double})"/>
        /// <seealso cref="eig(InArray{double},OutArray{complex},ref MatrixProperties,bool)"/>
        public static Array<complex> eig(InArray<double> A, OutArray<complex> V) {
            MatrixProperties props = MatrixProperties.None;
            return eig(A, V, ref props, true);
        }
        /// <summary>
        /// Find eigenvalues  and eigenvectors of square matrix <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">Input: square matrix, size [n x n].</param>
        /// <param name="V">[Optional] Output, matrix of eigenvectors of <paramref name="A"/>. Size [n x n]. If null: do not compute. If not null on entry <paramref name="V"/> returns the Eigenvectors.</param>
        /// <param name="propsA">Matrix properties, on input - if specified, 
        /// will be used to choose an advantageous path to the solution. On exit will be 
        /// filled according to the properties of <paramref name="A"/> (symmetric | hermitian).</param>
        /// <param name="balance">True: permute <paramref name="A"/> in order to increase the 
        /// numerical stability, false: do not permute <paramref name="A"/>.</param>
        /// <returns>Eigenvalues as vector (if <paramref name="V"/> is null) or as diagonoal 
        /// matrix (if <paramref name="V"/> was not null).</returns>
        /// <remarks><para>The eigenvalues of <paramref name="A"/> are found by use of the 
        /// Lapack functions dgeevx, sgeevx, cgeevx and zgeevx. </para>
        /// <para>The matrices returned will be complex, since <paramref name="A"/> may be nonsymmetric. Use <see cref="eigSymm(InArray{double})"/> for computing real eigenvalues of symmetric matrices.</para>
        /// <para>Depending on the parameter <paramref name="balance"/> <paramref name="A"/> 
        /// will be balanced first. This includes permutations and scaling of A in order to 
        /// improve the conditioning of the eigenvalues.</para></remarks>
        /// <seealso cref="eig(InArray{double})"/>
        /// <seealso cref="eig(InArray{double}, OutArray{complex}, ref MatrixProperties, bool)"/>
        /// <exception cref="ArgumentException">if <paramref name="A"/> is not a square matrix.</exception>
        
        public static Array<complex> eig(InArray<double> A, OutArray<complex> V, ref MatrixProperties propsA, bool balance) {
            using (Scope.Enter()) {
                Array<double> _A = A; 
                if (_A.IsEmpty) {
                    if (!object.Equals(V, null))
                        lock (V.SynchObj)
                            V.a = empty<complex>(_A.Size);
                    return empty<complex>(_A.Size);
                }
                Array<complex> ret = empty<complex>();
                var n = _A.Size[0];
                bool createVR = (object.Equals(V, null)) ? false : true;
                if (n != _A.Size[1])
                    throw new ArgumentException($"eig(): matrix A must be a square matrix. Found: {_A.S}.");

                propsA |= MatrixProperties.Square;
                if (((propsA & MatrixProperties.Hermitian) != 0 || ishermitian(_A.C))) {
                    propsA |= MatrixProperties.Hermitian;
                    if (createVR) {
                        Array<double> Vd = (double)1;
                        Array<Double> tmpRet = eigSymm(_A, Vd);
                        lock (V.SynchObj)
                            V.a = tocomplex(Vd);
                        ret.a = tocomplex(tmpRet);
                    } else {
                        ret.a = tocomplex(eigSymm(_A, null));
                    }
                } else {
                    // nonsymmetric case
                    MemoryHandle scaleH = null, rcondeH = null, rcondevH = null;
                    unsafe {
                        try {

                            char bal = (balance) ? 'B' : 'N', jobvr;
                            Array<double> tmpA = _A.C;

                            Array<double> wr = empty<double>(n);
                            /*!HC:HC_isreal*/
#if !IS_CMPLX
                            Array<double> wi = empty<double>(n);
#endif
                            scaleH = New<Double>(n);
                            Double* scale = (Double*)scaleH.Pointer;

                            rcondeH = New<Double>(n);
                            Double* rconde = (Double*)rcondeH.Pointer;

                            rcondevH = New<Double>(n);
                            Double* rcondv = (Double*)rcondevH.Pointer;

                            Double abnrm = 0;
                            int ldvr, ilo = 0, ihi = 0, info = 0;

                            Array<double> VR = null;
                            double* vr = null; 
                            if (createVR) {
                                ldvr = (int)n;
                                VR = empty<double>(n, n, StorageOrders.ColumnMajor);
                                vr = (double*)VR.GetHostPointerForRead();
                                jobvr = 'V';
                            } else {
                                ldvr = 1;
                                ////vrH = New<double>(1); 
                                //vr = null;
                                jobvr = 'N';
                            }
                            /*!HC:HC?geevx*/
                            Lapack.dgeevx(bal, 'N', jobvr, 'N', (int)n, (double*)tmpA.GetHostPointerForWrite(), (int)n, (double*)wr.GetHostPointerForWrite(), (double*)wi.GetHostPointerForWrite(), null, 1, vr, ldvr, ref ilo, ref ihi, scale, ref abnrm, rconde, rcondv, ref info);

                            if (info != 0)
                                throw new ArgumentException("eig(): error in Lapack '?geevx'. Info returned: (" + info + ")");

                            /*!HC:CreateRetEV*/
                            ret.a = ccomplex(wr, wi);

                            if (createVR) {
                                /*!HC:HC_isreal*/
#if !IS_CMPLX
                                #region HCSortEVec
                                System.Diagnostics.Debug.Assert(vr != null);
                                lock (V.SynchObj) {
                                    V.a = empty<complex>(n, n, StorageOrders.ColumnMajor);
                                    
                                    complex* VArr = (complex*)V.GetHostPointerForRead(); // sic! we use VArr for writing, though. 

                                    for (int c = 0; c < n;) {
                                        if (c < n - 1 && wi.GetValue(c) != 0 && wi.GetValue(c + 1) != 0) {
                                            // complex conjugate eigentvalue pair
                                            ilo = (int)n * c; ihi = ilo + (int)n;
                                            for (int r = 0; r < n; r++) {
                                                VArr[ilo].real = vr[ilo];
                                                VArr[ilo].imag = vr[ihi];
                                                VArr[ihi].real = vr[ilo];
                                                VArr[ihi].imag = -vr[ihi];
                                                ilo++; ihi++;
                                            }
                                            c += 2;
                                        } else {
                                            // real eigenvalue
                                            ilo = (int)n * c;
                                            for (int r = 0; r < n; r++) {
                                                VArr[ilo].real = vr[ilo];
                                                VArr[ilo].imag = 0;
                                                ilo++;
                                            }
                                            c++;
                                        }
                                    }
                                    #endregion HYCALPER
                                    GC.KeepAlive(VR);
                                    GC.KeepAlive(V);
                                }
#else
                                lock (V.SynchObj) 
                                    V.a = VR; 
#endif
                                ret.a = diag<complex>(ret);
                            }
                        } finally {
                            if (scaleH != null) free<double>(scaleH, 0);
                            if (rcondeH != null) free<double>(rcondeH, 0);
                            if (rcondevH != null) free<double>(rcondevH, 0);
                        }
                    } // unsafe
                }
                return ret;
            }
        }
        #endregion HYCALPER LOOPEND 
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
        /// <summary>
        /// Computes eigenvalues of general square matrix <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input matrix. Size [n x n].</param>
        /// <returns>Vector of eigenvalues of <paramref name="A"/>. Size [n x 1].</returns>
        /// <remarks><para>The eigenvalues of <paramref name="A"/> are found by use of the Lapack functions dgeevx, sgeevx, cgeevx and zgeevx. </para>
        /// <para>The vector returned is fcomplex, since <paramref name="A"/> may be nonsymmetric. Use <see cref="eigSymm(InArray{float})"/> for computing real eigenvalues of symmetric matrices.</para>
        /// <para><paramref name="A"/> is internally balanced first. This includes permutations and scaling of <paramref name="A"/> in order to improve the conditioning of the eigenvalues.</para></remarks>
        /// <seealso cref="eig(InArray{float}, OutArray{fcomplex})"/>
        /// <seealso cref="eig(InArray{float},OutArray{fcomplex},ref MatrixProperties,bool)"/>
        public static Array<fcomplex> eig(InArray<float> A) {
            MatrixProperties props = MatrixProperties.None;
            return eig(A, null, ref props, true);

        }
        /// <summary>
        /// Computes eigenvalues and eigenvectors of general square matrix <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input matrix. Size [n x n].</param>
        /// <param name="V">[Optional] Output, matrix of eigenvectors of <paramref name="A"/>. Size [n x n]. Default: (null) do not compute. If not null on entry <paramref name="V"/> returns the Eigenvectors.</param>
        /// <returns>Diagonal matrix with eigenvalues of <paramref name="A"/>. Size [n x n].</returns>
        /// <remarks><para>The eigenvalues of <paramref name="A"/> are found by use of the Lapack functions dgeevx, sgeevx, cgeevx and zgeevx. </para>
        /// <para>The matrices returned will be fcomplex, since <paramref name="A"/> may be nonsymmetric. Use <see cref="eigSymm(InArray{float})"/> for computing real eigenvalues of symmetric matrices.</para>
        /// <para><paramref name="A"/> is internally balanced first. This includes permutations and scaling of <paramref name="A"/> in order to improve the conditioning of the eigenvalues.</para></remarks>
        /// <seealso cref="eig(InArray{float})"/>
        /// <seealso cref="eig(InArray{float},OutArray{fcomplex},ref MatrixProperties,bool)"/>
        public static Array<fcomplex> eig(InArray<float> A, OutArray<fcomplex> V) {
            MatrixProperties props = MatrixProperties.None;
            return eig(A, V, ref props, true);
        }
        /// <summary>
        /// Find eigenvalues  and eigenvectors of square matrix <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">Input: square matrix, size [n x n].</param>
        /// <param name="V">[Optional] Output, matrix of eigenvectors of <paramref name="A"/>. Size [n x n]. If null: do not compute. If not null on entry <paramref name="V"/> returns the Eigenvectors.</param>
        /// <param name="propsA">Matrix properties, on input - if specified, 
        /// will be used to choose an advantageous path to the solution. On exit will be 
        /// filled according to the properties of <paramref name="A"/> (symmetric | hermitian).</param>
        /// <param name="balance">True: permute <paramref name="A"/> in order to increase the 
        /// numerical stability, false: do not permute <paramref name="A"/>.</param>
        /// <returns>Eigenvalues as vector (if <paramref name="V"/> is null) or as diagonoal 
        /// matrix (if <paramref name="V"/> was not null).</returns>
        /// <remarks><para>The eigenvalues of <paramref name="A"/> are found by use of the 
        /// Lapack functions dgeevx, sgeevx, cgeevx and zgeevx. </para>
        /// <para>The matrices returned will be fcomplex, since <paramref name="A"/> may be nonsymmetric. Use <see cref="eigSymm(InArray{float})"/> for computing real eigenvalues of symmetric matrices.</para>
        /// <para>Depending on the parameter <paramref name="balance"/> <paramref name="A"/> 
        /// will be balanced first. This includes permutations and scaling of A in order to 
        /// improve the conditioning of the eigenvalues.</para></remarks>
        /// <seealso cref="eig(InArray{float})"/>
        /// <seealso cref="eig(InArray{float}, OutArray{fcomplex}, ref MatrixProperties, bool)"/>
        /// <exception cref="ArgumentException">if <paramref name="A"/> is not a square matrix.</exception>
        
        public static Array<fcomplex> eig(InArray<float> A, OutArray<fcomplex> V, ref MatrixProperties propsA, bool balance) {
            using (Scope.Enter()) {
                Array<float> _A = A; 
                if (_A.IsEmpty) {
                    if (!object.Equals(V, null))
                        lock (V.SynchObj)
                            V.a = empty<fcomplex>(_A.Size);
                    return empty<fcomplex>(_A.Size);
                }
                Array<fcomplex> ret = empty<fcomplex>();
                var n = _A.Size[0];
                bool createVR = (object.Equals(V, null)) ? false : true;
                if (n != _A.Size[1])
                    throw new ArgumentException($"eig(): matrix A must be a square matrix. Found: {_A.S}.");

                propsA |= MatrixProperties.Square;
                if (((propsA & MatrixProperties.Hermitian) != 0 || ishermitian(_A.C))) {
                    propsA |= MatrixProperties.Hermitian;
                    if (createVR) {
                        Array<float> Vd = (float)1;
                        Array<Single> tmpRet = eigSymm(_A, Vd);
                        lock (V.SynchObj)
                            V.a = tofcomplex(Vd);
                        ret.a = tofcomplex(tmpRet);
                    } else {
                        ret.a = tofcomplex(eigSymm(_A, null));
                    }
                } else {
                    // nonsymmetric case
                    MemoryHandle scaleH = null, rcondeH = null, rcondevH = null;
                    unsafe {
                        try {

                            char bal = (balance) ? 'B' : 'N', jobvr;
                            Array<float> tmpA = _A.C;

                            Array<float> wr = empty<float>(n);
                            #if !IS_CMPLX
                            Array<float> wi = empty<float>(n);
#endif
                            scaleH = New<Single>(n);
                            Single* scale = (Single*)scaleH.Pointer;

                            rcondeH = New<Single>(n);
                            Single* rconde = (Single*)rcondeH.Pointer;

                            rcondevH = New<Single>(n);
                            Single* rcondv = (Single*)rcondevH.Pointer;

                            Single abnrm = 0;
                            int ldvr, ilo = 0, ihi = 0, info = 0;

                            Array<float> VR = null;
                            float* vr = null; 
                            if (createVR) {
                                ldvr = (int)n;
                                VR = empty<float>(n, n, StorageOrders.ColumnMajor);
                                vr = (float*)VR.GetHostPointerForRead();
                                jobvr = 'V';
                            } else {
                                ldvr = 1;
                                ////vrH = New<float>(1); 
                                //vr = null;
                                jobvr = 'N';
                            }
                            Lapack.sgeevx(bal,'N',jobvr,'N',(int)n,(float*)tmpA.GetHostPointerForWrite(),(int)n,(float*)wr.GetHostPointerForWrite(),(float*)wi.GetHostPointerForWrite(),null,1,vr,ldvr,ref ilo,ref ihi,scale,ref abnrm,rconde,rcondv,ref info);   

                            if (info != 0)
                                throw new ArgumentException("eig(): error in Lapack '?geevx'. Info returned: (" + info + ")");

                            ret.a = ccomplex(wr,wi);

                            if (createVR) {
                                #if !IS_CMPLX
                                #region HCSortEVec
                                System.Diagnostics.Debug.Assert(vr != null);
                                lock (V.SynchObj) {
                                    V.a = empty<fcomplex>(n, n, StorageOrders.ColumnMajor);
                                    
                                    fcomplex* VArr = (fcomplex*)V.GetHostPointerForRead(); // sic! we use VArr for writing, though. 

                                    for (int c = 0; c < n;) {
                                        if (c < n - 1 && wi.GetValue(c) != 0 && wi.GetValue(c + 1) != 0) {
                                            // fcomplex conjugate eigentvalue pair
                                            ilo = (int)n * c; ihi = ilo + (int)n;
                                            for (int r = 0; r < n; r++) {
                                                VArr[ilo].real = vr[ilo];
                                                VArr[ilo].imag = vr[ihi];
                                                VArr[ihi].real = vr[ilo];
                                                VArr[ihi].imag = -vr[ihi];
                                                ilo++; ihi++;
                                            }
                                            c += 2;
                                        } else {
                                            // real eigenvalue
                                            ilo = (int)n * c;
                                            for (int r = 0; r < n; r++) {
                                                VArr[ilo].real = vr[ilo];
                                                VArr[ilo].imag = 0;
                                                ilo++;
                                            }
                                            c++;
                                        }
                                    }
                                    #endregion HYCALPER
                                    GC.KeepAlive(VR);
                                    GC.KeepAlive(V);
                                }
#else
                                lock (V.SynchObj) 
                                    V.a = VR; 
#endif
                                ret.a = diag<fcomplex>(ret);
                            }
                        } finally {
                            if (scaleH != null) free<float>(scaleH, 0);
                            if (rcondeH != null) free<float>(rcondeH, 0);
                            if (rcondevH != null) free<float>(rcondevH, 0);
                        }
                    } // unsafe
                }
                return ret;
            }
        }
        /// <summary>
        /// Computes eigenvalues of general square matrix <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input matrix. Size [n x n].</param>
        /// <returns>Vector of eigenvalues of <paramref name="A"/>. Size [n x 1].</returns>
        /// <remarks><para>The eigenvalues of <paramref name="A"/> are found by use of the Lapack functions dgeevx, sgeevx, cgeevx and zgeevx. </para>
        /// <para>The vector returned is fcomplex, since <paramref name="A"/> may be nonsymmetric. Use <see cref="eigSymm(InArray{fcomplex})"/> for computing real eigenvalues of symmetric matrices.</para>
        /// <para><paramref name="A"/> is internally balanced first. This includes permutations and scaling of <paramref name="A"/> in order to improve the conditioning of the eigenvalues.</para></remarks>
        /// <seealso cref="eig(InArray{fcomplex}, OutArray{fcomplex})"/>
        /// <seealso cref="eig(InArray{fcomplex},OutArray{fcomplex},ref MatrixProperties,bool)"/>
        public static Array<fcomplex> eig(InArray<fcomplex> A) {
            MatrixProperties props = MatrixProperties.None;
            return eig(A, null, ref props, true);

        }
        /// <summary>
        /// Computes eigenvalues and eigenvectors of general square matrix <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input matrix. Size [n x n].</param>
        /// <param name="V">[Optional] Output, matrix of eigenvectors of <paramref name="A"/>. Size [n x n]. Default: (null) do not compute. If not null on entry <paramref name="V"/> returns the Eigenvectors.</param>
        /// <returns>Diagonal matrix with eigenvalues of <paramref name="A"/>. Size [n x n].</returns>
        /// <remarks><para>The eigenvalues of <paramref name="A"/> are found by use of the Lapack functions dgeevx, sgeevx, cgeevx and zgeevx. </para>
        /// <para>The matrices returned will be fcomplex, since <paramref name="A"/> may be nonsymmetric. Use <see cref="eigSymm(InArray{fcomplex})"/> for computing real eigenvalues of symmetric matrices.</para>
        /// <para><paramref name="A"/> is internally balanced first. This includes permutations and scaling of <paramref name="A"/> in order to improve the conditioning of the eigenvalues.</para></remarks>
        /// <seealso cref="eig(InArray{fcomplex})"/>
        /// <seealso cref="eig(InArray{fcomplex},OutArray{fcomplex},ref MatrixProperties,bool)"/>
        public static Array<fcomplex> eig(InArray<fcomplex> A, OutArray<fcomplex> V) {
            MatrixProperties props = MatrixProperties.None;
            return eig(A, V, ref props, true);
        }
        /// <summary>
        /// Find eigenvalues  and eigenvectors of square matrix <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">Input: square matrix, size [n x n].</param>
        /// <param name="V">[Optional] Output, matrix of eigenvectors of <paramref name="A"/>. Size [n x n]. If null: do not compute. If not null on entry <paramref name="V"/> returns the Eigenvectors.</param>
        /// <param name="propsA">Matrix properties, on input - if specified, 
        /// will be used to choose an advantageous path to the solution. On exit will be 
        /// filled according to the properties of <paramref name="A"/> (symmetric | hermitian).</param>
        /// <param name="balance">True: permute <paramref name="A"/> in order to increase the 
        /// numerical stability, false: do not permute <paramref name="A"/>.</param>
        /// <returns>Eigenvalues as vector (if <paramref name="V"/> is null) or as diagonoal 
        /// matrix (if <paramref name="V"/> was not null).</returns>
        /// <remarks><para>The eigenvalues of <paramref name="A"/> are found by use of the 
        /// Lapack functions dgeevx, sgeevx, cgeevx and zgeevx. </para>
        /// <para>The matrices returned will be fcomplex, since <paramref name="A"/> may be nonsymmetric. Use <see cref="eigSymm(InArray{fcomplex})"/> for computing real eigenvalues of symmetric matrices.</para>
        /// <para>Depending on the parameter <paramref name="balance"/> <paramref name="A"/> 
        /// will be balanced first. This includes permutations and scaling of A in order to 
        /// improve the conditioning of the eigenvalues.</para></remarks>
        /// <seealso cref="eig(InArray{fcomplex})"/>
        /// <seealso cref="eig(InArray{fcomplex}, OutArray{fcomplex}, ref MatrixProperties, bool)"/>
        /// <exception cref="ArgumentException">if <paramref name="A"/> is not a square matrix.</exception>
        
        public static Array<fcomplex> eig(InArray<fcomplex> A, OutArray<fcomplex> V, ref MatrixProperties propsA, bool balance) {
            using (Scope.Enter()) {
                Array<fcomplex> _A = A; 
                if (_A.IsEmpty) {
                    if (!object.Equals(V, null))
                        lock (V.SynchObj)
                            V.a = empty<fcomplex>(_A.Size);
                    return empty<fcomplex>(_A.Size);
                }
                Array<fcomplex> ret = empty<fcomplex>();
                var n = _A.Size[0];
                bool createVR = (object.Equals(V, null)) ? false : true;
                if (n != _A.Size[1])
                    throw new ArgumentException($"eig(): matrix A must be a square matrix. Found: {_A.S}.");

                propsA |= MatrixProperties.Square;
                if (((propsA & MatrixProperties.Hermitian) != 0 || ishermitian(_A.C))) {
                    propsA |= MatrixProperties.Hermitian;
                    if (createVR) {
                        Array<fcomplex> Vd = (fcomplex)1;
                        Array<Single> tmpRet = eigSymm(_A, Vd);
                        lock (V.SynchObj)
                            V.a = tofcomplex(Vd);
                        ret.a = tofcomplex(tmpRet);
                    } else {
                        ret.a = tofcomplex(eigSymm(_A, null));
                    }
                } else {
                    // nonsymmetric case
                    MemoryHandle scaleH = null, rcondeH = null, rcondevH = null;
                    unsafe {
                        try {

                            char bal = (balance) ? 'B' : 'N', jobvr;
                            Array<fcomplex> tmpA = _A.C;

                            Array<fcomplex> wr = empty<fcomplex>(n);
                            #if IS_CMPLX
                            Array<fcomplex> wi = empty<fcomplex>(n);
#endif
                            scaleH = New<Single>(n);
                            Single* scale = (Single*)scaleH.Pointer;

                            rcondeH = New<Single>(n);
                            Single* rconde = (Single*)rcondeH.Pointer;

                            rcondevH = New<Single>(n);
                            Single* rcondv = (Single*)rcondevH.Pointer;

                            Single abnrm = 0;
                            int ldvr, ilo = 0, ihi = 0, info = 0;

                            Array<fcomplex> VR = null;
                            fcomplex* vr = null; 
                            if (createVR) {
                                ldvr = (int)n;
                                VR = empty<fcomplex>(n, n, StorageOrders.ColumnMajor);
                                vr = (fcomplex*)VR.GetHostPointerForRead();
                                jobvr = 'V';
                            } else {
                                ldvr = 1;
                                ////vrH = New<fcomplex>(1); 
                                //vr = null;
                                jobvr = 'N';
                            }
                            Lapack.cgeevx(bal,'N',jobvr,'N',(int)n,(fcomplex*)tmpA.GetHostPointerForWrite(),(int)n,(fcomplex*)wr.GetHostPointerForWrite(),   null,1,vr,ldvr,ref ilo,ref ihi,scale,ref abnrm,rconde,rcondv,ref info);   

                            if (info != 0)
                                throw new ArgumentException("eig(): error in Lapack '?geevx'. Info returned: (" + info + ")");

                            ret.a = wr;

                            if (createVR) {
                                #if IS_CMPLX
                                #region HCSortEVec
                                System.Diagnostics.Debug.Assert(vr != null);
                                lock (V.SynchObj) {
                                    V.a = empty<fcomplex>(n, n, StorageOrders.ColumnMajor);
                                    
                                    fcomplex* VArr = (fcomplex*)V.GetHostPointerForRead(); // sic! we use VArr for writing, though. 

                                    for (int c = 0; c < n;) {
                                        if (c < n - 1 && wi.GetValue(c) != 0 && wi.GetValue(c + 1) != 0) {
                                            // fcomplex conjugate eigentvalue pair
                                            ilo = (int)n * c; ihi = ilo + (int)n;
                                            for (int r = 0; r < n; r++) {
                                                VArr[ilo].real = vr[ilo];
                                                VArr[ilo].imag = vr[ihi];
                                                VArr[ihi].real = vr[ilo];
                                                VArr[ihi].imag = -vr[ihi];
                                                ilo++; ihi++;
                                            }
                                            c += 2;
                                        } else {
                                            // real eigenvalue
                                            ilo = (int)n * c;
                                            for (int r = 0; r < n; r++) {
                                                VArr[ilo].real = vr[ilo];
                                                VArr[ilo].imag = 0;
                                                ilo++;
                                            }
                                            c++;
                                        }
                                    }
                                    #endregion HYCALPER
                                    GC.KeepAlive(VR);
                                    GC.KeepAlive(V);
                                }
#else
                                lock (V.SynchObj) 
                                    V.a = VR; 
#endif
                                ret.a = diag<fcomplex>(ret);
                            }
                        } finally {
                            if (scaleH != null) free<fcomplex>(scaleH, 0);
                            if (rcondeH != null) free<fcomplex>(rcondeH, 0);
                            if (rcondevH != null) free<fcomplex>(rcondevH, 0);
                        }
                    } // unsafe
                }
                return ret;
            }
        }
        /// <summary>
        /// Computes eigenvalues of general square matrix <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input matrix. Size [n x n].</param>
        /// <returns>Vector of eigenvalues of <paramref name="A"/>. Size [n x 1].</returns>
        /// <remarks><para>The eigenvalues of <paramref name="A"/> are found by use of the Lapack functions dgeevx, sgeevx, cgeevx and zgeevx. </para>
        /// <para>The vector returned is complex, since <paramref name="A"/> may be nonsymmetric. Use <see cref="eigSymm(InArray{complex})"/> for computing real eigenvalues of symmetric matrices.</para>
        /// <para><paramref name="A"/> is internally balanced first. This includes permutations and scaling of <paramref name="A"/> in order to improve the conditioning of the eigenvalues.</para></remarks>
        /// <seealso cref="eig(InArray{complex}, OutArray{complex})"/>
        /// <seealso cref="eig(InArray{complex},OutArray{complex},ref MatrixProperties,bool)"/>
        public static Array<complex> eig(InArray<complex> A) {
            MatrixProperties props = MatrixProperties.None;
            return eig(A, null, ref props, true);

        }
        /// <summary>
        /// Computes eigenvalues and eigenvectors of general square matrix <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Input matrix. Size [n x n].</param>
        /// <param name="V">[Optional] Output, matrix of eigenvectors of <paramref name="A"/>. Size [n x n]. Default: (null) do not compute. If not null on entry <paramref name="V"/> returns the Eigenvectors.</param>
        /// <returns>Diagonal matrix with eigenvalues of <paramref name="A"/>. Size [n x n].</returns>
        /// <remarks><para>The eigenvalues of <paramref name="A"/> are found by use of the Lapack functions dgeevx, sgeevx, cgeevx and zgeevx. </para>
        /// <para>The matrices returned will be complex, since <paramref name="A"/> may be nonsymmetric. Use <see cref="eigSymm(InArray{complex})"/> for computing real eigenvalues of symmetric matrices.</para>
        /// <para><paramref name="A"/> is internally balanced first. This includes permutations and scaling of <paramref name="A"/> in order to improve the conditioning of the eigenvalues.</para></remarks>
        /// <seealso cref="eig(InArray{complex})"/>
        /// <seealso cref="eig(InArray{complex},OutArray{complex},ref MatrixProperties,bool)"/>
        public static Array<complex> eig(InArray<complex> A, OutArray<complex> V) {
            MatrixProperties props = MatrixProperties.None;
            return eig(A, V, ref props, true);
        }
        /// <summary>
        /// Find eigenvalues  and eigenvectors of square matrix <paramref name="A"/>. 
        /// </summary>
        /// <param name="A">Input: square matrix, size [n x n].</param>
        /// <param name="V">[Optional] Output, matrix of eigenvectors of <paramref name="A"/>. Size [n x n]. If null: do not compute. If not null on entry <paramref name="V"/> returns the Eigenvectors.</param>
        /// <param name="propsA">Matrix properties, on input - if specified, 
        /// will be used to choose an advantageous path to the solution. On exit will be 
        /// filled according to the properties of <paramref name="A"/> (symmetric | hermitian).</param>
        /// <param name="balance">True: permute <paramref name="A"/> in order to increase the 
        /// numerical stability, false: do not permute <paramref name="A"/>.</param>
        /// <returns>Eigenvalues as vector (if <paramref name="V"/> is null) or as diagonoal 
        /// matrix (if <paramref name="V"/> was not null).</returns>
        /// <remarks><para>The eigenvalues of <paramref name="A"/> are found by use of the 
        /// Lapack functions dgeevx, sgeevx, cgeevx and zgeevx. </para>
        /// <para>The matrices returned will be complex, since <paramref name="A"/> may be nonsymmetric. Use <see cref="eigSymm(InArray{complex})"/> for computing real eigenvalues of symmetric matrices.</para>
        /// <para>Depending on the parameter <paramref name="balance"/> <paramref name="A"/> 
        /// will be balanced first. This includes permutations and scaling of A in order to 
        /// improve the conditioning of the eigenvalues.</para></remarks>
        /// <seealso cref="eig(InArray{complex})"/>
        /// <seealso cref="eig(InArray{complex}, OutArray{complex}, ref MatrixProperties, bool)"/>
        /// <exception cref="ArgumentException">if <paramref name="A"/> is not a square matrix.</exception>
        
        public static Array<complex> eig(InArray<complex> A, OutArray<complex> V, ref MatrixProperties propsA, bool balance) {
            using (Scope.Enter()) {
                Array<complex> _A = A; 
                if (_A.IsEmpty) {
                    if (!object.Equals(V, null))
                        lock (V.SynchObj)
                            V.a = empty<complex>(_A.Size);
                    return empty<complex>(_A.Size);
                }
                Array<complex> ret = empty<complex>();
                var n = _A.Size[0];
                bool createVR = (object.Equals(V, null)) ? false : true;
                if (n != _A.Size[1])
                    throw new ArgumentException($"eig(): matrix A must be a square matrix. Found: {_A.S}.");

                propsA |= MatrixProperties.Square;
                if (((propsA & MatrixProperties.Hermitian) != 0 || ishermitian(_A.C))) {
                    propsA |= MatrixProperties.Hermitian;
                    if (createVR) {
                        Array<complex> Vd = (complex)1;
                        Array<Double> tmpRet = eigSymm(_A, Vd);
                        lock (V.SynchObj)
                            V.a = tocomplex(Vd);
                        ret.a = tocomplex(tmpRet);
                    } else {
                        ret.a = tocomplex(eigSymm(_A, null));
                    }
                } else {
                    // nonsymmetric case
                    MemoryHandle scaleH = null, rcondeH = null, rcondevH = null;
                    unsafe {
                        try {

                            char bal = (balance) ? 'B' : 'N', jobvr;
                            Array<complex> tmpA = _A.C;

                            Array<complex> wr = empty<complex>(n);
                            #if IS_CMPLX
                            Array<complex> wi = empty<complex>(n);
#endif
                            scaleH = New<Double>(n);
                            Double* scale = (Double*)scaleH.Pointer;

                            rcondeH = New<Double>(n);
                            Double* rconde = (Double*)rcondeH.Pointer;

                            rcondevH = New<Double>(n);
                            Double* rcondv = (Double*)rcondevH.Pointer;

                            Double abnrm = 0;
                            int ldvr, ilo = 0, ihi = 0, info = 0;

                            Array<complex> VR = null;
                            complex* vr = null; 
                            if (createVR) {
                                ldvr = (int)n;
                                VR = empty<complex>(n, n, StorageOrders.ColumnMajor);
                                vr = (complex*)VR.GetHostPointerForRead();
                                jobvr = 'V';
                            } else {
                                ldvr = 1;
                                ////vrH = New<complex>(1); 
                                //vr = null;
                                jobvr = 'N';
                            }
                            Lapack.zgeevx(bal,'N',jobvr,'N',(int)n,(complex*)tmpA.GetHostPointerForWrite(),(int)n,(complex*)wr.GetHostPointerForWrite(),   null,1,vr,ldvr,ref ilo,ref ihi,scale,ref abnrm,rconde,rcondv,ref info);   

                            if (info != 0)
                                throw new ArgumentException("eig(): error in Lapack '?geevx'. Info returned: (" + info + ")");

                            ret.a = wr;

                            if (createVR) {
                                #if IS_CMPLX
                                #region HCSortEVec
                                System.Diagnostics.Debug.Assert(vr != null);
                                lock (V.SynchObj) {
                                    V.a = empty<complex>(n, n, StorageOrders.ColumnMajor);
                                    
                                    complex* VArr = (complex*)V.GetHostPointerForRead(); // sic! we use VArr for writing, though. 

                                    for (int c = 0; c < n;) {
                                        if (c < n - 1 && wi.GetValue(c) != 0 && wi.GetValue(c + 1) != 0) {
                                            // complex conjugate eigentvalue pair
                                            ilo = (int)n * c; ihi = ilo + (int)n;
                                            for (int r = 0; r < n; r++) {
                                                VArr[ilo].real = vr[ilo];
                                                VArr[ilo].imag = vr[ihi];
                                                VArr[ihi].real = vr[ilo];
                                                VArr[ihi].imag = -vr[ihi];
                                                ilo++; ihi++;
                                            }
                                            c += 2;
                                        } else {
                                            // real eigenvalue
                                            ilo = (int)n * c;
                                            for (int r = 0; r < n; r++) {
                                                VArr[ilo].real = vr[ilo];
                                                VArr[ilo].imag = 0;
                                                ilo++;
                                            }
                                            c++;
                                        }
                                    }
                                    #endregion HYCALPER
                                    GC.KeepAlive(VR);
                                    GC.KeepAlive(V);
                                }
#else
                                lock (V.SynchObj) 
                                    V.a = VR; 
#endif
                                ret.a = diag<complex>(ret);
                            }
                        } finally {
                            if (scaleH != null) free<complex>(scaleH, 0);
                            if (rcondeH != null) free<complex>(rcondeH, 0);
                            if (rcondevH != null) free<complex>(rcondevH, 0);
                        }
                    } // unsafe
                }
                return ret;
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

        #region HYCALPER LOOPSTART symmetric / hermitian eigenproblems

        /// <summary>
        /// Find all eigenvalues of symmetric / hermitian matrix.
        /// </summary>
        /// <param name="A">Input matrix, Size [n x n], symmetric (hermitian for complex <paramref name="A"/>).</param> 
        /// <returns>Array of size [n,1] with eigenvalues of <paramref name="A"/> in ascending order.</returns>
        /// <remarks><para>For computation the Lapack functions dsyevr, ssyevr, chesvr and zheesv are used.</para>
        /// <para>Since <paramref name="A"/> is symmetric the eigenvalues and the array returned have the same real/complex element type as <paramref name="A"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="A"/> is not square.</exception>
        /// <seealso cref="eig(InArray{double}, OutArray{complex}, ref MatrixProperties, bool)"/>
        
        public static Array<Double> eigSymm(InArray<double> A) {
            using (Scope.Enter()) {
                Array<double> _A = A; 
                if (_A.IsEmpty) {
                    return empty<Double>(_A.Size);
                }
                long n = _A.Size[0];
                if (n != _A.Size[1])
                    throw new ArgumentException("eigSymm(): input matrix _A must be square and symmetric/hermitian.");
                int m = 0, info = 0;
                Array<Double> w = empty<Double>(n, StorageOrders.ColumnMajor);
                unsafe {
                    MemoryHandle isuppzH = New<int>(2 * n);
                    double z = (double)1;
                    int* isuppz = (int*)isuppzH.Pointer;
                    Array<double> Acopy = _A.C;

                    /*!HC:lapack_???evr*/
                    Lapack.dsyevr('N', 'A', 'U', (int)n, (double*)Acopy.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)n, 0, 0, 0, 0, 0, ref m, (Double*)w.GetHostPointerForWrite(), &z, 1, isuppz, ref info);

                    // required? 
                    GC.KeepAlive(Acopy); 

                    return (w);
                }
            }
        }
        /// <summary>
        /// Find all eigenvalues and -vectors of a symmetric /hermitian matrix. 
        /// </summary>
        /// <param name="A">Input matrix, Size [n x n], symmetric (hermitian for complex <paramref name="A"/>).</param> 
        /// <param name="V">Output: n eigenvectors in ascending order as columns. Size [n x n]. If <paramref name="V"/> 
        /// is null on input, the eigenvectors will not be computed.</param>
        /// <returns>Diagonal matrix of size [n,n] with eigenvalues of <paramref name="A"/> on the main diagonal.</returns>
        /// <remarks><para>For computation the Lapack functions dsyevr, ssyevr, chesvr and zheesv are used.</para>
        /// <para>Since <paramref name="A"/> is symmetric, eigenvalues are real. The return value will be of the same real/complex type as <paramref name="A"/>.</para></remarks>
        /// <exception cref="ArgumentException">if <paramref name="A"/> is not square.</exception>
        public static Array<Double> eigSymm(InArray<double> A, OutArray<double> V = null) {
            using (Scope.Enter()) {
                Array<double> _A = A;
                if (A.IsEmpty) {
                    if (!object.Equals(V, null))
                        lock (V.SynchObj)
                            V.a = empty<double>(A.Size);
                    return empty<Double>(A.Size);
                }
                if (!A.IsMatrix) {
                    throw new ArgumentException("eigSymm: A must be a matrix.");
                }
                long n = A.Size[0];
                if (n != A.Size[1])
                    throw new ArgumentException("eigSymm: input matrix A must be square and symmetric/hermitian.");
                int m = 0, ldz = 0, info = 0;
                Array<Double> w = empty<Double>(n);
                unsafe {
                    var lockObj = V?.SynchObj ?? new object();
                    lock (lockObj) {
                        double* z = null;
                        char jobz;
                        if (object.Equals(V, null)) {
                            jobz = 'N';
                            ldz = 1;
                        } else {
                            V.a = empty<double>(n, n);
                            z = (double*)V.GetHostPointerForRead();  // sic! using the read pointer for writing here
                            jobz = 'V';
                            ldz = (int)n;
                        }
                        Array<int> isuppz = empty<int>(2 * n);

                        Array<double> Acopy = A.C;

                        double* AcArr = (double*)Acopy.GetHostPointerForWrite(StorageOrders.ColumnMajor);
                        /*!HC:lapack_???evr*/
                        Lapack.dsyevr(jobz, 'A', 'U', (int)n, AcArr, (int)n, 1, n, 0, 0, 0, ref m, (Double*)w.GetHostPointerForWrite(), z, ldz, (int*)isuppz.GetHostPointerForWrite(), ref info);

                        GC.KeepAlive(Acopy);

                        if (info != 0)
                            throw new Exception("error returned from lapack: " + info);

                        //w.a = w[r(0,m)];

                        if (jobz == 'V') {
                            System.Diagnostics.Debug.Assert(!object.Equals(V, null));

                            V.a = V[full, r(0, m - 1)];
                            return diag(w);
                        } else {
                            return w;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Find some eigenvalues and -vectors of symmetric (hermitian) matrix. Filter by (1 based) index of eigenvalues.
        /// </summary>
        /// <param name="A">Input matrix, Size [n x n], symmetric (hermitian for complex <paramref name="A"/>). </param> 
        /// <param name="V">Output: m eigenvectors as columns. Size [n x m]. If <paramref name="V"/> is null on input the eigenvectors will not be computed.</param>
        /// <param name="rangeStart">Determines the lower limit of the index range of eigenvalues to be returned.</param>
        /// <param name="rangeEnd">Determines the upper limit of the index range of eigenvalues to be returned.</param>
        /// <returns>Diagonal matrix of size [n,m] with eigenvalues of <paramref name="A"/> on the main diagonal if <paramref name="V"/> is requested. Otherwise a vector with the first m elements being the requested eigenvalues.</returns>
        /// <remarks><para>For computation the Lapack functions dsyevr, ssyevr, chesvr and zheesv are used.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="A"/> is not square or <paramref name="rangeEnd"/> &lt; <paramref name="rangeStart"/>.</exception>
        public static Array<Double> eigSymm(InArray<double> A, OutArray<double> V, int rangeStart, int rangeEnd) {
            return eigSymm(A, V, rangeStart, rangeEnd, true); 
        }
        /// <summary>
        /// Find some eigenvalues and -vectors of symmetric (hermitian) matrix. Filter by value of eigenvalues.
        /// </summary>
        /// <param name="A">Input matrix, Size [n x n], symmetric (hermitian for complex <paramref name="A"/>). </param> 
        /// <param name="V">Output: m eigenvectors as columns. Size [n x m]. If <paramref name="V"/> is null on input the eigenvectors will not be computed.</param>
        /// <param name="rangeStart">Determines the lower limit of the index range of eigenvalues to be returned.</param>
        /// <param name="rangeEnd">Determines the upper limit of the index range of eigenvalues to be returned.</param>
        /// <returns>Diagonal matrix of size [n,m] with eigenvalues of <paramref name="A"/> on the main diagonal if <paramref name="V"/> is requested. Otherwise a vector with the first m elements being the requested eigenvalues.</returns>
        /// <remarks><para>For computation the Lapack functions dsyevr, ssyevr, chesvr and zheesv are used.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="A"/> is not square or <paramref name="rangeEnd"/> &lt; <paramref name="rangeStart"/>.</exception>
        public static Array<Double> eigSymm(InArray<double> A, OutArray<double> V, Double rangeStart, Double rangeEnd) {
            return eigSymm(A, V, rangeStart, rangeEnd, false);
        }
        /// <summary>
        /// Find some eigenvalues and -vectors of symmetric (hermitian) matrix. Filter by value of eigenvalues.
        /// </summary>
        /// <param name="A">Input matrix, Size [n x n], symmetric (hermitian for complex <paramref name="A"/>). </param> 
        /// <param name="V">Output: m eigenvectors as columns. Size [n x m]. If <paramref name="V"/> is null on input the eigenvectors will not be computed.</param>
        /// <param name="rangeStart">Determines the lower limit of the index range of eigenvalues to be returned.</param>
        /// <param name="rangeEnd">Determines the upper limit of the index range of eigenvalues to be returned.</param>
        /// <param name="index_range">true: filter by indices of eigenvalues, as given by <paramref name="rangeStart"/> and <paramref name="rangeEnd"/>. False: filter by values of eigenvalues.</param>
        /// <returns>Diagonal matrix of size [n,m] with eigenvalues of <paramref name="A"/> on the main diagonal if <paramref name="V"/> is requested. Otherwise a vector with the first m elements being the requested eigenvalues.</returns>
        /// <remarks><para>For computation the Lapack functions dsyevr, ssyevr, chesvr and zheesv are used.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="A"/> is not square or <paramref name="rangeEnd"/> &lt; <paramref name="rangeStart"/>.</exception>
        public static Array<Double> eigSymm(InArray<double> A, OutArray<double> V, Double rangeStart, Double rangeEnd, bool index_range) {
            using (Scope.Enter()) {
                Array<double> _A = A; 
                if (_A.IsEmpty) {
                    if (!object.Equals(V, null))
                        lock (V.SynchObj)
                            V.a = empty<double>(_A.Size);
                    return empty<Double>(_A.Size);
                }
                var n = _A.Size[0];
                if (n != _A.Size[1])
                    throw new ArgumentException("eigSymm: input matrix A must be square and symmetric/hermitian.");
                int m = 0, ldz = 0, info = 0;
                if (rangeEnd < rangeStart || (index_range && rangeStart < 1))
                    throw new ArgumentException("eigSymm: invalid range of eigenvalues requested");
                Array<Double> w = empty<Double>(n, StorageOrders.ColumnMajor);
                unsafe {
                    var lockObj = V?.SynchObj ?? new object();
                    lock (lockObj) {
                        double* z = null;
                        char jobz;
                        if (object.Equals(V, null)) {
                            jobz = 'N';
                            ldz = 1;
                        } else {
                            V.a = empty<double>(n, n, StorageOrders.ColumnMajor);
                            z = (double*)V.GetHostPointerForRead();  // sic! using the read pointer for writing here
                            jobz = 'V';
                            ldz = (int)n;
                        }
                        Array<int> isuppz = empty<int>(2 * n);

                        Array<double> Acopy = _A.C;

                        double* AcArr = (double*)Acopy.GetHostPointerForWrite(StorageOrders.ColumnMajor);
                        if (index_range) {
                            Lapack.dsyevr(jobz, 'I', 'U', (int)n, AcArr, (int)n, 1, n, (int)rangeStart, (int)rangeEnd, 0, ref m, (Double*)w.GetHostPointerForWrite(), z, ldz, (int*)isuppz.GetHostPointerForWrite(), ref info);
                        } else {
                            Lapack.dsyevr(jobz, 'V', 'U', (int)n, AcArr, (int)n, rangeStart, rangeEnd, 0, 0, 0, ref m, (Double*)w.GetHostPointerForWrite(), z, ldz, (int*)isuppz.GetHostPointerForWrite(), ref info);

                        }
                        GC.KeepAlive(Acopy);

                        if (info != 0)
                            throw new Exception("error returned from lapack: " + info);

                        w.a = w[r(0, m - 1)];

                        if (jobz == 'V') {
                            System.Diagnostics.Debug.Assert(!object.Equals(V, null));

                            V.a = V[full, r(0, m - 1)];
                            return diag(w);
                        } else {
                            return w;
                        }
                    }
                }
            }
        }
        #endregion HYCALPER LOOPEND 
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

        /// <summary>
        /// Find all eigenvalues of symmetric / hermitian matrix.
        /// </summary>
        /// <param name="A">Input matrix, Size [n x n], symmetric (hermitian for fcomplex <paramref name="A"/>).</param> 
        /// <returns>Array of size [n,1] with eigenvalues of <paramref name="A"/> in ascending order.</returns>
        /// <remarks><para>For computation the Lapack functions ssyevr, ssyevr, chesvr and zheesv are used.</para>
        /// <para>Since <paramref name="A"/> is symmetric the eigenvalues and the array returned have the same real/fcomplex element type as <paramref name="A"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="A"/> is not square.</exception>
        /// <seealso cref="eig(InArray{float}, OutArray{fcomplex}, ref MatrixProperties, bool)"/>
        
        public static Array<Single> eigSymm(InArray<float> A) {
            using (Scope.Enter()) {
                Array<float> _A = A; 
                if (_A.IsEmpty) {
                    return empty<Single>(_A.Size);
                }
                long n = _A.Size[0];
                if (n != _A.Size[1])
                    throw new ArgumentException("eigSymm(): input matrix _A must be square and symmetric/hermitian.");
                int m = 0, info = 0;
                Array<Single> w = empty<Single>(n, StorageOrders.ColumnMajor);
                unsafe {
                    MemoryHandle isuppzH = New<int>(2 * n);
                    float z = (float)1;
                    int* isuppz = (int*)isuppzH.Pointer;
                    Array<float> Acopy = _A.C;

                   
                    Lapack.ssyevr('N', 'A', 'U', (int)n, (float*)Acopy.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)n, 0, 0, 0, 0, 0, ref m, (Single*)w.GetHostPointerForWrite(), &z, 1, isuppz, ref info);

                    // required? 
                    GC.KeepAlive(Acopy); 

                    return (w);
                }
            }
        }
        /// <summary>
        /// Find all eigenvalues and -vectors of a symmetric /hermitian matrix. 
        /// </summary>
        /// <param name="A">Input matrix, Size [n x n], symmetric (hermitian for fcomplex <paramref name="A"/>).</param> 
        /// <param name="V">Output: n eigenvectors in ascending order as columns. Size [n x n]. If <paramref name="V"/> 
        /// is null on input, the eigenvectors will not be computed.</param>
        /// <returns>Diagonal matrix of size [n,n] with eigenvalues of <paramref name="A"/> on the main diagonal.</returns>
        /// <remarks><para>For computation the Lapack functions ssyevr, ssyevr, chesvr and zheesv are used.</para>
        /// <para>Since <paramref name="A"/> is symmetric, eigenvalues are real. The return value will be of the same real/fcomplex type as <paramref name="A"/>.</para></remarks>
        /// <exception cref="ArgumentException">if <paramref name="A"/> is not square.</exception>
        public static Array<Single> eigSymm(InArray<float> A, OutArray<float> V = null) {
            using (Scope.Enter()) {
                Array<float> _A = A;
                if (A.IsEmpty) {
                    if (!object.Equals(V, null))
                        lock (V.SynchObj)
                            V.a = empty<float>(A.Size);
                    return empty<Single>(A.Size);
                }
                if (!A.IsMatrix) {
                    throw new ArgumentException("eigSymm: A must be a matrix.");
                }
                long n = A.Size[0];
                if (n != A.Size[1])
                    throw new ArgumentException("eigSymm: input matrix A must be square and symmetric/hermitian.");
                int m = 0, ldz = 0, info = 0;
                Array<Single> w = empty<Single>(n);
                unsafe {
                    var lockObj = V?.SynchObj ?? new object();
                    lock (lockObj) {
                        float* z = null;
                        char jobz;
                        if (object.Equals(V, null)) {
                            jobz = 'N';
                            ldz = 1;
                        } else {
                            V.a = empty<float>(n, n);
                            z = (float*)V.GetHostPointerForRead();  // sic! using the read pointer for writing here
                            jobz = 'V';
                            ldz = (int)n;
                        }
                        Array<int> isuppz = empty<int>(2 * n);

                        Array<float> Acopy = A.C;

                        float* AcArr = (float*)Acopy.GetHostPointerForWrite(StorageOrders.ColumnMajor);
                       
                        Lapack.ssyevr(jobz, 'A', 'U', (int)n, AcArr, (int)n, 1, n, 0, 0, 0, ref m, (Single*)w.GetHostPointerForWrite(), z, ldz, (int*)isuppz.GetHostPointerForWrite(), ref info);

                        GC.KeepAlive(Acopy);

                        if (info != 0)
                            throw new Exception("error returned from lapack: " + info);

                        //w.a = w[r(0,m)];

                        if (jobz == 'V') {
                            System.Diagnostics.Debug.Assert(!object.Equals(V, null));

                            V.a = V[full, r(0, m - 1)];
                            return diag(w);
                        } else {
                            return w;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Find some eigenvalues and -vectors of symmetric (hermitian) matrix. Filter by (1 based) index of eigenvalues.
        /// </summary>
        /// <param name="A">Input matrix, Size [n x n], symmetric (hermitian for fcomplex <paramref name="A"/>). </param> 
        /// <param name="V">Output: m eigenvectors as columns. Size [n x m]. If <paramref name="V"/> is null on input the eigenvectors will not be computed.</param>
        /// <param name="rangeStart">Determines the lower limit of the index range of eigenvalues to be returned.</param>
        /// <param name="rangeEnd">Determines the upper limit of the index range of eigenvalues to be returned.</param>
        /// <returns>Diagonal matrix of size [n,m] with eigenvalues of <paramref name="A"/> on the main diagonal if <paramref name="V"/> is requested. Otherwise a vector with the first m elements being the requested eigenvalues.</returns>
        /// <remarks><para>For computation the Lapack functions ssyevr, ssyevr, chesvr and zheesv are used.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="A"/> is not square or <paramref name="rangeEnd"/> &lt; <paramref name="rangeStart"/>.</exception>
        public static Array<Single> eigSymm(InArray<float> A, OutArray<float> V, int rangeStart, int rangeEnd) {
            return eigSymm(A, V, rangeStart, rangeEnd, true); 
        }
        /// <summary>
        /// Find some eigenvalues and -vectors of symmetric (hermitian) matrix. Filter by value of eigenvalues.
        /// </summary>
        /// <param name="A">Input matrix, Size [n x n], symmetric (hermitian for fcomplex <paramref name="A"/>). </param> 
        /// <param name="V">Output: m eigenvectors as columns. Size [n x m]. If <paramref name="V"/> is null on input the eigenvectors will not be computed.</param>
        /// <param name="rangeStart">Determines the lower limit of the index range of eigenvalues to be returned.</param>
        /// <param name="rangeEnd">Determines the upper limit of the index range of eigenvalues to be returned.</param>
        /// <returns>Diagonal matrix of size [n,m] with eigenvalues of <paramref name="A"/> on the main diagonal if <paramref name="V"/> is requested. Otherwise a vector with the first m elements being the requested eigenvalues.</returns>
        /// <remarks><para>For computation the Lapack functions ssyevr, ssyevr, chesvr and zheesv are used.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="A"/> is not square or <paramref name="rangeEnd"/> &lt; <paramref name="rangeStart"/>.</exception>
        public static Array<Single> eigSymm(InArray<float> A, OutArray<float> V, Single rangeStart, Single rangeEnd) {
            return eigSymm(A, V, rangeStart, rangeEnd, false);
        }
        /// <summary>
        /// Find some eigenvalues and -vectors of symmetric (hermitian) matrix. Filter by value of eigenvalues.
        /// </summary>
        /// <param name="A">Input matrix, Size [n x n], symmetric (hermitian for fcomplex <paramref name="A"/>). </param> 
        /// <param name="V">Output: m eigenvectors as columns. Size [n x m]. If <paramref name="V"/> is null on input the eigenvectors will not be computed.</param>
        /// <param name="rangeStart">Determines the lower limit of the index range of eigenvalues to be returned.</param>
        /// <param name="rangeEnd">Determines the upper limit of the index range of eigenvalues to be returned.</param>
        /// <param name="index_range">true: filter by indices of eigenvalues, as given by <paramref name="rangeStart"/> and <paramref name="rangeEnd"/>. False: filter by values of eigenvalues.</param>
        /// <returns>Diagonal matrix of size [n,m] with eigenvalues of <paramref name="A"/> on the main diagonal if <paramref name="V"/> is requested. Otherwise a vector with the first m elements being the requested eigenvalues.</returns>
        /// <remarks><para>For computation the Lapack functions ssyevr, ssyevr, chesvr and zheesv are used.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="A"/> is not square or <paramref name="rangeEnd"/> &lt; <paramref name="rangeStart"/>.</exception>
        public static Array<Single> eigSymm(InArray<float> A, OutArray<float> V, Single rangeStart, Single rangeEnd, bool index_range) {
            using (Scope.Enter()) {
                Array<float> _A = A; 
                if (_A.IsEmpty) {
                    if (!object.Equals(V, null))
                        lock (V.SynchObj)
                            V.a = empty<float>(_A.Size);
                    return empty<Single>(_A.Size);
                }
                var n = _A.Size[0];
                if (n != _A.Size[1])
                    throw new ArgumentException("eigSymm: input matrix A must be square and symmetric/hermitian.");
                int m = 0, ldz = 0, info = 0;
                if (rangeEnd < rangeStart || (index_range && rangeStart < 1))
                    throw new ArgumentException("eigSymm: invalid range of eigenvalues requested");
                Array<Single> w = empty<Single>(n, StorageOrders.ColumnMajor);
                unsafe {
                    var lockObj = V?.SynchObj ?? new object();
                    lock (lockObj) {
                        float* z = null;
                        char jobz;
                        if (object.Equals(V, null)) {
                            jobz = 'N';
                            ldz = 1;
                        } else {
                            V.a = empty<float>(n, n, StorageOrders.ColumnMajor);
                            z = (float*)V.GetHostPointerForRead();  // sic! using the read pointer for writing here
                            jobz = 'V';
                            ldz = (int)n;
                        }
                        Array<int> isuppz = empty<int>(2 * n);

                        Array<float> Acopy = _A.C;

                        float* AcArr = (float*)Acopy.GetHostPointerForWrite(StorageOrders.ColumnMajor);
                        if (index_range) {
                            Lapack.ssyevr(jobz, 'I', 'U', (int)n, AcArr, (int)n, 1, n, (int)rangeStart, (int)rangeEnd, 0, ref m, (Single*)w.GetHostPointerForWrite(), z, ldz, (int*)isuppz.GetHostPointerForWrite(), ref info);
                        } else {
                            Lapack.ssyevr(jobz, 'V', 'U', (int)n, AcArr, (int)n, rangeStart, rangeEnd, 0, 0, 0, ref m, (Single*)w.GetHostPointerForWrite(), z, ldz, (int*)isuppz.GetHostPointerForWrite(), ref info);

                        }
                        GC.KeepAlive(Acopy);

                        if (info != 0)
                            throw new Exception("error returned from lapack: " + info);

                        w.a = w[r(0, m - 1)];

                        if (jobz == 'V') {
                            System.Diagnostics.Debug.Assert(!object.Equals(V, null));

                            V.a = V[full, r(0, m - 1)];
                            return diag(w);
                        } else {
                            return w;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Find all eigenvalues of symmetric / hermitian matrix.
        /// </summary>
        /// <param name="A">Input matrix, Size [n x n], symmetric (hermitian for fcomplex <paramref name="A"/>).</param> 
        /// <returns>Array of size [n,1] with eigenvalues of <paramref name="A"/> in ascending order.</returns>
        /// <remarks><para>For computation the Lapack functions cheevr, ssyevr, chesvr and zheesv are used.</para>
        /// <para>Since <paramref name="A"/> is symmetric the eigenvalues and the array returned have the same real/fcomplex element type as <paramref name="A"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="A"/> is not square.</exception>
        /// <seealso cref="eig(InArray{fcomplex}, OutArray{fcomplex}, ref MatrixProperties, bool)"/>
        
        public static Array<Single> eigSymm(InArray<fcomplex> A) {
            using (Scope.Enter()) {
                Array<fcomplex> _A = A; 
                if (_A.IsEmpty) {
                    return empty<Single>(_A.Size);
                }
                long n = _A.Size[0];
                if (n != _A.Size[1])
                    throw new ArgumentException("eigSymm(): input matrix _A must be square and symmetric/hermitian.");
                int m = 0, info = 0;
                Array<Single> w = empty<Single>(n, StorageOrders.ColumnMajor);
                unsafe {
                    MemoryHandle isuppzH = New<int>(2 * n);
                    fcomplex z = (fcomplex)1;
                    int* isuppz = (int*)isuppzH.Pointer;
                    Array<fcomplex> Acopy = _A.C;

                   
                    Lapack.cheevr('N', 'A', 'U', (int)n, (fcomplex*)Acopy.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)n, 0, 0, 0, 0, 0, ref m, (Single*)w.GetHostPointerForWrite(), &z, 1, isuppz, ref info);

                    // required? 
                    GC.KeepAlive(Acopy); 

                    return (w);
                }
            }
        }
        /// <summary>
        /// Find all eigenvalues and -vectors of a symmetric /hermitian matrix. 
        /// </summary>
        /// <param name="A">Input matrix, Size [n x n], symmetric (hermitian for fcomplex <paramref name="A"/>).</param> 
        /// <param name="V">Output: n eigenvectors in ascending order as columns. Size [n x n]. If <paramref name="V"/> 
        /// is null on input, the eigenvectors will not be computed.</param>
        /// <returns>Diagonal matrix of size [n,n] with eigenvalues of <paramref name="A"/> on the main diagonal.</returns>
        /// <remarks><para>For computation the Lapack functions cheevr, ssyevr, chesvr and zheesv are used.</para>
        /// <para>Since <paramref name="A"/> is symmetric, eigenvalues are real. The return value will be of the same real/fcomplex type as <paramref name="A"/>.</para></remarks>
        /// <exception cref="ArgumentException">if <paramref name="A"/> is not square.</exception>
        public static Array<Single> eigSymm(InArray<fcomplex> A, OutArray<fcomplex> V = null) {
            using (Scope.Enter()) {
                Array<fcomplex> _A = A;
                if (A.IsEmpty) {
                    if (!object.Equals(V, null))
                        lock (V.SynchObj)
                            V.a = empty<fcomplex>(A.Size);
                    return empty<Single>(A.Size);
                }
                if (!A.IsMatrix) {
                    throw new ArgumentException("eigSymm: A must be a matrix.");
                }
                long n = A.Size[0];
                if (n != A.Size[1])
                    throw new ArgumentException("eigSymm: input matrix A must be square and symmetric/hermitian.");
                int m = 0, ldz = 0, info = 0;
                Array<Single> w = empty<Single>(n);
                unsafe {
                    var lockObj = V?.SynchObj ?? new object();
                    lock (lockObj) {
                        fcomplex* z = null;
                        char jobz;
                        if (object.Equals(V, null)) {
                            jobz = 'N';
                            ldz = 1;
                        } else {
                            V.a = empty<fcomplex>(n, n);
                            z = (fcomplex*)V.GetHostPointerForRead();  // sic! using the read pointer for writing here
                            jobz = 'V';
                            ldz = (int)n;
                        }
                        Array<int> isuppz = empty<int>(2 * n);

                        Array<fcomplex> Acopy = A.C;

                        fcomplex* AcArr = (fcomplex*)Acopy.GetHostPointerForWrite(StorageOrders.ColumnMajor);
                       
                        Lapack.cheevr(jobz, 'A', 'U', (int)n, AcArr, (int)n, 1, n, 0, 0, 0, ref m, (Single*)w.GetHostPointerForWrite(), z, ldz, (int*)isuppz.GetHostPointerForWrite(), ref info);

                        GC.KeepAlive(Acopy);

                        if (info != 0)
                            throw new Exception("error returned from lapack: " + info);

                        //w.a = w[r(0,m)];

                        if (jobz == 'V') {
                            System.Diagnostics.Debug.Assert(!object.Equals(V, null));

                            V.a = V[full, r(0, m - 1)];
                            return diag(w);
                        } else {
                            return w;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Find some eigenvalues and -vectors of symmetric (hermitian) matrix. Filter by (1 based) index of eigenvalues.
        /// </summary>
        /// <param name="A">Input matrix, Size [n x n], symmetric (hermitian for fcomplex <paramref name="A"/>). </param> 
        /// <param name="V">Output: m eigenvectors as columns. Size [n x m]. If <paramref name="V"/> is null on input the eigenvectors will not be computed.</param>
        /// <param name="rangeStart">Determines the lower limit of the index range of eigenvalues to be returned.</param>
        /// <param name="rangeEnd">Determines the upper limit of the index range of eigenvalues to be returned.</param>
        /// <returns>Diagonal matrix of size [n,m] with eigenvalues of <paramref name="A"/> on the main diagonal if <paramref name="V"/> is requested. Otherwise a vector with the first m elements being the requested eigenvalues.</returns>
        /// <remarks><para>For computation the Lapack functions cheevr, ssyevr, chesvr and zheesv are used.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="A"/> is not square or <paramref name="rangeEnd"/> &lt; <paramref name="rangeStart"/>.</exception>
        public static Array<Single> eigSymm(InArray<fcomplex> A, OutArray<fcomplex> V, int rangeStart, int rangeEnd) {
            return eigSymm(A, V, rangeStart, rangeEnd, true); 
        }
        /// <summary>
        /// Find some eigenvalues and -vectors of symmetric (hermitian) matrix. Filter by value of eigenvalues.
        /// </summary>
        /// <param name="A">Input matrix, Size [n x n], symmetric (hermitian for fcomplex <paramref name="A"/>). </param> 
        /// <param name="V">Output: m eigenvectors as columns. Size [n x m]. If <paramref name="V"/> is null on input the eigenvectors will not be computed.</param>
        /// <param name="rangeStart">Determines the lower limit of the index range of eigenvalues to be returned.</param>
        /// <param name="rangeEnd">Determines the upper limit of the index range of eigenvalues to be returned.</param>
        /// <returns>Diagonal matrix of size [n,m] with eigenvalues of <paramref name="A"/> on the main diagonal if <paramref name="V"/> is requested. Otherwise a vector with the first m elements being the requested eigenvalues.</returns>
        /// <remarks><para>For computation the Lapack functions cheevr, ssyevr, chesvr and zheesv are used.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="A"/> is not square or <paramref name="rangeEnd"/> &lt; <paramref name="rangeStart"/>.</exception>
        public static Array<Single> eigSymm(InArray<fcomplex> A, OutArray<fcomplex> V, Single rangeStart, Single rangeEnd) {
            return eigSymm(A, V, rangeStart, rangeEnd, false);
        }
        /// <summary>
        /// Find some eigenvalues and -vectors of symmetric (hermitian) matrix. Filter by value of eigenvalues.
        /// </summary>
        /// <param name="A">Input matrix, Size [n x n], symmetric (hermitian for fcomplex <paramref name="A"/>). </param> 
        /// <param name="V">Output: m eigenvectors as columns. Size [n x m]. If <paramref name="V"/> is null on input the eigenvectors will not be computed.</param>
        /// <param name="rangeStart">Determines the lower limit of the index range of eigenvalues to be returned.</param>
        /// <param name="rangeEnd">Determines the upper limit of the index range of eigenvalues to be returned.</param>
        /// <param name="index_range">true: filter by indices of eigenvalues, as given by <paramref name="rangeStart"/> and <paramref name="rangeEnd"/>. False: filter by values of eigenvalues.</param>
        /// <returns>Diagonal matrix of size [n,m] with eigenvalues of <paramref name="A"/> on the main diagonal if <paramref name="V"/> is requested. Otherwise a vector with the first m elements being the requested eigenvalues.</returns>
        /// <remarks><para>For computation the Lapack functions cheevr, ssyevr, chesvr and zheesv are used.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="A"/> is not square or <paramref name="rangeEnd"/> &lt; <paramref name="rangeStart"/>.</exception>
        public static Array<Single> eigSymm(InArray<fcomplex> A, OutArray<fcomplex> V, Single rangeStart, Single rangeEnd, bool index_range) {
            using (Scope.Enter()) {
                Array<fcomplex> _A = A; 
                if (_A.IsEmpty) {
                    if (!object.Equals(V, null))
                        lock (V.SynchObj)
                            V.a = empty<fcomplex>(_A.Size);
                    return empty<Single>(_A.Size);
                }
                var n = _A.Size[0];
                if (n != _A.Size[1])
                    throw new ArgumentException("eigSymm: input matrix A must be square and symmetric/hermitian.");
                int m = 0, ldz = 0, info = 0;
                if (rangeEnd < rangeStart || (index_range && rangeStart < 1))
                    throw new ArgumentException("eigSymm: invalid range of eigenvalues requested");
                Array<Single> w = empty<Single>(n, StorageOrders.ColumnMajor);
                unsafe {
                    var lockObj = V?.SynchObj ?? new object();
                    lock (lockObj) {
                        fcomplex* z = null;
                        char jobz;
                        if (object.Equals(V, null)) {
                            jobz = 'N';
                            ldz = 1;
                        } else {
                            V.a = empty<fcomplex>(n, n, StorageOrders.ColumnMajor);
                            z = (fcomplex*)V.GetHostPointerForRead();  // sic! using the read pointer for writing here
                            jobz = 'V';
                            ldz = (int)n;
                        }
                        Array<int> isuppz = empty<int>(2 * n);

                        Array<fcomplex> Acopy = _A.C;

                        fcomplex* AcArr = (fcomplex*)Acopy.GetHostPointerForWrite(StorageOrders.ColumnMajor);
                        if (index_range) {
                            Lapack.cheevr(jobz, 'I', 'U', (int)n, AcArr, (int)n, 1, n, (int)rangeStart, (int)rangeEnd, 0, ref m, (Single*)w.GetHostPointerForWrite(), z, ldz, (int*)isuppz.GetHostPointerForWrite(), ref info);
                        } else {
                            Lapack.cheevr(jobz, 'V', 'U', (int)n, AcArr, (int)n, rangeStart, rangeEnd, 0, 0, 0, ref m, (Single*)w.GetHostPointerForWrite(), z, ldz, (int*)isuppz.GetHostPointerForWrite(), ref info);

                        }
                        GC.KeepAlive(Acopy);

                        if (info != 0)
                            throw new Exception("error returned from lapack: " + info);

                        w.a = w[r(0, m - 1)];

                        if (jobz == 'V') {
                            System.Diagnostics.Debug.Assert(!object.Equals(V, null));

                            V.a = V[full, r(0, m - 1)];
                            return diag(w);
                        } else {
                            return w;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Find all eigenvalues of symmetric / hermitian matrix.
        /// </summary>
        /// <param name="A">Input matrix, Size [n x n], symmetric (hermitian for complex <paramref name="A"/>).</param> 
        /// <returns>Array of size [n,1] with eigenvalues of <paramref name="A"/> in ascending order.</returns>
        /// <remarks><para>For computation the Lapack functions zheevr, ssyevr, chesvr and zheesv are used.</para>
        /// <para>Since <paramref name="A"/> is symmetric the eigenvalues and the array returned have the same real/complex element type as <paramref name="A"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="A"/> is not square.</exception>
        /// <seealso cref="eig(InArray{complex}, OutArray{complex}, ref MatrixProperties, bool)"/>
        
        public static Array<Double> eigSymm(InArray<complex> A) {
            using (Scope.Enter()) {
                Array<complex> _A = A; 
                if (_A.IsEmpty) {
                    return empty<Double>(_A.Size);
                }
                long n = _A.Size[0];
                if (n != _A.Size[1])
                    throw new ArgumentException("eigSymm(): input matrix _A must be square and symmetric/hermitian.");
                int m = 0, info = 0;
                Array<Double> w = empty<Double>(n, StorageOrders.ColumnMajor);
                unsafe {
                    MemoryHandle isuppzH = New<int>(2 * n);
                    complex z = (complex)1;
                    int* isuppz = (int*)isuppzH.Pointer;
                    Array<complex> Acopy = _A.C;

                   
                    Lapack.zheevr('N', 'A', 'U', (int)n, (complex*)Acopy.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)n, 0, 0, 0, 0, 0, ref m, (Double*)w.GetHostPointerForWrite(), &z, 1, isuppz, ref info);

                    // required? 
                    GC.KeepAlive(Acopy); 

                    return (w);
                }
            }
        }
        /// <summary>
        /// Find all eigenvalues and -vectors of a symmetric /hermitian matrix. 
        /// </summary>
        /// <param name="A">Input matrix, Size [n x n], symmetric (hermitian for complex <paramref name="A"/>).</param> 
        /// <param name="V">Output: n eigenvectors in ascending order as columns. Size [n x n]. If <paramref name="V"/> 
        /// is null on input, the eigenvectors will not be computed.</param>
        /// <returns>Diagonal matrix of size [n,n] with eigenvalues of <paramref name="A"/> on the main diagonal.</returns>
        /// <remarks><para>For computation the Lapack functions zheevr, ssyevr, chesvr and zheesv are used.</para>
        /// <para>Since <paramref name="A"/> is symmetric, eigenvalues are real. The return value will be of the same real/complex type as <paramref name="A"/>.</para></remarks>
        /// <exception cref="ArgumentException">if <paramref name="A"/> is not square.</exception>
        public static Array<Double> eigSymm(InArray<complex> A, OutArray<complex> V = null) {
            using (Scope.Enter()) {
                Array<complex> _A = A;
                if (A.IsEmpty) {
                    if (!object.Equals(V, null))
                        lock (V.SynchObj)
                            V.a = empty<complex>(A.Size);
                    return empty<Double>(A.Size);
                }
                if (!A.IsMatrix) {
                    throw new ArgumentException("eigSymm: A must be a matrix.");
                }
                long n = A.Size[0];
                if (n != A.Size[1])
                    throw new ArgumentException("eigSymm: input matrix A must be square and symmetric/hermitian.");
                int m = 0, ldz = 0, info = 0;
                Array<Double> w = empty<Double>(n);
                unsafe {
                    var lockObj = V?.SynchObj ?? new object();
                    lock (lockObj) {
                        complex* z = null;
                        char jobz;
                        if (object.Equals(V, null)) {
                            jobz = 'N';
                            ldz = 1;
                        } else {
                            V.a = empty<complex>(n, n);
                            z = (complex*)V.GetHostPointerForRead();  // sic! using the read pointer for writing here
                            jobz = 'V';
                            ldz = (int)n;
                        }
                        Array<int> isuppz = empty<int>(2 * n);

                        Array<complex> Acopy = A.C;

                        complex* AcArr = (complex*)Acopy.GetHostPointerForWrite(StorageOrders.ColumnMajor);
                       
                        Lapack.zheevr(jobz, 'A', 'U', (int)n, AcArr, (int)n, 1, n, 0, 0, 0, ref m, (Double*)w.GetHostPointerForWrite(), z, ldz, (int*)isuppz.GetHostPointerForWrite(), ref info);

                        GC.KeepAlive(Acopy);

                        if (info != 0)
                            throw new Exception("error returned from lapack: " + info);

                        //w.a = w[r(0,m)];

                        if (jobz == 'V') {
                            System.Diagnostics.Debug.Assert(!object.Equals(V, null));

                            V.a = V[full, r(0, m - 1)];
                            return diag(w);
                        } else {
                            return w;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Find some eigenvalues and -vectors of symmetric (hermitian) matrix. Filter by (1 based) index of eigenvalues.
        /// </summary>
        /// <param name="A">Input matrix, Size [n x n], symmetric (hermitian for complex <paramref name="A"/>). </param> 
        /// <param name="V">Output: m eigenvectors as columns. Size [n x m]. If <paramref name="V"/> is null on input the eigenvectors will not be computed.</param>
        /// <param name="rangeStart">Determines the lower limit of the index range of eigenvalues to be returned.</param>
        /// <param name="rangeEnd">Determines the upper limit of the index range of eigenvalues to be returned.</param>
        /// <returns>Diagonal matrix of size [n,m] with eigenvalues of <paramref name="A"/> on the main diagonal if <paramref name="V"/> is requested. Otherwise a vector with the first m elements being the requested eigenvalues.</returns>
        /// <remarks><para>For computation the Lapack functions zheevr, ssyevr, chesvr and zheesv are used.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="A"/> is not square or <paramref name="rangeEnd"/> &lt; <paramref name="rangeStart"/>.</exception>
        public static Array<Double> eigSymm(InArray<complex> A, OutArray<complex> V, int rangeStart, int rangeEnd) {
            return eigSymm(A, V, rangeStart, rangeEnd, true); 
        }
        /// <summary>
        /// Find some eigenvalues and -vectors of symmetric (hermitian) matrix. Filter by value of eigenvalues.
        /// </summary>
        /// <param name="A">Input matrix, Size [n x n], symmetric (hermitian for complex <paramref name="A"/>). </param> 
        /// <param name="V">Output: m eigenvectors as columns. Size [n x m]. If <paramref name="V"/> is null on input the eigenvectors will not be computed.</param>
        /// <param name="rangeStart">Determines the lower limit of the index range of eigenvalues to be returned.</param>
        /// <param name="rangeEnd">Determines the upper limit of the index range of eigenvalues to be returned.</param>
        /// <returns>Diagonal matrix of size [n,m] with eigenvalues of <paramref name="A"/> on the main diagonal if <paramref name="V"/> is requested. Otherwise a vector with the first m elements being the requested eigenvalues.</returns>
        /// <remarks><para>For computation the Lapack functions zheevr, ssyevr, chesvr and zheesv are used.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="A"/> is not square or <paramref name="rangeEnd"/> &lt; <paramref name="rangeStart"/>.</exception>
        public static Array<Double> eigSymm(InArray<complex> A, OutArray<complex> V, Double rangeStart, Double rangeEnd) {
            return eigSymm(A, V, rangeStart, rangeEnd, false);
        }
        /// <summary>
        /// Find some eigenvalues and -vectors of symmetric (hermitian) matrix. Filter by value of eigenvalues.
        /// </summary>
        /// <param name="A">Input matrix, Size [n x n], symmetric (hermitian for complex <paramref name="A"/>). </param> 
        /// <param name="V">Output: m eigenvectors as columns. Size [n x m]. If <paramref name="V"/> is null on input the eigenvectors will not be computed.</param>
        /// <param name="rangeStart">Determines the lower limit of the index range of eigenvalues to be returned.</param>
        /// <param name="rangeEnd">Determines the upper limit of the index range of eigenvalues to be returned.</param>
        /// <param name="index_range">true: filter by indices of eigenvalues, as given by <paramref name="rangeStart"/> and <paramref name="rangeEnd"/>. False: filter by values of eigenvalues.</param>
        /// <returns>Diagonal matrix of size [n,m] with eigenvalues of <paramref name="A"/> on the main diagonal if <paramref name="V"/> is requested. Otherwise a vector with the first m elements being the requested eigenvalues.</returns>
        /// <remarks><para>For computation the Lapack functions zheevr, ssyevr, chesvr and zheesv are used.</para>
        /// </remarks>
        /// <exception cref="ArgumentException">if <paramref name="A"/> is not square or <paramref name="rangeEnd"/> &lt; <paramref name="rangeStart"/>.</exception>
        public static Array<Double> eigSymm(InArray<complex> A, OutArray<complex> V, Double rangeStart, Double rangeEnd, bool index_range) {
            using (Scope.Enter()) {
                Array<complex> _A = A; 
                if (_A.IsEmpty) {
                    if (!object.Equals(V, null))
                        lock (V.SynchObj)
                            V.a = empty<complex>(_A.Size);
                    return empty<Double>(_A.Size);
                }
                var n = _A.Size[0];
                if (n != _A.Size[1])
                    throw new ArgumentException("eigSymm: input matrix A must be square and symmetric/hermitian.");
                int m = 0, ldz = 0, info = 0;
                if (rangeEnd < rangeStart || (index_range && rangeStart < 1))
                    throw new ArgumentException("eigSymm: invalid range of eigenvalues requested");
                Array<Double> w = empty<Double>(n, StorageOrders.ColumnMajor);
                unsafe {
                    var lockObj = V?.SynchObj ?? new object();
                    lock (lockObj) {
                        complex* z = null;
                        char jobz;
                        if (object.Equals(V, null)) {
                            jobz = 'N';
                            ldz = 1;
                        } else {
                            V.a = empty<complex>(n, n, StorageOrders.ColumnMajor);
                            z = (complex*)V.GetHostPointerForRead();  // sic! using the read pointer for writing here
                            jobz = 'V';
                            ldz = (int)n;
                        }
                        Array<int> isuppz = empty<int>(2 * n);

                        Array<complex> Acopy = _A.C;

                        complex* AcArr = (complex*)Acopy.GetHostPointerForWrite(StorageOrders.ColumnMajor);
                        if (index_range) {
                            Lapack.zheevr(jobz, 'I', 'U', (int)n, AcArr, (int)n, 1, n, (int)rangeStart, (int)rangeEnd, 0, ref m, (Double*)w.GetHostPointerForWrite(), z, ldz, (int*)isuppz.GetHostPointerForWrite(), ref info);
                        } else {
                            Lapack.zheevr(jobz, 'V', 'U', (int)n, AcArr, (int)n, rangeStart, rangeEnd, 0, 0, 0, ref m, (Double*)w.GetHostPointerForWrite(), z, ldz, (int*)isuppz.GetHostPointerForWrite(), ref info);

                        }
                        GC.KeepAlive(Acopy);

                        if (info != 0)
                            throw new Exception("error returned from lapack: " + info);

                        w.a = w[r(0, m - 1)];

                        if (jobz == 'V') {
                            System.Diagnostics.Debug.Assert(!object.Equals(V, null));

                            V.a = V[full, r(0, m - 1)];
                            return diag(w);
                        } else {
                            return w;
                        }
                    }
                }
            }
        }

#endregion HYCALPER AUTO GENERATED CODE

        #region HYCALPER LOOPSTART generalized eigenproblems

        /*!HC:TYPELIST:
        <hycalper>
        <type>
            <source locate="here">
                complex
            </source>
            <destination>complex</destination>
            <destination>fcomplex</destination>
            <destination>fcomplex</destination>
        </type>
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
            <destination>Double</destination>
            <destination>Single</destination>
            <destination>Single</destination>
        </type>
        <type>
            <source locate="here">
                dsygv
            </source>
            <destination>zhegv</destination>
            <destination>chegv</destination>
            <destination>ssygv</destination>
        </type>
        </hycalper>
         */

        /// <summary>
        /// Computes eigenvalues and eigenvectors (optional) of symmetric/hermitian inputs <paramref name="A"/> and <paramref name="B"/>: A*V=lamda*B*V. 
        /// </summary>
        /// <param name="A">Square, symmetric/hermitian input matrix, size [n x n].</param>
        /// <param name="B">Square, symmetric/hermitian and positive definite matrix, size [n x n].</param>
        /// <param name="V">[Output, optional] Returns the eigenvectors in columns (size [n x n]). Default: (null) eigenvectors are not returned.</param>
        /// <param name="type">[Optional] Determines the eigen problem type. Default: (null) Ax_eq_lambBx: A*V = r*B*V.</param>
        /// <param name="skipSymmCheck">[Optional] true: skip tests for <paramref name="A"/> and <paramref name="B"/> being hermitian. Default: false.</param>
        /// <returns>Vector [n] of eigenvalues or a diagonal matrix [n,n] with the eigenvalues on the main diagonal if <paramref name="V"/> is not null.</returns>
        /// <remarks><para>This function solves the generalized eigenproblem A*V=lamda*B*V or the problem as specified by <paramref name="type"/>. </para>
        /// <para>Eigenvectors in <paramref name="V"/> are not normalized! See details about the normalization performed by the underlying Lapack routines.</para>
        /// <para>Internally, the generalized eigenproblem A*V = r*B*V will be reduced to B<sup>-1</sup>*A*V = r*V using cholesky factorization. The 
        /// computations are handled by LAPACK functions DSYGV,SSYGV,CHEGV and ZHEGV respectively.</para></remarks>
        /// <exception cref="ArgumentException">if <paramref name="B"/> was not positive definite, if <paramref name="A"/> and <paramref name="B"/> was not of the same size, 
        /// if either <paramref name="A"/> and/or <paramref name="B"/> was found not to be symmetric/hermitian, or if the algorithm did not converge. All 
        /// exceptions will give additional error details in their exception message.</exception>
        /// <seealso cref="GenEigenType"/>
        /// <seealso cref="eigSymm(InArray{double})"/>
        /// <seealso cref="eig(InArray{double}, OutArray{complex}, ref MatrixProperties, bool)"/>
        /// <seealso cref="chol(InArray{double}, bool)"/>
        public static Array<Double> eigGen(InArray<double> A, InArray<double> B, 
                                                              OutArray<double> V = null, GenEigenType type = GenEigenType.Ax_eq_lambBx, 
                                                              bool skipSymmCheck = false) {
            using (Scope.Enter()) {
                // check input arguments
                if (object.Equals(A, null) || object.Equals(B, null))
                    throw new ArgumentException($"eigGen A and B must not be null!");

                Array<double> _A = A, _B = B; 
                if (!_A.IsMatrix || !_B.IsMatrix)
                    throw new ArgumentException("eigGen(): A & B must be matrices!");
                var n = _A.Size[0];
                if (n != _A.Size[1])
                    throw new ArgumentException("eigGen(): input matrices must be square!");
                if (!_A.Size.IsSameSize(_B.Size))
                    throw new ArgumentException("eigGen(): A and B must have the same size!");
                if (_A.IsEmpty) {
                    if (object.Equals(V, null))
                        return empty<Double>(_A.Size);
                    else {
                        lock (V.SynchObj)
                            V.a = _A.C;
                        return empty<Double>(_A.Size);
                    }
                }
                if (!skipSymmCheck && !ishermitian(_A)) {
                    throw new ArgumentException("eigGen(): A must be hermitian!");
                }
                if (!skipSymmCheck && !ishermitian(_B)) {
                    throw new ArgumentException("eigGen(): B must be hermitian!");
                }
                int info = -1;
                int itype = (int)type;
                char jobz = object.Equals(V, null) ? 'N' : 'V';

                Array<double> AC = copyUpperTriangle<double>(_A, n, n); // ensures column major order

                Array<double> BC = _B.C;

                Array<Double> ret = empty<Double>(1, n, StorageOrders.ColumnMajor); 

                unsafe {
                    Double* w = (Double*)ret.GetHostPointerForWrite();

                    Lapack.dsygv(itype, jobz, 'U', (int)n, (double*)AC.GetHostPointerForWrite(), (int)n, (double*)BC.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)BC.Size[0], w, ref info);
                    if (info == 0) {
                        if (jobz == 'N') {
                            return ret;
                        } else {
                            lock (V.SynchObj)
                                V.a = AC;
                            return diag(ret);
                        }
                    } else if (info < 0) {
                        throw new ArgumentException("eigGen(): invalid parameter reported from Lapack module (dsygv): #" + (-info));
                    } else {
                        if (info <= n) {
                            throw new ArgumentException($"eigGen() did not converge! {info} off-diagonal elements unequal 0.");
                        } else if (info < 2 * n) {
                            throw new ArgumentException($"eigGen(): B must be positive definite! Lapack dsygv() info: {info}");
                        } else {
                            throw new ArgumentException($"eigGen() requires A and B to be hermitian and _B to be positive definite. Info from Lapack.dsygv: {info}.");
                        }
                    }
                }
            }
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

       

        /// <summary>
        /// Computes eigenvalues and eigenvectors (optional) of symmetric/hermitian inputs <paramref name="A"/> and <paramref name="B"/>: A*V=lamda*B*V. 
        /// </summary>
        /// <param name="A">Square, symmetric/hermitian input matrix, size [n x n].</param>
        /// <param name="B">Square, symmetric/hermitian and positive definite matrix, size [n x n].</param>
        /// <param name="V">[Output, optional] Returns the eigenvectors in columns (size [n x n]). Default: (null) eigenvectors are not returned.</param>
        /// <param name="type">[Optional] Determines the eigen problem type. Default: (null) Ax_eq_lambBx: A*V = r*B*V.</param>
        /// <param name="skipSymmCheck">[Optional] true: skip tests for <paramref name="A"/> and <paramref name="B"/> being hermitian. Default: false.</param>
        /// <returns>Vector [n] of eigenvalues or a diagonal matrix [n,n] with the eigenvalues on the main diagonal if <paramref name="V"/> is not null.</returns>
        /// <remarks><para>This function solves the generalized eigenproblem A*V=lamda*B*V or the problem as specified by <paramref name="type"/>. </para>
        /// <para>Eigenvectors in <paramref name="V"/> are not normalized! See details about the normalization performed by the underlying Lapack routines.</para>
        /// <para>Internally, the generalized eigenproblem A*V = r*B*V will be reduced to B<sup>-1</sup>*A*V = r*V using cholesky factorization. The 
        /// computations are handled by LAPACK functions DSYGV,SSYGV,CHEGV and ZHEGV respectively.</para></remarks>
        /// <exception cref="ArgumentException">if <paramref name="B"/> was not positive definite, if <paramref name="A"/> and <paramref name="B"/> was not of the same size, 
        /// if either <paramref name="A"/> and/or <paramref name="B"/> was found not to be symmetric/hermitian, or if the algorithm did not converge. All 
        /// exceptions will give additional error details in their exception message.</exception>
        /// <seealso cref="GenEigenType"/>
        /// <seealso cref="eigSymm(InArray{float})"/>
        /// <seealso cref="eig(InArray{float}, OutArray{fcomplex}, ref MatrixProperties, bool)"/>
        /// <seealso cref="chol(InArray{float}, bool)"/>
        public static Array<Single> eigGen(InArray<float> A, InArray<float> B, 
                                                              OutArray<float> V = null, GenEigenType type = GenEigenType.Ax_eq_lambBx, 
                                                              bool skipSymmCheck = false) {
            using (Scope.Enter()) {
                // check input arguments
                if (object.Equals(A, null) || object.Equals(B, null))
                    throw new ArgumentException($"eigGen A and B must not be null!");

                Array<float> _A = A, _B = B; 
                if (!_A.IsMatrix || !_B.IsMatrix)
                    throw new ArgumentException("eigGen(): A & B must be matrices!");
                var n = _A.Size[0];
                if (n != _A.Size[1])
                    throw new ArgumentException("eigGen(): input matrices must be square!");
                if (!_A.Size.IsSameSize(_B.Size))
                    throw new ArgumentException("eigGen(): A and B must have the same size!");
                if (_A.IsEmpty) {
                    if (object.Equals(V, null))
                        return empty<Single>(_A.Size);
                    else {
                        lock (V.SynchObj)
                            V.a = _A.C;
                        return empty<Single>(_A.Size);
                    }
                }
                if (!skipSymmCheck && !ishermitian(_A)) {
                    throw new ArgumentException("eigGen(): A must be hermitian!");
                }
                if (!skipSymmCheck && !ishermitian(_B)) {
                    throw new ArgumentException("eigGen(): B must be hermitian!");
                }
                int info = -1;
                int itype = (int)type;
                char jobz = object.Equals(V, null) ? 'N' : 'V';

                Array<float> AC = copyUpperTriangle<float>(_A, n, n); // ensures column major order

                Array<float> BC = _B.C;

                Array<Single> ret = empty<Single>(1, n, StorageOrders.ColumnMajor); 

                unsafe {
                    Single* w = (Single*)ret.GetHostPointerForWrite();

                    Lapack.ssygv(itype, jobz, 'U', (int)n, (float*)AC.GetHostPointerForWrite(), (int)n, (float*)BC.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)BC.Size[0], w, ref info);
                    if (info == 0) {
                        if (jobz == 'N') {
                            return ret;
                        } else {
                            lock (V.SynchObj)
                                V.a = AC;
                            return diag(ret);
                        }
                    } else if (info < 0) {
                        throw new ArgumentException("eigGen(): invalid parameter reported from Lapack module (ssygv): #" + (-info));
                    } else {
                        if (info <= n) {
                            throw new ArgumentException($"eigGen() did not converge! {info} off-diagonal elements unequal 0.");
                        } else if (info < 2 * n) {
                            throw new ArgumentException($"eigGen(): B must be positive definite! Lapack ssygv() info: {info}");
                        } else {
                            throw new ArgumentException($"eigGen() requires A and B to be hermitian and _B to be positive definite. Info from Lapack.ssygv: {info}.");
                        }
                    }
                }
            }
        }

       

        /// <summary>
        /// Computes eigenvalues and eigenvectors (optional) of symmetric/hermitian inputs <paramref name="A"/> and <paramref name="B"/>: A*V=lamda*B*V. 
        /// </summary>
        /// <param name="A">Square, symmetric/hermitian input matrix, size [n x n].</param>
        /// <param name="B">Square, symmetric/hermitian and positive definite matrix, size [n x n].</param>
        /// <param name="V">[Output, optional] Returns the eigenvectors in columns (size [n x n]). Default: (null) eigenvectors are not returned.</param>
        /// <param name="type">[Optional] Determines the eigen problem type. Default: (null) Ax_eq_lambBx: A*V = r*B*V.</param>
        /// <param name="skipSymmCheck">[Optional] true: skip tests for <paramref name="A"/> and <paramref name="B"/> being hermitian. Default: false.</param>
        /// <returns>Vector [n] of eigenvalues or a diagonal matrix [n,n] with the eigenvalues on the main diagonal if <paramref name="V"/> is not null.</returns>
        /// <remarks><para>This function solves the generalized eigenproblem A*V=lamda*B*V or the problem as specified by <paramref name="type"/>. </para>
        /// <para>Eigenvectors in <paramref name="V"/> are not normalized! See details about the normalization performed by the underlying Lapack routines.</para>
        /// <para>Internally, the generalized eigenproblem A*V = r*B*V will be reduced to B<sup>-1</sup>*A*V = r*V using cholesky factorization. The 
        /// computations are handled by LAPACK functions DSYGV,SSYGV,CHEGV and ZHEGV respectively.</para></remarks>
        /// <exception cref="ArgumentException">if <paramref name="B"/> was not positive definite, if <paramref name="A"/> and <paramref name="B"/> was not of the same size, 
        /// if either <paramref name="A"/> and/or <paramref name="B"/> was found not to be symmetric/hermitian, or if the algorithm did not converge. All 
        /// exceptions will give additional error details in their exception message.</exception>
        /// <seealso cref="GenEigenType"/>
        /// <seealso cref="eigSymm(InArray{fcomplex})"/>
        /// <seealso cref="eig(InArray{fcomplex}, OutArray{fcomplex}, ref MatrixProperties, bool)"/>
        /// <seealso cref="chol(InArray{fcomplex}, bool)"/>
        public static Array<Single> eigGen(InArray<fcomplex> A, InArray<fcomplex> B, 
                                                              OutArray<fcomplex> V = null, GenEigenType type = GenEigenType.Ax_eq_lambBx, 
                                                              bool skipSymmCheck = false) {
            using (Scope.Enter()) {
                // check input arguments
                if (object.Equals(A, null) || object.Equals(B, null))
                    throw new ArgumentException($"eigGen A and B must not be null!");

                Array<fcomplex> _A = A, _B = B; 
                if (!_A.IsMatrix || !_B.IsMatrix)
                    throw new ArgumentException("eigGen(): A & B must be matrices!");
                var n = _A.Size[0];
                if (n != _A.Size[1])
                    throw new ArgumentException("eigGen(): input matrices must be square!");
                if (!_A.Size.IsSameSize(_B.Size))
                    throw new ArgumentException("eigGen(): A and B must have the same size!");
                if (_A.IsEmpty) {
                    if (object.Equals(V, null))
                        return empty<Single>(_A.Size);
                    else {
                        lock (V.SynchObj)
                            V.a = _A.C;
                        return empty<Single>(_A.Size);
                    }
                }
                if (!skipSymmCheck && !ishermitian(_A)) {
                    throw new ArgumentException("eigGen(): A must be hermitian!");
                }
                if (!skipSymmCheck && !ishermitian(_B)) {
                    throw new ArgumentException("eigGen(): B must be hermitian!");
                }
                int info = -1;
                int itype = (int)type;
                char jobz = object.Equals(V, null) ? 'N' : 'V';

                Array<fcomplex> AC = copyUpperTriangle<fcomplex>(_A, n, n); // ensures column major order

                Array<fcomplex> BC = _B.C;

                Array<Single> ret = empty<Single>(1, n, StorageOrders.ColumnMajor); 

                unsafe {
                    Single* w = (Single*)ret.GetHostPointerForWrite();

                    Lapack.chegv(itype, jobz, 'U', (int)n, (fcomplex*)AC.GetHostPointerForWrite(), (int)n, (fcomplex*)BC.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)BC.Size[0], w, ref info);
                    if (info == 0) {
                        if (jobz == 'N') {
                            return ret;
                        } else {
                            lock (V.SynchObj)
                                V.a = AC;
                            return diag(ret);
                        }
                    } else if (info < 0) {
                        throw new ArgumentException("eigGen(): invalid parameter reported from Lapack module (chegv): #" + (-info));
                    } else {
                        if (info <= n) {
                            throw new ArgumentException($"eigGen() did not converge! {info} off-diagonal elements unequal 0.");
                        } else if (info < 2 * n) {
                            throw new ArgumentException($"eigGen(): B must be positive definite! Lapack chegv() info: {info}");
                        } else {
                            throw new ArgumentException($"eigGen() requires A and B to be hermitian and _B to be positive definite. Info from Lapack.chegv: {info}.");
                        }
                    }
                }
            }
        }

       

        /// <summary>
        /// Computes eigenvalues and eigenvectors (optional) of symmetric/hermitian inputs <paramref name="A"/> and <paramref name="B"/>: A*V=lamda*B*V. 
        /// </summary>
        /// <param name="A">Square, symmetric/hermitian input matrix, size [n x n].</param>
        /// <param name="B">Square, symmetric/hermitian and positive definite matrix, size [n x n].</param>
        /// <param name="V">[Output, optional] Returns the eigenvectors in columns (size [n x n]). Default: (null) eigenvectors are not returned.</param>
        /// <param name="type">[Optional] Determines the eigen problem type. Default: (null) Ax_eq_lambBx: A*V = r*B*V.</param>
        /// <param name="skipSymmCheck">[Optional] true: skip tests for <paramref name="A"/> and <paramref name="B"/> being hermitian. Default: false.</param>
        /// <returns>Vector [n] of eigenvalues or a diagonal matrix [n,n] with the eigenvalues on the main diagonal if <paramref name="V"/> is not null.</returns>
        /// <remarks><para>This function solves the generalized eigenproblem A*V=lamda*B*V or the problem as specified by <paramref name="type"/>. </para>
        /// <para>Eigenvectors in <paramref name="V"/> are not normalized! See details about the normalization performed by the underlying Lapack routines.</para>
        /// <para>Internally, the generalized eigenproblem A*V = r*B*V will be reduced to B<sup>-1</sup>*A*V = r*V using cholesky factorization. The 
        /// computations are handled by LAPACK functions DSYGV,SSYGV,CHEGV and ZHEGV respectively.</para></remarks>
        /// <exception cref="ArgumentException">if <paramref name="B"/> was not positive definite, if <paramref name="A"/> and <paramref name="B"/> was not of the same size, 
        /// if either <paramref name="A"/> and/or <paramref name="B"/> was found not to be symmetric/hermitian, or if the algorithm did not converge. All 
        /// exceptions will give additional error details in their exception message.</exception>
        /// <seealso cref="GenEigenType"/>
        /// <seealso cref="eigSymm(InArray{complex})"/>
        /// <seealso cref="eig(InArray{complex}, OutArray{complex}, ref MatrixProperties, bool)"/>
        /// <seealso cref="chol(InArray{complex}, bool)"/>
        public static Array<Double> eigGen(InArray<complex> A, InArray<complex> B, 
                                                              OutArray<complex> V = null, GenEigenType type = GenEigenType.Ax_eq_lambBx, 
                                                              bool skipSymmCheck = false) {
            using (Scope.Enter()) {
                // check input arguments
                if (object.Equals(A, null) || object.Equals(B, null))
                    throw new ArgumentException($"eigGen A and B must not be null!");

                Array<complex> _A = A, _B = B; 
                if (!_A.IsMatrix || !_B.IsMatrix)
                    throw new ArgumentException("eigGen(): A & B must be matrices!");
                var n = _A.Size[0];
                if (n != _A.Size[1])
                    throw new ArgumentException("eigGen(): input matrices must be square!");
                if (!_A.Size.IsSameSize(_B.Size))
                    throw new ArgumentException("eigGen(): A and B must have the same size!");
                if (_A.IsEmpty) {
                    if (object.Equals(V, null))
                        return empty<Double>(_A.Size);
                    else {
                        lock (V.SynchObj)
                            V.a = _A.C;
                        return empty<Double>(_A.Size);
                    }
                }
                if (!skipSymmCheck && !ishermitian(_A)) {
                    throw new ArgumentException("eigGen(): A must be hermitian!");
                }
                if (!skipSymmCheck && !ishermitian(_B)) {
                    throw new ArgumentException("eigGen(): B must be hermitian!");
                }
                int info = -1;
                int itype = (int)type;
                char jobz = object.Equals(V, null) ? 'N' : 'V';

                Array<complex> AC = copyUpperTriangle<complex>(_A, n, n); // ensures column major order

                Array<complex> BC = _B.C;

                Array<Double> ret = empty<Double>(1, n, StorageOrders.ColumnMajor); 

                unsafe {
                    Double* w = (Double*)ret.GetHostPointerForWrite();

                    Lapack.zhegv(itype, jobz, 'U', (int)n, (complex*)AC.GetHostPointerForWrite(), (int)n, (complex*)BC.GetHostPointerForWrite(StorageOrders.ColumnMajor), (int)BC.Size[0], w, ref info);
                    if (info == 0) {
                        if (jobz == 'N') {
                            return ret;
                        } else {
                            lock (V.SynchObj)
                                V.a = AC;
                            return diag(ret);
                        }
                    } else if (info < 0) {
                        throw new ArgumentException("eigGen(): invalid parameter reported from Lapack module (zhegv): #" + (-info));
                    } else {
                        if (info <= n) {
                            throw new ArgumentException($"eigGen() did not converge! {info} off-diagonal elements unequal 0.");
                        } else if (info < 2 * n) {
                            throw new ArgumentException($"eigGen(): B must be positive definite! Lapack zhegv() info: {info}");
                        } else {
                            throw new ArgumentException($"eigGen() requires A and B to be hermitian and _B to be positive definite. Info from Lapack.zhegv: {info}.");
                        }
                    }
                }
            }
        }

#endregion HYCALPER AUTO GENERATED CODE
    }
}

