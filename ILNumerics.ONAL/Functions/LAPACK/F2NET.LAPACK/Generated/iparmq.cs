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
//*> \brief \b IPARMQ 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download IPARMQ + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/iparmq.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/iparmq.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/iparmq.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       INTEGER FUNCTION IPARMQ( ISPEC, NAME, OPTS, N, ILO, IHI, LWORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            IHI, ILO, ISPEC, LWORK, N 
//*       CHARACTER          NAME*( * ), OPTS*( * ) 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*>      This program sets problem and machine dependent parameters 
//*>      useful for xHSEQR and related subroutines for eigenvalue 
//*>      problems. It is called whenever 
//*>      IPARMQ is called with 12 <= ISPEC <= 16 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] ISPEC 
//*> \verbatim 
//*>          ISPEC is INTEGER 
//*>              ISPEC specifies which tunable parameter IPARMQ should 
//*>              return. 
//*> 
//*>              ISPEC=12: (INMIN)  Matrices of order nmin or less 
//*>                        are sent directly to xLAHQR, the implicit 
//*>                        double shift QR algorithm.  NMIN must be 
//*>                        at least 11. 
//*> 
//*>              ISPEC=13: (INWIN)  Size of the deflation window. 
//*>                        This is best set greater than or equal to 
//*>                        the number of simultaneous shifts NS. 
//*>                        Larger matrices benefit from larger deflation 
//*>                        windows. 
//*> 
//*>              ISPEC=14: (INIBL) Determines when to stop nibbling and 
//*>                        invest in an (expensive) multi-shift QR sweep. 
//*>                        If the aggressive early deflation subroutine 
//*>                        finds LD converged eigenvalues from an order 
//*>                        NW deflation window and LD > (NW*NIBBLE)/100, 
//*>                        then the next QR sweep is skipped and early 
//*>                        deflation is applied immediately to the 
//*>                        remaining active diagonal block.  Setting 
//*>                        IPARMQ(ISPEC=14) = 0 causes TTQRE to skip a 
//*>                        multi-shift QR sweep whenever early deflation 
//*>                        finds a converged eigenvalue.  Setting 
//*>                        IPARMQ(ISPEC=14) greater than or equal to 100 
//*>                        prevents TTQRE from skipping a multi-shift 
//*>                        QR sweep. 
//*> 
//*>              ISPEC=15: (NSHFTS) The number of simultaneous shifts in 
//*>                        a multi-shift QR iteration. 
//*> 
//*>              ISPEC=16: (IACC22) IPARMQ is set to 0, 1 or 2 with the 
//*>                        following meanings. 
//*>                        0:  During the multi-shift QR/QZ sweep, 
//*>                            blocked eigenvalue reordering, blocked 
//*>                            Hessenberg-triangular reduction, 
//*>                            reflections and/or rotations are not 
//*>                            accumulated when updating the 
//*>                            far-from-diagonal matrix entries. 
//*>                        1:  During the multi-shift QR/QZ sweep, 
//*>                            blocked eigenvalue reordering, blocked 
//*>                            Hessenberg-triangular reduction, 
//*>                            reflections and/or rotations are 
//*>                            accumulated, and matrix-matrix 
//*>                            multiplication is used to update the 
//*>                            far-from-diagonal matrix entries. 
//*>                        2:  During the multi-shift QR/QZ sweep, 
//*>                            blocked eigenvalue reordering, blocked 
//*>                            Hessenberg-triangular reduction, 
//*>                            reflections and/or rotations are 
//*>                            accumulated, and 2-by-2 block structure 
//*>                            is exploited during matrix-matrix 
//*>                            multiplies. 
//*>                        (If xTRMM is slower than xGEMM, then 
//*>                        IPARMQ(ISPEC=16)=1 may be more efficient than 
//*>                        IPARMQ(ISPEC=16)=2 despite the greater level of 
//*>                        arithmetic work implied by the latter choice.) 
//*> \endverbatim 
//*> 
//*> \param[in] NAME 
//*> \verbatim 
//*>          NAME is CHARACTER string 
//*>               Name of the calling subroutine 
//*> \endverbatim 
//*> 
//*> \param[in] OPTS 
//*> \verbatim 
//*>          OPTS is CHARACTER string 
//*>               This is a concatenation of the string arguments to 
//*>               TTQRE. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>               N is the order of the Hessenberg matrix H. 
//*> \endverbatim 
//*> 
//*> \param[in] ILO 
//*> \verbatim 
//*>          ILO is INTEGER 
//*> \endverbatim 
//*> 
//*> \param[in] IHI 
//*> \verbatim 
//*>          IHI is INTEGER 
//*>               It is assumed that H is already upper triangular 
//*>               in rows and columns 1:ILO-1 and IHI+1:N. 
//*> \endverbatim 
//*> 
//*> \param[in] LWORK 
//*> \verbatim 
//*>          LWORK is INTEGER 
//*>               The amount of workspace available. 
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
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>       Little is known about how best to choose these parameters. 
//*>       It is possible to use different values of the parameters 
//*>       for each of CHSEQR, DHSEQR, SHSEQR and ZHSEQR. 
//*> 
//*>       It is probably best to choose different parameters for 
//*>       different matrices and different parameters at different 
//*>       times during the iteration, but this has not been 
//*>       implemented --- yet. 
//*> 
//*> 
//*>       The best choices of most of the parameters depend 
//*>       in an ill-understood way on the relative execution 
//*>       rate of xLAQR3 and xLAQR5 and on the nature of each 
//*>       particular eigenvalue problem.  Experiment may be the 
//*>       only practical way to determine which choices are most 
//*>       effective. 
//*> 
//*>       Following is a list of default values supplied by IPARMQ. 
//*>       These defaults may be adjusted in order to attain better 
//*>       performance in any particular computational environment. 
//*> 
//*>       IPARMQ(ISPEC=12) The xLAHQR vs xLAQR0 crossover point. 
//*>                        Default: 75. (Must be at least 11.) 
//*> 
//*>       IPARMQ(ISPEC=13) Recommended deflation window size. 
//*>                        This depends on ILO, IHI and NS, the 
//*>                        number of simultaneous shifts returned 
//*>                        by IPARMQ(ISPEC=15).  The default for 
//*>                        (IHI-ILO+1) <= 500 is NS.  The default 
//*>                        for (IHI-ILO+1) > 500 is 3*NS/2. 
//*> 
//*>       IPARMQ(ISPEC=14) Nibble crossover point.  Default: 14. 
//*> 
//*>       IPARMQ(ISPEC=15) Number of simultaneous shifts, NS. 
//*>                        a multi-shift QR iteration. 
//*> 
//*>                        If IHI-ILO+1 is ... 
//*> 
//*>                        greater than      ...but less    ... the 
//*>                        or equal to ...      than        default is 
//*> 
//*>                                0               30       NS =   2+ 
//*>                               30               60       NS =   4+ 
//*>                               60              150       NS =  10 
//*>                              150              590       NS =  ** 
//*>                              590             3000       NS =  64 
//*>                             3000             6000       NS = 128 
//*>                             6000             infinity   NS = 256 
//*> 
//*>                    (+)  By default matrices of this order are 
//*>                         passed to the implicit double shift routine 
//*>                         xLAHQR.  See IPARMQ(ISPEC=12) above.   These 
//*>                         values of NS are used only in case of a rare 
//*>                         xLAHQR failure. 
//*> 
//*>                    (**) The asterisks (**) indicate an ad-hoc 
//*>                         function increasing from 10 to 64. 
//*> 
//*>       IPARMQ(ISPEC=16) Select structured matrix multiply. 
//*>                        (See ISPEC=16 above for details.) 
//*>                        Default: 3. 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static Int32 _scrthew8(ref Int32 _r22uqjla, FString _hr3apt47, FString _c7e3jn3t, ref Int32 _dxpq0xkr, ref Int32 _pew3blan, ref Int32 _9c1csucx, ref Int32 _6fnxzlyp)
	{
#region variable declarations
Int32 _scrthew8 = default;
Int32 _4mt4rndh =  (int)12;
Int32 _v05hpqyb =  (int)13;
Int32 _fd75paqo =  (int)14;
Int32 _jnt2fyhq =  (int)15;
Int32 _0ks4abp5 =  (int)16;
Int32 _rs56fkjq =  (int)75;
Int32 _rmsvn393 =  (int)14;
Int32 _6eue3lc7 =  (int)14;
Int32 _spqe84bd =  (int)14;
Int32 _yco7eui8 =  (int)500;
Single _5m0mjfxm =  2f;
Int32 _aym8a085 =  default;
Int32 _edl2gwc7 =  default;
Int32 _b5p6od9s =  default;
Int32 _8jzcrkri =  default;
Int32 _qbqg6u98 =  default;
FString _jhl0dtr8 =  new FString(6);
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
		//* 
		//*  ================================================================ 
		//*     .. Parameters .. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		
		if (((_r22uqjla == _jnt2fyhq) | (_r22uqjla == _v05hpqyb)) | (_r22uqjla == _0ks4abp5))
		{
			//* 
			//*        ==== Set the number simultaneous shifts ==== 
			//* 
			
			_aym8a085 = ((_9c1csucx - _pew3blan) + (int)1);
			_edl2gwc7 = (int)2;
			if (_aym8a085 >= (int)30)
			_edl2gwc7 = (int)4;
			if (_aym8a085 >= (int)60)
			_edl2gwc7 = (int)10;
			if (_aym8a085 >= (int)150)
			_edl2gwc7 = ILNumerics.F2NET.Intrinsics.MAX((int)10 ,_aym8a085 / ILNumerics.F2NET.Intrinsics.NINT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.REAL(_aym8a085 ) ) / ILNumerics.F2NET.Intrinsics.LOG(_5m0mjfxm ) ) );
			if (_aym8a085 >= (int)590)
			_edl2gwc7 = (int)64;
			if (_aym8a085 >= (int)3000)
			_edl2gwc7 = (int)128;
			if (_aym8a085 >= (int)6000)
			_edl2gwc7 = (int)256;
			_edl2gwc7 = ILNumerics.F2NET.Intrinsics.MAX((int)2 ,_edl2gwc7 - ILNumerics.F2NET.Intrinsics.MOD(_edl2gwc7 ,(int)2 ) );
		}
		//* 
		
		if (_r22uqjla == _4mt4rndh)
		{
			//* 
			//* 
			//*        ===== Matrices of order smaller than NMIN get sent 
			//*        .     to xLAHQR, the classic double shift algorithm. 
			//*        .     This must be at least 11. ==== 
			//* 
			
			_scrthew8 = _rs56fkjq;//* 
			
		}
		else
		if (_r22uqjla == _fd75paqo)
		{
			//* 
			//*        ==== INIBL: skip a multi-shift qr iteration and 
			//*        .    whenever aggressive early deflation finds 
			//*        .    at least (NIBBLE*(window size)/100) deflations. ==== 
			//* 
			
			_scrthew8 = _spqe84bd;//* 
			
		}
		else
		if (_r22uqjla == _jnt2fyhq)
		{
			//* 
			//*        ==== NSHFTS: The number of simultaneous shifts ===== 
			//* 
			
			_scrthew8 = _edl2gwc7;//* 
			
		}
		else
		if (_r22uqjla == _v05hpqyb)
		{
			//* 
			//*        ==== NW: deflation window size.  ==== 
			//* 
			
			if (_aym8a085 <= _yco7eui8)
			{
				
				_scrthew8 = _edl2gwc7;
			}
			else
			{
				
				_scrthew8 = (((int)3 * _edl2gwc7) / (int)2);
			}
			//* 
			
		}
		else
		if (_r22uqjla == _0ks4abp5)
		{
			//* 
			//*        ==== IACC22: Whether to accumulate reflections 
			//*        .     before updating the far-from-diagonal elements 
			//*        .     and whether to use 2-by-2 block structure while 
			//*        .     doing it.  A small amount of work could be saved 
			//*        .     by making this choice dependent also upon the 
			//*        .     NH=IHI-ILO+1. 
			//* 
			//* 
			//*        Convert NAME to upper case if the first character is lower case. 
			//* 
			
			_scrthew8 = (int)0;
			
			_jhl0dtr8 = (_hr3apt47).AssignTo(6);
			_8jzcrkri = ILNumerics.F2NET.Intrinsics.ICHAR(_jhl0dtr8[(int)1,(int)1] );
			_qbqg6u98 = ILNumerics.F2NET.Intrinsics.ICHAR("Z" );
			if ((_qbqg6u98 == (int)90) | (_qbqg6u98 == (int)122))
			{
				//* 
				//*           ASCII character set 
				//* 
				
				if ((_8jzcrkri >= (int)97) & (_8jzcrkri <= (int)122))
				{
					
					
					_jhl0dtr8[(int)1,(int)1] = ILNumerics.F2NET.Intrinsics.CHAR(_8jzcrkri - (int)32 );
					{
						System.Int32 __81fgg2dlsvn47 = (System.Int32)((int)2);
						const System.Int32 __81fgg2step47 = (System.Int32)((int)1);
						System.Int32 __81fgg2count47;
						for (__81fgg2count47 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)6) - __81fgg2dlsvn47 + __81fgg2step47) / __81fgg2step47)), _b5p6od9s = __81fgg2dlsvn47; __81fgg2count47 != 0; __81fgg2count47--, _b5p6od9s += (__81fgg2step47)) {

						{
							
							_8jzcrkri = ILNumerics.F2NET.Intrinsics.ICHAR(_jhl0dtr8[_b5p6od9s,_b5p6od9s] );
							if ((_8jzcrkri >= (int)97) & (_8jzcrkri <= (int)122))
							
							_jhl0dtr8[_b5p6od9s,_b5p6od9s] = ILNumerics.F2NET.Intrinsics.CHAR(_8jzcrkri - (int)32 );
						}
												}					}
				}
				//* 
				
			}
			else
			if ((_qbqg6u98 == (int)233) | (_qbqg6u98 == (int)169))
			{
				//* 
				//*           EBCDIC character set 
				//* 
				
				if ((((_8jzcrkri >= (int)129) & (_8jzcrkri <= (int)137)) | ((_8jzcrkri >= (int)145) & (_8jzcrkri <= (int)153))) | ((_8jzcrkri >= (int)162) & (_8jzcrkri <= (int)169)))
				{
					
					
					_jhl0dtr8[(int)1,(int)1] = ILNumerics.F2NET.Intrinsics.CHAR(_8jzcrkri + (int)64 );
					{
						System.Int32 __81fgg2dlsvn48 = (System.Int32)((int)2);
						const System.Int32 __81fgg2step48 = (System.Int32)((int)1);
						System.Int32 __81fgg2count48;
						for (__81fgg2count48 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)6) - __81fgg2dlsvn48 + __81fgg2step48) / __81fgg2step48)), _b5p6od9s = __81fgg2dlsvn48; __81fgg2count48 != 0; __81fgg2count48--, _b5p6od9s += (__81fgg2step48)) {

						{
							
							_8jzcrkri = ILNumerics.F2NET.Intrinsics.ICHAR(_jhl0dtr8[_b5p6od9s,_b5p6od9s] );
							if ((((_8jzcrkri >= (int)129) & (_8jzcrkri <= (int)137)) | ((_8jzcrkri >= (int)145) & (_8jzcrkri <= (int)153))) | ((_8jzcrkri >= (int)162) & (_8jzcrkri <= (int)169)))
							
							_jhl0dtr8[_b5p6od9s,_b5p6od9s] = ILNumerics.F2NET.Intrinsics.CHAR(_8jzcrkri + (int)64 );
						}
												}					}
				}
				//* 
				
			}
			else
			if ((_qbqg6u98 == (int)218) | (_qbqg6u98 == (int)250))
			{
				//* 
				//*           Prime machines:  ASCII+128 
				//* 
				
				if ((_8jzcrkri >= (int)225) & (_8jzcrkri <= (int)250))
				{
					
					
					_jhl0dtr8[(int)1,(int)1] = ILNumerics.F2NET.Intrinsics.CHAR(_8jzcrkri - (int)32 );
					{
						System.Int32 __81fgg2dlsvn49 = (System.Int32)((int)2);
						const System.Int32 __81fgg2step49 = (System.Int32)((int)1);
						System.Int32 __81fgg2count49;
						for (__81fgg2count49 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)6) - __81fgg2dlsvn49 + __81fgg2step49) / __81fgg2step49)), _b5p6od9s = __81fgg2dlsvn49; __81fgg2count49 != 0; __81fgg2count49--, _b5p6od9s += (__81fgg2step49)) {

						{
							
							_8jzcrkri = ILNumerics.F2NET.Intrinsics.ICHAR(_jhl0dtr8[_b5p6od9s,_b5p6od9s] );
							if ((_8jzcrkri >= (int)225) & (_8jzcrkri <= (int)250))
							
							_jhl0dtr8[_b5p6od9s,_b5p6od9s] = ILNumerics.F2NET.Intrinsics.CHAR(_8jzcrkri - (int)32 );
						}
												}					}
				}
				
			}
			//* 
			
			if ((_jhl0dtr8[(int)2,(int)6] == "GGHRD") | (_jhl0dtr8[(int)2,(int)6] == "GGHD3"))
			{
				
				_scrthew8 = (int)1;
				if (_aym8a085 >= _rmsvn393)
				_scrthew8 = (int)2;
			}
			else
			if (_jhl0dtr8[(int)4,(int)6] == "EXC")
			{
				
				if (_aym8a085 >= _6eue3lc7)
				_scrthew8 = (int)1;
				if (_aym8a085 >= _rmsvn393)
				_scrthew8 = (int)2;
			}
			else
			if ((_jhl0dtr8[(int)2,(int)6] == "HSEQR") | (_jhl0dtr8[(int)2,(int)5] == "LAQR"))
			{
				
				if (_edl2gwc7 >= _6eue3lc7)
				_scrthew8 = (int)1;
				if (_edl2gwc7 >= _rmsvn393)
				_scrthew8 = (int)2;
			}
			//* 
			
		}
		else
		{
			//*        ===== invalid value of ispec ===== 
			
			_scrthew8 = (int)-1;//* 
			
		}
		//* 
		//*     ==== End of IPARMQ ==== 
		//* 
		
	}
	
	return _scrthew8;
	} // 177

} // end class 
} // end namespace
#endif
