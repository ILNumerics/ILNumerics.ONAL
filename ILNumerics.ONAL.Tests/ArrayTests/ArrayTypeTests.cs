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
using ILNumerics.Core.StorageLayer;
using System.Reflection;

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class ArrayTypesBasicHandling {

        [TestMethod]
        public void ArrayScalarCreationTest() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = 1;

                // untyped interface
                Assert.IsTrue(A.GetItem(0) != null);
                var obj = A.GetItem(0);
                Assert.IsTrue(obj.Equals(1.0));

                // always true: Assert.IsTrue(A.GetValue(0) is double);
                Assert.IsTrue(A.GetValue(0) == 1.0);

                Assert.IsTrue(A.S[0] == 1);
                Assert.IsTrue(A.S[1] == 1);
                Assert.IsTrue(A.S[2] == 1);
                Assert.IsTrue(A.S.NumberOfDimensions == 0);
                Assert.IsTrue(A.S.NumberOfElements == 1);
            }

        }

        [TestMethod]
        public void ArrayMultipleDisposeTest() {
            Array<int> A = 10;
            Array<int> B = A.C;

            using (Scope.Enter(A)) {

                A.Dispose(); 

            } // here A is disposed a 2nd time! 

            // make sure the reference counter of the disposed object is reasonable (0, not -1)

            // subsequent arrays have a reasonable reference count too
            Array <int> C = 20;
            Assert.IsTrue(C.Storage.ReferenceCount == 1);
            Assert.IsTrue(C.Storage.m_handles.ReferenceCount == 1); 
        }

        [TestMethod]
        public void ArrayAssignTest() {

            Array<int> A = 10;

            A.a = new int[] { 20, 30 }; // this creates a new storage for the right side. During assignment to A this gives its 
                                        // handles to A's storage and disposes off. The right side afterwards lays in the cache.


            Assert.IsTrue(A.S.NumberOfDimensions == Settings.MinNumberOfArrayDimensions); 
            Assert.IsTrue(A.S.NumberOfElements == 2);

            Assert.IsTrue(A.S[0] == 2); 
            Assert.IsTrue(A.S[1] == 1);
            Assert.IsTrue(A.S[2] == 1);

        }

        [TestMethod]
        public void ArrayScopeTest() {

            Array<uint> A = 11;

            //Assert.IsTrue(A.Storage.ScopeState == 0);

            Array<int> B; 
            using (Scope.Enter(A)) {  // this registers A in the scope


                B = 20;  // this registers B in the scope! 

                //Assert.IsTrue(B.Storage.ScopeState == 2);

                Assert.IsTrue(B.GetValue(0) == 20);          

            } // this releases A and B 
            //Assert.IsTrue(B.Storage.ScopeState == 0);

            //Assert.IsTrue(A.Storage.ScopeState == 0);

        }
        [TestMethod]
        public void ArrayScopeTest2() {
            Array<uint> A;
            using (Scope.Enter()) {

                A = 11;

                //Assert.IsTrue(A.Storage.ScopeState == 2);

                Array<int> B;
                using (Scope.Enter(A)) {

                    //Assert.IsTrue(A.Storage.ScopeState == 2);

                    B = 20;

                    //Assert.IsTrue(B.Storage.ScopeState == 2);

                    Assert.IsTrue(B.GetValue(0) == 20);

                }
                //Assert.IsTrue(B.Storage.ScopeState == 0);

                //Assert.IsTrue(A.Storage.ScopeState == 2);
                Assert.IsTrue(A.GetValue(0) == 11);

            }
            //Assert.IsTrue(A.Storage.ScopeState == 0);
        }
        [TestMethod]
        public void ArrayScopeTest3NoAcc() {

            // tests LocalT->InT conversion w/o outer scope

            //ILN(enabled=false)
            Array<uint> A = 1;
            //Assert.IsTrue(A.Storage.ScopeState == 0);

            myfuncInTFromLocalT<uint>(A, 1, 0);  // no scope handling, A keeps InArray alive

            //Assert.IsTrue(A.Storage.ScopeState == 0);

            using (Scope.Enter()) {

                // tests LocalT->InT conversion with outer scope
                A = 11;

                //Assert.IsTrue(A.Storage.ScopeState == 2);

                myfuncInTFromLocalT<uint>(A, 1, 2); // only the outermost scope is considered

                //Assert.IsTrue(A.Storage.ScopeState == 2);

            }
            //Assert.IsTrue(A.Storage.ScopeState == 0);
            //ILN(enabled=true)
        }
        [TestMethod]
        public void ArrayScopeTest3_NoAcc() {

            // tests LocalT->InT conversion w/o outer scope

            //ILN(enabled=false)
            Array<uint> A = 1;
            
            myfuncInTFromLocalT<uint>(A, 1, 0);  // A is CLONED to InArray. It will be cleaned up when OUTER scope is disposed.
            var aHndl = A.Storage.m_handles;
            CountableArray a2Handl = null; 


            using (Scope.Enter()) {

                // tests LocalT->InT conversion with outer scope
                A = 11;
                a2Handl = A.Storage.m_handles;


                myfuncInTFromLocalT<uint>(A, 1, 2); // only the outermost scope is considered


            }
            //ILN(enabled=true)

        }
        void myfuncInTFromLocalT<T>(InArray<T> B, int storageCount, int scopeCount) {
            using (Scope.Enter()) {
                // nothing do to here 
                System.Diagnostics.Trace.WriteLine($"In myfunc: {B.ToString()}");

            }
        }

        [TestMethod]
        public void Array_CloneTest_ML_empty() {
            Array<uint> A = new uint[0] { }; 
            // make the assignent B = A in a two-step process

            // simple clone of A -> AC
            var AC = A.C;
            // now, 2 storages exist. 
            Assert.IsFalse(object.ReferenceEquals(AC.Storage, A.Storage));

            try {
                Assert.IsTrue(A.GetValue(0) == 999);
                Assert.Fail("Index out of range should throw!"); 
            } catch (IndexOutOfRangeException) {
                // ... 
            }
            try {
                Assert.IsTrue(AC.GetValue(0) == 999);
                Assert.Fail("Index out of range should throw!");
            } catch (IndexOutOfRangeException) {
                // ... 
            }
            // implementation detail. Not specified / guaranteed: 
            Assert.IsTrue(object.ReferenceEquals(AC.Storage.m_handles, A.Storage.m_handles));

        }
        [TestMethod]
        public void Array_CloneTest_Numpy_empty() {

            using (Settings.Ensure(nameof(Settings.ArrayStyle), ArrayStyles.numpy)) {

                Array<uint> A = new uint[0] { };
                // make the assignent B = A in a two-step process

                // simple clone of A -> AC
                var AC = A.C;
                // now, 2 storages exist. 
                Assert.IsFalse(object.ReferenceEquals(AC.Storage, A.Storage));

                try {
                    Assert.IsTrue(A.GetValue(0) == 999);
                    Assert.Fail("Index out of range should throw!");
                } catch (IndexOutOfRangeException) {
                    // ... 
                }
                try {
                    Assert.IsTrue(AC.GetValue(0) == 999);
                    Assert.Fail("Index out of range should throw!");
                } catch (IndexOutOfRangeException) {
                    // ... 
                }
                // implementation detail. Not specified / guaranteed: 
                Assert.IsTrue(object.ReferenceEquals(AC.Storage.m_handles, A.Storage.m_handles));

            }
        }

        [TestMethod]
        public void ArrayCreateMultiDimLongTest() {
            var SysArr = new long[,] {
                { 1, 2, 3  },
                { 4, 5, 6  },
                { 7, 8, 9  },
                { 10, 11, 12 },
            };
            Array<long> A = SysArr;

            Assert.IsNotNull(A);
            Assert.IsTrue(A.S[0] == 4);
            Assert.IsTrue(A.S[1] == 3);
            Assert.IsTrue(A.S[2] == 1);
            Assert.IsTrue(A.S[3] == 1);
            Assert.IsTrue(A.S.NumberOfDimensions == 2);
            Assert.IsTrue(A.S.NumberOfElements == 12);

            Assert.IsTrue(A.GetValue(0) == 1);
            Assert.IsTrue(A.GetValue(1) == 4);
            Assert.IsTrue(A.GetValue(2) == 7);
            Assert.IsTrue(A.GetValue(3) == 10);
            Assert.IsTrue(A.GetValue(4) == 2);
            Assert.IsTrue(A.GetValue(5) == 5);
            Assert.IsTrue(A.GetValue(6) == 8);
            Assert.IsTrue(A.GetValue(7) == 11);
            Assert.IsTrue(A.GetValue(8) == 3);
            Assert.IsTrue(A.GetValue(9) == 6);
            Assert.IsTrue(A.GetValue(10) == 9);
            Assert.IsTrue(A.GetValue(11) == 12);

            Assert.IsTrue(A.GetValue(0, 0) == 1);
            Assert.IsTrue(A.GetValue(0, 1) == 2);
            Assert.IsTrue(A.GetValue(0, 2) == 3);
            Assert.IsTrue(A.GetValue(1, 0) == 4);
            Assert.IsTrue(A.GetValue(1, 1) == 5);
            Assert.IsTrue(A.GetValue(1, 2) == 6);
            Assert.IsTrue(A.GetValue(2, 0) == 7);
            Assert.IsTrue(A.GetValue(2, 1) == 8);
            Assert.IsTrue(A.GetValue(2, 2) == 9);
            Assert.IsTrue(A.GetValue(3, 0) == 10);
            Assert.IsTrue(A.GetValue(3, 1) == 11);
            Assert.IsTrue(A.GetValue(3, 2) == 12);

            Assert.IsTrue(A.S.NumberOfElements == 12);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
        }

        [TestMethod]
        public void ArrayCreateMultiDimDoubleTest() {
            var SysArr = new double[,] {
                { 1, 2, 3  },
                { 4, 5, 6  },
                { 7, 8, 9  },
                { 10, 11, 12 },
            };
            Array<double> A = SysArr;

            Assert.IsNotNull(A);
            Assert.IsTrue(A.S[0] == 4);
            Assert.IsTrue(A.S[1] == 3);
            Assert.IsTrue(A.S[2] == 1);
            Assert.IsTrue(A.S[3] == 1);
            Assert.IsTrue(A.S.NumberOfDimensions == 2);
            Assert.IsTrue(A.S.NumberOfElements == 12);

            Assert.IsTrue(A.GetValue(0) == 1);
            Assert.IsTrue(A.GetValue(1) == 4);
            Assert.IsTrue(A.GetValue(2) == 7);
            Assert.IsTrue(A.GetValue(3) == 10);
            Assert.IsTrue(A.GetValue(4) == 2);
            Assert.IsTrue(A.GetValue(5) == 5);
            Assert.IsTrue(A.GetValue(6) == 8);
            Assert.IsTrue(A.GetValue(7) == 11);
            Assert.IsTrue(A.GetValue(8) == 3);
            Assert.IsTrue(A.GetValue(9) == 6);
            Assert.IsTrue(A.GetValue(10) == 9);
            Assert.IsTrue(A.GetValue(11) == 12);

            Assert.IsTrue(A.GetValue(0, 0) == 1);
            Assert.IsTrue(A.GetValue(0, 1) == 2);
            Assert.IsTrue(A.GetValue(0, 2) == 3);
            Assert.IsTrue(A.GetValue(1, 0) == 4);
            Assert.IsTrue(A.GetValue(1, 1) == 5);
            Assert.IsTrue(A.GetValue(1, 2) == 6);
            Assert.IsTrue(A.GetValue(2, 0) == 7);
            Assert.IsTrue(A.GetValue(2, 1) == 8);
            Assert.IsTrue(A.GetValue(2, 2) == 9);
            Assert.IsTrue(A.GetValue(3, 0) == 10);
            Assert.IsTrue(A.GetValue(3, 1) == 11);
            Assert.IsTrue(A.GetValue(3, 2) == 12);

            Assert.IsTrue(A.S.NumberOfElements == 12);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
        }
        [TestMethod]
        public void ArrayCreateMultiDimULongTest() {
            var SysArr = new ulong[,] {
                { 1, 2, 3  },
                { 4, 5, 6  },
                { 7, 8, 9  },
                { 10, 11, 12 },
            };
            Array<ulong> A = SysArr;

            Assert.IsNotNull(A);
            Assert.IsTrue(A.S[0] == 4);
            Assert.IsTrue(A.S[1] == 3);
            Assert.IsTrue(A.S[2] == 1);
            Assert.IsTrue(A.S[3] == 1);
            Assert.IsTrue(A.S.NumberOfDimensions == 2);
            Assert.IsTrue(A.S.NumberOfElements == 12);

            Assert.IsTrue(A.GetValue(0) == 1);
            Assert.IsTrue(A.GetValue(1) == 4);
            Assert.IsTrue(A.GetValue(2) == 7);
            Assert.IsTrue(A.GetValue(3) == 10);
            Assert.IsTrue(A.GetValue(4) == 2);
            Assert.IsTrue(A.GetValue(5) == 5);
            Assert.IsTrue(A.GetValue(6) == 8);
            Assert.IsTrue(A.GetValue(7) == 11);
            Assert.IsTrue(A.GetValue(8) == 3);
            Assert.IsTrue(A.GetValue(9) == 6);
            Assert.IsTrue(A.GetValue(10) == 9);
            Assert.IsTrue(A.GetValue(11) == 12);

            Assert.IsTrue(A.GetValue(0, 0) == 1);
            Assert.IsTrue(A.GetValue(0, 1) == 2);
            Assert.IsTrue(A.GetValue(0, 2) == 3);
            Assert.IsTrue(A.GetValue(1, 0) == 4);
            Assert.IsTrue(A.GetValue(1, 1) == 5);
            Assert.IsTrue(A.GetValue(1, 2) == 6);
            Assert.IsTrue(A.GetValue(2, 0) == 7);
            Assert.IsTrue(A.GetValue(2, 1) == 8);
            Assert.IsTrue(A.GetValue(2, 2) == 9);
            Assert.IsTrue(A.GetValue(3, 0) == 10);
            Assert.IsTrue(A.GetValue(3, 1) == 11);
            Assert.IsTrue(A.GetValue(3, 2) == 12);

            Assert.IsTrue(A.S.NumberOfElements == 12);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
        }
        [TestMethod]
        public void ArrayCreateMultiDimUIntTest() {
            var SysArr = new uint[,] {
                { 1, 2, 3  },
                { 4, 5, 6  },
                { 7, 8, 9  },
                { 10, 11, 12 },
            };
            Array<uint> A = SysArr;

            Assert.IsNotNull(A);
            Assert.IsTrue(A.S[0] == 4);
            Assert.IsTrue(A.S[1] == 3);
            Assert.IsTrue(A.S[2] == 1);
            Assert.IsTrue(A.S[3] == 1);
            Assert.IsTrue(A.S.NumberOfDimensions == 2);
            Assert.IsTrue(A.S.NumberOfElements == 12);

            Assert.IsTrue(A.GetValue(0) == 1);
            Assert.IsTrue(A.GetValue(1) == 4);
            Assert.IsTrue(A.GetValue(2) == 7);
            Assert.IsTrue(A.GetValue(3) == 10);
            Assert.IsTrue(A.GetValue(4) == 2);
            Assert.IsTrue(A.GetValue(5) == 5);
            Assert.IsTrue(A.GetValue(6) == 8);
            Assert.IsTrue(A.GetValue(7) == 11);
            Assert.IsTrue(A.GetValue(8) == 3);
            Assert.IsTrue(A.GetValue(9) == 6);
            Assert.IsTrue(A.GetValue(10) == 9);
            Assert.IsTrue(A.GetValue(11) == 12);

            Assert.IsTrue(A.GetValue(0, 0) == 1);
            Assert.IsTrue(A.GetValue(0, 1) == 2);
            Assert.IsTrue(A.GetValue(0, 2) == 3);
            Assert.IsTrue(A.GetValue(1, 0) == 4);
            Assert.IsTrue(A.GetValue(1, 1) == 5);
            Assert.IsTrue(A.GetValue(1, 2) == 6);
            Assert.IsTrue(A.GetValue(2, 0) == 7);
            Assert.IsTrue(A.GetValue(2, 1) == 8);
            Assert.IsTrue(A.GetValue(2, 2) == 9);
            Assert.IsTrue(A.GetValue(3, 0) == 10);
            Assert.IsTrue(A.GetValue(3, 1) == 11);
            Assert.IsTrue(A.GetValue(3, 2) == 12);

            Assert.IsTrue(A.S.NumberOfElements == 12);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
        }
        [TestMethod]
        public void ArrayCreateMultiDimByteTest() {
            var SysArr = new byte[,] {
                { 1, 2, 3  },
                { 4, 5, 6  },
                { 7, 8, 9  },
                { 10, 11, 12 },
            };
            Array<byte> A = SysArr;

            Assert.IsNotNull(A);
            Assert.IsTrue(A.S[0] == 4);
            Assert.IsTrue(A.S[1] == 3);
            Assert.IsTrue(A.S[2] == 1);
            Assert.IsTrue(A.S[3] == 1);
            Assert.IsTrue(A.S.NumberOfDimensions == 2);
            Assert.IsTrue(A.S.NumberOfElements == 12);

            Assert.IsTrue(A.GetValue(0) == 1);
            Assert.IsTrue(A.GetValue(1) == 4);
            Assert.IsTrue(A.GetValue(2) == 7);
            Assert.IsTrue(A.GetValue(3) == 10);
            Assert.IsTrue(A.GetValue(4) == 2);
            Assert.IsTrue(A.GetValue(5) == 5);
            Assert.IsTrue(A.GetValue(6) == 8);
            Assert.IsTrue(A.GetValue(7) == 11);
            Assert.IsTrue(A.GetValue(8) == 3);
            Assert.IsTrue(A.GetValue(9) == 6);
            Assert.IsTrue(A.GetValue(10) == 9);
            Assert.IsTrue(A.GetValue(11) == 12);

            Assert.IsTrue(A.GetValue(0, 0) == 1);
            Assert.IsTrue(A.GetValue(0, 1) == 2);
            Assert.IsTrue(A.GetValue(0, 2) == 3);
            Assert.IsTrue(A.GetValue(1, 0) == 4);
            Assert.IsTrue(A.GetValue(1, 1) == 5);
            Assert.IsTrue(A.GetValue(1, 2) == 6);
            Assert.IsTrue(A.GetValue(2, 0) == 7);
            Assert.IsTrue(A.GetValue(2, 1) == 8);
            Assert.IsTrue(A.GetValue(2, 2) == 9);
            Assert.IsTrue(A.GetValue(3, 0) == 10);
            Assert.IsTrue(A.GetValue(3, 1) == 11);
            Assert.IsTrue(A.GetValue(3, 2) == 12);

            Assert.IsTrue(A.S.NumberOfElements == 12);
            Assert.IsTrue(A.S.StorageOrder == StorageOrders.RowMajor);
        }

        [TestMethod]
        public void ArrayGetValueTestComplex() {

            Array<complex> A = new complex[] {
                new complex(1,2), new complex(3,4)
            };

            Assert.IsNotNull(A);
            Assert.IsTrue(A.S[0] == 2);
            Assert.IsTrue(A.S[1] == 1);
            Assert.IsTrue(A.S[2] == 1);
            Assert.IsTrue(A.S[3] == 1);
            Assert.IsTrue(A.S.NumberOfDimensions == 2);
            Assert.IsTrue(A.S.NumberOfElements == 2);

            Assert.IsTrue(A.GetValue(0) == new complex(1, 2));
            Assert.IsTrue(A.GetValue(1) == new complex(3, 4));

        }

        [TestMethod]
        public void ArrayGetValue_Generic_simple() {
            Array<double> A = 1;
            Assert.IsTrue(A.GetValue(0) == 1);

            Array<string> S = "hello";
            Assert.IsTrue(S.GetValue(0) == "hello");

            //Cell C = (Cell)A;
            //Assert.IsTrue(C.GetValue(0).Equals(A));

            Logical L = true;
            Assert.IsTrue(L.GetValue(0) == true); 

        }

        [TestMethod]
        public void ArrayEqualTest() {

            var SysArr = new uint[,] {
                { 1, 2, 3  },
                { 4, 5, 6  },
                { 7, 8, 9  },
                { 10, 11, 12 },
            };
            var SysArr2 = new int[,] {
                { 1, 2, 3  },
                { 4, 5, 6  },
                { 7, 8, 9  },
                { 10, 11, 12 },
            };
            Array<uint> A = SysArr;
            Array<uint> B = SysArr;
            Array<int> C = SysArr2; 

            Assert.IsTrue(A.Equals(B));
            Assert.IsFalse(A.Equals(2)); 
            Assert.IsFalse(A.Equals(C));

            A = 10;
            Assert.IsFalse(A.Equals(B));
            Assert.IsFalse(A.Equals(C));

            B = 10;

            Assert.IsTrue(A.Equals(B));
            Assert.IsTrue(B.Equals(A));

            A = null;
            B = 10;

            Assert.IsFalse(B.Equals(A));

            A = 10;
            B = 10;
            A.S.SetAll(1, 1, 1, 1, 1, 1);

            A.S.SetAll(0, 1);
            Assert.IsFalse(A.Equals(B)); 
            Assert.IsFalse(B.Equals(A)); 
            Assert.IsFalse(A.Equals(0));
            Assert.IsFalse(((Array<int>)0).Equals(A));

            B.S.SetAll(0, 3);  // strides are not considered anyway
            Assert.IsTrue(A.Equals(B)); 
            Assert.IsTrue(B.Equals(A));

        }
        [TestMethod]
        public void ArrayEqualTest_empty() {
            Array<double> A = new double[0], A2 = new double[0];
            Array<uint> B = new uint[0];
            Array<uint> C = 1;

            Assert.IsFalse(A.Equals(B));
            Assert.IsFalse(A.Equals(C));
            Assert.IsTrue(A.Equals(A));

            Assert.IsFalse(B.Equals(A));
            Assert.IsFalse(B.Equals(C));
            Assert.IsTrue(B.Equals(B));

            Assert.IsFalse(C.Equals(A));
            Assert.IsFalse(C.Equals(B));
            Assert.IsTrue(C.Equals(C));

            Assert.IsTrue(A.Equals(A2)); 

        }
        [TestMethod]
        public void ArrayConversionTest() {

            Logical A = true;

            var I = (InLogical)A;                    // no increase!
            Assert.IsTrue(!object.ReferenceEquals(I.Storage, A.Storage));

            using (Scope.Enter(I)) {
                //Assert.IsTrue(I.Storage.ScopeState == 0);
            }

            var R = A;
            Assert.IsTrue(object.ReferenceEquals(R.Storage, A.Storage));
            Assert.IsTrue(R.S.NumberOfElements == 1); // release

            var O = (OutLogical)A;
            Assert.IsTrue(object.ReferenceEquals(O.Storage, A.Storage));

        }

        [TestMethod]
        public void ScopeWithArrayStyle() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                // numpy style is valid here...
                Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);

                using (Scope.Enter(ArrayStyles.ILNumericsV4)) {
                    // in the scope: array style is ILNumericsV4

                    Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);

                    // the style may be changed again ... 
                    Settings.ArrayStyle = ArrayStyles.numpy;
                    Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);
                    Settings.ArrayStyle = ArrayStyles.ILNumericsV4;
                    Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
                    // ...
                }
                // array style which was valid immediately before the scope began is restored 
                Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);

            }
        }
        [TestMethod]
        public void ScopeWithArrayStyleAnd1Array() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                // numpy style is valid here...
                Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);

                using (Scope.Enter(null, ArrayStyles.ILNumericsV4)) {
                    // in the scope: array style is ILNumericsV4

                    Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);

                    // the style may be changed again ... 
                    Settings.ArrayStyle = ArrayStyles.numpy;
                    Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);
                    Settings.ArrayStyle = ArrayStyles.ILNumericsV4;
                    Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
                    // ...
                }
                // array style which was valid immediately before the scope began is restored 
                Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);

            }
        }
        [TestMethod]
        public void ScopeWithArrayStyleAnd2Array() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                // numpy style is valid here...
                Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);
                Array<double> A = 1, B = 2; 
                using (Scope.Enter(A, B, ArrayStyles.ILNumericsV4)) {
                    // in the scope: array style is ILNumericsV4

                    Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);

                    // the style may be changed again ... 
                    Settings.ArrayStyle = ArrayStyles.numpy;
                    Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);
                    Settings.ArrayStyle = ArrayStyles.ILNumericsV4;
                    Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
                    // ...
                }
                // array style which was valid immediately before the scope began is restored 
                Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);

            }
        }
        [TestMethod]
        public void ScopeWithArray2StylesAnd2Array() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                // numpy style is valid here...
                Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);
                Array<double> A = 1, B = 2; 
                using (Scope.Enter(A, B, ArrayStyles.ILNumericsV4)) {
                    // in the scope: array style is ILNumericsV4

                    Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);

                    // the style may be changed again ... 
                    using (Scope.Enter(A, B, ArrayStyles.numpy)) {
                        Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);
                        //Settings.ArrayStyle = ArrayStyles.ILNumericsV4;
                    }
                    Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.ILNumericsV4);
                    // ...
                }
                // array style which was valid immediately before the scope began is restored 
                Assert.IsTrue(Settings.ArrayStyle == ArrayStyles.numpy);

            }
        }

    }
}
