
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
//*> \brief \b DLARFX applies an elementary reflector to a general rectangular matrix, with loop unrolling when the reflector has order ≤ 10. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLARFX + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlarfx.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlarfx.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlarfx.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLARFX( SIDE, M, N, V, TAU, C, LDC, WORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          SIDE 
//*       INTEGER            LDC, M, N 
//*       DOUBLE PRECISION   TAU 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   C( LDC, * ), V( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLARFX applies a real elementary reflector H to a real m by n 
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
//*>          V is DOUBLE PRECISION array, dimension (M) if SIDE = 'L' 
//*>                                     or (N) if SIDE = 'R' 
//*>          The vector v in the representation of H. 
//*> \endverbatim 
//*> 
//*> \param[in] TAU 
//*> \verbatim 
//*>          TAU is DOUBLE PRECISION 
//*>          The value tau in the representation of H. 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is DOUBLE PRECISION array, dimension (LDC,N) 
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
//*>          WORK is DOUBLE PRECISION array, dimension 
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
//*> \ingroup doubleOTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _42r3zmw1(FString _m2cn2gjg, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, Double* _ycxba85s, ref Double _0446f4de, Double* _3crf0qn3, ref Int32 _1s3eymp4, Double* _apig8meb)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Int32 _znpjgsef =  default;
Double _6j9l5fwy =  default;
Double _dkyrz0g2 =  default;
Double _ia0551b4 =  default;
Double _jzq4t39k =  default;
Double _sihp0s0u =  default;
Double _9i8orex5 =  default;
Double _t3e90rm8 =  default;
Double _phglqglk =  default;
Double _mpowry76 =  default;
Double _4ldqm5m0 =  default;
Double _evj5hwb1 =  default;
Double _z1p8kxoj =  default;
Double _y2pyu0n3 =  default;
Double _j3ir6imc =  default;
Double _s8neut4h =  default;
Double _7ycc0bo5 =  default;
Double _6ifoo5vu =  default;
Double _4qg46pc0 =  default;
Double _ow8yjime =  default;
Double _8jty3k9d =  default;
Double _ho99rwm2 =  default;
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
			
			_bbpftela(_m2cn2gjg ,ref _ev4xhht5 ,ref _dxpq0xkr ,_ycxba85s ,ref Unsafe.AsRef((int)1) ,ref _0446f4de ,_3crf0qn3 ,ref _1s3eymp4 ,_apig8meb );goto Mark410;
