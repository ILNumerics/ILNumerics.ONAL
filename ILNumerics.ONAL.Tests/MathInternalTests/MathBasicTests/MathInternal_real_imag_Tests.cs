using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using static ILNumerics.Globals; 

namespace ILNumerics.Core.UnitTests.MathInternalTests {
    [TestClass]
    public class MathInternal_real_imag_Tests {

        [TestMethod]
        public void MathInternal_realImag_Simple() {
            Array<complex> A = new complex[,] {
                { new complex(1,2), new complex(3,4), new complex(5,6) },
                { new complex(7,8), new complex(9,10), new complex(11,12) }
            };

            Array<double> R = real(A);
            Array<double> I = imag(A);

            Assert.IsTrue(R.Equals(counter(1.0, 2.0, 2, 3, StorageOrders.RowMajor)));
            Assert.IsTrue(I.Equals(counter(2.0, 2.0, 2, 3, StorageOrders.RowMajor)));
        }
        [TestMethod]
        public void MathInternal_realImag_RetT() {
            Array<complex> A = new complex[,] {
                { new complex(1,2), new complex(3,4), new complex(5,6) },
                { new complex(7,8), new complex(9,10), new complex(11,12) }
            };

            Array<double> R = real(A[full, full]);
            Array<double> I = imag(A[full, full]);

            Assert.IsTrue(R.Equals(counter(1.0, 2.0, 2, 3, StorageOrders.RowMajor)));
            Assert.IsTrue(I.Equals(counter(2.0, 2.0, 2, 3, StorageOrders.RowMajor)));
        }

        [TestMethod]
        public void MathInternal_realImag_RetTScalar() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Array<complex> A = new complex[,] {
                    { new complex(1,2), new complex(3,4), new complex(5,6) },
                    { new complex(7,8), new complex(9,10), new complex(11,12) }
                };

                Array<double> R = real(A[1, -1]);
                Array<double> I = imag(A[1, -1]);

                Assert.IsTrue(R.Equals(counter(11.0, 2.0, 1, StorageOrders.RowMajor)));
                Assert.IsTrue(I.Equals(counter(12.0, 2.0, 1, StorageOrders.RowMajor)));
            }
        }
        [TestMethod]
        public void MathInternal_realImag_RetTScalar_NoAcc() {
            //ILN(enabled=false)
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Array<complex> A = new complex[,] {
                    { new complex(1,2), new complex(3,4), new complex(5,6) },
                    { new complex(7,8), new complex(9,10), new complex(11,12) }
                };

                Array<double> R = real(A[1, -1]);
                Array<double> I = imag(A[1, -1]);

                Assert.IsTrue(R.Equals(counter(11.0, 2.0, 1, StorageOrders.RowMajor)));
                Assert.IsTrue(I.Equals(counter(12.0, 2.0, 1, StorageOrders.RowMajor)));
            }
            //ILN(enabled=true)
        }
        [TestMethod]
        public void MathInternal_realImag_RetTempty() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Array<complex> A = new complex[,] {
                    { new complex(1,2), new complex(3,4), new complex(5,6) },
                    { new complex(7,8), new complex(9,10), new complex(11,12) }
                };

                Array<double> R = real(A["", -1]);
                Array<double> I = imag(A["", -1]);

                Assert.IsTrue(R.Equals(counter(11.0, 2.0, 0, StorageOrders.RowMajor)));
                Assert.IsTrue(I.Equals(counter(12.0, 2.0, 0, StorageOrders.RowMajor)));
            }
        }

        [TestMethod]
        public void Ccomplex_emptyVectorTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = empty<double>(0);
                Array<double> B = empty<double>(0);
                Array<complex> R = ccomplex(A,B);

                Assert.IsTrue(R.IsEmpty);
                Assert.IsTrue(R.S.NumberOfDimensions == 1);
                Assert.IsTrue(R.S.NumberOfElements == 0);
            }
        }


        [TestMethod]
        public void MathInternal_ccomplexLargeTest() {

            Array<double> A = counter(1.0, 1.0, 100, 200, 50); 
            Array<double> B = -counter(1.0, 1.0, 100, 200, 1);
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {

                Array<complex> C = ccomplex(A, B);
                Assert.IsTrue(C.S.IsSameShape(A.S));

                Array<double> AC = real(C);
                Array<double> BC = imag(C);

                Assert.IsTrue(AC.Equals(A));
                Assert.IsTrue(BC.Equals(B + zeros<double>(1, 1, 50)));
            }
        }
        [TestMethod]
        public void MathInternal_FccomplexLargeTest() {

            Array<float> A = counter<float>(1.0f, 1.0f, 100, 200, 50); 
            Array<float> B = -counter<float>(1.0f, 1.0f, 100, 1, 50);
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {

                Array<fcomplex> C = ccomplex(A, B);
                Assert.IsTrue(C.S.IsSameShape(A.S));

                Array<float> AC = real(C);
                Array<float> BC = imag(C);

                Assert.IsTrue(AC.Equals(A));
                Assert.IsTrue(BC.Equals(B + zeros<float>(1,200,1)));
            }
        }
    }
}
