
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
//*> \brief \b DLASQ3 checks for deflation, computes a shift and calls dqds. Used by sbdsqr. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLASQ3 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlasq3.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlasq3.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlasq3.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLASQ3( I0, N0, Z, PP, DMIN, SIGMA, DESIG, QMAX, NFAIL, 
//*                          ITER, NDIV, IEEE, TTYPE, DMIN1, DMIN2, DN, DN1, 
//*                          DN2, G, TAU ) 
//* 
//*       .. Scalar Arguments .. 
//*       LOGICAL            IEEE 
//*       INTEGER            I0, ITER, N0, NDIV, NFAIL, PP 
//*       DOUBLE PRECISION   DESIG, DMIN, DMIN1, DMIN2, DN, DN1, DN2, G, 
//*      $                   QMAX, SIGMA, TAU 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   Z( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLASQ3 checks for deflation, computes a shift (TAU) and calls dqds. 
//*> In case of failure it changes shifts, and tries again until output 
//*> is positive. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] I0 
//*> \verbatim 
//*>          I0 is INTEGER 
//*>         First index. 
//*> \endverbatim 
//*> 
//*> \param[in,out] N0 
//*> \verbatim 
//*>          N0 is INTEGER 
//*>         Last index. 
//*> \endverbatim 
//*> 
//*> \param[in,out] Z 
//*> \verbatim 
//*>          Z is DOUBLE PRECISION array, dimension ( 4*N0 ) 
//*>         Z holds the qd array. 
//*> \endverbatim 
//*> 
//*> \param[in,out] PP 
//*> \verbatim 
//*>          PP is INTEGER 
//*>         PP=0 for ping, PP=1 for pong. 
//*>         PP=2 indicates that flipping was applied to the Z array 
//*>         and that the initial tests for deflation should not be 
//*>         performed. 
//*> \endverbatim 
//*> 
//*> \param[out] DMIN 
//*> \verbatim 
//*>          DMIN is DOUBLE PRECISION 
//*>         Minimum value of d. 
//*> \endverbatim 
//*> 
//*> \param[out] SIGMA 
//*> \verbatim 
//*>          SIGMA is DOUBLE PRECISION 
//*>         Sum of shifts used in current segment. 
//*> \endverbatim 
//*> 
//*> \param[in,out] DESIG 
//*> \verbatim 
//*>          DESIG is DOUBLE PRECISION 
//*>         Lower order part of SIGMA 
//*> \endverbatim 
//*> 
//*> \param[in] QMAX 
//*> \verbatim 
//*>          QMAX is DOUBLE PRECISION 
//*>         Maximum value of q. 
//*> \endverbatim 
//*> 
//*> \param[in,out] NFAIL 
//*> \verbatim 
//*>          NFAIL is INTEGER 
//*>         Increment NFAIL by 1 each time the shift was too big. 
//*> \endverbatim 
//*> 
//*> \param[in,out] ITER 
//*> \verbatim 
//*>          ITER is INTEGER 
//*>         Increment ITER by 1 for each iteration. 
//*> \endverbatim 
//*> 
//*> \param[in,out] NDIV 
//*> \verbatim 
//*>          NDIV is INTEGER 
//*>         Increment NDIV by 1 for each division. 
//*> \endverbatim 
//*> 
//*> \param[in] IEEE 
//*> \verbatim 
//*>          IEEE is LOGICAL 
//*>         Flag for IEEE or non IEEE arithmetic (passed to DLASQ5). 
//*> \endverbatim 
//*> 
//*> \param[in,out] TTYPE 
//*> \verbatim 
//*>          TTYPE is INTEGER 
//*>         Shift type. 
//*> \endverbatim 
//*> 
//*> \param[in,out] DMIN1 
//*> \verbatim 
//*>          DMIN1 is DOUBLE PRECISION 
//*> \endverbatim 
//*> 
//*> \param[in,out] DMIN2 
//*> \verbatim 
//*>          DMIN2 is DOUBLE PRECISION 
//*> \endverbatim 
//*> 
//*> \param[in,out] DN 
//*> \verbatim 
//*>          DN is DOUBLE PRECISION 
//*> \endverbatim 
//*> 
//*> \param[in,out] DN1 
//*> \verbatim 
//*>          DN1 is DOUBLE PRECISION 
//*> \endverbatim 
//*> 
//*> \param[in,out] DN2 
//*> \verbatim 
//*>          DN2 is DOUBLE PRECISION 
//*> \endverbatim 
//*> 
//*> \param[in,out] G 
//*> \verbatim 
//*>          G is DOUBLE PRECISION 
//*> \endverbatim 
//*> 
//*> \param[in,out] TAU 
//*> \verbatim 
//*>          TAU is DOUBLE PRECISION 
//*> 
//*>         These are passed as arguments in order to save their values 
//*>         between calls to DLASQ3. 
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
//*> \date June 2016 
//* 
//*> \ingroup auxOTHERcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _yt0gzo2v(ref Int32 _kgliup4t, ref Int32 _psb09l5j, Double* _7e60fcso, ref Int32 _rk50assb, ref Double _tt3ji15i, ref Double _91a1vq5f, ref Double _xvfwic6z, ref Double _uvmwuql8, ref Int32 _gguzru7t, ref Int32 _em7fbywm, ref Int32 _6vmhvjma, ref Boolean _id0vp1yu, ref Int32 _tx1pza71, ref Double _y61kuds7, ref Double _aaaeq9ec, ref Double _b10nc13b, ref Double _iqx7r7kg, ref Double _i3q9kmqd, ref Double _mu73se41, ref Double _0446f4de)
	{
#region variable declarations
Double _0tzcjs6r =  1.5d;
Double _d0547bi2 =  0d;
Double _52hsdgkl =  0.25d;
Double _gbf4169i =  0.5d;
Double _kxg5drh2 =  1d;
Double _5m0mjfxm =  2d;
Double _2yce0i2m =  100d;
Int32 _828n391q =  default;
Int32 _h5f9ahvx =  default;
Int32 _ubuwekc4 =  default;
Int32 _8dgyhtzt =  default;
Double _p1iqarg6 =  default;
Double _irk8i6qr =  default;
Double _2ivtt43r =  default;
Double _1ajfmh55 =  default;
Double _txq1gp7u =  default;
Double _ecd0ne62 =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK computational routine (version 3.7.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     June 2016 
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
		//*     .. External Function .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		_ubuwekc4 = _psb09l5j;
		_p1iqarg6 = _f43eg0w0("Precision" );
		_txq1gp7u = (_p1iqarg6 * _2yce0i2m);
		_ecd0ne62 = __POW2(_txq1gp7u);//* 
		//*     Check for deflation. 
		//* 
		
Mark10:;
		// continue//* 
		
		if (_psb09l5j < _kgliup4t)
		return;
		if (_psb09l5j == _kgliup4t)goto Mark20;
		_8dgyhtzt = (((int)4 * _psb09l5j) + _rk50assb);
		if (_psb09l5j == (_kgliup4t + (int)1))goto Mark40;//* 
		//*     Check whether E(N0-1) is negligible, 1 eigenvalue. 
		//* 
		
		if ((*(_7e60fcso+(_8dgyhtzt - (int)5 - 1)) > (_ecd0ne62 * (_91a1vq5f + *(_7e60fcso+(_8dgyhtzt - (int)3 - 1))))) & (*(_7e60fcso+((_8dgyhtzt - ((int)2 * _rk50assb)) - (int)4 - 1)) > (_ecd0ne62 * *(_7e60fcso+(_8dgyhtzt - (int)7 - 1)))))goto Mark30;//* 
		
Mark20:;
		// continue//* 
		
		*(_7e60fcso+(((int)4 * _psb09l5j) - (int)3 - 1)) = (*(_7e60fcso+((((int)4 * _psb09l5j) + _rk50assb) - (int)3 - 1)) + _91a1vq5f);
		_psb09l5j = (_psb09l5j - (int)1);goto Mark10;//* 
		//*     Check  whether E(N0-2) is negligible, 2 eigenvalues. 
		//* 
		
Mark30:;
		// continue//* 
		
		if ((*(_7e60fcso+(_8dgyhtzt - (int)9 - 1)) > (_ecd0ne62 * _91a1vq5f)) & (*(_7e60fcso+((_8dgyhtzt - ((int)2 * _rk50assb)) - (int)8 - 1)) > (_ecd0ne62 * *(_7e60fcso+(_8dgyhtzt - (int)11 - 1)))))goto Mark50;//* 
		
Mark40:;
		// continue//* 
		
		if (*(_7e60fcso+(_8dgyhtzt - (int)3 - 1)) > *(_7e60fcso+(_8dgyhtzt - (int)7 - 1)))
		{
			
			_irk8i6qr = *(_7e60fcso+(_8dgyhtzt - (int)3 - 1));
			*(_7e60fcso+(_8dgyhtzt - (int)3 - 1)) = *(_7e60fcso+(_8dgyhtzt - (int)7 - 1));
			*(_7e60fcso+(_8dgyhtzt - (int)7 - 1)) = _irk8i6qr;
		}
		
		_2ivtt43r = (_gbf4169i * ((*(_7e60fcso+(_8dgyhtzt - (int)7 - 1)) - *(_7e60fcso+(_8dgyhtzt - (int)3 - 1))) + *(_7e60fcso+(_8dgyhtzt - (int)5 - 1))));
		if ((*(_7e60fcso+(_8dgyhtzt - (int)5 - 1)) > (*(_7e60fcso+(_8dgyhtzt - (int)3 - 1)) * _ecd0ne62)) & (_2ivtt43r != _d0547bi2))
		{
			
			_irk8i6qr = (*(_7e60fcso+(_8dgyhtzt - (int)3 - 1)) * (*(_7e60fcso+(_8dgyhtzt - (int)5 - 1)) / _2ivtt43r));
			if (_irk8i6qr <= _2ivtt43r)
			{
				
				_irk8i6qr = (*(_7e60fcso+(_8dgyhtzt - (int)3 - 1)) * (*(_7e60fcso+(_8dgyhtzt - (int)5 - 1)) / (_2ivtt43r * (_kxg5drh2 + ILNumerics.F2NET.Intrinsics.SQRT(_kxg5drh2 + (_irk8i6qr / _2ivtt43r) )))));
			}
			else
			{
				
				_irk8i6qr = (*(_7e60fcso+(_8dgyhtzt - (int)3 - 1)) * (*(_7e60fcso+(_8dgyhtzt - (int)5 - 1)) / (_2ivtt43r + (ILNumerics.F2NET.Intrinsics.SQRT(_2ivtt43r ) * ILNumerics.F2NET.Intrinsics.SQRT(_2ivtt43r + _irk8i6qr )))));
			}
			
			_2ivtt43r = (*(_7e60fcso+(_8dgyhtzt - (int)7 - 1)) + (_irk8i6qr + *(_7e60fcso+(_8dgyhtzt - (int)5 - 1))));
			*(_7e60fcso+(_8dgyhtzt - (int)3 - 1)) = (*(_7e60fcso+(_8dgyhtzt - (int)3 - 1)) * (*(_7e60fcso+(_8dgyhtzt - (int)7 - 1)) / _2ivtt43r));
			*(_7e60fcso+(_8dgyhtzt - (int)7 - 1)) = _2ivtt43r;
		}
		
		*(_7e60fcso+(((int)4 * _psb09l5j) - (int)7 - 1)) = (*(_7e60fcso+(_8dgyhtzt - (int)7 - 1)) + _91a1vq5f);
		*(_7e60fcso+(((int)4 * _psb09l5j) - (int)3 - 1)) = (*(_7e60fcso+(_8dgyhtzt - (int)3 - 1)) + _91a1vq5f);
		_psb09l5j = (_psb09l5j - (int)2);goto Mark10;//* 
		
Mark50:;
		// continue
		if (_rk50assb == (int)2)
		_rk50assb = (int)0;//* 
		//*     Reverse the qd-array, if warranted. 
		//* 
		
		if ((_tt3ji15i <= _d0547bi2) | (_psb09l5j < _ubuwekc4))
		{
			
			if ((_0tzcjs6r * *(_7e60fcso+((((int)4 * _kgliup4t) + _rk50assb) - (int)3 - 1))) < *(_7e60fcso+((((int)4 * _psb09l5j) + _rk50assb) - (int)3 - 1)))
			{
				
				_828n391q = ((int)4 * (_kgliup4t + _psb09l5j));
				{
					System.Int32 __81fgg2dlsvn319 = (System.Int32)(((int)4 * _kgliup4t));
					System.Int32 __81fgg2step319 = (System.Int32)((int)4);
					System.Int32 __81fgg2count319;
					for (__81fgg2count319 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)2 * ((_kgliup4t + _psb09l5j) - (int)1)) - __81fgg2dlsvn319 + __81fgg2step319) / __81fgg2step319)), _h5f9ahvx = __81fgg2dlsvn319; __81fgg2count319 != 0; __81fgg2count319--, _h5f9ahvx += (__81fgg2step319)) {

					{
						
						_1ajfmh55 = *(_7e60fcso+(_h5f9ahvx - (int)3 - 1));
						*(_7e60fcso+(_h5f9ahvx - (int)3 - 1)) = *(_7e60fcso+((_828n391q - _h5f9ahvx) - (int)3 - 1));
						*(_7e60fcso+((_828n391q - _h5f9ahvx) - (int)3 - 1)) = _1ajfmh55;
						_1ajfmh55 = *(_7e60fcso+(_h5f9ahvx - (int)2 - 1));
						*(_7e60fcso+(_h5f9ahvx - (int)2 - 1)) = *(_7e60fcso+((_828n391q - _h5f9ahvx) - (int)2 - 1));
						*(_7e60fcso+((_828n391q - _h5f9ahvx) - (int)2 - 1)) = _1ajfmh55;
						_1ajfmh55 = *(_7e60fcso+(_h5f9ahvx - (int)1 - 1));
						*(_7e60fcso+(_h5f9ahvx - (int)1 - 1)) = *(_7e60fcso+((_828n391q - _h5f9ahvx) - (int)5 - 1));
						*(_7e60fcso+((_828n391q - _h5f9ahvx) - (int)5 - 1)) = _1ajfmh55;
						_1ajfmh55 = *(_7e60fcso+(_h5f9ahvx - 1));
						*(_7e60fcso+(_h5f9ahvx - 1)) = *(_7e60fcso+((_828n391q - _h5f9ahvx) - (int)4 - 1));
						*(_7e60fcso+((_828n391q - _h5f9ahvx) - (int)4 - 1)) = _1ajfmh55;
Mark60:;
						// continue
					}
										}				}
				if ((_psb09l5j - _kgliup4t) <= (int)4)
				{
					
					*(_7e60fcso+((((int)4 * _psb09l5j) + _rk50assb) - (int)1 - 1)) = *(_7e60fcso+((((int)4 * _kgliup4t) + _rk50assb) - (int)1 - 1));
					*(_7e60fcso+(((int)4 * _psb09l5j) - _rk50assb - 1)) = *(_7e60fcso+(((int)4 * _kgliup4t) - _rk50assb - 1));
				}
				
				_aaaeq9ec = ILNumerics.F2NET.Intrinsics.MIN(_aaaeq9ec ,*(_7e60fcso+((((int)4 * _psb09l5j) + _rk50assb) - (int)1 - 1)) );
				*(_7e60fcso+((((int)4 * _psb09l5j) + _rk50assb) - (int)1 - 1)) = ILNumerics.F2NET.Intrinsics.MIN(*(_7e60fcso+((((int)4 * _psb09l5j) + _rk50assb) - (int)1 - 1)) ,*(_7e60fcso+((((int)4 * _kgliup4t) + _rk50assb) - (int)1 - 1)) ,*(_7e60fcso+((((int)4 * _kgliup4t) + _rk50assb) + (int)3 - 1)) );
				*(_7e60fcso+(((int)4 * _psb09l5j) - _rk50assb - 1)) = ILNumerics.F2NET.Intrinsics.MIN(*(_7e60fcso+(((int)4 * _psb09l5j) - _rk50assb - 1)) ,*(_7e60fcso+(((int)4 * _kgliup4t) - _rk50assb - 1)) ,*(_7e60fcso+((((int)4 * _kgliup4t) - _rk50assb) + (int)4 - 1)) );
				_uvmwuql8 = ILNumerics.F2NET.Intrinsics.MAX(_uvmwuql8 ,*(_7e60fcso+((((int)4 * _kgliup4t) + _rk50assb) - (int)3 - 1)) ,*(_7e60fcso+((((int)4 * _kgliup4t) + _rk50assb) + (int)1 - 1)) );
				_tt3ji15i = (-(_d0547bi2));
			}
			
		}
		//* 
		//*     Choose a shift. 
		//* 
		
		_s7aqcmn1(ref _kgliup4t ,ref _psb09l5j ,_7e60fcso ,ref _rk50assb ,ref _ubuwekc4 ,ref _tt3ji15i ,ref _y61kuds7 ,ref _aaaeq9ec ,ref _b10nc13b ,ref _iqx7r7kg ,ref _i3q9kmqd ,ref _0446f4de ,ref _tx1pza71 ,ref _mu73se41 );//* 
		//*     Call dqds until DMIN > 0. 
		//* 
		
