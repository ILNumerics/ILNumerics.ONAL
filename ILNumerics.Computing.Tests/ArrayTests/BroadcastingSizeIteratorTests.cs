using System;
using ILNumerics.Core.StorageLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.ILMath; 

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public unsafe class BroadcastingSizeIteratorTests {
        [TestMethod]
        public void BCSizeIteratorSimple() {

            Array<double> A = counter(0.0, 1.0, 5, 4, 3);
            long* buffer = stackalloc long[3 * 3];
            long* bcSize = stackalloc long[3];
            bcSize[0] = 5;
            bcSize[1] = 4;
            bcSize[2] = 3;

            var it = new Iterators.BroadcastingSizeRowMajorIterator(A.S, bcSize, 3, &buffer);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
            it.Reset(); 

            foreach(var a in A.Iterator(i => (long)i, StorageOrders.RowMajor)) {
            Assert.IsTrue(it.MoveNext());
                Assert.IsTrue(a == it.Current);
            }

        }
        [TestMethod]
        public void BCSizeIterator_scalarNoBCTest() {
            Array<double> A = 99;

            long* buffer = stackalloc long[1 * 3];
            long* bcSize = stackalloc long[1];
            bcSize[0] = 1;

            var it = new Iterators.BroadcastingSizeRowMajorIterator(A.S, bcSize, 1, &buffer);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
            Assert.IsFalse(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
            it.Reset();
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
        }
        [TestMethod]
        public void BCSizeIterator_MLscalarBCTest() {
            Array<double> A = 99;
            Array<double> I = counter(0.0, 1.0, 5, 4, 3);
            long* buffer = stackalloc long[3 * 3];
            long* bcSize = stackalloc long[3];
            bcSize[0] = 5;
            bcSize[1] = 4;
            bcSize[2] = 3;

            var it = new Iterators.BroadcastingSizeRowMajorIterator(A.S, bcSize, 3, &buffer);

            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
            it.Reset();

            foreach (var a in I.Iterator(i => (long)i, StorageOrders.RowMajor)) {
                Assert.IsTrue(it.MoveNext());
                Assert.IsTrue(it.Current == 0);
            }
            Assert.IsFalse(it.MoveNext());
            Assert.IsTrue(it.Current == 0);

        }
        [TestMethod]
        public void BCSizeIterator_MLRowBCTest() {

            Array<double> I = counter(0.0, 1.0, 1, 1, 5);
            long* buffer = stackalloc long[3 * 3];
            long* bcSize = stackalloc long[3];
            bcSize[0] = 3;
            bcSize[1] = 4;
            bcSize[2] = 5;

            var it = new Iterators.BroadcastingSizeRowMajorIterator(I.S, bcSize, 3, &buffer);

            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
            it.Reset();

            for (int i1 = 0; i1 < 3; i1++) {
                for (int i2 = 0; i2 < 4; i2++) {
                    for (int i3 = 0; i3 < 5; i3++) {
                        Assert.IsTrue(it.MoveNext());
                        Assert.IsTrue(it.Current == i3);
                    }
                }
            }
            Assert.IsFalse(it.MoveNext());
            Assert.IsTrue(it.Current == 4);

        }
        [TestMethod]
        public void BCSizeIterator_MLColBC3defTest() {

            Array<double> I = counter(0.0, 1.0, 1, 4, 1);
            long* buffer = stackalloc long[3 * 3];
            long* bcSize = stackalloc long[3];
            bcSize[0] = 5;
            bcSize[1] = 4;
            bcSize[2] = 3;

            var it = new Iterators.BroadcastingSizeRowMajorIterator(I.S, bcSize, 3, &buffer);

            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
            it.Reset();

            for (int i1 = 0; i1 < 5; i1++) {
                for (int i2 = 0; i2 < 4; i2++) {
                    for (int i3 = 0; i3 < 3; i3++) {
                        Assert.IsTrue(it.MoveNext());
                        Assert.IsTrue(it.Current == i2);
                    }
                }
            }
            Assert.IsFalse(it.MoveNext());
            Assert.IsTrue(it.Current == 3);

        }
        [TestMethod]
        public void BCSizeIterator_MLMatBC2defTest() {

            Array<double> I = counter(0.0, 1.0, 5, 4);
            long* buffer = stackalloc long[3 * 3];
            long* bcSize = stackalloc long[3];
            bcSize[0] = 3;
            bcSize[1] = 5;
            bcSize[2] = 4;

            var it = new Iterators.BroadcastingSizeRowMajorIterator(I.S, bcSize, 3, &buffer);

            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
            it.Reset();

            for (int i1 = 0; i1 < bcSize[0]; i1++) {
                for (int i2 = 0; i2 < bcSize[1]; i2++) {
                    for (int i3 = 0; i3 < bcSize[2]; i3++) {
                        Assert.IsTrue(it.MoveNext());
                        Assert.IsTrue(it.Current == i2 * I.S.GetStride(0) + i3 * I.S.GetStride(1));
                    }
                }
            }
            Assert.IsFalse(it.MoveNext());
            Assert.IsTrue(it.Current == 19);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BCSizeIterator_MLNonBCableFail() {

            Array<double> I = counter(0.0, 1.0, 1, 5, 4);
            long* buffer = stackalloc long[4 * 3];
            long* bcSize = stackalloc long[4];
            bcSize[0] = 5;
            bcSize[1] = 6;
            bcSize[2] = 4;
            bcSize[3] = 4;

            var it = new Iterators.BroadcastingSizeRowMajorIterator(I.S, bcSize, 4, &buffer);

        }

        [TestMethod]
        public void BCSizeIterator_MLBaseOffsetTest() {
            Array<double> I = counter(0.0, 1.0, 2, 3, 4);
            long* buffer = stackalloc long[4 * 3];
            long* bcSize = stackalloc long[4];
            bcSize[0] = 4;
            bcSize[1] = 5;
            bcSize[2] = 3;
            bcSize[3] = 4;

            var it = new Iterators.BroadcastingSizeRowMajorIterator(I[1, Globals.ellipsis].S, bcSize, 4, &buffer);

            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);
            it.Reset();

            for (int i1 = 0; i1 < bcSize[0]; i1++) {
                for (int i2 = 0; i2 < bcSize[1]; i2++) {
                    for (int i3 = 0; i3 < bcSize[2]; i3++) {
                        for (int i4 = 0; i4 < bcSize[3]; i4++) {
                            Assert.IsTrue(it.MoveNext());
                            Assert.IsTrue(it.Current == i3 * I.S.GetStride(1) + i4 * I.S.GetStride(2) + 1);
                        }
                    }
                }
            }
            Assert.IsFalse(it.MoveNext());
            Assert.IsTrue(it.Current == 23);


        }
        [TestMethod]
        public void BCSizeIterator_MLMat3defBC4Test() {

            Array<double> I = counter(0.0, 1.0, 1, 3, 4);
            long* buffer = stackalloc long[4 * 3];
            long* bcSize = stackalloc long[4];
            bcSize[0] = 5;
            bcSize[1] = 5;
            bcSize[2] = 3;
            bcSize[3] = 4;

            var it = new Iterators.BroadcastingSizeRowMajorIterator(I.S, bcSize, 4, &buffer);

            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
            it.Reset();

            for (int i1 = 0; i1 < bcSize[0]; i1++) {
                for (int i2 = 0; i2 < bcSize[1]; i2++) {
                    for (int i3 = 0; i3 < bcSize[2]; i3++) {
                        for (int i4 = 0; i4 < bcSize[3]; i4++) {
                            Assert.IsTrue(it.MoveNext());
                            Assert.IsTrue(it.Current == i3 + i4 * 3);
                        }
                    }
                }
            }
            Assert.IsFalse(it.MoveNext());
            Assert.IsTrue(it.Current == 11);

        }
        [TestMethod]
        public void BCSizeIterator_npScalarBCTest() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> A = 99;
                Array<double> I = counter(0.0, 1.0, 5, 4, 3); // still column major but values don't matter here anyways
                long* buffer = stackalloc long[3 * 3];
                long* bcSize = stackalloc long[3];
                bcSize[0] = 5;
                bcSize[1] = 4;
                bcSize[2] = 3;

                var it = new Iterators.BroadcastingSizeRowMajorIterator(A.S, bcSize, 3, &buffer);

                Assert.IsTrue(it.MoveNext());
                Assert.IsTrue(it.Current == 0);
                it.Reset();

                foreach (var a in I.Iterator(i => (long)i, StorageOrders.RowMajor)) {
                    Assert.IsTrue(it.MoveNext());
                    Assert.IsTrue(it.Current == 0);
                }
                Assert.IsFalse(it.MoveNext());
                Assert.IsTrue(it.Current == 0);
            }
        }
    }
}
