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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static ILNumerics.Core.Functions.Builtin.MathInternal; 

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

        #region HYCALPER LOOPSTART BINARY_OPERATOR_TEMPLATE

        /*!HC:TYPELIST:
        <hycalper>
        <type>
            <source locate="here">
                BitAnd
            </source>
            <destination>BitOr</destination>
            <destination>BitXor</destination>
        </type>
        <type>
            <source locate="here">
                And.Bool
            </source>
            <destination>Or.Bool</destination>
            <destination>Xor.Bool</destination>
        </type>
        <type>
            <source locate="comment">
                summary
            </source>
            <destination>Elementwise bitwise 'or' operator on two integer arrays.</destination>
            <destination>Elementwise bitwise 'xor' operator on two integer arrays.</destination>
        </type>
        <type>
            <source locate="comment">
                returns
            </source>
            <destination>Array with result of bitwise A or B.</destination>
            <destination>Array with result of bitwise A xor B.</destination>
        </type>
        <type>
            <source locate="here">
                <![CDATA[&]]>
            </source>
            <destination>|</destination>
            <destination>^</destination>
        </type>
        </hycalper>
        */

        /// <summary>
        /// Binary, elementwise, bitwise/logical 'and' operator.
        /// </summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The second array.</param>
        /// <returns>Array with result of A &amp; B, with element type <typeref name="T1"/>.</returns>
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
        /// result of the binary operation is exactly the same as for operating the elements (<see href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/checked-and-unchecked">unchecked</see>) as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// <para>This function is parallelized for multiple cores. If possible, a suitable <paramref name="A"/> or <paramref name="B"/> is 
        /// directly utilized in order to perform the operation inplace: instead of storing the results into new memory the 
        /// input array and/or its storage is reused and returned.</para>
        /// <para>T must be a numeric value type, including <see cref="complex"/> and <see cref="fcomplex"/>.</para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        public static RetT operator &(ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A, ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> B) {
            if (A is BaseArray<int>) {
                return Core.Functions.Builtin.InnerLoops.BitAnd.Int32.Instance.operate(
                A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>,
                B as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>) as RetT;
            } else if (A is BaseArray<uint>) {
                return Core.Functions.Builtin.InnerLoops.BitAnd.UInt32.Instance.operate(
                A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>,
                B as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>) as RetT;
            } else if (A is BaseArray<long>) {
                return Core.Functions.Builtin.InnerLoops.BitAnd.Int64.Instance.operate(
                A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>,
                B as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>) as RetT;
            } else if (A is BaseArray<ulong>) {
                return Core.Functions.Builtin.InnerLoops.BitAnd.UInt64.Instance.operate(
                A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>,
                B as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>) as RetT;
            } else if (A is BaseArray<bool>) {
                return Core.Functions.Builtin.InnerLoops.And.Bool.Instance.operate(
                A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>,
                B as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>) as RetT;
            } else if (A is BaseArray<short>) {
                return Core.Functions.Builtin.InnerLoops.BitAnd.Int16.Instance.operate(
                A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>,
                B as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>) as RetT;
            } else if (A is BaseArray<ushort>) {
                return Core.Functions.Builtin.InnerLoops.BitAnd.UInt16.Instance.operate(
                A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>,
                B as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>) as RetT;
            } else if (A is BaseArray<sbyte>) {
                return Core.Functions.Builtin.InnerLoops.BitAnd.SByte.Instance.operate(
                A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>,
                B as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>) as RetT;
            } else if (A is BaseArray<byte>) {
                return Core.Functions.Builtin.InnerLoops.BitAnd.Byte.Instance.operate(
                A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>,
                B as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>) as RetT;
            }
            throw new ArgumentException($"The & operator is not defined for arrays of type <{typeof(T1).Name}>.");
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 

       

        /// <summary>Elementwise bitwise 'xor' operator on two integer arrays.</summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The second array.</param>
        /// <returns>Array with result of bitwise A xor B.</returns>
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
        /// result of the binary operation is exactly the same as for operating the elements (<see href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/checked-and-unchecked">unchecked</see>) as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// <para>This function is parallelized for multiple cores. If possible, a suitable <paramref name="A"/> or <paramref name="B"/> is 
        /// directly utilized in order to perform the operation inplace: instead of storing the results into new memory the 
        /// input array and/or its storage is reused and returned.</para>
        /// <para>T must be a numeric value type, including <see cref="complex"/> and <see cref="fcomplex"/>.</para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        public static RetT operator ^(ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A, ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> B) {
            if (A is BaseArray<int>) {
                return Core.Functions.Builtin.InnerLoops.BitXor.Int32.Instance.operate(
                A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>,
                B as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>) as RetT;
            } else if (A is BaseArray<uint>) {
                return Core.Functions.Builtin.InnerLoops.BitXor.UInt32.Instance.operate(
                A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>,
                B as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>) as RetT;
            } else if (A is BaseArray<long>) {
                return Core.Functions.Builtin.InnerLoops.BitXor.Int64.Instance.operate(
                A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>,
                B as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>) as RetT;
            } else if (A is BaseArray<ulong>) {
                return Core.Functions.Builtin.InnerLoops.BitXor.UInt64.Instance.operate(
                A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>,
                B as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>) as RetT;
            } else if (A is BaseArray<bool>) {
                return Core.Functions.Builtin.InnerLoops.Xor.Bool.Instance.operate(
                A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>,
                B as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>) as RetT;
            } else if (A is BaseArray<short>) {
                return Core.Functions.Builtin.InnerLoops.BitXor.Int16.Instance.operate(
                A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>,
                B as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>) as RetT;
            } else if (A is BaseArray<ushort>) {
                return Core.Functions.Builtin.InnerLoops.BitXor.UInt16.Instance.operate(
                A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>,
                B as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>) as RetT;
            } else if (A is BaseArray<sbyte>) {
                return Core.Functions.Builtin.InnerLoops.BitXor.SByte.Instance.operate(
                A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>,
                B as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>) as RetT;
            } else if (A is BaseArray<byte>) {
                return Core.Functions.Builtin.InnerLoops.BitXor.Byte.Instance.operate(
                A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>,
                B as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>) as RetT;
            }
            throw new ArgumentException($"The ^ operator is not defined for arrays of type <{typeof(T1).Name}>.");
        }

       

        /// <summary>Elementwise bitwise 'or' operator on two integer arrays.</summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The second array.</param>
        /// <returns>Array with result of bitwise A or B.</returns>
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
        /// result of the binary operation is exactly the same as for operating the elements (<see href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/checked-and-unchecked">unchecked</see>) as System types (and as in numpy itself).</item>
        /// </list>
        /// </para>
        /// <para>This function is parallelized for multiple cores. If possible, a suitable <paramref name="A"/> or <paramref name="B"/> is 
        /// directly utilized in order to perform the operation inplace: instead of storing the results into new memory the 
        /// input array and/or its storage is reused and returned.</para>
        /// <para>T must be a numeric value type, including <see cref="complex"/> and <see cref="fcomplex"/>.</para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        public static RetT operator |(ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A, ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> B) {
            if (A is BaseArray<int>) {
                return Core.Functions.Builtin.InnerLoops.BitOr.Int32.Instance.operate(
                A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>,
                B as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>) as RetT;
            } else if (A is BaseArray<uint>) {
                return Core.Functions.Builtin.InnerLoops.BitOr.UInt32.Instance.operate(
                A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>,
                B as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>) as RetT;
            } else if (A is BaseArray<long>) {
                return Core.Functions.Builtin.InnerLoops.BitOr.Int64.Instance.operate(
                A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>,
                B as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>) as RetT;
            } else if (A is BaseArray<ulong>) {
                return Core.Functions.Builtin.InnerLoops.BitOr.UInt64.Instance.operate(
                A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>,
                B as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>) as RetT;
            } else if (A is BaseArray<bool>) {
                return Core.Functions.Builtin.InnerLoops.Or.Bool.Instance.operate(
                A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>,
                B as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>) as RetT;
            } else if (A is BaseArray<short>) {
                return Core.Functions.Builtin.InnerLoops.BitOr.Int16.Instance.operate(
                A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>,
                B as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>) as RetT;
            } else if (A is BaseArray<ushort>) {
                return Core.Functions.Builtin.InnerLoops.BitOr.UInt16.Instance.operate(
                A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>,
                B as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>) as RetT;
            } else if (A is BaseArray<sbyte>) {
                return Core.Functions.Builtin.InnerLoops.BitOr.SByte.Instance.operate(
                A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>,
                B as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>) as RetT;
            } else if (A is BaseArray<byte>) {
                return Core.Functions.Builtin.InnerLoops.BitOr.Byte.Instance.operate(
                A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>,
                B as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>) as RetT;
            }
            throw new ArgumentException($"The | operator is not defined for arrays of type <{typeof(T1).Name}>.");
        }

