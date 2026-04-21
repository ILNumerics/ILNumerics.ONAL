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

	 
	public static void _szbmqxuz(ref Int32 _4djnmv2o, ref Int32 _jejunmnd, ref Int32 _cjahrwwv, ref Int32 _cf4kqqk2, Single* _wkmvx6rb, Single* _13s1ly0n)
	{
#region variable declarations
Int32 _qb0uu4i2 =  default;
Int32 _ld42zxsk =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//      dimension r(ldr,*),w(ldw,*)
		
		{
			System.Int32 __81fgg2dlsvn424 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step424 = (System.Int32)((int)1);
			System.Int32 __81fgg2count424;
			for (__81fgg2count424 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cf4kqqk2) - __81fgg2dlsvn424 + __81fgg2step424) / __81fgg2step424)), _qb0uu4i2 = __81fgg2dlsvn424; __81fgg2count424 != 0; __81fgg2count424--, _qb0uu4i2 += (__81fgg2step424)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn425 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step425 = (System.Int32)((int)1);
					System.Int32 __81fgg2count425;
					for (__81fgg2count425 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cjahrwwv) - __81fgg2dlsvn425 + __81fgg2step425) / __81fgg2step425)), _ld42zxsk = __81fgg2dlsvn425; __81fgg2count425 != 0; __81fgg2count425--, _ld42zxsk += (__81fgg2step425)) {

					{
						
						*(_13s1ly0n+(_ld42zxsk - 1) + (_qb0uu4i2 - 1) * 1 * (_jejunmnd)) = *(_wkmvx6rb+(_ld42zxsk - 1) + (_qb0uu4i2 - 1) * 1 * (_4djnmv2o));
					}
										}				}
			}
						}		}
		return;
	}
	
	} // 177

} // end class 
} // end namespace
#endif
