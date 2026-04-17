
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
//*> \brief \b SLAGTS solves the system of equations (T-λI)x = y or (T-λI)Tx = y,where T is a general tridiagonal matrix and λ a scalar, using the LU factorization computed by slagtf. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLAGTS + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slagts.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slagts.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slagts.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLAGTS( JOB, N, A, B, C, D, IN, Y, TOL, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, JOB, N 
//*       REAL               TOL 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IN( * ) 
//*       REAL               A( * ), B( * ), C( * ), D( * ), Y( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLAGTS may be used to solve one of the systems of equations 
//*> 
//*>    (T - lambda*I)*x = y   or   (T - lambda*I)**T*x = y, 
//*> 
//*> where T is an n by n tridiagonal matrix, for x, following the 
//*> factorization of (T - lambda*I) as 
//*> 
//*>    (T - lambda*I) = P*L*U , 
//*> 
//*> by routine SLAGTF. The choice of equation to be solved is 
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
//*>          Specifies the job to be performed by SLAGTS as follows: 
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
//*>          A is REAL array, dimension (N) 
//*>          On entry, A must contain the diagonal elements of U as 
//*>          returned from SLAGTF. 
//*> \endverbatim 
//*> 
//*> \param[in] B 
//*> \verbatim 
//*>          B is REAL array, dimension (N-1) 
//*>          On entry, B must contain the first super-diagonal elements of 
//*>          U as returned from SLAGTF. 
//*> \endverbatim 
//*> 
//*> \param[in] C 
//*> \verbatim 
//*>          C is REAL array, dimension (N-1) 
//*>          On entry, C must contain the sub-diagonal elements of L as 
//*>          returned from SLAGTF. 
//*> \endverbatim 
//*> 
//*> \param[in] D 
//*> \verbatim 
//*>          D is REAL array, dimension (N-2) 
//*>          On entry, D must contain the second super-diagonal elements 
//*>          of U as returned from SLAGTF. 
//*> \endverbatim 
//*> 
//*> \param[in] IN 
//*> \verbatim 
//*>          IN is INTEGER array, dimension (N) 
//*>          On entry, IN must contain details of the matrix P as returned 
//*>          from SLAGTF. 
//*> \endverbatim 
//*> 
//*> \param[in,out] Y 
//*> \verbatim 
//*>          Y is REAL array, dimension (N) 
//*>          On entry, the right hand side vector y. 
//*>          On exit, Y is overwritten by the solution vector x. 
//*> \endverbatim 
//*> 
//*> \param[in,out] TOL 
//*> \verbatim 
//*>          TOL is REAL 
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
//*>          = 0: successful exit 
//*>          < 0: if INFO = -i, the i-th argument had an illegal value 
//*>          > 0: overflow would occur when computing the INFO(th) 
//*>               element of the solution vector x. This can only occur 
//*>               when JOB is supplied as positive and either means 
//*>               that a diagonal element of U is very small, or that 
//*>               the elements of the right-hand side vector y are very 
//*>               large. 
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

	 
	public static void _xke7eh7k(ref Int32 _xcrv93xi, ref Int32 _dxpq0xkr, Single* _vxfgpup9, Single* _p9n405a5, Single* _3crf0qn3, Single* _plfm7z8g, Int32* _oxr7eu3o, Single* _f3z3edv0, ref Single _txq1gp7u, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _kxg5drh2 =  1f;
Single _d0547bi2 =  0f;
Int32 _umlkckdg =  default;
Single _z1ce5y8v =  default;
Single _qaq8j8dg =  default;
Single _av7j8yda =  default;
Single _p1iqarg6 =  default;
Single _5yjbn35p =  default;
Single _ptpa0vax =  default;
Single _1ajfmh55 =  default;
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
			
			_ut9qalzx("SLAGTS" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		
		if (_dxpq0xkr == (int)0)
		return;//* 
		
		_p1iqarg6 = _d5tu038y("Epsilon" );
		_ptpa0vax = _d5tu038y("Safe minimum" );
		_av7j8yda = (_kxg5drh2 / _ptpa0vax);//* 
		
		if (_xcrv93xi < (int)0)
		{
			
			if (_txq1gp7u <= _d0547bi2)
			{
				
				_txq1gp7u = ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+((int)1 - 1)) );
				if (_dxpq0xkr > (int)1)
				_txq1gp7u = ILNumerics.F2NET.Intrinsics.MAX(_txq1gp7u ,ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+((int)2 - 1)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_p9n405a5+((int)1 - 1)) ) );
				{
					System.Int32 __81fgg2dlsvn3426 = (System.Int32)((int)3);
					const System.Int32 __81fgg2step3426 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3426;
					for (__81fgg2count3426 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3426 + __81fgg2step3426) / __81fgg2step3426)), _umlkckdg = __81fgg2dlsvn3426; __81fgg2count3426 != 0; __81fgg2count3426--, _umlkckdg += (__81fgg2step3426)) {

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
				System.Int32 __81fgg2dlsvn3427 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step3427 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3427;
				for (__81fgg2count3427 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3427 + __81fgg2step3427) / __81fgg2step3427)), _umlkckdg = __81fgg2dlsvn3427; __81fgg2count3427 != 0; __81fgg2count3427--, _umlkckdg += (__81fgg2step3427)) {

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
					System.Int32 __81fgg2dlsvn3428 = (System.Int32)(_dxpq0xkr);
					System.Int32 __81fgg2step3428 = (System.Int32)((int)-1);
					System.Int32 __81fgg2count3428;
					for (__81fgg2count3428 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn3428 + __81fgg2step3428) / __81fgg2step3428)), _umlkckdg = __81fgg2dlsvn3428; __81fgg2count3428 != 0; __81fgg2count3428--, _umlkckdg += (__81fgg2step3428)) {

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
					System.Int32 __81fgg2dlsvn3429 = (System.Int32)(_dxpq0xkr);
					System.Int32 __81fgg2step3429 = (System.Int32)((int)-1);
					System.Int32 __81fgg2count3429;
					for (__81fgg2count3429 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn3429 + __81fgg2step3429) / __81fgg2step3429)), _umlkckdg = __81fgg2dlsvn3429; __81fgg2count3429 != 0; __81fgg2count3429--, _umlkckdg += (__81fgg2step3429)) {

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
					System.Int32 __81fgg2dlsvn3430 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3430 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3430;
					for (__81fgg2count3430 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3430 + __81fgg2step3430) / __81fgg2step3430)), _umlkckdg = __81fgg2dlsvn3430; __81fgg2count3430 != 0; __81fgg2count3430--, _umlkckdg += (__81fgg2step3430)) {

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
					System.Int32 __81fgg2dlsvn3431 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3431 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3431;
					for (__81fgg2count3431 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3431 + __81fgg2step3431) / __81fgg2step3431)), _umlkckdg = __81fgg2dlsvn3431; __81fgg2count3431 != 0; __81fgg2count3431--, _umlkckdg += (__81fgg2step3431)) {

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
				System.Int32 __81fgg2dlsvn3432 = (System.Int32)(_dxpq0xkr);
				System.Int32 __81fgg2step3432 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count3432;
				for (__81fgg2count3432 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)2) - __81fgg2dlsvn3432 + __81fgg2step3432) / __81fgg2step3432)), _umlkckdg = __81fgg2dlsvn3432; __81fgg2count3432 != 0; __81fgg2count3432--, _umlkckdg += (__81fgg2step3432)) {

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
		//*     End of SLAGTS 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
