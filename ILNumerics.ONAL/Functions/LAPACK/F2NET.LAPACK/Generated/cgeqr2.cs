
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
//*> \brief \b CGEQR2 computes the QR factorization of a general rectangular matrix using an unblocked algorithm. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CGEQR2 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/cgeqr2.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/cgeqr2.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/cgeqr2.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CGEQR2( M, N, A, LDA, TAU, WORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, LDA, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX            A( LDA, * ), TAU( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CGEQR2 computes a QR factorization of a complex m-by-n matrix A: 
//*> 
//*>    A = Q * ( R ), 
//*>            ( 0 ) 
//*> 
//*> where: 
//*> 
//*>    Q is a m-by-m orthogonal matrix; 
//*>    R is an upper-triangular n-by-n matrix; 
//*>    0 is a (m-n)-by-n zero matrix, if m > n. 
//*> 
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
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is COMPLEX array, dimension (LDA,N) 
//*>          On entry, the m by n matrix A. 
//*>          On exit, the elements on and above the diagonal of the array 
//*>          contain the min(m,n) by n upper trapezoidal matrix R (R is 
//*>          upper triangular if m >= n); the elements below the diagonal, 
//*>          with the array TAU, represent the unitary matrix Q as a 
//*>          product of elementary reflectors (see Further Details). 
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
//*>          TAU is COMPLEX array, dimension (min(M,N)) 
//*>          The scalar factors of the elementary reflectors (see Further 
//*>          Details). 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is COMPLEX array, dimension (N) 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0: successful exit 
//*>          < 0: if INFO = -i, the i-th argument had an illegal value 
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
//*> \date November 2019 
//* 
//*> \ingroup complexGEcomputational 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  The matrix Q is represented as a product of elementary reflectors 
//*> 
//*>     Q = H(1) H(2) . . . H(k), where k = min(m,n). 
//*> 
//*>  Each H(i) has the form 
//*> 
//*>     H(i) = I - tau * v * v**H 
//*> 
//*>  where tau is a complex scalar, and v is a complex vector with 
//*>  v(1:i-1) = 0 and v(i) = 1; v(i+1:m) is stored on exit in A(i+1:m,i), 
//*>  and tau in TAU(i). 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _zoti9crw(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, fcomplex* _0446f4de, fcomplex* _apig8meb, ref Int32 _gro5yvfo)
	{
#region variable declarations
fcomplex _kxg5drh2 =   new fcomplex(1f,0f);
Int32 _b5p6od9s =  default;
Int32 _umlkckdg =  default;
fcomplex _r7cfteg3 =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK computational routine (version 3.9.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     November 2019 
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
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input arguments 
		//* 
		
		_gro5yvfo = (int)0;
		if (_ev4xhht5 < (int)0)
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ))
		{
			
			_gro5yvfo = (int)-4;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("CGEQR2" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		
		_umlkckdg = ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_dxpq0xkr );//* 
		
		{
			System.Int32 __81fgg2dlsvn1039 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1039 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1039;
			for (__81fgg2count1039 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1039 + __81fgg2step1039) / __81fgg2step1039)), _b5p6od9s = __81fgg2dlsvn1039; __81fgg2count1039 != 0; __81fgg2count1039--, _b5p6od9s += (__81fgg2step1039)) {

			{
				//* 
				//*        Generate elementary reflector H(i) to annihilate A(i+1:m,i) 
				//* 
				
				_ocp87dc1(ref Unsafe.AsRef((_ev4xhht5 - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))) ,(_vxfgpup9+(ILNumerics.F2NET.Intrinsics.MIN(_b5p6od9s + (int)1 ,_ev4xhht5 ) - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_0446f4de+(_b5p6od9s - 1))) );
				if (_b5p6od9s < _dxpq0xkr)
				{
					//* 
					//*           Apply H(i)**H to A(i:m,i+1:n) from the left 
					//* 
					
					_r7cfteg3 = *(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c));
					*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;
					_ok06eljh("Left" ,ref Unsafe.AsRef((_ev4xhht5 - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.CONJG(*(_0446f4de+(_b5p6od9s - 1)) )) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_apig8meb );
					*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _r7cfteg3;
				}
				
Mark10:;
				// continue
			}
						}		}
		return;//* 
		//*     End of CGEQR2 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
