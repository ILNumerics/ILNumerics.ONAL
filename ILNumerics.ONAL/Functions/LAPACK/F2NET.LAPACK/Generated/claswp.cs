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
public static unsafe partial class LAPACK {
//*> \brief \b CLASWP performs a series of row interchanges on a general rectangular matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CLASWP + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/claswp.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/claswp.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/claswp.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CLASWP( N, A, LDA, K1, K2, IPIV, INCX ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INCX, K1, K2, LDA, N 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IPIV( * ) 
//*       COMPLEX            A( LDA, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CLASWP performs a series of row interchanges on the matrix A. 
//*> One row interchange is initiated for each of rows K1 through K2 of A. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of columns of the matrix A. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is COMPLEX array, dimension (LDA,N) 
//*>          On entry, the matrix of column dimension N to which the row 
//*>          interchanges will be applied. 
//*>          On exit, the permuted matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A. 
//*> \endverbatim 
//*> 
//*> \param[in] K1 
//*> \verbatim 
//*>          K1 is INTEGER 
//*>          The first element of IPIV for which a row interchange will 
//*>          be done. 
//*> \endverbatim 
//*> 
//*> \param[in] K2 
//*> \verbatim 
//*>          K2 is INTEGER 
//*>          (K2-K1+1) is the number of elements of IPIV for which a row 
//*>          interchange will be done. 
//*> \endverbatim 
//*> 
//*> \param[in] IPIV 
//*> \verbatim 
//*>          IPIV is INTEGER array, dimension (K1+(K2-K1)*abs(INCX)) 
//*>          The vector of pivot indices. Only the elements in positions 
//*>          K1 through K1+(K2-K1)*abs(INCX) of IPIV are accessed. 
//*>          IPIV(K1+(K-K1)*abs(INCX)) = L implies rows K and L are to be 
//*>          interchanged. 
//*> \endverbatim 
//*> 
//*> \param[in] INCX 
//*> \verbatim 
//*>          INCX is INTEGER 
//*>          The increment between successive values of IPIV. If INCX 
//*>          is negative, the pivots are applied in reverse order. 
//*> \endverbatim 
//* 
//*  Authors: 
//*  ======== 
//* 
//*> \author Univ. of Tennessee 
//*> \author Univ. of California Berkeley 
//*> \author Univ. of Colorado Denver 
//*> \author NAG Ltd. 
//* 
//*> \date June 2017 
//* 
//*> \ingroup complexOTHERauxiliary 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  Modified by 
//*>   R. C. Whaley, Computer Science Dept., Univ. of Tenn., Knoxville, USA 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _iuxulk3d(ref Int32 _dxpq0xkr, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, ref Int32 _psyg0fin, ref Int32 _bddf4r7h, Int32* _w1ilvusp, ref Int32 _1eqjusqc)
	{
#region variable declarations
Int32 _b5p6od9s =  default;
Int32 _egqdmelt =  default;
Int32 _8ur10vsh =  default;
Int32 _dlh3qgsb =  default;
Int32 _8t9w2q8d =  default;
Int32 _b69ritwm =  default;
Int32 _mut8jzc1 =  default;
Int32 _znpjgsef =  default;
Int32 _umlkckdg =  default;
Int32 _nhhnxdcx =  default;
fcomplex _1ajfmh55 =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.1) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     June 2017 
		//* 
		//*     .. Scalar Arguments .. 
		//*     .. 
		//*     .. Array Arguments .. 
		//*     .. 
		//* 
		//* ===================================================================== 
		//* 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Interchange row I with row IPIV(K1+(I-K1)*abs(INCX)) for each of rows 
		//*     K1 through K2. 
		//* 
		
		if (_1eqjusqc > (int)0)
		{
			
			_mut8jzc1 = _psyg0fin;
			_egqdmelt = _psyg0fin;
			_8ur10vsh = _bddf4r7h;
			_dlh3qgsb = (int)1;
		}
		else
		if (_1eqjusqc < (int)0)
		{
			
			_mut8jzc1 = (_psyg0fin + ((_psyg0fin - _bddf4r7h) * _1eqjusqc));
			_egqdmelt = _bddf4r7h;
			_8ur10vsh = _psyg0fin;
			_dlh3qgsb = (int)-1;
		}
		else
		{
			
			return;
		}
		//* 
		
