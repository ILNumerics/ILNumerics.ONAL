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
//*> \brief \b CLATRS solves a triangular system of equations with the scale factor set to prevent overflow. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CLATRS + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/clatrs.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/clatrs.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/clatrs.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CLATRS( UPLO, TRANS, DIAG, NORMIN, N, A, LDA, X, SCALE, 
//*                          CNORM, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          DIAG, NORMIN, TRANS, UPLO 
//*       INTEGER            INFO, LDA, N 
//*       REAL               SCALE 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               CNORM( * ) 
//*       COMPLEX            A( LDA, * ), X( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CLATRS solves one of the triangular systems 
//*> 
//*>    A * x = s*b,  A**T * x = s*b,  or  A**H * x = s*b, 
//*> 
//*> with scaling to prevent overflow.  Here A is an upper or lower 
//*> triangular matrix, A**T denotes the transpose of A, A**H denotes the 
//*> conjugate transpose of A, x and b are n-element vectors, and s is a 
//*> scaling factor, usually less than or equal to 1, chosen so that the 
//*> components of x will be less than the overflow threshold.  If the 
//*> unscaled problem will not cause overflow, the Level 2 BLAS routine 
//*> CTRSV is called. If the matrix A is singular (A(j,j) = 0 for some j), 
//*> then s is set to 0 and a non-trivial solution to A*x = 0 is returned. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>          Specifies whether the matrix A is upper or lower triangular. 
//*>          = 'U':  Upper triangular 
//*>          = 'L':  Lower triangular 
//*> \endverbatim 
//*> 
//*> \param[in] TRANS 
//*> \verbatim 
//*>          TRANS is CHARACTER*1 
//*>          Specifies the operation applied to A. 
//*>          = 'N':  Solve A * x = s*b     (No transpose) 
//*>          = 'T':  Solve A**T * x = s*b  (Transpose) 
//*>          = 'C':  Solve A**H * x = s*b  (Conjugate transpose) 
//*> \endverbatim 
//*> 
//*> \param[in] DIAG 
//*> \verbatim 
//*>          DIAG is CHARACTER*1 
//*>          Specifies whether or not the matrix A is unit triangular. 
//*>          = 'N':  Non-unit triangular 
//*>          = 'U':  Unit triangular 
//*> \endverbatim 
//*> 
//*> \param[in] NORMIN 
//*> \verbatim 
//*>          NORMIN is CHARACTER*1 
//*>          Specifies whether CNORM has been set or not. 
//*>          = 'Y':  CNORM contains the column norms on entry 
//*>          = 'N':  CNORM is not set on entry.  On exit, the norms will 
//*>                  be computed and stored in CNORM. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix A.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is COMPLEX array, dimension (LDA,N) 
//*>          The triangular matrix A.  If UPLO = 'U', the leading n by n 
//*>          upper triangular part of the array A contains the upper 
//*>          triangular matrix, and the strictly lower triangular part of 
//*>          A is not referenced.  If UPLO = 'L', the leading n by n lower 
//*>          triangular part of the array A contains the lower triangular 
//*>          matrix, and the strictly upper triangular part of A is not 
//*>          referenced.  If DIAG = 'U', the diagonal elements of A are 
//*>          also not referenced and are assumed to be 1. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max (1,N). 
//*> \endverbatim 
//*> 
//*> \param[in,out] X 
//*> \verbatim 
//*>          X is COMPLEX array, dimension (N) 
//*>          On entry, the right hand side b of the triangular system. 
//*>          On exit, X is overwritten by the solution vector x. 
//*> \endverbatim 
//*> 
//*> \param[out] SCALE 
//*> \verbatim 
//*>          SCALE is REAL 
//*>          The scaling factor s for the triangular system 
//*>             A * x = s*b,  A**T * x = s*b,  or  A**H * x = s*b. 
//*>          If SCALE = 0, the matrix A is singular or badly scaled, and 
//*>          the vector x is an exact or approximate solution to A*x = 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] CNORM 
//*> \verbatim 
//*>          CNORM is REAL array, dimension (N) 
//*> 
//*>          If NORMIN = 'Y', CNORM is an input argument and CNORM(j) 
//*>          contains the norm of the off-diagonal part of the j-th column 
//*>          of A.  If TRANS = 'N', CNORM(j) must be greater than or equal 
//*>          to the infinity-norm, and if TRANS = 'T' or 'C', CNORM(j) 
//*>          must be greater than or equal to the 1-norm. 
//*> 
//*>          If NORMIN = 'N', CNORM is an output argument and CNORM(j) 
//*>          returns the 1-norm of the offdiagonal part of the j-th column 
//*>          of A. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit 
//*>          < 0:  if INFO = -k, the k-th argument had an illegal value 
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
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  A rough bound on x is computed; if that is less than overflow, CTRSV 
//*>  is called, otherwise, specific code is used which checks for possible 
//*>  overflow or divide-by-zero at every operation. 
//*> 
//*>  A columnwise scheme is used for solving A*x = b.  The basic algorithm 
//*>  if A is lower triangular is 
//*> 
//*>       x[1:n] := b[1:n] 
//*>       for j = 1, ..., n 
//*>            x(j) := x(j) / A(j,j) 
//*>            x[j+1:n] := x[j+1:n] - x(j) * A[j+1:n,j] 
//*>       end 
//*> 
//*>  Define bounds on the components of x after j iterations of the loop: 
//*>     M(j) = bound on x[1:j] 
//*>     G(j) = bound on x[j+1:n] 
//*>  Initially, let M(0) = 0 and G(0) = max{x(i), i=1,...,n}. 
//*> 
//*>  Then for iteration j+1 we have 
//*>     M(j+1) <= G(j) / | A(j+1,j+1) | 
//*>     G(j+1) <= G(j) + M(j+1) * | A[j+2:n,j+1] | 
//*>            <= G(j) ( 1 + CNORM(j+1) / | A(j+1,j+1) | ) 
//*> 
//*>  where CNORM(j+1) is greater than or equal to the infinity-norm of 
//*>  column j+1 of A, not counting the diagonal.  Hence 
//*> 
//*>     G(j) <= G(0) product ( 1 + CNORM(i) / | A(i,i) | ) 
//*>                  1<=i<=j 
//*>  and 
//*> 
//*>     |x(j)| <= ( G(0) / |A(j,j)| ) product ( 1 + CNORM(i) / |A(i,i)| ) 
//*>                                   1<=i< j 
//*> 
//*>  Since |x(j)| <= M(j), we use the Level 2 BLAS routine CTRSV if the 
//*>  reciprocal of the largest M(j), j=1,..,n, is larger than 
//*>  max(underflow, 1/overflow). 
//*> 
//*>  The bound on x(j) is also used to determine when a step in the 
//*>  columnwise method can be performed without fear of overflow.  If 
//*>  the computed bound is greater than a large constant, x is scaled to 
//*>  prevent overflow, but if the bound overflows, x is set to 0, x(j) to 
//*>  1, and scale to 0, and a non-trivial solution to A*x = 0 is found. 
//*> 
//*>  Similarly, a row-wise scheme is used to solve A**T *x = b  or 
//*>  A**H *x = b.  The basic algorithm for A upper triangular is 
//*> 
//*>       for j = 1, ..., n 
//*>            x(j) := ( b(j) - A[1:j-1,j]' * x[1:j-1] ) / A(j,j) 
//*>       end 
//*> 
//*>  We simultaneously compute two bounds 
//*>       G(j) = bound on ( b(i) - A[1:i-1,i]' * x[1:i-1] ), 1<=i<=j 
//*>       M(j) = bound on x(i), 1<=i<=j 
//*> 
//*>  The initial values are G(0) = 0, M(0) = max{b(i), i=1,..,n}, and we 
//*>  add the constraint G(j) >= G(j-1) and M(j) >= M(j-1) for j >= 1. 
//*>  Then the bound on x(j) is 
//*> 
//*>       M(j) <= M(j-1) * ( 1 + CNORM(j) ) / | A(j,j) | 
//*> 
//*>            <= M(0) * product ( ( 1 + CNORM(i) ) / |A(i,i)| ) 
//*>                      1<=i<=j 
//*> 
//*>  and we can safely call CTRSV if 1/M(n) and 1/G(n) are both greater 
//*>  than max(underflow, 1/overflow). 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _i8j3yqqn(FString _9wyre9zc, FString _scuo79v4, FString _2scffxp3, FString _t2799vyr, ref Int32 _dxpq0xkr, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, fcomplex* _ta7zuy9k, ref Single _1m44vtuk, Single* _5wpr31sx, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Single _gbf4169i =  0.5f;
Single _kxg5drh2 =  1f;
Single _5m0mjfxm =  2f;
Boolean _2bzw4gjb =  default;
Boolean _rcjmgxm4 =  default;
Boolean _l08igmvf =  default;
Int32 _b5p6od9s =  default;
Int32 _gmv1yrg4 =  default;
Int32 _znpjgsef =  default;
Int32 _wmosh54i =  default;
Int32 _6545j6h8 =  default;
Int32 _4qkl4wkr =  default;
Single _av7j8yda =  default;
Single _k4zaopyq =  default;
Single _egqfb6nh =  default;
Single _bogm0gwy =  default;
Single _c8zglj2w =  default;
Single _szk1rfo3 =  default;
Single _v6csnizn =  default;
Single _eb63wudx =  default;
Single _rtkm3mk9 =  default;
Single _u2jk5kqa =  default;
fcomplex _u8zf5xch =  default;
fcomplex _x79suc10 =  default;
fcomplex _qbkm3zub =  default;
fcomplex _8jjrmha3 =  default;
string fLanavab = default;
#endregion  variable declarations
_9wyre9zc = _9wyre9zc.Convert(1);
_scuo79v4 = _scuo79v4.Convert(1);
_2scffxp3 = _2scffxp3.Convert(1);
_t2799vyr = _t2799vyr.Convert(1);

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
		//*     .. Statement Functions .. 
		//*     .. 
		//*     .. Statement Function definitions .. 
		
		
		Func<fcomplex,Single> _4jqx89by = (_em6ns547) => { return (ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.REAL(_em6ns547 ) ) + ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.AIMAG(_em6ns547 ) )); };;
		
		Func<fcomplex,Single> _vxgsqi77 = (_em6ns547) => { return (ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.REAL(_em6ns547 ) / 2f ) + ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.AIMAG(_em6ns547 ) / 2f )); };;//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		_gro5yvfo = (int)0;
		_l08igmvf = _w8y2rzgy(_9wyre9zc ,"U" );
		_2bzw4gjb = _w8y2rzgy(_scuo79v4 ,"N" );
		_rcjmgxm4 = _w8y2rzgy(_2scffxp3 ,"N" );//* 
		//*     Test the input parameters. 
		//* 
		
		if ((!(_l08igmvf)) & (!(_w8y2rzgy(_9wyre9zc ,"L" ))))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (((!(_2bzw4gjb)) & (!(_w8y2rzgy(_scuo79v4 ,"T" )))) & (!(_w8y2rzgy(_scuo79v4 ,"C" ))))
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if ((!(_rcjmgxm4)) & (!(_w8y2rzgy(_2scffxp3 ,"U" ))))
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if ((!(_w8y2rzgy(_t2799vyr ,"Y" ))) & (!(_w8y2rzgy(_t2799vyr ,"N" ))))
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-5;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)-7;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("CLATRS" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr == (int)0)
		return;//* 
		//*     Determine machine dependent parameters to control overflow. 
		//* 
		
		_bogm0gwy = _d5tu038y("Safe minimum" );
		_av7j8yda = (_kxg5drh2 / _bogm0gwy);
		_6cljvt6b(ref _bogm0gwy ,ref _av7j8yda );
		_bogm0gwy = (_bogm0gwy / _d5tu038y("Precision" ));
		_av7j8yda = (_kxg5drh2 / _bogm0gwy);
		_1m44vtuk = _kxg5drh2;//* 
		
		if (_w8y2rzgy(_t2799vyr ,"N" ))
		{
			//* 
			//*        Compute the 1-norm of each column, not including the diagonal. 
			//* 
			
			if (_l08igmvf)
			{
				//* 
				//*           A is upper triangular. 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn2596 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2596 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2596;
					for (__81fgg2count2596 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2596 + __81fgg2step2596) / __81fgg2step2596)), _znpjgsef = __81fgg2dlsvn2596; __81fgg2count2596 != 0; __81fgg2count2596--, _znpjgsef += (__81fgg2step2596)) {

					{
						
						*(_5wpr31sx+(_znpjgsef - 1)) = _ojoz4216(ref Unsafe.AsRef(_znpjgsef - (int)1) ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
Mark10:;
						// continue
					}
										}				}
			}
			else
			{
				//* 
				//*           A is lower triangular. 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn2597 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2597 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2597;
					for (__81fgg2count2597 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn2597 + __81fgg2step2597) / __81fgg2step2597)), _znpjgsef = __81fgg2dlsvn2597; __81fgg2count2597 != 0; __81fgg2count2597--, _znpjgsef += (__81fgg2step2597)) {

					{
						
						*(_5wpr31sx+(_znpjgsef - 1)) = _ojoz4216(ref Unsafe.AsRef(_dxpq0xkr - _znpjgsef) ,(_vxfgpup9+(_znpjgsef + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
Mark20:;
						// continue
					}
										}				}
				*(_5wpr31sx+(_dxpq0xkr - 1)) = _d0547bi2;
			}
			
		}
		//* 
		//*     Scale the column norms by TSCAL if the maximum element in CNORM is 
		//*     greater than BIGNUM/2. 
		//* 
		
		_gmv1yrg4 = _z5b2nqbf(ref _dxpq0xkr ,_5wpr31sx ,ref Unsafe.AsRef((int)1) );
		_szk1rfo3 = *(_5wpr31sx+(_gmv1yrg4 - 1));
		if (_szk1rfo3 <= (_av7j8yda * _gbf4169i))
		{
			
			_v6csnizn = _kxg5drh2;
		}
		else
		{
			
			_v6csnizn = (_gbf4169i / (_bogm0gwy * _szk1rfo3));
			_ct5qqrv7(ref _dxpq0xkr ,ref _v6csnizn ,_5wpr31sx ,ref Unsafe.AsRef((int)1) );
		}
		//* 
		//*     Compute a bound on the computed solution vector to see if the 
		//*     Level 2 BLAS routine CTRSV can be used. 
		//* 
		
		_u2jk5kqa = _d0547bi2;
		{
			System.Int32 __81fgg2dlsvn2598 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2598 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2598;
			for (__81fgg2count2598 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2598 + __81fgg2step2598) / __81fgg2step2598)), _znpjgsef = __81fgg2dlsvn2598; __81fgg2count2598 != 0; __81fgg2count2598--, _znpjgsef += (__81fgg2step2598)) {

			{
				
				_u2jk5kqa = ILNumerics.F2NET.Intrinsics.MAX(_u2jk5kqa ,_vxgsqi77(*(_ta7zuy9k+(_znpjgsef - 1)) ) );
Mark30:;
				// continue
			}
						}		}
		_eb63wudx = _u2jk5kqa;//* 
		
		if (_2bzw4gjb)
		{
			//* 
			//*        Compute the growth in A * x = b. 
			//* 
			
			if (_l08igmvf)
			{
				
				_wmosh54i = _dxpq0xkr;
				_4qkl4wkr = (int)1;
				_6545j6h8 = (int)-1;
			}
			else
			{
				
				_wmosh54i = (int)1;
				_4qkl4wkr = _dxpq0xkr;
				_6545j6h8 = (int)1;
			}
			//* 
			
			if (_v6csnizn != _kxg5drh2)
			{
				
				_k4zaopyq = _d0547bi2;goto Mark60;
			}
			//* 
			
			if (_rcjmgxm4)
			{
				//* 
				//*           A is non-unit triangular. 
				//* 
				//*           Compute GROW = 1/G(j) and XBND = 1/M(j). 
				//*           Initially, G(0) = max{x(i), i=1,...,n}. 
				//* 
				
				_k4zaopyq = (_gbf4169i / ILNumerics.F2NET.Intrinsics.MAX(_eb63wudx ,_bogm0gwy ));
				_eb63wudx = _k4zaopyq;
				{
					System.Int32 __81fgg2dlsvn2599 = (System.Int32)(_wmosh54i);
					System.Int32 __81fgg2step2599 = (System.Int32)(_6545j6h8);
					System.Int32 __81fgg2count2599;
					for (__81fgg2count2599 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4qkl4wkr) - __81fgg2dlsvn2599 + __81fgg2step2599) / __81fgg2step2599)), _znpjgsef = __81fgg2dlsvn2599; __81fgg2count2599 != 0; __81fgg2count2599--, _znpjgsef += (__81fgg2step2599)) {

					{
						//* 
						//*              Exit the loop if the growth factor is too small. 
						//* 
						
						if (_k4zaopyq <= _bogm0gwy)goto Mark60;//* 
						
						_x79suc10 = *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c));
						_c8zglj2w = _4jqx89by(_x79suc10 );//* 
						
						if (_c8zglj2w >= _bogm0gwy)
						{
							//* 
							//*                 M(j) = G(j-1) / abs(A(j,j)) 
							//* 
							
							_eb63wudx = ILNumerics.F2NET.Intrinsics.MIN(_eb63wudx ,ILNumerics.F2NET.Intrinsics.MIN(_kxg5drh2 ,_c8zglj2w ) * _k4zaopyq );
						}
						else
						{
							//* 
							//*                 M(j) could overflow, set XBND to 0. 
							//* 
							
							_eb63wudx = _d0547bi2;
						}
						//* 
						
						if ((_c8zglj2w + *(_5wpr31sx+(_znpjgsef - 1))) >= _bogm0gwy)
						{
							//* 
							//*                 G(j) = G(j-1)*( 1 + CNORM(j) / abs(A(j,j)) ) 
							//* 
							
							_k4zaopyq = (_k4zaopyq * (_c8zglj2w / (_c8zglj2w + *(_5wpr31sx+(_znpjgsef - 1)))));
						}
						else
						{
							//* 
							//*                 G(j) could overflow, set GROW to 0. 
							//* 
							
							_k4zaopyq = _d0547bi2;
						}
						
