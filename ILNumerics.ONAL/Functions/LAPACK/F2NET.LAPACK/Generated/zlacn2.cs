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
//*> \brief \b ZLACN2 estimates the 1-norm of a square matrix, using reverse communication for evaluating matrix-vector products. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZLACN2 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zlacn2.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zlacn2.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zlacn2.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZLACN2( N, V, X, EST, KASE, ISAVE ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            KASE, N 
//*       DOUBLE PRECISION   EST 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            ISAVE( 3 ) 
//*       COMPLEX*16         V( * ), X( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZLACN2 estimates the 1-norm of a square, complex matrix A. 
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
//*>          V is COMPLEX*16 array, dimension (N) 
//*>         On the final return, V = A*W,  where  EST = norm(V)/norm(W) 
//*>         (W is not returned). 
//*> \endverbatim 
//*> 
//*> \param[in,out] X 
//*> \verbatim 
//*>          X is COMPLEX*16 array, dimension (N) 
//*>         On an intermediate return, X should be overwritten by 
//*>               A * X,   if KASE=1, 
//*>               A**H * X,  if KASE=2, 
//*>         where A**H is the conjugate transpose of A, and ZLACN2 must be 
//*>         re-called with all the other parameters unchanged. 
//*> \endverbatim 
//*> 
//*> \param[in,out] EST 
//*> \verbatim 
//*>          EST is DOUBLE PRECISION 
//*>         On entry with KASE = 1 or 2 and ISAVE(1) = 3, EST should be 
//*>         unchanged from the previous call to ZLACN2. 
//*>         On exit, EST is an estimate (a lower bound) for norm(A). 
//*> \endverbatim 
//*> 
//*> \param[in,out] KASE 
//*> \verbatim 
//*>          KASE is INTEGER 
//*>         On the initial call to ZLACN2, KASE should be 0. 
//*>         On an intermediate return, KASE will be 1 or 2, indicating 
//*>         whether X should be overwritten by A * X  or A**H * X. 
//*>         On the final return from ZLACN2, KASE will again be 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] ISAVE 
//*> \verbatim 
//*>          ISAVE is INTEGER array, dimension (3) 
//*>         ISAVE is used to save variables between calls to ZLACN2 
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
//*> \ingroup complex16OTHERauxiliary 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  Originally named CONEST, dated March 16, 1988. 
//*> 
//*>  Last modified:  April, 1999 
//*> 
//*>  This is a thread safe version of ZLACON, which uses the array ISAVE 
//*>  in place of a SAVE statement, as follows: 
//*> 
//*>     ZLACON     ZLACN2 
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

	 
	public static void _09gqi3a8(ref Int32 _dxpq0xkr, complex* _ycxba85s, complex* _ta7zuy9k, ref Double _xfqajabj, ref Int32 _56nn7y27, Int32* _cpeoijal)
	{
#region variable declarations
Int32 _7u74ue5o =  (int)5;
Double _kxg5drh2 =  1d;
Double _5m0mjfxm =  2d;
complex _gdjumcqt =   new fcomplex(0f,0f);
complex _40vhxf9f =   new fcomplex(1f,0f);
Int32 _b5p6od9s =  default;
Int32 _4qkl4wkr =  default;
Double _zma2tpvz =  default;
Double _lk5h8crn =  default;
Double _tuo9mg3i =  default;
Double _h75qnr7l =  default;
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
		
		_h75qnr7l = _f43eg0w0("Safe minimum" );
		if (_56nn7y27 == (int)0)
		{
			
			{
				System.Int32 __81fgg2dlsvn2799 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2799 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2799;
				for (__81fgg2count2799 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2799 + __81fgg2step2799) / __81fgg2step2799)), _b5p6od9s = __81fgg2dlsvn2799; __81fgg2count2799 != 0; __81fgg2count2799--, _b5p6od9s += (__81fgg2step2799)) {

				{
					
					*(_ta7zuy9k+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.DCMPLX(_kxg5drh2 / ILNumerics.F2NET.Intrinsics.DBLE(_dxpq0xkr ) );
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
			goto Mark90;
			case 5:
			goto Mark120;
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
			goto Mark130;
		}
		
		_xfqajabj = _re44grwz(ref _dxpq0xkr ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );//* 
		
		{
			System.Int32 __81fgg2dlsvn2800 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2800 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2800;
			for (__81fgg2count2800 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2800 + __81fgg2step2800) / __81fgg2step2800)), _b5p6od9s = __81fgg2dlsvn2800; __81fgg2count2800 != 0; __81fgg2count2800--, _b5p6od9s += (__81fgg2step2800)) {

			{
				
				_zma2tpvz = ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_b5p6od9s - 1)) );
				if (_zma2tpvz > _h75qnr7l)
				{
					
					*(_ta7zuy9k+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.DCMPLX(ILNumerics.F2NET.Intrinsics.DBLE(*(_ta7zuy9k+(_b5p6od9s - 1)) ) / _zma2tpvz ,ILNumerics.F2NET.Intrinsics.DIMAG(*(_ta7zuy9k+(_b5p6od9s - 1)) ) / _zma2tpvz );
				}
				else
				{
					
					*(_ta7zuy9k+(_b5p6od9s - 1)) = _40vhxf9f;
				}
				
Mark30:;
				// continue
			}
						}		}
		_56nn7y27 = (int)2;
		*(_cpeoijal+((int)1 - 1)) = (int)2;
		return;//* 
		//*     ................ ENTRY   (ISAVE( 1 ) = 2) 
		//*     FIRST ITERATION.  X HAS BEEN OVERWRITTEN BY CTRANS(A)*X. 
		//* 
		
