//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////

#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.Text;
using System.Security; 
using System.Runtime.InteropServices;
using ILNumerics.Core.Functions.Builtin;
using ILNumerics.Core.Misc;
using ILNumerics.Core.StorageLayer;

namespace ILNumerics.Core.Native {

    /// <summary>
    /// Wrapper for FFT interface using MKL 10_03
    /// </summary>
    public class MKLFFT : IFFT {

        public static void Init() {
            try {
                // ho: commented ...  // unless the user explicitely configured the number of threads, let omp configure it! 
                //if (Settings.MaxNumberThreadsConfigured) {
                SetNumThreads((int)Settings.MaxNumberThreads);
                //}
            } catch (System.IO.FileNotFoundException) {
            } catch (System.BadImageFormatException) {
            }
        }

        
        public static void SetNumThreads(int numThreads) {
            if (numThreads != GetMaxThreads()) {
                int mklfft = MKLValues.MKL_FFT;
                MKLImports.mkl_domain_set_num_threads(ref numThreads, ref mklfft);
            }
        }

        
        public static int GetMaxThreads() {
            int mklfft = MKLValues.MKL_FFT;
            return MKLImports.mkl_domain_get_max_threads(ref mklfft);
        }

        
        private static void SetDynamicThreads(bool dynamic) {
            int val = dynamic ? 1 : 0;
            MKLImports.mkl_set_dynamic(ref val);
        }

        #region constructor 
        public MKLFFT() {
            Init();
        }
        #endregion

        #region IILFFT Member - 1-D

        #region HYCALPER LOOPSTART 1D complex
        /*!HC:TYPELIST:
<hycalper>
    <type>
        <source locate="after">
            HCretArr
        </source>
        <destination>complex</destination>
        <destination>fcomplex</destination>
        <destination>fcomplex</destination>
        <destination>complex</destination>
        <destination>fcomplex</destination>
    </type>
    <type>
        <source locate="after">
            funcName
        </source>
        <destination>FFTForward1D</destination>
        <destination>FFTForward1D</destination>
        <destination>FFTForward1D</destination>
        <destination>FFTBackward1D</destination>
        <destination>FFTBackward1D</destination>
    </type>
    <type>
        <source locate="after">
            inArr
        </source>
        <destination>complex</destination>
        <destination>float</destination>
        <destination>fcomplex</destination>
        <destination>complex</destination>
        <destination>fcomplex</destination>
    </type>
    <type>
        <source locate="after" endmark=" ;">
            convRet
        </source>
        <destination>A.C</destination>
        <destination>MathInternal.tofcomplex(A)</destination>
        <destination>A.C</destination>
        <destination>A.C</destination>
        <destination>A.C</destination>
    </type>
    <type>
        <source locate="after" endmark=",">
            mklPrec
        </source>
        <destination>MKLValues.DOUBLE</destination>
        <destination>MKLValues.SINGLE</destination>
        <destination>MKLValues.SINGLE</destination>
        <destination>MKLValues.DOUBLE</destination>
        <destination>MKLValues.SINGLE</destination>
    </type>
    <type>
        <source locate="after">
            dftiFunc
        </source>
        <destination>MKLImports.DftiComputeForward</destination>
        <destination>MKLImports.DftiComputeForward</destination>
        <destination>MKLImports.DftiComputeForward</destination>
        <destination>MKLImports.DftiComputeBackward</destination>
        <destination>MKLImports.DftiComputeBackward</destination>
    </type>               
    <type>
        <source locate="nextline">
            HCbackwScale
        </source>
        <destination></destination>
        <destination></destination>
        <destination></destination>
        <destination><![CDATA[ret.a = ret / new complex(A.S[dim],0);]]></destination>
        <destination><![CDATA[ret.a = ret / new fcomplex(A.S[dim],0);]]></destination>
    </type>               
 </hycalper>
 */
        
        public unsafe Array</*!HC:HCretArr*/ complex> /*!HC:funcName*/ FFTForward1D(InArray</*!HC:inArr*/ double> A, uint dim) {
            using (Scope.Enter(A)) {
                if (object.Equals(A, null)) {
                    throw new ArgumentNullException(nameof(A));
                }
                if (A.IsEmpty) {
                    return MathInternal.empty</*!HC:HCretArr*/ complex>(A.Size);
                }
                if (dim >= A.Size.NumberOfDimensions || A.Size[dim] == 1) {
                    return /*!HC:convRet*/ MathInternal.tocomplex(A);
                }
                // prepare output array 
                Array</*!HC:HCretArr*/ complex> ret = /*!HC:convRet*/ MathInternal.tocomplex(A);
                if (ret.S.StorageOrder != StorageOrders.ColumnMajor && ret.S.StorageOrder != StorageOrders.RowMajor) {
                    ret.Storage.EnsureStorageOrder(Settings.DefaultStorageOrder, inplace: true); 
                }
                ret.Detach(); 
                
                long* outBSD = StorageLayer.Storage<double>.Context.TmpBuffer1000; 
                var descriptor = getDescriptor1D(/*!HC:mklPrec*/ MKLValues.DOUBLE, MKLValues.COMPLEX, ret.Size, dim, true, outBSD);
                // create iterator for outside iterations
                // order is not that important here (inplace iteration), but the size iteration requires a continous layout order
                var itOrder = ret.Size.IsContinuous ? ret.S.StorageOrder : Settings.DefaultStorageOrder; 
                using (var it = new Iterators.StridedSizeIterator(outBSD, itOrder)) {
                    int error = 0;
                    // do the transform(s)
                    /*!HC:HCretArr*/
                    complex* retArr = (/*!HC:HCretArr*/complex*)ret.GetHostPointerForWrite();
                    while (it.MoveNext()) {
                        if ((error =  /*!HC:dftiFunc*/ MKLImports.DftiComputeForward(descriptor.Pointer, (IntPtr)(retArr + it.Current))) != 0)
                            throw new InvalidOperationException($"Error performing FFT (outer start index:{it.Current}). Error returned: " + MKLFFT.MKLGetError(error));
                    }
                }
                MKLDescriptor.Free(descriptor);

                /*!HC:HCbackwScale*/

                
                return ret;
            }
        }

