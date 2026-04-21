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
//*> \brief \b DHSEQR 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DHSEQR + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dhseqr.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dhseqr.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dhseqr.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DHSEQR( JOB, COMPZ, N, ILO, IHI, H, LDH, WR, WI, Z, 
//*                          LDZ, WORK, LWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            IHI, ILO, INFO, LDH, LDZ, LWORK, N 
//*       CHARACTER          COMPZ, JOB 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   H( LDH, * ), WI( * ), WORK( * ), WR( * ), 
//*      $                   Z( LDZ, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*>    DHSEQR computes the eigenvalues of a Hessenberg matrix H 
//*>    and, optionally, the matrices T and Z from the Schur decomposition 
//*>    H = Z T Z**T, where T is an upper quasi-triangular matrix (the 
//*>    Schur form), and Z is the orthogonal matrix of Schur vectors. 
//*> 
//*>    Optionally Z may be postmultiplied into an input orthogonal 
//*>    matrix Q so that this routine can give the Schur factorization 
//*>    of a matrix A which has been reduced to the Hessenberg form H 
//*>    by the orthogonal matrix Q:  A = Q*H*Q**T = (QZ)*T*(QZ)**T. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] JOB 
//*> \verbatim 
//*>          JOB is CHARACTER*1 
//*>           = 'E':  compute eigenvalues only; 
//*>           = 'S':  compute eigenvalues and the Schur form T. 
//*> \endverbatim 
//*> 
//*> \param[in] COMPZ 
//*> \verbatim 
//*>          COMPZ is CHARACTER*1 
//*>           = 'N':  no Schur vectors are computed; 
//*>           = 'I':  Z is initialized to the unit matrix and the matrix Z 
//*>                   of Schur vectors of H is returned; 
//*>           = 'V':  Z must contain an orthogonal matrix Q on entry, and 
//*>                   the product Q*Z is returned. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>           The order of the matrix H.  N >= 0. 
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
//*> 
//*>           It is assumed that H is already upper triangular in rows 
//*>           and columns 1:ILO-1 and IHI+1:N. ILO and IHI are normally 
//*>           set by a previous call to DGEBAL, and then passed to ZGEHRD 
//*>           when the matrix output by DGEBAL is reduced to Hessenberg 
//*>           form. Otherwise ILO and IHI should be set to 1 and N 
//*>           respectively.  If N > 0, then 1 <= ILO <= IHI <= N. 
//*>           If N = 0, then ILO = 1 and IHI = 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] H 
//*> \verbatim 
//*>          H is DOUBLE PRECISION array, dimension (LDH,N) 
//*>           On entry, the upper Hessenberg matrix H. 
//*>           On exit, if INFO = 0 and JOB = 'S', then H contains the 
//*>           upper quasi-triangular matrix T from the Schur decomposition 
//*>           (the Schur form); 2-by-2 diagonal blocks (corresponding to 
//*>           complex conjugate pairs of eigenvalues) are returned in 
//*>           standard form, with H(i,i) = H(i+1,i+1) and 
//*>           H(i+1,i)*H(i,i+1) < 0. If INFO = 0 and JOB = 'E', the 
//*>           contents of H are unspecified on exit.  (The output value of 
//*>           H when INFO > 0 is given under the description of INFO 
//*>           below.) 
//*> 
//*>           Unlike earlier versions of DHSEQR, this subroutine may 
//*>           explicitly H(i,j) = 0 for i > j and j = 1, 2, ... ILO-1 
//*>           or j = IHI+1, IHI+2, ... N. 
//*> \endverbatim 
//*> 
//*> \param[in] LDH 
//*> \verbatim 
//*>          LDH is INTEGER 
//*>           The leading dimension of the array H. LDH >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[out] WR 
//*> \verbatim 
//*>          WR is DOUBLE PRECISION array, dimension (N) 
//*> \endverbatim 
//*> 
//*> \param[out] WI 
//*> \verbatim 
//*>          WI is DOUBLE PRECISION array, dimension (N) 
//*> 
//*>           The real and imaginary parts, respectively, of the computed 
//*>           eigenvalues. If two eigenvalues are computed as a complex 
//*>           conjugate pair, they are stored in consecutive elements of 
//*>           WR and WI, say the i-th and (i+1)th, with WI(i) > 0 and 
//*>           WI(i+1) < 0. If JOB = 'S', the eigenvalues are stored in 
//*>           the same order as on the diagonal of the Schur form returned 
//*>           in H, with WR(i) = H(i,i) and, if H(i:i+1,i:i+1) is a 2-by-2 
//*>           diagonal block, WI(i) = sqrt(-H(i+1,i)*H(i,i+1)) and 
//*>           WI(i+1) = -WI(i). 
//*> \endverbatim 
//*> 
//*> \param[in,out] Z 
//*> \verbatim 
//*>          Z is DOUBLE PRECISION array, dimension (LDZ,N) 
//*>           If COMPZ = 'N', Z is not referenced. 
//*>           If COMPZ = 'I', on entry Z need not be set and on exit, 
//*>           if INFO = 0, Z contains the orthogonal matrix Z of the Schur 
//*>           vectors of H.  If COMPZ = 'V', on entry Z must contain an 
//*>           N-by-N matrix Q, which is assumed to be equal to the unit 
//*>           matrix except for the submatrix Z(ILO:IHI,ILO:IHI). On exit, 
//*>           if INFO = 0, Z contains Q*Z. 
//*>           Normally Q is the orthogonal matrix generated by DORGHR 
//*>           after the call to DGEHRD which formed the Hessenberg matrix 
//*>           H. (The output value of Z when INFO > 0 is given under 
//*>           the description of INFO below.) 
//*> \endverbatim 
//*> 
//*> \param[in] LDZ 
//*> \verbatim 
//*>          LDZ is INTEGER 
//*>           The leading dimension of the array Z.  if COMPZ = 'I' or 
//*>           COMPZ = 'V', then LDZ >= MAX(1,N).  Otherwise, LDZ >= 1. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is DOUBLE PRECISION array, dimension (LWORK) 
//*>           On exit, if INFO = 0, WORK(1) returns an estimate of 
//*>           the optimal value for LWORK. 
//*> \endverbatim 
//*> 
//*> \param[in] LWORK 
//*> \verbatim 
//*>          LWORK is INTEGER 
//*>           The dimension of the array WORK.  LWORK >= max(1,N) 
//*>           is sufficient and delivers very good and sometimes 
//*>           optimal performance.  However, LWORK as large as 11*N 
//*>           may be required for optimal performance.  A workspace 
//*>           query is recommended to determine the optimal workspace 
//*>           size. 
//*> 
//*>           If LWORK = -1, then DHSEQR does a workspace query. 
//*>           In this case, DHSEQR checks the input parameters and 
//*>           estimates the optimal workspace size for the given 
//*>           values of N, ILO and IHI.  The estimate is returned 
//*>           in WORK(1).  No error message related to LWORK is 
//*>           issued by XERBLA.  Neither H nor Z are accessed. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>             = 0:  successful exit 
//*>             < 0:  if INFO = -i, the i-th argument had an illegal 
//*>                    value 
//*>             > 0:  if INFO = i, DHSEQR failed to compute all of 
//*>                the eigenvalues.  Elements 1:ilo-1 and i+1:n of WR 
//*>                and WI contain those eigenvalues which have been 
//*>                successfully computed.  (Failures are rare.) 
//*> 
//*>                If INFO > 0 and JOB = 'E', then on exit, the 
//*>                remaining unconverged eigenvalues are the eigen- 
//*>                values of the upper Hessenberg matrix rows and 
//*>                columns ILO through INFO of the final, output 
//*>                value of H. 
//*> 
//*>                If INFO > 0 and JOB   = 'S', then on exit 
//*> 
//*>           (*)  (initial value of H)*U  = U*(final value of H) 
//*> 
//*>                where U is an orthogonal matrix.  The final 
//*>                value of H is upper Hessenberg and quasi-triangular 
//*>                in rows and columns INFO+1 through IHI. 
//*> 
//*>                If INFO > 0 and COMPZ = 'V', then on exit 
//*> 
//*>                  (final value of Z)  =  (initial value of Z)*U 
//*> 
//*>                where U is the orthogonal matrix in (*) (regard- 
//*>                less of the value of JOB.) 
//*> 
//*>                If INFO > 0 and COMPZ = 'I', then on exit 
//*>                      (final value of Z)  = U 
//*>                where U is the orthogonal matrix in (*) (regard- 
//*>                less of the value of JOB.) 
//*> 
//*>                If INFO > 0 and COMPZ = 'N', then Z is not 
//*>                accessed. 
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
//*> \ingroup doubleOTHERcomputational 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>       Karen Braman and Ralph Byers, Department of Mathematics, 
//*>       University of Kansas, USA 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>             Default values supplied by 
//*>             ILAENV(ISPEC,'DHSEQR',JOB(:1)//COMPZ(:1),N,ILO,IHI,LWORK). 
//*>             It is suggested that these defaults be adjusted in order 
//*>             to attain best performance in each particular 
//*>             computational environment. 
//*> 
//*>            ISPEC=12: The DLAHQR vs DLAQR0 crossover point. 
//*>                      Default: 75. (Must be at least 11.) 
//*> 
//*>            ISPEC=13: Recommended deflation window size. 
//*>                      This depends on ILO, IHI and NS.  NS is the 
//*>                      number of simultaneous shifts returned 
//*>                      by ILAENV(ISPEC=15).  (See ISPEC=15 below.) 
//*>                      The default for (IHI-ILO+1) <= 500 is NS. 
//*>                      The default for (IHI-ILO+1) >  500 is 3*NS/2. 
//*> 
//*>            ISPEC=14: Nibble crossover point. (See IPARMQ for 
//*>                      details.)  Default: 14% of deflation window 
//*>                      size. 
//*> 
//*>            ISPEC=15: Number of simultaneous shifts in a multishift 
//*>                      QR iteration. 
//*> 
//*>                      If IHI-ILO+1 is ... 
//*> 
//*>                      greater than      ...but less    ... the 
//*>                      or equal to ...      than        default is 
//*> 
//*>                           1               30          NS =   2(+) 
//*>                          30               60          NS =   4(+) 
//*>                          60              150          NS =  10(+) 
//*>                         150              590          NS =  ** 
//*>                         590             3000          NS =  64 
//*>                        3000             6000          NS = 128 
//*>                        6000             infinity      NS = 256 
//*> 
//*>                  (+)  By default some or all matrices of this order 
//*>                       are passed to the implicit double shift routine 
//*>                       DLAHQR and this parameter is ignored.  See 
//*>                       ISPEC=12 above and comments in IPARMQ for 
//*>                       details. 
//*> 
//*>                 (**)  The asterisks (**) indicate an ad-hoc 
//*>                       function of N increasing from 10 to 64. 
//*> 
//*>            ISPEC=16: Select structured matrix multiply. 
//*>                      If the number of simultaneous shifts (specified 
//*>                      by ISPEC=15) is less than 14, then the default 
//*>                      for ISPEC=16 is 0.  Otherwise the default for 
//*>                      ISPEC=16 is 2. 
//*> \endverbatim 
//* 
//*> \par References: 
//*  ================ 
//*> 
//*>       K. Braman, R. Byers and R. Mathias, The Multi-Shift QR 
//*>       Algorithm Part I: Maintaining Well Focused Shifts, and Level 3 
//*>       Performance, SIAM Journal of Matrix Analysis, volume 23, pages 
//*>       929--947, 2002. 
//*> \n 
//*>       K. Braman, R. Byers and R. Mathias, The Multi-Shift QR 
//*>       Algorithm Part II: Aggressive Early Deflation, SIAM Journal 
//*>       of Matrix Analysis, volume 23, pages 948--973, 2002. 
//* 
//*  ===================================================================== 

	 
	public static void _7qmn977e(FString _xcrv93xi, FString _w65aho3z, ref Int32 _dxpq0xkr, ref Int32 _pew3blan, ref Int32 _9c1csucx, Double* _ogkjl6gu, ref Int32 _1iekxpnw, Double* _b5j6m2b7, Double* _nc0qphpn, Double* _7e60fcso, ref Int32 _5l1tna8s, Double* _apig8meb, ref Int32 _6fnxzlyp, ref Int32 _gro5yvfo)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)19600 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Int32 _rnzh2ik3 =  (int)11;
