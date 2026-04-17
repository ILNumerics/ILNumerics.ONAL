
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
//*> \brief \b SLAHQR computes the eigenvalues and Schur factorization of an upper Hessenberg matrix, using the double-shift/single-shift QR algorithm. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLAHQR + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slahqr.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slahqr.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slahqr.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLAHQR( WANTT, WANTZ, N, ILO, IHI, H, LDH, WR, WI, 
//*                          ILOZ, IHIZ, Z, LDZ, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            IHI, IHIZ, ILO, ILOZ, INFO, LDH, LDZ, N 
//*       LOGICAL            WANTT, WANTZ 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               H( LDH, * ), WI( * ), WR( * ), Z( LDZ, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*>    SLAHQR is an auxiliary routine called by SHSEQR to update the 
//*>    eigenvalues and Schur decomposition already computed by SHSEQR, by 
//*>    dealing with the Hessenberg submatrix in rows and columns ILO to 
//*>    IHI. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] WANTT 
//*> \verbatim 
//*>          WANTT is LOGICAL 
//*>          = .TRUE. : the full Schur form T is required; 
//*>          = .FALSE.: only eigenvalues are required. 
//*> \endverbatim 
//*> 
//*> \param[in] WANTZ 
//*> \verbatim 
//*>          WANTZ is LOGICAL 
//*>          = .TRUE. : the matrix of Schur vectors Z is required; 
//*>          = .FALSE.: Schur vectors are not required. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix H.  N >= 0. 
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
//*>          It is assumed that H is already upper quasi-triangular in 
//*>          rows and columns IHI+1:N, and that H(ILO,ILO-1) = 0 (unless 
//*>          ILO = 1). SLAHQR works primarily with the Hessenberg 
//*>          submatrix in rows and columns ILO to IHI, but applies 
//*>          transformations to all of H if WANTT is .TRUE.. 
//*>          1 <= ILO <= max(1,IHI); IHI <= N. 
//*> \endverbatim 
//*> 
//*> \param[in,out] H 
//*> \verbatim 
//*>          H is REAL array, dimension (LDH,N) 
//*>          On entry, the upper Hessenberg matrix H. 
//*>          On exit, if INFO is zero and if WANTT is .TRUE., H is upper 
//*>          quasi-triangular in rows and columns ILO:IHI, with any 
//*>          2-by-2 diagonal blocks in standard form. If INFO is zero 
//*>          and WANTT is .FALSE., the contents of H are unspecified on 
//*>          exit.  The output state of H if INFO is nonzero is given 
//*>          below under the description of INFO. 
//*> \endverbatim 
//*> 
//*> \param[in] LDH 
//*> \verbatim 
//*>          LDH is INTEGER 
//*>          The leading dimension of the array H. LDH >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[out] WR 
//*> \verbatim 
//*>          WR is REAL array, dimension (N) 
//*> \endverbatim 
//*> 
//*> \param[out] WI 
//*> \verbatim 
//*>          WI is REAL array, dimension (N) 
//*>          The real and imaginary parts, respectively, of the computed 
//*>          eigenvalues ILO to IHI are stored in the corresponding 
//*>          elements of WR and WI. If two eigenvalues are computed as a 
//*>          complex conjugate pair, they are stored in consecutive 
//*>          elements of WR and WI, say the i-th and (i+1)th, with 
//*>          WI(i) > 0 and WI(i+1) < 0. If WANTT is .TRUE., the 
//*>          eigenvalues are stored in the same order as on the diagonal 
//*>          of the Schur form returned in H, with WR(i) = H(i,i), and, if 
//*>          H(i:i+1,i:i+1) is a 2-by-2 diagonal block, 
//*>          WI(i) = sqrt(H(i+1,i)*H(i,i+1)) and WI(i+1) = -WI(i). 
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
//*>          applied if WANTZ is .TRUE.. 
//*>          1 <= ILOZ <= ILO; IHI <= IHIZ <= N. 
//*> \endverbatim 
//*> 
//*> \param[in,out] Z 
//*> \verbatim 
//*>          Z is REAL array, dimension (LDZ,N) 
//*>          If WANTZ is .TRUE., on entry Z must contain the current 
//*>          matrix Z of transformations accumulated by SHSEQR, and on 
//*>          exit Z has been updated; transformations are applied only to 
//*>          the submatrix Z(ILOZ:IHIZ,ILO:IHI). 
//*>          If WANTZ is .FALSE., Z is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] LDZ 
//*> \verbatim 
//*>          LDZ is INTEGER 
//*>          The leading dimension of the array Z. LDZ >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>           = 0:   successful exit 
//*>           > 0:   If INFO = i, SLAHQR failed to compute all the 
//*>                  eigenvalues ILO to IHI in a total of 30 iterations 
//*>                  per eigenvalue; elements i+1:ihi of WR and WI 
//*>                  contain those eigenvalues which have been 
//*>                  successfully computed. 
//*> 
//*>                  If INFO > 0 and WANTT is .FALSE., then on exit, 
//*>                  the remaining unconverged eigenvalues are the 
//*>                  eigenvalues of the upper Hessenberg matrix rows 
//*>                  and columns ILO through INFO of the final, output 
//*>                  value of H. 
//*> 
//*>                  If INFO > 0 and WANTT is .TRUE., then on exit 
//*>          (*)       (initial value of H)*U  = U*(final value of H) 
//*>                  where U is an orthogonal matrix.    The final 
//*>                  value of H is upper Hessenberg and triangular in 
//*>                  rows and columns INFO+1 through IHI. 
//*> 
//*>                  If INFO > 0 and WANTZ is .TRUE., then on exit 
//*>                      (final value of Z)  = (initial value of Z)*U 
//*>                  where U is the orthogonal matrix in (*) 
//*>                  (regardless of the value of WANTT.) 
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
//*> \ingroup realOTHERauxiliary 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>     02-96 Based on modifications by 
//*>     David Day, Sandia National Laboratory, USA 
//*> 
//*>     12-04 Further modifications by 
//*>     Ralph Byers, University of Kansas, USA 
//*>     This is a modified version of SLAHQR from LAPACK version 3.0. 
//*>     It is (1) more robust against overflow and underflow and 
//*>     (2) adopts the more conservative Ahues & Tisseur stopping 
//*>     criterion (LAWN 122, 1997). 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _7ds1b9hb(ref Boolean _amt8y1zm, ref Boolean _189gzykk, ref Int32 _dxpq0xkr, ref Int32 _pew3blan, ref Int32 _9c1csucx, Single* _ogkjl6gu, ref Int32 _1iekxpnw, Single* _b5j6m2b7, Single* _nc0qphpn, ref Int32 _pinc1ofz, ref Int32 _mg9v9w4h, Single* _7e60fcso, ref Int32 _5l1tna8s, ref Int32 _gro5yvfo)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)12 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
Single _5m0mjfxm =  2f;
Single _av6f7vuf =  3f / 4f;
Single _j7jyjdw9 =  -0.4375f;
Single _zwm0s9sq =  default;
Single _9tcc7f14 =  default;
Single _7v8y2qii =  default;
Single _uv0s0qmf =  default;
Single _82tpdhyl =  default;
Single _wo1bf490 =  default;
Single _j4eznmsw =  default;
Single _54yqyd88 =  default;
Single _sq0bv6ma =  default;
Single _3rsvj09v =  default;
Single _p1s2d3vl =  default;
Single _xoempplp =  default;
Single _iinphjl3 =  default;
Single _w3mf666f =  default;
Single _9l52vb05 =  default;
Single _ogo0zwlp =  default;
Single _irk8i6qr =  default;
Single _odf6ja0t =  default;
Single _h75qnr7l =  default;
Single _bogm0gwy =  default;
Single _8tmd0ner =  default;
Single _6j9l5fwy =  default;
Single _dkyrz0g2 =  default;
Single _jzq4t39k =  default;
Single _sihp0s0u =  default;
Single _fy8po3y0 =  default;
Single _ts63qxkr =  default;
Single _0h4yb5wu =  default;
Single _j3ir6imc =  default;
Single _s8neut4h =  default;
Int32 _b5p6od9s =  default;
Int32 _egqdmelt =  default;
Int32 _8ur10vsh =  default;
Int32 _qrxtt59n =  default;
Int32 _7u74ue5o =  default;
Int32 _znpjgsef =  default;
Int32 _umlkckdg =  default;
Int32 _68ec3gbh =  default;
Int32 _ev4xhht5 =  default;
Int32 _aym8a085 =  default;
Int32 _oqpc3yjg =  default;
Int32 _iq0q8194 =  default;
Single* _ycxba85s =  (Single*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Single) * ((int)3);
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
		//*  ========================================================= 
		//* 
		//*     .. Parameters .. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Local Arrays .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		_gro5yvfo = (int)0;//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr == (int)0)
		return;
		if (_pew3blan == _9c1csucx)
		{
			
			*(_b5j6m2b7+(_pew3blan - 1)) = *(_ogkjl6gu+(_pew3blan - 1) + (_pew3blan - 1) * 1 * (_1iekxpnw));
			*(_nc0qphpn+(_pew3blan - 1)) = _d0547bi2;
			return;
		}
		//* 
		//*     ==== clear out the trash ==== 
		
		{
			System.Int32 __81fgg2dlsvn2362 = (System.Int32)(_pew3blan);
			const System.Int32 __81fgg2step2362 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2362;
			for (__81fgg2count2362 = System.Math.Max(0, (System.Int32)(((System.Int32)(_9c1csucx - (int)3) - __81fgg2dlsvn2362 + __81fgg2step2362) / __81fgg2step2362)), _znpjgsef = __81fgg2dlsvn2362; __81fgg2count2362 != 0; __81fgg2count2362--, _znpjgsef += (__81fgg2step2362)) {

			{
				
				*(_ogkjl6gu+(_znpjgsef + (int)2 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) = _d0547bi2;
				*(_ogkjl6gu+(_znpjgsef + (int)3 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) = _d0547bi2;
Mark10:;
				// continue
			}
						}		}
		if (_pew3blan <= (_9c1csucx - (int)2))
		*(_ogkjl6gu+(_9c1csucx - 1) + (_9c1csucx - (int)2 - 1) * 1 * (_1iekxpnw)) = _d0547bi2;//* 
		
		_aym8a085 = ((_9c1csucx - _pew3blan) + (int)1);
		_iq0q8194 = ((_mg9v9w4h - _pinc1ofz) + (int)1);//* 
		//*     Set machine-dependent constants for the stopping criterion. 
		//* 
		
		_h75qnr7l = _d5tu038y("SAFE MINIMUM" );
		_odf6ja0t = (_kxg5drh2 / _h75qnr7l);
		_6cljvt6b(ref _h75qnr7l ,ref _odf6ja0t );
		_0h4yb5wu = _d5tu038y("PRECISION" );
		_bogm0gwy = (_h75qnr7l * (ILNumerics.F2NET.Intrinsics.REAL(_aym8a085 ) / _0h4yb5wu));//* 
		//*     I1 and I2 are the indices of the first row and last column of H 
		//*     to which transformations must be applied. If eigenvalues only are 
		//*     being computed, I1 and I2 are set inside the main loop. 
		//* 
		
		if (_amt8y1zm)
		{
			
			_egqdmelt = (int)1;
			_8ur10vsh = _dxpq0xkr;
		}
		//* 
		//*     ITMAX is the total number of QR iterations allowed. 
		//* 
		
		_7u74ue5o = ((int)30 * ILNumerics.F2NET.Intrinsics.MAX((int)10 ,_aym8a085 ));//* 
		//*     The main loop begins here. I is the loop index and decreases from 
		//*     IHI to ILO in steps of 1 or 2. Each iteration of the loop works 
		//*     with the active submatrix in rows and columns L to I. 
		//*     Eigenvalues I+1 to IHI have already converged. Either L = ILO or 
		//*     H(L,L-1) is negligible so that the matrix splits. 
		//* 
		
		_b5p6od9s = _9c1csucx;
Mark20:;
		// continue
		_68ec3gbh = _pew3blan;
		if (_b5p6od9s < _pew3blan)goto Mark160;//* 
		//*     Perform QR iterations on rows and columns ILO to I until a 
		//*     submatrix of order 1 or 2 splits off at the bottom because a 
		//*     subdiagonal element has become negligible. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn2363 = (System.Int32)((int)0);
			const System.Int32 __81fgg2step2363 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2363;
			for (__81fgg2count2363 = System.Math.Max(0, (System.Int32)(((System.Int32)(_7u74ue5o) - __81fgg2dlsvn2363 + __81fgg2step2363) / __81fgg2step2363)), _qrxtt59n = __81fgg2dlsvn2363; __81fgg2count2363 != 0; __81fgg2count2363--, _qrxtt59n += (__81fgg2step2363)) {

			{
				//* 
				//*        Look for a single small subdiagonal element. 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn2364 = (System.Int32)(_b5p6od9s);
					System.Int32 __81fgg2step2364 = (System.Int32)((int)-1);
					System.Int32 __81fgg2count2364;
					for (__81fgg2count2364 = System.Math.Max(0, (System.Int32)(((System.Int32)(_68ec3gbh + (int)1) - __81fgg2dlsvn2364 + __81fgg2step2364) / __81fgg2step2364)), _umlkckdg = __81fgg2dlsvn2364; __81fgg2count2364 != 0; __81fgg2count2364--, _umlkckdg += (__81fgg2step2364)) {

					{
						
						if (ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - (int)1 - 1) * 1 * (_1iekxpnw)) ) <= _bogm0gwy)goto Mark40;
						_ts63qxkr = (ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg - (int)1 - 1) + (_umlkckdg - (int)1 - 1) * 1 * (_1iekxpnw)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) ));
						if (_ts63qxkr == _d0547bi2)
						{
							
							if ((_umlkckdg - (int)2) >= _pew3blan)
							_ts63qxkr = (_ts63qxkr + ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg - (int)1 - 1) + (_umlkckdg - (int)2 - 1) * 1 * (_1iekxpnw)) ));
							if ((_umlkckdg + (int)1) <= _9c1csucx)
							_ts63qxkr = (_ts63qxkr + ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) ));
						}
						//*           ==== The following is a conservative small subdiagonal 
						//*           .    deflation  criterion due to Ahues & Tisseur (LAWN 122, 
						//*           .    1997). It has better mathematical foundation and 
						//*           .    improves accuracy in some cases.  ==== 
						
						if (ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - (int)1 - 1) * 1 * (_1iekxpnw)) ) <= (_0h4yb5wu * _ts63qxkr))
						{
							
							_9tcc7f14 = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - (int)1 - 1) * 1 * (_1iekxpnw)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg - (int)1 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) ) );
							_7v8y2qii = ILNumerics.F2NET.Intrinsics.MIN(ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - (int)1 - 1) * 1 * (_1iekxpnw)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg - (int)1 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) ) );
							_zwm0s9sq = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg - (int)1 - 1) + (_umlkckdg - (int)1 - 1) * 1 * (_1iekxpnw)) - *(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) ) );
							_uv0s0qmf = ILNumerics.F2NET.Intrinsics.MIN(ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_umlkckdg - (int)1 - 1) + (_umlkckdg - (int)1 - 1) * 1 * (_1iekxpnw)) - *(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) ) );
							_irk8i6qr = (_zwm0s9sq + _9tcc7f14);
							if ((_7v8y2qii * (_9tcc7f14 / _irk8i6qr)) <= ILNumerics.F2NET.Intrinsics.MAX(_bogm0gwy ,_0h4yb5wu * (_uv0s0qmf * (_zwm0s9sq / _irk8i6qr)) ))goto Mark40;
						}
						