        #endregion HYCALPER LOOPEND 
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        
        public unsafe Array< fcomplex>  FFTBackward1D(InArray< fcomplex> A, uint dim) {
            using (Scope.Enter(A)) {
                if (object.Equals(A, null)) {
                    throw new ArgumentNullException(nameof(A));
                }
                if (A.IsEmpty) {
                    return MathInternal.empty< fcomplex>(A.Size);
                }
                if (dim >= A.Size.NumberOfDimensions || A.Size[dim] == 1) {
                    return  A.C;
                }
                // prepare output array 
                Array< fcomplex> ret =  A.C;
                if (ret.S.StorageOrder != StorageOrders.ColumnMajor && ret.S.StorageOrder != StorageOrders.RowMajor) {
                    ret.Storage.EnsureStorageOrder(Settings.DefaultStorageOrder, inplace: true); 
                }
                ret.Detach(); 
                
                long* outBSD = StorageLayer.Storage<double>.Context.TmpBuffer1000; 
                var descriptor = getDescriptor1D( MKLValues.SINGLE, MKLValues.COMPLEX, ret.Size, dim, true, outBSD);
                // create iterator for outside iterations
                // order is not that important here (inplace iteration), but the size iteration requires a continous layout order
                var itOrder = ret.Size.IsContinuous ? ret.S.StorageOrder : Settings.DefaultStorageOrder; 
                using (var it = new Iterators.StridedSizeIterator(outBSD, itOrder)) {
                    int error = 0;
                    // do the transform(s)
                   
                    fcomplex* retArr = (fcomplex*)ret.GetHostPointerForWrite();
                    while (it.MoveNext()) {
                        if ((error =  MKLImports.DftiComputeBackward(descriptor.Pointer, (IntPtr)(retArr + it.Current))) != 0)
                            throw new InvalidOperationException($"Error performing FFT (outer start index:{it.Current}). Error returned: " + MKLFFT.MKLGetError(error));
                    }
                }
                MKLDescriptor.Free(descriptor);

                ret.a = ret / new fcomplex(A.S[dim],0);
                
                return ret;
            }
        }

       
        
        public unsafe Array< complex>  FFTBackward1D(InArray< complex> A, uint dim) {
            using (Scope.Enter(A)) {
                if (object.Equals(A, null)) {
                    throw new ArgumentNullException(nameof(A));
                }
                if (A.IsEmpty) {
                    return MathInternal.empty< complex>(A.Size);
                }
                if (dim >= A.Size.NumberOfDimensions || A.Size[dim] == 1) {
                    return  A.C;
                }
                // prepare output array 
                Array< complex> ret =  A.C;
                if (ret.S.StorageOrder != StorageOrders.ColumnMajor && ret.S.StorageOrder != StorageOrders.RowMajor) {
                    ret.Storage.EnsureStorageOrder(Settings.DefaultStorageOrder, inplace: true); 
                }
                ret.Detach(); 
                
                long* outBSD = StorageLayer.Storage<double>.Context.TmpBuffer1000; 
                var descriptor = getDescriptor1D( MKLValues.DOUBLE, MKLValues.COMPLEX, ret.Size, dim, true, outBSD);
                // create iterator for outside iterations
                // order is not that important here (inplace iteration), but the size iteration requires a continous layout order
                var itOrder = ret.Size.IsContinuous ? ret.S.StorageOrder : Settings.DefaultStorageOrder; 
                using (var it = new Iterators.StridedSizeIterator(outBSD, itOrder)) {
                    int error = 0;
                    // do the transform(s)
                   
                    complex* retArr = (complex*)ret.GetHostPointerForWrite();
                    while (it.MoveNext()) {
                        if ((error =  MKLImports.DftiComputeBackward(descriptor.Pointer, (IntPtr)(retArr + it.Current))) != 0)
                            throw new InvalidOperationException($"Error performing FFT (outer start index:{it.Current}). Error returned: " + MKLFFT.MKLGetError(error));
                    }
                }
                MKLDescriptor.Free(descriptor);

                ret.a = ret / new complex(A.S[dim],0);
                
                return ret;
            }
        }

       
        
