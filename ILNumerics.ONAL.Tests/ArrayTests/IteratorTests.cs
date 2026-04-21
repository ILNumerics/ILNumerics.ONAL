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
using System.Collections.Generic;
using static ILNumerics.Globals; 
using static ILNumerics.ILMath;
using ILNumerics.Core.StorageLayer;

namespace ILNumerics.Core.UnitTests {
    [TestClass]
    public class IteratorTests {

        // for matrices, vectors, (ML scalars), (ML) empties, (np) empties, (np) scalars

        #region HYCALPER LOOPSTART
        /*!HC:TYPELIST:
<hycalper>
    <type>
        <source locate="here">
            todouble
        </source>
        <destination>tosingle</destination>
        <destination>tocomplex</destination>
        <destination>tofcomplex</destination>
        <destination>touint64</destination>
        <destination>toint64</destination>
        <destination>touint32</destination>
        <destination>toint32</destination>
        <destination>touint16</destination>
        <destination>toint16</destination>
        <destination>touint8</destination>
        <destination>toint8</destination>
    </type>
    <type>
        <source locate="here">
            double
        </source>
        <destination>float</destination>
        <destination>complex</destination>
        <destination>fcomplex</destination>
        <destination>ulong</destination>
        <destination>long</destination>
        <destination>uint</destination>
        <destination>int</destination>
        <destination>ushort</destination>
        <destination>short</destination>
        <destination>byte</destination>
        <destination>sbyte</destination>
    </type>
</hycalper>
*/

        [TestMethod]
        public void IteratorDoTests_double_continous() {

            IteratorDoTests(todouble(counter(1.0, 1.0, 3, 4, StorageOrders.ColumnMajor)));
            IteratorDoTests(todouble(counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor)));

            IteratorDoTests(todouble(counter(1.0, 1.0, 1, 4, StorageOrders.ColumnMajor)));
            IteratorDoTests(todouble(counter(1.0, 1.0, 1, 4, StorageOrders.RowMajor)));

