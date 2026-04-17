
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
//*> \brief \b DLARTG generates a plane rotation with real cosine and real sine. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLARTG + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlartg.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlartg.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlartg.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLARTG( F, G, CS, SN, R ) 
//* 
//*       .. Scalar Arguments .. 
//*       DOUBLE PRECISION   CS, F, G, R, SN 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLARTG generate a plane rotation so that 
//*> 
//*>    [  CS  SN  ]  .  [ F ]  =  [ R ]   where CS**2 + SN**2 = 1. 
//*>    [ -SN  CS  ]     [ G ]     [ 0 ] 
//*> 
//*> This is a slower, more accurate version of the BLAS1 routine DROTG, 
//*> with the following other differences: 
//*>    F and G are unchanged on return. 
//*>    If G=0, then CS=1 and SN=0. 
//*>    If F=0 and (G .ne. 0), then CS=0 and SN=1 without doing any 
//*>       floating point operations (saves work in DBDSQR when 
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
//*>          F is DOUBLE PRECISION 
//*>          The first component of vector to be rotated. 
//*> \endverbatim 
//*> 
//*> \param[in] G 
//*> \verbatim 
//*>          G is DOUBLE PRECISION 
//*>          The second component of vector to be rotated. 
//*> \endverbatim 
//*> 
//*> \param[out] CS 
//*> \verbatim 
//*>          CS is DOUBLE PRECISION 
//*>          The cosine of the rotation. 
//*> \endverbatim 
//*> 
//*> \param[out] SN 
//*> \verbatim 
//*>          SN is DOUBLE PRECISION 
//*>          The sine of the rotation. 
//*> \endverbatim 
//*> 
//*> \param[out] R 
//*> \verbatim 
//*>          R is DOUBLE PRECISION 
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

	 
	public static void _uasfzoa5(ref Double _8plnuphw, ref Double _mu73se41, ref Double _82tpdhyl, ref Double _8tmd0ner, ref Double _q2vwp05i)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Double _5m0mjfxm =  2d;
Int32 _jfcumjho =  default;
Int32 _b5p6od9s =  default;
Double _p1iqarg6 =  default;
Double _hh40br9s =  default;
Double _sy3edify =  default;
Double _h75qnr7l =  default;
Double _kiuz6tsq =  default;
Double _9w41ej9y =  default;
Double _1m44vtuk =  default;
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
		
		_h75qnr7l = _f43eg0w0("S" );
		_p1iqarg6 = _f43eg0w0("E" );
		_kiuz6tsq = __POW(_f43eg0w0("B" ), ILNumerics.F2NET.Intrinsics.INT((ILNumerics.F2NET.Intrinsics.LOG(_h75qnr7l / _p1iqarg6 ) / ILNumerics.F2NET.Intrinsics.LOG(_f43eg0w0("B" ) )) / _5m0mjfxm ));
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
					System.Int32 __81fgg2dlsvn182 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step182 = (System.Int32)((int)1);
					System.Int32 __81fgg2count182;
					for (__81fgg2count182 = System.Math.Max(0, (System.Int32)(((System.Int32)(_jfcumjho) - __81fgg2dlsvn182 + __81fgg2step182) / __81fgg2step182)), _b5p6od9s = __81fgg2dlsvn182; __81fgg2count182 != 0; __81fgg2count182--, _b5p6od9s += (__81fgg2step182)) {

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
					System.Int32 __81fgg2dlsvn183 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step183 = (System.Int32)((int)1);
					System.Int32 __81fgg2count183;
					for (__81fgg2count183 = System.Math.Max(0, (System.Int32)(((System.Int32)(_jfcumjho) - __81fgg2dlsvn183 + __81fgg2step183) / __81fgg2step183)), _b5p6od9s = __81fgg2dlsvn183; __81fgg2count183 != 0; __81fgg2count183--, _b5p6od9s += (__81fgg2step183)) {

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
		//*     End of DLARTG 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
