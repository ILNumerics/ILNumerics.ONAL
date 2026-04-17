
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
//*> \brief \b ZLALSD uses the singular value decomposition of A to solve the least squares problem. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZLALSD + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zlalsd.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zlalsd.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zlalsd.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZLALSD( UPLO, SMLSIZ, N, NRHS, D, E, B, LDB, RCOND, 
//*                          RANK, WORK, RWORK, IWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          UPLO 
//*       INTEGER            INFO, LDB, N, NRHS, RANK, SMLSIZ 
//*       DOUBLE PRECISION   RCOND 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IWORK( * ) 
//*       DOUBLE PRECISION   D( * ), E( * ), RWORK( * ) 
//*       COMPLEX*16         B( LDB, * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZLALSD uses the singular value decomposition of A to solve the least 
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
//*>          B is COMPLEX*16 array, dimension (LDB,NRHS) 
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
//*>          WORK is COMPLEX*16 array, dimension (N * NRHS) 
//*> \endverbatim 
//*> 
//*> \param[out] RWORK 
//*> \verbatim 
//*>          RWORK is DOUBLE PRECISION array, dimension at least 
//*>         (9*N + 2*N*SMLSIZ + 8*N*NLVL + 3*SMLSIZ*NRHS + 
//*>         MAX( (SMLSIZ+1)**2, N*(1+NRHS) + 2*NRHS ), 
//*>         where 
//*>         NLVL = MAX( 0, INT( LOG_2( MIN( M,N )/(SMLSIZ+1) ) ) + 1 ) 
//*> \endverbatim 
//*> 
//*> \param[out] IWORK 
//*> \verbatim 
//*>          IWORK is INTEGER array, dimension at least 
//*>         (3*N*NLVL + 11*N). 
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
//*> \date June 2017 
//* 
//*> \ingroup complex16OTHERcomputational 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>     Ming Gu and Ren-Cang Li, Computer Science Division, University of 
//*>       California at Berkeley, USA \n 
//*>     Osni Marques, LBNL/NERSC, USA \n 
//* 
//*  ===================================================================== 

	 
	public static void _fw6ai0ms(FString _9wyre9zc, ref Int32 _q1xpyios, ref Int32 _dxpq0xkr, ref Int32 _3nayvi7h, Double* _plfm7z8g, Double* _864fslqq, complex* _p9n405a5, ref Int32 _ly9opahg, ref Double _9zr5olpw, ref Int32 _uy2xc65y, complex* _apig8meb, Double* _dqanbbw3, Int32* _4b6rt45i, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Double _5m0mjfxm =  2d;
complex _gdjumcqt =   new fcomplex(0f,0f);
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
Int32 _tbvraoac =  default;
Int32 _8haoin41 =  default;
Int32 _oetrgbzj =  default;
Int32 _ap4cac77 =  default;
Int32 _fdt03m7m =  default;
Int32 _ihsgdr6g =  default;
Int32 _426n50rt =  default;
Int32 _znpjgsef =  default;
Int32 _vmeebitr =  default;
Int32 _llqvt7c6 =  default;
Int32 _jcjf3d8c =  default;
Int32 _hgi9ttgs =  default;
Int32 _umlkckdg =  default;
Int32 _0n683y3x =  default;
Int32 _3xbv3idt =  default;
Int32 _r49fp4o3 =  default;
Int32 _2bwe2jrn =  default;
Int32 _ew765vcx =  default;
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
Double _tu2rb1wg =  default;
Double _q2vwp05i =  default;
Double _8tmd0ner =  default;
Double _txq1gp7u =  default;
string fLanavab = default;
#endregion  variable declarations
_9wyre9zc = _9wyre9zc.Convert(1);

	{
		//* 
		//*  -- LAPACK computational routine (version 3.7.1) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     June 2017 
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
			
			_ut9qalzx("ZLALSD" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
				
				_k14i9nd8("A" ,ref Unsafe.AsRef((int)1) ,ref _3nayvi7h ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_gdjumcqt) ,_p9n405a5 ,ref _ly9opahg );
			}
			else
			{
				
				_uy2xc65y = (int)1;
				_j6h8q4u5("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef(*(_plfm7z8g+((int)1 - 1))) ,ref Unsafe.AsRef(_kxg5drh2) ,ref Unsafe.AsRef((int)1) ,ref _3nayvi7h ,_p9n405a5 ,ref _ly9opahg ,ref _gro5yvfo );
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
				System.Int32 __81fgg2dlsvn1989 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1989 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1989;
				for (__81fgg2count1989 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn1989 + __81fgg2step1989) / __81fgg2step1989)), _b5p6od9s = __81fgg2dlsvn1989; __81fgg2count1989 != 0; __81fgg2count1989--, _b5p6od9s += (__81fgg2step1989)) {

				{
					
					_uasfzoa5(ref Unsafe.AsRef(*(_plfm7z8g+(_b5p6od9s - 1))) ,ref Unsafe.AsRef(*(_864fslqq+(_b5p6od9s - 1))) ,ref _82tpdhyl ,ref _8tmd0ner ,ref _q2vwp05i );
					*(_plfm7z8g+(_b5p6od9s - 1)) = _q2vwp05i;
					*(_864fslqq+(_b5p6od9s - 1)) = (_8tmd0ner * *(_plfm7z8g+(_b5p6od9s + (int)1 - 1)));
					*(_plfm7z8g+(_b5p6od9s + (int)1 - 1)) = (_82tpdhyl * *(_plfm7z8g+(_b5p6od9s + (int)1 - 1)));
					if (_3nayvi7h == (int)1)
					{
						
						_yl1tlikm(ref Unsafe.AsRef((int)1) ,(_p9n405a5+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref Unsafe.AsRef((int)1) ,(_p9n405a5+(_b5p6od9s + (int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref Unsafe.AsRef((int)1) ,ref _82tpdhyl ,ref _8tmd0ner );
					}
					else
					{
						
						*(_dqanbbw3+((_b5p6od9s * (int)2) - (int)1 - 1)) = _82tpdhyl;
						*(_dqanbbw3+(_b5p6od9s * (int)2 - 1)) = _8tmd0ner;
					}
					
Mark10:;
					// continue
				}
								}			}
			if (_3nayvi7h > (int)1)
			{
				
				{
					System.Int32 __81fgg2dlsvn1990 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1990 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1990;
					for (__81fgg2count1990 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn1990 + __81fgg2step1990) / __81fgg2step1990)), _b5p6od9s = __81fgg2dlsvn1990; __81fgg2count1990 != 0; __81fgg2count1990--, _b5p6od9s += (__81fgg2step1990)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn1991 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1991 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1991;
							for (__81fgg2count1991 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn1991 + __81fgg2step1991) / __81fgg2step1991)), _znpjgsef = __81fgg2dlsvn1991; __81fgg2count1991 != 0; __81fgg2count1991--, _znpjgsef += (__81fgg2step1991)) {

							{
								
								_82tpdhyl = *(_dqanbbw3+((_znpjgsef * (int)2) - (int)1 - 1));
								_8tmd0ner = *(_dqanbbw3+(_znpjgsef * (int)2 - 1));
								_yl1tlikm(ref Unsafe.AsRef((int)1) ,(_p9n405a5+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_ly9opahg)),ref Unsafe.AsRef((int)1) ,(_p9n405a5+(_znpjgsef + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ly9opahg)),ref Unsafe.AsRef((int)1) ,ref _82tpdhyl ,ref _8tmd0ner );
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
			
			_k14i9nd8("A" ,ref _dxpq0xkr ,ref _3nayvi7h ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_gdjumcqt) ,_p9n405a5 ,ref _ly9opahg );
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
			
			_ap4cac77 = (int)1;
			_fdt03m7m = (_ap4cac77 + (_dxpq0xkr * _dxpq0xkr));
			_ihsgdr6g = (_fdt03m7m + (_dxpq0xkr * _dxpq0xkr));
			_oetrgbzj = _ihsgdr6g;
			_8haoin41 = (_oetrgbzj + (_dxpq0xkr * _3nayvi7h));
			_tbvraoac = (_8haoin41 + (_dxpq0xkr * _3nayvi7h));
			_rta9tuwm("A" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,(_dqanbbw3+(_ap4cac77 - 1)),ref _dxpq0xkr );
			_rta9tuwm("A" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,(_dqanbbw3+(_fdt03m7m - 1)),ref _dxpq0xkr );
			_5gomekdd("U" ,ref Unsafe.AsRef((int)0) ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,_plfm7z8g ,_864fslqq ,(_dqanbbw3+(_fdt03m7m - 1)),ref _dxpq0xkr ,(_dqanbbw3+(_ap4cac77 - 1)),ref _dxpq0xkr ,(_dqanbbw3+(_ihsgdr6g - 1)),ref Unsafe.AsRef((int)1) ,(_dqanbbw3+(_ihsgdr6g - 1)),ref _gro5yvfo );
			if (_gro5yvfo != (int)0)
			{
				
				return;
			}
			//* 
			//*        In the real version, B is passed to DLASDQ and multiplied 
			//*        internally by Q**H. Here B is complex and that product is 
			//*        computed below in two steps (real and imaginary parts). 
			//* 
			
			_znpjgsef = (_tbvraoac - (int)1);
			{
				System.Int32 __81fgg2dlsvn1992 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1992 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1992;
				for (__81fgg2count1992 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn1992 + __81fgg2step1992) / __81fgg2step1992)), _vmeebitr = __81fgg2dlsvn1992; __81fgg2count1992 != 0; __81fgg2count1992--, _vmeebitr += (__81fgg2step1992)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn1993 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1993 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1993;
						for (__81fgg2count1993 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1993 + __81fgg2step1993) / __81fgg2step1993)), _hgi9ttgs = __81fgg2dlsvn1993; __81fgg2count1993 != 0; __81fgg2count1993--, _hgi9ttgs += (__81fgg2step1993)) {

						{
							
							_znpjgsef = (_znpjgsef + (int)1);
							*(_dqanbbw3+(_znpjgsef - 1)) = ILNumerics.F2NET.Intrinsics.DBLE(*(_p9n405a5+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_ly9opahg)) );
Mark40:;
							// continue
						}
												}					}
