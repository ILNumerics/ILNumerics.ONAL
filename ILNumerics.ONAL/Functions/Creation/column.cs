using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ILNumerics.Globals; 

namespace ILNumerics.Core.Functions.Builtin {

    internal static partial class MathInternal {
          
        /// <summary>
        /// Creates a row vector. This function is deprecated. Use <see cref="vector{T}(T[])"/> and combine with <see cref="reshape{T}(BaseArray{T}, InArray{long}, StorageOrders?)"/> instead.
        /// </summary>
        /// <typeparam name="T">Element type</typeparam>
        /// <param name="elements">Elements of the row vector.</param>
        /// <returns>New row vector.</returns>
        /// <remarks><para>The same effect is achieved by: <![CDATA[<c>vector<T>(elements).Reshape(1, Equals(elements, null) ? 0 : elements.Length)</c>]]>.</para></remarks>
        [Obsolete("Use vector(1,2,3).Reshape(1,3) instead! In ArrayStyle ILNumericsV4 vector(1,2,3).T is an easier alternative.")]
        internal static Array<T> row<T>(params T[] elements) {

            return vector<T>(elements).Reshape(1, Equals(elements,null) ? 0 : elements.Length);
        }
        /// <summary>
        /// Creates a column vector. This function is deprecated. Use <see cref="vector{T}(T[])"/> instead.
        /// </summary>
        /// <typeparam name="T">Element type</typeparam>
        /// <param name="elements">Elements of the column vector.</param>
        /// <returns>New column vector.</returns>
        /// <remarks><para>The same effect is achieved by: <![CDATA[<c>vector<T>(elements).Reshape(Equals(elements, null) ? 0 : elements.Length, 1)</c>]]>.</para></remarks>
        [Obsolete("Use vector<T>(T[]) instead!")]
        internal static Array<T> column<T>(params T[] elements) {

            return vector<T>(elements).Reshape(Equals(elements, null) ? 0 : elements.Length, 1);

        }


    }

}
