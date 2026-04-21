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
//*> \brief \b DLASRT sorts numbers in increasing or decreasing order. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLASRT + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlasrt.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlasrt.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlasrt.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLASRT( ID, N, D, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          ID 
//*       INTEGER            INFO, N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   D( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> Sort the numbers in D in increasing order (if ID = 'I') or 
//*> in decreasing order (if ID = 'D' ). 
//*> 
//*> Use Quick Sort, reverting to Insertion sort on arrays of 
//*> size <= 20. Dimension of STACK limits N to about 2**32. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] ID 
//*> \verbatim 
//*>          ID is CHARACTER*1 
//*>          = 'I': sort D in increasing order; 
//*>          = 'D': sort D in decreasing order. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The length of the array D. 
//*> \endverbatim 
//*> 
//*> \param[in,out] D 
//*> \verbatim 
//*>          D is DOUBLE PRECISION array, dimension (N) 
//*>          On entry, the array to be sorted. 
//*>          On exit, D has been sorted into increasing order 
//*>          (D(1) <= ... <= D(N) ) or into decreasing order 
//*>          (D(1) >= ... >= D(N) ), depending on ID. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value 
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
//*> \date June 2016 
//* 
//*> \ingroup auxOTHERcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _agod5jth(FString _iffl9738, ref Int32 _dxpq0xkr, Double* _plfm7z8g, ref Int32 _gro5yvfo)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)256 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Int32 _2vi7x6ig =  (int)20;
Int32 _mk5h43sm =  default;
Int32 _6vfy7wef =  default;
Int32 _b5p6od9s =  default;
Int32 _znpjgsef =  default;
Int32 _cusbn3d9 =  default;
Int32 _canqyzuo =  default;
Double _5v2i3gjn =  default;
Double _vt4856ip =  default;
Double _w2on1fbf =  default;
Double _qc4ws2rx =  default;
Double _2qcyvkhx =  default;
Int32* _zrmjbjtz =  (Int32*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Int32) * ((int)2)*((int)32);
string fLanavab = default;
#endregion  variable declarations
_iffl9738 = _iffl9738.Convert(1);

	{
		//* 
		//*  -- LAPACK computational routine (version 3.7.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     June 2016 
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
		//*     .. External Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input parameters. 
		//* 
		
		_gro5yvfo = (int)0;
		_mk5h43sm = (int)-1;
		if (_w8y2rzgy(_iffl9738 ,"D" ))
		{
			
			_mk5h43sm = (int)0;
		}
		else
		if (_w8y2rzgy(_iffl9738 ,"I" ))
		{
			
			_mk5h43sm = (int)1;
		}
		
		if (_mk5h43sm == (int)-1)
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-2;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("DLASRT" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr <= (int)1)
		return;//* 
		
		_canqyzuo = (int)1;
		*(_zrmjbjtz+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2)) = (int)1;
		*(_zrmjbjtz+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2)) = _dxpq0xkr;
Mark10:;
		// continue
		_cusbn3d9 = *(_zrmjbjtz+((int)1 - 1) + (_canqyzuo - 1) * 1 * ((int)2));
		_6vfy7wef = *(_zrmjbjtz+((int)2 - 1) + (_canqyzuo - 1) * 1 * ((int)2));
		_canqyzuo = (_canqyzuo - (int)1);
		if (((_6vfy7wef - _cusbn3d9) <= _2vi7x6ig) & ((_6vfy7wef - _cusbn3d9) > (int)0))
		{
			//* 
			//*        Do Insertion sort on D( START:ENDD ) 
			//* 
			
			if (_mk5h43sm == (int)0)
			{
				//* 
				//*           Sort into decreasing order 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn326 = (System.Int32)((_cusbn3d9 + (int)1));
					const System.Int32 __81fgg2step326 = (System.Int32)((int)1);
					System.Int32 __81fgg2count326;
					for (__81fgg2count326 = System.Math.Max(0, (System.Int32)(((System.Int32)(_6vfy7wef) - __81fgg2dlsvn326 + __81fgg2step326) / __81fgg2step326)), _b5p6od9s = __81fgg2dlsvn326; __81fgg2count326 != 0; __81fgg2count326--, _b5p6od9s += (__81fgg2step326)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn327 = (System.Int32)(_b5p6od9s);
							System.Int32 __81fgg2step327 = (System.Int32)((int)-1);
							System.Int32 __81fgg2count327;
							for (__81fgg2count327 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cusbn3d9 + (int)1) - __81fgg2dlsvn327 + __81fgg2step327) / __81fgg2step327)), _znpjgsef = __81fgg2dlsvn327; __81fgg2count327 != 0; __81fgg2count327--, _znpjgsef += (__81fgg2step327)) {

							{
								
								if (*(_plfm7z8g+(_znpjgsef - 1)) > *(_plfm7z8g+(_znpjgsef - (int)1 - 1)))
								{
									
									_qc4ws2rx = *(_plfm7z8g+(_znpjgsef - 1));
									*(_plfm7z8g+(_znpjgsef - 1)) = *(_plfm7z8g+(_znpjgsef - (int)1 - 1));
									*(_plfm7z8g+(_znpjgsef - (int)1 - 1)) = _qc4ws2rx;
								}
								else
								{
									goto Mark30;
								}
								
Mark20:;
								// continue
							}
														}						}
