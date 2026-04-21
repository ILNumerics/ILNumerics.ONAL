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

	 
	public static void _atkgqkko(ref Int32 _99xbntpe, Double* _elimqrjs, ref Double _56ihyirc, Double* _rh59f4a7)
	{
#region variable declarations
Int32 _dwg2abd9 =  default;
Int32 _g15rvq6a =  default;
Int32 _1mqgnnli =  default;
Int32 _kr2fml4a =  default;
Int32 _zmzafydt =  default;
Int32 _7hieka8c =  default;
Int32 _ivvongrz =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//C 
		
		_4b22km0z(ref _99xbntpe ,ref _dwg2abd9 ,_rh59f4a7 );
		_56ihyirc = DBLE(_dwg2abd9);
		_g15rvq6a = (int)1;
		_1mqgnnli = (int)1;
		{
			System.Int32 __81fgg2dlsvn3 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step3 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3;
			for (__81fgg2count3 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dwg2abd9) - __81fgg2dlsvn3 + __81fgg2step3) / __81fgg2step3)), _kr2fml4a = __81fgg2dlsvn3; __81fgg2count3 != 0; __81fgg2count3--, _kr2fml4a += (__81fgg2step3)) {

			{
				
				_zmzafydt = INT(*(_rh59f4a7+(_kr2fml4a - 1)));
				_7hieka8c = (_1mqgnnli * _zmzafydt);
				_ivvongrz = (_99xbntpe / _7hieka8c);
				_wwtddqmv(ref _ivvongrz ,ref _zmzafydt ,(_elimqrjs+(_g15rvq6a - 1)));
				_g15rvq6a = (_g15rvq6a + ((_zmzafydt - (int)1) * (_ivvongrz + _ivvongrz)));
				_1mqgnnli = _7hieka8c;
Mark110:;
				// continue
			}
						}		}
		return;
	}
	
	} // 177

} // end class 
} // end namespace
#endif
