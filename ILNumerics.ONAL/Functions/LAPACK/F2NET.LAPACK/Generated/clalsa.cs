
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
//*> \brief \b CLALSA computes the SVD of the coefficient matrix in compact form. Used by sgelsd. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CLALSA + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/clalsa.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/clalsa.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/clalsa.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CLALSA( ICOMPQ, SMLSIZ, N, NRHS, B, LDB, BX, LDBX, U, 
//*                          LDU, VT, K, DIFL, DIFR, Z, POLES, GIVPTR, 
//*                          GIVCOL, LDGCOL, PERM, GIVNUM, C, S, RWORK, 
//*                          IWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            ICOMPQ, INFO, LDB, LDBX, LDGCOL, LDU, N, NRHS, 
//*      $                   SMLSIZ 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            GIVCOL( LDGCOL, * ), GIVPTR( * ), IWORK( * ), 
//*      $                   K( * ), PERM( LDGCOL, * ) 
//*       REAL               C( * ), DIFL( LDU, * ), DIFR( LDU, * ), 
//*      $                   GIVNUM( LDU, * ), POLES( LDU, * ), RWORK( * ), 
//*      $                   S( * ), U( LDU, * ), VT( LDU, * ), Z( LDU, * ) 
//*       COMPLEX            B( LDB, * ), BX( LDBX, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CLALSA is an itermediate step in solving the least squares problem 
//*> by computing the SVD of the coefficient matrix in compact form (The 
//*> singular vectors are computed as products of simple orthorgonal 
//*> matrices.). 
//*> 
//*> If ICOMPQ = 0, CLALSA applies the inverse of the left singular vector 
//*> matrix of an upper bidiagonal matrix to the right hand side; and if 
//*> ICOMPQ = 1, CLALSA applies the right singular vector matrix to the 
//*> right hand side. The singular vector matrices were generated in 
//*> compact form by CLALSA. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] ICOMPQ 
//*> \verbatim 
//*>          ICOMPQ is INTEGER 
//*>         Specifies whether the left or the right singular vector 
//*>         matrix is involved. 
//*>         = 0: Left singular vector matrix 
//*>         = 1: Right singular vector matrix 
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
//*>         The row and column dimensions of the upper bidiagonal matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] NRHS 
//*> \verbatim 
//*>          NRHS is INTEGER 
//*>         The number of columns of B and BX. NRHS must be at least 1. 
//*> \endverbatim 
//*> 
//*> \param[in,out] B 
//*> \verbatim 
//*>          B is COMPLEX array, dimension ( LDB, NRHS ) 
//*>         On input, B contains the right hand sides of the least 
//*>         squares problem in rows 1 through M. 
//*>         On output, B contains the solution X in rows 1 through N. 
//*> \endverbatim 
//*> 
//*> \param[in] LDB 
//*> \verbatim 
//*>          LDB is INTEGER 
//*>         The leading dimension of B in the calling subprogram. 
//*>         LDB must be at least max(1,MAX( M, N ) ). 
//*> \endverbatim 
//*> 
//*> \param[out] BX 
//*> \verbatim 
//*>          BX is COMPLEX array, dimension ( LDBX, NRHS ) 
//*>         On exit, the result of applying the left or right singular 
//*>         vector matrix to B. 
//*> \endverbatim 
//*> 
//*> \param[in] LDBX 
//*> \verbatim 
//*>          LDBX is INTEGER 
//*>         The leading dimension of BX. 
//*> \endverbatim 
//*> 
//*> \param[in] U 
//*> \verbatim 
//*>          U is REAL array, dimension ( LDU, SMLSIZ ). 
//*>         On entry, U contains the left singular vector matrices of all 
//*>         subproblems at the bottom level. 
//*> \endverbatim 
//*> 
//*> \param[in] LDU 
//*> \verbatim 
//*>          LDU is INTEGER, LDU = > N. 
//*>         The leading dimension of arrays U, VT, DIFL, DIFR, 
//*>         POLES, GIVNUM, and Z. 
//*> \endverbatim 
//*> 
//*> \param[in] VT 
//*> \verbatim 
//*>          VT is REAL array, dimension ( LDU, SMLSIZ+1 ). 
//*>         On entry, VT**H contains the right singular vector matrices of 
//*>         all subproblems at the bottom level. 
//*> \endverbatim 
//*> 
//*> \param[in] K 
//*> \verbatim 
//*>          K is INTEGER array, dimension ( N ). 
//*> \endverbatim 
//*> 
//*> \param[in] DIFL 
//*> \verbatim 
//*>          DIFL is REAL array, dimension ( LDU, NLVL ). 
//*>         where NLVL = INT(log_2 (N/(SMLSIZ+1))) + 1. 
//*> \endverbatim 
//*> 
//*> \param[in] DIFR 
//*> \verbatim 
//*>          DIFR is REAL array, dimension ( LDU, 2 * NLVL ). 
//*>         On entry, DIFL(*, I) and DIFR(*, 2 * I -1) record 
//*>         distances between singular values on the I-th level and 
//*>         singular values on the (I -1)-th level, and DIFR(*, 2 * I) 
//*>         record the normalizing factors of the right singular vectors 
//*>         matrices of subproblems on I-th level. 
//*> \endverbatim 
//*> 
//*> \param[in] Z 
//*> \verbatim 
//*>          Z is REAL array, dimension ( LDU, NLVL ). 
//*>         On entry, Z(1, I) contains the components of the deflation- 
//*>         adjusted updating row vector for subproblems on the I-th 
//*>         level. 
//*> \endverbatim 
//*> 
//*> \param[in] POLES 
//*> \verbatim 
//*>          POLES is REAL array, dimension ( LDU, 2 * NLVL ). 
//*>         On entry, POLES(*, 2 * I -1: 2 * I) contains the new and old 
//*>         singular values involved in the secular equations on the I-th 
//*>         level. 
//*> \endverbatim 
//*> 
//*> \param[in] GIVPTR 
//*> \verbatim 
//*>          GIVPTR is INTEGER array, dimension ( N ). 
//*>         On entry, GIVPTR( I ) records the number of Givens 
//*>         rotations performed on the I-th problem on the computation 
//*>         tree. 
//*> \endverbatim 
//*> 
//*> \param[in] GIVCOL 
//*> \verbatim 
//*>          GIVCOL is INTEGER array, dimension ( LDGCOL, 2 * NLVL ). 
//*>         On entry, for each I, GIVCOL(*, 2 * I - 1: 2 * I) records the 
//*>         locations of Givens rotations performed on the I-th level on 
//*>         the computation tree. 
//*> \endverbatim 
//*> 
//*> \param[in] LDGCOL 
//*> \verbatim 
//*>          LDGCOL is INTEGER, LDGCOL = > N. 
//*>         The leading dimension of arrays GIVCOL and PERM. 
//*> \endverbatim 
//*> 
//*> \param[in] PERM 
//*> \verbatim 
//*>          PERM is INTEGER array, dimension ( LDGCOL, NLVL ). 
//*>         On entry, PERM(*, I) records permutations done on the I-th 
//*>         level of the computation tree. 
//*> \endverbatim 
//*> 
//*> \param[in] GIVNUM 
//*> \verbatim 
//*>          GIVNUM is REAL array, dimension ( LDU, 2 * NLVL ). 
//*>         On entry, GIVNUM(*, 2 *I -1 : 2 * I) records the C- and S- 
//*>         values of Givens rotations performed on the I-th level on the 
//*>         computation tree. 
//*> \endverbatim 
//*> 
//*> \param[in] C 
//*> \verbatim 
//*>          C is REAL array, dimension ( N ). 
//*>         On entry, if the I-th subproblem is not square, 
//*>         C( I ) contains the C-value of a Givens rotation related to 
//*>         the right null space of the I-th subproblem. 
//*> \endverbatim 
//*> 
//*> \param[in] S 
//*> \verbatim 
//*>          S is REAL array, dimension ( N ). 
//*>         On entry, if the I-th subproblem is not square, 
//*>         S( I ) contains the S-value of a Givens rotation related to 
//*>         the right null space of the I-th subproblem. 
//*> \endverbatim 
//*> 
//*> \param[out] RWORK 
//*> \verbatim 
//*>          RWORK is REAL array, dimension at least 
//*>         MAX( (SMLSZ+1)*NRHS*3, N*(1+NRHS) + 2*NRHS ). 
//*> \endverbatim 
//*> 
//*> \param[out] IWORK 
//*> \verbatim 
//*>          IWORK is INTEGER array, dimension (3*N) 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit. 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value. 
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
//*> \ingroup complexOTHERcomputational 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>     Ming Gu and Ren-Cang Li, Computer Science Division, University of 
//*>       California at Berkeley, USA \n 
//*>     Osni Marques, LBNL/NERSC, USA \n 
//* 
//*  ===================================================================== 

	 
	public static void _tyrx8366(ref Int32 _y1be9goe, ref Int32 _q1xpyios, ref Int32 _dxpq0xkr, ref Int32 _3nayvi7h, fcomplex* _p9n405a5, ref Int32 _ly9opahg, fcomplex* _uqckf55l, ref Int32 _hde8nv3t, Single* _7u55mqkq, ref Int32 _u6e6d39b, Single* _xdbczr8u, Int32* _umlkckdg, Single* _i8976ehd, Single* _doljbvm2, Single* _7e60fcso, Single* _7nk40y8b, Int32* _8vecpt74, Int32* _0zwn6fsy, ref Int32 _uhi0ls8i, Int32* _umao48xu, Single* _gh266ol1, Single* _3crf0qn3, Single* _irk8i6qr, Single* _dqanbbw3, Int32* _4b6rt45i, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
Int32 _b5p6od9s =  default;
Int32 _egqdmelt =  default;
Int32 _8jzcrkri =  default;
Int32 _3dimb00t =  default;
Int32 _q8fqp221 =  default;
Int32 _znpjgsef =  default;
Int32 _vmeebitr =  default;
Int32 _llqvt7c6 =  default;
Int32 _jcjf3d8c =  default;
Int32 _hgi9ttgs =  default;
Int32 _es1scagl =  default;
Int32 _jnnmt81a =  default;
Int32 _u0afxfs0 =  default;
Int32 _4ekj6112 =  default;
Int32 _rwm6akyl =  default;
Int32 _v4ofzyw5 =  default;
Int32 _b53e0l58 =  default;
Int32 _9bq9s7q7 =  default;
Int32 _zx57w4aj =  default;
Int32 _cyu21nam =  default;
Int32 _qwh8ts9f =  default;
Int32 _0n683y3x =  default;
Int32 _oqpc3yjg =  default;
Int32 _5tcb1chw =  default;
Int32 _bds60snh =  default;
Int32 _9qyq7j3e =  default;
string fLanavab = default;
#endregion  variable declarations

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
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input parameters. 
		//* 
		
		_gro5yvfo = (int)0;//* 
		
		if ((_y1be9goe < (int)0) | (_y1be9goe > (int)1))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_q1xpyios < (int)3)
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_dxpq0xkr < _q1xpyios)
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if (_3nayvi7h < (int)1)
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if (_ly9opahg < _dxpq0xkr)
		{
			
			_gro5yvfo = (int)-6;
		}
		else
		if (_hde8nv3t < _dxpq0xkr)
		{
			
			_gro5yvfo = (int)-8;
		}
		else
		if (_u6e6d39b < _dxpq0xkr)
		{
			
			_gro5yvfo = (int)-10;
		}
		else
		if (_uhi0ls8i < _dxpq0xkr)
		{
			
			_gro5yvfo = (int)-19;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("CLALSA" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Book-keeping and  setting up the computation tree. 
		//* 
		
		_q8fqp221 = (int)1;
		_b53e0l58 = (_q8fqp221 + _dxpq0xkr);
		_9bq9s7q7 = (_b53e0l58 + _dxpq0xkr);//* 
		
		_k6l39brz(ref _dxpq0xkr ,ref _0n683y3x ,ref _rwm6akyl ,(_4b6rt45i+(_q8fqp221 - 1)),(_4b6rt45i+(_b53e0l58 - 1)),(_4b6rt45i+(_9bq9s7q7 - 1)),ref _q1xpyios );//* 
		//*     The following code applies back the left singular vector factors. 
		//*     For applying back the right singular vector factors, go to 170. 
		//* 
		
		if (_y1be9goe == (int)1)
		{
			goto Mark170;
		}
		//* 
		//*     The nodes on the bottom level of the tree were solved 
		//*     by SLASDQ. The corresponding left and right singular vector 
		//*     matrices are in explicit form. First apply back the left 
		//*     singular vector matrices. 
		//* 
		
		_v4ofzyw5 = ((_rwm6akyl + (int)1) / (int)2);
		{
			System.Int32 __81fgg2dlsvn1938 = (System.Int32)(_v4ofzyw5);
			const System.Int32 __81fgg2step1938 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1938;
			for (__81fgg2count1938 = System.Math.Max(0, (System.Int32)(((System.Int32)(_rwm6akyl) - __81fgg2dlsvn1938 + __81fgg2step1938) / __81fgg2step1938)), _b5p6od9s = __81fgg2dlsvn1938; __81fgg2count1938 != 0; __81fgg2count1938--, _b5p6od9s += (__81fgg2step1938)) {

			{
				//* 
				//*        IC : center row of each node 
				//*        NL : number of rows of left  subproblem 
				//*        NR : number of rows of right subproblem 
				//*        NLF: starting row of the left   subproblem 
				//*        NRF: starting row of the right  subproblem 
				//* 
				
				_egqdmelt = (_b5p6od9s - (int)1);
				_8jzcrkri = *(_4b6rt45i+(_q8fqp221 + _egqdmelt - 1));
				_zx57w4aj = *(_4b6rt45i+(_b53e0l58 + _egqdmelt - 1));
				_oqpc3yjg = *(_4b6rt45i+(_9bq9s7q7 + _egqdmelt - 1));
				_cyu21nam = (_8jzcrkri - _zx57w4aj);
				_5tcb1chw = (_8jzcrkri + (int)1);//* 
				//*        Since B and BX are complex, the following call to SGEMM 
				//*        is performed in two steps (real and imaginary parts). 
				//* 
				//*        CALL SGEMM( 'T', 'N', NL, NRHS, NL, ONE, U( NLF, 1 ), LDU, 
				//*     $               B( NLF, 1 ), LDB, ZERO, BX( NLF, 1 ), LDBX ) 
				//* 
				
				_znpjgsef = ((_zx57w4aj * _3nayvi7h) * (int)2);
				{
					System.Int32 __81fgg2dlsvn1939 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1939 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1939;
					for (__81fgg2count1939 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn1939 + __81fgg2step1939) / __81fgg2step1939)), _vmeebitr = __81fgg2dlsvn1939; __81fgg2count1939 != 0; __81fgg2count1939--, _vmeebitr += (__81fgg2step1939)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn1940 = (System.Int32)(_cyu21nam);
							const System.Int32 __81fgg2step1940 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1940;
							for (__81fgg2count1940 = System.Math.Max(0, (System.Int32)(((System.Int32)((_cyu21nam + _zx57w4aj) - (int)1) - __81fgg2dlsvn1940 + __81fgg2step1940) / __81fgg2step1940)), _hgi9ttgs = __81fgg2dlsvn1940; __81fgg2count1940 != 0; __81fgg2count1940--, _hgi9ttgs += (__81fgg2step1940)) {

							{
								
								_znpjgsef = (_znpjgsef + (int)1);
								*(_dqanbbw3+(_znpjgsef - 1)) = ILNumerics.F2NET.Intrinsics.REAL(*(_p9n405a5+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_ly9opahg)) );
Mark10:;
								// continue
							}
														}						}
Mark20:;
						// continue
					}
										}				}
				_b8wa9454("T" ,"N" ,ref _zx57w4aj ,ref _3nayvi7h ,ref _zx57w4aj ,ref Unsafe.AsRef(_kxg5drh2) ,(_7u55mqkq+(_cyu21nam - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_dqanbbw3+((int)1 + ((_zx57w4aj * _3nayvi7h) * (int)2) - 1)),ref _zx57w4aj ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+((int)1 - 1)),ref _zx57w4aj );
				_znpjgsef = ((_zx57w4aj * _3nayvi7h) * (int)2);
				{
					System.Int32 __81fgg2dlsvn1941 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1941 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1941;
					for (__81fgg2count1941 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn1941 + __81fgg2step1941) / __81fgg2step1941)), _vmeebitr = __81fgg2dlsvn1941; __81fgg2count1941 != 0; __81fgg2count1941--, _vmeebitr += (__81fgg2step1941)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn1942 = (System.Int32)(_cyu21nam);
							const System.Int32 __81fgg2step1942 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1942;
							for (__81fgg2count1942 = System.Math.Max(0, (System.Int32)(((System.Int32)((_cyu21nam + _zx57w4aj) - (int)1) - __81fgg2dlsvn1942 + __81fgg2step1942) / __81fgg2step1942)), _hgi9ttgs = __81fgg2dlsvn1942; __81fgg2count1942 != 0; __81fgg2count1942--, _hgi9ttgs += (__81fgg2step1942)) {

							{
								
								_znpjgsef = (_znpjgsef + (int)1);
								*(_dqanbbw3+(_znpjgsef - 1)) = ILNumerics.F2NET.Intrinsics.AIMAG(*(_p9n405a5+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_ly9opahg)) );
Mark30:;
								// continue
							}
														}						}
