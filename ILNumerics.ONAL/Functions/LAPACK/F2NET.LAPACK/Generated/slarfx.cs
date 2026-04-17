
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
//*> \brief \b SLARFX applies an elementary reflector to a general rectangular matrix, with loop unrolling when the reflector has order ≤ 10. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLARFX + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slarfx.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slarfx.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slarfx.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLARFX( SIDE, M, N, V, TAU, C, LDC, WORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          SIDE 
//*       INTEGER            LDC, M, N 
//*       REAL               TAU 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               C( LDC, * ), V( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLARFX applies a real elementary reflector H to a real m by n 
//*> matrix C, from either the left or the right. H is represented in the 
//*> form 
//*> 
//*>       H = I - tau * v * v**T 
//*> 
//*> where tau is a real scalar and v is a real vector. 
//*> 
//*> If tau = 0, then H is taken to be the unit matrix 
//*> 
//*> This version uses inline code if H has order < 11. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] SIDE 
//*> \verbatim 
//*>          SIDE is CHARACTER*1 
//*>          = 'L': form  H * C 
//*>          = 'R': form  C * H 
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
//*> \param[in] V 
//*> \verbatim 
//*>          V is REAL array, dimension (M) if SIDE = 'L' 
//*>                                     or (N) if SIDE = 'R' 
//*>          The vector v in the representation of H. 
//*> \endverbatim 
//*> 
//*> \param[in] TAU 
//*> \verbatim 
//*>          TAU is REAL 
//*>          The value tau in the representation of H. 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is REAL array, dimension (LDC,N) 
//*>          On entry, the m by n matrix C. 
//*>          On exit, C is overwritten by the matrix H * C if SIDE = 'L', 
//*>          or C * H if SIDE = 'R'. 
//*> \endverbatim 
//*> 
//*> \param[in] LDC 
//*> \verbatim 
//*>          LDC is INTEGER 
//*>          The leading dimension of the array C. LDC >= (1,M). 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is REAL array, dimension 
//*>                      (N) if SIDE = 'L' 
//*>                      or (M) if SIDE = 'R' 
//*>          WORK is not referenced if H has order < 11. 
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
//*> \ingroup realOTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _ffkyv4kk(FString _m2cn2gjg, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, Single* _ycxba85s, ref Single _0446f4de, Single* _3crf0qn3, ref Int32 _1s3eymp4, Single* _apig8meb)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
Int32 _znpjgsef =  default;
Single _6j9l5fwy =  default;
Single _dkyrz0g2 =  default;
Single _ia0551b4 =  default;
Single _jzq4t39k =  default;
Single _sihp0s0u =  default;
Single _9i8orex5 =  default;
Single _t3e90rm8 =  default;
Single _phglqglk =  default;
Single _mpowry76 =  default;
Single _4ldqm5m0 =  default;
Single _evj5hwb1 =  default;
Single _z1p8kxoj =  default;
Single _y2pyu0n3 =  default;
Single _j3ir6imc =  default;
Single _s8neut4h =  default;
Single _7ycc0bo5 =  default;
Single _6ifoo5vu =  default;
Single _4qg46pc0 =  default;
Single _ow8yjime =  default;
Single _8jty3k9d =  default;
Single _ho99rwm2 =  default;
string fLanavab = default;
#endregion  variable declarations
_m2cn2gjg = _m2cn2gjg.Convert(1);

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
		
		if (_0446f4de == _d0547bi2)
		return;
		if (_w8y2rzgy(_m2cn2gjg ,"L" ))
		{
			//* 
			//*        Form  H * C, where H has order m. 
			//* 
			
			switch (_ev4xhht5) {
								case 1:
				goto Mark10;
				case 2:
				goto Mark30;
				case 3:
				goto Mark50;
				case 4:
				goto Mark70;
				case 5:
				goto Mark90;
				case 6:
				goto Mark110;
				case 7:
				goto Mark130;
				case 8:
				goto Mark150;
				case 9:
				goto Mark170;
				case 10:
				goto Mark190;
				default:
				break;
			}
//* 
			//*        Code for general M 
			//* 
			
			_tfywat2m(_m2cn2gjg ,ref _ev4xhht5 ,ref _dxpq0xkr ,_ycxba85s ,ref Unsafe.AsRef((int)1) ,ref _0446f4de ,_3crf0qn3 ,ref _1s3eymp4 ,_apig8meb );goto Mark410;
Mark10:;
			// continue//* 
			//*        Special code for 1 x 1 Householder 
			//* 
			
			_dkyrz0g2 = (_kxg5drh2 - ((_0446f4de * *(_ycxba85s+((int)1 - 1))) * *(_ycxba85s+((int)1 - 1))));
			{
				System.Int32 __81fgg2dlsvn2393 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2393 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2393;
				for (__81fgg2count2393 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2393 + __81fgg2step2393) / __81fgg2step2393)), _znpjgsef = __81fgg2dlsvn2393; __81fgg2count2393 != 0; __81fgg2count2393--, _znpjgsef += (__81fgg2step2393)) {

				{
					
					*(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (_dkyrz0g2 * *(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)));
Mark20:;
					// continue
				}
								}			}goto Mark410;
Mark30:;
			// continue//* 
			//*        Special code for 2 x 2 Householder 
			//* 
			
			_z1p8kxoj = *(_ycxba85s+((int)1 - 1));
			_dkyrz0g2 = (_0446f4de * _z1p8kxoj);
			_j3ir6imc = *(_ycxba85s+((int)2 - 1));
			_jzq4t39k = (_0446f4de * _j3ir6imc);
			{
				System.Int32 __81fgg2dlsvn2394 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2394 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2394;
				for (__81fgg2count2394 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2394 + __81fgg2step2394) / __81fgg2step2394)), _znpjgsef = __81fgg2dlsvn2394; __81fgg2count2394 != 0; __81fgg2count2394--, _znpjgsef += (__81fgg2step2394)) {

				{
					
					_6j9l5fwy = ((_z1p8kxoj * *(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))) + (_j3ir6imc * *(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))));
					*(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _dkyrz0g2));
					*(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _jzq4t39k));