        public unsafe Array< fcomplex>  FFTForward1D(InArray< fcomplex> A, uint dim) {
            using (Scope.Enter(A)) {
                if (object.Equals(A, null)) {
                    throw new ArgumentNullException(nameof(A));
                }
                if (A.IsEmpty) {
                    return MathInternal.empty< fcomplex>(A.Size);
                }
                if (dim >= A.Size.NumberOfDimensions || A.Size[dim] == 1) {
                    return  A.C;
                }
                // prepare output array 
                Array< fcomplex> ret =  A.C;
                if (ret.S.StorageOrder != StorageOrders.ColumnMajor && ret.S.StorageOrder != StorageOrders.RowMajor) {
                    ret.Storage.EnsureStorageOrder(Settings.DefaultStorageOrder, inplace: true); 
                }
                ret.Detach(); 
                
                long* outBSD = StorageLayer.Storage<double>.Context.TmpBuffer1000; 
                var descriptor = getDescriptor1D( MKLValues.SINGLE, MKLValues.COMPLEX, ret.Size, dim, true, outBSD);
                // create iterator for outside iterations
                // order is not that important here (inplace iteration), but the size iteration requires a continous layout order
                var itOrder = ret.Size.IsContinuous ? ret.S.StorageOrder : Settings.DefaultStorageOrder; 
                using (var it = new Iterators.StridedSizeIterator(outBSD, itOrder)) {
                    int error = 0;
                    // do the transform(s)
                   
                    fcomplex* retArr = (fcomplex*)ret.GetHostPointerForWrite();
                    while (it.MoveNext()) {
                        if ((error =  MKLImports.DftiComputeForward(descriptor.Pointer, (IntPtr)(retArr + it.Current))) != 0)
                            throw new InvalidOperationException($"Error performing FFT (outer start index:{it.Current}). Error returned: " + MKLFFT.MKLGetError(error));
                    }
                }
                MKLDescriptor.Free(descriptor);

                
                
                return ret;
            }
        }

       
        
        public unsafe Array< fcomplex>  FFTForward1D(InArray< float> A, uint dim) {
            using (Scope.Enter(A)) {
                if (object.Equals(A, null)) {
                    throw new ArgumentNullException(nameof(A));
                }
                if (A.IsEmpty) {
                    return MathInternal.empty< fcomplex>(A.Size);
                }
                if (dim >= A.Size.NumberOfDimensions || A.Size[dim] == 1) {
                    return  MathInternal.tofcomplex(A);
                }
                // prepare output array 
                Array< fcomplex> ret =  MathInternal.tofcomplex(A);
                if (ret.S.StorageOrder != StorageOrders.ColumnMajor && ret.S.StorageOrder != StorageOrders.RowMajor) {
                    ret.Storage.EnsureStorageOrder(Settings.DefaultStorageOrder, inplace: true); 
                }
                ret.Detach(); 
                
                long* outBSD = StorageLayer.Storage<double>.Context.TmpBuffer1000; 
                var descriptor = getDescriptor1D( MKLValues.SINGLE, MKLValues.COMPLEX, ret.Size, dim, true, outBSD);
                // create iterator for outside iterations
                // order is not that important here (inplace iteration), but the size iteration requires a continous layout order
                var itOrder = ret.Size.IsContinuous ? ret.S.StorageOrder : Settings.DefaultStorageOrder; 
                using (var it = new Iterators.StridedSizeIterator(outBSD, itOrder)) {
                    int error = 0;
                    // do the transform(s)
                   
                    fcomplex* retArr = (fcomplex*)ret.GetHostPointerForWrite();
                    while (it.MoveNext()) {
                        if ((error =  MKLImports.DftiComputeForward(descriptor.Pointer, (IntPtr)(retArr + it.Current))) != 0)
                            throw new InvalidOperationException($"Error performing FFT (outer start index:{it.Current}). Error returned: " + MKLFFT.MKLGetError(error));
                    }
                }
                MKLDescriptor.Free(descriptor);

                
                
                return ret;
            }
        }

       
        
        public unsafe Array< complex>  FFTForward1D(InArray< complex> A, uint dim) {
            using (Scope.Enter(A)) {
                if (object.Equals(A, null)) {
                    throw new ArgumentNullException(nameof(A));
                }
                if (A.IsEmpty) {
                    return MathInternal.empty< complex>(A.Size);
                }
                if (dim >= A.Size.NumberOfDimensions || A.Size[dim] == 1) {
                    return  A.C;
                }
                // prepare output array 
                Array< complex> ret =  A.C;
                if (ret.S.StorageOrder != StorageOrders.ColumnMajor && ret.S.StorageOrder != StorageOrders.RowMajor) {
                    ret.Storage.EnsureStorageOrder(Settings.DefaultStorageOrder, inplace: true); 
                }
                ret.Detach(); 
                
                long* outBSD = StorageLayer.Storage<double>.Context.TmpBuffer1000; 
                var descriptor = getDescriptor1D( MKLValues.DOUBLE, MKLValues.COMPLEX, ret.Size, dim, true, outBSD);
                // create iterator for outside iterations
                // order is not that important here (inplace iteration), but the size iteration requires a continous layout order
                var itOrder = ret.Size.IsContinuous ? ret.S.StorageOrder : Settings.DefaultStorageOrder; 
                using (var it = new Iterators.StridedSizeIterator(outBSD, itOrder)) {
                    int error = 0;
                    // do the transform(s)
                   
                    complex* retArr = (complex*)ret.GetHostPointerForWrite();
                    while (it.MoveNext()) {
                        if ((error =  MKLImports.DftiComputeForward(descriptor.Pointer, (IntPtr)(retArr + it.Current))) != 0)
                            throw new InvalidOperationException($"Error performing FFT (outer start index:{it.Current}). Error returned: " + MKLFFT.MKLGetError(error));
                    }
                }
                MKLDescriptor.Free(descriptor);

                
                
                return ret;
            }
        }


#endregion HYCALPER AUTO GENERATED CODE

