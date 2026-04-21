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
using ILNumerics.Core.Functions.Builtin;

namespace ILNumerics.Core.UnitTests {

    [TestClass]
    public class ConvertTests {

    // TODO: add tests for ...
    /*
        * convert(RetArray (BaseArray)) -> keeps RetArray alive
        * does not copy same types 
        * all type combinations work (fcomplex / complex?) 
        * scalar & np scalars
        * empty
        * NaNs <-> integer types
        * overflows ulong -> uint etc. Needs specification! #
        * reinterprete cast all integer uint <-> int types 
        */

        [TestMethod]
        public void ConvertComplex2UintTest() {
            Array<complex> A = new[,] {
                { new complex(-1, double.PositiveInfinity), new complex(double.NaN, 999) },
                { new complex(1, double.NegativeInfinity), new complex(double.NaN, 0) },
            };
            Array<uint> B = MathInternal.touint32(A);
            Assert.IsTrue(B.Storage.GetValue(0) == uint.MaxValue);
            Assert.IsTrue(B.Storage.GetValue(1) == 1);
            Assert.IsTrue(B.Storage.GetValue(2) == 0);
            Assert.IsTrue(B.Storage.GetValue(3) == 0);
            Assert.IsTrue(B.S.IsSameShape(A.S)); 
            Assert.IsTrue(B.S.BaseOffset == 0);

            //Assert.IsTrue(!object.ReferenceEquals(A.Storage.m_handles, B.Storage.m_handles)); 
            //Assert.IsTrue(!object.ReferenceEquals(A.Storage, B.Storage));
        }

