//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;

namespace ILNumerics.Core.Arrays {

    /// <summary>
    /// General base class for array objects like <see cref="Array{T}"/> and similar arrays types. 
    /// </summary>
    public abstract partial class ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>   // T1 must not read "T" to prevent from conflicts with the .T (transpose) property.
        : BaseArray<T1>, IEnumerable<T1>
        where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {

        private static bool doSaturate(bool force) {
            if (!force) return false; 
            if (Settings.SaturateIntegerOps.HasValue) {
                return Settings.SaturateIntegerOps.Value;
            } else {
                return Settings.ArrayStyle == ArrayStyles.ILNumericsV4;
            }
        }

        #region HYCALPER LOOPSTART BINARY_OPERATOR_TEMPLATE

        /*!HC:TYPELIST:
        <hycalper>
        <type>
            <source locate="here">
                Add
            </source>
            <destination>Subtract</destination>
            <destination>MultiplyElem</destination>
            <destination>Divide</destination>
            <destination>Modulo</destination>
        </type>
        <type>
            <source locate="comment">
                summary
            </source>
            <destination>Elementwise subtraction operator on two numeric arrays.</destination>
            <destination>Elementwise multiplication operator on two numeric arrays.</destination>
            <destination>Elementwise division operator on two numeric arrays.</destination>
            <destination>Elementwise modulo operator on two numeric arrays.</destination>
        </type>
        <type>
            <source locate="comment">
                returns
            </source>
            <destination>Array with result of A - B, with element type T.</destination>
            <destination>Array with result of A * B, with element type T.</destination>
            <destination>Array with result of A / B, with element type T.</destination>
            <destination>Array with result of A % B, with element type T.</destination>
        </type>
        <type>
            <source locate="here">
                +
            </source>
            <destination>-</destination>
            <destination>*</destination>
            <destination>/</destination>
            <destination>%</destination>
        </type>
        <type>
            <source locate="nextline">
                nocomplex
            </source>
            <destination>#if !NOCOMPLEX</destination>
            <destination>#if !NOCOMPLEX</destination>
            <destination>#if !NOCOMPLEX</destination>
            <destination>#if NOCOMPLEX</destination>
        </type>
        <type>
            <source locate="after">
                IsSaturating
            </source>
            <destination>true</destination>
            <destination>true</destination>
            <destination>false</destination>
            <destination>false</destination>
        </type>
        <type>
            <source locate="nextline">
                PREP_INC_SAT
            </source>
            <destination>#if !SATURATING</destination>
            <destination>#if !SATURATING</destination>
            <destination>#if SATURATING</destination>
            <destination>#if SATURATING</destination>
        </type>
        </hycalper>
        */

        /// <summary>
        /// Elementwise addition operator on two numeric arrays.
        /// </summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The second array.</param>
        /// <returns>Array with result of A + B, with element type T.</returns>
        /// <remarks><para>General notes on binary broadcasting operators: The operation is performed 
        /// elementwise on corresponding elements of <paramref name="A"/> and <paramref name="B"/>. The sizes of both 
        /// input arrays must be broadcastable and determine the size of the resulting array. Note, that 
        /// broadcasting is performed, regardless of the current setting of <see cref="Settings.ArrayStyle"/>. However, 
        /// the <see cref="Settings.ArrayStyle"/> configuration affects the operation in the following ways: 
        /// <list type="bullet">
        /// <item>If <paramref name="A"/> and <paramref name="B"/> have different number of dimensions, the array with 
        /// fewer dimensions is (virtually) padded with singleton dimensions, until the number of dimensions of both arrays match. In 
        /// <see cref="ArrayStyles.ILNumericsV4"/> the singleton dimensions are added to the end of dimensions. For <see cref="ArrayStyles.numpy"/> 
        /// singletons are padded to the start, such that here, broadcasting aligns the <i>last</i> dimensions.</item>
        /// <item><see cref="ArrayStyles.ILNumericsV4"/>: Result values of integer element types T are clamped to the 
        /// natural range of the interger type. I.e.: there will be no 'wrapping around' when exceeding the upper or lower 
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="double"/> precision 
        /// floating point arithmetics and subsequently casting the result to the integer value. For <see cref="ArrayStyles.numpy"/> no such precautions exist and the 
        /// result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// <para>This function is parallelized for multiple cores. If possible, a suitable <paramref name="A"/> or <paramref name="B"/> is 
        /// directly utilized in order to perform the operation inplace: instead of storing the results into new memory the 
        /// input array and/or its storage is reused and returned.</para>
        /// <para>T must be a numeric value type, including <see cref="complex"/> and <see cref="fcomplex"/>.</para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        public static RetT operator +(ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A, ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> B) {
            if (A is BaseArray<double>) {
                return ILNumerics.Core.Functions.Builtin.InnerLoops.Add.Double.Instance.operate(
                    A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, 
                    B as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>) as RetT;
            } else if (A is BaseArray<float>) {
                return Core.Functions.Builtin.InnerLoops.Add.Single.Instance.operate(
                    A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, 
                    B as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>) as RetT;
/*!HC:nocomplex*/
#if !NOCOMPLEX
            } else if (A is BaseArray<complex>) {
                return Core.Functions.Builtin.InnerLoops.Add.Complex.Instance.operate(
                    A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>,
                    B as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>) as RetT;
            } else if (A is BaseArray<fcomplex>) {
                return Core.Functions.Builtin.InnerLoops.Add.FComplex.Instance.operate(
                    A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>,
                    B as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>) as RetT;
#endif
            }

            /*!HC:PREP_INC_SAT*/
#if !SATURATING
            if (doSaturate(/*!HC:IsSaturating*/true)) { // saturating
                if (A is BaseArray<int>) {
                    return Core.Functions.Builtin.InnerLoops.Add.Int32Sat.Instance.operate(
                    A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>,
                    B as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>) as RetT;
                } else if (A is BaseArray<uint>) {
                    return Core.Functions.Builtin.InnerLoops.Add.UInt32Sat.Instance.operate(
                    A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>,
                    B as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>) as RetT;
                } else if (A is BaseArray<long>) {
                    return Core.Functions.Builtin.InnerLoops.Add.Int64Sat.Instance.operate(
                    A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>,
                    B as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>) as RetT;
                } else if (A is BaseArray<ulong>) {
                    return Core.Functions.Builtin.InnerLoops.Add.UInt64Sat.Instance.operate(
                    A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>,
                    B as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>) as RetT;
                } else if (A is BaseArray<short>) {
                    return Core.Functions.Builtin.InnerLoops.Add.Int16Sat.Instance.operate(
                    A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>,
                    B as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>) as RetT;
                } else if (A is BaseArray<ushort>) {
                    return Core.Functions.Builtin.InnerLoops.Add.UInt16Sat.Instance.operate(
                    A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>,
                    B as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>) as RetT;
                } else if (A is BaseArray<sbyte>) {
                    return Core.Functions.Builtin.InnerLoops.Add.SByteSat.Instance.operate(
                    A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>,
                    B as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>) as RetT;
                } else if (A is BaseArray<byte>) {
                    return Core.Functions.Builtin.InnerLoops.Add.ByteSat.Instance.operate(
                    A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>,
                    B as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>) as RetT;
                }
            } else {
#endif
                // numpy style /not saturating but wrapping around
                if (A is BaseArray<int>) {
                    return Core.Functions.Builtin.InnerLoops.Add.Int32.Instance.operate(
                    A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>,
                    B as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>) as RetT;
                } else if (A is BaseArray<uint>) {
                    return Core.Functions.Builtin.InnerLoops.Add.UInt32.Instance.operate(
                    A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>,
                    B as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>) as RetT;
                } else if (A is BaseArray<long>) {
                    return Core.Functions.Builtin.InnerLoops.Add.Int64.Instance.operate(
                    A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>,
                    B as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>) as RetT;
                } else if (A is BaseArray<ulong>) {
                    return Core.Functions.Builtin.InnerLoops.Add.UInt64.Instance.operate(
                    A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>,
                    B as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>) as RetT;
                } else if (A is BaseArray<short>) {
                    return Core.Functions.Builtin.InnerLoops.Add.Int16.Instance.operate(
                    A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>,
                    B as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>) as RetT;
                } else if (A is BaseArray<ushort>) {
                    return Core.Functions.Builtin.InnerLoops.Add.UInt16.Instance.operate(
                    A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>,
                    B as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>) as RetT;
                } else if (A is BaseArray<sbyte>) {
                    return Core.Functions.Builtin.InnerLoops.Add.SByte.Instance.operate(
                    A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>,
                    B as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>) as RetT;
                } else if (A is BaseArray<byte>) {
                    return Core.Functions.Builtin.InnerLoops.Add.Byte.Instance.operate(
                    A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>,
                    B as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>) as RetT;
                }
                /*!HC:PREP_INC_SAT*/
#if !SATURATING
            }
#endif
            throw new ArgumentException($"The '+' (plus) operator is not defined for arrays of type <{typeof(T1).Name}>."); 
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

       

        /// <summary>Elementwise modulo operator on two numeric arrays.</summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The second array.</param>
        /// <returns>Array with result of A % B, with element type T.</returns>
        /// <remarks><para>General notes on binary broadcasting operators: The operation is performed 
        /// elementwise on corresponding elements of <paramref name="A"/> and <paramref name="B"/>. The sizes of both 
        /// input arrays must be broadcastable and determine the size of the resulting array. Note, that 
        /// broadcasting is performed, regardless of the current setting of <see cref="Settings.ArrayStyle"/>. However, 
        /// the <see cref="Settings.ArrayStyle"/> configuration affects the operation in the following ways: 
        /// <list type="bullet">
        /// <item>If <paramref name="A"/> and <paramref name="B"/> have different number of dimensions, the array with 
        /// fewer dimensions is (virtually) padded with singleton dimensions, until the number of dimensions of both arrays match. In 
        /// <see cref="ArrayStyles.ILNumericsV4"/> the singleton dimensions are added to the end of dimensions. For <see cref="ArrayStyles.numpy"/> 
        /// singletons are padded to the start, such that here, broadcasting aligns the <i>last</i> dimensions.</item>
        /// <item><see cref="ArrayStyles.ILNumericsV4"/>: Result values of integer element types T are clamped to the 
        /// natural range of the interger type. I.e.: there will be no 'wrapping around' when exceeding the upper or lower 
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="double"/> precision 
        /// floating point arithmetics and subsequently casting the result to the integer value. For <see cref="ArrayStyles.numpy"/> no such precautions exist and the 
        /// result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// <para>This function is parallelized for multiple cores. If possible, a suitable <paramref name="A"/> or <paramref name="B"/> is 
        /// directly utilized in order to perform the operation inplace: instead of storing the results into new memory the 
        /// input array and/or its storage is reused and returned.</para>
        /// <para>T must be a numeric value type, including <see cref="complex"/> and <see cref="fcomplex"/>.</para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        public static RetT operator %(ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A, ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> B) {
            if (A is BaseArray<double>) {
                return ILNumerics.Core.Functions.Builtin.InnerLoops.Modulo.Double.Instance.operate(
                    A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, 
                    B as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>) as RetT;
            } else if (A is BaseArray<float>) {
                return Core.Functions.Builtin.InnerLoops.Modulo.Single.Instance.operate(
                    A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, 
                    B as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>) as RetT;
#if NOCOMPLEX
            } else if (A is BaseArray<complex>) {
                return Core.Functions.Builtin.InnerLoops.Modulo.Complex.Instance.operate(
                    A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>,
                    B as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>) as RetT;
            } else if (A is BaseArray<fcomplex>) {
                return Core.Functions.Builtin.InnerLoops.Modulo.FComplex.Instance.operate(
                    A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>,
                    B as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>) as RetT;
#endif
            }

            #if SATURATING
            if (doSaturate(false)) { // saturating
                if (A is BaseArray<int>) {
                    return Core.Functions.Builtin.InnerLoops.Modulo.Int32Sat.Instance.operate(
                    A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>,
                    B as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>) as RetT;
                } else if (A is BaseArray<uint>) {
                    return Core.Functions.Builtin.InnerLoops.Modulo.UInt32Sat.Instance.operate(
                    A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>,
                    B as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>) as RetT;
                } else if (A is BaseArray<long>) {
                    return Core.Functions.Builtin.InnerLoops.Modulo.Int64Sat.Instance.operate(
                    A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>,
                    B as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>) as RetT;
                } else if (A is BaseArray<ulong>) {
                    return Core.Functions.Builtin.InnerLoops.Modulo.UInt64Sat.Instance.operate(
                    A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>,
                    B as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>) as RetT;
                } else if (A is BaseArray<short>) {
                    return Core.Functions.Builtin.InnerLoops.Modulo.Int16Sat.Instance.operate(
                    A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>,
                    B as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>) as RetT;
                } else if (A is BaseArray<ushort>) {
                    return Core.Functions.Builtin.InnerLoops.Modulo.UInt16Sat.Instance.operate(
                    A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>,
                    B as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>) as RetT;
                } else if (A is BaseArray<sbyte>) {
                    return Core.Functions.Builtin.InnerLoops.Modulo.SByteSat.Instance.operate(
                    A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>,
                    B as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>) as RetT;
                } else if (A is BaseArray<byte>) {
                    return Core.Functions.Builtin.InnerLoops.Modulo.ByteSat.Instance.operate(
                    A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>,
                    B as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>) as RetT;
                }
            } else {
#endif
                // numpy style /not saturating but wrapping around
                if (A is BaseArray<int>) {
                    return Core.Functions.Builtin.InnerLoops.Modulo.Int32.Instance.operate(
                    A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>,
                    B as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>) as RetT;
                } else if (A is BaseArray<uint>) {
                    return Core.Functions.Builtin.InnerLoops.Modulo.UInt32.Instance.operate(
                    A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>,
                    B as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>) as RetT;
                } else if (A is BaseArray<long>) {
                    return Core.Functions.Builtin.InnerLoops.Modulo.Int64.Instance.operate(
                    A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>,
                    B as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>) as RetT;
                } else if (A is BaseArray<ulong>) {
                    return Core.Functions.Builtin.InnerLoops.Modulo.UInt64.Instance.operate(
                    A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>,
                    B as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>) as RetT;
                } else if (A is BaseArray<short>) {
                    return Core.Functions.Builtin.InnerLoops.Modulo.Int16.Instance.operate(
                    A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>,
                    B as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>) as RetT;
                } else if (A is BaseArray<ushort>) {
                    return Core.Functions.Builtin.InnerLoops.Modulo.UInt16.Instance.operate(
                    A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>,
                    B as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>) as RetT;
                } else if (A is BaseArray<sbyte>) {
                    return Core.Functions.Builtin.InnerLoops.Modulo.SByte.Instance.operate(
                    A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>,
                    B as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>) as RetT;
                } else if (A is BaseArray<byte>) {
                    return Core.Functions.Builtin.InnerLoops.Modulo.Byte.Instance.operate(
                    A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>,
                    B as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>) as RetT;
                }
                #if SATURATING
            }
#endif
            throw new ArgumentException($"The '%' (plus) operator is not defined for arrays of type <{typeof(T1).Name}>."); 
        }

       

