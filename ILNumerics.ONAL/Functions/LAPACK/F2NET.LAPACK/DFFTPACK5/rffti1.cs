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

	 
	public static void _umif87vs(ref Int32 _99xbntpe, Double* _elimqrjs, Double* _rh59f4a7)
	{
#region variable declarations
Double _uur4gpcs =  default;
Double _yn3hkl2r =  default;
Double _b0pxlrgp =  default;
Double _fcvylu1s =  default;
Int32 _pjsy11vi =  default;
Int32 _dwg2abd9 =  default;
Int32 _qb0uu4i2 =  default;
Int32 _72kdq0tf =  default;
Int32 _80w6f66o =  default;
Int32 _mhn1tpiz =  default;
Int32 _ld42zxsk =  default;
Int32 _pst1znx3 =  default;
Int32 _w0dwmzet =  default;
Int32 _g7s8h45d =  default;
Int32 _1mqgnnli =  default;
Int32 _kr2fml4a =  default;
Int32 _zmzafydt =  default;
Int32 _f295m6ge =  default;
Int32 _7hieka8c =  default;
Int32 _ivvongrz =  default;
Int32 _wk112b1y =  default;
Double _dvbvnucq =  default;
Int32 _7bsob2kv =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		
		{var vals = new Int32[] { (int)4,(int)2,(int)3,(int)5 };var valsIter = 0;

		*(__rffti1._prothyj8+((int)1 - 1)) = vals[valsIter++];
		*(__rffti1._prothyj8+((int)2 - 1)) = vals[valsIter++];
		*(__rffti1._prothyj8+((int)3 - 1)) = vals[valsIter++];
		*(__rffti1._prothyj8+((int)4 - 1)) = vals[valsIter++];
		}//C 
		
		_pjsy11vi = _99xbntpe;
		_dwg2abd9 = (int)0;
		_qb0uu4i2 = (int)0;
Mark101:;
		
		_qb0uu4i2 = (_qb0uu4i2 + (int)1);if ((_qb0uu4i2 - (int)4) < 0) goto Mark102; else if ((_qb0uu4i2 - (int)4) == 0) goto Mark102; else goto Mark103;
Mark102:;
		
		_72kdq0tf = *(__rffti1._prothyj8+(_qb0uu4i2 - 1));goto Mark104;
Mark103:;
		
		_72kdq0tf = (_72kdq0tf + (int)2);
Mark104:;
		
		_80w6f66o = (_pjsy11vi / _72kdq0tf);
		_mhn1tpiz = (_pjsy11vi - (_72kdq0tf * _80w6f66o));if ((_mhn1tpiz) < 0) goto Mark101; else if ((_mhn1tpiz) == 0) goto Mark105; else goto Mark101;
Mark105:;
		
		_dwg2abd9 = (_dwg2abd9 + (int)1);
		*(_rh59f4a7+(_dwg2abd9 + (int)2 - 1)) = DBLE(_72kdq0tf);
		_pjsy11vi = _80w6f66o;
		if (_72kdq0tf != (int)2)goto Mark107;
		if (_dwg2abd9 == (int)1)goto Mark107;
		{
			System.Int32 __81fgg2dlsvn225 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step225 = (System.Int32)((int)1);
			System.Int32 __81fgg2count225;
			for (__81fgg2count225 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dwg2abd9) - __81fgg2dlsvn225 + __81fgg2step225) / __81fgg2step225)), _ld42zxsk = __81fgg2dlsvn225; __81fgg2count225 != 0; __81fgg2count225--, _ld42zxsk += (__81fgg2step225)) {

			{
				
				_pst1znx3 = ((_dwg2abd9 - _ld42zxsk) + (int)2);
				*(_rh59f4a7+(_pst1znx3 + (int)2 - 1)) = *(_rh59f4a7+(_pst1znx3 + (int)1 - 1));
Mark106:;
				// continue
			}
						}		}
		*(_rh59f4a7+((int)3 - 1)) = DBLE((int)2);
