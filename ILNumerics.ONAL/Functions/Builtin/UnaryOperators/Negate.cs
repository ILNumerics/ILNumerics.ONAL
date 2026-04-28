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
using ILNumerics.Core.Arrays;
using ILNumerics.Core.StorageLayer;
using System;
using System.Security;

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
        <destination>float</destination>
        <destination>double</destination>
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
        <destination>Single</destination>
        <destination>Double</destination>
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
        <destination>float</destination>
        <destination>double</destination>
    </type>
    <type>
        <source locate="after">
            funcname
        </source>
        <destination>negate</destination>
        <destination>negate</destination>
        <destination>negate</destination>
        <destination>negate</destination>
        <destination>negate</destination>
        <destination>negate</destination>
        <destination>negate</destination>
        <destination>negate</destination>
        <destination>negate</destination>
    </type>
    <type>
        <source locate="after" endmark=" ()">
            operatorfunc
        </source>
        <destination>(sbyte) -</destination>
        <destination>(byte) -</destination>
        <destination>(short) -</destination>
        <destination>(ushort) -</destination>
        <destination>-</destination>
        <destination>(uint) -</destination>
        <destination>-</destination>
        <destination>-</destination>
        <destination>-</destination>
    </type>
    <type>
        <source locate="after" endmark=" :,*.()">
            innerloopname
        </source>
        <destination>Negate</destination>
        <destination>Negate</destination>
        <destination>Negate</destination>
        <destination>Negate</destination>
        <destination>Negate</destination>
        <destination>Negate</destination>
        <destination>Negate</destination>
        <destination>Negate</destination>
        <destination>Negate</destination>
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
        <destination>#if !IMPLACE_DISABLED</destination>
    </type>
    <type>
        <source locate="comment">
            summary
        </source>
        <destination>Unary negation of array elements.</destination>
        <destination>Unary negation of array elements.</destination>
        <destination>Unary negation of array elements.</destination>
        <destination>Unary negation of array elements.</destination>
        <destination>Unary negation of array elements.</destination>
        <destination>Unary negation of array elements.</destination>
        <destination>Unary negation of array elements.</destination>
        <destination>Unary negation of array elements.</destination>
        <destination>Unary negation of array elements.</destination>
    </type>
    <type>
        <source locate="comment"> 
            returns
        </source>
        <destination>Unary negation of array elements.</destination>
        <destination>Unary negation of array elements.</destination>
        <destination>Unary negation of array elements.</destination>
        <destination>Unary negation of array elements.</destination>
        <destination>Unary negation of array elements.</destination>
        <destination>Unary negation of array elements.</destination>
        <destination>Unary negation of array elements.</destination>
        <destination>Unary negation of array elements.</destination>
        <destination>Unary negation of array elements.</destination>
    </type>
</hycalper> 
*/

namespace ILNumerics.Core.Functions.Builtin {

    #region HYCALPER LOOPSTART UNARY_OPERATOR_TEMPLATE@Functions\Builtin\UnaryOperators\Sin.cs

    #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
   
    internal static partial class MathInternal {

        /// <summary>Unary negation of array elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Unary negation of array elements.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<double> negate(BaseArray<double> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.Negate.Double.Instance.operate(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace Negate {

            
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
                        outP[outI] = -(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Unary negation of array elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Unary negation of array elements.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<float> negate(BaseArray<float> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.Negate.Single.Instance.operate(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace Negate {

            
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
                        outP[outI] = -(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Unary negation of array elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Unary negation of array elements.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<long> negate(BaseArray<long> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.Negate.Int64.Instance.operate(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace Negate {

            
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
                        outP[outI] = -(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Unary negation of array elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Unary negation of array elements.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<uint> negate(BaseArray<uint> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.Negate.UInt32.Instance.operate(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace Negate {

            
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
                        outP[outI] = (uint) -(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Unary negation of array elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Unary negation of array elements.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<int> negate(BaseArray<int> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.Negate.Int32.Instance.operate(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace Negate {

            
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
                        outP[outI] = -(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Unary negation of array elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Unary negation of array elements.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<ushort> negate(BaseArray<ushort> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.Negate.UInt16.Instance.operate(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace Negate {

            
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
                        outP[outI] = (ushort) -(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Unary negation of array elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Unary negation of array elements.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<short> negate(BaseArray<short> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.Negate.Int16.Instance.operate(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace Negate {

            
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
                        outP[outI] = (short) -(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Unary negation of array elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Unary negation of array elements.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<byte> negate(BaseArray<byte> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.Negate.Byte.Instance.operate(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace Negate {

            
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
                        outP[outI] = (byte) -(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }
   
    internal static partial class MathInternal {

        /// <summary>Unary negation of array elements.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Unary negation of array elements.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para></remarks>
        
        internal unsafe static Array<sbyte> negate(BaseArray<sbyte> A) {

            if (object.Equals(A, null)) {
                return null;
            }
            var ret = InnerLoops.Negate.SByte.Instance.operate(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>);
            return ret; 
        }
    }
    namespace InnerLoops {

        namespace Negate {

            
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
                        outP[outI] = (sbyte) -(inP[i])  /**/; outI++; 
                    }

                }
            }
        }
    }

#endregion HYCALPER AUTO GENERATED CODE

}
