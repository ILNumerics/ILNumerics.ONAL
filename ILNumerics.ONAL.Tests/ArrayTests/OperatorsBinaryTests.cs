using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath;


namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class OperatorsBinaryTests {

        // test | operators - conflicts with Logicals || ?
        [TestMethod]
        public void LogicalOrOperatorTest() {

            Logical A = true;
            Logical B = false; 
            Assert.IsTrue((bool)A && (bool)!B);
            Assert.IsTrue(A & (bool)!B);

            A.a = new bool[] { false, true }; 
            Logical C = A & !B;
            Assert.IsFalse(C.GetValue(0)); 
            Assert.IsTrue(C.GetValue(1)); 
            //var c = (A & A) == true & (counter(1.0, 1.0, 3) ^ 2) > 0;
        }
        [TestMethod]
        public void BitwiseXOr_Operators_AllTypes() {

            Array<int> Aint = toint32(counter(1.0, 1.0, 5, 4, 3));
            isOperated<int>(Aint ^ 10, Aint, a => a ^ 10);

            Array<uint> Auint = touint32(counter(1.0, 1.0, 5, 4, 3));
            isOperated<uint>(Auint ^ 10, Auint, a => a ^ 10);

            Array<long> Along = toint64(counter(1.0, 1.0, 5, 4, 3));
            isOperated<long>(Along ^ 10, Along, a => a ^ 10);

            Array<ulong> Aulong = touint64(counter(1.0, 1.0, 5, 4, 3));
            isOperated<ulong>(Aulong ^ 10, Aulong, a => a ^ 10);

            Array<short> Ashort = toint16(counter(1.0, 1.0, 5, 4, 3));
            isOperated<short>(Ashort ^ 10, Ashort, a => (short)(a ^ 10));

            Array<ushort> Aushort = touint16(counter(1.0, 1.0, 5, 4, 3));
            isOperated<ushort>(Aushort ^ 10, Aushort, a => (ushort)(a ^ 10));

            Array<byte> Abyte = touint8(counter(1.0, 1.0, 5, 4, 3));
            isOperated<byte>(Abyte ^ 10, Abyte, a => (byte)(a ^ 10));

            Array<sbyte> Asbyte = toint8(counter(1.0, 1.0, 5, 4, 3));
            isOperated<sbyte>(Asbyte ^ 10, Asbyte, a => (sbyte)(a ^ 10));

        }
        [TestMethod]
        public void BitwiseOr_Operators_AllTypes() {

            Array<int> Aint = toint32(counter(1.0, 1.0, 5, 4, 3));
            isOperated<int>(Aint | 10, Aint, a => a | 10);

            Array<uint> Auint = touint32(counter(1.0, 1.0, 5, 4, 3));
            isOperated<uint>(Auint | 10, Auint, a => a | 10);

            Array<long> Along = toint64(counter(1.0, 1.0, 5, 4, 3));
            isOperated<long>(Along | 10, Along, a => a | 10);

            Array<ulong> Aulong = touint64(counter(1.0, 1.0, 5, 4, 3));
            isOperated<ulong>(Aulong | 10, Aulong, a => a | 10);

            Array<short> Ashort = toint16(counter(1.0, 1.0, 5, 4, 3));
            isOperated<short>(Ashort | 10, Ashort, a => (short)(a | 10));

            Array<ushort> Aushort = touint16(counter(1.0, 1.0, 5, 4, 3));
            isOperated<ushort>(Aushort | 10, Aushort, a => (ushort)(a | 10));

            Array<byte> Abyte = touint8(counter(1.0, 1.0, 5, 4, 3));
            isOperated<byte>(Abyte | 10, Abyte, a => (byte)(a | 10));

            Array<sbyte> Asbyte = toint8(counter(1.0, 1.0, 5, 4, 3));
            isOperated<sbyte>(Asbyte | 10, Asbyte, a => (sbyte)(a | 10));

        }

        [TestMethod]
        public void BitwiseAnd_Operators_AllTypes() {

            Array<int> Aint = toint32(counter(1.0, 1.0, 5, 4, 3));
            isOperated<int>(Aint & 10, Aint, a => a & 10);

            Array<uint> Auint = touint32(counter(1.0, 1.0, 5, 4, 3));
            isOperated<uint>(Auint & 10, Auint, a => a & 10);

            Array<long> Along = toint64(counter(1.0, 1.0, 5, 4, 3));
            isOperated<long>(Along & 10, Along, a => a & 10);

            Array<ulong> Aulong = touint64(counter(1.0, 1.0, 5, 4, 3));
            isOperated<ulong>(Aulong & 10, Aulong, a => a & 10);

            Array<short> Ashort = toint16(counter(1.0, 1.0, 5, 4, 3));
            isOperated<short>(Ashort & 10, Ashort, a => (short)(a & 10));

            Array<ushort> Aushort = touint16(counter(1.0, 1.0, 5, 4, 3));
            isOperated<ushort>(Aushort & 10, Aushort, a => (ushort)(a & 10));

            Array<byte> Abyte = touint8(counter(1.0, 1.0, 5, 4, 3));
            isOperated<byte>(Abyte & 10, Abyte, a => (byte)(a & 10));

            Array<sbyte> Asbyte = toint8(counter(1.0, 1.0, 5, 4, 3));
            isOperated<sbyte>(Asbyte & 10, Asbyte, a => (sbyte)(a & 10));

        }

        [TestMethod]
        public void BitwiseShiftRight_Operator_AllTypes() {

            Array<int> Aint1 = toint32(counter(1.0, 1.0, 5, 4, 3));
            isOperated<int>(Aint1 >> 2, Aint1, a => a >> 2);

            Array<uint> Auint = touint32(counter(1.0, 1.0, 5, 4, 3));
            Array<int> Aint2 = 2;
            isOperated<uint>(Auint >> 2, Auint, a => a >> 2);

            Array<long> Along = toint64(counter(1.0, 1.0, 5, 4, 3));
            isOperated<long>(Along >> 2, Along, a => a >> 2);

            Array<ulong> Aulong = touint64(counter(1.0, 1.0, 5, 4, 3));
            isOperated<ulong>(Aulong >> 2, Aulong, a => a >> 2);

            Array<short> Ashort = toint16(counter(1.0, 1.0, 5, 4, 3));
            isOperated<short>(Ashort >> 2, Ashort, a => (short)(a >> 2));

            Array<ushort> Aushort = touint16(counter(1.0, 1.0, 5, 4, 3));
            isOperated<ushort>(Aushort >> 2, Aushort, a => (ushort)(a >> 2));

            Array<byte> Abyte = touint8(counter(1.0, 1.0, 5, 4, 3));
            isOperated<byte>(Abyte >> 2, Abyte, a => (byte)(a >> 2));

            Array<sbyte> Asbyte = toint8(counter(1.0, 1.0, 5, 4, 3));
            isOperated<sbyte>(Asbyte >> 2, Asbyte, a => (sbyte)(a >> 2));


        }
        [TestMethod]
        public void BitwiseShiftLeft_Operator_AllTypes() {

            Array<int> Aint1 = toint32(counter(1.0, 1.0, 5, 4, 3));
            isOperated<int>(Aint1 << 2, Aint1, a => a << 2);

            Array<uint> Auint = touint32(counter(1.0, 1.0, 5, 4, 3));
            Array<int> Aint2 = 2;
            isOperated<uint>(Auint << 2, Auint, a => a << 2);

            Array<long> Along = toint64(counter(1.0, 1.0, 5, 4, 3));
            isOperated<long>(Along << 2, Along, a => a << 2);

            Array<ulong> Aulong = touint64(counter(1.0, 1.0, 5, 4, 3));
            isOperated<ulong>(Aulong << 2, Aulong, a => a << 2);

            Array<short> Ashort = toint16(counter(1.0, 1.0, 5, 4, 3));
            isOperated<short>(Ashort << 2, Ashort, a => (short)(a << 2));

            Array<ushort> Aushort = touint16(counter(1.0, 1.0, 5, 4, 3));
            isOperated<ushort>(Aushort << 2, Aushort, a => (ushort)(a << 2));

            Array<byte> Abyte = touint8(counter(1.0, 1.0, 5, 4, 3));
            isOperated<byte>(Abyte << 2, Abyte, a => (byte)(a << 2));

            Array<sbyte> Asbyte = toint8(counter(1.0, 1.0, 5, 4, 3));
            isOperated<sbyte>(Asbyte << 2, Asbyte, a => (sbyte)(a << 2));

        }
        [TestMethod]
        public void ArithmeticPlus_AllTypes() {

            isOperated<double>(counter(1.0, 1.0, 6, 4, 2) + 2, counter(1.0, 1.0, 6, 4, 2), 2.0, (a, b) => a + b);
            isOperated<float>(counter<float>(1, 1, 6, 4, 2) + 2, counter<float>(1, 1, 6, 4, 2), 2, (a, b) => a + b);
            isOperated<complex>(tocomplex(counter<double>(1, 1, 6, 4, 2)) + new complex(2, 0), tocomplex(counter<double>(1, 1, 6, 4, 2)), new complex(2, 0), (a, b) => a + b);
            isOperated<fcomplex>(tofcomplex(counter<float>(1, 1, 6, 4, 2)) + new fcomplex(2, 0), tofcomplex(counter<float>(1, 1, 6, 4, 2)), new fcomplex(2, 0), (a, b) => a + b);
            isOperated<ulong>(counter<ulong>(1, 1, 6, 4, 2) + 2, counter<ulong>(1, 1, 6, 4, 2), 2, (a, b) => a + b);
            isOperated<long>(counter<long>(1, 1, 6, 4, 2) + 2, counter<long>(1, 1, 6, 4, 2), 2, (a, b) => a + b);
            isOperated<uint>(counter<uint>(1, 1, 6, 4, 2) + 2, counter<uint>(1, 1, 6, 4, 2), 2, (a, b) => a + b);
            isOperated<int>(counter<int>(1, 1, 6, 4, 2) + 2, counter<int>(1, 1, 6, 4, 2), 2, (a, b) => a + b);
            isOperated<ushort>(counter<ushort>(1, 1, 6, 4, 2) + 2, counter<ushort>(1, 1, 6, 4, 2), 2, (a, b) => (ushort)(a + b));
            isOperated<short>(counter<short>(1, 1, 6, 4, 2) + 2, counter<short>(1, 1, 6, 4, 2), 2, (a, b) => (short)(a + b));
            isOperated<byte>(counter<byte>(1, 1, 6, 4, 2) + 2, counter<byte>(1, 1, 6, 4, 2), 2, (a, b) => (byte)(a + b));
            isOperated<sbyte>(counter<sbyte>(1, 1, 6, 4, 2) + 2, counter<sbyte>(1, 1, 6, 4, 2), 2, (a, b) => (sbyte)(a + b));

        }
        [TestMethod]
        public void ArithmeticMultiply_AllTypes() {

            isOperated<double>(counter(1.0, 1.0, 6, 4, 2) * 2, counter(1.0, 1.0, 6, 4, 2), 2.0, (a, b) => a * b);
            isOperated<float>(counter<float>(1, 1, 6, 4, 2) * 2, counter<float>(1, 1, 6, 4, 2), 2, (a, b) => a * b);
            isOperated<complex>(tocomplex(counter<double>(1, 1, 6, 4, 2)) * new complex(2, 0), tocomplex(counter<double>(1, 1, 6, 4, 2)), new complex(2, 0), (a, b) => a * b);
            isOperated<fcomplex>(tofcomplex(counter<float>(1, 1, 6, 4, 2)) * new fcomplex(2, 0), tofcomplex(counter<float>(1, 1, 6, 4, 2)), new fcomplex(2, 0), (a, b) => a * b);
            isOperated<ulong>(counter<ulong>(1, 1, 6, 4, 2) * 2, counter<ulong>(1, 1, 6, 4, 2), 2, (a, b) => a * b);
            isOperated<long>(counter<long>(1, 1, 6, 4, 2) * 2, counter<long>(1, 1, 6, 4, 2), 2, (a, b) => a * b);
            isOperated<uint>(counter<uint>(1, 1, 6, 4, 2) * 2, counter<uint>(1, 1, 6, 4, 2), 2, (a, b) => a * b);
            isOperated<int>(counter<int>(1, 1, 6, 4, 2) * 2, counter<int>(1, 1, 6, 4, 2), 2, (a, b) => a * b);
            isOperated<ushort>(counter<ushort>(1, 1, 6, 4, 2) * 2, counter<ushort>(1, 1, 6, 4, 2), 2, (a, b) => (ushort)(a * b));
            isOperated<short>(counter<short>(1, 1, 6, 4, 2) * 2, counter<short>(1, 1, 6, 4, 2), 2, (a, b) => (short)(a * b));
            isOperated<byte>(counter<byte>(1, 1, 6, 4, 2) * 2, counter<byte>(1, 1, 6, 4, 2), 2, (a, b) => (byte)(a * b));
            isOperated<sbyte>(counter<sbyte>(1, 1, 6, 4, 2) * 2, counter<sbyte>(1, 1, 6, 4, 2), 2, (a, b) => (sbyte)(a * b));

        }
        [TestMethod]
        public void ArithmeticDivide_AllTypes() {

            isOperated<double>(counter(1.0, 1.0, 6, 4, 2) / 2, counter(1.0, 1.0, 6, 4, 2), 2.0, (a, b) => a / b);
            isOperated<float>(counter<float>(10, 1, 6, 4, 2) / 2, counter<float>(10, 1, 6, 4, 2), 2, (a, b) => a / b);
            isOperated<complex>(tocomplex(counter<double>(10, 1, 6, 4, 2)) / new complex(2, 0), tocomplex(counter<double>(10, 1, 6, 4, 2)), new complex(2, 0), (a, b) => a / b);
            isOperated<fcomplex>(tofcomplex(counter<float>(10, 1, 6, 4, 2)) / new fcomplex(2, 0), tofcomplex(counter<float>(10, 1, 6, 4, 2)), new fcomplex(2, 0), (a, b) => a / b);
            isOperated<ulong>(counter<ulong>(10, 1, 6, 4, 2) / 2, counter<ulong>(10, 1, 6, 4, 2), 2, (a, b) => a / b);
            isOperated<long>(counter<long>(10, 1, 6, 4, 2) / 2, counter<long>(10, 1, 6, 4, 2), 2, (a, b) => a / b);
            isOperated<uint>(counter<uint>(10, 1, 6, 4, 2) / 2, counter<uint>(10, 1, 6, 4, 2), 2, (a, b) => a / b);
            isOperated<int>(counter<int>(10, 1, 6, 4, 2) / 2, counter<int>(10, 1, 6, 4, 2), 2, (a, b) => a / b);
            isOperated<ushort>(counter<ushort>(10, 1, 6, 4, 2) / 2, counter<ushort>(10, 1, 6, 4, 2), 2, (a, b) => (ushort)(a / b));
            isOperated<short>(counter<short>(10, 1, 6, 4, 2) / 2, counter<short>(10, 1, 6, 4, 2), 2, (a, b) => (short)(a / b));
            isOperated<byte>(counter<byte>(10, 1, 6, 4, 2) / 2, counter<byte>(10, 1, 6, 4, 2), 2, (a, b) => (byte)(a / b));
            isOperated<sbyte>(counter<sbyte>(10, 1, 6, 4, 2) / 2, counter<sbyte>(10, 1, 6, 4, 2), 2, (a, b) => (sbyte)(a / b));

        }

        [TestMethod]
        public void ArithmeticMinus_AllTypes() {

            isOperated<double>(counter(1.0, 1.0, 6, 4, 2) - 2, counter(1.0, 1.0, 6, 4, 2), 2.0, (a, b) => a - b);
            isOperated<float>(counter<float>(1, 1, 6, 4, 2) - 2, counter<float>(1, 1, 6, 4, 2), 2, (a, b) => a - b);
            isOperated<complex>(tocomplex(counter<double>(1, 1, 6, 4, 2)) - new complex(2, 0), tocomplex(counter<double>(1, 1, 6, 4, 2)), new complex(2, 0), (a, b) => a - b);
            isOperated<fcomplex>(tofcomplex(counter<float>(1, 1, 6, 4, 2)) - new fcomplex(2, 0), tofcomplex(counter<float>(1, 1, 6, 4, 2)), new fcomplex(2, 0), (a, b) => a - b);
            isOperated<ulong>(counter<ulong>(1, 1, 6, 4, 2) - 2, counter<ulong>(1, 1, 6, 4, 2), 2, (a, b) => (ulong)Math.Max((long)a - (long)b, 0));
            isOperated<long>(counter<long>(1, 1, 6, 4, 2) - 2, counter<long>(1, 1, 6, 4, 2), 2, (a, b) => a - b);
            isOperated<uint>(counter<uint>(1, 1, 6, 4, 2) - 2, counter<uint>(1, 1, 6, 4, 2), 2, (a, b) => (uint)Math.Max((int)a - (int)b, 0));
            isOperated<int>(counter<int>(1, 1, 6, 4, 2) - 2, counter<int>(1, 1, 6, 4, 2), 2, (a, b) => a - b);
            isOperated<ushort>(counter<ushort>(1, 1, 6, 4, 2) - 2, counter<ushort>(1, 1, 6, 4, 2), 2, (a, b) => (ushort)Math.Max((short)a - b, 0));
            isOperated<short>(counter<short>(1, 1, 6, 4, 2) - 2, counter<short>(1, 1, 6, 4, 2), 2, (a, b) => (short)(a - b));
            isOperated<byte>(counter<byte>(1, 1, 6, 4, 2) - 2, counter<byte>(1, 1, 6, 4, 2), 2, (a, b) => (byte)Math.Max((sbyte)a - b, 0));
            isOperated<sbyte>(counter<sbyte>(1, 1, 6, 4, 2) - 2, counter<sbyte>(1, 1, 6, 4, 2), 2, (a, b) => (sbyte)(a - b));

        }

        [TestMethod]
        public void ComparisonEqualTo_AllTypes() {

            isOperated<double>(empty<double>(0) == empty<double>(0), empty<double>(0), empty<double>(0), (a, b) => a == b);
            isOperated<double>(counter(1.0, 1.0, 6, 4, 2) == 2, counter(1.0, 1.0, 6, 4, 2), 2.0, (a, b) => a == b);
            isOperated<float>(counter<float>(1, 1, 6, 4, 2) == 2, counter<float>(1, 1, 6, 4, 2), 2, (a, b) => a == b);
            isOperated<complex>(tocomplex(counter<double>(1, 1, 6, 4, 2)) == new complex(2, 0), tocomplex(counter<double>(1, 1, 6, 4, 2)), new complex(2, 0), (a, b) => a == b);
            isOperated<fcomplex>(tofcomplex(counter<float>(1, 1, 6, 4, 2)) == new fcomplex(2, 0), tofcomplex(counter<float>(1, 1, 6, 4, 2)), new fcomplex(2, 0), (a, b) => a == b);
            isOperated<ulong>(counter<ulong>(1, 1, 6, 4, 2) == 2, counter<ulong>(1, 1, 6, 4, 2), 2, (a, b) => a == b);
            isOperated<long>(counter<long>(1, 1, 6, 4, 2) == 2, counter<long>(1, 1, 6, 4, 2), 2, (a, b) => a == b);
            isOperated<uint>(counter<uint>(1, 1, 6, 4, 2) == 2, counter<uint>(1, 1, 6, 4, 2), 2, (a, b) => a == b);
            isOperated<int>(counter<int>(1, 1, 6, 4, 2) == 2, counter<int>(1, 1, 6, 4, 2), 2, (a, b) => a == b);
            isOperated<ushort>(counter<ushort>(1, 1, 6, 4, 2) == 2, counter<ushort>(1, 1, 6, 4, 2), 2, (a, b) => (a == b));
            isOperated<short>(counter<short>(1, 1, 6, 4, 2) == 2, counter<short>(1, 1, 6, 4, 2), 2, (a, b) => (a == b));
            isOperated<byte>(counter<byte>(1, 1, 6, 4, 2) == 2, counter<byte>(1, 1, 6, 4, 2), 2, (a, b) => (a == b));
            isOperated<sbyte>(counter<sbyte>(1, 1, 6, 4, 2) == 2, counter<sbyte>(1, 1, 6, 4, 2), 2, (a, b) => (a == b));

            isOperated(counter<int>(1, 1, 6, 4, 2) > 21 == false, counter<int>(1, 1, 6, 4, 2) > 21, false, (a, b) => a == b);

        }
        [TestMethod]
        public void ComparisonUnEqualTo_AllTypes() {

            isOperated<double>(counter(1.0, 1.0, 6, 4, 2) !=  2, counter(1.0, 1.0, 6, 4, 2), 2.0, (a, b) => a !=  b);
            isOperated<float>(counter<float>(1, 1, 6, 4, 2) !=  2, counter<float>(1, 1, 6, 4, 2), 2, (a, b) => a !=  b);
            isOperated<complex>(tocomplex(counter<double>(1, 1, 6, 4, 2)) !=  new complex(2, 0), tocomplex(counter<double>(1, 1, 6, 4, 2)), new complex(2, 0), (a, b) => a !=  b);
            isOperated<fcomplex>(tofcomplex(counter<float>(1, 1, 6, 4, 2)) !=  new fcomplex(2, 0), tofcomplex(counter<float>(1, 1, 6, 4, 2)), new fcomplex(2, 0), (a, b) => a !=  b);
            isOperated<ulong>(counter<ulong>(1, 1, 6, 4, 2) !=  2, counter<ulong>(1, 1, 6, 4, 2), 2, (a, b) => a !=  b);
            isOperated<long>(counter<long>(1, 1, 6, 4, 2) !=  2, counter<long>(1, 1, 6, 4, 2), 2, (a, b) => a !=  b);
            isOperated<uint>(counter<uint>(1, 1, 6, 4, 2) !=  2, counter<uint>(1, 1, 6, 4, 2), 2, (a, b) => a !=  b);
            isOperated<int>(counter<int>(1, 1, 6, 4, 2) !=  2, counter<int>(1, 1, 6, 4, 2), 2, (a, b) => a !=  b);
            isOperated<ushort>(counter<ushort>(1, 1, 6, 4, 2) !=  2, counter<ushort>(1, 1, 6, 4, 2), 2, (a, b) => (a !=  b));
            isOperated<short>(counter<short>(1, 1, 6, 4, 2) !=  2, counter<short>(1, 1, 6, 4, 2), 2, (a, b) => (a !=  b));
            isOperated<byte>(counter<byte>(1, 1, 6, 4, 2) !=  2, counter<byte>(1, 1, 6, 4, 2), 2, (a, b) => (a !=  b));
            isOperated<sbyte>(counter<sbyte>(1, 1, 6, 4, 2) !=  2, counter<sbyte>(1, 1, 6, 4, 2), 2, (a, b) => (a !=  b));

            isOperated(counter<int>(1, 1, 6, 4, 2) > 21 !=  false, counter<int>(1, 1, 6, 4, 2) > 21, false, (a, b) => a !=  b);

        }

        [TestMethod]
        public void ComparisonLowerThanTo_AllTypes() {

            isOperated<double>(counter(1.0, 1.0, 6, 4, 2) <  2, counter(1.0, 1.0, 6, 4, 2), 2.0, (a, b) => a <  b);
            isOperated<float>(counter<float>(1, 1, 6, 4, 2) <  2, counter<float>(1, 1, 6, 4, 2), 2, (a, b) => a <  b);
            isOperated<complex>(tocomplex(counter<double>(1, 1, 6, 4, 2)) <  new complex(2, 0), tocomplex(counter<double>(1, 1, 6, 4, 2)), new complex(2, 0), (a, b) => a <  b);
            isOperated<fcomplex>(tofcomplex(counter<float>(1, 1, 6, 4, 2)) <  new fcomplex(2, 0), tofcomplex(counter<float>(1, 1, 6, 4, 2)), new fcomplex(2, 0), (a, b) => a <  b);
            isOperated<ulong>(counter<ulong>(1, 1, 6, 4, 2) <  2, counter<ulong>(1, 1, 6, 4, 2), 2, (a, b) => a <  b);
            isOperated<long>(counter<long>(1, 1, 6, 4, 2) <  2, counter<long>(1, 1, 6, 4, 2), 2, (a, b) => a <  b);
            isOperated<uint>(counter<uint>(1, 1, 6, 4, 2) <  2, counter<uint>(1, 1, 6, 4, 2), 2, (a, b) => a <  b);
            isOperated<int>(counter<int>(1, 1, 6, 4, 2) <  2, counter<int>(1, 1, 6, 4, 2), 2, (a, b) => a <  b);
            isOperated<ushort>(counter<ushort>(1, 1, 6, 4, 2) <  2, counter<ushort>(1, 1, 6, 4, 2), 2, (a, b) => (a <  b));
            isOperated<short>(counter<short>(1, 1, 6, 4, 2) <  2, counter<short>(1, 1, 6, 4, 2), 2, (a, b) => (a <  b));
            isOperated<byte>(counter<byte>(1, 1, 6, 4, 2) <  2, counter<byte>(1, 1, 6, 4, 2), 2, (a, b) => (a <  b));
            isOperated<sbyte>(counter<sbyte>(1, 1, 6, 4, 2) <  2, counter<sbyte>(1, 1, 6, 4, 2), 2, (a, b) => (a <  b));

        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void ComparisonLowerThan_NotDefinedLogicalFail() {
            var a = counter<int>(1, 1, 6, 4, 2) > 21 < false;
 
        }
        [TestMethod]
        public void ComparisonLowerEqualTo_AllTypes() {

            isOperated<double>(counter(1.0, 1.0, 6, 4, 2) <=  2, counter(1.0, 1.0, 6, 4, 2), 2.0, (a, b) => a <=  b);
            isOperated<float>(counter<float>(1, 1, 6, 4, 2) <=  2, counter<float>(1, 1, 6, 4, 2), 2, (a, b) => a <=  b);
            isOperated<complex>(tocomplex(counter<double>(1, 1, 6, 4, 2)) <=  new complex(2, 0), tocomplex(counter<double>(1, 1, 6, 4, 2)), new complex(2, 0), (a, b) => a <=  b);
            isOperated<fcomplex>(tofcomplex(counter<float>(1, 1, 6, 4, 2)) <=  new fcomplex(2, 0), tofcomplex(counter<float>(1, 1, 6, 4, 2)), new fcomplex(2, 0), (a, b) => a <=  b);
            isOperated<ulong>(counter<ulong>(1, 1, 6, 4, 2) <=  2, counter<ulong>(1, 1, 6, 4, 2), 2, (a, b) => a <=  b);
            isOperated<long>(counter<long>(1, 1, 6, 4, 2) <=  2, counter<long>(1, 1, 6, 4, 2), 2, (a, b) => a <=  b);
            isOperated<uint>(counter<uint>(1, 1, 6, 4, 2) <=  2, counter<uint>(1, 1, 6, 4, 2), 2, (a, b) => a <=  b);
            isOperated<int>(counter<int>(1, 1, 6, 4, 2) <=  2, counter<int>(1, 1, 6, 4, 2), 2, (a, b) => a <=  b);
            isOperated<ushort>(counter<ushort>(1, 1, 6, 4, 2) <=  2, counter<ushort>(1, 1, 6, 4, 2), 2, (a, b) => (a <=  b));
            isOperated<short>(counter<short>(1, 1, 6, 4, 2) <=  2, counter<short>(1, 1, 6, 4, 2), 2, (a, b) => (a <=  b));
            isOperated<byte>(counter<byte>(1, 1, 6, 4, 2) <=  2, counter<byte>(1, 1, 6, 4, 2), 2, (a, b) => (a <=  b));
            isOperated<sbyte>(counter<sbyte>(1, 1, 6, 4, 2) <=  2, counter<sbyte>(1, 1, 6, 4, 2), 2, (a, b) => (a <=  b));

        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void ComparisonLowerEqual_NotDefinedLogicalFail() {
            var a = counter<int>(1, 1, 6, 4, 2) > 21 <= false;
 
        }
        [TestMethod]
        public void ComparisonGreaterEqualTo_AllTypes() {

            isOperated<double>(counter(1.0, 1.0, 6, 4, 2) >=  2, counter(1.0, 1.0, 6, 4, 2), 2.0, (a, b) => a >=  b);
            isOperated<float>(counter<float>(1, 1, 6, 4, 2) >=  2, counter<float>(1, 1, 6, 4, 2), 2, (a, b) => a >=  b);
            isOperated<complex>(tocomplex(counter<double>(1, 1, 6, 4, 2)) >=  new complex(2, 0), tocomplex(counter<double>(1, 1, 6, 4, 2)), new complex(2, 0), (a, b) => a >=  b);
            isOperated<fcomplex>(tofcomplex(counter<float>(1, 1, 6, 4, 2)) >=  new fcomplex(2, 0), tofcomplex(counter<float>(1, 1, 6, 4, 2)), new fcomplex(2, 0), (a, b) => a >=  b);
            isOperated<ulong>(counter<ulong>(1, 1, 6, 4, 2) >=  2, counter<ulong>(1, 1, 6, 4, 2), 2, (a, b) => a >=  b);
            isOperated<long>(counter<long>(1, 1, 6, 4, 2) >=  2, counter<long>(1, 1, 6, 4, 2), 2, (a, b) => a >=  b);
            isOperated<uint>(counter<uint>(1, 1, 6, 4, 2) >=  2, counter<uint>(1, 1, 6, 4, 2), 2, (a, b) => a >=  b);
            isOperated<int>(counter<int>(1, 1, 6, 4, 2) >=  2, counter<int>(1, 1, 6, 4, 2), 2, (a, b) => a >=  b);
            isOperated<ushort>(counter<ushort>(1, 1, 6, 4, 2) >=  2, counter<ushort>(1, 1, 6, 4, 2), 2, (a, b) => (a >=  b));
            isOperated<short>(counter<short>(1, 1, 6, 4, 2) >=  2, counter<short>(1, 1, 6, 4, 2), 2, (a, b) => (a >=  b));
            isOperated<byte>(counter<byte>(1, 1, 6, 4, 2) >=  2, counter<byte>(1, 1, 6, 4, 2), 2, (a, b) => (a >=  b));
            isOperated<sbyte>(counter<sbyte>(1, 1, 6, 4, 2) >=  2, counter<sbyte>(1, 1, 6, 4, 2), 2, (a, b) => (a >=  b));
            // 20 dims array
            isOperated<sbyte>(counter<sbyte>(1, 1, counter<long>(0, 1, 20, 1) % 3 + 1) >=  2, counter<sbyte>(1, 1, counter<long>(0, 1, 20, 1) % 3 + 1), 2, (a, b) => (a >=  b));

        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void ComparisonGreaterEqual_NotDefinedLogicalFail() {
            var a = counter<int>(1, 1, 6, 4, 2) > 21 >= false;
        }
        [TestMethod]
        public void ComparisonGreaterThanTo_AllTypes() {

            isOperated<double>(counter(1.0, 1.0, 6, 4, 2) > 2, counter(1.0, 1.0, 6, 4, 2), 2.0, (a, b) => a > b);
            isOperated<float>(counter<float>(1, 1, 6, 4, 2) > 2, counter<float>(1, 1, 6, 4, 2), 2, (a, b) => a > b);
            isOperated<complex>(tocomplex(counter<double>(1, 1, 6, 4, 2)) > new complex(2, 0), tocomplex(counter<double>(1, 1, 6, 4, 2)), new complex(2, 0), (a, b) => a > b);
            isOperated<fcomplex>(tofcomplex(counter<float>(1, 1, 6, 4, 2)) > new fcomplex(2, 0), tofcomplex(counter<float>(1, 1, 6, 4, 2)), new fcomplex(2, 0), (a, b) => a > b);
            isOperated<ulong>(counter<ulong>(1, 1, 6, 4, 2) > 2, counter<ulong>(1, 1, 6, 4, 2), 2, (a, b) => a > b);
            isOperated<long>(counter<long>(1, 1, 6, 4, 2) > 2, counter<long>(1, 1, 6, 4, 2), 2, (a, b) => a > b);
            isOperated<uint>(counter<uint>(1, 1, 6, 4, 2) > 2, counter<uint>(1, 1, 6, 4, 2), 2, (a, b) => a > b);
            isOperated<int>(counter<int>(1, 1, 6, 4, 2) > 2, counter<int>(1, 1, 6, 4, 2), 2, (a, b) => a > b);
            isOperated<ushort>(counter<ushort>(1, 1, 6, 4, 2) > 2, counter<ushort>(1, 1, 6, 4, 2), 2, (a, b) => (a > b));
            isOperated<short>(counter<short>(1, 1, 6, 4, 2) > 2, counter<short>(1, 1, 6, 4, 2), 2, (a, b) => (a > b));
            isOperated<byte>(counter<byte>(1, 1, 6, 4, 2) > 2, counter<byte>(1, 1, 6, 4, 2), 2, (a, b) => (a > b));
            isOperated<sbyte>(counter<sbyte>(1, 1, 6, 4, 2) > 2, counter<sbyte>(1, 1, 6, 4, 2), 2, (a, b) => (a > b));

        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void ComparisonGreaterThan_NotDefinedLogicalFail() {
            var a = counter<int>(1, 1, 6, 4, 2) > 21 > false;
 
        }

        #region & | ^ tests (boolean / binary)

        [TestMethod]
        public void BinaryAnd_AllTypes() {

            isOperated<ulong>(counter<ulong>(1, 1, 6, 4, 2) & 2, counter<ulong>(1, 1, 6, 4, 2), 2, (a, b) => a & b);
            isOperated<long>(counter<long>(1, 1, 6, 4, 2) & 2, counter<long>(1, 1, 6, 4, 2), 2, (a, b) => a & b);
            isOperated<uint>(counter<uint>(1, 1, 6, 4, 2) & 2, counter<uint>(1, 1, 6, 4, 2), 2, (a, b) => a & b);
            isOperated<int>(counter<int>(1, 1, 6, 4, 2) & 2, counter<int>(1, 1, 6, 4, 2), 2, (a, b) => a & b);
            isOperated<ushort>(counter<ushort>(1, 1, 6, 4, 2) & 2, counter<ushort>(1, 1, 6, 4, 2), 2, (a, b) => (ushort)(a & b));
            isOperated<short>(counter<short>(1, 1, 6, 4, 2) & 2, counter<short>(1, 1, 6, 4, 2), 2, (a, b) => (short)(a & b));
            isOperated<byte>(counter<byte>(1, 1, 6, 4, 2) & 2, counter<byte>(1, 1, 6, 4, 2), 2, (a, b) => (byte)(a & b));
            isOperated<sbyte>(counter<sbyte>(1, 1, 6, 4, 2) & 2, counter<sbyte>(1, 1, 6, 4, 2), 2, (a, b) => (sbyte)(a & b));

            isOperated(counter<int>(1, 1, 6, 4, 2) > 21 & true, counter<int>(1, 1, 6, 4, 2) > 21, true, (a, b) => a & b);

        }
        [TestMethod]
        public void BinaryXOr_AllTypes() {

            isOperated<ulong>(counter<ulong>(1, 1, 6, 4, 2) ^ 2, counter<ulong>(1, 1, 6, 4, 2), 2, (a, b) => a ^ b);
            isOperated<long>(counter<long>(1, 1, 6, 4, 2) ^ 2, counter<long>(1, 1, 6, 4, 2), 2, (a, b) => a ^ b);
            isOperated<uint>(counter<uint>(1, 1, 6, 4, 2) ^ 2, counter<uint>(1, 1, 6, 4, 2), 2, (a, b) => a ^ b);
            isOperated<int>(counter<int>(1, 1, 6, 4, 2) ^ 2, counter<int>(1, 1, 6, 4, 2), 2, (a, b) => a ^ b);
            isOperated<ushort>(counter<ushort>(1, 1, 6, 4, 2) ^ 2, counter<ushort>(1, 1, 6, 4, 2), 2, (a, b) => (ushort)(a ^ b));
            isOperated<short>(counter<short>(1, 1, 6, 4, 2) ^ 2, counter<short>(1, 1, 6, 4, 2), 2, (a, b) => (short)(a ^ b));
            isOperated<byte>(counter<byte>(1, 1, 6, 4, 2) ^ 2, counter<byte>(1, 1, 6, 4, 2), 2, (a, b) => (byte)(a ^ b));
            isOperated<sbyte>(counter<sbyte>(1, 1, 6, 4, 2) ^ 2, counter<sbyte>(1, 1, 6, 4, 2), 2, (a, b) => (sbyte)(a ^ b));

            isOperated(counter<int>(1, 1, 6, 4, 2) > 21 ^ true, counter<int>(1, 1, 6, 4, 2) > 21, true, (a, b) => a ^ b);
            isOperated(counter<sbyte>(1, 1, counter<long>(0, 1, 20, 1) % 3 + 1) > 21 ^ true, counter<sbyte>(1, 1, counter<long>(0, 1, 20, 1) % 3 + 1) > 21, true, (a, b) => a ^ b);

        }
        [TestMethod]
        public void BinaryOr_AllTypes() {

            isOperated<ulong>(counter<ulong>(1, 1, 6, 4, 2) | 2, counter<ulong>(1, 1, 6, 4, 2), 2, (a, b) => a | b);
            isOperated<long>(counter<long>(1, 1, 6, 4, 2) | 2, counter<long>(1, 1, 6, 4, 2), 2, (a, b) => a | b);
            isOperated<uint>(counter<uint>(1, 1, 6, 4, 2) | 2, counter<uint>(1, 1, 6, 4, 2), 2, (a, b) => a | b);
            isOperated<int>(counter<int>(1, 1, 6, 4, 2) | 2, counter<int>(1, 1, 6, 4, 2), 2, (a, b) => a | b);
            isOperated<ushort>(counter<ushort>(1, 1, 6, 4, 2) | 2, counter<ushort>(1, 1, 6, 4, 2), 2, (a, b) => (ushort)(a | b));
            isOperated<short>(counter<short>(1, 1, 6, 4, 2) | 2, counter<short>(1, 1, 6, 4, 2), 2, (a, b) => (short)(a | b));
            isOperated<byte>(counter<byte>(1, 1, 6, 4, 2) | 2, counter<byte>(1, 1, 6, 4, 2), 2, (a, b) => (byte)(a | b));
            isOperated<sbyte>(counter<sbyte>(1, 1, 6, 4, 2) | 2, counter<sbyte>(1, 1, 6, 4, 2), 2, (a, b) => (sbyte)(a | b));

            isOperated(counter<int>(1, 1, 6, 4, 2) > 21 | true, counter<int>(1, 1, 6, 4, 2) > 21, true, (a, b) => a | b);

        }
        #endregion

        #region hopefully not defined: floating point and bit / logical operators
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void BinaryAnd_NotDefinedDoubleFail() {
            var a = counter<double>(1, 1, 6, 4, 2) & 21;
 
        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void BinaryAnd_NotDefinedSingleFail() {
            var a = counter<float>(1, 1, 6, 4, 2) & 21;
        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void BinaryAnd_NotDefinedComplexFail() {
            var a = tocomplex(counter<double>(1, 1, 6, 4, 2)) & new complex(21, 0);
        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void BinaryAnd_NotDefinedfComplexFail() {
            var a = tofcomplex(counter<double>(1, 1, 6, 4, 2)) & new fcomplex(21, 0);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void BinaryOr_NotDefinedDoubleFail() {
            var a = counter<double>(1, 1, 6, 4, 2) | 21;
        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void BinaryOr_NotDefinedSingleFail() {
            var a = counter<float>(1, 1, 6, 4, 2) | 21;
        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void BinaryOr_NotDefinedComplexFail() {
            var a = tocomplex(counter<double>(1, 1, 6, 4, 2)) | new complex(21,0);
        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void BinaryOr_NotDefinedfComplexFail() {
            var a = tofcomplex(counter<double>(1, 1, 6, 4, 2)) | new fcomplex(21,0);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void BinaryXOr_NotDefinedDoubleFail() {
            var a = counter<double>(1, 1, 6, 4, 2) ^ 21;
        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void BinaryXOr_NotDefinedSingleFail() {
            var a = counter<float>(1, 1, 6, 4, 2) ^ 21;
        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void BinaryXOr_NotDefinedComplexFail() {
            var a = tocomplex(counter<double>(1, 1, 6, 4, 2)) ^ new complex(21,0);
        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void BinaryXOr_NotDefinedfComplexFail() {
            var a = tofcomplex(counter<double>(1, 1, 6, 4, 2)) ^ new fcomplex(21,0);
        }
        #endregion

        private void isOperated<T>(InArray<T> AR, InArray<T> AS, Func<T, T> p) {
            using (Scope.Enter(AR, AS)) {
                var itS = AS.GetEnumerator();
                var isR = AR.GetEnumerator();
                while (itS.MoveNext() && isR.MoveNext()) {
                    Assert.IsTrue(Equals(isR.Current, p(itS.Current)));
                }
            }
        }

        private void isOperated<T>(InArray<T> AR1, InArray<T> AS1, InArray<T> AS2, Func<T, T, T> p) {
            using (Scope.Enter(AR1, AS2, AS1)) {
                var itS1 = AS1.GetEnumerator();
                var itS2 = AS2.GetEnumerator();
                var itR1 = AR1.GetEnumerator();
                while (itS1.MoveNext() && itS2.MoveNext() && itR1.MoveNext()) {
                    Assert.IsTrue(Equals(itR1.Current, p(itS1.Current, itS2.Current)));
                }
            }
        }
        private void isOperated<T>(Logical AR1, InArray<T> AS1, InArray<T> AS2, Func<T, T, bool> p) {
            using (Scope.Enter(AR1, AS2, AS1)) {
                var itS1 = AS1.GetEnumerator();
                var itS2 = AS2.GetEnumerator();
                var itR1 = AR1.GetEnumerator();
                while (itS1.MoveNext() && itS2.MoveNext() && itR1.MoveNext()) {
                    Assert.IsTrue(Equals(itR1.Current, p(itS1.Current, itS2.Current)));
                }
            }
        }
        private void isOperated(InLogical AR1, InLogical AS1, InLogical AS2, Func<bool,bool,bool> p) {
            using (Scope.Enter(AR1, AS2, AS1)) {
                var itS1 = AS1.GetEnumerator();
                var itS2 = AS2.GetEnumerator();
                var itR1 = AR1.GetEnumerator();
                while (itS1.MoveNext() && itS2.MoveNext() && itR1.MoveNext()) {
                    Assert.IsTrue(itR1.Current == p(itS1.Current, itS2.Current));
                }
            }
        }

        #region broadcasting & ArrayStyles tests

        [TestMethod]
        public void Binary_Subtract_BC_ML_Empties() {

            Array<float> A = empty<float>(0, 1, 2, 3);
            Array<float> B = ones<float>(1,1,1);

            Array<float> ML = A - B;
            Assert.IsTrue(ML.IsEmpty);
            Assert.IsTrue(ML.S[0] == 0);
            Assert.IsTrue(ML.S[1] == 1);
            Assert.IsTrue(ML.S[2] == 2);
            Assert.IsTrue(ML.S[3] == 3);

            B = ones<float>(1, 4, 1); // broadcasting starts at first dimension

            ML = A - B;
            Assert.IsTrue(ML.IsEmpty);
            Assert.IsTrue(ML.S[0] == 0);
            Assert.IsTrue(ML.S[1] == 4); // !
            Assert.IsTrue(ML.S[2] == 2);
            Assert.IsTrue(ML.S[3] == 3);
        }
        [TestMethod]
        public void Binary_Subtract_BC_NP_Empties() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<float> A = empty<float>(0, 1, 2, 3);
                Array<float> B = ones<float>(1,1,1);

                Array<float> NP = A - B;
                Assert.IsTrue(NP.IsEmpty);
                Assert.IsTrue(NP.S[0] == 0);
                Assert.IsTrue(NP.S[1] == 1);
                Assert.IsTrue(NP.S[2] == 2);
                Assert.IsTrue(NP.S[3] == 3);


                B = ones<float>(4, 1, 1); // broadcasting starts at last dimension

                NP = A - B;
                Assert.IsTrue(NP.IsEmpty);
                Assert.IsTrue(NP.S[0] == 0);
                Assert.IsTrue(NP.S[1] == 4); // !
                Assert.IsTrue(NP.S[2] == 2);
                Assert.IsTrue(NP.S[3] == 3);
            }
        }

        #endregion
    }
}
