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
using ILNumerics.Core.StorageLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.Globals;
using static ILNumerics.ILMath; 

namespace ILNumerics.Core.UnitTests {

    //ILN(enabled=false)  //disable accelerator for this file. It changes execution order and may lead to wrong assumptions on reference counts here!
    [TestClass]
    public class MemoryManagementTests {
        [TestMethod]
        public void InArrayDisposedTest() {

            Array<double> A = new double[] { 1, 2, 3 };
            //Assert.IsTrue(A.Storage.ScopeState == 0); 

            InArray<double> I = A;  // as if we provided A to myFunc
            using (Scope.Enter(I)) { // only here ref.count gets increased
                //Assert.IsTrue(A.Storage.ScopeState == 0);
            }
            // all is as before

            using (Scope.Enter(I)) { // does not consider any scopes! A keeps I alive.
            }

            var R = myFunc(I);


        }

        [TestMethod]
        public void InArrayDisposedMultipleScopesTest() {
            InArray<double> I1 = 10; // "function parameters"
            Array<double> A, B = 1; 
            using (Scope.Enter(I1)) {


                using (Scope.Enter()) { 

                    A = new double[] { 1, 2, 3 };

                    I1 = A;  // this assignment could be made in the expectation
                             // that A is now 'delivered' to the outer scope. 
                             // But it is not! A and I1 are released when the current
                             // scope ends. Solution: B.a = A; 

                    using (Scope.Enter(I1)) { // I1 does not enter this scope, it is not touched
                    }
                    // all is as before

                } // release A, I1 dies with it

            } // release I1



        }


        private Array<double> myFunc(InArray<double> A) {
            using (Scope.Enter()) {
                Assert.IsTrue(A.ReferenceCount > 0);
                A = new double[] { 10, 20, 30 };
                return A + 1;
            }
        }
        private void myFuncReleaseA(InArray<double> A) {
            using (Scope.Enter(A)) {
                Assert.IsTrue(A.ReferenceCount > 0);
                //Assert.IsTrue(A.Storage.ScopeCount == 2);  // general state of scope flag for in parameters
            }
        }

        [TestMethod]
        public void ScopeTest_RetArray2InArrayLocal() {
            // make a ret array
            // assign it to an inarry (parameter)
            // make sure it is still valid after the inarray came back from another function
            InArray<double> In = null;
            using (Scope.Enter()) {
                var A = counter<double>(1.0, 1.0, 4, 3);

                In = A;

                Assert.IsTrue(Scope.Context.CurScope > 0);
                var wr = Scope.Context.Arrays[Scope.Context.CurArray];
                Assert.IsTrue(wr != null);
                IStorage target;
                Assert.IsTrue(wr.storage.TryGetTarget(out target));
                Assert.IsTrue(object.ReferenceEquals(target, In.Storage));

                myFuncReleaseA(In);

                Assert.IsTrue(In.ReferenceCount > 0); //not disposed yet! 
                // use it multiple times...
                myFuncReleaseA(In);
                Assert.IsTrue(In.ReferenceCount > 0); //not disposed yet! 
                //Assert.IsTrue(In.Storage.m_scopeState > 0); // basically always == 2 as long as it lays in the outermost scope

            }
            //Assert.IsTrue(In.Storage.m_scopeState == 0); //not disposed yet! 

        }

        [TestMethod]
        public void ScopeTest_LocArray2InArrayNoLocalScope() {
            // make a local array
            // check that is was already stored into the scope, but only if such scope exists. Don't create one automatically! 
            // assign it to an inarry (parameter)
            // make sure that both are still valid after the inarray came back from another function
            InArray<double> In = null;

            Array<double> A = counter<double>(1.0, 1.0, 4, 3);
            { 

                Assert.IsTrue(Scope.Context.CurScope == 0);
                int curArray = Scope.Context.CurArray;
                Assert.IsTrue(curArray <= 0); 
                //Assert.IsTrue(A.Storage.ScopeState == 0);


                In = A;
                Assert.IsTrue(!ReferenceEquals(A.Storage, In.Storage));
                //Assert.IsTrue(A.Storage.ScopeState == 0); 

                Assert.IsTrue(Scope.Context.CurArray == curArray);
                Assert.IsTrue(Scope.Context.CurScope == 0);

                myFuncReleaseA(In); // does not enter the function scope!

                //But it is still hold by A. 
                //Assert.IsTrue(A.Storage.ScopeState == 0);
                //Assert.IsTrue(In.Storage.ScopeState == 0);
                Assert.IsTrue(!ReferenceEquals(A.Storage, In.Storage));

                // use it multiple times...
                myFuncReleaseA(In); // 'In' does not enter the scope anymore! it is held alive by the local Array<T> scope, though.

                //Assert.IsTrue(A.Storage.ScopeState == 0);
                //Assert.IsTrue(In.Storage.ScopeState == 0);
                Assert.IsTrue(!ReferenceEquals(A.Storage, In.Storage));

                // we can also use A ...
                myFuncReleaseA(A); // 'A' converts, enters the funcs scope, leaves, releases().
                // outcome: A is not affected and the same as before

                //Assert.IsTrue(A.Storage.ScopeState == 0);
                Assert.IsTrue(!ReferenceEquals(A.Storage, In.Storage));

                // ... even multiple times...
                myFuncReleaseA(A); // 'In' does not enter the scope anymore! it is held alive by the local Array<T> scope, though.

                //Assert.IsTrue(A.Storage.ScopeState == 0);
                Assert.IsTrue(!ReferenceEquals(A.Storage, In.Storage));

            }
            //Assert.IsTrue(A.Storage.m_scopeState == 0); //no local scope: A is not disposed  
            // all is cleaned up by GC!
        }

        [TestMethod]
        public void ScopeTest_SameArrayAs2ParametersWithLocalScope() {
            // distL2Sq(A,A); -> A the same after fucntion returned.
            using (Scope.Enter()) {

                Array<double> A = rand(5, 4);
                //Assert.IsTrue(A.Storage.ScopeState == 2);  

                Array<double> B = distL2sq(A, A);

                //Assert.IsTrue(A.Storage.ScopeState == 2);


            }

        }


    }
}