Mark40:;
						// continue
					}
										}				}
				_k4zaopyq = _eb63wudx;
			}
			else
			{
				//* 
				//*           A is unit triangular. 
				//* 
				//*           Compute GROW = 1/G(j), where G(0) = max{x(i), i=1,...,n}. 
				//* 
				
				_k4zaopyq = ILNumerics.F2NET.Intrinsics.MIN(_kxg5drh2 ,_gbf4169i / ILNumerics.F2NET.Intrinsics.MAX(_eb63wudx ,_bogm0gwy ) );
				{
					System.Int32 __81fgg2dlsvn2600 = (System.Int32)(_wmosh54i);
					System.Int32 __81fgg2step2600 = (System.Int32)(_6545j6h8);
					System.Int32 __81fgg2count2600;
					for (__81fgg2count2600 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4qkl4wkr) - __81fgg2dlsvn2600 + __81fgg2step2600) / __81fgg2step2600)), _znpjgsef = __81fgg2dlsvn2600; __81fgg2count2600 != 0; __81fgg2count2600--, _znpjgsef += (__81fgg2step2600)) {

					{
						//* 
						//*              Exit the loop if the growth factor is too small. 
						//* 
						
						if (_k4zaopyq <= _bogm0gwy)goto Mark60;//* 
						//*              G(j) = G(j-1)*( 1 + CNORM(j) ) 
						//* 
						
						_k4zaopyq = (_k4zaopyq * (_kxg5drh2 / (_kxg5drh2 + *(_5wpr31sx+(_znpjgsef - 1)))));
Mark50:;
						// continue
					}
										}				}
			}
			
