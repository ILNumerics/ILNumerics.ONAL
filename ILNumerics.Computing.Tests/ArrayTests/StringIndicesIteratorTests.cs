using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics;
using ILNumerics.Core.StorageLayer;

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class StringIndicesIteratorTests {
        [TestMethod]
        public void StringIndicesIterator_simple1() {
            var s = "1:end";

            var it = s.AsIndices(3);
            var sit = (StringIndicesIterator)it; 
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 2);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 3);

            Assert.IsFalse(it.MoveNext());
            it.Reset(); 
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1); 

        }
        [TestMethod]
        public void StringIndicesIterator_stepped1() {
            var s = "1:2:end";

            var it = s.AsIndices(6);
            var sit = (StringIndicesIterator)it;
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 3);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 5);

            Assert.IsFalse(it.MoveNext());
            it.Reset();
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);

        }
        [TestMethod]
        public void StringIndicesIterator_single1() {
            var s = "3";

            var it = s.AsIndices(6);
            var sit = (StringIndicesIterator)it;
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 3);

            Assert.IsFalse(it.MoveNext());
            it.Reset();
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 3);

        }
        [TestMethod]
        public void StringIndicesIterator_single1end() {
            var s = "end";

            var it = s.AsIndices(6);
            var sit = (StringIndicesIterator)it;
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 6);

            Assert.IsFalse(it.MoveNext());
            it.Reset();
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 6);

        }
        [TestMethod]
        public void StringIndicesIterator_single1endNeg1() {
            var s = "-1";

            var it = s.AsIndices(6);
            var sit = (StringIndicesIterator)it;
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 6);

            Assert.IsFalse(it.MoveNext());
            it.Reset();
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 6);

        }
        [TestMethod]
        public void StringIndicesIterator_single1endNeg2() {
            var s = "-2";

            var it = s.AsIndices(6);
            var sit = (StringIndicesIterator)it;
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 5);

            Assert.IsFalse(it.MoveNext());
            it.Reset();
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 5);

        }
        [TestMethod]
        public void StringIndicesIterator_simpleEndNeg2() {
            var s = ":-2";

            var it = s.AsIndices(3);
            var sit = (StringIndicesIterator)it;
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 2);

            Assert.IsFalse(it.MoveNext());
            it.Reset();
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);

        }
        [TestMethod]
        public void StringIndicesIterator_simpleStartEndNeg() {
            var s = "-3:-1";

            var it = s.AsIndices(3);
            var sit = (StringIndicesIterator)it;
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 2);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 3);

            Assert.IsFalse(it.MoveNext());
            it.Reset();
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 2);

        }
        [TestMethod]
        public void StringIndicesIterator_singleColonIsFull() {
            var s = ":";

            var it = s.AsIndices(2);
            var sit = (StringIndicesIterator)it;
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 2);

            Assert.IsFalse(it.MoveNext());
            it.Reset();
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StringIndicesIterator_steppedFai1l() {
            var s = "1:end:10";

            var it = s.AsIndices(6);
            it.MoveNext(); 

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StringIndicesIterator_steppedFail2() {
            var s = "1:end:end";

            var it = s.AsIndices(6);
            it.MoveNext();

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StringIndicesIterator_toomaynColonsFail() {
            var s = "1:end:2:";

            var it = s.AsIndices(6);
            it.MoveNext();

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StringIndicesIterator_invalidStartFail() {
            var s = "a1:end:2";

            var it = s.AsIndices(6);
            it.MoveNext();

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StringIndicesIterator_invalidEndFail() {
            var s = "1:1:d2";

            var it = s.AsIndices(6);
            it.MoveNext();

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StringIndicesIterator_invalidStepFail() {
            var s = "1:as2:2";

            var it = s.AsIndices(6);
            it.MoveNext();

        }
        [TestMethod]
        public void StringIndicesIterator_SteppedEmptyEndIsLast() {
            var s = "1:1:";

            var it = s.AsIndices(3);

            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 2);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 3);

            Assert.IsFalse(it.MoveNext());
            it.Reset();
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);

        }
        [TestMethod]
        public void StringIndicesIterator_SimpleEmptyEndIsLast() {
            var s = "1:";

            var it = s.AsIndices(3);

            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 2);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 3);

            Assert.IsFalse(it.MoveNext());
            it.Reset();
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);

        }
        [TestMethod]
        public void StringIndicesIterator_SimpleEmptyStartIsZero() {
            var s = ":3";

            var it = s.AsIndices(3);

            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 2);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 3);

            Assert.IsFalse(it.MoveNext());
            it.Reset();
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);

        }
        [TestMethod]
        public void StringIndicesIterator_SteppedEmptyStartIsZero() {
            var s = ":1:3";

            var it = s.AsIndices(3);

            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 2);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 3);

            Assert.IsFalse(it.MoveNext());
            it.Reset();
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);

        }
        [TestMethod]
        public void StringIndicesIterator_EmptyString() {
            var s = "";

            var it = s.AsIndices(3);

            Assert.IsFalse(it.MoveNext());
            it.Reset();
            Assert.IsFalse(it.MoveNext());
            // it.Current result is undefined
        }
        [TestMethod]
        public void StringIndicesIterator_SteppedEmptyStepIsOne() {
            var s = "0::3";

            var it = s.AsIndices(3);

            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 2);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 3);

            Assert.IsFalse(it.MoveNext());
            it.Reset();
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);

        }
        [TestMethod]
        public void StringIndicesIterator_SteppedSimpleRangeGetMinimum() {
            var s = "1::3";

            var it = s.AsIndices(3);

            Assert.IsTrue(it.GetMinimum() == 1);
            Assert.IsTrue(it.GetMaximum() == 3);
            Assert.IsTrue(it.GetStepSize() == 1);
            
        }
        [TestMethod]
        public void StringIndicesIterator_MultiRangeGetStep() {
            var s = "2,1::3";

            var it = s.AsIndices(3);

            //Assert.IsTrue(it.GetMinimum() == 1);
            //Assert.IsTrue(it.GetMaximum() == 3);
            Assert.IsFalse(it.GetStepSize().HasValue);
            
        }
        #region OOR tests
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void StringIndicesIterator_OORFailSingle() {
            var s = "5";

            var it = s.AsIndices(4);
            it.MoveNext();
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void StringIndicesIterator_OORFailSimpleStart() {
            var s = "5:end";

            var it = s.AsIndices(4);
            it.MoveNext();
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void StringIndicesIterator_OORFailSimpleEnd() {
            var s = "2:5";

            var it = s.AsIndices(4);
            it.MoveNext();
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void StringIndicesIterator_OORFailSteppedEnd() {
            var s = "2:1:5";

            var it = s.AsIndices(4);
            it.MoveNext();
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void StringIndicesIterator_OORFailSteppedStart() {
            var s = "5:1:4";

            var it = s.AsIndices(4);
            it.MoveNext();
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void StringIndicesIterator_OORFailSimpleStartNeg() {
            var s = "-6:1:4";

            var it = s.AsIndices(4);
            it.MoveNext();
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void StringIndicesIterator_OORFailSteppedEndNeg() {
            var s = "2:1:-6";

            var it = s.AsIndices(4);
            it.MoveNext();
        }

        #endregion

        #region multi range tests
        [TestMethod]
        public void StringIndicesIterator_MultiSteppedEmptyStepIsOne() {
            var s = "3,0::3";

            var it = s.AsIndices(3);

            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 3);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 2);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 3);

            Assert.IsFalse(it.MoveNext());
            it.Reset();
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 3);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);

        }
        [TestMethod]
        public void StringIndicesIterator_Multi2SteppedEmptyStepIsOne() {
            var s = "3,,0::3";

            var it = s.AsIndices(3);

            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 3);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 2);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 3);

            Assert.IsFalse(it.MoveNext());
            it.Reset();
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 3);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);

        }
        [TestMethod]
        public void StringIndicesIterator_Multi3SteppedEmptyStepIsOne() {
            var s = "3,,0::3,";

            var it = s.AsIndices(3);

            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 3);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 2);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 3);

            Assert.IsFalse(it.MoveNext());
            it.Reset();
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 3);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);

        }
        [TestMethod]
        public void StringIndicesIterator_Multi4SteppedEmptyStepIsOne() {
            var s = "-2,3,,0::3,";

            var it = s.AsIndices(3);

            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 2);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 3);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 2);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 3);

            Assert.IsFalse(it.MoveNext());
            it.Reset();
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 2);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 3);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);

        }
        [TestMethod]
        public void StringIndicesIterator_IgnoreWhitespace() {
            var s = " -2, 3, ,0 : : 3 , ";

            var it = s.AsIndices(3);

            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 2);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 3);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 2);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 3);

            Assert.IsFalse(it.MoveNext());
            it.Reset();
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 2);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 3);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 0);
            Assert.IsTrue(it.MoveNext());
            Assert.IsTrue(it.Current == 1);

        }

        #endregion

        #region indexiterator test
        [TestMethod]
        public void StringIndicesIterator_GetLengthSimple001() {
            var s = "-2,3,,0::3,";

            var it = s.AsIndices(3);
            Assert.IsTrue(it.GetLength() == 6);
            Assert.IsTrue("".AsIndices(3).GetLength() == 0);
            Assert.IsTrue("".AsIndices(0).GetLength() == 0);
            Assert.IsTrue("0".AsIndices(0).GetLength() == 1);
            Assert.IsTrue(",".AsIndices(0).GetLength() == 0);
            Assert.IsTrue("0,".AsIndices(0).GetLength() == 1);
            Assert.IsTrue(",1".AsIndices(10).GetLength() == 1);
            Assert.IsTrue("2,1".AsIndices(10).GetLength() == 2);
            Assert.IsTrue("2,1:3:3".AsIndices(10).GetLength() == 2);
            Assert.IsTrue("2,1:3:5".AsIndices(10).GetLength() == 3);
        }
        [TestMethod]
        public void StringIndicesIterator_GetMaximumSimple001() {

            Assert.IsTrue("-2,3,,0::3,".AsIndices(3).GetMaximum() == 3);
            Assert.IsTrue("-1:3,".AsIndices(13).GetMaximum() == 13);
            Assert.IsTrue("".AsIndices(3).GetMaximum() == null);
            Assert.IsTrue("".AsIndices(0).GetMaximum() == null);
            Assert.IsTrue("0".AsIndices(10).GetMaximum() == 0);
            Assert.IsTrue(",".AsIndices(0).GetMaximum() == null);
            Assert.IsTrue("0,".AsIndices(10).GetMaximum() == 0);
            Assert.IsTrue(",1".AsIndices(10).GetMaximum() == 1);
            Assert.IsTrue("2,1".AsIndices(10).GetMaximum() == 2);
            Assert.IsTrue("2,1:3:3".AsIndices(10).GetMaximum() == 3);
            Assert.IsTrue("2,1:3:5,,".AsIndices(10).GetMaximum() == 5);
            Assert.IsTrue("2,1:3:5,,end".AsIndices(10).GetMaximum() == 10);
        }
        #endregion

    }
}
