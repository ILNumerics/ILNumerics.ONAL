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
public static unsafe partial class FFTPACK5 {
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

	 
	public static void _zcmgczf5(ref Int32 _ivvongrz, ref Int32 _1mqgnnli, ref Int32 _0isd60h3, Single* _g0tjc1wj, ref Int32 _411n0nn5, Single* _tyzpsh18, ref Int32 _a8qh3w7g, Single* _elimqrjs)
	{
#region variable declarations
Single _x1gqiqyx =  default;
Int32 _1m894xin =  default;
Single _4vz8g6wc =  default;
Single _ari8efzi =  default;
Single _acxfj1dn =  default;
Single _q5ohdjfe =  default;
Single _p3s8p84p =  default;
Single _zmitv3pp =  default;
Int32 _ld42zxsk =  default;
Single _5ngvqg1k =  default;
Single _sihlgehx =  default;
Single _ta4e2isb =  default;
Single _w2i26fwx =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		
		{var vals = new Single[] { (float)-0.5,(float)-0.866025403784439 };var valsIter = 0;

		__c1f3kf._qhqyhuml = vals[valsIter++];
		__c1f3kf._6zfj7kh8 = vals[valsIter++];
		}//C 
		
		if (_ivvongrz > (int)1)goto Mark102;
		_x1gqiqyx = ((float)1 / ILNumerics.F2NET.Intrinsics.REAL((int)3 * _1mqgnnli ));
		if (_0isd60h3 == (int)1)goto Mark106;
		{
			System.Int32 __81fgg2dlsvn9 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step9 = (System.Int32)((int)1);
			System.Int32 __81fgg2count9;
			for (__81fgg2count9 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn9 + __81fgg2step9) / __81fgg2step9)), _1m894xin = __81fgg2dlsvn9; __81fgg2count9 != 0; __81fgg2count9--, _1m894xin += (__81fgg2step9)) {

			{
				
				_4vz8g6wc = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_ari8efzi = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__c1f3kf._qhqyhuml * _4vz8g6wc));
				*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_x1gqiqyx * (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _4vz8g6wc));
				_acxfj1dn = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_q5ohdjfe = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__c1f3kf._qhqyhuml * _acxfj1dn));
				*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_x1gqiqyx * (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _acxfj1dn));
				_p3s8p84p = (__c1f3kf._6zfj7kh8 * (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
				_zmitv3pp = (__c1f3kf._6zfj7kh8 * (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
				*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_x1gqiqyx * (_ari8efzi - _zmitv3pp));
				*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_x1gqiqyx * (_ari8efzi + _zmitv3pp));
				*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_x1gqiqyx * (_q5ohdjfe + _p3s8p84p));
				*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_x1gqiqyx * (_q5ohdjfe - _p3s8p84p));
Mark101:;
				// continue
			}
						}		}
		return;
