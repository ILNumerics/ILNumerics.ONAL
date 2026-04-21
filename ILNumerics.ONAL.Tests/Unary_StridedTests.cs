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
//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics; 
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.UnitTests {

    [TestClass]
    public class Unary_StridedTests {

        [TestMethod]
        public void acos_double_strided001() {

            Array<double> A = counter<double>(1.0, 1.0, 1013, 59, 11, StorageOrders.ColumnMajor) / 6e5; 
            A.a = A[r(2,3,17), full, r(3,2,end)] ;

            Assert.IsTrue(A.S.NumberOfDimensions == 3);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.Other);

            // compute the trivial / scalar way
            Array<double> Res = empty<double>(A.S, A.S.StorageOrder); 
            for (int i0 = 0; i0 < A.S[0]; i0++) {
                for (int i1 = 0; i1 < A.S[1]; i1++) {
                    for (int i2 = 0; i2 < A.S[2]; i2++) {
                        var a = A.GetValue(i0, i1, i2); 
                        Res.SetValue(Math.Acos(a), i0, i1, i2);  
                    }
                }
            }

            Assert.IsTrue(maxall(abs(acos(A) - Res)) < eps);
            Assert.IsTrue(Res.S.NumberOfDimensions == 3);
            Assert.IsTrue(Res.S[0] == A.S[0]);
            Assert.IsTrue(Res.S[1] == A.S[1]);
            Assert.IsTrue(Res.S[2] == A.S[2]);

        }
    }
}
