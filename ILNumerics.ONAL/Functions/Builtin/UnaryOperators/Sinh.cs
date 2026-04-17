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
        <destination>complex</destination>
        <destination>fcomplex</destination>
    </type>
    <type>
    <source locate="here">
        Double
    </source>
        <destination>Double</destination>
        <destination>Single</destination>
        <destination>Complex</destination>
        <destination>FComplex</destination>
    </type>
    <type>
        <source locate="after">
            funcname
        </source>
        <destination>sinh</destination>
        <destination>sinh</destination>
        <destination>sinh</destination>
        <destination>sinh</destination>
    </type>
    <type>
        <source locate="after" endmark=" ()">
            operatorfunc
        </source>
        <destination>Math.Sinh</destination>
        <destination>(float)Math.Sinh</destination>
        <destination>complex.Sinh</destination>
        <destination>fcomplex.Sinh</destination>
    </type>
    <type>
        <source locate="after" endmark=" :,*.()">
            innerloopname
        </source>
        <destination>HyperbolicSine</destination>
        <destination>HyperbolicSine</destination>
        <destination>HyperbolicSine</destination>
        <destination>HyperbolicSine</destination>
    </type>
    <type>
        <source locate="comment">
            summary
        </source>
        <destination>Hyperbolic sine of array elements.</destination>
        <destination>Hyperbolic sine of array elements.</destination>
        <destination>Hyperbolic sine of array elements.</destination>
        <destination>Hyperbolic sine of array elements.</destination>
    </type>
    <type>
        <source locate="comment">
            returns
        </source>
        <destination><![CDATA[Array of the same size/type as <paramref name="A"/> with the hyperbolic sine of A's array elements. See: <see cref="System.Math.Cosh(Double)"/>]]></destination>
        <destination><![CDATA[Array of the same size/type as <paramref name="A"/> with the hyperbolic sine of A's array elements. See: <see cref="System.Math.Cosh(Double)"/>]]></destination>
        <destination><![CDATA[Array of the same size/type as <paramref name="A"/> with the hyperbolic sine of A's array elements. See: <see cref="ILNumerics.complex.Cosh(complex)"/>]]></destination>
        <destination><![CDATA[Array of the same size/type as <paramref name="A"/> with the hyperbolic sine of A's array elements. See: <see cref="ILNumerics.fcomplex.Cosh(fcomplex)"/>]]></destination>
    </type>
</hycalper>
*/

namespace ILNumerics.Core.Functions.Builtin {

    #region HYCALPER LOOPSTART UNARY_OPERATOR_TEMPLATE@Functions\Builtin\UnaryOperators\Sin.cs

    #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
   
    internal static partial class MathInternal {

        /// <summary>Hyperbolic sine of array elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array of the same size/type as <paramref name="A"/> with the hyperbolic sine of A's array elements. See: <see cref="ILNumerics.fcomplex.Cosh(fcomplex)"/></returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<fcomplex> sinh(BaseArray<fcomplex> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.HyperbolicSine.FComplex.Instance.operate(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace HyperbolicSine {

            
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
                        outP[outI] = fcomplex.Sinh(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Hyperbolic sine of array elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array of the same size/type as <paramref name="A"/> with the hyperbolic sine of A's array elements. See: <see cref="ILNumerics.complex.Cosh(complex)"/></returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<complex> sinh(BaseArray<complex> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.HyperbolicSine.Complex.Instance.operate(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace HyperbolicSine {

            
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
                        outP[outI] = complex.Sinh(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Hyperbolic sine of array elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array of the same size/type as <paramref name="A"/> with the hyperbolic sine of A's array elements. See: <see cref="System.Math.Cosh(Double)"/></returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<float> sinh(BaseArray<float> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.HyperbolicSine.Single.Instance.operate(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace HyperbolicSine {

            
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
                        outP[outI] = (float)Math.Sinh(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Hyperbolic sine of array elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array of the same size/type as <paramref name="A"/> with the hyperbolic sine of A's array elements. See: <see cref="System.Math.Cosh(Double)"/></returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<double> sinh(BaseArray<double> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.HyperbolicSine.Double.Instance.operate(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace HyperbolicSine {

            
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
                        outP[outI] = Math.Sinh(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }

#endregion HYCALPER AUTO GENERATED CODE

}
