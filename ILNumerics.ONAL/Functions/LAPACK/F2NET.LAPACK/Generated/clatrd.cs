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
//*> \brief \b CLATRD reduces the first nb rows and columns of a symmetric/Hermitian matrix A to real tridiagonal form by an unitary similarity transformation. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CLATRD + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/clatrd.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/clatrd.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/clatrd.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CLATRD( UPLO, N, NB, A, LDA, E, TAU, W, LDW ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          UPLO 
//*       INTEGER            LDA, LDW, N, NB 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               E( * ) 
//*       COMPLEX            A( LDA, * ), TAU( * ), W( LDW, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CLATRD reduces NB rows and columns of a complex Hermitian matrix A to 
//*> Hermitian tridiagonal form by a unitary similarity 
//*> transformation Q**H * A * Q, and returns the matrices V and W which are 
//*> needed to apply the transformation to the unreduced part of A. 
//*> 
//*> If UPLO = 'U', CLATRD reduces the last NB rows and columns of a 
//*> matrix, of which the upper triangle is supplied; 
//*> if UPLO = 'L', CLATRD reduces the first NB rows and columns of a 
//*> matrix, of which the lower triangle is supplied. 
//*> 
//*> This is an auxiliary routine called by CHETRD. 
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
//*>          = 'U': Upper triangular 
//*>          = 'L': Lower triangular 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix A. 
//*> \endverbatim 
//*> 
//*> \param[in] NB 
//*> \verbatim 
//*>          NB is INTEGER 
//*>          The number of rows and columns to be reduced. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is COMPLEX array, dimension (LDA,N) 
//*>          On entry, the Hermitian matrix A.  If UPLO = 'U', the leading 
//*>          n-by-n upper triangular part of A contains the upper 
//*>          triangular part of the matrix A, and the strictly lower 
//*>          triangular part of A is not referenced.  If UPLO = 'L', the 
//*>          leading n-by-n lower triangular part of A contains the lower 
//*>          triangular part of the matrix A, and the strictly upper 
//*>          triangular part of A is not referenced. 
//*>          On exit: 
//*>          if UPLO = 'U', the last NB columns have been reduced to 
//*>            tridiagonal form, with the diagonal elements overwriting 
//*>            the diagonal elements of A; the elements above the diagonal 
//*>            with the array TAU, represent the unitary matrix Q as a 
//*>            product of elementary reflectors; 
//*>          if UPLO = 'L', the first NB columns have been reduced to 
//*>            tridiagonal form, with the diagonal elements overwriting 
//*>            the diagonal elements of A; the elements below the diagonal 
//*>            with the array TAU, represent the  unitary matrix Q as a 
//*>            product of elementary reflectors. 
//*>          See Further Details. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[out] E 
//*> \verbatim 
//*>          E is REAL array, dimension (N-1) 
//*>          If UPLO = 'U', E(n-nb:n-1) contains the superdiagonal 
//*>          elements of the last NB columns of the reduced matrix; 
//*>          if UPLO = 'L', E(1:nb) contains the subdiagonal elements of 
//*>          the first NB columns of the reduced matrix. 
//*> \endverbatim 
//*> 
//*> \param[out] TAU 
//*> \verbatim 
//*>          TAU is COMPLEX array, dimension (N-1) 
//*>          The scalar factors of the elementary reflectors, stored in 
//*>          TAU(n-nb:n-1) if UPLO = 'U', and in TAU(1:nb) if UPLO = 'L'. 
//*>          See Further Details. 
//*> \endverbatim 
//*> 
//*> \param[out] W 
//*> \verbatim 
//*>          W is COMPLEX array, dimension (LDW,NB) 
//*>          The n-by-nb matrix W required to update the unreduced part 
//*>          of A. 
//*> \endverbatim 
//*> 
//*> \param[in] LDW 
//*> \verbatim 
//*>          LDW is INTEGER 
//*>          The leading dimension of the array W. LDW >= max(1,N). 
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
//*> \ingroup complexOTHERauxiliary 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  If UPLO = 'U', the matrix Q is represented as a product of elementary 
//*>  reflectors 
//*> 
//*>     Q = H(n) H(n-1) . . . H(n-nb+1). 
//*> 
//*>  Each H(i) has the form 
//*> 
//*>     H(i) = I - tau * v * v**H 
//*> 
//*>  where tau is a complex scalar, and v is a complex vector with 
//*>  v(i:n) = 0 and v(i-1) = 1; v(1:i-1) is stored on exit in A(1:i-1,i), 
//*>  and tau in TAU(i-1). 
//*> 
//*>  If UPLO = 'L', the matrix Q is represented as a product of elementary 
//*>  reflectors 
//*> 
//*>     Q = H(1) H(2) . . . H(nb). 
//*> 
//*>  Each H(i) has the form 
//*> 
//*>     H(i) = I - tau * v * v**H 
//*> 
//*>  where tau is a complex scalar, and v is a complex vector with 
//*>  v(1:i) = 0 and v(i+1) = 1; v(i+1:n) is stored on exit in A(i+1:n,i), 
//*>  and tau in TAU(i). 
//*> 
//*>  The elements of the vectors v together form the n-by-nb matrix V 
//*>  which is needed, with W, to apply the transformation to the unreduced 
//*>  part of the matrix, using a Hermitian rank-2k update of the form: 
//*>  A := A - V*W**H - W*V**H. 
//*> 
//*>  The contents of A on exit are illustrated by the following examples 
//*>  with n = 5 and nb = 2: 
//*> 
//*>  if UPLO = 'U':                       if UPLO = 'L': 
//*> 
//*>    (  a   a   a   v4  v5 )              (  d                  ) 
//*>    (      a   a   v4  v5 )              (  1   d              ) 
//*>    (          a   1   v5 )              (  v1  1   a          ) 
//*>    (              d   1  )              (  v1  v2  a   a      ) 
//*>    (                  d  )              (  v1  v2  a   a   a  ) 
//*> 
//*>  where d denotes a diagonal element of the reduced matrix, a denotes 
//*>  an element of the original matrix that is unchanged, and vi denotes 
//*>  an element of the vector defining H(i). 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _fy10g36o(FString _9wyre9zc, ref Int32 _dxpq0xkr, ref Int32 _f7059815, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, Single* _864fslqq, fcomplex* _0446f4de, fcomplex* _z1ioc3c8, ref Int32 _aax48utu)
	{
#region variable declarations
fcomplex _d0547bi2 =   new fcomplex(0f,0f);
fcomplex _kxg5drh2 =   new fcomplex(1f,0f);
fcomplex _gbf4169i =   new fcomplex(0.5f,0f);
Int32 _b5p6od9s =  default;
Int32 _11qhqs00 =  default;
fcomplex _r7cfteg3 =  default;
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
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr <= (int)0)
		return;//* 
		
		if (_w8y2rzgy(_9wyre9zc ,"U" ))
		{
			//* 
			//*        Reduce last NB columns of upper triangle 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn3558 = (System.Int32)(_dxpq0xkr);
				System.Int32 __81fgg2step3558 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count3558;
				for (__81fgg2count3558 = System.Math.Max(0, (System.Int32)(((System.Int32)((_dxpq0xkr - _f7059815) + (int)1) - __81fgg2dlsvn3558 + __81fgg2step3558) / __81fgg2step3558)), _b5p6od9s = __81fgg2dlsvn3558; __81fgg2count3558 != 0; __81fgg2count3558--, _b5p6od9s += (__81fgg2step3558)) {

				{
					
					_11qhqs00 = ((_b5p6od9s - _dxpq0xkr) + _f7059815);
					if (_b5p6od9s < _dxpq0xkr)
					{
						//* 
						//*              Update A(1:i,i) 
						//* 
						
						*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = CMPLX(ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ));
						_png2g84j(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,(_z1ioc3c8+(_b5p6od9s - 1) + (_11qhqs00 + (int)1 - 1) * 1 * (_aax48utu)),ref _aax48utu );
						_f0oh3lvv("No transpose" ,ref _b5p6od9s ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_z1ioc3c8+(_b5p6od9s - 1) + (_11qhqs00 + (int)1 - 1) * 1 * (_aax48utu)),ref _aax48utu ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
						_png2g84j(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,(_z1ioc3c8+(_b5p6od9s - 1) + (_11qhqs00 + (int)1 - 1) * 1 * (_aax48utu)),ref _aax48utu );
						_png2g84j(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						_f0oh3lvv("No transpose" ,ref _b5p6od9s ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_z1ioc3c8+((int)1 - 1) + (_11qhqs00 + (int)1 - 1) * 1 * (_aax48utu)),ref _aax48utu ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
						_png2g84j(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = CMPLX(ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ));
					}
					
					if (_b5p6od9s > (int)1)
					{
						//* 
						//*              Generate elementary reflector H(i) to annihilate 
						//*              A(1:i-2,i) 
						//* 
						
						_r7cfteg3 = *(_vxfgpup9+(_b5p6od9s - (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c));
						_ocp87dc1(ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref _r7cfteg3 ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_0446f4de+(_b5p6od9s - (int)1 - 1))) );
						*(_864fslqq+(_b5p6od9s - (int)1 - 1)) = REAL(_r7cfteg3);
						*(_vxfgpup9+(_b5p6od9s - (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;//* 
						//*              Compute W(1:i-1,i) 
						//* 
						
						_c637kid8("Upper" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,_vxfgpup9 ,ref _ocv8fk5c ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_z1ioc3c8+((int)1 - 1) + (_11qhqs00 - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
						if (_b5p6od9s < _dxpq0xkr)
						{
							
							_f0oh3lvv("Conjugate transpose" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(_kxg5drh2) ,(_z1ioc3c8+((int)1 - 1) + (_11qhqs00 + (int)1 - 1) * 1 * (_aax48utu)),ref _aax48utu ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_z1ioc3c8+(_b5p6od9s + (int)1 - 1) + (_11qhqs00 - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
							_f0oh3lvv("No transpose" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_z1ioc3c8+(_b5p6od9s + (int)1 - 1) + (_11qhqs00 - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_z1ioc3c8+((int)1 - 1) + (_11qhqs00 - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
							_f0oh3lvv("Conjugate transpose" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_z1ioc3c8+(_b5p6od9s + (int)1 - 1) + (_11qhqs00 - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
							_f0oh3lvv("No transpose" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_z1ioc3c8+((int)1 - 1) + (_11qhqs00 + (int)1 - 1) * 1 * (_aax48utu)),ref _aax48utu ,(_z1ioc3c8+(_b5p6od9s + (int)1 - 1) + (_11qhqs00 - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_z1ioc3c8+((int)1 - 1) + (_11qhqs00 - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
						}
						
						_00l5hgpk(ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(*(_0446f4de+(_b5p6od9s - (int)1 - 1))) ,(_z1ioc3c8+((int)1 - 1) + (_11qhqs00 - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
						_r7cfteg3 = (-(((_gbf4169i * *(_0446f4de+(_b5p6od9s - (int)1 - 1))) * _f18dve92(ref Unsafe.AsRef(_b5p6od9s - (int)1) ,(_z1ioc3c8+((int)1 - 1) + (_11qhqs00 - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ))));
						_0vz4nsob(ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref _r7cfteg3 ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,(_z1ioc3c8+((int)1 - 1) + (_11qhqs00 - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
					}
					//* 
					
Mark10:;
					// continue
				}
								}			}
		}
		else
		{
			//* 
			//*        Reduce first NB columns of lower triangle 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn3559 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step3559 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3559;
				for (__81fgg2count3559 = System.Math.Max(0, (System.Int32)(((System.Int32)(_f7059815) - __81fgg2dlsvn3559 + __81fgg2step3559) / __81fgg2step3559)), _b5p6od9s = __81fgg2dlsvn3559; __81fgg2count3559 != 0; __81fgg2count3559--, _b5p6od9s += (__81fgg2step3559)) {

				{
					//* 
					//*           Update A(i:n,i) 
					//* 
					
					*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = CMPLX(ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ));
					_png2g84j(ref Unsafe.AsRef(_b5p6od9s - (int)1) ,(_z1ioc3c8+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_aax48utu)),ref _aax48utu );
					_f0oh3lvv("No transpose" ,ref Unsafe.AsRef((_dxpq0xkr - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_z1ioc3c8+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_aax48utu)),ref _aax48utu ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
					_png2g84j(ref Unsafe.AsRef(_b5p6od9s - (int)1) ,(_z1ioc3c8+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_aax48utu)),ref _aax48utu );
					_png2g84j(ref Unsafe.AsRef(_b5p6od9s - (int)1) ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
					_f0oh3lvv("No transpose" ,ref Unsafe.AsRef((_dxpq0xkr - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_z1ioc3c8+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_aax48utu)),ref _aax48utu ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
					_png2g84j(ref Unsafe.AsRef(_b5p6od9s - (int)1) ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
					*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = CMPLX(ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ));
					if (_b5p6od9s < _dxpq0xkr)
					{
						//* 
						//*              Generate elementary reflector H(i) to annihilate 
						//*              A(i+2:n,i) 
						//* 
						
						_r7cfteg3 = *(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c));
						_ocp87dc1(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref _r7cfteg3 ,(_vxfgpup9+(ILNumerics.F2NET.Intrinsics.MIN(_b5p6od9s + (int)2 ,_dxpq0xkr ) - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_0446f4de+(_b5p6od9s - 1))) );
						*(_864fslqq+(_b5p6od9s - 1)) = REAL(_r7cfteg3);
						*(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;//* 
						//*              Compute W(i+1:n,i) 
						//* 
						
						_c637kid8("Lower" ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_z1ioc3c8+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
						_f0oh3lvv("Conjugate transpose" ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_z1ioc3c8+(_b5p6od9s + (int)1 - 1) + ((int)1 - 1) * 1 * (_aax48utu)),ref _aax48utu ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_z1ioc3c8+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
						_f0oh3lvv("No transpose" ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_z1ioc3c8+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_z1ioc3c8+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
						_f0oh3lvv("Conjugate transpose" ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_z1ioc3c8+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
						_f0oh3lvv("No transpose" ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_z1ioc3c8+(_b5p6od9s + (int)1 - 1) + ((int)1 - 1) * 1 * (_aax48utu)),ref _aax48utu ,(_z1ioc3c8+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_z1ioc3c8+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
						_00l5hgpk(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(*(_0446f4de+(_b5p6od9s - 1))) ,(_z1ioc3c8+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
						_r7cfteg3 = (-(((_gbf4169i * *(_0446f4de+(_b5p6od9s - 1))) * _f18dve92(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,(_z1ioc3c8+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ))));
						_0vz4nsob(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref _r7cfteg3 ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,(_z1ioc3c8+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
					}
					//* 
					
Mark20:;
					// continue
				}
								}			}
		}
		//* 
		
		return;//* 
		//*     End of CLATRD 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
