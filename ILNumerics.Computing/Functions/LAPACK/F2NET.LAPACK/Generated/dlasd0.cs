
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
//*> \brief \b DLASD0 computes the singular values of a real upper bidiagonal n-by-m matrix B with diagonal d and off-diagonal e. Used by sbdsdc. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLASD0 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlasd0.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlasd0.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlasd0.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLASD0( N, SQRE, D, E, U, LDU, VT, LDVT, SMLSIZ, IWORK, 
//*                          WORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, LDU, LDVT, N, SMLSIZ, SQRE 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IWORK( * ) 
//*       DOUBLE PRECISION   D( * ), E( * ), U( LDU, * ), VT( LDVT, * ), 
//*      $                   WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> Using a divide and conquer approach, DLASD0 computes the singular 
//*> value decomposition (SVD) of a real upper bidiagonal N-by-M 
//*> matrix B with diagonal D and offdiagonal E, where M = N + SQRE. 
//*> The algorithm computes orthogonal matrices U and VT such that 
//*> B = U * S * VT. The singular values S are overwritten on D. 
//*> 
//*> A related subroutine, DLASDA, computes only the singular values, 
//*> and optionally, the singular vectors in compact form. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>         On entry, the row dimension of the upper bidiagonal matrix. 
//*>         This is also the dimension of the main diagonal array D. 
//*> \endverbatim 
//*> 
//*> \param[in] SQRE 
//*> \verbatim 
//*>          SQRE is INTEGER 
//*>         Specifies the column dimension of the bidiagonal matrix. 
//*>         = 0: The bidiagonal matrix has column dimension M = N; 
//*>         = 1: The bidiagonal matrix has column dimension M = N+1; 
//*> \endverbatim 
//*> 
//*> \param[in,out] D 
//*> \verbatim 
//*>          D is DOUBLE PRECISION array, dimension (N) 
//*>         On entry D contains the main diagonal of the bidiagonal 
//*>         matrix. 
//*>         On exit D, if INFO = 0, contains its singular values. 
//*> \endverbatim 
//*> 
//*> \param[in,out] E 
//*> \verbatim 
//*>          E is DOUBLE PRECISION array, dimension (M-1) 
//*>         Contains the subdiagonal entries of the bidiagonal matrix. 
//*>         On exit, E has been destroyed. 
//*> \endverbatim 
//*> 
//*> \param[out] U 
//*> \verbatim 
//*>          U is DOUBLE PRECISION array, dimension (LDU, N) 
//*>         On exit, U contains the left singular vectors. 
//*> \endverbatim 
//*> 
//*> \param[in] LDU 
//*> \verbatim 
//*>          LDU is INTEGER 
//*>         On entry, leading dimension of U. 
//*> \endverbatim 
//*> 
//*> \param[out] VT 
//*> \verbatim 
//*>          VT is DOUBLE PRECISION array, dimension (LDVT, M) 
//*>         On exit, VT**T contains the right singular vectors. 
//*> \endverbatim 
//*> 
//*> \param[in] LDVT 
//*> \verbatim 
//*>          LDVT is INTEGER 
//*>         On entry, leading dimension of VT. 
//*> \endverbatim 
//*> 
//*> \param[in] SMLSIZ 
//*> \verbatim 
//*>          SMLSIZ is INTEGER 
//*>         On entry, maximum size of the subproblems at the 
//*>         bottom of the computation tree. 
//*> \endverbatim 
//*> 
//*> \param[out] IWORK 
//*> \verbatim 
//*>          IWORK is INTEGER array, dimension (8*N) 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is DOUBLE PRECISION array, dimension (3*M**2+2*M) 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit. 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value. 
//*>          > 0:  if INFO = 1, a singular value did not converge 
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
//*> \ingroup OTHERauxiliary 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>     Ming Gu and Huan Ren, Computer Science Division, University of 
//*>     California at Berkeley, USA 
//*> 
//*  ===================================================================== 

	 
	public static void _ycis8rat(ref Int32 _dxpq0xkr, ref Int32 _9qyq7j3e, Double* _plfm7z8g, Double* _864fslqq, Double* _7u55mqkq, ref Int32 _u6e6d39b, Double* _xdbczr8u, ref Int32 _h4ibbatv, ref Int32 _q1xpyios, Int32* _4b6rt45i, Double* _apig8meb, ref Int32 _gro5yvfo)
	{
#region variable declarations
Int32 _b5p6od9s =  default;
Int32 _egqdmelt =  default;
Int32 _8jzcrkri =  default;
Int32 _ca64lzpg =  default;
Int32 _2mrn743b =  default;
Int32 _3dimb00t =  default;
Int32 _q8fqp221 =  default;
Int32 _m1gysdbg =  default;
Int32 _426n50rt =  default;
Int32 _znpjgsef =  default;
Int32 _es1scagl =  default;
Int32 _jnnmt81a =  default;
Int32 _u0afxfs0 =  default;
Int32 _ev4xhht5 =  default;
Int32 _bcsi4mx0 =  default;
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
Int32 _x0hi7bqn =  default;
Double _r7cfteg3 =  default;
Double _bafcbx97 =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.1) -- 
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
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input parameters. 
		//* 
		
		_gro5yvfo = (int)0;//* 
		
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if ((_9qyq7j3e < (int)0) | (_9qyq7j3e > (int)1))
		{
			
			_gro5yvfo = (int)-2;
		}
		//* 
		
		_ev4xhht5 = (_dxpq0xkr + _9qyq7j3e);//* 
		
		if (_u6e6d39b < _dxpq0xkr)
		{
			
			_gro5yvfo = (int)-6;
		}
		else
		if (_h4ibbatv < _ev4xhht5)
		{
			
			_gro5yvfo = (int)-8;
		}
		else
		if (_q1xpyios < (int)3)
		{
			
			_gro5yvfo = (int)-9;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("DLASD0" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     If the input matrix is too small, call DLASDQ to find the SVD. 
		//* 
		
		if (_dxpq0xkr <= _q1xpyios)
		{
			
			_5gomekdd("U" ,ref _9qyq7j3e ,ref _dxpq0xkr ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,_plfm7z8g ,_864fslqq ,_xdbczr8u ,ref _h4ibbatv ,_7u55mqkq ,ref _u6e6d39b ,_7u55mqkq ,ref _u6e6d39b ,_apig8meb ,ref _gro5yvfo );
			return;
		}
		//* 
		//*     Set up the computation tree. 
		//* 
		
		_q8fqp221 = (int)1;
		_b53e0l58 = (_q8fqp221 + _dxpq0xkr);
		_9bq9s7q7 = (_b53e0l58 + _dxpq0xkr);
		_ca64lzpg = (_9bq9s7q7 + _dxpq0xkr);
		_426n50rt = (_ca64lzpg + _dxpq0xkr);
		_56ok77t2(ref _dxpq0xkr ,ref _0n683y3x ,ref _rwm6akyl ,(_4b6rt45i+(_q8fqp221 - 1)),(_4b6rt45i+(_b53e0l58 - 1)),(_4b6rt45i+(_9bq9s7q7 - 1)),ref _q1xpyios );//* 
		//*     For the nodes on bottom level of the tree, solve 
		//*     their subproblems by DLASDQ. 
		//* 
		
		_v4ofzyw5 = ((_rwm6akyl + (int)1) / (int)2);
		_bcsi4mx0 = (int)0;
		{
			System.Int32 __81fgg2dlsvn198 = (System.Int32)(_v4ofzyw5);
			const System.Int32 __81fgg2step198 = (System.Int32)((int)1);
			System.Int32 __81fgg2count198;
			for (__81fgg2count198 = System.Math.Max(0, (System.Int32)(((System.Int32)(_rwm6akyl) - __81fgg2dlsvn198 + __81fgg2step198) / __81fgg2step198)), _b5p6od9s = __81fgg2dlsvn198; __81fgg2count198 != 0; __81fgg2count198--, _b5p6od9s += (__81fgg2step198)) {

			{
				//* 
				//*     IC : center row of each node 
				//*     NL : number of rows of left  subproblem 
				//*     NR : number of rows of right subproblem 
				//*     NLF: starting row of the left   subproblem 
				//*     NRF: starting row of the right  subproblem 
				//* 
				
				_egqdmelt = (_b5p6od9s - (int)1);
				_8jzcrkri = *(_4b6rt45i+(_q8fqp221 + _egqdmelt - 1));
				_zx57w4aj = *(_4b6rt45i+(_b53e0l58 + _egqdmelt - 1));
				_qwh8ts9f = (_zx57w4aj + (int)1);
				_oqpc3yjg = *(_4b6rt45i+(_9bq9s7q7 + _egqdmelt - 1));
				_bds60snh = (_oqpc3yjg + (int)1);
				_cyu21nam = (_8jzcrkri - _zx57w4aj);
				_5tcb1chw = (_8jzcrkri + (int)1);
				_x0hi7bqn = (int)1;
				_5gomekdd("U" ,ref _x0hi7bqn ,ref _zx57w4aj ,ref _qwh8ts9f ,ref _zx57w4aj ,ref _bcsi4mx0 ,(_plfm7z8g+(_cyu21nam - 1)),(_864fslqq+(_cyu21nam - 1)),(_xdbczr8u+(_cyu21nam - 1) + (_cyu21nam - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv ,(_7u55mqkq+(_cyu21nam - 1) + (_cyu21nam - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_7u55mqkq+(_cyu21nam - 1) + (_cyu21nam - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,_apig8meb ,ref _gro5yvfo );
				if (_gro5yvfo != (int)0)
				{
					
					return;
				}
				
				_m1gysdbg = ((_ca64lzpg + _cyu21nam) - (int)2);
				{
					System.Int32 __81fgg2dlsvn199 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step199 = (System.Int32)((int)1);
					System.Int32 __81fgg2count199;
					for (__81fgg2count199 = System.Math.Max(0, (System.Int32)(((System.Int32)(_zx57w4aj) - __81fgg2dlsvn199 + __81fgg2step199) / __81fgg2step199)), _znpjgsef = __81fgg2dlsvn199; __81fgg2count199 != 0; __81fgg2count199--, _znpjgsef += (__81fgg2step199)) {

					{
						
						*(_4b6rt45i+(_m1gysdbg + _znpjgsef - 1)) = _znpjgsef;
Mark10:;
						// continue
					}
										}				}
				if (_b5p6od9s == _rwm6akyl)
				{
					
					_x0hi7bqn = _9qyq7j3e;
				}
				else
				{
					
					_x0hi7bqn = (int)1;
				}
				
				_bds60snh = (_oqpc3yjg + _x0hi7bqn);
				_5gomekdd("U" ,ref _x0hi7bqn ,ref _oqpc3yjg ,ref _bds60snh ,ref _oqpc3yjg ,ref _bcsi4mx0 ,(_plfm7z8g+(_5tcb1chw - 1)),(_864fslqq+(_5tcb1chw - 1)),(_xdbczr8u+(_5tcb1chw - 1) + (_5tcb1chw - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv ,(_7u55mqkq+(_5tcb1chw - 1) + (_5tcb1chw - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_7u55mqkq+(_5tcb1chw - 1) + (_5tcb1chw - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,_apig8meb ,ref _gro5yvfo );
				if (_gro5yvfo != (int)0)
				{
					
					return;
				}
				
				_m1gysdbg = (_ca64lzpg + _8jzcrkri);
				{
					System.Int32 __81fgg2dlsvn200 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step200 = (System.Int32)((int)1);
					System.Int32 __81fgg2count200;
					for (__81fgg2count200 = System.Math.Max(0, (System.Int32)(((System.Int32)(_oqpc3yjg) - __81fgg2dlsvn200 + __81fgg2step200) / __81fgg2step200)), _znpjgsef = __81fgg2dlsvn200; __81fgg2count200 != 0; __81fgg2count200--, _znpjgsef += (__81fgg2step200)) {

					{
						
						*(_4b6rt45i+((_m1gysdbg + _znpjgsef) - (int)1 - 1)) = _znpjgsef;
Mark20:;
						// continue
					}
										}				}
Mark30:;
				// continue
			}
						}		}//* 
		//*     Now conquer each subproblem bottom-up. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn201 = (System.Int32)(_0n683y3x);
			System.Int32 __81fgg2step201 = (System.Int32)((int)-1);
			System.Int32 __81fgg2count201;
			for (__81fgg2count201 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn201 + __81fgg2step201) / __81fgg2step201)), _u0afxfs0 = __81fgg2dlsvn201; __81fgg2count201 != 0; __81fgg2count201--, _u0afxfs0 += (__81fgg2step201)) {

			{
				//* 
				//*        Find the first node LF and last node LL on the 
				//*        current level LVL. 
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
					System.Int32 __81fgg2dlsvn202 = (System.Int32)(_es1scagl);
					const System.Int32 __81fgg2step202 = (System.Int32)((int)1);
					System.Int32 __81fgg2count202;
					for (__81fgg2count202 = System.Math.Max(0, (System.Int32)(((System.Int32)(_jnnmt81a) - __81fgg2dlsvn202 + __81fgg2step202) / __81fgg2step202)), _b5p6od9s = __81fgg2dlsvn202; __81fgg2count202 != 0; __81fgg2count202--, _b5p6od9s += (__81fgg2step202)) {

					{
						
						_3dimb00t = (_b5p6od9s - (int)1);
						_8jzcrkri = *(_4b6rt45i+(_q8fqp221 + _3dimb00t - 1));
						_zx57w4aj = *(_4b6rt45i+(_b53e0l58 + _3dimb00t - 1));
						_oqpc3yjg = *(_4b6rt45i+(_9bq9s7q7 + _3dimb00t - 1));
						_cyu21nam = (_8jzcrkri - _zx57w4aj);
						if ((_9qyq7j3e == (int)0) & (_b5p6od9s == _jnnmt81a))
						{
							
							_x0hi7bqn = _9qyq7j3e;
						}
						else
						{
							
							_x0hi7bqn = (int)1;
						}
						
						_2mrn743b = ((_ca64lzpg + _cyu21nam) - (int)1);
						_r7cfteg3 = *(_plfm7z8g+(_8jzcrkri - 1));
						_bafcbx97 = *(_864fslqq+(_8jzcrkri - 1));
						_mac9p8ny(ref _zx57w4aj ,ref _oqpc3yjg ,ref _x0hi7bqn ,(_plfm7z8g+(_cyu21nam - 1)),ref _r7cfteg3 ,ref _bafcbx97 ,(_7u55mqkq+(_cyu21nam - 1) + (_cyu21nam - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_xdbczr8u+(_cyu21nam - 1) + (_cyu21nam - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv ,(_4b6rt45i+(_2mrn743b - 1)),(_4b6rt45i+(_426n50rt - 1)),_apig8meb ,ref _gro5yvfo );//* 
						//*        Report the possible convergence failure. 
						//* 
						
						if (_gro5yvfo != (int)0)
						{
							
							return;
						}
						
Mark40:;
						// continue
					}
										}				}
Mark50:;
				// continue
			}
						}		}//* 
		
		return;//* 
		//*     End of DLASD0 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
