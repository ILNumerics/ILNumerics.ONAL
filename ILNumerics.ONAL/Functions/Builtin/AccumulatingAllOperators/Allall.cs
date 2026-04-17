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

    #region HYCALPER LOOPSTART ACCUMALL_OPERATOR_TEMPLATE

    internal static partial class MathInternal {

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
    <destination>bool</destination>
</type>
<type>
    <source locate="after" endmark=" )=(*.">
        innerElemType
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
    <destination>byte</destination>
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
    <destination>Bool</destination>
</type>
<type>
    <source locate="here">
        <![CDATA[<bool, Array<bool>, InArray<bool>, OutArray<bool>, Array<bool>, Storage<bool>]]>
    </source>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination><![CDATA[<bool,Logical,InLogical,OutLogical,Logical,LogicalStorage]]></destination>
</type>
<type>
    <source locate="here">
        <![CDATA[Storage<bool>]]>
    </source>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination></destination>
    <destination><![CDATA[LogicalStorage]]></destination>
</type>
<type>
    <source locate="after" endmark=" (">
        funcname
    </source>
    <destination>allall</destination>
    <destination>allall</destination>
    <destination>allall</destination>
    <destination>allall</destination>
    <destination>allall</destination>
    <destination>allall</destination>
    <destination>allall</destination>
    <destination>allall</destination>
    <destination>allall</destination>
    <destination>allall</destination>
    <destination>allall</destination>
    <destination>allallInternal</destination>
</type>
<type>
    <source locate="after" endmark=" ({">
        innerloopname
    </source>
    <destination>Allall</destination>
    <destination>Allall</destination>
    <destination>Allall</destination>
    <destination>Allall</destination>
    <destination>Allall</destination>
    <destination>Allall</destination>
    <destination>Allall</destination>
    <destination>Allall</destination>
    <destination>Allall</destination>
    <destination>Allall</destination>
    <destination>Allall</destination>
    <destination>Allall</destination>
</type>
</hycalper>
*/

        /// <summary>
        /// Tests if all elements of <paramref name="A"/> are non-zero.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>New scalar logical. True if the test for non-zero succeeded for all elements of <paramref name="A"/>. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>For floating point element types, 'not a number' (<see cref="float.NaN"/>) and other special floating point values (Inf) are considered non-zero.</para>
        /// </remarks>
        internal unsafe static Logical /*!HC:funcname*/allall(BaseArray<double> A) {
            return InnerLoops.Allall.Double.Instance.operate(
                A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>);
        }
    }
    namespace InnerLoops {

        namespace /*!HC:innerloopname*/Allall {

            internal class Double 
                : AccumulatingAllBase<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>,
                                     bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>
                {
                 
                internal static Double Instance = new Double();

                internal override unsafe void Strided(Storage<double> A, LogicalStorage Ret) {

                    var s = true;
                    var endIdx = A.S.NumberOfElements; 
                    for (var t = 0; t < endIdx; t++) {
                        var v = A.GetValue(t); 
                        if (v == default(double)) {
                            s = false;
                            break; 
                        } 
                    }

                    Ret.Assign(Ret.SetValue(s, 0), true, fromRetT: false);
                }
            }
        }
    }
