
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
//*> \brief \b SLAQR5 performs a single small-bulge multi-shift QR sweep. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLAQR5 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slaqr5.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slaqr5.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slaqr5.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLAQR5( WANTT, WANTZ, KACC22, N, KTOP, KBOT, NSHFTS, 
//*                          SR, SI, H, LDH, ILOZ, IHIZ, Z, LDZ, V, LDV, U, 
//*                          LDU, NV, WV, LDWV, NH, WH, LDWH ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            IHIZ, ILOZ, KACC22, KBOT, KTOP, LDH, LDU, LDV, 
//*      $                   LDWH, LDWV, LDZ, N, NH, NSHFTS, NV 
//*       LOGICAL            WANTT, WANTZ 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               H( LDH, * ), SI( * ), SR( * ), U( LDU, * ), 
//*      $                   V( LDV, * ), WH( LDWH, * ), WV( LDWV, * ), 
//*      $                   Z( LDZ, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*>    SLAQR5, called by SLAQR0, performs a 
//*>    single small-bulge multi-shift QR sweep. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] WANTT 
//*> \verbatim 
//*>          WANTT is LOGICAL 
//*>             WANTT = .true. if the quasi-triangular Schur factor 
//*>             is being computed.  WANTT is set to .false. otherwise. 
//*> \endverbatim 
//*> 
//*> \param[in] WANTZ 
//*> \verbatim 
//*>          WANTZ is LOGICAL 
//*>             WANTZ = .true. if the orthogonal Schur factor is being 
//*>             computed.  WANTZ is set to .false. otherwise. 
//*> \endverbatim 
//*> 
//*> \param[in] KACC22 
//*> \verbatim 
//*>          KACC22 is INTEGER with value 0, 1, or 2. 
//*>             Specifies the computation mode of far-from-diagonal 
//*>             orthogonal updates. 
//*>        = 0: SLAQR5 does not accumulate reflections and does not 
//*>             use matrix-matrix multiply to update far-from-diagonal 
//*>             matrix entries. 
//*>        = 1: SLAQR5 accumulates reflections and uses matrix-matrix 
//*>             multiply to update the far-from-diagonal matrix entries. 
//*>        = 2: SLAQR5 accumulates reflections, uses matrix-matrix 
//*>             multiply to update the far-from-diagonal matrix entries, 
//*>             and takes advantage of 2-by-2 block structure during 
//*>             matrix multiplies. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>             N is the order of the Hessenberg matrix H upon which this 
//*>             subroutine operates. 
//*> \endverbatim 
//*> 
//*> \param[in] KTOP 
//*> \verbatim 
//*>          KTOP is INTEGER 
//*> \endverbatim 
//*> 
//*> \param[in] KBOT 
//*> \verbatim 
//*>          KBOT is INTEGER 
//*>             These are the first and last rows and columns of an 
//*>             isolated diagonal block upon which the QR sweep is to be 
//*>             applied. It is assumed without a check that 
//*>                       either KTOP = 1  or   H(KTOP,KTOP-1) = 0 
//*>             and 
//*>                       either KBOT = N  or   H(KBOT+1,KBOT) = 0. 
//*> \endverbatim 
//*> 
//*> \param[in] NSHFTS 
//*> \verbatim 
//*>          NSHFTS is INTEGER 
//*>             NSHFTS gives the number of simultaneous shifts.  NSHFTS 
//*>             must be positive and even. 
//*> \endverbatim 
//*> 
//*> \param[in,out] SR 
//*> \verbatim 
//*>          SR is REAL array, dimension (NSHFTS) 
//*> \endverbatim 
//*> 
//*> \param[in,out] SI 
//*> \verbatim 
//*>          SI is REAL array, dimension (NSHFTS) 
//*>             SR contains the real parts and SI contains the imaginary 
//*>             parts of the NSHFTS shifts of origin that define the 
//*>             multi-shift QR sweep.  On output SR and SI may be 
//*>             reordered. 
//*> \endverbatim 
//*> 
//*> \param[in,out] H 
//*> \verbatim 
//*>          H is REAL array, dimension (LDH,N) 
//*>             On input H contains a Hessenberg matrix.  On output a 
//*>             multi-shift QR sweep with shifts SR(J)+i*SI(J) is applied 
//*>             to the isolated diagonal block in rows and columns KTOP 
//*>             through KBOT. 
//*> \endverbatim 
//*> 
//*> \param[in] LDH 
//*> \verbatim 
//*>          LDH is INTEGER 
//*>             LDH is the leading dimension of H just as declared in the 
//*>             calling procedure.  LDH >= MAX(1,N). 
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
//*>             Specify the rows of Z to which transformations must be 
//*>             applied if WANTZ is .TRUE.. 1 <= ILOZ <= IHIZ <= N 
//*> \endverbatim 
//*> 
//*> \param[in,out] Z 
//*> \verbatim 
//*>          Z is REAL array, dimension (LDZ,IHIZ) 
//*>             If WANTZ = .TRUE., then the QR Sweep orthogonal 
//*>             similarity transformation is accumulated into 
//*>             Z(ILOZ:IHIZ,ILOZ:IHIZ) from the right. 
//*>             If WANTZ = .FALSE., then Z is unreferenced. 
//*> \endverbatim 
//*> 
//*> \param[in] LDZ 
//*> \verbatim 
//*>          LDZ is INTEGER 
//*>             LDA is the leading dimension of Z just as declared in 
//*>             the calling procedure. LDZ >= N. 
//*> \endverbatim 
//*> 
//*> \param[out] V 
//*> \verbatim 
//*>          V is REAL array, dimension (LDV,NSHFTS/2) 
//*> \endverbatim 
//*> 
//*> \param[in] LDV 
//*> \verbatim 
//*>          LDV is INTEGER 
//*>             LDV is the leading dimension of V as declared in the 
//*>             calling procedure.  LDV >= 3. 
//*> \endverbatim 
//*> 
//*> \param[out] U 
//*> \verbatim 
//*>          U is REAL array, dimension (LDU,3*NSHFTS-3) 
//*> \endverbatim 
//*> 
//*> \param[in] LDU 
//*> \verbatim 
//*>          LDU is INTEGER 
//*>             LDU is the leading dimension of U just as declared in the 
//*>             in the calling subroutine.  LDU >= 3*NSHFTS-3. 
//*> \endverbatim 
//*> 
//*> \param[in] NV 
//*> \verbatim 
//*>          NV is INTEGER 
//*>             NV is the number of rows in WV agailable for workspace. 
//*>             NV >= 1. 
//*> \endverbatim 
//*> 
//*> \param[out] WV 
//*> \verbatim 
//*>          WV is REAL array, dimension (LDWV,3*NSHFTS-3) 
//*> \endverbatim 
//*> 
//*> \param[in] LDWV 
//*> \verbatim 
//*>          LDWV is INTEGER 
//*>             LDWV is the leading dimension of WV as declared in the 
//*>             in the calling subroutine.  LDWV >= NV. 
//*> \endverbatim 
//* 
//*> \param[in] NH 
//*> \verbatim 
//*>          NH is INTEGER 
//*>             NH is the number of columns in array WH available for 
//*>             workspace. NH >= 1. 
//*> \endverbatim 
//*> 
//*> \param[out] WH 
//*> \verbatim 
//*>          WH is REAL array, dimension (LDWH,NH) 
//*> \endverbatim 
//*> 
//*> \param[in] LDWH 
//*> \verbatim 
//*>          LDWH is INTEGER 
//*>             Leading dimension of WH just as declared in the 
//*>             calling procedure.  LDWH >= 3*NSHFTS-3. 
//*> \endverbatim 
//*> 
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
//* 
//*> \par References: 
//*  ================ 
//*> 
//*>       K. Braman, R. Byers and R. Mathias, The Multi-Shift QR 
//*>       Algorithm Part I: Maintaining Well Focused Shifts, and Level 3 
//*>       Performance, SIAM Journal of Matrix Analysis, volume 23, pages 
//*>       929--947, 2002. 
//*> 
//*  ===================================================================== 

	 
	public static void _ud1qriia(ref Boolean _amt8y1zm, ref Boolean _189gzykk, ref Int32 _v32676ye, ref Int32 _dxpq0xkr, ref Int32 _4a77vvpa, ref Int32 _7d8gh478, ref Int32 _rsjf6fvx, Single* _o2dp5e8w, Single* _sgos5dql, Single* _ogkjl6gu, ref Int32 _1iekxpnw, ref Int32 _pinc1ofz, ref Int32 _mg9v9w4h, Single* _7e60fcso, ref Int32 _5l1tna8s, Single* _ycxba85s, ref Int32 _ys09rxze, Single* _7u55mqkq, ref Int32 _u6e6d39b, ref Int32 _aiz0v1d1, Single* _pm308b4n, ref Int32 _vlfzpxiy, ref Int32 _aym8a085, Single* _gxe16fct, ref Int32 _gqb11x6f)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)12 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