Mark50:;
					// continue
				}
								}			}
			_5nsxi69c("T" ,"N" ,ref _dxpq0xkr ,ref _3nayvi7h ,ref _dxpq0xkr ,ref Unsafe.AsRef(_kxg5drh2) ,(_dqanbbw3+(_ap4cac77 - 1)),ref _dxpq0xkr ,(_dqanbbw3+(_tbvraoac - 1)),ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+(_oetrgbzj - 1)),ref _dxpq0xkr );
			_znpjgsef = (_tbvraoac - (int)1);
			{
				System.Int32 __81fgg2dlsvn1994 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1994 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1994;
				for (__81fgg2count1994 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn1994 + __81fgg2step1994) / __81fgg2step1994)), _vmeebitr = __81fgg2dlsvn1994; __81fgg2count1994 != 0; __81fgg2count1994--, _vmeebitr += (__81fgg2step1994)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn1995 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1995 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1995;
						for (__81fgg2count1995 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1995 + __81fgg2step1995) / __81fgg2step1995)), _hgi9ttgs = __81fgg2dlsvn1995; __81fgg2count1995 != 0; __81fgg2count1995--, _hgi9ttgs += (__81fgg2step1995)) {

						{
							
							_znpjgsef = (_znpjgsef + (int)1);
							*(_dqanbbw3+(_znpjgsef - 1)) = ILNumerics.F2NET.Intrinsics.DIMAG(*(_p9n405a5+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_ly9opahg)) );
Mark60:;
							// continue
						}
												}					}
