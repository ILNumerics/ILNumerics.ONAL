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
//*> \brief \b DLASQ1 computes the singular values of a real square bidiagonal matrix. Used by sbdsqr. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLASQ1 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlasq1.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlasq1.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlasq1.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLASQ1( N, D, E, WORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   D( * ), E( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLASQ1 computes the singular values of a real N-by-N bidiagonal 
//*> matrix with diagonal D and off-diagonal E. The singular values 
//*> are computed to high relative accuracy, in the absence of 
//*> denormalization, underflow and overflow. The algorithm was first 
//*> presented in 
//*> 
//*> "Accurate singular values and differential qd algorithms" by K. V. 
//*> Fernando and B. N. Parlett, Numer. Math., Vol-67, No. 2, pp. 191-230, 
//*> 1994, 
//*> 
//*> and the present implementation is described in "An implementation of 
//*> the dqds Algorithm (Positive Case)", LAPACK Working Note. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>        The number of rows and columns in the matrix. N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] D 
//*> \verbatim 
//*>          D is DOUBLE PRECISION array, dimension (N) 
//*>        On entry, D contains the diagonal elements of the 
//*>        bidiagonal matrix whose SVD is desired. On normal exit, 
//*>        D contains the singular values in decreasing order. 
//*> \endverbatim 
//*> 
//*> \param[in,out] E 
//*> \verbatim 
//*>          E is DOUBLE PRECISION array, dimension (N) 
//*>        On entry, elements E(1:N-1) contain the off-diagonal elements 
//*>        of the bidiagonal matrix whose SVD is desired. 
//*>        On exit, E is overwritten. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is DOUBLE PRECISION array, dimension (4*N) 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>        = 0: successful exit 
//*>        < 0: if INFO = -i, the i-th argument had an illegal value 
//*>        > 0: the algorithm failed 
//*>             = 1, a split was marked by a positive value in E 
//*>             = 2, current block of Z not diagonalized after 100*N 
//*>                  iterations (in inner while loop)  On exit D and E 
//*>                  represent a matrix with the same singular values 
//*>                  which the calling subroutine could use to finish the 
//*>                  computation, or even feed back into DLASQ1 
//*>             = 3, termination criterion of outer while loop not met 
//*>                  (program created more than N unreduced blocks) 
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
//*> \ingroup auxOTHERcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _ovwsxjz5(ref Int32 _dxpq0xkr, Double* _plfm7z8g, Double* _864fslqq, Double* _apig8meb, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Int32 _b5p6od9s =  default;
Int32 _itfnbz60 =  default;
Double _p1iqarg6 =  default;
Double _1m44vtuk =  default;
Double _h75qnr7l =  default;
Double _sq0ypuw6 =  default;
Double _7d8q5cd7 =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK computational routine (version 3.7.0) -- 
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
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		_gro5yvfo = (int)0;
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-1;
			_ut9qalzx("DLASQ1" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		else
		if (_dxpq0xkr == (int)0)
		{
			
			return;
		}
		else
		if (_dxpq0xkr == (int)1)
		{
			
			*(_plfm7z8g+((int)1 - 1)) = ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+((int)1 - 1)) );
			return;
		}
		else
		if (_dxpq0xkr == (int)2)
		{
			
			_i0q1t486(ref Unsafe.AsRef(*(_plfm7z8g+((int)1 - 1))) ,ref Unsafe.AsRef(*(_864fslqq+((int)1 - 1))) ,ref Unsafe.AsRef(*(_plfm7z8g+((int)2 - 1))) ,ref _sq0ypuw6 ,ref _7d8q5cd7 );
			*(_plfm7z8g+((int)1 - 1)) = _7d8q5cd7;
			*(_plfm7z8g+((int)2 - 1)) = _sq0ypuw6;
			return;
		}
		//* 
		//*     Estimate the largest singular value. 
		//* 
		
		_7d8q5cd7 = _d0547bi2;
		{
			System.Int32 __81fgg2dlsvn296 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step296 = (System.Int32)((int)1);
			System.Int32 __81fgg2count296;
			for (__81fgg2count296 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn296 + __81fgg2step296) / __81fgg2step296)), _b5p6od9s = __81fgg2dlsvn296; __81fgg2count296 != 0; __81fgg2count296--, _b5p6od9s += (__81fgg2step296)) {

			{
				
				*(_plfm7z8g+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_b5p6od9s - 1)) );
				_7d8q5cd7 = ILNumerics.F2NET.Intrinsics.MAX(_7d8q5cd7 ,ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_b5p6od9s - 1)) ) );
