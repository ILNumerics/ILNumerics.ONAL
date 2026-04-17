
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
//*> \brief \b CLARCM copies all or part of a real two-dimensional array to a complex array. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CLARCM + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/clarcm.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/clarcm.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/clarcm.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CLARCM( M, N, A, LDA, B, LDB, C, LDC, RWORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            LDA, LDB, LDC, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               A( LDA, * ), RWORK( * ) 
//*       COMPLEX            B( LDB, * ), C( LDC, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CLARCM performs a very simple matrix-matrix multiplication: 
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
//*>          A is REAL array, dimension (LDA, M) 
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
//*>          B is COMPLEX array, dimension (LDB, N) 
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
//*>          C is COMPLEX array, dimension (LDC, N) 
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
//*> \date June 2016 
//* 
//*> \ingroup complexOTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _myub3lkw(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, Single* _vxfgpup9, ref Int32 _ocv8fk5c, fcomplex* _p9n405a5, ref Int32 _ly9opahg, fcomplex* _3crf0qn3, ref Int32 _1s3eymp4, Single* _dqanbbw3)
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
			System.Int32 __81fgg2dlsvn1060 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1060 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1060;
			for (__81fgg2count1060 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1060 + __81fgg2step1060) / __81fgg2step1060)), _znpjgsef = __81fgg2dlsvn1060; __81fgg2count1060 != 0; __81fgg2count1060--, _znpjgsef += (__81fgg2step1060)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn1061 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1061 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1061;
					for (__81fgg2count1061 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1061 + __81fgg2step1061) / __81fgg2step1061)), _b5p6od9s = __81fgg2dlsvn1061; __81fgg2count1061 != 0; __81fgg2count1061--, _b5p6od9s += (__81fgg2step1061)) {

					{
						
						*(_dqanbbw3+(((_znpjgsef - (int)1) * _ev4xhht5) + _b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.REAL(*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) );
Mark10:;
						// continue
					}
										}				}
Mark20:;
				// continue
			}
						}		}//* 
		
		_68ec3gbh = ((_ev4xhht5 * _dxpq0xkr) + (int)1);
		_b8wa9454("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,ref Unsafe.AsRef(_kxg5drh2) ,_vxfgpup9 ,ref _ocv8fk5c ,_dqanbbw3 ,ref _ev4xhht5 ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+(_68ec3gbh - 1)),ref _ev4xhht5 );
		{
			System.Int32 __81fgg2dlsvn1062 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1062 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1062;
			for (__81fgg2count1062 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1062 + __81fgg2step1062) / __81fgg2step1062)), _znpjgsef = __81fgg2dlsvn1062; __81fgg2count1062 != 0; __81fgg2count1062--, _znpjgsef += (__81fgg2step1062)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn1063 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1063 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1063;
					for (__81fgg2count1063 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1063 + __81fgg2step1063) / __81fgg2step1063)), _b5p6od9s = __81fgg2dlsvn1063; __81fgg2count1063 != 0; __81fgg2count1063--, _b5p6od9s += (__81fgg2step1063)) {

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
			System.Int32 __81fgg2dlsvn1064 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1064 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1064;
			for (__81fgg2count1064 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1064 + __81fgg2step1064) / __81fgg2step1064)), _znpjgsef = __81fgg2dlsvn1064; __81fgg2count1064 != 0; __81fgg2count1064--, _znpjgsef += (__81fgg2step1064)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn1065 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1065 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1065;
					for (__81fgg2count1065 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1065 + __81fgg2step1065) / __81fgg2step1065)), _b5p6od9s = __81fgg2dlsvn1065; __81fgg2count1065 != 0; __81fgg2count1065--, _b5p6od9s += (__81fgg2step1065)) {

					{
						
						*(_dqanbbw3+(((_znpjgsef - (int)1) * _ev4xhht5) + _b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.AIMAG(*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) );
Mark50:;
						// continue
					}
										}				}
Mark60:;
				// continue
			}
						}		}
		_b8wa9454("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,ref Unsafe.AsRef(_kxg5drh2) ,_vxfgpup9 ,ref _ocv8fk5c ,_dqanbbw3 ,ref _ev4xhht5 ,ref Unsafe.AsRef(_d0547bi2) ,(_dqanbbw3+(_68ec3gbh - 1)),ref _ev4xhht5 );
		{
			System.Int32 __81fgg2dlsvn1066 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1066 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1066;
			for (__81fgg2count1066 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1066 + __81fgg2step1066) / __81fgg2step1066)), _znpjgsef = __81fgg2dlsvn1066; __81fgg2count1066 != 0; __81fgg2count1066--, _znpjgsef += (__81fgg2step1066)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn1067 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1067 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1067;
					for (__81fgg2count1067 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1067 + __81fgg2step1067) / __81fgg2step1067)), _b5p6od9s = __81fgg2dlsvn1067; __81fgg2count1067 != 0; __81fgg2count1067--, _b5p6od9s += (__81fgg2step1067)) {

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
		//*     End of CLARCM 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
