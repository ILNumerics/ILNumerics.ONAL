
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
//*> \brief \b SLASET initializes the off-diagonal elements and the diagonal elements of a matrix to given values. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLASET + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slaset.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slaset.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slaset.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLASET( UPLO, M, N, ALPHA, BETA, A, LDA ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          UPLO 
//*       INTEGER            LDA, M, N 
//*       REAL               ALPHA, BETA 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               A( LDA, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLASET initializes an m-by-n matrix A to BETA on the diagonal and 
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
//*>          ALPHA is REAL 
//*>          The constant to which the offdiagonal elements are to be set. 
//*> \endverbatim 
//*> 
//*> \param[in] BETA 
//*> \verbatim 
//*>          BETA is REAL 
//*>          The constant to which the diagonal elements are to be set. 
//*> \endverbatim 
//*> 
//*> \param[out] A 
//*> \verbatim 
//*>          A is REAL array, dimension (LDA,N) 
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

	 
	public static void _t013e1c8(FString _9wyre9zc, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Single _r7cfteg3, ref Single _bafcbx97, Single* _vxfgpup9, ref Int32 _ocv8fk5c)
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
				System.Int32 __81fgg2dlsvn594 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step594 = (System.Int32)((int)1);
				System.Int32 __81fgg2count594;
				for (__81fgg2count594 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn594 + __81fgg2step594) / __81fgg2step594)), _znpjgsef = __81fgg2dlsvn594; __81fgg2count594 != 0; __81fgg2count594--, _znpjgsef += (__81fgg2step594)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn595 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step595 = (System.Int32)((int)1);
						System.Int32 __81fgg2count595;
						for (__81fgg2count595 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN(_znpjgsef - (int)1 ,_ev4xhht5 )) - __81fgg2dlsvn595 + __81fgg2step595) / __81fgg2step595)), _b5p6od9s = __81fgg2dlsvn595; __81fgg2count595 != 0; __81fgg2count595--, _b5p6od9s += (__81fgg2step595)) {

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
				System.Int32 __81fgg2dlsvn596 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step596 = (System.Int32)((int)1);
				System.Int32 __81fgg2count596;
				for (__81fgg2count596 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_dxpq0xkr )) - __81fgg2dlsvn596 + __81fgg2step596) / __81fgg2step596)), _znpjgsef = __81fgg2dlsvn596; __81fgg2count596 != 0; __81fgg2count596--, _znpjgsef += (__81fgg2step596)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn597 = (System.Int32)((_znpjgsef + (int)1));
						const System.Int32 __81fgg2step597 = (System.Int32)((int)1);
						System.Int32 __81fgg2count597;
						for (__81fgg2count597 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn597 + __81fgg2step597) / __81fgg2step597)), _b5p6od9s = __81fgg2dlsvn597; __81fgg2count597 != 0; __81fgg2count597--, _b5p6od9s += (__81fgg2step597)) {

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
				System.Int32 __81fgg2dlsvn598 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step598 = (System.Int32)((int)1);
				System.Int32 __81fgg2count598;
				for (__81fgg2count598 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn598 + __81fgg2step598) / __81fgg2step598)), _znpjgsef = __81fgg2dlsvn598; __81fgg2count598 != 0; __81fgg2count598--, _znpjgsef += (__81fgg2step598)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn599 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step599 = (System.Int32)((int)1);
						System.Int32 __81fgg2count599;
						for (__81fgg2count599 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn599 + __81fgg2step599) / __81fgg2step599)), _b5p6od9s = __81fgg2dlsvn599; __81fgg2count599 != 0; __81fgg2count599--, _b5p6od9s += (__81fgg2step599)) {

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
			System.Int32 __81fgg2dlsvn600 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step600 = (System.Int32)((int)1);
			System.Int32 __81fgg2count600;
			for (__81fgg2count600 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_dxpq0xkr )) - __81fgg2dlsvn600 + __81fgg2step600) / __81fgg2step600)), _b5p6od9s = __81fgg2dlsvn600; __81fgg2count600 != 0; __81fgg2count600--, _b5p6od9s += (__81fgg2step600)) {

			{
				
				*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _bafcbx97;
Mark70:;
				// continue
			}
						}		}//* 
		
		return;//* 
		//*     End of SLASET 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
