
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
//*> \brief \b SLATRZ factors an upper trapezoidal matrix by means of orthogonal transformations. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLATRZ + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slatrz.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slatrz.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slatrz.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLATRZ( M, N, L, A, LDA, TAU, WORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            L, LDA, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               A( LDA, * ), TAU( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLATRZ factors the M-by-(M+L) real upper trapezoidal matrix 
//*> [ A1 A2 ] = [ A(1:M,1:M) A(1:M,N-L+1:N) ] as ( R  0 ) * Z, by means 
//*> of orthogonal transformations.  Z is an (M+L)-by-(M+L) orthogonal 
//*> matrix and, R and A1 are M-by-M upper triangular matrices. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of rows of the matrix A.  M >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of columns of the matrix A.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] L 
//*> \verbatim 
//*>          L is INTEGER 
//*>          The number of columns of the matrix A containing the 
//*>          meaningful part of the Householder vectors. N-M >= L >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is REAL array, dimension (LDA,N) 
//*>          On entry, the leading M-by-N upper trapezoidal part of the 
//*>          array A must contain the matrix to be factorized. 
//*>          On exit, the leading M-by-M upper triangular part of A 
//*>          contains the upper triangular matrix R, and elements N-L+1 to 
//*>          N of the first M rows of A, with the array TAU, represent the 
//*>          orthogonal matrix Z as a product of M elementary reflectors. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,M). 
//*> \endverbatim 
//*> 
//*> \param[out] TAU 
//*> \verbatim 
//*>          TAU is REAL array, dimension (M) 
//*>          The scalar factors of the elementary reflectors. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is REAL array, dimension (M) 
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
//*> \ingroup realOTHERcomputational 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>    A. Petitet, Computer Science Dept., Univ. of Tenn., Knoxville, USA 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  The factorization is obtained by Householder's method.  The kth 
//*>  transformation matrix, Z( k ), which is used to introduce zeros into 
//*>  the ( m - k + 1 )th row of A, is given in the form 
//*> 
//*>     Z( k ) = ( I     0   ), 
//*>              ( 0  T( k ) ) 
//*> 
//*>  where 
//*> 
//*>     T( k ) = I - tau*u( k )*u( k )**T,   u( k ) = (   1    ), 
//*>                                                 (   0    ) 
//*>                                                 ( z( k ) ) 
//*> 
//*>  tau is a scalar and z( k ) is an l element vector. tau and z( k ) 
//*>  are chosen to annihilate the elements of the kth row of A2. 
//*> 
//*>  The scalar tau is returned in the kth element of TAU and the vector 
//*>  u( k ) in the kth row of A2, such that the elements of z( k ) are 
//*>  in  a( k, l + 1 ), ..., a( k, n ). The elements of R are returned in 
//*>  the upper triangular part of A1. 
//*> 
//*>  Z is given by 
//*> 
//*>     Z =  Z( 1 ) * Z( 2 ) * ... * Z( m ). 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _92cyg8om(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _68ec3gbh, Single* _vxfgpup9, ref Int32 _ocv8fk5c, Single* _0446f4de, Single* _apig8meb)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Int32 _b5p6od9s =  default;
string fLanavab = default;
#endregion  variable declarations

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
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input arguments 
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_ev4xhht5 == (int)0)
		{
			
			return;
		}
		else
		if (_ev4xhht5 == _dxpq0xkr)
		{
			
			{
				System.Int32 __81fgg2dlsvn2114 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2114 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2114;
				for (__81fgg2count2114 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2114 + __81fgg2step2114) / __81fgg2step2114)), _b5p6od9s = __81fgg2dlsvn2114; __81fgg2count2114 != 0; __81fgg2count2114--, _b5p6od9s += (__81fgg2step2114)) {

				{
					
					*(_0446f4de+(_b5p6od9s - 1)) = _d0547bi2;
Mark10:;
					// continue
				}
								}			}
			return;
		}
		//* 
		
		{
			System.Int32 __81fgg2dlsvn2115 = (System.Int32)(_ev4xhht5);
			System.Int32 __81fgg2step2115 = (System.Int32)((int)-1);
			System.Int32 __81fgg2count2115;
			for (__81fgg2count2115 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn2115 + __81fgg2step2115) / __81fgg2step2115)), _b5p6od9s = __81fgg2dlsvn2115; __81fgg2count2115 != 0; __81fgg2count2115--, _b5p6od9s += (__81fgg2step2115)) {

			{
				//* 
				//*        Generate elementary reflector H(i) to annihilate 
				//*        [ A(i,i) A(i,n-l+1:n) ] 
				//* 
				
				_mbabw0s0(ref Unsafe.AsRef(_68ec3gbh + (int)1) ,ref Unsafe.AsRef(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))) ,(_vxfgpup9+(_b5p6od9s - 1) + ((_dxpq0xkr - _68ec3gbh) + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(*(_0446f4de+(_b5p6od9s - 1))) );//* 
				//*        Apply H(i) to A(1:i-1,i:n) from the right 
				//* 
				
				_65mpo9wk("Right" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef((_dxpq0xkr - _b5p6od9s) + (int)1) ,ref _68ec3gbh ,(_vxfgpup9+(_b5p6od9s - 1) + ((_dxpq0xkr - _68ec3gbh) + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(*(_0446f4de+(_b5p6od9s - 1))) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_apig8meb );//* 
				
Mark20:;
				// continue
			}
						}		}//* 
		
		return;//* 
		//*     End of SLATRZ 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