Mark30:;
						// continue
					}
										}				}
Mark40:;
				// continue
				_68ec3gbh = _umlkckdg;
				if (_68ec3gbh > _pew3blan)
				{
					//* 
					//*           H(L,L-1) is negligible 
					//* 
					
					*(_ogkjl6gu+(_68ec3gbh - 1) + (_68ec3gbh - (int)1 - 1) * 1 * (_1iekxpnw)) = _d0547bi2;
				}
				//* 
				//*        Exit from loop if a submatrix of order 1 or 2 has split off. 
				//* 
				
				if (_68ec3gbh >= (_b5p6od9s - (int)1))goto Mark150;//* 
				//*        Now the active submatrix is in rows and columns L to I. If 
				//*        eigenvalues only are being computed, only the active submatrix 
				//*        need be transformed. 
				//* 
				
				if (!(_amt8y1zm))
				{
					
					_egqdmelt = _68ec3gbh;
					_8ur10vsh = _b5p6od9s;
				}
				//* 
				
				if (_qrxtt59n == (int)10)
				{
					//* 
					//*           Exceptional shift. 
					//* 
					
					_irk8i6qr = (ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_68ec3gbh + (int)1 - 1) + (_68ec3gbh - 1) * 1 * (_1iekxpnw)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_68ec3gbh + (int)2 - 1) + (_68ec3gbh + (int)1 - 1) * 1 * (_1iekxpnw)) ));
					_j4eznmsw = ((_av6f7vuf * _irk8i6qr) + *(_ogkjl6gu+(_68ec3gbh - 1) + (_68ec3gbh - 1) * 1 * (_1iekxpnw)));
					_54yqyd88 = (_j7jyjdw9 * _irk8i6qr);
					_sq0bv6ma = _irk8i6qr;
					_p1s2d3vl = _j4eznmsw;
				}
				else
				if (_qrxtt59n == (int)20)
				{
					//* 
					//*           Exceptional shift. 
					//* 
					
					_irk8i6qr = (ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_b5p6od9s - 1) + (_b5p6od9s - (int)1 - 1) * 1 * (_1iekxpnw)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_b5p6od9s - (int)1 - 1) + (_b5p6od9s - (int)2 - 1) * 1 * (_1iekxpnw)) ));
					_j4eznmsw = ((_av6f7vuf * _irk8i6qr) + *(_ogkjl6gu+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_1iekxpnw)));
					_54yqyd88 = (_j7jyjdw9 * _irk8i6qr);
					_sq0bv6ma = _irk8i6qr;
					_p1s2d3vl = _j4eznmsw;
				}
				else
				{
					//* 
					//*           Prepare to use Francis' double shift 
					//*           (i.e. 2nd degree generalized Rayleigh quotient) 
					//* 
					
					_j4eznmsw = *(_ogkjl6gu+(_b5p6od9s - (int)1 - 1) + (_b5p6od9s - (int)1 - 1) * 1 * (_1iekxpnw));
					_sq0bv6ma = *(_ogkjl6gu+(_b5p6od9s - 1) + (_b5p6od9s - (int)1 - 1) * 1 * (_1iekxpnw));
					_54yqyd88 = *(_ogkjl6gu+(_b5p6od9s - (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_1iekxpnw));
					_p1s2d3vl = *(_ogkjl6gu+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_1iekxpnw));
				}
				
				_irk8i6qr = (((ILNumerics.F2NET.Intrinsics.ABS(_j4eznmsw ) + ILNumerics.F2NET.Intrinsics.ABS(_54yqyd88 )) + ILNumerics.F2NET.Intrinsics.ABS(_sq0bv6ma )) + ILNumerics.F2NET.Intrinsics.ABS(_p1s2d3vl ));
				if (_irk8i6qr == _d0547bi2)
				{
					
					_iinphjl3 = _d0547bi2;
					_xoempplp = _d0547bi2;
					_9l52vb05 = _d0547bi2;
					_w3mf666f = _d0547bi2;
				}
				else
				{
					
					_j4eznmsw = (_j4eznmsw / _irk8i6qr);
					_sq0bv6ma = (_sq0bv6ma / _irk8i6qr);
					_54yqyd88 = (_54yqyd88 / _irk8i6qr);
					_p1s2d3vl = (_p1s2d3vl / _irk8i6qr);
					_fy8po3y0 = ((_j4eznmsw + _p1s2d3vl) / _5m0mjfxm);
					_wo1bf490 = (((_j4eznmsw - _fy8po3y0) * (_p1s2d3vl - _fy8po3y0)) - (_54yqyd88 * _sq0bv6ma));
					_ogo0zwlp = ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(_wo1bf490 ) );
					if (_wo1bf490 >= _d0547bi2)
					{
						//* 
						//*              ==== complex conjugate shifts ==== 
						//* 
						
						_iinphjl3 = (_fy8po3y0 * _irk8i6qr);
						_9l52vb05 = _iinphjl3;
						_xoempplp = (_ogo0zwlp * _irk8i6qr);
						_w3mf666f = (-(_xoempplp));
					}
					else
					{
						//* 
						//*              ==== real shifts (use only one of them)  ==== 
						//* 
						
						_iinphjl3 = (_fy8po3y0 + _ogo0zwlp);
						_9l52vb05 = (_fy8po3y0 - _ogo0zwlp);
						if (ILNumerics.F2NET.Intrinsics.ABS(_iinphjl3 - _p1s2d3vl ) <= ILNumerics.F2NET.Intrinsics.ABS(_9l52vb05 - _p1s2d3vl ))
						{
							
							_iinphjl3 = (_iinphjl3 * _irk8i6qr);
							_9l52vb05 = _iinphjl3;
						}
						else
						{
							
							_9l52vb05 = (_9l52vb05 * _irk8i6qr);
							_iinphjl3 = _9l52vb05;
						}
						
						_xoempplp = _d0547bi2;
						_w3mf666f = _d0547bi2;
					}
					
				}
				//* 
				//*        Look for two consecutive small subdiagonal elements. 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn2365 = (System.Int32)((_b5p6od9s - (int)2));
					System.Int32 __81fgg2step2365 = (System.Int32)((int)-1);
					System.Int32 __81fgg2count2365;
					for (__81fgg2count2365 = System.Math.Max(0, (System.Int32)(((System.Int32)(_68ec3gbh) - __81fgg2dlsvn2365 + __81fgg2step2365) / __81fgg2step2365)), _ev4xhht5 = __81fgg2dlsvn2365; __81fgg2count2365 != 0; __81fgg2count2365--, _ev4xhht5 += (__81fgg2step2365)) {

					{
						//*           Determine the effect of starting the double-shift QR 
						//*           iteration at row M, and see if this would make H(M,M-1) 
						//*           negligible.  (The following uses scaling to avoid 
						//*           overflows and most underflows.) 
						//* 
						
						_3rsvj09v = *(_ogkjl6gu+(_ev4xhht5 + (int)1 - 1) + (_ev4xhht5 - 1) * 1 * (_1iekxpnw));
						_irk8i6qr = ((ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_ev4xhht5 - 1) + (_ev4xhht5 - 1) * 1 * (_1iekxpnw)) - _9l52vb05 ) + ILNumerics.F2NET.Intrinsics.ABS(_w3mf666f )) + ILNumerics.F2NET.Intrinsics.ABS(_3rsvj09v ));
						_3rsvj09v = (*(_ogkjl6gu+(_ev4xhht5 + (int)1 - 1) + (_ev4xhht5 - 1) * 1 * (_1iekxpnw)) / _irk8i6qr);
						*(_ycxba85s+((int)1 - 1)) = (((_3rsvj09v * *(_ogkjl6gu+(_ev4xhht5 - 1) + (_ev4xhht5 + (int)1 - 1) * 1 * (_1iekxpnw))) + ((*(_ogkjl6gu+(_ev4xhht5 - 1) + (_ev4xhht5 - 1) * 1 * (_1iekxpnw)) - _iinphjl3) * ((*(_ogkjl6gu+(_ev4xhht5 - 1) + (_ev4xhht5 - 1) * 1 * (_1iekxpnw)) - _9l52vb05) / _irk8i6qr))) - (_xoempplp * (_w3mf666f / _irk8i6qr)));
						*(_ycxba85s+((int)2 - 1)) = (_3rsvj09v * (((*(_ogkjl6gu+(_ev4xhht5 - 1) + (_ev4xhht5 - 1) * 1 * (_1iekxpnw)) + *(_ogkjl6gu+(_ev4xhht5 + (int)1 - 1) + (_ev4xhht5 + (int)1 - 1) * 1 * (_1iekxpnw))) - _iinphjl3) - _9l52vb05));
						*(_ycxba85s+((int)3 - 1)) = (_3rsvj09v * *(_ogkjl6gu+(_ev4xhht5 + (int)2 - 1) + (_ev4xhht5 + (int)1 - 1) * 1 * (_1iekxpnw)));
						_irk8i6qr = ((ILNumerics.F2NET.Intrinsics.ABS(*(_ycxba85s+((int)1 - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_ycxba85s+((int)2 - 1)) )) + ILNumerics.F2NET.Intrinsics.ABS(*(_ycxba85s+((int)3 - 1)) ));
						*(_ycxba85s+((int)1 - 1)) = (*(_ycxba85s+((int)1 - 1)) / _irk8i6qr);
						*(_ycxba85s+((int)2 - 1)) = (*(_ycxba85s+((int)2 - 1)) / _irk8i6qr);
						*(_ycxba85s+((int)3 - 1)) = (*(_ycxba85s+((int)3 - 1)) / _irk8i6qr);
						if (_ev4xhht5 == _68ec3gbh)goto Mark60;
						if ((ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_ev4xhht5 - 1) + (_ev4xhht5 - (int)1 - 1) * 1 * (_1iekxpnw)) ) * (ILNumerics.F2NET.Intrinsics.ABS(*(_ycxba85s+((int)2 - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_ycxba85s+((int)3 - 1)) ))) <= ((_0h4yb5wu * ILNumerics.F2NET.Intrinsics.ABS(*(_ycxba85s+((int)1 - 1)) )) * ((ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_ev4xhht5 - (int)1 - 1) + (_ev4xhht5 - (int)1 - 1) * 1 * (_1iekxpnw)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_ev4xhht5 - 1) + (_ev4xhht5 - 1) * 1 * (_1iekxpnw)) )) + ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_ev4xhht5 + (int)1 - 1) + (_ev4xhht5 + (int)1 - 1) * 1 * (_1iekxpnw)) ))))goto Mark60;
