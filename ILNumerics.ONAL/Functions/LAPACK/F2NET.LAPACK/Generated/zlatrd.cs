
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
//*> \brief \b ZLATRD reduces the first nb rows and columns of a symmetric/Hermitian matrix A to real tridiagonal form by an unitary similarity transformation. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZLATRD + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zlatrd.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zlatrd.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zlatrd.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZLATRD( UPLO, N, NB, A, LDA, E, TAU, W, LDW ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          UPLO 
//*       INTEGER            LDA, LDW, N, NB 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   E( * ) 
//*       COMPLEX*16         A( LDA, * ), TAU( * ), W( LDW, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZLATRD reduces NB rows and columns of a complex Hermitian matrix A to 
//*> Hermitian tridiagonal form by a unitary similarity 
//*> transformation Q**H * A * Q, and returns the matrices V and W which are 
//*> needed to apply the transformation to the unreduced part of A. 
//*> 
//*> If UPLO = 'U', ZLATRD reduces the last NB rows and columns of a 
//*> matrix, of which the upper triangle is supplied; 
//*> if UPLO = 'L', ZLATRD reduces the first NB rows and columns of a 
//*> matrix, of which the lower triangle is supplied. 
//*> 
//*> This is an auxiliary routine called by ZHETRD. 
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
//*>          A is COMPLEX*16 array, dimension (LDA,N) 
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
//*>          E is DOUBLE PRECISION array, dimension (N-1) 
//*>          If UPLO = 'U', E(n-nb:n-1) contains the superdiagonal 
//*>          elements of the last NB columns of the reduced matrix; 
//*>          if UPLO = 'L', E(1:nb) contains the subdiagonal elements of 
//*>          the first NB columns of the reduced matrix. 
//*> \endverbatim 
//*> 
//*> \param[out] TAU 
//*> \verbatim 
//*>          TAU is COMPLEX*16 array, dimension (N-1) 
//*>          The scalar factors of the elementary reflectors, stored in 
//*>          TAU(n-nb:n-1) if UPLO = 'U', and in TAU(1:nb) if UPLO = 'L'. 
//*>          See Further Details. 
//*> \endverbatim 
//*> 
//*> \param[out] W 
//*> \verbatim 
//*>          W is COMPLEX*16 array, dimension (LDW,NB) 
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
//*> \ingroup complex16OTHERauxiliary 
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

	 
	public static void _fpvay7gp(FString _9wyre9zc, ref Int32 _dxpq0xkr, ref Int32 _f7059815, complex* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _864fslqq, complex* _0446f4de, complex* _z1ioc3c8, ref Int32 _aax48utu)
	{
#region variable declarations
complex _d0547bi2 =   new fcomplex(0f,0f);
complex _kxg5drh2 =   new fcomplex(1f,0f);
complex _gbf4169i =   new fcomplex(0.5f,0f);
Int32 _b5p6od9s =  default;
Int32 _11qhqs00 =  default;
complex _r7cfteg3 =  default;
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
				System.Int32 __81fgg2dlsvn3670 = (System.Int32)(_dxpq0xkr);
				System.Int32 __81fgg2step3670 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count3670;
				for (__81fgg2count3670 = System.Math.Max(0, (System.Int32)(((System.Int32)((_dxpq0xkr - _f7059815) + (int)1) - __81fgg2dlsvn3670 + __81fgg2step3670) / __81fgg2step3670)), _b5p6od9s = __81fgg2dlsvn3670; __81fgg2count3670 != 0; __81fgg2count3670--, _b5p6od9s += (__81fgg2step3670)) {

				{
					
					_11qhqs00 = ((_b5p6od9s - _dxpq0xkr) + _f7059815);
					if (_b5p6od9s < _dxpq0xkr)
					{
						//* 
						//*              Update A(1:i,i) 
						//* 
						
						*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = DCMPLX(ILNumerics.F2NET.Intrinsics.DBLE(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ));
						_42wgkyoq(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,(_z1ioc3c8+(_b5p6od9s - 1) + (_11qhqs00 + (int)1 - 1) * 1 * (_aax48utu)),ref _aax48utu );
						_xfaqgfxk("No transpose" ,ref _b5p6od9s ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_z1ioc3c8+(_b5p6od9s - 1) + (_11qhqs00 + (int)1 - 1) * 1 * (_aax48utu)),ref _aax48utu ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
						_42wgkyoq(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,(_z1ioc3c8+(_b5p6od9s - 1) + (_11qhqs00 + (int)1 - 1) * 1 * (_aax48utu)),ref _aax48utu );
						_42wgkyoq(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						_xfaqgfxk("No transpose" ,ref _b5p6od9s ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_z1ioc3c8+((int)1 - 1) + (_11qhqs00 + (int)1 - 1) * 1 * (_aax48utu)),ref _aax48utu ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
						_42wgkyoq(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = DCMPLX(ILNumerics.F2NET.Intrinsics.DBLE(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ));
					}
					
					if (_b5p6od9s > (int)1)
					{
						//* 
						//*              Generate elementary reflector H(i) to annihilate 
						//*              A(1:i-2,i) 
						//* 
						
						_r7cfteg3 = *(_vxfgpup9+(_b5p6od9s - (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c));
						_4btmjfem(ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref _r7cfteg3 ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_0446f4de+(_b5p6od9s - (int)1 - 1))) );
						*(_864fslqq+(_b5p6od9s - (int)1 - 1)) = DBLE(_r7cfteg3);
						*(_vxfgpup9+(_b5p6od9s - (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;//* 
						//*              Compute W(1:i-1,i) 
						//* 
						
						_taqe77dx("Upper" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,_vxfgpup9 ,ref _ocv8fk5c ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_z1ioc3c8+((int)1 - 1) + (_11qhqs00 - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
						if (_b5p6od9s < _dxpq0xkr)
						{
							
							_xfaqgfxk("Conjugate transpose" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(_kxg5drh2) ,(_z1ioc3c8+((int)1 - 1) + (_11qhqs00 + (int)1 - 1) * 1 * (_aax48utu)),ref _aax48utu ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_z1ioc3c8+(_b5p6od9s + (int)1 - 1) + (_11qhqs00 - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
							_xfaqgfxk("No transpose" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_z1ioc3c8+(_b5p6od9s + (int)1 - 1) + (_11qhqs00 - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_z1ioc3c8+((int)1 - 1) + (_11qhqs00 - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
							_xfaqgfxk("Conjugate transpose" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_z1ioc3c8+(_b5p6od9s + (int)1 - 1) + (_11qhqs00 - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
							_xfaqgfxk("No transpose" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_z1ioc3c8+((int)1 - 1) + (_11qhqs00 + (int)1 - 1) * 1 * (_aax48utu)),ref _aax48utu ,(_z1ioc3c8+(_b5p6od9s + (int)1 - 1) + (_11qhqs00 - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_z1ioc3c8+((int)1 - 1) + (_11qhqs00 - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
						}
						
						_wv0on4xy(ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(*(_0446f4de+(_b5p6od9s - (int)1 - 1))) ,(_z1ioc3c8+((int)1 - 1) + (_11qhqs00 - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
						_r7cfteg3 = (-(((_gbf4169i * *(_0446f4de+(_b5p6od9s - (int)1 - 1))) * _s2hgtw14(ref Unsafe.AsRef(_b5p6od9s - (int)1) ,(_z1ioc3c8+((int)1 - 1) + (_11qhqs00 - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ))));
						_chy9ita6(ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref _r7cfteg3 ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,(_z1ioc3c8+((int)1 - 1) + (_11qhqs00 - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
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
				System.Int32 __81fgg2dlsvn3671 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step3671 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3671;
				for (__81fgg2count3671 = System.Math.Max(0, (System.Int32)(((System.Int32)(_f7059815) - __81fgg2dlsvn3671 + __81fgg2step3671) / __81fgg2step3671)), _b5p6od9s = __81fgg2dlsvn3671; __81fgg2count3671 != 0; __81fgg2count3671--, _b5p6od9s += (__81fgg2step3671)) {

				{
					//* 
					//*           Update A(i:n,i) 
					//* 
					
					*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = DCMPLX(ILNumerics.F2NET.Intrinsics.DBLE(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ));
					_42wgkyoq(ref Unsafe.AsRef(_b5p6od9s - (int)1) ,(_z1ioc3c8+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_aax48utu)),ref _aax48utu );
					_xfaqgfxk("No transpose" ,ref Unsafe.AsRef((_dxpq0xkr - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_z1ioc3c8+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_aax48utu)),ref _aax48utu ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
					_42wgkyoq(ref Unsafe.AsRef(_b5p6od9s - (int)1) ,(_z1ioc3c8+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_aax48utu)),ref _aax48utu );
					_42wgkyoq(ref Unsafe.AsRef(_b5p6od9s - (int)1) ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
					_xfaqgfxk("No transpose" ,ref Unsafe.AsRef((_dxpq0xkr - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_z1ioc3c8+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_aax48utu)),ref _aax48utu ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
					_42wgkyoq(ref Unsafe.AsRef(_b5p6od9s - (int)1) ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
					*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = DCMPLX(ILNumerics.F2NET.Intrinsics.DBLE(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ));
					if (_b5p6od9s < _dxpq0xkr)
					{
						//* 
						//*              Generate elementary reflector H(i) to annihilate 
						//*              A(i+2:n,i) 
						//* 
						
						_r7cfteg3 = *(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c));
						_4btmjfem(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref _r7cfteg3 ,(_vxfgpup9+(ILNumerics.F2NET.Intrinsics.MIN(_b5p6od9s + (int)2 ,_dxpq0xkr ) - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_0446f4de+(_b5p6od9s - 1))) );
						*(_864fslqq+(_b5p6od9s - 1)) = DBLE(_r7cfteg3);
						*(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;//* 
						//*              Compute W(i+1:n,i) 
						//* 
						
						_taqe77dx("Lower" ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_z1ioc3c8+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
						_xfaqgfxk("Conjugate transpose" ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_z1ioc3c8+(_b5p6od9s + (int)1 - 1) + ((int)1 - 1) * 1 * (_aax48utu)),ref _aax48utu ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_z1ioc3c8+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
						_xfaqgfxk("No transpose" ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_z1ioc3c8+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_z1ioc3c8+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
						_xfaqgfxk("Conjugate transpose" ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_z1ioc3c8+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
						_xfaqgfxk("No transpose" ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_z1ioc3c8+(_b5p6od9s + (int)1 - 1) + ((int)1 - 1) * 1 * (_aax48utu)),ref _aax48utu ,(_z1ioc3c8+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_z1ioc3c8+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
						_wv0on4xy(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(*(_0446f4de+(_b5p6od9s - 1))) ,(_z1ioc3c8+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
						_r7cfteg3 = (-(((_gbf4169i * *(_0446f4de+(_b5p6od9s - 1))) * _s2hgtw14(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,(_z1ioc3c8+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ))));
						_chy9ita6(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref _r7cfteg3 ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,(_z1ioc3c8+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_aax48utu)),ref Unsafe.AsRef((int)1) );
					}
					//* 
					
Mark20:;
					// continue
				}
								}			}
		}
		//* 
		
		return;//* 
		//*     End of ZLATRD 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
