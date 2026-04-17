
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
//*> \brief \b SLARFB applies a block reflector or its transpose to a general rectangular matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLARFB + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slarfb.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slarfb.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slarfb.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLARFB( SIDE, TRANS, DIRECT, STOREV, M, N, K, V, LDV, 
//*                          T, LDT, C, LDC, WORK, LDWORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          DIRECT, SIDE, STOREV, TRANS 
//*       INTEGER            K, LDC, LDT, LDV, LDWORK, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               C( LDC, * ), T( LDT, * ), V( LDV, * ), 
//*      $                   WORK( LDWORK, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLARFB applies a real block reflector H or its transpose H**T to a 
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
//*>          V is REAL array, dimension 
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
//*>          T is REAL array, dimension (LDT,K) 
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
//*>          C is REAL array, dimension (LDC,N) 
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
//*>          WORK is REAL array, dimension (LDWORK,K) 
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
//*> \ingroup realOTHERauxiliary 
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

	 
	public static void _8ewy8tu3(FString _m2cn2gjg, FString _scuo79v4, FString _uw10mx43, FString _tjtvdgd6, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, Single* _ycxba85s, ref Int32 _ys09rxze, Single* _2ivtt43r, ref Int32 _w8yhbr2r, Single* _3crf0qn3, ref Int32 _1s3eymp4, Single* _apig8meb, ref Int32 _iykhdriq)
	{
#region variable declarations
Single _kxg5drh2 =  1f;
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
						System.Int32 __81fgg2dlsvn791 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step791 = (System.Int32)((int)1);
						System.Int32 __81fgg2count791;
						for (__81fgg2count791 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn791 + __81fgg2step791) / __81fgg2step791)), _znpjgsef = __81fgg2dlsvn791; __81fgg2count791 != 0; __81fgg2count791--, _znpjgsef += (__81fgg2step791)) {

						{
							
							_wcs7ne88(ref _dxpq0xkr ,(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark10:;
							// continue
						}
												}					}//* 
					//*              W := W * V1 
					//* 
					
					_sdtp2num("Right" ,"Lower" ,"No transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 W := W + C2**T * V2 
						//* 
						
						_b8wa9454("Transpose" ,"No transpose" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_ycxba85s+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T**T  or  W * T 
					//* 
					
					_sdtp2num("Right" ,"Upper" ,_l09pt3ga ,"Non-unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - V * W**T 
					//* 
					
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 C2 := C2 - V2 * W**T 
						//* 
						
						_b8wa9454("No transpose" ,"Transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_ycxba85s+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V1**T 
					//* 
					
					_sdtp2num("Right" ,"Lower" ,"Transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C1 := C1 - W**T 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn792 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step792 = (System.Int32)((int)1);
						System.Int32 __81fgg2count792;
						for (__81fgg2count792 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn792 + __81fgg2step792) / __81fgg2step792)), _znpjgsef = __81fgg2dlsvn792; __81fgg2count792 != 0; __81fgg2count792--, _znpjgsef += (__81fgg2step792)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn793 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step793 = (System.Int32)((int)1);
								System.Int32 __81fgg2count793;
								for (__81fgg2count793 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn793 + __81fgg2step793) / __81fgg2step793)), _b5p6od9s = __81fgg2dlsvn793; __81fgg2count793 != 0; __81fgg2count793--, _b5p6od9s += (__81fgg2step793)) {

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
						System.Int32 __81fgg2dlsvn794 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step794 = (System.Int32)((int)1);
						System.Int32 __81fgg2count794;
						for (__81fgg2count794 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn794 + __81fgg2step794) / __81fgg2step794)), _znpjgsef = __81fgg2dlsvn794; __81fgg2count794 != 0; __81fgg2count794--, _znpjgsef += (__81fgg2step794)) {

						{
							
							_wcs7ne88(ref _ev4xhht5 ,(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark40:;
							// continue
						}
												}					}//* 
					//*              W := W * V1 
					//* 
					
					_sdtp2num("Right" ,"Lower" ,"No transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 W := W + C2 * V2 
						//* 
						
						_b8wa9454("No transpose" ,"No transpose" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_ycxba85s+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T  or  W * T**T 
					//* 
					
					_sdtp2num("Right" ,"Upper" ,_scuo79v4 ,"Non-unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - W * V**T 
					//* 
					
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 C2 := C2 - W * V2**T 
						//* 
						
						_b8wa9454("No transpose" ,"Transpose" ,ref _ev4xhht5 ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_apig8meb ,ref _iykhdriq ,(_ycxba85s+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V1**T 
					//* 
					
					_sdtp2num("Right" ,"Lower" ,"Transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C1 := C1 - W 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn795 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step795 = (System.Int32)((int)1);
						System.Int32 __81fgg2count795;
						for (__81fgg2count795 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn795 + __81fgg2step795) / __81fgg2step795)), _znpjgsef = __81fgg2dlsvn795; __81fgg2count795 != 0; __81fgg2count795--, _znpjgsef += (__81fgg2step795)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn796 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step796 = (System.Int32)((int)1);
								System.Int32 __81fgg2count796;
								for (__81fgg2count796 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn796 + __81fgg2step796) / __81fgg2step796)), _b5p6od9s = __81fgg2dlsvn796; __81fgg2count796 != 0; __81fgg2count796--, _b5p6od9s += (__81fgg2step796)) {

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
						System.Int32 __81fgg2dlsvn797 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step797 = (System.Int32)((int)1);
						System.Int32 __81fgg2count797;
						for (__81fgg2count797 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn797 + __81fgg2step797) / __81fgg2step797)), _znpjgsef = __81fgg2dlsvn797; __81fgg2count797 != 0; __81fgg2count797--, _znpjgsef += (__81fgg2step797)) {

						{
							
							_wcs7ne88(ref _dxpq0xkr ,(_3crf0qn3+((_ev4xhht5 - _umlkckdg) + _znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark70:;
							// continue
						}
												}					}//* 
					//*              W := W * V2 
					//* 
					
					_sdtp2num("Right" ,"Upper" ,"No transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((_ev4xhht5 - _umlkckdg) + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 W := W + C1**T * V1 
						//* 
						
						_b8wa9454("Transpose" ,"No transpose" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T**T  or  W * T 
					//* 
					
					_sdtp2num("Right" ,"Lower" ,_l09pt3ga ,"Non-unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - V * W**T 
					//* 
					
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 C1 := C1 - V1 * W**T 
						//* 
						
						_b8wa9454("No transpose" ,"Transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V2**T 
					//* 
					
					_sdtp2num("Right" ,"Upper" ,"Transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((_ev4xhht5 - _umlkckdg) + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C2 := C2 - W**T 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn798 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step798 = (System.Int32)((int)1);
						System.Int32 __81fgg2count798;
						for (__81fgg2count798 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn798 + __81fgg2step798) / __81fgg2step798)), _znpjgsef = __81fgg2dlsvn798; __81fgg2count798 != 0; __81fgg2count798--, _znpjgsef += (__81fgg2step798)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn799 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step799 = (System.Int32)((int)1);
								System.Int32 __81fgg2count799;
								for (__81fgg2count799 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn799 + __81fgg2step799) / __81fgg2step799)), _b5p6od9s = __81fgg2dlsvn799; __81fgg2count799 != 0; __81fgg2count799--, _b5p6od9s += (__81fgg2step799)) {

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
					//*              Form  C * H  or  C * H'  where  C = ( C1  C2 ) 
					//* 
					//*              W := C * V  =  (C1*V1 + C2*V2)  (stored in WORK) 
					//* 
					//*              W := C2 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn800 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step800 = (System.Int32)((int)1);
						System.Int32 __81fgg2count800;
						for (__81fgg2count800 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn800 + __81fgg2step800) / __81fgg2step800)), _znpjgsef = __81fgg2dlsvn800; __81fgg2count800 != 0; __81fgg2count800--, _znpjgsef += (__81fgg2step800)) {

						{
							
							_wcs7ne88(ref _ev4xhht5 ,(_3crf0qn3+((int)1 - 1) + ((_dxpq0xkr - _umlkckdg) + _znpjgsef - 1) * 1 * (_1s3eymp4)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark100:;
							// continue
						}
												}					}//* 
					//*              W := W * V2 
					//* 
					
					_sdtp2num("Right" ,"Upper" ,"No transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((_dxpq0xkr - _umlkckdg) + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 W := W + C1 * V1 
						//* 
						
						_b8wa9454("No transpose" ,"No transpose" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T  or  W * T**T 
					//* 
					
					_sdtp2num("Right" ,"Lower" ,_scuo79v4 ,"Non-unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - W * V**T 
					//* 
					
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 C1 := C1 - W * V1**T 
						//* 
						
						_b8wa9454("No transpose" ,"Transpose" ,ref _ev4xhht5 ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_apig8meb ,ref _iykhdriq ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V2**T 
					//* 
					
					_sdtp2num("Right" ,"Upper" ,"Transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((_dxpq0xkr - _umlkckdg) + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C2 := C2 - W 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn801 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step801 = (System.Int32)((int)1);
						System.Int32 __81fgg2count801;
						for (__81fgg2count801 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn801 + __81fgg2step801) / __81fgg2step801)), _znpjgsef = __81fgg2dlsvn801; __81fgg2count801 != 0; __81fgg2count801--, _znpjgsef += (__81fgg2step801)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn802 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step802 = (System.Int32)((int)1);
								System.Int32 __81fgg2count802;
								for (__81fgg2count802 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn802 + __81fgg2step802) / __81fgg2step802)), _b5p6od9s = __81fgg2dlsvn802; __81fgg2count802 != 0; __81fgg2count802--, _b5p6od9s += (__81fgg2step802)) {

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
						System.Int32 __81fgg2dlsvn803 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step803 = (System.Int32)((int)1);
						System.Int32 __81fgg2count803;
						for (__81fgg2count803 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn803 + __81fgg2step803) / __81fgg2step803)), _znpjgsef = __81fgg2dlsvn803; __81fgg2count803 != 0; __81fgg2count803--, _znpjgsef += (__81fgg2step803)) {

						{
							
							_wcs7ne88(ref _dxpq0xkr ,(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark130:;
							// continue
						}
												}					}//* 
					//*              W := W * V1**T 
					//* 
					
					_sdtp2num("Right" ,"Upper" ,"Transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 W := W + C2**T * V2**T 
						//* 
						
						_b8wa9454("Transpose" ,"Transpose" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_ycxba85s+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T**T  or  W * T 
					//* 
					
					_sdtp2num("Right" ,"Upper" ,_l09pt3ga ,"Non-unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - V**T * W**T 
					//* 
					
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 C2 := C2 - V2**T * W**T 
						//* 
						
						_b8wa9454("Transpose" ,"Transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_ycxba85s+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V1 
					//* 
					
					_sdtp2num("Right" ,"Upper" ,"No transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C1 := C1 - W**T 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn804 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step804 = (System.Int32)((int)1);
						System.Int32 __81fgg2count804;
						for (__81fgg2count804 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn804 + __81fgg2step804) / __81fgg2step804)), _znpjgsef = __81fgg2dlsvn804; __81fgg2count804 != 0; __81fgg2count804--, _znpjgsef += (__81fgg2step804)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn805 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step805 = (System.Int32)((int)1);
								System.Int32 __81fgg2count805;
								for (__81fgg2count805 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn805 + __81fgg2step805) / __81fgg2step805)), _b5p6od9s = __81fgg2dlsvn805; __81fgg2count805 != 0; __81fgg2count805--, _b5p6od9s += (__81fgg2step805)) {

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
						System.Int32 __81fgg2dlsvn806 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step806 = (System.Int32)((int)1);
						System.Int32 __81fgg2count806;
						for (__81fgg2count806 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn806 + __81fgg2step806) / __81fgg2step806)), _znpjgsef = __81fgg2dlsvn806; __81fgg2count806 != 0; __81fgg2count806--, _znpjgsef += (__81fgg2step806)) {

						{
							
							_wcs7ne88(ref _ev4xhht5 ,(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark160:;
							// continue
						}
												}					}//* 
					//*              W := W * V1**T 
					//* 
					
					_sdtp2num("Right" ,"Upper" ,"Transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 W := W + C2 * V2**T 
						//* 
						
						_b8wa9454("No transpose" ,"Transpose" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_ycxba85s+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T  or  W * T**T 
					//* 
					
					_sdtp2num("Right" ,"Upper" ,_scuo79v4 ,"Non-unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - W * V 
					//* 
					
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 C2 := C2 - W * V2 
						//* 
						
						_b8wa9454("No transpose" ,"No transpose" ,ref _ev4xhht5 ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_apig8meb ,ref _iykhdriq ,(_ycxba85s+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V1 
					//* 
					
					_sdtp2num("Right" ,"Upper" ,"No transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C1 := C1 - W 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn807 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step807 = (System.Int32)((int)1);
						System.Int32 __81fgg2count807;
						for (__81fgg2count807 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn807 + __81fgg2step807) / __81fgg2step807)), _znpjgsef = __81fgg2dlsvn807; __81fgg2count807 != 0; __81fgg2count807--, _znpjgsef += (__81fgg2step807)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn808 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step808 = (System.Int32)((int)1);
								System.Int32 __81fgg2count808;
								for (__81fgg2count808 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn808 + __81fgg2step808) / __81fgg2step808)), _b5p6od9s = __81fgg2dlsvn808; __81fgg2count808 != 0; __81fgg2count808--, _b5p6od9s += (__81fgg2step808)) {

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
						System.Int32 __81fgg2dlsvn809 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step809 = (System.Int32)((int)1);
						System.Int32 __81fgg2count809;
						for (__81fgg2count809 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn809 + __81fgg2step809) / __81fgg2step809)), _znpjgsef = __81fgg2dlsvn809; __81fgg2count809 != 0; __81fgg2count809--, _znpjgsef += (__81fgg2step809)) {

						{
							
							_wcs7ne88(ref _dxpq0xkr ,(_3crf0qn3+((_ev4xhht5 - _umlkckdg) + _znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark190:;
							// continue
						}
												}					}//* 
					//*              W := W * V2**T 
					//* 
					
					_sdtp2num("Right" ,"Lower" ,"Transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((int)1 - 1) + ((_ev4xhht5 - _umlkckdg) + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 W := W + C1**T * V1**T 
						//* 
						
						_b8wa9454("Transpose" ,"Transpose" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T**T  or  W * T 
					//* 
					
					_sdtp2num("Right" ,"Lower" ,_l09pt3ga ,"Non-unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - V**T * W**T 
					//* 
					
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 C1 := C1 - V1**T * W**T 
						//* 
						
						_b8wa9454("Transpose" ,"Transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V2 
					//* 
					
					_sdtp2num("Right" ,"Lower" ,"No transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((int)1 - 1) + ((_ev4xhht5 - _umlkckdg) + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C2 := C2 - W**T 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn810 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step810 = (System.Int32)((int)1);
						System.Int32 __81fgg2count810;
						for (__81fgg2count810 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn810 + __81fgg2step810) / __81fgg2step810)), _znpjgsef = __81fgg2dlsvn810; __81fgg2count810 != 0; __81fgg2count810--, _znpjgsef += (__81fgg2step810)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn811 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step811 = (System.Int32)((int)1);
								System.Int32 __81fgg2count811;
								for (__81fgg2count811 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn811 + __81fgg2step811) / __81fgg2step811)), _b5p6od9s = __81fgg2dlsvn811; __81fgg2count811 != 0; __81fgg2count811--, _b5p6od9s += (__81fgg2step811)) {

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
					//*              Form  C * H  or  C * H**T  where  C = ( C1  C2 ) 
					//* 
					//*              W := C * V**T  =  (C1*V1**T + C2*V2**T)  (stored in WORK) 
					//* 
					//*              W := C2 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn812 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step812 = (System.Int32)((int)1);
						System.Int32 __81fgg2count812;
						for (__81fgg2count812 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn812 + __81fgg2step812) / __81fgg2step812)), _znpjgsef = __81fgg2dlsvn812; __81fgg2count812 != 0; __81fgg2count812--, _znpjgsef += (__81fgg2step812)) {

						{
							
							_wcs7ne88(ref _ev4xhht5 ,(_3crf0qn3+((int)1 - 1) + ((_dxpq0xkr - _umlkckdg) + _znpjgsef - 1) * 1 * (_1s3eymp4)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark220:;
							// continue
						}
												}					}//* 
					//*              W := W * V2**T 
					//* 
					
					_sdtp2num("Right" ,"Lower" ,"Transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((int)1 - 1) + ((_dxpq0xkr - _umlkckdg) + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 W := W + C1 * V1**T 
						//* 
						
						_b8wa9454("No transpose" ,"Transpose" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T  or  W * T**T 
					//* 
					
					_sdtp2num("Right" ,"Lower" ,_scuo79v4 ,"Non-unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - W * V 
					//* 
					
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 C1 := C1 - W * V1 
						//* 
						
						_b8wa9454("No transpose" ,"No transpose" ,ref _ev4xhht5 ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_apig8meb ,ref _iykhdriq ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V2 
					//* 
					
					_sdtp2num("Right" ,"Lower" ,"No transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((int)1 - 1) + ((_dxpq0xkr - _umlkckdg) + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C1 := C1 - W 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn813 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step813 = (System.Int32)((int)1);
						System.Int32 __81fgg2count813;
						for (__81fgg2count813 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn813 + __81fgg2step813) / __81fgg2step813)), _znpjgsef = __81fgg2dlsvn813; __81fgg2count813 != 0; __81fgg2count813--, _znpjgsef += (__81fgg2step813)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn814 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step814 = (System.Int32)((int)1);
								System.Int32 __81fgg2count814;
								for (__81fgg2count814 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn814 + __81fgg2step814) / __81fgg2step814)), _b5p6od9s = __81fgg2dlsvn814; __81fgg2count814 != 0; __81fgg2count814--, _b5p6od9s += (__81fgg2step814)) {

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
		//*     End of SLARFB 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
