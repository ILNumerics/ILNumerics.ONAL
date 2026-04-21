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
//*> \brief \b DLARRC computes the number of eigenvalues of the symmetric tridiagonal matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLARRC + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlarrc.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlarrc.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlarrc.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLARRC( JOBT, N, VL, VU, D, E, PIVMIN, 
//*                                   EIGCNT, LCNT, RCNT, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          JOBT 
//*       INTEGER            EIGCNT, INFO, LCNT, N, RCNT 
//*       DOUBLE PRECISION   PIVMIN, VL, VU 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   D( * ), E( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> Find the number of eigenvalues of the symmetric tridiagonal matrix T 
//*> that are in the interval (VL,VU] if JOBT = 'T', and of L D L^T 
//*> if JOBT = 'L'. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] JOBT 
//*> \verbatim 
//*>          JOBT is CHARACTER*1 
//*>          = 'T':  Compute Sturm count for matrix T. 
//*>          = 'L':  Compute Sturm count for matrix L D L^T. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix. N > 0. 
//*> \endverbatim 
//*> 
//*> \param[in] VL 
//*> \verbatim 
//*>          VL is DOUBLE PRECISION 
//*>          The lower bound for the eigenvalues. 
//*> \endverbatim 
//*> 
//*> \param[in] VU 
//*> \verbatim 
//*>          VU is DOUBLE PRECISION 
//*>          The upper bound for the eigenvalues. 
//*> \endverbatim 
//*> 
//*> \param[in] D 
//*> \verbatim 
//*>          D is DOUBLE PRECISION array, dimension (N) 
//*>          JOBT = 'T': The N diagonal elements of the tridiagonal matrix T. 
//*>          JOBT = 'L': The N diagonal elements of the diagonal matrix D. 
//*> \endverbatim 
//*> 
//*> \param[in] E 
//*> \verbatim 
//*>          E is DOUBLE PRECISION array, dimension (N) 
//*>          JOBT = 'T': The N-1 offdiagonal elements of the matrix T. 
//*>          JOBT = 'L': The N-1 offdiagonal elements of the matrix L. 
//*> \endverbatim 
//*> 
//*> \param[in] PIVMIN 
//*> \verbatim 
//*>          PIVMIN is DOUBLE PRECISION 
//*>          The minimum pivot in the Sturm sequence for T. 
//*> \endverbatim 
//*> 
//*> \param[out] EIGCNT 
//*> \verbatim 
//*>          EIGCNT is INTEGER 
//*>          The number of eigenvalues of the symmetric tridiagonal matrix T 
//*>          that are in the interval (VL,VU] 
//*> \endverbatim 
//*> 
//*> \param[out] LCNT 
//*> \verbatim 
//*>          LCNT is INTEGER 
//*> \endverbatim 
//*> 
//*> \param[out] RCNT 
//*> \verbatim 
//*>          RCNT is INTEGER 
//*>          The left and right negcounts of the interval. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
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

	 
	public static void _13cm454j(FString _6799ol60, ref Int32 _dxpq0xkr, ref Double _ppzorcqs, ref Double _qqhwr930, Double* _plfm7z8g, Double* _864fslqq, ref Double _3aphllyg, ref Int32 _4gjn9p45, ref Int32 _hh4nzb0e, ref Int32 _zpohdf76, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Int32 _b5p6od9s =  default;
Boolean _rbdz7blp =  default;
Double _joptee0t =  default;
Double _6jleymm4 =  default;
Double _g5j86zxy =  default;
Double _in57mf8g =  default;
Double _2qcyvkhx =  default;
Double _ww3bdyup =  default;
string fLanavab = default;
#endregion  variable declarations
_6799ol60 = _6799ol60.Convert(1);

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.1) -- 
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
		//*     .. Parameters .. 
		//*     .. 
		//*     .. Local Scalars .. 
		// 
		//*     .. 
		//*     .. External Functions .. 
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
		
		_hh4nzb0e = (int)0;
		_zpohdf76 = (int)0;
		_4gjn9p45 = (int)0;
		_rbdz7blp = _w8y2rzgy(_6799ol60 ,"T" );// 
		// 
		
		if (_rbdz7blp)
		{
			//*        Sturm sequence count on T 
			
			_joptee0t = (*(_plfm7z8g+((int)1 - 1)) - _ppzorcqs);
			_6jleymm4 = (*(_plfm7z8g+((int)1 - 1)) - _qqhwr930);
			if (_joptee0t <= _d0547bi2)
			{
				
				_hh4nzb0e = (_hh4nzb0e + (int)1);
			}
			
			if (_6jleymm4 <= _d0547bi2)
			{
				
				_zpohdf76 = (_zpohdf76 + (int)1);
			}
			
			{
				System.Int32 __81fgg2dlsvn2867 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2867 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2867;
				for (__81fgg2count2867 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn2867 + __81fgg2step2867) / __81fgg2step2867)), _b5p6od9s = __81fgg2dlsvn2867; __81fgg2count2867 != 0; __81fgg2count2867--, _b5p6od9s += (__81fgg2step2867)) {

				{
					
					_2qcyvkhx = __POW2(*(_864fslqq+(_b5p6od9s - 1)));
					_joptee0t = ((*(_plfm7z8g+(_b5p6od9s + (int)1 - 1)) - _ppzorcqs) - (_2qcyvkhx / _joptee0t));
					_6jleymm4 = ((*(_plfm7z8g+(_b5p6od9s + (int)1 - 1)) - _qqhwr930) - (_2qcyvkhx / _6jleymm4));
					if (_joptee0t <= _d0547bi2)
					{
						
						_hh4nzb0e = (_hh4nzb0e + (int)1);
					}
					
					if (_6jleymm4 <= _d0547bi2)
					{
						
						_zpohdf76 = (_zpohdf76 + (int)1);
					}
					
Mark10:;
					// continue
				}
								}			}
		}
		else
		{
			//*        Sturm sequence count on L D L^T 
			
			_g5j86zxy = (-(_ppzorcqs));
			_in57mf8g = (-(_qqhwr930));
			{
				System.Int32 __81fgg2dlsvn2868 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2868 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2868;
				for (__81fgg2count2868 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn2868 + __81fgg2step2868) / __81fgg2step2868)), _b5p6od9s = __81fgg2dlsvn2868; __81fgg2count2868 != 0; __81fgg2count2868--, _b5p6od9s += (__81fgg2step2868)) {

				{
					
					_joptee0t = (*(_plfm7z8g+(_b5p6od9s - 1)) + _g5j86zxy);
					_6jleymm4 = (*(_plfm7z8g+(_b5p6od9s - 1)) + _in57mf8g);
					if (_joptee0t <= _d0547bi2)
					{
						
						_hh4nzb0e = (_hh4nzb0e + (int)1);
					}
					
					if (_6jleymm4 <= _d0547bi2)
					{
						
						_zpohdf76 = (_zpohdf76 + (int)1);
					}
					
					_2qcyvkhx = ((*(_864fslqq+(_b5p6od9s - 1)) * *(_plfm7z8g+(_b5p6od9s - 1))) * *(_864fslqq+(_b5p6od9s - 1)));//* 
					
					_ww3bdyup = (_2qcyvkhx / _joptee0t);
					if (_ww3bdyup == _d0547bi2)
					{
						
						_g5j86zxy = (_2qcyvkhx - _ppzorcqs);
					}
					else
					{
						
						_g5j86zxy = ((_g5j86zxy * _ww3bdyup) - _ppzorcqs);
					}
					//* 
					
					_ww3bdyup = (_2qcyvkhx / _6jleymm4);
					if (_ww3bdyup == _d0547bi2)
					{
						
						_in57mf8g = (_2qcyvkhx - _qqhwr930);
					}
					else
					{
						
						_in57mf8g = ((_in57mf8g * _ww3bdyup) - _qqhwr930);
					}
					
Mark20:;
					// continue
				}
								}			}
			_joptee0t = (*(_plfm7z8g+(_dxpq0xkr - 1)) + _g5j86zxy);
			_6jleymm4 = (*(_plfm7z8g+(_dxpq0xkr - 1)) + _in57mf8g);
			if (_joptee0t <= _d0547bi2)
			{
				
				_hh4nzb0e = (_hh4nzb0e + (int)1);
			}
			
			if (_6jleymm4 <= _d0547bi2)
			{
				
				_zpohdf76 = (_zpohdf76 + (int)1);
			}
			
		}
		
		_4gjn9p45 = (_zpohdf76 - _hh4nzb0e);// 
		
		return;//* 
		//*     end of DLARRC 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
