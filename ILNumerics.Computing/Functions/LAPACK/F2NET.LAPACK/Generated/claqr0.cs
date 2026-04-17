
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
//*> \brief \b CLAQR0 computes the eigenvalues of a Hessenberg matrix, and optionally the matrices from the Schur decomposition. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CLAQR0 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/claqr0.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/claqr0.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/claqr0.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CLAQR0( WANTT, WANTZ, N, ILO, IHI, H, LDH, W, ILOZ, 
//*                          IHIZ, Z, LDZ, WORK, LWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            IHI, IHIZ, ILO, ILOZ, INFO, LDH, LDZ, LWORK, N 
//*       LOGICAL            WANTT, WANTZ 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX            H( LDH, * ), W( * ), WORK( * ), Z( LDZ, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*>    CLAQR0 computes the eigenvalues of a Hessenberg matrix H 
//*>    and, optionally, the matrices T and Z from the Schur decomposition 
//*>    H = Z T Z**H, where T is an upper triangular matrix (the 
//*>    Schur form), and Z is the unitary matrix of Schur vectors. 
//*> 
//*>    Optionally Z may be postmultiplied into an input unitary 
//*>    matrix Q so that this routine can give the Schur factorization 
//*>    of a matrix A which has been reduced to the Hessenberg form H 
//*>    by the unitary matrix Q:  A = Q*H*Q**H = (QZ)*H*(QZ)**H. 
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
//*>           It is assumed that H is already upper triangular in rows 
//*>           and columns 1:ILO-1 and IHI+1:N and, if ILO > 1, 
//*>           H(ILO,ILO-1) is zero. ILO and IHI are normally set by a 
//*>           previous call to CGEBAL, and then passed to CGEHRD when the 
//*>           matrix output by CGEBAL is reduced to Hessenberg form. 
//*>           Otherwise, ILO and IHI should be set to 1 and N, 
//*>           respectively.  If N > 0, then 1 <= ILO <= IHI <= N. 
//*>           If N = 0, then ILO = 1 and IHI = 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] H 
//*> \verbatim 
//*>          H is COMPLEX array, dimension (LDH,N) 
//*>           On entry, the upper Hessenberg matrix H. 
//*>           On exit, if INFO = 0 and WANTT is .TRUE., then H 
//*>           contains the upper triangular matrix T from the Schur 
//*>           decomposition (the Schur form). If INFO = 0 and WANT is 
//*>           .FALSE., then the contents of H are unspecified on exit. 
//*>           (The output value of H when INFO > 0 is given under the 
//*>           description of INFO below.) 
//*> 
//*>           This subroutine may explicitly set H(i,j) = 0 for i > j and 
//*>           j = 1, 2, ... ILO-1 or j = IHI+1, IHI+2, ... N. 
//*> \endverbatim 
//*> 
//*> \param[in] LDH 
//*> \verbatim 
//*>          LDH is INTEGER 
//*>           The leading dimension of the array H. LDH >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[out] W 
//*> \verbatim 
//*>          W is COMPLEX array, dimension (N) 
//*>           The computed eigenvalues of H(ILO:IHI,ILO:IHI) are stored 
//*>           in W(ILO:IHI). If WANTT is .TRUE., then the eigenvalues are 
//*>           stored in the same order as on the diagonal of the Schur 
//*>           form returned in H, with W(i) = H(i,i). 
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
//*>           Specify the rows of Z to which transformations must be 
//*>           applied if WANTZ is .TRUE.. 
//*>           1 <= ILOZ <= ILO; IHI <= IHIZ <= N. 
//*> \endverbatim 
//*> 
//*> \param[in,out] Z 
//*> \verbatim 
//*>          Z is COMPLEX array, dimension (LDZ,IHI) 
//*>           If WANTZ is .FALSE., then Z is not referenced. 
//*>           If WANTZ is .TRUE., then Z(ILO:IHI,ILOZ:IHIZ) is 
//*>           replaced by Z(ILO:IHI,ILOZ:IHIZ)*U where U is the 
//*>           orthogonal Schur factor of H(ILO:IHI,ILO:IHI). 
//*>           (The output value of Z when INFO > 0 is given under 
//*>           the description of INFO below.) 
//*> \endverbatim 
//*> 
//*> \param[in] LDZ 
//*> \verbatim 
//*>          LDZ is INTEGER 
//*>           The leading dimension of the array Z.  if WANTZ is .TRUE. 
//*>           then LDZ >= MAX(1,IHIZ).  Otherwise, LDZ >= 1. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is COMPLEX array, dimension LWORK 
//*>           On exit, if LWORK = -1, WORK(1) returns an estimate of 
//*>           the optimal value for LWORK. 
//*> \endverbatim 
//*> 
//*> \param[in] LWORK 
//*> \verbatim 
//*>          LWORK is INTEGER 
//*>           The dimension of the array WORK.  LWORK >= max(1,N) 
//*>           is sufficient, but LWORK typically as large as 6*N may 
//*>           be required for optimal performance.  A workspace query 
//*>           to determine the optimal workspace size is recommended. 
//*> 
//*>           If LWORK = -1, then CLAQR0 does a workspace query. 
//*>           In this case, CLAQR0 checks the input parameters and 
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
//*>             > 0:  if INFO = i, CLAQR0 failed to compute all of 
//*>                the eigenvalues.  Elements 1:ilo-1 and i+1:n of WR 
//*>                and WI contain those eigenvalues which have been 
//*>                successfully computed.  (Failures are rare.) 
//*> 
//*>                If INFO > 0 and WANT is .FALSE., then on exit, 
//*>                the remaining unconverged eigenvalues are the eigen- 
//*>                values of the upper Hessenberg matrix rows and 
//*>                columns ILO through INFO of the final, output 
//*>                value of H. 
//*> 
//*>                If INFO > 0 and WANTT is .TRUE., then on exit 
//*> 
//*>           (*)  (initial value of H)*U  = U*(final value of H) 
//*> 
//*>                where U is a unitary matrix.  The final 
//*>                value of  H is upper Hessenberg and triangular in 
//*>                rows and columns INFO+1 through IHI. 
//*> 
//*>                If INFO > 0 and WANTZ is .TRUE., then on exit 
//*> 
//*>                  (final value of Z(ILO:IHI,ILOZ:IHIZ) 
//*>                   =  (initial value of Z(ILO:IHI,ILOZ:IHIZ)*U 
//*> 
//*>                where U is the unitary matrix in (*) (regard- 
//*>                less of the value of WANTT.) 
//*> 
//*>                If INFO > 0 and WANTZ is .FALSE., then Z is not 
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
//*> \ingroup complexOTHERauxiliary 
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
//*> \n 
//*>       K. Braman, R. Byers and R. Mathias, The Multi-Shift QR 
//*>       Algorithm Part II: Aggressive Early Deflation, SIAM Journal 
//*>       of Matrix Analysis, volume 23, pages 948--973, 2002. 
//*> 
//*  ===================================================================== 

	 
	public static void _eqgqwe2p(ref Boolean _amt8y1zm, ref Boolean _189gzykk, ref Int32 _dxpq0xkr, ref Int32 _pew3blan, ref Int32 _9c1csucx, fcomplex* _ogkjl6gu, ref Int32 _1iekxpnw, fcomplex* _z1ioc3c8, ref Int32 _pinc1ofz, ref Int32 _mg9v9w4h, fcomplex* _7e60fcso, ref Int32 _5l1tna8s, fcomplex* _apig8meb, ref Int32 _6fnxzlyp, ref Int32 _gro5yvfo)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)8 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Int32 _rnzh2ik3 =  (int)11;
