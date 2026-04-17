using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ILNumerics.Globals; 
using static ILNumerics.ILMath;

namespace ILNumerics.Core.UnitTests.MathInternalTests {
    [TestClass]
    public class MathInternal_applyTests {

        [TestMethod]
        public void apply_Double_simple() {
            Array<double> A = counter<double>(1.0, 1.0, 10, 20, 30);
            Array<double> C = apply(A, -1, (a, b) => a * b);
            Array<double> Res = counter<double>(-1.0, -1.0, 10, 20, 30);
            Assert.IsTrue(allall(C == Res));
        }
        [TestMethod]
        public void apply_Double_simple_MT3() {
            Array<double> A = counter<double>(1.0, 1.0, 10, 20, 30);
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {
                Array<double> C = apply(A, -1, (a, b) => a * b);
                Array<double> Res = counter<double>(-1.0, -1.0, 10, 20, 30);
                Assert.IsTrue(allall(C == Res));
            }
        }
        [TestMethod]
        public void apply_Double_simple_MT3lg() {
            Array<double> A = counter<double>(1.0, 1.0, 10000, 20, 30);
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {
                Array<double> C = apply(A, -1, (a, b) => a * b);
                Array<double> Res = counter<double>(-1.0, -1.0, 10000, 20, 30);
                Assert.IsTrue(allall(C == Res));
            }
        }
        [TestMethod]
        public void apply_Double_simple_MT3lg2() {
            Array<double> A = counter<double>(1.0, 1.0, 10000, 20, 30);
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {
                Array<double> C = apply(A, -1, (a, b) => a * b);
                Array<double> Res = counter<double>(-1.0, -1.0, 10000, 20, 30);
                Assert.IsTrue(allall(C == Res));
            }
        }
        [TestMethod]
        public void apply_Double_simple_MT4lg() {
            Array<double> A = counter<double>(1.0, 1.0, 100000, 20, 10);
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {
                Array<double> C = apply(A, -1, (a, b) => a * b);
                Array<double> Res = counter<double>(-1.0, -1.0, 100000, 20, 10);
                Assert.IsTrue(allall(C == Res));
            }
        }
        [TestMethod]
        public void apply_Double_simple_MT5lg() {
            Array<double> A = counter<double>(1.0, 1.0, 100000, 20, 10);
            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {
                Array<double> C = apply(A, -1, (a, b) => a * b);
                Array<double> Res = counter<double>(-1.0, -1.0, 100000, 20, 10);
                Assert.IsTrue(allall(C == Res));
            }
        }
        [TestMethod]
        public void apply_String_simple() {
            Array<string> A = vector(new[] { "1", "2", "3", "4", "5", "6" }).Reshape(2,3, StorageOrders.RowMajor);
            
            Array<string> C = apply<string>("u", A, (a, b) => b + a);
            Array<string> Res = vector(new[] { "1u", "2u", "3u", "4u", "5u", "6u" }).Reshape(2, 3, StorageOrders.RowMajor);

        }

        [TestMethod]
        public void apply_string_MT3() {
            Array<string> A = array("", 100, 200, StorageOrders.RowMajor); 
            foreach(var i in A.S.Iterator()) {
                A.SetValue(i.ToString(), i); 
            }
            Assert.IsTrue(A.GetValue(111) == "111");

            Array<string> B = apply("heäh?", A, (a, b) => a + b + "_");
            //Assert.IsFalse(anyall(B == string.Empty));
            for (int i = 0; i < A.S.NumberOfElements; i++) {
                Assert.IsTrue(B.GetValue(i) == $"heäh?{i}_");
            }

        }

        [TestMethod]
        public void apply_generic_simple() {
            Array<double> A = counter<double>(-1, -1, 5, 4, 3);
            Array<int> B = 2;
            Array<string> C = apply(A, B, (a, b) => $"{a}+{b}={a + b}");

            foreach (var i in A.S.Iterator(StorageOrders.ColumnMajor)) {
                Assert.IsTrue(C.GetValue(i) == $"{A.GetValue(i)}+{2}={A.GetValue(i) + 2}");
            }
        }
        [TestMethod]
        public void apply_generic_MT3lg() {
            Array<double> A = counter<double>(-1, -1, 100, 20, 30);
            Array<int> B = 2;
            Array<string> C;
            using (Settings.Ensure(()=> Settings.MaxNumberThreads, 3u)) 
                C = apply(A, B, (a, b) => $"{a}+{b}={a + b}");

            foreach (var i in A.S.Iterator(StorageOrders.ColumnMajor)) {
                Assert.IsTrue(C.GetValue(i) == $"{A.GetValue(i)}+{2}={A.GetValue(i) + 2}");
            }
        }