Mark40:;
		// continue
		*(_cpeoijal+((int)2 - 1)) = _wb593x9n(ref _dxpq0xkr ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
		*(_cpeoijal+((int)3 - 1)) = (int)2;//* 
		//*     MAIN LOOP - ITERATIONS 2,3,...,ITMAX. 
		//* 
		
Mark50:;
		// continue
		{
			System.Int32 __81fgg2dlsvn2801 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2801 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2801;
			for (__81fgg2count2801 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2801 + __81fgg2step2801) / __81fgg2step2801)), _b5p6od9s = __81fgg2dlsvn2801; __81fgg2count2801 != 0; __81fgg2count2801--, _b5p6od9s += (__81fgg2step2801)) {

			{
				
				*(_ta7zuy9k+(_b5p6od9s - 1)) = _gdjumcqt;
Mark60:;
				// continue
			}
						}		}
		*(_ta7zuy9k+(*(_cpeoijal+((int)2 - 1)) - 1)) = _40vhxf9f;
		_56nn7y27 = (int)1;
		*(_cpeoijal+((int)1 - 1)) = (int)3;
		return;//* 
		//*     ................ ENTRY   (ISAVE( 1 ) = 3) 
		//*     X HAS BEEN OVERWRITTEN BY A*X. 
		//* 
		
Mark70:;
		// continue
		_ly902k7t(ref _dxpq0xkr ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) ,_ycxba85s ,ref Unsafe.AsRef((int)1) );
		_tuo9mg3i = _xfqajabj;
		_xfqajabj = _re44grwz(ref _dxpq0xkr ,_ycxba85s ,ref Unsafe.AsRef((int)1) );//* 
		//*     TEST FOR CYCLING. 
		
		if (_xfqajabj <= _tuo9mg3i)goto Mark100;//* 
		
		{
			System.Int32 __81fgg2dlsvn2802 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2802 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2802;
			for (__81fgg2count2802 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2802 + __81fgg2step2802) / __81fgg2step2802)), _b5p6od9s = __81fgg2dlsvn2802; __81fgg2count2802 != 0; __81fgg2count2802--, _b5p6od9s += (__81fgg2step2802)) {

			{
				
				_zma2tpvz = ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_b5p6od9s - 1)) );
				if (_zma2tpvz > _h75qnr7l)
				{
					
					*(_ta7zuy9k+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.DCMPLX(ILNumerics.F2NET.Intrinsics.DBLE(*(_ta7zuy9k+(_b5p6od9s - 1)) ) / _zma2tpvz ,ILNumerics.F2NET.Intrinsics.DIMAG(*(_ta7zuy9k+(_b5p6od9s - 1)) ) / _zma2tpvz );
				}
				else
				{
					
					*(_ta7zuy9k+(_b5p6od9s - 1)) = _40vhxf9f;
				}
				
Mark80:;
				// continue
			}
						}		}
		_56nn7y27 = (int)2;
		*(_cpeoijal+((int)1 - 1)) = (int)4;
		return;//* 
		//*     ................ ENTRY   (ISAVE( 1 ) = 4) 
		//*     X HAS BEEN OVERWRITTEN BY CTRANS(A)*X. 
		//* 
		
Mark90:;
		// continue
		_4qkl4wkr = *(_cpeoijal+((int)2 - 1));
		*(_cpeoijal+((int)2 - 1)) = _wb593x9n(ref _dxpq0xkr ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
		if ((ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_4qkl4wkr - 1)) ) != ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(*(_cpeoijal+((int)2 - 1)) - 1)) )) & (*(_cpeoijal+((int)3 - 1)) < _7u74ue5o))
		{
			
			*(_cpeoijal+((int)3 - 1)) = (*(_cpeoijal+((int)3 - 1)) + (int)1);goto Mark50;
		}
		//* 
		//*     ITERATION COMPLETE.  FINAL STAGE. 
		//* 
		
Mark100:;
		// continue
		_lk5h8crn = _kxg5drh2;
		{
			System.Int32 __81fgg2dlsvn2803 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2803 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2803;
			for (__81fgg2count2803 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2803 + __81fgg2step2803) / __81fgg2step2803)), _b5p6od9s = __81fgg2dlsvn2803; __81fgg2count2803 != 0; __81fgg2count2803--, _b5p6od9s += (__81fgg2step2803)) {

			{
				
				*(_ta7zuy9k+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.DCMPLX(_lk5h8crn * (_kxg5drh2 + (ILNumerics.F2NET.Intrinsics.DBLE(_b5p6od9s - (int)1 ) / ILNumerics.F2NET.Intrinsics.DBLE(_dxpq0xkr - (int)1 ))) );
				_lk5h8crn = (-(_lk5h8crn));
Mark110:;
				// continue
			}
						}		}
		_56nn7y27 = (int)1;
		*(_cpeoijal+((int)1 - 1)) = (int)5;
		return;//* 
		//*     ................ ENTRY   (ISAVE( 1 ) = 5) 
		//*     X HAS BEEN OVERWRITTEN BY A*X. 
		//* 
		
Mark120:;
		// continue
		_1ajfmh55 = (_5m0mjfxm * (_re44grwz(ref _dxpq0xkr ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) ) / ILNumerics.F2NET.Intrinsics.DBLE((int)3 * _dxpq0xkr )));
		if (_1ajfmh55 > _xfqajabj)
		{
			
			_ly902k7t(ref _dxpq0xkr ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) ,_ycxba85s ,ref Unsafe.AsRef((int)1) );
			_xfqajabj = _1ajfmh55;
		}
		//* 
		
Mark130:;
		// continue
		_56nn7y27 = (int)0;
		return;//* 
		//*     End of ZLACN2 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
