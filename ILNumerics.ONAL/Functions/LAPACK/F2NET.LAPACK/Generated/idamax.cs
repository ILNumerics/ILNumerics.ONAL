
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
//*> \brief \b IDAMAX 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       INTEGER FUNCTION IDAMAX(N,DX,INCX) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER INCX,N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION DX(*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*>    IDAMAX finds the index of the first element having maximum absolute value. 
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
//*> \param[in] DX 
//*> \verbatim 
//*>          DX is DOUBLE PRECISION array, dimension ( 1 + ( N - 1 )*abs( INCX ) ) 
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
//*> \ingroup aux_blas 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>     jack dongarra, linpack, 3/11/78. 
//*>     modified 3/93 to return if incx .le. 0. 
//*>     modified 12/3/93, array(1) declarations changed to array(*) 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static Int32 _ei7om7ok(ref Int32 _dxpq0xkr, Double* _dr47k4na, ref Int32 _1eqjusqc)
	{
#region variable declarations
Int32 _ei7om7ok = default;
Double _49t2npjg =  default;
Int32 _b5p6od9s =  default;
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
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		
		_ei7om7ok = (int)0;
		if ((_dxpq0xkr < (int)1) | (_1eqjusqc <= (int)0))
		return _ei7om7ok;
		_ei7om7ok = (int)1;
		if (_dxpq0xkr == (int)1)
		return _ei7om7ok;
		if (_1eqjusqc == (int)1)
		{
			//* 
			//*        code for increment equal to 1 
			//* 
			
			_49t2npjg = ILNumerics.F2NET.Intrinsics.DABS(*(_dr47k4na+((int)1 - 1)) );
			{
				System.Int32 __81fgg2dlsvn1760 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step1760 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1760;
				for (__81fgg2count1760 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1760 + __81fgg2step1760) / __81fgg2step1760)), _b5p6od9s = __81fgg2dlsvn1760; __81fgg2count1760 != 0; __81fgg2count1760--, _b5p6od9s += (__81fgg2step1760)) {

				{
					
					if (ILNumerics.F2NET.Intrinsics.DABS(*(_dr47k4na+(_b5p6od9s - 1)) ) > _49t2npjg)
					{
						
						_ei7om7ok = _b5p6od9s;
						_49t2npjg = ILNumerics.F2NET.Intrinsics.DABS(*(_dr47k4na+(_b5p6od9s - 1)) );
					}
					
				}
								}			}
		}
		else
		{
			//* 
			//*        code for increment not equal to 1 
			//* 
			
			_b69ritwm = (int)1;
			_49t2npjg = ILNumerics.F2NET.Intrinsics.DABS(*(_dr47k4na+((int)1 - 1)) );
			_b69ritwm = (_b69ritwm + _1eqjusqc);
			{
				System.Int32 __81fgg2dlsvn1761 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step1761 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1761;
				for (__81fgg2count1761 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1761 + __81fgg2step1761) / __81fgg2step1761)), _b5p6od9s = __81fgg2dlsvn1761; __81fgg2count1761 != 0; __81fgg2count1761--, _b5p6od9s += (__81fgg2step1761)) {

				{
					
					if (ILNumerics.F2NET.Intrinsics.DABS(*(_dr47k4na+(_b69ritwm - 1)) ) > _49t2npjg)
					{
						
						_ei7om7ok = _b5p6od9s;
						_49t2npjg = ILNumerics.F2NET.Intrinsics.DABS(*(_dr47k4na+(_b69ritwm - 1)) );
					}
					
					_b69ritwm = (_b69ritwm + _1eqjusqc);
				}
								}			}
		}
		
		return _ei7om7ok;
	}
	
	return _ei7om7ok;
	} // 177

} // end class 
} // end namespace
#endif