Mark70:;
					// continue
				}
								}			}
			_5nsxi69c("T" ,"N" ,ref _dxpq0xkr ,ref _3nayvi7h ,ref _dxpq0xkr ,ref Unsafe.AsRef(_kxg5drh2) ,(_dqanbbw3+(_ap4cac77 - 1)),ref _dxpq0xkr ,(_dqanbbw3+(_tbvraoac - 1)),ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+(_8haoin41 - 1)),ref _dxpq0xkr );
			_jcjf3d8c = (_oetrgbzj - (int)1);
			_llqvt7c6 = (_8haoin41 - (int)1);
			{
				System.Int32 __81fgg2dlsvn1996 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1996 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1996;
				for (__81fgg2count1996 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn1996 + __81fgg2step1996) / __81fgg2step1996)), _vmeebitr = __81fgg2dlsvn1996; __81fgg2count1996 != 0; __81fgg2count1996--, _vmeebitr += (__81fgg2step1996)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn1997 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1997 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1997;
						for (__81fgg2count1997 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1997 + __81fgg2step1997) / __81fgg2step1997)), _hgi9ttgs = __81fgg2dlsvn1997; __81fgg2count1997 != 0; __81fgg2count1997--, _hgi9ttgs += (__81fgg2step1997)) {

						{
							
							_jcjf3d8c = (_jcjf3d8c + (int)1);
							_llqvt7c6 = (_llqvt7c6 + (int)1);
							*(_p9n405a5+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_ly9opahg)) = ILNumerics.F2NET.Intrinsics.DCMPLX(*(_dqanbbw3+(_jcjf3d8c - 1)) ,*(_dqanbbw3+(_llqvt7c6 - 1)) );
Mark80:;
							// continue
						}
												}					}
Mark90:;
					// continue
				}
								}			}//* 
			
			_txq1gp7u = (_tu2rb1wg * ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_ei7om7ok(ref _dxpq0xkr ,_plfm7z8g ,ref Unsafe.AsRef((int)1) ) - 1)) ));
			{
				System.Int32 __81fgg2dlsvn1998 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1998 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1998;
				for (__81fgg2count1998 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1998 + __81fgg2step1998) / __81fgg2step1998)), _b5p6od9s = __81fgg2dlsvn1998; __81fgg2count1998 != 0; __81fgg2count1998--, _b5p6od9s += (__81fgg2step1998)) {

				{
					
					if (*(_plfm7z8g+(_b5p6od9s - 1)) <= _txq1gp7u)
					{
						
						_k14i9nd8("A" ,ref Unsafe.AsRef((int)1) ,ref _3nayvi7h ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_gdjumcqt) ,(_p9n405a5+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg );
					}
					else
					{
						
						_j6h8q4u5("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef(*(_plfm7z8g+(_b5p6od9s - 1))) ,ref Unsafe.AsRef(_kxg5drh2) ,ref Unsafe.AsRef((int)1) ,ref _3nayvi7h ,(_p9n405a5+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref _gro5yvfo );
						_uy2xc65y = (_uy2xc65y + (int)1);
					}
					
Mark100:;
					// continue
				}
								}			}//* 
			//*        Since B is complex, the following call to DGEMM is performed 
			//*        in two steps (real and imaginary parts). That is for V * B 
			//*        (in the real version of the code V**H is stored in WORK). 
			//* 
			//*        CALL DGEMM( 'T', 'N', N, NRHS, N, ONE, WORK, N, B, LDB, ZERO, 
			//*    $               WORK( NWORK ), N ) 
			//* 
			
			_znpjgsef = (_tbvraoac - (int)1);
			{
				System.Int32 __81fgg2dlsvn1999 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1999 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1999;
				for (__81fgg2count1999 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn1999 + __81fgg2step1999) / __81fgg2step1999)), _vmeebitr = __81fgg2dlsvn1999; __81fgg2count1999 != 0; __81fgg2count1999--, _vmeebitr += (__81fgg2step1999)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn2000 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2000 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2000;
						for (__81fgg2count2000 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2000 + __81fgg2step2000) / __81fgg2step2000)), _hgi9ttgs = __81fgg2dlsvn2000; __81fgg2count2000 != 0; __81fgg2count2000--, _hgi9ttgs += (__81fgg2step2000)) {

						{
							
							_znpjgsef = (_znpjgsef + (int)1);
							*(_dqanbbw3+(_znpjgsef - 1)) = ILNumerics.F2NET.Intrinsics.DBLE(*(_p9n405a5+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_ly9opahg)) );
Mark110:;
							// continue
						}
												}					}
