
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
//*> \brief \b DLAGTF computes an LU factorization of a matrix T-λI, where T is a general tridiagonal matrix, and λ a scalar, using partial pivoting with row interchanges. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLAGTF + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlagtf.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlagtf.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlagtf.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLAGTF( N, A, LAMBDA, B, C, TOL, D, IN, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, N 
//*       DOUBLE PRECISION   LAMBDA, TOL 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IN( * ) 
//*       DOUBLE PRECISION   A( * ), B( * ), C( * ), D( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLAGTF factorizes the matrix (T - lambda*I), where T is an n by n 
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
//*> The parameter LAMBDA is included in the routine so that DLAGTF may 
//*> be used, in conjunction with DLAGTS, to obtain eigenvectors of T by 
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
//*>          A is DOUBLE PRECISION array, dimension (N) 
//*>          On entry, A must contain the diagonal elements of T. 
//*> 
//*>          On exit, A is overwritten by the n diagonal elements of the 
//*>          upper triangular matrix U of the factorization of T. 
//*> \endverbatim 
//*> 
//*> \param[in] LAMBDA 
//*> \verbatim 
//*>          LAMBDA is DOUBLE PRECISION 
//*>          On entry, the scalar lambda. 
//*> \endverbatim 
//*> 
//*> \param[in,out] B 
//*> \verbatim 
//*>          B is DOUBLE PRECISION array, dimension (N-1) 
//*>          On entry, B must contain the (n-1) super-diagonal elements of 
//*>          T. 
//*> 
//*>          On exit, B is overwritten by the (n-1) super-diagonal 
//*>          elements of the matrix U of the factorization of T. 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is DOUBLE PRECISION array, dimension (N-1) 
//*>          On entry, C must contain the (n-1) sub-diagonal elements of 
//*>          T. 
//*> 
//*>          On exit, C is overwritten by the (n-1) sub-diagonal elements 
//*>          of the matrix L of the factorization of T. 
//*> \endverbatim 
//*> 
//*> \param[in] TOL 
//*> \verbatim 
//*>          TOL is DOUBLE PRECISION 
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
//*>          D is DOUBLE PRECISION array, dimension (N-2) 
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
//*>          = 0:  successful exit 
//*>          < 0:  if INFO = -k, the kth argument had an illegal value 
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

	 
	public static void _0fmho4hf(ref Int32 _dxpq0xkr, Double* _vxfgpup9, ref Double _w637r8jo, Double* _p9n405a5, Double* _3crf0qn3, ref Double _txq1gp7u, Double* _plfm7z8g, Int32* _oxr7eu3o, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Int32 _umlkckdg =  default;
Double _p1iqarg6 =  default;
Double _i7a61lqu =  default;
Double _fk8thwq7 =  default;
Double _8b1e1cy0 =  default;
Double _vrm8t9sf =  default;
Double _xgrjl2kp =  default;
Double _1ajfmh55 =  default;
Double _qcwsqci1 =  default;
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
			_ut9qalzx("DLAGTF" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
		
		_p1iqarg6 = _f43eg0w0("Epsilon" );//* 
		
		_qcwsqci1 = ILNumerics.F2NET.Intrinsics.MAX(_txq1gp7u ,_p1iqarg6 );
		_vrm8t9sf = (ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+((int)1 - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_p9n405a5+((int)1 - 1)) ));
		{
			System.Int32 __81fgg2dlsvn3084 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step3084 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3084;
			for (__81fgg2count3084 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn3084 + __81fgg2step3084) / __81fgg2step3084)), _umlkckdg = __81fgg2dlsvn3084; __81fgg2count3084 != 0; __81fgg2count3084--, _umlkckdg += (__81fgg2step3084)) {

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
		//*     End of DLAGTF 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
