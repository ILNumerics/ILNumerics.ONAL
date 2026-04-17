using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using ILNumerics.Core.StorageLayer;
using ILNumerics.Core.Misc;
using ILNumerics.Core.Arrays;
using System.Diagnostics;

namespace ILNumerics.Core.Functions.Builtin {

    internal static partial class MathInternal {

        [ThreadStatic]
        private static NRandom m_nrandomGenerator;
        internal static NRandom NRandomGenerator {
            get {
                if (m_nrandomGenerator == null)
                    m_nrandomGenerator = new NRandom((int)Stopwatch.GetTimestamp() + System.Threading.Thread.CurrentThread.ManagedThreadId);
                return m_nrandomGenerator; 
            }
        }

        /// <summary>
        /// Creates a square matrix with normally distributed, pseudo random values.
        /// </summary>
        /// <param name="d0">Number of columns and rows.</param>
        /// <param name="order">[Optional] Storage order of elements. Default: undefined.</param>
        /// <returns>Square matrix filled with random numbers.</returns>
        /// <seealso cref="rand(InArray{long}, StorageOrders)"/>
        /// <seealso cref="randn(InArray{long}, StorageOrders)"/>
        /// <seealso cref="randn(long, long, StorageOrders)"/>
        internal unsafe static Array<double> randn(long d0, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = Storage<double>.Create();
            ret.S.SetAll(dim0: d0, dim1: d0, order: order);

            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<double>((ulong)ret.S.NumberOfElements);
            return randnInplace_internal(ret.RetArray);
        }
        /// <summary>
        /// Creates an array with uniformly distributed, pseudo randnom values.
        /// </summary>
        /// <param name="d0">Length of dimension 0.</param>
        /// <param name="d1">Length of dimension 1.</param>
        /// <param name="order">[Optional] Storage order of elements. Default: undefined.</param>
        /// <returns>Matrix filled with randnom numbers.</returns>
        /// <remarks><para>Values range from 0.0 to 1.0, uniformly distributed.</para></remarks>
        internal unsafe static Array<double> randn(long d0, long d1, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = Storage<double>.Create();
            ret.S.SetAll(d0, d1, order);

            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<double>((ulong)ret.S.NumberOfElements);
            return randnInplace_internal(ret.RetArray);
        }
        /// <summary>
        /// Creates an array with uniformly distributed, pseudo randnom values.
        /// </summary>
        /// <param name="d0">Length of dimension 0.</param>
        /// <param name="d1">Length of dimension 1.</param>
        /// <param name="d2">Length of dimension 2.</param>
        /// <param name="order">[Optional] Storage order of elements. Default: undefined.</param>
        /// <returns>Array filled with randnom numbers.</returns>
        /// <remarks><para>Values range from 0.0 to 1.0, uniformly distributed.</para></remarks>
        internal unsafe static Array<double> randn(long d0, long d1, long d2, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = Storage<double>.Create();
            ret.S.SetAll(d0, d1, d2, order);

            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<double>((ulong)ret.S.NumberOfElements);
            return randnInplace_internal(ret.RetArray);
        }
        /// <summary>
        /// Creates an array with uniformly distributed, pseudo randnom values.
        /// </summary>
        /// <param name="d0">Length of dimension 0.</param>
        /// <param name="d1">Length of dimension 1.</param>
        /// <param name="d2">Length of dimension 2.</param>
        /// <param name="d3">Length of dimension 3.</param>
        /// <param name="order">[Optional] Storage order of elements. Default: undefined.</param>
        /// <returns>Array filled with randnom numbers.</returns>
        /// <remarks><para>Values range from 0.0 to 1.0, uniformly distributed.</para></remarks>
        internal unsafe static Array<double> randn(long d0, long d1, long d2, long d3, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = Storage<double>.Create();
            ret.S.SetAll(d0, d1, d2, d3, order);

            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<double>((ulong)ret.S.NumberOfElements);
            return randnInplace_internal(ret.RetArray);
        }
        /// <summary>
        /// Creates an array with uniformly distributed, pseudo randnom values.
        /// </summary>
        /// <param name="d0">Length of dimension 0.</param>
        /// <param name="d1">Length of dimension 1.</param>
        /// <param name="d2">Length of dimension 2.</param>
        /// <param name="d3">Length of dimension 3.</param>
        /// <param name="d4">Length of dimension 4.</param>
        /// <param name="order">[Optional] Storage order of elements. Default: undefined.</param>
        /// <returns>Array filled with randnom numbers.</returns>
        /// <remarks><para>Values range from 0.0 to 1.0, uniformly distributed.</para></remarks>
        internal unsafe static Array<double> randn(long d0, long d1, long d2, long d3, long d4, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = Storage<double>.Create();
            ret.S.SetAll(d0, d1, d2, d3, d4, order);

            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<double>((ulong)ret.S.NumberOfElements);
            return randnInplace_internal(ret.RetArray);
        }
        /// <summary>
        /// Creates an array with uniformly distributed, pseudo randnom values.
        /// </summary>
        /// <param name="d0">Length of dimension 0.</param>
        /// <param name="d1">Length of dimension 1.</param>
        /// <param name="d2">Length of dimension 2.</param>
        /// <param name="d3">Length of dimension 3.</param>
        /// <param name="d4">Length of dimension 4.</param>
        /// <param name="d5">Length of dimension 5.</param>
        /// <param name="order">[Optional] Storage order of elements. Default: undefined.</param>
        /// <returns>Array filled with randnom numbers.</returns>
        /// <remarks><para>Values range from 0.0 to 1.0, uniformly distributed.</para></remarks>
        internal unsafe static Array<double> randn(long d0, long d1, long d2, long d3, long d4, long d5, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = Storage<double>.Create();
            ret.S.SetAll(d0, d1, d2, d3, d4, d5, order);

            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<double>((ulong)ret.S.NumberOfElements);
            return randnInplace_internal(ret.RetArray);
        }
        /// <summary>
        /// Creates an array with uniformly distributed, pseudo randnom values.
        /// </summary>
        /// <param name="d0">Length of dimension 0.</param>
        /// <param name="d1">Length of dimension 1.</param>
        /// <param name="d2">Length of dimension 2.</param>
        /// <param name="d3">Length of dimension 3.</param>
        /// <param name="d4">Length of dimension 4.</param>
        /// <param name="d5">Length of dimension 5.</param>
        /// <param name="d6">Length of dimension 6.</param>
        /// <param name="order">[Optional] Storage order of elements. Default: undefined.</param>
        /// <returns>Array filled with randnom numbers.</returns>
        /// <remarks><para>Values range from 0.0 to 1.0, uniformly distributed.</para></remarks>
        internal unsafe static Array<double> randn(long d0, long d1, long d2, long d3, long d4, long d5, long d6, StorageOrders order = StorageOrders.ColumnMajor) {

            var ret = Storage<double>.Create();
            ret.S.SetAll(d0, d1, d2, d3, d4, d5, d6, order);

            ret.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<double>((ulong)ret.S.NumberOfElements);
            return randnInplace_internal(ret.RetArray);
        }
        /// <summary>
        /// Creates an array with normally distributed, pseudo random values.
        /// </summary>
        /// <param name="size">Array with target dimension lengths.</param>
        /// <param name="order">[Optional] Target arrays storage order. Default: undefined.</param>
        /// <returns>New array with random numbers and shape as determined.</returns>
        internal unsafe static Array<double> randn(InArray<long> size, StorageOrders order = StorageOrders.ColumnMajor) {
            return randnInplace_internal(Storage<double>.Create(size, order).RetArray);
        }
        /// <summary>
        /// Creates a new array of normally distributed, pseudo random values and a size determined by <paramref name="size"/>.
        /// </summary>
        /// <param name="size">Variable length <see cref="System.Array"/> or comma separated list with lengths of the dimensions of the new array.</param>
        /// <returns>New array of the specified size and undefined storage order, random values.</returns>
        /// <remarks>
        /// <para><paramref name="size"/> cannot be <c>null</c>. Its length determines the number of dimensions of the new array. The elements  
        /// determine the lengths of corresponding dimensions and cannot contain negative values.</para>
        /// <para>Arrays returned from this overload are in undefined storage order. Use <see cref="randn(InArray{long}, StorageOrders)"/> for 
        /// determining the storage order explicitly.</para>
        /// <para>Since the variable length parameter list and the 'params' keyword (C#) implicitly allocates new storage under the control of the 
        /// GC consider using one of the <see cref="vector{T}(T)"/> overloads and <see cref="randn(InArray{long}, StorageOrders)"/> instead to provide the dimension lengths. 
        /// This method is recommended when high-performance is important.</para>
        /// </remarks>
        /// <seealso cref="zeros{T}(long, long, StorageOrders)"/>
        /// <seealso cref="rand(InArray{long}, StorageOrders)"/>
        /// <seealso cref="rand(InArray{long}, StorageOrders)"/>
        /// <seealso cref="vector{T}(T, T, T, T, T, T, T, T, T, T)"/>
        /// <seealso cref="ones{T}(long, long, StorageOrders)"/>
        /// <seealso cref="empty{T}(InArray{long}, StorageOrders)"/>
        /// <seealso cref="Size.MaxNumberOfDimensions"/>
        /// <seealso href="https://ilnumerics.net/ArrayCreation3.html"/>
        /// <exception cref="ArgumentException">if <paramref name="size"/> is null, has more elements than <see cref="Size.MaxNumberOfDimensions"/> or contains negative values.</exception>
        internal static Array<double> randn(params long[] size) {
            return randn(vector(size));
        }

