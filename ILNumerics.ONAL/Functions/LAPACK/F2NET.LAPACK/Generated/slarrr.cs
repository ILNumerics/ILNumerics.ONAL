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
//*> \brief \b SLARRR performs tests to decide whether the symmetric tridiagonal matrix T warrants expensive computations which guarantee high relative accuracy in the eigenvalues. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLARRR + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slarrr.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slarrr.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slarrr.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLARRR( N, D, E, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            N, INFO 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               D( * ), E( * ) 
//*       .. 
//* 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> Perform tests to decide whether the symmetric tridiagonal matrix T 
//*> warrants expensive computations which guarantee high relative accuracy 
//*> in the eigenvalues. 
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
//*>          D is REAL array, dimension (N) 
//*>          The N diagonal elements of the tridiagonal matrix T. 
//*> \endverbatim 
//*> 
//*> \param[in,out] E 
//*> \verbatim 
//*>          E is REAL array, dimension (N) 
//*>          On entry, the first (N-1) entries contain the subdiagonal 
//*>          elements of the tridiagonal matrix T; E(N) is set to ZERO. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          INFO = 0(default) : the matrix warrants computations preserving 
//*>                              relative accuracy. 
//*>          INFO = 1          : the matrix warrants computations guaranteeing 
//*>                              only absolute accuracy. 
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

	 
	public static void _jzgwtfxi(ref Int32 _dxpq0xkr, Single* _plfm7z8g, Single* _864fslqq, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Single _20xa9qen =  0.999f;
Int32 _b5p6od9s =  default;
Boolean _klwzccc7 =  default;
Single _p1iqarg6 =  default;
Single _h75qnr7l =  default;
Single _bogm0gwy =  default;
Single _sg2xsi4l =  default;
Single _2qcyvkhx =  default;
Single _ww3bdyup =  default;
Single _j6h5mzqu =  default;
Single _jmusjdey =  default;
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
		//* 
		//*  ===================================================================== 
		//* 
		//*     .. Parameters .. 
		//*     .. 
		//*     .. Local Scalars .. 
		// 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr <= (int)0)
		{
			
			_gro5yvfo = (int)0;
			return;
		}
		//* 
		//*     As a default, do NOT go for relative-accuracy preserving computations. 
		
		_gro5yvfo = (int)1;// 
		
		_h75qnr7l = _d5tu038y("Safe minimum" );
		_p1iqarg6 = _d5tu038y("Precision" );
		_bogm0gwy = (_h75qnr7l / _p1iqarg6);
		_sg2xsi4l = ILNumerics.F2NET.Intrinsics.SQRT(_bogm0gwy );// 
		//*     Tests for relative accuracy 
		//* 
		//*     Test for scaled diagonal dominance 
		//*     Scale the diagonal entries to one and check whether the sum of the 
		//*     off-diagonals is less than one 
		//* 
		//*     The sdd relative error bounds have a 1/(1- 2*x) factor in them, 
		//*     x = max(OFFDIG + OFFDIG2), so when x is close to 1/2, no relative 
		//*     accuracy is promised.  In the notation of the code fragment below, 
		//*     1/(1 - (OFFDIG + OFFDIG2)) is the condition number. 
		//*     We don't think it is worth going into "sdd mode" unless the relative 
		//*     condition number is reasonable, not 1/macheps. 
		//*     The threshold should be compatible with other thresholds used in the 
		//*     code. We set  OFFDIG + OFFDIG2 <= .999 =: RELCOND, it corresponds 
		//*     to losing at most 3 decimal digits: 1 / (1 - (OFFDIG + OFFDIG2)) <= 1000 
		//*     instead of the current OFFDIG + OFFDIG2 < 1 
		//* 
		
		_klwzccc7 = true;
		_j6h5mzqu = _d0547bi2;
		_2qcyvkhx = ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+((int)1 - 1)) ) );
		if (_2qcyvkhx < _sg2xsi4l)
		_klwzccc7 = false;
		if (!(_klwzccc7))goto Mark11;
		{
			System.Int32 __81fgg2dlsvn3391 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step3391 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3391;
			for (__81fgg2count3391 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3391 + __81fgg2step3391) / __81fgg2step3391)), _b5p6od9s = __81fgg2dlsvn3391; __81fgg2count3391 != 0; __81fgg2count3391--, _b5p6od9s += (__81fgg2step3391)) {

			{
				
				_ww3bdyup = ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_b5p6od9s - 1)) ) );
				if (_ww3bdyup < _sg2xsi4l)
				_klwzccc7 = false;
				if (!(_klwzccc7))goto Mark11;
				_jmusjdey = (ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_b5p6od9s - (int)1 - 1)) ) / (_2qcyvkhx * _ww3bdyup));
				if ((_j6h5mzqu + _jmusjdey) >= _20xa9qen)
				_klwzccc7 = false;
				if (!(_klwzccc7))goto Mark11;
				_2qcyvkhx = _ww3bdyup;
				_j6h5mzqu = _jmusjdey;
Mark10:;
				// continue
			}
						}		}
Mark11:;
		// continue// 
		
		if (_klwzccc7)
		{
			
			_gro5yvfo = (int)0;
			return;
		}
		//* 
		// 
		//* 
		//*     *** MORE TO BE IMPLEMENTED *** 
		//* 
		// 
		//* 
		//*     Test if the lower bidiagonal matrix L from T = L D L^T 
		//*     (zero shift facto) is well conditioned 
		//* 
		// 
		//* 
		//*     Test if the upper bidiagonal matrix U from T = U D U^T 
		//*     (zero shift facto) is well conditioned. 
		//*     In this case, the matrix needs to be flipped and, at the end 
		//*     of the eigenvector computation, the flip needs to be applied 
		//*     to the computed eigenvectors (and the support) 
		//* 
		// 
		//* 
		
		return;//* 
		//*     END OF SLARRR 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
