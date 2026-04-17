
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
//*> \brief \b DLARNV returns a vector of random numbers from a uniform or normal distribution. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLARNV + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlarnv.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlarnv.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlarnv.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLARNV( IDIST, ISEED, N, X ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            IDIST, N 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            ISEED( 4 ) 
//*       DOUBLE PRECISION   X( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLARNV returns a vector of n random real numbers from a uniform or 
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
//*>          X is DOUBLE PRECISION array, dimension (N) 
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
//*>  This routine calls the auxiliary routine DLARUV to generate random 
//*>  real numbers from a uniform (0,1) distribution, in batches of up to 
//*>  128 using vectorisable code. The Box-Muller method is used to 
//*>  transform numbers from a uniform to a normal distribution. 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _dx0mpk8s(ref Int32 _d2ls1f6c, Int32* _5c1snrj6, ref Int32 _dxpq0xkr, Double* _ta7zuy9k)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)1024 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Double _kxg5drh2 =  1d;
Double _5m0mjfxm =  2d;
Int32 _smago7mr =  (int)128;
Double _o7hfri9a =  6.283185307179586d;
Int32 _b5p6od9s =  default;
Int32 _ic6kua09 =  default;
Int32 _e82ceh87 =  default;
Int32 _uicet2a7 =  default;
Double* _7u55mqkq =  (Double*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Double) * ((int)128);
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
			System.Int32 __81fgg2dlsvn2889 = (System.Int32)((int)1);
			System.Int32 __81fgg2step2889 = (System.Int32)(_smago7mr / (int)2);
			System.Int32 __81fgg2count2889;
			for (__81fgg2count2889 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2889 + __81fgg2step2889) / __81fgg2step2889)), _uicet2a7 = __81fgg2dlsvn2889; __81fgg2count2889 != 0; __81fgg2count2889--, _uicet2a7 += (__81fgg2step2889)) {

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
				//*        Call DLARUV to generate IL2 numbers from a uniform (0,1) 
				//*        distribution (IL2 <= LV) 
				//* 
				
				_bd86cfcf(_5c1snrj6 ,ref _e82ceh87 ,_7u55mqkq );//* 
				
				if (_d2ls1f6c == (int)1)
				{
					//* 
					//*           Copy generated numbers 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn2890 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2890 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2890;
						for (__81fgg2count2890 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ic6kua09) - __81fgg2dlsvn2890 + __81fgg2step2890) / __81fgg2step2890)), _b5p6od9s = __81fgg2dlsvn2890; __81fgg2count2890 != 0; __81fgg2count2890--, _b5p6od9s += (__81fgg2step2890)) {

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
						System.Int32 __81fgg2dlsvn2891 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2891 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2891;
						for (__81fgg2count2891 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ic6kua09) - __81fgg2dlsvn2891 + __81fgg2step2891) / __81fgg2step2891)), _b5p6od9s = __81fgg2dlsvn2891; __81fgg2count2891 != 0; __81fgg2count2891--, _b5p6od9s += (__81fgg2step2891)) {

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
						System.Int32 __81fgg2dlsvn2892 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2892 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2892;
						for (__81fgg2count2892 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ic6kua09) - __81fgg2dlsvn2892 + __81fgg2step2892) / __81fgg2step2892)), _b5p6od9s = __81fgg2dlsvn2892; __81fgg2count2892 != 0; __81fgg2count2892--, _b5p6od9s += (__81fgg2step2892)) {

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
		//*     End of DLARNV 
		//* 
		
	}
	
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
