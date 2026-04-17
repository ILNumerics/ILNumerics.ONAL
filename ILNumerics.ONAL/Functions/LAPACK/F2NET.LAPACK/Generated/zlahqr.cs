
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
//*> \brief \b ZLAHQR computes the eigenvalues and Schur factorization of an upper Hessenberg matrix, using the double-shift/single-shift QR algorithm. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZLAHQR + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zlahqr.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zlahqr.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zlahqr.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZLAHQR( WANTT, WANTZ, N, ILO, IHI, H, LDH, W, ILOZ, 
//*                          IHIZ, Z, LDZ, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            IHI, IHIZ, ILO, ILOZ, INFO, LDH, LDZ, N 
//*       LOGICAL            WANTT, WANTZ 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX*16         H( LDH, * ), W( * ), Z( LDZ, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*>    ZLAHQR is an auxiliary routine called by CHSEQR to update the 
//*>    eigenvalues and Schur decomposition already computed by CHSEQR, by 
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
//*>          It is assumed that H is already upper triangular in rows and 
//*>          columns IHI+1:N, and that H(ILO,ILO-1) = 0 (unless ILO = 1). 
//*>          ZLAHQR works primarily with the Hessenberg submatrix in rows 
//*>          and columns ILO to IHI, but applies transformations to all of 
//*>          H if WANTT is .TRUE.. 
//*>          1 <= ILO <= max(1,IHI); IHI <= N. 
//*> \endverbatim 
//*> 
//*> \param[in,out] H 
//*> \verbatim 
//*>          H is COMPLEX*16 array, dimension (LDH,N) 
//*>          On entry, the upper Hessenberg matrix H. 
//*>          On exit, if INFO is zero and if WANTT is .TRUE., then H 
//*>          is upper triangular in rows and columns ILO:IHI.  If INFO 
//*>          is zero and if WANTT is .FALSE., then the contents of H 
//*>          are unspecified on exit.  The output state of H in case 
//*>          INF is positive is below under the description of INFO. 
//*> \endverbatim 
//*> 
//*> \param[in] LDH 
//*> \verbatim 
//*>          LDH is INTEGER 
//*>          The leading dimension of the array H. LDH >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[out] W 
//*> \verbatim 
//*>          W is COMPLEX*16 array, dimension (N) 
//*>          The computed eigenvalues ILO to IHI are stored in the 
//*>          corresponding elements of W. If WANTT is .TRUE., the 
//*>          eigenvalues are stored in the same order as on the diagonal 
//*>          of the Schur form returned in H, with W(i) = H(i,i). 
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
//*>          Z is COMPLEX*16 array, dimension (LDZ,N) 
//*>          If WANTZ is .TRUE., on entry Z must contain the current 
//*>          matrix Z of transformations accumulated by CHSEQR, and on 
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
//*>           > 0:   if INFO = i, ZLAHQR failed to compute all the 
//*>                  eigenvalues ILO to IHI in a total of 30 iterations 
//*>                  per eigenvalue; elements i+1:ihi of W contain 
//*>                  those eigenvalues which have been successfully 
//*>                  computed. 
//*> 
//*>                  If INFO > 0 and WANTT is .FALSE., then on exit, 
//*>                  the remaining unconverged eigenvalues are the 
//*>                  eigenvalues of the upper Hessenberg matrix 
//*>                  rows and columns ILO through INFO of the final, 
//*>                  output value of H. 
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
//*> \ingroup complex16OTHERauxiliary 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*> \verbatim 
//*> 
//*>     02-96 Based on modifications by 
//*>     David Day, Sandia National Laboratory, USA 
//*> 
//*>     12-04 Further modifications by 
//*>     Ralph Byers, University of Kansas, USA 
//*>     This is a modified version of ZLAHQR from LAPACK version 3.0. 
//*>     It is (1) more robust against overflow and underflow and 
//*>     (2) adopts the more conservative Ahues & Tisseur stopping 
//*>     criterion (LAWN 122, 1997). 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _p11f7879(ref Boolean _amt8y1zm, ref Boolean _189gzykk, ref Int32 _dxpq0xkr, ref Int32 _pew3blan, ref Int32 _9c1csucx, complex* _ogkjl6gu, ref Int32 _1iekxpnw, complex* _z1ioc3c8, ref Int32 _pinc1ofz, ref Int32 _mg9v9w4h, complex* _7e60fcso, ref Int32 _5l1tna8s, ref Int32 _gro5yvfo)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)32 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
complex _d0547bi2 =   new fcomplex(0f,0f);
complex _kxg5drh2 =   new fcomplex(1f,0f);
Double _7cb2gzzt =  0d;
Double _zfxjga59 =  1d;
Double _gbf4169i =  0.5d;
Double _av6f7vuf =  3d / 4d;
complex _n7plx4io =  default;
complex _j4eznmsw =  default;
complex _qp548ua4 =  default;
complex _p1s2d3vl =  default;
complex _r6pjko54 =  default;
complex _6j9l5fwy =  default;
complex _2ivtt43r =  default;
complex _dkyrz0g2 =  default;
complex _1ajfmh55 =  default;
complex _7u55mqkq =  default;
complex _j3ir6imc =  default;
complex _ta7zuy9k =  default;
complex _f3z3edv0 =  default;
Double _zwm0s9sq =  default;
Double _9tcc7f14 =  default;
Double _7v8y2qii =  default;
Double _uv0s0qmf =  default;
Double _hr1k6vut =  default;
Double _sq0bv6ma =  default;
Double _b56sf68i =  default;
Double _irk8i6qr =  default;
Double _odf6ja0t =  default;
Double _h75qnr7l =  default;
Double _bogm0gwy =  default;
Double _s66poh0u =  default;
Double _jzq4t39k =  default;
Double _ts63qxkr =  default;
Double _0h4yb5wu =  default;
Int32 _b5p6od9s =  default;
Int32 _egqdmelt =  default;
Int32 _8ur10vsh =  default;
Int32 _qrxtt59n =  default;
Int32 _7u74ue5o =  default;
Int32 _znpjgsef =  default;
Int32 _ezvtm8xn =  default;
Int32 _c669miu7 =  default;
Int32 _umlkckdg =  default;
Int32 _68ec3gbh =  default;
Int32 _ev4xhht5 =  default;
Int32 _aym8a085 =  default;
Int32 _iq0q8194 =  default;
complex* _ycxba85s =  (complex*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(complex) * ((int)2);
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
		//*     .. Statement Functions .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Statement Function definitions .. 
		
		
		Func<complex,Double> _4jqx89by = (_a94616nn) => { return (ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.DBLE(_a94616nn ) ) + ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.DIMAG(_a94616nn ) )); };;//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		_gro5yvfo = (int)0;//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr == (int)0)
		return;
		if (_pew3blan == _9c1csucx)
		{
			
			*(_z1ioc3c8+(_pew3blan - 1)) = *(_ogkjl6gu+(_pew3blan - 1) + (_pew3blan - 1) * 1 * (_1iekxpnw));
			return;
		}
		//* 
		//*     ==== clear out the trash ==== 
		
		{
			System.Int32 __81fgg2dlsvn2676 = (System.Int32)(_pew3blan);
			const System.Int32 __81fgg2step2676 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2676;
			for (__81fgg2count2676 = System.Math.Max(0, (System.Int32)(((System.Int32)(_9c1csucx - (int)3) - __81fgg2dlsvn2676 + __81fgg2step2676) / __81fgg2step2676)), _znpjgsef = __81fgg2dlsvn2676; __81fgg2count2676 != 0; __81fgg2count2676--, _znpjgsef += (__81fgg2step2676)) {

			{
				
				*(_ogkjl6gu+(_znpjgsef + (int)2 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) = _d0547bi2;
				*(_ogkjl6gu+(_znpjgsef + (int)3 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) = _d0547bi2;
Mark10:;
				// continue
			}
						}		}
		if (_pew3blan <= (_9c1csucx - (int)2))
		*(_ogkjl6gu+(_9c1csucx - 1) + (_9c1csucx - (int)2 - 1) * 1 * (_1iekxpnw)) = _d0547bi2;//*     ==== ensure that subdiagonal entries are real ==== 
		
		if (_amt8y1zm)
		{
			
			_c669miu7 = (int)1;
			_ezvtm8xn = _dxpq0xkr;
		}
		else
		{
			
			_c669miu7 = _pew3blan;
			_ezvtm8xn = _9c1csucx;
		}
		
		{
			System.Int32 __81fgg2dlsvn2677 = (System.Int32)((_pew3blan + (int)1));
			const System.Int32 __81fgg2step2677 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2677;
			for (__81fgg2count2677 = System.Math.Max(0, (System.Int32)(((System.Int32)(_9c1csucx) - __81fgg2dlsvn2677 + __81fgg2step2677) / __81fgg2step2677)), _b5p6od9s = __81fgg2dlsvn2677; __81fgg2count2677 != 0; __81fgg2count2677--, _b5p6od9s += (__81fgg2step2677)) {

			{
				
				if (ILNumerics.F2NET.Intrinsics.DIMAG(*(_ogkjl6gu+(_b5p6od9s - 1) + (_b5p6od9s - (int)1 - 1) * 1 * (_1iekxpnw)) ) != _7cb2gzzt)
				{
					//*           ==== The following redundant normalization 
					//*           .    avoids problems with both gradual and 
					//*           .    sudden underflow in ABS(H(I,I-1)) ==== 
					
					_r6pjko54 = (*(_ogkjl6gu+(_b5p6od9s - 1) + (_b5p6od9s - (int)1 - 1) * 1 * (_1iekxpnw)) / _4jqx89by(*(_ogkjl6gu+(_b5p6od9s - 1) + (_b5p6od9s - (int)1 - 1) * 1 * (_1iekxpnw)) ));
					_r6pjko54 = (ILNumerics.F2NET.Intrinsics.DCONJG(_r6pjko54 ) / ILNumerics.F2NET.Intrinsics.ABS(_r6pjko54 ));
					*(_ogkjl6gu+(_b5p6od9s - 1) + (_b5p6od9s - (int)1 - 1) * 1 * (_1iekxpnw)) = DCMPLX(ILNumerics.F2NET.Intrinsics.ABS(*(_ogkjl6gu+(_b5p6od9s - 1) + (_b5p6od9s - (int)1 - 1) * 1 * (_1iekxpnw)) ));
					_wv0on4xy(ref Unsafe.AsRef((_ezvtm8xn - _b5p6od9s) + (int)1) ,ref _r6pjko54 ,(_ogkjl6gu+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw );
					_wv0on4xy(ref Unsafe.AsRef((ILNumerics.F2NET.Intrinsics.MIN(_ezvtm8xn ,_b5p6od9s + (int)1 ) - _c669miu7) + (int)1) ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.DCONJG(_r6pjko54 )) ,(_ogkjl6gu+(_c669miu7 - 1) + (_b5p6od9s - 1) * 1 * (_1iekxpnw)),ref Unsafe.AsRef((int)1) );
					if (_189gzykk)
					_wv0on4xy(ref Unsafe.AsRef((_mg9v9w4h - _pinc1ofz) + (int)1) ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.DCONJG(_r6pjko54 )) ,(_7e60fcso+(_pinc1ofz - 1) + (_b5p6od9s - 1) * 1 * (_5l1tna8s)),ref Unsafe.AsRef((int)1) );
				}
				
Mark20:;
				// continue
			}
						}		}//* 
		
		_aym8a085 = ((_9c1csucx - _pew3blan) + (int)1);
		_iq0q8194 = ((_mg9v9w4h - _pinc1ofz) + (int)1);//* 
		//*     Set machine-dependent constants for the stopping criterion. 
		//* 
		
		_h75qnr7l = _f43eg0w0("SAFE MINIMUM" );
		_odf6ja0t = (_zfxjga59 / _h75qnr7l);
		_to4dtyqc(ref _h75qnr7l ,ref _odf6ja0t );
		_0h4yb5wu = _f43eg0w0("PRECISION" );
		_bogm0gwy = (_h75qnr7l * (ILNumerics.F2NET.Intrinsics.DBLE(_aym8a085 ) / _0h4yb5wu));//* 
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
		//*     IHI to ILO in steps of 1. Each iteration of the loop works 
		//*     with the active submatrix in rows and columns L to I. 
		//*     Eigenvalues I+1 to IHI have already converged. Either L = ILO, or 
		//*     H(L,L-1) is negligible so that the matrix splits. 
		//* 
		
		_b5p6od9s = _9c1csucx;
Mark30:;
		// continue
		if (_b5p6od9s < _pew3blan)goto Mark150;//* 
		//*     Perform QR iterations on rows and columns ILO to I until a 
		//*     submatrix of order 1 splits off at the bottom because a 
		//*     subdiagonal element has become negligible. 
		//* 
		
		_68ec3gbh = _pew3blan;
		{
			System.Int32 __81fgg2dlsvn2678 = (System.Int32)((int)0);
			const System.Int32 __81fgg2step2678 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2678;
			for (__81fgg2count2678 = System.Math.Max(0, (System.Int32)(((System.Int32)(_7u74ue5o) - __81fgg2dlsvn2678 + __81fgg2step2678) / __81fgg2step2678)), _qrxtt59n = __81fgg2dlsvn2678; __81fgg2count2678 != 0; __81fgg2count2678--, _qrxtt59n += (__81fgg2step2678)) {

			{
				//* 
				//*        Look for a single small subdiagonal element. 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn2679 = (System.Int32)(_b5p6od9s);
					System.Int32 __81fgg2step2679 = (System.Int32)((int)-1);
					System.Int32 __81fgg2count2679;
					for (__81fgg2count2679 = System.Math.Max(0, (System.Int32)(((System.Int32)(_68ec3gbh + (int)1) - __81fgg2dlsvn2679 + __81fgg2step2679) / __81fgg2step2679)), _umlkckdg = __81fgg2dlsvn2679; __81fgg2count2679 != 0; __81fgg2count2679--, _umlkckdg += (__81fgg2step2679)) {

					{
						
						if (_4jqx89by(*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - (int)1 - 1) * 1 * (_1iekxpnw)) ) <= _bogm0gwy)goto Mark50;
						_ts63qxkr = (_4jqx89by(*(_ogkjl6gu+(_umlkckdg - (int)1 - 1) + (_umlkckdg - (int)1 - 1) * 1 * (_1iekxpnw)) ) + _4jqx89by(*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) ));
						if (_ts63qxkr == _d0547bi2)
						{
							
							if ((_umlkckdg - (int)2) >= _pew3blan)
							_ts63qxkr = (_ts63qxkr + ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.DBLE(*(_ogkjl6gu+(_umlkckdg - (int)1 - 1) + (_umlkckdg - (int)2 - 1) * 1 * (_1iekxpnw)) ) ));
							if ((_umlkckdg + (int)1) <= _9c1csucx)
							_ts63qxkr = (_ts63qxkr + ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.DBLE(*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) ) ));
						}
						//*           ==== The following is a conservative small subdiagonal 
						//*           .    deflation criterion due to Ahues & Tisseur (LAWN 122, 
						//*           .    1997). It has better mathematical foundation and 
						//*           .    improves accuracy in some examples.  ==== 
						
						if (ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.DBLE(*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - (int)1 - 1) * 1 * (_1iekxpnw)) ) ) <= (_0h4yb5wu * _ts63qxkr))
						{
							
							_9tcc7f14 = ILNumerics.F2NET.Intrinsics.MAX(_4jqx89by(*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - (int)1 - 1) * 1 * (_1iekxpnw)) ) ,_4jqx89by(*(_ogkjl6gu+(_umlkckdg - (int)1 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) ) );
							_7v8y2qii = ILNumerics.F2NET.Intrinsics.MIN(_4jqx89by(*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - (int)1 - 1) * 1 * (_1iekxpnw)) ) ,_4jqx89by(*(_ogkjl6gu+(_umlkckdg - (int)1 - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) ) );
							_zwm0s9sq = ILNumerics.F2NET.Intrinsics.MAX(_4jqx89by(*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) ) ,_4jqx89by(*(_ogkjl6gu+(_umlkckdg - (int)1 - 1) + (_umlkckdg - (int)1 - 1) * 1 * (_1iekxpnw)) - *(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) ) );
							_uv0s0qmf = ILNumerics.F2NET.Intrinsics.MIN(_4jqx89by(*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) ) ,_4jqx89by(*(_ogkjl6gu+(_umlkckdg - (int)1 - 1) + (_umlkckdg - (int)1 - 1) * 1 * (_1iekxpnw)) - *(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) ) );
							_irk8i6qr = (_zwm0s9sq + _9tcc7f14);
							if ((_7v8y2qii * (_9tcc7f14 / _irk8i6qr)) <= ILNumerics.F2NET.Intrinsics.MAX(_bogm0gwy ,_0h4yb5wu * (_uv0s0qmf * (_zwm0s9sq / _irk8i6qr)) ))goto Mark50;
						}
						