Mark50:;
						// continue
					}
										}				}
Mark60:;
				// continue//* 
				//*        Double-shift QR step 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn2366 = (System.Int32)(_ev4xhht5);
					const System.Int32 __81fgg2step2366 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2366;
					for (__81fgg2count2366 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn2366 + __81fgg2step2366) / __81fgg2step2366)), _umlkckdg = __81fgg2dlsvn2366; __81fgg2count2366 != 0; __81fgg2count2366--, _umlkckdg += (__81fgg2step2366)) {

					{
						//* 
						//*           The first iteration of this loop determines a reflection G 
						//*           from the vector V and applies it from left and right to H, 
						//*           thus creating a nonzero bulge below the subdiagonal. 
						//* 
						//*           Each subsequent iteration determines a reflection G to 
						//*           restore the Hessenberg form in the (K-1)th column, and thus 
						//*           chases the bulge one step toward the bottom of the active 
						//*           submatrix. NR is the order of G. 
						//* 
						
						_oqpc3yjg = ILNumerics.F2NET.Intrinsics.MIN((int)3 ,(_b5p6od9s - _umlkckdg) + (int)1 );
						if (_umlkckdg > _ev4xhht5)
						_wcs7ne88(ref _oqpc3yjg ,(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - (int)1 - 1) * 1 * (_1iekxpnw)),ref Unsafe.AsRef((int)1) ,_ycxba85s ,ref Unsafe.AsRef((int)1) );
						_mbabw0s0(ref _oqpc3yjg ,ref Unsafe.AsRef(*(_ycxba85s+((int)1 - 1))) ,(_ycxba85s+((int)2 - 1)),ref Unsafe.AsRef((int)1) ,ref _dkyrz0g2 );
						if (_umlkckdg > _ev4xhht5)
						{
							
							*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - (int)1 - 1) * 1 * (_1iekxpnw)) = *(_ycxba85s+((int)1 - 1));
							*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_umlkckdg - (int)1 - 1) * 1 * (_1iekxpnw)) = _d0547bi2;
							if (_umlkckdg < (_b5p6od9s - (int)1))
							*(_ogkjl6gu+(_umlkckdg + (int)2 - 1) + (_umlkckdg - (int)1 - 1) * 1 * (_1iekxpnw)) = _d0547bi2;
						}
						else
						if (_ev4xhht5 > _68ec3gbh)
						{
							//*               ==== Use the following instead of 
							//*               .    H( K, K-1 ) = -H( K, K-1 ) to 
							//*               .    avoid a bug when v(2) and v(3) 
							//*               .    underflow. ==== 
							
							*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - (int)1 - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - (int)1 - 1) * 1 * (_1iekxpnw)) * (_kxg5drh2 - _dkyrz0g2));
						}
						
						_j3ir6imc = *(_ycxba85s+((int)2 - 1));
						_jzq4t39k = (_dkyrz0g2 * _j3ir6imc);
						if (_oqpc3yjg == (int)3)
						{
							
							_s8neut4h = *(_ycxba85s+((int)3 - 1));
							_sihp0s0u = (_dkyrz0g2 * _s8neut4h);//* 
							//*              Apply G from the left to transform the rows of the matrix 
							//*              in columns K to I2. 
							//* 
							
							{
								System.Int32 __81fgg2dlsvn2367 = (System.Int32)(_umlkckdg);
								const System.Int32 __81fgg2step2367 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2367;
								for (__81fgg2count2367 = System.Math.Max(0, (System.Int32)(((System.Int32)(_8ur10vsh) - __81fgg2dlsvn2367 + __81fgg2step2367) / __81fgg2step2367)), _znpjgsef = __81fgg2dlsvn2367; __81fgg2count2367 != 0; __81fgg2count2367--, _znpjgsef += (__81fgg2step2367)) {

								{
									
									_6j9l5fwy = ((*(_ogkjl6gu+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) + (_j3ir6imc * *(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)))) + (_s8neut4h * *(_ogkjl6gu+(_umlkckdg + (int)2 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw))));
									*(_ogkjl6gu+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) - (_6j9l5fwy * _dkyrz0g2));
									*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) - (_6j9l5fwy * _jzq4t39k));
									*(_ogkjl6gu+(_umlkckdg + (int)2 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_umlkckdg + (int)2 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) - (_6j9l5fwy * _sihp0s0u));