        #region access T via pointers - playground
        [TestMethod]
        public unsafe void apply_pointer_playground() {

            double a = 1;
            double[] A = new double[] { -1, -2 };
            double* sA = stackalloc double[2];

            Func(a); 
            Func(A);
            return; 
            //var pa = AddressOfStructVariable(ref a);
            //var psA = AddressOfStructVariable(ref sA[0]);
            //var pA = AddressOfVariableToClass(ref A);

            //var pa2 = System.Runtime.CompilerServices.Unsafe.AsPointer(ref a);
            //var psA2 = System.Runtime.CompilerServices.Unsafe.Read<double>((void*)&psA);
            //var pA2 = System.Runtime.CompilerServices.Unsafe.Read<double[]>((void*)&pA);

            //* ((double*)pa) = 99;

            //var pa = ILNumerics.Core.Global.Helper.AddressOf(a);
            //var p2 = System.Runtime.CompilerServices.Unsafe.AsPointer(ref a); 
            //var par = Global.Helper.AddressOf(ref a);
            //*((double*)pa) = 99;
            //*((double*)pa) = 999;

            // see: https://stackoverflow.com/questions/34935659/create-a-copy-of-method-from-il
            // and: https://limbioliong.wordpress.com/2011/07/22/passing-a-reference-parameter-to-type-memberinvoke/

            /*
             * typeof(System.Runtime.CompilerServices.Unsafe).GetMethod("AsPointer").GetMethodBody().GetILAsByteArray()
{byte[3]}
    [0]: 2
    [1]: 224
    [2]: 42
             */
        }

        private static unsafe void Func<T>(T val) {
            T local = val;
            if (default(T) is ValueType) {

                //IntPtr transfA = AddressOfStructVariable(ref local);
                IntPtr transfA = (IntPtr)System.Runtime.CompilerServices.Unsafe.AsPointer(ref local); 
                T copiedLocal = System.Runtime.CompilerServices.Unsafe.Read<T>((void*)transfA);

                Assert.IsTrue(Equals(local, copiedLocal), $"ValueType: local: {local}. copy: {copiedLocal}"); 

            } else {
                System.Diagnostics.Debug.Assert(!(default(T) is ValueType));

                //IntPtr transfA = AddressOfVariableToClass(ref local);
                IntPtr transfA = (IntPtr)System.Runtime.CompilerServices.Unsafe.AsPointer(ref local);
                T copiedLocal = System.Runtime.CompilerServices.Unsafe.Read<T>((void*)transfA);

                Assert.IsTrue(ReferenceEquals(local, copiedLocal), $"RefType: local: {local}. copy: {copiedLocal}");
            }
        }
#pragma warning disable CS8500
        private static unsafe IntPtr AddressOfVariableToClass<T>(ref T val) {

            System.TypedReference reference = __makeref(val);
            return **(System.IntPtr**)(&reference);

        }
        private static unsafe IntPtr AddressOfStructVariable<T>(ref T val) {

            System.TypedReference reference = __makeref(val);
            return *(System.IntPtr*)(&reference);

        }

#pragma warning restore
        #endregion

