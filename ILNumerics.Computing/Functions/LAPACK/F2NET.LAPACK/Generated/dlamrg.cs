
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
//*> \brief \b DLAMRG creates a permutation list to merge the entries of two independently sorted sets into a single set sorted in ascending order. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLAMRG + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlamrg.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlamrg.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlamrg.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLAMRG( N1, N2, A, DTRD1, DTRD2, INDEX ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            DTRD1, DTRD2, N1, N2 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            INDEX( * ) 
//*       DOUBLE PRECISION   A( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLAMRG will create a permutation list which will merge the elements 
//*> of A (which is composed of two independently sorted sets) into a 
//*> single set which is sorted in ascending order. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N1 
//*> \verbatim 
//*>          N1 is INTEGER 
//*> \endverbatim 
//*> 
//*> \param[in] N2 
//*> \verbatim 
//*>          N2 is INTEGER 
//*>         These arguments contain the respective lengths of the two 
//*>         sorted lists to be merged. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is DOUBLE PRECISION array, dimension (N1+N2) 
//*>         The first N1 elements of A contain a list of numbers which 
//*>         are sorted in either ascending or descending order.  Likewise 
//*>         for the final N2 elements. 
//*> \endverbatim 
//*> 
//*> \param[in] DTRD1 
//*> \verbatim 
//*>          DTRD1 is INTEGER 
//*> \endverbatim 
//*> 
//*> \param[in] DTRD2 
//*> \verbatim 
//*>          DTRD2 is INTEGER 
//*>         These are the strides to be taken through the array A. 
//*>         Allowable strides are 1 and -1.  They indicate whether a 
//*>         subset of A is sorted in ascending (DTRDx = 1) or descending 
//*>         (DTRDx = -1) order. 
//*> \endverbatim 
//*> 
//*> \param[out] INDEX 
//*> \verbatim 
//*>          INDEX is INTEGER array, dimension (N1+N2) 
//*>         On exit this array will contain a permutation such that 
//*>         if B( I ) = A( INDEX( I ) ) for I=1,N1+N2, then B will be 
//*>         sorted in ascending order. 
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
//*> \date June 2016 
//* 
//*> \ingroup auxOTHERcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _csi3ymnh(ref Int32 _4o1bt8b1, ref Int32 _tixk7d1h, Double* _vxfgpup9, ref Int32 _sthvw5uv, ref Int32 _j99gljhp, Int32* _38qfrq10)
	{
#region variable declarations
Int32 _b5p6od9s =  default;
Int32 _wgcg01rd =  default;
Int32 _3zy83kgw =  default;
Int32 _isav2oo2 =  default;
Int32 _l39z5izc =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK computational routine (version 3.7.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     June 2016 
		//* 
		//*     .. Scalar Arguments .. 
		//*     .. 
		//*     .. Array Arguments .. 
		//*     .. 
		//* 
		//*  ===================================================================== 
		//* 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		_isav2oo2 = _4o1bt8b1;
		_l39z5izc = _tixk7d1h;
		if (_sthvw5uv > (int)0)
		{
			
			_wgcg01rd = (int)1;
		}
		else
		{
			
			_wgcg01rd = _4o1bt8b1;
		}
		
		if (_j99gljhp > (int)0)
		{
			
			_3zy83kgw = ((int)1 + _4o1bt8b1);
		}
		else
		{
			
			_3zy83kgw = (_4o1bt8b1 + _tixk7d1h);
		}
		
		_b5p6od9s = (int)1;//*     while ( (N1SV > 0) & (N2SV > 0) ) 
		
Mark10:;
		// continue
		if ((_isav2oo2 > (int)0) & (_l39z5izc > (int)0))
		{
			
			if (*(_vxfgpup9+(_wgcg01rd - 1)) <= *(_vxfgpup9+(_3zy83kgw - 1)))
			{
				
				*(_38qfrq10+(_b5p6od9s - 1)) = _wgcg01rd;
				_b5p6od9s = (_b5p6od9s + (int)1);
				_wgcg01rd = (_wgcg01rd + _sthvw5uv);
				_isav2oo2 = (_isav2oo2 - (int)1);
			}
			else
			{
				
				*(_38qfrq10+(_b5p6od9s - 1)) = _3zy83kgw;
				_b5p6od9s = (_b5p6od9s + (int)1);
				_3zy83kgw = (_3zy83kgw + _j99gljhp);
				_l39z5izc = (_l39z5izc - (int)1);
			}
			goto Mark10;
		}
		//*     end while 
		
		if (_isav2oo2 == (int)0)
		{
			
			{
				System.Int32 __81fgg2dlsvn204 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step204 = (System.Int32)((int)1);
				System.Int32 __81fgg2count204;
				for (__81fgg2count204 = System.Math.Max(0, (System.Int32)(((System.Int32)(_l39z5izc) - __81fgg2dlsvn204 + __81fgg2step204) / __81fgg2step204)), _isav2oo2 = __81fgg2dlsvn204; __81fgg2count204 != 0; __81fgg2count204--, _isav2oo2 += (__81fgg2step204)) {

				{
					
					*(_38qfrq10+(_b5p6od9s - 1)) = _3zy83kgw;
					_b5p6od9s = (_b5p6od9s + (int)1);
					_3zy83kgw = (_3zy83kgw + _j99gljhp);
Mark20:;
					// continue
				}
								}			}
		}
		else
		{
			//*     N2SV .EQ. 0 
			
			{
				System.Int32 __81fgg2dlsvn205 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step205 = (System.Int32)((int)1);
				System.Int32 __81fgg2count205;
				for (__81fgg2count205 = System.Math.Max(0, (System.Int32)(((System.Int32)(_isav2oo2) - __81fgg2dlsvn205 + __81fgg2step205) / __81fgg2step205)), _l39z5izc = __81fgg2dlsvn205; __81fgg2count205 != 0; __81fgg2count205--, _l39z5izc += (__81fgg2step205)) {

				{
					
					*(_38qfrq10+(_b5p6od9s - 1)) = _wgcg01rd;
					_b5p6od9s = (_b5p6od9s + (int)1);
					_wgcg01rd = (_wgcg01rd + _sthvw5uv);
Mark30:;
					// continue
				}
								}			}
		}
		//* 
		
		return;//* 
		//*     End of DLAMRG 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