Mark40:;
						// continue
					}
										}				}
Mark50:;
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
				//*        Exit from loop if a submatrix of order 1 has split off. 
				//* 
				
				if (_68ec3gbh >= _b5p6od9s)goto Mark140;//* 
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
					
					_irk8i6qr = (_av6f7vuf * ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.DBLE(*(_ogkjl6gu+(_68ec3gbh + (int)1 - 1) + (_68ec3gbh - 1) * 1 * (_1iekxpnw)) ) ));
					_2ivtt43r = (_irk8i6qr + *(_ogkjl6gu+(_68ec3gbh - 1) + (_68ec3gbh - 1) * 1 * (_1iekxpnw)));
				}
				else
				if (_qrxtt59n == (int)20)
				{
					//* 
					//*           Exceptional shift. 
					//* 
					
					_irk8i6qr = (_av6f7vuf * ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.DBLE(*(_ogkjl6gu+(_b5p6od9s - 1) + (_b5p6od9s - (int)1 - 1) * 1 * (_1iekxpnw)) ) ));
					_2ivtt43r = (_irk8i6qr + *(_ogkjl6gu+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_1iekxpnw)));
				}
				else
				{
					//* 
					//*           Wilkinson's shift. 
					//* 
					
					_2ivtt43r = *(_ogkjl6gu+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_1iekxpnw));
					_7u55mqkq = (ILNumerics.F2NET.Intrinsics.SQRT(*(_ogkjl6gu+(_b5p6od9s - (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_1iekxpnw)) ) * ILNumerics.F2NET.Intrinsics.SQRT(*(_ogkjl6gu+(_b5p6od9s - 1) + (_b5p6od9s - (int)1 - 1) * 1 * (_1iekxpnw)) ));
					_irk8i6qr = _4jqx89by(_7u55mqkq );
					if (_irk8i6qr != _7cb2gzzt)
					{
						
						_ta7zuy9k = (_gbf4169i * (*(_ogkjl6gu+(_b5p6od9s - (int)1 - 1) + (_b5p6od9s - (int)1 - 1) * 1 * (_1iekxpnw)) - _2ivtt43r));
						_s66poh0u = _4jqx89by(_ta7zuy9k );
						_irk8i6qr = ILNumerics.F2NET.Intrinsics.MAX(_irk8i6qr ,_4jqx89by(_ta7zuy9k ) );
						_f3z3edv0 = (_irk8i6qr * ILNumerics.F2NET.Intrinsics.SQRT(__POW2((_ta7zuy9k / _irk8i6qr)) + __POW2((_7u55mqkq / _irk8i6qr)) ));
						if (_s66poh0u > _7cb2gzzt)
						{
							
							if (((ILNumerics.F2NET.Intrinsics.DBLE(_ta7zuy9k / _s66poh0u ) * ILNumerics.F2NET.Intrinsics.DBLE(_f3z3edv0 )) + (ILNumerics.F2NET.Intrinsics.DIMAG(_ta7zuy9k / _s66poh0u ) * ILNumerics.F2NET.Intrinsics.DIMAG(_f3z3edv0 ))) < _7cb2gzzt)
							_f3z3edv0 = (-(_f3z3edv0));
						}
						
						_2ivtt43r = (_2ivtt43r - (_7u55mqkq * _530gk4dk(ref _7u55mqkq ,ref Unsafe.AsRef((_ta7zuy9k + _f3z3edv0)) )));
					}
					
				}
				//* 
				//*        Look for two consecutive small subdiagonal elements. 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn2680 = (System.Int32)((_b5p6od9s - (int)1));
					System.Int32 __81fgg2step2680 = (System.Int32)((int)-1);
					System.Int32 __81fgg2count2680;
					for (__81fgg2count2680 = System.Math.Max(0, (System.Int32)(((System.Int32)(_68ec3gbh + (int)1) - __81fgg2dlsvn2680 + __81fgg2step2680) / __81fgg2step2680)), _ev4xhht5 = __81fgg2dlsvn2680; __81fgg2count2680 != 0; __81fgg2count2680--, _ev4xhht5 += (__81fgg2step2680)) {

					{
						//* 
						//*           Determine the effect of starting the single-shift QR 
						//*           iteration at row M, and see if this would make H(M,M-1) 
						//*           negligible. 
						//* 
						
						_j4eznmsw = *(_ogkjl6gu+(_ev4xhht5 - 1) + (_ev4xhht5 - 1) * 1 * (_1iekxpnw));
						_p1s2d3vl = *(_ogkjl6gu+(_ev4xhht5 + (int)1 - 1) + (_ev4xhht5 + (int)1 - 1) * 1 * (_1iekxpnw));
						_qp548ua4 = (_j4eznmsw - _2ivtt43r);
						_sq0bv6ma = ILNumerics.F2NET.Intrinsics.DBLE(*(_ogkjl6gu+(_ev4xhht5 + (int)1 - 1) + (_ev4xhht5 - 1) * 1 * (_1iekxpnw)) );
						_irk8i6qr = (_4jqx89by(_qp548ua4 ) + ILNumerics.F2NET.Intrinsics.ABS(_sq0bv6ma ));
						_qp548ua4 = (_qp548ua4 / _irk8i6qr);
						_sq0bv6ma = (_sq0bv6ma / _irk8i6qr);
						*(_ycxba85s+((int)1 - 1)) = _qp548ua4;
						*(_ycxba85s+((int)2 - 1)) = DCMPLX(_sq0bv6ma);
						_hr1k6vut = ILNumerics.F2NET.Intrinsics.DBLE(*(_ogkjl6gu+(_ev4xhht5 - 1) + (_ev4xhht5 - (int)1 - 1) * 1 * (_1iekxpnw)) );
						if ((ILNumerics.F2NET.Intrinsics.ABS(_hr1k6vut ) * ILNumerics.F2NET.Intrinsics.ABS(_sq0bv6ma )) <= (_0h4yb5wu * (_4jqx89by(_qp548ua4 ) * (_4jqx89by(_j4eznmsw ) + _4jqx89by(_p1s2d3vl )))))goto Mark70;
Mark60:;
						// continue
					}
										}				}
				_j4eznmsw = *(_ogkjl6gu+(_68ec3gbh - 1) + (_68ec3gbh - 1) * 1 * (_1iekxpnw));
				_p1s2d3vl = *(_ogkjl6gu+(_68ec3gbh + (int)1 - 1) + (_68ec3gbh + (int)1 - 1) * 1 * (_1iekxpnw));
				_qp548ua4 = (_j4eznmsw - _2ivtt43r);
				_sq0bv6ma = ILNumerics.F2NET.Intrinsics.DBLE(*(_ogkjl6gu+(_68ec3gbh + (int)1 - 1) + (_68ec3gbh - 1) * 1 * (_1iekxpnw)) );
				_irk8i6qr = (_4jqx89by(_qp548ua4 ) + ILNumerics.F2NET.Intrinsics.ABS(_sq0bv6ma ));
				_qp548ua4 = (_qp548ua4 / _irk8i6qr);
				_sq0bv6ma = (_sq0bv6ma / _irk8i6qr);
				*(_ycxba85s+((int)1 - 1)) = _qp548ua4;
				*(_ycxba85s+((int)2 - 1)) = DCMPLX(_sq0bv6ma);