Mark40:;
						// continue
					}
										}				}
				_b8wa9454("T" ,"N" ,ref _zx57w4aj ,ref _3nayvi7h ,ref _zx57w4aj ,ref Unsafe.AsRef(_kxg5drh2) ,(_7u55mqkq+(_cyu21nam - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_dqanbbw3+((int)1 + ((_zx57w4aj * _3nayvi7h) * (int)2) - 1)),ref _zx57w4aj ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+((int)1 + (_zx57w4aj * _3nayvi7h) - 1)),ref _zx57w4aj );
				_jcjf3d8c = (int)0;
				_llqvt7c6 = (_zx57w4aj * _3nayvi7h);
				{
					System.Int32 __81fgg2dlsvn1943 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1943 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1943;
					for (__81fgg2count1943 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn1943 + __81fgg2step1943) / __81fgg2step1943)), _vmeebitr = __81fgg2dlsvn1943; __81fgg2count1943 != 0; __81fgg2count1943--, _vmeebitr += (__81fgg2step1943)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn1944 = (System.Int32)(_cyu21nam);
							const System.Int32 __81fgg2step1944 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1944;
							for (__81fgg2count1944 = System.Math.Max(0, (System.Int32)(((System.Int32)((_cyu21nam + _zx57w4aj) - (int)1) - __81fgg2dlsvn1944 + __81fgg2step1944) / __81fgg2step1944)), _hgi9ttgs = __81fgg2dlsvn1944; __81fgg2count1944 != 0; __81fgg2count1944--, _hgi9ttgs += (__81fgg2step1944)) {

							{
								
								_jcjf3d8c = (_jcjf3d8c + (int)1);
								_llqvt7c6 = (_llqvt7c6 + (int)1);
								*(_uqckf55l+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_hde8nv3t)) = ILNumerics.F2NET.Intrinsics.CMPLX(*(_dqanbbw3+(_jcjf3d8c - 1)) ,*(_dqanbbw3+(_llqvt7c6 - 1)) );
Mark50:;
								// continue
							}
														}						}
