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
//////////////////////////////////////////////////////////////////
//  This is an auto - generated source file.                    //
//  Do not manually edit this file! Any changes made will be    //
//  lost on the next build! Edit the corresponding source file  //
//  (per default located in the template/ folder) instead!      //
//                                                              //
//////////////////////////////////////////////////////////////////

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
public static unsafe partial class DFFTPACK5 {
//C     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
//C     *                                                               * 
//C     *                  copyright (c) 2011 by UCAR                   * 
//C     *                                                               * 
//C     *       University Corporation for Atmospheric Research         * 
//C     *                                                               * 
//C     *                      all rights reserved                      * 
//C     *                                                               * 
//C     *                     FFTPACK  version 5.1                      * 
//C     *                                                               * 
//C     *                 A Fortran Package of Fast Fourier             * 
//C     *                                                               * 
//C     *                Subroutines and Example Programs               * 
//C     *                                                               * 
//C     *                             by                                * 
//C     *                                                               * 
//C     *               Paul Swarztrauber and Dick Valent               * 
//C     *                                                               * 
//C     *                             of                                * 
//C     *                                                               * 
//C     *         the National Center for Atmospheric Research          * 
//C     *                                                               * 
//C     *                Boulder, Colorado  (80307)  U.S.A.             * 
//C     *                                                               * 
//C     *                   which is sponsored by                       * 
//C     *                                                               * 
//C     *              the National Science Foundation                  * 
//C     *                                                               * 
//C     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
//C 

	 
	public static void _q8m0cuyi(ref Int32 _ivvongrz, ref Int32 _1mqgnnli, ref Int32 _0isd60h3, Double* _g0tjc1wj, ref Int32 _411n0nn5, Double* _tyzpsh18, ref Int32 _a8qh3w7g, Double* _elimqrjs)
	{
#region variable declarations
Double _x1gqiqyx =  default;
Int32 _1m894xin =  default;
Double _bhqg1tjv =  default;
Double _acxfj1dn =  default;
Double _evau9kxw =  default;
Double _4e8x89om =  default;
Double _wu5h2ru8 =  default;
Double _4vz8g6wc =  default;
Double _mvcv2lkd =  default;
Double _sxykfbxf =  default;
Int32 _ld42zxsk =  default;
Double _p3s8p84p =  default;
Double _zmitv3pp =  default;
Double _ari8efzi =  default;
Double _lz58wakc =  default;
Double _q5ohdjfe =  default;
Double _2dugxc3w =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//C 
		//C FFTPACK 5.1 auxiliary routine 
		//C 
		
		if (_ivvongrz > (int)1)goto Mark102;
		_x1gqiqyx = ((double)1 / ILNumerics.F2NET.Intrinsics.DBLE((int)4 * _1mqgnnli ));
		if (_0isd60h3 == (int)1)goto Mark106;
		{
			System.Int32 __81fgg2dlsvn14 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step14 = (System.Int32)((int)1);
			System.Int32 __81fgg2count14;
			for (__81fgg2count14 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn14 + __81fgg2step14) / __81fgg2step14)), _1m894xin = __81fgg2dlsvn14; __81fgg2count14 != 0; __81fgg2count14--, _1m894xin += (__81fgg2step14)) {

			{
				
				_bhqg1tjv = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_acxfj1dn = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_evau9kxw = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_4e8x89om = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_wu5h2ru8 = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_4vz8g6wc = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_mvcv2lkd = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_sxykfbxf = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_x1gqiqyx * (_4vz8g6wc + _sxykfbxf));
				*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_x1gqiqyx * (_4vz8g6wc - _sxykfbxf));
				*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_x1gqiqyx * (_acxfj1dn + _4e8x89om));
				*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_x1gqiqyx * (_acxfj1dn - _4e8x89om));
				*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_x1gqiqyx * (_wu5h2ru8 + _evau9kxw));
				*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_x1gqiqyx * (_wu5h2ru8 - _evau9kxw));
				*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_x1gqiqyx * (_bhqg1tjv + _mvcv2lkd));
				*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_x1gqiqyx * (_bhqg1tjv - _mvcv2lkd));
Mark101:;
				// continue
			}
						}		}
		return;