Mark70:;
									// continue
								}
																}							}//* 
							//*              Apply G from the right to transform the columns of the 
							//*              matrix in rows I1 to min(K+3,I). 
							//* 
							
							{
								System.Int32 __81fgg2dlsvn2368 = (System.Int32)(_egqdmelt);
								const System.Int32 __81fgg2step2368 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2368;
								for (__81fgg2count2368 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN(_umlkckdg + (int)3 ,_b5p6od9s )) - __81fgg2dlsvn2368 + __81fgg2step2368) / __81fgg2step2368)), _znpjgsef = __81fgg2dlsvn2368; __81fgg2count2368 != 0; __81fgg2count2368--, _znpjgsef += (__81fgg2step2368)) {

								{
									
									_6j9l5fwy = ((*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) + (_j3ir6imc * *(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)))) + (_s8neut4h * *(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)2 - 1) * 1 * (_1iekxpnw))));
									*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) - (_6j9l5fwy * _dkyrz0g2));
									*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)) - (_6j9l5fwy * _jzq4t39k));
									*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)2 - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)2 - 1) * 1 * (_1iekxpnw)) - (_6j9l5fwy * _sihp0s0u));
Mark80:;
									// continue
								}
																}							}//* 
							
							if (_189gzykk)
							{
								//* 
								//*                 Accumulate transformations in the matrix Z 
								//* 
								
								{
									System.Int32 __81fgg2dlsvn2369 = (System.Int32)(_pinc1ofz);
									const System.Int32 __81fgg2step2369 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2369;
									for (__81fgg2count2369 = System.Math.Max(0, (System.Int32)(((System.Int32)(_mg9v9w4h) - __81fgg2dlsvn2369 + __81fgg2step2369) / __81fgg2step2369)), _znpjgsef = __81fgg2dlsvn2369; __81fgg2count2369 != 0; __81fgg2count2369--, _znpjgsef += (__81fgg2step2369)) {

									{
										
										_6j9l5fwy = ((*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_5l1tna8s)) + (_j3ir6imc * *(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_5l1tna8s)))) + (_s8neut4h * *(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)2 - 1) * 1 * (_5l1tna8s))));
										*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_5l1tna8s)) = (*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_5l1tna8s)) - (_6j9l5fwy * _dkyrz0g2));
										*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_5l1tna8s)) = (*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_5l1tna8s)) - (_6j9l5fwy * _jzq4t39k));
										*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)2 - 1) * 1 * (_5l1tna8s)) = (*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)2 - 1) * 1 * (_5l1tna8s)) - (_6j9l5fwy * _sihp0s0u));
