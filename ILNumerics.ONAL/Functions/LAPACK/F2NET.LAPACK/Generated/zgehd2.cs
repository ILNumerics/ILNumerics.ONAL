
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
//*> \brief \b ZGEHD2 reduces a general square matrix to upper Hessenberg form using an unblocked algorithm. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZGEHD2 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zgehd2.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zgehd2.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zgehd2.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZGEHD2( N, ILO, IHI, A, LDA, TAU, WORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            IHI, ILO, INFO, LDA, N 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX*16         A( LDA, * ), TAU( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZGEHD2 reduces a complex general matrix A to upper Hessenberg form H 
//*> by a unitary similarity transformation:  Q**H * A * Q = H . 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix A.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] ILO 
//*> \verbatim 
//*>          ILO is INTEGER 
//*> \endverbatim 
//*> 
//*> \param[in] IHI 
//*> \verbatim 
//*>          IHI is INTEGER 
//*> 
//*>          It is assumed that A is already upper triangular in rows 
//*>          and columns 1:ILO-1 and IHI+1:N. ILO and IHI are normally 
//*>          set by a previous call to ZGEBAL; otherwise they should be 
//*>          set to 1 and N respectively. See Further Details. 
//*>          1 <= ILO <= IHI <= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension (LDA,N) 
//*>          On entry, the n by n general matrix to be reduced. 
//*>          On exit, the upper triangle and the first subdiagonal of A 
//*>          are overwritten with the upper Hessenberg matrix H, and the 
//*>          elements below the first subdiagonal, with the array TAU, 
//*>          represent the unitary matrix Q as a product of elementary 
//*>          reflectors. See Further Details. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[out] TAU 
//*> \verbatim 
//*>          TAU is COMPLEX*16 array, dimension (N-1) 
//*>          The scalar factors of the elementary reflectors (see Further 
//*>          Details). 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is COMPLEX*16 array, dimension (N) 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit 
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
//*> \ingroup complex16GEcomputational 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  The matrix Q is represented as a product of (ihi-ilo) elementary 
//*>  reflectors 
//*> 
//*>     Q = H(ilo) H(ilo+1) . . . H(ihi-1). 
//*> 
//*>  Each H(i) has the form 
//*> 
//*>     H(i) = I - tau * v * v**H 
//*> 
//*>  where tau is a complex scalar, and v is a complex vector with 
//*>  v(1:i) = 0, v(i+1) = 1 and v(ihi+1:n) = 0; v(i+2:ihi) is stored on 
//*>  exit in A(i+2:ihi,i), and tau in TAU(i). 
//*> 
//*>  The contents of A are illustrated by the following example, with 
//*>  n = 7, ilo = 2 and ihi = 6: 
//*> 
//*>  on entry,                        on exit, 
//*> 
//*>  ( a   a   a   a   a   a   a )    (  a   a   h   h   h   h   a ) 
//*>  (     a   a   a   a   a   a )    (      a   h   h   h   h   a ) 
//*>  (     a   a   a   a   a   a )    (      h   h   h   h   h   h ) 
//*>  (     a   a   a   a   a   a )    (      v2  h   h   h   h   h ) 
//*>  (     a   a   a   a   a   a )    (      v2  v3  h   h   h   h ) 
//*>  (     a   a   a   a   a   a )    (      v2  v3  v4  h   h   h ) 
//*>  (                         a )    (                          a ) 
//*> 
//*>  where a denotes an element of the original matrix A, h denotes a 
//*>  modified element of the upper Hessenberg matrix H, and vi denotes an 
//*>  element of the vector defining H(i). 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _687k4r9b(ref Int32 _dxpq0xkr, ref Int32 _pew3blan, ref Int32 _9c1csucx, complex* _vxfgpup9, ref Int32 _ocv8fk5c, complex* _0446f4de, complex* _apig8meb, ref Int32 _gro5yvfo)
	{
#region variable declarations
complex _kxg5drh2 =   new fcomplex(1f,0f);
Int32 _b5p6od9s =  default;
complex _r7cfteg3 =  default;
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
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input parameters 
		//* 
		
		_gro5yvfo = (int)0;
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if ((_pew3blan < (int)1) | (_pew3blan > ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr )))
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if ((_9c1csucx < ILNumerics.F2NET.Intrinsics.MIN(_pew3blan ,_dxpq0xkr )) | (_9c1csucx > _dxpq0xkr))
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
			
			_ut9qalzx("ZGEHD2" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		
		{
			System.Int32 __81fgg2dlsvn2674 = (System.Int32)(_pew3blan);
			const System.Int32 __81fgg2step2674 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2674;
			for (__81fgg2count2674 = System.Math.Max(0, (System.Int32)(((System.Int32)(_9c1csucx - (int)1) - __81fgg2dlsvn2674 + __81fgg2step2674) / __81fgg2step2674)), _b5p6od9s = __81fgg2dlsvn2674; __81fgg2count2674 != 0; __81fgg2count2674--, _b5p6od9s += (__81fgg2step2674)) {

			{
				//* 
				//*        Compute elementary reflector H(i) to annihilate A(i+2:ihi,i) 
				//* 
				
				_r7cfteg3 = *(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c));
				_4btmjfem(ref Unsafe.AsRef(_9c1csucx - _b5p6od9s) ,ref _r7cfteg3 ,(_vxfgpup9+(ILNumerics.F2NET.Intrinsics.MIN(_b5p6od9s + (int)2 ,_dxpq0xkr ) - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_0446f4de+(_b5p6od9s - 1))) );
				*(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;//* 
				//*        Apply H(i) to A(1:ihi,i+1:ihi) from the right 
				//* 
				
				_h7ckdrdn("Right" ,ref _9c1csucx ,ref Unsafe.AsRef(_9c1csucx - _b5p6od9s) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_0446f4de+(_b5p6od9s - 1))) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_apig8meb );//* 
				//*        Apply H(i)**H to A(i+1:ihi,i+1:n) from the left 
				//* 
				
				_h7ckdrdn("Left" ,ref Unsafe.AsRef(_9c1csucx - _b5p6od9s) ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.DCONJG(*(_0446f4de+(_b5p6od9s - 1)) )) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_apig8meb );//* 
				
				*(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _r7cfteg3;
Mark10:;
				// continue
			}
						}		}//* 
		
		return;//* 
		//*     End of ZGEHD2 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
