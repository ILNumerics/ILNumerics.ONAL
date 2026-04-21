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
//*> \brief \b DLANGE returns the value of the 1-norm, Frobenius norm, infinity-norm, or the largest absolute value of any element of a general rectangular matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLANGE + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlange.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlange.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlange.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       DOUBLE PRECISION FUNCTION DLANGE( NORM, M, N, A, LDA, WORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          NORM 
//*       INTEGER            LDA, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   A( LDA, * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLANGE  returns the value of the one norm,  or the Frobenius norm, or 
//*> the  infinity norm,  or the  element of  largest absolute value  of a 
//*> real matrix A. 
//*> \endverbatim 
//*> 
//*> \return DLANGE 
//*> \verbatim 
//*> 
//*>    DLANGE = ( max(abs(A(i,j))), NORM = 'M' or 'm' 
//*>             ( 
//*>             ( norm1(A),         NORM = '1', 'O' or 'o' 
//*>             ( 
//*>             ( normI(A),         NORM = 'I' or 'i' 
//*>             ( 
//*>             ( normF(A),         NORM = 'F', 'f', 'E' or 'e' 
//*> 
//*> where  norm1  denotes the  one norm of a matrix (maximum column sum), 
//*> normI  denotes the  infinity norm  of a matrix  (maximum row sum) and 
//*> normF  denotes the  Frobenius norm of a matrix (square root of sum of 
//*> squares).  Note that  max(abs(A(i,j)))  is not a consistent matrix norm. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] NORM 
//*> \verbatim 
//*>          NORM is CHARACTER*1 
//*>          Specifies the value to be returned in DLANGE as described 
//*>          above. 
//*> \endverbatim 
//*> 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of rows of the matrix A.  M >= 0.  When M = 0, 
//*>          DLANGE is set to zero. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of columns of the matrix A.  N >= 0.  When N = 0, 
//*>          DLANGE is set to zero. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is DOUBLE PRECISION array, dimension (LDA,N) 
//*>          The m by n matrix A. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(M,1). 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is DOUBLE PRECISION array, dimension (MAX(1,LWORK)), 
//*>          where LWORK >= M when NORM = 'I'; otherwise, WORK is not 
//*>          referenced. 
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
//*> \ingroup doubleGEauxiliary 
//* 
//*  ===================================================================== 

	 
	public static Double _oui78ayq(FString _gq71rsgu, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, Double* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _apig8meb)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)32 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Double _oui78ayq = default;
Double _kxg5drh2 =  1d;
Double _d0547bi2 =  0d;
Int32 _b5p6od9s =  default;
Int32 _znpjgsef =  default;
Double _6j9l5fwy =  default;
Double _lwoxlbje =  default;
Double _1ajfmh55 =  default;
Double* _8l4yph2p =  (Double*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Double) * ((int)2);
Double* _70n56i0m =  (Double*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Double) * ((int)2);
string fLanavab = default;
#endregion  variable declarations
_gq71rsgu = _gq71rsgu.Convert(1);

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
		//* ===================================================================== 
		//* 
		//*     .. Parameters .. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Local Arrays .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		if (ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_dxpq0xkr ) == (int)0)
		{
			
			_lwoxlbje = _d0547bi2;
		}
		else
		if (_w8y2rzgy(_gq71rsgu ,"M" ))
		{
			//* 
			//*        Find max(abs(A(i,j))). 
			//* 
			
			_lwoxlbje = _d0547bi2;
			{
				System.Int32 __81fgg2dlsvn528 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step528 = (System.Int32)((int)1);
				System.Int32 __81fgg2count528;
				for (__81fgg2count528 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn528 + __81fgg2step528) / __81fgg2step528)), _znpjgsef = __81fgg2dlsvn528; __81fgg2count528 != 0; __81fgg2count528--, _znpjgsef += (__81fgg2step528)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn529 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step529 = (System.Int32)((int)1);
						System.Int32 __81fgg2count529;
						for (__81fgg2count529 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn529 + __81fgg2step529) / __81fgg2step529)), _b5p6od9s = __81fgg2dlsvn529; __81fgg2count529 != 0; __81fgg2count529--, _b5p6od9s += (__81fgg2step529)) {

						{
							
							_1ajfmh55 = ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) );
							if ((_lwoxlbje < _1ajfmh55) | _fk98jwhi(ref _1ajfmh55 ))
							_lwoxlbje = _1ajfmh55;
Mark10:;
							// continue
						}
												}					}
