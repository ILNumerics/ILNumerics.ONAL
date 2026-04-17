
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
//*> \brief \b ILAZLR scans a matrix for its last non-zero row. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ILAZLR + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/ilazlr.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/ilazlr.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/ilazlr.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       INTEGER FUNCTION ILAZLR( M, N, A, LDA ) 
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
//*> ILAZLR scans A for its last non-zero row. 
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

	 
	public static Int32 _351nrs0x(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, complex* _vxfgpup9, ref Int32 _ocv8fk5c)
	{
#region variable declarations
Int32 _351nrs0x = default;
complex _d0547bi2 =   new fcomplex(0f,0f);
Int32 _b5p6od9s =  default;
Int32 _znpjgsef =  default;
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
		
		if (_ev4xhht5 == (int)0)
		{
			
			_351nrs0x = _ev4xhht5;
		}
		else
		if ((*(_vxfgpup9+(_ev4xhht5 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) != _d0547bi2) | (*(_vxfgpup9+(_ev4xhht5 - 1) + (_dxpq0xkr - 1) * 1 * (_ocv8fk5c)) != _d0547bi2))
		{
			
			_351nrs0x = _ev4xhht5;
		}
		else
		{
			//*     Scan up each column tracking the last zero row seen. 
			
			_351nrs0x = (int)0;
			{
				System.Int32 __81fgg2dlsvn1160 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1160 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1160;
				for (__81fgg2count1160 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1160 + __81fgg2step1160) / __81fgg2step1160)), _znpjgsef = __81fgg2dlsvn1160; __81fgg2count1160 != 0; __81fgg2count1160--, _znpjgsef += (__81fgg2step1160)) {

				{
					
					_b5p6od9s = _ev4xhht5;
					{
while (((*(_vxfgpup9+(ILNumerics.F2NET.Intrinsics.MAX(_b5p6od9s ,(int)1 ) - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) == _d0547bi2) & (_b5p6od9s >= (int)1))) {
						{
							
							_b5p6od9s = (_b5p6od9s - (int)1);
						}
												}					}
					_351nrs0x = ILNumerics.F2NET.Intrinsics.MAX(_351nrs0x ,_b5p6od9s );
				}
								}			}
		}
		
		return _351nrs0x;
	}
	
	return _351nrs0x;
	} // 177

} // end class 
} // end namespace
#endif