Mark70:;
				// continue//* 
				//*        Single-shift QR step 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn2681 = (System.Int32)(_ev4xhht5);
					const System.Int32 __81fgg2step2681 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2681;
					for (__81fgg2count2681 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn2681 + __81fgg2step2681) / __81fgg2step2681)), _umlkckdg = __81fgg2dlsvn2681; __81fgg2count2681 != 0; __81fgg2count2681--, _umlkckdg += (__81fgg2step2681)) {

					{
						//* 
						//*           The first iteration of this loop determines a reflection G 
						//*           from the vector V and applies it from left and right to H, 
						//*           thus creating a nonzero bulge below the subdiagonal. 
						//* 
						//*           Each subsequent iteration determines a reflection G to 
						//*           restore the Hessenberg form in the (K-1)th column, and thus 
						//*           chases the bulge one step toward the bottom of the active 
						//*           submatrix. 
						//* 
						//*           V(2) is always real before the call to ZLARFG, and hence 
						//*           after the call T2 ( = T1*V(2) ) is also real. 
						//* 
						
						if (_umlkckdg > _ev4xhht5)
						_ly902k7t(ref Unsafe.AsRef((int)2) ,(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - (int)1 - 1) * 1 * (_1iekxpnw)),ref Unsafe.AsRef((int)1) ,_ycxba85s ,ref Unsafe.AsRef((int)1) );
						_4btmjfem(ref Unsafe.AsRef((int)2) ,ref Unsafe.AsRef(*(_ycxba85s+((int)1 - 1))) ,(_ycxba85s+((int)2 - 1)),ref Unsafe.AsRef((int)1) ,ref _dkyrz0g2 );
						if (_umlkckdg > _ev4xhht5)
						{
							
							*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - (int)1 - 1) * 1 * (_1iekxpnw)) = *(_ycxba85s+((int)1 - 1));
							*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_umlkckdg - (int)1 - 1) * 1 * (_1iekxpnw)) = _d0547bi2;
						}
						
						_j3ir6imc = *(_ycxba85s+((int)2 - 1));
						_jzq4t39k = ILNumerics.F2NET.Intrinsics.DBLE(_dkyrz0g2 * _j3ir6imc );//* 
						//*           Apply G from the left to transform the rows of the matrix 
						//*           in columns K to I2. 
						//* 
						
						{
							System.Int32 __81fgg2dlsvn2682 = (System.Int32)(_umlkckdg);
							const System.Int32 __81fgg2step2682 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2682;
							for (__81fgg2count2682 = System.Math.Max(0, (System.Int32)(((System.Int32)(_8ur10vsh) - __81fgg2dlsvn2682 + __81fgg2step2682) / __81fgg2step2682)), _znpjgsef = __81fgg2dlsvn2682; __81fgg2count2682 != 0; __81fgg2count2682--, _znpjgsef += (__81fgg2step2682)) {

							{
								
								_6j9l5fwy = ((ILNumerics.F2NET.Intrinsics.DCONJG(_dkyrz0g2 ) * *(_ogkjl6gu+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw))) + (_jzq4t39k * *(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw))));
								*(_ogkjl6gu+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) - _6j9l5fwy);
								*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_umlkckdg + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)) - (_6j9l5fwy * _j3ir6imc));
