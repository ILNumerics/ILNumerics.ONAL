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
//*> \brief \b SGER 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SGER(M,N,ALPHA,X,INCX,Y,INCY,A,LDA) 
//* 
//*       .. Scalar Arguments .. 
//*       REAL ALPHA 
//*       INTEGER INCX,INCY,LDA,M,N 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL A(LDA,*),X(*),Y(*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SGER   performs the rank 1 operation 
//*> 
//*>    A := alpha*x*y**T + A, 
//*> 
//*> where alpha is a scalar, x is an m element vector, y is an n element 
//*> vector and A is an m by n matrix. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>           On entry, M specifies the number of rows of the matrix A. 
//*>           M must be at least zero. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>           On entry, N specifies the number of columns of the matrix A. 
//*>           N must be at least zero. 
//*> \endverbatim 
//*> 
//*> \param[in] ALPHA 
//*> \verbatim 
//*>          ALPHA is REAL 
//*>           On entry, ALPHA specifies the scalar alpha. 
//*> \endverbatim 
//*> 
//*> \param[in] X 
//*> \verbatim 
//*>          X is REAL array, dimension at least 
//*>           ( 1 + ( m - 1 )*abs( INCX ) ). 
//*>           Before entry, the incremented array X must contain the m 
//*>           element vector x. 
//*> \endverbatim 
//*> 
//*> \param[in] INCX 
//*> \verbatim 
//*>          INCX is INTEGER 
//*>           On entry, INCX specifies the increment for the elements of 
//*>           X. INCX must not be zero. 
//*> \endverbatim 
//*> 
//*> \param[in] Y 
//*> \verbatim 
//*>          Y is REAL array, dimension at least 
//*>           ( 1 + ( n - 1 )*abs( INCY ) ). 
//*>           Before entry, the incremented array Y must contain the n 
//*>           element vector y. 
//*> \endverbatim 
//*> 
//*> \param[in] INCY 
//*> \verbatim 
//*>          INCY is INTEGER 
//*>           On entry, INCY specifies the increment for the elements of 
//*>           Y. INCY must not be zero. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is REAL array, dimension ( LDA, N ) 
//*>           Before entry, the leading m by n part of the array A must 
//*>           contain the matrix of coefficients. On exit, A is 
//*>           overwritten by the updated matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>           On entry, LDA specifies the first dimension of A as declared 
//*>           in the calling (sub) program. LDA must be at least 
//*>           max( 1, m ). 
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
//*> \date December 2016 
//* 
//*> \ingroup single_blas_level2 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  Level 2 Blas routine. 
//*> 
//*>  -- Written on 22-October-1986. 
//*>     Jack Dongarra, Argonne National Lab. 
//*>     Jeremy Du Croz, Nag Central Office. 
//*>     Sven Hammarling, Nag Central Office. 
//*>     Richard Hanson, Sandia National Labs. 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _wlowjtxr(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Single _r7cfteg3, Single* _ta7zuy9k, ref Int32 _1eqjusqc, Single* _f3z3edv0, ref Int32 _bbrxgmj7, Single* _vxfgpup9, ref Int32 _ocv8fk5c)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Single _1ajfmh55 =  default;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _b69ritwm =  default;
Int32 _znpjgsef =  default;
Int32 _lwc63p7q =  default;
Int32 _ziknm33t =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- Reference BLAS level2 routine (version 3.7.0) -- 
		//*  -- Reference BLAS is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     December 2016 
		//* 
		//*     .. Scalar Arguments .. 
		//*     .. 
		//*     .. Array Arguments .. 
		//*     .. 
		//* 
		//*  ===================================================================== 
		//* 
		//*     .. Parameters .. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//* 
		//*     Test the input parameters. 
		//* 
		
		_gro5yvfo = (int)0;
		if (_ev4xhht5 < (int)0)
		{
			
			_gro5yvfo = (int)1;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)2;
		}
		else
		if (_1eqjusqc == (int)0)
		{
			
			_gro5yvfo = (int)5;
		}
		else
		if (_bbrxgmj7 == (int)0)
		{
			
			_gro5yvfo = (int)7;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ))
		{
			
			_gro5yvfo = (int)9;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("SGER  " ,ref _gro5yvfo );
			return;
		}
		//* 
		//*     Quick return if possible. 
		//* 
		
		if (((_ev4xhht5 == (int)0) | (_dxpq0xkr == (int)0)) | (_r7cfteg3 == _d0547bi2))
		return;//* 
		//*     Start the operations. In this version the elements of A are 
		//*     accessed sequentially with one pass through A. 
		//* 
		
		if (_bbrxgmj7 > (int)0)
		{
			
			_lwc63p7q = (int)1;
		}
		else
		{
			
			_lwc63p7q = ((int)1 - ((_dxpq0xkr - (int)1) * _bbrxgmj7));
		}
		
		if (_1eqjusqc == (int)1)
		{
			
			{
				System.Int32 __81fgg2dlsvn779 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step779 = (System.Int32)((int)1);
				System.Int32 __81fgg2count779;
				for (__81fgg2count779 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn779 + __81fgg2step779) / __81fgg2step779)), _znpjgsef = __81fgg2dlsvn779; __81fgg2count779 != 0; __81fgg2count779--, _znpjgsef += (__81fgg2step779)) {

				{
					
					if (*(_f3z3edv0+(_lwc63p7q - 1)) != _d0547bi2)
					{
						
						_1ajfmh55 = (_r7cfteg3 * *(_f3z3edv0+(_lwc63p7q - 1)));
						{
							System.Int32 __81fgg2dlsvn780 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step780 = (System.Int32)((int)1);
							System.Int32 __81fgg2count780;
							for (__81fgg2count780 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn780 + __81fgg2step780) / __81fgg2step780)), _b5p6od9s = __81fgg2dlsvn780; __81fgg2count780 != 0; __81fgg2count780--, _b5p6od9s += (__81fgg2step780)) {

							{
								
								*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) + (*(_ta7zuy9k+(_b5p6od9s - 1)) * _1ajfmh55));
Mark10:;
								// continue
							}
														}						}
					}
					
					_lwc63p7q = (_lwc63p7q + _bbrxgmj7);
