
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
//*> \brief \b CUNGL2 generates all or part of the unitary matrix Q from an LQ factorization determined by cgelqf (unblocked algorithm). 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CUNGL2 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/cungl2.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/cungl2.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/cungl2.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CUNGL2( M, N, K, A, LDA, TAU, WORK, INFO ) 
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
//*> CUNGL2 generates an m-by-n complex matrix Q with orthonormal rows, 
//*> which is defined as the first m rows of a product of k elementary 
//*> reflectors of order n 
//*> 
//*>       Q  =  H(k)**H . . . H(2)**H H(1)**H 
//*> 
//*> as returned by CGELQF. 
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
//*>          The number of columns of the matrix Q. N >= M. 
//*> \endverbatim 
//*> 
//*> \param[in] K 
//*> \verbatim 
//*>          K is INTEGER 
//*>          The number of elementary reflectors whose product defines the 
//*>          matrix Q. M >= K >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is COMPLEX array, dimension (LDA,N) 
//*>          On entry, the i-th row must contain the vector which defines 
//*>          the elementary reflector H(i), for i = 1,2,...,k, as returned 
//*>          by CGELQF in the first k rows of its array argument A. 
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
//*>          reflector H(i), as returned by CGELQF. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is COMPLEX array, dimension (M) 
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

	 
	public static void _6cd8pzo9(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, fcomplex* _0446f4de, fcomplex* _apig8meb, ref Int32 _gro5yvfo)
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
		if (_dxpq0xkr < _ev4xhht5)
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if ((_umlkckdg < (int)0) | (_umlkckdg > _ev4xhht5))
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
			
			_ut9qalzx("CUNGL2" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_ev4xhht5 <= (int)0)
		return;//* 
		
		if (_umlkckdg < _ev4xhht5)
		{
			//* 
			//*        Initialise rows k+1:m to rows of the unit matrix 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn1102 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1102 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1102;
				for (__81fgg2count1102 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1102 + __81fgg2step1102) / __81fgg2step1102)), _znpjgsef = __81fgg2dlsvn1102; __81fgg2count1102 != 0; __81fgg2count1102--, _znpjgsef += (__81fgg2step1102)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn1103 = (System.Int32)((_umlkckdg + (int)1));
						const System.Int32 __81fgg2step1103 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1103;
						for (__81fgg2count1103 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1103 + __81fgg2step1103) / __81fgg2step1103)), _68ec3gbh = __81fgg2dlsvn1103; __81fgg2count1103 != 0; __81fgg2count1103--, _68ec3gbh += (__81fgg2step1103)) {

						{
							
							*(_vxfgpup9+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
Mark10:;
							// continue
						}
												}					}
					if ((_znpjgsef > _umlkckdg) & (_znpjgsef <= _ev4xhht5))
					*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;
Mark20:;
					// continue
				}
								}			}
		}
		//* 
		
		{
			System.Int32 __81fgg2dlsvn1104 = (System.Int32)(_umlkckdg);
			System.Int32 __81fgg2step1104 = (System.Int32)((int)-1);
			System.Int32 __81fgg2count1104;
			for (__81fgg2count1104 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1104 + __81fgg2step1104) / __81fgg2step1104)), _b5p6od9s = __81fgg2dlsvn1104; __81fgg2count1104 != 0; __81fgg2count1104--, _b5p6od9s += (__81fgg2step1104)) {

			{
				//* 
				//*        Apply H(i)**H to A(i:m,i:n) from the right 
				//* 
				
				if (_b5p6od9s < _dxpq0xkr)
				{
					
					_png2g84j(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
					if (_b5p6od9s < _ev4xhht5)
					{
						
						*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;
						_ok06eljh("Right" ,ref Unsafe.AsRef(_ev4xhht5 - _b5p6od9s) ,ref Unsafe.AsRef((_dxpq0xkr - _b5p6od9s) + (int)1) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.CONJG(*(_0446f4de+(_b5p6od9s - 1)) )) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_apig8meb );
					}
					
					_00l5hgpk(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(-(*(_0446f4de+(_b5p6od9s - 1)))) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
					_png2g84j(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
				}
				
				*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = (_kxg5drh2 - ILNumerics.F2NET.Intrinsics.CONJG(*(_0446f4de+(_b5p6od9s - 1)) ));//* 
				//*        Set A(i,1:i-1,i) to zero 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn1105 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1105 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1105;
					for (__81fgg2count1105 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn1105 + __81fgg2step1105) / __81fgg2step1105)), _68ec3gbh = __81fgg2dlsvn1105; __81fgg2count1105 != 0; __81fgg2count1105--, _68ec3gbh += (__81fgg2step1105)) {

					{
						
						*(_vxfgpup9+(_b5p6od9s - 1) + (_68ec3gbh - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
Mark30:;
						// continue
					}
										}				}
Mark40:;
				// continue
			}
						}		}
		return;//* 
		//*     End of CUNGL2 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