        #region HYCALPER LOOPSTART 1D real (backward only)

        /*!HC:TYPELIST:
        <hycalper>
            <type>
                <source locate="after">
                    HCretArr
                </source>
                <destination>float</destination>
            </type>
            <type>
                <source locate="after">
                    inArr
                </source>
                <destination>fcomplex</destination>
            </type>
            <type>
                <source locate="after">
                    mklPrec
                </source>
                <destination>MKLValues.SINGLE</destination>
            </type>
         </hycalper>
         */
        
        public unsafe Array</*!HC:HCretArr*/ double> FFTBackwSym1D(InArray</*!HC:inArr*/ complex> A, uint dim) {
            return MathInternal.real(FFTBackward1D(A, dim));
        }

#endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

       
        
        public unsafe Array< float> FFTBackwSym1D(InArray< fcomplex> A, uint dim) {
            return MathInternal.real(FFTBackward1D(A, dim));
        }


#endregion HYCALPER AUTO GENERATED CODE

#endregion

#region IFFT Member - n-D

#region HYCALPER LOOPSTART ND-TRANSFORMS 
        /*!HC:TYPELIST:
        <hycalper>
            <type>
                <source locate="here">
                    complex
                </source>
                <destination>complex</destination>
                <destination>fcomplex</destination>
                <destination>fcomplex</destination>
                <destination>fcomplex</destination>
                <destination>complex</destination>
            </type>
            <type>
                <source locate="here">
                    FFTForward
                </source>
                <destination>FFTForward</destination>
                <destination>FFTForward</destination>
                <destination>FFTForward</destination>
                <destination>FFTBackward</destination>
                <destination>FFTBackward</destination>
            </type>
            <type>
                <source locate="here">
                    double
                </source>
                <destination>complex</destination>
                <destination>float</destination>
                <destination>fcomplex</destination>
                <destination>fcomplex</destination>
                <destination>complex</destination>
            </type>
            <type>
                <source locate="after" endmark=" ;">
                    convRet
                </source>
                <destination>A.C</destination>
                <destination>MathInternal.tofcomplex(A)</destination>
                <destination>A.C</destination>
                <destination>A.C</destination>
                <destination>A.C</destination>
            </type>
            <type>
                <source locate="after">
                    mklPrec
                </source>
                <destination>MKLValues.DOUBLE</destination>
                <destination>MKLValues.SINGLE</destination>
                <destination>MKLValues.SINGLE</destination>
                <destination>MKLValues.SINGLE</destination>
                <destination>MKLValues.DOUBLE</destination>
            </type>
            <type>
                <source locate="here">
                    DftiComputeForward
                </source>
                <destination>DftiComputeForward</destination>
                <destination>DftiComputeForward</destination>
                <destination>DftiComputeForward</destination>
                <destination>DftiComputeBackward</destination>
                <destination>DftiComputeBackward</destination>
            </type>               
            <type>
                <source locate="nextline">
                    HCbackwScale
                </source>
                <destination>#if BACKWARD_SCALE</destination>
                <destination>#if BACKWARD_SCALE</destination>
                <destination>#if BACKWARD_SCALE</destination>
                <destination>#if !BACKWARD_SCALE</destination>
                <destination>#if !BACKWARD_SCALE</destination>
            </type>               
            <type>
                <source locate="nextline">
                    limit3D
                </source>
                <destination></destination>
                <destination><![CDATA[if (nDims > 3) return FFTForward(MathInternal.tofcomplex(A),nDims);]]></destination>
                <destination></destination>
                <destination></destination>
                <destination></destination>
            </type>               
         </hycalper>
         */
        
