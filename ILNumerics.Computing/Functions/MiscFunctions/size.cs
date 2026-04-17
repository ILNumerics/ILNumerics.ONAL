using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {

        /// <summary>
        /// Provides empty dimension lengths for operations expecting a size argument.
        /// </summary>
        /// <remarks><para>This function is an alias for <c><![CDATA[empty<long>(0)]]></c> and returns a vector 
        /// of 0 <see cref="long"/> elements.</para></remarks>
        internal static Array<long> size() {
            return empty<long>(0);
        }
        /// <summary>
        /// Provides dimension lengths for operations expecting a size argument. Targets 1 dimension.
        /// </summary>
        /// <remarks><para>This function is an alias for <see cref="vector{T}(T)"/> and returns a vector 
        /// of <see cref="long"/> elements.</para></remarks>
        internal static Array<long> size(long d0) {
            return vector<long>(d0);
        }
        /// <summary>
        /// Provides dimension lengths for operations expecting a size argument. Targets 2 dimensions.
        /// </summary>
        /// <remarks><para>This function is an alias for <see cref="vector{T}(T,T)"/> and returns a vector 
        /// of <see cref="long"/> elements.</para></remarks>
        internal static Array<long> size(long d0, long d1) {
            return vector<long>(d0, d1);
        }
        /// <summary>
        /// Provides dimension lengths for operations expecting a size argument. Targets 3 dimensions.
        /// </summary>
        /// <remarks><para>This function is an alias for <see cref="vector{T}(T,T,T)"/> and returns a vector 
        /// of <see cref="long"/> elements.</para></remarks>
        internal static Array<long> size(long d0, long d1, long d2) {
            return vector<long>(d0, d1, d2);
        }
        /// <summary>
        /// Provides dimension lengths for operations expecting a size argument. Targets 4 dimensions.
        /// </summary>
        /// <remarks><para>This function is an alias for <see cref="vector{T}(T,T,T,T)"/> and returns a vector 
        /// of <see cref="long"/> elements.</para></remarks>
        internal static Array<long> size(long d0, long d1, long d2, long d3) {
            return vector<long>(d0, d1, d2, d3);
        }
        /// <summary>
        /// Provides dimension lengths for operations expecting a size argument. Targets 5 dimensions.
        /// </summary>
        /// <remarks><para>This function is an alias for <see cref="vector{T}(T,T,T,T,T)"/> and returns a vector 
        /// of <see cref="long"/> elements.</para></remarks>
        internal static Array<long> size(long d0, long d1, long d2, long d3, long d4) {
            return vector<long>(d0, d1, d2, d3, d4);
        }
        /// <summary>
        /// Provides dimension lengths for operations expecting a size argument. Targets 6 dimensions.
        /// </summary>
        /// <remarks><para>This function is an alias for <see cref="vector{T}(T,T,T,T,T,T)"/> and returns a vector 
        /// of <see cref="long"/> elements.</para></remarks>
        internal static Array<long> size(long d0, long d1, long d2, long d3, long d4, long d5) {
            return vector<long>(d0, d1, d2, d3, d4, d5);
        }
        /// <summary>
        /// Provides dimension lengths for operations expecting a size argument. Targets 7 dimensions.
        /// </summary>
        /// <remarks><para>This function is an alias for <see cref="vector{T}(T,T,T,T,T,T,T)"/> and returns a vector 
        /// of <see cref="long"/> elements.</para></remarks>
        internal static Array<long> size(long d0, long d1, long d2, long d3, long d4, long d5, long d6) {
            return vector<long>(d0, d1, d2, d3, d4, d5, d6);
        }

        /// <summary>
        /// Provides dimension lengths for operations expecting a size argument. Targets more than 7 dimensions.
        /// </summary>
        /// <remarks><para>This function is an alias for <see cref="vector{T}(T[])"/> and returns a vector 
        /// of <see cref="long"/> elements.</para>
        /// <para>For situations requiring high-performance execution you may consider using <see cref="vector{T}(T[])"/> 
        /// in order to create the required size vector once. Keep this size array around and reuse it. 
        /// This prevents one from allocating additional, GC managed memory from the managed heap and may helps 
        /// to increase the performance / to decrease GC activity. Otherwise, the 'params' (variable length argument) 
        /// may puts pressure on the GC when used frequently in tight loops.</para>
        /// </remarks>
        internal static Array<long> size(params long[] dimensions) {
            using (Scope.Enter()) {
                if (Equals(dimensions, null)) {
                    throw new ArgumentNullException("The 'dimensions' argument cannot be null.");
                }
                Array<long> ret = vector<long>(dimensions);
                if (anyall(ret < 0)) {
                    throw new ArgumentException("All elements of the 'dimensions' argument must be non-negative.");
                }
                return ret; 
            }
        }
    }
}
