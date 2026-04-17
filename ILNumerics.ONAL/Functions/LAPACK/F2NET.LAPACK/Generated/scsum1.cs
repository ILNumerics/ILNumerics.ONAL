
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
//*> \brief \b SCSUM1 forms the 1-norm of the complex vector using the true absolute value. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SCSUM1 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/scsum1.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/scsum1.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/scsum1.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       REAL             FUNCTION SCSUM1( N, CX, INCX ) 
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
//*> SCSUM1 takes the sum of the absolute values of a complex 
//*> vector and returns a single precision result. 
//*> 
//*> Based on SCASUM from the Level 1 BLAS. 
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
//*>          The vector whose elements will be summed. 
//*> \endverbatim 
//*> 
//*> \param[in] INCX 
//*> \verbatim 
//*>          INCX is INTEGER 
//*>          The spacing between successive values of CX.  INCX > 0. 
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
//*> \par Contributors: 
//*  ================== 
//*> 
//*> Nick Higham for use with CLACON. 
//* 
//*  ===================================================================== 

	 
	public static Single _ent84e28(ref Int32 _dxpq0xkr, fcomplex* _c9wzt3lw, ref Int32 _1eqjusqc)
	{
#region variable declarations
Single _ent84e28 = default;
Int32 _b5p6od9s =  default;
Int32 _dbc5n539 =  default;
Single _chiot9on =  default;
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
		//*  ===================================================================== 
		//* 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		_ent84e28 = 0f;
		_chiot9on = 0f;
		if (_dxpq0xkr <= (int)0)
		return _ent84e28;
		if (_1eqjusqc == (int)1)goto Mark20;//* 
		//*     CODE FOR INCREMENT NOT EQUAL TO 1 
		//* 
		
		_dbc5n539 = (_dxpq0xkr * _1eqjusqc);
		{
			System.Int32 __81fgg2dlsvn2645 = (System.Int32)((int)1);
			System.Int32 __81fgg2step2645 = (System.Int32)(_1eqjusqc);
			System.Int32 __81fgg2count2645;
			for (__81fgg2count2645 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dbc5n539) - __81fgg2dlsvn2645 + __81fgg2step2645) / __81fgg2step2645)), _b5p6od9s = __81fgg2dlsvn2645; __81fgg2count2645 != 0; __81fgg2count2645--, _b5p6od9s += (__81fgg2step2645)) {

			{
				//* 
				//*        NEXT LINE MODIFIED. 
				//* 
				
				_chiot9on = (_chiot9on + ILNumerics.F2NET.Intrinsics.ABS(*(_c9wzt3lw+(_b5p6od9s - 1)) ));
Mark10:;
				// continue
			}
						}		}
		_ent84e28 = _chiot9on;
		return _ent84e28;//* 
		//*     CODE FOR INCREMENT EQUAL TO 1 
		//* 
		
Mark20:;
		// continue
		{
			System.Int32 __81fgg2dlsvn2646 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2646 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2646;
			for (__81fgg2count2646 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2646 + __81fgg2step2646) / __81fgg2step2646)), _b5p6od9s = __81fgg2dlsvn2646; __81fgg2count2646 != 0; __81fgg2count2646--, _b5p6od9s += (__81fgg2step2646)) {

			{
				//* 
				//*        NEXT LINE MODIFIED. 
				//* 
				
				_chiot9on = (_chiot9on + ILNumerics.F2NET.Intrinsics.ABS(*(_c9wzt3lw+(_b5p6od9s - 1)) ));
Mark30:;
				// continue
			}
						}		}
		_ent84e28 = _chiot9on;
		return _ent84e28;//* 
		//*     End of SCSUM1 
		//* 
		
	}
	
	return _ent84e28;
	} // 177

} // end class 
} // end namespace
#endif
