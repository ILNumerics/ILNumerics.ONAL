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
//*> \brief \b SLANSY returns the value of the 1-norm, or the Frobenius norm, or the infinity norm, or the element of largest absolute value of a real symmetric matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLANSY + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slansy.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slansy.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slansy.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       REAL             FUNCTION SLANSY( NORM, UPLO, N, A, LDA, WORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          NORM, UPLO 
//*       INTEGER            LDA, N 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               A( LDA, * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLANSY  returns the value of the one norm,  or the Frobenius norm, or 
//*> the  infinity norm,  or the  element of  largest absolute value  of a 
//*> real symmetric matrix A. 
//*> \endverbatim 
//*> 
//*> \return SLANSY 
//*> \verbatim 
//*> 
//*>    SLANSY = ( max(abs(A(i,j))), NORM = 'M' or 'm' 
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
//*>          Specifies the value to be returned in SLANSY as described 
//*>          above. 
//*> \endverbatim 
//*> 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>          Specifies whether the upper or lower triangular part of the 
//*>          symmetric matrix A is to be referenced. 
//*>          = 'U':  Upper triangular part of A is referenced 
//*>          = 'L':  Lower triangular part of A is referenced 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix A.  N >= 0.  When N = 0, SLANSY is 
//*>          set to zero. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is REAL array, dimension (LDA,N) 
//*>          The symmetric matrix A.  If UPLO = 'U', the leading n by n 
//*>          upper triangular part of A contains the upper triangular part 
//*>          of the matrix A, and the strictly lower triangular part of A 
//*>          is not referenced.  If UPLO = 'L', the leading n by n lower 
//*>          triangular part of A contains the lower triangular part of 
//*>          the matrix A, and the strictly upper triangular part of A is 
//*>          not referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(N,1). 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is REAL array, dimension (MAX(1,LWORK)), 
//*>          where LWORK >= N when NORM = 'I' or '1' or 'O'; otherwise, 
//*>          WORK is not referenced. 
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
//*> \ingroup realSYauxiliary 
//* 
//*  ===================================================================== 

	 
	public static Single _494ans48(FString _gq71rsgu, FString _9wyre9zc, ref Int32 _dxpq0xkr, Single* _vxfgpup9, ref Int32 _ocv8fk5c, Single* _apig8meb)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)16 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Single _494ans48 = default;
Single _kxg5drh2 =  1f;
Single _d0547bi2 =  0f;
Int32 _b5p6od9s =  default;
Int32 _znpjgsef =  default;
Single _8lucc4fb =  default;
Single _6j9l5fwy =  default;
Single _lwoxlbje =  default;
Single* _8l4yph2p =  (Single*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Single) * ((int)2);
Single* _70n56i0m =  (Single*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Single) * ((int)2);
string fLanavab = default;
#endregion  variable declarations
_gq71rsgu = _gq71rsgu.Convert(1);
_9wyre9zc = _9wyre9zc.Convert(1);

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
		//*     .. External Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		if (_dxpq0xkr == (int)0)
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
			if (_w8y2rzgy(_9wyre9zc ,"U" ))
			{
				
				{
					System.Int32 __81fgg2dlsvn3155 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3155 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3155;
					for (__81fgg2count3155 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3155 + __81fgg2step3155) / __81fgg2step3155)), _znpjgsef = __81fgg2dlsvn3155; __81fgg2count3155 != 0; __81fgg2count3155--, _znpjgsef += (__81fgg2step3155)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn3156 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3156 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3156;
							for (__81fgg2count3156 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn3156 + __81fgg2step3156) / __81fgg2step3156)), _b5p6od9s = __81fgg2dlsvn3156; __81fgg2count3156 != 0; __81fgg2count3156--, _b5p6od9s += (__81fgg2step3156)) {

							{
								
								_6j9l5fwy = ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) );
								if ((_lwoxlbje < _6j9l5fwy) | _lilv8egi(ref _6j9l5fwy ))
								_lwoxlbje = _6j9l5fwy;
Mark10:;
								// continue
							}
														}						}
