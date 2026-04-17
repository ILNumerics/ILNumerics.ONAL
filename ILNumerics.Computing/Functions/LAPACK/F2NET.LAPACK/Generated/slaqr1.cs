
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
//*> \brief \b SLAQR1 sets a scalar multiple of the first column of the product of 2-by-2 or 3-by-3 matrix H and specified shifts. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLAQR1 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slaqr1.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slaqr1.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slaqr1.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLAQR1( N, H, LDH, SR1, SI1, SR2, SI2, V ) 
//* 
//*       .. Scalar Arguments .. 
//*       REAL               SI1, SI2, SR1, SR2 
//*       INTEGER            LDH, N 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               H( LDH, * ), V( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*>      Given a 2-by-2 or 3-by-3 matrix H, SLAQR1 sets v to a 
//*>      scalar multiple of the first column of the product 
//*> 
//*>      (*)  K = (H - (sr1 + i*si1)*I)*(H - (sr2 + i*si2)*I) 
//*> 
//*>      scaling to avoid overflows and most underflows. It 
//*>      is assumed that either 
//*> 
//*>              1) sr1 = sr2 and si1 = -si2 
//*>          or 
//*>              2) si1 = si2 = 0. 
//*> 
//*>      This is useful for starting double implicit shift bulges 
//*>      in the QR algorithm. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>              Order of the matrix H. N must be either 2 or 3. 
//*> \endverbatim 
//*> 
//*> \param[in] H 
//*> \verbatim 
//*>          H is REAL array, dimension (LDH,N) 
//*>              The 2-by-2 or 3-by-3 matrix H in (*). 
//*> \endverbatim 
//*> 
//*> \param[in] LDH 
//*> \verbatim 
//*>          LDH is INTEGER 
//*>              The leading dimension of H as declared in 
//*>              the calling procedure.  LDH >= N 
//*> \endverbatim 
//*> 
//*> \param[in] SR1 
//*> \verbatim 
//*>          SR1 is REAL 
//*> \endverbatim 
//*> 
//*> \param[in] SI1 
//*> \verbatim 
//*>          SI1 is REAL 
//*> \endverbatim 
//*> 
//*> \param[in] SR2 
//*> \verbatim 
//*>          SR2 is REAL 
//*> \endverbatim 
//*> 
//*> \param[in] SI2 
//*> \verbatim 
//*>          SI2 is REAL 
//*>              The shifts in (*). 
//*> \endverbatim 
//*> 
//*> \param[out] V 
//*> \verbatim 
//*>          V is REAL array, dimension (N) 
//*>              A scalar multiple of the first column of the 
//*>              matrix K in (*). 
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
//*> \date June 2017 
//* 
//*> \ingroup realOTHERauxiliary 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>       Karen Braman and Ralph Byers, Department of Mathematics, 
//*>       University of Kansas, USA 
//*> 
//*  ===================================================================== 

	 
	public static void _jg99g0io(ref Int32 _dxpq0xkr, Single* _ogkjl6gu, ref Int32 _1iekxpnw, ref Single _xjebioqh, ref Single _0na05cuo, ref Single _3y6zsooz, ref Single _amnhwjgr, Single* _ycxba85s)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Single _3rsvj09v =  default;
Single _u9ugq0yb =  default;
Single _irk8i6qr =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.1) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     June 2017 
		//* 
		//*     .. Scalar Arguments .. 
		//*     .. 
		//*     .. Array Arguments .. 
		//*     .. 
		//* 
		//*  ================================================================ 
		//* 
		//*     .. Parameters .. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Quick return if possible 
		//* 
		
		if ((_dxpq0xkr != (int)2) & (_dxpq0xkr != (int)3))
		{
			
			return;
		}
		//* 
		
		if (_dxpq0xkr == (int)2)
		{
			
			_irk8i6qr = ((ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+((int)1 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) - _3y6zsooz ) + ILNumerics.F2NET.Intrinsics.ABS(_amnhwjgr )) + ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+((int)2 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) ));
			if (_irk8i6qr == _d0547bi2)
			{
				
				*(_ycxba85s+((int)1 - 1)) = _d0547bi2;
				*(_ycxba85s+((int)2 - 1)) = _d0547bi2;
			}
			else
			{
				
				_3rsvj09v = (*(_ogkjl6gu+((int)2 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) / _irk8i6qr);
				*(_ycxba85s+((int)1 - 1)) = (((_3rsvj09v * *(_ogkjl6gu+((int)1 - 1) + ((int)2 - 1) * 1 * (_1iekxpnw))) + ((*(_ogkjl6gu+((int)1 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) - _xjebioqh) * ((*(_ogkjl6gu+((int)1 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) - _3y6zsooz) / _irk8i6qr))) - (_0na05cuo * (_amnhwjgr / _irk8i6qr)));
				*(_ycxba85s+((int)2 - 1)) = (_3rsvj09v * (((*(_ogkjl6gu+((int)1 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) + *(_ogkjl6gu+((int)2 - 1) + ((int)2 - 1) * 1 * (_1iekxpnw))) - _xjebioqh) - _3y6zsooz));
			}
			
		}
		else
		{
			
			_irk8i6qr = (((ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+((int)1 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) - _3y6zsooz ) + ILNumerics.F2NET.Intrinsics.ABS(_amnhwjgr )) + ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+((int)2 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) )) + ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+((int)3 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) ));
			if (_irk8i6qr == _d0547bi2)
			{
				
				*(_ycxba85s+((int)1 - 1)) = _d0547bi2;
				*(_ycxba85s+((int)2 - 1)) = _d0547bi2;
				*(_ycxba85s+((int)3 - 1)) = _d0547bi2;
			}
			else
			{
				
				_3rsvj09v = (*(_ogkjl6gu+((int)2 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) / _irk8i6qr);
				_u9ugq0yb = (*(_ogkjl6gu+((int)3 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) / _irk8i6qr);
				*(_ycxba85s+((int)1 - 1)) = (((((*(_ogkjl6gu+((int)1 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) - _xjebioqh) * ((*(_ogkjl6gu+((int)1 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) - _3y6zsooz) / _irk8i6qr)) - (_0na05cuo * (_amnhwjgr / _irk8i6qr))) + (*(_ogkjl6gu+((int)1 - 1) + ((int)2 - 1) * 1 * (_1iekxpnw)) * _3rsvj09v)) + (*(_ogkjl6gu+((int)1 - 1) + ((int)3 - 1) * 1 * (_1iekxpnw)) * _u9ugq0yb));
				*(_ycxba85s+((int)2 - 1)) = ((_3rsvj09v * (((*(_ogkjl6gu+((int)1 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) + *(_ogkjl6gu+((int)2 - 1) + ((int)2 - 1) * 1 * (_1iekxpnw))) - _xjebioqh) - _3y6zsooz)) + (*(_ogkjl6gu+((int)2 - 1) + ((int)3 - 1) * 1 * (_1iekxpnw)) * _u9ugq0yb));
				*(_ycxba85s+((int)3 - 1)) = ((_u9ugq0yb * (((*(_ogkjl6gu+((int)1 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) + *(_ogkjl6gu+((int)3 - 1) + ((int)3 - 1) * 1 * (_1iekxpnw))) - _xjebioqh) - _3y6zsooz)) + (_3rsvj09v * *(_ogkjl6gu+((int)3 - 1) + ((int)2 - 1) * 1 * (_1iekxpnw))));
			}
			
		}
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
