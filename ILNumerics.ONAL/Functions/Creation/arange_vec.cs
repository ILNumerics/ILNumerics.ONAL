//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
using System;
using ILNumerics;
using ILNumerics.Core.StorageLayer;
using static ILNumerics.Globals;

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {

        #region vec() Obsolete

        /// <summary>
        /// Creates vector of evenly spaced values over a closed interval, elements of <see cref="System.Double"/>.
        /// </summary>
        /// <param name="start">Start value, inclusive.</param>
        /// <param name="end">End value, inclusive.</param>
        /// <returns>Vector with n equally spaced elements from <paramref name="start"/> to <paramref name="end"/>, all with interval 1.</returns>
        /// <remarks>
        /// <para>The last element of the returned vector is less than or equal to <paramref name="end"/>.</para>
        /// <para><see cref="vec(double, double)"/> can not create empty vectors! Use <see cref="empty{T}(long, long, StorageOrders)"/>, <see cref="zeros{T}(long, long, StorageOrders)"/>, <see cref="array{T}(T, long, StorageOrders)"/> or the like to create empty arrays.</para>
        /// <para>This function is provided for compatibility with older versions only. Use <see cref="arange{T, Ts}(T, Ts, T, int?)"/> instead!</para>
        /// </remarks>
        /// <exception cref="ArgumentException"> if <paramref name="start"/><![CDATA[>]]> <paramref name="end"/>.</exception>
        /// <seealso cref="counter{T}(T, T, long, long, StorageOrders)"/>
        /// <seealso cref="arange{T, Ts}(T, Ts, T, int?)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T)"/>
        /// <seealso cref="linspace{T}(InArray{T}, InArray{T}, InArray{T})"/>
        [Obsolete("Use arange() instead!")]
        internal static Array<double> vec(double start, double end) {
            return arange<double,double>(start, 1, end);
        }

        /// <summary>
        /// Creates vector of evenly spaced values of certain step size over a closed interval, elements of <see cref="System.Double"/>.
        /// </summary>
        /// <param name="start">Start value, inclusive.</param>
        /// <param name="end">End value, inclusive.</param>
        /// <param name="step">Step value.</param>
        /// <returns>Vector with equally spaced elements from <paramref name="start"/> to <paramref name="end"/>, all with interval 1.</returns>
        /// <remarks>
        /// <para>The last element of the returned vector is less than or equal to <paramref name="end"/>.</para>
        /// <para><see cref="vec(double, double, double)"/> and <see cref="vec(double, double)"/> can not create 
        /// empty vectors! Use <see cref="empty{T}(long, long, StorageOrders)"/>, 
        /// <see cref="zeros{T}(long, long, StorageOrders)"/>, <see cref="array{T}(T, long, StorageOrders)"/> or 
        /// the similar to create empty arrays.</para>
        /// <para>This function is provided for compatibility with older versions only. Use <see cref="arange{T, Ts}(T, Ts, T, int?)"/> instead!</para>
        /// </remarks>
        /// <exception cref="ArgumentException"> if <paramref name="start"/><![CDATA[>]]> <paramref name="end"/>.</exception>
        /// <seealso cref="counter{T}(T, T, long, long, StorageOrders)"/>
        /// <seealso cref="empty{T}(long, long, long, StorageOrders)"/>
        /// <seealso cref="arange{T, Ts}(T, Ts, T, int?)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T)"/>
        /// <seealso cref="linspace{T}(InArray{T}, InArray{T}, InArray{T})"/>
        [Obsolete("Use arange<T,Ts>(T,TS,T) instead!")]
        internal static Array<double> vec(double start, double step, double end) {
            return arange<double,double>(start, step, end);
        }

        /// <summary>
        /// Creates vector of evenly spaced values over a closed interval, elements of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="start">Start value, inclusive.</param>
        /// <param name="end">End value, inclusive.</param>
        /// <returns>Vector with n equally spaced elements from <paramref name="start"/> to <paramref name="end"/>, all with interval 1.</returns>
        /// <remarks><para>The last element of the returned vector is less than or equal to <paramref name="end"/>.</para>
        /// <para><see cref="vec{T}(T, T)"/> can not create empty vectors! Use <see cref="empty{T}(long, long, StorageOrders)"/>, <see cref="zeros{T}(long, long, StorageOrders)"/>, <see cref="array{T}(T, long, StorageOrders)"/> or the like to create empty arrays.</para>
        /// <para>This function is provided for compatibility with older versions only. Use <see cref="arange{T, Ts}(T, Ts, T, int?)"/> instead!</para>
        /// </remarks>
        /// <exception cref="ArgumentException"> if <paramref name="start"/><![CDATA[>]]> <paramref name="end"/>, 
        /// if <typeparamref name="T"/> is not a supported numeric value type, if <paramref name="start"/> or <paramref name="end"/> is 
        /// not scalar, is null or is not of a supported numeric, convertible element type.</exception>
        /// <seealso cref="counter{T}(T, T, long, long, StorageOrders)"/>
        /// <seealso cref="arange{T, Ts}(T, Ts, T, int?)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T)"/>
        /// <seealso cref="linspace{T}(InArray{T}, InArray{T}, InArray{T})"/>
        [Obsolete("Use arange() instead!")]
        internal static Array<T> vec<T>(T start, T end) where T : IConvertible {
            return arange<T,double>(start, 1, end);
        }

        /// <summary>
        /// Creates vector of N evenly spaced values of given step size over a closed interval, numeric, real elements of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="start">Start value.</param>
        /// <param name="step">Step size.</param>
        /// <param name="end">End value.</param>
        /// <returns>(Column) vector of length N.</returns>
        /// <remarks>
        /// <para>The function creates N values from <paramref name="start"/> to <paramref name="end"/>, all equally spaced with stepsize <paramref name="step"/>.<paramref name="step"/>.</para>
        /// <para>The last element of the returned vector will be less than or equal to <paramref name="end"/>, if <paramref name="start"/>
        /// <![CDATA[<]]> <paramref name="end"/>. </para>
        /// <para>If <paramref name="start"/> <![CDATA[>]]> <paramref name="end"/> elements in the vector will linearly <i>decrease</i> from 
        /// <paramref name="start"/> to <paramref name="end"/>. In this case, <paramref name="step"/> must be negative.</para>
        /// <para>Values are computed in the precision of the datatype <typeparamref name="T"/>. For integer <typeparamref name="T"/> this may imply rounding issues.</para>
        /// <para><see cref="vec{T}(T, T, T)"/> can not create empty vectors! Use <see cref="empty{T}(long, long, StorageOrders)"/>, <see cref="zeros{T}(long, long, StorageOrders)"/>, <see cref="array{T}(T, long, StorageOrders)"/> or the like to create empty arrays.</para>
        /// <para>This function is provided for compatibility with older versions only. Use <see cref="arange{T, Ts}(T, Ts, T, int?)"/> instead!</para>
        /// </remarks>
        /// <exception cref="ArgumentException"> if either of <paramref name="start"/>, <paramref name="end"/>, or
        /// <paramref name="step"/> is null, non-numeric, or non-scalar, if <paramref name="step"/> is 0 or the sign 
        /// of <paramref name="step"/> does not match the sign of 'end - start'.</exception>
        /// <seealso cref="counter{T}(T, T, long, long, StorageOrders)"/>
        /// <seealso cref="arange{T, Ts}(T, Ts, T, int?)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T)"/>
        /// <seealso cref="linspace{T}(InArray{T}, InArray{T}, InArray{T})"/>
        [Obsolete("Use arange<T,TS>(T,TS,T) instead!")]
        internal static Array<T> vec<T>(T start, T step, T end) where T : IConvertible {
            using (Scope.Enter()) {
                double dStart = start.ToDouble(null);
                double dStep = step.ToDouble(null);
                double dEnd = end.ToDouble(null);

                if (dStep == 0)
                    throw new ArgumentException("Step must not be 0");
                if (dStart > dEnd && dStep >= 0)
                    throw new ArgumentException("If start > end, step must be negativ.");
                if (dStart <= dEnd && dStep < 0)
                    throw new ArgumentException("If start <= end, step must be positiv.");
                uint nrElements = (uint)Math.Floor((dEnd - dStart) / dStep) + 1;
                //return counter<double>(0.0, 0.0, nrElements) * dStart + dStart; 

                if (false) {
                    #region HYCALPER LOOPSTART
                    /*!HC:TYPELIST:
<hycalper>
    <type>
        <source locate="here">
            double
        </source>
        <destination>float</destination>
        <destination>sbyte</destination>
        <destination>short</destination>
        <destination>int</destination>
        <destination>long</destination>
    </type>
 </hycalper>
 */
                } else if (Storage<T>.ElementInstance is double) {
                    return (counter<double>(0, 1, nrElements) * (double)dStep + (double)dStart) as Array<T>;
                    #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
                   
                } else if (Storage<T>.ElementInstance is long) {
                    return (counter<long>(0, 1, nrElements) * (long)dStep + (long)dStart) as Array<T>;
                   
                } else if (Storage<T>.ElementInstance is int) {
                    return (counter<int>(0, 1, nrElements) * (int)dStep + (int)dStart) as Array<T>;
                   
                } else if (Storage<T>.ElementInstance is short) {
                    return (counter<short>(0, 1, nrElements) * (short)dStep + (short)dStart) as Array<T>;
                   
                } else if (Storage<T>.ElementInstance is sbyte) {
                    return (counter<sbyte>(0, 1, nrElements) * (sbyte)dStep + (sbyte)dStart) as Array<T>;
                   
                } else if (Storage<T>.ElementInstance is float) {
                    return (counter<float>(0, 1, nrElements) * (float)dStep + (float)dStart) as Array<T>;

#endregion HYCALPER AUTO GENERATED CODE

                } else {
                    throw new ArgumentException($"vec() is not defined for element type '{typeof(T).Name}'. Use a numeric, signed, real type!");
                }
            }
        }

        #endregion

        #region arange

        /// <summary>
        /// Creates vector of evenly spaced values over a closed interval, elements of <see cref="System.Double"/>.
        /// </summary>
        /// <param name="start">Start value, inclusive.</param>
        /// <param name="end">End value, inclusive.</param>
        /// <returns>Vector with n equally spaced elements from <paramref name="start"/> to <paramref name="end"/>, all with interval 1.</returns>
        /// <remarks>
        /// <para>The last element of the returned vector is less than or equal to <paramref name="end"/>.</para>
        /// <para><see cref="arange(double, double)"/> can not create empty vectors! Use <see cref="empty{T}(long, long, StorageOrders)"/>, <see cref="zeros{T}(long, long, StorageOrders)"/>, <see cref="array{T}(T, long, StorageOrders)"/> or the like to create empty arrays.</para>
        /// </remarks>
        /// <exception cref="ArgumentException"> if <paramref name="start"/><![CDATA[>]]> <paramref name="end"/>.</exception>
        /// <seealso cref="counter{T}(T, T, long, long, StorageOrders)"/>
        /// <seealso cref="arange{T, Ts}(T, Ts, T, int?)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T)"/>
        /// <seealso cref="linspace{T}(InArray{T}, InArray{T}, InArray{T})"/>
        internal static Array<double> arange(double start, double end) {
            return arange<double, double>(start, 1, end);
        }
        /// <summary>
        /// Creates vector of evenly spaced values over a closed interval, elements of <see cref="System.Double"/>.
        /// </summary>
        /// <param name="start">Start value, inclusive.</param>
        /// <param name="end">End value, inclusive.</param>
        /// <param name="step">Step value.</param>
        /// <returns>Vector with n equally spaced elements from <paramref name="start"/> to <paramref name="end"/>, all with interval 1.</returns>
        /// <remarks>
        /// <para>The last element of the returned vector is less than or equal to <paramref name="end"/>.</para>
        /// <para><see cref="arange(double, double)"/> can not create empty vectors! Use <see cref="empty{T}(long, long, StorageOrders)"/>, <see cref="zeros{T}(long, long, StorageOrders)"/>, <see cref="array{T}(T, long, StorageOrders)"/> or the like to create empty arrays.</para>
        /// </remarks>
        /// <exception cref="ArgumentException"> if <paramref name="start"/><![CDATA[>]]> <paramref name="end"/>.</exception>
        /// <seealso cref="counter{T}(T, T, long, long, StorageOrders)"/>
        /// <seealso cref="arange{T, Ts}(T, Ts, T, int?)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T)"/>
        /// <seealso cref="linspace{T}(InArray{T}, InArray{T}, InArray{T})"/>
        internal static Array<double> arange(double start, double step, double end) {
            return arange<double, double>(start, step, end);
        }

        /// <summary>
        /// Creates vector of evenly spaced values over a closed interval, elements of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="start">Start value, inclusive.</param>
        /// <param name="end">End value, inclusive.</param>
        /// <param name="axis">[Optional] null: result will be a vector (1 dimension). Positive value: the resulting vector will run along the specified dimension. Default: null.</param>
        /// <returns>Vector with n equally spaced elements from <paramref name="start"/> to <paramref name="end"/>, all with interval 1.</returns>
        /// <remarks><para>The last element of the returned vector is less than or equal to <paramref name="end"/>.</para>
        /// <para><see cref="arange{T}(T, T, int?)"/> can not create empty vectors! Use <see cref="empty{T}(long, long, StorageOrders)"/>, <see cref="zeros{T}(long, long, StorageOrders)"/>, <see cref="array{T}(T, long, StorageOrders)"/> or the like to create empty arrays.</para>
        /// </remarks>
        /// <exception cref="ArgumentException"> if <paramref name="start"/><![CDATA[>]]> <paramref name="end"/>, 
        /// if <typeparamref name="T"/> is not a supported numeric value type, if <paramref name="start"/> or <paramref name="end"/> is 
        /// not scalar, is null or is not of a supported numeric, convertible element type.</exception>
        /// <seealso cref="counter{T}(T, T, long, long, StorageOrders)"/>
        /// <seealso cref="arange{T, Ts}(T, Ts, T, int?)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T)"/>
        /// <seealso cref="linspace{T}(InArray{T}, InArray{T}, InArray{T})"/>
        internal static Array<T> arange<T>(T start, T end, int? axis = null) where T : IConvertible {
            return arange<T,double>(start, 1, end, axis);
        }

        /// <summary>
        /// Creates vector of N evenly spaced values of given step size over a closed interval, numeric, real elements of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="start">Start value.</param>
        /// <param name="step">Step size.</param>
        /// <param name="end">End value.</param>
        /// <param name="axis">[Optional] null: result will be a vector (1 dimension). Positive value: the resulting vector will run along the specified dimension. Default: null.</param>
        /// <returns>(Column) vector of length N.</returns>
        /// <remarks>
        /// <para>The function creates N values from <paramref name="start"/> to <paramref name="end"/>, all equally spaced with stepsize <paramref name="step"/>.<paramref name="step"/>.</para>
        /// <para>The last element of the returned vector will be less than or equal to <paramref name="end"/>, if <paramref name="start"/>
        /// <![CDATA[<]]> <paramref name="end"/>. </para>
        /// <para>If <paramref name="start"/> <![CDATA[>]]> <paramref name="end"/> elements in the vector will linearly <i>decrease</i> from 
        /// <paramref name="start"/> to <paramref name="end"/>. In this case, <paramref name="step"/> must be negative.</para>
        /// <para>Values are computed in the precision of the datatype <typeparamref name="T"/>. For integer <typeparamref name="T"/> this may imply rounding issues.</para>
        /// <para><see cref="arange{T, Ts}(T, Ts, T, int?)"/> can not create empty vectors! Use <see cref="empty{T}(long, long, StorageOrders)"/>, <see cref="zeros{T}(long, long, StorageOrders)"/>, <see cref="array{T}(T, long, StorageOrders)"/> or the like to create empty arrays.</para>
        /// </remarks>
        /// <exception cref="ArgumentException"> if either of <paramref name="start"/>, <paramref name="end"/>, or
        /// <paramref name="step"/> is null, non-numeric, or non-scalar, if <paramref name="step"/> is 0 or the sign 
        /// of <paramref name="step"/> does not match the sign of 'end - start'. If <paramref name="axis"/> has a negative value.</exception>
        /// <seealso cref="counter{T}(T, T, long, long, StorageOrders)"/>
        /// <seealso cref="arange{T, Ts}(T, Ts, T, int?)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T)"/>
        /// <seealso cref="linspace{T}(InArray{T}, InArray{T}, InArray{T})"/>
        internal static Array<T> arange<T, Ts>(T start, Ts step, T end, int? axis = null) where T : IConvertible where Ts : IConvertible {
            using (Scope.Enter()) {
                double dStart = start.ToDouble(null);
                double dStep = step.ToDouble(null);
                double dEnd = end.ToDouble(null);

                if (dStep == 0)
                    throw new ArgumentException("Step must not be 0");
                if (dStart > dEnd && dStep >= 0)
                    throw new ArgumentException("If start > end, step must be negativ.");
                if (dStart < dEnd && dStep < 0)
                    throw new ArgumentException("If start <= end, step must be positiv.");
                uint nrElements = (uint)Math.Floor((dEnd - dStart) / dStep) + 1;
                Array<long> size = 1; 
                
                if (axis.HasValue) {
                    var ax = Math.Abs(axis.GetValueOrDefault());  
                    if (ax >= Size.MaxNumberOfDimensions) {
                        throw new ArgumentException($"Absolute value of parameter 'axis' must be in the range [0 ... ILNumerics.Size.MaxNumberOfDimensions). Found: {axis}."); 
                    }
                    size = ones<long>(ax + 1, 1);
                    if (axis >= 0) {
                        size[Globals.end] = nrElements;
                    } else {
                        size[0] = nrElements; 
                    }
                } else {
                    size.a = nrElements; 
                }

                if (false) {
                    #region HYCALPER LOOPSTART
                    /*!HC:TYPELIST:
<hycalper>
    <type>
        <source locate="here">
            double
        </source>
        <destination>float</destination>
        <destination>sbyte</destination>
        <destination>short</destination>
        <destination>int</destination>
        <destination>long</destination>
    </type>
 </hycalper>
 */
                } else if (Storage<T>.ElementInstance is double) {
                    return (counter<double>(0, 1, size) * (double)dStep + (double)dStart) as Array<T>;
                    #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
                   
                } else if (Storage<T>.ElementInstance is long) {
                    return (counter<long>(0, 1, size) * (long)dStep + (long)dStart) as Array<T>;
                   
                } else if (Storage<T>.ElementInstance is int) {
                    return (counter<int>(0, 1, size) * (int)dStep + (int)dStart) as Array<T>;
                   
                } else if (Storage<T>.ElementInstance is short) {
                    return (counter<short>(0, 1, size) * (short)dStep + (short)dStart) as Array<T>;
                   
                } else if (Storage<T>.ElementInstance is sbyte) {
                    return (counter<sbyte>(0, 1, size) * (sbyte)dStep + (sbyte)dStart) as Array<T>;
                   
                } else if (Storage<T>.ElementInstance is float) {
                    return (counter<float>(0, 1, size) * (float)dStep + (float)dStart) as Array<T>;

#endregion HYCALPER AUTO GENERATED CODE

                } else {
                    throw new ArgumentException($"arange() is not defined for element type '{typeof(T).Name}'. Use a numeric, signed, real type!");
                }
            }
        }

        #endregion
    }
}
