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
    public class UnaryAccumOps_NPScalarTests : NumpyTestClass {

        #region HYCALPER LOOPSTART all_any_with_Bool
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
<destination>bool</destination>
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
<destination>true</destination>
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
<destination>false</destination>
</type>
<type>
<source locate="here">
<![CDATA[Array<bool>]]>
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
<destination>Logical</destination>
</type>
</hycalper>
*/

        [TestMethod]
        public void NPScalar_all_any_double() {

            Array<double> A = 1.0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A));
            Assert.IsTrue(all(A, 0));
            Assert.IsTrue(all(A, 1));
            Assert.IsTrue(all(A, 2));
            Assert.IsTrue(any(A, 0));
            Assert.IsTrue(any(A, 1));
            Assert.IsTrue(any(A, 2));

            Assert.IsTrue(all(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 2).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 2).S.NumberOfDimensions == 0);

            A = 0.0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsFalse(all(A));
            Assert.IsFalse(all(A, 0));
            Assert.IsFalse(all(A, 1));
            Assert.IsFalse(all(A, 2));
            Assert.IsFalse(any(A, 0));
            Assert.IsFalse(any(A, 1));
            Assert.IsFalse(any(A, 2));

            Assert.IsTrue(all(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 2).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 2).S.NumberOfDimensions == 0);


        }

        [TestMethod]
        public void NPScalar_flip_double() {

            Array<double> A = 1.0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(flip(A) == 1.0);
            Assert.IsTrue(flip(A, 0) == 1.0);
            Assert.IsTrue(flip(A, 1) == 1.0);
            Assert.IsTrue(flip(A, 2) == 1.0);

            A = 0.0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(flip(A) == 0.0);
            Assert.IsTrue(flip(A, 0) == 0.0);
            Assert.IsTrue(flip(A, 1) == 0.0);
            Assert.IsTrue(flip(A, 2) == 0.0);

        }

        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        [TestMethod]
        public void NPScalar_all_any_bool() {

            Logical A = true;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A));
            Assert.IsTrue(all(A, 0));
            Assert.IsTrue(all(A, 1));
            Assert.IsTrue(all(A, 2));
            Assert.IsTrue(any(A, 0));
            Assert.IsTrue(any(A, 1));
            Assert.IsTrue(any(A, 2));

            Assert.IsTrue(all(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 2).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 2).S.NumberOfDimensions == 0);

            A = false;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsFalse(all(A));
            Assert.IsFalse(all(A, 0));
            Assert.IsFalse(all(A, 1));
            Assert.IsFalse(all(A, 2));
            Assert.IsFalse(any(A, 0));
            Assert.IsFalse(any(A, 1));
            Assert.IsFalse(any(A, 2));

            Assert.IsTrue(all(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 2).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 2).S.NumberOfDimensions == 0);


        }

        [TestMethod]
        public void NPScalar_flip_bool() {

            Logical A = true;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(flip(A) == true);
            Assert.IsTrue(flip(A, 0) == true);
            Assert.IsTrue(flip(A, 1) == true);
            Assert.IsTrue(flip(A, 2) == true);

            A = false;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(flip(A) == false);
            Assert.IsTrue(flip(A, 0) == false);
            Assert.IsTrue(flip(A, 1) == false);
            Assert.IsTrue(flip(A, 2) == false);

        }

       

        [TestMethod]
        public void NPScalar_all_any_ulong() {

            Array<ulong> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A));
            Assert.IsTrue(all(A, 0));
            Assert.IsTrue(all(A, 1));
            Assert.IsTrue(all(A, 2));
            Assert.IsTrue(any(A, 0));
            Assert.IsTrue(any(A, 1));
            Assert.IsTrue(any(A, 2));

            Assert.IsTrue(all(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 2).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 2).S.NumberOfDimensions == 0);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsFalse(all(A));
            Assert.IsFalse(all(A, 0));
            Assert.IsFalse(all(A, 1));
            Assert.IsFalse(all(A, 2));
            Assert.IsFalse(any(A, 0));
            Assert.IsFalse(any(A, 1));
            Assert.IsFalse(any(A, 2));

            Assert.IsTrue(all(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 2).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 2).S.NumberOfDimensions == 0);


        }

        [TestMethod]
        public void NPScalar_flip_ulong() {

            Array<ulong> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(flip(A) == 1);
            Assert.IsTrue(flip(A, 0) == 1);
            Assert.IsTrue(flip(A, 1) == 1);
            Assert.IsTrue(flip(A, 2) == 1);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(flip(A) == 0);
            Assert.IsTrue(flip(A, 0) == 0);
            Assert.IsTrue(flip(A, 1) == 0);
            Assert.IsTrue(flip(A, 2) == 0);

        }

       

        [TestMethod]
        public void NPScalar_all_any_long() {

            Array<long> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A));
            Assert.IsTrue(all(A, 0));
            Assert.IsTrue(all(A, 1));
            Assert.IsTrue(all(A, 2));
            Assert.IsTrue(any(A, 0));
            Assert.IsTrue(any(A, 1));
            Assert.IsTrue(any(A, 2));

            Assert.IsTrue(all(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 2).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 2).S.NumberOfDimensions == 0);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsFalse(all(A));
            Assert.IsFalse(all(A, 0));
            Assert.IsFalse(all(A, 1));
            Assert.IsFalse(all(A, 2));
            Assert.IsFalse(any(A, 0));
            Assert.IsFalse(any(A, 1));
            Assert.IsFalse(any(A, 2));

            Assert.IsTrue(all(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 2).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 2).S.NumberOfDimensions == 0);


        }

        [TestMethod]
        public void NPScalar_flip_long() {

            Array<long> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(flip(A) == 1);
            Assert.IsTrue(flip(A, 0) == 1);
            Assert.IsTrue(flip(A, 1) == 1);
            Assert.IsTrue(flip(A, 2) == 1);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(flip(A) == 0);
            Assert.IsTrue(flip(A, 0) == 0);
            Assert.IsTrue(flip(A, 1) == 0);
            Assert.IsTrue(flip(A, 2) == 0);

        }

       

        [TestMethod]
        public void NPScalar_all_any_uint() {

            Array<uint> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A));
            Assert.IsTrue(all(A, 0));
            Assert.IsTrue(all(A, 1));
            Assert.IsTrue(all(A, 2));
            Assert.IsTrue(any(A, 0));
            Assert.IsTrue(any(A, 1));
            Assert.IsTrue(any(A, 2));

            Assert.IsTrue(all(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 2).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 2).S.NumberOfDimensions == 0);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsFalse(all(A));
            Assert.IsFalse(all(A, 0));
            Assert.IsFalse(all(A, 1));
            Assert.IsFalse(all(A, 2));
            Assert.IsFalse(any(A, 0));
            Assert.IsFalse(any(A, 1));
            Assert.IsFalse(any(A, 2));

            Assert.IsTrue(all(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 2).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 2).S.NumberOfDimensions == 0);


        }

        [TestMethod]
        public void NPScalar_flip_uint() {

            Array<uint> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(flip(A) == 1);
            Assert.IsTrue(flip(A, 0) == 1);
            Assert.IsTrue(flip(A, 1) == 1);
            Assert.IsTrue(flip(A, 2) == 1);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(flip(A) == 0);
            Assert.IsTrue(flip(A, 0) == 0);
            Assert.IsTrue(flip(A, 1) == 0);
            Assert.IsTrue(flip(A, 2) == 0);

        }

       

        [TestMethod]
        public void NPScalar_all_any_int() {

            Array<int> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A));
            Assert.IsTrue(all(A, 0));
            Assert.IsTrue(all(A, 1));
            Assert.IsTrue(all(A, 2));
            Assert.IsTrue(any(A, 0));
            Assert.IsTrue(any(A, 1));
            Assert.IsTrue(any(A, 2));

            Assert.IsTrue(all(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 2).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 2).S.NumberOfDimensions == 0);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsFalse(all(A));
            Assert.IsFalse(all(A, 0));
            Assert.IsFalse(all(A, 1));
            Assert.IsFalse(all(A, 2));
            Assert.IsFalse(any(A, 0));
            Assert.IsFalse(any(A, 1));
            Assert.IsFalse(any(A, 2));

            Assert.IsTrue(all(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 2).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 2).S.NumberOfDimensions == 0);


        }

        [TestMethod]
        public void NPScalar_flip_int() {

            Array<int> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(flip(A) == 1);
            Assert.IsTrue(flip(A, 0) == 1);
            Assert.IsTrue(flip(A, 1) == 1);
            Assert.IsTrue(flip(A, 2) == 1);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(flip(A) == 0);
            Assert.IsTrue(flip(A, 0) == 0);
            Assert.IsTrue(flip(A, 1) == 0);
            Assert.IsTrue(flip(A, 2) == 0);

        }

       

        [TestMethod]
        public void NPScalar_all_any_ushort() {

            Array<ushort> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A));
            Assert.IsTrue(all(A, 0));
            Assert.IsTrue(all(A, 1));
            Assert.IsTrue(all(A, 2));
            Assert.IsTrue(any(A, 0));
            Assert.IsTrue(any(A, 1));
            Assert.IsTrue(any(A, 2));

            Assert.IsTrue(all(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 2).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 2).S.NumberOfDimensions == 0);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsFalse(all(A));
            Assert.IsFalse(all(A, 0));
            Assert.IsFalse(all(A, 1));
            Assert.IsFalse(all(A, 2));
            Assert.IsFalse(any(A, 0));
            Assert.IsFalse(any(A, 1));
            Assert.IsFalse(any(A, 2));

            Assert.IsTrue(all(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 2).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 2).S.NumberOfDimensions == 0);


        }

        [TestMethod]
        public void NPScalar_flip_ushort() {

            Array<ushort> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(flip(A) == 1);
            Assert.IsTrue(flip(A, 0) == 1);
            Assert.IsTrue(flip(A, 1) == 1);
            Assert.IsTrue(flip(A, 2) == 1);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(flip(A) == 0);
            Assert.IsTrue(flip(A, 0) == 0);
            Assert.IsTrue(flip(A, 1) == 0);
            Assert.IsTrue(flip(A, 2) == 0);

        }

       

        [TestMethod]
        public void NPScalar_all_any_short() {

            Array<short> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A));
            Assert.IsTrue(all(A, 0));
            Assert.IsTrue(all(A, 1));
            Assert.IsTrue(all(A, 2));
            Assert.IsTrue(any(A, 0));
            Assert.IsTrue(any(A, 1));
            Assert.IsTrue(any(A, 2));

            Assert.IsTrue(all(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 2).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 2).S.NumberOfDimensions == 0);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsFalse(all(A));
            Assert.IsFalse(all(A, 0));
            Assert.IsFalse(all(A, 1));
            Assert.IsFalse(all(A, 2));
            Assert.IsFalse(any(A, 0));
            Assert.IsFalse(any(A, 1));
            Assert.IsFalse(any(A, 2));

            Assert.IsTrue(all(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 2).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 2).S.NumberOfDimensions == 0);


        }

        [TestMethod]
        public void NPScalar_flip_short() {

            Array<short> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(flip(A) == 1);
            Assert.IsTrue(flip(A, 0) == 1);
            Assert.IsTrue(flip(A, 1) == 1);
            Assert.IsTrue(flip(A, 2) == 1);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(flip(A) == 0);
            Assert.IsTrue(flip(A, 0) == 0);
            Assert.IsTrue(flip(A, 1) == 0);
            Assert.IsTrue(flip(A, 2) == 0);

        }

       

        [TestMethod]
        public void NPScalar_all_any_byte() {

            Array<byte> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A));
            Assert.IsTrue(all(A, 0));
            Assert.IsTrue(all(A, 1));
            Assert.IsTrue(all(A, 2));
            Assert.IsTrue(any(A, 0));
            Assert.IsTrue(any(A, 1));
            Assert.IsTrue(any(A, 2));

            Assert.IsTrue(all(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 2).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 2).S.NumberOfDimensions == 0);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsFalse(all(A));
            Assert.IsFalse(all(A, 0));
            Assert.IsFalse(all(A, 1));
            Assert.IsFalse(all(A, 2));
            Assert.IsFalse(any(A, 0));
            Assert.IsFalse(any(A, 1));
            Assert.IsFalse(any(A, 2));

            Assert.IsTrue(all(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 2).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 2).S.NumberOfDimensions == 0);


        }

        [TestMethod]
        public void NPScalar_flip_byte() {

            Array<byte> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(flip(A) == 1);
            Assert.IsTrue(flip(A, 0) == 1);
            Assert.IsTrue(flip(A, 1) == 1);
            Assert.IsTrue(flip(A, 2) == 1);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(flip(A) == 0);
            Assert.IsTrue(flip(A, 0) == 0);
            Assert.IsTrue(flip(A, 1) == 0);
            Assert.IsTrue(flip(A, 2) == 0);

        }

       

        [TestMethod]
        public void NPScalar_all_any_sbyte() {

            Array<sbyte> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A));
            Assert.IsTrue(all(A, 0));
            Assert.IsTrue(all(A, 1));
            Assert.IsTrue(all(A, 2));
            Assert.IsTrue(any(A, 0));
            Assert.IsTrue(any(A, 1));
            Assert.IsTrue(any(A, 2));

            Assert.IsTrue(all(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 2).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 2).S.NumberOfDimensions == 0);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsFalse(all(A));
            Assert.IsFalse(all(A, 0));
            Assert.IsFalse(all(A, 1));
            Assert.IsFalse(all(A, 2));
            Assert.IsFalse(any(A, 0));
            Assert.IsFalse(any(A, 1));
            Assert.IsFalse(any(A, 2));

            Assert.IsTrue(all(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 2).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 2).S.NumberOfDimensions == 0);


        }

        [TestMethod]
        public void NPScalar_flip_sbyte() {

            Array<sbyte> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(flip(A) == 1);
            Assert.IsTrue(flip(A, 0) == 1);
            Assert.IsTrue(flip(A, 1) == 1);
            Assert.IsTrue(flip(A, 2) == 1);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(flip(A) == 0);
            Assert.IsTrue(flip(A, 0) == 0);
            Assert.IsTrue(flip(A, 1) == 0);
            Assert.IsTrue(flip(A, 2) == 0);

        }

       

        [TestMethod]
        public void NPScalar_all_any_fcomplex() {

            Array<fcomplex> A = new fcomplex(1f, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A));
            Assert.IsTrue(all(A, 0));
            Assert.IsTrue(all(A, 1));
            Assert.IsTrue(all(A, 2));
            Assert.IsTrue(any(A, 0));
            Assert.IsTrue(any(A, 1));
            Assert.IsTrue(any(A, 2));

            Assert.IsTrue(all(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 2).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 2).S.NumberOfDimensions == 0);

            A = new fcomplex(0f, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsFalse(all(A));
            Assert.IsFalse(all(A, 0));
            Assert.IsFalse(all(A, 1));
            Assert.IsFalse(all(A, 2));
            Assert.IsFalse(any(A, 0));
            Assert.IsFalse(any(A, 1));
            Assert.IsFalse(any(A, 2));

            Assert.IsTrue(all(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 2).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 2).S.NumberOfDimensions == 0);


        }

        [TestMethod]
        public void NPScalar_flip_fcomplex() {

            Array<fcomplex> A = new fcomplex(1f, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(flip(A) == new fcomplex(1f, 0));
            Assert.IsTrue(flip(A, 0) == new fcomplex(1f, 0));
            Assert.IsTrue(flip(A, 1) == new fcomplex(1f, 0));
            Assert.IsTrue(flip(A, 2) == new fcomplex(1f, 0));

            A = new fcomplex(0f, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(flip(A) == new fcomplex(0f, 0));
            Assert.IsTrue(flip(A, 0) == new fcomplex(0f, 0));
            Assert.IsTrue(flip(A, 1) == new fcomplex(0f, 0));
            Assert.IsTrue(flip(A, 2) == new fcomplex(0f, 0));

        }

       

        [TestMethod]
        public void NPScalar_all_any_complex() {

            Array<complex> A = new complex(1.0, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A));
            Assert.IsTrue(all(A, 0));
            Assert.IsTrue(all(A, 1));
            Assert.IsTrue(all(A, 2));
            Assert.IsTrue(any(A, 0));
            Assert.IsTrue(any(A, 1));
            Assert.IsTrue(any(A, 2));

            Assert.IsTrue(all(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 2).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 2).S.NumberOfDimensions == 0);

            A = new complex(0.0, 0.0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsFalse(all(A));
            Assert.IsFalse(all(A, 0));
            Assert.IsFalse(all(A, 1));
            Assert.IsFalse(all(A, 2));
            Assert.IsFalse(any(A, 0));
            Assert.IsFalse(any(A, 1));
            Assert.IsFalse(any(A, 2));

            Assert.IsTrue(all(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 2).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 2).S.NumberOfDimensions == 0);


        }

        [TestMethod]
        public void NPScalar_flip_complex() {

            Array<complex> A = new complex(1.0, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(flip(A) == new complex(1.0, 0));
            Assert.IsTrue(flip(A, 0) == new complex(1.0, 0));
            Assert.IsTrue(flip(A, 1) == new complex(1.0, 0));
            Assert.IsTrue(flip(A, 2) == new complex(1.0, 0));

            A = new complex(0.0, 0.0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(flip(A) == new complex(0.0, 0.0));
            Assert.IsTrue(flip(A, 0) == new complex(0.0, 0.0));
            Assert.IsTrue(flip(A, 1) == new complex(0.0, 0.0));
            Assert.IsTrue(flip(A, 2) == new complex(0.0, 0.0));

        }

       

        [TestMethod]
        public void NPScalar_all_any_float() {

            Array<float> A = 1f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A));
            Assert.IsTrue(all(A, 0));
            Assert.IsTrue(all(A, 1));
            Assert.IsTrue(all(A, 2));
            Assert.IsTrue(any(A, 0));
            Assert.IsTrue(any(A, 1));
            Assert.IsTrue(any(A, 2));

            Assert.IsTrue(all(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 2).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 2).S.NumberOfDimensions == 0);

            A = 0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsFalse(all(A));
            Assert.IsFalse(all(A, 0));
            Assert.IsFalse(all(A, 1));
            Assert.IsFalse(all(A, 2));
            Assert.IsFalse(any(A, 0));
            Assert.IsFalse(any(A, 1));
            Assert.IsFalse(any(A, 2));

            Assert.IsTrue(all(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(all(A, 2).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 0).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 1).S.NumberOfDimensions == 0);
            Assert.IsTrue(any(A, 2).S.NumberOfDimensions == 0);


        }

        [TestMethod]
        public void NPScalar_flip_float() {

            Array<float> A = 1f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(flip(A) == 1f);
            Assert.IsTrue(flip(A, 0) == 1f);
            Assert.IsTrue(flip(A, 1) == 1f);
            Assert.IsTrue(flip(A, 2) == 1f);

            A = 0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(flip(A) == 0f);
            Assert.IsTrue(flip(A, 0) == 0f);
            Assert.IsTrue(flip(A, 1) == 0f);
            Assert.IsTrue(flip(A, 2) == 0f);

        }