Mark10:;
			// continue//* 
			//*        Special code for 1 x 1 Householder 
			//* 
			
			_dkyrz0g2 = (_kxg5drh2 - ((_0446f4de * *(_ycxba85s+((int)1 - 1))) * *(_ycxba85s+((int)1 - 1))));
			{
				System.Int32 __81fgg2dlsvn2228 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2228 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2228;
				for (__81fgg2count2228 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2228 + __81fgg2step2228) / __81fgg2step2228)), _znpjgsef = __81fgg2dlsvn2228; __81fgg2count2228 != 0; __81fgg2count2228--, _znpjgsef += (__81fgg2step2228)) {

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
				System.Int32 __81fgg2dlsvn2229 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2229 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2229;
				for (__81fgg2count2229 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2229 + __81fgg2step2229) / __81fgg2step2229)), _znpjgsef = __81fgg2dlsvn2229; __81fgg2count2229 != 0; __81fgg2count2229--, _znpjgsef += (__81fgg2step2229)) {

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
				System.Int32 __81fgg2dlsvn2230 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2230 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2230;
				for (__81fgg2count2230 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2230 + __81fgg2step2230) / __81fgg2step2230)), _znpjgsef = __81fgg2dlsvn2230; __81fgg2count2230 != 0; __81fgg2count2230--, _znpjgsef += (__81fgg2step2230)) {

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
				System.Int32 __81fgg2dlsvn2231 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2231 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2231;
				for (__81fgg2count2231 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2231 + __81fgg2step2231) / __81fgg2step2231)), _znpjgsef = __81fgg2dlsvn2231; __81fgg2count2231 != 0; __81fgg2count2231--, _znpjgsef += (__81fgg2step2231)) {

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
				System.Int32 __81fgg2dlsvn2232 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2232 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2232;
				for (__81fgg2count2232 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2232 + __81fgg2step2232) / __81fgg2step2232)), _znpjgsef = __81fgg2dlsvn2232; __81fgg2count2232 != 0; __81fgg2count2232--, _znpjgsef += (__81fgg2step2232)) {

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
				System.Int32 __81fgg2dlsvn2233 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2233 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2233;
				for (__81fgg2count2233 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2233 + __81fgg2step2233) / __81fgg2step2233)), _znpjgsef = __81fgg2dlsvn2233; __81fgg2count2233 != 0; __81fgg2count2233--, _znpjgsef += (__81fgg2step2233)) {

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
				System.Int32 __81fgg2dlsvn2234 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2234 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2234;
				for (__81fgg2count2234 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2234 + __81fgg2step2234) / __81fgg2step2234)), _znpjgsef = __81fgg2dlsvn2234; __81fgg2count2234 != 0; __81fgg2count2234--, _znpjgsef += (__81fgg2step2234)) {

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
				System.Int32 __81fgg2dlsvn2235 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2235 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2235;
				for (__81fgg2count2235 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2235 + __81fgg2step2235) / __81fgg2step2235)), _znpjgsef = __81fgg2dlsvn2235; __81fgg2count2235 != 0; __81fgg2count2235--, _znpjgsef += (__81fgg2step2235)) {

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
				System.Int32 __81fgg2dlsvn2236 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2236 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2236;
				for (__81fgg2count2236 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2236 + __81fgg2step2236) / __81fgg2step2236)), _znpjgsef = __81fgg2dlsvn2236; __81fgg2count2236 != 0; __81fgg2count2236--, _znpjgsef += (__81fgg2step2236)) {

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
				System.Int32 __81fgg2dlsvn2237 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2237 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2237;
				for (__81fgg2count2237 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2237 + __81fgg2step2237) / __81fgg2step2237)), _znpjgsef = __81fgg2dlsvn2237; __81fgg2count2237 != 0; __81fgg2count2237--, _znpjgsef += (__81fgg2step2237)) {

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
			
			_bbpftela(_m2cn2gjg ,ref _ev4xhht5 ,ref _dxpq0xkr ,_ycxba85s ,ref Unsafe.AsRef((int)1) ,ref _0446f4de ,_3crf0qn3 ,ref _1s3eymp4 ,_apig8meb );goto Mark410;
Mark210:;
			// continue//* 
			//*        Special code for 1 x 1 Householder 
			//* 
			
			_dkyrz0g2 = (_kxg5drh2 - ((_0446f4de * *(_ycxba85s+((int)1 - 1))) * *(_ycxba85s+((int)1 - 1))));
			{
				System.Int32 __81fgg2dlsvn2238 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2238 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2238;
				for (__81fgg2count2238 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn2238 + __81fgg2step2238) / __81fgg2step2238)), _znpjgsef = __81fgg2dlsvn2238; __81fgg2count2238 != 0; __81fgg2count2238--, _znpjgsef += (__81fgg2step2238)) {

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
				System.Int32 __81fgg2dlsvn2239 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2239 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2239;
				for (__81fgg2count2239 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn2239 + __81fgg2step2239) / __81fgg2step2239)), _znpjgsef = __81fgg2dlsvn2239; __81fgg2count2239 != 0; __81fgg2count2239--, _znpjgsef += (__81fgg2step2239)) {

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
				System.Int32 __81fgg2dlsvn2240 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2240 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2240;
				for (__81fgg2count2240 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn2240 + __81fgg2step2240) / __81fgg2step2240)), _znpjgsef = __81fgg2dlsvn2240; __81fgg2count2240 != 0; __81fgg2count2240--, _znpjgsef += (__81fgg2step2240)) {

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
				System.Int32 __81fgg2dlsvn2241 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2241 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2241;
				for (__81fgg2count2241 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn2241 + __81fgg2step2241) / __81fgg2step2241)), _znpjgsef = __81fgg2dlsvn2241; __81fgg2count2241 != 0; __81fgg2count2241--, _znpjgsef += (__81fgg2step2241)) {

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
				System.Int32 __81fgg2dlsvn2242 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2242 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2242;
				for (__81fgg2count2242 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn2242 + __81fgg2step2242) / __81fgg2step2242)), _znpjgsef = __81fgg2dlsvn2242; __81fgg2count2242 != 0; __81fgg2count2242--, _znpjgsef += (__81fgg2step2242)) {

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
				System.Int32 __81fgg2dlsvn2243 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2243 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2243;
				for (__81fgg2count2243 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn2243 + __81fgg2step2243) / __81fgg2step2243)), _znpjgsef = __81fgg2dlsvn2243; __81fgg2count2243 != 0; __81fgg2count2243--, _znpjgsef += (__81fgg2step2243)) {

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
				System.Int32 __81fgg2dlsvn2244 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2244 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2244;
				for (__81fgg2count2244 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn2244 + __81fgg2step2244) / __81fgg2step2244)), _znpjgsef = __81fgg2dlsvn2244; __81fgg2count2244 != 0; __81fgg2count2244--, _znpjgsef += (__81fgg2step2244)) {

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
				System.Int32 __81fgg2dlsvn2245 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2245 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2245;
				for (__81fgg2count2245 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn2245 + __81fgg2step2245) / __81fgg2step2245)), _znpjgsef = __81fgg2dlsvn2245; __81fgg2count2245 != 0; __81fgg2count2245--, _znpjgsef += (__81fgg2step2245)) {

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
				System.Int32 __81fgg2dlsvn2246 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2246 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2246;
				for (__81fgg2count2246 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn2246 + __81fgg2step2246) / __81fgg2step2246)), _znpjgsef = __81fgg2dlsvn2246; __81fgg2count2246 != 0; __81fgg2count2246--, _znpjgsef += (__81fgg2step2246)) {

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
				System.Int32 __81fgg2dlsvn2247 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2247 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2247;
				for (__81fgg2count2247 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn2247 + __81fgg2step2247) / __81fgg2step2247)), _znpjgsef = __81fgg2dlsvn2247; __81fgg2count2247 != 0; __81fgg2count2247--, _znpjgsef += (__81fgg2step2247)) {

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
		// continue
		return;//* 
		//*     End of DLARFX 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
