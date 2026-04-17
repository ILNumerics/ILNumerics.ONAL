
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
//*> \brief \b DORGL2 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DORGL2 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dorgl2.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dorgl2.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dorgl2.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DORGL2( M, N, K, A, LDA, TAU, WORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, K, LDA, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   A( LDA, * ), TAU( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DORGL2 generates an m by n real matrix Q with orthonormal rows, 
//*> which is defined as the first m rows of a product of k elementary 
//*> reflectors of order n 
//*> 
//*>       Q  =  H(k) . . . H(2) H(1) 
//*> 
//*> as returned by DGELQF. 
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
//*>          A is DOUBLE PRECISION array, dimension (LDA,N) 
//*>          On entry, the i-th row must contain the vector which defines 
//*>          the elementary reflector H(i), for i = 1,2,...,k, as returned 
//*>          by DGELQF in the first k rows of its array argument A. 
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
//*>          TAU is DOUBLE PRECISION array, dimension (K) 
//*>          TAU(i) must contain the scalar factor of the elementary 
//*>          reflector H(i), as returned by DGELQF. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is DOUBLE PRECISION array, dimension (M) 
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
//*> \ingroup doubleOTHERcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _leq0qf83(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, Double* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _0446f4de, Double* _apig8meb, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _kxg5drh2 =  1d;
Double _d0547bi2 =  0d;
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
			
			_ut9qalzx("DORGL2" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
				System.Int32 __81fgg2dlsvn511 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step511 = (System.Int32)((int)1);
				System.Int32 __81fgg2count511;
				for (__81fgg2count511 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn511 + __81fgg2step511) / __81fgg2step511)), _znpjgsef = __81fgg2dlsvn511; __81fgg2count511 != 0; __81fgg2count511--, _znpjgsef += (__81fgg2step511)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn512 = (System.Int32)((_umlkckdg + (int)1));
						const System.Int32 __81fgg2step512 = (System.Int32)((int)1);
						System.Int32 __81fgg2count512;
						for (__81fgg2count512 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn512 + __81fgg2step512) / __81fgg2step512)), _68ec3gbh = __81fgg2dlsvn512; __81fgg2count512 != 0; __81fgg2count512--, _68ec3gbh += (__81fgg2step512)) {

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
			System.Int32 __81fgg2dlsvn513 = (System.Int32)(_umlkckdg);
			System.Int32 __81fgg2step513 = (System.Int32)((int)-1);
			System.Int32 __81fgg2count513;
			for (__81fgg2count513 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn513 + __81fgg2step513) / __81fgg2step513)), _b5p6od9s = __81fgg2dlsvn513; __81fgg2count513 != 0; __81fgg2count513--, _b5p6od9s += (__81fgg2step513)) {

			{
				//* 
				//*        Apply H(i) to A(i:m,i:n) from the right 
				//* 
				
				if (_b5p6od9s < _dxpq0xkr)
				{
					
					if (_b5p6od9s < _ev4xhht5)
					{
						
						*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;
						_bbpftela("Right" ,ref Unsafe.AsRef(_ev4xhht5 - _b5p6od9s) ,ref Unsafe.AsRef((_dxpq0xkr - _b5p6od9s) + (int)1) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(*(_0446f4de+(_b5p6od9s - 1))) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_apig8meb );
					}
					
					_f6jqcjk1(ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,ref Unsafe.AsRef(-(*(_0446f4de+(_b5p6od9s - 1)))) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
				}
				
				*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = (_kxg5drh2 - *(_0446f4de+(_b5p6od9s - 1)));//* 
				//*        Set A(i,1:i-1) to zero 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn514 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step514 = (System.Int32)((int)1);
					System.Int32 __81fgg2count514;
					for (__81fgg2count514 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn514 + __81fgg2step514) / __81fgg2step514)), _68ec3gbh = __81fgg2dlsvn514; __81fgg2count514 != 0; __81fgg2count514--, _68ec3gbh += (__81fgg2step514)) {

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
		//*     End of DORGL2 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
