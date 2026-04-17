
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
//*> \brief \b ZLARTG generates a plane rotation with real cosine and complex sine. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZLARTG + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zlartg.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zlartg.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zlartg.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZLARTG( F, G, CS, SN, R ) 
//* 
//*       .. Scalar Arguments .. 
//*       DOUBLE PRECISION   CS 
//*       COMPLEX*16         F, G, R, SN 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZLARTG generates a plane rotation so that 
//*> 
//*>    [  CS  SN  ]     [ F ]     [ R ] 
//*>    [  __      ]  .  [   ]  =  [   ]   where CS**2 + |SN|**2 = 1. 
//*>    [ -SN  CS  ]     [ G ]     [ 0 ] 
//*> 
//*> This is a faster version of the BLAS1 routine ZROTG, except for 
//*> the following differences: 
//*>    F and G are unchanged on return. 
//*>    If G=0, then CS=1 and SN=0. 
//*>    If F=0, then CS=0 and SN is chosen so that R is real. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] F 
//*> \verbatim 
//*>          F is COMPLEX*16 
//*>          The first component of vector to be rotated. 
//*> \endverbatim 
//*> 
//*> \param[in] G 
//*> \verbatim 
//*>          G is COMPLEX*16 
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
//*>          SN is COMPLEX*16 
//*>          The sine of the rotation. 
//*> \endverbatim 
//*> 
//*> \param[out] R 
//*> \verbatim 
//*>          R is COMPLEX*16 
//*>          The nonzero component of the rotated vector. 
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
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  3-5-96 - Modified with a new algorithm by W. Kahan and J. Demmel 
//*> 
//*>  This version has a few statements commented out for thread safety 
//*>  (machine parameters are computed on each entry). 10 feb 03, SJH. 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _o54yscpo(ref complex _8plnuphw, ref complex _mu73se41, ref Double _82tpdhyl, ref complex _8tmd0ner, ref complex _q2vwp05i)
	{
#region variable declarations
Double _5m0mjfxm =  2d;
Double _kxg5drh2 =  1d;
Double _d0547bi2 =  0d;
complex _gdjumcqt =   new fcomplex(0f,0f);
Int32 _jfcumjho =  default;
Int32 _b5p6od9s =  default;
Double _plfm7z8g =  default;
Double _mvydvfo6 =  default;
Double _zilpsmzp =  default;
Double _p1iqarg6 =  default;
Double _a09b7doz =  default;
Double _1zjphdhf =  default;
Double _v4k86mue =  default;
Double _lwvd9nch =  default;
Double _h75qnr7l =  default;
Double _kiuz6tsq =  default;
Double _9w41ej9y =  default;
Double _1m44vtuk =  default;
complex _f87tutdj =  default;
complex _h8m658zw =  default;
complex _bf3yb1tx =  default;
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
		//*     .. Statement Functions .. 
		//*     .. 
		//*     .. Statement Function definitions .. 
		
		
		Func<complex,Double> _f5zlrkzn = (_wckifxvb) => { return ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.DBLE(_wckifxvb ) ) ,ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.DIMAG(_wckifxvb ) ) ); };;
		
		Func<complex,Double> _0b179yin = (_wckifxvb) => { return (__POW2(ILNumerics.F2NET.Intrinsics.DBLE(_wckifxvb )) + __POW2(ILNumerics.F2NET.Intrinsics.DIMAG(_wckifxvb ))); };;//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		_h75qnr7l = _f43eg0w0("S" );
		_p1iqarg6 = _f43eg0w0("E" );
		_kiuz6tsq = __POW(_f43eg0w0("B" ), ILNumerics.F2NET.Intrinsics.INT((ILNumerics.F2NET.Intrinsics.LOG(_h75qnr7l / _p1iqarg6 ) / ILNumerics.F2NET.Intrinsics.LOG(_f43eg0w0("B" ) )) / _5m0mjfxm ));
		_9w41ej9y = (_kxg5drh2 / _kiuz6tsq);
		_1m44vtuk = ILNumerics.F2NET.Intrinsics.MAX(_f5zlrkzn(_8plnuphw ) ,_f5zlrkzn(_mu73se41 ) );
		_h8m658zw = _8plnuphw;
		_bf3yb1tx = _mu73se41;
		_jfcumjho = (int)0;
		if (_1m44vtuk >= _9w41ej9y)
		{
			
Mark10:;
			// continue
			_jfcumjho = (_jfcumjho + (int)1);
			_h8m658zw = (_h8m658zw * _kiuz6tsq);
			_bf3yb1tx = (_bf3yb1tx * _kiuz6tsq);
			_1m44vtuk = (_1m44vtuk * _kiuz6tsq);
			if (_1m44vtuk >= _9w41ej9y)goto Mark10;
		}
		else
		if (_1m44vtuk <= _kiuz6tsq)
		{
			
			if ((_mu73se41 == _gdjumcqt) | _fk98jwhi(ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.ABS(_mu73se41 )) ))
			{
				
				_82tpdhyl = _kxg5drh2;
				_8tmd0ner = _gdjumcqt;
				_q2vwp05i = _8plnuphw;
				return;
			}
			
