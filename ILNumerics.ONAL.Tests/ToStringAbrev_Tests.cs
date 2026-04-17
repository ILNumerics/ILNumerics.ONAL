using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics; 
using static ILNumerics.Globals;
using static ILNumerics.ILMath;
using System.Diagnostics; 

namespace Core_Tests_small {

    [TestClass]
    public class ToStringAbrev_Tests {

        [TestMethod]
        public void InArrayPerformance() {
            var count = 05;

            Array<double> array = arange<double>(1.0, 3000 * 3001).Reshape(3000, 3001);
            {
                var time1 = 0L;
                for (int i = 0; i < count; i++) {
                    using (Scope.Enter()) {
                        Array<double> retArray1 = array + i;
                        Array<double> retArray2 = array - i;
                        Stopwatch sw = new Stopwatch();
                        sw.Start();
                        var multiply1 = retArray1 * retArray2;
                        sw.Stop();
                        Console.WriteLine("Elapsed={0}", sw.ElapsedMilliseconds);
                        time1 += sw.ElapsedMilliseconds;
                    }
                }

                Console.WriteLine("Total time={0}. \n", time1);

                var time2 = 0L;
                for (int i = 0; i < count; i++) {
                    InArray<double> inArray1 = array + i;
                    InArray<double> inArray2 = array - i;
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    var multiply2 = inArray1 * inArray2;
                    sw.Stop();
                    Console.WriteLine("Elapsed={0}", sw.ElapsedMilliseconds);
                    time2 += sw.ElapsedMilliseconds;
                }

                Console.WriteLine("Total time={0}. \n", time2);
            }
        }

        [TestMethod]
        public void ToString_simple() {

            Array<double> A = counter(5, 6, 7);

            var s = A.ToString(maxNumberElementsPerDimension: 6, maxNumberElements: 1000, style: StorageOrders.ColumnMajor);
        }

        [TestMethod]
        public void cross_simpleDim() {

            Array<double> A = vector(0.0, 0, 1) * ones(3, 4, 5);
            Array<double> B = vector(0.0, 1, 0) * ones(3, 4, 5);

            Array<double> C = cross(A, B, dimension: 0);

            Array<double> Res = vector(-1.0, 0, 0) * ones(3, 4, 5);

            Array<double> Af = vector(0.0, 0, 1), Bf = vector(0.0, 1, 0), Cf = vector(-1.0, 0, 0);

            Assert.IsTrue(cross(Af * ones(3, 4, 5), Bf * ones(3, 4, 5), dimension: 0).Equals(Cf * ones(3, 4, 5)));
            Assert.IsTrue(cross(Af.T * ones(4, 3, 5), Bf.T * ones(4, 3, 5), dimension: 1).Equals(Cf.T * ones(4, 3, 5)));
            using (Scope.Enter(ArrayStyles.numpy)) {
                Assert.IsTrue(cross(Af[newaxis] * ones(4, 3, 5), Bf[newaxis] * ones(4, 3, 5), dimension: 1).Equals(Cf[newaxis] * ones(4, 3, 5)));
                Assert.IsTrue(cross(Af.T[newaxis] * ones(4, 5, 3), Bf.T[newaxis] * ones(4, 5, 3), dimension: 2).Equals(Cf.T[newaxis] * ones(4, 5, 3)));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void cross_invalidDimNoAtleast3Fails() {

            cross(ones(2, 2, 1), ones(2, 2, 1));
        }

    }
}
