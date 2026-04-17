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

	 
	public static void _anx81z64(ref Int32 _cf4kqqk2, ref Int32 _b2ekxz2c, ref Int32 _99xbntpe, ref Int32 _pxpm3zlt, Double* _4zklolxv, Double* _tyzpsh18, Double* _elimqrjs, Double* _rh59f4a7)
	{
#region variable declarations
Int32 _dwg2abd9 =  default;
Int32 _0isd60h3 =  default;
Int32 _kr2fml4a =  default;
Int32 _zmzafydt =  default;
Double _h3we9dab =  default;
Double _mgo62z1p =  default;
Int32 _9vgqarki =  default;
Int32 _pjsy11vi =  default;
Int32 _34fm7prn =  default;
Int32 _ld42zxsk =  default;
Int32 _qb0uu4i2 =  default;
Int32 _1mqgnnli =  default;
Int32 _g15rvq6a =  default;
Int32 _7hieka8c =  default;
Int32 _ivvongrz =  default;
Int32 _bjro92m6 =  default;
Int32 _ds0a02y3 =  default;
Int32 _fnz4j6q9 =  default;
Int32 _u3pr3bjv =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//C 
		
		_dwg2abd9 = INT(*(_rh59f4a7+((int)2 - 1)));
		_0isd60h3 = (int)0;
		{
			System.Int32 __81fgg2dlsvn510 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step510 = (System.Int32)((int)1);
			System.Int32 __81fgg2count510;
			for (__81fgg2count510 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dwg2abd9) - __81fgg2dlsvn510 + __81fgg2step510) / __81fgg2step510)), _kr2fml4a = __81fgg2dlsvn510; __81fgg2count510 != 0; __81fgg2count510--, _kr2fml4a += (__81fgg2step510)) {

			{
				
				_zmzafydt = INT(*(_rh59f4a7+(_kr2fml4a + (int)2 - 1)));
				_0isd60h3 = ((int)1 - _0isd60h3);
				if (_zmzafydt <= (int)5)goto Mark10;
				if (_kr2fml4a == _dwg2abd9)goto Mark10;
				_0isd60h3 = ((int)1 - _0isd60h3);
Mark10:;
				// continue
			}
						}		}
		_h3we9dab = (double)0.5;
		_mgo62z1p = (double)-0.5;
		_9vgqarki = ILNumerics.F2NET.Intrinsics.MOD(_99xbntpe ,(int)2 );
		_pjsy11vi = (_99xbntpe - (int)2);
		if (_9vgqarki != (int)0)
		_pjsy11vi = (_99xbntpe - (int)1);
		if (_0isd60h3 == (int)0)goto Mark120;
		_34fm7prn = ((int)1 - _b2ekxz2c);
		{
			System.Int32 __81fgg2dlsvn511 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step511 = (System.Int32)((int)1);
			System.Int32 __81fgg2count511;
			for (__81fgg2count511 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cf4kqqk2) - __81fgg2dlsvn511 + __81fgg2step511) / __81fgg2step511)), _ld42zxsk = __81fgg2dlsvn511; __81fgg2count511 != 0; __81fgg2count511--, _ld42zxsk += (__81fgg2step511)) {

			{
				
				_34fm7prn = (_34fm7prn + _b2ekxz2c);
				*(_tyzpsh18+(_ld42zxsk - 1) + ((int)1 - 1) * 1 * (_cf4kqqk2)) = *(_4zklolxv+(_34fm7prn - 1) + ((int)1 - 1) * 1 * (_pxpm3zlt));
				*(_tyzpsh18+(_ld42zxsk - 1) + (_99xbntpe - 1) * 1 * (_cf4kqqk2)) = *(_4zklolxv+(_34fm7prn - 1) + (_99xbntpe - 1) * 1 * (_pxpm3zlt));
Mark117:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn512 = (System.Int32)((int)2);
			System.Int32 __81fgg2step512 = (System.Int32)((int)2);
			System.Int32 __81fgg2count512;
			for (__81fgg2count512 = System.Math.Max(0, (System.Int32)(((System.Int32)(_pjsy11vi) - __81fgg2dlsvn512 + __81fgg2step512) / __81fgg2step512)), _qb0uu4i2 = __81fgg2dlsvn512; __81fgg2count512 != 0; __81fgg2count512--, _qb0uu4i2 += (__81fgg2step512)) {

			{
				
				_34fm7prn = ((int)1 - _b2ekxz2c);
				{
					System.Int32 __81fgg2dlsvn513 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step513 = (System.Int32)((int)1);
					System.Int32 __81fgg2count513;
					for (__81fgg2count513 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cf4kqqk2) - __81fgg2dlsvn513 + __81fgg2step513) / __81fgg2step513)), _ld42zxsk = __81fgg2dlsvn513; __81fgg2count513 != 0; __81fgg2count513--, _ld42zxsk += (__81fgg2step513)) {

					{
						
						_34fm7prn = (_34fm7prn + _b2ekxz2c);
						*(_tyzpsh18+(_ld42zxsk - 1) + (_qb0uu4i2 - 1) * 1 * (_cf4kqqk2)) = (_h3we9dab * *(_4zklolxv+(_34fm7prn - 1) + (_qb0uu4i2 - 1) * 1 * (_pxpm3zlt)));
						*(_tyzpsh18+(_ld42zxsk - 1) + (_qb0uu4i2 + (int)1 - 1) * 1 * (_cf4kqqk2)) = (_mgo62z1p * *(_4zklolxv+(_34fm7prn - 1) + (_qb0uu4i2 + (int)1 - 1) * 1 * (_pxpm3zlt)));
Mark118:;
						// continue
					}
										}				}
			}
						}		}goto Mark124;
