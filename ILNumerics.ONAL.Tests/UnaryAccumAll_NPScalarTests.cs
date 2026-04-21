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
//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILNumerics; 
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

namespace ILNumerics.Core.UnitTests {

    [TestClass]
    public class UnaryAccumAll_NPScalarTests : NumpyTestClass {

        #region HYCALPER LOOPSTART Allall_Anyall
        /*!HC:TYPELIST:
<hycalper>
<type>
<source locate="here">
double
</source>
<destination>float</destination>
<destination>complex</destination>
<destination>fcomplex</destination>
<destination>sbyte</destination>
<destination>byte</destination>
<destination>short</destination>
<destination>ushort</destination>
<destination>int</destination>
<destination>uint</destination>
<destination>long</destination>
<destination>ulong</destination>
</type>
<type>
<source locate="here">
1.0
</source>
<destination>1f</destination>
<destination>new complex(1.0, 0)</destination>
<destination>new fcomplex(1f, 0)</destination>
<destination>1</destination>
<destination>1</destination>
<destination>1</destination>
<destination>1</destination>
<destination>1</destination>
<destination>1</destination>
<destination>1</destination>
<destination>1</destination>
</type>
<type>
<source locate="here">
0.0
</source>
<destination>0f</destination>
<destination>new complex(0.0, 0.0)</destination>
<destination>new fcomplex(0f, 0)</destination>
<destination>0</destination>
<destination>0</destination>
<destination>0</destination>
<destination>0</destination>
<destination>0</destination>
<destination>0</destination>
<destination>0</destination>
<destination>0</destination>
</type>
</hycalper>
*/

        [TestMethod]
        public void NPScalar_allall_anyall_double() {

            Array<double> A = 1.0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(allall(A));
            Assert.IsTrue(anyall(A));

            A = 0.0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsFalse(allall(A));
            Assert.IsFalse(anyall(A));

        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        [TestMethod]
        public void NPScalar_allall_anyall_ulong() {

            Array<ulong> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(allall(A));
            Assert.IsTrue(anyall(A));

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsFalse(allall(A));
            Assert.IsFalse(anyall(A));

        }
       

        [TestMethod]
        public void NPScalar_allall_anyall_long() {

            Array<long> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(allall(A));
            Assert.IsTrue(anyall(A));

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsFalse(allall(A));
            Assert.IsFalse(anyall(A));

        }
       

        [TestMethod]
        public void NPScalar_allall_anyall_uint() {

            Array<uint> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(allall(A));
            Assert.IsTrue(anyall(A));

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsFalse(allall(A));
            Assert.IsFalse(anyall(A));

        }
       

        [TestMethod]
        public void NPScalar_allall_anyall_int() {

            Array<int> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(allall(A));
            Assert.IsTrue(anyall(A));

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsFalse(allall(A));
            Assert.IsFalse(anyall(A));

        }
       

        [TestMethod]
        public void NPScalar_allall_anyall_ushort() {

            Array<ushort> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(allall(A));
            Assert.IsTrue(anyall(A));

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsFalse(allall(A));
            Assert.IsFalse(anyall(A));

        }
       

        [TestMethod]
        public void NPScalar_allall_anyall_short() {

            Array<short> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(allall(A));
            Assert.IsTrue(anyall(A));

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsFalse(allall(A));
            Assert.IsFalse(anyall(A));

        }
       

        [TestMethod]
        public void NPScalar_allall_anyall_byte() {

            Array<byte> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(allall(A));
            Assert.IsTrue(anyall(A));

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsFalse(allall(A));
            Assert.IsFalse(anyall(A));

        }
       

        [TestMethod]
        public void NPScalar_allall_anyall_sbyte() {

            Array<sbyte> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(allall(A));
            Assert.IsTrue(anyall(A));

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsFalse(allall(A));
            Assert.IsFalse(anyall(A));

        }
       

        [TestMethod]
        public void NPScalar_allall_anyall_fcomplex() {

            Array<fcomplex> A = new fcomplex(1f, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(allall(A));
            Assert.IsTrue(anyall(A));

            A = new fcomplex(0f, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsFalse(allall(A));
            Assert.IsFalse(anyall(A));

        }
       

        [TestMethod]
        public void NPScalar_allall_anyall_complex() {

            Array<complex> A = new complex(1.0, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(allall(A));
            Assert.IsTrue(anyall(A));

            A = new complex(0.0, 0.0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsFalse(allall(A));
            Assert.IsFalse(anyall(A));

        }
       

        [TestMethod]
        public void NPScalar_allall_anyall_float() {

            Array<float> A = 1f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(allall(A));
            Assert.IsTrue(anyall(A));

            A = 0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsFalse(allall(A));
            Assert.IsFalse(anyall(A));

        }

#endregion HYCALPER AUTO GENERATED CODE

        [TestMethod]
        public void NPScalar_allall_anyall_bool() {

            Logical A = true;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(allall(A));
            Assert.IsTrue(anyall(A));

            A = false;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsFalse(allall(A));
            Assert.IsFalse(anyall(A));

        }

        #region HYCALPER LOOPSTART Sumall_Prodall
        /*!HC:TYPELIST:
<hycalper>
<type>
<source locate="here">
double
</source>
<destination>float</destination>
<destination>complex</destination>
<destination>fcomplex</destination>
<destination>sbyte</destination>
<destination>byte</destination>
<destination>short</destination>
<destination>ushort</destination>
<destination>int</destination>
<destination>uint</destination>
<destination>long</destination>
<destination>ulong</destination>
</type>
<type>
<source locate="here">
1.0
</source>
<destination>1f</destination>
<destination>new complex(1.0, 0)</destination>
<destination>new fcomplex(1f, 0)</destination>
<destination>1</destination>
<destination>1</destination>
<destination>1</destination>
<destination>1</destination>
<destination>1</destination>
<destination>1</destination>
<destination>1</destination>
<destination>1</destination>
</type>
<type>
<source locate="here">
0.0
</source>
<destination>0f</destination>
<destination>new complex(0.0, 0.0)</destination>
<destination>new fcomplex(0f, 0)</destination>
<destination>0</destination>
<destination>0</destination>
<destination>0</destination>
<destination>0</destination>
<destination>0</destination>
<destination>0</destination>
<destination>0</destination>
<destination>0</destination>
</type>
</hycalper>
*/

        [TestMethod]
        public void NPScalar_sumall_prodall_double() {

            Array<double> A = 1.0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sumall(A) == 1.0);
            Assert.IsTrue(prodall(A) == 1.0);

            A = 0.0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sumall(A) == 0.0);
            Assert.IsTrue(prodall(A)== 0.0);

        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        [TestMethod]
        public void NPScalar_sumall_prodall_ulong() {

            Array<ulong> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sumall(A) == 1);
            Assert.IsTrue(prodall(A) == 1);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sumall(A) == 0);
            Assert.IsTrue(prodall(A)== 0);

        }
       

        [TestMethod]
        public void NPScalar_sumall_prodall_long() {

            Array<long> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sumall(A) == 1);
            Assert.IsTrue(prodall(A) == 1);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sumall(A) == 0);
            Assert.IsTrue(prodall(A)== 0);

        }
       

