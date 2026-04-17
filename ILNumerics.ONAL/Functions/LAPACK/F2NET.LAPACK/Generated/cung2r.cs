
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
//*> \brief \b CUNG2R 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CUNG2R + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/cung2r.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/cung2r.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/cung2r.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CUNG2R( M, N, K, A, LDA, TAU, WORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, K, LDA, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX            A( LDA, * ), TAU( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CUNG2R generates an m by n complex matrix Q with orthonormal columns, 
//*> which is defined as the first n columns of a product of k elementary 
//*> reflectors of order m 
//*> 
//*>       Q  =  H(1) H(2) . . . H(k) 
//*> 
//*> as returned by CGEQRF. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of rows of the matrix Q. M >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of columns of the matrix Q. M >= N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] K 
//*> \verbatim 
//*>          K is INTEGER 
//*>          The number of elementary reflectors whose product defines the 
//*>          matrix Q. N >= K >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is COMPLEX array, dimension (LDA,N) 
//*>          On entry, the i-th column must contain the vector which 
//*>          defines the elementary reflector H(i), for i = 1,2,...,k, as 
//*>          returned by CGEQRF in the first k columns of its array 
//*>          argument A. 
//*>          On exit, the m by n matrix Q. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The first dimension of the array A. LDA >= max(1,M). 
//*> \endverbatim 
//*> 
//*> \param[in] TAU 
//*> \verbatim 
//*>          TAU is COMPLEX array, dimension (K) 
//*>          TAU(i) must contain the scalar factor of the elementary 
//*>          reflector H(i), as returned by CGEQRF. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is COMPLEX array, dimension (N) 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0: successful exit 
//*>          < 0: if INFO = -i, the i-th argument has an illegal value 
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
//*  ===================================================================== 

	 
	public static void _kdktkqhe(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, fcomplex* _0446f4de, fcomplex* _apig8meb, ref Int32 _gro5yvfo)
	{
#region variable declarations
fcomplex _kxg5drh2 =   new fcomplex(1f,0f);
fcomplex _d0547bi2 =   new fcomplex(0f,0f);
Int32 _b5p6od9s =  default;
Int32 _znpjgsef =  default;
Int32 _68ec3gbh =  default;
string fLanavab = default;
#endregion  variable declarations

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
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input arguments 
		//* 
		
		_gro5yvfo = (int)0;
		if (_ev4xhht5 < (int)0)
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if ((_dxpq0xkr < (int)0) | (_dxpq0xkr > _ev4xhht5))
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if ((_umlkckdg < (int)0) | (_umlkckdg > _dxpq0xkr))
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ))
		{
			
			_gro5yvfo = (int)-5;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("CUNG2R" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr <= (int)0)
		return;//* 
		//*     Initialise columns k+1:n to columns of the unit matrix 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn1111 = (System.Int32)((_umlkckdg + (int)1));
			const System.Int32 __81fgg2step1111 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1111;
			for (__81fgg2count1111 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1111 + __81fgg2step1111) / __81fgg2step1111)), _znpjgsef = __81fgg2dlsvn1111; __81fgg2count1111 != 0; __81fgg2count1111--, _znpjgsef += (__81fgg2step1111)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn1112 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1112 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1112;
					for (__81fgg2count1112 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1112 + __81fgg2step1112) / __81fgg2step1112)), _68ec3gbh = __81fgg2dlsvn1112; __81fgg2count1112 != 0; __81fgg2count1112--, _68ec3gbh += (__81fgg2step1112)) {

					{
						
						*(_vxfgpup9+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
Mark10:;
						// continue
					}
										}				}
				*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;
Mark20:;
				// continue
			}
						}		}//* 
		
		{
			System.Int32 __81fgg2dlsvn1113 = (System.Int32)(_umlkckdg);
			System.Int32 __81fgg2step1113 = (System.Int32)((int)-1);
			System.Int32 __81fgg2count1113;
			for (__81fgg2count1113 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1113 + __81fgg2step1113) / __81fgg2step1113)), _b5p6od9s = __81fgg2dlsvn1113; __81fgg2count1113 != 0; __81fgg2count1113--, _b5p6od9s += (__81fgg2step1113)) {

			{
				//* 
				//*        Apply H(i) to A(i:m,i:n) from the left 
				//* 
				
				if (_b5p6od9s < _dxpq0xkr)
				{
					
					*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;
					_ok06eljh("Left" ,ref Unsafe.AsRef((_ev4xhht5 - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_0446f4de+(_b5p6od9s - 1))) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_apig8meb );
				}
				
				if (_b5p6od9s < _ev4xhht5)
				_00l5hgpk(ref Unsafe.AsRef(_ev4xhht5 - _b5p6od9s) ,ref Unsafe.AsRef(-(*(_0446f4de+(_b5p6od9s - 1)))) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
				*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = (_kxg5drh2 - *(_0446f4de+(_b5p6od9s - 1)));//* 
				//*        Set A(1:i-1,i) to zero 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn1114 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1114 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1114;
					for (__81fgg2count1114 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn1114 + __81fgg2step1114) / __81fgg2step1114)), _68ec3gbh = __81fgg2dlsvn1114; __81fgg2count1114 != 0; __81fgg2count1114--, _68ec3gbh += (__81fgg2step1114)) {

					{
						
						*(_vxfgpup9+(_68ec3gbh - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
Mark30:;
						// continue
					}
										}				}
Mark40:;
				// continue
			}
						}		}
		return;//* 
		//*     End of CUNG2R 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
