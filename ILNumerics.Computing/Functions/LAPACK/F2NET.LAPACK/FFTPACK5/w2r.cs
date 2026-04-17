
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

	 
	public static void _x1ghx7ba(ref Int32 _4djnmv2o, ref Int32 _jejunmnd, ref Int32 _cjahrwwv, ref Int32 _cf4kqqk2, Single* _wkmvx6rb, Single* _13s1ly0n)
	{
#region variable declarations
Int32 _qb0uu4i2 =  default;
Int32 _ld42zxsk =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//      dimension r(ldr,*),w(ldw,*)
		
		{
			System.Int32 __81fgg2dlsvn426 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step426 = (System.Int32)((int)1);
			System.Int32 __81fgg2count426;
			for (__81fgg2count426 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cf4kqqk2) - __81fgg2dlsvn426 + __81fgg2step426) / __81fgg2step426)), _qb0uu4i2 = __81fgg2dlsvn426; __81fgg2count426 != 0; __81fgg2count426--, _qb0uu4i2 += (__81fgg2step426)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn427 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step427 = (System.Int32)((int)1);
					System.Int32 __81fgg2count427;
					for (__81fgg2count427 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cjahrwwv) - __81fgg2dlsvn427 + __81fgg2step427) / __81fgg2step427)), _ld42zxsk = __81fgg2dlsvn427; __81fgg2count427 != 0; __81fgg2count427--, _ld42zxsk += (__81fgg2step427)) {

					{
						
						*(_wkmvx6rb+(_ld42zxsk - 1) + (_qb0uu4i2 - 1) * 1 * (_4djnmv2o)) = *(_13s1ly0n+(_ld42zxsk - 1) + (_qb0uu4i2 - 1) * 1 * (_jejunmnd));
					}
										}				}
			}
						}		}
		return;
	}
	
	} // 177

} // end class 
} // end namespace
#endif
