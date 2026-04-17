using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.InteropServices;
using ILNumerics.Core.Segments;
using System.Security.Cryptography;
//ILN(enabled=false)

namespace ILNumerics.Core.UnitTests {

    [TestClass]
    public class AssignSynchTests {

        [TestMethod]
        public void AssignLocalFromLocalSynchTest() {

            Array<double> A = 1.0;
            Array<double> B = 2.0;

            A.a = B.T;

            Assert.IsTrue(A.Equals(2.0));

            Assert.IsTrue(B.Equals(2.0));

            Assert.IsTrue(!object.Equals(A.Storage, B.Storage));

        }

        [TestMethod]
        public void AssignLocalFromInArraySynchTest() {

            Array<double> A = 1.0;
            Array<double> B = 2.0;

            void innerTest(InArray<double> b) {
                using (Scope.Enter(b)) {
                    A.a = b;

                }
            }
            innerTest(B);
            Assert.IsTrue(A.Equals(2.0));

            Assert.IsTrue(B.Equals(2.0));

            Assert.IsTrue(!object.Equals(A.Storage, B.Storage));

        }

        [TestMethod]
        public void AssignLocalFromOutArraySynchTest() {

            Array<double> A = 1.0;
            Array<double> B = 2.0;

            void innerTest(OutArray<double> b) {
                using (Scope.Enter()) {
                    A.a = b;
                }
            }
            innerTest(B);
            Assert.IsTrue(A.Equals(2.0));

            Assert.IsTrue(B.Equals(2.0));

            Assert.IsTrue(!object.Equals(A.Storage, B.Storage));

        }

        [TestMethod]
        public void AssignLocalFromNullSynchTest() {
            
            using (Scope.Enter(arrayStyle: ArrayStyles.ILNumericsV4)) {
                Array<double> A = 1.0;

                A.a = null;

                Assert.IsTrue(A.IsEmpty);
                Assert.IsTrue(A.S.NumberOfDimensions == 2);
                Assert.IsTrue(A.S.NumberOfElements == 0);
            }

            using (Scope.Enter(arrayStyle: ArrayStyles.numpy)) {
                Array<double> A = 1.0;

                A.a = null;

                Assert.IsTrue(A.IsEmpty);
                Assert.IsTrue(A.S.NumberOfDimensions == 1);
                Assert.IsTrue(A.S.NumberOfElements == 0);
            }

        }



    }
}