Mark60:;
			// continue//* 
			
		}
		else
		{
			//* 
			//*        Compute the growth in A**T * x = b  or  A**H * x = b. 
			//* 
			
			if (_l08igmvf)
			{
				
				_wmosh54i = (int)1;
				_4qkl4wkr = _dxpq0xkr;
				_6545j6h8 = (int)1;
			}
			else
			{
				
				_wmosh54i = _dxpq0xkr;
				_4qkl4wkr = (int)1;
				_6545j6h8 = (int)-1;
			}
			//* 
			
			if (_v6csnizn != _kxg5drh2)
			{
				
				_k4zaopyq = _d0547bi2;goto Mark90;
			}
			//* 
			
			if (_rcjmgxm4)
			{
				//* 
				//*           A is non-unit triangular. 
				//* 
				//*           Compute GROW = 1/G(j) and XBND = 1/M(j). 
				//*           Initially, M(0) = max{x(i), i=1,...,n}. 
				//* 
				
				_k4zaopyq = (_gbf4169i / ILNumerics.F2NET.Intrinsics.MAX(_eb63wudx ,_bogm0gwy ));
				_eb63wudx = _k4zaopyq;
				{
					System.Int32 __81fgg2dlsvn2601 = (System.Int32)(_wmosh54i);
					System.Int32 __81fgg2step2601 = (System.Int32)(_6545j6h8);
					System.Int32 __81fgg2count2601;
					for (__81fgg2count2601 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4qkl4wkr) - __81fgg2dlsvn2601 + __81fgg2step2601) / __81fgg2step2601)), _znpjgsef = __81fgg2dlsvn2601; __81fgg2count2601 != 0; __81fgg2count2601--, _znpjgsef += (__81fgg2step2601)) {

					{
						//* 
						//*              Exit the loop if the growth factor is too small. 
						//* 
						
						if (_k4zaopyq <= _bogm0gwy)goto Mark90;//* 
						//*              G(j) = max( G(j-1), M(j-1)*( 1 + CNORM(j) ) ) 
						//* 
						
						_rtkm3mk9 = (_kxg5drh2 + *(_5wpr31sx+(_znpjgsef - 1)));
						_k4zaopyq = ILNumerics.F2NET.Intrinsics.MIN(_k4zaopyq ,_eb63wudx / _rtkm3mk9 );//* 
						
						_x79suc10 = *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c));
						_c8zglj2w = _4jqx89by(_x79suc10 );//* 
						
						if (_c8zglj2w >= _bogm0gwy)
						{
							//* 
							//*                 M(j) = M(j-1)*( 1 + CNORM(j) ) / abs(A(j,j)) 
							//* 
							
							if (_rtkm3mk9 > _c8zglj2w)
							_eb63wudx = (_eb63wudx * (_c8zglj2w / _rtkm3mk9));
						}
						else
						{
							//* 
							//*                 M(j) could overflow, set XBND to 0. 
							//* 
							
							_eb63wudx = _d0547bi2;
						}
						