Single _r7cfteg3 =  default;
Single _bafcbx97 =  default;
Single _j4eznmsw =  default;
Single _54yqyd88 =  default;
Single _sq0bv6ma =  default;
Single _p1s2d3vl =  default;
Single _a47u4hi3 =  default;
Single _odf6ja0t =  default;
Single _h75qnr7l =  default;
Single _ofbdxt08 =  default;
Single _bogm0gwy =  default;
Single _ir7y3k4r =  default;
Single _5kkq3axm =  default;
Single _ru2k46vn =  default;
Single _0h4yb5wu =  default;
Int32 _b5p6od9s =  default;
Int32 _8ur10vsh =  default;
Int32 _3xy4w22e =  default;
Int32 _zjghysh4 =  default;
Int32 _znpjgsef =  default;
Int32 _psodi8to =  default;
Int32 _h5f9ahvx =  default;
Int32 _vefcha38 =  default;
Int32 _vmeebitr =  default;
Int32 _pl5v4w0g =  default;
Int32 _hgi9ttgs =  default;
Int32 _3yjg5vld =  default;
Int32 _umlkckdg =  default;
Int32 _psyg0fin =  default;
Int32 _5c51t51g =  default;
Int32 _iu5ubaen =  default;
Int32 _dq4z5uer =  default;
Int32 _145saasd =  default;
Int32 _taoaib9a =  default;
Int32 _ev4xhht5 =  default;
Int32 _2b3xfi6o =  default;
Int32 _gssn1aki =  default;
Int32 _t2paz2ci =  default;
Int32 _b2bbveeb =  default;
Int32 _r4631rms =  default;
Int32 _7rmsrelt =  default;
Int32 _zgqqw8ka =  default;
Int32 _edl2gwc7 =  default;
Int32 _8uvmh43c =  default;
Boolean _nq67drqc =  default;
Boolean _q6ciqu17 =  default;
Boolean _qrmoxypt =  default;
Single* _xdbczr8u =  (Single*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Single) * ((int)3);
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
		//*     .. Intrinsic Functions .. 
		//* 
		//*     .. 
		//*     .. Local Arrays .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     ==== If there are no shifts, then there is nothing to do. ==== 
		//* 
		
		if (_rsjf6fvx < (int)2)
		return;//* 
		//*     ==== If the active block is empty or 1-by-1, then there 
		//*     .    is nothing to do. ==== 
		//* 
		
		if (_4a77vvpa >= _7d8gh478)
		return;//* 
		//*     ==== Shuffle shifts into pairs of real shifts and pairs 
		//*     .    of complex conjugate shifts assuming complex 
		//*     .    conjugate shifts are already adjacent to one 
		//*     .    another. ==== 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn2421 = (System.Int32)((int)1);
			System.Int32 __81fgg2step2421 = (System.Int32)((int)2);
			System.Int32 __81fgg2count2421;
			for (__81fgg2count2421 = System.Math.Max(0, (System.Int32)(((System.Int32)(_rsjf6fvx - (int)2) - __81fgg2dlsvn2421 + __81fgg2step2421) / __81fgg2step2421)), _b5p6od9s = __81fgg2dlsvn2421; __81fgg2count2421 != 0; __81fgg2count2421--, _b5p6od9s += (__81fgg2step2421)) {

			{
				
				if (*(_sgos5dql+(_b5p6od9s - 1)) != (-(*(_sgos5dql+(_b5p6od9s + (int)1 - 1)))))
				{
					//* 
					
					_ir7y3k4r = *(_o2dp5e8w+(_b5p6od9s - 1));
					*(_o2dp5e8w+(_b5p6od9s - 1)) = *(_o2dp5e8w+(_b5p6od9s + (int)1 - 1));
					*(_o2dp5e8w+(_b5p6od9s + (int)1 - 1)) = *(_o2dp5e8w+(_b5p6od9s + (int)2 - 1));
					*(_o2dp5e8w+(_b5p6od9s + (int)2 - 1)) = _ir7y3k4r;//* 
					
					_ir7y3k4r = *(_sgos5dql+(_b5p6od9s - 1));
					*(_sgos5dql+(_b5p6od9s - 1)) = *(_sgos5dql+(_b5p6od9s + (int)1 - 1));
					*(_sgos5dql+(_b5p6od9s + (int)1 - 1)) = *(_sgos5dql+(_b5p6od9s + (int)2 - 1));
					*(_sgos5dql+(_b5p6od9s + (int)2 - 1)) = _ir7y3k4r;
				}
				
Mark10:;
				// continue
			}
						}		}//* 
		//*     ==== NSHFTS is supposed to be even, but if it is odd, 
		//*     .    then simply reduce it by one.  The shuffle above 
		//*     .    ensures that the dropped shift is real and that 
		//*     .    the remaining shifts are paired. ==== 
		//* 
		
		_edl2gwc7 = (_rsjf6fvx - ILNumerics.F2NET.Intrinsics.MOD(_rsjf6fvx ,(int)2 ));//* 
		//*     ==== Machine constants for deflation ==== 
		//* 
		
		_h75qnr7l = _d5tu038y("SAFE MINIMUM" );
		_odf6ja0t = (_kxg5drh2 / _h75qnr7l);
		_6cljvt6b(ref _h75qnr7l ,ref _odf6ja0t );
		_0h4yb5wu = _d5tu038y("PRECISION" );
		_bogm0gwy = (_h75qnr7l * (ILNumerics.F2NET.Intrinsics.REAL(_dxpq0xkr ) / _0h4yb5wu));//* 
		//*     ==== Use accumulated reflections to update far-from-diagonal 
		//*     .    entries ? ==== 
		//* 
		
		_nq67drqc = ((_v32676ye == (int)1) | (_v32676ye == (int)2));//* 
		//*     ==== If so, exploit the 2-by-2 block structure? ==== 
		//* 
		
		_q6ciqu17 = ((_edl2gwc7 > (int)2) & (_v32676ye == (int)2));//* 
		//*     ==== clear trash ==== 
		//* 
		
		if ((_4a77vvpa + (int)2) <= _7d8gh478)
		*(_ogkjl6gu+(_4a77vvpa + (int)2 - 1) + (_4a77vvpa - 1) * 1 * (_1iekxpnw)) = _d0547bi2;//* 
		//*     ==== NBMPS = number of 2-shift bulges in the chain ==== 
		//* 
		
		_7rmsrelt = (_edl2gwc7 / (int)2);//* 
		//*     ==== KDU = width of slab ==== 
		//* 
		
		_5c51t51g = (((int)6 * _7rmsrelt) - (int)3);//* 
		//*     ==== Create and chase chains of NBMPS bulges ==== 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn2422 = (System.Int32)(((((int)3 * ((int)1 - _7rmsrelt)) + _4a77vvpa) - (int)1));
			System.Int32 __81fgg2step2422 = (System.Int32)(((int)3 * _7rmsrelt) - (int)2);
			System.Int32 __81fgg2count2422;
			for (__81fgg2count2422 = System.Math.Max(0, (System.Int32)(((System.Int32)(_7d8gh478 - (int)2) - __81fgg2dlsvn2422 + __81fgg2step2422) / __81fgg2step2422)), _zjghysh4 = __81fgg2dlsvn2422; __81fgg2count2422 != 0; __81fgg2count2422--, _zjghysh4 += (__81fgg2step2422)) {

			{
				
				_zgqqw8ka = (_zjghysh4 + _5c51t51g);
				if (_nq67drqc)
				_t013e1c8("ALL" ,ref _5c51t51g ,ref _5c51t51g ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,_7u55mqkq ,ref _u6e6d39b );//* 
				//*        ==== Near-the-diagonal bulge chase.  The following loop 
				//*        .    performs the near-the-diagonal part of a small bulge 
				//*        .    multi-shift QR sweep.  Each 6*NBMPS-2 column diagonal 
				//*        .    chunk extends from column INCOL to column NDCOL 
				//*        .    (including both column INCOL and column NDCOL). The 
				//*        .    following loop chases a 3*NBMPS column long chain of 
				//*        .    NBMPS bulges 3*NBMPS-2 columns to the right.  (INCOL 
				//*        .    may be less than KTOP and and NDCOL may be greater than 
				//*        .    KBOT indicating phantom columns from which to chase 
				//*        .    bulges before they are actually introduced or to which 
				//*        .    to chase bulges beyond column KBOT.)  ==== 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn2423 = (System.Int32)(_zjghysh4);
					const System.Int32 __81fgg2step2423 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2423;
					for (__81fgg2count2423 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN((_zjghysh4 + ((int)3 * _7rmsrelt)) - (int)3 ,_7d8gh478 - (int)2 )) - __81fgg2dlsvn2423 + __81fgg2step2423) / __81fgg2step2423)), _145saasd = __81fgg2dlsvn2423; __81fgg2count2423 != 0; __81fgg2count2423--, _145saasd += (__81fgg2step2423)) {

					{
						//* 
						//*           ==== Bulges number MTOP to MBOT are active double implicit 
						//*           .    shift bulges.  There may or may not also be small 
						//*           .    2-by-2 bulge, if there is room.  The inactive bulges 
						//*           .    (if any) must wait until the active bulges have moved 
						//*           .    down the diagonal to make room.  The phantom matrix 
						//*           .    paradigm described above helps keep track.  ==== 
						//* 
						
						_r4631rms = ILNumerics.F2NET.Intrinsics.MAX((int)1 ,((((_4a77vvpa - (int)1) - _145saasd) + (int)2) / (int)3) + (int)1 );
						_gssn1aki = ILNumerics.F2NET.Intrinsics.MIN(_7rmsrelt ,(_7d8gh478 - _145saasd) / (int)3 );
						_2b3xfi6o = (_gssn1aki + (int)1);
						_qrmoxypt = ((_gssn1aki < _7rmsrelt) & ((_145saasd + ((int)3 * (_2b3xfi6o - (int)1))) == (_7d8gh478 - (int)2)));//* 
						//*           ==== Generate reflections to chase the chain right 
						//*           .    one column.  (The minimum value of K is KTOP-1.) ==== 
						//* 
						
						{
							System.Int32 __81fgg2dlsvn2424 = (System.Int32)(_r4631rms);
							const System.Int32 __81fgg2step2424 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2424;
							for (__81fgg2count2424 = System.Math.Max(0, (System.Int32)(((System.Int32)(_gssn1aki) - __81fgg2dlsvn2424 + __81fgg2step2424) / __81fgg2step2424)), _ev4xhht5 = __81fgg2dlsvn2424; __81fgg2count2424 != 0; __81fgg2count2424--, _ev4xhht5 += (__81fgg2step2424)) {

							{
								
								_umlkckdg = (_145saasd + ((int)3 * (_ev4xhht5 - (int)1)));
								if (_umlkckdg == (_4a77vvpa - (int)1))
								{
									
									_jg99g0io(ref Unsafe.AsRef((int)3) ,(_ogkjl6gu+(_4a77vvpa - 1) + (_4a77vvpa - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,ref Unsafe.AsRef(*(_o2dp5e8w+(((int)2 * _ev4xhht5) - (int)1 - 1))) ,ref Unsafe.AsRef(*(_sgos5dql+(((int)2 * _ev4xhht5) - (int)1 - 1))) ,ref Unsafe.AsRef(*(_o2dp5e8w+((int)2 * _ev4xhht5 - 1))) ,ref Unsafe.AsRef(*(_sgos5dql+((int)2 * _ev4xhht5 - 1))) ,(_ycxba85s+((int)1 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze)));
									_r7cfteg3 = *(_ycxba85s+((int)1 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze));
									_mbabw0s0(ref Unsafe.AsRef((int)3) ,ref _r7cfteg3 ,(_ycxba85s+((int)2 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_ycxba85s+((int)1 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze))) );
								}
								else
								{
									
									_bafcbx97 = *(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw));
									*(_ycxba85s+((int)2 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze)) = *(_ogkjl6gu+(_umlkckdg + (int)2 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw));
									*(_ycxba85s+((int)3 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze)) = *(_ogkjl6gu+(_umlkckdg + (int)3 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw));
									_mbabw0s0(ref Unsafe.AsRef((int)3) ,ref _bafcbx97 ,(_ycxba85s+((int)2 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_ycxba85s+((int)1 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze))) );//* 
									//*                 ==== A Bulge may collapse because of vigilant 
									//*                 .    deflation or destructive underflow.  In the 
									//*                 .    underflow case, try the two-small-subdiagonals 
									//*                 .    trick to try to reinflate the bulge.  ==== 
									//* 
									
									if (((*(_ogkjl6gu+(_umlkckdg + (int)3 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) != _d0547bi2) | (*(_ogkjl6gu+(_umlkckdg + (int)3 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)) != _d0547bi2)) | (*(_ogkjl6gu+(_umlkckdg + (int)3 - 1) + (_umlkckdg + (int)2 - 1) * 1 * (_1iekxpnw)) == _d0547bi2))
									{
										//* 
										//*                    ==== Typical case: not collapsed (yet). ==== 
										//* 
										
										*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) = _bafcbx97;
										*(_ogkjl6gu+(_umlkckdg + (int)2 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) = _d0547bi2;
										*(_ogkjl6gu+(_umlkckdg + (int)3 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) = _d0547bi2;
									}
									else
									{
										//* 
										//*                    ==== Atypical case: collapsed.  Attempt to 
										//*                    .    reintroduce ignoring H(K+1,K) and H(K+2,K). 
										//*                    .    If the fill resulting from the new 
										//*                    .    reflector is too large, then abandon it. 
										//*                    .    Otherwise, use the new one. ==== 
										//* 
										
										_jg99g0io(ref Unsafe.AsRef((int)3) ,(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,ref Unsafe.AsRef(*(_o2dp5e8w+(((int)2 * _ev4xhht5) - (int)1 - 1))) ,ref Unsafe.AsRef(*(_sgos5dql+(((int)2 * _ev4xhht5) - (int)1 - 1))) ,ref Unsafe.AsRef(*(_o2dp5e8w+((int)2 * _ev4xhht5 - 1))) ,ref Unsafe.AsRef(*(_sgos5dql+((int)2 * _ev4xhht5 - 1))) ,_xdbczr8u );
										_r7cfteg3 = *(_xdbczr8u+((int)1 - 1));
										_mbabw0s0(ref Unsafe.AsRef((int)3) ,ref _r7cfteg3 ,(_xdbczr8u+((int)2 - 1)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_xdbczr8u+((int)1 - 1))) );
										_a47u4hi3 = (*(_xdbczr8u+((int)1 - 1)) * (*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) + (*(_xdbczr8u+((int)2 - 1)) * *(_ogkjl6gu+(_umlkckdg + (int)2 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)))));//* 
										
										if ((ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg + (int)2 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) - (_a47u4hi3 * *(_xdbczr8u+((int)2 - 1))) ) + ILNumerics.F2NET.Intrinsics.ABS(_a47u4hi3 * *(_xdbczr8u+((int)3 - 1)) )) > (_0h4yb5wu * ((ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)) )) + ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg + (int)2 - 1) + (_umlkckdg + (int)2 - 1) * 1 * (_1iekxpnw)) ))))
										{
											//* 
											//*                       ==== Starting a new bulge here would 
											//*                       .    create non-negligible fill.  Use 
											//*                       .    the old one with trepidation. ==== 
											//* 
											
											*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) = _bafcbx97;
											*(_ogkjl6gu+(_umlkckdg + (int)2 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) = _d0547bi2;
											*(_ogkjl6gu+(_umlkckdg + (int)3 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) = _d0547bi2;
										}
										else
										{
											//* 
											//*                       ==== Stating a new bulge here would 
											//*                       .    create only negligible fill. 
											//*                       .    Replace the old reflector with 
											//*                       .    the new one. ==== 
											//* 
											
											*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) - _a47u4hi3);
											*(_ogkjl6gu+(_umlkckdg + (int)2 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) = _d0547bi2;
											*(_ogkjl6gu+(_umlkckdg + (int)3 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) = _d0547bi2;
											*(_ycxba85s+((int)1 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze)) = *(_xdbczr8u+((int)1 - 1));
											*(_ycxba85s+((int)2 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze)) = *(_xdbczr8u+((int)2 - 1));
											*(_ycxba85s+((int)3 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze)) = *(_xdbczr8u+((int)3 - 1));
										}
										
									}
									
								}
								
