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
//*> \brief \b DLASD5 computes the square root of the i-th eigenvalue of a positive symmetric rank-one modification of a 2-by-2 diagonal matrix. Used by sbdsdc. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLASD5 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlasd5.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlasd5.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlasd5.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLASD5( I, D, Z, DELTA, RHO, DSIGMA, WORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            I 
//*       DOUBLE PRECISION   DSIGMA, RHO 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   D( 2 ), DELTA( 2 ), WORK( 2 ), Z( 2 ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> This subroutine computes the square root of the I-th eigenvalue 
//*> of a positive symmetric rank-one modification of a 2-by-2 diagonal 
//*> matrix 
//*> 
//*>            diag( D ) * diag( D ) +  RHO * Z * transpose(Z) . 
//*> 
//*> The diagonal entries in the array D are assumed to satisfy 
//*> 
//*>            0 <= D(i) < D(j)  for  i < j . 
//*> 
//*> We also assume RHO > 0 and that the Euclidean norm of the vector 
//*> Z is one. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] I 
//*> \verbatim 
//*>          I is INTEGER 
//*>         The index of the eigenvalue to be computed.  I = 1 or I = 2. 
//*> \endverbatim 
//*> 
//*> \param[in] D 
//*> \verbatim 
//*>          D is DOUBLE PRECISION array, dimension ( 2 ) 
//*>         The original eigenvalues.  We assume 0 <= D(1) < D(2). 
//*> \endverbatim 
//*> 
//*> \param[in] Z 
//*> \verbatim 
//*>          Z is DOUBLE PRECISION array, dimension ( 2 ) 
//*>         The components of the updating vector. 
//*> \endverbatim 
//*> 
//*> \param[out] DELTA 
//*> \verbatim 
//*>          DELTA is DOUBLE PRECISION array, dimension ( 2 ) 
//*>         Contains (D(j) - sigma_I) in its  j-th component. 
//*>         The vector DELTA contains the information necessary 
//*>         to construct the eigenvectors. 
//*> \endverbatim 
//*> 
//*> \param[in] RHO 
//*> \verbatim 
//*>          RHO is DOUBLE PRECISION 
//*>         The scalar in the symmetric updating formula. 
//*> \endverbatim 
//*> 
//*> \param[out] DSIGMA 
//*> \verbatim 
//*>          DSIGMA is DOUBLE PRECISION 
//*>         The computed sigma_I, the I-th updated eigenvalue. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is DOUBLE PRECISION array, dimension ( 2 ) 
//*>         WORK contains (D(j) + sigma_I) in its  j-th component. 
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
//*>     Ren-Cang Li, Computer Science Division, University of California 
//*>     at Berkeley, USA 
//*> 
//*  ===================================================================== 

	 
	public static void _aue9t9j1(ref Int32 _b5p6od9s, Double* _plfm7z8g, Double* _7e60fcso, Double* _9zhf8o7p, ref Double _4qwfue8o, ref Double _1r8q3o4r, Double* _apig8meb)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Double _5m0mjfxm =  2d;