        /// <summary>Elementwise division operator on two numeric arrays.</summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The second array.</param>
        /// <returns>Array with result of A / B, with element type T.</returns>
        /// <remarks><para>General notes on binary broadcasting operators: The operation is performed 
        /// elementwise on corresponding elements of <paramref name="A"/> and <paramref name="B"/>. The sizes of both 
        /// input arrays must be broadcastable and determine the size of the resulting array. Note, that 
        /// broadcasting is performed, regardless of the current setting of <see cref="Settings.ArrayStyle"/>. However, 
        /// the <see cref="Settings.ArrayStyle"/> configuration affects the operation in the following ways: 
        /// <list type="bullet">
        /// <item>If <paramref name="A"/> and <paramref name="B"/> have different number of dimensions, the array with 
        /// fewer dimensions is (virtually) padded with singleton dimensions, until the number of dimensions of both arrays match. In 
        /// <see cref="ArrayStyles.ILNumericsV4"/> the singleton dimensions are added to the end of dimensions. For <see cref="ArrayStyles.numpy"/> 
        /// singletons are padded to the start, such that here, broadcasting aligns the <i>last</i> dimensions.</item>
        /// <item><see cref="ArrayStyles.ILNumericsV4"/>: Result values of integer element types T are clamped to the 
        /// natural range of the interger type. I.e.: there will be no 'wrapping around' when exceeding the upper or lower 
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="double"/> precision 
        /// floating point arithmetics and subsequently casting the result to the integer value. For <see cref="ArrayStyles.numpy"/> no such precautions exist and the 
        /// result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// <para>This function is parallelized for multiple cores. If possible, a suitable <paramref name="A"/> or <paramref name="B"/> is 
        /// directly utilized in order to perform the operation inplace: instead of storing the results into new memory the 
        /// input array and/or its storage is reused and returned.</para>
        /// <para>T must be a numeric value type, including <see cref="complex"/> and <see cref="fcomplex"/>.</para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        public static RetT operator /(ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A, ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> B) {
            if (A is BaseArray<double>) {
                return ILNumerics.Core.Functions.Builtin.InnerLoops.Divide.Double.Instance.operate(
                    A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, 
                    B as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>) as RetT;
            } else if (A is BaseArray<float>) {
                return Core.Functions.Builtin.InnerLoops.Divide.Single.Instance.operate(
                    A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, 
                    B as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>) as RetT;
#if !NOCOMPLEX
            } else if (A is BaseArray<complex>) {
                return Core.Functions.Builtin.InnerLoops.Divide.Complex.Instance.operate(
                    A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>,
                    B as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>) as RetT;
            } else if (A is BaseArray<fcomplex>) {
                return Core.Functions.Builtin.InnerLoops.Divide.FComplex.Instance.operate(
                    A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>,
                    B as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>) as RetT;
#endif
            }