Mark20:;
								// continue
							}
														}						}//* 
						//*           ==== Generate a 2-by-2 reflection, if needed. ==== 
						//* 
						
						_umlkckdg = (_145saasd + ((int)3 * (_2b3xfi6o - (int)1)));
						if (_qrmoxypt)
						{
							
							if (_umlkckdg == (_4a77vvpa - (int)1))
							{
								
								_jg99g0io(ref Unsafe.AsRef((int)2) ,(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,ref Unsafe.AsRef(*(_o2dp5e8w+(((int)2 * _2b3xfi6o) - (int)1 - 1))) ,ref Unsafe.AsRef(*(_sgos5dql+(((int)2 * _2b3xfi6o) - (int)1 - 1))) ,ref Unsafe.AsRef(*(_o2dp5e8w+((int)2 * _2b3xfi6o - 1))) ,ref Unsafe.AsRef(*(_sgos5dql+((int)2 * _2b3xfi6o - 1))) ,(_ycxba85s+((int)1 - 1) + (_2b3xfi6o - 1) * 1 * (_ys09rxze)));
								_bafcbx97 = *(_ycxba85s+((int)1 - 1) + (_2b3xfi6o - 1) * 1 * (_ys09rxze));
								_mbabw0s0(ref Unsafe.AsRef((int)2) ,ref _bafcbx97 ,(_ycxba85s+((int)2 - 1) + (_2b3xfi6o - 1) * 1 * (_ys09rxze)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_ycxba85s+((int)1 - 1) + (_2b3xfi6o - 1) * 1 * (_ys09rxze))) );
							}
							else
							{
								
								_bafcbx97 = *(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw));
								*(_ycxba85s+((int)2 - 1) + (_2b3xfi6o - 1) * 1 * (_ys09rxze)) = *(_ogkjl6gu+(_umlkckdg + (int)2 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw));
								_mbabw0s0(ref Unsafe.AsRef((int)2) ,ref _bafcbx97 ,(_ycxba85s+((int)2 - 1) + (_2b3xfi6o - 1) * 1 * (_ys09rxze)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_ycxba85s+((int)1 - 1) + (_2b3xfi6o - 1) * 1 * (_ys09rxze))) );
								*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) = _bafcbx97;
								*(_ogkjl6gu+(_umlkckdg + (int)2 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) = _d0547bi2;
							}
							
						}
						//* 
						//*           ==== Multiply H by reflections from the left ==== 
						//* 
						
						if (_nq67drqc)
						{
							
							_vefcha38 = ILNumerics.F2NET.Intrinsics.MIN(_zgqqw8ka ,_7d8gh478 );
						}
						else
						if (_amt8y1zm)
						{
							
							_vefcha38 = _dxpq0xkr;
						}
						else
						{
							
							_vefcha38 = _7d8gh478;
						}
						
						{
							System.Int32 __81fgg2dlsvn2425 = (System.Int32)(ILNumerics.F2NET.Intrinsics.MAX(_4a77vvpa ,_145saasd ));
							const System.Int32 __81fgg2step2425 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2425;
							for (__81fgg2count2425 = System.Math.Max(0, (System.Int32)(((System.Int32)(_vefcha38) - __81fgg2dlsvn2425 + __81fgg2step2425) / __81fgg2step2425)), _znpjgsef = __81fgg2dlsvn2425; __81fgg2count2425 != 0; __81fgg2count2425--, _znpjgsef += (__81fgg2step2425)) {

							{
								
								_t2paz2ci = ILNumerics.F2NET.Intrinsics.MIN(_gssn1aki ,((_znpjgsef - _145saasd) + (int)2) / (int)3 );
								{
									System.Int32 __81fgg2dlsvn2426 = (System.Int32)(_r4631rms);
									const System.Int32 __81fgg2step2426 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2426;
									for (__81fgg2count2426 = System.Math.Max(0, (System.Int32)(((System.Int32)(_t2paz2ci) - __81fgg2dlsvn2426 + __81fgg2step2426) / __81fgg2step2426)), _ev4xhht5 = __81fgg2dlsvn2426; __81fgg2count2426 != 0; __81fgg2count2426--, _ev4xhht5 += (__81fgg2step2426)) {

									{
										
										_umlkckdg = (_145saasd + ((int)3 * (_ev4xhht5 - (int)1)));
										_a47u4hi3 = (*(_ycxba85s+((int)1 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze)) * ((*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) + (*(_ycxba85s+((int)2 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze)) * *(_ogkjl6gu+(_umlkckdg + (int)2 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)))) + (*(_ycxba85s+((int)3 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze)) * *(_ogkjl6gu+(_umlkckdg + (int)3 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)))));
										*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) - _a47u4hi3);
										*(_ogkjl6gu+(_umlkckdg + (int)2 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_umlkckdg + (int)2 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) - (_a47u4hi3 * *(_ycxba85s+((int)2 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze))));
										*(_ogkjl6gu+(_umlkckdg + (int)3 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_umlkckdg + (int)3 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) - (_a47u4hi3 * *(_ycxba85s+((int)3 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze))));
Mark30:;
										// continue
									}
																		}								}
