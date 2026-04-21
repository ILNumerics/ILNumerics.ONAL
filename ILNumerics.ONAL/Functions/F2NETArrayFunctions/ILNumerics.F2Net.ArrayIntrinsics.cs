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
using ILNumerics.Core.Arrays;
using ILNumerics.Core.Functions.Builtin;
using ILNumerics.Core.StorageLayer;

namespace ILNumerics.F2NET.Array {
    public static partial class Intrinsics {

        public static double MAXVAL(BaseArray<double> A) {
            var a = A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>;
            if (a.Storage.S.NumberOfElements == 0) {
                return double.MinValue;
            }
            return (double)MathInternal.maxall(a);
        }
        public static float MAXVAL(BaseArray<float> A) {
            var a = A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>;
            if (a.Storage.S.NumberOfElements == 0) {
                return float.MinValue;
            }
            return (float)MathInternal.maxall(A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>);
        }

        /* NOTE: TODO: (1) the mask parameter is actually specified as LOGICAL array of conforming size. In LAPACK it is not used and always '1' (as of version 3.9.0.). 
         * Hence, we make our live easier for now by ignoring it. It is, therefore, defined as 'int' to enable automatic resolution. We error, if this 
         * function will ever be called with another value than 1. 
         * (2) the return type is actually an array with the (1-based) indices of the largest element value found (dimensional indices). Since in vers. 3.9.0
         * this function is only used with 1-dimensional arrays A and since in LAPACK it is only assigned to scalars we - again - simplify things here and return a scalar.
         */ 
        public static int MAXLOC(BaseArray<double> A, int dummyMask = 1) {
            var a = A as ConcreteArray<double, Array<double>, InArray<double>, OutArray<double>, Array<double>, Storage<double>>;
            if (a.Storage.S.NumberOfElements == 0) {
                return 0;
            }
            using (Scope.Enter()) {
                Array<long> ind = 0; 
                MathInternal.maxall(a, index: ind, order: StorageOrders.ColumnMajor, ignoreNaN: false).Dispose();
                return (int)ind; 
            }
        }
        public static int MAXLOC(BaseArray<float> A, int dummyMask = 1) {
            var a = A as ConcreteArray<float, Array<float>, InArray<float>, OutArray<float>, Array<float>, Storage<float>>;
            if (a.Storage.S.NumberOfElements == 0) {
                return 0;
            }
            Array<long> ind = 0;
            MathInternal.maxall(a, index: ind, order: StorageOrders.ColumnMajor, ignoreNaN: false).Dispose();
            return (int)ind;
        }

    }
}