        [TestMethod]
        public void NPScalar_sumall_prodall_uint() {

            Array<uint> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sumall(A) == 1);
            Assert.IsTrue(prodall(A) == 1);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sumall(A) == 0);
            Assert.IsTrue(prodall(A)== 0);

        }
       

        [TestMethod]
        public void NPScalar_sumall_prodall_int() {

            Array<int> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sumall(A) == 1);
            Assert.IsTrue(prodall(A) == 1);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sumall(A) == 0);
            Assert.IsTrue(prodall(A)== 0);

        }
       

        [TestMethod]
        public void NPScalar_sumall_prodall_ushort() {

            Array<ushort> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sumall(A) == 1);
            Assert.IsTrue(prodall(A) == 1);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sumall(A) == 0);
            Assert.IsTrue(prodall(A)== 0);

        }
       

        [TestMethod]
        public void NPScalar_sumall_prodall_short() {

            Array<short> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sumall(A) == 1);
            Assert.IsTrue(prodall(A) == 1);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sumall(A) == 0);
            Assert.IsTrue(prodall(A)== 0);

        }
       

        [TestMethod]
        public void NPScalar_sumall_prodall_byte() {

            Array<byte> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sumall(A) == 1);
            Assert.IsTrue(prodall(A) == 1);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sumall(A) == 0);
            Assert.IsTrue(prodall(A)== 0);

        }
       

        [TestMethod]
        public void NPScalar_sumall_prodall_sbyte() {

            Array<sbyte> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sumall(A) == 1);
            Assert.IsTrue(prodall(A) == 1);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sumall(A) == 0);
            Assert.IsTrue(prodall(A)== 0);

        }
       

        [TestMethod]
        public void NPScalar_sumall_prodall_fcomplex() {

            Array<fcomplex> A = new fcomplex(1f, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sumall(A) == new fcomplex(1f, 0));
            Assert.IsTrue(prodall(A) == new fcomplex(1f, 0));

            A = new fcomplex(0f, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sumall(A) == new fcomplex(0f, 0));
            Assert.IsTrue(prodall(A)== new fcomplex(0f, 0));

        }
       

        [TestMethod]
        public void NPScalar_sumall_prodall_complex() {

            Array<complex> A = new complex(1.0, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sumall(A) == new complex(1.0, 0));
            Assert.IsTrue(prodall(A) == new complex(1.0, 0));

            A = new complex(0.0, 0.0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sumall(A) == new complex(0.0, 0.0));
            Assert.IsTrue(prodall(A)== new complex(0.0, 0.0));

        }
       

        [TestMethod]
        public void NPScalar_sumall_prodall_float() {

            Array<float> A = 1f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sumall(A) == 1f);
            Assert.IsTrue(prodall(A) == 1f);

            A = 0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(sumall(A) == 0f);
            Assert.IsTrue(prodall(A)== 0f);

        }

#endregion HYCALPER AUTO GENERATED CODE

    }
}
