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
//*> \brief \b SSYGV 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SSYGV + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/ssygv.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/ssygv.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/ssygv.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SSYGV( ITYPE, JOBZ, UPLO, N, A, LDA, B, LDB, W, WORK, 
//*                         LWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          JOBZ, UPLO 
//*       INTEGER            INFO, ITYPE, LDA, LDB, LWORK, N 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               A( LDA, * ), B( LDB, * ), W( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SSYGV computes all the eigenvalues, and optionally, the eigenvectors 
//*> of a real generalized symmetric-definite eigenproblem, of the form 
//*> A*x=(lambda)*B*x,  A*Bx=(lambda)*x,  or B*A*x=(lambda)*x. 
//*> Here A and B are assumed to be symmetric and B is also 
//*> positive definite. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] ITYPE 
//*> \verbatim 
//*>          ITYPE is INTEGER 
//*>          Specifies the problem type to be solved: 
//*>          = 1:  A*x = (lambda)*B*x 
//*>          = 2:  A*B*x = (lambda)*x 
//*>          = 3:  B*A*x = (lambda)*x 
//*> \endverbatim 
//*> 
//*> \param[in] JOBZ 
//*> \verbatim 
//*>          JOBZ is CHARACTER*1 
//*>          = 'N':  Compute eigenvalues only; 
//*>          = 'V':  Compute eigenvalues and eigenvectors. 
//*> \endverbatim 
//*> 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>          = 'U':  Upper triangles of A and B are stored; 
//*>          = 'L':  Lower triangles of A and B are stored. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrices A and B.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is REAL array, dimension (LDA, N) 
//*>          On entry, the symmetric matrix A.  If UPLO = 'U', the 
//*>          leading N-by-N upper triangular part of A contains the 
//*>          upper triangular part of the matrix A.  If UPLO = 'L', 
//*>          the leading N-by-N lower triangular part of A contains 
//*>          the lower triangular part of the matrix A. 
//*> 
//*>          On exit, if JOBZ = 'V', then if INFO = 0, A contains the 
//*>          matrix Z of eigenvectors.  The eigenvectors are normalized 
//*>          as follows: 
//*>          if ITYPE = 1 or 2, Z**T*B*Z = I; 
//*>          if ITYPE = 3, Z**T*inv(B)*Z = I. 
//*>          If JOBZ = 'N', then on exit the upper triangle (if UPLO='U') 
//*>          or the lower triangle (if UPLO='L') of A, including the 
//*>          diagonal, is destroyed. 
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
//*>          B is REAL array, dimension (LDB, N) 
//*>          On entry, the symmetric positive definite matrix B. 
//*>          If UPLO = 'U', the leading N-by-N upper triangular part of B 
//*>          contains the upper triangular part of the matrix B. 
//*>          If UPLO = 'L', the leading N-by-N lower triangular part of B 
//*>          contains the lower triangular part of the matrix B. 
//*> 
//*>          On exit, if INFO <= N, the part of B containing the matrix is 
//*>          overwritten by the triangular factor U or L from the Cholesky 
//*>          factorization B = U**T*U or B = L*L**T. 
//*> \endverbatim 
//*> 
//*> \param[in] LDB 
//*> \verbatim 
//*>          LDB is INTEGER 
//*>          The leading dimension of the array B.  LDB >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[out] W 
//*> \verbatim 
//*>          W is REAL array, dimension (N) 
//*>          If INFO = 0, the eigenvalues in ascending order. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is REAL array, dimension (MAX(1,LWORK)) 
//*>          On exit, if INFO = 0, WORK(1) returns the optimal LWORK. 
//*> \endverbatim 
//*> 
//*> \param[in] LWORK 
//*> \verbatim 
//*>          LWORK is INTEGER 
//*>          The length of the array WORK.  LWORK >= max(1,3*N-1). 
//*>          For optimal efficiency, LWORK >= (NB+2)*N, 
//*>          where NB is the blocksize for SSYTRD returned by ILAENV. 
//*> 
//*>          If LWORK = -1, then a workspace query is assumed; the routine 
//*>          only calculates the optimal size of the WORK array, returns 
//*>          this value as the first entry of the WORK array, and no error 
//*>          message related to LWORK is issued by XERBLA. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value 
//*>          > 0:  SPOTRF or SSYEV returned an error code: 
//*>             <= N:  if INFO = i, SSYEV failed to converge; 
//*>                    i off-diagonal elements of an intermediate 
//*>                    tridiagonal form did not converge to zero; 
//*>             > N:   if INFO = N + i, for 1 <= i <= N, then the leading 
//*>                    minor of order i of B is not positive definite. 
//*>                    The factorization of B could not be completed and 
//*>                    no eigenvalues or eigenvectors were computed. 
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
//*> \ingroup realSYeigen 
//* 
//*  ===================================================================== 

	 
	public static void _c3m9iro8(ref Int32 _842z9590, FString _w6igfk2h, FString _9wyre9zc, ref Int32 _dxpq0xkr, Single* _vxfgpup9, ref Int32 _ocv8fk5c, Single* _p9n405a5, ref Int32 _ly9opahg, Single* _z1ioc3c8, Single* _apig8meb, ref Int32 _6fnxzlyp, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _kxg5drh2 =  1f;
Boolean _lhlgm7z5 =  default;
Boolean _l08igmvf =  default;
Boolean _189gzykk =  default;
FString _scuo79v4 =  new FString(1);
Int32 _yq36eu37 =  default;
Int32 _e4ueamrn =  default;
Int32 _f7059815 =  default;
Int32 _zptdq03m =  default;
string fLanavab = default;
#endregion  variable declarations
_w6igfk2h = _w6igfk2h.Convert(1);
_9wyre9zc = _9wyre9zc.Convert(1);

	{
		//* 
		//*  -- LAPACK driver routine (version 3.7.0) -- 
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
		
		_189gzykk = _w8y2rzgy(_w6igfk2h ,"V" );
		_l08igmvf = _w8y2rzgy(_9wyre9zc ,"U" );
		_lhlgm7z5 = (_6fnxzlyp == (int)-1);//* 
		
		_gro5yvfo = (int)0;
		if ((_842z9590 < (int)1) | (_842z9590 > (int)3))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (!((_189gzykk | _w8y2rzgy(_w6igfk2h ,"N" ))))
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (!((_l08igmvf | _w8y2rzgy(_9wyre9zc ,"L" ))))
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)-6;
		}
		else
		if (_ly9opahg < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)-8;
		}
		//* 
		
		if (_gro5yvfo == (int)0)
		{
			
			_yq36eu37 = ILNumerics.F2NET.Intrinsics.MAX((int)1 ,((int)3 * _dxpq0xkr) - (int)1 );
			_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"SSYTRD" ,_9wyre9zc ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) );
			_e4ueamrn = ILNumerics.F2NET.Intrinsics.MAX(_yq36eu37 ,(_f7059815 + (int)2) * _dxpq0xkr );
			*(_apig8meb+((int)1 - 1)) = REAL(_e4ueamrn);//* 
			
			if ((_6fnxzlyp < _yq36eu37) & (!(_lhlgm7z5)))
			{
				
				_gro5yvfo = (int)-11;
			}
			
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("SSYGV " ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		else
		if (_lhlgm7z5)
		{
			
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr == (int)0)
		return;//* 
		//*     Form a Cholesky factorization of B. 
		//* 
		
		_d0eywe7s(_9wyre9zc ,ref _dxpq0xkr ,_p9n405a5 ,ref _ly9opahg ,ref _gro5yvfo );
		if (_gro5yvfo != (int)0)
		{
			
			_gro5yvfo = (_dxpq0xkr + _gro5yvfo);
			return;
		}
		//* 
		//*     Transform problem to standard eigenvalue problem and solve. 
		//* 
		
		_kwjz3u26(ref _842z9590 ,_9wyre9zc ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_p9n405a5 ,ref _ly9opahg ,ref _gro5yvfo );
		_1uqlwjkb(_w6igfk2h ,_9wyre9zc ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_z1ioc3c8 ,_apig8meb ,ref _6fnxzlyp ,ref _gro5yvfo );//* 
		
		if (_189gzykk)
		{
			//* 
			//*        Backtransform eigenvectors to the original problem. 
			//* 
			
			_zptdq03m = _dxpq0xkr;
			if (_gro5yvfo > (int)0)
			_zptdq03m = (_gro5yvfo - (int)1);
			if ((_842z9590 == (int)1) | (_842z9590 == (int)2))
			{
				//* 
				//*           For A*x=(lambda)*B*x and A*B*x=(lambda)*x; 
				//*           backtransform eigenvectors: x = inv(L)**T*y or inv(U)*y 
				//* 
				
				if (_l08igmvf)
				{
					
					
					_scuo79v4 = "N";
				}
				else
				{
					
					
					_scuo79v4 = "T";
				}
				//* 
				
				_ieiywhin("Left" ,_9wyre9zc ,_scuo79v4 ,"Non-unit" ,ref _dxpq0xkr ,ref _zptdq03m ,ref Unsafe.AsRef(_kxg5drh2) ,_p9n405a5 ,ref _ly9opahg ,_vxfgpup9 ,ref _ocv8fk5c );//* 
				
			}
			else
			if (_842z9590 == (int)3)
			{
				//* 
				//*           For B*A*x=(lambda)*x; 
				//*           backtransform eigenvectors: x = L*y or U**T*y 
				//* 
				
				if (_l08igmvf)
				{
					
					
					_scuo79v4 = "T";
				}
				else
				{
					
					
					_scuo79v4 = "N";
				}
				//* 
				
				_sdtp2num("Left" ,_9wyre9zc ,_scuo79v4 ,"Non-unit" ,ref _dxpq0xkr ,ref _zptdq03m ,ref Unsafe.AsRef(_kxg5drh2) ,_p9n405a5 ,ref _ly9opahg ,_vxfgpup9 ,ref _ocv8fk5c );
			}
			
		}
		//* 
		
		*(_apig8meb+((int)1 - 1)) = REAL(_e4ueamrn);
		return;//* 
		//*     End of SSYGV 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
