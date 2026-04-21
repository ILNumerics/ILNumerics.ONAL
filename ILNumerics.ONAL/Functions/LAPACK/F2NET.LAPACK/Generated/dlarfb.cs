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
//*> \brief \b DLARFB applies a block reflector or its transpose to a general rectangular matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLARFB + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlarfb.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlarfb.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlarfb.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLARFB( SIDE, TRANS, DIRECT, STOREV, M, N, K, V, LDV, 
//*                          T, LDT, C, LDC, WORK, LDWORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          DIRECT, SIDE, STOREV, TRANS 
//*       INTEGER            K, LDC, LDT, LDV, LDWORK, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   C( LDC, * ), T( LDT, * ), V( LDV, * ), 
//*      $                   WORK( LDWORK, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLARFB applies a real block reflector H or its transpose H**T to a 
//*> real m by n matrix C, from either the left or the right. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] SIDE 
//*> \verbatim 
//*>          SIDE is CHARACTER*1 
//*>          = 'L': apply H or H**T from the Left 
//*>          = 'R': apply H or H**T from the Right 
//*> \endverbatim 
//*> 
//*> \param[in] TRANS 
//*> \verbatim 
//*>          TRANS is CHARACTER*1 
//*>          = 'N': apply H (No transpose) 
//*>          = 'T': apply H**T (Transpose) 
//*> \endverbatim 
//*> 
//*> \param[in] DIRECT 
//*> \verbatim 
//*>          DIRECT is CHARACTER*1 
//*>          Indicates how H is formed from a product of elementary 
//*>          reflectors 
//*>          = 'F': H = H(1) H(2) . . . H(k) (Forward) 
//*>          = 'B': H = H(k) . . . H(2) H(1) (Backward) 
//*> \endverbatim 
//*> 
//*> \param[in] STOREV 
//*> \verbatim 
//*>          STOREV is CHARACTER*1 
//*>          Indicates how the vectors which define the elementary 
//*>          reflectors are stored: 
//*>          = 'C': Columnwise 
//*>          = 'R': Rowwise 
//*> \endverbatim 
//*> 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of rows of the matrix C. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of columns of the matrix C. 
//*> \endverbatim 
//*> 
//*> \param[in] K 
//*> \verbatim 
//*>          K is INTEGER 
//*>          The order of the matrix T (= the number of elementary 
//*>          reflectors whose product defines the block reflector). 
//*>          If SIDE = 'L', M >= K >= 0; 
//*>          if SIDE = 'R', N >= K >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] V 
//*> \verbatim 
//*>          V is DOUBLE PRECISION array, dimension 
//*>                                (LDV,K) if STOREV = 'C' 
//*>                                (LDV,M) if STOREV = 'R' and SIDE = 'L' 
//*>                                (LDV,N) if STOREV = 'R' and SIDE = 'R' 
//*>          The matrix V. See Further Details. 
//*> \endverbatim 
//*> 
//*> \param[in] LDV 
//*> \verbatim 
//*>          LDV is INTEGER 
//*>          The leading dimension of the array V. 
//*>          If STOREV = 'C' and SIDE = 'L', LDV >= max(1,M); 
//*>          if STOREV = 'C' and SIDE = 'R', LDV >= max(1,N); 
//*>          if STOREV = 'R', LDV >= K. 
//*> \endverbatim 
//*> 
//*> \param[in] T 
//*> \verbatim 
//*>          T is DOUBLE PRECISION array, dimension (LDT,K) 
//*>          The triangular k by k matrix T in the representation of the 
//*>          block reflector. 
//*> \endverbatim 
//*> 
//*> \param[in] LDT 
//*> \verbatim 
//*>          LDT is INTEGER 
//*>          The leading dimension of the array T. LDT >= K. 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is DOUBLE PRECISION array, dimension (LDC,N) 
//*>          On entry, the m by n matrix C. 
//*>          On exit, C is overwritten by H*C or H**T*C or C*H or C*H**T. 
//*> \endverbatim 
//*> 
//*> \param[in] LDC 
//*> \verbatim 
//*>          LDC is INTEGER 
//*>          The leading dimension of the array C. LDC >= max(1,M). 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is DOUBLE PRECISION array, dimension (LDWORK,K) 
//*> \endverbatim 
//*> 
//*> \param[in] LDWORK 
//*> \verbatim 
//*>          LDWORK is INTEGER 
//*>          The leading dimension of the array WORK. 
//*>          If SIDE = 'L', LDWORK >= max(1,N); 
//*>          if SIDE = 'R', LDWORK >= max(1,M). 
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
//*> \date June 2013 
//* 
//*> \ingroup doubleOTHERauxiliary 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  The shape of the matrix V and the storage of the vectors which define 
//*>  the H(i) is best illustrated by the following example with n = 5 and 
//*>  k = 3. The elements equal to 1 are not stored; the corresponding 
//*>  array elements are modified but restored on exit. The rest of the 
//*>  array is not used. 
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

	 
	public static void _s5p1x6x6(FString _m2cn2gjg, FString _scuo79v4, FString _uw10mx43, FString _tjtvdgd6, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, Double* _ycxba85s, ref Int32 _ys09rxze, Double* _2ivtt43r, ref Int32 _w8yhbr2r, Double* _3crf0qn3, ref Int32 _1s3eymp4, Double* _apig8meb, ref Int32 _iykhdriq)
	{
#region variable declarations
Double _kxg5drh2 =  1d;
FString _l09pt3ga =  new FString(1);
Int32 _b5p6od9s =  default;
Int32 _znpjgsef =  default;
string fLanavab = default;
#endregion  variable declarations
_m2cn2gjg = _m2cn2gjg.Convert(1);
_scuo79v4 = _scuo79v4.Convert(1);
_uw10mx43 = _uw10mx43.Convert(1);
_tjtvdgd6 = _tjtvdgd6.Convert(1);

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     June 2013 
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
		//*     .. External Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Quick return if possible 
		//* 
		
		if ((_ev4xhht5 <= (int)0) | (_dxpq0xkr <= (int)0))
		return;//* 
		
		if (_w8y2rzgy(_scuo79v4 ,"N" ))
		{
			
			
			_l09pt3ga = "T";
		}
		else
		{
			
			
			_l09pt3ga = "N";
		}
		//* 
		
		if (_w8y2rzgy(_tjtvdgd6 ,"C" ))
		{
			//* 
			
			if (_w8y2rzgy(_uw10mx43 ,"F" ))
			{
				//* 
				//*           Let  V =  ( V1 )    (first K rows) 
				//*                     ( V2 ) 
				//*           where  V1  is unit lower triangular. 
				//* 
				
				if (_w8y2rzgy(_m2cn2gjg ,"L" ))
				{
					//* 
					//*              Form  H * C  or  H**T * C  where  C = ( C1 ) 
					//*                                                    ( C2 ) 
					//* 
					//*              W := C**T * V  =  (C1**T * V1 + C2**T * V2)  (stored in WORK) 
					//* 
					//*              W := C1**T 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn416 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step416 = (System.Int32)((int)1);
						System.Int32 __81fgg2count416;
						for (__81fgg2count416 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn416 + __81fgg2step416) / __81fgg2step416)), _znpjgsef = __81fgg2dlsvn416; __81fgg2count416 != 0; __81fgg2count416--, _znpjgsef += (__81fgg2step416)) {

						{
							
							_gvjhlct0(ref _dxpq0xkr ,(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark10:;
							// continue
						}
												}					}//* 
					//*              W := W * V1 
					//* 
					
					_birntqim("Right" ,"Lower" ,"No transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 W := W + C2**T * V2 
						//* 
						
						_5nsxi69c("Transpose" ,"No transpose" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_ycxba85s+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T**T  or  W * T 
					//* 
					
					_birntqim("Right" ,"Upper" ,_l09pt3ga ,"Non-unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - V * W**T 
					//* 
					
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 C2 := C2 - V2 * W**T 
						//* 
						
						_5nsxi69c("No transpose" ,"Transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_ycxba85s+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V1**T 
					//* 
					
					_birntqim("Right" ,"Lower" ,"Transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C1 := C1 - W**T 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn417 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step417 = (System.Int32)((int)1);
						System.Int32 __81fgg2count417;
						for (__81fgg2count417 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn417 + __81fgg2step417) / __81fgg2step417)), _znpjgsef = __81fgg2dlsvn417; __81fgg2count417 != 0; __81fgg2count417--, _znpjgsef += (__81fgg2step417)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn418 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step418 = (System.Int32)((int)1);
								System.Int32 __81fgg2count418;
								for (__81fgg2count418 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn418 + __81fgg2step418) / __81fgg2step418)), _b5p6od9s = __81fgg2dlsvn418; __81fgg2count418 != 0; __81fgg2count418--, _b5p6od9s += (__81fgg2step418)) {

								{
									
									*(_3crf0qn3+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_1s3eymp4)) - *(_apig8meb+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)));
Mark20:;
									// continue
								}
																}							}
