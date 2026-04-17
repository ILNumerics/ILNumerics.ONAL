using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics;
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.Tests {
    [TestClass]
    public class Indexers_Overloads32{

        [TestMethod]
        public void GetRange_this_overloads_mutable() {

            Array<uint> A = counter<uint>(1, 1, size(5, 4, 1, 1, 1, 1, 1, 3)); // 8 dim

            Assert.IsTrue(A[0L] == 1);
            Assert.IsTrue(A[r(0, 0)] == 1);
            Assert.IsTrue(A[zeros(1)] == 1);

            Assert.IsTrue(A[0, 0L] == 1);
            Assert.IsTrue(A[r(0, 0), 0L] == 1);
            Assert.IsTrue(A[zeros(1), 0L] == 1);

            Assert.IsTrue(A[0L, 0, 0L] == 1);
            Assert.IsTrue(A[0L, r(0, 0), 0L] == 1);
            Assert.IsTrue(A[0L, zeros(1), 0L] == 1);


            Assert.IsTrue(A[0L, 0L, 0, 0L] == 1);
            Assert.IsTrue(A[0L, 0L, r(0, 0), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, zeros(1), 0L] == 1);

            Assert.IsTrue(A[0L, 0L, 0L, 0, 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, r(0, 0), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, zeros(1), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0, 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, r(0, 0), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, zeros(1), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, 0, 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, r(0, 0), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, zeros(1), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, 0L, 0, 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, 0L, r(0, 0), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, 0L, zeros(1), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, 0L, 0L, 0, 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, 0L, 0L, r(0, 0), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, 0L, 0L, zeros(1), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0, 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, r(0, 0), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, zeros(1), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0, 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, r(0, 0), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, zeros(1), 0L] == 1);

            Assert.IsTrue(A[0u, 0u, 0u, 0, 0u] == 1);
            Assert.IsTrue(A[0u, 0u, 0u, 0u, 0u, 0u, 0, 0u] == 1);
            Assert.IsTrue(A[0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0, 0u] == 1);

        }

        [TestMethod]
        public void GetRange_this_overloads_immutable() {

            InArray<uint> A = counter<uint>(1, 1, size(5, 4, 1, 1, 1, 1, 1, 3)); // 8 dim

            Assert.IsTrue(A[0L] == 1);
            Assert.IsTrue(A[r(0, 0)] == 1);
            Assert.IsTrue(A[zeros(1)] == 1);

            Assert.IsTrue(A[0, 0L] == 1);
            Assert.IsTrue(A[r(0, 0), 0L] == 1);
            Assert.IsTrue(A[zeros(1), 0L] == 1);

            Assert.IsTrue(A[0L, 0, 0L] == 1);
            Assert.IsTrue(A[0L, r(0, 0), 0L] == 1);
            Assert.IsTrue(A[0L, zeros(1), 0L] == 1);


            Assert.IsTrue(A[0L, 0L, 0, 0L] == 1);
            Assert.IsTrue(A[0L, 0L, r(0, 0), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, zeros(1), 0L] == 1);

            Assert.IsTrue(A[0L, 0L, 0L, 0, 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, r(0, 0), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, zeros(1), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0, 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, r(0, 0), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, zeros(1), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, 0, 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, r(0, 0), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, zeros(1), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, 0L, 0, 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, 0L, r(0, 0), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, 0L, zeros(1), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, 0L, 0L, 0, 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, 0L, 0L, r(0, 0), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, 0L, 0L, zeros(1), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0, 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, r(0, 0), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, zeros(1), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0, 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, r(0, 0), 0L] == 1);
            Assert.IsTrue(A[0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, 0L, zeros(1), 0L] == 1);

            Assert.IsTrue(A[0u, 0u, 0u, 0, 0u] == 1);
            Assert.IsTrue(A[0u, 0u, 0u, 0u, 0u, 0u, 0, 0u] == 1);
            Assert.IsTrue(A[0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0u, 0, 0u] == 1);

        }

        [TestMethod]
        public void SetRange_this_overloads_mutable() {

            Array<uint> A = counter<uint>(1, 1, size(5, 4, 1, 1, 1, 1, 1, 3)); // 8 dim
            A[0L] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0u] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[r(0, 0)] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[zeros(1)] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;

            A[0, 0L] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0u] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, r(0, 0)] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, zeros(1)] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0L] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0u] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, r(0, 0)] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, zeros(1)] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, 0L] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, 0u] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, r(0, 0)] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, zeros(1)] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, 0, 0L] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, 0, 0u] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, 0, r(0, 0)] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, 0, zeros(1)] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, 0, 0, 0L] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, 0, 0, 0u] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, 0, 0, r(0, 0)] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, 0, 0, zeros(1)] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, 0, 0, 0, 0L] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, 0, 0, 0, 0u] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, 0, 0, 0, r(0, 0)] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, 0, 0, 0, zeros(1)] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, 0, 0, 0, 0, 0L] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, 0, 0, 0, 0, 0u] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, 0, 0, 0, 0, r(0, 0)] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, 0, 0, 0, 0, zeros(1)] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, 0, 0, 0, 0, 0, 0L] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, 0, 0, 0, 0, 0, 0u] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, 0, 0, 0, 0, 0, r(0, 0)] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, 0, 0, 0, 0, 0, zeros(1)] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0L] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0u] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, r(0, 0)] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;
            A[0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, zeros(1)] = 99; Assert.IsTrue(A[0] == 99); A[0L] = 1;

        }

    }
}
