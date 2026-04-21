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
//*> \brief \b CGETRF 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CGETRF + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/cgetrf.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/cgetrf.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/cgetrf.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CGETRF( M, N, A, LDA, IPIV, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, LDA, M, N 
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
//*> CGETRF computes an LU factorization of a general M-by-N matrix A 
//*> using partial pivoting with row interchanges. 
//*> 
//*> The factorization has the form 
//*>    A = P * L * U 
//*> where P is a permutation matrix, L is lower triangular with unit 
//*> diagonal elements (lower trapezoidal if m > n), and U is upper 
//*> triangular (upper trapezoidal if m < n). 
//*> 
//*> This is the right-looking Level 3 BLAS version of the algorithm. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of rows of the matrix A.  M >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of columns of the matrix A.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is COMPLEX array, dimension (LDA,N) 
//*>          On entry, the M-by-N matrix to be factored. 
//*>          On exit, the factors L and U from the factorization 
//*>          A = P*L*U; the unit diagonal elements of L are not stored. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,M). 
//*> \endverbatim 
//*> 
//*> \param[out] IPIV 
//*> \verbatim 
//*>          IPIV is INTEGER array, dimension (min(M,N)) 
//*>          The pivot indices; for 1 <= i <= min(M,N), row i of the 
//*>          matrix was interchanged with row IPIV(i). 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value 
//*>          > 0:  if INFO = i, U(i,i) is exactly zero. The factorization 
//*>                has been completed, but the factor U is exactly 
//*>                singular, and division by zero will occur if it is used 
//*>                to solve a system of equations. 
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
//*> \ingroup complexGEcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _ianoe0kp(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, Int32* _w1ilvusp, ref Int32 _gro5yvfo)
	{
#region variable declarations
fcomplex _kxg5drh2 =   new fcomplex(1f,0f);
Int32 _b5p6od9s =  default;
Int32 _itfnbz60 =  default;
Int32 _znpjgsef =  default;
Int32 _pscq8l5q =  default;
Int32 _f7059815 =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK computational routine (version 3.7.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
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
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input parameters. 
		//* 
		
		_gro5yvfo = (int)0;
		if (_ev4xhht5 < (int)0)
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ))
		{
			
			_gro5yvfo = (int)-4;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("CGETRF" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if ((_ev4xhht5 == (int)0) | (_dxpq0xkr == (int)0))
		return;//* 
		//*     Determine the block size for this environment. 
		//* 
		
		_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"CGETRF" ," " ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) );
		if ((_f7059815 <= (int)1) | (_f7059815 >= ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_dxpq0xkr )))
		{
			//* 
			//*        Use unblocked code. 
			//* 
			
			_61ogh5t9(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_w1ilvusp ,ref _gro5yvfo );
		}
		else
		{
			//* 
			//*        Use blocked code. 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn1778 = (System.Int32)((int)1);
				System.Int32 __81fgg2step1778 = (System.Int32)(_f7059815);
				System.Int32 __81fgg2count1778;
				for (__81fgg2count1778 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_dxpq0xkr )) - __81fgg2dlsvn1778 + __81fgg2step1778) / __81fgg2step1778)), _znpjgsef = __81fgg2dlsvn1778; __81fgg2count1778 != 0; __81fgg2count1778--, _znpjgsef += (__81fgg2step1778)) {

				{
					
					_pscq8l5q = ILNumerics.F2NET.Intrinsics.MIN((ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_dxpq0xkr ) - _znpjgsef) + (int)1 ,_f7059815 );//* 
					//*           Factor diagonal and subdiagonal blocks and test for exact 
					//*           singularity. 
					//* 
					
					_61ogh5t9(ref Unsafe.AsRef((_ev4xhht5 - _znpjgsef) + (int)1) ,ref _pscq8l5q ,(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_w1ilvusp+(_znpjgsef - 1)),ref _itfnbz60 );//* 
					//*           Adjust INFO and the pivot indices. 
					//* 
					
					if ((_gro5yvfo == (int)0) & (_itfnbz60 > (int)0))
					_gro5yvfo = ((_itfnbz60 + _znpjgsef) - (int)1);
					{
						System.Int32 __81fgg2dlsvn1779 = (System.Int32)(_znpjgsef);
						const System.Int32 __81fgg2step1779 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1779;
						for (__81fgg2count1779 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,(_znpjgsef + _pscq8l5q) - (int)1 )) - __81fgg2dlsvn1779 + __81fgg2step1779) / __81fgg2step1779)), _b5p6od9s = __81fgg2dlsvn1779; __81fgg2count1779 != 0; __81fgg2count1779--, _b5p6od9s += (__81fgg2step1779)) {

						{
							
							*(_w1ilvusp+(_b5p6od9s - 1)) = ((_znpjgsef - (int)1) + *(_w1ilvusp+(_b5p6od9s - 1)));
Mark10:;
							// continue
						}
												}					}//* 
					//*           Apply interchanges to columns 1:J-1. 
					//* 
					
					_iuxulk3d(ref Unsafe.AsRef(_znpjgsef - (int)1) ,_vxfgpup9 ,ref _ocv8fk5c ,ref _znpjgsef ,ref Unsafe.AsRef((_znpjgsef + _pscq8l5q) - (int)1) ,_w1ilvusp ,ref Unsafe.AsRef((int)1) );//* 
					
					if ((_znpjgsef + _pscq8l5q) <= _dxpq0xkr)
					{
						//* 
						//*              Apply interchanges to columns J+JB:N. 
						//* 
						
						_iuxulk3d(ref Unsafe.AsRef(((_dxpq0xkr - _znpjgsef) - _pscq8l5q) + (int)1) ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef + _pscq8l5q - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref _znpjgsef ,ref Unsafe.AsRef((_znpjgsef + _pscq8l5q) - (int)1) ,_w1ilvusp ,ref Unsafe.AsRef((int)1) );//* 
						//*              Compute block row of U. 
						//* 
						
						_goj1gzmg("Left" ,"Lower" ,"No transpose" ,"Unit" ,ref _pscq8l5q ,ref Unsafe.AsRef(((_dxpq0xkr - _znpjgsef) - _pscq8l5q) + (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef + _pscq8l5q - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						if ((_znpjgsef + _pscq8l5q) <= _ev4xhht5)
						{
							//* 
							//*                 Update trailing submatrix. 
							//* 
							
							_5p0w9905("No transpose" ,"No transpose" ,ref Unsafe.AsRef(((_ev4xhht5 - _znpjgsef) - _pscq8l5q) + (int)1) ,ref Unsafe.AsRef(((_dxpq0xkr - _znpjgsef) - _pscq8l5q) + (int)1) ,ref _pscq8l5q ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+(_znpjgsef + _pscq8l5q - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef + _pscq8l5q - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_znpjgsef + _pscq8l5q - 1) + (_znpjgsef + _pscq8l5q - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						}
						
					}
					
Mark20:;
					// continue
				}
								}			}
		}
		
		return;//* 
		//*     End of CGETRF 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
