using ILNumerics.Core.Arrays;
using ILNumerics.Core.StorageLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics {

    public static partial class ExtensionMethods {

        #region SetValue Extensions

        #region general <mostly value typed T, or string> extensions
        /// <summary>
        /// Sets the value of the element addressed by <paramref name="d0"/>. 
        /// </summary>
        /// <param name="value">The new value.</param>
        /// <param name="A">This mutable array.</param>
        /// <param name="d0">Index into dimension #0.</param>
        /// <remarks>
        /// <para>This function replaces a single value of <paramref name="A"/> at the position as specified by 
        /// the dimensional index <paramref name="d0"/>.</para>
        /// <para><paramref name="d0"/> is considered a <b>sequential</b> index into its own and into subsequent, merged 
        /// dimensions (Matlab/ILNumericsV4 indexing style).</para>
        /// <para>The function supports automatic expansion when addressing elements outside of the 
        /// current dimension range. However, removal of parts of the array is not supported. Use the indexers 
        /// defined on <see cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}"/> for removal operations 
        /// in array style <see cref="ArrayStyles.ILNumericsV4"/>. It is recommended to initialize your arrays with a fixed 
        /// size and to prevent from array size changing operations for best performance, though.</para>
        /// </remarks>
        /// <exception cref="OutOfMemoryException"> if the expanded size as determined by the given indices is 
        /// too large to be allocated.</exception>
        /// <exception cref="InvalidOperationException">if this array is currently in use by other arrays and 
        /// locked against modifications. This should not happen during 'regular' use and may indicate a design flaw.</exception>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long)"/>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html">Subarray tutorial in the ILNumerics online documentation</seealso>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        public static void SetValue<T1, LocalT, InT, OutT, RetT, StorageT>(
                this Mutable<T1, LocalT, InT, OutT, RetT, StorageT> A, 
                T1 value, 
                long d0)
            where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {
            lock (A.SynchObj) {
                
                A.Storage.Assign(A.Storage.SetValue(value, d0), true, true); // SetValue() handles Expand also
            }
        }
        /// <summary>
        /// Sets the value of the element addressed by <paramref name="d0"/>...<paramref name="d1"/>. 
        /// </summary>
        /// <param name="value">The new value.</param>
        /// <param name="A">This mutable array.</param>
        /// <param name="d0">Index into dimension #0.</param>
        /// <param name="d1">Index into dimension #1.</param>
        /// <remarks>
        /// <para>This function replaces a single value of <paramref name="A"/> at the position as specified by 
        /// the dimensional indices <paramref name="d0"/>...<paramref name="d1"/>.</para>
        /// <para><paramref name="d1"/> is considered a <b>sequential</b> index into its own and into subsequent, merged 
        /// dimensions (Matlab/ILNumericsV4 indexing style).</para>
        /// <para>The function supports automatic expansion when addressing elements outside of the 
        /// current dimension range. However, removal of parts of the array is not supported. Use the indexers 
        /// defined on <see cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}"/> for removal operations 
        /// in array style <see cref="ArrayStyles.ILNumericsV4"/>. It is recommended to initialize your arrays with a fixed 
        /// size and to prevent from array size changing operations for best performance, though.</para>
        /// </remarks>
        /// <exception cref="OutOfMemoryException"> if the expanded size as determined by the given indices is 
        /// too large to be allocated.</exception>
        /// <exception cref="InvalidOperationException">if this array is currently in use by other arrays and 
        /// locked against modifications. This should not happen during 'regular' use and may indicate a bad design.</exception>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long)"/>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html">Subarray tutorial in the ILNumerics online documentation</seealso>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        public static void SetValue<T1, LocalT, InT, OutT, RetT, StorageT>(
                this Mutable<T1, LocalT, InT, OutT, RetT, StorageT> A,
                T1 value, long d0, long d1)
            where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {
            lock (A.SynchObj) {
                
                A.Storage.Assign(A.Storage.SetValue(value, d0, d1), true, true); // SetValue() handles Expand also
            }
        }
        /// <summary>
        /// Sets the value of the element addressed by <paramref name="d0"/>...<paramref name="d2"/>. 
        /// </summary>
        /// <param name="value">The new value.</param>
        /// <param name="A">This mutable array.</param>
        /// <param name="d0">Index into dimension #0.</param>
        /// <param name="d1">Index into dimension #1.</param>
        /// <param name="d2">Index into dimension #2.</param>
        /// <remarks>
        /// <para>This function replaces a single value of <paramref name="A"/> at the position as specified by 
        /// the dimensional indices <paramref name="d0"/>...<paramref name="d2"/>.</para>
        /// <para><paramref name="d2"/> is considered a <b>sequential</b> index into its own and into subsequent, merged 
        /// dimensions (Matlab/ILNumericsV4 indexing style).</para>
        /// <para>The function supports automatic expansion when addressing elements outside of the 
        /// current dimension range. However, removal of parts of the array is not supported. Use the indexers 
        /// defined on <see cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}"/> for removal operations 
        /// in array style <see cref="ArrayStyles.ILNumericsV4"/>. It is recommended to initialize your arrays with a fixed 
        /// size and to prevent from array size changing operations for best performance, though.</para>
        /// </remarks>
        /// <exception cref="OutOfMemoryException"> if the expanded size as determined by the given indices is 
        /// too large to be allocated.</exception>
        /// <exception cref="InvalidOperationException">if this array is currently in use by other arrays and 
        /// locked against modifications. This should not happen during 'regular' use and may indicate a bad design.</exception>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long)"/>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html">Subarray tutorial in the ILNumerics online documentation</seealso>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        public static void SetValue<T1, LocalT, InT, OutT, RetT, StorageT>(
                this Mutable<T1, LocalT, InT, OutT, RetT, StorageT> A,
                T1 value, long d0, long d1, long d2)
            where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {
            lock (A.SynchObj) {
                
                A.Storage.Assign(A.Storage.SetValue(value, d0, d1, d2), true, true); // SetValue() handles Expand also
            }
        }
        /// <summary>
        /// Sets the value of the element addressed by <paramref name="d0"/>...<paramref name="d3"/>. 
        /// </summary>
        /// <param name="value">The new value.</param>
        /// <param name="A">This mutable array.</param>
        /// <param name="d0">Index into dimension #0.</param>
        /// <param name="d1">Index into dimension #1.</param>
        /// <param name="d2">Index into dimension #2.</param>
        /// <param name="d3">Index into dimension #3.</param>
        /// <remarks>
        /// <para>This function replaces a single value of <paramref name="A"/> at the position as specified by 
        /// the dimensional indices <paramref name="d0"/>...<paramref name="d3"/>.</para>
        /// <para><paramref name="d3"/> is considered a <b>sequential</b> index into its own and into subsequent, merged 
        /// dimensions (Matlab/ILNumericsV4 indexing style).</para>
        /// <para>The function supports automatic expansion when addressing elements outside of the 
        /// current dimension range. However, removal of parts of the array is not supported. Use the indexers 
        /// defined on <see cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}"/> for removal operations 
        /// in array style <see cref="ArrayStyles.ILNumericsV4"/>. It is recommended to initialize your arrays with a fixed 
        /// size and to prevent from array size changing operations for best performance, though.</para>
        /// </remarks>
        /// <exception cref="OutOfMemoryException"> if the expanded size as determined by the given indices is 
        /// too large to be allocated.</exception>
        /// <exception cref="InvalidOperationException">if this array is currently in use by other arrays and 
        /// locked against modifications. This should not happen during 'regular' use and may indicate a bad design.</exception>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long)"/>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html">Subarray tutorial in the ILNumerics online documentation</seealso>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        public static void SetValue<T1, LocalT, InT, OutT, RetT, StorageT>(
                this Mutable<T1, LocalT, InT, OutT, RetT, StorageT> A,
                T1 value, long d0, long d1, long d2, long d3)
            where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {
            lock (A.SynchObj) {
                
                A.Storage.Assign(A.Storage.SetValue(value, d0, d1, d2, d3), true, true); // SetValue() handles Expand also
            }
        }
        /// <summary>
        /// Sets the value of the element addressed by <paramref name="d0"/>...<paramref name="d4"/>. 
        /// </summary>
        /// <param name="value">The new value.</param>
        /// <param name="A">This mutable array.</param>
        /// <param name="d0">Index into dimension #0.</param>
        /// <param name="d1">Index into dimension #1.</param>
        /// <param name="d2">Index into dimension #2.</param>
        /// <param name="d3">Index into dimension #3.</param>
        /// <param name="d4">Index into dimension #4.</param>
        /// <remarks>
        /// <para>This function replaces a single value of <paramref name="A"/> at the position as specified by 
        /// the dimensional indices <paramref name="d0"/>...<paramref name="d4"/>.</para>
        /// <para><paramref name="d4"/> is considered a <b>sequential</b> index into its own and into subsequent, merged 
        /// dimensions (Matlab/ILNumericsV4 indexing style).</para>
        /// <para>The function supports automatic expansion when addressing elements outside of the 
        /// current dimension range. However, removal of parts of the array is not supported. Use the indexers 
        /// defined on <see cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}"/> for removal operations 
        /// in array style <see cref="ArrayStyles.ILNumericsV4"/>. It is recommended to initialize your arrays with a fixed 
        /// size and to prevent from array size changing operations for best performance, though.</para>
        /// </remarks>
        /// <exception cref="OutOfMemoryException"> if the expanded size as determined by the given indices is 
        /// too large to be allocated.</exception>
        /// <exception cref="InvalidOperationException">if this array is currently in use by other arrays and 
        /// locked against modifications. This should not happen during 'regular' use and may indicate a bad design.</exception>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long)"/>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html">Subarray tutorial in the ILNumerics online documentation</seealso>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        public static void SetValue<T1, LocalT, InT, OutT, RetT, StorageT>(
                this Mutable<T1, LocalT, InT, OutT, RetT, StorageT> A,
                T1 value, long d0, long d1, long d2, long d3, long d4)
            where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {
            lock (A.SynchObj) {
                
                A.Storage.Assign(A.Storage.SetValue(value, d0, d1, d2, d3, d4), true, true); // SetValue() handles Expand also
            }
        }
        /// <summary>
        /// Sets the value of the element addressed by <paramref name="d0"/>...<paramref name="d5"/>. 
        /// </summary>
        /// <param name="value">The new value.</param>
        /// <param name="A">This mutable array.</param>
        /// <param name="d0">Index into dimension #0.</param>
        /// <param name="d1">Index into dimension #1.</param>
        /// <param name="d2">Index into dimension #2.</param>
        /// <param name="d3">Index into dimension #3.</param>
        /// <param name="d4">Index into dimension #4.</param>
        /// <param name="d5">Index into dimension #5.</param>
        /// <remarks>
        /// <para>This function replaces a single value of <paramref name="A"/> at the position as specified by 
        /// the dimensional indices <paramref name="d0"/>...<paramref name="d5"/>.</para>
        /// <para><paramref name="d5"/> is considered a <b>sequential</b> index into its own and into subsequent, merged 
        /// dimensions (Matlab/ILNumericsV4 indexing style).</para>
        /// <para>The function supports automatic expansion when addressing elements outside of the 
        /// current dimension range. However, removal of parts of the array is not supported. Use the indexers 
        /// defined on <see cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}"/> for removal operations 
        /// in array style <see cref="ArrayStyles.ILNumericsV4"/>. It is recommended to initialize your arrays with a fixed 
        /// size and to prevent from array size changing operations for best performance, though.</para>
        /// </remarks>
        /// <exception cref="OutOfMemoryException"> if the expanded size as determined by the given indices is 
        /// too large to be allocated.</exception>
        /// <exception cref="InvalidOperationException">if this array is currently in use by other arrays and 
        /// locked against modifications. This should not happen during 'regular' use and may indicate a bad design.</exception>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long)"/>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html">Subarray tutorial in the ILNumerics online documentation</seealso>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        public static void SetValue<T1, LocalT, InT, OutT, RetT, StorageT>(
                this Mutable<T1, LocalT, InT, OutT, RetT, StorageT> A,
                T1 value, long d0, long d1, long d2, long d3, long d4, long d5)
            where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {
            lock (A.SynchObj) {
                
                A.Storage.Assign(A.Storage.SetValue(value, d0, d1, d2, d3, d4, d5), true, true); // SetValue() handles Expand also
            }
        }
        /// <summary>
        /// Sets the value of the element addressed by <paramref name="d0"/>...<paramref name="d6"/>. 
        /// </summary>
        /// <param name="value">The new value.</param>
        /// <param name="A">This mutable array.</param>
        /// <param name="d0">Index into dimension #0.</param>
        /// <param name="d1">Index into dimension #1.</param>
        /// <param name="d2">Index into dimension #2.</param>
        /// <param name="d3">Index into dimension #3.</param>
        /// <param name="d4">Index into dimension #4.</param>
        /// <param name="d5">Index into dimension #5.</param>
        /// <param name="d6">Index into dimension #6.</param>
        /// <remarks>
        /// <para>This function replaces a single value of <paramref name="A"/> at the position as specified by 
        /// the dimensional indices <paramref name="d0"/>...<paramref name="d6"/>.</para>
        /// <para><paramref name="d6"/> is considered a <b>sequential</b> index into its own and into subsequent, merged 
        /// dimensions (Matlab/ILNumericsV4 indexing style).</para>
        /// <para>The function supports automatic expansion when addressing elements outside of the 
        /// current dimension range. However, removal of parts of the array is not supported. Use the indexers 
        /// defined on <see cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}"/> for removal operations 
        /// in array style <see cref="ArrayStyles.ILNumericsV4"/>. It is recommended to initialize your arrays with a fixed 
        /// size and to prevent from array size changing operations for best performance, though.</para>
        /// </remarks>
        /// <exception cref="OutOfMemoryException"> if the expanded size as determined by the given indices is 
        /// too large to be allocated.</exception>
        /// <exception cref="InvalidOperationException">if this array is currently in use by other arrays and 
        /// locked against modifications. This should not happen during 'regular' use and may indicate a bad design.</exception>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long)"/>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html">Subarray tutorial in the ILNumerics online documentation</seealso>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        public static void SetValue<T1, LocalT, InT, OutT, RetT, StorageT>(
                this Mutable<T1, LocalT, InT, OutT, RetT, StorageT> A,
                T1 value, long d0, long d1, long d2, long d3, long d4, long d5, long d6)
            where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {
            lock (A.SynchObj) {
                
                A.Storage.Assign(A.Storage.SetValue(value, d0, d1, d2, d3, d4, d5, d6), true, true); // SetValue() handles Expand also
            }
        }
        #endregion

        #region SetValue(params long[]), SetValue(InArray<long>)

        /// <summary>
        /// Sets the value of the element addressed by index array <paramref name="dims"/>. 
        /// </summary>
        /// <param name="value">The new value.</param>
        /// <param name="A">This mutable array.</param>
        /// <param name="dims">Index array addressing the elements location in all referenced dimensions.</param>
        /// <remarks>
        /// <para>This function replaces a single value of <paramref name="A"/> at the position as specified by 
        /// the dimensional indices <paramref name="dims"/></para>
        /// <para>The function supports automatic expansion when addressing elements outside of the 
        /// current dimension range. However, removal of parts of the array is not supported. Use the indexers 
        /// defined on <see cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}"/> arrays for removal operations 
        /// in array style <see cref="ArrayStyles.ILNumericsV4"/>. It is recommended to initialize your arrays with a fixed 
        /// size and to prevent from array size changing operations for best performance, though.</para>
        /// </remarks>
        /// <exception cref="OutOfMemoryException"> if the expanded size as determined by the given indices is 
        /// too large to be allocated.</exception>
        /// <exception cref="InvalidOperationException">if this array is currently in use by other arrays and 
        /// locked against modifications. This should not happen during 'regular' use and often indicate bad design.</exception>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long)"/>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html">Subarray tutorial in the ILNumerics online documentation</seealso>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        public static void SetValue<T1, LocalT, InT, OutT, RetT, StorageT>(
                this Mutable<T1, LocalT, InT, OutT, RetT, StorageT> A,
                T1 value, params long[] dims)
            where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {
            lock (A.SynchObj) {
                
                A.Storage.Assign(A.Storage.SetValue(value, dims, true), true, true); // SetValue() handles Expand also
            }
        }
        /// <summary>
        /// Sets the value of the element addressed by index array <paramref name="dims"/>. 
        /// </summary>
        /// <param name="value">The new value.</param>
        /// <param name="A">This mutable array.</param>
        /// <param name="dims">Index array addressing the elements location in all referenced dimensions.</param>
        /// <remarks>
        /// <para>This function replaces a single value of <paramref name="A"/> at the position as specified by 
        /// the dimensional indices <paramref name="dims"/></para>
        /// <para>The function supports automatic expansion when addressing elements outside of the 
        /// current dimension range. However, removal of parts of the array is not supported. Use the indexers 
        /// defined on <see cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}"/> arrays for removal operations 
        /// in array style <see cref="ArrayStyles.ILNumericsV4"/>. It is recommended to initialize your arrays with a fixed 
        /// size and to prevent from array size changing operations for best performance, though.</para>
        /// </remarks>
        /// <exception cref="OutOfMemoryException"> if the expanded size as determined by the given indices is 
        /// too large to be allocated.</exception>
        /// <exception cref="InvalidOperationException">if this array is currently in use by other arrays and 
        /// locked against modifications. This should not happen during 'regular' use and often indicate bad design.</exception>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long)"/>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html">Subarray tutorial in the ILNumerics online documentation</seealso>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        public static void SetValue<T1, LocalT, InT, OutT, RetT, StorageT>(
                this Mutable<T1, LocalT, InT, OutT, RetT, StorageT> A,
                T1 value, InArray<long> dims)
            where StorageT : BaseStorage<T1, LocalT, InT, OutT, RetT, StorageT>, new()
            where LocalT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where InT : Immutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where OutT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT>
            where RetT : Mutable<T1, LocalT, InT, OutT, RetT, StorageT> {
            lock (A.SynchObj) {
                
                A.Storage.Assign(A.Storage.SetValue(value, dims, true), true, true); // SetValue() handles Expand also
            }
        }

        #endregion

        #region SetValue Cell Extensions

        /// <summary>
        /// Sets the value of the scalar (0-dimensional) cell array <paramref name="A"/>. 
        /// </summary>
        /// <param name="value">The new value.</param>
        /// <param name="A">This mutable, scalar (0-dim) array.</param>
        /// <remarks>
        /// <para>This function replaces the only value of the 0-dimensional scalar array <paramref name="A"/>. It 
        /// supports the deep-indexing feature on cell arrays. Regular arrays can be addressed by an index value 0 for 
        /// virtual dimensions. In contrast to that indexing with integer values into cell arrays (deep indexing) 
        /// requires exactly one index for each (non-virtual) dimension of every array addressed. Therefore, a 0-index 
        /// overload exists for indexing into scalar (0-dim) cell arrays.</para>
        /// <para>The function supports automatic expansion when addressing elements outside of the 
        /// current dimension range. However, removal of parts of the array is not supported. Use the indexers 
        /// defined on <see cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}"/> for removal operations 
        /// in array style <see cref="ArrayStyles.ILNumericsV4"/>. It is recommended to initialize your arrays with a fixed 
        /// size and to prevent from array size changing operations for best performance, though.</para>
        /// </remarks>
        /// <exception cref="OutOfMemoryException"> if the expanded size as determined by the given indices is 
        /// too large to be allocated.</exception>
        /// <exception cref="InvalidOperationException">if this array is currently in use by other arrays and 
        /// locked against modifications. This should not happen during 'regular' use and may indicate a bad design.</exception>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long)"/>
        /// <seealso href="https://ilnumerics.net/cell-arrays.html">ILNumerics cell arrays tutorial online.</seealso>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html">Subarray tutorial in the ILNumerics online documentation.</seealso>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        public static unsafe void SetValue(this Mutable<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, BaseArray value) {

            lock (A.SynchObj) {
                A.Storage.Assign((A.Storage as IStorage).SetCellContentDirect(value, new Span<long>(), 0, true) as CellStorage); 
            }
        }

        /// <summary>
        /// Sets the value of the element addressed by <paramref name="d0"/>. Supports deep indexing into cell elements.
        /// </summary>
        /// <param name="value">The new value.</param>
        /// <param name="A">This mutable cell array.</param>
        /// <param name="d0">Index into dimension #0.</param>
        /// <remarks>
        /// <para>This function replaces a single value of <paramref name="A"/> at the position as specified by 
        /// the dimensional index <paramref name="d0"/>.</para>
        /// <para><paramref name="d0"/> is considered a <b>sequential</b> index into its own and into subsequent, merged 
        /// dimensions (Matlab/ILNumericsV4 indexing style).</para>
        /// <para>Deep indexing: the element to be replaced is located by matching the full set of given indices with 
        /// the existing dimensions of the cell, starting with index <paramref name="d0"/>. If the indices corresponding 
        /// to the number of dimensions of this cell point to an ILNumerics array subsequent indices are used as indices into that array. On the 
        /// other hand, if the element found is a cell, indexing is continued recursively at that cell element.</para>
        /// <para>The function supports automatic expansion for existing dimensions of regular (non-cell) arrays. When 
        /// the given indices address a non-existing element of a non-cell array the array currently stored in the cell 
        /// is expanded in a way that the new value can be stored at the provided location of the array.</para>
        /// <para>Removal of parts of the array is not supported. Use the indexers 
        /// defined on <see cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}"/> for removal operations 
        /// in array style <see cref="ArrayStyles.ILNumericsV4"/>. It is recommended to initialize your arrays with a fixed 
        /// size and to prevent from array size changing operations for best performance, though.</para>
        /// </remarks>
        /// <exception cref="OutOfMemoryException"> if the expanded size as determined by the given indices is 
        /// too large to be allocated.</exception>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long)"/>
        /// <seealso href="https://ilnumerics.net/cell-arrays.html">ILNumerics cell arrays tutorial online.</seealso>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html">Subarray tutorial in the ILNumerics online documentation</seealso>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        public static void SetValue(this Mutable<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, BaseArray value,
                long d0) {

            lock (A.SynchObj) {
                A.Storage.Assign((A.Storage as IStorage).SetCellContentDirect(value, _params<long>.Get(d0), 0, allowExpand: true) as CellStorage);
            }
        }
        /// <summary>
        /// Sets the value of the element addressed by <paramref name="d0"/>...<paramref name="d1"/>. Supports deep indexing into cell elements. 
        /// </summary>
        /// <param name="value">The new value.</param>
        /// <param name="A">This mutable array.</param>
        /// <param name="d0">Index into dimension #0.</param>
        /// <param name="d1">Index into dimension #1.</param>
        /// <remarks>
        /// <para>This function replaces a single value of <paramref name="A"/> at the position as specified by 
        /// the dimensional indices <paramref name="d0"/> and <paramref name="d1"/>.</para>
        /// <para><paramref name="d1"/> is considered a <b>sequential</b> index into its own and into subsequent, merged 
        /// dimensions (Matlab/ILNumericsV4 indexing style).</para>
        /// <para>Deep indexing: the element to be replaced is located by matching the full set of given indices with 
        /// the existing dimensions of the cell, starting with index <paramref name="d0"/>. If the indices corresponding 
        /// to the number of dimensions of this cell point to an ILNumerics array subsequent indices are used as indices into that array. On the 
        /// other hand, if the element found is a cell, indexing is continued recursively at that cell element.</para>
        /// <para>The function supports automatic expansion for existing dimensions of regular (non-cell) arrays. When 
        /// the given indices address a non-existing element of a non-cell array the array currently stored in the cell 
        /// is expanded in a way that the new value can be stored at the provided location of the array.</para>
        /// <para>Removal of parts of the array is not supported. Use the indexers 
        /// defined on <see cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}"/> for removal operations 
        /// in array style <see cref="ArrayStyles.ILNumericsV4"/>. It is recommended to initialize your arrays with a fixed 
        /// size and to prevent from array size changing operations for best performance, though.</para>
        /// </remarks>
        /// <exception cref="OutOfMemoryException"> if the expanded size as determined by the given indices is 
        /// too large to be allocated.</exception>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long)"/>
        /// <seealso href="https://ilnumerics.net/cell-arrays.html">ILNumerics cell arrays tutorial online.</seealso>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html">Subarray tutorial in the ILNumerics online documentation</seealso>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        public static void SetValue(this Mutable<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, BaseArray value,
                long d0, long d1) {

            lock (A.SynchObj) {
                A.Storage.Assign((A.Storage as IStorage).SetCellContentDirect(value, _params<long>.Get(d0, d1), 0, allowExpand: true) as CellStorage, toOutT: true, fromRetT: true);
            }
        }

        /// <summary>
        /// Sets the value of the element addressed by <paramref name="d0"/>...<paramref name="d2"/>. Supports deep indexing into cell elements. 
        /// </summary>
        /// <param name="value">The new value.</param>
        /// <param name="A">This mutable array.</param>
        /// <param name="d0">Index into dimension #0.</param>
        /// <param name="d1">Index into dimension #1.</param>
        /// <param name="d2">Index into dimension #2.</param>
        /// <remarks>
        /// <para>This function replaces a single value of <paramref name="A"/> at the position as specified by 
        /// the dimensional indices <paramref name="d0"/> ... <paramref name="d2"/>.</para>
        /// <para><paramref name="d2"/> is considered a <b>sequential</b> index into its own and into subsequent, merged 
        /// dimensions (Matlab/ILNumericsV4 indexing style).</para>
        /// <para>Deep indexing: the element to be replaced is located by matching the full set of given indices with 
        /// the existing dimensions of the cell, starting with index <paramref name="d0"/>. If the indices corresponding 
        /// to the number of dimensions of this cell point to an ILNumerics array subsequent indices are used as indices into that array. On the 
        /// other hand, if the element found is a cell, indexing is continued recursively at that cell element.</para>
        /// <para>The function supports automatic expansion for existing dimensions of regular (non-cell) arrays. When 
        /// the given indices address a non-existing element of a non-cell array the array currently stored in the cell 
        /// is expanded in a way that the new value can be stored at the provided location of the array.</para>
        /// <para>Removal of parts of the array is not supported. Use the indexers 
        /// defined on <see cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}"/> for removal operations 
        /// in array style <see cref="ArrayStyles.ILNumericsV4"/>. It is recommended to initialize your arrays with a fixed 
        /// size and to prevent from array size changing operations for best performance, though.</para>
        /// </remarks>
        /// <exception cref="OutOfMemoryException"> if the expanded size as determined by the given indices is 
        /// too large to be allocated.</exception>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long)"/>
        /// <seealso href="https://ilnumerics.net/cell-arrays.html">ILNumerics cell arrays tutorial online.</seealso>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html">Subarray tutorial in the ILNumerics online documentation</seealso>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        public static void SetValue(this Mutable<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, BaseArray value,
                long d0, long d1, long d2) {

            lock (A.SynchObj) {
                A.Storage.Assign((A.Storage as IStorage).SetCellContentDirect(value, _params<long>.Get(d0, d1, d2), 0, allowExpand: true) as CellStorage);
            }
        }

        /// <summary>
        /// Sets the value of the element addressed by <paramref name="d0"/>...<paramref name="d3"/>. Supports deep indexing into cell elements. 
        /// </summary>
        /// <param name="value">The new value.</param>
        /// <param name="A">This mutable array.</param>
        /// <param name="d0">Index into dimension #0.</param>
        /// <param name="d1">Index into dimension #1.</param>
        /// <param name="d2">Index into dimension #2.</param>
        /// <param name="d3">Index into dimension #3.</param>
        /// <remarks>
        /// <para>This function replaces a single value of <paramref name="A"/> at the position as specified by 
        /// the dimensional indices <paramref name="d0"/> ... <paramref name="d3"/>.</para>
        /// <para><paramref name="d3"/> is considered a <b>sequential</b> index into its own and into subsequent, merged 
        /// dimensions (Matlab/ILNumericsV4 indexing style).</para>
        /// <para>Deep indexing: the element to be replaced is located by matching the full set of given indices with 
        /// the existing dimensions of the cell, starting with index <paramref name="d0"/>. If the indices corresponding 
        /// to the number of dimensions of this cell point to an ILNumerics array subsequent indices are used as indices into that array. On the 
        /// other hand, if the element found is a cell, indexing is continued recursively at that cell element.</para>
        /// <para>The function supports automatic expansion for existing dimensions of regular (non-cell) arrays. When 
        /// the given indices address a non-existing element of a non-cell array the array currently stored in the cell 
        /// is expanded in a way that the new value can be stored at the provided location of the array.</para>
        /// <para>Removal of parts of the array is not supported. Use the indexers 
        /// defined on <see cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}"/> for removal operations 
        /// in array style <see cref="ArrayStyles.ILNumericsV4"/>. It is recommended to initialize your arrays with a fixed 
        /// size and to prevent from array size changing operations for best performance, though.</para>
        /// </remarks>
        /// <exception cref="OutOfMemoryException"> if the expanded size as determined by the given indices is 
        /// too large to be allocated.</exception>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long)"/>
        /// <seealso href="https://ilnumerics.net/cell-arrays.html">ILNumerics cell arrays tutorial online.</seealso>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html">Subarray tutorial in the ILNumerics online documentation</seealso>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        public static void SetValue(this Mutable<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, BaseArray value,
                long d0, long d1, long d2, long d3) {

            lock (A.SynchObj) {
                A.Storage.Assign((A.Storage as IStorage).SetCellContentDirect(value, _params<long>.Get(d0, d1, d2, d3), 0, allowExpand: true) as CellStorage);
            }
        }

        /// <summary>
        /// Sets the value of the element addressed by <paramref name="d0"/>...<paramref name="d4"/>. Supports deep indexing into cell elements. 
        /// </summary>
        /// <param name="value">The new value.</param>
        /// <param name="A">This mutable array.</param>
        /// <param name="d0">Index into dimension #0.</param>
        /// <param name="d1">Index into dimension #1.</param>
        /// <param name="d2">Index into dimension #2.</param>
        /// <param name="d3">Index into dimension #3.</param>
        /// <param name="d4">Index into dimension #4.</param>
        /// <remarks>
        /// <para>This function replaces a single value of <paramref name="A"/> at the position as specified by 
        /// the dimensional indices <paramref name="d0"/> ... <paramref name="d4"/>.</para>
        /// <para><paramref name="d4"/> is considered a <b>sequential</b> index into its own and into subsequent, merged 
        /// dimensions (Matlab/ILNumericsV4 indexing style).</para>
        /// <para>Deep indexing: the element to be replaced is located by matching the full set of given indices with 
        /// the existing dimensions of the cell, starting with index <paramref name="d0"/>. If the indices corresponding 
        /// to the number of dimensions of this cell point to an ILNumerics array subsequent indices are used as indices into that array. On the 
        /// other hand, if the element found is a cell, indexing is continued recursively at that cell element.</para>
        /// <para>The function supports automatic expansion for existing dimensions of regular (non-cell) arrays. When 
        /// the given indices address a non-existing element of a non-cell array the array currently stored in the cell 
        /// is expanded in a way that the new value can be stored at the provided location of the array.</para>
        /// <para>Removal of parts of the array is not supported. Use the indexers 
        /// defined on <see cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}"/> for removal operations 
        /// in array style <see cref="ArrayStyles.ILNumericsV4"/>. It is recommended to initialize your arrays with a fixed 
        /// size and to prevent from array size changing operations for best performance, though.</para>
        /// </remarks>
        /// <exception cref="OutOfMemoryException"> if the expanded size as determined by the given indices is 
        /// too large to be allocated.</exception>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long)"/>
        /// <seealso href="https://ilnumerics.net/cell-arrays.html">ILNumerics cell arrays tutorial online.</seealso>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html">Subarray tutorial in the ILNumerics online documentation</seealso>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        public static void SetValue(this Mutable<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, BaseArray value,
                long d0, long d1, long d2, long d3, long d4) {

            lock (A.SynchObj) {
                A.Storage.Assign((A.Storage as IStorage).SetCellContentDirect(value, _params<long>.Get(d0, d1, d2, d3, d4), 0, allowExpand: true) as CellStorage);
            }
        }

        /// <summary>
        /// Sets the value of the element addressed by <paramref name="d0"/>...<paramref name="d5"/>. Supports deep indexing into cell elements. 
        /// </summary>
        /// <param name="value">The new value.</param>
        /// <param name="A">This mutable array.</param>
        /// <param name="d0">Index into dimension #0.</param>
        /// <param name="d1">Index into dimension #1.</param>
        /// <param name="d2">Index into dimension #2.</param>
        /// <param name="d3">Index into dimension #3.</param>
        /// <param name="d4">Index into dimension #4.</param>
        /// <param name="d5">Index into dimension #5.</param>
        /// <remarks>
        /// <para>This function replaces a single value of <paramref name="A"/> at the position as specified by 
        /// the dimensional indices <paramref name="d0"/> ... <paramref name="d5"/>.</para>
        /// <para><paramref name="d5"/> is considered a <b>sequential</b> index into its own and into subsequent, merged 
        /// dimensions (Matlab/ILNumericsV4 indexing style).</para>
        /// <para>Deep indexing: the element to be replaced is located by matching the full set of given indices with 
        /// the existing dimensions of the cell, starting with index <paramref name="d0"/>. If the indices corresponding 
        /// to the number of dimensions of this cell point to an ILNumerics array subsequent indices are used as indices into that array. On the 
        /// other hand, if the element found is a cell, indexing is continued recursively at that cell element.</para>
        /// <para>The function supports automatic expansion for existing dimensions of regular (non-cell) arrays. When 
        /// the given indices address a non-existing element of a non-cell array the array currently stored in the cell 
        /// is expanded in a way that the new value can be stored at the provided location of the array.</para>
        /// <para>Removal of parts of the array is not supported. Use the indexers 
        /// defined on <see cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}"/> for removal operations 
        /// in array style <see cref="ArrayStyles.ILNumericsV4"/>. It is recommended to initialize your arrays with a fixed 
        /// size and to prevent from array size changing operations for best performance, though.</para>
        /// </remarks>
        /// <exception cref="OutOfMemoryException"> if the expanded size as determined by the given indices is 
        /// too large to be allocated.</exception>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long)"/>
        /// <seealso href="https://ilnumerics.net/cell-arrays.html">ILNumerics cell arrays tutorial online.</seealso>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html">Subarray tutorial in the ILNumerics online documentation</seealso>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        public static void SetValue(this Mutable<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, BaseArray value,
                long d0, long d1, long d2, long d3, long d4, long d5) {

            lock (A.SynchObj) {
                A.Storage.Assign((A.Storage as IStorage).SetCellContentDirect(value, _params<long>.Get(d0, d1, d2, d3, d4, d5), 0, allowExpand: true) as CellStorage);
            }
        }

        /// <summary>
        /// Sets the value of the element addressed by <paramref name="d0"/>...<paramref name="d6"/>. Supports deep indexing into cell elements.
        /// </summary>
        /// <param name="value">The new value.</param>
        /// <param name="A">This mutable array.</param>
        /// <param name="d0">Index into dimension #0.</param>
        /// <param name="d1">Index into dimension #1.</param>
        /// <param name="d2">Index into dimension #2.</param>
        /// <param name="d3">Index into dimension #3.</param>
        /// <param name="d4">Index into dimension #4.</param>
        /// <param name="d5">Index into dimension #5.</param>
        /// <param name="d6">Index into dimension #6.</param>
        /// <remarks>
        /// <para>This function replaces a single value of <paramref name="A"/> at the position as specified by 
        /// the dimensional index <paramref name="d6"/>.</para>
        /// <para><paramref name="d6"/> is considered a <b>sequential</b> index into its own and into subsequent, merged 
        /// dimensions (Matlab/ILNumericsV4 indexing style).</para>
        /// <para>Deep indexing: the element to be replaced is located by matching the full set of given indices with 
        /// the existing dimensions of the cell, starting with index <paramref name="d0"/>. If the indices corresponding 
        /// to the number of dimensions of this cell point to an ILNumerics array subsequent indices are used as indices into that array. On the 
        /// other hand, if the element found is a cell, indexing is continued recursively at that cell element.</para>
        /// <para>The function supports automatic expansion for existing dimensions of regular (non-cell) arrays. When 
        /// the given indices address a non-existing element of a non-cell array the array currently stored in the cell 
        /// is expanded in a way that the new value can be stored at the provided location of the array.</para>
        /// <para>Removal of parts of the array is not supported. Use the indexers 
        /// defined on <see cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}"/> for removal operations 
        /// in array style <see cref="ArrayStyles.ILNumericsV4"/>. It is recommended to initialize your arrays with a fixed 
        /// size and to prevent from array size changing operations for best performance, though.</para>
        /// </remarks>
        /// <exception cref="OutOfMemoryException"> if the expanded size as determined by the given indices is 
        /// too large to be allocated.</exception>
        /// <seealso cref="Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.SetRange(BaseArray{T1}, DimSpec, DimSpec)"/>
        /// <seealso cref="ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}.GetItem(long)"/>
        /// <seealso cref="ILNumerics.ExtensionMethods.GetValue(BaseArray{bool}, long)"/>
        /// <seealso href="https://ilnumerics.net/cell-arrays.html">ILNumerics cell arrays tutorial online.</seealso>
        /// <seealso href="https://ilnumerics.net/subarrays-v5.html">Subarray tutorial in the ILNumerics online documentation</seealso>
        /// <seealso cref="ILNumerics.Core.Arrays.Mutable{T1, LocalT, InT, OutT, RetT, StorageT}.GetHostPointerForWrite(StorageOrders?)">Array{T}.GetHostPointerForWrite(StorageOrders)</seealso>
        public static void SetValue(this Mutable<BaseArray, Cell, InCell, OutCell, Cell, CellStorage> A, BaseArray value,
                long d0, long d1, long d2, long d3, long d4, long d5, long d6) {

            lock (A.SynchObj) {
                A.Storage.Assign((A.Storage as IStorage).SetCellContentDirect(value, _params<long>.Get(d0, d1, d2, d3, d4, d5, d6), 0, allowExpand: true) as CellStorage);
            }
        }

        #endregion

        #endregion
    }
}
