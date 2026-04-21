// MIT License
// 
// Copyright (c) 2026 ILNumerics GmbH
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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
//*> \brief \b ZLARFG generates an elementary reflector (Householder matrix). 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZLARFG + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zlarfg.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zlarfg.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zlarfg.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZLARFG( N, ALPHA, X, INCX, TAU ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INCX, N 
//*       COMPLEX*16         ALPHA, TAU 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX*16         X( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZLARFG generates a complex elementary reflector H of order n, such 
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
//*>          ALPHA is COMPLEX*16 
//*>          On entry, the value alpha. 
//*>          On exit, it is overwritten with the value beta. 
//*> \endverbatim 
//*> 
//*> \param[in,out] X 
//*> \verbatim 
//*>          X is COMPLEX*16 array, dimension 
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
//*>          TAU is COMPLEX*16 
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
//*> \ingroup complex16OTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _4btmjfem(ref Int32 _dxpq0xkr, ref complex _r7cfteg3, complex* _ta7zuy9k, ref Int32 _1eqjusqc, ref complex _0446f4de)
	{
#region variable declarations
Double _kxg5drh2 =  1d;
Double _d0547bi2 =  0d;
Int32 _znpjgsef =  default;
Int32 _hcqqzxmr =  default;
Double _y2wf3yz6 =  default;
Double _dd1wacp1 =  default;
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
		
		if (_dxpq0xkr <= (int)0)
		{
			
			_0446f4de = DCMPLX(_d0547bi2);
			return;
		}
		//* 
		
		_ziu6urj2 = _yzrhzz6l(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,_ta7zuy9k ,ref _1eqjusqc );
		_dd1wacp1 = ILNumerics.F2NET.Intrinsics.DBLE(_r7cfteg3 );
		_y2wf3yz6 = ILNumerics.F2NET.Intrinsics.DIMAG(_r7cfteg3 );//* 
		
		if ((_ziu6urj2 == _d0547bi2) & (_y2wf3yz6 == _d0547bi2))
		{
			//* 
			//*        H  =  I 
			//* 
			
			_0446f4de = DCMPLX(_d0547bi2);
		}
		else
		{
			//* 
			//*        general case 
			//* 
			
			_bafcbx97 = (-(ILNumerics.F2NET.Intrinsics.SIGN(_b8sey8kb(ref _dd1wacp1 ,ref _y2wf3yz6 ,ref _ziu6urj2 ) ,_dd1wacp1 )));
			_h75qnr7l = (_f43eg0w0("S" ) / _f43eg0w0("E" ));
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
				_z5tkm94d(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref _32rqm46k ,_ta7zuy9k ,ref _1eqjusqc );
				_bafcbx97 = (_bafcbx97 * _32rqm46k);
				_y2wf3yz6 = (_y2wf3yz6 * _32rqm46k);
				_dd1wacp1 = (_dd1wacp1 * _32rqm46k);
				if ((ILNumerics.F2NET.Intrinsics.ABS(_bafcbx97 ) < _h75qnr7l) & (_hcqqzxmr < (int)20))goto Mark10;//* 
				//*           New BETA is at most 1, at least SAFMIN 
				//* 
				
				_ziu6urj2 = _yzrhzz6l(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,_ta7zuy9k ,ref _1eqjusqc );
				_r7cfteg3 = ILNumerics.F2NET.Intrinsics.DCMPLX(_dd1wacp1 ,_y2wf3yz6 );
				_bafcbx97 = (-(ILNumerics.F2NET.Intrinsics.SIGN(_b8sey8kb(ref _dd1wacp1 ,ref _y2wf3yz6 ,ref _ziu6urj2 ) ,_dd1wacp1 )));
			}
			
			_0446f4de = ILNumerics.F2NET.Intrinsics.DCMPLX((_bafcbx97 - _dd1wacp1) / _bafcbx97 ,-((_y2wf3yz6 / _bafcbx97)) );
			_r7cfteg3 = _530gk4dk(ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.DCMPLX(_kxg5drh2 )) ,ref Unsafe.AsRef(_r7cfteg3 - _bafcbx97) );
			_wv0on4xy(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref _r7cfteg3 ,_ta7zuy9k ,ref _1eqjusqc );//* 
			//*        If ALPHA is subnormal, it may lose relative accuracy 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn1163 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1163 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1163;
				for (__81fgg2count1163 = System.Math.Max(0, (System.Int32)(((System.Int32)(_hcqqzxmr) - __81fgg2dlsvn1163 + __81fgg2step1163) / __81fgg2step1163)), _znpjgsef = __81fgg2dlsvn1163; __81fgg2count1163 != 0; __81fgg2count1163--, _znpjgsef += (__81fgg2step1163)) {

				{
					
					_bafcbx97 = (_bafcbx97 * _h75qnr7l);
Mark20:;
					// continue
				}
								}			}
			_r7cfteg3 = DCMPLX(_bafcbx97);
		}
		//* 
		
		return;//* 
		//*     End of ZLARFG 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
