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
//*> \brief \b SLARTG generates a plane rotation with real cosine and real sine. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLARTG + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slartg.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slartg.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slartg.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLARTG( F, G, CS, SN, R ) 
//* 
//*       .. Scalar Arguments .. 
//*       REAL               CS, F, G, R, SN 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLARTG generate a plane rotation so that 
//*> 
//*>    [  CS  SN  ]  .  [ F ]  =  [ R ]   where CS**2 + SN**2 = 1. 
//*>    [ -SN  CS  ]     [ G ]     [ 0 ] 
//*> 
//*> This is a slower, more accurate version of the BLAS1 routine SROTG, 
//*> with the following other differences: 
//*>    F and G are unchanged on return. 
//*>    If G=0, then CS=1 and SN=0. 
//*>    If F=0 and (G .ne. 0), then CS=0 and SN=1 without doing any 
//*>       floating point operations (saves work in SBDSQR when 
//*>       there are zeros on the diagonal). 
//*> 
//*> If F exceeds G in magnitude, CS will be positive. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] F 
//*> \verbatim 
//*>          F is REAL 
//*>          The first component of vector to be rotated. 
//*> \endverbatim 
//*> 
//*> \param[in] G 
//*> \verbatim 
//*>          G is REAL 
//*>          The second component of vector to be rotated. 
//*> \endverbatim 
//*> 
//*> \param[out] CS 
//*> \verbatim 
//*>          CS is REAL 
//*>          The cosine of the rotation. 
//*> \endverbatim 
//*> 
//*> \param[out] SN 
//*> \verbatim 
//*>          SN is REAL 
//*>          The sine of the rotation. 
//*> \endverbatim 
//*> 
//*> \param[out] R 
//*> \verbatim 
//*>          R is REAL 
//*>          The nonzero component of the rotated vector. 
//*> 
//*>  This version has a few statements commented out for thread safety 
//*>  (machine parameters are computed on each entry). 10 feb 03, SJH. 
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
//*  ===================================================================== 

	 
	public static void _uf57gsrz(ref Single _8plnuphw, ref Single _mu73se41, ref Single _82tpdhyl, ref Single _8tmd0ner, ref Single _q2vwp05i)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
