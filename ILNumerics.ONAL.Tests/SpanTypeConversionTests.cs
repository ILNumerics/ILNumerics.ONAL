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
//ILN(enabled=false)
namespace ImplicitComplexArrayConvTests {

    [TestClass]
    public class SpanTypeConversionTests {

        [TestMethod]
        public void SpanTypeConversionSimple() {

            Span<double> a = stackalloc double [] { 1.0, 2.0, 3.0 };
            Array<double> r = vector<double>(a);

ReadOnlySpan<uint> B = [1, 2, 3];
Array<uint> L = vector(B); 

            Array<complex> A = ccomplex(1.0, 2.0);
            complex c = -1.0;

            Array<complex> R = A * c;

            /* Note: currently, it is not possible to do: A * 1.0. We could enable it (no need to convert 1.0 -> complex) but this would 
             * mean to accept ALL parameters as second parameter, which will be (implicitly) converted to BaseArray<T1>. Errors due to 
             * unmatching element types will only be generated at runtime. This would cause a large set of errors to be delayed until runtime
             * and we would loose the (nice) compiler support at development time here. Therefore, we do not provide this features for now. 
             * Users must explicitly cast the scalar, non-type-matching parameter to the corresponding array type instead: (complex)-1.0 or: new complex(-1.0, 0.0)
             */
        }

        [TestMethod]
        public void Mutable2SpanTest001() {

Array<double> A = new[] { 10, 20, -99.9 };

var span = A.AsReadOnlySpan();

Assert.IsTrue(span[0] == 10);
Assert.IsTrue(span[1] == 20);
Assert.IsTrue(span[2] == -99.9);

        }

        [TestMethod]
        public void Mutable2SpanShareMemoryTest001() {

Array<double> A = new[] { 10, 20, -99.9 };

var span = A.AsSpan();

Assert.IsTrue(span[0] == 10);
Assert.IsTrue(span[1] == 20);
Assert.IsTrue(span[2] == -99.9);

// modify the span
span[2] = pi;

Assert.IsTrue(A.GetValue(2) == pi);

        }
        [TestMethod]
        public void Mutable2SpanShareMemoryOfSharedArrayTest001() {

Array<double> A = new[] { 10, 20, -99.9 };
Array<double> B = A.T;  // shared memory

var span = A.AsSpan();

Assert.IsTrue(span[0] == 10);
Assert.IsTrue(span[1] == 20);
Assert.IsTrue(span[2] == -99.9);

// modify the span
span[2] = pi;

Assert.IsTrue(A.GetValue(2) == pi);
// B should have left been untouched
Assert.IsTrue(B.GetValue(2) == -99.9);

        }
        [TestMethod]
        public void Mutable2SpanShareMemoryOfSharedArrayTest002() {

            Array<double> A = new[] { 10, 20, -99.9 };
            Array<double> B = A.T;  // shared memory

            var span = A.AsSpan();

            Assert.IsTrue(span[0] == 10);
            Assert.IsTrue(span[1] == 20);
            Assert.IsTrue(span[2] == -99.9);

            // modify B
            B[2] = pi;

            Assert.IsTrue(A.GetValue(2) == -99.9);
            // should have left untouched A
            Assert.IsTrue(B.GetValue(2) == pi);

        }

        [TestMethod]
        public void Logical2SpanTest002() {

            Logical A = ones<double>(1, 10) < 10;

            var span = A.AsSpan();

            Assert.IsTrue(span[0]);
            Assert.IsTrue(span[1]);
            Assert.IsTrue(span[2]);

        }

        [TestMethod]
        public void Logical2SpanSharedTest002() {

            Logical A = ones<double>(1, 10) < 10;
            Logical B = A.T;


            var span = A.AsSpan();  // detaches A from B's storage. Vers. 8: may not release B in Debug mode!  


            Assert.IsTrue(span[0]);
            Assert.IsTrue(span[1]);
            Assert.IsTrue(span[2]);

        }
        [TestMethod]
        public void Mutable2ROSpanShareMemoryTest002() {

Array<double> A = new[] { 10, 20, -99.9 };
Array<double> B = A.T;  // shared memory

var span = A.AsReadOnlySpan();
// span still shares the memory with A and B

Assert.IsTrue(span[0] == 10);
Assert.IsTrue(span[1] == 20);
Assert.IsTrue(span[2] == -99.9);

// modify B
B[2] = pi;

Assert.IsTrue(A.GetValue(2) == -99.9);
Assert.IsTrue(span[2] == -99.9);

// should have left untouched A
Assert.IsTrue(B.GetValue(2) == pi);

        }
        [TestMethod]
        public void InArray2ROSpanShareMemoryTest002() {

            Array<double> A = new[] { 10, 20, -99.9 };
            Array<double> B = A.T;  // shared memory

            var span = ((InArray<double>)A).AsReadOnlySpan();
            // span still shares the memory with A and B

            Assert.IsTrue(span[0] == 10);
            Assert.IsTrue(span[1] == 20);
            Assert.IsTrue(span[2] == -99.9);

            // modify B
            B[2] = pi;

            Assert.IsTrue(A.GetValue(2) == -99.9);
            Assert.IsTrue(span[2] == -99.9);

            // should have left untouched A
            Assert.IsTrue(B.GetValue(2) == pi);

        }

    }
}
