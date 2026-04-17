using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics;
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.Tests {
    [TestClass]
    public class SetRangeML_paramsDimSpec {

        [TestMethod]
        public void SetRange_ML_ParamsDimSpec_scalars() {
            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A.SetRange(-1, r(1, 1)); Assert.IsTrue((double)A.Subarray(ones(1)) == -1); A.SetRange(99, r(1, 1));
            A.SetRange(-1, r(1, 1), 0); Assert.IsTrue((double)A.Subarray(ones(1), 0) == -1); A.SetRange(99, r(1, 1), 0);
            A.SetRange(-1, r(1, 1), 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
            A.SetRange(-1, r(1, 1), 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
            A.SetRange(-1, r(1, 1), 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
            A.SetRange(-1, r(1, 1), 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
            A.SetRange(-1, r(1, 1), 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
            A.SetRange(-1, r(1, 1), 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
            A.SetRange(-1, r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
            A.SetRange(-1, r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
            A.SetRange(-1, r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);
            A.SetRange(-1, r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0) == -1); A.SetRange(99, r(1, 1), 0);

        }
        [TestMethod]
        public void SetRange_ML_paramsDimSpec_ellipsis() {
            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));  
            // returns range
            A.SetRange(-1, ellipsis); Assert.IsTrue(allall(A == -1)); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A.SetRange(-1, r(1, 1), ellipsis);
            {
                Assert.IsTrue(A[0, full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[0, full, full]));
                Assert.IsTrue(allall(A[1, full, full] == -1));
                Assert.IsTrue(A[r(2, end), full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[r(2,end), full, full]));

                A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            }
            A.SetRange(-1, r(1, 1), 0, ellipsis); 
            {
                Assert.IsTrue(A[full, r(1,end), full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[full, r(1, end), full]));
                Assert.IsTrue(A[0, full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[0, full, full]));
                Assert.IsTrue(allall(A[1, 0, full] == -1));
                Assert.IsTrue(A[r(2,end), full, full].Equals(counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1))[r(2,end), full, full]));
                A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            }
            //returns scalar
            A.SetRange(-1, r(1, 1), 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A.SetRange(-1, r(1, 1), 0, 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A.SetRange(-1, r(1, 1), 0, 0, 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A.SetRange(-1, r(1, 1), 0, 0, 0, 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A.SetRange(-1, r(1, 1), 0, 0, 0, 0, 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A.SetRange(-1, r(1, 1), 0, 0, 0, 0, 0, 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A.SetRange(-1, r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A.SetRange(-1, r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0) == -1); A.a = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1));
            A.SetRange(-1, r(1, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ellipsis); Assert.IsTrue((double)A.Subarray(ones(1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, ellipsis) == -1);

        }
        [TestMethod]
        public void SetRange_Non0BaseOffset_DimSpec_ML() {

            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1), StorageOrders.RowMajor)[2, ellipsis]
                .Reshape(size(4,3,1,1,1,1,1,1), StorageOrders.RowMajor);

            Assert.IsTrue(A.S.BaseOffset == 24);
            Assert.IsTrue(A.S.NumberOfDimensions == 8);
            Array<double> R = counter<double>(25, 1.0, size(4, 3, 1, 1, 1, 1, 1, 1), StorageOrders.RowMajor);
            Assert.IsTrue(A.Equals(R));

            A[r(1, 1), 0, 0, ellipsis] = -1;
            R.SetValue(-1, 1, 0);

            Assert.IsTrue(A.Equals(R));
            Assert.IsTrue(A.GetValue(1, 0) == -1);

        }
        [TestMethod]
        public void SetRange_ValNon0BaseOffset_DimSpec_ML() {
            //ILN(enabled=false)
            Array<double> A = counter<double>(1.0, 1.0, size(5, 4, 3, 1, 1, 1, 1, 1, 1), StorageOrders.RowMajor)[2, ellipsis]
                .Reshape(size(4, 3, 1, 1, 1, 1, 1, 1), StorageOrders.RowMajor);

            Assert.IsTrue(A.S.BaseOffset == 24);
            Assert.IsTrue(A.S.NumberOfDimensions == 8);
            Array<double> R = counter<double>(25, 1.0, size(4, 3, 1, 1, 1, 1, 1, 1), StorageOrders.RowMajor);
            Assert.IsTrue(A.Equals(R));

            Array<double> Val = vector(1.0, 2.0, -3.0)[2];
            Assert.IsTrue(Val.S.BaseOffset == 2);
            Assert.IsTrue(Val.GetValue(0) == -3.0);
            //ILN(enabled=true)

            A[r(1, 1), 0, 0, ellipsis] = Val;
            R.SetValue(-3, 1, 0);

            Assert.IsTrue(A.Equals(R));
            Assert.IsTrue(A.GetValue(1, 0) == -3);

        }

    }
}