#endregion HYCALPER LOOPEND ACCUMALL_OPERATOR_TEMPLATE
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

    internal static partial class MathInternal {

       

        /// <summary>
        /// Tests if all elements of <paramref name="A"/> are non-zero.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>New scalar logical. True if the test for non-zero succeeded for all elements of <paramref name="A"/>. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>For floating point element types, 'not a number' (<see cref="float.NaN"/>) and other special floating point values (Inf) are considered non-zero.</para>
        /// </remarks>
        internal unsafe static Logical allallInternal(BaseArray<bool> A) {
            return InnerLoops.Allall.Bool.Instance.operate(
                A as ConcreteArray<bool,Logical,InLogical,OutLogical,Logical,LogicalStorage>);
        }
    }
    namespace InnerLoops {

        namespace Allall {

            internal class Bool 
                : AccumulatingAllBase<bool,Logical,InLogical,OutLogical,Logical,LogicalStorage,
                                     bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>
                {
                 
                internal static Bool Instance = new Bool();

                internal override unsafe void Strided(LogicalStorage A, LogicalStorage Ret) {

                    var s = true;
                    var endIdx = A.S.NumberOfElements; 
                    for (var t = 0; t < endIdx; t++) {
                        var v = A.GetValue(t); 
                        if (v == default(bool)) {
                            s = false;
                            break; 
                        } 
                    }

                    Ret.Assign(Ret.SetValue(s, 0), true, fromRetT: false);
                }
            }
        }
    }

    internal static partial class MathInternal {

       

        /// <summary>
        /// Tests if all elements of <paramref name="A"/> are non-zero.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>New scalar logical. True if the test for non-zero succeeded for all elements of <paramref name="A"/>. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>For floating point element types, 'not a number' (<see cref="float.NaN"/>) and other special floating point values (Inf) are considered non-zero.</para>
        /// </remarks>
        internal unsafe static Logical allall(BaseArray<ulong> A) {
            return InnerLoops.Allall.UInt64.Instance.operate(
                A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>);
        }
    }
    namespace InnerLoops {

        namespace Allall {

            internal class UInt64 
                : AccumulatingAllBase<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>,
                                     bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>
                {
                 
                internal static UInt64 Instance = new UInt64();

                internal override unsafe void Strided(Storage<ulong> A, LogicalStorage Ret) {

                    var s = true;
                    var endIdx = A.S.NumberOfElements; 
                    for (var t = 0; t < endIdx; t++) {
                        var v = A.GetValue(t); 
                        if (v == default(ulong)) {
                            s = false;
                            break; 
                        } 
                    }

                    Ret.Assign(Ret.SetValue(s, 0), true, fromRetT: false);
                }
            }
        }
    }

    internal static partial class MathInternal {

       

        /// <summary>
        /// Tests if all elements of <paramref name="A"/> are non-zero.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>New scalar logical. True if the test for non-zero succeeded for all elements of <paramref name="A"/>. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>For floating point element types, 'not a number' (<see cref="float.NaN"/>) and other special floating point values (Inf) are considered non-zero.</para>
        /// </remarks>
        internal unsafe static Logical allall(BaseArray<long> A) {
            return InnerLoops.Allall.Int64.Instance.operate(
                A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>);
        }
    }
    namespace InnerLoops {

        namespace Allall {

            internal class Int64 
                : AccumulatingAllBase<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>,
                                     bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>
                {
                 
                internal static Int64 Instance = new Int64();

                internal override unsafe void Strided(Storage<long> A, LogicalStorage Ret) {

                    var s = true;
                    var endIdx = A.S.NumberOfElements; 
                    for (var t = 0; t < endIdx; t++) {
                        var v = A.GetValue(t); 
                        if (v == default(long)) {
                            s = false;
                            break; 
                        } 
                    }

                    Ret.Assign(Ret.SetValue(s, 0), true, fromRetT: false);
                }
            }
        }
    }

    internal static partial class MathInternal {

       

        /// <summary>
        /// Tests if all elements of <paramref name="A"/> are non-zero.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>New scalar logical. True if the test for non-zero succeeded for all elements of <paramref name="A"/>. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>For floating point element types, 'not a number' (<see cref="float.NaN"/>) and other special floating point values (Inf) are considered non-zero.</para>
        /// </remarks>
        internal unsafe static Logical allall(BaseArray<uint> A) {
            return InnerLoops.Allall.UInt32.Instance.operate(
                A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>);
        }
    }
    namespace InnerLoops {

        namespace Allall {

            internal class UInt32 
                : AccumulatingAllBase<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>,
                                     bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>
                {
                 
                internal static UInt32 Instance = new UInt32();

                internal override unsafe void Strided(Storage<uint> A, LogicalStorage Ret) {

                    var s = true;
                    var endIdx = A.S.NumberOfElements; 
                    for (var t = 0; t < endIdx; t++) {
                        var v = A.GetValue(t); 
                        if (v == default(uint)) {
                            s = false;
                            break; 
                        } 
                    }

                    Ret.Assign(Ret.SetValue(s, 0), true, fromRetT: false);
                }
            }
        }
    }

    internal static partial class MathInternal {

       

        /// <summary>
        /// Tests if all elements of <paramref name="A"/> are non-zero.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>New scalar logical. True if the test for non-zero succeeded for all elements of <paramref name="A"/>. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>For floating point element types, 'not a number' (<see cref="float.NaN"/>) and other special floating point values (Inf) are considered non-zero.</para>
        /// </remarks>
        internal unsafe static Logical allall(BaseArray<int> A) {
            return InnerLoops.Allall.Int32.Instance.operate(
                A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>);
        }
    }
    namespace InnerLoops {

        namespace Allall {

            internal class Int32 
                : AccumulatingAllBase<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>,
                                     bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>
                {
                 
                internal static Int32 Instance = new Int32();

                internal override unsafe void Strided(Storage<int> A, LogicalStorage Ret) {

                    var s = true;
                    var endIdx = A.S.NumberOfElements; 
                    for (var t = 0; t < endIdx; t++) {
                        var v = A.GetValue(t); 
                        if (v == default(int)) {
                            s = false;
                            break; 
                        } 
                    }

                    Ret.Assign(Ret.SetValue(s, 0), true, fromRetT: false);
                }
            }
        }
    }

    internal static partial class MathInternal {

       

        /// <summary>
        /// Tests if all elements of <paramref name="A"/> are non-zero.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>New scalar logical. True if the test for non-zero succeeded for all elements of <paramref name="A"/>. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>For floating point element types, 'not a number' (<see cref="float.NaN"/>) and other special floating point values (Inf) are considered non-zero.</para>
        /// </remarks>
        internal unsafe static Logical allall(BaseArray<ushort> A) {
            return InnerLoops.Allall.UInt16.Instance.operate(
                A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>);
        }
    }
    namespace InnerLoops {

        namespace Allall {

            internal class UInt16 
                : AccumulatingAllBase<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>,
                                     bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>
                {
                 
                internal static UInt16 Instance = new UInt16();

                internal override unsafe void Strided(Storage<ushort> A, LogicalStorage Ret) {

                    var s = true;
                    var endIdx = A.S.NumberOfElements; 
                    for (var t = 0; t < endIdx; t++) {
                        var v = A.GetValue(t); 
                        if (v == default(ushort)) {
                            s = false;
                            break; 
                        } 
                    }

                    Ret.Assign(Ret.SetValue(s, 0), true, fromRetT: false);
                }
            }
        }
    }

    internal static partial class MathInternal {

       

        /// <summary>
        /// Tests if all elements of <paramref name="A"/> are non-zero.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>New scalar logical. True if the test for non-zero succeeded for all elements of <paramref name="A"/>. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>For floating point element types, 'not a number' (<see cref="float.NaN"/>) and other special floating point values (Inf) are considered non-zero.</para>
        /// </remarks>
        internal unsafe static Logical allall(BaseArray<short> A) {
            return InnerLoops.Allall.Int16.Instance.operate(
                A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>);
        }
    }
    namespace InnerLoops {

        namespace Allall {

            internal class Int16 
                : AccumulatingAllBase<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>,
                                     bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>
                {
                 
                internal static Int16 Instance = new Int16();

                internal override unsafe void Strided(Storage<short> A, LogicalStorage Ret) {

                    var s = true;
                    var endIdx = A.S.NumberOfElements; 
                    for (var t = 0; t < endIdx; t++) {
                        var v = A.GetValue(t); 
                        if (v == default(short)) {
                            s = false;
                            break; 
                        } 
                    }

                    Ret.Assign(Ret.SetValue(s, 0), true, fromRetT: false);
                }
            }
        }
    }

    internal static partial class MathInternal {

       

        /// <summary>
        /// Tests if all elements of <paramref name="A"/> are non-zero.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>New scalar logical. True if the test for non-zero succeeded for all elements of <paramref name="A"/>. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>For floating point element types, 'not a number' (<see cref="float.NaN"/>) and other special floating point values (Inf) are considered non-zero.</para>
        /// </remarks>
        internal unsafe static Logical allall(BaseArray<byte> A) {
            return InnerLoops.Allall.Byte.Instance.operate(
                A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>);
        }
    }
    namespace InnerLoops {

        namespace Allall {

            internal class Byte 
                : AccumulatingAllBase<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>,
                                     bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>
                {
                 
                internal static Byte Instance = new Byte();

                internal override unsafe void Strided(Storage<byte> A, LogicalStorage Ret) {

                    var s = true;
                    var endIdx = A.S.NumberOfElements; 
                    for (var t = 0; t < endIdx; t++) {
                        var v = A.GetValue(t); 
                        if (v == default(byte)) {
                            s = false;
                            break; 
                        } 
                    }

                    Ret.Assign(Ret.SetValue(s, 0), true, fromRetT: false);
                }
            }
        }
    }

    internal static partial class MathInternal {

       

        /// <summary>
        /// Tests if all elements of <paramref name="A"/> are non-zero.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>New scalar logical. True if the test for non-zero succeeded for all elements of <paramref name="A"/>. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>For floating point element types, 'not a number' (<see cref="float.NaN"/>) and other special floating point values (Inf) are considered non-zero.</para>
        /// </remarks>
        internal unsafe static Logical allall(BaseArray<sbyte> A) {
            return InnerLoops.Allall.Sbyte.Instance.operate(
                A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>);
        }
    }
    namespace InnerLoops {

        namespace Allall {

            internal class Sbyte 
                : AccumulatingAllBase<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>,
                                     bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>
                {
                 
                internal static Sbyte Instance = new Sbyte();

                internal override unsafe void Strided(Storage<sbyte> A, LogicalStorage Ret) {

                    var s = true;
                    var endIdx = A.S.NumberOfElements; 
                    for (var t = 0; t < endIdx; t++) {
                        var v = A.GetValue(t); 
                        if (v == default(sbyte)) {
                            s = false;
                            break; 
                        } 
                    }

                    Ret.Assign(Ret.SetValue(s, 0), true, fromRetT: false);
                }
            }
        }
    }

    internal static partial class MathInternal {

       

        /// <summary>
        /// Tests if all elements of <paramref name="A"/> are non-zero.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>New scalar logical. True if the test for non-zero succeeded for all elements of <paramref name="A"/>. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>For floating point element types, 'not a number' (<see cref="float.NaN"/>) and other special floating point values (Inf) are considered non-zero.</para>
        /// </remarks>
        internal unsafe static Logical allall(BaseArray<fcomplex> A) {
            return InnerLoops.Allall.FComplex.Instance.operate(
                A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>);
        }
    }
    namespace InnerLoops {

        namespace Allall {

            internal class FComplex 
                : AccumulatingAllBase<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>,
                                     bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>
                {
                 
                internal static FComplex Instance = new FComplex();

                internal override unsafe void Strided(Storage<fcomplex> A, LogicalStorage Ret) {

                    var s = true;
                    var endIdx = A.S.NumberOfElements; 
                    for (var t = 0; t < endIdx; t++) {
                        var v = A.GetValue(t); 
                        if (v == default(fcomplex)) {
                            s = false;
                            break; 
                        } 
                    }

                    Ret.Assign(Ret.SetValue(s, 0), true, fromRetT: false);
                }
            }
        }
    }

    internal static partial class MathInternal {

       

        /// <summary>
        /// Tests if all elements of <paramref name="A"/> are non-zero.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>New scalar logical. True if the test for non-zero succeeded for all elements of <paramref name="A"/>. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>For floating point element types, 'not a number' (<see cref="float.NaN"/>) and other special floating point values (Inf) are considered non-zero.</para>
        /// </remarks>
        internal unsafe static Logical allall(BaseArray<complex> A) {
            return InnerLoops.Allall.Complex.Instance.operate(
                A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>);
        }
    }
    namespace InnerLoops {

        namespace Allall {

            internal class Complex 
                : AccumulatingAllBase<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>,
                                     bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>
                {
                 
                internal static Complex Instance = new Complex();

                internal override unsafe void Strided(Storage<complex> A, LogicalStorage Ret) {

                    var s = true;
                    var endIdx = A.S.NumberOfElements; 
                    for (var t = 0; t < endIdx; t++) {
                        var v = A.GetValue(t); 
                        if (v == default(complex)) {
                            s = false;
                            break; 
                        } 
                    }

                    Ret.Assign(Ret.SetValue(s, 0), true, fromRetT: false);
                }
            }
        }
    }

    internal static partial class MathInternal {

       

        /// <summary>
        /// Tests if all elements of <paramref name="A"/> are non-zero.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>New scalar logical. True if the test for non-zero succeeded for all elements of <paramref name="A"/>. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>For floating point element types, 'not a number' (<see cref="float.NaN"/>) and other special floating point values (Inf) are considered non-zero.</para>
        /// </remarks>
        internal unsafe static Logical allall(BaseArray<float> A) {
            return InnerLoops.Allall.Single.Instance.operate(
                A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>);
        }
    }
    namespace InnerLoops {

        namespace Allall {

            internal class Single 
                : AccumulatingAllBase<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>,
                                     bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>
                {
                 
                internal static Single Instance = new Single();

                internal override unsafe void Strided(Storage<float> A, LogicalStorage Ret) {

                    var s = true;
                    var endIdx = A.S.NumberOfElements; 
                    for (var t = 0; t < endIdx; t++) {
                        var v = A.GetValue(t); 
                        if (v == default(float)) {
                            s = false;
                            break; 
                        } 
                    }

                    Ret.Assign(Ret.SetValue(s, 0), true, fromRetT: false);
                }
            }
        }
    }

