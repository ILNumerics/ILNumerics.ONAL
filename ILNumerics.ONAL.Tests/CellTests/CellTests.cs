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
using ILNumerics.Core.MemoryLayer;
using ILNumerics.Core.StorageLayer;
using static ILNumerics.Globals;
using static ILNumerics.ILMath;

// ILN(enabled=false)  

// most cell tests are designed for testing internals of
// cell mechanisms - without taking into account that cells store _accelerated_
// storages and allow them to be completed after storage into the cell. They 
// utilize the asynch features in Clone(). However, reference counting may not 
// give the expected results and one may observe more storages to be created.
// Thus, we disable acceleration for these tests here.

namespace ILNumerics.UnitTests.Legacy_Tests {

    [TestClass]
    public class CellTests {


        [TestMethod]
        public void Cell_DeepIndexing_GetIndx() {

            Cell C = cell(size(2, 1));
            C[1] = cell(arrays: 
                            vector<BaseArray>(counter<double>(1,1, 5,4), 
                            10u,                // this creates a <double> array [1,1]
                            vector<uint>(10u)   // this creates an <uint> array [1,1] 
                            ));

            // use deep indexing to retrieve values from inner cell
            Cell R = C[1, 0, 0, 0, 0, 1];  // double 0
            Cell S = C[1, 0, 1, 0, 0, 0];  //  10u
            Cell T = C[1, 0, 2, 0, 0, 0];  //  10u

            Assert.IsTrue(R.IsScalar);
            Assert.IsTrue(S.IsScalar);
            Assert.IsTrue(T.IsScalar);

            Assert.IsTrue(R.GetValue<double>(0) == 6);
            Assert.IsTrue(S.GetValue<double>(0) == 10u);
            Assert.IsTrue(T.GetValue<uint>(0) == 10u);

            Assert.IsTrue(C.GetValue<double>(1, 0, 0, 0, 0, 1) == 6);
            Assert.IsTrue(C.GetValue<double>(1, 0, 1, 0, 0, 0) == 10u);
            Assert.IsTrue(C.GetValue<uint>(1, 0, 2, 0, 0, 0) == 10u);

            // ... and without deep indexing: 
            Assert.IsTrue(C.GetCell(1).GetArray<double>(0, 0).GetValue(0, 1) == 6);
            Assert.IsTrue(C.GetCell(1).GetArray<double>(1, 0).GetValue(0, 0) == 10u);
            Assert.IsTrue(C.GetCell(1).GetArray<uint>(2, 0).GetValue(0, 0) == 10u);



        }
        [TestMethod]
        public void Cell_simple() {

            Cell c = cell(size(3), vector<BaseArray>(1, 2, 3));
            Cell c2 = c.C; // this will trigger Detach() at next SetValue() below

            Array<long> I = vector<long>(10, -99);
            c.SetValue(I, 1);

            c.SetValue(I + 1, 1, 1);
            I[-1] = 1000;

            Assert.IsTrue(c.GetValue(0).ToArray<double>() == 1);
            Assert.IsTrue(c.GetValue(1, 0L).ToArray<long>().Equals(vector<long>(10, -99)));
            Assert.IsTrue(c.GetValue(2, 0, 0, 0u).ToArray<double>() == 3);

            Assert.IsTrue(isnull(c.GetValue(0, 1).ToArray<double>()));
            Assert.IsTrue(c.GetValue(1, 1L, 0).ToArray<long>().Equals(vector<long>(10) + 1));
            Assert.IsTrue(c.GetArray<long>(1, 1L).Equals(vector<long>(10, -99) + 1));
            Assert.IsTrue(c.GetArray<long>(1, 1u).Equals(vector<long>(10, -99) + 1));
            Assert.IsTrue(isnull(c.GetArray<double>(2, 1)));
            Assert.IsTrue(isnull(c.GetValue(2, 1).ToArray<double>()));
            Assert.IsTrue(c.GetValue(1, 0) is Array<long>);
            Assert.IsTrue(isnull(c.GetArray<double>(2, 1L).ToArray<double>()));

            Assert.IsTrue(c.GetArray<double>(0) == 1);
            Assert.IsTrue(c.GetArray<long>(size(1, 0L)).Equals(vector<long>(10, -99)));
            Assert.IsTrue(c.GetArray<double>(size(2, 0, 0, 0u)) == 3);

            Assert.IsTrue(isnull(c.GetArray<double>(size(0, 1))));
            Assert.IsTrue(c.GetArray<long>(size(1, 1L)).Equals(vector<long>(10, -99) + 1));
            Assert.IsTrue(c.GetValue(0).IsOfType<double>());
            Assert.IsTrue(c.GetValue(1).IsOfType<long>());
            Assert.IsTrue(c.GetValue(2).IsOfType<double>());
            Assert.IsTrue(c.GetValue(4).IsOfType<long>());
            Assert.IsTrue(isnull(c.GetArray<double>(size(2, 1u))));

        }
        [TestMethod]
        public void Cell_docu001() {

            Array<double> A = rand(10, 20, 30);
            var vec = vector<BaseArray>(A, A + 1, zeros<float>(2, 3), "4th element"); 
            Cell C = cell(size(3,2), vec);

            Assert.IsTrue(C.GetValue(0).Equals(A));
            Assert.IsTrue(C.GetArray<double>(0).Equals(A));

            Assert.IsTrue(C.GetValue(1).Equals(A + 1));
            Assert.IsTrue(C.GetArray<double>(1).Equals(A + 1));

            Assert.IsTrue(C.GetValue(2).Equals(zeros<float>(2,3)));
            Assert.IsTrue(C.GetArray<float>(2).Equals(zeros<float>(2,3)));

            Assert.IsTrue(C.GetValue(3).Equals("4th element"));
            Assert.IsTrue(C.GetArray<string>(3).Equals("4th element"));

            Assert.IsTrue(isnull(C.GetValue(4))); 
            Assert.IsTrue(isnull(C.GetValue(5)));

            Array<double> B = A.C; 
            A[0] = -1;


            Assert.IsTrue(C.GetValue(0).Equals(B));
            Assert.IsFalse(A.Equals(B));
            Assert.IsTrue(C.GetArray<double>(0).Equals(B));


            Assert.IsTrue(C.S[0] == 3); 
            Assert.IsTrue(C.S[1] == 2);
            Assert.IsTrue(C.S.NumberOfDimensions == 2); 
        }
        [TestMethod]
        public void Cell_deepIndexing_NPScalar() {
            using (Scope.Enter(ArrayStyles.numpy)) {

                Array<double> A = vector<double>(1, 2, 3);
                Cell C = A;  // np scalar
                Assert.IsTrue(C.S.NumberOfDimensions == 0);

                Cell B = C[0, 0, 0, 0]; // -> 1
                Assert.IsTrue(B.GetValue<double>(0) == 1);
                
                Array<double> D = A.GetValue<double>(1); // -> 2

                Assert.IsTrue(D.GetValue(0) == 2); 

            }
        }

