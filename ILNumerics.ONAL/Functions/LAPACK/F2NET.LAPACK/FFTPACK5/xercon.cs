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

	 
	public static Boolean _kzi0pmdc(ref Int32 _x7l3p3wq, ref Int32 _zaw1d3zs, ref Int32 _99xbntpe, ref Int32 _b6wzgw7j)
	{
#region variable declarations
Boolean _kzi0pmdc = default;
Int32 _ld42zxsk =  default;
Int32 _qb0uu4i2 =  default;
Int32 _xodp4m6h =  default;
Int32 _s2r7laqz =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//C 
		//C     Definition: positive integers INC, JUMP, N and LOT are consistent  
		//C                                                            ---------- 
		//C     if I1*INC + J1*JUMP = I2*INC + J2*JUMP for I1,I2 < N and J1,J2  
		//C     < LOT implies I1=I2 and J1=J2. 
		//C 
		//C     For multiple FFTs to execute correctly, input parameters INC,  
		//C     JUMP, N and LOT must be consistent ... otherwise at least one  
		//C     array element mistakenly is transformed more than once. 
		//C 
		//C     XERCON = .TRUE. if and only if INC, JUMP, N and LOT are  
		//C     consistent. 
		//C 
		//C     ------------------------------------------------------------------ 
		//C 
		//C     Compute I = greatest common divisor (INC, JUMP) 
		//C 
		
		_ld42zxsk = _x7l3p3wq;
		_qb0uu4i2 = _zaw1d3zs;
Mark10:;
		// continue
		if (_qb0uu4i2 != (int)0)
		{
			
			_xodp4m6h = ILNumerics.F2NET.Intrinsics.MOD(_ld42zxsk ,_qb0uu4i2 );
			_ld42zxsk = _qb0uu4i2;
			_qb0uu4i2 = _xodp4m6h;goto Mark10;
		}
		//C 
		//C Compute LCM = least common multiple (INC, JUMP) 
		//C 
		
		_s2r7laqz = ((_x7l3p3wq * _zaw1d3zs) / _ld42zxsk);//C 
		//C Check consistency of INC, JUMP, N, LOT 
		//C 
		
		if ((_s2r7laqz <= ((_99xbntpe - (int)1) * _x7l3p3wq)) & (_s2r7laqz <= ((_b6wzgw7j - (int)1) * _zaw1d3zs)))
		{
			
			_kzi0pmdc = false;
		}
		else
		{
			
			_kzi0pmdc = true;
		}
		//C 
		
		return _kzi0pmdc;
	}
	
	return _kzi0pmdc;
	} // 177

} // end class 
} // end namespace
#endif
