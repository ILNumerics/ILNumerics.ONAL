
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

	 
	public static void _ock1ycl5(ref Int32 _cjahrwwv, ref Int32 _cf4kqqk2, Single* _uuvyil0u, ref Int32 _xjpd3das, ref Int32 _va7gplz4)
	{
#region variable declarations
Int32 _xyk50wc4 =  default;
Int32 _j5x18dti =  default;
Int32 _wmsh4kw4 =  default;
Int32 _fn23rpdf =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//C 
		//C INITIALIZE IER 
		//C 
		
		_va7gplz4 = (int)0;//C 
		//C VERIFY LENSAV 
		//C 
		
		_xyk50wc4 = ((_cjahrwwv + ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.REAL(_cjahrwwv ) ) / ILNumerics.F2NET.Intrinsics.LOG((float)2 ) )) + (int)4);
		_j5x18dti = ((((int)2 * _cf4kqqk2) + ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.REAL(_cf4kqqk2 ) ) / ILNumerics.F2NET.Intrinsics.LOG((float)2 ) )) + (int)4);
		_wmsh4kw4 = ((_cf4kqqk2 + ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.REAL(_cf4kqqk2 ) ) / ILNumerics.F2NET.Intrinsics.LOG((float)2 ) )) + (int)4);
		if (_xjpd3das < ((_xyk50wc4 + _j5x18dti) + _wmsh4kw4))
		{
			
			_va7gplz4 = (int)2;
			_yc1iu9qu("RFFT2I" ,ref Unsafe.AsRef((int)4) );goto Mark100;
		}
		//C 
		
		_ace27n0e(ref _cjahrwwv ,(_uuvyil0u+((int)1 - 1)),ref _xyk50wc4 ,ref _fn23rpdf );
		if (_fn23rpdf != (int)0)
		{
			
			_va7gplz4 = (int)20;
			_yc1iu9qu("RFFT2I" ,ref Unsafe.AsRef((int)-5) );goto Mark100;
		}
		
		_d4ay1zl0(ref _cf4kqqk2 ,(_uuvyil0u+(_xyk50wc4 + (int)1 - 1)),ref _j5x18dti ,ref _fn23rpdf );
		if (_fn23rpdf != (int)0)
		{
			
			_va7gplz4 = (int)20;
			_yc1iu9qu("RFFT2I" ,ref Unsafe.AsRef((int)-5) );
		}
		//C 
		
		_ace27n0e(ref _cf4kqqk2 ,(_uuvyil0u+((_xyk50wc4 + _j5x18dti) + (int)1 - 1)),ref _wmsh4kw4 ,ref _fn23rpdf );
		if (_fn23rpdf != (int)0)
		{
			
			_va7gplz4 = (int)20;
			_yc1iu9qu("RFFT2I" ,ref Unsafe.AsRef((int)-5) );goto Mark100;
		}
		//C 
		
Mark100:;
		// continue
		return;
	}
	
	} // 177

} // end class 
} // end namespace
#endif
