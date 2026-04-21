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
//*> \brief \b SLASD4 computes the square root of the i-th updated eigenvalue of a positive symmetric rank-one modification to a positive diagonal matrix. Used by sbdsdc. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLASD4 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slasd4.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slasd4.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slasd4.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLASD4( N, I, D, Z, DELTA, RHO, SIGMA, WORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            I, INFO, N 
//*       REAL               RHO, SIGMA 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               D( * ), DELTA( * ), WORK( * ), Z( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> This subroutine computes the square root of the I-th updated 
//*> eigenvalue of a positive symmetric rank-one modification to 
//*> a positive diagonal matrix whose entries are given as the squares 
//*> of the corresponding entries in the array d, and that 
//*> 
//*>        0 <= D(i) < D(j)  for  i < j 
//*> 
//*> and that RHO > 0. This is arranged by the calling routine, and is 
//*> no loss in generality.  The rank-one modified system is thus 
//*> 
//*>        diag( D ) * diag( D ) +  RHO * Z * Z_transpose. 
//*> 
//*> where we assume the Euclidean norm of Z is 1. 
//*> 
//*> The method consists of approximating the rational functions in the 
//*> secular equation by simpler interpolating rational functions. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>         The length of all arrays. 
//*> \endverbatim 
//*> 
//*> \param[in] I 
//*> \verbatim 
//*>          I is INTEGER 
//*>         The index of the eigenvalue to be computed.  1 <= I <= N. 
//*> \endverbatim 
//*> 
//*> \param[in] D 
//*> \verbatim 
//*>          D is REAL array, dimension ( N ) 
//*>         The original eigenvalues.  It is assumed that they are in 
//*>         order, 0 <= D(I) < D(J)  for I < J. 
//*> \endverbatim 
//*> 
//*> \param[in] Z 
//*> \verbatim 
//*>          Z is REAL array, dimension ( N ) 
//*>         The components of the updating vector. 
//*> \endverbatim 
//*> 
//*> \param[out] DELTA 
//*> \verbatim 
//*>          DELTA is REAL array, dimension ( N ) 
//*>         If N .ne. 1, DELTA contains (D(j) - sigma_I) in its  j-th 
//*>         component.  If N = 1, then DELTA(1) = 1.  The vector DELTA 
//*>         contains the information necessary to construct the 
//*>         (singular) eigenvectors. 
//*> \endverbatim 
//*> 
//*> \param[in] RHO 
//*> \verbatim 
//*>          RHO is REAL 
//*>         The scalar in the symmetric updating formula. 
//*> \endverbatim 
//*> 
//*> \param[out] SIGMA 
//*> \verbatim 
//*>          SIGMA is REAL 
//*>         The computed sigma_I, the I-th updated eigenvalue. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is REAL array, dimension ( N ) 
//*>         If N .ne. 1, WORK contains (D(j) + sigma_I) in its  j-th 
//*>         component.  If N = 1, then WORK( 1 ) = 1. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>         = 0:  successful exit 
//*>         > 0:  if INFO = 1, the updating process failed. 
//*> \endverbatim 
//* 
//*> \par Internal Parameters: 
//*  ========================= 
//*> 
//*> \verbatim 
//*>  Logical variable ORGATI (origin-at-i?) is used for distinguishing 
//*>  whether D(i) or D(i+1) is treated as the origin. 
//*> 
//*>            ORGATI = .true.    origin at i 
//*>            ORGATI = .false.   origin at i+1 
//*> 
//*>  Logical variable SWTCH3 (switch-for-3-poles?) is for noting 
//*>  if we are working with THREE poles! 
//*> 
//*>  MAXIT is the maximum number of iterations allowed for each 
//*>  eigenvalue. 
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
//*> \par Contributors: 
//*  ================== 
//*> 
//*>     Ren-Cang Li, Computer Science Division, University of California 
//*>     at Berkeley, USA 
//*> 
//*  ===================================================================== 

	 
	public static void _bnshiskn(ref Int32 _dxpq0xkr, ref Int32 _b5p6od9s, Single* _plfm7z8g, Single* _7e60fcso, Single* _9zhf8o7p, ref Single _4qwfue8o, ref Single _91a1vq5f, Single* _apig8meb, ref Int32 _gro5yvfo)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)24 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Int32 _gaia76w5 =  (int)400;
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
Single _5m0mjfxm =  2f;
Single _08e01ee2 =  3f;
Single _ax5ijvbx =  4f;
Single _2j4711hv =  8f;
Single _76g572q1 =  10f;
Boolean _p5ldiiph =  default;
Boolean _x6inc284 =  default;
Boolean _upfltzfu =  default;
Boolean _9vda0187 =  default;
Int32 _retbwjxi =  default;
Int32 _snl53at2 =  default;
Int32 _hxummyuf =  default;
Int32 _8lksxei0 =  default;
Int32 _em7fbywm =  default;
Int32 _znpjgsef =  default;
Int32 _00exfor6 =  default;
Single _vxfgpup9 =  default;
Single _p9n405a5 =  default;
Single _3crf0qn3 =  default;
Single _8k4zqcbt =  default;
Single _5md5e3hl =  default;
Single _a7coqqkn =  default;
Single _evfc66x3 =  default;
Single _gxob35y5 =  default;
Single _k1wkbmv7 =  default;
Single _s3whlcqr =  default;
Single _aqu578x9 =  default;
Single _zkzqogre =  default;
Single _jhey49m1 =  default;
Single _9itcjprd =  default;
Single _h6amk27m =  default;
Single _p1iqarg6 =  default;
Single _dxv1xfsm =  default;
Single _lr8d81xb =  default;
Single _e26zouzj =  default;
Single _2vjsff4q =  default;
Single _k2ugnue4 =  default;
Single _0h01rnfp =  default;
Single _j9hr6i7t =  default;
Single _8l8b6ahg =  default;
Single _0446f4de =  default;
Single _xs3ggrxu =  default;
Single _1ajfmh55 =  default;
Single _yc8h372p =  default;
Single _q3ig7mub =  default;
Single _z1ioc3c8 =  default;
Single* _f4rvsg6o =  (Single*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Single) * ((int)3);
Single* _4bicvd70 =  (Single*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Single) * ((int)3);
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
		//*     .. Local Arrays .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Since this routine is called in an inner loop, we do no argument 
		//*     checking. 
		//* 
		//*     Quick return for N=1 and 2. 
		//* 
		
		_gro5yvfo = (int)0;
		if (_dxpq0xkr == (int)1)
		{
			//* 
			//*        Presumably, I=1 upon entry 
			//* 
			
			_91a1vq5f = ILNumerics.F2NET.Intrinsics.SQRT((*(_plfm7z8g+((int)1 - 1)) * *(_plfm7z8g+((int)1 - 1))) + ((_4qwfue8o * *(_7e60fcso+((int)1 - 1))) * *(_7e60fcso+((int)1 - 1))) );
			*(_9zhf8o7p+((int)1 - 1)) = _kxg5drh2;
			*(_apig8meb+((int)1 - 1)) = _kxg5drh2;
			return;
		}
		
		if (_dxpq0xkr == (int)2)
		{
			
			_xg1krgaf(ref _b5p6od9s ,_plfm7z8g ,_7e60fcso ,_9zhf8o7p ,ref _4qwfue8o ,ref _91a1vq5f ,_apig8meb );
			return;
		}
		//* 
		//*     Compute machine epsilon 
		//* 
		
		_p1iqarg6 = _d5tu038y("Epsilon" );
		_0h01rnfp = (_kxg5drh2 / _4qwfue8o);
		_xs3ggrxu = _d0547bi2;//* 
		//*     The case I = N 
		//* 
		
		if (_b5p6od9s == _dxpq0xkr)
		{
			//* 
			//*        Initialize some basic variables 
			//* 
			
			_retbwjxi = (_dxpq0xkr - (int)1);
			_00exfor6 = (int)1;//* 
			//*        Calculate initial guess 
			//* 
			
			_1ajfmh55 = (_4qwfue8o / _5m0mjfxm);//* 
			//*        If ||Z||_2 is not one, then TEMP should be set to 
			//*        RHO * ||Z||_2^2 / TWO 
			//* 
			
			_yc8h372p = (_1ajfmh55 / (*(_plfm7z8g+(_dxpq0xkr - 1)) + ILNumerics.F2NET.Intrinsics.SQRT((*(_plfm7z8g+(_dxpq0xkr - 1)) * *(_plfm7z8g+(_dxpq0xkr - 1))) + _1ajfmh55 )));
			{
				System.Int32 __81fgg2dlsvn617 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step617 = (System.Int32)((int)1);
				System.Int32 __81fgg2count617;
				for (__81fgg2count617 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn617 + __81fgg2step617) / __81fgg2step617)), _znpjgsef = __81fgg2dlsvn617; __81fgg2count617 != 0; __81fgg2count617--, _znpjgsef += (__81fgg2step617)) {

				{
					
					*(_apig8meb+(_znpjgsef - 1)) = ((*(_plfm7z8g+(_znpjgsef - 1)) + *(_plfm7z8g+(_dxpq0xkr - 1))) + _yc8h372p);
					*(_9zhf8o7p+(_znpjgsef - 1)) = ((*(_plfm7z8g+(_znpjgsef - 1)) - *(_plfm7z8g+(_dxpq0xkr - 1))) - _yc8h372p);
Mark10:;
					// continue
				}
								}			}//* 
			
			_k2ugnue4 = _d0547bi2;
			{
				System.Int32 __81fgg2dlsvn618 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step618 = (System.Int32)((int)1);
				System.Int32 __81fgg2count618;
				for (__81fgg2count618 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)2) - __81fgg2dlsvn618 + __81fgg2step618) / __81fgg2step618)), _znpjgsef = __81fgg2dlsvn618; __81fgg2count618 != 0; __81fgg2count618--, _znpjgsef += (__81fgg2step618)) {

				{
					
					_k2ugnue4 = (_k2ugnue4 + ((*(_7e60fcso+(_znpjgsef - 1)) * *(_7e60fcso+(_znpjgsef - 1))) / (*(_9zhf8o7p+(_znpjgsef - 1)) * *(_apig8meb+(_znpjgsef - 1)))));
Mark20:;
					// continue
				}
								}			}//* 
			
			_3crf0qn3 = (_0h01rnfp + _k2ugnue4);
			_z1ioc3c8 = ((_3crf0qn3 + ((*(_7e60fcso+(_retbwjxi - 1)) * *(_7e60fcso+(_retbwjxi - 1))) / (*(_9zhf8o7p+(_retbwjxi - 1)) * *(_apig8meb+(_retbwjxi - 1))))) + ((*(_7e60fcso+(_dxpq0xkr - 1)) * *(_7e60fcso+(_dxpq0xkr - 1))) / (*(_9zhf8o7p+(_dxpq0xkr - 1)) * *(_apig8meb+(_dxpq0xkr - 1)))));//* 
			
			if (_z1ioc3c8 <= _d0547bi2)
			{
				
				_yc8h372p = ILNumerics.F2NET.Intrinsics.SQRT((*(_plfm7z8g+(_dxpq0xkr - 1)) * *(_plfm7z8g+(_dxpq0xkr - 1))) + _4qwfue8o );
				_1ajfmh55 = (((*(_7e60fcso+(_dxpq0xkr - (int)1 - 1)) * *(_7e60fcso+(_dxpq0xkr - (int)1 - 1))) / ((*(_plfm7z8g+(_dxpq0xkr - (int)1 - 1)) + _yc8h372p) * ((*(_plfm7z8g+(_dxpq0xkr - 1)) - *(_plfm7z8g+(_dxpq0xkr - (int)1 - 1))) + (_4qwfue8o / (*(_plfm7z8g+(_dxpq0xkr - 1)) + _yc8h372p))))) + ((*(_7e60fcso+(_dxpq0xkr - 1)) * *(_7e60fcso+(_dxpq0xkr - 1))) / _4qwfue8o));//* 
				//*           The following TAU2 is to approximate 
				//*           SIGMA_n^2 - D( N )*D( N ) 
				//* 
				
				if (_3crf0qn3 <= _1ajfmh55)
				{
					
					_0446f4de = _4qwfue8o;
				}
				else
				{
					
					_8k4zqcbt = ((*(_plfm7z8g+(_dxpq0xkr - 1)) - *(_plfm7z8g+(_dxpq0xkr - (int)1 - 1))) * (*(_plfm7z8g+(_dxpq0xkr - 1)) + *(_plfm7z8g+(_dxpq0xkr - (int)1 - 1))));
					_vxfgpup9 = (((-((_3crf0qn3 * _8k4zqcbt))) + (*(_7e60fcso+(_dxpq0xkr - (int)1 - 1)) * *(_7e60fcso+(_dxpq0xkr - (int)1 - 1)))) + (*(_7e60fcso+(_dxpq0xkr - 1)) * *(_7e60fcso+(_dxpq0xkr - 1))));
					_p9n405a5 = ((*(_7e60fcso+(_dxpq0xkr - 1)) * *(_7e60fcso+(_dxpq0xkr - 1))) * _8k4zqcbt);
					if (_vxfgpup9 < _d0547bi2)
					{
						
						_xs3ggrxu = ((_5m0mjfxm * _p9n405a5) / (ILNumerics.F2NET.Intrinsics.SQRT((_vxfgpup9 * _vxfgpup9) + ((_ax5ijvbx * _p9n405a5) * _3crf0qn3) ) - _vxfgpup9));
					}
					else
					{
						
						_xs3ggrxu = ((_vxfgpup9 + ILNumerics.F2NET.Intrinsics.SQRT((_vxfgpup9 * _vxfgpup9) + ((_ax5ijvbx * _p9n405a5) * _3crf0qn3) )) / (_5m0mjfxm * _3crf0qn3));
					}
					
					_0446f4de = (_xs3ggrxu / (*(_plfm7z8g+(_dxpq0xkr - 1)) + ILNumerics.F2NET.Intrinsics.SQRT((*(_plfm7z8g+(_dxpq0xkr - 1)) * *(_plfm7z8g+(_dxpq0xkr - 1))) + _xs3ggrxu )));
				}
				//* 
				//*           It can be proved that 
				//*               D(N)^2+RHO/2 <= SIGMA_n^2 < D(N)^2+TAU2 <= D(N)^2+RHO 
				//* 
				
			}
			else
			{
				
				_8k4zqcbt = ((*(_plfm7z8g+(_dxpq0xkr - 1)) - *(_plfm7z8g+(_dxpq0xkr - (int)1 - 1))) * (*(_plfm7z8g+(_dxpq0xkr - 1)) + *(_plfm7z8g+(_dxpq0xkr - (int)1 - 1))));
				_vxfgpup9 = (((-((_3crf0qn3 * _8k4zqcbt))) + (*(_7e60fcso+(_dxpq0xkr - (int)1 - 1)) * *(_7e60fcso+(_dxpq0xkr - (int)1 - 1)))) + (*(_7e60fcso+(_dxpq0xkr - 1)) * *(_7e60fcso+(_dxpq0xkr - 1))));
				_p9n405a5 = ((*(_7e60fcso+(_dxpq0xkr - 1)) * *(_7e60fcso+(_dxpq0xkr - 1))) * _8k4zqcbt);//* 
				//*           The following TAU2 is to approximate 
				//*           SIGMA_n^2 - D( N )*D( N ) 
				//* 
				
				if (_vxfgpup9 < _d0547bi2)
				{
					
					_xs3ggrxu = ((_5m0mjfxm * _p9n405a5) / (ILNumerics.F2NET.Intrinsics.SQRT((_vxfgpup9 * _vxfgpup9) + ((_ax5ijvbx * _p9n405a5) * _3crf0qn3) ) - _vxfgpup9));
				}
				else
				{
					
					_xs3ggrxu = ((_vxfgpup9 + ILNumerics.F2NET.Intrinsics.SQRT((_vxfgpup9 * _vxfgpup9) + ((_ax5ijvbx * _p9n405a5) * _3crf0qn3) )) / (_5m0mjfxm * _3crf0qn3));
				}
				
				_0446f4de = (_xs3ggrxu / (*(_plfm7z8g+(_dxpq0xkr - 1)) + ILNumerics.F2NET.Intrinsics.SQRT((*(_plfm7z8g+(_dxpq0xkr - 1)) * *(_plfm7z8g+(_dxpq0xkr - 1))) + _xs3ggrxu )));// 
				//* 
				//*           It can be proved that 
				//*           D(N)^2 < D(N)^2+TAU2 < SIGMA(N)^2 < D(N)^2+RHO/2 
				//* 
				
			}
			//* 
			//*        The following TAU is to approximate SIGMA_n - D( N ) 
			//* 
			//*         TAU = TAU2 / ( D( N )+SQRT( D( N )*D( N )+TAU2 ) ) 
			//* 
			
			_91a1vq5f = (*(_plfm7z8g+(_dxpq0xkr - 1)) + _0446f4de);
			{
				System.Int32 __81fgg2dlsvn619 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step619 = (System.Int32)((int)1);
				System.Int32 __81fgg2count619;
				for (__81fgg2count619 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn619 + __81fgg2step619) / __81fgg2step619)), _znpjgsef = __81fgg2dlsvn619; __81fgg2count619 != 0; __81fgg2count619--, _znpjgsef += (__81fgg2step619)) {

				{
					
					*(_9zhf8o7p+(_znpjgsef - 1)) = ((*(_plfm7z8g+(_znpjgsef - 1)) - *(_plfm7z8g+(_dxpq0xkr - 1))) - _0446f4de);
					*(_apig8meb+(_znpjgsef - 1)) = ((*(_plfm7z8g+(_znpjgsef - 1)) + *(_plfm7z8g+(_dxpq0xkr - 1))) + _0446f4de);
Mark30:;
					// continue
				}
								}			}//* 
			//*        Evaluate PSI and the derivative DPSI 
			//* 
			
			_gxob35y5 = _d0547bi2;
			_k2ugnue4 = _d0547bi2;
			_dxv1xfsm = _d0547bi2;
			{
				System.Int32 __81fgg2dlsvn620 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step620 = (System.Int32)((int)1);
				System.Int32 __81fgg2count620;
				for (__81fgg2count620 = System.Math.Max(0, (System.Int32)(((System.Int32)(_retbwjxi) - __81fgg2dlsvn620 + __81fgg2step620) / __81fgg2step620)), _znpjgsef = __81fgg2dlsvn620; __81fgg2count620 != 0; __81fgg2count620--, _znpjgsef += (__81fgg2step620)) {

				{
					
					_1ajfmh55 = (*(_7e60fcso+(_znpjgsef - 1)) / (*(_9zhf8o7p+(_znpjgsef - 1)) * *(_apig8meb+(_znpjgsef - 1))));
					_k2ugnue4 = (_k2ugnue4 + (*(_7e60fcso+(_znpjgsef - 1)) * _1ajfmh55));
					_gxob35y5 = (_gxob35y5 + (_1ajfmh55 * _1ajfmh55));
					_dxv1xfsm = (_dxv1xfsm + _k2ugnue4);
Mark40:;
					// continue
				}
								}			}
			_dxv1xfsm = ILNumerics.F2NET.Intrinsics.ABS(_dxv1xfsm );//* 
			//*        Evaluate PHI and the derivative DPHI 
			//* 
			
			_1ajfmh55 = (*(_7e60fcso+(_dxpq0xkr - 1)) / (*(_9zhf8o7p+(_dxpq0xkr - 1)) * *(_apig8meb+(_dxpq0xkr - 1))));
			_e26zouzj = (*(_7e60fcso+(_dxpq0xkr - 1)) * _1ajfmh55);
			_evfc66x3 = (_1ajfmh55 * _1ajfmh55);
			_dxv1xfsm = ((((_2j4711hv * ((-(_e26zouzj)) - _k2ugnue4)) + _dxv1xfsm) - _e26zouzj) + _0h01rnfp);//*    $          + ABS( TAU2 )*( DPSI+DPHI ) 
			//* 
			
			_z1ioc3c8 = ((_0h01rnfp + _e26zouzj) + _k2ugnue4);//* 
			//*        Test for convergence 
			//* 
			
			if (ILNumerics.F2NET.Intrinsics.ABS(_z1ioc3c8 ) <= (_p1iqarg6 * _dxv1xfsm))
			{
				goto Mark240;
			}
			//* 
			//*        Calculate the new step 
			//* 
			
			_00exfor6 = (_00exfor6 + (int)1);
			_9itcjprd = (*(_apig8meb+(_dxpq0xkr - (int)1 - 1)) * *(_9zhf8o7p+(_dxpq0xkr - (int)1 - 1)));
			_jhey49m1 = (*(_apig8meb+(_dxpq0xkr - 1)) * *(_9zhf8o7p+(_dxpq0xkr - 1)));
			_3crf0qn3 = ((_z1ioc3c8 - (_9itcjprd * _gxob35y5)) - (_jhey49m1 * _evfc66x3));
			_vxfgpup9 = (((_jhey49m1 + _9itcjprd) * _z1ioc3c8) - ((_jhey49m1 * _9itcjprd) * (_gxob35y5 + _evfc66x3)));
			_p9n405a5 = ((_jhey49m1 * _9itcjprd) * _z1ioc3c8);
			if (_3crf0qn3 < _d0547bi2)
			_3crf0qn3 = ILNumerics.F2NET.Intrinsics.ABS(_3crf0qn3 );
			if (_3crf0qn3 == _d0547bi2)
			{
				
				_lr8d81xb = (_4qwfue8o - (_91a1vq5f * _91a1vq5f));
			}
			else
			if (_vxfgpup9 >= _d0547bi2)
			{
				
				_lr8d81xb = ((_vxfgpup9 + ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS((_vxfgpup9 * _vxfgpup9) - ((_ax5ijvbx * _p9n405a5) * _3crf0qn3) ) )) / (_5m0mjfxm * _3crf0qn3));
			}
			else
			{
				
				_lr8d81xb = ((_5m0mjfxm * _p9n405a5) / (_vxfgpup9 - ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS((_vxfgpup9 * _vxfgpup9) - ((_ax5ijvbx * _p9n405a5) * _3crf0qn3) ) )));
			}
			//* 
			//*        Note, eta should be positive if w is negative, and 
			//*        eta should be negative otherwise. However, 
			//*        if for some reason caused by roundoff, eta*w > 0, 
			//*        we simply use one Newton step instead. This way 
			//*        will guarantee eta*w < 0. 
			//* 
			
			if ((_z1ioc3c8 * _lr8d81xb) > _d0547bi2)
			_lr8d81xb = (-((_z1ioc3c8 / (_gxob35y5 + _evfc66x3))));
			_1ajfmh55 = (_lr8d81xb - _jhey49m1);
			if (_1ajfmh55 > _4qwfue8o)
			_lr8d81xb = (_4qwfue8o + _jhey49m1);//* 
			
			_lr8d81xb = (_lr8d81xb / (_91a1vq5f + ILNumerics.F2NET.Intrinsics.SQRT(_lr8d81xb + (_91a1vq5f * _91a1vq5f) )));
			_0446f4de = (_0446f4de + _lr8d81xb);
			_91a1vq5f = (_91a1vq5f + _lr8d81xb);//* 
			
			{
				System.Int32 __81fgg2dlsvn621 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step621 = (System.Int32)((int)1);
				System.Int32 __81fgg2count621;
				for (__81fgg2count621 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn621 + __81fgg2step621) / __81fgg2step621)), _znpjgsef = __81fgg2dlsvn621; __81fgg2count621 != 0; __81fgg2count621--, _znpjgsef += (__81fgg2step621)) {

				{
					
					*(_9zhf8o7p+(_znpjgsef - 1)) = (*(_9zhf8o7p+(_znpjgsef - 1)) - _lr8d81xb);
					*(_apig8meb+(_znpjgsef - 1)) = (*(_apig8meb+(_znpjgsef - 1)) + _lr8d81xb);
Mark50:;
					// continue
				}
								}			}//* 
			//*        Evaluate PSI and the derivative DPSI 
			//* 
			
			_gxob35y5 = _d0547bi2;
			_k2ugnue4 = _d0547bi2;
			_dxv1xfsm = _d0547bi2;
			{
				System.Int32 __81fgg2dlsvn622 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step622 = (System.Int32)((int)1);
				System.Int32 __81fgg2count622;
				for (__81fgg2count622 = System.Math.Max(0, (System.Int32)(((System.Int32)(_retbwjxi) - __81fgg2dlsvn622 + __81fgg2step622) / __81fgg2step622)), _znpjgsef = __81fgg2dlsvn622; __81fgg2count622 != 0; __81fgg2count622--, _znpjgsef += (__81fgg2step622)) {

				{
					
					_1ajfmh55 = (*(_7e60fcso+(_znpjgsef - 1)) / (*(_apig8meb+(_znpjgsef - 1)) * *(_9zhf8o7p+(_znpjgsef - 1))));
					_k2ugnue4 = (_k2ugnue4 + (*(_7e60fcso+(_znpjgsef - 1)) * _1ajfmh55));
					_gxob35y5 = (_gxob35y5 + (_1ajfmh55 * _1ajfmh55));
					_dxv1xfsm = (_dxv1xfsm + _k2ugnue4);
Mark60:;
					// continue
				}
								}			}
			_dxv1xfsm = ILNumerics.F2NET.Intrinsics.ABS(_dxv1xfsm );//* 
			//*        Evaluate PHI and the derivative DPHI 
			//* 
			
			_xs3ggrxu = (*(_apig8meb+(_dxpq0xkr - 1)) * *(_9zhf8o7p+(_dxpq0xkr - 1)));
			_1ajfmh55 = (*(_7e60fcso+(_dxpq0xkr - 1)) / _xs3ggrxu);
			_e26zouzj = (*(_7e60fcso+(_dxpq0xkr - 1)) * _1ajfmh55);
			_evfc66x3 = (_1ajfmh55 * _1ajfmh55);
			_dxv1xfsm = ((((_2j4711hv * ((-(_e26zouzj)) - _k2ugnue4)) + _dxv1xfsm) - _e26zouzj) + _0h01rnfp);//*    $          + ABS( TAU2 )*( DPSI+DPHI ) 
			//* 
			
			_z1ioc3c8 = ((_0h01rnfp + _e26zouzj) + _k2ugnue4);//* 
			//*        Main loop to update the values of the array   DELTA 
			//* 
			
			_em7fbywm = (_00exfor6 + (int)1);//* 
			
			{
				System.Int32 __81fgg2dlsvn623 = (System.Int32)(_em7fbywm);
				const System.Int32 __81fgg2step623 = (System.Int32)((int)1);
				System.Int32 __81fgg2count623;
				for (__81fgg2count623 = System.Math.Max(0, (System.Int32)(((System.Int32)(_gaia76w5) - __81fgg2dlsvn623 + __81fgg2step623) / __81fgg2step623)), _00exfor6 = __81fgg2dlsvn623; __81fgg2count623 != 0; __81fgg2count623--, _00exfor6 += (__81fgg2step623)) {

				{
					//* 
					//*           Test for convergence 
					//* 
					
					if (ILNumerics.F2NET.Intrinsics.ABS(_z1ioc3c8 ) <= (_p1iqarg6 * _dxv1xfsm))
					{
						goto Mark240;
					}
					//* 
					//*           Calculate the new step 
					//* 
					
					_9itcjprd = (*(_apig8meb+(_dxpq0xkr - (int)1 - 1)) * *(_9zhf8o7p+(_dxpq0xkr - (int)1 - 1)));
					_jhey49m1 = (*(_apig8meb+(_dxpq0xkr - 1)) * *(_9zhf8o7p+(_dxpq0xkr - 1)));
					_3crf0qn3 = ((_z1ioc3c8 - (_9itcjprd * _gxob35y5)) - (_jhey49m1 * _evfc66x3));
					_vxfgpup9 = (((_jhey49m1 + _9itcjprd) * _z1ioc3c8) - ((_9itcjprd * _jhey49m1) * (_gxob35y5 + _evfc66x3)));
					_p9n405a5 = ((_9itcjprd * _jhey49m1) * _z1ioc3c8);
					if (_vxfgpup9 >= _d0547bi2)
					{
						
						_lr8d81xb = ((_vxfgpup9 + ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS((_vxfgpup9 * _vxfgpup9) - ((_ax5ijvbx * _p9n405a5) * _3crf0qn3) ) )) / (_5m0mjfxm * _3crf0qn3));
					}
					else
					{
						
						_lr8d81xb = ((_5m0mjfxm * _p9n405a5) / (_vxfgpup9 - ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS((_vxfgpup9 * _vxfgpup9) - ((_ax5ijvbx * _p9n405a5) * _3crf0qn3) ) )));
					}
					//* 
					//*           Note, eta should be positive if w is negative, and 
					//*           eta should be negative otherwise. However, 
					//*           if for some reason caused by roundoff, eta*w > 0, 
					//*           we simply use one Newton step instead. This way 
					//*           will guarantee eta*w < 0. 
					//* 
					
					if ((_z1ioc3c8 * _lr8d81xb) > _d0547bi2)
					_lr8d81xb = (-((_z1ioc3c8 / (_gxob35y5 + _evfc66x3))));
					_1ajfmh55 = (_lr8d81xb - _jhey49m1);
					if (_1ajfmh55 <= _d0547bi2)
					_lr8d81xb = (_lr8d81xb / _5m0mjfxm);//* 
					
					_lr8d81xb = (_lr8d81xb / (_91a1vq5f + ILNumerics.F2NET.Intrinsics.SQRT(_lr8d81xb + (_91a1vq5f * _91a1vq5f) )));
					_0446f4de = (_0446f4de + _lr8d81xb);
					_91a1vq5f = (_91a1vq5f + _lr8d81xb);//* 
					
					{
						System.Int32 __81fgg2dlsvn624 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step624 = (System.Int32)((int)1);
						System.Int32 __81fgg2count624;
						for (__81fgg2count624 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn624 + __81fgg2step624) / __81fgg2step624)), _znpjgsef = __81fgg2dlsvn624; __81fgg2count624 != 0; __81fgg2count624--, _znpjgsef += (__81fgg2step624)) {

						{
							
							*(_9zhf8o7p+(_znpjgsef - 1)) = (*(_9zhf8o7p+(_znpjgsef - 1)) - _lr8d81xb);
							*(_apig8meb+(_znpjgsef - 1)) = (*(_apig8meb+(_znpjgsef - 1)) + _lr8d81xb);
Mark70:;
							// continue
						}
												}					}//* 
					//*           Evaluate PSI and the derivative DPSI 
					//* 
					
					_gxob35y5 = _d0547bi2;
					_k2ugnue4 = _d0547bi2;
					_dxv1xfsm = _d0547bi2;
					{
						System.Int32 __81fgg2dlsvn625 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step625 = (System.Int32)((int)1);
						System.Int32 __81fgg2count625;
						for (__81fgg2count625 = System.Math.Max(0, (System.Int32)(((System.Int32)(_retbwjxi) - __81fgg2dlsvn625 + __81fgg2step625) / __81fgg2step625)), _znpjgsef = __81fgg2dlsvn625; __81fgg2count625 != 0; __81fgg2count625--, _znpjgsef += (__81fgg2step625)) {

						{
							
							_1ajfmh55 = (*(_7e60fcso+(_znpjgsef - 1)) / (*(_apig8meb+(_znpjgsef - 1)) * *(_9zhf8o7p+(_znpjgsef - 1))));
							_k2ugnue4 = (_k2ugnue4 + (*(_7e60fcso+(_znpjgsef - 1)) * _1ajfmh55));
							_gxob35y5 = (_gxob35y5 + (_1ajfmh55 * _1ajfmh55));
							_dxv1xfsm = (_dxv1xfsm + _k2ugnue4);
Mark80:;
							// continue
						}
												}					}
					_dxv1xfsm = ILNumerics.F2NET.Intrinsics.ABS(_dxv1xfsm );//* 
					//*           Evaluate PHI and the derivative DPHI 
					//* 
					
					_xs3ggrxu = (*(_apig8meb+(_dxpq0xkr - 1)) * *(_9zhf8o7p+(_dxpq0xkr - 1)));
					_1ajfmh55 = (*(_7e60fcso+(_dxpq0xkr - 1)) / _xs3ggrxu);
					_e26zouzj = (*(_7e60fcso+(_dxpq0xkr - 1)) * _1ajfmh55);
					_evfc66x3 = (_1ajfmh55 * _1ajfmh55);
					_dxv1xfsm = ((((_2j4711hv * ((-(_e26zouzj)) - _k2ugnue4)) + _dxv1xfsm) - _e26zouzj) + _0h01rnfp);//*    $             + ABS( TAU2 )*( DPSI+DPHI ) 
					//* 
					
					_z1ioc3c8 = ((_0h01rnfp + _e26zouzj) + _k2ugnue4);
