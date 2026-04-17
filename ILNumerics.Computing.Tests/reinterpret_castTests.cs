using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics; 
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.Tests {

    [TestClass]
    public class _reinterpret_castTests {

        [TestMethod]
        public void reinterpret_castExpandTests001() {

            Array<int> A = ones<int>(2, 3, StorageOrders.ColumnMajor);
            Array<byte> R = reinterpret_cast<int, byte>(A);

            Assert.IsTrue(R.S.NumberOfDimensions == 3);
            Assert.IsTrue(R.S.NumberOfElements == 24);

            Assert.IsTrue(R.S[0] == 2);
            Assert.IsTrue(R.S[1] == 3);
            Assert.IsTrue(R.S[2] == 4);

            Assert.IsTrue(allall(R[full, full, 0] == ones<byte>(2, 3)));
            Assert.IsTrue(allall(R[full, full, 1] == zeros<byte>(2, 3)));
            Assert.IsTrue(allall(R[full, full, 2] == zeros<byte>(2, 3)));
            Assert.IsTrue(allall(R[full, full, 3] == zeros<byte>(2, 3)));

        }

        [TestMethod]
        public void reinterpret_castCompressTests() {

            Array<int> A = ones<int>(4, 3, StorageOrders.ColumnMajor);
            Array<long> R = reinterpret_cast<int, long>(A);

            Assert.IsTrue(R.S.NumberOfDimensions == 2);
            Assert.IsTrue(R.S.NumberOfElements == 6);

            Assert.IsTrue(R.S[0] == 2);
            Assert.IsTrue(R.S[1] == 3);

            Assert.IsTrue(allall(R == ones<long>(2, 3) * 0x0000000100000001));

        }

        [TestMethod]
        public void reinterpret_castEqualSizeTests() {

            Array<int> A = ones<int>(4, 3, StorageOrders.ColumnMajor) * -1;
            Array<uint> R = reinterpret_cast<int, uint>(A);

            Assert.IsTrue(R.S.NumberOfDimensions == 2);
            Assert.IsTrue(R.S.NumberOfElements == 12);

            Assert.IsTrue(R.S[0] == 4);
            Assert.IsTrue(R.S[1] == 3);

            Assert.IsTrue(allall(R == ones<uint>(4, 3) * 0xffffffff));

        }

        [TestMethod]
        public void reinterpret_castComplexRoundtripTest() {

            Array<double> A = rand(10, 20, 2, StorageOrders.RowMajor);  // forces complex compression along 3rd dimension
            Array<complex> C = reinterpret_cast<double, complex>(A);

            Array<double> R_ = reinterpret_cast<complex, double>(squeeze(C));

            Assert.IsTrue(allall(R_ == A)); 

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "should throw due to: continous dimension #0 length is not even / divisable by 2")]
        public void ReinterpreteWrongDimensionLengthTest() {

            Array<float> A = ones<float>(5, 4, StorageOrders.ColumnMajor);
            Array<double> R = reinterpret_cast<float, double>(A); //throws: continues dimension #0 length is not even / divisable by 2 
 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "should throw due to: no 1-strided dimension found")]
        public void ReinterpreteWrongStrideTest() {

            Array<int> A = arange<int>(1, 100).Reshape(20, 5)[r(1, 2, 7), full];
            Array<long> R = reinterpret_cast<int, long>(A); // throws: no 1-strided dimension
 
        }

#pragma warning disable CS0169
        struct ThreeBytes {
            byte byte1;
            byte byte2;
            byte byte3;
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "should throw due to: incompatible element type lengths")]
        public void ReinterpreteWrongElementTypesTest() {
            // ??? not possible ? 
            Array<double> A = ones<double>(10, 10, StorageOrders.ColumnMajor);
            Array<ThreeBytes> B = reinterpret_cast<double, ThreeBytes>(A); 
        }

    }
}
