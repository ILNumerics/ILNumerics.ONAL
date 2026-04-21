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
//*> \brief \b SORMTR 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SORMTR + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/sormtr.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/sormtr.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/sormtr.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SORMTR( SIDE, UPLO, TRANS, M, N, A, LDA, TAU, C, LDC, 
//*                          WORK, LWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          SIDE, TRANS, UPLO 
//*       INTEGER            INFO, LDA, LDC, LWORK, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               A( LDA, * ), C( LDC, * ), TAU( * ), 
//*      $                   WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SORMTR overwrites the general real M-by-N matrix C with 
//*> 
//*>                 SIDE = 'L'     SIDE = 'R' 
//*> TRANS = 'N':      Q * C          C * Q 
//*> TRANS = 'T':      Q**T * C       C * Q**T 
//*> 
//*> where Q is a real orthogonal matrix of order nq, with nq = m if 
//*> SIDE = 'L' and nq = n if SIDE = 'R'. Q is defined as the product of 
//*> nq-1 elementary reflectors, as returned by SSYTRD: 
//*> 
//*> if UPLO = 'U', Q = H(nq-1) . . . H(2) H(1); 
//*> 
//*> if UPLO = 'L', Q = H(1) H(2) . . . H(nq-1). 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] SIDE 
//*> \verbatim 
//*>          SIDE is CHARACTER*1 
//*>          = 'L': apply Q or Q**T from the Left; 
//*>          = 'R': apply Q or Q**T from the Right. 
//*> \endverbatim 
//*> 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>          = 'U': Upper triangle of A contains elementary reflectors 
//*>                 from SSYTRD; 
//*>          = 'L': Lower triangle of A contains elementary reflectors 
//*>                 from SSYTRD. 
//*> \endverbatim 
//*> 
//*> \param[in] TRANS 
//*> \verbatim 
//*>          TRANS is CHARACTER*1 
//*>          = 'N':  No transpose, apply Q; 
//*>          = 'T':  Transpose, apply Q**T. 
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
//*> \param[in] A 
//*> \verbatim 
//*>          A is REAL array, dimension 
//*>                               (LDA,M) if SIDE = 'L' 
//*>                               (LDA,N) if SIDE = 'R' 
//*>          The vectors which define the elementary reflectors, as 
//*>          returned by SSYTRD. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A. 
//*>          LDA >= max(1,M) if SIDE = 'L'; LDA >= max(1,N) if SIDE = 'R'. 
//*> \endverbatim 
//*> 
//*> \param[in] TAU 
//*> \verbatim 
//*>          TAU is REAL array, dimension 
//*>                               (M-1) if SIDE = 'L' 
//*>                               (N-1) if SIDE = 'R' 
//*>          TAU(i) must contain the scalar factor of the elementary 
//*>          reflector H(i), as returned by SSYTRD. 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is REAL array, dimension (LDC,N) 
//*>          On entry, the M-by-N matrix C. 
//*>          On exit, C is overwritten by Q*C or Q**T*C or C*Q**T or C*Q. 
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
//*>          WORK is REAL array, dimension (MAX(1,LWORK)) 
//*>          On exit, if INFO = 0, WORK(1) returns the optimal LWORK. 
//*> \endverbatim 
//*> 
//*> \param[in] LWORK 
//*> \verbatim 
//*>          LWORK is INTEGER 
//*>          The dimension of the array WORK. 
//*>          If SIDE = 'L', LWORK >= max(1,N); 
//*>          if SIDE = 'R', LWORK >= max(1,M). 
//*>          For optimum performance LWORK >= N*NB if SIDE = 'L', and 
//*>          LWORK >= M*NB if SIDE = 'R', where NB is the optimal 
//*>          blocksize. 
//*> 
//*>          If LWORK = -1, then a workspace query is assumed; the routine 
//*>          only calculates the optimal size of the WORK array, returns 
//*>          this value as the first entry of the WORK array, and no error 
//*>          message related to LWORK is issued by XERBLA. 
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
//*> \ingroup realOTHERcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _7wa9zshq(FString _m2cn2gjg, FString _9wyre9zc, FString _scuo79v4, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, Single* _vxfgpup9, ref Int32 _ocv8fk5c, Single* _0446f4de, Single* _3crf0qn3, ref Int32 _1s3eymp4, Single* _apig8meb, ref Int32 _6fnxzlyp, ref Int32 _gro5yvfo)
	{
#region variable declarations
Boolean _pvwxvshr =  default;
Boolean _lhlgm7z5 =  default;
Boolean _l08igmvf =  default;
Int32 _egqdmelt =  default;
Int32 _8ur10vsh =  default;
Int32 _itfnbz60 =  default;
Int32 _e4ueamrn =  default;
Int32 _31eu052u =  default;
Int32 _q8n03esx =  default;
Int32 _f7059815 =  default;
Int32 _joervqa5 =  default;
Int32 _w6pmxgch =  default;
string fLanavab = default;
#endregion  variable declarations
_m2cn2gjg = _m2cn2gjg.Convert(1);
_9wyre9zc = _9wyre9zc.Convert(1);
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
		_l08igmvf = _w8y2rzgy(_9wyre9zc ,"U" );
		_lhlgm7z5 = (_6fnxzlyp == (int)-1);//* 
		//*     NQ is the order of Q and NW is the minimum dimension of WORK 
		//* 
		
		if (_pvwxvshr)
		{
			
			_joervqa5 = _ev4xhht5;
			_w6pmxgch = _dxpq0xkr;
		}
		else
		{
			
			_joervqa5 = _dxpq0xkr;
			_w6pmxgch = _ev4xhht5;
		}
		
		if ((!(_pvwxvshr)) & (!(_w8y2rzgy(_m2cn2gjg ,"R" ))))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if ((!(_l08igmvf)) & (!(_w8y2rzgy(_9wyre9zc ,"L" ))))
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if ((!(_w8y2rzgy(_scuo79v4 ,"N" ))) & (!(_w8y2rzgy(_scuo79v4 ,"T" ))))
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if (_ev4xhht5 < (int)0)
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if (_dxpq0xkr < (int)0)
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
		else
		if ((_6fnxzlyp < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_w6pmxgch )) & (!(_lhlgm7z5)))
		{
			
			_gro5yvfo = (int)-12;
		}
		//* 
		
		if (_gro5yvfo == (int)0)
		{
			
			if (_l08igmvf)
			{
				
				if (_pvwxvshr)
				{
					
					_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"SORMQL" ,_m2cn2gjg + _scuo79v4 ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref _dxpq0xkr ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef((int)-1) );
				}
				else
				{
					
					_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"SORMQL" ,_m2cn2gjg + _scuo79v4 ,ref _ev4xhht5 ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef((int)-1) );
				}
				
			}
			else
			{
				
				if (_pvwxvshr)
				{
					
					_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"SORMQR" ,_m2cn2gjg + _scuo79v4 ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref _dxpq0xkr ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef((int)-1) );
				}
				else
				{
					
					_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"SORMQR" ,_m2cn2gjg + _scuo79v4 ,ref _ev4xhht5 ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef((int)-1) );
				}
				
			}
			
			_e4ueamrn = (ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_w6pmxgch ) * _f7059815);
			*(_apig8meb+((int)1 - 1)) = REAL(_e4ueamrn);
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("SORMTR" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		else
		if (_lhlgm7z5)
		{
			
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if (((_ev4xhht5 == (int)0) | (_dxpq0xkr == (int)0)) | (_joervqa5 == (int)1))
		{
			
			*(_apig8meb+((int)1 - 1)) = REAL((int)1);
			return;
		}
		//* 
		
		if (_pvwxvshr)
		{
			
			_31eu052u = (_ev4xhht5 - (int)1);
			_q8n03esx = _dxpq0xkr;
		}
		else
		{
			
			_31eu052u = _ev4xhht5;
			_q8n03esx = (_dxpq0xkr - (int)1);
		}
		//* 
		
		if (_l08igmvf)
		{
			//* 
			//*        Q was determined by a call to SSYTRD with UPLO = 'U' 
			//* 
			
			_8ena82is(_m2cn2gjg ,_scuo79v4 ,ref _31eu052u ,ref _q8n03esx ,ref Unsafe.AsRef(_joervqa5 - (int)1) ,(_vxfgpup9+((int)1 - 1) + ((int)2 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_0446f4de ,_3crf0qn3 ,ref _1s3eymp4 ,_apig8meb ,ref _6fnxzlyp ,ref _itfnbz60 );
		}
		else
		{
			//* 
			//*        Q was determined by a call to SSYTRD with UPLO = 'L' 
			//* 
			
			if (_pvwxvshr)
			{
				
				_egqdmelt = (int)2;
				_8ur10vsh = (int)1;
			}
			else
			{
				
				_egqdmelt = (int)1;
				_8ur10vsh = (int)2;
			}
			
			_yzo7hbs3(_m2cn2gjg ,_scuo79v4 ,ref _31eu052u ,ref _q8n03esx ,ref Unsafe.AsRef(_joervqa5 - (int)1) ,(_vxfgpup9+((int)2 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_0446f4de ,(_3crf0qn3+(_egqdmelt - 1) + (_8ur10vsh - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,_apig8meb ,ref _6fnxzlyp ,ref _itfnbz60 );
		}
		
		*(_apig8meb+((int)1 - 1)) = REAL(_e4ueamrn);
		return;//* 
		//*     End of SORMTR 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