        [TestMethod]
        public void Cell_SetValue_deepIndexing() {

            Cell C = cell(size(4, 3));
            C.SetRange(cellv(-1), full);
            Cell D = cell(size(10));
            D.SetValue("D0", 0);
            D.SetValue("D1", 1);

            Array<string> D2s = new string[] { "D2_0", "D2_1" };
            D.SetValue(D2s, 2);

            C.SetRange(cellv(D), end);

            Assert.IsTrue(C.GetValue<string>(3, 2, 1, 0) == "D1");
            Array<string> Set = "set";

            C.SetValue(Set, 3, 2, 1, 0);
            Assert.IsTrue(C.GetValue<string>(3, 2, 1, 0) == "set");

            // sets a cell into an inner cell element
            C.SetValue(cellv(D), 3, 2, 3, 0); // not from implicit cast -> tests CellStorage.SetValue ref counting
            Assert.IsTrue(C.GetValue<string>(3, 2, 3, 0, 0, 0, 2) == "D2_0");
            // repeating = delete old + store new -> ref count the same?
            C.SetValue(cellv(D), 3, 2, 3, 0); // not from implicit cast -> tests CellStorage.SetValue ref counting
            Assert.IsTrue(C.GetValue<string>(3, 2, 3, 0, 0, 0, 2) == "D2_0");

            // now store it w/o the wrapping cellv()
            C.SetValue(D, 3, 2, 3, 0);
            Assert.IsTrue(C.GetValue<string>(3, 2, 3, 0, 2, 0, 0) == "D2_0"); // peek into cell into cell into cell into strin array 
            Assert.IsTrue(C.GetValue<string>(3, 2, 3, 0, 2, 0, 1) == "D2_1");

            // store array of value type into cell, check: detached, ref count
            Array<double> A = counter<double>(1.0, 1.0, 55, 55, StorageOrders.ColumnMajor);
            C.SetValue(A[full, r(0, end / 2)], 3, 2, 4, 0);
            Assert.IsTrue(A[end, 20] == 1155);
            Assert.IsTrue(C.GetValue<double>(3, 2, 4, 0, 54, 20) == 1155);

            A[end, 20] = -99;
            Assert.IsTrue(A[end, 20] == -99);
            Assert.IsTrue(C.GetValue<double>(3, 2, 4, 0, 54, 20) == 1155);

            // store array of REF type into cell, check: detached, ref count
            var v = vector("0", "1", "2", "3", "4", "5");
            var t = v.T;

            Array<string> R = t;

            C.SetValue(R[full, r(0, end / 2)], 3, 2, 5, 0);
            Assert.IsTrue(R.GetValue(2) == "2");
            Assert.IsTrue(C.GetValue<string>(3, 2, 5, 0, 0, 2) == "2");

            R[end, 2] = "-99";
            Assert.IsTrue(R.GetValue(2) == "-99");
            Assert.IsTrue(C.GetValue<string>(3, 2, 5, 0, 0, 2) == "2");

            // change an entry in the string array inside the cell
            C.SetValue("newD2_1", 3, 2, 3, 0, 2, 0, 1);

            Assert.IsTrue(C.GetValue<string>(3, 2, 3, 0, 2, 0, 1) == "newD2_1");
            C.SetValue("hey!", 3, 2, 3, 0, 2, 0, 1);
            Assert.IsTrue(C.GetValue<string>(3, 2, 3, 0, 2, 0, 1) == "hey!");

        }
        [TestMethod]
        public void Cell_SetIndexInt_deepIndexing() {

            Cell C = cell(size(4, 3));
            C[full] = -1;
            Cell D = cell(size(10));
            D[0] = "D0";
            D[1] = "D1";
            Array<string> D2s = new string[] { "D2_0", "D2_1" };
            D[2] = D2s;

            Cell illegal = cellv(D);  // was: RetCell illegal = ... 

            C[end] = illegal;

            Assert.IsTrue(C.GetValue<string>(3, 2, 1, 0) == "D1");
            Array<string> Set = "set";

            C[3, 2, 1, 0] = Set;
            Assert.IsTrue(C.GetValue<string>(3, 2, 1, 0) == "set");

            // sets a cell into an inner cell element
            C[3, 2, 3, 0] = cellv(D); // not from implicit cast -> tests CellStorage.SetValue ref counting
            Assert.IsTrue(C.GetValue<string>(3, 2, 3, 0, 0, 0, 2) == "D2_0");
            // repeating = delete old + store new -> ref count the same?
            C[3, 2, 3, 0] = cellv(D); // not from implicit cast -> tests CellStorage.SetValue ref counting
            Assert.IsTrue(C.GetValue<string>(3, 2, 3, 0, 0, 0, 2) == "D2_0");

            // now store it w/o the wrapping cellv()
            C[3, 2, 3, 0] = D;
            Assert.IsTrue(C.GetValue<string>(3, 2, 3, 0, 2, 0, 0) == "D2_0"); // peek into cell into cell into cell into strin array 
            Assert.IsTrue(C.GetValue<string>(3, 2, 3, 0, 2, 0, 1) == "D2_1");

            // store array of value type into cell, check: detached, ref count
            Array<double> A = counter<double>(1.0, 1.0, 55, 55, StorageOrders.ColumnMajor);
            C[3, 2, 4, 0] = A[full, r(0, end / 2)];
            Assert.IsTrue(A[end, 20] == 1155);
            Assert.IsTrue(C.GetValue<double>(3, 2, 4, 0, 54, 20) == 1155);

            A[end, 20] = -99;
            Assert.IsTrue(A[end, 20] == -99);
            Assert.IsTrue(C.GetValue<double>(3, 2, 4, 0, 54, 20) == 1155);

            // store array of REF type into cell, check: detached, ref count
            var v = vector("0", "1", "2", "3", "4", "5");
            var t = v.T;

            Array<string> R = t;

            C[3, 2, 5, 0] = R[full, r(0, end / 2)];
            Assert.IsTrue(R.GetValue(2) == "2");
            Assert.IsTrue(C.GetValue<string>(3, 2, 5, 0, 0, 2) == "2");

            R[end, 2] = "-99";
            Assert.IsTrue(R.GetValue(2) == "-99");
            Assert.IsTrue(C.GetValue<string>(3, 2, 5, 0, 0, 2) == "2");

            // change an entry in the string array inside the cell
            C[3, 2, 3, 0, 2, 0, 1] = "newD2_1";

            Assert.IsTrue(C.GetValue<string>(3, 2, 3, 0, 2, 0, 1) == "newD2_1");
            C[3, 2, 3, 0, 2, 0, 1] = "hey!";
            Assert.IsTrue(C.GetValue<string>(3, 2, 3, 0, 2, 0, 1) == "hey!");

        }
        [TestMethod]
        public void Cell_npGetValue_deepIndx_lessDims() {

            using (Scope.Enter(ArrayStyles.numpy)) {

                Cell C = cell(size(4, 3));
                C[full] = -1;
                Cell D = cell(size(10));
                D[0] = "D0";
                D[1] = "D1";
                C[end] = cellv(D);
//C
//Cell [4,3]
//[0]:           
//[1]:              {<Int32> []}             {<Int32> []}             {<Int32> []}
//[2]:              {<Int32> []}             {<Int32> []}             {<Int32> []}
//[3]:              {<Int32> []}             {<Int32> []}             {<Int32> []}
//[4]:               {Cell [10]}              {Cell [10]}              {Cell [10]}

                Cell E = C[3, 2, 1, 0];
                Assert.IsTrue(E.GetValue<string>(0) == "D1");

                Assert.IsTrue(C.Subarray(3,2,0,0).GetArray<string>(0).Equals("D0"));
                Assert.IsTrue(C.Subarray(3,2,0,0).GetArray<string>(1).Equals("D1"));

                Cell F = C.Subarray("3", r(2,2));                       // fetches: Cell [1,1] { Cell [10] }
                Assert.IsTrue(F.GetArray<string>(0,0,0).Equals("D0"));  // fetches: 0-dim scalar <string> [] { "D0" }
                Assert.IsTrue(F.GetArray<string>(0,0,1).Equals("D1"));  // fetches: 0-dim scalar <string> [] { "D1" }

            }
        }
        [TestMethod]
        public void Cellv_simple() {
            Array<double> A = 1.0;

            Cell C = A;
            Cell D = C.C;

            Cell E = cellv(A, C, D);
            Cell F = cellv(A, C, D, null).Reshape(2, 2);
            E[3, 0] = F;  // expands E to [4,1]

            Assert.IsTrue(D.GetArray<double>(0, 0).Equals(ones<double>(1,1)));
            Assert.IsTrue(E.GetValue<double>(3, 0, 1, 0, 0) == 1.0); 
        }

