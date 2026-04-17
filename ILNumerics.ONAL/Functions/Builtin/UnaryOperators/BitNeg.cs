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
        <destination>sbyte</destination>
        <destination>byte</destination>
        <destination>short</destination>
        <destination>ushort</destination>
        <destination>int</destination>
        <destination>uint</destination>
        <destination>long</destination>
        <destination>ulong</destination>
    </type>
    <type>
    <source locate="here">
        Double
    </source>
        <destination>SByte</destination>
        <destination>Byte</destination>
        <destination>Int16</destination>
        <destination>UInt16</destination>
        <destination>Int32</destination>
        <destination>UInt32</destination>
        <destination>Int64</destination>
        <destination>UInt64</destination>
    </type>
    <type>
    <source locate="after" endmark=" >*()">
        outArr
    </source>
        <destination>sbyte</destination>
        <destination>byte</destination>
        <destination>short</destination>
        <destination>ushort</destination>
        <destination>int</destination>
        <destination>uint</destination>
        <destination>long</destination>
        <destination>ulong</destination>
    </type>
    <type>
        <source locate="after">
            funcname
        </source>
        <destination>bitneg</destination>
        <destination>bitneg</destination>
        <destination>bitneg</destination>
        <destination>bitneg</destination>
        <destination>bitneg</destination>
        <destination>bitneg</destination>
        <destination>bitneg</destination>
        <destination>bitneg</destination>
    </type>
    <type>
        <source locate="after" endmark=" ()">
            operatorfunc
        </source>
        <destination>(sbyte) ~</destination>
        <destination>(byte) ~</destination>
        <destination>(short) ~</destination>
        <destination>(ushort) ~</destination>
        <destination>~</destination>
        <destination>~</destination>
        <destination>~</destination>
        <destination>~</destination>
    </type>
    <type>
        <source locate="after" endmark=" :,*.()">
            innerloopname
        </source>
        <destination>BitNeg</destination>
        <destination>BitNeg</destination>
        <destination>BitNeg</destination>
        <destination>BitNeg</destination>
        <destination>BitNeg</destination>
        <destination>BitNeg</destination>
        <destination>BitNeg</destination>
        <destination>BitNeg</destination>
    </type>
    <type>
        <source locate="nextline">
            implacedisabled
        </source>
        <destination>#if !IMPLACE_DISABLED</destination>
        <destination>#if !IMPLACE_DISABLED</destination>
        <destination>#if !IMPLACE_DISABLED</destination>
        <destination>#if !IMPLACE_DISABLED</destination>
        <destination>#if !IMPLACE_DISABLED</destination>
        <destination>#if !IMPLACE_DISABLED</destination>
        <destination>#if !IMPLACE_DISABLED</destination>
        <destination>#if !IMPLACE_DISABLED</destination>
    </type>
    <type>
        <source locate="comment">
            summary
        </source>
        <destination>Bitwise negation of array elements.</destination>
        <destination>Bitwise negation of array elements.</destination>
        <destination>Bitwise negation of array elements.</destination>
        <destination>Bitwise negation of array elements.</destination>
        <destination>Bitwise negation of array elements.</destination>
        <destination>Bitwise negation of array elements.</destination>
        <destination>Bitwise negation of array elements.</destination>
        <destination>Bitwise negation of array elements.</destination>
    </type>
    <type>
        <source locate="comment"> 
            returns
        </source>
        <destination>Bitwise negation of array elements.</destination>
        <destination>Bitwise negation of array elements.</destination>
        <destination>Bitwise negation of array elements.</destination>
        <destination>Bitwise negation of array elements.</destination>
        <destination>Bitwise negation of array elements.</destination>
        <destination>Bitwise negation of array elements.</destination>
        <destination>Bitwise negation of array elements.</destination>
        <destination>Bitwise negation of array elements.</destination>
    </type>
</hycalper>
*/

namespace ILNumerics.Core.Functions.Builtin {

    #region HYCALPER LOOPSTART UNARY_OPERATOR_TEMPLATE@Functions\Builtin\UnaryOperators\Sin.cs

    #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
   
    internal static partial class MathInternal {

        /// <summary>Bitwise negation of array elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Bitwise negation of array elements.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<ulong> bitneg(BaseArray<ulong> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.BitNeg.UInt64.Instance.operate(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace BitNeg {

            
            internal sealed class UInt64 :
            #if !IMPLACE_DISABLED
            UnaryBaseInPlace<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>
#else
            UnaryBaseOutOfPlace<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>, 
                               