Mark60:;
						// continue
					}
										}				}//* 
				//*        Since B and BX are complex, the following call to SGEMM 
				//*        is performed in two steps (real and imaginary parts). 
				//* 
				//*        CALL SGEMM( 'T', 'N', NR, NRHS, NR, ONE, U( NRF, 1 ), LDU, 
				//*    $               B( NRF, 1 ), LDB, ZERO, BX( NRF, 1 ), LDBX ) 
				//* 
				
				_znpjgsef = ((_oqpc3yjg * _3nayvi7h) * (int)2);
				{
					System.Int32 __81fgg2dlsvn1945 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1945 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1945;
					for (__81fgg2count1945 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn1945 + __81fgg2step1945) / __81fgg2step1945)), _vmeebitr = __81fgg2dlsvn1945; __81fgg2count1945 != 0; __81fgg2count1945--, _vmeebitr += (__81fgg2step1945)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn1946 = (System.Int32)(_5tcb1chw);
							const System.Int32 __81fgg2step1946 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1946;
							for (__81fgg2count1946 = System.Math.Max(0, (System.Int32)(((System.Int32)((_5tcb1chw + _oqpc3yjg) - (int)1) - __81fgg2dlsvn1946 + __81fgg2step1946) / __81fgg2step1946)), _hgi9ttgs = __81fgg2dlsvn1946; __81fgg2count1946 != 0; __81fgg2count1946--, _hgi9ttgs += (__81fgg2step1946)) {

							{
								
								_znpjgsef = (_znpjgsef + (int)1);
								*(_dqanbbw3+(_znpjgsef - 1)) = ILNumerics.F2NET.Intrinsics.REAL(*(_p9n405a5+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_ly9opahg)) );
Mark70:;
								// continue
							}
														}						}