Mark40:;
								// continue
							}
														}						}
						if (_qrmoxypt)
						{
							
							_umlkckdg = (_145saasd + ((int)3 * (_2b3xfi6o - (int)1)));
							{
								System.Int32 __81fgg2dlsvn2427 = (System.Int32)(ILNumerics.F2NET.Intrinsics.MAX(_umlkckdg + (int)1 ,_4a77vvpa ));
								const System.Int32 __81fgg2step2427 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2427;
								for (__81fgg2count2427 = System.Math.Max(0, (System.Int32)(((System.Int32)(_vefcha38) - __81fgg2dlsvn2427 + __81fgg2step2427) / __81fgg2step2427)), _znpjgsef = __81fgg2dlsvn2427; __81fgg2count2427 != 0; __81fgg2count2427--, _znpjgsef += (__81fgg2step2427)) {

								{
									
									_a47u4hi3 = (*(_ycxba85s+((int)1 - 1) + (_2b3xfi6o - 1) * 1 * (_ys09rxze)) * (*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) + (*(_ycxba85s+((int)2 - 1) + (_2b3xfi6o - 1) * 1 * (_ys09rxze)) * *(_ogkjl6gu+(_umlkckdg + (int)2 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)))));
									*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) - _a47u4hi3);
									*(_ogkjl6gu+(_umlkckdg + (int)2 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_umlkckdg + (int)2 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) - (_a47u4hi3 * *(_ycxba85s+((int)2 - 1) + (_2b3xfi6o - 1) * 1 * (_ys09rxze))));
Mark50:;
									// continue
								}
																}							}
						}
						//* 
						//*           ==== Multiply H by reflections from the right. 
						//*           .    Delay filling in the last row until the 
						//*           .    vigilant deflation check is complete. ==== 
						//* 
						
						if (_nq67drqc)
						{
							
							_3yjg5vld = ILNumerics.F2NET.Intrinsics.MAX(_4a77vvpa ,_zjghysh4 );
						}
						else
						if (_amt8y1zm)
						{
							
							_3yjg5vld = (int)1;
						}
						else
						{
							
							_3yjg5vld = _4a77vvpa;
						}
						
						{
							System.Int32 __81fgg2dlsvn2428 = (System.Int32)(_r4631rms);
							const System.Int32 __81fgg2step2428 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2428;
							for (__81fgg2count2428 = System.Math.Max(0, (System.Int32)(((System.Int32)(_gssn1aki) - __81fgg2dlsvn2428 + __81fgg2step2428) / __81fgg2step2428)), _ev4xhht5 = __81fgg2dlsvn2428; __81fgg2count2428 != 0; __81fgg2count2428--, _ev4xhht5 += (__81fgg2step2428)) {

							{
								
								if (*(_ycxba85s+((int)1 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze)) != _d0547bi2)
								{
									
									_umlkckdg = (_145saasd + ((int)3 * (_ev4xhht5 - (int)1)));
									{
										System.Int32 __81fgg2dlsvn2429 = (System.Int32)(_3yjg5vld);
										const System.Int32 __81fgg2step2429 = (System.Int32)((int)1);
										System.Int32 __81fgg2count2429;
										for (__81fgg2count2429 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN(_7d8gh478 ,_umlkckdg + (int)3 )) - __81fgg2dlsvn2429 + __81fgg2step2429) / __81fgg2step2429)), _znpjgsef = __81fgg2dlsvn2429; __81fgg2count2429 != 0; __81fgg2count2429--, _znpjgsef += (__81fgg2step2429)) {

										{
											
											_a47u4hi3 = (*(_ycxba85s+((int)1 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze)) * ((*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)) + (*(_ycxba85s+((int)2 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze)) * *(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)2 - 1) * 1 * (_1iekxpnw)))) + (*(_ycxba85s+((int)3 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze)) * *(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)3 - 1) * 1 * (_1iekxpnw)))));
											*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)) - _a47u4hi3);
											*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)2 - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)2 - 1) * 1 * (_1iekxpnw)) - (_a47u4hi3 * *(_ycxba85s+((int)2 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze))));
											*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)3 - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)3 - 1) * 1 * (_1iekxpnw)) - (_a47u4hi3 * *(_ycxba85s+((int)3 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze))));
Mark60:;
											// continue
										}
																				}									}//* 
									
									if (_nq67drqc)
									{
										//* 
										//*                    ==== Accumulate U. (If necessary, update Z later 
										//*                    .    with with an efficient matrix-matrix 
										//*                    .    multiply.) ==== 
										//* 
										
										_iu5ubaen = (_umlkckdg - _zjghysh4);
										{
											System.Int32 __81fgg2dlsvn2430 = (System.Int32)(ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_4a77vvpa - _zjghysh4 ));
											const System.Int32 __81fgg2step2430 = (System.Int32)((int)1);
											System.Int32 __81fgg2count2430;
											for (__81fgg2count2430 = System.Math.Max(0, (System.Int32)(((System.Int32)(_5c51t51g) - __81fgg2dlsvn2430 + __81fgg2step2430) / __81fgg2step2430)), _znpjgsef = __81fgg2dlsvn2430; __81fgg2count2430 != 0; __81fgg2count2430--, _znpjgsef += (__81fgg2step2430)) {

											{
												
												_a47u4hi3 = (*(_ycxba85s+((int)1 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze)) * ((*(_7u55mqkq+(_znpjgsef - 1) + (_iu5ubaen + (int)1 - 1) * 1 * (_u6e6d39b)) + (*(_ycxba85s+((int)2 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze)) * *(_7u55mqkq+(_znpjgsef - 1) + (_iu5ubaen + (int)2 - 1) * 1 * (_u6e6d39b)))) + (*(_ycxba85s+((int)3 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze)) * *(_7u55mqkq+(_znpjgsef - 1) + (_iu5ubaen + (int)3 - 1) * 1 * (_u6e6d39b)))));
												*(_7u55mqkq+(_znpjgsef - 1) + (_iu5ubaen + (int)1 - 1) * 1 * (_u6e6d39b)) = (*(_7u55mqkq+(_znpjgsef - 1) + (_iu5ubaen + (int)1 - 1) * 1 * (_u6e6d39b)) - _a47u4hi3);
												*(_7u55mqkq+(_znpjgsef - 1) + (_iu5ubaen + (int)2 - 1) * 1 * (_u6e6d39b)) = (*(_7u55mqkq+(_znpjgsef - 1) + (_iu5ubaen + (int)2 - 1) * 1 * (_u6e6d39b)) - (_a47u4hi3 * *(_ycxba85s+((int)2 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze))));
												*(_7u55mqkq+(_znpjgsef - 1) + (_iu5ubaen + (int)3 - 1) * 1 * (_u6e6d39b)) = (*(_7u55mqkq+(_znpjgsef - 1) + (_iu5ubaen + (int)3 - 1) * 1 * (_u6e6d39b)) - (_a47u4hi3 * *(_ycxba85s+((int)3 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze))));
Mark70:;
												// continue
											}
																						}										}
									}
									else
									if (_189gzykk)
									{
										//* 
										//*                    ==== U is not accumulated, so update Z 
										//*                    .    now by multiplying by reflections 
										//*                    .    from the right. ==== 
										//* 
										
										{
											System.Int32 __81fgg2dlsvn2431 = (System.Int32)(_pinc1ofz);
											const System.Int32 __81fgg2step2431 = (System.Int32)((int)1);
											System.Int32 __81fgg2count2431;
											for (__81fgg2count2431 = System.Math.Max(0, (System.Int32)(((System.Int32)(_mg9v9w4h) - __81fgg2dlsvn2431 + __81fgg2step2431) / __81fgg2step2431)), _znpjgsef = __81fgg2dlsvn2431; __81fgg2count2431 != 0; __81fgg2count2431--, _znpjgsef += (__81fgg2step2431)) {

											{
												
												_a47u4hi3 = (*(_ycxba85s+((int)1 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze)) * ((*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_5l1tna8s)) + (*(_ycxba85s+((int)2 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze)) * *(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)2 - 1) * 1 * (_5l1tna8s)))) + (*(_ycxba85s+((int)3 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze)) * *(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)3 - 1) * 1 * (_5l1tna8s)))));
												*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_5l1tna8s)) = (*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_5l1tna8s)) - _a47u4hi3);
												*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)2 - 1) * 1 * (_5l1tna8s)) = (*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)2 - 1) * 1 * (_5l1tna8s)) - (_a47u4hi3 * *(_ycxba85s+((int)2 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze))));
												*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)3 - 1) * 1 * (_5l1tna8s)) = (*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)3 - 1) * 1 * (_5l1tna8s)) - (_a47u4hi3 * *(_ycxba85s+((int)3 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze))));
