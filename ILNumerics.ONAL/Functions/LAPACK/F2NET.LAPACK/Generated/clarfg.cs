
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
//*> \brief \b CLARFG generates an elementary reflector (Householder matrix). 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CLARFG + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/clarfg.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/clarfg.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/clarfg.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CLARFG( N, ALPHA, X, INCX, TAU ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INCX, N 
//*       COMPLEX            ALPHA, TAU 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX            X( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CLARFG generates a complex elementary reflector H of order n, such 
//*> that 
//*> 
//*>       H**H * ( alpha ) = ( beta ),   H**H * H = I. 
//*>              (   x   )   (   0  ) 
//*> 
//*> where alpha and beta are scalars, with beta real, and x is an 
//*> (n-1)-element complex vector. H is represented in the form 
//*> 
//*>       H = I - tau * ( 1 ) * ( 1 v**H ) , 
//*>                     ( v ) 
//*> 
//*> where tau is a complex scalar and v is a complex (n-1)-element 
//*> vector. Note that H is not hermitian. 
//*> 
//*> If the elements of x are all zero and alpha is real, then tau = 0 
//*> and H is taken to be the unit matrix. 
//*> 
//*> Otherwise  1 <= real(tau) <= 2  and  abs(tau-1) <= 1 . 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the elementary reflector. 
//*> \endverbatim 
//*> 
//*> \param[in,out] ALPHA 
//*> \verbatim 
//*>          ALPHA is COMPLEX 
//*>          On entry, the value alpha. 
//*>          On exit, it is overwritten with the value beta. 
//*> \endverbatim 
//*> 
//*> \param[in,out] X 
//*> \verbatim 
//*>          X is COMPLEX array, dimension 
//*>                         (1+(N-2)*abs(INCX)) 
//*>          On entry, the vector x. 
//*>          On exit, it is overwritten with the vector v. 
//*> \endverbatim 
//*> 
//*> \param[in] INCX 
//*> \verbatim 
//*>          INCX is INTEGER 
//*>          The increment between elements of X. INCX > 0. 
//*> \endverbatim 
//*> 
//*> \param[out] TAU 
//*> \verbatim 
//*>          TAU is COMPLEX 
//*>          The value tau. 
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
//*> \date November 2017 
//* 
//*> \ingroup complexOTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _ocp87dc1(ref Int32 _dxpq0xkr, ref fcomplex _r7cfteg3, fcomplex* _ta7zuy9k, ref Int32 _1eqjusqc, ref fcomplex _0446f4de)
	{
#region variable declarations
Single _kxg5drh2 =  1f;
Single _d0547bi2 =  0f;
Int32 _znpjgsef =  default;
Int32 _hcqqzxmr =  default;
Single _y2wf3yz6 =  default;
Single _dd1wacp1 =  default;
Single _bafcbx97 =  default;
Single _32rqm46k =  default;
Single _h75qnr7l =  default;
Single _ziu6urj2 =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.8.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     November 2017 
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
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		if (_dxpq0xkr <= (int)0)
		{
			
			_0446f4de = CMPLX(_d0547bi2);
			return;
		}
		//* 
		
		_ziu6urj2 = _igbqnt3f(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,_ta7zuy9k ,ref _1eqjusqc );
		_dd1wacp1 = ILNumerics.F2NET.Intrinsics.REAL(_r7cfteg3 );
		_y2wf3yz6 = ILNumerics.F2NET.Intrinsics.AIMAG(_r7cfteg3 );//* 
		
		if ((_ziu6urj2 == _d0547bi2) & (_y2wf3yz6 == _d0547bi2))
		{
			//* 
			//*        H  =  I 
			//* 
			
			_0446f4de = CMPLX(_d0547bi2);
		}
		else
		{
			//* 
			//*        general case 
			//* 
			
			_bafcbx97 = (-(ILNumerics.F2NET.Intrinsics.SIGN(_wyozskqa(ref _dd1wacp1 ,ref _y2wf3yz6 ,ref _ziu6urj2 ) ,_dd1wacp1 )));
			_h75qnr7l = (_d5tu038y("S" ) / _d5tu038y("E" ));
			_32rqm46k = (_kxg5drh2 / _h75qnr7l);//* 
			
			_hcqqzxmr = (int)0;
			if (ILNumerics.F2NET.Intrinsics.ABS(_bafcbx97 ) < _h75qnr7l)
			{
				//* 
				//*           XNORM, BETA may be inaccurate; scale X and recompute them 
				//* 
				
Mark10:;
				// continue
				_hcqqzxmr = (_hcqqzxmr + (int)1);
				_2ylagitj(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref _32rqm46k ,_ta7zuy9k ,ref _1eqjusqc );
				_bafcbx97 = (_bafcbx97 * _32rqm46k);
				_y2wf3yz6 = (_y2wf3yz6 * _32rqm46k);
				_dd1wacp1 = (_dd1wacp1 * _32rqm46k);
				if ((ILNumerics.F2NET.Intrinsics.ABS(_bafcbx97 ) < _h75qnr7l) & (_hcqqzxmr < (int)20))goto Mark10;//* 
				//*           New BETA is at most 1, at least SAFMIN 
				//* 
				
				_ziu6urj2 = _igbqnt3f(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,_ta7zuy9k ,ref _1eqjusqc );
				_r7cfteg3 = ILNumerics.F2NET.Intrinsics.CMPLX(_dd1wacp1 ,_y2wf3yz6 );
				_bafcbx97 = (-(ILNumerics.F2NET.Intrinsics.SIGN(_wyozskqa(ref _dd1wacp1 ,ref _y2wf3yz6 ,ref _ziu6urj2 ) ,_dd1wacp1 )));
			}
			
			_0446f4de = ILNumerics.F2NET.Intrinsics.CMPLX((_bafcbx97 - _dd1wacp1) / _bafcbx97 ,-((_y2wf3yz6 / _bafcbx97)) );
			_r7cfteg3 = _r6l3poxb(ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.CMPLX(_kxg5drh2 )) ,ref Unsafe.AsRef(_r7cfteg3 - _bafcbx97) );
			_00l5hgpk(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref _r7cfteg3 ,_ta7zuy9k ,ref _1eqjusqc );//* 
			//*        If ALPHA is subnormal, it may lose relative accuracy 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn38 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step38 = (System.Int32)((int)1);
				System.Int32 __81fgg2count38;
				for (__81fgg2count38 = System.Math.Max(0, (System.Int32)(((System.Int32)(_hcqqzxmr) - __81fgg2dlsvn38 + __81fgg2step38) / __81fgg2step38)), _znpjgsef = __81fgg2dlsvn38; __81fgg2count38 != 0; __81fgg2count38--, _znpjgsef += (__81fgg2step38)) {

				{
					
					_bafcbx97 = (_bafcbx97 * _h75qnr7l);
Mark20:;
					// continue
				}
								}			}
			_r7cfteg3 = CMPLX(_bafcbx97);
		}
		//* 
		
		return;//* 
		//*     End of CLARFG 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