Mark70:;
						// continue
					}
										}				}
				_k4zaopyq = ILNumerics.F2NET.Intrinsics.MIN(_k4zaopyq ,_eb63wudx );
			}
			else
			{
				//* 
				//*           A is unit triangular. 
				//* 
				//*           Compute GROW = 1/G(j), where G(0) = max{x(i), i=1,...,n}. 
				//* 
				
				_k4zaopyq = ILNumerics.F2NET.Intrinsics.MIN(_kxg5drh2 ,_gbf4169i / ILNumerics.F2NET.Intrinsics.MAX(_eb63wudx ,_bogm0gwy ) );
				{
					System.Int32 __81fgg2dlsvn2602 = (System.Int32)(_wmosh54i);
					System.Int32 __81fgg2step2602 = (System.Int32)(_6545j6h8);
					System.Int32 __81fgg2count2602;
					for (__81fgg2count2602 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4qkl4wkr) - __81fgg2dlsvn2602 + __81fgg2step2602) / __81fgg2step2602)), _znpjgsef = __81fgg2dlsvn2602; __81fgg2count2602 != 0; __81fgg2count2602--, _znpjgsef += (__81fgg2step2602)) {

					{
						//* 
						//*              Exit the loop if the growth factor is too small. 
						//* 
						
						if (_k4zaopyq <= _bogm0gwy)goto Mark90;//* 
						//*              G(j) = ( 1 + CNORM(j) )*G(j-1) 
						//* 
						
						_rtkm3mk9 = (_kxg5drh2 + *(_5wpr31sx+(_znpjgsef - 1)));
						_k4zaopyq = (_k4zaopyq / _rtkm3mk9);
Mark80:;
						// continue
					}
										}				}
			}
			