        [TestMethod]
        public void apply_writeToStructs() {

            Array<uint> A = counter<uint>(1, 1, 50, 40, 30, 2);
            Array<string> B = "-";

            Array<float> R = apply(A, B, (a, b) => {
                return float.Parse($"{b}{a}");
            });

            string b_ = B.GetValue(0);
            foreach (var i in A.S.Iterator(StorageOrders.ColumnMajor)) {
                uint a = A.GetValue(i);
                Assert.IsTrue(R.GetValue(i) == float.Parse($"{b_}{a}"));
            }
        }
        [TestMethod]
        public void apply_writeToStructs_MT() {

            Array<uint> A = counter<uint>(1, 1, 50, 40, 30, 20);
            Array<string> B = "-";
            Array<float> R;

            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {
                R = apply(A, B, (a, b) => float.Parse($"{b}{a}"));
            }

            string b_ = B.GetValue(0);
            foreach (var i in A.S.Iterator(StorageOrders.ColumnMajor)) {
                uint a = A.GetValue(i);
                Assert.IsTrue(R.GetValue(i) == float.Parse($"{b_}{a}"));
            }
        }
        [TestMethod]
        public void apply_writeToStructs_MT_rev() {

            Array<uint> A = counter<uint>(1, 1, 50, 40, 30, 20);
            Array<string> B = "-";
            Array<float> R;

            using (Settings.Ensure(() => Settings.MaxNumberThreads, 3u)) {
                R = apply(B, A, (a, b) => float.Parse($"{a}{b}"));
            }

            string b_ = B.GetValue(0);
            foreach (var i in A.S.Iterator(StorageOrders.ColumnMajor)) {
                uint a = A.GetValue(i);
                Assert.IsTrue(R.GetValue(i) == float.Parse($"{b_}{a}"));
            }
        }

        [TestMethod]
        public void apply_complex() {
            Array<short> A = vector<short>(1, 2, 3, 4);
            Array<string> B = "-"; 
            Array<complex> R = apply(A * A.T, B, (a, b) => new complex(double.Parse($"{b}{a}"), a));

            string b_ = B.GetValue(0);
            foreach (var i in A.S.Iterator(StorageOrders.ColumnMajor)) {
                short a = A.GetValue(i);
                Assert.IsTrue(R.GetValue(i) == new complex(double.Parse($"{b_}{a}"), a));
            }
        }

        [TestMethod]
        public void apply_empty() {
            Assert.IsTrue(apply<float, string, double>(empty<float>(1, 2, 0, 3), "hello", (a, b) => 1.0).IsEmpty);
            Assert.IsTrue(apply<float, string, double>(empty<float>(1, 2, 0, 3), "hello", (a, b) => 1.0).S[0] == 1);
            Assert.IsTrue(apply<float, string, double>(empty<float>(1, 2, 0, 3), "hello", (a, b) => 1.0).S[1] == 2);
            Assert.IsTrue(apply<float, string, double>(empty<float>(1, 2, 0, 3), "hello", (a, b) => 1.0).S[2] == 0);
            Assert.IsTrue(apply<float, string, double>(empty<float>(1, 2, 0, 3), "hello", (a, b) => 1.0).S[3] == 3);
            Assert.IsTrue(apply<float, string, double>(empty<float>(1, 2, 0, 3), "hello", (a, b) => 1.0).S.NumberOfDimensions == 4);

        }
        [TestMethod]
        public void apply_scalar() {
            Array<float> A = -1;
            Array<string> B = "hello";
            Assert.IsTrue(apply<float, string, double>(A, B, (a, b) => a).IsScalar);
            Assert.IsTrue(apply<float, string, double>(A, B, (a, b) => a).S[0] == 1);
            Assert.IsTrue(apply<float, string, double>(A, B, (a, b) => a).S[1] == 1);
            Assert.IsTrue(apply<float, string, double>(A, B, (a, b) => a).S[2] == 1);
            Assert.IsTrue(apply<float, string, double>(A, B, (a, b) => a).S[3] == 1);
            Assert.IsTrue(apply<float, string, double>(A, B, (a, b) => a).S.NumberOfDimensions == 2);
            Assert.IsTrue(apply<float, string, double>(A, B, (a, b) => a).GetValue(0) == -1);

        }
        [TestMethod]
        public void apply_scalarNP() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                Array<float> A = -1;
                Array<string> B = "hello";
                Assert.IsTrue(apply<float, string, double>(A, B, (a, b) => a).IsScalar);
                Assert.IsTrue(apply<float, string, double>(A, B, (a, b) => a).S[0] == 1);
                Assert.IsTrue(apply<float, string, double>(A, B, (a, b) => a).S[1] == 1);
                Assert.IsTrue(apply<float, string, double>(A, B, (a, b) => a).S[2] == 1);
                Assert.IsTrue(apply<float, string, double>(A, B, (a, b) => a).S[3] == 1);
                Assert.IsTrue(apply<float, string, double>(A, B, (a, b) => a).S.NumberOfDimensions == 0);
                Assert.IsTrue(apply<float, string, double>(A, B, (a, b) => a).GetValue(0) == -1);
            }
        }

    }
}