Mark40:;
					// continue
				}
								}			}goto Mark410;
Mark50:;
			// continue//* 
			//*        Special code for 3 x 3 Householder 
			//* 
			
			_z1p8kxoj = *(_ycxba85s+((int)1 - 1));
			_dkyrz0g2 = (_0446f4de * _z1p8kxoj);
			_j3ir6imc = *(_ycxba85s+((int)2 - 1));
			_jzq4t39k = (_0446f4de * _j3ir6imc);
			_s8neut4h = *(_ycxba85s+((int)3 - 1));
			_sihp0s0u = (_0446f4de * _s8neut4h);
			{
				System.Int32 __81fgg2dlsvn2395 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2395 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2395;
				for (__81fgg2count2395 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2395 + __81fgg2step2395) / __81fgg2step2395)), _znpjgsef = __81fgg2dlsvn2395; __81fgg2count2395 != 0; __81fgg2count2395--, _znpjgsef += (__81fgg2step2395)) {

				{
					
					_6j9l5fwy = (((_z1p8kxoj * *(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))) + (_j3ir6imc * *(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_s8neut4h * *(_3crf0qn3+((int)3 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))));
					*(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _dkyrz0g2));
					*(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _jzq4t39k));
					*(_3crf0qn3+((int)3 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)3 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _sihp0s0u));
Mark60:;
					// continue
				}
								}			}goto Mark410;
Mark70:;
			// continue//* 
			//*        Special code for 4 x 4 Householder 
			//* 
			
			_z1p8kxoj = *(_ycxba85s+((int)1 - 1));
			_dkyrz0g2 = (_0446f4de * _z1p8kxoj);
			_j3ir6imc = *(_ycxba85s+((int)2 - 1));
			_jzq4t39k = (_0446f4de * _j3ir6imc);
			_s8neut4h = *(_ycxba85s+((int)3 - 1));
			_sihp0s0u = (_0446f4de * _s8neut4h);
			_7ycc0bo5 = *(_ycxba85s+((int)4 - 1));
			_9i8orex5 = (_0446f4de * _7ycc0bo5);
			{
				System.Int32 __81fgg2dlsvn2396 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2396 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2396;
				for (__81fgg2count2396 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2396 + __81fgg2step2396) / __81fgg2step2396)), _znpjgsef = __81fgg2dlsvn2396; __81fgg2count2396 != 0; __81fgg2count2396--, _znpjgsef += (__81fgg2step2396)) {

				{
					
					_6j9l5fwy = ((((_z1p8kxoj * *(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))) + (_j3ir6imc * *(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_s8neut4h * *(_3crf0qn3+((int)3 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_7ycc0bo5 * *(_3crf0qn3+((int)4 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))));
					*(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _dkyrz0g2));
					*(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _jzq4t39k));
					*(_3crf0qn3+((int)3 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)3 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _sihp0s0u));
					*(_3crf0qn3+((int)4 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)4 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _9i8orex5));
Mark80:;
					// continue
				}
								}			}goto Mark410;