Mark107:;
		
		if (_pjsy11vi != (int)1)goto Mark104;
		*(_rh59f4a7+((int)1 - 1)) = DBLE(_99xbntpe);
		*(_rh59f4a7+((int)2 - 1)) = DBLE(_dwg2abd9);
		_uur4gpcs = ((double)8d * ILNumerics.F2NET.Intrinsics.DATAN((double)1d ));
		_yn3hkl2r = (_uur4gpcs / ILNumerics.F2NET.Intrinsics.DBLE(_99xbntpe ));
		_w0dwmzet = (int)0;
		_g7s8h45d = (_dwg2abd9 - (int)1);
		_1mqgnnli = (int)1;
		if (_g7s8h45d == (int)0)
		return;
		{
			System.Int32 __81fgg2dlsvn226 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step226 = (System.Int32)((int)1);
			System.Int32 __81fgg2count226;
			for (__81fgg2count226 = System.Math.Max(0, (System.Int32)(((System.Int32)(_g7s8h45d) - __81fgg2dlsvn226 + __81fgg2step226) / __81fgg2step226)), _kr2fml4a = __81fgg2dlsvn226; __81fgg2count226 != 0; __81fgg2count226--, _kr2fml4a += (__81fgg2step226)) {

			{
				
				_zmzafydt = INT(*(_rh59f4a7+(_kr2fml4a + (int)2 - 1)));
				_f295m6ge = (int)0;
				_7hieka8c = (_1mqgnnli * _zmzafydt);
				_ivvongrz = (_99xbntpe / _7hieka8c);
				_wk112b1y = (_zmzafydt - (int)1);
				{
					System.Int32 __81fgg2dlsvn227 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step227 = (System.Int32)((int)1);
					System.Int32 __81fgg2count227;
					for (__81fgg2count227 = System.Math.Max(0, (System.Int32)(((System.Int32)(_wk112b1y) - __81fgg2dlsvn227 + __81fgg2step227) / __81fgg2step227)), _qb0uu4i2 = __81fgg2dlsvn227; __81fgg2count227 != 0; __81fgg2count227--, _qb0uu4i2 += (__81fgg2step227)) {

					{
						
						_f295m6ge = (_f295m6ge + _1mqgnnli);
						_ld42zxsk = _w0dwmzet;
						_b0pxlrgp = (ILNumerics.F2NET.Intrinsics.DBLE(_f295m6ge ) * _yn3hkl2r);
						_dvbvnucq = (double)0;
						{
							System.Int32 __81fgg2dlsvn228 = (System.Int32)((int)3);
							System.Int32 __81fgg2step228 = (System.Int32)((int)2);
							System.Int32 __81fgg2count228;
							for (__81fgg2count228 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ivvongrz) - __81fgg2dlsvn228 + __81fgg2step228) / __81fgg2step228)), _7bsob2kv = __81fgg2dlsvn228; __81fgg2count228 != 0; __81fgg2count228--, _7bsob2kv += (__81fgg2step228)) {

							{
								
								_ld42zxsk = (_ld42zxsk + (int)2);
								_dvbvnucq = (_dvbvnucq + (double)1);
								_fcvylu1s = (_dvbvnucq * _b0pxlrgp);
								*(_elimqrjs+(_ld42zxsk - (int)1 - 1)) = DBLE(ILNumerics.F2NET.Intrinsics.DCOS(_fcvylu1s ));
								*(_elimqrjs+(_ld42zxsk - 1)) = DBLE(ILNumerics.F2NET.Intrinsics.DSIN(_fcvylu1s ));
Mark108:;
								// continue
							}
														}						}
						_w0dwmzet = (_w0dwmzet + _ivvongrz);
Mark109:;
						// continue
					}
										}				}
				_1mqgnnli = _7hieka8c;
Mark110:;
				// continue
			}
						}		}
		return;
	}
	
	} // 177


internal unsafe class __rffti1 { 
internal static MemoryHandle _prothyj8H_ =  ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<Int32>((ulong)((int)4));
internal static Int32* _prothyj8 = (Int32*)_prothyj8H_.Pointer;

}

} // end class 
} // end namespace
#endif
