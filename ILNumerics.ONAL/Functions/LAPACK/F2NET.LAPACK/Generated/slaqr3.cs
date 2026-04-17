
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
//*> \brief \b SLAQR3 performs the orthogonal similarity transformation of a Hessenberg matrix to detect and deflate fully converged eigenvalues from a trailing principal submatrix (aggressive early deflation). 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLAQR3 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slaqr3.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slaqr3.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slaqr3.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLAQR3( WANTT, WANTZ, N, KTOP, KBOT, NW, H, LDH, ILOZ, 
//*                          IHIZ, Z, LDZ, NS, ND, SR, SI, V, LDV, NH, T, 
//*                          LDT, NV, WV, LDWV, WORK, LWORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            IHIZ, ILOZ, KBOT, KTOP, LDH, LDT, LDV, LDWV, 
//*      $                   LDZ, LWORK, N, ND, NH, NS, NV, NW 
//*       LOGICAL            WANTT, WANTZ 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               H( LDH, * ), SI( * ), SR( * ), T( LDT, * ), 
//*      $                   V( LDV, * ), WORK( * ), WV( LDWV, * ), 
//*      $                   Z( LDZ, * ) 
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
//*>    SLAQR3 accepts as input an upper Hessenberg matrix 
//*>    H and performs an orthogonal similarity transformation 
//*>    designed to detect and deflate fully converged eigenvalues from 
//*>    a trailing principal submatrix.  On output H has been over- 
//*>    written by a new Hessenberg matrix that is a perturbation of 
//*>    an orthogonal similarity transformation of H.  It is to be 
//*>    hoped that the final version of H has many zero subdiagonal 
//*>    entries. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] WANTT 
//*> \verbatim 
//*>          WANTT is LOGICAL 
//*>          If .TRUE., then the Hessenberg matrix H is fully updated 
//*>          so that the quasi-triangular Schur factor may be 
//*>          computed (in cooperation with the calling subroutine). 
//*>          If .FALSE., then only enough of H is updated to preserve 
//*>          the eigenvalues. 
//*> \endverbatim 
//*> 
//*> \param[in] WANTZ 
//*> \verbatim 
//*>          WANTZ is LOGICAL 
//*>          If .TRUE., then the orthogonal matrix Z is updated so 
//*>          so that the orthogonal Schur factor may be computed 
//*>          (in cooperation with the calling subroutine). 
//*>          If .FALSE., then Z is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix H and (if WANTZ is .TRUE.) the 
//*>          order of the orthogonal matrix Z. 
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
//*>          H is REAL array, dimension (LDH,N) 
//*>          On input the initial N-by-N section of H stores the 
//*>          Hessenberg matrix undergoing aggressive early deflation. 
//*>          On output H has been transformed by an orthogonal 
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
//*>          Z is REAL array, dimension (LDZ,N) 
//*>          IF WANTZ is .TRUE., then on output, the orthogonal 
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
//*> \param[out] SR 
//*> \verbatim 
//*>          SR is REAL array, dimension (KBOT) 
//*> \endverbatim 
//*> 
//*> \param[out] SI 
//*> \verbatim 
//*>          SI is REAL array, dimension (KBOT) 
//*>          On output, the real and imaginary parts of approximate 
//*>          eigenvalues that may be used for shifts are stored in 
//*>          SR(KBOT-ND-NS+1) through SR(KBOT-ND) and 
//*>          SI(KBOT-ND-NS+1) through SI(KBOT-ND), respectively. 
//*>          The real and imaginary parts of converged eigenvalues 
//*>          are stored in SR(KBOT-ND+1) through SR(KBOT) and 
//*>          SI(KBOT-ND+1) through SI(KBOT), respectively. 
//*> \endverbatim 
//*> 
//*> \param[out] V 
//*> \verbatim 
//*>          V is REAL array, dimension (LDV,NW) 
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
//*>          T is REAL array, dimension (LDT,NW) 
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
//*>          WV is REAL array, dimension (LDWV,NW) 
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
//*>          WORK is REAL array, dimension (LWORK) 
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
//*>          If LWORK = -1, then a workspace query is assumed; SLAQR3 
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
//*> \ingroup realOTHERauxiliary 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>       Karen Braman and Ralph Byers, Department of Mathematics, 
//*>       University of Kansas, USA 
//*> 
//*  ===================================================================== 

	 
	public static void _i5390nva(ref Boolean _amt8y1zm, ref Boolean _189gzykk, ref Int32 _dxpq0xkr, ref Int32 _4a77vvpa, ref Int32 _7d8gh478, ref Int32 _w6pmxgch, Single* _ogkjl6gu, ref Int32 _1iekxpnw, ref Int32 _pinc1ofz, ref Int32 _mg9v9w4h, Single* _7e60fcso, ref Int32 _5l1tna8s, ref Int32 _edl2gwc7, ref Int32 _rwm6akyl, Single* _o2dp5e8w, Single* _sgos5dql, Single* _ycxba85s, ref Int32 _ys09rxze, ref Int32 _aym8a085, Single* _2ivtt43r, ref Int32 _w8yhbr2r, ref Int32 _aiz0v1d1, Single* _pm308b4n, ref Int32 _vlfzpxiy, Single* _apig8meb, ref Int32 _6fnxzlyp)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
