
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
//*> \brief \b DLARRA computes the splitting points with the specified threshold. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLARRA + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlarra.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlarra.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlarra.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLARRA( N, D, E, E2, SPLTOL, TNRM, 
//*                           NSPLIT, ISPLIT, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, N, NSPLIT 
//*       DOUBLE PRECISION    SPLTOL, TNRM 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            ISPLIT( * ) 
//*       DOUBLE PRECISION   D( * ), E( * ), E2( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> Compute the splitting points with threshold SPLTOL. 
//*> DLARRA sets any "small" off-diagonal elements to zero. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix. N > 0. 
//*> \endverbatim 
//*> 
//*> \param[in] D 
//*> \verbatim 
//*>          D is DOUBLE PRECISION array, dimension (N) 
//*>          On entry, the N diagonal elements of the tridiagonal 
//*>          matrix T. 
//*> \endverbatim 
//*> 
//*> \param[in,out] E 
//*> \verbatim 
//*>          E is DOUBLE PRECISION array, dimension (N) 
//*>          On entry, the first (N-1) entries contain the subdiagonal 
//*>          elements of the tridiagonal matrix T; E(N) need not be set. 
//*>          On exit, the entries E( ISPLIT( I ) ), 1 <= I <= NSPLIT, 
//*>          are set to zero, the other entries of E are untouched. 
//*> \endverbatim 
//*> 
//*> \param[in,out] E2 
//*> \verbatim 
//*>          E2 is DOUBLE PRECISION array, dimension (N) 
//*>          On entry, the first (N-1) entries contain the SQUARES of the 
//*>          subdiagonal elements of the tridiagonal matrix T; 
//*>          E2(N) need not be set. 
//*>          On exit, the entries E2( ISPLIT( I ) ), 
//*>          1 <= I <= NSPLIT, have been set to zero 
//*> \endverbatim 
//*> 
//*> \param[in] SPLTOL 
//*> \verbatim 
//*>          SPLTOL is DOUBLE PRECISION 
//*>          The threshold for splitting. Two criteria can be used: 
//*>          SPLTOL<0 : criterion based on absolute off-diagonal value 
//*>          SPLTOL>0 : criterion that preserves relative accuracy 
//*> \endverbatim 
//*> 
//*> \param[in] TNRM 
//*> \verbatim 
//*>          TNRM is DOUBLE PRECISION 
//*>          The norm of the matrix. 
//*> \endverbatim 
//*> 
//*> \param[out] NSPLIT 
//*> \verbatim 
//*>          NSPLIT is INTEGER 
//*>          The number of blocks T splits into. 1 <= NSPLIT <= N. 
//*> \endverbatim 
//*> 
//*> \param[out] ISPLIT 
//*> \verbatim 
//*>          ISPLIT is INTEGER array, dimension (N) 
//*>          The splitting points, at which T breaks up into blocks. 
//*>          The first block consists of rows/columns 1 to ISPLIT(1), 
//*>          the second of rows/columns ISPLIT(1)+1 through ISPLIT(2), 
//*>          etc., and the NSPLIT-th consists of rows/columns 
//*>          ISPLIT(NSPLIT-1)+1 through ISPLIT(NSPLIT)=N. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit 
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
//*> \date June 2017 
//* 
//*> \ingroup OTHERauxiliary 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*> Beresford Parlett, University of California, Berkeley, USA \n 
//*> Jim Demmel, University of California, Berkeley, USA \n 
//*> Inderjit Dhillon, University of Texas, Austin, USA \n 
//*> Osni Marques, LBNL/NERSC, USA \n 
//*> Christof Voemel, University of California, Berkeley, USA 
//* 
//*  ===================================================================== 

	 
	public static void _bnf6z4to(ref Int32 _dxpq0xkr, Double* _plfm7z8g, Double* _864fslqq, Double* _0maek8rz, ref Double _6odkwg9v, ref Double _gq29adzg, ref Int32 _naa7acm7, Int32* _nn033w1s, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Int32 _b5p6od9s =  default;
Double _rwvs4hol =  default;
Double _c0o9kuh7 =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.1) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     June 2017 
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
		// 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		_gro5yvfo = (int)0;//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr <= (int)0)
		{
			
			return;
		}
		//* 
		//*     Compute splitting points 
		
		_naa7acm7 = (int)1;
		if (_6odkwg9v < _d0547bi2)
		{
			//*        Criterion based on absolute off-diagonal value 
			
			_c0o9kuh7 = (ILNumerics.F2NET.Intrinsics.ABS(_6odkwg9v ) * _gq29adzg);
			{
				System.Int32 __81fgg2dlsvn3022 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step3022 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3022;
				for (__81fgg2count3022 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn3022 + __81fgg2step3022) / __81fgg2step3022)), _b5p6od9s = __81fgg2dlsvn3022; __81fgg2count3022 != 0; __81fgg2count3022--, _b5p6od9s += (__81fgg2step3022)) {

				{
					
					_rwvs4hol = ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_b5p6od9s - 1)) );
					if (_rwvs4hol <= _c0o9kuh7)
					{
						
						*(_864fslqq+(_b5p6od9s - 1)) = _d0547bi2;
						*(_0maek8rz+(_b5p6od9s - 1)) = _d0547bi2;
						*(_nn033w1s+(_naa7acm7 - 1)) = _b5p6od9s;
						_naa7acm7 = (_naa7acm7 + (int)1);
					}
					
Mark9:;
					// continue
				}
								}			}
		}
		else
		{
			//*        Criterion that guarantees relative accuracy 
			
			{
				System.Int32 __81fgg2dlsvn3023 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step3023 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3023;
				for (__81fgg2count3023 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn3023 + __81fgg2step3023) / __81fgg2step3023)), _b5p6od9s = __81fgg2dlsvn3023; __81fgg2count3023 != 0; __81fgg2count3023--, _b5p6od9s += (__81fgg2step3023)) {

				{
					
					_rwvs4hol = ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_b5p6od9s - 1)) );
					if (_rwvs4hol <= ((_6odkwg9v * ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_b5p6od9s - 1)) ) )) * ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_b5p6od9s + (int)1 - 1)) ) )))
					{
						
						*(_864fslqq+(_b5p6od9s - 1)) = _d0547bi2;
						*(_0maek8rz+(_b5p6od9s - 1)) = _d0547bi2;
						*(_nn033w1s+(_naa7acm7 - 1)) = _b5p6od9s;
						_naa7acm7 = (_naa7acm7 + (int)1);
					}
					
Mark10:;
					// continue
				}
								}			}
		}
		
		*(_nn033w1s+(_naa7acm7 - 1)) = _dxpq0xkr;// 
		
		return;//* 
		//*     End of DLARRA 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