Mark80:;
						// continue
					}
										}				}
				_b8wa9454("T" ,"N" ,ref _oqpc3yjg ,ref _3nayvi7h ,ref _oqpc3yjg ,ref Unsafe.AsRef(_kxg5drh2) ,(_7u55mqkq+(_5tcb1chw - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_dqanbbw3+((int)1 + ((_oqpc3yjg * _3nayvi7h) * (int)2) - 1)),ref _oqpc3yjg ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+((int)1 - 1)),ref _oqpc3yjg );
				_znpjgsef = ((_oqpc3yjg * _3nayvi7h) * (int)2);
				{
					System.Int32 __81fgg2dlsvn1947 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1947 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1947;
					for (__81fgg2count1947 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn1947 + __81fgg2step1947) / __81fgg2step1947)), _vmeebitr = __81fgg2dlsvn1947; __81fgg2count1947 != 0; __81fgg2count1947--, _vmeebitr += (__81fgg2step1947)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn1948 = (System.Int32)(_5tcb1chw);
							const System.Int32 __81fgg2step1948 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1948;
							for (__81fgg2count1948 = System.Math.Max(0, (System.Int32)(((System.Int32)((_5tcb1chw + _oqpc3yjg) - (int)1) - __81fgg2dlsvn1948 + __81fgg2step1948) / __81fgg2step1948)), _hgi9ttgs = __81fgg2dlsvn1948; __81fgg2count1948 != 0; __81fgg2count1948--, _hgi9ttgs += (__81fgg2step1948)) {

							{
								
								_znpjgsef = (_znpjgsef + (int)1);
								*(_dqanbbw3+(_znpjgsef - 1)) = ILNumerics.F2NET.Intrinsics.AIMAG(*(_p9n405a5+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_ly9opahg)) );
Mark90:;
								// continue
							}
														}						}