Mark106:;
		
		{
			System.Int32 __81fgg2dlsvn15 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step15 = (System.Int32)((int)1);
			System.Int32 __81fgg2count15;
			for (__81fgg2count15 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn15 + __81fgg2step15) / __81fgg2step15)), _1m894xin = __81fgg2dlsvn15; __81fgg2count15 != 0; __81fgg2count15--, _1m894xin += (__81fgg2step15)) {

			{
				
				_bhqg1tjv = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_acxfj1dn = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_evau9kxw = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_4e8x89om = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_wu5h2ru8 = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_4vz8g6wc = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_mvcv2lkd = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_sxykfbxf = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)4)) = (_x1gqiqyx * (_4vz8g6wc + _sxykfbxf));
				*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)4)) = (_x1gqiqyx * (_4vz8g6wc - _sxykfbxf));
				*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)4)) = (_x1gqiqyx * (_acxfj1dn + _4e8x89om));
				*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)4)) = (_x1gqiqyx * (_acxfj1dn - _4e8x89om));
				*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)4)) = (_x1gqiqyx * (_wu5h2ru8 + _evau9kxw));
				*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)4 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)4)) = (_x1gqiqyx * (_wu5h2ru8 - _evau9kxw));
				*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)4)) = (_x1gqiqyx * (_bhqg1tjv + _mvcv2lkd));
				*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)4 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)4)) = (_x1gqiqyx * (_bhqg1tjv - _mvcv2lkd));
Mark107:;
				// continue
			}
						}		}
		return;
Mark102:;
		
		{
			System.Int32 __81fgg2dlsvn16 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step16 = (System.Int32)((int)1);
			System.Int32 __81fgg2count16;
			for (__81fgg2count16 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn16 + __81fgg2step16) / __81fgg2step16)), _1m894xin = __81fgg2dlsvn16; __81fgg2count16 != 0; __81fgg2count16--, _1m894xin += (__81fgg2step16)) {

			{
				
				_bhqg1tjv = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_acxfj1dn = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_evau9kxw = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_4e8x89om = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_wu5h2ru8 = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_4vz8g6wc = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_mvcv2lkd = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_sxykfbxf = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)4)) = (_4vz8g6wc + _sxykfbxf);
				*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)4)) = (_4vz8g6wc - _sxykfbxf);
				*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)4)) = (_acxfj1dn + _4e8x89om);
				*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)4)) = (_acxfj1dn - _4e8x89om);
				*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)4)) = (_wu5h2ru8 + _evau9kxw);
				*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)4 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)4)) = (_wu5h2ru8 - _evau9kxw);
				*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)4)) = (_bhqg1tjv + _mvcv2lkd);
				*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)4 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)4)) = (_bhqg1tjv - _mvcv2lkd);
Mark103:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn17 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step17 = (System.Int32)((int)1);
			System.Int32 __81fgg2count17;
			for (__81fgg2count17 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ivvongrz) - __81fgg2dlsvn17 + __81fgg2step17) / __81fgg2step17)), _ld42zxsk = __81fgg2dlsvn17; __81fgg2count17 != 0; __81fgg2count17--, _ld42zxsk += (__81fgg2step17)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn18 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step18 = (System.Int32)((int)1);
					System.Int32 __81fgg2count18;
					for (__81fgg2count18 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn18 + __81fgg2step18) / __81fgg2step18)), _1m894xin = __81fgg2dlsvn18; __81fgg2count18 != 0; __81fgg2count18--, _1m894xin += (__81fgg2step18)) {

					{
						
						_bhqg1tjv = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_acxfj1dn = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_4e8x89om = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_evau9kxw = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_wu5h2ru8 = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_4vz8g6wc = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_mvcv2lkd = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_sxykfbxf = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)4)) = (_4vz8g6wc + _sxykfbxf);
						_p3s8p84p = (_4vz8g6wc - _sxykfbxf);
						*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)4)) = (_acxfj1dn + _4e8x89om);
						_zmitv3pp = (_acxfj1dn - _4e8x89om);
						_ari8efzi = (_wu5h2ru8 + _evau9kxw);
						_lz58wakc = (_wu5h2ru8 - _evau9kxw);
						_q5ohdjfe = (_bhqg1tjv + _mvcv2lkd);
						_2dugxc3w = (_bhqg1tjv - _mvcv2lkd);
						*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)4)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)1 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)3)) * _ari8efzi) + (*(_elimqrjs+(_ld42zxsk - 1) + ((int)1 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)3)) * _q5ohdjfe));
						*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)4)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)1 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)3)) * _q5ohdjfe) - (*(_elimqrjs+(_ld42zxsk - 1) + ((int)1 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)3)) * _ari8efzi));
						*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)4)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)2 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)3)) * _p3s8p84p) + (*(_elimqrjs+(_ld42zxsk - 1) + ((int)2 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)3)) * _zmitv3pp));
						*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)4)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)2 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)3)) * _zmitv3pp) - (*(_elimqrjs+(_ld42zxsk - 1) + ((int)2 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)3)) * _p3s8p84p));
						*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)4 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)4)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)3 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)3)) * _lz58wakc) + (*(_elimqrjs+(_ld42zxsk - 1) + ((int)3 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)3)) * _2dugxc3w));
						*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)4 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)4)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)3 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)3)) * _2dugxc3w) - (*(_elimqrjs+(_ld42zxsk - 1) + ((int)3 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)3)) * _lz58wakc));
Mark104:;
						// continue
					}
										}				}
Mark105:;
				// continue
			}
						}		}
		return;
	}
	
	} // 177

} // end class 
} // end namespace
#endif
