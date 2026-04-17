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
        double
    </source>
        <destination>double</destination>
        <destination>float</destination>
        <destination>fcomplex</destination>
        <destination>complex</destination>
        <destination>double</destination>
        <destination>float</destination>
    </type>
    <type>
    <source locate="after">
        outArr
    </source>
        <destination>double</destination>
        <destination>float</destination>
        <destination>fcomplex</destination>
        <destination>complex</destination>
        <destination>complex</destination>
        <destination>fcomplex</destination>
    </type>
    <type>
    <source locate="here">
        Double
    </source>
        <destination>Double</destination>
        <destination>Single</destination>
        <destination>FComplex</destination>
        <destination>Complex</destination>
        <destination>ComplexC</destination>
        <destination>FComplexC</destination>
    </type>
    <type>
        <source locate="after">
            funcname
        </source>
        <destination>sqrt</destination>
        <destination>sqrt</destination>
        <destination>sqrt</destination>
        <destination>sqrt</destination>
        <destination>sqrtc</destination>
        <destination>sqrtc</destination>
    </type>
    <type>
        <source locate="after" endmark=" ()">
            operatorfunc
        </source>
        <destination>Math.Sqrt</destination>
        <destination>(float)Math.Sqrt</destination>
        <destination>fcomplex.Sqrt</destination>
        <destination>complex.Sqrt</destination>
        <destination>complex.Sqrt</destination>
        <destination>fcomplex.Sqrt</destination>
    </type>
    <type>
        <source locate="after" endmark=" :,*.()">
            innerloopname
        </source>
        <destination>SquareRoot</destination>
        <destination>SquareRoot</destination>
        <destination>SquareRoot</destination>
        <destination>SquareRoot</destination>
        <destination>SquareRootC</destination>
        <destination>SquareRootC</destination>
    </type>
    <type>
        <source locate="nextline">
            implacedisabled
        </source>
        <destination><![CDATA[#if !IMPLACE_DISABLED]]></destination>
        <destination><![CDATA[#if !IMPLACE_DISABLED]]></destination>
        <destination><![CDATA[#if !IMPLACE_DISABLED]]></destination>
        <destination><![CDATA[#if !IMPLACE_DISABLED]]></destination>
        <destination><![CDATA[#if IMPLACE_DISABLED]]></destination>
        <destination><![CDATA[#if IMPLACE_DISABLED]]></destination>
    </type>
    <type>
        <source locate="comment">
            summary
        </source>
        <destination>Square root of array elements - real output.</destination>
        <destination>Square root of array elements - real output.</destination>
        <destination>Square root of array elements.</destination>
        <destination>Square root of array elements.</destination>
        <destination>Square root of array elements - complex output.</destination>
        <destination>Square root of array elements - complex output.</destination>
    </type>
    <type>
        <source locate="comment">
            returns
        </source>
        <destination><![CDATA[Array{int} of the same shape as <paramref name="A"/> with elementwise result of sqrt(A).]]>.</destination>
        <destination><![CDATA[Array{int} of the same shape as <paramref name="A"/> with elementwise result of sqrt(A).]]>.</destination>
        <destination><![CDATA[Array{int} of the same shape as <paramref name="A"/> with elementwise result of sqrt(A).]]>.</destination>
        <destination><![CDATA[Array{int} of the same shape as <paramref name="A"/> with elementwise result of sqrt(A).]]>.</destination>
        <destination><![CDATA[Array{int} of the same shape as <paramref name="A"/> with elementwise result of sqrtc(A).]]>.</destination>
        <destination><![CDATA[Array{int} of the same shape as <paramref name="A"/> with elementwise result of sqrtc(A).]]>.</destination>
    </type>
</hycalper>
*/

namespace ILNumerics.Core.Functions.Builtin {

    #region HYCALPER LOOPSTART UNARY_OPERATOR_TEMPLATE@Functions\Builtin\UnaryOperators\Sin.cs

    #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
   
    internal static partial class MathInternal {

        /// <summary>Square root of array elements - complex output.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{int} of the same shape as <paramref name="A"/> with elementwise result of sqrtc(A)..</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<fcomplex> sqrtc(BaseArray<float> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.SquareRootC.FComplexC.Instance.operate(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace SquareRootC {

            
            internal sealed class FComplexC :
            #if IMPLACE_DISABLED
            UnaryBaseInPlace<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>
#else
            UnaryBaseOutOfPlace<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>, 
                               
                               fcomplex , Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>
            >
#endif
            {

                internal static FComplexC Instance = new FComplexC();

                
                public unsafe override void Strided64(Storage<float> A, Storage<fcomplex> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    fcomplex* outP = (fcomplex*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    float* inP = (float*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = fcomplex.Sqrt(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Square root of array elements - complex output.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{int} of the same shape as <paramref name="A"/> with elementwise result of sqrtc(A)..</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<complex> sqrtc(BaseArray<double> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.SquareRootC.ComplexC.Instance.operate(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace SquareRootC {

            
            internal sealed class ComplexC :
            #if IMPLACE_DISABLED
            UnaryBaseInPlace<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>
#else
            UnaryBaseOutOfPlace<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>, 
                               
                               complex , Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>
            >
#endif
            {

                internal static ComplexC Instance = new ComplexC();

                
                public unsafe override void Strided64(Storage<double> A, Storage<complex> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    complex* outP = (complex*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    double* inP = (double*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = complex.Sqrt(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Square root of array elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{int} of the same shape as <paramref name="A"/> with elementwise result of sqrt(A)..</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<complex> sqrt(BaseArray<complex> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.SquareRoot.Complex.Instance.operate(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace SquareRoot {

            
            internal sealed class Complex :
            #if !IMPLACE_DISABLED
            UnaryBaseInPlace<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>
#else
            UnaryBaseOutOfPlace<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>, 
                               
                               complex , Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>
            >
#endif
            {

                internal static Complex Instance = new Complex();

                
                public unsafe override void Strided64(Storage<complex> A, Storage<complex> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    complex* outP = (complex*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    complex* inP = (complex*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = complex.Sqrt(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Square root of array elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{int} of the same shape as <paramref name="A"/> with elementwise result of sqrt(A)..</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<fcomplex> sqrt(BaseArray<fcomplex> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.SquareRoot.FComplex.Instance.operate(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace SquareRoot {

            
            internal sealed class FComplex :
            #if !IMPLACE_DISABLED
            UnaryBaseInPlace<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>
#else
            UnaryBaseOutOfPlace<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>, 
                               
                               fcomplex , Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>
            >
#endif
            {

                internal static FComplex Instance = new FComplex();

                
                public unsafe override void Strided64(Storage<fcomplex> A, Storage<fcomplex> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    fcomplex* outP = (fcomplex*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    fcomplex* inP = (fcomplex*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = fcomplex.Sqrt(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Square root of array elements - real output.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{int} of the same shape as <paramref name="A"/> with elementwise result of sqrt(A)..</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<float> sqrt(BaseArray<float> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.SquareRoot.Single.Instance.operate(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace SquareRoot {

            
            internal sealed class Single :
            #if !IMPLACE_DISABLED
            UnaryBaseInPlace<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>
#else
            UnaryBaseOutOfPlace<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>, 
                               
                               float , Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>
            >
#endif
            {

                internal static Single Instance = new Single();

                
                public unsafe override void Strided64(Storage<float> A, Storage<float> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    float* outP = (float*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    float* inP = (float*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = (float)Math.Sqrt(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Square root of array elements - real output.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{int} of the same shape as <paramref name="A"/> with elementwise result of sqrt(A)..</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<double> sqrt(BaseArray<double> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.SquareRoot.Double.Instance.operate(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace SquareRoot {

            
            internal sealed class Double :
            #if !IMPLACE_DISABLED
            UnaryBaseInPlace<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>
#else
            UnaryBaseOutOfPlace<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>, 
                               
                               double , Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>
            >
#endif
            {

                internal static Double Instance = new Double();

                
                public unsafe override void Strided64(Storage<double> A, Storage<double> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    double* outP = (double*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    double* inP = (double*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = Math.Sqrt(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }

#endregion HYCALPER AUTO GENERATED CODE

}
