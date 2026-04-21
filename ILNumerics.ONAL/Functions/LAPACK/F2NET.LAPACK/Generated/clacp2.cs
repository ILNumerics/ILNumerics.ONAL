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
//*> \brief \b CLACP2 copies all or part of a real two-dimensional array to a complex array. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CLACP2 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/clacp2.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/clacp2.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/clacp2.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CLACP2( UPLO, M, N, A, LDA, B, LDB ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          UPLO 
//*       INTEGER            LDA, LDB, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               A( LDA, * ) 
//*       COMPLEX            B( LDB, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CLACP2 copies all or part of a real two-dimensional matrix A to a 
//*> complex matrix B. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>          Specifies the part of the matrix A to be copied to B. 
//*>          = 'U':      Upper triangular part 
//*>          = 'L':      Lower triangular part 
//*>          Otherwise:  All of the matrix A 
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
//*> \param[in] A 
//*> \verbatim 
//*>          A is REAL array, dimension (LDA,N) 
//*>          The m by n matrix A.  If UPLO = 'U', only the upper trapezium 
//*>          is accessed; if UPLO = 'L', only the lower trapezium is 
//*>          accessed. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,M). 
//*> \endverbatim 
//*> 
//*> \param[out] B 
//*> \verbatim 
//*>          B is COMPLEX array, dimension (LDB,N) 
//*>          On exit, B = A in the locations specified by UPLO. 
//*> \endverbatim 
//*> 
//*> \param[in] LDB 
//*> \verbatim 
//*>          LDB is INTEGER 
//*>          The leading dimension of the array B.  LDB >= max(1,M). 
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
//*  ===================================================================== 

	 
	public static void _d6fojrrt(FString _9wyre9zc, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, Single* _vxfgpup9, ref Int32 _ocv8fk5c, fcomplex* _p9n405a5, ref Int32 _ly9opahg)
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
		//*  ===================================================================== 
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
			
			{
				System.Int32 __81fgg2dlsvn1040 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1040 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1040;
				for (__81fgg2count1040 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1040 + __81fgg2step1040) / __81fgg2step1040)), _znpjgsef = __81fgg2dlsvn1040; __81fgg2count1040 != 0; __81fgg2count1040--, _znpjgsef += (__81fgg2step1040)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn1041 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1041 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1041;
						for (__81fgg2count1041 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN(_znpjgsef ,_ev4xhht5 )) - __81fgg2dlsvn1041 + __81fgg2step1041) / __81fgg2step1041)), _b5p6od9s = __81fgg2dlsvn1041; __81fgg2count1041 != 0; __81fgg2count1041--, _b5p6od9s += (__81fgg2step1041)) {

						{
							
							*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = CMPLX(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
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
			
			{
				System.Int32 __81fgg2dlsvn1042 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1042 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1042;
				for (__81fgg2count1042 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1042 + __81fgg2step1042) / __81fgg2step1042)), _znpjgsef = __81fgg2dlsvn1042; __81fgg2count1042 != 0; __81fgg2count1042--, _znpjgsef += (__81fgg2step1042)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn1043 = (System.Int32)(_znpjgsef);
						const System.Int32 __81fgg2step1043 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1043;
						for (__81fgg2count1043 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1043 + __81fgg2step1043) / __81fgg2step1043)), _b5p6od9s = __81fgg2dlsvn1043; __81fgg2count1043 != 0; __81fgg2count1043--, _b5p6od9s += (__81fgg2step1043)) {

						{
							
							*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = CMPLX(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
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
			
			{
				System.Int32 __81fgg2dlsvn1044 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1044 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1044;
				for (__81fgg2count1044 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1044 + __81fgg2step1044) / __81fgg2step1044)), _znpjgsef = __81fgg2dlsvn1044; __81fgg2count1044 != 0; __81fgg2count1044--, _znpjgsef += (__81fgg2step1044)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn1045 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1045 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1045;
						for (__81fgg2count1045 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1045 + __81fgg2step1045) / __81fgg2step1045)), _b5p6od9s = __81fgg2dlsvn1045; __81fgg2count1045 != 0; __81fgg2count1045--, _b5p6od9s += (__81fgg2step1045)) {

						{
							
							*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = CMPLX(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
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
		
		return;//* 
		//*     End of CLACP2 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