Mark90:;
			// continue//* 
			//*        Special code for 5 x 5 Householder 
			//* 
			
			_z1p8kxoj = *(_ycxba85s+((int)1 - 1));
			_dkyrz0g2 = (_0446f4de * _z1p8kxoj);
			_j3ir6imc = *(_ycxba85s+((int)2 - 1));
			_jzq4t39k = (_0446f4de * _j3ir6imc);
			_s8neut4h = *(_ycxba85s+((int)3 - 1));
			_sihp0s0u = (_0446f4de * _s8neut4h);
			_7ycc0bo5 = *(_ycxba85s+((int)4 - 1));
			_9i8orex5 = (_0446f4de * _7ycc0bo5);
			_6ifoo5vu = *(_ycxba85s+((int)5 - 1));
			_t3e90rm8 = (_0446f4de * _6ifoo5vu);
			{
				System.Int32 __81fgg2dlsvn2397 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2397 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2397;
				for (__81fgg2count2397 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2397 + __81fgg2step2397) / __81fgg2step2397)), _znpjgsef = __81fgg2dlsvn2397; __81fgg2count2397 != 0; __81fgg2count2397--, _znpjgsef += (__81fgg2step2397)) {

				{
					
					_6j9l5fwy = (((((_z1p8kxoj * *(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))) + (_j3ir6imc * *(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_s8neut4h * *(_3crf0qn3+((int)3 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_7ycc0bo5 * *(_3crf0qn3+((int)4 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_6ifoo5vu * *(_3crf0qn3+((int)5 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))));
					*(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _dkyrz0g2));
					*(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _jzq4t39k));
					*(_3crf0qn3+((int)3 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)3 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _sihp0s0u));
					*(_3crf0qn3+((int)4 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)4 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _9i8orex5));
					*(_3crf0qn3+((int)5 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)5 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _t3e90rm8));
Mark100:;
					// continue
				}
								}			}goto Mark410;
Mark110:;
			// continue//* 
			//*        Special code for 6 x 6 Householder 
			//* 
			
			_z1p8kxoj = *(_ycxba85s+((int)1 - 1));
			_dkyrz0g2 = (_0446f4de * _z1p8kxoj);
			_j3ir6imc = *(_ycxba85s+((int)2 - 1));
			_jzq4t39k = (_0446f4de * _j3ir6imc);
			_s8neut4h = *(_ycxba85s+((int)3 - 1));
			_sihp0s0u = (_0446f4de * _s8neut4h);
			_7ycc0bo5 = *(_ycxba85s+((int)4 - 1));
			_9i8orex5 = (_0446f4de * _7ycc0bo5);
			_6ifoo5vu = *(_ycxba85s+((int)5 - 1));
			_t3e90rm8 = (_0446f4de * _6ifoo5vu);
			_4qg46pc0 = *(_ycxba85s+((int)6 - 1));
			_phglqglk = (_0446f4de * _4qg46pc0);
			{
				System.Int32 __81fgg2dlsvn2398 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2398 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2398;
				for (__81fgg2count2398 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2398 + __81fgg2step2398) / __81fgg2step2398)), _znpjgsef = __81fgg2dlsvn2398; __81fgg2count2398 != 0; __81fgg2count2398--, _znpjgsef += (__81fgg2step2398)) {

				{
					
					_6j9l5fwy = ((((((_z1p8kxoj * *(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))) + (_j3ir6imc * *(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_s8neut4h * *(_3crf0qn3+((int)3 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_7ycc0bo5 * *(_3crf0qn3+((int)4 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_6ifoo5vu * *(_3crf0qn3+((int)5 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_4qg46pc0 * *(_3crf0qn3+((int)6 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))));
					*(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _dkyrz0g2));
					*(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _jzq4t39k));
					*(_3crf0qn3+((int)3 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)3 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _sihp0s0u));
					*(_3crf0qn3+((int)4 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)4 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _9i8orex5));
					*(_3crf0qn3+((int)5 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)5 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _t3e90rm8));
					*(_3crf0qn3+((int)6 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)6 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _phglqglk));
Mark120:;
					// continue
				}
								}			}goto Mark410;
Mark130:;
			// continue//* 
			//*        Special code for 7 x 7 Householder 
			//* 
			
			_z1p8kxoj = *(_ycxba85s+((int)1 - 1));
			_dkyrz0g2 = (_0446f4de * _z1p8kxoj);
			_j3ir6imc = *(_ycxba85s+((int)2 - 1));
			_jzq4t39k = (_0446f4de * _j3ir6imc);
			_s8neut4h = *(_ycxba85s+((int)3 - 1));
			_sihp0s0u = (_0446f4de * _s8neut4h);
			_7ycc0bo5 = *(_ycxba85s+((int)4 - 1));
			_9i8orex5 = (_0446f4de * _7ycc0bo5);
			_6ifoo5vu = *(_ycxba85s+((int)5 - 1));
			_t3e90rm8 = (_0446f4de * _6ifoo5vu);
			_4qg46pc0 = *(_ycxba85s+((int)6 - 1));
			_phglqglk = (_0446f4de * _4qg46pc0);
			_ow8yjime = *(_ycxba85s+((int)7 - 1));
			_mpowry76 = (_0446f4de * _ow8yjime);
			{
				System.Int32 __81fgg2dlsvn2399 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2399 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2399;
				for (__81fgg2count2399 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2399 + __81fgg2step2399) / __81fgg2step2399)), _znpjgsef = __81fgg2dlsvn2399; __81fgg2count2399 != 0; __81fgg2count2399--, _znpjgsef += (__81fgg2step2399)) {

				{
					
					_6j9l5fwy = (((((((_z1p8kxoj * *(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))) + (_j3ir6imc * *(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_s8neut4h * *(_3crf0qn3+((int)3 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_7ycc0bo5 * *(_3crf0qn3+((int)4 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_6ifoo5vu * *(_3crf0qn3+((int)5 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_4qg46pc0 * *(_3crf0qn3+((int)6 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_ow8yjime * *(_3crf0qn3+((int)7 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))));
					*(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _dkyrz0g2));
					*(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _jzq4t39k));
					*(_3crf0qn3+((int)3 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)3 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _sihp0s0u));
					*(_3crf0qn3+((int)4 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)4 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _9i8orex5));
					*(_3crf0qn3+((int)5 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)5 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _t3e90rm8));
					*(_3crf0qn3+((int)6 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)6 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _phglqglk));
					*(_3crf0qn3+((int)7 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)7 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _mpowry76));
Mark140:;
					// continue
				}
								}			}goto Mark410;
Mark150:;
			// continue//* 
			//*        Special code for 8 x 8 Householder 
			//* 
			
			_z1p8kxoj = *(_ycxba85s+((int)1 - 1));
			_dkyrz0g2 = (_0446f4de * _z1p8kxoj);
			_j3ir6imc = *(_ycxba85s+((int)2 - 1));
			_jzq4t39k = (_0446f4de * _j3ir6imc);
			_s8neut4h = *(_ycxba85s+((int)3 - 1));
			_sihp0s0u = (_0446f4de * _s8neut4h);
			_7ycc0bo5 = *(_ycxba85s+((int)4 - 1));
			_9i8orex5 = (_0446f4de * _7ycc0bo5);
			_6ifoo5vu = *(_ycxba85s+((int)5 - 1));
			_t3e90rm8 = (_0446f4de * _6ifoo5vu);
			_4qg46pc0 = *(_ycxba85s+((int)6 - 1));
			_phglqglk = (_0446f4de * _4qg46pc0);
			_ow8yjime = *(_ycxba85s+((int)7 - 1));
			_mpowry76 = (_0446f4de * _ow8yjime);
			_8jty3k9d = *(_ycxba85s+((int)8 - 1));
			_4ldqm5m0 = (_0446f4de * _8jty3k9d);
			{
				System.Int32 __81fgg2dlsvn2400 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2400 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2400;
				for (__81fgg2count2400 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2400 + __81fgg2step2400) / __81fgg2step2400)), _znpjgsef = __81fgg2dlsvn2400; __81fgg2count2400 != 0; __81fgg2count2400--, _znpjgsef += (__81fgg2step2400)) {

				{
					
					_6j9l5fwy = ((((((((_z1p8kxoj * *(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))) + (_j3ir6imc * *(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_s8neut4h * *(_3crf0qn3+((int)3 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_7ycc0bo5 * *(_3crf0qn3+((int)4 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_6ifoo5vu * *(_3crf0qn3+((int)5 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_4qg46pc0 * *(_3crf0qn3+((int)6 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_ow8yjime * *(_3crf0qn3+((int)7 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_8jty3k9d * *(_3crf0qn3+((int)8 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))));
					*(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _dkyrz0g2));
					*(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _jzq4t39k));
					*(_3crf0qn3+((int)3 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)3 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _sihp0s0u));
					*(_3crf0qn3+((int)4 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)4 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _9i8orex5));
					*(_3crf0qn3+((int)5 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)5 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _t3e90rm8));
					*(_3crf0qn3+((int)6 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)6 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _phglqglk));
					*(_3crf0qn3+((int)7 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)7 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _mpowry76));
					*(_3crf0qn3+((int)8 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)8 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _4ldqm5m0));
Mark160:;
					// continue
				}
								}			}goto Mark410;
Mark170:;
			// continue//* 
			//*        Special code for 9 x 9 Householder 
			//* 
			
			_z1p8kxoj = *(_ycxba85s+((int)1 - 1));
			_dkyrz0g2 = (_0446f4de * _z1p8kxoj);
			_j3ir6imc = *(_ycxba85s+((int)2 - 1));
			_jzq4t39k = (_0446f4de * _j3ir6imc);
			_s8neut4h = *(_ycxba85s+((int)3 - 1));
			_sihp0s0u = (_0446f4de * _s8neut4h);
			_7ycc0bo5 = *(_ycxba85s+((int)4 - 1));
			_9i8orex5 = (_0446f4de * _7ycc0bo5);
			_6ifoo5vu = *(_ycxba85s+((int)5 - 1));
			_t3e90rm8 = (_0446f4de * _6ifoo5vu);
			_4qg46pc0 = *(_ycxba85s+((int)6 - 1));
			_phglqglk = (_0446f4de * _4qg46pc0);
			_ow8yjime = *(_ycxba85s+((int)7 - 1));
			_mpowry76 = (_0446f4de * _ow8yjime);
			_8jty3k9d = *(_ycxba85s+((int)8 - 1));
			_4ldqm5m0 = (_0446f4de * _8jty3k9d);
			_ho99rwm2 = *(_ycxba85s+((int)9 - 1));
			_evj5hwb1 = (_0446f4de * _ho99rwm2);
			{
				System.Int32 __81fgg2dlsvn2401 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2401 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2401;
				for (__81fgg2count2401 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2401 + __81fgg2step2401) / __81fgg2step2401)), _znpjgsef = __81fgg2dlsvn2401; __81fgg2count2401 != 0; __81fgg2count2401--, _znpjgsef += (__81fgg2step2401)) {

				{
					
					_6j9l5fwy = (((((((((_z1p8kxoj * *(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))) + (_j3ir6imc * *(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_s8neut4h * *(_3crf0qn3+((int)3 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_7ycc0bo5 * *(_3crf0qn3+((int)4 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_6ifoo5vu * *(_3crf0qn3+((int)5 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_4qg46pc0 * *(_3crf0qn3+((int)6 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_ow8yjime * *(_3crf0qn3+((int)7 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_8jty3k9d * *(_3crf0qn3+((int)8 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_ho99rwm2 * *(_3crf0qn3+((int)9 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))));
					*(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _dkyrz0g2));
					*(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _jzq4t39k));
					*(_3crf0qn3+((int)3 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)3 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _sihp0s0u));
					*(_3crf0qn3+((int)4 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)4 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _9i8orex5));
					*(_3crf0qn3+((int)5 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)5 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _t3e90rm8));
					*(_3crf0qn3+((int)6 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)6 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _phglqglk));
					*(_3crf0qn3+((int)7 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)7 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _mpowry76));
					*(_3crf0qn3+((int)8 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)8 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _4ldqm5m0));
					*(_3crf0qn3+((int)9 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)9 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _evj5hwb1));
Mark180:;
					// continue
				}
								}			}goto Mark410;
Mark190:;
			// continue//* 
			//*        Special code for 10 x 10 Householder 
			//* 
			
			_z1p8kxoj = *(_ycxba85s+((int)1 - 1));
			_dkyrz0g2 = (_0446f4de * _z1p8kxoj);
			_j3ir6imc = *(_ycxba85s+((int)2 - 1));
			_jzq4t39k = (_0446f4de * _j3ir6imc);
			_s8neut4h = *(_ycxba85s+((int)3 - 1));
			_sihp0s0u = (_0446f4de * _s8neut4h);
			_7ycc0bo5 = *(_ycxba85s+((int)4 - 1));
			_9i8orex5 = (_0446f4de * _7ycc0bo5);
			_6ifoo5vu = *(_ycxba85s+((int)5 - 1));
			_t3e90rm8 = (_0446f4de * _6ifoo5vu);
			_4qg46pc0 = *(_ycxba85s+((int)6 - 1));
			_phglqglk = (_0446f4de * _4qg46pc0);
			_ow8yjime = *(_ycxba85s+((int)7 - 1));
			_mpowry76 = (_0446f4de * _ow8yjime);
			_8jty3k9d = *(_ycxba85s+((int)8 - 1));
			_4ldqm5m0 = (_0446f4de * _8jty3k9d);
			_ho99rwm2 = *(_ycxba85s+((int)9 - 1));
			_evj5hwb1 = (_0446f4de * _ho99rwm2);
			_y2pyu0n3 = *(_ycxba85s+((int)10 - 1));
			_ia0551b4 = (_0446f4de * _y2pyu0n3);
			{
				System.Int32 __81fgg2dlsvn2402 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2402 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2402;
				for (__81fgg2count2402 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2402 + __81fgg2step2402) / __81fgg2step2402)), _znpjgsef = __81fgg2dlsvn2402; __81fgg2count2402 != 0; __81fgg2count2402--, _znpjgsef += (__81fgg2step2402)) {

				{
					
					_6j9l5fwy = ((((((((((_z1p8kxoj * *(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))) + (_j3ir6imc * *(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_s8neut4h * *(_3crf0qn3+((int)3 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_7ycc0bo5 * *(_3crf0qn3+((int)4 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_6ifoo5vu * *(_3crf0qn3+((int)5 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_4qg46pc0 * *(_3crf0qn3+((int)6 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_ow8yjime * *(_3crf0qn3+((int)7 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_8jty3k9d * *(_3crf0qn3+((int)8 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_ho99rwm2 * *(_3crf0qn3+((int)9 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)))) + (_y2pyu0n3 * *(_3crf0qn3+((int)10 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4))));
					*(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _dkyrz0g2));
					*(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _jzq4t39k));
					*(_3crf0qn3+((int)3 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)3 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _sihp0s0u));
					*(_3crf0qn3+((int)4 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)4 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _9i8orex5));
					*(_3crf0qn3+((int)5 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)5 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _t3e90rm8));
					*(_3crf0qn3+((int)6 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)6 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _phglqglk));
					*(_3crf0qn3+((int)7 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)7 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _mpowry76));
					*(_3crf0qn3+((int)8 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)8 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _4ldqm5m0));
					*(_3crf0qn3+((int)9 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)9 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _evj5hwb1));
					*(_3crf0qn3+((int)10 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+((int)10 - 1) + (_znpjgsef - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _ia0551b4));
Mark200:;
					// continue
				}
								}			}goto Mark410;
		}
		else
		{
			//* 
			//*        Form  C * H, where H has order n. 
			//* 
			
			switch (_dxpq0xkr) {
								case 1:
				goto Mark210;
				case 2:
				goto Mark230;
				case 3:
				goto Mark250;
				case 4:
				goto Mark270;
				case 5:
				goto Mark290;
				case 6:
				goto Mark310;
				case 7:
				goto Mark330;
				case 8:
				goto Mark350;
				case 9:
				goto Mark370;
				case 10:
				goto Mark390;
				default:
				break;
			}
//* 
			//*        Code for general N 
			//* 
			
			_tfywat2m(_m2cn2gjg ,ref _ev4xhht5 ,ref _dxpq0xkr ,_ycxba85s ,ref Unsafe.AsRef((int)1) ,ref _0446f4de ,_3crf0qn3 ,ref _1s3eymp4 ,_apig8meb );goto Mark410;
Mark210:;
			// continue//* 
			//*        Special code for 1 x 1 Householder 
			//* 
			
			_dkyrz0g2 = (_kxg5drh2 - ((_0446f4de * *(_ycxba85s+((int)1 - 1))) * *(_ycxba85s+((int)1 - 1))));
			{
				System.Int32 __81fgg2dlsvn2403 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2403 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2403;
				for (__81fgg2count2403 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn2403 + __81fgg2step2403) / __81fgg2step2403)), _znpjgsef = __81fgg2dlsvn2403; __81fgg2count2403 != 0; __81fgg2count2403--, _znpjgsef += (__81fgg2step2403)) {

				{
					
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)) = (_dkyrz0g2 * *(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)));
Mark220:;
					// continue
				}
								}			}goto Mark410;
Mark230:;
			// continue//* 
			//*        Special code for 2 x 2 Householder 
			//* 
			
			_z1p8kxoj = *(_ycxba85s+((int)1 - 1));
			_dkyrz0g2 = (_0446f4de * _z1p8kxoj);
			_j3ir6imc = *(_ycxba85s+((int)2 - 1));
			_jzq4t39k = (_0446f4de * _j3ir6imc);
			{
				System.Int32 __81fgg2dlsvn2404 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2404 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2404;
				for (__81fgg2count2404 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn2404 + __81fgg2step2404) / __81fgg2step2404)), _znpjgsef = __81fgg2dlsvn2404; __81fgg2count2404 != 0; __81fgg2count2404--, _znpjgsef += (__81fgg2step2404)) {

				{
					
					_6j9l5fwy = ((_z1p8kxoj * *(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4))) + (_j3ir6imc * *(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4))));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _dkyrz0g2));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _jzq4t39k));
Mark240:;
					// continue
				}
								}			}goto Mark410;
Mark250:;
			// continue//* 
			//*        Special code for 3 x 3 Householder 
			//* 
			
			_z1p8kxoj = *(_ycxba85s+((int)1 - 1));
			_dkyrz0g2 = (_0446f4de * _z1p8kxoj);
			_j3ir6imc = *(_ycxba85s+((int)2 - 1));
			_jzq4t39k = (_0446f4de * _j3ir6imc);
			_s8neut4h = *(_ycxba85s+((int)3 - 1));
			_sihp0s0u = (_0446f4de * _s8neut4h);
			{
				System.Int32 __81fgg2dlsvn2405 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2405 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2405;
				for (__81fgg2count2405 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn2405 + __81fgg2step2405) / __81fgg2step2405)), _znpjgsef = __81fgg2dlsvn2405; __81fgg2count2405 != 0; __81fgg2count2405--, _znpjgsef += (__81fgg2step2405)) {

				{
					
					_6j9l5fwy = (((_z1p8kxoj * *(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4))) + (_j3ir6imc * *(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4)))) + (_s8neut4h * *(_3crf0qn3+(_znpjgsef - 1) + ((int)3 - 1) * 1 * (_1s3eymp4))));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _dkyrz0g2));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _jzq4t39k));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)3 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)3 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _sihp0s0u));
Mark260:;
					// continue
				}
								}			}goto Mark410;