Mark90:;
										// continue
									}
																		}								}
							}
							
						}
						else
						if (_oqpc3yjg == (int)2)
						{
							//* 
							//*              Apply G from the left to transform the rows of the matrix 
							//*              in columns K to I2. 
							//* 
							
							{
								System.Int32 __81fgg2dlsvn2370 = (System.Int32)(_umlkckdg);
								const System.Int32 __81fgg2step2370 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2370;
								for (__81fgg2count2370 = System.Math.Max(0, (System.Int32)(((System.Int32)(_8ur10vsh) - __81fgg2dlsvn2370 + __81fgg2step2370) / __81fgg2step2370)), _znpjgsef = __81fgg2dlsvn2370; __81fgg2count2370 != 0; __81fgg2count2370--, _znpjgsef += (__81fgg2step2370)) {

								{
									
									_6j9l5fwy = (*(_ogkjl6gu+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) + (_j3ir6imc * *(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw))));
									*(_ogkjl6gu+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) - (_6j9l5fwy * _dkyrz0g2));
									*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) - (_6j9l5fwy * _jzq4t39k));
Mark100:;
									// continue
								}
																}							}//* 
							//*              Apply G from the right to transform the columns of the 
							//*              matrix in rows I1 to min(K+3,I). 
							//* 
							
							{
								System.Int32 __81fgg2dlsvn2371 = (System.Int32)(_egqdmelt);
								const System.Int32 __81fgg2step2371 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2371;
								for (__81fgg2count2371 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s) - __81fgg2dlsvn2371 + __81fgg2step2371) / __81fgg2step2371)), _znpjgsef = __81fgg2dlsvn2371; __81fgg2count2371 != 0; __81fgg2count2371--, _znpjgsef += (__81fgg2step2371)) {

								{
									
									_6j9l5fwy = (*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) + (_j3ir6imc * *(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw))));
									*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) - (_6j9l5fwy * _dkyrz0g2));
									*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)) - (_6j9l5fwy * _jzq4t39k));
