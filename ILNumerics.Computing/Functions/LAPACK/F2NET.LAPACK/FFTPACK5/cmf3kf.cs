
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

	 
	public static void _thmokna6(ref Int32 _b6wzgw7j, ref Int32 _ivvongrz, ref Int32 _1mqgnnli, ref Int32 _0isd60h3, Single* _g0tjc1wj, ref Int32 _yj2u3o8x, ref Int32 _411n0nn5, Single* _tyzpsh18, ref Int32 _28cbiyim, ref Int32 _a8qh3w7g, Single* _elimqrjs)
	{
#region variable declarations
Int32 _dpill1hb =  default;
Int32 _q6xocrqe =  default;
Single _x1gqiqyx =  default;
Int32 _1m894xin =  default;
Int32 _010vumne =  default;
Single _4vz8g6wc =  default;
Single _ari8efzi =  default;
Single _acxfj1dn =  default;
Single _q5ohdjfe =  default;
Single _p3s8p84p =  default;
Single _zmitv3pp =  default;
Int32 _34fm7prn =  default;
Int32 _ld42zxsk =  default;
Single _5ngvqg1k =  default;
Single _sihlgehx =  default;
Single _ta4e2isb =  default;
Single _w2i26fwx =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		
		{var vals = new Single[] { (float)-0.5,(float)-0.866025403784439 };var valsIter = 0;

		__cmf3kf._qhqyhuml = vals[valsIter++];
		__cmf3kf._6zfj7kh8 = vals[valsIter++];
		}//C 
		
		_dpill1hb = (((_b6wzgw7j - (int)1) * _yj2u3o8x) + (int)1);
		_q6xocrqe = ((int)1 - _28cbiyim);
		if (_ivvongrz > (int)1)goto Mark102;
		_x1gqiqyx = ((float)1 / ILNumerics.F2NET.Intrinsics.REAL((int)3 * _1mqgnnli ));
		if (_0isd60h3 == (int)1)goto Mark106;
		{
			System.Int32 __81fgg2dlsvn97 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step97 = (System.Int32)((int)1);
			System.Int32 __81fgg2count97;
			for (__81fgg2count97 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn97 + __81fgg2step97) / __81fgg2step97)), _1m894xin = __81fgg2dlsvn97; __81fgg2count97 != 0; __81fgg2count97--, _1m894xin += (__81fgg2step97)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn98 = (System.Int32)((int)1);
					System.Int32 __81fgg2step98 = (System.Int32)(_yj2u3o8x);
					System.Int32 __81fgg2count98;
					for (__81fgg2count98 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dpill1hb) - __81fgg2dlsvn98 + __81fgg2step98) / __81fgg2step98)), _010vumne = __81fgg2dlsvn98; __81fgg2count98 != 0; __81fgg2count98--, _010vumne += (__81fgg2step98)) {

					{
						
						_4vz8g6wc = (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_ari8efzi = (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__cmf3kf._qhqyhuml * _4vz8g6wc));
						*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_x1gqiqyx * (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _4vz8g6wc));
						_acxfj1dn = (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_q5ohdjfe = (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__cmf3kf._qhqyhuml * _acxfj1dn));
						*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_x1gqiqyx * (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _acxfj1dn));
						_p3s8p84p = (__cmf3kf._6zfj7kh8 * (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
						_zmitv3pp = (__cmf3kf._6zfj7kh8 * (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
						*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_x1gqiqyx * (_ari8efzi - _zmitv3pp));
						*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_x1gqiqyx * (_ari8efzi + _zmitv3pp));
						*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_x1gqiqyx * (_q5ohdjfe + _p3s8p84p));
						*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_x1gqiqyx * (_q5ohdjfe - _p3s8p84p));
Mark101:;
						// continue
					}
										}				}
			}
						}		}
		return;