Mark30:;
							// continue
						}
												}					}//* 
					
				}
				else
				if (_w8y2rzgy(_m2cn2gjg ,"R" ))
				{
					//* 
					//*              Form  C * H  or  C * H**T  where  C = ( C1  C2 ) 
					//* 
					//*              W := C * V  =  (C1*V1 + C2*V2)  (stored in WORK) 
					//* 
					//*              W := C1 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn419 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step419 = (System.Int32)((int)1);
						System.Int32 __81fgg2count419;
						for (__81fgg2count419 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn419 + __81fgg2step419) / __81fgg2step419)), _znpjgsef = __81fgg2dlsvn419; __81fgg2count419 != 0; __81fgg2count419--, _znpjgsef += (__81fgg2step419)) {

						{
							
							_gvjhlct0(ref _ev4xhht5 ,(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark40:;
							// continue
						}
												}					}//* 
					//*              W := W * V1 
					//* 
					
					_birntqim("Right" ,"Lower" ,"No transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 W := W + C2 * V2 
						//* 
						
						_5nsxi69c("No transpose" ,"No transpose" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_ycxba85s+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T  or  W * T**T 
					//* 
					
					_birntqim("Right" ,"Upper" ,_scuo79v4 ,"Non-unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - W * V**T 
					//* 
					
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 C2 := C2 - W * V2**T 
						//* 
						
						_5nsxi69c("No transpose" ,"Transpose" ,ref _ev4xhht5 ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_apig8meb ,ref _iykhdriq ,(_ycxba85s+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V1**T 
					//* 
					
					_birntqim("Right" ,"Lower" ,"Transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C1 := C1 - W 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn420 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step420 = (System.Int32)((int)1);
						System.Int32 __81fgg2count420;
						for (__81fgg2count420 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn420 + __81fgg2step420) / __81fgg2step420)), _znpjgsef = __81fgg2dlsvn420; __81fgg2count420 != 0; __81fgg2count420--, _znpjgsef += (__81fgg2step420)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn421 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step421 = (System.Int32)((int)1);
								System.Int32 __81fgg2count421;
								for (__81fgg2count421 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn421 + __81fgg2step421) / __81fgg2step421)), _b5p6od9s = __81fgg2dlsvn421; __81fgg2count421 != 0; __81fgg2count421--, _b5p6od9s += (__81fgg2step421)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - *(_apig8meb+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)));
Mark50:;
									// continue
								}
																}							}