Mark20:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn3157 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3157 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3157;
					for (__81fgg2count3157 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3157 + __81fgg2step3157) / __81fgg2step3157)), _znpjgsef = __81fgg2dlsvn3157; __81fgg2count3157 != 0; __81fgg2count3157--, _znpjgsef += (__81fgg2step3157)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn3158 = (System.Int32)(_znpjgsef);
							const System.Int32 __81fgg2step3158 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3158;
							for (__81fgg2count3158 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3158 + __81fgg2step3158) / __81fgg2step3158)), _b5p6od9s = __81fgg2dlsvn3158; __81fgg2count3158 != 0; __81fgg2count3158--, _b5p6od9s += (__81fgg2step3158)) {

							{
								
								_6j9l5fwy = ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) );
								if ((_lwoxlbje < _6j9l5fwy) | _lilv8egi(ref _6j9l5fwy ))
								_lwoxlbje = _6j9l5fwy;
Mark30:;
								// continue
							}
														}						}
Mark40:;
						// continue
					}
										}				}
			}
			
		}
		else
		if (((_w8y2rzgy(_gq71rsgu ,"I" )) | (_w8y2rzgy(_gq71rsgu ,"O" ))) | (_gq71rsgu == "1"))
		{
			//* 
			//*        Find normI(A) ( = norm1(A), since A is symmetric). 
			//* 
			
			_lwoxlbje = _d0547bi2;
			if (_w8y2rzgy(_9wyre9zc ,"U" ))
			{
				
				{
					System.Int32 __81fgg2dlsvn3159 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3159 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3159;
					for (__81fgg2count3159 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3159 + __81fgg2step3159) / __81fgg2step3159)), _znpjgsef = __81fgg2dlsvn3159; __81fgg2count3159 != 0; __81fgg2count3159--, _znpjgsef += (__81fgg2step3159)) {

					{
						
						_6j9l5fwy = _d0547bi2;
						{
							System.Int32 __81fgg2dlsvn3160 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3160 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3160;
							for (__81fgg2count3160 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn3160 + __81fgg2step3160) / __81fgg2step3160)), _b5p6od9s = __81fgg2dlsvn3160; __81fgg2count3160 != 0; __81fgg2count3160--, _b5p6od9s += (__81fgg2step3160)) {

							{
								
								_8lucc4fb = ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) );
								_6j9l5fwy = (_6j9l5fwy + _8lucc4fb);
								*(_apig8meb+(_b5p6od9s - 1)) = (*(_apig8meb+(_b5p6od9s - 1)) + _8lucc4fb);
Mark50:;
								// continue
							}
														}						}
						*(_apig8meb+(_znpjgsef - 1)) = (_6j9l5fwy + ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
Mark60:;
						// continue
					}
										}				}
				{
					System.Int32 __81fgg2dlsvn3161 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3161 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3161;
					for (__81fgg2count3161 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3161 + __81fgg2step3161) / __81fgg2step3161)), _b5p6od9s = __81fgg2dlsvn3161; __81fgg2count3161 != 0; __81fgg2count3161--, _b5p6od9s += (__81fgg2step3161)) {

					{
						
						_6j9l5fwy = *(_apig8meb+(_b5p6od9s - 1));
						if ((_lwoxlbje < _6j9l5fwy) | _lilv8egi(ref _6j9l5fwy ))
						_lwoxlbje = _6j9l5fwy;
Mark70:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn3162 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3162 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3162;
					for (__81fgg2count3162 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3162 + __81fgg2step3162) / __81fgg2step3162)), _b5p6od9s = __81fgg2dlsvn3162; __81fgg2count3162 != 0; __81fgg2count3162--, _b5p6od9s += (__81fgg2step3162)) {

					{
						
						*(_apig8meb+(_b5p6od9s - 1)) = _d0547bi2;
Mark80:;
						// continue
					}
										}				}
				{
					System.Int32 __81fgg2dlsvn3163 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3163 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3163;
					for (__81fgg2count3163 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3163 + __81fgg2step3163) / __81fgg2step3163)), _znpjgsef = __81fgg2dlsvn3163; __81fgg2count3163 != 0; __81fgg2count3163--, _znpjgsef += (__81fgg2step3163)) {

					{
						
						_6j9l5fwy = (*(_apig8meb+(_znpjgsef - 1)) + ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
						{
							System.Int32 __81fgg2dlsvn3164 = (System.Int32)((_znpjgsef + (int)1));
							const System.Int32 __81fgg2step3164 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3164;
							for (__81fgg2count3164 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3164 + __81fgg2step3164) / __81fgg2step3164)), _b5p6od9s = __81fgg2dlsvn3164; __81fgg2count3164 != 0; __81fgg2count3164--, _b5p6od9s += (__81fgg2step3164)) {

							{
								
								_8lucc4fb = ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) );
								_6j9l5fwy = (_6j9l5fwy + _8lucc4fb);
								*(_apig8meb+(_b5p6od9s - 1)) = (*(_apig8meb+(_b5p6od9s - 1)) + _8lucc4fb);
Mark90:;
								// continue
							}
														}						}
						if ((_lwoxlbje < _6j9l5fwy) | _lilv8egi(ref _6j9l5fwy ))
						_lwoxlbje = _6j9l5fwy;
