
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
//*> \brief \b CROT applies a plane rotation with real cosine and complex sine to a pair of complex vectors. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CROT + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/crot.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/crot.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/crot.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CROT( N, CX, INCX, CY, INCY, C, S ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INCX, INCY, N 
//*       REAL               C 
//*       COMPLEX            S 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX            CX( * ), CY( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CROT   applies a plane rotation, where the cos (C) is real and the 
//*> sin (S) is complex, and the vectors CX and CY are complex. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of elements in the vectors CX and CY. 
//*> \endverbatim 
//*> 
//*> \param[in,out] CX 
//*> \verbatim 
//*>          CX is COMPLEX array, dimension (N) 
//*>          On input, the vector X. 
//*>          On output, CX is overwritten with C*X + S*Y. 
//*> \endverbatim 
//*> 
//*> \param[in] INCX 
//*> \verbatim 
//*>          INCX is INTEGER 
//*>          The increment between successive values of CY.  INCX <> 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] CY 
//*> \verbatim 
//*>          CY is COMPLEX array, dimension (N) 
//*>          On input, the vector Y. 
//*>          On output, CY is overwritten with -CONJG(S)*X + C*Y. 
//*> \endverbatim 
//*> 
//*> \param[in] INCY 
//*> \verbatim 
//*>          INCY is INTEGER 
//*>          The increment between successive values of CY.  INCX <> 0. 
//*> \endverbatim 
//*> 
//*> \param[in] C 
//*> \verbatim 
//*>          C is REAL 
//*> \endverbatim 
//*> 
//*> \param[in] S 
//*> \verbatim 
//*>          S is COMPLEX 
//*>          C and S define a rotation 
//*>             [  C          S  ] 
//*>             [ -conjg(S)   C  ] 
//*>          where C*C + S*CONJG(S) = 1.0. 
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
//*> \ingroup complexOTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _t5n7kuvb(ref Int32 _dxpq0xkr, fcomplex* _c9wzt3lw, ref Int32 _1eqjusqc, fcomplex* _8czizhxc, ref Int32 _bbrxgmj7, ref Single _3crf0qn3, ref fcomplex _irk8i6qr)
	{
#region variable declarations
Int32 _b5p6od9s =  default;
Int32 _b69ritwm =  default;
Int32 _821h5yui =  default;
fcomplex _chiot9on =  default;
string fLanavab = default;
#endregion  variable declarations

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
		//* ===================================================================== 
		//* 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		if (_dxpq0xkr <= (int)0)
		return;
		if ((_1eqjusqc == (int)1) & (_bbrxgmj7 == (int)1))goto Mark20;//* 
		//*     Code for unequal increments or equal increments not equal to 1 
		//* 
		
		_b69ritwm = (int)1;
		_821h5yui = (int)1;
		if (_1eqjusqc < (int)0)
		_b69ritwm = ((((-(_dxpq0xkr)) + (int)1) * _1eqjusqc) + (int)1);
		if (_bbrxgmj7 < (int)0)
		_821h5yui = ((((-(_dxpq0xkr)) + (int)1) * _bbrxgmj7) + (int)1);
		{
			System.Int32 __81fgg2dlsvn2554 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2554 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2554;
			for (__81fgg2count2554 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2554 + __81fgg2step2554) / __81fgg2step2554)), _b5p6od9s = __81fgg2dlsvn2554; __81fgg2count2554 != 0; __81fgg2count2554--, _b5p6od9s += (__81fgg2step2554)) {

			{
				
				_chiot9on = ((_3crf0qn3 * *(_c9wzt3lw+(_b69ritwm - 1))) + (_irk8i6qr * *(_8czizhxc+(_821h5yui - 1))));
				*(_8czizhxc+(_821h5yui - 1)) = ((_3crf0qn3 * *(_8czizhxc+(_821h5yui - 1))) - (ILNumerics.F2NET.Intrinsics.CONJG(_irk8i6qr ) * *(_c9wzt3lw+(_b69ritwm - 1))));
				*(_c9wzt3lw+(_b69ritwm - 1)) = _chiot9on;
				_b69ritwm = (_b69ritwm + _1eqjusqc);
				_821h5yui = (_821h5yui + _bbrxgmj7);
Mark10:;
				// continue
			}
						}		}
		return;//* 
		//*     Code for both increments equal to 1 
		//* 
		
Mark20:;
		// continue
		{
			System.Int32 __81fgg2dlsvn2555 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2555 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2555;
			for (__81fgg2count2555 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2555 + __81fgg2step2555) / __81fgg2step2555)), _b5p6od9s = __81fgg2dlsvn2555; __81fgg2count2555 != 0; __81fgg2count2555--, _b5p6od9s += (__81fgg2step2555)) {

			{
				
				_chiot9on = ((_3crf0qn3 * *(_c9wzt3lw+(_b5p6od9s - 1))) + (_irk8i6qr * *(_8czizhxc+(_b5p6od9s - 1))));
				*(_8czizhxc+(_b5p6od9s - 1)) = ((_3crf0qn3 * *(_8czizhxc+(_b5p6od9s - 1))) - (ILNumerics.F2NET.Intrinsics.CONJG(_irk8i6qr ) * *(_c9wzt3lw+(_b5p6od9s - 1))));
				*(_c9wzt3lw+(_b5p6od9s - 1)) = _chiot9on;
Mark30:;
				// continue
			}
						}		}
		return;
	}
	
	} // 177

} // end class 
} // end namespace
#endif
