
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
//*> \brief \b CTRTI2 computes the inverse of a triangular matrix (unblocked algorithm). 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CTRTI2 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/ctrti2.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/ctrti2.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/ctrti2.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CTRTI2( UPLO, DIAG, N, A, LDA, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          DIAG, UPLO 
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
//*> CTRTI2 computes the inverse of a complex upper or lower triangular 
//*> matrix. 
//*> 
//*> This is the Level 2 BLAS version of the algorithm. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>          Specifies whether the matrix A is upper or lower triangular. 
//*>          = 'U':  Upper triangular 
//*>          = 'L':  Lower triangular 
//*> \endverbatim 
//*> 
//*> \param[in] DIAG 
//*> \verbatim 
//*>          DIAG is CHARACTER*1 
//*>          Specifies whether or not the matrix A is unit triangular. 
//*>          = 'N':  Non-unit triangular 
//*>          = 'U':  Unit triangular 
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
//*>          A is COMPLEX array, dimension (LDA,N) 
//*>          On entry, the triangular matrix A.  If UPLO = 'U', the 
//*>          leading n by n upper triangular part of the array A contains 
//*>          the upper triangular matrix, and the strictly lower 
//*>          triangular part of A is not referenced.  If UPLO = 'L', the 
//*>          leading n by n lower triangular part of the array A contains 
//*>          the lower triangular matrix, and the strictly upper 
//*>          triangular part of A is not referenced.  If DIAG = 'U', the 
//*>          diagonal elements of A are also not referenced and are 
//*>          assumed to be 1. 
//*> 
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
//*> \ingroup complexOTHERcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _dks8nulj(FString _9wyre9zc, FString _2scffxp3, ref Int32 _dxpq0xkr, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, ref Int32 _gro5yvfo)
	{
#region variable declarations
fcomplex _kxg5drh2 =   new fcomplex(1f,0f);
Boolean _rcjmgxm4 =  default;
Boolean _l08igmvf =  default;
Int32 _znpjgsef =  default;
fcomplex _h43lgmvz =  default;
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
			
			_ut9qalzx("CTRTI2" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		
		if (_l08igmvf)
		{
			//* 
			//*        Compute inverse of upper triangular matrix. 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn1743 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1743 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1743;
				for (__81fgg2count1743 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1743 + __81fgg2step1743) / __81fgg2step1743)), _znpjgsef = __81fgg2dlsvn1743; __81fgg2count1743 != 0; __81fgg2count1743--, _znpjgsef += (__81fgg2step1743)) {

				{
					
					if (_rcjmgxm4)
					{
						
						*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = (_kxg5drh2 / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
						_h43lgmvz = (-(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
					}
					else
					{
						
						_h43lgmvz = (-(_kxg5drh2));
					}
					//* 
					//*           Compute elements 1:j-1 of j-th column. 
					//* 
					
					_09cah3zx("Upper" ,"No transpose" ,_2scffxp3 ,ref Unsafe.AsRef(_znpjgsef - (int)1) ,_vxfgpup9 ,ref _ocv8fk5c ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
					_00l5hgpk(ref Unsafe.AsRef(_znpjgsef - (int)1) ,ref _h43lgmvz ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
Mark10:;
					// continue
				}
								}			}
		}
		else
		{
			//* 
			//*        Compute inverse of lower triangular matrix. 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn1744 = (System.Int32)(_dxpq0xkr);
				System.Int32 __81fgg2step1744 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count1744;
				for (__81fgg2count1744 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1744 + __81fgg2step1744) / __81fgg2step1744)), _znpjgsef = __81fgg2dlsvn1744; __81fgg2count1744 != 0; __81fgg2count1744--, _znpjgsef += (__81fgg2step1744)) {

				{
					
					if (_rcjmgxm4)
					{
						
						*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = (_kxg5drh2 / *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)));
						_h43lgmvz = (-(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
					}
					else
					{
						
						_h43lgmvz = (-(_kxg5drh2));
					}
					
					if (_znpjgsef < _dxpq0xkr)
					{
						//* 
						//*              Compute elements j+1:n of j-th column. 
						//* 
						
						_09cah3zx("Lower" ,"No transpose" ,_2scffxp3 ,ref Unsafe.AsRef(_dxpq0xkr - _znpjgsef) ,(_vxfgpup9+(_znpjgsef + (int)1 - 1) + (_znpjgsef + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_znpjgsef + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
						_00l5hgpk(ref Unsafe.AsRef(_dxpq0xkr - _znpjgsef) ,ref _h43lgmvz ,(_vxfgpup9+(_znpjgsef + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
					}
					
Mark20:;
					// continue
				}
								}			}
		}
		//* 
		
		return;//* 
		//*     End of CTRTI2 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