Mark270:;
			// continue//* 
			//*        Special code for 4 x 4 Householder 
			//* 
			
			_z1p8kxoj = *(_ycxba85s+((int)1 - 1));
			_dkyrz0g2 = (_0446f4de * _z1p8kxoj);
			_j3ir6imc = *(_ycxba85s+((int)2 - 1));
			_jzq4t39k = (_0446f4de * _j3ir6imc);
			_s8neut4h = *(_ycxba85s+((int)3 - 1));
			_sihp0s0u = (_0446f4de * _s8neut4h);
			_7ycc0bo5 = *(_ycxba85s+((int)4 - 1));
			_9i8orex5 = (_0446f4de * _7ycc0bo5);
			{
				System.Int32 __81fgg2dlsvn2406 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2406 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2406;
				for (__81fgg2count2406 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn2406 + __81fgg2step2406) / __81fgg2step2406)), _znpjgsef = __81fgg2dlsvn2406; __81fgg2count2406 != 0; __81fgg2count2406--, _znpjgsef += (__81fgg2step2406)) {

				{
					
					_6j9l5fwy = ((((_z1p8kxoj * *(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4))) + (_j3ir6imc * *(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4)))) + (_s8neut4h * *(_3crf0qn3+(_znpjgsef - 1) + ((int)3 - 1) * 1 * (_1s3eymp4)))) + (_7ycc0bo5 * *(_3crf0qn3+(_znpjgsef - 1) + ((int)4 - 1) * 1 * (_1s3eymp4))));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _dkyrz0g2));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _jzq4t39k));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)3 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)3 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _sihp0s0u));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)4 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)4 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _9i8orex5));