Mark120:;
					// continue
				}
								}			}
			_5nsxi69c("T" ,"N" ,ref _dxpq0xkr ,ref _3nayvi7h ,ref _dxpq0xkr ,ref Unsafe.AsRef(_kxg5drh2) ,(_dqanbbw3+(_fdt03m7m - 1)),ref _dxpq0xkr ,(_dqanbbw3+(_tbvraoac - 1)),ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+(_oetrgbzj - 1)),ref _dxpq0xkr );
			_znpjgsef = (_tbvraoac - (int)1);
			{
				System.Int32 __81fgg2dlsvn2001 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2001 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2001;
				for (__81fgg2count2001 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn2001 + __81fgg2step2001) / __81fgg2step2001)), _vmeebitr = __81fgg2dlsvn2001; __81fgg2count2001 != 0; __81fgg2count2001--, _vmeebitr += (__81fgg2step2001)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn2002 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2002 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2002;
						for (__81fgg2count2002 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2002 + __81fgg2step2002) / __81fgg2step2002)), _hgi9ttgs = __81fgg2dlsvn2002; __81fgg2count2002 != 0; __81fgg2count2002--, _hgi9ttgs += (__81fgg2step2002)) {

						{
							
							_znpjgsef = (_znpjgsef + (int)1);
							*(_dqanbbw3+(_znpjgsef - 1)) = ILNumerics.F2NET.Intrinsics.DIMAG(*(_p9n405a5+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_ly9opahg)) );
Mark130:;
							// continue
						}
												}					}
Mark140:;
					// continue
				}
								}			}
			_5nsxi69c("T" ,"N" ,ref _dxpq0xkr ,ref _3nayvi7h ,ref _dxpq0xkr ,ref Unsafe.AsRef(_kxg5drh2) ,(_dqanbbw3+(_fdt03m7m - 1)),ref _dxpq0xkr ,(_dqanbbw3+(_tbvraoac - 1)),ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+(_8haoin41 - 1)),ref _dxpq0xkr );
			_jcjf3d8c = (_oetrgbzj - (int)1);
			_llqvt7c6 = (_8haoin41 - (int)1);
			{
				System.Int32 __81fgg2dlsvn2003 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2003 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2003;
				for (__81fgg2count2003 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn2003 + __81fgg2step2003) / __81fgg2step2003)), _vmeebitr = __81fgg2dlsvn2003; __81fgg2count2003 != 0; __81fgg2count2003--, _vmeebitr += (__81fgg2step2003)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn2004 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2004 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2004;
						for (__81fgg2count2004 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2004 + __81fgg2step2004) / __81fgg2step2004)), _hgi9ttgs = __81fgg2dlsvn2004; __81fgg2count2004 != 0; __81fgg2count2004--, _hgi9ttgs += (__81fgg2step2004)) {

						{
							
							_jcjf3d8c = (_jcjf3d8c + (int)1);
							_llqvt7c6 = (_llqvt7c6 + (int)1);
							*(_p9n405a5+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_ly9opahg)) = ILNumerics.F2NET.Intrinsics.DCMPLX(*(_dqanbbw3+(_jcjf3d8c - 1)) ,*(_dqanbbw3+(_llqvt7c6 - 1)) );
Mark150:;
							// continue
						}
												}					}