Single _5m0mjfxm =  2f;
Int32 _jfcumjho =  default;
Int32 _b5p6od9s =  default;
Single _p1iqarg6 =  default;
Single _hh40br9s =  default;
Single _sy3edify =  default;
Single _h75qnr7l =  default;
Single _kiuz6tsq =  default;
Single _9w41ej9y =  default;
Single _1m44vtuk =  default;
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
		//* 
		//*  ===================================================================== 
		//* 
		//*     .. Parameters .. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     LOGICAL            FIRST 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Save statement .. 
		//*     SAVE               FIRST, SAFMX2, SAFMIN, SAFMN2 
		//*     .. 
		//*     .. Data statements .. 
		//*     DATA               FIRST / .TRUE. / 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     IF( FIRST ) THEN 
		
		_h75qnr7l = _d5tu038y("S" );
		_p1iqarg6 = _d5tu038y("E" );
		_kiuz6tsq = __POW(_d5tu038y("B" ), ILNumerics.F2NET.Intrinsics.INT((ILNumerics.F2NET.Intrinsics.LOG(_h75qnr7l / _p1iqarg6 ) / ILNumerics.F2NET.Intrinsics.LOG(_d5tu038y("B" ) )) / _5m0mjfxm ));
		_9w41ej9y = (_kxg5drh2 / _kiuz6tsq);//*        FIRST = .FALSE. 
		//*     END IF 
		
		if (_mu73se41 == _d0547bi2)
		{
			
			_82tpdhyl = _kxg5drh2;
			_8tmd0ner = _d0547bi2;
			_q2vwp05i = _8plnuphw;
		}
		else
		if (_8plnuphw == _d0547bi2)
		{
			
			_82tpdhyl = _d0547bi2;
			_8tmd0ner = _kxg5drh2;
			_q2vwp05i = _mu73se41;
		}
		else
		{
			
			_hh40br9s = _8plnuphw;
			_sy3edify = _mu73se41;
			_1m44vtuk = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_hh40br9s ) ,ILNumerics.F2NET.Intrinsics.ABS(_sy3edify ) );
			if (_1m44vtuk >= _9w41ej9y)
			{
				
				_jfcumjho = (int)0;
Mark10:;
				// continue
				_jfcumjho = (_jfcumjho + (int)1);
				_hh40br9s = (_hh40br9s * _kiuz6tsq);
				_sy3edify = (_sy3edify * _kiuz6tsq);
				_1m44vtuk = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_hh40br9s ) ,ILNumerics.F2NET.Intrinsics.ABS(_sy3edify ) );
				if (_1m44vtuk >= _9w41ej9y)goto Mark10;
				_q2vwp05i = ILNumerics.F2NET.Intrinsics.SQRT(__POW2(_hh40br9s) + __POW2(_sy3edify) );
				_82tpdhyl = (_hh40br9s / _q2vwp05i);
				_8tmd0ner = (_sy3edify / _q2vwp05i);
				{
					System.Int32 __81fgg2dlsvn549 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step549 = (System.Int32)((int)1);
					System.Int32 __81fgg2count549;
					for (__81fgg2count549 = System.Math.Max(0, (System.Int32)(((System.Int32)(_jfcumjho) - __81fgg2dlsvn549 + __81fgg2step549) / __81fgg2step549)), _b5p6od9s = __81fgg2dlsvn549; __81fgg2count549 != 0; __81fgg2count549--, _b5p6od9s += (__81fgg2step549)) {

					{
						
						_q2vwp05i = (_q2vwp05i * _9w41ej9y);
Mark20:;
						// continue
					}
										}				}
			}
			else
			if (_1m44vtuk <= _kiuz6tsq)
			{
				
				_jfcumjho = (int)0;
Mark30:;
				// continue
				_jfcumjho = (_jfcumjho + (int)1);
				_hh40br9s = (_hh40br9s * _9w41ej9y);
				_sy3edify = (_sy3edify * _9w41ej9y);
				_1m44vtuk = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_hh40br9s ) ,ILNumerics.F2NET.Intrinsics.ABS(_sy3edify ) );
				if (_1m44vtuk <= _kiuz6tsq)goto Mark30;
				_q2vwp05i = ILNumerics.F2NET.Intrinsics.SQRT(__POW2(_hh40br9s) + __POW2(_sy3edify) );
				_82tpdhyl = (_hh40br9s / _q2vwp05i);
				_8tmd0ner = (_sy3edify / _q2vwp05i);
				{
					System.Int32 __81fgg2dlsvn550 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step550 = (System.Int32)((int)1);
					System.Int32 __81fgg2count550;
					for (__81fgg2count550 = System.Math.Max(0, (System.Int32)(((System.Int32)(_jfcumjho) - __81fgg2dlsvn550 + __81fgg2step550) / __81fgg2step550)), _b5p6od9s = __81fgg2dlsvn550; __81fgg2count550 != 0; __81fgg2count550--, _b5p6od9s += (__81fgg2step550)) {

					{
						
						_q2vwp05i = (_q2vwp05i * _kiuz6tsq);
Mark40:;
						// continue
					}
										}				}
			}
			else
			{
				
				_q2vwp05i = ILNumerics.F2NET.Intrinsics.SQRT(__POW2(_hh40br9s) + __POW2(_sy3edify) );
				_82tpdhyl = (_hh40br9s / _q2vwp05i);
				_8tmd0ner = (_sy3edify / _q2vwp05i);
			}
			
			if ((ILNumerics.F2NET.Intrinsics.ABS(_8plnuphw ) > ILNumerics.F2NET.Intrinsics.ABS(_mu73se41 )) & (_82tpdhyl < _d0547bi2))
			{
				
				_82tpdhyl = (-(_82tpdhyl));
				_8tmd0ner = (-(_8tmd0ner));
				_q2vwp05i = (-(_q2vwp05i));
			}
			
		}
		
		return;//* 
		//*     End of SLARTG 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
