
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
//*> \brief \b SNRM2 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       REAL FUNCTION SNRM2(N,X,INCX) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER INCX,N 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL X(*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SNRM2 returns the euclidean norm of a vector via the function 
//*> name, so that 
//*> 
//*>    SNRM2 := sqrt( x'*x ). 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>         number of elements in input vector(s) 
//*> \endverbatim 
//*> 
//*> \param[in] X 
//*> \verbatim 
//*>          X is REAL array, dimension ( 1 + ( N - 1 )*abs( INCX ) ) 
//*> \endverbatim 
//*> 
//*> \param[in] INCX 
//*> \verbatim 
//*>          INCX is INTEGER 
//*>         storage spacing between elements of SX 
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
//*> \date November 2017 
//* 
//*> \ingroup single_blas_level1 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  -- This version written on 25-October-1982. 
//*>     Modified on 14-October-1993 to inline the call to SLASSQ. 
//*>     Sven Hammarling, Nag Ltd. 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static Single _z20xbrro(ref Int32 _dxpq0xkr, Single* _ta7zuy9k, ref Int32 _1eqjusqc)
	{
#region variable declarations
Single _z20xbrro = default;
Single _kxg5drh2 =  1f;
Single _d0547bi2 =  0f;
Single _zma2tpvz =  default;
Single _gq71rsgu =  default;
Single _1m44vtuk =  default;
Single _8l4yph2p =  default;
Int32 _b69ritwm =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- Reference BLAS level1 routine (version 3.8.0) -- 
		//*  -- Reference BLAS is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     November 2017 
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
		//*     .. Intrinsic Functions .. 
		//*     .. 
		
		if ((_dxpq0xkr < (int)1) | (_1eqjusqc < (int)1))
		{
			
			_gq71rsgu = _d0547bi2;
		}
		else
		if (_dxpq0xkr == (int)1)
		{
			
			_gq71rsgu = ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+((int)1 - 1)) );
		}
		else
		{
			
			_1m44vtuk = _d0547bi2;
			_8l4yph2p = _kxg5drh2;//*        The following loop is equivalent to this call to the LAPACK 
			//*        auxiliary routine: 
			//*        CALL SLASSQ( N, X, INCX, SCALE, SSQ ) 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn616 = (System.Int32)((int)1);
				System.Int32 __81fgg2step616 = (System.Int32)(_1eqjusqc);
				System.Int32 __81fgg2count616;
				for (__81fgg2count616 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1 + ((_dxpq0xkr - (int)1) * _1eqjusqc)) - __81fgg2dlsvn616 + __81fgg2step616) / __81fgg2step616)), _b69ritwm = __81fgg2dlsvn616; __81fgg2count616 != 0; __81fgg2count616--, _b69ritwm += (__81fgg2step616)) {

				{
					
					if (*(_ta7zuy9k+(_b69ritwm - 1)) != _d0547bi2)
					{
						
						_zma2tpvz = ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_b69ritwm - 1)) );
						if (_1m44vtuk < _zma2tpvz)
						{
							
							_8l4yph2p = (_kxg5drh2 + (_8l4yph2p * __POW2((_1m44vtuk / _zma2tpvz))));
							_1m44vtuk = _zma2tpvz;
						}
						else
						{
							
							_8l4yph2p = (_8l4yph2p + __POW2((_zma2tpvz / _1m44vtuk)));
						}
						
					}
					
Mark10:;
					// continue
				}
								}			}
			_gq71rsgu = (_1m44vtuk * ILNumerics.F2NET.Intrinsics.SQRT(_8l4yph2p ));
		}
		//* 
		
		_z20xbrro = _gq71rsgu;
		return _z20xbrro;//* 
		//*     End of SNRM2. 
		//* 
		
	}
	
	return _z20xbrro;
	} // 177

} // end class 
} // end namespace
#endif
