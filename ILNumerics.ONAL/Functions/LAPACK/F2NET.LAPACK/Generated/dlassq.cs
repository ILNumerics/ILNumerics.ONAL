
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
//*> \brief \b DLASSQ updates a sum of squares represented in scaled form. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLASSQ + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlassq.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlassq.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlassq.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLASSQ( N, X, INCX, SCALE, SUMSQ ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INCX, N 
//*       DOUBLE PRECISION   SCALE, SUMSQ 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   X( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLASSQ  returns the values  scl  and  smsq  such that 
//*> 
//*>    ( scl**2 )*smsq = x( 1 )**2 +...+ x( n )**2 + ( scale**2 )*sumsq, 
//*> 
//*> where  x( i ) = X( 1 + ( i - 1 )*INCX ). The value of  sumsq  is 
//*> assumed to be non-negative and  scl  returns the value 
//*> 
//*>    scl = max( scale, abs( x( i ) ) ). 
//*> 
//*> scale and sumsq must be supplied in SCALE and SUMSQ and 
//*> scl and smsq are overwritten on SCALE and SUMSQ respectively. 
//*> 
//*> The routine makes only one pass through the vector x. 
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
//*>          X is DOUBLE PRECISION array, dimension (1+(N-1)*INCX) 
//*>          The vector for which a scaled sum of squares is computed. 
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
//*>          SCALE is DOUBLE PRECISION 
//*>          On entry, the value  scale  in the equation above. 
//*>          On exit, SCALE is overwritten with  scl , the scaling factor 
//*>          for the sum of squares. 
//*> \endverbatim 
//*> 
//*> \param[in,out] SUMSQ 
//*> \verbatim 
//*>          SUMSQ is DOUBLE PRECISION 
//*>          On entry, the value  sumsq  in the equation above. 
//*>          On exit, SUMSQ is overwritten with  smsq , the basic sum of 
//*>          squares from which  scl  has been factored out. 
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
//*> \ingroup OTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _g54gbghr(ref Int32 _dxpq0xkr, Double* _ta7zuy9k, ref Int32 _1eqjusqc, ref Double _1m44vtuk, ref Double _juhpuov6)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Int32 _b69ritwm =  default;
Double _zma2tpvz =  default;
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
				System.Int32 __81fgg2dlsvn178 = (System.Int32)((int)1);
				System.Int32 __81fgg2step178 = (System.Int32)(_1eqjusqc);
				System.Int32 __81fgg2count178;
				for (__81fgg2count178 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1 + ((_dxpq0xkr - (int)1) * _1eqjusqc)) - __81fgg2dlsvn178 + __81fgg2step178) / __81fgg2step178)), _b69ritwm = __81fgg2dlsvn178; __81fgg2count178 != 0; __81fgg2count178--, _b69ritwm += (__81fgg2step178)) {

				{
					
					_zma2tpvz = ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_b69ritwm - 1)) );
					if ((_zma2tpvz > _d0547bi2) | _fk98jwhi(ref _zma2tpvz ))
					{
						
						if (_1m44vtuk < _zma2tpvz)
						{
							
							_juhpuov6 = ((int)1 + (_juhpuov6 * __POW2((_1m44vtuk / _zma2tpvz))));
							_1m44vtuk = _zma2tpvz;
						}
						else
						{
							
							_juhpuov6 = (_juhpuov6 + __POW2((_zma2tpvz / _1m44vtuk)));
						}
						
					}
					
Mark10:;
					// continue
				}
								}			}
		}
		
		return;//* 
		//*     End of DLASSQ 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