Mark80:;
												// continue
											}
																						}										}
									}
									
								}
								
Mark90:;
								// continue
							}
														}						}//* 
						//*           ==== Special case: 2-by-2 reflection (if needed) ==== 
						//* 
						
						_umlkckdg = (_145saasd + ((int)3 * (_2b3xfi6o - (int)1)));
						if (_qrmoxypt)
						{
							
							if (*(_ycxba85s+((int)1 - 1) + (_2b3xfi6o - 1) * 1 * (_ys09rxze)) != _d0547bi2)
							{
								
								{
									System.Int32 __81fgg2dlsvn2432 = (System.Int32)(_3yjg5vld);
									const System.Int32 __81fgg2step2432 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2432;
									for (__81fgg2count2432 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN(_7d8gh478 ,_umlkckdg + (int)3 )) - __81fgg2dlsvn2432 + __81fgg2step2432) / __81fgg2step2432)), _znpjgsef = __81fgg2dlsvn2432; __81fgg2count2432 != 0; __81fgg2count2432--, _znpjgsef += (__81fgg2step2432)) {

									{
										
										_a47u4hi3 = (*(_ycxba85s+((int)1 - 1) + (_2b3xfi6o - 1) * 1 * (_ys09rxze)) * (*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)) + (*(_ycxba85s+((int)2 - 1) + (_2b3xfi6o - 1) * 1 * (_ys09rxze)) * *(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)2 - 1) * 1 * (_1iekxpnw)))));
										*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)) - _a47u4hi3);
										*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)2 - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)2 - 1) * 1 * (_1iekxpnw)) - (_a47u4hi3 * *(_ycxba85s+((int)2 - 1) + (_2b3xfi6o - 1) * 1 * (_ys09rxze))));
Mark100:;
										// continue
									}
																		}								}//* 
								
								if (_nq67drqc)
								{
									
									_iu5ubaen = (_umlkckdg - _zjghysh4);
									{
										System.Int32 __81fgg2dlsvn2433 = (System.Int32)(ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_4a77vvpa - _zjghysh4 ));
										const System.Int32 __81fgg2step2433 = (System.Int32)((int)1);
										System.Int32 __81fgg2count2433;
										for (__81fgg2count2433 = System.Math.Max(0, (System.Int32)(((System.Int32)(_5c51t51g) - __81fgg2dlsvn2433 + __81fgg2step2433) / __81fgg2step2433)), _znpjgsef = __81fgg2dlsvn2433; __81fgg2count2433 != 0; __81fgg2count2433--, _znpjgsef += (__81fgg2step2433)) {

										{
											
											_a47u4hi3 = (*(_ycxba85s+((int)1 - 1) + (_2b3xfi6o - 1) * 1 * (_ys09rxze)) * (*(_7u55mqkq+(_znpjgsef - 1) + (_iu5ubaen + (int)1 - 1) * 1 * (_u6e6d39b)) + (*(_ycxba85s+((int)2 - 1) + (_2b3xfi6o - 1) * 1 * (_ys09rxze)) * *(_7u55mqkq+(_znpjgsef - 1) + (_iu5ubaen + (int)2 - 1) * 1 * (_u6e6d39b)))));
											*(_7u55mqkq+(_znpjgsef - 1) + (_iu5ubaen + (int)1 - 1) * 1 * (_u6e6d39b)) = (*(_7u55mqkq+(_znpjgsef - 1) + (_iu5ubaen + (int)1 - 1) * 1 * (_u6e6d39b)) - _a47u4hi3);
											*(_7u55mqkq+(_znpjgsef - 1) + (_iu5ubaen + (int)2 - 1) * 1 * (_u6e6d39b)) = (*(_7u55mqkq+(_znpjgsef - 1) + (_iu5ubaen + (int)2 - 1) * 1 * (_u6e6d39b)) - (_a47u4hi3 * *(_ycxba85s+((int)2 - 1) + (_2b3xfi6o - 1) * 1 * (_ys09rxze))));
Mark110:;
											// continue
										}
																				}									}
								}
								else
								if (_189gzykk)
								{
									
									{
										System.Int32 __81fgg2dlsvn2434 = (System.Int32)(_pinc1ofz);
										const System.Int32 __81fgg2step2434 = (System.Int32)((int)1);
										System.Int32 __81fgg2count2434;
										for (__81fgg2count2434 = System.Math.Max(0, (System.Int32)(((System.Int32)(_mg9v9w4h) - __81fgg2dlsvn2434 + __81fgg2step2434) / __81fgg2step2434)), _znpjgsef = __81fgg2dlsvn2434; __81fgg2count2434 != 0; __81fgg2count2434--, _znpjgsef += (__81fgg2step2434)) {

										{
											
											_a47u4hi3 = (*(_ycxba85s+((int)1 - 1) + (_2b3xfi6o - 1) * 1 * (_ys09rxze)) * (*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_5l1tna8s)) + (*(_ycxba85s+((int)2 - 1) + (_2b3xfi6o - 1) * 1 * (_ys09rxze)) * *(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)2 - 1) * 1 * (_5l1tna8s)))));
											*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_5l1tna8s)) = (*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_5l1tna8s)) - _a47u4hi3);
											*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)2 - 1) * 1 * (_5l1tna8s)) = (*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)2 - 1) * 1 * (_5l1tna8s)) - (_a47u4hi3 * *(_ycxba85s+((int)2 - 1) + (_2b3xfi6o - 1) * 1 * (_ys09rxze))));
