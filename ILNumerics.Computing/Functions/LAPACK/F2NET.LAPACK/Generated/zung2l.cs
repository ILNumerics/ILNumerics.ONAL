
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
//*> \brief \b ZUNG2L generates all or part of the unitary matrix Q from a QL factorization determined by cgeqlf (unblocked algorithm). 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZUNG2L + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zung2l.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zung2l.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zung2l.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZUNG2L( M, N, K, A, LDA, TAU, WORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, K, LDA, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX*16         A( LDA, * ), TAU( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZUNG2L generates an m by n complex matrix Q with orthonormal columns, 
//*> which is defined as the last n columns of a product of k elementary 
//*> reflectors of order m 
//*> 
//*>       Q  =  H(k) . . . H(2) H(1) 
//*> 
//*> as returned by ZGEQLF. 
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
//*>          A is COMPLEX*16 array, dimension (LDA,N) 
//*>          On entry, the (n-k+i)-th column must contain the vector which 
//*>          defines the elementary reflector H(i), for i = 1,2,...,k, as 
//*>          returned by ZGEQLF in the last k columns of its array 
//*>          argument A. 
//*>          On exit, the m-by-n matrix Q. 
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
//*>          TAU is COMPLEX*16 array, dimension (K) 
//*>          TAU(i) must contain the scalar factor of the elementary 
//*>          reflector H(i), as returned by ZGEQLF. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is COMPLEX*16 array, dimension (N) 
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
//*> \ingroup complex16OTHERcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _wxrormlm(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, complex* _vxfgpup9, ref Int32 _ocv8fk5c, complex* _0446f4de, complex* _apig8meb, ref Int32 _gro5yvfo)
	{
#region variable declarations
complex _kxg5drh2 =   new fcomplex(1f,0f);
complex _d0547bi2 =   new fcomplex(0f,0f);
Int32 _b5p6od9s =  default;
Int32 _retbwjxi =  default;
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
			
			_ut9qalzx("ZUNG2L" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr <= (int)0)
		return;//* 
		//*     Initialise columns 1:n-k to columns of the unit matrix 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn3937 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step3937 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3937;
			for (__81fgg2count3937 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - _umlkckdg) - __81fgg2dlsvn3937 + __81fgg2step3937) / __81fgg2step3937)), _znpjgsef = __81fgg2dlsvn3937; __81fgg2count3937 != 0; __81fgg2count3937--, _znpjgsef += (__81fgg2step3937)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn3938 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3938 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3938;
					for (__81fgg2count3938 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3938 + __81fgg2step3938) / __81fgg2step3938)), _68ec3gbh = __81fgg2dlsvn3938; __81fgg2count3938 != 0; __81fgg2count3938--, _68ec3gbh += (__81fgg2step3938)) {

					{
						
						*(_vxfgpup9+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
Mark10:;
						// continue
					}
										}				}
				*(_vxfgpup9+((_ev4xhht5 - _dxpq0xkr) + _znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;
Mark20:;
				// continue
			}
						}		}//* 
		
		{
			System.Int32 __81fgg2dlsvn3939 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step3939 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3939;
			for (__81fgg2count3939 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn3939 + __81fgg2step3939) / __81fgg2step3939)), _b5p6od9s = __81fgg2dlsvn3939; __81fgg2count3939 != 0; __81fgg2count3939--, _b5p6od9s += (__81fgg2step3939)) {

			{
				
				_retbwjxi = ((_dxpq0xkr - _umlkckdg) + _b5p6od9s);//* 
				//*        Apply H(i) to A(1:m-k+i,1:n-k+i) from the left 
				//* 
				
				*(_vxfgpup9+((_ev4xhht5 - _dxpq0xkr) + _retbwjxi - 1) + (_retbwjxi - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;
				_h7ckdrdn("Left" ,ref Unsafe.AsRef((_ev4xhht5 - _dxpq0xkr) + _retbwjxi) ,ref Unsafe.AsRef(_retbwjxi - (int)1) ,(_vxfgpup9+((int)1 - 1) + (_retbwjxi - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_0446f4de+(_b5p6od9s - 1))) ,_vxfgpup9 ,ref _ocv8fk5c ,_apig8meb );
				_wv0on4xy(ref Unsafe.AsRef(((_ev4xhht5 - _dxpq0xkr) + _retbwjxi) - (int)1) ,ref Unsafe.AsRef(-(*(_0446f4de+(_b5p6od9s - 1)))) ,(_vxfgpup9+((int)1 - 1) + (_retbwjxi - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
				*(_vxfgpup9+((_ev4xhht5 - _dxpq0xkr) + _retbwjxi - 1) + (_retbwjxi - 1) * 1 * (_ocv8fk5c)) = (_kxg5drh2 - *(_0446f4de+(_b5p6od9s - 1)));//* 
				//*        Set A(m-k+i+1:m,n-k+i) to zero 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn3940 = (System.Int32)((((_ev4xhht5 - _dxpq0xkr) + _retbwjxi) + (int)1));
					const System.Int32 __81fgg2step3940 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3940;
					for (__81fgg2count3940 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3940 + __81fgg2step3940) / __81fgg2step3940)), _68ec3gbh = __81fgg2dlsvn3940; __81fgg2count3940 != 0; __81fgg2count3940--, _68ec3gbh += (__81fgg2step3940)) {

					{
						
						*(_vxfgpup9+(_68ec3gbh - 1) + (_retbwjxi - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
Mark30:;
						// continue
					}
										}				}
Mark40:;
				// continue
			}
						}		}
		return;//* 
		//*     End of ZUNG2L 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
