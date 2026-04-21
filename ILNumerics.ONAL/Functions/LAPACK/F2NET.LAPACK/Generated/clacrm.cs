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
//*> \brief \b CLACRM multiplies a complex matrix by a square real matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CLACRM + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/clacrm.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/clacrm.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/clacrm.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CLACRM( M, N, A, LDA, B, LDB, C, LDC, RWORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            LDA, LDB, LDC, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               B( LDB, * ), RWORK( * ) 
//*       COMPLEX            A( LDA, * ), C( LDC, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CLACRM performs a very simple matrix-matrix multiplication: 
//*>          C := A * B, 
//*> where A is M by N and complex; B is N by N and real; 
//*> C is M by N and complex. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of rows of the matrix A and of the matrix C. 
//*>          M >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of columns and rows of the matrix B and 
//*>          the number of columns of the matrix C. 
//*>          N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is COMPLEX array, dimension (LDA, N) 
//*>          On entry, A contains the M by N matrix A. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A. LDA >=max(1,M). 
//*> \endverbatim 
//*> 
//*> \param[in] B 
//*> \verbatim 
//*>          B is REAL array, dimension (LDB, N) 
//*>          On entry, B contains the N by N matrix B. 
//*> \endverbatim 
//*> 
//*> \param[in] LDB 
//*> \verbatim 
//*>          LDB is INTEGER 
//*>          The leading dimension of the array B. LDB >=max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[out] C 
//*> \verbatim 
//*>          C is COMPLEX array, dimension (LDC, N) 
//*>          On exit, C contains the M by N matrix C. 
//*> \endverbatim 
//*> 
//*> \param[in] LDC 
//*> \verbatim 
//*>          LDC is INTEGER 
//*>          The leading dimension of the array C. LDC >=max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[out] RWORK 
//*> \verbatim 
//*>          RWORK is REAL array, dimension (2*M*N) 
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

	 
	public static void _cj2s14w4(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, Single* _p9n405a5, ref Int32 _ly9opahg, fcomplex* _3crf0qn3, ref Int32 _1s3eymp4, Single* _dqanbbw3)
	{
#region variable declarations
Single _kxg5drh2 =  1f;
Single _d0547bi2 =  0f;
Int32 _b5p6od9s =  default;
Int32 _znpjgsef =  default;
Int32 _68ec3gbh =  default;
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
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Quick return if possible. 
		//* 
		
		if ((_ev4xhht5 == (int)0) | (_dxpq0xkr == (int)0))
		return;//* 
		
		{
			System.Int32 __81fgg2dlsvn1052 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1052 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1052;
			for (__81fgg2count1052 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1052 + __81fgg2step1052) / __81fgg2step1052)), _znpjgsef = __81fgg2dlsvn1052; __81fgg2count1052 != 0; __81fgg2count1052--, _znpjgsef += (__81fgg2step1052)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn1053 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1053 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1053;
					for (__81fgg2count1053 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1053 + __81fgg2step1053) / __81fgg2step1053)), _b5p6od9s = __81fgg2dlsvn1053; __81fgg2count1053 != 0; __81fgg2count1053--, _b5p6od9s += (__81fgg2step1053)) {

					{
						
						*(_dqanbbw3+(((_znpjgsef - (int)1) * _ev4xhht5) + _b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) );
Mark10:;
						// continue
					}
										}				}
Mark20:;
				// continue
			}
						}		}//* 
		
		_68ec3gbh = ((_ev4xhht5 * _dxpq0xkr) + (int)1);
		_b8wa9454("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_kxg5drh2) ,_dqanbbw3 ,ref _ev4xhht5 ,_p9n405a5 ,ref _ly9opahg ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+(_68ec3gbh - 1)),ref _ev4xhht5 );
		{
			System.Int32 __81fgg2dlsvn1054 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1054 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1054;
			for (__81fgg2count1054 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1054 + __81fgg2step1054) / __81fgg2step1054)), _znpjgsef = __81fgg2dlsvn1054; __81fgg2count1054 != 0; __81fgg2count1054--, _znpjgsef += (__81fgg2step1054)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn1055 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1055 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1055;
					for (__81fgg2count1055 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1055 + __81fgg2step1055) / __81fgg2step1055)), _b5p6od9s = __81fgg2dlsvn1055; __81fgg2count1055 != 0; __81fgg2count1055--, _b5p6od9s += (__81fgg2step1055)) {

					{
						
						*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = CMPLX(*(_dqanbbw3+(((_68ec3gbh + ((_znpjgsef - (int)1) * _ev4xhht5)) + _b5p6od9s) - (int)1 - 1)));
Mark30:;
						// continue
					}
										}				}
Mark40:;
				// continue
			}
						}		}//* 
		
		{
			System.Int32 __81fgg2dlsvn1056 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1056 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1056;
			for (__81fgg2count1056 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1056 + __81fgg2step1056) / __81fgg2step1056)), _znpjgsef = __81fgg2dlsvn1056; __81fgg2count1056 != 0; __81fgg2count1056--, _znpjgsef += (__81fgg2step1056)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn1057 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1057 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1057;
					for (__81fgg2count1057 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1057 + __81fgg2step1057) / __81fgg2step1057)), _b5p6od9s = __81fgg2dlsvn1057; __81fgg2count1057 != 0; __81fgg2count1057--, _b5p6od9s += (__81fgg2step1057)) {

					{
						
						*(_dqanbbw3+(((_znpjgsef - (int)1) * _ev4xhht5) + _b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.AIMAG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) );
Mark50:;
						// continue
					}
										}				}
Mark60:;
				// continue
			}
						}		}
		_b8wa9454("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_kxg5drh2) ,_dqanbbw3 ,ref _ev4xhht5 ,_p9n405a5 ,ref _ly9opahg ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+(_68ec3gbh - 1)),ref _ev4xhht5 );
		{
			System.Int32 __81fgg2dlsvn1058 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1058 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1058;
			for (__81fgg2count1058 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1058 + __81fgg2step1058) / __81fgg2step1058)), _znpjgsef = __81fgg2dlsvn1058; __81fgg2count1058 != 0; __81fgg2count1058--, _znpjgsef += (__81fgg2step1058)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn1059 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1059 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1059;
					for (__81fgg2count1059 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1059 + __81fgg2step1059) / __81fgg2step1059)), _b5p6od9s = __81fgg2dlsvn1059; __81fgg2count1059 != 0; __81fgg2count1059--, _b5p6od9s += (__81fgg2step1059)) {

					{
						
						*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ILNumerics.F2NET.Intrinsics.CMPLX(ILNumerics.F2NET.Intrinsics.REAL(*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) ) ,*(_dqanbbw3+(((_68ec3gbh + ((_znpjgsef - (int)1) * _ev4xhht5)) + _b5p6od9s) - (int)1 - 1)) );
Mark70:;
						// continue
					}
										}				}
Mark80:;
				// continue
			}
						}		}//* 
		
		return;//* 
		//*     End of CLACRM 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