Mark100:;
						// continue
					}
										}				}
				_b8wa9454("T" ,"N" ,ref _oqpc3yjg ,ref _3nayvi7h ,ref _oqpc3yjg ,ref Unsafe.AsRef(_kxg5drh2) ,(_7u55mqkq+(_5tcb1chw - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_dqanbbw3+((int)1 + ((_oqpc3yjg * _3nayvi7h) * (int)2) - 1)),ref _oqpc3yjg ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+((int)1 + (_oqpc3yjg * _3nayvi7h) - 1)),ref _oqpc3yjg );
				_jcjf3d8c = (int)0;
				_llqvt7c6 = (_oqpc3yjg * _3nayvi7h);
				{
					System.Int32 __81fgg2dlsvn1949 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1949 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1949;
					for (__81fgg2count1949 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn1949 + __81fgg2step1949) / __81fgg2step1949)), _vmeebitr = __81fgg2dlsvn1949; __81fgg2count1949 != 0; __81fgg2count1949--, _vmeebitr += (__81fgg2step1949)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn1950 = (System.Int32)(_5tcb1chw);
							const System.Int32 __81fgg2step1950 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1950;
							for (__81fgg2count1950 = System.Math.Max(0, (System.Int32)(((System.Int32)((_5tcb1chw + _oqpc3yjg) - (int)1) - __81fgg2dlsvn1950 + __81fgg2step1950) / __81fgg2step1950)), _hgi9ttgs = __81fgg2dlsvn1950; __81fgg2count1950 != 0; __81fgg2count1950--, _hgi9ttgs += (__81fgg2step1950)) {

							{
								
								_jcjf3d8c = (_jcjf3d8c + (int)1);
								_llqvt7c6 = (_llqvt7c6 + (int)1);
								*(_uqckf55l+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_hde8nv3t)) = ILNumerics.F2NET.Intrinsics.CMPLX(*(_dqanbbw3+(_jcjf3d8c - 1)) ,*(_dqanbbw3+(_llqvt7c6 - 1)) );
Mark110:;
								// continue
							}
														}						}
Mark120:;
						// continue
					}
										}				}//* 
				
