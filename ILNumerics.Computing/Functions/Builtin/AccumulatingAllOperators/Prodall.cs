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

namespace ILNumerics.Core.Functions.Builtin {

    #region HYCALPER LOOPSTART

    /*!HC:TYPELIST:
<hycalper>
<type>
<source locate="here">
    double
</source>
<destination>float</destination>
<destination>complex</destination>
<destination>fcomplex</destination>
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
<destination>Single</destination>
<destination>Complex</destination>
<destination>FComplex</destination>
<destination>Sbyte</destination>
<destination>Byte</destination>
<destination>Int16</destination>
<destination>UInt16</destination>
<destination>Int32</destination>
<destination>UInt32</destination>
<destination>Int64</destination>
<destination>UInt64</destination>
</type>
</hycalper>
*/
    internal static partial class MathInternal {

        /// <summary>Computes the product of all elements of <paramref name="A"/>.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Scalar array with the product of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>This operation returns a scalar array, regardless of the shape of <paramref name="A"/>. The number of dimensions 
        /// of the scalar returned depends on the setting of <see cref="Settings.MinNumberOfArrayDimensions"/>. If <paramref name="A"/> 
        /// is empty the scalar value returned corresponds to the default value of the element datatype. For numerical element types the 
        /// default value is 0. For boolean / logical elements the default is false.</para>
        /// </remarks>
        
        internal unsafe static Array<double> prodall(BaseArray<double> A) {
            return InnerLoops.Prodall.Double.Instance.operate(
                A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>);
        }
    }
    namespace InnerLoops {

        namespace Prodall {

            internal class Double
                : AccumulatingAllBase<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>,
                                      double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> {

                internal static Double Instance = new Double();

                internal override unsafe void Strided(Storage<double> A, Storage<double> Ret) {

                    double s = 1;

                    var endIdx = A.S.NumberOfElements;
                    for (int j = 0; j < endIdx; j++) {
                        var v = A.GetValue(j);
                        s = (double)(s * v);
                    }

                    Ret.SetValueSeq(s, 0);

                }
            }
        }
    }

    #endregion HYCALPER LOOPEND ACCUMALL_OPERATOR_TEMPLATE
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

   
    internal static partial class MathInternal {

        /// <summary>Computes the product of all elements of <paramref name="A"/>.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Scalar array with the product of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>This operation returns a scalar array, regardless of the shape of <paramref name="A"/>. The number of dimensions 
        /// of the scalar returned depends on the setting of <see cref="Settings.MinNumberOfArrayDimensions"/>. If <paramref name="A"/> 
        /// is empty the scalar value returned corresponds to the default value of the element datatype. For numerical element types the 
        /// default value is 0. For boolean / logical elements the default is false.</para>
        /// </remarks>
        
        internal unsafe static Array<ulong> prodall(BaseArray<ulong> A) {
            return InnerLoops.Prodall.UInt64.Instance.operate(
                A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>);
        }
    }
    namespace InnerLoops {

        namespace Prodall {

            internal class UInt64
                : AccumulatingAllBase<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>,
                                      ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>> {

                internal static UInt64 Instance = new UInt64();

                internal override unsafe void Strided(Storage<ulong> A, Storage<ulong> Ret) {

                    ulong s = 1;

                    var endIdx = A.S.NumberOfElements;
                    for (int j = 0; j < endIdx; j++) {
                        var v = A.GetValue(j);
                        s = (ulong)(s * v);
                    }

                    Ret.SetValueSeq(s, 0);

                }
            }
        }
    }


   
    internal static partial class MathInternal {

        /// <summary>Computes the product of all elements of <paramref name="A"/>.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Scalar array with the product of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>This operation returns a scalar array, regardless of the shape of <paramref name="A"/>. The number of dimensions 
        /// of the scalar returned depends on the setting of <see cref="Settings.MinNumberOfArrayDimensions"/>. If <paramref name="A"/> 
        /// is empty the scalar value returned corresponds to the default value of the element datatype. For numerical element types the 
        /// default value is 0. For boolean / logical elements the default is false.</para>
        /// </remarks>
        
        internal unsafe static Array<long> prodall(BaseArray<long> A) {
            return InnerLoops.Prodall.Int64.Instance.operate(
                A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>);
        }
    }
    namespace InnerLoops {

