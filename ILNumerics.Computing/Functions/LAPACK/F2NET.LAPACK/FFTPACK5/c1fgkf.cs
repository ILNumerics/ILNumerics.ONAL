
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

	 
	public static void _ivfsuz8c(ref Int32 _ivvongrz, ref Int32 _zmzafydt, ref Int32 _1mqgnnli, ref Int32 _p39hja58, ref Int32 _0isd60h3, Single* _g0tjc1wj, Single* _ong9x5ud, ref Int32 _411n0nn5, Single* _tyzpsh18, Single* _ynzbkk10, ref Int32 _a8qh3w7g, Single* _elimqrjs)
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
Single _x1gqiqyx =  default;
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
			System.Int32 __81fgg2dlsvn24 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step24 = (System.Int32)((int)1);
			System.Int32 __81fgg2count24;
			for (__81fgg2count24 = System.Math.Max(0, (System.Int32)(((System.Int32)(_p39hja58) - __81fgg2dlsvn24 + __81fgg2step24) / __81fgg2step24)), _tm2fl5em = __81fgg2dlsvn24; __81fgg2count24 != 0; __81fgg2count24--, _tm2fl5em += (__81fgg2step24)) {

			{
				
				*(_ynzbkk10+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) = *(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_p39hja58));
				*(_ynzbkk10+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) = *(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_p39hja58));
