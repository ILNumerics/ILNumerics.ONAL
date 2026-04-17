
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
//*> \brief \b ZHEGS2 reduces a Hermitian definite generalized eigenproblem to standard form, using the factorization results obtained from cpotrf (unblocked algorithm). 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZHEGS2 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zhegs2.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zhegs2.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zhegs2.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZHEGS2( ITYPE, UPLO, N, A, LDA, B, LDB, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          UPLO 
//*       INTEGER            INFO, ITYPE, LDA, LDB, N 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX*16         A( LDA, * ), B( LDB, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZHEGS2 reduces a complex Hermitian-definite generalized 
//*> eigenproblem to standard form. 
//*> 
//*> If ITYPE = 1, the problem is A*x = lambda*B*x, 
//*> and A is overwritten by inv(U**H)*A*inv(U) or inv(L)*A*inv(L**H) 
//*> 
//*> If ITYPE = 2 or 3, the problem is A*B*x = lambda*x or 
//*> B*A*x = lambda*x, and A is overwritten by U*A*U**H or L**H *A*L. 
//*> 
//*> B must have been previously factorized as U**H *U or L*L**H by ZPOTRF. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] ITYPE 
//*> \verbatim 
//*>          ITYPE is INTEGER 
//*>          = 1: compute inv(U**H)*A*inv(U) or inv(L)*A*inv(L**H); 
//*>          = 2 or 3: compute U*A*U**H or L**H *A*L. 
//*> \endverbatim 
//*> 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>          Specifies whether the upper or lower triangular part of the 
//*>          Hermitian matrix A is stored, and how B has been factorized. 
//*>          = 'U':  Upper triangular 
//*>          = 'L':  Lower triangular 
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
//*>          A is COMPLEX*16 array, dimension (LDA,N) 
//*>          On entry, the Hermitian matrix A.  If UPLO = 'U', the leading 
//*>          n by n upper triangular part of A contains the upper 
//*>          triangular part of the matrix A, and the strictly lower 
//*>          triangular part of A is not referenced.  If UPLO = 'L', the 
//*>          leading n by n lower triangular part of A contains the lower 
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
//*> \param[in,out] B 
//*> \verbatim 
//*>          B is COMPLEX*16 array, dimension (LDB,N) 
//*>          The triangular factor from the Cholesky factorization of B, 
//*>          as returned by ZPOTRF. 
//*>          B is modified by the routine but restored on exit. 
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
//*>          = 0:  successful exit. 
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
//*  ===================================================================== 

	 
	public static void _nkwprxsp(ref Int32 _842z9590, FString _9wyre9zc, ref Int32 _dxpq0xkr, complex* _vxfgpup9, ref Int32 _ocv8fk5c, complex* _p9n405a5, ref Int32 _ly9opahg, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _kxg5drh2 =  1d;
Double _gbf4169i =  0.5d;
complex _40vhxf9f =   new fcomplex(1f,0f);
Boolean _l08igmvf =  default;
Int32 _umlkckdg =  default;
Double _2v7qhxyg =  default;
Double _t4j86qde =  default;
complex _hr7bl0wv =  default;
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
			
			_ut9qalzx("ZHEGS2" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		
		if (_842z9590 == (int)1)
		{
			
			if (_l08igmvf)
			{
				//* 
				//*           Compute inv(U**H)*A*inv(U) 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn3945 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3945 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3945;
					for (__81fgg2count3945 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3945 + __81fgg2step3945) / __81fgg2step3945)), _umlkckdg = __81fgg2dlsvn3945; __81fgg2count3945 != 0; __81fgg2count3945--, _umlkckdg += (__81fgg2step3945)) {

					{
						//* 
						//*              Update the upper triangle of A(k:n,k:n) 
						//* 
						
						_2v7qhxyg = DBLE(*(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
						_t4j86qde = DBLE(*(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)));
						_2v7qhxyg = (_2v7qhxyg / __POW2(_t4j86qde));
						*(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) = DCMPLX(_2v7qhxyg);
						if (_umlkckdg < _dxpq0xkr)
						{
							
							_z5tkm94d(ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2 / _t4j86qde) ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_hr7bl0wv = DCMPLX((-((_gbf4169i * _2v7qhxyg))));
							_42wgkyoq(ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_42wgkyoq(ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg );
							_chy9ita6(ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _hr7bl0wv ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_tr6pyj4t(_9wyre9zc ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref Unsafe.AsRef(-(_40vhxf9f)) ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_vxfgpup9+(_umlkckdg + (int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_chy9ita6(ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _hr7bl0wv ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_42wgkyoq(ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg );
							_tqoxi2p2(_9wyre9zc ,"Conjugate transpose" ,"Non-unit" ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,(_p9n405a5+(_umlkckdg + (int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_42wgkyoq(ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						}
						
Mark10:;
						// continue
					}
										}				}
			}
			else
			{
				//* 
				//*           Compute inv(L)*A*inv(L**H) 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn3946 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3946 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3946;
					for (__81fgg2count3946 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3946 + __81fgg2step3946) / __81fgg2step3946)), _umlkckdg = __81fgg2dlsvn3946; __81fgg2count3946 != 0; __81fgg2count3946--, _umlkckdg += (__81fgg2step3946)) {

					{
						//* 
						//*              Update the lower triangle of A(k:n,k:n) 
						//* 
						
						_2v7qhxyg = DBLE(*(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
						_t4j86qde = DBLE(*(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)));
						_2v7qhxyg = (_2v7qhxyg / __POW2(_t4j86qde));
						*(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) = DCMPLX(_2v7qhxyg);
						if (_umlkckdg < _dxpq0xkr)
						{
							
							_z5tkm94d(ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2 / _t4j86qde) ,(_vxfgpup9+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
							_hr7bl0wv = DCMPLX((-((_gbf4169i * _2v7qhxyg))));
							_chy9ita6(ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _hr7bl0wv ,(_p9n405a5+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref Unsafe.AsRef((int)1) ,(_vxfgpup9+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
							_tr6pyj4t(_9wyre9zc ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref Unsafe.AsRef(-(_40vhxf9f)) ,(_vxfgpup9+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,(_p9n405a5+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref Unsafe.AsRef((int)1) ,(_vxfgpup9+(_umlkckdg + (int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_chy9ita6(ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _hr7bl0wv ,(_p9n405a5+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref Unsafe.AsRef((int)1) ,(_vxfgpup9+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
							_tqoxi2p2(_9wyre9zc ,"No transpose" ,"Non-unit" ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,(_p9n405a5+(_umlkckdg + (int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_vxfgpup9+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
						}
						
Mark20:;
						// continue
					}
										}				}
			}
			
		}
		else
		{
			
			if (_l08igmvf)
			{
				//* 
				//*           Compute U*A*U**H 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn3947 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3947 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3947;
					for (__81fgg2count3947 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3947 + __81fgg2step3947) / __81fgg2step3947)), _umlkckdg = __81fgg2dlsvn3947; __81fgg2count3947 != 0; __81fgg2count3947--, _umlkckdg += (__81fgg2step3947)) {

					{
						//* 
						//*              Update the upper triangle of A(1:k,1:k) 
						//* 
						
						_2v7qhxyg = DBLE(*(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
						_t4j86qde = DBLE(*(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)));
						_xajlj6s7(_9wyre9zc ,"No transpose" ,"Non-unit" ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,_p9n405a5 ,ref _ly9opahg ,(_vxfgpup9+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
						_hr7bl0wv = DCMPLX((_gbf4169i * _2v7qhxyg));
						_chy9ita6(ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref _hr7bl0wv ,(_p9n405a5+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref Unsafe.AsRef((int)1) ,(_vxfgpup9+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
						_tr6pyj4t(_9wyre9zc ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,(_p9n405a5+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref Unsafe.AsRef((int)1) ,_vxfgpup9 ,ref _ocv8fk5c );
						_chy9ita6(ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref _hr7bl0wv ,(_p9n405a5+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref Unsafe.AsRef((int)1) ,(_vxfgpup9+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
						_z5tkm94d(ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref _t4j86qde ,(_vxfgpup9+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
						*(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) = DCMPLX((_2v7qhxyg * __POW2(_t4j86qde)));
Mark30:;
						// continue
					}
										}				}
			}
			else
			{
				//* 
				//*           Compute L**H *A*L 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn3948 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3948 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3948;
					for (__81fgg2count3948 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3948 + __81fgg2step3948) / __81fgg2step3948)), _umlkckdg = __81fgg2dlsvn3948; __81fgg2count3948 != 0; __81fgg2count3948--, _umlkckdg += (__81fgg2step3948)) {

					{
						//* 
						//*              Update the lower triangle of A(1:k,1:k) 
						//* 
						
						_2v7qhxyg = DBLE(*(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)));
						_t4j86qde = DBLE(*(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)));
						_42wgkyoq(ref Unsafe.AsRef(_umlkckdg - (int)1) ,(_vxfgpup9+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						_xajlj6s7(_9wyre9zc ,"Conjugate transpose" ,"Non-unit" ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,_p9n405a5 ,ref _ly9opahg ,(_vxfgpup9+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						_hr7bl0wv = DCMPLX((_gbf4169i * _2v7qhxyg));
						_42wgkyoq(ref Unsafe.AsRef(_umlkckdg - (int)1) ,(_p9n405a5+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg );
						_chy9ita6(ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref _hr7bl0wv ,(_p9n405a5+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_vxfgpup9+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						_tr6pyj4t(_9wyre9zc ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,_vxfgpup9 ,ref _ocv8fk5c );
						_chy9ita6(ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref _hr7bl0wv ,(_p9n405a5+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_vxfgpup9+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						_42wgkyoq(ref Unsafe.AsRef(_umlkckdg - (int)1) ,(_p9n405a5+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg );
						_z5tkm94d(ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref _t4j86qde ,(_vxfgpup9+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						_42wgkyoq(ref Unsafe.AsRef(_umlkckdg - (int)1) ,(_vxfgpup9+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						*(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)) = DCMPLX((_2v7qhxyg * __POW2(_t4j86qde)));
Mark40:;
						// continue
					}
										}				}
			}
			
		}
		
		return;//* 
		//*     End of ZHEGS2 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