Mark130:;
				// continue
			}
						}		}//* 
		//*     Next copy the rows of B that correspond to unchanged rows 
		//*     in the bidiagonal matrix to BX. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn1951 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1951 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1951;
			for (__81fgg2count1951 = System.Math.Max(0, (System.Int32)(((System.Int32)(_rwm6akyl) - __81fgg2dlsvn1951 + __81fgg2step1951) / __81fgg2step1951)), _b5p6od9s = __81fgg2dlsvn1951; __81fgg2count1951 != 0; __81fgg2count1951--, _b5p6od9s += (__81fgg2step1951)) {

			{
				
				_8jzcrkri = *(_4b6rt45i+((_q8fqp221 + _b5p6od9s) - (int)1 - 1));
				_33e0jk6i(ref _3nayvi7h ,(_p9n405a5+(_8jzcrkri - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_uqckf55l+(_8jzcrkri - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t );
Mark140:;
				// continue
			}
						}		}//* 
		//*     Finally go through the left singular vector matrices of all 
		//*     the other subproblems bottom-up on the tree. 
		//* 
		
		_znpjgsef = __POW((int)2, _0n683y3x);
		_9qyq7j3e = (int)0;//* 
		
		{
			System.Int32 __81fgg2dlsvn1952 = (System.Int32)(_0n683y3x);
			System.Int32 __81fgg2step1952 = (System.Int32)((int)-1);
			System.Int32 __81fgg2count1952;
			for (__81fgg2count1952 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1952 + __81fgg2step1952) / __81fgg2step1952)), _u0afxfs0 = __81fgg2dlsvn1952; __81fgg2count1952 != 0; __81fgg2count1952--, _u0afxfs0 += (__81fgg2step1952)) {

			{
				
				_4ekj6112 = (((int)2 * _u0afxfs0) - (int)1);//* 
				//*        find the first node LF and last node LL on 
				//*        the current level LVL 
				//* 
				
				if (_u0afxfs0 == (int)1)
				{
					
					_es1scagl = (int)1;
					_jnnmt81a = (int)1;
				}
				else
				{
					
					_es1scagl = __POW((int)2, (_u0afxfs0 - (int)1));
					_jnnmt81a = (((int)2 * _es1scagl) - (int)1);
				}
				
				{
					System.Int32 __81fgg2dlsvn1953 = (System.Int32)(_es1scagl);
					const System.Int32 __81fgg2step1953 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1953;
					for (__81fgg2count1953 = System.Math.Max(0, (System.Int32)(((System.Int32)(_jnnmt81a) - __81fgg2dlsvn1953 + __81fgg2step1953) / __81fgg2step1953)), _b5p6od9s = __81fgg2dlsvn1953; __81fgg2count1953 != 0; __81fgg2count1953--, _b5p6od9s += (__81fgg2step1953)) {

					{
						
						_3dimb00t = (_b5p6od9s - (int)1);
						_8jzcrkri = *(_4b6rt45i+(_q8fqp221 + _3dimb00t - 1));
						_zx57w4aj = *(_4b6rt45i+(_b53e0l58 + _3dimb00t - 1));
						_oqpc3yjg = *(_4b6rt45i+(_9bq9s7q7 + _3dimb00t - 1));
						_cyu21nam = (_8jzcrkri - _zx57w4aj);
						_5tcb1chw = (_8jzcrkri + (int)1);
						_znpjgsef = (_znpjgsef - (int)1);
						_ubpr5m2k(ref _y1be9goe ,ref _zx57w4aj ,ref _oqpc3yjg ,ref _9qyq7j3e ,ref _3nayvi7h ,(_uqckf55l+(_cyu21nam - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t ,(_p9n405a5+(_cyu21nam - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_umao48xu+(_cyu21nam - 1) + (_u0afxfs0 - 1) * 1 * (_uhi0ls8i)),ref Unsafe.AsRef(*(_8vecpt74+(_znpjgsef - 1))) ,(_0zwn6fsy+(_cyu21nam - 1) + (_4ekj6112 - 1) * 1 * (_uhi0ls8i)),ref _uhi0ls8i ,(_gh266ol1+(_cyu21nam - 1) + (_4ekj6112 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_7nk40y8b+(_cyu21nam - 1) + (_4ekj6112 - 1) * 1 * (_u6e6d39b)),(_i8976ehd+(_cyu21nam - 1) + (_u0afxfs0 - 1) * 1 * (_u6e6d39b)),(_doljbvm2+(_cyu21nam - 1) + (_4ekj6112 - 1) * 1 * (_u6e6d39b)),(_7e60fcso+(_cyu21nam - 1) + (_u0afxfs0 - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef(*(_umlkckdg+(_znpjgsef - 1))) ,ref Unsafe.AsRef(*(_3crf0qn3+(_znpjgsef - 1))) ,ref Unsafe.AsRef(*(_irk8i6qr+(_znpjgsef - 1))) ,_dqanbbw3 ,ref _gro5yvfo );
Mark150:;
						// continue
					}
										}				}
Mark160:;
				// continue
			}
						}		}goto Mark330;//* 
		//*     ICOMPQ = 1: applying back the right singular vector factors. 
		//* 
		
Mark170:;
		// continue//* 
		//*     First now go through the right singular vector matrices of all 
		//*     the tree nodes top-down. 
		//* 
		
		_znpjgsef = (int)0;
		{
			System.Int32 __81fgg2dlsvn1954 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1954 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1954;
			for (__81fgg2count1954 = System.Math.Max(0, (System.Int32)(((System.Int32)(_0n683y3x) - __81fgg2dlsvn1954 + __81fgg2step1954) / __81fgg2step1954)), _u0afxfs0 = __81fgg2dlsvn1954; __81fgg2count1954 != 0; __81fgg2count1954--, _u0afxfs0 += (__81fgg2step1954)) {

			{
				
				_4ekj6112 = (((int)2 * _u0afxfs0) - (int)1);//* 
				//*        Find the first node LF and last node LL on 
				//*        the current level LVL. 
				//* 
				
				if (_u0afxfs0 == (int)1)
				{
					
					_es1scagl = (int)1;
					_jnnmt81a = (int)1;
				}
				else
				{
					
					_es1scagl = __POW((int)2, (_u0afxfs0 - (int)1));
					_jnnmt81a = (((int)2 * _es1scagl) - (int)1);
				}
				
				{
					System.Int32 __81fgg2dlsvn1955 = (System.Int32)(_jnnmt81a);
					System.Int32 __81fgg2step1955 = (System.Int32)((int)-1);
					System.Int32 __81fgg2count1955;
					for (__81fgg2count1955 = System.Math.Max(0, (System.Int32)(((System.Int32)(_es1scagl) - __81fgg2dlsvn1955 + __81fgg2step1955) / __81fgg2step1955)), _b5p6od9s = __81fgg2dlsvn1955; __81fgg2count1955 != 0; __81fgg2count1955--, _b5p6od9s += (__81fgg2step1955)) {

					{
						
						_3dimb00t = (_b5p6od9s - (int)1);
						_8jzcrkri = *(_4b6rt45i+(_q8fqp221 + _3dimb00t - 1));
						_zx57w4aj = *(_4b6rt45i+(_b53e0l58 + _3dimb00t - 1));
						_oqpc3yjg = *(_4b6rt45i+(_9bq9s7q7 + _3dimb00t - 1));
						_cyu21nam = (_8jzcrkri - _zx57w4aj);
						_5tcb1chw = (_8jzcrkri + (int)1);
						if (_b5p6od9s == _jnnmt81a)
						{
							
							_9qyq7j3e = (int)0;
						}
						else
						{
							
							_9qyq7j3e = (int)1;
						}
						
						_znpjgsef = (_znpjgsef + (int)1);
						_ubpr5m2k(ref _y1be9goe ,ref _zx57w4aj ,ref _oqpc3yjg ,ref _9qyq7j3e ,ref _3nayvi7h ,(_p9n405a5+(_cyu21nam - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_uqckf55l+(_cyu21nam - 1) + ((int)1 - 1) * 1 * (_hde8nv3t)),ref _hde8nv3t ,(_umao48xu+(_cyu21nam - 1) + (_u0afxfs0 - 1) * 1 * (_uhi0ls8i)),ref Unsafe.AsRef(*(_8vecpt74+(_znpjgsef - 1))) ,(_0zwn6fsy+(_cyu21nam - 1) + (_4ekj6112 - 1) * 1 * (_uhi0ls8i)),ref _uhi0ls8i ,(_gh266ol1+(_cyu21nam - 1) + (_4ekj6112 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_7nk40y8b+(_cyu21nam - 1) + (_4ekj6112 - 1) * 1 * (_u6e6d39b)),(_i8976ehd+(_cyu21nam - 1) + (_u0afxfs0 - 1) * 1 * (_u6e6d39b)),(_doljbvm2+(_cyu21nam - 1) + (_4ekj6112 - 1) * 1 * (_u6e6d39b)),(_7e60fcso+(_cyu21nam - 1) + (_u0afxfs0 - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef(*(_umlkckdg+(_znpjgsef - 1))) ,ref Unsafe.AsRef(*(_3crf0qn3+(_znpjgsef - 1))) ,ref Unsafe.AsRef(*(_irk8i6qr+(_znpjgsef - 1))) ,_dqanbbw3 ,ref _gro5yvfo );
Mark180:;
						// continue
					}
										}				}
Mark190:;
				// continue
			}
						}		}//* 
		//*     The nodes on the bottom level of the tree were solved 
		//*     by SLASDQ. The corresponding right singular vector 
		//*     matrices are in explicit form. Apply them back. 
		//* 
		
		_v4ofzyw5 = ((_rwm6akyl + (int)1) / (int)2);
		{
			System.Int32 __81fgg2dlsvn1956 = (System.Int32)(_v4ofzyw5);
			const System.Int32 __81fgg2step1956 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1956;
			for (__81fgg2count1956 = System.Math.Max(0, (System.Int32)(((System.Int32)(_rwm6akyl) - __81fgg2dlsvn1956 + __81fgg2step1956) / __81fgg2step1956)), _b5p6od9s = __81fgg2dlsvn1956; __81fgg2count1956 != 0; __81fgg2count1956--, _b5p6od9s += (__81fgg2step1956)) {

			{
				
				_egqdmelt = (_b5p6od9s - (int)1);
				_8jzcrkri = *(_4b6rt45i+(_q8fqp221 + _egqdmelt - 1));
				_zx57w4aj = *(_4b6rt45i+(_b53e0l58 + _egqdmelt - 1));
				_oqpc3yjg = *(_4b6rt45i+(_9bq9s7q7 + _egqdmelt - 1));
				_qwh8ts9f = (_zx57w4aj + (int)1);
				if (_b5p6od9s == _rwm6akyl)
				{
					
					_bds60snh = _oqpc3yjg;
				}
				else
				{
					
					_bds60snh = (_oqpc3yjg + (int)1);
				}
				
				_cyu21nam = (_8jzcrkri - _zx57w4aj);
				_5tcb1chw = (_8jzcrkri + (int)1);//* 
				//*        Since B and BX are complex, the following call to SGEMM is 
				//*        performed in two steps (real and imaginary parts). 
				//* 
				//*        CALL SGEMM( 'T', 'N', NLP1, NRHS, NLP1, ONE, VT( NLF, 1 ), LDU, 
				//*    $               B( NLF, 1 ), LDB, ZERO, BX( NLF, 1 ), LDBX ) 
				//* 
				
				_znpjgsef = ((_qwh8ts9f * _3nayvi7h) * (int)2);
				{
					System.Int32 __81fgg2dlsvn1957 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1957 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1957;
					for (__81fgg2count1957 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn1957 + __81fgg2step1957) / __81fgg2step1957)), _vmeebitr = __81fgg2dlsvn1957; __81fgg2count1957 != 0; __81fgg2count1957--, _vmeebitr += (__81fgg2step1957)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn1958 = (System.Int32)(_cyu21nam);
							const System.Int32 __81fgg2step1958 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1958;
							for (__81fgg2count1958 = System.Math.Max(0, (System.Int32)(((System.Int32)((_cyu21nam + _qwh8ts9f) - (int)1) - __81fgg2dlsvn1958 + __81fgg2step1958) / __81fgg2step1958)), _hgi9ttgs = __81fgg2dlsvn1958; __81fgg2count1958 != 0; __81fgg2count1958--, _hgi9ttgs += (__81fgg2step1958)) {

							{
								
								_znpjgsef = (_znpjgsef + (int)1);
								*(_dqanbbw3+(_znpjgsef - 1)) = ILNumerics.F2NET.Intrinsics.REAL(*(_p9n405a5+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_ly9opahg)) );
Mark200:;
								// continue
							}
														}						}
Mark210:;
						// continue
					}
										}				}
				_b8wa9454("T" ,"N" ,ref _qwh8ts9f ,ref _3nayvi7h ,ref _qwh8ts9f ,ref Unsafe.AsRef(_kxg5drh2) ,(_xdbczr8u+(_cyu21nam - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_dqanbbw3+((int)1 + ((_qwh8ts9f * _3nayvi7h) * (int)2) - 1)),ref _qwh8ts9f ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+((int)1 - 1)),ref _qwh8ts9f );
				_znpjgsef = ((_qwh8ts9f * _3nayvi7h) * (int)2);
				{
					System.Int32 __81fgg2dlsvn1959 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1959 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1959;
					for (__81fgg2count1959 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn1959 + __81fgg2step1959) / __81fgg2step1959)), _vmeebitr = __81fgg2dlsvn1959; __81fgg2count1959 != 0; __81fgg2count1959--, _vmeebitr += (__81fgg2step1959)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn1960 = (System.Int32)(_cyu21nam);
							const System.Int32 __81fgg2step1960 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1960;
							for (__81fgg2count1960 = System.Math.Max(0, (System.Int32)(((System.Int32)((_cyu21nam + _qwh8ts9f) - (int)1) - __81fgg2dlsvn1960 + __81fgg2step1960) / __81fgg2step1960)), _hgi9ttgs = __81fgg2dlsvn1960; __81fgg2count1960 != 0; __81fgg2count1960--, _hgi9ttgs += (__81fgg2step1960)) {

							{
								
								_znpjgsef = (_znpjgsef + (int)1);
								*(_dqanbbw3+(_znpjgsef - 1)) = ILNumerics.F2NET.Intrinsics.AIMAG(*(_p9n405a5+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_ly9opahg)) );
Mark220:;
								// continue
							}
														}						}
Mark230:;
						// continue
					}
										}				}
				_b8wa9454("T" ,"N" ,ref _qwh8ts9f ,ref _3nayvi7h ,ref _qwh8ts9f ,ref Unsafe.AsRef(_kxg5drh2) ,(_xdbczr8u+(_cyu21nam - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_dqanbbw3+((int)1 + ((_qwh8ts9f * _3nayvi7h) * (int)2) - 1)),ref _qwh8ts9f ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+((int)1 + (_qwh8ts9f * _3nayvi7h) - 1)),ref _qwh8ts9f );
				_jcjf3d8c = (int)0;
				_llqvt7c6 = (_qwh8ts9f * _3nayvi7h);
				{
					System.Int32 __81fgg2dlsvn1961 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1961 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1961;
					for (__81fgg2count1961 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn1961 + __81fgg2step1961) / __81fgg2step1961)), _vmeebitr = __81fgg2dlsvn1961; __81fgg2count1961 != 0; __81fgg2count1961--, _vmeebitr += (__81fgg2step1961)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn1962 = (System.Int32)(_cyu21nam);
							const System.Int32 __81fgg2step1962 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1962;
							for (__81fgg2count1962 = System.Math.Max(0, (System.Int32)(((System.Int32)((_cyu21nam + _qwh8ts9f) - (int)1) - __81fgg2dlsvn1962 + __81fgg2step1962) / __81fgg2step1962)), _hgi9ttgs = __81fgg2dlsvn1962; __81fgg2count1962 != 0; __81fgg2count1962--, _hgi9ttgs += (__81fgg2step1962)) {

							{
								
								_jcjf3d8c = (_jcjf3d8c + (int)1);
								_llqvt7c6 = (_llqvt7c6 + (int)1);
								*(_uqckf55l+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_hde8nv3t)) = ILNumerics.F2NET.Intrinsics.CMPLX(*(_dqanbbw3+(_jcjf3d8c - 1)) ,*(_dqanbbw3+(_llqvt7c6 - 1)) );
Mark240:;
								// continue
							}
														}						}