                               ulong , Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>
            >
#endif
            {

                internal static UInt64 Instance = new UInt64();

                
                public unsafe override void Strided64(Storage<ulong> A, Storage<ulong> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    ulong* outP = (ulong*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    ulong* inP = (ulong*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = ~(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Bitwise negation of array elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Bitwise negation of array elements.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<long> bitneg(BaseArray<long> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.BitNeg.Int64.Instance.operate(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace BitNeg {

            
            internal sealed class Int64 :
            #if !IMPLACE_DISABLED
            UnaryBaseInPlace<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>
#else
            UnaryBaseOutOfPlace<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>, 
                               
                               long , Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>
            >
#endif
            {

                internal static Int64 Instance = new Int64();

                
                public unsafe override void Strided64(Storage<long> A, Storage<long> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    long* outP = (long*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    long* inP = (long*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = ~(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Bitwise negation of array elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Bitwise negation of array elements.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<uint> bitneg(BaseArray<uint> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.BitNeg.UInt32.Instance.operate(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace BitNeg {

            
            internal sealed class UInt32 :
            #if !IMPLACE_DISABLED
            UnaryBaseInPlace<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>
#else
            UnaryBaseOutOfPlace<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>, 
                               
                               uint , Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>
            >
#endif
            {

                internal static UInt32 Instance = new UInt32();

                
                public unsafe override void Strided64(Storage<uint> A, Storage<uint> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    uint* outP = (uint*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    uint* inP = (uint*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = ~(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Bitwise negation of array elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Bitwise negation of array elements.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<int> bitneg(BaseArray<int> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.BitNeg.Int32.Instance.operate(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace BitNeg {

            
            internal sealed class Int32 :
            #if !IMPLACE_DISABLED
            UnaryBaseInPlace<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>
#else
            UnaryBaseOutOfPlace<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>, 
                               
                               int , Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>
            >
#endif
            {

                internal static Int32 Instance = new Int32();

                
                public unsafe override void Strided64(Storage<int> A, Storage<int> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    int* outP = (int*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    int* inP = (int*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = ~(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Bitwise negation of array elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Bitwise negation of array elements.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<ushort> bitneg(BaseArray<ushort> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.BitNeg.UInt16.Instance.operate(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace BitNeg {

            
            internal sealed class UInt16 :
            #if !IMPLACE_DISABLED
            UnaryBaseInPlace<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>
#else
            UnaryBaseOutOfPlace<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>, 
                               
                               ushort , Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>
            >
#endif
            {

                internal static UInt16 Instance = new UInt16();

                
                public unsafe override void Strided64(Storage<ushort> A, Storage<ushort> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    ushort* outP = (ushort*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    ushort* inP = (ushort*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = (ushort) ~(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Bitwise negation of array elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Bitwise negation of array elements.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<short> bitneg(BaseArray<short> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.BitNeg.Int16.Instance.operate(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace BitNeg {

            
            internal sealed class Int16 :
            #if !IMPLACE_DISABLED
            UnaryBaseInPlace<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>
#else
            UnaryBaseOutOfPlace<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>, 
                               
                               short , Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>
            >
#endif
            {

                internal static Int16 Instance = new Int16();

                
                public unsafe override void Strided64(Storage<short> A, Storage<short> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    short* outP = (short*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    short* inP = (short*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = (short) ~(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Bitwise negation of array elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Bitwise negation of array elements.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<byte> bitneg(BaseArray<byte> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.BitNeg.Byte.Instance.operate(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace BitNeg {

            
            internal sealed class Byte :
            #if !IMPLACE_DISABLED
            UnaryBaseInPlace<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>
#else
            UnaryBaseOutOfPlace<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>, 
                               
                               byte , Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>
            >
#endif
            {

                internal static Byte Instance = new Byte();

                
                public unsafe override void Strided64(Storage<byte> A, Storage<byte> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    byte* outP = (byte*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    byte* inP = (byte*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = (byte) ~(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Bitwise negation of array elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Bitwise negation of array elements.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<sbyte> bitneg(BaseArray<sbyte> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.BitNeg.SByte.Instance.operate(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace BitNeg {

            
            internal sealed class SByte :
            #if !IMPLACE_DISABLED
            UnaryBaseInPlace<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>
#else
            UnaryBaseOutOfPlace<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>, 
                               
                               sbyte , Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>
            >
#endif
            {

                internal static SByte Instance = new SByte();

                
                public unsafe override void Strided64(Storage<sbyte> A, Storage<sbyte> Out) {

                    // Out is iterated continously, A may be strided. We use long indices, no inplace, no cache awareness, no unrolling. 
                    var outI = 0;
                   
                    sbyte* outP = (sbyte*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    sbyte* inP = (sbyte*)A.Handles[0].Pointer;  // base offset is included in Size.Iterator! 
                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous); 
                    foreach (var i in A.S.Iterator(Out.S.StorageOrder)) { 
                        outP[outI] = (sbyte) ~(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }

#endregion HYCALPER AUTO GENERATED CODE

}
