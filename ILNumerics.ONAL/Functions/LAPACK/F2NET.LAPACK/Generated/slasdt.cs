
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
//*> \brief \b SLASDT creates a tree of subproblems for bidiagonal divide and conquer. Used by sbdsdc. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLASDT + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slasdt.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slasdt.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slasdt.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLASDT( N, LVL, ND, INODE, NDIML, NDIMR, MSUB ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            LVL, MSUB, N, ND 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            INODE( * ), NDIML( * ), NDIMR( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLASDT creates a tree of subproblems for bidiagonal divide and 
//*> conquer. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          On entry, the number of diagonal elements of the 
//*>          bidiagonal matrix. 
//*> \endverbatim 
//*> 
//*> \param[out] LVL 
//*> \verbatim 
//*>          LVL is INTEGER 
//*>          On exit, the number of levels on the computation tree. 
//*> \endverbatim 
//*> 
//*> \param[out] ND 
//*> \verbatim 
//*>          ND is INTEGER 
//*>          On exit, the number of nodes on the tree. 
//*> \endverbatim 
//*> 
//*> \param[out] INODE 
//*> \verbatim 
//*>          INODE is INTEGER array, dimension ( N ) 
//*>          On exit, centers of subproblems. 
//*> \endverbatim 
//*> 
//*> \param[out] NDIML 
//*> \verbatim 
//*>          NDIML is INTEGER array, dimension ( N ) 
//*>          On exit, row dimensions of left children. 
//*> \endverbatim 
//*> 
//*> \param[out] NDIMR 
//*> \verbatim 
//*>          NDIMR is INTEGER array, dimension ( N ) 
//*>          On exit, row dimensions of right children. 
//*> \endverbatim 
//*> 
//*> \param[in] MSUB 
//*> \verbatim 
//*>          MSUB is INTEGER 
//*>          On entry, the maximum row dimension each subproblem at the 
//*>          bottom of the tree can be of. 
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
//*> \ingroup OTHERauxiliary 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>     Ming Gu and Huan Ren, Computer Science Division, University of 
//*>     California at Berkeley, USA 
//*> 
//*  ===================================================================== 

	 
	public static void _k6l39brz(ref Int32 _dxpq0xkr, ref Int32 _u0afxfs0, ref Int32 _rwm6akyl, Int32* _q8fqp221, Int32* _b53e0l58, Int32* _9bq9s7q7, ref Int32 _tykfkam9)
	{
#region variable declarations
Single _5m0mjfxm =  2f;
Int32 _b5p6od9s =  default;
Int32 _ic6kua09 =  default;
Int32 _m9w6lk7x =  default;
Int32 _nql7n2z0 =  default;
Int32 _yvgb81xo =  default;
Int32 _doirkxnn =  default;
Int32 _0n683y3x =  default;
Single _1ajfmh55 =  default;
string fLanavab = default;
#endregion  variable declarations

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
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Find the number of levels on the tree. 
		//* 
		
		_yvgb81xo = ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr );
		_1ajfmh55 = (ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.REAL(_yvgb81xo ) / ILNumerics.F2NET.Intrinsics.REAL(_tykfkam9 + (int)1 ) ) / ILNumerics.F2NET.Intrinsics.LOG(_5m0mjfxm ));
		_u0afxfs0 = (ILNumerics.F2NET.Intrinsics.INT(_1ajfmh55 ) + (int)1);//* 
		
		_b5p6od9s = (_dxpq0xkr / (int)2);
		*(_q8fqp221+((int)1 - 1)) = (_b5p6od9s + (int)1);
		*(_b53e0l58+((int)1 - 1)) = _b5p6od9s;
		*(_9bq9s7q7+((int)1 - 1)) = ((_dxpq0xkr - _b5p6od9s) - (int)1);
		_ic6kua09 = (int)0;
		_m9w6lk7x = (int)1;
		_nql7n2z0 = (int)1;
		{
			System.Int32 __81fgg2dlsvn735 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step735 = (System.Int32)((int)1);
			System.Int32 __81fgg2count735;
			for (__81fgg2count735 = System.Math.Max(0, (System.Int32)(((System.Int32)(_u0afxfs0 - (int)1) - __81fgg2dlsvn735 + __81fgg2step735) / __81fgg2step735)), _0n683y3x = __81fgg2dlsvn735; __81fgg2count735 != 0; __81fgg2count735--, _0n683y3x += (__81fgg2step735)) {

			{
				//* 
				//*        Constructing the tree at (NLVL+1)-st level. The number of 
				//*        nodes created on this level is LLST * 2. 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn736 = (System.Int32)((int)0);
					const System.Int32 __81fgg2step736 = (System.Int32)((int)1);
					System.Int32 __81fgg2count736;
					for (__81fgg2count736 = System.Math.Max(0, (System.Int32)(((System.Int32)(_nql7n2z0 - (int)1) - __81fgg2dlsvn736 + __81fgg2step736) / __81fgg2step736)), _b5p6od9s = __81fgg2dlsvn736; __81fgg2count736 != 0; __81fgg2count736--, _b5p6od9s += (__81fgg2step736)) {

					{
						
						_ic6kua09 = (_ic6kua09 + (int)2);
						_m9w6lk7x = (_m9w6lk7x + (int)2);
						_doirkxnn = (_nql7n2z0 + _b5p6od9s);
						*(_b53e0l58+(_ic6kua09 - 1)) = (*(_b53e0l58+(_doirkxnn - 1)) / (int)2);
						*(_9bq9s7q7+(_ic6kua09 - 1)) = ((*(_b53e0l58+(_doirkxnn - 1)) - *(_b53e0l58+(_ic6kua09 - 1))) - (int)1);
						*(_q8fqp221+(_ic6kua09 - 1)) = ((*(_q8fqp221+(_doirkxnn - 1)) - *(_9bq9s7q7+(_ic6kua09 - 1))) - (int)1);
						*(_b53e0l58+(_m9w6lk7x - 1)) = (*(_9bq9s7q7+(_doirkxnn - 1)) / (int)2);
						*(_9bq9s7q7+(_m9w6lk7x - 1)) = ((*(_9bq9s7q7+(_doirkxnn - 1)) - *(_b53e0l58+(_m9w6lk7x - 1))) - (int)1);
						*(_q8fqp221+(_m9w6lk7x - 1)) = ((*(_q8fqp221+(_doirkxnn - 1)) + *(_b53e0l58+(_m9w6lk7x - 1))) + (int)1);
Mark10:;
						// continue
					}
										}				}
				_nql7n2z0 = (_nql7n2z0 * (int)2);
Mark20:;
				// continue
			}
						}		}
		_rwm6akyl = ((_nql7n2z0 * (int)2) - (int)1);//* 
		
		return;//* 
		//*     End of SLASDT 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
