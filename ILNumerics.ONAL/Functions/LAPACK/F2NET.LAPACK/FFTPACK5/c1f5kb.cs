
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

	 
	public static void _c23y4a7n(ref Int32 _ivvongrz, ref Int32 _1mqgnnli, ref Int32 _0isd60h3, Single* _g0tjc1wj, ref Int32 _411n0nn5, Single* _tyzpsh18, ref Int32 _a8qh3w7g, Single* _elimqrjs)
	{
#region variable declarations
Int32 _1m894xin =  default;
Single _6me05a1z =  default;
Single _acxfj1dn =  default;
Single _mvcv2lkd =  default;
Single _4e8x89om =  default;
Single _5aj8f19p =  default;
Single _4vz8g6wc =  default;
Single _evau9kxw =  default;
Single _sxykfbxf =  default;
Single _5wtia2l9 =  default;
Single _9ujekppi =  default;
Single _ari8efzi =  default;
Single _q5ohdjfe =  default;
Single _p3s8p84p =  default;
Single _zmitv3pp =  default;
Single _y6eijd1z =  default;
Single _pyqj3d1y =  default;
Single _lz58wakc =  default;
Single _2dugxc3w =  default;
Int32 _ld42zxsk =  default;
Single _sihlgehx =  default;
Single _2xgkqwqk =  default;
Single _w2i26fwx =  default;
Single _ic414rkc =  default;
Single _c3p6ey92 =  default;
Single _5ngvqg1k =  default;
Single _uplgts52 =  default;
Single _ta4e2isb =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		
		{var vals = new Single[] { (float)0.3090169943749474,(float)0.9510565162951536,(float)-0.8090169943749475,(float)0.5877852522924731 };var valsIter = 0;

		__c1f5kb._35fkldhu = vals[valsIter++];
		__c1f5kb._7z6cwvml = vals[valsIter++];
		__c1f5kb._ka2z1dpq = vals[valsIter++];
		__c1f5kb._3bje2i7i = vals[valsIter++];
		}//C 
		//C FFTPACK 5.1 auxiliary routine 
		//C 
		
		if ((_ivvongrz > (int)1) | (_0isd60h3 == (int)1))goto Mark102;
		{
			System.Int32 __81fgg2dlsvn62 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step62 = (System.Int32)((int)1);
			System.Int32 __81fgg2count62;
			for (__81fgg2count62 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn62 + __81fgg2step62) / __81fgg2step62)), _1m894xin = __81fgg2dlsvn62; __81fgg2count62 != 0; __81fgg2count62--, _1m894xin += (__81fgg2step62)) {

			{
				
				_6me05a1z = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)5 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_acxfj1dn = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)5 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_mvcv2lkd = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_4e8x89om = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_5aj8f19p = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)5 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_4vz8g6wc = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)5 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_evau9kxw = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_sxykfbxf = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_5wtia2l9 = ((*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _4vz8g6wc) + _sxykfbxf);
				_9ujekppi = ((*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _acxfj1dn) + _4e8x89om);
				_ari8efzi = ((*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__c1f5kb._35fkldhu * _4vz8g6wc)) + (__c1f5kb._ka2z1dpq * _sxykfbxf));
				_q5ohdjfe = ((*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__c1f5kb._35fkldhu * _acxfj1dn)) + (__c1f5kb._ka2z1dpq * _4e8x89om));
				_p3s8p84p = ((*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__c1f5kb._ka2z1dpq * _4vz8g6wc)) + (__c1f5kb._35fkldhu * _sxykfbxf));
				_zmitv3pp = ((*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__c1f5kb._ka2z1dpq * _acxfj1dn)) + (__c1f5kb._35fkldhu * _4e8x89om));
				*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = _5wtia2l9;
				*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = _9ujekppi;
				_y6eijd1z = ((__c1f5kb._7z6cwvml * _5aj8f19p) + (__c1f5kb._3bje2i7i * _evau9kxw));
				_pyqj3d1y = ((__c1f5kb._7z6cwvml * _6me05a1z) + (__c1f5kb._3bje2i7i * _mvcv2lkd));
				_lz58wakc = ((__c1f5kb._3bje2i7i * _5aj8f19p) - (__c1f5kb._7z6cwvml * _evau9kxw));
				_2dugxc3w = ((__c1f5kb._3bje2i7i * _6me05a1z) - (__c1f5kb._7z6cwvml * _mvcv2lkd));
				*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_ari8efzi - _pyqj3d1y);
				*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)5 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_ari8efzi + _pyqj3d1y);
				*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_q5ohdjfe + _y6eijd1z);
				*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_zmitv3pp + _lz58wakc);
				*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_p3s8p84p - _2dugxc3w);
				*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_p3s8p84p + _2dugxc3w);
				*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_zmitv3pp - _lz58wakc);
				*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)5 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) = (_q5ohdjfe - _y6eijd1z);
