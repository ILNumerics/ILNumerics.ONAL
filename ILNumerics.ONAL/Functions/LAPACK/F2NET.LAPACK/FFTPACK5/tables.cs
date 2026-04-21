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

	 
	public static void _wwtddqmv(ref Int32 _ivvongrz, ref Int32 _zmzafydt, Single* _elimqrjs)
	{
#region variable declarations
Single _uur4gpcs =  default;
Single _oaxulsbg =  default;
Single _0ybgzhu7 =  default;
Int32 _qb0uu4i2 =  default;
Single _t991i9f1 =  default;
Int32 _ld42zxsk =  default;
Single _khwebsiw =  default;
Single _kbw1pd3m =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//C 
		
		_uur4gpcs = ((float)8 * ILNumerics.F2NET.Intrinsics.ATAN((float)1 ));
		_oaxulsbg = (_uur4gpcs / ILNumerics.F2NET.Intrinsics.REAL(_zmzafydt ));
		_0ybgzhu7 = (_uur4gpcs / ILNumerics.F2NET.Intrinsics.REAL(_ivvongrz * _zmzafydt ));
		{
			System.Int32 __81fgg2dlsvn1 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step1 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1;
			for (__81fgg2count1 = System.Math.Max(0, (System.Int32)(((System.Int32)(_zmzafydt) - __81fgg2dlsvn1 + __81fgg2step1) / __81fgg2step1)), _qb0uu4i2 = __81fgg2dlsvn1; __81fgg2count1 != 0; __81fgg2count1--, _qb0uu4i2 += (__81fgg2step1)) {

			{
				
				_t991i9f1 = (ILNumerics.F2NET.Intrinsics.REAL(_qb0uu4i2 - (int)1 ) * _0ybgzhu7);
				{
					System.Int32 __81fgg2dlsvn2 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2;
					for (__81fgg2count2 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ivvongrz) - __81fgg2dlsvn2 + __81fgg2step2) / __81fgg2step2)), _ld42zxsk = __81fgg2dlsvn2; __81fgg2count2 != 0; __81fgg2count2--, _ld42zxsk += (__81fgg2step2)) {

					{
						
						_khwebsiw = (ILNumerics.F2NET.Intrinsics.REAL(_ld42zxsk - (int)1 ) * _t991i9f1);
						*(_elimqrjs+(_ld42zxsk - 1) + (_qb0uu4i2 - (int)1 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * (_zmzafydt - (int)1)) = ILNumerics.F2NET.Intrinsics.COS(_khwebsiw );
						*(_elimqrjs+(_ld42zxsk - 1) + (_qb0uu4i2 - (int)1 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * (_zmzafydt - (int)1)) = ILNumerics.F2NET.Intrinsics.SIN(_khwebsiw );
Mark100:;
						// continue
					}
										}				}
				if (_zmzafydt <= (int)5)goto Mark110;
				_kbw1pd3m = (ILNumerics.F2NET.Intrinsics.REAL(_qb0uu4i2 - (int)1 ) * _oaxulsbg);
				*(_elimqrjs+((int)1 - 1) + (_qb0uu4i2 - (int)1 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * (_zmzafydt - (int)1)) = ILNumerics.F2NET.Intrinsics.COS(_kbw1pd3m );
				*(_elimqrjs+((int)1 - 1) + (_qb0uu4i2 - (int)1 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * (_zmzafydt - (int)1)) = ILNumerics.F2NET.Intrinsics.SIN(_kbw1pd3m );
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
