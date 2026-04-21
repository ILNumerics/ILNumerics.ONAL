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

	 
	public static void _n2wpyrp6(ref Int32 _ivvongrz, ref Int32 _zmzafydt, ref Int32 _1mqgnnli, ref Int32 _bjro92m6, Single* _g0tjc1wj, Single* _x6xmgmdo, Single* _9r3shihc, ref Int32 _411n0nn5, Single* _tyzpsh18, Single* _j0np1jxc, ref Int32 _a8qh3w7g, Single* _elimqrjs)
	{
#region variable declarations
Single _uur4gpcs =  default;
Single _fcvylu1s =  default;
Single _evo84wzi =  default;
Single _9dnxhtea =  default;
Int32 _4nqkz2st =  default;
Int32 _gbsim2rn =  default;
Int32 _dmboubtu =  default;
Int32 _zipgx6y2 =  default;
Int32 _cpfio7eo =  default;
Int32 _qb0uu4i2 =  default;
Int32 _1m894xin =  default;
Int32 _w0dwmzet =  default;
Int32 _jvhv8etn =  default;
Int32 _ld42zxsk =  default;
Int32 _bb3g971f =  default;
Single _2vte4t4z =  default;
Single _wm0t3roi =  default;
Int32 _cjahrwwv =  default;
Int32 _63ekojo6 =  default;
Single _6v6pqr3c =  default;
Single _89ovlscu =  default;
Single _ycncnu8s =  default;
Single _uqwbgvu1 =  default;
Single _g9zmie6l =  default;
Single _1qf5ejnd =  default;
Int32 _kg41dm4l =  default;
Int32 _knhh7y2m =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//C 
		
		_uur4gpcs = (((float)2 * (float)4) * ILNumerics.F2NET.Intrinsics.ATAN((float)1 ));
		_fcvylu1s = (_uur4gpcs / ILNumerics.F2NET.Intrinsics.FLOAT(_zmzafydt ));
		_evo84wzi = ILNumerics.F2NET.Intrinsics.COS(_fcvylu1s );
		_9dnxhtea = ILNumerics.F2NET.Intrinsics.SIN(_fcvylu1s );
		_4nqkz2st = ((_zmzafydt + (int)1) / (int)2);
		_gbsim2rn = (_zmzafydt + (int)2);
		_dmboubtu = (_ivvongrz + (int)2);
		_zipgx6y2 = ((_ivvongrz - (int)1) / (int)2);
		if (_ivvongrz == (int)1)goto Mark119;
		{
			System.Int32 __81fgg2dlsvn243 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step243 = (System.Int32)((int)1);
			System.Int32 __81fgg2count243;
			for (__81fgg2count243 = System.Math.Max(0, (System.Int32)(((System.Int32)(_bjro92m6) - __81fgg2dlsvn243 + __81fgg2step243) / __81fgg2step243)), _cpfio7eo = __81fgg2dlsvn243; __81fgg2count243 != 0; __81fgg2count243--, _cpfio7eo += (__81fgg2step243)) {

			{
				
				*(_j0np1jxc+((int)1 - 1) + (_cpfio7eo - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_bjro92m6)) = *(_9r3shihc+((int)1 - 1) + (_cpfio7eo - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_bjro92m6));
Mark101:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn244 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step244 = (System.Int32)((int)1);
			System.Int32 __81fgg2count244;
			for (__81fgg2count244 = System.Math.Max(0, (System.Int32)(((System.Int32)(_zmzafydt) - __81fgg2dlsvn244 + __81fgg2step244) / __81fgg2step244)), _qb0uu4i2 = __81fgg2dlsvn244; __81fgg2count244 != 0; __81fgg2count244--, _qb0uu4i2 += (__81fgg2step244)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn245 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step245 = (System.Int32)((int)1);
					System.Int32 __81fgg2count245;
					for (__81fgg2count245 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn245 + __81fgg2step245) / __81fgg2step245)), _1m894xin = __81fgg2dlsvn245; __81fgg2count245 != 0; __81fgg2count245--, _1m894xin += (__81fgg2step245)) {

					{
						
						*(_tyzpsh18+((int)1 - 1) + ((int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = *(_x6xmgmdo+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli));
Mark102:;
						// continue
					}
										}				}
Mark103:;
				// continue
			}
						}		}
		if (_zipgx6y2 > _1mqgnnli)goto Mark107;
		_w0dwmzet = (-(_ivvongrz));
		{
			System.Int32 __81fgg2dlsvn246 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step246 = (System.Int32)((int)1);
			System.Int32 __81fgg2count246;
			for (__81fgg2count246 = System.Math.Max(0, (System.Int32)(((System.Int32)(_zmzafydt) - __81fgg2dlsvn246 + __81fgg2step246) / __81fgg2step246)), _qb0uu4i2 = __81fgg2dlsvn246; __81fgg2count246 != 0; __81fgg2count246--, _qb0uu4i2 += (__81fgg2step246)) {

			{
				
				_w0dwmzet = (_w0dwmzet + _ivvongrz);
				_jvhv8etn = _w0dwmzet;
				{
					System.Int32 __81fgg2dlsvn247 = (System.Int32)((int)3);
					System.Int32 __81fgg2step247 = (System.Int32)((int)2);
					System.Int32 __81fgg2count247;
					for (__81fgg2count247 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ivvongrz) - __81fgg2dlsvn247 + __81fgg2step247) / __81fgg2step247)), _ld42zxsk = __81fgg2dlsvn247; __81fgg2count247 != 0; __81fgg2count247--, _ld42zxsk += (__81fgg2step247)) {

					{
						
						_jvhv8etn = (_jvhv8etn + (int)2);
						{
							System.Int32 __81fgg2dlsvn248 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step248 = (System.Int32)((int)1);
							System.Int32 __81fgg2count248;
							for (__81fgg2count248 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn248 + __81fgg2step248) / __81fgg2step248)), _1m894xin = __81fgg2dlsvn248; __81fgg2count248 != 0; __81fgg2count248--, _1m894xin += (__81fgg2step248)) {

							{
								
								*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = ((*(_elimqrjs+(_jvhv8etn - (int)1 - 1)) * *(_x6xmgmdo+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_elimqrjs+(_jvhv8etn - 1)) * *(_x6xmgmdo+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))));
								*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = ((*(_elimqrjs+(_jvhv8etn - (int)1 - 1)) * *(_x6xmgmdo+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_elimqrjs+(_jvhv8etn - 1)) * *(_x6xmgmdo+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))));
