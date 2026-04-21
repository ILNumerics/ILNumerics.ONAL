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
//*> \brief \b SLARF applies an elementary reflector to a general rectangular matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLARF + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slarf.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slarf.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slarf.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLARF( SIDE, M, N, V, INCV, TAU, C, LDC, WORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          SIDE 
//*       INTEGER            INCV, LDC, M, N 
//*       REAL               TAU 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               C( LDC, * ), V( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLARF applies a real elementary reflector H to a real m by n matrix 
//*> C, from either the left or the right. H is represented in the form 
//*> 
//*>       H = I - tau * v * v**T 
//*> 
//*> where tau is a real scalar and v is a real vector. 
//*> 
//*> If tau = 0, then H is taken to be the unit matrix. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] SIDE 
//*> \verbatim 
//*>          SIDE is CHARACTER*1 
//*>          = 'L': form  H * C 
//*>          = 'R': form  C * H 
//*> \endverbatim 
//*> 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of rows of the matrix C. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of columns of the matrix C. 
//*> \endverbatim 
//*> 
//*> \param[in] V 
//*> \verbatim 
//*>          V is REAL array, dimension 
//*>                     (1 + (M-1)*abs(INCV)) if SIDE = 'L' 
//*>                  or (1 + (N-1)*abs(INCV)) if SIDE = 'R' 
//*>          The vector v in the representation of H. V is not used if 
//*>          TAU = 0. 
//*> \endverbatim 
//*> 
//*> \param[in] INCV 
//*> \verbatim 
//*>          INCV is INTEGER 
//*>          The increment between elements of v. INCV <> 0. 
//*> \endverbatim 
//*> 
//*> \param[in] TAU 
//*> \verbatim 
//*>          TAU is REAL 
//*>          The value tau in the representation of H. 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is REAL array, dimension (LDC,N) 
//*>          On entry, the m by n matrix C. 
//*>          On exit, C is overwritten by the matrix H * C if SIDE = 'L', 
//*>          or C * H if SIDE = 'R'. 
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
//*>          WORK is REAL array, dimension 
//*>                         (N) if SIDE = 'L' 
//*>                      or (M) if SIDE = 'R' 
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
//*> \ingroup realOTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _tfywat2m(FString _m2cn2gjg, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, Single* _ycxba85s, ref Int32 _un5zhi97, ref Single _0446f4de, Single* _3crf0qn3, ref Int32 _1s3eymp4, Single* _apig8meb)
	{
#region variable declarations
Single _kxg5drh2 =  1f;
Single _d0547bi2 =  0f;
Boolean _ijtf5prt =  default;
Int32 _b5p6od9s =  default;
Int32 _thvhilfl =  default;
Int32 _mfjw2q1q =  default;
string fLanavab = default;
#endregion  variable declarations
_m2cn2gjg = _m2cn2gjg.Convert(1);

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
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		_ijtf5prt = _w8y2rzgy(_m2cn2gjg ,"L" );
		_thvhilfl = (int)0;
		_mfjw2q1q = (int)0;
		if (_0446f4de != _d0547bi2)
		{
			//!     Set up variables for scanning V.  LASTV begins pointing to the end 
			//!     of V. 
			
			if (_ijtf5prt)
			{
				
				_thvhilfl = _ev4xhht5;
			}
			else
			{
				
				_thvhilfl = _dxpq0xkr;
			}
			
			if (_un5zhi97 > (int)0)
			{
				
				_b5p6od9s = ((int)1 + ((_thvhilfl - (int)1) * _un5zhi97));
			}
			else
			{
				
				_b5p6od9s = (int)1;
			}
			//!     Look for the last non-zero row in V. 
			
			{
while (((_thvhilfl > (int)0) & (*(_ycxba85s+(_b5p6od9s - 1)) == _d0547bi2))) {
				{
					
					_thvhilfl = (_thvhilfl - (int)1);
					_b5p6od9s = (_b5p6od9s - _un5zhi97);
				}
								}			}
			if (_ijtf5prt)
			{
				//!     Scan for the last non-zero column in C(1:lastv,:). 
				
				_mfjw2q1q = _grpg1g94(ref _thvhilfl ,ref _dxpq0xkr ,_3crf0qn3 ,ref _1s3eymp4 );
			}
			else
			{
				//!     Scan for the last non-zero row in C(:,1:lastv). 
				
				_mfjw2q1q = _5x6qyzht(ref _ev4xhht5 ,ref _thvhilfl ,_3crf0qn3 ,ref _1s3eymp4 );
			}
			
		}
		//!     Note that lastc.eq.0 renders the BLAS operations null; no special 
		//!     case is needed at this level. 
		
		if (_ijtf5prt)
		{
			//* 
			//*        Form  H * C 
			//* 
			
			if (_thvhilfl > (int)0)
			{
				//* 
				//*           w(1:lastc,1) := C(1:lastv,1:lastc)**T * v(1:lastv,1) 
				//* 
				
				_9mvi1n8m("Transpose" ,ref _thvhilfl ,ref _mfjw2q1q ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 ,_ycxba85s ,ref _un5zhi97 ,ref Unsafe.AsRef(_d0547bi2) ,_apig8meb ,ref Unsafe.AsRef((int)1) );//* 
				//*           C(1:lastv,1:lastc) := C(...) - v(1:lastv,1) * w(1:lastc,1)**T 
				//* 
				
				_wlowjtxr(ref _thvhilfl ,ref _mfjw2q1q ,ref Unsafe.AsRef(-(_0446f4de)) ,_ycxba85s ,ref _un5zhi97 ,_apig8meb ,ref Unsafe.AsRef((int)1) ,_3crf0qn3 ,ref _1s3eymp4 );
			}
			
		}
		else
		{
			//* 
			//*        Form  C * H 
			//* 
			
			if (_thvhilfl > (int)0)
			{
				//* 
				//*           w(1:lastc,1) := C(1:lastc,1:lastv) * v(1:lastv,1) 
				//* 
				
				_9mvi1n8m("No transpose" ,ref _mfjw2q1q ,ref _thvhilfl ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 ,_ycxba85s ,ref _un5zhi97 ,ref Unsafe.AsRef(_d0547bi2) ,_apig8meb ,ref Unsafe.AsRef((int)1) );//* 
				//*           C(1:lastc,1:lastv) := C(...) - w(1:lastc,1) * v(1:lastv,1)**T 
				//* 
				
				_wlowjtxr(ref _mfjw2q1q ,ref _thvhilfl ,ref Unsafe.AsRef(-(_0446f4de)) ,_apig8meb ,ref Unsafe.AsRef((int)1) ,_ycxba85s ,ref _un5zhi97 ,_3crf0qn3 ,ref _1s3eymp4 );
			}
			
		}
		
		return;//* 
		//*     End of SLARF 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
