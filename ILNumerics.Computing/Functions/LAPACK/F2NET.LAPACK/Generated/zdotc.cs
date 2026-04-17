
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
//*> \brief \b ZDOTC 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       COMPLEX*16 FUNCTION ZDOTC(N,ZX,INCX,ZY,INCY) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER INCX,INCY,N 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX*16 ZX(*),ZY(*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZDOTC forms the dot product of two complex vectors 
//*>      ZDOTC = X^H * Y 
//*> 
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
//*> \param[in] ZX 
//*> \verbatim 
//*>          ZX is REAL array, dimension ( 1 + ( N - 1 )*abs( INCX ) ) 
//*> \endverbatim 
//*> 
//*> \param[in] INCX 
//*> \verbatim 
//*>          INCX is INTEGER 
//*>         storage spacing between elements of ZX 
//*> \endverbatim 
//*> 
//*> \param[in] ZY 
//*> \verbatim 
//*>          ZY is REAL array, dimension ( 1 + ( N - 1 )*abs( INCY ) ) 
//*> \endverbatim 
//*> 
//*> \param[in] INCY 
//*> \verbatim 
//*>          INCY is INTEGER 
//*>         storage spacing between elements of ZY 
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
//*> \ingroup complex16_blas_level1 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>     jack dongarra, 3/11/78. 
//*>     modified 12/3/93, array(1) declarations changed to array(*) 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static complex _s2hgtw14(ref Int32 _dxpq0xkr, complex* _kb1qxpu5, ref Int32 _1eqjusqc, complex* _qcwj4p63, ref Int32 _bbrxgmj7)
	{
#region variable declarations
complex _s2hgtw14 = default;
complex _tt7ajqor =  default;
Int32 _b5p6od9s =  default;
Int32 _b69ritwm =  default;
Int32 _821h5yui =  default;
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
		
		_tt7ajqor =  new fcomplex(0f,0f);
		_s2hgtw14 =  new fcomplex(0f,0f);
		if (_dxpq0xkr <= (int)0)
		return _s2hgtw14;
		if ((_1eqjusqc == (int)1) & (_bbrxgmj7 == (int)1))
		{
			//* 
			//*        code for both increments equal to 1 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn1749 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1749 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1749;
				for (__81fgg2count1749 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1749 + __81fgg2step1749) / __81fgg2step1749)), _b5p6od9s = __81fgg2dlsvn1749; __81fgg2count1749 != 0; __81fgg2count1749--, _b5p6od9s += (__81fgg2step1749)) {

				{
					
					_tt7ajqor = (_tt7ajqor + (ILNumerics.F2NET.Intrinsics.DCONJG(*(_kb1qxpu5+(_b5p6od9s - 1)) ) * *(_qcwj4p63+(_b5p6od9s - 1))));
				}
								}			}
		}
		else
		{
			//* 
			//*        code for unequal increments or equal increments 
			//*          not equal to 1 
			//* 
			
			_b69ritwm = (int)1;
			_821h5yui = (int)1;
			if (_1eqjusqc < (int)0)
			_b69ritwm = ((((-(_dxpq0xkr)) + (int)1) * _1eqjusqc) + (int)1);
			if (_bbrxgmj7 < (int)0)
			_821h5yui = ((((-(_dxpq0xkr)) + (int)1) * _bbrxgmj7) + (int)1);
			{
				System.Int32 __81fgg2dlsvn1750 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1750 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1750;
				for (__81fgg2count1750 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1750 + __81fgg2step1750) / __81fgg2step1750)), _b5p6od9s = __81fgg2dlsvn1750; __81fgg2count1750 != 0; __81fgg2count1750--, _b5p6od9s += (__81fgg2step1750)) {

				{
					
					_tt7ajqor = (_tt7ajqor + (ILNumerics.F2NET.Intrinsics.DCONJG(*(_kb1qxpu5+(_b69ritwm - 1)) ) * *(_qcwj4p63+(_821h5yui - 1))));
					_b69ritwm = (_b69ritwm + _1eqjusqc);
					_821h5yui = (_821h5yui + _bbrxgmj7);
				}
								}			}
		}
		
		_s2hgtw14 = _tt7ajqor;
		return _s2hgtw14;
	}
	
	return _s2hgtw14;
	} // 177

} // end class 
} // end namespace
#endif
