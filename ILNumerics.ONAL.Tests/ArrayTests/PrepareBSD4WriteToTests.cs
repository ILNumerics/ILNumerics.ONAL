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
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class PrepareBSD4WriteToTests {
        [TestMethod]
        public unsafe void PrepareBSD4WriteTo_3dimRM_2dimRM_Test() {

            // left: 3dim, right: 2 dim, tests broadcasting + outBSD storage
            Array<double> inp = new double[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            Assert.IsTrue(inp.S.StorageOrder == StorageOrders.RowMajor); 
            // inpBSD is: 2, 6, 0, 2, 3, 3, 1

            // left side array (imaginary) 2 x 3 x 4, row major
            long* outBSD = stackalloc long[3 + 2 * 3];
            outBSD[0] = (3); outBSD[1] = (4u * 3 * 2); outBSD[2] = (10);

            outBSD[3] = (2u);
            outBSD[4] = (3u);
            outBSD[5] = (4u);
            // row major
            outBSD[6] = (12u);
            outBSD[7] = (4u);
            outBSD[8] = (1u);

            uint outFlag = 0;
            Size.CheckFlags(outBSD, ref outFlag);
            Assert.IsTrue((outFlag & (uint)StorageOrders.RowMajor) != 0); 

            long* ordered_bsd = stackalloc long[3 + 3 * 3];
            Global.Helper.PrepareBSD4WriteTo(inp.S, outBSD, 8, ordered_bsd);

            // expected values: 
            // inp is (virtually) transformed to 3 dim: 2 x 3 x 1 (3, 1, 0), shows up reversed (row major): 
            long[] orderedRes = new long[3 + 3 * 3] { 3, 4u * 3 * 2, 10, 4, 3, 2, 1 * 8, 4 * 8, 12 * 8, /*inp:*/ 0, 1 * 8, 3 * 8 }; 
            for (int i = 0; i < orderedRes.Length; i++) {
                Assert.IsTrue(ordered_bsd[i] == orderedRes[i]); 
            }
        }
        [TestMethod]
        public unsafe void PrepareBSD4WriteTo_3dimRM_2dimCM_Test() {

            // left: 3dim, right: 2 dim, tests broadcasting + outBSD storage
            Array<double> inp = new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            inp.a = inp.T; 
            Assert.IsTrue(inp.S.StorageOrder == StorageOrders.ColumnMajor); 
            // inpBSD is: 2, 6, 0, 2, 3, 1, 2

            // left side array (imaginary) 2 x 3 x 4, row major
            long* outBSD = stackalloc long[3 + 2 * 3];
            outBSD[0] = (3); outBSD[1] = (4u * 3 * 2); outBSD[2] = (10);

            outBSD[3] = (2u);
            outBSD[4] = (3u);
            outBSD[5] = (4u);
            // row major
            outBSD[6] = (12u);
            outBSD[7] = (4u);
            outBSD[8] = (1u);

            uint outFlag = 0;
            Size.CheckFlags(outBSD, ref outFlag);
            Assert.IsTrue((outFlag & (uint)StorageOrders.RowMajor) != 0); 

            long* ordered_bsd = stackalloc long[3 + 3 * 3];
            Global.Helper.PrepareBSD4WriteTo(inp.S, outBSD, 8, ordered_bsd);

            // expected values: 
            // inp is (virtually) transformed to 3 dim: 2 x 3 x 1 (1, 2, 0), shows up reversed (row major): 
            long[] orderedRes = new long[3 + 3 * 3] { 3, 4u * 3 * 2, 10, 4, 3, 2, 1 * 8, 4 * 8, 12 * 8, /*inp:*/  0, 2 * 8, 1 * 8 }; 
            for (int i = 0; i < orderedRes.Length; i++) {
                Assert.IsTrue(ordered_bsd[i] == orderedRes[i], $"failed: {i}"); 
            }
        }

        [TestMethod]
        public unsafe void PrepareBSD4WriteTo_3dimCM_2dimCM_Test() {

            // left: 3dim, right: 2 dim, tests broadcasting + outBSD storage
            Array<double> inp = new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            inp.a = inp.T; 
            Assert.IsTrue(inp.S.StorageOrder == StorageOrders.ColumnMajor); 
            // inpBSD is: 2, 6, 0, 2, 3, 1, 2

            // left side array (imaginary) 2 x 3 x 4, column major
            long* outBSD = stackalloc long[3 + 2 * 3];
            outBSD[0] = (3); outBSD[1] = (4u * 3 * 2); outBSD[2] = (11);

            outBSD[3] = (2u);
            outBSD[4] = (3u);
            outBSD[5] = (4u);
            // column major
            outBSD[6] = (1u);
            outBSD[7] = (2u);
            outBSD[8] = (6u);

            uint outFlag = 0;
            Size.CheckFlags(outBSD, ref outFlag);
            Assert.IsTrue((outFlag & (uint)StorageOrders.ColumnMajor) != 0); 

            long* ordered_bsd = stackalloc long[3 + 3 * 3];
            Global.Helper.PrepareBSD4WriteTo(inp.S, outBSD, 8, ordered_bsd);

            // expected values: 
            // inp is (virtually) transformed to 3 dim: 2 x 3 x 1 (1, 2, 0), shows up NOT reversed (colun major): 
            long[] orderedRes = new long[3 + 3 * 3] { 3, 4u * 3 * 2, 11, 2, 3, 4, 1 * 8, 2 * 8, 6 * 8, /*inp:*/ 1 * 8, 2 * 8, 0 }; 
            for (int i = 0; i < orderedRes.Length; i++) {
                Assert.IsTrue(ordered_bsd[i] == orderedRes[i], $"failed: {i}"); 
            }
        }

        [TestMethod]
        public unsafe void PrepareBSD4WriteTo_3dimOtherOrder_2dimCM_Test() {

            // left: 3dim, right: 2 dim, tests broadcasting + outBSD storage
            Array<double> inp = new double[,,] { { { 1 }, { 2 }, { 3 } } };
            inp.a = inp.T; 
            Assert.IsTrue(inp.S.StorageOrder == StorageOrders.ColumnMajor); 
            // inpBSD is: 3, 3, 0, 3, 1, 1,   1, 1, 3, 

            // left side array (imaginary) 3 x 2 x 4, column major
            long* outBSD = stackalloc long[3 + 2 * 3];
            outBSD[0] = (3); outBSD[1] = (40u * 3 * 2); outBSD[2] = (12);

            outBSD[3] = (3u);
            outBSD[4] = (2u);
            outBSD[5] = (40u);
            // 'other' order 
            outBSD[6] = (3u);
            outBSD[7] = (1u);
            outBSD[8] = (6u);
            // _-> reordered for performance:2,4,3,  1,6,3
            uint outFlag = 0;
            Size.CheckFlags(outBSD, ref outFlag);
            Assert.IsTrue((outFlag & (uint)StorageOrders.Other) != 0);
            Assert.IsTrue(outBSD[1] > Global.Helper.REORDER_THRESHOLD_WRITETO_OTHERSTORAGE); 

            long* ordered_bsd = stackalloc long[3 + 3 * 3];
            Global.Helper.PrepareBSD4WriteTo(inp.S, outBSD, 8, ordered_bsd);

            // expected values: 
            // inp is 3 dim: 3 x 1 x 1 (1, 1, 3), shows up reordered ('other' order of outBSD): 1,1,3,  [1,3,1] -> [0,0,8]
            long[] orderedRes = new long[3 + 3 * 3] { 3, 40u * 3 * 2, 12, 2, 40, 3, 1*8, 6*8, 3*8, /*inp:*/ 0, 0, 1*8 }; 
            for (int i = 0; i < orderedRes.Length; i++) {
                Assert.IsTrue(ordered_bsd[i] == orderedRes[i], $"failed: {i}"); 
            }
        }
        [TestMethod]
        public unsafe void PrepareBSD4WriteTo_3dimColumnOrder_0dimScalar_Test() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                // left: 3dim, right: scalar, tests broadcasting + outBSD storage
                Array<double> inp = 111; // creates a numpy scalar (0 dims, 1 element)

                Assert.IsTrue(inp.S.StorageOrder == StorageOrders.ColumnMajor);
                // inpBSD is: 0, 1, 0 

                // left side array (imaginary) 3 x 2 x 4, column major
                long* outBSD = stackalloc long[3 + 2 * 3];
                outBSD[0] = (3); outBSD[1] = (4u * 3 * 2); outBSD[2] = (13);

                outBSD[3] = (3u);
                outBSD[4] = (2u);
                outBSD[5] = (4u);
                // 'other' order 
                outBSD[6] = (1u);
                outBSD[7] = (3u);
                outBSD[8] = (6u);
                // _-> reordered for performance:3,2,4,  1,3,6
                uint outFlag = 0;
                Size.CheckFlags(outBSD, ref outFlag);
                Assert.IsTrue((outFlag & (uint)StorageOrders.ColumnMajor) != 0);

                long* ordered_bsd = stackalloc long[3 + 3 * 3];
                Global.Helper.PrepareBSD4WriteTo(inp.S, outBSD, 8, ordered_bsd);

                // expected values: 
                // inp is 0 dim: 1 x 1 x 1 (1, 1, 1), shows up reordered -> [0,0,0]
                long[] orderedRes = new long[3 + 3 * 3] { 3, 4u * 3 * 2, 13, 3, 2, 4, 1 * 8, 3 * 8, 6 * 8, /*inp:*/ 0, 0, 0 };
                for (int i = 0; i < orderedRes.Length; i++) {
                    Assert.IsTrue(ordered_bsd[i] == orderedRes[i], $"failed: {i}");
                }
            }
        }
    }
}