Mark106:;
		
		{
			System.Int32 __81fgg2dlsvn99 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step99 = (System.Int32)((int)1);
			System.Int32 __81fgg2count99;
			for (__81fgg2count99 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn99 + __81fgg2step99) / __81fgg2step99)), _1m894xin = __81fgg2dlsvn99; __81fgg2count99 != 0; __81fgg2count99--, _1m894xin += (__81fgg2step99)) {

			{
				
				_34fm7prn = _q6xocrqe;
				{
					System.Int32 __81fgg2dlsvn100 = (System.Int32)((int)1);
					System.Int32 __81fgg2step100 = (System.Int32)(_yj2u3o8x);
					System.Int32 __81fgg2count100;
					for (__81fgg2count100 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dpill1hb) - __81fgg2dlsvn100 + __81fgg2step100) / __81fgg2step100)), _010vumne = __81fgg2dlsvn100; __81fgg2count100 != 0; __81fgg2count100--, _010vumne += (__81fgg2step100)) {

					{
						
						_34fm7prn = (_34fm7prn + _28cbiyim);
						_4vz8g6wc = (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_ari8efzi = (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__cmf3kf._qhqyhuml * _4vz8g6wc));
						*(_tyzpsh18+((int)1 - 1) + (_34fm7prn - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_a8qh3w7g) + ((int)1 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (_x1gqiqyx * (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _4vz8g6wc));
						_acxfj1dn = (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_q5ohdjfe = (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__cmf3kf._qhqyhuml * _acxfj1dn));
						*(_tyzpsh18+((int)2 - 1) + (_34fm7prn - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_a8qh3w7g) + ((int)1 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (_x1gqiqyx * (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _acxfj1dn));
						_p3s8p84p = (__cmf3kf._6zfj7kh8 * (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
						_zmitv3pp = (__cmf3kf._6zfj7kh8 * (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
						*(_tyzpsh18+((int)1 - 1) + (_34fm7prn - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_a8qh3w7g) + ((int)2 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (_x1gqiqyx * (_ari8efzi - _zmitv3pp));
						*(_tyzpsh18+((int)1 - 1) + (_34fm7prn - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_a8qh3w7g) + ((int)3 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (_x1gqiqyx * (_ari8efzi + _zmitv3pp));
						*(_tyzpsh18+((int)2 - 1) + (_34fm7prn - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_a8qh3w7g) + ((int)2 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (_x1gqiqyx * (_q5ohdjfe + _p3s8p84p));
						*(_tyzpsh18+((int)2 - 1) + (_34fm7prn - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_a8qh3w7g) + ((int)3 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (_x1gqiqyx * (_q5ohdjfe - _p3s8p84p));
Mark107:;
						// continue
					}
										}				}
			}
						}		}
		return;
Mark102:;
		
		{
			System.Int32 __81fgg2dlsvn101 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step101 = (System.Int32)((int)1);
			System.Int32 __81fgg2count101;
			for (__81fgg2count101 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn101 + __81fgg2step101) / __81fgg2step101)), _1m894xin = __81fgg2dlsvn101; __81fgg2count101 != 0; __81fgg2count101--, _1m894xin += (__81fgg2step101)) {

			{
				
				_34fm7prn = _q6xocrqe;
				{
					System.Int32 __81fgg2dlsvn102 = (System.Int32)((int)1);
					System.Int32 __81fgg2step102 = (System.Int32)(_yj2u3o8x);
					System.Int32 __81fgg2count102;
					for (__81fgg2count102 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dpill1hb) - __81fgg2dlsvn102 + __81fgg2step102) / __81fgg2step102)), _010vumne = __81fgg2dlsvn102; __81fgg2count102 != 0; __81fgg2count102--, _010vumne += (__81fgg2step102)) {

					{
						
						_34fm7prn = (_34fm7prn + _28cbiyim);
						_4vz8g6wc = (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_ari8efzi = (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__cmf3kf._qhqyhuml * _4vz8g6wc));
						*(_tyzpsh18+((int)1 - 1) + (_34fm7prn - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_a8qh3w7g) + ((int)1 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _4vz8g6wc);
						_acxfj1dn = (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_q5ohdjfe = (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__cmf3kf._qhqyhuml * _acxfj1dn));
						*(_tyzpsh18+((int)2 - 1) + (_34fm7prn - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_a8qh3w7g) + ((int)1 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _acxfj1dn);
						_p3s8p84p = (__cmf3kf._6zfj7kh8 * (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
						_zmitv3pp = (__cmf3kf._6zfj7kh8 * (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
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
		{
			System.Int32 __81fgg2dlsvn103 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step103 = (System.Int32)((int)1);
			System.Int32 __81fgg2count103;
			for (__81fgg2count103 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ivvongrz) - __81fgg2dlsvn103 + __81fgg2step103) / __81fgg2step103)), _ld42zxsk = __81fgg2dlsvn103; __81fgg2count103 != 0; __81fgg2count103--, _ld42zxsk += (__81fgg2step103)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn104 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step104 = (System.Int32)((int)1);
					System.Int32 __81fgg2count104;
					for (__81fgg2count104 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn104 + __81fgg2step104) / __81fgg2step104)), _1m894xin = __81fgg2dlsvn104; __81fgg2count104 != 0; __81fgg2count104--, _1m894xin += (__81fgg2step104)) {

					{
						
						_34fm7prn = _q6xocrqe;
						{
							System.Int32 __81fgg2dlsvn105 = (System.Int32)((int)1);
							System.Int32 __81fgg2step105 = (System.Int32)(_yj2u3o8x);
							System.Int32 __81fgg2count105;
							for (__81fgg2count105 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dpill1hb) - __81fgg2dlsvn105 + __81fgg2step105) / __81fgg2step105)), _010vumne = __81fgg2dlsvn105; __81fgg2count105 != 0; __81fgg2count105--, _010vumne += (__81fgg2step105)) {

							{
								
								_34fm7prn = (_34fm7prn + _28cbiyim);
								_4vz8g6wc = (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + (_ld42zxsk - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + (_ld42zxsk - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
								_ari8efzi = (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + (_ld42zxsk - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__cmf3kf._qhqyhuml * _4vz8g6wc));
								*(_tyzpsh18+((int)1 - 1) + (_34fm7prn - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_a8qh3w7g) + ((int)1 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + (_ld42zxsk - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _4vz8g6wc);
								_acxfj1dn = (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + (_ld42zxsk - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + (_ld42zxsk - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
								_q5ohdjfe = (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + (_ld42zxsk - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__cmf3kf._qhqyhuml * _acxfj1dn));
								*(_tyzpsh18+((int)2 - 1) + (_34fm7prn - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_a8qh3w7g) + ((int)1 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + (_ld42zxsk - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _acxfj1dn);
								_p3s8p84p = (__cmf3kf._6zfj7kh8 * (*(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + (_ld42zxsk - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + (_ld42zxsk - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
								_zmitv3pp = (__cmf3kf._6zfj7kh8 * (*(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + (_ld42zxsk - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_010vumne - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_411n0nn5) + (_ld42zxsk - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * ((int)2) * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz))));
								_5ngvqg1k = (_ari8efzi - _zmitv3pp);
								_sihlgehx = (_ari8efzi + _zmitv3pp);
								_ta4e2isb = (_q5ohdjfe + _p3s8p84p);
								_w2i26fwx = (_q5ohdjfe - _p3s8p84p);
								*(_tyzpsh18+((int)2 - 1) + (_34fm7prn - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_a8qh3w7g) + ((int)2 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)1 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)2)) * _ta4e2isb) - (*(_elimqrjs+(_ld42zxsk - 1) + ((int)1 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)2)) * _5ngvqg1k));
								*(_tyzpsh18+((int)1 - 1) + (_34fm7prn - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_a8qh3w7g) + ((int)2 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)1 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)2)) * _5ngvqg1k) + (*(_elimqrjs+(_ld42zxsk - 1) + ((int)1 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)2)) * _ta4e2isb));
								*(_tyzpsh18+((int)2 - 1) + (_34fm7prn - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_a8qh3w7g) + ((int)3 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)2 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)2)) * _w2i26fwx) - (*(_elimqrjs+(_ld42zxsk - 1) + ((int)2 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)2)) * _sihlgehx));
								*(_tyzpsh18+((int)1 - 1) + (_34fm7prn - 1) * 1 * ((int)2) + (_1m894xin - 1) * 1 * ((int)2) * (_a8qh3w7g) + ((int)3 - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * ((int)2) * (_a8qh3w7g) * (_1mqgnnli) * ((int)3)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)2 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)2)) * _sihlgehx) + (*(_elimqrjs+(_ld42zxsk - 1) + ((int)2 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)2)) * _w2i26fwx));
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


internal unsafe class __cmf3kf { 
internal static Single _qhqyhuml =  default;
internal static Single _6zfj7kh8 =  default;

}

} // end class 
} // end namespace
#endif
