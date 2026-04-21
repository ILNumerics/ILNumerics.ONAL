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
using System.Diagnostics;

namespace ILNumerics.UnitTests.Legacy_Tests {

    [TestClass]
    public partial class ArrayTests {

        [TestMethod]
        public void StringIndexFullShortcut001() {

            Array<double> A = counter<double>(1.0, 1.0, 4, 3, 2);

            // trailing 0(s): just reuse the existing array
            Array<double> B = A[":;:;0"];
            Assert.IsTrue(B.Equals(counter<double>(1.0, 1.0, 4, 3)));

            // non-0 base offset: must create new + copy existing storage
            B.a = A[":;:;1"];
 
            Assert.IsTrue(B.Equals(counter<double>(1.0, 1.0, 4, 3) + 12));

            Assert.IsTrue(A[":;2;1"].shape.Equals(size(4, 1, 1)));
            Assert.IsTrue(A[":;0;1"].shape.Equals(size(4, 1, 1)));
            Assert.IsTrue(A[":;:;0"].shape.Equals(size(4, 3, 1)));
            Assert.IsTrue(A[":;0;0"].Equals(counter<double>(1.0, 1.0, 4, 1, 1)));

            A = counter<double>(1.0, 1.0, 1, 10);
            Assert.IsTrue(counter<double>(1.0, 1.0, 4, 3, 2)[":;:;:"].Equals(counter<double>(1.0, 1.0, 4, 3, 2)));
            Assert.IsTrue(counter<double>(1.0, 1.0, 4, 3, 2)[":;:"].Equals(counter<double>(1.0, 1.0, 4, 6)));
            Assert.IsTrue(counter<double>(1.0, 1.0, 4, 3, 2)[":"].Equals(counter<double>(1.0, 1.0, 24, 1)));
            Assert.IsTrue(counter<double>(1.0, 1.0, 4, 3, 2)[":;:;0"].Equals(counter<double>(1.0, 1.0, 4, 3)));
            Assert.IsTrue(counter<double>(1.0, 1.0, 4, 3, 2)[":;:;1"].Equals(counter<double>(1.0, 1.0, 4, 3) + 12));

            //ILN(enabled=false)
            //ILN(enabled=true)
            //Equals(counter()))   
        }

        [TestMethod]
        public void StringIndexFullShortcutStringNeg() {

            Array<double> A = counter<double>(1.0, 1.0, 4, 3, 2)[":;:;-1"];
            Assert.IsTrue(A.Equals(counter<double>(1.0, 1.0, 4, 3, 2)[full, full, end])); 
        }
        
        [ExpectedException(typeof(ArgumentException), "only integer subscript indices should be allowed")]
        [TestMethod]
        public void StringIndexFullShortcutFAIL002() {

            Array<double> A = counter<double>(1.0, 1.0, 4, 3, 2)[":;:;a1"];
 
        }
        
        [TestMethod]
        public void StringIndexFullShortcut002_NoAcc() {
            //ILN(enabled=false)
            // shortcut is used: 
            Array<double> A = counter<double>(1.0, 1.0, 4, 3, 2, 3); 
            Array<double> B = A[":;:;0;0"];
            Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles));

            // without the shortcut: 
            Array<double> C = counter<double>(1.0, 1.0, 4, 3, 2, 4);
            Array<double> D = C[":;:;0;:"];
            Assert.IsTrue(object.ReferenceEquals(C.Storage.m_handles, D.Storage.m_handles));

            D.a = C[":;:;0;0:1"];
            Assert.IsTrue(object.ReferenceEquals(C.Storage.m_handles, D.Storage.m_handles));