Mark106:;
		
		{
			System.Int32 __81fgg2dlsvn10 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step10 = (System.Int32)((int)1);
			System.Int32 __81fgg2count10;
			for (__81fgg2count10 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn10 + __81fgg2step10) / __81fgg2step10)), _1m894xin = __81fgg2dlsvn10; __81fgg2count10 != 0; __81fgg2count10--, _1m894xin += (__81fgg2step10)) {

			{
				
				_4vz8g6wc = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_ari8efzi = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__c1f3kf._qhqyhuml * _4vz8g6wc));
				*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (_x1gqiqyx * (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _4vz8g6wc));
				_acxfj1dn = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_q5ohdjfe = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__c1f3kf._qhqyhuml * _acxfj1dn));
				*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (_x1gqiqyx * (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _acxfj1dn));
				_p3s8p84p = (__c1f3kf._6zfj7kh8 * (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
				_zmitv3pp = (__c1f3kf._6zfj7kh8 * (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
				*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (_x1gqiqyx * (_ari8efzi - _zmitv3pp));
				*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (_x1gqiqyx * (_ari8efzi + _zmitv3pp));
				*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (_x1gqiqyx * (_q5ohdjfe + _p3s8p84p));
				*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (_x1gqiqyx * (_q5ohdjfe - _p3s8p84p));
Mark107:;
				// continue
			}
						}		}
		return;
Mark102:;
		
		{
			System.Int32 __81fgg2dlsvn11 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step11 = (System.Int32)((int)1);
			System.Int32 __81fgg2count11;
			for (__81fgg2count11 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn11 + __81fgg2step11) / __81fgg2step11)), _1m894xin = __81fgg2dlsvn11; __81fgg2count11 != 0; __81fgg2count11--, _1m894xin += (__81fgg2step11)) {

			{
				
				_4vz8g6wc = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_ari8efzi = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__c1f3kf._qhqyhuml * _4vz8g6wc));
				*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _4vz8g6wc);
				_acxfj1dn = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_q5ohdjfe = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__c1f3kf._qhqyhuml * _acxfj1dn));
				*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _acxfj1dn);
				_p3s8p84p = (__c1f3kf._6zfj7kh8 * (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
				_zmitv3pp = (__c1f3kf._6zfj7kh8 * (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
				*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (_ari8efzi - _zmitv3pp);
				*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (_ari8efzi + _zmitv3pp);
				*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (_q5ohdjfe + _p3s8p84p);
				*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (_q5ohdjfe - _p3s8p84p);
Mark103:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn12 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step12 = (System.Int32)((int)1);
			System.Int32 __81fgg2count12;
			for (__81fgg2count12 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ivvongrz) - __81fgg2dlsvn12 + __81fgg2step12) / __81fgg2step12)), _ld42zxsk = __81fgg2dlsvn12; __81fgg2count12 != 0; __81fgg2count12--, _ld42zxsk += (__81fgg2step12)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn13 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step13 = (System.Int32)((int)1);
					System.Int32 __81fgg2count13;
					for (__81fgg2count13 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn13 + __81fgg2step13) / __81fgg2step13)), _1m894xin = __81fgg2dlsvn13; __81fgg2count13 != 0; __81fgg2count13--, _1m894xin += (__81fgg2step13)) {

					{
						
						_4vz8g6wc = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_ari8efzi = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__c1f3kf._qhqyhuml * _4vz8g6wc));
						*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _4vz8g6wc);
						_acxfj1dn = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_q5ohdjfe = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__c1f3kf._qhqyhuml * _acxfj1dn));
						*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _acxfj1dn);
						_p3s8p84p = (__c1f3kf._6zfj7kh8 * (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
						_zmitv3pp = (__c1f3kf._6zfj7kh8 * (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
						_5ngvqg1k = (_ari8efzi - _zmitv3pp);
						_sihlgehx = (_ari8efzi + _zmitv3pp);
						_ta4e2isb = (_q5ohdjfe + _p3s8p84p);
						_w2i26fwx = (_q5ohdjfe - _p3s8p84p);
						*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)1 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)2)) * _ta4e2isb) - (*(_elimqrjs+(_ld42zxsk - 1) + ((int)1 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)2)) * _5ngvqg1k));
						*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)1 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)2)) * _5ngvqg1k) + (*(_elimqrjs+(_ld42zxsk - 1) + ((int)1 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)2)) * _ta4e2isb));
						*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)2 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)2)) * _w2i26fwx) - (*(_elimqrjs+(_ld42zxsk - 1) + ((int)2 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)2)) * _sihlgehx));
						*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)2 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)2)) * _sihlgehx) + (*(_elimqrjs+(_ld42zxsk - 1) + ((int)2 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)2)) * _w2i26fwx));
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


internal unsafe class __c1f3kf { 
internal static Single _qhqyhuml =  default;
internal static Single _6zfj7kh8 =  default;

}

} // end class 
} // end namespace
#endif
