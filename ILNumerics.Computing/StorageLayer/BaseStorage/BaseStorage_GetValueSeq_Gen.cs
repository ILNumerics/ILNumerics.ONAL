//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////

using ILNumerics.Core.Arrays;
using ILNumerics.Core.MemoryLayer;
using ILNumerics.Core.Native;
using System;
using System.Security;

namespace ILNumerics.Core.StorageLayer {

    public abstract partial class BaseStorage<T, LocalT, InT, OutT, RetT, StorageT> : IDisposable, IStorage
        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

        #region GetValue, generic read access

        /// <summary>
        /// Retrieves the element at sequential position <paramref name="elementIndex"/>, assuming column major storage. 
        /// </summary>
        /// <param name="elementIndex">Linear index into the array. Matlab indexing style.</param>
        /// <returns>Copy of the value found.</returns>
        
        internal T GetValue(long elementIndex) {
            return GetValueSeq(Size.GetSeqIndex(elementIndex));
        }

        /// <summary>
        /// (Inefficiently) retrieves the element value of type <typeparamref name="T"/> at the memory position indicated by <paramref name="offset"/>.
        /// </summary>
        /// <param name="offset">Offset <i>in ELEMENTS</i> relative to the memory handle pointer <see cref="MemoryHandle.Pointer"/> to start reading the element value.</param>
        /// <returns>The value found at the position <paramref name="offset"/>.</returns>
        /// <remarks><para>This method is provided for compatibility reasons only. It is recommended to use the closed type method extensions on <see cref="ILNumerics.Core.Arrays.ConcreteArray{T1, LocalT, InT, OutT, RetT, StorageT}"/> 
        /// in order to prevent from having to copy arbitrary <typeparamref name="T"/> values.</para>
        /// <para>This method acts as a memory barrier. It waits (blocks) until all asynchronous operations <em>writing</em> to this storage have finished.</para>
        /// </remarks>
        
        internal unsafe virtual T GetValueSeq(long offset) {

            //TODO: check/ improve: implement single value access on devices instead of copying all elements? 
            // But: if one element is requested likely more will be needed soon, no?!
            //if (!m_handles.IsOnDevice(0)) {
            //    var curDevIdx = m_handles.CurrentDeviceIdx;
            //    System.Diagnostics.Debug.Assert(curDevIdx > 0);
            //    DeviceManagement.DeviceManager.GetDevice(0).EnsureBuffer(m_handles);
            //}

            var hostHandle = Handles[0];
            System.Diagnostics.Debug.Assert(hostHandle != null, $"Assertion failed: the handle at buffer slot #0 was null.");
            // m_handles.Finish();  // redundant: Handles waits for the storage state, Pointer waits for the buffer state.

            if (hostHandle is ManagedHostHandle<T>) {
                return (hostHandle as ManagedHostHandle<T>).HostArray[offset];
            } else if (hostHandle is ManagedHostHandle<IStorage>) {
                return (T)(object)((hostHandle as ManagedHostHandle<IStorage>).HostArray[offset]?.GetBaseArrayClone());
            } else {
                System.Diagnostics.Debug.Assert(hostHandle is NativeHostHandle, $"Assertion failed: expected NativeHostHandle. Found: {hostHandle.GetType().Name}");
                System.Diagnostics.Debug.Assert((hostHandle as NativeHostHandle).m_referenceCount > 0, $"Invalid host handle: ref.count = {(hostHandle as NativeHostHandle).m_referenceCount}");
                byte* p = (byte*)hostHandle.Pointer;
                if (p != (byte*)0) {
                    System.Diagnostics.Debug.Assert((hostHandle as NativeHostHandle).m_referenceCount > 0, $"Invalid host handle: ref.count = {(hostHandle as NativeHostHandle).m_referenceCount}");
                    return System.Runtime.CompilerServices.Unsafe.Read<T>(p + offset * SizeOfT); 
                    //System.TypedReference tr = __makeref(ret);
                    //*(IntPtr*)(&tr) = (IntPtr)(p + offset * SizeOfT);
                    //return __refvalue(tr, T);
                } else {
                    throw new InvalidOperationException($"The element type {typeof(T).Name} is unknown.");
                }
            }
            //// This was the 1st attempt, kept here for reference. It is not too bad, but uses a local array wrapper and a new GCHandle. 
            //// If need arises: long-term fix the PinBox to save the GCHandle!

            //GCHandle retHandle = GCHandle.Alloc(PinBox, GCHandleType.Pinned);
            //try {
            //    switch (SizeOfT) {
            //        case 1:
            //            *((byte*)retHandle.AddrOfPinnedObject()) = *((byte*)m_handles[0].Pointer + offset);
            //            break;
            //        case 2:
            //            *((short*)retHandle.AddrOfPinnedObject()) = *((short*)m_handles[0].Pointer + offset);
            //            break;
            //        case 4:
            //            *((int*)retHandle.AddrOfPinnedObject()) = *((int*)m_handles[0].Pointer + offset);
            //            break;
            //        case 8:
            //            *((long*)retHandle.AddrOfPinnedObject()) = *((long*)m_handles[0].Pointer + offset);
            //            break;
            //        default:
            //            break;
            //    }
            //    return PinBox[0]; 
            //} finally {
            //    if (retHandle.IsAllocated) {
            //        retHandle.Free();
            //    }
            //}
        }

        #endregion
    }
}
