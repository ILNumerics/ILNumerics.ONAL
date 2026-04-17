
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
//*> \brief \b DLARFT forms the triangular factor T of a block reflector H = I - vtvH 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLARFT + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlarft.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlarft.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlarft.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLARFT( DIRECT, STOREV, N, K, V, LDV, TAU, T, LDT ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          DIRECT, STOREV 
//*       INTEGER            K, LDT, LDV, N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   T( LDT, * ), TAU( * ), V( LDV, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLARFT forms the triangular factor T of a real block reflector H 
//*> of order n, which is defined as a product of k elementary reflectors. 
//*> 
//*> If DIRECT = 'F', H = H(1) H(2) . . . H(k) and T is upper triangular; 
//*> 
//*> If DIRECT = 'B', H = H(k) . . . H(2) H(1) and T is lower triangular. 
//*> 
//*> If STOREV = 'C', the vector which defines the elementary reflector 
//*> H(i) is stored in the i-th column of the array V, and 
//*> 
//*>    H  =  I - V * T * V**T 
//*> 
//*> If STOREV = 'R', the vector which defines the elementary reflector 
//*> H(i) is stored in the i-th row of the array V, and 
//*> 
//*>    H  =  I - V**T * T * V 
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
//*>          V is DOUBLE PRECISION array, dimension 
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
//*>          TAU is DOUBLE PRECISION array, dimension (K) 
//*>          TAU(i) must contain the scalar factor of the elementary 
//*>          reflector H(i). 
//*> \endverbatim 
//*> 
//*> \param[out] T 
//*> \verbatim 
//*>          T is DOUBLE PRECISION array, dimension (LDT,K) 
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
//*> \date December 2016 
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

	 
	public static void _kq0awdbm(FString _uw10mx43, FString _tjtvdgd6, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, Double* _ycxba85s, ref Int32 _ys09rxze, Double* _0446f4de, Double* _2ivtt43r, ref Int32 _w8yhbr2r)
	{
#region variable declarations
Double _kxg5drh2 =  1d;
Double _d0547bi2 =  0d;
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
				System.Int32 __81fgg2dlsvn470 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step470 = (System.Int32)((int)1);
				System.Int32 __81fgg2count470;
				for (__81fgg2count470 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn470 + __81fgg2step470) / __81fgg2step470)), _b5p6od9s = __81fgg2dlsvn470; __81fgg2count470 != 0; __81fgg2count470--, _b5p6od9s += (__81fgg2step470)) {

				{
					
					_zlpsoxly = ILNumerics.F2NET.Intrinsics.MAX(_b5p6od9s ,_zlpsoxly );
					if (*(_0446f4de+(_b5p6od9s - 1)) == _d0547bi2)
					{
						//* 
						//*              H(i)  =  I 
						//* 
						
						{
							System.Int32 __81fgg2dlsvn471 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step471 = (System.Int32)((int)1);
							System.Int32 __81fgg2count471;
							for (__81fgg2count471 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s) - __81fgg2dlsvn471 + __81fgg2step471) / __81fgg2step471)), _znpjgsef = __81fgg2dlsvn471; __81fgg2count471 != 0; __81fgg2count471--, _znpjgsef += (__81fgg2step471)) {

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
								System.Int32 __81fgg2dlsvn472 = (System.Int32)(_dxpq0xkr);
								System.Int32 __81fgg2step472 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count472;
								for (__81fgg2count472 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s + (int)1) - __81fgg2dlsvn472 + __81fgg2step472) / __81fgg2step472)), _thvhilfl = __81fgg2dlsvn472; __81fgg2count472 != 0; __81fgg2count472--, _thvhilfl += (__81fgg2step472)) {

								{
									
									if (*(_ycxba85s+(_thvhilfl - 1) + (_b5p6od9s - 1) * 1 * (_ys09rxze)) != _d0547bi2)
									break;
								}
																}							}
							{
								System.Int32 __81fgg2dlsvn473 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step473 = (System.Int32)((int)1);
								System.Int32 __81fgg2count473;
								for (__81fgg2count473 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn473 + __81fgg2step473) / __81fgg2step473)), _znpjgsef = __81fgg2dlsvn473; __81fgg2count473 != 0; __81fgg2count473--, _znpjgsef += (__81fgg2step473)) {

								{
									
									*(_2ivtt43r+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)) = (-((*(_0446f4de+(_b5p6od9s - 1)) * *(_ycxba85s+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ys09rxze)))));
								}
																}							}
							_znpjgsef = ILNumerics.F2NET.Intrinsics.MIN(_thvhilfl ,_zlpsoxly );//* 
							//*                 T(1:i-1,i) := - tau(i) * V(i:j,1:i-1)**T * V(i:j,i) 
							//* 
							
							_t5wmtd1j("Transpose" ,ref Unsafe.AsRef(_znpjgsef - _b5p6od9s) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(-(*(_0446f4de+(_b5p6od9s - 1)))) ,(_ycxba85s+(_b5p6od9s + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,(_ycxba85s+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ys09rxze)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_2ivtt43r+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) );
						}
						else
						{
							//*                 Skip any trailing zeros. 
							
							{
								System.Int32 __81fgg2dlsvn474 = (System.Int32)(_dxpq0xkr);
								System.Int32 __81fgg2step474 = (System.Int32)((int)-1);
								System.Int32 __81fgg2count474;
								for (__81fgg2count474 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s + (int)1) - __81fgg2dlsvn474 + __81fgg2step474) / __81fgg2step474)), _thvhilfl = __81fgg2dlsvn474; __81fgg2count474 != 0; __81fgg2count474--, _thvhilfl += (__81fgg2step474)) {

								{
									
									if (*(_ycxba85s+(_b5p6od9s - 1) + (_thvhilfl - 1) * 1 * (_ys09rxze)) != _d0547bi2)
									break;
								}
																}							}
							{
								System.Int32 __81fgg2dlsvn475 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step475 = (System.Int32)((int)1);
								System.Int32 __81fgg2count475;
								for (__81fgg2count475 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn475 + __81fgg2step475) / __81fgg2step475)), _znpjgsef = __81fgg2dlsvn475; __81fgg2count475 != 0; __81fgg2count475--, _znpjgsef += (__81fgg2step475)) {

								{
									
									*(_2ivtt43r+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)) = (-((*(_0446f4de+(_b5p6od9s - 1)) * *(_ycxba85s+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_ys09rxze)))));
								}
																}							}
							_znpjgsef = ILNumerics.F2NET.Intrinsics.MIN(_thvhilfl ,_zlpsoxly );//* 
							//*                 T(1:i-1,i) := - tau(i) * V(1:i-1,i:j) * V(i,i:j)**T 
							//* 
							
							_t5wmtd1j("No transpose" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_znpjgsef - _b5p6od9s) ,ref Unsafe.AsRef(-(*(_0446f4de+(_b5p6od9s - 1)))) ,(_ycxba85s+((int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,(_ycxba85s+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,(_2ivtt43r+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) );
						}
						//* 
						//*              T(1:i-1,i) := T(1:i-1,1:i-1) * T(1:i-1,i) 
						//* 
						
						_lg2hqio3("Upper" ,"No transpose" ,"Non-unit" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,_2ivtt43r ,ref _w8yhbr2r ,(_2ivtt43r+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) );
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
				System.Int32 __81fgg2dlsvn476 = (System.Int32)(_umlkckdg);
				System.Int32 __81fgg2step476 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count476;
				for (__81fgg2count476 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn476 + __81fgg2step476) / __81fgg2step476)), _b5p6od9s = __81fgg2dlsvn476; __81fgg2count476 != 0; __81fgg2count476--, _b5p6od9s += (__81fgg2step476)) {

				{
					
					if (*(_0446f4de+(_b5p6od9s - 1)) == _d0547bi2)
					{
						//* 
						//*              H(i)  =  I 
						//* 
						
						{
							System.Int32 __81fgg2dlsvn477 = (System.Int32)(_b5p6od9s);
							const System.Int32 __81fgg2step477 = (System.Int32)((int)1);
							System.Int32 __81fgg2count477;
							for (__81fgg2count477 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn477 + __81fgg2step477) / __81fgg2step477)), _znpjgsef = __81fgg2dlsvn477; __81fgg2count477 != 0; __81fgg2count477--, _znpjgsef += (__81fgg2step477)) {

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
									System.Int32 __81fgg2dlsvn478 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step478 = (System.Int32)((int)1);
									System.Int32 __81fgg2count478;
									for (__81fgg2count478 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn478 + __81fgg2step478) / __81fgg2step478)), _thvhilfl = __81fgg2dlsvn478; __81fgg2count478 != 0; __81fgg2count478--, _thvhilfl += (__81fgg2step478)) {

									{
										
										if (*(_ycxba85s+(_thvhilfl - 1) + (_b5p6od9s - 1) * 1 * (_ys09rxze)) != _d0547bi2)
										break;
									}
																		}								}
								{
									System.Int32 __81fgg2dlsvn479 = (System.Int32)((_b5p6od9s + (int)1));
									const System.Int32 __81fgg2step479 = (System.Int32)((int)1);
									System.Int32 __81fgg2count479;
									for (__81fgg2count479 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn479 + __81fgg2step479) / __81fgg2step479)), _znpjgsef = __81fgg2dlsvn479; __81fgg2count479 != 0; __81fgg2count479--, _znpjgsef += (__81fgg2step479)) {

									{
										
										*(_2ivtt43r+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)) = (-((*(_0446f4de+(_b5p6od9s - 1)) * *(_ycxba85s+((_dxpq0xkr - _umlkckdg) + _b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ys09rxze)))));
									}
																		}								}
								_znpjgsef = ILNumerics.F2NET.Intrinsics.MAX(_thvhilfl ,_zlpsoxly );//* 
								//*                    T(i+1:k,i) = -tau(i) * V(j:n-k+i,i+1:k)**T * V(j:n-k+i,i) 
								//* 
								
								_t5wmtd1j("Transpose" ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) + _b5p6od9s) - _znpjgsef) ,ref Unsafe.AsRef(_umlkckdg - _b5p6od9s) ,ref Unsafe.AsRef(-(*(_0446f4de+(_b5p6od9s - 1)))) ,(_ycxba85s+(_znpjgsef - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,(_ycxba85s+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_ys09rxze)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_2ivtt43r+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) );
							}
							else
							{
								//*                    Skip any leading zeros. 
								
								{
									System.Int32 __81fgg2dlsvn480 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step480 = (System.Int32)((int)1);
									System.Int32 __81fgg2count480;
									for (__81fgg2count480 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn480 + __81fgg2step480) / __81fgg2step480)), _thvhilfl = __81fgg2dlsvn480; __81fgg2count480 != 0; __81fgg2count480--, _thvhilfl += (__81fgg2step480)) {

									{
										
										if (*(_ycxba85s+(_b5p6od9s - 1) + (_thvhilfl - 1) * 1 * (_ys09rxze)) != _d0547bi2)
										break;
									}
																		}								}
								{
									System.Int32 __81fgg2dlsvn481 = (System.Int32)((_b5p6od9s + (int)1));
									const System.Int32 __81fgg2step481 = (System.Int32)((int)1);
									System.Int32 __81fgg2count481;
									for (__81fgg2count481 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn481 + __81fgg2step481) / __81fgg2step481)), _znpjgsef = __81fgg2dlsvn481; __81fgg2count481 != 0; __81fgg2count481--, _znpjgsef += (__81fgg2step481)) {

									{
										
										*(_2ivtt43r+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)) = (-((*(_0446f4de+(_b5p6od9s - 1)) * *(_ycxba85s+(_znpjgsef - 1) + ((_dxpq0xkr - _umlkckdg) + _b5p6od9s - 1) * 1 * (_ys09rxze)))));
									}
																		}								}
								_znpjgsef = ILNumerics.F2NET.Intrinsics.MAX(_thvhilfl ,_zlpsoxly );//* 
								//*                    T(i+1:k,i) = -tau(i) * V(i+1:k,j:n-k+i) * V(i,j:n-k+i)**T 
								//* 
								
								_t5wmtd1j("No transpose" ,ref Unsafe.AsRef(_umlkckdg - _b5p6od9s) ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) + _b5p6od9s) - _znpjgsef) ,ref Unsafe.AsRef(-(*(_0446f4de+(_b5p6od9s - 1)))) ,(_ycxba85s+(_b5p6od9s + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,(_ycxba85s+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_kxg5drh2) ,(_2ivtt43r+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) );
							}
							//* 
							//*                 T(i+1:k,i) := T(i+1:k,i+1:k) * T(i+1:k,i) 
							//* 
							
							_lg2hqio3("Lower" ,"No transpose" ,"Non-unit" ,ref Unsafe.AsRef(_umlkckdg - _b5p6od9s) ,(_2ivtt43r+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,(_2ivtt43r+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) );
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
		//*     End of DLARFT 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
