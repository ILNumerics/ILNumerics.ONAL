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
//*> \brief \b CTRSNA 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CTRSNA + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/ctrsna.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/ctrsna.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/ctrsna.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CTRSNA( JOB, HOWMNY, SELECT, N, T, LDT, VL, LDVL, VR, 
//*                          LDVR, S, SEP, MM, M, WORK, LDWORK, RWORK, 
//*                          INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          HOWMNY, JOB 
//*       INTEGER            INFO, LDT, LDVL, LDVR, LDWORK, M, MM, N 
//*       .. 
//*       .. Array Arguments .. 
//*       LOGICAL            SELECT( * ) 
//*       REAL               RWORK( * ), S( * ), SEP( * ) 
//*       COMPLEX            T( LDT, * ), VL( LDVL, * ), VR( LDVR, * ), 
//*      $                   WORK( LDWORK, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CTRSNA estimates reciprocal condition numbers for specified 
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
//*>          T is COMPLEX array, dimension (LDT,N) 
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
//*>          VL is COMPLEX array, dimension (LDVL,M) 
//*>          If JOB = 'E' or 'B', VL must contain left eigenvectors of T 
//*>          (or of any Q*T*Q**H with Q unitary), corresponding to the 
//*>          eigenpairs specified by HOWMNY and SELECT. The eigenvectors 
//*>          must be stored in consecutive columns of VL, as returned by 
//*>          CHSEIN or CTREVC. 
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
//*>          VR is COMPLEX array, dimension (LDVR,M) 
//*>          If JOB = 'E' or 'B', VR must contain right eigenvectors of T 
//*>          (or of any Q*T*Q**H with Q unitary), corresponding to the 
//*>          eigenpairs specified by HOWMNY and SELECT. The eigenvectors 
//*>          must be stored in consecutive columns of VR, as returned by 
//*>          CHSEIN or CTREVC. 
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
//*>          array. Thus S(j), SEP(j), and the j-th columns of VL and VR 
//*>          all correspond to the same eigenpair (but not in general the 
//*>          j-th eigenpair, unless all eigenpairs are selected). 
//*>          If JOB = 'V', S is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[out] SEP 
//*> \verbatim 
//*>          SEP is REAL array, dimension (MM) 
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
//*>          WORK is COMPLEX array, dimension (LDWORK,N+6) 
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
//*>          RWORK is REAL array, dimension (N) 
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
//*> \date December 2016 
//* 
//*> \ingroup complexOTHERcomputational 
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

	 
	public static void _n6aaq0cm(FString _xcrv93xi, FString _beyjxzyr, Boolean* _2vi7x6ig, ref Int32 _dxpq0xkr, fcomplex* _2ivtt43r, ref Int32 _w8yhbr2r, fcomplex* _ppzorcqs, ref Int32 _uq25zlw0, fcomplex* _b88wiuwq, ref Int32 _oxoory3e, Single* _irk8i6qr, Single* _k21k7dwo, ref Int32 _e9y2lltf, ref Int32 _ev4xhht5, fcomplex* _apig8meb, ref Int32 _iykhdriq, Single* _dqanbbw3, ref Int32 _gro5yvfo)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)20 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f + (int)0;
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
Single _av7j8yda =  default;
Single _p1iqarg6 =  default;
Single _xfqajabj =  default;
Single _rge68tos =  default;
Single _vwz2i1yb =  default;
Single _1m44vtuk =  default;
Single _bogm0gwy =  default;
Single _ziu6urj2 =  default;
fcomplex _n7plx4io =  default;
fcomplex _5mo6x93c =  default;
Int32* _cpeoijal =  (Int32*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Int32) * ((int)3);
fcomplex* _xcf69958 =  (fcomplex*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(fcomplex) * ((int)1);
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
		//*     .. Statement Functions .. 
		//*     .. 
		//*     .. Statement Function definitions .. 
		
		
		Func<fcomplex,Single> _4jqx89by = (_a94616nn) => { return (ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.REAL(_a94616nn ) ) + ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.AIMAG(_a94616nn ) )); };;//*     .. 
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
				System.Int32 __81fgg2dlsvn2635 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2635 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2635;
				for (__81fgg2count2635 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2635 + __81fgg2step2635) / __81fgg2step2635)), _znpjgsef = __81fgg2dlsvn2635; __81fgg2count2635 != 0; __81fgg2count2635--, _znpjgsef += (__81fgg2step2635)) {

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
			
			_ut9qalzx("CTRSNA" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
		
		_y4o69i44 = (int)1;
		{
			System.Int32 __81fgg2dlsvn2636 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2636 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2636;
			for (__81fgg2count2636 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2636 + __81fgg2step2636) / __81fgg2step2636)), _umlkckdg = __81fgg2dlsvn2636; __81fgg2count2636 != 0; __81fgg2count2636--, _umlkckdg += (__81fgg2step2636)) {

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
					
					_5mo6x93c = _f18dve92(ref _dxpq0xkr ,(_b88wiuwq+((int)1 - 1) + (_y4o69i44 - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) ,(_ppzorcqs+((int)1 - 1) + (_y4o69i44 - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );
					_vwz2i1yb = _igbqnt3f(ref _dxpq0xkr ,(_b88wiuwq+((int)1 - 1) + (_y4o69i44 - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );
					_rge68tos = _igbqnt3f(ref _dxpq0xkr ,(_ppzorcqs+((int)1 - 1) + (_y4o69i44 - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );
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
					
					_szaic8qw("Full" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_2ivtt43r ,ref _w8yhbr2r ,_apig8meb ,ref _iykhdriq );
					_r10bvo9j("No Q" ,ref _dxpq0xkr ,_apig8meb ,ref _iykhdriq ,_xcf69958 ,ref Unsafe.AsRef((int)1) ,ref _umlkckdg ,ref Unsafe.AsRef((int)1) ,ref _bhsiylw4 );//* 
					//*           Form  C = T22 - lambda*I in WORK(2:N,2:N). 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn2637 = (System.Int32)((int)2);
						const System.Int32 __81fgg2step2637 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2637;
						for (__81fgg2count2637 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2637 + __81fgg2step2637) / __81fgg2step2637)), _b5p6od9s = __81fgg2dlsvn2637; __81fgg2count2637 != 0; __81fgg2count2637--, _b5p6od9s += (__81fgg2step2637)) {

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
					_nxat1zy1(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,(_apig8meb+((int)1 - 1) + (_dxpq0xkr + (int)1 - 1) * 1 * (_iykhdriq)),_apig8meb ,ref _xfqajabj ,ref _56nn7y27 ,_cpeoijal );//* 
					
					if (_56nn7y27 != (int)0)
					{
						
						if (_56nn7y27 == (int)1)
						{
							//* 
							//*                 Solve C**H*x = scale*b 
							//* 
							
							_i8j3yqqn("Upper" ,"Conjugate transpose" ,"Nonunit" ,_t2799vyr ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,(_apig8meb+((int)2 - 1) + ((int)2 - 1) * 1 * (_iykhdriq)),ref _iykhdriq ,_apig8meb ,ref _1m44vtuk ,_dqanbbw3 ,ref _bhsiylw4 );
						}
						else
						{
							//* 
							//*                 Solve C*x = scale*b 
							//* 
							
							_i8j3yqqn("Upper" ,"No transpose" ,"Nonunit" ,_t2799vyr ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,(_apig8meb+((int)2 - 1) + ((int)2 - 1) * 1 * (_iykhdriq)),ref _iykhdriq ,_apig8meb ,ref _1m44vtuk ,_dqanbbw3 ,ref _bhsiylw4 );
						}
						
						
						_t2799vyr = "Y";
						if (_1m44vtuk != _kxg5drh2)
						{
							//* 
							//*                 Multiply by 1/SCALE if doing so will not cause 
							//*                 overflow. 
							//* 
							
							_b69ritwm = _r3truie3(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,_apig8meb ,ref Unsafe.AsRef((int)1) );
							_ziu6urj2 = _4jqx89by(*(_apig8meb+(_b69ritwm - 1) + ((int)1 - 1) * 1 * (_iykhdriq)) );
							if ((_1m44vtuk < (_ziu6urj2 * _bogm0gwy)) | (_1m44vtuk == _d0547bi2))goto Mark40;
							_dc775ijh(ref _dxpq0xkr ,ref _1m44vtuk ,_apig8meb ,ref Unsafe.AsRef((int)1) );
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
		//*     End of CTRSNA 
		//* 
		
	}
	
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
