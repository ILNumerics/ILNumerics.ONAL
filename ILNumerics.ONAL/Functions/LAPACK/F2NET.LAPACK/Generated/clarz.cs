
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
//*> \brief \b CLARZ applies an elementary reflector (as returned by stzrzf) to a general matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CLARZ + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/clarz.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/clarz.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/clarz.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CLARZ( SIDE, M, N, L, V, INCV, TAU, C, LDC, WORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          SIDE 
//*       INTEGER            INCV, L, LDC, M, N 
//*       COMPLEX            TAU 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX            C( LDC, * ), V( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CLARZ applies a complex elementary reflector H to a complex 
//*> M-by-N matrix C, from either the left or the right. H is represented 
//*> in the form 
//*> 
//*>       H = I - tau * v * v**H 
//*> 
//*> where tau is a complex scalar and v is a complex vector. 
//*> 
//*> If tau = 0, then H is taken to be the unit matrix. 
//*> 
//*> To apply H**H (the conjugate transpose of H), supply conjg(tau) instead 
//*> tau. 
//*> 
//*> H is a product of k elementary reflectors as returned by CTZRZF. 
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
//*> \param[in] L 
//*> \verbatim 
//*>          L is INTEGER 
//*>          The number of entries of the vector V containing 
//*>          the meaningful part of the Householder vectors. 
//*>          If SIDE = 'L', M >= L >= 0, if SIDE = 'R', N >= L >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] V 
//*> \verbatim 
//*>          V is COMPLEX array, dimension (1+(L-1)*abs(INCV)) 
//*>          The vector v in the representation of H as returned by 
//*>          CTZRZF. V is not used if TAU = 0. 
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
//*>          TAU is COMPLEX 
//*>          The value tau in the representation of H. 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is COMPLEX array, dimension (LDC,N) 
//*>          On entry, the M-by-N matrix C. 
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
//*>          WORK is COMPLEX array, dimension 
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
//*> \ingroup complexOTHERcomputational 
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

	 
	public static void _weq8yonr(FString _m2cn2gjg, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _68ec3gbh, fcomplex* _ycxba85s, ref Int32 _un5zhi97, ref fcomplex _0446f4de, fcomplex* _3crf0qn3, ref Int32 _1s3eymp4, fcomplex* _apig8meb)
	{
#region variable declarations
fcomplex _kxg5drh2 =   new fcomplex(1f,0f);
fcomplex _d0547bi2 =   new fcomplex(0f,0f);
string fLanavab = default;
#endregion  variable declarations
_m2cn2gjg = _m2cn2gjg.Convert(1);

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
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		if (_w8y2rzgy(_m2cn2gjg ,"L" ))
		{
			//* 
			//*        Form  H * C 
			//* 
			
			if (_0446f4de != _d0547bi2)
			{
				//* 
				//*           w( 1:n ) = conjg( C( 1, 1:n ) ) 
				//* 
				
				_33e0jk6i(ref _dxpq0xkr ,_3crf0qn3 ,ref _1s3eymp4 ,_apig8meb ,ref Unsafe.AsRef((int)1) );
				_png2g84j(ref _dxpq0xkr ,_apig8meb ,ref Unsafe.AsRef((int)1) );//* 
				//*           w( 1:n ) = conjg( w( 1:n ) + C( m-l+1:m, 1:n )**H * v( 1:l ) ) 
				//* 
				
				_f0oh3lvv("Conjugate transpose" ,ref _68ec3gbh ,ref _dxpq0xkr ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+((_ev4xhht5 - _68ec3gbh) + (int)1 - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,_ycxba85s ,ref _un5zhi97 ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref Unsafe.AsRef((int)1) );
				_png2g84j(ref _dxpq0xkr ,_apig8meb ,ref Unsafe.AsRef((int)1) );//* 
				//*           C( 1, 1:n ) = C( 1, 1:n ) - tau * w( 1:n ) 
				//* 
				
				_0vz4nsob(ref _dxpq0xkr ,ref Unsafe.AsRef(-(_0446f4de)) ,_apig8meb ,ref Unsafe.AsRef((int)1) ,_3crf0qn3 ,ref _1s3eymp4 );//* 
				//*           C( m-l+1:m, 1:n ) = C( m-l+1:m, 1:n ) - ... 
				//*                               tau * v( 1:l ) * w( 1:n )**H 
				//* 
				
				_audfhame(ref _68ec3gbh ,ref _dxpq0xkr ,ref Unsafe.AsRef(-(_0446f4de)) ,_ycxba85s ,ref _un5zhi97 ,_apig8meb ,ref Unsafe.AsRef((int)1) ,(_3crf0qn3+((_ev4xhht5 - _68ec3gbh) + (int)1 - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );
			}
			//* 
			
		}
		else
		{
			//* 
			//*        Form  C * H 
			//* 
			
			if (_0446f4de != _d0547bi2)
			{
				//* 
				//*           w( 1:m ) = C( 1:m, 1 ) 
				//* 
				
				_33e0jk6i(ref _ev4xhht5 ,_3crf0qn3 ,ref Unsafe.AsRef((int)1) ,_apig8meb ,ref Unsafe.AsRef((int)1) );//* 
				//*           w( 1:m ) = w( 1:m ) + C( 1:m, n-l+1:n, 1:n ) * v( 1:l ) 
				//* 
				
				_f0oh3lvv("No transpose" ,ref _ev4xhht5 ,ref _68ec3gbh ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+((int)1 - 1) + ((_dxpq0xkr - _68ec3gbh) + (int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,_ycxba85s ,ref _un5zhi97 ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref Unsafe.AsRef((int)1) );//* 
				//*           C( 1:m, 1 ) = C( 1:m, 1 ) - tau * w( 1:m ) 
				//* 
				
				_0vz4nsob(ref _ev4xhht5 ,ref Unsafe.AsRef(-(_0446f4de)) ,_apig8meb ,ref Unsafe.AsRef((int)1) ,_3crf0qn3 ,ref Unsafe.AsRef((int)1) );//* 
				//*           C( 1:m, n-l+1:n ) = C( 1:m, n-l+1:n ) - ... 
				//*                               tau * w( 1:m ) * v( 1:l )**H 
				//* 
				
				_b59d4z51(ref _ev4xhht5 ,ref _68ec3gbh ,ref Unsafe.AsRef(-(_0446f4de)) ,_apig8meb ,ref Unsafe.AsRef((int)1) ,_ycxba85s ,ref _un5zhi97 ,(_3crf0qn3+((int)1 - 1) + ((_dxpq0xkr - _68ec3gbh) + (int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );//* 
				
			}
			//* 
			
		}
		//* 
		
		return;//* 
		//*     End of CLARZ 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
