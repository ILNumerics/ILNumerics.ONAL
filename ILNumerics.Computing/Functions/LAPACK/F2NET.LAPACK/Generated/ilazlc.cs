
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
//*> \brief \b ILAZLC scans a matrix for its last non-zero column. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ILAZLC + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/ilazlc.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/ilazlc.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/ilazlc.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       INTEGER FUNCTION ILAZLC( M, N, A, LDA ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            M, N, LDA 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX*16         A( LDA, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ILAZLC scans A for its last non-zero column. 
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
//*>          A is COMPLEX*16 array, dimension (LDA,N) 
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
//*> \ingroup complex16OTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static Int32 _gnffrjin(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, complex* _vxfgpup9, ref Int32 _ocv8fk5c)
	{
#region variable declarations
Int32 _gnffrjin = default;
complex _d0547bi2 =   new fcomplex(0f,0f);
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
			
			_gnffrjin = _dxpq0xkr;
		}
		else
		if ((*(_vxfgpup9+((int)1 - 1) + (_dxpq0xkr - 1) * 1 * (_ocv8fk5c)) != _d0547bi2) | (*(_vxfgpup9+(_ev4xhht5 - 1) + (_dxpq0xkr - 1) * 1 * (_ocv8fk5c)) != _d0547bi2))
		{
			
			_gnffrjin = _dxpq0xkr;
		}
		else
		{
			//*     Now scan each column from the end, returning with the first non-zero. 
			
			{
				System.Int32 __81fgg2dlsvn1161 = (System.Int32)(_dxpq0xkr);
				System.Int32 __81fgg2step1161 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count1161;
				for (__81fgg2count1161 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1161 + __81fgg2step1161) / __81fgg2step1161)), _gnffrjin = __81fgg2dlsvn1161; __81fgg2count1161 != 0; __81fgg2count1161--, _gnffrjin += (__81fgg2step1161)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn1162 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1162 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1162;
						for (__81fgg2count1162 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1162 + __81fgg2step1162) / __81fgg2step1162)), _b5p6od9s = __81fgg2dlsvn1162; __81fgg2count1162 != 0; __81fgg2count1162--, _b5p6od9s += (__81fgg2step1162)) {

						{
							
							if (*(_vxfgpup9+(_b5p6od9s - 1) + (_gnffrjin - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
							return _gnffrjin;
						}
												}					}
				}
								}			}
		}
		
		return _gnffrjin;
	}
	
	return _gnffrjin;
	} // 177

} // end class 
} // end namespace
#endif
