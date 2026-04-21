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

	 
	public static void _08hj2wgo(ref Int32 _ivvongrz, ref Int32 _zmzafydt, ref Int32 _1mqgnnli, ref Int32 _p39hja58, ref Int32 _0isd60h3, Single* _g0tjc1wj, Single* _ong9x5ud, ref Int32 _411n0nn5, Single* _tyzpsh18, Single* _ynzbkk10, ref Int32 _a8qh3w7g, Single* _elimqrjs)
	{
#region variable declarations
Int32 _gbsim2rn =  default;
Int32 _4nqkz2st =  default;
Int32 _tm2fl5em =  default;
Int32 _qb0uu4i2 =  default;
Int32 _bb3g971f =  default;
Int32 _cjahrwwv =  default;
Int32 _63ekojo6 =  default;
Int32 _5fr8svok =  default;
Single _7ghk8kn7 =  default;
Single _vlip1kc6 =  default;
Single _5wtia2l9 =  default;
Single _9ujekppi =  default;
Int32 _ld42zxsk =  default;
Int32 _1m894xin =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//C 
		//C FFTPACK 5.1 auxiliary routine 
		//C 
		
		_gbsim2rn = (_zmzafydt + (int)2);
		_4nqkz2st = ((_zmzafydt + (int)1) / (int)2);
		{
			System.Int32 __81fgg2dlsvn66 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step66 = (System.Int32)((int)1);
			System.Int32 __81fgg2count66;
			for (__81fgg2count66 = System.Math.Max(0, (System.Int32)(((System.Int32)(_p39hja58) - __81fgg2dlsvn66 + __81fgg2step66) / __81fgg2step66)), _tm2fl5em = __81fgg2dlsvn66; __81fgg2count66 != 0; __81fgg2count66--, _tm2fl5em += (__81fgg2step66)) {

			{
				
				*(_ynzbkk10+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) = *(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_p39hja58));
				*(_ynzbkk10+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) = *(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_p39hja58));
Mark110:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn67 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step67 = (System.Int32)((int)1);
			System.Int32 __81fgg2count67;
			for (__81fgg2count67 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4nqkz2st) - __81fgg2dlsvn67 + __81fgg2step67) / __81fgg2step67)), _qb0uu4i2 = __81fgg2dlsvn67; __81fgg2count67 != 0; __81fgg2count67--, _qb0uu4i2 += (__81fgg2step67)) {

			{
				
				_bb3g971f = (_gbsim2rn - _qb0uu4i2);
				{
					System.Int32 __81fgg2dlsvn68 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step68 = (System.Int32)((int)1);
					System.Int32 __81fgg2count68;
					for (__81fgg2count68 = System.Math.Max(0, (System.Int32)(((System.Int32)(_p39hja58) - __81fgg2dlsvn68 + __81fgg2step68) / __81fgg2step68)), _tm2fl5em = __81fgg2dlsvn68; __81fgg2count68 != 0; __81fgg2count68--, _tm2fl5em += (__81fgg2step68)) {

					{
						
						*(_ynzbkk10+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) = (*(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) + *(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58)));
						*(_ynzbkk10+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) = (*(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) - *(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58)));
						*(_ynzbkk10+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) = (*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) + *(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58)));
						*(_ynzbkk10+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) = (*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) - *(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58)));
Mark112:;
						// continue
					}
										}				}
Mark111:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn69 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step69 = (System.Int32)((int)1);
			System.Int32 __81fgg2count69;
			for (__81fgg2count69 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4nqkz2st) - __81fgg2dlsvn69 + __81fgg2step69) / __81fgg2step69)), _qb0uu4i2 = __81fgg2dlsvn69; __81fgg2count69 != 0; __81fgg2count69--, _qb0uu4i2 += (__81fgg2step69)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn70 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step70 = (System.Int32)((int)1);
					System.Int32 __81fgg2count70;
					for (__81fgg2count70 = System.Math.Max(0, (System.Int32)(((System.Int32)(_p39hja58) - __81fgg2dlsvn70 + __81fgg2step70) / __81fgg2step70)), _tm2fl5em = __81fgg2dlsvn70; __81fgg2count70 != 0; __81fgg2count70--, _tm2fl5em += (__81fgg2step70)) {

					{
						
						*(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_p39hja58)) = (*(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_p39hja58)) + *(_ynzbkk10+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_p39hja58)));
						*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_p39hja58)) = (*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_p39hja58)) + *(_ynzbkk10+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_p39hja58)));
Mark117:;
						// continue
					}
										}				}