Mark80:;
								// continue
							}
														}						}//* 
						//*           Apply G from the right to transform the columns of the 
						//*           matrix in rows I1 to min(K+2,I). 
						//* 
						
						{
							System.Int32 __81fgg2dlsvn2683 = (System.Int32)(_egqdmelt);
							const System.Int32 __81fgg2step2683 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2683;
							for (__81fgg2count2683 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN(_umlkckdg + (int)2 ,_b5p6od9s )) - __81fgg2dlsvn2683 + __81fgg2step2683) / __81fgg2step2683)), _znpjgsef = __81fgg2dlsvn2683; __81fgg2count2683 != 0; __81fgg2count2683--, _znpjgsef += (__81fgg2step2683)) {

							{
								
								_6j9l5fwy = ((_dkyrz0g2 * *(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw))) + (_jzq4t39k * *(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw))));
								*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_1iekxpnw)) - _6j9l5fwy);
								*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_1iekxpnw)) - (_6j9l5fwy * ILNumerics.F2NET.Intrinsics.DCONJG(_j3ir6imc )));
Mark90:;
								// continue
							}
														}						}//* 
						
						if (_189gzykk)
						{
							//* 
							//*              Accumulate transformations in the matrix Z 
							//* 
							
							{
								System.Int32 __81fgg2dlsvn2684 = (System.Int32)(_pinc1ofz);
								const System.Int32 __81fgg2step2684 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2684;
								for (__81fgg2count2684 = System.Math.Max(0, (System.Int32)(((System.Int32)(_mg9v9w4h) - __81fgg2dlsvn2684 + __81fgg2step2684) / __81fgg2step2684)), _znpjgsef = __81fgg2dlsvn2684; __81fgg2count2684 != 0; __81fgg2count2684--, _znpjgsef += (__81fgg2step2684)) {

								{
									
									_6j9l5fwy = ((_dkyrz0g2 * *(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_5l1tna8s))) + (_jzq4t39k * *(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_5l1tna8s))));
									*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_5l1tna8s)) = (*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_5l1tna8s)) - _6j9l5fwy);
									*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_5l1tna8s)) = (*(_7e60fcso+(_znpjgsef - 1) + (_umlkckdg + (int)1 - 1) * 1 * (_5l1tna8s)) - (_6j9l5fwy * ILNumerics.F2NET.Intrinsics.DCONJG(_j3ir6imc )));
