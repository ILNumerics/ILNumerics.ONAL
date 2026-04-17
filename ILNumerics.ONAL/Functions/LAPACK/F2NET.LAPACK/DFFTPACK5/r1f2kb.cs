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

	 
	public static void _ano1ul7c(ref Int32 _ivvongrz, ref Int32 _1mqgnnli, Double* _g0tjc1wj, ref Int32 _411n0nn5, Double* _tyzpsh18, ref Int32 _a8qh3w7g, Double* _t99b7n0f)
	{
#region variable declarations
Int32 _1m894xin =  default;
Int32 _dmboubtu =  default;
Int32 _ld42zxsk =  default;
Int32 _knhh7y2m =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//C 
		
		{
			System.Int32 __81fgg2dlsvn286 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step286 = (System.Int32)((int)1);
			System.Int32 __81fgg2count286;
			for (__81fgg2count286 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn286 + __81fgg2step286) / __81fgg2step286)), _1m894xin = __81fgg2dlsvn286; __81fgg2count286 != 0; __81fgg2count286--, _1m894xin += (__81fgg2step286)) {

			{
				
				*(_tyzpsh18+((int)1 - 1) + ((int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = (*(_g0tjc1wj+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)2)) + *(_g0tjc1wj+((int)1 - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)2)));
				*(_tyzpsh18+((int)1 - 1) + ((int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = (*(_g0tjc1wj+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)2)) - *(_g0tjc1wj+((int)1 - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)2)));
Mark101:;
				// continue
			}
						}		}if ((_ivvongrz - (int)2) < 0) goto Mark107; else if ((_ivvongrz - (int)2) == 0) goto Mark105; else goto Mark102;
Mark102:;
		
		_dmboubtu = (_ivvongrz + (int)2);
		{
			System.Int32 __81fgg2dlsvn287 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step287 = (System.Int32)((int)1);
			System.Int32 __81fgg2count287;
			for (__81fgg2count287 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn287 + __81fgg2step287) / __81fgg2step287)), _1m894xin = __81fgg2dlsvn287; __81fgg2count287 != 0; __81fgg2count287--, _1m894xin += (__81fgg2step287)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn288 = (System.Int32)((int)3);
					System.Int32 __81fgg2step288 = (System.Int32)((int)2);
					System.Int32 __81fgg2count288;
					for (__81fgg2count288 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ivvongrz) - __81fgg2dlsvn288 + __81fgg2step288) / __81fgg2step288)), _ld42zxsk = __81fgg2dlsvn288; __81fgg2count288 != 0; __81fgg2count288--, _ld42zxsk += (__81fgg2step288)) {

					{
						
						_knhh7y2m = (_dmboubtu - _ld42zxsk);// 
						
						*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = (*(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)2)) + *(_g0tjc1wj+((int)1 - 1) + (_knhh7y2m - (int)1 - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)2)));
						*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = (*(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)2)) - *(_g0tjc1wj+((int)1 - 1) + (_knhh7y2m - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)2)));// 
						
						*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = ((*(_t99b7n0f+(_ld42zxsk - (int)2 - 1)) * (*(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)2)) - *(_g0tjc1wj+((int)1 - 1) + (_knhh7y2m - (int)1 - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)2)))) - (*(_t99b7n0f+(_ld42zxsk - (int)1 - 1)) * (*(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)2)) + *(_g0tjc1wj+((int)1 - 1) + (_knhh7y2m - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)2)))));
						*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = ((*(_t99b7n0f+(_ld42zxsk - (int)2 - 1)) * (*(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)2)) + *(_g0tjc1wj+((int)1 - 1) + (_knhh7y2m - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)2)))) + (*(_t99b7n0f+(_ld42zxsk - (int)1 - 1)) * (*(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)2)) - *(_g0tjc1wj+((int)1 - 1) + (_knhh7y2m - (int)1 - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)2)))));// 
						
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
			System.Int32 __81fgg2dlsvn289 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step289 = (System.Int32)((int)1);
			System.Int32 __81fgg2count289;
			for (__81fgg2count289 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn289 + __81fgg2step289) / __81fgg2step289)), _1m894xin = __81fgg2dlsvn289; __81fgg2count289 != 0; __81fgg2count289--, _1m894xin += (__81fgg2step289)) {

			{
				
				*(_tyzpsh18+((int)1 - 1) + (_ivvongrz - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = (*(_g0tjc1wj+((int)1 - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)2)) + *(_g0tjc1wj+((int)1 - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)2)));
				*(_tyzpsh18+((int)1 - 1) + (_ivvongrz - 1) * 1 * (_a8qh3w7g) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * (_1mqgnnli)) = (-((*(_g0tjc1wj+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)2)) + *(_g0tjc1wj+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) * ((int)2)))));
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
