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
//*> \brief \b DLASET initializes the off-diagonal elements and the diagonal elements of a matrix to given values. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLASET + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlaset.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlaset.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlaset.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLASET( UPLO, M, N, ALPHA, BETA, A, LDA ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          UPLO 
//*       INTEGER            LDA, M, N 
//*       DOUBLE PRECISION   ALPHA, BETA 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   A( LDA, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLASET initializes an m-by-n matrix A to BETA on the diagonal and 
//*> ALPHA on the offdiagonals. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>          Specifies the part of the matrix A to be set. 
//*>          = 'U':      Upper triangular part is set; the strictly lower 
//*>                      triangular part of A is not changed. 
//*>          = 'L':      Lower triangular part is set; the strictly upper 
//*>                      triangular part of A is not changed. 
//*>          Otherwise:  All of the matrix A is set. 
//*> \endverbatim 
//*> 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of rows of the matrix A.  M >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of columns of the matrix A.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] ALPHA 
//*> \verbatim 
//*>          ALPHA is DOUBLE PRECISION 
//*>          The constant to which the offdiagonal elements are to be set. 
//*> \endverbatim 
//*> 
//*> \param[in] BETA 
//*> \verbatim 
//*>          BETA is DOUBLE PRECISION 
//*>          The constant to which the diagonal elements are to be set. 
//*> \endverbatim 
//*> 
//*> \param[out] A 
//*> \verbatim 
//*>          A is DOUBLE PRECISION array, dimension (LDA,N) 
//*>          On exit, the leading m-by-n submatrix of A is set as follows: 
//*> 
//*>          if UPLO = 'U', A(i,j) = ALPHA, 1<=i<=j-1, 1<=j<=n, 
//*>          if UPLO = 'L', A(i,j) = ALPHA, j+1<=i<=m, 1<=j<=n, 
//*>          otherwise,     A(i,j) = ALPHA, 1<=i<=m, 1<=j<=n, i.ne.j, 
//*> 
//*>          and, for all UPLO, A(i,i) = BETA, 1<=i<=min(m,n). 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,M). 
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
//*> \ingroup OTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _rta9tuwm(FString _9wyre9zc, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Double _r7cfteg3, ref Double _bafcbx97, Double* _vxfgpup9, ref Int32 _ocv8fk5c)
	{
#region variable declarations
Int32 _b5p6od9s =  default;
Int32 _znpjgsef =  default;
string fLanavab = default;
#endregion  variable declarations
_9wyre9zc = _9wyre9zc.Convert(1);

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
		//* ===================================================================== 
		//* 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		if (_w8y2rzgy(_9wyre9zc ,"U" ))
		{
			//* 
			//*        Set the strictly upper triangular or trapezoidal part of the 
			//*        array to ALPHA. 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn227 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step227 = (System.Int32)((int)1);
				System.Int32 __81fgg2count227;
				for (__81fgg2count227 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn227 + __81fgg2step227) / __81fgg2step227)), _znpjgsef = __81fgg2dlsvn227; __81fgg2count227 != 0; __81fgg2count227--, _znpjgsef += (__81fgg2step227)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn228 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step228 = (System.Int32)((int)1);
						System.Int32 __81fgg2count228;
						for (__81fgg2count228 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN(_znpjgsef - (int)1 ,_ev4xhht5 )) - __81fgg2dlsvn228 + __81fgg2step228) / __81fgg2step228)), _b5p6od9s = __81fgg2dlsvn228; __81fgg2count228 != 0; __81fgg2count228--, _b5p6od9s += (__81fgg2step228)) {

						{
							
							*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _r7cfteg3;
Mark10:;
							// continue
						}
												}					}
Mark20:;
					// continue
				}
								}			}//* 
			
		}
		else
		if (_w8y2rzgy(_9wyre9zc ,"L" ))
		{
			//* 
			//*        Set the strictly lower triangular or trapezoidal part of the 
			//*        array to ALPHA. 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn229 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step229 = (System.Int32)((int)1);
				System.Int32 __81fgg2count229;
				for (__81fgg2count229 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_dxpq0xkr )) - __81fgg2dlsvn229 + __81fgg2step229) / __81fgg2step229)), _znpjgsef = __81fgg2dlsvn229; __81fgg2count229 != 0; __81fgg2count229--, _znpjgsef += (__81fgg2step229)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn230 = (System.Int32)((_znpjgsef + (int)1));
						const System.Int32 __81fgg2step230 = (System.Int32)((int)1);
						System.Int32 __81fgg2count230;
						for (__81fgg2count230 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn230 + __81fgg2step230) / __81fgg2step230)), _b5p6od9s = __81fgg2dlsvn230; __81fgg2count230 != 0; __81fgg2count230--, _b5p6od9s += (__81fgg2step230)) {

						{
							
							*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _r7cfteg3;
Mark30:;
							// continue
						}
												}					}
Mark40:;
					// continue
				}
								}			}//* 
			
		}
		else
		{
			//* 
			//*        Set the leading m-by-n submatrix to ALPHA. 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn231 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step231 = (System.Int32)((int)1);
				System.Int32 __81fgg2count231;
				for (__81fgg2count231 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn231 + __81fgg2step231) / __81fgg2step231)), _znpjgsef = __81fgg2dlsvn231; __81fgg2count231 != 0; __81fgg2count231--, _znpjgsef += (__81fgg2step231)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn232 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step232 = (System.Int32)((int)1);
						System.Int32 __81fgg2count232;
						for (__81fgg2count232 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn232 + __81fgg2step232) / __81fgg2step232)), _b5p6od9s = __81fgg2dlsvn232; __81fgg2count232 != 0; __81fgg2count232--, _b5p6od9s += (__81fgg2step232)) {

						{
							
							*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _r7cfteg3;
Mark50:;
							// continue
						}
												}					}
Mark60:;
					// continue
				}
								}			}
		}
		//* 
		//*     Set the first min(M,N) diagonal elements to BETA. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn233 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step233 = (System.Int32)((int)1);
			System.Int32 __81fgg2count233;
			for (__81fgg2count233 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_dxpq0xkr )) - __81fgg2dlsvn233 + __81fgg2step233) / __81fgg2step233)), _b5p6od9s = __81fgg2dlsvn233; __81fgg2count233 != 0; __81fgg2count233--, _b5p6od9s += (__81fgg2step233)) {

			{
				
				*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _bafcbx97;
Mark70:;
				// continue
			}
						}		}//* 
		
		return;//* 
		//*     End of DLASET 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