Mark30:;
						// continue
					}
										}				}//* 
				
			}
			else
			{
				//* 
				//*           Sort into increasing order 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn328 = (System.Int32)((_cusbn3d9 + (int)1));
					const System.Int32 __81fgg2step328 = (System.Int32)((int)1);
					System.Int32 __81fgg2count328;
					for (__81fgg2count328 = System.Math.Max(0, (System.Int32)(((System.Int32)(_6vfy7wef) - __81fgg2dlsvn328 + __81fgg2step328) / __81fgg2step328)), _b5p6od9s = __81fgg2dlsvn328; __81fgg2count328 != 0; __81fgg2count328--, _b5p6od9s += (__81fgg2step328)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn329 = (System.Int32)(_b5p6od9s);
							System.Int32 __81fgg2step329 = (System.Int32)((int)-1);
							System.Int32 __81fgg2count329;
							for (__81fgg2count329 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cusbn3d9 + (int)1) - __81fgg2dlsvn329 + __81fgg2step329) / __81fgg2step329)), _znpjgsef = __81fgg2dlsvn329; __81fgg2count329 != 0; __81fgg2count329--, _znpjgsef += (__81fgg2step329)) {

							{
								
								if (*(_plfm7z8g+(_znpjgsef - 1)) < *(_plfm7z8g+(_znpjgsef - (int)1 - 1)))
								{
									
									_qc4ws2rx = *(_plfm7z8g+(_znpjgsef - 1));
									*(_plfm7z8g+(_znpjgsef - 1)) = *(_plfm7z8g+(_znpjgsef - (int)1 - 1));
									*(_plfm7z8g+(_znpjgsef - (int)1 - 1)) = _qc4ws2rx;
								}
								else
								{
									goto Mark50;
								}
								
Mark40:;
								// continue
							}
														}						}
