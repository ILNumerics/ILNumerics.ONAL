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

	 
	public static void _f3rl2flz(ref Int32 _cf4kqqk2, ref Int32 _ivvongrz, ref Int32 _1mqgnnli, Double* _g0tjc1wj, ref Int32 _yj2u3o8x, ref Int32 _411n0nn5, Double* _tyzpsh18, ref Int32 _28cbiyim, ref Int32 _a8qh3w7g, Double* _t99b7n0f)
	{
#region variable declarations
Int32 _dpill1hb =  default;
Int32 _q6xocrqe =  default;
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
		{
			System.Int32 __81fgg2dlsvn346 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step346 = (System.Int32)((int)1);
			System.Int32 __81fgg2count346;
			for (__81fgg2count346 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn346 + __81fgg2step346) / __81fgg2step346)), _1m894xin = __81fgg2dlsvn346; __81fgg2count346 != 0; __81fgg2count346--, _1m894xin += (__81fgg2step346)) {

			{
				
				_34fm7prn = _q6xocrqe;
				{
					System.Int32 __81fgg2dlsvn347 = (System.Int32)((int)1);
					System.Int32 __81fgg2step347 = (System.Int32)(_yj2u3o8x);
					System.Int32 __81fgg2count347;
					for (__81fgg2count347 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dpill1hb) - __81fgg2dlsvn347 + __81fgg2step347) / __81fgg2step347)), _010vumne = __81fgg2dlsvn347; __81fgg2count347 != 0; __81fgg2count347--, _010vumne += (__81fgg2step347)) {

					{
						
						_34fm7prn = (_34fm7prn + _28cbiyim);
						*(_tyzpsh18+(_34fm7prn - 1) + ((int)1 - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)2)) = (*(_g0tjc1wj+(_010vumne - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) + *(_g0tjc1wj+(_010vumne - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)));
						*(_tyzpsh18+(_34fm7prn - 1) + (_ivvongrz - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)2)) = (*(_g0tjc1wj+(_010vumne - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) - *(_g0tjc1wj+(_010vumne - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)));
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
			System.Int32 __81fgg2dlsvn348 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step348 = (System.Int32)((int)1);
			System.Int32 __81fgg2count348;
			for (__81fgg2count348 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn348 + __81fgg2step348) / __81fgg2step348)), _1m894xin = __81fgg2dlsvn348; __81fgg2count348 != 0; __81fgg2count348--, _1m894xin += (__81fgg2step348)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn349 = (System.Int32)((int)3);
					System.Int32 __81fgg2step349 = (System.Int32)((int)2);
					System.Int32 __81fgg2count349;
					for (__81fgg2count349 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ivvongrz) - __81fgg2dlsvn349 + __81fgg2step349) / __81fgg2step349)), _ld42zxsk = __81fgg2dlsvn349; __81fgg2count349 != 0; __81fgg2count349--, _ld42zxsk += (__81fgg2step349)) {

					{
						
						_knhh7y2m = (_dmboubtu - _ld42zxsk);
						_34fm7prn = _q6xocrqe;
						{
							System.Int32 __81fgg2dlsvn350 = (System.Int32)((int)1);
							System.Int32 __81fgg2step350 = (System.Int32)(_yj2u3o8x);
							System.Int32 __81fgg2count350;
							for (__81fgg2count350 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dpill1hb) - __81fgg2dlsvn350 + __81fgg2step350) / __81fgg2step350)), _010vumne = __81fgg2dlsvn350; __81fgg2count350 != 0; __81fgg2count350--, _010vumne += (__81fgg2step350)) {

							{
								
								_34fm7prn = (_34fm7prn + _28cbiyim);
								*(_tyzpsh18+(_34fm7prn - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)2)) = (*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) + ((*(_t99b7n0f+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_t99b7n0f+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))));
								*(_tyzpsh18+(_34fm7prn - 1) + (_knhh7y2m - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)2)) = (((*(_t99b7n0f+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_t99b7n0f+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))) - *(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)));
								*(_tyzpsh18+(_34fm7prn - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)2)) = (*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) + ((*(_t99b7n0f+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_t99b7n0f+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))));
								*(_tyzpsh18+(_34fm7prn - 1) + (_knhh7y2m - (int)1 - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)2)) = (*(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) - ((*(_t99b7n0f+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_t99b7n0f+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+(_010vumne - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))));
Mark1003:;
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
		
		{
			System.Int32 __81fgg2dlsvn351 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step351 = (System.Int32)((int)1);
			System.Int32 __81fgg2count351;
			for (__81fgg2count351 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn351 + __81fgg2step351) / __81fgg2step351)), _1m894xin = __81fgg2dlsvn351; __81fgg2count351 != 0; __81fgg2count351--, _1m894xin += (__81fgg2step351)) {

			{
				
				_34fm7prn = _q6xocrqe;
				{
					System.Int32 __81fgg2dlsvn352 = (System.Int32)((int)1);
					System.Int32 __81fgg2step352 = (System.Int32)(_yj2u3o8x);
					System.Int32 __81fgg2count352;
					for (__81fgg2count352 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dpill1hb) - __81fgg2dlsvn352 + __81fgg2step352) / __81fgg2step352)), _010vumne = __81fgg2dlsvn352; __81fgg2count352 != 0; __81fgg2count352--, _010vumne += (__81fgg2step352)) {

					{
						
						_34fm7prn = (_34fm7prn + _28cbiyim);
						*(_tyzpsh18+(_34fm7prn - 1) + ((int)1 - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)2)) = (-(*(_g0tjc1wj+(_010vumne - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))));
						*(_tyzpsh18+(_34fm7prn - 1) + (_ivvongrz - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)2)) = *(_g0tjc1wj+(_010vumne - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli));
Mark1006:;
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
