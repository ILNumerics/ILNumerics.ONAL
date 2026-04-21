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
using System.Security;
using ILNumerics;
using ILNumerics.Core.Arrays;
using ILNumerics.Core.StorageLayer;
using static ILNumerics.Globals;

namespace ILNumerics.Core.Functions.Builtin {

    internal static partial class MathInternal {

        /// <summary>
        /// Creates a new logical array from the values provided in <paramref name="values"/> with the shape given by <paramref name="size"/>.
        /// </summary>
        /// <param name="values">Values to be copied to the new ILNumerics array.</param>
        /// <param name="size">The size of the new ILNumerics array. Number of elements must correspond to the number of values in <paramref name="values"/>.</param>
        /// <param name="order">[Optional] Storage order for the new ILNumerics array. Default: (<see cref="StorageOrders.ColumnMajor"/>).</param>
        /// <returns>A new ILNumerics array with copies of the given values and the given shape.</returns>
        /// <remarks>A copy is made from <paramref name="values"/>. The array <paramref name="values"/> is not referenced 
        /// by ILNumerics after the function returns.
        /// <para>Make sure that the number of elements configured by the dimension lengths in <paramref name="size"/>
        /// matches the number of elements in <paramref name="values"/>.</para>
        /// </remarks>
        /// <example>
        /// <code><![CDATA[Assert.IsTrue(logical(new[] { true }, size(1)) == true);]]></code></example>
        internal static Logical logical(bool[] values, InArray<long> size, StorageOrders order = StorageOrders.ColumnMajor) {
            using (Scope.Enter()) {
                return ((Logical)values).Reshape(size, order);
            }
        }

        /// <summary>
        /// Creates a new logical array with given shape and optional storage order.
        /// </summary>
        /// <param name="size">The size of the new array.</param>
        /// <param name="order">[Optional] Storage order for the new ILNumerics array. Default: (<see cref="Settings.DefaultStorageOrder"/>).</param>
        /// <param name="clear">[Optional] True: initialize the elements of the new logical with value 'False'. Default: true.</param>
        /// <returns>A new ILNumerics array with copies of the given values and the given shape.</returns>
        /// <remarks><para>Use this function to create and optionally initialize (large) logicals from scratch, outside of a binary operation and without 
        /// converting from a .NET array of booleans.</para></remarks>
        /// <example>
        /// <code><![CDATA[Logical L = logical(size(1000,2000), StorageOrders.RowMajor);]]></code></example>
        internal static Logical logical(InArray<long> size, StorageOrders? order = null, bool clear = true) {
            using (Scope.Enter(size)) {

                LogicalStorage L = LogicalStorage.Create();
                L.S.SetAll(size, order ?? Settings.DefaultStorageOrder);
                L.Handles[0] = DeviceManagement.DeviceManager.GetDevice(0).New<bool>((ulong)L.S.NumberOfElements, clear: clear); 

                return L.RetArray; 

            }
        }
    }
}