		_nhhnxdcx = ((_dxpq0xkr / (int)32) * (int)32);
		if (_nhhnxdcx != (int)0)
		{
			
			{
				System.Int32 __81fgg2dlsvn1784 = (System.Int32)((int)1);
				System.Int32 __81fgg2step1784 = (System.Int32)((int)32);
				System.Int32 __81fgg2count1784;
				for (__81fgg2count1784 = System.Math.Max(0, (System.Int32)(((System.Int32)(_nhhnxdcx) - __81fgg2dlsvn1784 + __81fgg2step1784) / __81fgg2step1784)), _znpjgsef = __81fgg2dlsvn1784; __81fgg2count1784 != 0; __81fgg2count1784--, _znpjgsef += (__81fgg2step1784)) {

				{
					
					_b69ritwm = _mut8jzc1;
					{
						System.Int32 __81fgg2dlsvn1785 = (System.Int32)(_egqdmelt);
						System.Int32 __81fgg2step1785 = (System.Int32)(_dlh3qgsb);
						System.Int32 __81fgg2count1785;
						for (__81fgg2count1785 = System.Math.Max(0, (System.Int32)(((System.Int32)(_8ur10vsh) - __81fgg2dlsvn1785 + __81fgg2step1785) / __81fgg2step1785)), _b5p6od9s = __81fgg2dlsvn1785; __81fgg2count1785 != 0; __81fgg2count1785--, _b5p6od9s += (__81fgg2step1785)) {

						{
							
							_8t9w2q8d = *(_w1ilvusp+(_b69ritwm - 1));
							if (_8t9w2q8d != _b5p6od9s)
							{
								
								{
									System.Int32 __81fgg2dlsvn1786 = (System.Int32)(_znpjgsef);
									const System.Int32 __81fgg2step1786 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1786;
									for (__81fgg2count1786 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef + (int)31) - __81fgg2dlsvn1786 + __81fgg2step1786) / __81fgg2step1786)), _umlkckdg = __81fgg2dlsvn1786; __81fgg2count1786 != 0; __81fgg2count1786--, _umlkckdg += (__81fgg2step1786)) {

									{
										
										_1ajfmh55 = *(_vxfgpup9+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c));
										*(_vxfgpup9+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) = *(_vxfgpup9+(_8t9w2q8d - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c));
										*(_vxfgpup9+(_8t9w2q8d - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) = _1ajfmh55;
Mark10:;
										// continue
									}
																		}								}
							}
							
							_b69ritwm = (_b69ritwm + _1eqjusqc);
Mark20:;
							// continue
						}
												}					}
Mark30:;
					// continue
				}
								}			}
		}
		
		if (_nhhnxdcx != _dxpq0xkr)
		{
			
			_nhhnxdcx = (_nhhnxdcx + (int)1);
			_b69ritwm = _mut8jzc1;
			{
				System.Int32 __81fgg2dlsvn1787 = (System.Int32)(_egqdmelt);
				System.Int32 __81fgg2step1787 = (System.Int32)(_dlh3qgsb);
				System.Int32 __81fgg2count1787;
				for (__81fgg2count1787 = System.Math.Max(0, (System.Int32)(((System.Int32)(_8ur10vsh) - __81fgg2dlsvn1787 + __81fgg2step1787) / __81fgg2step1787)), _b5p6od9s = __81fgg2dlsvn1787; __81fgg2count1787 != 0; __81fgg2count1787--, _b5p6od9s += (__81fgg2step1787)) {

				{
					
					_8t9w2q8d = *(_w1ilvusp+(_b69ritwm - 1));
					if (_8t9w2q8d != _b5p6od9s)
					{
						
						{
							System.Int32 __81fgg2dlsvn1788 = (System.Int32)(_nhhnxdcx);
							const System.Int32 __81fgg2step1788 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1788;
							for (__81fgg2count1788 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1788 + __81fgg2step1788) / __81fgg2step1788)), _umlkckdg = __81fgg2dlsvn1788; __81fgg2count1788 != 0; __81fgg2count1788--, _umlkckdg += (__81fgg2step1788)) {

							{
								
								_1ajfmh55 = *(_vxfgpup9+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c));
								*(_vxfgpup9+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) = *(_vxfgpup9+(_8t9w2q8d - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c));
								*(_vxfgpup9+(_8t9w2q8d - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) = _1ajfmh55;
Mark40:;
								// continue
							}
														}						}
					}
					
					_b69ritwm = (_b69ritwm + _1eqjusqc);
Mark50:;
					// continue
				}
								}			}
		}
		//* 
		
		return;//* 
		//*     End of CLASWP 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
