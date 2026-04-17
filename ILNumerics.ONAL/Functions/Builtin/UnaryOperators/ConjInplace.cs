//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
using ILNumerics.Core.Arrays;
using ILNumerics.Core.Global;
using ILNumerics.Core.StorageLayer;
using System;
using System.Security;
using System.Threading;

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
        Complex
    </source>
        <destination>FComplex</destination>
    </type>
</hycalper>
*/

namespace ILNumerics.Core.Functions.Builtin {

    #region HYCALPER LOOPSTART UNARY_OPERATOR_TEMPLATE
    internal static partial class MathInternal {

        /// <summary>Conjugates complex elements inplace.</summary>
        /// <param name="A">Mutable input array.</param>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/> inplace.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>Elements of the input array <paramref name="A"/> are directly altered. New memory is only used if the elements 
        /// of <paramref name="A"/> are currently shared with other arrays. Only in this case a copy is created automatically.</para></remarks>
        
        internal unsafe static void conjInplace(Mutable<complex,Array<complex>,InArray<complex>,OutArray<complex>,Array<complex>,Storage<complex>> A) {
            if (object.Equals(A, null)) {
                return;
            }
            InnerLoops.ConjInplace.Complex.Instance.operate(A);
        }
    }
    namespace InnerLoops {

        namespace ConjInplace {
            internal class Complex :

            UnaryBaseInPlaceOnly<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>
            {

                internal static Complex Instance = new Complex();

                public unsafe override void Strided64Inplace(byte* pSrc, long start, long len, long* bsd) {

                    uint ndims = (uint)bsd[0];
                    if (start == 0 && len == 1) {
                        // this includes np scalars: ndims == 0
                        (*(complex*)pSrc).Conjugate();
                        return; 
                    }

                    long* dims = bsd + 3;
                    long* strides = dims + ndims;
                    long stride0 = strides[0];
                    long* cur = stackalloc long[(int)ndims];

                    // figure out the dimension index position for start
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");
                    // dims are - 1! strides are bytes!
                    cur[0] = start % (dims[0] + 1);
                    long f = start / (dims[0] + 1);
                    long higdims = 0;
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        var d = dims[i] + 1; 
                        cur[i] = f % d;
                        f /= d;
                        higdims += cur[i] * strides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0; 
                    }
                    System.Diagnostics.Debug.Assert(f == 0);


                    while (true) {

                        byte* pIn = pSrc + higdims + cur[0] * stride0;

                        // iteration length limited to either the dimension lengths or the end of the requested chunk 
                        long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                        len -= leadLen;

                        while (leadLen-- > 0) {
                            (*(complex*)pIn).Conjugate();
                            pIn += stride0;
                        }
                        if (len == 0) {
                            break;
                        }
                        // reset initial offset in lead dimension after first iteration
                        cur[0] = 0;  // TODO: how can we improve this? Subseqent stores are redundant!

                        // increase higher dims
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                higdims += strides[d];
                                cur[d]++;
                                break;
                            } else {
                                cur[d] = 0;
                                higdims -= strides[d] * dims[d];
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }


    #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
    internal static partial class MathInternal {

        /// <summary>Conjugates fcomplex elements inplace.</summary>
        /// <param name="A">Mutable input array.</param>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/> inplace.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>Elements of the input array <paramref name="A"/> are directly altered. New memory is only used if the elements 
        /// of <paramref name="A"/> are currently shared with other arrays. Only in this case a copy is created automatically.</para></remarks>
        
        internal unsafe static void conjInplace(Mutable<fcomplex,Array<fcomplex>,InArray<fcomplex>,OutArray<fcomplex>,Array<fcomplex>,Storage<fcomplex>> A) {
            if (object.Equals(A, null)) {
                return;
            }
            InnerLoops.ConjInplace.FComplex.Instance.operate(A);
        }
    }
    namespace InnerLoops {

        namespace ConjInplace {
            internal class FComplex :

            UnaryBaseInPlaceOnly<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>
            {

                internal static FComplex Instance = new FComplex();

                public unsafe override void Strided64Inplace(byte* pSrc, long start, long len, long* bsd) {

                    uint ndims = (uint)bsd[0];
                    if (start == 0 && len == 1) {
                        // this includes np scalars: ndims == 0
                        (*(fcomplex*)pSrc).Conjugate();
                        return; 
                    }

                    long* dims = bsd + 3;
                    long* strides = dims + ndims;
                    long stride0 = strides[0];
                    long* cur = stackalloc long[(int)ndims];

                    // figure out the dimension index position for start
                    System.Diagnostics.Debug.Assert(ndims > 0, "Scalar arrays should be handled outside of this method.");
                    // dims are - 1! strides are bytes!
                    cur[0] = start % (dims[0] + 1);
                    long f = start / (dims[0] + 1);
                    long higdims = 0;
                    int i = 1;
                    for (; f > 0 && i < ndims; i++) {
                        var d = dims[i] + 1; 
                        cur[i] = f % d;
                        f /= d;
                        higdims += cur[i] * strides[i];
                    }
                    while (i < ndims) {
                        cur[i++] = 0; 
                    }
                    System.Diagnostics.Debug.Assert(f == 0);


                    while (true) {

                        byte* pIn = pSrc + higdims + cur[0] * stride0;

                        // iteration length limited to either the dimension lengths or the end of the requested chunk 
                        long leadLen = Math.Min(len, dims[0] + 1 - cur[0]);
                        len -= leadLen;

                        while (leadLen-- > 0) {
                            (*(fcomplex*)pIn).Conjugate();
                            pIn += stride0;
                        }
                        if (len == 0) {
                            break;
                        }
                        // reset initial offset in lead dimension after first iteration
                        cur[0] = 0;  // TODO: how can we improve this? Subseqent stores are redundant!

                        // increase higher dims
                        int d = 1;
                        while (d < ndims) {
                            if (cur[d] < dims[d]) {
                                higdims += strides[d];
                                cur[d]++;
                                break;
                            } else {
                                cur[d] = 0;
                                higdims -= strides[d] * dims[d];
                                d++;
                            }
                        }
                    }
                }
            }
        }
    }



#endregion HYCALPER AUTO GENERATED CODE

}