        public unsafe Array<complex> FFTForward(InArray<double> A, uint nDims) {
            using (Scope.Enter(A)) {
                if (object.Equals(A, null)) {
                    throw new ArgumentNullException(nameof(A));
                }
                if (nDims < 1) {
                    throw new ArgumentException($"{nameof(nDims)} must be larger or equal to 1."); 
                }
                if (nDims > A.Size.NumberOfDimensions)
                    nDims = A.Size.NumberOfDimensions;
                if (A.IsEmpty) {
                    return MathInternal.empty<complex>(A.Size);
                }
                if (A.IsScalar || (A.Size[0] == 1 && nDims == 1))
                    return /*!HC:convRet*/ MathInternal.tocomplex(A);

                /*!HC:limit3D*/
                if (nDims > 3) return FFTForward(MathInternal.tocomplex(A), nDims);

                // prepare output array 
                Array<complex> ret = /*!HC:convRet*/ MathInternal.tocomplex(A);
                ret.Detach();

                long* outBSD = StorageLayer.Storage<double>.Context.TmpBuffer1000;
                var descriptor = getDescriptorND(/*!HC:mklPrec*/ MKLValues.DOUBLE, MKLValues.COMPLEX, ret.Size, nDims, true, outBSD);
                var outerIterOrder = A.Size.StorageOrder == StorageOrders.ColumnMajor ? StorageOrders.ColumnMajor : StorageOrders.RowMajor; 
                // create iterator for outside iterations
                using (var it = new Iterators.StridedSizeIterator(outBSD, outerIterOrder)) {
                    int error = 0;
                    // do the transform(s)

                    complex* retArr = (complex*)ret.GetHostPointerForWrite();
                    while (it.MoveNext()) {
                        if ((error = MKLImports.DftiComputeForward(descriptor.Pointer, (IntPtr)(retArr + it.Current))) != 0)
                            throw new InvalidOperationException($"Error performing FFT (outer start index:{it.Current}). Error returned: " + MKLFFT.MKLGetError(error));
                    }
                }
                MKLDescriptor.Free(descriptor); 

                /*!HC:HCbackwScale*/
#if BACKWARD_SCALE
                double scale = 1;
                for (uint i = 0; i < nDims; i++) {
                    scale *= A.Size[i];
                }
                return ret / scale;
#else
                return ret;
#endif
            }
        }

#endregion HYCALPER LOOPEND 
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        
        public unsafe Array<complex> FFTBackward(InArray<complex> A, uint nDims) {
            using (Scope.Enter(A)) {
                if (object.Equals(A, null)) {
                    throw new ArgumentNullException(nameof(A));
                }
                if (nDims < 1) {
                    throw new ArgumentException($"{nameof(nDims)} must be larger or equal to 1."); 
                }
                if (nDims > A.Size.NumberOfDimensions)
                    nDims = A.Size.NumberOfDimensions;
                if (A.IsEmpty) {
                    return MathInternal.empty<complex>(A.Size);
                }
                if (A.IsScalar || (A.Size[0] == 1 && nDims == 1))
                    return  A.C;

                

                // prepare output array 
                Array<complex> ret =  A.C;
                ret.Detach();

                long* outBSD = StorageLayer.Storage<complex>.Context.TmpBuffer1000;
                var descriptor = getDescriptorND( MKLValues.DOUBLE, MKLValues.COMPLEX, ret.Size, nDims, true, outBSD);
                var outerIterOrder = A.Size.StorageOrder == StorageOrders.ColumnMajor ? StorageOrders.ColumnMajor : StorageOrders.RowMajor; 
                // create iterator for outside iterations
                using (var it = new Iterators.StridedSizeIterator(outBSD, outerIterOrder)) {
                    int error = 0;
                    // do the transform(s)

                    complex* retArr = (complex*)ret.GetHostPointerForWrite();
                    while (it.MoveNext()) {
                        if ((error = MKLImports.DftiComputeBackward(descriptor.Pointer, (IntPtr)(retArr + it.Current))) != 0)
                            throw new InvalidOperationException($"Error performing FFT (outer start index:{it.Current}). Error returned: " + MKLFFT.MKLGetError(error));
                    }
                }
                MKLDescriptor.Free(descriptor); 

                #if !BACKWARD_SCALE
                complex scale = 1;
                for (uint i = 0; i < nDims; i++) {
                    scale *= A.Size[i];
                }
                return ret / scale;
#else
                return ret;
#endif
            }
        }

       
        
        public unsafe Array<fcomplex> FFTBackward(InArray<fcomplex> A, uint nDims) {
            using (Scope.Enter(A)) {
                if (object.Equals(A, null)) {
                    throw new ArgumentNullException(nameof(A));
                }
                if (nDims < 1) {
                    throw new ArgumentException($"{nameof(nDims)} must be larger or equal to 1."); 
                }
                if (nDims > A.Size.NumberOfDimensions)
                    nDims = A.Size.NumberOfDimensions;
                if (A.IsEmpty) {
                    return MathInternal.empty<fcomplex>(A.Size);
                }
                if (A.IsScalar || (A.Size[0] == 1 && nDims == 1))
                    return  A.C;

                

                // prepare output array 
                Array<fcomplex> ret =  A.C;
                ret.Detach();

                long* outBSD = StorageLayer.Storage<fcomplex>.Context.TmpBuffer1000;
                var descriptor = getDescriptorND( MKLValues.SINGLE, MKLValues.COMPLEX, ret.Size, nDims, true, outBSD);
                var outerIterOrder = A.Size.StorageOrder == StorageOrders.ColumnMajor ? StorageOrders.ColumnMajor : StorageOrders.RowMajor; 
                // create iterator for outside iterations
                using (var it = new Iterators.StridedSizeIterator(outBSD, outerIterOrder)) {
                    int error = 0;
                    // do the transform(s)

                    fcomplex* retArr = (fcomplex*)ret.GetHostPointerForWrite();
                    while (it.MoveNext()) {
                        if ((error = MKLImports.DftiComputeBackward(descriptor.Pointer, (IntPtr)(retArr + it.Current))) != 0)
                            throw new InvalidOperationException($"Error performing FFT (outer start index:{it.Current}). Error returned: " + MKLFFT.MKLGetError(error));
                    }
                }
                MKLDescriptor.Free(descriptor); 

                #if !BACKWARD_SCALE
                fcomplex scale = 1;
                for (uint i = 0; i < nDims; i++) {
                    scale *= A.Size[i];
                }
                return ret / scale;
#else
                return ret;
#endif
            }
        }

       
        
