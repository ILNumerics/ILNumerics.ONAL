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

	 
	public static void _teedmsbo(ref Int32 _cf4kqqk2, ref Int32 _ivvongrz, ref Int32 _1mqgnnli, Single* _g0tjc1wj, ref Int32 _yj2u3o8x, ref Int32 _411n0nn5, Single* _tyzpsh18, ref Int32 _28cbiyim, ref Int32 _a8qh3w7g, Single* _t99b7n0f, Single* _g45v2kzv, Single* _zt7ko2c4)
	{
#region variable declarations
Int32 _dpill1hb =  default;
Int32 _q6xocrqe =  default;
Single _h2si43vn =  default;
Int32 _1m894xin =  default;
Int32 _34fm7prn =  default;
Int32 _010vumne =  default;
Int32 _dmboubtu =  default;
Int32 _ld42zxsk =  default;
Int32 _knhh7y2m =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//C 
		
		_dpill1hb = (((_cf4kqqk2 - (int)1) * _yj2u3o8x) + (int)1);
		_q6xocrqe = ((int)1 - _28cbiyim);
		_h2si43vn = ILNumerics.F2NET.Intrinsics.SQRT((float)2 );
		{
			System.Int32 __81fgg2dlsvn436 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step436 = (System.Int32)((int)1);
			System.Int32 __81fgg2count436;
			for (__81fgg2count436 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn436 + __81fgg2step436) / __81fgg2step436)), _1m894xin = __81fgg2dlsvn436; __81fgg2count436 != 0; __81fgg2count436--, _1m894xin += (__81fgg2step436)) {

			{
				
				_34fm7prn = _q6xocrqe;
				{
					System.Int32 __81fgg2dlsvn437 = (System.Int32)((int)1);
					System.Int32 __81fgg2step437 = (System.Int32)(_yj2u3o8x);
					System.Int32 __81fgg2count437;
					for (__81fgg2count437 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dpill1hb) - __81fgg2dlsvn437 + __81fgg2step437) / __81fgg2step437)), _010vumne = __81fgg2dlsvn437; __81fgg2count437 != 0; __81fgg2count437--, _010vumne += (__81fgg2step437)) {

					{
						
						_34fm7prn = (_34fm7prn + _28cbiyim);
						*(_tyzpsh18+(_34fm7prn - 1) + ((int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = ((*(_g0tjc1wj+(_010vumne - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) + *(_g0tjc1wj+(_010vumne - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))) - (*(_g0tjc1wj+(_010vumne - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) + *(_g0tjc1wj+(_010vumne - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))));
						*(_tyzpsh18+(_34fm7prn - 1) + ((int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = ((*(_g0tjc1wj+(_010vumne - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) + *(_g0tjc1wj+(_010vumne - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))) + (*(_g0tjc1wj+(_010vumne - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) + *(_g0tjc1wj+(_010vumne - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))));
						*(_tyzpsh18+(_34fm7prn - 1) + ((int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + ((int)4 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = ((*(_g0tjc1wj+(_010vumne - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) - *(_g0tjc1wj+(_010vumne - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))) + (*(_g0tjc1wj+(_010vumne - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) + *(_g0tjc1wj+(_010vumne - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))));
						*(_tyzpsh18+(_34fm7prn - 1) + ((int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = ((*(_g0tjc1wj+(_010vumne - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) - *(_g0tjc1wj+(_010vumne - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))) - (*(_g0tjc1wj+(_010vumne - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) + *(_g0tjc1wj+(_010vumne - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))));
Mark1001:;
						// continue
					}
										}				}
Mark101:;
				// continue
			}
						}		}if ((_ivvongrz - (int)2) < 0) goto Mark107; else if ((_ivvongrz - (int)2) == 0) goto Mark105; else goto Mark102;
Mark102:;
		
		_dmboubtu = (_ivvongrz + (int)2);
		{
			System.Int32 __81fgg2dlsvn438 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step438 = (System.Int32)((int)1);
			System.Int32 __81fgg2count438;
			for (__81fgg2count438 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn438 + __81fgg2step438) / __81fgg2step438)), _1m894xin = __81fgg2dlsvn438; __81fgg2count438 != 0; __81fgg2count438--, _1m894xin += (__81fgg2step438)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn439 = (System.Int32)((int)3);
					System.Int32 __81fgg2step439 = (System.Int32)((int)2);
					System.Int32 __81fgg2count439;
					for (__81fgg2count439 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ivvongrz) - __81fgg2dlsvn439 + __81fgg2step439) / __81fgg2step439)), _ld42zxsk = __81fgg2dlsvn439; __81fgg2count439 != 0; __81fgg2count439--, _ld42zxsk += (__81fgg2step439)) {

					{
						
						_knhh7y2m = (_dmboubtu - _ld42zxsk);
						_34fm7prn = _q6xocrqe;
						{
							System.Int32 __81fgg2dlsvn440 = (System.Int32)((int)1);
							System.Int32 __81fgg2step440 = (System.Int32)(_yj2u3o8x);
							System.Int32 __81fgg2count440;
							for (__81fgg2count440 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dpill1hb) - __81fgg2dlsvn440 + __81fgg2step440) / __81fgg2step440)), _010vumne = __81fgg2dlsvn440; __81fgg2count440 != 0; __81fgg2count440--, _010vumne += (__81fgg2step440)) {

							{
								
								_34fm7prn = (_34fm7prn + _28cbiyim);
								*(_tyzpsh18+(_34fm7prn - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = ((*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) + *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - (int)1 - 1) * 1 * (_411n0nn5) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))) + (*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) + *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - (int)1 - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))));
								*(_tyzpsh18+(_34fm7prn - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = ((*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) - *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - 1) * 1 * (_411n0nn5) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))) + (*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) - *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))));
								*(_tyzpsh18+(_34fm7prn - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = ((*(_t99b7n0f+(_ld42zxsk - (int)2 - 1)) * ((*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) - *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - (int)1 - 1) * 1 * (_411n0nn5) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))) - (*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) + *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))))) - (*(_t99b7n0f+(_ld42zxsk - (int)1 - 1)) * ((*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) + *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - 1) * 1 * (_411n0nn5) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))) + (*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) - *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - (int)1 - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))))));
								*(_tyzpsh18+(_34fm7prn - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = ((*(_t99b7n0f+(_ld42zxsk - (int)2 - 1)) * ((*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) + *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - 1) * 1 * (_411n0nn5) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))) + (*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) - *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - (int)1 - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))))) + (*(_t99b7n0f+(_ld42zxsk - (int)1 - 1)) * ((*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) - *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - (int)1 - 1) * 1 * (_411n0nn5) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))) - (*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) + *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))))));
								*(_tyzpsh18+(_34fm7prn - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = ((*(_g45v2kzv+(_ld42zxsk - (int)2 - 1)) * ((*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) + *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - (int)1 - 1) * 1 * (_411n0nn5) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))) - (*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) + *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - (int)1 - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))))) - (*(_g45v2kzv+(_ld42zxsk - (int)1 - 1)) * ((*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) - *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - 1) * 1 * (_411n0nn5) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))) - (*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) - *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))))));
								*(_tyzpsh18+(_34fm7prn - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = ((*(_g45v2kzv+(_ld42zxsk - (int)2 - 1)) * ((*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) - *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - 1) * 1 * (_411n0nn5) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))) - (*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) - *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))))) + (*(_g45v2kzv+(_ld42zxsk - (int)1 - 1)) * ((*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) + *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - (int)1 - 1) * 1 * (_411n0nn5) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))) - (*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) + *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - (int)1 - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))))));
								*(_tyzpsh18+(_34fm7prn - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + ((int)4 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = ((*(_zt7ko2c4+(_ld42zxsk - (int)2 - 1)) * ((*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) - *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - (int)1 - 1) * 1 * (_411n0nn5) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))) + (*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) + *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))))) - (*(_zt7ko2c4+(_ld42zxsk - (int)1 - 1)) * ((*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) + *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - 1) * 1 * (_411n0nn5) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))) - (*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) - *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - (int)1 - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))))));
								*(_tyzpsh18+(_34fm7prn - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + ((int)4 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = ((*(_zt7ko2c4+(_ld42zxsk - (int)2 - 1)) * ((*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) + *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - 1) * 1 * (_411n0nn5) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))) - (*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) - *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - (int)1 - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))))) + (*(_zt7ko2c4+(_ld42zxsk - (int)1 - 1)) * ((*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) - *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - (int)1 - 1) * 1 * (_411n0nn5) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))) + (*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) + *(_g0tjc1wj+(_010vumne - 1) + (_knhh7y2m - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))))));
