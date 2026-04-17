
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
//*> \brief \b ILASLC scans a matrix for its last non-zero column. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ILASLC + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/ilaslc.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/ilaslc.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/ilaslc.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       INTEGER FUNCTION ILASLC( M, N, A, LDA ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            M, N, LDA 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               A( LDA, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ILASLC scans A for its last non-zero column. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of rows of the matrix A. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of columns of the matrix A. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is REAL array, dimension (LDA,N) 
//*>          The m by n matrix A. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A. LDA >= max(1,M). 
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
//*> \ingroup realOTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static Int32 _grpg1g94(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, Single* _vxfgpup9, ref Int32 _ocv8fk5c)
	{
#region variable declarations
Int32 _grpg1g94 = default;
Single _d0547bi2 =  0f;
Int32 _b5p6od9s =  default;
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
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Quick test for the common case where one corner is non-zero. 
		
		if (_dxpq0xkr == (int)0)
		{
			
			_grpg1g94 = _dxpq0xkr;
		}
		else
		if ((*(_vxfgpup9+((int)1 - 1) + (_dxpq0xkr - 1) * 1 * (_ocv8fk5c)) != _d0547bi2) | (*(_vxfgpup9+(_ev4xhht5 - 1) + (_dxpq0xkr - 1) * 1 * (_ocv8fk5c)) != _d0547bi2))
		{
			
			_grpg1g94 = _dxpq0xkr;
		}
		else
		{
			//*     Now scan each column from the end, returning with the first non-zero. 
			
			{
				System.Int32 __81fgg2dlsvn784 = (System.Int32)(_dxpq0xkr);
				System.Int32 __81fgg2step784 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count784;
				for (__81fgg2count784 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn784 + __81fgg2step784) / __81fgg2step784)), _grpg1g94 = __81fgg2dlsvn784; __81fgg2count784 != 0; __81fgg2count784--, _grpg1g94 += (__81fgg2step784)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn785 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step785 = (System.Int32)((int)1);
						System.Int32 __81fgg2count785;
						for (__81fgg2count785 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn785 + __81fgg2step785) / __81fgg2step785)), _b5p6od9s = __81fgg2dlsvn785; __81fgg2count785 != 0; __81fgg2count785--, _b5p6od9s += (__81fgg2step785)) {

						{
							
							if (*(_vxfgpup9+(_b5p6od9s - 1) + (_grpg1g94 - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
							return _grpg1g94;
						}
												}					}
				}
								}			}
		}
		
		return _grpg1g94;
	}
	
	return _grpg1g94;
	} // 177

} // end class 
} // end namespace
#endif