Mark120:;
		// continue
		{
			System.Int32 __81fgg2dlsvn514 = (System.Int32)((int)2);
			System.Int32 __81fgg2step514 = (System.Int32)((int)2);
			System.Int32 __81fgg2count514;
			for (__81fgg2count514 = System.Math.Max(0, (System.Int32)(((System.Int32)(_pjsy11vi) - __81fgg2dlsvn514 + __81fgg2step514) / __81fgg2step514)), _qb0uu4i2 = __81fgg2dlsvn514; __81fgg2count514 != 0; __81fgg2count514--, _qb0uu4i2 += (__81fgg2step514)) {

			{
				
				_34fm7prn = ((int)1 - _b2ekxz2c);
				{
					System.Int32 __81fgg2dlsvn515 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step515 = (System.Int32)((int)1);
					System.Int32 __81fgg2count515;
					for (__81fgg2count515 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cf4kqqk2) - __81fgg2dlsvn515 + __81fgg2step515) / __81fgg2step515)), _ld42zxsk = __81fgg2dlsvn515; __81fgg2count515 != 0; __81fgg2count515--, _ld42zxsk += (__81fgg2step515)) {

					{
						
						_34fm7prn = (_34fm7prn + _b2ekxz2c);
						*(_4zklolxv+(_34fm7prn - 1) + (_qb0uu4i2 - 1) * 1 * (_pxpm3zlt)) = (_h3we9dab * *(_4zklolxv+(_34fm7prn - 1) + (_qb0uu4i2 - 1) * 1 * (_pxpm3zlt)));
						*(_4zklolxv+(_34fm7prn - 1) + (_qb0uu4i2 + (int)1 - 1) * 1 * (_pxpm3zlt)) = (_mgo62z1p * *(_4zklolxv+(_34fm7prn - 1) + (_qb0uu4i2 + (int)1 - 1) * 1 * (_pxpm3zlt)));
Mark122:;
						// continue
					}
										}				}
			}
						}		}