Mark100:;
						// continue
					}
										}				}
			}
			
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
			*(_8l4yph2p+((int)2 - 1)) = _kxg5drh2;//* 
			//*        Sum off-diagonals 
			//* 
			
			if (_w8y2rzgy(_9wyre9zc ,"U" ))
			{
				
				{
					System.Int32 __81fgg2dlsvn3165 = (System.Int32)((int)2);
					const System.Int32 __81fgg2step3165 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3165;
					for (__81fgg2count3165 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3165 + __81fgg2step3165) / __81fgg2step3165)), _znpjgsef = __81fgg2dlsvn3165; __81fgg2count3165 != 0; __81fgg2count3165--, _znpjgsef += (__81fgg2step3165)) {

					{
						
						*(_70n56i0m+((int)1 - 1)) = _d0547bi2;
						*(_70n56i0m+((int)2 - 1)) = _kxg5drh2;
						_0mgnvelf(ref Unsafe.AsRef(_znpjgsef - (int)1) ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_70n56i0m+((int)1 - 1))) ,ref Unsafe.AsRef(*(_70n56i0m+((int)2 - 1))) );
						_4hcafcgj(_8l4yph2p ,_70n56i0m );
Mark110:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn3166 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3166 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3166;
					for (__81fgg2count3166 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn3166 + __81fgg2step3166) / __81fgg2step3166)), _znpjgsef = __81fgg2dlsvn3166; __81fgg2count3166 != 0; __81fgg2count3166--, _znpjgsef += (__81fgg2step3166)) {

					{
						
						*(_70n56i0m+((int)1 - 1)) = _d0547bi2;
						*(_70n56i0m+((int)2 - 1)) = _kxg5drh2;
						_0mgnvelf(ref Unsafe.AsRef(_dxpq0xkr - _znpjgsef) ,(_vxfgpup9+(_znpjgsef + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_70n56i0m+((int)1 - 1))) ,ref Unsafe.AsRef(*(_70n56i0m+((int)2 - 1))) );
						_4hcafcgj(_8l4yph2p ,_70n56i0m );
Mark120:;
						// continue
					}
										}				}
			}
			
			*(_8l4yph2p+((int)2 - 1)) = ((int)2 * *(_8l4yph2p+((int)2 - 1)));//* 
			//*        Sum diagonal 
			//* 
			
			*(_70n56i0m+((int)1 - 1)) = _d0547bi2;
			*(_70n56i0m+((int)2 - 1)) = _kxg5drh2;
			_0mgnvelf(ref _dxpq0xkr ,_vxfgpup9 ,ref Unsafe.AsRef(_ocv8fk5c + (int)1) ,ref Unsafe.AsRef(*(_70n56i0m+((int)1 - 1))) ,ref Unsafe.AsRef(*(_70n56i0m+((int)2 - 1))) );
			_4hcafcgj(_8l4yph2p ,_70n56i0m );
			_lwoxlbje = (*(_8l4yph2p+((int)1 - 1)) * ILNumerics.F2NET.Intrinsics.SQRT(*(_8l4yph2p+((int)2 - 1)) ));
		}
		//* 
		
		_494ans48 = _lwoxlbje;
		return _494ans48;//* 
		//*     End of SLANSY 
		//* 
		
	}
	
	return _494ans48;
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
