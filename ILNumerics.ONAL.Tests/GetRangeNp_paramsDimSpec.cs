using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics;
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.Tests {
    [TestClass]
    public class GetRangeNp_paramsDimSpec : ILNumerics.Core.UnitTests.NumpyTestClass {

        [TestMethod]
        public void GetRange_np_paramDimSpecEllipsisNewaxis() {

            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 2, 1, 2, 2, 2));

            Assert.IsTrue(A[newaxis, ellipsis].Equals(A[newaxis, full, full, full, full, full, full, full, full]));
            Assert.IsTrue(A[ellipsis, newaxis].Equals(A[full, full, full, full, full, full, full, full, newaxis]));
            Assert.IsTrue(A[newaxis, newaxis].Equals(A[newaxis, newaxis, full, full, full, full, full, full, full, full]));
            Assert.IsTrue(A[ellipsis, ellipsis].Equals(A[full, full, full, full, full, full, full, full]));
            Assert.IsTrue(A[0, newaxis].Equals(A[0, newaxis, full, full, full, full, full, full, full]));
            Assert.IsTrue(A[0, full, full, full, full, full, full, full, newaxis].size_ == A.S.NumberOfElements / A.S[0]);
            Assert.IsTrue(A[newaxis, 0].Equals(A[newaxis, 0, full, full, full, full, full, full, full]));

            Assert.IsTrue(A[newaxis, ellipsis, newaxis].Equals(A[newaxis, full, full, full, full, full, full, full, full, newaxis]));
            Assert.IsTrue(A[newaxis, newaxis, ellipsis, newaxis].Equals(A[newaxis, newaxis, full, full, full, full, full, full, full, full, newaxis]));
            Assert.IsTrue(A[newaxis, ellipsis, newaxis, newaxis].Equals(A[newaxis, full, full, full, full, full, full, full, full, newaxis, newaxis]));
            Assert.IsTrue(A[ellipsis, newaxis, newaxis, newaxis].Equals(A[full, full, full, full, full, full, full, full, newaxis, newaxis, newaxis]));
            Assert.IsTrue(A[ellipsis, newaxis, 0, newaxis].Equals(A[full, full, full, full, full, full, full, newaxis, 0, newaxis]));
            Assert.IsTrue(A[0, ellipsis, newaxis, newaxis].Equals(A[0, full, full, full, full, full, full, full, newaxis, newaxis]));

            A = counter<double>(1.0, 1.0, size(5 ));
            Assert.IsTrue(A[ellipsis, newaxis, 0, newaxis].Equals(A[newaxis, 0, newaxis]));
            Assert.IsTrue(A[0, ellipsis, newaxis, newaxis].Equals(A[0, newaxis, newaxis]));
            Assert.IsTrue(A[0, newaxis, ellipsis, newaxis].Equals(A[0, newaxis, newaxis]));

            A = counter<double>(1.0, 1.0, size(5, 4));
            Assert.IsTrue(A[ellipsis, newaxis, 0, newaxis].Equals(A[full, newaxis, 0, newaxis]));
            Assert.IsTrue(A[0, ellipsis, newaxis, newaxis].Equals(A[0, full, newaxis, newaxis]));
            Assert.IsTrue(A[0, newaxis, ellipsis, newaxis].Equals(A[0, newaxis, full, newaxis]));

            A = counter<double>(1.0, 1.0, size(5, 4, 3));
            Assert.IsTrue(A[ellipsis, newaxis, 0, newaxis].Equals(A[full, full, newaxis, 0, newaxis]));
            Assert.IsTrue(A[0, ellipsis, newaxis, newaxis].Equals(A[0, full, full, newaxis, newaxis]));
            Assert.IsTrue(A[0, newaxis, ellipsis, newaxis].Equals(A[0, newaxis, full, full, newaxis]));

            A = counter<double>(1.0, 1.0, size(5, 4, 3, 2));
            Assert.IsTrue(A[ellipsis, newaxis, 0, newaxis].Equals(A[full, full, full, newaxis, 0, newaxis]));
            Assert.IsTrue(A[0, ellipsis, newaxis, newaxis].Equals(A[0, full, full, full, newaxis, newaxis]));
            Assert.IsTrue(A[0, newaxis, ellipsis, newaxis].Equals(A[0, newaxis, full, full, full, newaxis]));

            // erase superfluous ellipsis
            Assert.IsTrue(A[0, ellipsis, newaxis, newaxis].Equals(A[0, full, full, full, newaxis, newaxis]));
            Assert.IsTrue(A[0, ellipsis, ellipsis, newaxis, newaxis].Equals(A[0, full, full, full, newaxis, newaxis]));
            Assert.IsTrue(A[0, ellipsis, newaxis, ellipsis, newaxis, ellipsis, ellipsis].Equals(A[0, full, full, full, newaxis, newaxis]));

            A = counter<double>(1.0, 1.0, size(5, 4, 3, 2, 1));
            Assert.IsTrue(A[ellipsis, newaxis, 0, newaxis].Equals(A[full, full, full, full,  newaxis, 0, newaxis]));
            Assert.IsTrue(A[0, ellipsis, newaxis, newaxis].Equals(A[0, full, full, full, full,  newaxis, newaxis]));
            Assert.IsTrue(A[0, newaxis, ellipsis, newaxis].Equals(A[0, full, full, full, full,  newaxis, newaxis]));

        }

        //[TestMethod]
        //public void SetRange_np_ParamsDimSpec_scalars() {

        //    Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
        //    A.SetRange(-1, r(1, 1)); Assert.IsTrue((double)A.Subarray(ones(1)) == -1); A.SetRange(99, r(1, 1));
        //    A.SetRange(-1, r(1, 1), 0); Assert.IsTrue((double)A.Subarray(ones(1), 0) == -1); A.SetRange(99, r(1, 1), 0);
        //    A.SetRange(-1, r(1, 1), 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
        //    A.SetRange(-1, r(1, 1), 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
        //    A.SetRange(-1, r(1, 1), 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
        //    A.SetRange(-1, r(1, 1), 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
        //    A.SetRange(-1, r(1, 1), 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
        //    A.SetRange(-1, r(1, 1), 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
        //    A.SetRange(-1, r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
        //    A.SetRange(-1, r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
        //    A.SetRange(-1, r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
        //    A.SetRange(-1, r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);

        //}
        //[TestMethod]
        //public void SetRange_ML_paramsDimSpec_ellipsis() {
        //    Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));  
        //    // returns range
        //    A.SetRange(-1, ellipsis); Assert.IsTrue(allall(A == -1)); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
        //    A.SetRange(-1, r(1, 1), ellipsis);
        //    {
        //        Assert.IsTrue(A[0, full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[0, full, full]));
        //        Assert.IsTrue(allall(A[1, full, full] == -1));
        //        Assert.IsTrue(A[r(2, end), full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[r(2,end), full, full]));

        //        A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
        //    }
        //    A.SetRange(-1, r(1, 1), 0, ellipsis); 
        //    {
        //        Assert.IsTrue(A[full, r(1,end), full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[full, r(1, end), full]));
        //        Assert.IsTrue(A[0, full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[0, full, full]));
        //        Assert.IsTrue(allall(A[1, 0, full] == -1));
        //        Assert.IsTrue(A[r(2,end), full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[r(2,end), full, full]));
        //        A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
        //    }
        //    //returns scalar
        //    A.SetRange(-1, r(1, 1), 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
        //    A.SetRange(-1, r(1, 1), 0, 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
        //    A.SetRange(-1, r(1, 1), 0, 0, 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
        //    A.SetRange(-1, r(1, 1), 0, 0, 0, 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
        //    A.SetRange(-1, r(1, 1), 0, 0, 0, 0, 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
        //    A.SetRange(-1, r(1, 1), 0, 0, 0, 0, 0, 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
        //    A.SetRange(-1, r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
        //    A.SetRange(-1, r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
        //    A.SetRange(-1, r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ellipsis) == -1);

        //}


    }
}