#endregion HYCALPER AUTO GENERATED CODE
        
        #region Left Shift / Right Shift
        /// <summary>Elementwise bitwise '<![CDATA[<<]]>' operator on two integer arrays.</summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The second array.</param>
        /// <returns>Array with result of bit shifting elements of A by B.</returns>
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
        public static RetT operator <<(ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A, int B) {
            if (A is BaseArray<int>) {
                return Core.Functions.Builtin.InnerLoops.BitShiftLeft.Int32.Instance.operate(
                A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>,
                Storage<int>.Create(B).RetArray) as RetT;
            } else if (A is BaseArray<uint>) {
                return Core.Functions.Builtin.InnerLoops.BitShiftLeft.UInt32.Instance.operate(
                A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>,
                Storage<uint>.Create((uint)B).RetArray) as RetT;
            } else if (A is BaseArray<long>) {
                return Core.Functions.Builtin.InnerLoops.BitShiftLeft.Int64.Instance.operate(
                A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>,
                Storage<long>.Create((long)B).RetArray) as RetT;
            } else if (A is BaseArray<ulong>) {
                return Core.Functions.Builtin.InnerLoops.BitShiftLeft.UInt64.Instance.operate(
                A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>,
                Storage<ulong>.Create((ulong)B).RetArray) as RetT;
            } else if (A is BaseArray<short>) {
                return Core.Functions.Builtin.InnerLoops.BitShiftLeft.Int16.Instance.operate(
                A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>,
                Storage<short>.Create((short)B).RetArray) as RetT;
            } else if (A is BaseArray<ushort>) {
                return Core.Functions.Builtin.InnerLoops.BitShiftLeft.UInt16.Instance.operate(
                A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>,
                Storage<ushort>.Create((ushort)B).RetArray) as RetT;
            } else if (A is BaseArray<sbyte>) {
                return Core.Functions.Builtin.InnerLoops.BitShiftLeft.SByte.Instance.operate(
                A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>,
                Storage<sbyte>.Create((sbyte)B).RetArray) as RetT;
            } else if (A is BaseArray<byte>) {
                return Core.Functions.Builtin.InnerLoops.BitShiftLeft.Byte.Instance.operate(
                A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>,
                Storage<byte>.Create((byte)B).RetArray) as RetT;
            }
            throw new ArgumentException($"The << operator is not defined for arrays of type <{typeof(T1).Name}>.");
        }
        /// <summary>Elementwise bitwise '<![CDATA[>>]]>' operator on two integer arrays.</summary>
        /// <param name="A">The first array.</param>
        /// <param name="B">The second array.</param>
        /// <returns>Array with result of bit shifting elements of A by B.</returns>
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
        public static RetT operator >>(ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A, int B) {
            if (A is BaseArray<int>) {
                return Core.Functions.Builtin.InnerLoops.BitShiftRight.Int32.Instance.operate(
                A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>,
                Storage<int>.Create(B).RetArray) as RetT;
            } else if (A is BaseArray<uint>) {
                return Core.Functions.Builtin.InnerLoops.BitShiftRight.UInt32.Instance.operate(
                A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>,
                Storage<uint>.Create((uint)B).RetArray) as RetT;
            } else if (A is BaseArray<long>) {
                return Core.Functions.Builtin.InnerLoops.BitShiftRight.Int64.Instance.operate(
                A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>,
                Storage<long>.Create((long)B).RetArray) as RetT;
            } else if (A is BaseArray<ulong>) {
                return Core.Functions.Builtin.InnerLoops.BitShiftRight.UInt64.Instance.operate(
                A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>,
                Storage<ulong>.Create((ulong)B).RetArray) as RetT;
            } else if (A is BaseArray<short>) {
                return Core.Functions.Builtin.InnerLoops.BitShiftRight.Int16.Instance.operate(
                A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>,
                Storage<short>.Create((short)B).RetArray) as RetT;
            } else if (A is BaseArray<ushort>) {
                return Core.Functions.Builtin.InnerLoops.BitShiftRight.UInt16.Instance.operate(
                A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>,
                Storage<ushort>.Create((ushort)B).RetArray) as RetT;
            } else if (A is BaseArray<sbyte>) {
                return Core.Functions.Builtin.InnerLoops.BitShiftRight.SByte.Instance.operate(
                A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>,
                Storage<sbyte>.Create((sbyte)B).RetArray) as RetT;
            } else if (A is BaseArray<byte>) {
                return Core.Functions.Builtin.InnerLoops.BitShiftRight.Byte.Instance.operate(
                A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>,
                Storage<byte>.Create((byte)B).RetArray) as RetT;
            }
            throw new ArgumentException($"The >> operator is not defined for arrays of type <{typeof(T1).Name}>.");
        }
        #endregion

    }
}
