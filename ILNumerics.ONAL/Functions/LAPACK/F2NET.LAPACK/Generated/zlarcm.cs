
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
//*> \brief \b ZLARCM copies all or part of a real two-dimensional array to a complex array. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZLARCM + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zlarcm.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zlarcm.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zlarcm.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZLARCM( M, N, A, LDA, B, LDB, C, LDC, RWORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            LDA, LDB, LDC, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   A( LDA, * ), RWORK( * ) 
//*       COMPLEX*16         B( LDB, * ), C( LDC, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZLARCM performs a very simple matrix-matrix multiplication: 
//*>          C := A * B, 
//*> where A is M by M and real; B is M by N and complex; 
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
//*>          A is DOUBLE PRECISION array, dimension (LDA, M) 
//*>          On entry, A contains the M by M matrix A. 
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
//*>          B is COMPLEX*16 array, dimension (LDB, N) 
//*>          On entry, B contains the M by N matrix B. 
//*> \endverbatim 
//*> 
//*> \param[in] LDB 
//*> \verbatim 
//*>          LDB is INTEGER 
//*>          The leading dimension of the array B. LDB >=max(1,M). 
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
//*>          The leading dimension of the array C. LDC >=max(1,M). 
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
//*> \date June 2016 
//* 
//*> \ingroup complex16OTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _zwe74bak(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, Double* _vxfgpup9, ref Int32 _ocv8fk5c, complex* _p9n405a5, ref Int32 _ly9opahg, complex* _3crf0qn3, ref Int32 _1s3eymp4, Double* _dqanbbw3)
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
		//*     June 2016 
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
			System.Int32 __81fgg2dlsvn1285 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1285 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1285;
			for (__81fgg2count1285 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1285 + __81fgg2step1285) / __81fgg2step1285)), _znpjgsef = __81fgg2dlsvn1285; __81fgg2count1285 != 0; __81fgg2count1285--, _znpjgsef += (__81fgg2step1285)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn1286 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1286 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1286;
					for (__81fgg2count1286 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1286 + __81fgg2step1286) / __81fgg2step1286)), _b5p6od9s = __81fgg2dlsvn1286; __81fgg2count1286 != 0; __81fgg2count1286--, _b5p6od9s += (__81fgg2step1286)) {

					{
						
						*(_dqanbbw3+(((_znpjgsef - (int)1) * _ev4xhht5) + _b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.DBLE(*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) );
Mark10:;
						// continue
					}
										}				}
Mark20:;
				// continue
			}
						}		}//* 
		
		_68ec3gbh = ((_ev4xhht5 * _dxpq0xkr) + (int)1);
		_5nsxi69c("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,ref Unsafe.AsRef(_kxg5drh2) ,_vxfgpup9 ,ref _ocv8fk5c ,_dqanbbw3 ,ref _ev4xhht5 ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+(_68ec3gbh - 1)),ref _ev4xhht5 );
		{
			System.Int32 __81fgg2dlsvn1287 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1287 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1287;
			for (__81fgg2count1287 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1287 + __81fgg2step1287) / __81fgg2step1287)), _znpjgsef = __81fgg2dlsvn1287; __81fgg2count1287 != 0; __81fgg2count1287--, _znpjgsef += (__81fgg2step1287)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn1288 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1288 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1288;
					for (__81fgg2count1288 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1288 + __81fgg2step1288) / __81fgg2step1288)), _b5p6od9s = __81fgg2dlsvn1288; __81fgg2count1288 != 0; __81fgg2count1288--, _b5p6od9s += (__81fgg2step1288)) {

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
			System.Int32 __81fgg2dlsvn1289 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1289 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1289;
			for (__81fgg2count1289 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1289 + __81fgg2step1289) / __81fgg2step1289)), _znpjgsef = __81fgg2dlsvn1289; __81fgg2count1289 != 0; __81fgg2count1289--, _znpjgsef += (__81fgg2step1289)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn1290 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1290 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1290;
					for (__81fgg2count1290 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1290 + __81fgg2step1290) / __81fgg2step1290)), _b5p6od9s = __81fgg2dlsvn1290; __81fgg2count1290 != 0; __81fgg2count1290--, _b5p6od9s += (__81fgg2step1290)) {

					{
						
						*(_dqanbbw3+(((_znpjgsef - (int)1) * _ev4xhht5) + _b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.DIMAG(*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) );
Mark50:;
						// continue
					}
										}				}
Mark60:;
				// continue
			}
						}		}
		_5nsxi69c("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,ref Unsafe.AsRef(_kxg5drh2) ,_vxfgpup9 ,ref _ocv8fk5c ,_dqanbbw3 ,ref _ev4xhht5 ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+(_68ec3gbh - 1)),ref _ev4xhht5 );
		{
			System.Int32 __81fgg2dlsvn1291 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1291 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1291;
			for (__81fgg2count1291 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1291 + __81fgg2step1291) / __81fgg2step1291)), _znpjgsef = __81fgg2dlsvn1291; __81fgg2count1291 != 0; __81fgg2count1291--, _znpjgsef += (__81fgg2step1291)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn1292 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1292 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1292;
					for (__81fgg2count1292 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1292 + __81fgg2step1292) / __81fgg2step1292)), _b5p6od9s = __81fgg2dlsvn1292; __81fgg2count1292 != 0; __81fgg2count1292--, _b5p6od9s += (__81fgg2step1292)) {

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
		//*     End of ZLARCM 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
