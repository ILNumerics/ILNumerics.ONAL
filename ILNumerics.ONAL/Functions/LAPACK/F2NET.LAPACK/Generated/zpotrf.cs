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
//*> \brief \b ZPOTRF 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZPOTRF + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zpotrf.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zpotrf.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zpotrf.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZPOTRF( UPLO, N, A, LDA, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          UPLO 
//*       INTEGER            INFO, LDA, N 
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
//*> ZPOTRF computes the Cholesky factorization of a complex Hermitian 
//*> positive definite matrix A. 
//*> 
//*> The factorization has the form 
//*>    A = U**H * U,  if UPLO = 'U', or 
//*>    A = L  * L**H,  if UPLO = 'L', 
//*> where U is an upper triangular matrix and L is lower triangular. 
//*> 
//*> This is the block version of the algorithm, calling Level 3 BLAS. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>          = 'U':  Upper triangle of A is stored; 
//*>          = 'L':  Lower triangle of A is stored. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix A.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension (LDA,N) 
//*>          On entry, the Hermitian matrix A.  If UPLO = 'U', the leading 
//*>          N-by-N upper triangular part of A contains the upper 
//*>          triangular part of the matrix A, and the strictly lower 
//*>          triangular part of A is not referenced.  If UPLO = 'L', the 
//*>          leading N-by-N lower triangular part of A contains the lower 
//*>          triangular part of the matrix A, and the strictly upper 
//*>          triangular part of A is not referenced. 
//*> 
//*>          On exit, if INFO = 0, the factor U or L from the Cholesky 
//*>          factorization A = U**H *U or A = L*L**H. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value 
//*>          > 0:  if INFO = i, the leading minor of order i is not 
//*>                positive definite, and the factorization could not be 
//*>                completed. 
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
//*> \ingroup complex16POcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _30im5i83(FString _9wyre9zc, ref Int32 _dxpq0xkr, complex* _vxfgpup9, ref Int32 _ocv8fk5c, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _kxg5drh2 =  1d;
complex _40vhxf9f =   new fcomplex(1f,0f);
Boolean _l08igmvf =  default;
Int32 _znpjgsef =  default;
Int32 _pscq8l5q =  default;
Int32 _f7059815 =  default;
string fLanavab = default;
#endregion  variable declarations
_9wyre9zc = _9wyre9zc.Convert(1);

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
		_l08igmvf = _w8y2rzgy(_9wyre9zc ,"U" );
		if ((!(_l08igmvf)) & (!(_w8y2rzgy(_9wyre9zc ,"L" ))))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)-4;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("ZPOTRF" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr == (int)0)
		return;//* 
		//*     Determine the block size for this environment. 
		//* 
		
		_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZPOTRF" ,_9wyre9zc ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) );
		if ((_f7059815 <= (int)1) | (_f7059815 >= _dxpq0xkr))
		{
			//* 
			//*        Use unblocked code. 
			//* 
			
			_b0r9w8vc(_9wyre9zc ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,ref _gro5yvfo );
		}
		else
		{
			//* 
			//*        Use blocked code. 
			//* 
			
			if (_l08igmvf)
			{
				//* 
				//*           Compute the Cholesky factorization A = U**H *U. 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn1650 = (System.Int32)((int)1);
					System.Int32 __81fgg2step1650 = (System.Int32)(_f7059815);
					System.Int32 __81fgg2count1650;
					for (__81fgg2count1650 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1650 + __81fgg2step1650) / __81fgg2step1650)), _znpjgsef = __81fgg2dlsvn1650; __81fgg2count1650 != 0; __81fgg2count1650--, _znpjgsef += (__81fgg2step1650)) {

					{
						//* 
						//*              Update and factorize the current diagonal block and test 
						//*              for non-positive-definiteness. 
						//* 
						
						_pscq8l5q = ILNumerics.F2NET.Intrinsics.MIN(_f7059815 ,(_dxpq0xkr - _znpjgsef) + (int)1 );
						_qd483w5z("Upper" ,"Conjugate transpose" ,ref _pscq8l5q ,ref Unsafe.AsRef(_znpjgsef - (int)1) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						_b0r9w8vc("Upper" ,ref _pscq8l5q ,(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref _gro5yvfo );
						if (_gro5yvfo != (int)0)goto Mark30;
						if ((_znpjgsef + _pscq8l5q) <= _dxpq0xkr)
						{
							//* 
							//*                 Compute the current block row. 
							//* 
							
							_xos1d1er("Conjugate transpose" ,"No transpose" ,ref _pscq8l5q ,ref Unsafe.AsRef(((_dxpq0xkr - _znpjgsef) - _pscq8l5q) + (int)1) ,ref Unsafe.AsRef(_znpjgsef - (int)1) ,ref Unsafe.AsRef(-(_40vhxf9f)) ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef + _pscq8l5q - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef + _pscq8l5q - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_qlsh8rhv("Left" ,"Upper" ,"Conjugate transpose" ,"Non-unit" ,ref _pscq8l5q ,ref Unsafe.AsRef(((_dxpq0xkr - _znpjgsef) - _pscq8l5q) + (int)1) ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef + _pscq8l5q - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						}
						
Mark10:;
						// continue
					}
										}				}//* 
				
			}
			else
			{
				//* 
				//*           Compute the Cholesky factorization A = L*L**H. 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn1651 = (System.Int32)((int)1);
					System.Int32 __81fgg2step1651 = (System.Int32)(_f7059815);
					System.Int32 __81fgg2count1651;
					for (__81fgg2count1651 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1651 + __81fgg2step1651) / __81fgg2step1651)), _znpjgsef = __81fgg2dlsvn1651; __81fgg2count1651 != 0; __81fgg2count1651--, _znpjgsef += (__81fgg2step1651)) {

					{
						//* 
						//*              Update and factorize the current diagonal block and test 
						//*              for non-positive-definiteness. 
						//* 
						
						_pscq8l5q = ILNumerics.F2NET.Intrinsics.MIN(_f7059815 ,(_dxpq0xkr - _znpjgsef) + (int)1 );
						_qd483w5z("Lower" ,"No transpose" ,ref _pscq8l5q ,ref Unsafe.AsRef(_znpjgsef - (int)1) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						_b0r9w8vc("Lower" ,ref _pscq8l5q ,(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref _gro5yvfo );
						if (_gro5yvfo != (int)0)goto Mark30;
						if ((_znpjgsef + _pscq8l5q) <= _dxpq0xkr)
						{
							//* 
							//*                 Compute the current block column. 
							//* 
							
							_xos1d1er("No transpose" ,"Conjugate transpose" ,ref Unsafe.AsRef(((_dxpq0xkr - _znpjgsef) - _pscq8l5q) + (int)1) ,ref _pscq8l5q ,ref Unsafe.AsRef(_znpjgsef - (int)1) ,ref Unsafe.AsRef(-(_40vhxf9f)) ,(_vxfgpup9+(_znpjgsef + _pscq8l5q - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+(_znpjgsef + _pscq8l5q - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_qlsh8rhv("Right" ,"Lower" ,"Conjugate transpose" ,"Non-unit" ,ref Unsafe.AsRef(((_dxpq0xkr - _znpjgsef) - _pscq8l5q) + (int)1) ,ref _pscq8l5q ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_znpjgsef + _pscq8l5q - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						}
						
Mark20:;
						// continue
					}
										}				}
			}
			
		}
		goto Mark40;//* 
		
Mark30:;
		// continue
		_gro5yvfo = ((_gro5yvfo + _znpjgsef) - (int)1);//* 
		
Mark40:;
		// continue
		return;//* 
		//*     End of ZPOTRF 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