Mark118:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn71 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step71 = (System.Int32)((int)1);
			System.Int32 __81fgg2count71;
			for (__81fgg2count71 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4nqkz2st) - __81fgg2dlsvn71 + __81fgg2step71) / __81fgg2step71)), _cjahrwwv = __81fgg2dlsvn71; __81fgg2count71 != 0; __81fgg2count71--, _cjahrwwv += (__81fgg2step71)) {

			{
				
				_63ekojo6 = (_gbsim2rn - _cjahrwwv);
				{
					System.Int32 __81fgg2dlsvn72 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step72 = (System.Int32)((int)1);
					System.Int32 __81fgg2count72;
					for (__81fgg2count72 = System.Math.Max(0, (System.Int32)(((System.Int32)(_p39hja58) - __81fgg2dlsvn72 + __81fgg2step72) / __81fgg2step72)), _tm2fl5em = __81fgg2dlsvn72; __81fgg2count72 != 0; __81fgg2count72--, _tm2fl5em += (__81fgg2step72)) {

					{
						
						*(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_cjahrwwv - 1) * 1 * (_411n0nn5) * (_p39hja58)) = (*(_ynzbkk10+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) + (*(_elimqrjs+((int)1 - 1) + (_cjahrwwv - (int)1 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * (_zmzafydt - (int)1)) * *(_ynzbkk10+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_p39hja58))));
						*(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_63ekojo6 - 1) * 1 * (_411n0nn5) * (_p39hja58)) = (*(_elimqrjs+((int)1 - 1) + (_cjahrwwv - (int)1 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * (_zmzafydt - (int)1)) * *(_ynzbkk10+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + (_zmzafydt - 1) * 1 * (_a8qh3w7g) * (_p39hja58)));
						*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_cjahrwwv - 1) * 1 * (_411n0nn5) * (_p39hja58)) = (*(_ynzbkk10+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) + (*(_elimqrjs+((int)1 - 1) + (_cjahrwwv - (int)1 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * (_zmzafydt - (int)1)) * *(_ynzbkk10+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_p39hja58))));
						*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_63ekojo6 - 1) * 1 * (_411n0nn5) * (_p39hja58)) = (*(_elimqrjs+((int)1 - 1) + (_cjahrwwv - (int)1 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * (_zmzafydt - (int)1)) * *(_ynzbkk10+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + (_zmzafydt - 1) * 1 * (_a8qh3w7g) * (_p39hja58)));
Mark113:;
						// continue
					}
										}				}
				{
					System.Int32 __81fgg2dlsvn73 = (System.Int32)((int)3);
					const System.Int32 __81fgg2step73 = (System.Int32)((int)1);
					System.Int32 __81fgg2count73;
					for (__81fgg2count73 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4nqkz2st) - __81fgg2dlsvn73 + __81fgg2step73) / __81fgg2step73)), _qb0uu4i2 = __81fgg2dlsvn73; __81fgg2count73 != 0; __81fgg2count73--, _qb0uu4i2 += (__81fgg2step73)) {

					{
						
						_bb3g971f = (_gbsim2rn - _qb0uu4i2);
						_5fr8svok = ILNumerics.F2NET.Intrinsics.MOD((_cjahrwwv - (int)1) * (_qb0uu4i2 - (int)1) ,_zmzafydt );
						_7ghk8kn7 = *(_elimqrjs+((int)1 - 1) + (_5fr8svok - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * (_zmzafydt - (int)1));
						_vlip1kc6 = *(_elimqrjs+((int)1 - 1) + (_5fr8svok - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * (_zmzafydt - (int)1));
						{
							System.Int32 __81fgg2dlsvn74 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step74 = (System.Int32)((int)1);
							System.Int32 __81fgg2count74;
							for (__81fgg2count74 = System.Math.Max(0, (System.Int32)(((System.Int32)(_p39hja58) - __81fgg2dlsvn74 + __81fgg2step74) / __81fgg2step74)), _tm2fl5em = __81fgg2dlsvn74; __81fgg2count74 != 0; __81fgg2count74--, _tm2fl5em += (__81fgg2step74)) {

							{
								
								*(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_cjahrwwv - 1) * 1 * (_411n0nn5) * (_p39hja58)) = (*(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_cjahrwwv - 1) * 1 * (_411n0nn5) * (_p39hja58)) + (_7ghk8kn7 * *(_ynzbkk10+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_p39hja58))));
								*(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_63ekojo6 - 1) * 1 * (_411n0nn5) * (_p39hja58)) = (*(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_63ekojo6 - 1) * 1 * (_411n0nn5) * (_p39hja58)) + (_vlip1kc6 * *(_ynzbkk10+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_p39hja58))));
								*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_cjahrwwv - 1) * 1 * (_411n0nn5) * (_p39hja58)) = (*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_cjahrwwv - 1) * 1 * (_411n0nn5) * (_p39hja58)) + (_7ghk8kn7 * *(_ynzbkk10+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_p39hja58))));
								*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_63ekojo6 - 1) * 1 * (_411n0nn5) * (_p39hja58)) = (*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_63ekojo6 - 1) * 1 * (_411n0nn5) * (_p39hja58)) + (_vlip1kc6 * *(_ynzbkk10+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_p39hja58))));
