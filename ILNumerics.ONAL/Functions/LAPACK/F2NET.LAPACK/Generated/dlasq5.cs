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
//*> \brief \b DLASQ5 computes one dqds transform in ping-pong form. Used by sbdsqr and sstegr. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLASQ5 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlasq5.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlasq5.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlasq5.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLASQ5( I0, N0, Z, PP, TAU, SIGMA, DMIN, DMIN1, DMIN2, DN, 
//*                          DNM1, DNM2, IEEE, EPS ) 
//* 
//*       .. Scalar Arguments .. 
//*       LOGICAL            IEEE 
//*       INTEGER            I0, N0, PP 
//*       DOUBLE PRECISION   DMIN, DMIN1, DMIN2, DN, DNM1, DNM2, TAU, SIGMA, EPS 
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
//*> DLASQ5 computes one dqds transform in ping-pong form, one 
//*> version for IEEE machines another for non IEEE machines. 
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
//*>          Z is DOUBLE PRECISION array, dimension ( 4*N ) 
//*>        Z holds the qd array. EMIN is stored in Z(4*N0) to avoid 
//*>        an extra argument. 
//*> \endverbatim 
//*> 
//*> \param[in] PP 
//*> \verbatim 
//*>          PP is INTEGER 
//*>        PP=0 for ping, PP=1 for pong. 
//*> \endverbatim 
//*> 
//*> \param[in] TAU 
//*> \verbatim 
//*>          TAU is DOUBLE PRECISION 
//*>        This is the shift. 
//*> \endverbatim 
//*> 
//*> \param[in] SIGMA 
//*> \verbatim 
//*>          SIGMA is DOUBLE PRECISION 
//*>        This is the accumulated shift up to this step. 
//*> \endverbatim 
//*> 
//*> \param[out] DMIN 
//*> \verbatim 
//*>          DMIN is DOUBLE PRECISION 
//*>        Minimum value of d. 
//*> \endverbatim 
//*> 
//*> \param[out] DMIN1 
//*> \verbatim 
//*>          DMIN1 is DOUBLE PRECISION 
//*>        Minimum value of d, excluding D( N0 ). 
//*> \endverbatim 
//*> 
//*> \param[out] DMIN2 
//*> \verbatim 
//*>          DMIN2 is DOUBLE PRECISION 
//*>        Minimum value of d, excluding D( N0 ) and D( N0-1 ). 
//*> \endverbatim 
//*> 
//*> \param[out] DN 
//*> \verbatim 
//*>          DN is DOUBLE PRECISION 
//*>        d(N0), the last value of d. 
//*> \endverbatim 
//*> 
//*> \param[out] DNM1 
//*> \verbatim 
//*>          DNM1 is DOUBLE PRECISION 
//*>        d(N0-1). 
//*> \endverbatim 
//*> 
//*> \param[out] DNM2 
//*> \verbatim 
//*>          DNM2 is DOUBLE PRECISION 
//*>        d(N0-2). 
//*> \endverbatim 
//*> 
//*> \param[in] IEEE 
//*> \verbatim 
//*>          IEEE is LOGICAL 
//*>        Flag for IEEE or non IEEE arithmetic. 
//*> \endverbatim 
//*> 
//*> \param[in] EPS 
//*> \verbatim 
//*>          EPS is DOUBLE PRECISION 
//*>        This is the value of epsilon used. 
//*> \endverbatim 
//*> 
//*  Authors: 
//*  ======== 
//* 
//*> \author Univ. of Tennessee 
//*> \author Univ. of California Berkeley 
//*> \author Univ. of Colorado Denver 
//*> \author NAG Ltd. 
//* 
//*> \date June 2017 
//* 
//*> \ingroup auxOTHERcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _3yji8pkn(ref Int32 _kgliup4t, ref Int32 _psb09l5j, Double* _7e60fcso, ref Int32 _rk50assb, ref Double _0446f4de, ref Double _91a1vq5f, ref Double _tt3ji15i, ref Double _y61kuds7, ref Double _aaaeq9ec, ref Double _b10nc13b, ref Double _ozgqyi38, ref Double _9vhid3u5, ref Boolean _id0vp1yu, ref Double _p1iqarg6)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Double _gbf4169i =  (double)(0.5f);
Int32 _h5f9ahvx =  default;
Int32 _bwb7am8r =  default;
Double _plfm7z8g =  default;
Double _48oov32t =  default;
Double _1ajfmh55 =  default;
Double _fh3l9l14 =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK computational routine (version 3.7.1) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     June 2017 
		//* 
		//*     .. Scalar Arguments .. 
		//*     .. 
		//*     .. Array Arguments .. 
		//*     .. 
		//* 
		//*  ===================================================================== 
		//* 
		//*     .. Parameter .. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		if (((_psb09l5j - _kgliup4t) - (int)1) <= (int)0)
		return;//* 
		
		_fh3l9l14 = (_p1iqarg6 * (_91a1vq5f + _0446f4de));
		if (_0446f4de < (_fh3l9l14 * _gbf4169i))
		_0446f4de = _d0547bi2;
		if (_0446f4de != _d0547bi2)
		{
			
			_h5f9ahvx = ((((int)4 * _kgliup4t) + _rk50assb) - (int)3);
			_48oov32t = *(_7e60fcso+(_h5f9ahvx + (int)4 - 1));
			_plfm7z8g = (*(_7e60fcso+(_h5f9ahvx - 1)) - _0446f4de);
			_tt3ji15i = _plfm7z8g;
			_y61kuds7 = (-(*(_7e60fcso+(_h5f9ahvx - 1))));//* 
			
			if (_id0vp1yu)
			{
				//* 
				//*        Code for IEEE arithmetic. 
				//* 
				
				if (_rk50assb == (int)0)
				{
					
					{
						System.Int32 __81fgg2dlsvn1 = (System.Int32)(((int)4 * _kgliup4t));
						System.Int32 __81fgg2step1 = (System.Int32)((int)4);
						System.Int32 __81fgg2count1;
						for (__81fgg2count1 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)4 * (_psb09l5j - (int)3)) - __81fgg2dlsvn1 + __81fgg2step1) / __81fgg2step1)), _h5f9ahvx = __81fgg2dlsvn1; __81fgg2count1 != 0; __81fgg2count1--, _h5f9ahvx += (__81fgg2step1)) {

						{
							
							*(_7e60fcso+(_h5f9ahvx - (int)2 - 1)) = (_plfm7z8g + *(_7e60fcso+(_h5f9ahvx - (int)1 - 1)));
							_1ajfmh55 = (*(_7e60fcso+(_h5f9ahvx + (int)1 - 1)) / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1)));
							_plfm7z8g = ((_plfm7z8g * _1ajfmh55) - _0446f4de);
							_tt3ji15i = ILNumerics.F2NET.Intrinsics.MIN(_tt3ji15i ,_plfm7z8g );
							*(_7e60fcso+(_h5f9ahvx - 1)) = (*(_7e60fcso+(_h5f9ahvx - (int)1 - 1)) * _1ajfmh55);
							_48oov32t = ILNumerics.F2NET.Intrinsics.MIN(*(_7e60fcso+(_h5f9ahvx - 1)) ,_48oov32t );
