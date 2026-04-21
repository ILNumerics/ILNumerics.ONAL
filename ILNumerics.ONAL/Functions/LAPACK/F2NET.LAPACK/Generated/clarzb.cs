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
//*> \brief \b CLARZB applies a block reflector or its conjugate-transpose to a general matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CLARZB + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/clarzb.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/clarzb.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/clarzb.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CLARZB( SIDE, TRANS, DIRECT, STOREV, M, N, K, L, V, 
//*                          LDV, T, LDT, C, LDC, WORK, LDWORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          DIRECT, SIDE, STOREV, TRANS 
//*       INTEGER            K, L, LDC, LDT, LDV, LDWORK, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX            C( LDC, * ), T( LDT, * ), V( LDV, * ), 
//*      $                   WORK( LDWORK, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CLARZB applies a complex block reflector H or its transpose H**H 
//*> to a complex distributed M-by-N  C from the left or the right. 
//*> 
//*> Currently, only STOREV = 'R' and DIRECT = 'B' are supported. 
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
//*>          = 'F': H = H(1) H(2) . . . H(k) (Forward, not supported yet) 
//*>          = 'B': H = H(k) . . . H(2) H(1) (Backward) 
//*> \endverbatim 
//*> 
//*> \param[in] STOREV 
//*> \verbatim 
//*>          STOREV is CHARACTER*1 
//*>          Indicates how the vectors which define the elementary 
//*>          reflectors are stored: 
//*>          = 'C': Columnwise                        (not supported yet) 
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
//*> \endverbatim 
//*> 
//*> \param[in] L 
//*> \verbatim 
//*>          L is INTEGER 
//*>          The number of columns of the matrix V containing the 
//*>          meaningful part of the Householder reflectors. 
//*>          If SIDE = 'L', M >= L >= 0, if SIDE = 'R', N >= L >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] V 
//*> \verbatim 
//*>          V is COMPLEX array, dimension (LDV,NV). 
//*>          If STOREV = 'C', NV = K; if STOREV = 'R', NV = L. 
//*> \endverbatim 
//*> 
//*> \param[in] LDV 
//*> \verbatim 
//*>          LDV is INTEGER 
//*>          The leading dimension of the array V. 
//*>          If STOREV = 'C', LDV >= L; if STOREV = 'R', LDV >= K. 
//*> \endverbatim 
//*> 
//*> \param[in] T 
//*> \verbatim 
//*>          T is COMPLEX array, dimension (LDT,K) 
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
//*>          C is COMPLEX array, dimension (LDC,N) 
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
//*>          WORK is COMPLEX array, dimension (LDWORK,K) 
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
//*> \date December 2016 
//* 
//*> \ingroup complexOTHERcomputational 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>    A. Petitet, Computer Science Dept., Univ. of Tenn., Knoxville, USA 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _4c5950dc(FString _m2cn2gjg, FString _scuo79v4, FString _uw10mx43, FString _tjtvdgd6, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, ref Int32 _68ec3gbh, fcomplex* _ycxba85s, ref Int32 _ys09rxze, fcomplex* _2ivtt43r, ref Int32 _w8yhbr2r, fcomplex* _3crf0qn3, ref Int32 _1s3eymp4, fcomplex* _apig8meb, ref Int32 _iykhdriq)
	{
#region variable declarations
fcomplex _kxg5drh2 =   new fcomplex(1f,0f);
FString _l09pt3ga =  new FString(1);
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _znpjgsef =  default;
string fLanavab = default;
#endregion  variable declarations
_m2cn2gjg = _m2cn2gjg.Convert(1);
_scuo79v4 = _scuo79v4.Convert(1);
_uw10mx43 = _uw10mx43.Convert(1);
_tjtvdgd6 = _tjtvdgd6.Convert(1);

	{
		//* 
		//*  -- LAPACK computational routine (version 3.7.0) -- 
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
		//*     Check for currently supported options 
		//* 
		
		_gro5yvfo = (int)0;
		if (!(_w8y2rzgy(_uw10mx43 ,"B" )))
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if (!(_w8y2rzgy(_tjtvdgd6 ,"R" )))
		{
			
			_gro5yvfo = (int)-4;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("CLARZB" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		
		if (_w8y2rzgy(_scuo79v4 ,"N" ))
		{
			
			
			_l09pt3ga = "C";
		}
		else
		{
			
			
			_l09pt3ga = "N";
		}
		//* 
		
		if (_w8y2rzgy(_m2cn2gjg ,"L" ))
		{
			//* 
			//*        Form  H * C  or  H**H * C 
			//* 
			//*        W( 1:n, 1:k ) = C( 1:k, 1:n )**H 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn2123 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2123 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2123;
				for (__81fgg2count2123 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn2123 + __81fgg2step2123) / __81fgg2step2123)), _znpjgsef = __81fgg2dlsvn2123; __81fgg2count2123 != 0; __81fgg2count2123--, _znpjgsef += (__81fgg2step2123)) {

				{
					
					_33e0jk6i(ref _dxpq0xkr ,(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark10:;
					// continue
				}
								}			}//* 
			//*        W( 1:n, 1:k ) = W( 1:n, 1:k ) + ... 
			//*                        C( m-l+1:m, 1:n )**H * V( 1:k, 1:l )**T 
			//* 
			
			if (_68ec3gbh > (int)0)
			_5p0w9905("Transpose" ,"Conjugate transpose" ,ref _dxpq0xkr ,ref _umlkckdg ,ref _68ec3gbh ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+((_ev4xhht5 - _68ec3gbh) + (int)1 - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );//* 
			//*        W( 1:n, 1:k ) = W( 1:n, 1:k ) * T**T  or  W( 1:m, 1:k ) * T 
			//* 
			
			_smeynpn3("Right" ,"Lower" ,_l09pt3ga ,"Non-unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
			//*        C( 1:k, 1:n ) = C( 1:k, 1:n ) - W( 1:n, 1:k )**H 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn2124 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2124 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2124;
				for (__81fgg2count2124 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2124 + __81fgg2step2124) / __81fgg2step2124)), _znpjgsef = __81fgg2dlsvn2124; __81fgg2count2124 != 0; __81fgg2count2124--, _znpjgsef += (__81fgg2step2124)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn2125 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2125 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2125;
						for (__81fgg2count2125 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn2125 + __81fgg2step2125) / __81fgg2step2125)), _b5p6od9s = __81fgg2dlsvn2125; __81fgg2count2125 != 0; __81fgg2count2125--, _b5p6od9s += (__81fgg2step2125)) {

						{
							
							*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - *(_apig8meb+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_iykhdriq)));
