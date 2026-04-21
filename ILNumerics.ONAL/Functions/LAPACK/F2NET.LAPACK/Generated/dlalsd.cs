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
//*> \brief \b DLALSD uses the singular value decomposition of A to solve the least squares problem. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLALSD + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlalsd.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlalsd.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlalsd.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLALSD( UPLO, SMLSIZ, N, NRHS, D, E, B, LDB, RCOND, 
//*                          RANK, WORK, IWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          UPLO 
//*       INTEGER            INFO, LDB, N, NRHS, RANK, SMLSIZ 
//*       DOUBLE PRECISION   RCOND 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IWORK( * ) 
//*       DOUBLE PRECISION   B( LDB, * ), D( * ), E( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLALSD uses the singular value decomposition of A to solve the least 
//*> squares problem of finding X to minimize the Euclidean norm of each 
//*> column of A*X-B, where A is N-by-N upper bidiagonal, and X and B 
//*> are N-by-NRHS. The solution X overwrites B. 
//*> 
//*> The singular values of A smaller than RCOND times the largest 
//*> singular value are treated as zero in solving the least squares 
//*> problem; in this case a minimum norm solution is returned. 
//*> The actual singular values are returned in D in ascending order. 
//*> 
//*> This code makes very mild assumptions about floating point 
//*> arithmetic. It will work on machines with a guard digit in 
//*> add/subtract, or on those binary machines without guard digits 
//*> which subtract like the Cray XMP, Cray YMP, Cray C 90, or Cray 2. 
//*> It could conceivably fail on hexadecimal or decimal machines 
//*> without guard digits, but we know of none. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>         = 'U': D and E define an upper bidiagonal matrix. 
//*>         = 'L': D and E define a  lower bidiagonal matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] SMLSIZ 
//*> \verbatim 
//*>          SMLSIZ is INTEGER 
//*>         The maximum size of the subproblems at the bottom of the 
//*>         computation tree. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>         The dimension of the  bidiagonal matrix.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] NRHS 
//*> \verbatim 
//*>          NRHS is INTEGER 
//*>         The number of columns of B. NRHS must be at least 1. 
//*> \endverbatim 
//*> 
//*> \param[in,out] D 
//*> \verbatim 
//*>          D is DOUBLE PRECISION array, dimension (N) 
//*>         On entry D contains the main diagonal of the bidiagonal 
//*>         matrix. On exit, if INFO = 0, D contains its singular values. 
//*> \endverbatim 
//*> 
//*> \param[in,out] E 
//*> \verbatim 
//*>          E is DOUBLE PRECISION array, dimension (N-1) 
//*>         Contains the super-diagonal entries of the bidiagonal matrix. 
//*>         On exit, E has been destroyed. 
//*> \endverbatim 
//*> 
//*> \param[in,out] B 
//*> \verbatim 
//*>          B is DOUBLE PRECISION array, dimension (LDB,NRHS) 
//*>         On input, B contains the right hand sides of the least 
//*>         squares problem. On output, B contains the solution X. 
//*> \endverbatim 
//*> 
//*> \param[in] LDB 
//*> \verbatim 
//*>          LDB is INTEGER 
//*>         The leading dimension of B in the calling subprogram. 
//*>         LDB must be at least max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[in] RCOND 
//*> \verbatim 
//*>          RCOND is DOUBLE PRECISION 
//*>         The singular values of A less than or equal to RCOND times 
//*>         the largest singular value are treated as zero in solving 
//*>         the least squares problem. If RCOND is negative, 
//*>         machine precision is used instead. 
//*>         For example, if diag(S)*X=B were the least squares problem, 
//*>         where diag(S) is a diagonal matrix of singular values, the 
//*>         solution would be X(i) = B(i) / S(i) if S(i) is greater than 
//*>         RCOND*max(S), and X(i) = 0 if S(i) is less than or equal to 
//*>         RCOND*max(S). 
//*> \endverbatim 
//*> 
//*> \param[out] RANK 
//*> \verbatim 
//*>          RANK is INTEGER 
//*>         The number of singular values of A greater than RCOND times 
//*>         the largest singular value. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is DOUBLE PRECISION array, dimension at least 
//*>         (9*N + 2*N*SMLSIZ + 8*N*NLVL + N*NRHS + (SMLSIZ+1)**2), 
//*>         where NLVL = max(0, INT(log_2 (N/(SMLSIZ+1))) + 1). 
//*> \endverbatim 
//*> 
//*> \param[out] IWORK 
//*> \verbatim 
//*>          IWORK is INTEGER array, dimension at least 
//*>         (3*N*NLVL + 11*N) 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>         = 0:  successful exit. 
//*>         < 0:  if INFO = -i, the i-th argument had an illegal value. 
//*>         > 0:  The algorithm failed to compute a singular value while 
//*>               working on the submatrix lying in rows and columns 
//*>               INFO/(N+1) through MOD(INFO,N+1). 
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
//*>     Ming Gu and Ren-Cang Li, Computer Science Division, University of 
//*>       California at Berkeley, USA \n 
//*>     Osni Marques, LBNL/NERSC, USA \n 
//* 
//*  ===================================================================== 

	 
	public static void _uss5cegj(FString _9wyre9zc, ref Int32 _q1xpyios, ref Int32 _dxpq0xkr, ref Int32 _3nayvi7h, Double* _plfm7z8g, Double* _864fslqq, Double* _p9n405a5, ref Int32 _ly9opahg, ref Double _9zr5olpw, ref Int32 _uy2xc65y, Double* _apig8meb, Int32* _4b6rt45i, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Double _5m0mjfxm =  2d;
Int32 _uqckf55l =  default;
Int32 _x737e9xs =  default;
Int32 _3crf0qn3 =  default;
Int32 _i8976ehd =  default;
Int32 _doljbvm2 =  default;
Int32 _0zwn6fsy =  default;
Int32 _gh266ol1 =  default;
Int32 _8vecpt74 =  default;
Int32 _b5p6od9s =  default;
Int32 _2jyv4h8z =  default;
Int32 _9fy98qnj =  default;
Int32 _426n50rt =  default;
Int32 _znpjgsef =  default;
Int32 _umlkckdg =  default;
Int32 _0n683y3x =  default;
Int32 _3xbv3idt =  default;
Int32 _2bwe2jrn =  default;
Int32 _ew765vcx =  default;
Int32 _1myocm5q =  default;
Int32 _umao48xu =  default;
Int32 _7nk40y8b =  default;
Int32 _irk8i6qr =  default;
Int32 _stpw4s8v =  default;
Int32 _kku1nkf4 =  default;
Int32 _9qyq7j3e =  default;
Int32 _4ac2pvpn =  default;
Int32 _smlag6mh =  default;
Int32 _7u55mqkq =  default;
Int32 _xdbczr8u =  default;
Int32 _7e60fcso =  default;
Double _82tpdhyl =  default;
Double _p1iqarg6 =  default;
Double _wby36o6h =  default;
Double _q2vwp05i =  default;
Double _tu2rb1wg =  default;
Double _8tmd0ner =  default;
Double _txq1gp7u =  default;
string fLanavab = default;
#endregion  variable declarations
_9wyre9zc = _9wyre9zc.Convert(1);

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
		//*     .. External Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input parameters. 
		//* 
		
		_gro5yvfo = (int)0;//* 
		
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if (_3nayvi7h < (int)1)
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if ((_ly9opahg < (int)1) | (_ly9opahg < _dxpq0xkr))
		{
			
			_gro5yvfo = (int)-8;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("DLALSD" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		
		_p1iqarg6 = _f43eg0w0("Epsilon" );//* 
		//*     Set up the tolerance. 
		//* 
		
		if ((_9zr5olpw <= _d0547bi2) | (_9zr5olpw >= _kxg5drh2))
		{
			
			_tu2rb1wg = _p1iqarg6;
		}
		else
		{
			
			_tu2rb1wg = _9zr5olpw;
		}
		//* 
		
		_uy2xc65y = (int)0;//* 
		//*     Quick return if possible. 
		//* 
		
		if (_dxpq0xkr == (int)0)
		{
			
			return;
		}
		else
		if (_dxpq0xkr == (int)1)
		{
			
			if (*(_plfm7z8g+((int)1 - 1)) == _d0547bi2)
			{
				
				_rta9tuwm("A" ,ref Unsafe.AsRef((int)1) ,ref _3nayvi7h ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,_p9n405a5 ,ref _ly9opahg );
			}
			else
			{
				
				_uy2xc65y = (int)1;
				_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef(*(_plfm7z8g+((int)1 - 1))) ,ref Unsafe.AsRef(_kxg5drh2) ,ref Unsafe.AsRef((int)1) ,ref _3nayvi7h ,_p9n405a5 ,ref _ly9opahg ,ref _gro5yvfo );
				*(_plfm7z8g+((int)1 - 1)) = ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+((int)1 - 1)) );
			}
			
			return;
		}
		//* 
		//*     Rotate the matrix if it is lower bidiagonal. 
		//* 
		
		if (_9wyre9zc == "L")
		{
			
			{
				System.Int32 __81fgg2dlsvn1856 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1856 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1856;
				for (__81fgg2count1856 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn1856 + __81fgg2step1856) / __81fgg2step1856)), _b5p6od9s = __81fgg2dlsvn1856; __81fgg2count1856 != 0; __81fgg2count1856--, _b5p6od9s += (__81fgg2step1856)) {

				{
					
					_uasfzoa5(ref Unsafe.AsRef(*(_plfm7z8g+(_b5p6od9s - 1))) ,ref Unsafe.AsRef(*(_864fslqq+(_b5p6od9s - 1))) ,ref _82tpdhyl ,ref _8tmd0ner ,ref _q2vwp05i );
					*(_plfm7z8g+(_b5p6od9s - 1)) = _q2vwp05i;
					*(_864fslqq+(_b5p6od9s - 1)) = (_8tmd0ner * *(_plfm7z8g+(_b5p6od9s + (int)1 - 1)));
					*(_plfm7z8g+(_b5p6od9s + (int)1 - 1)) = (_82tpdhyl * *(_plfm7z8g+(_b5p6od9s + (int)1 - 1)));
					if (_3nayvi7h == (int)1)
					{
						
						_2197fa5i(ref Unsafe.AsRef((int)1) ,(_p9n405a5+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref Unsafe.AsRef((int)1) ,(_p9n405a5+(_b5p6od9s + (int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref Unsafe.AsRef((int)1) ,ref _82tpdhyl ,ref _8tmd0ner );
					}
					else
					{
						
						*(_apig8meb+((_b5p6od9s * (int)2) - (int)1 - 1)) = _82tpdhyl;
						*(_apig8meb+(_b5p6od9s * (int)2 - 1)) = _8tmd0ner;
					}
					
Mark10:;
					// continue
				}
								}			}
			if (_3nayvi7h > (int)1)
			{
				
				{
					System.Int32 __81fgg2dlsvn1857 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1857 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1857;
					for (__81fgg2count1857 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn1857 + __81fgg2step1857) / __81fgg2step1857)), _b5p6od9s = __81fgg2dlsvn1857; __81fgg2count1857 != 0; __81fgg2count1857--, _b5p6od9s += (__81fgg2step1857)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn1858 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1858 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1858;
							for (__81fgg2count1858 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn1858 + __81fgg2step1858) / __81fgg2step1858)), _znpjgsef = __81fgg2dlsvn1858; __81fgg2count1858 != 0; __81fgg2count1858--, _znpjgsef += (__81fgg2step1858)) {

							{
								
								_82tpdhyl = *(_apig8meb+((_znpjgsef * (int)2) - (int)1 - 1));
								_8tmd0ner = *(_apig8meb+(_znpjgsef * (int)2 - 1));
								_2197fa5i(ref Unsafe.AsRef((int)1) ,(_p9n405a5+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_ly9opahg)),ref Unsafe.AsRef((int)1) ,(_p9n405a5+(_znpjgsef + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ly9opahg)),ref Unsafe.AsRef((int)1) ,ref _82tpdhyl ,ref _8tmd0ner );
Mark20:;
								// continue
							}
														}						}
