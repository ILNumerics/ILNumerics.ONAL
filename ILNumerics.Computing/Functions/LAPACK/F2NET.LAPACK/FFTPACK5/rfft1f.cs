
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

	 
	public static void _ahut30qn(ref Int32 _99xbntpe, ref Int32 _x7l3p3wq, Single* _wkmvx6rb, ref Int32 _ifgqgx1i, Single* _uuvyil0u, ref Int32 _xjpd3das, Single* _y1uc90sp, ref Int32 _sybtf40y, ref Int32 _va7gplz4)
	{
#region variable declarations
string fLanavab = default;
#endregion  variable declarations

	{
		//C 
		
		_va7gplz4 = (int)0;//C 
		
		if (_ifgqgx1i < ((_x7l3p3wq * (_99xbntpe - (int)1)) + (int)1))
		{
			
			_va7gplz4 = (int)1;
			_yc1iu9qu("RFFT1F " ,ref Unsafe.AsRef((int)6) );
		}
		else
		if (_xjpd3das < ((_99xbntpe + ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.REAL(_99xbntpe ) ) / ILNumerics.F2NET.Intrinsics.LOG((float)2 ) )) + (int)4))
		{
			
			_va7gplz4 = (int)2;
			_yc1iu9qu("RFFT1F " ,ref Unsafe.AsRef((int)8) );
		}
		else
		if (_sybtf40y < _99xbntpe)
		{
			
			_va7gplz4 = (int)3;
			_yc1iu9qu("RFFT1F " ,ref Unsafe.AsRef((int)10) );
		}
		//C 
		
		if (_99xbntpe == (int)1)
		return;//C 
		
		_xn5a525m(ref _99xbntpe ,ref _x7l3p3wq ,_wkmvx6rb ,_y1uc90sp ,_uuvyil0u ,(_uuvyil0u+(_99xbntpe + (int)1 - 1)));
		return;
	}
	
	} // 177

} // end class 
} // end namespace
#endif