Mark250:;
						// continue
					}
										}				}//* 
				//*        Since B and BX are complex, the following call to SGEMM is 
				//*        performed in two steps (real and imaginary parts). 
				//* 
				//*        CALL SGEMM( 'T', 'N', NRP1, NRHS, NRP1, ONE, VT( NRF, 1 ), LDU, 
				//*    $               B( NRF, 1 ), LDB, ZERO, BX( NRF, 1 ), LDBX ) 
				//* 
				
				_znpjgsef = ((_bds60snh * _3nayvi7h) * (int)2);
				{
					System.Int32 __81fgg2dlsvn1963 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1963 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1963;
					for (__81fgg2count1963 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn1963 + __81fgg2step1963) / __81fgg2step1963)), _vmeebitr = __81fgg2dlsvn1963; __81fgg2count1963 != 0; __81fgg2count1963--, _vmeebitr += (__81fgg2step1963)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn1964 = (System.Int32)(_5tcb1chw);
							const System.Int32 __81fgg2step1964 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1964;
							for (__81fgg2count1964 = System.Math.Max(0, (System.Int32)(((System.Int32)((_5tcb1chw + _bds60snh) - (int)1) - __81fgg2dlsvn1964 + __81fgg2step1964) / __81fgg2step1964)), _hgi9ttgs = __81fgg2dlsvn1964; __81fgg2count1964 != 0; __81fgg2count1964--, _hgi9ttgs += (__81fgg2step1964)) {

							{
								
								_znpjgsef = (_znpjgsef + (int)1);
								*(_dqanbbw3+(_znpjgsef - 1)) = ILNumerics.F2NET.Intrinsics.REAL(*(_p9n405a5+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_ly9opahg)) );
Mark260:;
								// continue
							}
														}						}