Mark114:;
								// continue
							}
														}						}
Mark115:;
						// continue
					}
										}				}
Mark116:;
				// continue
			}
						}		}
		if ((_ivvongrz > (int)1) | (_0isd60h3 == (int)1))goto Mark136;
		{
			System.Int32 __81fgg2dlsvn75 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step75 = (System.Int32)((int)1);
			System.Int32 __81fgg2count75;
			for (__81fgg2count75 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4nqkz2st) - __81fgg2dlsvn75 + __81fgg2step75) / __81fgg2step75)), _qb0uu4i2 = __81fgg2dlsvn75; __81fgg2count75 != 0; __81fgg2count75--, _qb0uu4i2 += (__81fgg2step75)) {

			{
				
				_bb3g971f = (_gbsim2rn - _qb0uu4i2);
				{
					System.Int32 __81fgg2dlsvn76 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step76 = (System.Int32)((int)1);
					System.Int32 __81fgg2count76;
					for (__81fgg2count76 = System.Math.Max(0, (System.Int32)(((System.Int32)(_p39hja58) - __81fgg2dlsvn76 + __81fgg2step76) / __81fgg2step76)), _tm2fl5em = __81fgg2dlsvn76; __81fgg2count76 != 0; __81fgg2count76--, _tm2fl5em += (__81fgg2step76)) {

					{
						
						_5wtia2l9 = (*(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) - *(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58)));
						_9ujekppi = (*(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) + *(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58)));
						*(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) = _5wtia2l9;
						*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58)) = (*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) - *(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58)));
						*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) = (*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) + *(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58)));
						*(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58)) = _9ujekppi;
Mark119:;
						// continue
					}
										}				}
Mark120:;
				// continue
			}
						}		}
		return;
Mark136:;
		
		{
			System.Int32 __81fgg2dlsvn77 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step77 = (System.Int32)((int)1);
			System.Int32 __81fgg2count77;
			for (__81fgg2count77 = System.Math.Max(0, (System.Int32)(((System.Int32)(_p39hja58) - __81fgg2dlsvn77 + __81fgg2step77) / __81fgg2step77)), _tm2fl5em = __81fgg2dlsvn77; __81fgg2count77 != 0; __81fgg2count77--, _tm2fl5em += (__81fgg2step77)) {

			{
				
				*(_ynzbkk10+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) = *(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_p39hja58));
				*(_ynzbkk10+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) = *(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_p39hja58));
Mark137:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn78 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step78 = (System.Int32)((int)1);
			System.Int32 __81fgg2count78;
			for (__81fgg2count78 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4nqkz2st) - __81fgg2dlsvn78 + __81fgg2step78) / __81fgg2step78)), _qb0uu4i2 = __81fgg2dlsvn78; __81fgg2count78 != 0; __81fgg2count78--, _qb0uu4i2 += (__81fgg2step78)) {

			{
				
				_bb3g971f = (_gbsim2rn - _qb0uu4i2);
				{
					System.Int32 __81fgg2dlsvn79 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step79 = (System.Int32)((int)1);
					System.Int32 __81fgg2count79;
					for (__81fgg2count79 = System.Math.Max(0, (System.Int32)(((System.Int32)(_p39hja58) - __81fgg2dlsvn79 + __81fgg2step79) / __81fgg2step79)), _tm2fl5em = __81fgg2dlsvn79; __81fgg2count79 != 0; __81fgg2count79--, _tm2fl5em += (__81fgg2step79)) {

					{
						
						*(_ynzbkk10+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) = (*(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) - *(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58)));
						*(_ynzbkk10+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) = (*(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) + *(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58)));
						*(_ynzbkk10+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) = (*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) - *(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58)));
						*(_ynzbkk10+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) = (*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) + *(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58)));
Mark134:;
						// continue
					}
										}				}
