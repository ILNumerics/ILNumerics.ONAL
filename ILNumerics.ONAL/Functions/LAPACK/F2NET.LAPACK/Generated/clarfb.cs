
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
//*> \brief \b CLARFB applies a block reflector or its conjugate-transpose to a general rectangular matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CLARFB + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/clarfb.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/clarfb.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/clarfb.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CLARFB( SIDE, TRANS, DIRECT, STOREV, M, N, K, V, LDV, 
//*                          T, LDT, C, LDC, WORK, LDWORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          DIRECT, SIDE, STOREV, TRANS 
//*       INTEGER            K, LDC, LDT, LDV, LDWORK, M, N 
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
//*> CLARFB applies a complex block reflector H or its transpose H**H to a 
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
//*>          V is COMPLEX array, dimension 
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
//*> \date June 2013 
//* 
//*> \ingroup complexOTHERauxiliary 
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

	 
	public static void _h65laft9(FString _m2cn2gjg, FString _scuo79v4, FString _uw10mx43, FString _tjtvdgd6, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, fcomplex* _ycxba85s, ref Int32 _ys09rxze, fcomplex* _2ivtt43r, ref Int32 _w8yhbr2r, fcomplex* _3crf0qn3, ref Int32 _1s3eymp4, fcomplex* _apig8meb, ref Int32 _iykhdriq)
	{
#region variable declarations
fcomplex _kxg5drh2 =   new fcomplex(1f,0f);
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
						System.Int32 __81fgg2dlsvn950 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step950 = (System.Int32)((int)1);
						System.Int32 __81fgg2count950;
						for (__81fgg2count950 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn950 + __81fgg2step950) / __81fgg2step950)), _znpjgsef = __81fgg2dlsvn950; __81fgg2count950 != 0; __81fgg2count950--, _znpjgsef += (__81fgg2step950)) {

						{
							
							_33e0jk6i(ref _dxpq0xkr ,(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
							_png2g84j(ref _dxpq0xkr ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark10:;
							// continue
						}
												}					}//* 
					//*              W := W * V1 
					//* 
					
					_smeynpn3("Right" ,"Lower" ,"No transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 W := W + C2**H *V2 
						//* 
						
						_5p0w9905("Conjugate transpose" ,"No transpose" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_ycxba85s+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T**H  or  W * T 
					//* 
					
					_smeynpn3("Right" ,"Upper" ,_l09pt3ga ,"Non-unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - V * W**H 
					//* 
					
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 C2 := C2 - V2 * W**H 
						//* 
						
						_5p0w9905("No transpose" ,"Conjugate transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_ycxba85s+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V1**H 
					//* 
					
					_smeynpn3("Right" ,"Lower" ,"Conjugate transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C1 := C1 - W**H 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn951 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step951 = (System.Int32)((int)1);
						System.Int32 __81fgg2count951;
						for (__81fgg2count951 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn951 + __81fgg2step951) / __81fgg2step951)), _znpjgsef = __81fgg2dlsvn951; __81fgg2count951 != 0; __81fgg2count951--, _znpjgsef += (__81fgg2step951)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn952 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step952 = (System.Int32)((int)1);
								System.Int32 __81fgg2count952;
								for (__81fgg2count952 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn952 + __81fgg2step952) / __81fgg2step952)), _b5p6od9s = __81fgg2dlsvn952; __81fgg2count952 != 0; __81fgg2count952--, _b5p6od9s += (__81fgg2step952)) {

								{
									
									*(_3crf0qn3+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_1s3eymp4)) - ILNumerics.F2NET.Intrinsics.CONJG(*(_apig8meb+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)) ));
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
						System.Int32 __81fgg2dlsvn953 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step953 = (System.Int32)((int)1);
						System.Int32 __81fgg2count953;
						for (__81fgg2count953 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn953 + __81fgg2step953) / __81fgg2step953)), _znpjgsef = __81fgg2dlsvn953; __81fgg2count953 != 0; __81fgg2count953--, _znpjgsef += (__81fgg2step953)) {

						{
							
							_33e0jk6i(ref _ev4xhht5 ,(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark40:;
							// continue
						}
												}					}//* 
					//*              W := W * V1 
					//* 
					
					_smeynpn3("Right" ,"Lower" ,"No transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 W := W + C2 * V2 
						//* 
						
						_5p0w9905("No transpose" ,"No transpose" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_ycxba85s+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T  or  W * T**H 
					//* 
					
					_smeynpn3("Right" ,"Upper" ,_scuo79v4 ,"Non-unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - W * V**H 
					//* 
					
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 C2 := C2 - W * V2**H 
						//* 
						
						_5p0w9905("No transpose" ,"Conjugate transpose" ,ref _ev4xhht5 ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_apig8meb ,ref _iykhdriq ,(_ycxba85s+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V1**H 
					//* 
					
					_smeynpn3("Right" ,"Lower" ,"Conjugate transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C1 := C1 - W 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn954 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step954 = (System.Int32)((int)1);
						System.Int32 __81fgg2count954;
						for (__81fgg2count954 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn954 + __81fgg2step954) / __81fgg2step954)), _znpjgsef = __81fgg2dlsvn954; __81fgg2count954 != 0; __81fgg2count954--, _znpjgsef += (__81fgg2step954)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn955 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step955 = (System.Int32)((int)1);
								System.Int32 __81fgg2count955;
								for (__81fgg2count955 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn955 + __81fgg2step955) / __81fgg2step955)), _b5p6od9s = __81fgg2dlsvn955; __81fgg2count955 != 0; __81fgg2count955--, _b5p6od9s += (__81fgg2step955)) {

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
					//*                                                  ( C2 ) 
					//* 
					//*              W := C**H * V  =  (C1**H * V1 + C2**H * V2)  (stored in WORK) 
					//* 
					//*              W := C2**H 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn956 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step956 = (System.Int32)((int)1);
						System.Int32 __81fgg2count956;
						for (__81fgg2count956 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn956 + __81fgg2step956) / __81fgg2step956)), _znpjgsef = __81fgg2dlsvn956; __81fgg2count956 != 0; __81fgg2count956--, _znpjgsef += (__81fgg2step956)) {

						{
							
							_33e0jk6i(ref _dxpq0xkr ,(_3crf0qn3+((_ev4xhht5 - _umlkckdg) + _znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
							_png2g84j(ref _dxpq0xkr ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark70:;
							// continue
						}
												}					}//* 
					//*              W := W * V2 
					//* 
					
					_smeynpn3("Right" ,"Upper" ,"No transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((_ev4xhht5 - _umlkckdg) + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 W := W + C1**H * V1 
						//* 
						
						_5p0w9905("Conjugate transpose" ,"No transpose" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T**H  or  W * T 
					//* 
					
					_smeynpn3("Right" ,"Lower" ,_l09pt3ga ,"Non-unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - V * W**H 
					//* 
					
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 C1 := C1 - V1 * W**H 
						//* 
						
						_5p0w9905("No transpose" ,"Conjugate transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V2**H 
					//* 
					
					_smeynpn3("Right" ,"Upper" ,"Conjugate transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((_ev4xhht5 - _umlkckdg) + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C2 := C2 - W**H 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn957 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step957 = (System.Int32)((int)1);
						System.Int32 __81fgg2count957;
						for (__81fgg2count957 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn957 + __81fgg2step957) / __81fgg2step957)), _znpjgsef = __81fgg2dlsvn957; __81fgg2count957 != 0; __81fgg2count957--, _znpjgsef += (__81fgg2step957)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn958 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step958 = (System.Int32)((int)1);
								System.Int32 __81fgg2count958;
								for (__81fgg2count958 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn958 + __81fgg2step958) / __81fgg2step958)), _b5p6od9s = __81fgg2dlsvn958; __81fgg2count958 != 0; __81fgg2count958--, _b5p6od9s += (__81fgg2step958)) {

								{
									
									*(_3crf0qn3+((_ev4xhht5 - _umlkckdg) + _znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((_ev4xhht5 - _umlkckdg) + _znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_1s3eymp4)) - ILNumerics.F2NET.Intrinsics.CONJG(*(_apig8meb+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)) ));
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
						System.Int32 __81fgg2dlsvn959 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step959 = (System.Int32)((int)1);
						System.Int32 __81fgg2count959;
						for (__81fgg2count959 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn959 + __81fgg2step959) / __81fgg2step959)), _znpjgsef = __81fgg2dlsvn959; __81fgg2count959 != 0; __81fgg2count959--, _znpjgsef += (__81fgg2step959)) {

						{
							
							_33e0jk6i(ref _ev4xhht5 ,(_3crf0qn3+((int)1 - 1) + ((_dxpq0xkr - _umlkckdg) + _znpjgsef - 1) * 1 * (_1s3eymp4)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark100:;
							// continue
						}
												}					}//* 
					//*              W := W * V2 
					//* 
					
					_smeynpn3("Right" ,"Upper" ,"No transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((_dxpq0xkr - _umlkckdg) + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 W := W + C1 * V1 
						//* 
						
						_5p0w9905("No transpose" ,"No transpose" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T  or  W * T**H 
					//* 
					
					_smeynpn3("Right" ,"Lower" ,_scuo79v4 ,"Non-unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - W * V**H 
					//* 
					
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 C1 := C1 - W * V1**H 
						//* 
						
						_5p0w9905("No transpose" ,"Conjugate transpose" ,ref _ev4xhht5 ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_apig8meb ,ref _iykhdriq ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V2**H 
					//* 
					
					_smeynpn3("Right" ,"Upper" ,"Conjugate transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((_dxpq0xkr - _umlkckdg) + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C2 := C2 - W 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn960 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step960 = (System.Int32)((int)1);
						System.Int32 __81fgg2count960;
						for (__81fgg2count960 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn960 + __81fgg2step960) / __81fgg2step960)), _znpjgsef = __81fgg2dlsvn960; __81fgg2count960 != 0; __81fgg2count960--, _znpjgsef += (__81fgg2step960)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn961 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step961 = (System.Int32)((int)1);
								System.Int32 __81fgg2count961;
								for (__81fgg2count961 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn961 + __81fgg2step961) / __81fgg2step961)), _b5p6od9s = __81fgg2dlsvn961; __81fgg2count961 != 0; __81fgg2count961--, _b5p6od9s += (__81fgg2step961)) {

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
						System.Int32 __81fgg2dlsvn962 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step962 = (System.Int32)((int)1);
						System.Int32 __81fgg2count962;
						for (__81fgg2count962 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn962 + __81fgg2step962) / __81fgg2step962)), _znpjgsef = __81fgg2dlsvn962; __81fgg2count962 != 0; __81fgg2count962--, _znpjgsef += (__81fgg2step962)) {

						{
							
							_33e0jk6i(ref _dxpq0xkr ,(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
							_png2g84j(ref _dxpq0xkr ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark130:;
							// continue
						}
												}					}//* 
					//*              W := W * V1**H 
					//* 
					
					_smeynpn3("Right" ,"Upper" ,"Conjugate transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 W := W + C2**H * V2**H 
						//* 
						
						_5p0w9905("Conjugate transpose" ,"Conjugate transpose" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_ycxba85s+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T**H  or  W * T 
					//* 
					
					_smeynpn3("Right" ,"Upper" ,_l09pt3ga ,"Non-unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - V**H * W**H 
					//* 
					
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 C2 := C2 - V2**H * W**H 
						//* 
						
						_5p0w9905("Conjugate transpose" ,"Conjugate transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_ycxba85s+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V1 
					//* 
					
					_smeynpn3("Right" ,"Upper" ,"No transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C1 := C1 - W**H 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn963 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step963 = (System.Int32)((int)1);
						System.Int32 __81fgg2count963;
						for (__81fgg2count963 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn963 + __81fgg2step963) / __81fgg2step963)), _znpjgsef = __81fgg2dlsvn963; __81fgg2count963 != 0; __81fgg2count963--, _znpjgsef += (__81fgg2step963)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn964 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step964 = (System.Int32)((int)1);
								System.Int32 __81fgg2count964;
								for (__81fgg2count964 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn964 + __81fgg2step964) / __81fgg2step964)), _b5p6od9s = __81fgg2dlsvn964; __81fgg2count964 != 0; __81fgg2count964--, _b5p6od9s += (__81fgg2step964)) {

								{
									
									*(_3crf0qn3+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_1s3eymp4)) - ILNumerics.F2NET.Intrinsics.CONJG(*(_apig8meb+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)) ));
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
						System.Int32 __81fgg2dlsvn965 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step965 = (System.Int32)((int)1);
						System.Int32 __81fgg2count965;
						for (__81fgg2count965 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn965 + __81fgg2step965) / __81fgg2step965)), _znpjgsef = __81fgg2dlsvn965; __81fgg2count965 != 0; __81fgg2count965--, _znpjgsef += (__81fgg2step965)) {

						{
							
							_33e0jk6i(ref _ev4xhht5 ,(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark160:;
							// continue
						}
												}					}//* 
					//*              W := W * V1**H 
					//* 
					
					_smeynpn3("Right" ,"Upper" ,"Conjugate transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 W := W + C2 * V2**H 
						//* 
						
						_5p0w9905("No transpose" ,"Conjugate transpose" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_ycxba85s+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T  or  W * T**H 
					//* 
					
					_smeynpn3("Right" ,"Upper" ,_scuo79v4 ,"Non-unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - W * V 
					//* 
					
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 C2 := C2 - W * V2 
						//* 
						
						_5p0w9905("No transpose" ,"No transpose" ,ref _ev4xhht5 ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_apig8meb ,ref _iykhdriq ,(_ycxba85s+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,(_3crf0qn3+((int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V1 
					//* 
					
					_smeynpn3("Right" ,"Upper" ,"No transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C1 := C1 - W 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn966 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step966 = (System.Int32)((int)1);
						System.Int32 __81fgg2count966;
						for (__81fgg2count966 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn966 + __81fgg2step966) / __81fgg2step966)), _znpjgsef = __81fgg2dlsvn966; __81fgg2count966 != 0; __81fgg2count966--, _znpjgsef += (__81fgg2step966)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn967 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step967 = (System.Int32)((int)1);
								System.Int32 __81fgg2count967;
								for (__81fgg2count967 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn967 + __81fgg2step967) / __81fgg2step967)), _b5p6od9s = __81fgg2dlsvn967; __81fgg2count967 != 0; __81fgg2count967--, _b5p6od9s += (__81fgg2step967)) {

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
						System.Int32 __81fgg2dlsvn968 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step968 = (System.Int32)((int)1);
						System.Int32 __81fgg2count968;
						for (__81fgg2count968 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn968 + __81fgg2step968) / __81fgg2step968)), _znpjgsef = __81fgg2dlsvn968; __81fgg2count968 != 0; __81fgg2count968--, _znpjgsef += (__81fgg2step968)) {

						{
							
							_33e0jk6i(ref _dxpq0xkr ,(_3crf0qn3+((_ev4xhht5 - _umlkckdg) + _znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
							_png2g84j(ref _dxpq0xkr ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark190:;
							// continue
						}
												}					}//* 
					//*              W := W * V2**H 
					//* 
					
					_smeynpn3("Right" ,"Lower" ,"Conjugate transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((int)1 - 1) + ((_ev4xhht5 - _umlkckdg) + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 W := W + C1**H * V1**H 
						//* 
						
						_5p0w9905("Conjugate transpose" ,"Conjugate transpose" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T**H  or  W * T 
					//* 
					
					_smeynpn3("Right" ,"Lower" ,_l09pt3ga ,"Non-unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - V**H * W**H 
					//* 
					
					if (_ev4xhht5 > _umlkckdg)
					{
						//* 
						//*                 C1 := C1 - V1**H * W**H 
						//* 
						
						_5p0w9905("Conjugate transpose" ,"Conjugate transpose" ,ref Unsafe.AsRef(_ev4xhht5 - _umlkckdg) ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _iykhdriq ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V2 
					//* 
					
					_smeynpn3("Right" ,"Lower" ,"No transpose" ,"Unit" ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((int)1 - 1) + ((_ev4xhht5 - _umlkckdg) + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C2 := C2 - W**H 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn969 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step969 = (System.Int32)((int)1);
						System.Int32 __81fgg2count969;
						for (__81fgg2count969 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn969 + __81fgg2step969) / __81fgg2step969)), _znpjgsef = __81fgg2dlsvn969; __81fgg2count969 != 0; __81fgg2count969--, _znpjgsef += (__81fgg2step969)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn970 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step970 = (System.Int32)((int)1);
								System.Int32 __81fgg2count970;
								for (__81fgg2count970 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn970 + __81fgg2step970) / __81fgg2step970)), _b5p6od9s = __81fgg2dlsvn970; __81fgg2count970 != 0; __81fgg2count970--, _b5p6od9s += (__81fgg2step970)) {

								{
									
									*(_3crf0qn3+((_ev4xhht5 - _umlkckdg) + _znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((_ev4xhht5 - _umlkckdg) + _znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_1s3eymp4)) - ILNumerics.F2NET.Intrinsics.CONJG(*(_apig8meb+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)) ));
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
						System.Int32 __81fgg2dlsvn971 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step971 = (System.Int32)((int)1);
						System.Int32 __81fgg2count971;
						for (__81fgg2count971 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn971 + __81fgg2step971) / __81fgg2step971)), _znpjgsef = __81fgg2dlsvn971; __81fgg2count971 != 0; __81fgg2count971--, _znpjgsef += (__81fgg2step971)) {

						{
							
							_33e0jk6i(ref _ev4xhht5 ,(_3crf0qn3+((int)1 - 1) + ((_dxpq0xkr - _umlkckdg) + _znpjgsef - 1) * 1 * (_1s3eymp4)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)),ref Unsafe.AsRef((int)1) );
Mark220:;
							// continue
						}
												}					}//* 
					//*              W := W * V2**H 
					//* 
					
					_smeynpn3("Right" ,"Lower" ,"Conjugate transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((int)1 - 1) + ((_dxpq0xkr - _umlkckdg) + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 W := W + C1 * V1**H 
						//* 
						
						_5p0w9905("No transpose" ,"Conjugate transpose" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_apig8meb ,ref _iykhdriq );
					}
					//* 
					//*              W := W * T  or  W * T**H 
					//* 
					
					_smeynpn3("Right" ,"Lower" ,_scuo79v4 ,"Non-unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );//* 
					//*              C := C - W * V 
					//* 
					
					if (_dxpq0xkr > _umlkckdg)
					{
						//* 
						//*                 C1 := C1 - W * V1 
						//* 
						
						_5p0w9905("No transpose" ,"No transpose" ,ref _ev4xhht5 ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref _umlkckdg ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_apig8meb ,ref _iykhdriq ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,_3crf0qn3 ,ref _1s3eymp4 );
					}
					//* 
					//*              W := W * V2 
					//* 
					
					_smeynpn3("Right" ,"Lower" ,"No transpose" ,"Unit" ,ref _ev4xhht5 ,ref _umlkckdg ,ref Unsafe.AsRef(_kxg5drh2) ,(_ycxba85s+((int)1 - 1) + ((_dxpq0xkr - _umlkckdg) + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,_apig8meb ,ref _iykhdriq );//* 
					//*              C1 := C1 - W 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn972 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step972 = (System.Int32)((int)1);
						System.Int32 __81fgg2count972;
						for (__81fgg2count972 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn972 + __81fgg2step972) / __81fgg2step972)), _znpjgsef = __81fgg2dlsvn972; __81fgg2count972 != 0; __81fgg2count972--, _znpjgsef += (__81fgg2step972)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn973 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step973 = (System.Int32)((int)1);
								System.Int32 __81fgg2count973;
								for (__81fgg2count973 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn973 + __81fgg2step973) / __81fgg2step973)), _b5p6od9s = __81fgg2dlsvn973; __81fgg2count973 != 0; __81fgg2count973--, _b5p6od9s += (__81fgg2step973)) {

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
		//*     End of CLARFB 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
