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
//*> \brief \b DNRM2 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       DOUBLE PRECISION FUNCTION DNRM2(N,X,INCX) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER INCX,N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION X(*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DNRM2 returns the euclidean norm of a vector via the function 
//*> name, so that 
//*> 
//*>    DNRM2 := sqrt( x'*x ) 
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
//*>          X is DOUBLE PRECISION array, dimension ( 1 + ( N - 1 )*abs( INCX ) ) 
//*> \endverbatim 
//*> 
//*> \param[in] INCX 
//*> \verbatim 
//*>          INCX is INTEGER 
//*>         storage spacing between elements of DX 
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
//*> \ingroup double_blas_level1 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  -- This version written on 25-October-1982. 
//*>     Modified on 14-October-1993 to inline the call to DLASSQ. 
//*>     Sven Hammarling, Nag Ltd. 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static Double _gmlreqin(ref Int32 _dxpq0xkr, Double* _ta7zuy9k, ref Int32 _1eqjusqc)
	{
#region variable declarations
Double _gmlreqin = default;
Double _kxg5drh2 =  1d;
Double _d0547bi2 =  0d;
Double _zma2tpvz =  default;
Double _gq71rsgu =  default;
Double _1m44vtuk =  default;
Double _8l4yph2p =  default;
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
			//*        CALL DLASSQ( N, X, INCX, SCALE, SSQ ) 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn249 = (System.Int32)((int)1);
				System.Int32 __81fgg2step249 = (System.Int32)(_1eqjusqc);
				System.Int32 __81fgg2count249;
				for (__81fgg2count249 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1 + ((_dxpq0xkr - (int)1) * _1eqjusqc)) - __81fgg2dlsvn249 + __81fgg2step249) / __81fgg2step249)), _b69ritwm = __81fgg2dlsvn249; __81fgg2count249 != 0; __81fgg2count249--, _b69ritwm += (__81fgg2step249)) {

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
		
		_gmlreqin = _gq71rsgu;
		return _gmlreqin;//* 
		//*     End of DNRM2. 
		//* 
		
	}
	
	return _gmlreqin;
	} // 177

} // end class 
} // end namespace
#endif