Mark60:;
							// continue
						}
												}					}
				}
				//* 
				
			}
			else
			{
				//* 
				//*           Let  V =  ( V1 ) 
				//*                     ( V2 )    (last K rows) 
				//*           where  V2  is unit upper triangular. 
				//* 
				
				if (_w8y2rzgy(_m2cn2gjg ,"L" ))
				{
					//* 
					//*              Form  H * C  or  H**T * C  where  C = ( C1 ) 
					//*                                                    ( C2 ) 
					//* 
					//*              W := C**T * V  =  (C1**T * V1 + C2**T * V2)  (stored in WORK) 
					//* 
					//*              W := C2**T 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn422 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step422 = (System.Int32)((int)1);
						System.Int32 __81fgg2count422;
						for (__81fgg2count422 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn422 + __81fgg2step422) / __81fgg2step422)), _znpjgsef = __81fgg2dlsvn422; __81fgg2count422 != 0; __81fgg2count422--, _znpjgsef += (__81fgg2step422)) {

						{
							
							_gvjhlct0(ref _dxpq0xkr ,(_3crf0qn3+((_ev4xhht5 - _umlkckdg) + _znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark70:;
							// continue
						}
												}					}//* 
					//*              W := W * V2 
					//* 
					
					_birntqim("Right" ,"Upper" ,"No transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((_ev4xhht5 - _umlkckdg) + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 W := W + C1**T * V1 
						//* 
						
						_5nsxi69c("Transpose" ,"No transpose" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T**T  or  W * T 
					//* 
					
					_birntqim("Right" ,"Lower" ,_l09pt3ga ,"Non-unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - V * W**T 
					//* 
					
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 C1 := C1 - V1 * W**T 
						//* 
						
						_5nsxi69c("No transpose" ,"Transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V2**T 
					//* 
					
					_birntqim("Right" ,"Upper" ,"Transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((_ev4xhht5 - _umlkckdg) + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C2 := C2 - W**T 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn423 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step423 = (System.Int32)((int)1);
						System.Int32 __81fgg2count423;
						for (__81fgg2count423 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn423 + __81fgg2step423) / __81fgg2step423)), _znpjgsef = __81fgg2dlsvn423; __81fgg2count423 != 0; __81fgg2count423--, _znpjgsef += (__81fgg2step423)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn424 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step424 = (System.Int32)((int)1);
								System.Int32 __81fgg2count424;
								for (__81fgg2count424 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn424 + __81fgg2step424) / __81fgg2step424)), _b5p6od9s = __81fgg2dlsvn424; __81fgg2count424 != 0; __81fgg2count424--, _b5p6od9s += (__81fgg2step424)) {

								{
									
									*(_3crf0qn3+((_ev4xhht5 - _umlkckdg) + _znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((_ev4xhht5 - _umlkckdg) + _znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_1s3eymp4)) - *(_apig8meb+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)));
Mark80:;
									// continue
								}
																}							}
