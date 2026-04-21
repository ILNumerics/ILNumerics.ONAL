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
//*> \brief \b ZLANHE returns the value of the 1-norm, or the Frobenius norm, or the infinity norm, or the element of largest absolute value of a complex Hermitian matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZLANHE + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zlanhe.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zlanhe.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zlanhe.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       DOUBLE PRECISION FUNCTION ZLANHE( NORM, UPLO, N, A, LDA, WORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          NORM, UPLO 
//*       INTEGER            LDA, N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   WORK( * ) 
//*       COMPLEX*16         A( LDA, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZLANHE  returns the value of the one norm,  or the Frobenius norm, or 
//*> the  infinity norm,  or the  element of  largest absolute value  of a 
//*> complex hermitian matrix A. 
//*> \endverbatim 
//*> 
//*> \return ZLANHE 
//*> \verbatim 
//*> 
//*>    ZLANHE = ( max(abs(A(i,j))), NORM = 'M' or 'm' 
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
//*>          Specifies the value to be returned in ZLANHE as described 
//*>          above. 
//*> \endverbatim 
//*> 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>          Specifies whether the upper or lower triangular part of the 
//*>          hermitian matrix A is to be referenced. 
//*>          = 'U':  Upper triangular part of A is referenced 
//*>          = 'L':  Lower triangular part of A is referenced 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix A.  N >= 0.  When N = 0, ZLANHE is 
//*>          set to zero. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension (LDA,N) 
//*>          The hermitian matrix A.  If UPLO = 'U', the leading n by n 
//*>          upper triangular part of A contains the upper triangular part 
//*>          of the matrix A, and the strictly lower triangular part of A 
//*>          is not referenced.  If UPLO = 'L', the leading n by n lower 
//*>          triangular part of A contains the lower triangular part of 
//*>          the matrix A, and the strictly upper triangular part of A is 
//*>          not referenced. Note that the imaginary parts of the diagonal 
//*>          elements need not be set and are assumed to be zero. 
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
//*>          WORK is DOUBLE PRECISION array, dimension (MAX(1,LWORK)), 
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
//*> \ingroup complex16HEauxiliary 
//* 
//*  ===================================================================== 

	 
	public static Double _130vmvn8(FString _gq71rsgu, FString _9wyre9zc, ref Int32 _dxpq0xkr, complex* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _apig8meb)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)32 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Double _130vmvn8 = default;
Double _kxg5drh2 =  1d;
Double _d0547bi2 =  0d;
Int32 _b5p6od9s =  default;
Int32 _znpjgsef =  default;
Double _8lucc4fb =  default;
Double _6j9l5fwy =  default;
Double _lwoxlbje =  default;
Double* _8l4yph2p =  (Double*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Double) * ((int)2);
Double* _70n56i0m =  (Double*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Double) * ((int)2);
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
					System.Int32 __81fgg2dlsvn3905 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3905 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3905;
					for (__81fgg2count3905 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3905 + __81fgg2step3905) / __81fgg2step3905)), _znpjgsef = __81fgg2dlsvn3905; __81fgg2count3905 != 0; __81fgg2count3905--, _znpjgsef += (__81fgg2step3905)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn3906 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3906 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3906;
							for (__81fgg2count3906 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn3906 + __81fgg2step3906) / __81fgg2step3906)), _b5p6od9s = __81fgg2dlsvn3906; __81fgg2count3906 != 0; __81fgg2count3906--, _b5p6od9s += (__81fgg2step3906)) {

							{
								
								_6j9l5fwy = ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) );
								if ((_lwoxlbje < _6j9l5fwy) | _fk98jwhi(ref _6j9l5fwy ))
								_lwoxlbje = _6j9l5fwy;
Mark10:;
								// continue
							}
														}						}
						_6j9l5fwy = ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.DBLE(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) );
						if ((_lwoxlbje < _6j9l5fwy) | _fk98jwhi(ref _6j9l5fwy ))
						_lwoxlbje = _6j9l5fwy;
