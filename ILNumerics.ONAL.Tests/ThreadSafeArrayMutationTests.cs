using System;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.Globals;
using static ILNumerics.ILMath;
using System.Threading.Tasks;

namespace ILNumerics.Core.UnitTests {

    [TestClass]
    public class ThreadSafeArrayMutationTests {

        [TestMethod]
        public void ReadWhileMutatingILNV4_Test() {

            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {
                // an array is read from while other threads mutate the array. 
                // Expected outcome: result is undetermined. No errors occour! (arrays remain integrity and never corrupt)
                Array<double> A = zeros<double>(1000, 1000);
                Array<double> R = zeros<double>(1000, 1000);

                // each row reflects the current values already computed. 
                for (int i = 0; i < R.Length; i++) {

                    R[i, full] = R[full, i].T + i;

                }
                //ILN(enabled = false)
                Parallel.For(0, 1000, i => {

                    A[i, full] = A[full, i].T + i;

                });
                //ILN(enabled = true)

                // Don't even try to check result element values here! They cannot be "correct" without proper locking inside the parallel loop.
                //Assert.IsTrue(A.Equals(R));  // never.ever. !! 

                // If we end here, we did not see an error and all is fine! 
                Assert.IsTrue(A.S.IsSameSize(R.S));  // never.ever. !! 
            }
        }

        [TestMethod]
        public void ReadWhileMutatingNumpy_Test() {

            using (Scope.Enter(ArrayStyles.numpy)) {
                // an array is read from while other threads mutate the array. 
                // Expected outcome: result is undetermined. No errors occour! (arrays remain integrity and never corrupt)
                Array<double> A = zeros<double>(1000, 1000);
                Array<double> R = zeros<double>(1000, 1000);

                // each row reflects the current values already computed. 
                for (int i = 0; i < R.Length; i++) {

                    R[i, full] = R[full, i].T + i;

                }

                //ILN(enabled = false)
                Parallel.For(0, 1000, i => {

                    A[i, full] = A[full, i].T + i;

                });
                //ILN(enabled = true)

                // Don't even try to check result element values here! They cannot be "correct" without proper locking inside the parallel loop.
                //Assert.IsTrue(A.Equals(R));  // never.ever. !! 

                // If we end here, we did not see an error and all is fine! 
                Assert.IsTrue(A.S.IsSameSize(R.S));  // never.ever. !! 
            }
        }

        [TestMethod]
        public void ExpandConcurrently_ILNV4_Test() {

            // an array is expanded from multiple threads. no data can get missing! 
            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {

                Array<long> A = 0; // lazy copy-on write at first iteration

                Array<long> R = A.C;

                //ILN(enabled = false)
                Parallel.For(1, 1000, i => {
                    
                    Settings.ArrayStyle = ArrayStyles.ILNumericsV4; 
                    A[i] = i;

                });
                //ILN(enabled = true)

                for (int i = 1; i < 1000; i++) {

                    R[i] = i;

                }
                Assert.IsTrue(A.Equals(R));

            }
        }
        [TestMethod]
        public void GetSetEarlierElements_Numpy_Test() {

            // an array is expanded from multiple threads. no data can get missing! 
            using (Scope.Enter(ArrayStyles.numpy)) {

                Array<long> A = zeros<long>(1000, 1000); 

                Array<long> R = A.C;

                //ILN(enabled = false)
                Parallel.For(1, 1000, i => {

                    A.SetValue(A.GetValue(i-1, i-1) + 1, i, i);

                });
                //ILN(enabled = true)

                for (int i = 1; i < 1000; i++) {

                    R.SetValue(R.GetValue(i - 1, i - 1) + 1, i, i);

                }
                Assert.IsTrue(A.S.IsSameSize(R.S));

            }
        }
    }
}