        /// <summary>Random numbers, uniform, parallelized. </summary>
        /// <param name="A">Input array.</param>
        /// <returns>Fills the array with randnom numbers.</returns>
        /// <remarks><para>The operation is efficiently performed on all elements of the input array <paramref name="A"/>.</para>
        /// <para>The storage order of the array returned depends on the order of <paramref name="A"/>. If 
        /// <paramref name="A"/>.<see cref="Size.IsContinuous"/> is <c>true</c> the array returned will have the same <see cref="Size.StorageOrder"/> 
        /// as <paramref name="A"/>. Otherwise, the elements of the returned array will be reordered in undefined order.</para>
        /// <para>The input array <paramref name="A"/> is not altered but its memory may be reused for the returned result.</para></remarks>
        private unsafe static Array<double> randnInplace_internal(BaseArray<double> A) {
            if (object.Equals(A, null)) {
                return null;
            }
            return InnerLoops.randnParallel.Double.Instance.operate(A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>);
        }

    }
    namespace InnerLoops {

        namespace randnParallel {

            
            internal class Double :

            UnaryBaseInPlace<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>> {

                internal static Double Instance = new Double();

                
                public unsafe override void Strided64(Storage<double> A4Size, Storage<double> Out) {

                    System.Diagnostics.Debug.Assert(Out.S.IsContinuous);
                    var random = MathInternal.NRandomGenerator;

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