Mark104:;
								// continue
							}
														}						}
Mark105:;
						// continue
					}
										}				}
Mark106:;
				// continue
			}
						}		}goto Mark111;
Mark107:;
		
		_w0dwmzet = (-(_ivvongrz));
		{
			System.Int32 __81fgg2dlsvn249 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step249 = (System.Int32)((int)1);
			System.Int32 __81fgg2count249;
			for (__81fgg2count249 = System.Math.Max(0, (System.Int32)(((System.Int32)(_zmzafydt) - __81fgg2dlsvn249 + __81fgg2step249) / __81fgg2step249)), _qb0uu4i2 = __81fgg2dlsvn249; __81fgg2count249 != 0; __81fgg2count249--, _qb0uu4i2 += (__81fgg2step249)) {

			{
				
				_w0dwmzet = (_w0dwmzet + _ivvongrz);
				{
					System.Int32 __81fgg2dlsvn250 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step250 = (System.Int32)((int)1);
					System.Int32 __81fgg2count250;
					for (__81fgg2count250 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn250 + __81fgg2step250) / __81fgg2step250)), _1m894xin = __81fgg2dlsvn250; __81fgg2count250 != 0; __81fgg2count250--, _1m894xin += (__81fgg2step250)) {

					{
						
						_jvhv8etn = _w0dwmzet;
						{
							System.Int32 __81fgg2dlsvn251 = (System.Int32)((int)3);
							System.Int32 __81fgg2step251 = (System.Int32)((int)2);
							System.Int32 __81fgg2count251;
							for (__81fgg2count251 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ivvongrz) - __81fgg2dlsvn251 + __81fgg2step251) / __81fgg2step251)), _ld42zxsk = __81fgg2dlsvn251; __81fgg2count251 != 0; __81fgg2count251--, _ld42zxsk += (__81fgg2step251)) {

							{
								
								_jvhv8etn = (_jvhv8etn + (int)2);
								*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = ((*(_elimqrjs+(_jvhv8etn - (int)1 - 1)) * *(_x6xmgmdo+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_elimqrjs+(_jvhv8etn - 1)) * *(_x6xmgmdo+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))));
								*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = ((*(_elimqrjs+(_jvhv8etn - (int)1 - 1)) * *(_x6xmgmdo+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_elimqrjs+(_jvhv8etn - 1)) * *(_x6xmgmdo+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))));