Mark20:;
							// continue
						}
												}					}
Mark30:;
					// continue
				}
								}			}//* 
			//*        C( m-l+1:m, 1:n ) = C( m-l+1:m, 1:n ) - ... 
			//*                            V( 1:k, 1:l )**H * W( 1:n, 1:k )**H 
			//* 
			
			if (_68ec3gbh > (int)0)
			_5p0w9905("Transpose" ,"Transpose" ,ref _68ec3gbh ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+((_ev4xhht5 - _68ec3gbh) + (int)1 - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );//* 
			
		}
		else
		if (_w8y2rzgy(_m2cn2gjg ,"R" ))
		{
			//* 
			//*        Form  C * H  or  C * H**H 
			//* 
			//*        W( 1:m, 1:k ) = C( 1:m, 1:k ) 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn2126 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2126 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2126;
				for (__81fgg2count2126 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn2126 + __81fgg2step2126) / __81fgg2step2126)), _znpjgsef = __81fgg2dlsvn2126; __81fgg2count2126 != 0; __81fgg2count2126--, _znpjgsef += (__81fgg2step2126)) {

				{
					
					_33e0jk6i(ref _ev4xhht5 ,(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark40:;
					// continue
				}
								}			}//* 
			//*        W( 1:m, 1:k ) = W( 1:m, 1:k ) + ... 
			//*                        C( 1:m, n-l+1:n ) * V( 1:k, 1:l )**H 
			//* 
			
			if (_68ec3gbh > (int)0)
			_5p0w9905("No transpose" ,"Transpose" ,ref _ev4xhht5 ,ref _umlkckdg ,ref _68ec3gbh ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+((int)1 - 1) + ((_dxpq0xkr - _68ec3gbh) + (int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );//* 
			//*        W( 1:m, 1:k ) = W( 1:m, 1:k ) * conjg( T )  or 
			//*                        W( 1:m, 1:k ) * T**H 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn2127 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2127 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2127;
				for (__81fgg2count2127 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn2127 + __81fgg2step2127) / __81fgg2step2127)), _znpjgsef = __81fgg2dlsvn2127; __81fgg2count2127 != 0; __81fgg2count2127--, _znpjgsef += (__81fgg2step2127)) {

				{
					
					_png2g84j(ref Unsafe.AsRef((_umlkckdg - _znpjgsef) + (int)1) ,(_2ivtt43r+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) );
Mark50:;
					// continue
				}
								}			}
			_smeynpn3("Right" ,"Lower" ,_scuo79v4 ,"Non-unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );
			{
				System.Int32 __81fgg2dlsvn2128 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2128 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2128;
				for (__81fgg2count2128 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn2128 + __81fgg2step2128) / __81fgg2step2128)), _znpjgsef = __81fgg2dlsvn2128; __81fgg2count2128 != 0; __81fgg2count2128--, _znpjgsef += (__81fgg2step2128)) {

				{
					
					_png2g84j(ref Unsafe.AsRef((_umlkckdg - _znpjgsef) + (int)1) ,(_2ivtt43r+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) );
Mark60:;
					// continue
				}
								}			}//* 
			//*        C( 1:m, 1:k ) = C( 1:m, 1:k ) - W( 1:m, 1:k ) 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn2129 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2129 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2129;
				for (__81fgg2count2129 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn2129 + __81fgg2step2129) / __81fgg2step2129)), _znpjgsef = __81fgg2dlsvn2129; __81fgg2count2129 != 0; __81fgg2count2129--, _znpjgsef += (__81fgg2step2129)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn2130 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2130 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2130;
						for (__81fgg2count2130 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn2130 + __81fgg2step2130) / __81fgg2step2130)), _b5p6od9s = __81fgg2dlsvn2130; __81fgg2count2130 != 0; __81fgg2count2130--, _b5p6od9s += (__81fgg2step2130)) {

						{
							
							*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - *(_apig8meb+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)));