        [TestMethod]
        public void ConvertSByteToAllTypesTests() {
            Array<sbyte> A = new sbyte[,] {
                { -1, 0, 1 },
                { sbyte.MinValue, sbyte.MaxValue, 99 }
            };

            TestArrayTo_ConvertResults<sbyte, byte>(A, MathInternal.touint8, i => (byte)i);
            TestArrayTo_ConvertResults<sbyte, short>(A, MathInternal.toint16, i => (short)i);
            TestArrayTo_ConvertResults<sbyte, ushort>(A, MathInternal.touint16, i => (ushort)i);
            TestArrayTo_ConvertResults<sbyte, int>(A, MathInternal.toint32, i => (int)i);
            TestArrayTo_ConvertResults<sbyte, uint>(A, MathInternal.touint32, i => (uint)i);
            TestArrayTo_ConvertResults<sbyte, long>(A, MathInternal.toint64, i => (long)i);
            TestArrayTo_ConvertResults<sbyte, ulong>(A, MathInternal.touint64, i => (ulong)i);
            TestArrayTo_ConvertResults<sbyte, double>(A, MathInternal.todouble, i => (double)i);
            TestArrayTo_ConvertResults<sbyte, float>(A, MathInternal.tosingle, i => (float)i);
            TestArrayTo_ConvertResults<sbyte, complex>(A, MathInternal.tocomplex, i => (complex)i);
            TestArrayTo_ConvertResults<sbyte, fcomplex>(A, MathInternal.tofcomplex, i => (fcomplex)i);
        }
        [TestMethod]
        public void ConvertByteToAllTypesTests() {
            Array<byte> A = new byte[,] {
                { 200, 0, 1 },
                { byte.MinValue, byte.MaxValue, 99 }
            };

            TestArrayTo_ConvertResults<byte, sbyte>(A, MathInternal.toint8, i => (sbyte)i);
            TestArrayTo_ConvertResults<byte, byte>(A, MathInternal.touint8, i => (byte)i);
            TestArrayTo_ConvertResults<byte, short>(A, MathInternal.toint16, i => (short)i);
            TestArrayTo_ConvertResults<byte, ushort>(A, MathInternal.touint16, i => (ushort)i);
            TestArrayTo_ConvertResults<byte, int>(A, MathInternal.toint32, i => (int)i);
            TestArrayTo_ConvertResults<byte, uint>(A, MathInternal.touint32, i => (uint)i);
            TestArrayTo_ConvertResults<byte, long>(A, MathInternal.toint64, i => (long)i);
            TestArrayTo_ConvertResults<byte, ulong>(A, MathInternal.touint64, i => (ulong)i);
            TestArrayTo_ConvertResults<byte, double>(A, MathInternal.todouble, i => (double)i);
            TestArrayTo_ConvertResults<byte, float>(A, MathInternal.tosingle, i => (float)i);
            TestArrayTo_ConvertResults<byte, complex>(A, MathInternal.tocomplex, i => (complex)i);
            TestArrayTo_ConvertResults<byte, fcomplex>(A, MathInternal.tofcomplex, i => (fcomplex)i);
        }
        [TestMethod]
        public void ConvertShortToAllTypesTests() {
            Array<short> A = new short[,] {
                { -200, 0, 1 },
                { short.MinValue, short.MaxValue, 99 }
            };

            TestArrayTo_ConvertResults<short, sbyte>(A, MathInternal.toint8, i => (sbyte)i);
            TestArrayTo_ConvertResults<short, byte>(A, MathInternal.touint8, i => (byte)i);
            TestArrayTo_ConvertResults<short, short>(A, MathInternal.toint16, i => (short)i);
            TestArrayTo_ConvertResults<short, ushort>(A, MathInternal.touint16, i => (ushort)i);
            TestArrayTo_ConvertResults<short, int>(A, MathInternal.toint32, i => (int)i);
            TestArrayTo_ConvertResults<short, uint>(A, MathInternal.touint32, i => (uint)i);
            TestArrayTo_ConvertResults<short, long>(A, MathInternal.toint64, i => (long)i);
            TestArrayTo_ConvertResults<short, ulong>(A, MathInternal.touint64, i => (ulong)i);
            TestArrayTo_ConvertResults<short, double>(A, MathInternal.todouble, i => (double)i);
            TestArrayTo_ConvertResults<short, float>(A, MathInternal.tosingle, i => (float)i);
            TestArrayTo_ConvertResults<short, complex>(A, MathInternal.tocomplex, i => (complex)i);
            TestArrayTo_ConvertResults<short, fcomplex>(A, MathInternal.tofcomplex, i => (fcomplex)i);
        }
        [TestMethod]
        public void ConvertUshortToAllTypesTests() {
            Array<ushort> A = new ushort[,] {
                { short.MaxValue + 1, 0, 1 },
                { ushort.MinValue, ushort.MaxValue, 99 }
            };

            TestArrayTo_ConvertResults<ushort, sbyte>(A, MathInternal.toint8, i => (sbyte)i);
            TestArrayTo_ConvertResults<ushort, byte>(A, MathInternal.touint8, i => (byte)i);
            TestArrayTo_ConvertResults<ushort, short>(A, MathInternal.toint16, i => (short)i);
            TestArrayTo_ConvertResults<ushort, ushort>(A, MathInternal.touint16, i => (ushort)i);
            TestArrayTo_ConvertResults<ushort, int>(A, MathInternal.toint32, i => (int)i);
            TestArrayTo_ConvertResults<ushort, uint>(A, MathInternal.touint32, i => (uint)i);
            TestArrayTo_ConvertResults<ushort, long>(A, MathInternal.toint64, i => (long)i);
            TestArrayTo_ConvertResults<ushort, ulong>(A, MathInternal.touint64, i => (ulong)i);
            TestArrayTo_ConvertResults<ushort, double>(A, MathInternal.todouble, i => (double)i);
            TestArrayTo_ConvertResults<ushort, float>(A, MathInternal.tosingle, i => (float)i);
            TestArrayTo_ConvertResults<ushort, complex>(A, MathInternal.tocomplex, i => (complex)i);
            TestArrayTo_ConvertResults<ushort, fcomplex>(A, MathInternal.tofcomplex, i => (fcomplex)i);
        }
        [TestMethod]
        public void ConvertIntToAllTypesTests() {
            Array<int> A = new int[,] {
                { -1, 0, 1 },
                { int.MinValue, int.MaxValue, 99 }
            };

            TestArrayTo_ConvertResults<int, sbyte>(A, MathInternal.toint8, i => (sbyte)i);
            TestArrayTo_ConvertResults<int, byte>(A, MathInternal.touint8, i => (byte)i);
            TestArrayTo_ConvertResults<int, short>(A, MathInternal.toint16, i => (short)i);
            TestArrayTo_ConvertResults<int, ushort>(A, MathInternal.touint16, i => (ushort)i);
            TestArrayTo_ConvertResults<int, int>(A, MathInternal.toint32, i => (int)i);
            TestArrayTo_ConvertResults<int, uint>(A, MathInternal.touint32, i => (uint)i);
            TestArrayTo_ConvertResults<int, long>(A, MathInternal.toint64, i => (long)i);
            TestArrayTo_ConvertResults<int, ulong>(A, MathInternal.touint64, i => (ulong)i);
            TestArrayTo_ConvertResults<int, double>(A, MathInternal.todouble, i => (double)i);
            TestArrayTo_ConvertResults<int, float>(A, MathInternal.tosingle, i => (float)i);
            TestArrayTo_ConvertResults<int, complex>(A, MathInternal.tocomplex, i => (complex)i);
            TestArrayTo_ConvertResults<int, fcomplex>(A, MathInternal.tofcomplex, i => (fcomplex)i);
        }
        [TestMethod]
        public void ConvertUIntToAllTypesTests() {
            Array<uint> A = new uint[,] {
                { 200, 0, 1 },
                { uint.MinValue, uint.MaxValue, 99 }
            };

            TestArrayTo_ConvertResults<uint, sbyte>(A, MathInternal.toint8, i => (sbyte)i);
            TestArrayTo_ConvertResults<uint, byte>(A, MathInternal.touint8, i => (byte)i);
            TestArrayTo_ConvertResults<uint, short>(A, MathInternal.toint16, i => (short)i);
            TestArrayTo_ConvertResults<uint, ushort>(A, MathInternal.touint16, i => (ushort)i);
            TestArrayTo_ConvertResults<uint, int>(A, MathInternal.toint32, i => (int)i);
            TestArrayTo_ConvertResults<uint, uint>(A, MathInternal.touint32, i => (uint)i);
            TestArrayTo_ConvertResults<uint, long>(A, MathInternal.toint64, i => (long)i);
            TestArrayTo_ConvertResults<uint, ulong>(A, MathInternal.touint64, i => (ulong)i);
            TestArrayTo_ConvertResults<uint, double>(A, MathInternal.todouble, i => (double)i);
            TestArrayTo_ConvertResults<uint, float>(A, MathInternal.tosingle, i => (float)i);
            TestArrayTo_ConvertResults<uint, complex>(A, MathInternal.tocomplex, i => (complex)i);
            TestArrayTo_ConvertResults<uint, fcomplex>(A, MathInternal.tofcomplex, i => (fcomplex)i);
        }
        [TestMethod]
        public void ConvertLongToAllTypesTests() {
            Array<long> A = new long[,] {
                { -1, 0, 1 },
                { long.MinValue, long.MaxValue, 99 }
            };

            TestArrayTo_ConvertResults<long, sbyte>(A, MathInternal.toint8, i => (sbyte)i);
            TestArrayTo_ConvertResults<long, byte>(A, MathInternal.touint8, i => (byte)i);
            TestArrayTo_ConvertResults<long, short>(A, MathInternal.toint16, i => (short)i);
            TestArrayTo_ConvertResults<long, ushort>(A, MathInternal.touint16, i => (ushort)i);
            TestArrayTo_ConvertResults<long, int>(A, MathInternal.toint32, i => (int)i);
            TestArrayTo_ConvertResults<long, uint>(A, MathInternal.touint32, i => (uint)i);
            TestArrayTo_ConvertResults<long, long>(A, MathInternal.toint64, i => (long)i);
            TestArrayTo_ConvertResults<long, ulong>(A, MathInternal.touint64, i => (ulong)i);
            TestArrayTo_ConvertResults<long, double>(A, MathInternal.todouble, i => (double)i);
            TestArrayTo_ConvertResults<long, float>(A, MathInternal.tosingle, i => (float)i);
            TestArrayTo_ConvertResults<long, complex>(A, MathInternal.tocomplex, i => (complex)i);
            TestArrayTo_ConvertResults<long, fcomplex>(A, MathInternal.tofcomplex, i => (fcomplex)i);
        }
        [TestMethod]
        public void ConvertULongToAllTypesTests() {
            Array<ulong> A = new ulong[,] {
                { 200, 0, 1 },
                { ulong.MinValue, ulong.MaxValue, 99 }
            };

            TestArrayTo_ConvertResults<ulong, sbyte>(A, MathInternal.toint8, i => (sbyte)i);
            TestArrayTo_ConvertResults<ulong, byte>(A, MathInternal.touint8, i => (byte)i);
            TestArrayTo_ConvertResults<ulong, short>(A, MathInternal.toint16, i => (short)i);
            TestArrayTo_ConvertResults<ulong, ushort>(A, MathInternal.touint16, i => (ushort)i);
            TestArrayTo_ConvertResults<ulong, int>(A, MathInternal.toint32, i => (int)i);
            TestArrayTo_ConvertResults<ulong, uint>(A, MathInternal.touint32, i => (uint)i);
            TestArrayTo_ConvertResults<ulong, long>(A, MathInternal.toint64, i => (long)i);
            TestArrayTo_ConvertResults<ulong, ulong>(A, MathInternal.touint64, i => (ulong)i);
            TestArrayTo_ConvertResults<ulong, double>(A, MathInternal.todouble, i => (double)i);
            TestArrayTo_ConvertResults<ulong, float>(A, MathInternal.tosingle, i => (float)i);
            TestArrayTo_ConvertResults<ulong, complex>(A, MathInternal.tocomplex, i => (complex)i);
            TestArrayTo_ConvertResults<ulong, fcomplex>(A, MathInternal.tofcomplex, i => (fcomplex)i);
        }
        [TestMethod]
        public void ConvertDoubleToAllTypesTests() {
            Array<double> A = new double[,] {
                { -1, 0, 1 },
                { double.MinValue, double.MaxValue, double.NaN }
            };

            TestArrayTo_ConvertResults<double, sbyte>(A, MathInternal.toint8, i => (sbyte)i);
            TestArrayTo_ConvertResults<double, byte>(A, MathInternal.touint8, i => (byte)i);
            TestArrayTo_ConvertResults<double, short>(A, MathInternal.toint16, i => (short)i);
            TestArrayTo_ConvertResults<double, ushort>(A, MathInternal.touint16, i => (ushort)i);
            TestArrayTo_ConvertResults<double, int>(A, MathInternal.toint32, i => (int)i);
            TestArrayTo_ConvertResults<double, uint>(A, MathInternal.touint32, i => (uint)i);
            TestArrayTo_ConvertResults<double, long>(A, MathInternal.toint64, i => (long)i);
            TestArrayTo_ConvertResults<double, ulong>(A, MathInternal.touint64, i => (ulong)i);
            TestArrayTo_ConvertResults<double, double>(A, MathInternal.todouble, i => (double)i);
            TestArrayTo_ConvertResults<double, float>(A, MathInternal.tosingle, i => (float)i);
            TestArrayTo_ConvertResults<double, complex>(A, MathInternal.tocomplex, i => (complex)i);
            TestArrayTo_ConvertResults<double, fcomplex>(A, MathInternal.tofcomplex, i => (fcomplex)i);
        }
        [TestMethod]
        public void ConvertSingleToAllTypesTests() {
            Array<float> A = new float[,] {
                { -1, 0, 1 },
                { float.MinValue, float.MaxValue, float.NaN }
            };

            TestArrayTo_ConvertResults<float, sbyte>(A, MathInternal.toint8, i => (sbyte)i);
            TestArrayTo_ConvertResults<float, byte>(A, MathInternal.touint8, i => (byte)i);
            TestArrayTo_ConvertResults<float, short>(A, MathInternal.toint16, i => (short)i);
            TestArrayTo_ConvertResults<float, ushort>(A, MathInternal.touint16, i => (ushort)i);
            TestArrayTo_ConvertResults<float, int>(A, MathInternal.toint32, i => (int)i);
            TestArrayTo_ConvertResults<float, uint>(A, MathInternal.touint32, i => (uint)i);
            TestArrayTo_ConvertResults<float, long>(A, MathInternal.toint64, i => (long)i);
            TestArrayTo_ConvertResults<float, ulong>(A, MathInternal.touint64, i => (ulong)i);
            TestArrayTo_ConvertResults<float, double>(A, MathInternal.todouble, i => (double)i);
            TestArrayTo_ConvertResults<float, float>(A, MathInternal.tosingle, i => (float)i);
            TestArrayTo_ConvertResults<float, complex>(A, MathInternal.tocomplex, i => (complex)i);
            TestArrayTo_ConvertResults<float, fcomplex>(A, MathInternal.tofcomplex, i => (fcomplex)i);
        }
        [TestMethod]
        public void ConvertComplexToAllTypesTests() {
            Array<complex> A = new complex[,] {
                { -1, 0, 1 },
                { double.MaxValue, double.MinValue, complex.NaN }
            };

            TestArrayTo_ConvertResults<complex, sbyte>(A, MathInternal.toint8, i => (sbyte)i);
            TestArrayTo_ConvertResults<complex, byte>(A, MathInternal.touint8, i => (byte)i);
            TestArrayTo_ConvertResults<complex, short>(A, MathInternal.toint16, i => (short)i);
            TestArrayTo_ConvertResults<complex, ushort>(A, MathInternal.touint16, i => (ushort)i);
            TestArrayTo_ConvertResults<complex, int>(A, MathInternal.toint32, i => (int)i);
            TestArrayTo_ConvertResults<complex, uint>(A, MathInternal.touint32, i => (uint)i);
            TestArrayTo_ConvertResults<complex, long>(A, MathInternal.toint64, i => (long)i);
            TestArrayTo_ConvertResults<complex, ulong>(A, MathInternal.touint64, i => (ulong)i);
            TestArrayTo_ConvertResults<complex, double>(A, MathInternal.todouble, i => (double)i);
            TestArrayTo_ConvertResults<complex, float>(A, MathInternal.tosingle, i => (float)i);
            TestArrayTo_ConvertResults<complex, complex>(A, MathInternal.tocomplex, i => (complex)i);
            TestArrayTo_ConvertResults<complex, fcomplex>(A, MathInternal.tofcomplex, i => (fcomplex)i);
        }
        [TestMethod]
        public void ConvertFcomplexToAllTypesTests() {
            Array<fcomplex> A = new fcomplex[,] {
                { -1, 0, 1 },
                { double.MaxValue, double.MinValue, fcomplex.NaN }
            };

            TestArrayTo_ConvertResults<fcomplex, sbyte>(A, MathInternal.toint8, i => (sbyte)i);
            TestArrayTo_ConvertResults<fcomplex, byte>(A, MathInternal.touint8, i => (byte)i);
            TestArrayTo_ConvertResults<fcomplex, short>(A, MathInternal.toint16, i => (short)i);
            TestArrayTo_ConvertResults<fcomplex, ushort>(A, MathInternal.touint16, i => (ushort)i);
            TestArrayTo_ConvertResults<fcomplex, int>(A, MathInternal.toint32, i => (int)i);
            TestArrayTo_ConvertResults<fcomplex, uint>(A, MathInternal.touint32, i => (uint)i);
            TestArrayTo_ConvertResults<fcomplex, long>(A, MathInternal.toint64, i => (long)i);
            TestArrayTo_ConvertResults<fcomplex, ulong>(A, MathInternal.touint64, i => (ulong)i);
            TestArrayTo_ConvertResults<fcomplex, double>(A, MathInternal.todouble, i => (double)i);
            TestArrayTo_ConvertResults<fcomplex, float>(A, MathInternal.tosingle, i => (float)i);
            TestArrayTo_ConvertResults<fcomplex, complex>(A, MathInternal.tocomplex, i => (complex)i);
            TestArrayTo_ConvertResults<fcomplex, fcomplex>(A, MathInternal.tofcomplex, i => (fcomplex)i);
        }