#endregion HYCALPER AUTO GENERATED CODE

    internal static partial class MathInternal {

        /// <summary>
        /// Tests if all elements of <paramref name="A"/> are non-zero.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>New scalar logical. True if the test for non-zero succeeded for all elements of <paramref name="A"/>. False otherwise.</returns>
        /// <remarks><para>The operation is efficiently performed and elements of the input array <paramref name="A"/> are not iterated if the number of Trues are known for the logical input.</para>
        /// <para>If elements must be iterated the operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered.</para>
        /// <para>For floating point element types, 'not a number' (<see cref="float.NaN"/>) and other special floating point values (Inf) are considered non-zero.</para>
        /// </remarks>

        internal unsafe static Logical allall(BaseArray<bool> A) {
            var concrArray = A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>;
            LogicalStorage logStorage = concrArray?.Storage;
            if (logStorage != null) {
                if (logStorage.IsNumberTruesCached) {
                    return logStorage.NumberTrues == logStorage.S.NumberOfElements;
                }
                return InnerLoops.Allall.Bool.Instance.operate(concrArray);
            } else {
                var storage = (A as ConcreteArray<bool, Array<bool>, InArray<bool>, OutArray<bool>, Array<bool>, StorageLayer.Storage<bool>>).Storage;
                if (storage != null) {
                    foreach (var v in storage.RetArray) {
                        if (!v) return false;
                    }
                    return true;
                } else {
                    throw new ArgumentException($"The type of the array argument {nameof(A)} could not be determined. A logical array was expected.");
                }
            }
        }
    }

}