        public unsafe Array<fcomplex> FFTForward(InArray<fcomplex> A, uint nDims) {
            using (Scope.Enter(A)) {
                if (object.Equals(A, null)) {
                    throw new ArgumentNullException(nameof(A));
                }
                if (nDims < 1) {
                    throw new ArgumentException($"{nameof(nDims)} must be larger or equal to 1."); 
                }
                if (nDims > A.Size.NumberOfDimensions)
                    nDims = A.Size.NumberOfDimensions;
                if (A.IsEmpty) {
                    return MathInternal.empty<fcomplex>(A.Size);
                }
                if (A.IsScalar || (A.Size[0] == 1 && nDims == 1))
                    return  A.C;

                

                // prepare output array 
                Array<fcomplex> ret =  A.C;
                ret.Detach();

                long* outBSD = StorageLayer.Storage<fcomplex>.Context.TmpBuffer1000;
                var descriptor = getDescriptorND( MKLValues.SINGLE, MKLValues.COMPLEX, ret.Size, nDims, true, outBSD);
                var outerIterOrder = A.Size.StorageOrder == StorageOrders.ColumnMajor ? StorageOrders.ColumnMajor : StorageOrders.RowMajor; 
                // create iterator for outside iterations
                using (var it = new Iterators.StridedSizeIterator(outBSD, outerIterOrder)) {
                    int error = 0;
                    // do the transform(s)

                    fcomplex* retArr = (fcomplex*)ret.GetHostPointerForWrite();
                    while (it.MoveNext()) {
                        if ((error = MKLImports.DftiComputeForward(descriptor.Pointer, (IntPtr)(retArr + it.Current))) != 0)
                            throw new InvalidOperationException($"Error performing FFT (outer start index:{it.Current}). Error returned: " + MKLFFT.MKLGetError(error));
                    }
                }
                MKLDescriptor.Free(descriptor); 

                #if BACKWARD_SCALE
                fcomplex scale = 1;
                for (uint i = 0; i < nDims; i++) {
                    scale *= A.Size[i];
                }
                return ret / scale;
#else
                return ret;
#endif
            }
        }

       
        
        public unsafe Array<fcomplex> FFTForward(InArray<float> A, uint nDims) {
            using (Scope.Enter(A)) {
                if (object.Equals(A, null)) {
                    throw new ArgumentNullException(nameof(A));
                }
                if (nDims < 1) {
                    throw new ArgumentException($"{nameof(nDims)} must be larger or equal to 1."); 
                }
                if (nDims > A.Size.NumberOfDimensions)
                    nDims = A.Size.NumberOfDimensions;
                if (A.IsEmpty) {
                    return MathInternal.empty<fcomplex>(A.Size);
                }
                if (A.IsScalar || (A.Size[0] == 1 && nDims == 1))
                    return  MathInternal.tofcomplex(A);

                if (nDims > 3) return FFTForward(MathInternal.tofcomplex(A),nDims);

                // prepare output array 
                Array<fcomplex> ret =  MathInternal.tofcomplex(A);
                ret.Detach();

                long* outBSD = StorageLayer.Storage<float>.Context.TmpBuffer1000;
                var descriptor = getDescriptorND( MKLValues.SINGLE, MKLValues.COMPLEX, ret.Size, nDims, true, outBSD);
                var outerIterOrder = A.Size.StorageOrder == StorageOrders.ColumnMajor ? StorageOrders.ColumnMajor : StorageOrders.RowMajor; 
                // create iterator for outside iterations
                using (var it = new Iterators.StridedSizeIterator(outBSD, outerIterOrder)) {
                    int error = 0;
                    // do the transform(s)

                    fcomplex* retArr = (fcomplex*)ret.GetHostPointerForWrite();
                    while (it.MoveNext()) {
                        if ((error = MKLImports.DftiComputeForward(descriptor.Pointer, (IntPtr)(retArr + it.Current))) != 0)
                            throw new InvalidOperationException($"Error performing FFT (outer start index:{it.Current}). Error returned: " + MKLFFT.MKLGetError(error));
                    }
                }
                MKLDescriptor.Free(descriptor); 

                #if BACKWARD_SCALE
                float scale = 1;
                for (uint i = 0; i < nDims; i++) {
                    scale *= A.Size[i];
                }
                return ret / scale;
#else
                return ret;
#endif
            }
        }

       
        
