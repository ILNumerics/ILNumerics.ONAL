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
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Native {

    /// <summary>
    /// Static helper class for PInvoke definitions of native function imports.
    /// </summary>
    internal static unsafe class NativeMethods {

        //[DllImport("kernel32", EntryPoint = "LocalAlloc")]
        //[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        //[SuppressUnmanagedCodeSecurity]
        //internal static extern NativeHostHandle LocalAlloc(int uFlags, UIntPtr sizetdwBytes);

        //[DllImport("kernel32", SetLastError = true)]
        //[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        //[SuppressUnmanagedCodeSecurity]
        //internal static extern IntPtr LocalFree(IntPtr handle);

        [DllImport("kernel32", SetLastError = true), SuppressUnmanagedCodeSecurity]
        internal static extern IntPtr CreateMemoryResourceNotification(MemoryResourceNotificationType notificationType);

        [DllImport("kernel32.dll", SetLastError = true), SuppressUnmanagedCodeSecurity]
        internal static extern int QueryMemoryResourceNotification(IntPtr resourceNotificationHandle, out int resourceState);

        //[DllImport("msvcrt", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        //public static extern IntPtr CopyMemory(IntPtr dest, IntPtr source, IntPtr length);

        //[DllImport("msvcrt", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        //public static extern IntPtr CopyMemory(byte* dest, byte* source, ulong length);

        //[DllImport("kernel32", EntryPoint = "RtlCopyMemory")]
        //public static unsafe extern void CopyMemory(void* dest, void* source, UIntPtr length);

        internal enum MemoryResourceNotificationType : int {
            LowMemoryResourceNotification = 0,
            HighMemoryResourceNotification = 1,
        }
    }

}
