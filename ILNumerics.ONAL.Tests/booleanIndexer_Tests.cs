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
using ILNumerics; 
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

namespace Core_Tests_small {

    [TestClass]
    public class booleanIndexer_Tests {

        [TestMethod]
        public void booleanIndexer_simple() {
            Array<int> A = new[,] {
                {1,2,3,4 },
                {-1,-2,-3,-4 },
                {10,20,30,40 },
                {-11,-22,-33,-44 }
                }; 
            Logical L = new[] { false, true, false, true };
            Array<int> B = A[L, full];

            Array<int> Res = vector<int>(-1, -11, -2, -22, -3, -33, -4, -44).Reshape(size(2, 4), StorageOrders.ColumnMajor); 
            Assert.IsTrue(B.Equals(Res));
        }
    }
}
