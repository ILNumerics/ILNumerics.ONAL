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

	 
	public static void _p6esv8tm(ref Int32 _99xbntpe, ref Int32 _x7l3p3wq, complex* _4zklolxv, ref Int32 _iqxa9tr2, Double* _uuvyil0u, ref Int32 _xjpd3das, Double* _y1uc90sp, ref Int32 _sybtf40y, ref Int32 _va7gplz4)
	{
#region variable declarations
Int32 _64unh100 =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//C 
		
		_va7gplz4 = (int)0;//C 
		
		if (_iqxa9tr2 < ((_x7l3p3wq * (_99xbntpe - (int)1)) + (int)1))
		{
			
			_va7gplz4 = (int)1;
			_yc1iu9qu("CFFT1B " ,ref Unsafe.AsRef((int)4) );
		}
		else
		if (_xjpd3das < ((((int)2 * _99xbntpe) + ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.DBLE(_99xbntpe ) ) / ILNumerics.F2NET.Intrinsics.LOG((double)2 ) )) + (int)4))
		{
			
			_va7gplz4 = (int)2;
			_yc1iu9qu("CFFT1B " ,ref Unsafe.AsRef((int)6) );
		}
		else
		if (_sybtf40y < ((int)2 * _99xbntpe))
		{
			
			_va7gplz4 = (int)3;
			_yc1iu9qu("CFFT1B " ,ref Unsafe.AsRef((int)8) );
		}
		//C 
		
		if (_99xbntpe == (int)1)
		return;//C 
		
		_64unh100 = ((_99xbntpe + _99xbntpe) + (int)1);
		_xw72xt06(ref _99xbntpe ,ref _x7l3p3wq ,_4zklolxv ,_y1uc90sp ,_uuvyil0u ,ref Unsafe.AsRef(*(_uuvyil0u+(_64unh100 - 1))) ,(_uuvyil0u+(_64unh100 + (int)1 - 1)));
		return;
	}
	
	} // 177

} // end class 
} // end namespace
#endif
