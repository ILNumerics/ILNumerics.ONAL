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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Functions.Builtin {
    public abstract unsafe partial class BroadcastingBinaryBase<T, LocalT, InT, OutT, RetT, StorageT> 

        where StorageT : BaseStorage<T, LocalT, InT, OutT, RetT, StorageT>, new()
        where LocalT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where InT : Immutable<T, LocalT, InT, OutT, RetT, StorageT>
        where OutT : Mutable<T, LocalT, InT, OutT, RetT, StorageT>
        where RetT : Mutable<T, LocalT, InT, OutT, RetT, StorageT> {

        protected static byte saturateByte(double a) {
            if (double.IsNaN(a)) return 0;
            a = Math.Round(a, MidpointRounding.AwayFromZero);
            if (a > byte.MaxValue) return byte.MaxValue;
            if (a < byte.MinValue) return byte.MinValue;
            return (byte)a;
        }
        protected static byte saturateByte(int a) {
            if (a > byte.MaxValue) return byte.MaxValue;
            if (a < byte.MinValue) return byte.MinValue;
            return (byte)a;
        }
        protected static sbyte saturateSByte(double a) {
            if (double.IsNaN(a)) return 0;
            a = Math.Round(a, MidpointRounding.AwayFromZero);
            if (a > sbyte.MaxValue) return sbyte.MaxValue;
            if (a < sbyte.MinValue) return sbyte.MinValue;
            return (sbyte)a;
        }
        protected static char saturateChar(double a) {
            if (double.IsNaN(a)) return (char)0;
            a = Math.Round(a, MidpointRounding.AwayFromZero);
            if (a > char.MaxValue) return char.MaxValue;
            if (a < char.MinValue) return char.MinValue;
            return (char)a;
        }
        protected static short saturateInt16(double a) {
            if (double.IsNaN(a)) return 0;
            a = Math.Round(a, MidpointRounding.AwayFromZero);
            if (a > short.MaxValue) return short.MaxValue;
            if (a < short.MinValue) return short.MinValue;
            return (short)a;
        }
        protected static int saturateInt32(double a) {
            if (double.IsNaN(a)) return 0;
            a = Math.Round(a, MidpointRounding.AwayFromZero);
            if (a > int.MaxValue) return int.MaxValue;
            if (a < int.MinValue) return int.MinValue;
            return (int)a;
        }
        protected static long saturateInt64(double a) {
            if (double.IsNaN(a)) return 0;
            a = Math.Round(a, MidpointRounding.AwayFromZero);
            if (a > long.MaxValue) return long.MaxValue;
            if (a < long.MinValue) return long.MinValue;
            return (long)a;
        }
        protected static ushort saturateUInt16(double a) {
            if (double.IsNaN(a)) return 0;
            a = Math.Round(a, MidpointRounding.AwayFromZero);
            if (a > ushort.MaxValue) return ushort.MaxValue;
            if (a < ushort.MinValue) return ushort.MinValue;
            return (ushort)a;
        }
        protected static uint saturateUInt32(double a) {
            if (double.IsNaN(a)) return 0;
            a = Math.Round(a, MidpointRounding.AwayFromZero);
            if (a > uint.MaxValue) return uint.MaxValue;
            if (a < uint.MinValue) return uint.MinValue;
            return (uint)a;
        }
        protected static ulong saturateUInt64(double a) {
            if (double.IsNaN(a)) return 0;
            a = Math.Round(a, MidpointRounding.AwayFromZero);
            if (a > ulong.MaxValue) return ulong.MaxValue;
            if (a < ulong.MinValue) return ulong.MinValue;
            return (ulong)a;
        }
        #region specialized saaturation funcstions for pow(). This simply simplifies Hycalping.

        protected static byte saturateBytePow(byte a, byte b) {
            return saturateByte(Math.Pow(a, b));
        }
        protected static sbyte saturateSBytePow(sbyte a, sbyte b) {
            return saturateSByte(Math.Pow(a, b));
        }
        protected static short saturateInt16Pow(short a, short b) {
            return saturateInt16(Math.Pow(a, b));
        }
        protected static ushort saturateUInt16Pow(ushort a, ushort b) {
            return saturateUInt16(Math.Pow(a, b));
        }
        protected static int saturateInt32Pow(int a, int b) {
            return saturateInt32(Math.Pow(a, b));
        }
        protected static uint saturateUInt32Pow(uint a, uint b) {
            return saturateUInt32(Math.Pow(a, b));
        }
        protected static long saturateInt64Pow(long a, long b) {
            return saturateInt64(Math.Pow(a, b));
        }
        protected static ulong saturateUInt64Pow(ulong a, ulong b) {
            return saturateUInt64(Math.Pow(a, b));
        }
        #endregion


    }
}
