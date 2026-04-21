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
//*> \brief \b SGETRF2 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       RECURSIVE SUBROUTINE SGETRF2( M, N, A, LDA, IPIV, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, LDA, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IPIV( * ) 
//*       REAL               A( LDA, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SGETRF2 computes an LU factorization of a general M-by-N matrix A 
//*> using partial pivoting with row interchanges. 
//*> 
//*> The factorization has the form 
//*>    A = P * L * U 
//*> where P is a permutation matrix, L is lower triangular with unit 
//*> diagonal elements (lower trapezoidal if m > n), and U is upper 
//*> triangular (upper trapezoidal if m < n). 
//*> 
//*> This is the recursive version of the algorithm. It divides 
//*> the matrix into four submatrices: 
//*> 
//*>        [  A11 | A12  ]  where A11 is n1 by n1 and A22 is n2 by n2 
//*>    A = [ -----|----- ]  with n1 = min(m,n)/2 
//*>        [  A21 | A22  ]       n2 = n-n1 
//*> 
//*>                                       [ A11 ] 
//*> The subroutine calls itself to factor [ --- ], 
//*>                                       [ A12 ] 
//*>                 [ A12 ] 
//*> do the swaps on [ --- ], solve A12, update A22, 
//*>                 [ A22 ] 
//*> 
//*> then calls itself to factor A22 and do the swaps on A21. 
//*> 
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
//*>          A is REAL array, dimension (LDA,N) 
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
//*> \date June 2016 
//* 
//*> \ingroup realGEcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _g65iiicz(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, Single* _vxfgpup9, ref Int32 _ocv8fk5c, Int32* _w1ilvusp, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _kxg5drh2 =  1f;
Single _d0547bi2 =  0f;
Single _ptpa0vax =  default;
Single _1ajfmh55 =  default;
Int32 _b5p6od9s =  default;
Int32 _itfnbz60 =  default;
Int32 _4o1bt8b1 =  default;
Int32 _tixk7d1h =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK computational routine (version 3.7.1) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     June 2016 
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
		//*     .. External Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input parameters 
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
			
			_ut9qalzx("SGETRF2" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if ((_ev4xhht5 == (int)0) | (_dxpq0xkr == (int)0))
		return;// 
		
		if (_ev4xhht5 == (int)1)
		{
			//* 
			//*        Use unblocked code for one row case 
			//*        Just need to handle IPIV and INFO 
			//* 
			
			*(_w1ilvusp+((int)1 - 1)) = (int)1;
			if (*(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) == _d0547bi2)
			_gro5yvfo = (int)1;//* 
			
		}
		else
		if (_dxpq0xkr == (int)1)
		{
			//* 
			//*        Use unblocked code for one column case 
			//* 
			//* 
			//*        Compute machine safe minimum 
			//* 
			
			_ptpa0vax = _d5tu038y("S" );//* 
			//*        Find pivot and test for singularity 
			//* 
			
			_b5p6od9s = _z5b2nqbf(ref _ev4xhht5 ,(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
			*(_w1ilvusp+((int)1 - 1)) = _b5p6od9s;
			if (*(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
			{
				//* 
				//*           Apply the interchange 
				//* 
				
				if (_b5p6od9s != (int)1)
				{
					
					_1ajfmh55 = *(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c));
					*(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) = *(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c));
					*(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) = _1ajfmh55;
				}
				//* 
				//*           Compute elements 2:M of the column 
				//* 
				
				if (ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) ) >= _ptpa0vax)
				{
					
					_ct5qqrv7(ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_kxg5drh2 / *(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c))) ,(_vxfgpup9+((int)2 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn1769 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1769 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1769;
						for (__81fgg2count1769 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5 - (int)1) - __81fgg2dlsvn1769 + __81fgg2step1769) / __81fgg2step1769)), _b5p6od9s = __81fgg2dlsvn1769; __81fgg2count1769 != 0; __81fgg2count1769--, _b5p6od9s += (__81fgg2step1769)) {

						{
							
							*(_vxfgpup9+((int)1 + _b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) = (*(_vxfgpup9+((int)1 + _b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) / *(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)));
Mark10:;
							// continue
						}
												}					}
				}
				//* 
				
			}
			else
			{
				
				_gro5yvfo = (int)1;
			}
			//* 
			
		}
		else
		{
			//* 
			//*        Use recursive code 
			//* 
			
			_4o1bt8b1 = (ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_dxpq0xkr ) / (int)2);
			_tixk7d1h = (_dxpq0xkr - _4o1bt8b1);//* 
			//*               [ A11 ] 
			//*        Factor [ --- ] 
			//*               [ A21 ] 
			//* 
			
			_g65iiicz(ref _ev4xhht5 ,ref _4o1bt8b1 ,_vxfgpup9 ,ref _ocv8fk5c ,_w1ilvusp ,ref _itfnbz60 );// 
			
			if ((_gro5yvfo == (int)0) & (_itfnbz60 > (int)0))
			_gro5yvfo = _itfnbz60;//* 
			//*                              [ A12 ] 
			//*        Apply interchanges to [ --- ] 
			//*                              [ A22 ] 
			//* 
			
			_o2e3xtu7(ref _tixk7d1h ,(_vxfgpup9+((int)1 - 1) + (_4o1bt8b1 + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef((int)1) ,ref _4o1bt8b1 ,_w1ilvusp ,ref Unsafe.AsRef((int)1) );//* 
			//*        Solve A12 
			//* 
			
			_ieiywhin("L" ,"L" ,"N" ,"U" ,ref _4o1bt8b1 ,ref _tixk7d1h ,ref Unsafe.AsRef(_kxg5drh2) ,_vxfgpup9 ,ref _ocv8fk5c ,(_vxfgpup9+((int)1 - 1) + (_4o1bt8b1 + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );//* 
			//*        Update A22 
			//* 
			
			_b8wa9454("N" ,"N" ,ref Unsafe.AsRef(_ev4xhht5 - _4o1bt8b1) ,ref _tixk7d1h ,ref _4o1bt8b1 ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+(_4o1bt8b1 + (int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+((int)1 - 1) + (_4o1bt8b1 + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_4o1bt8b1 + (int)1 - 1) + (_4o1bt8b1 + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );//* 
			//*        Factor A22 
			//* 
			
			_g65iiicz(ref Unsafe.AsRef(_ev4xhht5 - _4o1bt8b1) ,ref _tixk7d1h ,(_vxfgpup9+(_4o1bt8b1 + (int)1 - 1) + (_4o1bt8b1 + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_w1ilvusp+(_4o1bt8b1 + (int)1 - 1)),ref _itfnbz60 );//* 
			//*        Adjust INFO and the pivot indices 
			//* 
			
			if ((_gro5yvfo == (int)0) & (_itfnbz60 > (int)0))
			_gro5yvfo = (_itfnbz60 + _4o1bt8b1);
			{
				System.Int32 __81fgg2dlsvn1770 = (System.Int32)((_4o1bt8b1 + (int)1));
				const System.Int32 __81fgg2step1770 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1770;
				for (__81fgg2count1770 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_dxpq0xkr )) - __81fgg2dlsvn1770 + __81fgg2step1770) / __81fgg2step1770)), _b5p6od9s = __81fgg2dlsvn1770; __81fgg2count1770 != 0; __81fgg2count1770--, _b5p6od9s += (__81fgg2step1770)) {

				{
					
					*(_w1ilvusp+(_b5p6od9s - 1)) = (*(_w1ilvusp+(_b5p6od9s - 1)) + _4o1bt8b1);
Mark20:;
					// continue
				}
								}			}//* 
			//*        Apply interchanges to A21 
			//* 
			
			_o2e3xtu7(ref _4o1bt8b1 ,(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_4o1bt8b1 + (int)1) ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_dxpq0xkr )) ,_w1ilvusp ,ref Unsafe.AsRef((int)1) );//* 
			
		}
		
		return;//* 
		//*     End of SGETRF2 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
