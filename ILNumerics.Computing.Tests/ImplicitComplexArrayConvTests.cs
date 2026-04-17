using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics; 
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

namespace ImplicitComplexArrayConvTests {

    [TestClass]
    public class ComplexImplConvTests {

        //[TestMethod]
        //public void ComplexImplSimple001() {

        //    Array<double> a = 1.0;

        //    Array<double> r = a * -1.0; 

        //    Array<complex> A = ccomplex(1.0, 2.0);
        //    complex c = -1.0; 

        //    Array<complex> R = A * c; 

        /* Note: currently, it is not possible to do: A * 1.0. We could enable it (no need to convert 1.0 -> complex) but this would 
         * mean to accept ALL parameters as second parameter, which will be (implicitly) converted to BaseArray<T1>. Errors due to 
         * unmatching element types will only be generated at runtime. This would cause a large set of errors to be delayed until runtime
         * and we would loose the (nice) compiler support at development time here. Therefore, we do not provide this features for now. 
         * Users must explicitly cast the scalar, non-type-matching parameter to the corresponding array type instead: (complex)-1.0 or: new complex(-1.0, 0.0)
         */
        //}

    }
}