Mark110:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn25 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step25 = (System.Int32)((int)1);
			System.Int32 __81fgg2count25;
			for (__81fgg2count25 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4nqkz2st) - __81fgg2dlsvn25 + __81fgg2step25) / __81fgg2step25)), _qb0uu4i2 = __81fgg2dlsvn25; __81fgg2count25 != 0; __81fgg2count25--, _qb0uu4i2 += (__81fgg2step25)) {

			{
				
				_bb3g971f = (_gbsim2rn - _qb0uu4i2);
				{
					System.Int32 __81fgg2dlsvn26 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step26 = (System.Int32)((int)1);
					System.Int32 __81fgg2count26;
					for (__81fgg2count26 = System.Math.Max(0, (System.Int32)(((System.Int32)(_p39hja58) - __81fgg2dlsvn26 + __81fgg2step26) / __81fgg2step26)), _tm2fl5em = __81fgg2dlsvn26; __81fgg2count26 != 0; __81fgg2count26--, _tm2fl5em += (__81fgg2step26)) {

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
			System.Int32 __81fgg2dlsvn27 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step27 = (System.Int32)((int)1);
			System.Int32 __81fgg2count27;
			for (__81fgg2count27 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4nqkz2st) - __81fgg2dlsvn27 + __81fgg2step27) / __81fgg2step27)), _qb0uu4i2 = __81fgg2dlsvn27; __81fgg2count27 != 0; __81fgg2count27--, _qb0uu4i2 += (__81fgg2step27)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn28 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step28 = (System.Int32)((int)1);
					System.Int32 __81fgg2count28;
					for (__81fgg2count28 = System.Math.Max(0, (System.Int32)(((System.Int32)(_p39hja58) - __81fgg2dlsvn28 + __81fgg2step28) / __81fgg2step28)), _tm2fl5em = __81fgg2dlsvn28; __81fgg2count28 != 0; __81fgg2count28--, _tm2fl5em += (__81fgg2step28)) {

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
			System.Int32 __81fgg2dlsvn29 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step29 = (System.Int32)((int)1);
			System.Int32 __81fgg2count29;
			for (__81fgg2count29 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4nqkz2st) - __81fgg2dlsvn29 + __81fgg2step29) / __81fgg2step29)), _cjahrwwv = __81fgg2dlsvn29; __81fgg2count29 != 0; __81fgg2count29--, _cjahrwwv += (__81fgg2step29)) {

			{
				
				_63ekojo6 = (_gbsim2rn - _cjahrwwv);
				{
					System.Int32 __81fgg2dlsvn30 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step30 = (System.Int32)((int)1);
					System.Int32 __81fgg2count30;
					for (__81fgg2count30 = System.Math.Max(0, (System.Int32)(((System.Int32)(_p39hja58) - __81fgg2dlsvn30 + __81fgg2step30) / __81fgg2step30)), _tm2fl5em = __81fgg2dlsvn30; __81fgg2count30 != 0; __81fgg2count30--, _tm2fl5em += (__81fgg2step30)) {

					{
						
						*(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_cjahrwwv - 1) * 1 * (_411n0nn5) * (_p39hja58)) = (*(_ynzbkk10+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) + (*(_elimqrjs+((int)1 - 1) + (_cjahrwwv - (int)1 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * (_zmzafydt - (int)1)) * *(_ynzbkk10+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_p39hja58))));
						*(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_63ekojo6 - 1) * 1 * (_411n0nn5) * (_p39hja58)) = (-((*(_elimqrjs+((int)1 - 1) + (_cjahrwwv - (int)1 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * (_zmzafydt - (int)1)) * *(_ynzbkk10+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + (_zmzafydt - 1) * 1 * (_a8qh3w7g) * (_p39hja58)))));
						*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_cjahrwwv - 1) * 1 * (_411n0nn5) * (_p39hja58)) = (*(_ynzbkk10+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) + (*(_elimqrjs+((int)1 - 1) + (_cjahrwwv - (int)1 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * (_zmzafydt - (int)1)) * *(_ynzbkk10+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_p39hja58))));
						*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_63ekojo6 - 1) * 1 * (_411n0nn5) * (_p39hja58)) = (-((*(_elimqrjs+((int)1 - 1) + (_cjahrwwv - (int)1 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * (_zmzafydt - (int)1)) * *(_ynzbkk10+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + (_zmzafydt - 1) * 1 * (_a8qh3w7g) * (_p39hja58)))));
Mark113:;
						// continue
					}
										}				}
				{
					System.Int32 __81fgg2dlsvn31 = (System.Int32)((int)3);
					const System.Int32 __81fgg2step31 = (System.Int32)((int)1);
					System.Int32 __81fgg2count31;
					for (__81fgg2count31 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4nqkz2st) - __81fgg2dlsvn31 + __81fgg2step31) / __81fgg2step31)), _qb0uu4i2 = __81fgg2dlsvn31; __81fgg2count31 != 0; __81fgg2count31--, _qb0uu4i2 += (__81fgg2step31)) {

					{
						
						_bb3g971f = (_gbsim2rn - _qb0uu4i2);
						_5fr8svok = ILNumerics.F2NET.Intrinsics.MOD((_cjahrwwv - (int)1) * (_qb0uu4i2 - (int)1) ,_zmzafydt );
						_7ghk8kn7 = *(_elimqrjs+((int)1 - 1) + (_5fr8svok - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * (_zmzafydt - (int)1));
						_vlip1kc6 = (-(*(_elimqrjs+((int)1 - 1) + (_5fr8svok - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * (_zmzafydt - (int)1))));
						{
							System.Int32 __81fgg2dlsvn32 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step32 = (System.Int32)((int)1);
							System.Int32 __81fgg2count32;
							for (__81fgg2count32 = System.Math.Max(0, (System.Int32)(((System.Int32)(_p39hja58) - __81fgg2dlsvn32 + __81fgg2step32) / __81fgg2step32)), _tm2fl5em = __81fgg2dlsvn32; __81fgg2count32 != 0; __81fgg2count32--, _tm2fl5em += (__81fgg2step32)) {

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
		if (_ivvongrz > (int)1)goto Mark136;
		_x1gqiqyx = ((float)1 / ILNumerics.F2NET.Intrinsics.REAL(_zmzafydt * _1mqgnnli ));
		if (_0isd60h3 == (int)1)goto Mark146;
		{
			System.Int32 __81fgg2dlsvn33 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step33 = (System.Int32)((int)1);
			System.Int32 __81fgg2count33;
			for (__81fgg2count33 = System.Math.Max(0, (System.Int32)(((System.Int32)(_p39hja58) - __81fgg2dlsvn33 + __81fgg2step33) / __81fgg2step33)), _tm2fl5em = __81fgg2dlsvn33; __81fgg2count33 != 0; __81fgg2count33--, _tm2fl5em += (__81fgg2step33)) {

			{
				
				*(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_p39hja58)) = (_x1gqiqyx * *(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_p39hja58)));
				*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_p39hja58)) = (_x1gqiqyx * *(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_p39hja58)));
Mark149:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn34 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step34 = (System.Int32)((int)1);
			System.Int32 __81fgg2count34;
			for (__81fgg2count34 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4nqkz2st) - __81fgg2dlsvn34 + __81fgg2step34) / __81fgg2step34)), _qb0uu4i2 = __81fgg2dlsvn34; __81fgg2count34 != 0; __81fgg2count34--, _qb0uu4i2 += (__81fgg2step34)) {

			{
				
				_bb3g971f = (_gbsim2rn - _qb0uu4i2);
				{
					System.Int32 __81fgg2dlsvn35 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step35 = (System.Int32)((int)1);
					System.Int32 __81fgg2count35;
					for (__81fgg2count35 = System.Math.Max(0, (System.Int32)(((System.Int32)(_p39hja58) - __81fgg2dlsvn35 + __81fgg2step35) / __81fgg2step35)), _tm2fl5em = __81fgg2dlsvn35; __81fgg2count35 != 0; __81fgg2count35--, _tm2fl5em += (__81fgg2step35)) {

					{
						
						_5wtia2l9 = (_x1gqiqyx * (*(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) - *(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58))));
						_9ujekppi = (_x1gqiqyx * (*(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) + *(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58))));
						*(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) = _5wtia2l9;
						*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58)) = (_x1gqiqyx * (*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) - *(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58))));
						*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) = (_x1gqiqyx * (*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) + *(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58))));
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
Mark146:;
		
		{
			System.Int32 __81fgg2dlsvn36 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step36 = (System.Int32)((int)1);
			System.Int32 __81fgg2count36;
			for (__81fgg2count36 = System.Math.Max(0, (System.Int32)(((System.Int32)(_p39hja58) - __81fgg2dlsvn36 + __81fgg2step36) / __81fgg2step36)), _tm2fl5em = __81fgg2dlsvn36; __81fgg2count36 != 0; __81fgg2count36--, _tm2fl5em += (__81fgg2step36)) {

			{
				
				*(_ynzbkk10+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) = (_x1gqiqyx * *(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_p39hja58)));
				*(_ynzbkk10+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) = (_x1gqiqyx * *(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_p39hja58)));
Mark147:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn37 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step37 = (System.Int32)((int)1);
			System.Int32 __81fgg2count37;
			for (__81fgg2count37 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4nqkz2st) - __81fgg2dlsvn37 + __81fgg2step37) / __81fgg2step37)), _qb0uu4i2 = __81fgg2dlsvn37; __81fgg2count37 != 0; __81fgg2count37--, _qb0uu4i2 += (__81fgg2step37)) {

			{
				
				_bb3g971f = (_gbsim2rn - _qb0uu4i2);
				{
					System.Int32 __81fgg2dlsvn38 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step38 = (System.Int32)((int)1);
					System.Int32 __81fgg2count38;
					for (__81fgg2count38 = System.Math.Max(0, (System.Int32)(((System.Int32)(_p39hja58) - __81fgg2dlsvn38 + __81fgg2step38) / __81fgg2step38)), _tm2fl5em = __81fgg2dlsvn38; __81fgg2count38 != 0; __81fgg2count38--, _tm2fl5em += (__81fgg2step38)) {

					{
						
						*(_ynzbkk10+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) = (_x1gqiqyx * (*(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) - *(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58))));
						*(_ynzbkk10+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) = (_x1gqiqyx * (*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) + *(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58))));
						*(_ynzbkk10+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) = (_x1gqiqyx * (*(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) + *(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58))));
						*(_ynzbkk10+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) = (_x1gqiqyx * (*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) - *(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58))));