Mark90:;
							// continue
						}
												}					}//* 
					
				}
				else
				if (_w8y2rzgy(_m2cn2gjg ,"R" ))
				{
					//* 
					//*              Form  C * H  or  C * H**T  where  C = ( C1  C2 ) 
					//* 
					//*              W := C * V  =  (C1*V1 + C2*V2)  (stored in WORK) 
					//* 
					//*              W := C2 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn425 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step425 = (System.Int32)((int)1);
						System.Int32 __81fgg2count425;
						for (__81fgg2count425 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn425 + __81fgg2step425) / __81fgg2step425)), _znpjgsef = __81fgg2dlsvn425; __81fgg2count425 != 0; __81fgg2count425--, _znpjgsef += (__81fgg2step425)) {

						{
							
							_gvjhlct0(ref _ev4xhht5 ,(_3crf0qn3+((int)1 - 1) + ((_dxpq0xkr - _umlkckdg) + _znpjgsef - 1) * 1 * (_1s3eymp4)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark100:;
							// continue
						}
												}					}//* 
					//*              W := W * V2 
					//* 
					
					_birntqim("Right" ,"Upper" ,"No transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((_dxpq0xkr - _umlkckdg) + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 W := W + C1 * V1 
						//* 
						
						_5nsxi69c("No transpose" ,"No transpose" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T  or  W * T**T 
					//* 
					
					_birntqim("Right" ,"Lower" ,_scuo79v4 ,"Non-unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - W * V**T 
					//* 
					
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 C1 := C1 - W * V1**T 
						//* 
						
						_5nsxi69c("No transpose" ,"Transpose" ,ref _ev4xhht5 ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_apig8meb ,ref _iykhdriq ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V2**T 
					//* 
					
					_birntqim("Right" ,"Upper" ,"Transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((_dxpq0xkr - _umlkckdg) + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C2 := C2 - W 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn426 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step426 = (System.Int32)((int)1);
						System.Int32 __81fgg2count426;
						for (__81fgg2count426 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn426 + __81fgg2step426) / __81fgg2step426)), _znpjgsef = __81fgg2dlsvn426; __81fgg2count426 != 0; __81fgg2count426--, _znpjgsef += (__81fgg2step426)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn427 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step427 = (System.Int32)((int)1);
								System.Int32 __81fgg2count427;
								for (__81fgg2count427 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn427 + __81fgg2step427) / __81fgg2step427)), _b5p6od9s = __81fgg2dlsvn427; __81fgg2count427 != 0; __81fgg2count427--, _b5p6od9s += (__81fgg2step427)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + ((_dxpq0xkr - _umlkckdg) + _znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_b5p6od9s - 1) + ((_dxpq0xkr - _umlkckdg) + _znpjgsef - 1) * 1 * (_1s3eymp4)) - *(_apig8meb+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)));
