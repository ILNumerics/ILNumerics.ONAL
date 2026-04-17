
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
//*> \brief \b ZLASET initializes the off-diagonal elements and the diagonal elements of a matrix to given values. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZLASET + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zlaset.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zlaset.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zlaset.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZLASET( UPLO, M, N, ALPHA, BETA, A, LDA ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          UPLO 
//*       INTEGER            LDA, M, N 
//*       COMPLEX*16         ALPHA, BETA 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX*16         A( LDA, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZLASET initializes a 2-D array A to BETA on the diagonal and 
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
//*>          = 'U':      Upper triangular part is set. The lower triangle 
//*>                      is unchanged. 
//*>          = 'L':      Lower triangular part is set. The upper triangle 
//*>                      is unchanged. 
//*>          Otherwise:  All of the matrix A is set. 
//*> \endverbatim 
//*> 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          On entry, M specifies the number of rows of A. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          On entry, N specifies the number of columns of A. 
//*> \endverbatim 
//*> 
//*> \param[in] ALPHA 
//*> \verbatim 
//*>          ALPHA is COMPLEX*16 
//*>          All the offdiagonal array elements are set to ALPHA. 
//*> \endverbatim 
//*> 
//*> \param[in] BETA 
//*> \verbatim 
//*>          BETA is COMPLEX*16 
//*>          All the diagonal array elements are set to BETA. 
//*> \endverbatim 
//*> 
//*> \param[out] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension (LDA,N) 
//*>          On entry, the m by n matrix A. 
//*>          On exit, A(i,j) = ALPHA, 1 <= i <= m, 1 <= j <= n, i.ne.j; 
//*>                   A(i,i) = BETA , 1 <= i <= min(m,n) 
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
//*> \ingroup complex16OTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _k14i9nd8(FString _9wyre9zc, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref complex _r7cfteg3, ref complex _bafcbx97, complex* _vxfgpup9, ref Int32 _ocv8fk5c)
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
			//* 
			//*        Set the diagonal to BETA and the strictly upper triangular 
			//*        part of the array to ALPHA. 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn1307 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step1307 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1307;
				for (__81fgg2count1307 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1307 + __81fgg2step1307) / __81fgg2step1307)), _znpjgsef = __81fgg2dlsvn1307; __81fgg2count1307 != 0; __81fgg2count1307--, _znpjgsef += (__81fgg2step1307)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn1308 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1308 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1308;
						for (__81fgg2count1308 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN(_znpjgsef - (int)1 ,_ev4xhht5 )) - __81fgg2dlsvn1308 + __81fgg2step1308) / __81fgg2step1308)), _b5p6od9s = __81fgg2dlsvn1308; __81fgg2count1308 != 0; __81fgg2count1308--, _b5p6od9s += (__81fgg2step1308)) {

						{
							
							*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _r7cfteg3;
Mark10:;
							// continue
						}
												}					}
Mark20:;
					// continue
				}
								}			}
			{
				System.Int32 __81fgg2dlsvn1309 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1309 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1309;
				for (__81fgg2count1309 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN(_dxpq0xkr ,_ev4xhht5 )) - __81fgg2dlsvn1309 + __81fgg2step1309) / __81fgg2step1309)), _b5p6od9s = __81fgg2dlsvn1309; __81fgg2count1309 != 0; __81fgg2count1309--, _b5p6od9s += (__81fgg2step1309)) {

				{
					
					*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _bafcbx97;
Mark30:;
					// continue
				}
								}			}//* 
			
		}
		else
		if (_w8y2rzgy(_9wyre9zc ,"L" ))
		{
			//* 
			//*        Set the diagonal to BETA and the strictly lower triangular 
			//*        part of the array to ALPHA. 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn1310 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1310 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1310;
				for (__81fgg2count1310 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_dxpq0xkr )) - __81fgg2dlsvn1310 + __81fgg2step1310) / __81fgg2step1310)), _znpjgsef = __81fgg2dlsvn1310; __81fgg2count1310 != 0; __81fgg2count1310--, _znpjgsef += (__81fgg2step1310)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn1311 = (System.Int32)((_znpjgsef + (int)1));
						const System.Int32 __81fgg2step1311 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1311;
						for (__81fgg2count1311 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1311 + __81fgg2step1311) / __81fgg2step1311)), _b5p6od9s = __81fgg2dlsvn1311; __81fgg2count1311 != 0; __81fgg2count1311--, _b5p6od9s += (__81fgg2step1311)) {

						{
							
							*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _r7cfteg3;
Mark40:;
							// continue
						}
												}					}
Mark50:;
					// continue
				}
								}			}
			{
				System.Int32 __81fgg2dlsvn1312 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1312 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1312;
				for (__81fgg2count1312 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN(_dxpq0xkr ,_ev4xhht5 )) - __81fgg2dlsvn1312 + __81fgg2step1312) / __81fgg2step1312)), _b5p6od9s = __81fgg2dlsvn1312; __81fgg2count1312 != 0; __81fgg2count1312--, _b5p6od9s += (__81fgg2step1312)) {

				{
					
					*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _bafcbx97;
Mark60:;
					// continue
				}
								}			}//* 
			
		}
		else
		{
			//* 
			//*        Set the array to BETA on the diagonal and ALPHA on the 
			//*        offdiagonal. 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn1313 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1313 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1313;
				for (__81fgg2count1313 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1313 + __81fgg2step1313) / __81fgg2step1313)), _znpjgsef = __81fgg2dlsvn1313; __81fgg2count1313 != 0; __81fgg2count1313--, _znpjgsef += (__81fgg2step1313)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn1314 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1314 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1314;
						for (__81fgg2count1314 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1314 + __81fgg2step1314) / __81fgg2step1314)), _b5p6od9s = __81fgg2dlsvn1314; __81fgg2count1314 != 0; __81fgg2count1314--, _b5p6od9s += (__81fgg2step1314)) {

						{
							
							*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _r7cfteg3;
Mark70:;
							// continue
						}
												}					}
Mark80:;
					// continue
				}
								}			}
			{
				System.Int32 __81fgg2dlsvn1315 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1315 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1315;
				for (__81fgg2count1315 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_dxpq0xkr )) - __81fgg2dlsvn1315 + __81fgg2step1315) / __81fgg2step1315)), _b5p6od9s = __81fgg2dlsvn1315; __81fgg2count1315 != 0; __81fgg2count1315--, _b5p6od9s += (__81fgg2step1315)) {

				{
					
					*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _bafcbx97;
Mark90:;
					// continue
				}
								}			}
		}
		//* 
		
		return;//* 
		//*     End of ZLASET 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