        [TestMethod]
        public void Cell_ellipsis() {
            Cell C = cell(size(4, 3));
            Array<int> I = -1; 
            C[full] = I;

            Assert.IsTrue(C.GetValue<int>(1,0,0,0) == -1); 

            Cell D = C[ellipsis];

            Assert.IsTrue(D.S.IsSameShape(C.S));
            Assert.IsTrue(D.GetValue<int>(1, 0, 0, 0) == -1);
        }
        [TestMethod]
        public void Cell_docu002() {

            Cell C = cell(size(4, 3));
            Cell D = cell(arrays: vector<BaseArray>(0, 1, 2, 3));    // initialize with scalars
            Cell E = cell(size(2, 2), new BaseArray[] { pi, "ILNumerics", D, counter<uint>(1, 1, 5, 4) });

            Cell F = cellv(C, D, null, E).Reshape(2, 2); 
//Cell [2,2]
//[0]:         
//[1]:              {Cell [4,3]}                   {null}
//[2]:              {Cell [4,1]}             {Cell [2,2]}

            
            C[0, 1] = rand(4);                  // set single cell element
            C[end, -1] = vector<int>(1, 2, 3); // sets single cell element with special index placeholders
            C[2, full] = -1f;                // sets ranges / slices

            //Cell [4,3]
            //    [0]:           
            //    [1]:                    {null}         {<Double> [4,4]}                   {null}
            //    [2]:                    {null}                   {null}                   {null}
            //    [3]:          {<Single> [1,1]}         {<Single> [1,1]}         {<Single> [1,1]}
            //    [4]:                    {null}                   {null}          {<Int32> [3,1]}
C[1, full] = null;  // removes the 2nd row from C
//Cell [3,3]
//[0]:           
//[1]:                    {null}         {<Double> [4,4]}                   {null}
//[2]:          {<Single> [1,1]}         {<Single> [1,1]}         {<Single> [1,1]}
//[3]:                    {null}                   {null}          {<Int32> [3,1]}

C[0, end + 1] = zeros<long>(1000, 1000);  // adds a new column to storage the new matrix
                                          //Cell [3,4]
                                          //[0]:             
                                          //[1]:                    {null}         {<Double> [4,4]}                   {null}    {<Int64> [1000,1000]}
                                          //[2]:          {<Single> [1,1]}         {<Single> [1,1]}         {<Single> [1,1]}                   {null}
                                          //[3]:                    {null}                   {null}          {<Int32> [3,1]}                   {null}

            Array<double> A = rand(10);
            C[0] = A;

            A[full] = -1; 
            //<Double> [10,10] -1...-1 |
            //[0]:          -1         -1         -1         -1         -1         -1         -1         -1         -1         -1
            //[1]:          -1         -1         -1         -1         -1         -1         -1         -1         -1         -1
            //[2]:          -1         -1         -1         -1         -1         -1         -1         -1         -1         -1
            //[3]:          -1         -1         -1         -1         -1         -1         -1         -1         -1         -1
            //[4]:          -1         -1         -1         -1         -1         -1         -1         -1         -1         -1
            //[5]:          -1         -1         -1         -1         -1         -1         -1         -1         -1         -1
            //[6]:          -1         -1         -1         -1         -1         -1         -1         -1         -1         -1
            //[7]:          -1         -1         -1         -1         -1         -1         -1         -1         -1         -1
            //[8]:          -1         -1         -1         -1         -1         -1         -1         -1         -1         -1
            //[9]:          -1         -1         -1         -1         -1         -1         -1         -1         -1         -1

                        //C.GetArray<double>(0)
            //<Double> [10,10] 0.267580671826182...0.350646412163343 |
            //[0]:    0.267581   0.525202   0.504702   0.199297   0.792394   0.273340   0.904597   0.386165   0.885733   0.923937
            //[1]:    0.285513   0.938723   0.881879   0.115937   0.255526   0.020289   0.225861   0.702291   0.615201   0.554263
            //[2]:    0.523616   0.939707   0.339653   0.008345   0.883328   0.347485   0.468002   0.046753   0.396092   0.352800
            //[3]:    0.953715   0.208626   0.298420   0.150519   0.173306   0.078857   0.968390   0.116021   0.208275   0.808185
            //[4]:    0.157043   0.482095   0.218523   0.787785   0.759911   0.247092   0.186982   0.232120   0.172560   0.711771
            //[5]:    0.084583   0.165623   0.063360   0.869798   0.347256   0.385701   0.409264   0.249176   0.179009   0.566534
            //[6]:    0.479849   0.790283   0.179986   0.779458   0.177627   0.945860   0.930379   0.998551   0.768452   0.787525
            //[7]:    0.340319   0.948620   0.253988   0.087992   0.564786   0.225196   0.789187   0.166346   0.929488   0.914938
            //[8]:    0.414290   0.732389   0.872318   0.901867   0.857896   0.735192   0.420841   0.538509   0.903428   0.986324
            //[9]:    0.386279   0.024515   0.445900   0.616368   0.664042   0.093683   0.612297   0.871266   0.402084   0.350646

            // deep indexing
            //D[0,0,]
        }
        [TestMethod]
        public void Cell_docu003() {
            Array<double> A = rand(4);
            Cell C = A;
            //C
            //Cell [1,1]
            //[0]:       
            //[1]:          {<Double> [4,4]}
            // .. or in numpy array style: 
            using (Scope.Enter(ArrayStyles.numpy)) {
                Cell D = A;
                //D
                //Cell []
                //  [0]:  <Double> [4,4]
                Cell F = cellv(C, zeros<uint>(10, 3), null, D).Reshape(2, 2, StorageOrders.ColumnMajor);
                //Cell [2,2]
                //[0]:         
                //[1]:              {Cell [1,1]}                   {null}
                //[2]:         {<UInt32> [10,3]}                {Cell []}

                Cell G = cell(size(4, 3));  // creates a new cell array, all elements are null
                                            //Cell [4,3]
                                            //    [0]:           
                                            //    [1]:                    {null}                   {null}                   {null}
                                            //    [2]:                    {null}                   {null}                   {null}
                                            //    [3]:                    {null}                   {null}                   {null}
                                            //    [4]:                    {null}                   {null}                   {null}
                Cell H = cell(arrays: vector<BaseArray>(0, 1, "2", 3));    // initialize with scalar arrays {0},{1},{"2"},{3} 
                                                                           //Cell [4,1]
                                                                           //    [0]:       
                                                                           //    [1]:             {<Double> []}
                                                                           //    [2]:             {<Double> []}
                                                                           //    [3]:             {<String> []}
                                                                           //    [4]:             {<Double> []}

                Cell I = cell(size: size(2, 2), arrays: new BaseArray[] { pi, "ILNumerics", H, counter<uint>(1, 1, 5, 4) });
                //Cell [2,2]
                //    [0]:         
                //    [1]:             {<Double> []}             {Cell [4,1]}
                //    [2]:             {<String> []}         {<UInt32> [5,4]}
                // create uninitialized cell [3 x 4]
                Cell E = cell(size(3, 4));

                // create 4 scalar arrays and fill first row of E with these values
                E[0, full] = cellv(1, "2", 3, ccomplex(-1.0, 1.0));
                //Cell [3,4]
                //[0]:             
                //[1]:             {<Double> []}            {<String> []}            {<Double> []}           {<complex> []}
                //[2]:                    {null}                   {null}                   {null}                   {null}
                //[3]:                    {null}                   {null}                   {null}                   {null}

                Settings.ArrayStyle = ArrayStyles.ILNumericsV4;
                E[1, full] = null;  // removes the 2nd row from E
                                    //Cell [2,4]
                                    //[0]:             
                                    //[1]:             {<Double> []}            {<String> []}            {<Double> []}           {<complex> []}
                                    //[2]:                    {null}                   {null}                   {null}                   {null}

                E[end + 1, 2] = cellv(I);  // store the new matrix into a new row
                                           //Cell [3,4]
                                           //[0]:             
                                           //[1]:             {<Double> []}            {<String> []}            {<Double> []}           {<complex> []}
                                           //[2]:                    {null}                   {null}                   {null}                   {null}
                                           //[3]:                    {null}                   {null}             {Cell [2,2]}                   {null}

                Settings.ArrayStyle = ArrayStyles.numpy;
                E.SetValue(I, 3, 2);   // stores the cell I into a new row of E
                                       //Cell [4,4]
                                       //[0]:             
                                       //[1]:             {<Double> []}            {<String> []}            {<Double> []}           {<complex> []}
                                       //[2]:                    {null}                   {null}                   {null}                   {null}
                                       //[3]:                    {null}                   {null}             {Cell [2,2]}                   {null}
                                       //[4]:                    {null}                   {null}             {Cell [2,2]}                   {null}

                using (Scope.Enter(ArrayStyles.numpy)) {
                    // create empty cell in numpy mode
                    Cell Z = cell();
                    //Cell [0]
                    //[0]: [empty]

                    Z[2] = zeros<double>(10, 20);
                    //Cell [3]
                    //[0]:       
                    //[1]:                    {null}
                    //[2]:                    {null}
                    //[3]:        {<Double> [10,20]}
                }
                double v = E.GetCell(3, 2).GetArray<double>(0).GetValue(0);
                //v
                //3.1415926535897931

                //Replacing the stored value v with 2*pi is a bit more involved:
                Cell tmpCell = E.GetCell(3, 2);
                Array<double> tmpArray = tmpCell.GetArray<double>(0);
                // changing the value 
                tmpArray[0] = tmpArray[0] * 2;
                // store back in reversed order
                tmpCell[0] = tmpArray;
                E[3, 2] = tmpCell;

                // check 
                double v2 = E.GetCell(3, 2).GetArray<double>(0).GetValue(0);
                //v2
                //6.2831853071795862

                E.GetValue(3, 2).ToString();
                // fill with values 
                //E[full] = cellv(1,2,3,4);
                //E[ellipsis] = cellv(1, 2, 3, 4).Reshape(2, 2);
                //E[r(0, 1), "0,1"] = cellv(1, 2, 3, 4).Reshape(2, 2);
                double v3 = E.GetValue<double>(3, 2, 0, 0);
                E.SetValue(v3 * 2, 3, 2, 0, 0);

                Cell K = 10.0;
                //K[0]
                //Cell []
                //    [0]:  <Double> []
                //K.GetValue()
                //<Double> [] -99 |
                //    [0]:  -99
            }
Cell M = cell(size(3, 5));
M[slice(0,2), full] = cellv(1, 2, "3", 4, 5, 6, 7, 8, 9, 10).Reshape(2,5, StorageOrders.RowMajor); 
//Cell [3,5]
//[0]:               
//[1]:          {<Double> [1,1]}         {<Double> [1,1]}         {<String> [1,1]}         {<Double> [1,1]}         {<Double> [1,1]}
//[2]:          {<Double> [1,1]}         {<Double> [1,1]}         {<Double> [1,1]}         {<Double> [1,1]}         {<Double> [1,1]}
//[3]:                    {null}                   {null}                   {null}                   {null}                   {null}
M[2,1] = rand(5,4);
            //Cell [3,5]
            //[0]:               
            //[1]:          {<Double> [1,1]}         {<Double> [1,1]}         {<String> [1,1]}         {<Double> [1,1]}         {<Double> [1,1]}
            //[2]:          {<Double> [1,1]}         {<Double> [1,1]}         {<Double> [1,1]}         {<Double> [1,1]}         {<Double> [1,1]}
            //[3]:                    {null}         {<Double> [5,4]}                   {null}                   {null}                   {null}

            //M.GetArray<double>(2,1)
            //<Double> [5,4] 0.731685337485599...0.799241759720837 |
            //[0]:    0.731685   0.779106   0.339409   0.081569
            //[1]:    0.071056   0.978556   0.563593   0.081390
            //[2]:    0.746626   0.842316   0.618610   0.108820
            //[3]:    0.460761   0.821666   0.142223   0.767078
            //[4]:    0.602697   0.099746   0.161226   0.799242

M[2, 2] = M[full, 1];  // creates a cell with the elements of the 2nd colum & stores it at [2,2]
//Cell [3,5]
//[0]:               
//[1]:          {<Double> [1,1]}         {<Double> [1,1]}         {<String> [1,1]}         {<Double> [1,1]}         {<Double> [1,1]}
//[2]:          {<Double> [1,1]}         {<Double> [1,1]}         {<Double> [1,1]}         {<Double> [1,1]}         {<Double> [1,1]}
//[3]:                    {null}         {<Double> [5,4]}               {Cell [3]}                   {null}                   {null}

//M.GetCell(2,2)
//Cell [3]
//[0]:       
//[1]:          {<Double> [1,1]}
//[2]:          {<Double> [1,1]}
//[3]:          {<Double> [5,4]}

M[2, 0] = M.GetArray<double>(2, 1) < .5;
            //Cell [3,5]
            //[0]:               
            //[1]:          {<Double> [1,1]}         {<Double> [1,1]}         {<String> [1,1]}         {<Double> [1,1]}         {<Double> [1,1]}
            //[2]:          {<Double> [1,1]}         {<Double> [1,1]}         {<Double> [1,1]}         {<Double> [1,1]}         {<Double> [1,1]}
            //[3]:           {Logical [5,4]}         {<Double> [5,4]}               {Cell [3]}                   {null}                   {null}

            //M.GetLogical(2)
            //Logical[5, 4] False...False |
            //[0]:             
            //[1]:  ▯ ▯ ▮ ▮
            //[2]:  ▮ ▯ ▯ ▮
            //[3]:  ▯ ▯ ▯ ▮
            //[4]:  ▮ ▯ ▮ ▯
            //[5]:  ▯ ▮ ▮ ▯

        }

