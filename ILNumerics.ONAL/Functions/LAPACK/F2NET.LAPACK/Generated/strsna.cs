
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
//*> \brief \b STRSNA 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download STRSNA + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/strsna.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/strsna.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/strsna.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE STRSNA( JOB, HOWMNY, SELECT, N, T, LDT, VL, LDVL, VR, 
//*                          LDVR, S, SEP, MM, M, WORK, LDWORK, IWORK, 
//*                          INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          HOWMNY, JOB 
//*       INTEGER            INFO, LDT, LDVL, LDVR, LDWORK, M, MM, N 
//*       .. 
//*       .. Array Arguments .. 
//*       LOGICAL            SELECT( * ) 
//*       INTEGER            IWORK( * ) 
//*       REAL               S( * ), SEP( * ), T( LDT, * ), VL( LDVL, * ), 
//*      $                   VR( LDVR, * ), WORK( LDWORK, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> STRSNA estimates reciprocal condition numbers for specified 
//*> eigenvalues and/or right eigenvectors of a real upper 
//*> quasi-triangular matrix T (or of any matrix Q*T*Q**T with Q 
//*> orthogonal). 
//*> 
//*> T must be in Schur canonical form (as returned by SHSEQR), that is, 
//*> block upper triangular with 1-by-1 and 2-by-2 diagonal blocks; each 
//*> 2-by-2 diagonal block has its diagonal elements equal and its 
//*> off-diagonal elements of opposite sign. 
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
//*>          for the eigenpair corresponding to a real eigenvalue w(j), 
//*>          SELECT(j) must be set to .TRUE.. To select condition numbers 
//*>          corresponding to a complex conjugate pair of eigenvalues w(j) 
//*>          and w(j+1), either SELECT(j) or SELECT(j+1) or both, must be 
//*>          set to .TRUE.. 
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
//*>          T is REAL array, dimension (LDT,N) 
//*>          The upper quasi-triangular matrix T, in Schur canonical form. 
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
//*>          VL is REAL array, dimension (LDVL,M) 
//*>          If JOB = 'E' or 'B', VL must contain left eigenvectors of T 
//*>          (or of any Q*T*Q**T with Q orthogonal), corresponding to the 
//*>          eigenpairs specified by HOWMNY and SELECT. The eigenvectors 
//*>          must be stored in consecutive columns of VL, as returned by 
//*>          SHSEIN or STREVC. 
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
//*>          VR is REAL array, dimension (LDVR,M) 
//*>          If JOB = 'E' or 'B', VR must contain right eigenvectors of T 
//*>          (or of any Q*T*Q**T with Q orthogonal), corresponding to the 
//*>          eigenpairs specified by HOWMNY and SELECT. The eigenvectors 
//*>          must be stored in consecutive columns of VR, as returned by 
//*>          SHSEIN or STREVC. 
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
//*>          S is REAL array, dimension (MM) 
//*>          If JOB = 'E' or 'B', the reciprocal condition numbers of the 
//*>          selected eigenvalues, stored in consecutive elements of the 
//*>          array. For a complex conjugate pair of eigenvalues two 
//*>          consecutive elements of S are set to the same value. Thus 
//*>          S(j), SEP(j), and the j-th columns of VL and VR all 
//*>          correspond to the same eigenpair (but not in general the 
//*>          j-th eigenpair, unless all eigenpairs are selected). 
//*>          If JOB = 'V', S is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[out] SEP 
//*> \verbatim 
//*>          SEP is REAL array, dimension (MM) 
//*>          If JOB = 'V' or 'B', the estimated reciprocal condition 
//*>          numbers of the selected eigenvectors, stored in consecutive 
//*>          elements of the array. For a complex eigenvector two 
//*>          consecutive elements of SEP are set to the same value. If 
//*>          the eigenvalues cannot be reordered to compute SEP(j), SEP(j) 
//*>          is set to 0; this can only occur when the true value would be 
//*>          very small anyway. 
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
//*>          WORK is REAL array, dimension (LDWORK,N+6) 
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
//*> \param[out] IWORK 
//*> \verbatim 
//*>          IWORK is INTEGER array, dimension (2*(N-1)) 
//*>          If JOB = 'E', IWORK is not referenced. 
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
//*> \date December 2016 
//* 
//*> \ingroup realOTHERcomputational 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  The reciprocal of the condition number of an eigenvalue lambda is 
//*>  defined as 
//*> 
//*>          S(lambda) = |v**T*u| / (norm(u)*norm(v)) 
//*> 
//*>  where u and v are the right and left eigenvectors of T corresponding 
//*>  to lambda; v**T denotes the transpose of v, and norm(u) 
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

	 
	public static void _aif3dzif(FString _xcrv93xi, FString _beyjxzyr, Boolean* _2vi7x6ig, ref Int32 _dxpq0xkr, Single* _2ivtt43r, ref Int32 _w8yhbr2r, Single* _ppzorcqs, ref Int32 _uq25zlw0, Single* _b88wiuwq, ref Int32 _oxoory3e, Single* _irk8i6qr, Single* _k21k7dwo, ref Int32 _e9y2lltf, ref Int32 _ev4xhht5, Single* _apig8meb, ref Int32 _iykhdriq, Int32* _4b6rt45i, ref Int32 _gro5yvfo)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)16 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