Single _zwm0s9sq =  default;
Single _uv0s0qmf =  default;
Single _bafcbx97 =  default;
Single _985e9e9b =  default;
Single _82tpdhyl =  default;
Single _f4rvsg6o =  default;
Single _azyn2kcv =  default;
Single _g7802d9w =  default;
Single _yl9tlixq =  default;
Single _irk8i6qr =  default;
Single _odf6ja0t =  default;
Single _h75qnr7l =  default;
Single _bogm0gwy =  default;
Single _8tmd0ner =  default;
Single _0446f4de =  default;
Single _0h4yb5wu =  default;
Int32 _b5p6od9s =  default;
Int32 _la6t805m =  default;
Int32 _ab05c09e =  default;
Int32 _gro5yvfo =  default;
Int32 _jhtbcevs =  default;
Int32 _znpjgsef =  default;
Int32 _msaexyn2 =  default;
Int32 _umlkckdg =  default;
Int32 _chb1oswp =  default;
Int32 _bw00meu7 =  default;
Int32 _m4jdywnh =  default;
Int32 _pnhq01yl =  default;
Int32 _87d05vk3 =  default;
Int32 _y834yh01 =  default;
Int32 _ve9qn4fh =  default;
Int32 _7c13pjdp =  default;
Int32 _44malgzk =  default;
Int32 _e4ueamrn =  default;
Int32 _rs56fkjq =  default;
Boolean _7pcolzjf =  default;
Boolean _3sak82w7 =  default;
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
			//*        ==== Workspace query call to SGEHRD ==== 
			//* 
			
			_jgi8r1sb(ref _msaexyn2 ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_msaexyn2 - (int)1) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,_apig8meb ,ref Unsafe.AsRef((int)-1) ,ref _gro5yvfo );
			_ve9qn4fh = ILNumerics.F2NET.Intrinsics.INT(*(_apig8meb+((int)1 - 1)) );//* 
			//*        ==== Workspace query call to SORMHR ==== 
			//* 
			
			_68lkolob("R" ,"N" ,ref _msaexyn2 ,ref _msaexyn2 ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_msaexyn2 - (int)1) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref Unsafe.AsRef((int)-1) ,ref _gro5yvfo );
			_7c13pjdp = ILNumerics.F2NET.Intrinsics.INT(*(_apig8meb+((int)1 - 1)) );//* 
			//*        ==== Workspace query call to SLAQR4 ==== 
			//* 
			
			_zthy4g1a(ref Unsafe.AsRef(true) ,ref Unsafe.AsRef(true) ,ref _msaexyn2 ,ref Unsafe.AsRef((int)1) ,ref _msaexyn2 ,_2ivtt43r ,ref _w8yhbr2r ,_o2dp5e8w ,_sgos5dql ,ref Unsafe.AsRef((int)1) ,ref _msaexyn2 ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref Unsafe.AsRef((int)-1) ,ref _jhtbcevs );
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
			
			*(_apig8meb+((int)1 - 1)) = ILNumerics.F2NET.Intrinsics.REAL(_e4ueamrn );
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
		
		_h75qnr7l = _d5tu038y("SAFE MINIMUM" );
		_odf6ja0t = (_kxg5drh2 / _h75qnr7l);
		_6cljvt6b(ref _h75qnr7l ,ref _odf6ja0t );
		_0h4yb5wu = _d5tu038y("PRECISION" );
		_bogm0gwy = (_h75qnr7l * (ILNumerics.F2NET.Intrinsics.REAL(_dxpq0xkr ) / _0h4yb5wu));//* 
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
			
			*(_o2dp5e8w+(_87d05vk3 - 1)) = *(_ogkjl6gu+(_87d05vk3 - 1) + (_87d05vk3 - 1) * 1 * (_1iekxpnw));
			*(_sgos5dql+(_87d05vk3 - 1)) = _d0547bi2;
			_edl2gwc7 = (int)1;
			_rwm6akyl = (int)0;
			if (ILNumerics.F2NET.Intrinsics.ABS(_irk8i6qr ) <= ILNumerics.F2NET.Intrinsics.MAX(_bogm0gwy ,_0h4yb5wu * ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_87d05vk3 - 1) + (_87d05vk3 - 1) * 1 * (_1iekxpnw)) ) ))
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
		
		_m38y8dyg("U" ,ref _msaexyn2 ,ref _msaexyn2 ,(_ogkjl6gu+(_87d05vk3 - 1) + (_87d05vk3 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,_2ivtt43r ,ref _w8yhbr2r );
		_wcs7ne88(ref Unsafe.AsRef(_msaexyn2 - (int)1) ,(_ogkjl6gu+(_87d05vk3 + (int)1 - 1) + (_87d05vk3 - 1) * 1 * (_1iekxpnw)),ref Unsafe.AsRef(_1iekxpnw + (int)1) ,(_2ivtt43r+((int)2 - 1) + ((int)1 - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef(_w8yhbr2r + (int)1) );//* 
		
		_t013e1c8("A" ,ref _msaexyn2 ,ref _msaexyn2 ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze );
		_rs56fkjq = _4mvd6e4d(ref Unsafe.AsRef((int)12) ,"SLAQR3" ,"SV" ,ref _msaexyn2 ,ref Unsafe.AsRef((int)1) ,ref _msaexyn2 ,ref _6fnxzlyp );
		if (_msaexyn2 > _rs56fkjq)
		{
			
			_zthy4g1a(ref Unsafe.AsRef(true) ,ref Unsafe.AsRef(true) ,ref _msaexyn2 ,ref Unsafe.AsRef((int)1) ,ref _msaexyn2 ,_2ivtt43r ,ref _w8yhbr2r ,(_o2dp5e8w+(_87d05vk3 - 1)),(_sgos5dql+(_87d05vk3 - 1)),ref Unsafe.AsRef((int)1) ,ref _msaexyn2 ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _6fnxzlyp ,ref _jhtbcevs );
		}
		else
		{
			
			_7ds1b9hb(ref Unsafe.AsRef(true) ,ref Unsafe.AsRef(true) ,ref _msaexyn2 ,ref Unsafe.AsRef((int)1) ,ref _msaexyn2 ,_2ivtt43r ,ref _w8yhbr2r ,(_o2dp5e8w+(_87d05vk3 - 1)),(_sgos5dql+(_87d05vk3 - 1)),ref Unsafe.AsRef((int)1) ,ref _msaexyn2 ,_ycxba85s ,ref _ys09rxze ,ref _jhtbcevs );
		}
		//* 
		//*     ==== STREXC needs a clean margin near the diagonal ==== 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn2379 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2379 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2379;
			for (__81fgg2count2379 = System.Math.Max(0, (System.Int32)(((System.Int32)(_msaexyn2 - (int)3) - __81fgg2dlsvn2379 + __81fgg2step2379) / __81fgg2step2379)), _znpjgsef = __81fgg2dlsvn2379; __81fgg2count2379 != 0; __81fgg2count2379--, _znpjgsef += (__81fgg2step2379)) {

			{
				
				*(_2ivtt43r+(_znpjgsef + (int)2 - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)) = _d0547bi2;
				*(_2ivtt43r+(_znpjgsef + (int)3 - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)) = _d0547bi2;
Mark10:;
				// continue
			}
						}		}
		if (_msaexyn2 > (int)2)
		*(_2ivtt43r+(_msaexyn2 - 1) + (_msaexyn2 - (int)2 - 1) * 1 * (_w8yhbr2r)) = _d0547bi2;//* 
		//*     ==== Deflation detection loop ==== 
		//* 
		
		_edl2gwc7 = _msaexyn2;
		_ab05c09e = (_jhtbcevs + (int)1);
Mark20:;
		// continue
		if (_ab05c09e <= _edl2gwc7)
		{
			
			if (_edl2gwc7 == (int)1)
			{
				
				_7pcolzjf = false;
			}
			else
			{
				
				_7pcolzjf = (*(_2ivtt43r+(_edl2gwc7 - 1) + (_edl2gwc7 - (int)1 - 1) * 1 * (_w8yhbr2r)) != _d0547bi2);
			}
			//* 
			//*        ==== Small spike tip test for deflation ==== 
			//* 
			
			if (!(_7pcolzjf))
			{
				//* 
				//*           ==== Real eigenvalue ==== 
				//* 
				
				_yl9tlixq = ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+(_edl2gwc7 - 1) + (_edl2gwc7 - 1) * 1 * (_w8yhbr2r)) );
				if (_yl9tlixq == _d0547bi2)
				_yl9tlixq = ILNumerics.F2NET.Intrinsics.ABS(_irk8i6qr );
				if (ILNumerics.F2NET.Intrinsics.ABS(_irk8i6qr * *(_ycxba85s+((int)1 - 1) + (_edl2gwc7 - 1) * 1 * (_ys09rxze)) ) <= ILNumerics.F2NET.Intrinsics.MAX(_bogm0gwy ,_0h4yb5wu * _yl9tlixq ))
				{
					//* 
					//*              ==== Deflatable ==== 
					//* 
					
					_edl2gwc7 = (_edl2gwc7 - (int)1);
				}
				else
				{
					//* 
					//*              ==== Undeflatable.   Move it up out of the way. 
					//*              .    (STREXC can not fail in this case.) ==== 
					//* 
					
					_la6t805m = _edl2gwc7;
					_ogu7zj4u("V" ,ref _msaexyn2 ,_2ivtt43r ,ref _w8yhbr2r ,_ycxba85s ,ref _ys09rxze ,ref _la6t805m ,ref _ab05c09e ,_apig8meb ,ref _gro5yvfo );
					_ab05c09e = (_ab05c09e + (int)1);
				}
				
			}
			else
			{
				//* 
				//*           ==== Complex conjugate pair ==== 
				//* 
				
				_yl9tlixq = (ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+(_edl2gwc7 - 1) + (_edl2gwc7 - 1) * 1 * (_w8yhbr2r)) ) + (ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+(_edl2gwc7 - 1) + (_edl2gwc7 - (int)1 - 1) * 1 * (_w8yhbr2r)) ) ) * ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+(_edl2gwc7 - (int)1 - 1) + (_edl2gwc7 - 1) * 1 * (_w8yhbr2r)) ) )));
				if (_yl9tlixq == _d0547bi2)
				_yl9tlixq = ILNumerics.F2NET.Intrinsics.ABS(_irk8i6qr );
				if (ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_irk8i6qr * *(_ycxba85s+((int)1 - 1) + (_edl2gwc7 - 1) * 1 * (_ys09rxze)) ) ,ILNumerics.F2NET.Intrinsics.ABS(_irk8i6qr * *(_ycxba85s+((int)1 - 1) + (_edl2gwc7 - (int)1 - 1) * 1 * (_ys09rxze)) ) ) <= ILNumerics.F2NET.Intrinsics.MAX(_bogm0gwy ,_0h4yb5wu * _yl9tlixq ))
				{
					//* 
					//*              ==== Deflatable ==== 
					//* 
					
					_edl2gwc7 = (_edl2gwc7 - (int)2);
				}
				else
				{
					//* 
					//*              ==== Undeflatable. Move them up out of the way. 
					//*              .    Fortunately, STREXC does the right thing with 
					//*              .    ILST in case of a rare exchange failure. ==== 
					//* 
					
					_la6t805m = _edl2gwc7;
					_ogu7zj4u("V" ,ref _msaexyn2 ,_2ivtt43r ,ref _w8yhbr2r ,_ycxba85s ,ref _ys09rxze ,ref _la6t805m ,ref _ab05c09e ,_apig8meb ,ref _gro5yvfo );
					_ab05c09e = (_ab05c09e + (int)2);
				}
				
			}
			//* 
			//*        ==== End deflation detection loop ==== 
			//* 
			goto Mark20;
		}
		//* 
		//*        ==== Return to Hessenberg form ==== 
		//* 
		
		if (_edl2gwc7 == (int)0)
		_irk8i6qr = _d0547bi2;//* 
		
		if (_edl2gwc7 < _msaexyn2)
		{
			//* 
			//*        ==== sorting diagonal blocks of T improves accuracy for 
			//*        .    graded matrices.  Bubble sort deals well with 
			//*        .    exchange failures. ==== 
			//* 
			
			_3sak82w7 = false;
			_b5p6od9s = (_edl2gwc7 + (int)1);
Mark30:;
			// continue
			if (_3sak82w7)goto Mark50;
			_3sak82w7 = true;//* 
			
			_bw00meu7 = (_b5p6od9s - (int)1);
			_b5p6od9s = (_jhtbcevs + (int)1);
			if (_b5p6od9s == _edl2gwc7)
			{
				
				_umlkckdg = (_b5p6od9s + (int)1);
			}
			else
			if (*(_2ivtt43r+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)) == _d0547bi2)
			{
				
				_umlkckdg = (_b5p6od9s + (int)1);
			}
			else
			{
				
				_umlkckdg = (_b5p6od9s + (int)2);
			}
			