Mark90:;
					// continue
				}
								}			}//* 
			//*        Return with INFO = 1, NITER = MAXIT and not converged 
			//* 
			
			_gro5yvfo = (int)1;goto Mark240;//* 
			//*        End for the case I = N 
			//* 
			
		}
		else
		{
			//* 
			//*        The case for I < N 
			//* 
			
			_00exfor6 = (int)1;
			_8lksxei0 = (_b5p6od9s + (int)1);//* 
			//*        Calculate initial guess 
			//* 
			
			_8k4zqcbt = ((*(_plfm7z8g+(_8lksxei0 - 1)) - *(_plfm7z8g+(_b5p6od9s - 1))) * (*(_plfm7z8g+(_8lksxei0 - 1)) + *(_plfm7z8g+(_b5p6od9s - 1))));
			_5md5e3hl = (_8k4zqcbt / _5m0mjfxm);
			_a7coqqkn = ILNumerics.F2NET.Intrinsics.SQRT(((*(_plfm7z8g+(_b5p6od9s - 1)) * *(_plfm7z8g+(_b5p6od9s - 1))) + (*(_plfm7z8g+(_8lksxei0 - 1)) * *(_plfm7z8g+(_8lksxei0 - 1)))) / _5m0mjfxm );
			_1ajfmh55 = (_5md5e3hl / (*(_plfm7z8g+(_b5p6od9s - 1)) + _a7coqqkn));
			{
				System.Int32 __81fgg2dlsvn626 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step626 = (System.Int32)((int)1);
				System.Int32 __81fgg2count626;
				for (__81fgg2count626 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn626 + __81fgg2step626) / __81fgg2step626)), _znpjgsef = __81fgg2dlsvn626; __81fgg2count626 != 0; __81fgg2count626--, _znpjgsef += (__81fgg2step626)) {

				{
					
					*(_apig8meb+(_znpjgsef - 1)) = ((*(_plfm7z8g+(_znpjgsef - 1)) + *(_plfm7z8g+(_b5p6od9s - 1))) + _1ajfmh55);
					*(_9zhf8o7p+(_znpjgsef - 1)) = ((*(_plfm7z8g+(_znpjgsef - 1)) - *(_plfm7z8g+(_b5p6od9s - 1))) - _1ajfmh55);
Mark100:;
					// continue
				}
								}			}//* 
			
			_k2ugnue4 = _d0547bi2;
			{
				System.Int32 __81fgg2dlsvn627 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step627 = (System.Int32)((int)1);
				System.Int32 __81fgg2count627;
				for (__81fgg2count627 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn627 + __81fgg2step627) / __81fgg2step627)), _znpjgsef = __81fgg2dlsvn627; __81fgg2count627 != 0; __81fgg2count627--, _znpjgsef += (__81fgg2step627)) {

				{
					
					_k2ugnue4 = (_k2ugnue4 + ((*(_7e60fcso+(_znpjgsef - 1)) * *(_7e60fcso+(_znpjgsef - 1))) / (*(_apig8meb+(_znpjgsef - 1)) * *(_9zhf8o7p+(_znpjgsef - 1)))));
Mark110:;
					// continue
				}
								}			}//* 
			
			_e26zouzj = _d0547bi2;
			{
				System.Int32 __81fgg2dlsvn628 = (System.Int32)(_dxpq0xkr);
				System.Int32 __81fgg2step628 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count628;
				for (__81fgg2count628 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s + (int)2) - __81fgg2dlsvn628 + __81fgg2step628) / __81fgg2step628)), _znpjgsef = __81fgg2dlsvn628; __81fgg2count628 != 0; __81fgg2count628--, _znpjgsef += (__81fgg2step628)) {

				{
					
					_e26zouzj = (_e26zouzj + ((*(_7e60fcso+(_znpjgsef - 1)) * *(_7e60fcso+(_znpjgsef - 1))) / (*(_apig8meb+(_znpjgsef - 1)) * *(_9zhf8o7p+(_znpjgsef - 1)))));
Mark120:;
					// continue
				}
								}			}
			_3crf0qn3 = ((_0h01rnfp + _k2ugnue4) + _e26zouzj);
			_z1ioc3c8 = ((_3crf0qn3 + ((*(_7e60fcso+(_b5p6od9s - 1)) * *(_7e60fcso+(_b5p6od9s - 1))) / (*(_apig8meb+(_b5p6od9s - 1)) * *(_9zhf8o7p+(_b5p6od9s - 1))))) + ((*(_7e60fcso+(_8lksxei0 - 1)) * *(_7e60fcso+(_8lksxei0 - 1))) / (*(_apig8meb+(_8lksxei0 - 1)) * *(_9zhf8o7p+(_8lksxei0 - 1)))));//* 
			
			_9vda0187 = false;
			if (_z1ioc3c8 > _d0547bi2)
			{
				//* 
				//*           d(i)^2 < the ith sigma^2 < (d(i)^2+d(i+1)^2)/2 
				//* 
				//*           We choose d(i) as origin. 
				//* 
				
				_p5ldiiph = true;
				_retbwjxi = _b5p6od9s;
				_j9hr6i7t = _d0547bi2;
				_8l8b6ahg = (_5md5e3hl / (*(_plfm7z8g+(_b5p6od9s - 1)) + _a7coqqkn));
				_vxfgpup9 = (((_3crf0qn3 * _8k4zqcbt) + (*(_7e60fcso+(_b5p6od9s - 1)) * *(_7e60fcso+(_b5p6od9s - 1)))) + (*(_7e60fcso+(_8lksxei0 - 1)) * *(_7e60fcso+(_8lksxei0 - 1))));
				_p9n405a5 = ((*(_7e60fcso+(_b5p6od9s - 1)) * *(_7e60fcso+(_b5p6od9s - 1))) * _8k4zqcbt);
				if (_vxfgpup9 > _d0547bi2)
				{
					
					_xs3ggrxu = ((_5m0mjfxm * _p9n405a5) / (_vxfgpup9 + ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS((_vxfgpup9 * _vxfgpup9) - ((_ax5ijvbx * _p9n405a5) * _3crf0qn3) ) )));
				}
				else
				{
					
					_xs3ggrxu = ((_vxfgpup9 - ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS((_vxfgpup9 * _vxfgpup9) - ((_ax5ijvbx * _p9n405a5) * _3crf0qn3) ) )) / (_5m0mjfxm * _3crf0qn3));
				}
				//* 
				//*           TAU2 now is an estimation of SIGMA^2 - D( I )^2. The 
				//*           following, however, is the corresponding estimation of 
				//*           SIGMA - D( I ). 
				//* 
				
				_0446f4de = (_xs3ggrxu / (*(_plfm7z8g+(_b5p6od9s - 1)) + ILNumerics.F2NET.Intrinsics.SQRT((*(_plfm7z8g+(_b5p6od9s - 1)) * *(_plfm7z8g+(_b5p6od9s - 1))) + _xs3ggrxu )));
				_1ajfmh55 = ILNumerics.F2NET.Intrinsics.SQRT(_p1iqarg6 );
				if (((*(_plfm7z8g+(_b5p6od9s - 1)) <= (_1ajfmh55 * *(_plfm7z8g+(_8lksxei0 - 1)))) & (ILNumerics.F2NET.Intrinsics.ABS(*(_7e60fcso+(_b5p6od9s - 1)) ) <= _1ajfmh55)) & (*(_plfm7z8g+(_b5p6od9s - 1)) > _d0547bi2))
				{
					
					_0446f4de = ILNumerics.F2NET.Intrinsics.MIN(_76g572q1 * *(_plfm7z8g+(_b5p6od9s - 1)) ,_8l8b6ahg );
					_9vda0187 = true;
				}
				
			}
			else
			{
				//* 
				//*           (d(i)^2+d(i+1)^2)/2 <= the ith sigma^2 < d(i+1)^2/2 
				//* 
				//*           We choose d(i+1) as origin. 
				//* 
				
				_p5ldiiph = false;
				_retbwjxi = _8lksxei0;
				_j9hr6i7t = (-((_5md5e3hl / (*(_plfm7z8g+(_retbwjxi - 1)) + _a7coqqkn))));
				_8l8b6ahg = _d0547bi2;
				_vxfgpup9 = (((_3crf0qn3 * _8k4zqcbt) - (*(_7e60fcso+(_b5p6od9s - 1)) * *(_7e60fcso+(_b5p6od9s - 1)))) - (*(_7e60fcso+(_8lksxei0 - 1)) * *(_7e60fcso+(_8lksxei0 - 1))));
				_p9n405a5 = ((*(_7e60fcso+(_8lksxei0 - 1)) * *(_7e60fcso+(_8lksxei0 - 1))) * _8k4zqcbt);
				if (_vxfgpup9 < _d0547bi2)
				{
					
					_xs3ggrxu = ((_5m0mjfxm * _p9n405a5) / (_vxfgpup9 - ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS((_vxfgpup9 * _vxfgpup9) + ((_ax5ijvbx * _p9n405a5) * _3crf0qn3) ) )));
				}
				else
				{
					
					_xs3ggrxu = (-(((_vxfgpup9 + ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS((_vxfgpup9 * _vxfgpup9) + ((_ax5ijvbx * _p9n405a5) * _3crf0qn3) ) )) / (_5m0mjfxm * _3crf0qn3))));
				}
				//* 
				//*           TAU2 now is an estimation of SIGMA^2 - D( IP1 )^2. The 
				//*           following, however, is the corresponding estimation of 
				//*           SIGMA - D( IP1 ). 
				//* 
				
				_0446f4de = (_xs3ggrxu / (*(_plfm7z8g+(_8lksxei0 - 1)) + ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS((*(_plfm7z8g+(_8lksxei0 - 1)) * *(_plfm7z8g+(_8lksxei0 - 1))) + _xs3ggrxu ) )));
			}
			//* 
			
			_91a1vq5f = (*(_plfm7z8g+(_retbwjxi - 1)) + _0446f4de);
			{
				System.Int32 __81fgg2dlsvn629 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step629 = (System.Int32)((int)1);
				System.Int32 __81fgg2count629;
				for (__81fgg2count629 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn629 + __81fgg2step629) / __81fgg2step629)), _znpjgsef = __81fgg2dlsvn629; __81fgg2count629 != 0; __81fgg2count629--, _znpjgsef += (__81fgg2step629)) {

				{
					
					*(_apig8meb+(_znpjgsef - 1)) = ((*(_plfm7z8g+(_znpjgsef - 1)) + *(_plfm7z8g+(_retbwjxi - 1))) + _0446f4de);
					*(_9zhf8o7p+(_znpjgsef - 1)) = ((*(_plfm7z8g+(_znpjgsef - 1)) - *(_plfm7z8g+(_retbwjxi - 1))) - _0446f4de);
Mark130:;
					// continue
				}
								}			}
			_snl53at2 = (_retbwjxi - (int)1);
			_hxummyuf = (_retbwjxi + (int)1);//* 
			//*        Evaluate PSI and the derivative DPSI 
			//* 
			
			_gxob35y5 = _d0547bi2;
			_k2ugnue4 = _d0547bi2;
			_dxv1xfsm = _d0547bi2;
			{
				System.Int32 __81fgg2dlsvn630 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step630 = (System.Int32)((int)1);
				System.Int32 __81fgg2count630;
				for (__81fgg2count630 = System.Math.Max(0, (System.Int32)(((System.Int32)(_snl53at2) - __81fgg2dlsvn630 + __81fgg2step630) / __81fgg2step630)), _znpjgsef = __81fgg2dlsvn630; __81fgg2count630 != 0; __81fgg2count630--, _znpjgsef += (__81fgg2step630)) {

				{
					
					_1ajfmh55 = (*(_7e60fcso+(_znpjgsef - 1)) / (*(_apig8meb+(_znpjgsef - 1)) * *(_9zhf8o7p+(_znpjgsef - 1))));
					_k2ugnue4 = (_k2ugnue4 + (*(_7e60fcso+(_znpjgsef - 1)) * _1ajfmh55));
					_gxob35y5 = (_gxob35y5 + (_1ajfmh55 * _1ajfmh55));
					_dxv1xfsm = (_dxv1xfsm + _k2ugnue4);
Mark150:;
					// continue
				}
								}			}
			_dxv1xfsm = ILNumerics.F2NET.Intrinsics.ABS(_dxv1xfsm );//* 
			//*        Evaluate PHI and the derivative DPHI 
			//* 
			
			_evfc66x3 = _d0547bi2;
			_e26zouzj = _d0547bi2;
			{
				System.Int32 __81fgg2dlsvn631 = (System.Int32)(_dxpq0xkr);
				System.Int32 __81fgg2step631 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count631;
				for (__81fgg2count631 = System.Math.Max(0, (System.Int32)(((System.Int32)(_hxummyuf) - __81fgg2dlsvn631 + __81fgg2step631) / __81fgg2step631)), _znpjgsef = __81fgg2dlsvn631; __81fgg2count631 != 0; __81fgg2count631--, _znpjgsef += (__81fgg2step631)) {

				{
					
					_1ajfmh55 = (*(_7e60fcso+(_znpjgsef - 1)) / (*(_apig8meb+(_znpjgsef - 1)) * *(_9zhf8o7p+(_znpjgsef - 1))));
					_e26zouzj = (_e26zouzj + (*(_7e60fcso+(_znpjgsef - 1)) * _1ajfmh55));
					_evfc66x3 = (_evfc66x3 + (_1ajfmh55 * _1ajfmh55));
					_dxv1xfsm = (_dxv1xfsm + _e26zouzj);
Mark160:;
					// continue
				}
								}			}//* 
			
			_z1ioc3c8 = ((_0h01rnfp + _e26zouzj) + _k2ugnue4);//* 
			//*        W is the value of the secular function with 
			//*        its ii-th element removed. 
			//* 
			
			_upfltzfu = false;
			if (_p5ldiiph)
			{
				
				if (_z1ioc3c8 < _d0547bi2)
				_upfltzfu = true;
			}
			else
			{
				
				if (_z1ioc3c8 > _d0547bi2)
				_upfltzfu = true;
			}
			
			if ((_retbwjxi == (int)1) | (_retbwjxi == _dxpq0xkr))
			_upfltzfu = false;//* 
			
			_1ajfmh55 = (*(_7e60fcso+(_retbwjxi - 1)) / (*(_apig8meb+(_retbwjxi - 1)) * *(_9zhf8o7p+(_retbwjxi - 1))));
			_h6amk27m = ((_gxob35y5 + _evfc66x3) + (_1ajfmh55 * _1ajfmh55));
			_1ajfmh55 = (*(_7e60fcso+(_retbwjxi - 1)) * _1ajfmh55);
			_z1ioc3c8 = (_z1ioc3c8 + _1ajfmh55);
			_dxv1xfsm = ((((_2j4711hv * (_e26zouzj - _k2ugnue4)) + _dxv1xfsm) + (_5m0mjfxm * _0h01rnfp)) + (_08e01ee2 * ILNumerics.F2NET.Intrinsics.ABS(_1ajfmh55 )));//*    $          + ABS( TAU2 )*DW 
			//* 
			//*        Test for convergence 
			//* 
			
			if (ILNumerics.F2NET.Intrinsics.ABS(_z1ioc3c8 ) <= (_p1iqarg6 * _dxv1xfsm))
			{
				goto Mark240;
			}
			//* 
			
			if (_z1ioc3c8 <= _d0547bi2)
			{
				
				_j9hr6i7t = ILNumerics.F2NET.Intrinsics.MAX(_j9hr6i7t ,_0446f4de );
			}
			else
			{
				
				_8l8b6ahg = ILNumerics.F2NET.Intrinsics.MIN(_8l8b6ahg ,_0446f4de );
			}
			//* 
			//*        Calculate the new step 
			//* 
			
			_00exfor6 = (_00exfor6 + (int)1);
			if (!(_upfltzfu))
			{
				
				_aqu578x9 = (*(_apig8meb+(_8lksxei0 - 1)) * *(_9zhf8o7p+(_8lksxei0 - 1)));
				_zkzqogre = (*(_apig8meb+(_b5p6od9s - 1)) * *(_9zhf8o7p+(_b5p6od9s - 1)));
				if (_p5ldiiph)
				{
					
					_3crf0qn3 = ((_z1ioc3c8 - (_aqu578x9 * _h6amk27m)) + (_8k4zqcbt * __POW2((*(_7e60fcso+(_b5p6od9s - 1)) / _zkzqogre))));
				}
				else
				{
					
					_3crf0qn3 = ((_z1ioc3c8 - (_zkzqogre * _h6amk27m)) - (_8k4zqcbt * __POW2((*(_7e60fcso+(_8lksxei0 - 1)) / _aqu578x9))));
				}
				
				_vxfgpup9 = (((_aqu578x9 + _zkzqogre) * _z1ioc3c8) - ((_aqu578x9 * _zkzqogre) * _h6amk27m));
				_p9n405a5 = ((_aqu578x9 * _zkzqogre) * _z1ioc3c8);
				if (_3crf0qn3 == _d0547bi2)
				{
					
					if (_vxfgpup9 == _d0547bi2)
					{
						
						if (_p5ldiiph)
						{
							
							_vxfgpup9 = ((*(_7e60fcso+(_b5p6od9s - 1)) * *(_7e60fcso+(_b5p6od9s - 1))) + ((_aqu578x9 * _aqu578x9) * (_gxob35y5 + _evfc66x3)));
						}
						else
						{
							
							_vxfgpup9 = ((*(_7e60fcso+(_8lksxei0 - 1)) * *(_7e60fcso+(_8lksxei0 - 1))) + ((_zkzqogre * _zkzqogre) * (_gxob35y5 + _evfc66x3)));
						}
						
					}
					
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
				
			}
			else
			{
				//* 
				//*           Interpolation using THREE most relevant poles 
				//* 
				
				_k1wkbmv7 = (*(_apig8meb+(_snl53at2 - 1)) * *(_9zhf8o7p+(_snl53at2 - 1)));
				_s3whlcqr = (*(_apig8meb+(_hxummyuf - 1)) * *(_9zhf8o7p+(_hxummyuf - 1)));
				_1ajfmh55 = ((_0h01rnfp + _k2ugnue4) + _e26zouzj);
				if (_p5ldiiph)
				{
					
					_yc8h372p = (*(_7e60fcso+(_snl53at2 - 1)) / _k1wkbmv7);
					_yc8h372p = (_yc8h372p * _yc8h372p);
					_3crf0qn3 = ((_1ajfmh55 - (_s3whlcqr * (_gxob35y5 + _evfc66x3))) - (((*(_plfm7z8g+(_snl53at2 - 1)) - *(_plfm7z8g+(_hxummyuf - 1))) * (*(_plfm7z8g+(_snl53at2 - 1)) + *(_plfm7z8g+(_hxummyuf - 1)))) * _yc8h372p));
					*(_4bicvd70+((int)1 - 1)) = (*(_7e60fcso+(_snl53at2 - 1)) * *(_7e60fcso+(_snl53at2 - 1)));
					if (_gxob35y5 < _yc8h372p)
					{
						
						*(_4bicvd70+((int)3 - 1)) = ((_s3whlcqr * _s3whlcqr) * _evfc66x3);
					}
					else
					{
						
						*(_4bicvd70+((int)3 - 1)) = ((_s3whlcqr * _s3whlcqr) * ((_gxob35y5 - _yc8h372p) + _evfc66x3));
					}
					
				}
				else
				{
					
					_yc8h372p = (*(_7e60fcso+(_hxummyuf - 1)) / _s3whlcqr);
					_yc8h372p = (_yc8h372p * _yc8h372p);
					_3crf0qn3 = ((_1ajfmh55 - (_k1wkbmv7 * (_gxob35y5 + _evfc66x3))) - (((*(_plfm7z8g+(_hxummyuf - 1)) - *(_plfm7z8g+(_snl53at2 - 1))) * (*(_plfm7z8g+(_snl53at2 - 1)) + *(_plfm7z8g+(_hxummyuf - 1)))) * _yc8h372p));
					if (_evfc66x3 < _yc8h372p)
					{
						
						*(_4bicvd70+((int)1 - 1)) = ((_k1wkbmv7 * _k1wkbmv7) * _gxob35y5);
					}
					else
					{
						
						*(_4bicvd70+((int)1 - 1)) = ((_k1wkbmv7 * _k1wkbmv7) * (_gxob35y5 + (_evfc66x3 - _yc8h372p)));
					}
					
					*(_4bicvd70+((int)3 - 1)) = (*(_7e60fcso+(_hxummyuf - 1)) * *(_7e60fcso+(_hxummyuf - 1)));
				}
				
				*(_4bicvd70+((int)2 - 1)) = (*(_7e60fcso+(_retbwjxi - 1)) * *(_7e60fcso+(_retbwjxi - 1)));
				*(_f4rvsg6o+((int)1 - 1)) = _k1wkbmv7;
				*(_f4rvsg6o+((int)2 - 1)) = (*(_9zhf8o7p+(_retbwjxi - 1)) * *(_apig8meb+(_retbwjxi - 1)));
				*(_f4rvsg6o+((int)3 - 1)) = _s3whlcqr;
				_mca1c6k9(ref _00exfor6 ,ref _p5ldiiph ,ref _3crf0qn3 ,_f4rvsg6o ,_4bicvd70 ,ref _z1ioc3c8 ,ref _lr8d81xb ,ref _gro5yvfo );//* 
				
				if (_gro5yvfo != (int)0)
				{
					//* 
					//*              If INFO is not 0, i.e., SLAED6 failed, switch back 
					//*              to 2 pole interpolation. 
					//* 
					
					_upfltzfu = false;
					_gro5yvfo = (int)0;
					_aqu578x9 = (*(_apig8meb+(_8lksxei0 - 1)) * *(_9zhf8o7p+(_8lksxei0 - 1)));
					_zkzqogre = (*(_apig8meb+(_b5p6od9s - 1)) * *(_9zhf8o7p+(_b5p6od9s - 1)));
					if (_p5ldiiph)
					{
						
						_3crf0qn3 = ((_z1ioc3c8 - (_aqu578x9 * _h6amk27m)) + (_8k4zqcbt * __POW2((*(_7e60fcso+(_b5p6od9s - 1)) / _zkzqogre))));
					}
					else
					{
						
						_3crf0qn3 = ((_z1ioc3c8 - (_zkzqogre * _h6amk27m)) - (_8k4zqcbt * __POW2((*(_7e60fcso+(_8lksxei0 - 1)) / _aqu578x9))));
					}
					
					_vxfgpup9 = (((_aqu578x9 + _zkzqogre) * _z1ioc3c8) - ((_aqu578x9 * _zkzqogre) * _h6amk27m));
					_p9n405a5 = ((_aqu578x9 * _zkzqogre) * _z1ioc3c8);
					if (_3crf0qn3 == _d0547bi2)
					{
						
						if (_vxfgpup9 == _d0547bi2)
						{
							
							if (_p5ldiiph)
							{
								
								_vxfgpup9 = ((*(_7e60fcso+(_b5p6od9s - 1)) * *(_7e60fcso+(_b5p6od9s - 1))) + ((_aqu578x9 * _aqu578x9) * (_gxob35y5 + _evfc66x3)));
							}
							else
							{
								
								_vxfgpup9 = ((*(_7e60fcso+(_8lksxei0 - 1)) * *(_7e60fcso+(_8lksxei0 - 1))) + ((_zkzqogre * _zkzqogre) * (_gxob35y5 + _evfc66x3)));
							}
							
						}
						
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
					
				}
				
			}
			//* 
			//*        Note, eta should be positive if w is negative, and 
			//*        eta should be negative otherwise. However, 
			//*        if for some reason caused by roundoff, eta*w > 0, 
			//*        we simply use one Newton step instead. This way 
			//*        will guarantee eta*w < 0. 
			//* 
			
			if ((_z1ioc3c8 * _lr8d81xb) >= _d0547bi2)
			_lr8d81xb = (-((_z1ioc3c8 / _h6amk27m)));//* 
			
			_lr8d81xb = (_lr8d81xb / (_91a1vq5f + ILNumerics.F2NET.Intrinsics.SQRT((_91a1vq5f * _91a1vq5f) + _lr8d81xb )));
			_1ajfmh55 = (_0446f4de + _lr8d81xb);
			if ((_1ajfmh55 > _8l8b6ahg) | (_1ajfmh55 < _j9hr6i7t))
			{
				
				if (_z1ioc3c8 < _d0547bi2)
				{
					
					_lr8d81xb = ((_8l8b6ahg - _0446f4de) / _5m0mjfxm);
				}
				else
				{
					
					_lr8d81xb = ((_j9hr6i7t - _0446f4de) / _5m0mjfxm);
				}
				
				if (_9vda0187)
				{
					
					if (_z1ioc3c8 < _d0547bi2)
					{
						
						if (_0446f4de > _d0547bi2)
						{
							
							_lr8d81xb = (ILNumerics.F2NET.Intrinsics.SQRT(_8l8b6ahg * _0446f4de ) - _0446f4de);
						}
						
					}
					else
					{
						
						if (_j9hr6i7t > _d0547bi2)
						{
							
							_lr8d81xb = (ILNumerics.F2NET.Intrinsics.SQRT(_j9hr6i7t * _0446f4de ) - _0446f4de);
						}
						
					}
					
				}
				
			}
			//* 
			
			_2vjsff4q = _z1ioc3c8;//* 
			
			_0446f4de = (_0446f4de + _lr8d81xb);
			_91a1vq5f = (_91a1vq5f + _lr8d81xb);//* 
			
			{
				System.Int32 __81fgg2dlsvn632 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step632 = (System.Int32)((int)1);
				System.Int32 __81fgg2count632;
				for (__81fgg2count632 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn632 + __81fgg2step632) / __81fgg2step632)), _znpjgsef = __81fgg2dlsvn632; __81fgg2count632 != 0; __81fgg2count632--, _znpjgsef += (__81fgg2step632)) {

				{
					
					*(_apig8meb+(_znpjgsef - 1)) = (*(_apig8meb+(_znpjgsef - 1)) + _lr8d81xb);
					*(_9zhf8o7p+(_znpjgsef - 1)) = (*(_9zhf8o7p+(_znpjgsef - 1)) - _lr8d81xb);
Mark170:;
					// continue
				}
								}			}//* 
			//*        Evaluate PSI and the derivative DPSI 
			//* 
			
			_gxob35y5 = _d0547bi2;
			_k2ugnue4 = _d0547bi2;
			_dxv1xfsm = _d0547bi2;
			{
				System.Int32 __81fgg2dlsvn633 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step633 = (System.Int32)((int)1);
				System.Int32 __81fgg2count633;
				for (__81fgg2count633 = System.Math.Max(0, (System.Int32)(((System.Int32)(_snl53at2) - __81fgg2dlsvn633 + __81fgg2step633) / __81fgg2step633)), _znpjgsef = __81fgg2dlsvn633; __81fgg2count633 != 0; __81fgg2count633--, _znpjgsef += (__81fgg2step633)) {

				{
					
					_1ajfmh55 = (*(_7e60fcso+(_znpjgsef - 1)) / (*(_apig8meb+(_znpjgsef - 1)) * *(_9zhf8o7p+(_znpjgsef - 1))));
					_k2ugnue4 = (_k2ugnue4 + (*(_7e60fcso+(_znpjgsef - 1)) * _1ajfmh55));
					_gxob35y5 = (_gxob35y5 + (_1ajfmh55 * _1ajfmh55));
					_dxv1xfsm = (_dxv1xfsm + _k2ugnue4);
Mark180:;
					// continue
				}
								}			}
			_dxv1xfsm = ILNumerics.F2NET.Intrinsics.ABS(_dxv1xfsm );//* 
			//*        Evaluate PHI and the derivative DPHI 
			//* 
			
			_evfc66x3 = _d0547bi2;
			_e26zouzj = _d0547bi2;
			{
				System.Int32 __81fgg2dlsvn634 = (System.Int32)(_dxpq0xkr);
				System.Int32 __81fgg2step634 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count634;
				for (__81fgg2count634 = System.Math.Max(0, (System.Int32)(((System.Int32)(_hxummyuf) - __81fgg2dlsvn634 + __81fgg2step634) / __81fgg2step634)), _znpjgsef = __81fgg2dlsvn634; __81fgg2count634 != 0; __81fgg2count634--, _znpjgsef += (__81fgg2step634)) {

				{
					
					_1ajfmh55 = (*(_7e60fcso+(_znpjgsef - 1)) / (*(_apig8meb+(_znpjgsef - 1)) * *(_9zhf8o7p+(_znpjgsef - 1))));
					_e26zouzj = (_e26zouzj + (*(_7e60fcso+(_znpjgsef - 1)) * _1ajfmh55));
					_evfc66x3 = (_evfc66x3 + (_1ajfmh55 * _1ajfmh55));
					_dxv1xfsm = (_dxv1xfsm + _e26zouzj);
Mark190:;
					// continue
				}
								}			}//* 
			
			_xs3ggrxu = (*(_apig8meb+(_retbwjxi - 1)) * *(_9zhf8o7p+(_retbwjxi - 1)));
			_1ajfmh55 = (*(_7e60fcso+(_retbwjxi - 1)) / _xs3ggrxu);
			_h6amk27m = ((_gxob35y5 + _evfc66x3) + (_1ajfmh55 * _1ajfmh55));
			_1ajfmh55 = (*(_7e60fcso+(_retbwjxi - 1)) * _1ajfmh55);
			_z1ioc3c8 = (((_0h01rnfp + _e26zouzj) + _k2ugnue4) + _1ajfmh55);
			_dxv1xfsm = ((((_2j4711hv * (_e26zouzj - _k2ugnue4)) + _dxv1xfsm) + (_5m0mjfxm * _0h01rnfp)) + (_08e01ee2 * ILNumerics.F2NET.Intrinsics.ABS(_1ajfmh55 )));//*    $          + ABS( TAU2 )*DW 
			//* 
			
			_x6inc284 = false;
			if (_p5ldiiph)
			{
				
				if ((-(_z1ioc3c8)) > (ILNumerics.F2NET.Intrinsics.ABS(_2vjsff4q ) / _76g572q1))
				_x6inc284 = true;
			}
			else
			{
				
				if (_z1ioc3c8 > (ILNumerics.F2NET.Intrinsics.ABS(_2vjsff4q ) / _76g572q1))
				_x6inc284 = true;
			}
			//* 
			//*        Main loop to update the values of the array   DELTA and WORK 
			//* 
			
			_em7fbywm = (_00exfor6 + (int)1);//* 
			
			{
				System.Int32 __81fgg2dlsvn635 = (System.Int32)(_em7fbywm);
				const System.Int32 __81fgg2step635 = (System.Int32)((int)1);
				System.Int32 __81fgg2count635;
				for (__81fgg2count635 = System.Math.Max(0, (System.Int32)(((System.Int32)(_gaia76w5) - __81fgg2dlsvn635 + __81fgg2step635) / __81fgg2step635)), _00exfor6 = __81fgg2dlsvn635; __81fgg2count635 != 0; __81fgg2count635--, _00exfor6 += (__81fgg2step635)) {

				{
					//* 
					//*           Test for convergence 
					//* 
					
					if (ILNumerics.F2NET.Intrinsics.ABS(_z1ioc3c8 ) <= (_p1iqarg6 * _dxv1xfsm))
					{
						//*     $          .OR. (SGUB-SGLB).LE.EIGHT*ABS(SGUB+SGLB) ) THEN 
						goto Mark240;
					}
					//* 
					
					if (_z1ioc3c8 <= _d0547bi2)
					{
						
						_j9hr6i7t = ILNumerics.F2NET.Intrinsics.MAX(_j9hr6i7t ,_0446f4de );
					}
					else
					{
						
						_8l8b6ahg = ILNumerics.F2NET.Intrinsics.MIN(_8l8b6ahg ,_0446f4de );
					}
					//* 
					//*           Calculate the new step 
					//* 
					
					if (!(_upfltzfu))
					{
						
						_aqu578x9 = (*(_apig8meb+(_8lksxei0 - 1)) * *(_9zhf8o7p+(_8lksxei0 - 1)));
						_zkzqogre = (*(_apig8meb+(_b5p6od9s - 1)) * *(_9zhf8o7p+(_b5p6od9s - 1)));
						if (!(_x6inc284))
						{
							
							if (_p5ldiiph)
							{
								
								_3crf0qn3 = ((_z1ioc3c8 - (_aqu578x9 * _h6amk27m)) + (_8k4zqcbt * __POW2((*(_7e60fcso+(_b5p6od9s - 1)) / _zkzqogre))));
							}
							else
							{
								
								_3crf0qn3 = ((_z1ioc3c8 - (_zkzqogre * _h6amk27m)) - (_8k4zqcbt * __POW2((*(_7e60fcso+(_8lksxei0 - 1)) / _aqu578x9))));
							}
							
						}
						else
						{
							
							_1ajfmh55 = (*(_7e60fcso+(_retbwjxi - 1)) / (*(_apig8meb+(_retbwjxi - 1)) * *(_9zhf8o7p+(_retbwjxi - 1))));
							if (_p5ldiiph)
							{
								
								_gxob35y5 = (_gxob35y5 + (_1ajfmh55 * _1ajfmh55));
							}
							else
							{
								
								_evfc66x3 = (_evfc66x3 + (_1ajfmh55 * _1ajfmh55));
							}
							
							_3crf0qn3 = ((_z1ioc3c8 - (_zkzqogre * _gxob35y5)) - (_aqu578x9 * _evfc66x3));
						}
						
						_vxfgpup9 = (((_aqu578x9 + _zkzqogre) * _z1ioc3c8) - ((_aqu578x9 * _zkzqogre) * _h6amk27m));
						_p9n405a5 = ((_aqu578x9 * _zkzqogre) * _z1ioc3c8);
						if (_3crf0qn3 == _d0547bi2)
						{
							
							if (_vxfgpup9 == _d0547bi2)
							{
								
								if (!(_x6inc284))
								{
									
									if (_p5ldiiph)
									{
										
										_vxfgpup9 = ((*(_7e60fcso+(_b5p6od9s - 1)) * *(_7e60fcso+(_b5p6od9s - 1))) + ((_aqu578x9 * _aqu578x9) * (_gxob35y5 + _evfc66x3)));
									}
									else
									{
										
										_vxfgpup9 = ((*(_7e60fcso+(_8lksxei0 - 1)) * *(_7e60fcso+(_8lksxei0 - 1))) + ((_zkzqogre * _zkzqogre) * (_gxob35y5 + _evfc66x3)));
									}
									
								}
								else
								{
									
									_vxfgpup9 = (((_zkzqogre * _zkzqogre) * _gxob35y5) + ((_aqu578x9 * _aqu578x9) * _evfc66x3));
								}
								
							}
							
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
						
					}
					else
					{
						//* 
						//*              Interpolation using THREE most relevant poles 
						//* 
						
						_k1wkbmv7 = (*(_apig8meb+(_snl53at2 - 1)) * *(_9zhf8o7p+(_snl53at2 - 1)));
						_s3whlcqr = (*(_apig8meb+(_hxummyuf - 1)) * *(_9zhf8o7p+(_hxummyuf - 1)));
						_1ajfmh55 = ((_0h01rnfp + _k2ugnue4) + _e26zouzj);
						if (_x6inc284)
						{
							
							_3crf0qn3 = ((_1ajfmh55 - (_k1wkbmv7 * _gxob35y5)) - (_s3whlcqr * _evfc66x3));
							*(_4bicvd70+((int)1 - 1)) = ((_k1wkbmv7 * _k1wkbmv7) * _gxob35y5);
							*(_4bicvd70+((int)3 - 1)) = ((_s3whlcqr * _s3whlcqr) * _evfc66x3);
						}
						else
						{
							
							if (_p5ldiiph)
							{
								
								_yc8h372p = (*(_7e60fcso+(_snl53at2 - 1)) / _k1wkbmv7);
								_yc8h372p = (_yc8h372p * _yc8h372p);
								_q3ig7mub = (((*(_plfm7z8g+(_snl53at2 - 1)) - *(_plfm7z8g+(_hxummyuf - 1))) * (*(_plfm7z8g+(_snl53at2 - 1)) + *(_plfm7z8g+(_hxummyuf - 1)))) * _yc8h372p);
								_3crf0qn3 = ((_1ajfmh55 - (_s3whlcqr * (_gxob35y5 + _evfc66x3))) - _q3ig7mub);
								*(_4bicvd70+((int)1 - 1)) = (*(_7e60fcso+(_snl53at2 - 1)) * *(_7e60fcso+(_snl53at2 - 1)));
								if (_gxob35y5 < _yc8h372p)
								{
									
									*(_4bicvd70+((int)3 - 1)) = ((_s3whlcqr * _s3whlcqr) * _evfc66x3);
								}
								else
								{
									
									*(_4bicvd70+((int)3 - 1)) = ((_s3whlcqr * _s3whlcqr) * ((_gxob35y5 - _yc8h372p) + _evfc66x3));
								}
								
							}
							else
							{
								
								_yc8h372p = (*(_7e60fcso+(_hxummyuf - 1)) / _s3whlcqr);
								_yc8h372p = (_yc8h372p * _yc8h372p);
								_q3ig7mub = (((*(_plfm7z8g+(_hxummyuf - 1)) - *(_plfm7z8g+(_snl53at2 - 1))) * (*(_plfm7z8g+(_snl53at2 - 1)) + *(_plfm7z8g+(_hxummyuf - 1)))) * _yc8h372p);
								_3crf0qn3 = ((_1ajfmh55 - (_k1wkbmv7 * (_gxob35y5 + _evfc66x3))) - _q3ig7mub);
								if (_evfc66x3 < _yc8h372p)
								{
									
									*(_4bicvd70+((int)1 - 1)) = ((_k1wkbmv7 * _k1wkbmv7) * _gxob35y5);
								}
								else
								{
									
									*(_4bicvd70+((int)1 - 1)) = ((_k1wkbmv7 * _k1wkbmv7) * (_gxob35y5 + (_evfc66x3 - _yc8h372p)));
								}
								
								*(_4bicvd70+((int)3 - 1)) = (*(_7e60fcso+(_hxummyuf - 1)) * *(_7e60fcso+(_hxummyuf - 1)));
							}
							
						}
						
						*(_f4rvsg6o+((int)1 - 1)) = _k1wkbmv7;
						*(_f4rvsg6o+((int)2 - 1)) = (*(_9zhf8o7p+(_retbwjxi - 1)) * *(_apig8meb+(_retbwjxi - 1)));
						*(_f4rvsg6o+((int)3 - 1)) = _s3whlcqr;
						_mca1c6k9(ref _00exfor6 ,ref _p5ldiiph ,ref _3crf0qn3 ,_f4rvsg6o ,_4bicvd70 ,ref _z1ioc3c8 ,ref _lr8d81xb ,ref _gro5yvfo );//* 
						
						if (_gro5yvfo != (int)0)
						{
							//* 
							//*                 If INFO is not 0, i.e., SLAED6 failed, switch 
							//*                 back to two pole interpolation 
							//* 
							
							_upfltzfu = false;
							_gro5yvfo = (int)0;
							_aqu578x9 = (*(_apig8meb+(_8lksxei0 - 1)) * *(_9zhf8o7p+(_8lksxei0 - 1)));
							_zkzqogre = (*(_apig8meb+(_b5p6od9s - 1)) * *(_9zhf8o7p+(_b5p6od9s - 1)));
							if (!(_x6inc284))
							{
								
								if (_p5ldiiph)
								{
									
									_3crf0qn3 = ((_z1ioc3c8 - (_aqu578x9 * _h6amk27m)) + (_8k4zqcbt * __POW2((*(_7e60fcso+(_b5p6od9s - 1)) / _zkzqogre))));
								}
								else
								{
									
									_3crf0qn3 = ((_z1ioc3c8 - (_zkzqogre * _h6amk27m)) - (_8k4zqcbt * __POW2((*(_7e60fcso+(_8lksxei0 - 1)) / _aqu578x9))));
								}
								
							}
							else
							{
								
								_1ajfmh55 = (*(_7e60fcso+(_retbwjxi - 1)) / (*(_apig8meb+(_retbwjxi - 1)) * *(_9zhf8o7p+(_retbwjxi - 1))));
								if (_p5ldiiph)
								{
									
									_gxob35y5 = (_gxob35y5 + (_1ajfmh55 * _1ajfmh55));
								}
								else
								{
									
									_evfc66x3 = (_evfc66x3 + (_1ajfmh55 * _1ajfmh55));
								}
								
								_3crf0qn3 = ((_z1ioc3c8 - (_zkzqogre * _gxob35y5)) - (_aqu578x9 * _evfc66x3));
							}
							
							_vxfgpup9 = (((_aqu578x9 + _zkzqogre) * _z1ioc3c8) - ((_aqu578x9 * _zkzqogre) * _h6amk27m));
							_p9n405a5 = ((_aqu578x9 * _zkzqogre) * _z1ioc3c8);
							if (_3crf0qn3 == _d0547bi2)
							{
								
								if (_vxfgpup9 == _d0547bi2)
								{
									
									if (!(_x6inc284))
									{
										
										if (_p5ldiiph)
										{
											
											_vxfgpup9 = ((*(_7e60fcso+(_b5p6od9s - 1)) * *(_7e60fcso+(_b5p6od9s - 1))) + ((_aqu578x9 * _aqu578x9) * (_gxob35y5 + _evfc66x3)));
										}
										else
										{
											
											_vxfgpup9 = ((*(_7e60fcso+(_8lksxei0 - 1)) * *(_7e60fcso+(_8lksxei0 - 1))) + ((_zkzqogre * _zkzqogre) * (_gxob35y5 + _evfc66x3)));
										}
										
									}
									else
									{
										
										_vxfgpup9 = (((_zkzqogre * _zkzqogre) * _gxob35y5) + ((_aqu578x9 * _aqu578x9) * _evfc66x3));
									}
									
								}
								
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
							
						}
						
					}
					//* 
					//*           Note, eta should be positive if w is negative, and 
					//*           eta should be negative otherwise. However, 
					//*           if for some reason caused by roundoff, eta*w > 0, 
					//*           we simply use one Newton step instead. This way 
					//*           will guarantee eta*w < 0. 
					//* 
					
					if ((_z1ioc3c8 * _lr8d81xb) >= _d0547bi2)
					_lr8d81xb = (-((_z1ioc3c8 / _h6amk27m)));//* 
					
					_lr8d81xb = (_lr8d81xb / (_91a1vq5f + ILNumerics.F2NET.Intrinsics.SQRT((_91a1vq5f * _91a1vq5f) + _lr8d81xb )));
					_1ajfmh55 = (_0446f4de + _lr8d81xb);
					if ((_1ajfmh55 > _8l8b6ahg) | (_1ajfmh55 < _j9hr6i7t))
					{
						
						if (_z1ioc3c8 < _d0547bi2)
						{
							
							_lr8d81xb = ((_8l8b6ahg - _0446f4de) / _5m0mjfxm);
						}
						else
						{
							
							_lr8d81xb = ((_j9hr6i7t - _0446f4de) / _5m0mjfxm);
						}
						
						if (_9vda0187)
						{
							
							if (_z1ioc3c8 < _d0547bi2)
							{
								
								if (_0446f4de > _d0547bi2)
								{
									
									_lr8d81xb = (ILNumerics.F2NET.Intrinsics.SQRT(_8l8b6ahg * _0446f4de ) - _0446f4de);
								}
								
							}
							else
							{
								
								if (_j9hr6i7t > _d0547bi2)
								{
									
									_lr8d81xb = (ILNumerics.F2NET.Intrinsics.SQRT(_j9hr6i7t * _0446f4de ) - _0446f4de);
								}
								
							}
							
						}
						
					}
					//* 
					
					_2vjsff4q = _z1ioc3c8;//* 
					
					_0446f4de = (_0446f4de + _lr8d81xb);
					_91a1vq5f = (_91a1vq5f + _lr8d81xb);//* 
					
					{
						System.Int32 __81fgg2dlsvn636 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step636 = (System.Int32)((int)1);
						System.Int32 __81fgg2count636;
						for (__81fgg2count636 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn636 + __81fgg2step636) / __81fgg2step636)), _znpjgsef = __81fgg2dlsvn636; __81fgg2count636 != 0; __81fgg2count636--, _znpjgsef += (__81fgg2step636)) {

						{
							
							*(_apig8meb+(_znpjgsef - 1)) = (*(_apig8meb+(_znpjgsef - 1)) + _lr8d81xb);
							*(_9zhf8o7p+(_znpjgsef - 1)) = (*(_9zhf8o7p+(_znpjgsef - 1)) - _lr8d81xb);
Mark200:;
							// continue
						}
												}					}//* 
					//*           Evaluate PSI and the derivative DPSI 
					//* 
					
					_gxob35y5 = _d0547bi2;
					_k2ugnue4 = _d0547bi2;
					_dxv1xfsm = _d0547bi2;
					{
						System.Int32 __81fgg2dlsvn637 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step637 = (System.Int32)((int)1);
						System.Int32 __81fgg2count637;
						for (__81fgg2count637 = System.Math.Max(0, (System.Int32)(((System.Int32)(_snl53at2) - __81fgg2dlsvn637 + __81fgg2step637) / __81fgg2step637)), _znpjgsef = __81fgg2dlsvn637; __81fgg2count637 != 0; __81fgg2count637--, _znpjgsef += (__81fgg2step637)) {

						{
							
							_1ajfmh55 = (*(_7e60fcso+(_znpjgsef - 1)) / (*(_apig8meb+(_znpjgsef - 1)) * *(_9zhf8o7p+(_znpjgsef - 1))));
							_k2ugnue4 = (_k2ugnue4 + (*(_7e60fcso+(_znpjgsef - 1)) * _1ajfmh55));
							_gxob35y5 = (_gxob35y5 + (_1ajfmh55 * _1ajfmh55));
							_dxv1xfsm = (_dxv1xfsm + _k2ugnue4);
Mark210:;
							// continue
						}
												}					}
					_dxv1xfsm = ILNumerics.F2NET.Intrinsics.ABS(_dxv1xfsm );//* 
					//*           Evaluate PHI and the derivative DPHI 
					//* 
					
					_evfc66x3 = _d0547bi2;
					_e26zouzj = _d0547bi2;
					{
						System.Int32 __81fgg2dlsvn638 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step638 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count638;
						for (__81fgg2count638 = System.Math.Max(0, (System.Int32)(((System.Int32)(_hxummyuf) - __81fgg2dlsvn638 + __81fgg2step638) / __81fgg2step638)), _znpjgsef = __81fgg2dlsvn638; __81fgg2count638 != 0; __81fgg2count638--, _znpjgsef += (__81fgg2step638)) {

						{
							
							_1ajfmh55 = (*(_7e60fcso+(_znpjgsef - 1)) / (*(_apig8meb+(_znpjgsef - 1)) * *(_9zhf8o7p+(_znpjgsef - 1))));
							_e26zouzj = (_e26zouzj + (*(_7e60fcso+(_znpjgsef - 1)) * _1ajfmh55));
							_evfc66x3 = (_evfc66x3 + (_1ajfmh55 * _1ajfmh55));
							_dxv1xfsm = (_dxv1xfsm + _e26zouzj);
Mark220:;
							// continue
						}
												}					}//* 
					
					_xs3ggrxu = (*(_apig8meb+(_retbwjxi - 1)) * *(_9zhf8o7p+(_retbwjxi - 1)));
					_1ajfmh55 = (*(_7e60fcso+(_retbwjxi - 1)) / _xs3ggrxu);
					_h6amk27m = ((_gxob35y5 + _evfc66x3) + (_1ajfmh55 * _1ajfmh55));
					_1ajfmh55 = (*(_7e60fcso+(_retbwjxi - 1)) * _1ajfmh55);
					_z1ioc3c8 = (((_0h01rnfp + _e26zouzj) + _k2ugnue4) + _1ajfmh55);
					_dxv1xfsm = ((((_2j4711hv * (_e26zouzj - _k2ugnue4)) + _dxv1xfsm) + (_5m0mjfxm * _0h01rnfp)) + (_08e01ee2 * ILNumerics.F2NET.Intrinsics.ABS(_1ajfmh55 )));//*    $             + ABS( TAU2 )*DW 
					//* 
					
					if (((_z1ioc3c8 * _2vjsff4q) > _d0547bi2) & (ILNumerics.F2NET.Intrinsics.ABS(_z1ioc3c8 ) > (ILNumerics.F2NET.Intrinsics.ABS(_2vjsff4q ) / _76g572q1)))
					_x6inc284 = (!(_x6inc284));//* 
					
Mark230:;
					// continue
				}
								}			}//* 
			//*        Return with INFO = 1, NITER = MAXIT and not converged 
			//* 
			
			_gro5yvfo = (int)1;//* 
			
		}
		//* 
		
Mark240:;
		// continue
		return;//* 
		//*     End of SLASD4 
		//* 
		
	}
	
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