Mark90:;
			// continue
		}
		//* 
		
		if ((_k4zaopyq * _v6csnizn) > _bogm0gwy)
		{
			//* 
			//*        Use the Level 2 BLAS solve if the reciprocal of the bound on 
			//*        elements of X is not too small. 
			//* 
			
			_354qig2c(_9wyre9zc ,_scuo79v4 ,_2scffxp3 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
		}
		else
		{
			//* 
			//*        Use a Level 1 BLAS solve, scaling intermediate results. 
			//* 
			
			if (_u2jk5kqa > (_av7j8yda * _gbf4169i))
			{
				//* 
				//*           Scale X so that its components are less than or equal to 
				//*           BIGNUM in absolute value. 
				//* 
				
				_1m44vtuk = ((_av7j8yda * _gbf4169i) / _u2jk5kqa);
				_2ylagitj(ref _dxpq0xkr ,ref _1m44vtuk ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
				_u2jk5kqa = _av7j8yda;
			}
			else
			{
				
				_u2jk5kqa = (_u2jk5kqa * _5m0mjfxm);
			}
			//* 
			
			if (_2bzw4gjb)
			{
				//* 
				//*           Solve A * x = b 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn2603 = (System.Int32)(_wmosh54i);
					System.Int32 __81fgg2step2603 = (System.Int32)(_6545j6h8);
					System.Int32 __81fgg2count2603;
					for (__81fgg2count2603 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4qkl4wkr) - __81fgg2dlsvn2603 + __81fgg2step2603) / __81fgg2step2603)), _znpjgsef = __81fgg2dlsvn2603; __81fgg2count2603 != 0; __81fgg2count2603--, _znpjgsef += (__81fgg2step2603)) {

					{
						//* 
						//*              Compute x(j) = b(j) / A(j,j), scaling x if necessary. 
						//* 
						
						_rtkm3mk9 = _4jqx89by(*(_ta7zuy9k+(_znpjgsef - 1)) );
						if (_rcjmgxm4)
						{
							
							_x79suc10 = (*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * _v6csnizn);
						}
						else
						{
							
							_x79suc10 = CMPLX(_v6csnizn);
							if (_v6csnizn == _kxg5drh2)goto Mark105;
						}
						
						_c8zglj2w = _4jqx89by(_x79suc10 );
						if (_c8zglj2w > _bogm0gwy)
						{
							//* 
							//*                    abs(A(j,j)) > SMLNUM: 
							//* 
							
							if (_c8zglj2w < _kxg5drh2)
							{
								
								if (_rtkm3mk9 > (_c8zglj2w * _av7j8yda))
								{
									//* 
									//*                          Scale x by 1/b(j). 
									//* 
									
									_egqfb6nh = (_kxg5drh2 / _rtkm3mk9);
									_2ylagitj(ref _dxpq0xkr ,ref _egqfb6nh ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
									_1m44vtuk = (_1m44vtuk * _egqfb6nh);
									_u2jk5kqa = (_u2jk5kqa * _egqfb6nh);
								}
								
							}
							
							*(_ta7zuy9k+(_znpjgsef - 1)) = _r6l3poxb(ref Unsafe.AsRef(*(_ta7zuy9k+(_znpjgsef - 1))) ,ref _x79suc10 );
							_rtkm3mk9 = _4jqx89by(*(_ta7zuy9k+(_znpjgsef - 1)) );
						}
						else
						if (_c8zglj2w > _d0547bi2)
						{
							//* 
							//*                    0 < abs(A(j,j)) <= SMLNUM: 
							//* 
							
							if (_rtkm3mk9 > (_c8zglj2w * _av7j8yda))
							{
								//* 
								//*                       Scale x by (1/abs(x(j)))*abs(A(j,j))*BIGNUM 
								//*                       to avoid overflow when dividing by A(j,j). 
								//* 
								
								_egqfb6nh = ((_c8zglj2w * _av7j8yda) / _rtkm3mk9);
								if (*(_5wpr31sx+(_znpjgsef - 1)) > _kxg5drh2)
								{
									//* 
									//*                          Scale by 1/CNORM(j) to avoid overflow when 
									//*                          multiplying x(j) times column j. 
									//* 
									
									_egqfb6nh = (_egqfb6nh / *(_5wpr31sx+(_znpjgsef - 1)));
								}
								
								_2ylagitj(ref _dxpq0xkr ,ref _egqfb6nh ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
								_1m44vtuk = (_1m44vtuk * _egqfb6nh);
								_u2jk5kqa = (_u2jk5kqa * _egqfb6nh);
							}
							
							*(_ta7zuy9k+(_znpjgsef - 1)) = _r6l3poxb(ref Unsafe.AsRef(*(_ta7zuy9k+(_znpjgsef - 1))) ,ref _x79suc10 );
							_rtkm3mk9 = _4jqx89by(*(_ta7zuy9k+(_znpjgsef - 1)) );
						}
						else
						{
							//* 
							//*                    A(j,j) = 0:  Set x(1:n) = 0, x(j) = 1, and 
							//*                    scale = 0, and compute a solution to A*x = 0. 
							//* 
							
							{
								System.Int32 __81fgg2dlsvn2604 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step2604 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2604;
								for (__81fgg2count2604 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2604 + __81fgg2step2604) / __81fgg2step2604)), _b5p6od9s = __81fgg2dlsvn2604; __81fgg2count2604 != 0; __81fgg2count2604--, _b5p6od9s += (__81fgg2step2604)) {

								{
									
									*(_ta7zuy9k+(_b5p6od9s - 1)) = CMPLX(_d0547bi2);
Mark100:;
									// continue
								}
																}							}
							*(_ta7zuy9k+(_znpjgsef - 1)) = CMPLX(_kxg5drh2);
							_rtkm3mk9 = _kxg5drh2;
							_1m44vtuk = _d0547bi2;
							_u2jk5kqa = _d0547bi2;
						}
						
Mark105:;
						// continue//* 
						//*              Scale x if necessary to avoid overflow when adding a 
						//*              multiple of column j of A. 
						//* 
						
						if (_rtkm3mk9 > _kxg5drh2)
						{
							
							_egqfb6nh = (_kxg5drh2 / _rtkm3mk9);
							if (*(_5wpr31sx+(_znpjgsef - 1)) > ((_av7j8yda - _u2jk5kqa) * _egqfb6nh))
							{
								//* 
								//*                    Scale x by 1/(2*abs(x(j))). 
								//* 
								
								_egqfb6nh = (_egqfb6nh * _gbf4169i);
								_2ylagitj(ref _dxpq0xkr ,ref _egqfb6nh ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
								_1m44vtuk = (_1m44vtuk * _egqfb6nh);
							}
							
						}
						else
						if ((_rtkm3mk9 * *(_5wpr31sx+(_znpjgsef - 1))) > (_av7j8yda - _u2jk5kqa))
						{
							//* 
							//*                 Scale x by 1/2. 
							//* 
							
							_2ylagitj(ref _dxpq0xkr ,ref Unsafe.AsRef(_gbf4169i) ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
							_1m44vtuk = (_1m44vtuk * _gbf4169i);
						}
						//* 
						
						if (_l08igmvf)
						{
							
							if (_znpjgsef > (int)1)
							{
								//* 
								//*                    Compute the update 
								//*                       x(1:j-1) := x(1:j-1) - x(j) * A(1:j-1,j) 
								//* 
								
								_0vz4nsob(ref Unsafe.AsRef(_znpjgsef - (int)1) ,ref Unsafe.AsRef(-((*(_ta7zuy9k+(_znpjgsef - 1)) * _v6csnizn))) ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
								_b5p6od9s = _r3truie3(ref Unsafe.AsRef(_znpjgsef - (int)1) ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
								_u2jk5kqa = _4jqx89by(*(_ta7zuy9k+(_b5p6od9s - 1)) );
							}
							
						}
						else
						{
							
							if (_znpjgsef < _dxpq0xkr)
							{
								//* 
								//*                    Compute the update 
								//*                       x(j+1:n) := x(j+1:n) - x(j) * A(j+1:n,j) 
								//* 
								
								_0vz4nsob(ref Unsafe.AsRef(_dxpq0xkr - _znpjgsef) ,ref Unsafe.AsRef(-((*(_ta7zuy9k+(_znpjgsef - 1)) * _v6csnizn))) ,(_vxfgpup9+(_znpjgsef + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,(_ta7zuy9k+(_znpjgsef + (int)1 - 1)),ref Unsafe.AsRef((int)1) );
								_b5p6od9s = (_znpjgsef + _r3truie3(ref Unsafe.AsRef(_dxpq0xkr - _znpjgsef) ,(_ta7zuy9k+(_znpjgsef + (int)1 - 1)),ref Unsafe.AsRef((int)1) ));
								_u2jk5kqa = _4jqx89by(*(_ta7zuy9k+(_b5p6od9s - 1)) );
							}
							
						}
						
Mark110:;
						// continue
					}
										}				}//* 
				
			}
			else
			if (_w8y2rzgy(_scuo79v4 ,"T" ))
			{
				//* 
				//*           Solve A**T * x = b 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn2605 = (System.Int32)(_wmosh54i);
					System.Int32 __81fgg2step2605 = (System.Int32)(_6545j6h8);
					System.Int32 __81fgg2count2605;
					for (__81fgg2count2605 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4qkl4wkr) - __81fgg2dlsvn2605 + __81fgg2step2605) / __81fgg2step2605)), _znpjgsef = __81fgg2dlsvn2605; __81fgg2count2605 != 0; __81fgg2count2605--, _znpjgsef += (__81fgg2step2605)) {

					{
						//* 
						//*              Compute x(j) = b(j) - sum A(k,j)*x(k). 
						//*                                    k<>j 
						//* 
						
						_rtkm3mk9 = _4jqx89by(*(_ta7zuy9k+(_znpjgsef - 1)) );
						_qbkm3zub = CMPLX(_v6csnizn);
						_egqfb6nh = (_kxg5drh2 / ILNumerics.F2NET.Intrinsics.MAX(_u2jk5kqa ,_kxg5drh2 ));
						if (*(_5wpr31sx+(_znpjgsef - 1)) > ((_av7j8yda - _rtkm3mk9) * _egqfb6nh))
						{
							//* 
							//*                 If x(j) could overflow, scale x by 1/(2*XMAX). 
							//* 
							
							_egqfb6nh = (_egqfb6nh * _gbf4169i);
							if (_rcjmgxm4)
							{
								
								_x79suc10 = (*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * _v6csnizn);
							}
							else
							{
								
								_x79suc10 = CMPLX(_v6csnizn);
							}
							
							_c8zglj2w = _4jqx89by(_x79suc10 );
							if (_c8zglj2w > _kxg5drh2)
							{
								//* 
								//*                       Divide by A(j,j) when scaling x if A(j,j) > 1. 
								//* 
								
								_egqfb6nh = ILNumerics.F2NET.Intrinsics.MIN(_kxg5drh2 ,_egqfb6nh * _c8zglj2w );
								_qbkm3zub = _r6l3poxb(ref _qbkm3zub ,ref _x79suc10 );
							}
							
							if (_egqfb6nh < _kxg5drh2)
							{
								
								_2ylagitj(ref _dxpq0xkr ,ref _egqfb6nh ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
								_1m44vtuk = (_1m44vtuk * _egqfb6nh);
								_u2jk5kqa = (_u2jk5kqa * _egqfb6nh);
							}
							
						}
						//* 
						
						_u8zf5xch = CMPLX(_d0547bi2);
						if (_qbkm3zub == ILNumerics.F2NET.Intrinsics.CMPLX(_kxg5drh2 ))
						{
							//* 
							//*                 If the scaling needed for A in the dot product is 1, 
							//*                 call CDOTU to perform the dot product. 
							//* 
							
							if (_l08igmvf)
							{
								
								_u8zf5xch = _jfmvk035(ref Unsafe.AsRef(_znpjgsef - (int)1) ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
							}
							else
							if (_znpjgsef < _dxpq0xkr)
							{
								
								_u8zf5xch = _jfmvk035(ref Unsafe.AsRef(_dxpq0xkr - _znpjgsef) ,(_vxfgpup9+(_znpjgsef + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,(_ta7zuy9k+(_znpjgsef + (int)1 - 1)),ref Unsafe.AsRef((int)1) );
							}
							
						}
						else
						{
							//* 
							//*                 Otherwise, use in-line code for the dot product. 
							//* 
							
							if (_l08igmvf)
							{
								
								{
									System.Int32 __81fgg2dlsvn2606 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step2606 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2606;
									for (__81fgg2count2606 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn2606 + __81fgg2step2606) / __81fgg2step2606)), _b5p6od9s = __81fgg2dlsvn2606; __81fgg2count2606 != 0; __81fgg2count2606--, _b5p6od9s += (__81fgg2step2606)) {

									{
										
										_u8zf5xch = (_u8zf5xch + ((*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * _qbkm3zub) * *(_ta7zuy9k+(_b5p6od9s - 1))));
Mark120:;
										// continue
									}
																		}								}
							}
							else
							if (_znpjgsef < _dxpq0xkr)
							{
								
								{
									System.Int32 __81fgg2dlsvn2607 = (System.Int32)((_znpjgsef + (int)1));
									const System.Int32 __81fgg2step2607 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2607;
									for (__81fgg2count2607 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2607 + __81fgg2step2607) / __81fgg2step2607)), _b5p6od9s = __81fgg2dlsvn2607; __81fgg2count2607 != 0; __81fgg2count2607--, _b5p6od9s += (__81fgg2step2607)) {

									{
										
										_u8zf5xch = (_u8zf5xch + ((*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * _qbkm3zub) * *(_ta7zuy9k+(_b5p6od9s - 1))));
Mark130:;
										// continue
									}
																		}								}
							}
							
						}
						//* 
						
						if (_qbkm3zub == ILNumerics.F2NET.Intrinsics.CMPLX(_v6csnizn ))
						{
							//* 
							//*                 Compute x(j) := ( x(j) - CSUMJ ) / A(j,j) if 1/A(j,j) 
							//*                 was not used to scale the dotproduct. 
							//* 
							
							*(_ta7zuy9k+(_znpjgsef - 1)) = (*(_ta7zuy9k+(_znpjgsef - 1)) - _u8zf5xch);
							_rtkm3mk9 = _4jqx89by(*(_ta7zuy9k+(_znpjgsef - 1)) );
							if (_rcjmgxm4)
							{
								
								_x79suc10 = (*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * _v6csnizn);
							}
							else
							{
								
								_x79suc10 = CMPLX(_v6csnizn);
								if (_v6csnizn == _kxg5drh2)goto Mark145;
							}
							//* 
							//*                    Compute x(j) = x(j) / A(j,j), scaling if necessary. 
							//* 
							
							_c8zglj2w = _4jqx89by(_x79suc10 );
							if (_c8zglj2w > _bogm0gwy)
							{
								//* 
								//*                       abs(A(j,j)) > SMLNUM: 
								//* 
								
								if (_c8zglj2w < _kxg5drh2)
								{
									
									if (_rtkm3mk9 > (_c8zglj2w * _av7j8yda))
									{
										//* 
										//*                             Scale X by 1/abs(x(j)). 
										//* 
										
										_egqfb6nh = (_kxg5drh2 / _rtkm3mk9);
										_2ylagitj(ref _dxpq0xkr ,ref _egqfb6nh ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
										_1m44vtuk = (_1m44vtuk * _egqfb6nh);
										_u2jk5kqa = (_u2jk5kqa * _egqfb6nh);
									}
									
								}
								
								*(_ta7zuy9k+(_znpjgsef - 1)) = _r6l3poxb(ref Unsafe.AsRef(*(_ta7zuy9k+(_znpjgsef - 1))) ,ref _x79suc10 );
							}
							else
							if (_c8zglj2w > _d0547bi2)
							{
								//* 
								//*                       0 < abs(A(j,j)) <= SMLNUM: 
								//* 
								
								if (_rtkm3mk9 > (_c8zglj2w * _av7j8yda))
								{
									//* 
									//*                          Scale x by (1/abs(x(j)))*abs(A(j,j))*BIGNUM. 
									//* 
									
									_egqfb6nh = ((_c8zglj2w * _av7j8yda) / _rtkm3mk9);
									_2ylagitj(ref _dxpq0xkr ,ref _egqfb6nh ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
									_1m44vtuk = (_1m44vtuk * _egqfb6nh);
									_u2jk5kqa = (_u2jk5kqa * _egqfb6nh);
								}
								
								*(_ta7zuy9k+(_znpjgsef - 1)) = _r6l3poxb(ref Unsafe.AsRef(*(_ta7zuy9k+(_znpjgsef - 1))) ,ref _x79suc10 );
							}
							else
							{
								//* 
								//*                       A(j,j) = 0:  Set x(1:n) = 0, x(j) = 1, and 
								//*                       scale = 0 and compute a solution to A**T *x = 0. 
								//* 
								
								{
									System.Int32 __81fgg2dlsvn2608 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step2608 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2608;
									for (__81fgg2count2608 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2608 + __81fgg2step2608) / __81fgg2step2608)), _b5p6od9s = __81fgg2dlsvn2608; __81fgg2count2608 != 0; __81fgg2count2608--, _b5p6od9s += (__81fgg2step2608)) {

									{
										
										*(_ta7zuy9k+(_b5p6od9s - 1)) = CMPLX(_d0547bi2);
Mark140:;
										// continue
									}
																		}								}
								*(_ta7zuy9k+(_znpjgsef - 1)) = CMPLX(_kxg5drh2);
								_1m44vtuk = _d0547bi2;
								_u2jk5kqa = _d0547bi2;
							}
							
