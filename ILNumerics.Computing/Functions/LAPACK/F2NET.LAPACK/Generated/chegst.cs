
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
//*> \brief \b CHEGST 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CHEGST + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/chegst.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/chegst.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/chegst.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CHEGST( ITYPE, UPLO, N, A, LDA, B, LDB, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          UPLO 
//*       INTEGER            INFO, ITYPE, LDA, LDB, N 
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
//*> CHEGST reduces a complex Hermitian-definite generalized 
//*> eigenproblem to standard form. 
//*> 
//*> If ITYPE = 1, the problem is A*x = lambda*B*x, 
//*> and A is overwritten by inv(U**H)*A*inv(U) or inv(L)*A*inv(L**H) 
//*> 
//*> If ITYPE = 2 or 3, the problem is A*B*x = lambda*x or 
//*> B*A*x = lambda*x, and A is overwritten by U*A*U**H or L**H*A*L. 
//*> 
//*> B must have been previously factorized as U**H*U or L*L**H by CPOTRF. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] ITYPE 
//*> \verbatim 
//*>          ITYPE is INTEGER 
//*>          = 1: compute inv(U**H)*A*inv(U) or inv(L)*A*inv(L**H); 
//*>          = 2 or 3: compute U*A*U**H or L**H*A*L. 
//*> \endverbatim 
//*> 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>          = 'U':  Upper triangle of A is stored and B is factored as 
//*>                  U**H*U; 
//*>          = 'L':  Lower triangle of A is stored and B is factored as 
//*>                  L*L**H. 
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
//*>          A is COMPLEX array, dimension (LDA,N) 
//*>          On entry, the Hermitian matrix A.  If UPLO = 'U', the leading 
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
//*> \param[in,out] B 
//*> \verbatim 
//*>          B is COMPLEX array, dimension (LDB,N) 
//*>          The triangular factor from the Cholesky factorization of B, 
//*>          as returned by CPOTRF. 
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
//*> \ingroup complexHEcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _ndvppb6i(ref Int32 _842z9590, FString _9wyre9zc, ref Int32 _dxpq0xkr, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, fcomplex* _p9n405a5, ref Int32 _ly9opahg, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _kxg5drh2 =  1f;
fcomplex _40vhxf9f =   new fcomplex(1f,0f);
fcomplex _gbf4169i =   new fcomplex(0.5f,0f);
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
			
			_ut9qalzx("CHEGST" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr == (int)0)
		return;//* 
		//*     Determine the block size for this environment. 
		//* 
		
		_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"CHEGST" ,_9wyre9zc ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) );//* 
		
		if ((_f7059815 <= (int)1) | (_f7059815 >= _dxpq0xkr))
		{
			//* 
			//*        Use unblocked code 
			//* 
			
			_ao9x3rgn(ref _842z9590 ,_9wyre9zc ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_p9n405a5 ,ref _ly9opahg ,ref _gro5yvfo );
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
					//*              Compute inv(U**H)*A*inv(U) 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn3880 = (System.Int32)((int)1);
						System.Int32 __81fgg2step3880 = (System.Int32)(_f7059815);
						System.Int32 __81fgg2count3880;
						for (__81fgg2count3880 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3880 + __81fgg2step3880) / __81fgg2step3880)), _umlkckdg = __81fgg2dlsvn3880; __81fgg2count3880 != 0; __81fgg2count3880--, _umlkckdg += (__81fgg2step3880)) {

						{
							
							_93gbwsug = ILNumerics.F2NET.Intrinsics.MIN((_dxpq0xkr - _umlkckdg) + (int)1 ,_f7059815 );//* 
							//*                 Update the upper triangle of A(k:n,k:n) 
							//* 
							
							_ao9x3rgn(ref _842z9590 ,_9wyre9zc ,ref _93gbwsug ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref _gro5yvfo );
							if ((_umlkckdg + _93gbwsug) <= _dxpq0xkr)
							{
								
								_goj1gzmg("Left" ,_9wyre9zc ,"Conjugate transpose" ,"Non-unit" ,ref _93gbwsug ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) - _93gbwsug) + (int)1) ,ref Unsafe.AsRef(_40vhxf9f) ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg + _93gbwsug - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
								_7hsjii7x("Left" ,_9wyre9zc ,ref _93gbwsug ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) - _93gbwsug) + (int)1) ,ref Unsafe.AsRef(-(_gbf4169i)) ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg + _93gbwsug - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg + _93gbwsug - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
								_qpixscj7(_9wyre9zc ,"Conjugate transpose" ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) - _93gbwsug) + (int)1) ,ref _93gbwsug ,ref Unsafe.AsRef(-(_40vhxf9f)) ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg + _93gbwsug - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg + _93gbwsug - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_umlkckdg + _93gbwsug - 1) + (_umlkckdg + _93gbwsug - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
								_7hsjii7x("Left" ,_9wyre9zc ,ref _93gbwsug ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) - _93gbwsug) + (int)1) ,ref Unsafe.AsRef(-(_gbf4169i)) ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg + _93gbwsug - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg + _93gbwsug - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
								_goj1gzmg("Right" ,_9wyre9zc ,"No transpose" ,"Non-unit" ,ref _93gbwsug ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) - _93gbwsug) + (int)1) ,ref Unsafe.AsRef(_40vhxf9f) ,(_p9n405a5+(_umlkckdg + _93gbwsug - 1) + (_umlkckdg + _93gbwsug - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg + _93gbwsug - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							}
							
Mark10:;
							// continue
						}
												}					}
				}
				else
				{
					//* 
					//*              Compute inv(L)*A*inv(L**H) 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn3881 = (System.Int32)((int)1);
						System.Int32 __81fgg2step3881 = (System.Int32)(_f7059815);
						System.Int32 __81fgg2count3881;
						for (__81fgg2count3881 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3881 + __81fgg2step3881) / __81fgg2step3881)), _umlkckdg = __81fgg2dlsvn3881; __81fgg2count3881 != 0; __81fgg2count3881--, _umlkckdg += (__81fgg2step3881)) {

						{
							
							_93gbwsug = ILNumerics.F2NET.Intrinsics.MIN((_dxpq0xkr - _umlkckdg) + (int)1 ,_f7059815 );//* 
							//*                 Update the lower triangle of A(k:n,k:n) 
							//* 
							
							_ao9x3rgn(ref _842z9590 ,_9wyre9zc ,ref _93gbwsug ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref _gro5yvfo );
							if ((_umlkckdg + _93gbwsug) <= _dxpq0xkr)
							{
								
								_goj1gzmg("Right" ,_9wyre9zc ,"Conjugate transpose" ,"Non-unit" ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) - _93gbwsug) + (int)1) ,ref _93gbwsug ,ref Unsafe.AsRef(_40vhxf9f) ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_vxfgpup9+(_umlkckdg + _93gbwsug - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
								_7hsjii7x("Right" ,_9wyre9zc ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) - _93gbwsug) + (int)1) ,ref _93gbwsug ,ref Unsafe.AsRef(-(_gbf4169i)) ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg + _93gbwsug - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+(_umlkckdg + _93gbwsug - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
								_qpixscj7(_9wyre9zc ,"No transpose" ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) - _93gbwsug) + (int)1) ,ref _93gbwsug ,ref Unsafe.AsRef(-(_40vhxf9f)) ,(_vxfgpup9+(_umlkckdg + _93gbwsug - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg + _93gbwsug - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_umlkckdg + _93gbwsug - 1) + (_umlkckdg + _93gbwsug - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
								_7hsjii7x("Right" ,_9wyre9zc ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) - _93gbwsug) + (int)1) ,ref _93gbwsug ,ref Unsafe.AsRef(-(_gbf4169i)) ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg + _93gbwsug - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+(_umlkckdg + _93gbwsug - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
								_goj1gzmg("Left" ,_9wyre9zc ,"No transpose" ,"Non-unit" ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) - _93gbwsug) + (int)1) ,ref _93gbwsug ,ref Unsafe.AsRef(_40vhxf9f) ,(_p9n405a5+(_umlkckdg + _93gbwsug - 1) + (_umlkckdg + _93gbwsug - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_vxfgpup9+(_umlkckdg + _93gbwsug - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
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
					//*              Compute U*A*U**H 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn3882 = (System.Int32)((int)1);
						System.Int32 __81fgg2step3882 = (System.Int32)(_f7059815);
						System.Int32 __81fgg2count3882;
						for (__81fgg2count3882 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3882 + __81fgg2step3882) / __81fgg2step3882)), _umlkckdg = __81fgg2dlsvn3882; __81fgg2count3882 != 0; __81fgg2count3882--, _umlkckdg += (__81fgg2step3882)) {

						{
							
							_93gbwsug = ILNumerics.F2NET.Intrinsics.MIN((_dxpq0xkr - _umlkckdg) + (int)1 ,_f7059815 );//* 
							//*                 Update the upper triangle of A(1:k+kb-1,1:k+kb-1) 
							//* 
							
							_smeynpn3("Left" ,_9wyre9zc ,"No transpose" ,"Non-unit" ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref _93gbwsug ,ref Unsafe.AsRef(_40vhxf9f) ,_p9n405a5 ,ref _ly9opahg ,(_vxfgpup9+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_7hsjii7x("Right" ,_9wyre9zc ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref _93gbwsug ,ref Unsafe.AsRef(_gbf4169i) ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_qpixscj7(_9wyre9zc ,"No transpose" ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref _93gbwsug ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_kxg5drh2) ,_vxfgpup9 ,ref _ocv8fk5c );
							_7hsjii7x("Right" ,_9wyre9zc ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref _93gbwsug ,ref Unsafe.AsRef(_gbf4169i) ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_smeynpn3("Right" ,_9wyre9zc ,"Conjugate transpose" ,"Non-unit" ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref _93gbwsug ,ref Unsafe.AsRef(_40vhxf9f) ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_vxfgpup9+((int)1 - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_ao9x3rgn(ref _842z9590 ,_9wyre9zc ,ref _93gbwsug ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref _gro5yvfo );
Mark30:;
							// continue
						}
												}					}
				}
				else
				{
					//* 
					//*              Compute L**H*A*L 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn3883 = (System.Int32)((int)1);
						System.Int32 __81fgg2step3883 = (System.Int32)(_f7059815);
						System.Int32 __81fgg2count3883;
						for (__81fgg2count3883 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3883 + __81fgg2step3883) / __81fgg2step3883)), _umlkckdg = __81fgg2dlsvn3883; __81fgg2count3883 != 0; __81fgg2count3883--, _umlkckdg += (__81fgg2step3883)) {

						{
							
							_93gbwsug = ILNumerics.F2NET.Intrinsics.MIN((_dxpq0xkr - _umlkckdg) + (int)1 ,_f7059815 );//* 
							//*                 Update the lower triangle of A(1:k+kb-1,1:k+kb-1) 
							//* 
							
							_smeynpn3("Right" ,_9wyre9zc ,"No transpose" ,"Non-unit" ,ref _93gbwsug ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref Unsafe.AsRef(_40vhxf9f) ,_p9n405a5 ,ref _ly9opahg ,(_vxfgpup9+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_7hsjii7x("Left" ,_9wyre9zc ,ref _93gbwsug ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref Unsafe.AsRef(_gbf4169i) ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_qpixscj7(_9wyre9zc ,"Conjugate transpose" ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref _93gbwsug ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_kxg5drh2) ,_vxfgpup9 ,ref _ocv8fk5c );
							_7hsjii7x("Left" ,_9wyre9zc ,ref _93gbwsug ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref Unsafe.AsRef(_gbf4169i) ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_smeynpn3("Left" ,_9wyre9zc ,"Conjugate transpose" ,"Non-unit" ,ref _93gbwsug ,ref Unsafe.AsRef(_umlkckdg - (int)1) ,ref Unsafe.AsRef(_40vhxf9f) ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,(_vxfgpup9+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_ao9x3rgn(ref _842z9590 ,_9wyre9zc ,ref _93gbwsug ,(_vxfgpup9+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_p9n405a5+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_ly9opahg)),ref _ly9opahg ,ref _gro5yvfo );
Mark40:;
							// continue
						}
												}					}
				}
				
			}
			
		}
		
		return;//* 
		//*     End of CHEGST 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