Mark110:;
									// continue
								}
																}							}//* 
							
							if (_189gzykk)
							{
								//* 
								//*                 Accumulate transformations in the matrix Z 
								//* 
								
								{
									System.Int32 __81fgg2dlsvn2372 = (System.Int32)(_pinc1ofz);
									const System.Int32 __81fgg2step2372 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2372;
									for (__81fgg2count2372 = System.Math.Max(0, (System.Int32)(((System.Int32)(_mg9v9w4h) - __81fgg2dlsvn2372 + __81fgg2step2372) / __81fgg2step2372)), _znpjgsef = __81fgg2dlsvn2372; __81fgg2count2372 != 0; __81fgg2count2372--, _znpjgsef += (__81fgg2step2372)) {

									{
										
										_6j9l5fwy = (*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_5l1tna8s)) + (_j3ir6imc * *(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_5l1tna8s))));
										*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_5l1tna8s)) = (*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_5l1tna8s)) - (_6j9l5fwy * _dkyrz0g2));
										*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_5l1tna8s)) = (*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_5l1tna8s)) - (_6j9l5fwy * _jzq4t39k));
Mark120:;
										// continue
									}
																		}								}
							}
							
						}
						
Mark130:;
						// continue
					}
										}				}//* 
				
Mark140:;
				// continue
			}
						}		}//* 
		//*     Failure to converge in remaining number of iterations 
		//* 
		
		_gro5yvfo = _b5p6od9s;
		return;//* 
		