        public void TestArrayTo_ConvertResults<Tin, Tout>(BaseArray<Tin> A, Func<BaseArray<Tin>,Array<Tout>> func, Func<Tin,Tout> cast) {
            Array<Tout> B = func(A);
            Assert.IsTrue(B.S.IsSameSize(A.S));
            Assert.IsTrue(B.S.StorageOrder == A.S.StorageOrder);

            var AArr = A.ToArray<Tin>().GetArrayForRead();
            var RArr = new Tout[A.S.NumberOfElements];
            for (int i = 0; i < RArr.Length; i++) {
                RArr[i] = cast(AArr[i]); 
            }

            ArrayAssert.ValuesEqual(B.GetArrayForRead(), RArr); 
        }

        [TestMethod]
        public void ConvertUintShortTest() {
            Array<uint> A = new uint[] { 0, 1, 2, 3, uint.MaxValue };
            Array<short> B = MathInternal.toint16(A);
            Assert.IsTrue(B.Storage.GetValue(4) == -1);
            var C = MathInternal.touint32(B);
            Assert.IsTrue(!object.ReferenceEquals(A.Storage.m_handles, C.Storage.m_handles));

            Assert.IsTrue(C.Storage.GetValue(4) == uint.MaxValue);
            Assert.IsTrue(A.Equals(C));
            Assert.IsTrue(!object.ReferenceEquals(A.Storage.m_handles, B.Storage.m_handles));

            Array<ushort> D = MathInternal.touint16(A);
            Array<ushort> E = new ushort[] { 0, 1, 2, 3, ushort.MaxValue };
            Assert.IsTrue(D.Equals(E));
        }
        [TestMethod]
        public void ConvertCastUintIntTest() {
            Array<uint> A = new uint[] { 1, 2, 3, uint.MaxValue };
            Array<int> B = MathInternal.toint32(A);
            Assert.IsTrue(B.Storage.GetValue(3) == -1);
            var C = MathInternal.touint32(B);
            Assert.IsTrue(object.ReferenceEquals(A.Storage.m_handles, C.Storage.m_handles));

            Assert.IsTrue(C.Storage.GetValue(3) == uint.MaxValue);
            Assert.IsTrue(A.Equals(C));
            Assert.IsTrue(object.ReferenceEquals(A.Storage.m_handles, B.Storage.m_handles));

        }
        [TestMethod]
        public void ConvertCastUlongLongTest() {
            Array<ulong> A = new ulong[] { 1, 2, 3, ulong.MaxValue };
            Array<long> B = MathInternal.toint64(A);
            Assert.IsTrue(B.Storage.GetValue(3) == -1);
            var C = MathInternal.touint64(B);
            Assert.IsTrue(C.Storage.GetValue(3) == ulong.MaxValue);
            Assert.IsTrue(object.ReferenceEquals(A.Storage.m_handles, C.Storage.m_handles));

            Assert.IsTrue(A.Equals(C));
            Assert.IsTrue(object.ReferenceEquals(A.Storage.m_handles, B.Storage.m_handles));

        }
        [TestMethod]
        public void ConvertCastUshortShortTest() {
            Array<ushort> A = new ushort[] { 1, 2, 3, ushort.MaxValue };
            Array<short> B = MathInternal.toint16(A);
            Assert.IsTrue(B.Storage.GetValue(3) == -1);
            var C = MathInternal.touint16(B);
            Assert.IsTrue(C.Storage.GetValue(3) == ushort.MaxValue);
            Assert.IsTrue(object.ReferenceEquals(A.Storage.m_handles, C.Storage.m_handles));

            Assert.IsTrue(A.Equals(C));
            Assert.IsTrue(object.ReferenceEquals(A.Storage.m_handles, B.Storage.m_handles));

        }
        [TestMethod]
        public void ConvertCastByteSbyteTest() {
            Array<byte> A = new byte[] { 1, 2, 3, byte.MaxValue };
            Array<sbyte> B = MathInternal.toint8(A);
            Assert.IsTrue(B.Storage.GetValue(3) == -1);
            var C = MathInternal.touint8(B);
            Assert.IsTrue(C.Storage.GetValue(3) == byte.MaxValue);
            Assert.IsTrue(object.ReferenceEquals(A.Storage.m_handles, C.Storage.m_handles));

            Assert.IsTrue(A.Equals(C));
            Assert.IsTrue(object.ReferenceEquals(A.Storage.m_handles, B.Storage.m_handles));

        }