#endregion HYCALPER AUTO GENERATED CODE

        #region HYCALPER LOOPSTART cum-/prod_cum-/sum,sort
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
        public void NPScalar_cum_prod_cum_sum_sort_double() {

            Array<double> A = 1.0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cumprod(A) == 1.0);
            Assert.IsTrue(cumsum(A) == 1.0);
            Assert.IsTrue(prod(A) == 1.0);
            Assert.IsTrue(sum(A) == 1.0);
            Assert.IsTrue(sort(A) == 1.0);

            Assert.IsTrue(cumprod(A, 0) == 1.0);
            Assert.IsTrue(cumsum(A, 0) == 1.0);
            Assert.IsTrue(prod(A, 0) == 1.0);
            Assert.IsTrue(sum(A, 0) == 1.0);
            Assert.IsTrue(sort(A, 0) == 1.0);

            Assert.IsTrue(cumprod(A, 1) == 1.0);
            Assert.IsTrue(cumsum(A, 1) == 1.0);
            Assert.IsTrue(prod(A, 1) == 1.0);
            Assert.IsTrue(sum(A, 1) == 1.0);
            Assert.IsTrue(sort(A, 1) == 1.0);

            Assert.IsTrue(cumprod(A, 2) == 1.0);
            Assert.IsTrue(cumsum(A, 2) == 1.0);
            Assert.IsTrue(prod(A, 2) == 1.0);
            Assert.IsTrue(sum(A, 2) == 1.0);
            Assert.IsTrue(sort(A, 2) == 1.0);

            A = 0.0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cumprod(A) == 0.0);
            Assert.IsTrue(cumsum(A) == 0.0);
            Assert.IsTrue(prod(A) == 0.0);
            Assert.IsTrue(sort(A) == 0.0);

            Assert.IsTrue(cumprod(A, 0) == 0.0);
            Assert.IsTrue(cumsum(A, 0) == 0.0);
            Assert.IsTrue(prod(A, 0) == 0.0);
            Assert.IsTrue(sort(A, 0) == 0.0);

            Assert.IsTrue(cumprod(A, 1) == 0.0);
            Assert.IsTrue(cumsum(A, 1) == 0.0);
            Assert.IsTrue(prod(A, 1) == 0.0);
            Assert.IsTrue(sort(A, 1) == 0.0);

            Assert.IsTrue(cumprod(A, 2) == 0.0);
            Assert.IsTrue(cumsum(A, 2) == 0.0);
            Assert.IsTrue(prod(A, 2) == 0.0);
            Assert.IsTrue(sort(A, 2) == 0.0);

        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        [TestMethod]
        public void NPScalar_cum_prod_cum_sum_sort_ulong() {

            Array<ulong> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cumprod(A) == 1);
            Assert.IsTrue(cumsum(A) == 1);
            Assert.IsTrue(prod(A) == 1);
            Assert.IsTrue(sum(A) == 1);
            Assert.IsTrue(sort(A) == 1);

            Assert.IsTrue(cumprod(A, 0) == 1);
            Assert.IsTrue(cumsum(A, 0) == 1);
            Assert.IsTrue(prod(A, 0) == 1);
            Assert.IsTrue(sum(A, 0) == 1);
            Assert.IsTrue(sort(A, 0) == 1);

            Assert.IsTrue(cumprod(A, 1) == 1);
            Assert.IsTrue(cumsum(A, 1) == 1);
            Assert.IsTrue(prod(A, 1) == 1);
            Assert.IsTrue(sum(A, 1) == 1);
            Assert.IsTrue(sort(A, 1) == 1);

            Assert.IsTrue(cumprod(A, 2) == 1);
            Assert.IsTrue(cumsum(A, 2) == 1);
            Assert.IsTrue(prod(A, 2) == 1);
            Assert.IsTrue(sum(A, 2) == 1);
            Assert.IsTrue(sort(A, 2) == 1);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cumprod(A) == 0);
            Assert.IsTrue(cumsum(A) == 0);
            Assert.IsTrue(prod(A) == 0);
            Assert.IsTrue(sort(A) == 0);

            Assert.IsTrue(cumprod(A, 0) == 0);
            Assert.IsTrue(cumsum(A, 0) == 0);
            Assert.IsTrue(prod(A, 0) == 0);
            Assert.IsTrue(sort(A, 0) == 0);

            Assert.IsTrue(cumprod(A, 1) == 0);
            Assert.IsTrue(cumsum(A, 1) == 0);
            Assert.IsTrue(prod(A, 1) == 0);
            Assert.IsTrue(sort(A, 1) == 0);

            Assert.IsTrue(cumprod(A, 2) == 0);
            Assert.IsTrue(cumsum(A, 2) == 0);
            Assert.IsTrue(prod(A, 2) == 0);
            Assert.IsTrue(sort(A, 2) == 0);

        }
       

        [TestMethod]
        public void NPScalar_cum_prod_cum_sum_sort_long() {

            Array<long> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cumprod(A) == 1);
            Assert.IsTrue(cumsum(A) == 1);
            Assert.IsTrue(prod(A) == 1);
            Assert.IsTrue(sum(A) == 1);
            Assert.IsTrue(sort(A) == 1);

            Assert.IsTrue(cumprod(A, 0) == 1);
            Assert.IsTrue(cumsum(A, 0) == 1);
            Assert.IsTrue(prod(A, 0) == 1);
            Assert.IsTrue(sum(A, 0) == 1);
            Assert.IsTrue(sort(A, 0) == 1);

            Assert.IsTrue(cumprod(A, 1) == 1);
            Assert.IsTrue(cumsum(A, 1) == 1);
            Assert.IsTrue(prod(A, 1) == 1);
            Assert.IsTrue(sum(A, 1) == 1);
            Assert.IsTrue(sort(A, 1) == 1);

            Assert.IsTrue(cumprod(A, 2) == 1);
            Assert.IsTrue(cumsum(A, 2) == 1);
            Assert.IsTrue(prod(A, 2) == 1);
            Assert.IsTrue(sum(A, 2) == 1);
            Assert.IsTrue(sort(A, 2) == 1);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cumprod(A) == 0);
            Assert.IsTrue(cumsum(A) == 0);
            Assert.IsTrue(prod(A) == 0);
            Assert.IsTrue(sort(A) == 0);

            Assert.IsTrue(cumprod(A, 0) == 0);
            Assert.IsTrue(cumsum(A, 0) == 0);
            Assert.IsTrue(prod(A, 0) == 0);
            Assert.IsTrue(sort(A, 0) == 0);

            Assert.IsTrue(cumprod(A, 1) == 0);
            Assert.IsTrue(cumsum(A, 1) == 0);
            Assert.IsTrue(prod(A, 1) == 0);
            Assert.IsTrue(sort(A, 1) == 0);

            Assert.IsTrue(cumprod(A, 2) == 0);
            Assert.IsTrue(cumsum(A, 2) == 0);
            Assert.IsTrue(prod(A, 2) == 0);
            Assert.IsTrue(sort(A, 2) == 0);

        }
       

        [TestMethod]
        public void NPScalar_cum_prod_cum_sum_sort_uint() {

            Array<uint> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cumprod(A) == 1);
            Assert.IsTrue(cumsum(A) == 1);
            Assert.IsTrue(prod(A) == 1);
            Assert.IsTrue(sum(A) == 1);
            Assert.IsTrue(sort(A) == 1);

            Assert.IsTrue(cumprod(A, 0) == 1);
            Assert.IsTrue(cumsum(A, 0) == 1);
            Assert.IsTrue(prod(A, 0) == 1);
            Assert.IsTrue(sum(A, 0) == 1);
            Assert.IsTrue(sort(A, 0) == 1);

            Assert.IsTrue(cumprod(A, 1) == 1);
            Assert.IsTrue(cumsum(A, 1) == 1);
            Assert.IsTrue(prod(A, 1) == 1);
            Assert.IsTrue(sum(A, 1) == 1);
            Assert.IsTrue(sort(A, 1) == 1);

            Assert.IsTrue(cumprod(A, 2) == 1);
            Assert.IsTrue(cumsum(A, 2) == 1);
            Assert.IsTrue(prod(A, 2) == 1);
            Assert.IsTrue(sum(A, 2) == 1);
            Assert.IsTrue(sort(A, 2) == 1);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cumprod(A) == 0);
            Assert.IsTrue(cumsum(A) == 0);
            Assert.IsTrue(prod(A) == 0);
            Assert.IsTrue(sort(A) == 0);

            Assert.IsTrue(cumprod(A, 0) == 0);
            Assert.IsTrue(cumsum(A, 0) == 0);
            Assert.IsTrue(prod(A, 0) == 0);
            Assert.IsTrue(sort(A, 0) == 0);

            Assert.IsTrue(cumprod(A, 1) == 0);
            Assert.IsTrue(cumsum(A, 1) == 0);
            Assert.IsTrue(prod(A, 1) == 0);
            Assert.IsTrue(sort(A, 1) == 0);

            Assert.IsTrue(cumprod(A, 2) == 0);
            Assert.IsTrue(cumsum(A, 2) == 0);
            Assert.IsTrue(prod(A, 2) == 0);
            Assert.IsTrue(sort(A, 2) == 0);

        }
       

        [TestMethod]
        public void NPScalar_cum_prod_cum_sum_sort_int() {

            Array<int> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cumprod(A) == 1);
            Assert.IsTrue(cumsum(A) == 1);
            Assert.IsTrue(prod(A) == 1);
            Assert.IsTrue(sum(A) == 1);
            Assert.IsTrue(sort(A) == 1);

            Assert.IsTrue(cumprod(A, 0) == 1);
            Assert.IsTrue(cumsum(A, 0) == 1);
            Assert.IsTrue(prod(A, 0) == 1);
            Assert.IsTrue(sum(A, 0) == 1);
            Assert.IsTrue(sort(A, 0) == 1);

            Assert.IsTrue(cumprod(A, 1) == 1);
            Assert.IsTrue(cumsum(A, 1) == 1);
            Assert.IsTrue(prod(A, 1) == 1);
            Assert.IsTrue(sum(A, 1) == 1);
            Assert.IsTrue(sort(A, 1) == 1);

            Assert.IsTrue(cumprod(A, 2) == 1);
            Assert.IsTrue(cumsum(A, 2) == 1);
            Assert.IsTrue(prod(A, 2) == 1);
            Assert.IsTrue(sum(A, 2) == 1);
            Assert.IsTrue(sort(A, 2) == 1);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cumprod(A) == 0);
            Assert.IsTrue(cumsum(A) == 0);
            Assert.IsTrue(prod(A) == 0);
            Assert.IsTrue(sort(A) == 0);

            Assert.IsTrue(cumprod(A, 0) == 0);
            Assert.IsTrue(cumsum(A, 0) == 0);
            Assert.IsTrue(prod(A, 0) == 0);
            Assert.IsTrue(sort(A, 0) == 0);

            Assert.IsTrue(cumprod(A, 1) == 0);
            Assert.IsTrue(cumsum(A, 1) == 0);
            Assert.IsTrue(prod(A, 1) == 0);
            Assert.IsTrue(sort(A, 1) == 0);

            Assert.IsTrue(cumprod(A, 2) == 0);
            Assert.IsTrue(cumsum(A, 2) == 0);
            Assert.IsTrue(prod(A, 2) == 0);
            Assert.IsTrue(sort(A, 2) == 0);

        }
       

        [TestMethod]
        public void NPScalar_cum_prod_cum_sum_sort_ushort() {

            Array<ushort> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cumprod(A) == 1);
            Assert.IsTrue(cumsum(A) == 1);
            Assert.IsTrue(prod(A) == 1);
            Assert.IsTrue(sum(A) == 1);
            Assert.IsTrue(sort(A) == 1);

            Assert.IsTrue(cumprod(A, 0) == 1);
            Assert.IsTrue(cumsum(A, 0) == 1);
            Assert.IsTrue(prod(A, 0) == 1);
            Assert.IsTrue(sum(A, 0) == 1);
            Assert.IsTrue(sort(A, 0) == 1);

            Assert.IsTrue(cumprod(A, 1) == 1);
            Assert.IsTrue(cumsum(A, 1) == 1);
            Assert.IsTrue(prod(A, 1) == 1);
            Assert.IsTrue(sum(A, 1) == 1);
            Assert.IsTrue(sort(A, 1) == 1);

            Assert.IsTrue(cumprod(A, 2) == 1);
            Assert.IsTrue(cumsum(A, 2) == 1);
            Assert.IsTrue(prod(A, 2) == 1);
            Assert.IsTrue(sum(A, 2) == 1);
            Assert.IsTrue(sort(A, 2) == 1);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cumprod(A) == 0);
            Assert.IsTrue(cumsum(A) == 0);
            Assert.IsTrue(prod(A) == 0);
            Assert.IsTrue(sort(A) == 0);

            Assert.IsTrue(cumprod(A, 0) == 0);
            Assert.IsTrue(cumsum(A, 0) == 0);
            Assert.IsTrue(prod(A, 0) == 0);
            Assert.IsTrue(sort(A, 0) == 0);

            Assert.IsTrue(cumprod(A, 1) == 0);
            Assert.IsTrue(cumsum(A, 1) == 0);
            Assert.IsTrue(prod(A, 1) == 0);
            Assert.IsTrue(sort(A, 1) == 0);

            Assert.IsTrue(cumprod(A, 2) == 0);
            Assert.IsTrue(cumsum(A, 2) == 0);
            Assert.IsTrue(prod(A, 2) == 0);
            Assert.IsTrue(sort(A, 2) == 0);

        }
       

        [TestMethod]
        public void NPScalar_cum_prod_cum_sum_sort_short() {

            Array<short> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cumprod(A) == 1);
            Assert.IsTrue(cumsum(A) == 1);
            Assert.IsTrue(prod(A) == 1);
            Assert.IsTrue(sum(A) == 1);
            Assert.IsTrue(sort(A) == 1);

            Assert.IsTrue(cumprod(A, 0) == 1);
            Assert.IsTrue(cumsum(A, 0) == 1);
            Assert.IsTrue(prod(A, 0) == 1);
            Assert.IsTrue(sum(A, 0) == 1);
            Assert.IsTrue(sort(A, 0) == 1);

            Assert.IsTrue(cumprod(A, 1) == 1);
            Assert.IsTrue(cumsum(A, 1) == 1);
            Assert.IsTrue(prod(A, 1) == 1);
            Assert.IsTrue(sum(A, 1) == 1);
            Assert.IsTrue(sort(A, 1) == 1);

            Assert.IsTrue(cumprod(A, 2) == 1);
            Assert.IsTrue(cumsum(A, 2) == 1);
            Assert.IsTrue(prod(A, 2) == 1);
            Assert.IsTrue(sum(A, 2) == 1);
            Assert.IsTrue(sort(A, 2) == 1);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cumprod(A) == 0);
            Assert.IsTrue(cumsum(A) == 0);
            Assert.IsTrue(prod(A) == 0);
            Assert.IsTrue(sort(A) == 0);

            Assert.IsTrue(cumprod(A, 0) == 0);
            Assert.IsTrue(cumsum(A, 0) == 0);
            Assert.IsTrue(prod(A, 0) == 0);
            Assert.IsTrue(sort(A, 0) == 0);

            Assert.IsTrue(cumprod(A, 1) == 0);
            Assert.IsTrue(cumsum(A, 1) == 0);
            Assert.IsTrue(prod(A, 1) == 0);
            Assert.IsTrue(sort(A, 1) == 0);

            Assert.IsTrue(cumprod(A, 2) == 0);
            Assert.IsTrue(cumsum(A, 2) == 0);
            Assert.IsTrue(prod(A, 2) == 0);
            Assert.IsTrue(sort(A, 2) == 0);

        }
       

        [TestMethod]
        public void NPScalar_cum_prod_cum_sum_sort_byte() {

            Array<byte> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cumprod(A) == 1);
            Assert.IsTrue(cumsum(A) == 1);
            Assert.IsTrue(prod(A) == 1);
            Assert.IsTrue(sum(A) == 1);
            Assert.IsTrue(sort(A) == 1);

            Assert.IsTrue(cumprod(A, 0) == 1);
            Assert.IsTrue(cumsum(A, 0) == 1);
            Assert.IsTrue(prod(A, 0) == 1);
            Assert.IsTrue(sum(A, 0) == 1);
            Assert.IsTrue(sort(A, 0) == 1);

            Assert.IsTrue(cumprod(A, 1) == 1);
            Assert.IsTrue(cumsum(A, 1) == 1);
            Assert.IsTrue(prod(A, 1) == 1);
            Assert.IsTrue(sum(A, 1) == 1);
            Assert.IsTrue(sort(A, 1) == 1);

            Assert.IsTrue(cumprod(A, 2) == 1);
            Assert.IsTrue(cumsum(A, 2) == 1);
            Assert.IsTrue(prod(A, 2) == 1);
            Assert.IsTrue(sum(A, 2) == 1);
            Assert.IsTrue(sort(A, 2) == 1);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cumprod(A) == 0);
            Assert.IsTrue(cumsum(A) == 0);
            Assert.IsTrue(prod(A) == 0);
            Assert.IsTrue(sort(A) == 0);

            Assert.IsTrue(cumprod(A, 0) == 0);
            Assert.IsTrue(cumsum(A, 0) == 0);
            Assert.IsTrue(prod(A, 0) == 0);
            Assert.IsTrue(sort(A, 0) == 0);

            Assert.IsTrue(cumprod(A, 1) == 0);
            Assert.IsTrue(cumsum(A, 1) == 0);
            Assert.IsTrue(prod(A, 1) == 0);
            Assert.IsTrue(sort(A, 1) == 0);

            Assert.IsTrue(cumprod(A, 2) == 0);
            Assert.IsTrue(cumsum(A, 2) == 0);
            Assert.IsTrue(prod(A, 2) == 0);
            Assert.IsTrue(sort(A, 2) == 0);

        }
       

        [TestMethod]
        public void NPScalar_cum_prod_cum_sum_sort_sbyte() {

            Array<sbyte> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cumprod(A) == 1);
            Assert.IsTrue(cumsum(A) == 1);
            Assert.IsTrue(prod(A) == 1);
            Assert.IsTrue(sum(A) == 1);
            Assert.IsTrue(sort(A) == 1);

            Assert.IsTrue(cumprod(A, 0) == 1);
            Assert.IsTrue(cumsum(A, 0) == 1);
            Assert.IsTrue(prod(A, 0) == 1);
            Assert.IsTrue(sum(A, 0) == 1);
            Assert.IsTrue(sort(A, 0) == 1);

            Assert.IsTrue(cumprod(A, 1) == 1);
            Assert.IsTrue(cumsum(A, 1) == 1);
            Assert.IsTrue(prod(A, 1) == 1);
            Assert.IsTrue(sum(A, 1) == 1);
            Assert.IsTrue(sort(A, 1) == 1);

            Assert.IsTrue(cumprod(A, 2) == 1);
            Assert.IsTrue(cumsum(A, 2) == 1);
            Assert.IsTrue(prod(A, 2) == 1);
            Assert.IsTrue(sum(A, 2) == 1);
            Assert.IsTrue(sort(A, 2) == 1);

            A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cumprod(A) == 0);
            Assert.IsTrue(cumsum(A) == 0);
            Assert.IsTrue(prod(A) == 0);
            Assert.IsTrue(sort(A) == 0);

            Assert.IsTrue(cumprod(A, 0) == 0);
            Assert.IsTrue(cumsum(A, 0) == 0);
            Assert.IsTrue(prod(A, 0) == 0);
            Assert.IsTrue(sort(A, 0) == 0);

            Assert.IsTrue(cumprod(A, 1) == 0);
            Assert.IsTrue(cumsum(A, 1) == 0);
            Assert.IsTrue(prod(A, 1) == 0);
            Assert.IsTrue(sort(A, 1) == 0);

            Assert.IsTrue(cumprod(A, 2) == 0);
            Assert.IsTrue(cumsum(A, 2) == 0);
            Assert.IsTrue(prod(A, 2) == 0);
            Assert.IsTrue(sort(A, 2) == 0);

        }
       

        [TestMethod]
        public void NPScalar_cum_prod_cum_sum_sort_fcomplex() {

            Array<fcomplex> A = new fcomplex(1f, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cumprod(A) == new fcomplex(1f, 0));
            Assert.IsTrue(cumsum(A) == new fcomplex(1f, 0));
            Assert.IsTrue(prod(A) == new fcomplex(1f, 0));
            Assert.IsTrue(sum(A) == new fcomplex(1f, 0));
            Assert.IsTrue(sort(A) == new fcomplex(1f, 0));

            Assert.IsTrue(cumprod(A, 0) == new fcomplex(1f, 0));
            Assert.IsTrue(cumsum(A, 0) == new fcomplex(1f, 0));
            Assert.IsTrue(prod(A, 0) == new fcomplex(1f, 0));
            Assert.IsTrue(sum(A, 0) == new fcomplex(1f, 0));
            Assert.IsTrue(sort(A, 0) == new fcomplex(1f, 0));

            Assert.IsTrue(cumprod(A, 1) == new fcomplex(1f, 0));
            Assert.IsTrue(cumsum(A, 1) == new fcomplex(1f, 0));
            Assert.IsTrue(prod(A, 1) == new fcomplex(1f, 0));
            Assert.IsTrue(sum(A, 1) == new fcomplex(1f, 0));
            Assert.IsTrue(sort(A, 1) == new fcomplex(1f, 0));

            Assert.IsTrue(cumprod(A, 2) == new fcomplex(1f, 0));
            Assert.IsTrue(cumsum(A, 2) == new fcomplex(1f, 0));
            Assert.IsTrue(prod(A, 2) == new fcomplex(1f, 0));
            Assert.IsTrue(sum(A, 2) == new fcomplex(1f, 0));
            Assert.IsTrue(sort(A, 2) == new fcomplex(1f, 0));

            A = new fcomplex(0f, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cumprod(A) == new fcomplex(0f, 0));
            Assert.IsTrue(cumsum(A) == new fcomplex(0f, 0));
            Assert.IsTrue(prod(A) == new fcomplex(0f, 0));
            Assert.IsTrue(sort(A) == new fcomplex(0f, 0));

            Assert.IsTrue(cumprod(A, 0) == new fcomplex(0f, 0));
            Assert.IsTrue(cumsum(A, 0) == new fcomplex(0f, 0));
            Assert.IsTrue(prod(A, 0) == new fcomplex(0f, 0));
            Assert.IsTrue(sort(A, 0) == new fcomplex(0f, 0));

            Assert.IsTrue(cumprod(A, 1) == new fcomplex(0f, 0));
            Assert.IsTrue(cumsum(A, 1) == new fcomplex(0f, 0));
            Assert.IsTrue(prod(A, 1) == new fcomplex(0f, 0));
            Assert.IsTrue(sort(A, 1) == new fcomplex(0f, 0));

            Assert.IsTrue(cumprod(A, 2) == new fcomplex(0f, 0));
            Assert.IsTrue(cumsum(A, 2) == new fcomplex(0f, 0));
            Assert.IsTrue(prod(A, 2) == new fcomplex(0f, 0));
            Assert.IsTrue(sort(A, 2) == new fcomplex(0f, 0));

        }
       

        [TestMethod]
        public void NPScalar_cum_prod_cum_sum_sort_complex() {

            Array<complex> A = new complex(1.0, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cumprod(A) == new complex(1.0, 0));
            Assert.IsTrue(cumsum(A) == new complex(1.0, 0));
            Assert.IsTrue(prod(A) == new complex(1.0, 0));
            Assert.IsTrue(sum(A) == new complex(1.0, 0));
            Assert.IsTrue(sort(A) == new complex(1.0, 0));

            Assert.IsTrue(cumprod(A, 0) == new complex(1.0, 0));
            Assert.IsTrue(cumsum(A, 0) == new complex(1.0, 0));
            Assert.IsTrue(prod(A, 0) == new complex(1.0, 0));
            Assert.IsTrue(sum(A, 0) == new complex(1.0, 0));
            Assert.IsTrue(sort(A, 0) == new complex(1.0, 0));

            Assert.IsTrue(cumprod(A, 1) == new complex(1.0, 0));
            Assert.IsTrue(cumsum(A, 1) == new complex(1.0, 0));
            Assert.IsTrue(prod(A, 1) == new complex(1.0, 0));
            Assert.IsTrue(sum(A, 1) == new complex(1.0, 0));
            Assert.IsTrue(sort(A, 1) == new complex(1.0, 0));

            Assert.IsTrue(cumprod(A, 2) == new complex(1.0, 0));
            Assert.IsTrue(cumsum(A, 2) == new complex(1.0, 0));
            Assert.IsTrue(prod(A, 2) == new complex(1.0, 0));
            Assert.IsTrue(sum(A, 2) == new complex(1.0, 0));
            Assert.IsTrue(sort(A, 2) == new complex(1.0, 0));

            A = new complex(0.0, 0.0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cumprod(A) == new complex(0.0, 0.0));
            Assert.IsTrue(cumsum(A) == new complex(0.0, 0.0));
            Assert.IsTrue(prod(A) == new complex(0.0, 0.0));
            Assert.IsTrue(sort(A) == new complex(0.0, 0.0));

            Assert.IsTrue(cumprod(A, 0) == new complex(0.0, 0.0));
            Assert.IsTrue(cumsum(A, 0) == new complex(0.0, 0.0));
            Assert.IsTrue(prod(A, 0) == new complex(0.0, 0.0));
            Assert.IsTrue(sort(A, 0) == new complex(0.0, 0.0));

            Assert.IsTrue(cumprod(A, 1) == new complex(0.0, 0.0));
            Assert.IsTrue(cumsum(A, 1) == new complex(0.0, 0.0));
            Assert.IsTrue(prod(A, 1) == new complex(0.0, 0.0));
            Assert.IsTrue(sort(A, 1) == new complex(0.0, 0.0));

            Assert.IsTrue(cumprod(A, 2) == new complex(0.0, 0.0));
            Assert.IsTrue(cumsum(A, 2) == new complex(0.0, 0.0));
            Assert.IsTrue(prod(A, 2) == new complex(0.0, 0.0));
            Assert.IsTrue(sort(A, 2) == new complex(0.0, 0.0));

        }
       

        [TestMethod]
        public void NPScalar_cum_prod_cum_sum_sort_float() {

            Array<float> A = 1f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cumprod(A) == 1f);
            Assert.IsTrue(cumsum(A) == 1f);
            Assert.IsTrue(prod(A) == 1f);
            Assert.IsTrue(sum(A) == 1f);
            Assert.IsTrue(sort(A) == 1f);

            Assert.IsTrue(cumprod(A, 0) == 1f);
            Assert.IsTrue(cumsum(A, 0) == 1f);
            Assert.IsTrue(prod(A, 0) == 1f);
            Assert.IsTrue(sum(A, 0) == 1f);
            Assert.IsTrue(sort(A, 0) == 1f);

            Assert.IsTrue(cumprod(A, 1) == 1f);
            Assert.IsTrue(cumsum(A, 1) == 1f);
            Assert.IsTrue(prod(A, 1) == 1f);
            Assert.IsTrue(sum(A, 1) == 1f);
            Assert.IsTrue(sort(A, 1) == 1f);

            Assert.IsTrue(cumprod(A, 2) == 1f);
            Assert.IsTrue(cumsum(A, 2) == 1f);
            Assert.IsTrue(prod(A, 2) == 1f);
            Assert.IsTrue(sum(A, 2) == 1f);
            Assert.IsTrue(sort(A, 2) == 1f);

            A = 0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(cumprod(A) == 0f);
            Assert.IsTrue(cumsum(A) == 0f);
            Assert.IsTrue(prod(A) == 0f);
            Assert.IsTrue(sort(A) == 0f);

            Assert.IsTrue(cumprod(A, 0) == 0f);
            Assert.IsTrue(cumsum(A, 0) == 0f);
            Assert.IsTrue(prod(A, 0) == 0f);
            Assert.IsTrue(sort(A, 0) == 0f);

            Assert.IsTrue(cumprod(A, 1) == 0f);
            Assert.IsTrue(cumsum(A, 1) == 0f);
            Assert.IsTrue(prod(A, 1) == 0f);
            Assert.IsTrue(sort(A, 1) == 0f);

            Assert.IsTrue(cumprod(A, 2) == 0f);
            Assert.IsTrue(cumsum(A, 2) == 0f);
            Assert.IsTrue(prod(A, 2) == 0f);
            Assert.IsTrue(sort(A, 2) == 0f);

        }