Single _5m0mjfxm =  2f;
Boolean _94xnzecl =  default;
Boolean _dmht3316 =  default;
Boolean _5nz93l7l =  default;
Boolean _hb7ocmyb =  default;
Boolean _d3t15w6s =  default;
Int32 _b5p6od9s =  default;
Int32 _bhsiylw4 =  default;
Int32 _la6t805m =  default;
Int32 _ab05c09e =  default;
Int32 _znpjgsef =  default;
Int32 _umlkckdg =  default;
Int32 _56nn7y27 =  default;
Int32 _y4o69i44 =  default;
Int32 _tixk7d1h =  default;
Int32 _8dgyhtzt =  default;
Single _av7j8yda =  default;
Single _cdyvumg9 =  default;
Single _82tpdhyl =  default;
Single _9zhf8o7p =  default;
Single _5pc6ghl1 =  default;
Single _p1iqarg6 =  default;
Single _xfqajabj =  default;
Single _rge68tos =  default;
Single _4s3y5e1x =  default;
Single _5mo6x93c =  default;
Single _j9m8xcr0 =  default;
Single _xu5pt9my =  default;
Single _vwz2i1yb =  default;
Single _1m44vtuk =  default;
Single _bogm0gwy =  default;
Single _8tmd0ner =  default;
Int32* _cpeoijal =  (Int32*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Int32) * ((int)3);
Single* _xcf69958 =  (Single*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Single) * ((int)1);
string fLanavab = default;
#endregion  variable declarations
_xcrv93xi = _xcrv93xi.Convert(1);
_beyjxzyr = _beyjxzyr.Convert(1);

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
		//*     Decode and test the input parameters 
		//* 
		
		_5nz93l7l = _w8y2rzgy(_xcrv93xi ,"B" );
		_hb7ocmyb = (_w8y2rzgy(_xcrv93xi ,"E" ) | _5nz93l7l);
		_d3t15w6s = (_w8y2rzgy(_xcrv93xi ,"V" ) | _5nz93l7l);//* 
		
		_dmht3316 = _w8y2rzgy(_beyjxzyr ,"S" );//* 
		
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
		{
			//* 
			//*        Set M to the number of eigenpairs for which condition numbers 
			//*        are required, and test MM. 
			//* 
			
			if (_dmht3316)
			{
				
				_ev4xhht5 = (int)0;
				_94xnzecl = false;
				{
					System.Int32 __81fgg2dlsvn2482 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2482 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2482;
					for (__81fgg2count2482 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2482 + __81fgg2step2482) / __81fgg2step2482)), _umlkckdg = __81fgg2dlsvn2482; __81fgg2count2482 != 0; __81fgg2count2482--, _umlkckdg += (__81fgg2step2482)) {

					{
						
						if (_94xnzecl)
						{
							
							_94xnzecl = false;
						}
						else
						{
							
							if (_umlkckdg < _dxpq0xkr)
							{
								
								if (*(_2ivtt43r+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_w8yhbr2r)) == _d0547bi2)
								{
									
									if (*(_2vi7x6ig+(_umlkckdg - 1)))
									_ev4xhht5 = (_ev4xhht5 + (int)1);
								}
								else
								{
									
									_94xnzecl = true;
									if (*(_2vi7x6ig+(_umlkckdg - 1)) | *(_2vi7x6ig+(_umlkckdg + (int)1 - 1)))
									_ev4xhht5 = (_ev4xhht5 + (int)2);
								}
								
							}
							else
							{
								
								if (*(_2vi7x6ig+(_dxpq0xkr - 1)))
								_ev4xhht5 = (_ev4xhht5 + (int)1);
							}
							
						}
						
Mark10:;
						// continue
					}
										}				}
			}
			else
			{
				
				_ev4xhht5 = _dxpq0xkr;
			}
			//* 
			
			if (_e9y2lltf < _ev4xhht5)
			{
				
				_gro5yvfo = (int)-13;
			}
			else
			if ((_iykhdriq < (int)1) | (_d3t15w6s & (_iykhdriq < _dxpq0xkr)))
			{
				
				_gro5yvfo = (int)-16;
			}
			
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("STRSNA" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
		
		_p1iqarg6 = _d5tu038y("P" );
		_bogm0gwy = (_d5tu038y("S" ) / _p1iqarg6);
		_av7j8yda = (_kxg5drh2 / _bogm0gwy);
		_6cljvt6b(ref _bogm0gwy ,ref _av7j8yda );//* 
		
		_y4o69i44 = (int)0;
		_94xnzecl = false;
		{
			System.Int32 __81fgg2dlsvn2483 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2483 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2483;
			for (__81fgg2count2483 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2483 + __81fgg2step2483) / __81fgg2step2483)), _umlkckdg = __81fgg2dlsvn2483; __81fgg2count2483 != 0; __81fgg2count2483--, _umlkckdg += (__81fgg2step2483)) {

			{
				//* 
				//*        Determine whether T(k,k) begins a 1-by-1 or 2-by-2 block. 
				//* 
				
				if (_94xnzecl)
				{
					
					_94xnzecl = false;goto Mark60;
				}
				else
				{
					
					if (_umlkckdg < _dxpq0xkr)
					_94xnzecl = (*(_2ivtt43r+(_umlkckdg + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_w8yhbr2r)) != _d0547bi2);
				}
				//* 
				//*        Determine whether condition numbers are required for the k-th 
				//*        eigenpair. 
				//* 
				
				if (_dmht3316)
				{
					
					if (_94xnzecl)
					{
						
						if ((!(*(_2vi7x6ig+(_umlkckdg - 1)))) & (!(*(_2vi7x6ig+(_umlkckdg + (int)1 - 1)))))goto Mark60;
					}
					else
					{
						
						if (!(*(_2vi7x6ig+(_umlkckdg - 1))))goto Mark60;
					}
					
				}
				//* 
				
				_y4o69i44 = (_y4o69i44 + (int)1);//* 
				
				if (_hb7ocmyb)
				{
					//* 
					//*           Compute the reciprocal condition number of the k-th 
					//*           eigenvalue. 
					//* 
					
					if (!(_94xnzecl))
					{
						//* 
						//*              Real eigenvalue. 
						//* 
						
						_5mo6x93c = _j4n7j2pu(ref _dxpq0xkr ,(_b88wiuwq+((int)1 - 1) + (_y4o69i44 - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) ,(_ppzorcqs+((int)1 - 1) + (_y4o69i44 - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );
						_vwz2i1yb = _z20xbrro(ref _dxpq0xkr ,(_b88wiuwq+((int)1 - 1) + (_y4o69i44 - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );
						_rge68tos = _z20xbrro(ref _dxpq0xkr ,(_ppzorcqs+((int)1 - 1) + (_y4o69i44 - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );
						*(_irk8i6qr+(_y4o69i44 - 1)) = (ILNumerics.F2NET.Intrinsics.ABS(_5mo6x93c ) / (_vwz2i1yb * _rge68tos));
					}
					else
					{
						//* 
						//*              Complex eigenvalue. 
						//* 
						
						_j9m8xcr0 = _j4n7j2pu(ref _dxpq0xkr ,(_b88wiuwq+((int)1 - 1) + (_y4o69i44 - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) ,(_ppzorcqs+((int)1 - 1) + (_y4o69i44 - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );
						_j9m8xcr0 = (_j9m8xcr0 + _j4n7j2pu(ref _dxpq0xkr ,(_b88wiuwq+((int)1 - 1) + (_y4o69i44 + (int)1 - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) ,(_ppzorcqs+((int)1 - 1) + (_y4o69i44 + (int)1 - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) ));
						_xu5pt9my = _j4n7j2pu(ref _dxpq0xkr ,(_ppzorcqs+((int)1 - 1) + (_y4o69i44 - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) ,(_b88wiuwq+((int)1 - 1) + (_y4o69i44 + (int)1 - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );
						_xu5pt9my = (_xu5pt9my - _j4n7j2pu(ref _dxpq0xkr ,(_ppzorcqs+((int)1 - 1) + (_y4o69i44 + (int)1 - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) ,(_b88wiuwq+((int)1 - 1) + (_y4o69i44 - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) ));
						_vwz2i1yb = _syk7170d(ref Unsafe.AsRef(_z20xbrro(ref _dxpq0xkr ,(_b88wiuwq+((int)1 - 1) + (_y4o69i44 - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) )) ,ref Unsafe.AsRef(_z20xbrro(ref _dxpq0xkr ,(_b88wiuwq+((int)1 - 1) + (_y4o69i44 + (int)1 - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) )) );
						_rge68tos = _syk7170d(ref Unsafe.AsRef(_z20xbrro(ref _dxpq0xkr ,(_ppzorcqs+((int)1 - 1) + (_y4o69i44 - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) )) ,ref Unsafe.AsRef(_z20xbrro(ref _dxpq0xkr ,(_ppzorcqs+((int)1 - 1) + (_y4o69i44 + (int)1 - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) )) );
						_cdyvumg9 = (_syk7170d(ref _j9m8xcr0 ,ref _xu5pt9my ) / (_vwz2i1yb * _rge68tos));
						*(_irk8i6qr+(_y4o69i44 - 1)) = _cdyvumg9;
						*(_irk8i6qr+(_y4o69i44 + (int)1 - 1)) = _cdyvumg9;
					}
					
				}
				//* 
				
				if (_d3t15w6s)
				{
					//* 
					//*           Estimate the reciprocal condition number of the k-th 
					//*           eigenvector. 
					//* 
					//*           Copy the matrix T to the array WORK and swap the diagonal 
					//*           block beginning at T(k,k) to the (1,1) position. 
					//* 
					
					_m38y8dyg("Full" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );
					_la6t805m = _umlkckdg;
					_ab05c09e = (int)1;
					_ogu7zj4u("No Q" ,ref _dxpq0xkr ,_apig8meb ,ref _iykhdriq ,_xcf69958 ,ref Unsafe.AsRef((int)1) ,ref _la6t805m ,ref _ab05c09e ,(_apig8meb+((int)1 - 1) + (_dxpq0xkr + (int)1 - 1) * 1 * (_iykhdriq)),ref _bhsiylw4 );//* 
					
					if ((_bhsiylw4 == (int)1) | (_bhsiylw4 == (int)2))
					{
						//* 
						//*              Could not swap because blocks not well separated 
						//* 
						
						_1m44vtuk = _kxg5drh2;
						_xfqajabj = _av7j8yda;
					}
					else
					{
						//* 
						//*              Reordering successful 
						//* 
						
						if (*(_apig8meb+((int)2 - 1) + ((int)1 - 1) * 1 * (_iykhdriq)) == _d0547bi2)
						{
							//* 
							//*                 Form C = T22 - lambda*I in WORK(2:N,2:N). 
							//* 
							
							{
								System.Int32 __81fgg2dlsvn2484 = (System.Int32)((int)2);
								const System.Int32 __81fgg2step2484 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2484;
								for (__81fgg2count2484 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2484 + __81fgg2step2484) / __81fgg2step2484)), _b5p6od9s = __81fgg2dlsvn2484; __81fgg2count2484 != 0; __81fgg2count2484--, _b5p6od9s += (__81fgg2step2484)) {

								{
									
									*(_apig8meb+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_iykhdriq)) = (*(_apig8meb+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_iykhdriq)) - *(_apig8meb+((int)1 - 1) + ((int)1 - 1) * 1 * (_iykhdriq)));
Mark20:;
									// continue
								}
																}							}
							_tixk7d1h = (int)1;
							_8dgyhtzt = (_dxpq0xkr - (int)1);
						}
						else
						{
							//* 
							//*                 Triangularize the 2 by 2 block by unitary 
							//*                 transformation U = [  cs   i*ss ] 
							//*                                    [ i*ss   cs  ]. 
							//*                 such that the (1,1) position of WORK is complex 
							//*                 eigenvalue lambda with positive imaginary part. (2,2) 
							//*                 position of WORK is the complex eigenvalue lambda 
							//*                 with negative imaginary  part. 
							//* 
							
							_4s3y5e1x = (ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+((int)1 - 1) + ((int)2 - 1) * 1 * (_iykhdriq)) ) ) * ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+((int)2 - 1) + ((int)1 - 1) * 1 * (_iykhdriq)) ) ));
							_9zhf8o7p = _syk7170d(ref _4s3y5e1x ,ref Unsafe.AsRef(*(_apig8meb+((int)2 - 1) + ((int)1 - 1) * 1 * (_iykhdriq))) );
							_82tpdhyl = (_4s3y5e1x / _9zhf8o7p);
							_8tmd0ner = (-((*(_apig8meb+((int)2 - 1) + ((int)1 - 1) * 1 * (_iykhdriq)) / _9zhf8o7p)));//* 
							//*                 Form 
							//* 
							//*                 C**T = WORK(2:N,2:N) + i*[rwork(1) ..... rwork(n-1) ] 
							//*                                          [   mu                     ] 
							//*                                          [         ..               ] 
							//*                                          [             ..           ] 
							//*                                          [                  mu      ] 
							//*                 where C**T is transpose of matrix C, 
							//*                 and RWORK is stored starting in the N+1-st column of 
							//*                 WORK. 
							//* 
							
							{
								System.Int32 __81fgg2dlsvn2485 = (System.Int32)((int)3);
								const System.Int32 __81fgg2step2485 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2485;
								for (__81fgg2count2485 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2485 + __81fgg2step2485) / __81fgg2step2485)), _znpjgsef = __81fgg2dlsvn2485; __81fgg2count2485 != 0; __81fgg2count2485--, _znpjgsef += (__81fgg2step2485)) {

								{
									
									*(_apig8meb+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)) = (_82tpdhyl * *(_apig8meb+((int)2 - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)));
									*(_apig8meb+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)) = (*(_apig8meb+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_iykhdriq)) - *(_apig8meb+((int)1 - 1) + ((int)1 - 1) * 1 * (_iykhdriq)));
Mark30:;
									// continue
								}
																}							}
							*(_apig8meb+((int)2 - 1) + ((int)2 - 1) * 1 * (_iykhdriq)) = _d0547bi2;//* 
							
							*(_apig8meb+((int)1 - 1) + (_dxpq0xkr + (int)1 - 1) * 1 * (_iykhdriq)) = (_5m0mjfxm * _4s3y5e1x);
							{
								System.Int32 __81fgg2dlsvn2486 = (System.Int32)((int)2);
								const System.Int32 __81fgg2step2486 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2486;
								for (__81fgg2count2486 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn2486 + __81fgg2step2486) / __81fgg2step2486)), _b5p6od9s = __81fgg2dlsvn2486; __81fgg2count2486 != 0; __81fgg2count2486--, _b5p6od9s += (__81fgg2step2486)) {

								{
									
									*(_apig8meb+(_b5p6od9s - 1) + (_dxpq0xkr + (int)1 - 1) * 1 * (_iykhdriq)) = (_8tmd0ner * *(_apig8meb+((int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_iykhdriq)));
Mark40:;
									// continue
								}
																}							}
							_tixk7d1h = (int)2;
							_8dgyhtzt = ((int)2 * (_dxpq0xkr - (int)1));
						}
						//* 
						//*              Estimate norm(inv(C**T)) 
						//* 
						
						_xfqajabj = _d0547bi2;
						_56nn7y27 = (int)0;
Mark50:;
						// continue
						_vuk6gcrf(ref _8dgyhtzt ,(_apig8meb+((int)1 - 1) + (_dxpq0xkr + (int)2 - 1) * 1 * (_iykhdriq)),(_apig8meb+((int)1 - 1) + (_dxpq0xkr + (int)4 - 1) * 1 * (_iykhdriq)),_4b6rt45i ,ref _xfqajabj ,ref _56nn7y27 ,_cpeoijal );
						if (_56nn7y27 != (int)0)
						{
							
							if (_56nn7y27 == (int)1)
							{
								
								if (_tixk7d1h == (int)1)
								{
									//* 
									//*                       Real eigenvalue: solve C**T*x = scale*c. 
									//* 
									
									_5wnwis1w(ref Unsafe.AsRef(true) ,ref Unsafe.AsRef(true) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,(_apig8meb+((int)2 - 1) + ((int)2 - 1) * 1 * (_iykhdriq)),ref _iykhdriq ,_xcf69958 ,ref _5pc6ghl1 ,ref _1m44vtuk ,(_apig8meb+((int)1 - 1) + (_dxpq0xkr + (int)4 - 1) * 1 * (_iykhdriq)),(_apig8meb+((int)1 - 1) + (_dxpq0xkr + (int)6 - 1) * 1 * (_iykhdriq)),ref _bhsiylw4 );
								}
								else
								{
									//* 
									//*                       Complex eigenvalue: solve 
									//*                       C**T*(p+iq) = scale*(c+id) in real arithmetic. 
									//* 
									
									_5wnwis1w(ref Unsafe.AsRef(true) ,ref Unsafe.AsRef(false) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,(_apig8meb+((int)2 - 1) + ((int)2 - 1) * 1 * (_iykhdriq)),ref _iykhdriq ,(_apig8meb+((int)1 - 1) + (_dxpq0xkr + (int)1 - 1) * 1 * (_iykhdriq)),ref _4s3y5e1x ,ref _1m44vtuk ,(_apig8meb+((int)1 - 1) + (_dxpq0xkr + (int)4 - 1) * 1 * (_iykhdriq)),(_apig8meb+((int)1 - 1) + (_dxpq0xkr + (int)6 - 1) * 1 * (_iykhdriq)),ref _bhsiylw4 );
								}
								
							}
							else
							{
								
								if (_tixk7d1h == (int)1)
								{
									//* 
									//*                       Real eigenvalue: solve C*x = scale*c. 
									//* 
									
									_5wnwis1w(ref Unsafe.AsRef(false) ,ref Unsafe.AsRef(true) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,(_apig8meb+((int)2 - 1) + ((int)2 - 1) * 1 * (_iykhdriq)),ref _iykhdriq ,_xcf69958 ,ref _5pc6ghl1 ,ref _1m44vtuk ,(_apig8meb+((int)1 - 1) + (_dxpq0xkr + (int)4 - 1) * 1 * (_iykhdriq)),(_apig8meb+((int)1 - 1) + (_dxpq0xkr + (int)6 - 1) * 1 * (_iykhdriq)),ref _bhsiylw4 );
								}
								else
								{
									//* 
									//*                       Complex eigenvalue: solve 
									//*                       C*(p+iq) = scale*(c+id) in real arithmetic. 
									//* 
									
									_5wnwis1w(ref Unsafe.AsRef(false) ,ref Unsafe.AsRef(false) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,(_apig8meb+((int)2 - 1) + ((int)2 - 1) * 1 * (_iykhdriq)),ref _iykhdriq ,(_apig8meb+((int)1 - 1) + (_dxpq0xkr + (int)1 - 1) * 1 * (_iykhdriq)),ref _4s3y5e1x ,ref _1m44vtuk ,(_apig8meb+((int)1 - 1) + (_dxpq0xkr + (int)4 - 1) * 1 * (_iykhdriq)),(_apig8meb+((int)1 - 1) + (_dxpq0xkr + (int)6 - 1) * 1 * (_iykhdriq)),ref _bhsiylw4 );//* 
									
								}
								
							}
							//* 
							goto Mark50;
						}
						
					}
					//* 
					
					*(_k21k7dwo+(_y4o69i44 - 1)) = (_1m44vtuk / ILNumerics.F2NET.Intrinsics.MAX(_xfqajabj ,_bogm0gwy ));
					if (_94xnzecl)
					*(_k21k7dwo+(_y4o69i44 + (int)1 - 1)) = *(_k21k7dwo+(_y4o69i44 - 1));
				}
				//* 
				
				if (_94xnzecl)
				_y4o69i44 = (_y4o69i44 + (int)1);//* 
				
Mark60:;
				// continue
			}
						}		}
		return;//* 
		//*     End of STRSNA 
		//* 
		
	}
	
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
