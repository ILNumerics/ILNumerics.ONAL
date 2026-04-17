
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
//*> \brief \b DLACN2 estimates the 1-norm of a square matrix, using reverse communication for evaluating matrix-vector products. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLACN2 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlacn2.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlacn2.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlacn2.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLACN2( N, V, X, ISGN, EST, KASE, ISAVE ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            KASE, N 
//*       DOUBLE PRECISION   EST 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            ISGN( * ), ISAVE( 3 ) 
//*       DOUBLE PRECISION   V( * ), X( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLACN2 estimates the 1-norm of a square, real matrix A. 
//*> Reverse communication is used for evaluating matrix-vector products. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>         The order of the matrix.  N >= 1. 
//*> \endverbatim 
//*> 
//*> \param[out] V 
//*> \verbatim 
//*>          V is DOUBLE PRECISION array, dimension (N) 
//*>         On the final return, V = A*W,  where  EST = norm(V)/norm(W) 
//*>         (W is not returned). 
//*> \endverbatim 
//*> 
//*> \param[in,out] X 
//*> \verbatim 
//*>          X is DOUBLE PRECISION array, dimension (N) 
//*>         On an intermediate return, X should be overwritten by 
//*>               A * X,   if KASE=1, 
//*>               A**T * X,  if KASE=2, 
//*>         and DLACN2 must be re-called with all the other parameters 
//*>         unchanged. 
//*> \endverbatim 
//*> 
//*> \param[out] ISGN 
//*> \verbatim 
//*>          ISGN is INTEGER array, dimension (N) 
//*> \endverbatim 
//*> 
//*> \param[in,out] EST 
//*> \verbatim 
//*>          EST is DOUBLE PRECISION 
//*>         On entry with KASE = 1 or 2 and ISAVE(1) = 3, EST should be 
//*>         unchanged from the previous call to DLACN2. 
//*>         On exit, EST is an estimate (a lower bound) for norm(A). 
//*> \endverbatim 
//*> 
//*> \param[in,out] KASE 
//*> \verbatim 
//*>          KASE is INTEGER 
//*>         On the initial call to DLACN2, KASE should be 0. 
//*>         On an intermediate return, KASE will be 1 or 2, indicating 
//*>         whether X should be overwritten by A * X  or A**T * X. 
//*>         On the final return from DLACN2, KASE will again be 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] ISAVE 
//*> \verbatim 
//*>          ISAVE is INTEGER array, dimension (3) 
//*>         ISAVE is used to save variables between calls to DLACN2 
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
//*> \ingroup doubleOTHERauxiliary 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  Originally named SONEST, dated March 16, 1988. 
//*> 
//*>  This is a thread safe version of DLACON, which uses the array ISAVE 
//*>  in place of a SAVE statement, as follows: 
//*> 
//*>     DLACON     DLACN2 
//*>      JUMP     ISAVE(1) 
//*>      J        ISAVE(2) 
//*>      ITER     ISAVE(3) 
//*> \endverbatim 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>     Nick Higham, University of Manchester 
//* 
//*> \par References: 
//*  ================ 
//*> 
//*>  N.J. Higham, "FORTRAN codes for estimating the one-norm of 
//*>  a real or complex matrix, with applications to condition estimation", 
//*>  ACM Trans. Math. Soft., vol. 14, no. 4, pp. 381-396, December 1988. 
//*> 
//*  ===================================================================== 

	 
	public static void _btxg4mdv(ref Int32 _dxpq0xkr, Double* _ycxba85s, Double* _ta7zuy9k, Int32* _6aqem61u, ref Double _xfqajabj, ref Int32 _56nn7y27, Int32* _cpeoijal)
	{
#region variable declarations
Int32 _7u74ue5o =  (int)5;
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Double _5m0mjfxm =  2d;
Int32 _b5p6od9s =  default;
Int32 _4qkl4wkr =  default;
Double _lk5h8crn =  default;
Double _tuo9mg3i =  default;
Double _1ajfmh55 =  default;
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
		//*     .. Parameters .. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		if (_56nn7y27 == (int)0)
		{
			
			{
				System.Int32 __81fgg2dlsvn2322 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2322 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2322;
				for (__81fgg2count2322 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2322 + __81fgg2step2322) / __81fgg2step2322)), _b5p6od9s = __81fgg2dlsvn2322; __81fgg2count2322 != 0; __81fgg2count2322--, _b5p6od9s += (__81fgg2step2322)) {

				{
					
					*(_ta7zuy9k+(_b5p6od9s - 1)) = (_kxg5drh2 / ILNumerics.F2NET.Intrinsics.DBLE(_dxpq0xkr ));
Mark10:;
					// continue
				}
								}			}
			_56nn7y27 = (int)1;
			*(_cpeoijal+((int)1 - 1)) = (int)1;
			return;
		}
		//* 
		
		switch (*(_cpeoijal+((int)1 - 1))) {
						case 1:
			goto Mark20;
			case 2:
			goto Mark40;
			case 3:
			goto Mark70;
			case 4:
			goto Mark110;
			case 5:
			goto Mark140;
			default:
			break;
		}
