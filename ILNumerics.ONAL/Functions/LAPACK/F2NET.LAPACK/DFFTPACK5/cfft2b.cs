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

	 
	public static void _s4vtj3jv(ref Int32 _14t4iqnj, ref Int32 _cjahrwwv, ref Int32 _cf4kqqk2, complex* _4zklolxv, Double* _uuvyil0u, ref Int32 _xjpd3das, Double* _y1uc90sp, ref Int32 _sybtf40y, ref Int32 _va7gplz4)
	{
#region variable declarations
Int32 _g15rvq6a =  default;
Int32 _fn23rpdf =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//C 
		//C Initialize error return 
		//C 
		
		_va7gplz4 = (int)0;//C 
		
		if (_cjahrwwv > _14t4iqnj)
		{
			
			_va7gplz4 = (int)5;
			_yc1iu9qu("CFFT2B" ,ref Unsafe.AsRef((int)-2) );goto Mark100;
		}
		else
		if (_xjpd3das < ((((((int)2 * _cjahrwwv) + ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.DBLE(_cjahrwwv ) ) / ILNumerics.F2NET.Intrinsics.LOG((double)2 ) )) + ((int)2 * _cf4kqqk2)) + ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.DBLE(_cf4kqqk2 ) ) / ILNumerics.F2NET.Intrinsics.LOG((double)2 ) )) + (int)8))
		{
			
			_va7gplz4 = (int)2;
			_yc1iu9qu("CFFT2B" ,ref Unsafe.AsRef((int)6) );goto Mark100;
		}
		else
		if (_sybtf40y < (((int)2 * _cjahrwwv) * _cf4kqqk2))
		{
			
			_va7gplz4 = (int)3;
			_yc1iu9qu("CFFT2B" ,ref Unsafe.AsRef((int)8) );goto Mark100;
		}
		//C 
		//C Transform X lines of C array 
		
		_g15rvq6a = ((((int)2 * _cjahrwwv) + ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.DBLE(_cjahrwwv ) ) / ILNumerics.F2NET.Intrinsics.LOG((double)2 ) )) + (int)3);
		_gw6i40vj(ref _cjahrwwv ,ref Unsafe.AsRef((int)1) ,ref _cf4kqqk2 ,ref _14t4iqnj ,_4zklolxv ,ref Unsafe.AsRef(((_cjahrwwv - (int)1) + (_14t4iqnj * (_cf4kqqk2 - (int)1))) + (int)1) ,(_uuvyil0u+(_g15rvq6a - 1)),ref Unsafe.AsRef((((int)2 * _cf4kqqk2) + ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.DBLE(_cf4kqqk2 ) ) / ILNumerics.F2NET.Intrinsics.LOG((double)2 ) )) + (int)4) ,_y1uc90sp ,ref Unsafe.AsRef(((int)2 * _cjahrwwv) * _cf4kqqk2) ,ref _fn23rpdf );
		if (_fn23rpdf != (int)0)
		{
			
			_va7gplz4 = (int)20;
			_yc1iu9qu("CFFT2B" ,ref Unsafe.AsRef((int)-5) );goto Mark100;
		}
		//C 
		//C Transform Y lines of C array 
		
		_g15rvq6a = (int)1;
		_gw6i40vj(ref _cf4kqqk2 ,ref _14t4iqnj ,ref _cjahrwwv ,ref Unsafe.AsRef((int)1) ,_4zklolxv ,ref Unsafe.AsRef(((_cf4kqqk2 - (int)1) * _14t4iqnj) + _cjahrwwv) ,(_uuvyil0u+(_g15rvq6a - 1)),ref Unsafe.AsRef((((int)2 * _cjahrwwv) + ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.DBLE(_cjahrwwv ) ) / ILNumerics.F2NET.Intrinsics.LOG((double)2 ) )) + (int)4) ,_y1uc90sp ,ref Unsafe.AsRef(((int)2 * _cf4kqqk2) * _cjahrwwv) ,ref _fn23rpdf );
		if (_fn23rpdf != (int)0)
		{
			
			_va7gplz4 = (int)20;
			_yc1iu9qu("CFFT2B" ,ref Unsafe.AsRef((int)-5) );
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
