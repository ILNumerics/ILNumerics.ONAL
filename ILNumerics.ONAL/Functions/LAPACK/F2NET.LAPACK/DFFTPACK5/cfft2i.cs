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
//  This is an auto - generated source file.                    //
//  Do not manually edit this file! Any changes made will be    //
//  lost on the next build! Edit the corresponding source file  //
//  (per default located in the template/ folder) instead!      //
//                                                              //
//////////////////////////////////////////////////////////////////

#pragma warning disable CS0164, CS0219, CS0162
#if !OBSOLETE
using System;
using System.Security;
using System.IO;
using System.Collections.Generic;
using ILNumerics.F2NET.Formatting;
using ILNumerics.Core.MemoryLayer;
using ILNumerics.Core.Runtime;
using static ILNumerics.F2NET.Intrinsics; 
using static ILNumerics.F2NET.Array.Intrinsics; 
using System.Runtime.CompilerServices; 
using static ILNumerics.Globals;
using ILNumerics.F2NET.Array; 

namespace ILNumerics.F2NET { 
public static unsafe partial class DFFTPACK5 {
//C     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
//C     *                                                               * 
//C     *                  copyright (c) 2011 by UCAR                   * 
//C     *                                                               * 
//C     *       University Corporation for Atmospheric Research         * 
//C     *                                                               * 
//C     *                      all rights reserved                      * 
//C     *                                                               * 
//C     *                     FFTPACK  version 5.1                      * 
//C     *                                                               * 
//C     *                 A Fortran Package of Fast Fourier             * 
//C     *                                                               * 
//C     *                Subroutines and Example Programs               * 
//C     *                                                               * 
//C     *                             by                                * 
//C     *                                                               * 
//C     *               Paul Swarztrauber and Dick Valent               * 
//C     *                                                               * 
//C     *                             of                                * 
//C     *                                                               * 
//C     *         the National Center for Atmospheric Research          * 
//C     *                                                               * 
//C     *                Boulder, Colorado  (80307)  U.S.A.             * 
//C     *                                                               * 
//C     *                   which is sponsored by                       * 
//C     *                                                               * 
//C     *              the National Science Foundation                  * 
//C     *                                                               * 
//C     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
//C 

	 
	public static void _11s6a9z2(ref Int32 _cjahrwwv, ref Int32 _cf4kqqk2, Double* _uuvyil0u, ref Int32 _xjpd3das, ref Int32 _va7gplz4)
	{
#region variable declarations
Int32 _fn23rpdf =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//C 
		//C Initialize error return 
		//C 
		
		_va7gplz4 = (int)0;//C 
		
		if (_xjpd3das < ((((((int)2 * _cjahrwwv) + ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.DBLE(_cjahrwwv ) ) / ILNumerics.F2NET.Intrinsics.LOG((double)2 ) )) + ((int)2 * _cf4kqqk2)) + ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.DBLE(_cf4kqqk2 ) ) / ILNumerics.F2NET.Intrinsics.LOG((double)2 ) )) + (int)8))
		{
			
			_va7gplz4 = (int)2;
			_yc1iu9qu("CFFT2I" ,ref Unsafe.AsRef((int)4) );goto Mark100;
		}
		//C 
		
		_d4ay1zl0(ref _cjahrwwv ,(_uuvyil0u+((int)1 - 1)),ref Unsafe.AsRef((((int)2 * _cjahrwwv) + ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.DBLE(_cjahrwwv ) ) / ILNumerics.F2NET.Intrinsics.LOG((double)2 ) )) + (int)4) ,ref _fn23rpdf );
		if (_fn23rpdf != (int)0)
		{
			
			_va7gplz4 = (int)20;
			_yc1iu9qu("CFFT2I" ,ref Unsafe.AsRef((int)-5) );goto Mark100;
		}
		
		_d4ay1zl0(ref _cf4kqqk2 ,(_uuvyil0u+((((int)2 * _cjahrwwv) + ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.DBLE(_cjahrwwv ) ) / ILNumerics.F2NET.Intrinsics.LOG((double)2 ) )) + (int)3 - 1)),ref Unsafe.AsRef((((int)2 * _cf4kqqk2) + ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.DBLE(_cf4kqqk2 ) ) / ILNumerics.F2NET.Intrinsics.LOG((double)2 ) )) + (int)4) ,ref _fn23rpdf );
		if (_fn23rpdf != (int)0)
		{
			
			_va7gplz4 = (int)20;
			_yc1iu9qu("CFFT2I" ,ref Unsafe.AsRef((int)-5) );
		}
		//C 
		
Mark100:;
		// continue
		return;
	}
	
	} // 177

} // end class 
} // end namespace
#endif