Mark20:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn3907 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3907 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3907;
					for (__81fgg2count3907 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3907 + __81fgg2step3907) / __81fgg2step3907)), _znpjgsef = __81fgg2dlsvn3907; __81fgg2count3907 != 0; __81fgg2count3907--, _znpjgsef += (__81fgg2step3907)) {

					{
						
						_6j9l5fwy = ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.DBLE(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) );
						if ((_lwoxlbje < _6j9l5fwy) | _fk98jwhi(ref _6j9l5fwy ))
						_lwoxlbje = _6j9l5fwy;
						{
							System.Int32 __81fgg2dlsvn3908 = (System.Int32)((_znpjgsef + (int)1));
							const System.Int32 __81fgg2step3908 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3908;
							for (__81fgg2count3908 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3908 + __81fgg2step3908) / __81fgg2step3908)), _b5p6od9s = __81fgg2dlsvn3908; __81fgg2count3908 != 0; __81fgg2count3908--, _b5p6od9s += (__81fgg2step3908)) {

							{
								
								_6j9l5fwy = ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) );
								if ((_lwoxlbje < _6j9l5fwy) | _fk98jwhi(ref _6j9l5fwy ))
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
			//*        Find normI(A) ( = norm1(A), since A is hermitian). 
			//* 
			
			_lwoxlbje = _d0547bi2;
			if (_w8y2rzgy(_9wyre9zc ,"U" ))
			{
				
				{
					System.Int32 __81fgg2dlsvn3909 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3909 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3909;
					for (__81fgg2count3909 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3909 + __81fgg2step3909) / __81fgg2step3909)), _znpjgsef = __81fgg2dlsvn3909; __81fgg2count3909 != 0; __81fgg2count3909--, _znpjgsef += (__81fgg2step3909)) {

					{
						
						_6j9l5fwy = _d0547bi2;
						{
							System.Int32 __81fgg2dlsvn3910 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3910 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3910;
							for (__81fgg2count3910 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn3910 + __81fgg2step3910) / __81fgg2step3910)), _b5p6od9s = __81fgg2dlsvn3910; __81fgg2count3910 != 0; __81fgg2count3910--, _b5p6od9s += (__81fgg2step3910)) {

							{
								
								_8lucc4fb = ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) );
								_6j9l5fwy = (_6j9l5fwy + _8lucc4fb);
								*(_apig8meb+(_b5p6od9s - 1)) = (*(_apig8meb+(_b5p6od9s - 1)) + _8lucc4fb);
Mark50:;
								// continue
							}
														}						}
						*(_apig8meb+(_znpjgsef - 1)) = (_6j9l5fwy + ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.DBLE(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) ));
Mark60:;
						// continue
					}
										}				}
				{
					System.Int32 __81fgg2dlsvn3911 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3911 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3911;
					for (__81fgg2count3911 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3911 + __81fgg2step3911) / __81fgg2step3911)), _b5p6od9s = __81fgg2dlsvn3911; __81fgg2count3911 != 0; __81fgg2count3911--, _b5p6od9s += (__81fgg2step3911)) {

					{
						
						_6j9l5fwy = *(_apig8meb+(_b5p6od9s - 1));
						if ((_lwoxlbje < _6j9l5fwy) | _fk98jwhi(ref _6j9l5fwy ))
						_lwoxlbje = _6j9l5fwy;
Mark70:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn3912 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3912 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3912;
					for (__81fgg2count3912 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3912 + __81fgg2step3912) / __81fgg2step3912)), _b5p6od9s = __81fgg2dlsvn3912; __81fgg2count3912 != 0; __81fgg2count3912--, _b5p6od9s += (__81fgg2step3912)) {

					{
						
						*(_apig8meb+(_b5p6od9s - 1)) = _d0547bi2;
Mark80:;
						// continue
					}
										}				}
				{
					System.Int32 __81fgg2dlsvn3913 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3913 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3913;
					for (__81fgg2count3913 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3913 + __81fgg2step3913) / __81fgg2step3913)), _znpjgsef = __81fgg2dlsvn3913; __81fgg2count3913 != 0; __81fgg2count3913--, _znpjgsef += (__81fgg2step3913)) {

					{
						
						_6j9l5fwy = (*(_apig8meb+(_znpjgsef - 1)) + ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.DBLE(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) ));
						{
							System.Int32 __81fgg2dlsvn3914 = (System.Int32)((_znpjgsef + (int)1));
							const System.Int32 __81fgg2step3914 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3914;
							for (__81fgg2count3914 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3914 + __81fgg2step3914) / __81fgg2step3914)), _b5p6od9s = __81fgg2dlsvn3914; __81fgg2count3914 != 0; __81fgg2count3914--, _b5p6od9s += (__81fgg2step3914)) {

							{
								
								_8lucc4fb = ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) );
								_6j9l5fwy = (_6j9l5fwy + _8lucc4fb);
								*(_apig8meb+(_b5p6od9s - 1)) = (*(_apig8meb+(_b5p6od9s - 1)) + _8lucc4fb);
