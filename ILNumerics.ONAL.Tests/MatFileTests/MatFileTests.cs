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
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using ILNumerics;
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

namespace ILNumerics.UnitTests.Legacy_Tests {
    [TestClass]
    public class MatFileTests {

        public const string MATLAB_2ArrTest_NAME = "MATLAB_2ArrTest.mat";
        public const string test_matfile_NAME = "test_matfile.mat";

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "names starting with numbers are not allowed!")]
        public void MatFile_NameRestrictions_FailI()
        {
            MatFile ml = new MatFile();
            Array<double> A = rand(3, 23, 2);
            ml["1fsdf"] = A + 1;
            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "names with blank space are not allowed!")]
        public void MatFile_NameRestrictions_FailII()
        {
            MatFile ml = new MatFile();
            Array<double> A = rand(3, 23, 2);
            ml["invaldi name"] = A + 1;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "names with invalid characters are not allowed!")]
        public void MatFile_NameRestrictions_FailIII()
        {
            MatFile ml = new MatFile();
            Array<double> A = rand(3, 23, 2);
            ml["a$me"] = A + 1;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "key not found expected")]
        public void MatFile_NameRestrictions_FailIV()
        {
            MatFile ml = new MatFile();
            Array<double> A = rand(3, 23, 2);
            A = ml.GetArray<double>("hallo_");
 
        }

        [TestMethod]
        public void MatFile_NameRestrictions()
        {
            MatFile ml = new MatFile();
            Array<double> A = rand(3, 23, 2);
            ml["hallo"] = A;
            Array<double> res = ml.GetArray<double>("hallo");
            Assert.AreEqual(res, A, "invalid array returned for : 'hallo'");
            var oldLength = ml.Count;
            ml["a1"] = A + 1;
            Assert.AreEqual(ml.Count,oldLength + 1);
            ml["hallo"] = null;
            Assert.AreEqual(ml.Count, 1u, "unable to remove array from MatFile");

        }


        [TestMethod]
        public void MatFile_Test_AllTypes()
        {
            //ILN(enabled=false)
            Array<double> tmp = empty<double>();
            Test_StreamMatlab("testarray1.mat", tmp);
            Test_StreamMatlab("testarray1.mat", ones(1, 1));
            Test_StreamMatlab("testarray1.mat", rand(10, 1));
            Test_StreamMatlab("testarray1.mat", rand(1, 10));
            Test_StreamMatlab("testarray1.mat", rand(0, 1));
            Test_StreamMatlab("testarray1.mat", rand(10, 100, 4));

            Test_StreamMatlab("testarray1.mat", tosingle(ones(1, 1)));
            Test_StreamMatlab("testarray1.mat", tosingle(empty<double>()));
            Test_StreamMatlab("testarray1.mat", tosingle(rand(10, 1)));
            Test_StreamMatlab("testarray1.mat", tosingle(rand(1, 10)));
            Test_StreamMatlab("testarray1.mat", tosingle(rand(0, 1)));
            Test_StreamMatlab("testarray1.mat", tosingle(rand(10, 100, 4)));

            Test_StreamMatlab("testarray1.mat", tological(ones(1, 1)));
            Test_StreamMatlab("testarray1.mat", tological(empty<double>()));
            Test_StreamMatlab("testarray1.mat", tological(rand(10, 1)));
            Test_StreamMatlab("testarray1.mat", tological(rand(1, 10)));
            Test_StreamMatlab("testarray1.mat", tological(rand(0, 1)));
            Test_StreamMatlab("testarray1.mat", tological(rand(10, 100, 4)));

            Test_StreamMatlab("testarray1.mat", vector(new complex(1.0, 2.0)));
            Test_StreamMatlab("testarray1.mat", tocomplex(empty<double>()));
            Test_StreamMatlab("testarray1.mat", tocomplex(rand(10, 1)));
            Test_StreamMatlab("testarray1.mat", tocomplex(rand(1, 10)));
            Test_StreamMatlab("testarray1.mat", tocomplex(rand(0, 1)));
            Test_StreamMatlab("testarray1.mat", tocomplex(rand(10, 100, 4)));

            Test_StreamMatlab("testarray1.mat", vector(new fcomplex(1.0f, 2.0f)));
            Test_StreamMatlab("testarray1.mat", tofcomplex(empty<double>()));
            Test_StreamMatlab("testarray1.mat", tofcomplex(rand(10, 1)));
            Test_StreamMatlab("testarray1.mat", tofcomplex(rand(1, 10)));
            Test_StreamMatlab("testarray1.mat", tofcomplex(rand(0, 1)));
            Test_StreamMatlab("testarray1.mat", tofcomplex(rand(10, 100, 4)));

            //Test_StreamMatlab("testarray1.mat",new Array<char>(new char[]{'A','B','F'})); 
            //Test_StreamMatlab("testarray1.mat",new Array<char>(new char[]{'A','B','F'}).T); 

            Test_StreamMatlab("testarray1.mat", toint8(ones(1, 1)));
            Test_StreamMatlab("testarray1.mat", toint8(empty<double>()));
            Test_StreamMatlab("testarray1.mat", toint8(rand(10, 1)));
            Test_StreamMatlab("testarray1.mat", toint8(rand(1, 10) * 255));
            Test_StreamMatlab("testarray1.mat", toint8(rand(0, 1) * 255));
            Test_StreamMatlab("testarray1.mat", toint8(rand(10, 100, 4) * 255));

            Test_StreamMatlab("testarray1.mat", touint8(ones(1, 1)));
            Test_StreamMatlab("testarray1.mat", touint8(empty<double>()));
            Test_StreamMatlab("testarray1.mat", touint8(rand(10, 1)));
            Test_StreamMatlab("testarray1.mat", touint8(rand(1, 10) * 255));
            Test_StreamMatlab("testarray1.mat", touint8(rand(0, 1) * 255));
            Test_StreamMatlab("testarray1.mat", touint8(rand(10, 100, 4) * 255));

            Test_StreamMatlab("testarray1.mat", toint16(ones(1, 1)));
            Test_StreamMatlab("testarray1.mat", toint16(empty<double>()));
            Test_StreamMatlab("testarray1.mat", toint16(rand(10, 1)));
            Test_StreamMatlab("testarray1.mat", toint16(rand(1, 10) * 255));
            Test_StreamMatlab("testarray1.mat", toint16(rand(0, 1) * 255));
            Test_StreamMatlab("testarray1.mat", toint16(rand(10, 100, 4) * 255));

            Test_StreamMatlab("testarray1.mat", touint16(ones(1, 1)));
            Test_StreamMatlab("testarray1.mat", touint16(empty<double>()));
            Test_StreamMatlab("testarray1.mat", touint16(rand(10, 1)));
            Test_StreamMatlab("testarray1.mat", touint16(rand(1, 10) * 255));
            Test_StreamMatlab("testarray1.mat", touint16(rand(0, 1) * 255));
            Test_StreamMatlab("testarray1.mat", touint16(rand(10, 100, 4) * 255));

            Test_StreamMatlab("testarray1.mat", toint32(ones(1, 1)));
            Test_StreamMatlab("testarray1.mat", toint32(empty<double>()));
            Test_StreamMatlab("testarray1.mat", toint32(rand(10, 1)));
            Test_StreamMatlab("testarray1.mat", toint32(rand(1, 10) * 255));
            Test_StreamMatlab("testarray1.mat", toint32(rand(0, 1) * 255));
            Test_StreamMatlab("testarray1.mat", toint32(rand(10, 100, 4) * 255));

            Test_StreamMatlab("testarray1.mat", touint32(ones(1, 1)));
            Test_StreamMatlab("testarray1.mat", touint32(empty<double>()));
            Test_StreamMatlab("testarray1.mat", touint32(rand(10, 1)));
            Test_StreamMatlab("testarray1.mat", touint32(rand(1, 10) * 255));
            Test_StreamMatlab("testarray1.mat", touint32(rand(0, 1) * 255));
            Test_StreamMatlab("testarray1.mat", touint32(rand(10, 100, 4) * 255));

            Test_StreamMatlab("testarray1.mat", toint64(ones(1, 1) * 16000));
            Test_StreamMatlab("testarray1.mat", toint64(empty<double>() * 16000));
            Test_StreamMatlab("testarray1.mat", toint64(rand(10, 1) * 16000));
            Test_StreamMatlab("testarray1.mat", toint64(rand(1, 10) * 16000));
            Test_StreamMatlab("testarray1.mat", toint64(rand(0, 1) * 16000));
            Test_StreamMatlab("testarray1.mat", toint64(rand(10, 100, 4) * 16000));

            Test_StreamMatlab("testarray1.mat", touint64(ones(1, 1) * 16000));
            Test_StreamMatlab("testarray1.mat", touint64(empty<double>() * 16000));
            Test_StreamMatlab("testarray1.mat", touint64(rand(10, 1) * 16000));
            Test_StreamMatlab("testarray1.mat", touint64(rand(1, 10) * 16000));
            Test_StreamMatlab("testarray1.mat", touint64(rand(0, 1) * 16000));
            Test_StreamMatlab("testarray1.mat", touint64(rand(10, 100, 4) * 16000));
            //ILN(enabled=true)

        }


        public void Test_StreamMatlab(string filename, InCell arr)
        {
            using (Scope.Enter(arr))
            {

                MatFile outFile = new MatFile(cellv(ones<uint>(55, 4, 2, 4), arr.GetValue(0), counter<float>(1f, 1f, 16, 17)));
                if (File.Exists(filename)) {
                    File.Delete(filename); 
                }
                outFile.Write(filename: filename); 
                //using (FileStream s = new FileStream(filename, FileMode.Create))
                //{
                //    arr.ToStream(s, "", ArrayStreamSerializationFlags.Matlab);
                //}
                // test -> read back 
                MatFile inp = new MatFile(filename);
                Assert.AreEqual(3u, inp.Count, "invalid number of arrays after read back from matfile!");
                Assert.IsTrue(Equals(inp.Arrays.GetArray<uint>(0), ones<uint>(55, 4, 2, 4)), "invalid values after re-reading from mat file! Index: 0");
                BaseArray reread = inp.Arrays.GetValue(1);
                Assert.IsTrue(Equals(reread, arr.GetValue(0)), "invalid values after re-reading from mat file! Index: 1");
                Assert.IsTrue(Equals(counter<float>(1f, 1f, 16, 17), inp.Arrays.GetArray<float>(2)), "invalid values after re-reading from mat file! Index: 2");

            }
        }
        
        [TestMethod]
        public void MatFile_writeMatlab()
        {
            using (Scope.Enter())
            {


                Array<double> A = counter(1.0, 1.0, 4, 5);
                Array<float> B = zeros<float>(100, 200);
                var matfile = new MatFile();
                matfile["A"] = A;
                matfile["B"] = B;
                matfile.Write("sample.mat");
                // read back
                var matfile2 = new MatFile("sample.mat");
                Array<double> a = matfile2.GetArray<double>(0);
                Array<float> b = matfile2.GetArray<float>("B");
                Assert.AreEqual(a,A);
                Assert.AreEqual(b,B);
            }
        }
                
        [TestMethod]
        public void MatFile_ImportMatlab()
        {
            using (Scope.Enter()) {

                if (!File.Exists(MATLAB_2ArrTest_NAME)) {
                    ILNumerics.Core.UnitTests.Helper.GetResourceFileName(MATLAB_2ArrTest_NAME);
                }
                var matFile = new MatFile(MATLAB_2ArrTest_NAME);
                IList<string> keys = matFile.Keys;
                Assert.AreEqual(keys.Count, 2);
                Assert.IsTrue(keys.Contains("logical10x10"));
                Assert.IsTrue(keys.Contains("double3x2x2"));
                Cell arraysFromMatFile = matFile.Arrays;
                Assert.AreEqual(arraysFromMatFile.Length, 2, "Test_ImportMatlab failed: invalid number of arrays returned.");
                Assert.AreEqual(arraysFromMatFile.GetLogical(0).Size.NumberOfElements, 100, "Test_ImportMatlab failed: invalid size of first array returned.");
                Assert.AreEqual(arraysFromMatFile.GetArray<double>(1).Size.NumberOfElements, 12, "Test_ImportMatlab failed: invalid size of 2. array returned.");

            }
        }

        [TestMethod]
        public void MatFile_TestMatlab()
        {
            using (Scope.Enter()) {

                if (!File.Exists(test_matfile_NAME)) {
                    ILNumerics.Core.UnitTests.Helper.GetResourceFileName(test_matfile_NAME);
                }
                MatFile matFile = new MatFile(test_matfile_NAME);
                Cell arraysFromMatFile = matFile.Arrays;
                Assert.AreEqual(arraysFromMatFile.Length, 2, "Test_ImportMatlab failed: invalid number of arrays returned.");
                Assert.AreEqual(arraysFromMatFile.GetValue(0).Size.NumberOfElements, 100, "Test_ImportMatlab failed: invalid size of first array returned.");
                Assert.AreEqual(arraysFromMatFile.GetValue(1).Size.NumberOfElements, 12, "Test_ImportMatlab failed: invalid size of 2. array returned.");

            }
        }
    }
}