            #if SATURATING
            if (doSaturate(false)) { // saturating
                if (A is BaseArray<int>) {
                    return Core.Functions.Builtin.InnerLoops.Divide.Int32Sat.Instance.operate(
                    A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>,
                    B as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>) as RetT;
                } else if (A is BaseArray<uint>) {
                    return Core.Functions.Builtin.InnerLoops.Divide.UInt32Sat.Instance.operate(
                    A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>,
                    B as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>) as RetT;
                } else if (A is BaseArray<long>) {
                    return Core.Functions.Builtin.InnerLoops.Divide.Int64Sat.Instance.operate(
                    A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>,
                    B as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>) as RetT;
                } else if (A is BaseArray<ulong>) {
                    return Core.Functions.Builtin.InnerLoops.Divide.UInt64Sat.Instance.operate(
                    A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>,
                    B as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>) as RetT;
                } else if (A is BaseArray<short>) {
                    return Core.Functions.Builtin.InnerLoops.Divide.Int16Sat.Instance.operate(
                    A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>,
                    B as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>) as RetT;
                } else if (A is BaseArray<ushort>) {
                    return Core.Functions.Builtin.InnerLoops.Divide.UInt16Sat.Instance.operate(
                    A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>,
                    B as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>) as RetT;
                } else if (A is BaseArray<sbyte>) {
                    return Core.Functions.Builtin.InnerLoops.Divide.SByteSat.Instance.operate(
                    A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>,
                    B as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>) as RetT;
                } else if (A is BaseArray<byte>) {
                    return Core.Functions.Builtin.InnerLoops.Divide.ByteSat.Instance.operate(
                    A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>,
                    B as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>) as RetT;
                }
            } else {
#endif
                // numpy style /not saturating but wrapping around
                if (A is BaseArray<int>) {
                    return Core.Functions.Builtin.InnerLoops.Divide.Int32.Instance.operate(
                    A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>,
                    B as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>) as RetT;
                } else if (A is BaseArray<uint>) {
                    return Core.Functions.Builtin.InnerLoops.Divide.UInt32.Instance.operate(
                    A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>,
                    B as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>) as RetT;
                } else if (A is BaseArray<long>) {
                    return Core.Functions.Builtin.InnerLoops.Divide.Int64.Instance.operate(
                    A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>,
                    B as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>) as RetT;
                } else if (A is BaseArray<ulong>) {
                    return Core.Functions.Builtin.InnerLoops.Divide.UInt64.Instance.operate(
                    A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>,
                    B as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>) as RetT;
                } else if (A is BaseArray<short>) {
                    return Core.Functions.Builtin.InnerLoops.Divide.Int16.Instance.operate(
                    A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>,
                    B as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>) as RetT;
                } else if (A is BaseArray<ushort>) {
                    return Core.Functions.Builtin.InnerLoops.Divide.UInt16.Instance.operate(
                    A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>,
                    B as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>) as RetT;
                } else if (A is BaseArray<sbyte>) {
                    return Core.Functions.Builtin.InnerLoops.Divide.SByte.Instance.operate(
                    A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>,
                    B as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>) as RetT;
                } else if (A is BaseArray<byte>) {
                    return Core.Functions.Builtin.InnerLoops.Divide.Byte.Instance.operate(
                    A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>,
                    B as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>) as RetT;
                }
                #if SATURATING
            }
#endif
            throw new ArgumentException($"The '/' (plus) operator is not defined for arrays of type <{typeof(T1).Name}>."); 
        }

       

        /// <summary>Elementwise multiplication operator on two numeric arrays.</summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The second array.</param>
        /// <returns>Array with result of A * B, with element type T.</returns>
        /// <remarks><para>General notes on binary broadcasting operators: The operation is performed 
        /// elementwise on corresponding elements of <paramref name="A"/> and <paramref name="B"/>. The sizes of both 
        /// input arrays must be broadcastable and determine the size of the resulting array. Note, that 
        /// broadcasting is performed, regardless of the current setting of <see cref="Settings.ArrayStyle"/>. However, 
        /// the <see cref="Settings.ArrayStyle"/> configuration affects the operation in the following ways: 
        /// <list type="bullet">
        /// <item>If <paramref name="A"/> and <paramref name="B"/> have different number of dimensions, the array with 
        /// fewer dimensions is (virtually) padded with singleton dimensions, until the number of dimensions of both arrays match. In 
        /// <see cref="ArrayStyles.ILNumericsV4"/> the singleton dimensions are added to the end of dimensions. For <see cref="ArrayStyles.numpy"/> 
        /// singletons are padded to the start, such that here, broadcasting aligns the <i>last</i> dimensions.</item>
        /// <item><see cref="ArrayStyles.ILNumericsV4"/>: Result values of integer element types T are clamped to the 
        /// natural range of the interger type. I.e.: there will be no 'wrapping around' when exceeding the upper or lower 
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="double"/> precision 
        /// floating point arithmetics and subsequently casting the result to the integer value. For <see cref="ArrayStyles.numpy"/> no such precautions exist and the 
        /// result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// <para>This function is parallelized for multiple cores. If possible, a suitable <paramref name="A"/> or <paramref name="B"/> is 
        /// directly utilized in order to perform the operation inplace: instead of storing the results into new memory the 
        /// input array and/or its storage is reused and returned.</para>
        /// <para>T must be a numeric value type, including <see cref="complex"/> and <see cref="fcomplex"/>.</para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        public static RetT operator *(ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A, ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> B) {
            if (A is BaseArray<double>) {
                return ILNumerics.Core.Functions.Builtin.InnerLoops.MultiplyElem.Double.Instance.operate(
                    A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, 
                    B as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>) as RetT;
            } else if (A is BaseArray<float>) {
                return Core.Functions.Builtin.InnerLoops.MultiplyElem.Single.Instance.operate(
                    A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, 
                    B as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>) as RetT;
#if !NOCOMPLEX
            } else if (A is BaseArray<complex>) {
                return Core.Functions.Builtin.InnerLoops.MultiplyElem.Complex.Instance.operate(
                    A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>,
                    B as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>) as RetT;
            } else if (A is BaseArray<fcomplex>) {
                return Core.Functions.Builtin.InnerLoops.MultiplyElem.FComplex.Instance.operate(
                    A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>,
                    B as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>) as RetT;
#endif
            }

            #if !SATURATING
            if (doSaturate(true)) { // saturating
                if (A is BaseArray<int>) {
                    return Core.Functions.Builtin.InnerLoops.MultiplyElem.Int32Sat.Instance.operate(
                    A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>,
                    B as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>) as RetT;
                } else if (A is BaseArray<uint>) {
                    return Core.Functions.Builtin.InnerLoops.MultiplyElem.UInt32Sat.Instance.operate(
                    A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>,
                    B as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>) as RetT;
                } else if (A is BaseArray<long>) {
                    return Core.Functions.Builtin.InnerLoops.MultiplyElem.Int64Sat.Instance.operate(
                    A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>,
                    B as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>) as RetT;
                } else if (A is BaseArray<ulong>) {
                    return Core.Functions.Builtin.InnerLoops.MultiplyElem.UInt64Sat.Instance.operate(
                    A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>,
                    B as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>) as RetT;
                } else if (A is BaseArray<short>) {
                    return Core.Functions.Builtin.InnerLoops.MultiplyElem.Int16Sat.Instance.operate(
                    A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>,
                    B as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>) as RetT;
                } else if (A is BaseArray<ushort>) {
                    return Core.Functions.Builtin.InnerLoops.MultiplyElem.UInt16Sat.Instance.operate(
                    A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>,
                    B as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>) as RetT;
                } else if (A is BaseArray<sbyte>) {
                    return Core.Functions.Builtin.InnerLoops.MultiplyElem.SByteSat.Instance.operate(
                    A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>,
                    B as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>) as RetT;
                } else if (A is BaseArray<byte>) {
                    return Core.Functions.Builtin.InnerLoops.MultiplyElem.ByteSat.Instance.operate(
                    A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>,
                    B as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>) as RetT;
                }
            } else {
#endif
                // numpy style /not saturating but wrapping around
                if (A is BaseArray<int>) {
                    return Core.Functions.Builtin.InnerLoops.MultiplyElem.Int32.Instance.operate(
                    A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>,
                    B as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>) as RetT;
                } else if (A is BaseArray<uint>) {
                    return Core.Functions.Builtin.InnerLoops.MultiplyElem.UInt32.Instance.operate(
                    A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>,
                    B as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>) as RetT;
                } else if (A is BaseArray<long>) {
                    return Core.Functions.Builtin.InnerLoops.MultiplyElem.Int64.Instance.operate(
                    A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>,
                    B as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>) as RetT;
                } else if (A is BaseArray<ulong>) {
                    return Core.Functions.Builtin.InnerLoops.MultiplyElem.UInt64.Instance.operate(
                    A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>,
                    B as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>) as RetT;
                } else if (A is BaseArray<short>) {
                    return Core.Functions.Builtin.InnerLoops.MultiplyElem.Int16.Instance.operate(
                    A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>,
                    B as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>) as RetT;
                } else if (A is BaseArray<ushort>) {
                    return Core.Functions.Builtin.InnerLoops.MultiplyElem.UInt16.Instance.operate(
                    A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>,
                    B as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>) as RetT;
                } else if (A is BaseArray<sbyte>) {
                    return Core.Functions.Builtin.InnerLoops.MultiplyElem.SByte.Instance.operate(
                    A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>,
                    B as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>) as RetT;
                } else if (A is BaseArray<byte>) {
                    return Core.Functions.Builtin.InnerLoops.MultiplyElem.Byte.Instance.operate(
                    A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>,
                    B as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>) as RetT;
                }
                #if !SATURATING
            }
