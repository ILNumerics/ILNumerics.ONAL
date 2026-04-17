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

        /// <summary>
        /// Logical negation operator.
        /// </summary>
        /// <param name="A">The input array.</param>
        /// <returns>Array with result of negating all elements, type <typeref name="T1"/>.</returns>
        /// <remarks><para>The operation is defined on Logical arrays only and corresponds to the ! operator on <see cref="System.Boolean"/>.</para>
        /// <para>If possible, the operation is performed 'inplace' by reusing the input array for the result.</para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        public static RetT operator !(ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A) {
            if (A is BaseArray<bool>) {
                return Core.Functions.Builtin.MathInternal.not(
                A as ConcreteArray<bool, Logical, InLogical, OutLogical, Logical, LogicalStorage>) as RetT;
            }
            throw new ArgumentException($"The logical operator ! is defined for Logical arrays only (not for arrays of <{typeof(T1).Name}>).");
        }

        /// <summary>
        /// Unary, bitwise negation operator.
        /// </summary>
        /// <param name="A">The input array.</param>
        /// <returns>Array with result of negating all elements, type <typeref name="T1"/>.</returns>
        /// <remarks><para>The operation is defined on integer arrays only and corresponds to the ~ operator on <see cref="System.Int32"/>.</para>
        /// <para>If possible, the operation is performed 'inplace' by reusing the input array for the result.</para>
        /// <para>Note, that ~ is not defined on floating point types and not on arrays of element type <see cref="System.UInt64"/>.</para>
        /// </remarks>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        public static RetT operator ~(ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A) {
            if (A is BaseArray<int>) {
                return Core.Functions.Builtin.MathInternal.bitneg(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>) as RetT;
            } else if (A is BaseArray<uint>) {
                return Core.Functions.Builtin.MathInternal.bitneg(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>) as RetT;
            } else if (A is BaseArray<long>) {
                return Core.Functions.Builtin.MathInternal.bitneg(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>) as RetT;
            } else if (A is BaseArray<ulong>) {
                return Core.Functions.Builtin.MathInternal.bitneg(A as ConcreteArray<ulong, Array<ulong>, InArray<ulong>, OutArray<ulong>, Array<ulong>, Storage<ulong>>) as RetT;
            } else if (A is BaseArray<short>) {
                return Core.Functions.Builtin.MathInternal.bitneg(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>) as RetT;
            } else if (A is BaseArray<ushort>) {
                return Core.Functions.Builtin.MathInternal.bitneg(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>) as RetT;
            } else if (A is BaseArray<byte>) {
                return Core.Functions.Builtin.MathInternal.bitneg(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>) as RetT;
            } else if (A is BaseArray<sbyte>) {
                return Core.Functions.Builtin.MathInternal.bitneg(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>) as RetT;
            }
            throw new ArgumentException($"The unary bitwise negation operator ~ is not defined for arrays of <{typeof(T1).Name}>).");
        }

        /// <summary>
        /// Unary, elementwise negation operator.
        /// </summary>
        /// <param name="A">Input array.</param>
        /// <returns>Array of the same size and type as <paramref name="A"/> having its elements negated (as if: multiplying by -1).</returns>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        public static RetT operator -(ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT> A) {
            if (A is BaseArray<int>) {
                return Core.Functions.Builtin.MathInternal.negate(A as ConcreteArray<int, Array<int>, InArray<int>, OutArray<int>, Array<int>, Storage<int>>) as RetT;
            } else if (A is BaseArray<uint>) {
                return Core.Functions.Builtin.MathInternal.negate(A as ConcreteArray<uint, Array<uint>, InArray<uint>, OutArray<uint>, Array<uint>, Storage<uint>>) as RetT;
            } else if (A is BaseArray<long>) {
                return Core.Functions.Builtin.MathInternal.negate(A as ConcreteArray<long, Array<long>, InArray<long>, OutArray<long>, Array<long>, Storage<long>>) as RetT;
            } else if (A is BaseArray<double>) {
                return Core.Functions.Builtin.MathInternal.negate(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>) as RetT;
            } else if (A is BaseArray<float>) {
                return Core.Functions.Builtin.MathInternal.negate(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>) as RetT;
            } else if (A is BaseArray<short>) {
                return Core.Functions.Builtin.MathInternal.negate(A as ConcreteArray<short, Array<short>, InArray<short>, OutArray<short>, Array<short>, Storage<short>>) as RetT;
            } else if (A is BaseArray<ushort>) {
                return Core.Functions.Builtin.MathInternal.negate(A as ConcreteArray<ushort, Array<ushort>, InArray<ushort>, OutArray<ushort>, Array<ushort>, Storage<ushort>>) as RetT;
            } else if (A is BaseArray<byte>) {
                return Core.Functions.Builtin.MathInternal.negate(A as ConcreteArray<byte, Array<byte>, InArray<byte>, OutArray<byte>, Array<byte>, Storage<byte>>) as RetT;
            } else if (A is BaseArray<sbyte>) {
                return Core.Functions.Builtin.MathInternal.negate(A as ConcreteArray<sbyte, Array<sbyte>, InArray<sbyte>, OutArray<sbyte>, Array<sbyte>, Storage<sbyte>>) as RetT;
            }
            throw new ArgumentException($"The unary negation operator - is not defined for arrays of <{typeof(T1).Name}>).");
        }

        /// <summary>
        /// Implicitly convert system scalar <paramref name="A"/> to a scalar array.
        /// </summary>
        /// <param name="A">Scalar value as <see cref="System.ValueType"/>.</param>
        /// <seealso href="https://ilnumerics.net/array-operators_v5.html"/>
        public static implicit operator ConcreteArray<T1, LocalT, InT, OutT, RetT, StorageT>(T1 A) {

            return BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>.Create(A).RetArray;
        }

    }
}
