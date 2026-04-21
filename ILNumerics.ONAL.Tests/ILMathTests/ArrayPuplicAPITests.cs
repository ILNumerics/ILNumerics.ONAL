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
using System.Linq; 
using System.Reflection;
using ILNumerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILNumerics.Core.UnitTests {

    [TestClass]
    public class ArrayPuplicAPITests {

        [TestMethod]
        public void ArrayExportValuesTest() {

            var SysArr = new uint[,] {
                { 1, 2, 3  },
                { 4, 5, 6  },
                { 7, 8, 9  },
                { 10, 11, 12 },
            };

             Array<uint> A = SysArr;

            uint[] data = null; // = new uint[A.S.NumberOfElements];
            A.ExportValues(ref data);
            Array<uint> B = data;
            B.a = B.Reshape(4, 3); 

            Assert.IsTrue(A.Equals(B));
            Assert.IsFalse(object.ReferenceEquals(SysArr, data));  
        }

        [TestMethod]
        public void MathInternal_nopublic_API() {

            BindingFlags bindFlags = BindingFlags.Static | BindingFlags.Public;
            var methods = typeof(ILNumerics.Core.Functions.Builtin.MathInternal).GetMethods(bindFlags);
            Assert.IsTrue(methods.Length == 0, "Unexpected public member of MathInternal: " + String.Join(",", methods.Select(m => m.Name)));

            var members = typeof(ILNumerics.Core.Functions.Builtin.MathInternal).GetMembers(bindFlags);
            Assert.IsTrue(members.Length == 0, "Unexpected public member of MathInternal: " + String.Join(",", members.Select(m => m.Name)));
            

        }
    }
}
