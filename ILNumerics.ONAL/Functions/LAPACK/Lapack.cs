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
using ILNumerics;
using static ILNumerics.Globals;
using static ILNumerics.Core.Functions.Builtin.MathInternal;
using ILNumerics.Core.Functions.Builtin;
using ILNumerics.Core.Native;
using System.Security;

namespace ILNumerics {
    
    public partial class ILMath {

        static ILMath() {

            // causes intialization + detects for MKL
            if (MathInternal.Lapack == null) {
                MathInternal.Lapack = new ILNumerics.F2NET.ManagedLAPACK();
            }
            if (MathInternal.FFTImplementation == null) {
                MathInternal.FFTImplementation = new ILNumerics.F2NET.ManagedFFTPACK5(); 
            }
        }

        /// <summary>
        /// Lapack implementation.
        /// </summary>
        public static ILapack Lapack {

            get { return MathInternal.Lapack; }
            set { MathInternal.Lapack = value; }
        }
   }
}
