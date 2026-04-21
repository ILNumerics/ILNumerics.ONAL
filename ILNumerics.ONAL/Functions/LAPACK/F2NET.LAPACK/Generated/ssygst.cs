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
//*> \brief \b SSYGST 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SSYGST + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/ssygst.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/ssygst.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/ssygst.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SSYGST( ITYPE, UPLO, N, A, LDA, B, LDB, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          UPLO 
//*       INTEGER            INFO, ITYPE, LDA, LDB, N 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               A( LDA, * ), B( LDB, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SSYGST reduces a real symmetric-definite generalized eigenproblem 
//*> to standard form. 
//*> 
//*> If ITYPE = 1, the problem is A*x = lambda*B*x, 
//*> and A is overwritten by inv(U**T)*A*inv(U) or inv(L)*A*inv(L**T) 
//*> 
//*> If ITYPE = 2 or 3, the problem is A*B*x = lambda*x or 
//*> B*A*x = lambda*x, and A is overwritten by U*A*U**T or L**T*A*L. 
//*> 
//*> B must have been previously factorized as U**T*U or L*L**T by SPOTRF. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] ITYPE 
//*> \verbatim 
//*>          ITYPE is INTEGER 
//*>          = 1: compute inv(U**T)*A*inv(U) or inv(L)*A*inv(L**T); 
//*>          = 2 or 3: compute U*A*U**T or L**T*A*L. 
//*> \endverbatim 
//*> 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>          = 'U':  Upper triangle of A is stored and B is factored as 
//*>                  U**T*U; 
//*>          = 'L':  Lower triangle of A is stored and B is factored as 
//*>                  L*L**T. 
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
//*>          A is REAL array, dimension (LDA,N) 
//*>          On entry, the symmetric matrix A.  If UPLO = 'U', the leading 
//*>          N-by-N upper triangular part of A contains the upper 
//*>          triangular part of the matrix A, and the strictly lower 
//*>          triangular part of A is not referenced.  If UPLO = 'L', the 
//*>          leading N-by-N lower triangular part of A contains the lower 
//*>          triangular part of the matrix A, and the strictly upper 
//*>          triangular part of A is not referenced. 
//*> 
//*>          On exit, if INFO = 0, the transformed matrix, stored in the 
//*>          same format as A. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[in] B 
//*> \verbatim 
//*>          B is REAL array, dimension (LDB,N) 
//*>          The triangular factor from the Cholesky factorization of B, 
//*>          as returned by SPOTRF. 
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
//*> \ingroup realSYcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _kwjz3u26(ref Int32 _842z9590, FString _9wyre9zc, ref Int32 _dxpq0xkr, Single* _vxfgpup9, ref Int32 _ocv8fk5c, Single* _p9n405a5, ref Int32 _ly9opahg, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _kxg5drh2 =  1f;
Single _gbf4169i =  0.5f;
Boolean _l08igmvf =  default;
Int32 _umlkckdg =  default;
Int32 _93gbwsug =  default;
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
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input parameters. 
		//* 
		
		_gro5yvfo = (int)0;
		_l08igmvf = _w8y2rzgy(_9wyre9zc ,"U" );
		if ((_842z9590 < (int)1) | (_842z9590 > (int)3))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if ((!(_l08igmvf)) & (!(_w8y2rzgy(_9wyre9zc ,"L" ))))
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_dxpq0xkr < (int)0)
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
			
			_gro5yvfo = (int)-7;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("SSYGST" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr == (int)0)
		return;//* 
		//*     Determine the block size for this environment. 
		//* 
		
		_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"SSYGST" ,_9wyre9zc ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) );//* 
		
		if ((_f7059815 <= (int)1) | (_f7059815 >= _dxpq0xkr))
		{
			//* 
			//*        Use unblocked code 
			//* 
			
			_7ij25w1y(ref _842z9590 ,_9wyre9zc ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_p9n405a5 ,ref _ly9opahg ,ref _gro5yvfo );
		}
		else
		{
			//* 
			//*        Use blocked code 
			//* 
			
			if (_842z9590 == (int)1)
			{
				
				if (_l08igmvf)
				{
					//* 
					//*              Compute inv(U**T)*A*inv(U) 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn3803 = (System.Int32)((int)1);
						System.Int32 __81fgg2step3803 = (System.Int32)(_f7059815);
						System.Int32 __81fgg2count3803;
						for (__81fgg2count3803 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3803 + __81fgg2step3803) / __81fgg2step3803)), _umlkckdg = __81fgg2dlsvn3803; __81fgg2count3803 != 0; __81fgg2count3803--, _umlkckdg += (__81fgg2step3803)) {

						{
							
							_93gbwsug = ILNumerics.F2NET.Intrinsics.MIN((_dxpq0xkr - _umlkckdg) + (int)1 ,_f7059815 );//* 
							//*                 Update the upper triangle of A(k:n,k:n) 
							//* 
							
							_7ij25w1y(ref _842z9590 ,_9wyre9zc ,ref _93gbwsug ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref _gro5yvfo );
							if ((_umlkckdg + _93gbwsug) <= _dxpq0xkr)
							{
								
								_ieiywhin("Left" ,_9wyre9zc ,"Transpose" ,"Non-unit" ,ref _93gbwsug ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) - _93gbwsug) + (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg + _93gbwsug - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
								_imym557o("Left" ,_9wyre9zc ,ref _93gbwsug ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) - _93gbwsug) + (int)1) ,ref Unsafe.AsRef(-(_gbf4169i)) ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg + _93gbwsug - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg + _93gbwsug - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
								_kk3tn6zp(_9wyre9zc ,"Transpose" ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) - _93gbwsug) + (int)1) ,ref _93gbwsug ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg + _93gbwsug - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg + _93gbwsug - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_umlkckdg + _93gbwsug - 1) + (_umlkckdg + _93gbwsug - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
								_imym557o("Left" ,_9wyre9zc ,ref _93gbwsug ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) - _93gbwsug) + (int)1) ,ref Unsafe.AsRef(-(_gbf4169i)) ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg + _93gbwsug - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg + _93gbwsug - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
								_ieiywhin("Right" ,_9wyre9zc ,"No transpose" ,"Non-unit" ,ref _93gbwsug ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) - _93gbwsug) + (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_p9n405a5+(_umlkckdg + _93gbwsug - 1) + (_umlkckdg + _93gbwsug - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg + _93gbwsug - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							}
							
Mark10:;
							// continue
						}
												}					}
				}
				else
				{
					//* 
					//*              Compute inv(L)*A*inv(L**T) 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn3804 = (System.Int32)((int)1);
						System.Int32 __81fgg2step3804 = (System.Int32)(_f7059815);
						System.Int32 __81fgg2count3804;
						for (__81fgg2count3804 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3804 + __81fgg2step3804) / __81fgg2step3804)), _umlkckdg = __81fgg2dlsvn3804; __81fgg2count3804 != 0; __81fgg2count3804--, _umlkckdg += (__81fgg2step3804)) {

						{
							
							_93gbwsug = ILNumerics.F2NET.Intrinsics.MIN((_dxpq0xkr - _umlkckdg) + (int)1 ,_f7059815 );//* 
							//*                 Update the lower triangle of A(k:n,k:n) 
							//* 
							
							_7ij25w1y(ref _842z9590 ,_9wyre9zc ,ref _93gbwsug ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref _gro5yvfo );
							if ((_umlkckdg + _93gbwsug) <= _dxpq0xkr)
							{
								
								_ieiywhin("Right" ,_9wyre9zc ,"Transpose" ,"Non-unit" ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) - _93gbwsug) + (int)1) ,ref _93gbwsug ,ref Unsafe.AsRef(_kxg5drh2) ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_vxfgpup9+(_umlkckdg + _93gbwsug - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
								_imym557o("Right" ,_9wyre9zc ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) - _93gbwsug) + (int)1) ,ref _93gbwsug ,ref Unsafe.AsRef(-(_gbf4169i)) ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg + _93gbwsug - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_umlkckdg + _93gbwsug - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
								_kk3tn6zp(_9wyre9zc ,"No transpose" ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) - _93gbwsug) + (int)1) ,ref _93gbwsug ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+(_umlkckdg + _93gbwsug - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg + _93gbwsug - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_umlkckdg + _93gbwsug - 1) + (_umlkckdg + _93gbwsug - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
								_imym557o("Right" ,_9wyre9zc ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) - _93gbwsug) + (int)1) ,ref _93gbwsug ,ref Unsafe.AsRef(-(_gbf4169i)) ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg + _93gbwsug - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_umlkckdg + _93gbwsug - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
								_ieiywhin("Left" ,_9wyre9zc ,"No transpose" ,"Non-unit" ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) - _93gbwsug) + (int)1) ,ref _93gbwsug ,ref Unsafe.AsRef(_kxg5drh2) ,(_p9n405a5+(_umlkckdg + _93gbwsug - 1) + (_umlkckdg + _93gbwsug - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_vxfgpup9+(_umlkckdg + _93gbwsug - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							}
							
Mark20:;
							// continue
						}
												}					}
				}
				
			}
			else
			{
				
				if (_l08igmvf)
				{
					//* 
					//*              Compute U*A*U**T 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn3805 = (System.Int32)((int)1);
						System.Int32 __81fgg2step3805 = (System.Int32)(_f7059815);
						System.Int32 __81fgg2count3805;
						for (__81fgg2count3805 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3805 + __81fgg2step3805) / __81fgg2step3805)), _umlkckdg = __81fgg2dlsvn3805; __81fgg2count3805 != 0; __81fgg2count3805--, _umlkckdg += (__81fgg2step3805)) {

						{
							
							_93gbwsug = ILNumerics.F2NET.Intrinsics.MIN((_dxpq0xkr - _umlkckdg) + (int)1 ,_f7059815 );//* 
							//*                 Update the upper triangle of A(1:k+kb-1,1:k+kb-1) 
							//* 
							
							_sdtp2num("Left" ,_9wyre9zc ,"No transpose" ,"Non-unit" ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref _93gbwsug ,ref Unsafe.AsRef(_kxg5drh2) ,_p9n405a5 ,ref _ly9opahg ,(_vxfgpup9+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_imym557o("Right" ,_9wyre9zc ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref _93gbwsug ,ref Unsafe.AsRef(_gbf4169i) ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_kk3tn6zp(_9wyre9zc ,"No transpose" ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref _93gbwsug ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_kxg5drh2) ,_vxfgpup9 ,ref _ocv8fk5c );
							_imym557o("Right" ,_9wyre9zc ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref _93gbwsug ,ref Unsafe.AsRef(_gbf4169i) ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_sdtp2num("Right" ,_9wyre9zc ,"Transpose" ,"Non-unit" ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref _93gbwsug ,ref Unsafe.AsRef(_kxg5drh2) ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_vxfgpup9+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_7ij25w1y(ref _842z9590 ,_9wyre9zc ,ref _93gbwsug ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref _gro5yvfo );
Mark30:;
							// continue
						}
												}					}
				}
				else
				{
					//* 
					//*              Compute L**T*A*L 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn3806 = (System.Int32)((int)1);
						System.Int32 __81fgg2step3806 = (System.Int32)(_f7059815);
						System.Int32 __81fgg2count3806;
						for (__81fgg2count3806 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3806 + __81fgg2step3806) / __81fgg2step3806)), _umlkckdg = __81fgg2dlsvn3806; __81fgg2count3806 != 0; __81fgg2count3806--, _umlkckdg += (__81fgg2step3806)) {

						{
							
							_93gbwsug = ILNumerics.F2NET.Intrinsics.MIN((_dxpq0xkr - _umlkckdg) + (int)1 ,_f7059815 );//* 
							//*                 Update the lower triangle of A(1:k+kb-1,1:k+kb-1) 
							//* 
							
							_sdtp2num("Right" ,_9wyre9zc ,"No transpose" ,"Non-unit" ,ref _93gbwsug ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,_p9n405a5 ,ref _ly9opahg ,(_vxfgpup9+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_imym557o("Left" ,_9wyre9zc ,ref _93gbwsug ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref Unsafe.AsRef(_gbf4169i) ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_kk3tn6zp(_9wyre9zc ,"Transpose" ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref _93gbwsug ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_kxg5drh2) ,_vxfgpup9 ,ref _ocv8fk5c );
							_imym557o("Left" ,_9wyre9zc ,ref _93gbwsug ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref Unsafe.AsRef(_gbf4169i) ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_sdtp2num("Left" ,_9wyre9zc ,"Transpose" ,"Non-unit" ,ref _93gbwsug ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_vxfgpup9+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_7ij25w1y(ref _842z9590 ,_9wyre9zc ,ref _93gbwsug ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref _gro5yvfo );
Mark40:;
							// continue
						}
												}					}
				}
				
			}
			
		}
		
		return;//* 
		//*     End of SSYGST 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
