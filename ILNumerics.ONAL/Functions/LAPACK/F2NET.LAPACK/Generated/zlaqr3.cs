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
//*> \brief \b ZLAQR3 performs the unitary similarity transformation of a Hessenberg matrix to detect and deflate fully converged eigenvalues from a trailing principal submatrix (aggressive early deflation). 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZLAQR3 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zlaqr3.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zlaqr3.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zlaqr3.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZLAQR3( WANTT, WANTZ, N, KTOP, KBOT, NW, H, LDH, ILOZ, 
//*                          IHIZ, Z, LDZ, NS, ND, SH, V, LDV, NH, T, LDT, 
//*                          NV, WV, LDWV, WORK, LWORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            IHIZ, ILOZ, KBOT, KTOP, LDH, LDT, LDV, LDWV, 
//*      $                   LDZ, LWORK, N, ND, NH, NS, NV, NW 
//*       LOGICAL            WANTT, WANTZ 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX*16         H( LDH, * ), SH( * ), T( LDT, * ), V( LDV, * ), 
//*      $                   WORK( * ), WV( LDWV, * ), Z( LDZ, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*>    Aggressive early deflation: 
//*> 
//*>    ZLAQR3 accepts as input an upper Hessenberg matrix 
//*>    H and performs an unitary similarity transformation 
//*>    designed to detect and deflate fully converged eigenvalues from 
//*>    a trailing principal submatrix.  On output H has been over- 
//*>    written by a new Hessenberg matrix that is a perturbation of 
//*>    an unitary similarity transformation of H.  It is to be 
//*>    hoped that the final version of H has many zero subdiagonal 
//*>    entries. 
//*> 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] WANTT 
//*> \verbatim 
//*>          WANTT is LOGICAL 
//*>          If .TRUE., then the Hessenberg matrix H is fully updated 
//*>          so that the triangular Schur factor may be 
//*>          computed (in cooperation with the calling subroutine). 
//*>          If .FALSE., then only enough of H is updated to preserve 
//*>          the eigenvalues. 
//*> \endverbatim 
//*> 
//*> \param[in] WANTZ 
//*> \verbatim 
//*>          WANTZ is LOGICAL 
//*>          If .TRUE., then the unitary matrix Z is updated so 
//*>          so that the unitary Schur factor may be computed 
//*>          (in cooperation with the calling subroutine). 
//*>          If .FALSE., then Z is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix H and (if WANTZ is .TRUE.) the 
//*>          order of the unitary matrix Z. 
//*> \endverbatim 
//*> 
//*> \param[in] KTOP 
//*> \verbatim 
//*>          KTOP is INTEGER 
//*>          It is assumed that either KTOP = 1 or H(KTOP,KTOP-1)=0. 
//*>          KBOT and KTOP together determine an isolated block 
//*>          along the diagonal of the Hessenberg matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] KBOT 
//*> \verbatim 
//*>          KBOT is INTEGER 
//*>          It is assumed without a check that either 
//*>          KBOT = N or H(KBOT+1,KBOT)=0.  KBOT and KTOP together 
//*>          determine an isolated block along the diagonal of the 
//*>          Hessenberg matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] NW 
//*> \verbatim 
//*>          NW is INTEGER 
//*>          Deflation window size.  1 <= NW <= (KBOT-KTOP+1). 
//*> \endverbatim 
//*> 
//*> \param[in,out] H 
//*> \verbatim 
//*>          H is COMPLEX*16 array, dimension (LDH,N) 
//*>          On input the initial N-by-N section of H stores the 
//*>          Hessenberg matrix undergoing aggressive early deflation. 
//*>          On output H has been transformed by a unitary 
//*>          similarity transformation, perturbed, and the returned 
//*>          to Hessenberg form that (it is to be hoped) has some 
//*>          zero subdiagonal entries. 
//*> \endverbatim 
//*> 
//*> \param[in] LDH 
//*> \verbatim 
//*>          LDH is INTEGER 
//*>          Leading dimension of H just as declared in the calling 
//*>          subroutine.  N <= LDH 
//*> \endverbatim 
//*> 
//*> \param[in] ILOZ 
//*> \verbatim 
//*>          ILOZ is INTEGER 
//*> \endverbatim 
//*> 
//*> \param[in] IHIZ 
//*> \verbatim 
//*>          IHIZ is INTEGER 
//*>          Specify the rows of Z to which transformations must be 
//*>          applied if WANTZ is .TRUE.. 1 <= ILOZ <= IHIZ <= N. 
//*> \endverbatim 
//*> 
//*> \param[in,out] Z 
//*> \verbatim 
//*>          Z is COMPLEX*16 array, dimension (LDZ,N) 
//*>          IF WANTZ is .TRUE., then on output, the unitary 
//*>          similarity transformation mentioned above has been 
//*>          accumulated into Z(ILOZ:IHIZ,ILOZ:IHIZ) from the right. 
//*>          If WANTZ is .FALSE., then Z is unreferenced. 
//*> \endverbatim 
//*> 
//*> \param[in] LDZ 
//*> \verbatim 
//*>          LDZ is INTEGER 
//*>          The leading dimension of Z just as declared in the 
//*>          calling subroutine.  1 <= LDZ. 
//*> \endverbatim 
//*> 
//*> \param[out] NS 
//*> \verbatim 
//*>          NS is INTEGER 
//*>          The number of unconverged (ie approximate) eigenvalues 
//*>          returned in SR and SI that may be used as shifts by the 
//*>          calling subroutine. 
//*> \endverbatim 
//*> 
//*> \param[out] ND 
//*> \verbatim 
//*>          ND is INTEGER 
//*>          The number of converged eigenvalues uncovered by this 
//*>          subroutine. 
//*> \endverbatim 
//*> 
//*> \param[out] SH 
//*> \verbatim 
//*>          SH is COMPLEX*16 array, dimension (KBOT) 
//*>          On output, approximate eigenvalues that may 
//*>          be used for shifts are stored in SH(KBOT-ND-NS+1) 
//*>          through SR(KBOT-ND).  Converged eigenvalues are 
//*>          stored in SH(KBOT-ND+1) through SH(KBOT). 
//*> \endverbatim 
//*> 
//*> \param[out] V 
//*> \verbatim 
//*>          V is COMPLEX*16 array, dimension (LDV,NW) 
//*>          An NW-by-NW work array. 
//*> \endverbatim 
//*> 
//*> \param[in] LDV 
//*> \verbatim 
//*>          LDV is INTEGER 
//*>          The leading dimension of V just as declared in the 
//*>          calling subroutine.  NW <= LDV 
//*> \endverbatim 
//*> 
//*> \param[in] NH 
//*> \verbatim 
//*>          NH is INTEGER 
//*>          The number of columns of T.  NH >= NW. 
//*> \endverbatim 
//*> 
//*> \param[out] T 
//*> \verbatim 
//*>          T is COMPLEX*16 array, dimension (LDT,NW) 
//*> \endverbatim 
//*> 
//*> \param[in] LDT 
//*> \verbatim 
//*>          LDT is INTEGER 
//*>          The leading dimension of T just as declared in the 
//*>          calling subroutine.  NW <= LDT 
//*> \endverbatim 
//*> 
//*> \param[in] NV 
//*> \verbatim 
//*>          NV is INTEGER 
//*>          The number of rows of work array WV available for 
//*>          workspace.  NV >= NW. 
//*> \endverbatim 
//*> 
//*> \param[out] WV 
//*> \verbatim 
//*>          WV is COMPLEX*16 array, dimension (LDWV,NW) 
//*> \endverbatim 
//*> 
//*> \param[in] LDWV 
//*> \verbatim 
//*>          LDWV is INTEGER 
//*>          The leading dimension of W just as declared in the 
//*>          calling subroutine.  NW <= LDV 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is COMPLEX*16 array, dimension (LWORK) 
//*>          On exit, WORK(1) is set to an estimate of the optimal value 
//*>          of LWORK for the given values of N, NW, KTOP and KBOT. 
//*> \endverbatim 
//*> 
//*> \param[in] LWORK 
//*> \verbatim 
//*>          LWORK is INTEGER 
//*>          The dimension of the work array WORK.  LWORK = 2*NW 
//*>          suffices, but greater efficiency may result from larger 
//*>          values of LWORK. 
//*> 
//*>          If LWORK = -1, then a workspace query is assumed; ZLAQR3 
//*>          only estimates the optimal workspace size for the given 
//*>          values of N, NW, KTOP and KBOT.  The estimate is returned 
//*>          in WORK(1).  No error message related to LWORK is issued 
//*>          by XERBLA.  Neither H nor Z are accessed. 
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
//*> \ingroup complex16OTHERauxiliary 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>       Karen Braman and Ralph Byers, Department of Mathematics, 
//*>       University of Kansas, USA 
//*> 
//*  ===================================================================== 

	 
	public static void _pc0tk08k(ref Boolean _amt8y1zm, ref Boolean _189gzykk, ref Int32 _dxpq0xkr, ref Int32 _4a77vvpa, ref Int32 _7d8gh478, ref Int32 _w6pmxgch, complex* _ogkjl6gu, ref Int32 _1iekxpnw, ref Int32 _pinc1ofz, ref Int32 _mg9v9w4h, complex* _7e60fcso, ref Int32 _5l1tna8s, ref Int32 _edl2gwc7, ref Int32 _rwm6akyl, complex* _9tenoh4m, complex* _ycxba85s, ref Int32 _ys09rxze, ref Int32 _aym8a085, complex* _2ivtt43r, ref Int32 _w8yhbr2r, ref Int32 _aiz0v1d1, complex* _pm308b4n, ref Int32 _vlfzpxiy, complex* _apig8meb, ref Int32 _6fnxzlyp)
	{
#region variable declarations
complex _d0547bi2 =   new fcomplex(0f,0f);
complex _kxg5drh2 =   new fcomplex(1f,0f);
Double _7cb2gzzt =  0d;
Double _zfxjga59 =  1d;
complex _bafcbx97 =  default;
complex _n7plx4io =  default;
complex _irk8i6qr =  default;
complex _0446f4de =  default;
Double _yl9tlixq =  default;
Double _odf6ja0t =  default;
Double _h75qnr7l =  default;
Double _bogm0gwy =  default;
Double _0h4yb5wu =  default;
Int32 _b5p6od9s =  default;
Int32 _la6t805m =  default;
Int32 _ab05c09e =  default;
Int32 _gro5yvfo =  default;
Int32 _jhtbcevs =  default;
Int32 _znpjgsef =  default;
Int32 _msaexyn2 =  default;
Int32 _chb1oswp =  default;
Int32 _m4jdywnh =  default;
Int32 _hcqqzxmr =  default;
Int32 _pnhq01yl =  default;
Int32 _87d05vk3 =  default;
Int32 _y834yh01 =  default;
Int32 _ve9qn4fh =  default;
Int32 _7c13pjdp =  default;
Int32 _44malgzk =  default;
Int32 _e4ueamrn =  default;
Int32 _rs56fkjq =  default;
string fLanavab = default;
#endregion  variable declarations

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
		//*  ================================================================ 
		//* 
		//*     .. Parameters .. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Statement Functions .. 
		//*     .. 
		//*     .. Statement Function definitions .. 
		
		
		Func<complex,Double> _4jqx89by = (_a94616nn) => { return (ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.DBLE(_a94616nn ) ) + ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.DIMAG(_a94616nn ) )); };;//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     ==== Estimate optimal workspace. ==== 
		//* 
		
		_msaexyn2 = ILNumerics.F2NET.Intrinsics.MIN(_w6pmxgch ,(_7d8gh478 - _4a77vvpa) + (int)1 );
		if (_msaexyn2 <= (int)2)
		{
			
			_e4ueamrn = (int)1;
		}
		else
		{
			//* 
			//*        ==== Workspace query call to ZGEHRD ==== 
			//* 
			
			_j69fohs3(ref _msaexyn2 ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_msaexyn2 - (int)1) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,_apig8meb ,ref Unsafe.AsRef((int)-1) ,ref _gro5yvfo );
			_ve9qn4fh = ILNumerics.F2NET.Intrinsics.INT(*(_apig8meb+((int)1 - 1)) );//* 
			//*        ==== Workspace query call to ZUNMHR ==== 
			//* 
			
			_ml2zcgc8("R" ,"N" ,ref _msaexyn2 ,ref _msaexyn2 ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_msaexyn2 - (int)1) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref Unsafe.AsRef((int)-1) ,ref _gro5yvfo );
			_7c13pjdp = ILNumerics.F2NET.Intrinsics.INT(*(_apig8meb+((int)1 - 1)) );//* 
			//*        ==== Workspace query call to ZLAQR4 ==== 
			//* 
			
			_o05elgn6(ref Unsafe.AsRef(true) ,ref Unsafe.AsRef(true) ,ref _msaexyn2 ,ref Unsafe.AsRef((int)1) ,ref _msaexyn2 ,_2ivtt43r ,ref _w8yhbr2r ,_9tenoh4m ,ref Unsafe.AsRef((int)1) ,ref _msaexyn2 ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref Unsafe.AsRef((int)-1) ,ref _jhtbcevs );
			_44malgzk = ILNumerics.F2NET.Intrinsics.INT(*(_apig8meb+((int)1 - 1)) );//* 
			//*        ==== Optimal workspace ==== 
			//* 
			
			_e4ueamrn = ILNumerics.F2NET.Intrinsics.MAX(_msaexyn2 + ILNumerics.F2NET.Intrinsics.MAX(_ve9qn4fh ,_7c13pjdp ) ,_44malgzk );
		}
		//* 
		//*     ==== Quick return in case of workspace query. ==== 
		//* 
		
		if (_6fnxzlyp == (int)-1)
		{
			
			*(_apig8meb+((int)1 - 1)) = ILNumerics.F2NET.Intrinsics.DCMPLX(_e4ueamrn ,(int)0 );
			return;
		}
		//* 
		//*     ==== Nothing to do ... 
		//*     ... for an empty active block ... ==== 
		
		_edl2gwc7 = (int)0;
		_rwm6akyl = (int)0;
		*(_apig8meb+((int)1 - 1)) = _kxg5drh2;
		if (_4a77vvpa > _7d8gh478)
		return;//*     ... nor for an empty deflation window. ==== 
		
		if (_w6pmxgch < (int)1)
		return;//* 
		//*     ==== Machine constants ==== 
		//* 
		
		_h75qnr7l = _f43eg0w0("SAFE MINIMUM" );
		_odf6ja0t = (_zfxjga59 / _h75qnr7l);
		_to4dtyqc(ref _h75qnr7l ,ref _odf6ja0t );
		_0h4yb5wu = _f43eg0w0("PRECISION" );
		_bogm0gwy = (_h75qnr7l * (ILNumerics.F2NET.Intrinsics.DBLE(_dxpq0xkr ) / _0h4yb5wu));//* 
		//*     ==== Setup deflation window ==== 
		//* 
		
		_msaexyn2 = ILNumerics.F2NET.Intrinsics.MIN(_w6pmxgch ,(_7d8gh478 - _4a77vvpa) + (int)1 );
		_87d05vk3 = ((_7d8gh478 - _msaexyn2) + (int)1);
		if (_87d05vk3 == _4a77vvpa)
		{
			
			_irk8i6qr = _d0547bi2;
		}
		else
		{
			
			_irk8i6qr = *(_ogkjl6gu+(_87d05vk3 - 1) + (_87d05vk3 - (int)1 - 1) * 1 * (_1iekxpnw));
		}
		//* 
		
		if (_7d8gh478 == _87d05vk3)
		{
			//* 
			//*        ==== 1-by-1 deflation window: not much to do ==== 
			//* 
			
			*(_9tenoh4m+(_87d05vk3 - 1)) = *(_ogkjl6gu+(_87d05vk3 - 1) + (_87d05vk3 - 1) * 1 * (_1iekxpnw));
			_edl2gwc7 = (int)1;
			_rwm6akyl = (int)0;
			if (_4jqx89by(_irk8i6qr ) <= ILNumerics.F2NET.Intrinsics.MAX(_bogm0gwy ,_0h4yb5wu * _4jqx89by(*(_ogkjl6gu+(_87d05vk3 - 1) + (_87d05vk3 - 1) * 1 * (_1iekxpnw)) ) ))
			{
				
				_edl2gwc7 = (int)0;
				_rwm6akyl = (int)1;
				if (_87d05vk3 > _4a77vvpa)
				*(_ogkjl6gu+(_87d05vk3 - 1) + (_87d05vk3 - (int)1 - 1) * 1 * (_1iekxpnw)) = _d0547bi2;
			}
			
			*(_apig8meb+((int)1 - 1)) = _kxg5drh2;
			return;
		}
		//* 
		//*     ==== Convert to spike-triangular form.  (In case of a 
		//*     .    rare QR failure, this routine continues to do 
		//*     .    aggressive early deflation using that part of 
		//*     .    the deflation window that converged using INFQR 
		//*     .    here and there to keep track.) ==== 
		//* 
		
		_nihu9ses("U" ,ref _msaexyn2 ,ref _msaexyn2 ,(_ogkjl6gu+(_87d05vk3 - 1) + (_87d05vk3 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,_2ivtt43r ,ref _w8yhbr2r );
		_ly902k7t(ref Unsafe.AsRef(_msaexyn2 - (int)1) ,(_ogkjl6gu+(_87d05vk3 + (int)1 - 1) + (_87d05vk3 - 1) * 1 * (_1iekxpnw)),ref Unsafe.AsRef(_1iekxpnw + (int)1) ,(_2ivtt43r+((int)2 - 1) + ((int)1 - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef(_w8yhbr2r + (int)1) );//* 
		
		_k14i9nd8("A" ,ref _msaexyn2 ,ref _msaexyn2 ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze );
		_rs56fkjq = _4mvd6e4d(ref Unsafe.AsRef((int)12) ,"ZLAQR3" ,"SV" ,ref _msaexyn2 ,ref Unsafe.AsRef((int)1) ,ref _msaexyn2 ,ref _6fnxzlyp );
		if (_msaexyn2 > _rs56fkjq)
		{
			
			_o05elgn6(ref Unsafe.AsRef(true) ,ref Unsafe.AsRef(true) ,ref _msaexyn2 ,ref Unsafe.AsRef((int)1) ,ref _msaexyn2 ,_2ivtt43r ,ref _w8yhbr2r ,(_9tenoh4m+(_87d05vk3 - 1)),ref Unsafe.AsRef((int)1) ,ref _msaexyn2 ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _6fnxzlyp ,ref _jhtbcevs );
		}
		else
		{
			
			_p11f7879(ref Unsafe.AsRef(true) ,ref Unsafe.AsRef(true) ,ref _msaexyn2 ,ref Unsafe.AsRef((int)1) ,ref _msaexyn2 ,_2ivtt43r ,ref _w8yhbr2r ,(_9tenoh4m+(_87d05vk3 - 1)),ref Unsafe.AsRef((int)1) ,ref _msaexyn2 ,_ycxba85s ,ref _ys09rxze ,ref _jhtbcevs );
		}
		//* 
		//*     ==== Deflation detection loop ==== 
		//* 
		
		_edl2gwc7 = _msaexyn2;
		_ab05c09e = (_jhtbcevs + (int)1);
		{
			System.Int32 __81fgg2dlsvn2691 = (System.Int32)((_jhtbcevs + (int)1));
			const System.Int32 __81fgg2step2691 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2691;
			for (__81fgg2count2691 = System.Math.Max(0, (System.Int32)(((System.Int32)(_msaexyn2) - __81fgg2dlsvn2691 + __81fgg2step2691) / __81fgg2step2691)), _hcqqzxmr = __81fgg2dlsvn2691; __81fgg2count2691 != 0; __81fgg2count2691--, _hcqqzxmr += (__81fgg2step2691)) {

			{
				//* 
				//*        ==== Small spike tip deflation test ==== 
				//* 
				
				_yl9tlixq = _4jqx89by(*(_2ivtt43r+(_edl2gwc7 - 1) + (_edl2gwc7 - 1) * 1 * (_w8yhbr2r)) );
				if (_yl9tlixq == _7cb2gzzt)
				_yl9tlixq = _4jqx89by(_irk8i6qr );
				if ((_4jqx89by(_irk8i6qr ) * _4jqx89by(*(_ycxba85s+((int)1 - 1) + (_edl2gwc7 - 1) * 1 * (_ys09rxze)) )) <= ILNumerics.F2NET.Intrinsics.MAX(_bogm0gwy ,_0h4yb5wu * _yl9tlixq ))
				{
					//* 
					//*           ==== One more converged eigenvalue ==== 
					//* 
					
					_edl2gwc7 = (_edl2gwc7 - (int)1);
				}
				else
				{
					//* 
					//*           ==== One undeflatable eigenvalue.  Move it up out of the 
					//*           .    way.   (ZTREXC can not fail in this case.) ==== 
					//* 
					
					_la6t805m = _edl2gwc7;
					_a8521p1n("V" ,ref _msaexyn2 ,_2ivtt43r ,ref _w8yhbr2r ,_ycxba85s ,ref _ys09rxze ,ref _la6t805m ,ref _ab05c09e ,ref _gro5yvfo );
					_ab05c09e = (_ab05c09e + (int)1);
				}
				
Mark10:;
				// continue
			}
						}		}//* 
		//*        ==== Return to Hessenberg form ==== 
		//* 
		
		if (_edl2gwc7 == (int)0)
		_irk8i6qr = _d0547bi2;//* 
		
		if (_edl2gwc7 < _msaexyn2)
		{
			//* 
			//*        ==== sorting the diagonal of T improves accuracy for 
			//*        .    graded matrices.  ==== 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn2692 = (System.Int32)((_jhtbcevs + (int)1));
				const System.Int32 __81fgg2step2692 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2692;
				for (__81fgg2count2692 = System.Math.Max(0, (System.Int32)(((System.Int32)(_edl2gwc7) - __81fgg2dlsvn2692 + __81fgg2step2692) / __81fgg2step2692)), _b5p6od9s = __81fgg2dlsvn2692; __81fgg2count2692 != 0; __81fgg2count2692--, _b5p6od9s += (__81fgg2step2692)) {

				{
					
					_la6t805m = _b5p6od9s;
					{
						System.Int32 __81fgg2dlsvn2693 = (System.Int32)((_b5p6od9s + (int)1));
						const System.Int32 __81fgg2step2693 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2693;
						for (__81fgg2count2693 = System.Math.Max(0, (System.Int32)(((System.Int32)(_edl2gwc7) - __81fgg2dlsvn2693 + __81fgg2step2693) / __81fgg2step2693)), _znpjgsef = __81fgg2dlsvn2693; __81fgg2count2693 != 0; __81fgg2count2693--, _znpjgsef += (__81fgg2step2693)) {

						{
							
							if (_4jqx89by(*(_2ivtt43r+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)) ) > _4jqx89by(*(_2ivtt43r+(_la6t805m - 1) + (_la6t805m - 1) * 1 * (_w8yhbr2r)) ))
							_la6t805m = _znpjgsef;
Mark20:;
							// continue
						}
												}					}
					_ab05c09e = _b5p6od9s;
					if (_la6t805m != _ab05c09e)
					_a8521p1n("V" ,ref _msaexyn2 ,_2ivtt43r ,ref _w8yhbr2r ,_ycxba85s ,ref _ys09rxze ,ref _la6t805m ,ref _ab05c09e ,ref _gro5yvfo );
Mark30:;
					// continue
				}
								}			}
		}
		//* 
		//*     ==== Restore shift/eigenvalue array from T ==== 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn2694 = (System.Int32)((_jhtbcevs + (int)1));
			const System.Int32 __81fgg2step2694 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2694;
			for (__81fgg2count2694 = System.Math.Max(0, (System.Int32)(((System.Int32)(_msaexyn2) - __81fgg2dlsvn2694 + __81fgg2step2694) / __81fgg2step2694)), _b5p6od9s = __81fgg2dlsvn2694; __81fgg2count2694 != 0; __81fgg2count2694--, _b5p6od9s += (__81fgg2step2694)) {

			{
				
				*(_9tenoh4m+((_87d05vk3 + _b5p6od9s) - (int)1 - 1)) = *(_2ivtt43r+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r));
Mark40:;
				// continue
			}
						}		}//* 
		//* 
		
		if ((_edl2gwc7 < _msaexyn2) | (_irk8i6qr == _d0547bi2))
		{
			
			if ((_edl2gwc7 > (int)1) & (_irk8i6qr != _d0547bi2))
			{
				//* 
				//*           ==== Reflect spike back into lower triangle ==== 
				//* 
				
				_ly902k7t(ref _edl2gwc7 ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref Unsafe.AsRef((int)1) );
				{
					System.Int32 __81fgg2dlsvn2695 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2695 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2695;
					for (__81fgg2count2695 = System.Math.Max(0, (System.Int32)(((System.Int32)(_edl2gwc7) - __81fgg2dlsvn2695 + __81fgg2step2695) / __81fgg2step2695)), _b5p6od9s = __81fgg2dlsvn2695; __81fgg2count2695 != 0; __81fgg2count2695--, _b5p6od9s += (__81fgg2step2695)) {

					{
						
						*(_apig8meb+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.DCONJG(*(_apig8meb+(_b5p6od9s - 1)) );
Mark50:;
						// continue
					}
										}				}
				_bafcbx97 = *(_apig8meb+((int)1 - 1));
				_4btmjfem(ref _edl2gwc7 ,ref _bafcbx97 ,(_apig8meb+((int)2 - 1)),ref Unsafe.AsRef((int)1) ,ref _0446f4de );
				*(_apig8meb+((int)1 - 1)) = _kxg5drh2;//* 
				
				_k14i9nd8("L" ,ref Unsafe.AsRef(_msaexyn2 - (int)2) ,ref Unsafe.AsRef(_msaexyn2 - (int)2) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_2ivtt43r+((int)3 - 1) + ((int)1 - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r );//* 
				
				_h7ckdrdn("L" ,ref _edl2gwc7 ,ref _msaexyn2 ,_apig8meb ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.DCONJG(_0446f4de )) ,_2ivtt43r ,ref _w8yhbr2r ,(_apig8meb+(_msaexyn2 + (int)1 - 1)));
				_h7ckdrdn("R" ,ref _edl2gwc7 ,ref _edl2gwc7 ,_apig8meb ,ref Unsafe.AsRef((int)1) ,ref _0446f4de ,_2ivtt43r ,ref _w8yhbr2r ,(_apig8meb+(_msaexyn2 + (int)1 - 1)));
				_h7ckdrdn("R" ,ref _msaexyn2 ,ref _edl2gwc7 ,_apig8meb ,ref Unsafe.AsRef((int)1) ,ref _0446f4de ,_ycxba85s ,ref _ys09rxze ,(_apig8meb+(_msaexyn2 + (int)1 - 1)));//* 
				
				_j69fohs3(ref _msaexyn2 ,ref Unsafe.AsRef((int)1) ,ref _edl2gwc7 ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,(_apig8meb+(_msaexyn2 + (int)1 - 1)),ref Unsafe.AsRef(_6fnxzlyp - _msaexyn2) ,ref _gro5yvfo );
			}
			//* 
			//*        ==== Copy updated reduced window into place ==== 
			//* 
			
			if (_87d05vk3 > (int)1)
			*(_ogkjl6gu+(_87d05vk3 - 1) + (_87d05vk3 - (int)1 - 1) * 1 * (_1iekxpnw)) = (_irk8i6qr * ILNumerics.F2NET.Intrinsics.DCONJG(*(_ycxba85s+((int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)) ));
			_nihu9ses("U" ,ref _msaexyn2 ,ref _msaexyn2 ,_2ivtt43r ,ref _w8yhbr2r ,(_ogkjl6gu+(_87d05vk3 - 1) + (_87d05vk3 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw );
			_ly902k7t(ref Unsafe.AsRef(_msaexyn2 - (int)1) ,(_2ivtt43r+((int)2 - 1) + ((int)1 - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef(_w8yhbr2r + (int)1) ,(_ogkjl6gu+(_87d05vk3 + (int)1 - 1) + (_87d05vk3 - 1) * 1 * (_1iekxpnw)),ref Unsafe.AsRef(_1iekxpnw + (int)1) );//* 
			//*        ==== Accumulate orthogonal matrix in order update 
			//*        .    H and Z, if requested.  ==== 
			//* 
			
			if ((_edl2gwc7 > (int)1) & (_irk8i6qr != _d0547bi2))
			_ml2zcgc8("R" ,"N" ,ref _msaexyn2 ,ref _edl2gwc7 ,ref Unsafe.AsRef((int)1) ,ref _edl2gwc7 ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,_ycxba85s ,ref _ys09rxze ,(_apig8meb+(_msaexyn2 + (int)1 - 1)),ref Unsafe.AsRef(_6fnxzlyp - _msaexyn2) ,ref _gro5yvfo );//* 
			//*        ==== Update vertical slab in H ==== 
			//* 
			
			if (_amt8y1zm)
			{
				
				_y834yh01 = (int)1;
			}
			else
			{
				
				_y834yh01 = _4a77vvpa;
			}
			
			{
				System.Int32 __81fgg2dlsvn2696 = (System.Int32)(_y834yh01);
				System.Int32 __81fgg2step2696 = (System.Int32)(_aiz0v1d1);
				System.Int32 __81fgg2count2696;
				for (__81fgg2count2696 = System.Math.Max(0, (System.Int32)(((System.Int32)(_87d05vk3 - (int)1) - __81fgg2dlsvn2696 + __81fgg2step2696) / __81fgg2step2696)), _pnhq01yl = __81fgg2dlsvn2696; __81fgg2count2696 != 0; __81fgg2count2696--, _pnhq01yl += (__81fgg2step2696)) {

				{
					
					_m4jdywnh = ILNumerics.F2NET.Intrinsics.MIN(_aiz0v1d1 ,_87d05vk3 - _pnhq01yl );
					_xos1d1er("N" ,"N" ,ref _m4jdywnh ,ref _msaexyn2 ,ref _msaexyn2 ,ref Unsafe.AsRef(_kxg5drh2) ,(_ogkjl6gu+(_pnhq01yl - 1) + (_87d05vk3 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_d0547bi2) ,_pm308b4n ,ref _vlfzpxiy );
					_nihu9ses("A" ,ref _m4jdywnh ,ref _msaexyn2 ,_pm308b4n ,ref _vlfzpxiy ,(_ogkjl6gu+(_pnhq01yl - 1) + (_87d05vk3 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw );
Mark60:;
					// continue
				}
								}			}//* 
			//*        ==== Update horizontal slab in H ==== 
			//* 
			
			if (_amt8y1zm)
			{
				
				{
					System.Int32 __81fgg2dlsvn2697 = (System.Int32)((_7d8gh478 + (int)1));
					System.Int32 __81fgg2step2697 = (System.Int32)(_aym8a085);
					System.Int32 __81fgg2count2697;
					for (__81fgg2count2697 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2697 + __81fgg2step2697) / __81fgg2step2697)), _chb1oswp = __81fgg2dlsvn2697; __81fgg2count2697 != 0; __81fgg2count2697--, _chb1oswp += (__81fgg2step2697)) {

					{
						
						_m4jdywnh = ILNumerics.F2NET.Intrinsics.MIN(_aym8a085 ,(_dxpq0xkr - _chb1oswp) + (int)1 );
						_xos1d1er("C" ,"N" ,ref _msaexyn2 ,ref _m4jdywnh ,ref _msaexyn2 ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,(_ogkjl6gu+(_87d05vk3 - 1) + (_chb1oswp - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,ref Unsafe.AsRef(_d0547bi2) ,_2ivtt43r ,ref _w8yhbr2r );
						_nihu9ses("A" ,ref _msaexyn2 ,ref _m4jdywnh ,_2ivtt43r ,ref _w8yhbr2r ,(_ogkjl6gu+(_87d05vk3 - 1) + (_chb1oswp - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw );
Mark70:;
						// continue
					}
										}				}
			}
			//* 
			//*        ==== Update vertical slab in Z ==== 
			//* 
			
			if (_189gzykk)
			{
				
				{
					System.Int32 __81fgg2dlsvn2698 = (System.Int32)(_pinc1ofz);
					System.Int32 __81fgg2step2698 = (System.Int32)(_aiz0v1d1);
					System.Int32 __81fgg2count2698;
					for (__81fgg2count2698 = System.Math.Max(0, (System.Int32)(((System.Int32)(_mg9v9w4h) - __81fgg2dlsvn2698 + __81fgg2step2698) / __81fgg2step2698)), _pnhq01yl = __81fgg2dlsvn2698; __81fgg2count2698 != 0; __81fgg2count2698--, _pnhq01yl += (__81fgg2step2698)) {

					{
						
						_m4jdywnh = ILNumerics.F2NET.Intrinsics.MIN(_aiz0v1d1 ,(_mg9v9w4h - _pnhq01yl) + (int)1 );
						_xos1d1er("N" ,"N" ,ref _m4jdywnh ,ref _msaexyn2 ,ref _msaexyn2 ,ref Unsafe.AsRef(_kxg5drh2) ,(_7e60fcso+(_pnhq01yl - 1) + (_87d05vk3 - 1) * 1 * (_5l1tna8s)),ref _5l1tna8s ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_d0547bi2) ,_pm308b4n ,ref _vlfzpxiy );
						_nihu9ses("A" ,ref _m4jdywnh ,ref _msaexyn2 ,_pm308b4n ,ref _vlfzpxiy ,(_7e60fcso+(_pnhq01yl - 1) + (_87d05vk3 - 1) * 1 * (_5l1tna8s)),ref _5l1tna8s );
Mark80:;
						// continue
					}
										}				}
			}
			
		}
		//* 
		//*     ==== Return the number of deflations ... ==== 
		//* 
		
		_rwm6akyl = (_msaexyn2 - _edl2gwc7);//* 
		//*     ==== ... and the number of shifts. (Subtracting 
		//*     .    INFQR from the spike length takes care 
		//*     .    of the case of a rare QR failure while 
		//*     .    calculating eigenvalues of the deflation 
		//*     .    window.)  ==== 
		//* 
		
		_edl2gwc7 = (_edl2gwc7 - _jhtbcevs);//* 
		//*      ==== Return optimal workspace. ==== 
		//* 
		
		*(_apig8meb+((int)1 - 1)) = ILNumerics.F2NET.Intrinsics.DCMPLX(_e4ueamrn ,(int)0 );//* 
		//*     ==== End of ZLAQR3 ==== 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