Mark110:;
									// continue
								}
																}							}
Mark120:;
							// continue
						}
												}					}
				}
				
			}
			//* 
			
		}
		else
		if (_w8y2rzgy(_tjtvdgd6 ,"R" ))
		{
			//* 
			
			if (_w8y2rzgy(_uw10mx43 ,"F" ))
			{
				//* 
				//*           Let  V =  ( V1  V2 )    (V1: first K columns) 
				//*           where  V1  is unit upper triangular. 
				//* 
				
				if (_w8y2rzgy(_m2cn2gjg ,"L" ))
				{
					//* 
					//*              Form  H * C  or  H**T * C  where  C = ( C1 ) 
					//*                                                    ( C2 ) 
					//* 
					//*              W := C**T * V**T  =  (C1**T * V1**T + C2**T * V2**T) (stored in WORK) 
					//* 
					//*              W := C1**T 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn428 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step428 = (System.Int32)((int)1);
						System.Int32 __81fgg2count428;
						for (__81fgg2count428 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn428 + __81fgg2step428) / __81fgg2step428)), _znpjgsef = __81fgg2dlsvn428; __81fgg2count428 != 0; __81fgg2count428--, _znpjgsef += (__81fgg2step428)) {

						{
							
							_gvjhlct0(ref _dxpq0xkr ,(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark130:;
							// continue
						}
												}					}//* 
					//*              W := W * V1**T 
					//* 
					
					_birntqim("Right" ,"Upper" ,"Transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 W := W + C2**T * V2**T 
						//* 
						
						_5nsxi69c("Transpose" ,"Transpose" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_ycxba85s+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T**T  or  W * T 
					//* 
					
					_birntqim("Right" ,"Upper" ,_l09pt3ga ,"Non-unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - V**T * W**T 
					//* 
					
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 C2 := C2 - V2**T * W**T 
						//* 
						
						_5nsxi69c("Transpose" ,"Transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_ycxba85s+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V1 
					//* 
					
					_birntqim("Right" ,"Upper" ,"No transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C1 := C1 - W**T 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn429 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step429 = (System.Int32)((int)1);
						System.Int32 __81fgg2count429;
						for (__81fgg2count429 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn429 + __81fgg2step429) / __81fgg2step429)), _znpjgsef = __81fgg2dlsvn429; __81fgg2count429 != 0; __81fgg2count429--, _znpjgsef += (__81fgg2step429)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn430 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step430 = (System.Int32)((int)1);
								System.Int32 __81fgg2count430;
								for (__81fgg2count430 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn430 + __81fgg2step430) / __81fgg2step430)), _b5p6od9s = __81fgg2dlsvn430; __81fgg2count430 != 0; __81fgg2count430--, _b5p6od9s += (__81fgg2step430)) {

								{
									
									*(_3crf0qn3+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_1s3eymp4)) - *(_apig8meb+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)));
Mark140:;
									// continue
								}
																}							}
