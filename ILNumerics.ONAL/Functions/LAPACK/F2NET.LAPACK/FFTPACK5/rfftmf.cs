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
public static unsafe partial class FFTPACK5 {
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

	 
	public static void _xlz6l0rm(ref Int32 _b6wzgw7j, ref Int32 _zaw1d3zs, ref Int32 _99xbntpe, ref Int32 _x7l3p3wq, Single* _wkmvx6rb, ref Int32 _ifgqgx1i, Single* _uuvyil0u, ref Int32 _xjpd3das, Single* _y1uc90sp, ref Int32 _sybtf40y, ref Int32 _va7gplz4)
	{
#region variable declarations
string fLanavab = default;
#endregion  variable declarations

	{
		//C 
		
		_va7gplz4 = (int)0;//C 
		
		if (_ifgqgx1i < ((((_b6wzgw7j - (int)1) * _zaw1d3zs) + (_x7l3p3wq * (_99xbntpe - (int)1))) + (int)1))
		{
			
			_va7gplz4 = (int)1;
			_yc1iu9qu("RFFTMF " ,ref Unsafe.AsRef((int)6) );
		}
		else
		if (_xjpd3das < ((_99xbntpe + ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.REAL(_99xbntpe ) ) / ILNumerics.F2NET.Intrinsics.LOG((float)2 ) )) + (int)4))
		{
			
			_va7gplz4 = (int)2;
			_yc1iu9qu("RFFTMF " ,ref Unsafe.AsRef((int)8) );
		}
		else
		if (_sybtf40y < (_b6wzgw7j * _99xbntpe))
		{
			
			_va7gplz4 = (int)3;
			_yc1iu9qu("RFFTMF " ,ref Unsafe.AsRef((int)10) );
		}
		else
		if (!(_kzi0pmdc(ref _x7l3p3wq ,ref _zaw1d3zs ,ref _99xbntpe ,ref _b6wzgw7j )))
		{
			
			_va7gplz4 = (int)4;
			_yc1iu9qu("RFFTMF " ,ref Unsafe.AsRef((int)-1) );
		}
		//C 
		
		if (_99xbntpe == (int)1)
		return;//C 
		
		_yswewipq(ref _b6wzgw7j ,ref _zaw1d3zs ,ref _99xbntpe ,ref _x7l3p3wq ,_wkmvx6rb ,_y1uc90sp ,_uuvyil0u ,(_uuvyil0u+(_99xbntpe + (int)1 - 1)));
		return;
	}
	
	} // 177

} // end class 
} // end namespace
#endif