Mark150:;
		// continue//* 
		
		if (_68ec3gbh == _b5p6od9s)
		{
			//* 
			//*        H(I,I-1) is negligible: one eigenvalue has converged. 
			//* 
			
			*(_b5j6m2b7+(_b5p6od9s - 1)) = *(_ogkjl6gu+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_1iekxpnw));
			*(_nc0qphpn+(_b5p6od9s - 1)) = _d0547bi2;
		}
		else
		if (_68ec3gbh == (_b5p6od9s - (int)1))
		{
			//* 
			//*        H(I-1,I-2) is negligible: a pair of eigenvalues have converged. 
			//* 
			//*        Transform the 2-by-2 submatrix to standard Schur form, 
			//*        and compute and store the eigenvalues. 
			//* 
			
			_ticz2j2b(ref Unsafe.AsRef(*(_ogkjl6gu+(_b5p6od9s - (int)1 - 1) + (_b5p6od9s - (int)1 - 1) * 1 * (_1iekxpnw))) ,ref Unsafe.AsRef(*(_ogkjl6gu+(_b5p6od9s - (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_1iekxpnw))) ,ref Unsafe.AsRef(*(_ogkjl6gu+(_b5p6od9s - 1) + (_b5p6od9s - (int)1 - 1) * 1 * (_1iekxpnw))) ,ref Unsafe.AsRef(*(_ogkjl6gu+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_1iekxpnw))) ,ref Unsafe.AsRef(*(_b5j6m2b7+(_b5p6od9s - (int)1 - 1))) ,ref Unsafe.AsRef(*(_nc0qphpn+(_b5p6od9s - (int)1 - 1))) ,ref Unsafe.AsRef(*(_b5j6m2b7+(_b5p6od9s - 1))) ,ref Unsafe.AsRef(*(_nc0qphpn+(_b5p6od9s - 1))) ,ref _82tpdhyl ,ref _8tmd0ner );//* 
			
			if (_amt8y1zm)
			{
				//* 
				//*           Apply the transformation to the rest of H. 
				//* 
				
				if (_8ur10vsh > _b5p6od9s)
				_5obdubpp(ref Unsafe.AsRef(_8ur10vsh - _b5p6od9s) ,(_ogkjl6gu+(_b5p6od9s - (int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,(_ogkjl6gu+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,ref _82tpdhyl ,ref _8tmd0ner );
				_5obdubpp(ref Unsafe.AsRef((_b5p6od9s - _egqdmelt) - (int)1) ,(_ogkjl6gu+(_egqdmelt - 1) + (_b5p6od9s - (int)1 - 1) * 1 * (_1iekxpnw)),ref Unsafe.AsRef((int)1) ,(_ogkjl6gu+(_egqdmelt - 1) + (_b5p6od9s - 1) * 1 * (_1iekxpnw)),ref Unsafe.AsRef((int)1) ,ref _82tpdhyl ,ref _8tmd0ner );
			}
			
			if (_189gzykk)
			{
				//* 
				//*           Apply the transformation to Z. 
				//* 
				
				_5obdubpp(ref _iq0q8194 ,(_7e60fcso+(_pinc1ofz - 1) + (_b5p6od9s - (int)1 - 1) * 1 * (_5l1tna8s)),ref Unsafe.AsRef((int)1) ,(_7e60fcso+(_pinc1ofz - 1) + (_b5p6od9s - 1) * 1 * (_5l1tna8s)),ref Unsafe.AsRef((int)1) ,ref _82tpdhyl ,ref _8tmd0ner );
			}
			
		}
		//* 
		//*     return to start of the main loop with new value of I. 
		//* 
		
		_b5p6od9s = (_68ec3gbh - (int)1);goto Mark20;//* 
		
Mark160:;
		// continue
		return;//* 
		//*     End of SLAHQR 
		//* 
		
	}
	
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