Mark108:;
								// continue
							}
														}						}
Mark109:;
						// continue
					}
										}				}
Mark110:;
				// continue
			}
						}		}
Mark111:;
		
		if (_zipgx6y2 < _1mqgnnli)goto Mark115;
		{
			System.Int32 __81fgg2dlsvn252 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step252 = (System.Int32)((int)1);
			System.Int32 __81fgg2count252;
			for (__81fgg2count252 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4nqkz2st) - __81fgg2dlsvn252 + __81fgg2step252) / __81fgg2step252)), _qb0uu4i2 = __81fgg2dlsvn252; __81fgg2count252 != 0; __81fgg2count252--, _qb0uu4i2 += (__81fgg2step252)) {

			{
				
				_bb3g971f = (_gbsim2rn - _qb0uu4i2);
				{
					System.Int32 __81fgg2dlsvn253 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step253 = (System.Int32)((int)1);
					System.Int32 __81fgg2count253;
					for (__81fgg2count253 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn253 + __81fgg2step253) / __81fgg2step253)), _1m894xin = __81fgg2dlsvn253; __81fgg2count253 != 0; __81fgg2count253--, _1m894xin += (__81fgg2step253)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn254 = (System.Int32)((int)3);
							System.Int32 __81fgg2step254 = (System.Int32)((int)2);
							System.Int32 __81fgg2count254;
							for (__81fgg2count254 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ivvongrz) - __81fgg2dlsvn254 + __81fgg2step254) / __81fgg2step254)), _ld42zxsk = __81fgg2dlsvn254; __81fgg2count254 != 0; __81fgg2count254--, _ld42zxsk += (__81fgg2step254)) {

							{
								
								*(_x6xmgmdo+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) = (*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) + *(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)));
								*(_x6xmgmdo+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) = (*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) - *(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)));
								*(_x6xmgmdo+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) = (*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) + *(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)));
								*(_x6xmgmdo+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) = (*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) - *(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)));
Mark112:;
								// continue
							}
														}						}
Mark113:;
						// continue
					}
										}				}
Mark114:;
				// continue
			}
						}		}goto Mark121;
Mark115:;
		
		{
			System.Int32 __81fgg2dlsvn255 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step255 = (System.Int32)((int)1);
			System.Int32 __81fgg2count255;
			for (__81fgg2count255 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4nqkz2st) - __81fgg2dlsvn255 + __81fgg2step255) / __81fgg2step255)), _qb0uu4i2 = __81fgg2dlsvn255; __81fgg2count255 != 0; __81fgg2count255--, _qb0uu4i2 += (__81fgg2step255)) {

			{
				
				_bb3g971f = (_gbsim2rn - _qb0uu4i2);
				{
					System.Int32 __81fgg2dlsvn256 = (System.Int32)((int)3);
					System.Int32 __81fgg2step256 = (System.Int32)((int)2);
					System.Int32 __81fgg2count256;
					for (__81fgg2count256 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ivvongrz) - __81fgg2dlsvn256 + __81fgg2step256) / __81fgg2step256)), _ld42zxsk = __81fgg2dlsvn256; __81fgg2count256 != 0; __81fgg2count256--, _ld42zxsk += (__81fgg2step256)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn257 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step257 = (System.Int32)((int)1);
							System.Int32 __81fgg2count257;
							for (__81fgg2count257 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn257 + __81fgg2step257) / __81fgg2step257)), _1m894xin = __81fgg2dlsvn257; __81fgg2count257 != 0; __81fgg2count257--, _1m894xin += (__81fgg2step257)) {

							{
								
								*(_x6xmgmdo+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) = (*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) + *(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)));
								*(_x6xmgmdo+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) = (*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) - *(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)));
								*(_x6xmgmdo+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) = (*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) + *(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)));
								*(_x6xmgmdo+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) = (*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) - *(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)));
Mark116:;
								// continue
							}
														}						}
Mark117:;
						// continue
					}
										}				}
Mark118:;
				// continue
			}
						}		}goto Mark121;
