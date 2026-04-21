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
using static ILNumerics.ILMath;
using static ILNumerics.Globals; 

namespace ILNumerics.Core.UnitTests.MathInternalTests {

    [TestClass]
    public class MathInternal_loadTests {

        [TestMethod]
        public void mathInternal_load_simpleTest() {

            // MatFile should be used in an 'using' block, 
            // cleaning up its resources automatically.
            using (MatFile mat = new MatFile()) {
                mat.AddArray(counter<sbyte>(-10, 2, 4, 8, 13), "myArray");
                mat.Write("file.mat");
            }

            // reading back using ILMath.loadArray<T>(...)
            Array<sbyte> A = loadArray<sbyte>("file.mat", "myArray");
            Assert.IsTrue(A.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));

            // reading back using MatFile
            using (var back = new MatFile("file.mat")) {
                Array<sbyte> B = back.GetArray<sbyte>("myArray");

                // ... or usign cell methods: 
                Array<sbyte> C = back.Arrays.GetArray<sbyte>(0);

                Assert.IsTrue(B.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));
                Assert.IsTrue(C.Equals(counter<sbyte>(-10, 2, 4, 8, 13)));
            }
        }
        [TestMethod]
        public void mathInternal_MatFile_docu() {

Array<sbyte> A = counter<sbyte>(-10, 2, 4, 8, 13); 
Array<double> B = ones<double>(199, 200); 

// MatFile should be used in an 'using' block, 
// cleaning up its resources automatically.
using (MatFile mat = new MatFile(cellv(A, B))) {
    mat.Write("file.mat");
}

// reading back using ILMath.loadArray<T>(...)
Array<sbyte> C = loadArray<sbyte>("file.mat", "Array0"); // names are automatic
Array<double> D = loadArray<double>("file.mat", "Array1");
Assert.IsTrue(C.Equals(A));
Assert.IsTrue(D.Equals(B));

        }
        [TestMethod]
        public void mathInternal_load_simpleTest_allTypes() {
            TestLoad<double>(); 
            TestLoad<float>(); 
            TestLoad<sbyte>(); 
            TestLoad<byte>(); 
            TestLoad<short>(); 
            TestLoad<ushort>();
            TestLoad<int>();
            TestLoad<uint>();
            TestLoad<long>();
            TestLoad<ulong>();
        }
        private void TestLoad<T>() where T : struct, IConvertible, IEquatable<T> {
            MatFile mat = new MatFile();
            mat.AddArray(counter<T>(default(T), ones<T>(1).GetValue(0), 4, 8, 13), "A1");
            mat.AddArray(counter<T>(default(T), ones<T>(1).GetValue(0), 0), "A2");
            mat.AddArray(counter<T>(default(T), ones<T>(1).GetValue(0), 0), "A3"); // sic: test two empties, adjacending
            mat.AddArray(counter<T>(default(T), ones<T>(1).GetValue(0), 0, 1), "A4");
            mat.AddArray(counter<T>(default(T), ones<T>(1).GetValue(0), 1, 1), "A5");
            mat.AddArray(counter<T>(default(T), ones<T>(1).GetValue(0), 1, 1000), "A6");
            mat.AddArray(counter<T>(default(T), ones<T>(1).GetValue(0), 1000, 2), "A7");
            mat.Write("file.mat");


            Assert.IsTrue(loadArray<T>("file.mat", "A1").Equals(counter<T>(default(T), ones<T>(1).GetValue(0), 4, 8, 13)));
            Assert.IsTrue(loadArray<T>("file.mat", "A2").Equals(counter<T>(default(T), ones<T>(1).GetValue(0), 0)));
            Assert.IsTrue(loadArray<T>("file.mat", "A3").Equals(counter<T>(default(T), ones<T>(1).GetValue(0), 0)));
            Assert.IsTrue(loadArray<T>("file.mat", "A4").Equals(counter<T>(default(T), ones<T>(1).GetValue(0), 0, 1)));
            Assert.IsTrue(loadArray<T>("file.mat", "A5").Equals(counter<T>(default(T), ones<T>(1).GetValue(0), 1, 1)));
            Assert.IsTrue(loadArray<T>("file.mat", "A6").Equals(counter<T>(default(T), ones<T>(1).GetValue(0), 1, 1000)));
            Assert.IsTrue(loadArray<T>("file.mat", "A7").Equals(counter<T>(default(T), ones<T>(1).GetValue(0), 1000, 2)));

        }
    }
}
