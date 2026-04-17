
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
//*> \brief \b ILACLC scans a matrix for its last non-zero column. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ILACLC + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/ilaclc.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/ilaclc.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/ilaclc.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       INTEGER FUNCTION ILACLC( M, N, A, LDA ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            M, N, LDA 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX            A( LDA, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ILACLC scans A for its last non-zero column. 
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
//*>          A is COMPLEX array, dimension (LDA,N) 
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
//*> \date December 2016 
//* 
//*> \ingroup complexOTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static Int32 _u3r7gain(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c)
	{
#region variable declarations
Int32 _u3r7gain = default;
fcomplex _d0547bi2 =   new fcomplex(0f,0f);
Int32 _b5p6od9s =  default;
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
		//*     .. Executable Statements .. 
		//* 
		//*     Quick test for the common case where one corner is non-zero. 
		
		if (_dxpq0xkr == (int)0)
		{
			
			_u3r7gain = _dxpq0xkr;
		}
		else
		if ((*(_vxfgpup9+((int)1 - 1) + (_dxpq0xkr - 1) * 1 * (_ocv8fk5c)) != _d0547bi2) | (*(_vxfgpup9+(_ev4xhht5 - 1) + (_dxpq0xkr - 1) * 1 * (_ocv8fk5c)) != _d0547bi2))
		{
			
			_u3r7gain = _dxpq0xkr;
		}
		else
		{
			//*     Now scan each column from the end, returning with the first non-zero. 
			
			{
				System.Int32 __81fgg2dlsvn944 = (System.Int32)(_dxpq0xkr);
				System.Int32 __81fgg2step944 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count944;
				for (__81fgg2count944 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn944 + __81fgg2step944) / __81fgg2step944)), _u3r7gain = __81fgg2dlsvn944; __81fgg2count944 != 0; __81fgg2count944--, _u3r7gain += (__81fgg2step944)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn945 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step945 = (System.Int32)((int)1);
						System.Int32 __81fgg2count945;
						for (__81fgg2count945 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn945 + __81fgg2step945) / __81fgg2step945)), _b5p6od9s = __81fgg2dlsvn945; __81fgg2count945 != 0; __81fgg2count945--, _b5p6od9s += (__81fgg2step945)) {

						{
							
							if (*(_vxfgpup9+(_b5p6od9s - 1) + (_u3r7gain - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
							return _u3r7gain;
						}
												}					}
				}
								}			}
		}
		
		return _u3r7gain;
	}
	
	return _u3r7gain;
	} // 177

} // end class 
} // end namespace
#endif