        public unsafe Array<complex> FFTForward(InArray<complex> A, uint nDims) {
            using (Scope.Enter(A)) {
                if (object.Equals(A, null)) {
                    throw new ArgumentNullException(nameof(A));
                }
                if (nDims < 1) {
                    throw new ArgumentException($"{nameof(nDims)} must be larger or equal to 1."); 
                }
                if (nDims > A.Size.NumberOfDimensions)
                    nDims = A.Size.NumberOfDimensions;
                if (A.IsEmpty) {
                    return MathInternal.empty<complex>(A.Size);
                }
                if (A.IsScalar || (A.Size[0] == 1 && nDims == 1))
                    return  A.C;

                

                // prepare output array 
                Array<complex> ret =  A.C;
                ret.Detach();

                long* outBSD = StorageLayer.Storage<complex>.Context.TmpBuffer1000;
                var descriptor = getDescriptorND( MKLValues.DOUBLE, MKLValues.COMPLEX, ret.Size, nDims, true, outBSD);
                var outerIterOrder = A.Size.StorageOrder == StorageOrders.ColumnMajor ? StorageOrders.ColumnMajor : StorageOrders.RowMajor; 
                // create iterator for outside iterations
                using (var it = new Iterators.StridedSizeIterator(outBSD, outerIterOrder)) {
                    int error = 0;
                    // do the transform(s)

                    complex* retArr = (complex*)ret.GetHostPointerForWrite();
                    while (it.MoveNext()) {
                        if ((error = MKLImports.DftiComputeForward(descriptor.Pointer, (IntPtr)(retArr + it.Current))) != 0)
                            throw new InvalidOperationException($"Error performing FFT (outer start index:{it.Current}). Error returned: " + MKLFFT.MKLGetError(error));
                    }
                }
                MKLDescriptor.Free(descriptor); 

                #if BACKWARD_SCALE
                complex scale = 1;
                for (uint i = 0; i < nDims; i++) {
                    scale *= A.Size[i];
                }
                return ret / scale;
#else
                return ret;
#endif
            }
        }


#endregion HYCALPER AUTO GENERATED CODE

#region HYCALPER LOOPSTART BACKWARD SYM ND 
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
            complex
        </source>
        <destination>fcomplex</destination>
    </type>
    <type>
        <source locate="here">
            MKLValues.DOUBLE
        </source>
        <destination>MKLValues.SINGLE</destination>
    </type>
 </hycalper>
 */
        
        public unsafe Array<double> FFTBackwSym(InArray<complex> A, uint nDims) {

            return MathInternal.real(FFTBackward(A, nDims));

        }
#endregion HYCALPER LOOPEND 
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       
        
        public unsafe Array<float> FFTBackwSym(InArray<fcomplex> A, uint nDims) {

            return MathInternal.real(FFTBackward(A, nDims));

        }

#endregion HYCALPER AUTO GENERATED CODE

#endregion

#region IILFFT Member - Misc
        /// <summary>
        /// (True) Informs about the capability of this <see cref="IFFT"/> implementation to optimize subsequent transforms of similar data (regarding shape and type).
        /// </summary>
        public bool CachePlans {
            get {
                return true;
            }
        }

        /// <summary>
        /// Invalidates the currently cached fft descriptor for this thread.
        /// </summary>
        /// <remarks><para>Calling this method manually may help in situations where multithreading was 
        /// triggered manually and many threads are involved. Note that
        /// FFT plans are cached on a thread level! Therefore, if you have created fft transforms on multiple 
        /// threads, make sure to dispose the descriptor on each thread individually.</para>
        /// <para>FFT descriptors carry information about the last FFT transform and reuse it for subsequent, 
        /// similar transforms on the same thread. This saves the re-creation of intermediate data during the 
        /// transforms. Only the descriptor of the last FFT transform is cached. Hence, in a single threaded 
        /// scenario (default) disposing the FFT descriptor data manually is rarely required.</para></remarks>
        public void FreePlans() {
            MKLDescriptor.Cache.Dispose();
        }
        /// <summary>
        /// (True) Informs about the capability of this <see cref="IFFT"/> implementation to perform optimized transforms for symmetric / hermitian data.
        /// </summary>
        public bool SpeedyHermitian {
            get { return true; }
        }

#endregion
        
