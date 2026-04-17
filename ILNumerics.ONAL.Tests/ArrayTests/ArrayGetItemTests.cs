using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILNumerics.Core.UnitTests {

    [TestClass]
    public class ArrayGetItemTests {

        [TestMethod]
        public void ArrayGetItemTest_Scalar() {

            Array<double> A = 22;

            var res = A.GetItem(0);

            Assert.IsTrue(res != null);
            Assert.IsTrue(res is double);
            Assert.IsTrue(res.Equals(22.0)); 

            try {
                A.GetItem(1);
                Assert.Fail("Index out of range should throw"); 
            } catch(IndexOutOfRangeException) {

            }
            Assert.IsFalse(res.Equals((int)22));
        }


        [TestMethod]
        public void ArrayGetItemTest_ScalarTypes() {

            Assert.IsTrue(((Array<double>)1).GetItem(0).Equals((double)1)); 
            Assert.IsTrue(((Array<float>)1).GetItem(0).Equals((float)1));
            Assert.IsTrue(((Array<ushort>)1).GetItem(0).Equals((ushort)1));
            Assert.IsTrue(((Array<short>)1).GetItem(0).Equals((short)1));
            Assert.IsTrue(((Array<fcomplex>) new fcomplex(1, -1)).GetItem(0).Equals(new fcomplex(1, -1)));
            Assert.IsTrue(((Array<complex>)new complex(1, -1)).GetItem(0).Equals(new complex(1, -1)));
            Assert.IsTrue(((Array<ulong>)1).GetItem(0).Equals((ulong)1));
            Assert.IsTrue(((Array<long>)1).GetItem(0).Equals((long)1));
            Assert.IsTrue(((Array<uint>)1).GetItem(0).Equals((uint)1));
            Assert.IsTrue(((Array<int>)1).GetItem(0).Equals((int)1));
            Assert.IsTrue(((Array<sbyte>)1).GetItem(0).Equals((sbyte)1));
            Assert.IsTrue(((Array<byte>)1).GetItem(0).Equals((byte)1));
            Assert.IsTrue(((Array<bool>)true).GetItem(0).Equals(true));

        }
    }
}