Mark20:;
					// continue
				}
								}			}
		}
		else
		if ((_w8y2rzgy(_gq71rsgu ,"O" )) | (_gq71rsgu == "1"))
		{
			//* 
			//*        Find norm1(A). 
			//* 
			
			_lwoxlbje = _d0547bi2;
			{
				System.Int32 __81fgg2dlsvn530 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step530 = (System.Int32)((int)1);
				System.Int32 __81fgg2count530;
				for (__81fgg2count530 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn530 + __81fgg2step530) / __81fgg2step530)), _znpjgsef = __81fgg2dlsvn530; __81fgg2count530 != 0; __81fgg2count530--, _znpjgsef += (__81fgg2step530)) {

				{
					
					_6j9l5fwy = _d0547bi2;
					{
						System.Int32 __81fgg2dlsvn531 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step531 = (System.Int32)((int)1);
						System.Int32 __81fgg2count531;
						for (__81fgg2count531 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn531 + __81fgg2step531) / __81fgg2step531)), _b5p6od9s = __81fgg2dlsvn531; __81fgg2count531 != 0; __81fgg2count531--, _b5p6od9s += (__81fgg2step531)) {

						{
							
							_6j9l5fwy = (_6j9l5fwy + ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
Mark30:;
							// continue
						}
												}					}
					if ((_lwoxlbje < _6j9l5fwy) | _fk98jwhi(ref _6j9l5fwy ))
					_lwoxlbje = _6j9l5fwy;
Mark40:;
					// continue
				}
								}			}
		}
		else
		if (_w8y2rzgy(_gq71rsgu ,"I" ))
		{
			//* 
			//*        Find normI(A). 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn532 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step532 = (System.Int32)((int)1);
				System.Int32 __81fgg2count532;
				for (__81fgg2count532 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn532 + __81fgg2step532) / __81fgg2step532)), _b5p6od9s = __81fgg2dlsvn532; __81fgg2count532 != 0; __81fgg2count532--, _b5p6od9s += (__81fgg2step532)) {

				{
					
					*(_apig8meb+(_b5p6od9s - 1)) = _d0547bi2;
Mark50:;
					// continue
				}
								}			}
			{
				System.Int32 __81fgg2dlsvn533 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step533 = (System.Int32)((int)1);
				System.Int32 __81fgg2count533;
				for (__81fgg2count533 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn533 + __81fgg2step533) / __81fgg2step533)), _znpjgsef = __81fgg2dlsvn533; __81fgg2count533 != 0; __81fgg2count533--, _znpjgsef += (__81fgg2step533)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn534 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step534 = (System.Int32)((int)1);
						System.Int32 __81fgg2count534;
						for (__81fgg2count534 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn534 + __81fgg2step534) / __81fgg2step534)), _b5p6od9s = __81fgg2dlsvn534; __81fgg2count534 != 0; __81fgg2count534--, _b5p6od9s += (__81fgg2step534)) {

						{
							
							*(_apig8meb+(_b5p6od9s - 1)) = (*(_apig8meb+(_b5p6od9s - 1)) + ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
Mark60:;
							// continue
						}
												}					}
Mark70:;
					// continue
				}
								}			}
			_lwoxlbje = _d0547bi2;
			{
				System.Int32 __81fgg2dlsvn535 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step535 = (System.Int32)((int)1);
				System.Int32 __81fgg2count535;
				for (__81fgg2count535 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn535 + __81fgg2step535) / __81fgg2step535)), _b5p6od9s = __81fgg2dlsvn535; __81fgg2count535 != 0; __81fgg2count535--, _b5p6od9s += (__81fgg2step535)) {

				{
					
					_1ajfmh55 = *(_apig8meb+(_b5p6od9s - 1));
					if ((_lwoxlbje < _1ajfmh55) | _fk98jwhi(ref _1ajfmh55 ))
					_lwoxlbje = _1ajfmh55;
Mark80:;
					// continue
				}
								}			}
		}
		else
		if ((_w8y2rzgy(_gq71rsgu ,"F" )) | (_w8y2rzgy(_gq71rsgu ,"E" )))
		{
			//* 
			//*        Find normF(A). 
			//*        SSQ(1) is scale 
			//*        SSQ(2) is sum-of-squares 
			//*        For better accuracy, sum each column separately. 
			//* 
			
			*(_8l4yph2p+((int)1 - 1)) = _d0547bi2;
			*(_8l4yph2p+((int)2 - 1)) = _kxg5drh2;
			{
				System.Int32 __81fgg2dlsvn536 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step536 = (System.Int32)((int)1);
				System.Int32 __81fgg2count536;
				for (__81fgg2count536 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn536 + __81fgg2step536) / __81fgg2step536)), _znpjgsef = __81fgg2dlsvn536; __81fgg2count536 != 0; __81fgg2count536--, _znpjgsef += (__81fgg2step536)) {

				{
					
					*(_70n56i0m+((int)1 - 1)) = _d0547bi2;
					*(_70n56i0m+((int)2 - 1)) = _kxg5drh2;
					_g54gbghr(ref _ev4xhht5 ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_70n56i0m+((int)1 - 1))) ,ref Unsafe.AsRef(*(_70n56i0m+((int)2 - 1))) );
					_2ah5b9jc(_8l4yph2p ,_70n56i0m );
Mark90:;
					// continue
				}
								}			}
			_lwoxlbje = (*(_8l4yph2p+((int)1 - 1)) * ILNumerics.F2NET.Intrinsics.SQRT(*(_8l4yph2p+((int)2 - 1)) ));
		}
		//* 
		
		_oui78ayq = _lwoxlbje;
		return _oui78ayq;//* 
		//*     End of DLANGE 
		//* 
		
	}
	
	return _oui78ayq;
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