Mark10:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn2 = (System.Int32)(((int)4 * _kgliup4t));
						System.Int32 __81fgg2step2 = (System.Int32)((int)4);
						System.Int32 __81fgg2count2;
						for (__81fgg2count2 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)4 * (_psb09l5j - (int)3)) - __81fgg2dlsvn2 + __81fgg2step2) / __81fgg2step2)), _h5f9ahvx = __81fgg2dlsvn2; __81fgg2count2 != 0; __81fgg2count2--, _h5f9ahvx += (__81fgg2step2)) {

						{
							
							*(_7e60fcso+(_h5f9ahvx - (int)3 - 1)) = (_plfm7z8g + *(_7e60fcso+(_h5f9ahvx - 1)));
							_1ajfmh55 = (*(_7e60fcso+(_h5f9ahvx + (int)2 - 1)) / *(_7e60fcso+(_h5f9ahvx - (int)3 - 1)));
							_plfm7z8g = ((_plfm7z8g * _1ajfmh55) - _0446f4de);
							_tt3ji15i = ILNumerics.F2NET.Intrinsics.MIN(_tt3ji15i ,_plfm7z8g );
							*(_7e60fcso+(_h5f9ahvx - (int)1 - 1)) = (*(_7e60fcso+(_h5f9ahvx - 1)) * _1ajfmh55);
							_48oov32t = ILNumerics.F2NET.Intrinsics.MIN(*(_7e60fcso+(_h5f9ahvx - (int)1 - 1)) ,_48oov32t );
Mark20:;
							// continue
						}
												}					}
				}
				//* 
				//*        Unroll last two steps. 
				//* 
				
				_9vhid3u5 = _plfm7z8g;
				_aaaeq9ec = _tt3ji15i;
				_h5f9ahvx = (((int)4 * (_psb09l5j - (int)2)) - _rk50assb);
				_bwb7am8r = ((_h5f9ahvx + ((int)2 * _rk50assb)) - (int)1);
				*(_7e60fcso+(_h5f9ahvx - (int)2 - 1)) = (_9vhid3u5 + *(_7e60fcso+(_bwb7am8r - 1)));
				*(_7e60fcso+(_h5f9ahvx - 1)) = (*(_7e60fcso+(_bwb7am8r + (int)2 - 1)) * (*(_7e60fcso+(_bwb7am8r - 1)) / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1))));
				_ozgqyi38 = ((*(_7e60fcso+(_bwb7am8r + (int)2 - 1)) * (_9vhid3u5 / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1)))) - _0446f4de);
				_tt3ji15i = ILNumerics.F2NET.Intrinsics.MIN(_tt3ji15i ,_ozgqyi38 );//* 
				
				_y61kuds7 = _tt3ji15i;
				_h5f9ahvx = (_h5f9ahvx + (int)4);
				_bwb7am8r = ((_h5f9ahvx + ((int)2 * _rk50assb)) - (int)1);
				*(_7e60fcso+(_h5f9ahvx - (int)2 - 1)) = (_ozgqyi38 + *(_7e60fcso+(_bwb7am8r - 1)));
				*(_7e60fcso+(_h5f9ahvx - 1)) = (*(_7e60fcso+(_bwb7am8r + (int)2 - 1)) * (*(_7e60fcso+(_bwb7am8r - 1)) / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1))));
				_b10nc13b = ((*(_7e60fcso+(_bwb7am8r + (int)2 - 1)) * (_ozgqyi38 / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1)))) - _0446f4de);
				_tt3ji15i = ILNumerics.F2NET.Intrinsics.MIN(_tt3ji15i ,_b10nc13b );//* 
				
			}
			else
			{
				//* 
				//*        Code for non IEEE arithmetic. 
				//* 
				
				if (_rk50assb == (int)0)
				{
					
					{
						System.Int32 __81fgg2dlsvn3 = (System.Int32)(((int)4 * _kgliup4t));
						System.Int32 __81fgg2step3 = (System.Int32)((int)4);
						System.Int32 __81fgg2count3;
						for (__81fgg2count3 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)4 * (_psb09l5j - (int)3)) - __81fgg2dlsvn3 + __81fgg2step3) / __81fgg2step3)), _h5f9ahvx = __81fgg2dlsvn3; __81fgg2count3 != 0; __81fgg2count3--, _h5f9ahvx += (__81fgg2step3)) {

						{
							
							*(_7e60fcso+(_h5f9ahvx - (int)2 - 1)) = (_plfm7z8g + *(_7e60fcso+(_h5f9ahvx - (int)1 - 1)));
							if (_plfm7z8g < _d0547bi2)
							{
								
								return;
							}
							else
							{
								
								*(_7e60fcso+(_h5f9ahvx - 1)) = (*(_7e60fcso+(_h5f9ahvx + (int)1 - 1)) * (*(_7e60fcso+(_h5f9ahvx - (int)1 - 1)) / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1))));
								_plfm7z8g = ((*(_7e60fcso+(_h5f9ahvx + (int)1 - 1)) * (_plfm7z8g / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1)))) - _0446f4de);
							}
							
							_tt3ji15i = ILNumerics.F2NET.Intrinsics.MIN(_tt3ji15i ,_plfm7z8g );
							_48oov32t = ILNumerics.F2NET.Intrinsics.MIN(_48oov32t ,*(_7e60fcso+(_h5f9ahvx - 1)) );
