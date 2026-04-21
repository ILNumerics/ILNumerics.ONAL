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

	 
	public static void _7fb1iozl(ref Int32 _ivvongrz, ref Int32 _1mqgnnli, Single* _g0tjc1wj, ref Int32 _411n0nn5, Single* _tyzpsh18, ref Int32 _a8qh3w7g, Single* _t99b7n0f, Single* _g45v2kzv)
	{
#region variable declarations
Single _fcvylu1s =  default;
Single _qhqyhuml =  default;
Single _6zfj7kh8 =  default;
Int32 _1m894xin =  default;
Int32 _dmboubtu =  default;
Int32 _ld42zxsk =  default;
Int32 _knhh7y2m =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//C 
		
		_fcvylu1s = ((((float)2 * (float)4) * ILNumerics.F2NET.Intrinsics.ATAN((float)1 )) / (float)3);
		_qhqyhuml = ILNumerics.F2NET.Intrinsics.COS(_fcvylu1s );
		_6zfj7kh8 = ILNumerics.F2NET.Intrinsics.SIN(_fcvylu1s );
		{
			System.Int32 __81fgg2dlsvn237 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step237 = (System.Int32)((int)1);
			System.Int32 __81fgg2count237;
			for (__81fgg2count237 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn237 + __81fgg2step237) / __81fgg2step237)), _1m894xin = __81fgg2dlsvn237; __81fgg2count237 != 0; __81fgg2count237--, _1m894xin += (__81fgg2step237)) {

			{
				
				*(_tyzpsh18+((int)1 - 1) + ((int)1 - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)3)) = (*(_g0tjc1wj+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) + (*(_g0tjc1wj+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) + *(_g0tjc1wj+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))));
				*(_tyzpsh18+((int)1 - 1) + ((int)1 - 1) * 1 * (_a8qh3w7g) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)3)) = (_6zfj7kh8 * (*(_g0tjc1wj+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) - *(_g0tjc1wj+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))));
				*(_tyzpsh18+((int)1 - 1) + (_ivvongrz - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)3)) = (*(_g0tjc1wj+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) + (_qhqyhuml * (*(_g0tjc1wj+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) + *(_g0tjc1wj+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))));
Mark101:;
				// continue
			}
						}		}
		if (_ivvongrz == (int)1)
		return;
		_dmboubtu = (_ivvongrz + (int)2);
		{
			System.Int32 __81fgg2dlsvn238 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step238 = (System.Int32)((int)1);
			System.Int32 __81fgg2count238;
			for (__81fgg2count238 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn238 + __81fgg2step238) / __81fgg2step238)), _1m894xin = __81fgg2dlsvn238; __81fgg2count238 != 0; __81fgg2count238--, _1m894xin += (__81fgg2step238)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn239 = (System.Int32)((int)3);
					System.Int32 __81fgg2step239 = (System.Int32)((int)2);
					System.Int32 __81fgg2count239;
					for (__81fgg2count239 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ivvongrz) - __81fgg2dlsvn239 + __81fgg2step239) / __81fgg2step239)), _ld42zxsk = __81fgg2dlsvn239; __81fgg2count239 != 0; __81fgg2count239--, _ld42zxsk += (__81fgg2step239)) {

					{
						
						_knhh7y2m = (_dmboubtu - _ld42zxsk);
						*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)3)) = (*(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) + (((*(_t99b7n0f+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_t99b7n0f+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))) + ((*(_g45v2kzv+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_g45v2kzv+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))))));
						*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)3)) = (*(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) + (((*(_t99b7n0f+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_t99b7n0f+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))) + ((*(_g45v2kzv+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_g45v2kzv+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))))));
						*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)3)) = ((*(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) + (_qhqyhuml * (((*(_t99b7n0f+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_t99b7n0f+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))) + ((*(_g45v2kzv+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_g45v2kzv+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))))))) + (_6zfj7kh8 * (((*(_t99b7n0f+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_t99b7n0f+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))) - ((*(_g45v2kzv+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_g45v2kzv+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))))));
						*(_tyzpsh18+((int)1 - 1) + (_knhh7y2m - (int)1 - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)3)) = ((*(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) + (_qhqyhuml * (((*(_t99b7n0f+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_t99b7n0f+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))) + ((*(_g45v2kzv+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_g45v2kzv+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))))))) - (_6zfj7kh8 * (((*(_t99b7n0f+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_t99b7n0f+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))) - ((*(_g45v2kzv+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_g45v2kzv+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))))));
						*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)3)) = ((*(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) + (_qhqyhuml * (((*(_t99b7n0f+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_t99b7n0f+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))) + ((*(_g45v2kzv+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_g45v2kzv+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))))))) + (_6zfj7kh8 * (((*(_g45v2kzv+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_g45v2kzv+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))) - ((*(_t99b7n0f+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_t99b7n0f+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))))));
						*(_tyzpsh18+((int)1 - 1) + (_knhh7y2m - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)3)) = ((_6zfj7kh8 * (((*(_g45v2kzv+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_g45v2kzv+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))) - ((*(_t99b7n0f+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_t99b7n0f+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))))) - (*(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) + (_qhqyhuml * (((*(_t99b7n0f+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_t99b7n0f+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))) + ((*(_g45v2kzv+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_g45v2kzv+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))))))));
Mark102:;
						// continue
					}
										}				}
Mark103:;
				// continue
			}
						}		}
		return;
	}
	
	} // 177

} // end class 
} // end namespace
#endif