Mark1002:;
								// continue
							}
														}						}
Mark103:;
						// continue
					}
										}				}
Mark104:;
				// continue
			}
						}		}
		if (ILNumerics.F2NET.Intrinsics.MOD(_ivvongrz ,(int)2 ) == (int)1)
		return;
Mark105:;
		// continue
		{
			System.Int32 __81fgg2dlsvn441 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step441 = (System.Int32)((int)1);
			System.Int32 __81fgg2count441;
			for (__81fgg2count441 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn441 + __81fgg2step441) / __81fgg2step441)), _1m894xin = __81fgg2dlsvn441; __81fgg2count441 != 0; __81fgg2count441--, _1m894xin += (__81fgg2step441)) {

			{
				
				_34fm7prn = _q6xocrqe;
				{
					System.Int32 __81fgg2dlsvn442 = (System.Int32)((int)1);
					System.Int32 __81fgg2step442 = (System.Int32)(_yj2u3o8x);
					System.Int32 __81fgg2count442;
					for (__81fgg2count442 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dpill1hb) - __81fgg2dlsvn442 + __81fgg2step442) / __81fgg2step442)), _010vumne = __81fgg2dlsvn442; __81fgg2count442 != 0; __81fgg2count442--, _010vumne += (__81fgg2step442)) {

					{
						
						_34fm7prn = (_34fm7prn + _28cbiyim);
						*(_tyzpsh18+(_34fm7prn - 1) + (_ivvongrz - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = ((*(_g0tjc1wj+(_010vumne - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) + *(_g0tjc1wj+(_010vumne - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))) + (*(_g0tjc1wj+(_010vumne - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) + *(_g0tjc1wj+(_010vumne - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))));
						*(_tyzpsh18+(_34fm7prn - 1) + (_ivvongrz - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = (_h2si43vn * ((*(_g0tjc1wj+(_010vumne - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) - *(_g0tjc1wj+(_010vumne - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))) - (*(_g0tjc1wj+(_010vumne - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) + *(_g0tjc1wj+(_010vumne - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)))));
						*(_tyzpsh18+(_34fm7prn - 1) + (_ivvongrz - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = ((*(_g0tjc1wj+(_010vumne - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) - *(_g0tjc1wj+(_010vumne - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))) + (*(_g0tjc1wj+(_010vumne - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) - *(_g0tjc1wj+(_010vumne - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))));
						*(_tyzpsh18+(_34fm7prn - 1) + (_ivvongrz - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + ((int)4 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = (-((_h2si43vn * ((*(_g0tjc1wj+(_010vumne - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) - *(_g0tjc1wj+(_010vumne - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4))) + (*(_g0tjc1wj+(_010vumne - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)) + *(_g0tjc1wj+(_010vumne - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)4)))))));
Mark1003:;
						// continue
					}
										}				}
Mark106:;
				// continue
			}
						}		}
Mark107:;
		
		return;
	}
	
	} // 177

} // end class 
} // end namespace
#endif