        namespace Prodall {

            internal class Int64
                : AccumulatingAllBase<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>,
                                      long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>> {

                internal static Int64 Instance = new Int64();

                internal override unsafe void Strided(Storage<long> A, Storage<long> Ret) {

                    long s = 1;

                    var endIdx = A.S.NumberOfElements;
                    for (int j = 0; j < endIdx; j++) {
                        var v = A.GetValue(j);
                        s = (long)(s * v);
                    }

                    Ret.SetValueSeq(s, 0);

                }
            }
        }
    }


   
    internal static partial class MathInternal {

        /// <summary>Computes the product of all elements of <paramref name="A"/>.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Scalar array with the product of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>This operation returns a scalar array, regardless of the shape of <paramref name="A"/>. The number of dimensions 
        /// of the scalar returned depends on the setting of <see cref="Settings.MinNumberOfArrayDimensions"/>. If <paramref name="A"/> 
        /// is empty the scalar value returned corresponds to the default value of the element datatype. For numerical element types the 
        /// default value is 0. For boolean / logical elements the default is false.</para>
        /// </remarks>
        
        internal unsafe static Array<uint> prodall(BaseArray<uint> A) {
            return InnerLoops.Prodall.UInt32.Instance.operate(
                A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>);
        }
    }
    namespace InnerLoops {

        namespace Prodall {

            internal class UInt32
                : AccumulatingAllBase<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>,
                                      uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>> {

                internal static UInt32 Instance = new UInt32();

                internal override unsafe void Strided(Storage<uint> A, Storage<uint> Ret) {

                    uint s = 1;

                    var endIdx = A.S.NumberOfElements;
                    for (int j = 0; j < endIdx; j++) {
                        var v = A.GetValue(j);
                        s = (uint)(s * v);
                    }

                    Ret.SetValueSeq(s, 0);

                }
            }
        }
    }


   
    internal static partial class MathInternal {

        /// <summary>Computes the product of all elements of <paramref name="A"/>.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Scalar array with the product of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>This operation returns a scalar array, regardless of the shape of <paramref name="A"/>. The number of dimensions 
        /// of the scalar returned depends on the setting of <see cref="Settings.MinNumberOfArrayDimensions"/>. If <paramref name="A"/> 
        /// is empty the scalar value returned corresponds to the default value of the element datatype. For numerical element types the 
        /// default value is 0. For boolean / logical elements the default is false.</para>
        /// </remarks>
        
        internal unsafe static Array<int> prodall(BaseArray<int> A) {
            return InnerLoops.Prodall.Int32.Instance.operate(
                A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>);
        }
    }
    namespace InnerLoops {

        namespace Prodall {

            internal class Int32
                : AccumulatingAllBase<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>,
                                      int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>> {

                internal static Int32 Instance = new Int32();

                internal override unsafe void Strided(Storage<int> A, Storage<int> Ret) {

                    int s = 1;

                    var endIdx = A.S.NumberOfElements;
                    for (int j = 0; j < endIdx; j++) {
                        var v = A.GetValue(j);
                        s = (int)(s * v);
                    }

                    Ret.SetValueSeq(s, 0);

                }
            }
        }
    }


   
    internal static partial class MathInternal {

        /// <summary>Computes the product of all elements of <paramref name="A"/>.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Scalar array with the product of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>This operation returns a scalar array, regardless of the shape of <paramref name="A"/>. The number of dimensions 
        /// of the scalar returned depends on the setting of <see cref="Settings.MinNumberOfArrayDimensions"/>. If <paramref name="A"/> 
        /// is empty the scalar value returned corresponds to the default value of the element datatype. For numerical element types the 
        /// default value is 0. For boolean / logical elements the default is false.</para>
        /// </remarks>
        
        internal unsafe static Array<ushort> prodall(BaseArray<ushort> A) {
            return InnerLoops.Prodall.UInt16.Instance.operate(
                A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>);
        }
    }
    namespace InnerLoops {

        namespace Prodall {