Mark100:;
									// continue
								}
																}							}
						}
						//* 
						
						if ((_umlkckdg == _ev4xhht5) & (_ev4xhht5 > _68ec3gbh))
						{
							//* 
							//*              If the QR step was started at row M > L because two 
							//*              consecutive small subdiagonals were found, then extra 
							//*              scaling must be performed to ensure that H(M,M-1) remains 
							//*              real. 
							//* 
							
							_1ajfmh55 = (_kxg5drh2 - _dkyrz0g2);
							_1ajfmh55 = (_1ajfmh55 / ILNumerics.F2NET.Intrinsics.ABS(_1ajfmh55 ));
							*(_ogkjl6gu+(_ev4xhht5 + (int)1 - 1) + (_ev4xhht5 - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_ev4xhht5 + (int)1 - 1) + (_ev4xhht5 - 1) * 1 * (_1iekxpnw)) * ILNumerics.F2NET.Intrinsics.DCONJG(_1ajfmh55 ));
							if ((_ev4xhht5 + (int)2) <= _b5p6od9s)
							*(_ogkjl6gu+(_ev4xhht5 + (int)2 - 1) + (_ev4xhht5 + (int)1 - 1) * 1 * (_1iekxpnw)) = (*(_ogkjl6gu+(_ev4xhht5 + (int)2 - 1) + (_ev4xhht5 + (int)1 - 1) * 1 * (_1iekxpnw)) * _1ajfmh55);
							{
								System.Int32 __81fgg2dlsvn2685 = (System.Int32)(_ev4xhht5);
								const System.Int32 __81fgg2step2685 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2685;
								for (__81fgg2count2685 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s) - __81fgg2dlsvn2685 + __81fgg2step2685) / __81fgg2step2685)), _znpjgsef = __81fgg2dlsvn2685; __81fgg2count2685 != 0; __81fgg2count2685--, _znpjgsef += (__81fgg2step2685)) {

								{
									
									if (_znpjgsef != (_ev4xhht5 + (int)1))
									{
										
										if (_8ur10vsh > _znpjgsef)
										_wv0on4xy(ref Unsafe.AsRef(_8ur10vsh - _znpjgsef) ,ref _1ajfmh55 ,(_ogkjl6gu+(_znpjgsef - 1) + (_znpjgsef + (int)1 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw );
										_wv0on4xy(ref Unsafe.AsRef(_znpjgsef - _egqdmelt) ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.DCONJG(_1ajfmh55 )) ,(_ogkjl6gu+(_egqdmelt - 1) + (_znpjgsef - 1) * 1 * (_1iekxpnw)),ref Unsafe.AsRef((int)1) );
										if (_189gzykk)
										{
											
											_wv0on4xy(ref _iq0q8194 ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.DCONJG(_1ajfmh55 )) ,(_7e60fcso+(_pinc1ofz - 1) + (_znpjgsef - 1) * 1 * (_5l1tna8s)),ref Unsafe.AsRef((int)1) );
										}
										
									}
									
Mark110:;
									// continue
								}
																}							}
						}
						