Mark160:;
					// continue
				}
								}			}//* 
			//*        Unscale. 
			//* 
			
			_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef(_kxg5drh2) ,ref _wby36o6h ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1) ,_plfm7z8g ,ref _dxpq0xkr ,ref _gro5yvfo );
			_agod5jth("D" ,ref _dxpq0xkr ,_plfm7z8g ,ref _gro5yvfo );
			_j6h8q4u5("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _wby36o6h ,ref Unsafe.AsRef(_kxg5drh2) ,ref _dxpq0xkr ,ref _3nayvi7h ,_p9n405a5 ,ref _ly9opahg ,ref _gro5yvfo );//* 
			
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
		_r49fp4o3 = (_gh266ol1 + (((int)2 * _0n683y3x) * _dxpq0xkr));
		_uqckf55l = (int)1;//* 
		
		_oetrgbzj = _r49fp4o3;
		_8haoin41 = (_oetrgbzj + (_q1xpyios * _3nayvi7h));
		_tbvraoac = (_8haoin41 + (_q1xpyios * _3nayvi7h));//* 
		
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
			System.Int32 __81fgg2dlsvn2005 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2005 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2005;
			for (__81fgg2count2005 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2005 + __81fgg2step2005) / __81fgg2step2005)), _b5p6od9s = __81fgg2dlsvn2005; __81fgg2count2005 != 0; __81fgg2count2005--, _b5p6od9s += (__81fgg2step2005)) {

			{
				
				if (ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_b5p6od9s - 1)) ) < _p1iqarg6)
				{
					
					*(_plfm7z8g+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.SIGN(_p1iqarg6 ,*(_plfm7z8g+(_b5p6od9s - 1)) );
				}
				
Mark170:;
				// continue
			}
						}		}//* 
		
		{
			System.Int32 __81fgg2dlsvn2006 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2006 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2006;
			for (__81fgg2count2006 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3xbv3idt) - __81fgg2dlsvn2006 + __81fgg2step2006) / __81fgg2step2006)), _b5p6od9s = __81fgg2dlsvn2006; __81fgg2count2006 != 0; __81fgg2count2006--, _b5p6od9s += (__81fgg2step2006)) {

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
						_ly902k7t(ref _3nayvi7h ,(_p9n405a5+(_dxpq0xkr - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_apig8meb+(_uqckf55l + _3xbv3idt - 1)),ref _dxpq0xkr );
					}
					
					_smlag6mh = (_4ac2pvpn - (int)1);
					if (_2bwe2jrn == (int)1)
					{
						//* 
						//*              This is a 1-by-1 subproblem and is not solved 
						//*              explicitly. 
						//* 
						
						_ly902k7t(ref _3nayvi7h ,(_p9n405a5+(_4ac2pvpn - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_apig8meb+(_uqckf55l + _smlag6mh - 1)),ref _dxpq0xkr );
					}
					else
					if (_2bwe2jrn <= _q1xpyios)
					{
						//* 
						//*              This is a small subproblem and is solved by DLASDQ. 
						//* 
						
						_rta9tuwm("A" ,ref _2bwe2jrn ,ref _2bwe2jrn ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,(_dqanbbw3+(_xdbczr8u + _smlag6mh - 1)),ref _dxpq0xkr );
						_rta9tuwm("A" ,ref _2bwe2jrn ,ref _2bwe2jrn ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,(_dqanbbw3+(_7u55mqkq + _smlag6mh - 1)),ref _dxpq0xkr );
						_5gomekdd("U" ,ref Unsafe.AsRef((int)0) ,ref _2bwe2jrn ,ref _2bwe2jrn ,ref _2bwe2jrn ,ref Unsafe.AsRef((int)0) ,(_plfm7z8g+(_4ac2pvpn - 1)),(_864fslqq+(_4ac2pvpn - 1)),(_dqanbbw3+(_xdbczr8u + _smlag6mh - 1)),ref _dxpq0xkr ,(_dqanbbw3+(_7u55mqkq + _smlag6mh - 1)),ref _dxpq0xkr ,(_dqanbbw3+(_r49fp4o3 - 1)),ref Unsafe.AsRef((int)1) ,(_dqanbbw3+(_r49fp4o3 - 1)),ref _gro5yvfo );
						if (_gro5yvfo != (int)0)
						{
							
							return;
						}
						//* 
						//*              In the real version, B is passed to DLASDQ and multiplied 
						//*              internally by Q**H. Here B is complex and that product is 
						//*              computed below in two steps (real and imaginary parts). 
						//* 
						
						_znpjgsef = (_tbvraoac - (int)1);
						{
							System.Int32 __81fgg2dlsvn2007 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step2007 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2007;
							for (__81fgg2count2007 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn2007 + __81fgg2step2007) / __81fgg2step2007)), _vmeebitr = __81fgg2dlsvn2007; __81fgg2count2007 != 0; __81fgg2count2007--, _vmeebitr += (__81fgg2step2007)) {

							{
								
								{
									System.Int32 __81fgg2dlsvn2008 = (System.Int32)(_4ac2pvpn);
									const System.Int32 __81fgg2step2008 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2008;
									for (__81fgg2count2008 = System.Math.Max(0, (System.Int32)(((System.Int32)((_4ac2pvpn + _2bwe2jrn) - (int)1) - __81fgg2dlsvn2008 + __81fgg2step2008) / __81fgg2step2008)), _hgi9ttgs = __81fgg2dlsvn2008; __81fgg2count2008 != 0; __81fgg2count2008--, _hgi9ttgs += (__81fgg2step2008)) {

									{
										
										_znpjgsef = (_znpjgsef + (int)1);
										*(_dqanbbw3+(_znpjgsef - 1)) = ILNumerics.F2NET.Intrinsics.DBLE(*(_p9n405a5+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_ly9opahg)) );
Mark180:;
										// continue
									}
																		}								}
Mark190:;
								// continue
							}
														}						}
						_5nsxi69c("T" ,"N" ,ref _2bwe2jrn ,ref _3nayvi7h ,ref _2bwe2jrn ,ref Unsafe.AsRef(_kxg5drh2) ,(_dqanbbw3+(_7u55mqkq + _smlag6mh - 1)),ref _dxpq0xkr ,(_dqanbbw3+(_tbvraoac - 1)),ref _2bwe2jrn ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+(_oetrgbzj - 1)),ref _2bwe2jrn );
						_znpjgsef = (_tbvraoac - (int)1);
						{
							System.Int32 __81fgg2dlsvn2009 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step2009 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2009;
							for (__81fgg2count2009 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn2009 + __81fgg2step2009) / __81fgg2step2009)), _vmeebitr = __81fgg2dlsvn2009; __81fgg2count2009 != 0; __81fgg2count2009--, _vmeebitr += (__81fgg2step2009)) {

							{
								
								{
									System.Int32 __81fgg2dlsvn2010 = (System.Int32)(_4ac2pvpn);
									const System.Int32 __81fgg2step2010 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2010;
									for (__81fgg2count2010 = System.Math.Max(0, (System.Int32)(((System.Int32)((_4ac2pvpn + _2bwe2jrn) - (int)1) - __81fgg2dlsvn2010 + __81fgg2step2010) / __81fgg2step2010)), _hgi9ttgs = __81fgg2dlsvn2010; __81fgg2count2010 != 0; __81fgg2count2010--, _hgi9ttgs += (__81fgg2step2010)) {

									{
										
										_znpjgsef = (_znpjgsef + (int)1);
										*(_dqanbbw3+(_znpjgsef - 1)) = ILNumerics.F2NET.Intrinsics.DIMAG(*(_p9n405a5+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_ly9opahg)) );
Mark200:;
										// continue
									}
																		}								}
Mark210:;
								// continue
							}
														}						}
						_5nsxi69c("T" ,"N" ,ref _2bwe2jrn ,ref _3nayvi7h ,ref _2bwe2jrn ,ref Unsafe.AsRef(_kxg5drh2) ,(_dqanbbw3+(_7u55mqkq + _smlag6mh - 1)),ref _dxpq0xkr ,(_dqanbbw3+(_tbvraoac - 1)),ref _2bwe2jrn ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+(_8haoin41 - 1)),ref _2bwe2jrn );
						_jcjf3d8c = (_oetrgbzj - (int)1);
						_llqvt7c6 = (_8haoin41 - (int)1);
						{
							System.Int32 __81fgg2dlsvn2011 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step2011 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2011;
							for (__81fgg2count2011 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn2011 + __81fgg2step2011) / __81fgg2step2011)), _vmeebitr = __81fgg2dlsvn2011; __81fgg2count2011 != 0; __81fgg2count2011--, _vmeebitr += (__81fgg2step2011)) {

							{
								
								{
									System.Int32 __81fgg2dlsvn2012 = (System.Int32)(_4ac2pvpn);
									const System.Int32 __81fgg2step2012 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2012;
									for (__81fgg2count2012 = System.Math.Max(0, (System.Int32)(((System.Int32)((_4ac2pvpn + _2bwe2jrn) - (int)1) - __81fgg2dlsvn2012 + __81fgg2step2012) / __81fgg2step2012)), _hgi9ttgs = __81fgg2dlsvn2012; __81fgg2count2012 != 0; __81fgg2count2012--, _hgi9ttgs += (__81fgg2step2012)) {

									{
										
										_jcjf3d8c = (_jcjf3d8c + (int)1);
										_llqvt7c6 = (_llqvt7c6 + (int)1);
										*(_p9n405a5+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_ly9opahg)) = ILNumerics.F2NET.Intrinsics.DCMPLX(*(_dqanbbw3+(_jcjf3d8c - 1)) ,*(_dqanbbw3+(_llqvt7c6 - 1)) );
Mark220:;
										// continue
									}
																		}								}
Mark230:;
								// continue
							}
														}						}//* 
						
						_nihu9ses("A" ,ref _2bwe2jrn ,ref _3nayvi7h ,(_p9n405a5+(_4ac2pvpn - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_apig8meb+(_uqckf55l + _smlag6mh - 1)),ref _dxpq0xkr );
					}
					else
					{
						//* 
						//*              A large problem. Solve it using divide and conquer. 
						//* 
						
						_r8j651rt(ref _2jyv4h8z ,ref _q1xpyios ,ref _2bwe2jrn ,ref _9qyq7j3e ,(_plfm7z8g+(_4ac2pvpn - 1)),(_864fslqq+(_4ac2pvpn - 1)),(_dqanbbw3+(_7u55mqkq + _smlag6mh - 1)),ref _dxpq0xkr ,(_dqanbbw3+(_xdbczr8u + _smlag6mh - 1)),(_4b6rt45i+(_umlkckdg + _smlag6mh - 1)),(_dqanbbw3+(_i8976ehd + _smlag6mh - 1)),(_dqanbbw3+(_doljbvm2 + _smlag6mh - 1)),(_dqanbbw3+(_7e60fcso + _smlag6mh - 1)),(_dqanbbw3+(_7nk40y8b + _smlag6mh - 1)),(_4b6rt45i+(_8vecpt74 + _smlag6mh - 1)),(_4b6rt45i+(_0zwn6fsy + _smlag6mh - 1)),ref _dxpq0xkr ,(_4b6rt45i+(_umao48xu + _smlag6mh - 1)),(_dqanbbw3+(_gh266ol1 + _smlag6mh - 1)),(_dqanbbw3+(_3crf0qn3 + _smlag6mh - 1)),(_dqanbbw3+(_irk8i6qr + _smlag6mh - 1)),(_dqanbbw3+(_r49fp4o3 - 1)),(_4b6rt45i+(_426n50rt - 1)),ref _gro5yvfo );
						if (_gro5yvfo != (int)0)
						{
							
							return;
						}
						
						_x737e9xs = (_uqckf55l + _smlag6mh);
						_ob749t81(ref _9fy98qnj ,ref _q1xpyios ,ref _2bwe2jrn ,ref _3nayvi7h ,(_p9n405a5+(_4ac2pvpn - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_apig8meb+(_x737e9xs - 1)),ref _dxpq0xkr ,(_dqanbbw3+(_7u55mqkq + _smlag6mh - 1)),ref _dxpq0xkr ,(_dqanbbw3+(_xdbczr8u + _smlag6mh - 1)),(_4b6rt45i+(_umlkckdg + _smlag6mh - 1)),(_dqanbbw3+(_i8976ehd + _smlag6mh - 1)),(_dqanbbw3+(_doljbvm2 + _smlag6mh - 1)),(_dqanbbw3+(_7e60fcso + _smlag6mh - 1)),(_dqanbbw3+(_7nk40y8b + _smlag6mh - 1)),(_4b6rt45i+(_8vecpt74 + _smlag6mh - 1)),(_4b6rt45i+(_0zwn6fsy + _smlag6mh - 1)),ref _dxpq0xkr ,(_4b6rt45i+(_umao48xu + _smlag6mh - 1)),(_dqanbbw3+(_gh266ol1 + _smlag6mh - 1)),(_dqanbbw3+(_3crf0qn3 + _smlag6mh - 1)),(_dqanbbw3+(_irk8i6qr + _smlag6mh - 1)),(_dqanbbw3+(_r49fp4o3 - 1)),(_4b6rt45i+(_426n50rt - 1)),ref _gro5yvfo );
						if (_gro5yvfo != (int)0)
						{
							
							return;
						}
						
					}
					
					_4ac2pvpn = (_b5p6od9s + (int)1);
				}
				
