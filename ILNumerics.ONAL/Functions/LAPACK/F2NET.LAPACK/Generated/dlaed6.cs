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
//*> \brief \b DLAED6 used by sstedc. Computes one Newton step in solution of the secular equation. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLAED6 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlaed6.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlaed6.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlaed6.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLAED6( KNITER, ORGATI, RHO, D, Z, FINIT, TAU, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       LOGICAL            ORGATI 
//*       INTEGER            INFO, KNITER 
//*       DOUBLE PRECISION   FINIT, RHO, TAU 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   D( 3 ), Z( 3 ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLAED6 computes the positive or negative root (closest to the origin) 
//*> of 
//*>                  z(1)        z(2)        z(3) 
//*> f(x) =   rho + --------- + ---------- + --------- 
//*>                 d(1)-x      d(2)-x      d(3)-x 
//*> 
//*> It is assumed that 
//*> 
//*>       if ORGATI = .true. the root is between d(2) and d(3); 
//*>       otherwise it is between d(1) and d(2) 
//*> 
//*> This routine will be called by DLAED4 when necessary. In most cases, 
//*> the root sought is the smallest in magnitude, though it might not be 
//*> in some extremely rare situations. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] KNITER 
//*> \verbatim 
//*>          KNITER is INTEGER 
//*>               Refer to DLAED4 for its significance. 
//*> \endverbatim 
//*> 
//*> \param[in] ORGATI 
//*> \verbatim 
//*>          ORGATI is LOGICAL 
//*>               If ORGATI is true, the needed root is between d(2) and 
//*>               d(3); otherwise it is between d(1) and d(2).  See 
//*>               DLAED4 for further details. 
//*> \endverbatim 
//*> 
//*> \param[in] RHO 
//*> \verbatim 
//*>          RHO is DOUBLE PRECISION 
//*>               Refer to the equation f(x) above. 
//*> \endverbatim 
//*> 
//*> \param[in] D 
//*> \verbatim 
//*>          D is DOUBLE PRECISION array, dimension (3) 
//*>               D satisfies d(1) < d(2) < d(3). 
//*> \endverbatim 
//*> 
//*> \param[in] Z 
//*> \verbatim 
//*>          Z is DOUBLE PRECISION array, dimension (3) 
//*>               Each of the elements in z must be positive. 
//*> \endverbatim 
//*> 
//*> \param[in] FINIT 
//*> \verbatim 
//*>          FINIT is DOUBLE PRECISION 
//*>               The value of f at 0. It is more accurate than the one 
//*>               evaluated inside this routine (if someone wants to do 
//*>               so). 
//*> \endverbatim 
//*> 
//*> \param[out] TAU 
//*> \verbatim 
//*>          TAU is DOUBLE PRECISION 
//*>               The root of the equation f(x). 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>               = 0: successful exit 
//*>               > 0: if INFO = 1, failure to converge 
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
//*> \ingroup auxOTHERcomputational 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  10/02/03: This version has a few statements commented out for thread 
//*>  safety (machine parameters are computed on each entry). SJH. 
//*> 
//*>  05/10/06: Modified from a new version of Ren-Cang Li, use 
//*>     Gragg-Thornton-Warner cubic convergent scheme for better stability. 
//*> \endverbatim 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>     Ren-Cang Li, Computer Science Division, University of California 
//*>     at Berkeley, USA 
//*> 
//*  ===================================================================== 

	 
	public static void _lx33j0za(ref Int32 _stuc1ajk, ref Boolean _p5ldiiph, ref Double _4qwfue8o, Double* _plfm7z8g, Double* _7e60fcso, ref Double _71leiv8v, ref Double _0446f4de, ref Int32 _gro5yvfo)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)48 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Int32 _gaia76w5 =  (int)40;
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Double _5m0mjfxm =  2d;
Double _08e01ee2 =  3d;
Double _ax5ijvbx =  4d;
Double _2j4711hv =  8d;
Double* _eihj335r =  (Double*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Double) * ((int)3);
Double* _uq4kt02e =  (Double*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Double) * ((int)3);
Boolean _1m44vtuk =  default;
Int32 _b5p6od9s =  default;
Int32 _em7fbywm =  default;
Int32 _00exfor6 =  default;
Double _vxfgpup9 =  default;
Double _p9n405a5 =  default;
Double _mitbs599 =  default;
Double _3crf0qn3 =  default;
Double _k3b2xetz =  default;
Double _yhyatpgz =  default;
Double _p1iqarg6 =  default;
Double _dxv1xfsm =  default;
Double _lr8d81xb =  default;
Double _8plnuphw =  default;
Double _ydskg90g =  default;
Double _9g2ab6cj =  default;
Double _jh9a1d3q =  default;
Double _7evlc1v7 =  default;
Double _b4nonz2l =  default;
Double _c42rmsy1 =  default;
Double _ep1lqg7v =  default;
Double _1ajfmh55 =  default;
Double _yc8h372p =  default;
Double _q3ig7mub =  default;
Double _jbmbnpeh =  default;
Double _v48hty5u =  default;
Double _px76kmep =  default;
Double _q4gwakr5 =  default;
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
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Local Arrays .. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		_gro5yvfo = (int)0;//* 
		
		if (_p5ldiiph)
		{
			
			_px76kmep = *(_plfm7z8g+((int)2 - 1));
			_q4gwakr5 = *(_plfm7z8g+((int)3 - 1));
		}
		else
		{
			
			_px76kmep = *(_plfm7z8g+((int)1 - 1));
			_q4gwakr5 = *(_plfm7z8g+((int)2 - 1));
		}
		
		if (_71leiv8v < _d0547bi2)
		{
			
			_px76kmep = _d0547bi2;
		}
		else
		{
			
			_q4gwakr5 = _d0547bi2;
		}
		//* 
		
		_00exfor6 = (int)1;
		_0446f4de = _d0547bi2;
		if (_stuc1ajk == (int)2)
		{
			
			if (_p5ldiiph)
			{
				
				_1ajfmh55 = ((*(_plfm7z8g+((int)3 - 1)) - *(_plfm7z8g+((int)2 - 1))) / _5m0mjfxm);
				_3crf0qn3 = (_4qwfue8o + (*(_7e60fcso+((int)1 - 1)) / ((*(_plfm7z8g+((int)1 - 1)) - *(_plfm7z8g+((int)2 - 1))) - _1ajfmh55)));
				_vxfgpup9 = (((_3crf0qn3 * (*(_plfm7z8g+((int)2 - 1)) + *(_plfm7z8g+((int)3 - 1)))) + *(_7e60fcso+((int)2 - 1))) + *(_7e60fcso+((int)3 - 1)));
				_p9n405a5 = ((((_3crf0qn3 * *(_plfm7z8g+((int)2 - 1))) * *(_plfm7z8g+((int)3 - 1))) + (*(_7e60fcso+((int)2 - 1)) * *(_plfm7z8g+((int)3 - 1)))) + (*(_7e60fcso+((int)3 - 1)) * *(_plfm7z8g+((int)2 - 1))));
			}
			else
			{
				
				_1ajfmh55 = ((*(_plfm7z8g+((int)1 - 1)) - *(_plfm7z8g+((int)2 - 1))) / _5m0mjfxm);
				_3crf0qn3 = (_4qwfue8o + (*(_7e60fcso+((int)3 - 1)) / ((*(_plfm7z8g+((int)3 - 1)) - *(_plfm7z8g+((int)2 - 1))) - _1ajfmh55)));
				_vxfgpup9 = (((_3crf0qn3 * (*(_plfm7z8g+((int)1 - 1)) + *(_plfm7z8g+((int)2 - 1)))) + *(_7e60fcso+((int)1 - 1))) + *(_7e60fcso+((int)2 - 1)));
				_p9n405a5 = ((((_3crf0qn3 * *(_plfm7z8g+((int)1 - 1))) * *(_plfm7z8g+((int)2 - 1))) + (*(_7e60fcso+((int)1 - 1)) * *(_plfm7z8g+((int)2 - 1)))) + (*(_7e60fcso+((int)2 - 1)) * *(_plfm7z8g+((int)1 - 1))));
			}
			
			_1ajfmh55 = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_vxfgpup9 ) ,ILNumerics.F2NET.Intrinsics.ABS(_p9n405a5 ) ,ILNumerics.F2NET.Intrinsics.ABS(_3crf0qn3 ) );
			_vxfgpup9 = (_vxfgpup9 / _1ajfmh55);
			_p9n405a5 = (_p9n405a5 / _1ajfmh55);
			_3crf0qn3 = (_3crf0qn3 / _1ajfmh55);
			if (_3crf0qn3 == _d0547bi2)
			{
				
				_0446f4de = (_p9n405a5 / _vxfgpup9);
			}
			else
			if (_vxfgpup9 <= _d0547bi2)
			{
				
				_0446f4de = ((_vxfgpup9 - ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS((_vxfgpup9 * _vxfgpup9) - ((_ax5ijvbx * _p9n405a5) * _3crf0qn3) ) )) / (_5m0mjfxm * _3crf0qn3));
			}
			else
			{
				
				_0446f4de = ((_5m0mjfxm * _p9n405a5) / (_vxfgpup9 + ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS((_vxfgpup9 * _vxfgpup9) - ((_ax5ijvbx * _p9n405a5) * _3crf0qn3) ) )));
			}
			
			if ((_0446f4de < _px76kmep) | (_0446f4de > _q4gwakr5))
			_0446f4de = ((_px76kmep + _q4gwakr5) / _5m0mjfxm);
			if (((*(_plfm7z8g+((int)1 - 1)) == _0446f4de) | (*(_plfm7z8g+((int)2 - 1)) == _0446f4de)) | (*(_plfm7z8g+((int)3 - 1)) == _0446f4de))
			{
				
				_0446f4de = _d0547bi2;
			}
			else
			{
				
				_1ajfmh55 = (((_71leiv8v + ((_0446f4de * *(_7e60fcso+((int)1 - 1))) / (*(_plfm7z8g+((int)1 - 1)) * (*(_plfm7z8g+((int)1 - 1)) - _0446f4de)))) + ((_0446f4de * *(_7e60fcso+((int)2 - 1))) / (*(_plfm7z8g+((int)2 - 1)) * (*(_plfm7z8g+((int)2 - 1)) - _0446f4de)))) + ((_0446f4de * *(_7e60fcso+((int)3 - 1))) / (*(_plfm7z8g+((int)3 - 1)) * (*(_plfm7z8g+((int)3 - 1)) - _0446f4de))));
				if (_1ajfmh55 <= _d0547bi2)
				{
					
					_px76kmep = _0446f4de;
				}
				else
				{
					
					_q4gwakr5 = _0446f4de;
				}
				
				if (ILNumerics.F2NET.Intrinsics.ABS(_71leiv8v ) <= ILNumerics.F2NET.Intrinsics.ABS(_1ajfmh55 ))
				_0446f4de = _d0547bi2;
			}
			
		}
		//* 
		//*     get machine parameters for possible scaling to avoid overflow 
		//* 
		//*     modified by Sven: parameters SMALL1, SMINV1, SMALL2, 
		//*     SMINV2, EPS are not SAVEd anymore between one call to the 
		//*     others but recomputed at each call 
		//* 
		
		_p1iqarg6 = _f43eg0w0("Epsilon" );
		_mitbs599 = _f43eg0w0("Base" );
		_7evlc1v7 = __POW(_mitbs599, (ILNumerics.F2NET.Intrinsics.INT((ILNumerics.F2NET.Intrinsics.LOG(_f43eg0w0("SafMin" ) ) / ILNumerics.F2NET.Intrinsics.LOG(_mitbs599 )) / _08e01ee2 )));
		_c42rmsy1 = (_kxg5drh2 / _7evlc1v7);
		_b4nonz2l = (_7evlc1v7 * _7evlc1v7);
		_ep1lqg7v = (_c42rmsy1 * _c42rmsy1);//* 
		//*     Determine if scaling of inputs necessary to avoid overflow 
		//*     when computing 1/TEMP**3 
		//* 
		
		if (_p5ldiiph)
		{
			
			_1ajfmh55 = ILNumerics.F2NET.Intrinsics.MIN(ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+((int)2 - 1)) - _0446f4de ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+((int)3 - 1)) - _0446f4de ) );
		}
		else
		{
			
			_1ajfmh55 = ILNumerics.F2NET.Intrinsics.MIN(ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+((int)1 - 1)) - _0446f4de ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+((int)2 - 1)) - _0446f4de ) );
		}
		
		_1m44vtuk = false;
		if (_1ajfmh55 <= _7evlc1v7)
		{
			
			_1m44vtuk = true;
			if (_1ajfmh55 <= _b4nonz2l)
			{
				//* 
				//*        Scale up by power of radix nearest 1/SAFMIN**(2/3) 
				//* 
				
				_9g2ab6cj = _ep1lqg7v;
				_jh9a1d3q = _b4nonz2l;
			}
			else
			{
				//* 
				//*        Scale up by power of radix nearest 1/SAFMIN**(1/3) 
				//* 
				
				_9g2ab6cj = _c42rmsy1;
				_jh9a1d3q = _7evlc1v7;
			}
			//* 
			//*        Scaling up safe because D, Z, TAU scaled elsewhere to be O(1) 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn272 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step272 = (System.Int32)((int)1);
				System.Int32 __81fgg2count272;
				for (__81fgg2count272 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)3) - __81fgg2dlsvn272 + __81fgg2step272) / __81fgg2step272)), _b5p6od9s = __81fgg2dlsvn272; __81fgg2count272 != 0; __81fgg2count272--, _b5p6od9s += (__81fgg2step272)) {

				{
					
					*(_eihj335r+(_b5p6od9s - 1)) = (*(_plfm7z8g+(_b5p6od9s - 1)) * _9g2ab6cj);
					*(_uq4kt02e+(_b5p6od9s - 1)) = (*(_7e60fcso+(_b5p6od9s - 1)) * _9g2ab6cj);
Mark10:;
					// continue
				}
								}			}
			_0446f4de = (_0446f4de * _9g2ab6cj);
			_px76kmep = (_px76kmep * _9g2ab6cj);
			_q4gwakr5 = (_q4gwakr5 * _9g2ab6cj);
		}
		else
		{
			//* 
			//*        Copy D and Z to DSCALE and ZSCALE 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn273 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step273 = (System.Int32)((int)1);
				System.Int32 __81fgg2count273;
				for (__81fgg2count273 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)3) - __81fgg2dlsvn273 + __81fgg2step273) / __81fgg2step273)), _b5p6od9s = __81fgg2dlsvn273; __81fgg2count273 != 0; __81fgg2count273--, _b5p6od9s += (__81fgg2step273)) {

				{
					
					*(_eihj335r+(_b5p6od9s - 1)) = *(_plfm7z8g+(_b5p6od9s - 1));
					*(_uq4kt02e+(_b5p6od9s - 1)) = *(_7e60fcso+(_b5p6od9s - 1));
Mark20:;
					// continue
				}
								}			}
		}
		//* 
		
		_ydskg90g = _d0547bi2;
		_yhyatpgz = _d0547bi2;
		_k3b2xetz = _d0547bi2;
		{
			System.Int32 __81fgg2dlsvn274 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step274 = (System.Int32)((int)1);
			System.Int32 __81fgg2count274;
			for (__81fgg2count274 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)3) - __81fgg2dlsvn274 + __81fgg2step274) / __81fgg2step274)), _b5p6od9s = __81fgg2dlsvn274; __81fgg2count274 != 0; __81fgg2count274--, _b5p6od9s += (__81fgg2step274)) {

			{
				
				_1ajfmh55 = (_kxg5drh2 / (*(_eihj335r+(_b5p6od9s - 1)) - _0446f4de));
				_yc8h372p = (*(_uq4kt02e+(_b5p6od9s - 1)) * _1ajfmh55);
				_q3ig7mub = (_yc8h372p * _1ajfmh55);
				_jbmbnpeh = (_q3ig7mub * _1ajfmh55);
				_ydskg90g = (_ydskg90g + (_yc8h372p / *(_eihj335r+(_b5p6od9s - 1))));
				_yhyatpgz = (_yhyatpgz + _q3ig7mub);
				_k3b2xetz = (_k3b2xetz + _jbmbnpeh);
Mark30:;
				// continue
			}
						}		}
		_8plnuphw = (_71leiv8v + (_0446f4de * _ydskg90g));//* 
		
		if (ILNumerics.F2NET.Intrinsics.ABS(_8plnuphw ) <= _d0547bi2)goto Mark60;
		if (_8plnuphw <= _d0547bi2)
		{
			
			_px76kmep = _0446f4de;
		}
		else
		{
			
			_q4gwakr5 = _0446f4de;
		}
		//* 
		//*        Iteration begins -- Use Gragg-Thornton-Warner cubic convergent 
		//*                            scheme 
		//* 
		//*     It is not hard to see that 
		//* 
		//*           1) Iterations will go up monotonically 
		//*              if FINIT < 0; 
		//* 
		//*           2) Iterations will go down monotonically 
		//*              if FINIT > 0. 
		//* 
		
		_em7fbywm = (_00exfor6 + (int)1);//* 
		
		{
			System.Int32 __81fgg2dlsvn275 = (System.Int32)(_em7fbywm);
			const System.Int32 __81fgg2step275 = (System.Int32)((int)1);
			System.Int32 __81fgg2count275;
			for (__81fgg2count275 = System.Math.Max(0, (System.Int32)(((System.Int32)(_gaia76w5) - __81fgg2dlsvn275 + __81fgg2step275) / __81fgg2step275)), _00exfor6 = __81fgg2dlsvn275; __81fgg2count275 != 0; __81fgg2count275--, _00exfor6 += (__81fgg2step275)) {

			{
				//* 
				
				if (_p5ldiiph)
				{
					
					_yc8h372p = (*(_eihj335r+((int)2 - 1)) - _0446f4de);
					_q3ig7mub = (*(_eihj335r+((int)3 - 1)) - _0446f4de);
				}
				else
				{
					
					_yc8h372p = (*(_eihj335r+((int)1 - 1)) - _0446f4de);
					_q3ig7mub = (*(_eihj335r+((int)2 - 1)) - _0446f4de);
				}
				
				_vxfgpup9 = (((_yc8h372p + _q3ig7mub) * _8plnuphw) - ((_yc8h372p * _q3ig7mub) * _yhyatpgz));
				_p9n405a5 = ((_yc8h372p * _q3ig7mub) * _8plnuphw);
				_3crf0qn3 = ((_8plnuphw - ((_yc8h372p + _q3ig7mub) * _yhyatpgz)) + ((_yc8h372p * _q3ig7mub) * _k3b2xetz));
				_1ajfmh55 = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_vxfgpup9 ) ,ILNumerics.F2NET.Intrinsics.ABS(_p9n405a5 ) ,ILNumerics.F2NET.Intrinsics.ABS(_3crf0qn3 ) );
				_vxfgpup9 = (_vxfgpup9 / _1ajfmh55);
				_p9n405a5 = (_p9n405a5 / _1ajfmh55);
				_3crf0qn3 = (_3crf0qn3 / _1ajfmh55);
				if (_3crf0qn3 == _d0547bi2)
				{
					
					_lr8d81xb = (_p9n405a5 / _vxfgpup9);
				}
				else
				if (_vxfgpup9 <= _d0547bi2)
				{
					
					_lr8d81xb = ((_vxfgpup9 - ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS((_vxfgpup9 * _vxfgpup9) - ((_ax5ijvbx * _p9n405a5) * _3crf0qn3) ) )) / (_5m0mjfxm * _3crf0qn3));
				}
				else
				{
					
					_lr8d81xb = ((_5m0mjfxm * _p9n405a5) / (_vxfgpup9 + ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS((_vxfgpup9 * _vxfgpup9) - ((_ax5ijvbx * _p9n405a5) * _3crf0qn3) ) )));
				}
				
				if ((_8plnuphw * _lr8d81xb) >= _d0547bi2)
				{
					
					_lr8d81xb = (-((_8plnuphw / _yhyatpgz)));
				}
				//* 
				
				_0446f4de = (_0446f4de + _lr8d81xb);
				if ((_0446f4de < _px76kmep) | (_0446f4de > _q4gwakr5))
				_0446f4de = ((_px76kmep + _q4gwakr5) / _5m0mjfxm);//* 
				
				_ydskg90g = _d0547bi2;
				_dxv1xfsm = _d0547bi2;
				_yhyatpgz = _d0547bi2;
				_k3b2xetz = _d0547bi2;
				{
					System.Int32 __81fgg2dlsvn276 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step276 = (System.Int32)((int)1);
					System.Int32 __81fgg2count276;
					for (__81fgg2count276 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)3) - __81fgg2dlsvn276 + __81fgg2step276) / __81fgg2step276)), _b5p6od9s = __81fgg2dlsvn276; __81fgg2count276 != 0; __81fgg2count276--, _b5p6od9s += (__81fgg2step276)) {

					{
						
						if ((*(_eihj335r+(_b5p6od9s - 1)) - _0446f4de) != _d0547bi2)
						{
							
							_1ajfmh55 = (_kxg5drh2 / (*(_eihj335r+(_b5p6od9s - 1)) - _0446f4de));
							_yc8h372p = (*(_uq4kt02e+(_b5p6od9s - 1)) * _1ajfmh55);
							_q3ig7mub = (_yc8h372p * _1ajfmh55);
							_jbmbnpeh = (_q3ig7mub * _1ajfmh55);
							_v48hty5u = (_yc8h372p / *(_eihj335r+(_b5p6od9s - 1)));
							_ydskg90g = (_ydskg90g + _v48hty5u);
							_dxv1xfsm = (_dxv1xfsm + ILNumerics.F2NET.Intrinsics.ABS(_v48hty5u ));
							_yhyatpgz = (_yhyatpgz + _q3ig7mub);
							_k3b2xetz = (_k3b2xetz + _jbmbnpeh);
						}
						else
						{
							goto Mark60;
						}
						
Mark40:;
						// continue
					}
										}				}
				_8plnuphw = (_71leiv8v + (_0446f4de * _ydskg90g));
				_dxv1xfsm = ((_2j4711hv * (ILNumerics.F2NET.Intrinsics.ABS(_71leiv8v ) + (ILNumerics.F2NET.Intrinsics.ABS(_0446f4de ) * _dxv1xfsm))) + (ILNumerics.F2NET.Intrinsics.ABS(_0446f4de ) * _yhyatpgz));
				if ((ILNumerics.F2NET.Intrinsics.ABS(_8plnuphw ) <= ((_ax5ijvbx * _p1iqarg6) * _dxv1xfsm)) | ((_q4gwakr5 - _px76kmep) <= ((_ax5ijvbx * _p1iqarg6) * ILNumerics.F2NET.Intrinsics.ABS(_0446f4de ))))goto Mark60;
				if (_8plnuphw <= _d0547bi2)
				{
					
					_px76kmep = _0446f4de;
				}
				else
				{
					
					_q4gwakr5 = _0446f4de;
				}
				
Mark50:;
				// continue
			}
						}		}
		_gro5yvfo = (int)1;
Mark60:;
		// continue//* 
		//*     Undo scaling 
		//* 
		
		if (_1m44vtuk)
		_0446f4de = (_0446f4de * _jh9a1d3q);
		return;//* 
		//*     End of DLAED6 
		//* 
		
	}
	
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
