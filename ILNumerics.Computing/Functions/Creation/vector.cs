//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using ILNumerics.Core.StorageLayer;

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {

        /// <summary>
        /// Generator function, creates vector of specified length, compute values by index.
        /// </summary>
        /// <typeparam name="T">The element type.</typeparam>
        /// <param name="length">The number of elements for the new vector.</param>
        /// <param name="func">Generator function, transforms the 0-based index into a value.</param>
        /// <returns>Vector of given <paramref name="length"/> with values computed based on their index.</returns>
        internal unsafe static Array<T> vector<T>(long length, Func<long,T> func) {
            if (length < 0) {
                throw new ArgumentException($"The length for the new vector must be positive. Found: {length}.");
            }
            var ret = Storage<T>.Create();
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>((ulong)length, ret is Storage<BaseArray>);
            ret.S.SetAll(length);
            return apply<T,T,T>(ret.RetArray, default(T), (a, b, j) => func(j));
        }

        /// <summary>
        /// Create a cell vector from provided <paramref name="arrays"/>. 
        /// </summary>
        /// <param name="arrays">ILNumerics arrays to be stored into the cell.</param>
        /// <returns>Cell array with number and values of elements as given by <paramref name="arrays"/>.</returns>
        internal unsafe static Cell cellv(params BaseArray[] arrays) {
            
            return cell(arrays: arrays);
        }

        /// <summary>
        /// Creates a vector from provided 1-D <see cref="System.Array"/>. 
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="values">.Net array with values for the new vector.</param>
        /// <returns>New ILNumerics array with number and values of elements as given by <paramref name="values"/>.</returns>
        internal unsafe static Array<T> vector<T>(params T[] values) {
            return values;
        }
        /// <summary>
        /// Creates a vector from <see cref="System.ReadOnlySpan{T}"/>. 
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="values">Span with values for the new vector.</param>
        /// <returns>New ILNumerics array with number and values of elements as given by <paramref name="values"/>.</returns>
        /// <remarks>A copy from <paramref name="values"/> will be made.</remarks>
        internal unsafe static Array<T> vector<T>(ReadOnlySpan<T> values) where T : unmanaged {
            var ret = Storage<T>.Create();
            var l = values.Length;
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>((ulong)l, ret is Storage<BaseArray>);
            ret.S.SetAll(l);
            values.CopyTo(new Span<T>((void*)ret.Handles[0].Pointer, l)); 
            return ret.RetArray;
        }
        /// <summary>
        /// Creates a new scalar ILNumerics 1-D array with the given value. 
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="v0">The single value for the target array.</param>
        /// <returns>ILNumerics scalar of type <typeparamref name="T"/>.</returns>
        internal unsafe static Array<T> vector<T>(T v0) {
            var ret = Storage<T>.Create();
            const int l = 1;
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>(l, ret is Storage<BaseArray>);
            ret.S.SetAll(l);
            ret.SetValue(v0, 0);
            return ret.RetArray;
        }
        /// <summary>
        /// Initializes a new ILNumerics vector with given values. 
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="v0">First value.</param>
        /// <param name="v1">Second value.</param>
        /// <returns>ILNumerics vector with given values.</returns>
        
        internal unsafe static Array<T> vector<T>(T v0, T v1) {
            var ret = Storage<T>.Create();
            const int l = 2;
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>(l, ret is Storage<BaseArray>);
            ret.S.SetAll(l);
            ret.SetValue(v0, 0);
            ret.SetValue(v1, 1);
            return ret.RetArray;
        }
        /// <summary>
        /// Initializes a new ILNumerics vector with given values. 
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="v0">First value.</param>
        /// <param name="v1">Second value.</param>
        /// <param name="v2">Third value.</param>
        /// <returns>ILNumerics vector with given values.</returns>
        
        internal unsafe static Array<T> vector<T>(T v0, T v1, T v2) {
            var ret = Storage<T>.Create();
            const int l = 3;
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>(l, ret is Storage<BaseArray>);
            ret.S.SetAll(l);
            ret.SetValue(v0, 0);
            ret.SetValue(v1, 1);
            ret.SetValue(v2, 2);
            return ret.RetArray;
        }
        /// <summary>
        /// Initializes a new ILNumerics vector with given values. 
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="v0">Value #0.</param>
        /// <param name="v1">Value #1.</param>
        /// <param name="v2">Value #2.</param>
        /// <param name="v3">Value #3.</param>
        /// <returns>ILNumerics vector with given values.</returns>
        
        internal unsafe static Array<T> vector<T>(T v0, T v1, T v2, T v3) {
            var ret = Storage<T>.Create();
            const int l = 4;
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>(l, ret is Storage<BaseArray>);
            ret.S.SetAll(l);
            ret.SetValue(v0, 0);
            ret.SetValue(v1, 1);
            ret.SetValue(v2, 2);
            ret.SetValue(v3, 3);
            return ret.RetArray;
        }
        /// <summary>
        /// Initializes a new ILNumerics vector with given values. 
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="v0">Value #0.</param>
        /// <param name="v1">Value #1.</param>
        /// <param name="v2">Value #2.</param>
        /// <param name="v3">Value #3.</param>
        /// <param name="v4">Value #4.</param>
        /// <returns>ILNumerics vector with given values.</returns>
        
        internal unsafe static Array<T> vector<T>(T v0, T v1, T v2, T v3, T v4) {
            var ret = Storage<T>.Create();
            const int l = 5;
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>(l, ret is Storage<BaseArray>);
            ret.S.SetAll(l);
            ret.SetValue(v0, 0);
            ret.SetValue(v1, 1);
            ret.SetValue(v2, 2);
            ret.SetValue(v3, 3);
            ret.SetValue(v4, 4);
            return ret.RetArray;
        }
        /// <summary>
        /// Initializes a new ILNumerics vector with given values. 
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="v0">Value #0.</param>
        /// <param name="v1">Value #1.</param>
        /// <param name="v2">Value #2.</param>
        /// <param name="v3">Value #3.</param>
        /// <param name="v4">Value #4.</param>
        /// <param name="v5">Value #5.</param>
        /// <returns>ILNumerics vector with given values.</returns>
        
        internal unsafe static Array<T> vector<T>(T v0, T v1, T v2, T v3, T v4, T v5) {
            var ret = Storage<T>.Create();
            const int l = 6;
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>(l, ret is Storage<BaseArray>);
            ret.S.SetAll(l);
            ret.SetValue(v0, 0);
            ret.SetValue(v1, 1);
            ret.SetValue(v2, 2);
            ret.SetValue(v3, 3);
            ret.SetValue(v4, 4);
            ret.SetValue(v5, 5);
            return ret.RetArray;
        }
        /// <summary>
        /// Initializes a new ILNumerics vector with given values. 
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="v0">Value #0.</param>
        /// <param name="v1">Value #1.</param>
        /// <param name="v2">Value #2.</param>
        /// <param name="v3">Value #3.</param>
        /// <param name="v4">Value #4.</param>
        /// <param name="v5">Value #5.</param>
        /// <param name="v6">Value #6.</param>
        /// <returns>ILNumerics vector with given values.</returns>
        
        internal unsafe static Array<T> vector<T>(T v0, T v1, T v2, T v3, T v4, T v5, T v6) {
            var ret = Storage<T>.Create();
            const int l = 7;
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>(l, ret is Storage<BaseArray>);
            ret.S.SetAll(l);
            ret.SetValue(v0, 0);
            ret.SetValue(v1, 1);
            ret.SetValue(v2, 2);
            ret.SetValue(v3, 3);
            ret.SetValue(v4, 4);
            ret.SetValue(v5, 5);
            ret.SetValue(v6, 6);
            return ret.RetArray;
        }
        /// <summary>
        /// Initializes a new ILNumerics vector with given values. 
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="v0">Value #0.</param>
        /// <param name="v1">Value #1.</param>
        /// <param name="v2">Value #2.</param>
        /// <param name="v3">Value #3.</param>
        /// <param name="v4">Value #4.</param>
        /// <param name="v5">Value #5.</param>
        /// <param name="v6">Value #6.</param>
        /// <param name="v7">Value #7.</param>
        /// <returns>ILNumerics vector with given values.</returns>
        
        internal unsafe static Array<T> vector<T>(T v0, T v1, T v2, T v3, T v4, T v5, T v6, T v7) {
            var ret = Storage<T>.Create();
            const int l = 8;
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>(l, ret is Storage<BaseArray>);
            ret.S.SetAll(l);
            ret.SetValue(v0, 0);
            ret.SetValue(v1, 1);
            ret.SetValue(v2, 2);
            ret.SetValue(v3, 3);
            ret.SetValue(v4, 4);
            ret.SetValue(v5, 5);
            ret.SetValue(v6, 6);
            ret.SetValue(v7, 7);
            return ret.RetArray;
        }
        /// <summary>
        /// Initializes a new ILNumerics vector with given values. 
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="v0">Value #0.</param>
        /// <param name="v1">Value #1.</param>
        /// <param name="v2">Value #2.</param>
        /// <param name="v3">Value #3.</param>
        /// <param name="v4">Value #4.</param>
        /// <param name="v5">Value #5.</param>
        /// <param name="v6">Value #6.</param>
        /// <param name="v7">Value #7.</param>
        /// <param name="v8">Value #8.</param>
        /// <returns>ILNumerics vector with given values.</returns>
        
        internal unsafe static Array<T> vector<T>(T v0, T v1, T v2, T v3, T v4, T v5, T v6, T v7, T v8) {
            var ret = Storage<T>.Create();
            const int l = 9;
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>(l, ret is Storage<BaseArray>);
            ret.S.SetAll(l);
            ret.SetValue(v0, 0);
            ret.SetValue(v1, 1);
            ret.SetValue(v2, 2);
            ret.SetValue(v3, 3);
            ret.SetValue(v4, 4);
            ret.SetValue(v5, 5);
            ret.SetValue(v6, 6);
            ret.SetValue(v7, 7);
            ret.SetValue(v8, 8);
            return ret.RetArray;
        }
        /// <summary>
        /// Initializes a new ILNumerics vector with given values. 
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="v0">Value #0.</param>
        /// <param name="v1">Value #1.</param>
        /// <param name="v2">Value #2.</param>
        /// <param name="v3">Value #3.</param>
        /// <param name="v4">Value #4.</param>
        /// <param name="v5">Value #5.</param>
        /// <param name="v6">Value #6.</param>
        /// <param name="v7">Value #7.</param>
        /// <param name="v8">Value #8.</param>
        /// <param name="v9">Value #9.</param>
        /// <returns>ILNumerics vector with given values.</returns>
        
        internal unsafe static Array<T> vector<T>(T v0, T v1, T v2, T v3, T v4, T v5, T v6, T v7, T v8, T v9) {
            var ret = Storage<T>.Create();
            const int l = 10;
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>(l, ret is Storage<BaseArray>);
            ret.S.SetAll(l);
            ret.SetValue(v0, 0);
            ret.SetValue(v1, 1);
            ret.SetValue(v2, 2);
            ret.SetValue(v3, 3);
            ret.SetValue(v4, 4);
            ret.SetValue(v5, 5);
            ret.SetValue(v6, 6);
            ret.SetValue(v7, 7);
            ret.SetValue(v8, 8);
            ret.SetValue(v9, 9);
            return ret.RetArray;
        }
        /// <summary>
        /// Initializes a new ILNumerics vector with given values. 
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="v0">Value #0.</param>
        /// <param name="v1">Value #1.</param>
        /// <param name="v2">Value #2.</param>
        /// <param name="v3">Value #3.</param>
        /// <param name="v4">Value #4.</param>
        /// <param name="v5">Value #5.</param>
        /// <param name="v6">Value #6.</param>
        /// <param name="v7">Value #7.</param>
        /// <param name="v8">Value #8.</param>
        /// <param name="v9">Value #9.</param>
        /// <param name="v10">Value #10.</param>
        /// <returns>ILNumerics vector with given values.</returns>
        
        internal unsafe static Array<T> vector<T>(T v0, T v1, T v2, T v3, T v4, T v5, T v6, T v7, T v8, T v9, T v10) {
            var ret = Storage<T>.Create();
            const int l = 11;
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>(l, ret is Storage<BaseArray>);
            ret.S.SetAll(l);
            ret.SetValue(v0, 0);
            ret.SetValue(v1, 1);
            ret.SetValue(v2, 2);
            ret.SetValue(v3, 3);
            ret.SetValue(v4, 4);
            ret.SetValue(v5, 5);
            ret.SetValue(v6, 6);
            ret.SetValue(v7, 7);
            ret.SetValue(v8, 8);
            ret.SetValue(v9, 9);
            ret.SetValue(v10, 10);
            return ret.RetArray;
        }
        /// <summary>
        /// Initializes a new ILNumerics vector with given values. 
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="v0">Value #0.</param>
        /// <param name="v1">Value #1.</param>
        /// <param name="v2">Value #2.</param>
        /// <param name="v3">Value #3.</param>
        /// <param name="v4">Value #4.</param>
        /// <param name="v5">Value #5.</param>
        /// <param name="v6">Value #6.</param>
        /// <param name="v7">Value #7.</param>
        /// <param name="v8">Value #8.</param>
        /// <param name="v9">Value #9.</param>
        /// <param name="v10">Value #10.</param>
        /// <param name="v11">Value #11.</param>
        /// <returns>ILNumerics vector with given values.</returns>
        
        internal unsafe static Array<T> vector<T>(T v0, T v1, T v2, T v3, T v4, T v5, T v6, T v7, T v8, T v9, 
            T v10, T v11) {
            var ret = Storage<T>.Create();
            const int l = 12;
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>(l, ret is Storage<BaseArray>);
            ret.S.SetAll(l);
            ret.SetValue(v0, 0);
            ret.SetValue(v1, 1);
            ret.SetValue(v2, 2);
            ret.SetValue(v3, 3);
            ret.SetValue(v4, 4);
            ret.SetValue(v5, 5);
            ret.SetValue(v6, 6);
            ret.SetValue(v7, 7);
            ret.SetValue(v8, 8);
            ret.SetValue(v9, 9);
            ret.SetValue(v10, 10);
            ret.SetValue(v11, 11);
            return ret.RetArray;
        }
        /// <summary>
        /// Initializes a new ILNumerics vector with given values. 
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="v0">Value #0.</param>
        /// <param name="v1">Value #1.</param>
        /// <param name="v2">Value #2.</param>
        /// <param name="v3">Value #3.</param>
        /// <param name="v4">Value #4.</param>
        /// <param name="v5">Value #5.</param>
        /// <param name="v6">Value #6.</param>
        /// <param name="v7">Value #7.</param>
        /// <param name="v8">Value #8.</param>
        /// <param name="v9">Value #9.</param>
        /// <param name="v10">Value #10.</param>
        /// <param name="v11">Value #11.</param>
        /// <param name="v12">Value #12.</param>
        /// <returns>ILNumerics vector with given values.</returns>
        
        internal unsafe static Array<T> vector<T>(T v0, T v1, T v2, T v3, T v4, T v5, T v6, T v7, T v8, T v9, 
                                                    T v10, T v11, T v12) {
            var ret = Storage<T>.Create();
            const int l = 13;
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>(l, ret is Storage<BaseArray>);
            ret.S.SetAll(l);
            ret.SetValue(v0, 0);
            ret.SetValue(v1, 1);
            ret.SetValue(v2, 2);
            ret.SetValue(v3, 3);
            ret.SetValue(v4, 4);
            ret.SetValue(v5, 5);
            ret.SetValue(v6, 6);
            ret.SetValue(v7, 7);
            ret.SetValue(v8, 8);
            ret.SetValue(v9, 9);
            ret.SetValue(v10, 10);
            ret.SetValue(v11, 11);
            ret.SetValue(v12, 12);
            return ret.RetArray;
        }
        /// <summary>
        /// Initializes a new ILNumerics vector with given values. 
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="v0">Value #0.</param>
        /// <param name="v1">Value #1.</param>
        /// <param name="v2">Value #2.</param>
        /// <param name="v3">Value #3.</param>
        /// <param name="v4">Value #4.</param>
        /// <param name="v5">Value #5.</param>
        /// <param name="v6">Value #6.</param>
        /// <param name="v7">Value #7.</param>
        /// <param name="v8">Value #8.</param>
        /// <param name="v9">Value #9.</param>
        /// <param name="v10">Value #10.</param>
        /// <param name="v11">Value #11.</param>
        /// <param name="v12">Value #12.</param>
        /// <param name="v13">Value #13.</param>
        /// <returns>ILNumerics vector with given values.</returns>
        
        internal unsafe static Array<T> vector<T>(T v0, T v1, T v2, T v3, T v4, T v5, T v6, T v7, T v8, T v9, 
                                        T v10, T v11, T v12, T v13) {
            var ret = Storage<T>.Create();
            const int l = 14;
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>(l, ret is Storage<BaseArray>);
            ret.S.SetAll(l);
            ret.SetValue(v0, 0);
            ret.SetValue(v1, 1);
            ret.SetValue(v2, 2);
            ret.SetValue(v3, 3);
            ret.SetValue(v4, 4);
            ret.SetValue(v5, 5);
            ret.SetValue(v6, 6);
            ret.SetValue(v7, 7);
            ret.SetValue(v8, 8);
            ret.SetValue(v9, 9);
            ret.SetValue(v10, 10);
            ret.SetValue(v11, 11);
            ret.SetValue(v12, 12);
            ret.SetValue(v13, 13);
            return ret.RetArray;
        }
        /// <summary>
        /// Initializes a new ILNumerics vector with given values. 
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="v0">Value #0.</param>
        /// <param name="v1">Value #1.</param>
        /// <param name="v2">Value #2.</param>
        /// <param name="v3">Value #3.</param>
        /// <param name="v4">Value #4.</param>
        /// <param name="v5">Value #5.</param>
        /// <param name="v6">Value #6.</param>
        /// <param name="v7">Value #7.</param>
        /// <param name="v8">Value #8.</param>
        /// <param name="v9">Value #9.</param>
        /// <param name="v10">Value #10.</param>
        /// <param name="v11">Value #11.</param>
        /// <param name="v12">Value #12.</param>
        /// <param name="v13">Value #13.</param>
        /// <param name="v14">Value #14.</param>
        /// <returns>ILNumerics vector with given values.</returns>
        
        internal unsafe static Array<T> vector<T>(T v0, T v1, T v2, T v3, T v4, T v5, T v6, T v7, T v8, T v9, 
                                        T v10, T v11, T v12, T v13, T v14) {
            var ret = Storage<T>.Create();
            const int l = 15;
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>(l, ret is Storage<BaseArray>);
            ret.S.SetAll(l);
            ret.SetValue(v0, 0);
            ret.SetValue(v1, 1);
            ret.SetValue(v2, 2);
            ret.SetValue(v3, 3);
            ret.SetValue(v4, 4);
            ret.SetValue(v5, 5);
            ret.SetValue(v6, 6);
            ret.SetValue(v7, 7);
            ret.SetValue(v8, 8);
            ret.SetValue(v9, 9);
            ret.SetValue(v10, 10);
            ret.SetValue(v11, 11);
            ret.SetValue(v12, 12);
            ret.SetValue(v13, 13);
            ret.SetValue(v14, 14);
            return ret.RetArray;
        }
        /// <summary>
        /// Initializes a new ILNumerics vector with given values. 
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="v0">Value #0.</param>
        /// <param name="v1">Value #1.</param>
        /// <param name="v2">Value #2.</param>
        /// <param name="v3">Value #3.</param>
        /// <param name="v4">Value #4.</param>
        /// <param name="v5">Value #5.</param>
        /// <param name="v6">Value #6.</param>
        /// <param name="v7">Value #7.</param>
        /// <param name="v8">Value #8.</param>
        /// <param name="v9">Value #9.</param>
        /// <param name="v10">Value #10.</param>
        /// <param name="v11">Value #11.</param>
        /// <param name="v12">Value #12.</param>
        /// <param name="v13">Value #13.</param>
        /// <param name="v14">Value #14.</param>
        /// <param name="v15">Value #15.</param>
        /// <returns>ILNumerics vector with given values.</returns>
        
        internal unsafe static Array<T> vector<T>(T v0, T v1, T v2, T v3, T v4, T v5, T v6, T v7, T v8, T v9, 
                                    T v10, T v11, T v12, T v13, T v14, T v15) {
            var ret = Storage<T>.Create();
            const int l = 16;
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>(l, ret is Storage<BaseArray>);
            ret.S.SetAll(l);
            ret.SetValue(v0, 0);
            ret.SetValue(v1, 1);
            ret.SetValue(v2, 2);
            ret.SetValue(v3, 3);
            ret.SetValue(v4, 4);
            ret.SetValue(v5, 5);
            ret.SetValue(v6, 6);
            ret.SetValue(v7, 7);
            ret.SetValue(v8, 8);
            ret.SetValue(v9, 9);
            ret.SetValue(v10, 10);
            ret.SetValue(v11, 11);
            ret.SetValue(v12, 12);
            ret.SetValue(v13, 13);
            ret.SetValue(v14, 14);
            ret.SetValue(v15, 15);
            return ret.RetArray;
        }
        /// <summary>
        /// Initializes a new ILNumerics vector with given values. 
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="v0">Value #0.</param>
        /// <param name="v1">Value #1.</param>
        /// <param name="v2">Value #2.</param>
        /// <param name="v3">Value #3.</param>
        /// <param name="v4">Value #4.</param>
        /// <param name="v5">Value #5.</param>
        /// <param name="v6">Value #6.</param>
        /// <param name="v7">Value #7.</param>
        /// <param name="v8">Value #8.</param>
        /// <param name="v9">Value #9.</param>
        /// <param name="v10">Value #10.</param>
        /// <param name="v11">Value #11.</param>
        /// <param name="v12">Value #12.</param>
        /// <param name="v13">Value #13.</param>
        /// <param name="v14">Value #14.</param>
        /// <param name="v15">Value #15.</param>
        /// <param name="v16">Value #16.</param>
        /// <returns>ILNumerics vector with given values.</returns>
        
        internal unsafe static Array<T> vector<T>(T v0, T v1, T v2, T v3, T v4, T v5, T v6, T v7, T v8, T v9, 
                                                T v10, T v11, T v12, T v13, T v14, T v15, T v16) {
            var ret = Storage<T>.Create();
            const int l = 17;
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>(l, ret is Storage<BaseArray>);
            ret.S.SetAll(l);
            ret.SetValue(v0, 0);
            ret.SetValue(v1, 1);
            ret.SetValue(v2, 2);
            ret.SetValue(v3, 3);
            ret.SetValue(v4, 4);
            ret.SetValue(v5, 5);
            ret.SetValue(v6, 6);
            ret.SetValue(v7, 7);
            ret.SetValue(v8, 8);
            ret.SetValue(v9, 9);
            ret.SetValue(v10, 10);
            ret.SetValue(v11, 11);
            ret.SetValue(v12, 12);
            ret.SetValue(v13, 13);
            ret.SetValue(v14, 14);
            ret.SetValue(v15, 15);
            ret.SetValue(v16, 16);
            return ret.RetArray;
        }
        /// <summary>
        /// Initializes a new ILNumerics vector with given values. 
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="v0">Value #0.</param>
        /// <param name="v1">Value #1.</param>
        /// <param name="v2">Value #2.</param>
        /// <param name="v3">Value #3.</param>
        /// <param name="v4">Value #4.</param>
        /// <param name="v5">Value #5.</param>
        /// <param name="v6">Value #6.</param>
        /// <param name="v7">Value #7.</param>
        /// <param name="v8">Value #8.</param>
        /// <param name="v9">Value #9.</param>
        /// <param name="v10">Value #10.</param>
        /// <param name="v11">Value #11.</param>
        /// <param name="v12">Value #12.</param>
        /// <param name="v13">Value #13.</param>
        /// <param name="v14">Value #14.</param>
        /// <param name="v15">Value #15.</param>
        /// <param name="v16">Value #16.</param>
        /// <param name="v17">Value #17.</param>
        /// <returns>ILNumerics vector with given values.</returns>
        
        internal unsafe static Array<T> vector<T>(T v0, T v1, T v2, T v3, T v4, T v5, T v6, T v7, T v8, T v9, 
                                            T v10, T v11, T v12, T v13, T v14, T v15, T v16, T v17) {
            var ret = Storage<T>.Create();
            const int l = 18;
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>(l, ret is Storage<BaseArray>);
            ret.S.SetAll(l);
            ret.SetValue(v0, 0);
            ret.SetValue(v1, 1);
            ret.SetValue(v2, 2);
            ret.SetValue(v3, 3);
            ret.SetValue(v4, 4);
            ret.SetValue(v5, 5);
            ret.SetValue(v6, 6);
            ret.SetValue(v7, 7);
            ret.SetValue(v8, 8);
            ret.SetValue(v9, 9);
            ret.SetValue(v10, 10);
            ret.SetValue(v11, 11);
            ret.SetValue(v12, 12);
            ret.SetValue(v13, 13);
            ret.SetValue(v14, 14);
            ret.SetValue(v15, 15);
            ret.SetValue(v16, 16);
            ret.SetValue(v17, 17);
            return ret.RetArray;
        }
        /// <summary>
        /// Initializes a new ILNumerics vector with given values. 
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="v0">Value #0.</param>
        /// <param name="v1">Value #1.</param>
        /// <param name="v2">Value #2.</param>
        /// <param name="v3">Value #3.</param>
        /// <param name="v4">Value #4.</param>
        /// <param name="v5">Value #5.</param>
        /// <param name="v6">Value #6.</param>
        /// <param name="v7">Value #7.</param>
        /// <param name="v8">Value #8.</param>
        /// <param name="v9">Value #9.</param>
        /// <param name="v10">Value #10.</param>
        /// <param name="v11">Value #11.</param>
        /// <param name="v12">Value #12.</param>
        /// <param name="v13">Value #13.</param>
        /// <param name="v14">Value #14.</param>
        /// <param name="v15">Value #15.</param>
        /// <param name="v16">Value #16.</param>
        /// <param name="v17">Value #17.</param>
        /// <param name="v18">Value #18.</param>
        /// <returns>ILNumerics vector with given values.</returns>
        
        internal unsafe static Array<T> vector<T>(T v0, T v1, T v2, T v3, T v4, T v5, T v6, T v7, T v8, T v9, 
                                            T v10, T v11, T v12, T v13, T v14, T v15, T v16, T v17, T v18) {
            var ret = Storage<T>.Create();
            const int l = 19;
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>(l, ret is Storage<BaseArray>);
            ret.S.SetAll(l);
            ret.SetValue(v0, 0);
            ret.SetValue(v1, 1);
            ret.SetValue(v2, 2);
            ret.SetValue(v3, 3);
            ret.SetValue(v4, 4);
            ret.SetValue(v5, 5);
            ret.SetValue(v6, 6);
            ret.SetValue(v7, 7);
            ret.SetValue(v8, 8);
            ret.SetValue(v9, 9);
            ret.SetValue(v10, 10);
            ret.SetValue(v11, 11);
            ret.SetValue(v12, 12);
            ret.SetValue(v13, 13);
            ret.SetValue(v14, 14);
            ret.SetValue(v15, 15);
            ret.SetValue(v16, 16);
            ret.SetValue(v17, 17);
            ret.SetValue(v18, 18);
            return ret.RetArray;
        }

        /// <summary>
        /// Initializes a new ILNumerics vector with given values. 
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="v0">Value #0.</param>
        /// <param name="v1">Value #1.</param>
        /// <param name="v2">Value #2.</param>
        /// <param name="v3">Value #3.</param>
        /// <param name="v4">Value #4.</param>
        /// <param name="v5">Value #5.</param>
        /// <param name="v6">Value #6.</param>
        /// <param name="v7">Value #7.</param>
        /// <param name="v8">Value #8.</param>
        /// <param name="v9">Value #9.</param>
        /// <param name="v10">Value #10.</param>
        /// <param name="v11">Value #11.</param>
        /// <param name="v12">Value #12.</param>
        /// <param name="v13">Value #13.</param>
        /// <param name="v14">Value #14.</param>
        /// <param name="v15">Value #15.</param>
        /// <param name="v16">Value #16.</param>
        /// <param name="v17">Value #17.</param>
        /// <param name="v18">Value #18.</param>
        /// <param name="v19">Value #19.</param>
        /// <returns>ILNumerics vector with given values.</returns>
        
        internal unsafe static Array<T> vector<T>(T v0, T v1, T v2, T v3, T v4, T v5, T v6, T v7, T v8, T v9, 
                                                T v10, T v11, T v12, T v13, T v14, T v15, T v16, T v17, T v18, T v19) {
            var ret = Storage<T>.Create();
            const int l = 20;
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<T>(l, ret is Storage<BaseArray>);
            ret.S.SetAll(l);
            ret.SetValue(v0, 0);
            ret.SetValue(v1, 1);
            ret.SetValue(v2, 2);
            ret.SetValue(v3, 3);
            ret.SetValue(v4, 4);
            ret.SetValue(v5, 5);
            ret.SetValue(v6, 6);
            ret.SetValue(v7, 7);
            ret.SetValue(v8, 8);
            ret.SetValue(v9, 9);
            ret.SetValue(v10, 10);
            ret.SetValue(v11, 11);
            ret.SetValue(v12, 12);
            ret.SetValue(v13, 13);
            ret.SetValue(v14, 14);
            ret.SetValue(v15, 15);
            ret.SetValue(v16, 16);
            ret.SetValue(v17, 17);
            ret.SetValue(v18, 18);
            ret.SetValue(v19, 19);
            return ret.RetArray;
        }

    }
}