#endregion HYCALPER AUTO GENERATED CODE

        #region HYCALPER LOOPSTART diff
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
        [ExpectedException(typeof(ArgumentException))]
        public void NPScalar_diff_double_1() {

            Array<double> A = 1.0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            diff(A);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NPScalar_diff_double_0() {

            Array<double> A = 0.0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            diff(A);

        }

        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NPScalar_diff_ulong_1() {

            Array<ulong> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            diff(A);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NPScalar_diff_ulong_0() {

            Array<ulong> A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            diff(A);

        }

       

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NPScalar_diff_long_1() {

            Array<long> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            diff(A);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NPScalar_diff_long_0() {

            Array<long> A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            diff(A);

        }

       

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NPScalar_diff_uint_1() {

            Array<uint> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            diff(A);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NPScalar_diff_uint_0() {

            Array<uint> A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            diff(A);

        }

       

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NPScalar_diff_int_1() {

            Array<int> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            diff(A);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NPScalar_diff_int_0() {

            Array<int> A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            diff(A);

        }

       

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NPScalar_diff_ushort_1() {

            Array<ushort> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            diff(A);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NPScalar_diff_ushort_0() {

            Array<ushort> A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            diff(A);

        }

       

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NPScalar_diff_short_1() {

            Array<short> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            diff(A);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NPScalar_diff_short_0() {

            Array<short> A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            diff(A);

        }

       

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NPScalar_diff_byte_1() {

            Array<byte> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            diff(A);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NPScalar_diff_byte_0() {

            Array<byte> A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            diff(A);

        }

       

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NPScalar_diff_sbyte_1() {

            Array<sbyte> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            diff(A);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NPScalar_diff_sbyte_0() {

            Array<sbyte> A = 0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            diff(A);

        }

       

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NPScalar_diff_fcomplex_1() {

            Array<fcomplex> A = new fcomplex(1f, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            diff(A);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NPScalar_diff_fcomplex_0() {

            Array<fcomplex> A = new fcomplex(0f, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            diff(A);

        }

       

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NPScalar_diff_complex_1() {

            Array<complex> A = new complex(1.0, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            diff(A);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NPScalar_diff_complex_0() {

            Array<complex> A = new complex(0.0, 0.0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            diff(A);

        }

       

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NPScalar_diff_float_1() {

            Array<float> A = 1f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            diff(A);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NPScalar_diff_float_0() {

            Array<float> A = 0f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            diff(A);

        }


#endregion HYCALPER AUTO GENERATED CODE
        #region HYCALPER LOOPSTART max_min
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
        public void NPScalar_max_min_double() {

            Array<long> I = -1;
            Array<double> A = 1.0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(max(A) == 1.0);
            Assert.IsTrue(max(A, dim: 0) == 1.0);
            Assert.IsTrue(max(A, dim: 1) == 1.0);
            Assert.IsTrue(max(A, dim: 2) == 1.0);

            Assert.IsTrue(max(A, I: I) == 1.0);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 0) == 1.0);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 1) == 1.0);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 2) == 1.0);
            Assert.IsTrue(I == 0);

            I = -1;
            A = 1.0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(min(A) == 1.0);
            Assert.IsTrue(min(A, dim: 0) == 1.0);
            Assert.IsTrue(min(A, dim: 1) == 1.0);
            Assert.IsTrue(min(A, dim: 2) == 1.0);

            Assert.IsTrue(min(A, I: I) == 1.0);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 0) == 1.0);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 1) == 1.0);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 2) == 1.0);
            Assert.IsTrue(I == 0);

        }

        #endregion HYCALPER LOOPEND 
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        [TestMethod]
        public void NPScalar_max_min_ulong() {

            Array<long> I = -1;
            Array<ulong> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(max(A) == 1);
            Assert.IsTrue(max(A, dim: 0) == 1);
            Assert.IsTrue(max(A, dim: 1) == 1);
            Assert.IsTrue(max(A, dim: 2) == 1);

            Assert.IsTrue(max(A, I: I) == 1);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 0) == 1);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 1) == 1);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 2) == 1);
            Assert.IsTrue(I == 0);

            I = -1;
            A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(min(A) == 1);
            Assert.IsTrue(min(A, dim: 0) == 1);
            Assert.IsTrue(min(A, dim: 1) == 1);
            Assert.IsTrue(min(A, dim: 2) == 1);

            Assert.IsTrue(min(A, I: I) == 1);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 0) == 1);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 1) == 1);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 2) == 1);
            Assert.IsTrue(I == 0);

        }

       

        [TestMethod]
        public void NPScalar_max_min_long() {

            Array<long> I = -1;
            Array<long> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(max(A) == 1);
            Assert.IsTrue(max(A, dim: 0) == 1);
            Assert.IsTrue(max(A, dim: 1) == 1);
            Assert.IsTrue(max(A, dim: 2) == 1);

            Assert.IsTrue(max(A, I: I) == 1);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 0) == 1);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 1) == 1);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 2) == 1);
            Assert.IsTrue(I == 0);

            I = -1;
            A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(min(A) == 1);
            Assert.IsTrue(min(A, dim: 0) == 1);
            Assert.IsTrue(min(A, dim: 1) == 1);
            Assert.IsTrue(min(A, dim: 2) == 1);

            Assert.IsTrue(min(A, I: I) == 1);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 0) == 1);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 1) == 1);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 2) == 1);
            Assert.IsTrue(I == 0);

        }

       

        [TestMethod]
        public void NPScalar_max_min_uint() {

            Array<long> I = -1;
            Array<uint> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(max(A) == 1);
            Assert.IsTrue(max(A, dim: 0) == 1);
            Assert.IsTrue(max(A, dim: 1) == 1);
            Assert.IsTrue(max(A, dim: 2) == 1);

            Assert.IsTrue(max(A, I: I) == 1);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 0) == 1);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 1) == 1);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 2) == 1);
            Assert.IsTrue(I == 0);

            I = -1;
            A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(min(A) == 1);
            Assert.IsTrue(min(A, dim: 0) == 1);
            Assert.IsTrue(min(A, dim: 1) == 1);
            Assert.IsTrue(min(A, dim: 2) == 1);

            Assert.IsTrue(min(A, I: I) == 1);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 0) == 1);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 1) == 1);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 2) == 1);
            Assert.IsTrue(I == 0);

        }

       

        [TestMethod]
        public void NPScalar_max_min_int() {

            Array<long> I = -1;
            Array<int> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(max(A) == 1);
            Assert.IsTrue(max(A, dim: 0) == 1);
            Assert.IsTrue(max(A, dim: 1) == 1);
            Assert.IsTrue(max(A, dim: 2) == 1);

            Assert.IsTrue(max(A, I: I) == 1);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 0) == 1);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 1) == 1);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 2) == 1);
            Assert.IsTrue(I == 0);

            I = -1;
            A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(min(A) == 1);
            Assert.IsTrue(min(A, dim: 0) == 1);
            Assert.IsTrue(min(A, dim: 1) == 1);
            Assert.IsTrue(min(A, dim: 2) == 1);

            Assert.IsTrue(min(A, I: I) == 1);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 0) == 1);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 1) == 1);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 2) == 1);
            Assert.IsTrue(I == 0);

        }

       

        [TestMethod]
        public void NPScalar_max_min_ushort() {

            Array<long> I = -1;
            Array<ushort> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(max(A) == 1);
            Assert.IsTrue(max(A, dim: 0) == 1);
            Assert.IsTrue(max(A, dim: 1) == 1);
            Assert.IsTrue(max(A, dim: 2) == 1);

            Assert.IsTrue(max(A, I: I) == 1);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 0) == 1);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 1) == 1);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 2) == 1);
            Assert.IsTrue(I == 0);

            I = -1;
            A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(min(A) == 1);
            Assert.IsTrue(min(A, dim: 0) == 1);
            Assert.IsTrue(min(A, dim: 1) == 1);
            Assert.IsTrue(min(A, dim: 2) == 1);

            Assert.IsTrue(min(A, I: I) == 1);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 0) == 1);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 1) == 1);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 2) == 1);
            Assert.IsTrue(I == 0);

        }

       

        [TestMethod]
        public void NPScalar_max_min_short() {

            Array<long> I = -1;
            Array<short> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(max(A) == 1);
            Assert.IsTrue(max(A, dim: 0) == 1);
            Assert.IsTrue(max(A, dim: 1) == 1);
            Assert.IsTrue(max(A, dim: 2) == 1);

            Assert.IsTrue(max(A, I: I) == 1);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 0) == 1);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 1) == 1);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 2) == 1);
            Assert.IsTrue(I == 0);

            I = -1;
            A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(min(A) == 1);
            Assert.IsTrue(min(A, dim: 0) == 1);
            Assert.IsTrue(min(A, dim: 1) == 1);
            Assert.IsTrue(min(A, dim: 2) == 1);

            Assert.IsTrue(min(A, I: I) == 1);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 0) == 1);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 1) == 1);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 2) == 1);
            Assert.IsTrue(I == 0);

        }

       

        [TestMethod]
        public void NPScalar_max_min_byte() {

            Array<long> I = -1;
            Array<byte> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(max(A) == 1);
            Assert.IsTrue(max(A, dim: 0) == 1);
            Assert.IsTrue(max(A, dim: 1) == 1);
            Assert.IsTrue(max(A, dim: 2) == 1);

            Assert.IsTrue(max(A, I: I) == 1);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 0) == 1);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 1) == 1);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 2) == 1);
            Assert.IsTrue(I == 0);

            I = -1;
            A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(min(A) == 1);
            Assert.IsTrue(min(A, dim: 0) == 1);
            Assert.IsTrue(min(A, dim: 1) == 1);
            Assert.IsTrue(min(A, dim: 2) == 1);

            Assert.IsTrue(min(A, I: I) == 1);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 0) == 1);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 1) == 1);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 2) == 1);
            Assert.IsTrue(I == 0);

        }

       

        [TestMethod]
        public void NPScalar_max_min_sbyte() {

            Array<long> I = -1;
            Array<sbyte> A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(max(A) == 1);
            Assert.IsTrue(max(A, dim: 0) == 1);
            Assert.IsTrue(max(A, dim: 1) == 1);
            Assert.IsTrue(max(A, dim: 2) == 1);

            Assert.IsTrue(max(A, I: I) == 1);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 0) == 1);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 1) == 1);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 2) == 1);
            Assert.IsTrue(I == 0);

            I = -1;
            A = 1;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(min(A) == 1);
            Assert.IsTrue(min(A, dim: 0) == 1);
            Assert.IsTrue(min(A, dim: 1) == 1);
            Assert.IsTrue(min(A, dim: 2) == 1);

            Assert.IsTrue(min(A, I: I) == 1);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 0) == 1);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 1) == 1);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 2) == 1);
            Assert.IsTrue(I == 0);

        }

       

        [TestMethod]
        public void NPScalar_max_min_fcomplex() {

            Array<long> I = -1;
            Array<fcomplex> A = new fcomplex(1f, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(max(A) == new fcomplex(1f, 0));
            Assert.IsTrue(max(A, dim: 0) == new fcomplex(1f, 0));
            Assert.IsTrue(max(A, dim: 1) == new fcomplex(1f, 0));
            Assert.IsTrue(max(A, dim: 2) == new fcomplex(1f, 0));

            Assert.IsTrue(max(A, I: I) == new fcomplex(1f, 0));
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 0) == new fcomplex(1f, 0));
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 1) == new fcomplex(1f, 0));
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 2) == new fcomplex(1f, 0));
            Assert.IsTrue(I == 0);

            I = -1;
            A = new fcomplex(1f, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(min(A) == new fcomplex(1f, 0));
            Assert.IsTrue(min(A, dim: 0) == new fcomplex(1f, 0));
            Assert.IsTrue(min(A, dim: 1) == new fcomplex(1f, 0));
            Assert.IsTrue(min(A, dim: 2) == new fcomplex(1f, 0));

            Assert.IsTrue(min(A, I: I) == new fcomplex(1f, 0));
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 0) == new fcomplex(1f, 0));
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 1) == new fcomplex(1f, 0));
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 2) == new fcomplex(1f, 0));
            Assert.IsTrue(I == 0);

        }

       

        [TestMethod]
        public void NPScalar_max_min_complex() {

            Array<long> I = -1;
            Array<complex> A = new complex(1.0, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(max(A) == new complex(1.0, 0));
            Assert.IsTrue(max(A, dim: 0) == new complex(1.0, 0));
            Assert.IsTrue(max(A, dim: 1) == new complex(1.0, 0));
            Assert.IsTrue(max(A, dim: 2) == new complex(1.0, 0));

            Assert.IsTrue(max(A, I: I) == new complex(1.0, 0));
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 0) == new complex(1.0, 0));
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 1) == new complex(1.0, 0));
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 2) == new complex(1.0, 0));
            Assert.IsTrue(I == 0);

            I = -1;
            A = new complex(1.0, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(min(A) == new complex(1.0, 0));
            Assert.IsTrue(min(A, dim: 0) == new complex(1.0, 0));
            Assert.IsTrue(min(A, dim: 1) == new complex(1.0, 0));
            Assert.IsTrue(min(A, dim: 2) == new complex(1.0, 0));

            Assert.IsTrue(min(A, I: I) == new complex(1.0, 0));
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 0) == new complex(1.0, 0));
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 1) == new complex(1.0, 0));
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 2) == new complex(1.0, 0));
            Assert.IsTrue(I == 0);

        }

       

        [TestMethod]
        public void NPScalar_max_min_float() {

            Array<long> I = -1;
            Array<float> A = 1f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(max(A) == 1f);
            Assert.IsTrue(max(A, dim: 0) == 1f);
            Assert.IsTrue(max(A, dim: 1) == 1f);
            Assert.IsTrue(max(A, dim: 2) == 1f);

            Assert.IsTrue(max(A, I: I) == 1f);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 0) == 1f);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 1) == 1f);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(max(A, I: I, dim: 2) == 1f);
            Assert.IsTrue(I == 0);

            I = -1;
            A = 1f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(min(A) == 1f);
            Assert.IsTrue(min(A, dim: 0) == 1f);
            Assert.IsTrue(min(A, dim: 1) == 1f);
            Assert.IsTrue(min(A, dim: 2) == 1f);

            Assert.IsTrue(min(A, I: I) == 1f);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 0) == 1f);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 1) == 1f);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(min(A, I: I, dim: 2) == 1f);
            Assert.IsTrue(I == 0);

        }


