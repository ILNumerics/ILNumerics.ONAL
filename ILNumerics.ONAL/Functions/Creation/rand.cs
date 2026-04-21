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
using ILNumerics.Core.Arrays;
using ILNumerics.Core.StorageLayer;
using System;
using System.Diagnostics;

namespace ILNumerics.Core.Functions.Builtin {
    
    internal static partial class MathInternal {

        [ThreadStatic]
        private static Random m_randomGenerator;

        /// <summary>
        /// Random number generator for generation of random numbers.
        /// </summary>
        /// <remarks>This property is thread safe (by means of the [ThreadStatic] attribute).</remarks>
        internal static Random RandomGenerator {
            get {
                if (m_randomGenerator == null) {
                    m_randomGenerator = new Random((int)Stopwatch.GetTimestamp() + System.Threading.Thread.CurrentThread.ManagedThreadId);
                }
                return m_randomGenerator;
            }
        }
        /// <summary>
        /// Creates a symmetric matrix with uniformly distributed, pseudo random values.
        /// </summary>
        /// <param name="d0">Number of columns and number of rows.</param>
        /// <param name="order">[Optional] Storage order of elements. Default: undefined.</param>
        /// <returns>Square matrix filled with random numbers.</returns>
        /// <remarks><para>Values range from 0.0 to 1.0, uniformly distributed.</para>
        /// <para>If the optional argument <paramref name="order"/> is omitted ILNumerics is free to decide for the storage order. Often, an advantageous 
        /// (in terms of processing performance) storage order is selected according to the current algorithmic context, to the current setting of <see cref="Settings.ArrayStyle"/> 
        /// and other factors.</para>
        /// </remarks>
        internal unsafe static Array<double> rand(long d0, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = Storage<double>.Create();
            ret.S.SetAll(dim0: d0, dim1: d0, order: order);
            
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<double>((ulong)ret.S.NumberOfElements);
            return randImplace(ret.RetArray);
        }
        /// <summary>
        /// Creates an array with uniformly distributed, pseudo random values.
        /// </summary>
        /// <param name="d0">Length of dimension 0.</param>
        /// <param name="d1">Length of dimension 1.</param>
        /// <param name="order">[Optional] Storage order of elements. Default: undefined.</param>
        /// <returns>Matrix filled with random numbers.</returns>
        /// <remarks><para>Values range from 0.0 to 1.0, uniformly distributed.</para>
        /// <para>If the optional argument <paramref name="order"/> is omitted ILNumerics is free to decide for the storage order. Often, an advantageous 
        /// (in terms of processing performance) storage order is selected according to the current algorithmic context, to the current setting of <see cref="Settings.ArrayStyle"/> 
        /// and other factors.</para>
        /// </remarks>
        internal unsafe static Array<double> rand(long d0, long d1, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = Storage<double>.Create();
            ret.S.SetAll(d0, d1, order);
            
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<double>((ulong)ret.S.NumberOfElements);
            return randImplace(ret.RetArray);
        }
        /// <summary>
        /// Creates an array with uniformly distributed, pseudo random values.
        /// </summary>
        /// <param name="d0">Length of dimension 0.</param>
        /// <param name="d1">Length of dimension 1.</param>
        /// <param name="d2">Length of dimension 2.</param>
        /// <param name="order">[Optional] Storage order of elements. Default: undefined.</param>
        /// <returns>Array filled with random numbers.</returns>
        /// <remarks><para>Values range from 0.0 to 1.0, uniformly distributed.</para>
        /// <para>If the optional argument <paramref name="order"/> is omitted ILNumerics is free to decide for the storage order. Often, an advantageous 
        /// (in terms of processing performance) storage order is selected according to the current algorithmic context, to the current setting of <see cref="Settings.ArrayStyle"/> 
        /// and other factors.</para>
        /// </remarks>
        internal unsafe static Array<double> rand(long d0, long d1, long d2, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = Storage<double>.Create();
            ret.S.SetAll(d0, d1, d2, order);
            
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<double>((ulong)ret.S.NumberOfElements);
            return randImplace(ret.RetArray);
        }
        /// <summary>
        /// Creates an array with uniformly distributed, pseudo random values.
        /// </summary>
        /// <param name="d0">Length of dimension 0.</param>
        /// <param name="d1">Length of dimension 1.</param>
        /// <param name="d2">Length of dimension 2.</param>
        /// <param name="d3">Length of dimension 3.</param>
        /// <param name="order">[Optional] Storage order of elements. Default: undefined.</param>
        /// <returns>Array filled with random numbers.</returns>
        /// <remarks><para>Values range from 0.0 to 1.0, uniformly distributed.</para>
        /// <para>If the optional argument <paramref name="order"/> is omitted ILNumerics is free to decide for the storage order. Often, an advantageous 
        /// (in terms of processing performance) storage order is selected according to the current algorithmic context, to the current setting of <see cref="Settings.ArrayStyle"/> 
        /// and other factors.</para>
        /// </remarks>
        internal unsafe static Array<double> rand(long d0, long d1, long d2, long d3, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = Storage<double>.Create();
            ret.S.SetAll(d0, d1, d2, d3, order);
            
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<double>((ulong)ret.S.NumberOfElements);
            return randImplace(ret.RetArray);
        }
        /// <summary>
        /// Creates an array with uniformly distributed, pseudo random values.
        /// </summary>
        /// <param name="d0">Length of dimension 0.</param>
        /// <param name="d1">Length of dimension 1.</param>
        /// <param name="d2">Length of dimension 2.</param>
        /// <param name="d3">Length of dimension 3.</param>
        /// <param name="d4">Length of dimension 4.</param>
        /// <param name="order">[Optional] Storage order of elements. Default: undefined.</param>
        /// <returns>Array filled with random numbers.</returns>
        /// <remarks><para>Values range from 0.0 to 1.0, uniformly distributed.</para>
        /// <para>If the optional argument <paramref name="order"/> is omitted ILNumerics is free to decide for the storage order. Often, an advantageous 
        /// (in terms of processing performance) storage order is selected according to the current algorithmic context, to the current setting of <see cref="Settings.ArrayStyle"/> 
        /// and other factors.</para>
        /// </remarks>
        internal unsafe static Array<double> rand(long d0, long d1, long d2, long d3, long d4, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = Storage<double>.Create();
            ret.S.SetAll(d0, d1, d2, d3, d4, order);
            
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<double>((ulong)ret.S.NumberOfElements);
            return randImplace(ret.RetArray);
        }
        /// <summary>
        /// Creates an array with uniformly distributed, pseudo random values.
        /// </summary>
        /// <param name="d0">Length of dimension 0.</param>
        /// <param name="d1">Length of dimension 1.</param>
        /// <param name="d2">Length of dimension 2.</param>
        /// <param name="d3">Length of dimension 3.</param>
        /// <param name="d4">Length of dimension 4.</param>
        /// <param name="d5">Length of dimension 5.</param>
        /// <param name="order">[Optional] Storage order of elements. Default: undefined.</param>
        /// <returns>Array filled with random numbers.</returns>
        /// <remarks><para>Values range from 0.0 to 1.0, uniformly distributed.</para></remarks>
        internal unsafe static Array<double> rand(long d0, long d1, long d2, long d3, long d4, long d5, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = Storage<double>.Create();
            ret.S.SetAll(d0, d1, d2, d3, d4, d5, order);
            
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<double>((ulong)ret.S.NumberOfElements);
            return randImplace(ret.RetArray);
        }
        /// <summary>
        /// Creates an array with uniformly distributed, pseudo random values.
        /// </summary>
        /// <param name="d0">Length of dimension 0.</param>
        /// <param name="d1">Length of dimension 1.</param>
        /// <param name="d2">Length of dimension 2.</param>
        /// <param name="d3">Length of dimension 3.</param>
        /// <param name="d4">Length of dimension 4.</param>
        /// <param name="d5">Length of dimension 5.</param>
        /// <param name="d6">Length of dimension 6.</param>
        /// <param name="order">[Optional] Storage order of elements. Default: undefined.</param>
        /// <returns>Array filled with random numbers.</returns>
        /// <remarks><para>Values range from 0.0 to 1.0, uniformly distributed.</para>
        /// <para>If the optional argument <paramref name="order"/> is omitted ILNumerics is free to decide for the storage order. Often, an advantageous 
        /// (in terms of processing performance) storage order is selected according to the current algorithmic context, to the current setting of <see cref="Settings.ArrayStyle"/> 
        /// and other factors.</para>
        /// </remarks>
        internal unsafe static Array<double> rand(long d0, long d1, long d2, long d3, long d4, long d5, long d6, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = Storage<double>.Create();
            ret.S.SetAll(d0, d1, d2, d3, d4, d5, d6, order);
            
            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<double>((ulong)ret.S.NumberOfElements);
            return randImplace(ret.RetArray);
        }
        /// <summary>
        /// Creates an array with uniformly distributed, pseudo random values.
        /// </summary>
        /// <param name="size">Array with target dimension lengths.</param>
        /// <param name="order">[Optional] Target arrays storage order. Default: undefined.</param>
        /// <returns>New array with random numbers and shape as determined.</returns>
        internal unsafe static Array<double> rand(InArray<long> size, StorageOrders order = StorageOrders.ColumnMajor) {
            return randImplace(Storage<double>.Create(size, order).RetArray);
        }