Mark40:;
			// continue
			if (_umlkckdg <= _bw00meu7)
			{
				
				if (_umlkckdg == (_b5p6od9s + (int)1))
				{
					
					_azyn2kcv = ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)) );
				}
				else
				{
					
					_azyn2kcv = (ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)) ) + (ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)) ) ) * ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_w8yhbr2r)) ) )));
				}
				//* 
				
				if (_umlkckdg == _bw00meu7)
				{
					
					_g7802d9w = ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_w8yhbr2r)) );
				}
				else
				if (*(_2ivtt43r+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_w8yhbr2r)) == _d0547bi2)
				{
					
					_g7802d9w = ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_w8yhbr2r)) );
				}
				else
				{
					
					_g7802d9w = (ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_w8yhbr2r)) ) + (ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_w8yhbr2r)) ) ) * ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+(_umlkckdg - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_w8yhbr2r)) ) )));
				}
				//* 
				
				if (_azyn2kcv >= _g7802d9w)
				{
					
					_b5p6od9s = _umlkckdg;
				}
				else
				{
					
					_3sak82w7 = false;
					_la6t805m = _b5p6od9s;
					_ab05c09e = _umlkckdg;
					_ogu7zj4u("V" ,ref _msaexyn2 ,_2ivtt43r ,ref _w8yhbr2r ,_ycxba85s ,ref _ys09rxze ,ref _la6t805m ,ref _ab05c09e ,_apig8meb ,ref _gro5yvfo );
					if (_gro5yvfo == (int)0)
					{
						
						_b5p6od9s = _ab05c09e;
					}
					else
					{
						
						_b5p6od9s = _umlkckdg;
					}
					
				}
				
				if (_b5p6od9s == _bw00meu7)
				{
					
					_umlkckdg = (_b5p6od9s + (int)1);
				}
				else
				if (*(_2ivtt43r+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)) == _d0547bi2)
				{
					
					_umlkckdg = (_b5p6od9s + (int)1);
				}
				else
				{
					
					_umlkckdg = (_b5p6od9s + (int)2);
				}
				goto Mark40;
			}
			goto Mark30;
