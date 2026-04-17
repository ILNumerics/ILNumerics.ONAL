
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

	 
	public static void _t4yr1m7z(ref Int32 _99xbntpe, Single* _uuvyil0u, ref Int32 _xjpd3das, ref Int32 _va7gplz4)
	{
#region variable declarations
string fLanavab = default;
#endregion  variable declarations

	{
		//C 
		
		_va7gplz4 = (int)0;//C 
		
		if (_xjpd3das < ((_99xbntpe + ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.REAL(_99xbntpe ) ) / ILNumerics.F2NET.Intrinsics.LOG((float)2 ) )) + (int)4))
		{
			
			_va7gplz4 = (int)2;
			_yc1iu9qu("RFFT1I " ,ref Unsafe.AsRef((int)3) );
		}
		//C 
		
		if (_99xbntpe == (int)1)
		return;//C 
		
		_umif87vs(ref _99xbntpe ,(_uuvyil0u+((int)1 - 1)),(_uuvyil0u+(_99xbntpe + (int)1 - 1)));
		return;
	}
	
	} // 177

} // end class 
} // end namespace
#endif