Mark30:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn4 = (System.Int32)(((int)4 * _kgliup4t));
						System.Int32 __81fgg2step4 = (System.Int32)((int)4);
						System.Int32 __81fgg2count4;
						for (__81fgg2count4 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)4 * (_psb09l5j - (int)3)) - __81fgg2dlsvn4 + __81fgg2step4) / __81fgg2step4)), _h5f9ahvx = __81fgg2dlsvn4; __81fgg2count4 != 0; __81fgg2count4--, _h5f9ahvx += (__81fgg2step4)) {

						{
							
							*(_7e60fcso+(_h5f9ahvx - (int)3 - 1)) = (_plfm7z8g + *(_7e60fcso+(_h5f9ahvx - 1)));
							if (_plfm7z8g < _d0547bi2)
							{
								
								return;
							}
							else
							{
								
								*(_7e60fcso+(_h5f9ahvx - (int)1 - 1)) = (*(_7e60fcso+(_h5f9ahvx + (int)2 - 1)) * (*(_7e60fcso+(_h5f9ahvx - 1)) / *(_7e60fcso+(_h5f9ahvx - (int)3 - 1))));
								_plfm7z8g = ((*(_7e60fcso+(_h5f9ahvx + (int)2 - 1)) * (_plfm7z8g / *(_7e60fcso+(_h5f9ahvx - (int)3 - 1)))) - _0446f4de);
							}
							
							_tt3ji15i = ILNumerics.F2NET.Intrinsics.MIN(_tt3ji15i ,_plfm7z8g );
							_48oov32t = ILNumerics.F2NET.Intrinsics.MIN(_48oov32t ,*(_7e60fcso+(_h5f9ahvx - (int)1 - 1)) );
Mark40:;
							// continue
						}
												}					}
				}
				//* 
				//*        Unroll last two steps. 
				//* 
				
				_9vhid3u5 = _plfm7z8g;
				_aaaeq9ec = _tt3ji15i;
				_h5f9ahvx = (((int)4 * (_psb09l5j - (int)2)) - _rk50assb);
				_bwb7am8r = ((_h5f9ahvx + ((int)2 * _rk50assb)) - (int)1);
				*(_7e60fcso+(_h5f9ahvx - (int)2 - 1)) = (_9vhid3u5 + *(_7e60fcso+(_bwb7am8r - 1)));
				if (_9vhid3u5 < _d0547bi2)
				{
					
					return;
				}
				else
				{
					
					*(_7e60fcso+(_h5f9ahvx - 1)) = (*(_7e60fcso+(_bwb7am8r + (int)2 - 1)) * (*(_7e60fcso+(_bwb7am8r - 1)) / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1))));
					_ozgqyi38 = ((*(_7e60fcso+(_bwb7am8r + (int)2 - 1)) * (_9vhid3u5 / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1)))) - _0446f4de);
				}
				
				_tt3ji15i = ILNumerics.F2NET.Intrinsics.MIN(_tt3ji15i ,_ozgqyi38 );//* 
				
				_y61kuds7 = _tt3ji15i;
				_h5f9ahvx = (_h5f9ahvx + (int)4);
				_bwb7am8r = ((_h5f9ahvx + ((int)2 * _rk50assb)) - (int)1);
				*(_7e60fcso+(_h5f9ahvx - (int)2 - 1)) = (_ozgqyi38 + *(_7e60fcso+(_bwb7am8r - 1)));
				if (_ozgqyi38 < _d0547bi2)
				{
					
					return;
				}
				else
				{
					
					*(_7e60fcso+(_h5f9ahvx - 1)) = (*(_7e60fcso+(_bwb7am8r + (int)2 - 1)) * (*(_7e60fcso+(_bwb7am8r - 1)) / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1))));
					_b10nc13b = ((*(_7e60fcso+(_bwb7am8r + (int)2 - 1)) * (_ozgqyi38 / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1)))) - _0446f4de);
				}
				
				_tt3ji15i = ILNumerics.F2NET.Intrinsics.MIN(_tt3ji15i ,_b10nc13b );//* 
				
			}
			
		}
		else
		{
			//*     This is the version that sets d's to zero if they are small enough 
			
			_h5f9ahvx = ((((int)4 * _kgliup4t) + _rk50assb) - (int)3);
			_48oov32t = *(_7e60fcso+(_h5f9ahvx + (int)4 - 1));
			_plfm7z8g = (*(_7e60fcso+(_h5f9ahvx - 1)) - _0446f4de);
			_tt3ji15i = _plfm7z8g;
			_y61kuds7 = (-(*(_7e60fcso+(_h5f9ahvx - 1))));
			if (_id0vp1yu)
			{
				//* 
				//*     Code for IEEE arithmetic. 
				//* 
				
				if (_rk50assb == (int)0)
				{
					
					{
						System.Int32 __81fgg2dlsvn5 = (System.Int32)(((int)4 * _kgliup4t));
						System.Int32 __81fgg2step5 = (System.Int32)((int)4);
						System.Int32 __81fgg2count5;
						for (__81fgg2count5 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)4 * (_psb09l5j - (int)3)) - __81fgg2dlsvn5 + __81fgg2step5) / __81fgg2step5)), _h5f9ahvx = __81fgg2dlsvn5; __81fgg2count5 != 0; __81fgg2count5--, _h5f9ahvx += (__81fgg2step5)) {

						{
							
							*(_7e60fcso+(_h5f9ahvx - (int)2 - 1)) = (_plfm7z8g + *(_7e60fcso+(_h5f9ahvx - (int)1 - 1)));
							_1ajfmh55 = (*(_7e60fcso+(_h5f9ahvx + (int)1 - 1)) / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1)));
							_plfm7z8g = ((_plfm7z8g * _1ajfmh55) - _0446f4de);
							if (_plfm7z8g < _fh3l9l14)
							_plfm7z8g = _d0547bi2;
							_tt3ji15i = ILNumerics.F2NET.Intrinsics.MIN(_tt3ji15i ,_plfm7z8g );
							*(_7e60fcso+(_h5f9ahvx - 1)) = (*(_7e60fcso+(_h5f9ahvx - (int)1 - 1)) * _1ajfmh55);
							_48oov32t = ILNumerics.F2NET.Intrinsics.MIN(*(_7e60fcso+(_h5f9ahvx - 1)) ,_48oov32t );
