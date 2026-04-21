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

	 
	public static void _oov31wa9(ref Int32 _ivvongrz, ref Int32 _1mqgnnli, ref Int32 _0isd60h3, Single* _g0tjc1wj, ref Int32 _411n0nn5, Single* _tyzpsh18, ref Int32 _a8qh3w7g, Single* _elimqrjs)
	{
#region variable declarations
Single _x1gqiqyx =  default;
Int32 _1m894xin =  default;
Single _5wtia2l9 =  default;
Single _9ujekppi =  default;
Int32 _ld42zxsk =  default;
Single _4vz8g6wc =  default;
Single _acxfj1dn =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//C 
		
		if (_ivvongrz > (int)1)goto Mark102;
		_x1gqiqyx = ((float)1 / ILNumerics.F2NET.Intrinsics.REAL((int)2 * _1mqgnnli ));
		if (_0isd60h3 == (int)1)goto Mark106;
		{
			System.Int32 __81fgg2dlsvn4 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step4 = (System.Int32)((int)1);
			System.Int32 __81fgg2count4;
			for (__81fgg2count4 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn4 + __81fgg2step4) / __81fgg2step4)), _1m894xin = __81fgg2dlsvn4; __81fgg2count4 != 0; __81fgg2count4--, _1m894xin += (__81fgg2step4)) {

			{
				
				_5wtia2l9 = (_x1gqiqyx * (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
				*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_x1gqiqyx * (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
				*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = _5wtia2l9;
				_9ujekppi = (_x1gqiqyx * (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
				*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_x1gqiqyx * (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
				*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = _9ujekppi;
Mark101:;
				// continue
			}
						}		}
		return;
Mark106:;
		
		{
			System.Int32 __81fgg2dlsvn5 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step5 = (System.Int32)((int)1);
			System.Int32 __81fgg2count5;
			for (__81fgg2count5 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn5 + __81fgg2step5) / __81fgg2step5)), _1m894xin = __81fgg2dlsvn5; __81fgg2count5 != 0; __81fgg2count5--, _1m894xin += (__81fgg2step5)) {

			{
				
				*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)2)) = (_x1gqiqyx * (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
				*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)2)) = (_x1gqiqyx * (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
				*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)2)) = (_x1gqiqyx * (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
				*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)2)) = (_x1gqiqyx * (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
Mark107:;
				// continue
			}
						}		}
		return;
Mark102:;
		
		{
			System.Int32 __81fgg2dlsvn6 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step6 = (System.Int32)((int)1);
			System.Int32 __81fgg2count6;
			for (__81fgg2count6 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn6 + __81fgg2step6) / __81fgg2step6)), _1m894xin = __81fgg2dlsvn6; __81fgg2count6 != 0; __81fgg2count6--, _1m894xin += (__81fgg2step6)) {

			{
				
				*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)2)) = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)2)) = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)2)) = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)2)) = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
Mark103:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn7 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step7 = (System.Int32)((int)1);
			System.Int32 __81fgg2count7;
			for (__81fgg2count7 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ivvongrz) - __81fgg2dlsvn7 + __81fgg2step7) / __81fgg2step7)), _ld42zxsk = __81fgg2dlsvn7; __81fgg2count7 != 0; __81fgg2count7--, _ld42zxsk += (__81fgg2step7)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn8 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step8 = (System.Int32)((int)1);
					System.Int32 __81fgg2count8;
					for (__81fgg2count8 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn8 + __81fgg2step8) / __81fgg2step8)), _1m894xin = __81fgg2dlsvn8; __81fgg2count8 != 0; __81fgg2count8--, _1m894xin += (__81fgg2step8)) {

					{
						
						*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)2)) = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_4vz8g6wc = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)2)) = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_acxfj1dn = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)2)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)1 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)1)) * _acxfj1dn) - (*(_elimqrjs+(_ld42zxsk - 1) + ((int)1 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)1)) * _4vz8g6wc));
						*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)2)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)1 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)1)) * _4vz8g6wc) + (*(_elimqrjs+(_ld42zxsk - 1) + ((int)1 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)1)) * _acxfj1dn));
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