        /// <summary>
        /// Creates a new array of uniformly distributed, pseudo random values and a size determined by <paramref name="size"/>.
        /// </summary>
        /// <param name="size">Variable length <see cref="System.Array"/> or comma separated list with lengths of the dimensions of the new array.</param>
        /// <returns>New array of the specified size and undefined storage order, random values.</returns>
        /// <remarks>
        /// <para><paramref name="size"/> cannot be <c>null</c>. Its length determines the number of dimensions of the new array. The elements  
        /// determine the lengths of corresponding dimensions and cannot contain negative values.</para>
        /// <para>Arrays returned from this overload are in undefined storage order. Use <see cref="rand(InArray{long}, StorageOrders)"/> for 
        /// determining the storage order explicitly.</para>
        /// <para>Since the variable length parameter list and the 'params' keyword (C#) implicitly allocates new storage under the control of the 
        /// GC consider using one of the <see cref="vector{T}(T)"/> overloads and <see cref="rand(InArray{long}, StorageOrders)"/> instead to provide the dimension lengths. 
        /// This method is recommended when high-performance is important.</para>
        /// </remarks>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        /// <seealso cref="rand(InArray{long}, StorageOrders)"/>
        /// <seealso cref="randn(InArray{long}, StorageOrders)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T, T)"/>
        /// <seealso cref="ones{T}(long, long, StorageOrders)"/>
        /// <seealso cref="empty{T}(InArray{long}, StorageOrders)"/>
        /// <seealso cref="Size.MaxNumberOfDimensions"/>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        /// <exception cref="ArgumentException">if <paramref name="size"/> is null, has more elements than <see cref="Size.MaxNumberOfDimensions"/> or contains negative values.</exception>
        internal static Array<double> rand(params long[] size) {
            return rand(vector(size));
        }