Mark70:;
		// continue//* 
		
		_3yji8pkn(ref _kgliup4t ,ref _psb09l5j ,_7e60fcso ,ref _rk50assb ,ref _0446f4de ,ref _91a1vq5f ,ref _tt3ji15i ,ref _y61kuds7 ,ref _aaaeq9ec ,ref _b10nc13b ,ref _iqx7r7kg ,ref _i3q9kmqd ,ref _id0vp1yu ,ref _p1iqarg6 );//* 
		
		_6vmhvjma = (_6vmhvjma + ((_psb09l5j - _kgliup4t) + (int)2));
		_em7fbywm = (_em7fbywm + (int)1);//* 
		//*     Check status. 
		//* 
		
		if ((_tt3ji15i >= _d0547bi2) & (_y61kuds7 >= _d0547bi2))
		{
			//* 
			//*        Success. 
			//* 
			goto Mark90;//* 
			
		}
		else
		if ((((_tt3ji15i < _d0547bi2) & (_y61kuds7 > _d0547bi2)) & (*(_7e60fcso+(((int)4 * (_psb09l5j - (int)1)) - _rk50assb - 1)) < (_txq1gp7u * (_91a1vq5f + _iqx7r7kg)))) & (ILNumerics.F2NET.Intrinsics.ABS(_b10nc13b ) < (_txq1gp7u * _91a1vq5f)))
		{
			//* 
			//*        Convergence hidden by negative DN. 
			//* 
			
			*(_7e60fcso+((((int)4 * (_psb09l5j - (int)1)) - _rk50assb) + (int)2 - 1)) = _d0547bi2;
			_tt3ji15i = _d0547bi2;goto Mark90;
		}
		else
		if (_tt3ji15i < _d0547bi2)
		{
			//* 
			//*        TAU too big. Select new TAU and try again. 
			//* 
			
			_gguzru7t = (_gguzru7t + (int)1);
			if (_tx1pza71 < (int)-22)
			{
				//* 
				//*           Failed twice. Play it safe. 
				//* 
				
				_0446f4de = _d0547bi2;
			}
			else
			if (_y61kuds7 > _d0547bi2)
			{
				//* 
				//*           Late failure. Gives excellent shift. 
				//* 
				
				_0446f4de = ((_0446f4de + _tt3ji15i) * (_kxg5drh2 - (_5m0mjfxm * _p1iqarg6)));
				_tx1pza71 = (_tx1pza71 - (int)11);
			}
			else
			{
				//* 
				//*           Early failure. Divide by 4. 
				//* 
				
				_0446f4de = (_52hsdgkl * _0446f4de);
				_tx1pza71 = (_tx1pza71 - (int)12);
			}
			goto Mark70;
		}
		else
		if (_fk98jwhi(ref _tt3ji15i ))
		{
			//* 
			//*        NaN. 
			//* 
			
			if (_0446f4de == _d0547bi2)
			{
				goto Mark80;
			}
			else
			{
				
				_0446f4de = _d0547bi2;goto Mark70;
			}
			
		}
		else
		{
			//* 
			//*        Possible underflow. Play it safe. 
			//* 
			goto Mark80;
		}
		//* 
		//*     Risk of underflow. 
		//* 
		
Mark80:;
		// continue
		_8oz847ut(ref _kgliup4t ,ref _psb09l5j ,_7e60fcso ,ref _rk50assb ,ref _tt3ji15i ,ref _y61kuds7 ,ref _aaaeq9ec ,ref _b10nc13b ,ref _iqx7r7kg ,ref _i3q9kmqd );
		_6vmhvjma = (_6vmhvjma + ((_psb09l5j - _kgliup4t) + (int)2));
		_em7fbywm = (_em7fbywm + (int)1);
		_0446f4de = _d0547bi2;//* 
		
Mark90:;
		// continue
		if (_0446f4de < _91a1vq5f)
		{
			
			_xvfwic6z = (_xvfwic6z + _0446f4de);
			_2ivtt43r = (_91a1vq5f + _xvfwic6z);
			_xvfwic6z = (_xvfwic6z - (_2ivtt43r - _91a1vq5f));
		}
		else
		{
			
			_2ivtt43r = (_91a1vq5f + _0446f4de);
			_xvfwic6z = ((_91a1vq5f - (_2ivtt43r - _0446f4de)) + _xvfwic6z);
		}
		
		_91a1vq5f = _2ivtt43r;//* 
		
		return;//* 
		//*     End of DLASQ3 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
