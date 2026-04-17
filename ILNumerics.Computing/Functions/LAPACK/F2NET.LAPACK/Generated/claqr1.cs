
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
//*> \brief \b CLAQR1 sets a scalar multiple of the first column of the product of 2-by-2 or 3-by-3 matrix H and specified shifts. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CLAQR1 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/claqr1.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/claqr1.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/claqr1.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CLAQR1( N, H, LDH, S1, S2, V ) 
//* 
//*       .. Scalar Arguments .. 
//*       COMPLEX            S1, S2 
//*       INTEGER            LDH, N 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX            H( LDH, * ), V( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*>      Given a 2-by-2 or 3-by-3 matrix H, CLAQR1 sets v to a 
//*>      scalar multiple of the first column of the product 
//*> 
//*>      (*)  K = (H - s1*I)*(H - s2*I) 
//*> 
//*>      scaling to avoid overflows and most underflows. 
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
//*>          H is COMPLEX array, dimension (LDH,N) 
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
//*> \param[in] S1 
//*> \verbatim 
//*>          S1 is COMPLEX 
//*> \endverbatim 
//*> 
//*> \param[in] S2 
//*> \verbatim 
//*>          S2 is COMPLEX 
//*> 
//*>          S1 and S2 are the shifts defining K in (*) above. 
//*> \endverbatim 
//*> 
//*> \param[out] V 
//*> \verbatim 
//*>          V is COMPLEX array, dimension (N) 
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
//*> \ingroup complexOTHERauxiliary 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>       Karen Braman and Ralph Byers, Department of Mathematics, 
//*>       University of Kansas, USA 
//*> 
//*  ===================================================================== 

	 
	public static void _79c4tofo(ref Int32 _dxpq0xkr, fcomplex* _ogkjl6gu, ref Int32 _1iekxpnw, ref fcomplex _fmb4u5ka, ref fcomplex _slkbnmvx, fcomplex* _ycxba85s)
	{
#region variable declarations
fcomplex _d0547bi2 =   new fcomplex(0f,0f);
Single _7cb2gzzt =  0f;
fcomplex _n7plx4io =  default;
fcomplex _3rsvj09v =  default;
fcomplex _u9ugq0yb =  default;
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
		//*     .. Statement Functions .. 
		//*     .. 
		//*     .. Statement Function definitions .. 
		
		
		Func<fcomplex,Single> _4jqx89by = (_a94616nn) => { return (ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.REAL(_a94616nn ) ) + ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.AIMAG(_a94616nn ) )); };;//*     .. 
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
			
			_irk8i6qr = (_4jqx89by(*(_ogkjl6gu+((int)1 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) - _slkbnmvx ) + _4jqx89by(*(_ogkjl6gu+((int)2 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) ));
			if (_irk8i6qr == _7cb2gzzt)
			{
				
				*(_ycxba85s+((int)1 - 1)) = _d0547bi2;
				*(_ycxba85s+((int)2 - 1)) = _d0547bi2;
			}
			else
			{
				
				_3rsvj09v = (*(_ogkjl6gu+((int)2 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) / _irk8i6qr);
				*(_ycxba85s+((int)1 - 1)) = ((_3rsvj09v * *(_ogkjl6gu+((int)1 - 1) + ((int)2 - 1) * 1 * (_1iekxpnw))) + ((*(_ogkjl6gu+((int)1 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) - _fmb4u5ka) * ((*(_ogkjl6gu+((int)1 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) - _slkbnmvx) / _irk8i6qr)));
				*(_ycxba85s+((int)2 - 1)) = (_3rsvj09v * (((*(_ogkjl6gu+((int)1 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) + *(_ogkjl6gu+((int)2 - 1) + ((int)2 - 1) * 1 * (_1iekxpnw))) - _fmb4u5ka) - _slkbnmvx));
			}
			
		}
		else
		{
			
			_irk8i6qr = ((_4jqx89by(*(_ogkjl6gu+((int)1 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) - _slkbnmvx ) + _4jqx89by(*(_ogkjl6gu+((int)2 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) )) + _4jqx89by(*(_ogkjl6gu+((int)3 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) ));
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
				*(_ycxba85s+((int)1 - 1)) = ((((*(_ogkjl6gu+((int)1 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) - _fmb4u5ka) * ((*(_ogkjl6gu+((int)1 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) - _slkbnmvx) / _irk8i6qr)) + (*(_ogkjl6gu+((int)1 - 1) + ((int)2 - 1) * 1 * (_1iekxpnw)) * _3rsvj09v)) + (*(_ogkjl6gu+((int)1 - 1) + ((int)3 - 1) * 1 * (_1iekxpnw)) * _u9ugq0yb));
				*(_ycxba85s+((int)2 - 1)) = ((_3rsvj09v * (((*(_ogkjl6gu+((int)1 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) + *(_ogkjl6gu+((int)2 - 1) + ((int)2 - 1) * 1 * (_1iekxpnw))) - _fmb4u5ka) - _slkbnmvx)) + (*(_ogkjl6gu+((int)2 - 1) + ((int)3 - 1) * 1 * (_1iekxpnw)) * _u9ugq0yb));
				*(_ycxba85s+((int)3 - 1)) = ((_u9ugq0yb * (((*(_ogkjl6gu+((int)1 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)) + *(_ogkjl6gu+((int)3 - 1) + ((int)3 - 1) * 1 * (_1iekxpnw))) - _fmb4u5ka) - _slkbnmvx)) + (_3rsvj09v * *(_ogkjl6gu+((int)3 - 1) + ((int)2 - 1) * 1 * (_1iekxpnw))));
			}
			
		}
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