//* 
		//*     ................ ENTRY   (ISAVE( 1 ) = 1) 
		//*     FIRST ITERATION.  X HAS BEEN OVERWRITTEN BY A*X. 
		//* 
		
Mark20:;
		// continue
		if (_dxpq0xkr == (int)1)
		{
			
			*(_ycxba85s+((int)1 - 1)) = *(_ta7zuy9k+((int)1 - 1));
			_xfqajabj = ILNumerics.F2NET.Intrinsics.ABS(*(_ycxba85s+((int)1 - 1)) );//*        ... QUIT 
			goto Mark150;
		}
		
		_xfqajabj = _seesn96r(ref _dxpq0xkr ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );//* 
		
		{
			System.Int32 __81fgg2dlsvn2323 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2323 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2323;
			for (__81fgg2count2323 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2323 + __81fgg2step2323) / __81fgg2step2323)), _b5p6od9s = __81fgg2dlsvn2323; __81fgg2count2323 != 0; __81fgg2count2323--, _b5p6od9s += (__81fgg2step2323)) {

			{
				
				*(_ta7zuy9k+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,*(_ta7zuy9k+(_b5p6od9s - 1)) );
				*(_6aqem61u+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.NINT(*(_ta7zuy9k+(_b5p6od9s - 1)) );
Mark30:;
				// continue
			}
						}		}
		_56nn7y27 = (int)2;
		*(_cpeoijal+((int)1 - 1)) = (int)2;
		return;//* 
		//*     ................ ENTRY   (ISAVE( 1 ) = 2) 
		//*     FIRST ITERATION.  X HAS BEEN OVERWRITTEN BY TRANSPOSE(A)*X. 
		//* 
		
Mark40:;
		// continue
		*(_cpeoijal+((int)2 - 1)) = _ei7om7ok(ref _dxpq0xkr ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
		*(_cpeoijal+((int)3 - 1)) = (int)2;//* 
		//*     MAIN LOOP - ITERATIONS 2,3,...,ITMAX. 
		//* 
		
Mark50:;
		// continue
		{
			System.Int32 __81fgg2dlsvn2324 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2324 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2324;
			for (__81fgg2count2324 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2324 + __81fgg2step2324) / __81fgg2step2324)), _b5p6od9s = __81fgg2dlsvn2324; __81fgg2count2324 != 0; __81fgg2count2324--, _b5p6od9s += (__81fgg2step2324)) {

			{
				
				*(_ta7zuy9k+(_b5p6od9s - 1)) = _d0547bi2;
Mark60:;
				// continue
			}
						}		}
		*(_ta7zuy9k+(*(_cpeoijal+((int)2 - 1)) - 1)) = _kxg5drh2;
		_56nn7y27 = (int)1;
		*(_cpeoijal+((int)1 - 1)) = (int)3;
		return;//* 
		//*     ................ ENTRY   (ISAVE( 1 ) = 3) 
		//*     X HAS BEEN OVERWRITTEN BY A*X. 
		//* 
		
