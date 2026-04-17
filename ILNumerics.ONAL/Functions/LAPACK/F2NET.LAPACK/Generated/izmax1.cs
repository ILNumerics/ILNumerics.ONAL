
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
//*> \brief \b IZMAX1 finds the index of the first vector element of maximum absolute value. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download IZMAX1 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/izmax1.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/izmax1.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/izmax1.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       INTEGER          FUNCTION IZMAX1( N, ZX, INCX ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INCX, N 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX*16         ZX( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> IZMAX1 finds the index of the first vector element of maximum absolute value. 
//*> 
//*> Based on IZAMAX from Level 1 BLAS. 
//*> The change is to use the 'genuine' absolute value. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of elements in the vector ZX. 
//*> \endverbatim 
//*> 
//*> \param[in] ZX 
//*> \verbatim 
//*>          ZX is COMPLEX*16 array, dimension (N) 
//*>          The vector ZX. The IZMAX1 function returns the index of its first 
//*>          element of maximum absolute value. 
//*> \endverbatim 
//*> 
//*> \param[in] INCX 
//*> \verbatim 
//*>          INCX is INTEGER 
//*>          The spacing between successive values of ZX.  INCX >= 1. 
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
//*> \date February 2014 
//* 
//*> \ingroup complexOTHERauxiliary 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*> Nick Higham for use with ZLACON. 
//* 
//*  ===================================================================== 

	 
	public static Int32 _wb593x9n(ref Int32 _dxpq0xkr, complex* _kb1qxpu5, ref Int32 _1eqjusqc)
	{
#region variable declarations
Int32 _wb593x9n = default;
Double _49t2npjg =  default;
Int32 _b5p6od9s =  default;
Int32 _b69ritwm =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     February 2014 
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
		//*     .. Executable Statements .. 
		//* 
		
		_wb593x9n = (int)0;
		if ((_dxpq0xkr < (int)1) | (_1eqjusqc <= (int)0))
		return _wb593x9n;
		_wb593x9n = (int)1;
		if (_dxpq0xkr == (int)1)
		return _wb593x9n;
		if (_1eqjusqc == (int)1)
		{
			//* 
			//*        code for increment equal to 1 
			//* 
			
			_49t2npjg = ILNumerics.F2NET.Intrinsics.ABS(*(_kb1qxpu5+((int)1 - 1)) );
			{
				System.Int32 __81fgg2dlsvn2804 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step2804 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2804;
				for (__81fgg2count2804 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2804 + __81fgg2step2804) / __81fgg2step2804)), _b5p6od9s = __81fgg2dlsvn2804; __81fgg2count2804 != 0; __81fgg2count2804--, _b5p6od9s += (__81fgg2step2804)) {

				{
					
					if (ILNumerics.F2NET.Intrinsics.ABS(*(_kb1qxpu5+(_b5p6od9s - 1)) ) > _49t2npjg)
					{
						
						_wb593x9n = _b5p6od9s;
						_49t2npjg = ILNumerics.F2NET.Intrinsics.ABS(*(_kb1qxpu5+(_b5p6od9s - 1)) );
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
			_49t2npjg = ILNumerics.F2NET.Intrinsics.ABS(*(_kb1qxpu5+((int)1 - 1)) );
			_b69ritwm = (_b69ritwm + _1eqjusqc);
			{
				System.Int32 __81fgg2dlsvn2805 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step2805 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2805;
				for (__81fgg2count2805 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2805 + __81fgg2step2805) / __81fgg2step2805)), _b5p6od9s = __81fgg2dlsvn2805; __81fgg2count2805 != 0; __81fgg2count2805--, _b5p6od9s += (__81fgg2step2805)) {

				{
					
					if (ILNumerics.F2NET.Intrinsics.ABS(*(_kb1qxpu5+(_b69ritwm - 1)) ) > _49t2npjg)
					{
						
						_wb593x9n = _b5p6od9s;
						_49t2npjg = ILNumerics.F2NET.Intrinsics.ABS(*(_kb1qxpu5+(_b69ritwm - 1)) );
					}
					
					_b69ritwm = (_b69ritwm + _1eqjusqc);
				}
								}			}
		}
		
		return _wb593x9n;//* 
		//*     End of IZMAX1 
		//* 
		
	}
	
	return _wb593x9n;
	} // 177

} // end class 
} // end namespace
#endif
