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

	 
	public static void _4b22km0z(ref Int32 _99xbntpe, ref Int32 _dwg2abd9, Double* _rh59f4a7)
	{
#region variable declarations
Int32 _pjsy11vi =  default;
Int32 _qb0uu4i2 =  default;
Int32 _72kdq0tf =  default;
Int32 _80w6f66o =  default;
Int32 _mhn1tpiz =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		
		{var vals = new Int32[] { (int)4,(int)2,(int)3,(int)5 };var valsIter = 0;

		*(__factor._prothyj8+((int)1 - 1)) = vals[valsIter++];
		*(__factor._prothyj8+((int)2 - 1)) = vals[valsIter++];
		*(__factor._prothyj8+((int)3 - 1)) = vals[valsIter++];
		*(__factor._prothyj8+((int)4 - 1)) = vals[valsIter++];
		}//C 
		
		_pjsy11vi = _99xbntpe;
		_dwg2abd9 = (int)0;
		_qb0uu4i2 = (int)0;
Mark101:;
		
		_qb0uu4i2 = (_qb0uu4i2 + (int)1);if ((_qb0uu4i2 - (int)4) < 0) goto Mark102; else if ((_qb0uu4i2 - (int)4) == 0) goto Mark102; else goto Mark103;
Mark102:;
		
		_72kdq0tf = *(__factor._prothyj8+(_qb0uu4i2 - 1));goto Mark104;
Mark103:;
		
		_72kdq0tf = (_72kdq0tf + (int)2);
Mark104:;
		
		_80w6f66o = (_pjsy11vi / _72kdq0tf);
		_mhn1tpiz = (_pjsy11vi - (_72kdq0tf * _80w6f66o));if ((_mhn1tpiz) < 0) goto Mark101; else if ((_mhn1tpiz) == 0) goto Mark105; else goto Mark101;
Mark105:;
		
		_dwg2abd9 = (_dwg2abd9 + (int)1);
		*(_rh59f4a7+(_dwg2abd9 - 1)) = DBLE(_72kdq0tf);
		_pjsy11vi = _80w6f66o;
		if (_pjsy11vi != (int)1)goto Mark104;
		return;
	}
	
	} // 177


internal unsafe class __factor { 
internal static MemoryHandle _prothyj8H_ =  ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<Int32>((ulong)((int)4));
internal static Int32* _prothyj8 = (Int32*)_prothyj8H_.Pointer;

}

} // end class 
} // end namespace
#endif
