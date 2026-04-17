
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

	 
	public static void _yc1iu9qu(FString _lf8syo38, ref Int32 _26g4g1wj)
	{
#region variable declarations
string fLanavab = default;
#endregion  variable declarations
_lf8syo38 = _lf8syo38.Convert(6);

	{
		//C 
		//C     .. Scalar Arguments .. 
		//C 
		//C     .. 
		//C 
		//C  Purpose 
		//C  ======= 
		//C 
		//C  XERFFT  is an error handler for library FFTPACK version 5.1 routines. 
		//C  It is called by an FFTPACK 5.1 routine if an input parameter has an 
		//C  invalid value.  A message is printed and execution stops. 
		//C 
		//C  Installers may consider modifying the STOP statement in order to 
		//C  call system-specific exception-handling facilities. 
		//C 
		//C  Arguments 
		//C  ========= 
		//C 
		//C  SRNAME  (input) CHARACTER*6 
		//C          The name of the routine which called XERFFT. 
		//C 
		//C  INFO    (input) INTEGER 
		//C          When a single  invalid parameter in the parameter list of 
		//C          the calling routine has been detected, INFO is the position 
		//C          of that parameter.  In the case when an illegal combination 
		//C          of LOT, JUMP, N, and INC has been detected, the calling 
		//C          subprogram calls XERFFT with INFO = -1. 
		//C 
		//C ===================================================================== 
		//C 
		//C     .. Executable Statements .. 
		//C 
		
		if (_26g4g1wj >= (int)1)
		{
			
			{

				fLanavab = "(A,A,A,I3,A)";
				var fLanamab = FormatParser.Parse(fLanavab).GetEnumerator();
				Helper.WRITEObjectFormatted((int)6, " ** On entry to ", fLanamab);
				Helper.WRITEObjectFormatted((int)6, _lf8syo38, fLanamab);
				Helper.WRITEObjectFormatted((int)6, " parameter number ", fLanamab);
				Helper.WRITEObjectFormatted((int)6, _26g4g1wj, fLanamab);
				Helper.WRITEObjectFormatted((int)6, " had an illegal value", fLanamab);
				Helper.WRITEObjectFormatted((int)6, Helper.Null , fLanamab);
			}

		}
		else
		if (_26g4g1wj == (int)-1)
		{
			
			{

				fLanavab = "(A,A,A,A)";
				var fLanamab = FormatParser.Parse(fLanavab).GetEnumerator();
				Helper.WRITEObjectFormatted((int)6, " ** On entry to ", fLanamab);
				Helper.WRITEObjectFormatted((int)6, _lf8syo38, fLanamab);
				Helper.WRITEObjectFormatted((int)6, " parameters LOT, JUMP, N and INC are inconsistent", fLanamab);
				Helper.WRITEObjectFormatted((int)6, Helper.Null , fLanamab);
			}

		}
		else
		if (_26g4g1wj == (int)-2)
		{
			
			{

				fLanavab = "(A,A,A,A)";
				var fLanamab = FormatParser.Parse(fLanavab).GetEnumerator();
				Helper.WRITEObjectFormatted((int)6, " ** On entry to ", fLanamab);
				Helper.WRITEObjectFormatted((int)6, _lf8syo38, fLanamab);
				Helper.WRITEObjectFormatted((int)6, " parameter L is greater than LDIM", fLanamab);
				Helper.WRITEObjectFormatted((int)6, Helper.Null , fLanamab);
			}

		}
		else
		if (_26g4g1wj == (int)-3)
		{
			
			{

				fLanavab = "(A,A,A,A)";
				var fLanamab = FormatParser.Parse(fLanavab).GetEnumerator();
				Helper.WRITEObjectFormatted((int)6, " ** On entry to ", fLanamab);
				Helper.WRITEObjectFormatted((int)6, _lf8syo38, fLanamab);
				Helper.WRITEObjectFormatted((int)6, " parameter M is greater than MDIM", fLanamab);
				Helper.WRITEObjectFormatted((int)6, Helper.Null , fLanamab);
			}

		}
		else
		if (_26g4g1wj == (int)-5)
		{
			
			{

				fLanavab = "(A,A,A,A)";
				var fLanamab = FormatParser.Parse(fLanavab).GetEnumerator();
				Helper.WRITEObjectFormatted((int)6, " ** Within ", fLanamab);
				Helper.WRITEObjectFormatted((int)6, _lf8syo38, fLanamab);
				Helper.WRITEObjectFormatted((int)6, " input error returned by lower level routine", fLanamab);
				Helper.WRITEObjectFormatted((int)6, Helper.Null , fLanamab);
			}

		}
		else
		if (_26g4g1wj == (int)-6)
		{
			
			{

				fLanavab = "(A,A,A,A)";
				var fLanamab = FormatParser.Parse(fLanavab).GetEnumerator();
				Helper.WRITEObjectFormatted((int)6, " ** On entry to ", fLanamab);
				Helper.WRITEObjectFormatted((int)6, _lf8syo38, fLanamab);
				Helper.WRITEObjectFormatted((int)6, " parameter LDIM is less than 2*(L/2+1)", fLanamab);
				Helper.WRITEObjectFormatted((int)6, Helper.Null , fLanamab);
			}

		}
		//C 
		throw new InvalidProgramException($"The program was terminated unexpectedly.StackTrace: { Environment.StackTrace}.");//C 
		//C     End of XERFFT 
		//C 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
