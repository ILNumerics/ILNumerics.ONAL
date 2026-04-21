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
//*> \brief \b DBDSDC 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DBDSDC + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dbdsdc.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dbdsdc.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dbdsdc.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DBDSDC( UPLO, COMPQ, N, D, E, U, LDU, VT, LDVT, Q, IQ, 
//*                          WORK, IWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          COMPQ, UPLO 
//*       INTEGER            INFO, LDU, LDVT, N 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IQ( * ), IWORK( * ) 
//*       DOUBLE PRECISION   D( * ), E( * ), Q( * ), U( LDU, * ), 
//*      $                   VT( LDVT, * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DBDSDC computes the singular value decomposition (SVD) of a real 
//*> N-by-N (upper or lower) bidiagonal matrix B:  B = U * S * VT, 
//*> using a divide and conquer method, where S is a diagonal matrix 
//*> with non-negative diagonal elements (the singular values of B), and 
//*> U and VT are orthogonal matrices of left and right singular vectors, 
//*> respectively. DBDSDC can be used to compute all singular values, 
//*> and optionally, singular vectors or singular vectors in compact form. 
//*> 
//*> This code makes very mild assumptions about floating point 
//*> arithmetic. It will work on machines with a guard digit in 
//*> add/subtract, or on those binary machines without guard digits 
//*> which subtract like the Cray X-MP, Cray Y-MP, Cray C-90, or Cray-2. 
//*> It could conceivably fail on hexadecimal or decimal machines 
//*> without guard digits, but we know of none.  See DLASD3 for details. 
//*> 
//*> The code currently calls DLASDQ if singular values only are desired. 
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
//*>          D is DOUBLE PRECISION array, dimension (N) 
//*>          On entry, the n diagonal elements of the bidiagonal matrix B. 
//*>          On exit, if INFO=0, the singular values of B. 
//*> \endverbatim 
//*> 
//*> \param[in,out] E 
//*> \verbatim 
//*>          E is DOUBLE PRECISION array, dimension (N-1) 
//*>          On entry, the elements of E contain the offdiagonal 
//*>          elements of the bidiagonal matrix whose SVD is desired. 
//*>          On exit, E has been destroyed. 
//*> \endverbatim 
//*> 
//*> \param[out] U 
//*> \verbatim 
//*>          U is DOUBLE PRECISION array, dimension (LDU,N) 
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
//*>          VT is DOUBLE PRECISION array, dimension (LDVT,N) 
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
//*>          Q is DOUBLE PRECISION array, dimension (LDQ) 
//*>          If  COMPQ = 'P', then: 
//*>             On exit, if INFO = 0, Q and IQ contain the left 
//*>             and right singular vectors in a compact form, 
//*>             requiring O(N log N) space instead of 2*N**2. 
//*>             In particular, Q contains all the DOUBLE PRECISION data in 
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
//*>          WORK is DOUBLE PRECISION array, dimension (MAX(1,LWORK)) 
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

	 
	public static void _c3rn9m7n(FString _9wyre9zc, FString _bzlmbpq3, ref Int32 _dxpq0xkr, Double* _plfm7z8g, Double* _864fslqq, Double* _7u55mqkq, ref Int32 _u6e6d39b, Double* _xdbczr8u, ref Int32 _h4ibbatv, Double* _atumjwo3, Int32* _zicl1k1z, Double* _apig8meb, Int32* _4b6rt45i, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Double _5m0mjfxm =  2d;
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
Double _82tpdhyl =  default;
Double _p1iqarg6 =  default;
Double _wby36o6h =  default;
Double _ejwydfmr =  default;
Double _q2vwp05i =  default;
Double _8tmd0ner =  default;
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
			
			_ut9qalzx("DBDSDC" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr == (int)0)
		return;
		_q1xpyios = _4mvd6e4d(ref Unsafe.AsRef((int)9) ,"DBDSDC" ," " ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) );
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
			
			_gvjhlct0(ref _dxpq0xkr ,_plfm7z8g ,ref Unsafe.AsRef((int)1) ,(_atumjwo3+((int)1 - 1)),ref Unsafe.AsRef((int)1) );
			_gvjhlct0(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,_864fslqq ,ref Unsafe.AsRef((int)1) ,(_atumjwo3+(_dxpq0xkr + (int)1 - 1)),ref Unsafe.AsRef((int)1) );
		}
		
		if (_ql1ymlhy == (int)2)
		{
			
			_w15yjv54 = (int)5;
			if (_y1be9goe == (int)2)
			_rg8cki6y = (((int)2 * _dxpq0xkr) - (int)1);
			{
				System.Int32 __81fgg2dlsvn171 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step171 = (System.Int32)((int)1);
				System.Int32 __81fgg2count171;
				for (__81fgg2count171 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn171 + __81fgg2step171) / __81fgg2step171)), _b5p6od9s = __81fgg2dlsvn171; __81fgg2count171 != 0; __81fgg2count171--, _b5p6od9s += (__81fgg2step171)) {

				{
					
					_uasfzoa5(ref Unsafe.AsRef(*(_plfm7z8g+(_b5p6od9s - 1))) ,ref Unsafe.AsRef(*(_864fslqq+(_b5p6od9s - 1))) ,ref _82tpdhyl ,ref _8tmd0ner ,ref _q2vwp05i );
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
		//*     If ICOMPQ = 0, use DLASDQ to compute the singular values. 
		//* 
		
		if (_y1be9goe == (int)0)
		{
			//*        Ignore WSTART, instead using WORK( 1 ), since the two vectors 
			//*        for CS and -SN above are added only if ICOMPQ == 2, 
			//*        and adding them exceeds documented WORK size of 4*n. 
			
			_5gomekdd("U" ,ref Unsafe.AsRef((int)0) ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,_plfm7z8g ,_864fslqq ,_xdbczr8u ,ref _h4ibbatv ,_7u55mqkq ,ref _u6e6d39b ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+((int)1 - 1)),ref _gro5yvfo );goto Mark40;
		}
		//* 
		//*     If N is smaller than the minimum divide size SMLSIZ, then solve 
		//*     the problem with another solver. 
		//* 
		
		if (_dxpq0xkr <= _q1xpyios)
		{
			
			if (_y1be9goe == (int)2)
			{
				
				_rta9tuwm("A" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,_7u55mqkq ,ref _u6e6d39b );
				_rta9tuwm("A" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,_xdbczr8u ,ref _h4ibbatv );
				_5gomekdd("U" ,ref Unsafe.AsRef((int)0) ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,_plfm7z8g ,_864fslqq ,_xdbczr8u ,ref _h4ibbatv ,_7u55mqkq ,ref _u6e6d39b ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_rg8cki6y - 1)),ref _gro5yvfo );
			}
			else
			if (_y1be9goe == (int)1)
			{
				
				_j4l29b9c = (int)1;
				_gt43n8d1 = (_j4l29b9c + _dxpq0xkr);
				_rta9tuwm("A" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,(_atumjwo3+(_j4l29b9c + ((_w15yjv54 - (int)1) * _dxpq0xkr) - 1)),ref _dxpq0xkr );
				_rta9tuwm("A" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,(_atumjwo3+(_gt43n8d1 + ((_w15yjv54 - (int)1) * _dxpq0xkr) - 1)),ref _dxpq0xkr );
				_5gomekdd("U" ,ref Unsafe.AsRef((int)0) ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,_plfm7z8g ,_864fslqq ,(_atumjwo3+(_gt43n8d1 + ((_w15yjv54 - (int)1) * _dxpq0xkr) - 1)),ref _dxpq0xkr ,(_atumjwo3+(_j4l29b9c + ((_w15yjv54 - (int)1) * _dxpq0xkr) - 1)),ref _dxpq0xkr ,(_atumjwo3+(_j4l29b9c + ((_w15yjv54 - (int)1) * _dxpq0xkr) - 1)),ref _dxpq0xkr ,(_apig8meb+(_rg8cki6y - 1)),ref _gro5yvfo );
			}
			goto Mark40;
		}
		//* 
		
		if (_y1be9goe == (int)2)
		{
			
			_rta9tuwm("A" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,_7u55mqkq ,ref _u6e6d39b );
			_rta9tuwm("A" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,_xdbczr8u ,ref _h4ibbatv );
		}
		//* 
		//*     Scale. 
		//* 
		
		_wby36o6h = _j0e1628u("M" ,ref _dxpq0xkr ,_plfm7z8g ,_864fslqq );
		if (_wby36o6h == _d0547bi2)
		return;
		_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _wby36o6h ,ref Unsafe.AsRef(_kxg5drh2) ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1) ,_plfm7z8g ,ref _dxpq0xkr ,ref _bhsiylw4 );
		_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _wby36o6h ,ref Unsafe.AsRef(_kxg5drh2) ,ref _3xbv3idt ,ref Unsafe.AsRef((int)1) ,_864fslqq ,ref _3xbv3idt ,ref _bhsiylw4 );//* 
		
		_p1iqarg6 = ((0.9d) * _f43eg0w0("Epsilon" ));//* 
		
		_12yfdoz9 = (ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.DBLE(_dxpq0xkr ) / ILNumerics.F2NET.Intrinsics.DBLE(_q1xpyios + (int)1 ) ) / ILNumerics.F2NET.Intrinsics.LOG(_5m0mjfxm ) ) + (int)1);
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
			System.Int32 __81fgg2dlsvn172 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step172 = (System.Int32)((int)1);
			System.Int32 __81fgg2count172;
			for (__81fgg2count172 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn172 + __81fgg2step172) / __81fgg2step172)), _b5p6od9s = __81fgg2dlsvn172; __81fgg2count172 != 0; __81fgg2count172--, _b5p6od9s += (__81fgg2step172)) {

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
			System.Int32 __81fgg2dlsvn173 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step173 = (System.Int32)((int)1);
			System.Int32 __81fgg2count173;
			for (__81fgg2count173 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3xbv3idt) - __81fgg2dlsvn173 + __81fgg2step173) / __81fgg2step173)), _b5p6od9s = __81fgg2dlsvn173; __81fgg2count173 != 0; __81fgg2count173--, _b5p6od9s += (__81fgg2step173)) {

			{
				
				if ((ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_b5p6od9s - 1)) ) < _p1iqarg6) | (_b5p6od9s == _3xbv3idt))
				{
					//* 
					//*           Subproblem found. First determine its size and then 
					//*           apply divide and conquer on it. 
					//* 
					
					if (_b5p6od9s < _3xbv3idt)
					{
						//* 
						//*              A subproblem with E(I) small for I < NM1. 
						//* 
						
						_2bwe2jrn = ((_b5p6od9s - _cusbn3d9) + (int)1);
					}
					else
					if (ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_b5p6od9s - 1)) ) >= _p1iqarg6)
					{
						//* 
						//*              A subproblem with E(NM1) not too small but I = NM1. 
						//* 
						
						_2bwe2jrn = ((_dxpq0xkr - _cusbn3d9) + (int)1);
					}
					else
					{
						//* 
						//*              A subproblem with E(NM1) small. This implies an 
						//*              1-by-1 subproblem at D(N). Solve this 1-by-1 problem 
						//*              first. 
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
						
						_ycis8rat(ref _2bwe2jrn ,ref _9qyq7j3e ,(_plfm7z8g+(_cusbn3d9 - 1)),(_864fslqq+(_cusbn3d9 - 1)),(_7u55mqkq+(_cusbn3d9 - 1) + (_cusbn3d9 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_xdbczr8u+(_cusbn3d9 - 1) + (_cusbn3d9 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv ,ref _q1xpyios ,_4b6rt45i ,(_apig8meb+(_rg8cki6y - 1)),ref _gro5yvfo );
					}
					else
					{
						
						_r8j651rt(ref _y1be9goe ,ref _q1xpyios ,ref _2bwe2jrn ,ref _9qyq7j3e ,(_plfm7z8g+(_cusbn3d9 - 1)),(_864fslqq+(_cusbn3d9 - 1)),(_atumjwo3+(_cusbn3d9 + (((_j4l29b9c + _w15yjv54) - (int)2) * _dxpq0xkr) - 1)),ref _dxpq0xkr ,(_atumjwo3+(_cusbn3d9 + (((_gt43n8d1 + _w15yjv54) - (int)2) * _dxpq0xkr) - 1)),(_zicl1k1z+(_cusbn3d9 + (_umlkckdg * _dxpq0xkr) - 1)),(_atumjwo3+(_cusbn3d9 + (((_i8976ehd + _w15yjv54) - (int)2) * _dxpq0xkr) - 1)),(_atumjwo3+(_cusbn3d9 + (((_doljbvm2 + _w15yjv54) - (int)2) * _dxpq0xkr) - 1)),(_atumjwo3+(_cusbn3d9 + (((_7e60fcso + _w15yjv54) - (int)2) * _dxpq0xkr) - 1)),(_atumjwo3+(_cusbn3d9 + (((_7nk40y8b + _w15yjv54) - (int)2) * _dxpq0xkr) - 1)),(_zicl1k1z+(_cusbn3d9 + (_8vecpt74 * _dxpq0xkr) - 1)),(_zicl1k1z+(_cusbn3d9 + (_0zwn6fsy * _dxpq0xkr) - 1)),ref _dxpq0xkr ,(_zicl1k1z+(_cusbn3d9 + (_umao48xu * _dxpq0xkr) - 1)),(_atumjwo3+(_cusbn3d9 + (((_gh266ol1 + _w15yjv54) - (int)2) * _dxpq0xkr) - 1)),(_atumjwo3+(_cusbn3d9 + (((_8jzcrkri + _w15yjv54) - (int)2) * _dxpq0xkr) - 1)),(_atumjwo3+(_cusbn3d9 + (((_5kucxo3c + _w15yjv54) - (int)2) * _dxpq0xkr) - 1)),(_apig8meb+(_rg8cki6y - 1)),_4b6rt45i ,ref _gro5yvfo );
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
		
		_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef(_kxg5drh2) ,ref _wby36o6h ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1) ,_plfm7z8g ,ref _dxpq0xkr ,ref _bhsiylw4 );
Mark40:;
		// continue//* 
		//*     Use Selection Sort to minimize swaps of singular vectors 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn174 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step174 = (System.Int32)((int)1);
			System.Int32 __81fgg2count174;
			for (__81fgg2count174 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn174 + __81fgg2step174) / __81fgg2step174)), _retbwjxi = __81fgg2dlsvn174; __81fgg2count174 != 0; __81fgg2count174--, _retbwjxi += (__81fgg2step174)) {

			{
				
				_b5p6od9s = (_retbwjxi - (int)1);
				_dulqqknh = _b5p6od9s;
				_ejwydfmr = *(_plfm7z8g+(_b5p6od9s - 1));
				{
					System.Int32 __81fgg2dlsvn175 = (System.Int32)(_retbwjxi);
					const System.Int32 __81fgg2step175 = (System.Int32)((int)1);
					System.Int32 __81fgg2count175;
					for (__81fgg2count175 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn175 + __81fgg2step175) / __81fgg2step175)), _znpjgsef = __81fgg2dlsvn175; __81fgg2count175 != 0; __81fgg2count175--, _znpjgsef += (__81fgg2step175)) {

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
						
						_trit81n6(ref _dxpq0xkr ,(_7u55mqkq+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef((int)1) ,(_7u55mqkq+((int)1 - 1) + (_dulqqknh - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef((int)1) );
						_trit81n6(ref _dxpq0xkr ,(_xdbczr8u+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv ,(_xdbczr8u+(_dulqqknh - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv );
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
		_sg7u7241("L" ,"V" ,"B" ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+((int)1 - 1)),(_apig8meb+(_dxpq0xkr - 1)),_7u55mqkq ,ref _u6e6d39b );//* 
		
		return;//* 
		//*     End of DBDSDC 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