            internal class UInt16
                : AccumulatingAllBase<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>,
                                      ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>> {

                internal static UInt16 Instance = new UInt16();

                internal override unsafe void Strided(Storage<ushort> A, Storage<ushort> Ret) {

                    ushort s = 1;

                    var endIdx = A.S.NumberOfElements;
                    for (int j = 0; j < endIdx; j++) {
                        var v = A.GetValue(j);
                        s = (ushort)(s * v);
                    }

                    Ret.SetValueSeq(s, 0);

                }
            }
        }
    }


   
    internal static partial class MathInternal {

        /// <summary>Computes the product of all elements of <paramref name="A"/>.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Scalar array with the product of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>This operation returns a scalar array, regardless of the shape of <paramref name="A"/>. The number of dimensions 
        /// of the scalar returned depends on the setting of <see cref="Settings.MinNumberOfArrayDimensions"/>. If <paramref name="A"/> 
        /// is empty the scalar value returned corresponds to the default value of the element datatype. For numerical element types the 
        /// default value is 0. For boolean / logical elements the default is false.</para>
        /// </remarks>
        
        internal unsafe static Array<short> prodall(BaseArray<short> A) {
            return InnerLoops.Prodall.Int16.Instance.operate(
                A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>);
        }
    }
    namespace InnerLoops {

        namespace Prodall {

            internal class Int16
                : AccumulatingAllBase<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>,
                                      short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>> {

                internal static Int16 Instance = new Int16();

                internal override unsafe void Strided(Storage<short> A, Storage<short> Ret) {

                    short s = 1;

                    var endIdx = A.S.NumberOfElements;
                    for (int j = 0; j < endIdx; j++) {
                        var v = A.GetValue(j);
                        s = (short)(s * v);
                    }

                    Ret.SetValueSeq(s, 0);

                }
            }
        }
    }


   
    internal static partial class MathInternal {

        /// <summary>Computes the product of all elements of <paramref name="A"/>.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Scalar array with the product of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>This operation returns a scalar array, regardless of the shape of <paramref name="A"/>. The number of dimensions 
        /// of the scalar returned depends on the setting of <see cref="Settings.MinNumberOfArrayDimensions"/>. If <paramref name="A"/> 
        /// is empty the scalar value returned corresponds to the default value of the element datatype. For numerical element types the 
        /// default value is 0. For boolean / logical elements the default is false.</para>
        /// </remarks>
        
        internal unsafe static Array<byte> prodall(BaseArray<byte> A) {
            return InnerLoops.Prodall.Byte.Instance.operate(
                A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>);
        }
    }
    namespace InnerLoops {

        namespace Prodall {

            internal class Byte
                : AccumulatingAllBase<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>,
                                      byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>> {

                internal static Byte Instance = new Byte();

                internal override unsafe void Strided(Storage<byte> A, Storage<byte> Ret) {

                    byte s = 1;

                    var endIdx = A.S.NumberOfElements;
                    for (int j = 0; j < endIdx; j++) {
                        var v = A.GetValue(j);
                        s = (byte)(s * v);
                    }

                    Ret.SetValueSeq(s, 0);

                }
            }
        }
    }


   
    internal static partial class MathInternal {

        /// <summary>Computes the product of all elements of <paramref name="A"/>.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Scalar array with the product of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>This operation returns a scalar array, regardless of the shape of <paramref name="A"/>. The number of dimensions 
        /// of the scalar returned depends on the setting of <see cref="Settings.MinNumberOfArrayDimensions"/>. If <paramref name="A"/> 
        /// is empty the scalar value returned corresponds to the default value of the element datatype. For numerical element types the 
        /// default value is 0. For boolean / logical elements the default is false.</para>
        /// </remarks>
        
        internal unsafe static Array<sbyte> prodall(BaseArray<sbyte> A) {
            return InnerLoops.Prodall.Sbyte.Instance.operate(
                A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>);
        }
    }
    namespace InnerLoops {

        namespace Prodall {

            internal class Sbyte
                : AccumulatingAllBase<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>,
                                      sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>> {

                internal static Sbyte Instance = new Sbyte();

                internal override unsafe void Strided(Storage<sbyte> A, Storage<sbyte> Ret) {

                    sbyte s = 1;

                    var endIdx = A.S.NumberOfElements;
                    for (int j = 0; j < endIdx; j++) {
                        var v = A.GetValue(j);
                        s = (sbyte)(s * v);
                    }

                    Ret.SetValueSeq(s, 0);

                }
            }
        }
    }


   
    internal static partial class MathInternal {