Mark270:;
						// continue
					}
										}				}
				_b8wa9454("T" ,"N" ,ref _bds60snh ,ref _3nayvi7h ,ref _bds60snh ,ref Unsafe.AsRef(_kxg5drh2) ,(_xdbczr8u+(_5tcb1chw - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_dqanbbw3+((int)1 + ((_bds60snh * _3nayvi7h) * (int)2) - 1)),ref _bds60snh ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+((int)1 - 1)),ref _bds60snh );
				_znpjgsef = ((_bds60snh * _3nayvi7h) * (int)2);
				{
					System.Int32 __81fgg2dlsvn1965 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1965 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1965;
					for (__81fgg2count1965 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn1965 + __81fgg2step1965) / __81fgg2step1965)), _vmeebitr = __81fgg2dlsvn1965; __81fgg2count1965 != 0; __81fgg2count1965--, _vmeebitr += (__81fgg2step1965)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn1966 = (System.Int32)(_5tcb1chw);
							const System.Int32 __81fgg2step1966 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1966;
							for (__81fgg2count1966 = System.Math.Max(0, (System.Int32)(((System.Int32)((_5tcb1chw + _bds60snh) - (int)1) - __81fgg2dlsvn1966 + __81fgg2step1966) / __81fgg2step1966)), _hgi9ttgs = __81fgg2dlsvn1966; __81fgg2count1966 != 0; __81fgg2count1966--, _hgi9ttgs += (__81fgg2step1966)) {

							{
								
								_znpjgsef = (_znpjgsef + (int)1);
								*(_dqanbbw3+(_znpjgsef - 1)) = ILNumerics.F2NET.Intrinsics.AIMAG(*(_p9n405a5+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_ly9opahg)) );
Mark280:;
								// continue
							}
														}						}
Mark290:;
						// continue
					}
										}				}
				_b8wa9454("T" ,"N" ,ref _bds60snh ,ref _3nayvi7h ,ref _bds60snh ,ref Unsafe.AsRef(_kxg5drh2) ,(_xdbczr8u+(_5tcb1chw - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_dqanbbw3+((int)1 + ((_bds60snh * _3nayvi7h) * (int)2) - 1)),ref _bds60snh ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+((int)1 + (_bds60snh * _3nayvi7h) - 1)),ref _bds60snh );
				_jcjf3d8c = (int)0;
				_llqvt7c6 = (_bds60snh * _3nayvi7h);
				{
					System.Int32 __81fgg2dlsvn1967 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1967 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1967;
					for (__81fgg2count1967 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn1967 + __81fgg2step1967) / __81fgg2step1967)), _vmeebitr = __81fgg2dlsvn1967; __81fgg2count1967 != 0; __81fgg2count1967--, _vmeebitr += (__81fgg2step1967)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn1968 = (System.Int32)(_5tcb1chw);
							const System.Int32 __81fgg2step1968 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1968;
							for (__81fgg2count1968 = System.Math.Max(0, (System.Int32)(((System.Int32)((_5tcb1chw + _bds60snh) - (int)1) - __81fgg2dlsvn1968 + __81fgg2step1968) / __81fgg2step1968)), _hgi9ttgs = __81fgg2dlsvn1968; __81fgg2count1968 != 0; __81fgg2count1968--, _hgi9ttgs += (__81fgg2step1968)) {

							{
								
								_jcjf3d8c = (_jcjf3d8c + (int)1);
								_llqvt7c6 = (_llqvt7c6 + (int)1);
								*(_uqckf55l+(_hgi9ttgs - 1) + (_vmeebitr - 1) * 1 * (_hde8nv3t)) = ILNumerics.F2NET.Intrinsics.CMPLX(*(_dqanbbw3+(_jcjf3d8c - 1)) ,*(_dqanbbw3+(_llqvt7c6 - 1)) );
Mark300:;
								// continue
							}
														}						}
Mark310:;
						// continue
					}
										}				}//* 
				
Mark320:;
				// continue
			}
						}		}//* 
		
Mark330:;
		// continue//* 
		
		return;//* 
		//*     End of CLALSA 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
