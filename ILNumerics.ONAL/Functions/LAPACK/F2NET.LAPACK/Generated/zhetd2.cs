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
//*> \brief \b ZHETD2 reduces a Hermitian matrix to real symmetric tridiagonal form by an unitary similarity transformation (unblocked algorithm). 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZHETD2 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zhetd2.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zhetd2.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zhetd2.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZHETD2( UPLO, N, A, LDA, D, E, TAU, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          UPLO 
//*       INTEGER            INFO, LDA, N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   D( * ), E( * ) 
//*       COMPLEX*16         A( LDA, * ), TAU( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZHETD2 reduces a complex Hermitian matrix A to real symmetric 
//*> tridiagonal form T by a unitary similarity transformation: 
//*> Q**H * A * Q = T. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>          Specifies whether the upper or lower triangular part of the 
//*>          Hermitian matrix A is stored: 
//*>          = 'U':  Upper triangular 
//*>          = 'L':  Lower triangular 
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
//*>          n-by-n upper triangular part of A contains the upper 
//*>          triangular part of the matrix A, and the strictly lower 
//*>          triangular part of A is not referenced.  If UPLO = 'L', the 
//*>          leading n-by-n lower triangular part of A contains the lower 
//*>          triangular part of the matrix A, and the strictly upper 
//*>          triangular part of A is not referenced. 
//*>          On exit, if UPLO = 'U', the diagonal and first superdiagonal 
//*>          of A are overwritten by the corresponding elements of the 
//*>          tridiagonal matrix T, and the elements above the first 
//*>          superdiagonal, with the array TAU, represent the unitary 
//*>          matrix Q as a product of elementary reflectors; if UPLO 
//*>          = 'L', the diagonal and first subdiagonal of A are over- 
//*>          written by the corresponding elements of the tridiagonal 
//*>          matrix T, and the elements below the first subdiagonal, with 
//*>          the array TAU, represent the unitary matrix Q as a product 
//*>          of elementary reflectors. See Further Details. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[out] D 
//*> \verbatim 
//*>          D is DOUBLE PRECISION array, dimension (N) 
//*>          The diagonal elements of the tridiagonal matrix T: 
//*>          D(i) = A(i,i). 
//*> \endverbatim 
//*> 
//*> \param[out] E 
//*> \verbatim 
//*>          E is DOUBLE PRECISION array, dimension (N-1) 
//*>          The off-diagonal elements of the tridiagonal matrix T: 
//*>          E(i) = A(i,i+1) if UPLO = 'U', E(i) = A(i+1,i) if UPLO = 'L'. 
//*> \endverbatim 
//*> 
//*> \param[out] TAU 
//*> \verbatim 
//*>          TAU is COMPLEX*16 array, dimension (N-1) 
//*>          The scalar factors of the elementary reflectors (see Further 
//*>          Details). 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value. 
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
//*> \ingroup complex16HEcomputational 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  If UPLO = 'U', the matrix Q is represented as a product of elementary 
//*>  reflectors 
//*> 
//*>     Q = H(n-1) . . . H(2) H(1). 
//*> 
//*>  Each H(i) has the form 
//*> 
//*>     H(i) = I - tau * v * v**H 
//*> 
//*>  where tau is a complex scalar, and v is a complex vector with 
//*>  v(i+1:n) = 0 and v(i) = 1; v(1:i-1) is stored on exit in 
//*>  A(1:i-1,i+1), and tau in TAU(i). 
//*> 
//*>  If UPLO = 'L', the matrix Q is represented as a product of elementary 
//*>  reflectors 
//*> 
//*>     Q = H(1) H(2) . . . H(n-1). 
//*> 
//*>  Each H(i) has the form 
//*> 
//*>     H(i) = I - tau * v * v**H 
//*> 
//*>  where tau is a complex scalar, and v is a complex vector with 
//*>  v(1:i) = 0 and v(i+1) = 1; v(i+2:n) is stored on exit in A(i+2:n,i), 
//*>  and tau in TAU(i). 
//*> 
//*>  The contents of A on exit are illustrated by the following examples 
//*>  with n = 5: 
//*> 
//*>  if UPLO = 'U':                       if UPLO = 'L': 
//*> 
//*>    (  d   e   v2  v3  v4 )              (  d                  ) 
//*>    (      d   e   v3  v4 )              (  e   d              ) 
//*>    (          d   e   v4 )              (  v1  e   d          ) 
//*>    (              d   e  )              (  v1  v2  e   d      ) 
//*>    (                  d  )              (  v1  v2  v3  e   d  ) 
//*> 
//*>  where d and e denote diagonal and off-diagonal elements of T, and vi 
//*>  denotes an element of the vector defining H(i). 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _au997jkk(FString _9wyre9zc, ref Int32 _dxpq0xkr, complex* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _plfm7z8g, Double* _864fslqq, complex* _0446f4de, ref Int32 _gro5yvfo)
	{
#region variable declarations
complex _kxg5drh2 =   new fcomplex(1f,0f);
complex _d0547bi2 =   new fcomplex(0f,0f);
complex _gbf4169i =   new fcomplex(0.5f,0f);
Boolean _l08igmvf =  default;
Int32 _b5p6od9s =  default;
complex _r7cfteg3 =  default;
complex _uc0dsfni =  default;
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
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input parameters 
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
			
			_ut9qalzx("ZHETD2" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr <= (int)0)
		return;//* 
		
		if (_l08igmvf)
		{
			//* 
			//*        Reduce the upper triangle of A 
			//* 
			
			*(_vxfgpup9+(_dxpq0xkr - 1) + (_dxpq0xkr - 1) * 1 * (_ocv8fk5c)) = DCMPLX(ILNumerics.F2NET.Intrinsics.DBLE(*(_vxfgpup9+(_dxpq0xkr - 1) + (_dxpq0xkr - 1) * 1 * (_ocv8fk5c)) ));
			{
				System.Int32 __81fgg2dlsvn3648 = (System.Int32)((_dxpq0xkr - (int)1));
				System.Int32 __81fgg2step3648 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count3648;
				for (__81fgg2count3648 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn3648 + __81fgg2step3648) / __81fgg2step3648)), _b5p6od9s = __81fgg2dlsvn3648; __81fgg2count3648 != 0; __81fgg2count3648--, _b5p6od9s += (__81fgg2step3648)) {

				{
					//* 
					//*           Generate elementary reflector H(i) = I - tau * v * v**H 
					//*           to annihilate A(1:i-1,i+1) 
					//* 
					
					_r7cfteg3 = *(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c));
					_4btmjfem(ref _b5p6od9s ,ref _r7cfteg3 ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref _uc0dsfni );
					*(_864fslqq+(_b5p6od9s - 1)) = DBLE(_r7cfteg3);//* 
					
					if (_uc0dsfni != _d0547bi2)
					{
						//* 
						//*              Apply H(i) from both sides to A(1:i,1:i) 
						//* 
						
						*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;//* 
						//*              Compute  x := tau * A * v  storing x in TAU(1:i) 
						//* 
						
						_taqe77dx(_9wyre9zc ,ref _b5p6od9s ,ref _uc0dsfni ,_vxfgpup9 ,ref _ocv8fk5c ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,_0446f4de ,ref Unsafe.AsRef((int)1) );//* 
						//*              Compute  w := x - 1/2 * tau * (x**H * v) * v 
						//* 
						
						_r7cfteg3 = (-(((_gbf4169i * _uc0dsfni) * _s2hgtw14(ref _b5p6od9s ,_0446f4de ,ref Unsafe.AsRef((int)1) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ))));
						_chy9ita6(ref _b5p6od9s ,ref _r7cfteg3 ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,_0446f4de ,ref Unsafe.AsRef((int)1) );//* 
						//*              Apply the transformation as a rank-2 update: 
						//*                 A := A - v * w**H - w * v**H 
						//* 
						
						_tr6pyj4t(_9wyre9zc ,ref _b5p6od9s ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,_0446f4de ,ref Unsafe.AsRef((int)1) ,_vxfgpup9 ,ref _ocv8fk5c );//* 
						
					}
					else
					{
						
						*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = DCMPLX(ILNumerics.F2NET.Intrinsics.DBLE(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ));
					}
					
					*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)) = DCMPLX(*(_864fslqq+(_b5p6od9s - 1)));
					*(_plfm7z8g+(_b5p6od9s + (int)1 - 1)) = DBLE(*(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)));
					*(_0446f4de+(_b5p6od9s - 1)) = _uc0dsfni;