Mark150:;
							// continue
						}
												}					}//* 
					
				}
				else
				if (_w8y2rzgy(_m2cn2gjg ,"R" ))
				{
					//* 
					//*              Form  C * H  or  C * H**T  where  C = ( C1  C2 ) 
					//* 
					//*              W := C * V**T  =  (C1*V1**T + C2*V2**T)  (stored in WORK) 
					//* 
					//*              W := C1 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn431 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step431 = (System.Int32)((int)1);
						System.Int32 __81fgg2count431;
						for (__81fgg2count431 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn431 + __81fgg2step431) / __81fgg2step431)), _znpjgsef = __81fgg2dlsvn431; __81fgg2count431 != 0; __81fgg2count431--, _znpjgsef += (__81fgg2step431)) {

						{
							
							_gvjhlct0(ref _ev4xhht5 ,(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark160:;
							// continue
						}
												}					}//* 
					//*              W := W * V1**T 
					//* 
					
					_birntqim("Right" ,"Upper" ,"Transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 W := W + C2 * V2**T 
						//* 
						
						_5nsxi69c("No transpose" ,"Transpose" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_ycxba85s+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T  or  W * T**T 
					//* 
					
					_birntqim("Right" ,"Upper" ,_scuo79v4 ,"Non-unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - W * V 
					//* 
					
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 C2 := C2 - W * V2 
						//* 
						
						_5nsxi69c("No transpose" ,"No transpose" ,ref _ev4xhht5 ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_apig8meb ,ref _iykhdriq ,(_ycxba85s+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V1 
					//* 
					
					_birntqim("Right" ,"Upper" ,"No transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C1 := C1 - W 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn432 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step432 = (System.Int32)((int)1);
						System.Int32 __81fgg2count432;
						for (__81fgg2count432 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn432 + __81fgg2step432) / __81fgg2step432)), _znpjgsef = __81fgg2dlsvn432; __81fgg2count432 != 0; __81fgg2count432--, _znpjgsef += (__81fgg2step432)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn433 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step433 = (System.Int32)((int)1);
								System.Int32 __81fgg2count433;
								for (__81fgg2count433 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn433 + __81fgg2step433) / __81fgg2step433)), _b5p6od9s = __81fgg2dlsvn433; __81fgg2count433 != 0; __81fgg2count433--, _b5p6od9s += (__81fgg2step433)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - *(_apig8meb+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)));
Mark170:;
									// continue
								}
																}							}
Mark180:;
							// continue
						}
												}					}//* 
					
				}
				//* 
				
			}
			else
			{
				//* 
				//*           Let  V =  ( V1  V2 )    (V2: last K columns) 
				//*           where  V2  is unit lower triangular. 
				//* 
				
				if (_w8y2rzgy(_m2cn2gjg ,"L" ))
				{
					//* 
					//*              Form  H * C  or  H**T * C  where  C = ( C1 ) 
					//*                                                    ( C2 ) 
					//* 
					//*              W := C**T * V**T  =  (C1**T * V1**T + C2**T * V2**T) (stored in WORK) 
					//* 
					//*              W := C2**T 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn434 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step434 = (System.Int32)((int)1);
						System.Int32 __81fgg2count434;
						for (__81fgg2count434 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn434 + __81fgg2step434) / __81fgg2step434)), _znpjgsef = __81fgg2dlsvn434; __81fgg2count434 != 0; __81fgg2count434--, _znpjgsef += (__81fgg2step434)) {

						{
							
							_gvjhlct0(ref _dxpq0xkr ,(_3crf0qn3+((_ev4xhht5 - _umlkckdg) + _znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark190:;
							// continue
						}
												}					}//* 
					//*              W := W * V2**T 
					//* 
					
					_birntqim("Right" ,"Lower" ,"Transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((int)1 - 1) + ((_ev4xhht5 - _umlkckdg) + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 W := W + C1**T * V1**T 
						//* 
						
						_5nsxi69c("Transpose" ,"Transpose" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T**T  or  W * T 
					//* 
					
					_birntqim("Right" ,"Lower" ,_l09pt3ga ,"Non-unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - V**T * W**T 
					//* 
					
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 C1 := C1 - V1**T * W**T 
						//* 
						
						_5nsxi69c("Transpose" ,"Transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V2 
					//* 
					
					_birntqim("Right" ,"Lower" ,"No transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((int)1 - 1) + ((_ev4xhht5 - _umlkckdg) + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C2 := C2 - W**T 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn435 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step435 = (System.Int32)((int)1);
						System.Int32 __81fgg2count435;
						for (__81fgg2count435 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn435 + __81fgg2step435) / __81fgg2step435)), _znpjgsef = __81fgg2dlsvn435; __81fgg2count435 != 0; __81fgg2count435--, _znpjgsef += (__81fgg2step435)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn436 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step436 = (System.Int32)((int)1);
								System.Int32 __81fgg2count436;
								for (__81fgg2count436 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn436 + __81fgg2step436) / __81fgg2step436)), _b5p6od9s = __81fgg2dlsvn436; __81fgg2count436 != 0; __81fgg2count436--, _b5p6od9s += (__81fgg2step436)) {

								{
									
									*(_3crf0qn3+((_ev4xhht5 - _umlkckdg) + _znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((_ev4xhht5 - _umlkckdg) + _znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_1s3eymp4)) - *(_apig8meb+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)));
Mark200:;
									// continue
								}
																}							}
