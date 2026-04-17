using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics; 
using static ILNumerics.Globals;
using static ILNumerics.ILMath;
namespace SpecialFunctionsTest
{
    [TestClass]
    public class LogFactorialTest 
    {
        [TestMethod]
        public void LogFactorialTestInputScalar()
        {
            Assert.IsTrue(floor(exp(factorialLog(5)))== 120, "The logFactorial of 5 should return log(120)");
            Assert.IsTrue(floor((exp(factorialLog(0)))) == 1, "The logFactorial of 0 should return 0");
            Assert.IsTrue(isnan(factorialLog(-5)), "The logFactorial of a negative number should return NaN");
        }
        [TestMethod]
        public void LogFactorialTestInputArray()
        {
            Array<int> B = 5 * ones<int>(1000, 200);
            Array<double> FactB = 120 * ones<double>(1000, 200);
            Assert.IsTrue(floor(exp(factorialLog(B))).Equals(FactB), "Should return a vector of 120");
        }
        [TestMethod]
        public void LogFactorialTestInputArrayCounter()
        {
            Array<int> B = counter<int>(1, 1, 5, 2);
            Array<double> FactB = array<double>(new double[] { (double)factorialLog(1), (double)factorialLog(2), 
                (double)factorialLog(3), (double)factorialLog(4), 
                (double)factorialLog(5), (double)factorialLog(6), 
                (double)factorialLog(7), (double)factorialLog(8), 
                (double)factorialLog(9), (double)factorialLog(10) }, 5, 2);
            Assert.IsTrue(floor(exp(factorialLog(B))).Equals(floor(exp(FactB))), "Should return componentwise logFactorials");
        }
        [TestMethod]
        public void LogFactorialTestInputNegativewithNAN()
        {
            Array<int> C = counter<int>(-30, 1, 5, 5);
            Array<double> FactC = vector<double>((double)factorialLog(C[0, 0]), (double)factorialLog(C[1, 0]),
                (double)factorialLog(C[2, 0]), (double)factorialLog(C[3, 0]),
                (double)factorialLog(C[4, 0]), (double)factorialLog(C[0, 1]), 
                (double)factorialLog(C[1, 1]), (double)factorialLog(C[2, 1]), 
                (double)factorialLog(C[3, 1]), (double)factorialLog(C[4, 1]), 
                (double)factorialLog(C[0, 2]), (double)factorialLog(C[1, 2]),
                (double)factorialLog(C[2, 2]), (double)factorialLog(C[3, 2]), 
                (double)factorialLog(C[4, 2]), (double)factorialLog(C[0, 3]), 
                (double)factorialLog(C[1, 3]), (double)factorialLog(C[2, 3]), 
                (double)factorialLog(C[3, 3]), (double)factorialLog(C[4, 3]), 
                (double)factorialLog(C[0, 4]), (double)factorialLog(C[1, 4]), 
                (double)factorialLog(C[2, 4]), (double)factorialLog(C[3, 4]),
                (double)factorialLog(C[4, 4])).Reshape(5, 5);
            Assert.IsTrue(floor(exp(factorialLog(C))).Equals(floor(exp(FactC))), "Should return  the componentwise logFactorials with NaN");
        }

    }
}
