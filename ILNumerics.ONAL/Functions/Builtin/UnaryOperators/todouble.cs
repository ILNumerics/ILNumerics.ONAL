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
        <destination>float</destination>
        <destination>long</destination>
        <destination>ulong</destination>
        <destination>int</destination>
        <destination>uint</destination>
        <destination>short</destination>
        <destination>ushort</destination>
        <destination>sbyte</destination>
        <destination>byte</destination>
        <destination>complex</destination>
        <destination>fcomplex</destination>
    </type>
    <type>
    <source locate="after">
        outArr
    </source>
        <destination>double</destination>
        <destination>double</destination>
        <destination>double</destination>
        <destination>double</destination>
        <destination>double</destination>
        <destination>double</destination>
        <destination>double</destination>
        <destination>double</destination>
        <destination>double</destination>
        <destination>double</destination>
        <destination>double</destination>
    </type>
    <type>
    <source locate="here">
        Double
    </source>
        <destination>Single</destination>
        <destination>Int64</destination>
        <destination>UInt64</destination>
        <destination>Int32</destination>
        <destination>UInt32</destination>
        <destination>Int16</destination>
        <destination>UInt16</destination>
        <destination>SByte</destination>
        <destination>Byte</destination>
        <destination>Complex</destination>
        <destination>FComplex</destination>
    </type>
    <type>
        <source locate="after">
            funcname
        </source>
        <destination>todouble</destination>
        <destination>todouble</destination>
        <destination>todouble</destination>
        <destination>todouble</destination>
        <destination>todouble</destination>
        <destination>todouble</destination>
        <destination>todouble</destination>
        <destination>todouble</destination>
        <destination>todouble</destination>
        <destination>todouble</destination>
        <destination>todouble</destination>
    </type>
    <type>
        <source locate="after" endmark=" ()">
            operatorfunc
        </source>
        <destination>(double)</destination>
        <destination>(double)</destination>
        <destination>(double)</destination>
        <destination>(double)</destination>
        <destination>(double)</destination>
        <destination>(double)</destination>
        <destination>(double)</destination>
        <destination>(double)</destination>
        <destination>(double)</destination>
        <destination>(double)</destination>
        <destination>(double)</destination>
    </type>
    <type>
        <source locate="after" endmark=" :,*.()">
            innerloopname
        </source>
        <destination>ToDouble</destination>
        <destination>ToDouble</destination>
        <destination>ToDouble</destination>
        <destination>ToDouble</destination>
        <destination>ToDouble</destination>
        <destination>ToDouble</destination>
        <destination>ToDouble</destination>
        <destination>ToDouble</destination>
        <destination>ToDouble</destination>
        <destination>ToDouble</destination>
        <destination>ToDouble</destination>
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
        <destination><![CDATA[#if IMPLACE_DISABLED]]></destination>
    </type>
    <type>
        <source locate="comment">
            summary
        </source>
        <destination>Convert elements to Double.</destination>
        <destination>Convert elements to Double.</destination>
        <destination>Convert elements to Double.</destination>
        <destination>Convert elements to Double.</destination>
        <destination>Convert elements to Double.</destination>
        <destination>Convert elements to Double.</destination>
        <destination>Convert elements to Double.</destination>
        <destination>Convert elements to Double.</destination>
        <destination>Convert elements to Double.</destination>
        <destination>Convert elements to Double.</destination>
        <destination>Convert elements to Double.</destination>
    </type>
    <type>
        <source locate="comment">
            returns
        </source>
        <destination><![CDATA[Array{double} of the same shape as <paramref name="A"/>]]>.</destination>
        <destination><![CDATA[Array{double} of the same shape as <paramref name="A"/>]]>.</destination>
        <destination><![CDATA[Array{double} of the same shape as <paramref name="A"/>]]>.</destination>
        <destination><![CDATA[Array{double} of the same shape as <paramref name="A"/>]]>.</destination>
        <destination><![CDATA[Array{double} of the same shape as <paramref name="A"/>]]>.</destination>
        <destination><![CDATA[Array{double} of the same shape as <paramref name="A"/>]]>.</destination>
        <destination><![CDATA[Array{double} of the same shape as <paramref name="A"/>]]>.</destination>
        <destination><![CDATA[Array{double} of the same shape as <paramref name="A"/>]]>.</destination>
        <destination><![CDATA[Array{double} of the same shape as <paramref name="A"/>]]>.</destination>
        <destination><![CDATA[Array{double} of the same shape as <paramref name="A"/>]]>.</destination>
        <destination><![CDATA[Array{double} of the same shape as <paramref name="A"/>]]>.</destination>
    </type>
</hycalper>
*/

namespace ILNumerics.Core.Functions.Builtin {

    #region HYCALPER LOOPSTART UNARY_OPERATOR_TEMPLATE@Functions\Builtin\UnaryOperators\Sin.cs

    #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
   
    internal static partial class MathInternal {

        /// <summary>Convert elements to Double.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{double} of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<double> todouble(BaseArray<fcomplex> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.ToDouble.FComplex.Instance.operate(A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace ToDouble {

            
            internal sealed class FComplex :
            #if IMPLACE_DISABLED
            UnaryBaseInPlace<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>
#else
            UnaryBaseOutOfPlace<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>, 
                               
                               double , Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>
            >
#endif
            {

                internal static FComplex Instance = new FComplex();

                
                public unsafe override void Strided64(Storage<fcomplex> A, Storage<double> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    double* outP = (double*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    fcomplex* inP = (fcomplex*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = (double)(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Convert elements to Double.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{double} of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<double> todouble(BaseArray<complex> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.ToDouble.Complex.Instance.operate(A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace ToDouble {

            
            internal sealed class Complex :
            #if IMPLACE_DISABLED
            UnaryBaseInPlace<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>
#else
            UnaryBaseOutOfPlace<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>, 
                               
                               double , Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>
            >
#endif
            {

                internal static Complex Instance = new Complex();

                
                public unsafe override void Strided64(Storage<complex> A, Storage<double> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    double* outP = (double*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    complex* inP = (complex*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = (double)(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Convert elements to Double.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{double} of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<double> todouble(BaseArray<byte> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.ToDouble.Byte.Instance.operate(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace ToDouble {

            
            internal sealed class Byte :
            #if IMPLACE_DISABLED
            UnaryBaseInPlace<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>
#else
            UnaryBaseOutOfPlace<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>, 
                               
                               double , Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>
            >
#endif
            {

                internal static Byte Instance = new Byte();

                
                public unsafe override void Strided64(Storage<byte> A, Storage<double> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    double* outP = (double*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    byte* inP = (byte*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = (double)(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Convert elements to Double.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{double} of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<double> todouble(BaseArray<sbyte> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.ToDouble.SByte.Instance.operate(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace ToDouble {

            
            internal sealed class SByte :
            #if IMPLACE_DISABLED
            UnaryBaseInPlace<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>
#else
            UnaryBaseOutOfPlace<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>, 
                               
                               double , Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>
            >
#endif
            {

                internal static SByte Instance = new SByte();

                
                public unsafe override void Strided64(Storage<sbyte> A, Storage<double> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    double* outP = (double*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    sbyte* inP = (sbyte*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = (double)(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Convert elements to Double.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{double} of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<double> todouble(BaseArray<ushort> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.ToDouble.UInt16.Instance.operate(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace ToDouble {

            
            internal sealed class UInt16 :
            #if IMPLACE_DISABLED
            UnaryBaseInPlace<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>
#else
            UnaryBaseOutOfPlace<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>, 
                               
                               double , Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>
            >
#endif
            {

                internal static UInt16 Instance = new UInt16();

                
                public unsafe override void Strided64(Storage<ushort> A, Storage<double> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    double* outP = (double*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    ushort* inP = (ushort*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = (double)(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Convert elements to Double.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{double} of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<double> todouble(BaseArray<short> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.ToDouble.Int16.Instance.operate(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace ToDouble {

            
            internal sealed class Int16 :
            #if IMPLACE_DISABLED
            UnaryBaseInPlace<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>
#else
            UnaryBaseOutOfPlace<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>, 
                               
                               double , Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>
            >
#endif
            {

                internal static Int16 Instance = new Int16();

                
                public unsafe override void Strided64(Storage<short> A, Storage<double> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    double* outP = (double*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    short* inP = (short*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = (double)(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Convert elements to Double.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{double} of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<double> todouble(BaseArray<uint> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.ToDouble.UInt32.Instance.operate(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace ToDouble {

            
            internal sealed class UInt32 :
            #if IMPLACE_DISABLED
            UnaryBaseInPlace<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>
#else
            UnaryBaseOutOfPlace<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>, 
                               
                               double , Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>
            >
#endif
            {

                internal static UInt32 Instance = new UInt32();

                
                public unsafe override void Strided64(Storage<uint> A, Storage<double> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    double* outP = (double*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    uint* inP = (uint*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = (double)(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Convert elements to Double.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{double} of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<double> todouble(BaseArray<int> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.ToDouble.Int32.Instance.operate(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace ToDouble {

            
            internal sealed class Int32 :
            #if IMPLACE_DISABLED
            UnaryBaseInPlace<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>
#else
            UnaryBaseOutOfPlace<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>, 
                               
                               double , Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>
            >
#endif
            {

                internal static Int32 Instance = new Int32();

                
                public unsafe override void Strided64(Storage<int> A, Storage<double> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    double* outP = (double*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    int* inP = (int*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = (double)(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Convert elements to Double.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{double} of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<double> todouble(BaseArray<ulong> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.ToDouble.UInt64.Instance.operate(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace ToDouble {

            
            internal sealed class UInt64 :
            #if IMPLACE_DISABLED
            UnaryBaseInPlace<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>
#else
            UnaryBaseOutOfPlace<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>, 
                               
                               double , Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>
            >
#endif
            {

                internal static UInt64 Instance = new UInt64();

                
                public unsafe override void Strided64(Storage<ulong> A, Storage<double> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    double* outP = (double*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    ulong* inP = (ulong*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = (double)(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Convert elements to Double.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{double} of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<double> todouble(BaseArray<long> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.ToDouble.Int64.Instance.operate(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace ToDouble {

            
            internal sealed class Int64 :
            #if IMPLACE_DISABLED
            UnaryBaseInPlace<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>
#else
            UnaryBaseOutOfPlace<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>, 
                               
                               double , Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>
            >
#endif
            {

                internal static Int64 Instance = new Int64();

                
                public unsafe override void Strided64(Storage<long> A, Storage<double> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    double* outP = (double*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    long* inP = (long*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = (double)(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Convert elements to Double.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array{double} of the same shape as <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<double> todouble(BaseArray<float> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.ToDouble.Single.Instance.operate(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace ToDouble {

            
            internal sealed class Single :
            #if IMPLACE_DISABLED
            UnaryBaseInPlace<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>
#else
            UnaryBaseOutOfPlace<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>, 
                               
                               double , Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>
            >
#endif
            {

                internal static Single Instance = new Single();

                
                public unsafe override void Strided64(Storage<float> A, Storage<double> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    double* outP = (double*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    float* inP = (float*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = (double)(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }

#endregion HYCALPER AUTO GENERATED CODE

    internal static partial class MathInternal {

        /// <summary>
        /// NOP. Converts numeric array from double to <see cref="System.Double"/> elements. This is provided for convenience reasons. No element values are copied.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>New array.</returns>
        internal static Array<double> todouble(BaseArray<double> A) {
            return A?.ToArray<double>();
        }

        /// <summary>
        /// Convert numeric array of unknown type to an ILNumerics array with <see cref="System.Double"/> elements.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>New array.</returns>
        internal static Array<double> todouble(BaseArray A) {
            if (object.Equals(A, null) || A is BaseArray<double>) {
                return A?.ToArray<double>(); // save the null check 
            } else if (A is BaseArray<float>) {
                return todouble(A as BaseArray<float>); 
            } else if (A is BaseArray<long>) {
                return todouble(A as BaseArray<long>);
            } else if (A is BaseArray<ulong>) {
                return todouble(A as BaseArray<ulong>);
            } else if (A is BaseArray<int>) {
                return todouble(A as BaseArray<int>);
            } else if (A is BaseArray<uint>) {
                return todouble(A as BaseArray<uint>);
            } else if (A is BaseArray<short>) {
                return todouble(A as BaseArray<short>);
            } else if (A is BaseArray<ushort>) {
                return todouble(A as BaseArray<ushort>);
            } else if (A is BaseArray<sbyte>) {
                return todouble(A as BaseArray<sbyte>);
            } else if (A is BaseArray<byte>) {
                return todouble(A as BaseArray<byte>);
            } else if (A is BaseArray<complex>) {
                return todouble(A as BaseArray<complex>);
            } else if (A is BaseArray<fcomplex>) {
                return todouble(A as BaseArray<fcomplex>);
            } else {
                throw new InvalidCastException($"Unable to convert BaseArray of type {A.GetType().Name} to Array<double>."); 
            }
        }
    }

}
