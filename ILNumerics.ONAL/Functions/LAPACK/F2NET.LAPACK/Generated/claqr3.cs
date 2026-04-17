
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
//*> \brief \b CLAQR3 performs the unitary similarity transformation of a Hessenberg matrix to detect and deflate fully converged eigenvalues from a trailing principal submatrix (aggressive early deflation). 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CLAQR3 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/claqr3.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/claqr3.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/claqr3.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CLAQR3( WANTT, WANTZ, N, KTOP, KBOT, NW, H, LDH, ILOZ, 
//*                          IHIZ, Z, LDZ, NS, ND, SH, V, LDV, NH, T, LDT, 
//*                          NV, WV, LDWV, WORK, LWORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            IHIZ, ILOZ, KBOT, KTOP, LDH, LDT, LDV, LDWV, 
//*      $                   LDZ, LWORK, N, ND, NH, NS, NV, NW 
//*       LOGICAL            WANTT, WANTZ 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX            H( LDH, * ), SH( * ), T( LDT, * ), V( LDV, * ), 
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
//*>    CLAQR3 accepts as input an upper Hessenberg matrix 
//*>    H and performs an unitary similarity transformation 
//*>    designed to detect and deflate fully converged eigenvalues from 
//*>    a trailing principal submatrix.  On output H has been over- 
//*>    written by a new Hessenberg matrix that is a perturbation of 
//*>    an unitary similarity transformation of H.  It is to be 
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
//*>          H is COMPLEX array, dimension (LDH,N) 
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
//*>          Z is COMPLEX array, dimension (LDZ,N) 
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
//*>          SH is COMPLEX array, dimension (KBOT) 
//*>          On output, approximate eigenvalues that may 
//*>          be used for shifts are stored in SH(KBOT-ND-NS+1) 
//*>          through SR(KBOT-ND).  Converged eigenvalues are 
//*>          stored in SH(KBOT-ND+1) through SH(KBOT). 
//*> \endverbatim 
//*> 
//*> \param[out] V 
//*> \verbatim 
//*>          V is COMPLEX array, dimension (LDV,NW) 
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
//*>          T is COMPLEX array, dimension (LDT,NW) 
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
//*>          WV is COMPLEX array, dimension (LDWV,NW) 
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
//*>          WORK is COMPLEX array, dimension (LWORK) 
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
//*>          If LWORK = -1, then a workspace query is assumed; CLAQR3 
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
//*> \ingroup complexOTHERauxiliary 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>       Karen Braman and Ralph Byers, Department of Mathematics, 
//*>       University of Kansas, USA 
//*> 
//*  ===================================================================== 

	 
	public static void _4n8jnp8p(ref Boolean _amt8y1zm, ref Boolean _189gzykk, ref Int32 _dxpq0xkr, ref Int32 _4a77vvpa, ref Int32 _7d8gh478, ref Int32 _w6pmxgch, fcomplex* _ogkjl6gu, ref Int32 _1iekxpnw, ref Int32 _pinc1ofz, ref Int32 _mg9v9w4h, fcomplex* _7e60fcso, ref Int32 _5l1tna8s, ref Int32 _edl2gwc7, ref Int32 _rwm6akyl, fcomplex* _9tenoh4m, fcomplex* _ycxba85s, ref Int32 _ys09rxze, ref Int32 _aym8a085, fcomplex* _2ivtt43r, ref Int32 _w8yhbr2r, ref Int32 _aiz0v1d1, fcomplex* _pm308b4n, ref Int32 _vlfzpxiy, fcomplex* _apig8meb, ref Int32 _6fnxzlyp)
	{
#region variable declarations
fcomplex _d0547bi2 =   new fcomplex(0f,0f);
fcomplex _kxg5drh2 =   new fcomplex(1f,0f);
Single _7cb2gzzt =  0f;
Single _zfxjga59 =  1f;
fcomplex _bafcbx97 =  default;
fcomplex _n7plx4io =  default;
fcomplex _irk8i6qr =  default;
fcomplex _0446f4de =  default;
Single _yl9tlixq =  default;
Single _odf6ja0t =  default;
Single _h75qnr7l =  default;
Single _bogm0gwy =  default;
Single _0h4yb5wu =  default;
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
		
		
		Func<fcomplex,Single> _4jqx89by = (_a94616nn) => { return (ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.REAL(_a94616nn ) ) + ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.AIMAG(_a94616nn ) )); };;//*     .. 
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
			//*        ==== Workspace query call to CGEHRD ==== 
			//* 
			
			_fhvkihza(ref _msaexyn2 ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_msaexyn2 - (int)1) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,_apig8meb ,ref Unsafe.AsRef((int)-1) ,ref _gro5yvfo );
			_ve9qn4fh = ILNumerics.F2NET.Intrinsics.INT(*(_apig8meb+((int)1 - 1)) );//* 
			//*        ==== Workspace query call to CUNMHR ==== 
			//* 
			
			_s2mzx69z("R" ,"N" ,ref _msaexyn2 ,ref _msaexyn2 ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_msaexyn2 - (int)1) ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref Unsafe.AsRef((int)-1) ,ref _gro5yvfo );
			_7c13pjdp = ILNumerics.F2NET.Intrinsics.INT(*(_apig8meb+((int)1 - 1)) );//* 
			//*        ==== Workspace query call to CLAQR4 ==== 
			//* 
			
			_zusop5je(ref Unsafe.AsRef(true) ,ref Unsafe.AsRef(true) ,ref _msaexyn2 ,ref Unsafe.AsRef((int)1) ,ref _msaexyn2 ,_2ivtt43r ,ref _w8yhbr2r ,_9tenoh4m ,ref Unsafe.AsRef((int)1) ,ref _msaexyn2 ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref Unsafe.AsRef((int)-1) ,ref _jhtbcevs );
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
			
			*(_apig8meb+((int)1 - 1)) = ILNumerics.F2NET.Intrinsics.CMPLX(_e4ueamrn ,(int)0 );
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
		_odf6ja0t = (_zfxjga59 / _h75qnr7l);
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
		
		_szaic8qw("U" ,ref _msaexyn2 ,ref _msaexyn2 ,(_ogkjl6gu+(_87d05vk3 - 1) + (_87d05vk3 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,_2ivtt43r ,ref _w8yhbr2r );
		_33e0jk6i(ref Unsafe.AsRef(_msaexyn2 - (int)1) ,(_ogkjl6gu+(_87d05vk3 + (int)1 - 1) + (_87d05vk3 - 1) * 1 * (_1iekxpnw)),ref Unsafe.AsRef(_1iekxpnw + (int)1) ,(_2ivtt43r+((int)2 - 1) + ((int)1 - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef(_w8yhbr2r + (int)1) );//* 
		
		_663dvznc("A" ,ref _msaexyn2 ,ref _msaexyn2 ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze );
		_rs56fkjq = _4mvd6e4d(ref Unsafe.AsRef((int)12) ,"CLAQR3" ,"SV" ,ref _msaexyn2 ,ref Unsafe.AsRef((int)1) ,ref _msaexyn2 ,ref _6fnxzlyp );
		if (_msaexyn2 > _rs56fkjq)
		{
			
			_zusop5je(ref Unsafe.AsRef(true) ,ref Unsafe.AsRef(true) ,ref _msaexyn2 ,ref Unsafe.AsRef((int)1) ,ref _msaexyn2 ,_2ivtt43r ,ref _w8yhbr2r ,(_9tenoh4m+(_87d05vk3 - 1)),ref Unsafe.AsRef((int)1) ,ref _msaexyn2 ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref _6fnxzlyp ,ref _jhtbcevs );
		}
		else
		{
			
			_b6ujfmgo(ref Unsafe.AsRef(true) ,ref Unsafe.AsRef(true) ,ref _msaexyn2 ,ref Unsafe.AsRef((int)1) ,ref _msaexyn2 ,_2ivtt43r ,ref _w8yhbr2r ,(_9tenoh4m+(_87d05vk3 - 1)),ref Unsafe.AsRef((int)1) ,ref _msaexyn2 ,_ycxba85s ,ref _ys09rxze ,ref _jhtbcevs );
		}
		//* 
		//*     ==== Deflation detection loop ==== 
		//* 
		
		_edl2gwc7 = _msaexyn2;
		_ab05c09e = (_jhtbcevs + (int)1);
		{
			System.Int32 __81fgg2dlsvn2530 = (System.Int32)((_jhtbcevs + (int)1));
			const System.Int32 __81fgg2step2530 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2530;
			for (__81fgg2count2530 = System.Math.Max(0, (System.Int32)(((System.Int32)(_msaexyn2) - __81fgg2dlsvn2530 + __81fgg2step2530) / __81fgg2step2530)), _hcqqzxmr = __81fgg2dlsvn2530; __81fgg2count2530 != 0; __81fgg2count2530--, _hcqqzxmr += (__81fgg2step2530)) {

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
					//*           .    way.   (CTREXC can not fail in this case.) ==== 
					//* 
					
					_la6t805m = _edl2gwc7;
					_r10bvo9j("V" ,ref _msaexyn2 ,_2ivtt43r ,ref _w8yhbr2r ,_ycxba85s ,ref _ys09rxze ,ref _la6t805m ,ref _ab05c09e ,ref _gro5yvfo );
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
				System.Int32 __81fgg2dlsvn2531 = (System.Int32)((_jhtbcevs + (int)1));
				const System.Int32 __81fgg2step2531 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2531;
				for (__81fgg2count2531 = System.Math.Max(0, (System.Int32)(((System.Int32)(_edl2gwc7) - __81fgg2dlsvn2531 + __81fgg2step2531) / __81fgg2step2531)), _b5p6od9s = __81fgg2dlsvn2531; __81fgg2count2531 != 0; __81fgg2count2531--, _b5p6od9s += (__81fgg2step2531)) {

				{
					
					_la6t805m = _b5p6od9s;
					{
						System.Int32 __81fgg2dlsvn2532 = (System.Int32)((_b5p6od9s + (int)1));
						const System.Int32 __81fgg2step2532 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2532;
						for (__81fgg2count2532 = System.Math.Max(0, (System.Int32)(((System.Int32)(_edl2gwc7) - __81fgg2dlsvn2532 + __81fgg2step2532) / __81fgg2step2532)), _znpjgsef = __81fgg2dlsvn2532; __81fgg2count2532 != 0; __81fgg2count2532--, _znpjgsef += (__81fgg2step2532)) {

						{
							
							if (_4jqx89by(*(_2ivtt43r+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)) ) > _4jqx89by(*(_2ivtt43r+(_la6t805m - 1) + (_la6t805m - 1) * 1 * (_w8yhbr2r)) ))
							_la6t805m = _znpjgsef;
Mark20:;
							// continue
						}
												}					}
					_ab05c09e = _b5p6od9s;
					if (_la6t805m != _ab05c09e)
					_r10bvo9j("V" ,ref _msaexyn2 ,_2ivtt43r ,ref _w8yhbr2r ,_ycxba85s ,ref _ys09rxze ,ref _la6t805m ,ref _ab05c09e ,ref _gro5yvfo );
Mark30:;
					// continue
				}
								}			}
		}
		//* 
		//*     ==== Restore shift/eigenvalue array from T ==== 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn2533 = (System.Int32)((_jhtbcevs + (int)1));
			const System.Int32 __81fgg2step2533 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2533;
			for (__81fgg2count2533 = System.Math.Max(0, (System.Int32)(((System.Int32)(_msaexyn2) - __81fgg2dlsvn2533 + __81fgg2step2533) / __81fgg2step2533)), _b5p6od9s = __81fgg2dlsvn2533; __81fgg2count2533 != 0; __81fgg2count2533--, _b5p6od9s += (__81fgg2step2533)) {

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
				
				_33e0jk6i(ref _edl2gwc7 ,_ycxba85s ,ref _ys09rxze ,_apig8meb ,ref Unsafe.AsRef((int)1) );
				{
					System.Int32 __81fgg2dlsvn2534 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2534 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2534;
					for (__81fgg2count2534 = System.Math.Max(0, (System.Int32)(((System.Int32)(_edl2gwc7) - __81fgg2dlsvn2534 + __81fgg2step2534) / __81fgg2step2534)), _b5p6od9s = __81fgg2dlsvn2534; __81fgg2count2534 != 0; __81fgg2count2534--, _b5p6od9s += (__81fgg2step2534)) {

					{
						
						*(_apig8meb+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.CONJG(*(_apig8meb+(_b5p6od9s - 1)) );
Mark50:;
						// continue
					}
										}				}
				_bafcbx97 = *(_apig8meb+((int)1 - 1));
				_ocp87dc1(ref _edl2gwc7 ,ref _bafcbx97 ,(_apig8meb+((int)2 - 1)),ref Unsafe.AsRef((int)1) ,ref _0446f4de );
				*(_apig8meb+((int)1 - 1)) = _kxg5drh2;//* 
				
				_663dvznc("L" ,ref Unsafe.AsRef(_msaexyn2 - (int)2) ,ref Unsafe.AsRef(_msaexyn2 - (int)2) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_2ivtt43r+((int)3 - 1) + ((int)1 - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r );//* 
				
				_ok06eljh("L" ,ref _edl2gwc7 ,ref _msaexyn2 ,_apig8meb ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.CONJG(_0446f4de )) ,_2ivtt43r ,ref _w8yhbr2r ,(_apig8meb+(_msaexyn2 + (int)1 - 1)));
				_ok06eljh("R" ,ref _edl2gwc7 ,ref _edl2gwc7 ,_apig8meb ,ref Unsafe.AsRef((int)1) ,ref _0446f4de ,_2ivtt43r ,ref _w8yhbr2r ,(_apig8meb+(_msaexyn2 + (int)1 - 1)));
				_ok06eljh("R" ,ref _msaexyn2 ,ref _edl2gwc7 ,_apig8meb ,ref Unsafe.AsRef((int)1) ,ref _0446f4de ,_ycxba85s ,ref _ys09rxze ,(_apig8meb+(_msaexyn2 + (int)1 - 1)));//* 
				
				_fhvkihza(ref _msaexyn2 ,ref Unsafe.AsRef((int)1) ,ref _edl2gwc7 ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,(_apig8meb+(_msaexyn2 + (int)1 - 1)),ref Unsafe.AsRef(_6fnxzlyp - _msaexyn2) ,ref _gro5yvfo );
			}
			//* 
			//*        ==== Copy updated reduced window into place ==== 
			//* 
			
			if (_87d05vk3 > (int)1)
			*(_ogkjl6gu+(_87d05vk3 - 1) + (_87d05vk3 - (int)1 - 1) * 1 * (_1iekxpnw)) = (_irk8i6qr * ILNumerics.F2NET.Intrinsics.CONJG(*(_ycxba85s+((int)1 - 1) + ((int)1 - 1) * 1 * (_ys09rxze)) ));
			_szaic8qw("U" ,ref _msaexyn2 ,ref _msaexyn2 ,_2ivtt43r ,ref _w8yhbr2r ,(_ogkjl6gu+(_87d05vk3 - 1) + (_87d05vk3 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw );
			_33e0jk6i(ref Unsafe.AsRef(_msaexyn2 - (int)1) ,(_2ivtt43r+((int)2 - 1) + ((int)1 - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef(_w8yhbr2r + (int)1) ,(_ogkjl6gu+(_87d05vk3 + (int)1 - 1) + (_87d05vk3 - 1) * 1 * (_1iekxpnw)),ref Unsafe.AsRef(_1iekxpnw + (int)1) );//* 
			//*        ==== Accumulate orthogonal matrix in order update 
			//*        .    H and Z, if requested.  ==== 
			//* 
			
			if ((_edl2gwc7 > (int)1) & (_irk8i6qr != _d0547bi2))
			_s2mzx69z("R" ,"N" ,ref _msaexyn2 ,ref _edl2gwc7 ,ref Unsafe.AsRef((int)1) ,ref _edl2gwc7 ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,_ycxba85s ,ref _ys09rxze ,(_apig8meb+(_msaexyn2 + (int)1 - 1)),ref Unsafe.AsRef(_6fnxzlyp - _msaexyn2) ,ref _gro5yvfo );//* 
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
				System.Int32 __81fgg2dlsvn2535 = (System.Int32)(_y834yh01);
				System.Int32 __81fgg2step2535 = (System.Int32)(_aiz0v1d1);
				System.Int32 __81fgg2count2535;
				for (__81fgg2count2535 = System.Math.Max(0, (System.Int32)(((System.Int32)(_87d05vk3 - (int)1) - __81fgg2dlsvn2535 + __81fgg2step2535) / __81fgg2step2535)), _pnhq01yl = __81fgg2dlsvn2535; __81fgg2count2535 != 0; __81fgg2count2535--, _pnhq01yl += (__81fgg2step2535)) {

				{
					
					_m4jdywnh = ILNumerics.F2NET.Intrinsics.MIN(_aiz0v1d1 ,_87d05vk3 - _pnhq01yl );
					_5p0w9905("N" ,"N" ,ref _m4jdywnh ,ref _msaexyn2 ,ref _msaexyn2 ,ref Unsafe.AsRef(_kxg5drh2) ,(_ogkjl6gu+(_pnhq01yl - 1) + (_87d05vk3 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_d0547bi2) ,_pm308b4n ,ref _vlfzpxiy );
					_szaic8qw("A" ,ref _m4jdywnh ,ref _msaexyn2 ,_pm308b4n ,ref _vlfzpxiy ,(_ogkjl6gu+(_pnhq01yl - 1) + (_87d05vk3 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw );
Mark60:;
					// continue
				}
								}			}//* 
			//*        ==== Update horizontal slab in H ==== 
			//* 
			
			if (_amt8y1zm)
			{
				
				{
					System.Int32 __81fgg2dlsvn2536 = (System.Int32)((_7d8gh478 + (int)1));
					System.Int32 __81fgg2step2536 = (System.Int32)(_aym8a085);
					System.Int32 __81fgg2count2536;
					for (__81fgg2count2536 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2536 + __81fgg2step2536) / __81fgg2step2536)), _chb1oswp = __81fgg2dlsvn2536; __81fgg2count2536 != 0; __81fgg2count2536--, _chb1oswp += (__81fgg2step2536)) {

					{
						
						_m4jdywnh = ILNumerics.F2NET.Intrinsics.MIN(_aym8a085 ,(_dxpq0xkr - _chb1oswp) + (int)1 );
						_5p0w9905("C" ,"N" ,ref _msaexyn2 ,ref _m4jdywnh ,ref _msaexyn2 ,ref Unsafe.AsRef(_kxg5drh2) ,_ycxba85s ,ref _ys09rxze ,(_ogkjl6gu+(_87d05vk3 - 1) + (_chb1oswp - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,ref Unsafe.AsRef(_d0547bi2) ,_2ivtt43r ,ref _w8yhbr2r );
						_szaic8qw("A" ,ref _msaexyn2 ,ref _m4jdywnh ,_2ivtt43r ,ref _w8yhbr2r ,(_ogkjl6gu+(_87d05vk3 - 1) + (_chb1oswp - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw );
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
					System.Int32 __81fgg2dlsvn2537 = (System.Int32)(_pinc1ofz);
					System.Int32 __81fgg2step2537 = (System.Int32)(_aiz0v1d1);
					System.Int32 __81fgg2count2537;
					for (__81fgg2count2537 = System.Math.Max(0, (System.Int32)(((System.Int32)(_mg9v9w4h) - __81fgg2dlsvn2537 + __81fgg2step2537) / __81fgg2step2537)), _pnhq01yl = __81fgg2dlsvn2537; __81fgg2count2537 != 0; __81fgg2count2537--, _pnhq01yl += (__81fgg2step2537)) {

					{
						
						_m4jdywnh = ILNumerics.F2NET.Intrinsics.MIN(_aiz0v1d1 ,(_mg9v9w4h - _pnhq01yl) + (int)1 );
						_5p0w9905("N" ,"N" ,ref _m4jdywnh ,ref _msaexyn2 ,ref _msaexyn2 ,ref Unsafe.AsRef(_kxg5drh2) ,(_7e60fcso+(_pnhq01yl - 1) + (_87d05vk3 - 1) * 1 * (_5l1tna8s)),ref _5l1tna8s ,_ycxba85s ,ref _ys09rxze ,ref Unsafe.AsRef(_d0547bi2) ,_pm308b4n ,ref _vlfzpxiy );
						_szaic8qw("A" ,ref _m4jdywnh ,ref _msaexyn2 ,_pm308b4n ,ref _vlfzpxiy ,(_7e60fcso+(_pnhq01yl - 1) + (_87d05vk3 - 1) * 1 * (_5l1tna8s)),ref _5l1tna8s );
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
		
		*(_apig8meb+((int)1 - 1)) = ILNumerics.F2NET.Intrinsics.CMPLX(_e4ueamrn ,(int)0 );//* 
		//*     ==== End of CLAQR3 ==== 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
