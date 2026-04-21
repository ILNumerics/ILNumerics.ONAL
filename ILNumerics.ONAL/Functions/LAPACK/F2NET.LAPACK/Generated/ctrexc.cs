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
//*> \brief \b CTREXC 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CTREXC + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/ctrexc.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/ctrexc.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/ctrexc.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CTREXC( COMPQ, N, T, LDT, Q, LDQ, IFST, ILST, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          COMPQ 
//*       INTEGER            IFST, ILST, INFO, LDQ, LDT, N 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX            Q( LDQ, * ), T( LDT, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CTREXC reorders the Schur factorization of a complex matrix 
//*> A = Q*T*Q**H, so that the diagonal element of T with row index IFST 
//*> is moved to row ILST. 
//*> 
//*> The Schur form T is reordered by a unitary similarity transformation 
//*> Z**H*T*Z, and optionally the matrix Q of Schur vectors is updated by 
//*> postmultplying it with Z. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] COMPQ 
//*> \verbatim 
//*>          COMPQ is CHARACTER*1 
//*>          = 'V':  update the matrix Q of Schur vectors; 
//*>          = 'N':  do not update Q. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix T. N >= 0. 
//*>          If N == 0 arguments ILST and IFST may be any value. 
//*> \endverbatim 
//*> 
//*> \param[in,out] T 
//*> \verbatim 
//*>          T is COMPLEX array, dimension (LDT,N) 
//*>          On entry, the upper triangular matrix T. 
//*>          On exit, the reordered upper triangular matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] LDT 
//*> \verbatim 
//*>          LDT is INTEGER 
//*>          The leading dimension of the array T. LDT >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[in,out] Q 
//*> \verbatim 
//*>          Q is COMPLEX array, dimension (LDQ,N) 
//*>          On entry, if COMPQ = 'V', the matrix Q of Schur vectors. 
//*>          On exit, if COMPQ = 'V', Q has been postmultiplied by the 
//*>          unitary transformation matrix Z which reorders T. 
//*>          If COMPQ = 'N', Q is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] LDQ 
//*> \verbatim 
//*>          LDQ is INTEGER 
//*>          The leading dimension of the array Q.  LDQ >= 1, and if 
//*>          COMPQ = 'V', LDQ >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[in] IFST 
//*> \verbatim 
//*>          IFST is INTEGER 
//*> \endverbatim 
//*> 
//*> \param[in] ILST 
//*> \verbatim 
//*>          ILST is INTEGER 
//*> 
//*>          Specify the reordering of the diagonal elements of T: 
//*>          The element with row index IFST is moved to row ILST by a 
//*>          sequence of transpositions between adjacent elements. 
//*>          1 <= IFST <= N; 1 <= ILST <= N. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value 
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
//*> \ingroup complexOTHERcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _r10bvo9j(FString _bzlmbpq3, ref Int32 _dxpq0xkr, fcomplex* _2ivtt43r, ref Int32 _w8yhbr2r, fcomplex* _atumjwo3, ref Int32 _u3fpniqy, ref Int32 _la6t805m, ref Int32 _ab05c09e, ref Int32 _gro5yvfo)
	{
#region variable declarations
Boolean _gh3pzgwj =  default;
Int32 _umlkckdg =  default;
Int32 _kwmm2c1l =  default;
Int32 _btk3kubr =  default;
Int32 _0g5xtuqg =  default;
Single _82tpdhyl =  default;
fcomplex _8tmd0ner =  default;
fcomplex _bhnvwhs6 =  default;
fcomplex _n2r5pyie =  default;
fcomplex _1ajfmh55 =  default;
string fLanavab = default;
#endregion  variable declarations
_bzlmbpq3 = _bzlmbpq3.Convert(1);

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
		//*     Decode and test the input parameters. 
		//* 
		
		_gro5yvfo = (int)0;
		_gh3pzgwj = _w8y2rzgy(_bzlmbpq3 ,"V" );
		if ((!(_w8y2rzgy(_bzlmbpq3 ,"N" ))) & (!(_gh3pzgwj)))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_w8yhbr2r < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if ((_u3fpniqy < (int)1) | (_gh3pzgwj & (_u3fpniqy < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))))
		{
			
			_gro5yvfo = (int)-6;
		}
		else
		if (((_la6t805m < (int)1) | (_la6t805m > _dxpq0xkr)) & (_dxpq0xkr > (int)0))
		{
			
			_gro5yvfo = (int)-7;
		}
		else
		if (((_ab05c09e < (int)1) | (_ab05c09e > _dxpq0xkr)) & (_dxpq0xkr > (int)0))
		{
			
			_gro5yvfo = (int)-8;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("CTREXC" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if ((_dxpq0xkr <= (int)1) | (_la6t805m == _ab05c09e))
		return;//* 
		
		if (_la6t805m < _ab05c09e)
		{
			//* 
			//*        Move the IFST-th diagonal element forward down the diagonal. 
			//* 
			
			_kwmm2c1l = (int)0;
			_btk3kubr = (int)-1;
			_0g5xtuqg = (int)1;
		}
		else
		{
			//* 
			//*        Move the IFST-th diagonal element backward up the diagonal. 
			//* 
			
			_kwmm2c1l = (int)-1;
			_btk3kubr = (int)0;
			_0g5xtuqg = (int)-1;
		}
		//* 
		
		{
			System.Int32 __81fgg2dlsvn2551 = (System.Int32)((_la6t805m + _kwmm2c1l));
			System.Int32 __81fgg2step2551 = (System.Int32)(_0g5xtuqg);
			System.Int32 __81fgg2count2551;
			for (__81fgg2count2551 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ab05c09e + _btk3kubr) - __81fgg2dlsvn2551 + __81fgg2step2551) / __81fgg2step2551)), _umlkckdg = __81fgg2dlsvn2551; __81fgg2count2551 != 0; __81fgg2count2551--, _umlkckdg += (__81fgg2step2551)) {

			{
				//* 
				//*        Interchange the k-th and (k+1)-th diagonal elements. 
				//* 
				
				_bhnvwhs6 = *(_2ivtt43r+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_w8yhbr2r));
				_n2r5pyie = *(_2ivtt43r+(_umlkckdg + (int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_w8yhbr2r));//* 
				//*        Determine the transformation to perform the interchange. 
				//* 
				
				_kztlml2u(ref Unsafe.AsRef(*(_2ivtt43r+(_umlkckdg - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_w8yhbr2r))) ,ref Unsafe.AsRef(_n2r5pyie - _bhnvwhs6) ,ref _82tpdhyl ,ref _8tmd0ner ,ref _1ajfmh55 );//* 
				//*        Apply transformation to the matrix T. 
				//* 
				
				if ((_umlkckdg + (int)2) <= _dxpq0xkr)
				_t5n7kuvb(ref Unsafe.AsRef((_dxpq0xkr - _umlkckdg) - (int)1) ,(_2ivtt43r+(_umlkckdg - 1) + (_umlkckdg + (int)2 - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,(_2ivtt43r+(_umlkckdg + (int)1 - 1) + (_umlkckdg + (int)2 - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,ref _82tpdhyl ,ref _8tmd0ner );
				_t5n7kuvb(ref Unsafe.AsRef(_umlkckdg - (int)1) ,(_2ivtt43r+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_2ivtt43r+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,ref _82tpdhyl ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.CONJG(_8tmd0ner )) );//* 
				
				*(_2ivtt43r+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_w8yhbr2r)) = _n2r5pyie;
				*(_2ivtt43r+(_umlkckdg + (int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_w8yhbr2r)) = _bhnvwhs6;//* 
				
				if (_gh3pzgwj)
				{
					//* 
					//*           Accumulate transformation in the matrix Q. 
					//* 
					
					_t5n7kuvb(ref _dxpq0xkr ,(_atumjwo3+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_u3fpniqy)),ref Unsafe.AsRef((int)1) ,(_atumjwo3+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_u3fpniqy)),ref Unsafe.AsRef((int)1) ,ref _82tpdhyl ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.CONJG(_8tmd0ner )) );
				}
				//* 
				
Mark10:;
				// continue
			}
						}		}//* 
		
		return;//* 
		//*     End of CTREXC 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
