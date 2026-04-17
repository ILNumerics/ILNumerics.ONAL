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
        <destination>long</destination>
        <destination>ulong</destination>
        <destination>int</destination>
        <destination>uint</destination>
        <destination>short</destination>
        <destination>ushort</destination>
        <destination>complex</destination>
        <destination>fcomplex</destination>
    </type>
    <type>
    <source locate="after">
        outArr
    </source>
        <destination>sbyte</destination>
        <destination>sbyte</destination>
        <destination>sbyte</destination>
        <destination>sbyte</destination>
        <destination>sbyte</destination>
        <destination>sbyte</destination>
        <destination>sbyte</destination>
        <destination>sbyte</destination>
        <destination>sbyte</destination>
        <destination>sbyte</destination>
    </type>
    <type>
    <source locate="here">
        Double
    </source>
        <destination>Double</destination>
        <destination>Single</destination>
        <destination>Int64</destination>
        <destination>UInt64</destination>
        <destination>Int32</destination>
        <destination>UInt32</destination>
        <destination>Int16</destination>
        <destination>UInt16</destination>
        <destination>Complex</destination>
        <destination>FComplex</destination>
    </type>
    <type>
        <source locate="after">
            funcname
        </source>
        <destination>toint8</destination>
        <destination>toint8</destination>
        <destination>toint8</destination>
        <destination>toint8</destination>
        <destination>toint8</destination>
        <destination>toint8</destination>
        <destination>toint8</destination>
        <destination>toint8</destination>
        <destination>toint8</destination>
        <destination>toint8</destination>
    </type>
    <type>
        <source locate="after" endmark=" ()">
            operatorfunc
        </source>
        <destination>(sbyte)</destination>
        <destination>(sbyte)</destination>
        <destination>(sbyte)</destination>
        <destination>(sbyte)</destination>
        <destination>(sbyte)</destination>
        <destination>(sbyte)</destination>
        <destination>(sbyte)</destination>
        <destination>(sbyte)</destination>
        <destination>(sbyte)</destination>
        <destination>(sbyte)</destination>
    </type>
    <type>
        <source locate="after" endmark=" :,*.()">
            innerloopname
        </source>
        <destination>ToInt8</destination>
        <destination>ToInt8</destination>
        <destination>ToInt8</destination>
        <destination>ToInt8</destination>
        <destination>ToInt8</destination>
        <destination>ToInt8</destination>
        <destination>ToInt8</destination>
        <destination>ToInt8</destination>
        <destination>ToInt8</destination>
        <destination>ToInt8</destination>
    </type>
    <type>
        <source locate="nextline">
            implacedisabled
        </source>
        <destination><![CDATA[#if IMPLACE_DISABLED]]></destination>
        <destination><![CDATA[#if IMPLACE_DISABLED]]></destination>
        <destination><![CDATA[#if IMPLACE_DISABLED]]></destination>
        <destination><![CDATA[#if IMPLACE_DISABLED]]></destination>
        <destination><![CDATA[#if IMPLACE_DISABLED]]></destination>
        <destination><![CDATA[#if IMPLACE_DISABLED]]></destination>
        <destination><![CDATA[#if IMPLACE_DISABLED]]></destination>
        <destination><![CDATA[#if IMPLACE_DISABLED]]></destination>
        <destination><![CDATA[#if IMPLACE_DISABLED]]></destination>
        <destination><![CDATA[#if IMPLACE_DISABLED]]></destination>
    </type>
    <type>
        <source locate="comment">
            summary
        </source>
        <destination>Convert elements to SByte.</destination>
        <destination>Convert elements to SByte.</destination>
        <destination>Convert elements to SByte.</destination>
        <destination>Convert elements to SByte.</destination>
        <destination>Convert elements to SByte.</destination>
        <destination>Convert elements to SByte.</destination>
        <destination>Convert elements to SByte.</destination>
        <destination>Convert elements to SByte.</destination>
        <destination>Convert elements to SByte.</destination>
        <destination>Convert elements to SByte.</destination>
    </type>
    <type>
        <source locate="comment">
            returns
        </source>
        <destination><![CDATA[Array{sbyte} of the same shape as <paramref name="A"/>]]>.</destination>
        <destination><![CDATA[Array{sbyte} of the same shape as <paramref name="A"/>]]>.</destination>
        <destination><![CDATA[Array{sbyte} of the same shape as <paramref name="A"/>]]>.</destination>
        <destination><![CDATA[Array{sbyte} of the same shape as <paramref name="A"/>]]>.</destination>
        <destination><![CDATA[Array{sbyte} of the same shape as <paramref name="A"/>]]>.</destination>
        <destination><![CDATA[Array{sbyte} of the same shape as <paramref name="A"/>]]>.</destination>
        <destination><![CDATA[Array{sbyte} of the same shape as <paramref name="A"/>]]>.</destination>
        <destination><![CDATA[Array{sbyte} of the same shape as <paramref name="A"/>]]>.</destination>
        <destination><![CDATA[Array{sbyte} of the same shape as <paramref name="A"/>]]>.</destination>
        <destination><![CDATA[Array{sbyte} of the same shape as <paramref name="A"/>]]>.</destination>
    </type>
</hycalper>
*/

namespace ILNumerics.Core.Functions.Builtin {

    #region HYCALPER LOOPSTART UNARY_OPERATOR_TEMPLATE@Functions\Builtin\UnaryOperators\Sin.cs

    #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
   
    internal static partial class MathInternal {

        /// <summary>Convert elements to SByte.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{sbyte} of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<sbyte> toint8(BaseArray<fcomplex> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.ToInt8.FComplex.Instance.operate(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace ToInt8 {

            
            internal sealed class FComplex :
            #if IMPLACE_DISABLED
            UnaryBaseInPlace<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>
#else
            UnaryBaseOutOfPlace<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>, 
                               
                               sbyte , Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>
            >
#endif
            {

                internal static FComplex Instance = new FComplex();

                
                public unsafe override void Strided64(Storage<fcomplex> A, Storage<sbyte> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    sbyte* outP = (sbyte*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    fcomplex* inP = (fcomplex*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = (sbyte)(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Convert elements to SByte.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{sbyte} of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<sbyte> toint8(BaseArray<complex> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.ToInt8.Complex.Instance.operate(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace ToInt8 {

            
            internal sealed class Complex :
            #if IMPLACE_DISABLED
            UnaryBaseInPlace<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>
#else
            UnaryBaseOutOfPlace<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>, 
                               
                               sbyte , Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>
            >
#endif
            {

                internal static Complex Instance = new Complex();

                
                public unsafe override void Strided64(Storage<complex> A, Storage<sbyte> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    sbyte* outP = (sbyte*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    complex* inP = (complex*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = (sbyte)(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Convert elements to SByte.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{sbyte} of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<sbyte> toint8(BaseArray<ushort> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.ToInt8.UInt16.Instance.operate(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace ToInt8 {

            
            internal sealed class UInt16 :
            #if IMPLACE_DISABLED
            UnaryBaseInPlace<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>
#else
            UnaryBaseOutOfPlace<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>, 
                               
                               sbyte , Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>
            >
#endif
            {

                internal static UInt16 Instance = new UInt16();

                
                public unsafe override void Strided64(Storage<ushort> A, Storage<sbyte> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    sbyte* outP = (sbyte*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    ushort* inP = (ushort*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = (sbyte)(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Convert elements to SByte.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{sbyte} of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<sbyte> toint8(BaseArray<short> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.ToInt8.Int16.Instance.operate(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace ToInt8 {

            
            internal sealed class Int16 :
            #if IMPLACE_DISABLED
            UnaryBaseInPlace<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>
#else
            UnaryBaseOutOfPlace<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>, 
                               
                               sbyte , Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>
            >
#endif
            {

                internal static Int16 Instance = new Int16();

                
                public unsafe override void Strided64(Storage<short> A, Storage<sbyte> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    sbyte* outP = (sbyte*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    short* inP = (short*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = (sbyte)(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Convert elements to SByte.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{sbyte} of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<sbyte> toint8(BaseArray<uint> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.ToInt8.UInt32.Instance.operate(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace ToInt8 {

            
            internal sealed class UInt32 :
            #if IMPLACE_DISABLED
            UnaryBaseInPlace<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>
#else
            UnaryBaseOutOfPlace<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>, 
                               
                               sbyte , Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>
            >
#endif
            {

                internal static UInt32 Instance = new UInt32();

                
                public unsafe override void Strided64(Storage<uint> A, Storage<sbyte> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    sbyte* outP = (sbyte*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    uint* inP = (uint*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = (sbyte)(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Convert elements to SByte.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{sbyte} of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<sbyte> toint8(BaseArray<int> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.ToInt8.Int32.Instance.operate(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace ToInt8 {

            
            internal sealed class Int32 :
            #if IMPLACE_DISABLED
            UnaryBaseInPlace<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>
#else
            UnaryBaseOutOfPlace<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>, 
                               
                               sbyte , Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>
            >
#endif
            {

                internal static Int32 Instance = new Int32();

                
                public unsafe override void Strided64(Storage<int> A, Storage<sbyte> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    sbyte* outP = (sbyte*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    int* inP = (int*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = (sbyte)(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Convert elements to SByte.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{sbyte} of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<sbyte> toint8(BaseArray<ulong> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.ToInt8.UInt64.Instance.operate(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace ToInt8 {

            
            internal sealed class UInt64 :
            #if IMPLACE_DISABLED
            UnaryBaseInPlace<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>
#else
            UnaryBaseOutOfPlace<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>, 
                               
                               sbyte , Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>
            >
#endif
            {

                internal static UInt64 Instance = new UInt64();

                
                public unsafe override void Strided64(Storage<ulong> A, Storage<sbyte> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    sbyte* outP = (sbyte*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    ulong* inP = (ulong*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = (sbyte)(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Convert elements to SByte.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{sbyte} of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<sbyte> toint8(BaseArray<long> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.ToInt8.Int64.Instance.operate(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace ToInt8 {

            
            internal sealed class Int64 :
            #if IMPLACE_DISABLED
            UnaryBaseInPlace<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>
#else
            UnaryBaseOutOfPlace<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>, 
                               
                               sbyte , Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>
            >
#endif
            {

                internal static Int64 Instance = new Int64();

                
                public unsafe override void Strided64(Storage<long> A, Storage<sbyte> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    sbyte* outP = (sbyte*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    long* inP = (long*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = (sbyte)(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Convert elements to SByte.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{sbyte} of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<sbyte> toint8(BaseArray<float> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.ToInt8.Single.Instance.operate(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace ToInt8 {

            
            internal sealed class Single :
            #if IMPLACE_DISABLED
            UnaryBaseInPlace<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>
#else
            UnaryBaseOutOfPlace<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>, 
                               
                               sbyte , Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>
            >
#endif
            {

                internal static Single Instance = new Single();

                
                public unsafe override void Strided64(Storage<float> A, Storage<sbyte> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    sbyte* outP = (sbyte*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    float* inP = (float*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = (sbyte)(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Convert elements to SByte.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{sbyte} of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<sbyte> toint8(BaseArray<double> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.ToInt8.Double.Instance.operate(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace ToInt8 {

            
            internal sealed class Double :
            #if IMPLACE_DISABLED
            UnaryBaseInPlace<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>
#else
            UnaryBaseOutOfPlace<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>, 
                               
                               sbyte , Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>
            >
#endif
            {

                internal static Double Instance = new Double();

                
                public unsafe override void Strided64(Storage<double> A, Storage<sbyte> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    sbyte* outP = (sbyte*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    double* inP = (double*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = (sbyte)(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }

#endregion HYCALPER AUTO GENERATED CODE

    internal static partial class MathInternal {

        /// <summary>
        /// NOP. Converts numeric array from sbyte to <see cref="sbyte"/> elements. This is provided for convenience reasons. No element values are copied.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>New array.</returns>
        internal static Array<sbyte> toint8(BaseArray<sbyte> A) {
            return A?.ToArray<sbyte>();
        }
        
        /// <summary>
        /// Convert numeric array of unknown type to an ILNumerics array with <see cref="System.SByte"/> elements.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>New array.</returns>
        internal static Array<sbyte> toint8(BaseArray A) {
            if (object.Equals(A, null) || A is BaseArray<sbyte>) {
                return A?.ToArray<sbyte>(); // safe the null check 
            } else if (A is BaseArray<float>) {
                return toint8(A as BaseArray<float>);
            } else if (A is BaseArray<long>) {
                return toint8(A as BaseArray<long>);
            } else if (A is BaseArray<ulong>) {
                return toint8(A as BaseArray<ulong>);
            } else if (A is BaseArray<int>) {
                return toint8(A as BaseArray<int>);
            } else if (A is BaseArray<uint>) {
                return toint8(A as BaseArray<uint>);
            } else if (A is BaseArray<short>) {
                return toint8(A as BaseArray<short>);
            } else if (A is BaseArray<ushort>) {
                return toint8(A as BaseArray<ushort>);
            } else if (A is BaseArray<double>) {
                return toint8(A as BaseArray<double>);
            } else if (A is BaseArray<byte>) {
                return toint8(A as BaseArray<byte>);
            } else if (A is BaseArray<complex>) {
                return toint8(A as BaseArray<complex>);
            } else if (A is BaseArray<fcomplex>) {
                return toint8(A as BaseArray<fcomplex>);
            } else {
                throw new InvalidCastException($"Unable to convert BaseArray of type {A.GetType().Name} to Array<sbyte>."); 
            }
        }
        /// <summary>(Reinterpret) cast array of unsigned elements to signed elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{short} of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed without copying any elements. The returned array 
        /// has the same storage order and the same size as A.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<sbyte> toint8(BaseArray<byte> A) {
            //TODO: this + int8, uint64..8 (all int types) !
            if (object.Equals(A, null)) {
                return null;
            }
            var storage = (A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>).Storage;
            var ret = Storage<sbyte>.Create();
            ret.S.SetAll(storage.S);
            ret.Handles = storage.Handles;
            ret.Handles.Retain();
            return ret.RetArray;
        }

    }

}