Mark119:;
		
		{
			System.Int32 __81fgg2dlsvn258 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step258 = (System.Int32)((int)1);
			System.Int32 __81fgg2count258;
			for (__81fgg2count258 = System.Math.Max(0, (System.Int32)(((System.Int32)(_bjro92m6) - __81fgg2dlsvn258 + __81fgg2step258) / __81fgg2step258)), _cpfio7eo = __81fgg2dlsvn258; __81fgg2count258 != 0; __81fgg2count258--, _cpfio7eo += (__81fgg2step258)) {

			{
				
				*(_9r3shihc+((int)1 - 1) + (_cpfio7eo - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_bjro92m6)) = *(_j0np1jxc+((int)1 - 1) + (_cpfio7eo - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_bjro92m6));
Mark120:;
				// continue
			}
						}		}
Mark121:;
		
		{
			System.Int32 __81fgg2dlsvn259 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step259 = (System.Int32)((int)1);
			System.Int32 __81fgg2count259;
			for (__81fgg2count259 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4nqkz2st) - __81fgg2dlsvn259 + __81fgg2step259) / __81fgg2step259)), _qb0uu4i2 = __81fgg2dlsvn259; __81fgg2count259 != 0; __81fgg2count259--, _qb0uu4i2 += (__81fgg2step259)) {

			{
				
				_bb3g971f = (_gbsim2rn - _qb0uu4i2);
				{
					System.Int32 __81fgg2dlsvn260 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step260 = (System.Int32)((int)1);
					System.Int32 __81fgg2count260;
					for (__81fgg2count260 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn260 + __81fgg2step260) / __81fgg2step260)), _1m894xin = __81fgg2dlsvn260; __81fgg2count260 != 0; __81fgg2count260--, _1m894xin += (__81fgg2step260)) {

					{
						
						*(_x6xmgmdo+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) = (*(_tyzpsh18+((int)1 - 1) + ((int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) + *(_tyzpsh18+((int)1 - 1) + ((int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)));
						*(_x6xmgmdo+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) = (*(_tyzpsh18+((int)1 - 1) + ((int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) - *(_tyzpsh18+((int)1 - 1) + ((int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)));
Mark122:;
						// continue
					}
										}				}
Mark123:;
				// continue
			}
						}		}//C 
		
		_2vte4t4z = (float)1;
		_wm0t3roi = (float)0;
		{
			System.Int32 __81fgg2dlsvn261 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step261 = (System.Int32)((int)1);
			System.Int32 __81fgg2count261;
			for (__81fgg2count261 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4nqkz2st) - __81fgg2dlsvn261 + __81fgg2step261) / __81fgg2step261)), _cjahrwwv = __81fgg2dlsvn261; __81fgg2count261 != 0; __81fgg2count261--, _cjahrwwv += (__81fgg2step261)) {

			{
				
				_63ekojo6 = (_gbsim2rn - _cjahrwwv);
				_6v6pqr3c = ((_evo84wzi * _2vte4t4z) - (_9dnxhtea * _wm0t3roi));
				_wm0t3roi = ((_evo84wzi * _wm0t3roi) + (_9dnxhtea * _2vte4t4z));
				_2vte4t4z = _6v6pqr3c;
				{
					System.Int32 __81fgg2dlsvn262 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step262 = (System.Int32)((int)1);
					System.Int32 __81fgg2count262;
					for (__81fgg2count262 = System.Math.Max(0, (System.Int32)(((System.Int32)(_bjro92m6) - __81fgg2dlsvn262 + __81fgg2step262) / __81fgg2step262)), _cpfio7eo = __81fgg2dlsvn262; __81fgg2count262 != 0; __81fgg2count262--, _cpfio7eo += (__81fgg2step262)) {

					{
						
						*(_j0np1jxc+((int)1 - 1) + (_cpfio7eo - 1) * 1 * (_a8qh3w7g) + (_cjahrwwv - 1) * 1 * (_a8qh3w7g) * (_bjro92m6)) = (*(_9r3shihc+((int)1 - 1) + (_cpfio7eo - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_bjro92m6)) + (_2vte4t4z * *(_9r3shihc+((int)1 - 1) + (_cpfio7eo - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_bjro92m6))));
						*(_j0np1jxc+((int)1 - 1) + (_cpfio7eo - 1) * 1 * (_a8qh3w7g) + (_63ekojo6 - 1) * 1 * (_a8qh3w7g) * (_bjro92m6)) = (_wm0t3roi * *(_9r3shihc+((int)1 - 1) + (_cpfio7eo - 1) * 1 * (_411n0nn5) + (_zmzafydt - 1) * 1 * (_411n0nn5) * (_bjro92m6)));
Mark124:;
						// continue
					}
										}				}
				_89ovlscu = _2vte4t4z;
				_ycncnu8s = _wm0t3roi;
				_uqwbgvu1 = _2vte4t4z;
				_g9zmie6l = _wm0t3roi;
				{
					System.Int32 __81fgg2dlsvn263 = (System.Int32)((int)3);
					const System.Int32 __81fgg2step263 = (System.Int32)((int)1);
					System.Int32 __81fgg2count263;
					for (__81fgg2count263 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4nqkz2st) - __81fgg2dlsvn263 + __81fgg2step263) / __81fgg2step263)), _qb0uu4i2 = __81fgg2dlsvn263; __81fgg2count263 != 0; __81fgg2count263--, _qb0uu4i2 += (__81fgg2step263)) {

					{
						
						_bb3g971f = (_gbsim2rn - _qb0uu4i2);
						_1qf5ejnd = ((_89ovlscu * _uqwbgvu1) - (_ycncnu8s * _g9zmie6l));
						_g9zmie6l = ((_89ovlscu * _g9zmie6l) + (_ycncnu8s * _uqwbgvu1));
						_uqwbgvu1 = _1qf5ejnd;
						{
							System.Int32 __81fgg2dlsvn264 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step264 = (System.Int32)((int)1);
							System.Int32 __81fgg2count264;
							for (__81fgg2count264 = System.Math.Max(0, (System.Int32)(((System.Int32)(_bjro92m6) - __81fgg2dlsvn264 + __81fgg2step264) / __81fgg2step264)), _cpfio7eo = __81fgg2dlsvn264; __81fgg2count264 != 0; __81fgg2count264--, _cpfio7eo += (__81fgg2step264)) {

							{
								
								*(_j0np1jxc+((int)1 - 1) + (_cpfio7eo - 1) * 1 * (_a8qh3w7g) + (_cjahrwwv - 1) * 1 * (_a8qh3w7g) * (_bjro92m6)) = (*(_j0np1jxc+((int)1 - 1) + (_cpfio7eo - 1) * 1 * (_a8qh3w7g) + (_cjahrwwv - 1) * 1 * (_a8qh3w7g) * (_bjro92m6)) + (_uqwbgvu1 * *(_9r3shihc+((int)1 - 1) + (_cpfio7eo - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_bjro92m6))));
								*(_j0np1jxc+((int)1 - 1) + (_cpfio7eo - 1) * 1 * (_a8qh3w7g) + (_63ekojo6 - 1) * 1 * (_a8qh3w7g) * (_bjro92m6)) = (*(_j0np1jxc+((int)1 - 1) + (_cpfio7eo - 1) * 1 * (_a8qh3w7g) + (_63ekojo6 - 1) * 1 * (_a8qh3w7g) * (_bjro92m6)) + (_g9zmie6l * *(_9r3shihc+((int)1 - 1) + (_cpfio7eo - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_bjro92m6))));
Mark125:;
								// continue
							}
														}						}
Mark126:;
						// continue
					}
										}				}
