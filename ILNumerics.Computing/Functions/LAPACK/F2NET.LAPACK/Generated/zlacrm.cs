
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
//*> \brief \b ZLACRM multiplies a complex matrix by a square real matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZLACRM + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zlacrm.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zlacrm.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zlacrm.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZLACRM( M, N, A, LDA, B, LDB, C, LDC, RWORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            LDA, LDB, LDC, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   B( LDB, * ), RWORK( * ) 
//*       COMPLEX*16         A( LDA, * ), C( LDC, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZLACRM performs a very simple matrix-matrix multiplication: 
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
//*>          A is COMPLEX*16 array, dimension (LDA, N) 
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
//*>          B is DOUBLE PRECISION array, dimension (LDB, N) 
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
//*>          C is COMPLEX*16 array, dimension (LDC, N) 
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
//*>          RWORK is DOUBLE PRECISION array, dimension (2*M*N) 
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
//*  ===================================================================== 

	 
	public static void _szowpa5a(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, complex* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _p9n405a5, ref Int32 _ly9opahg, complex* _3crf0qn3, ref Int32 _1s3eymp4, Double* _dqanbbw3)
	{
#region variable declarations
Double _kxg5drh2 =  1d;
Double _d0547bi2 =  0d;
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
			System.Int32 __81fgg2dlsvn1277 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1277 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1277;
			for (__81fgg2count1277 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1277 + __81fgg2step1277) / __81fgg2step1277)), _znpjgsef = __81fgg2dlsvn1277; __81fgg2count1277 != 0; __81fgg2count1277--, _znpjgsef += (__81fgg2step1277)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn1278 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1278 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1278;
					for (__81fgg2count1278 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1278 + __81fgg2step1278) / __81fgg2step1278)), _b5p6od9s = __81fgg2dlsvn1278; __81fgg2count1278 != 0; __81fgg2count1278--, _b5p6od9s += (__81fgg2step1278)) {

					{
						
						*(_dqanbbw3+(((_znpjgsef - (int)1) * _ev4xhht5) + _b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.DBLE(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) );
Mark10:;
						// continue
					}
										}				}
Mark20:;
				// continue
			}
						}		}//* 
		
		_68ec3gbh = ((_ev4xhht5 * _dxpq0xkr) + (int)1);
		_5nsxi69c("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_kxg5drh2) ,_dqanbbw3 ,ref _ev4xhht5 ,_p9n405a5 ,ref _ly9opahg ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+(_68ec3gbh - 1)),ref _ev4xhht5 );
		{
			System.Int32 __81fgg2dlsvn1279 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1279 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1279;
			for (__81fgg2count1279 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1279 + __81fgg2step1279) / __81fgg2step1279)), _znpjgsef = __81fgg2dlsvn1279; __81fgg2count1279 != 0; __81fgg2count1279--, _znpjgsef += (__81fgg2step1279)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn1280 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1280 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1280;
					for (__81fgg2count1280 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1280 + __81fgg2step1280) / __81fgg2step1280)), _b5p6od9s = __81fgg2dlsvn1280; __81fgg2count1280 != 0; __81fgg2count1280--, _b5p6od9s += (__81fgg2step1280)) {

					{
						
						*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = DCMPLX(*(_dqanbbw3+(((_68ec3gbh + ((_znpjgsef - (int)1) * _ev4xhht5)) + _b5p6od9s) - (int)1 - 1)));
Mark30:;
						// continue
					}
										}				}
Mark40:;
				// continue
			}
						}		}//* 
		
		{
			System.Int32 __81fgg2dlsvn1281 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1281 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1281;
			for (__81fgg2count1281 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1281 + __81fgg2step1281) / __81fgg2step1281)), _znpjgsef = __81fgg2dlsvn1281; __81fgg2count1281 != 0; __81fgg2count1281--, _znpjgsef += (__81fgg2step1281)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn1282 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1282 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1282;
					for (__81fgg2count1282 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1282 + __81fgg2step1282) / __81fgg2step1282)), _b5p6od9s = __81fgg2dlsvn1282; __81fgg2count1282 != 0; __81fgg2count1282--, _b5p6od9s += (__81fgg2step1282)) {

					{
						
						*(_dqanbbw3+(((_znpjgsef - (int)1) * _ev4xhht5) + _b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.DIMAG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) );
Mark50:;
						// continue
					}
										}				}
Mark60:;
				// continue
			}
						}		}
		_5nsxi69c("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_kxg5drh2) ,_dqanbbw3 ,ref _ev4xhht5 ,_p9n405a5 ,ref _ly9opahg ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+(_68ec3gbh - 1)),ref _ev4xhht5 );
		{
			System.Int32 __81fgg2dlsvn1283 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1283 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1283;
			for (__81fgg2count1283 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1283 + __81fgg2step1283) / __81fgg2step1283)), _znpjgsef = __81fgg2dlsvn1283; __81fgg2count1283 != 0; __81fgg2count1283--, _znpjgsef += (__81fgg2step1283)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn1284 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1284 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1284;
					for (__81fgg2count1284 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1284 + __81fgg2step1284) / __81fgg2step1284)), _b5p6od9s = __81fgg2dlsvn1284; __81fgg2count1284 != 0; __81fgg2count1284--, _b5p6od9s += (__81fgg2step1284)) {

					{
						
						*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = ILNumerics.F2NET.Intrinsics.DCMPLX(ILNumerics.F2NET.Intrinsics.DBLE(*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) ) ,*(_dqanbbw3+(((_68ec3gbh + ((_znpjgsef - (int)1) * _ev4xhht5)) + _b5p6od9s) - (int)1 - 1)) );
Mark70:;
						// continue
					}
										}				}
Mark80:;
				// continue
			}
						}		}//* 
		
		return;//* 
		//*     End of ZLACRM 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
