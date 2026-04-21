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
//*> \brief \b DLATRZ factors an upper trapezoidal matrix by means of orthogonal transformations. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLATRZ + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlatrz.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlatrz.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlatrz.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLATRZ( M, N, L, A, LDA, TAU, WORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            L, LDA, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   A( LDA, * ), TAU( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLATRZ factors the M-by-(M+L) real upper trapezoidal matrix 
//*> [ A1 A2 ] = [ A(1:M,1:M) A(1:M,N-L+1:N) ] as ( R  0 ) * Z, by means 
//*> of orthogonal transformations.  Z is an (M+L)-by-(M+L) orthogonal 
//*> matrix and, R and A1 are M-by-M upper triangular matrices. 
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
//*> \param[in] L 
//*> \verbatim 
//*>          L is INTEGER 
//*>          The number of columns of the matrix A containing the 
//*>          meaningful part of the Householder vectors. N-M >= L >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is DOUBLE PRECISION array, dimension (LDA,N) 
//*>          On entry, the leading M-by-N upper trapezoidal part of the 
//*>          array A must contain the matrix to be factorized. 
//*>          On exit, the leading M-by-M upper triangular part of A 
//*>          contains the upper triangular matrix R, and elements N-L+1 to 
//*>          N of the first M rows of A, with the array TAU, represent the 
//*>          orthogonal matrix Z as a product of M elementary reflectors. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,M). 
//*> \endverbatim 
//*> 
//*> \param[out] TAU 
//*> \verbatim 
//*>          TAU is DOUBLE PRECISION array, dimension (M) 
//*>          The scalar factors of the elementary reflectors. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is DOUBLE PRECISION array, dimension (M) 
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
//*> \ingroup doubleOTHERcomputational 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>    A. Petitet, Computer Science Dept., Univ. of Tenn., Knoxville, USA 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  The factorization is obtained by Householder's method.  The kth 
//*>  transformation matrix, Z( k ), which is used to introduce zeros into 
//*>  the ( m - k + 1 )th row of A, is given in the form 
//*> 
//*>     Z( k ) = ( I     0   ), 
//*>              ( 0  T( k ) ) 
//*> 
//*>  where 
//*> 
//*>     T( k ) = I - tau*u( k )*u( k )**T,   u( k ) = (   1    ), 
//*>                                                 (   0    ) 
//*>                                                 ( z( k ) ) 
//*> 
//*>  tau is a scalar and z( k ) is an l element vector. tau and z( k ) 
//*>  are chosen to annihilate the elements of the kth row of A2. 
//*> 
//*>  The scalar tau is returned in the kth element of TAU and the vector 
//*>  u( k ) in the kth row of A2, such that the elements of z( k ) are 
//*>  in  a( k, l + 1 ), ..., a( k, n ). The elements of R are returned in 
//*>  the upper triangular part of A1. 
//*> 
//*>  Z is given by 
//*> 
//*>     Z =  Z( 1 ) * Z( 2 ) * ... * Z( m ). 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _wfabsqdk(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _68ec3gbh, Double* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _0446f4de, Double* _apig8meb)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Int32 _b5p6od9s =  default;
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
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input arguments 
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_ev4xhht5 == (int)0)
		{
			
			return;
		}
		else
		if (_ev4xhht5 == _dxpq0xkr)
		{
			
			{
				System.Int32 __81fgg2dlsvn2092 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2092 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2092;
				for (__81fgg2count2092 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2092 + __81fgg2step2092) / __81fgg2step2092)), _b5p6od9s = __81fgg2dlsvn2092; __81fgg2count2092 != 0; __81fgg2count2092--, _b5p6od9s += (__81fgg2step2092)) {

				{
					
					*(_0446f4de+(_b5p6od9s - 1)) = _d0547bi2;
Mark10:;
					// continue
				}
								}			}
			return;
		}
		//* 
		
		{
			System.Int32 __81fgg2dlsvn2093 = (System.Int32)(_ev4xhht5);
			System.Int32 __81fgg2step2093 = (System.Int32)((int)-1);
			System.Int32 __81fgg2count2093;
			for (__81fgg2count2093 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn2093 + __81fgg2step2093) / __81fgg2step2093)), _b5p6od9s = __81fgg2dlsvn2093; __81fgg2count2093 != 0; __81fgg2count2093--, _b5p6od9s += (__81fgg2step2093)) {

			{
				//* 
				//*        Generate elementary reflector H(i) to annihilate 
				//*        [ A(i,i) A(i,n-l+1:n) ] 
				//* 
				
				_a51k3mk0(ref Unsafe.AsRef(_68ec3gbh + (int)1) ,ref Unsafe.AsRef(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))) ,(_vxfgpup9+(_b5p6od9s - 1) + ((_dxpq0xkr - _68ec3gbh) + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(*(_0446f4de+(_b5p6od9s - 1))) );//* 
				//*        Apply H(i) to A(1:i-1,i:n) from the right 
				//* 
				
				_qtvbvdij("Right" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef((_dxpq0xkr - _b5p6od9s) + (int)1) ,ref _68ec3gbh ,(_vxfgpup9+(_b5p6od9s - 1) + ((_dxpq0xkr - _68ec3gbh) + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(*(_0446f4de+(_b5p6od9s - 1))) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_apig8meb );//* 
				
Mark20:;
				// continue
			}
						}		}//* 
		
		return;//* 
		//*     End of DLATRZ 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