Mark70:;
							// continue
						}
												}					}
Mark80:;
					// continue
				}
								}			}//* 
			//*        C( 1:m, n-l+1:n ) = C( 1:m, n-l+1:n ) - ... 
			//*                            W( 1:m, 1:k ) * conjg( V( 1:k, 1:l ) ) 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn2131 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2131 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2131;
				for (__81fgg2count2131 = System.Math.Max(0, (System.Int32)(((System.Int32)(_68ec3gbh) - __81fgg2dlsvn2131 + __81fgg2step2131) / __81fgg2step2131)), _znpjgsef = __81fgg2dlsvn2131; __81fgg2count2131 != 0; __81fgg2count2131--, _znpjgsef += (__81fgg2step2131)) {

				{
					
					_png2g84j(ref _umlkckdg ,(_ycxba85s+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ys09rxze)),ref Unsafe.AsRef((int)1) );
Mark90:;
					// continue
				}
								}			}
			if (_68ec3gbh > (int)0)
			_5p0w9905("No transpose" ,"No transpose" ,ref _ev4xhht5 ,ref _68ec3gbh ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_apig8meb ,ref _iykhdriq ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+((int)1 - 1) + ((_dxpq0xkr - _68ec3gbh) + (int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );
			{
				System.Int32 __81fgg2dlsvn2132 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2132 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2132;
				for (__81fgg2count2132 = System.Math.Max(0, (System.Int32)(((System.Int32)(_68ec3gbh) - __81fgg2dlsvn2132 + __81fgg2step2132) / __81fgg2step2132)), _znpjgsef = __81fgg2dlsvn2132; __81fgg2count2132 != 0; __81fgg2count2132--, _znpjgsef += (__81fgg2step2132)) {

				{
					
					_png2g84j(ref _umlkckdg ,(_ycxba85s+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ys09rxze)),ref Unsafe.AsRef((int)1) );
Mark100:;
					// continue
				}
								}			}//* 
			
		}
		//* 
		
		return;//* 
		//*     End of CLARZB 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