        [TestMethod]
        public void ConvertCastIntUintTest() {
            Array<int> A = new int[] { 1, 2, 3, -1 };
            Array<uint> B = MathInternal.touint32(A);
            Assert.IsTrue(B.Storage.GetValue(3) == uint.MaxValue);
            var C = MathInternal.toint32(B);
            Assert.IsTrue(object.ReferenceEquals(A.Storage.m_handles, C.Storage.m_handles));

            Assert.IsTrue(C.Storage.GetValue(3) == -1);
            Assert.IsTrue(A.Equals(C));
            Assert.IsTrue(object.ReferenceEquals(A.Storage.m_handles, B.Storage.m_handles));

        }
        [TestMethod]
        public void ConvertCastLongUlongTest() {
            Array<long> A = new long[] { 1, 2, 3, -1 };
            Array<ulong> B = MathInternal.touint64(A);
            Assert.IsTrue(B.Storage.GetValue(3) == ulong.MaxValue);
            var C = MathInternal.toint64(B);
            Assert.IsTrue(C.Storage.GetValue(3) == -1);
            Assert.IsTrue(object.ReferenceEquals(A.Storage.m_handles, C.Storage.m_handles));

            Assert.IsTrue(A.Equals(C));
            Assert.IsTrue(object.ReferenceEquals(A.Storage.m_handles, B.Storage.m_handles));

        }
        [TestMethod]
        public void ConvertCastShortUshortTest() {
            Array<short> A = new short[] { 1, 2, 3, -1 };
            Array<ushort> B = MathInternal.touint16(A);
            Assert.IsTrue(B.Storage.GetValue(3) == ushort.MaxValue);
            var C = MathInternal.toint16(B);
            Assert.IsTrue(C.Storage.GetValue(3) == -1);
            Assert.IsTrue(object.ReferenceEquals(A.Storage.m_handles, C.Storage.m_handles));

            Assert.IsTrue(A.Equals(C));
            Assert.IsTrue(object.ReferenceEquals(A.Storage.m_handles, B.Storage.m_handles));

        }
        [TestMethod]
        public void ConvertCastSByteByteTest() {
            Array<sbyte> A = new sbyte[] { 1, 2, 3, -1 };
            Array<byte> B = MathInternal.touint8(A);
            Assert.IsTrue(B.Storage.GetValue(3) == byte.MaxValue);
            var C = MathInternal.toint8(B);
            Assert.IsTrue(C.Storage.GetValue(3) == -1);
            Assert.IsTrue(object.ReferenceEquals(A.Storage.m_handles, C.Storage.m_handles));

            Assert.IsTrue(A.Equals(C));
            Assert.IsTrue(object.ReferenceEquals(A.Storage.m_handles, B.Storage.m_handles));

        }
    }
}
