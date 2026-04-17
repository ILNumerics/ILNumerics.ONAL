
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
//*> \brief \b ZLAIC1 applies one step of incremental condition estimation. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZLAIC1 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zlaic1.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zlaic1.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zlaic1.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZLAIC1( JOB, J, X, SEST, W, GAMMA, SESTPR, S, C ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            J, JOB 
//*       DOUBLE PRECISION   SEST, SESTPR 
//*       COMPLEX*16         C, GAMMA, S 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX*16         W( J ), X( J ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZLAIC1 applies one step of incremental condition estimation in 
//*> its simplest version: 
//*> 
//*> Let x, twonorm(x) = 1, be an approximate singular vector of an j-by-j 
//*> lower triangular matrix L, such that 
//*>          twonorm(L*x) = sest 
//*> Then ZLAIC1 computes sestpr, s, c such that 
//*> the vector 
//*>                 [ s*x ] 
//*>          xhat = [  c  ] 
//*> is an approximate singular vector of 
//*>                 [ L       0  ] 
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
//*> where  alpha =  x**H * w. 
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
//*>          X is COMPLEX*16 array, dimension (J) 
//*>          The j-vector x. 
//*> \endverbatim 
//*> 
//*> \param[in] SEST 
//*> \verbatim 
//*>          SEST is DOUBLE PRECISION 
//*>          Estimated singular value of j by j matrix L 
//*> \endverbatim 
//*> 
//*> \param[in] W 
//*> \verbatim 
//*>          W is COMPLEX*16 array, dimension (J) 
//*>          The j-vector w. 
//*> \endverbatim 
//*> 
//*> \param[in] GAMMA 
//*> \verbatim 
//*>          GAMMA is COMPLEX*16 
//*>          The diagonal element gamma. 
//*> \endverbatim 
//*> 
//*> \param[out] SESTPR 
//*> \verbatim 
//*>          SESTPR is DOUBLE PRECISION 
//*>          Estimated singular value of (j+1) by (j+1) matrix Lhat. 
//*> \endverbatim 
//*> 
//*> \param[out] S 
//*> \verbatim 
//*>          S is COMPLEX*16 
//*>          Sine needed in forming xhat. 
//*> \endverbatim 
//*> 
//*> \param[out] C 
//*> \verbatim 
//*>          C is COMPLEX*16 
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
//*> \ingroup complex16OTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _663qlh4f(ref Int32 _xcrv93xi, ref Int32 _znpjgsef, complex* _ta7zuy9k, ref Double _d7hhk8za, complex* _z1ioc3c8, ref complex _zf88apxo, ref Double _6egl0ako, ref complex _irk8i6qr, ref complex _3crf0qn3)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Double _5m0mjfxm =  2d;
Double _gbf4169i =  0.5d;
Double _ax5ijvbx =  4d;
Double _7ahwn9vx =  default;
Double _t8f5ar2x =  default;
Double _6ata7ybv =  default;
Double _p9n405a5 =  default;
Double _p1iqarg6 =  default;
Double _1cy9vb4e =  default;
Double _fmb4u5ka =  default;
Double _slkbnmvx =  default;
Double _ofbdxt08 =  default;
Double _2ivtt43r =  default;
Double _lso0qkri =  default;
Double _2qcyvkhx =  default;
Double _8w7bl1gz =  default;
Double _frd823b5 =  default;
complex _r7cfteg3 =  default;
complex _ylx3r42x =  default;
complex _ltxm349p =  default;
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
		
		_p1iqarg6 = _f43eg0w0("Epsilon" );
		_r7cfteg3 = _s2hgtw14(ref _znpjgsef ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) ,_z1ioc3c8 ,ref Unsafe.AsRef((int)1) );//* 
		
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
					
					_irk8i6qr = DCMPLX(_d0547bi2);
					_3crf0qn3 = DCMPLX(_kxg5drh2);
					_6egl0ako = _d0547bi2;
				}
				else
				{
					
					_irk8i6qr = (_r7cfteg3 / _fmb4u5ka);
					_3crf0qn3 = (_zf88apxo / _fmb4u5ka);
					_2qcyvkhx = DBLE(ILNumerics.F2NET.Intrinsics.SQRT((_irk8i6qr * ILNumerics.F2NET.Intrinsics.DCONJG(_irk8i6qr )) + (_3crf0qn3 * ILNumerics.F2NET.Intrinsics.DCONJG(_3crf0qn3 )) ));
					_irk8i6qr = (_irk8i6qr / _2qcyvkhx);
					_3crf0qn3 = (_3crf0qn3 / _2qcyvkhx);
					_6egl0ako = (_fmb4u5ka * _2qcyvkhx);
				}
				
				return;
			}
			else
			if (_6ata7ybv <= (_p1iqarg6 * _t8f5ar2x))
			{
				
				_irk8i6qr = DCMPLX(_kxg5drh2);
				_3crf0qn3 = DCMPLX(_d0547bi2);
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
					
					_irk8i6qr = DCMPLX(_kxg5drh2);
					_3crf0qn3 = DCMPLX(_d0547bi2);
					_6egl0ako = _slkbnmvx;
				}
				else
				{
					
					_irk8i6qr = DCMPLX(_d0547bi2);
					_3crf0qn3 = DCMPLX(_kxg5drh2);
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
				_3crf0qn3 = DCMPLX((_8w7bl1gz * _8w7bl1gz));
				if (_p9n405a5 > _d0547bi2)
				{
					
					_2ivtt43r = DBLE((_3crf0qn3 / (_p9n405a5 + ILNumerics.F2NET.Intrinsics.SQRT((_p9n405a5 * _p9n405a5) + _3crf0qn3 ))));
				}
				else
				{
					
					_2ivtt43r = DBLE((ILNumerics.F2NET.Intrinsics.SQRT((_p9n405a5 * _p9n405a5) + _3crf0qn3 ) - _p9n405a5));
				}
				//* 
				
				_ltxm349p = (-(((_r7cfteg3 / _t8f5ar2x) / _2ivtt43r)));
				_ylx3r42x = (-(((_zf88apxo / _t8f5ar2x) / (_kxg5drh2 + _2ivtt43r))));
				_2qcyvkhx = DBLE(ILNumerics.F2NET.Intrinsics.SQRT((_ltxm349p * ILNumerics.F2NET.Intrinsics.DCONJG(_ltxm349p )) + (_ylx3r42x * ILNumerics.F2NET.Intrinsics.DCONJG(_ylx3r42x )) ));
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
					
					_ltxm349p = DCMPLX(_kxg5drh2);
					_ylx3r42x = DCMPLX(_d0547bi2);
				}
				else
				{
					
					_ltxm349p = (-(ILNumerics.F2NET.Intrinsics.DCONJG(_zf88apxo )));
					_ylx3r42x = ILNumerics.F2NET.Intrinsics.DCONJG(_r7cfteg3 );
				}
				
				_fmb4u5ka = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_ltxm349p ) ,ILNumerics.F2NET.Intrinsics.ABS(_ylx3r42x ) );
				_irk8i6qr = (_ltxm349p / _fmb4u5ka);
				_3crf0qn3 = (_ylx3r42x / _fmb4u5ka);
				_2qcyvkhx = DBLE(ILNumerics.F2NET.Intrinsics.SQRT((_irk8i6qr * ILNumerics.F2NET.Intrinsics.DCONJG(_irk8i6qr )) + (_3crf0qn3 * ILNumerics.F2NET.Intrinsics.DCONJG(_3crf0qn3 )) ));
				_irk8i6qr = (_irk8i6qr / _2qcyvkhx);
				_3crf0qn3 = (_3crf0qn3 / _2qcyvkhx);
				return;
			}
			else
			if (_6ata7ybv <= (_p1iqarg6 * _t8f5ar2x))
			{
				
				_irk8i6qr = DCMPLX(_d0547bi2);
				_3crf0qn3 = DCMPLX(_kxg5drh2);
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
					
					_irk8i6qr = DCMPLX(_d0547bi2);
					_3crf0qn3 = DCMPLX(_kxg5drh2);
					_6egl0ako = _fmb4u5ka;
				}
				else
				{
					
					_irk8i6qr = DCMPLX(_kxg5drh2);
					_3crf0qn3 = DCMPLX(_d0547bi2);
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
					_irk8i6qr = (-(((ILNumerics.F2NET.Intrinsics.DCONJG(_zf88apxo ) / _slkbnmvx) / _ofbdxt08)));
					_3crf0qn3 = ((ILNumerics.F2NET.Intrinsics.DCONJG(_r7cfteg3 ) / _slkbnmvx) / _ofbdxt08);
				}
				else
				{
					
					_2qcyvkhx = (_slkbnmvx / _fmb4u5ka);
					_ofbdxt08 = ILNumerics.F2NET.Intrinsics.SQRT(_kxg5drh2 + (_2qcyvkhx * _2qcyvkhx) );
					_6egl0ako = (_t8f5ar2x / _ofbdxt08);
					_irk8i6qr = (-(((ILNumerics.F2NET.Intrinsics.DCONJG(_zf88apxo ) / _fmb4u5ka) / _ofbdxt08)));
					_3crf0qn3 = ((ILNumerics.F2NET.Intrinsics.DCONJG(_r7cfteg3 ) / _fmb4u5ka) / _ofbdxt08);
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
					_3crf0qn3 = DCMPLX((_frd823b5 * _frd823b5));
					_2ivtt43r = DBLE((_3crf0qn3 / (_p9n405a5 + ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS((_p9n405a5 * _p9n405a5) - _3crf0qn3 ) ))));
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
					_3crf0qn3 = DCMPLX((_8w7bl1gz * _8w7bl1gz));
					if (_p9n405a5 >= _d0547bi2)
					{
						
						_2ivtt43r = DBLE((-((_3crf0qn3 / (_p9n405a5 + ILNumerics.F2NET.Intrinsics.SQRT((_p9n405a5 * _p9n405a5) + _3crf0qn3 ))))));
					}
					else
					{
						
						_2ivtt43r = DBLE((_p9n405a5 - ILNumerics.F2NET.Intrinsics.SQRT((_p9n405a5 * _p9n405a5) + _3crf0qn3 )));
					}
					
					_ltxm349p = (-(((_r7cfteg3 / _t8f5ar2x) / _2ivtt43r)));
					_ylx3r42x = (-(((_zf88apxo / _t8f5ar2x) / (_kxg5drh2 + _2ivtt43r))));
					_6egl0ako = (ILNumerics.F2NET.Intrinsics.SQRT((_kxg5drh2 + _2ivtt43r) + (((_ax5ijvbx * _p1iqarg6) * _p1iqarg6) * _1cy9vb4e) ) * _t8f5ar2x);
				}
				
				_2qcyvkhx = DBLE(ILNumerics.F2NET.Intrinsics.SQRT((_ltxm349p * ILNumerics.F2NET.Intrinsics.DCONJG(_ltxm349p )) + (_ylx3r42x * ILNumerics.F2NET.Intrinsics.DCONJG(_ylx3r42x )) ));
				_irk8i6qr = (_ltxm349p / _2qcyvkhx);
				_3crf0qn3 = (_ylx3r42x / _2qcyvkhx);
				return;//* 
				
			}
			
		}
		
		return;//* 
		//*     End of ZLAIC1 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
