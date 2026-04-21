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
//*> \brief \b ZLARFB applies a block reflector or its conjugate-transpose to a general rectangular matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZLARFB + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zlarfb.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zlarfb.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zlarfb.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZLARFB( SIDE, TRANS, DIRECT, STOREV, M, N, K, V, LDV, 
//*                          T, LDT, C, LDC, WORK, LDWORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          DIRECT, SIDE, STOREV, TRANS 
//*       INTEGER            K, LDC, LDT, LDV, LDWORK, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX*16         C( LDC, * ), T( LDT, * ), V( LDV, * ), 
//*      $                   WORK( LDWORK, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZLARFB applies a complex block reflector H or its transpose H**H to a 
//*> complex M-by-N matrix C, from either the left or the right. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] SIDE 
//*> \verbatim 
//*>          SIDE is CHARACTER*1 
//*>          = 'L': apply H or H**H from the Left 
//*>          = 'R': apply H or H**H from the Right 
//*> \endverbatim 
//*> 
//*> \param[in] TRANS 
//*> \verbatim 
//*>          TRANS is CHARACTER*1 
//*>          = 'N': apply H (No transpose) 
//*>          = 'C': apply H**H (Conjugate transpose) 
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
//*>          V is COMPLEX*16 array, dimension 
//*>                                (LDV,K) if STOREV = 'C' 
//*>                                (LDV,M) if STOREV = 'R' and SIDE = 'L' 
//*>                                (LDV,N) if STOREV = 'R' and SIDE = 'R' 
//*>          See Further Details. 
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
//*>          T is COMPLEX*16 array, dimension (LDT,K) 
//*>          The triangular K-by-K matrix T in the representation of the 
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
//*>          C is COMPLEX*16 array, dimension (LDC,N) 
//*>          On entry, the M-by-N matrix C. 
//*>          On exit, C is overwritten by H*C or H**H*C or C*H or C*H**H. 
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
//*>          WORK is COMPLEX*16 array, dimension (LDWORK,K) 
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
//*> \ingroup complex16OTHERauxiliary 
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

	 
	public static void _k44wg3oj(FString _m2cn2gjg, FString _scuo79v4, FString _uw10mx43, FString _tjtvdgd6, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, complex* _ycxba85s, ref Int32 _ys09rxze, complex* _2ivtt43r, ref Int32 _w8yhbr2r, complex* _3crf0qn3, ref Int32 _1s3eymp4, complex* _apig8meb, ref Int32 _iykhdriq)
	{
#region variable declarations
complex _kxg5drh2 =   new fcomplex(1f,0f);
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
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Quick return if possible 
		//* 
		
		if ((_ev4xhht5 <= (int)0) | (_dxpq0xkr <= (int)0))
		return;//* 
		
		if (_w8y2rzgy(_scuo79v4 ,"N" ))
		{
			
			
			_l09pt3ga = "C";
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
					//*              Form  H * C  or  H**H * C  where  C = ( C1 ) 
					//*                                                    ( C2 ) 
					//* 
					//*              W := C**H * V  =  (C1**H * V1 + C2**H * V2)  (stored in WORK) 
					//* 
					//*              W := C1**H 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn1173 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1173 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1173;
						for (__81fgg2count1173 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1173 + __81fgg2step1173) / __81fgg2step1173)), _znpjgsef = __81fgg2dlsvn1173; __81fgg2count1173 != 0; __81fgg2count1173--, _znpjgsef += (__81fgg2step1173)) {

						{
							
							_ly902k7t(ref _dxpq0xkr ,(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
							_42wgkyoq(ref _dxpq0xkr ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark10:;
							// continue
						}
												}					}//* 
					//*              W := W * V1 
					//* 
					
					_dbxixtiz("Right" ,"Lower" ,"No transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 W := W + C2**H * V2 
						//* 
						
						_xos1d1er("Conjugate transpose" ,"No transpose" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_ycxba85s+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T**H  or  W * T 
					//* 
					
					_dbxixtiz("Right" ,"Upper" ,_l09pt3ga ,"Non-unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - V * W**H 
					//* 
					
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 C2 := C2 - V2 * W**H 
						//* 
						
						_xos1d1er("No transpose" ,"Conjugate transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_ycxba85s+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V1**H 
					//* 
					
					_dbxixtiz("Right" ,"Lower" ,"Conjugate transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C1 := C1 - W**H 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn1174 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1174 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1174;
						for (__81fgg2count1174 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1174 + __81fgg2step1174) / __81fgg2step1174)), _znpjgsef = __81fgg2dlsvn1174; __81fgg2count1174 != 0; __81fgg2count1174--, _znpjgsef += (__81fgg2step1174)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1175 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1175 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1175;
								for (__81fgg2count1175 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1175 + __81fgg2step1175) / __81fgg2step1175)), _b5p6od9s = __81fgg2dlsvn1175; __81fgg2count1175 != 0; __81fgg2count1175--, _b5p6od9s += (__81fgg2step1175)) {

								{
									
									*(_3crf0qn3+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_1s3eymp4)) - ILNumerics.F2NET.Intrinsics.DCONJG(*(_apig8meb+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)) ));
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
					//*              Form  C * H  or  C * H**H  where  C = ( C1  C2 ) 
					//* 
					//*              W := C * V  =  (C1*V1 + C2*V2)  (stored in WORK) 
					//* 
					//*              W := C1 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn1176 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1176 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1176;
						for (__81fgg2count1176 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1176 + __81fgg2step1176) / __81fgg2step1176)), _znpjgsef = __81fgg2dlsvn1176; __81fgg2count1176 != 0; __81fgg2count1176--, _znpjgsef += (__81fgg2step1176)) {

						{
							
							_ly902k7t(ref _ev4xhht5 ,(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark40:;
							// continue
						}
												}					}//* 
					//*              W := W * V1 
					//* 
					
					_dbxixtiz("Right" ,"Lower" ,"No transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 W := W + C2 * V2 
						//* 
						
						_xos1d1er("No transpose" ,"No transpose" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_ycxba85s+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T  or  W * T**H 
					//* 
					
					_dbxixtiz("Right" ,"Upper" ,_scuo79v4 ,"Non-unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - W * V**H 
					//* 
					
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 C2 := C2 - W * V2**H 
						//* 
						
						_xos1d1er("No transpose" ,"Conjugate transpose" ,ref _ev4xhht5 ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_apig8meb ,ref _iykhdriq ,(_ycxba85s+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V1**H 
					//* 
					
					_dbxixtiz("Right" ,"Lower" ,"Conjugate transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C1 := C1 - W 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn1177 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1177 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1177;
						for (__81fgg2count1177 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1177 + __81fgg2step1177) / __81fgg2step1177)), _znpjgsef = __81fgg2dlsvn1177; __81fgg2count1177 != 0; __81fgg2count1177--, _znpjgsef += (__81fgg2step1177)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1178 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1178 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1178;
								for (__81fgg2count1178 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1178 + __81fgg2step1178) / __81fgg2step1178)), _b5p6od9s = __81fgg2dlsvn1178; __81fgg2count1178 != 0; __81fgg2count1178--, _b5p6od9s += (__81fgg2step1178)) {

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
					//*              Form  H * C  or  H**H * C  where  C = ( C1 ) 
					//*                                                    ( C2 ) 
					//* 
					//*              W := C**H * V  =  (C1**H * V1 + C2**H * V2)  (stored in WORK) 
					//* 
					//*              W := C2**H 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn1179 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1179 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1179;
						for (__81fgg2count1179 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1179 + __81fgg2step1179) / __81fgg2step1179)), _znpjgsef = __81fgg2dlsvn1179; __81fgg2count1179 != 0; __81fgg2count1179--, _znpjgsef += (__81fgg2step1179)) {

						{
							
							_ly902k7t(ref _dxpq0xkr ,(_3crf0qn3+((_ev4xhht5 - _umlkckdg) + _znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
							_42wgkyoq(ref _dxpq0xkr ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark70:;
							// continue
						}
												}					}//* 
					//*              W := W * V2 
					//* 
					
					_dbxixtiz("Right" ,"Upper" ,"No transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((_ev4xhht5 - _umlkckdg) + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 W := W + C1**H * V1 
						//* 
						
						_xos1d1er("Conjugate transpose" ,"No transpose" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T**H  or  W * T 
					//* 
					
					_dbxixtiz("Right" ,"Lower" ,_l09pt3ga ,"Non-unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - V * W**H 
					//* 
					
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 C1 := C1 - V1 * W**H 
						//* 
						
						_xos1d1er("No transpose" ,"Conjugate transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V2**H 
					//* 
					
					_dbxixtiz("Right" ,"Upper" ,"Conjugate transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((_ev4xhht5 - _umlkckdg) + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C2 := C2 - W**H 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn1180 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1180 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1180;
						for (__81fgg2count1180 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1180 + __81fgg2step1180) / __81fgg2step1180)), _znpjgsef = __81fgg2dlsvn1180; __81fgg2count1180 != 0; __81fgg2count1180--, _znpjgsef += (__81fgg2step1180)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1181 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1181 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1181;
								for (__81fgg2count1181 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1181 + __81fgg2step1181) / __81fgg2step1181)), _b5p6od9s = __81fgg2dlsvn1181; __81fgg2count1181 != 0; __81fgg2count1181--, _b5p6od9s += (__81fgg2step1181)) {

								{
									
									*(_3crf0qn3+((_ev4xhht5 - _umlkckdg) + _znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((_ev4xhht5 - _umlkckdg) + _znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_1s3eymp4)) - ILNumerics.F2NET.Intrinsics.DCONJG(*(_apig8meb+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)) ));
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
					//*              Form  C * H  or  C * H**H  where  C = ( C1  C2 ) 
					//* 
					//*              W := C * V  =  (C1*V1 + C2*V2)  (stored in WORK) 
					//* 
					//*              W := C2 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn1182 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1182 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1182;
						for (__81fgg2count1182 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1182 + __81fgg2step1182) / __81fgg2step1182)), _znpjgsef = __81fgg2dlsvn1182; __81fgg2count1182 != 0; __81fgg2count1182--, _znpjgsef += (__81fgg2step1182)) {

						{
							
							_ly902k7t(ref _ev4xhht5 ,(_3crf0qn3+((int)1 - 1) + ((_dxpq0xkr - _umlkckdg) + _znpjgsef - 1) * 1 * (_1s3eymp4)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark100:;
							// continue
						}
												}					}//* 
					//*              W := W * V2 
					//* 
					
					_dbxixtiz("Right" ,"Upper" ,"No transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((_dxpq0xkr - _umlkckdg) + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 W := W + C1 * V1 
						//* 
						
						_xos1d1er("No transpose" ,"No transpose" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T  or  W * T**H 
					//* 
					
					_dbxixtiz("Right" ,"Lower" ,_scuo79v4 ,"Non-unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - W * V**H 
					//* 
					
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 C1 := C1 - W * V1**H 
						//* 
						
						_xos1d1er("No transpose" ,"Conjugate transpose" ,ref _ev4xhht5 ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_apig8meb ,ref _iykhdriq ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V2**H 
					//* 
					
					_dbxixtiz("Right" ,"Upper" ,"Conjugate transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((_dxpq0xkr - _umlkckdg) + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C2 := C2 - W 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn1183 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1183 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1183;
						for (__81fgg2count1183 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1183 + __81fgg2step1183) / __81fgg2step1183)), _znpjgsef = __81fgg2dlsvn1183; __81fgg2count1183 != 0; __81fgg2count1183--, _znpjgsef += (__81fgg2step1183)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1184 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1184 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1184;
								for (__81fgg2count1184 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1184 + __81fgg2step1184) / __81fgg2step1184)), _b5p6od9s = __81fgg2dlsvn1184; __81fgg2count1184 != 0; __81fgg2count1184--, _b5p6od9s += (__81fgg2step1184)) {

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
					//*              Form  H * C  or  H**H * C  where  C = ( C1 ) 
					//*                                                    ( C2 ) 
					//* 
					//*              W := C**H * V**H  =  (C1**H * V1**H + C2**H * V2**H) (stored in WORK) 
					//* 
					//*              W := C1**H 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn1185 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1185 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1185;
						for (__81fgg2count1185 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1185 + __81fgg2step1185) / __81fgg2step1185)), _znpjgsef = __81fgg2dlsvn1185; __81fgg2count1185 != 0; __81fgg2count1185--, _znpjgsef += (__81fgg2step1185)) {

						{
							
							_ly902k7t(ref _dxpq0xkr ,(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
							_42wgkyoq(ref _dxpq0xkr ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark130:;
							// continue
						}
												}					}//* 
					//*              W := W * V1**H 
					//* 
					
					_dbxixtiz("Right" ,"Upper" ,"Conjugate transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 W := W + C2**H * V2**H 
						//* 
						
						_xos1d1er("Conjugate transpose" ,"Conjugate transpose" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_ycxba85s+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T**H  or  W * T 
					//* 
					
					_dbxixtiz("Right" ,"Upper" ,_l09pt3ga ,"Non-unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - V**H * W**H 
					//* 
					
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 C2 := C2 - V2**H * W**H 
						//* 
						
						_xos1d1er("Conjugate transpose" ,"Conjugate transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_ycxba85s+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V1 
					//* 
					
					_dbxixtiz("Right" ,"Upper" ,"No transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C1 := C1 - W**H 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn1186 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1186 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1186;
						for (__81fgg2count1186 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1186 + __81fgg2step1186) / __81fgg2step1186)), _znpjgsef = __81fgg2dlsvn1186; __81fgg2count1186 != 0; __81fgg2count1186--, _znpjgsef += (__81fgg2step1186)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1187 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1187 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1187;
								for (__81fgg2count1187 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1187 + __81fgg2step1187) / __81fgg2step1187)), _b5p6od9s = __81fgg2dlsvn1187; __81fgg2count1187 != 0; __81fgg2count1187--, _b5p6od9s += (__81fgg2step1187)) {

								{
									
									*(_3crf0qn3+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_1s3eymp4)) - ILNumerics.F2NET.Intrinsics.DCONJG(*(_apig8meb+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)) ));
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
					//*              Form  C * H  or  C * H**H  where  C = ( C1  C2 ) 
					//* 
					//*              W := C * V**H  =  (C1*V1**H + C2*V2**H)  (stored in WORK) 
					//* 
					//*              W := C1 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn1188 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1188 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1188;
						for (__81fgg2count1188 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1188 + __81fgg2step1188) / __81fgg2step1188)), _znpjgsef = __81fgg2dlsvn1188; __81fgg2count1188 != 0; __81fgg2count1188--, _znpjgsef += (__81fgg2step1188)) {

						{
							
							_ly902k7t(ref _ev4xhht5 ,(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark160:;
							// continue
						}
												}					}//* 
					//*              W := W * V1**H 
					//* 
					
					_dbxixtiz("Right" ,"Upper" ,"Conjugate transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 W := W + C2 * V2**H 
						//* 
						
						_xos1d1er("No transpose" ,"Conjugate transpose" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_ycxba85s+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T  or  W * T**H 
					//* 
					
					_dbxixtiz("Right" ,"Upper" ,_scuo79v4 ,"Non-unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - W * V 
					//* 
					
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 C2 := C2 - W * V2 
						//* 
						
						_xos1d1er("No transpose" ,"No transpose" ,ref _ev4xhht5 ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_apig8meb ,ref _iykhdriq ,(_ycxba85s+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V1 
					//* 
					
					_dbxixtiz("Right" ,"Upper" ,"No transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C1 := C1 - W 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn1189 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1189 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1189;
						for (__81fgg2count1189 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1189 + __81fgg2step1189) / __81fgg2step1189)), _znpjgsef = __81fgg2dlsvn1189; __81fgg2count1189 != 0; __81fgg2count1189--, _znpjgsef += (__81fgg2step1189)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1190 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1190 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1190;
								for (__81fgg2count1190 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1190 + __81fgg2step1190) / __81fgg2step1190)), _b5p6od9s = __81fgg2dlsvn1190; __81fgg2count1190 != 0; __81fgg2count1190--, _b5p6od9s += (__81fgg2step1190)) {

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
					//*              Form  H * C  or  H**H * C  where  C = ( C1 ) 
					//*                                                    ( C2 ) 
					//* 
					//*              W := C**H * V**H  =  (C1**H * V1**H + C2**H * V2**H) (stored in WORK) 
					//* 
					//*              W := C2**H 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn1191 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1191 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1191;
						for (__81fgg2count1191 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1191 + __81fgg2step1191) / __81fgg2step1191)), _znpjgsef = __81fgg2dlsvn1191; __81fgg2count1191 != 0; __81fgg2count1191--, _znpjgsef += (__81fgg2step1191)) {

						{
							
							_ly902k7t(ref _dxpq0xkr ,(_3crf0qn3+((_ev4xhht5 - _umlkckdg) + _znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
							_42wgkyoq(ref _dxpq0xkr ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark190:;
							// continue
						}
												}					}//* 
					//*              W := W * V2**H 
					//* 
					
					_dbxixtiz("Right" ,"Lower" ,"Conjugate transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((int)1 - 1) + ((_ev4xhht5 - _umlkckdg) + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 W := W + C1**H * V1**H 
						//* 
						
						_xos1d1er("Conjugate transpose" ,"Conjugate transpose" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T**H  or  W * T 
					//* 
					
					_dbxixtiz("Right" ,"Lower" ,_l09pt3ga ,"Non-unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - V**H * W**H 
					//* 
					
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 C1 := C1 - V1**H * W**H 
						//* 
						
						_xos1d1er("Conjugate transpose" ,"Conjugate transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V2 
					//* 
					
					_dbxixtiz("Right" ,"Lower" ,"No transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((int)1 - 1) + ((_ev4xhht5 - _umlkckdg) + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C2 := C2 - W**H 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn1192 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1192 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1192;
						for (__81fgg2count1192 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1192 + __81fgg2step1192) / __81fgg2step1192)), _znpjgsef = __81fgg2dlsvn1192; __81fgg2count1192 != 0; __81fgg2count1192--, _znpjgsef += (__81fgg2step1192)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1193 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1193 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1193;
								for (__81fgg2count1193 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1193 + __81fgg2step1193) / __81fgg2step1193)), _b5p6od9s = __81fgg2dlsvn1193; __81fgg2count1193 != 0; __81fgg2count1193--, _b5p6od9s += (__81fgg2step1193)) {

								{
									
									*(_3crf0qn3+((_ev4xhht5 - _umlkckdg) + _znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((_ev4xhht5 - _umlkckdg) + _znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_1s3eymp4)) - ILNumerics.F2NET.Intrinsics.DCONJG(*(_apig8meb+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)) ));
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
					//*              Form  C * H  or  C * H**H  where  C = ( C1  C2 ) 
					//* 
					//*              W := C * V**H  =  (C1*V1**H + C2*V2**H)  (stored in WORK) 
					//* 
					//*              W := C2 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn1194 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1194 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1194;
						for (__81fgg2count1194 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1194 + __81fgg2step1194) / __81fgg2step1194)), _znpjgsef = __81fgg2dlsvn1194; __81fgg2count1194 != 0; __81fgg2count1194--, _znpjgsef += (__81fgg2step1194)) {

						{
							
							_ly902k7t(ref _ev4xhht5 ,(_3crf0qn3+((int)1 - 1) + ((_dxpq0xkr - _umlkckdg) + _znpjgsef - 1) * 1 * (_1s3eymp4)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark220:;
							// continue
						}
												}					}//* 
					//*              W := W * V2**H 
					//* 
					
					_dbxixtiz("Right" ,"Lower" ,"Conjugate transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((int)1 - 1) + ((_dxpq0xkr - _umlkckdg) + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 W := W + C1 * V1**H 
						//* 
						
						_xos1d1er("No transpose" ,"Conjugate transpose" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T  or  W * T**H 
					//* 
					
					_dbxixtiz("Right" ,"Lower" ,_scuo79v4 ,"Non-unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - W * V 
					//* 
					
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 C1 := C1 - W * V1 
						//* 
						
						_xos1d1er("No transpose" ,"No transpose" ,ref _ev4xhht5 ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_apig8meb ,ref _iykhdriq ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V2 
					//* 
					
					_dbxixtiz("Right" ,"Lower" ,"No transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((int)1 - 1) + ((_dxpq0xkr - _umlkckdg) + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C1 := C1 - W 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn1195 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1195 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1195;
						for (__81fgg2count1195 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn1195 + __81fgg2step1195) / __81fgg2step1195)), _znpjgsef = __81fgg2dlsvn1195; __81fgg2count1195 != 0; __81fgg2count1195--, _znpjgsef += (__81fgg2step1195)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1196 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1196 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1196;
								for (__81fgg2count1196 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1196 + __81fgg2step1196) / __81fgg2step1196)), _b5p6od9s = __81fgg2dlsvn1196; __81fgg2count1196 != 0; __81fgg2count1196--, _b5p6od9s += (__81fgg2step1196)) {

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
		//*     End of ZLARFB 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
