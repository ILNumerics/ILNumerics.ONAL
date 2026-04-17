
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
//*> \brief \b SDOT 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       REAL FUNCTION SDOT(N,SX,INCX,SY,INCY) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER INCX,INCY,N 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL SX(*),SY(*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*>    SDOT forms the dot product of two vectors. 
//*>    uses unrolled loops for increments equal to one. 
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
//*> \param[in] SX 
//*> \verbatim 
//*>          SX is REAL array, dimension ( 1 + ( N - 1 )*abs( INCX ) ) 
//*> \endverbatim 
//*> 
//*> \param[in] INCX 
//*> \verbatim 
//*>          INCX is INTEGER 
//*>         storage spacing between elements of SX 
//*> \endverbatim 
//*> 
//*> \param[in] SY 
//*> \verbatim 
//*>          SY is REAL array, dimension ( 1 + ( N - 1 )*abs( INCY ) ) 
//*> \endverbatim 
//*> 
//*> \param[in] INCY 
//*> \verbatim 
//*>          INCY is INTEGER 
//*>         storage spacing between elements of SY 
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
//*>     jack dongarra, linpack, 3/11/78. 
//*>     modified 12/3/93, array(1) declarations changed to array(*) 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static Single _j4n7j2pu(ref Int32 _dxpq0xkr, Single* _s66poh0u, ref Int32 _1eqjusqc, Single* _bljg4w68, ref Int32 _bbrxgmj7)
	{
#region variable declarations
Single _j4n7j2pu = default;
Single _chiot9on =  default;
Int32 _b5p6od9s =  default;
Int32 _b69ritwm =  default;
Int32 _821h5yui =  default;
Int32 _ev4xhht5 =  default;
Int32 _97on0doo =  default;
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
		
		_chiot9on = 0f;
		_j4n7j2pu = 0f;
		if (_dxpq0xkr <= (int)0)
		return _j4n7j2pu;
		if ((_1eqjusqc == (int)1) & (_bbrxgmj7 == (int)1))
		{
			//* 
			//*        code for both increments equal to 1 
			//* 
			//* 
			//*        clean-up loop 
			//* 
			
			_ev4xhht5 = ILNumerics.F2NET.Intrinsics.MOD(_dxpq0xkr ,(int)5 );
			if (_ev4xhht5 != (int)0)
			{
				
				{
					System.Int32 __81fgg2dlsvn759 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step759 = (System.Int32)((int)1);
					System.Int32 __81fgg2count759;
					for (__81fgg2count759 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn759 + __81fgg2step759) / __81fgg2step759)), _b5p6od9s = __81fgg2dlsvn759; __81fgg2count759 != 0; __81fgg2count759--, _b5p6od9s += (__81fgg2step759)) {

					{
						
						_chiot9on = (_chiot9on + (*(_s66poh0u+(_b5p6od9s - 1)) * *(_bljg4w68+(_b5p6od9s - 1))));
					}
										}				}
				if (_dxpq0xkr < (int)5)
				{
					
					_j4n7j2pu = _chiot9on;
					return _j4n7j2pu;
				}
				
			}
			
			_97on0doo = (_ev4xhht5 + (int)1);
			{
				System.Int32 __81fgg2dlsvn760 = (System.Int32)(_97on0doo);
				System.Int32 __81fgg2step760 = (System.Int32)((int)5);
				System.Int32 __81fgg2count760;
				for (__81fgg2count760 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn760 + __81fgg2step760) / __81fgg2step760)), _b5p6od9s = __81fgg2dlsvn760; __81fgg2count760 != 0; __81fgg2count760--, _b5p6od9s += (__81fgg2step760)) {

				{
					
					_chiot9on = (((((_chiot9on + (*(_s66poh0u+(_b5p6od9s - 1)) * *(_bljg4w68+(_b5p6od9s - 1)))) + (*(_s66poh0u+(_b5p6od9s + (int)1 - 1)) * *(_bljg4w68+(_b5p6od9s + (int)1 - 1)))) + (*(_s66poh0u+(_b5p6od9s + (int)2 - 1)) * *(_bljg4w68+(_b5p6od9s + (int)2 - 1)))) + (*(_s66poh0u+(_b5p6od9s + (int)3 - 1)) * *(_bljg4w68+(_b5p6od9s + (int)3 - 1)))) + (*(_s66poh0u+(_b5p6od9s + (int)4 - 1)) * *(_bljg4w68+(_b5p6od9s + (int)4 - 1))));
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
				System.Int32 __81fgg2dlsvn761 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step761 = (System.Int32)((int)1);
				System.Int32 __81fgg2count761;
				for (__81fgg2count761 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn761 + __81fgg2step761) / __81fgg2step761)), _b5p6od9s = __81fgg2dlsvn761; __81fgg2count761 != 0; __81fgg2count761--, _b5p6od9s += (__81fgg2step761)) {

				{
					
					_chiot9on = (_chiot9on + (*(_s66poh0u+(_b69ritwm - 1)) * *(_bljg4w68+(_821h5yui - 1))));
					_b69ritwm = (_b69ritwm + _1eqjusqc);
					_821h5yui = (_821h5yui + _bbrxgmj7);
				}
								}			}
		}
		
		_j4n7j2pu = _chiot9on;
		return _j4n7j2pu;
	}
	
	return _j4n7j2pu;
	} // 177

} // end class 
} // end namespace
#endif