Mark127:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn265 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step265 = (System.Int32)((int)1);
			System.Int32 __81fgg2count265;
			for (__81fgg2count265 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4nqkz2st) - __81fgg2dlsvn265 + __81fgg2step265) / __81fgg2step265)), _qb0uu4i2 = __81fgg2dlsvn265; __81fgg2count265 != 0; __81fgg2count265--, _qb0uu4i2 += (__81fgg2step265)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn266 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step266 = (System.Int32)((int)1);
					System.Int32 __81fgg2count266;
					for (__81fgg2count266 = System.Math.Max(0, (System.Int32)(((System.Int32)(_bjro92m6) - __81fgg2dlsvn266 + __81fgg2step266) / __81fgg2step266)), _cpfio7eo = __81fgg2dlsvn266; __81fgg2count266 != 0; __81fgg2count266--, _cpfio7eo += (__81fgg2step266)) {

					{
						
						*(_j0np1jxc+((int)1 - 1) + (_cpfio7eo - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_bjro92m6)) = (*(_j0np1jxc+((int)1 - 1) + (_cpfio7eo - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_bjro92m6)) + *(_9r3shihc+((int)1 - 1) + (_cpfio7eo - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_bjro92m6)));
Mark128:;
						// continue
					}
										}				}
Mark129:;
				// continue
			}
						}		}//C 
		
		if (_ivvongrz < _1mqgnnli)goto Mark132;
		{
			System.Int32 __81fgg2dlsvn267 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step267 = (System.Int32)((int)1);
			System.Int32 __81fgg2count267;
			for (__81fgg2count267 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn267 + __81fgg2step267) / __81fgg2step267)), _1m894xin = __81fgg2dlsvn267; __81fgg2count267 != 0; __81fgg2count267--, _1m894xin += (__81fgg2step267)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn268 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step268 = (System.Int32)((int)1);
					System.Int32 __81fgg2count268;
					for (__81fgg2count268 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ivvongrz) - __81fgg2dlsvn268 + __81fgg2step268) / __81fgg2step268)), _ld42zxsk = __81fgg2dlsvn268; __81fgg2count268 != 0; __81fgg2count268--, _ld42zxsk += (__81fgg2step268)) {

					{
						
						*(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_zmzafydt)) = *(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli));