Mark50:;
			// continue
		}
		//* 
		//*     ==== Restore shift/eigenvalue array from T ==== 
		//* 
		
		_b5p6od9s = _msaexyn2;
Mark60:;
		// continue
		if (_b5p6od9s >= (_jhtbcevs + (int)1))
		{
			
			if (_b5p6od9s == (_jhtbcevs + (int)1))
			{
				
				*(_o2dp5e8w+((_87d05vk3 + _b5p6od9s) - (int)1 - 1)) = *(_2ivtt43r+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r));
				*(_sgos5dql+((_87d05vk3 + _b5p6od9s) - (int)1 - 1)) = _d0547bi2;
				_b5p6od9s = (_b5p6od9s - (int)1);
			}
			else
			if (*(_2ivtt43r+(_b5p6od9s - 1) + (_b5p6od9s - (int)1 - 1) * 1 * (_w8yhbr2r)) == _d0547bi2)
			{
				
				*(_o2dp5e8w+((_87d05vk3 + _b5p6od9s) - (int)1 - 1)) = *(_2ivtt43r+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r));
				*(_sgos5dql+((_87d05vk3 + _b5p6od9s) - (int)1 - 1)) = _d0547bi2;
				_b5p6od9s = (_b5p6od9s - (int)1);
			}
			else
			{
				
				_zwm0s9sq = *(_2ivtt43r+(_b5p6od9s - (int)1 - 1) + (_b5p6od9s - (int)1 - 1) * 1 * (_w8yhbr2r));
				_985e9e9b = *(_2ivtt43r+(_b5p6od9s - 1) + (_b5p6od9s - (int)1 - 1) * 1 * (_w8yhbr2r));
				_uv0s0qmf = *(_2ivtt43r+(_b5p6od9s - (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r));
				_f4rvsg6o = *(_2ivtt43r+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r));
				_ticz2j2b(ref _zwm0s9sq ,ref _uv0s0qmf ,ref _985e9e9b ,ref _f4rvsg6o ,ref Unsafe.AsRef(*(_o2dp5e8w+((_87d05vk3 + _b5p6od9s) - (int)2 - 1))) ,ref Unsafe.AsRef(*(_sgos5dql+((_87d05vk3 + _b5p6od9s) - (int)2 - 1))) ,ref Unsafe.AsRef(*(_o2dp5e8w+((_87d05vk3 + _b5p6od9s) - (int)1 - 1))) ,ref Unsafe.AsRef(*(_sgos5dql+((_87d05vk3 + _b5p6od9s) - (int)1 - 1))) ,ref _82tpdhyl ,ref _8tmd0ner );
				_b5p6od9s = (_b5p6od9s - (int)2);
			}
			goto Mark60;
		}
		//* 
		
		if ((_edl2gwc7 < _msaexyn2) | (_irk8i6qr == _d0547bi2))
		{
			
			if ((_edl2gwc7 > (int)1) & (_irk8i6qr != _d0547bi2))
			{
				//* 
				//*           ==== Reflect spike back into lower triangle ==== 
				//* 
				
				_wcs7ne88(ref _edl2gwc7 ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref Unsafe.AsRef((int)1) );
				_bafcbx97 = *(_apig8meb+((int)1 - 1));
				_mbabw0s0(ref _edl2gwc7 ,ref _bafcbx97 ,(_apig8meb+((int)2 - 1)),ref Unsafe.AsRef((int)1) ,ref _0446f4de );
				*(_apig8meb+((int)1 - 1)) = _kxg5drh2;//* 
				
				_t013e1c8("L" ,ref Unsafe.AsRef(_msaexyn2 - (int)2) ,ref Unsafe.AsRef(_msaexyn2 - (int)2) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_2ivtt43r+((int)3 - 1) + ((int)1 - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r );//* 
				
				_tfywat2m("L" ,ref _edl2gwc7 ,ref _msaexyn2 ,_apig8meb ,ref Unsafe.AsRef((int)1) ,ref _0446f4de ,_2ivtt43r ,ref _w8yhbr2r ,(_apig8meb+(_msaexyn2 + (int)1 - 1)));
				_tfywat2m("R" ,ref _edl2gwc7 ,ref _edl2gwc7 ,_apig8meb ,ref Unsafe.AsRef((int)1) ,ref _0446f4de ,_2ivtt43r ,ref _w8yhbr2r ,(_apig8meb+(_msaexyn2 + (int)1 - 1)));
				_tfywat2m("R" ,ref _msaexyn2 ,ref _edl2gwc7 ,_apig8meb ,ref Unsafe.AsRef((int)1) ,ref _0446f4de ,_ycxba85s ,ref _ys09rxze ,(_apig8meb+(_msaexyn2 + (int)1 - 1)));//* 
				
				_jgi8r1sb(ref _msaexyn2 ,ref Unsafe.AsRef((int)1) ,ref _edl2gwc7 ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,(_apig8meb+(_msaexyn2 + (int)1 - 1)),ref Unsafe.AsRef(_6fnxzlyp - _msaexyn2) ,ref _gro5yvfo );
			}
			//* 
			//*        ==== Copy updated reduced window into place ==== 
			//* 
			
			if (_87d05vk3 > (int)1)
			*(_ogkjl6gu+(_87d05vk3 - 1) + (_87d05vk3 - (int)1 - 1) * 1 * (_1iekxpnw)) = (_irk8i6qr * *(_ycxba85s+((int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)));
			_m38y8dyg("U" ,ref _msaexyn2 ,ref _msaexyn2 ,_2ivtt43r ,ref _w8yhbr2r ,(_ogkjl6gu+(_87d05vk3 - 1) + (_87d05vk3 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw );
			_wcs7ne88(ref Unsafe.AsRef(_msaexyn2 - (int)1) ,(_2ivtt43r+((int)2 - 1) + ((int)1 - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef(_w8yhbr2r + (int)1) ,(_ogkjl6gu+(_87d05vk3 + (int)1 - 1) + (_87d05vk3 - 1) * 1 * (_1iekxpnw)),ref Unsafe.AsRef(_1iekxpnw + (int)1) );//* 
			//*        ==== Accumulate orthogonal matrix in order update 
			//*        .    H and Z, if requested.  ==== 
			//* 
			
			if ((_edl2gwc7 > (int)1) & (_irk8i6qr != _d0547bi2))
			_68lkolob("R" ,"N" ,ref _msaexyn2 ,ref _edl2gwc7 ,ref Unsafe.AsRef((int)1) ,ref _edl2gwc7 ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,_ycxba85s ,ref _ys09rxze ,(_apig8meb+(_msaexyn2 + (int)1 - 1)),ref Unsafe.AsRef(_6fnxzlyp - _msaexyn2) ,ref _gro5yvfo );//* 
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
				System.Int32 __81fgg2dlsvn2380 = (System.Int32)(_y834yh01);
				System.Int32 __81fgg2step2380 = (System.Int32)(_aiz0v1d1);
				System.Int32 __81fgg2count2380;
				for (__81fgg2count2380 = System.Math.Max(0, (System.Int32)(((System.Int32)(_87d05vk3 - (int)1) - __81fgg2dlsvn2380 + __81fgg2step2380) / __81fgg2step2380)), _pnhq01yl = __81fgg2dlsvn2380; __81fgg2count2380 != 0; __81fgg2count2380--, _pnhq01yl += (__81fgg2step2380)) {

				{
					
					_m4jdywnh = ILNumerics.F2NET.Intrinsics.MIN(_aiz0v1d1 ,_87d05vk3 - _pnhq01yl );
					_b8wa9454("N" ,"N" ,ref _m4jdywnh ,ref _msaexyn2 ,ref _msaexyn2 ,ref Unsafe.AsRef(_kxg5drh2) ,(_ogkjl6gu+(_pnhq01yl - 1) + (_87d05vk3 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_d0547bi2) ,_pm308b4n ,ref _vlfzpxiy );
					_m38y8dyg("A" ,ref _m4jdywnh ,ref _msaexyn2 ,_pm308b4n ,ref _vlfzpxiy ,(_ogkjl6gu+(_pnhq01yl - 1) + (_87d05vk3 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw );
Mark70:;
					// continue
				}
								}			}//* 
			//*        ==== Update horizontal slab in H ==== 
			//* 
			
			if (_amt8y1zm)
			{
				
				{
					System.Int32 __81fgg2dlsvn2381 = (System.Int32)((_7d8gh478 + (int)1));
					System.Int32 __81fgg2step2381 = (System.Int32)(_aym8a085);
					System.Int32 __81fgg2count2381;
					for (__81fgg2count2381 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2381 + __81fgg2step2381) / __81fgg2step2381)), _chb1oswp = __81fgg2dlsvn2381; __81fgg2count2381 != 0; __81fgg2count2381--, _chb1oswp += (__81fgg2step2381)) {

					{
						
						_m4jdywnh = ILNumerics.F2NET.Intrinsics.MIN(_aym8a085 ,(_dxpq0xkr - _chb1oswp) + (int)1 );
						_b8wa9454("C" ,"N" ,ref _msaexyn2 ,ref _m4jdywnh ,ref _msaexyn2 ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,(_ogkjl6gu+(_87d05vk3 - 1) + (_chb1oswp - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,ref Unsafe.AsRef(_d0547bi2) ,_2ivtt43r ,ref _w8yhbr2r );
						_m38y8dyg("A" ,ref _msaexyn2 ,ref _m4jdywnh ,_2ivtt43r ,ref _w8yhbr2r ,(_ogkjl6gu+(_87d05vk3 - 1) + (_chb1oswp - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw );
Mark80:;
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
					System.Int32 __81fgg2dlsvn2382 = (System.Int32)(_pinc1ofz);
					System.Int32 __81fgg2step2382 = (System.Int32)(_aiz0v1d1);
					System.Int32 __81fgg2count2382;
					for (__81fgg2count2382 = System.Math.Max(0, (System.Int32)(((System.Int32)(_mg9v9w4h) - __81fgg2dlsvn2382 + __81fgg2step2382) / __81fgg2step2382)), _pnhq01yl = __81fgg2dlsvn2382; __81fgg2count2382 != 0; __81fgg2count2382--, _pnhq01yl += (__81fgg2step2382)) {

					{
						
						_m4jdywnh = ILNumerics.F2NET.Intrinsics.MIN(_aiz0v1d1 ,(_mg9v9w4h - _pnhq01yl) + (int)1 );
						_b8wa9454("N" ,"N" ,ref _m4jdywnh ,ref _msaexyn2 ,ref _msaexyn2 ,ref Unsafe.AsRef(_kxg5drh2) ,(_7e60fcso+(_pnhq01yl - 1) + (_87d05vk3 - 1) * 1 * (_5l1tna8s)),ref _5l1tna8s ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_d0547bi2) ,_pm308b4n ,ref _vlfzpxiy );
						_m38y8dyg("A" ,ref _m4jdywnh ,ref _msaexyn2 ,_pm308b4n ,ref _vlfzpxiy ,(_7e60fcso+(_pnhq01yl - 1) + (_87d05vk3 - 1) * 1 * (_5l1tna8s)),ref _5l1tna8s );
Mark90:;
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
		
		*(_apig8meb+((int)1 - 1)) = ILNumerics.F2NET.Intrinsics.REAL(_e4ueamrn );//* 
		//*     ==== End of SLAQR3 ==== 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
