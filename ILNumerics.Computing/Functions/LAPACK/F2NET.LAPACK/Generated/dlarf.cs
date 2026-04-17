
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
//*> \brief \b DLARF applies an elementary reflector to a general rectangular matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLARF + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlarf.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlarf.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlarf.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLARF( SIDE, M, N, V, INCV, TAU, C, LDC, WORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          SIDE 
//*       INTEGER            INCV, LDC, M, N 
//*       DOUBLE PRECISION   TAU 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   C( LDC, * ), V( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLARF applies a real elementary reflector H to a real m by n matrix 
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
//*>          V is DOUBLE PRECISION array, dimension 
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
//*>          TAU is DOUBLE PRECISION 
//*>          The value tau in the representation of H. 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is DOUBLE PRECISION array, dimension (LDC,N) 
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
//*>          WORK is DOUBLE PRECISION array, dimension 
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
//*> \ingroup doubleOTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _bbpftela(FString _m2cn2gjg, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, Double* _ycxba85s, ref Int32 _un5zhi97, ref Double _0446f4de, Double* _3crf0qn3, ref Int32 _1s3eymp4, Double* _apig8meb)
	{
#region variable declarations
Double _kxg5drh2 =  1d;
Double _d0547bi2 =  0d;
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
				
				_mfjw2q1q = _uqrs8y52(ref _thvhilfl ,ref _dxpq0xkr ,_3crf0qn3 ,ref _1s3eymp4 );
			}
			else
			{
				//!     Scan for the last non-zero row in C(:,1:lastv). 
				
				_mfjw2q1q = _63ct3f4w(ref _ev4xhht5 ,ref _thvhilfl ,_3crf0qn3 ,ref _1s3eymp4 );
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
				
				_t5wmtd1j("Transpose" ,ref _thvhilfl ,ref _mfjw2q1q ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 ,_ycxba85s ,ref _un5zhi97 ,ref Unsafe.AsRef(_d0547bi2) ,_apig8meb ,ref Unsafe.AsRef((int)1) );//* 
				//*           C(1:lastv,1:lastc) := C(...) - v(1:lastv,1) * w(1:lastc,1)**T 
				//* 
				
				_humb8nf1(ref _thvhilfl ,ref _mfjw2q1q ,ref Unsafe.AsRef(-(_0446f4de)) ,_ycxba85s ,ref _un5zhi97 ,_apig8meb ,ref Unsafe.AsRef((int)1) ,_3crf0qn3 ,ref _1s3eymp4 );
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
				
				_t5wmtd1j("No transpose" ,ref _mfjw2q1q ,ref _thvhilfl ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 ,_ycxba85s ,ref _un5zhi97 ,ref Unsafe.AsRef(_d0547bi2) ,_apig8meb ,ref Unsafe.AsRef((int)1) );//* 
				//*           C(1:lastc,1:lastv) := C(...) - w(1:lastc,1) * v(1:lastv,1)**T 
				//* 
				
				_humb8nf1(ref _mfjw2q1q ,ref _thvhilfl ,ref Unsafe.AsRef(-(_0446f4de)) ,_apig8meb ,ref Unsafe.AsRef((int)1) ,_ycxba85s ,ref _un5zhi97 ,_3crf0qn3 ,ref _1s3eymp4 );
			}
			
		}
		
		return;//* 
		//*     End of DLARF 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