Mark101:;
				// continue
			}
						}		}
		return;
Mark102:;
		
		{
			System.Int32 __81fgg2dlsvn63 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step63 = (System.Int32)((int)1);
			System.Int32 __81fgg2count63;
			for (__81fgg2count63 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn63 + __81fgg2step63) / __81fgg2step63)), _1m894xin = __81fgg2dlsvn63; __81fgg2count63 != 0; __81fgg2count63--, _1m894xin += (__81fgg2step63)) {

			{
				
				_6me05a1z = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)5 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_acxfj1dn = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)5 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_mvcv2lkd = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_4e8x89om = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_5aj8f19p = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)5 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_4vz8g6wc = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)5 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_evau9kxw = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				_sxykfbxf = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
				*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)5)) = ((*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _4vz8g6wc) + _sxykfbxf);
				*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)5)) = ((*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _acxfj1dn) + _4e8x89om);
				_ari8efzi = ((*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__c1f5kb._35fkldhu * _4vz8g6wc)) + (__c1f5kb._ka2z1dpq * _sxykfbxf));
				_q5ohdjfe = ((*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__c1f5kb._35fkldhu * _acxfj1dn)) + (__c1f5kb._ka2z1dpq * _4e8x89om));
				_p3s8p84p = ((*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__c1f5kb._ka2z1dpq * _4vz8g6wc)) + (__c1f5kb._35fkldhu * _sxykfbxf));
				_zmitv3pp = ((*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__c1f5kb._ka2z1dpq * _acxfj1dn)) + (__c1f5kb._35fkldhu * _4e8x89om));
				_y6eijd1z = ((__c1f5kb._7z6cwvml * _5aj8f19p) + (__c1f5kb._3bje2i7i * _evau9kxw));
				_pyqj3d1y = ((__c1f5kb._7z6cwvml * _6me05a1z) + (__c1f5kb._3bje2i7i * _mvcv2lkd));
				_lz58wakc = ((__c1f5kb._3bje2i7i * _5aj8f19p) - (__c1f5kb._7z6cwvml * _evau9kxw));
				_2dugxc3w = ((__c1f5kb._3bje2i7i * _6me05a1z) - (__c1f5kb._7z6cwvml * _mvcv2lkd));
				*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)5)) = (_ari8efzi - _pyqj3d1y);
				*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)5 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)5)) = (_ari8efzi + _pyqj3d1y);
				*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)5)) = (_q5ohdjfe + _y6eijd1z);
				*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)5)) = (_zmitv3pp + _lz58wakc);
				*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)5)) = (_p3s8p84p - _2dugxc3w);
				*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)4 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)5)) = (_p3s8p84p + _2dugxc3w);
				*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)4 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)5)) = (_zmitv3pp - _lz58wakc);
				*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)5 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)5)) = (_q5ohdjfe - _y6eijd1z);