Mark20:;
			// continue
			_jfcumjho = (_jfcumjho - (int)1);
			_h8m658zw = (_h8m658zw * _9w41ej9y);
			_bf3yb1tx = (_bf3yb1tx * _9w41ej9y);
			_1m44vtuk = (_1m44vtuk * _9w41ej9y);
			if (_1m44vtuk <= _kiuz6tsq)goto Mark20;
		}
		
		_a09b7doz = _0b179yin(_h8m658zw );
		_v4k86mue = _0b179yin(_bf3yb1tx );
		if (_a09b7doz <= (ILNumerics.F2NET.Intrinsics.MAX(_v4k86mue ,_kxg5drh2 ) * _h75qnr7l))
		{
			//* 
			//*        This is a rare case: F is very small. 
			//* 
			
			if (_8plnuphw == _gdjumcqt)
			{
				
				_82tpdhyl = _d0547bi2;
				_q2vwp05i = DCMPLX(_1uc27645(ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.DBLE(_mu73se41 )) ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.DIMAG(_mu73se41 )) ));//*           Do complex/real division explicitly with two real divisions 
				
				_plfm7z8g = _1uc27645(ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.DBLE(_bf3yb1tx )) ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.DIMAG(_bf3yb1tx )) );
				_8tmd0ner = ILNumerics.F2NET.Intrinsics.DCMPLX(ILNumerics.F2NET.Intrinsics.DBLE(_bf3yb1tx ) / _plfm7z8g ,-((ILNumerics.F2NET.Intrinsics.DIMAG(_bf3yb1tx ) / _plfm7z8g)) );
				return;
			}
			
			_1zjphdhf = _1uc27645(ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.DBLE(_h8m658zw )) ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.DIMAG(_h8m658zw )) );//*        G2 and G2S are accurate 
			//*        G2 is at least SAFMIN, and G2S is at least SAFMN2 
			
			_lwvd9nch = ILNumerics.F2NET.Intrinsics.SQRT(_v4k86mue );//*        Error in CS from underflow in F2S is at most 
			//*        UNFL / SAFMN2 .lt. sqrt(UNFL*EPS) .lt. EPS 
			//*        If MAX(G2,ONE)=G2, then F2 .lt. G2*SAFMIN, 
			//*        and so CS .lt. sqrt(SAFMIN) 
			//*        If MAX(G2,ONE)=ONE, then F2 .lt. SAFMIN 
			//*        and so CS .lt. sqrt(SAFMIN)/SAFMN2 = sqrt(EPS) 
			//*        Therefore, CS = F2S/G2S / sqrt( 1 + (F2S/G2S)**2 ) = F2S/G2S 
			
			_82tpdhyl = (_1zjphdhf / _lwvd9nch);//*        Make sure abs(FF) = 1 
			//*        Do complex/real division explicitly with 2 real divisions 
			
			if (_f5zlrkzn(_8plnuphw ) > _kxg5drh2)
			{
				
				_plfm7z8g = _1uc27645(ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.DBLE(_8plnuphw )) ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.DIMAG(_8plnuphw )) );
				_f87tutdj = ILNumerics.F2NET.Intrinsics.DCMPLX(ILNumerics.F2NET.Intrinsics.DBLE(_8plnuphw ) / _plfm7z8g ,ILNumerics.F2NET.Intrinsics.DIMAG(_8plnuphw ) / _plfm7z8g );
			}
			else
			{
				
				_zilpsmzp = (_9w41ej9y * ILNumerics.F2NET.Intrinsics.DBLE(_8plnuphw ));
				_mvydvfo6 = (_9w41ej9y * ILNumerics.F2NET.Intrinsics.DIMAG(_8plnuphw ));
				_plfm7z8g = _1uc27645(ref _zilpsmzp ,ref _mvydvfo6 );
				_f87tutdj = ILNumerics.F2NET.Intrinsics.DCMPLX(_zilpsmzp / _plfm7z8g ,_mvydvfo6 / _plfm7z8g );
			}
			
			_8tmd0ner = (_f87tutdj * ILNumerics.F2NET.Intrinsics.DCMPLX(ILNumerics.F2NET.Intrinsics.DBLE(_bf3yb1tx ) / _lwvd9nch ,-((ILNumerics.F2NET.Intrinsics.DIMAG(_bf3yb1tx ) / _lwvd9nch)) ));
			_q2vwp05i = ((_82tpdhyl * _8plnuphw) + (_8tmd0ner * _mu73se41));
		}
		else
		{
			//* 
			//*        This is the most common case. 
			//*        Neither F2 nor F2/G2 are less than SAFMIN 
			//*        F2S cannot overflow, and it is accurate 
			//* 
			
			_1zjphdhf = ILNumerics.F2NET.Intrinsics.SQRT(_kxg5drh2 + (_v4k86mue / _a09b7doz) );//*        Do the F2S(real)*FS(complex) multiply with two real multiplies 
			
			_q2vwp05i = ILNumerics.F2NET.Intrinsics.DCMPLX(_1zjphdhf * ILNumerics.F2NET.Intrinsics.DBLE(_h8m658zw ) ,_1zjphdhf * ILNumerics.F2NET.Intrinsics.DIMAG(_h8m658zw ) );
			_82tpdhyl = (_kxg5drh2 / _1zjphdhf);
			_plfm7z8g = (_a09b7doz + _v4k86mue);//*        Do complex/real division explicitly with two real divisions 
			
			_8tmd0ner = ILNumerics.F2NET.Intrinsics.DCMPLX(ILNumerics.F2NET.Intrinsics.DBLE(_q2vwp05i ) / _plfm7z8g ,ILNumerics.F2NET.Intrinsics.DIMAG(_q2vwp05i ) / _plfm7z8g );
			_8tmd0ner = (_8tmd0ner * ILNumerics.F2NET.Intrinsics.DCONJG(_bf3yb1tx ));
			if (_jfcumjho != (int)0)
			{
				
				if (_jfcumjho > (int)0)
				{
					
					{
						System.Int32 __81fgg2dlsvn2713 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2713 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2713;
						for (__81fgg2count2713 = System.Math.Max(0, (System.Int32)(((System.Int32)(_jfcumjho) - __81fgg2dlsvn2713 + __81fgg2step2713) / __81fgg2step2713)), _b5p6od9s = __81fgg2dlsvn2713; __81fgg2count2713 != 0; __81fgg2count2713--, _b5p6od9s += (__81fgg2step2713)) {

						{
							
							_q2vwp05i = (_q2vwp05i * _9w41ej9y);
Mark30:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn2714 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2714 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2714;
						for (__81fgg2count2714 = System.Math.Max(0, (System.Int32)(((System.Int32)(-(_jfcumjho)) - __81fgg2dlsvn2714 + __81fgg2step2714) / __81fgg2step2714)), _b5p6od9s = __81fgg2dlsvn2714; __81fgg2count2714 != 0; __81fgg2count2714--, _b5p6od9s += (__81fgg2step2714)) {

						{
							
							_q2vwp05i = (_q2vwp05i * _kiuz6tsq);
Mark40:;
							// continue
						}
												}					}
				}
				
			}
			
		}
		
		return;//* 
		//*     End of ZLARTG 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