Mark280:;
					// continue
				}
								}			}goto Mark410;
Mark290:;
			// continue//* 
			//*        Special code for 5 x 5 Householder 
			//* 
			
			_z1p8kxoj = *(_ycxba85s+((int)1 - 1));
			_dkyrz0g2 = (_0446f4de * _z1p8kxoj);
			_j3ir6imc = *(_ycxba85s+((int)2 - 1));
			_jzq4t39k = (_0446f4de * _j3ir6imc);
			_s8neut4h = *(_ycxba85s+((int)3 - 1));
			_sihp0s0u = (_0446f4de * _s8neut4h);
			_7ycc0bo5 = *(_ycxba85s+((int)4 - 1));
			_9i8orex5 = (_0446f4de * _7ycc0bo5);
			_6ifoo5vu = *(_ycxba85s+((int)5 - 1));
			_t3e90rm8 = (_0446f4de * _6ifoo5vu);
			{
				System.Int32 __81fgg2dlsvn2407 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2407 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2407;
				for (__81fgg2count2407 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn2407 + __81fgg2step2407) / __81fgg2step2407)), _znpjgsef = __81fgg2dlsvn2407; __81fgg2count2407 != 0; __81fgg2count2407--, _znpjgsef += (__81fgg2step2407)) {

				{
					
					_6j9l5fwy = (((((_z1p8kxoj * *(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4))) + (_j3ir6imc * *(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4)))) + (_s8neut4h * *(_3crf0qn3+(_znpjgsef - 1) + ((int)3 - 1) * 1 * (_1s3eymp4)))) + (_7ycc0bo5 * *(_3crf0qn3+(_znpjgsef - 1) + ((int)4 - 1) * 1 * (_1s3eymp4)))) + (_6ifoo5vu * *(_3crf0qn3+(_znpjgsef - 1) + ((int)5 - 1) * 1 * (_1s3eymp4))));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _dkyrz0g2));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _jzq4t39k));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)3 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)3 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _sihp0s0u));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)4 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)4 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _9i8orex5));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)5 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)5 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _t3e90rm8));