Mark130:;
						// continue
					}
										}				}
Mark131:;
				// continue
			}
						}		}goto Mark135;
Mark132:;
		
		{
			System.Int32 __81fgg2dlsvn269 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step269 = (System.Int32)((int)1);
			System.Int32 __81fgg2count269;
			for (__81fgg2count269 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ivvongrz) - __81fgg2dlsvn269 + __81fgg2step269) / __81fgg2step269)), _ld42zxsk = __81fgg2dlsvn269; __81fgg2count269 != 0; __81fgg2count269--, _ld42zxsk += (__81fgg2step269)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn270 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step270 = (System.Int32)((int)1);
					System.Int32 __81fgg2count270;
					for (__81fgg2count270 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn270 + __81fgg2step270) / __81fgg2step270)), _1m894xin = __81fgg2dlsvn270; __81fgg2count270 != 0; __81fgg2count270--, _1m894xin += (__81fgg2step270)) {

					{
						
						*(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_zmzafydt)) = *(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli));
Mark133:;
						// continue
					}
										}				}
Mark134:;
				// continue
			}
						}		}
Mark135:;
		
		{
			System.Int32 __81fgg2dlsvn271 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step271 = (System.Int32)((int)1);
			System.Int32 __81fgg2count271;
			for (__81fgg2count271 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4nqkz2st) - __81fgg2dlsvn271 + __81fgg2step271) / __81fgg2step271)), _qb0uu4i2 = __81fgg2dlsvn271; __81fgg2count271 != 0; __81fgg2count271--, _qb0uu4i2 += (__81fgg2step271)) {

			{
				
				_bb3g971f = (_gbsim2rn - _qb0uu4i2);
				_kg41dm4l = (_qb0uu4i2 + _qb0uu4i2);
				{
					System.Int32 __81fgg2dlsvn272 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step272 = (System.Int32)((int)1);
					System.Int32 __81fgg2count272;
					for (__81fgg2count272 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn272 + __81fgg2step272) / __81fgg2step272)), _1m894xin = __81fgg2dlsvn272; __81fgg2count272 != 0; __81fgg2count272--, _1m894xin += (__81fgg2step272)) {

					{
						
						*(_g0tjc1wj+((int)1 - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + (_kg41dm4l - (int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_zmzafydt)) = *(_tyzpsh18+((int)1 - 1) + ((int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli));
						*(_g0tjc1wj+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_kg41dm4l - (int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_zmzafydt)) = *(_tyzpsh18+((int)1 - 1) + ((int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli));
Mark136:;
						// continue
					}
										}				}
Mark137:;
				// continue
			}
						}		}
		if (_ivvongrz == (int)1)
		return;
		if (_zipgx6y2 < _1mqgnnli)goto Mark141;
		{
			System.Int32 __81fgg2dlsvn273 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step273 = (System.Int32)((int)1);
			System.Int32 __81fgg2count273;
			for (__81fgg2count273 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4nqkz2st) - __81fgg2dlsvn273 + __81fgg2step273) / __81fgg2step273)), _qb0uu4i2 = __81fgg2dlsvn273; __81fgg2count273 != 0; __81fgg2count273--, _qb0uu4i2 += (__81fgg2step273)) {

			{
				
				_bb3g971f = (_gbsim2rn - _qb0uu4i2);
				_kg41dm4l = (_qb0uu4i2 + _qb0uu4i2);
				{
					System.Int32 __81fgg2dlsvn274 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step274 = (System.Int32)((int)1);
					System.Int32 __81fgg2count274;
					for (__81fgg2count274 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn274 + __81fgg2step274) / __81fgg2step274)), _1m894xin = __81fgg2dlsvn274; __81fgg2count274 != 0; __81fgg2count274--, _1m894xin += (__81fgg2step274)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn275 = (System.Int32)((int)3);
							System.Int32 __81fgg2step275 = (System.Int32)((int)2);
							System.Int32 __81fgg2count275;
							for (__81fgg2count275 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ivvongrz) - __81fgg2dlsvn275 + __81fgg2step275) / __81fgg2step275)), _ld42zxsk = __81fgg2dlsvn275; __81fgg2count275 != 0; __81fgg2count275--, _ld42zxsk += (__81fgg2step275)) {

							{
								
								_knhh7y2m = (_dmboubtu - _ld42zxsk);
								*(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_kg41dm4l - (int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_zmzafydt)) = (*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) + *(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)));
								*(_g0tjc1wj+((int)1 - 1) + (_knhh7y2m - (int)1 - 1) * 1 * (_411n0nn5) + (_kg41dm4l - (int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_zmzafydt)) = (*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) - *(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)));
								*(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_kg41dm4l - (int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_zmzafydt)) = (*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) + *(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)));
								*(_g0tjc1wj+((int)1 - 1) + (_knhh7y2m - 1) * 1 * (_411n0nn5) + (_kg41dm4l - (int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_zmzafydt)) = (*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) - *(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)));