Mark30:;
						// continue
					}
										}				}
			}
			
		}
		//* 
		//*     Scale. 
		//* 
		
		_3xbv3idt = (_dxpq0xkr - (int)1);
		_wby36o6h = _j0e1628u("M" ,ref _dxpq0xkr ,_plfm7z8g ,_864fslqq );
		if (_wby36o6h == _d0547bi2)
		{
			
			_rta9tuwm("A" ,ref _dxpq0xkr ,ref _3nayvi7h ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,_p9n405a5 ,ref _ly9opahg );
			return;
		}
		//* 
		
		_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _wby36o6h ,ref Unsafe.AsRef(_kxg5drh2) ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1) ,_plfm7z8g ,ref _dxpq0xkr ,ref _gro5yvfo );
		_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _wby36o6h ,ref Unsafe.AsRef(_kxg5drh2) ,ref _3xbv3idt ,ref Unsafe.AsRef((int)1) ,_864fslqq ,ref _3xbv3idt ,ref _gro5yvfo );//* 
		//*     If N is smaller than the minimum divide size SMLSIZ, then solve 
		//*     the problem with another solver. 
		//* 
		
		if (_dxpq0xkr <= _q1xpyios)
		{
			
			_1myocm5q = ((int)1 + (_dxpq0xkr * _dxpq0xkr));
			_rta9tuwm("A" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _dxpq0xkr );
			_5gomekdd("U" ,ref Unsafe.AsRef((int)0) ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,ref _3nayvi7h ,_plfm7z8g ,_864fslqq ,_apig8meb ,ref _dxpq0xkr ,_apig8meb ,ref _dxpq0xkr ,_p9n405a5 ,ref _ly9opahg ,(_apig8meb+(_1myocm5q - 1)),ref _gro5yvfo );
			if (_gro5yvfo != (int)0)
			{
				
				return;
			}
			
			_txq1gp7u = (_tu2rb1wg * ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_ei7om7ok(ref _dxpq0xkr ,_plfm7z8g ,ref Unsafe.AsRef((int)1) ) - 1)) ));
			{
				System.Int32 __81fgg2dlsvn1859 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1859 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1859;
				for (__81fgg2count1859 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1859 + __81fgg2step1859) / __81fgg2step1859)), _b5p6od9s = __81fgg2dlsvn1859; __81fgg2count1859 != 0; __81fgg2count1859--, _b5p6od9s += (__81fgg2step1859)) {

				{
					
					if (*(_plfm7z8g+(_b5p6od9s - 1)) <= _txq1gp7u)
					{
						
						_rta9tuwm("A" ,ref Unsafe.AsRef((int)1) ,ref _3nayvi7h ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_p9n405a5+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg );
					}
					else
					{
						
						_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef(*(_plfm7z8g+(_b5p6od9s - 1))) ,ref Unsafe.AsRef(_kxg5drh2) ,ref Unsafe.AsRef((int)1) ,ref _3nayvi7h ,(_p9n405a5+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref _gro5yvfo );
						_uy2xc65y = (_uy2xc65y + (int)1);
					}
					
Mark40:;
					// continue
				}
								}			}
			_5nsxi69c("T" ,"N" ,ref _dxpq0xkr ,ref _3nayvi7h ,ref _dxpq0xkr ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _dxpq0xkr ,_p9n405a5 ,ref _ly9opahg ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_1myocm5q - 1)),ref _dxpq0xkr );
			_hhtvj1kb("A" ,ref _dxpq0xkr ,ref _3nayvi7h ,(_apig8meb+(_1myocm5q - 1)),ref _dxpq0xkr ,_p9n405a5 ,ref _ly9opahg );//* 
			//*        Unscale. 
			//* 
			
			_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef(_kxg5drh2) ,ref _wby36o6h ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1) ,_plfm7z8g ,ref _dxpq0xkr ,ref _gro5yvfo );
			_agod5jth("D" ,ref _dxpq0xkr ,_plfm7z8g ,ref _gro5yvfo );
			_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _wby36o6h ,ref Unsafe.AsRef(_kxg5drh2) ,ref _dxpq0xkr ,ref _3nayvi7h ,_p9n405a5 ,ref _ly9opahg ,ref _gro5yvfo );//* 
			
			return;
		}
		//* 
		//*     Book-keeping and setting up some constants. 
		//* 
		
		_0n683y3x = (ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.DBLE(_dxpq0xkr ) / ILNumerics.F2NET.Intrinsics.DBLE(_q1xpyios + (int)1 ) ) / ILNumerics.F2NET.Intrinsics.LOG(_5m0mjfxm ) ) + (int)1);//* 
		
		_kku1nkf4 = (_q1xpyios + (int)1);//* 
		
		_7u55mqkq = (int)1;
		_xdbczr8u = ((int)1 + (_q1xpyios * _dxpq0xkr));
		_i8976ehd = (_xdbczr8u + (_kku1nkf4 * _dxpq0xkr));
		_doljbvm2 = (_i8976ehd + (_0n683y3x * _dxpq0xkr));
		_7e60fcso = (_doljbvm2 + ((_0n683y3x * _dxpq0xkr) * (int)2));
		_3crf0qn3 = (_7e60fcso + (_0n683y3x * _dxpq0xkr));
		_irk8i6qr = (_3crf0qn3 + _dxpq0xkr);
		_7nk40y8b = (_irk8i6qr + _dxpq0xkr);
		_gh266ol1 = (_7nk40y8b + (((int)2 * _0n683y3x) * _dxpq0xkr));
		_uqckf55l = (_gh266ol1 + (((int)2 * _0n683y3x) * _dxpq0xkr));
		_1myocm5q = (_uqckf55l + (_dxpq0xkr * _3nayvi7h));//* 
		
		_stpw4s8v = ((int)1 + _dxpq0xkr);
		_umlkckdg = (_stpw4s8v + _dxpq0xkr);
		_8vecpt74 = (_umlkckdg + _dxpq0xkr);
		_umao48xu = (_8vecpt74 + _dxpq0xkr);
		_0zwn6fsy = (_umao48xu + (_0n683y3x * _dxpq0xkr));
		_426n50rt = (_0zwn6fsy + ((_0n683y3x * _dxpq0xkr) * (int)2));//* 
		
		_4ac2pvpn = (int)1;
		_9qyq7j3e = (int)0;
		_2jyv4h8z = (int)1;
		_9fy98qnj = (int)0;
		_ew765vcx = (int)0;//* 
		
		{
			System.Int32 __81fgg2dlsvn1860 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1860 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1860;
			for (__81fgg2count1860 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1860 + __81fgg2step1860) / __81fgg2step1860)), _b5p6od9s = __81fgg2dlsvn1860; __81fgg2count1860 != 0; __81fgg2count1860--, _b5p6od9s += (__81fgg2step1860)) {

			{
				
				if (ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_b5p6od9s - 1)) ) < _p1iqarg6)
				{
					
					*(_plfm7z8g+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.SIGN(_p1iqarg6 ,*(_plfm7z8g+(_b5p6od9s - 1)) );
				}
				
Mark50:;
				// continue
			}
						}		}//* 
		
		{
			System.Int32 __81fgg2dlsvn1861 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1861 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1861;
			for (__81fgg2count1861 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3xbv3idt) - __81fgg2dlsvn1861 + __81fgg2step1861) / __81fgg2step1861)), _b5p6od9s = __81fgg2dlsvn1861; __81fgg2count1861 != 0; __81fgg2count1861--, _b5p6od9s += (__81fgg2step1861)) {

			{
				
				if ((ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_b5p6od9s - 1)) ) < _p1iqarg6) | (_b5p6od9s == _3xbv3idt))
				{
					
					_ew765vcx = (_ew765vcx + (int)1);
					*(_4b6rt45i+(_ew765vcx - 1)) = _4ac2pvpn;//* 
					//*           Subproblem found. First determine its size and then 
					//*           apply divide and conquer on it. 
					//* 
					
					if (_b5p6od9s < _3xbv3idt)
					{
						//* 
						//*              A subproblem with E(I) small for I < NM1. 
						//* 
						
						_2bwe2jrn = ((_b5p6od9s - _4ac2pvpn) + (int)1);
						*(_4b6rt45i+((_stpw4s8v + _ew765vcx) - (int)1 - 1)) = _2bwe2jrn;
					}
					else
					if (ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_b5p6od9s - 1)) ) >= _p1iqarg6)
					{
						//* 
						//*              A subproblem with E(NM1) not too small but I = NM1. 
						//* 
						
						_2bwe2jrn = ((_dxpq0xkr - _4ac2pvpn) + (int)1);
						*(_4b6rt45i+((_stpw4s8v + _ew765vcx) - (int)1 - 1)) = _2bwe2jrn;
					}
					else
					{
						//* 
						//*              A subproblem with E(NM1) small. This implies an 
						//*              1-by-1 subproblem at D(N), which is not solved 
						//*              explicitly. 
						//* 
						
						_2bwe2jrn = ((_b5p6od9s - _4ac2pvpn) + (int)1);
						*(_4b6rt45i+((_stpw4s8v + _ew765vcx) - (int)1 - 1)) = _2bwe2jrn;
						_ew765vcx = (_ew765vcx + (int)1);
						*(_4b6rt45i+(_ew765vcx - 1)) = _dxpq0xkr;
						*(_4b6rt45i+((_stpw4s8v + _ew765vcx) - (int)1 - 1)) = (int)1;
						_gvjhlct0(ref _3nayvi7h ,(_p9n405a5+(_dxpq0xkr - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_apig8meb+(_uqckf55l + _3xbv3idt - 1)),ref _dxpq0xkr );
					}
					
					_smlag6mh = (_4ac2pvpn - (int)1);
					if (_2bwe2jrn == (int)1)
					{
						//* 
						//*              This is a 1-by-1 subproblem and is not solved 
						//*              explicitly. 
						//* 
						
						_gvjhlct0(ref _3nayvi7h ,(_p9n405a5+(_4ac2pvpn - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_apig8meb+(_uqckf55l + _smlag6mh - 1)),ref _dxpq0xkr );
					}
					else
					if (_2bwe2jrn <= _q1xpyios)
					{
						//* 
						//*              This is a small subproblem and is solved by DLASDQ. 
						//* 
						
						_rta9tuwm("A" ,ref _2bwe2jrn ,ref _2bwe2jrn ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+(_xdbczr8u + _smlag6mh - 1)),ref _dxpq0xkr );
						_5gomekdd("U" ,ref Unsafe.AsRef((int)0) ,ref _2bwe2jrn ,ref _2bwe2jrn ,ref Unsafe.AsRef((int)0) ,ref _3nayvi7h ,(_plfm7z8g+(_4ac2pvpn - 1)),(_864fslqq+(_4ac2pvpn - 1)),(_apig8meb+(_xdbczr8u + _smlag6mh - 1)),ref _dxpq0xkr ,(_apig8meb+(_1myocm5q - 1)),ref _dxpq0xkr ,(_p9n405a5+(_4ac2pvpn - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_apig8meb+(_1myocm5q - 1)),ref _gro5yvfo );
						if (_gro5yvfo != (int)0)
						{
							
							return;
						}
						
						_hhtvj1kb("A" ,ref _2bwe2jrn ,ref _3nayvi7h ,(_p9n405a5+(_4ac2pvpn - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_apig8meb+(_uqckf55l + _smlag6mh - 1)),ref _dxpq0xkr );
					}
					else
					{
						//* 
						//*              A large problem. Solve it using divide and conquer. 
						//* 
						
						_r8j651rt(ref _2jyv4h8z ,ref _q1xpyios ,ref _2bwe2jrn ,ref _9qyq7j3e ,(_plfm7z8g+(_4ac2pvpn - 1)),(_864fslqq+(_4ac2pvpn - 1)),(_apig8meb+(_7u55mqkq + _smlag6mh - 1)),ref _dxpq0xkr ,(_apig8meb+(_xdbczr8u + _smlag6mh - 1)),(_4b6rt45i+(_umlkckdg + _smlag6mh - 1)),(_apig8meb+(_i8976ehd + _smlag6mh - 1)),(_apig8meb+(_doljbvm2 + _smlag6mh - 1)),(_apig8meb+(_7e60fcso + _smlag6mh - 1)),(_apig8meb+(_7nk40y8b + _smlag6mh - 1)),(_4b6rt45i+(_8vecpt74 + _smlag6mh - 1)),(_4b6rt45i+(_0zwn6fsy + _smlag6mh - 1)),ref _dxpq0xkr ,(_4b6rt45i+(_umao48xu + _smlag6mh - 1)),(_apig8meb+(_gh266ol1 + _smlag6mh - 1)),(_apig8meb+(_3crf0qn3 + _smlag6mh - 1)),(_apig8meb+(_irk8i6qr + _smlag6mh - 1)),(_apig8meb+(_1myocm5q - 1)),(_4b6rt45i+(_426n50rt - 1)),ref _gro5yvfo );
						if (_gro5yvfo != (int)0)
						{
							
							return;
						}
						
						_x737e9xs = (_uqckf55l + _smlag6mh);
						_hack1l4q(ref _9fy98qnj ,ref _q1xpyios ,ref _2bwe2jrn ,ref _3nayvi7h ,(_p9n405a5+(_4ac2pvpn - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_apig8meb+(_x737e9xs - 1)),ref _dxpq0xkr ,(_apig8meb+(_7u55mqkq + _smlag6mh - 1)),ref _dxpq0xkr ,(_apig8meb+(_xdbczr8u + _smlag6mh - 1)),(_4b6rt45i+(_umlkckdg + _smlag6mh - 1)),(_apig8meb+(_i8976ehd + _smlag6mh - 1)),(_apig8meb+(_doljbvm2 + _smlag6mh - 1)),(_apig8meb+(_7e60fcso + _smlag6mh - 1)),(_apig8meb+(_7nk40y8b + _smlag6mh - 1)),(_4b6rt45i+(_8vecpt74 + _smlag6mh - 1)),(_4b6rt45i+(_0zwn6fsy + _smlag6mh - 1)),ref _dxpq0xkr ,(_4b6rt45i+(_umao48xu + _smlag6mh - 1)),(_apig8meb+(_gh266ol1 + _smlag6mh - 1)),(_apig8meb+(_3crf0qn3 + _smlag6mh - 1)),(_apig8meb+(_irk8i6qr + _smlag6mh - 1)),(_apig8meb+(_1myocm5q - 1)),(_4b6rt45i+(_426n50rt - 1)),ref _gro5yvfo );
						if (_gro5yvfo != (int)0)
						{
							
							return;
						}
						
					}
					
					_4ac2pvpn = (_b5p6od9s + (int)1);
				}
				
Mark60:;
				// continue
			}
						}		}//* 
		//*     Apply the singular values and treat the tiny ones as zero. 
		//* 
		
		_txq1gp7u = (_tu2rb1wg * ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_ei7om7ok(ref _dxpq0xkr ,_plfm7z8g ,ref Unsafe.AsRef((int)1) ) - 1)) ));//* 
		
		{
			System.Int32 __81fgg2dlsvn1862 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1862 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1862;
			for (__81fgg2count1862 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1862 + __81fgg2step1862) / __81fgg2step1862)), _b5p6od9s = __81fgg2dlsvn1862; __81fgg2count1862 != 0; __81fgg2count1862--, _b5p6od9s += (__81fgg2step1862)) {

			{
				//* 
				//*        Some of the elements in D can be negative because 1-by-1 
				//*        subproblems were not solved explicitly. 
				//* 
				
				if (ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_b5p6od9s - 1)) ) <= _txq1gp7u)
				{
					
					_rta9tuwm("A" ,ref Unsafe.AsRef((int)1) ,ref _3nayvi7h ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+((_uqckf55l + _b5p6od9s) - (int)1 - 1)),ref _dxpq0xkr );
				}
				else
				{
					
					_uy2xc65y = (_uy2xc65y + (int)1);
					_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef(*(_plfm7z8g+(_b5p6od9s - 1))) ,ref Unsafe.AsRef(_kxg5drh2) ,ref Unsafe.AsRef((int)1) ,ref _3nayvi7h ,(_apig8meb+((_uqckf55l + _b5p6od9s) - (int)1 - 1)),ref _dxpq0xkr ,ref _gro5yvfo );
				}
				
				*(_plfm7z8g+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_b5p6od9s - 1)) );