Mark120:;
											// continue
										}
																				}									}
								}
								
							}
							
						}
						//* 
						//*           ==== Vigilant deflation check ==== 
						//* 
						
						_b2bbveeb = _r4631rms;
						if ((_145saasd + ((int)3 * (_b2bbveeb - (int)1))) < _4a77vvpa)
						_b2bbveeb = (_b2bbveeb + (int)1);
						_t2paz2ci = _gssn1aki;
						if (_qrmoxypt)
						_t2paz2ci = (_t2paz2ci + (int)1);
						if (_145saasd == (_7d8gh478 - (int)2))
						_t2paz2ci = (_t2paz2ci + (int)1);
						{
							System.Int32 __81fgg2dlsvn2435 = (System.Int32)(_b2bbveeb);
							const System.Int32 __81fgg2step2435 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2435;
							for (__81fgg2count2435 = System.Math.Max(0, (System.Int32)(((System.Int32)(_t2paz2ci) - __81fgg2dlsvn2435 + __81fgg2step2435) / __81fgg2step2435)), _ev4xhht5 = __81fgg2dlsvn2435; __81fgg2count2435 != 0; __81fgg2count2435--, _ev4xhht5 += (__81fgg2step2435)) {

							{
								
								_umlkckdg = ILNumerics.F2NET.Intrinsics.MIN(_7d8gh478 - (int)1 ,_145saasd + ((int)3 * (_ev4xhht5 - (int)1)) );//* 
								//*              ==== The following convergence test requires that 
								//*              .    the tradition small-compared-to-nearby-diagonals 
								//*              .    criterion and the Ahues & Tisseur (LAWN 122, 1997) 
								//*              .    criteria both be satisfied.  The latter improves 
								//*              .    accuracy in some examples. Falling back on an 
								//*              .    alternate convergence criterion when TST1 or TST2 
								//*              .    is zero (as done here) is traditional but probably 
								//*              .    unnecessary. ==== 
								//* 
								
								if (*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) != _d0547bi2)
								{
									
									_5kkq3axm = (ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)) ));
									if (_5kkq3axm == _d0547bi2)
									{
										
										if (_umlkckdg >= (_4a77vvpa + (int)1))
										_5kkq3axm = (_5kkq3axm + ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - (int)1 - 1) * 1 * (_1iekxpnw)) ));
										if (_umlkckdg >= (_4a77vvpa + (int)2))
										_5kkq3axm = (_5kkq3axm + ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - (int)2 - 1) * 1 * (_1iekxpnw)) ));
										if (_umlkckdg >= (_4a77vvpa + (int)3))
										_5kkq3axm = (_5kkq3axm + ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - (int)3 - 1) * 1 * (_1iekxpnw)) ));
										if (_umlkckdg <= (_7d8gh478 - (int)2))
										_5kkq3axm = (_5kkq3axm + ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg + (int)2 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)) ));
										if (_umlkckdg <= (_7d8gh478 - (int)3))
										_5kkq3axm = (_5kkq3axm + ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg + (int)3 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)) ));
										if (_umlkckdg <= (_7d8gh478 - (int)4))
										_5kkq3axm = (_5kkq3axm + ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg + (int)4 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)) ));
									}
									
									if (ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) ) <= ILNumerics.F2NET.Intrinsics.MAX(_bogm0gwy ,_0h4yb5wu * _5kkq3axm ))
									{
										
										_54yqyd88 = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)) ) );
										_sq0bv6ma = ILNumerics.F2NET.Intrinsics.MIN(ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)) ) );
										_j4eznmsw = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) - *(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)) ) );
										_p1s2d3vl = ILNumerics.F2NET.Intrinsics.MIN(ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) - *(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)) ) );
										_ofbdxt08 = (_j4eznmsw + _54yqyd88);
										_ru2k46vn = (_p1s2d3vl * (_j4eznmsw / _ofbdxt08));//* 
										
										if ((_ru2k46vn == _d0547bi2) | ((_sq0bv6ma * (_54yqyd88 / _ofbdxt08)) <= ILNumerics.F2NET.Intrinsics.MAX(_bogm0gwy ,_0h4yb5wu * _ru2k46vn )))
										*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) = _d0547bi2;
									}
									
								}
								
Mark130:;
								// continue
							}
														}						}//* 
						//*           ==== Fill in the last row of each bulge. ==== 
						//* 
						
						_t2paz2ci = ILNumerics.F2NET.Intrinsics.MIN(_7rmsrelt ,((_7d8gh478 - _145saasd) - (int)1) / (int)3 );
						{
							System.Int32 __81fgg2dlsvn2436 = (System.Int32)(_r4631rms);
							const System.Int32 __81fgg2step2436 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2436;
							for (__81fgg2count2436 = System.Math.Max(0, (System.Int32)(((System.Int32)(_t2paz2ci) - __81fgg2dlsvn2436 + __81fgg2step2436) / __81fgg2step2436)), _ev4xhht5 = __81fgg2dlsvn2436; __81fgg2count2436 != 0; __81fgg2count2436--, _ev4xhht5 += (__81fgg2step2436)) {

							{
								
								_umlkckdg = (_145saasd + ((int)3 * (_ev4xhht5 - (int)1)));
								_a47u4hi3 = ((*(_ycxba85s+((int)1 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze)) * *(_ycxba85s+((int)3 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze))) * *(_ogkjl6gu+(_umlkckdg + (int)4 - 1) + (_umlkckdg + (int)3 - 1) * 1 * (_1iekxpnw)));
								*(_ogkjl6gu+(_umlkckdg + (int)4 - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)) = (-(_a47u4hi3));
								*(_ogkjl6gu+(_umlkckdg + (int)4 - 1) + (_umlkckdg + (int)2 - 1) * 1 * (_1iekxpnw)) = (-((_a47u4hi3 * *(_ycxba85s+((int)2 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze)))));
								*(_ogkjl6gu+(_umlkckdg + (int)4 - 1) + (_umlkckdg + (int)3 - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_umlkckdg + (int)4 - 1) + (_umlkckdg + (int)3 - 1) * 1 * (_1iekxpnw)) - (_a47u4hi3 * *(_ycxba85s+((int)3 - 1) + (_ev4xhht5 - 1) * 1 * (_ys09rxze))));
Mark140:;
								// continue
							}
														}						}//* 
						//*           ==== End of near-the-diagonal bulge chase. ==== 
						//* 
						