        /// <summary>Random numbers, uniform, parallelized.</summary>
        /// <param name="A">Input array.</param>
        /// <returns>Fills the array with random numbers.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The storage order of the array returned depends on the order of <paramref name="A"/>. If 
        /// <paramref name="A"/>.<see cref="Size.IsContinuous"/> is <c>true</c> the array returned will have the same <see cref="Size.StorageOrder"/> 
        /// as <paramref name="A"/>. Otherwise, the elements of the returned array will be reordered in the undefined element order.</para>
        /// <para>The operation is performed in multiple threads, according to the current settings of <see cref="Settings.MaxNumberThreads"/>.</para>
        /// <para>The input array <paramref name="A"/> is not altered. However, if <paramref name="A"/>'s storage is suitable and not 
        /// shared by other arrays its memory will be used for the result and the operation is performed 'inplace'.</para></remarks>
        private unsafe static Array<double> randImplace(BaseArray<double> A) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.randParallel.Double.Instance.operate(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>); 
        }
    }
    namespace InnerLoops {

        namespace randParallel {

            
            internal class Double :

            UnaryBaseInPlace<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>
            {

                internal static Double Instance = new Double();

                
                public unsafe override void Strided64(Storage<double> A4Size, Storage<double> Out) {

                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous);
                    var random = MathInternal.RandomGenerator;

                    double* p = (double*)Out.Handles[0].Pointer + Out.S.BaseOffset;
                    var len = Out.S.NumberOfElements;
                    long i = 0;
                    while (i < len) {
                        p[i++] = random.NextDouble();
                    }

                }
            }
        }
    }
}