        /// <summary>Computes the product of all elements of <paramref name="A"/>.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Scalar array with the product of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>This operation returns a scalar array, regardless of the shape of <paramref name="A"/>. The number of dimensions 
        /// of the scalar returned depends on the setting of <see cref="Settings.MinNumberOfArrayDimensions"/>. If <paramref name="A"/> 
        /// is empty the scalar value returned corresponds to the default value of the element datatype. For numerical element types the 
        /// default value is 0. For boolean / logical elements the default is false.</para>
        /// </remarks>
        
        internal unsafe static Array<fcomplex> prodall(BaseArray<fcomplex> A) {
            return InnerLoops.Prodall.FComplex.Instance.operate(
                A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>);
        }
    }
    namespace InnerLoops {

        namespace Prodall {

            internal class FComplex
                : AccumulatingAllBase<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>,
                                      fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>> {

                internal static FComplex Instance = new FComplex();

                internal override unsafe void Strided(Storage<fcomplex> A, Storage<fcomplex> Ret) {

                    fcomplex s = 1;

                    var endIdx = A.S.NumberOfElements;
                    for (int j = 0; j < endIdx; j++) {
                        var v = A.GetValue(j);
                        s = (fcomplex)(s * v);
                    }

                    Ret.SetValueSeq(s, 0);

                }
            }
        }
    }


   
    internal static partial class MathInternal {

        /// <summary>Computes the product of all elements of <paramref name="A"/>.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Scalar array with the product of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>This operation returns a scalar array, regardless of the shape of <paramref name="A"/>. The number of dimensions 
        /// of the scalar returned depends on the setting of <see cref="Settings.MinNumberOfArrayDimensions"/>. If <paramref name="A"/> 
        /// is empty the scalar value returned corresponds to the default value of the element datatype. For numerical element types the 
        /// default value is 0. For boolean / logical elements the default is false.</para>
        /// </remarks>
        
        internal unsafe static Array<complex> prodall(BaseArray<complex> A) {
            return InnerLoops.Prodall.Complex.Instance.operate(
                A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>);
        }
    }
    namespace InnerLoops {

        namespace Prodall {

            internal class Complex
                : AccumulatingAllBase<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>,
                                      complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>> {

                internal static Complex Instance = new Complex();

                internal override unsafe void Strided(Storage<complex> A, Storage<complex> Ret) {

                    complex s = 1;

                    var endIdx = A.S.NumberOfElements;
                    for (int j = 0; j < endIdx; j++) {
                        var v = A.GetValue(j);
                        s = (complex)(s * v);
                    }

                    Ret.SetValueSeq(s, 0);

                }
            }
        }
    }


   
    internal static partial class MathInternal {

        /// <summary>Computes the product of all elements of <paramref name="A"/>.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Scalar array with the product of all elements of <paramref name="A"/>.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>This operation returns a scalar array, regardless of the shape of <paramref name="A"/>. The number of dimensions 
        /// of the scalar returned depends on the setting of <see cref="Settings.MinNumberOfArrayDimensions"/>. If <paramref name="A"/> 
        /// is empty the scalar value returned corresponds to the default value of the element datatype. For numerical element types the 
        /// default value is 0. For boolean / logical elements the default is false.</para>
        /// </remarks>
        
        internal unsafe static Array<float> prodall(BaseArray<float> A) {
            return InnerLoops.Prodall.Single.Instance.operate(
                A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>);
        }
    }
    namespace InnerLoops {

        namespace Prodall {

            internal class Single
                : AccumulatingAllBase<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>,
                                      float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>> {

                internal static Single Instance = new Single();

                internal override unsafe void Strided(Storage<float> A, Storage<float> Ret) {

                    float s = 1;

                    var endIdx = A.S.NumberOfElements;
                    for (int j = 0; j < endIdx; j++) {
                        var v = A.GetValue(j);
                        s = (float)(s * v);
                    }

                    Ret.SetValueSeq(s, 0);

                }
            }
        }
    }


#endregion HYCALPER AUTO GENERATED CODE

}

