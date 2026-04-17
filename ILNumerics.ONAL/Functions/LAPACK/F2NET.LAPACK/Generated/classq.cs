
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
//*> \brief \b CLASSQ updates a sum of squares represented in scaled form. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CLASSQ + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/classq.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/classq.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/classq.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CLASSQ( N, X, INCX, SCALE, SUMSQ ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INCX, N 
//*       REAL               SCALE, SUMSQ 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX            X( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CLASSQ returns the values scl and ssq such that 
//*> 
//*>    ( scl**2 )*ssq = x( 1 )**2 +...+ x( n )**2 + ( scale**2 )*sumsq, 
//*> 
//*> where x( i ) = abs( X( 1 + ( i - 1 )*INCX ) ). The value of sumsq is 
//*> assumed to be at least unity and the value of ssq will then satisfy 
//*> 
//*>    1.0 <= ssq <= ( sumsq + 2*n ). 
//*> 
//*> scale is assumed to be non-negative and scl returns the value 
//*> 
//*>    scl = max( scale, abs( real( x( i ) ) ), abs( aimag( x( i ) ) ) ), 
//*>           i 
//*> 
//*> scale and sumsq must be supplied in SCALE and SUMSQ respectively. 
//*> SCALE and SUMSQ are overwritten by scl and ssq respectively. 
//*> 
//*> The routine makes only one pass through the vector X. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of elements to be used from the vector X. 
//*> \endverbatim 
//*> 
//*> \param[in] X 
//*> \verbatim 
//*>          X is COMPLEX array, dimension (1+(N-1)*INCX) 
//*>          The vector x as described above. 
//*>             x( i )  = X( 1 + ( i - 1 )*INCX ), 1 <= i <= n. 
//*> \endverbatim 
//*> 
//*> \param[in] INCX 
//*> \verbatim 
//*>          INCX is INTEGER 
//*>          The increment between successive values of the vector X. 
//*>          INCX > 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] SCALE 
//*> \verbatim 
//*>          SCALE is REAL 
//*>          On entry, the value  scale  in the equation above. 
//*>          On exit, SCALE is overwritten with the value  scl . 
//*> \endverbatim 
//*> 
//*> \param[in,out] SUMSQ 
//*> \verbatim 
//*>          SUMSQ is REAL 
//*>          On entry, the value  sumsq  in the equation above. 
//*>          On exit, SUMSQ is overwritten with the value  ssq . 
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

	 
	public static void _bz1hd2n9(ref Int32 _dxpq0xkr, fcomplex* _ta7zuy9k, ref Int32 _1eqjusqc, ref Single _1m44vtuk, ref Single _juhpuov6)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Int32 _b69ritwm =  default;
Single _yc8h372p =  default;
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
		//*     .. Parameters .. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		if (_dxpq0xkr > (int)0)
		{
			
			{
				System.Int32 __81fgg2dlsvn1128 = (System.Int32)((int)1);
				System.Int32 __81fgg2step1128 = (System.Int32)(_1eqjusqc);
				System.Int32 __81fgg2count1128;
				for (__81fgg2count1128 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1 + ((_dxpq0xkr - (int)1) * _1eqjusqc)) - __81fgg2dlsvn1128 + __81fgg2step1128) / __81fgg2step1128)), _b69ritwm = __81fgg2dlsvn1128; __81fgg2count1128 != 0; __81fgg2count1128--, _b69ritwm += (__81fgg2step1128)) {

				{
					
					_yc8h372p = ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.REAL(*(_ta7zuy9k+(_b69ritwm - 1)) ) );
					if ((_yc8h372p > _d0547bi2) | _lilv8egi(ref _yc8h372p ))
					{
						
						if (_1m44vtuk < _yc8h372p)
						{
							
							_juhpuov6 = ((int)1 + (_juhpuov6 * __POW2((_1m44vtuk / _yc8h372p))));
							_1m44vtuk = _yc8h372p;
						}
						else
						{
							
							_juhpuov6 = (_juhpuov6 + __POW2((_yc8h372p / _1m44vtuk)));
						}
						
					}
					
					_yc8h372p = ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.AIMAG(*(_ta7zuy9k+(_b69ritwm - 1)) ) );
					if ((_yc8h372p > _d0547bi2) | _lilv8egi(ref _yc8h372p ))
					{
						
						if ((_1m44vtuk < _yc8h372p) | _lilv8egi(ref _yc8h372p ))
						{
							
							_juhpuov6 = ((int)1 + (_juhpuov6 * __POW2((_1m44vtuk / _yc8h372p))));
							_1m44vtuk = _yc8h372p;
						}
						else
						{
							
							_juhpuov6 = (_juhpuov6 + __POW2((_yc8h372p / _1m44vtuk)));
						}
						
					}
					
Mark10:;
					// continue
				}
								}			}
		}
		//* 
		
		return;//* 
		//*     End of CLASSQ 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
