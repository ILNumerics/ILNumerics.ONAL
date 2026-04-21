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
//*> \brief \b CUNM2R multiplies a general matrix by the unitary matrix from a QR factorization determined by cgeqrf (unblocked algorithm). 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CUNM2R + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/cunm2r.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/cunm2r.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/cunm2r.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CUNM2R( SIDE, TRANS, M, N, K, A, LDA, TAU, C, LDC, 
//*                          WORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          SIDE, TRANS 
//*       INTEGER            INFO, K, LDA, LDC, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX            A( LDA, * ), C( LDC, * ), TAU( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CUNM2R overwrites the general complex m-by-n matrix C with 
//*> 
//*>       Q * C  if SIDE = 'L' and TRANS = 'N', or 
//*> 
//*>       Q**H* C  if SIDE = 'L' and TRANS = 'C', or 
//*> 
//*>       C * Q  if SIDE = 'R' and TRANS = 'N', or 
//*> 
//*>       C * Q**H if SIDE = 'R' and TRANS = 'C', 
//*> 
//*> where Q is a complex unitary matrix defined as the product of k 
//*> elementary reflectors 
//*> 
//*>       Q = H(1) H(2) . . . H(k) 
//*> 
//*> as returned by CGEQRF. Q is of order m if SIDE = 'L' and of order n 
//*> if SIDE = 'R'. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] SIDE 
//*> \verbatim 
//*>          SIDE is CHARACTER*1 
//*>          = 'L': apply Q or Q**H from the Left 
//*>          = 'R': apply Q or Q**H from the Right 
//*> \endverbatim 
//*> 
//*> \param[in] TRANS 
//*> \verbatim 
//*>          TRANS is CHARACTER*1 
//*>          = 'N': apply Q  (No transpose) 
//*>          = 'C': apply Q**H (Conjugate transpose) 
//*> \endverbatim 
//*> 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of rows of the matrix C. M >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of columns of the matrix C. N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] K 
//*> \verbatim 
//*>          K is INTEGER 
//*>          The number of elementary reflectors whose product defines 
//*>          the matrix Q. 
//*>          If SIDE = 'L', M >= K >= 0; 
//*>          if SIDE = 'R', N >= K >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is COMPLEX array, dimension (LDA,K) 
//*>          The i-th column must contain the vector which defines the 
//*>          elementary reflector H(i), for i = 1,2,...,k, as returned by 
//*>          CGEQRF in the first k columns of its array argument A. 
//*>          A is modified by the routine but restored on exit. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A. 
//*>          If SIDE = 'L', LDA >= max(1,M); 
//*>          if SIDE = 'R', LDA >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[in] TAU 
//*> \verbatim 
//*>          TAU is COMPLEX array, dimension (K) 
//*>          TAU(i) must contain the scalar factor of the elementary 
//*>          reflector H(i), as returned by CGEQRF. 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is COMPLEX array, dimension (LDC,N) 
//*>          On entry, the m-by-n matrix C. 
//*>          On exit, C is overwritten by Q*C or Q**H*C or C*Q**H or C*Q. 
//*> \endverbatim 
//*> 
//*> \param[in] LDC 
//*> \verbatim 
//*>          LDC is INTEGER 
//*>          The leading dimension of the array C. LDC >= max(1,M). 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is COMPLEX array, dimension 
//*>                                   (N) if SIDE = 'L', 
//*>                                   (M) if SIDE = 'R' 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0: successful exit 
//*>          < 0: if INFO = -i, the i-th argument had an illegal value 
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

	 
	public static void _hoac9h9p(FString _m2cn2gjg, FString _scuo79v4, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, fcomplex* _0446f4de, fcomplex* _3crf0qn3, ref Int32 _1s3eymp4, fcomplex* _apig8meb, ref Int32 _gro5yvfo)
	{
#region variable declarations
fcomplex _kxg5drh2 =   new fcomplex(1f,0f);
Boolean _pvwxvshr =  default;
Boolean _2bzw4gjb =  default;
Int32 _b5p6od9s =  default;
Int32 _egqdmelt =  default;
Int32 _8ur10vsh =  default;
Int32 _b3a707ow =  default;
Int32 _8jzcrkri =  default;
Int32 _aynldcwj =  default;
Int32 _31eu052u =  default;
Int32 _q8n03esx =  default;
Int32 _joervqa5 =  default;
fcomplex _qmqa6kps =  default;
fcomplex _uc0dsfni =  default;
string fLanavab = default;
#endregion  variable declarations
_m2cn2gjg = _m2cn2gjg.Convert(1);
_scuo79v4 = _scuo79v4.Convert(1);

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
		//*     Test the input arguments 
		//* 
		
		_gro5yvfo = (int)0;
		_pvwxvshr = _w8y2rzgy(_m2cn2gjg ,"L" );
		_2bzw4gjb = _w8y2rzgy(_scuo79v4 ,"N" );//* 
		//*     NQ is the order of Q 
		//* 
		
		if (_pvwxvshr)
		{
			
			_joervqa5 = _ev4xhht5;
		}
		else
		{
			
			_joervqa5 = _dxpq0xkr;
		}
		
		if ((!(_pvwxvshr)) & (!(_w8y2rzgy(_m2cn2gjg ,"R" ))))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if ((!(_2bzw4gjb)) & (!(_w8y2rzgy(_scuo79v4 ,"C" ))))
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_ev4xhht5 < (int)0)
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if ((_umlkckdg < (int)0) | (_umlkckdg > _joervqa5))
		{
			
			_gro5yvfo = (int)-5;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_joervqa5 ))
		{
			
			_gro5yvfo = (int)-7;
		}
		else
		if (_1s3eymp4 < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ))
		{
			
			_gro5yvfo = (int)-10;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("CUNM2R" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if (((_ev4xhht5 == (int)0) | (_dxpq0xkr == (int)0)) | (_umlkckdg == (int)0))
		return;//* 
		
		if (((_pvwxvshr & (!(_2bzw4gjb))) | ((!(_pvwxvshr)) & _2bzw4gjb)))
		{
			
			_egqdmelt = (int)1;
			_8ur10vsh = _umlkckdg;
			_b3a707ow = (int)1;
		}
		else
		{
			
			_egqdmelt = _umlkckdg;
			_8ur10vsh = (int)1;
			_b3a707ow = (int)-1;
		}
		//* 
		
		if (_pvwxvshr)
		{
			
			_q8n03esx = _dxpq0xkr;
			_aynldcwj = (int)1;
		}
		else
		{
			
			_31eu052u = _ev4xhht5;
			_8jzcrkri = (int)1;
		}
		//* 
		
		{
			System.Int32 __81fgg2dlsvn1118 = (System.Int32)(_egqdmelt);
			System.Int32 __81fgg2step1118 = (System.Int32)(_b3a707ow);
			System.Int32 __81fgg2count1118;
			for (__81fgg2count1118 = System.Math.Max(0, (System.Int32)(((System.Int32)(_8ur10vsh) - __81fgg2dlsvn1118 + __81fgg2step1118) / __81fgg2step1118)), _b5p6od9s = __81fgg2dlsvn1118; __81fgg2count1118 != 0; __81fgg2count1118--, _b5p6od9s += (__81fgg2step1118)) {

			{
				
				if (_pvwxvshr)
				{
					//* 
					//*           H(i) or H(i)**H is applied to C(i:m,1:n) 
					//* 
					
					_31eu052u = ((_ev4xhht5 - _b5p6od9s) + (int)1);
					_8jzcrkri = _b5p6od9s;
				}
				else
				{
					//* 
					//*           H(i) or H(i)**H is applied to C(1:m,i:n) 
					//* 
					
					_q8n03esx = ((_dxpq0xkr - _b5p6od9s) + (int)1);
					_aynldcwj = _b5p6od9s;
				}
				//* 
				//*        Apply H(i) or H(i)**H 
				//* 
				
				if (_2bzw4gjb)
				{
					
					_uc0dsfni = *(_0446f4de+(_b5p6od9s - 1));
				}
				else
				{
					
					_uc0dsfni = ILNumerics.F2NET.Intrinsics.CONJG(*(_0446f4de+(_b5p6od9s - 1)) );
				}
				
				_qmqa6kps = *(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c));
				*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;
				_ok06eljh(_m2cn2gjg ,ref _31eu052u ,ref _q8n03esx ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref _uc0dsfni ,(_3crf0qn3+(_8jzcrkri - 1) + (_aynldcwj - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,_apig8meb );
				*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _qmqa6kps;
Mark10:;
				// continue
			}
						}		}
		return;//* 
		//*     End of CUNM2R 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