Mark145:;
							// continue
						}
						else
						{
							//* 
							//*                 Compute x(j) := x(j) / A(j,j) - CSUMJ if the dot 
							//*                 product has already been divided by 1/A(j,j). 
							//* 
							
							*(_ta7zuy9k+(_znpjgsef - 1)) = (_r6l3poxb(ref Unsafe.AsRef(*(_ta7zuy9k+(_znpjgsef - 1))) ,ref _x79suc10 ) - _u8zf5xch);
						}
						
						_u2jk5kqa = ILNumerics.F2NET.Intrinsics.MAX(_u2jk5kqa ,_4jqx89by(*(_ta7zuy9k+(_znpjgsef - 1)) ) );
Mark150:;
						// continue
					}
										}				}//* 
				
			}
			else
			{
				//* 
				//*           Solve A**H * x = b 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn2609 = (System.Int32)(_wmosh54i);
					System.Int32 __81fgg2step2609 = (System.Int32)(_6545j6h8);
					System.Int32 __81fgg2count2609;
					for (__81fgg2count2609 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4qkl4wkr) - __81fgg2dlsvn2609 + __81fgg2step2609) / __81fgg2step2609)), _znpjgsef = __81fgg2dlsvn2609; __81fgg2count2609 != 0; __81fgg2count2609--, _znpjgsef += (__81fgg2step2609)) {

					{
						//* 
						//*              Compute x(j) = b(j) - sum A(k,j)*x(k). 
						//*                                    k<>j 
						//* 
						
						_rtkm3mk9 = _4jqx89by(*(_ta7zuy9k+(_znpjgsef - 1)) );
						_qbkm3zub = CMPLX(_v6csnizn);
						_egqfb6nh = (_kxg5drh2 / ILNumerics.F2NET.Intrinsics.MAX(_u2jk5kqa ,_kxg5drh2 ));
						if (*(_5wpr31sx+(_znpjgsef - 1)) > ((_av7j8yda - _rtkm3mk9) * _egqfb6nh))
						{
							//* 
							//*                 If x(j) could overflow, scale x by 1/(2*XMAX). 
							//* 
							
							_egqfb6nh = (_egqfb6nh * _gbf4169i);
							if (_rcjmgxm4)
							{
								
								_x79suc10 = (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * _v6csnizn);
							}
							else
							{
								
								_x79suc10 = CMPLX(_v6csnizn);
							}
							
							_c8zglj2w = _4jqx89by(_x79suc10 );
							if (_c8zglj2w > _kxg5drh2)
							{
								//* 
								//*                       Divide by A(j,j) when scaling x if A(j,j) > 1. 
								//* 
								
								_egqfb6nh = ILNumerics.F2NET.Intrinsics.MIN(_kxg5drh2 ,_egqfb6nh * _c8zglj2w );
								_qbkm3zub = _r6l3poxb(ref _qbkm3zub ,ref _x79suc10 );
							}
							
							if (_egqfb6nh < _kxg5drh2)
							{
								
								_2ylagitj(ref _dxpq0xkr ,ref _egqfb6nh ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
								_1m44vtuk = (_1m44vtuk * _egqfb6nh);
								_u2jk5kqa = (_u2jk5kqa * _egqfb6nh);
							}
							
						}
						//* 
						
						_u8zf5xch = CMPLX(_d0547bi2);
						if (_qbkm3zub == ILNumerics.F2NET.Intrinsics.CMPLX(_kxg5drh2 ))
						{
							//* 
							//*                 If the scaling needed for A in the dot product is 1, 
							//*                 call CDOTC to perform the dot product. 
							//* 
							
							if (_l08igmvf)
							{
								
								_u8zf5xch = _f18dve92(ref Unsafe.AsRef(_znpjgsef - (int)1) ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
							}
							else
							if (_znpjgsef < _dxpq0xkr)
							{
								
								_u8zf5xch = _f18dve92(ref Unsafe.AsRef(_dxpq0xkr - _znpjgsef) ,(_vxfgpup9+(_znpjgsef + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,(_ta7zuy9k+(_znpjgsef + (int)1 - 1)),ref Unsafe.AsRef((int)1) );
							}
							
						}
						else
						{
							//* 
							//*                 Otherwise, use in-line code for the dot product. 
							//* 
							
							if (_l08igmvf)
							{
								
								{
									System.Int32 __81fgg2dlsvn2610 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step2610 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2610;
									for (__81fgg2count2610 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn2610 + __81fgg2step2610) / __81fgg2step2610)), _b5p6od9s = __81fgg2dlsvn2610; __81fgg2count2610 != 0; __81fgg2count2610--, _b5p6od9s += (__81fgg2step2610)) {

									{
										
										_u8zf5xch = (_u8zf5xch + ((ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * _qbkm3zub) * *(_ta7zuy9k+(_b5p6od9s - 1))));
Mark160:;
										// continue
									}
																		}								}
							}
							else
							if (_znpjgsef < _dxpq0xkr)
							{
								
								{
									System.Int32 __81fgg2dlsvn2611 = (System.Int32)((_znpjgsef + (int)1));
									const System.Int32 __81fgg2step2611 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2611;
									for (__81fgg2count2611 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2611 + __81fgg2step2611) / __81fgg2step2611)), _b5p6od9s = __81fgg2dlsvn2611; __81fgg2count2611 != 0; __81fgg2count2611--, _b5p6od9s += (__81fgg2step2611)) {

									{
										
										_u8zf5xch = (_u8zf5xch + ((ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * _qbkm3zub) * *(_ta7zuy9k+(_b5p6od9s - 1))));
Mark170:;
										// continue
									}
																		}								}
							}
							
						}
						//* 
						
						if (_qbkm3zub == ILNumerics.F2NET.Intrinsics.CMPLX(_v6csnizn ))
						{
							//* 
							//*                 Compute x(j) := ( x(j) - CSUMJ ) / A(j,j) if 1/A(j,j) 
							//*                 was not used to scale the dotproduct. 
							//* 
							
							*(_ta7zuy9k+(_znpjgsef - 1)) = (*(_ta7zuy9k+(_znpjgsef - 1)) - _u8zf5xch);
							_rtkm3mk9 = _4jqx89by(*(_ta7zuy9k+(_znpjgsef - 1)) );
							if (_rcjmgxm4)
							{
								
								_x79suc10 = (ILNumerics.F2NET.Intrinsics.CONJG(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * _v6csnizn);
							}
							else
							{
								
								_x79suc10 = CMPLX(_v6csnizn);
								if (_v6csnizn == _kxg5drh2)goto Mark185;
							}
							//* 
							//*                    Compute x(j) = x(j) / A(j,j), scaling if necessary. 
							//* 
							
							_c8zglj2w = _4jqx89by(_x79suc10 );
							if (_c8zglj2w > _bogm0gwy)
							{
								//* 
								//*                       abs(A(j,j)) > SMLNUM: 
								//* 
								
								if (_c8zglj2w < _kxg5drh2)
								{
									
									if (_rtkm3mk9 > (_c8zglj2w * _av7j8yda))
									{
										//* 
										//*                             Scale X by 1/abs(x(j)). 
										//* 
										
										_egqfb6nh = (_kxg5drh2 / _rtkm3mk9);
										_2ylagitj(ref _dxpq0xkr ,ref _egqfb6nh ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
										_1m44vtuk = (_1m44vtuk * _egqfb6nh);
										_u2jk5kqa = (_u2jk5kqa * _egqfb6nh);
									}
									
								}
								
								*(_ta7zuy9k+(_znpjgsef - 1)) = _r6l3poxb(ref Unsafe.AsRef(*(_ta7zuy9k+(_znpjgsef - 1))) ,ref _x79suc10 );
							}
							else
							if (_c8zglj2w > _d0547bi2)
							{
								//* 
								//*                       0 < abs(A(j,j)) <= SMLNUM: 
								//* 
								
								if (_rtkm3mk9 > (_c8zglj2w * _av7j8yda))
								{
									//* 
									//*                          Scale x by (1/abs(x(j)))*abs(A(j,j))*BIGNUM. 
									//* 
									
									_egqfb6nh = ((_c8zglj2w * _av7j8yda) / _rtkm3mk9);
									_2ylagitj(ref _dxpq0xkr ,ref _egqfb6nh ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
									_1m44vtuk = (_1m44vtuk * _egqfb6nh);
									_u2jk5kqa = (_u2jk5kqa * _egqfb6nh);
								}
								
								*(_ta7zuy9k+(_znpjgsef - 1)) = _r6l3poxb(ref Unsafe.AsRef(*(_ta7zuy9k+(_znpjgsef - 1))) ,ref _x79suc10 );
							}
							else
							{
								//* 
								//*                       A(j,j) = 0:  Set x(1:n) = 0, x(j) = 1, and 
								//*                       scale = 0 and compute a solution to A**H *x = 0. 
								//* 
								
								{
									System.Int32 __81fgg2dlsvn2612 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step2612 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2612;
									for (__81fgg2count2612 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2612 + __81fgg2step2612) / __81fgg2step2612)), _b5p6od9s = __81fgg2dlsvn2612; __81fgg2count2612 != 0; __81fgg2count2612--, _b5p6od9s += (__81fgg2step2612)) {

									{
										
										*(_ta7zuy9k+(_b5p6od9s - 1)) = CMPLX(_d0547bi2);
Mark180:;
										// continue
									}
																		}								}
								*(_ta7zuy9k+(_znpjgsef - 1)) = CMPLX(_kxg5drh2);
								_1m44vtuk = _d0547bi2;
								_u2jk5kqa = _d0547bi2;
							}
							
Mark185:;
							// continue
						}
						else
						{
							//* 
							//*                 Compute x(j) := x(j) / A(j,j) - CSUMJ if the dot 
							//*                 product has already been divided by 1/A(j,j). 
							//* 
							
							*(_ta7zuy9k+(_znpjgsef - 1)) = (_r6l3poxb(ref Unsafe.AsRef(*(_ta7zuy9k+(_znpjgsef - 1))) ,ref _x79suc10 ) - _u8zf5xch);
						}
						
						_u2jk5kqa = ILNumerics.F2NET.Intrinsics.MAX(_u2jk5kqa ,_4jqx89by(*(_ta7zuy9k+(_znpjgsef - 1)) ) );
Mark190:;
						// continue
					}
										}				}
			}
			
			_1m44vtuk = (_1m44vtuk / _v6csnizn);
		}
		//* 
		//*     Scale the column norms by 1/TSCAL for return. 
		//* 
		
		if (_v6csnizn != _kxg5drh2)
		{
			
			_ct5qqrv7(ref _dxpq0xkr ,ref Unsafe.AsRef(_kxg5drh2 / _v6csnizn) ,_5wpr31sx ,ref Unsafe.AsRef((int)1) );
		}
		//* 
		
		return;//* 
		//*     End of CLATRS 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