Mark50:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn6 = (System.Int32)(((int)4 * _kgliup4t));
						System.Int32 __81fgg2step6 = (System.Int32)((int)4);
						System.Int32 __81fgg2count6;
						for (__81fgg2count6 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)4 * (_psb09l5j - (int)3)) - __81fgg2dlsvn6 + __81fgg2step6) / __81fgg2step6)), _h5f9ahvx = __81fgg2dlsvn6; __81fgg2count6 != 0; __81fgg2count6--, _h5f9ahvx += (__81fgg2step6)) {

						{
							
							*(_7e60fcso+(_h5f9ahvx - (int)3 - 1)) = (_plfm7z8g + *(_7e60fcso+(_h5f9ahvx - 1)));
							_1ajfmh55 = (*(_7e60fcso+(_h5f9ahvx + (int)2 - 1)) / *(_7e60fcso+(_h5f9ahvx - (int)3 - 1)));
							_plfm7z8g = ((_plfm7z8g * _1ajfmh55) - _0446f4de);
							if (_plfm7z8g < _fh3l9l14)
							_plfm7z8g = _d0547bi2;
							_tt3ji15i = ILNumerics.F2NET.Intrinsics.MIN(_tt3ji15i ,_plfm7z8g );
							*(_7e60fcso+(_h5f9ahvx - (int)1 - 1)) = (*(_7e60fcso+(_h5f9ahvx - 1)) * _1ajfmh55);
							_48oov32t = ILNumerics.F2NET.Intrinsics.MIN(*(_7e60fcso+(_h5f9ahvx - (int)1 - 1)) ,_48oov32t );
Mark60:;
							// continue
						}
												}					}
				}
				//* 
				//*     Unroll last two steps. 
				//* 
				
				_9vhid3u5 = _plfm7z8g;
				_aaaeq9ec = _tt3ji15i;
				_h5f9ahvx = (((int)4 * (_psb09l5j - (int)2)) - _rk50assb);
				_bwb7am8r = ((_h5f9ahvx + ((int)2 * _rk50assb)) - (int)1);
				*(_7e60fcso+(_h5f9ahvx - (int)2 - 1)) = (_9vhid3u5 + *(_7e60fcso+(_bwb7am8r - 1)));
				*(_7e60fcso+(_h5f9ahvx - 1)) = (*(_7e60fcso+(_bwb7am8r + (int)2 - 1)) * (*(_7e60fcso+(_bwb7am8r - 1)) / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1))));
				_ozgqyi38 = ((*(_7e60fcso+(_bwb7am8r + (int)2 - 1)) * (_9vhid3u5 / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1)))) - _0446f4de);
				_tt3ji15i = ILNumerics.F2NET.Intrinsics.MIN(_tt3ji15i ,_ozgqyi38 );//* 
				
				_y61kuds7 = _tt3ji15i;
				_h5f9ahvx = (_h5f9ahvx + (int)4);
				_bwb7am8r = ((_h5f9ahvx + ((int)2 * _rk50assb)) - (int)1);
				*(_7e60fcso+(_h5f9ahvx - (int)2 - 1)) = (_ozgqyi38 + *(_7e60fcso+(_bwb7am8r - 1)));
				*(_7e60fcso+(_h5f9ahvx - 1)) = (*(_7e60fcso+(_bwb7am8r + (int)2 - 1)) * (*(_7e60fcso+(_bwb7am8r - 1)) / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1))));
				_b10nc13b = ((*(_7e60fcso+(_bwb7am8r + (int)2 - 1)) * (_ozgqyi38 / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1)))) - _0446f4de);
				_tt3ji15i = ILNumerics.F2NET.Intrinsics.MIN(_tt3ji15i ,_b10nc13b );//* 
				
			}
			else
			{
				//* 
				//*     Code for non IEEE arithmetic. 
				//* 
				
				if (_rk50assb == (int)0)
				{
					
					{
						System.Int32 __81fgg2dlsvn7 = (System.Int32)(((int)4 * _kgliup4t));
						System.Int32 __81fgg2step7 = (System.Int32)((int)4);
						System.Int32 __81fgg2count7;
						for (__81fgg2count7 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)4 * (_psb09l5j - (int)3)) - __81fgg2dlsvn7 + __81fgg2step7) / __81fgg2step7)), _h5f9ahvx = __81fgg2dlsvn7; __81fgg2count7 != 0; __81fgg2count7--, _h5f9ahvx += (__81fgg2step7)) {

						{
							
							*(_7e60fcso+(_h5f9ahvx - (int)2 - 1)) = (_plfm7z8g + *(_7e60fcso+(_h5f9ahvx - (int)1 - 1)));
							if (_plfm7z8g < _d0547bi2)
							{
								
								return;
							}
							else
							{
								
								*(_7e60fcso+(_h5f9ahvx - 1)) = (*(_7e60fcso+(_h5f9ahvx + (int)1 - 1)) * (*(_7e60fcso+(_h5f9ahvx - (int)1 - 1)) / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1))));
								_plfm7z8g = ((*(_7e60fcso+(_h5f9ahvx + (int)1 - 1)) * (_plfm7z8g / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1)))) - _0446f4de);
							}
							
							if (_plfm7z8g < _fh3l9l14)
							_plfm7z8g = _d0547bi2;
							_tt3ji15i = ILNumerics.F2NET.Intrinsics.MIN(_tt3ji15i ,_plfm7z8g );
							_48oov32t = ILNumerics.F2NET.Intrinsics.MIN(_48oov32t ,*(_7e60fcso+(_h5f9ahvx - 1)) );
