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
//*> \brief \b SLARNV returns a vector of random numbers from a uniform or normal distribution. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLARNV + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slarnv.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slarnv.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slarnv.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLARNV( IDIST, ISEED, N, X ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            IDIST, N 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            ISEED( 4 ) 
//*       REAL               X( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLARNV returns a vector of n random real numbers from a uniform or 
//*> normal distribution. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] IDIST 
//*> \verbatim 
//*>          IDIST is INTEGER 
//*>          Specifies the distribution of the random numbers: 
//*>          = 1:  uniform (0,1) 
//*>          = 2:  uniform (-1,1) 
//*>          = 3:  normal (0,1) 
//*> \endverbatim 
//*> 
//*> \param[in,out] ISEED 
//*> \verbatim 
//*>          ISEED is INTEGER array, dimension (4) 
//*>          On entry, the seed of the random number generator; the array 
//*>          elements must be between 0 and 4095, and ISEED(4) must be 
//*>          odd. 
//*>          On exit, the seed is updated. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of random numbers to be generated. 
//*> \endverbatim 
//*> 
//*> \param[out] X 
//*> \verbatim 
//*>          X is REAL array, dimension (N) 
//*>          The generated random numbers. 
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
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  This routine calls the auxiliary routine SLARUV to generate random 
//*>  real numbers from a uniform (0,1) distribution, in batches of up to 
//*>  128 using vectorisable code. The Box-Muller method is used to 
//*>  transform numbers from a uniform to a normal distribution. 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _rcijgesd(ref Int32 _d2ls1f6c, Int32* _5c1snrj6, ref Int32 _dxpq0xkr, Single* _ta7zuy9k)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)512 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Single _kxg5drh2 =  1f;
Single _5m0mjfxm =  2f;
Int32 _smago7mr =  (int)128;
Single _o7hfri9a =  6.2831855f;
Int32 _b5p6od9s =  default;
Int32 _ic6kua09 =  default;
Int32 _e82ceh87 =  default;
Int32 _uicet2a7 =  default;
Single* _7u55mqkq =  (Single*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Single) * ((int)128);
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
		//*     .. Array Arguments .. 
		//*     .. 
		//* 
		//*  ===================================================================== 
		//* 
		//*     .. Parameters .. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Local Arrays .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn3224 = (System.Int32)((int)1);
			System.Int32 __81fgg2step3224 = (System.Int32)(_smago7mr / (int)2);
			System.Int32 __81fgg2count3224;
			for (__81fgg2count3224 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3224 + __81fgg2step3224) / __81fgg2step3224)), _uicet2a7 = __81fgg2dlsvn3224; __81fgg2count3224 != 0; __81fgg2count3224--, _uicet2a7 += (__81fgg2step3224)) {

			{
				
				_ic6kua09 = ILNumerics.F2NET.Intrinsics.MIN(_smago7mr / (int)2 ,(_dxpq0xkr - _uicet2a7) + (int)1 );
				if (_d2ls1f6c == (int)3)
				{
					
					_e82ceh87 = ((int)2 * _ic6kua09);
				}
				else
				{
					
					_e82ceh87 = _ic6kua09;
				}
				//* 
				//*        Call SLARUV to generate IL2 numbers from a uniform (0,1) 
				//*        distribution (IL2 <= LV) 
				//* 
				
				_gxybsoeh(_5c1snrj6 ,ref _e82ceh87 ,_7u55mqkq );//* 
				
				if (_d2ls1f6c == (int)1)
				{
					//* 
					//*           Copy generated numbers 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn3225 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3225 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3225;
						for (__81fgg2count3225 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ic6kua09) - __81fgg2dlsvn3225 + __81fgg2step3225) / __81fgg2step3225)), _b5p6od9s = __81fgg2dlsvn3225; __81fgg2count3225 != 0; __81fgg2count3225--, _b5p6od9s += (__81fgg2step3225)) {

						{
							
							*(_ta7zuy9k+((_uicet2a7 + _b5p6od9s) - (int)1 - 1)) = *(_7u55mqkq+(_b5p6od9s - 1));
Mark10:;
							// continue
						}
												}					}
				}
				else
				if (_d2ls1f6c == (int)2)
				{
					//* 
					//*           Convert generated numbers to uniform (-1,1) distribution 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn3226 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3226 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3226;
						for (__81fgg2count3226 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ic6kua09) - __81fgg2dlsvn3226 + __81fgg2step3226) / __81fgg2step3226)), _b5p6od9s = __81fgg2dlsvn3226; __81fgg2count3226 != 0; __81fgg2count3226--, _b5p6od9s += (__81fgg2step3226)) {

						{
							
							*(_ta7zuy9k+((_uicet2a7 + _b5p6od9s) - (int)1 - 1)) = ((_5m0mjfxm * *(_7u55mqkq+(_b5p6od9s - 1))) - _kxg5drh2);
Mark20:;
							// continue
						}
												}					}
				}
				else
				if (_d2ls1f6c == (int)3)
				{
					//* 
					//*           Convert generated numbers to normal (0,1) distribution 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn3227 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3227 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3227;
						for (__81fgg2count3227 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ic6kua09) - __81fgg2dlsvn3227 + __81fgg2step3227) / __81fgg2step3227)), _b5p6od9s = __81fgg2dlsvn3227; __81fgg2count3227 != 0; __81fgg2count3227--, _b5p6od9s += (__81fgg2step3227)) {

						{
							
							*(_ta7zuy9k+((_uicet2a7 + _b5p6od9s) - (int)1 - 1)) = (ILNumerics.F2NET.Intrinsics.SQRT(-((_5m0mjfxm * ILNumerics.F2NET.Intrinsics.LOG(*(_7u55mqkq+(((int)2 * _b5p6od9s) - (int)1 - 1)) ))) ) * ILNumerics.F2NET.Intrinsics.COS(_o7hfri9a * *(_7u55mqkq+((int)2 * _b5p6od9s - 1)) ));
Mark30:;
							// continue
						}
												}					}
				}
				
Mark40:;
				// continue
			}
						}		}
		return;//* 
		//*     End of SLARNV 
		//* 
		
	}
	
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
