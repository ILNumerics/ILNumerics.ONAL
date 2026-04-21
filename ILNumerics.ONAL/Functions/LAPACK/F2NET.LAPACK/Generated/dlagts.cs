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
//*> \brief \b DLAGTS solves the system of equations (T-Î»I)x = y or (T-Î»I)Tx = y,where T is a general tridiagonal matrix and Î» a scalar, using the LU factorization computed by slagtf. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLAGTS + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlagts.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlagts.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlagts.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLAGTS( JOB, N, A, B, C, D, IN, Y, TOL, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, JOB, N 
//*       DOUBLE PRECISION   TOL 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IN( * ) 
//*       DOUBLE PRECISION   A( * ), B( * ), C( * ), D( * ), Y( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLAGTS may be used to solve one of the systems of equations 
//*> 
//*>    (T - lambda*I)*x = y   or   (T - lambda*I)**T*x = y, 
//*> 
//*> where T is an n by n tridiagonal matrix, for x, following the 
//*> factorization of (T - lambda*I) as 
//*> 
//*>    (T - lambda*I) = P*L*U , 
//*> 
//*> by routine DLAGTF. The choice of equation to be solved is 
//*> controlled by the argument JOB, and in each case there is an option 
//*> to perturb zero or very small diagonal elements of U, this option 
//*> being intended for use in applications such as inverse iteration. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] JOB 
//*> \verbatim 
//*>          JOB is INTEGER 
//*>          Specifies the job to be performed by DLAGTS as follows: 
//*>          =  1: The equations  (T - lambda*I)x = y  are to be solved, 
//*>                but diagonal elements of U are not to be perturbed. 
//*>          = -1: The equations  (T - lambda*I)x = y  are to be solved 
//*>                and, if overflow would otherwise occur, the diagonal 
//*>                elements of U are to be perturbed. See argument TOL 
//*>                below. 
//*>          =  2: The equations  (T - lambda*I)**Tx = y  are to be solved, 
//*>                but diagonal elements of U are not to be perturbed. 
//*>          = -2: The equations  (T - lambda*I)**Tx = y  are to be solved 
//*>                and, if overflow would otherwise occur, the diagonal 
//*>                elements of U are to be perturbed. See argument TOL 
//*>                below. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix T. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is DOUBLE PRECISION array, dimension (N) 
//*>          On entry, A must contain the diagonal elements of U as 
//*>          returned from DLAGTF. 
//*> \endverbatim 
//*> 
//*> \param[in] B 
//*> \verbatim 
//*>          B is DOUBLE PRECISION array, dimension (N-1) 
//*>          On entry, B must contain the first super-diagonal elements of 
//*>          U as returned from DLAGTF. 
//*> \endverbatim 
//*> 
//*> \param[in] C 
//*> \verbatim 
//*>          C is DOUBLE PRECISION array, dimension (N-1) 
//*>          On entry, C must contain the sub-diagonal elements of L as 
//*>          returned from DLAGTF. 
//*> \endverbatim 
//*> 
//*> \param[in] D 
//*> \verbatim 
//*>          D is DOUBLE PRECISION array, dimension (N-2) 
//*>          On entry, D must contain the second super-diagonal elements 
//*>          of U as returned from DLAGTF. 
//*> \endverbatim 
//*> 
//*> \param[in] IN 
//*> \verbatim 
//*>          IN is INTEGER array, dimension (N) 
//*>          On entry, IN must contain details of the matrix P as returned 
//*>          from DLAGTF. 
//*> \endverbatim 
//*> 
//*> \param[in,out] Y 
//*> \verbatim 
//*>          Y is DOUBLE PRECISION array, dimension (N) 
//*>          On entry, the right hand side vector y. 
//*>          On exit, Y is overwritten by the solution vector x. 
//*> \endverbatim 
//*> 
//*> \param[in,out] TOL 
//*> \verbatim 
//*>          TOL is DOUBLE PRECISION 
//*>          On entry, with  JOB < 0, TOL should be the minimum 
//*>          perturbation to be made to very small diagonal elements of U. 
//*>          TOL should normally be chosen as about eps*norm(U), where eps 
//*>          is the relative machine precision, but if TOL is supplied as 
//*>          non-positive, then it is reset to eps*max( abs( u(i,j) ) ). 
//*>          If  JOB > 0  then TOL is not referenced. 
//*> 
//*>          On exit, TOL is changed as described above, only if TOL is 
//*>          non-positive on entry. Otherwise TOL is unchanged. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value 
//*>          > 0:  overflow would occur when computing the INFO(th) 
//*>                element of the solution vector x. This can only occur 
//*>                when JOB is supplied as positive and either means 
//*>                that a diagonal element of U is very small, or that 
//*>                the elements of the right-hand side vector y are very 
//*>                large. 
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
//*> \ingroup OTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _h67p9ac4(ref Int32 _xcrv93xi, ref Int32 _dxpq0xkr, Double* _vxfgpup9, Double* _p9n405a5, Double* _3crf0qn3, Double* _plfm7z8g, Int32* _oxr7eu3o, Double* _f3z3edv0, ref Double _txq1gp7u, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _kxg5drh2 =  1d;
Double _d0547bi2 =  0d;
Int32 _umlkckdg =  default;
Double _z1ce5y8v =  default;
Double _qaq8j8dg =  default;
Double _av7j8yda =  default;
Double _p1iqarg6 =  default;
Double _5yjbn35p =  default;
Double _ptpa0vax =  default;
Double _1ajfmh55 =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.0) -- 
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
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		_gro5yvfo = (int)0;
		if ((ILNumerics.F2NET.Intrinsics.ABS(_xcrv93xi ) > (int)2) | (_xcrv93xi == (int)0))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-2;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("DLAGTS" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		
		if (_dxpq0xkr == (int)0)
		return;//* 
		
		_p1iqarg6 = _f43eg0w0("Epsilon" );
		_ptpa0vax = _f43eg0w0("Safe minimum" );
		_av7j8yda = (_kxg5drh2 / _ptpa0vax);//* 
		
		if (_xcrv93xi < (int)0)
		{
			
			if (_txq1gp7u <= _d0547bi2)
			{
				
				_txq1gp7u = ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+((int)1 - 1)) );
				if (_dxpq0xkr > (int)1)
				_txq1gp7u = ILNumerics.F2NET.Intrinsics.MAX(_txq1gp7u ,ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+((int)2 - 1)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_p9n405a5+((int)1 - 1)) ) );
				{
					System.Int32 __81fgg2dlsvn3085 = (System.Int32)((int)3);
					const System.Int32 __81fgg2step3085 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3085;
					for (__81fgg2count3085 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3085 + __81fgg2step3085) / __81fgg2step3085)), _umlkckdg = __81fgg2dlsvn3085; __81fgg2count3085 != 0; __81fgg2count3085--, _umlkckdg += (__81fgg2step3085)) {

					{
						
						_txq1gp7u = ILNumerics.F2NET.Intrinsics.MAX(_txq1gp7u ,ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_umlkckdg - 1)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_p9n405a5+(_umlkckdg - (int)1 - 1)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_umlkckdg - (int)2 - 1)) ) );
