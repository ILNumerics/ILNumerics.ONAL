
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
//*> \brief \b ZLARZT forms the triangular factor T of a block reflector H = I - vtvH. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZLARZT + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zlarzt.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zlarzt.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zlarzt.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZLARZT( DIRECT, STOREV, N, K, V, LDV, TAU, T, LDT ) 
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
//*> ZLARZT forms the triangular factor T of a complex block reflector 
//*> H of order > n, which is defined as a product of k elementary 
//*> reflectors. 
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
//*> 
//*> Currently, only STOREV = 'R' and DIRECT = 'B' are supported. 
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
//*>          = 'F': H = H(1) H(2) . . . H(k) (Forward, not supported yet) 
//*>          = 'B': H = H(k) . . . H(2) H(1) (Backward) 
//*> \endverbatim 
//*> 
//*> \param[in] STOREV 
//*> \verbatim 
//*>          STOREV is CHARACTER*1 
//*>          Specifies how the vectors which define the elementary 
//*>          reflectors are stored (see also Further Details): 
//*>          = 'C': columnwise                        (not supported yet) 
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
//*> \param[in,out] V 
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
//*> \date December 2016 
//* 
//*> \ingroup complex16OTHERcomputational 
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
//*> 
//*>  The shape of the matrix V and the storage of the vectors which define 
//*>  the H(i) is best illustrated by the following example with n = 5 and 
//*>  k = 3. The elements equal to 1 are not stored; the corresponding 
//*>  array elements are modified but restored on exit. The rest of the 
//*>  array is not used. 
//*> 
//*>  DIRECT = 'F' and STOREV = 'C':         DIRECT = 'F' and STOREV = 'R': 
//*> 
//*>                                              ______V_____ 
//*>         ( v1 v2 v3 )                        /            \ 
//*>         ( v1 v2 v3 )                      ( v1 v1 v1 v1 v1 . . . . 1 ) 
//*>     V = ( v1 v2 v3 )                      ( v2 v2 v2 v2 v2 . . . 1   ) 
//*>         ( v1 v2 v3 )                      ( v3 v3 v3 v3 v3 . . 1     ) 
//*>         ( v1 v2 v3 ) 
//*>            .  .  . 
//*>            .  .  . 
//*>            1  .  . 
//*>               1  . 
//*>                  1 
//*> 
//*>  DIRECT = 'B' and STOREV = 'C':         DIRECT = 'B' and STOREV = 'R': 
//*> 
//*>                                                        ______V_____ 
//*>            1                                          /            \ 
//*>            .  1                           ( 1 . . . . v1 v1 v1 v1 v1 ) 
//*>            .  .  1                        ( . 1 . . . v2 v2 v2 v2 v2 ) 
//*>            .  .  .                        ( . . 1 . . v3 v3 v3 v3 v3 ) 
//*>            .  .  . 
//*>         ( v1 v2 v3 ) 
//*>         ( v1 v2 v3 ) 
//*>     V = ( v1 v2 v3 ) 
//*>         ( v1 v2 v3 ) 
//*>         ( v1 v2 v3 ) 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _s87qy803(FString _uw10mx43, FString _tjtvdgd6, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, complex* _ycxba85s, ref Int32 _ys09rxze, complex* _0446f4de, complex* _2ivtt43r, ref Int32 _w8yhbr2r)
	{
#region variable declarations
complex _d0547bi2 =   new fcomplex(0f,0f);
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _znpjgsef =  default;
string fLanavab = default;
#endregion  variable declarations
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
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Check for currently supported options 
		//* 
		
		_gro5yvfo = (int)0;
		if (!(_w8y2rzgy(_uw10mx43 ,"B" )))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (!(_w8y2rzgy(_tjtvdgd6 ,"R" )))
		{
			
			_gro5yvfo = (int)-2;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("ZLARZT" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		
		{
			System.Int32 __81fgg2dlsvn2162 = (System.Int32)(_umlkckdg);
			System.Int32 __81fgg2step2162 = (System.Int32)((int)-1);
			System.Int32 __81fgg2count2162;
			for (__81fgg2count2162 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn2162 + __81fgg2step2162) / __81fgg2step2162)), _b5p6od9s = __81fgg2dlsvn2162; __81fgg2count2162 != 0; __81fgg2count2162--, _b5p6od9s += (__81fgg2step2162)) {

			{
				
				if (*(_0446f4de+(_b5p6od9s - 1)) == _d0547bi2)
				{
					//* 
					//*           H(i)  =  I 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn2163 = (System.Int32)(_b5p6od9s);
						const System.Int32 __81fgg2step2163 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2163;
						for (__81fgg2count2163 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn2163 + __81fgg2step2163) / __81fgg2step2163)), _znpjgsef = __81fgg2dlsvn2163; __81fgg2count2163 != 0; __81fgg2count2163--, _znpjgsef += (__81fgg2step2163)) {

						{
							
							*(_2ivtt43r+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)) = _d0547bi2;
Mark10:;
							// continue
						}
												}					}
				}
				else
				{
					//* 
					//*           general case 
					//* 
					
					if (_b5p6od9s < _umlkckdg)
					{
						//* 
						//*              T(i+1:k,i) = - tau(i) * V(i+1:k,1:n) * V(i,1:n)**H 
						//* 
						
						_42wgkyoq(ref _dxpq0xkr ,(_ycxba85s+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze );
						_xfaqgfxk("No transpose" ,ref Unsafe.AsRef(_umlkckdg - _b5p6od9s) ,ref _dxpq0xkr ,ref Unsafe.AsRef(-(*(_0446f4de+(_b5p6od9s - 1)))) ,(_ycxba85s+(_b5p6od9s + (int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,(_ycxba85s+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,ref Unsafe.AsRef(_d0547bi2) ,(_2ivtt43r+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) );
						_42wgkyoq(ref _dxpq0xkr ,(_ycxba85s+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze );//* 
						//*              T(i+1:k,i) = T(i+1:k,i+1:k) * T(i+1:k,i) 
						//* 
						
						_xajlj6s7("Lower" ,"No transpose" ,"Non-unit" ,ref Unsafe.AsRef(_umlkckdg - _b5p6od9s) ,(_2ivtt43r+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,(_2ivtt43r+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) );
					}
					
					*(_2ivtt43r+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)) = *(_0446f4de+(_b5p6od9s - 1));
				}
				
Mark20:;
				// continue
			}
						}		}
		return;//* 
		//*     End of ZLARZT 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
