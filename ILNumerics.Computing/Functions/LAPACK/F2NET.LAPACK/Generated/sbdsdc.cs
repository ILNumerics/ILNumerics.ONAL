
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
//*> \brief \b SBDSDC 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SBDSDC + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/sbdsdc.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/sbdsdc.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/sbdsdc.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SBDSDC( UPLO, COMPQ, N, D, E, U, LDU, VT, LDVT, Q, IQ, 
//*                          WORK, IWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          COMPQ, UPLO 
//*       INTEGER            INFO, LDU, LDVT, N 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IQ( * ), IWORK( * ) 
//*       REAL               D( * ), E( * ), Q( * ), U( LDU, * ), 
//*      $                   VT( LDVT, * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SBDSDC computes the singular value decomposition (SVD) of a real 
//*> N-by-N (upper or lower) bidiagonal matrix B:  B = U * S * VT, 
//*> using a divide and conquer method, where S is a diagonal matrix 
//*> with non-negative diagonal elements (the singular values of B), and 
//*> U and VT are orthogonal matrices of left and right singular vectors, 
//*> respectively. SBDSDC can be used to compute all singular values, 
//*> and optionally, singular vectors or singular vectors in compact form. 
//*> 
//*> This code makes very mild assumptions about floating point 
//*> arithmetic. It will work on machines with a guard digit in 
//*> add/subtract, or on those binary machines without guard digits 
//*> which subtract like the Cray X-MP, Cray Y-MP, Cray C-90, or Cray-2. 
//*> It could conceivably fail on hexadecimal or decimal machines 
//*> without guard digits, but we know of none.  See SLASD3 for details. 
//*> 
//*> The code currently calls SLASDQ if singular values only are desired. 
//*> However, it can be slightly modified to compute singular values 
//*> using the divide and conquer method. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>          = 'U':  B is upper bidiagonal. 
//*>          = 'L':  B is lower bidiagonal. 
//*> \endverbatim 
//*> 
//*> \param[in] COMPQ 
//*> \verbatim 
//*>          COMPQ is CHARACTER*1 
//*>          Specifies whether singular vectors are to be computed 
//*>          as follows: 
//*>          = 'N':  Compute singular values only; 
//*>          = 'P':  Compute singular values and compute singular 
//*>                  vectors in compact form; 
//*>          = 'I':  Compute singular values and singular vectors. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix B.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] D 
//*> \verbatim 
//*>          D is REAL array, dimension (N) 
//*>          On entry, the n diagonal elements of the bidiagonal matrix B. 
//*>          On exit, if INFO=0, the singular values of B. 
//*> \endverbatim 
//*> 
//*> \param[in,out] E 
//*> \verbatim 
//*>          E is REAL array, dimension (N-1) 
//*>          On entry, the elements of E contain the offdiagonal 
//*>          elements of the bidiagonal matrix whose SVD is desired. 
//*>          On exit, E has been destroyed. 
//*> \endverbatim 
//*> 
//*> \param[out] U 
//*> \verbatim 
//*>          U is REAL array, dimension (LDU,N) 
//*>          If  COMPQ = 'I', then: 
//*>             On exit, if INFO = 0, U contains the left singular vectors 
//*>             of the bidiagonal matrix. 
//*>          For other values of COMPQ, U is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] LDU 
//*> \verbatim 
//*>          LDU is INTEGER 
//*>          The leading dimension of the array U.  LDU >= 1. 
//*>          If singular vectors are desired, then LDU >= max( 1, N ). 
//*> \endverbatim 
//*> 
//*> \param[out] VT 
//*> \verbatim 
//*>          VT is REAL array, dimension (LDVT,N) 
//*>          If  COMPQ = 'I', then: 
//*>             On exit, if INFO = 0, VT**T contains the right singular 
//*>             vectors of the bidiagonal matrix. 
//*>          For other values of COMPQ, VT is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] LDVT 
//*> \verbatim 
//*>          LDVT is INTEGER 
//*>          The leading dimension of the array VT.  LDVT >= 1. 
//*>          If singular vectors are desired, then LDVT >= max( 1, N ). 
//*> \endverbatim 
//*> 
//*> \param[out] Q 
//*> \verbatim 
//*>          Q is REAL array, dimension (LDQ) 
//*>          If  COMPQ = 'P', then: 
//*>             On exit, if INFO = 0, Q and IQ contain the left 
//*>             and right singular vectors in a compact form, 
//*>             requiring O(N log N) space instead of 2*N**2. 
//*>             In particular, Q contains all the REAL data in 
//*>             LDQ >= N*(11 + 2*SMLSIZ + 8*INT(LOG_2(N/(SMLSIZ+1)))) 
//*>             words of memory, where SMLSIZ is returned by ILAENV and 
//*>             is equal to the maximum size of the subproblems at the 
//*>             bottom of the computation tree (usually about 25). 
//*>          For other values of COMPQ, Q is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[out] IQ 
//*> \verbatim 
//*>          IQ is INTEGER array, dimension (LDIQ) 
//*>          If  COMPQ = 'P', then: 
//*>             On exit, if INFO = 0, Q and IQ contain the left 
//*>             and right singular vectors in a compact form, 
//*>             requiring O(N log N) space instead of 2*N**2. 
//*>             In particular, IQ contains all INTEGER data in 
//*>             LDIQ >= N*(3 + 3*INT(LOG_2(N/(SMLSIZ+1)))) 
//*>             words of memory, where SMLSIZ is returned by ILAENV and 
//*>             is equal to the maximum size of the subproblems at the 
//*>             bottom of the computation tree (usually about 25). 
//*>          For other values of COMPQ, IQ is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is REAL array, dimension (MAX(1,LWORK)) 
//*>          If COMPQ = 'N' then LWORK >= (4 * N). 
//*>          If COMPQ = 'P' then LWORK >= (6 * N). 
//*>          If COMPQ = 'I' then LWORK >= (3 * N**2 + 4 * N). 
//*> \endverbatim 
//*> 
//*> \param[out] IWORK 
//*> \verbatim 
//*>          IWORK is INTEGER array, dimension (8*N) 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit. 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value. 
//*>          > 0:  The algorithm failed to compute a singular value. 
//*>                The update process of divide and conquer failed. 
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
//*> \ingroup auxOTHERcomputational 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>     Ming Gu and Huan Ren, Computer Science Division, University of 
//*>     California at Berkeley, USA 
//*> 
//*  ===================================================================== 

	 
	public static void _nr77ztza(FString _9wyre9zc, FString _bzlmbpq3, ref Int32 _dxpq0xkr, Single* _plfm7z8g, Single* _864fslqq, Single* _7u55mqkq, ref Int32 _u6e6d39b, Single* _xdbczr8u, ref Int32 _h4ibbatv, Single* _atumjwo3, Int32* _zicl1k1z, Single* _apig8meb, Int32* _4b6rt45i, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
Single _5m0mjfxm =  2f;
Int32 _i8976ehd =  default;
Int32 _doljbvm2 =  default;
Int32 _0zwn6fsy =  default;
Int32 _gh266ol1 =  default;
Int32 _8vecpt74 =  default;
Int32 _b5p6od9s =  default;
Int32 _8jzcrkri =  default;
Int32 _y1be9goe =  default;
Int32 _bhsiylw4 =  default;
Int32 _retbwjxi =  default;
Int32 _5kucxo3c =  default;
Int32 _j4l29b9c =  default;
Int32 _ql1ymlhy =  default;
Int32 _gt43n8d1 =  default;
Int32 _znpjgsef =  default;
Int32 _umlkckdg =  default;
Int32 _dulqqknh =  default;
Int32 _12yfdoz9 =  default;
Int32 _3xbv3idt =  default;
Int32 _2bwe2jrn =  default;
Int32 _umao48xu =  default;
Int32 _7nk40y8b =  default;
Int32 _w15yjv54 =  default;
Int32 _q1xpyios =  default;
Int32 _kku1nkf4 =  default;
Int32 _9qyq7j3e =  default;
Int32 _cusbn3d9 =  default;
Int32 _rg8cki6y =  default;
Int32 _7e60fcso =  default;
Single _82tpdhyl =  default;
Single _p1iqarg6 =  default;
Single _wby36o6h =  default;
Single _ejwydfmr =  default;
Single _q2vwp05i =  default;
Single _8tmd0ner =  default;
string fLanavab = default;
#endregion  variable declarations
_9wyre9zc = _9wyre9zc.Convert(1);
_bzlmbpq3 = _bzlmbpq3.Convert(1);

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
		//*  Changed dimension statement in comment describing E from (N) to 
		//*  (N-1).  Sven, 17 Feb 05. 
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
		
		_ql1ymlhy = (int)0;
		if (_w8y2rzgy(_9wyre9zc ,"U" ))
		_ql1ymlhy = (int)1;
		if (_w8y2rzgy(_9wyre9zc ,"L" ))
		_ql1ymlhy = (int)2;
		if (_w8y2rzgy(_bzlmbpq3 ,"N" ))
		{
			
			_y1be9goe = (int)0;
		}
		else
		if (_w8y2rzgy(_bzlmbpq3 ,"P" ))
		{
			
			_y1be9goe = (int)1;
		}
		else
		if (_w8y2rzgy(_bzlmbpq3 ,"I" ))
		{
			
			_y1be9goe = (int)2;
		}
		else
		{
			
			_y1be9goe = (int)-1;
		}
		
		if (_ql1ymlhy == (int)0)
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_y1be9goe < (int)0)
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if ((_u6e6d39b < (int)1) | ((_y1be9goe == (int)2) & (_u6e6d39b < _dxpq0xkr)))
		{
			
			_gro5yvfo = (int)-7;
		}
		else
		if ((_h4ibbatv < (int)1) | ((_y1be9goe == (int)2) & (_h4ibbatv < _dxpq0xkr)))
		{
			
			_gro5yvfo = (int)-9;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("SBDSDC" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr == (int)0)
		return;
		_q1xpyios = _4mvd6e4d(ref Unsafe.AsRef((int)9) ,"SBDSDC" ," " ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) );
		if (_dxpq0xkr == (int)1)
		{
			
			if (_y1be9goe == (int)1)
			{
				
				*(_atumjwo3+((int)1 - 1)) = ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,*(_plfm7z8g+((int)1 - 1)) );
				*(_atumjwo3+((int)1 + (_q1xpyios * _dxpq0xkr) - 1)) = _kxg5drh2;
			}
			else
			if (_y1be9goe == (int)2)
			{
				
				*(_7u55mqkq+((int)1 - 1) + ((int)1 - 1) * 1 * (_u6e6d39b)) = ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,*(_plfm7z8g+((int)1 - 1)) );
				*(_xdbczr8u+((int)1 - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)) = _kxg5drh2;
			}
			
			*(_plfm7z8g+((int)1 - 1)) = ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+((int)1 - 1)) );
			return;
		}
		
		_3xbv3idt = (_dxpq0xkr - (int)1);//* 
		//*     If matrix lower bidiagonal, rotate to be upper bidiagonal 
		//*     by applying Givens rotations on the left 
		//* 
		
		_rg8cki6y = (int)1;
		_w15yjv54 = (int)3;
		if (_y1be9goe == (int)1)
		{
			
			_wcs7ne88(ref _dxpq0xkr ,_plfm7z8g ,ref Unsafe.AsRef((int)1) ,(_atumjwo3+((int)1 - 1)),ref Unsafe.AsRef((int)1) );
			_wcs7ne88(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,_864fslqq ,ref Unsafe.AsRef((int)1) ,(_atumjwo3+(_dxpq0xkr + (int)1 - 1)),ref Unsafe.AsRef((int)1) );
		}
		
		if (_ql1ymlhy == (int)2)
		{
			
			_w15yjv54 = (int)5;
			if (_y1be9goe == (int)2)
			_rg8cki6y = (((int)2 * _dxpq0xkr) - (int)1);
			{
				System.Int32 __81fgg2dlsvn541 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step541 = (System.Int32)((int)1);
				System.Int32 __81fgg2count541;
				for (__81fgg2count541 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn541 + __81fgg2step541) / __81fgg2step541)), _b5p6od9s = __81fgg2dlsvn541; __81fgg2count541 != 0; __81fgg2count541--, _b5p6od9s += (__81fgg2step541)) {

				{
					
					_uf57gsrz(ref Unsafe.AsRef(*(_plfm7z8g+(_b5p6od9s - 1))) ,ref Unsafe.AsRef(*(_864fslqq+(_b5p6od9s - 1))) ,ref _82tpdhyl ,ref _8tmd0ner ,ref _q2vwp05i );
					*(_plfm7z8g+(_b5p6od9s - 1)) = _q2vwp05i;
					*(_864fslqq+(_b5p6od9s - 1)) = (_8tmd0ner * *(_plfm7z8g+(_b5p6od9s + (int)1 - 1)));
					*(_plfm7z8g+(_b5p6od9s + (int)1 - 1)) = (_82tpdhyl * *(_plfm7z8g+(_b5p6od9s + (int)1 - 1)));
					if (_y1be9goe == (int)1)
					{
						
						*(_atumjwo3+(_b5p6od9s + ((int)2 * _dxpq0xkr) - 1)) = _82tpdhyl;
						*(_atumjwo3+(_b5p6od9s + ((int)3 * _dxpq0xkr) - 1)) = _8tmd0ner;
					}
					else
					if (_y1be9goe == (int)2)
					{
						
						*(_apig8meb+(_b5p6od9s - 1)) = _82tpdhyl;
						*(_apig8meb+(_3xbv3idt + _b5p6od9s - 1)) = (-(_8tmd0ner));
					}
					
Mark10:;
					// continue
				}
								}			}
		}
		//* 
		//*     If ICOMPQ = 0, use SLASDQ to compute the singular values. 
		//* 
		
		if (_y1be9goe == (int)0)
		{
			//*        Ignore WSTART, instead using WORK( 1 ), since the two vectors 
			//*        for CS and -SN above are added only if ICOMPQ == 2, 
			//*        and adding them exceeds documented WORK size of 4*n. 
			
			_zfmooian("U" ,ref Unsafe.AsRef((int)0) ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,_plfm7z8g ,_864fslqq ,_xdbczr8u ,ref _h4ibbatv ,_7u55mqkq ,ref _u6e6d39b ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+((int)1 - 1)),ref _gro5yvfo );goto Mark40;
		}
		//* 
		//*     If N is smaller than the minimum divide size SMLSIZ, then solve 
		//*     the problem with another solver. 
		//* 
		
		if (_dxpq0xkr <= _q1xpyios)
		{
			
			if (_y1be9goe == (int)2)
			{
				
				_t013e1c8("A" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,_7u55mqkq ,ref _u6e6d39b );
				_t013e1c8("A" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,_xdbczr8u ,ref _h4ibbatv );
				_zfmooian("U" ,ref Unsafe.AsRef((int)0) ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,_plfm7z8g ,_864fslqq ,_xdbczr8u ,ref _h4ibbatv ,_7u55mqkq ,ref _u6e6d39b ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_rg8cki6y - 1)),ref _gro5yvfo );
			}
			else
			if (_y1be9goe == (int)1)
			{
				
				_j4l29b9c = (int)1;
				_gt43n8d1 = (_j4l29b9c + _dxpq0xkr);
				_t013e1c8("A" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,(_atumjwo3+(_j4l29b9c + ((_w15yjv54 - (int)1) * _dxpq0xkr) - 1)),ref _dxpq0xkr );
				_t013e1c8("A" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,(_atumjwo3+(_gt43n8d1 + ((_w15yjv54 - (int)1) * _dxpq0xkr) - 1)),ref _dxpq0xkr );
				_zfmooian("U" ,ref Unsafe.AsRef((int)0) ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,_plfm7z8g ,_864fslqq ,(_atumjwo3+(_gt43n8d1 + ((_w15yjv54 - (int)1) * _dxpq0xkr) - 1)),ref _dxpq0xkr ,(_atumjwo3+(_j4l29b9c + ((_w15yjv54 - (int)1) * _dxpq0xkr) - 1)),ref _dxpq0xkr ,(_atumjwo3+(_j4l29b9c + ((_w15yjv54 - (int)1) * _dxpq0xkr) - 1)),ref _dxpq0xkr ,(_apig8meb+(_rg8cki6y - 1)),ref _gro5yvfo );
			}
			goto Mark40;
		}
		//* 
		
		if (_y1be9goe == (int)2)
		{
			
			_t013e1c8("A" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,_7u55mqkq ,ref _u6e6d39b );
			_t013e1c8("A" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,_xdbczr8u ,ref _h4ibbatv );
		}
		//* 
		//*     Scale. 
		//* 
		
		_wby36o6h = _5kajfnos("M" ,ref _dxpq0xkr ,_plfm7z8g ,_864fslqq );
		if (_wby36o6h == _d0547bi2)
		return;
		_cq2kgmi4("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _wby36o6h ,ref Unsafe.AsRef(_kxg5drh2) ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1) ,_plfm7z8g ,ref _dxpq0xkr ,ref _bhsiylw4 );
		_cq2kgmi4("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _wby36o6h ,ref Unsafe.AsRef(_kxg5drh2) ,ref _3xbv3idt ,ref Unsafe.AsRef((int)1) ,_864fslqq ,ref _3xbv3idt ,ref _bhsiylw4 );//* 
		
		_p1iqarg6 = _d5tu038y("Epsilon" );//* 
		
		_12yfdoz9 = (ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.REAL(_dxpq0xkr ) / ILNumerics.F2NET.Intrinsics.REAL(_q1xpyios + (int)1 ) ) / ILNumerics.F2NET.Intrinsics.LOG(_5m0mjfxm ) ) + (int)1);
		_kku1nkf4 = (_q1xpyios + (int)1);//* 
		
		if (_y1be9goe == (int)1)
		{
			
			_j4l29b9c = (int)1;
			_gt43n8d1 = ((int)1 + _q1xpyios);
			_i8976ehd = (_gt43n8d1 + _kku1nkf4);
			_doljbvm2 = (_i8976ehd + _12yfdoz9);
			_7e60fcso = (_doljbvm2 + (_12yfdoz9 * (int)2));
			_8jzcrkri = (_7e60fcso + _12yfdoz9);
			_5kucxo3c = (_8jzcrkri + (int)1);
			_7nk40y8b = (_5kucxo3c + (int)1);
			_gh266ol1 = (_7nk40y8b + ((int)2 * _12yfdoz9));//* 
			
			_umlkckdg = (int)1;
			_8vecpt74 = (int)2;
			_umao48xu = (int)3;
			_0zwn6fsy = (_umao48xu + _12yfdoz9);
		}
		//* 
		
		{
			System.Int32 __81fgg2dlsvn542 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step542 = (System.Int32)((int)1);
			System.Int32 __81fgg2count542;
			for (__81fgg2count542 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn542 + __81fgg2step542) / __81fgg2step542)), _b5p6od9s = __81fgg2dlsvn542; __81fgg2count542 != 0; __81fgg2count542--, _b5p6od9s += (__81fgg2step542)) {

			{
				
				if (ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_b5p6od9s - 1)) ) < _p1iqarg6)
				{
					
					*(_plfm7z8g+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.SIGN(_p1iqarg6 ,*(_plfm7z8g+(_b5p6od9s - 1)) );
				}
				
Mark20:;
				// continue
			}
						}		}//* 
		
		_cusbn3d9 = (int)1;
		_9qyq7j3e = (int)0;//* 
		
		{
			System.Int32 __81fgg2dlsvn543 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step543 = (System.Int32)((int)1);
			System.Int32 __81fgg2count543;
			for (__81fgg2count543 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3xbv3idt) - __81fgg2dlsvn543 + __81fgg2step543) / __81fgg2step543)), _b5p6od9s = __81fgg2dlsvn543; __81fgg2count543 != 0; __81fgg2count543--, _b5p6od9s += (__81fgg2step543)) {

			{
				
				if ((ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_b5p6od9s - 1)) ) < _p1iqarg6) | (_b5p6od9s == _3xbv3idt))
				{
					//* 
					//*        Subproblem found. First determine its size and then 
					//*        apply divide and conquer on it. 
					//* 
					
					if (_b5p6od9s < _3xbv3idt)
					{
						//* 
						//*        A subproblem with E(I) small for I < NM1. 
						//* 
						
						_2bwe2jrn = ((_b5p6od9s - _cusbn3d9) + (int)1);
					}
					else
					if (ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_b5p6od9s - 1)) ) >= _p1iqarg6)
					{
						//* 
						//*        A subproblem with E(NM1) not too small but I = NM1. 
						//* 
						
						_2bwe2jrn = ((_dxpq0xkr - _cusbn3d9) + (int)1);
					}
					else
					{
						//* 
						//*        A subproblem with E(NM1) small. This implies an 
						//*        1-by-1 subproblem at D(N). Solve this 1-by-1 problem 
						//*        first. 
						//* 
						
						_2bwe2jrn = ((_b5p6od9s - _cusbn3d9) + (int)1);
						if (_y1be9goe == (int)2)
						{
							
							*(_7u55mqkq+(_dxpq0xkr - 1) + (_dxpq0xkr - 1) * 1 * (_u6e6d39b)) = ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,*(_plfm7z8g+(_dxpq0xkr - 1)) );
							*(_xdbczr8u+(_dxpq0xkr - 1) + (_dxpq0xkr - 1) * 1 * (_h4ibbatv)) = _kxg5drh2;
						}
						else
						if (_y1be9goe == (int)1)
						{
							
							*(_atumjwo3+(_dxpq0xkr + ((_w15yjv54 - (int)1) * _dxpq0xkr) - 1)) = ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,*(_plfm7z8g+(_dxpq0xkr - 1)) );
							*(_atumjwo3+(_dxpq0xkr + (((_q1xpyios + _w15yjv54) - (int)1) * _dxpq0xkr) - 1)) = _kxg5drh2;
						}
						
						*(_plfm7z8g+(_dxpq0xkr - 1)) = ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_dxpq0xkr - 1)) );
					}
					
					if (_y1be9goe == (int)2)
					{
						
						_027mou5p(ref _2bwe2jrn ,ref _9qyq7j3e ,(_plfm7z8g+(_cusbn3d9 - 1)),(_864fslqq+(_cusbn3d9 - 1)),(_7u55mqkq+(_cusbn3d9 - 1) + (_cusbn3d9 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_xdbczr8u+(_cusbn3d9 - 1) + (_cusbn3d9 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv ,ref _q1xpyios ,_4b6rt45i ,(_apig8meb+(_rg8cki6y - 1)),ref _gro5yvfo );
					}
					else
					{
						
						_cui96f25(ref _y1be9goe ,ref _q1xpyios ,ref _2bwe2jrn ,ref _9qyq7j3e ,(_plfm7z8g+(_cusbn3d9 - 1)),(_864fslqq+(_cusbn3d9 - 1)),(_atumjwo3+(_cusbn3d9 + (((_j4l29b9c + _w15yjv54) - (int)2) * _dxpq0xkr) - 1)),ref _dxpq0xkr ,(_atumjwo3+(_cusbn3d9 + (((_gt43n8d1 + _w15yjv54) - (int)2) * _dxpq0xkr) - 1)),(_zicl1k1z+(_cusbn3d9 + (_umlkckdg * _dxpq0xkr) - 1)),(_atumjwo3+(_cusbn3d9 + (((_i8976ehd + _w15yjv54) - (int)2) * _dxpq0xkr) - 1)),(_atumjwo3+(_cusbn3d9 + (((_doljbvm2 + _w15yjv54) - (int)2) * _dxpq0xkr) - 1)),(_atumjwo3+(_cusbn3d9 + (((_7e60fcso + _w15yjv54) - (int)2) * _dxpq0xkr) - 1)),(_atumjwo3+(_cusbn3d9 + (((_7nk40y8b + _w15yjv54) - (int)2) * _dxpq0xkr) - 1)),(_zicl1k1z+(_cusbn3d9 + (_8vecpt74 * _dxpq0xkr) - 1)),(_zicl1k1z+(_cusbn3d9 + (_0zwn6fsy * _dxpq0xkr) - 1)),ref _dxpq0xkr ,(_zicl1k1z+(_cusbn3d9 + (_umao48xu * _dxpq0xkr) - 1)),(_atumjwo3+(_cusbn3d9 + (((_gh266ol1 + _w15yjv54) - (int)2) * _dxpq0xkr) - 1)),(_atumjwo3+(_cusbn3d9 + (((_8jzcrkri + _w15yjv54) - (int)2) * _dxpq0xkr) - 1)),(_atumjwo3+(_cusbn3d9 + (((_5kucxo3c + _w15yjv54) - (int)2) * _dxpq0xkr) - 1)),(_apig8meb+(_rg8cki6y - 1)),_4b6rt45i ,ref _gro5yvfo );
					}
					
					if (_gro5yvfo != (int)0)
					{
						
						return;
					}
					
					_cusbn3d9 = (_b5p6od9s + (int)1);
				}
				
Mark30:;
				// continue
			}
						}		}//* 
		//*     Unscale 
		//* 
		
		_cq2kgmi4("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef(_kxg5drh2) ,ref _wby36o6h ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1) ,_plfm7z8g ,ref _dxpq0xkr ,ref _bhsiylw4 );
Mark40:;
		// continue//* 
		//*     Use Selection Sort to minimize swaps of singular vectors 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn544 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step544 = (System.Int32)((int)1);
			System.Int32 __81fgg2count544;
			for (__81fgg2count544 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn544 + __81fgg2step544) / __81fgg2step544)), _retbwjxi = __81fgg2dlsvn544; __81fgg2count544 != 0; __81fgg2count544--, _retbwjxi += (__81fgg2step544)) {

			{
				
				_b5p6od9s = (_retbwjxi - (int)1);
				_dulqqknh = _b5p6od9s;
				_ejwydfmr = *(_plfm7z8g+(_b5p6od9s - 1));
				{
					System.Int32 __81fgg2dlsvn545 = (System.Int32)(_retbwjxi);
					const System.Int32 __81fgg2step545 = (System.Int32)((int)1);
					System.Int32 __81fgg2count545;
					for (__81fgg2count545 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn545 + __81fgg2step545) / __81fgg2step545)), _znpjgsef = __81fgg2dlsvn545; __81fgg2count545 != 0; __81fgg2count545--, _znpjgsef += (__81fgg2step545)) {

					{
						
						if (*(_plfm7z8g+(_znpjgsef - 1)) > _ejwydfmr)
						{
							
							_dulqqknh = _znpjgsef;
							_ejwydfmr = *(_plfm7z8g+(_znpjgsef - 1));
						}
						
Mark50:;
						// continue
					}
										}				}
				if (_dulqqknh != _b5p6od9s)
				{
					
					*(_plfm7z8g+(_dulqqknh - 1)) = *(_plfm7z8g+(_b5p6od9s - 1));
					*(_plfm7z8g+(_b5p6od9s - 1)) = _ejwydfmr;
					if (_y1be9goe == (int)1)
					{
						
						*(_zicl1k1z+(_b5p6od9s - 1)) = _dulqqknh;
					}
					else
					if (_y1be9goe == (int)2)
					{
						
						_ahhuglvd(ref _dxpq0xkr ,(_7u55mqkq+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef((int)1) ,(_7u55mqkq+((int)1 - 1) + (_dulqqknh - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef((int)1) );
						_ahhuglvd(ref _dxpq0xkr ,(_xdbczr8u+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv ,(_xdbczr8u+(_dulqqknh - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv );
					}
					
				}
				else
				if (_y1be9goe == (int)1)
				{
					
					*(_zicl1k1z+(_b5p6od9s - 1)) = _b5p6od9s;
				}
				
Mark60:;
				// continue
			}
						}		}//* 
		//*     If ICOMPQ = 1, use IQ(N,1) as the indicator for UPLO 
		//* 
		
		if (_y1be9goe == (int)1)
		{
			
			if (_ql1ymlhy == (int)1)
			{
				
				*(_zicl1k1z+(_dxpq0xkr - 1)) = (int)1;
			}
			else
			{
				
				*(_zicl1k1z+(_dxpq0xkr - 1)) = (int)0;
			}
			
		}
		//* 
		//*     If B is lower bidiagonal, update U by those Givens rotations 
		//*     which rotated B to be upper bidiagonal 
		//* 
		
		if ((_ql1ymlhy == (int)2) & (_y1be9goe == (int)2))
		_2u3ycobg("L" ,"V" ,"B" ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+((int)1 - 1)),(_apig8meb+(_dxpq0xkr - 1)),_7u55mqkq ,ref _u6e6d39b );//* 
		
		return;//* 
		//*     End of SBDSDC 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