Mark103:;
				// continue
			}
						}		}
		if (_ivvongrz == (int)1)
		return;
		{
			System.Int32 __81fgg2dlsvn64 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step64 = (System.Int32)((int)1);
			System.Int32 __81fgg2count64;
			for (__81fgg2count64 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ivvongrz) - __81fgg2dlsvn64 + __81fgg2step64) / __81fgg2step64)), _ld42zxsk = __81fgg2dlsvn64; __81fgg2count64 != 0; __81fgg2count64--, _ld42zxsk += (__81fgg2step64)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn65 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step65 = (System.Int32)((int)1);
					System.Int32 __81fgg2count65;
					for (__81fgg2count65 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn65 + __81fgg2step65) / __81fgg2step65)), _1m894xin = __81fgg2dlsvn65; __81fgg2count65 != 0; __81fgg2count65--, _1m894xin += (__81fgg2step65)) {

					{
						
						_6me05a1z = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)5 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_acxfj1dn = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)5 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_mvcv2lkd = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_4e8x89om = (*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_5aj8f19p = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)5 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_4vz8g6wc = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)5 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_evau9kxw = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) - *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						_sxykfbxf = (*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)3 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + *(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)4 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)));
						*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)5)) = ((*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _4vz8g6wc) + _sxykfbxf);
						*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)5)) = ((*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + _acxfj1dn) + _4e8x89om);
						_ari8efzi = ((*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__c1f5kb._35fkldhu * _4vz8g6wc)) + (__c1f5kb._ka2z1dpq * _sxykfbxf));
						_q5ohdjfe = ((*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__c1f5kb._35fkldhu * _acxfj1dn)) + (__c1f5kb._ka2z1dpq * _4e8x89om));
						_p3s8p84p = ((*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__c1f5kb._ka2z1dpq * _4vz8g6wc)) + (__c1f5kb._35fkldhu * _sxykfbxf));
						_zmitv3pp = ((*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + ((int)1 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_ivvongrz)) + (__c1f5kb._ka2z1dpq * _acxfj1dn)) + (__c1f5kb._35fkldhu * _4e8x89om));
						_y6eijd1z = ((__c1f5kb._7z6cwvml * _5aj8f19p) + (__c1f5kb._3bje2i7i * _evau9kxw));
						_pyqj3d1y = ((__c1f5kb._7z6cwvml * _6me05a1z) + (__c1f5kb._3bje2i7i * _mvcv2lkd));
						_lz58wakc = ((__c1f5kb._3bje2i7i * _5aj8f19p) - (__c1f5kb._7z6cwvml * _evau9kxw));
						_2dugxc3w = ((__c1f5kb._3bje2i7i * _6me05a1z) - (__c1f5kb._7z6cwvml * _mvcv2lkd));
						_sihlgehx = (_p3s8p84p - _2dugxc3w);
						_2xgkqwqk = (_p3s8p84p + _2dugxc3w);
						_w2i26fwx = (_zmitv3pp + _lz58wakc);
						_ic414rkc = (_zmitv3pp - _lz58wakc);
						_c3p6ey92 = (_ari8efzi + _pyqj3d1y);
						_5ngvqg1k = (_ari8efzi - _pyqj3d1y);
						_uplgts52 = (_q5ohdjfe - _y6eijd1z);
						_ta4e2isb = (_q5ohdjfe + _y6eijd1z);
						*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)5)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)1 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)4)) * _5ngvqg1k) - (*(_elimqrjs+(_ld42zxsk - 1) + ((int)1 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)4)) * _ta4e2isb));
						*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)5)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)1 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)4)) * _ta4e2isb) + (*(_elimqrjs+(_ld42zxsk - 1) + ((int)1 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)4)) * _5ngvqg1k));
						*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)5)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)2 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)4)) * _sihlgehx) - (*(_elimqrjs+(_ld42zxsk - 1) + ((int)2 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)4)) * _w2i26fwx));
						*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)5)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)2 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)4)) * _w2i26fwx) + (*(_elimqrjs+(_ld42zxsk - 1) + ((int)2 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)4)) * _sihlgehx));
						*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)4 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)5)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)3 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)4)) * _2xgkqwqk) - (*(_elimqrjs+(_ld42zxsk - 1) + ((int)3 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)4)) * _ic414rkc));
						*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)4 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)5)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)3 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)4)) * _ic414rkc) + (*(_elimqrjs+(_ld42zxsk - 1) + ((int)3 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)4)) * _2xgkqwqk));
						*(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)5 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)5)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)4 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)4)) * _c3p6ey92) - (*(_elimqrjs+(_ld42zxsk - 1) + ((int)4 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)4)) * _uplgts52));
						*(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + ((int)5 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * ((int)5)) = ((*(_elimqrjs+(_ld42zxsk - 1) + ((int)4 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * ((int)4)) * _uplgts52) + (*(_elimqrjs+(_ld42zxsk - 1) + ((int)4 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * ((int)4)) * _c3p6ey92));
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


internal unsafe class __c1f5kb { 
internal static Single _35fkldhu =  default;
internal static Single _7z6cwvml =  default;
internal static Single _ka2z1dpq =  default;
internal static Single _3bje2i7i =  default;

}

} // end class 
} // end namespace
#endif