Mark144:;
						// continue
					}
										}				}
Mark145:;
				// continue
			}
						}		}
		return;
Mark136:;
		
		{
			System.Int32 __81fgg2dlsvn39 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step39 = (System.Int32)((int)1);
			System.Int32 __81fgg2count39;
			for (__81fgg2count39 = System.Math.Max(0, (System.Int32)(((System.Int32)(_p39hja58) - __81fgg2dlsvn39 + __81fgg2step39) / __81fgg2step39)), _tm2fl5em = __81fgg2dlsvn39; __81fgg2count39 != 0; __81fgg2count39--, _tm2fl5em += (__81fgg2step39)) {

			{
				
				*(_ynzbkk10+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) = *(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_p39hja58));
				*(_ynzbkk10+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) = *(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + ((int)1 - 1) * 1 * (_411n0nn5) * (_p39hja58));
Mark137:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn40 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step40 = (System.Int32)((int)1);
			System.Int32 __81fgg2count40;
			for (__81fgg2count40 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4nqkz2st) - __81fgg2dlsvn40 + __81fgg2step40) / __81fgg2step40)), _qb0uu4i2 = __81fgg2dlsvn40; __81fgg2count40 != 0; __81fgg2count40--, _qb0uu4i2 += (__81fgg2step40)) {

			{
				
				_bb3g971f = (_gbsim2rn - _qb0uu4i2);
				{
					System.Int32 __81fgg2dlsvn41 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step41 = (System.Int32)((int)1);
					System.Int32 __81fgg2count41;
					for (__81fgg2count41 = System.Math.Max(0, (System.Int32)(((System.Int32)(_p39hja58) - __81fgg2dlsvn41 + __81fgg2step41) / __81fgg2step41)), _tm2fl5em = __81fgg2dlsvn41; __81fgg2count41 != 0; __81fgg2count41--, _tm2fl5em += (__81fgg2step41)) {

					{
						
						*(_ynzbkk10+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) = (*(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) - *(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58)));
						*(_ynzbkk10+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) = (*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) + *(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58)));
						*(_ynzbkk10+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) = (*(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) + *(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58)));
						*(_ynzbkk10+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_a8qh3w7g) + (_bb3g971f - 1) * 1 * (_a8qh3w7g) * (_p39hja58)) = (*(_ong9x5ud+((int)2 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_p39hja58)) - *(_ong9x5ud+((int)1 - 1) + (_tm2fl5em - 1) * 1 * (_411n0nn5) + (_bb3g971f - 1) * 1 * (_411n0nn5) * (_p39hja58)));
