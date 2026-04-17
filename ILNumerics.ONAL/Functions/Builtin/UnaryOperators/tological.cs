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


    internal static partial class MathInternal {

        /// <summary>
        /// Creates a logical array with 'true' values at the positions with non-0 elements of <paramref name="A"/>.
        /// </summary>
        /// <param name="A">Source array.</param>
        /// <returns>New array.</returns>
        internal static Logical tological(BaseArray A) {
            if (object.Equals(A, null) || A is Logical) {
                return A?.ToLogical(); // safe the null check 
            } else if (A is BaseArray<float>) {
                return neq(A as BaseArray<float>, 0);
            } else if (A is BaseArray<long>) {
                return neq(A as BaseArray<long>, 0);
            } else if (A is BaseArray<ulong>) {
                return neq(A as BaseArray<ulong>, 0);
            } else if (A is BaseArray<int>) {
                return neq(A as BaseArray<int>, 0);
            } else if (A is BaseArray<uint>) {
                return neq(A as BaseArray<uint>, 0);
            } else if (A is BaseArray<short>) {
                return neq(A as BaseArray<short>, 0);
            } else if (A is BaseArray<ushort>) {
                return neq(A as BaseArray<ushort>, 0);
            } else if (A is BaseArray<sbyte>) {
                return neq(A as BaseArray<sbyte>, 0);
            } else if (A is BaseArray<double>) {
                return neq(A as BaseArray<double>, 0);
            } else if (A is BaseArray<complex>) {
                return neq(A as BaseArray<complex>, complex.Zero); 
            } else if (A is BaseArray<fcomplex>) {
                return neq(A as BaseArray<fcomplex>, fcomplex.Zero);
            } else if (A is BaseArray<bool>) {
                return neq(A as BaseArray<bool>, true);
            } else {
                throw new InvalidCastException($"Unable to convert BaseArray of type {A.GetType().Name} to Array<byte>."); 
            }
        }

    }

}