Mark138:;
								// continue
							}
														}						}
Mark139:;
						// continue
					}
										}				}
Mark140:;
				// continue
			}
						}		}
		return;
Mark141:;
		
		{
			System.Int32 __81fgg2dlsvn276 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step276 = (System.Int32)((int)1);
			System.Int32 __81fgg2count276;
			for (__81fgg2count276 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4nqkz2st) - __81fgg2dlsvn276 + __81fgg2step276) / __81fgg2step276)), _qb0uu4i2 = __81fgg2dlsvn276; __81fgg2count276 != 0; __81fgg2count276--, _qb0uu4i2 += (__81fgg2step276)) {

			{
				
				_bb3g971f = (_gbsim2rn - _qb0uu4i2);
				_kg41dm4l = (_qb0uu4i2 + _qb0uu4i2);
				{
					System.Int32 __81fgg2dlsvn277 = (System.Int32)((int)3);
					System.Int32 __81fgg2step277 = (System.Int32)((int)2);
					System.Int32 __81fgg2count277;
					for (__81fgg2count277 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ivvongrz) - __81fgg2dlsvn277 + __81fgg2step277) / __81fgg2step277)), _ld42zxsk = __81fgg2dlsvn277; __81fgg2count277 != 0; __81fgg2count277--, _ld42zxsk += (__81fgg2step277)) {

					{
						
						_knhh7y2m = (_dmboubtu - _ld42zxsk);
						{
							System.Int32 __81fgg2dlsvn278 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step278 = (System.Int32)((int)1);
							System.Int32 __81fgg2count278;
							for (__81fgg2count278 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn278 + __81fgg2step278) / __81fgg2step278)), _1m894xin = __81fgg2dlsvn278; __81fgg2count278 != 0; __81fgg2count278--, _1m894xin += (__81fgg2step278)) {

							{
								
								*(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_kg41dm4l - (int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_zmzafydt)) = (*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) + *(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)));
								*(_g0tjc1wj+((int)1 - 1) + (_knhh7y2m - (int)1 - 1) * 1 * (_411n0nn5) + (_kg41dm4l - (int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_zmzafydt)) = (*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) - *(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)));
								*(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_kg41dm4l - (int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_zmzafydt)) = (*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) + *(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)));
								*(_g0tjc1wj+((int)1 - 1) + (_knhh7y2m - 1) * 1 * (_411n0nn5) + (_kg41dm4l - (int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_zmzafydt)) = (*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) - *(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)));
Mark142:;
								// continue
							}
														}						}
Mark143:;
						// continue
					}
										}				}
Mark144:;
				// continue
			}
						}		}
		return;
	}
	
	} // 177

} // end class 
} // end namespace
#endif