            // same with shortcut, but basindex != 0: 
            Array<double> E = counter<double>(1.0, 1.0, 4, 3, 2, 4);
            Array<double> F = E[":;:;0;1"];
            Assert.IsTrue(object.ReferenceEquals(E.Storage.m_handles, F.Storage.m_handles));
            //ILN(enabled=true)
        }


        [TestMethod]
        public void Cast_2DSystemArray()
        {

            using (ILNumerics.Scope.Enter())
            {
                Array<double> A = new double[,] { { 1.0, 2.0, 3.0 }, { 5.0, 6.0, 7.0 } };
                Assert.IsTrue(A.Size[0].Equals(2), "invalid shape of implicit cast result: [,]");
                Assert.IsTrue(A.Size[1].Equals(3), "invalid shape of implicit cast result: [,]");

                A = new double[,,]{{{1.0,2.0,3.0},{4.0,5.0,6.0}},
                                   {{7.0,8.0,9.0},{10.0,11.0,12.0}}};
                Assert.IsTrue(A.Size[0].Equals(2), "invalid shape of implicit cast result: [,,]");
                Assert.IsTrue(A.Size[1].Equals(2), "invalid shape of implicit cast result: [,,]");
                Assert.IsTrue(A.Size[2].Equals(3), "invalid shape of implicit cast result: [,,]");

                Assert.IsTrue(A.Equals(counter<double>(1,1, 2, 2, 3, StorageOrders.RowMajor)), "invalid result: cast from [,,]");

                Array<int> B = new int[] { 3, 4, 5, 6, 7, 8, 9 };
                Assert.IsTrue(B.IsColumnVector, "column vector expected: cast[]");
                Assert.IsTrue(B.S.NumberOfDimensions == 2); 

            }
        }

        public void MyFunc(InArray<double> inA, InArray<double> inB, InArray<double> inC)
        {
            using (Scope.Enter(inA, inB, inC))
            {
                // declare local arrays for all input parameters
                Array<double> A, B, C;
                // parameter checking via 'check': 
                A = check(inA);            // checks on null
                B = check(inB, (b) =>
                {    // checks on column vector also
                    Assert.IsTrue(b.IsVector, "parameter inB must be a vector!");
                    //if (!b.IsVector)
                    //    throw new ArgumentException("parameter inB must be a vector!");
                    return (b.IsRowVector) ? b.C : b.T;
                });
                // parameter checking the classical way:
                Assert.IsFalse(isnull(inC), "invalid parameter 'inC' provided");
                Assert.IsFalse(inC.IsEmpty, "invalid parameter 'inC' provided");
                Assert.IsTrue(inC.IsColumnVector, "invalid parameter 'inC' provided");
                Assert.IsTrue(inC.Length.Equals(inB.S[0]), "invalid parameter 'inC' provided");
                //if (isnull(inC) || inC.IsEmpty || !inC.IsColumnVector || inC.Length != inB.S[0])
                //    throw new ArgumentException("invalid parameter 'inC' provided");
                // proceed with local variables A,B and C...
                // ...
            }
        }

        [TestMethod]
        public void Test_MaxValue_double() {
            Array<double> A = new double[] { double.NaN, double.NaN, double.NaN };
            Assert.IsFalse(A.GetLimits(out double max, out _));
            //Assert.IsTrue(double.IsNaN(max), "max value should be NaN for row vector of double.NaN's!");  <-- no, undefined for all NaN values.
        }
        [TestMethod]
        public void Test_MaxValue_float() {

            Array<float> Af = new float[] { float.NaN, float.NaN, float.NaN };
            Assert.IsFalse(Af.GetLimits(out float maxf, out _));
            //Assert.IsTrue(float.IsNaN(maxf), "max value should be NaN for row vector of float.NaN's!");   < --no, undefined for all NaN values.
        }
        [TestMethod]
        public void Test_MaxValue_fcomplex() {

            Array<fcomplex> Afc = new fcomplex[] { fcomplex.NaN, fcomplex.NaN, fcomplex.NaN };
            Assert.IsFalse(Afc.GetLimits(out fcomplex maxfc, out _));
            Assert.IsTrue(fcomplex.IsNaN(maxfc), "max value should be NaN for row vector of fcomplex.NaN's!");
        }
        [TestMethod]
        public void Test_MaxValue_complex() {

            Array<complex> Ac = new complex[] { complex.NaN, complex.NaN, complex.NaN };
            Assert.IsFalse(Ac.GetLimits(out complex maxc, out _)); 
            Assert.IsTrue(complex.IsNaN(maxc), "max value should be NaN for row vector of complex.NaN's!");

        }

        [TestMethod]
        public void Test_MinValue_doubleFail() {

            Array<double> A = new double[] { double.NaN, double.NaN, double.NaN };
            Assert.IsFalse(A.GetLimits(out double max, out _)); 
        }
        [TestMethod]
        public void Test_MinValue_floatFail() {

            Array<float> Af = new float[] { float.NaN, float.NaN, float.NaN };
            Assert.IsFalse(Af.GetLimits(out float maxf, out _));
        }
        [TestMethod]
        public void Test_MinValue_fcomplexFail() {

            Array<fcomplex> Afc = new fcomplex[] { fcomplex.NaN, fcomplex.NaN, fcomplex.NaN };
            Assert.IsFalse(Afc.GetLimits(out fcomplex maxfc, out _)); 
        }
        [TestMethod]
        public void Test_MinValue_complexFail() {

            Array<complex> Ac = new complex[] { complex.NaN, complex.NaN, complex.NaN };
            Assert.IsFalse(Ac.GetLimits(out complex maxc, out _));
        }
        [TestMethod]
        public void Test_MinValue_complex() {

            Array<complex> Ac = new complex[] { complex.NaN, complex.NaN, complex.Zero - 1 };
            Assert.IsTrue(Ac.GetLimits(out complex maxc, out _));
            Assert.IsTrue(maxc.real == -1);
        }
        [TestMethod]
        public void Test_MinValue_double() {

            Array<double> A = vector<double>(double.PositiveInfinity, double.PositiveInfinity);
            Assert.IsTrue(min(A).Equals(double.PositiveInfinity), "invalid MinValue returned for: double.PositiveInfinity!");
            A.GetLimits(out double maxa, out double mina);
            Assert.IsTrue(mina.Equals(double.PositiveInfinity), "invalid MinValue returned for: double.PositiveInfinity!");

        }

        [TestMethod]
        public void Test_TrailingSingletonDimension()
        {

            Array<double> A = counter<double>(1.0, 1.0, 4, 3, 2);
            Array<int> idx = new int[] { 3, 2, 1, 0 };

            // single index access
            Array<double> Res = A[3, 2, 1, 0];
            Assert.IsTrue(Res.Equals(24.0), "invalid value detected!");

            Res = A[3, 2, 1, 0, 0];
            Assert.IsTrue(Res.Equals(24.0), "invalid value detected!");

            Res = A[3, 2, 1, 0, 0, 0, 0];
            Assert.IsTrue(Res.Equals(24.0), "invalid value detected!");

            Res = A["3;2;1;0;0"];
            Assert.IsTrue(Res.Equals(24.0), "invalid value detected!");

            Res = A["3;2;1;0;0;0"];
            Assert.IsTrue(Res.Equals(24.0), "invalid value detected!");

            Res = A["3;2;1;0;0;0;0"];
            Assert.IsTrue(Res.Equals(24.0), "invalid value detected!");

            idx = new int[] { 3 };
            Res = new double[] { 16.0, 20, 24 };
            Assert.IsTrue(Res.Equals(A[idx, ":", 1, 0]), "invalid value detected!");
            Assert.IsTrue(Res.Equals(A[idx, full, 1, 0, 0]), "invalid value detected!");
            Assert.IsTrue(Res.Equals(A[idx, ":", 1, 0, 0, 0]), "invalid value detected!");
            Assert.IsTrue(Res.Equals(A[idx, ":", 1, 0, 0, 0, 0]), "invalid value detected!");

        }

        
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Test_TrailingSingletonDimension_OutOfBound1()
        {
            Array<double> A = counter<double>(1.0, 1.0, 4, 3, 2);
            Array<double> Res = A["3;2;1;1"];
 
        }


        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException), "invalid index (out of bound) should throw exception")]
        public void Test_TrailingSingletonDimension_OutOfBound2()
        {
            Array<double> A = counter<double>(1.0, 1.0, 4, 3, 2);
            Array<double> Res = A["3;2;1;0;2"];
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException), "invalid index (out of bound) should throw exception")]
        public void Test_TrailingSingletonDimension_OutOfBound3()
        {
            Array<double> A = counter<double>(1.0, 1.0, 4, 3, 2);
            Array<int> idx = new int[] { 3 };
            Array<double> Res = A[":", idx, 1, 2];
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException), "invalid index (out of bound) should throw exception")]
        public void Test_TrailingSingletonDimension_OutOfBound4()
        {
            Array<double> A = counter<double>(1.0, 1.0, 4, 3, 2);
            Array<int> idx = new int[] { 3 };
            Array<double> Res = A[":", idx, 1, 0, 1];
        }




        [TestMethod]
        public void Test_Enumerator()
        {

            Array<double> A = counter<double>(1.0, 1.0, 10, 30, 20);
            Array<double> Res = zeros(10, 30, 20);
            int pos = 0;
            System.Diagnostics.Stopwatch p = new System.Diagnostics.Stopwatch();
            foreach (double a in A)
            {
                Res[pos++] = a;
            }
            Assert.IsTrue(Res.Equals(A), "Enumerator: invalid values detected!");

            Res[":;:;:"] = 0.0; pos = 0;
            foreach (double a in A)
            {
                Res[pos++] = a;
            }
            Assert.IsTrue(Res.Equals(A), "Enumerator: invalid values detected!");


            Res[":;:;:"] = 0.0; pos = 0;
            foreach (double a in A)
            {
                Res[pos++] = a;
            }
            Assert.IsTrue(Res.Equals(A), "Enumerator: invalid values detected!");

            Res[":;:"] = 0.0;
            int l = (int)A.Size.NumberOfElements;
            for (int i = 0; i < l; i++)
            {
                Res.SetValue(A.GetValue(i), i);
            }

        }

        ///// <summary>
        ///// Cosmetic function for website display
        ///// </summary>
        ///// <param name="A">arbitrary input</param>
        ///// <returns>array as result of this test</returns>
        ///// <remarks>This function is for testing 
        ///// only. It's usefull to demonstrate some 
        ///// arrays stuff.</remarks>
        //public Array<double> MyTest (Array<double> A) {
        //    if (A.IsMatrix && ishermitian(A)) {
        //        return A + 0.2 / A * A.T; 
        //    } else if (A.Dimensions[1] > 5) {
        //        Array<int> ind = new int[]{3,2,1,5,0,2}; 
        //        return randn(1,20,30) * A[0,i,null]; 
        //    }
        //    Array<double> ret 
        //        = ones(10,20); 
        //    return ret.R;
        //}


        [TestMethod]
        public void Test_SequentialLogicalAccess()
        {

            // test right hand side expression
            Array<double> A = counter<double>(1.0, 1.0, 5, 4, 3);
            Logical ind = counter<double>(1.0, 1.0, 4, 3) > 3;
            Array<double> res = A[ind];
            Assert.IsTrue(res.Equals(linspace(4.0, 12.0, 9)), "sequential index access from logical failed: invalid value");
           
            // test write - left h.s.  expression

            ind["9:11"] = false;
            A[ind] = -1.0;
            res = linspace(1.0, 60, 60).Reshape(A.shape);
            res["3:8"] = -1.0;
            Assert.IsTrue(A.Equals(res), "sequential index access from logical failed: invalid value");
            

            // test removal via logicals 
            A[A == -1] = null;
            res = linspace(1.0, 3.0, 3).Concat(linspace(10.0, 60.0, 51), 1);
            Assert.IsTrue(A.Equals(res), "sequential index access from logical failed: invalid value");
 
        }

        [TestMethod]
        public void Test_Test()
        {

            // matrix A: 8 'data points' in columns 
            Array<double> A, B, C, Euc;
            A = rand(4, 8) * 10 + 1;
            // assume a single data point B
            B = vector(100.0, 30.0, -20.0, 1.9);

            // distance between A and B:
            C = A - B;
            // eucl. distance of A and B:
            Euc = sqrt(sum(C * C));


            A[1, 4] = -1;       // alter single element 
                                //A[full, 1] = 999;   // set 2nd column to 999
            A[end, end, 1] = 2;
            A[full, full, 1] = empty<double>();

            A[r(end - 1, end), "1,2"] = rand(2, 2);

            A["0,11"] = 0;
            A[cellv(4, 8)] = vector(5.0, 9.0);
            A = A * 1;
        }

        [TestMethod]
        public void Test_Expand()
        {

            Array<double> A = new double[] { 1, 2, 3, 4, 5, 6 };
            //A = A.T;
            A["6"] = 7;
            A[7] = 8;
            Assert.IsTrue(A.IsVector, "invalid dimensions length after resize");
            Assert.IsTrue(A.Length.Equals(8), "invalid dimensions length after resize");
         
            A[8, 1] = 20;
            Assert.IsTrue(A.Size[0].Equals(9), "invalid dimensions length after resize");
            Assert.IsTrue(A.Size[1].Equals(2), "invalid dimensions length after resize");
            
            /////////////////////

            A = empty<double>();
            A[0] = 4;
            Assert.IsTrue(A.IsScalar, "invalid dimension length after resize");
            Assert.IsTrue(A[0].Equals(4), "invalid values detected");
            
            A[0, 1] = 4;
            Assert.IsTrue(A.Size[0].Equals(1), "invalid dimension length after resize");
            Assert.IsTrue(A.Size[1].Equals(2), "invalid dimension length after resize");

            Assert.IsTrue(A[0].Equals(4), "invalid values detected");
            Assert.IsTrue(A[1].Equals(4), "invalid values detected");
        
            ////////////////////

            A = empty<double>();
            Array<int> i = new int[] { 0 };
            A[i] = 4;
            Assert.IsTrue(A.IsScalar, "invalid dimension length after resize");
            Assert.IsTrue(A[0].Equals(4), "invalid values detected");
            
            Array<int> i2 = new int[] { 1 };
            A[i, i2] = 4;
            Assert.IsTrue(A.Size[0].Equals(1), "invalid dimension length after resize");
            Assert.IsTrue(A.Size[1].Equals(2), "invalid dimension length after resize");

            Assert.IsTrue(A[0].Equals(4), "invalid values detected");
            Assert.IsTrue(A[1].Equals(4), "invalid values detected");
         
        }

        [TestMethod]
        public void Test_CreatePhysicalSubarrayFromPhysicalSequential()
        {


            // From 3d array -> 3d Array  
            Array<double> A = counter<double>(1.0, 1.0, 4, 3, 2);
            Array<double> ind = counter(23.0, -1, 4, 3, 2);
            Array<double> a = A[ind];
            Array<double> Res = vector<double>(24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1).Reshape(4, 3, 2);
            Assert.IsTrue(a.Equals(Res), "invalid value: A[3d-Array]");
            
            // matrix
            ind = counter(23.0, -1, 4, 6);
            a = A[ind];
            Res = vector<double>(24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1).Reshape(4, 6);
            Assert.IsTrue(a.Equals(Res), "invalid value: A[Matrix]");
            
            // column vector 
            ind = counter<double>(1.0, 1.0, 24, 1) * -1 + 24;
            a = A[ind];
            Res = vector<double>(24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1).Reshape(24, 1);
            Assert.IsTrue(a.Equals(Res), "invalid value: A[Vector]");
         
            // row vector
            ind = counter<double>(1.0, 1.0, 1, 24) * -1 + 24;
            a = A[ind];
            Res = vector<double>(24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1).Reshape(1, 24);
            Assert.IsTrue(a.Equals(Res), "invalid value: A[Vector]");
           
            // extend into N-d array
            ind = counter<double>(1.0, 1.0, 1, 24) * -1 + 24;
            ind = repmat(ind, 1, 10);
            ind = reshape<double>(ind, 5, 4, 3, 2, 2);
            a = A[ind];
            Res = vector<double>(24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1).Reshape(5, 4, 3, 2, 2);
            Assert.IsTrue(a.Equals(Res), "invalid value: A[Matrix]");
           

            // same thing with indices as reference 
           
            A = (Array<double>)counter<double>(1.0, 1.0, 4, 3, 2);
            ind = counter<double>(1.0, 1.0, 4, 3, 2) * -1 + 24;
            ind = ind.C;
            a = A[ind];
            Res = vector<double>(24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1).Reshape(4, 3, 2);
            Assert.IsTrue(a.Equals(Res), "invalid value: A[3d-Array]");
           
            // matrix
            ind = counter<double>(1.0, 1.0, 4, 6) * -1 + 24;
            ind = ind.C;
            a = A[ind];
            Res = vector<double>(24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1).Reshape( 4, 6);
            Assert.IsTrue(a.Equals(Res), "invalid value: A[Matrix]");

            // column vector - -> not possible for ind beeing reference! <-
            // row vector

            // extend dimensions of the destination array 
            ind = counter<double>(1.0, 1.0, 1, 24) * -1 + 24;
            ind = repmat(ind, 1, 10);
            ind = reshape<double>(ind, 5, 4, 3, 2, 2);
            ind = ind.C;
            a = A[ind];
            Res = vector<double>( 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1).Reshape(5, 4, 3, 2, 2);
            Assert.IsTrue(a.Equals(Res), "invalid value: A[Matrix]");
        
        }

        [TestMethod]
        public unsafe void Test_SetRange_RangedPhysical()
        {

            Array<double> AOrig = arange(1.0, 24).Reshape(size(4, 3, 2));
            Array<double> ResOrig = vector<double>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24).Reshape(4, 3, 2);
            Array<double> A, Res, indb;
            // test setrange for single element (sequential access) 
            for (int i = 0; i < 20; i++)
            {
                A = AOrig.C;
                Res = ResOrig.C;
                A[i.ToString()] = 99;
                ((double*)Res.GetHostPointerForWrite())[i] = 99;
                Assert.IsTrue(A.Equals(Res), "A[\"" + i.ToString() + "\"] = 99 failed.");
                
                // test setrange via basearray
                A = AOrig.C;
                A[i] = 99;
                Assert.IsTrue(A.Equals(Res), "A[" + i.ToString() + "] = 99 failed.");
                
            }
            // test setrange for 2 single elements (sequential access) 

            for (int i = 0; i < 20; i++)
            {
                A = AOrig.C;
                Res = ResOrig.C;
                indb = new double[] { i, i + 3 };
                string inds = i.ToString() + "," + (i + 3).ToString();
                ((double*)Res.GetHostPointerForWrite())[i] = 99;
                ((double*)Res.GetHostPointerForWrite())[i + 3] = 99;
                A[inds] = 99;
                Assert.IsTrue(A.Equals(Res), "A[\"" + i.ToString() + "," + (i + 3).ToString() + "\"] = 99 failed.");
                
                // test setrange via basearray
                A = AOrig.C;
                A[indb] = 99;
                Assert.IsTrue(A.Equals(Res), "A[" + i.ToString() + "," + (i + 3).ToString() + "] = 99 failed.");
                
            }

            A = AOrig.C;
            // reshaping range in second dimension 
            A["1;1:end"] = new double[] { 100, 101, 102, 103, 104 };
            Res = ResOrig.C;
            ((double*)Res.GetHostPointerForWrite())[5] = 100;
            ((double*)Res.GetHostPointerForWrite())[9] = 101;
            ((double*)Res.GetHostPointerForWrite())[13] = 102;
            ((double*)Res.GetHostPointerForWrite())[17] = 103;
            ((double*) Res.GetHostPointerForWrite())[21] = 104;
            Assert.IsTrue(A.Equals(Res), "A[1,1:end] = [100...104] failed.");
            
            // reshaping range in 2nd dimension - basearray indices
            indb = new double[] { 1, 2, 3, 4, 5 };
            A[1.0, indb] = new double[] { 100, 101, 102, 103, 104 };
            Res = ResOrig.C;
            ((double*)Res.GetHostPointerForWrite())[5] = 100;
            ((double*)Res.GetHostPointerForWrite())[9] = 101;
            ((double*)Res.GetHostPointerForWrite())[13] = 102;
            ((double*)Res.GetHostPointerForWrite())[17] = 103;
            ((double*)Res.GetHostPointerForWrite())[21] = 104;
            Assert.IsTrue(A.Equals(Res), "A[1,[1,2,3,4,5]] = [100...104] failed.");
            
            // reshaping range in 2nd dimension - for reference storages 

            A = AOrig.C;
            Array<double> ARef = A.C;
            
            // reshaping range in second dimension 
            ARef["1;1:end"] = new double[] { 100, 101, 102, 103, 104 };
            Res = ResOrig.C;
            ((double*)Res.GetHostPointerForWrite())[5] = 100;
            ((double*)Res.GetHostPointerForWrite())[9] = 101;
            ((double*)Res.GetHostPointerForWrite())[13] = 102;
            ((double*)Res.GetHostPointerForWrite())[17] = 103;
            ((double*)Res.GetHostPointerForWrite())[21] = 104;
            Assert.IsTrue(ARef.Equals(Res), "A[1,1:end] = [100...104] failed.");
           
            // reshaping range in 2nd dimension - basearray indices
            indb = new double[] { 1, 2, 3, 4, 5 };
            ARef[1.0, indb] = new double[] { 100, 101, 102, 103, 104 };
            Res = ResOrig.C;
            ((double*)Res.GetHostPointerForWrite())[5] = 100;
            ((double*)Res.GetHostPointerForWrite())[9] = 101;
            ((double*)Res.GetHostPointerForWrite())[13] = 102;
            ((double*)Res.GetHostPointerForWrite())[17] = 103;
            ((double*)Res.GetHostPointerForWrite())[21] = 104;
            Assert.IsTrue(ARef.Equals(Res), "A[1,[1,2,3,4,5]] = [100...104] failed.");
            
            // 

            // test single element (sequential) via range for reference array
            for (int i = 0; i < 20; i++)
            {
                A = AOrig.C;
                ARef = A.C;
                Res = ResOrig.C;
                ARef[i.ToString()] = 99;
                ((double*)Res.GetHostPointerForWrite())[i] = 99;
                Assert.IsTrue(ARef.Equals(Res), "A[\"" + i.ToString() + "\"] = 99 failed.");
                
                // test setrange via basearray
                A = AOrig.C;
                ARef = A.C;
                ARef[i] = 99;
                Assert.IsTrue(ARef.Equals(Res), "A[" + i.ToString() + "] = 99 failed.");
               
            }


            // test scalar source - full dimension
            A = AOrig.C;
            A[":"] = 111;
            Res = ones(4, 3, 2) * 111;
            Assert.IsTrue(A.Equals(Res), "A[:] = {scalar} for physical array failed!");
           
            // ... explicit indices 
            Res = ones(4, 3, 2) * 112;
            A["0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23"] = 112;
            Assert.IsTrue(A.Equals(Res), "A[0..23] = {scalar} for physical array failed!");
            
            // .. for reference storages 

            A = AOrig.C;
            ARef = A.C;

            ARef[":"] = 111;
            Res = ones(4, 3, 2) * 111;
            Assert.IsTrue(ARef.Equals(Res));
            
            Res = ones(4, 3, 2) * 112;
            ARef["0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23"] = 112;
            Assert.IsTrue(ARef.Equals(Res), "A[0..23] = {scalar} for physical array failed!");
            
            // test scalar on all dimensions 

            A = AOrig.C;
            A[": ; : ; :"] = 113;
            Res = ones(4, 3, 2) * 113;
            if (!A.Equals(Res)) throw new Exception("A[:;:;:] = {scalar} for physical array failed!");
            // ... reference 
            ARef = A.C;
            ARef[":;:;:"] = 114;
            Res = ones(4, 3, 2) * 114;
            Assert.IsTrue(ARef.Equals(Res), "A[:;:;:] = {scalar} for reference array failed!");
            

            // test scalar on all dimensions for explicit indices 
            A = AOrig.C;
            A["0:end;0:end;1"] = 114;
            Res = vector<double>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 114, 114, 114, 114, 114, 114, 114, 114, 114, 114, 114, 114).Reshape(4, 3, 2);
            Assert.IsTrue(A.Equals(Res), "A[0:end;0:end;1] = {scalar} for physical array failed!");
            
            // ... reference 
            ARef = A.C;

            ARef["0:end;0:end;1"] = 115;
            Res = vector<double>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 115, 115, 115, 115, 115, 115, 115, 115, 115, 115, 115, 115).Reshape(4, 3, 2);
            Assert.IsTrue(ARef.Equals(Res), "A[0:end;0:end;1] = {scalar} for reference array failed!");
            
            // test extending range

            A = empty<double>();
            A[0] = 1;
            Assert.IsTrue(A.IsScalar, "A[0] = 1 on empty array should extend array!");
            Assert.IsTrue(A.GetValue(0).Equals(1.0), "A[0] = 1 on empty array should extend array!");
            // same test for string definitions
            A = empty<double>();
            ARef = A.C;
            A["0"] = 1;
            ARef["0"] = 1;
            Assert.IsTrue(A.IsScalar, "A[0] = 1 on empty array should extend array!");
            Assert.IsTrue(A.GetValue(0).Equals(1.0), "A[0] = 1 on empty array should extend array!");
            Assert.IsTrue(ARef.IsScalar, "ARef[0] = 1 on empty array should extend array!");
            Assert.IsTrue(ARef.GetValue(0).Equals(1.0), "ARef[0] = 1 on empty array should extend array!");
            
        }

        [TestMethod]
        public unsafe void Test_singleElementWriteAccessPhysical()
        {

            // physical array, non expanding access 
            Array<double> AOrig = arange(1.0, 24);
            AOrig = reshape<double>(AOrig, 4, 3, 2);
            Array<double> Res;
            Array<double> A;
            int i = 0;
            // single dimension access (sequential index only)
            for (i = 0; i < 24; i++)
            {
                A = AOrig.C;
                Res = AOrig.C;
                ((double*)Res.GetHostPointerForWrite())[i] = 99;
                A[i] = 99;
                Assert.IsTrue(A.Equals(Res), "A[?] = 99; failed at i=" + i.ToString());
                
            }
            // 2 dimension access (with/without sequential index access)

            i = 0;
            BaseArray[] indices = new BaseArray[2];
            for (int d1 = 0; d1 < AOrig.Size[1] * AOrig.Size[2]; d1++)
            {
                for (int d0 = 0; d0 < AOrig.Size[0]; d0++)
                {
                    indices[0] = d0;
                    indices[1] = d1;
                    A = AOrig.C;
                    Res = AOrig.C;
                    ((double*)Res.GetHostPointerForWrite())[i++] = 99;
                    A[indices] = 99;
                    Assert.IsTrue(A.Equals(Res), "A[m,n] = 99; failed at i=" + i.ToString());
                    
                }
            }
            // 3 dimension access (with/without sequential index access)

            i = 0;
            indices = new BaseArray[3];
            for (int d2 = 0; d2 < AOrig.Size[2]; d2++)
            {
                for (int d1 = 0; d1 < AOrig.Size[1]; d1++)
                {
                    for (int d0 = 0; d0 < AOrig.Size[0]; d0++)
                    {
                        indices[0] = d0;
                        indices[1] = d1;
                        indices[2] = d2;
                        A = AOrig.C;
                        Res = AOrig.C;
                        ((double*)Res.GetHostPointerForWrite())[i++] = 99;
                        A[indices] = 99;
                        Assert.IsTrue(A.Equals(Res), "A[m,n] = 99; failed at i=" + i.ToString());
                      
                    }
                }
            }
            // xpanding access ******************************************************************************
            // single dimension access (sequential index only)

            AOrig = arange(1.0, 24);
            A = AOrig.C;
            Res = vector<double>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25).Reshape(25, 1);
            A[24] = 25;
            Assert.IsTrue(A.IsColumnVector, "expanding col vector failed");
            Assert.IsTrue(A.Size[0].Equals(25), "expanding col vector failed");
            Assert.IsTrue(A.Equals(Res), "expanding col vector failed: invalid value.");
            
            // expand vector to matrix

            A = AOrig.C;
            Res = vector<double>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0).Reshape(24, 2);
            A[0, 1] = 25;
            Assert.IsTrue(A.IsMatrix, "expanding col vector failed");
            Assert.IsTrue(A.Size[0].Equals(24), "expanding col vector failed");
            Assert.IsTrue(A.Size[1].Equals(2), "expanding col vector failed");
            Assert.IsTrue(A.Equals(Res), "expanding col vector failed: invalid value.");
           

            // expand vector to matrix - both directions

            A = AOrig.C;
            Res = vector<double>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 52).Reshape(26, 2);
            A[25, 1] = 52;
            Assert.IsTrue(A.IsMatrix, "expanding col vector failed");
            Assert.IsTrue(A.Size[0].Equals(26), "expanding col vector failed");
            Assert.IsTrue(A.Size[1].Equals(2), "expanding col vector failed");
            Assert.IsTrue(A.Equals(Res), "expanding col vector failed: invalid value.");
            

            // expand vector to 3d array

            A = AOrig.C;
            Res = vector<double>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0).Reshape(24, 1, 2);
            A[0, 0, 1] = 25;
            Assert.IsTrue(A.Size[0].Equals(24), "expanding col vector failed");
            Assert.IsTrue(A.Size[1].Equals(1), "expanding col vector failed");
            Assert.IsTrue(A.Size[2].Equals(2), "expanding col vector failed");
            Assert.IsTrue(A.Equals(Res), "expanding col vector failed: invalid value.");
            
            // expand scalar to vector 

            A = 1.0;
            Res = vector<double>( 1.0, 2.0).Reshape(2, 1);
            A[1] = 2.0;
            Assert.IsTrue(A.Equals(Res), "expand scalar failed : A[1] = 2.0");
            
            A = 1.0;
            A[0, 1] = 2.0;
            Assert.IsTrue(A.Equals(Res.T), "expand scalar failed :  A[0,1] = 2.0");
            
            // expand scalar to matrix 

            A = 1.0;
            A[1, 1] = 4.0;
            Res = vector<double>( 1, 0, 0, 4).Reshape( 2, 2);
            Assert.IsTrue(A.Equals(Res), "expand scalar to matrix failed");
            A = 1.0;
            A[4, 2, 3] = 10;
            Res = zeros(5, 3, 4);
            ((double*)Res.GetHostPointerForWrite())[59] = 10;
            ((double*)Res.GetHostPointerForWrite())[0] = 1.0;
            Assert.IsTrue(A.Equals(Res), "expand scalar to matrix 2 failed");
            
            // expand from empty array 
            A = empty<double>();
            A[3] = 5.0;
            Res = zeros(4, 1);
            ((double*)Res.GetHostPointerForWrite())[3] = 5.0;
            Assert.IsTrue(A.Equals(Res), "expand from empty array failed");
           
            // expand empty -> scalar
            A = empty<double>();
            A[3] = 5.0;
            Res = zeros(4, 1);
            ((double*)Res.GetHostPointerForWrite())[3] = 5.0;
            Assert.IsTrue(A.Equals(Res), "expand from empty array failed (2).");
          
            // expand empty -> 2d
            A = empty<double>();
            A[1, 1] = 5.0;
            Res = zeros(4, 1);
            Res = reshape<double>(Res, 2, 2);
            ((double*)Res.GetHostPointerForWrite())[3] = 5.0;
            Assert.IsTrue(A.Equals(Res), "expand from empty array failed (2).");
            
            // expand empty -> 3D
            A = empty<double>();
            A[1, 1, 1] = 5.0;
            Res = zeros(8, 1);
            Res = reshape<double>(Res, 2, 2, 2);
            ((double*)Res.GetHostPointerForWrite())[7] = 5.0;
            Assert.IsTrue(A.Equals(Res), "expand from empty array failed (2).");
           

            // expand matrix -> matrix 1st dimension
            A = arange(1.0, 24);
            A = reshape<double>(A, 6, 4);
            Res = vector<double>(1, 2, 3, 4, 5, 6, 66, 7, 8, 9, 10, 11, 12, 0, 13, 14, 15, 16, 17, 18, 0, 19, 20, 21, 22, 23, 24, 0).Reshape(7, 4);
            A[6, 0] = 66;
            Assert.IsTrue(A.Equals(Res), "expand matrix -> matrix, 1st dimensions failed");
           
            // expand matrix -> matrix, 2nd dimensions 
            A = arange(1.0, 24);
            A = reshape<double>(A, 6, 4);
            Res = vector<double>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 0, 0, 0, 0, 0, 66).Reshape(6, 5);
            A[5, 4] = 66;
            Assert.IsTrue(A.Equals(Res), "expand matrix -> matrix, 2nd dimensions failed");

            // expand matrix -> matrix, both dimensions
            A = arange(1.0, 24);
            A = reshape<double>(A, 6, 4);
            Res = vector<double>(1, 2, 3, 4, 5, 6, 0, 7, 8, 9, 10, 11, 12, 0, 13, 14, 15, 16, 17, 18, 0, 19, 20, 21, 22, 23, 24, 0, 0, 0, 0, 0, 0, 0, 66).Reshape(7, 5);
            A[6, 4] = 66;
            Assert.IsTrue(A.Equals(Res), "expand matrix -> matrix, both dimensions failed");

            // expand matrix -> 3d
            A = arange(1.0, 24);
            A = reshape<double>(A, 6, 4);
            Res = vector<double>(1, 2, 3, 4, 5, 6, 0, 7, 8, 9, 10, 11, 12, 0, 13, 14, 15, 16, 17, 18, 0, 19, 20, 21, 22, 23, 24, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 66).Reshape(7, 5, 2);
            A[6, 4, 1] = 66;
            Assert.IsTrue(A.Equals(Res), "expand matrix -> 3d failed");
  
        }


        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException), "out of range should throw exception")]
        public void Test_singleElementAccess_OutOfBound1()
        {
            Array<int> A = vector<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12).Reshape(3, 4);
            Array<int> B = A[12];
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException), "out of range should throw exception")]
        public void Test_singleElementAccess_OutOfBound2()
        {
            Array<int> A = vector<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12).Reshape(3, 4);
            Array<int> B = A[3, 3];
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException), "out of range should throw exception")]
        public void Test_singleElementAccess_OutOfBound3()
        {
            Array<int> A = vector<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12).Reshape(3, 4);
            Array<int> B = A[2, 4];
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException), "out of range should throw exception")]
        public void Test_singleElementAccess_OutOfBound4()
        {
            Array<int> A = vector<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12).Reshape(3, 4);
            Array<int> B = A[4, 3];
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException), "out of range should throw exception")]
        public void Test_singleElementAccess_OutOfBound5()
        {
            Array<int> A = vector<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12).Reshape(3, 4);
            A = A.C;
            Array<int> B = A[12];
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException), "out of range should throw exception")]
        public void Test_singleElementAccess_OutOfBound6()
        {
            Array<int> A = vector<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12).Reshape(3, 4);
            A = A.C;
            Array<int> B = A[3, 3];
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException), "out of range should throw exception")]
        public void Test_singleElementAccess_OutOfBound7()
        {
            Array<int> A = vector<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12).Reshape(3, 4);
            A = A.C;
            Array<int> B = A[2, 4];
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException), "out of range should throw exception")]
        public void Test_singleElementAccess_OutOfBound8()
        {
            Array<int> A = vector<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12).Reshape(3, 4);
            A = A.C;
            Array<int> B = A[4, 3];
        }


        [TestMethod]
        public void Test_singleElementAccess()
        {

            Array<int> A = vector<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12).Reshape(3, 4);
            Assert.IsTrue(A[0].Equals(1), "A[0] failed");
            Assert.IsTrue(A[1].Equals(2), "A[1] failed");
            Assert.IsTrue(A[2].Equals(3), "A[2] failed");
            Assert.IsTrue(A[3].Equals(4), "A[3] failed");
            Assert.IsTrue(A[4].Equals(5), "A[4] failed");
            Assert.IsTrue(A[5].Equals(6), "A[5] failed");
            Assert.IsTrue(A[6].Equals(7), "A[6] failed");
            Assert.IsTrue(A[7].Equals(8), "A[7] failed");
            Assert.IsTrue(A[8].Equals(9), "A[8] failed");
            Assert.IsTrue(A[9].Equals(10), "A[9] failed");
            Assert.IsTrue(A[10].Equals(11), "A[10] failed");
            Assert.IsTrue(A[11].Equals(12), "A[11] failed");
            

           
            Assert.IsTrue(A[0, 0].Equals(1), "A[0,0] failed");
            Assert.IsTrue(A[0, 1].Equals(4), "A[0,1] failed");
            Assert.IsTrue(A[0, 2].Equals(7), "A[0,2] failed");
            Assert.IsTrue(A[0, 3].Equals(10), "A[0,3] failed");
            Assert.IsTrue(A[1, 0].Equals(2), "A[1,0] failed");
            Assert.IsTrue(A[1, 1].Equals(5), "A[1,1] failed");
            Assert.IsTrue(A[1, 2].Equals(8), "A[1,2] failed");
            Assert.IsTrue(A[1, 3].Equals(11), "A[1,3] failed");
            Assert.IsTrue(A[2, 0].Equals(3), "A[2,0] failed");
            Assert.IsTrue(A[2, 1].Equals(6), "A[2,1] failed");
            Assert.IsTrue(A[2, 2].Equals(9), "A[2,2] failed");
            Assert.IsTrue(A[2, 3].Equals(12), "A[2,3] failed");

        
            // test reshaping index in last dimension

            A = arange(1, 24);
            A = reshape<int>(A, 4, 3, 2);
            Assert.IsTrue(A[1, 5].Equals(22), "A[1,5] failed");
            Assert.IsTrue(A[3, 5].Equals(24), "A[3,5] failed");
          

            ///////////////  REFERENCE ARRAY /////////////////////////////

            A = vector<int>( 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 ).Reshape(3, 4);
            A = A.C;

            Assert.IsTrue(A[0].Equals(1), "A[0] failed");
            Assert.IsTrue(A[1].Equals(2), "A[1] failed");
            Assert.IsTrue(A[2].Equals(3), "A[2] failed");
            Assert.IsTrue(A[3].Equals(4), "A[3] failed");
            Assert.IsTrue(A[4].Equals(5), "A[4] failed");
            Assert.IsTrue(A[5].Equals(6), "A[5] failed");
            Assert.IsTrue(A[6].Equals(7), "A[6] failed");
            Assert.IsTrue(A[7].Equals(8), "A[7] failed");
            Assert.IsTrue(A[8].Equals(9), "A[8] failed");
            Assert.IsTrue(A[9].Equals(10), "A[9] failed");
            Assert.IsTrue(A[10].Equals(11), "A[10] failed");
            Assert.IsTrue(A[11].Equals(12), "A[11] failed");
            

            Assert.IsTrue(A[0, 0].Equals(1), "A[0,0] failed");
            Assert.IsTrue(A[0, 1].Equals(4), "A[0,1] failed");
            Assert.IsTrue(A[0, 2].Equals(7), "A[0,2] failed");
            Assert.IsTrue(A[0, 3].Equals(10), "A[0,3] failed");
            Assert.IsTrue(A[1, 0].Equals(2), "A[1,0] failed");
            Assert.IsTrue(A[1, 1].Equals(5), "A[1,1] failed");
            Assert.IsTrue(A[1, 2].Equals(8), "A[1,2] failed");
            Assert.IsTrue(A[1, 3].Equals(11), "A[1,3] failed");
            Assert.IsTrue(A[2, 0].Equals(3), "A[2,0] failed");
            Assert.IsTrue(A[2, 1].Equals(6), "A[2,1] failed");
            Assert.IsTrue(A[2, 2].Equals(9), "A[2,2] failed");
            Assert.IsTrue(A[2, 3].Equals(12), "A[2,3] failed");
            

            // test reshaping index in last dimension

            A = arange<int>(1, 24);
            A = reshape(A, 4, 3, 2);
            Assert.IsTrue(A[1, 5].Equals(22), "A[1,5] failed");
            Assert.IsTrue(A[3, 5].Equals(24), "A[3,5] failed");

        }


        [TestMethod]
        [ExpectedException(typeof(NotSupportedException), "invalid range specification should be refused for removal!")]
        public void Test_Remove_InvalidRange()
        {
            Array<fcomplex> fcA = vector(new fcomplex(1f, 0), 2, 3, 4);
            fcA["1,1,10:133"] = null;
        }

        [TestMethod]
        public void Test_Remove()
        {

            Array<double> A = arange<double>(1.0, 12.0).Reshape(3,2,2);

            Array<double> ARes = vector<double>(2.0, 3.0, 5.0, 6.0, 8.0, 9.0, 11.0, 12.0).Reshape( 2, 4);
            A[0, ":"] = empty<double>();
            Assert.IsTrue(A.Equals(ARes), "Test_Remove: remove by BaseArray indexer gives wrong result!");
                      
            A = counter<double>(1.0, 1.0, 3, 2, 2);
            A["0; : "] = null;
            Assert.IsTrue(A.Equals(ARes), "Test_Remove: remove by string indexer gives wrong result!");
            
            Array<fcomplex> fcA = vector(new fcomplex(1f, 0)).Repmat(2, 3, 4);
            
            fcA[1, 1, 2] = new fcomplex(4.0f, 5.0f);
            fcA[":;:;0,1,3"] = null;

            Assert.IsTrue(fcA.Size.NonSingletonDimensions.Equals(2), "Invalid dimension result after removal: fcA");
            Assert.IsTrue(fcA.Size.NumberOfDimensions.Equals(3), "Invalid dimension result after removal: fcA");
            Assert.IsTrue(fcA.Size[0].Equals(2), "Invalid dimension result after removal: fcA");
            Assert.IsTrue(fcA.Size[1].Equals(3), "Invalid dimension result after removal: fcA");

            Assert.IsTrue(fcA.GetValue(1, 1).real.Equals(4.0f), "Invalid value after removal: expected: 2x3, 1,1 = 4+5i");
            Assert.IsTrue(fcA.GetValue(1, 1).imag.Equals(5.0f), "Invalid value after removal: expected: 2x3, 1,1 = 4+5i");
            
            // fcA is [2x3x1] now. 
            fcA[":;0,2"] = null;
            Assert.IsTrue(fcA.IsColumnVector, "Invalid index detected after removal. Expected: 4+5i. Found: " + fcA[1, 0].ToString());
            Assert.IsTrue(fcA[1].Equals(new fcomplex(4.0f, 5.0f)), "Invalid index detected after removal. Expected: 4+5i. Found: " + fcA[1, 0].ToString());
            
            A = arange(1.0, 24);
            A = reshape<double>(A, 4, 3, 2);
            A["1,2;:"] = null;
            ARes = vector<double>(1, 4, 5, 8, 9, 12, 13, 16, 17, 20, 21, 24).Reshape(2, 6);
            Assert.IsTrue(A.Equals(ARes), "remove failed for: A[\"2,3;:\"] = null");
 
        }

        [TestMethod]
        public void Test_TypeAfterRemoval()
        {

            Array<double> A = counter<double>(1.0, 1.0, 4, 3);
            // reset reference parameters
            // test if after removal no vector was created as reference  - for solid matrix
            A["2:end;:"] = null;
            Assert.IsTrue(A.Size[0].Equals(2), "invalid ref type // shape after removal");
            Assert.IsTrue(A.Size[1].Equals(3), "invalid ref type // shape after removal");
          
            A = counter<double>(1.0, 1.0, 4, 3);
            A["1:end;:"] = null;
            Assert.IsTrue(A.IsRowVector, "invalid shape after removal");
           
        }

        [TestMethod]
        public void Test_IndexAccess()
        {

            Array<double> A = arange(0.0, 10).T;
            A = repmat(A, 5, 1);
            Array<double> ind1 = arange(1.0, 3.0, 9.0);
            Array<double> Res = vector<double>( 1, 1, 1, 4, 4, 4, 7, 7, 7 ).Reshape(3, 3);
            Array<double> B = A[vector<double>( 1.0, 2.0, 4.0), ind1];
            Assert.IsTrue(B.Equals(Res), "Test_IndexAccess: Invalid Values detected!");
                     
            B = A[arange(0, 4), ind1 + 2];
            Res = vector<double>(3, 3, 3, 3, 3, 6, 6, 6, 6, 6, 9, 9, 9, 9, 9).Reshape(5, 3);
            Assert.IsTrue(B.Equals(Res), "Test_IndexAccess: Invalid Values detected!");
           
            // test for reference index arrays 
            B = A[arange<double>(0, 4).T.T, ind1 + 2];
            Assert.IsTrue(B.Equals(Res), "Test_IndexAccess: Invalid Values detected!");

        }

        [TestMethod]
        public void Test_IndexAccessPhysfromPhys()
        {
            // test unshifted copy from physical array

            Array<double> A = arange(1.0, 24);
            A = reshape<double>(A, 4, 3, 2);
            // test full copy
            Array<double> Res = A.C;
            Array<double> B = A[":;:;:"];
            Assert.IsTrue(B.Equals(Res), "A[:;:;:] - invalid result!");
                                    
            B = A["0:end;0:end;0:end"];
            Assert.IsTrue(B.Equals(Res), "A[0:end;0:end;0:end] - invalid result!");
                      
            B = A["0:end;:;0:end"];
            Assert.IsTrue(B.Equals(Res), "A[0:end;0:end;0:end] - invalid result!");
            
            
            B = A["0:end;0:end;:"];
            Assert.IsTrue(B.Equals(Res), "A[0:end;0:end;0:end] - invalid result!");
            
           
            B = A[":;0:end;0:end"];
            Assert.IsTrue(B.Equals(Res), "A[0:end;0:end;0:end] - invalid result!");
            
            
            B = A["1:end;1:end;1:end"];
            Res = vector<double>( 18, 19, 20, 22, 23, 24).Reshape(3, 2);
            Assert.IsTrue(B.Equals(Res), "A[0:end;0:end;0:end] - invalid result!");
                        
            B = A["1,2,3;1:end;1:end"];
            Res = vector<double>(18, 19, 20, 22, 23, 24).Reshape(3, 2);
            Assert.IsTrue(B.Equals(Res), "A[1,2,3;1:end;1:end] - invalid result!");
                        
            B = A["1,2,3", arange(2,-1,0),"1"];
            Res = vector<double>(22, 23, 24, 18, 19, 20, 14, 15, 16).Reshape(3, 3);
            Assert.IsTrue(B.Equals(Res), "A[1,2,3;end:-1:0;1] - invalid result!");
 
        }

        [TestMethod]
        public void Test_ReshapingSubarrayCreation()
        {
            // test partially defined dimensions on right side

            // backwards index definition
            Array<double> A = counter<double>(1.0, 1.0, 4, 3, 2);

            Array<double> Res = new double[] { 24, 19, 14, 9, 4 };
            Array<double> B = A[arange(23, -5, 0)];
            Assert.IsTrue(Res.Equals(B), "invalid result");
            
            A = counter<double>(1.0, 1.0, 4, 3, 2);
            Res = new double[] { 1, 6, 11, 16, 21 };
            B = A[r(0, 5, end)];
            Assert.IsTrue(Res.Equals(B), "invalid result");

        }

        [TestMethod]
        public void Test_IsNumeric()
        {

            string[] data = new string[20];
            for (int i = 0; i < data.Length; i++)
                data[i] = "Value: " + i;
            BaseArray<string> A = vector<string>(data).Reshape(4, 5);
            double[] ddata = new double[20] {
                    4,2,5,1,8,9,0,0,1,1,2,3,19,18,17,16,15,14,13,12 };
            Array<double> B = vector<double>(ddata).Reshape(5, 2, 2);
            Assert.IsFalse(A.IsNumeric, "wrong numeric state detected!");
            Assert.IsTrue(B.IsNumeric, "wrong numeric state detected!");

        }

       
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Test_ReferenceFromArray_OutOfBound1()
        {
            string[] data = new string[20];
            for (int i = 0; i < data.Length; i++)
                data[i] = "Value: " + i;
            Array<string> A = vector<string>(data).Reshape(4, 5);
            double[] ddata = new double[20] {
                    4,2,5,1,8,9,0,0,1,1,2,3,19,18,17,16,15,14,13,12 };
            Array<double> B = vector<double>(ddata).Reshape(5, 2, 2);
            // create unshifted subarray, addressed by matrix -> physical storage
            Array<string> C = A[B];
            B[4, 1, 1] = 333.0;
            C = A[10, B]; // ( <= Quatsch!)
 

        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Test_ReferenceFromArray_OutOfBound2()
        {
            string[] data = new string[20];
            for (int i = 0; i < data.Length; i++)
                data[i] = "Value: " + i;
            Array<string> A = vector<string>(data).Reshape(4, 5);
            double[] ddata = new double[20] {
                    4,2,5,1,8,9,0,0,1,1,2,3,19,18,17,16,15,14,13,12 };
            Array<double> B = vector<double>(ddata).Reshape(5, 2, 2);
            // create unshifted subarray, addressed by matrix -> physical storage
            Array<string> C = A[B];
            B[4, 1, 1] = -1.0;
            C = A[1000, B];
        }


        [TestMethod]
        public void Test_ReferenceFromArray()
        {

            // B = A[idx,0]
            string[] data = new string[20];
            for (int i = 0; i < data.Length; i++)
                data[i] = "Value: " + i;
            Array<string> A = vector<string>(data).Reshape(4, 5);
            double[] ddata = new double[20] {
                    4,2,5,1,8,9,0,0,1,1,2,3,19,18,17,16,15,14,13,12 };
            Array<double> B = vector<double>(ddata).Reshape(5, 2, 2);
            // create unshifted subarray, addressed by matrix -> physical storage
            Array<string> C = A[B];
            string[] results = new string[20];
            for (int i = 0; i < results.Length; i++)
            {
                results[i] = data[(int)ddata[i]];
            }
            BaseArray<string> res = vector<string>(results).Reshape(5, 2, 2);
            Assert.IsTrue(res.Equals(C), "Invalid value of result!");
           

            
            // B = A[idx,2]

            // A[idx] = B

        }

        [TestMethod]
        public void Test_CopyFromArray()
        {

            // B = A[idx,0]
            Array<double> A = reshape<double>(arange(1.0, 1.0, 24.0), 2, 3, 4);
            Array<double> B = A["0,1;0,1,2;0,1,2,3"];
            Assert.IsTrue(B.Equals(A), "Invalid value of result!");
                        
            B = A["0,1;0,1,2;0,1,2,3"].T;
            Array<double> Res = vector<double>(1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24).Reshape(3, 4, 2);
            Assert.IsTrue(Res.Equals(B), "Copy physical shifted: Invald result detected!");

        }

        [TestMethod]
        public void Test_IndexAccessPhysicalSequential()
        {

            // B = A[idx,0]
            Array<double> A = reshape<double>(arange(0.0, 23.0), 2, 3, 4);
            Array<double> ind = vector<double>(2, 4, 6, 8).Reshape(1,4);
            Array<double> B = A[ind];
            Assert.IsTrue(B.Equals(ind), "Invalid value of result!");
            
            // B = A[idx,0]
            A = reshape<double>(arange(0.0, 23.0), 2, 3, 4);
            ind = vector<double>(2, 4, 6, 8).Reshape(1,4);
            B = A[ind];
            Assert.IsTrue(B.Equals(ind), "Invalid value of result!");
            
            ind = vector<double>( 2, 4, 6, 8).Reshape(2, 2);
            Array<double> Res = ind.C;
            B = A[ind];
            Assert.IsTrue(B.Equals(ind), "Invalid value of result!");
           
            ind = vector<double>(2, 4, 6, 8).Reshape(2, 2);
            Res = (Array<double>)ind.T;
            B = A[ind.T];
            Assert.IsTrue(B.Equals(Res), "Invalid value of result!");
           
        }
    }
}
