
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
//*> \brief \b ZTRSNA 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZTRSNA + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/ztrsna.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/ztrsna.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/ztrsna.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZTRSNA( JOB, HOWMNY, SELECT, N, T, LDT, VL, LDVL, VR, 
//*                          LDVR, S, SEP, MM, M, WORK, LDWORK, RWORK, 
//*                          INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          HOWMNY, JOB 
//*       INTEGER            INFO, LDT, LDVL, LDVR, LDWORK, M, MM, N 
//*       .. 
//*       .. Array Arguments .. 
//*       LOGICAL            SELECT( * ) 
//*       DOUBLE PRECISION   RWORK( * ), S( * ), SEP( * ) 
//*       COMPLEX*16         T( LDT, * ), VL( LDVL, * ), VR( LDVR, * ), 
//*      $                   WORK( LDWORK, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZTRSNA estimates reciprocal condition numbers for specified 
//*> eigenvalues and/or right eigenvectors of a complex upper triangular 
//*> matrix T (or of any matrix Q*T*Q**H with Q unitary). 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] JOB 
//*> \verbatim 
//*>          JOB is CHARACTER*1 
//*>          Specifies whether condition numbers are required for 
//*>          eigenvalues (S) or eigenvectors (SEP): 
//*>          = 'E': for eigenvalues only (S); 
//*>          = 'V': for eigenvectors only (SEP); 
//*>          = 'B': for both eigenvalues and eigenvectors (S and SEP). 
//*> \endverbatim 
//*> 
//*> \param[in] HOWMNY 
//*> \verbatim 
//*>          HOWMNY is CHARACTER*1 
//*>          = 'A': compute condition numbers for all eigenpairs; 
//*>          = 'S': compute condition numbers for selected eigenpairs 
//*>                 specified by the array SELECT. 
//*> \endverbatim 
//*> 
//*> \param[in] SELECT 
//*> \verbatim 
//*>          SELECT is LOGICAL array, dimension (N) 
//*>          If HOWMNY = 'S', SELECT specifies the eigenpairs for which 
//*>          condition numbers are required. To select condition numbers 
//*>          for the j-th eigenpair, SELECT(j) must be set to .TRUE.. 
//*>          If HOWMNY = 'A', SELECT is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix T. N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] T 
//*> \verbatim 
//*>          T is COMPLEX*16 array, dimension (LDT,N) 
//*>          The upper triangular matrix T. 
//*> \endverbatim 
//*> 
//*> \param[in] LDT 
//*> \verbatim 
//*>          LDT is INTEGER 
//*>          The leading dimension of the array T. LDT >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[in] VL 
//*> \verbatim 
//*>          VL is COMPLEX*16 array, dimension (LDVL,M) 
//*>          If JOB = 'E' or 'B', VL must contain left eigenvectors of T 
//*>          (or of any Q*T*Q**H with Q unitary), corresponding to the 
//*>          eigenpairs specified by HOWMNY and SELECT. The eigenvectors 
//*>          must be stored in consecutive columns of VL, as returned by 
//*>          ZHSEIN or ZTREVC. 
//*>          If JOB = 'V', VL is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] LDVL 
//*> \verbatim 
//*>          LDVL is INTEGER 
//*>          The leading dimension of the array VL. 
//*>          LDVL >= 1; and if JOB = 'E' or 'B', LDVL >= N. 
//*> \endverbatim 
//*> 
//*> \param[in] VR 
//*> \verbatim 
//*>          VR is COMPLEX*16 array, dimension (LDVR,M) 
//*>          If JOB = 'E' or 'B', VR must contain right eigenvectors of T 
//*>          (or of any Q*T*Q**H with Q unitary), corresponding to the 
//*>          eigenpairs specified by HOWMNY and SELECT. The eigenvectors 
//*>          must be stored in consecutive columns of VR, as returned by 
//*>          ZHSEIN or ZTREVC. 
//*>          If JOB = 'V', VR is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] LDVR 
//*> \verbatim 
//*>          LDVR is INTEGER 
//*>          The leading dimension of the array VR. 
//*>          LDVR >= 1; and if JOB = 'E' or 'B', LDVR >= N. 
//*> \endverbatim 
//*> 
//*> \param[out] S 
//*> \verbatim 
//*>          S is DOUBLE PRECISION array, dimension (MM) 
//*>          If JOB = 'E' or 'B', the reciprocal condition numbers of the 
//*>          selected eigenvalues, stored in consecutive elements of the 
//*>          array. Thus S(j), SEP(j), and the j-th columns of VL and VR 
//*>          all correspond to the same eigenpair (but not in general the 
//*>          j-th eigenpair, unless all eigenpairs are selected). 
//*>          If JOB = 'V', S is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[out] SEP 
//*> \verbatim 
//*>          SEP is DOUBLE PRECISION array, dimension (MM) 
//*>          If JOB = 'V' or 'B', the estimated reciprocal condition 
//*>          numbers of the selected eigenvectors, stored in consecutive 
//*>          elements of the array. 
//*>          If JOB = 'E', SEP is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] MM 
//*> \verbatim 
//*>          MM is INTEGER 
//*>          The number of elements in the arrays S (if JOB = 'E' or 'B') 
//*>           and/or SEP (if JOB = 'V' or 'B'). MM >= M. 
//*> \endverbatim 
//*> 
//*> \param[out] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of elements of the arrays S and/or SEP actually 
//*>          used to store the estimated condition numbers. 
//*>          If HOWMNY = 'A', M is set to N. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is COMPLEX*16 array, dimension (LDWORK,N+6) 
//*>          If JOB = 'E', WORK is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] LDWORK 
//*> \verbatim 
//*>          LDWORK is INTEGER 
//*>          The leading dimension of the array WORK. 
//*>          LDWORK >= 1; and if JOB = 'V' or 'B', LDWORK >= N. 
//*> \endverbatim 
//*> 
//*> \param[out] RWORK 
//*> \verbatim 
//*>          RWORK is DOUBLE PRECISION array, dimension (N) 
//*>          If JOB = 'E', RWORK is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0: successful exit 
//*>          < 0: if INFO = -i, the i-th argument had an illegal value 
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
//*> \date November 2017 
//* 
//*> \ingroup complex16OTHERcomputational 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  The reciprocal of the condition number of an eigenvalue lambda is 
//*>  defined as 
//*> 
//*>          S(lambda) = |v**H*u| / (norm(u)*norm(v)) 
//*> 
//*>  where u and v are the right and left eigenvectors of T corresponding 
//*>  to lambda; v**H denotes the conjugate transpose of v, and norm(u) 
//*>  denotes the Euclidean norm. These reciprocal condition numbers always 
//*>  lie between zero (very badly conditioned) and one (very well 
//*>  conditioned). If n = 1, S(lambda) is defined to be 1. 
//*> 
//*>  An approximate error bound for a computed eigenvalue W(i) is given by 
//*> 
//*>                      EPS * norm(T) / S(i) 
//*> 
//*>  where EPS is the machine precision. 
//*> 
//*>  The reciprocal of the condition number of the right eigenvector u 
//*>  corresponding to lambda is defined as follows. Suppose 
//*> 
//*>              T = ( lambda  c  ) 
//*>                  (   0    T22 ) 
//*> 
//*>  Then the reciprocal condition number is 
//*> 
//*>          SEP( lambda, T22 ) = sigma-min( T22 - lambda*I ) 
//*> 
//*>  where sigma-min denotes the smallest singular value. We approximate 
//*>  the smallest singular value by the reciprocal of an estimate of the 
//*>  one-norm of the inverse of T22 - lambda*I. If n = 1, SEP(1) is 
//*>  defined to be abs(T(1,1)). 
//*> 
//*>  An approximate error bound for a computed right eigenvector VR(i) 
//*>  is given by 
//*> 
//*>                      EPS * norm(T) / SEP(i) 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _f0z2djqh(FString _xcrv93xi, FString _beyjxzyr, Boolean* _2vi7x6ig, ref Int32 _dxpq0xkr, complex* _2ivtt43r, ref Int32 _w8yhbr2r, complex* _ppzorcqs, ref Int32 _uq25zlw0, complex* _b88wiuwq, ref Int32 _oxoory3e, Double* _irk8i6qr, Double* _k21k7dwo, ref Int32 _e9y2lltf, ref Int32 _ev4xhht5, complex* _apig8meb, ref Int32 _iykhdriq, Double* _dqanbbw3, ref Int32 _gro5yvfo)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)28 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d + (int)0;
Boolean _dmht3316 =  default;
Boolean _5nz93l7l =  default;
Boolean _hb7ocmyb =  default;
Boolean _d3t15w6s =  default;
FString _t2799vyr =  new FString(1);
Int32 _b5p6od9s =  default;
Int32 _bhsiylw4 =  default;
Int32 _b69ritwm =  default;
Int32 _znpjgsef =  default;
Int32 _umlkckdg =  default;
Int32 _56nn7y27 =  default;
Int32 _y4o69i44 =  default;
Double _av7j8yda =  default;
Double _p1iqarg6 =  default;
Double _xfqajabj =  default;
Double _rge68tos =  default;
Double _vwz2i1yb =  default;
Double _1m44vtuk =  default;
Double _bogm0gwy =  default;
Double _ziu6urj2 =  default;
complex _n7plx4io =  default;
complex _5mo6x93c =  default;
Int32* _cpeoijal =  (Int32*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Int32) * ((int)3);
complex* _xcf69958 =  (complex*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(complex) * ((int)1);
string fLanavab = default;
#endregion  variable declarations
_xcrv93xi = _xcrv93xi.Convert(1);
_beyjxzyr = _beyjxzyr.Convert(1);

	{
		//* 
		//*  -- LAPACK computational routine (version 3.8.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     November 2017 
		//* 
		//*     .. Scalar Arguments .. 
		//*     .. 
		//*     .. Array Arguments .. 
		//*     .. 
		//* 
		//*  ===================================================================== 
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
		//*     .. Statement Functions .. 
		//*     .. 
		//*     .. Statement Function definitions .. 
		
		
		Func<complex,Double> _4jqx89by = (_a94616nn) => { return (ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.DBLE(_a94616nn ) ) + ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.DIMAG(_a94616nn ) )); };;//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Decode and test the input parameters 
		//* 
		
		_5nz93l7l = _w8y2rzgy(_xcrv93xi ,"B" );
		_hb7ocmyb = (_w8y2rzgy(_xcrv93xi ,"E" ) | _5nz93l7l);
		_d3t15w6s = (_w8y2rzgy(_xcrv93xi ,"V" ) | _5nz93l7l);//* 
		
		_dmht3316 = _w8y2rzgy(_beyjxzyr ,"S" );//* 
		//*     Set M to the number of eigenpairs for which condition numbers are 
		//*     to be computed. 
		//* 
		
		if (_dmht3316)
		{
			
			_ev4xhht5 = (int)0;
			{
				System.Int32 __81fgg2dlsvn2796 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2796 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2796;
				for (__81fgg2count2796 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2796 + __81fgg2step2796) / __81fgg2step2796)), _znpjgsef = __81fgg2dlsvn2796; __81fgg2count2796 != 0; __81fgg2count2796--, _znpjgsef += (__81fgg2step2796)) {

				{
					
					if (*(_2vi7x6ig+(_znpjgsef - 1)))
					_ev4xhht5 = (_ev4xhht5 + (int)1);
Mark10:;
					// continue
				}
								}			}
		}
		else
		{
			
			_ev4xhht5 = _dxpq0xkr;
		}
		//* 
		
		_gro5yvfo = (int)0;
		if ((!(_hb7ocmyb)) & (!(_d3t15w6s)))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if ((!(_w8y2rzgy(_beyjxzyr ,"A" ))) & (!(_dmht3316)))
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if (_w8yhbr2r < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)-6;
		}
		else
		if ((_uq25zlw0 < (int)1) | (_hb7ocmyb & (_uq25zlw0 < _dxpq0xkr)))
		{
			
			_gro5yvfo = (int)-8;
		}
		else
		if ((_oxoory3e < (int)1) | (_hb7ocmyb & (_oxoory3e < _dxpq0xkr)))
		{
			
			_gro5yvfo = (int)-10;
		}
		else
		if (_e9y2lltf < _ev4xhht5)
		{
			
			_gro5yvfo = (int)-13;
		}
		else
		if ((_iykhdriq < (int)1) | (_d3t15w6s & (_iykhdriq < _dxpq0xkr)))
		{
			
			_gro5yvfo = (int)-16;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("ZTRSNA" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr == (int)0)
		return;//* 
		
		if (_dxpq0xkr == (int)1)
		{
			
			if (_dmht3316)
			{
				
				if (!(*(_2vi7x6ig+((int)1 - 1))))
				return;
			}
			
			if (_hb7ocmyb)
			*(_irk8i6qr+((int)1 - 1)) = _kxg5drh2;
			if (_d3t15w6s)
			*(_k21k7dwo+((int)1 - 1)) = ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+((int)1 - 1) + ((int)1 - 1) * 1 * (_w8yhbr2r)) );
			return;
		}
		//* 
		//*     Get machine constants 
		//* 
		
		_p1iqarg6 = _f43eg0w0("P" );
		_bogm0gwy = (_f43eg0w0("S" ) / _p1iqarg6);
		_av7j8yda = (_kxg5drh2 / _bogm0gwy);
		_to4dtyqc(ref _bogm0gwy ,ref _av7j8yda );//* 
		
		_y4o69i44 = (int)1;
		{
			System.Int32 __81fgg2dlsvn2797 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2797 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2797;
			for (__81fgg2count2797 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2797 + __81fgg2step2797) / __81fgg2step2797)), _umlkckdg = __81fgg2dlsvn2797; __81fgg2count2797 != 0; __81fgg2count2797--, _umlkckdg += (__81fgg2step2797)) {

			{
				//* 
				
				if (_dmht3316)
				{
					
					if (!(*(_2vi7x6ig+(_umlkckdg - 1))))goto Mark50;
				}
				//* 
				
				if (_hb7ocmyb)
				{
					//* 
					//*           Compute the reciprocal condition number of the k-th 
					//*           eigenvalue. 
					//* 
					
					_5mo6x93c = _s2hgtw14(ref _dxpq0xkr ,(_b88wiuwq+((int)1 - 1) + (_y4o69i44 - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) ,(_ppzorcqs+((int)1 - 1) + (_y4o69i44 - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );
					_vwz2i1yb = _yzrhzz6l(ref _dxpq0xkr ,(_b88wiuwq+((int)1 - 1) + (_y4o69i44 - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );
					_rge68tos = _yzrhzz6l(ref _dxpq0xkr ,(_ppzorcqs+((int)1 - 1) + (_y4o69i44 - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );
					*(_irk8i6qr+(_y4o69i44 - 1)) = (ILNumerics.F2NET.Intrinsics.ABS(_5mo6x93c ) / (_vwz2i1yb * _rge68tos));//* 
					
				}
				//* 
				
				if (_d3t15w6s)
				{
					//* 
					//*           Estimate the reciprocal condition number of the k-th 
					//*           eigenvector. 
					//* 
					//*           Copy the matrix T to the array WORK and swap the k-th 
					//*           diagonal element to the (1,1) position. 
					//* 
					
					_nihu9ses("Full" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );
					_a8521p1n("No Q" ,ref _dxpq0xkr ,_apig8meb ,ref _iykhdriq ,_xcf69958 ,ref Unsafe.AsRef((int)1) ,ref _umlkckdg ,ref Unsafe.AsRef((int)1) ,ref _bhsiylw4 );//* 
					//*           Form  C = T22 - lambda*I in WORK(2:N,2:N). 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn2798 = (System.Int32)((int)2);
						const System.Int32 __81fgg2step2798 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2798;
						for (__81fgg2count2798 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2798 + __81fgg2step2798) / __81fgg2step2798)), _b5p6od9s = __81fgg2dlsvn2798; __81fgg2count2798 != 0; __81fgg2count2798--, _b5p6od9s += (__81fgg2step2798)) {

						{
							
							*(_apig8meb+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_iykhdriq)) = (*(_apig8meb+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_iykhdriq)) - *(_apig8meb+((int)1 - 1) + ((int)1 - 1) * 1 * (_iykhdriq)));
Mark20:;
							// continue
						}
												}					}//* 
					//*           Estimate a lower bound for the 1-norm of inv(C**H). The 1st 
					//*           and (N+1)th columns of WORK are used to store work vectors. 
					//* 
					
					*(_k21k7dwo+(_y4o69i44 - 1)) = _d0547bi2;
					_xfqajabj = _d0547bi2;
					_56nn7y27 = (int)0;
					
					_t2799vyr = "N";
Mark30:;
					// continue
					_09gqi3a8(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,(_apig8meb+((int)1 - 1) + (_dxpq0xkr + (int)1 - 1) * 1 * (_iykhdriq)),_apig8meb ,ref _xfqajabj ,ref _56nn7y27 ,_cpeoijal );//* 
					
					if (_56nn7y27 != (int)0)
					{
						
						if (_56nn7y27 == (int)1)
						{
							//* 
							//*                 Solve C**H*x = scale*b 
							//* 
							
							_xtlhlop8("Upper" ,"Conjugate transpose" ,"Nonunit" ,_t2799vyr ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,(_apig8meb+((int)2 - 1) + ((int)2 - 1) * 1 * (_iykhdriq)),ref _iykhdriq ,_apig8meb ,ref _1m44vtuk ,_dqanbbw3 ,ref _bhsiylw4 );
						}
						else
						{
							//* 
							//*                 Solve C*x = scale*b 
							//* 
							
							_xtlhlop8("Upper" ,"No transpose" ,"Nonunit" ,_t2799vyr ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,(_apig8meb+((int)2 - 1) + ((int)2 - 1) * 1 * (_iykhdriq)),ref _iykhdriq ,_apig8meb ,ref _1m44vtuk ,_dqanbbw3 ,ref _bhsiylw4 );
						}
						
						
						_t2799vyr = "Y";
						if (_1m44vtuk != _kxg5drh2)
						{
							//* 
							//*                 Multiply by 1/SCALE if doing so will not cause 
							//*                 overflow. 
							//* 
							
							_b69ritwm = _b8lt7tux(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,_apig8meb ,ref Unsafe.AsRef((int)1) );
							_ziu6urj2 = _4jqx89by(*(_apig8meb+(_b69ritwm - 1) + ((int)1 - 1) * 1 * (_iykhdriq)) );
							if ((_1m44vtuk < (_ziu6urj2 * _bogm0gwy)) | (_1m44vtuk == _d0547bi2))goto Mark40;
							_pvlp8a6j(ref _dxpq0xkr ,ref _1m44vtuk ,_apig8meb ,ref Unsafe.AsRef((int)1) );
						}
						goto Mark30;
					}
					//* 
					
					*(_k21k7dwo+(_y4o69i44 - 1)) = (_kxg5drh2 / ILNumerics.F2NET.Intrinsics.MAX(_xfqajabj ,_bogm0gwy ));
				}
				//* 
				
Mark40:;
				// continue
				_y4o69i44 = (_y4o69i44 + (int)1);
Mark50:;
				// continue
			}
						}		}
		return;//* 
		//*     End of ZTRSNA 
		//* 
		
	}
	
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