Mark300:;
					// continue
				}
								}			}goto Mark410;
Mark310:;
			// continue//* 
			//*        Special code for 6 x 6 Householder 
			//* 
			
			_z1p8kxoj = *(_ycxba85s+((int)1 - 1));
			_dkyrz0g2 = (_0446f4de * _z1p8kxoj);
			_j3ir6imc = *(_ycxba85s+((int)2 - 1));
			_jzq4t39k = (_0446f4de * _j3ir6imc);
			_s8neut4h = *(_ycxba85s+((int)3 - 1));
			_sihp0s0u = (_0446f4de * _s8neut4h);
			_7ycc0bo5 = *(_ycxba85s+((int)4 - 1));
			_9i8orex5 = (_0446f4de * _7ycc0bo5);
			_6ifoo5vu = *(_ycxba85s+((int)5 - 1));
			_t3e90rm8 = (_0446f4de * _6ifoo5vu);
			_4qg46pc0 = *(_ycxba85s+((int)6 - 1));
			_phglqglk = (_0446f4de * _4qg46pc0);
			{
				System.Int32 __81fgg2dlsvn2408 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2408 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2408;
				for (__81fgg2count2408 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn2408 + __81fgg2step2408) / __81fgg2step2408)), _znpjgsef = __81fgg2dlsvn2408; __81fgg2count2408 != 0; __81fgg2count2408--, _znpjgsef += (__81fgg2step2408)) {

				{
					
					_6j9l5fwy = ((((((_z1p8kxoj * *(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4))) + (_j3ir6imc * *(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4)))) + (_s8neut4h * *(_3crf0qn3+(_znpjgsef - 1) + ((int)3 - 1) * 1 * (_1s3eymp4)))) + (_7ycc0bo5 * *(_3crf0qn3+(_znpjgsef - 1) + ((int)4 - 1) * 1 * (_1s3eymp4)))) + (_6ifoo5vu * *(_3crf0qn3+(_znpjgsef - 1) + ((int)5 - 1) * 1 * (_1s3eymp4)))) + (_4qg46pc0 * *(_3crf0qn3+(_znpjgsef - 1) + ((int)6 - 1) * 1 * (_1s3eymp4))));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _dkyrz0g2));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _jzq4t39k));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)3 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)3 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _sihp0s0u));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)4 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)4 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _9i8orex5));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)5 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)5 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _t3e90rm8));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)6 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)6 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _phglqglk));
Mark320:;
					// continue
				}
								}			}goto Mark410;
