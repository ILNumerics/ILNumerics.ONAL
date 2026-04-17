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

	 
	public static void _pumg5tjh(ref Int32 _b6wzgw7j, ref Int32 _ivvongrz, ref Int32 _1mqgnnli, ref Int32 _0isd60h3, Double* _g0tjc1wj, ref Int32 _yj2u3o8x, ref Int32 _411n0nn5, Double* _tyzpsh18, ref Int32 _28cbiyim, ref Int32 _a8qh3w7g, Double* _elimqrjs)
	{
#region variable declarations
Int32 _dpill1hb =  default;
Int32 _q6xocrqe =  default;
Int32 _1m894xin =  default;
Int32 _010vumne =  default;
Double _4vz8g6wc =  default;
Double _ari8efzi =  default;
Double _acxfj1dn =  default;
Double _q5ohdjfe =  default;
Double _p3s8p84p =  default;
Double _zmitv3pp =  default;
Int32 _34fm7prn =  default;
Int32 _ld42zxsk =  default;
Double _5ngvqg1k =  default;
Double _sihlgehx =  default;
Double _ta4e2isb =  default;
Double _w2i26fwx =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		
		{var vals = new Double[] { (double)-0.5,(double)0.866025403784439 };var valsIter = 0;

		__cmf3kb._qhqyhuml = vals[valsIter++];
		__cmf3kb._6zfj7kh8 = vals[valsIter++];
		}//C 
		
		_dpill1hb = (((_b6wzgw7j - (int)1) * _yj2u3o8x) + (int)1);
		_q6xocrqe = ((int)1 - _28cbiyim);
		if ((_ivvongrz > (int)1) | (_0isd60h3 == (int)1))goto Mark102;
		{
			System.Int32 __81fgg2dlsvn171 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step171 = (System.Int32)((int)1);
			System.Int32 __81fgg2count171;
			for (__81fgg2count171 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn171 + __81fgg2step171) / __81fgg2step171)), _1m894xin = __81fgg2dlsvn171; __81fgg2count171 != 0; __81fgg2count171--, _1m894xin += (__81fgg2step171)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn172 = (System.Int32)((int)1);
					System.Int32 __81fgg2step172 = (System.Int32)(_yj2u3o8x);
					System.Int32 __81fgg2count172;
					for (__81fgg2count172 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dpill1hb) - __81fgg2dlsvn172 + __81fgg2step172) / __81fgg2step172)), _010vumne = __81fgg2dlsvn172; __81fgg2count172 != 0; __81fgg2count172--, _010vumne += (__81fgg2step172)) {

					{
						
						_4vz8g6wc = (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_ari8efzi = (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__cmf3kb._qhqyhuml * _4vz8g6wc));
						*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _4vz8g6wc);
						_acxfj1dn = (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_q5ohdjfe = (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__cmf3kb._qhqyhuml * _acxfj1dn));
						*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _acxfj1dn);
						_p3s8p84p = (__cmf3kb._6zfj7kh8 * (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
						_zmitv3pp = (__cmf3kb._6zfj7kh8 * (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
						*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_ari8efzi - _zmitv3pp);
						*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_ari8efzi + _zmitv3pp);
						*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_q5ohdjfe + _p3s8p84p);
						*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_q5ohdjfe - _p3s8p84p);
Mark101:;
						// continue
					}
										}				}
			}
						}		}
		return;
