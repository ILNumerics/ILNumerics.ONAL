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

namespace ILNumerics.Core.Tests {

    [TestClass]
    public class numpyAPI_sepModule {

        [TestMethod]
        public void NumpyModuleBasicTest() {
            
            Array<int> A = 1;
            Assert.IsTrue(A.item(0) == 1); // requires ILNumerics.numpy module reference and license

            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4); 
            using (Scope.Enter(ArrayStyles.numpy)) {
                A[ellipsis] = -2;
                A.put(size(0,0,0), -1); 
                Assert.IsTrue(A.item(0, 0) == -1); 
                Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);
            }
            Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);

        }
    }
}