Int32 _byjyug94 =  (int)5;
Int32 _wcldm9or =  (int)6;
Single _ugimvvcq =  0.75f;
fcomplex _d0547bi2 =   new fcomplex(0f,0f);
fcomplex _kxg5drh2 =   new fcomplex(1f,0f);
Single _5m0mjfxm =  2f;
fcomplex _zwm0s9sq =  default;
fcomplex _uv0s0qmf =  default;
fcomplex _985e9e9b =  default;
fcomplex _n7plx4io =  default;
fcomplex _f4rvsg6o =  default;
fcomplex _wo1bf490 =  default;
fcomplex _ogo0zwlp =  default;
fcomplex _ir7y3k4r =  default;
fcomplex _fjuaugss =  default;
Single _irk8i6qr =  default;
Int32 _b5p6od9s =  default;
Int32 _7zw3wa5b =  default;
Int32 _acwgen0p =  default;
Int32 _7u74ue5o =  default;
Int32 _umlkckdg =  default;
Int32 _v32676ye =  default;
Int32 _7d8gh478 =  default;
Int32 _5c51t51g =  default;
Int32 _y4o69i44 =  default;
Int32 _2hbd4be4 =  default;
Int32 _4a77vvpa =  default;
Int32 _9okdzrrx =  default;
Int32 _ar8tferf =  default;
Int32 _lpsj94qm =  default;
Int32 _87d05vk3 =  default;
Int32 _7a7t83vc =  default;
Int32 _3sgad2fi =  default;
Int32 _po5rklzy =  default;
Int32 _e4ueamrn =  default;
Int32 _z62os55a =  default;
Int32 _tu8yryfp =  default;
Int32 _aym8a085 =  default;
Int32 _hevfg8gr =  default;
Int32 _spqe84bd =  default;
Int32 _rs56fkjq =  default;
Int32 _edl2gwc7 =  default;
Int32 _4p3u99cx =  default;
Int32 _ipa926kc =  default;
Int32 _e69o47u0 =  default;
Int32 _w6pmxgch =  default;
Int32 _jpjfzur1 =  default;
Int32 _zltdbkbt =  default;
Int32 _rlpbyfwl =  default;
Boolean _3sak82w7 =  default;
FString _4cltz77x =  new FString(2);
fcomplex* _8jjrmha3 =  (fcomplex*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(fcomplex) * ((int)1)*((int)1);
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
		//*  ================================================================ 
		//*     .. Parameters .. 
		//* 
		//*     ==== Matrices of order NTINY or smaller must be processed by 
		//*     .    CLAHQR because of insufficient subdiagonal scratch space. 
		//*     .    (This is a hard limit.) ==== 
		//* 
		//*     ==== Exceptional deflation windows:  try to cure rare 
		//*     .    slow convergence by varying the size of the 
		//*     .    deflation window after KEXNW iterations. ==== 
		//* 
		//*     ==== Exceptional shifts: try to cure rare slow convergence 
		//*     .    with ad-hoc exceptional shifts every KEXSH iterations. 
		//*     .    ==== 
		//* 
		//*     ==== The constant WILK1 is used to form the exceptional 
		//*     .    shifts. ==== 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Local Arrays .. 
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
		
		_gro5yvfo = (int)0;//* 
		//*     ==== Quick return for N = 0: nothing to do. ==== 
		//* 
		
		if (_dxpq0xkr == (int)0)
		{
			
			*(_apig8meb+((int)1 - 1)) = _kxg5drh2;
			return;
		}
		//* 
		
		if (_dxpq0xkr <= _rnzh2ik3)
		{
			//* 
			//*        ==== Tiny matrices must use CLAHQR. ==== 
			//* 
			
			_e4ueamrn = (int)1;
			if (_6fnxzlyp != (int)-1)
			_b6ujfmgo(ref _amt8y1zm ,ref _189gzykk ,ref _dxpq0xkr ,ref _pew3blan ,ref _9c1csucx ,_ogkjl6gu ,ref _1iekxpnw ,_z1ioc3c8 ,ref _pinc1ofz ,ref _mg9v9w4h ,_7e60fcso ,ref _5l1tna8s ,ref _gro5yvfo );
		}
		else
		{
			//* 
			//*        ==== Use small bulge multi-shift QR with aggressive early 
			//*        .    deflation on larger-than-tiny matrices. ==== 
			//* 
			//*        ==== Hope for the best. ==== 
			//* 
			
			_gro5yvfo = (int)0;//* 
			//*        ==== Set up job flags for ILAENV. ==== 
			//* 
			
			if (_amt8y1zm)
			{
				
				
				_4cltz77x[(int)1,(int)1] = "S";
			}
			else
			{
				
				
				_4cltz77x[(int)1,(int)1] = "E";
			}
			
			if (_189gzykk)
			{
				
				
				_4cltz77x[(int)2,(int)2] = "V";
			}
			else
			{
				
				
				_4cltz77x[(int)2,(int)2] = "N";
			}
			//* 
			//*        ==== NWR = recommended deflation window size.  At this 
			//*        .    point,  N .GT. NTINY = 11, so there is enough 
			//*        .    subdiagonal workspace for NWR.GE.2 as required. 
			//*        .    (In fact, there is enough subdiagonal space for 
			//*        .    NWR.GE.3.) ==== 
			//* 
			
			_zltdbkbt = _4mvd6e4d(ref Unsafe.AsRef((int)13) ,"CLAQR0" ,_4cltz77x ,ref _dxpq0xkr ,ref _pew3blan ,ref _9c1csucx ,ref _6fnxzlyp );
			_zltdbkbt = ILNumerics.F2NET.Intrinsics.MAX((int)2 ,_zltdbkbt );
			_zltdbkbt = ILNumerics.F2NET.Intrinsics.MIN((_9c1csucx - _pew3blan) + (int)1 ,(_dxpq0xkr - (int)1) / (int)3 ,_zltdbkbt );//* 
			//*        ==== NSR = recommended number of simultaneous shifts. 
			//*        .    At this point N .GT. NTINY = 11, so there is at 
			//*        .    enough subdiagonal workspace for NSR to be even 
			//*        .    and greater than or equal to two as required. ==== 
			//* 
			
			_ipa926kc = _4mvd6e4d(ref Unsafe.AsRef((int)15) ,"CLAQR0" ,_4cltz77x ,ref _dxpq0xkr ,ref _pew3blan ,ref _9c1csucx ,ref _6fnxzlyp );
			_ipa926kc = ILNumerics.F2NET.Intrinsics.MIN(_ipa926kc ,(_dxpq0xkr + (int)6) / (int)9 ,_9c1csucx - _pew3blan );
			_ipa926kc = ILNumerics.F2NET.Intrinsics.MAX((int)2 ,_ipa926kc - ILNumerics.F2NET.Intrinsics.MOD(_ipa926kc ,(int)2 ) );//* 
			//*        ==== Estimate optimal workspace ==== 
			//* 
			//*        ==== Workspace query call to CLAQR3 ==== 
			//* 
			
			_4n8jnp8p(ref _amt8y1zm ,ref _189gzykk ,ref _dxpq0xkr ,ref _pew3blan ,ref _9c1csucx ,ref Unsafe.AsRef(_zltdbkbt + (int)1) ,_ogkjl6gu ,ref _1iekxpnw ,ref _pinc1ofz ,ref _mg9v9w4h ,_7e60fcso ,ref _5l1tna8s ,ref _po5rklzy ,ref _3sgad2fi ,_z1ioc3c8 ,_ogkjl6gu ,ref _1iekxpnw ,ref _dxpq0xkr ,_ogkjl6gu ,ref _1iekxpnw ,ref _dxpq0xkr ,_ogkjl6gu ,ref _1iekxpnw ,_apig8meb ,ref Unsafe.AsRef((int)-1) );//* 
			//*        ==== Optimal workspace = MAX(CLAQR5, CLAQR3) ==== 
			//* 
			
			_e4ueamrn = ILNumerics.F2NET.Intrinsics.MAX(((int)3 * _ipa926kc) / (int)2 ,ILNumerics.F2NET.Intrinsics.INT(*(_apig8meb+((int)1 - 1)) ) );//* 
			//*        ==== Quick return in case of workspace query. ==== 
			//* 
			
			if (_6fnxzlyp == (int)-1)
			{
				
				*(_apig8meb+((int)1 - 1)) = ILNumerics.F2NET.Intrinsics.CMPLX(_e4ueamrn ,(int)0 );
				return;
			}
			//* 
			//*        ==== CLAHQR/CLAQR0 crossover point ==== 
			//* 
			
			_rs56fkjq = _4mvd6e4d(ref Unsafe.AsRef((int)12) ,"CLAQR0" ,_4cltz77x ,ref _dxpq0xkr ,ref _pew3blan ,ref _9c1csucx ,ref _6fnxzlyp );
			_rs56fkjq = ILNumerics.F2NET.Intrinsics.MAX(_rnzh2ik3 ,_rs56fkjq );//* 
			//*        ==== Nibble crossover point ==== 
			//* 
			
			_spqe84bd = _4mvd6e4d(ref Unsafe.AsRef((int)14) ,"CLAQR0" ,_4cltz77x ,ref _dxpq0xkr ,ref _pew3blan ,ref _9c1csucx ,ref _6fnxzlyp );
			_spqe84bd = ILNumerics.F2NET.Intrinsics.MAX((int)0 ,_spqe84bd );//* 
			//*        ==== Accumulate reflections during ttswp?  Use block 
			//*        .    2-by-2 structure during matrix-matrix multiply? ==== 
			//* 
			
			_v32676ye = _4mvd6e4d(ref Unsafe.AsRef((int)16) ,"CLAQR0" ,_4cltz77x ,ref _dxpq0xkr ,ref _pew3blan ,ref _9c1csucx ,ref _6fnxzlyp );
			_v32676ye = ILNumerics.F2NET.Intrinsics.MAX((int)0 ,_v32676ye );
			_v32676ye = ILNumerics.F2NET.Intrinsics.MIN((int)2 ,_v32676ye );//* 
			//*        ==== NWMAX = the largest possible deflation window for 
			//*        .    which there is sufficient workspace. ==== 
			//* 
			
			_jpjfzur1 = ILNumerics.F2NET.Intrinsics.MIN((_dxpq0xkr - (int)1) / (int)3 ,_6fnxzlyp / (int)2 );
			_w6pmxgch = _jpjfzur1;//* 
			//*        ==== NSMAX = the Largest number of simultaneous shifts 
			//*        .    for which there is sufficient workspace. ==== 
			//* 
			
			_4p3u99cx = ILNumerics.F2NET.Intrinsics.MIN((_dxpq0xkr + (int)6) / (int)9 ,((int)2 * _6fnxzlyp) / (int)3 );
			_4p3u99cx = (_4p3u99cx - ILNumerics.F2NET.Intrinsics.MOD(_4p3u99cx ,(int)2 ));//* 
			//*        ==== NDFL: an iteration count restarted at deflation. ==== 
			//* 
			
			_tu8yryfp = (int)1;//* 
			//*        ==== ITMAX = iteration limit ==== 
			//* 
			
			_7u74ue5o = (ILNumerics.F2NET.Intrinsics.MAX((int)30 ,(int)2 * _wcldm9or ) * ILNumerics.F2NET.Intrinsics.MAX((int)10 ,((_9c1csucx - _pew3blan) + (int)1) ));//* 
			//*        ==== Last row and column in the active block ==== 
			//* 
			
			_7d8gh478 = _9c1csucx;//* 
			//*        ==== Main Loop ==== 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn2525 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2525 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2525;
				for (__81fgg2count2525 = System.Math.Max(0, (System.Int32)(((System.Int32)(_7u74ue5o) - __81fgg2dlsvn2525 + __81fgg2step2525) / __81fgg2step2525)), _acwgen0p = __81fgg2dlsvn2525; __81fgg2count2525 != 0; __81fgg2count2525--, _acwgen0p += (__81fgg2step2525)) {

				{
					//* 
					//*           ==== Done when KBOT falls below ILO ==== 
					//* 
					
					if (_7d8gh478 < _pew3blan)goto Mark80;//* 
					//*           ==== Locate active block ==== 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn2526 = (System.Int32)(_7d8gh478);
						System.Int32 __81fgg2step2526 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count2526;
						for (__81fgg2count2526 = System.Math.Max(0, (System.Int32)(((System.Int32)(_pew3blan + (int)1) - __81fgg2dlsvn2526 + __81fgg2step2526) / __81fgg2step2526)), _umlkckdg = __81fgg2dlsvn2526; __81fgg2count2526 != 0; __81fgg2count2526--, _umlkckdg += (__81fgg2step2526)) {

						{
							
							if (*(_ogkjl6gu+(_umlkckdg - 1) + (_umlkckdg - (int)1 - 1) * 1 * (_1iekxpnw)) == _d0547bi2)goto Mark20;
Mark10:;
							// continue
						}
												}					}
					_umlkckdg = _pew3blan;
Mark20:;
					// continue
					_4a77vvpa = _umlkckdg;//* 
					//*           ==== Select deflation window size: 
					//*           .    Typical Case: 
					//*           .      If possible and advisable, nibble the entire 
					//*           .      active block.  If not, use size MIN(NWR,NWMAX) 
					//*           .      or MIN(NWR+1,NWMAX) depending upon which has 
					//*           .      the smaller corresponding subdiagonal entry 
					//*           .      (a heuristic). 
					//*           . 
					//*           .    Exceptional Case: 
					//*           .      If there have been no deflations in KEXNW or 
					//*           .      more iterations, then vary the deflation window 
					//*           .      size.   At first, because, larger windows are, 
					//*           .      in general, more powerful than smaller ones, 
					//*           .      rapidly increase the window to the maximum possible. 
					//*           .      Then, gradually reduce the window size. ==== 
					//* 
					
					_aym8a085 = ((_7d8gh478 - _4a77vvpa) + (int)1);
					_rlpbyfwl = ILNumerics.F2NET.Intrinsics.MIN(_aym8a085 ,_jpjfzur1 );
					if (_tu8yryfp < _byjyug94)
					{
						
						_w6pmxgch = ILNumerics.F2NET.Intrinsics.MIN(_rlpbyfwl ,_zltdbkbt );
					}
					else
					{
						
						_w6pmxgch = ILNumerics.F2NET.Intrinsics.MIN(_rlpbyfwl ,(int)2 * _w6pmxgch );
					}
					
					if (_w6pmxgch < _jpjfzur1)
					{
						
						if (_w6pmxgch >= (_aym8a085 - (int)1))
						{
							
							_w6pmxgch = _aym8a085;
						}
						else
						{
							
							_87d05vk3 = ((_7d8gh478 - _w6pmxgch) + (int)1);
							if (_4jqx89by(*(_ogkjl6gu+(_87d05vk3 - 1) + (_87d05vk3 - (int)1 - 1) * 1 * (_1iekxpnw)) ) > _4jqx89by(*(_ogkjl6gu+(_87d05vk3 - (int)1 - 1) + (_87d05vk3 - (int)2 - 1) * 1 * (_1iekxpnw)) ))
							_w6pmxgch = (_w6pmxgch + (int)1);
						}
						
					}
					
					if (_tu8yryfp < _byjyug94)
					{
						
						_z62os55a = (int)-1;
					}
					else
					if ((_z62os55a >= (int)0) | (_w6pmxgch >= _rlpbyfwl))
					{
						
						_z62os55a = (_z62os55a + (int)1);
						if ((_w6pmxgch - _z62os55a) < (int)2)
						_z62os55a = (int)0;
						_w6pmxgch = (_w6pmxgch - _z62os55a);
					}
					//* 
					//*           ==== Aggressive early deflation: 
					//*           .    split workspace under the subdiagonal into 
					//*           .      - an nw-by-nw work array V in the lower 
					//*           .        left-hand-corner, 
					//*           .      - an NW-by-at-least-NW-but-more-is-better 
					//*           .        (NW-by-NHO) horizontal work array along 
					//*           .        the bottom edge, 
					//*           .      - an at-least-NW-but-more-is-better (NHV-by-NW) 
					//*           .        vertical work array along the left-hand-edge. 
					//*           .        ==== 
					//* 
					
					_ar8tferf = ((_dxpq0xkr - _w6pmxgch) + (int)1);
					_2hbd4be4 = (_w6pmxgch + (int)1);
					_hevfg8gr = ((((_dxpq0xkr - _w6pmxgch) - (int)1) - _2hbd4be4) + (int)1);
					_7a7t83vc = (_w6pmxgch + (int)2);
					_e69o47u0 = (((_dxpq0xkr - _w6pmxgch) - _7a7t83vc) + (int)1);//* 
					//*           ==== Aggressive early deflation ==== 
					//* 
					
					_4n8jnp8p(ref _amt8y1zm ,ref _189gzykk ,ref _dxpq0xkr ,ref _4a77vvpa ,ref _7d8gh478 ,ref _w6pmxgch ,_ogkjl6gu ,ref _1iekxpnw ,ref _pinc1ofz ,ref _mg9v9w4h ,_7e60fcso ,ref _5l1tna8s ,ref _po5rklzy ,ref _3sgad2fi ,_z1ioc3c8 ,(_ogkjl6gu+(_ar8tferf - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,ref _hevfg8gr ,(_ogkjl6gu+(_ar8tferf - 1) + (_2hbd4be4 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,ref _e69o47u0 ,(_ogkjl6gu+(_7a7t83vc - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,_apig8meb ,ref _6fnxzlyp );//* 
					//*           ==== Adjust KBOT accounting for new deflations. ==== 
					//* 
					
					_7d8gh478 = (_7d8gh478 - _3sgad2fi);//* 
					//*           ==== KS points to the shifts. ==== 
					//* 
					
					_y4o69i44 = ((_7d8gh478 - _po5rklzy) + (int)1);//* 
					//*           ==== Skip an expensive QR sweep if there is a (partly 
					//*           .    heuristic) reason to expect that many eigenvalues 
					//*           .    will deflate without it.  Here, the QR sweep is 
					//*           .    skipped if many eigenvalues have just been deflated 
					//*           .    or if the remaining active block is small. 
					//* 
					
					if ((_3sgad2fi == (int)0) | ((((int)100 * _3sgad2fi) <= (_w6pmxgch * _spqe84bd)) & (((_7d8gh478 - _4a77vvpa) + (int)1) > ILNumerics.F2NET.Intrinsics.MIN(_rs56fkjq ,_jpjfzur1 ))))
					{
						//* 
						//*              ==== NS = nominal number of simultaneous shifts. 
						//*              .    This may be lowered (slightly) if CLAQR3 
						//*              .    did not provide that many shifts. ==== 
						//* 
						
						_edl2gwc7 = ILNumerics.F2NET.Intrinsics.MIN(_4p3u99cx ,_ipa926kc ,ILNumerics.F2NET.Intrinsics.MAX((int)2 ,_7d8gh478 - _4a77vvpa ) );
						_edl2gwc7 = (_edl2gwc7 - ILNumerics.F2NET.Intrinsics.MOD(_edl2gwc7 ,(int)2 ));//* 
						//*              ==== If there have been no deflations 
						//*              .    in a multiple of KEXSH iterations, 
						//*              .    then try exceptional shifts. 
						//*              .    Otherwise use shifts provided by 
						//*              .    CLAQR3 above or from the eigenvalues 
						//*              .    of a trailing principal submatrix. ==== 
						//* 
						
						if (ILNumerics.F2NET.Intrinsics.MOD(_tu8yryfp ,_wcldm9or ) == (int)0)
						{
							
							_y4o69i44 = ((_7d8gh478 - _edl2gwc7) + (int)1);
							{
								System.Int32 __81fgg2dlsvn2527 = (System.Int32)(_7d8gh478);
								System.Int32 __81fgg2step2527 = (System.Int32)((int)-2);
								System.Int32 __81fgg2count2527;
								for (__81fgg2count2527 = System.Math.Max(0, (System.Int32)(((System.Int32)(_y4o69i44 + (int)1) - __81fgg2dlsvn2527 + __81fgg2step2527) / __81fgg2step2527)), _b5p6od9s = __81fgg2dlsvn2527; __81fgg2count2527 != 0; __81fgg2count2527--, _b5p6od9s += (__81fgg2step2527)) {

								{
									
									*(_z1ioc3c8+(_b5p6od9s - 1)) = (*(_ogkjl6gu+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_1iekxpnw)) + (_ugimvvcq * _4jqx89by(*(_ogkjl6gu+(_b5p6od9s - 1) + (_b5p6od9s - (int)1 - 1) * 1 * (_1iekxpnw)) )));
									*(_z1ioc3c8+(_b5p6od9s - (int)1 - 1)) = *(_z1ioc3c8+(_b5p6od9s - 1));
Mark30:;
									// continue
								}
																}							}
						}
						else
						{
							//* 
							//*                 ==== Got NS/2 or fewer shifts? Use CLAQR4 or 
							//*                 .    CLAHQR on a trailing principal submatrix to 
							//*                 .    get more. (Since NS.LE.NSMAX.LE.(N+6)/9, 
							//*                 .    there is enough space below the subdiagonal 
							//*                 .    to fit an NS-by-NS scratch array.) ==== 
							//* 
							
							if (((_7d8gh478 - _y4o69i44) + (int)1) <= (_edl2gwc7 / (int)2))
							{
								
								_y4o69i44 = ((_7d8gh478 - _edl2gwc7) + (int)1);
								_2hbd4be4 = ((_dxpq0xkr - _edl2gwc7) + (int)1);
								_szaic8qw("A" ,ref _edl2gwc7 ,ref _edl2gwc7 ,(_ogkjl6gu+(_y4o69i44 - 1) + (_y4o69i44 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,(_ogkjl6gu+(_2hbd4be4 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw );
								if (_edl2gwc7 > _rs56fkjq)
								{
									
									_zusop5je(ref Unsafe.AsRef(false) ,ref Unsafe.AsRef(false) ,ref _edl2gwc7 ,ref Unsafe.AsRef((int)1) ,ref _edl2gwc7 ,(_ogkjl6gu+(_2hbd4be4 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,(_z1ioc3c8+(_y4o69i44 - 1)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef((int)1) ,_8jjrmha3 ,ref Unsafe.AsRef((int)1) ,_apig8meb ,ref _6fnxzlyp ,ref _7zw3wa5b );
								}
								else
								{
									
									_b6ujfmgo(ref Unsafe.AsRef(false) ,ref Unsafe.AsRef(false) ,ref _edl2gwc7 ,ref Unsafe.AsRef((int)1) ,ref _edl2gwc7 ,(_ogkjl6gu+(_2hbd4be4 - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,(_z1ioc3c8+(_y4o69i44 - 1)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef((int)1) ,_8jjrmha3 ,ref Unsafe.AsRef((int)1) ,ref _7zw3wa5b );
								}
								
								_y4o69i44 = (_y4o69i44 + _7zw3wa5b);//* 
								//*                    ==== In case of a rare QR failure use 
								//*                    .    eigenvalues of the trailing 2-by-2 
								//*                    .    principal submatrix.  Scale to avoid 
								//*                    .    overflows, underflows and subnormals. 
								//*                    .    (The scale factor S can not be zero, 
								//*                    .    because H(KBOT,KBOT-1) is nonzero.) ==== 
								//* 
								
								if (_y4o69i44 >= _7d8gh478)
								{
									
									_irk8i6qr = (((_4jqx89by(*(_ogkjl6gu+(_7d8gh478 - (int)1 - 1) + (_7d8gh478 - (int)1 - 1) * 1 * (_1iekxpnw)) ) + _4jqx89by(*(_ogkjl6gu+(_7d8gh478 - 1) + (_7d8gh478 - (int)1 - 1) * 1 * (_1iekxpnw)) )) + _4jqx89by(*(_ogkjl6gu+(_7d8gh478 - (int)1 - 1) + (_7d8gh478 - 1) * 1 * (_1iekxpnw)) )) + _4jqx89by(*(_ogkjl6gu+(_7d8gh478 - 1) + (_7d8gh478 - 1) * 1 * (_1iekxpnw)) ));
									_zwm0s9sq = (*(_ogkjl6gu+(_7d8gh478 - (int)1 - 1) + (_7d8gh478 - (int)1 - 1) * 1 * (_1iekxpnw)) / _irk8i6qr);
									_985e9e9b = (*(_ogkjl6gu+(_7d8gh478 - 1) + (_7d8gh478 - (int)1 - 1) * 1 * (_1iekxpnw)) / _irk8i6qr);
									_uv0s0qmf = (*(_ogkjl6gu+(_7d8gh478 - (int)1 - 1) + (_7d8gh478 - 1) * 1 * (_1iekxpnw)) / _irk8i6qr);
									_f4rvsg6o = (*(_ogkjl6gu+(_7d8gh478 - 1) + (_7d8gh478 - 1) * 1 * (_1iekxpnw)) / _irk8i6qr);
									_fjuaugss = ((_zwm0s9sq + _f4rvsg6o) / _5m0mjfxm);
									_wo1bf490 = (((_zwm0s9sq - _fjuaugss) * (_f4rvsg6o - _fjuaugss)) - (_uv0s0qmf * _985e9e9b));
									_ogo0zwlp = ILNumerics.F2NET.Intrinsics.SQRT(-(_wo1bf490) );
									*(_z1ioc3c8+(_7d8gh478 - (int)1 - 1)) = ((_fjuaugss + _ogo0zwlp) * _irk8i6qr);
									*(_z1ioc3c8+(_7d8gh478 - 1)) = ((_fjuaugss - _ogo0zwlp) * _irk8i6qr);//* 
									
									_y4o69i44 = (_7d8gh478 - (int)1);
								}
								
							}
							//* 
							
							if (((_7d8gh478 - _y4o69i44) + (int)1) > _edl2gwc7)
							{
								//* 
								//*                    ==== Sort the shifts (Helps a little) ==== 
								//* 
								
								_3sak82w7 = false;
								{
									System.Int32 __81fgg2dlsvn2528 = (System.Int32)(_7d8gh478);
									System.Int32 __81fgg2step2528 = (System.Int32)((int)-1);
									System.Int32 __81fgg2count2528;
									for (__81fgg2count2528 = System.Math.Max(0, (System.Int32)(((System.Int32)(_y4o69i44 + (int)1) - __81fgg2dlsvn2528 + __81fgg2step2528) / __81fgg2step2528)), _umlkckdg = __81fgg2dlsvn2528; __81fgg2count2528 != 0; __81fgg2count2528--, _umlkckdg += (__81fgg2step2528)) {

									{
										
										if (_3sak82w7)goto Mark60;
										_3sak82w7 = true;
										{
											System.Int32 __81fgg2dlsvn2529 = (System.Int32)(_y4o69i44);
											const System.Int32 __81fgg2step2529 = (System.Int32)((int)1);
											System.Int32 __81fgg2count2529;
											for (__81fgg2count2529 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg - (int)1) - __81fgg2dlsvn2529 + __81fgg2step2529) / __81fgg2step2529)), _b5p6od9s = __81fgg2dlsvn2529; __81fgg2count2529 != 0; __81fgg2count2529--, _b5p6od9s += (__81fgg2step2529)) {

											{
												
												if (_4jqx89by(*(_z1ioc3c8+(_b5p6od9s - 1)) ) < _4jqx89by(*(_z1ioc3c8+(_b5p6od9s + (int)1 - 1)) ))
												{
													
													_3sak82w7 = false;
													_ir7y3k4r = *(_z1ioc3c8+(_b5p6od9s - 1));
													*(_z1ioc3c8+(_b5p6od9s - 1)) = *(_z1ioc3c8+(_b5p6od9s + (int)1 - 1));
													*(_z1ioc3c8+(_b5p6od9s + (int)1 - 1)) = _ir7y3k4r;
												}
												
Mark40:;
												// continue
											}
																						}										}
Mark50:;
										// continue
									}
																		}								}
Mark60:;
								// continue
							}
							
						}
						//* 
						//*              ==== If there are only two shifts, then use 
						//*              .    only one.  ==== 
						//* 
						
						if (((_7d8gh478 - _y4o69i44) + (int)1) == (int)2)
						{
							
							if (_4jqx89by(*(_z1ioc3c8+(_7d8gh478 - 1)) - *(_ogkjl6gu+(_7d8gh478 - 1) + (_7d8gh478 - 1) * 1 * (_1iekxpnw)) ) < _4jqx89by(*(_z1ioc3c8+(_7d8gh478 - (int)1 - 1)) - *(_ogkjl6gu+(_7d8gh478 - 1) + (_7d8gh478 - 1) * 1 * (_1iekxpnw)) ))
							{
								
								*(_z1ioc3c8+(_7d8gh478 - (int)1 - 1)) = *(_z1ioc3c8+(_7d8gh478 - 1));
							}
							else
							{
								
								*(_z1ioc3c8+(_7d8gh478 - 1)) = *(_z1ioc3c8+(_7d8gh478 - (int)1 - 1));
							}
							
						}
						//* 
						//*              ==== Use up to NS of the the smallest magnitude 
						//*              .    shifts.  If there aren't NS shifts available, 
						//*              .    then use them all, possibly dropping one to 
						//*              .    make the number of shifts even. ==== 
						//* 
						
						_edl2gwc7 = ILNumerics.F2NET.Intrinsics.MIN(_edl2gwc7 ,(_7d8gh478 - _y4o69i44) + (int)1 );
						_edl2gwc7 = (_edl2gwc7 - ILNumerics.F2NET.Intrinsics.MOD(_edl2gwc7 ,(int)2 ));
						_y4o69i44 = ((_7d8gh478 - _edl2gwc7) + (int)1);//* 
						//*              ==== Small-bulge multi-shift QR sweep: 
						//*              .    split workspace under the subdiagonal into 
						//*              .    - a KDU-by-KDU work array U in the lower 
						//*              .      left-hand-corner, 
						//*              .    - a KDU-by-at-least-KDU-but-more-is-better 
						//*              .      (KDU-by-NHo) horizontal work array WH along 
						//*              .      the bottom edge, 
						//*              .    - and an at-least-KDU-but-more-is-better-by-KDU 
						//*              .      (NVE-by-KDU) vertical work WV arrow along 
						//*              .      the left-hand-edge. ==== 
						//* 
						
						_5c51t51g = (((int)3 * _edl2gwc7) - (int)3);
						_9okdzrrx = ((_dxpq0xkr - _5c51t51g) + (int)1);
						_lpsj94qm = (_5c51t51g + (int)1);
						_hevfg8gr = (((((_dxpq0xkr - _5c51t51g) + (int)1) - (int)4) - (_5c51t51g + (int)1)) + (int)1);
						_7a7t83vc = (_5c51t51g + (int)4);
						_e69o47u0 = (((_dxpq0xkr - _5c51t51g) - _7a7t83vc) + (int)1);//* 
						//*              ==== Small-bulge multi-shift QR sweep ==== 
						//* 
						
						_cjpbcdmo(ref _amt8y1zm ,ref _189gzykk ,ref _v32676ye ,ref _dxpq0xkr ,ref _4a77vvpa ,ref _7d8gh478 ,ref _edl2gwc7 ,(_z1ioc3c8+(_y4o69i44 - 1)),_ogkjl6gu ,ref _1iekxpnw ,ref _pinc1ofz ,ref _mg9v9w4h ,_7e60fcso ,ref _5l1tna8s ,_apig8meb ,ref Unsafe.AsRef((int)3) ,(_ogkjl6gu+(_9okdzrrx - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,ref _e69o47u0 ,(_ogkjl6gu+(_7a7t83vc - 1) + ((int)1 - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw ,ref _hevfg8gr ,(_ogkjl6gu+(_9okdzrrx - 1) + (_lpsj94qm - 1) * 1 * (_1iekxpnw)),ref _1iekxpnw );
					}
					//* 
					//*           ==== Note progress (or the lack of it). ==== 
					//* 
					
					if (_3sgad2fi > (int)0)
					{
						
						_tu8yryfp = (int)1;
					}
					else
					{
						
						_tu8yryfp = (_tu8yryfp + (int)1);
					}
					//* 
					//*           ==== End of main loop ==== 
					
Mark70:;
					// continue
				}
								}			}//* 
			//*        ==== Iteration limit exceeded.  Set INFO to show where 
			//*        .    the problem occurred and exit. ==== 
			//* 
			
			_gro5yvfo = _7d8gh478;
Mark80:;
			// continue
		}
		//* 
		//*     ==== Return the optimal value of LWORK. ==== 
		//* 
		
		*(_apig8meb+((int)1 - 1)) = ILNumerics.F2NET.Intrinsics.CMPLX(_e4ueamrn ,(int)0 );//* 
		//*     ==== End of CLAQR0 ==== 
		//* 
		
	}
	
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
