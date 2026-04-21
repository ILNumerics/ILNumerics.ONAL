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
//*> \brief \b SLASQ4 computes an approximation to the smallest eigenvalue using values of d from the previous transform. Used by sbdsqr. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLASQ4 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slasq4.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slasq4.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slasq4.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLASQ4( I0, N0, Z, PP, N0IN, DMIN, DMIN1, DMIN2, DN, 
//*                          DN1, DN2, TAU, TTYPE, G ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            I0, N0, N0IN, PP, TTYPE 
//*       REAL               DMIN, DMIN1, DMIN2, DN, DN1, DN2, G, TAU 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               Z( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLASQ4 computes an approximation TAU to the smallest eigenvalue 
//*> using values of d from the previous transform. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] I0 
//*> \verbatim 
//*>          I0 is INTEGER 
//*>        First index. 
//*> \endverbatim 
//*> 
//*> \param[in] N0 
//*> \verbatim 
//*>          N0 is INTEGER 
//*>        Last index. 
//*> \endverbatim 
//*> 
//*> \param[in] Z 
//*> \verbatim 
//*>          Z is REAL array, dimension ( 4*N0 ) 
//*>        Z holds the qd array. 
//*> \endverbatim 
//*> 
//*> \param[in] PP 
//*> \verbatim 
//*>          PP is INTEGER 
//*>        PP=0 for ping, PP=1 for pong. 
//*> \endverbatim 
//*> 
//*> \param[in] N0IN 
//*> \verbatim 
//*>          N0IN is INTEGER 
//*>        The value of N0 at start of EIGTEST. 
//*> \endverbatim 
//*> 
//*> \param[in] DMIN 
//*> \verbatim 
//*>          DMIN is REAL 
//*>        Minimum value of d. 
//*> \endverbatim 
//*> 
//*> \param[in] DMIN1 
//*> \verbatim 
//*>          DMIN1 is REAL 
//*>        Minimum value of d, excluding D( N0 ). 
//*> \endverbatim 
//*> 
//*> \param[in] DMIN2 
//*> \verbatim 
//*>          DMIN2 is REAL 
//*>        Minimum value of d, excluding D( N0 ) and D( N0-1 ). 
//*> \endverbatim 
//*> 
//*> \param[in] DN 
//*> \verbatim 
//*>          DN is REAL 
//*>        d(N) 
//*> \endverbatim 
//*> 
//*> \param[in] DN1 
//*> \verbatim 
//*>          DN1 is REAL 
//*>        d(N-1) 
//*> \endverbatim 
//*> 
//*> \param[in] DN2 
//*> \verbatim 
//*>          DN2 is REAL 
//*>        d(N-2) 
//*> \endverbatim 
//*> 
//*> \param[out] TAU 
//*> \verbatim 
//*>          TAU is REAL 
//*>        This is the shift. 
//*> \endverbatim 
//*> 
//*> \param[out] TTYPE 
//*> \verbatim 
//*>          TTYPE is INTEGER 
//*>        Shift type. 
//*> \endverbatim 
//*> 
//*> \param[in,out] G 
//*> \verbatim 
//*>          G is REAL 
//*>        G is passed as an argument in order to save its value between 
//*>        calls to SLASQ4. 
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
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  CNST1 = 9/16 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _t1k6xp6w(ref Int32 _kgliup4t, ref Int32 _psb09l5j, Single* _7e60fcso, ref Int32 _rk50assb, ref Int32 _ubuwekc4, ref Single _tt3ji15i, ref Single _y61kuds7, ref Single _aaaeq9ec, ref Single _b10nc13b, ref Single _iqx7r7kg, ref Single _i3q9kmqd, ref Single _0446f4de, ref Int32 _tx1pza71, ref Single _mu73se41)
	{
#region variable declarations
Single _gxighgmo =  0.563f;
Single _r6hwieom =  1.01f;
Single _dxafg3ih =  1.05f;
Single _52hsdgkl =  0.25f;
Single _x2yzud9a =  0.333f;
Single _gbf4169i =  0.5f;
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
Single _5m0mjfxm =  2f;
Single _2yce0i2m =  100f;
Int32 _3xy4w22e =  default;
Int32 _8dgyhtzt =  default;
Int32 _swsulsml =  default;
Single _vv9ge1s4 =  default;
Single _an5h6pnm =  default;
Single _pasosw3n =  default;
Single _7hdwv3au =  default;
Single _lyuu4cd6 =  default;
Single _j4zhi1ed =  default;
Single _irk8i6qr =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK computational routine (version 3.7.1) -- 
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
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     A negative DMIN forces the shift to take that absolute value 
		//*     TTYPE records the type of shift. 
		//* 
		
		if (_tt3ji15i <= _d0547bi2)
		{
			
			_0446f4de = (-(_tt3ji15i));
			_tx1pza71 = (int)-1;
			return;
		}
		//* 
		
		_8dgyhtzt = (((int)4 * _psb09l5j) + _rk50assb);
		if (_ubuwekc4 == _psb09l5j)
		{
			//* 
			//*        No eigenvalues deflated. 
			//* 
			
			if ((_tt3ji15i == _b10nc13b) | (_tt3ji15i == _iqx7r7kg))
			{
				//* 
				
				_an5h6pnm = (ILNumerics.F2NET.Intrinsics.SQRT(*(_7e60fcso+(_8dgyhtzt - (int)3 - 1)) ) * ILNumerics.F2NET.Intrinsics.SQRT(*(_7e60fcso+(_8dgyhtzt - (int)5 - 1)) ));
				_pasosw3n = (ILNumerics.F2NET.Intrinsics.SQRT(*(_7e60fcso+(_8dgyhtzt - (int)7 - 1)) ) * ILNumerics.F2NET.Intrinsics.SQRT(*(_7e60fcso+(_8dgyhtzt - (int)9 - 1)) ));
				_vv9ge1s4 = (*(_7e60fcso+(_8dgyhtzt - (int)7 - 1)) + *(_7e60fcso+(_8dgyhtzt - (int)5 - 1)));//* 
				//*           Cases 2 and 3. 
				//* 
				
				if ((_tt3ji15i == _b10nc13b) & (_y61kuds7 == _iqx7r7kg))
				{
					
					_j4zhi1ed = ((_aaaeq9ec - _vv9ge1s4) - (_aaaeq9ec * _52hsdgkl));
					if ((_j4zhi1ed > _d0547bi2) & (_j4zhi1ed > _pasosw3n))
					{
						
						_lyuu4cd6 = ((_vv9ge1s4 - _b10nc13b) - ((_pasosw3n / _j4zhi1ed) * _pasosw3n));
					}
					else
					{
						
						_lyuu4cd6 = ((_vv9ge1s4 - _b10nc13b) - (_an5h6pnm + _pasosw3n));
					}
					
					if ((_lyuu4cd6 > _d0547bi2) & (_lyuu4cd6 > _an5h6pnm))
					{
						
						_irk8i6qr = ILNumerics.F2NET.Intrinsics.MAX(_b10nc13b - ((_an5h6pnm / _lyuu4cd6) * _an5h6pnm) ,_gbf4169i * _tt3ji15i );
						_tx1pza71 = (int)-2;
					}
					else
					{
						
						_irk8i6qr = _d0547bi2;
						if (_b10nc13b > _an5h6pnm)
						_irk8i6qr = (_b10nc13b - _an5h6pnm);
						if (_vv9ge1s4 > (_an5h6pnm + _pasosw3n))
						_irk8i6qr = ILNumerics.F2NET.Intrinsics.MIN(_irk8i6qr ,_vv9ge1s4 - (_an5h6pnm + _pasosw3n) );
						_irk8i6qr = ILNumerics.F2NET.Intrinsics.MAX(_irk8i6qr ,_x2yzud9a * _tt3ji15i );
						_tx1pza71 = (int)-3;
					}
					
				}
				else
				{
					//* 
					//*              Case 4. 
					//* 
					
					_tx1pza71 = (int)-4;
					_irk8i6qr = (_52hsdgkl * _tt3ji15i);
					if (_tt3ji15i == _b10nc13b)
					{
						
						_7hdwv3au = _b10nc13b;
						_vv9ge1s4 = _d0547bi2;
						if (*(_7e60fcso+(_8dgyhtzt - (int)5 - 1)) > *(_7e60fcso+(_8dgyhtzt - (int)7 - 1)))
						return;
						_pasosw3n = (*(_7e60fcso+(_8dgyhtzt - (int)5 - 1)) / *(_7e60fcso+(_8dgyhtzt - (int)7 - 1)));
						_swsulsml = (_8dgyhtzt - (int)9);
					}
					else
					{
						
						_swsulsml = (_8dgyhtzt - ((int)2 * _rk50assb));
						_7hdwv3au = _iqx7r7kg;
						if (*(_7e60fcso+(_swsulsml - (int)4 - 1)) > *(_7e60fcso+(_swsulsml - (int)2 - 1)))
						return;
						_vv9ge1s4 = (*(_7e60fcso+(_swsulsml - (int)4 - 1)) / *(_7e60fcso+(_swsulsml - (int)2 - 1)));
						if (*(_7e60fcso+(_8dgyhtzt - (int)9 - 1)) > *(_7e60fcso+(_8dgyhtzt - (int)11 - 1)))
						return;
						_pasosw3n = (*(_7e60fcso+(_8dgyhtzt - (int)9 - 1)) / *(_7e60fcso+(_8dgyhtzt - (int)11 - 1)));
						_swsulsml = (_8dgyhtzt - (int)13);
					}
					//* 
					//*              Approximate contribution to norm squared from I < NN-1. 
					//* 
					
					_vv9ge1s4 = (_vv9ge1s4 + _pasosw3n);
					{
						System.Int32 __81fgg2dlsvn687 = (System.Int32)(_swsulsml);
						System.Int32 __81fgg2step687 = (System.Int32)((int)-4);
						System.Int32 __81fgg2count687;
						for (__81fgg2count687 = System.Math.Max(0, (System.Int32)(((System.Int32)((((int)4 * _kgliup4t) - (int)1) + _rk50assb) - __81fgg2dlsvn687 + __81fgg2step687) / __81fgg2step687)), _3xy4w22e = __81fgg2dlsvn687; __81fgg2count687 != 0; __81fgg2count687--, _3xy4w22e += (__81fgg2step687)) {

						{
							
							if (_pasosw3n == _d0547bi2)goto Mark20;
							_an5h6pnm = _pasosw3n;
							if (*(_7e60fcso+(_3xy4w22e - 1)) > *(_7e60fcso+(_3xy4w22e - (int)2 - 1)))
							return;
							_pasosw3n = (_pasosw3n * (*(_7e60fcso+(_3xy4w22e - 1)) / *(_7e60fcso+(_3xy4w22e - (int)2 - 1))));
							_vv9ge1s4 = (_vv9ge1s4 + _pasosw3n);
							if (((_2yce0i2m * ILNumerics.F2NET.Intrinsics.MAX(_pasosw3n ,_an5h6pnm )) < _vv9ge1s4) | (_gxighgmo < _vv9ge1s4))goto Mark20;
Mark10:;
							// continue
						}
												}					}
Mark20:;
					// continue
					_vv9ge1s4 = (_dxafg3ih * _vv9ge1s4);//* 
					//*              Rayleigh quotient residual bound. 
					//* 
					
					if (_vv9ge1s4 < _gxighgmo)
					_irk8i6qr = ((_7hdwv3au * (_kxg5drh2 - ILNumerics.F2NET.Intrinsics.SQRT(_vv9ge1s4 ))) / (_kxg5drh2 + _vv9ge1s4));
				}
				
			}
			else
			if (_tt3ji15i == _i3q9kmqd)
			{
				//* 
				//*           Case 5. 
				//* 
				
				_tx1pza71 = (int)-5;
				_irk8i6qr = (_52hsdgkl * _tt3ji15i);//* 
				//*           Compute contribution to norm squared from I > NN-2. 
				//* 
				
				_swsulsml = (_8dgyhtzt - ((int)2 * _rk50assb));
				_an5h6pnm = *(_7e60fcso+(_swsulsml - (int)2 - 1));
				_pasosw3n = *(_7e60fcso+(_swsulsml - (int)6 - 1));
				_7hdwv3au = _i3q9kmqd;
				if ((*(_7e60fcso+(_swsulsml - (int)8 - 1)) > _pasosw3n) | (*(_7e60fcso+(_swsulsml - (int)4 - 1)) > _an5h6pnm))
				return;
				_vv9ge1s4 = ((*(_7e60fcso+(_swsulsml - (int)8 - 1)) / _pasosw3n) * (_kxg5drh2 + (*(_7e60fcso+(_swsulsml - (int)4 - 1)) / _an5h6pnm)));//* 
				//*           Approximate contribution to norm squared from I < NN-2. 
				//* 
				
				if ((_psb09l5j - _kgliup4t) > (int)2)
				{
					
					_pasosw3n = (*(_7e60fcso+(_8dgyhtzt - (int)13 - 1)) / *(_7e60fcso+(_8dgyhtzt - (int)15 - 1)));
					_vv9ge1s4 = (_vv9ge1s4 + _pasosw3n);
					{
						System.Int32 __81fgg2dlsvn688 = (System.Int32)((_8dgyhtzt - (int)17));
						System.Int32 __81fgg2step688 = (System.Int32)((int)-4);
						System.Int32 __81fgg2count688;
						for (__81fgg2count688 = System.Math.Max(0, (System.Int32)(((System.Int32)((((int)4 * _kgliup4t) - (int)1) + _rk50assb) - __81fgg2dlsvn688 + __81fgg2step688) / __81fgg2step688)), _3xy4w22e = __81fgg2dlsvn688; __81fgg2count688 != 0; __81fgg2count688--, _3xy4w22e += (__81fgg2step688)) {

						{
							
							if (_pasosw3n == _d0547bi2)goto Mark40;
							_an5h6pnm = _pasosw3n;
							if (*(_7e60fcso+(_3xy4w22e - 1)) > *(_7e60fcso+(_3xy4w22e - (int)2 - 1)))
							return;
							_pasosw3n = (_pasosw3n * (*(_7e60fcso+(_3xy4w22e - 1)) / *(_7e60fcso+(_3xy4w22e - (int)2 - 1))));
							_vv9ge1s4 = (_vv9ge1s4 + _pasosw3n);
							if (((_2yce0i2m * ILNumerics.F2NET.Intrinsics.MAX(_pasosw3n ,_an5h6pnm )) < _vv9ge1s4) | (_gxighgmo < _vv9ge1s4))goto Mark40;
Mark30:;
							// continue
						}
												}					}
Mark40:;
					// continue
					_vv9ge1s4 = (_dxafg3ih * _vv9ge1s4);
				}
				//* 
				
				if (_vv9ge1s4 < _gxighgmo)
				_irk8i6qr = ((_7hdwv3au * (_kxg5drh2 - ILNumerics.F2NET.Intrinsics.SQRT(_vv9ge1s4 ))) / (_kxg5drh2 + _vv9ge1s4));
			}
			else
			{
				//* 
				//*           Case 6, no information to guide us. 
				//* 
				
				if (_tx1pza71 == (int)-6)
				{
					
					_mu73se41 = (_mu73se41 + (_x2yzud9a * (_kxg5drh2 - _mu73se41)));
				}
				else
				if (_tx1pza71 == (int)-18)
				{
					
					_mu73se41 = (_52hsdgkl * _x2yzud9a);
				}
				else
				{
					
					_mu73se41 = _52hsdgkl;
				}
				
				_irk8i6qr = (_mu73se41 * _tt3ji15i);
				_tx1pza71 = (int)-6;
			}
			//* 
			
		}
		else
		if (_ubuwekc4 == (_psb09l5j + (int)1))
		{
			//* 
			//*        One eigenvalue just deflated. Use DMIN1, DN1 for DMIN and DN. 
			//* 
			
			if ((_y61kuds7 == _iqx7r7kg) & (_aaaeq9ec == _i3q9kmqd))
			{
				//* 
				//*           Cases 7 and 8. 
				//* 
				
				_tx1pza71 = (int)-7;
				_irk8i6qr = (_x2yzud9a * _y61kuds7);
				if (*(_7e60fcso+(_8dgyhtzt - (int)5 - 1)) > *(_7e60fcso+(_8dgyhtzt - (int)7 - 1)))
				return;
				_an5h6pnm = (*(_7e60fcso+(_8dgyhtzt - (int)5 - 1)) / *(_7e60fcso+(_8dgyhtzt - (int)7 - 1)));
				_pasosw3n = _an5h6pnm;
				if (_pasosw3n == _d0547bi2)goto Mark60;
				{
					System.Int32 __81fgg2dlsvn689 = (System.Int32)(((((int)4 * _psb09l5j) - (int)9) + _rk50assb));
					System.Int32 __81fgg2step689 = (System.Int32)((int)-4);
					System.Int32 __81fgg2count689;
					for (__81fgg2count689 = System.Math.Max(0, (System.Int32)(((System.Int32)((((int)4 * _kgliup4t) - (int)1) + _rk50assb) - __81fgg2dlsvn689 + __81fgg2step689) / __81fgg2step689)), _3xy4w22e = __81fgg2dlsvn689; __81fgg2count689 != 0; __81fgg2count689--, _3xy4w22e += (__81fgg2step689)) {

					{
						
						_vv9ge1s4 = _an5h6pnm;
						if (*(_7e60fcso+(_3xy4w22e - 1)) > *(_7e60fcso+(_3xy4w22e - (int)2 - 1)))
						return;
						_an5h6pnm = (_an5h6pnm * (*(_7e60fcso+(_3xy4w22e - 1)) / *(_7e60fcso+(_3xy4w22e - (int)2 - 1))));
						_pasosw3n = (_pasosw3n + _an5h6pnm);
						if ((_2yce0i2m * ILNumerics.F2NET.Intrinsics.MAX(_an5h6pnm ,_vv9ge1s4 )) < _pasosw3n)goto Mark60;
Mark50:;
						// continue
					}
										}				}
Mark60:;
				// continue
				_pasosw3n = ILNumerics.F2NET.Intrinsics.SQRT(_dxafg3ih * _pasosw3n );
				_vv9ge1s4 = (_y61kuds7 / (_kxg5drh2 + __POW2(_pasosw3n)));
				_j4zhi1ed = ((_gbf4169i * _aaaeq9ec) - _vv9ge1s4);
				if ((_j4zhi1ed > _d0547bi2) & (_j4zhi1ed > (_pasosw3n * _vv9ge1s4)))
				{
					
					_irk8i6qr = ILNumerics.F2NET.Intrinsics.MAX(_irk8i6qr ,_vv9ge1s4 * (_kxg5drh2 - (((_r6hwieom * _vv9ge1s4) * (_pasosw3n / _j4zhi1ed)) * _pasosw3n)) );
				}
				else
				{
					
					_irk8i6qr = ILNumerics.F2NET.Intrinsics.MAX(_irk8i6qr ,_vv9ge1s4 * (_kxg5drh2 - (_r6hwieom * _pasosw3n)) );
					_tx1pza71 = (int)-8;
				}
				
			}
			else
			{
				//* 
				//*           Case 9. 
				//* 
				
				_irk8i6qr = (_52hsdgkl * _y61kuds7);
				if (_y61kuds7 == _iqx7r7kg)
				_irk8i6qr = (_gbf4169i * _y61kuds7);
				_tx1pza71 = (int)-9;
			}
			//* 
			
		}
		else
		if (_ubuwekc4 == (_psb09l5j + (int)2))
		{
			//* 
			//*        Two eigenvalues deflated. Use DMIN2, DN2 for DMIN and DN. 
			//* 
			//*        Cases 10 and 11. 
			//* 
			
			if ((_aaaeq9ec == _i3q9kmqd) & ((_5m0mjfxm * *(_7e60fcso+(_8dgyhtzt - (int)5 - 1))) < *(_7e60fcso+(_8dgyhtzt - (int)7 - 1))))
			{
				
				_tx1pza71 = (int)-10;
				_irk8i6qr = (_x2yzud9a * _aaaeq9ec);
				if (*(_7e60fcso+(_8dgyhtzt - (int)5 - 1)) > *(_7e60fcso+(_8dgyhtzt - (int)7 - 1)))
				return;
				_an5h6pnm = (*(_7e60fcso+(_8dgyhtzt - (int)5 - 1)) / *(_7e60fcso+(_8dgyhtzt - (int)7 - 1)));
				_pasosw3n = _an5h6pnm;
				if (_pasosw3n == _d0547bi2)goto Mark80;
				{
					System.Int32 __81fgg2dlsvn690 = (System.Int32)(((((int)4 * _psb09l5j) - (int)9) + _rk50assb));
					System.Int32 __81fgg2step690 = (System.Int32)((int)-4);
					System.Int32 __81fgg2count690;
					for (__81fgg2count690 = System.Math.Max(0, (System.Int32)(((System.Int32)((((int)4 * _kgliup4t) - (int)1) + _rk50assb) - __81fgg2dlsvn690 + __81fgg2step690) / __81fgg2step690)), _3xy4w22e = __81fgg2dlsvn690; __81fgg2count690 != 0; __81fgg2count690--, _3xy4w22e += (__81fgg2step690)) {

					{
						
						if (*(_7e60fcso+(_3xy4w22e - 1)) > *(_7e60fcso+(_3xy4w22e - (int)2 - 1)))
						return;
						_an5h6pnm = (_an5h6pnm * (*(_7e60fcso+(_3xy4w22e - 1)) / *(_7e60fcso+(_3xy4w22e - (int)2 - 1))));
						_pasosw3n = (_pasosw3n + _an5h6pnm);
						if ((_2yce0i2m * _an5h6pnm) < _pasosw3n)goto Mark80;
Mark70:;
						// continue
					}
										}				}
Mark80:;
				// continue
				_pasosw3n = ILNumerics.F2NET.Intrinsics.SQRT(_dxafg3ih * _pasosw3n );
				_vv9ge1s4 = (_aaaeq9ec / (_kxg5drh2 + __POW2(_pasosw3n)));
				_j4zhi1ed = (((*(_7e60fcso+(_8dgyhtzt - (int)7 - 1)) + *(_7e60fcso+(_8dgyhtzt - (int)9 - 1))) - (ILNumerics.F2NET.Intrinsics.SQRT(*(_7e60fcso+(_8dgyhtzt - (int)11 - 1)) ) * ILNumerics.F2NET.Intrinsics.SQRT(*(_7e60fcso+(_8dgyhtzt - (int)9 - 1)) ))) - _vv9ge1s4);
				if ((_j4zhi1ed > _d0547bi2) & (_j4zhi1ed > (_pasosw3n * _vv9ge1s4)))
				{
					
					_irk8i6qr = ILNumerics.F2NET.Intrinsics.MAX(_irk8i6qr ,_vv9ge1s4 * (_kxg5drh2 - (((_r6hwieom * _vv9ge1s4) * (_pasosw3n / _j4zhi1ed)) * _pasosw3n)) );
				}
				else
				{
					
					_irk8i6qr = ILNumerics.F2NET.Intrinsics.MAX(_irk8i6qr ,_vv9ge1s4 * (_kxg5drh2 - (_r6hwieom * _pasosw3n)) );
				}
				
			}
			else
			{
				
				_irk8i6qr = (_52hsdgkl * _aaaeq9ec);
				_tx1pza71 = (int)-11;
			}
			
		}
		else
		if (_ubuwekc4 > (_psb09l5j + (int)2))
		{
			//* 
			//*        Case 12, more than two eigenvalues deflated. No information. 
			//* 
			
			_irk8i6qr = _d0547bi2;
			_tx1pza71 = (int)-12;
		}
		//* 
		
		_0446f4de = _irk8i6qr;
		return;//* 
		//*     End of SLASQ4 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