#endregion HYCALPER AUTO GENERATED CODE

        #region HYCALPER LOOPSTART numpy.max_min
        /*!HC:TYPELIST:
<hycalper>
<type>
<source locate="here">
double
</source>
<destination>float</destination>
<destination>complex</destination>
<destination>fcomplex</destination>
</type>
<type>
<source locate="here">
1.0
</source>
<destination>1f</destination>
<destination>new complex(1.0, 0)</destination>
<destination>new fcomplex(1f, 0)</destination>
</type>
<type>
<source locate="here">
0.0
</source>
<destination>0f</destination>
<destination>new complex(0.0, 0.0)</destination>
<destination>new fcomplex(0f, 0)</destination>
</type>
</hycalper>
*/

        [TestMethod]
        public void NPScalar_numpy_max_min_double() {

            Array<long> I = -1;
            Array<double> A = 1.0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(numpy.max(A) == 1.0);
            Assert.IsTrue(numpy.max(A, dim: 0) == 1.0);
            Assert.IsTrue(numpy.max(A, dim: 1) == 1.0);
            Assert.IsTrue(numpy.max(A, dim: 2) == 1.0);

            Assert.IsTrue(numpy.max(A, I: I) == 1.0);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(numpy.max(A, I: I, dim: 0) == 1.0);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(numpy.max(A, I: I, dim: 1) == 1.0);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(numpy.max(A, I: I, dim: 2) == 1.0);
            Assert.IsTrue(I == 0);

            I = -1;
            A = 1.0;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(numpy.min(A) == 1.0);
            Assert.IsTrue(numpy.min(A, dim: 0) == 1.0);
            Assert.IsTrue(numpy.min(A, dim: 1) == 1.0);
            Assert.IsTrue(numpy.min(A, dim: 2) == 1.0);

            Assert.IsTrue(numpy.min(A, I: I) == 1.0);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(numpy.min(A, I: I, dim: 0) == 1.0);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(numpy.min(A, I: I, dim: 1) == 1.0);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(numpy.min(A, I: I, dim: 2) == 1.0);
            Assert.IsTrue(I == 0);

        }

        #endregion HYCALPER LOOPEND 
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        [TestMethod]
        public void NPScalar_numpy_max_min_fcomplex() {

            Array<long> I = -1;
            Array<fcomplex> A = new fcomplex(1f, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(numpy.max(A) == new fcomplex(1f, 0));
            Assert.IsTrue(numpy.max(A, dim: 0) == new fcomplex(1f, 0));
            Assert.IsTrue(numpy.max(A, dim: 1) == new fcomplex(1f, 0));
            Assert.IsTrue(numpy.max(A, dim: 2) == new fcomplex(1f, 0));

            Assert.IsTrue(numpy.max(A, I: I) == new fcomplex(1f, 0));
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(numpy.max(A, I: I, dim: 0) == new fcomplex(1f, 0));
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(numpy.max(A, I: I, dim: 1) == new fcomplex(1f, 0));
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(numpy.max(A, I: I, dim: 2) == new fcomplex(1f, 0));
            Assert.IsTrue(I == 0);

            I = -1;
            A = new fcomplex(1f, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(numpy.min(A) == new fcomplex(1f, 0));
            Assert.IsTrue(numpy.min(A, dim: 0) == new fcomplex(1f, 0));
            Assert.IsTrue(numpy.min(A, dim: 1) == new fcomplex(1f, 0));
            Assert.IsTrue(numpy.min(A, dim: 2) == new fcomplex(1f, 0));

            Assert.IsTrue(numpy.min(A, I: I) == new fcomplex(1f, 0));
            Assert.IsTrue(I == 0);
            Assert.IsTrue(numpy.min(A, I: I, dim: 0) == new fcomplex(1f, 0));
            Assert.IsTrue(I == 0);
            Assert.IsTrue(numpy.min(A, I: I, dim: 1) == new fcomplex(1f, 0));
            Assert.IsTrue(I == 0);
            Assert.IsTrue(numpy.min(A, I: I, dim: 2) == new fcomplex(1f, 0));
            Assert.IsTrue(I == 0);

        }

       

        [TestMethod]
        public void NPScalar_numpy_max_min_complex() {

            Array<long> I = -1;
            Array<complex> A = new complex(1.0, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(numpy.max(A) == new complex(1.0, 0));
            Assert.IsTrue(numpy.max(A, dim: 0) == new complex(1.0, 0));
            Assert.IsTrue(numpy.max(A, dim: 1) == new complex(1.0, 0));
            Assert.IsTrue(numpy.max(A, dim: 2) == new complex(1.0, 0));

            Assert.IsTrue(numpy.max(A, I: I) == new complex(1.0, 0));
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(numpy.max(A, I: I, dim: 0) == new complex(1.0, 0));
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(numpy.max(A, I: I, dim: 1) == new complex(1.0, 0));
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(numpy.max(A, I: I, dim: 2) == new complex(1.0, 0));
            Assert.IsTrue(I == 0);

            I = -1;
            A = new complex(1.0, 0);
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(numpy.min(A) == new complex(1.0, 0));
            Assert.IsTrue(numpy.min(A, dim: 0) == new complex(1.0, 0));
            Assert.IsTrue(numpy.min(A, dim: 1) == new complex(1.0, 0));
            Assert.IsTrue(numpy.min(A, dim: 2) == new complex(1.0, 0));

            Assert.IsTrue(numpy.min(A, I: I) == new complex(1.0, 0));
            Assert.IsTrue(I == 0);
            Assert.IsTrue(numpy.min(A, I: I, dim: 0) == new complex(1.0, 0));
            Assert.IsTrue(I == 0);
            Assert.IsTrue(numpy.min(A, I: I, dim: 1) == new complex(1.0, 0));
            Assert.IsTrue(I == 0);
            Assert.IsTrue(numpy.min(A, I: I, dim: 2) == new complex(1.0, 0));
            Assert.IsTrue(I == 0);

        }

       

        [TestMethod]
        public void NPScalar_numpy_max_min_float() {

            Array<long> I = -1;
            Array<float> A = 1f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(numpy.max(A) == 1f);
            Assert.IsTrue(numpy.max(A, dim: 0) == 1f);
            Assert.IsTrue(numpy.max(A, dim: 1) == 1f);
            Assert.IsTrue(numpy.max(A, dim: 2) == 1f);

            Assert.IsTrue(numpy.max(A, I: I) == 1f);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(numpy.max(A, I: I, dim: 0) == 1f);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(numpy.max(A, I: I, dim: 1) == 1f);
            Assert.IsTrue(I == 0); I = -1;
            Assert.IsTrue(numpy.max(A, I: I, dim: 2) == 1f);
            Assert.IsTrue(I == 0);

            I = -1;
            A = 1f;
            Assert.IsTrue(A.S.NumberOfDimensions == 0);
            Assert.IsTrue(numpy.min(A) == 1f);
            Assert.IsTrue(numpy.min(A, dim: 0) == 1f);
            Assert.IsTrue(numpy.min(A, dim: 1) == 1f);
            Assert.IsTrue(numpy.min(A, dim: 2) == 1f);

            Assert.IsTrue(numpy.min(A, I: I) == 1f);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(numpy.min(A, I: I, dim: 0) == 1f);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(numpy.min(A, I: I, dim: 1) == 1f);
            Assert.IsTrue(I == 0);
            Assert.IsTrue(numpy.min(A, I: I, dim: 2) == 1f);
            Assert.IsTrue(I == 0);

        }


#endregion HYCALPER AUTO GENERATED CODE
    }
}