Mark120:;
						// continue
					}
										}				}//* 
				//*        Ensure that H(I,I-1) is real. 
				//* 
				
				_1ajfmh55 = *(_ogkjl6gu+(_b5p6od9s - 1) + (_b5p6od9s - (int)1 - 1) * 1 * (_1iekxpnw));
				if (ILNumerics.F2NET.Intrinsics.DIMAG(_1ajfmh55 ) != _7cb2gzzt)
				{
					//*           This has been modified by ILN, see: https://github.com/Reference-LAPACK/lapack/issues/575  
					
					_b56sf68i = ILNumerics.F2NET.Intrinsics.ABS(_1ajfmh55 );//*          RTEMP = DZNRM2(1, TEMP, 1) 
					//*        IF (RTEMP .EQ. RZERO) THEN  
					//*            RTEMP = CABS1(TEMP) 
					//*            H( I, I-1 ) = RTEMP 
					//*            TEMP = TEMP / RTEMP 
					//*            RTEMP = ABS( TEMP) 
					//*            H( I, I-1 ) = H( I, I-1 )*RTEMP 
					//*        ELSE  
					//*            H( I, I-1 ) = RTEMP 
					//*        END IF 
					
					*(_ogkjl6gu+(_b5p6od9s - 1) + (_b5p6od9s - (int)1 - 1) * 1 * (_1iekxpnw)) = DCMPLX(_b56sf68i);
					_1ajfmh55 = (_1ajfmh55 / _b56sf68i);
					if (_8ur10vsh > _b5p6od9s)
					_wv0on4xy(ref Unsafe.AsRef(_8ur10vsh - _b5p6od9s) ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.DCONJG(_1ajfmh55 )) ,(_ogkjl6gu+(_b5p6od9s - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw );
					_wv0on4xy(ref Unsafe.AsRef(_b5p6od9s - _egqdmelt) ,ref _1ajfmh55 ,(_ogkjl6gu+(_egqdmelt - 1) + (_b5p6od9s - 1) * 1 * (_1iekxpnw)),ref Unsafe.AsRef((int)1) );
					if (_189gzykk)
					{
						
						_wv0on4xy(ref _iq0q8194 ,ref _1ajfmh55 ,(_7e60fcso+(_pinc1ofz - 1) + (_b5p6od9s - 1) * 1 * (_5l1tna8s)),ref Unsafe.AsRef((int)1) );
					}
					
				}
				//* 
				
Mark130:;
				// continue
			}
						}		}//* 
		//*     Failure to converge in remaining number of iterations 
		//* 
		
		_gro5yvfo = _b5p6od9s;
		return;//* 
		
Mark140:;
		// continue//* 
		//*     H(I,I-1) is negligible: one eigenvalue has converged. 
		//* 
		
		*(_z1ioc3c8+(_b5p6od9s - 1)) = *(_ogkjl6gu+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_1iekxpnw));//* 
		//*     return to start of the main loop with new value of I. 
		//* 
		
		_b5p6od9s = (_68ec3gbh - (int)1);goto Mark30;//* 
		
Mark150:;
		// continue
		return;//* 
		//*     End of ZLAHQR 
		//* 
		
	}
	
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