Mark210:;
							// continue
						}
												}					}//* 
					
				}
				else
				if (_w8y2rzgy(_m2cn2gjg ,"R" ))
				{
					//* 
					//*              Form  C * H  or  C * H'  where  C = ( C1  C2 ) 
					//* 
					//*              W := C * V**T  =  (C1*V1**T + C2*V2**T)  (stored in WORK) 
					//* 
					//*              W := C2 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn437 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step437 = (System.Int32)((int)1);
						System.Int32 __81fgg2count437;
						for (__81fgg2count437 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn437 + __81fgg2step437) / __81fgg2step437)), _znpjgsef = __81fgg2dlsvn437; __81fgg2count437 != 0; __81fgg2count437--, _znpjgsef += (__81fgg2step437)) {

						{
							
							_gvjhlct0(ref _ev4xhht5 ,(_3crf0qn3+((int)1 - 1) + ((_dxpq0xkr - _umlkckdg) + _znpjgsef - 1) * 1 * (_1s3eymp4)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark220:;
							// continue
						}
												}					}//* 
					//*              W := W * V2**T 
					//* 
					
					_birntqim("Right" ,"Lower" ,"Transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((int)1 - 1) + ((_dxpq0xkr - _umlkckdg) + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 W := W + C1 * V1**T 
						//* 
						
						_5nsxi69c("No transpose" ,"Transpose" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T  or  W * T**T 
					//* 
					
					_birntqim("Right" ,"Lower" ,_scuo79v4 ,"Non-unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - W * V 
					//* 
					
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 C1 := C1 - W * V1 
						//* 
						
						_5nsxi69c("No transpose" ,"No transpose" ,ref _ev4xhht5 ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_apig8meb ,ref _iykhdriq ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V2 
					//* 
					
					_birntqim("Right" ,"Lower" ,"No transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((int)1 - 1) + ((_dxpq0xkr - _umlkckdg) + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C1 := C1 - W 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn438 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step438 = (System.Int32)((int)1);
						System.Int32 __81fgg2count438;
						for (__81fgg2count438 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn438 + __81fgg2step438) / __81fgg2step438)), _znpjgsef = __81fgg2dlsvn438; __81fgg2count438 != 0; __81fgg2count438--, _znpjgsef += (__81fgg2step438)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn439 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step439 = (System.Int32)((int)1);
								System.Int32 __81fgg2count439;
								for (__81fgg2count439 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn439 + __81fgg2step439) / __81fgg2step439)), _b5p6od9s = __81fgg2dlsvn439; __81fgg2count439 != 0; __81fgg2count439--, _b5p6od9s += (__81fgg2step439)) {

								{
									
									*(_3crf0qn3+(_b5p6od9s - 1) + ((_dxpq0xkr - _umlkckdg) + _znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_b5p6od9s - 1) + ((_dxpq0xkr - _umlkckdg) + _znpjgsef - 1) * 1 * (_1s3eymp4)) - *(_apig8meb+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)));
Mark230:;
									// continue
								}
																}							}
Mark240:;
							// continue
						}
												}					}//* 
					
				}
				//* 
				
			}
			
		}
		//* 
		
		return;//* 
		//*     End of DLARFB 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