Double _08e01ee2 =  3d;
Double _ax5ijvbx =  4d;
Double _p9n405a5 =  default;
Double _3crf0qn3 =  default;
Double _62elm18x =  default;
Double _8k4zqcbt =  default;
Double _0446f4de =  default;
Double _z1ioc3c8 =  default;
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
		
		_62elm18x = (*(_plfm7z8g+((int)2 - 1)) - *(_plfm7z8g+((int)1 - 1)));
		_8k4zqcbt = (_62elm18x * (*(_plfm7z8g+((int)2 - 1)) + *(_plfm7z8g+((int)1 - 1))));
		if (_b5p6od9s == (int)1)
		{
			
			_z1ioc3c8 = (_kxg5drh2 + (((_ax5ijvbx * _4qwfue8o) * (((*(_7e60fcso+((int)2 - 1)) * *(_7e60fcso+((int)2 - 1))) / (*(_plfm7z8g+((int)1 - 1)) + (_08e01ee2 * *(_plfm7z8g+((int)2 - 1))))) - ((*(_7e60fcso+((int)1 - 1)) * *(_7e60fcso+((int)1 - 1))) / ((_08e01ee2 * *(_plfm7z8g+((int)1 - 1))) + *(_plfm7z8g+((int)2 - 1)))))) / _62elm18x));
			if (_z1ioc3c8 > _d0547bi2)
			{
				
				_p9n405a5 = (_8k4zqcbt + (_4qwfue8o * ((*(_7e60fcso+((int)1 - 1)) * *(_7e60fcso+((int)1 - 1))) + (*(_7e60fcso+((int)2 - 1)) * *(_7e60fcso+((int)2 - 1))))));
				_3crf0qn3 = (((_4qwfue8o * *(_7e60fcso+((int)1 - 1))) * *(_7e60fcso+((int)1 - 1))) * _8k4zqcbt);//* 
				//*           B > ZERO, always 
				//* 
				//*           The following TAU is DSIGMA * DSIGMA - D( 1 ) * D( 1 ) 
				//* 
				
				_0446f4de = ((_5m0mjfxm * _3crf0qn3) / (_p9n405a5 + ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS((_p9n405a5 * _p9n405a5) - (_ax5ijvbx * _3crf0qn3) ) )));//* 
				//*           The following TAU is DSIGMA - D( 1 ) 
				//* 
				
				_0446f4de = (_0446f4de / (*(_plfm7z8g+((int)1 - 1)) + ILNumerics.F2NET.Intrinsics.SQRT((*(_plfm7z8g+((int)1 - 1)) * *(_plfm7z8g+((int)1 - 1))) + _0446f4de )));
				_1r8q3o4r = (*(_plfm7z8g+((int)1 - 1)) + _0446f4de);
				*(_9zhf8o7p+((int)1 - 1)) = (-(_0446f4de));
				*(_9zhf8o7p+((int)2 - 1)) = (_62elm18x - _0446f4de);
				*(_apig8meb+((int)1 - 1)) = ((_5m0mjfxm * *(_plfm7z8g+((int)1 - 1))) + _0446f4de);
				*(_apig8meb+((int)2 - 1)) = ((*(_plfm7z8g+((int)1 - 1)) + _0446f4de) + *(_plfm7z8g+((int)2 - 1)));//*           DELTA( 1 ) = -Z( 1 ) / TAU 
				//*           DELTA( 2 ) = Z( 2 ) / ( DEL-TAU ) 
				
			}
			else
			{
				
				_p9n405a5 = ((-(_8k4zqcbt)) + (_4qwfue8o * ((*(_7e60fcso+((int)1 - 1)) * *(_7e60fcso+((int)1 - 1))) + (*(_7e60fcso+((int)2 - 1)) * *(_7e60fcso+((int)2 - 1))))));
				_3crf0qn3 = (((_4qwfue8o * *(_7e60fcso+((int)2 - 1))) * *(_7e60fcso+((int)2 - 1))) * _8k4zqcbt);//* 
				//*           The following TAU is DSIGMA * DSIGMA - D( 2 ) * D( 2 ) 
				//* 
				
				if (_p9n405a5 > _d0547bi2)
				{
					
					_0446f4de = (-(((_5m0mjfxm * _3crf0qn3) / (_p9n405a5 + ILNumerics.F2NET.Intrinsics.SQRT((_p9n405a5 * _p9n405a5) + (_ax5ijvbx * _3crf0qn3) )))));
				}
				else
				{
					
					_0446f4de = ((_p9n405a5 - ILNumerics.F2NET.Intrinsics.SQRT((_p9n405a5 * _p9n405a5) + (_ax5ijvbx * _3crf0qn3) )) / _5m0mjfxm);
				}
				//* 
				//*           The following TAU is DSIGMA - D( 2 ) 
				//* 
				
				_0446f4de = (_0446f4de / (*(_plfm7z8g+((int)2 - 1)) + ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS((*(_plfm7z8g+((int)2 - 1)) * *(_plfm7z8g+((int)2 - 1))) + _0446f4de ) )));
				_1r8q3o4r = (*(_plfm7z8g+((int)2 - 1)) + _0446f4de);
				*(_9zhf8o7p+((int)1 - 1)) = (-((_62elm18x + _0446f4de)));
				*(_9zhf8o7p+((int)2 - 1)) = (-(_0446f4de));
				*(_apig8meb+((int)1 - 1)) = ((*(_plfm7z8g+((int)1 - 1)) + _0446f4de) + *(_plfm7z8g+((int)2 - 1)));
				*(_apig8meb+((int)2 - 1)) = ((_5m0mjfxm * *(_plfm7z8g+((int)2 - 1))) + _0446f4de);//*           DELTA( 1 ) = -Z( 1 ) / ( DEL+TAU ) 
				//*           DELTA( 2 ) = -Z( 2 ) / TAU 
				
			}
			//*        TEMP = SQRT( DELTA( 1 )*DELTA( 1 )+DELTA( 2 )*DELTA( 2 ) ) 
			//*        DELTA( 1 ) = DELTA( 1 ) / TEMP 
			//*        DELTA( 2 ) = DELTA( 2 ) / TEMP 
			
		}
		else
		{
			//* 
			//*        Now I=2 
			//* 
			
			_p9n405a5 = ((-(_8k4zqcbt)) + (_4qwfue8o * ((*(_7e60fcso+((int)1 - 1)) * *(_7e60fcso+((int)1 - 1))) + (*(_7e60fcso+((int)2 - 1)) * *(_7e60fcso+((int)2 - 1))))));
			_3crf0qn3 = (((_4qwfue8o * *(_7e60fcso+((int)2 - 1))) * *(_7e60fcso+((int)2 - 1))) * _8k4zqcbt);//* 
			//*        The following TAU is DSIGMA * DSIGMA - D( 2 ) * D( 2 ) 
			//* 
			
			if (_p9n405a5 > _d0547bi2)
			{
				
				_0446f4de = ((_p9n405a5 + ILNumerics.F2NET.Intrinsics.SQRT((_p9n405a5 * _p9n405a5) + (_ax5ijvbx * _3crf0qn3) )) / _5m0mjfxm);
			}
			else
			{
				
				_0446f4de = ((_5m0mjfxm * _3crf0qn3) / ((-(_p9n405a5)) + ILNumerics.F2NET.Intrinsics.SQRT((_p9n405a5 * _p9n405a5) + (_ax5ijvbx * _3crf0qn3) )));
			}
			//* 
			//*        The following TAU is DSIGMA - D( 2 ) 
			//* 
			
			_0446f4de = (_0446f4de / (*(_plfm7z8g+((int)2 - 1)) + ILNumerics.F2NET.Intrinsics.SQRT((*(_plfm7z8g+((int)2 - 1)) * *(_plfm7z8g+((int)2 - 1))) + _0446f4de )));
			_1r8q3o4r = (*(_plfm7z8g+((int)2 - 1)) + _0446f4de);
			*(_9zhf8o7p+((int)1 - 1)) = (-((_62elm18x + _0446f4de)));
			*(_9zhf8o7p+((int)2 - 1)) = (-(_0446f4de));
			*(_apig8meb+((int)1 - 1)) = ((*(_plfm7z8g+((int)1 - 1)) + _0446f4de) + *(_plfm7z8g+((int)2 - 1)));
			*(_apig8meb+((int)2 - 1)) = ((_5m0mjfxm * *(_plfm7z8g+((int)2 - 1))) + _0446f4de);//*        DELTA( 1 ) = -Z( 1 ) / ( DEL+TAU ) 
			//*        DELTA( 2 ) = -Z( 2 ) / TAU 
			//*        TEMP = SQRT( DELTA( 1 )*DELTA( 1 )+DELTA( 2 )*DELTA( 2 ) ) 
			//*        DELTA( 1 ) = DELTA( 1 ) / TEMP 
			//*        DELTA( 2 ) = DELTA( 2 ) / TEMP 
			
		}
		
		return;//* 
		//*     End of DLASD5 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