Mark135:;
				// continue
			}
						}		}
		if (_ivvongrz == (int)1)
		return;
		{
			System.Int32 __81fgg2dlsvn80 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step80 = (System.Int32)((int)1);
			System.Int32 __81fgg2count80;
			for (__81fgg2count80 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ivvongrz) - __81fgg2dlsvn80 + __81fgg2step80) / __81fgg2step80)), _ld42zxsk = __81fgg2dlsvn80; __81fgg2count80 != 0; __81fgg2count80--, _ld42zxsk += (__81fgg2step80)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn81 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step81 = (System.Int32)((int)1);
					System.Int32 __81fgg2count81;
					for (__81fgg2count81 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn81 + __81fgg2step81) / __81fgg2step81)), _1m894xin = __81fgg2dlsvn81; __81fgg2count81 != 0; __81fgg2count81--, _1m894xin += (__81fgg2step81)) {

					{
						
						*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_zmzafydt)) = *(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * (_ivvongrz));
						*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_zmzafydt)) = *(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * (_ivvongrz));
Mark130:;
						// continue
					}
										}				}
Mark131:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn82 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step82 = (System.Int32)((int)1);
			System.Int32 __81fgg2count82;
			for (__81fgg2count82 = System.Math.Max(0, (System.Int32)(((System.Int32)(_zmzafydt) - __81fgg2dlsvn82 + __81fgg2step82) / __81fgg2step82)), _qb0uu4i2 = __81fgg2dlsvn82; __81fgg2count82 != 0; __81fgg2count82--, _qb0uu4i2 += (__81fgg2step82)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn83 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step83 = (System.Int32)((int)1);
					System.Int32 __81fgg2count83;
					for (__81fgg2count83 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn83 + __81fgg2step83) / __81fgg2step83)), _1m894xin = __81fgg2dlsvn83; __81fgg2count83 != 0; __81fgg2count83--, _1m894xin += (__81fgg2step83)) {

					{
						
						*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_zmzafydt)) = *(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * (_ivvongrz));
						*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_zmzafydt)) = *(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * (_ivvongrz));
Mark122:;
						// continue
					}
										}				}
Mark123:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn84 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step84 = (System.Int32)((int)1);
			System.Int32 __81fgg2count84;
			for (__81fgg2count84 = System.Math.Max(0, (System.Int32)(((System.Int32)(_zmzafydt) - __81fgg2dlsvn84 + __81fgg2step84) / __81fgg2step84)), _qb0uu4i2 = __81fgg2dlsvn84; __81fgg2count84 != 0; __81fgg2count84--, _qb0uu4i2 += (__81fgg2step84)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn85 = (System.Int32)((int)2);
					const System.Int32 __81fgg2step85 = (System.Int32)((int)1);
					System.Int32 __81fgg2count85;
					for (__81fgg2count85 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ivvongrz) - __81fgg2dlsvn85 + __81fgg2step85) / __81fgg2step85)), _ld42zxsk = __81fgg2dlsvn85; __81fgg2count85 != 0; __81fgg2count85--, _ld42zxsk += (__81fgg2step85)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn86 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step86 = (System.Int32)((int)1);
							System.Int32 __81fgg2count86;
							for (__81fgg2count86 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn86 + __81fgg2step86) / __81fgg2step86)), _1m894xin = __81fgg2dlsvn86; __81fgg2count86 != 0; __81fgg2count86--, _1m894xin += (__81fgg2step86)) {

							{
								
								*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_zmzafydt)) = ((*(_elimqrjs+(_ld42zxsk - 1) + (_qb0uu4i2 - (int)1 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * (_zmzafydt - (int)1)) * *(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * (_ivvongrz))) - (*(_elimqrjs+(_ld42zxsk - 1) + (_qb0uu4i2 - (int)1 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * (_zmzafydt - (int)1)) * *(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * (_ivvongrz))));
								*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_zmzafydt)) = ((*(_elimqrjs+(_ld42zxsk - 1) + (_qb0uu4i2 - (int)1 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * (_zmzafydt - (int)1)) * *(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * (_ivvongrz))) + (*(_elimqrjs+(_ld42zxsk - 1) + (_qb0uu4i2 - (int)1 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * (_zmzafydt - (int)1)) * *(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * (_ivvongrz))));
Mark124:;
								// continue
							}
														}						}
Mark125:;
						// continue
					}
										}				}
Mark126:;
				// continue
			}
						}		}
		return;
	}
	
	} // 177

} // end class 
} // end namespace
#endif
