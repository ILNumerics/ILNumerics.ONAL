
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
//*> \brief \b DORMR3 multiplies a general matrix by the orthogonal matrix from a RZ factorization determined by stzrzf (unblocked algorithm). 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DORMR3 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dormr3.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dormr3.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dormr3.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DORMR3( SIDE, TRANS, M, N, K, L, A, LDA, TAU, C, LDC, 
//*                          WORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          SIDE, TRANS 
//*       INTEGER            INFO, K, L, LDA, LDC, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   A( LDA, * ), C( LDC, * ), TAU( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DORMR3 overwrites the general real m by n matrix C with 
//*> 
//*>       Q * C  if SIDE = 'L' and TRANS = 'N', or 
//*> 
//*>       Q**T* C  if SIDE = 'L' and TRANS = 'C', or 
//*> 
//*>       C * Q  if SIDE = 'R' and TRANS = 'N', or 
//*> 
//*>       C * Q**T if SIDE = 'R' and TRANS = 'C', 
//*> 
//*> where Q is a real orthogonal matrix defined as the product of k 
//*> elementary reflectors 
//*> 
//*>       Q = H(1) H(2) . . . H(k) 
//*> 
//*> as returned by DTZRZF. Q is of order m if SIDE = 'L' and of order n 
//*> if SIDE = 'R'. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] SIDE 
//*> \verbatim 
//*>          SIDE is CHARACTER*1 
//*>          = 'L': apply Q or Q**T from the Left 
//*>          = 'R': apply Q or Q**T from the Right 
//*> \endverbatim 
//*> 
//*> \param[in] TRANS 
//*> \verbatim 
//*>          TRANS is CHARACTER*1 
//*>          = 'N': apply Q  (No transpose) 
//*>          = 'T': apply Q**T (Transpose) 
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
//*> \param[in] L 
//*> \verbatim 
//*>          L is INTEGER 
//*>          The number of columns of the matrix A containing 
//*>          the meaningful part of the Householder reflectors. 
//*>          If SIDE = 'L', M >= L >= 0, if SIDE = 'R', N >= L >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is DOUBLE PRECISION array, dimension 
//*>                               (LDA,M) if SIDE = 'L', 
//*>                               (LDA,N) if SIDE = 'R' 
//*>          The i-th row must contain the vector which defines the 
//*>          elementary reflector H(i), for i = 1,2,...,k, as returned by 
//*>          DTZRZF in the last k rows of its array argument A. 
//*>          A is modified by the routine but restored on exit. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A. LDA >= max(1,K). 
//*> \endverbatim 
//*> 
//*> \param[in] TAU 
//*> \verbatim 
//*>          TAU is DOUBLE PRECISION array, dimension (K) 
//*>          TAU(i) must contain the scalar factor of the elementary 
//*>          reflector H(i), as returned by DTZRZF. 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is DOUBLE PRECISION array, dimension (LDC,N) 
//*>          On entry, the m-by-n matrix C. 
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
//*>          WORK is DOUBLE PRECISION array, dimension 
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
//*> \ingroup doubleOTHERcomputational 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>    A. Petitet, Computer Science Dept., Univ. of Tenn., Knoxville, USA 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _mir8pchr(FString _m2cn2gjg, FString _scuo79v4, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, ref Int32 _68ec3gbh, Double* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _0446f4de, Double* _3crf0qn3, ref Int32 _1s3eymp4, Double* _apig8meb, ref Int32 _gro5yvfo)
	{
#region variable declarations
Boolean _pvwxvshr =  default;
Boolean _2bzw4gjb =  default;
Int32 _b5p6od9s =  default;
Int32 _egqdmelt =  default;
Int32 _8ur10vsh =  default;
Int32 _b3a707ow =  default;
Int32 _8jzcrkri =  default;
Int32 _fpznay24 =  default;
Int32 _aynldcwj =  default;
Int32 _31eu052u =  default;
Int32 _q8n03esx =  default;
Int32 _joervqa5 =  default;
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
		if ((!(_2bzw4gjb)) & (!(_w8y2rzgy(_scuo79v4 ,"T" ))))
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
		if (((_68ec3gbh < (int)0) | (_pvwxvshr & (_68ec3gbh > _ev4xhht5))) | ((!(_pvwxvshr)) & (_68ec3gbh > _dxpq0xkr)))
		{
			
			_gro5yvfo = (int)-6;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_umlkckdg ))
		{
			
			_gro5yvfo = (int)-8;
		}
		else
		if (_1s3eymp4 < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ))
		{
			
			_gro5yvfo = (int)-11;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("DORMR3" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
			_fpznay24 = ((_ev4xhht5 - _68ec3gbh) + (int)1);
			_aynldcwj = (int)1;
		}
		else
		{
			
			_31eu052u = _ev4xhht5;
			_fpznay24 = ((_dxpq0xkr - _68ec3gbh) + (int)1);
			_8jzcrkri = (int)1;
		}
		//* 
		
		{
			System.Int32 __81fgg2dlsvn2086 = (System.Int32)(_egqdmelt);
			System.Int32 __81fgg2step2086 = (System.Int32)(_b3a707ow);
			System.Int32 __81fgg2count2086;
			for (__81fgg2count2086 = System.Math.Max(0, (System.Int32)(((System.Int32)(_8ur10vsh) - __81fgg2dlsvn2086 + __81fgg2step2086) / __81fgg2step2086)), _b5p6od9s = __81fgg2dlsvn2086; __81fgg2count2086 != 0; __81fgg2count2086--, _b5p6od9s += (__81fgg2step2086)) {

			{
				
				if (_pvwxvshr)
				{
					//* 
					//*           H(i) or H(i)**T is applied to C(i:m,1:n) 
					//* 
					
					_31eu052u = ((_ev4xhht5 - _b5p6od9s) + (int)1);
					_8jzcrkri = _b5p6od9s;
				}
				else
				{
					//* 
					//*           H(i) or H(i)**T is applied to C(1:m,i:n) 
					//* 
					
					_q8n03esx = ((_dxpq0xkr - _b5p6od9s) + (int)1);
					_aynldcwj = _b5p6od9s;
				}
				//* 
				//*        Apply H(i) or H(i)**T 
				//* 
				
				_qtvbvdij(_m2cn2gjg ,ref _31eu052u ,ref _q8n03esx ,ref _68ec3gbh ,(_vxfgpup9+(_b5p6od9s - 1) + (_fpznay24 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(*(_0446f4de+(_b5p6od9s - 1))) ,(_3crf0qn3+(_8jzcrkri - 1) + (_aynldcwj - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,_apig8meb );//* 
				
Mark10:;
				// continue
			}
						}		}//* 
		
		return;//* 
		//*     End of DORMR3 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