Mark240:;
				// continue
			}
						}		}//* 
		//*     Apply the singular values and treat the tiny ones as zero. 
		//* 
		
		_txq1gp7u = (_tu2rb1wg * ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_ei7om7ok(ref _dxpq0xkr ,_plfm7z8g ,ref Unsafe.AsRef((int)1) ) - 1)) ));//* 
		
		{
			System.Int32 __81fgg2dlsvn2013 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2013 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2013;
			for (__81fgg2count2013 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2013 + __81fgg2step2013) / __81fgg2step2013)), _b5p6od9s = __81fgg2dlsvn2013; __81fgg2count2013 != 0; __81fgg2count2013--, _b5p6od9s += (__81fgg2step2013)) {

			{
				//* 
				//*        Some of the elements in D can be negative because 1-by-1 
				//*        subproblems were not solved explicitly. 
				//* 
				
				if (ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_b5p6od9s - 1)) ) <= _txq1gp7u)
				{
					
					_k14i9nd8("A" ,ref Unsafe.AsRef((int)1) ,ref _3nayvi7h ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_gdjumcqt) ,(_apig8meb+((_uqckf55l + _b5p6od9s) - (int)1 - 1)),ref _dxpq0xkr );
				}
				else
				{
					
					_uy2xc65y = (_uy2xc65y + (int)1);
					_j6h8q4u5("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef(*(_plfm7z8g+(_b5p6od9s - 1))) ,ref Unsafe.AsRef(_kxg5drh2) ,ref Unsafe.AsRef((int)1) ,ref _3nayvi7h ,(_apig8meb+((_uqckf55l + _b5p6od9s) - (int)1 - 1)),ref _dxpq0xkr ,ref _gro5yvfo );
				}
				
				*(_plfm7z8g+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_b5p6od9s - 1)) );