Mark10:;
					// continue
				}
								}			}
			*(_plfm7z8g+((int)1 - 1)) = DBLE(*(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)));
		}
		else
		{
			//* 
			//*        Reduce the lower triangle of A 
			//* 
			
			*(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) = DCMPLX(ILNumerics.F2NET.Intrinsics.DBLE(*(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) ));
			{
				System.Int32 __81fgg2dlsvn3649 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step3649 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3649;
				for (__81fgg2count3649 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn3649 + __81fgg2step3649) / __81fgg2step3649)), _b5p6od9s = __81fgg2dlsvn3649; __81fgg2count3649 != 0; __81fgg2count3649--, _b5p6od9s += (__81fgg2step3649)) {

				{
					//* 
					//*           Generate elementary reflector H(i) = I - tau * v * v**H 
					//*           to annihilate A(i+2:n,i) 
					//* 
					
					_r7cfteg3 = *(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c));
					_4btmjfem(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref _r7cfteg3 ,(_vxfgpup9+(ILNumerics.F2NET.Intrinsics.MIN(_b5p6od9s + (int)2 ,_dxpq0xkr ) - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref _uc0dsfni );
					*(_864fslqq+(_b5p6od9s - 1)) = DBLE(_r7cfteg3);//* 
					
					if (_uc0dsfni != _d0547bi2)
					{
						//* 
						//*              Apply H(i) from both sides to A(i+1:n,i+1:n) 
						//* 
						
						*(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;//* 
						//*              Compute  x := tau * A * v  storing y in TAU(i:n-1) 
						//* 
						
						_taqe77dx(_9wyre9zc ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref _uc0dsfni ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_0446f4de+(_b5p6od9s - 1)),ref Unsafe.AsRef((int)1) );//* 
						//*              Compute  w := x - 1/2 * tau * (x**H * v) * v 
						//* 
						
						_r7cfteg3 = (-(((_gbf4169i * _uc0dsfni) * _s2hgtw14(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,(_0446f4de+(_b5p6od9s - 1)),ref Unsafe.AsRef((int)1) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ))));
						_chy9ita6(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref _r7cfteg3 ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,(_0446f4de+(_b5p6od9s - 1)),ref Unsafe.AsRef((int)1) );//* 
						//*              Apply the transformation as a rank-2 update: 
						//*                 A := A - v * w**H - w * v**H 
						//* 
						
						_tr6pyj4t(_9wyre9zc ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,(_0446f4de+(_b5p6od9s - 1)),ref Unsafe.AsRef((int)1) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );//* 
						
					}
					else
					{
						
						*(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)) = DCMPLX(ILNumerics.F2NET.Intrinsics.DBLE(*(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)) ));
					}
					
					*(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = DCMPLX(*(_864fslqq+(_b5p6od9s - 1)));
					*(_plfm7z8g+(_b5p6od9s - 1)) = DBLE(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)));
					*(_0446f4de+(_b5p6od9s - 1)) = _uc0dsfni;
Mark20:;
					// continue
				}
								}			}
			*(_plfm7z8g+(_dxpq0xkr - 1)) = DBLE(*(_vxfgpup9+(_dxpq0xkr - 1) + (_dxpq0xkr - 1) * 1 * (_ocv8fk5c)));
		}
		//* 
		
		return;//* 
		//*     End of ZHETD2 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
