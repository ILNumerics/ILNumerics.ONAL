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
//*> \brief \b CLAIC1 applies one step of incremental condition estimation. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CLAIC1 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/claic1.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/claic1.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/claic1.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CLAIC1( JOB, J, X, SEST, W, GAMMA, SESTPR, S, C ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            J, JOB 
//*       REAL               SEST, SESTPR 
//*       COMPLEX            C, GAMMA, S 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX            W( J ), X( J ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CLAIC1 applies one step of incremental condition estimation in 
//*> its simplest version: 
//*> 
//*> Let x, twonorm(x) = 1, be an approximate singular vector of an j-by-j 
//*> lower triangular matrix L, such that 
//*>          twonorm(L*x) = sest 
//*> Then CLAIC1 computes sestpr, s, c such that 
//*> the vector 
//*>                 [ s*x ] 
//*>          xhat = [  c  ] 
//*> is an approximate singular vector of 
//*>                 [ L      0  ] 
//*>          Lhat = [ w**H gamma ] 
//*> in the sense that 
//*>          twonorm(Lhat*xhat) = sestpr. 
//*> 
//*> Depending on JOB, an estimate for the largest or smallest singular 
//*> value is computed. 
//*> 
//*> Note that [s c]**H and sestpr**2 is an eigenpair of the system 
//*> 
//*>     diag(sest*sest, 0) + [alpha  gamma] * [ conjg(alpha) ] 
//*>                                           [ conjg(gamma) ] 
//*> 
//*> where  alpha =  x**H*w. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] JOB 
//*> \verbatim 
//*>          JOB is INTEGER 
//*>          = 1: an estimate for the largest singular value is computed. 
//*>          = 2: an estimate for the smallest singular value is computed. 
//*> \endverbatim 
//*> 
//*> \param[in] J 
//*> \verbatim 
//*>          J is INTEGER 
//*>          Length of X and W 
//*> \endverbatim 
//*> 
//*> \param[in] X 
//*> \verbatim 
//*>          X is COMPLEX array, dimension (J) 
//*>          The j-vector x. 
//*> \endverbatim 
//*> 
//*> \param[in] SEST 
//*> \verbatim 
//*>          SEST is REAL 
//*>          Estimated singular value of j by j matrix L 
//*> \endverbatim 
//*> 
//*> \param[in] W 
//*> \verbatim 
//*>          W is COMPLEX array, dimension (J) 
//*>          The j-vector w. 
//*> \endverbatim 
//*> 
//*> \param[in] GAMMA 
//*> \verbatim 
//*>          GAMMA is COMPLEX 
//*>          The diagonal element gamma. 
//*> \endverbatim 
//*> 
//*> \param[out] SESTPR 
//*> \verbatim 
//*>          SESTPR is REAL 
//*>          Estimated singular value of (j+1) by (j+1) matrix Lhat. 
//*> \endverbatim 
//*> 
//*> \param[out] S 
//*> \verbatim 
//*>          S is COMPLEX 
//*>          Sine needed in forming xhat. 
//*> \endverbatim 
//*> 
//*> \param[out] C 
//*> \verbatim 
//*>          C is COMPLEX 
//*>          Cosine needed in forming xhat. 
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
//*> \ingroup complexOTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _q8o4ezsb(ref Int32 _xcrv93xi, ref Int32 _znpjgsef, fcomplex* _ta7zuy9k, ref Single _d7hhk8za, fcomplex* _z1ioc3c8, ref fcomplex _zf88apxo, ref Single _6egl0ako, ref fcomplex _irk8i6qr, ref fcomplex _3crf0qn3)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
Single _5m0mjfxm =  2f;
Single _gbf4169i =  0.5f;
Single _ax5ijvbx =  4f;
Single _7ahwn9vx =  default;
Single _t8f5ar2x =  default;
Single _6ata7ybv =  default;
Single _p9n405a5 =  default;
Single _p1iqarg6 =  default;
Single _1cy9vb4e =  default;
Single _fmb4u5ka =  default;
Single _slkbnmvx =  default;
Single _ofbdxt08 =  default;
Single _2ivtt43r =  default;
Single _lso0qkri =  default;
Single _2qcyvkhx =  default;
Single _8w7bl1gz =  default;
Single _frd823b5 =  default;
fcomplex _r7cfteg3 =  default;
fcomplex _ylx3r42x =  default;
fcomplex _ltxm349p =  default;
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
		//*     .. Array Arguments .. 
		//*     .. 
		//* 
		//*  ===================================================================== 
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
		
		_p1iqarg6 = _d5tu038y("Epsilon" );
		_r7cfteg3 = _f18dve92(ref _znpjgsef ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) ,_z1ioc3c8 ,ref Unsafe.AsRef((int)1) );//* 
		
		_7ahwn9vx = ILNumerics.F2NET.Intrinsics.ABS(_r7cfteg3 );
		_6ata7ybv = ILNumerics.F2NET.Intrinsics.ABS(_zf88apxo );
		_t8f5ar2x = ILNumerics.F2NET.Intrinsics.ABS(_d7hhk8za );//* 
		
		if (_xcrv93xi == (int)1)
		{
			//* 
			//*        Estimating largest singular value 
			//* 
			//*        special cases 
			//* 
			
			if (_d7hhk8za == _d0547bi2)
			{
				
				_fmb4u5ka = ILNumerics.F2NET.Intrinsics.MAX(_6ata7ybv ,_7ahwn9vx );
				if (_fmb4u5ka == _d0547bi2)
				{
					
					_irk8i6qr = CMPLX(_d0547bi2);
					_3crf0qn3 = CMPLX(_kxg5drh2);
					_6egl0ako = _d0547bi2;
				}
				else
				{
					
					_irk8i6qr = (_r7cfteg3 / _fmb4u5ka);
					_3crf0qn3 = (_zf88apxo / _fmb4u5ka);
					_2qcyvkhx = REAL(ILNumerics.F2NET.Intrinsics.SQRT((_irk8i6qr * ILNumerics.F2NET.Intrinsics.CONJG(_irk8i6qr )) + (_3crf0qn3 * ILNumerics.F2NET.Intrinsics.CONJG(_3crf0qn3 )) ));
					_irk8i6qr = (_irk8i6qr / _2qcyvkhx);
					_3crf0qn3 = (_3crf0qn3 / _2qcyvkhx);
					_6egl0ako = (_fmb4u5ka * _2qcyvkhx);
				}
				
				return;
			}
			else
			if (_6ata7ybv <= (_p1iqarg6 * _t8f5ar2x))
			{
				
				_irk8i6qr = CMPLX(_kxg5drh2);
				_3crf0qn3 = CMPLX(_d0547bi2);
				_2qcyvkhx = ILNumerics.F2NET.Intrinsics.MAX(_t8f5ar2x ,_7ahwn9vx );
				_fmb4u5ka = (_t8f5ar2x / _2qcyvkhx);
				_slkbnmvx = (_7ahwn9vx / _2qcyvkhx);
				_6egl0ako = (_2qcyvkhx * ILNumerics.F2NET.Intrinsics.SQRT((_fmb4u5ka * _fmb4u5ka) + (_slkbnmvx * _slkbnmvx) ));
				return;
			}
			else
			if (_7ahwn9vx <= (_p1iqarg6 * _t8f5ar2x))
			{
				
				_fmb4u5ka = _6ata7ybv;
				_slkbnmvx = _t8f5ar2x;
				if (_fmb4u5ka <= _slkbnmvx)
				{
					
					_irk8i6qr = CMPLX(_kxg5drh2);
					_3crf0qn3 = CMPLX(_d0547bi2);
					_6egl0ako = _slkbnmvx;
				}
				else
				{
					
					_irk8i6qr = CMPLX(_d0547bi2);
					_3crf0qn3 = CMPLX(_kxg5drh2);
					_6egl0ako = _fmb4u5ka;
				}
				
				return;
			}
			else
			if ((_t8f5ar2x <= (_p1iqarg6 * _7ahwn9vx)) | (_t8f5ar2x <= (_p1iqarg6 * _6ata7ybv)))
			{
				
				_fmb4u5ka = _6ata7ybv;
				_slkbnmvx = _7ahwn9vx;
				if (_fmb4u5ka <= _slkbnmvx)
				{
					
					_2qcyvkhx = (_fmb4u5ka / _slkbnmvx);
					_ofbdxt08 = ILNumerics.F2NET.Intrinsics.SQRT(_kxg5drh2 + (_2qcyvkhx * _2qcyvkhx) );
					_6egl0ako = (_slkbnmvx * _ofbdxt08);
					_irk8i6qr = ((_r7cfteg3 / _slkbnmvx) / _ofbdxt08);
					_3crf0qn3 = ((_zf88apxo / _slkbnmvx) / _ofbdxt08);
				}
				else
				{
					
					_2qcyvkhx = (_slkbnmvx / _fmb4u5ka);
					_ofbdxt08 = ILNumerics.F2NET.Intrinsics.SQRT(_kxg5drh2 + (_2qcyvkhx * _2qcyvkhx) );
					_6egl0ako = (_fmb4u5ka * _ofbdxt08);
					_irk8i6qr = ((_r7cfteg3 / _fmb4u5ka) / _ofbdxt08);
					_3crf0qn3 = ((_zf88apxo / _fmb4u5ka) / _ofbdxt08);
				}
				
				return;
			}
			else
			{
				//* 
				//*           normal case 
				//* 
				
				_8w7bl1gz = (_7ahwn9vx / _t8f5ar2x);
				_frd823b5 = (_6ata7ybv / _t8f5ar2x);//* 
				
				_p9n405a5 = (((_kxg5drh2 - (_8w7bl1gz * _8w7bl1gz)) - (_frd823b5 * _frd823b5)) * _gbf4169i);
				_3crf0qn3 = CMPLX((_8w7bl1gz * _8w7bl1gz));
				if (_p9n405a5 > _d0547bi2)
				{
					
					_2ivtt43r = REAL((_3crf0qn3 / (_p9n405a5 + ILNumerics.F2NET.Intrinsics.SQRT((_p9n405a5 * _p9n405a5) + _3crf0qn3 ))));
				}
				else
				{
					
					_2ivtt43r = REAL((ILNumerics.F2NET.Intrinsics.SQRT((_p9n405a5 * _p9n405a5) + _3crf0qn3 ) - _p9n405a5));
				}
				//* 
				
				_ltxm349p = (-(((_r7cfteg3 / _t8f5ar2x) / _2ivtt43r)));
				_ylx3r42x = (-(((_zf88apxo / _t8f5ar2x) / (_kxg5drh2 + _2ivtt43r))));
				_2qcyvkhx = REAL(ILNumerics.F2NET.Intrinsics.SQRT((_ltxm349p * ILNumerics.F2NET.Intrinsics.CONJG(_ltxm349p )) + (_ylx3r42x * ILNumerics.F2NET.Intrinsics.CONJG(_ylx3r42x )) ));
				_irk8i6qr = (_ltxm349p / _2qcyvkhx);
				_3crf0qn3 = (_ylx3r42x / _2qcyvkhx);
				_6egl0ako = (ILNumerics.F2NET.Intrinsics.SQRT(_2ivtt43r + _kxg5drh2 ) * _t8f5ar2x);
				return;
			}
			//* 
			
		}
		else
		if (_xcrv93xi == (int)2)
		{
			//* 
			//*        Estimating smallest singular value 
			//* 
			//*        special cases 
			//* 
			
			if (_d7hhk8za == _d0547bi2)
			{
				
				_6egl0ako = _d0547bi2;
				if (ILNumerics.F2NET.Intrinsics.MAX(_6ata7ybv ,_7ahwn9vx ) == _d0547bi2)
				{
					
					_ltxm349p = CMPLX(_kxg5drh2);
					_ylx3r42x = CMPLX(_d0547bi2);
				}
				else
				{
					
					_ltxm349p = (-(ILNumerics.F2NET.Intrinsics.CONJG(_zf88apxo )));
					_ylx3r42x = ILNumerics.F2NET.Intrinsics.CONJG(_r7cfteg3 );
				}
				
				_fmb4u5ka = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_ltxm349p ) ,ILNumerics.F2NET.Intrinsics.ABS(_ylx3r42x ) );
				_irk8i6qr = (_ltxm349p / _fmb4u5ka);
				_3crf0qn3 = (_ylx3r42x / _fmb4u5ka);
				_2qcyvkhx = REAL(ILNumerics.F2NET.Intrinsics.SQRT((_irk8i6qr * ILNumerics.F2NET.Intrinsics.CONJG(_irk8i6qr )) + (_3crf0qn3 * ILNumerics.F2NET.Intrinsics.CONJG(_3crf0qn3 )) ));
				_irk8i6qr = (_irk8i6qr / _2qcyvkhx);
				_3crf0qn3 = (_3crf0qn3 / _2qcyvkhx);
				return;
			}
			else
			if (_6ata7ybv <= (_p1iqarg6 * _t8f5ar2x))
			{
				
				_irk8i6qr = CMPLX(_d0547bi2);
				_3crf0qn3 = CMPLX(_kxg5drh2);
				_6egl0ako = _6ata7ybv;
				return;
			}
			else
			if (_7ahwn9vx <= (_p1iqarg6 * _t8f5ar2x))
			{
				
				_fmb4u5ka = _6ata7ybv;
				_slkbnmvx = _t8f5ar2x;
				if (_fmb4u5ka <= _slkbnmvx)
				{
					
					_irk8i6qr = CMPLX(_d0547bi2);
					_3crf0qn3 = CMPLX(_kxg5drh2);
					_6egl0ako = _fmb4u5ka;
				}
				else
				{
					
					_irk8i6qr = CMPLX(_kxg5drh2);
					_3crf0qn3 = CMPLX(_d0547bi2);
					_6egl0ako = _slkbnmvx;
				}
				
				return;
			}
			else
			if ((_t8f5ar2x <= (_p1iqarg6 * _7ahwn9vx)) | (_t8f5ar2x <= (_p1iqarg6 * _6ata7ybv)))
			{
				
				_fmb4u5ka = _6ata7ybv;
				_slkbnmvx = _7ahwn9vx;
				if (_fmb4u5ka <= _slkbnmvx)
				{
					
					_2qcyvkhx = (_fmb4u5ka / _slkbnmvx);
					_ofbdxt08 = ILNumerics.F2NET.Intrinsics.SQRT(_kxg5drh2 + (_2qcyvkhx * _2qcyvkhx) );
					_6egl0ako = (_t8f5ar2x * (_2qcyvkhx / _ofbdxt08));
					_irk8i6qr = (-(((ILNumerics.F2NET.Intrinsics.CONJG(_zf88apxo ) / _slkbnmvx) / _ofbdxt08)));
					_3crf0qn3 = ((ILNumerics.F2NET.Intrinsics.CONJG(_r7cfteg3 ) / _slkbnmvx) / _ofbdxt08);
				}
				else
				{
					
					_2qcyvkhx = (_slkbnmvx / _fmb4u5ka);
					_ofbdxt08 = ILNumerics.F2NET.Intrinsics.SQRT(_kxg5drh2 + (_2qcyvkhx * _2qcyvkhx) );
					_6egl0ako = (_t8f5ar2x / _ofbdxt08);
					_irk8i6qr = (-(((ILNumerics.F2NET.Intrinsics.CONJG(_zf88apxo ) / _fmb4u5ka) / _ofbdxt08)));
					_3crf0qn3 = ((ILNumerics.F2NET.Intrinsics.CONJG(_r7cfteg3 ) / _fmb4u5ka) / _ofbdxt08);
				}
				
				return;
			}
			else
			{
				//* 
				//*           normal case 
				//* 
				
				_8w7bl1gz = (_7ahwn9vx / _t8f5ar2x);
				_frd823b5 = (_6ata7ybv / _t8f5ar2x);//* 
				
				_1cy9vb4e = ILNumerics.F2NET.Intrinsics.MAX((_kxg5drh2 + (_8w7bl1gz * _8w7bl1gz)) + (_8w7bl1gz * _frd823b5) ,(_8w7bl1gz * _frd823b5) + (_frd823b5 * _frd823b5) );//* 
				//*           See if root is closer to zero or to ONE 
				//* 
				
				_lso0qkri = (_kxg5drh2 + ((_5m0mjfxm * (_8w7bl1gz - _frd823b5)) * (_8w7bl1gz + _frd823b5)));
				if (_lso0qkri >= _d0547bi2)
				{
					//* 
					//*              root is close to zero, compute directly 
					//* 
					
					_p9n405a5 = ((((_8w7bl1gz * _8w7bl1gz) + (_frd823b5 * _frd823b5)) + _kxg5drh2) * _gbf4169i);
					_3crf0qn3 = CMPLX((_frd823b5 * _frd823b5));
					_2ivtt43r = REAL((_3crf0qn3 / (_p9n405a5 + ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS((_p9n405a5 * _p9n405a5) - _3crf0qn3 ) ))));
					_ltxm349p = ((_r7cfteg3 / _t8f5ar2x) / (_kxg5drh2 - _2ivtt43r));
					_ylx3r42x = (-(((_zf88apxo / _t8f5ar2x) / _2ivtt43r)));
					_6egl0ako = (ILNumerics.F2NET.Intrinsics.SQRT(_2ivtt43r + (((_ax5ijvbx * _p1iqarg6) * _p1iqarg6) * _1cy9vb4e) ) * _t8f5ar2x);
				}
				else
				{
					//* 
					//*              root is closer to ONE, shift by that amount 
					//* 
					
					_p9n405a5 = ((((_frd823b5 * _frd823b5) + (_8w7bl1gz * _8w7bl1gz)) - _kxg5drh2) * _gbf4169i);
					_3crf0qn3 = CMPLX((_8w7bl1gz * _8w7bl1gz));
					if (_p9n405a5 >= _d0547bi2)
					{
						
						_2ivtt43r = REAL((-((_3crf0qn3 / (_p9n405a5 + ILNumerics.F2NET.Intrinsics.SQRT((_p9n405a5 * _p9n405a5) + _3crf0qn3 ))))));
					}
					else
					{
						
						_2ivtt43r = REAL((_p9n405a5 - ILNumerics.F2NET.Intrinsics.SQRT((_p9n405a5 * _p9n405a5) + _3crf0qn3 )));
					}
					
					_ltxm349p = (-(((_r7cfteg3 / _t8f5ar2x) / _2ivtt43r)));
					_ylx3r42x = (-(((_zf88apxo / _t8f5ar2x) / (_kxg5drh2 + _2ivtt43r))));
					_6egl0ako = (ILNumerics.F2NET.Intrinsics.SQRT((_kxg5drh2 + _2ivtt43r) + (((_ax5ijvbx * _p1iqarg6) * _p1iqarg6) * _1cy9vb4e) ) * _t8f5ar2x);
				}
				
				_2qcyvkhx = REAL(ILNumerics.F2NET.Intrinsics.SQRT((_ltxm349p * ILNumerics.F2NET.Intrinsics.CONJG(_ltxm349p )) + (_ylx3r42x * ILNumerics.F2NET.Intrinsics.CONJG(_ylx3r42x )) ));
				_irk8i6qr = (_ltxm349p / _2qcyvkhx);
				_3crf0qn3 = (_ylx3r42x / _2qcyvkhx);
				return;//* 
				
			}
			
		}
		
		return;//* 
		//*     End of CLAIC1 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
