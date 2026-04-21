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
//*> \brief \b CGETRS 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CGETRS + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/cgetrs.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/cgetrs.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/cgetrs.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CGETRS( TRANS, N, NRHS, A, LDA, IPIV, B, LDB, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          TRANS 
//*       INTEGER            INFO, LDA, LDB, N, NRHS 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IPIV( * ) 
//*       COMPLEX            A( LDA, * ), B( LDB, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CGETRS solves a system of linear equations 
//*>    A * X = B,  A**T * X = B,  or  A**H * X = B 
//*> with a general N-by-N matrix A using the LU factorization computed 
//*> by CGETRF. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] TRANS 
//*> \verbatim 
//*>          TRANS is CHARACTER*1 
//*>          Specifies the form of the system of equations: 
//*>          = 'N':  A * X = B     (No transpose) 
//*>          = 'T':  A**T * X = B  (Transpose) 
//*>          = 'C':  A**H * X = B  (Conjugate transpose) 
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
//*>          The factors L and U from the factorization A = P*L*U 
//*>          as computed by CGETRF. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[in] IPIV 
//*> \verbatim 
//*>          IPIV is INTEGER array, dimension (N) 
//*>          The pivot indices from CGETRF; for 1<=i<=N, row i of the 
//*>          matrix was interchanged with row IPIV(i). 
//*> \endverbatim 
//*> 
//*> \param[in,out] B 
//*> \verbatim 
//*>          B is COMPLEX array, dimension (LDB,NRHS) 
//*>          On entry, the right hand side matrix B. 
//*>          On exit, the solution matrix X. 
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
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value 
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
//*> \ingroup complexGEcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _us183wko(FString _scuo79v4, ref Int32 _dxpq0xkr, ref Int32 _3nayvi7h, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, Int32* _w1ilvusp, fcomplex* _p9n405a5, ref Int32 _ly9opahg, ref Int32 _gro5yvfo)
	{
#region variable declarations
fcomplex _kxg5drh2 =   new fcomplex(1f,0f);
Boolean _2bzw4gjb =  default;
string fLanavab = default;
#endregion  variable declarations
_scuo79v4 = _scuo79v4.Convert(1);

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
		_2bzw4gjb = _w8y2rzgy(_scuo79v4 ,"N" );
		if (((!(_2bzw4gjb)) & (!(_w8y2rzgy(_scuo79v4 ,"T" )))) & (!(_w8y2rzgy(_scuo79v4 ,"C" ))))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_3nayvi7h < (int)0)
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)-5;
		}
		else
		if (_ly9opahg < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)-8;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("CGETRS" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if ((_dxpq0xkr == (int)0) | (_3nayvi7h == (int)0))
		return;//* 
		
		if (_2bzw4gjb)
		{
			//* 
			//*        Solve A * X = B. 
			//* 
			//*        Apply row interchanges to the right hand sides. 
			//* 
			
			_iuxulk3d(ref _3nayvi7h ,_p9n405a5 ,ref _ly9opahg ,ref Unsafe.AsRef((int)1) ,ref _dxpq0xkr ,_w1ilvusp ,ref Unsafe.AsRef((int)1) );//* 
			//*        Solve L*X = B, overwriting B with X. 
			//* 
			
			_goj1gzmg("Left" ,"Lower" ,"No transpose" ,"Unit" ,ref _dxpq0xkr ,ref _3nayvi7h ,ref Unsafe.AsRef(_kxg5drh2) ,_vxfgpup9 ,ref _ocv8fk5c ,_p9n405a5 ,ref _ly9opahg );//* 
			//*        Solve U*X = B, overwriting B with X. 
			//* 
			
			_goj1gzmg("Left" ,"Upper" ,"No transpose" ,"Non-unit" ,ref _dxpq0xkr ,ref _3nayvi7h ,ref Unsafe.AsRef(_kxg5drh2) ,_vxfgpup9 ,ref _ocv8fk5c ,_p9n405a5 ,ref _ly9opahg );
		}
		else
		{
			//* 
			//*        Solve A**T * X = B  or A**H * X = B. 
			//* 
			//*        Solve U**T *X = B or U**H *X = B, overwriting B with X. 
			//* 
			
			_goj1gzmg("Left" ,"Upper" ,_scuo79v4 ,"Non-unit" ,ref _dxpq0xkr ,ref _3nayvi7h ,ref Unsafe.AsRef(_kxg5drh2) ,_vxfgpup9 ,ref _ocv8fk5c ,_p9n405a5 ,ref _ly9opahg );//* 
			//*        Solve L**T *X = B, or L**H *X = B overwriting B with X. 
			//* 
			
			_goj1gzmg("Left" ,"Lower" ,_scuo79v4 ,"Unit" ,ref _dxpq0xkr ,ref _3nayvi7h ,ref Unsafe.AsRef(_kxg5drh2) ,_vxfgpup9 ,ref _ocv8fk5c ,_p9n405a5 ,ref _ly9opahg );//* 
			//*        Apply row interchanges to the solution vectors. 
			//* 
			
			_iuxulk3d(ref _3nayvi7h ,_p9n405a5 ,ref _ly9opahg ,ref Unsafe.AsRef((int)1) ,ref _dxpq0xkr ,_w1ilvusp ,ref Unsafe.AsRef((int)-1) );
		}
		//* 
		
		return;//* 
		//*     End of CGETRS 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
