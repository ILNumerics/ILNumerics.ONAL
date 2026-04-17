
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
//*> \brief \b ICMAX1 finds the index of the first vector element of maximum absolute value. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ICMAX1 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/icmax1.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/icmax1.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/icmax1.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       INTEGER          FUNCTION ICMAX1( N, CX, INCX ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INCX, N 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX            CX( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ICMAX1 finds the index of the first vector element of maximum absolute value. 
//*> 
//*> Based on ICAMAX from Level 1 BLAS. 
//*> The change is to use the 'genuine' absolute value. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of elements in the vector CX. 
//*> \endverbatim 
//*> 
//*> \param[in] CX 
//*> \verbatim 
//*>          CX is COMPLEX array, dimension (N) 
//*>          The vector CX. The ICMAX1 function returns the index of its first 
//*>          element of maximum absolute value. 
//*> \endverbatim 
//*> 
//*> \param[in] INCX 
//*> \verbatim 
//*>          INCX is INTEGER 
//*>          The spacing between successive values of CX.  INCX >= 1. 
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
//*> Nick Higham for use with CLACON. 
//* 
//*  ===================================================================== 

	 
	public static Int32 _u3dmtjx4(ref Int32 _dxpq0xkr, fcomplex* _c9wzt3lw, ref Int32 _1eqjusqc)
	{
#region variable declarations
Int32 _u3dmtjx4 = default;
Single _t7m1e103 =  default;
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
		
		_u3dmtjx4 = (int)0;
		if ((_dxpq0xkr < (int)1) | (_1eqjusqc <= (int)0))
		return _u3dmtjx4;
		_u3dmtjx4 = (int)1;
		if (_dxpq0xkr == (int)1)
		return _u3dmtjx4;
		if (_1eqjusqc == (int)1)
		{
			//* 
			//*        code for increment equal to 1 
			//* 
			
			_t7m1e103 = ILNumerics.F2NET.Intrinsics.ABS(*(_c9wzt3lw+((int)1 - 1)) );
			{
				System.Int32 __81fgg2dlsvn2643 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step2643 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2643;
				for (__81fgg2count2643 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2643 + __81fgg2step2643) / __81fgg2step2643)), _b5p6od9s = __81fgg2dlsvn2643; __81fgg2count2643 != 0; __81fgg2count2643--, _b5p6od9s += (__81fgg2step2643)) {

				{
					
					if (ILNumerics.F2NET.Intrinsics.ABS(*(_c9wzt3lw+(_b5p6od9s - 1)) ) > _t7m1e103)
					{
						
						_u3dmtjx4 = _b5p6od9s;
						_t7m1e103 = ILNumerics.F2NET.Intrinsics.ABS(*(_c9wzt3lw+(_b5p6od9s - 1)) );
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
			_t7m1e103 = ILNumerics.F2NET.Intrinsics.ABS(*(_c9wzt3lw+((int)1 - 1)) );
			_b69ritwm = (_b69ritwm + _1eqjusqc);
			{
				System.Int32 __81fgg2dlsvn2644 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step2644 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2644;
				for (__81fgg2count2644 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2644 + __81fgg2step2644) / __81fgg2step2644)), _b5p6od9s = __81fgg2dlsvn2644; __81fgg2count2644 != 0; __81fgg2count2644--, _b5p6od9s += (__81fgg2step2644)) {

				{
					
					if (ILNumerics.F2NET.Intrinsics.ABS(*(_c9wzt3lw+(_b69ritwm - 1)) ) > _t7m1e103)
					{
						
						_u3dmtjx4 = _b5p6od9s;
						_t7m1e103 = ILNumerics.F2NET.Intrinsics.ABS(*(_c9wzt3lw+(_b69ritwm - 1)) );
					}
					
					_b69ritwm = (_b69ritwm + _1eqjusqc);
				}
								}			}
		}
		
		return _u3dmtjx4;//* 
		//*     End of ICMAX1 
		//* 
		
	}
	
	return _u3dmtjx4;
	} // 177

} // end class 
} // end namespace
#endif