Mark134:;
						// continue
					}
										}				}
Mark135:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn42 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step42 = (System.Int32)((int)1);
			System.Int32 __81fgg2count42;
			for (__81fgg2count42 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ivvongrz) - __81fgg2dlsvn42 + __81fgg2step42) / __81fgg2step42)), _ld42zxsk = __81fgg2dlsvn42; __81fgg2count42 != 0; __81fgg2count42--, _ld42zxsk += (__81fgg2step42)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn43 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step43 = (System.Int32)((int)1);
					System.Int32 __81fgg2count43;
					for (__81fgg2count43 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn43 + __81fgg2step43) / __81fgg2step43)), _1m894xin = __81fgg2dlsvn43; __81fgg2count43 != 0; __81fgg2count43--, _1m894xin += (__81fgg2step43)) {

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
			System.Int32 __81fgg2dlsvn44 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step44 = (System.Int32)((int)1);
			System.Int32 __81fgg2count44;
			for (__81fgg2count44 = System.Math.Max(0, (System.Int32)(((System.Int32)(_zmzafydt) - __81fgg2dlsvn44 + __81fgg2step44) / __81fgg2step44)), _qb0uu4i2 = __81fgg2dlsvn44; __81fgg2count44 != 0; __81fgg2count44--, _qb0uu4i2 += (__81fgg2step44)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn45 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step45 = (System.Int32)((int)1);
					System.Int32 __81fgg2count45;
					for (__81fgg2count45 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn45 + __81fgg2step45) / __81fgg2step45)), _1m894xin = __81fgg2dlsvn45; __81fgg2count45 != 0; __81fgg2count45--, _1m894xin += (__81fgg2step45)) {

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
			System.Int32 __81fgg2dlsvn46 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step46 = (System.Int32)((int)1);
			System.Int32 __81fgg2count46;
			for (__81fgg2count46 = System.Math.Max(0, (System.Int32)(((System.Int32)(_zmzafydt) - __81fgg2dlsvn46 + __81fgg2step46) / __81fgg2step46)), _qb0uu4i2 = __81fgg2dlsvn46; __81fgg2count46 != 0; __81fgg2count46--, _qb0uu4i2 += (__81fgg2step46)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn47 = (System.Int32)((int)2);
					const System.Int32 __81fgg2step47 = (System.Int32)((int)1);
					System.Int32 __81fgg2count47;
					for (__81fgg2count47 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ivvongrz) - __81fgg2dlsvn47 + __81fgg2step47) / __81fgg2step47)), _ld42zxsk = __81fgg2dlsvn47; __81fgg2count47 != 0; __81fgg2count47--, _ld42zxsk += (__81fgg2step47)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn48 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step48 = (System.Int32)((int)1);
							System.Int32 __81fgg2count48;
							for (__81fgg2count48 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn48 + __81fgg2step48) / __81fgg2step48)), _1m894xin = __81fgg2dlsvn48; __81fgg2count48 != 0; __81fgg2count48--, _1m894xin += (__81fgg2step48)) {

							{
								
								*(_g0tjc1wj+((int)1 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_zmzafydt)) = ((*(_elimqrjs+(_ld42zxsk - 1) + (_qb0uu4i2 - (int)1 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * (_zmzafydt - (int)1)) * *(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * (_ivvongrz))) + (*(_elimqrjs+(_ld42zxsk - 1) + (_qb0uu4i2 - (int)1 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * (_zmzafydt - (int)1)) * *(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * (_ivvongrz))));
								*(_g0tjc1wj+((int)2 - 1) + (_1m894xin - 1) * 1 * (_411n0nn5) + (_qb0uu4i2 - 1) * 1 * (_411n0nn5) * (_1mqgnnli) + (_ld42zxsk - 1) * 1 * (_411n0nn5) * (_1mqgnnli) * (_zmzafydt)) = ((*(_elimqrjs+(_ld42zxsk - 1) + (_qb0uu4i2 - (int)1 - 1) * 1 * (_ivvongrz) + ((int)1 - 1) * 1 * (_ivvongrz) * (_zmzafydt - (int)1)) * *(_tyzpsh18+((int)2 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * (_ivvongrz))) - (*(_elimqrjs+(_ld42zxsk - 1) + (_qb0uu4i2 - (int)1 - 1) * 1 * (_ivvongrz) + ((int)2 - 1) * 1 * (_ivvongrz) * (_zmzafydt - (int)1)) * *(_tyzpsh18+((int)1 - 1) + (_1m894xin - 1) * 1 * (_a8qh3w7g) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) + (_qb0uu4i2 - 1) * 1 * (_a8qh3w7g) * (_1mqgnnli) * (_ivvongrz))));
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
