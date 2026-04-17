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

	 
	public static void _xn5a525m(ref Int32 _99xbntpe, ref Int32 _pxpm3zlt, Double* _4zklolxv, Double* _tyzpsh18, Double* _elimqrjs, Double* _rh59f4a7)
	{
#region variable declarations
Int32 _dwg2abd9 =  default;
Int32 _0isd60h3 =  default;
Int32 _7hieka8c =  default;
Int32 _g15rvq6a =  default;
Int32 _kr2fml4a =  default;
Int32 _fck7ocol =  default;
Int32 _zmzafydt =  default;
Int32 _1mqgnnli =  default;
Int32 _ivvongrz =  default;
Int32 _bjro92m6 =  default;
Int32 _ds0a02y3 =  default;
Int32 _fnz4j6q9 =  default;
Int32 _u3pr3bjv =  default;
Double _x1gqiqyx =  default;
Double _4kvac77u =  default;
Double _9t6mb5vw =  default;
Int32 _9vgqarki =  default;
Int32 _pjsy11vi =  default;
Int32 _qb0uu4i2 =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//C 
		
		_dwg2abd9 = INT(*(_rh59f4a7+((int)2 - 1)));
		_0isd60h3 = (int)1;
		_7hieka8c = _99xbntpe;
		_g15rvq6a = _99xbntpe;
		{
			System.Int32 __81fgg2dlsvn279 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step279 = (System.Int32)((int)1);
			System.Int32 __81fgg2count279;
			for (__81fgg2count279 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dwg2abd9) - __81fgg2dlsvn279 + __81fgg2step279) / __81fgg2step279)), _kr2fml4a = __81fgg2dlsvn279; __81fgg2count279 != 0; __81fgg2count279--, _kr2fml4a += (__81fgg2step279)) {

			{
				
				_fck7ocol = (_dwg2abd9 - _kr2fml4a);
				_zmzafydt = INT(*(_rh59f4a7+(_fck7ocol + (int)3 - 1)));
				_1mqgnnli = (_7hieka8c / _zmzafydt);
				_ivvongrz = (_99xbntpe / _7hieka8c);
				_bjro92m6 = (_ivvongrz * _1mqgnnli);
				_g15rvq6a = (_g15rvq6a - ((_zmzafydt - (int)1) * _ivvongrz));
				_0isd60h3 = ((int)1 - _0isd60h3);
				if (_zmzafydt != (int)4)goto Mark102;
				_ds0a02y3 = (_g15rvq6a + _ivvongrz);
				_fnz4j6q9 = (_ds0a02y3 + _ivvongrz);
				if (_0isd60h3 != (int)0)goto Mark101;
				_wpv6n2rz(ref _ivvongrz ,ref _1mqgnnli ,_4zklolxv ,ref _pxpm3zlt ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,(_elimqrjs+(_g15rvq6a - 1)),(_elimqrjs+(_ds0a02y3 - 1)),(_elimqrjs+(_fnz4j6q9 - 1)));goto Mark110;
Mark101:;
				
				_wpv6n2rz(ref _ivvongrz ,ref _1mqgnnli ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,_4zklolxv ,ref _pxpm3zlt ,(_elimqrjs+(_g15rvq6a - 1)),(_elimqrjs+(_ds0a02y3 - 1)),(_elimqrjs+(_fnz4j6q9 - 1)));goto Mark110;
Mark102:;
				
				if (_zmzafydt != (int)2)goto Mark104;
				if (_0isd60h3 != (int)0)goto Mark103;
				_zag35hk1(ref _ivvongrz ,ref _1mqgnnli ,_4zklolxv ,ref _pxpm3zlt ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,(_elimqrjs+(_g15rvq6a - 1)));goto Mark110;
Mark103:;
				
				_zag35hk1(ref _ivvongrz ,ref _1mqgnnli ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,_4zklolxv ,ref _pxpm3zlt ,(_elimqrjs+(_g15rvq6a - 1)));goto Mark110;
Mark104:;
				
				if (_zmzafydt != (int)3)goto Mark106;
				_ds0a02y3 = (_g15rvq6a + _ivvongrz);
				if (_0isd60h3 != (int)0)goto Mark105;
				_7fb1iozl(ref _ivvongrz ,ref _1mqgnnli ,_4zklolxv ,ref _pxpm3zlt ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,(_elimqrjs+(_g15rvq6a - 1)),(_elimqrjs+(_ds0a02y3 - 1)));goto Mark110;
Mark105:;
				
				_7fb1iozl(ref _ivvongrz ,ref _1mqgnnli ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,_4zklolxv ,ref _pxpm3zlt ,(_elimqrjs+(_g15rvq6a - 1)),(_elimqrjs+(_ds0a02y3 - 1)));goto Mark110;
Mark106:;
				
				if (_zmzafydt != (int)5)goto Mark108;
				_ds0a02y3 = (_g15rvq6a + _ivvongrz);
				_fnz4j6q9 = (_ds0a02y3 + _ivvongrz);
				_u3pr3bjv = (_fnz4j6q9 + _ivvongrz);
				if (_0isd60h3 != (int)0)goto Mark107;
				_turqilay(ref _ivvongrz ,ref _1mqgnnli ,_4zklolxv ,ref _pxpm3zlt ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,(_elimqrjs+(_g15rvq6a - 1)),(_elimqrjs+(_ds0a02y3 - 1)),(_elimqrjs+(_fnz4j6q9 - 1)),(_elimqrjs+(_u3pr3bjv - 1)));goto Mark110;
Mark107:;
				
				_turqilay(ref _ivvongrz ,ref _1mqgnnli ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,_4zklolxv ,ref _pxpm3zlt ,(_elimqrjs+(_g15rvq6a - 1)),(_elimqrjs+(_ds0a02y3 - 1)),(_elimqrjs+(_fnz4j6q9 - 1)),(_elimqrjs+(_u3pr3bjv - 1)));goto Mark110;
Mark108:;
				
				if (_ivvongrz == (int)1)
				_0isd60h3 = ((int)1 - _0isd60h3);
				if (_0isd60h3 != (int)0)goto Mark109;
				_n2wpyrp6(ref _ivvongrz ,ref _zmzafydt ,ref _1mqgnnli ,ref _bjro92m6 ,_4zklolxv ,_4zklolxv ,_4zklolxv ,ref _pxpm3zlt ,_tyzpsh18 ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,(_elimqrjs+(_g15rvq6a - 1)));
				_0isd60h3 = (int)1;goto Mark110;
Mark109:;
				
				_n2wpyrp6(ref _ivvongrz ,ref _zmzafydt ,ref _1mqgnnli ,ref _bjro92m6 ,_tyzpsh18 ,_tyzpsh18 ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,_4zklolxv ,_4zklolxv ,ref _pxpm3zlt ,(_elimqrjs+(_g15rvq6a - 1)));
				_0isd60h3 = (int)0;
Mark110:;
				
				_7hieka8c = _1mqgnnli;
Mark111:;
				// continue
			}
						}		}
		_x1gqiqyx = ((double)1 / _99xbntpe);
		_4kvac77u = ((double)2 / _99xbntpe);
		_9t6mb5vw = (-(_4kvac77u));
		_9vgqarki = ILNumerics.F2NET.Intrinsics.MOD(_99xbntpe ,(int)2 );
		_pjsy11vi = (_99xbntpe - (int)2);
		if (_9vgqarki != (int)0)
		_pjsy11vi = (_99xbntpe - (int)1);
		if (_0isd60h3 != (int)0)goto Mark120;
		*(_4zklolxv+((int)1 - 1) + ((int)1 - 1) * 1 * (_pxpm3zlt)) = (_x1gqiqyx * *(_tyzpsh18+((int)1 - 1)));
		{
			System.Int32 __81fgg2dlsvn280 = (System.Int32)((int)2);
			System.Int32 __81fgg2step280 = (System.Int32)((int)2);
			System.Int32 __81fgg2count280;
			for (__81fgg2count280 = System.Math.Max(0, (System.Int32)(((System.Int32)(_pjsy11vi) - __81fgg2dlsvn280 + __81fgg2step280) / __81fgg2step280)), _qb0uu4i2 = __81fgg2dlsvn280; __81fgg2count280 != 0; __81fgg2count280--, _qb0uu4i2 += (__81fgg2step280)) {

			{
				
				*(_4zklolxv+((int)1 - 1) + (_qb0uu4i2 - 1) * 1 * (_pxpm3zlt)) = (_4kvac77u * *(_tyzpsh18+(_qb0uu4i2 - 1)));
				*(_4zklolxv+((int)1 - 1) + (_qb0uu4i2 + (int)1 - 1) * 1 * (_pxpm3zlt)) = (_9t6mb5vw * *(_tyzpsh18+(_qb0uu4i2 + (int)1 - 1)));
Mark118:;
				// continue
			}
						}		}
		if (_9vgqarki != (int)0)
		return;
		*(_4zklolxv+((int)1 - 1) + (_99xbntpe - 1) * 1 * (_pxpm3zlt)) = (_x1gqiqyx * *(_tyzpsh18+(_99xbntpe - 1)));
		return;
