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
using ILNumerics.Core.MemoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {

        /// <summary>
        /// Provides memory / T[] / a device buffer for the current thread. This is for expert users and rare low-level scenarios only. Use the common array creation functions instead! 
        /// </summary>
        /// <typeparam name="T">Element type, specifies the type and size of elements.</typeparam>
        /// <param name="elementCount">Number of elements which must at least fit into the memory region.</param>
        /// <param name="deviceIndex">[Optional] Index of the device owning the memory. Default: 0 is the host device.</param>
        /// <param name="clear">[Optional] Flag determining whether the memory should be initialized to zero. Default: do not clear.</param>
        /// <returns>The <see cref="MemoryHandle"/> returned will be a <see cref="NativeHostHandle"/> to a region on the main (virtual) memory,
        /// a <see cref="ManagedHostHandle{T}"/> to the managed heap or a <see cref="Buffer"/> on an OpenCL device.</returns>
        /// <remarks>
        /// <para>This function attempts to acquire the requested chunk of memory from the memory pool of the specified device. Only if no matching region was found, memory is allocated from the low level 
        /// allocation functions offered by the device.</para>
        /// <para>This function supports the utilization of memory regions in a C style manner: user have to manage memory explicitly and manually. 
        /// Instead of using this function it is recommended to use the common ILNumerics array creation functions to create and work with ILNumerics arrays. 
        /// which do not require to create, clean up or to pool memory manually. ILNumerics <see href="/FunctionRules.html">function rules</see> do all this automatically. Thus, 
        /// manual memory management is left to a few, very specific situations only.</para>
        /// <para>Depending on the type category the memory handle returned references one of the following memory types:
        /// <list type="number">
        /// <item>T is struct / ValueType: memory is allocated on the unmanaged heap. There is no inherent size limitation. Access to the memory is done 
        /// by means of the <see cref="MemoryHandle.Pointer"/> property. Users are responsible to correctly deal with such pointers. All risks of unsafe code apply.</item>
        /// <item>T is class / reference type: the function allocates an .NET <see cref="System.Array"/> on the managed heap. Access is done by first casting the memory handle 
        /// returned to the concrete type of <see cref="ManagedHostHandle{T}"/> and then using the <see cref="ManagedHostHandle{T}.HostArray"/> property to acquire the T[] array.</item>
        /// <item>if <paramref name="deviceIndex"/> is greater than 0 the handle returned may be a buffer to an OpenCL device. For shared 
        /// virtual memory devices (SVM), however, such buffers will be backed with a corresponding region of memory on the native virtual memory manager managed heap. It is this complexity
        /// which leads to the suggestion to not use this method of manual memory management ('New' and 'free') unless really necessary and unless you know what you are doing.</item>
        /// </list></para>
        /// <para>Attempting to access the <see cref="ManagedHostHandle{T}.HostArray"/> on a native handle generates an exception.</para>
        /// <para>Attempting to access the <see cref="MemoryHandle.Pointer"/> on a ManagedHostHandle{T} generates an exception.</para>
        /// <para>MemoryHandles are critical handles. Disposal is guaranteed by critical execution regions and the GC. However, users should 
        /// dispose the handle to free the memory region immediately after use. Use <see cref="free{T}"/> for disposing memory handles.</para>
        /// </remarks>
        /// <seealso cref="free{T}"/>
        /// <seealso cref="DeviceManagement.DeviceManager.GetDevices"/>
        /// <exception cref="OutOfMemoryException"> on failed attempts to allocate a memory region on the specified device.</exception>
        internal static MemoryHandle New<T>(ulong elementCount, uint deviceIndex = 0, bool clear = false) {
            return DeviceManagement.DeviceManager.GetDevice(deviceIndex).New<T>(elementCount, clear);
        }
        /// <summary>
        /// Provides memory / T[] / device buffer to the current thread. This is for expert users and rare low-level scenarios only. Use the common array creation functions instead! 
        /// </summary>
        /// <typeparam name="T">Element type, specifies the type and size of elements.</typeparam>
        /// <param name="elementCount">Number of elements which must at least fit into the memory region.</param>
        /// <param name="deviceIndex">[Optional] Index of the device owning the memory. Default: 0 is the host device.</param>
        /// <param name="clear">[Optional] Flag determining whether the memory should be initialized to zero. Default: do not clear.</param>
        /// <returns>The <see cref="MemoryHandle"/> returned will be a <see cref="NativeHostHandle"/> to a region on the main (virtual) memory,
        /// a <see cref="ManagedHostHandle{T}"/> to the managed heap or a <see cref="Buffer"/> on an OpenCL device.</returns>
        /// <remarks>
        /// <para>This function attempts to acquire the requested chunk of memory from the memory pool of the specified device. Only if no matching region was found, memory is allocated from the low level 
        /// allocation functions offered by the device.</para>
        /// <para>This function supports the utilization of memory regions in a C style manner: user have to manage memory explicitly and manually. 
        /// Instead of using this function it is recommended to use the common ILNumerics array creation functions to create and work with ILNumerics arrays. 
        /// which do not require to create, clean up or to pool memory manually. ILNumerics <see href="/FunctionRules.html">function rules</see> do all this automatically. Thus, 
        /// manual memory management is left to a few, very specific situations only.</para>
        /// <para>Depending on the type category the memory handle returned references one of the following memory types:
        /// <list type="number">
        /// <item>T is struct / ValueType: memory is allocated on the unmanaged heap. There is no inherent size limitation. Access to the memory is done 
        /// by means of the <see cref="MemoryHandle.Pointer"/> property. Users are responsible to correctly deal with such pointers. All risks of unsafe code apply.</item>
        /// <item>T is class / reference type: the function allocates an .NET <see cref="System.Array"/> on the managed heap. Access is done by first casting the memory handle 
        /// returned to the concrete type of <see cref="ManagedHostHandle{T}"/> and then using the <see cref="ManagedHostHandle{T}.HostArray"/> property to acquire the T[] array.</item>
        /// <item>if <paramref name="deviceIndex"/> is greater than 0 the handle returned may be a buffer to an OpenCL device. For shared 
        /// virtual memory devices (SVM), however, such buffers will be backed with a corresponding region of memory on the native virtual memory manager managed heap. It is this complexity
        /// which leads to the suggestion to not use this method of manual memory management ('New' and 'free') unless really necessary and unless you know what you are doing.</item>
        /// </list></para>
        /// <para>Attempting to access the <see cref="ManagedHostHandle{T}.HostArray"/> on a native handle generates an exception.</para>
        /// <para>Attempting to access the <see cref="MemoryHandle.Pointer"/> on a ManagedHostHandle{T} generates an exception.</para>
        /// <para>MemoryHandles are critical handles. Disposal is guaranteed by critical execution regions and the GC. However, users should 
        /// dispose the handle to free the memory region immediately after use. Use <see cref="free{T}"/> for disposing memory handles.</para>
        /// </remarks>
        /// <seealso cref="free{T}"/>
        /// <seealso cref="DeviceManagement.DeviceManager.GetDevices"/>
        /// <exception cref="OutOfMemoryException"> on failed attempts to allocate a memory region on the specified device.</exception>
        internal static MemoryHandle New<T>(long elementCount, uint deviceIndex = 0, bool clear = false) {
            return DeviceManagement.DeviceManager.GetDevice(deviceIndex).New<T>((ulong)elementCount, clear);
        }
        /// <summary>
        /// Frees the handle for a memory region after use. This is for expert users in rare low-level scenarios only. Use the common array creation functions instead!
        /// </summary>
        /// <typeparam name="T">Element type used to acquire the handle.</typeparam>
        /// <param name="handle">The handle pointing to the allocated memory region.</param>
        /// <param name="nocache">[Optional] Determines whether the memory is being returned to the OS. Default: false - the memory is cached into a pool for quick reusing.</param>
        /// <param name="deviceIndex">The index of the device where the handles memory lives on. This must be the same device as the one used during allocation.</param>
        /// <remarks><para>After having acquired the handle of a memory region on a device and having finished working with the memory make sure to release the memory 
        /// back to the ILNumerics memory manager.</para></remarks>
        /// <seealso cref="New{T}(ulong, uint, bool)"/>
        internal static void free<T>(MemoryHandle handle, uint deviceIndex, bool nocache = false) {
            if (nocache) {
                handle?.Dispose(); 
            } else {
                var device = DeviceManagement.DeviceManager.GetDevice(deviceIndex); 
                handle?.Cache(device.MemoryPool);
            }
        }
    }
}
