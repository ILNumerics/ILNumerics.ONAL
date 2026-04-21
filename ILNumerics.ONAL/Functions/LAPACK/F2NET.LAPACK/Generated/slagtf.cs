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
//*> \brief \b SLAGTF computes an LU factorization of a matrix T-Î»I, where T is a general tridiagonal matrix, and Î» a scalar, using partial pivoting with row interchanges. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLAGTF + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slagtf.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slagtf.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slagtf.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLAGTF( N, A, LAMBDA, B, C, TOL, D, IN, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, N 
//*       REAL               LAMBDA, TOL 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IN( * ) 
//*       REAL               A( * ), B( * ), C( * ), D( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLAGTF factorizes the matrix (T - lambda*I), where T is an n by n 
//*> tridiagonal matrix and lambda is a scalar, as 
//*> 
//*>    T - lambda*I = PLU, 
//*> 
//*> where P is a permutation matrix, L is a unit lower tridiagonal matrix 
//*> with at most one non-zero sub-diagonal elements per column and U is 
//*> an upper triangular matrix with at most two non-zero super-diagonal 
//*> elements per column. 
//*> 
//*> The factorization is obtained by Gaussian elimination with partial 
//*> pivoting and implicit row scaling. 
//*> 
//*> The parameter LAMBDA is included in the routine so that SLAGTF may 
//*> be used, in conjunction with SLAGTS, to obtain eigenvectors of T by 
//*> inverse iteration. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix T. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is REAL array, dimension (N) 
//*>          On entry, A must contain the diagonal elements of T. 
//*> 
//*>          On exit, A is overwritten by the n diagonal elements of the 
//*>          upper triangular matrix U of the factorization of T. 
//*> \endverbatim 
//*> 
//*> \param[in] LAMBDA 
//*> \verbatim 
//*>          LAMBDA is REAL 
//*>          On entry, the scalar lambda. 
//*> \endverbatim 
//*> 
//*> \param[in,out] B 
//*> \verbatim 
//*>          B is REAL array, dimension (N-1) 
//*>          On entry, B must contain the (n-1) super-diagonal elements of 
//*>          T. 
//*> 
//*>          On exit, B is overwritten by the (n-1) super-diagonal 
//*>          elements of the matrix U of the factorization of T. 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is REAL array, dimension (N-1) 
//*>          On entry, C must contain the (n-1) sub-diagonal elements of 
//*>          T. 
//*> 
//*>          On exit, C is overwritten by the (n-1) sub-diagonal elements 
//*>          of the matrix L of the factorization of T. 
//*> \endverbatim 
//*> 
//*> \param[in] TOL 
//*> \verbatim 
//*>          TOL is REAL 
//*>          On entry, a relative tolerance used to indicate whether or 
//*>          not the matrix (T - lambda*I) is nearly singular. TOL should 
//*>          normally be chose as approximately the largest relative error 
//*>          in the elements of T. For example, if the elements of T are 
//*>          correct to about 4 significant figures, then TOL should be 
//*>          set to about 5*10**(-4). If TOL is supplied as less than eps, 
//*>          where eps is the relative machine precision, then the value 
//*>          eps is used in place of TOL. 
//*> \endverbatim 
//*> 
//*> \param[out] D 
//*> \verbatim 
//*>          D is REAL array, dimension (N-2) 
//*>          On exit, D is overwritten by the (n-2) second super-diagonal 
//*>          elements of the matrix U of the factorization of T. 
//*> \endverbatim 
//*> 
//*> \param[out] IN 
//*> \verbatim 
//*>          IN is INTEGER array, dimension (N) 
//*>          On exit, IN contains details of the permutation matrix P. If 
//*>          an interchange occurred at the kth step of the elimination, 
//*>          then IN(k) = 1, otherwise IN(k) = 0. The element IN(n) 
//*>          returns the smallest positive integer j such that 
//*> 
//*>             abs( u(j,j) ) <= norm( (T - lambda*I)(j) )*TOL, 
//*> 
//*>          where norm( A(j) ) denotes the sum of the absolute values of 
//*>          the jth row of the matrix A. If no such j exists then IN(n) 
//*>          is returned as zero. If IN(n) is returned as positive, then a 
//*>          diagonal element of U is small, indicating that 
//*>          (T - lambda*I) is singular or nearly singular, 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0: successful exit 
//*>          < 0: if INFO = -k, the kth argument had an illegal value 
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
//*> \ingroup auxOTHERcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _8afyb8tq(ref Int32 _dxpq0xkr, Single* _vxfgpup9, ref Single _w637r8jo, Single* _p9n405a5, Single* _3crf0qn3, ref Single _txq1gp7u, Single* _plfm7z8g, Int32* _oxr7eu3o, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Int32 _umlkckdg =  default;
Single _p1iqarg6 =  default;
Single _i7a61lqu =  default;
Single _fk8thwq7 =  default;
Single _8b1e1cy0 =  default;
Single _vrm8t9sf =  default;
Single _xgrjl2kp =  default;
Single _1ajfmh55 =  default;
Single _qcwsqci1 =  default;
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
		//* ===================================================================== 
		//* 
		//*     .. Parameters .. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		_gro5yvfo = (int)0;
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-1;
			_ut9qalzx("SLAGTF" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		
		if (_dxpq0xkr == (int)0)
		return;//* 
		
		*(_vxfgpup9+((int)1 - 1)) = (*(_vxfgpup9+((int)1 - 1)) - _w637r8jo);
		*(_oxr7eu3o+(_dxpq0xkr - 1)) = (int)0;
		if (_dxpq0xkr == (int)1)
		{
			
			if (*(_vxfgpup9+((int)1 - 1)) == _d0547bi2)
			*(_oxr7eu3o+((int)1 - 1)) = (int)1;
			return;
		}
		//* 
		
		_p1iqarg6 = _d5tu038y("Epsilon" );//* 
		
		_qcwsqci1 = ILNumerics.F2NET.Intrinsics.MAX(_txq1gp7u ,_p1iqarg6 );
		_vrm8t9sf = (ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+((int)1 - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_p9n405a5+((int)1 - 1)) ));
		{
			System.Int32 __81fgg2dlsvn3425 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step3425 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3425;
			for (__81fgg2count3425 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn3425 + __81fgg2step3425) / __81fgg2step3425)), _umlkckdg = __81fgg2dlsvn3425; __81fgg2count3425 != 0; __81fgg2count3425--, _umlkckdg += (__81fgg2step3425)) {

			{
				
				*(_vxfgpup9+(_umlkckdg + (int)1 - 1)) = (*(_vxfgpup9+(_umlkckdg + (int)1 - 1)) - _w637r8jo);
				_xgrjl2kp = (ILNumerics.F2NET.Intrinsics.ABS(*(_3crf0qn3+(_umlkckdg - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_umlkckdg + (int)1 - 1)) ));
				if (_umlkckdg < (_dxpq0xkr - (int)1))
				_xgrjl2kp = (_xgrjl2kp + ILNumerics.F2NET.Intrinsics.ABS(*(_p9n405a5+(_umlkckdg + (int)1 - 1)) ));
				if (*(_vxfgpup9+(_umlkckdg - 1)) == _d0547bi2)
				{
					
					_fk8thwq7 = _d0547bi2;
				}
				else
				{
					
					_fk8thwq7 = (ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_umlkckdg - 1)) ) / _vrm8t9sf);
				}
				
				if (*(_3crf0qn3+(_umlkckdg - 1)) == _d0547bi2)
				{
					
					*(_oxr7eu3o+(_umlkckdg - 1)) = (int)0;
					_8b1e1cy0 = _d0547bi2;
					_vrm8t9sf = _xgrjl2kp;
					if (_umlkckdg < (_dxpq0xkr - (int)1))
					*(_plfm7z8g+(_umlkckdg - 1)) = _d0547bi2;
				}
				else
				{
					
					_8b1e1cy0 = (ILNumerics.F2NET.Intrinsics.ABS(*(_3crf0qn3+(_umlkckdg - 1)) ) / _xgrjl2kp);
					if (_8b1e1cy0 <= _fk8thwq7)
					{
						
						*(_oxr7eu3o+(_umlkckdg - 1)) = (int)0;
						_vrm8t9sf = _xgrjl2kp;
						*(_3crf0qn3+(_umlkckdg - 1)) = (*(_3crf0qn3+(_umlkckdg - 1)) / *(_vxfgpup9+(_umlkckdg - 1)));
						*(_vxfgpup9+(_umlkckdg + (int)1 - 1)) = (*(_vxfgpup9+(_umlkckdg + (int)1 - 1)) - (*(_3crf0qn3+(_umlkckdg - 1)) * *(_p9n405a5+(_umlkckdg - 1))));
						if (_umlkckdg < (_dxpq0xkr - (int)1))
						*(_plfm7z8g+(_umlkckdg - 1)) = _d0547bi2;
					}
					else
					{
						
						*(_oxr7eu3o+(_umlkckdg - 1)) = (int)1;
						_i7a61lqu = (*(_vxfgpup9+(_umlkckdg - 1)) / *(_3crf0qn3+(_umlkckdg - 1)));
						*(_vxfgpup9+(_umlkckdg - 1)) = *(_3crf0qn3+(_umlkckdg - 1));
						_1ajfmh55 = *(_vxfgpup9+(_umlkckdg + (int)1 - 1));
						*(_vxfgpup9+(_umlkckdg + (int)1 - 1)) = (*(_p9n405a5+(_umlkckdg - 1)) - (_i7a61lqu * _1ajfmh55));
						if (_umlkckdg < (_dxpq0xkr - (int)1))
						{
							
							*(_plfm7z8g+(_umlkckdg - 1)) = *(_p9n405a5+(_umlkckdg + (int)1 - 1));
							*(_p9n405a5+(_umlkckdg + (int)1 - 1)) = (-((_i7a61lqu * *(_plfm7z8g+(_umlkckdg - 1)))));
						}
						
						*(_p9n405a5+(_umlkckdg - 1)) = _1ajfmh55;
						*(_3crf0qn3+(_umlkckdg - 1)) = _i7a61lqu;
					}
					
				}
				
				if ((ILNumerics.F2NET.Intrinsics.MAX(_fk8thwq7 ,_8b1e1cy0 ) <= _qcwsqci1) & (*(_oxr7eu3o+(_dxpq0xkr - 1)) == (int)0))
				*(_oxr7eu3o+(_dxpq0xkr - 1)) = _umlkckdg;
Mark10:;
				// continue
			}
						}		}
		if ((ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_dxpq0xkr - 1)) ) <= (_vrm8t9sf * _qcwsqci1)) & (*(_oxr7eu3o+(_dxpq0xkr - 1)) == (int)0))
		*(_oxr7eu3o+(_dxpq0xkr - 1)) = _dxpq0xkr;//* 
		
		return;//* 
		//*     End of SLAGTF 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