            IteratorDoTests(todouble(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(todouble(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor)));

            IteratorDoTests(todouble(counter(1.0, 1.0, 1, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(todouble(counter(1.0, 1.0, 1, 1, StorageOrders.RowMajor)));

            IteratorDoTests(todouble(counter(1.0, 1.0, 0, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(todouble(counter(1.0, 1.0, 0, 1, StorageOrders.RowMajor)));

            IteratorDoTests(todouble(counter(1.0, 1.0, 1, 0, StorageOrders.ColumnMajor)));
            IteratorDoTests(todouble(counter(1.0, 1.0, 1, 0, StorageOrders.RowMajor)));

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> S = (double)99;
                IteratorDoTests(S);

                IteratorDoTests(todouble(counter(1.0, 1.0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(todouble(counter(1.0, 1.0, 5, StorageOrders.RowMajor)));

                IteratorDoTests(todouble(counter(1.0, 1.0, 0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(todouble(counter(1.0, 1.0, 0, 5, StorageOrders.RowMajor)));

            }

        }
        [TestMethod]
        public void IteratorDoTests_double_strided() {

            IteratorDoTests(todouble(counter(1.0, 1.0, 3, 4, StorageOrders.ColumnMajor))[r(1,end-1),full]);
            IteratorDoTests(todouble(counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor))[r(1, end-1), full]);

            IteratorDoTests(todouble(counter(1.0, 1.0, 1, 4, StorageOrders.ColumnMajor))[full, r(1, end)]);
            IteratorDoTests(todouble(counter(1.0, 1.0, 1, 4, StorageOrders.RowMajor))[full, r(1, end)]);

            IteratorDoTests(todouble(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor))[r(1, end), full]);
            IteratorDoTests(todouble(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor))[r(1, end), full]);

            IteratorDoTests(todouble(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor))[r(2,2)]);
            IteratorDoTests(todouble(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor))[r(2, 2)]);

            IteratorDoTests(todouble(counter(1.0, 1.0, 0, 1, StorageOrders.ColumnMajor))[";0"]);
            IteratorDoTests(todouble(counter(1.0, 1.0, 0, 1, StorageOrders.RowMajor))[";0"]);

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> S = (double)99;
                IteratorDoTests(S);

                Settings.ArrayStyle = ArrayStyles.ILNumericsV4;
                IteratorDoTests(todouble(counter(1.0, 1.0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(todouble(counter(1.0, 1.0, 5, StorageOrders.RowMajor))[2]);

                IteratorDoTests(todouble(counter(1.0, 1.0, 0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(todouble(counter(1.0, 1.0, 0, 5, StorageOrders.RowMajor)));

            }

        }
        private void IteratorDoTests(InArray<double> A) {
            using (Scope.Enter(A)) {

                /* tests if 
                * Reset()-ing an iterator places the Current position pointer _before_ the first element
                * Multiple runs are working 
                * Reseting before end is working
                * 
                * Permuting: 
                * all iterator kinds: same type, converting<Tout>, indexiterators
                * column major, row major
                */
                double[] values = null;
                int startRefCount = A.ReferenceCount;

                A.ExportValues(ref values, StorageOrders.ColumnMajor);
                TestIEnumerable(A.Iterator(StorageOrders.ColumnMajor), values);
                TestIEnumerable(A.Iterator<Double>(a => (Double)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (Double)a));
                TestIEnumerable(A.Iterator<float>(a => (float)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (float)a));
                TestIEnumerable(A.Iterator<ulong>(a => (ulong)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (ulong)a));
                TestIEnumerable(A.Iterator<long>(a => (long)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (long)a));
                TestIEnumerable(A.Iterator<uint>(a => (uint)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (uint)a));
                TestIEnumerable(A.Iterator<int>(a => (int)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (int)a));
                TestIEnumerable(A.Iterator<short>(a => (short)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (short)a));
                TestIEnumerable(A.Iterator<ushort>(a => (ushort)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (ushort)a));
                TestIEnumerable(A.Iterator<byte>(a => (byte)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (byte)a));
                TestIEnumerable(A.Iterator<sbyte>(a => (sbyte)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (sbyte)a));
                TestIEnumerable(A.IndexIterator(1000, StorageOrders.ColumnMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));

                A.ExportValues(ref values, StorageOrders.RowMajor);
                TestIEnumerable(A.Iterator(StorageOrders.RowMajor), values);
                TestIEnumerable(A.Iterator<Double>(a => (Double)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (Double)a));
                TestIEnumerable(A.Iterator<float>(a => (float)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (float)a));
                TestIEnumerable(A.Iterator<ulong>(a => (ulong)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (ulong)a));
                TestIEnumerable(A.Iterator<long>(a => (long)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (long)a));
                TestIEnumerable(A.Iterator<uint>(a => (uint)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (uint)a));
                TestIEnumerable(A.Iterator<int>(a => (int)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (int)a));
                TestIEnumerable(A.Iterator<short>(a => (short)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (short)a));
                TestIEnumerable(A.Iterator<ushort>(a => (ushort)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (ushort)a));
                TestIEnumerable(A.Iterator<byte>(a => (byte)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (byte)a));
                TestIEnumerable(A.Iterator<sbyte>(a => (sbyte)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (sbyte)a));
                TestIEnumerable(A.IndexIterator(1000, StorageOrders.RowMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));

            }
        }
        #endregion HYCALPER LOOPEND
#region HYCALPER AUTO GENERATED CODE
// DO NOT EDIT INSIDE THIS REGION !! CHANGES WILL BE LOST !! 
       

        [TestMethod]
        public void IteratorDoTests_sbyte_continous() {

            IteratorDoTests(toint8(counter(1.0, 1.0, 3, 4, StorageOrders.ColumnMajor)));
            IteratorDoTests(toint8(counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor)));

            IteratorDoTests(toint8(counter(1.0, 1.0, 1, 4, StorageOrders.ColumnMajor)));
            IteratorDoTests(toint8(counter(1.0, 1.0, 1, 4, StorageOrders.RowMajor)));

            IteratorDoTests(toint8(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(toint8(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor)));

            IteratorDoTests(toint8(counter(1.0, 1.0, 1, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(toint8(counter(1.0, 1.0, 1, 1, StorageOrders.RowMajor)));

            IteratorDoTests(toint8(counter(1.0, 1.0, 0, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(toint8(counter(1.0, 1.0, 0, 1, StorageOrders.RowMajor)));

            IteratorDoTests(toint8(counter(1.0, 1.0, 1, 0, StorageOrders.ColumnMajor)));
            IteratorDoTests(toint8(counter(1.0, 1.0, 1, 0, StorageOrders.RowMajor)));

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<sbyte> S = (sbyte)99;
                IteratorDoTests(S);

                IteratorDoTests(toint8(counter(1.0, 1.0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(toint8(counter(1.0, 1.0, 5, StorageOrders.RowMajor)));

                IteratorDoTests(toint8(counter(1.0, 1.0, 0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(toint8(counter(1.0, 1.0, 0, 5, StorageOrders.RowMajor)));

            }

        }
        [TestMethod]
        public void IteratorDoTests_sbyte_strided() {

            IteratorDoTests(toint8(counter(1.0, 1.0, 3, 4, StorageOrders.ColumnMajor))[r(1,end-1),full]);
            IteratorDoTests(toint8(counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor))[r(1, end-1), full]);

            IteratorDoTests(toint8(counter(1.0, 1.0, 1, 4, StorageOrders.ColumnMajor))[full, r(1, end)]);
            IteratorDoTests(toint8(counter(1.0, 1.0, 1, 4, StorageOrders.RowMajor))[full, r(1, end)]);

            IteratorDoTests(toint8(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor))[r(1, end), full]);
            IteratorDoTests(toint8(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor))[r(1, end), full]);

            IteratorDoTests(toint8(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor))[r(2,2)]);
            IteratorDoTests(toint8(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor))[r(2, 2)]);

            IteratorDoTests(toint8(counter(1.0, 1.0, 0, 1, StorageOrders.ColumnMajor))[";0"]);
            IteratorDoTests(toint8(counter(1.0, 1.0, 0, 1, StorageOrders.RowMajor))[";0"]);

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<sbyte> S = (sbyte)99;
                IteratorDoTests(S);

                Settings.ArrayStyle = ArrayStyles.ILNumericsV4;
                IteratorDoTests(toint8(counter(1.0, 1.0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(toint8(counter(1.0, 1.0, 5, StorageOrders.RowMajor))[2]);

                IteratorDoTests(toint8(counter(1.0, 1.0, 0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(toint8(counter(1.0, 1.0, 0, 5, StorageOrders.RowMajor)));

            }

        }
        private void IteratorDoTests(InArray<sbyte> A) {
            using (Scope.Enter(A)) {

                /* tests if 
                * Reset()-ing an iterator places the Current position pointer _before_ the first element
                * Multiple runs are working 
                * Reseting before end is working
                * 
                * Permuting: 
                * all iterator kinds: same type, converting<Tout>, indexiterators
                * column major, row major
                */
                sbyte[] values = null;
                int startRefCount = A.ReferenceCount;

                A.ExportValues(ref values, StorageOrders.ColumnMajor);
                TestIEnumerable(A.Iterator(StorageOrders.ColumnMajor), values);
                TestIEnumerable(A.Iterator<Double>(a => (Double)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (Double)a));
                TestIEnumerable(A.Iterator<float>(a => (float)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (float)a));
                TestIEnumerable(A.Iterator<ulong>(a => (ulong)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (ulong)a));
                TestIEnumerable(A.Iterator<long>(a => (long)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (long)a));
                TestIEnumerable(A.Iterator<uint>(a => (uint)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (uint)a));
                TestIEnumerable(A.Iterator<int>(a => (int)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (int)a));
                TestIEnumerable(A.Iterator<short>(a => (short)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (short)a));
                TestIEnumerable(A.Iterator<ushort>(a => (ushort)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (ushort)a));
                TestIEnumerable(A.Iterator<byte>(a => (byte)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (byte)a));
                TestIEnumerable(A.Iterator<sbyte>(a => (sbyte)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (sbyte)a));
                TestIEnumerable(A.IndexIterator(1000, StorageOrders.ColumnMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));

                A.ExportValues(ref values, StorageOrders.RowMajor);
                TestIEnumerable(A.Iterator(StorageOrders.RowMajor), values);
                TestIEnumerable(A.Iterator<Double>(a => (Double)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (Double)a));
                TestIEnumerable(A.Iterator<float>(a => (float)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (float)a));
                TestIEnumerable(A.Iterator<ulong>(a => (ulong)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (ulong)a));
                TestIEnumerable(A.Iterator<long>(a => (long)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (long)a));
                TestIEnumerable(A.Iterator<uint>(a => (uint)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (uint)a));
                TestIEnumerable(A.Iterator<int>(a => (int)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (int)a));
                TestIEnumerable(A.Iterator<short>(a => (short)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (short)a));
                TestIEnumerable(A.Iterator<ushort>(a => (ushort)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (ushort)a));
                TestIEnumerable(A.Iterator<byte>(a => (byte)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (byte)a));
                TestIEnumerable(A.Iterator<sbyte>(a => (sbyte)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (sbyte)a));
                TestIEnumerable(A.IndexIterator(1000, StorageOrders.RowMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));

            }
        }
       

        [TestMethod]
        public void IteratorDoTests_byte_continous() {

            IteratorDoTests(touint8(counter(1.0, 1.0, 3, 4, StorageOrders.ColumnMajor)));
            IteratorDoTests(touint8(counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor)));

            IteratorDoTests(touint8(counter(1.0, 1.0, 1, 4, StorageOrders.ColumnMajor)));
            IteratorDoTests(touint8(counter(1.0, 1.0, 1, 4, StorageOrders.RowMajor)));

            IteratorDoTests(touint8(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(touint8(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor)));

            IteratorDoTests(touint8(counter(1.0, 1.0, 1, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(touint8(counter(1.0, 1.0, 1, 1, StorageOrders.RowMajor)));

            IteratorDoTests(touint8(counter(1.0, 1.0, 0, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(touint8(counter(1.0, 1.0, 0, 1, StorageOrders.RowMajor)));

            IteratorDoTests(touint8(counter(1.0, 1.0, 1, 0, StorageOrders.ColumnMajor)));
            IteratorDoTests(touint8(counter(1.0, 1.0, 1, 0, StorageOrders.RowMajor)));

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<byte> S = (byte)99;
                IteratorDoTests(S);

                IteratorDoTests(touint8(counter(1.0, 1.0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(touint8(counter(1.0, 1.0, 5, StorageOrders.RowMajor)));

                IteratorDoTests(touint8(counter(1.0, 1.0, 0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(touint8(counter(1.0, 1.0, 0, 5, StorageOrders.RowMajor)));

            }

        }
        [TestMethod]
        public void IteratorDoTests_byte_strided() {

            IteratorDoTests(touint8(counter(1.0, 1.0, 3, 4, StorageOrders.ColumnMajor))[r(1,end-1),full]);
            IteratorDoTests(touint8(counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor))[r(1, end-1), full]);

            IteratorDoTests(touint8(counter(1.0, 1.0, 1, 4, StorageOrders.ColumnMajor))[full, r(1, end)]);
            IteratorDoTests(touint8(counter(1.0, 1.0, 1, 4, StorageOrders.RowMajor))[full, r(1, end)]);

            IteratorDoTests(touint8(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor))[r(1, end), full]);
            IteratorDoTests(touint8(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor))[r(1, end), full]);

            IteratorDoTests(touint8(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor))[r(2,2)]);
            IteratorDoTests(touint8(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor))[r(2, 2)]);

            IteratorDoTests(touint8(counter(1.0, 1.0, 0, 1, StorageOrders.ColumnMajor))[";0"]);
            IteratorDoTests(touint8(counter(1.0, 1.0, 0, 1, StorageOrders.RowMajor))[";0"]);

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<byte> S = (byte)99;
                IteratorDoTests(S);

                Settings.ArrayStyle = ArrayStyles.ILNumericsV4;
                IteratorDoTests(touint8(counter(1.0, 1.0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(touint8(counter(1.0, 1.0, 5, StorageOrders.RowMajor))[2]);

                IteratorDoTests(touint8(counter(1.0, 1.0, 0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(touint8(counter(1.0, 1.0, 0, 5, StorageOrders.RowMajor)));

            }

        }
        private void IteratorDoTests(InArray<byte> A) {
            using (Scope.Enter(A)) {

                /* tests if 
                * Reset()-ing an iterator places the Current position pointer _before_ the first element
                * Multiple runs are working 
                * Reseting before end is working
                * 
                * Permuting: 
                * all iterator kinds: same type, converting<Tout>, indexiterators
                * column major, row major
                */
                byte[] values = null;
                int startRefCount = A.ReferenceCount;

                A.ExportValues(ref values, StorageOrders.ColumnMajor);
                TestIEnumerable(A.Iterator(StorageOrders.ColumnMajor), values);
                TestIEnumerable(A.Iterator<Double>(a => (Double)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (Double)a));
                TestIEnumerable(A.Iterator<float>(a => (float)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (float)a));
                TestIEnumerable(A.Iterator<ulong>(a => (ulong)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (ulong)a));
                TestIEnumerable(A.Iterator<long>(a => (long)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (long)a));
                TestIEnumerable(A.Iterator<uint>(a => (uint)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (uint)a));
                TestIEnumerable(A.Iterator<int>(a => (int)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (int)a));
                TestIEnumerable(A.Iterator<short>(a => (short)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (short)a));
                TestIEnumerable(A.Iterator<ushort>(a => (ushort)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (ushort)a));
                TestIEnumerable(A.Iterator<byte>(a => (byte)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (byte)a));
                TestIEnumerable(A.Iterator<sbyte>(a => (sbyte)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (sbyte)a));
                TestIEnumerable(A.IndexIterator(1000, StorageOrders.ColumnMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));

                A.ExportValues(ref values, StorageOrders.RowMajor);
                TestIEnumerable(A.Iterator(StorageOrders.RowMajor), values);
                TestIEnumerable(A.Iterator<Double>(a => (Double)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (Double)a));
                TestIEnumerable(A.Iterator<float>(a => (float)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (float)a));
                TestIEnumerable(A.Iterator<ulong>(a => (ulong)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (ulong)a));
                TestIEnumerable(A.Iterator<long>(a => (long)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (long)a));
                TestIEnumerable(A.Iterator<uint>(a => (uint)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (uint)a));
                TestIEnumerable(A.Iterator<int>(a => (int)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (int)a));
                TestIEnumerable(A.Iterator<short>(a => (short)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (short)a));
                TestIEnumerable(A.Iterator<ushort>(a => (ushort)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (ushort)a));
                TestIEnumerable(A.Iterator<byte>(a => (byte)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (byte)a));
                TestIEnumerable(A.Iterator<sbyte>(a => (sbyte)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (sbyte)a));
                TestIEnumerable(A.IndexIterator(1000, StorageOrders.RowMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));

            }
        }
       

        [TestMethod]
        public void IteratorDoTests_short_continous() {

            IteratorDoTests(toint16(counter(1.0, 1.0, 3, 4, StorageOrders.ColumnMajor)));
            IteratorDoTests(toint16(counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor)));

            IteratorDoTests(toint16(counter(1.0, 1.0, 1, 4, StorageOrders.ColumnMajor)));
            IteratorDoTests(toint16(counter(1.0, 1.0, 1, 4, StorageOrders.RowMajor)));

            IteratorDoTests(toint16(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(toint16(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor)));

            IteratorDoTests(toint16(counter(1.0, 1.0, 1, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(toint16(counter(1.0, 1.0, 1, 1, StorageOrders.RowMajor)));

            IteratorDoTests(toint16(counter(1.0, 1.0, 0, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(toint16(counter(1.0, 1.0, 0, 1, StorageOrders.RowMajor)));

            IteratorDoTests(toint16(counter(1.0, 1.0, 1, 0, StorageOrders.ColumnMajor)));
            IteratorDoTests(toint16(counter(1.0, 1.0, 1, 0, StorageOrders.RowMajor)));

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<short> S = (short)99;
                IteratorDoTests(S);

                IteratorDoTests(toint16(counter(1.0, 1.0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(toint16(counter(1.0, 1.0, 5, StorageOrders.RowMajor)));

                IteratorDoTests(toint16(counter(1.0, 1.0, 0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(toint16(counter(1.0, 1.0, 0, 5, StorageOrders.RowMajor)));

            }

        }
        [TestMethod]
        public void IteratorDoTests_short_strided() {

            IteratorDoTests(toint16(counter(1.0, 1.0, 3, 4, StorageOrders.ColumnMajor))[r(1,end-1),full]);
            IteratorDoTests(toint16(counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor))[r(1, end-1), full]);

            IteratorDoTests(toint16(counter(1.0, 1.0, 1, 4, StorageOrders.ColumnMajor))[full, r(1, end)]);
            IteratorDoTests(toint16(counter(1.0, 1.0, 1, 4, StorageOrders.RowMajor))[full, r(1, end)]);

            IteratorDoTests(toint16(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor))[r(1, end), full]);
            IteratorDoTests(toint16(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor))[r(1, end), full]);

            IteratorDoTests(toint16(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor))[r(2,2)]);
            IteratorDoTests(toint16(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor))[r(2, 2)]);

            IteratorDoTests(toint16(counter(1.0, 1.0, 0, 1, StorageOrders.ColumnMajor))[";0"]);
            IteratorDoTests(toint16(counter(1.0, 1.0, 0, 1, StorageOrders.RowMajor))[";0"]);

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<short> S = (short)99;
                IteratorDoTests(S);

                Settings.ArrayStyle = ArrayStyles.ILNumericsV4;
                IteratorDoTests(toint16(counter(1.0, 1.0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(toint16(counter(1.0, 1.0, 5, StorageOrders.RowMajor))[2]);

                IteratorDoTests(toint16(counter(1.0, 1.0, 0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(toint16(counter(1.0, 1.0, 0, 5, StorageOrders.RowMajor)));

            }

        }
        private void IteratorDoTests(InArray<short> A) {
            using (Scope.Enter(A)) {

                /* tests if 
                * Reset()-ing an iterator places the Current position pointer _before_ the first element
                * Multiple runs are working 
                * Reseting before end is working
                * 
                * Permuting: 
                * all iterator kinds: same type, converting<Tout>, indexiterators
                * column major, row major
                */
                short[] values = null;
                int startRefCount = A.ReferenceCount;

                A.ExportValues(ref values, StorageOrders.ColumnMajor);
                TestIEnumerable(A.Iterator(StorageOrders.ColumnMajor), values);
                TestIEnumerable(A.Iterator<Double>(a => (Double)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (Double)a));
                TestIEnumerable(A.Iterator<float>(a => (float)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (float)a));
                TestIEnumerable(A.Iterator<ulong>(a => (ulong)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (ulong)a));
                TestIEnumerable(A.Iterator<long>(a => (long)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (long)a));
                TestIEnumerable(A.Iterator<uint>(a => (uint)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (uint)a));
                TestIEnumerable(A.Iterator<int>(a => (int)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (int)a));
                TestIEnumerable(A.Iterator<short>(a => (short)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (short)a));
                TestIEnumerable(A.Iterator<ushort>(a => (ushort)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (ushort)a));
                TestIEnumerable(A.Iterator<byte>(a => (byte)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (byte)a));
                TestIEnumerable(A.Iterator<sbyte>(a => (sbyte)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (sbyte)a));
                TestIEnumerable(A.IndexIterator(1000, StorageOrders.ColumnMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));

                A.ExportValues(ref values, StorageOrders.RowMajor);
                TestIEnumerable(A.Iterator(StorageOrders.RowMajor), values);
                TestIEnumerable(A.Iterator<Double>(a => (Double)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (Double)a));
                TestIEnumerable(A.Iterator<float>(a => (float)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (float)a));
                TestIEnumerable(A.Iterator<ulong>(a => (ulong)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (ulong)a));
                TestIEnumerable(A.Iterator<long>(a => (long)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (long)a));
                TestIEnumerable(A.Iterator<uint>(a => (uint)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (uint)a));
                TestIEnumerable(A.Iterator<int>(a => (int)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (int)a));
                TestIEnumerable(A.Iterator<short>(a => (short)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (short)a));
                TestIEnumerable(A.Iterator<ushort>(a => (ushort)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (ushort)a));
                TestIEnumerable(A.Iterator<byte>(a => (byte)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (byte)a));
                TestIEnumerable(A.Iterator<sbyte>(a => (sbyte)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (sbyte)a));
                TestIEnumerable(A.IndexIterator(1000, StorageOrders.RowMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));

            }
        }
       

        [TestMethod]
        public void IteratorDoTests_ushort_continous() {

            IteratorDoTests(touint16(counter(1.0, 1.0, 3, 4, StorageOrders.ColumnMajor)));
            IteratorDoTests(touint16(counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor)));

            IteratorDoTests(touint16(counter(1.0, 1.0, 1, 4, StorageOrders.ColumnMajor)));
            IteratorDoTests(touint16(counter(1.0, 1.0, 1, 4, StorageOrders.RowMajor)));

            IteratorDoTests(touint16(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(touint16(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor)));

            IteratorDoTests(touint16(counter(1.0, 1.0, 1, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(touint16(counter(1.0, 1.0, 1, 1, StorageOrders.RowMajor)));

            IteratorDoTests(touint16(counter(1.0, 1.0, 0, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(touint16(counter(1.0, 1.0, 0, 1, StorageOrders.RowMajor)));

            IteratorDoTests(touint16(counter(1.0, 1.0, 1, 0, StorageOrders.ColumnMajor)));
            IteratorDoTests(touint16(counter(1.0, 1.0, 1, 0, StorageOrders.RowMajor)));

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<ushort> S = (ushort)99;
                IteratorDoTests(S);

                IteratorDoTests(touint16(counter(1.0, 1.0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(touint16(counter(1.0, 1.0, 5, StorageOrders.RowMajor)));

                IteratorDoTests(touint16(counter(1.0, 1.0, 0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(touint16(counter(1.0, 1.0, 0, 5, StorageOrders.RowMajor)));

            }

        }
        [TestMethod]
        public void IteratorDoTests_ushort_strided() {

            IteratorDoTests(touint16(counter(1.0, 1.0, 3, 4, StorageOrders.ColumnMajor))[r(1,end-1),full]);
            IteratorDoTests(touint16(counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor))[r(1, end-1), full]);

            IteratorDoTests(touint16(counter(1.0, 1.0, 1, 4, StorageOrders.ColumnMajor))[full, r(1, end)]);
            IteratorDoTests(touint16(counter(1.0, 1.0, 1, 4, StorageOrders.RowMajor))[full, r(1, end)]);

            IteratorDoTests(touint16(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor))[r(1, end), full]);
            IteratorDoTests(touint16(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor))[r(1, end), full]);

            IteratorDoTests(touint16(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor))[r(2,2)]);
            IteratorDoTests(touint16(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor))[r(2, 2)]);

            IteratorDoTests(touint16(counter(1.0, 1.0, 0, 1, StorageOrders.ColumnMajor))[";0"]);
            IteratorDoTests(touint16(counter(1.0, 1.0, 0, 1, StorageOrders.RowMajor))[";0"]);

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<ushort> S = (ushort)99;
                IteratorDoTests(S);

                Settings.ArrayStyle = ArrayStyles.ILNumericsV4;
                IteratorDoTests(touint16(counter(1.0, 1.0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(touint16(counter(1.0, 1.0, 5, StorageOrders.RowMajor))[2]);

                IteratorDoTests(touint16(counter(1.0, 1.0, 0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(touint16(counter(1.0, 1.0, 0, 5, StorageOrders.RowMajor)));

            }

        }
        private void IteratorDoTests(InArray<ushort> A) {
            using (Scope.Enter(A)) {

                /* tests if 
                * Reset()-ing an iterator places the Current position pointer _before_ the first element
                * Multiple runs are working 
                * Reseting before end is working
                * 
                * Permuting: 
                * all iterator kinds: same type, converting<Tout>, indexiterators
                * column major, row major
                */
                ushort[] values = null;
                int startRefCount = A.ReferenceCount;

                A.ExportValues(ref values, StorageOrders.ColumnMajor);
                TestIEnumerable(A.Iterator(StorageOrders.ColumnMajor), values);
                TestIEnumerable(A.Iterator<Double>(a => (Double)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (Double)a));
                TestIEnumerable(A.Iterator<float>(a => (float)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (float)a));
                TestIEnumerable(A.Iterator<ulong>(a => (ulong)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (ulong)a));
                TestIEnumerable(A.Iterator<long>(a => (long)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (long)a));
                TestIEnumerable(A.Iterator<uint>(a => (uint)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (uint)a));
                TestIEnumerable(A.Iterator<int>(a => (int)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (int)a));
                TestIEnumerable(A.Iterator<short>(a => (short)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (short)a));
                TestIEnumerable(A.Iterator<ushort>(a => (ushort)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (ushort)a));
                TestIEnumerable(A.Iterator<byte>(a => (byte)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (byte)a));
                TestIEnumerable(A.Iterator<sbyte>(a => (sbyte)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (sbyte)a));
                TestIEnumerable(A.IndexIterator(1000, StorageOrders.ColumnMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));

                A.ExportValues(ref values, StorageOrders.RowMajor);
                TestIEnumerable(A.Iterator(StorageOrders.RowMajor), values);
                TestIEnumerable(A.Iterator<Double>(a => (Double)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (Double)a));
                TestIEnumerable(A.Iterator<float>(a => (float)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (float)a));
                TestIEnumerable(A.Iterator<ulong>(a => (ulong)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (ulong)a));
                TestIEnumerable(A.Iterator<long>(a => (long)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (long)a));
                TestIEnumerable(A.Iterator<uint>(a => (uint)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (uint)a));
                TestIEnumerable(A.Iterator<int>(a => (int)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (int)a));
                TestIEnumerable(A.Iterator<short>(a => (short)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (short)a));
                TestIEnumerable(A.Iterator<ushort>(a => (ushort)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (ushort)a));
                TestIEnumerable(A.Iterator<byte>(a => (byte)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (byte)a));
                TestIEnumerable(A.Iterator<sbyte>(a => (sbyte)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (sbyte)a));
                TestIEnumerable(A.IndexIterator(1000, StorageOrders.RowMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));

            }
        }
       

        [TestMethod]
        public void IteratorDoTests_int_continous() {

            IteratorDoTests(toint32(counter(1.0, 1.0, 3, 4, StorageOrders.ColumnMajor)));
            IteratorDoTests(toint32(counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor)));

            IteratorDoTests(toint32(counter(1.0, 1.0, 1, 4, StorageOrders.ColumnMajor)));
            IteratorDoTests(toint32(counter(1.0, 1.0, 1, 4, StorageOrders.RowMajor)));

            IteratorDoTests(toint32(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(toint32(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor)));

            IteratorDoTests(toint32(counter(1.0, 1.0, 1, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(toint32(counter(1.0, 1.0, 1, 1, StorageOrders.RowMajor)));

            IteratorDoTests(toint32(counter(1.0, 1.0, 0, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(toint32(counter(1.0, 1.0, 0, 1, StorageOrders.RowMajor)));

            IteratorDoTests(toint32(counter(1.0, 1.0, 1, 0, StorageOrders.ColumnMajor)));
            IteratorDoTests(toint32(counter(1.0, 1.0, 1, 0, StorageOrders.RowMajor)));

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<int> S = (int)99;
                IteratorDoTests(S);

                IteratorDoTests(toint32(counter(1.0, 1.0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(toint32(counter(1.0, 1.0, 5, StorageOrders.RowMajor)));

                IteratorDoTests(toint32(counter(1.0, 1.0, 0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(toint32(counter(1.0, 1.0, 0, 5, StorageOrders.RowMajor)));

            }

        }
        [TestMethod]
        public void IteratorDoTests_int_strided() {

            IteratorDoTests(toint32(counter(1.0, 1.0, 3, 4, StorageOrders.ColumnMajor))[r(1,end-1),full]);
            IteratorDoTests(toint32(counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor))[r(1, end-1), full]);

            IteratorDoTests(toint32(counter(1.0, 1.0, 1, 4, StorageOrders.ColumnMajor))[full, r(1, end)]);
            IteratorDoTests(toint32(counter(1.0, 1.0, 1, 4, StorageOrders.RowMajor))[full, r(1, end)]);

            IteratorDoTests(toint32(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor))[r(1, end), full]);
            IteratorDoTests(toint32(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor))[r(1, end), full]);

            IteratorDoTests(toint32(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor))[r(2,2)]);
            IteratorDoTests(toint32(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor))[r(2, 2)]);

            IteratorDoTests(toint32(counter(1.0, 1.0, 0, 1, StorageOrders.ColumnMajor))[";0"]);
            IteratorDoTests(toint32(counter(1.0, 1.0, 0, 1, StorageOrders.RowMajor))[";0"]);

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<int> S = (int)99;
                IteratorDoTests(S);

                Settings.ArrayStyle = ArrayStyles.ILNumericsV4;
                IteratorDoTests(toint32(counter(1.0, 1.0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(toint32(counter(1.0, 1.0, 5, StorageOrders.RowMajor))[2]);

                IteratorDoTests(toint32(counter(1.0, 1.0, 0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(toint32(counter(1.0, 1.0, 0, 5, StorageOrders.RowMajor)));

            }

        }
        private void IteratorDoTests(InArray<int> A) {
            using (Scope.Enter(A)) {

                /* tests if 
                * Reset()-ing an iterator places the Current position pointer _before_ the first element
                * Multiple runs are working 
                * Reseting before end is working
                * 
                * Permuting: 
                * all iterator kinds: same type, converting<Tout>, indexiterators
                * column major, row major
                */
                int[] values = null;
                int startRefCount = A.ReferenceCount;

                A.ExportValues(ref values, StorageOrders.ColumnMajor);
                TestIEnumerable(A.Iterator(StorageOrders.ColumnMajor), values);
                TestIEnumerable(A.Iterator<Double>(a => (Double)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (Double)a));
                TestIEnumerable(A.Iterator<float>(a => (float)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (float)a));
                TestIEnumerable(A.Iterator<ulong>(a => (ulong)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (ulong)a));
                TestIEnumerable(A.Iterator<long>(a => (long)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (long)a));
                TestIEnumerable(A.Iterator<uint>(a => (uint)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (uint)a));
                TestIEnumerable(A.Iterator<int>(a => (int)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (int)a));
                TestIEnumerable(A.Iterator<short>(a => (short)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (short)a));
                TestIEnumerable(A.Iterator<ushort>(a => (ushort)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (ushort)a));
                TestIEnumerable(A.Iterator<byte>(a => (byte)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (byte)a));
                TestIEnumerable(A.Iterator<sbyte>(a => (sbyte)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (sbyte)a));
                TestIEnumerable(A.IndexIterator(1000, StorageOrders.ColumnMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));

                A.ExportValues(ref values, StorageOrders.RowMajor);
                TestIEnumerable(A.Iterator(StorageOrders.RowMajor), values);
                TestIEnumerable(A.Iterator<Double>(a => (Double)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (Double)a));
                TestIEnumerable(A.Iterator<float>(a => (float)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (float)a));
                TestIEnumerable(A.Iterator<ulong>(a => (ulong)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (ulong)a));
                TestIEnumerable(A.Iterator<long>(a => (long)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (long)a));
                TestIEnumerable(A.Iterator<uint>(a => (uint)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (uint)a));
                TestIEnumerable(A.Iterator<int>(a => (int)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (int)a));
                TestIEnumerable(A.Iterator<short>(a => (short)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (short)a));
                TestIEnumerable(A.Iterator<ushort>(a => (ushort)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (ushort)a));
                TestIEnumerable(A.Iterator<byte>(a => (byte)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (byte)a));
                TestIEnumerable(A.Iterator<sbyte>(a => (sbyte)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (sbyte)a));
                TestIEnumerable(A.IndexIterator(1000, StorageOrders.RowMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));

            }
        }
       

        [TestMethod]
        public void IteratorDoTests_uint_continous() {

            IteratorDoTests(touint32(counter(1.0, 1.0, 3, 4, StorageOrders.ColumnMajor)));
            IteratorDoTests(touint32(counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor)));

            IteratorDoTests(touint32(counter(1.0, 1.0, 1, 4, StorageOrders.ColumnMajor)));
            IteratorDoTests(touint32(counter(1.0, 1.0, 1, 4, StorageOrders.RowMajor)));

            IteratorDoTests(touint32(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(touint32(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor)));

            IteratorDoTests(touint32(counter(1.0, 1.0, 1, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(touint32(counter(1.0, 1.0, 1, 1, StorageOrders.RowMajor)));

            IteratorDoTests(touint32(counter(1.0, 1.0, 0, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(touint32(counter(1.0, 1.0, 0, 1, StorageOrders.RowMajor)));

            IteratorDoTests(touint32(counter(1.0, 1.0, 1, 0, StorageOrders.ColumnMajor)));
            IteratorDoTests(touint32(counter(1.0, 1.0, 1, 0, StorageOrders.RowMajor)));

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<uint> S = (uint)99;
                IteratorDoTests(S);

                IteratorDoTests(touint32(counter(1.0, 1.0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(touint32(counter(1.0, 1.0, 5, StorageOrders.RowMajor)));

                IteratorDoTests(touint32(counter(1.0, 1.0, 0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(touint32(counter(1.0, 1.0, 0, 5, StorageOrders.RowMajor)));

            }

        }
        [TestMethod]
        public void IteratorDoTests_uint_strided() {

            IteratorDoTests(touint32(counter(1.0, 1.0, 3, 4, StorageOrders.ColumnMajor))[r(1,end-1),full]);
            IteratorDoTests(touint32(counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor))[r(1, end-1), full]);

            IteratorDoTests(touint32(counter(1.0, 1.0, 1, 4, StorageOrders.ColumnMajor))[full, r(1, end)]);
            IteratorDoTests(touint32(counter(1.0, 1.0, 1, 4, StorageOrders.RowMajor))[full, r(1, end)]);

            IteratorDoTests(touint32(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor))[r(1, end), full]);
            IteratorDoTests(touint32(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor))[r(1, end), full]);

            IteratorDoTests(touint32(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor))[r(2,2)]);
            IteratorDoTests(touint32(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor))[r(2, 2)]);

            IteratorDoTests(touint32(counter(1.0, 1.0, 0, 1, StorageOrders.ColumnMajor))[";0"]);
            IteratorDoTests(touint32(counter(1.0, 1.0, 0, 1, StorageOrders.RowMajor))[";0"]);

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<uint> S = (uint)99;
                IteratorDoTests(S);

                Settings.ArrayStyle = ArrayStyles.ILNumericsV4;
                IteratorDoTests(touint32(counter(1.0, 1.0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(touint32(counter(1.0, 1.0, 5, StorageOrders.RowMajor))[2]);

                IteratorDoTests(touint32(counter(1.0, 1.0, 0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(touint32(counter(1.0, 1.0, 0, 5, StorageOrders.RowMajor)));

            }

        }
        private void IteratorDoTests(InArray<uint> A) {
            using (Scope.Enter(A)) {

                /* tests if 
                * Reset()-ing an iterator places the Current position pointer _before_ the first element
                * Multiple runs are working 
                * Reseting before end is working
                * 
                * Permuting: 
                * all iterator kinds: same type, converting<Tout>, indexiterators
                * column major, row major
                */
                uint[] values = null;
                int startRefCount = A.ReferenceCount;

                A.ExportValues(ref values, StorageOrders.ColumnMajor);
                TestIEnumerable(A.Iterator(StorageOrders.ColumnMajor), values);
                TestIEnumerable(A.Iterator<Double>(a => (Double)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (Double)a));
                TestIEnumerable(A.Iterator<float>(a => (float)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (float)a));
                TestIEnumerable(A.Iterator<ulong>(a => (ulong)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (ulong)a));
                TestIEnumerable(A.Iterator<long>(a => (long)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (long)a));
                TestIEnumerable(A.Iterator<uint>(a => (uint)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (uint)a));
                TestIEnumerable(A.Iterator<int>(a => (int)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (int)a));
                TestIEnumerable(A.Iterator<short>(a => (short)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (short)a));
                TestIEnumerable(A.Iterator<ushort>(a => (ushort)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (ushort)a));
                TestIEnumerable(A.Iterator<byte>(a => (byte)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (byte)a));
                TestIEnumerable(A.Iterator<sbyte>(a => (sbyte)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (sbyte)a));
                TestIEnumerable(A.IndexIterator(1000, StorageOrders.ColumnMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));

                A.ExportValues(ref values, StorageOrders.RowMajor);
                TestIEnumerable(A.Iterator(StorageOrders.RowMajor), values);
                TestIEnumerable(A.Iterator<Double>(a => (Double)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (Double)a));
                TestIEnumerable(A.Iterator<float>(a => (float)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (float)a));
                TestIEnumerable(A.Iterator<ulong>(a => (ulong)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (ulong)a));
                TestIEnumerable(A.Iterator<long>(a => (long)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (long)a));
                TestIEnumerable(A.Iterator<uint>(a => (uint)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (uint)a));
                TestIEnumerable(A.Iterator<int>(a => (int)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (int)a));
                TestIEnumerable(A.Iterator<short>(a => (short)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (short)a));
                TestIEnumerable(A.Iterator<ushort>(a => (ushort)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (ushort)a));
                TestIEnumerable(A.Iterator<byte>(a => (byte)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (byte)a));
                TestIEnumerable(A.Iterator<sbyte>(a => (sbyte)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (sbyte)a));
                TestIEnumerable(A.IndexIterator(1000, StorageOrders.RowMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));

            }
        }
       

        [TestMethod]
        public void IteratorDoTests_long_continous() {

            IteratorDoTests(toint64(counter(1.0, 1.0, 3, 4, StorageOrders.ColumnMajor)));
            IteratorDoTests(toint64(counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor)));

            IteratorDoTests(toint64(counter(1.0, 1.0, 1, 4, StorageOrders.ColumnMajor)));
            IteratorDoTests(toint64(counter(1.0, 1.0, 1, 4, StorageOrders.RowMajor)));

            IteratorDoTests(toint64(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(toint64(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor)));

            IteratorDoTests(toint64(counter(1.0, 1.0, 1, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(toint64(counter(1.0, 1.0, 1, 1, StorageOrders.RowMajor)));

            IteratorDoTests(toint64(counter(1.0, 1.0, 0, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(toint64(counter(1.0, 1.0, 0, 1, StorageOrders.RowMajor)));

            IteratorDoTests(toint64(counter(1.0, 1.0, 1, 0, StorageOrders.ColumnMajor)));
            IteratorDoTests(toint64(counter(1.0, 1.0, 1, 0, StorageOrders.RowMajor)));

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<long> S = (long)99;
                IteratorDoTests(S);

                IteratorDoTests(toint64(counter(1.0, 1.0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(toint64(counter(1.0, 1.0, 5, StorageOrders.RowMajor)));

                IteratorDoTests(toint64(counter(1.0, 1.0, 0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(toint64(counter(1.0, 1.0, 0, 5, StorageOrders.RowMajor)));

            }

        }
        [TestMethod]
        public void IteratorDoTests_long_strided() {

            IteratorDoTests(toint64(counter(1.0, 1.0, 3, 4, StorageOrders.ColumnMajor))[r(1,end-1),full]);
            IteratorDoTests(toint64(counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor))[r(1, end-1), full]);

            IteratorDoTests(toint64(counter(1.0, 1.0, 1, 4, StorageOrders.ColumnMajor))[full, r(1, end)]);
            IteratorDoTests(toint64(counter(1.0, 1.0, 1, 4, StorageOrders.RowMajor))[full, r(1, end)]);

            IteratorDoTests(toint64(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor))[r(1, end), full]);
            IteratorDoTests(toint64(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor))[r(1, end), full]);

            IteratorDoTests(toint64(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor))[r(2,2)]);
            IteratorDoTests(toint64(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor))[r(2, 2)]);

            IteratorDoTests(toint64(counter(1.0, 1.0, 0, 1, StorageOrders.ColumnMajor))[";0"]);
            IteratorDoTests(toint64(counter(1.0, 1.0, 0, 1, StorageOrders.RowMajor))[";0"]);

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<long> S = (long)99;
                IteratorDoTests(S);

                Settings.ArrayStyle = ArrayStyles.ILNumericsV4;
                IteratorDoTests(toint64(counter(1.0, 1.0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(toint64(counter(1.0, 1.0, 5, StorageOrders.RowMajor))[2]);

                IteratorDoTests(toint64(counter(1.0, 1.0, 0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(toint64(counter(1.0, 1.0, 0, 5, StorageOrders.RowMajor)));

            }

        }
        private void IteratorDoTests(InArray<long> A) {
            using (Scope.Enter(A)) {

                /* tests if 
                * Reset()-ing an iterator places the Current position pointer _before_ the first element
                * Multiple runs are working 
                * Reseting before end is working
                * 
                * Permuting: 
                * all iterator kinds: same type, converting<Tout>, indexiterators
                * column major, row major
                */
                long[] values = null;
                int startRefCount = A.ReferenceCount;

                A.ExportValues(ref values, StorageOrders.ColumnMajor);
                TestIEnumerable(A.Iterator(StorageOrders.ColumnMajor), values);
                TestIEnumerable(A.Iterator<Double>(a => (Double)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (Double)a));
                TestIEnumerable(A.Iterator<float>(a => (float)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (float)a));
                TestIEnumerable(A.Iterator<ulong>(a => (ulong)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (ulong)a));
                TestIEnumerable(A.Iterator<long>(a => (long)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (long)a));
                TestIEnumerable(A.Iterator<uint>(a => (uint)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (uint)a));
                TestIEnumerable(A.Iterator<int>(a => (int)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (int)a));
                TestIEnumerable(A.Iterator<short>(a => (short)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (short)a));
                TestIEnumerable(A.Iterator<ushort>(a => (ushort)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (ushort)a));
                TestIEnumerable(A.Iterator<byte>(a => (byte)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (byte)a));
                TestIEnumerable(A.Iterator<sbyte>(a => (sbyte)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (sbyte)a));
                TestIEnumerable(A.IndexIterator(1000, StorageOrders.ColumnMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));

                A.ExportValues(ref values, StorageOrders.RowMajor);
                TestIEnumerable(A.Iterator(StorageOrders.RowMajor), values);
                TestIEnumerable(A.Iterator<Double>(a => (Double)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (Double)a));
                TestIEnumerable(A.Iterator<float>(a => (float)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (float)a));
                TestIEnumerable(A.Iterator<ulong>(a => (ulong)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (ulong)a));
                TestIEnumerable(A.Iterator<long>(a => (long)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (long)a));
                TestIEnumerable(A.Iterator<uint>(a => (uint)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (uint)a));
                TestIEnumerable(A.Iterator<int>(a => (int)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (int)a));
                TestIEnumerable(A.Iterator<short>(a => (short)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (short)a));
                TestIEnumerable(A.Iterator<ushort>(a => (ushort)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (ushort)a));
                TestIEnumerable(A.Iterator<byte>(a => (byte)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (byte)a));
                TestIEnumerable(A.Iterator<sbyte>(a => (sbyte)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (sbyte)a));
                TestIEnumerable(A.IndexIterator(1000, StorageOrders.RowMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));

            }
        }
       

        [TestMethod]
        public void IteratorDoTests_ulong_continous() {

            IteratorDoTests(touint64(counter(1.0, 1.0, 3, 4, StorageOrders.ColumnMajor)));
            IteratorDoTests(touint64(counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor)));

            IteratorDoTests(touint64(counter(1.0, 1.0, 1, 4, StorageOrders.ColumnMajor)));
            IteratorDoTests(touint64(counter(1.0, 1.0, 1, 4, StorageOrders.RowMajor)));

            IteratorDoTests(touint64(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(touint64(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor)));

            IteratorDoTests(touint64(counter(1.0, 1.0, 1, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(touint64(counter(1.0, 1.0, 1, 1, StorageOrders.RowMajor)));

            IteratorDoTests(touint64(counter(1.0, 1.0, 0, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(touint64(counter(1.0, 1.0, 0, 1, StorageOrders.RowMajor)));

            IteratorDoTests(touint64(counter(1.0, 1.0, 1, 0, StorageOrders.ColumnMajor)));
            IteratorDoTests(touint64(counter(1.0, 1.0, 1, 0, StorageOrders.RowMajor)));

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<ulong> S = (ulong)99;
                IteratorDoTests(S);

                IteratorDoTests(touint64(counter(1.0, 1.0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(touint64(counter(1.0, 1.0, 5, StorageOrders.RowMajor)));

                IteratorDoTests(touint64(counter(1.0, 1.0, 0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(touint64(counter(1.0, 1.0, 0, 5, StorageOrders.RowMajor)));

            }

        }
        [TestMethod]
        public void IteratorDoTests_ulong_strided() {

            IteratorDoTests(touint64(counter(1.0, 1.0, 3, 4, StorageOrders.ColumnMajor))[r(1,end-1),full]);
            IteratorDoTests(touint64(counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor))[r(1, end-1), full]);

            IteratorDoTests(touint64(counter(1.0, 1.0, 1, 4, StorageOrders.ColumnMajor))[full, r(1, end)]);
            IteratorDoTests(touint64(counter(1.0, 1.0, 1, 4, StorageOrders.RowMajor))[full, r(1, end)]);

            IteratorDoTests(touint64(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor))[r(1, end), full]);
            IteratorDoTests(touint64(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor))[r(1, end), full]);

            IteratorDoTests(touint64(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor))[r(2,2)]);
            IteratorDoTests(touint64(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor))[r(2, 2)]);

            IteratorDoTests(touint64(counter(1.0, 1.0, 0, 1, StorageOrders.ColumnMajor))[";0"]);
            IteratorDoTests(touint64(counter(1.0, 1.0, 0, 1, StorageOrders.RowMajor))[";0"]);

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<ulong> S = (ulong)99;
                IteratorDoTests(S);

                Settings.ArrayStyle = ArrayStyles.ILNumericsV4;
                IteratorDoTests(touint64(counter(1.0, 1.0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(touint64(counter(1.0, 1.0, 5, StorageOrders.RowMajor))[2]);

                IteratorDoTests(touint64(counter(1.0, 1.0, 0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(touint64(counter(1.0, 1.0, 0, 5, StorageOrders.RowMajor)));

            }

        }
        private void IteratorDoTests(InArray<ulong> A) {
            using (Scope.Enter(A)) {

                /* tests if 
                * Reset()-ing an iterator places the Current position pointer _before_ the first element
                * Multiple runs are working 
                * Reseting before end is working
                * 
                * Permuting: 
                * all iterator kinds: same type, converting<Tout>, indexiterators
                * column major, row major
                */
                ulong[] values = null;
                int startRefCount = A.ReferenceCount;

                A.ExportValues(ref values, StorageOrders.ColumnMajor);
                TestIEnumerable(A.Iterator(StorageOrders.ColumnMajor), values);
                TestIEnumerable(A.Iterator<Double>(a => (Double)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (Double)a));
                TestIEnumerable(A.Iterator<float>(a => (float)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (float)a));
                TestIEnumerable(A.Iterator<ulong>(a => (ulong)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (ulong)a));
                TestIEnumerable(A.Iterator<long>(a => (long)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (long)a));
                TestIEnumerable(A.Iterator<uint>(a => (uint)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (uint)a));
                TestIEnumerable(A.Iterator<int>(a => (int)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (int)a));
                TestIEnumerable(A.Iterator<short>(a => (short)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (short)a));
                TestIEnumerable(A.Iterator<ushort>(a => (ushort)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (ushort)a));
                TestIEnumerable(A.Iterator<byte>(a => (byte)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (byte)a));
                TestIEnumerable(A.Iterator<sbyte>(a => (sbyte)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (sbyte)a));
                TestIEnumerable(A.IndexIterator(1000, StorageOrders.ColumnMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));

                A.ExportValues(ref values, StorageOrders.RowMajor);
                TestIEnumerable(A.Iterator(StorageOrders.RowMajor), values);
                TestIEnumerable(A.Iterator<Double>(a => (Double)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (Double)a));
                TestIEnumerable(A.Iterator<float>(a => (float)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (float)a));
                TestIEnumerable(A.Iterator<ulong>(a => (ulong)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (ulong)a));
                TestIEnumerable(A.Iterator<long>(a => (long)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (long)a));
                TestIEnumerable(A.Iterator<uint>(a => (uint)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (uint)a));
                TestIEnumerable(A.Iterator<int>(a => (int)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (int)a));
                TestIEnumerable(A.Iterator<short>(a => (short)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (short)a));
                TestIEnumerable(A.Iterator<ushort>(a => (ushort)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (ushort)a));
                TestIEnumerable(A.Iterator<byte>(a => (byte)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (byte)a));
                TestIEnumerable(A.Iterator<sbyte>(a => (sbyte)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (sbyte)a));
                TestIEnumerable(A.IndexIterator(1000, StorageOrders.RowMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));

            }
        }
       

        [TestMethod]
        public void IteratorDoTests_fcomplex_continous() {

            IteratorDoTests(tofcomplex(counter(1.0, 1.0, 3, 4, StorageOrders.ColumnMajor)));
            IteratorDoTests(tofcomplex(counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor)));

            IteratorDoTests(tofcomplex(counter(1.0, 1.0, 1, 4, StorageOrders.ColumnMajor)));
            IteratorDoTests(tofcomplex(counter(1.0, 1.0, 1, 4, StorageOrders.RowMajor)));

            IteratorDoTests(tofcomplex(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(tofcomplex(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor)));

            IteratorDoTests(tofcomplex(counter(1.0, 1.0, 1, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(tofcomplex(counter(1.0, 1.0, 1, 1, StorageOrders.RowMajor)));

            IteratorDoTests(tofcomplex(counter(1.0, 1.0, 0, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(tofcomplex(counter(1.0, 1.0, 0, 1, StorageOrders.RowMajor)));

            IteratorDoTests(tofcomplex(counter(1.0, 1.0, 1, 0, StorageOrders.ColumnMajor)));
            IteratorDoTests(tofcomplex(counter(1.0, 1.0, 1, 0, StorageOrders.RowMajor)));

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<fcomplex> S = (fcomplex)99;
                IteratorDoTests(S);

                IteratorDoTests(tofcomplex(counter(1.0, 1.0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(tofcomplex(counter(1.0, 1.0, 5, StorageOrders.RowMajor)));

                IteratorDoTests(tofcomplex(counter(1.0, 1.0, 0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(tofcomplex(counter(1.0, 1.0, 0, 5, StorageOrders.RowMajor)));

            }

        }
        [TestMethod]
        public void IteratorDoTests_fcomplex_strided() {

            IteratorDoTests(tofcomplex(counter(1.0, 1.0, 3, 4, StorageOrders.ColumnMajor))[r(1,end-1),full]);
            IteratorDoTests(tofcomplex(counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor))[r(1, end-1), full]);

            IteratorDoTests(tofcomplex(counter(1.0, 1.0, 1, 4, StorageOrders.ColumnMajor))[full, r(1, end)]);
            IteratorDoTests(tofcomplex(counter(1.0, 1.0, 1, 4, StorageOrders.RowMajor))[full, r(1, end)]);

            IteratorDoTests(tofcomplex(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor))[r(1, end), full]);
            IteratorDoTests(tofcomplex(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor))[r(1, end), full]);

            IteratorDoTests(tofcomplex(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor))[r(2,2)]);
            IteratorDoTests(tofcomplex(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor))[r(2, 2)]);

            IteratorDoTests(tofcomplex(counter(1.0, 1.0, 0, 1, StorageOrders.ColumnMajor))[";0"]);
            IteratorDoTests(tofcomplex(counter(1.0, 1.0, 0, 1, StorageOrders.RowMajor))[";0"]);

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<fcomplex> S = (fcomplex)99;
                IteratorDoTests(S);

                Settings.ArrayStyle = ArrayStyles.ILNumericsV4;
                IteratorDoTests(tofcomplex(counter(1.0, 1.0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(tofcomplex(counter(1.0, 1.0, 5, StorageOrders.RowMajor))[2]);

                IteratorDoTests(tofcomplex(counter(1.0, 1.0, 0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(tofcomplex(counter(1.0, 1.0, 0, 5, StorageOrders.RowMajor)));

            }

        }
        private void IteratorDoTests(InArray<fcomplex> A) {
            using (Scope.Enter(A)) {

                /* tests if 
                * Reset()-ing an iterator places the Current position pointer _before_ the first element
                * Multiple runs are working 
                * Reseting before end is working
                * 
                * Permuting: 
                * all iterator kinds: same type, converting<Tout>, indexiterators
                * column major, row major
                */
                fcomplex[] values = null;
                int startRefCount = A.ReferenceCount;

                A.ExportValues(ref values, StorageOrders.ColumnMajor);
                TestIEnumerable(A.Iterator(StorageOrders.ColumnMajor), values);
                TestIEnumerable(A.Iterator<Double>(a => (Double)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (Double)a));
                TestIEnumerable(A.Iterator<float>(a => (float)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (float)a));
                TestIEnumerable(A.Iterator<ulong>(a => (ulong)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (ulong)a));
                TestIEnumerable(A.Iterator<long>(a => (long)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (long)a));
                TestIEnumerable(A.Iterator<uint>(a => (uint)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (uint)a));
                TestIEnumerable(A.Iterator<int>(a => (int)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (int)a));
                TestIEnumerable(A.Iterator<short>(a => (short)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (short)a));
                TestIEnumerable(A.Iterator<ushort>(a => (ushort)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (ushort)a));
                TestIEnumerable(A.Iterator<byte>(a => (byte)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (byte)a));
                TestIEnumerable(A.Iterator<sbyte>(a => (sbyte)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (sbyte)a));
                TestIEnumerable(A.IndexIterator(1000, StorageOrders.ColumnMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));

                A.ExportValues(ref values, StorageOrders.RowMajor);
                TestIEnumerable(A.Iterator(StorageOrders.RowMajor), values);
                TestIEnumerable(A.Iterator<Double>(a => (Double)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (Double)a));
                TestIEnumerable(A.Iterator<float>(a => (float)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (float)a));
                TestIEnumerable(A.Iterator<ulong>(a => (ulong)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (ulong)a));
                TestIEnumerable(A.Iterator<long>(a => (long)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (long)a));
                TestIEnumerable(A.Iterator<uint>(a => (uint)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (uint)a));
                TestIEnumerable(A.Iterator<int>(a => (int)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (int)a));
                TestIEnumerable(A.Iterator<short>(a => (short)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (short)a));
                TestIEnumerable(A.Iterator<ushort>(a => (ushort)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (ushort)a));
                TestIEnumerable(A.Iterator<byte>(a => (byte)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (byte)a));
                TestIEnumerable(A.Iterator<sbyte>(a => (sbyte)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (sbyte)a));
                TestIEnumerable(A.IndexIterator(1000, StorageOrders.RowMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));

            }
        }
       

        [TestMethod]
        public void IteratorDoTests_complex_continous() {

            IteratorDoTests(tocomplex(counter(1.0, 1.0, 3, 4, StorageOrders.ColumnMajor)));
            IteratorDoTests(tocomplex(counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor)));

            IteratorDoTests(tocomplex(counter(1.0, 1.0, 1, 4, StorageOrders.ColumnMajor)));
            IteratorDoTests(tocomplex(counter(1.0, 1.0, 1, 4, StorageOrders.RowMajor)));

            IteratorDoTests(tocomplex(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(tocomplex(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor)));

            IteratorDoTests(tocomplex(counter(1.0, 1.0, 1, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(tocomplex(counter(1.0, 1.0, 1, 1, StorageOrders.RowMajor)));

            IteratorDoTests(tocomplex(counter(1.0, 1.0, 0, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(tocomplex(counter(1.0, 1.0, 0, 1, StorageOrders.RowMajor)));

            IteratorDoTests(tocomplex(counter(1.0, 1.0, 1, 0, StorageOrders.ColumnMajor)));
            IteratorDoTests(tocomplex(counter(1.0, 1.0, 1, 0, StorageOrders.RowMajor)));

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<complex> S = (complex)99;
                IteratorDoTests(S);

                IteratorDoTests(tocomplex(counter(1.0, 1.0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(tocomplex(counter(1.0, 1.0, 5, StorageOrders.RowMajor)));

                IteratorDoTests(tocomplex(counter(1.0, 1.0, 0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(tocomplex(counter(1.0, 1.0, 0, 5, StorageOrders.RowMajor)));

            }

        }
        [TestMethod]
        public void IteratorDoTests_complex_strided() {

            IteratorDoTests(tocomplex(counter(1.0, 1.0, 3, 4, StorageOrders.ColumnMajor))[r(1,end-1),full]);
            IteratorDoTests(tocomplex(counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor))[r(1, end-1), full]);

            IteratorDoTests(tocomplex(counter(1.0, 1.0, 1, 4, StorageOrders.ColumnMajor))[full, r(1, end)]);
            IteratorDoTests(tocomplex(counter(1.0, 1.0, 1, 4, StorageOrders.RowMajor))[full, r(1, end)]);

            IteratorDoTests(tocomplex(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor))[r(1, end), full]);
            IteratorDoTests(tocomplex(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor))[r(1, end), full]);

            IteratorDoTests(tocomplex(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor))[r(2,2)]);
            IteratorDoTests(tocomplex(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor))[r(2, 2)]);

            IteratorDoTests(tocomplex(counter(1.0, 1.0, 0, 1, StorageOrders.ColumnMajor))[";0"]);
            IteratorDoTests(tocomplex(counter(1.0, 1.0, 0, 1, StorageOrders.RowMajor))[";0"]);

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<complex> S = (complex)99;
                IteratorDoTests(S);

                Settings.ArrayStyle = ArrayStyles.ILNumericsV4;
                IteratorDoTests(tocomplex(counter(1.0, 1.0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(tocomplex(counter(1.0, 1.0, 5, StorageOrders.RowMajor))[2]);

                IteratorDoTests(tocomplex(counter(1.0, 1.0, 0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(tocomplex(counter(1.0, 1.0, 0, 5, StorageOrders.RowMajor)));

            }

        }
        private void IteratorDoTests(InArray<complex> A) {
            using (Scope.Enter(A)) {

                /* tests if 
                * Reset()-ing an iterator places the Current position pointer _before_ the first element
                * Multiple runs are working 
                * Reseting before end is working
                * 
                * Permuting: 
                * all iterator kinds: same type, converting<Tout>, indexiterators
                * column major, row major
                */
                complex[] values = null;
                int startRefCount = A.ReferenceCount;

                A.ExportValues(ref values, StorageOrders.ColumnMajor);
                TestIEnumerable(A.Iterator(StorageOrders.ColumnMajor), values);
                TestIEnumerable(A.Iterator<Double>(a => (Double)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (Double)a));
                TestIEnumerable(A.Iterator<float>(a => (float)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (float)a));
                TestIEnumerable(A.Iterator<ulong>(a => (ulong)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (ulong)a));
                TestIEnumerable(A.Iterator<long>(a => (long)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (long)a));
                TestIEnumerable(A.Iterator<uint>(a => (uint)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (uint)a));
                TestIEnumerable(A.Iterator<int>(a => (int)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (int)a));
                TestIEnumerable(A.Iterator<short>(a => (short)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (short)a));
                TestIEnumerable(A.Iterator<ushort>(a => (ushort)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (ushort)a));
                TestIEnumerable(A.Iterator<byte>(a => (byte)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (byte)a));
                TestIEnumerable(A.Iterator<sbyte>(a => (sbyte)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (sbyte)a));
                TestIEnumerable(A.IndexIterator(1000, StorageOrders.ColumnMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));

                A.ExportValues(ref values, StorageOrders.RowMajor);
                TestIEnumerable(A.Iterator(StorageOrders.RowMajor), values);
                TestIEnumerable(A.Iterator<Double>(a => (Double)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (Double)a));
                TestIEnumerable(A.Iterator<float>(a => (float)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (float)a));
                TestIEnumerable(A.Iterator<ulong>(a => (ulong)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (ulong)a));
                TestIEnumerable(A.Iterator<long>(a => (long)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (long)a));
                TestIEnumerable(A.Iterator<uint>(a => (uint)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (uint)a));
                TestIEnumerable(A.Iterator<int>(a => (int)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (int)a));
                TestIEnumerable(A.Iterator<short>(a => (short)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (short)a));
                TestIEnumerable(A.Iterator<ushort>(a => (ushort)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (ushort)a));
                TestIEnumerable(A.Iterator<byte>(a => (byte)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (byte)a));
                TestIEnumerable(A.Iterator<sbyte>(a => (sbyte)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (sbyte)a));
                TestIEnumerable(A.IndexIterator(1000, StorageOrders.RowMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));

            }
        }
       

        [TestMethod]
        public void IteratorDoTests_float_continous() {

            IteratorDoTests(tosingle(counter(1.0, 1.0, 3, 4, StorageOrders.ColumnMajor)));
            IteratorDoTests(tosingle(counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor)));

            IteratorDoTests(tosingle(counter(1.0, 1.0, 1, 4, StorageOrders.ColumnMajor)));
            IteratorDoTests(tosingle(counter(1.0, 1.0, 1, 4, StorageOrders.RowMajor)));

            IteratorDoTests(tosingle(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(tosingle(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor)));

            IteratorDoTests(tosingle(counter(1.0, 1.0, 1, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(tosingle(counter(1.0, 1.0, 1, 1, StorageOrders.RowMajor)));

            IteratorDoTests(tosingle(counter(1.0, 1.0, 0, 1, StorageOrders.ColumnMajor)));
            IteratorDoTests(tosingle(counter(1.0, 1.0, 0, 1, StorageOrders.RowMajor)));

            IteratorDoTests(tosingle(counter(1.0, 1.0, 1, 0, StorageOrders.ColumnMajor)));
            IteratorDoTests(tosingle(counter(1.0, 1.0, 1, 0, StorageOrders.RowMajor)));

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<float> S = (float)99;
                IteratorDoTests(S);

                IteratorDoTests(tosingle(counter(1.0, 1.0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(tosingle(counter(1.0, 1.0, 5, StorageOrders.RowMajor)));

                IteratorDoTests(tosingle(counter(1.0, 1.0, 0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(tosingle(counter(1.0, 1.0, 0, 5, StorageOrders.RowMajor)));

            }

        }
        [TestMethod]
        public void IteratorDoTests_float_strided() {

            IteratorDoTests(tosingle(counter(1.0, 1.0, 3, 4, StorageOrders.ColumnMajor))[r(1,end-1),full]);
            IteratorDoTests(tosingle(counter(1.0, 1.0, 3, 4, StorageOrders.RowMajor))[r(1, end-1), full]);

            IteratorDoTests(tosingle(counter(1.0, 1.0, 1, 4, StorageOrders.ColumnMajor))[full, r(1, end)]);
            IteratorDoTests(tosingle(counter(1.0, 1.0, 1, 4, StorageOrders.RowMajor))[full, r(1, end)]);

            IteratorDoTests(tosingle(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor))[r(1, end), full]);
            IteratorDoTests(tosingle(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor))[r(1, end), full]);

            IteratorDoTests(tosingle(counter(1.0, 1.0, 4, 1, StorageOrders.ColumnMajor))[r(2,2)]);
            IteratorDoTests(tosingle(counter(1.0, 1.0, 4, 1, StorageOrders.RowMajor))[r(2, 2)]);

            IteratorDoTests(tosingle(counter(1.0, 1.0, 0, 1, StorageOrders.ColumnMajor))[";0"]);
            IteratorDoTests(tosingle(counter(1.0, 1.0, 0, 1, StorageOrders.RowMajor))[";0"]);

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<float> S = (float)99;
                IteratorDoTests(S);

                Settings.ArrayStyle = ArrayStyles.ILNumericsV4;
                IteratorDoTests(tosingle(counter(1.0, 1.0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(tosingle(counter(1.0, 1.0, 5, StorageOrders.RowMajor))[2]);

                IteratorDoTests(tosingle(counter(1.0, 1.0, 0, 5, StorageOrders.ColumnMajor)));
                IteratorDoTests(tosingle(counter(1.0, 1.0, 0, 5, StorageOrders.RowMajor)));

            }

        }
        private void IteratorDoTests(InArray<float> A) {
            using (Scope.Enter(A)) {

                /* tests if 
                * Reset()-ing an iterator places the Current position pointer _before_ the first element
                * Multiple runs are working 
                * Reseting before end is working
                * 
                * Permuting: 
                * all iterator kinds: same type, converting<Tout>, indexiterators
                * column major, row major
                */
                float[] values = null;
                int startRefCount = A.ReferenceCount;

                A.ExportValues(ref values, StorageOrders.ColumnMajor);
                TestIEnumerable(A.Iterator(StorageOrders.ColumnMajor), values);
                TestIEnumerable(A.Iterator<Double>(a => (Double)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (Double)a));
                TestIEnumerable(A.Iterator<float>(a => (float)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (float)a));
                TestIEnumerable(A.Iterator<ulong>(a => (ulong)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (ulong)a));
                TestIEnumerable(A.Iterator<long>(a => (long)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (long)a));
                TestIEnumerable(A.Iterator<uint>(a => (uint)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (uint)a));
                TestIEnumerable(A.Iterator<int>(a => (int)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (int)a));
                TestIEnumerable(A.Iterator<short>(a => (short)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (short)a));
                TestIEnumerable(A.Iterator<ushort>(a => (ushort)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (ushort)a));
                TestIEnumerable(A.Iterator<byte>(a => (byte)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (byte)a));
                TestIEnumerable(A.Iterator<sbyte>(a => (sbyte)a, StorageOrders.ColumnMajor), Array.ConvertAll(values, a => (sbyte)a));
                TestIEnumerable(A.IndexIterator(1000, StorageOrders.ColumnMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));

                A.ExportValues(ref values, StorageOrders.RowMajor);
                TestIEnumerable(A.Iterator(StorageOrders.RowMajor), values);
                TestIEnumerable(A.Iterator<Double>(a => (Double)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (Double)a));
                TestIEnumerable(A.Iterator<float>(a => (float)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (float)a));
                TestIEnumerable(A.Iterator<ulong>(a => (ulong)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (ulong)a));
                TestIEnumerable(A.Iterator<long>(a => (long)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (long)a));
                TestIEnumerable(A.Iterator<uint>(a => (uint)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (uint)a));
                TestIEnumerable(A.Iterator<int>(a => (int)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (int)a));
                TestIEnumerable(A.Iterator<short>(a => (short)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (short)a));
                TestIEnumerable(A.Iterator<ushort>(a => (ushort)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (ushort)a));
                TestIEnumerable(A.Iterator<byte>(a => (byte)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (byte)a));
                TestIEnumerable(A.Iterator<sbyte>(a => (sbyte)a, StorageOrders.RowMajor), Array.ConvertAll(values, a => (sbyte)a));
                TestIEnumerable(A.IndexIterator(1000, StorageOrders.RowMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));

            }
        }

#endregion HYCALPER AUTO GENERATED CODE


        [TestMethod]
        public void IteratorResetTest() {

            int[] values = new int[] { 1, 2, 3, 4 };
            Array<int> A1 = values;
            TestIEnumerable(A1.Iterator(), values);
            TestIEnumerable(A1.Iterator(StorageOrders.ColumnMajor), values);
            TestIEnumerable(A1.Iterator(StorageOrders.RowMajor), values);
            Array<int> AM = A1.Reshape(2, 2, StorageOrders.ColumnMajor);
            TestIEnumerable(AM.Iterator(StorageOrders.ColumnMajor), values);
            TestIEnumerable(AM.Iterator(StorageOrders.RowMajor), new int[] { 1, 3, 2, 4 });
            TestIEnumerable(AM.T.Iterator(StorageOrders.ColumnMajor), new int[] { 1, 3, 2, 4 });
            TestIEnumerable(AM.T.Iterator(StorageOrders.RowMajor), values);

            // index iterators tests
            TestIEnumerable(A1.IndexIterator(lastDimensionIdx: 4), Array.ConvertAll(values, a => (long)a));
            TestIEnumerable(A1.IndexIterator(4, StorageOrders.ColumnMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));
            TestIEnumerable(A1.IndexIterator(4, StorageOrders.RowMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));
            TestIEnumerable(A1.IndexIterator(100, StorageOrders.ColumnMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));
            TestIEnumerable(A1.IndexIterator(100, StorageOrders.RowMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));
            TestIEnumerable(AM.IndexIterator(4, StorageOrders.ColumnMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));
            TestIEnumerable(AM.IndexIterator(4, StorageOrders.RowMajor, keepAlive: false), new long[] { 1, 3, 2, 4 });
            TestIEnumerable(AM.T.IndexIterator(4, StorageOrders.ColumnMajor, keepAlive: false), new long[] { 1, 3, 2, 4 });
            TestIEnumerable(AM.T.IndexIterator(4, StorageOrders.RowMajor, keepAlive: false), Array.ConvertAll(values, a => (long)a));

            // DimSpec tests
            DimSpec r0;
            r0 = r(1, 4); r0.Evaluate(4); TestIEnumerable(r0, Array.ConvertAll(values, a => (long)a));
            r0 = r(1, end); r0.Evaluate(4); TestIEnumerable(r0, Array.ConvertAll(values, a => (long)a));
            r0 = r(1, -1); r0.Evaluate(4); TestIEnumerable(r0, Array.ConvertAll(values, a => (long)a));
            r0 = r(-4, -1); r0.Evaluate(4); TestIEnumerable(r0, Array.ConvertAll(values, a => (long)a));
            r0 = r(-4, 1, -1); r0.Evaluate(4); TestIEnumerable(r0, Array.ConvertAll(values, a => (long)a));
            r0 = r(1, -1); r0.Evaluate(4); TestIEnumerable(r0, Array.ConvertAll(values, a => (long)a));
            r0 = r(end - 3, -1); r0.Evaluate(4); TestIEnumerable(r0, Array.ConvertAll(values, a => (long)a));

            // slice tests
            DimSpec s0;
            s0 = slice(1, 5); s0.Evaluate(4); TestIEnumerable(s0, Array.ConvertAll(values, a => (long)a));
            s0 = slice(1, end + 1); s0.Evaluate(4); TestIEnumerable(s0, Array.ConvertAll(values, a => (long)a));
            s0 = slice(1, -1); s0.Evaluate(5); TestIEnumerable(s0, Array.ConvertAll(values, a => (long)a));
            s0 = slice(-5, -1); s0.Evaluate(5); TestIEnumerable(s0, Array.ConvertAll(values, a => (long)a));
            s0 = slice(-5, -1, 1); s0.Evaluate(5); TestIEnumerable(s0, Array.ConvertAll(values, a => (long)a));
            s0 = slice(1, -1); s0.Evaluate(5); TestIEnumerable(s0, Array.ConvertAll(values, a => (long)a));
            s0 = slice(end - 4, -1); s0.Evaluate(5); TestIEnumerable(s0, Array.ConvertAll(values, a => (long)a));

            // string iterator tests
            TestIEnumerable("1,2,3,4".AsIndices(100), Array.ConvertAll(values, a => (long)a));
            TestIEnumerable("1:4".AsIndices(100), Array.ConvertAll(values, a => (long)a));
            TestIEnumerable("1:end".AsIndices(4), Array.ConvertAll(values, a => (long)a));
            TestIEnumerable("1:-1".AsIndices(4), Array.ConvertAll(values, a => (long)a));
            TestIEnumerable("-4:-1".AsIndices(4), Array.ConvertAll(values, a => (long)a));
            TestIEnumerable("-4:1:-1".AsIndices(4), Array.ConvertAll(values, a => (long)a));
            TestIEnumerable(",1:-1".AsIndices(4), Array.ConvertAll(values, a => (long)a));
            TestIEnumerable("1:-1,".AsIndices(4), Array.ConvertAll(values, a => (long)a));
            TestIEnumerable("1:-1,,".AsIndices(4), Array.ConvertAll(values, a => (long)a));
            TestIEnumerable(",1:-1,,".AsIndices(4), Array.ConvertAll(values, a => (long)a));
            TestIEnumerable(", 1:-1,,".AsIndices(4), Array.ConvertAll(values, a => (long)a));
            TestIEnumerable(", 1: -1 , , ".AsIndices(4), Array.ConvertAll(values, a => (long)a));

        }

        [TestMethod]
        public void IteratorScalarResetTest() {

            // tests if Reset()-ing an iterator places the Current position pointer _before_ the first element

            int[] values = new int[] { 2 };
            Array<int> A1 = values;
            TestIEnumerable(A1.Iterator(), values);
            TestIEnumerable(A1.IndexIterator(lastDimensionIdx: 10), Array.ConvertAll(values, a => (long)a));
            TestIEnumerable(A1.Iterator<uint>(a => (uint)a), Array.ConvertAll(values, a => (uint)a));
            TestIEnumerable(A1.Iterator<float>(a => (float)a), Array.ConvertAll(values, a => (float)a));
        }

        [TestMethod]
        public void IndexIteratorBaseOffsetTests() {

            Array<ulong> A = touint64(counter(1.0, 1.0, 7, 13));
            Array<ulong> B = A["2:2:", "1:3:"];

            Assert.IsTrue(A.S.StorageOrder == StorageOrders.ColumnMajor);
            Assert.IsTrue(B.S.BaseOffset == 9 && B.S.StorageOrder == StorageOrders.Other);
            Assert.IsTrue(B.S[0] == 3);
            Assert.IsTrue(B.S[1] == 4);

            Array<float> I = tosingle(counter(-2.0, 1.0, 1, 6));

            Assert.IsTrue(B[":", "-2,-1,0,1,2,3"].Equals(B[":", I]));
            Assert.IsTrue(B[":", "end,1,3"].Equals(B[":", I["1:2:"]]));
            Assert.IsTrue(B[":", "end,1,3"].Equals(B[":", I["1:2:"].T]));

            B.Storage.EnsureStorageOrder(StorageOrders.RowMajor); 
            Assert.IsTrue(B[":", "-2,-1,0,1,2,3"].Equals(B[":", I]));
            Assert.IsTrue(B[":", "end,1,3"].Equals(B[":", I["1:2:"]]));
            Assert.IsTrue(B[":", "end,1,3"].Equals(B[":", I["1:2:"].T]));

            Assert.IsTrue(B[":", "end,1,3"].Equals(B[":", todouble(I["1:2:"])]));
            Assert.IsTrue(B[":", "end,1,3"].Equals(B[":", tosingle(I["1:2:"])]));
            //Assert.IsTrue(B[":", "end,1,3"].Equals(B[":", touint8(I["1:2:"])]));
            //Assert.IsTrue(B[":", "end,1,3"].Equals(B[":", touint16(I["1:2:"])]));
            //Assert.IsTrue(B[":", "end,1,3"].Equals(B[":", touint32(I["1:2:"])]));
            //Assert.IsTrue(B[":", "end,1,3"].Equals(B[":", touint64(I["1:2:"])]));
            Assert.IsTrue(B[":", "end,1,3"].Equals(B[":", toint8(I["1:2:"])]));
            Assert.IsTrue(B[":", "end,1,3"].Equals(B[":", toint16(I["1:2:"])]));
            Assert.IsTrue(B[":", "end,1,3"].Equals(B[":", toint32(I["1:2:"])]));
            Assert.IsTrue(B[":", "end,1,3"].Equals(B[":", toint64(I["1:2:"])]));
            Assert.IsTrue(B[":", "end,1,3"].Equals(B[":", toint64(I["1:2:"])]));
            Assert.IsTrue(B[":", "end,1,3"].Equals(B[":", toint64(I["1:2:"])]));
            Assert.IsTrue(B[":", "end,1,3"].Equals(B[":", toint64(I["1:2:"])]));

        }

        [TestMethod]
        public void EqualsReleasesAllInputsAndMiscMemoryManagement() {

            Array<ulong> A = touint64(counter(1.0, 1.0, 7, 13));
            Array<ulong> B = A["2:2:", "1:3:"];

            Assert.IsFalse(object.ReferenceEquals(B.Storage, A.Storage)); 
            Assert.IsTrue(object.ReferenceEquals(B.Storage.m_handles, A.Storage.m_handles)); 

            Array<float> I = tosingle(counter(-2.0, 1.0, 1, 6));
            var rs_I12_RetT = I["1:2:"];
            Assert.IsTrue(rs_I12_RetT is Array<float>);
            Assert.IsTrue(object.ReferenceEquals(rs_I12_RetT.Storage.m_handles, I.Storage.m_handles));

            var rs_I12T_RetT = rs_I12_RetT.T;
            Assert.IsTrue(rs_I12T_RetT is Array<float>);
            Assert.IsTrue(object.ReferenceEquals(rs_I12T_RetT.Storage.m_handles, I.Storage.m_handles));

            var rs_BSub = B[":", rs_I12T_RetT];
            Assert.IsTrue(rs_BSub is Array<ulong>);
            Assert.IsTrue(!object.ReferenceEquals(rs_BSub.Storage.m_handles, A.Storage.m_handles));

            var ls_RetT = B[":", "end,1,3"]; // counter(-2.0, 1.0, 1, 6)["1:2:"]
            Assert.IsTrue(ls_RetT is Array<ulong>);
            Assert.IsTrue(!object.ReferenceEquals(ls_RetT.Storage.m_handles, A.Storage.m_handles));

            // perform Equals
            Assert.IsTrue(ls_RetT.Equals(rs_BSub));

        }

        [TestMethod]
        public void IndexIteratorOORFailsBaseArray() {

            Array<double> A = 10;
            testOORIsThrown1D<double,double>(A, 1);
            testOORIsThrown1D<double,int>(A, 1);
            testOORIsThrown1D<double,uint>(A, 1);
            testOORIsThrown1D<double,long>(A, 1);
            testOORIsThrown1D<double,ulong>(A, 1);
            testOORIsThrown1D<double,short>(A, 1);
            testOORIsThrown1D<double,ushort>(A, 1);
            testOORIsThrown1D<double,float>(A, 1);
            testOORIsThrown1D<double,sbyte>(A, 1);
            testOORIsThrown1D<double,byte>(A, 1);

            testOORIsThrown2D<double,double>(A, 1);
            testOORIsThrown2D<double,int>(A, 1);
            testOORIsThrown2D<double,uint>(A, 1);
            testOORIsThrown2D<double,long>(A, 1);
            testOORIsThrown2D<double,ulong>(A, 1);
            testOORIsThrown2D<double,short>(A, 1);
            testOORIsThrown2D<double,ushort>(A, 1);
            testOORIsThrown2D<double,float>(A, 1);
            testOORIsThrown2D<double,sbyte>(A, 1);
            testOORIsThrown2D<double,byte>(A, 1);
        }
        void testOORIsThrown1D<T1, T2>(InArray<T1> A, InArray<T2> I) {

            try {
                Array<T1> B = A[I];
            } catch (IndexOutOfRangeException) {
                return; 
            }
            throw new AssertFailedException("OOR expected: 2D");
        }
        void testOORIsThrown2D<T1, T2>(InArray<T1> A, InArray<T2> I) {

            try {
                Array<T1> B = A[0, I];
            } catch (IndexOutOfRangeException) {
                try {
                    Array<T1> B = A[I, 0];
                } catch (IndexOutOfRangeException) {
                    return;
                }
            }
            throw new AssertFailedException("OOR expected: 2D"); 
        }


        private void TestIEnumerable<T>(IEnumerable<T> obj, T[] values) {

            var it = obj.GetEnumerator(); 

            Assert.IsTrue(values != null); 
            if (values.Length == 0) {
                Assert.IsFalse(it.MoveNext());
                it.Reset();
                Assert.IsFalse(it.MoveNext());
                return; 
            }

            int pos = 0; 
            while (it.MoveNext()) {
                Assert.IsTrue(it.Current.ToString() == values[pos].ToString());
                pos++; 
            }
            Assert.IsTrue(pos == values.Length);

            it.Reset(); // must set pointer before first element

            pos = 0;
            while (it.MoveNext() && pos < values.Length - 1) {
                Assert.IsTrue(it.Current.ToString() == values[pos].ToString());
                pos++;
            }
            Assert.IsTrue(pos == values.Length - 1);

            it.Reset(); // reset before end
            it.MoveNext();
            Assert.IsTrue(it.Current.ToString() == values[0].ToString());

        }
    }
}