Mark120:;
		
		*(_4zklolxv+((int)1 - 1) + ((int)1 - 1) * 1 * (_pxpm3zlt)) = (_x1gqiqyx * *(_4zklolxv+((int)1 - 1) + ((int)1 - 1) * 1 * (_pxpm3zlt)));
		{
			System.Int32 __81fgg2dlsvn281 = (System.Int32)((int)2);
			System.Int32 __81fgg2step281 = (System.Int32)((int)2);
			System.Int32 __81fgg2count281;
			for (__81fgg2count281 = System.Math.Max(0, (System.Int32)(((System.Int32)(_pjsy11vi) - __81fgg2dlsvn281 + __81fgg2step281) / __81fgg2step281)), _qb0uu4i2 = __81fgg2dlsvn281; __81fgg2count281 != 0; __81fgg2count281--, _qb0uu4i2 += (__81fgg2step281)) {

			{
				
				*(_4zklolxv+((int)1 - 1) + (_qb0uu4i2 - 1) * 1 * (_pxpm3zlt)) = (_4kvac77u * *(_4zklolxv+((int)1 - 1) + (_qb0uu4i2 - 1) * 1 * (_pxpm3zlt)));
				*(_4zklolxv+((int)1 - 1) + (_qb0uu4i2 + (int)1 - 1) * 1 * (_pxpm3zlt)) = (_9t6mb5vw * *(_4zklolxv+((int)1 - 1) + (_qb0uu4i2 + (int)1 - 1) * 1 * (_pxpm3zlt)));
Mark122:;
				// continue
			}
						}		}
		if (_9vgqarki != (int)0)
		return;
		*(_4zklolxv+((int)1 - 1) + (_99xbntpe - 1) * 1 * (_pxpm3zlt)) = (_x1gqiqyx * *(_4zklolxv+((int)1 - 1) + (_99xbntpe - 1) * 1 * (_pxpm3zlt)));
		return;
	}
	
	} // 177

} // end class 
} // end namespace
#endif
