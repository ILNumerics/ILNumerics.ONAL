
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
//*> \brief \b CTRTRS 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CTRTRS + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/ctrtrs.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/ctrtrs.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/ctrtrs.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CTRTRS( UPLO, TRANS, DIAG, N, NRHS, A, LDA, B, LDB, 
//*                          INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          DIAG, TRANS, UPLO 
//*       INTEGER            INFO, LDA, LDB, N, NRHS 
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
//*> CTRTRS solves a triangular system of the form 
//*> 
//*>    A * X = B,  A**T * X = B,  or  A**H * X = B, 
//*> 
//*> where A is a triangular matrix of order N, and B is an N-by-NRHS 
//*> matrix.  A check is made to verify that A is nonsingular. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>          = 'U':  A is upper triangular; 
//*>          = 'L':  A is lower triangular. 
//*> \endverbatim 
//*> 
//*> \param[in] TRANS 
//*> \verbatim 
//*>          TRANS is CHARACTER*1 
//*>          Specifies the form of the system of equations: 
//*>          = 'N':  A * X = B     (No transpose) 
//*>          = 'T':  A**T * X = B  (Transpose) 
//*>          = 'C':  A**H * X = B  (Conjugate transpose) 
//*> \endverbatim 
//*> 
//*> \param[in] DIAG 
//*> \verbatim 
//*>          DIAG is CHARACTER*1 
//*>          = 'N':  A is non-unit triangular; 
//*>          = 'U':  A is unit triangular. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix A.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] NRHS 
//*> \verbatim 
//*>          NRHS is INTEGER 
//*>          The number of right hand sides, i.e., the number of columns 
//*>          of the matrix B.  NRHS >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is COMPLEX array, dimension (LDA,N) 
//*>          The triangular matrix A.  If UPLO = 'U', the leading N-by-N 
//*>          upper triangular part of the array A contains the upper 
//*>          triangular matrix, and the strictly lower triangular part of 
//*>          A is not referenced.  If UPLO = 'L', the leading N-by-N lower 
//*>          triangular part of the array A contains the lower triangular 
//*>          matrix, and the strictly upper triangular part of A is not 
//*>          referenced.  If DIAG = 'U', the diagonal elements of A are 
//*>          also not referenced and are assumed to be 1. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[in,out] B 
//*> \verbatim 
//*>          B is COMPLEX array, dimension (LDB,NRHS) 
//*>          On entry, the right hand side matrix B. 
//*>          On exit, if INFO = 0, the solution matrix X. 
//*> \endverbatim 
//*> 
//*> \param[in] LDB 
//*> \verbatim 
//*>          LDB is INTEGER 
//*>          The leading dimension of the array B.  LDB >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit 
//*>          < 0: if INFO = -i, the i-th argument had an illegal value 
//*>          > 0: if INFO = i, the i-th diagonal element of A is zero, 
//*>               indicating that the matrix is singular and the solutions 
//*>               X have not been computed. 
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
//*> \ingroup complexOTHERcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _8jc4dph6(FString _9wyre9zc, FString _scuo79v4, FString _2scffxp3, ref Int32 _dxpq0xkr, ref Int32 _3nayvi7h, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, fcomplex* _p9n405a5, ref Int32 _ly9opahg, ref Int32 _gro5yvfo)
	{
#region variable declarations
fcomplex _d0547bi2 =   new fcomplex(0f,0f);
fcomplex _kxg5drh2 =   new fcomplex(1f,0f);
Boolean _rcjmgxm4 =  default;
string fLanavab = default;
#endregion  variable declarations
_9wyre9zc = _9wyre9zc.Convert(1);
_scuo79v4 = _scuo79v4.Convert(1);
_2scffxp3 = _2scffxp3.Convert(1);

	{
		//* 
		//*  -- LAPACK computational routine (version 3.7.0) -- 
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
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input parameters. 
		//* 
		
		_gro5yvfo = (int)0;
		_rcjmgxm4 = _w8y2rzgy(_2scffxp3 ,"N" );
		if ((!(_w8y2rzgy(_9wyre9zc ,"U" ))) & (!(_w8y2rzgy(_9wyre9zc ,"L" ))))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (((!(_w8y2rzgy(_scuo79v4 ,"N" ))) & (!(_w8y2rzgy(_scuo79v4 ,"T" )))) & (!(_w8y2rzgy(_scuo79v4 ,"C" ))))
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if ((!(_rcjmgxm4)) & (!(_w8y2rzgy(_2scffxp3 ,"U" ))))
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if (_3nayvi7h < (int)0)
		{
			
			_gro5yvfo = (int)-5;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)-7;
		}
		else
		if (_ly9opahg < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)-9;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("CTRTRS" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr == (int)0)
		return;//* 
		//*     Check for singularity. 
		//* 
		
		if (_rcjmgxm4)
		{
			
			{
				System.Int32 __81fgg2dlsvn1854 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1854 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1854;
				for (__81fgg2count1854 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1854 + __81fgg2step1854) / __81fgg2step1854)), _gro5yvfo = __81fgg2dlsvn1854; __81fgg2count1854 != 0; __81fgg2count1854--, _gro5yvfo += (__81fgg2step1854)) {

				{
					
					if (*(_vxfgpup9+(_gro5yvfo - 1) + (_gro5yvfo - 1) * 1 * (_ocv8fk5c)) == _d0547bi2)
					return;
Mark10:;
					// continue
				}
								}			}
		}
		
		_gro5yvfo = (int)0;//* 
		//*     Solve A * x = b,  A**T * x = b,  or  A**H * x = b. 
		//* 
		
		_goj1gzmg("Left" ,_9wyre9zc ,_scuo79v4 ,_2scffxp3 ,ref _dxpq0xkr ,ref _3nayvi7h ,ref Unsafe.AsRef(_kxg5drh2) ,_vxfgpup9 ,ref _ocv8fk5c ,_p9n405a5 ,ref _ly9opahg );//* 
		
		return;//* 
		//*     End of CTRTRS 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
