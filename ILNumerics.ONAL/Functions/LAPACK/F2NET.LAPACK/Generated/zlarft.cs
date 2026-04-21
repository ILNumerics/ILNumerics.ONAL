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
//*> \brief \b ZLARFT forms the triangular factor T of a block reflector H = I - vtvH 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZLARFT + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zlarft.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zlarft.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zlarft.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZLARFT( DIRECT, STOREV, N, K, V, LDV, TAU, T, LDT ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          DIRECT, STOREV 
//*       INTEGER            K, LDT, LDV, N 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX*16         T( LDT, * ), TAU( * ), V( LDV, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZLARFT forms the triangular factor T of a complex block reflector H 
//*> of order n, which is defined as a product of k elementary reflectors. 
//*> 
//*> If DIRECT = 'F', H = H(1) H(2) . . . H(k) and T is upper triangular; 
//*> 
//*> If DIRECT = 'B', H = H(k) . . . H(2) H(1) and T is lower triangular. 
//*> 
//*> If STOREV = 'C', the vector which defines the elementary reflector 
//*> H(i) is stored in the i-th column of the array V, and 
//*> 
//*>    H  =  I - V * T * V**H 
//*> 
//*> If STOREV = 'R', the vector which defines the elementary reflector 
//*> H(i) is stored in the i-th row of the array V, and 
//*> 
//*>    H  =  I - V**H * T * V 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] DIRECT 
//*> \verbatim 
//*>          DIRECT is CHARACTER*1 
//*>          Specifies the order in which the elementary reflectors are 
//*>          multiplied to form the block reflector: 
//*>          = 'F': H = H(1) H(2) . . . H(k) (Forward) 
//*>          = 'B': H = H(k) . . . H(2) H(1) (Backward) 
//*> \endverbatim 
//*> 
//*> \param[in] STOREV 
//*> \verbatim 
//*>          STOREV is CHARACTER*1 
//*>          Specifies how the vectors which define the elementary 
//*>          reflectors are stored (see also Further Details): 
//*>          = 'C': columnwise 
//*>          = 'R': rowwise 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the block reflector H. N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] K 
//*> \verbatim 
//*>          K is INTEGER 
//*>          The order of the triangular factor T (= the number of 
//*>          elementary reflectors). K >= 1. 
//*> \endverbatim 
//*> 
//*> \param[in] V 
//*> \verbatim 
//*>          V is COMPLEX*16 array, dimension 
//*>                               (LDV,K) if STOREV = 'C' 
//*>                               (LDV,N) if STOREV = 'R' 
//*>          The matrix V. See further details. 
//*> \endverbatim 
//*> 
//*> \param[in] LDV 
//*> \verbatim 
//*>          LDV is INTEGER 
//*>          The leading dimension of the array V. 
//*>          If STOREV = 'C', LDV >= max(1,N); if STOREV = 'R', LDV >= K. 
//*> \endverbatim 
//*> 
//*> \param[in] TAU 
//*> \verbatim 
//*>          TAU is COMPLEX*16 array, dimension (K) 
//*>          TAU(i) must contain the scalar factor of the elementary 
//*>          reflector H(i). 
//*> \endverbatim 
//*> 
//*> \param[out] T 
//*> \verbatim 
//*>          T is COMPLEX*16 array, dimension (LDT,K) 
//*>          The k by k triangular factor T of the block reflector. 
//*>          If DIRECT = 'F', T is upper triangular; if DIRECT = 'B', T is 
//*>          lower triangular. The rest of the array is not used. 
//*> \endverbatim 
//*> 
//*> \param[in] LDT 
//*> \verbatim 
//*>          LDT is INTEGER 
//*>          The leading dimension of the array T. LDT >= K. 
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
//*> \ingroup complex16OTHERauxiliary 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  The shape of the matrix V and the storage of the vectors which define 
//*>  the H(i) is best illustrated by the following example with n = 5 and 
//*>  k = 3. The elements equal to 1 are not stored. 
//*> 
//*>  DIRECT = 'F' and STOREV = 'C':         DIRECT = 'F' and STOREV = 'R': 
//*> 
//*>               V = (  1       )                 V = (  1 v1 v1 v1 v1 ) 
//*>                   ( v1  1    )                     (     1 v2 v2 v2 ) 
//*>                   ( v1 v2  1 )                     (        1 v3 v3 ) 
//*>                   ( v1 v2 v3 ) 
//*>                   ( v1 v2 v3 ) 
//*> 
//*>  DIRECT = 'B' and STOREV = 'C':         DIRECT = 'B' and STOREV = 'R': 
//*> 
//*>               V = ( v1 v2 v3 )                 V = ( v1 v1  1       ) 
//*>                   ( v1 v2 v3 )                     ( v2 v2 v2  1    ) 
//*>                   (  1 v2 v3 )                     ( v3 v3 v3 v3  1 ) 
//*>                   (     1 v3 ) 
//*>                   (        1 ) 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _4nve9r8g(FString _uw10mx43, FString _tjtvdgd6, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, complex* _ycxba85s, ref Int32 _ys09rxze, complex* _0446f4de, complex* _2ivtt43r, ref Int32 _w8yhbr2r)
	{
#region variable declarations
complex _kxg5drh2 =   new fcomplex(1f,0f);
complex _d0547bi2 =   new fcomplex(0f,0f);
Int32 _b5p6od9s =  default;
Int32 _znpjgsef =  default;
Int32 _zlpsoxly =  default;
Int32 _thvhilfl =  default;
string fLanavab = default;
#endregion  variable declarations
_uw10mx43 = _uw10mx43.Convert(1);
_tjtvdgd6 = _tjtvdgd6.Convert(1);

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.0) -- 
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
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr == (int)0)
		return;//* 
		
		if (_w8y2rzgy(_uw10mx43 ,"F" ))
		{
			
			_zlpsoxly = _dxpq0xkr;
			{
				System.Int32 __81fgg2dlsvn1231 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1231 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1231;
				for (__81fgg2count1231 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1231 + __81fgg2step1231) / __81fgg2step1231)), _b5p6od9s = __81fgg2dlsvn1231; __81fgg2count1231 != 0; __81fgg2count1231--, _b5p6od9s += (__81fgg2step1231)) {

				{
					
					_zlpsoxly = ILNumerics.F2NET.Intrinsics.MAX(_zlpsoxly ,_b5p6od9s );
					if (*(_0446f4de+(_b5p6od9s - 1)) == _d0547bi2)
					{
						//* 
						//*              H(i)  =  I 
						//* 
						
						{
							System.Int32 __81fgg2dlsvn1232 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1232 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1232;
							for (__81fgg2count1232 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s) - __81fgg2dlsvn1232 + __81fgg2step1232) / __81fgg2step1232)), _znpjgsef = __81fgg2dlsvn1232; __81fgg2count1232 != 0; __81fgg2count1232--, _znpjgsef += (__81fgg2step1232)) {

							{
								
								*(_2ivtt43r+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)) = _d0547bi2;
							}
														}						}
					}
					else
					{
						//* 
						//*              general case 
						//* 
						
						if (_w8y2rzgy(_tjtvdgd6 ,"C" ))
						{
							//*                 Skip any trailing zeros. 
							
							{
								System.Int32 __81fgg2dlsvn1233 = (System.Int32)(_dxpq0xkr);
								System.Int32 __81fgg2step1233 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count1233;
								for (__81fgg2count1233 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s + (int)1) - __81fgg2dlsvn1233 + __81fgg2step1233) / __81fgg2step1233)), _thvhilfl = __81fgg2dlsvn1233; __81fgg2count1233 != 0; __81fgg2count1233--, _thvhilfl += (__81fgg2step1233)) {

								{
									
									if (*(_ycxba85s+(_thvhilfl - 1) + (_b5p6od9s - 1) * 1 * (_ys09rxze)) != _d0547bi2)
									break;
								}
																}							}
							{
								System.Int32 __81fgg2dlsvn1234 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1234 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1234;
								for (__81fgg2count1234 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn1234 + __81fgg2step1234) / __81fgg2step1234)), _znpjgsef = __81fgg2dlsvn1234; __81fgg2count1234 != 0; __81fgg2count1234--, _znpjgsef += (__81fgg2step1234)) {

								{
									
									*(_2ivtt43r+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)) = (-((*(_0446f4de+(_b5p6od9s - 1)) * ILNumerics.F2NET.Intrinsics.CONJG(*(_ycxba85s+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ys09rxze)) ))));
								}
																}							}
							_znpjgsef = ILNumerics.F2NET.Intrinsics.MIN(_thvhilfl ,_zlpsoxly );//* 
							//*                 T(1:i-1,i) := - tau(i) * V(i:j,1:i-1)**H * V(i:j,i) 
							//* 
							
							_xfaqgfxk("Conjugate transpose" ,ref Unsafe.AsRef(_znpjgsef - _b5p6od9s) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(-(*(_0446f4de+(_b5p6od9s - 1)))) ,(_ycxba85s+(_b5p6od9s + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,(_ycxba85s+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ys09rxze)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_2ivtt43r+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) );
						}
						else
						{
							//*                 Skip any trailing zeros. 
							
							{
								System.Int32 __81fgg2dlsvn1235 = (System.Int32)(_dxpq0xkr);
								System.Int32 __81fgg2step1235 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count1235;
								for (__81fgg2count1235 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s + (int)1) - __81fgg2dlsvn1235 + __81fgg2step1235) / __81fgg2step1235)), _thvhilfl = __81fgg2dlsvn1235; __81fgg2count1235 != 0; __81fgg2count1235--, _thvhilfl += (__81fgg2step1235)) {

								{
									
									if (*(_ycxba85s+(_b5p6od9s - 1) + (_thvhilfl - 1) * 1 * (_ys09rxze)) != _d0547bi2)
									break;
								}
																}							}
							{
								System.Int32 __81fgg2dlsvn1236 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1236 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1236;
								for (__81fgg2count1236 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn1236 + __81fgg2step1236) / __81fgg2step1236)), _znpjgsef = __81fgg2dlsvn1236; __81fgg2count1236 != 0; __81fgg2count1236--, _znpjgsef += (__81fgg2step1236)) {

								{
									
									*(_2ivtt43r+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)) = (-((*(_0446f4de+(_b5p6od9s - 1)) * *(_ycxba85s+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_ys09rxze)))));
								}
																}							}
							_znpjgsef = ILNumerics.F2NET.Intrinsics.MIN(_thvhilfl ,_zlpsoxly );//* 
							//*                 T(1:i-1,i) := - tau(i) * V(1:i-1,i:j) * V(i,i:j)**H 
							//* 
							
							_xos1d1er("N" ,"C" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_znpjgsef - _b5p6od9s) ,ref Unsafe.AsRef(-(*(_0446f4de+(_b5p6od9s - 1)))) ,(_ycxba85s+((int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,(_ycxba85s+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,(_2ivtt43r+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r );
						}
						//* 
						//*              T(1:i-1,i) := T(1:i-1,1:i-1) * T(1:i-1,i) 
						//* 
						
						_xajlj6s7("Upper" ,"No transpose" ,"Non-unit" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,_2ivtt43r ,ref _w8yhbr2r ,(_2ivtt43r+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) );
						*(_2ivtt43r+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)) = *(_0446f4de+(_b5p6od9s - 1));
						if (_b5p6od9s > (int)1)
						{
							
							_zlpsoxly = ILNumerics.F2NET.Intrinsics.MAX(_zlpsoxly ,_thvhilfl );
						}
						else
						{
							
							_zlpsoxly = _thvhilfl;
						}
						
					}
					
				}
								}			}
		}
		else
		{
			
			_zlpsoxly = (int)1;
			{
				System.Int32 __81fgg2dlsvn1237 = (System.Int32)(_umlkckdg);
				System.Int32 __81fgg2step1237 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count1237;
				for (__81fgg2count1237 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1237 + __81fgg2step1237) / __81fgg2step1237)), _b5p6od9s = __81fgg2dlsvn1237; __81fgg2count1237 != 0; __81fgg2count1237--, _b5p6od9s += (__81fgg2step1237)) {

				{
					
					if (*(_0446f4de+(_b5p6od9s - 1)) == _d0547bi2)
					{
						//* 
						//*              H(i)  =  I 
						//* 
						
						{
							System.Int32 __81fgg2dlsvn1238 = (System.Int32)(_b5p6od9s);
							const System.Int32 __81fgg2step1238 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1238;
							for (__81fgg2count1238 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1238 + __81fgg2step1238) / __81fgg2step1238)), _znpjgsef = __81fgg2dlsvn1238; __81fgg2count1238 != 0; __81fgg2count1238--, _znpjgsef += (__81fgg2step1238)) {

							{
								
								*(_2ivtt43r+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)) = _d0547bi2;
							}
														}						}
					}
					else
					{
						//* 
						//*              general case 
						//* 
						
						if (_b5p6od9s < _umlkckdg)
						{
							
							if (_w8y2rzgy(_tjtvdgd6 ,"C" ))
							{
								//*                    Skip any leading zeros. 
								
								{
									System.Int32 __81fgg2dlsvn1239 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1239 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1239;
									for (__81fgg2count1239 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn1239 + __81fgg2step1239) / __81fgg2step1239)), _thvhilfl = __81fgg2dlsvn1239; __81fgg2count1239 != 0; __81fgg2count1239--, _thvhilfl += (__81fgg2step1239)) {

									{
										
										if (*(_ycxba85s+(_thvhilfl - 1) + (_b5p6od9s - 1) * 1 * (_ys09rxze)) != _d0547bi2)
										break;
									}
																		}								}
								{
									System.Int32 __81fgg2dlsvn1240 = (System.Int32)((_b5p6od9s + (int)1));
									const System.Int32 __81fgg2step1240 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1240;
									for (__81fgg2count1240 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1240 + __81fgg2step1240) / __81fgg2step1240)), _znpjgsef = __81fgg2dlsvn1240; __81fgg2count1240 != 0; __81fgg2count1240--, _znpjgsef += (__81fgg2step1240)) {

									{
										
										*(_2ivtt43r+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)) = (-((*(_0446f4de+(_b5p6od9s - 1)) * ILNumerics.F2NET.Intrinsics.CONJG(*(_ycxba85s+((_dxpq0xkr - _umlkckdg) + _b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ys09rxze)) ))));
									}
																		}								}
								_znpjgsef = ILNumerics.F2NET.Intrinsics.MAX(_thvhilfl ,_zlpsoxly );//* 
								//*                    T(i+1:k,i) = -tau(i) * V(j:n-k+i,i+1:k)**H * V(j:n-k+i,i) 
								//* 
								
								_xfaqgfxk("Conjugate transpose" ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) + _b5p6od9s) - _znpjgsef) ,ref Unsafe.AsRef(_umlkckdg - _b5p6od9s) ,ref Unsafe.AsRef(-(*(_0446f4de+(_b5p6od9s - 1)))) ,(_ycxba85s+(_znpjgsef - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,(_ycxba85s+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_ys09rxze)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_2ivtt43r+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) );
							}
							else
							{
								//*                    Skip any leading zeros. 
								
								{
									System.Int32 __81fgg2dlsvn1241 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1241 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1241;
									for (__81fgg2count1241 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn1241 + __81fgg2step1241) / __81fgg2step1241)), _thvhilfl = __81fgg2dlsvn1241; __81fgg2count1241 != 0; __81fgg2count1241--, _thvhilfl += (__81fgg2step1241)) {

									{
										
										if (*(_ycxba85s+(_b5p6od9s - 1) + (_thvhilfl - 1) * 1 * (_ys09rxze)) != _d0547bi2)
										break;
									}
																		}								}
								{
									System.Int32 __81fgg2dlsvn1242 = (System.Int32)((_b5p6od9s + (int)1));
									const System.Int32 __81fgg2step1242 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1242;
									for (__81fgg2count1242 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1242 + __81fgg2step1242) / __81fgg2step1242)), _znpjgsef = __81fgg2dlsvn1242; __81fgg2count1242 != 0; __81fgg2count1242--, _znpjgsef += (__81fgg2step1242)) {

									{
										
										*(_2ivtt43r+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)) = (-((*(_0446f4de+(_b5p6od9s - 1)) * *(_ycxba85s+(_znpjgsef - 1) + ((_dxpq0xkr - _umlkckdg) + _b5p6od9s - 1) * 1 * (_ys09rxze)))));
									}
																		}								}
								_znpjgsef = ILNumerics.F2NET.Intrinsics.MAX(_thvhilfl ,_zlpsoxly );//* 
								//*                    T(i+1:k,i) = -tau(i) * V(i+1:k,j:n-k+i) * V(i,j:n-k+i)**H 
								//* 
								
								_xos1d1er("N" ,"C" ,ref Unsafe.AsRef(_umlkckdg - _b5p6od9s) ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) + _b5p6od9s) - _znpjgsef) ,ref Unsafe.AsRef(-(*(_0446f4de+(_b5p6od9s - 1)))) ,(_ycxba85s+(_b5p6od9s + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,(_ycxba85s+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,(_2ivtt43r+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r );
							}
							//* 
							//*                 T(i+1:k,i) := T(i+1:k,i+1:k) * T(i+1:k,i) 
							//* 
							
							_xajlj6s7("Lower" ,"No transpose" ,"Non-unit" ,ref Unsafe.AsRef(_umlkckdg - _b5p6od9s) ,(_2ivtt43r+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,(_2ivtt43r+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) );
							if (_b5p6od9s > (int)1)
							{
								
								_zlpsoxly = ILNumerics.F2NET.Intrinsics.MIN(_zlpsoxly ,_thvhilfl );
							}
							else
							{
								
								_zlpsoxly = _thvhilfl;
							}
							
						}
						
						*(_2ivtt43r+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)) = *(_0446f4de+(_b5p6od9s - 1));
					}
					
				}
								}			}
		}
		
		return;//* 
		//*     End of ZLARFT 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