Mark90:;
								// continue
							}
														}						}
						if ((_lwoxlbje < _6j9l5fwy) | _fk98jwhi(ref _6j9l5fwy ))
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
					System.Int32 __81fgg2dlsvn3915 = (System.Int32)((int)2);
					const System.Int32 __81fgg2step3915 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3915;
					for (__81fgg2count3915 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3915 + __81fgg2step3915) / __81fgg2step3915)), _znpjgsef = __81fgg2dlsvn3915; __81fgg2count3915 != 0; __81fgg2count3915--, _znpjgsef += (__81fgg2step3915)) {

					{
						
						*(_70n56i0m+((int)1 - 1)) = _d0547bi2;
						*(_70n56i0m+((int)2 - 1)) = _kxg5drh2;
						_s6ao1et5(ref Unsafe.AsRef(_znpjgsef - (int)1) ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_70n56i0m+((int)1 - 1))) ,ref Unsafe.AsRef(*(_70n56i0m+((int)2 - 1))) );
						_2ah5b9jc(_8l4yph2p ,_70n56i0m );
Mark110:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn3916 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3916 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3916;
					for (__81fgg2count3916 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn3916 + __81fgg2step3916) / __81fgg2step3916)), _znpjgsef = __81fgg2dlsvn3916; __81fgg2count3916 != 0; __81fgg2count3916--, _znpjgsef += (__81fgg2step3916)) {

					{
						
						*(_70n56i0m+((int)1 - 1)) = _d0547bi2;
						*(_70n56i0m+((int)2 - 1)) = _kxg5drh2;
						_s6ao1et5(ref Unsafe.AsRef(_dxpq0xkr - _znpjgsef) ,(_vxfgpup9+(_znpjgsef + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_70n56i0m+((int)1 - 1))) ,ref Unsafe.AsRef(*(_70n56i0m+((int)2 - 1))) );
						_2ah5b9jc(_8l4yph2p ,_70n56i0m );
Mark120:;
						// continue
					}
										}				}
			}
			
			*(_8l4yph2p+((int)2 - 1)) = ((int)2 * *(_8l4yph2p+((int)2 - 1)));//* 
			//*        Sum diagonal 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn3917 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step3917 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3917;
				for (__81fgg2count3917 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3917 + __81fgg2step3917) / __81fgg2step3917)), _b5p6od9s = __81fgg2dlsvn3917; __81fgg2count3917 != 0; __81fgg2count3917--, _b5p6od9s += (__81fgg2step3917)) {

				{
					
					if (ILNumerics.F2NET.Intrinsics.DBLE(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) != _d0547bi2)
					{
						
						_8lucc4fb = ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.DBLE(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) );
						if (*(_8l4yph2p+((int)1 - 1)) < _8lucc4fb)
						{
							
							*(_8l4yph2p+((int)2 - 1)) = (_kxg5drh2 + (*(_8l4yph2p+((int)2 - 1)) * __POW2((*(_8l4yph2p+((int)1 - 1)) / _8lucc4fb))));
							*(_8l4yph2p+((int)1 - 1)) = _8lucc4fb;
						}
						else
						{
							
							*(_8l4yph2p+((int)2 - 1)) = (*(_8l4yph2p+((int)2 - 1)) + __POW2((_8lucc4fb / *(_8l4yph2p+((int)1 - 1)))));
						}
						
					}
					
Mark130:;
					// continue
				}
								}			}
			_lwoxlbje = (*(_8l4yph2p+((int)1 - 1)) * ILNumerics.F2NET.Intrinsics.SQRT(*(_8l4yph2p+((int)2 - 1)) ));
		}
		//* 
		
		_130vmvn8 = _lwoxlbje;
		return _130vmvn8;//* 
		//*     End of ZLANHE 
		//* 
		
	}
	
	return _130vmvn8;
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