        [TestMethod]
        public void Cell_docu0004() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<int> A = counter<int>(1, 1, 5, 4);
                Cell C = A;
                //Cell []
                //[0]:  <Int32> [5,4]

                Array<int> B = C.GetArray<int>(); // no indices
                //B
                //<Int32> [5,4] 1...20 |
                //[0]:            1           6          11          16
                //[1]:            2           7          12          17
                //[2]:            3           8          13          18
                //[3]:            4           9          14          19
                //[4]:            5          10          15          20

                // replace the value of C
                C.SetValue(-99.0);  // no indices
                //C
                //Cell[]
                //[0]:  <Double> []

                Array<double> D = C.GetArray<double>(); 
                //D
                //<Double> [] -99 |
                //[0]:  -99
                C.GetValue<double>();  // -> gives -99.0, no indices
                Assert.IsTrue(C.GetValue<double>() == -99); 

            }
        }

        [TestMethod]
        public void Cell_scalarImplicitConversion() {

            Array<float> A = new float[] { 1f, 2f, 3f };
            Cell C = cell(size(2, 1));

            var r1 = (Cell)A; 

            C[0] = r1;

            var amm1 = A * -1;

            var r2 = (Cell)(amm1);
            Assert.IsFalse(Equals(amm1.Storage.m_handles, null));

            C[1] = r2;

            var cArr = (C.Storage.m_handles[0] as ManagedHostHandle<IStorage>).HostArray;

            var c0 = (cArr[0] as Storage<float>); 
            var c1 = (cArr[1] as Storage<float>);



            Assert.IsTrue(c0.RetArray.Equals(A));
            Assert.IsTrue(c1.RetArray.Equals(A * -1));

        }
        [TestMethod]
        public void Cell_ToScalar() {
            var z = zeros<short>(4, 3);

            Cell C = (Cell)z;
            Assert.IsTrue(Equals(z.Storage.m_handles, C.GetArray<short>(0).Storage.Handles));


            Assert.IsTrue(C.IsScalar);
            Assert.IsTrue(C.S[0] == 1);
            Assert.IsTrue(C.S[1] == 1);

            Assert.IsTrue(C.GetArray<short>(0).Equals(zeros<short>(4, 3)));

        }

        [TestMethod]
        public void Cell_ToNPScalar() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {
                var z = zeros<short>(4, 3);

                Cell C = z;
 

                Assert.IsTrue(Equals(z.Storage.m_handles.ID, C.GetArray<short>().Storage.Handles.ID));
                Assert.IsFalse(Equals(z.Storage, C.GetArray<short>(0).Storage));


                Assert.IsTrue(C.IsScalar);
                Assert.IsTrue(C.S[0] == 1);
                Assert.IsTrue(C.S[1] == 1);
                Assert.IsTrue(C.S.NumberOfDimensions == 0);

                Assert.IsTrue(C.GetArray<short>().Equals(zeros<short>(4, 3)));
            }
        }
        [TestMethod]
        public void Cell_Equals() {

            Array<short> V = vector<short>(1, 2, 3, 4, 5, 6); 
            Cell cell1 = cell(size(3, 2), vector<BaseArray>(
                                "first element"
                                , 2.0
                                , cell(size(2), vector<BaseArray>(Math.PI, 100f))
                                , V
                                , vector(new double[] { -1.4, -1.5, -1.6 })));
            Cell cell2 = cell1.C;
            Assert.IsTrue(cell2.Equals(cell1));
            cell2.SetValue("new value", 0, 0);
            Assert.IsTrue(!cell2.Equals(cell1));
            cell1.SetValue("new value", 0, 0);
            Assert.IsTrue(cell2.Equals(cell1));

            cell2[end] = 10;
            Assert.IsTrue(!cell2.Equals(cell1));
            cell1[end] = 10;
            Assert.IsTrue(cell2.Equals(cell1));
        }
        [TestMethod]
        public void Cell_repmat() {
            Array<short> A1 = new short[] { 1, 2, 3 };

            Cell C1 = cell(arrays: vector<BaseArray>(A1, 1, null, "object"));

            Assert.IsTrue(C1.S[0] == 4); 
            Assert.IsTrue(C1.S[1] == 1);
            Assert.IsTrue(C1.S.NumberOfElements == 4);

            C1.a = C1.Repmat(size(2, 3));

            Cell C2 = cell(size(8, 3), vector<BaseArray>(
                A1, 1, null, "object", A1, 1, null, "object", 
                A1, 1, null, "object", A1, 1, null, "object", 
                A1, 1, null, "object", A1, 1, null, "object" ));

            Assert.IsTrue(C1.Equals(C2));

            Assert.IsTrue(C1.GetArray<string>(3).GetValue(0) == "object"); 
        }

        [TestMethod]
        public void Cell_Logicals() {

            Array<float> A = vector<float>(1, 2, 3, 4);
            Logical L = A < 2;

            Cell C = cell(size(1, 3), vector<BaseArray>(A, L));

            Logical L2 = C.GetValue(1).ToLogical();
            Assert.IsTrue(L2.Equals(L));


        }

        [TestMethod]
        public void Cell_GetLogical_simple() {

            Array<float> A = vector<float>(1, 2, 3, 4);
            Logical L = A < 2;

            Cell C = cell(size(1, 3), vector<BaseArray>(A, L));

            Logical L2 = C.GetLogical(1);
            Assert.IsTrue(L2.Equals(L));



            Assert.IsTrue(C.GetLogical(0, 1).Equals(L));
            Assert.IsTrue(C.GetLogical(0, 1, 0));
            Assert.IsTrue(C.GetLogical(0, 1, 1, 0).Equals(false));
            Assert.IsTrue(C.GetLogical(0, 1, 2, 0, 0).Equals(false));
            Assert.IsTrue(C.GetValue<bool>(0, 1, 0, 0, 0, 0) == true);
            Assert.IsTrue(C.GetValue<bool>(0, 1, 1, 0, 0, 0, 0) == false);
            Assert.IsTrue(C.GetLogical(vector<long>(0, 1, 1, 0, 0, 0, 0)).Equals(false));


        }

        [TestMethod]
        public void Cell_GetLogical_null() {

            Logical l = rand(4) < 0.5;

            // creates a cell: [logical 4x4]
            Cell C = l;
            C[1] = 1;  // expansion
            C[1] = cell();  // removal

            // ..:| ?? 
            Assert.IsTrue(C.GetLogical(0).Equals(l));

        }
        [TestMethod]
        public void Cell_Expand_simple() {

            Cell C = cell(); // 0,1
            Assert.IsTrue(C.S[0] == 0);
            Assert.IsTrue(C.S[1] == 1);
            Assert.IsTrue(C.S.NumberOfElements == 0);
            Assert.IsTrue(C.S.NumberOfDimensions == 2);

            C[4] = 1;
            Assert.IsTrue(C.S[0] == 5);
            Assert.IsTrue(C.S[1] == 1);
            Assert.IsTrue(C.S.NumberOfElements == 5);
            Assert.IsTrue(C.GetValue<int>(4, 0, 0) == 1);
            Assert.IsTrue(C.S.NumberOfDimensions == 2);

            C[1, 3] = (float)-8;
            Assert.IsTrue(C.S[0] == 5);
            Assert.IsTrue(C.S[1] == 4);
            Assert.IsTrue(C.S.NumberOfElements == 20);
            Assert.IsTrue(C.GetValue<int>(4, 0, 0) == 1);
            Assert.IsTrue(C.GetValue<float>(1, 3, 0) == -8);
            Assert.IsTrue(C.GetArray<float>(1, 3).Equals(vector<float>(-8)));
            Assert.IsTrue(C.S.NumberOfDimensions == 2);

        }
        [TestMethod]
        public void Cell_Expand_innerCell() {

            Array<double> A = counter<double>(1.0, 1.0, 5,4);
            Cell C = cellv(A, cellv(A));

            Assert.IsTrue(C.GetArray<double>(0, 0).Equals(A)); 
            Assert.IsTrue(C.GetArray<double>(1, 0, 0, 0).Equals(A));

            // the inner cell is expanded and should look as follows: 
            Array<double> B = A.C;

            B[7, 6] = -99;

            C[1, 0, 0, 0, 7, 6] = -99.0;
            Assert.IsTrue(C.GetArray<double>(0, 0).Equals(A));
            Assert.IsTrue(C.GetArray<double>(1, 0, 0, 0).Equals(B));

            C[0, 0, 7, 6] = -99.0;
            Assert.IsTrue(C.GetArray<double>(0, 0).Equals(B));
            Assert.IsTrue(C.GetArray<double>(1, 0, 0, 0).Equals(B));

        }



        [TestMethod]
        public void Cell_Expand_NPFail() {
            using (Scope.Enter(ArrayStyles.numpy)) {

                Cell C = cell(); // 0,1
                Assert.IsTrue(C.S[0] == 0);
                Assert.IsTrue(C.S[1] == 1);
                Assert.IsTrue(C.S.NumberOfElements == 0);
                Assert.IsTrue(C.S.NumberOfDimensions == 1);

                C[4] = 1; // IS  allowed in numpy mode

                Assert.IsTrue(C.S[0] == 5); // on cells allowed! 
            }
        }
        [TestMethod]
        public void Cell_ConcatRet() {

            Cell C = cell(size(5, 4));
            var a = rand(10);
            var hand = a.Storage.m_handles;

            C[full] = a;

            C.a = C.Concat(C, 0);

            C.a = C.Concat(C, 1);

        }
        [TestMethod]
        public void Cell_Concat() {

            Cell C = cell(size(5, 4));
            Array<double> a = rand(10);

            C[full] = a;  // this waits for a.Finish() in accelerated code

            var hand = a.Storage.m_handles;

            C.a = C.Concat(C, 0);

            C.a = C.Concat(C, 1);

            Cell R = cell(size(10, 8));
            R[ellipsis] = a;

            Assert.IsTrue(R.Equals(C)); 


        }

        [TestMethod]
        public void Cell_ImplicitConversionSystemScalar() {
            using (Scope.Enter(ArrayStyles.ILNumericsV4)) {
                Cell C = cell(10);
                C[0] = 1;
                C[1] = 2f;
                C[2] = 3.0;
                C[3] = 4u;
                var l = (Cell)4L;
                C[4] = l;
                C[5] = 4UL;
                C[6] = "8";
                C[7] = (byte)9;
                C[8] = new double[] { -1, -1.4, -1.6, double.NaN };
                C[9] = C;


                Assert.IsTrue(C.GetArray<int>(0).GetValue(0) == 1);
                Assert.IsTrue(C.GetArray<float>(1).GetValue(0) == 2);
                Assert.IsTrue(C.GetArray<double>(2).GetValue(0) == 3);
                Assert.IsTrue(C.GetArray<uint>(3).GetValue(0) == 4);
                Assert.IsTrue(C.GetArray<long>(4).GetValue(0) == 4);
                Assert.IsTrue(C.GetArray<ulong>(5).GetValue(0) == 4);
                Assert.IsTrue(C.GetArray<string>(6).GetValue(0) == "8");
                Assert.IsTrue(C.GetArray<byte>(7).GetValue(0) == 9);
                Assert.IsTrue(C.GetArray<double>(8).Equals(vector<double>(-1, -1.4, -1.6, double.NaN)));
                Assert.IsTrue((C.GetValue(9) as Cell).S.NumberOfElements == 10);
                Assert.IsTrue(C.GetValue(9,0,9) == null);  // the element stored was a clone of C before storing!
            }
        }
        [TestMethod]
        public void Cell_Detach1Test() {

            Array<double> A = vector(1.0, 2, 3).Reshape(1, 3);

            Cell C = cell(size(2, 2));

            for (int i = 0; i < C.Size.NumberOfElements; i++) {
                C[i] = i;
            }

            C[1] = A; string b = C.ToString();
            // test, if A is on its place 
            Assert.IsTrue(C is Cell);

            Assert.IsTrue(C.IsTypeOf<int>(0));
            Assert.IsTrue(C.GetArray<int>(0)[0].Equals(0));

            Assert.IsTrue(C.IsTypeOf<int>(2));
            Assert.IsTrue(C.GetArray<int>(2)[0].Equals(2));

            Assert.IsTrue(C.IsTypeOf<int>(3));
            Assert.IsTrue(C.GetArray<int>(3)[0].Equals(3));

            Assert.IsTrue(C.IsTypeOf<double>(1));

            Assert.IsTrue(C.GetArray<double>(1)[0].Equals(1.0));

            Assert.IsTrue(C.GetArray<double>(1)[1].Equals(2.0));

            Assert.IsTrue(C.GetArray<double>(1)[2].Equals(3.0));

            Array<double> FG = C.GetArray<double>(1);
            // alter A -> should NOT alter cell in C made from A 
            A[1] = 10;
            Assert.IsTrue(C.GetArray<double>(1)[1].Equals(2.0));

        }
        [TestMethod]
        public void Cell_Detach2Test() {


            Array<double> A = vector(1.0, 2, 3).Reshape(1, 3);
            Cell C = cell(size(2, 2));
            for (int i = 0; i < C.Size.NumberOfElements; i++) {
                C[i] = i;
            }
            C[1] = A;
            // .. alter cell content. Deep indexing is not supported anymore. retrieve the cell, change it and store it back: 
            Array<double> tmp = C.GetArray<double>(1, 0);
            tmp[0, 1] = -100;
            C[1, 0] = tmp;
            // was: C[1, 0, 0, 1] = -100;

            // ... and check, if A is unchanged
            Assert.IsTrue(C.GetArray<double>(size(1, 0))[1].Equals(-100.0));
            Assert.IsTrue(A.GetArrayForRead()[1].Equals(2));

        }

        [TestMethod]
        public void Cell_Detach3Test() {

            // test array in a cell in a cell: 
            Array<double> A1 = vector(1.0, 2, 3).Reshape(1, 3);
            Array<double> A2 = vector(4.0, 5, 6).Reshape(1, 3);
            // reference counting: should be the only copy
            Assert.IsTrue(A1.ReferenceCount.Equals(1));
            Assert.IsTrue(A2.ReferenceCount.Equals(1));


            Cell inCell = cell(null, vector<BaseArray>(A1, A2, -10));
            Assert.IsTrue(A1.ReferenceCount.Equals(1));
            Assert.IsTrue(A2.ReferenceCount.Equals(1));


            var vecTmp = vector<BaseArray>(1, 2f, inCell, 4, 5.0);

            Cell outCell = cell(null, vecTmp);

            // alter A2 
            A2[0] = -999;
            // .. and see, if cells contents (both!) did not change
            Assert.IsTrue(inCell.GetArray<double>(1)[0].Equals(4.0));

            //Assert.IsTrue((double)outCell.GetItem(outCell.S.GetSeqIndex(2, 0, 1, 0, 0)) == (4.0));

            Assert.IsTrue(outCell.GetCell(2).GetArray<double>(1).GetValue(0) == 4);
            Assert.IsTrue(inCell.GetArray<double>(1).GetValue(0) == 4);
            Assert.IsTrue(A2.GetArrayForRead()[0].Equals(-999));


        }
        [TestMethod]
        public void Cell_Detach4Test() {

            // setting a range of scalars must actually set copies of the scalar

            Cell C = cell(size(3, 2));
            C[r(0, end)] = 1.0;
            Assert.IsTrue(C.GetArray<double>(0).IsScalar);
            Assert.IsTrue(C.GetArray<double>(0).GetValue(0) == 1.0);
            C[end] = zeros<int>(5, 4, 3);

            // old values not touched ? 
            Assert.IsTrue(C.GetArray<double>(0).IsScalar);
            Assert.IsTrue(C.GetArray<double>(0).GetValue(0) == 1.0);
            Assert.IsTrue(C.GetArray<double>(1).IsScalar);
            Assert.IsTrue(C.GetArray<double>(1).GetValue(0) == 1.0);
            Assert.IsTrue(C.GetArray<double>(2).IsScalar);
            Assert.IsTrue(C.GetArray<double>(2).GetValue(0) == 1.0);
            Assert.IsTrue(C.GetArray<double>(3).IsScalar);
            Assert.IsTrue(C.GetArray<double>(3).GetValue(0) == 1.0);
            Assert.IsTrue(C.GetArray<double>(4).IsScalar);
            Assert.IsTrue(C.GetArray<double>(4).GetValue(0) == 1.0);
            // new value at its place
            Assert.IsTrue(C.GetArray<int>(5).S[0] == 5);
            Assert.IsTrue(C.GetArray<int>(5).S[1] == 4);
            Assert.IsTrue(C.GetArray<int>(5).S[2] == 3);
            Assert.IsTrue(C.GetArray<int>(5).GetValue(0) == 0.0);


        }
        [TestMethod]
        public void Cell_Detach5Test() {

            // setting via full is handled differently, so test it seperately

            Cell C = cell(size(3, 2));
            C[full] = 1.0;
            Assert.IsTrue(C.GetArray<double>(0).IsScalar);
            Assert.IsTrue(C.GetArray<double>(0).GetValue(0) == 1.0);
            C[end] = zeros<int>(5, 4, 3);
            // old values not touched ? 
            Assert.IsTrue(C.GetArray<double>(0).IsScalar);
            Assert.IsTrue(C.GetArray<double>(0).GetValue(0) == 1.0);
            Assert.IsTrue(C.GetArray<double>(1).IsScalar);
            Assert.IsTrue(C.GetArray<double>(1).GetValue(0) == 1.0);
            Assert.IsTrue(C.GetArray<double>(2).IsScalar);
            Assert.IsTrue(C.GetArray<double>(2).GetValue(0) == 1.0);
            Assert.IsTrue(C.GetArray<double>(3).IsScalar);
            Assert.IsTrue(C.GetArray<double>(3).GetValue(0) == 1.0);
            Assert.IsTrue(C.GetArray<double>(4).IsScalar);
            Assert.IsTrue(C.GetArray<double>(4).GetValue(0) == 1.0);
            // new value at its place
            Assert.IsTrue(C.GetArray<int>(5).S[0] == 5);
            Assert.IsTrue(C.GetArray<int>(5).S[1] == 4);
            Assert.IsTrue(C.GetArray<int>(5).S[2] == 3);
            Assert.IsTrue(C.GetArray<int>(5).GetValue(0) == 0.0);


        }

        [TestMethod]
        public void Cell_SetRange_ML_WriteToCell_RefCount() {
            Array<long> A = vector<long>(1, 2, 3, 4, 5, 6).Reshape(2, 3);

            Cell C = A;

            C[r(0, 0)] = vector<long>(-1, -2, -3, -4, -5, -6);  // same shape, same numel -> Write_To_Cell



        }
        [TestMethod]
        public void Cell_SetRange_ML_BSDIter_RefCount() {
            Array<long> A = vector<long>(1, 2, 3, 4, 5, 6).Reshape(2, 3);

            Cell C = cell(size(2, 2));
            C[full] = A;


            Cell R = cell(size(1, 4));
            R[0] = -1; R[1] = -2; R[2] = -3; R[3] = -3;
            C[r(0, end), full] = R;  // other shape, same numel -> BSD_ITER




        }
        [TestMethod]
        public void Cell_SetRange_ML_IterIter_RefCount() {

            Array<long> A = vector<long>(1, 2, 3, 4, 5, 6).Reshape(2, 3);

            Cell C = cell(size(2, 2));
            C[full] = A;


            Cell R = cell(size(1, 4));
            R[0] = -1; R[1] = -2; R[2] = -3; R[3] = -3;
            C[":", full] = R;  // other shape, same numel, ML mode -> ITER_ITER




        }
        [TestMethod]
        public void Cell_SetRange_ML_WriteToBSDIter_RefCount() {

            Array<long> A = vector<long>(1, 2, 3, 4, 5, 6).Reshape(2, 3);

            Cell C = cell(size(2, 2));
            C[full] = A;


            Cell R = cell(size(2, 2));
            R[0] = -1; R[1] = -2; R[2] = -3; R[3] = -3;
            C[full, ":"] = R;   // same shape, ML mode -> WriteToBSDIterOperators.WriteTo_BSD_Iter<T>




        }
        [TestMethod]
        public void Cell_DetachStride_ColMaj() {

            Array<long> A = vector<long>(1, 2, 3, 4, 5, 6).Reshape(2, 3);

            Cell C = cell(size(2, 2), order: StorageOrders.RowMajor);
            Assert.IsTrue(C.S.StorageOrder == StorageOrders.RowMajor);

            C[full] = A;  // this reorders already
            //Assert.IsTrue(C.S.StorageOrder == StorageOrders.RowMajor);

            C.Storage.EnsureStorageOrder(StorageOrders.ColumnMajor);

            Assert.IsTrue(C.S.StorageOrder == StorageOrders.ColumnMajor); 
            for (int i = 0; i < C.S.NumberOfElements; i++) {
                Assert.IsTrue(C.GetArray<long>(i).Equals(A)); 
            }
        }

        [TestMethod]
        public void Cell_RemoveSimple2D() {

            Array<double> O = counter(1.0, 1.0, 5, 4);
            Cell A = cell(size(5, 4));
            A[full] = O;

            A[full, 2] = null;

            Assert.IsTrue(A.S.NumberOfDimensions == 2);
            Assert.IsTrue(A.S.NumberOfElements == 5 * 3);

            for (int i = 0; i < A.S.NumberOfElements; i++) {
                Assert.IsTrue(A.GetArray<double>(i).Equals(O));
            }

            // removes all
            A.a = cell(size(5, 4));
            A[full] = O;

            A[full, full] = null;
            Assert.IsTrue(A.IsEmpty);
        }
        [TestMethod]
        public void Cell_Assign_NPScalar_Refcounting() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<double> O = counter(1.0, 1.0, 5, 4);
                Cell A = O;

                A[full] = O;
            }
        }

        [TestMethod]
        public void Cell_GetRange_ML_iterateSubarrayML() {

            Cell C = cell(size(5, 4));
            C[full] = arange<double>(0, 9);

            Array<double> back = C.GetArray<double>(0);

            C[full, vector<int>(1)] = zeros<long>(4);

            // all <long> and <double> arrays in the cell ref. the same storage resp.
            Cell D = C[full, vector<int>(1)];
            Array<long> back2 = D.GetArray<long>(0);


            for (int i = 0; i < D.S.NumberOfElements; i++) {
                Assert.IsTrue(D.GetArray<long>(i).Equals(zeros<long>(4)));
            }
            for (int i = 0; i < 5; i++) {
                Assert.IsTrue(C.GetArray<double>(i, 0).Equals(arange<double>(0, 9)));
                Assert.IsTrue(C.GetArray<double>(i, 2).Equals(arange<double>(0, 9)));
                Assert.IsTrue(C.GetArray<double>(i, 3).Equals(arange<double>(0, 9)));
            }

        }

        [TestMethod]
        public void Cell_ExportValues() {

            Cell C = cell(size(5, 4));
            Array<double> A = arange<double>(0, 9);
            C[full] = A;




        }
        [TestMethod]
        public void Cell_GetRange_ML_BSDRange() {

            Cell C = cell(size(5, 4));
            C[full] = arange<double>(0, 9);

            Array<double> back = C.GetArray<double>(0);

            C[full, vector<int>(1)] = zeros<long>(4);

            // all <long> and <double> arrays in the cell ref. the same storage resp.
            Cell D = C[full, 1];
            Array<long> back2 = D.GetArray<long>(0);


            for (int i = 0; i < D.S.NumberOfElements; i++) {
                Assert.IsTrue(D.GetArray<long>(i).Equals(zeros<long>(4)));
            }
            for (int i = 0; i < 5; i++) {
                Assert.IsTrue(C.GetArray<double>(i, 0).Equals(arange<double>(0, 9)));
                Assert.IsTrue(C.GetArray<double>(i, 2).Equals(arange<double>(0, 9)));
                Assert.IsTrue(C.GetArray<double>(i, 3).Equals(arange<double>(0, 9)));
            }

        }
        [TestMethod]
        public void Cell_SetRange_np_simple() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<float> A = counter<float>(1, 1, 5, 4);

                Cell C = cell(size(5, 4));
                C[full] = A;

                Assert.IsTrue(C.GetArray<float>(1, 2).Equals(A));


                Cell D = C[2];

                Assert.IsTrue(D.S[0] == 4);
                Assert.IsTrue(D.S.NumberOfDimensions == 1);
                Assert.IsTrue(D.GetArray<float>(2).Equals(A));



            }
        }
        [TestMethod]
        public void Cell_SetGetRange_np_scalar() {
            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<float> A = counter<float>(1, 1, 5, 4);

                Cell C = cell(size(5, 4));
                C[full] = A;

                Assert.IsTrue(C.GetArray<float>(1, 2).Equals(A));


                Cell D = C[2, 0];

                Assert.IsTrue(D.S[0] == 1);
                Assert.IsTrue(D.S.NumberOfDimensions == 0);
                Assert.IsTrue(D.GetArray<float>().Equals(A));



            }
        }
        [TestMethod]
        public void Cell_SetGetRange_np_3D() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.numpy)) {

                Array<float> A = counter<float>(1, 1, 5, 4);

                Cell C = cell(size(1, 3, 2));
                C[full] = A;

                Cell D = C[0]; 
                Assert.IsTrue(D.shape.Equals(size(3,2)));

                Cell E = C[0, 1];
                Assert.IsTrue(E.shape.Equals(size(2)));

                Cell F = C[0, r(1, 1)];
                Assert.IsTrue(F.shape.Equals(size(1, 2)));

                Cell G = C[0, slice(2, 3)];
                Assert.IsTrue(G.shape.Equals(size(1, 2)));

                Cell H = C[0, ellipsis, end];
                Assert.IsTrue(H.shape.Equals(size(3)));

                A[2] = -3;

                Assert.IsTrue(C.GetArray<float>(0).Equals(counter<float>(1, 1, 5, 4)));

            }
        }
        [TestMethod]
        public void Cell_GetSetRange_ML_2D() {

            using (Settings.Ensure(() => Settings.ArrayStyle, ArrayStyles.ILNumericsV4)) {

                Array<float> A = counter<float>(1, 1, 5, 4);

                Cell C = cell(size(5, 4));
                C[full] = A;

                Assert.IsTrue(C.GetArray<float>(1, 2).Equals(A));


                Cell D = C[2, full];

                Assert.IsTrue(D.S[0] == 1);
                Assert.IsTrue(D.S[1] == 4);
                Assert.IsTrue(D.S.NumberOfDimensions == 2);
                Assert.IsTrue(D.GetArray<float>(2).Equals(A));



            }
        }
        [TestMethod]
        public void Cell_assign_self() {

            Array<double> Z = zeros<double>(4, 3);
            Array<double> z = Z.C;

            Cell C = z;


            Assert.IsTrue(C.S.NumberOfDimensions == 2);
            Assert.IsTrue(C.S[0] == 1);
            Assert.IsTrue(C.S[1] == 1);


            C[1, 0] = 1; // expanding ...
            C[1, 0] = C; // + self assign


            Assert.IsTrue(C.S[0] == 2);
            Assert.IsTrue(C.S[1] == 1);
            Assert.IsTrue(C.S[2] == 1);
            Assert.IsTrue(C.S.NumberOfDimensions == 2); 

        }

        [TestMethod]
        public void Cell_IsTypeOf() {

            Cell cell1 = cell(size(3, 2), vector<BaseArray>(
                                "first element"
                                , 2.0
                                , cell(arrays: vector<BaseArray>(Math.PI, 100f))
                                , vector<short>(1, 2, 3, 4, 5, 6)
                                , vector<double>(-1.4, -1.5, -1.6 )));

            Console.Out.WriteLine("cell[0,0] is element type 'string': {0}", cell1.IsTypeOf<string>(0));
            Console.Out.WriteLine("cell[0,0] is element type 'double': {0}", cell1.IsTypeOf<double>(0));

            Console.Out.WriteLine("cell[1,0] is element type 'double': {0}", cell1.IsTypeOf<double>(1));
            Console.Out.WriteLine("cell[2,0] is element type 'Cell': {0}", cell1.IsTypeOf<Cell>(2));

            Console.Out.WriteLine("cell[0,1] is element type 'short': {0}", cell1.IsTypeOf<short>(0, 1));
            Console.Out.WriteLine("cell[1,1] is element type 'Cell': {0}", cell1.IsTypeOf<Cell>(1, 1));
            Console.Out.WriteLine("cell[2,1] is element type 'double': {0}", cell1.IsTypeOf<double>(2, 1));

            Assert.IsTrue(cell1.IsTypeOf<string>(0));
            Assert.IsTrue(!cell1.IsTypeOf<double>(0));

            Assert.IsTrue(cell1.IsTypeOf<double>(1));
            Assert.IsTrue(!cell1.IsTypeOf<int>(1));

            Assert.IsTrue(cell1.IsTypeOf<Cell>(2));
            Assert.IsTrue(!cell1.IsTypeOf<int>(2));

            Assert.IsTrue(cell1.IsTypeOf<short>(0, 1));
            Assert.IsTrue(!cell1.IsTypeOf<double>(0, 1));

            Assert.IsTrue(!cell1.IsTypeOf<Cell>(1, 1));
            Assert.IsTrue(cell1.IsTypeOf<double>(1, 1));

            Assert.IsTrue(!cell1.IsTypeOf<BaseArray>(2, 1));
            Assert.IsTrue(!cell1.IsTypeOf<double>(2, 1));
            Assert.IsTrue(cell1.IsTypeOf<BaseArray>(2, 0));

        }
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException), "Cell remove: index out of range not signaled!")]
        public void Cell_Adressing_IndexOutOfRangeFail() {
            Cell A = cell(size(2, 3, 2));
            Array<double> ind = new double[] { 15, 16, 22, 223 };
            A[ind] = null;
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException), "Cell remove: index out of range not signaled!")]
        public void Cell_Adressing_IndexOutOfRange_NP_Fail() {
            using (Scope.Enter(ArrayStyles.numpy)) {
                Cell A = cell(size(2, 3, 2));
                Array<double> ind = new double[] { 15, 16, 22, 223 };
                A[ind] = null;
 
            }
        }

        [TestMethod]
        public void Cell_deepIndexing_NPscalars() {

            using (Scope.Enter(ArrayStyles.numpy)) {

                Array<double> A = 99.0;

                Cell C = A;

                Assert.IsTrue(C.S.NumberOfDimensions == 0);
                Assert.IsTrue(A.GetValue<double>(0) == 99.0);
                Assert.IsTrue(C.GetValue<double>(0) == 99.0); // deep indexing! 
                Assert.IsTrue(C.GetArray<double>().Equals(A));


            }
        }
        [TestMethod]
        public void Cell_deepIndexing_NPscalarsRecrsv() {

            using (Scope.Enter(ArrayStyles.numpy)) {

                Array<double> A = 99.0;

                Cell C = A;

                Cell D = C.C; 

                Assert.IsTrue(C.S.NumberOfDimensions == 0);
                Assert.IsTrue(D.S.NumberOfDimensions == 0);
                Assert.IsTrue(A.GetValue<double>(0) == 99.0);
                Assert.IsTrue(C.GetValue<double>(0) == 99.0); // deep indexing! 
                Assert.IsTrue(D.GetValue<double>(0) == 99.0); // deep indexing! 

                Assert.IsTrue(C.GetArray<double>().Equals(A));
                Assert.IsTrue(D.GetArray<double>().Equals(A));


            }
        }
        [TestMethod]
        public void Cell_Addressing() {

            BaseArray[] data = new BaseArray[60];
            for (int i = 0; i < data.Length; i++)
                data[i] = i;
            Cell A = cell(size(20, 3), data);

            A[1, 1] = arange<double>(10.0, 100.0);
            A[2, 1] = arange(-10.0, -1, -100);

            // test subarray access: single
            Cell ret = A[23];

            Array<double> a23 = ret.GetArray<double>(0);

            Assert.IsTrue(a23.IsScalar, "Cell: returned element value mismatch! Expected: 23, found:" + a23.GetValue(0));
            Assert.IsTrue(a23.Equals(23), "Cell: returned element value mismatch! Expected: 23, found:" + a23.GetValue(0));

            // test subarray access: multiple via vector (sequential)
            // - put Cell into cell 
            Array<double> element1 = arange(10.0, 2, 20);
            Array<double> element2 = arange(20.0, 2, 30);
            Assert.IsTrue(element1.ReferenceCount.Equals(1));
            Assert.IsTrue(element2.ReferenceCount.Equals(1));

            Cell innerCell = cellv(element1, element2);
            Assert.IsTrue(element1.ReferenceCount.Equals(1));
            Assert.IsTrue(element2.ReferenceCount.Equals(1));

            A[3, 1] = innerCell;

            Array<double> ind = vector<double>(20.0, 21.0, 22.0, 23.0, 24.0).Reshape(1, 5);
            Cell cellresult_vect = A[ind];

            Assert.IsTrue(cellresult_vect.IsRowVector, "Cell: index access dimension mismatch: should be Cell 1x5! ");
            Assert.IsTrue(cellresult_vect.Size[0].Equals(1), "Cell: index access dimension mismatch: should be Cell 1x5! ");
            Assert.IsTrue(cellresult_vect.Size[1].Equals(5), "Cell: index access dimension mismatch: should be Cell 1x5! ");


            Assert.IsTrue(cellresult_vect.IsTypeOf<double>(0, 0), "Cell: index access failed. Inner element mismatch for vector indices.");
            Assert.IsTrue(cellresult_vect.IsTypeOf<double>(0, 1), "Cell: index access failed. Inner element mismatch for vector indices.");
            Assert.IsTrue(cellresult_vect.IsTypeOf<double>(0, 2), "Cell: index access failed. Inner element mismatch for vector indices.");
            Assert.IsTrue(cellresult_vect.IsTypeOf<Cell>(0, 3), "Cell: index access failed. Inner element mismatch for vector indices.");
            Assert.IsTrue(cellresult_vect.IsTypeOf<double>(0, 4), "Cell: index access failed. Inner element mismatch for vector indices.");



            // test if elements returned are properly referenced (detached from original)
            Array<double> tmps = cellresult_vect.GetArray<double>(0, 0);
            Assert.IsTrue(tmps.IsScalar, "Cell: invalid value returned: expected:20, found:" + tmps.GetValue(0));
            Assert.IsTrue(tmps.Equals(20), "Cell: invalid value returned: expected:20, found:" + tmps.GetValue(0));

            Array<double> tmp = cellresult_vect.GetArray<double>(1);
            Assert.IsTrue(tmp.IsVector, "Cell: invalid value returned: expected double vector 10:2:20");
            Assert.IsTrue((bool)(tmp.Length == 91), "Cell: invalid value returned: expected double vector 10:2:20");
            Assert.IsTrue((bool)(tmp[0] == 10), "Cell: invalid value returned: expected double vector 10:2:20");
            Assert.IsTrue((bool)(tmp["end"] == 100), "Cell: invalid value returned: expected double vector 10:2:20");

            tmp = cellresult_vect.GetArray<double>(2);
            Assert.IsTrue(tmp.IsVector, "Cell: invalid value returned: expected vector 20:2:30");
            Assert.IsTrue((bool)(tmp.Length == 91), "Cell: invalid value returned: expected vector 20:2:30");
            Assert.IsTrue((bool)(tmp[0] == -10), "Cell: invalid value returned: expected vector 20:2:30");
            Assert.IsTrue((bool)(tmp["end"] == -100), "Cell: invalid value returned: expected vector 20:2:30");

            tmps = cellresult_vect.GetArray<double>(4);
            Assert.IsTrue(tmps.IsScalar, "Cell: invalid value returned: expected int 24, found: " + tmps.GetValue(0));
            Assert.IsTrue((bool)(tmps["end"] == 24), "Cell: invalid value returned: expected int 24, found: " + tmps.GetValue(0));

            cellresult_vect[0, 3, 1, 0, 1] = -111.0;


            //if (cellresult_vect.GetValue<double>(0, 3, 1, 0, 1) != -111.0)
            //    throw new Exception("Cell: invalid result of inner element index access: expected: -111.0, found: " + (cellresult_vect.GetArray<double>(0, 3, 1, 0, 1)).GetValue(0));
            // was replaced by ... however, can't take over message
            Assert.AreEqual(cellresult_vect.GetCell(0, 3).GetArray<double>(1, 0).GetValue(1), -111.0, "Cell: invalid result of inner element index access: expected: -111.0");


            Assert.IsTrue(A.GetCell(3,1).IsTypeOf<double>(1,0), "Cell: original should not be altered if reference was altered!");
            Assert.AreEqual(A.GetCell(3,1).GetArray<double>(1,0).GetValue(1), 22.0, "Cell: original should not be altered if reference was altered!");

            // test subarray access: matrix (sequential)   **********************************************

            ind = vector<double>(20.0, 21.0, 22.0, 23.0, 24.0, 25.0).Reshape(3, 2);
            cellresult_vect = A[ind];


            Assert.IsTrue(cellresult_vect.IsMatrix, "Cell: index access dimension mismatch: should be Cell 3x2! ");
            Assert.IsTrue(cellresult_vect.Size[0].Equals(3), "Cell: index access dimension mismatch: should be Cell 3x2! ");
            Assert.IsTrue(cellresult_vect.Size[1].Equals(2), "Cell: index access dimension mismatch: should be Cell 3x2! ");

            Assert.IsTrue(cellresult_vect.IsTypeOf<double>(0, 0), "Cell: index access failed. Inner element mismatch for vector indices.");
            Assert.IsTrue(cellresult_vect.IsTypeOf<double>(1, 0), "Cell: index access failed. Inner element mismatch for vector indices.");
            Assert.IsTrue(cellresult_vect.IsTypeOf<double>(2, 0), "Cell: index access failed. Inner element mismatch for vector indices.");
            Assert.IsTrue(cellresult_vect.IsTypeOf<Cell>(0, 1), "Cell: index access failed. Inner element mismatch for vector indices.");
            Assert.IsTrue(cellresult_vect.IsTypeOf<double>(1, 1), "Cell: index access failed. Inner element mismatch for vector indices.");
            Assert.IsTrue(cellresult_vect.IsTypeOf<double>(2, 1), "Cell: index access failed. Inner element mismatch for vector indices.");



            Assert.IsTrue(cellresult_vect.IsTypeOf<double>(0), "Cell: index access failed. Inner element mismatch for vector indices.");
            Assert.IsTrue(cellresult_vect.IsTypeOf<double>(1), "Cell: index access failed. Inner element mismatch for vector indices.");
            Assert.IsTrue(cellresult_vect.IsTypeOf<double>(2), "Cell: index access failed. Inner element mismatch for vector indices.");
            Assert.IsTrue(cellresult_vect.IsTypeOf<Cell>(3), "Cell: index access failed. Inner element mismatch for vector indices.");
            Assert.IsTrue(cellresult_vect.IsTypeOf<double>(4), "Cell: index access failed. Inner element mismatch for vector indices.");
            Assert.IsTrue(cellresult_vect.IsTypeOf<double>(5), "Cell: index access failed. Inner element mismatch for vector indices.");



            // test if elements returned are properly referenced (detached from original)
            tmps = cellresult_vect.GetArray<double>(0, 0);
            Assert.IsTrue(tmps.IsScalar, "Cell: invalid value returned: expected:20, found:" + tmps.GetValue(0));
            Assert.IsTrue((byte)tmps == 20.0, "Cell: invalid value returned: expected:20, found:" + tmps.GetValue(0));

            tmp = cellresult_vect.GetArray<double>(1);
            Assert.IsTrue(tmp.IsVector, "Cell: invalid value returned: expected double vector 10:2:20");
            Assert.IsTrue(tmp.Length.Equals(91), "Cell: invalid value returned: expected double vector 10:2:20");
            Assert.IsTrue(tmp[0].Equals(10.0), "Cell: invalid value returned: expected double vector 10:2:20");
            Assert.IsTrue(tmp["end"].Equals(100.0), "Cell: invalid value returned: expected double vector 10:2:20");

            tmp = cellresult_vect.GetArray<double>(2);
            Assert.IsTrue(tmp.IsVector, "Cell: invalid value returned: expected vector 10:2:30");
            Assert.IsTrue(tmp.Length.Equals(91), "Cell: invalid value returned: expected vector 10:2:30");
            Assert.IsTrue(tmp[0].Equals(-10.0), "Cell: invalid value returned: expected vector 10:2:30");
            Assert.IsTrue(tmp["end"].Equals(-100.0), "Cell: invalid value returned: expected vector 10:2:30");

            tmps = cellresult_vect.GetArray<double>(4);
            Assert.IsTrue(tmps.IsScalar, "Cell: invalid value returned: expected int 24, found: " + tmps.GetValue(0));
            Assert.IsTrue((byte)tmps["end"] == 24.0, "Cell: invalid value returned: expected int 24, found: " + tmps.GetValue(0));

            cellresult_vect[0, 1, 1, 0, 1] = -111.0;
            Assert.AreEqual(cellresult_vect.GetCell(0,1).GetArray<double>(1, 0).GetValue(1), -111.0, "Cell: invalid result of inner element index access: expected: -111.0");
            //if (cellresult_vect.GetValue<double>(0, 1, 1, 0, 1) != -111.0)
            //    throw new Exception("Cell: invalid result of inner element index access: expected: -111.0, found: " + cellresult_vect.GetValue<double>(0, 3, 1, 0, 1));

            // test if original still untouched 
            Assert.AreEqual(A.GetCell(3,1).GetArray<double>(1,0).GetValue(1), 22.0, "Cell: original should not be altered when reference is altered!");
            //if (A.GetValue<double>(3, 1, 1, 0, 1) != 22.0)
            //    throw new Exception("Cell: original should not be altered if reference was altered! A[3,1,1,0,1] = " + A.GetValue<double>(3, 1, 1, 0, 1));

            // test sequential index array set access 
            innerCell = cell(size(4, 3, 2));
            innerCell[0] = arange(20.0, -3, -100);
            innerCell[1] = innerCell.GetArray<double>(0) < 0.0;
            Cell rangedsource = cell(size(2, 2), vector<BaseArray>(
                    vector<int>(3333),
                    ind > 0,
                    arange(1000.0, 2000),
                    innerCell));
            Array<double> range = vector<double>( 0, 51, 52, 59 );
            A[range] = rangedsource;
            Assert.IsTrue(A.IsTypeOf<int>(0), "Cell: seq.ranged set mismatch. index 0");
            Assert.IsTrue(A.IsTypeOf<bool>(51), "Cell: seq.ranged set mismatch. index 1");
            Assert.IsTrue(A.IsTypeOf<double>(52), "Cell: seq.ranged set mismatch. index 2");
            Assert.IsTrue(A.IsTypeOf<Cell>(59), "Cell: seq.ranged set mismatch. index 3");


            // test detached referencing (alter elements of query result)
            // test index access for ranges via BaseArray[]
            data = new BaseArray[60];
            for (int i = 0; i < data.Length; i++)
                data[i] = vector(i);
            A = cell(size(20, 3), data);
            Array<double> r = arange(0.0, 2);
            Array<int> c = vector<int>(1).Reshape(1, 1);
            Cell rangeGet = A[r, c];
            Assert.IsTrue(rangeGet.Size[0].Equals(3), "Cell: ranged index get access via BaseArray failed. Wrong dimension returned.");
            Assert.IsTrue(rangeGet.Size[1].Equals(1), "Cell: ranged index get access via BaseArray failed. Wrong dimension returned.");

            Assert.IsTrue(rangeGet.GetArray<int>(0, 0).Equals(20), "Cell: invalid index returned: exp.20, found:" + (rangeGet.GetArray<int>(0, 0)).GetValue(0));
            A[0, 1] = vector<double>(17, 18, 1000).Reshape(1,3);
            //Assert.IsTrue(rangeGet.GetArray<int>(0, 0).Equals(20), "Cell: invalid index returned: exp.20, found:" + (rangeGet.GetArray<double>(0, 0)).GetValue(0) + ". The reference returned was not detached properly!");

            // test ranged setter 
            A[c, r] = A[c + 10, r];
            Assert.IsTrue(A.GetArray<int>(1, 1).Equals(31), "Wrong value found after copying part of cell to other position.");

            A[1, 1] = -1122.0;
            Assert.IsTrue(A.GetArray<int>(11, 1).Equals(31), "wrong value stored or reference not properly un-linked in Cell.");
            Assert.IsTrue(A.GetArray<double>(1, 1).Equals(-1122.0), "wrong value stored or reference not properly un-linked in Cell.");

            // test removal of elements 

            A[vector<double>( 4.0, 5.0, 11.0), ":"] = null;
            Assert.IsTrue(A.Size[0].Equals(17), "Cell removal via BaseArray[] failed.");
            Assert.IsTrue(A.Size[1].Equals(3), "Cell removal via BaseArray[] failed.");

            A[vector<double>(4.0, 5.0, 11.0)] = null;
            Assert.IsTrue(A.Size[0].Equals(48), "Cell removal via sequential indexes failed!");
            Assert.IsTrue(A.Size[1].Equals(1), "Cell removal via sequential indexes failed!");


            // test if error on removal will restore old dimensions 
            A = cell(size(2, 3, 2));

            ind = new double[] { 15, 16, 22, 223 };

            Assert.IsTrue(A.Size[0].Equals(2), "Cell: error on remove should restore old dimensions!");
            Assert.IsTrue(A.Size[1].Equals(3), "Cell: error on remove should restore old dimensions!");
            Assert.IsTrue(A.Size[2].Equals(2), "Cell: error on remove should restore old dimensions!");

        }

    }
}