Mark70:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn8 = (System.Int32)(((int)4 * _kgliup4t));
						System.Int32 __81fgg2step8 = (System.Int32)((int)4);
						System.Int32 __81fgg2count8;
						for (__81fgg2count8 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)4 * (_psb09l5j - (int)3)) - __81fgg2dlsvn8 + __81fgg2step8) / __81fgg2step8)), _h5f9ahvx = __81fgg2dlsvn8; __81fgg2count8 != 0; __81fgg2count8--, _h5f9ahvx += (__81fgg2step8)) {

						{
							
							*(_7e60fcso+(_h5f9ahvx - (int)3 - 1)) = (_plfm7z8g + *(_7e60fcso+(_h5f9ahvx - 1)));
							if (_plfm7z8g < _d0547bi2)
							{
								
								return;
							}
							else
							{
								
								*(_7e60fcso+(_h5f9ahvx - (int)1 - 1)) = (*(_7e60fcso+(_h5f9ahvx + (int)2 - 1)) * (*(_7e60fcso+(_h5f9ahvx - 1)) / *(_7e60fcso+(_h5f9ahvx - (int)3 - 1))));
								_plfm7z8g = ((*(_7e60fcso+(_h5f9ahvx + (int)2 - 1)) * (_plfm7z8g / *(_7e60fcso+(_h5f9ahvx - (int)3 - 1)))) - _0446f4de);
							}
							
							if (_plfm7z8g < _fh3l9l14)
							_plfm7z8g = _d0547bi2;
							_tt3ji15i = ILNumerics.F2NET.Intrinsics.MIN(_tt3ji15i ,_plfm7z8g );
							_48oov32t = ILNumerics.F2NET.Intrinsics.MIN(_48oov32t ,*(_7e60fcso+(_h5f9ahvx - (int)1 - 1)) );
Mark80:;
							// continue
						}
												}					}
				}
				//* 
				//*     Unroll last two steps. 
				//* 
				
				_9vhid3u5 = _plfm7z8g;
				_aaaeq9ec = _tt3ji15i;
				_h5f9ahvx = (((int)4 * (_psb09l5j - (int)2)) - _rk50assb);
				_bwb7am8r = ((_h5f9ahvx + ((int)2 * _rk50assb)) - (int)1);
				*(_7e60fcso+(_h5f9ahvx - (int)2 - 1)) = (_9vhid3u5 + *(_7e60fcso+(_bwb7am8r - 1)));
				if (_9vhid3u5 < _d0547bi2)
				{
					
					return;
				}
				else
				{
					
					*(_7e60fcso+(_h5f9ahvx - 1)) = (*(_7e60fcso+(_bwb7am8r + (int)2 - 1)) * (*(_7e60fcso+(_bwb7am8r - 1)) / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1))));
					_ozgqyi38 = ((*(_7e60fcso+(_bwb7am8r + (int)2 - 1)) * (_9vhid3u5 / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1)))) - _0446f4de);
				}
				
				_tt3ji15i = ILNumerics.F2NET.Intrinsics.MIN(_tt3ji15i ,_ozgqyi38 );//* 
				
				_y61kuds7 = _tt3ji15i;
				_h5f9ahvx = (_h5f9ahvx + (int)4);
				_bwb7am8r = ((_h5f9ahvx + ((int)2 * _rk50assb)) - (int)1);
				*(_7e60fcso+(_h5f9ahvx - (int)2 - 1)) = (_ozgqyi38 + *(_7e60fcso+(_bwb7am8r - 1)));
				if (_ozgqyi38 < _d0547bi2)
				{
					
					return;
				}
				else
				{
					
					*(_7e60fcso+(_h5f9ahvx - 1)) = (*(_7e60fcso+(_bwb7am8r + (int)2 - 1)) * (*(_7e60fcso+(_bwb7am8r - 1)) / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1))));
					_b10nc13b = ((*(_7e60fcso+(_bwb7am8r + (int)2 - 1)) * (_ozgqyi38 / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1)))) - _0446f4de);
				}
				
				_tt3ji15i = ILNumerics.F2NET.Intrinsics.MIN(_tt3ji15i ,_b10nc13b );//* 
				
			}
			
		}
		//* 
		
		*(_7e60fcso+(_h5f9ahvx + (int)2 - 1)) = _b10nc13b;
		*(_7e60fcso+(((int)4 * _psb09l5j) - _rk50assb - 1)) = _48oov32t;
		return;//* 
		//*     End of DLASQ5 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
