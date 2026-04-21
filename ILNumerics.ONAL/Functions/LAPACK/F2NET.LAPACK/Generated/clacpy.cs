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
//*> \brief \b CLACPY copies all or part of one two-dimensional array to another. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CLACPY + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/clacpy.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/clacpy.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/clacpy.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CLACPY( UPLO, M, N, A, LDA, B, LDB ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          UPLO 
//*       INTEGER            LDA, LDB, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX            A( LDA, * ), B( LDB, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CLACPY copies all or part of a two-dimensional matrix A to another 
//*> matrix B. 
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
//*>          A is COMPLEX array, dimension (LDA,N) 
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

	 
	public static void _szaic8qw(FString _9wyre9zc, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, fcomplex* _p9n405a5, ref Int32 _ly9opahg)
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
				System.Int32 __81fgg2dlsvn1046 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1046 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1046;
				for (__81fgg2count1046 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1046 + __81fgg2step1046) / __81fgg2step1046)), _znpjgsef = __81fgg2dlsvn1046; __81fgg2count1046 != 0; __81fgg2count1046--, _znpjgsef += (__81fgg2step1046)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn1047 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1047 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1047;
						for (__81fgg2count1047 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN(_znpjgsef ,_ev4xhht5 )) - __81fgg2dlsvn1047 + __81fgg2step1047) / __81fgg2step1047)), _b5p6od9s = __81fgg2dlsvn1047; __81fgg2count1047 != 0; __81fgg2count1047--, _b5p6od9s += (__81fgg2step1047)) {

						{
							
							*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c));
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
				System.Int32 __81fgg2dlsvn1048 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1048 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1048;
				for (__81fgg2count1048 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1048 + __81fgg2step1048) / __81fgg2step1048)), _znpjgsef = __81fgg2dlsvn1048; __81fgg2count1048 != 0; __81fgg2count1048--, _znpjgsef += (__81fgg2step1048)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn1049 = (System.Int32)(_znpjgsef);
						const System.Int32 __81fgg2step1049 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1049;
						for (__81fgg2count1049 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1049 + __81fgg2step1049) / __81fgg2step1049)), _b5p6od9s = __81fgg2dlsvn1049; __81fgg2count1049 != 0; __81fgg2count1049--, _b5p6od9s += (__81fgg2step1049)) {

						{
							
							*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c));
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
				System.Int32 __81fgg2dlsvn1050 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1050 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1050;
				for (__81fgg2count1050 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1050 + __81fgg2step1050) / __81fgg2step1050)), _znpjgsef = __81fgg2dlsvn1050; __81fgg2count1050 != 0; __81fgg2count1050--, _znpjgsef += (__81fgg2step1050)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn1051 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1051 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1051;
						for (__81fgg2count1051 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1051 + __81fgg2step1051) / __81fgg2step1051)), _b5p6od9s = __81fgg2dlsvn1051; __81fgg2count1051 != 0; __81fgg2count1051--, _b5p6od9s += (__81fgg2step1051)) {

						{
							
							*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c));
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
		//*     End of CLACPY 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