Mark102:;
		
		{
			System.Int32 __81fgg2dlsvn173 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step173 = (System.Int32)((int)1);
			System.Int32 __81fgg2count173;
			for (__81fgg2count173 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn173 + __81fgg2step173) / __81fgg2step173)), _1m894xin = __81fgg2dlsvn173; __81fgg2count173 != 0; __81fgg2count173--, _1m894xin += (__81fgg2step173)) {

			{
				
				_34fm7prn = _q6xocrqe;
				{
					System.Int32 __81fgg2dlsvn174 = (System.Int32)((int)1);
					System.Int32 __81fgg2step174 = (System.Int32)(_yj2u3o8x);
					System.Int32 __81fgg2count174;
					for (__81fgg2count174 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dpill1hb) - __81fgg2dlsvn174 + __81fgg2step174) / __81fgg2step174)), _010vumne = __81fgg2dlsvn174; __81fgg2count174 != 0; __81fgg2count174--, _010vumne += (__81fgg2step174)) {

					{
						
						_34fm7prn = (_34fm7prn + _28cbiyim);
						_4vz8g6wc = (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_ari8efzi = (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__cmf3kb._qhqyhuml * _4vz8g6wc));
						*(_tyzpsh18+((int)1 - 1) + (_34fm7prn - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_a8qh3w7g) + ((int)1 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _4vz8g6wc);
						_acxfj1dn = (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_q5ohdjfe = (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__cmf3kb._qhqyhuml * _acxfj1dn));
						*(_tyzpsh18+((int)2 - 1) + (_34fm7prn - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_a8qh3w7g) + ((int)1 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _acxfj1dn);
						_p3s8p84p = (__cmf3kb._6zfj7kh8 * (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
						_zmitv3pp = (__cmf3kb._6zfj7kh8 * (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
						*(_tyzpsh18+((int)1 - 1) + (_34fm7prn - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_a8qh3w7g) + ((int)2 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (_ari8efzi - _zmitv3pp);
						*(_tyzpsh18+((int)1 - 1) + (_34fm7prn - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_a8qh3w7g) + ((int)3 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (_ari8efzi + _zmitv3pp);
						*(_tyzpsh18+((int)2 - 1) + (_34fm7prn - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_a8qh3w7g) + ((int)2 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (_q5ohdjfe + _p3s8p84p);
						*(_tyzpsh18+((int)2 - 1) + (_34fm7prn - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_a8qh3w7g) + ((int)3 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (_q5ohdjfe - _p3s8p84p);
Mark103:;
						// continue
					}
										}				}
			}
						}		}
		if (_ivvongrz == (int)1)
		return;
		{
			System.Int32 __81fgg2dlsvn175 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step175 = (System.Int32)((int)1);
			System.Int32 __81fgg2count175;
			for (__81fgg2count175 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ivvongrz) - __81fgg2dlsvn175 + __81fgg2step175) / __81fgg2step175)), _ld42zxsk = __81fgg2dlsvn175; __81fgg2count175 != 0; __81fgg2count175--, _ld42zxsk += (__81fgg2step175)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn176 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step176 = (System.Int32)((int)1);
					System.Int32 __81fgg2count176;
					for (__81fgg2count176 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn176 + __81fgg2step176) / __81fgg2step176)), _1m894xin = __81fgg2dlsvn176; __81fgg2count176 != 0; __81fgg2count176--, _1m894xin += (__81fgg2step176)) {

					{
						
						_34fm7prn = _q6xocrqe;
						{
							System.Int32 __81fgg2dlsvn177 = (System.Int32)((int)1);
							System.Int32 __81fgg2step177 = (System.Int32)(_yj2u3o8x);
							System.Int32 __81fgg2count177;
							for (__81fgg2count177 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dpill1hb) - __81fgg2dlsvn177 + __81fgg2step177) / __81fgg2step177)), _010vumne = __81fgg2dlsvn177; __81fgg2count177 != 0; __81fgg2count177--, _010vumne += (__81fgg2step177)) {

							{
								
								_34fm7prn = (_34fm7prn + _28cbiyim);
								_4vz8g6wc = (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + (_ld42zxsk - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + (_ld42zxsk - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
								_ari8efzi = (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + (_ld42zxsk - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__cmf3kb._qhqyhuml * _4vz8g6wc));
								*(_tyzpsh18+((int)1 - 1) + (_34fm7prn - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_a8qh3w7g) + ((int)1 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + (_ld42zxsk - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _4vz8g6wc);
								_acxfj1dn = (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + (_ld42zxsk - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + (_ld42zxsk - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
								_q5ohdjfe = (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + (_ld42zxsk - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__cmf3kb._qhqyhuml * _acxfj1dn));
								*(_tyzpsh18+((int)2 - 1) + (_34fm7prn - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_a8qh3w7g) + ((int)1 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + (_ld42zxsk - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _acxfj1dn);
								_p3s8p84p = (__cmf3kb._6zfj7kh8 * (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + (_ld42zxsk - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + (_ld42zxsk - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
								_zmitv3pp = (__cmf3kb._6zfj7kh8 * (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + (_ld42zxsk - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + (_ld42zxsk - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
								_5ngvqg1k = (_ari8efzi - _zmitv3pp);
								_sihlgehx = (_ari8efzi + _zmitv3pp);
								_ta4e2isb = (_q5ohdjfe + _p3s8p84p);
								_w2i26fwx = (_q5ohdjfe - _p3s8p84p);
								*(_tyzpsh18+((int)2 - 1) + (_34fm7prn - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_a8qh3w7g) + ((int)2 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)1 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)2)) * _ta4e2isb) + (*(_elimqrjs+(_ld42zxsk - 1) + ((int)1 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)2)) * _5ngvqg1k));
								*(_tyzpsh18+((int)1 - 1) + (_34fm7prn - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_a8qh3w7g) + ((int)2 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)1 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)2)) * _5ngvqg1k) - (*(_elimqrjs+(_ld42zxsk - 1) + ((int)1 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)2)) * _ta4e2isb));
								*(_tyzpsh18+((int)2 - 1) + (_34fm7prn - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_a8qh3w7g) + ((int)3 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)2 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)2)) * _w2i26fwx) + (*(_elimqrjs+(_ld42zxsk - 1) + ((int)2 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)2)) * _sihlgehx));
								*(_tyzpsh18+((int)1 - 1) + (_34fm7prn - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_a8qh3w7g) + ((int)3 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)2 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)2)) * _sihlgehx) - (*(_elimqrjs+(_ld42zxsk - 1) + ((int)2 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)2)) * _w2i26fwx));
Mark104:;
								// continue
							}
														}						}
					}
										}				}
Mark105:;
				// continue
			}
						}		}
		return;
	}
	
	} // 177


internal unsafe class __cmf3kb { 
internal static Double _qhqyhuml =  default;
internal static Double _6zfj7kh8 =  default;

}

} // end class 
} // end namespace
#endif
