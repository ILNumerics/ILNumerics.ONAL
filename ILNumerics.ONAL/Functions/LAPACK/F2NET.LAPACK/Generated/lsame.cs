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
//*> \brief \b LSAME 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       LOGICAL FUNCTION LSAME(CA,CB) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER CA,CB 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> LSAME returns .TRUE. if CA is the same letter as CB regardless of 
//*> case. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] CA 
//*> \verbatim 
//*>          CA is CHARACTER*1 
//*> \endverbatim 
//*> 
//*> \param[in] CB 
//*> \verbatim 
//*>          CB is CHARACTER*1 
//*>          CA and CB specify the single characters to be compared. 
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
//*> \ingroup aux_blas 
//* 
//*  ===================================================================== 

	 
	public static Boolean _w8y2rzgy(FString _han26hfi, FString _k9gii4ij)
	{
#region variable declarations
Boolean _w8y2rzgy = default;
Int32 _oqkroe9b =  default;
Int32 _tznsll8e =  default;
Int32 _0uy59qon =  default;
string fLanavab = default;
#endregion  variable declarations
_han26hfi = _han26hfi.Convert(1);
_k9gii4ij = _k9gii4ij.Convert(1);

	{
		//* 
		//*  -- Reference BLAS level1 routine (version 3.1) -- 
		//*  -- Reference BLAS is a software package provided by Univ. of Tennessee,    -- 
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
		//*     .. Local Scalars .. 
		//*     .. 
		//* 
		//*     Test if the characters are equal 
		//* 
		
		_w8y2rzgy = (_han26hfi == _k9gii4ij);
		if (_w8y2rzgy)
		return _w8y2rzgy;//* 
		//*     Now test for equivalence if both characters are alphabetic. 
		//* 
		
		_0uy59qon = ILNumerics.F2NET.Intrinsics.ICHAR("Z" );//* 
		//*     Use 'Z' rather than 'A' so that ASCII can be detected on Prime 
		//*     machines, on which ICHAR returns a value with bit 8 set. 
		//*     ICHAR('A') on Prime machines returns 193 which is the same as 
		//*     ICHAR('A') on an EBCDIC machine. 
		//* 
		
		_oqkroe9b = ILNumerics.F2NET.Intrinsics.ICHAR(_han26hfi );
		_tznsll8e = ILNumerics.F2NET.Intrinsics.ICHAR(_k9gii4ij );//* 
		
		if ((_0uy59qon == (int)90) | (_0uy59qon == (int)122))
		{
			//* 
			//*        ASCII is assumed - ZCODE is the ASCII code of either lower or 
			//*        upper case 'Z'. 
			//* 
			
			if ((_oqkroe9b >= (int)97) & (_oqkroe9b <= (int)122))
			_oqkroe9b = (_oqkroe9b - (int)32);
			if ((_tznsll8e >= (int)97) & (_tznsll8e <= (int)122))
			_tznsll8e = (_tznsll8e - (int)32);//* 
			
		}
		else
		if ((_0uy59qon == (int)233) | (_0uy59qon == (int)169))
		{
			//* 
			//*        EBCDIC is assumed - ZCODE is the EBCDIC code of either lower or 
			//*        upper case 'Z'. 
			//* 
			
			if ((((_oqkroe9b >= (int)129) & (_oqkroe9b <= (int)137)) | ((_oqkroe9b >= (int)145) & (_oqkroe9b <= (int)153))) | ((_oqkroe9b >= (int)162) & (_oqkroe9b <= (int)169)))
			_oqkroe9b = (_oqkroe9b + (int)64);
			if ((((_tznsll8e >= (int)129) & (_tznsll8e <= (int)137)) | ((_tznsll8e >= (int)145) & (_tznsll8e <= (int)153))) | ((_tznsll8e >= (int)162) & (_tznsll8e <= (int)169)))
			_tznsll8e = (_tznsll8e + (int)64);//* 
			
		}
		else
		if ((_0uy59qon == (int)218) | (_0uy59qon == (int)250))
		{
			//* 
			//*        ASCII is assumed, on Prime machines - ZCODE is the ASCII code 
			//*        plus 128 of either lower or upper case 'Z'. 
			//* 
			
			if ((_oqkroe9b >= (int)225) & (_oqkroe9b <= (int)250))
			_oqkroe9b = (_oqkroe9b - (int)32);
			if ((_tznsll8e >= (int)225) & (_tznsll8e <= (int)250))
			_tznsll8e = (_tznsll8e - (int)32);
		}
		
		_w8y2rzgy = (_oqkroe9b == _tznsll8e);//* 
		//*     RETURN 
		//* 
		//*     End of LSAME 
		//* 
		
	}
	
	return _w8y2rzgy;
	} // 177

} // end class 
} // end namespace
#endif