#endif
            throw new ArgumentException($"The '*' (plus) operator is not defined for arrays of type <{typeof(T1).Name}>."); 
        }

       

        /// <summary>Elementwise subtraction operator on two numeric arrays.</summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The second array.</param>
        /// <returns>Array with result of A - B, with element type T.</returns>
        /// <remarks><para>General notes on binary broadcasting operators: The operation is performed 
        /// elementwise on corresponding elements of <paramref name="A"/> and <paramref name="B"/>. The sizes of both 
        /// input arrays must be broadcastable and determine the size of the resulting array. Note, that 
        /// broadcasting is performed, regardless of the current setting of <see cref="Settings.ArrayStyle"/>. However, 
        /// the <see cref="Settings.ArrayStyle"/> configuration affects the operation in the following ways: 
        /// <list type="bullet">
        /// <item>If <paramref name="A"/> and <paramref name="B"/> have different number of dimensions, the array with 
        /// fewer dimensions is (virtually) padded with singleton dimensions, until the number of dimensions of both arrays match. In 
        /// <see cref="ArrayStyles.ILNumericsV4"/> the singleton dimensions are added to the end of dimensions. For <see cref="ArrayStyles.numpy"/> 
        /// singletons are padded to the start, such that here, broadcasting aligns the <i>last</i> dimensions.</item>
        /// <item><see cref="ArrayStyles.ILNumericsV4"/>: Result values of integer element types T are clamped to the 
        /// natural range of the interger type. I.e.: there will be no 'wrapping around' when exceeding the upper or lower 
        /// limits of the natural value range of the integer type. Also, the operation may be performed by utilizing <see cref="double"/> precision 
        /// floating point arithmetics and subsequently casting the result to the integer value. For <see cref="ArrayStyles.numpy"/> no such precautions exist and the 
        /// result of the binary operation is exactly the same as for operating the elements as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// <para>This function is parallelized for multiple cores. If possible, a suitable <paramref name="A"/> or <paramref name="B"/> is 
        /// directly utilized in order to perform the operation inplace: instead of storing the results into new memory the 
        /// input array and/or its storage is reused and returned.</para>
        /// <para>T must be a numeric value type, including <see cref="complex"/> and <see cref="fcomplex"/>.</para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        public static RetT operator -(ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A, ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> B) {
            if (A is BaseArray<double>) {
                return ILNumerics.Core.Functions.Builtin.InnerLoops.Subtract.Double.Instance.operate(
                    A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>, 
                    B as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>) as RetT;
            } else if (A is BaseArray<float>) {
                return Core.Functions.Builtin.InnerLoops.Subtract.Single.Instance.operate(
                    A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>, 
                    B as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>) as RetT;
#if !NOCOMPLEX
            } else if (A is BaseArray<complex>) {
                return Core.Functions.Builtin.InnerLoops.Subtract.Complex.Instance.operate(
                    A as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>,
                    B as ConcreteArray<complex, Array<complex>, InArray<complex>, OutArray<complex>, Array<complex>, Storage<complex>>) as RetT;
            } else if (A is BaseArray<fcomplex>) {
                return Core.Functions.Builtin.InnerLoops.Subtract.FComplex.Instance.operate(
                    A as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>,
                    B as ConcreteArray<fcomplex, Array<fcomplex>, InArray<fcomplex>, OutArray<fcomplex>, Array<fcomplex>, Storage<fcomplex>>) as RetT;
#endif
            }

            #if !SATURATING
            if (doSaturate(true)) { // saturating
                if (A is BaseArray<int>) {
                    return Core.Functions.Builtin.InnerLoops.Subtract.Int32Sat.Instance.operate(
                    A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>,
                    B as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>) as RetT;
                } else if (A is BaseArray<uint>) {
                    return Core.Functions.Builtin.InnerLoops.Subtract.UInt32Sat.Instance.operate(
                    A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>,
                    B as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>) as RetT;
                } else if (A is BaseArray<long>) {
                    return Core.Functions.Builtin.InnerLoops.Subtract.Int64Sat.Instance.operate(
                    A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>,
                    B as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>) as RetT;
                } else if (A is BaseArray<ulong>) {
                    return Core.Functions.Builtin.InnerLoops.Subtract.UInt64Sat.Instance.operate(
                    A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>,
                    B as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>) as RetT;
                } else if (A is BaseArray<short>) {
                    return Core.Functions.Builtin.InnerLoops.Subtract.Int16Sat.Instance.operate(
                    A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>,
                    B as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>) as RetT;
                } else if (A is BaseArray<ushort>) {
                    return Core.Functions.Builtin.InnerLoops.Subtract.UInt16Sat.Instance.operate(
                    A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>,
                    B as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>) as RetT;
                } else if (A is BaseArray<sbyte>) {
                    return Core.Functions.Builtin.InnerLoops.Subtract.SByteSat.Instance.operate(
                    A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>,
                    B as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>) as RetT;
                } else if (A is BaseArray<byte>) {
                    return Core.Functions.Builtin.InnerLoops.Subtract.ByteSat.Instance.operate(
                    A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>,
                    B as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>) as RetT;
                }
            } else {
#endif
                // numpy style /not saturating but wrapping around
                if (A is BaseArray<int>) {
                    return Core.Functions.Builtin.InnerLoops.Subtract.Int32.Instance.operate(
                    A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>,
                    B as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>) as RetT;
                } else if (A is BaseArray<uint>) {
                    return Core.Functions.Builtin.InnerLoops.Subtract.UInt32.Instance.operate(
                    A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>,
                    B as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>) as RetT;
                } else if (A is BaseArray<long>) {
                    return Core.Functions.Builtin.InnerLoops.Subtract.Int64.Instance.operate(
                    A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>,
                    B as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>) as RetT;
                } else if (A is BaseArray<ulong>) {
                    return Core.Functions.Builtin.InnerLoops.Subtract.UInt64.Instance.operate(
                    A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>,
                    B as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>) as RetT;
                } else if (A is BaseArray<short>) {
                    return Core.Functions.Builtin.InnerLoops.Subtract.Int16.Instance.operate(
                    A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>,
                    B as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>) as RetT;
                } else if (A is BaseArray<ushort>) {
                    return Core.Functions.Builtin.InnerLoops.Subtract.UInt16.Instance.operate(
                    A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>,
                    B as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>) as RetT;
                } else if (A is BaseArray<sbyte>) {
                    return Core.Functions.Builtin.InnerLoops.Subtract.SByte.Instance.operate(
                    A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>,
                    B as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>) as RetT;
                } else if (A is BaseArray<byte>) {
                    return Core.Functions.Builtin.InnerLoops.Subtract.Byte.Instance.operate(
                    A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>,
                    B as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>) as RetT;
                }
                #if !SATURATING
            }
#endif
            throw new ArgumentException($"The '-' (plus) operator is not defined for arrays of type <{typeof(T1).Name}>."); 
        }

#endregion HYCALPER AUTO GENERATED CODE

    }
}