Mark10:;
						// continue
					}
										}				}
				_txq1gp7u = (_txq1gp7u * _p1iqarg6);
				if (_txq1gp7u == _d0547bi2)
				_txq1gp7u = _p1iqarg6;
			}
			
		}
		//* 
		
		if (ILNumerics.F2NET.Intrinsics.ABS(_xcrv93xi ) == (int)1)
		{
			
			{
				System.Int32 __81fgg2dlsvn3086 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step3086 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3086;
				for (__81fgg2count3086 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3086 + __81fgg2step3086) / __81fgg2step3086)), _umlkckdg = __81fgg2dlsvn3086; __81fgg2count3086 != 0; __81fgg2count3086--, _umlkckdg += (__81fgg2step3086)) {

				{
					
					if (*(_oxr7eu3o+(_umlkckdg - (int)1 - 1)) == (int)0)
					{
						
						*(_f3z3edv0+(_umlkckdg - 1)) = (*(_f3z3edv0+(_umlkckdg - 1)) - (*(_3crf0qn3+(_umlkckdg - (int)1 - 1)) * *(_f3z3edv0+(_umlkckdg - (int)1 - 1))));
					}
					else
					{
						
						_1ajfmh55 = *(_f3z3edv0+(_umlkckdg - (int)1 - 1));
						*(_f3z3edv0+(_umlkckdg - (int)1 - 1)) = *(_f3z3edv0+(_umlkckdg - 1));
						*(_f3z3edv0+(_umlkckdg - 1)) = (_1ajfmh55 - (*(_3crf0qn3+(_umlkckdg - (int)1 - 1)) * *(_f3z3edv0+(_umlkckdg - 1))));
					}
					
Mark20:;
					// continue
				}
								}			}
			if (_xcrv93xi == (int)1)
			{
				
				{
					System.Int32 __81fgg2dlsvn3087 = (System.Int32)(_dxpq0xkr);
					System.Int32 __81fgg2step3087 = (System.Int32)((int)-1);
					System.Int32 __81fgg2count3087;
					for (__81fgg2count3087 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn3087 + __81fgg2step3087) / __81fgg2step3087)), _umlkckdg = __81fgg2dlsvn3087; __81fgg2count3087 != 0; __81fgg2count3087--, _umlkckdg += (__81fgg2step3087)) {

					{
						
						if (_umlkckdg <= (_dxpq0xkr - (int)2))
						{
							
							_1ajfmh55 = ((*(_f3z3edv0+(_umlkckdg - 1)) - (*(_p9n405a5+(_umlkckdg - 1)) * *(_f3z3edv0+(_umlkckdg + (int)1 - 1)))) - (*(_plfm7z8g+(_umlkckdg - 1)) * *(_f3z3edv0+(_umlkckdg + (int)2 - 1))));
						}
						else
						if (_umlkckdg == (_dxpq0xkr - (int)1))
						{
							
							_1ajfmh55 = (*(_f3z3edv0+(_umlkckdg - 1)) - (*(_p9n405a5+(_umlkckdg - 1)) * *(_f3z3edv0+(_umlkckdg + (int)1 - 1))));
						}
						else
						{
							
							_1ajfmh55 = *(_f3z3edv0+(_umlkckdg - 1));
						}
						
						_qaq8j8dg = *(_vxfgpup9+(_umlkckdg - 1));
						_z1ce5y8v = ILNumerics.F2NET.Intrinsics.ABS(_qaq8j8dg );
						if (_z1ce5y8v < _kxg5drh2)
						{
							
							if (_z1ce5y8v < _ptpa0vax)
							{
								
								if ((_z1ce5y8v == _d0547bi2) | ((ILNumerics.F2NET.Intrinsics.ABS(_1ajfmh55 ) * _ptpa0vax) > _z1ce5y8v))
								{
									
									_gro5yvfo = _umlkckdg;
									return;
								}
								else
								{
									
									_1ajfmh55 = (_1ajfmh55 * _av7j8yda);
									_qaq8j8dg = (_qaq8j8dg * _av7j8yda);
								}
								
							}
							else
							if (ILNumerics.F2NET.Intrinsics.ABS(_1ajfmh55 ) > (_z1ce5y8v * _av7j8yda))
							{
								
								_gro5yvfo = _umlkckdg;
								return;
							}
							
						}
						
						*(_f3z3edv0+(_umlkckdg - 1)) = (_1ajfmh55 / _qaq8j8dg);
Mark30:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn3088 = (System.Int32)(_dxpq0xkr);
					System.Int32 __81fgg2step3088 = (System.Int32)((int)-1);
					System.Int32 __81fgg2count3088;
					for (__81fgg2count3088 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn3088 + __81fgg2step3088) / __81fgg2step3088)), _umlkckdg = __81fgg2dlsvn3088; __81fgg2count3088 != 0; __81fgg2count3088--, _umlkckdg += (__81fgg2step3088)) {

					{
						
						if (_umlkckdg <= (_dxpq0xkr - (int)2))
						{
							
							_1ajfmh55 = ((*(_f3z3edv0+(_umlkckdg - 1)) - (*(_p9n405a5+(_umlkckdg - 1)) * *(_f3z3edv0+(_umlkckdg + (int)1 - 1)))) - (*(_plfm7z8g+(_umlkckdg - 1)) * *(_f3z3edv0+(_umlkckdg + (int)2 - 1))));
						}
						else
						if (_umlkckdg == (_dxpq0xkr - (int)1))
						{
							
							_1ajfmh55 = (*(_f3z3edv0+(_umlkckdg - 1)) - (*(_p9n405a5+(_umlkckdg - 1)) * *(_f3z3edv0+(_umlkckdg + (int)1 - 1))));
						}
						else
						{
							
							_1ajfmh55 = *(_f3z3edv0+(_umlkckdg - 1));
						}
						
						_qaq8j8dg = *(_vxfgpup9+(_umlkckdg - 1));
						_5yjbn35p = ILNumerics.F2NET.Intrinsics.SIGN(_txq1gp7u ,_qaq8j8dg );
Mark40:;
						// continue
						_z1ce5y8v = ILNumerics.F2NET.Intrinsics.ABS(_qaq8j8dg );
						if (_z1ce5y8v < _kxg5drh2)
						{
							
							if (_z1ce5y8v < _ptpa0vax)
							{
								
								if ((_z1ce5y8v == _d0547bi2) | ((ILNumerics.F2NET.Intrinsics.ABS(_1ajfmh55 ) * _ptpa0vax) > _z1ce5y8v))
								{
									
									_qaq8j8dg = (_qaq8j8dg + _5yjbn35p);
									_5yjbn35p = ((int)2 * _5yjbn35p);goto Mark40;
								}
								else
								{
									
									_1ajfmh55 = (_1ajfmh55 * _av7j8yda);
									_qaq8j8dg = (_qaq8j8dg * _av7j8yda);
								}
								
							}
							else
							if (ILNumerics.F2NET.Intrinsics.ABS(_1ajfmh55 ) > (_z1ce5y8v * _av7j8yda))
							{
								
								_qaq8j8dg = (_qaq8j8dg + _5yjbn35p);
								_5yjbn35p = ((int)2 * _5yjbn35p);goto Mark40;
							}
							
						}
						
						*(_f3z3edv0+(_umlkckdg - 1)) = (_1ajfmh55 / _qaq8j8dg);
Mark50:;
						// continue
					}
										}				}
			}
			
		}
		else
		{
			//* 
			//*        Come to here if  JOB = 2 or -2 
			//* 
			
			if (_xcrv93xi == (int)2)
			{
				
				{
					System.Int32 __81fgg2dlsvn3089 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3089 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3089;
					for (__81fgg2count3089 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3089 + __81fgg2step3089) / __81fgg2step3089)), _umlkckdg = __81fgg2dlsvn3089; __81fgg2count3089 != 0; __81fgg2count3089--, _umlkckdg += (__81fgg2step3089)) {

					{
						
						if (_umlkckdg >= (int)3)
						{
							
							_1ajfmh55 = ((*(_f3z3edv0+(_umlkckdg - 1)) - (*(_p9n405a5+(_umlkckdg - (int)1 - 1)) * *(_f3z3edv0+(_umlkckdg - (int)1 - 1)))) - (*(_plfm7z8g+(_umlkckdg - (int)2 - 1)) * *(_f3z3edv0+(_umlkckdg - (int)2 - 1))));
						}
						else
						if (_umlkckdg == (int)2)
						{
							
							_1ajfmh55 = (*(_f3z3edv0+(_umlkckdg - 1)) - (*(_p9n405a5+(_umlkckdg - (int)1 - 1)) * *(_f3z3edv0+(_umlkckdg - (int)1 - 1))));
						}
						else
						{
							
							_1ajfmh55 = *(_f3z3edv0+(_umlkckdg - 1));
						}
						
						_qaq8j8dg = *(_vxfgpup9+(_umlkckdg - 1));
						_z1ce5y8v = ILNumerics.F2NET.Intrinsics.ABS(_qaq8j8dg );
						if (_z1ce5y8v < _kxg5drh2)
						{
							
							if (_z1ce5y8v < _ptpa0vax)
							{
								
								if ((_z1ce5y8v == _d0547bi2) | ((ILNumerics.F2NET.Intrinsics.ABS(_1ajfmh55 ) * _ptpa0vax) > _z1ce5y8v))
								{
									
									_gro5yvfo = _umlkckdg;
									return;
								}
								else
								{
									
									_1ajfmh55 = (_1ajfmh55 * _av7j8yda);
									_qaq8j8dg = (_qaq8j8dg * _av7j8yda);
								}
								
							}
							else
							if (ILNumerics.F2NET.Intrinsics.ABS(_1ajfmh55 ) > (_z1ce5y8v * _av7j8yda))
							{
								
								_gro5yvfo = _umlkckdg;
								return;
							}
							
						}
						
						*(_f3z3edv0+(_umlkckdg - 1)) = (_1ajfmh55 / _qaq8j8dg);
Mark60:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn3090 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3090 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3090;
					for (__81fgg2count3090 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3090 + __81fgg2step3090) / __81fgg2step3090)), _umlkckdg = __81fgg2dlsvn3090; __81fgg2count3090 != 0; __81fgg2count3090--, _umlkckdg += (__81fgg2step3090)) {

					{
						
						if (_umlkckdg >= (int)3)
						{
							
							_1ajfmh55 = ((*(_f3z3edv0+(_umlkckdg - 1)) - (*(_p9n405a5+(_umlkckdg - (int)1 - 1)) * *(_f3z3edv0+(_umlkckdg - (int)1 - 1)))) - (*(_plfm7z8g+(_umlkckdg - (int)2 - 1)) * *(_f3z3edv0+(_umlkckdg - (int)2 - 1))));
						}
						else
						if (_umlkckdg == (int)2)
						{
							
							_1ajfmh55 = (*(_f3z3edv0+(_umlkckdg - 1)) - (*(_p9n405a5+(_umlkckdg - (int)1 - 1)) * *(_f3z3edv0+(_umlkckdg - (int)1 - 1))));
						}
						else
						{
							
							_1ajfmh55 = *(_f3z3edv0+(_umlkckdg - 1));
						}
						
						_qaq8j8dg = *(_vxfgpup9+(_umlkckdg - 1));
						_5yjbn35p = ILNumerics.F2NET.Intrinsics.SIGN(_txq1gp7u ,_qaq8j8dg );
Mark70:;
						// continue
						_z1ce5y8v = ILNumerics.F2NET.Intrinsics.ABS(_qaq8j8dg );
						if (_z1ce5y8v < _kxg5drh2)
						{
							
							if (_z1ce5y8v < _ptpa0vax)
							{
								
								if ((_z1ce5y8v == _d0547bi2) | ((ILNumerics.F2NET.Intrinsics.ABS(_1ajfmh55 ) * _ptpa0vax) > _z1ce5y8v))
								{
									
									_qaq8j8dg = (_qaq8j8dg + _5yjbn35p);
									_5yjbn35p = ((int)2 * _5yjbn35p);goto Mark70;
								}
								else
								{
									
									_1ajfmh55 = (_1ajfmh55 * _av7j8yda);
									_qaq8j8dg = (_qaq8j8dg * _av7j8yda);
								}
								
							}
							else
							if (ILNumerics.F2NET.Intrinsics.ABS(_1ajfmh55 ) > (_z1ce5y8v * _av7j8yda))
							{
								
								_qaq8j8dg = (_qaq8j8dg + _5yjbn35p);
								_5yjbn35p = ((int)2 * _5yjbn35p);goto Mark70;
							}
							
						}
						
						*(_f3z3edv0+(_umlkckdg - 1)) = (_1ajfmh55 / _qaq8j8dg);
Mark80:;
						// continue
					}
										}				}
			}
			//* 
			
			{
				System.Int32 __81fgg2dlsvn3091 = (System.Int32)(_dxpq0xkr);
				System.Int32 __81fgg2step3091 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count3091;
				for (__81fgg2count3091 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)2) - __81fgg2dlsvn3091 + __81fgg2step3091) / __81fgg2step3091)), _umlkckdg = __81fgg2dlsvn3091; __81fgg2count3091 != 0; __81fgg2count3091--, _umlkckdg += (__81fgg2step3091)) {

				{
					
					if (*(_oxr7eu3o+(_umlkckdg - (int)1 - 1)) == (int)0)
					{
						
						*(_f3z3edv0+(_umlkckdg - (int)1 - 1)) = (*(_f3z3edv0+(_umlkckdg - (int)1 - 1)) - (*(_3crf0qn3+(_umlkckdg - (int)1 - 1)) * *(_f3z3edv0+(_umlkckdg - 1))));
					}
					else
					{
						
						_1ajfmh55 = *(_f3z3edv0+(_umlkckdg - (int)1 - 1));
						*(_f3z3edv0+(_umlkckdg - (int)1 - 1)) = *(_f3z3edv0+(_umlkckdg - 1));
						*(_f3z3edv0+(_umlkckdg - 1)) = (_1ajfmh55 - (*(_3crf0qn3+(_umlkckdg - (int)1 - 1)) * *(_f3z3edv0+(_umlkckdg - 1))));
					}
					
Mark90:;
					// continue
				}
								}			}
		}
		//* 
		//*     End of DLAGTS 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