Mark330:;
			// continue//* 
			//*        Special code for 7 x 7 Householder 
			//* 
			
			_z1p8kxoj = *(_ycxba85s+((int)1 - 1));
			_dkyrz0g2 = (_0446f4de * _z1p8kxoj);
			_j3ir6imc = *(_ycxba85s+((int)2 - 1));
			_jzq4t39k = (_0446f4de * _j3ir6imc);
			_s8neut4h = *(_ycxba85s+((int)3 - 1));
			_sihp0s0u = (_0446f4de * _s8neut4h);
			_7ycc0bo5 = *(_ycxba85s+((int)4 - 1));
			_9i8orex5 = (_0446f4de * _7ycc0bo5);
			_6ifoo5vu = *(_ycxba85s+((int)5 - 1));
			_t3e90rm8 = (_0446f4de * _6ifoo5vu);
			_4qg46pc0 = *(_ycxba85s+((int)6 - 1));
			_phglqglk = (_0446f4de * _4qg46pc0);
			_ow8yjime = *(_ycxba85s+((int)7 - 1));
			_mpowry76 = (_0446f4de * _ow8yjime);
			{
				System.Int32 __81fgg2dlsvn2409 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2409 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2409;
				for (__81fgg2count2409 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn2409 + __81fgg2step2409) / __81fgg2step2409)), _znpjgsef = __81fgg2dlsvn2409; __81fgg2count2409 != 0; __81fgg2count2409--, _znpjgsef += (__81fgg2step2409)) {

				{
					
					_6j9l5fwy = (((((((_z1p8kxoj * *(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4))) + (_j3ir6imc * *(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4)))) + (_s8neut4h * *(_3crf0qn3+(_znpjgsef - 1) + ((int)3 - 1) * 1 * (_1s3eymp4)))) + (_7ycc0bo5 * *(_3crf0qn3+(_znpjgsef - 1) + ((int)4 - 1) * 1 * (_1s3eymp4)))) + (_6ifoo5vu * *(_3crf0qn3+(_znpjgsef - 1) + ((int)5 - 1) * 1 * (_1s3eymp4)))) + (_4qg46pc0 * *(_3crf0qn3+(_znpjgsef - 1) + ((int)6 - 1) * 1 * (_1s3eymp4)))) + (_ow8yjime * *(_3crf0qn3+(_znpjgsef - 1) + ((int)7 - 1) * 1 * (_1s3eymp4))));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _dkyrz0g2));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _jzq4t39k));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)3 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)3 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _sihp0s0u));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)4 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)4 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _9i8orex5));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)5 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)5 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _t3e90rm8));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)6 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)6 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _phglqglk));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)7 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)7 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _mpowry76));
Mark340:;
					// continue
				}
								}			}goto Mark410;
Mark350:;
			// continue//* 
			//*        Special code for 8 x 8 Householder 
			//* 
			
			_z1p8kxoj = *(_ycxba85s+((int)1 - 1));
			_dkyrz0g2 = (_0446f4de * _z1p8kxoj);
			_j3ir6imc = *(_ycxba85s+((int)2 - 1));
			_jzq4t39k = (_0446f4de * _j3ir6imc);
			_s8neut4h = *(_ycxba85s+((int)3 - 1));
			_sihp0s0u = (_0446f4de * _s8neut4h);
			_7ycc0bo5 = *(_ycxba85s+((int)4 - 1));
			_9i8orex5 = (_0446f4de * _7ycc0bo5);
			_6ifoo5vu = *(_ycxba85s+((int)5 - 1));
			_t3e90rm8 = (_0446f4de * _6ifoo5vu);
			_4qg46pc0 = *(_ycxba85s+((int)6 - 1));
			_phglqglk = (_0446f4de * _4qg46pc0);
			_ow8yjime = *(_ycxba85s+((int)7 - 1));
			_mpowry76 = (_0446f4de * _ow8yjime);
			_8jty3k9d = *(_ycxba85s+((int)8 - 1));
			_4ldqm5m0 = (_0446f4de * _8jty3k9d);
			{
				System.Int32 __81fgg2dlsvn2410 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2410 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2410;
				for (__81fgg2count2410 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn2410 + __81fgg2step2410) / __81fgg2step2410)), _znpjgsef = __81fgg2dlsvn2410; __81fgg2count2410 != 0; __81fgg2count2410--, _znpjgsef += (__81fgg2step2410)) {

				{
					
					_6j9l5fwy = ((((((((_z1p8kxoj * *(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4))) + (_j3ir6imc * *(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4)))) + (_s8neut4h * *(_3crf0qn3+(_znpjgsef - 1) + ((int)3 - 1) * 1 * (_1s3eymp4)))) + (_7ycc0bo5 * *(_3crf0qn3+(_znpjgsef - 1) + ((int)4 - 1) * 1 * (_1s3eymp4)))) + (_6ifoo5vu * *(_3crf0qn3+(_znpjgsef - 1) + ((int)5 - 1) * 1 * (_1s3eymp4)))) + (_4qg46pc0 * *(_3crf0qn3+(_znpjgsef - 1) + ((int)6 - 1) * 1 * (_1s3eymp4)))) + (_ow8yjime * *(_3crf0qn3+(_znpjgsef - 1) + ((int)7 - 1) * 1 * (_1s3eymp4)))) + (_8jty3k9d * *(_3crf0qn3+(_znpjgsef - 1) + ((int)8 - 1) * 1 * (_1s3eymp4))));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _dkyrz0g2));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _jzq4t39k));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)3 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)3 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _sihp0s0u));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)4 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)4 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _9i8orex5));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)5 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)5 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _t3e90rm8));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)6 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)6 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _phglqglk));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)7 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)7 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _mpowry76));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)8 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)8 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _4ldqm5m0));
Mark360:;
					// continue
				}
								}			}goto Mark410;