        internal static string MKLGetError(int error) {
            var pMsg = MKLImports.DftiErrorMessage(error);
            if (pMsg == IntPtr.Zero) return "";
            return Marshal.PtrToStringAnsi(pMsg);
        }

#region private helper
        private static bool isMKLError(int error) {
            // TODO check! only the first 32 bits seem to be relevant for error!
            //return MKLImports.DftiErrorClass(error, MKLValues.NO_ERROR) != 1;
            return error != MKLValues.NO_ERROR;
        }
        private unsafe static MKLDescriptor getDescriptor1D(int precision, int domain, Size size, uint d, bool inplace,
                              long* outsideIterationBSD) {

            System.Diagnostics.Debug.Assert(!Equals(size, null) && size.NumberOfDimensions > 0 && size.NumberOfElements > 0, "handle special array shapes separately!" );
            System.Diagnostics.Debug.Assert(size[d] != 1, "handle singleton working dimension separately!" );
            // fakes a two element int array: [0, stride_d]

            long stride = 0; ((int*)&stride)[1] = (int)size.GetStride(d);
            int dimension = (int)size[d];

            if (size.NumberOfDimensions == 1) { // TODO: Matlab vectors should also use this version. Check for NonSingletonDimensions instead ?
                // outside iteration not needed (means: is considered as "numpy scalar")
                outsideIterationBSD[0] = 0;
                outsideIterationBSD[1] = 1;
                outsideIterationBSD[2] = 0;
                // inside iteration is straight forward
                int distance = 0;
                return MKLDescriptor.GetOrCreate(precision, domain, 1, &dimension, (int*)&stride, 1, distance, inplace); 
            }

            if (size.IsContinuous) {
                if (d == 0 || d == size.NumberOfDimensions - 1) // size.GetStride(d) == 1) 
                {
                    // working along the leading or the last dimension: all done within the MKL
                    outsideIterationBSD[0] = 0; // outside iterator assumes a scalar array by ndims = 0
                    outsideIterationBSD[1] = 1;
                    outsideIterationBSD[2] = 0;

                    int distance, nrOfInnerTransforms;
                    if ((stride >> 32) == 1) {
                        nrOfInnerTransforms = (int)(size.NumberOfElements / dimension);
                        distance = dimension; 
                    } else {
                        nrOfInnerTransforms = (int)size.GetStride(d);
                        distance = 1; 
                    }
                    return MKLDescriptor.GetOrCreate(precision, domain, 1, &dimension, (int*)&stride, nrOfInnerTransforms, distance, inplace);
                } 
                else 
                {
                    // d is some arbitrary dimension within the dimension range (not the first, not the last)
                    int distance = 1, nrOfInnerTransforms = (int)size.GetStride(d);
                    
                    // column major: all 0...d dims -> MKL; d+1...n-1 dims -> outside
                    // row major; all 0..d-1 dims -> outside; d...n-1 dims -> MKL
#region outside iteration
                    System.Diagnostics.Debug.Assert(d > 0 && d < size.NumberOfDimensions - 1);  // otherwise the shortcut above would have been kicked in

                    outsideIterationBSD[0] = 1;
                    outsideIterationBSD[1] = size.MergeNextToEnd(d, ref outsideIterationBSD[4]); 

                    outsideIterationBSD[2] = 0;
                    outsideIterationBSD[3] = outsideIterationBSD[1];
#endregion
                    
                    return MKLDescriptor.GetOrCreate(precision, domain, 1, &dimension, (int*)&stride, nrOfInnerTransforms, distance, inplace);
                }
            } else {
                // arbitrary, non-contiguous storage order 
                System.Diagnostics.Debug.Assert(size.NumberOfDimensions >= 2); 
                // find inner iteration dim: we let MKL handle the longest non-working dim via repeated transforms. 
                uint k = 0;
#region determine 2nd, longest dim for inside iteration
                if (k == d) {
                    k++; // we have checked above that more dims exist
                } 
                long curLongest = 0; 
                for (uint i = 0; i < size.NumberOfDimensions; i++) {
                    if (i != d && i != k && size[i] > curLongest) {
                        curLongest = size[i];
                        k = i; 
                    } 
                }
#endregion

#region outside iteration is done over all but k and d
                outsideIterationBSD[1] = 1; 
                for (uint i = 0, o = 0; i < size.NumberOfDimensions; i++) {
                    if (i == k || i == d) continue;

                    outsideIterationBSD[3 + o] = size[i];
                    outsideIterationBSD[1] *= size[i]; 
                    outsideIterationBSD[3 + size.NumberOfDimensions + o] = size.GetStride(i);
                    o++; 
                }
                outsideIterationBSD[0] = size.NumberOfDimensions - 2; 
                outsideIterationBSD[2] = 0;
#endregion
                int distance = (int)size.GetStride(k);
                return MKLDescriptor.GetOrCreate(precision, domain, 1, &dimension, (int*)&stride, (int)size[k], distance, inplace);
            }
        }
        private unsafe static MKLDescriptor getDescriptorND(int precision, int domain, Size size, uint nDims, bool inplace, long* outBSD) {
            if (nDims == 1) {
                return getDescriptor1D(precision, domain, size, 0, inplace, outBSD); 
            }

            int* buffer = stackalloc int[(int)nDims * 2 + 1];

            buffer[nDims] = 0; // <- s_0
            for (uint i = 0; i < nDims; i++) {

                buffer[i] = (int)size[i]; // dimension lengths 
                buffer[i + nDims + 1] = (int)size.GetStride(i);  // strides 
            }
            // our first n dimensions are only efficiently together handleable inside MKL if A is a column major storage
            if (size.StorageOrder == StorageOrders.ColumnMajor) {
                // no outside iteration required - all in MKL
                outBSD[0] = 0;
                outBSD[1] = 1;

                outBSD[2] = 0;
                int nrTransforms = 1, distance = (int)size.GetStride4MLlastDimExpansion(nDims);
                // prevent from "NumberOfElements / stride[ndims]" due to DivByZero issues
                for (uint i = nDims; i < size.NumberOfDimensions; i++) {
                    nrTransforms *= (int)size[i];
                }
                return MKLDescriptor.GetOrCreate(precision, domain, (int)nDims, buffer, buffer + nDims, 
                                                    nrTransforms, distance, inplace);
            } else {
                // all iterations except the actual transformation dims are done outside MKL
                var nOuterDims = size.NumberOfDimensions - nDims;
                outBSD[0] = nOuterDims;
                outBSD[1] = 1;
                // prevent from "NumberOfElements / stride[ndims]" due to DivByZero issues
                for (uint i = nDims; i < size.NumberOfDimensions; i++) {
                    outBSD[1] *= (int)size[i];
                }
                outBSD[2] = 0; 

                for (uint i = 0; i < size.NumberOfDimensions - nDims; i++) {
                    outBSD[3 + i] = size[nDims + i];
                    outBSD[3 + i + nOuterDims] = size.GetStride(nDims + i); 
                }
                return MKLDescriptor.GetOrCreate(precision, domain, (int)nDims, buffer, buffer + nDims,
                                        1, 0, inplace);
            }

        }
#endregion

    }
}