Mark10:;
				// continue
			}
						}		}
		*(_plfm7z8g+(_dxpq0xkr - 1)) = ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_dxpq0xkr - 1)) );//* 
		//*     Early return if SIGMX is zero (matrix is already diagonal). 
		//* 
		
		if (_7d8q5cd7 == _d0547bi2)
		{
			
			_agod5jth("D" ,ref _dxpq0xkr ,_plfm7z8g ,ref _itfnbz60 );
			return;
		}
		//* 
		
		{
			System.Int32 __81fgg2dlsvn297 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step297 = (System.Int32)((int)1);
			System.Int32 __81fgg2count297;
			for (__81fgg2count297 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn297 + __81fgg2step297) / __81fgg2step297)), _b5p6od9s = __81fgg2dlsvn297; __81fgg2count297 != 0; __81fgg2count297--, _b5p6od9s += (__81fgg2step297)) {

			{
				
				_7d8q5cd7 = ILNumerics.F2NET.Intrinsics.MAX(_7d8q5cd7 ,*(_plfm7z8g+(_b5p6od9s - 1)) );
Mark20:;
				// continue
			}
						}		}//* 
		//*     Copy D and E into WORK (in the Z format) and scale (squaring the 
		//*     input data makes scaling by a power of the radix pointless). 
		//* 
		
		_p1iqarg6 = _f43eg0w0("Precision" );
		_h75qnr7l = _f43eg0w0("Safe minimum" );
		_1m44vtuk = ILNumerics.F2NET.Intrinsics.SQRT(_p1iqarg6 / _h75qnr7l );
		_gvjhlct0(ref _dxpq0xkr ,_plfm7z8g ,ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 - 1)),ref Unsafe.AsRef((int)2) );
		_gvjhlct0(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,_864fslqq ,ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)2 - 1)),ref Unsafe.AsRef((int)2) );
		_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _7d8q5cd7 ,ref _1m44vtuk ,ref Unsafe.AsRef(((int)2 * _dxpq0xkr) - (int)1) ,ref Unsafe.AsRef((int)1) ,_apig8meb ,ref Unsafe.AsRef(((int)2 * _dxpq0xkr) - (int)1) ,ref _itfnbz60 );//* 
		//*     Compute the q's and e's. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn298 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step298 = (System.Int32)((int)1);
			System.Int32 __81fgg2count298;
			for (__81fgg2count298 = System.Math.Max(0, (System.Int32)(((System.Int32)(((int)2 * _dxpq0xkr) - (int)1) - __81fgg2dlsvn298 + __81fgg2step298) / __81fgg2step298)), _b5p6od9s = __81fgg2dlsvn298; __81fgg2count298 != 0; __81fgg2count298--, _b5p6od9s += (__81fgg2step298)) {

			{
				
				*(_apig8meb+(_b5p6od9s - 1)) = __POW2(*(_apig8meb+(_b5p6od9s - 1)));
Mark30:;
				// continue
			}
						}		}
		*(_apig8meb+((int)2 * _dxpq0xkr - 1)) = _d0547bi2;//* 
		
		_i4v7i21v(ref _dxpq0xkr ,_apig8meb ,ref _gro5yvfo );//* 
		
		if (_gro5yvfo == (int)0)
		{
			
			{
				System.Int32 __81fgg2dlsvn299 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step299 = (System.Int32)((int)1);
				System.Int32 __81fgg2count299;
				for (__81fgg2count299 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn299 + __81fgg2step299) / __81fgg2step299)), _b5p6od9s = __81fgg2dlsvn299; __81fgg2count299 != 0; __81fgg2count299--, _b5p6od9s += (__81fgg2step299)) {

				{
					
					*(_plfm7z8g+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.SQRT(*(_apig8meb+(_b5p6od9s - 1)) );
Mark40:;
					// continue
				}
								}			}
			_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _1m44vtuk ,ref _7d8q5cd7 ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1) ,_plfm7z8g ,ref _dxpq0xkr ,ref _itfnbz60 );
		}
		else
		if (_gro5yvfo == (int)2)
		{
			//* 
			//*     Maximum number of iterations exceeded.  Move data from WORK 
			//*     into D and E so the calling subroutine can try to finish 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn300 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step300 = (System.Int32)((int)1);
				System.Int32 __81fgg2count300;
				for (__81fgg2count300 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn300 + __81fgg2step300) / __81fgg2step300)), _b5p6od9s = __81fgg2dlsvn300; __81fgg2count300 != 0; __81fgg2count300--, _b5p6od9s += (__81fgg2step300)) {

				{
					
					*(_plfm7z8g+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.SQRT(*(_apig8meb+(((int)2 * _b5p6od9s) - (int)1 - 1)) );
					*(_864fslqq+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.SQRT(*(_apig8meb+((int)2 * _b5p6od9s - 1)) );
				}
								}			}
			_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _1m44vtuk ,ref _7d8q5cd7 ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1) ,_plfm7z8g ,ref _dxpq0xkr ,ref _itfnbz60 );
			_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _1m44vtuk ,ref _7d8q5cd7 ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1) ,_864fslqq ,ref _dxpq0xkr ,ref _itfnbz60 );
		}
		//* 
		
		return;//* 
		//*     End of DLASQ1 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