Mark150:;
						// continue
					}
										}				}//* 
				//*        ==== Use U (if accumulated) to update far-from-diagonal 
				//*        .    entries in H.  If required, use U to update Z as 
				//*        .    well. ==== 
				//* 
				
				if (_nq67drqc)
				{
					
					if (_amt8y1zm)
					{
						
						_3yjg5vld = (int)1;
						_vefcha38 = _dxpq0xkr;
					}
					else
					{
						
						_3yjg5vld = _4a77vvpa;
						_vefcha38 = _7d8gh478;
					}
					
					if ((((!(_q6ciqu17)) | (_zjghysh4 < _4a77vvpa)) | (_zgqqw8ka > _7d8gh478)) | (_edl2gwc7 <= (int)2))
					{
						//* 
						//*              ==== Updates not exploiting the 2-by-2 block 
						//*              .    structure of U.  K1 and NU keep track of 
						//*              .    the location and size of U in the special 
						//*              .    cases of introducing bulges and chasing 
						//*              .    bulges off the bottom.  In these special 
						//*              .    cases and in case the number of shifts 
						//*              .    is NS = 2, there is no 2-by-2 block 
						//*              .    structure to exploit.  ==== 
						//* 
						
						_psyg0fin = ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_4a77vvpa - _zjghysh4 );
						_8uvmh43c = (((_5c51t51g - ILNumerics.F2NET.Intrinsics.MAX((int)0 ,_zgqqw8ka - _7d8gh478 )) - _psyg0fin) + (int)1);//* 
						//*              ==== Horizontal Multiply ==== 
						//* 
						
						{
							System.Int32 __81fgg2dlsvn2437 = (System.Int32)((ILNumerics.F2NET.Intrinsics.MIN(_zgqqw8ka ,_7d8gh478 ) + (int)1));
							System.Int32 __81fgg2step2437 = (System.Int32)(_aym8a085);
							System.Int32 __81fgg2count2437;
							for (__81fgg2count2437 = System.Math.Max(0, (System.Int32)(((System.Int32)(_vefcha38) - __81fgg2dlsvn2437 + __81fgg2step2437) / __81fgg2step2437)), _vmeebitr = __81fgg2dlsvn2437; __81fgg2count2437 != 0; __81fgg2count2437--, _vmeebitr += (__81fgg2step2437)) {

							{
								
								_pl5v4w0g = ILNumerics.F2NET.Intrinsics.MIN(_aym8a085 ,(_vefcha38 - _vmeebitr) + (int)1 );
								_b8wa9454("C" ,"N" ,ref _8uvmh43c ,ref _pl5v4w0g ,ref _8uvmh43c ,ref Unsafe.AsRef(_kxg5drh2) ,(_7u55mqkq+(_psyg0fin - 1) + (_psyg0fin - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_ogkjl6gu+(_zjghysh4 + _psyg0fin - 1) + (_vmeebitr - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,ref Unsafe.AsRef(_d0547bi2) ,_gxe16fct ,ref _gqb11x6f );
								_m38y8dyg("ALL" ,ref _8uvmh43c ,ref _pl5v4w0g ,_gxe16fct ,ref _gqb11x6f ,(_ogkjl6gu+(_zjghysh4 + _psyg0fin - 1) + (_vmeebitr - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw );
Mark160:;
								// continue
							}
														}						}//* 
						//*              ==== Vertical multiply ==== 
						//* 
						
						{
							System.Int32 __81fgg2dlsvn2438 = (System.Int32)(_3yjg5vld);
							System.Int32 __81fgg2step2438 = (System.Int32)(_aiz0v1d1);
							System.Int32 __81fgg2count2438;
							for (__81fgg2count2438 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MAX(_4a77vvpa ,_zjghysh4 ) - (int)1) - __81fgg2dlsvn2438 + __81fgg2step2438) / __81fgg2step2438)), _hgi9ttgs = __81fgg2dlsvn2438; __81fgg2count2438 != 0; __81fgg2count2438--, _hgi9ttgs += (__81fgg2step2438)) {

							{
								
								_pl5v4w0g = ILNumerics.F2NET.Intrinsics.MIN(_aiz0v1d1 ,ILNumerics.F2NET.Intrinsics.MAX(_4a77vvpa ,_zjghysh4 ) - _hgi9ttgs );
								_b8wa9454("N" ,"N" ,ref _pl5v4w0g ,ref _8uvmh43c ,ref _8uvmh43c ,ref Unsafe.AsRef(_kxg5drh2) ,(_ogkjl6gu+(_hgi9ttgs - 1) + (_zjghysh4 + _psyg0fin - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,(_7u55mqkq+(_psyg0fin - 1) + (_psyg0fin - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,ref Unsafe.AsRef(_d0547bi2) ,_pm308b4n ,ref _vlfzpxiy );
								_m38y8dyg("ALL" ,ref _pl5v4w0g ,ref _8uvmh43c ,_pm308b4n ,ref _vlfzpxiy ,(_ogkjl6gu+(_hgi9ttgs - 1) + (_zjghysh4 + _psyg0fin - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw );
Mark170:;
								// continue
							}
														}						}//* 
						//*              ==== Z multiply (also vertical) ==== 
						//* 
						
						if (_189gzykk)
						{
							
							{
								System.Int32 __81fgg2dlsvn2439 = (System.Int32)(_pinc1ofz);
								System.Int32 __81fgg2step2439 = (System.Int32)(_aiz0v1d1);
								System.Int32 __81fgg2count2439;
								for (__81fgg2count2439 = System.Math.Max(0, (System.Int32)(((System.Int32)(_mg9v9w4h) - __81fgg2dlsvn2439 + __81fgg2step2439) / __81fgg2step2439)), _hgi9ttgs = __81fgg2dlsvn2439; __81fgg2count2439 != 0; __81fgg2count2439--, _hgi9ttgs += (__81fgg2step2439)) {

								{
									
									_pl5v4w0g = ILNumerics.F2NET.Intrinsics.MIN(_aiz0v1d1 ,(_mg9v9w4h - _hgi9ttgs) + (int)1 );
									_b8wa9454("N" ,"N" ,ref _pl5v4w0g ,ref _8uvmh43c ,ref _8uvmh43c ,ref Unsafe.AsRef(_kxg5drh2) ,(_7e60fcso+(_hgi9ttgs - 1) + (_zjghysh4 + _psyg0fin - 1) * 1 * (_5l1tna8s)),ref _5l1tna8s ,(_7u55mqkq+(_psyg0fin - 1) + (_psyg0fin - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,ref Unsafe.AsRef(_d0547bi2) ,_pm308b4n ,ref _vlfzpxiy );
									_m38y8dyg("ALL" ,ref _pl5v4w0g ,ref _8uvmh43c ,_pm308b4n ,ref _vlfzpxiy ,(_7e60fcso+(_hgi9ttgs - 1) + (_zjghysh4 + _psyg0fin - 1) * 1 * (_5l1tna8s)),ref _5l1tna8s );
Mark180:;
									// continue
								}
																}							}
						}
						
					}
					else
					{
						//* 
						//*              ==== Updates exploiting U's 2-by-2 block structure. 
						//*              .    (I2, I4, J2, J4 are the last rows and columns 
						//*              .    of the blocks.) ==== 
						//* 
						
						_8ur10vsh = ((_5c51t51g + (int)1) / (int)2);
						_3xy4w22e = _5c51t51g;
						_psodi8to = (_3xy4w22e - _8ur10vsh);
						_h5f9ahvx = _5c51t51g;//* 
						//*              ==== KZS and KNZ deal with the band of zeros 
						//*              .    along the diagonal of one of the triangular 
						//*              .    blocks. ==== 
						//* 
						
						_taoaib9a = ((_h5f9ahvx - _psodi8to) - (_edl2gwc7 + (int)1));
						_dq4z5uer = (_edl2gwc7 + (int)1);//* 
						//*              ==== Horizontal multiply ==== 
						//* 
						
						{
							System.Int32 __81fgg2dlsvn2440 = (System.Int32)((ILNumerics.F2NET.Intrinsics.MIN(_zgqqw8ka ,_7d8gh478 ) + (int)1));
							System.Int32 __81fgg2step2440 = (System.Int32)(_aym8a085);
							System.Int32 __81fgg2count2440;
							for (__81fgg2count2440 = System.Math.Max(0, (System.Int32)(((System.Int32)(_vefcha38) - __81fgg2dlsvn2440 + __81fgg2step2440) / __81fgg2step2440)), _vmeebitr = __81fgg2dlsvn2440; __81fgg2count2440 != 0; __81fgg2count2440--, _vmeebitr += (__81fgg2step2440)) {

							{
								
								_pl5v4w0g = ILNumerics.F2NET.Intrinsics.MIN(_aym8a085 ,(_vefcha38 - _vmeebitr) + (int)1 );//* 
								//*                 ==== Copy bottom of H to top+KZS of scratch ==== 
								//*                  (The first KZS rows get multiplied by zero.) ==== 
								//* 
								
								_m38y8dyg("ALL" ,ref _dq4z5uer ,ref _pl5v4w0g ,(_ogkjl6gu+((_zjghysh4 + (int)1) + _psodi8to - 1) + (_vmeebitr - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,(_gxe16fct+(_taoaib9a + (int)1 - 1) + ((int)1 - 1) * 1 * (_gqb11x6f)),ref _gqb11x6f );//* 
								//*                 ==== Multiply by U21**T ==== 
								//* 
								
								_t013e1c8("ALL" ,ref _taoaib9a ,ref _pl5v4w0g ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,_gxe16fct ,ref _gqb11x6f );
								_sdtp2num("L" ,"U" ,"C" ,"N" ,ref _dq4z5uer ,ref _pl5v4w0g ,ref Unsafe.AsRef(_kxg5drh2) ,(_7u55mqkq+(_psodi8to + (int)1 - 1) + ((int)1 + _taoaib9a - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_gxe16fct+(_taoaib9a + (int)1 - 1) + ((int)1 - 1) * 1 * (_gqb11x6f)),ref _gqb11x6f );//* 
								//*                 ==== Multiply top of H by U11**T ==== 
								//* 
								
								_b8wa9454("C" ,"N" ,ref _8ur10vsh ,ref _pl5v4w0g ,ref _psodi8to ,ref Unsafe.AsRef(_kxg5drh2) ,_7u55mqkq ,ref _u6e6d39b ,(_ogkjl6gu+(_zjghysh4 + (int)1 - 1) + (_vmeebitr - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,ref Unsafe.AsRef(_kxg5drh2) ,_gxe16fct ,ref _gqb11x6f );//* 
								//*                 ==== Copy top of H to bottom of WH ==== 
								//* 
								
								_m38y8dyg("ALL" ,ref _psodi8to ,ref _pl5v4w0g ,(_ogkjl6gu+(_zjghysh4 + (int)1 - 1) + (_vmeebitr - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,(_gxe16fct+(_8ur10vsh + (int)1 - 1) + ((int)1 - 1) * 1 * (_gqb11x6f)),ref _gqb11x6f );//* 
								//*                 ==== Multiply by U21**T ==== 
								//* 
								
								_sdtp2num("L" ,"L" ,"C" ,"N" ,ref _psodi8to ,ref _pl5v4w0g ,ref Unsafe.AsRef(_kxg5drh2) ,(_7u55mqkq+((int)1 - 1) + (_8ur10vsh + (int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_gxe16fct+(_8ur10vsh + (int)1 - 1) + ((int)1 - 1) * 1 * (_gqb11x6f)),ref _gqb11x6f );//* 
								//*                 ==== Multiply by U22 ==== 
								//* 
								
								_b8wa9454("C" ,"N" ,ref Unsafe.AsRef(_3xy4w22e - _8ur10vsh) ,ref _pl5v4w0g ,ref Unsafe.AsRef(_h5f9ahvx - _psodi8to) ,ref Unsafe.AsRef(_kxg5drh2) ,(_7u55mqkq+(_psodi8to + (int)1 - 1) + (_8ur10vsh + (int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_ogkjl6gu+((_zjghysh4 + (int)1) + _psodi8to - 1) + (_vmeebitr - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,ref Unsafe.AsRef(_kxg5drh2) ,(_gxe16fct+(_8ur10vsh + (int)1 - 1) + ((int)1 - 1) * 1 * (_gqb11x6f)),ref _gqb11x6f );//* 
								//*                 ==== Copy it back ==== 
								//* 
								
								_m38y8dyg("ALL" ,ref _5c51t51g ,ref _pl5v4w0g ,_gxe16fct ,ref _gqb11x6f ,(_ogkjl6gu+(_zjghysh4 + (int)1 - 1) + (_vmeebitr - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw );
Mark190:;
								// continue
							}
														}						}//* 
						//*              ==== Vertical multiply ==== 
						//* 
						
						{
							System.Int32 __81fgg2dlsvn2441 = (System.Int32)(_3yjg5vld);
							System.Int32 __81fgg2step2441 = (System.Int32)(_aiz0v1d1);
							System.Int32 __81fgg2count2441;
							for (__81fgg2count2441 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MAX(_zjghysh4 ,_4a77vvpa ) - (int)1) - __81fgg2dlsvn2441 + __81fgg2step2441) / __81fgg2step2441)), _hgi9ttgs = __81fgg2dlsvn2441; __81fgg2count2441 != 0; __81fgg2count2441--, _hgi9ttgs += (__81fgg2step2441)) {

							{
								
								_pl5v4w0g = ILNumerics.F2NET.Intrinsics.MIN(_aiz0v1d1 ,ILNumerics.F2NET.Intrinsics.MAX(_zjghysh4 ,_4a77vvpa ) - _hgi9ttgs );//* 
								//*                 ==== Copy right of H to scratch (the first KZS 
								//*                 .    columns get multiplied by zero) ==== 
								//* 
								
								_m38y8dyg("ALL" ,ref _pl5v4w0g ,ref _dq4z5uer ,(_ogkjl6gu+(_hgi9ttgs - 1) + ((_zjghysh4 + (int)1) + _psodi8to - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,(_pm308b4n+((int)1 - 1) + ((int)1 + _taoaib9a - 1) * 1 * (_vlfzpxiy)),ref _vlfzpxiy );//* 
								//*                 ==== Multiply by U21 ==== 
								//* 
								
								_t013e1c8("ALL" ,ref _pl5v4w0g ,ref _taoaib9a ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,_pm308b4n ,ref _vlfzpxiy );
								_sdtp2num("R" ,"U" ,"N" ,"N" ,ref _pl5v4w0g ,ref _dq4z5uer ,ref Unsafe.AsRef(_kxg5drh2) ,(_7u55mqkq+(_psodi8to + (int)1 - 1) + ((int)1 + _taoaib9a - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_pm308b4n+((int)1 - 1) + ((int)1 + _taoaib9a - 1) * 1 * (_vlfzpxiy)),ref _vlfzpxiy );//* 
								//*                 ==== Multiply by U11 ==== 
								//* 
								
								_b8wa9454("N" ,"N" ,ref _pl5v4w0g ,ref _8ur10vsh ,ref _psodi8to ,ref Unsafe.AsRef(_kxg5drh2) ,(_ogkjl6gu+(_hgi9ttgs - 1) + (_zjghysh4 + (int)1 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,_7u55mqkq ,ref _u6e6d39b ,ref Unsafe.AsRef(_kxg5drh2) ,_pm308b4n ,ref _vlfzpxiy );//* 
								//*                 ==== Copy left of H to right of scratch ==== 
								//* 
								
								_m38y8dyg("ALL" ,ref _pl5v4w0g ,ref _psodi8to ,(_ogkjl6gu+(_hgi9ttgs - 1) + (_zjghysh4 + (int)1 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,(_pm308b4n+((int)1 - 1) + ((int)1 + _8ur10vsh - 1) * 1 * (_vlfzpxiy)),ref _vlfzpxiy );//* 
								//*                 ==== Multiply by U21 ==== 
								//* 
								
								_sdtp2num("R" ,"L" ,"N" ,"N" ,ref _pl5v4w0g ,ref Unsafe.AsRef(_3xy4w22e - _8ur10vsh) ,ref Unsafe.AsRef(_kxg5drh2) ,(_7u55mqkq+((int)1 - 1) + (_8ur10vsh + (int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_pm308b4n+((int)1 - 1) + ((int)1 + _8ur10vsh - 1) * 1 * (_vlfzpxiy)),ref _vlfzpxiy );//* 
								//*                 ==== Multiply by U22 ==== 
								//* 
								
								_b8wa9454("N" ,"N" ,ref _pl5v4w0g ,ref Unsafe.AsRef(_3xy4w22e - _8ur10vsh) ,ref Unsafe.AsRef(_h5f9ahvx - _psodi8to) ,ref Unsafe.AsRef(_kxg5drh2) ,(_ogkjl6gu+(_hgi9ttgs - 1) + ((_zjghysh4 + (int)1) + _psodi8to - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,(_7u55mqkq+(_psodi8to + (int)1 - 1) + (_8ur10vsh + (int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,ref Unsafe.AsRef(_kxg5drh2) ,(_pm308b4n+((int)1 - 1) + ((int)1 + _8ur10vsh - 1) * 1 * (_vlfzpxiy)),ref _vlfzpxiy );//* 
								//*                 ==== Copy it back ==== 
								//* 
								
								_m38y8dyg("ALL" ,ref _pl5v4w0g ,ref _5c51t51g ,_pm308b4n ,ref _vlfzpxiy ,(_ogkjl6gu+(_hgi9ttgs - 1) + (_zjghysh4 + (int)1 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw );
Mark200:;
								// continue
							}
														}						}//* 
						//*              ==== Multiply Z (also vertical) ==== 
						//* 
						
						if (_189gzykk)
						{
							
							{
								System.Int32 __81fgg2dlsvn2442 = (System.Int32)(_pinc1ofz);
								System.Int32 __81fgg2step2442 = (System.Int32)(_aiz0v1d1);
								System.Int32 __81fgg2count2442;
								for (__81fgg2count2442 = System.Math.Max(0, (System.Int32)(((System.Int32)(_mg9v9w4h) - __81fgg2dlsvn2442 + __81fgg2step2442) / __81fgg2step2442)), _hgi9ttgs = __81fgg2dlsvn2442; __81fgg2count2442 != 0; __81fgg2count2442--, _hgi9ttgs += (__81fgg2step2442)) {

								{
									
									_pl5v4w0g = ILNumerics.F2NET.Intrinsics.MIN(_aiz0v1d1 ,(_mg9v9w4h - _hgi9ttgs) + (int)1 );//* 
									//*                    ==== Copy right of Z to left of scratch (first 
									//*                    .     KZS columns get multiplied by zero) ==== 
									//* 
									
									_m38y8dyg("ALL" ,ref _pl5v4w0g ,ref _dq4z5uer ,(_7e60fcso+(_hgi9ttgs - 1) + ((_zjghysh4 + (int)1) + _psodi8to - 1) * 1 * (_5l1tna8s)),ref _5l1tna8s ,(_pm308b4n+((int)1 - 1) + ((int)1 + _taoaib9a - 1) * 1 * (_vlfzpxiy)),ref _vlfzpxiy );//* 
									//*                    ==== Multiply by U12 ==== 
									//* 
									
									_t013e1c8("ALL" ,ref _pl5v4w0g ,ref _taoaib9a ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,_pm308b4n ,ref _vlfzpxiy );
									_sdtp2num("R" ,"U" ,"N" ,"N" ,ref _pl5v4w0g ,ref _dq4z5uer ,ref Unsafe.AsRef(_kxg5drh2) ,(_7u55mqkq+(_psodi8to + (int)1 - 1) + ((int)1 + _taoaib9a - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_pm308b4n+((int)1 - 1) + ((int)1 + _taoaib9a - 1) * 1 * (_vlfzpxiy)),ref _vlfzpxiy );//* 
									//*                    ==== Multiply by U11 ==== 
									//* 
									
									_b8wa9454("N" ,"N" ,ref _pl5v4w0g ,ref _8ur10vsh ,ref _psodi8to ,ref Unsafe.AsRef(_kxg5drh2) ,(_7e60fcso+(_hgi9ttgs - 1) + (_zjghysh4 + (int)1 - 1) * 1 * (_5l1tna8s)),ref _5l1tna8s ,_7u55mqkq ,ref _u6e6d39b ,ref Unsafe.AsRef(_kxg5drh2) ,_pm308b4n ,ref _vlfzpxiy );//* 
									//*                    ==== Copy left of Z to right of scratch ==== 
									//* 
									
									_m38y8dyg("ALL" ,ref _pl5v4w0g ,ref _psodi8to ,(_7e60fcso+(_hgi9ttgs - 1) + (_zjghysh4 + (int)1 - 1) * 1 * (_5l1tna8s)),ref _5l1tna8s ,(_pm308b4n+((int)1 - 1) + ((int)1 + _8ur10vsh - 1) * 1 * (_vlfzpxiy)),ref _vlfzpxiy );//* 
									//*                    ==== Multiply by U21 ==== 
									//* 
									
									_sdtp2num("R" ,"L" ,"N" ,"N" ,ref _pl5v4w0g ,ref Unsafe.AsRef(_3xy4w22e - _8ur10vsh) ,ref Unsafe.AsRef(_kxg5drh2) ,(_7u55mqkq+((int)1 - 1) + (_8ur10vsh + (int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,(_pm308b4n+((int)1 - 1) + ((int)1 + _8ur10vsh - 1) * 1 * (_vlfzpxiy)),ref _vlfzpxiy );//* 
									//*                    ==== Multiply by U22 ==== 
									//* 
									
									_b8wa9454("N" ,"N" ,ref _pl5v4w0g ,ref Unsafe.AsRef(_3xy4w22e - _8ur10vsh) ,ref Unsafe.AsRef(_h5f9ahvx - _psodi8to) ,ref Unsafe.AsRef(_kxg5drh2) ,(_7e60fcso+(_hgi9ttgs - 1) + ((_zjghysh4 + (int)1) + _psodi8to - 1) * 1 * (_5l1tna8s)),ref _5l1tna8s ,(_7u55mqkq+(_psodi8to + (int)1 - 1) + (_8ur10vsh + (int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b ,ref Unsafe.AsRef(_kxg5drh2) ,(_pm308b4n+((int)1 - 1) + ((int)1 + _8ur10vsh - 1) * 1 * (_vlfzpxiy)),ref _vlfzpxiy );//* 
									//*                    ==== Copy the result back to Z ==== 
									//* 
									
									_m38y8dyg("ALL" ,ref _pl5v4w0g ,ref _5c51t51g ,_pm308b4n ,ref _vlfzpxiy ,(_7e60fcso+(_hgi9ttgs - 1) + (_zjghysh4 + (int)1 - 1) * 1 * (_5l1tna8s)),ref _5l1tna8s );
Mark210:;
									// continue
								}
																}							}
						}
						
					}
					
				}
				
Mark220:;
				// continue
			}
						}		}//* 
		//*     ==== End of SLAQR5 ==== 
		//* 
		
	}
	
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