Mark20:;
					// continue
				}
								}			}
		}
		else
		{
			
			if (_1eqjusqc > (int)0)
			{
				
				_ziknm33t = (int)1;
			}
			else
			{
				
				_ziknm33t = ((int)1 - ((_ev4xhht5 - (int)1) * _1eqjusqc));
			}
			
			{
				System.Int32 __81fgg2dlsvn781 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step781 = (System.Int32)((int)1);
				System.Int32 __81fgg2count781;
				for (__81fgg2count781 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn781 + __81fgg2step781) / __81fgg2step781)), _znpjgsef = __81fgg2dlsvn781; __81fgg2count781 != 0; __81fgg2count781--, _znpjgsef += (__81fgg2step781)) {

				{
					
					if (*(_f3z3edv0+(_lwc63p7q - 1)) != _d0547bi2)
					{
						
						_1ajfmh55 = (_r7cfteg3 * *(_f3z3edv0+(_lwc63p7q - 1)));
						_b69ritwm = _ziknm33t;
						{
							System.Int32 __81fgg2dlsvn782 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step782 = (System.Int32)((int)1);
							System.Int32 __81fgg2count782;
							for (__81fgg2count782 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn782 + __81fgg2step782) / __81fgg2step782)), _b5p6od9s = __81fgg2dlsvn782; __81fgg2count782 != 0; __81fgg2count782--, _b5p6od9s += (__81fgg2step782)) {

							{
								
								*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) + (*(_ta7zuy9k+(_b69ritwm - 1)) * _1ajfmh55));
								_b69ritwm = (_b69ritwm + _1eqjusqc);
Mark30:;
								// continue
							}
														}						}
					}
					
					_lwc63p7q = (_lwc63p7q + _bbrxgmj7);
Mark40:;
					// continue
				}
								}			}
		}
		//* 
		
		return;//* 
		//*     End of SGER  . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
