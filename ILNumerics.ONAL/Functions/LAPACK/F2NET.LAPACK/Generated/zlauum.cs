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
//*> \brief \b ZLAUUM computes the product UUH or LHL, where U and L are upper or lower triangular matrices (blocked algorithm). 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZLAUUM + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zlauum.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zlauum.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zlauum.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZLAUUM( UPLO, N, A, LDA, INFO ) 
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
//*> ZLAUUM computes the product U * U**H or L**H * L, where the triangular 
//*> factor U or L is stored in the upper or lower triangular part of 
//*> the array A. 
//*> 
//*> If UPLO = 'U' or 'u' then the upper triangle of the result is stored, 
//*> overwriting the factor U in A. 
//*> If UPLO = 'L' or 'l' then the lower triangle of the result is stored, 
//*> overwriting the factor L in A. 
//*> 
//*> This is the blocked form of the algorithm, calling Level 3 BLAS. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>          Specifies whether the triangular factor stored in the array A 
//*>          is upper or lower triangular: 
//*>          = 'U':  Upper triangular 
//*>          = 'L':  Lower triangular 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the triangular factor U or L.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension (LDA,N) 
//*>          On entry, the triangular factor U or L. 
//*>          On exit, if UPLO = 'U', the upper triangle of A is 
//*>          overwritten with the upper triangle of the product U * U**H; 
//*>          if UPLO = 'L', the lower triangle of A is overwritten with 
//*>          the lower triangle of the product L**H * L. 
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
//*>          = 0: successful exit 
//*>          < 0: if INFO = -k, the k-th argument had an illegal value 
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

	 
	public static void _c6s045x1(FString _9wyre9zc, ref Int32 _dxpq0xkr, complex* _vxfgpup9, ref Int32 _ocv8fk5c, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _kxg5drh2 =  1d;
complex _40vhxf9f =   new fcomplex(1f,0f);
Boolean _l08igmvf =  default;
Int32 _b5p6od9s =  default;
Int32 _vyr1z1si =  default;
Int32 _f7059815 =  default;
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
			
			_ut9qalzx("ZLAUUM" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr == (int)0)
		return;//* 
		//*     Determine the block size for this environment. 
		//* 
		
		_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZLAUUM" ,_9wyre9zc ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) );//* 
		
		if ((_f7059815 <= (int)1) | (_f7059815 >= _dxpq0xkr))
		{
			//* 
			//*        Use unblocked code 
			//* 
			
			_zb801nwa(_9wyre9zc ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,ref _gro5yvfo );
		}
		else
		{
			//* 
			//*        Use blocked code 
			//* 
			
			if (_l08igmvf)
			{
				//* 
				//*           Compute the product U * U**H. 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn1745 = (System.Int32)((int)1);
					System.Int32 __81fgg2step1745 = (System.Int32)(_f7059815);
					System.Int32 __81fgg2count1745;
					for (__81fgg2count1745 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1745 + __81fgg2step1745) / __81fgg2step1745)), _b5p6od9s = __81fgg2dlsvn1745; __81fgg2count1745 != 0; __81fgg2count1745--, _b5p6od9s += (__81fgg2step1745)) {

					{
						
						_vyr1z1si = ILNumerics.F2NET.Intrinsics.MIN(_f7059815 ,(_dxpq0xkr - _b5p6od9s) + (int)1 );
						_dbxixtiz("Right" ,"Upper" ,"Conjugate transpose" ,"Non-unit" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref _vyr1z1si ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						_zb801nwa("Upper" ,ref _vyr1z1si ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref _gro5yvfo );
						if ((_b5p6od9s + _vyr1z1si) <= _dxpq0xkr)
						{
							
							_xos1d1er("No transpose" ,"Conjugate transpose" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref _vyr1z1si ,ref Unsafe.AsRef(((_dxpq0xkr - _b5p6od9s) - _vyr1z1si) + (int)1) ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s + _vyr1z1si - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + _vyr1z1si - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_qd483w5z("Upper" ,"No transpose" ,ref _vyr1z1si ,ref Unsafe.AsRef(((_dxpq0xkr - _b5p6od9s) - _vyr1z1si) + (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + _vyr1z1si - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						}
						
Mark10:;
						// continue
					}
										}				}
			}
			else
			{
				//* 
				//*           Compute the product L**H * L. 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn1746 = (System.Int32)((int)1);
					System.Int32 __81fgg2step1746 = (System.Int32)(_f7059815);
					System.Int32 __81fgg2count1746;
					for (__81fgg2count1746 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1746 + __81fgg2step1746) / __81fgg2step1746)), _b5p6od9s = __81fgg2dlsvn1746; __81fgg2count1746 != 0; __81fgg2count1746--, _b5p6od9s += (__81fgg2step1746)) {

					{
						
						_vyr1z1si = ILNumerics.F2NET.Intrinsics.MIN(_f7059815 ,(_dxpq0xkr - _b5p6od9s) + (int)1 );
						_dbxixtiz("Left" ,"Lower" ,"Conjugate transpose" ,"Non-unit" ,ref _vyr1z1si ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						_zb801nwa("Lower" ,ref _vyr1z1si ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref _gro5yvfo );
						if ((_b5p6od9s + _vyr1z1si) <= _dxpq0xkr)
						{
							
							_xos1d1er("Conjugate transpose" ,"No transpose" ,ref _vyr1z1si ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(((_dxpq0xkr - _b5p6od9s) - _vyr1z1si) + (int)1) ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+(_b5p6od9s + _vyr1z1si - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_b5p6od9s + _vyr1z1si - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_qd483w5z("Lower" ,"Conjugate transpose" ,ref _vyr1z1si ,ref Unsafe.AsRef(((_dxpq0xkr - _b5p6od9s) - _vyr1z1si) + (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s + _vyr1z1si - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						}
						
Mark20:;
						// continue
					}
										}				}
			}
			
		}
		//* 
		
		return;//* 
		//*     End of ZLAUUM 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