Mark70:;
		// continue
		_gvjhlct0(ref _dxpq0xkr ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) ,_ycxba85s ,ref Unsafe.AsRef((int)1) );
		_tuo9mg3i = _xfqajabj;
		_xfqajabj = _seesn96r(ref _dxpq0xkr ,_ycxba85s ,ref Unsafe.AsRef((int)1) );
		{
			System.Int32 __81fgg2dlsvn2325 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2325 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2325;
			for (__81fgg2count2325 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2325 + __81fgg2step2325) / __81fgg2step2325)), _b5p6od9s = __81fgg2dlsvn2325; __81fgg2count2325 != 0; __81fgg2count2325--, _b5p6od9s += (__81fgg2step2325)) {

			{
				
				if (ILNumerics.F2NET.Intrinsics.NINT(ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,*(_ta7zuy9k+(_b5p6od9s - 1)) ) ) != *(_6aqem61u+(_b5p6od9s - 1)))goto Mark90;
Mark80:;
				// continue
			}
						}		}//*     REPEATED SIGN VECTOR DETECTED, HENCE ALGORITHM HAS CONVERGED. 
		goto Mark120;//* 
		
Mark90:;
		// continue//*     TEST FOR CYCLING. 
		
		if (_xfqajabj <= _tuo9mg3i)goto Mark120;//* 
		
		{
			System.Int32 __81fgg2dlsvn2326 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2326 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2326;
			for (__81fgg2count2326 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2326 + __81fgg2step2326) / __81fgg2step2326)), _b5p6od9s = __81fgg2dlsvn2326; __81fgg2count2326 != 0; __81fgg2count2326--, _b5p6od9s += (__81fgg2step2326)) {

			{
				
				*(_ta7zuy9k+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,*(_ta7zuy9k+(_b5p6od9s - 1)) );
				*(_6aqem61u+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.NINT(*(_ta7zuy9k+(_b5p6od9s - 1)) );
Mark100:;
				// continue
			}
						}		}
		_56nn7y27 = (int)2;
		*(_cpeoijal+((int)1 - 1)) = (int)4;
		return;//* 
		//*     ................ ENTRY   (ISAVE( 1 ) = 4) 
		//*     X HAS BEEN OVERWRITTEN BY TRANSPOSE(A)*X. 
		//* 
		
Mark110:;
		// continue
		_4qkl4wkr = *(_cpeoijal+((int)2 - 1));
		*(_cpeoijal+((int)2 - 1)) = _ei7om7ok(ref _dxpq0xkr ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
		if ((*(_ta7zuy9k+(_4qkl4wkr - 1)) != ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(*(_cpeoijal+((int)2 - 1)) - 1)) )) & (*(_cpeoijal+((int)3 - 1)) < _7u74ue5o))
		{
			
			*(_cpeoijal+((int)3 - 1)) = (*(_cpeoijal+((int)3 - 1)) + (int)1);goto Mark50;
		}
		//* 
		//*     ITERATION COMPLETE.  FINAL STAGE. 
		//* 
		
Mark120:;
		// continue
		_lk5h8crn = _kxg5drh2;
		{
			System.Int32 __81fgg2dlsvn2327 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2327 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2327;
			for (__81fgg2count2327 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2327 + __81fgg2step2327) / __81fgg2step2327)), _b5p6od9s = __81fgg2dlsvn2327; __81fgg2count2327 != 0; __81fgg2count2327--, _b5p6od9s += (__81fgg2step2327)) {

			{
				
				*(_ta7zuy9k+(_b5p6od9s - 1)) = (_lk5h8crn * (_kxg5drh2 + (ILNumerics.F2NET.Intrinsics.DBLE(_b5p6od9s - (int)1 ) / ILNumerics.F2NET.Intrinsics.DBLE(_dxpq0xkr - (int)1 ))));
				_lk5h8crn = (-(_lk5h8crn));
Mark130:;
				// continue
			}
						}		}
		_56nn7y27 = (int)1;
		*(_cpeoijal+((int)1 - 1)) = (int)5;
		return;//* 
		//*     ................ ENTRY   (ISAVE( 1 ) = 5) 
		//*     X HAS BEEN OVERWRITTEN BY A*X. 
		//* 
		
Mark140:;
		// continue
		_1ajfmh55 = (_5m0mjfxm * (_seesn96r(ref _dxpq0xkr ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) ) / ILNumerics.F2NET.Intrinsics.DBLE((int)3 * _dxpq0xkr )));
		if (_1ajfmh55 > _xfqajabj)
		{
			
			_gvjhlct0(ref _dxpq0xkr ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) ,_ycxba85s ,ref Unsafe.AsRef((int)1) );
			_xfqajabj = _1ajfmh55;
		}
		//* 
		
Mark150:;
		// continue
		_56nn7y27 = (int)0;
		return;//* 
		//*     End of DLACN2 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
