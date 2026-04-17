
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
//*> \brief \b CLAUU2 computes the product UUH or LHL, where U and L are upper or lower triangular matrices (unblocked algorithm). 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CLAUU2 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/clauu2.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/clauu2.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/clauu2.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CLAUU2( UPLO, N, A, LDA, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          UPLO 
//*       INTEGER            INFO, LDA, N 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX            A( LDA, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CLAUU2 computes the product U * U**H or L**H * L, where the triangular 
//*> factor U or L is stored in the upper or lower triangular part of 
//*> the array A. 
//*> 
//*> If UPLO = 'U' or 'u' then the upper triangle of the result is stored, 
//*> overwriting the factor U in A. 
//*> If UPLO = 'L' or 'l' then the lower triangle of the result is stored, 
//*> overwriting the factor L in A. 
//*> 
//*> This is the unblocked form of the algorithm, calling Level 2 BLAS. 
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
//*>          A is COMPLEX array, dimension (LDA,N) 
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
//*> \ingroup complexOTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _oc5u9amp(FString _9wyre9zc, ref Int32 _dxpq0xkr, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, ref Int32 _gro5yvfo)
	{
#region variable declarations
fcomplex _kxg5drh2 =   new fcomplex(1f,0f);
Boolean _l08igmvf =  default;
Int32 _b5p6od9s =  default;
Single _qmqa6kps =  default;
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
			
			_ut9qalzx("CLAUU2" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr == (int)0)
		return;//* 
		
		if (_l08igmvf)
		{
			//* 
			//*        Compute the product U * U**H. 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn1736 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1736 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1736;
				for (__81fgg2count1736 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1736 + __81fgg2step1736) / __81fgg2step1736)), _b5p6od9s = __81fgg2dlsvn1736; __81fgg2count1736 != 0; __81fgg2count1736--, _b5p6od9s += (__81fgg2step1736)) {

				{
					
					_qmqa6kps = REAL(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)));
					if (_b5p6od9s < _dxpq0xkr)
					{
						
						*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = CMPLX(((_qmqa6kps * _qmqa6kps) + ILNumerics.F2NET.Intrinsics.REAL(_f18dve92(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ) )));
						_png2g84j(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						_f0oh3lvv("No transpose" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.CMPLX(_qmqa6kps )) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
						_png2g84j(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
					}
					else
					{
						
						_2ylagitj(ref _b5p6od9s ,ref _qmqa6kps ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
					}
					
Mark10:;
					// continue
				}
								}			}//* 
			
		}
		else
		{
			//* 
			//*        Compute the product L**H * L. 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn1737 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1737 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1737;
				for (__81fgg2count1737 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1737 + __81fgg2step1737) / __81fgg2step1737)), _b5p6od9s = __81fgg2dlsvn1737; __81fgg2count1737 != 0; __81fgg2count1737--, _b5p6od9s += (__81fgg2step1737)) {

				{
					
					_qmqa6kps = REAL(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)));
					if (_b5p6od9s < _dxpq0xkr)
					{
						
						*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = CMPLX(((_qmqa6kps * _qmqa6kps) + ILNumerics.F2NET.Intrinsics.REAL(_f18dve92(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ) )));
						_png2g84j(ref Unsafe.AsRef(_b5p6od9s - (int)1) ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						_f0oh3lvv("Conjugate transpose" ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.CMPLX(_qmqa6kps )) ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						_png2g84j(ref Unsafe.AsRef(_b5p6od9s - (int)1) ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
					}
					else
					{
						
						_2ylagitj(ref _b5p6od9s ,ref _qmqa6kps ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
					}
					
Mark20:;
					// continue
				}
								}			}
		}
		//* 
		
		return;//* 
		//*     End of CLAUU2 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
