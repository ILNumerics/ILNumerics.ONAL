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
//*> \brief \b SLASV2 computes the singular value decomposition of a 2-by-2 triangular matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLASV2 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slasv2.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slasv2.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slasv2.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLASV2( F, G, H, SSMIN, SSMAX, SNR, CSR, SNL, CSL ) 
//* 
//*       .. Scalar Arguments .. 
//*       REAL               CSL, CSR, F, G, H, SNL, SNR, SSMAX, SSMIN 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLASV2 computes the singular value decomposition of a 2-by-2 
//*> triangular matrix 
//*>    [  F   G  ] 
//*>    [  0   H  ]. 
//*> On return, abs(SSMAX) is the larger singular value, abs(SSMIN) is the 
//*> smaller singular value, and (CSL,SNL) and (CSR,SNR) are the left and 
//*> right singular vectors for abs(SSMAX), giving the decomposition 
//*> 
//*>    [ CSL  SNL ] [  F   G  ] [ CSR -SNR ]  =  [ SSMAX   0   ] 
//*>    [-SNL  CSL ] [  0   H  ] [ SNR  CSR ]     [  0    SSMIN ]. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] F 
//*> \verbatim 
//*>          F is REAL 
//*>          The (1,1) element of the 2-by-2 matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] G 
//*> \verbatim 
//*>          G is REAL 
//*>          The (1,2) element of the 2-by-2 matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] H 
//*> \verbatim 
//*>          H is REAL 
//*>          The (2,2) element of the 2-by-2 matrix. 
//*> \endverbatim 
//*> 
//*> \param[out] SSMIN 
//*> \verbatim 
//*>          SSMIN is REAL 
//*>          abs(SSMIN) is the smaller singular value. 
//*> \endverbatim 
//*> 
//*> \param[out] SSMAX 
//*> \verbatim 
//*>          SSMAX is REAL 
//*>          abs(SSMAX) is the larger singular value. 
//*> \endverbatim 
//*> 
//*> \param[out] SNL 
//*> \verbatim 
//*>          SNL is REAL 
//*> \endverbatim 
//*> 
//*> \param[out] CSL 
//*> \verbatim 
//*>          CSL is REAL 
//*>          The vector (CSL, SNL) is a unit left singular vector for the 
//*>          singular value abs(SSMAX). 
//*> \endverbatim 
//*> 
//*> \param[out] SNR 
//*> \verbatim 
//*>          SNR is REAL 
//*> \endverbatim 
//*> 
//*> \param[out] CSR 
//*> \verbatim 
//*>          CSR is REAL 
//*>          The vector (CSR, SNR) is a unit right singular vector for the 
//*>          singular value abs(SSMAX). 
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
//*> \ingroup OTHERauxiliary 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  Any input parameter may be aliased with any output parameter. 
//*> 
//*>  Barring over/underflow and assuming a guard digit in subtraction, all 
//*>  output quantities are correct to within a few units in the last 
//*>  place (ulps). 
//*> 
//*>  In IEEE arithmetic, the code works correctly if one matrix element is 
//*>  infinite. 
//*> 
//*>  Overflow will not occur unless the largest singular value itself 
//*>  overflows or is within a few ulps of overflow. (On machines with 
//*>  partial overflow, like the Cray, overflow may occur if the largest 
//*>  singular value is within a factor of 2 of overflow.) 
//*> 
//*>  Underflow is harmless if underflow is gradual. Otherwise, results 
//*>  may correspond to a matrix modified by perturbations of size near 
//*>  the underflow threshold. 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _eyqnke7t(ref Single _8plnuphw, ref Single _mu73se41, ref Single _ogkjl6gu, ref Single _b7jlbaxv, ref Single _c5hmpmkx, ref Single _jyb9dynk, ref Single _3x3t9ums, ref Single _4xzps2ny, ref Single _7ilu75x3)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Single _gbf4169i =  0.5f;
Single _kxg5drh2 =  1f;
Single _5m0mjfxm =  2f;
Single _ax5ijvbx =  4f;
Boolean _uqxweww5 =  default;
Boolean _ir7y3k4r =  default;
Int32 _40t6j95r =  default;
Single _vxfgpup9 =  default;
Single _lmc4m2m7 =  default;
Single _aa9s7znh =  default;
Single _plfm7z8g =  default;
Single _d5yxxwr2 =  default;
Single _a3ga4y5w =  default;
Single _c0nzba5y =  default;
Single _o4zhl1tv =  default;
Single _nr9hio6a =  default;
Single _vgk2u1tp =  default;
Single _68ec3gbh =  default;
Single _ev4xhht5 =  default;
Single _e9y2lltf =  default;
Single _q2vwp05i =  default;
Single _irk8i6qr =  default;
Single _ml5q24ww =  default;
Single _rpqscfwi =  default;
Single _2ivtt43r =  default;
Single _1ajfmh55 =  default;
Single _rn71pvxj =  default;
Single _j0549clc =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     December 2016 
		//* 
		//*     .. Scalar Arguments .. 
		//*     .. 
		//* 
		//* ===================================================================== 
		//* 
		//*     .. Parameters .. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		_a3ga4y5w = _8plnuphw;
		_d5yxxwr2 = ILNumerics.F2NET.Intrinsics.ABS(_a3ga4y5w );
		_vgk2u1tp = _ogkjl6gu;
		_nr9hio6a = ILNumerics.F2NET.Intrinsics.ABS(_ogkjl6gu );//* 
		//*     PMAX points to the maximum absolute element of matrix 
		//*       PMAX = 1 if F largest in absolute values 
		//*       PMAX = 2 if G largest in absolute values 
		//*       PMAX = 3 if H largest in absolute values 
		//* 
		
		_40t6j95r = (int)1;
		_ir7y3k4r = (_nr9hio6a > _d5yxxwr2);
		if (_ir7y3k4r)
		{
			
			_40t6j95r = (int)3;
			_1ajfmh55 = _a3ga4y5w;
			_a3ga4y5w = _vgk2u1tp;
			_vgk2u1tp = _1ajfmh55;
			_1ajfmh55 = _d5yxxwr2;
			_d5yxxwr2 = _nr9hio6a;
			_nr9hio6a = _1ajfmh55;//* 
			//*        Now FA .ge. HA 
			//* 
			
		}
		
		_o4zhl1tv = _mu73se41;
		_c0nzba5y = ILNumerics.F2NET.Intrinsics.ABS(_o4zhl1tv );
		if (_c0nzba5y == _d0547bi2)
		{
			//* 
			//*        Diagonal matrix 
			//* 
			
			_b7jlbaxv = _nr9hio6a;
			_c5hmpmkx = _d5yxxwr2;
			_lmc4m2m7 = _kxg5drh2;
			_aa9s7znh = _kxg5drh2;
			_ml5q24ww = _d0547bi2;
			_rpqscfwi = _d0547bi2;
		}
		else
		{
			
			_uqxweww5 = true;
			if (_c0nzba5y > _d5yxxwr2)
			{
				
				_40t6j95r = (int)2;
				if ((_d5yxxwr2 / _c0nzba5y) < _d5tu038y("EPS" ))
				{
					//* 
					//*              Case of very large GA 
					//* 
					
					_uqxweww5 = false;
					_c5hmpmkx = _c0nzba5y;
					if (_nr9hio6a > _kxg5drh2)
					{
						
						_b7jlbaxv = (_d5yxxwr2 / (_c0nzba5y / _nr9hio6a));
					}
					else
					{
						
						_b7jlbaxv = ((_d5yxxwr2 / _c0nzba5y) * _nr9hio6a);
					}
					
					_lmc4m2m7 = _kxg5drh2;
					_ml5q24ww = (_vgk2u1tp / _o4zhl1tv);
					_rpqscfwi = _kxg5drh2;
					_aa9s7znh = (_a3ga4y5w / _o4zhl1tv);
				}
				
			}
			
			if (_uqxweww5)
			{
				//* 
				//*           Normal case 
				//* 
				
				_plfm7z8g = (_d5yxxwr2 - _nr9hio6a);
				if (_plfm7z8g == _d5yxxwr2)
				{
					//* 
					//*              Copes with infinite F or H 
					//* 
					
					_68ec3gbh = _kxg5drh2;
				}
				else
				{
					
					_68ec3gbh = (_plfm7z8g / _d5yxxwr2);
				}
				//* 
				//*           Note that 0 .le. L .le. 1 
				//* 
				
				_ev4xhht5 = (_o4zhl1tv / _a3ga4y5w);//* 
				//*           Note that abs(M) .le. 1/macheps 
				//* 
				
				_2ivtt43r = (_5m0mjfxm - _68ec3gbh);//* 
				//*           Note that T .ge. 1 
				//* 
				
				_e9y2lltf = (_ev4xhht5 * _ev4xhht5);
				_j0549clc = (_2ivtt43r * _2ivtt43r);
				_irk8i6qr = ILNumerics.F2NET.Intrinsics.SQRT(_j0549clc + _e9y2lltf );//* 
				//*           Note that 1 .le. S .le. 1 + 1/macheps 
				//* 
				
				if (_68ec3gbh == _d0547bi2)
				{
					
					_q2vwp05i = ILNumerics.F2NET.Intrinsics.ABS(_ev4xhht5 );
				}
				else
				{
					
					_q2vwp05i = ILNumerics.F2NET.Intrinsics.SQRT((_68ec3gbh * _68ec3gbh) + _e9y2lltf );
				}
				//* 
				//*           Note that 0 .le. R .le. 1 + 1/macheps 
				//* 
				
				_vxfgpup9 = (_gbf4169i * (_irk8i6qr + _q2vwp05i));//* 
				//*           Note that 1 .le. A .le. 1 + abs(M) 
				//* 
				
				_b7jlbaxv = (_nr9hio6a / _vxfgpup9);
				_c5hmpmkx = (_d5yxxwr2 * _vxfgpup9);
				if (_e9y2lltf == _d0547bi2)
				{
					//* 
					//*              Note that M is very tiny 
					//* 
					
					if (_68ec3gbh == _d0547bi2)
					{
						
						_2ivtt43r = (ILNumerics.F2NET.Intrinsics.SIGN(_5m0mjfxm ,_a3ga4y5w ) * ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,_o4zhl1tv ));
					}
					else
					{
						
						_2ivtt43r = ((_o4zhl1tv / ILNumerics.F2NET.Intrinsics.SIGN(_plfm7z8g ,_a3ga4y5w )) + (_ev4xhht5 / _2ivtt43r));
					}
					
				}
				else
				{
					
					_2ivtt43r = (((_ev4xhht5 / (_irk8i6qr + _2ivtt43r)) + (_ev4xhht5 / (_q2vwp05i + _68ec3gbh))) * (_kxg5drh2 + _vxfgpup9));
				}
				
				_68ec3gbh = ILNumerics.F2NET.Intrinsics.SQRT((_2ivtt43r * _2ivtt43r) + _ax5ijvbx );
				_aa9s7znh = (_5m0mjfxm / _68ec3gbh);
				_rpqscfwi = (_2ivtt43r / _68ec3gbh);
				_lmc4m2m7 = ((_aa9s7znh + (_rpqscfwi * _ev4xhht5)) / _vxfgpup9);
				_ml5q24ww = (((_vgk2u1tp / _a3ga4y5w) * _rpqscfwi) / _vxfgpup9);
			}
			
		}
		
		if (_ir7y3k4r)
		{
			
			_7ilu75x3 = _rpqscfwi;
			_4xzps2ny = _aa9s7znh;
			_3x3t9ums = _ml5q24ww;
			_jyb9dynk = _lmc4m2m7;
		}
		else
		{
			
			_7ilu75x3 = _lmc4m2m7;
			_4xzps2ny = _ml5q24ww;
			_3x3t9ums = _aa9s7znh;
			_jyb9dynk = _rpqscfwi;
		}
		//* 
		//*     Correct signs of SSMAX and SSMIN 
		//* 
		
		if (_40t6j95r == (int)1)
		_rn71pvxj = ((ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,_3x3t9ums ) * ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,_7ilu75x3 )) * ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,_8plnuphw ));
		if (_40t6j95r == (int)2)
		_rn71pvxj = ((ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,_jyb9dynk ) * ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,_7ilu75x3 )) * ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,_mu73se41 ));
		if (_40t6j95r == (int)3)
		_rn71pvxj = ((ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,_jyb9dynk ) * ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,_4xzps2ny )) * ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,_ogkjl6gu ));
		_c5hmpmkx = ILNumerics.F2NET.Intrinsics.SIGN(_c5hmpmkx ,_rn71pvxj );
		_b7jlbaxv = ILNumerics.F2NET.Intrinsics.SIGN(_b7jlbaxv ,(_rn71pvxj * ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,_8plnuphw )) * ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,_ogkjl6gu ) );
		return;//* 
		//*     End of SLASV2 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