Mark370:;
			// continue//* 
			//*        Special code for 9 x 9 Householder 
			//* 
			
			_z1p8kxoj = *(_ycxba85s+((int)1 - 1));
			_dkyrz0g2 = (_0446f4de * _z1p8kxoj);
			_j3ir6imc = *(_ycxba85s+((int)2 - 1));
			_jzq4t39k = (_0446f4de * _j3ir6imc);
			_s8neut4h = *(_ycxba85s+((int)3 - 1));
			_sihp0s0u = (_0446f4de * _s8neut4h);
			_7ycc0bo5 = *(_ycxba85s+((int)4 - 1));
			_9i8orex5 = (_0446f4de * _7ycc0bo5);
			_6ifoo5vu = *(_ycxba85s+((int)5 - 1));
			_t3e90rm8 = (_0446f4de * _6ifoo5vu);
			_4qg46pc0 = *(_ycxba85s+((int)6 - 1));
			_phglqglk = (_0446f4de * _4qg46pc0);
			_ow8yjime = *(_ycxba85s+((int)7 - 1));
			_mpowry76 = (_0446f4de * _ow8yjime);
			_8jty3k9d = *(_ycxba85s+((int)8 - 1));
			_4ldqm5m0 = (_0446f4de * _8jty3k9d);
			_ho99rwm2 = *(_ycxba85s+((int)9 - 1));
			_evj5hwb1 = (_0446f4de * _ho99rwm2);
			{
				System.Int32 __81fgg2dlsvn2411 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2411 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2411;
				for (__81fgg2count2411 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn2411 + __81fgg2step2411) / __81fgg2step2411)), _znpjgsef = __81fgg2dlsvn2411; __81fgg2count2411 != 0; __81fgg2count2411--, _znpjgsef += (__81fgg2step2411)) {

				{
					
					_6j9l5fwy = (((((((((_z1p8kxoj * *(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4))) + (_j3ir6imc * *(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4)))) + (_s8neut4h * *(_3crf0qn3+(_znpjgsef - 1) + ((int)3 - 1) * 1 * (_1s3eymp4)))) + (_7ycc0bo5 * *(_3crf0qn3+(_znpjgsef - 1) + ((int)4 - 1) * 1 * (_1s3eymp4)))) + (_6ifoo5vu * *(_3crf0qn3+(_znpjgsef - 1) + ((int)5 - 1) * 1 * (_1s3eymp4)))) + (_4qg46pc0 * *(_3crf0qn3+(_znpjgsef - 1) + ((int)6 - 1) * 1 * (_1s3eymp4)))) + (_ow8yjime * *(_3crf0qn3+(_znpjgsef - 1) + ((int)7 - 1) * 1 * (_1s3eymp4)))) + (_8jty3k9d * *(_3crf0qn3+(_znpjgsef - 1) + ((int)8 - 1) * 1 * (_1s3eymp4)))) + (_ho99rwm2 * *(_3crf0qn3+(_znpjgsef - 1) + ((int)9 - 1) * 1 * (_1s3eymp4))));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _dkyrz0g2));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _jzq4t39k));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)3 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)3 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _sihp0s0u));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)4 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)4 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _9i8orex5));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)5 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)5 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _t3e90rm8));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)6 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)6 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _phglqglk));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)7 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)7 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _mpowry76));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)8 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)8 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _4ldqm5m0));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)9 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)9 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _evj5hwb1));
Mark380:;
					// continue
				}
								}			}goto Mark410;
Mark390:;
			// continue//* 
			//*        Special code for 10 x 10 Householder 
			//* 
			
			_z1p8kxoj = *(_ycxba85s+((int)1 - 1));
			_dkyrz0g2 = (_0446f4de * _z1p8kxoj);
			_j3ir6imc = *(_ycxba85s+((int)2 - 1));
			_jzq4t39k = (_0446f4de * _j3ir6imc);
			_s8neut4h = *(_ycxba85s+((int)3 - 1));
			_sihp0s0u = (_0446f4de * _s8neut4h);
			_7ycc0bo5 = *(_ycxba85s+((int)4 - 1));
			_9i8orex5 = (_0446f4de * _7ycc0bo5);
			_6ifoo5vu = *(_ycxba85s+((int)5 - 1));
			_t3e90rm8 = (_0446f4de * _6ifoo5vu);
			_4qg46pc0 = *(_ycxba85s+((int)6 - 1));
			_phglqglk = (_0446f4de * _4qg46pc0);
			_ow8yjime = *(_ycxba85s+((int)7 - 1));
			_mpowry76 = (_0446f4de * _ow8yjime);
			_8jty3k9d = *(_ycxba85s+((int)8 - 1));
			_4ldqm5m0 = (_0446f4de * _8jty3k9d);
			_ho99rwm2 = *(_ycxba85s+((int)9 - 1));
			_evj5hwb1 = (_0446f4de * _ho99rwm2);
			_y2pyu0n3 = *(_ycxba85s+((int)10 - 1));
			_ia0551b4 = (_0446f4de * _y2pyu0n3);
			{
				System.Int32 __81fgg2dlsvn2412 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2412 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2412;
				for (__81fgg2count2412 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn2412 + __81fgg2step2412) / __81fgg2step2412)), _znpjgsef = __81fgg2dlsvn2412; __81fgg2count2412 != 0; __81fgg2count2412--, _znpjgsef += (__81fgg2step2412)) {

				{
					
					_6j9l5fwy = ((((((((((_z1p8kxoj * *(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4))) + (_j3ir6imc * *(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4)))) + (_s8neut4h * *(_3crf0qn3+(_znpjgsef - 1) + ((int)3 - 1) * 1 * (_1s3eymp4)))) + (_7ycc0bo5 * *(_3crf0qn3+(_znpjgsef - 1) + ((int)4 - 1) * 1 * (_1s3eymp4)))) + (_6ifoo5vu * *(_3crf0qn3+(_znpjgsef - 1) + ((int)5 - 1) * 1 * (_1s3eymp4)))) + (_4qg46pc0 * *(_3crf0qn3+(_znpjgsef - 1) + ((int)6 - 1) * 1 * (_1s3eymp4)))) + (_ow8yjime * *(_3crf0qn3+(_znpjgsef - 1) + ((int)7 - 1) * 1 * (_1s3eymp4)))) + (_8jty3k9d * *(_3crf0qn3+(_znpjgsef - 1) + ((int)8 - 1) * 1 * (_1s3eymp4)))) + (_ho99rwm2 * *(_3crf0qn3+(_znpjgsef - 1) + ((int)9 - 1) * 1 * (_1s3eymp4)))) + (_y2pyu0n3 * *(_3crf0qn3+(_znpjgsef - 1) + ((int)10 - 1) * 1 * (_1s3eymp4))));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _dkyrz0g2));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)2 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _jzq4t39k));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)3 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)3 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _sihp0s0u));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)4 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)4 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _9i8orex5));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)5 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)5 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _t3e90rm8));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)6 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)6 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _phglqglk));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)7 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)7 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _mpowry76));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)8 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)8 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _4ldqm5m0));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)9 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)9 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _evj5hwb1));
					*(_3crf0qn3+(_znpjgsef - 1) + ((int)10 - 1) * 1 * (_1s3eymp4)) = (*(_3crf0qn3+(_znpjgsef - 1) + ((int)10 - 1) * 1 * (_1s3eymp4)) - (_6j9l5fwy * _ia0551b4));
Mark400:;
					// continue
				}
								}			}goto Mark410;
		}
		
Mark410:;
		
		return;//* 
		//*     End of SLARFX 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
