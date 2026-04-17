
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
//*> \brief \b DLARFG generates an elementary reflector (Householder matrix). 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLARFG + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlarfg.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlarfg.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlarfg.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLARFG( N, ALPHA, X, INCX, TAU ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INCX, N 
//*       DOUBLE PRECISION   ALPHA, TAU 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   X( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLARFG generates a real elementary reflector H of order n, such 
//*> that 
//*> 
//*>       H * ( alpha ) = ( beta ),   H**T * H = I. 
//*>           (   x   )   (   0  ) 
//*> 
//*> where alpha and beta are scalars, and x is an (n-1)-element real 
//*> vector. H is represented in the form 
//*> 
//*>       H = I - tau * ( 1 ) * ( 1 v**T ) , 
//*>                     ( v ) 
//*> 
//*> where tau is a real scalar and v is a real (n-1)-element 
//*> vector. 
//*> 
//*> If the elements of x are all zero, then tau = 0 and H is taken to be 
//*> the unit matrix. 
//*> 
//*> Otherwise  1 <= tau <= 2. 
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
//*>          ALPHA is DOUBLE PRECISION 
//*>          On entry, the value alpha. 
//*>          On exit, it is overwritten with the value beta. 
//*> \endverbatim 
//*> 
//*> \param[in,out] X 
//*> \verbatim 
//*>          X is DOUBLE PRECISION array, dimension 
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
//*>          TAU is DOUBLE PRECISION 
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
//*> \ingroup doubleOTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _a51k3mk0(ref Int32 _dxpq0xkr, ref Double _r7cfteg3, Double* _ta7zuy9k, ref Int32 _1eqjusqc, ref Double _0446f4de)
	{
#region variable declarations
Double _kxg5drh2 =  1d;
Double _d0547bi2 =  0d;
Int32 _znpjgsef =  default;
Int32 _hcqqzxmr =  default;
Double _bafcbx97 =  default;
Double _32rqm46k =  default;
Double _h75qnr7l =  default;
Double _ziu6urj2 =  default;
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
		
		if (_dxpq0xkr <= (int)1)
		{
			
			_0446f4de = _d0547bi2;
			return;
		}
		//* 
		
		_ziu6urj2 = _gmlreqin(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,_ta7zuy9k ,ref _1eqjusqc );//* 
		
		if (_ziu6urj2 == _d0547bi2)
		{
			//* 
			//*        H  =  I 
			//* 
			
			_0446f4de = _d0547bi2;
		}
		else
		{
			//* 
			//*        general case 
			//* 
			
			_bafcbx97 = (-(ILNumerics.F2NET.Intrinsics.SIGN(_1uc27645(ref _r7cfteg3 ,ref _ziu6urj2 ) ,_r7cfteg3 )));
			_h75qnr7l = (_f43eg0w0("S" ) / _f43eg0w0("E" ));
			_hcqqzxmr = (int)0;
			if (ILNumerics.F2NET.Intrinsics.ABS(_bafcbx97 ) < _h75qnr7l)
			{
				//* 
				//*           XNORM, BETA may be inaccurate; scale X and recompute them 
				//* 
				
				_32rqm46k = (_kxg5drh2 / _h75qnr7l);
Mark10:;
				// continue
				_hcqqzxmr = (_hcqqzxmr + (int)1);
				_f6jqcjk1(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref _32rqm46k ,_ta7zuy9k ,ref _1eqjusqc );
				_bafcbx97 = (_bafcbx97 * _32rqm46k);
				_r7cfteg3 = (_r7cfteg3 * _32rqm46k);
				if ((ILNumerics.F2NET.Intrinsics.ABS(_bafcbx97 ) < _h75qnr7l) & (_hcqqzxmr < (int)20))goto Mark10;//* 
				//*           New BETA is at most 1, at least SAFMIN 
				//* 
				
				_ziu6urj2 = _gmlreqin(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,_ta7zuy9k ,ref _1eqjusqc );
				_bafcbx97 = (-(ILNumerics.F2NET.Intrinsics.SIGN(_1uc27645(ref _r7cfteg3 ,ref _ziu6urj2 ) ,_r7cfteg3 )));
			}
			
			_0446f4de = ((_bafcbx97 - _r7cfteg3) / _bafcbx97);
			_f6jqcjk1(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_kxg5drh2 / (_r7cfteg3 - _bafcbx97)) ,_ta7zuy9k ,ref _1eqjusqc );//* 
			//*        If ALPHA is subnormal, it may lose relative accuracy 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn411 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step411 = (System.Int32)((int)1);
				System.Int32 __81fgg2count411;
				for (__81fgg2count411 = System.Math.Max(0, (System.Int32)(((System.Int32)(_hcqqzxmr) - __81fgg2dlsvn411 + __81fgg2step411) / __81fgg2step411)), _znpjgsef = __81fgg2dlsvn411; __81fgg2count411 != 0; __81fgg2count411--, _znpjgsef += (__81fgg2step411)) {

				{
					
					_bafcbx97 = (_bafcbx97 * _h75qnr7l);
Mark20:;
					// continue
				}
								}			}
			_r7cfteg3 = _bafcbx97;
		}
		//* 
		
		return;//* 
		//*     End of DLARFG 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
