
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
//*> \brief \b SLACN2 estimates the 1-norm of a square matrix, using reverse communication for evaluating matrix-vector products. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLACN2 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slacn2.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slacn2.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slacn2.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLACN2( N, V, X, ISGN, EST, KASE, ISAVE ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            KASE, N 
//*       REAL               EST 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            ISGN( * ), ISAVE( 3 ) 
//*       REAL               V( * ), X( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLACN2 estimates the 1-norm of a square, real matrix A. 
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
//*>          V is REAL array, dimension (N) 
//*>         On the final return, V = A*W,  where  EST = norm(V)/norm(W) 
//*>         (W is not returned). 
//*> \endverbatim 
//*> 
//*> \param[in,out] X 
//*> \verbatim 
//*>          X is REAL array, dimension (N) 
//*>         On an intermediate return, X should be overwritten by 
//*>               A * X,   if KASE=1, 
//*>               A**T * X,  if KASE=2, 
//*>         and SLACN2 must be re-called with all the other parameters 
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
//*>          EST is REAL 
//*>         On entry with KASE = 1 or 2 and ISAVE(1) = 3, EST should be 
//*>         unchanged from the previous call to SLACN2. 
//*>         On exit, EST is an estimate (a lower bound) for norm(A). 
//*> \endverbatim 
//*> 
//*> \param[in,out] KASE 
//*> \verbatim 
//*>          KASE is INTEGER 
//*>         On the initial call to SLACN2, KASE should be 0. 
//*>         On an intermediate return, KASE will be 1 or 2, indicating 
//*>         whether X should be overwritten by A * X  or A**T * X. 
//*>         On the final return from SLACN2, KASE will again be 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] ISAVE 
//*> \verbatim 
//*>          ISAVE is INTEGER array, dimension (3) 
//*>         ISAVE is used to save variables between calls to SLACN2 
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
//*> \ingroup realOTHERauxiliary 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  Originally named SONEST, dated March 16, 1988. 
//*> 
//*>  This is a thread safe version of SLACON, which uses the array ISAVE 
//*>  in place of a SAVE statement, as follows: 
//*> 
//*>     SLACON     SLACN2 
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

	 
	public static void _vuk6gcrf(ref Int32 _dxpq0xkr, Single* _ycxba85s, Single* _ta7zuy9k, Int32* _6aqem61u, ref Single _xfqajabj, ref Int32 _56nn7y27, Int32* _cpeoijal)
	{
#region variable declarations
Int32 _7u74ue5o =  (int)5;
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
Single _5m0mjfxm =  2f;
Int32 _b5p6od9s =  default;
Int32 _4qkl4wkr =  default;
Single _lk5h8crn =  default;
Single _tuo9mg3i =  default;
Single _1ajfmh55 =  default;
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
				System.Int32 __81fgg2dlsvn2487 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2487 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2487;
				for (__81fgg2count2487 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2487 + __81fgg2step2487) / __81fgg2step2487)), _b5p6od9s = __81fgg2dlsvn2487; __81fgg2count2487 != 0; __81fgg2count2487--, _b5p6od9s += (__81fgg2step2487)) {

				{
					
					*(_ta7zuy9k+(_b5p6od9s - 1)) = (_kxg5drh2 / ILNumerics.F2NET.Intrinsics.REAL(_dxpq0xkr ));
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
		
		_xfqajabj = _04plmv36(ref _dxpq0xkr ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );//* 
		
		{
			System.Int32 __81fgg2dlsvn2488 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2488 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2488;
			for (__81fgg2count2488 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2488 + __81fgg2step2488) / __81fgg2step2488)), _b5p6od9s = __81fgg2dlsvn2488; __81fgg2count2488 != 0; __81fgg2count2488--, _b5p6od9s += (__81fgg2step2488)) {

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
		*(_cpeoijal+((int)2 - 1)) = _z5b2nqbf(ref _dxpq0xkr ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
		*(_cpeoijal+((int)3 - 1)) = (int)2;//* 
		//*     MAIN LOOP - ITERATIONS 2,3,...,ITMAX. 
		//* 
		
Mark50:;
		// continue
		{
			System.Int32 __81fgg2dlsvn2489 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2489 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2489;
			for (__81fgg2count2489 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2489 + __81fgg2step2489) / __81fgg2step2489)), _b5p6od9s = __81fgg2dlsvn2489; __81fgg2count2489 != 0; __81fgg2count2489--, _b5p6od9s += (__81fgg2step2489)) {

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
		_wcs7ne88(ref _dxpq0xkr ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) ,_ycxba85s ,ref Unsafe.AsRef((int)1) );
		_tuo9mg3i = _xfqajabj;
		_xfqajabj = _04plmv36(ref _dxpq0xkr ,_ycxba85s ,ref Unsafe.AsRef((int)1) );
		{
			System.Int32 __81fgg2dlsvn2490 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2490 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2490;
			for (__81fgg2count2490 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2490 + __81fgg2step2490) / __81fgg2step2490)), _b5p6od9s = __81fgg2dlsvn2490; __81fgg2count2490 != 0; __81fgg2count2490--, _b5p6od9s += (__81fgg2step2490)) {

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
			System.Int32 __81fgg2dlsvn2491 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2491 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2491;
			for (__81fgg2count2491 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2491 + __81fgg2step2491) / __81fgg2step2491)), _b5p6od9s = __81fgg2dlsvn2491; __81fgg2count2491 != 0; __81fgg2count2491--, _b5p6od9s += (__81fgg2step2491)) {

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
		*(_cpeoijal+((int)2 - 1)) = _z5b2nqbf(ref _dxpq0xkr ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
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
			System.Int32 __81fgg2dlsvn2492 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2492 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2492;
			for (__81fgg2count2492 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2492 + __81fgg2step2492) / __81fgg2step2492)), _b5p6od9s = __81fgg2dlsvn2492; __81fgg2count2492 != 0; __81fgg2count2492--, _b5p6od9s += (__81fgg2step2492)) {

			{
				
				*(_ta7zuy9k+(_b5p6od9s - 1)) = (_lk5h8crn * (_kxg5drh2 + (ILNumerics.F2NET.Intrinsics.REAL(_b5p6od9s - (int)1 ) / ILNumerics.F2NET.Intrinsics.REAL(_dxpq0xkr - (int)1 ))));
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
		_1ajfmh55 = (_5m0mjfxm * (_04plmv36(ref _dxpq0xkr ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) ) / ILNumerics.F2NET.Intrinsics.REAL((int)3 * _dxpq0xkr )));
		if (_1ajfmh55 > _xfqajabj)
		{
			
			_wcs7ne88(ref _dxpq0xkr ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) ,_ycxba85s ,ref Unsafe.AsRef((int)1) );
			_xfqajabj = _1ajfmh55;
		}
		//* 
		
Mark150:;
		// continue
		_56nn7y27 = (int)0;
		return;//* 
		//*     End of SLACN2 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
