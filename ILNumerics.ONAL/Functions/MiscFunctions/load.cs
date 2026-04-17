using System;
using System.Collections.Generic;
using System.Text;
using ILNumerics;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using static ILNumerics.Globals;
using static ILNumerics.Core.Functions.Builtin.MathInternal; 

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {

        /// <summary>
        /// Load single array from matfile file.
        /// </summary>
        /// <typeparam name="T">Element type of the array to read.</typeparam>
        /// <param name="filename">Path to the matfile.</param>
        /// <param name="arrayname">[Optional] name of the requested array in the matfile. Default: (empty) load the first array found.</param>
        /// <returns>The array read from matfile.</returns>
        /// <remarks><para>If <paramref name="arrayname"/> is ommited, the first array is returned.</para>
        /// <para>This function is based on <see cref="MatFile"/> which works with Matlab mat files version 6 only. 
        /// In order to access Matlab mat files of a newer version, use the <see cref="ILNumerics.IO.HDF5"/> API.</para>
        /// <para>The typeparameter <typeparamref name="T"/> must match the type of elements stored in the mat file.</para></remarks>
        /// <seealso cref="MatFile"/>
        /// <seealso cref="ILNumerics.IO.HDF5"/>
        internal static Array<T> loadArray<T>(string filename, string arrayname = "") {
            using (Scope.Enter()) {
                using (MatFile matfile = new MatFile(filename)) {
                    if (String.IsNullOrEmpty(arrayname) && matfile.Count > 0) {
                        return matfile.GetArray<T>(0);
                    } else {
                        return matfile.GetArray<T>(arrayname); 
                    }
                }
            }
        }

        /// <summary>
        /// Load binary data from stream.
        /// </summary>
        /// <typeparam name="T">Element type for the new array.</typeparam>
        /// <param name="stream">Input stream to read from.</param>
        /// <param name="leadDimLen">Length of the 'leading dimension', how many elements of T fit into one scanline?</param>
        /// <param name="height">Height of the area to read.</param>
        /// <param name="width">Widht of the area to read.</param>
        /// <param name="offsetWidth">[Optional] Skip that many columns. Default: 0.</param>
        /// <param name="offsetHeight">[Optional] Skip that many rows. Default: 0.</param>
        /// <param name="convertScanLine">[Optional] Function used for copying individual scanlines. Default: null (Buffer.BlockCopy). Parameters: dest*, src*, nrBytes.</param>
        /// <returns>Array with element type <typeparamref name="T"/> and given size with a binary copy of the region as read from <paramref name="stream"/>.</returns>
        internal unsafe static Array<T> loadBinary<T>(Stream stream, int leadDimLen, int height, int width, 
                                                int offsetWidth = 0, int offsetHeight = 0, 
                                                Action<IntPtr, IntPtr, long> convertScanLine = null) where T : struct {
            using (Scope.Enter()) {
                int sizeofT = System.Runtime.InteropServices.Marshal.SizeOf(typeof(T)); 
                Array<T> ret = MathInternal.zeros<T>(height,width, StorageOrders.ColumnMajor);
                byte* retP = (byte*)ret.GetHostPointerForWrite();

                if (convertScanLine == null) {
                    convertScanLine = CopyMemory; 
                }

                byte[] buffer = new byte[height * sizeofT];
                fixed (byte* bufferP = buffer) {
                    int curRetArrPos = 0;
                    int curBufLen;
                    for (int c = 0; c < width; c++) {
                        long pos = (leadDimLen * (long)(c + offsetWidth) + offsetHeight) * sizeofT;
                        stream.Seek(pos, SeekOrigin.Begin);
                        curBufLen = stream.Read(buffer, 0, buffer.Length);
                        if (curBufLen != buffer.Length)
                            throw new InvalidDataException("Could not read the file. Unexpected EOF detected. Invalid dimensions specified or stream too short?");
                        convertScanLine((IntPtr)(retP + curRetArrPos), (IntPtr)bufferP, curBufLen);
                        curRetArrPos += buffer.Length;
                    }
                }
                return ret; 
            }
        }

        private unsafe static void CopyMemory(IntPtr dest, IntPtr src, long len) {
            //Native.NativeMethods.CopyMemory(dest, src, new IntPtr(len));
            System.Buffer.MemoryCopy(
                source: (void*)src,
                destination: (void*)dest,
                sourceBytesToCopy: (long)len,
                destinationSizeInBytes: (long)len);
        }
    }
}
