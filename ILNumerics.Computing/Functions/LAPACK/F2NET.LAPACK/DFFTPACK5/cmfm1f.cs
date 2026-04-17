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

	 
	public static void _pk54fb3n(ref Int32 _b6wzgw7j, ref Int32 _zaw1d3zs, ref Int32 _99xbntpe, ref Int32 _x7l3p3wq, complex* _4zklolxv, Double* _tyzpsh18, Double* _elimqrjs, ref Double _56ihyirc, Double* _rh59f4a7)
	{
#region variable declarations
Int32 _dwg2abd9 =  default;
Int32 _0isd60h3 =  default;
Int32 _1mqgnnli =  default;
Int32 _g15rvq6a =  default;
Int32 _kr2fml4a =  default;
Int32 _zmzafydt =  default;
Int32 _7hieka8c =  default;
Int32 _ivvongrz =  default;
Int32 _p39hja58 =  default;
Int32 _duhmm6td =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//C 
		//C FFTPACK 5.0 auxiliary routine 
		//C 
		
		_dwg2abd9 = INT(_56ihyirc);
		_0isd60h3 = (int)0;
		_1mqgnnli = (int)1;
		_g15rvq6a = (int)1;
		{
			System.Int32 __81fgg2dlsvn163 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step163 = (System.Int32)((int)1);
			System.Int32 __81fgg2count163;
			for (__81fgg2count163 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dwg2abd9) - __81fgg2dlsvn163 + __81fgg2step163) / __81fgg2step163)), _kr2fml4a = __81fgg2dlsvn163; __81fgg2count163 != 0; __81fgg2count163--, _kr2fml4a += (__81fgg2step163)) {

			{
				
				_zmzafydt = INT(*(_rh59f4a7+(_kr2fml4a - 1)));
				_7hieka8c = (_zmzafydt * _1mqgnnli);
				_ivvongrz = (_99xbntpe / _7hieka8c);
				_p39hja58 = (_1mqgnnli * _ivvongrz);
				_duhmm6td = (((int)1 + _0isd60h3) + ((int)2 * ILNumerics.F2NET.Intrinsics.MIN(_zmzafydt - (int)2 ,(int)4 )));
				switch (_duhmm6td) {
										case 1:
					goto Mark52;
					case 2:
					goto Mark62;
					case 3:
					goto Mark53;
					case 4:
					goto Mark63;
					case 5:
					goto Mark54;
					case 6:
					goto Mark64;
					case 7:
					goto Mark55;
					case 8:
					goto Mark65;
					case 9:
					goto Mark56;
					case 10:
					goto Mark66;
					default:
					break;
				}

Mark52:;
				
				_b1up3tym(ref _b6wzgw7j ,ref _ivvongrz ,ref _1mqgnnli ,ref _0isd60h3 ,(System.Double*)(void*)(_4zklolxv) ,ref _zaw1d3zs ,ref _x7l3p3wq ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,ref _b6wzgw7j ,(_elimqrjs+(_g15rvq6a - 1)));goto Mark120;
Mark62:;
				
				_b1up3tym(ref _b6wzgw7j ,ref _ivvongrz ,ref _1mqgnnli ,ref _0isd60h3 ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,ref _b6wzgw7j ,(System.Double*)(void*)(_4zklolxv) ,ref _zaw1d3zs ,ref _x7l3p3wq ,(_elimqrjs+(_g15rvq6a - 1)));goto Mark120;
Mark53:;
				
				_thmokna6(ref _b6wzgw7j ,ref _ivvongrz ,ref _1mqgnnli ,ref _0isd60h3 ,(System.Double*)(void*)(_4zklolxv) ,ref _zaw1d3zs ,ref _x7l3p3wq ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,ref _b6wzgw7j ,(_elimqrjs+(_g15rvq6a - 1)));goto Mark120;
Mark63:;
				
				_thmokna6(ref _b6wzgw7j ,ref _ivvongrz ,ref _1mqgnnli ,ref _0isd60h3 ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,ref _b6wzgw7j ,(System.Double*)(void*)(_4zklolxv) ,ref _zaw1d3zs ,ref _x7l3p3wq ,(_elimqrjs+(_g15rvq6a - 1)));goto Mark120;
Mark54:;
				
				_b0d70tvk(ref _b6wzgw7j ,ref _ivvongrz ,ref _1mqgnnli ,ref _0isd60h3 ,(System.Double*)(void*)(_4zklolxv) ,ref _zaw1d3zs ,ref _x7l3p3wq ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,ref _b6wzgw7j ,(_elimqrjs+(_g15rvq6a - 1)));goto Mark120;
Mark64:;
				
				_b0d70tvk(ref _b6wzgw7j ,ref _ivvongrz ,ref _1mqgnnli ,ref _0isd60h3 ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,ref _b6wzgw7j ,(System.Double*)(void*)(_4zklolxv) ,ref _zaw1d3zs ,ref _x7l3p3wq ,(_elimqrjs+(_g15rvq6a - 1)));goto Mark120;
Mark55:;
				
				_s8kq7w21(ref _b6wzgw7j ,ref _ivvongrz ,ref _1mqgnnli ,ref _0isd60h3 ,(System.Double*)(void*)(_4zklolxv) ,ref _zaw1d3zs ,ref _x7l3p3wq ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,ref _b6wzgw7j ,(_elimqrjs+(_g15rvq6a - 1)));goto Mark120;
Mark65:;
				
				_s8kq7w21(ref _b6wzgw7j ,ref _ivvongrz ,ref _1mqgnnli ,ref _0isd60h3 ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,ref _b6wzgw7j ,(System.Double*)(void*)(_4zklolxv) ,ref _zaw1d3zs ,ref _x7l3p3wq ,(_elimqrjs+(_g15rvq6a - 1)));goto Mark120;
Mark56:;
				
				_berrw4eb(ref _b6wzgw7j ,ref _ivvongrz ,ref _zmzafydt ,ref _1mqgnnli ,ref _p39hja58 ,ref _0isd60h3 ,(System.Double*)(void*)(_4zklolxv) ,(System.Double*)(void*)(_4zklolxv) ,ref _zaw1d3zs ,ref _x7l3p3wq ,_tyzpsh18 ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,ref _b6wzgw7j ,(_elimqrjs+(_g15rvq6a - 1)));goto Mark120;
Mark66:;
				
				_berrw4eb(ref _b6wzgw7j ,ref _ivvongrz ,ref _zmzafydt ,ref _1mqgnnli ,ref _p39hja58 ,ref _0isd60h3 ,_tyzpsh18 ,_tyzpsh18 ,ref Unsafe.AsRef((int)1) ,ref _b6wzgw7j ,(System.Double*)(void*)(_4zklolxv) ,(System.Double*)(void*)(_4zklolxv) ,ref _zaw1d3zs ,ref _x7l3p3wq ,(_elimqrjs+(_g15rvq6a - 1)));
Mark120:;
				
				_1mqgnnli = _7hieka8c;
				_g15rvq6a = (_g15rvq6a + ((_zmzafydt - (int)1) * (_ivvongrz + _ivvongrz)));
				if (_zmzafydt <= (int)5)
				_0isd60h3 = ((int)1 - _0isd60h3);
Mark125:;
				// continue
			}
						}		}
		return;
	}
	
	} // 177

} // end class 
} // end namespace
#endif
