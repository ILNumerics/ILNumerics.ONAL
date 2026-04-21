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
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics {
    /// <summary>
    /// Helper class, realizes an efficient params[] API for stack storage for segmented code.
    /// </summary>
    /// <typeparam name="T">unmanged struct element type</typeparam>
    
    public sealed unsafe class _params<T> where T : unmanaged {

        [ThreadStatic]
        static MemoryHandle s_buffer;

        static T* Buffer {
            
            get {
                if (s_buffer == null) {
                    s_buffer = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<T>(32);
                }
                return (T*)s_buffer.Pointer;
            }
        }
        
        public static Span<T> Get(T p0) {
            var ret = Buffer;
            ret[0] = p0;
            return new Span<T>(ret, 1);
        }
        
        public static Span<T> Get(T p0, T p1) {
            var ret = Buffer;
            ret[0] = p0; ret[1] = p1;
            return new Span<T>(ret, 2);
        }
        
        public static Span<T> Get(T p0, T p1, T p2) {
            var ret = Buffer;
            ret[0] = p0; ret[1] = p1; ret[2] = p2;
            return new Span<T>(ret, 3);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3) {
            var ret = Buffer;
            ret[0] = p0; ret[1] = p1; ret[2] = p2; ret[3] = p3;
            return new Span<T>(ret, 4);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4) {
            var ret = Buffer;
            ret[0] = p0; ret[1] = p1; ret[2] = p2; ret[3] = p3; ret[4] = p4;
            return new Span<T>(ret, 5);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5) {
            var ret = Buffer;
            ret[0] = p0; ret[1] = p1; ret[2] = p2; ret[3] = p3; ret[4] = p4; ret[5] = p5;
            return new Span<T>(ret, 6);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5, T p6) {
            var ret = Buffer;
            ret[0] = p0; ret[1] = p1; ret[2] = p2; ret[3] = p3; ret[4] = p4; ret[5] = p5; ret[6] = p6;
            return new Span<T>(ret, 7);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5, T p6, T p7) {
            var ret = Buffer;
            ret[0] = p0;
            ret[1] = p1;
            ret[2] = p2;
            ret[3] = p3;
            ret[4] = p4;
            ret[5] = p5;
            ret[6] = p6;
            ret[7] = p7;
            return new Span<T>(ret, 8);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5, T p6, T p7, T p8) {
            var ret = Buffer;
            ret[0] = p0;
            ret[1] = p1;
            ret[2] = p2;
            ret[3] = p3;
            ret[4] = p4;
            ret[5] = p5;
            ret[6] = p6;
            ret[7] = p7;
            ret[8] = p8;
            return new Span<T>(ret, 9);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5, T p6, T p7, T p8, T p9) {
            var ret = Buffer;
            ret[0] = p0;
            ret[1] = p1;
            ret[2] = p2;
            ret[3] = p3;
            ret[4] = p4;
            ret[5] = p5;
            ret[6] = p6;
            ret[7] = p7;
            ret[8] = p8;
            ret[9] = p9;
            return new Span<T>(ret, 10);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5, T p6, T p7, T p8, T p9,
                      T p10) {
            var ret = Buffer;
            ret[0] = p0;
            ret[1] = p1;
            ret[2] = p2;
            ret[3] = p3;
            ret[4] = p4;
            ret[5] = p5;
            ret[6] = p6;
            ret[7] = p7;
            ret[8] = p8;
            ret[9] = p9;
            ret[10] = p10;
            return new Span<T>(ret, 11);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5, T p6, T p7, T p8, T p9,
                      T p10, T p11) {
            var ret = Buffer;
            ret[0] = p0;
            ret[1] = p1;
            ret[2] = p2;
            ret[3] = p3;
            ret[4] = p4;
            ret[5] = p5;
            ret[6] = p6;
            ret[7] = p7;
            ret[8] = p8;
            ret[9] = p9;
            ret[10] = p10;
            ret[11] = p11;
            return new Span<T>(ret, 12);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5, T p6, T p7, T p8, T p9,
                      T p10, T p11, T p12) {
            var ret = Buffer;
            ret[0] = p0;
            ret[1] = p1;
            ret[2] = p2;
            ret[3] = p3;
            ret[4] = p4;
            ret[5] = p5;
            ret[6] = p6;
            ret[7] = p7;
            ret[8] = p8;
            ret[9] = p9;
            ret[10] = p10;
            ret[11] = p11;
            ret[12] = p12;
            return new Span<T>(ret, 13);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5, T p6, T p7, T p8, T p9,
                      T p10, T p11, T p12, T p13) {
            var ret = Buffer;
            ret[0] = p0;
            ret[1] = p1;
            ret[2] = p2;
            ret[3] = p3;
            ret[4] = p4;
            ret[5] = p5;
            ret[6] = p6;
            ret[7] = p7;
            ret[8] = p8;
            ret[9] = p9;
            ret[10] = p10;
            ret[11] = p11;
            ret[12] = p12;
            ret[13] = p13;
            return new Span<T>(ret, 14);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5, T p6, T p7, T p8, T p9,
                      T p10, T p11, T p12, T p13, T p14) {
            var ret = Buffer;
            ret[0] = p0;
            ret[1] = p1;
            ret[2] = p2;
            ret[3] = p3;
            ret[4] = p4;
            ret[5] = p5;
            ret[6] = p6;
            ret[7] = p7;
            ret[8] = p8;
            ret[9] = p9;
            ret[10] = p10;
            ret[11] = p11;
            ret[12] = p12;
            ret[13] = p13;
            ret[14] = p14;
            return new Span<T>(ret, 15);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5, T p6, T p7, T p8, T p9,
                      T p10, T p11, T p12, T p13, T p14, T p15) {
            var ret = Buffer;
            ret[0] = p0;
            ret[1] = p1;
            ret[2] = p2;
            ret[3] = p3;
            ret[4] = p4;
            ret[5] = p5;
            ret[6] = p6;
            ret[7] = p7;
            ret[8] = p8;
            ret[9] = p9;
            ret[10] = p10;
            ret[11] = p11;
            ret[12] = p12;
            ret[13] = p13;
            ret[14] = p14;
            ret[15] = p15;
            return new Span<T>(ret, 16);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5, T p6, T p7, T p8, T p9,
                      T p10, T p11, T p12, T p13, T p14, T p15, T p16) {
            var ret = Buffer;
            ret[0] = p0;
            ret[1] = p1;
            ret[2] = p2;
            ret[3] = p3;
            ret[4] = p4;
            ret[5] = p5;
            ret[6] = p6;
            ret[7] = p7;
            ret[8] = p8;
            ret[9] = p9;
            ret[10] = p10;
            ret[11] = p11;
            ret[12] = p12;
            ret[13] = p13;
            ret[14] = p14;
            ret[15] = p15;
            ret[16] = p16;
            return new Span<T>(ret, 17);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5, T p6, T p7, T p8, T p9,
                      T p10, T p11, T p12, T p13, T p14, T p15, T p16, T p17) {
            var ret = Buffer;
            ret[0] = p0;
            ret[1] = p1;
            ret[2] = p2;
            ret[3] = p3;
            ret[4] = p4;
            ret[5] = p5;
            ret[6] = p6;
            ret[7] = p7;
            ret[8] = p8;
            ret[9] = p9;
            ret[10] = p10;
            ret[11] = p11;
            ret[12] = p12;
            ret[13] = p13;
            ret[14] = p14;
            ret[15] = p15;
            ret[16] = p16;
            ret[17] = p17;
            return new Span<T>(ret, 18);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5, T p6, T p7, T p8, T p9,
                      T p10, T p11, T p12, T p13, T p14, T p15, T p16, T p17, T p18) {
            var ret = Buffer;
            ret[0] = p0;
            ret[1] = p1;
            ret[2] = p2;
            ret[3] = p3;
            ret[4] = p4;
            ret[5] = p5;
            ret[6] = p6;
            ret[7] = p7;
            ret[8] = p8;
            ret[9] = p9;
            ret[10] = p10;
            ret[11] = p11;
            ret[12] = p12;
            ret[13] = p13;
            ret[14] = p14;
            ret[15] = p15;
            ret[16] = p16;
            ret[17] = p17;
            ret[18] = p18;
            return new Span<T>(ret, 19);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5, T p6, T p7, T p8, T p9,
                      T p10, T p11, T p12, T p13, T p14, T p15, T p16, T p17, T p18, T p19) {
            var ret = Buffer;
            ret[0] = p0;
            ret[1] = p1;
            ret[2] = p2;
            ret[3] = p3;
            ret[4] = p4;
            ret[5] = p5;
            ret[6] = p6;
            ret[7] = p7;
            ret[8] = p8;
            ret[9] = p9;
            ret[10] = p10;
            ret[11] = p11;
            ret[12] = p12;
            ret[13] = p13;
            ret[14] = p14;
            ret[15] = p15;
            ret[16] = p16;
            ret[17] = p17;
            ret[18] = p18;
            ret[19] = p19;
            return new Span<T>(ret, 20);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5, T p6, T p7, T p8, T p9,
                      T p10, T p11, T p12, T p13, T p14, T p15, T p16, T p17, T p18, T p19,
                      T p20) {
            var ret = Buffer;
            ret[0] = p0;
            ret[1] = p1;
            ret[2] = p2;
            ret[3] = p3;
            ret[4] = p4;
            ret[5] = p5;
            ret[6] = p6;
            ret[7] = p7;
            ret[8] = p8;
            ret[9] = p9;
            ret[10] = p10;
            ret[11] = p11;
            ret[12] = p12;
            ret[13] = p13;
            ret[14] = p14;
            ret[15] = p15;
            ret[16] = p16;
            ret[17] = p17;
            ret[18] = p18;
            ret[19] = p19;
            ret[20] = p20;
            return new Span<T>(ret, 21);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5, T p6, T p7, T p8, T p9,
                      T p10, T p11, T p12, T p13, T p14, T p15, T p16, T p17, T p18, T p19,
                      T p20, T p21) {
            var ret = Buffer;
            ret[0] = p0;
            ret[1] = p1;
            ret[2] = p2;
            ret[3] = p3;
            ret[4] = p4;
            ret[5] = p5;
            ret[6] = p6;
            ret[7] = p7;
            ret[8] = p8;
            ret[9] = p9;
            ret[10] = p10;
            ret[11] = p11;
            ret[12] = p12;
            ret[13] = p13;
            ret[14] = p14;
            ret[15] = p15;
            ret[16] = p16;
            ret[17] = p17;
            ret[18] = p18;
            ret[19] = p19;
            ret[20] = p20;
            ret[21] = p21;
            return new Span<T>(ret, 22);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5, T p6, T p7, T p8, T p9,
                      T p10, T p11, T p12, T p13, T p14, T p15, T p16, T p17, T p18, T p19,
                      T p20, T p21, T p22) {
            var ret = Buffer;
            ret[0] = p0;
            ret[1] = p1;
            ret[2] = p2;
            ret[3] = p3;
            ret[4] = p4;
            ret[5] = p5;
            ret[6] = p6;
            ret[7] = p7;
            ret[8] = p8;
            ret[9] = p9;
            ret[10] = p10;
            ret[11] = p11;
            ret[12] = p12;
            ret[13] = p13;
            ret[14] = p14;
            ret[15] = p15;
            ret[16] = p16;
            ret[17] = p17;
            ret[18] = p18;
            ret[19] = p19;
            ret[20] = p20;
            ret[21] = p21;
            ret[22] = p22;
            return new Span<T>(ret, 23);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5, T p6, T p7, T p8, T p9,
                      T p10, T p11, T p12, T p13, T p14, T p15, T p16, T p17, T p18, T p19,
                      T p20, T p21, T p22, T p23) {
            var ret = Buffer;
            ret[0] = p0;
            ret[1] = p1;
            ret[2] = p2;
            ret[3] = p3;
            ret[4] = p4;
            ret[5] = p5;
            ret[6] = p6;
            ret[7] = p7;
            ret[8] = p8;
            ret[9] = p9;
            ret[10] = p10;
            ret[11] = p11;
            ret[12] = p12;
            ret[13] = p13;
            ret[14] = p14;
            ret[15] = p15;
            ret[16] = p16;
            ret[17] = p17;
            ret[18] = p18;
            ret[19] = p19;
            ret[20] = p20;
            ret[21] = p21;
            ret[22] = p22;
            ret[23] = p23;
            return new Span<T>(ret, 24);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5, T p6, T p7, T p8, T p9,
                      T p10, T p11, T p12, T p13, T p14, T p15, T p16, T p17, T p18, T p19,
                      T p20, T p21, T p22, T p23, T p24) {
            var ret = Buffer;
            ret[0] = p0;
            ret[1] = p1;
            ret[2] = p2;
            ret[3] = p3;
            ret[4] = p4;
            ret[5] = p5;
            ret[6] = p6;
            ret[7] = p7;
            ret[8] = p8;
            ret[9] = p9;
            ret[10] = p10;
            ret[11] = p11;
            ret[12] = p12;
            ret[13] = p13;
            ret[14] = p14;
            ret[15] = p15;
            ret[16] = p16;
            ret[17] = p17;
            ret[18] = p18;
            ret[19] = p19;
            ret[20] = p20;
            ret[21] = p21;
            ret[22] = p22;
            ret[23] = p23;
            ret[24] = p24;
            return new Span<T>(ret, 25);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5, T p6, T p7, T p8, T p9,
                      T p10, T p11, T p12, T p13, T p14, T p15, T p16, T p17, T p18, T p19,
                      T p20, T p21, T p22, T p23, T p24, T p25) {
            var ret = Buffer;
            ret[0] = p0;
            ret[1] = p1;
            ret[2] = p2;
            ret[3] = p3;
            ret[4] = p4;
            ret[5] = p5;
            ret[6] = p6;
            ret[7] = p7;
            ret[8] = p8;
            ret[9] = p9;
            ret[10] = p10;
            ret[11] = p11;
            ret[12] = p12;
            ret[13] = p13;
            ret[14] = p14;
            ret[15] = p15;
            ret[16] = p16;
            ret[17] = p17;
            ret[18] = p18;
            ret[19] = p19;
            ret[20] = p20;
            ret[21] = p21;
            ret[22] = p22;
            ret[23] = p23;
            ret[24] = p24;
            ret[25] = p25;
            return new Span<T>(ret, 26);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5, T p6, T p7, T p8, T p9,
                      T p10, T p11, T p12, T p13, T p14, T p15, T p16, T p17, T p18, T p19,
                      T p20, T p21, T p22, T p23, T p24, T p25, T p26) {
            var ret = Buffer;
            ret[0] = p0;
            ret[1] = p1;
            ret[2] = p2;
            ret[3] = p3;
            ret[4] = p4;
            ret[5] = p5;
            ret[6] = p6;
            ret[7] = p7;
            ret[8] = p8;
            ret[9] = p9;
            ret[0] = p10;
            ret[11] = p11;
            ret[12] = p12;
            ret[13] = p13;
            ret[14] = p14;
            ret[15] = p15;
            ret[16] = p16;
            ret[17] = p17;
            ret[18] = p18;
            ret[19] = p19;
            ret[20] = p20;
            ret[21] = p21;
            ret[22] = p22;
            ret[23] = p23;
            ret[24] = p24;
            ret[25] = p25;
            ret[26] = p26;
            return new Span<T>(ret, 27);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5, T p6, T p7, T p8, T p9,
                      T p10, T p11, T p12, T p13, T p14, T p15, T p16, T p17, T p18, T p19,
                      T p20, T p21, T p22, T p23, T p24, T p25, T p26, T p27) {
            var ret = Buffer;
            ret[0] = p0;
            ret[1] = p1;
            ret[2] = p2;
            ret[3] = p3;
            ret[4] = p4;
            ret[5] = p5;
            ret[6] = p6;
            ret[7] = p7;
            ret[8] = p8;
            ret[9] = p9;
            ret[10] = p10;
            ret[11] = p11;
            ret[12] = p12;
            ret[13] = p13;
            ret[14] = p14;
            ret[15] = p15;
            ret[16] = p16;
            ret[17] = p17;
            ret[18] = p18;
            ret[19] = p19;
            ret[20] = p20;
            ret[21] = p21;
            ret[22] = p22;
            ret[23] = p23;
            ret[24] = p24;
            ret[25] = p25;
            ret[26] = p26;
            ret[27] = p27;
            return new Span<T>(ret, 28);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5, T p6, T p7, T p8, T p9,
                      T p10, T p11, T p12, T p13, T p14, T p15, T p16, T p17, T p18, T p19,
                      T p20, T p21, T p22, T p23, T p24, T p25, T p26, T p27, T p28) {
            var ret = Buffer;
            ret[0] = p0;
            ret[1] = p1;
            ret[2] = p2;
            ret[3] = p3;
            ret[4] = p4;
            ret[5] = p5;
            ret[6] = p6;
            ret[7] = p7;
            ret[8] = p8;
            ret[9] = p9;
            ret[10] = p10;
            ret[11] = p11;
            ret[12] = p12;
            ret[13] = p13;
            ret[14] = p14;
            ret[15] = p15;
            ret[16] = p16;
            ret[17] = p17;
            ret[18] = p18;
            ret[19] = p19;
            ret[20] = p20;
            ret[21] = p21;
            ret[22] = p22;
            ret[23] = p23;
            ret[24] = p24;
            ret[25] = p25;
            ret[26] = p26;
            ret[27] = p27;
            ret[28] = p28;
            return new Span<T>(ret, 29);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5, T p6, T p7, T p8, T p9,
                    T p10, T p11, T p12, T p13, T p14, T p15, T p16, T p17, T p18, T p19,
                    T p20, T p21, T p22, T p23, T p24, T p25, T p26, T p27, T p28, T p29) {
            var ret = Buffer;
            ret[0] = p0;
            ret[1] = p1;
            ret[2] = p2;
            ret[3] = p3;
            ret[4] = p4;
            ret[5] = p5;
            ret[6] = p6;
            ret[7] = p7;
            ret[8] = p8;
            ret[9] = p9;
            ret[10] = p10;
            ret[11] = p11;
            ret[12] = p12;
            ret[13] = p13;
            ret[14] = p14;
            ret[15] = p15;
            ret[16] = p16;
            ret[17] = p17;
            ret[18] = p18;
            ret[19] = p19;
            ret[20] = p20;
            ret[21] = p21;
            ret[22] = p22;
            ret[23] = p23;
            ret[24] = p24;
            ret[25] = p25;
            ret[26] = p26;
            ret[27] = p27;
            ret[28] = p28;
            ret[29] = p29;
            return new Span<T>(ret, 30);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5, T p6, T p7, T p8, T p9,
                    T p10, T p11, T p12, T p13, T p14, T p15, T p16, T p17, T p18, T p19,
                    T p20, T p21, T p22, T p23, T p24, T p25, T p26, T p27, T p28, T p29,
                    T p30) {
            var ret = Buffer;
            ret[0] = p0;
            ret[1] = p1;
            ret[2] = p2;
            ret[3] = p3;
            ret[4] = p4;
            ret[5] = p5;
            ret[6] = p6;
            ret[7] = p7;
            ret[8] = p8;
            ret[9] = p9;
            ret[10] = p10;
            ret[11] = p11;
            ret[12] = p12;
            ret[13] = p13;
            ret[14] = p14;
            ret[15] = p15;
            ret[16] = p16;
            ret[17] = p17;
            ret[18] = p18;
            ret[19] = p19;
            ret[20] = p20;
            ret[21] = p21;
            ret[22] = p22;
            ret[23] = p23;
            ret[24] = p24;
            ret[25] = p25;
            ret[26] = p26;
            ret[27] = p27;
            ret[28] = p28;
            ret[29] = p29;
            ret[30] = p30;
            return new Span<T>(ret, 31);
        }
        
        public static Span<T> Get(T p0, T p1, T p2, T p3, T p4, T p5, T p6, T p7, T p8, T p9,
                    T p10, T p11, T p12, T p13, T p14, T p15, T p16, T p17, T p18, T p19,
                    T p20, T p21, T p22, T p23, T p24, T p25, T p26, T p27, T p28, T p29,
                    T p30, T p31) {
            var ret = Buffer;
            ret[0] = p0;
            ret[1] = p1;
            ret[2] = p2;
            ret[3] = p3;
            ret[4] = p4;
            ret[5] = p5;
            ret[6] = p6;
            ret[7] = p7;
            ret[8] = p8;
            ret[9] = p9;
            ret[10] = p10;
            ret[11] = p11;
            ret[12] = p12;
            ret[13] = p13;
            ret[14] = p14;
            ret[15] = p15;
            ret[16] = p16;
            ret[17] = p17;
            ret[18] = p18;
            ret[19] = p19;
            ret[20] = p20;
            ret[21] = p21;
            ret[22] = p22;
            ret[23] = p23;
            ret[24] = p24;
            ret[25] = p25;
            ret[26] = p26;
            ret[27] = p27;
            ret[28] = p28;
            ret[29] = p29;
            ret[30] = p30;
            ret[31] = p31;
            return new Span<T>(ret, 32);
        }
    }
}
