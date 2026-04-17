
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
//*> \brief \b CGERU 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CGERU(M,N,ALPHA,X,INCX,Y,INCY,A,LDA) 
//* 
//*       .. Scalar Arguments .. 
//*       COMPLEX ALPHA 
//*       INTEGER INCX,INCY,LDA,M,N 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX A(LDA,*),X(*),Y(*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CGERU  performs the rank 1 operation 
//*> 
//*>    A := alpha*x*y**T + A, 
//*> 
//*> where alpha is a scalar, x is an m element vector, y is an n element 
//*> vector and A is an m by n matrix. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>           On entry, M specifies the number of rows of the matrix A. 
//*>           M must be at least zero. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>           On entry, N specifies the number of columns of the matrix A. 
//*>           N must be at least zero. 
//*> \endverbatim 
//*> 
//*> \param[in] ALPHA 
//*> \verbatim 
//*>          ALPHA is COMPLEX 
//*>           On entry, ALPHA specifies the scalar alpha. 
//*> \endverbatim 
//*> 
//*> \param[in] X 
//*> \verbatim 
//*>          X is COMPLEX array, dimension at least 
//*>           ( 1 + ( m - 1 )*abs( INCX ) ). 
//*>           Before entry, the incremented array X must contain the m 
//*>           element vector x. 
//*> \endverbatim 
//*> 
//*> \param[in] INCX 
//*> \verbatim 
//*>          INCX is INTEGER 
//*>           On entry, INCX specifies the increment for the elements of 
//*>           X. INCX must not be zero. 
//*> \endverbatim 
//*> 
//*> \param[in] Y 
//*> \verbatim 
//*>          Y is COMPLEX array, dimension at least 
//*>           ( 1 + ( n - 1 )*abs( INCY ) ). 
//*>           Before entry, the incremented array Y must contain the n 
//*>           element vector y. 
//*> \endverbatim 
//*> 
//*> \param[in] INCY 
//*> \verbatim 
//*>          INCY is INTEGER 
//*>           On entry, INCY specifies the increment for the elements of 
//*>           Y. INCY must not be zero. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is COMPLEX array, dimension ( LDA, N ) 
//*>           Before entry, the leading m by n part of the array A must 
//*>           contain the matrix of coefficients. On exit, A is 
//*>           overwritten by the updated matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>           On entry, LDA specifies the first dimension of A as declared 
//*>           in the calling (sub) program. LDA must be at least 
//*>           max( 1, m ). 
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
//*> \ingroup complex_blas_level2 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  Level 2 Blas routine. 
//*> 
//*>  -- Written on 22-October-1986. 
//*>     Jack Dongarra, Argonne National Lab. 
//*>     Jeremy Du Croz, Nag Central Office. 
//*>     Sven Hammarling, Nag Central Office. 
//*>     Richard Hanson, Sandia National Labs. 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _audfhame(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref fcomplex _r7cfteg3, fcomplex* _ta7zuy9k, ref Int32 _1eqjusqc, fcomplex* _f3z3edv0, ref Int32 _bbrxgmj7, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c)
	{
#region variable declarations
fcomplex _d0547bi2 =   new fcomplex(0f,0f);
fcomplex _1ajfmh55 =  default;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _b69ritwm =  default;
Int32 _znpjgsef =  default;
Int32 _lwc63p7q =  default;
Int32 _ziknm33t =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- Reference BLAS level2 routine (version 3.7.0) -- 
		//*  -- Reference BLAS is a software package provided by Univ. of Tennessee,    -- 
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
		//* 
		//*     Test the input parameters. 
		//* 
		
		_gro5yvfo = (int)0;
		if (_ev4xhht5 < (int)0)
		{
			
			_gro5yvfo = (int)1;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)2;
		}
		else
		if (_1eqjusqc == (int)0)
		{
			
			_gro5yvfo = (int)5;
		}
		else
		if (_bbrxgmj7 == (int)0)
		{
			
			_gro5yvfo = (int)7;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ))
		{
			
			_gro5yvfo = (int)9;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("CGERU " ,ref _gro5yvfo );
			return;
		}
		//* 
		//*     Quick return if possible. 
		//* 
		
		if (((_ev4xhht5 == (int)0) | (_dxpq0xkr == (int)0)) | (_r7cfteg3 == _d0547bi2))
		return;//* 
		//*     Start the operations. In this version the elements of A are 
		//*     accessed sequentially with one pass through A. 
		//* 
		
		if (_bbrxgmj7 > (int)0)
		{
			
			_lwc63p7q = (int)1;
		}
		else
		{
			
			_lwc63p7q = ((int)1 - ((_dxpq0xkr - (int)1) * _bbrxgmj7));
		}
		
		if (_1eqjusqc == (int)1)
		{
			
			{
				System.Int32 __81fgg2dlsvn2139 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2139 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2139;
				for (__81fgg2count2139 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2139 + __81fgg2step2139) / __81fgg2step2139)), _znpjgsef = __81fgg2dlsvn2139; __81fgg2count2139 != 0; __81fgg2count2139--, _znpjgsef += (__81fgg2step2139)) {

				{
					
					if (*(_f3z3edv0+(_lwc63p7q - 1)) != _d0547bi2)
					{
						
						_1ajfmh55 = (_r7cfteg3 * *(_f3z3edv0+(_lwc63p7q - 1)));
						{
							System.Int32 __81fgg2dlsvn2140 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step2140 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2140;
							for (__81fgg2count2140 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn2140 + __81fgg2step2140) / __81fgg2step2140)), _b5p6od9s = __81fgg2dlsvn2140; __81fgg2count2140 != 0; __81fgg2count2140--, _b5p6od9s += (__81fgg2step2140)) {

							{
								
								*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) + (*(_ta7zuy9k+(_b5p6od9s - 1)) * _1ajfmh55));
Mark10:;
								// continue
							}
														}						}
					}
					
					_lwc63p7q = (_lwc63p7q + _bbrxgmj7);
