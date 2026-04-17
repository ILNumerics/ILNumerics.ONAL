
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
//*> \brief \b ZTRTRI 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZTRTRI + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/ztrtri.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/ztrtri.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/ztrtri.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZTRTRI( UPLO, DIAG, N, A, LDA, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          DIAG, UPLO 
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
//*> ZTRTRI computes the inverse of a complex upper or lower triangular 
//*> matrix A. 
//*> 
//*> This is the Level 3 BLAS version of the algorithm. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>          = 'U':  A is upper triangular; 
//*>          = 'L':  A is lower triangular. 
//*> \endverbatim 
//*> 
//*> \param[in] DIAG 
//*> \verbatim 
//*>          DIAG is CHARACTER*1 
//*>          = 'N':  A is non-unit triangular; 
//*>          = 'U':  A is unit triangular. 
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
//*>          On entry, the triangular matrix A.  If UPLO = 'U', the 
//*>          leading N-by-N upper triangular part of the array A contains 
//*>          the upper triangular matrix, and the strictly lower 
//*>          triangular part of A is not referenced.  If UPLO = 'L', the 
//*>          leading N-by-N lower triangular part of the array A contains 
//*>          the lower triangular matrix, and the strictly upper 
//*>          triangular part of A is not referenced.  If DIAG = 'U', the 
//*>          diagonal elements of A are also not referenced and are 
//*>          assumed to be 1. 
//*>          On exit, the (triangular) inverse of the original matrix, in 
//*>          the same storage format. 
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
//*>          < 0: if INFO = -i, the i-th argument had an illegal value 
//*>          > 0: if INFO = i, A(i,i) is exactly zero.  The triangular 
//*>               matrix is singular and its inverse can not be computed. 
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
//*> \ingroup complex16OTHERcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _3gvzsp7u(FString _9wyre9zc, FString _2scffxp3, ref Int32 _dxpq0xkr, complex* _vxfgpup9, ref Int32 _ocv8fk5c, ref Int32 _gro5yvfo)
	{
#region variable declarations
complex _kxg5drh2 =   new fcomplex(1f,0f);
complex _d0547bi2 =   new fcomplex(0f,0f);
Boolean _rcjmgxm4 =  default;
Boolean _l08igmvf =  default;
Int32 _znpjgsef =  default;
Int32 _pscq8l5q =  default;
Int32 _f7059815 =  default;
Int32 _8dgyhtzt =  default;
string fLanavab = default;
#endregion  variable declarations
_9wyre9zc = _9wyre9zc.Convert(1);
_2scffxp3 = _2scffxp3.Convert(1);

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
		_rcjmgxm4 = _w8y2rzgy(_2scffxp3 ,"N" );
		if ((!(_l08igmvf)) & (!(_w8y2rzgy(_9wyre9zc ,"L" ))))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if ((!(_rcjmgxm4)) & (!(_w8y2rzgy(_2scffxp3 ,"U" ))))
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
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("ZTRTRI" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr == (int)0)
		return;//* 
		//*     Check for singularity if non-unit. 
		//* 
		
		if (_rcjmgxm4)
		{
			
			{
				System.Int32 __81fgg2dlsvn1751 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1751 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1751;
				for (__81fgg2count1751 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1751 + __81fgg2step1751) / __81fgg2step1751)), _gro5yvfo = __81fgg2dlsvn1751; __81fgg2count1751 != 0; __81fgg2count1751--, _gro5yvfo += (__81fgg2step1751)) {

				{
					
					if (*(_vxfgpup9+(_gro5yvfo - 1) + (_gro5yvfo - 1) * 1 * (_ocv8fk5c)) == _d0547bi2)
					return;
Mark10:;
					// continue
				}
								}			}
			_gro5yvfo = (int)0;
		}
		//* 
		//*     Determine the block size for this environment. 
		//* 
		
		_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZTRTRI" ,_9wyre9zc + _2scffxp3 ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) );
		if ((_f7059815 <= (int)1) | (_f7059815 >= _dxpq0xkr))
		{
			//* 
			//*        Use unblocked code 
			//* 
			
			_1v02ox70(_9wyre9zc ,_2scffxp3 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,ref _gro5yvfo );
		}
		else
		{
			//* 
			//*        Use blocked code 
			//* 
			
			if (_l08igmvf)
			{
				//* 
				//*           Compute inverse of upper triangular matrix 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn1752 = (System.Int32)((int)1);
					System.Int32 __81fgg2step1752 = (System.Int32)(_f7059815);
					System.Int32 __81fgg2count1752;
					for (__81fgg2count1752 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1752 + __81fgg2step1752) / __81fgg2step1752)), _znpjgsef = __81fgg2dlsvn1752; __81fgg2count1752 != 0; __81fgg2count1752--, _znpjgsef += (__81fgg2step1752)) {

					{
						
						_pscq8l5q = ILNumerics.F2NET.Intrinsics.MIN(_f7059815 ,(_dxpq0xkr - _znpjgsef) + (int)1 );//* 
						//*              Compute rows 1:j-1 of current block column 
						//* 
						
						_dbxixtiz("Left" ,"Upper" ,"No transpose" ,_2scffxp3 ,ref Unsafe.AsRef(_znpjgsef - (int)1) ,ref _pscq8l5q ,ref Unsafe.AsRef(_kxg5drh2) ,_vxfgpup9 ,ref _ocv8fk5c ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						_qlsh8rhv("Right" ,"Upper" ,"No transpose" ,_2scffxp3 ,ref Unsafe.AsRef(_znpjgsef - (int)1) ,ref _pscq8l5q ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );//* 
						//*              Compute inverse of current diagonal block 
						//* 
						
						_1v02ox70("Upper" ,_2scffxp3 ,ref _pscq8l5q ,(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref _gro5yvfo );
Mark20:;
						// continue
					}
										}				}
			}
			else
			{
				//* 
				//*           Compute inverse of lower triangular matrix 
				//* 
				
				_8dgyhtzt = ((((_dxpq0xkr - (int)1) / _f7059815) * _f7059815) + (int)1);
				{
					System.Int32 __81fgg2dlsvn1753 = (System.Int32)(_8dgyhtzt);
					System.Int32 __81fgg2step1753 = (System.Int32)(-(_f7059815));
					System.Int32 __81fgg2count1753;
					for (__81fgg2count1753 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1753 + __81fgg2step1753) / __81fgg2step1753)), _znpjgsef = __81fgg2dlsvn1753; __81fgg2count1753 != 0; __81fgg2count1753--, _znpjgsef += (__81fgg2step1753)) {

					{
						
						_pscq8l5q = ILNumerics.F2NET.Intrinsics.MIN(_f7059815 ,(_dxpq0xkr - _znpjgsef) + (int)1 );
						if ((_znpjgsef + _pscq8l5q) <= _dxpq0xkr)
						{
							//* 
							//*                 Compute rows j+jb:n of current block column 
							//* 
							
							_dbxixtiz("Left" ,"Lower" ,"No transpose" ,_2scffxp3 ,ref Unsafe.AsRef(((_dxpq0xkr - _znpjgsef) - _pscq8l5q) + (int)1) ,ref _pscq8l5q ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_znpjgsef + _pscq8l5q - 1) + (_znpjgsef + _pscq8l5q - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_znpjgsef + _pscq8l5q - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							_qlsh8rhv("Right" ,"Lower" ,"No transpose" ,_2scffxp3 ,ref Unsafe.AsRef(((_dxpq0xkr - _znpjgsef) - _pscq8l5q) + (int)1) ,ref _pscq8l5q ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_znpjgsef + _pscq8l5q - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
						}
						//* 
						//*              Compute inverse of current diagonal block 
						//* 
						
						_1v02ox70("Lower" ,_2scffxp3 ,ref _pscq8l5q ,(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref _gro5yvfo );
Mark30:;
						// continue
					}
										}				}
			}
			
		}
		//* 
		
		return;//* 
		//*     End of ZTRTRI 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
