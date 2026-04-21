// MIT License
// 
// Copyright (c) 2026 ILNumerics GmbH
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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
//*> \brief \b SLAMRG creates a permutation list to merge the entries of two independently sorted sets into a single set sorted in ascending order. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLAMRG + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slamrg.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slamrg.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slamrg.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLAMRG( N1, N2, A, STRD1, STRD2, INDEX ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            N1, N2, STRD1, STRD2 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            INDEX( * ) 
//*       REAL               A( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLAMRG will create a permutation list which will merge the elements 
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
//*>          A is REAL array, dimension (N1+N2) 
//*>         The first N1 elements of A contain a list of numbers which 
//*>         are sorted in either ascending or descending order.  Likewise 
//*>         for the final N2 elements. 
//*> \endverbatim 
//*> 
//*> \param[in] STRD1 
//*> \verbatim 
//*>          STRD1 is INTEGER 
//*> \endverbatim 
//*> 
//*> \param[in] STRD2 
//*> \verbatim 
//*>          STRD2 is INTEGER 
//*>         These are the strides to be taken through the array A. 
//*>         Allowable strides are 1 and -1.  They indicate whether a 
//*>         subset of A is sorted in ascending (STRDx = 1) or descending 
//*>         (STRDx = -1) order. 
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

	 
	public static void _5cjk91e1(ref Int32 _4o1bt8b1, ref Int32 _tixk7d1h, Single* _vxfgpup9, ref Int32 _ddf6fea5, ref Int32 _b1g1usod, Int32* _38qfrq10)
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
		if (_ddf6fea5 > (int)0)
		{
			
			_wgcg01rd = (int)1;
		}
		else
		{
			
			_wgcg01rd = _4o1bt8b1;
		}
		
		if (_b1g1usod > (int)0)
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
				_wgcg01rd = (_wgcg01rd + _ddf6fea5);
				_isav2oo2 = (_isav2oo2 - (int)1);
			}
			else
			{
				
				*(_38qfrq10+(_b5p6od9s - 1)) = _3zy83kgw;
				_b5p6od9s = (_b5p6od9s + (int)1);
				_3zy83kgw = (_3zy83kgw + _b1g1usod);
				_l39z5izc = (_l39z5izc - (int)1);
			}
			goto Mark10;
		}
		//*     end while 
		
		if (_isav2oo2 == (int)0)
		{
			
			{
				System.Int32 __81fgg2dlsvn571 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step571 = (System.Int32)((int)1);
				System.Int32 __81fgg2count571;
				for (__81fgg2count571 = System.Math.Max(0, (System.Int32)(((System.Int32)(_l39z5izc) - __81fgg2dlsvn571 + __81fgg2step571) / __81fgg2step571)), _isav2oo2 = __81fgg2dlsvn571; __81fgg2count571 != 0; __81fgg2count571--, _isav2oo2 += (__81fgg2step571)) {

				{
					
					*(_38qfrq10+(_b5p6od9s - 1)) = _3zy83kgw;
					_b5p6od9s = (_b5p6od9s + (int)1);
					_3zy83kgw = (_3zy83kgw + _b1g1usod);
Mark20:;
					// continue
				}
								}			}
		}
		else
		{
			//*     N2SV .EQ. 0 
			
			{
				System.Int32 __81fgg2dlsvn572 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step572 = (System.Int32)((int)1);
				System.Int32 __81fgg2count572;
				for (__81fgg2count572 = System.Math.Max(0, (System.Int32)(((System.Int32)(_isav2oo2) - __81fgg2dlsvn572 + __81fgg2step572) / __81fgg2step572)), _l39z5izc = __81fgg2dlsvn572; __81fgg2count572 != 0; __81fgg2count572--, _l39z5izc += (__81fgg2step572)) {

				{
					
					*(_38qfrq10+(_b5p6od9s - 1)) = _wgcg01rd;
					_b5p6od9s = (_b5p6od9s + (int)1);
					_wgcg01rd = (_wgcg01rd + _ddf6fea5);
Mark30:;
					// continue
				}
								}			}
		}
		//* 
		
		return;//* 
		//*     End of SLAMRG 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