Mark250:;
				// continue
			}
						}		}//* 
		//*     Now apply back the right singular vectors. 
		//* 
		
		_9fy98qnj = (int)1;
		{
			System.Int32 __81fgg2dlsvn2014 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2014 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2014;
			for (__81fgg2count2014 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ew765vcx) - __81fgg2dlsvn2014 + __81fgg2step2014) / __81fgg2step2014)), _b5p6od9s = __81fgg2dlsvn2014; __81fgg2count2014 != 0; __81fgg2count2014--, _b5p6od9s += (__81fgg2step2014)) {

			{
				
				_4ac2pvpn = *(_4b6rt45i+(_b5p6od9s - 1));
				_smlag6mh = (_4ac2pvpn - (int)1);
				_2bwe2jrn = *(_4b6rt45i+((_stpw4s8v + _b5p6od9s) - (int)1 - 1));
				_x737e9xs = (_uqckf55l + _smlag6mh);
				if (_2bwe2jrn == (int)1)
				{
					
					_ly902k7t(ref _3nayvi7h ,(_apig8meb+(_x737e9xs - 1)),ref _dxpq0xkr ,(_p9n405a5+(_4ac2pvpn - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg );
				}
				else
				if (_2bwe2jrn <= _q1xpyios)
				{
					//* 
					//*           Since B and BX are complex, the following call to DGEMM 
					//*           is performed in two steps (real and imaginary parts). 
					//* 
					//*           CALL DGEMM( 'T', 'N', NSIZE, NRHS, NSIZE, ONE, 
					//*    $                  RWORK( VT+ST1 ), N, RWORK( BXST ), N, ZERO, 
					//*    $                  B( ST, 1 ), LDB ) 
					//* 
					
					_znpjgsef = ((_x737e9xs - _dxpq0xkr) - (int)1);
					_jcjf3d8c = (_tbvraoac - (int)1);
					{
						System.Int32 __81fgg2dlsvn2015 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2015 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2015;
						for (__81fgg2count2015 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn2015 + __81fgg2step2015) / __81fgg2step2015)), _vmeebitr = __81fgg2dlsvn2015; __81fgg2count2015 != 0; __81fgg2count2015--, _vmeebitr += (__81fgg2step2015)) {

						{
							
							_znpjgsef = (_znpjgsef + _dxpq0xkr);
							{
								System.Int32 __81fgg2dlsvn2016 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step2016 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2016;
								for (__81fgg2count2016 = System.Math.Max(0, (System.Int32)(((System.Int32)(_2bwe2jrn) - __81fgg2dlsvn2016 + __81fgg2step2016) / __81fgg2step2016)), _hgi9ttgs = __81fgg2dlsvn2016; __81fgg2count2016 != 0; __81fgg2count2016--, _hgi9ttgs += (__81fgg2step2016)) {

								{
									
									_jcjf3d8c = (_jcjf3d8c + (int)1);
									*(_dqanbbw3+(_jcjf3d8c - 1)) = ILNumerics.F2NET.Intrinsics.DBLE(*(_apig8meb+(_znpjgsef + _hgi9ttgs - 1)) );
Mark260:;
									// continue
								}
																}							}
Mark270:;
							// continue
						}
												}					}
					_5nsxi69c("T" ,"N" ,ref _2bwe2jrn ,ref _3nayvi7h ,ref _2bwe2jrn ,ref Unsafe.AsRef(_kxg5drh2) ,(_dqanbbw3+(_xdbczr8u + _smlag6mh - 1)),ref _dxpq0xkr ,(_dqanbbw3+(_tbvraoac - 1)),ref _2bwe2jrn ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+(_oetrgbzj - 1)),ref _2bwe2jrn );
					_znpjgsef = ((_x737e9xs - _dxpq0xkr) - (int)1);
					_llqvt7c6 = (_tbvraoac - (int)1);
					{
						System.Int32 __81fgg2dlsvn2017 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2017 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2017;
						for (__81fgg2count2017 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn2017 + __81fgg2step2017) / __81fgg2step2017)), _vmeebitr = __81fgg2dlsvn2017; __81fgg2count2017 != 0; __81fgg2count2017--, _vmeebitr += (__81fgg2step2017)) {

						{
							
							_znpjgsef = (_znpjgsef + _dxpq0xkr);
							{
								System.Int32 __81fgg2dlsvn2018 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step2018 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2018;
								for (__81fgg2count2018 = System.Math.Max(0, (System.Int32)(((System.Int32)(_2bwe2jrn) - __81fgg2dlsvn2018 + __81fgg2step2018) / __81fgg2step2018)), _hgi9ttgs = __81fgg2dlsvn2018; __81fgg2count2018 != 0; __81fgg2count2018--, _hgi9ttgs += (__81fgg2step2018)) {

								{
									
									_llqvt7c6 = (_llqvt7c6 + (int)1);
									*(_dqanbbw3+(_llqvt7c6 - 1)) = ILNumerics.F2NET.Intrinsics.DIMAG(*(_apig8meb+(_znpjgsef + _hgi9ttgs - 1)) );
Mark280:;
									// continue
								}
																}							}
Mark290:;
							// continue
						}
												}					}
					_5nsxi69c("T" ,"N" ,ref _2bwe2jrn ,ref _3nayvi7h ,ref _2bwe2jrn ,ref Unsafe.AsRef(_kxg5drh2) ,(_dqanbbw3+(_xdbczr8u + _smlag6mh - 1)),ref _dxpq0xkr ,(_dqanbbw3+(_tbvraoac - 1)),ref _2bwe2jrn ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+(_8haoin41 - 1)),ref _2bwe2jrn );
					_jcjf3d8c = (_oetrgbzj - (int)1);
					_llqvt7c6 = (_8haoin41 - (int)1);
					{
						System.Int32 __81fgg2dlsvn2019 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2019 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2019;
						for (__81fgg2count2019 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn2019 + __81fgg2step2019) / __81fgg2step2019)), _vmeebitr = __81fgg2dlsvn2019; __81fgg2count2019 != 0; __81fgg2count2019--, _vmeebitr += (__81fgg2step2019)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn2020 = (System.Int32)(_4ac2pvpn);
								const System.Int32 __81fgg2step2020 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2020;
								for (__81fgg2count2020 = System.Math.Max(0, (System.Int32)(((System.Int32)((_4ac2pvpn + _2bwe2jrn) - (int)1) - __81fgg2dlsvn2020 + __81fgg2step2020) / __81fgg2step2020)), _hgi9ttgs = __81fgg2dlsvn2020; __81fgg2count2020 != 0; __81fgg2count2020--, _hgi9ttgs += (__81fgg2step2020)) {

								{
									
									_jcjf3d8c = (_jcjf3d8c + (int)1);
									_llqvt7c6 = (_llqvt7c6 + (int)1);
									*(_p9n405a5+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_ly9opahg)) = ILNumerics.F2NET.Intrinsics.DCMPLX(*(_dqanbbw3+(_jcjf3d8c - 1)) ,*(_dqanbbw3+(_llqvt7c6 - 1)) );
Mark300:;
									// continue
								}
																}							}
