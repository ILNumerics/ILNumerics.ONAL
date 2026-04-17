using System;
using System.Linq; 
using System.Reflection;
using ILNumerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILNumerics.Core.UnitTests {

    [TestClass]
    public class ArrayPuplicAPITests {

        [TestMethod]
        public void ArrayExportValuesTest() {

            var SysArr = new uint[,] {
                { 1, 2, 3  },
                { 4, 5, 6  },
                { 7, 8, 9  },
                { 10, 11, 12 },
            };

             Array<uint> A = SysArr;

            uint[] data = null; // = new uint[A.S.NumberOfElements];
            A.ExportValues(ref data);
            Array<uint> B = data;
            B.a = B.Reshape(4, 3); 

            Assert.IsTrue(A.Equals(B));
            Assert.IsFalse(object.ReferenceEquals(SysArr, data));  
        }

        [TestMethod]
        public void MathInternal_nopublic_API() {

            BindingFlags bindFlags = BindingFlags.Static | BindingFlags.Public;
            var methods = typeof(ILNumerics.Core.Functions.Builtin.MathInternal).GetMethods(bindFlags);
            Assert.IsTrue(methods.Length == 0, "Unexpected public member of MathInternal: " + String.Join(",", methods.Select(m => m.Name)));

            var members = typeof(ILNumerics.Core.Functions.Builtin.MathInternal).GetMembers(bindFlags);
            Assert.IsTrue(members.Length == 0, "Unexpected public member of MathInternal: " + String.Join(",", members.Select(m => m.Name)));
            

        }
    }
}
