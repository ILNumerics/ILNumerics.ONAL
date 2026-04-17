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