Mark20:;
					// continue
				}
								}			}
		}
		else
		{
			
			if (_1eqjusqc > (int)0)
			{
				
				_ziknm33t = (int)1;
			}
			else
			{
				
				_ziknm33t = ((int)1 - ((_ev4xhht5 - (int)1) * _1eqjusqc));
			}
			
			{
				System.Int32 __81fgg2dlsvn2141 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2141 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2141;
				for (__81fgg2count2141 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2141 + __81fgg2step2141) / __81fgg2step2141)), _znpjgsef = __81fgg2dlsvn2141; __81fgg2count2141 != 0; __81fgg2count2141--, _znpjgsef += (__81fgg2step2141)) {

				{
					
					if (*(_f3z3edv0+(_lwc63p7q - 1)) != _d0547bi2)
					{
						
						_1ajfmh55 = (_r7cfteg3 * *(_f3z3edv0+(_lwc63p7q - 1)));
						_b69ritwm = _ziknm33t;
						{
							System.Int32 __81fgg2dlsvn2142 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step2142 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2142;
							for (__81fgg2count2142 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn2142 + __81fgg2step2142) / __81fgg2step2142)), _b5p6od9s = __81fgg2dlsvn2142; __81fgg2count2142 != 0; __81fgg2count2142--, _b5p6od9s += (__81fgg2step2142)) {

							{
								
								*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) + (*(_ta7zuy9k+(_b69ritwm - 1)) * _1ajfmh55));
								_b69ritwm = (_b69ritwm + _1eqjusqc);
Mark30:;
								// continue
							}
														}						}
					}
					
					_lwc63p7q = (_lwc63p7q + _bbrxgmj7);
Mark40:;
					// continue
				}
								}			}
		}
		//* 
		
		return;//* 
		//*     End of CGERU . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
