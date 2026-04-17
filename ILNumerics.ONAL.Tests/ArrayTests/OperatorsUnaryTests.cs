using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;
using ILNumerics; 

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class OperatorsUnaryTests {
        [TestMethod]
        public void NotOperatorTestSimple() {

            Array<double> A = counter(1.0, 1.0, 5, 4, 3);
            Array<double> B = A[A % 2 == 0];
            Array<double> Res1 = counter(2.0, 2.0, 30);
            Assert.IsTrue(B.Equals(Res1));

            B.a = A[!(A % 2 == 0)];
            Assert.IsTrue(B.Equals(counter(1.0, 2.0, 30)));
        }
        [TestMethod]
        public void NotOperator_NumTrues() {
            Logical L = counter(0.0, 1.0, 5, 4, 3) % 3 == 0;

            Assert.IsTrue(L.Storage.NumberTrues == 20);
            Assert.IsTrue((!L).Storage.NumberTrues == 40);

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                // scalar
                L = false;
                Assert.IsTrue(L.Storage.NumberTrues == 0);
                Assert.IsTrue((!L).Storage.NumberTrues == 1);
            }

            // inplace 
            Assert.IsTrue((counter(0.0, 1.0, 5, 4, 3) % 4 == 0).Storage.NumberTrues == 15);
            Assert.IsTrue((!(counter(0.0, 1.0, 5, 4, 3) % 4 == 0)).Storage.NumberTrues == 45);
        }
        //[TestMethod]  // DISABLED: from v7.1, not is no longer working inplace, because of MM in ACC.
        //public void NotOperator_Implace() {
        //    var L = counter(0.0, 1.0, 5, 4, 3) % 4 == 0;
        //    Assert.IsTrue(L.Storage.NumberTrues == 15);

        //    var L2 = !L; 
        //    Assert.IsTrue(ReferenceEquals(L, L2));
        //    Assert.IsTrue(L2.Storage.NumberTrues == 45); 
        //}

        [TestMethod]
        public void NegateOperator_AllTypes() {
            Array<int> Aint = toint32(counter(1.0, 1.0, 5, 4, 3));
            Assert.IsTrue((-Aint).Equals(toint32(counter(-1.0, -1.0, 5, 4, 3))));

            Array<uint> Auint = touint32(counter(1.0, 1.0, 5, 4, 3));
            Assert.IsTrue((-Auint).Equals(touint32(counter(-1.0, -1.0, 5, 4, 3))));

            Array<long> Along = toint64(counter(1.0, 1.0, 5, 4, 3));
            Assert.IsTrue((-Along).Equals(toint64(counter(-1.0, -1.0, 5, 4, 3))));

            //Array<ulong> Aulong = touint64(counter(1.0, 1.0, 5, 4, 3));
            //Assert.IsTrue((-Aulong).Equals(touint64(counter(-1.0, -1.0, 5, 4, 3))));

            Array<short> Ashort = toint16(counter(1.0, 1.0, 5, 4, 3));
            Assert.IsTrue((-Ashort).Equals(toint16(counter(-1.0, -1.0, 5, 4, 3))));

            Array<ushort> Aushort = touint16(counter(1.0, 1.0, 5, 4, 3));
            Assert.IsTrue((-Aushort).Equals(touint16(counter(-1.0, -1.0, 5, 4, 3))));

            Array<byte> Abyte = touint8(counter(1.0, 1.0, 5, 4, 3));
            Assert.IsTrue((-Abyte).Equals(touint8(counter(-1.0, -1.0, 5, 4, 3))));

            Array<sbyte> Asbyte = toint8(counter(1.0, 1.0, 5, 4, 3));
            Assert.IsTrue((-Asbyte).Equals(toint8(counter(-1.0, -1.0, 5, 4, 3))));

            Array<float> Afloat = tosingle(counter(1.0, 1.0, 5, 4, 3));
            Assert.IsTrue((-Afloat).Equals(tosingle(counter(-1.0, -1.0, 5, 4, 3))));

            Array<double> Adouble = counter(1.0, 1.0, 5, 4, 3);
            Assert.IsTrue((-Adouble).Equals(counter(-1.0, -1.0, 5, 4, 3)));
        }

        [TestMethod]
        public void BitwiseNegate_Operators_AllTypes() {

            Array<int> Aint = toint32(counter(1.0, 1.0, 5, 4, 3));
            Assert.IsTrue((~Aint).Equals(toint32(counter(-1.0, -1.0, 5, 4, 3) - 1)));

            Array<uint> Auint = touint32(counter(1.0, 1.0, 5, 4, 3));
            Assert.IsTrue((~Auint).Equals(touint32(counter(-1.0, -1.0, 5, 4, 3) - 1)));

            Array<long> Along = toint64(counter(1.0, 1.0, 5, 4, 3));
            Assert.IsTrue((~Along).Equals(toint64(counter(-1.0, -1.0, 5, 4, 3) - 1)));

            Array<ulong> Aulong = touint64(counter(1.0, 1.0, 5, 4, 3));
            Assert.IsTrue((~Aulong).Equals(touint64(counter(-1.0, -1.0, 5, 4, 3) - 1)));

            Array<short> Ashort = toint16(counter(1.0, 1.0, 5, 4, 3));
            Assert.IsTrue((~Ashort).Equals(toint16(counter(-1.0, -1.0, 5, 4, 3) - 1)));

            Array<ushort> Aushort = touint16(counter(1.0, 1.0, 5, 4, 3));
            Assert.IsTrue((~Aushort).Equals(touint16(counter(-1.0, -1.0, 5, 4, 3) - 1)));

            Array<byte> Abyte = touint8(counter(1.0, 1.0, 5, 4, 3));
            Assert.IsTrue((~Abyte).Equals(touint8(counter(-1.0, -1.0, 5, 4, 3) - 1)));

            Array<sbyte> Asbyte = toint8(counter(1.0, 1.0, 5, 4, 3));
            Assert.IsTrue((~Asbyte).Equals(toint8(counter(-1.0, -1.0, 5, 4, 3) - 1)));


        }

        private void isOperated<T>(InArray<T> AR, InArray<T> AS, Func<T,T> p) {
            using (Scope.Enter(AR, AS)) {
                var itS = AS.GetEnumerator();
                var isR = AR.GetEnumerator(); 
                while (itS.MoveNext() && isR.MoveNext()) {
                    Assert.IsTrue(Equals(isR.Current, p(itS.Current))); 
                }
            }
        }
    }
}