Int32 _zx57w4aj =  (int)49;
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Double* _4tm8bfij =  (Double*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Double) * ((int)49)*((int)49);
Double* _q9phm6d1 =  (Double*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Double) * ((int)49);
Int32 _b5p6od9s =  default;
Int32 _7d8gh478 =  default;
Int32 _rs56fkjq =  default;
Boolean _ga9njzev =  default;
Boolean _lhlgm7z5 =  default;
Boolean _amt8y1zm =  default;
Boolean _189gzykk =  default;
string fLanavab = default;
#endregion  variable declarations
_xcrv93xi = _xcrv93xi.Convert(1);
_w65aho3z = _w65aho3z.Convert(1);

	{
		//* 
		//*  -- LAPACK computational routine (version 3.7.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     December 2016 
		//* 
		//*     .. Scalar Arguments .. 
		//*     .. 
		//*     .. Array Arguments .. 
		//*     .. 
		//* 
		//*  ===================================================================== 
		//* 
		//*     .. Parameters .. 
		//* 
		//*     ==== Matrices of order NTINY or smaller must be processed by 
		//*     .    DLAHQR because of insufficient subdiagonal scratch space. 
		//*     .    (This is a hard limit.) ==== 
		//* 
		//*     ==== NL allocates some local workspace to help small matrices 
		//*     .    through a rare DLAHQR failure.  NL > NTINY = 11 is 
		//*     .    required and NL <= NMIN = ILAENV(ISPEC=12,...) is recom- 
		//*     .    mended.  (The default value of NMIN is 75.)  Using NL = 49 
		//*     .    allows up to six simultaneous shifts and a 16-by-16 
		//*     .    deflation window.  ==== 
		//*     .. 
		//*     .. Local Arrays .. 
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
		//*     ==== Decode and check the input parameters. ==== 
		//* 
		
		_amt8y1zm = _w8y2rzgy(_xcrv93xi ,"S" );
		_ga9njzev = _w8y2rzgy(_w65aho3z ,"I" );
		_189gzykk = (_ga9njzev | _w8y2rzgy(_w65aho3z ,"V" ));
		*(_apig8meb+((int)1 - 1)) = ILNumerics.F2NET.Intrinsics.DBLE(ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ) );
		_lhlgm7z5 = (_6fnxzlyp == (int)-1);//* 
		
		_gro5yvfo = (int)0;
		if ((!(_w8y2rzgy(_xcrv93xi ,"E" ))) & (!(_amt8y1zm)))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if ((!(_w8y2rzgy(_w65aho3z ,"N" ))) & (!(_189gzykk)))
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if ((_pew3blan < (int)1) | (_pew3blan > ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr )))
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if ((_9c1csucx < ILNumerics.F2NET.Intrinsics.MIN(_pew3blan ,_dxpq0xkr )) | (_9c1csucx > _dxpq0xkr))
		{
			
			_gro5yvfo = (int)-5;
		}
		else
		if (_1iekxpnw < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)-7;
		}
		else
		if ((_5l1tna8s < (int)1) | (_189gzykk & (_5l1tna8s < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))))
		{
			
			_gro5yvfo = (int)-11;
		}
		else
		if ((_6fnxzlyp < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr )) & (!(_lhlgm7z5)))
		{
			
			_gro5yvfo = (int)-13;
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			//* 
			//*        ==== Quick return in case of invalid argument. ==== 
			//* 
			
			_ut9qalzx("DHSEQR" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;//* 
			
		}
		else
		if (_dxpq0xkr == (int)0)
		{
			//* 
			//*        ==== Quick return in case N = 0; nothing to do. ==== 
			//* 
			
			return;//* 
			
		}
		else
		if (_lhlgm7z5)
		{
			//* 
			//*        ==== Quick return in case of a workspace query ==== 
			//* 
			
			_t5i939kr(ref _amt8y1zm ,ref _189gzykk ,ref _dxpq0xkr ,ref _pew3blan ,ref _9c1csucx ,_ogkjl6gu ,ref _1iekxpnw ,_b5j6m2b7 ,_nc0qphpn ,ref _pew3blan ,ref _9c1csucx ,_7e60fcso ,ref _5l1tna8s ,_apig8meb ,ref _6fnxzlyp ,ref _gro5yvfo );//*        ==== Ensure reported workspace size is backward-compatible with 
			//*        .    previous LAPACK versions. ==== 
			
			*(_apig8meb+((int)1 - 1)) = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.DBLE(ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ) ) ,*(_apig8meb+((int)1 - 1)) );
			return;//* 
			
		}
		else
		{
			//* 
			//*        ==== copy eigenvalues isolated by DGEBAL ==== 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn2195 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2195 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2195;
				for (__81fgg2count2195 = System.Math.Max(0, (System.Int32)(((System.Int32)(_pew3blan - (int)1) - __81fgg2dlsvn2195 + __81fgg2step2195) / __81fgg2step2195)), _b5p6od9s = __81fgg2dlsvn2195; __81fgg2count2195 != 0; __81fgg2count2195--, _b5p6od9s += (__81fgg2step2195)) {

				{
					
					*(_b5j6m2b7+(_b5p6od9s - 1)) = *(_ogkjl6gu+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_1iekxpnw));
					*(_nc0qphpn+(_b5p6od9s - 1)) = _d0547bi2;
Mark10:;
					// continue
				}
								}			}
			{
				System.Int32 __81fgg2dlsvn2196 = (System.Int32)((_9c1csucx + (int)1));
				const System.Int32 __81fgg2step2196 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2196;
				for (__81fgg2count2196 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2196 + __81fgg2step2196) / __81fgg2step2196)), _b5p6od9s = __81fgg2dlsvn2196; __81fgg2count2196 != 0; __81fgg2count2196--, _b5p6od9s += (__81fgg2step2196)) {

				{
					
					*(_b5j6m2b7+(_b5p6od9s - 1)) = *(_ogkjl6gu+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_1iekxpnw));
					*(_nc0qphpn+(_b5p6od9s - 1)) = _d0547bi2;
Mark20:;
					// continue
				}
								}			}//* 
			//*        ==== Initialize Z, if requested ==== 
			//* 
			
			if (_ga9njzev)
			_rta9tuwm("A" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,_7e60fcso ,ref _5l1tna8s );//* 
			//*        ==== Quick return if possible ==== 
			//* 
			
			if (_pew3blan == _9c1csucx)
			{
				
				*(_b5j6m2b7+(_pew3blan - 1)) = *(_ogkjl6gu+(_pew3blan - 1) + (_pew3blan - 1) * 1 * (_1iekxpnw));
				*(_nc0qphpn+(_pew3blan - 1)) = _d0547bi2;
				return;
			}
			//* 
			//*        ==== DLAHQR/DLAQR0 crossover point ==== 
			//* 
			
			_rs56fkjq = _4mvd6e4d(ref Unsafe.AsRef((int)12) ,"DHSEQR" ,_xcrv93xi[(int)1,(int)1] + _w65aho3z[(int)1,(int)1] ,ref _dxpq0xkr ,ref _pew3blan ,ref _9c1csucx ,ref _6fnxzlyp );
			_rs56fkjq = ILNumerics.F2NET.Intrinsics.MAX(_rnzh2ik3 ,_rs56fkjq );//* 
			//*        ==== DLAQR0 for big matrices; DLAHQR for small ones ==== 
			//* 
			
			if (_dxpq0xkr > _rs56fkjq)
			{
				
				_t5i939kr(ref _amt8y1zm ,ref _189gzykk ,ref _dxpq0xkr ,ref _pew3blan ,ref _9c1csucx ,_ogkjl6gu ,ref _1iekxpnw ,_b5j6m2b7 ,_nc0qphpn ,ref _pew3blan ,ref _9c1csucx ,_7e60fcso ,ref _5l1tna8s ,_apig8meb ,ref _6fnxzlyp ,ref _gro5yvfo );
			}
			else
			{
				//* 
				//*           ==== Small matrix ==== 
				//* 
				
				_xwuh9vnu(ref _amt8y1zm ,ref _189gzykk ,ref _dxpq0xkr ,ref _pew3blan ,ref _9c1csucx ,_ogkjl6gu ,ref _1iekxpnw ,_b5j6m2b7 ,_nc0qphpn ,ref _pew3blan ,ref _9c1csucx ,_7e60fcso ,ref _5l1tna8s ,ref _gro5yvfo );//* 
				
				if (_gro5yvfo > (int)0)
				{
					//* 
					//*              ==== A rare DLAHQR failure!  DLAQR0 sometimes succeeds 
					//*              .    when DLAHQR fails. ==== 
					//* 
					
					_7d8gh478 = _gro5yvfo;//* 
					
					if (_dxpq0xkr >= _zx57w4aj)
					{
						//* 
						//*                 ==== Larger matrices have enough subdiagonal scratch 
						//*                 .    space to call DLAQR0 directly. ==== 
						//* 
						
						_t5i939kr(ref _amt8y1zm ,ref _189gzykk ,ref _dxpq0xkr ,ref _pew3blan ,ref _7d8gh478 ,_ogkjl6gu ,ref _1iekxpnw ,_b5j6m2b7 ,_nc0qphpn ,ref _pew3blan ,ref _9c1csucx ,_7e60fcso ,ref _5l1tna8s ,_apig8meb ,ref _6fnxzlyp ,ref _gro5yvfo );//* 
						
					}
					else
					{
						//* 
						//*                 ==== Tiny matrices don't have enough subdiagonal 
						//*                 .    scratch space to benefit from DLAQR0.  Hence, 
						//*                 .    tiny matrices must be copied into a larger 
						//*                 .    array before calling DLAQR0. ==== 
						//* 
						
						_hhtvj1kb("A" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_ogkjl6gu ,ref _1iekxpnw ,_4tm8bfij ,ref Unsafe.AsRef(_zx57w4aj) );
						*(_4tm8bfij+(_dxpq0xkr + (int)1 - 1) + (_dxpq0xkr - 1) * 1 * ((int)49)) = _d0547bi2;
						_rta9tuwm("A" ,ref Unsafe.AsRef(_zx57w4aj) ,ref Unsafe.AsRef(_zx57w4aj - _dxpq0xkr) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_4tm8bfij+((int)1 - 1) + (_dxpq0xkr + (int)1 - 1) * 1 * ((int)49)),ref Unsafe.AsRef(_zx57w4aj) );
						_t5i939kr(ref _amt8y1zm ,ref _189gzykk ,ref Unsafe.AsRef(_zx57w4aj) ,ref _pew3blan ,ref _7d8gh478 ,_4tm8bfij ,ref Unsafe.AsRef(_zx57w4aj) ,_b5j6m2b7 ,_nc0qphpn ,ref _pew3blan ,ref _9c1csucx ,_7e60fcso ,ref _5l1tna8s ,_q9phm6d1 ,ref Unsafe.AsRef(_zx57w4aj) ,ref _gro5yvfo );
						if (_amt8y1zm | (_gro5yvfo != (int)0))
						_hhtvj1kb("A" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_4tm8bfij ,ref Unsafe.AsRef(_zx57w4aj) ,_ogkjl6gu ,ref _1iekxpnw );
					}
					
				}
				
			}
			//* 
			//*        ==== Clear out the trash, if necessary. ==== 
			//* 
			
			if ((_amt8y1zm | (_gro5yvfo != (int)0)) & (_dxpq0xkr > (int)2))
			_rta9tuwm("L" ,ref Unsafe.AsRef(_dxpq0xkr - (int)2) ,ref Unsafe.AsRef(_dxpq0xkr - (int)2) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_ogkjl6gu+((int)3 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw );//* 
			//*        ==== Ensure reported workspace size is backward-compatible with 
			//*        .    previous LAPACK versions. ==== 
			//* 
			
			*(_apig8meb+((int)1 - 1)) = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.DBLE(ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ) ) ,*(_apig8meb+((int)1 - 1)) );
		}
		//* 
		//*     ==== End of DHSEQR ==== 
		//* 
		
	}
	
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
