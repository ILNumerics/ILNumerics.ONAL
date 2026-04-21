// MIT License
// 
// Copyright (c) 2026 ILNumerics GmbH
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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
public static unsafe partial class LAPACK {
//*> \brief \b XERBLA 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download XERBLA + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/xerbla.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/xerbla.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/xerbla.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE XERBLA( SRNAME, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER*(*)      SRNAME 
//*       INTEGER            INFO 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> XERBLA  is an error handler for the LAPACK routines. 
//*> It is called by an LAPACK routine if an input parameter has an 
//*> invalid value.  A message is printed and execution stops. 
//*> 
//*> Installers may consider modifying the STOP statement in order to 
//*> call system-specific exception-handling facilities. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] SRNAME 
//*> \verbatim 
//*>          SRNAME is CHARACTER*(*) 
//*>          The name of the routine which called XERBLA. 
//*> \endverbatim 
//*> 
//*> \param[in] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          The position of the invalid parameter in the parameter list 
//*>          of the calling routine. 
//*> \endverbatim 
//* 
//*  Authors: 
//*  ======== 
//* 
//*> \author Univ. of Tennessee 
//*> \author Univ. of California Berkeley 
//*> \author Univ. of Colorado Denver 
//*> \author NAG Ltd. 
//* 
//*> \date December 2016 
//* 
//*> \ingroup OTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _ut9qalzx(FString _7gf7cve5, ref Int32 _gro5yvfo)
	{
#region variable declarations
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     December 2016 
		//* 
		//*     .. Scalar Arguments .. 
		//*     .. 
		//* 
		//* ===================================================================== 
		//* 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		{

			fLanavab = __xerbla.fLananab[9999];
			var fLanamab = FormatParser.Parse(fLanavab).GetEnumerator();
			Helper.WRITEObjectFormatted((int)6, _7gf7cve5[(int)1,ILNumerics.F2NET.Intrinsics.LEN_TRIM(_7gf7cve5 )], fLanamab);
			Helper.WRITEObjectFormatted((int)6, _gro5yvfo, fLanamab);
			Helper.WRITEObjectFormatted((int)6, Helper.Null , fLanamab);
		}
//* 
		throw new InvalidProgramException($"The program was terminated unexpectedly.StackTrace: { Environment.StackTrace}.");//* 
		
Mark9999:;
		//* 
		//*     End of XERBLA 
		//* 
		
	}
	
	} // 177


internal unsafe class __xerbla { 

	internal static System.Collections.Generic.Dictionary<int,string> fLananab = new System.Collections.Generic.Dictionary<int,string>() {

		{ 9999, "(' ** On entry to ',1A,' parameter number ',1I2,' had ','an illegal value')" },

	};
}

} // end class 
} // end namespace
#endif
