using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.InteropServices;

namespace ILNumerics.Core.UnitTests {
    public class ArrayAssert { 

        public static unsafe void ValuesEqual<T>(Array a, T[] b) {

            Assert.IsTrue(a != null && b != null);
            Assert.IsTrue(a.LongLength == b.LongLength);  
            var aType = a.GetType().GetElementType();
            Assert.IsTrue(aType == b.GetType().GetElementType());

            var bSize = Marshal.SizeOf<T>();
            GCHandle hA = GCHandle.Alloc(a, GCHandleType.Pinned);
            GCHandle hB = GCHandle.Alloc(b, GCHandleType.Pinned); 
            try {
                byte* pA = (byte*)hA.AddrOfPinnedObject(); 
                byte* pB = (byte*)hB.AddrOfPinnedObject();
                for (ulong i = 0; i < (ulong) (bSize * a.LongLength); i++) {
                    Assert.IsTrue(pA[i] == pB[i], $"Bytes at position i={i}, element# {i / (ulong)bSize} are not equal. left:{pA[i]} right:{pB[i]}"); 
                }
            } finally {
                if (hA.IsAllocated) hA.Free(); 
                if (hB.IsAllocated) hB.Free(); 
            }
        }
    }
}