Mark124:;
		
		_1mqgnnli = (int)1;
		_g15rvq6a = (int)1;
		{
			System.Int32 __81fgg2dlsvn516 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step516 = (System.Int32)((int)1);
			System.Int32 __81fgg2count516;
			for (__81fgg2count516 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dwg2abd9) - __81fgg2dlsvn516 + __81fgg2step516) / __81fgg2step516)), _kr2fml4a = __81fgg2dlsvn516; __81fgg2count516 != 0; __81fgg2count516--, _kr2fml4a += (__81fgg2step516)) {

			{
				
				_zmzafydt = INT(*(_rh59f4a7+(_kr2fml4a + (int)2 - 1)));
				_7hieka8c = (_zmzafydt * _1mqgnnli);
				_ivvongrz = (_99xbntpe / _7hieka8c);
				_bjro92m6 = (_ivvongrz * _1mqgnnli);
				if (_zmzafydt != (int)4)goto Mark103;
				_ds0a02y3 = (_g15rvq6a + _ivvongrz);
				_fnz4j6q9 = (_ds0a02y3 + _ivvongrz);
				if (_0isd60h3 != (int)0)goto Mark101;
				_teedmsbo(ref _cf4kqqk2 ,ref _ivvongrz ,ref _1mqgnnli ,_4zklolxv ,ref _b2ekxz2c ,ref _pxpm3zlt ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,ref _cf4kqqk2 ,(_elimqrjs+(_g15rvq6a - 1)),(_elimqrjs+(_ds0a02y3 - 1)),(_elimqrjs+(_fnz4j6q9 - 1)));goto Mark102;
Mark101:;
				
				_teedmsbo(ref _cf4kqqk2 ,ref _ivvongrz ,ref _1mqgnnli ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,ref _cf4kqqk2 ,_4zklolxv ,ref _b2ekxz2c ,ref _pxpm3zlt ,(_elimqrjs+(_g15rvq6a - 1)),(_elimqrjs+(_ds0a02y3 - 1)),(_elimqrjs+(_fnz4j6q9 - 1)));
Mark102:;
				
				_0isd60h3 = ((int)1 - _0isd60h3);goto Mark115;
Mark103:;
				
				if (_zmzafydt != (int)2)goto Mark106;
				if (_0isd60h3 != (int)0)goto Mark104;
				_8yphtier(ref _cf4kqqk2 ,ref _ivvongrz ,ref _1mqgnnli ,_4zklolxv ,ref _b2ekxz2c ,ref _pxpm3zlt ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,ref _cf4kqqk2 ,(_elimqrjs+(_g15rvq6a - 1)));goto Mark105;
Mark104:;
				
				_8yphtier(ref _cf4kqqk2 ,ref _ivvongrz ,ref _1mqgnnli ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,ref _cf4kqqk2 ,_4zklolxv ,ref _b2ekxz2c ,ref _pxpm3zlt ,(_elimqrjs+(_g15rvq6a - 1)));
Mark105:;
				
				_0isd60h3 = ((int)1 - _0isd60h3);goto Mark115;
Mark106:;
				
				if (_zmzafydt != (int)3)goto Mark109;
				_ds0a02y3 = (_g15rvq6a + _ivvongrz);
				if (_0isd60h3 != (int)0)goto Mark107;
				_357ca1rg(ref _cf4kqqk2 ,ref _ivvongrz ,ref _1mqgnnli ,_4zklolxv ,ref _b2ekxz2c ,ref _pxpm3zlt ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,ref _cf4kqqk2 ,(_elimqrjs+(_g15rvq6a - 1)),(_elimqrjs+(_ds0a02y3 - 1)));goto Mark108;
Mark107:;
				
				_357ca1rg(ref _cf4kqqk2 ,ref _ivvongrz ,ref _1mqgnnli ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,ref _cf4kqqk2 ,_4zklolxv ,ref _b2ekxz2c ,ref _pxpm3zlt ,(_elimqrjs+(_g15rvq6a - 1)),(_elimqrjs+(_ds0a02y3 - 1)));
Mark108:;
				
				_0isd60h3 = ((int)1 - _0isd60h3);goto Mark115;
Mark109:;
				
				if (_zmzafydt != (int)5)goto Mark112;
				_ds0a02y3 = (_g15rvq6a + _ivvongrz);
				_fnz4j6q9 = (_ds0a02y3 + _ivvongrz);
				_u3pr3bjv = (_fnz4j6q9 + _ivvongrz);
				if (_0isd60h3 != (int)0)goto Mark110;
				_8jtnzb8y(ref _cf4kqqk2 ,ref _ivvongrz ,ref _1mqgnnli ,_4zklolxv ,ref _b2ekxz2c ,ref _pxpm3zlt ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,ref _cf4kqqk2 ,(_elimqrjs+(_g15rvq6a - 1)),(_elimqrjs+(_ds0a02y3 - 1)),(_elimqrjs+(_fnz4j6q9 - 1)),(_elimqrjs+(_u3pr3bjv - 1)));goto Mark111;
Mark110:;
				
				_8jtnzb8y(ref _cf4kqqk2 ,ref _ivvongrz ,ref _1mqgnnli ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,ref _cf4kqqk2 ,_4zklolxv ,ref _b2ekxz2c ,ref _pxpm3zlt ,(_elimqrjs+(_g15rvq6a - 1)),(_elimqrjs+(_ds0a02y3 - 1)),(_elimqrjs+(_fnz4j6q9 - 1)),(_elimqrjs+(_u3pr3bjv - 1)));
Mark111:;
				
				_0isd60h3 = ((int)1 - _0isd60h3);goto Mark115;
Mark112:;
				
				if (_0isd60h3 != (int)0)goto Mark113;
				_mt77zbmg(ref _cf4kqqk2 ,ref _ivvongrz ,ref _zmzafydt ,ref _1mqgnnli ,ref _bjro92m6 ,_4zklolxv ,_4zklolxv ,_4zklolxv ,ref _b2ekxz2c ,ref _pxpm3zlt ,_tyzpsh18 ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,ref _cf4kqqk2 ,(_elimqrjs+(_g15rvq6a - 1)));goto Mark114;
Mark113:;
				
				_mt77zbmg(ref _cf4kqqk2 ,ref _ivvongrz ,ref _zmzafydt ,ref _1mqgnnli ,ref _bjro92m6 ,_tyzpsh18 ,_tyzpsh18 ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,ref _cf4kqqk2 ,_4zklolxv ,_4zklolxv ,ref _b2ekxz2c ,ref _pxpm3zlt ,(_elimqrjs+(_g15rvq6a - 1)));
Mark114:;
				
				if (_ivvongrz == (int)1)
				_0isd60h3 = ((int)1 - _0isd60h3);
Mark115:;
				
				_1mqgnnli = _7hieka8c;
				_g15rvq6a = (_g15rvq6a + ((_zmzafydt - (int)1) * _ivvongrz));
Mark116:;
				// continue
			}
						}		}
		return;
	}
	
	} // 177

} // end class 
} // end namespace
#endif