Mark310:;
							// continue
						}
												}					}
				}
				else
				{
					
					_ob749t81(ref _9fy98qnj ,ref _q1xpyios ,ref _2bwe2jrn ,ref _3nayvi7h ,(_apig8meb+(_x737e9xs - 1)),ref _dxpq0xkr ,(_p9n405a5+(_4ac2pvpn - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_dqanbbw3+(_7u55mqkq + _smlag6mh - 1)),ref _dxpq0xkr ,(_dqanbbw3+(_xdbczr8u + _smlag6mh - 1)),(_4b6rt45i+(_umlkckdg + _smlag6mh - 1)),(_dqanbbw3+(_i8976ehd + _smlag6mh - 1)),(_dqanbbw3+(_doljbvm2 + _smlag6mh - 1)),(_dqanbbw3+(_7e60fcso + _smlag6mh - 1)),(_dqanbbw3+(_7nk40y8b + _smlag6mh - 1)),(_4b6rt45i+(_8vecpt74 + _smlag6mh - 1)),(_4b6rt45i+(_0zwn6fsy + _smlag6mh - 1)),ref _dxpq0xkr ,(_4b6rt45i+(_umao48xu + _smlag6mh - 1)),(_dqanbbw3+(_gh266ol1 + _smlag6mh - 1)),(_dqanbbw3+(_3crf0qn3 + _smlag6mh - 1)),(_dqanbbw3+(_irk8i6qr + _smlag6mh - 1)),(_dqanbbw3+(_r49fp4o3 - 1)),(_4b6rt45i+(_426n50rt - 1)),ref _gro5yvfo );
					if (_gro5yvfo != (int)0)
					{
						
						return;
					}
					
				}
				
Mark320:;
				// continue
			}
						}		}//* 
		//*     Unscale and sort the singular values. 
		//* 
		
		_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef(_kxg5drh2) ,ref _wby36o6h ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1) ,_plfm7z8g ,ref _dxpq0xkr ,ref _gro5yvfo );
		_agod5jth("D" ,ref _dxpq0xkr ,_plfm7z8g ,ref _gro5yvfo );
		_j6h8q4u5("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _wby36o6h ,ref Unsafe.AsRef(_kxg5drh2) ,ref _dxpq0xkr ,ref _3nayvi7h ,_p9n405a5 ,ref _ly9opahg ,ref _gro5yvfo );//* 
		
		return;//* 
		//*     End of ZLALSD 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