Mark70:;
				// continue
			}
						}		}//* 
		//*     Now apply back the right singular vectors. 
		//* 
		
		_9fy98qnj = (int)1;
		{
			System.Int32 __81fgg2dlsvn1863 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1863 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1863;
			for (__81fgg2count1863 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ew765vcx) - __81fgg2dlsvn1863 + __81fgg2step1863) / __81fgg2step1863)), _b5p6od9s = __81fgg2dlsvn1863; __81fgg2count1863 != 0; __81fgg2count1863--, _b5p6od9s += (__81fgg2step1863)) {

			{
				
				_4ac2pvpn = *(_4b6rt45i+(_b5p6od9s - 1));
				_smlag6mh = (_4ac2pvpn - (int)1);
				_2bwe2jrn = *(_4b6rt45i+((_stpw4s8v + _b5p6od9s) - (int)1 - 1));
				_x737e9xs = (_uqckf55l + _smlag6mh);
				if (_2bwe2jrn == (int)1)
				{
					
					_gvjhlct0(ref _3nayvi7h ,(_apig8meb+(_x737e9xs - 1)),ref _dxpq0xkr ,(_p9n405a5+(_4ac2pvpn - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg );
				}
				else
				if (_2bwe2jrn <= _q1xpyios)
				{
					
					_5nsxi69c("T" ,"N" ,ref _2bwe2jrn ,ref _3nayvi7h ,ref _2bwe2jrn ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+(_xdbczr8u + _smlag6mh - 1)),ref _dxpq0xkr ,(_apig8meb+(_x737e9xs - 1)),ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,(_p9n405a5+(_4ac2pvpn - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg );
				}
				else
				{
					
					_hack1l4q(ref _9fy98qnj ,ref _q1xpyios ,ref _2bwe2jrn ,ref _3nayvi7h ,(_apig8meb+(_x737e9xs - 1)),ref _dxpq0xkr ,(_p9n405a5+(_4ac2pvpn - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_apig8meb+(_7u55mqkq + _smlag6mh - 1)),ref _dxpq0xkr ,(_apig8meb+(_xdbczr8u + _smlag6mh - 1)),(_4b6rt45i+(_umlkckdg + _smlag6mh - 1)),(_apig8meb+(_i8976ehd + _smlag6mh - 1)),(_apig8meb+(_doljbvm2 + _smlag6mh - 1)),(_apig8meb+(_7e60fcso + _smlag6mh - 1)),(_apig8meb+(_7nk40y8b + _smlag6mh - 1)),(_4b6rt45i+(_8vecpt74 + _smlag6mh - 1)),(_4b6rt45i+(_0zwn6fsy + _smlag6mh - 1)),ref _dxpq0xkr ,(_4b6rt45i+(_umao48xu + _smlag6mh - 1)),(_apig8meb+(_gh266ol1 + _smlag6mh - 1)),(_apig8meb+(_3crf0qn3 + _smlag6mh - 1)),(_apig8meb+(_irk8i6qr + _smlag6mh - 1)),(_apig8meb+(_1myocm5q - 1)),(_4b6rt45i+(_426n50rt - 1)),ref _gro5yvfo );
					if (_gro5yvfo != (int)0)
					{
						
						return;
					}
					
				}
				
Mark80:;
				// continue
			}
						}		}//* 
		//*     Unscale and sort the singular values. 
		//* 
		
		_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef(_kxg5drh2) ,ref _wby36o6h ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1) ,_plfm7z8g ,ref _dxpq0xkr ,ref _gro5yvfo );
		_agod5jth("D" ,ref _dxpq0xkr ,_plfm7z8g ,ref _gro5yvfo );
		_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _wby36o6h ,ref Unsafe.AsRef(_kxg5drh2) ,ref _dxpq0xkr ,ref _3nayvi7h ,_p9n405a5 ,ref _ly9opahg ,ref _gro5yvfo );//* 
		
		return;//* 
		//*     End of DLALSD 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