Mark50:;
						// continue
					}
										}				}//* 
				
			}
			//* 
			
		}
		else
		if ((_6vfy7wef - _cusbn3d9) > _2vi7x6ig)
		{
			//* 
			//*        Partition D( START:ENDD ) and stack parts, largest one first 
			//* 
			//*        Choose partition entry as median of 3 
			//* 
			
			_5v2i3gjn = *(_plfm7z8g+(_cusbn3d9 - 1));
			_vt4856ip = *(_plfm7z8g+(_6vfy7wef - 1));
			_b5p6od9s = ((_cusbn3d9 + _6vfy7wef) / (int)2);
			_w2on1fbf = *(_plfm7z8g+(_b5p6od9s - 1));
			if (_5v2i3gjn < _vt4856ip)
			{
				
				if (_w2on1fbf < _5v2i3gjn)
				{
					
					_qc4ws2rx = _5v2i3gjn;
				}
				else
				if (_w2on1fbf < _vt4856ip)
				{
					
					_qc4ws2rx = _w2on1fbf;
				}
				else
				{
					
					_qc4ws2rx = _vt4856ip;
				}
				
			}
			else
			{
				
				if (_w2on1fbf < _vt4856ip)
				{
					
					_qc4ws2rx = _vt4856ip;
				}
				else
				if (_w2on1fbf < _5v2i3gjn)
				{
					
					_qc4ws2rx = _w2on1fbf;
				}
				else
				{
					
					_qc4ws2rx = _5v2i3gjn;
				}
				
			}
			//* 
			
			if (_mk5h43sm == (int)0)
			{
				//* 
				//*           Sort into decreasing order 
				//* 
				
				_b5p6od9s = (_cusbn3d9 - (int)1);
				_znpjgsef = (_6vfy7wef + (int)1);
Mark60:;
				// continue
Mark70:;
				// continue
				_znpjgsef = (_znpjgsef - (int)1);
				if (*(_plfm7z8g+(_znpjgsef - 1)) < _qc4ws2rx)goto Mark70;
Mark80:;
				// continue
				_b5p6od9s = (_b5p6od9s + (int)1);
				if (*(_plfm7z8g+(_b5p6od9s - 1)) > _qc4ws2rx)goto Mark80;
				if (_b5p6od9s < _znpjgsef)
				{
					
					_2qcyvkhx = *(_plfm7z8g+(_b5p6od9s - 1));
					*(_plfm7z8g+(_b5p6od9s - 1)) = *(_plfm7z8g+(_znpjgsef - 1));
					*(_plfm7z8g+(_znpjgsef - 1)) = _2qcyvkhx;goto Mark60;
				}
				
				if ((_znpjgsef - _cusbn3d9) > ((_6vfy7wef - _znpjgsef) - (int)1))
				{
					
					_canqyzuo = (_canqyzuo + (int)1);
					*(_zrmjbjtz+((int)1 - 1) + (_canqyzuo - 1) * 1 * ((int)2)) = _cusbn3d9;
					*(_zrmjbjtz+((int)2 - 1) + (_canqyzuo - 1) * 1 * ((int)2)) = _znpjgsef;
					_canqyzuo = (_canqyzuo + (int)1);
					*(_zrmjbjtz+((int)1 - 1) + (_canqyzuo - 1) * 1 * ((int)2)) = (_znpjgsef + (int)1);
					*(_zrmjbjtz+((int)2 - 1) + (_canqyzuo - 1) * 1 * ((int)2)) = _6vfy7wef;
				}
				else
				{
					
					_canqyzuo = (_canqyzuo + (int)1);
					*(_zrmjbjtz+((int)1 - 1) + (_canqyzuo - 1) * 1 * ((int)2)) = (_znpjgsef + (int)1);
					*(_zrmjbjtz+((int)2 - 1) + (_canqyzuo - 1) * 1 * ((int)2)) = _6vfy7wef;
					_canqyzuo = (_canqyzuo + (int)1);
					*(_zrmjbjtz+((int)1 - 1) + (_canqyzuo - 1) * 1 * ((int)2)) = _cusbn3d9;
					*(_zrmjbjtz+((int)2 - 1) + (_canqyzuo - 1) * 1 * ((int)2)) = _znpjgsef;
				}
				
			}
			else
			{
				//* 
				//*           Sort into increasing order 
				//* 
				
				_b5p6od9s = (_cusbn3d9 - (int)1);
				_znpjgsef = (_6vfy7wef + (int)1);
Mark90:;
				// continue
Mark100:;
				// continue
				_znpjgsef = (_znpjgsef - (int)1);
				if (*(_plfm7z8g+(_znpjgsef - 1)) > _qc4ws2rx)goto Mark100;
Mark110:;
				// continue
				_b5p6od9s = (_b5p6od9s + (int)1);
				if (*(_plfm7z8g+(_b5p6od9s - 1)) < _qc4ws2rx)goto Mark110;
				if (_b5p6od9s < _znpjgsef)
				{
					
					_2qcyvkhx = *(_plfm7z8g+(_b5p6od9s - 1));
					*(_plfm7z8g+(_b5p6od9s - 1)) = *(_plfm7z8g+(_znpjgsef - 1));
					*(_plfm7z8g+(_znpjgsef - 1)) = _2qcyvkhx;goto Mark90;
				}
				
				if ((_znpjgsef - _cusbn3d9) > ((_6vfy7wef - _znpjgsef) - (int)1))
				{
					
					_canqyzuo = (_canqyzuo + (int)1);
					*(_zrmjbjtz+((int)1 - 1) + (_canqyzuo - 1) * 1 * ((int)2)) = _cusbn3d9;
					*(_zrmjbjtz+((int)2 - 1) + (_canqyzuo - 1) * 1 * ((int)2)) = _znpjgsef;
					_canqyzuo = (_canqyzuo + (int)1);
					*(_zrmjbjtz+((int)1 - 1) + (_canqyzuo - 1) * 1 * ((int)2)) = (_znpjgsef + (int)1);
					*(_zrmjbjtz+((int)2 - 1) + (_canqyzuo - 1) * 1 * ((int)2)) = _6vfy7wef;
				}
				else
				{
					
					_canqyzuo = (_canqyzuo + (int)1);
					*(_zrmjbjtz+((int)1 - 1) + (_canqyzuo - 1) * 1 * ((int)2)) = (_znpjgsef + (int)1);
					*(_zrmjbjtz+((int)2 - 1) + (_canqyzuo - 1) * 1 * ((int)2)) = _6vfy7wef;
					_canqyzuo = (_canqyzuo + (int)1);
					*(_zrmjbjtz+((int)1 - 1) + (_canqyzuo - 1) * 1 * ((int)2)) = _cusbn3d9;
					*(_zrmjbjtz+((int)2 - 1) + (_canqyzuo - 1) * 1 * ((int)2)) = _znpjgsef;
				}
				
			}
			
		}
		
		if (_canqyzuo > (int)0)goto Mark10;
		return;//* 
		//*     End of DLASRT 
		//* 
		
	}
	
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
