
using System; 
using System.Globalization;
using System.IO;
using System.Text;
using System.Drawing; 
using System.Collections.Generic;
using ILNumerics; 
using ILNumerics.Core.Functions.Builtin; 
using ILNumerics.Core.MemoryLayer; 
using ILNumerics.Core.Arrays; 
using ILNumerics.Core.StorageLayer; 
using ILNumerics.Core.Misc;
using static ILNumerics.Globals;
using static ILNumerics.Core.Functions.Builtin.numpyInternal;


// THIS FILE WAS CREATED BY AN AUTOMATED TOOL: CEngPublicAPICreator.
// MANUAL CHANGES TO THIS FILE WILL BE SILENTLY DISCARDED WITH THE NEXT BUILD!
// 
// DO NOT CHANGE THIS FILE MANUALLY!!
// 
// In order to change information of this file, instead update the 
// corresponding information from ILNumerics.Core.Functions.Builtin.numpyInternal 
// from the corresponding module and rerun the CEngPublicAPICreator tool! 
// 
// DO NOT CHANGE THIS FILE MANUALLY !!
// 
namespace ILNumerics {
    public static partial class numpy {
public static Logical all<IndT>(BaseArray<UInt64> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.all<IndT>(A, axes, keepdims);}
public static Logical all<IndT>(BaseArray<Int64> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.all<IndT>(A, axes, keepdims);}
public static Logical all<IndT>(BaseArray<UInt32> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.all<IndT>(A, axes, keepdims);}
public static Logical all<IndT>(BaseArray<Int32> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.all<IndT>(A, axes, keepdims);}
public static Logical all<IndT>(BaseArray<UInt16> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.all<IndT>(A, axes, keepdims);}
public static Logical all<IndT>(BaseArray<Int16> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.all<IndT>(A, axes, keepdims);}
public static Logical all<IndT>(BaseArray<Byte> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.all<IndT>(A, axes, keepdims);}
public static Logical all<IndT>(BaseArray<SByte> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.all<IndT>(A, axes, keepdims);}
public static Logical all<IndT>(BaseArray<fcomplex> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.all<IndT>(A, axes, keepdims);}
public static Logical all<IndT>(BaseArray<complex> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.all<IndT>(A, axes, keepdims);}
public static Logical all<IndT>(BaseArray<Single> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.all<IndT>(A, axes, keepdims);}
public static Logical all<IndT>(BaseArray<Double> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.all<IndT>(A, axes, keepdims);}
public static Logical any<IndT>(BaseArray<UInt64> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.any<IndT>(A, axes, keepdims);}
public static Logical any<IndT>(BaseArray<Int64> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.any<IndT>(A, axes, keepdims);}
public static Logical any<IndT>(BaseArray<UInt32> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.any<IndT>(A, axes, keepdims);}
public static Logical any<IndT>(BaseArray<Int32> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.any<IndT>(A, axes, keepdims);}
public static Logical any<IndT>(BaseArray<UInt16> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.any<IndT>(A, axes, keepdims);}
public static Logical any<IndT>(BaseArray<Int16> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.any<IndT>(A, axes, keepdims);}
public static Logical any<IndT>(BaseArray<Byte> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.any<IndT>(A, axes, keepdims);}
public static Logical any<IndT>(BaseArray<SByte> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.any<IndT>(A, axes, keepdims);}
public static Logical any<IndT>(BaseArray<fcomplex> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.any<IndT>(A, axes, keepdims);}
public static Logical any<IndT>(BaseArray<complex> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.any<IndT>(A, axes, keepdims);}
public static Logical any<IndT>(BaseArray<Single> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.any<IndT>(A, axes, keepdims);}
public static Logical any<IndT>(BaseArray<Double> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.any<IndT>(A, axes, keepdims);}
public static Array<Double> cumprod<IndT>(BaseArray<Double> A, Nullable<IndT> axis = null) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.cumprod<IndT>(A, axis);}
public static Array<UInt64> cumprod<IndT>(BaseArray<UInt64> A, Nullable<IndT> axis = null) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.cumprod<IndT>(A, axis);}
public static Array<Int64> cumprod<IndT>(BaseArray<Int64> A, Nullable<IndT> axis = null) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.cumprod<IndT>(A, axis);}
public static Array<UInt32> cumprod<IndT>(BaseArray<UInt32> A, Nullable<IndT> axis = null) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.cumprod<IndT>(A, axis);}
public static Array<Int32> cumprod<IndT>(BaseArray<Int32> A, Nullable<IndT> axis = null) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.cumprod<IndT>(A, axis);}
public static Array<UInt16> cumprod<IndT>(BaseArray<UInt16> A, Nullable<IndT> axis = null) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.cumprod<IndT>(A, axis);}
public static Array<Int16> cumprod<IndT>(BaseArray<Int16> A, Nullable<IndT> axis = null) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.cumprod<IndT>(A, axis);}
public static Array<Byte> cumprod<IndT>(BaseArray<Byte> A, Nullable<IndT> axis = null) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.cumprod<IndT>(A, axis);}
public static Array<SByte> cumprod<IndT>(BaseArray<SByte> A, Nullable<IndT> axis = null) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.cumprod<IndT>(A, axis);}
public static Array<fcomplex> cumprod<IndT>(BaseArray<fcomplex> A, Nullable<IndT> axis = null) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.cumprod<IndT>(A, axis);}
public static Array<complex> cumprod<IndT>(BaseArray<complex> A, Nullable<IndT> axis = null) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.cumprod<IndT>(A, axis);}
public static Array<Single> cumprod<IndT>(BaseArray<Single> A, Nullable<IndT> axis = null) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.cumprod<IndT>(A, axis);}
public static Array<Double> cumsum<IndT>(BaseArray<Double> A, Nullable<IndT> axis = null) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.cumsum<IndT>(A, axis);}
public static Array<UInt64> cumsum<IndT>(BaseArray<UInt64> A, Nullable<IndT> axis = null) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.cumsum<IndT>(A, axis);}
public static Array<Int64> cumsum<IndT>(BaseArray<Int64> A, Nullable<IndT> axis = null) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.cumsum<IndT>(A, axis);}
public static Array<UInt32> cumsum<IndT>(BaseArray<UInt32> A, Nullable<IndT> axis = null) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.cumsum<IndT>(A, axis);}
public static Array<Int32> cumsum<IndT>(BaseArray<Int32> A, Nullable<IndT> axis = null) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.cumsum<IndT>(A, axis);}
public static Array<UInt16> cumsum<IndT>(BaseArray<UInt16> A, Nullable<IndT> axis = null) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.cumsum<IndT>(A, axis);}
public static Array<Int16> cumsum<IndT>(BaseArray<Int16> A, Nullable<IndT> axis = null) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.cumsum<IndT>(A, axis);}
public static Array<Byte> cumsum<IndT>(BaseArray<Byte> A, Nullable<IndT> axis = null) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.cumsum<IndT>(A, axis);}
public static Array<SByte> cumsum<IndT>(BaseArray<SByte> A, Nullable<IndT> axis = null) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.cumsum<IndT>(A, axis);}
public static Array<fcomplex> cumsum<IndT>(BaseArray<fcomplex> A, Nullable<IndT> axis = null) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.cumsum<IndT>(A, axis);}
public static Array<complex> cumsum<IndT>(BaseArray<complex> A, Nullable<IndT> axis = null) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.cumsum<IndT>(A, axis);}
public static Array<Single> cumsum<IndT>(BaseArray<Single> A, Nullable<IndT> axis = null) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.cumsum<IndT>(A, axis);}
/// <summary>
/// Computes the maximum of elements of <paramref name="A" /> along the specified dimension.
/// </summary>
/// <param name="A">Input array.</param>
/// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
/// <param name="dim">The index of the dimension to be reduced.</param>
/// <param name="I">[Optional] Output: 0-based index values of the found elements along the working dimension. Default: (null) don't return the indices.</param>
/// <returns>New array with the same shape as <paramref name="A" /> except that the
/// dimension specified by <paramref name="dim" /> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim" /> is false.</returns>
/// <remarks><para>Empty arrays are handled in the same way as other shapes: the dimension specified by <paramref name="dim" /> is reduced to 1. If A.S[dim] == 0 the
/// <paramref name="dim" />s dimension length in the array returned will also be 0.</para><para>Scalar arrays: numpy scalars give a numpy scalar (0 dim) as output.</para><para>Special floating point values (for floating point element types): positive and negative infinity are handled in the regular way. NaN ('not a number') values are ignored. If
/// all elements in a working dimension are NaN, NaN is returned as result in the corresponding output element.</para><para>If the optional output parameter <paramref name="I" /> is not null on entry the function computes and returns the indices in <paramref name="A" /> of the
/// values returned. Thus, <paramref name="I" /> has the same shape as the return array. If the storage of <paramref name="I" /> on entry is sufficient
/// (i.e.: large enough and not shared with other arrays) it will be directly used for storing the indices and I is
/// reshaped as required. Otherwise, a new array is created and returned. In order to safe the index computation
/// null can be provided as <paramref name="I" /> which is the default.</para><para>If <paramref name="I" /> is requested and multiple elements along the working dimension in <paramref name="A" /> have the same maximum value it is undefined which
/// element is 'picked' for the output. Hence, in this case the corresponding value in <paramref name="I" /> is undefined and may point to the first or any other occurrence of the
/// value returned from the working dimension.</para></remarks>
/// <exception cref="T:System.ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim" /> was false.</exception>
public static Array<Double> max(BaseArray<Double> A, OutArray<Int64> I = null, Int32 dim = -1, Boolean keepdim = true) { return ILNumerics.Core.Functions.Builtin.numpyInternal.max(A, I, dim, keepdim);}
/// <summary>
/// Computes the maximum of elements of <paramref name="A" /> along the specified dimension.
/// </summary>
/// <param name="A">Input array.</param>
/// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
/// <param name="dim">The index of the dimension to be reduced.</param>
/// <param name="I">[Optional] Output: 0-based index values of the found elements along the working dimension. Default: (null) don't return the indices.</param>
/// <returns>New array with the same shape as <paramref name="A" /> except that the
/// dimension specified by <paramref name="dim" /> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim" /> is false.</returns>
/// <remarks><para>Empty arrays are handled in the same way as other shapes: the dimension specified by <paramref name="dim" /> is reduced to 1. If A.S[dim] == 0 the
/// <paramref name="dim" />s dimension length in the array returned will also be 0.</para><para>Scalar arrays: numpy scalars give a numpy scalar (0 dim) as output.</para><para>Special floating point values (for floating point element types): positive and negative infinity are handled in the regular way. NaN ('not a number') values are ignored. If
/// all elements in a working dimension are NaN, NaN is returned as result in the corresponding output element.</para><para>If the optional output parameter <paramref name="I" /> is not null on entry the function computes and returns the indices in <paramref name="A" /> of the
/// values returned. Thus, <paramref name="I" /> has the same shape as the return array. If the storage of <paramref name="I" /> on entry is sufficient
/// (i.e.: large enough and not shared with other arrays) it will be directly used for storing the indices and I is
/// reshaped as required. Otherwise, a new array is created and returned. In order to safe the index computation
/// null can be provided as <paramref name="I" /> which is the default.</para><para>If <paramref name="I" /> is requested and multiple elements along the working dimension in <paramref name="A" /> have the same maximum value it is undefined which
/// element is 'picked' for the output. Hence, in this case the corresponding value in <paramref name="I" /> is undefined and may point to the first or any other occurrence of the
/// value returned from the working dimension.</para></remarks>
/// <exception cref="T:System.ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim" /> was false.</exception>
public static Array<fcomplex> max(BaseArray<fcomplex> A, OutArray<Int64> I = null, Int32 dim = -1, Boolean keepdim = true) { return ILNumerics.Core.Functions.Builtin.numpyInternal.max(A, I, dim, keepdim);}
/// <summary>
/// Computes the maximum of elements of <paramref name="A" /> along the specified dimension.
/// </summary>
/// <param name="A">Input array.</param>
/// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
/// <param name="dim">The index of the dimension to be reduced.</param>
/// <param name="I">[Optional] Output: 0-based index values of the found elements along the working dimension. Default: (null) don't return the indices.</param>
/// <returns>New array with the same shape as <paramref name="A" /> except that the
/// dimension specified by <paramref name="dim" /> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim" /> is false.</returns>
/// <remarks><para>Empty arrays are handled in the same way as other shapes: the dimension specified by <paramref name="dim" /> is reduced to 1. If A.S[dim] == 0 the
/// <paramref name="dim" />s dimension length in the array returned will also be 0.</para><para>Scalar arrays: numpy scalars give a numpy scalar (0 dim) as output.</para><para>Special floating point values (for floating point element types): positive and negative infinity are handled in the regular way. NaN ('not a number') values are ignored. If
/// all elements in a working dimension are NaN, NaN is returned as result in the corresponding output element.</para><para>If the optional output parameter <paramref name="I" /> is not null on entry the function computes and returns the indices in <paramref name="A" /> of the
/// values returned. Thus, <paramref name="I" /> has the same shape as the return array. If the storage of <paramref name="I" /> on entry is sufficient
/// (i.e.: large enough and not shared with other arrays) it will be directly used for storing the indices and I is
/// reshaped as required. Otherwise, a new array is created and returned. In order to safe the index computation
/// null can be provided as <paramref name="I" /> which is the default.</para><para>If <paramref name="I" /> is requested and multiple elements along the working dimension in <paramref name="A" /> have the same maximum value it is undefined which
/// element is 'picked' for the output. Hence, in this case the corresponding value in <paramref name="I" /> is undefined and may point to the first or any other occurrence of the
/// value returned from the working dimension.</para></remarks>
/// <exception cref="T:System.ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim" /> was false.</exception>
public static Array<complex> max(BaseArray<complex> A, OutArray<Int64> I = null, Int32 dim = -1, Boolean keepdim = true) { return ILNumerics.Core.Functions.Builtin.numpyInternal.max(A, I, dim, keepdim);}
/// <summary>
/// Computes the maximum of elements of <paramref name="A" /> along the specified dimension.
/// </summary>
/// <param name="A">Input array.</param>
/// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
/// <param name="dim">The index of the dimension to be reduced.</param>
/// <param name="I">[Optional] Output: 0-based index values of the found elements along the working dimension. Default: (null) don't return the indices.</param>
/// <returns>New array with the same shape as <paramref name="A" /> except that the
/// dimension specified by <paramref name="dim" /> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim" /> is false.</returns>
/// <remarks><para>Empty arrays are handled in the same way as other shapes: the dimension specified by <paramref name="dim" /> is reduced to 1. If A.S[dim] == 0 the
/// <paramref name="dim" />s dimension length in the array returned will also be 0.</para><para>Scalar arrays: numpy scalars give a numpy scalar (0 dim) as output.</para><para>Special floating point values (for floating point element types): positive and negative infinity are handled in the regular way. NaN ('not a number') values are ignored. If
/// all elements in a working dimension are NaN, NaN is returned as result in the corresponding output element.</para><para>If the optional output parameter <paramref name="I" /> is not null on entry the function computes and returns the indices in <paramref name="A" /> of the
/// values returned. Thus, <paramref name="I" /> has the same shape as the return array. If the storage of <paramref name="I" /> on entry is sufficient
/// (i.e.: large enough and not shared with other arrays) it will be directly used for storing the indices and I is
/// reshaped as required. Otherwise, a new array is created and returned. In order to safe the index computation
/// null can be provided as <paramref name="I" /> which is the default.</para><para>If <paramref name="I" /> is requested and multiple elements along the working dimension in <paramref name="A" /> have the same maximum value it is undefined which
/// element is 'picked' for the output. Hence, in this case the corresponding value in <paramref name="I" /> is undefined and may point to the first or any other occurrence of the
/// value returned from the working dimension.</para></remarks>
/// <exception cref="T:System.ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim" /> was false.</exception>
public static Array<Single> max(BaseArray<Single> A, OutArray<Int64> I = null, Int32 dim = -1, Boolean keepdim = true) { return ILNumerics.Core.Functions.Builtin.numpyInternal.max(A, I, dim, keepdim);}
/// <summary>
/// Gets a new array with the mean of elements of <paramref name="A" /> along dimensions specified by <paramref name="axes" />.
/// </summary>
/// <typeparam name="AxesT">the (integer) type for the dimension indices.</typeparam>
/// <param name="A">Input array.</param>
/// <param name="axes">Dimensions of <paramref name="A" /> to compute the mean for.</param>
/// <param name="keepdims"></param>
/// <returns>New array with the dimensions <paramref name="axes" /> reduced to the mean of corresponding elements in <paramref name="A" />.</returns>
/// <remarks>Accumulation is internally performed in uint precision. On empty array <paramref name="A" /> NaN is returned.</remarks>
/// <exception cref="T:System.ArgumentNullException"> when <paramref name="A" /> was null on entry.</exception>
/// <exception cref="T:System.ArgumentException">when <paramref name="axes" /> contains a value outside of the dimensions of <paramref name="A" />.</exception>
public static Array<Double> mean<AxesT>(BaseArray<UInt32> A, BaseArray<AxesT> axes = null, Boolean keepdims = false) where AxesT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.mean<AxesT>(A, axes, keepdims);}
/// <summary>
/// Gets a new array with the mean of elements of <paramref name="A" /> along dimensions specified by <paramref name="axes" />.
/// </summary>
/// <typeparam name="AxesT">the (integer) type for the dimension indices.</typeparam>
/// <param name="A">Input array.</param>
/// <param name="axes">Dimensions of <paramref name="A" /> to compute the mean for.</param>
/// <param name="keepdims"></param>
/// <returns>New array with the dimensions <paramref name="axes" /> reduced to the mean of corresponding elements in <paramref name="A" />.</returns>
/// <remarks>Accumulation is internally performed in int precision. On empty array <paramref name="A" /> NaN is returned.</remarks>
/// <exception cref="T:System.ArgumentNullException"> when <paramref name="A" /> was null on entry.</exception>
/// <exception cref="T:System.ArgumentException">when <paramref name="axes" /> contains a value outside of the dimensions of <paramref name="A" />.</exception>
public static Array<Double> mean<AxesT>(BaseArray<Int32> A, BaseArray<AxesT> axes = null, Boolean keepdims = false) where AxesT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.mean<AxesT>(A, axes, keepdims);}
/// <summary>
/// Gets a new array with the mean of elements of <paramref name="A" /> along dimensions specified by <paramref name="axes" />.
/// </summary>
/// <typeparam name="AxesT">the (integer) type for the dimension indices.</typeparam>
/// <param name="A">Input array.</param>
/// <param name="axes">Dimensions of <paramref name="A" /> to compute the mean for.</param>
/// <param name="keepdims"></param>
/// <returns>New array with the dimensions <paramref name="axes" /> reduced to the mean of corresponding elements in <paramref name="A" />.</returns>
/// <remarks>Accumulation is internally performed in ushort precision. On empty array <paramref name="A" /> NaN is returned.</remarks>
/// <exception cref="T:System.ArgumentNullException"> when <paramref name="A" /> was null on entry.</exception>
/// <exception cref="T:System.ArgumentException">when <paramref name="axes" /> contains a value outside of the dimensions of <paramref name="A" />.</exception>
public static Array<Double> mean<AxesT>(BaseArray<UInt16> A, BaseArray<AxesT> axes = null, Boolean keepdims = false) where AxesT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.mean<AxesT>(A, axes, keepdims);}
/// <summary>
/// Gets a new array with the mean of elements of <paramref name="A" /> along dimensions specified by <paramref name="axes" />.
/// </summary>
/// <typeparam name="AxesT">the (integer) type for the dimension indices.</typeparam>
/// <param name="A">Input array.</param>
/// <param name="axes">Dimensions of <paramref name="A" /> to compute the mean for.</param>
/// <param name="keepdims"></param>
/// <returns>New array with the dimensions <paramref name="axes" /> reduced to the mean of corresponding elements in <paramref name="A" />.</returns>
/// <remarks>Accumulation is internally performed in short precision. On empty array <paramref name="A" /> NaN is returned.</remarks>
/// <exception cref="T:System.ArgumentNullException"> when <paramref name="A" /> was null on entry.</exception>
/// <exception cref="T:System.ArgumentException">when <paramref name="axes" /> contains a value outside of the dimensions of <paramref name="A" />.</exception>
public static Array<Double> mean<AxesT>(BaseArray<Int16> A, BaseArray<AxesT> axes = null, Boolean keepdims = false) where AxesT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.mean<AxesT>(A, axes, keepdims);}
/// <summary>
/// Gets a new array with the mean of elements of <paramref name="A" /> along dimensions specified by <paramref name="axes" />.
/// </summary>
/// <typeparam name="AxesT">the (integer) type for the dimension indices.</typeparam>
/// <param name="A">Input array.</param>
/// <param name="axes">Dimensions of <paramref name="A" /> to compute the mean for.</param>
/// <param name="keepdims"></param>
/// <returns>New array with the dimensions <paramref name="axes" /> reduced to the mean of corresponding elements in <paramref name="A" />.</returns>
/// <remarks>Accumulation is internally performed in byte precision. On empty array <paramref name="A" /> NaN is returned.</remarks>
/// <exception cref="T:System.ArgumentNullException"> when <paramref name="A" /> was null on entry.</exception>
/// <exception cref="T:System.ArgumentException">when <paramref name="axes" /> contains a value outside of the dimensions of <paramref name="A" />.</exception>
public static Array<Double> mean<AxesT>(BaseArray<Byte> A, BaseArray<AxesT> axes = null, Boolean keepdims = false) where AxesT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.mean<AxesT>(A, axes, keepdims);}
/// <summary>
/// Gets a new array with the mean of elements of <paramref name="A" /> along dimensions specified by <paramref name="axes" />.
/// </summary>
/// <typeparam name="AxesT">the (integer) type for the dimension indices.</typeparam>
/// <param name="A">Input array.</param>
/// <param name="axes">Dimensions of <paramref name="A" /> to compute the mean for.</param>
/// <param name="keepdims"></param>
/// <returns>New array with the dimensions <paramref name="axes" /> reduced to the mean of corresponding elements in <paramref name="A" />.</returns>
/// <remarks>Accumulation is internally performed in sbyte precision. On empty array <paramref name="A" /> NaN is returned.</remarks>
/// <exception cref="T:System.ArgumentNullException"> when <paramref name="A" /> was null on entry.</exception>
/// <exception cref="T:System.ArgumentException">when <paramref name="axes" /> contains a value outside of the dimensions of <paramref name="A" />.</exception>
public static Array<Double> mean<AxesT>(BaseArray<SByte> A, BaseArray<AxesT> axes = null, Boolean keepdims = false) where AxesT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.mean<AxesT>(A, axes, keepdims);}
/// <summary>
/// Gets a new array with the mean of elements of <paramref name="A" /> along dimensions specified by <paramref name="axes" />.
/// </summary>
/// <typeparam name="AxesT">the (integer) type for the dimension indices.</typeparam>
/// <param name="A">Input array.</param>
/// <param name="axes">Dimensions of <paramref name="A" /> to compute the mean for.</param>
/// <param name="keepdims"></param>
/// <returns>New array with the dimensions <paramref name="axes" /> reduced to the mean of corresponding elements in <paramref name="A" />.</returns>
/// <remarks>Accumulation is internally performed in fcomplex precision. On empty array <paramref name="A" /> NaN is returned.</remarks>
/// <exception cref="T:System.ArgumentNullException"> when <paramref name="A" /> was null on entry.</exception>
/// <exception cref="T:System.ArgumentException">when <paramref name="axes" /> contains a value outside of the dimensions of <paramref name="A" />.</exception>
public static Array<fcomplex> mean<AxesT>(BaseArray<fcomplex> A, BaseArray<AxesT> axes = null, Boolean keepdims = false) where AxesT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.mean<AxesT>(A, axes, keepdims);}
/// <summary>
/// Gets a new array with the mean of elements of <paramref name="A" /> along dimensions specified by <paramref name="axes" />.
/// </summary>
/// <typeparam name="AxesT">the (integer) type for the dimension indices.</typeparam>
/// <param name="A">Input array.</param>
/// <param name="axes">Dimensions of <paramref name="A" /> to compute the mean for.</param>
/// <param name="keepdims"></param>
/// <returns>New array with the dimensions <paramref name="axes" /> reduced to the mean of corresponding elements in <paramref name="A" />.</returns>
/// <remarks>Accumulation is internally performed in complex precision. On empty array <paramref name="A" /> NaN is returned.</remarks>
/// <exception cref="T:System.ArgumentNullException"> when <paramref name="A" /> was null on entry.</exception>
/// <exception cref="T:System.ArgumentException">when <paramref name="axes" /> contains a value outside of the dimensions of <paramref name="A" />.</exception>
public static Array<complex> mean<AxesT>(BaseArray<complex> A, BaseArray<AxesT> axes = null, Boolean keepdims = false) where AxesT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.mean<AxesT>(A, axes, keepdims);}
/// <summary>
/// Gets a new array with the mean of elements of <paramref name="A" /> along dimensions specified by <paramref name="axes" />.
/// </summary>
/// <typeparam name="AxesT">the (integer) type for the dimension indices.</typeparam>
/// <param name="A">Input array.</param>
/// <param name="axes">Dimensions of <paramref name="A" /> to compute the mean for.</param>
/// <param name="keepdims"></param>
/// <returns>New array with the dimensions <paramref name="axes" /> reduced to the mean of corresponding elements in <paramref name="A" />.</returns>
/// <remarks>Accumulation is internally performed in float precision. On empty array <paramref name="A" /> NaN is returned.</remarks>
/// <exception cref="T:System.ArgumentNullException"> when <paramref name="A" /> was null on entry.</exception>
/// <exception cref="T:System.ArgumentException">when <paramref name="axes" /> contains a value outside of the dimensions of <paramref name="A" />.</exception>
public static Array<Single> mean<AxesT>(BaseArray<Single> A, BaseArray<AxesT> axes = null, Boolean keepdims = false) where AxesT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.mean<AxesT>(A, axes, keepdims);}
/// <summary>
/// Gets a new array with the mean of elements of <paramref name="A" /> along dimensions specified by <paramref name="axes" />.
/// </summary>
/// <typeparam name="AxesT">the (integer) type for the dimension indices.</typeparam>
/// <param name="A">Input array.</param>
/// <param name="axes">Dimensions of <paramref name="A" /> to compute the mean for.</param>
/// <param name="keepdims"></param>
/// <returns>New array with the dimensions <paramref name="axes" /> reduced to the mean of corresponding elements in <paramref name="A" />.</returns>
/// <remarks>Accumulation is internally performed in double precision. On empty array <paramref name="A" /> NaN is returned.</remarks>
/// <exception cref="T:System.ArgumentNullException"> when <paramref name="A" /> was null on entry.</exception>
/// <exception cref="T:System.ArgumentException">when <paramref name="axes" /> contains a value outside of the dimensions of <paramref name="A" />.</exception>
public static Array<Double> mean<AxesT>(BaseArray<Double> A, BaseArray<AxesT> axes = null, Boolean keepdims = false) where AxesT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.mean<AxesT>(A, axes, keepdims);}
/// <summary>
/// Gets a new array with the mean of elements of <paramref name="A" /> along dimensions specified by <paramref name="axes" />.
/// </summary>
/// <typeparam name="AxesT">the (integer) type for the dimension indices.</typeparam>
/// <param name="A">Input array.</param>
/// <param name="axes">Dimensions of <paramref name="A" /> to compute the mean for.</param>
/// <param name="keepdims"></param>
/// <returns>New array with the dimensions <paramref name="axes" /> reduced to the mean of corresponding elements in <paramref name="A" />.</returns>
/// <remarks>Accumulation is internally performed in ulong precision. On empty array <paramref name="A" /> NaN is returned.</remarks>
/// <exception cref="T:System.ArgumentNullException"> when <paramref name="A" /> was null on entry.</exception>
/// <exception cref="T:System.ArgumentException">when <paramref name="axes" /> contains a value outside of the dimensions of <paramref name="A" />.</exception>
public static Array<Double> mean<AxesT>(BaseArray<UInt64> A, BaseArray<AxesT> axes = null, Boolean keepdims = false) where AxesT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.mean<AxesT>(A, axes, keepdims);}
/// <summary>
/// Gets a new array with the mean of elements of <paramref name="A" /> along dimensions specified by <paramref name="axes" />.
/// </summary>
/// <typeparam name="AxesT">the (integer) type for the dimension indices.</typeparam>
/// <param name="A">Input array.</param>
/// <param name="axes">Dimensions of <paramref name="A" /> to compute the mean for.</param>
/// <param name="keepdims"></param>
/// <returns>New array with the dimensions <paramref name="axes" /> reduced to the mean of corresponding elements in <paramref name="A" />.</returns>
/// <remarks>Accumulation is internally performed in long precision. On empty array <paramref name="A" /> NaN is returned.</remarks>
/// <exception cref="T:System.ArgumentNullException"> when <paramref name="A" /> was null on entry.</exception>
/// <exception cref="T:System.ArgumentException">when <paramref name="axes" /> contains a value outside of the dimensions of <paramref name="A" />.</exception>
public static Array<Double> mean<AxesT>(BaseArray<Int64> A, BaseArray<AxesT> axes = null, Boolean keepdims = false) where AxesT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.mean<AxesT>(A, axes, keepdims);}
/// <summary>
/// Computes the minimum of elements of <paramref name="A" /> along the specified dimension.
/// </summary>
/// <param name="A">Input array.</param>
/// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
/// <param name="dim">The index of the dimension to be reduced.</param>
/// <param name="I">[Optional] Output: 0-based index values of the found elements along the working dimension. Default: (null) don't return the indices.</param>
/// <returns>New array with the same shape as <paramref name="A" /> except that the
/// dimension specified by <paramref name="dim" /> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim" /> is false.</returns>
/// <remarks><para>Empty arrays are handled in the same way as other shapes: the dimension specified by <paramref name="dim" /> is reduced to 1. If A.S[dim] == 0 the
/// <paramref name="dim" />s dimension length in the array returned will also be 0.</para><para>Scalar arrays: numpy scalars give a numpy scalar (0 dim) as output.</para><para>Special floating point values (for floating point element types): positive and negative infinity are handled in the regular way. NaN ('not a number') values are ignored. If
/// all elements in a working dimension are NaN, NaN is returned as result in the corresponding output element.</para><para>If the optional output parameter <paramref name="I" /> is not null on entry the function computes and returns the indices in <paramref name="A" /> of the
/// values returned. Thus, <paramref name="I" /> has the same shape as the return array. If the storage of <paramref name="I" /> on entry is sufficient
/// (i.e.: large enough and not shared with other arrays) it will be directly used for storing the indices and I is
/// reshaped as required. Otherwise, a new array is created and returned. In order to safe the index computation
/// null can be provided as <paramref name="I" /> which is the default.</para><para>If <paramref name="I" /> is requested and multiple elements along the working dimension in <paramref name="A" /> have the same minimum value it is undefined which
/// element is 'picked' for the output. Hence, in this case the corresponding value in <paramref name="I" /> is undefined and may point to the first or any other occurrence of the
/// value returned from the working dimension.</para></remarks>
/// <exception cref="T:System.ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim" /> was false.</exception>
public static Array<Double> min(BaseArray<Double> A, OutArray<Int64> I = null, Int32 dim = -1, Boolean keepdim = true) { return ILNumerics.Core.Functions.Builtin.numpyInternal.min(A, I, dim, keepdim);}
/// <summary>
/// Computes the minimum of elements of <paramref name="A" /> along the specified dimension.
/// </summary>
/// <param name="A">Input array.</param>
/// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
/// <param name="dim">The index of the dimension to be reduced.</param>
/// <param name="I">[Optional] Output: 0-based index values of the found elements along the working dimension. Default: (null) don't return the indices.</param>
/// <returns>New array with the same shape as <paramref name="A" /> except that the
/// dimension specified by <paramref name="dim" /> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim" /> is false.</returns>
/// <remarks><para>Empty arrays are handled in the same way as other shapes: the dimension specified by <paramref name="dim" /> is reduced to 1. If A.S[dim] == 0 the
/// <paramref name="dim" />s dimension length in the array returned will also be 0.</para><para>Scalar arrays: numpy scalars give a numpy scalar (0 dim) as output.</para><para>Special floating point values (for floating point element types): positive and negative infinity are handled in the regular way. NaN ('not a number') values are ignored. If
/// all elements in a working dimension are NaN, NaN is returned as result in the corresponding output element.</para><para>If the optional output parameter <paramref name="I" /> is not null on entry the function computes and returns the indices in <paramref name="A" /> of the
/// values returned. Thus, <paramref name="I" /> has the same shape as the return array. If the storage of <paramref name="I" /> on entry is sufficient
/// (i.e.: large enough and not shared with other arrays) it will be directly used for storing the indices and I is
/// reshaped as required. Otherwise, a new array is created and returned. In order to safe the index computation
/// null can be provided as <paramref name="I" /> which is the default.</para><para>If <paramref name="I" /> is requested and multiple elements along the working dimension in <paramref name="A" /> have the same minimum value it is undefined which
/// element is 'picked' for the output. Hence, in this case the corresponding value in <paramref name="I" /> is undefined and may point to the first or any other occurrence of the
/// value returned from the working dimension.</para></remarks>
/// <exception cref="T:System.ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim" /> was false.</exception>
public static Array<fcomplex> min(BaseArray<fcomplex> A, OutArray<Int64> I = null, Int32 dim = -1, Boolean keepdim = true) { return ILNumerics.Core.Functions.Builtin.numpyInternal.min(A, I, dim, keepdim);}
/// <summary>
/// Computes the minimum of elements of <paramref name="A" /> along the specified dimension.
/// </summary>
/// <param name="A">Input array.</param>
/// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
/// <param name="dim">The index of the dimension to be reduced.</param>
/// <param name="I">[Optional] Output: 0-based index values of the found elements along the working dimension. Default: (null) don't return the indices.</param>
/// <returns>New array with the same shape as <paramref name="A" /> except that the
/// dimension specified by <paramref name="dim" /> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim" /> is false.</returns>
/// <remarks><para>Empty arrays are handled in the same way as other shapes: the dimension specified by <paramref name="dim" /> is reduced to 1. If A.S[dim] == 0 the
/// <paramref name="dim" />s dimension length in the array returned will also be 0.</para><para>Scalar arrays: numpy scalars give a numpy scalar (0 dim) as output.</para><para>Special floating point values (for floating point element types): positive and negative infinity are handled in the regular way. NaN ('not a number') values are ignored. If
/// all elements in a working dimension are NaN, NaN is returned as result in the corresponding output element.</para><para>If the optional output parameter <paramref name="I" /> is not null on entry the function computes and returns the indices in <paramref name="A" /> of the
/// values returned. Thus, <paramref name="I" /> has the same shape as the return array. If the storage of <paramref name="I" /> on entry is sufficient
/// (i.e.: large enough and not shared with other arrays) it will be directly used for storing the indices and I is
/// reshaped as required. Otherwise, a new array is created and returned. In order to safe the index computation
/// null can be provided as <paramref name="I" /> which is the default.</para><para>If <paramref name="I" /> is requested and multiple elements along the working dimension in <paramref name="A" /> have the same minimum value it is undefined which
/// element is 'picked' for the output. Hence, in this case the corresponding value in <paramref name="I" /> is undefined and may point to the first or any other occurrence of the
/// value returned from the working dimension.</para></remarks>
/// <exception cref="T:System.ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim" /> was false.</exception>
public static Array<complex> min(BaseArray<complex> A, OutArray<Int64> I = null, Int32 dim = -1, Boolean keepdim = true) { return ILNumerics.Core.Functions.Builtin.numpyInternal.min(A, I, dim, keepdim);}
/// <summary>
/// Computes the minimum of elements of <paramref name="A" /> along the specified dimension.
/// </summary>
/// <param name="A">Input array.</param>
/// <param name="keepdim">[Optional] True: reduced singleton dimension is not removed from the output (default). False: the new singleton dimension is removed.</param>
/// <param name="dim">The index of the dimension to be reduced.</param>
/// <param name="I">[Optional] Output: 0-based index values of the found elements along the working dimension. Default: (null) don't return the indices.</param>
/// <returns>New array with the same shape as <paramref name="A" /> except that the
/// dimension specified by <paramref name="dim" /> is reduced to length min(1,A.S[dim]) and removed if <paramref name="keepdim" /> is false.</returns>
/// <remarks><para>Empty arrays are handled in the same way as other shapes: the dimension specified by <paramref name="dim" /> is reduced to 1. If A.S[dim] == 0 the
/// <paramref name="dim" />s dimension length in the array returned will also be 0.</para><para>Scalar arrays: numpy scalars give a numpy scalar (0 dim) as output.</para><para>Special floating point values (for floating point element types): positive and negative infinity are handled in the regular way. NaN ('not a number') values are ignored. If
/// all elements in a working dimension are NaN, NaN is returned as result in the corresponding output element.</para><para>If the optional output parameter <paramref name="I" /> is not null on entry the function computes and returns the indices in <paramref name="A" /> of the
/// values returned. Thus, <paramref name="I" /> has the same shape as the return array. If the storage of <paramref name="I" /> on entry is sufficient
/// (i.e.: large enough and not shared with other arrays) it will be directly used for storing the indices and I is
/// reshaped as required. Otherwise, a new array is created and returned. In order to safe the index computation
/// null can be provided as <paramref name="I" /> which is the default.</para><para>If <paramref name="I" /> is requested and multiple elements along the working dimension in <paramref name="A" /> have the same minimum value it is undefined which
/// element is 'picked' for the output. Hence, in this case the corresponding value in <paramref name="I" /> is undefined and may point to the first or any other occurrence of the
/// value returned from the working dimension.</para></remarks>
/// <exception cref="T:System.ArgumentException">If <c>A.S[dim] == 0</c> and <paramref name="keepdim" /> was false.</exception>
public static Array<Single> min(BaseArray<Single> A, OutArray<Int64> I = null, Int32 dim = -1, Boolean keepdim = true) { return ILNumerics.Core.Functions.Builtin.numpyInternal.min(A, I, dim, keepdim);}
public static Array<UInt64> prod<IndT>(BaseArray<UInt64> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.prod<IndT>(A, axes, keepdims);}
public static Array<Int64> prod<IndT>(BaseArray<Int64> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.prod<IndT>(A, axes, keepdims);}
public static Array<UInt32> prod<IndT>(BaseArray<UInt32> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.prod<IndT>(A, axes, keepdims);}
public static Array<Int32> prod<IndT>(BaseArray<Int32> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.prod<IndT>(A, axes, keepdims);}
public static Array<UInt16> prod<IndT>(BaseArray<UInt16> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.prod<IndT>(A, axes, keepdims);}
public static Array<Int16> prod<IndT>(BaseArray<Int16> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.prod<IndT>(A, axes, keepdims);}
public static Array<Byte> prod<IndT>(BaseArray<Byte> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.prod<IndT>(A, axes, keepdims);}
public static Array<SByte> prod<IndT>(BaseArray<SByte> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.prod<IndT>(A, axes, keepdims);}
public static Array<fcomplex> prod<IndT>(BaseArray<fcomplex> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.prod<IndT>(A, axes, keepdims);}
public static Array<complex> prod<IndT>(BaseArray<complex> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.prod<IndT>(A, axes, keepdims);}
public static Array<Single> prod<IndT>(BaseArray<Single> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.prod<IndT>(A, axes, keepdims);}
public static Array<Double> prod<IndT>(BaseArray<Double> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.prod<IndT>(A, axes, keepdims);}
/// <summary>
/// Replaces elements of <paramref name="A" /> with <paramref name="values" /> at positions given by sequential (i.e.: flatten, row-major) <paramref name="indices" />.
/// </summary>
/// <typeparam name="T1">Element type of <paramref name="A" />.</typeparam>
/// <typeparam name="LocalT">(subtype of <paramref name="A" />)</typeparam>
/// <typeparam name="InT">(subtype of <paramref name="A" />)</typeparam>
/// <typeparam name="OutT">(subtype of <paramref name="A" />)</typeparam>
/// <typeparam name="RetT">(subtype of <paramref name="A" />)</typeparam>
/// <typeparam name="StorageT">(subtype of <paramref name="A" />)</typeparam>
/// <typeparam name="IndT">Element type of indices. Must be numeric.</typeparam>
/// <param name="A">The array storing the elements to be replaced.</param>
/// <param name="indices">Index array. The shape is ignored. Values must be numeric and are read in row-major order.</param>
/// <param name="values">Values array. The shape is ignored. Values are read in row-major order.</param>
/// <param name="mode">[Optionl] Specifies how to handle index values in <paramref name="indices" /> which are out-of-range. Default: error.</param>
/// <remarks><para>This function has a similar effect as doing <c>A.flat[indices] = values</c> in numpy. However, in ILNumerics the iterator
/// returned from <c>A.flat</c> is read-only. Use <see cref="M:ILNumerics.Core.Functions.Builtin.numpyInternal.put``7(ILNumerics.Core.Arrays.Mutable{``0,``1,``2,``3,``4,``5},ILNumerics.BaseArray{``6},ILNumerics.InArray{``0},ILNumerics.PutModes)" /> as
/// a replacement.</para><para>Note that the values in <paramref name="indices" /> are considered <i>sequential</i> indices, i.e. they correspond to the
/// element position in a flattened array, where the flattening is performed in row-major order. For performance reasons and if
/// <paramref name="A" />.<see cref="P:ILNumerics.Size.StorageOrder" /> is not <see cref="F:ILNumerics.StorageOrders.RowMajor" /> A is converted to row-major
/// storage layout first and remains in row-major storage after the function returns. Note further, that the conversion happens
/// proactively, and is not rolled back in case of errors during the iteration.</para><para>Elements of <paramref name="indices" /> can be negative, which corresponds to indexing from the end of flattened <paramref name="A" />.</para><para>Repeated values in <paramref name="indices" /> lead to only the last corresponding value in <paramref name="values" /> to be stored
/// at the respective position in <paramref name="A" />.</para><para>If <paramref name="values" /> has more elements than are indices provided in <paramref name="indices" /> superfluent values are ignored. No exception
/// is thrown in this case. If <paramref name="values" /> has fewer elements than indices are provided in <paramref name="indices" /> existing values
/// are repeated as necessary.</para><para>The <paramref name="mode" /> parameter determines what happens with indices laying outside of the bounds of A. The default value
/// of <see cref="F:ILNumerics.PutModes.Raise" /> throws an <see cref="T:System.IndexOutOfRangeException" /> in this case. Two other options exist which bring the
/// index back into the valid range: <see cref="F:ILNumerics.PutModes.Wrap" /> computes the modulus, <see cref="F:ILNumerics.PutModes.Clip" /> limits the indices to the allowed range.
/// Note that negative indices behave as usual (counting from the end of A) for modes <see cref="F:ILNumerics.PutModes.Raise" /> and <see cref="F:ILNumerics.PutModes.Wrap" /> only.</para><para><see cref="M:ILNumerics.Core.Functions.Builtin.numpyInternal.put``7(ILNumerics.Core.Arrays.Mutable{``0,``1,``2,``3,``4,``5},ILNumerics.BaseArray{``6},ILNumerics.InArray{``0},ILNumerics.PutModes)" /> requires
/// the element type of <paramref name="A" /> to be value types (struct, commonly numeric).</para></remarks>
/// <exception cref="T:System.IndexOutOfRangeException"> if <paramref name="mode" /> is <see cref="F:ILNumerics.PutModes.Raise" /> and any element
/// in <paramref name="indices" /> is out of the allowed range of [-A.S.NumberOfElements...&gt;= A.<see cref="P:ILNumerics.Size.NumberOfElements" />].</exception>
/// <exception cref="T:System.ArgumentException">if either of <paramref name="indices" /> or <paramref name="values" /> is null,
/// if <paramref name="values" /> is empty but <paramref name="indices" /> is not,
/// if the specified <paramref name="mode" /> cannot successfully be applied (for example, because <paramref name="A" /> is empty).</exception>
public static void put<T1, LocalT, InT, OutT, RetT, StorageT, IndT>(Mutable<T1,LocalT,InT,OutT,RetT,StorageT> A, BaseArray<IndT> indices, InArray<T1> values, PutModes mode = PutModes.Raise) where T1 : struct where LocalT : Mutable<T1,LocalT,InT,OutT,RetT,StorageT> where InT : Immutable<T1,LocalT,InT,OutT,RetT,StorageT> where OutT : Mutable<T1,LocalT,InT,OutT,RetT,StorageT> where RetT : Mutable<T1,LocalT,InT,OutT,RetT,StorageT> where StorageT : BaseStorage<T1,LocalT,InT,OutT,RetT,StorageT>, new() where IndT : struct, IConvertible {  ILNumerics.Core.Functions.Builtin.numpyInternal.put<T1, LocalT, InT, OutT, RetT, StorageT, IndT>(A, indices, values, mode);}
/// <summary>
/// Repeat elements along a flattened array or a specific axis.
/// </summary>
/// <typeparam name="T1">Element type of <paramref name="A" />.</typeparam>
/// <typeparam name="LocalT">(subtype of <paramref name="A" />)</typeparam>
/// <typeparam name="InT">(subtype of <paramref name="A" />)</typeparam>
/// <typeparam name="OutT">(subtype of <paramref name="A" />)</typeparam>
/// <typeparam name="RetT">(subtype of <paramref name="A" />)</typeparam>
/// <typeparam name="StorageT">(subtype of <paramref name="A" />)</typeparam>
/// <typeparam name="IndT">Element type of <paramref name="repeats" />. Must be an integer type.</typeparam>
/// <param name="A">The array storing the elements to be repeated.</param>
/// <param name="repeats">Counts for element repetitions.</param>
/// <param name="axis">[Optional] The working dimension. Default: (null) flatten A and repeat all values along dimension #0.</param>
/// <remarks><para>This function repeats elements of <paramref name="A" /> along a single dimension. By default, where no <paramref name="axis" />
/// is defined <paramref name="A" /> is reshaped to a vector in row-major order and all elements are repeated according to <paramref name="repeats" />.
/// Otherwise, if an <paramref name="axis" /> was specified, repetitions are performed along that dimension only. In this case, the array returned
/// has the same shape as <paramref name="A" />, except that the working dimension <paramref name="axis" /> is enlarged.</para><para>The shape of <paramref name="repeats" /> is ignored. Its values give the counts for each element along the axis <paramref name="axis" />.
/// Values must be numeric, positive integers and are read in row-major order. If <paramref name="repeats" /> has exactly one element
/// all elements along the working dimension of <paramref name="A" /> are repeated by the same number. Alternatively, the length of <paramref name="repeats" />
/// must match A.S[axis] to specify individual repetition counts for each element along the working dimension. Thus, if <paramref name="axis" /> is
/// null (default) <paramref name="repeats" /> can be a scalar or an array with 'A.S.NumberOfElements == repeats.S.NumberOfElements'.</para><para>This function returns a new array and does not alter <paramref name="A" /> or any input parameters.</para></remarks>
/// <exception cref="T:System.ArgumentNullException"> if any of the <paramref name="A" /> or <paramref name="repeats" /> is null</exception>
/// <exception cref="T:System.ArgumentException">
/// if <paramref name="axis" /> points to a virtual dimension;
/// if <paramref name="repeats" /> is not a numeric array, is of a shape which is not broadcastable to the length of the working dimension or contains elements which are not convertible to positive integer values;
/// </exception>
public static RetT repeat<T1, LocalT, InT, OutT, RetT, StorageT, IndT>(ConcreteArray<T1,LocalT,InT,OutT,RetT,StorageT> A, BaseArray<IndT> repeats, Nullable<UInt32> axis = null) where T1 : struct where LocalT : Mutable<T1,LocalT,InT,OutT,RetT,StorageT> where InT : Immutable<T1,LocalT,InT,OutT,RetT,StorageT> where OutT : Mutable<T1,LocalT,InT,OutT,RetT,StorageT> where RetT : Mutable<T1,LocalT,InT,OutT,RetT,StorageT> where StorageT : BaseStorage<T1,LocalT,InT,OutT,RetT,StorageT>, new() where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.repeat<T1, LocalT, InT, OutT, RetT, StorageT, IndT>(A, repeats, axis);}
public static Array<Double> sum<IndT>(BaseArray<Double> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.sum<IndT>(A, axes, keepdims);}
public static Array<UInt64> sum<IndT>(BaseArray<UInt64> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.sum<IndT>(A, axes, keepdims);}
public static Array<Int64> sum<IndT>(BaseArray<Int64> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.sum<IndT>(A, axes, keepdims);}
public static Array<UInt32> sum<IndT>(BaseArray<UInt32> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.sum<IndT>(A, axes, keepdims);}
public static Array<Int32> sum<IndT>(BaseArray<Int32> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.sum<IndT>(A, axes, keepdims);}
public static Array<UInt16> sum<IndT>(BaseArray<UInt16> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.sum<IndT>(A, axes, keepdims);}
public static Array<Int16> sum<IndT>(BaseArray<Int16> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.sum<IndT>(A, axes, keepdims);}
public static Array<Byte> sum<IndT>(BaseArray<Byte> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.sum<IndT>(A, axes, keepdims);}
public static Array<SByte> sum<IndT>(BaseArray<SByte> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.sum<IndT>(A, axes, keepdims);}
public static Array<fcomplex> sum<IndT>(BaseArray<fcomplex> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.sum<IndT>(A, axes, keepdims);}
public static Array<complex> sum<IndT>(BaseArray<complex> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.sum<IndT>(A, axes, keepdims);}
public static Array<Single> sum<IndT>(BaseArray<Single> A, BaseArray<IndT> axes = null, Boolean keepdims = false) where IndT : struct, IConvertible { return ILNumerics.Core.Functions.Builtin.numpyInternal.sum<IndT>(A, axes, keepdims);}
  }
}