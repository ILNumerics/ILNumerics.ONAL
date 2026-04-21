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
//*> \brief \b SSTEMR 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SSTEMR + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/sstemr.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/sstemr.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/sstemr.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SSTEMR( JOBZ, RANGE, N, D, E, VL, VU, IL, IU, 
//*                          M, W, Z, LDZ, NZC, ISUPPZ, TRYRAC, WORK, LWORK, 
//*                          IWORK, LIWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          JOBZ, RANGE 
//*       LOGICAL            TRYRAC 
//*       INTEGER            IL, INFO, IU, LDZ, NZC, LIWORK, LWORK, M, N 
//*       REAL               VL, VU 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            ISUPPZ( * ), IWORK( * ) 
//*       REAL               D( * ), E( * ), W( * ), WORK( * ) 
//*       REAL               Z( LDZ, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SSTEMR computes selected eigenvalues and, optionally, eigenvectors 
//*> of a real symmetric tridiagonal matrix T. Any such unreduced matrix has 
//*> a well defined set of pairwise different real eigenvalues, the corresponding 
//*> real eigenvectors are pairwise orthogonal. 
//*> 
//*> The spectrum may be computed either completely or partially by specifying 
//*> either an interval (VL,VU] or a range of indices IL:IU for the desired 
//*> eigenvalues. 
//*> 
//*> Depending on the number of desired eigenvalues, these are computed either 
//*> by bisection or the dqds algorithm. Numerically orthogonal eigenvectors are 
//*> computed by the use of various suitable L D L^T factorizations near clusters 
//*> of close eigenvalues (referred to as RRRs, Relatively Robust 
//*> Representations). An informal sketch of the algorithm follows. 
//*> 
//*> For each unreduced block (submatrix) of T, 
//*>    (a) Compute T - sigma I  = L D L^T, so that L and D 
//*>        define all the wanted eigenvalues to high relative accuracy. 
//*>        This means that small relative changes in the entries of D and L 
//*>        cause only small relative changes in the eigenvalues and 
//*>        eigenvectors. The standard (unfactored) representation of the 
//*>        tridiagonal matrix T does not have this property in general. 
//*>    (b) Compute the eigenvalues to suitable accuracy. 
//*>        If the eigenvectors are desired, the algorithm attains full 
//*>        accuracy of the computed eigenvalues only right before 
//*>        the corresponding vectors have to be computed, see steps c) and d). 
//*>    (c) For each cluster of close eigenvalues, select a new 
//*>        shift close to the cluster, find a new factorization, and refine 
//*>        the shifted eigenvalues to suitable accuracy. 
//*>    (d) For each eigenvalue with a large enough relative separation compute 
//*>        the corresponding eigenvector by forming a rank revealing twisted 
//*>        factorization. Go back to (c) for any clusters that remain. 
//*> 
//*> For more details, see: 
//*> - Inderjit S. Dhillon and Beresford N. Parlett: "Multiple representations 
//*>   to compute orthogonal eigenvectors of symmetric tridiagonal matrices," 
//*>   Linear Algebra and its Applications, 387(1), pp. 1-28, August 2004. 
//*> - Inderjit Dhillon and Beresford Parlett: "Orthogonal Eigenvectors and 
//*>   Relative Gaps," SIAM Journal on Matrix Analysis and Applications, Vol. 25, 
//*>   2004.  Also LAPACK Working Note 154. 
//*> - Inderjit Dhillon: "A new O(n^2) algorithm for the symmetric 
//*>   tridiagonal eigenvalue/eigenvector problem", 
//*>   Computer Science Division Technical Report No. UCB/CSD-97-971, 
//*>   UC Berkeley, May 1997. 
//*> 
//*> Further Details 
//*> 1.SSTEMR works only on machines which follow IEEE-754 
//*> floating-point standard in their handling of infinities and NaNs. 
//*> This permits the use of efficient inner loops avoiding a check for 
//*> zero divisors. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] JOBZ 
//*> \verbatim 
//*>          JOBZ is CHARACTER*1 
//*>          = 'N':  Compute eigenvalues only; 
//*>          = 'V':  Compute eigenvalues and eigenvectors. 
//*> \endverbatim 
//*> 
//*> \param[in] RANGE 
//*> \verbatim 
//*>          RANGE is CHARACTER*1 
//*>          = 'A': all eigenvalues will be found. 
//*>          = 'V': all eigenvalues in the half-open interval (VL,VU] 
//*>                 will be found. 
//*>          = 'I': the IL-th through IU-th eigenvalues will be found. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] D 
//*> \verbatim 
//*>          D is REAL array, dimension (N) 
//*>          On entry, the N diagonal elements of the tridiagonal matrix 
//*>          T. On exit, D is overwritten. 
//*> \endverbatim 
//*> 
//*> \param[in,out] E 
//*> \verbatim 
//*>          E is REAL array, dimension (N) 
//*>          On entry, the (N-1) subdiagonal elements of the tridiagonal 
//*>          matrix T in elements 1 to N-1 of E. E(N) need not be set on 
//*>          input, but is used internally as workspace. 
//*>          On exit, E is overwritten. 
//*> \endverbatim 
//*> 
//*> \param[in] VL 
//*> \verbatim 
//*>          VL is REAL 
//*> 
//*>          If RANGE='V', the lower bound of the interval to 
//*>          be searched for eigenvalues. VL < VU. 
//*>          Not referenced if RANGE = 'A' or 'I'. 
//*> \endverbatim 
//*> 
//*> \param[in] VU 
//*> \verbatim 
//*>          VU is REAL 
//*> 
//*>          If RANGE='V', the upper bound of the interval to 
//*>          be searched for eigenvalues. VL < VU. 
//*>          Not referenced if RANGE = 'A' or 'I'. 
//*> \endverbatim 
//*> 
//*> \param[in] IL 
//*> \verbatim 
//*>          IL is INTEGER 
//*> 
//*>          If RANGE='I', the index of the 
//*>          smallest eigenvalue to be returned. 
//*>          1 <= IL <= IU <= N, if N > 0. 
//*>          Not referenced if RANGE = 'A' or 'V'. 
//*> \endverbatim 
//*> 
//*> \param[in] IU 
//*> \verbatim 
//*>          IU is INTEGER 
//*> 
//*>          If RANGE='I', the index of the 
//*>          largest eigenvalue to be returned. 
//*>          1 <= IL <= IU <= N, if N > 0. 
//*>          Not referenced if RANGE = 'A' or 'V'. 
//*> \endverbatim 
//*> 
//*> \param[out] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The total number of eigenvalues found.  0 <= M <= N. 
//*>          If RANGE = 'A', M = N, and if RANGE = 'I', M = IU-IL+1. 
//*> \endverbatim 
//*> 
//*> \param[out] W 
//*> \verbatim 
//*>          W is REAL array, dimension (N) 
//*>          The first M elements contain the selected eigenvalues in 
//*>          ascending order. 
//*> \endverbatim 
//*> 
//*> \param[out] Z 
//*> \verbatim 
//*>          Z is REAL array, dimension (LDZ, max(1,M) ) 
//*>          If JOBZ = 'V', and if INFO = 0, then the first M columns of Z 
//*>          contain the orthonormal eigenvectors of the matrix T 
//*>          corresponding to the selected eigenvalues, with the i-th 
//*>          column of Z holding the eigenvector associated with W(i). 
//*>          If JOBZ = 'N', then Z is not referenced. 
//*>          Note: the user must ensure that at least max(1,M) columns are 
//*>          supplied in the array Z; if RANGE = 'V', the exact value of M 
//*>          is not known in advance and can be computed with a workspace 
//*>          query by setting NZC = -1, see below. 
//*> \endverbatim 
//*> 
//*> \param[in] LDZ 
//*> \verbatim 
//*>          LDZ is INTEGER 
//*>          The leading dimension of the array Z.  LDZ >= 1, and if 
//*>          JOBZ = 'V', then LDZ >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[in] NZC 
//*> \verbatim 
//*>          NZC is INTEGER 
//*>          The number of eigenvectors to be held in the array Z. 
//*>          If RANGE = 'A', then NZC >= max(1,N). 
//*>          If RANGE = 'V', then NZC >= the number of eigenvalues in (VL,VU]. 
//*>          If RANGE = 'I', then NZC >= IU-IL+1. 
//*>          If NZC = -1, then a workspace query is assumed; the 
//*>          routine calculates the number of columns of the array Z that 
//*>          are needed to hold the eigenvectors. 
//*>          This value is returned as the first entry of the Z array, and 
//*>          no error message related to NZC is issued by XERBLA. 
//*> \endverbatim 
//*> 
//*> \param[out] ISUPPZ 
//*> \verbatim 
//*>          ISUPPZ is INTEGER array, dimension ( 2*max(1,M) ) 
//*>          The support of the eigenvectors in Z, i.e., the indices 
//*>          indicating the nonzero elements in Z. The i-th computed eigenvector 
//*>          is nonzero only in elements ISUPPZ( 2*i-1 ) through 
//*>          ISUPPZ( 2*i ). This is relevant in the case when the matrix 
//*>          is split. ISUPPZ is only accessed when JOBZ is 'V' and N > 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] TRYRAC 
//*> \verbatim 
//*>          TRYRAC is LOGICAL 
//*>          If TRYRAC = .TRUE., indicates that the code should check whether 
//*>          the tridiagonal matrix defines its eigenvalues to high relative 
//*>          accuracy.  If so, the code uses relative-accuracy preserving 
//*>          algorithms that might be (a bit) slower depending on the matrix. 
//*>          If the matrix does not define its eigenvalues to high relative 
//*>          accuracy, the code can uses possibly faster algorithms. 
//*>          If TRYRAC = .FALSE., the code is not required to guarantee 
//*>          relatively accurate eigenvalues and can use the fastest possible 
//*>          techniques. 
//*>          On exit, a .TRUE. TRYRAC will be set to .FALSE. if the matrix 
//*>          does not define its eigenvalues to high relative accuracy. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is REAL array, dimension (LWORK) 
//*>          On exit, if INFO = 0, WORK(1) returns the optimal 
//*>          (and minimal) LWORK. 
//*> \endverbatim 
//*> 
//*> \param[in] LWORK 
//*> \verbatim 
//*>          LWORK is INTEGER 
//*>          The dimension of the array WORK. LWORK >= max(1,18*N) 
//*>          if JOBZ = 'V', and LWORK >= max(1,12*N) if JOBZ = 'N'. 
//*>          If LWORK = -1, then a workspace query is assumed; the routine 
//*>          only calculates the optimal size of the WORK array, returns 
//*>          this value as the first entry of the WORK array, and no error 
//*>          message related to LWORK is issued by XERBLA. 
//*> \endverbatim 
//*> 
//*> \param[out] IWORK 
//*> \verbatim 
//*>          IWORK is INTEGER array, dimension (LIWORK) 
//*>          On exit, if INFO = 0, IWORK(1) returns the optimal LIWORK. 
//*> \endverbatim 
//*> 
//*> \param[in] LIWORK 
//*> \verbatim 
//*>          LIWORK is INTEGER 
//*>          The dimension of the array IWORK.  LIWORK >= max(1,10*N) 
//*>          if the eigenvectors are desired, and LIWORK >= max(1,8*N) 
//*>          if only the eigenvalues are to be computed. 
//*>          If LIWORK = -1, then a workspace query is assumed; the 
//*>          routine only calculates the optimal size of the IWORK array, 
//*>          returns this value as the first entry of the IWORK array, and 
//*>          no error message related to LIWORK is issued by XERBLA. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          On exit, INFO 
//*>          = 0:  successful exit 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value 
//*>          > 0:  if INFO = 1X, internal error in SLARRE, 
//*>                if INFO = 2X, internal error in SLARRV. 
//*>                Here, the digit X = ABS( IINFO ) < 10, where IINFO is 
//*>                the nonzero error code returned by SLARRE or 
//*>                SLARRV, respectively. 
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
//*> \ingroup realOTHERcomputational 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*> Beresford Parlett, University of California, Berkeley, USA \n 
//*> Jim Demmel, University of California, Berkeley, USA \n 
//*> Inderjit Dhillon, University of Texas, Austin, USA \n 
//*> Osni Marques, LBNL/NERSC, USA \n 
//*> Christof Voemel, University of California, Berkeley, USA 
//* 
//*  ===================================================================== 

	 
	public static void _wmtbz48f(FString _w6igfk2h, FString _wrqmi80z, ref Int32 _dxpq0xkr, Single* _plfm7z8g, Single* _864fslqq, ref Single _ppzorcqs, ref Single _qqhwr930, ref Int32 _ic6kua09, ref Int32 _j4l29b9c, ref Int32 _ev4xhht5, Single* _z1ioc3c8, Single* _7e60fcso, ref Int32 _5l1tna8s, ref Int32 _ghjpby1f, Int32* _nr4g8ae2, ref Boolean _4h81eu1p, Single* _apig8meb, ref Int32 _6fnxzlyp, Int32* _4b6rt45i, ref Int32 _29mhiasb, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
Single _ax5ijvbx =  4f;
Single _i7h9sv6c =  0.003f;
Boolean _w72qtubt =  default;
Boolean _neo0j9hw =  default;
Boolean _lhlgm7z5 =  default;
Boolean _hessdr7t =  default;
Boolean _189gzykk =  default;
Boolean _z3o1qtas =  default;
Int32 _b5p6od9s =  default;
Int32 _d0i9k0it =  default;
Int32 _9dbezfkf =  default;
Int32 _y7ic5zdn =  default;
Int32 _aapu20il =  default;
Int32 _yi1ytrp0 =  default;
Int32 _xdn7zni0 =  default;
Int32 _0u2khlex =  default;
Int32 _itfnbz60 =  default;
Int32 _g0pbjgnr =  default;
Int32 _y5lj3ish =  default;
Int32 _qitusv03 =  default;
Int32 _oxr7eu3o =  default;
Int32 _jh83swyf =  default;
Int32 _nxemfc2w =  default;
Int32 _1y4h88en =  default;
Int32 _der3corq =  default;
Int32 _z6j4b86r =  default;
Int32 _cu6tg1rd =  default;
Int32 _72jumgs0 =  default;
Int32 _uqke0mt1 =  default;
Int32 _znpjgsef =  default;
Int32 _18fz0zf9 =  default;
Int32 _j7zu8nju =  default;
Int32 _dgsb0lfe =  default;
Int32 _jc37k0o9 =  default;
Int32 _naa7acm7 =  default;
Int32 _gyxj38v2 =  default;
Int32 _1l9k9q9k =  default;
Int32 _1mfobugm =  default;
Int32 _xn5je7et =  default;
Single _av7j8yda =  default;
Single _82tpdhyl =  default;
Single _p1iqarg6 =  default;
Single _3aphllyg =  default;
Single _ntcc3h1m =  default;
Single _o7mrehk9 =  default;
Single _o8rgmibn =  default;
Single _sg2xsi4l =  default;
Single _ndrkejw5 =  default;
Single _nmnmq6ye =  default;
Single _h75qnr7l =  default;
Single _1m44vtuk =  default;
Single _bogm0gwy =  default;
Single _8tmd0ner =  default;
Single _j0ty6ytl =  default;
Single _2qcyvkhx =  default;
Single _gq29adzg =  default;
Single _wkzzmhv7 =  default;
Single _z2bllur5 =  default;
string fLanavab = default;
#endregion  variable declarations
_w6igfk2h = _w6igfk2h.Convert(1);
_wrqmi80z = _wrqmi80z.Convert(1);

	{
		//* 
		//*  -- LAPACK computational routine (version 3.7.1) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     June 2016 
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
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input parameters. 
		//* 
		
		_189gzykk = _w8y2rzgy(_w6igfk2h ,"V" );
		_w72qtubt = _w8y2rzgy(_wrqmi80z ,"A" );
		_hessdr7t = _w8y2rzgy(_wrqmi80z ,"V" );
		_neo0j9hw = _w8y2rzgy(_wrqmi80z ,"I" );//* 
		
		_lhlgm7z5 = ((_6fnxzlyp == (int)-1) | (_29mhiasb == (int)-1));
		_z3o1qtas = (_ghjpby1f == (int)-1);// 
		//*     SSTEMR needs WORK of size 6*N, IWORK of size 3*N. 
		//*     In addition, SLARRE needs WORK of size 6*N, IWORK of size 5*N. 
		//*     Furthermore, SLARRV needs WORK of size 12*N, IWORK of size 7*N. 
		
		if (_189gzykk)
		{
			
			_jc37k0o9 = ((int)18 * _dxpq0xkr);
			_dgsb0lfe = ((int)10 * _dxpq0xkr);
		}
		else
		{
			//*        need less workspace if only the eigenvalues are wanted 
			
			_jc37k0o9 = ((int)12 * _dxpq0xkr);
			_dgsb0lfe = ((int)8 * _dxpq0xkr);
		}
		// 
		
		_wkzzmhv7 = _d0547bi2;
		_z2bllur5 = _d0547bi2;
		_aapu20il = (int)0;
		_y5lj3ish = (int)0;
		_naa7acm7 = (int)0;// 
		
		if (_hessdr7t)
		{
			//*        We do not reference VL, VU in the cases RANGE = 'I','A' 
			//*        The interval (WL, WU] contains all the wanted eigenvalues. 
			//*        It is either given by the user or computed in SLARRE. 
			
			_wkzzmhv7 = _ppzorcqs;
			_z2bllur5 = _qqhwr930;
		}
		else
		if (_neo0j9hw)
		{
			//*        We do not reference IL, IU in the cases RANGE = 'V','A' 
			
			_aapu20il = _ic6kua09;
			_y5lj3ish = _j4l29b9c;
		}
		//* 
		
		_gro5yvfo = (int)0;
		if (!((_189gzykk | _w8y2rzgy(_w6igfk2h ,"N" ))))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (!(((_w72qtubt | _hessdr7t) | _neo0j9hw)))
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if ((_hessdr7t & (_dxpq0xkr > (int)0)) & (_z2bllur5 <= _wkzzmhv7))
		{
			
			_gro5yvfo = (int)-7;
		}
		else
		if (_neo0j9hw & ((_aapu20il < (int)1) | (_aapu20il > _dxpq0xkr)))
		{
			
			_gro5yvfo = (int)-8;
		}
		else
		if (_neo0j9hw & ((_y5lj3ish < _aapu20il) | (_y5lj3ish > _dxpq0xkr)))
		{
			
			_gro5yvfo = (int)-9;
		}
		else
		if ((_5l1tna8s < (int)1) | (_189gzykk & (_5l1tna8s < _dxpq0xkr)))
		{
			
			_gro5yvfo = (int)-13;
		}
		else
		if ((_6fnxzlyp < _jc37k0o9) & (!(_lhlgm7z5)))
		{
			
			_gro5yvfo = (int)-17;
		}
		else
		if ((_29mhiasb < _dgsb0lfe) & (!(_lhlgm7z5)))
		{
			
			_gro5yvfo = (int)-19;
		}
		//* 
		//*     Get machine constants. 
		//* 
		
		_h75qnr7l = _d5tu038y("Safe minimum" );
		_p1iqarg6 = _d5tu038y("Precision" );
		_bogm0gwy = (_h75qnr7l / _p1iqarg6);
		_av7j8yda = (_kxg5drh2 / _bogm0gwy);
		_sg2xsi4l = ILNumerics.F2NET.Intrinsics.SQRT(_bogm0gwy );
		_o8rgmibn = ILNumerics.F2NET.Intrinsics.MIN(ILNumerics.F2NET.Intrinsics.SQRT(_av7j8yda ) ,_kxg5drh2 / ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.SQRT(_h75qnr7l ) ) );//* 
		
		if (_gro5yvfo == (int)0)
		{
			
			*(_apig8meb+((int)1 - 1)) = REAL(_jc37k0o9);
			*(_4b6rt45i+((int)1 - 1)) = _dgsb0lfe;//* 
			
			if (_189gzykk & _w72qtubt)
			{
				
				_gyxj38v2 = _dxpq0xkr;
			}
			else
			if (_189gzykk & _hessdr7t)
			{
				
				_2xue1sxz("T" ,ref _dxpq0xkr ,ref _ppzorcqs ,ref _qqhwr930 ,_plfm7z8g ,_864fslqq ,ref _h75qnr7l ,ref _gyxj38v2 ,ref _72jumgs0 ,ref _uqke0mt1 ,ref _gro5yvfo );
			}
			else
			if (_189gzykk & _neo0j9hw)
			{
				
				_gyxj38v2 = ((_y5lj3ish - _aapu20il) + (int)1);
			}
			else
			{
				//*           WANTZ .EQ. FALSE. 
				
				_gyxj38v2 = (int)0;
			}
			
			if (_z3o1qtas & (_gro5yvfo == (int)0))
			{
				
				*(_7e60fcso+((int)1 - 1) + ((int)1 - 1) * 1 * (_5l1tna8s)) = REAL(_gyxj38v2);
			}
			else
			if ((_ghjpby1f < _gyxj38v2) & (!(_z3o1qtas)))
			{
				
				_gro5yvfo = (int)-14;
			}
			
		}
		// 
		
		if (_gro5yvfo != (int)0)
		{
			//* 
			
			_ut9qalzx("SSTEMR" ,ref Unsafe.AsRef(-(_gro5yvfo)) );//* 
			
			return;
		}
		else
		if (_lhlgm7z5 | _z3o1qtas)
		{
			
			return;
		}
		//* 
		//*     Handle N = 0, 1, and 2 cases immediately 
		//* 
		
		_ev4xhht5 = (int)0;
		if (_dxpq0xkr == (int)0)
		return;//* 
		
		if (_dxpq0xkr == (int)1)
		{
			
			if (_w72qtubt | _neo0j9hw)
			{
				
				_ev4xhht5 = (int)1;
				*(_z1ioc3c8+((int)1 - 1)) = *(_plfm7z8g+((int)1 - 1));
			}
			else
			{
				
				if ((_wkzzmhv7 < *(_plfm7z8g+((int)1 - 1))) & (_z2bllur5 >= *(_plfm7z8g+((int)1 - 1))))
				{
					
					_ev4xhht5 = (int)1;
					*(_z1ioc3c8+((int)1 - 1)) = *(_plfm7z8g+((int)1 - 1));
				}
				
			}
			
			if (_189gzykk & (!(_z3o1qtas)))
			{
				
				*(_7e60fcso+((int)1 - 1) + ((int)1 - 1) * 1 * (_5l1tna8s)) = _kxg5drh2;
				*(_nr4g8ae2+((int)1 - 1)) = (int)1;
				*(_nr4g8ae2+((int)2 - 1)) = (int)1;
			}
			
			return;
		}
		//* 
		
		if (_dxpq0xkr == (int)2)
		{
			
			if (!(_189gzykk))
			{
				
				_7vaufgro(ref Unsafe.AsRef(*(_plfm7z8g+((int)1 - 1))) ,ref Unsafe.AsRef(*(_864fslqq+((int)1 - 1))) ,ref Unsafe.AsRef(*(_plfm7z8g+((int)2 - 1))) ,ref _ntcc3h1m ,ref _o7mrehk9 );
			}
			else
			if (_189gzykk & (!(_z3o1qtas)))
			{
				
				_uh9ssfxw(ref Unsafe.AsRef(*(_plfm7z8g+((int)1 - 1))) ,ref Unsafe.AsRef(*(_864fslqq+((int)1 - 1))) ,ref Unsafe.AsRef(*(_plfm7z8g+((int)2 - 1))) ,ref _ntcc3h1m ,ref _o7mrehk9 ,ref _82tpdhyl ,ref _8tmd0ner );
			}
			
			if ((_w72qtubt | ((_hessdr7t & (_o7mrehk9 > _wkzzmhv7)) & (_o7mrehk9 <= _z2bllur5))) | (_neo0j9hw & (_aapu20il == (int)1)))
			{
				
				_ev4xhht5 = (_ev4xhht5 + (int)1);
				*(_z1ioc3c8+(_ev4xhht5 - 1)) = _o7mrehk9;
				if (_189gzykk & (!(_z3o1qtas)))
				{
					
					*(_7e60fcso+((int)1 - 1) + (_ev4xhht5 - 1) * 1 * (_5l1tna8s)) = (-(_8tmd0ner));
					*(_7e60fcso+((int)2 - 1) + (_ev4xhht5 - 1) * 1 * (_5l1tna8s)) = _82tpdhyl;//*              Note: At most one of SN and CS can be zero. 
					
					if (_8tmd0ner != _d0547bi2)
					{
						
						if (_82tpdhyl != _d0547bi2)
						{
							
							*(_nr4g8ae2+(((int)2 * _ev4xhht5) - (int)1 - 1)) = (int)1;
							*(_nr4g8ae2+((int)2 * _ev4xhht5 - 1)) = (int)2;
						}
						else
						{
							
							*(_nr4g8ae2+(((int)2 * _ev4xhht5) - (int)1 - 1)) = (int)1;
							*(_nr4g8ae2+((int)2 * _ev4xhht5 - 1)) = (int)1;
						}
						
					}
					else
					{
						
						*(_nr4g8ae2+(((int)2 * _ev4xhht5) - (int)1 - 1)) = (int)2;
						*(_nr4g8ae2+((int)2 * _ev4xhht5 - 1)) = (int)2;
					}
					
				}
				
			}
			
			if ((_w72qtubt | ((_hessdr7t & (_ntcc3h1m > _wkzzmhv7)) & (_ntcc3h1m <= _z2bllur5))) | (_neo0j9hw & (_y5lj3ish == (int)2)))
			{
				
				_ev4xhht5 = (_ev4xhht5 + (int)1);
				*(_z1ioc3c8+(_ev4xhht5 - 1)) = _ntcc3h1m;
				if (_189gzykk & (!(_z3o1qtas)))
				{
					
					*(_7e60fcso+((int)1 - 1) + (_ev4xhht5 - 1) * 1 * (_5l1tna8s)) = _82tpdhyl;
					*(_7e60fcso+((int)2 - 1) + (_ev4xhht5 - 1) * 1 * (_5l1tna8s)) = _8tmd0ner;//*              Note: At most one of SN and CS can be zero. 
					
					if (_8tmd0ner != _d0547bi2)
					{
						
						if (_82tpdhyl != _d0547bi2)
						{
							
							*(_nr4g8ae2+(((int)2 * _ev4xhht5) - (int)1 - 1)) = (int)1;
							*(_nr4g8ae2+((int)2 * _ev4xhht5 - 1)) = (int)2;
						}
						else
						{
							
							*(_nr4g8ae2+(((int)2 * _ev4xhht5) - (int)1 - 1)) = (int)1;
							*(_nr4g8ae2+((int)2 * _ev4xhht5 - 1)) = (int)1;
						}
						
					}
					else
					{
						
						*(_nr4g8ae2+(((int)2 * _ev4xhht5) - (int)1 - 1)) = (int)2;
						*(_nr4g8ae2+((int)2 * _ev4xhht5 - 1)) = (int)2;
					}
					
				}
				
			}
			
		}
		else
		{
			// 
			//*     Continue with general N 
			// 
			
			_z6j4b86r = (int)1;
			_1y4h88en = (((int)2 * _dxpq0xkr) + (int)1);
			_der3corq = (((int)3 * _dxpq0xkr) + (int)1);
			_jh83swyf = (((int)4 * _dxpq0xkr) + (int)1);
			_nxemfc2w = (((int)5 * _dxpq0xkr) + (int)1);
			_cu6tg1rd = (((int)6 * _dxpq0xkr) + (int)1);//* 
			
			_g0pbjgnr = (int)1;
			_yi1ytrp0 = (_dxpq0xkr + (int)1);
			_xdn7zni0 = (((int)2 * _dxpq0xkr) + (int)1);
			_0u2khlex = (((int)3 * _dxpq0xkr) + (int)1);//* 
			//*        Scale matrix to allowable range, if necessary. 
			//*        The allowable range is related to the PIVMIN parameter; see the 
			//*        comments in SLARRD.  The preference for scaling small values 
			//*        up is heuristic; we expect users' matrices not to be close to the 
			//*        RMAX threshold. 
			//* 
			
			_1m44vtuk = _kxg5drh2;
			_gq29adzg = _5kajfnos("M" ,ref _dxpq0xkr ,_plfm7z8g ,_864fslqq );
			if ((_gq29adzg > _d0547bi2) & (_gq29adzg < _sg2xsi4l))
			{
				
				_1m44vtuk = (_sg2xsi4l / _gq29adzg);
			}
			else
			if (_gq29adzg > _o8rgmibn)
			{
				
				_1m44vtuk = (_o8rgmibn / _gq29adzg);
			}
			
			if (_1m44vtuk != _kxg5drh2)
			{
				
				_ct5qqrv7(ref _dxpq0xkr ,ref _1m44vtuk ,_plfm7z8g ,ref Unsafe.AsRef((int)1) );
				_ct5qqrv7(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref _1m44vtuk ,_864fslqq ,ref Unsafe.AsRef((int)1) );
				_gq29adzg = (_gq29adzg * _1m44vtuk);
				if (_hessdr7t)
				{
					//*              If eigenvalues in interval have to be found, 
					//*              scale (WL, WU] accordingly 
					
					_wkzzmhv7 = (_wkzzmhv7 * _1m44vtuk);
					_z2bllur5 = (_z2bllur5 * _1m44vtuk);
				}
				
			}
			//* 
			//*        Compute the desired eigenvalues of the tridiagonal after splitting 
			//*        into smaller subblocks if the corresponding off-diagonal elements 
			//*        are small 
			//*        THRESH is the splitting parameter for SLARRE 
			//*        A negative THRESH forces the old splitting criterion based on the 
			//*        size of the off-diagonal. A positive THRESH switches to splitting 
			//*        which preserves relative accuracy. 
			//* 
			
			if (_4h81eu1p)
			{
				//*           Test whether the matrix warrants the more expensive relative approach. 
				
				_jzgwtfxi(ref _dxpq0xkr ,_plfm7z8g ,_864fslqq ,ref _itfnbz60 );
			}
			else
			{
				//*           The user does not care about relative accurately eigenvalues 
				
				_itfnbz60 = (int)-1;
			}
			//*        Set the splitting criterion 
			
			if (_itfnbz60 == (int)0)
			{
				
				_j0ty6ytl = _p1iqarg6;
			}
			else
			{
				
				_j0ty6ytl = (-(_p1iqarg6));//*           relative accuracy is desired but T does not guarantee it 
				
				_4h81eu1p = false;
			}
			//* 
			
			if (_4h81eu1p)
			{
				//*           Copy original diagonal, needed to guarantee relative accuracy 
				
				_wcs7ne88(ref _dxpq0xkr ,_plfm7z8g ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_jh83swyf - 1)),ref Unsafe.AsRef((int)1) );
			}
			//*        Store the squares of the offdiagonal values of T 
			
			{
				System.Int32 __81fgg2dlsvn3197 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step3197 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3197;
				for (__81fgg2count3197 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn3197 + __81fgg2step3197) / __81fgg2step3197)), _znpjgsef = __81fgg2dlsvn3197; __81fgg2count3197 != 0; __81fgg2count3197--, _znpjgsef += (__81fgg2step3197)) {

				{
					
					*(_apig8meb+((_nxemfc2w + _znpjgsef) - (int)1 - 1)) = __POW2(*(_864fslqq+(_znpjgsef - 1)));
Mark5:;
					// continue
				}
								}			}// 
			//*        Set the tolerance parameters for bisection 
			
			if (!(_189gzykk))
			{
				//*           SLARRE computes the eigenvalues to full precision. 
				
				_ndrkejw5 = (_ax5ijvbx * _p1iqarg6);
				_nmnmq6ye = (_ax5ijvbx * _p1iqarg6);
			}
			else
			{
				//*           SLARRE computes the eigenvalues to less than full precision. 
				//*           SLARRV will refine the eigenvalue approximations, and we can 
				//*           need less accurate initial bisection in SLARRE. 
				//*           Note: these settings do only affect the subset case and SLARRE 
				
				_ndrkejw5 = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.SQRT(_p1iqarg6 ) * 0.05f ,_ax5ijvbx * _p1iqarg6 );
				_nmnmq6ye = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.SQRT(_p1iqarg6 ) * 0.005f ,_ax5ijvbx * _p1iqarg6 );
			}
			
			_mlilid03(_wrqmi80z ,ref _dxpq0xkr ,ref _wkzzmhv7 ,ref _z2bllur5 ,ref _aapu20il ,ref _y5lj3ish ,_plfm7z8g ,_864fslqq ,(_apig8meb+(_nxemfc2w - 1)),ref _ndrkejw5 ,ref _nmnmq6ye ,ref _j0ty6ytl ,ref _naa7acm7 ,(_4b6rt45i+(_g0pbjgnr - 1)),ref _ev4xhht5 ,_z1ioc3c8 ,(_apig8meb+(_1y4h88en - 1)),(_apig8meb+(_der3corq - 1)),(_4b6rt45i+(_yi1ytrp0 - 1)),(_4b6rt45i+(_xdn7zni0 - 1)),(_apig8meb+(_z6j4b86r - 1)),ref _3aphllyg ,(_apig8meb+(_cu6tg1rd - 1)),(_4b6rt45i+(_0u2khlex - 1)),ref _itfnbz60 );
			if (_itfnbz60 != (int)0)
			{
				
				_gro5yvfo = ((int)10 + ILNumerics.F2NET.Intrinsics.ABS(_itfnbz60 ));
				return;
			}
			//*        Note that if RANGE .NE. 'V', SLARRE computes bounds on the desired 
			//*        part of the spectrum. All desired eigenvalues are contained in 
			//*        (WL,WU] 
			// 
			// 
			
			if (_189gzykk)
			{
				//* 
				//*           Compute the desired eigenvectors corresponding to the computed 
				//*           eigenvalues 
				//* 
				
				_1mqx40id(ref _dxpq0xkr ,ref _wkzzmhv7 ,ref _z2bllur5 ,_plfm7z8g ,_864fslqq ,ref _3aphllyg ,(_4b6rt45i+(_g0pbjgnr - 1)),ref _ev4xhht5 ,ref Unsafe.AsRef((int)1) ,ref _ev4xhht5 ,ref Unsafe.AsRef(_i7h9sv6c) ,ref _ndrkejw5 ,ref _nmnmq6ye ,_z1ioc3c8 ,(_apig8meb+(_1y4h88en - 1)),(_apig8meb+(_der3corq - 1)),(_4b6rt45i+(_yi1ytrp0 - 1)),(_4b6rt45i+(_xdn7zni0 - 1)),(_apig8meb+(_z6j4b86r - 1)),_7e60fcso ,ref _5l1tna8s ,_nr4g8ae2 ,(_apig8meb+(_cu6tg1rd - 1)),(_4b6rt45i+(_0u2khlex - 1)),ref _itfnbz60 );
				if (_itfnbz60 != (int)0)
				{
					
					_gro5yvfo = ((int)20 + ILNumerics.F2NET.Intrinsics.ABS(_itfnbz60 ));
					return;
				}
				
			}
			else
			{
				//*           SLARRE computes eigenvalues of the (shifted) root representation 
				//*           SLARRV returns the eigenvalues of the unshifted matrix. 
				//*           However, if the eigenvectors are not desired by the user, we need 
				//*           to apply the corresponding shifts from SLARRE to obtain the 
				//*           eigenvalues of the original matrix. 
				
				{
					System.Int32 __81fgg2dlsvn3198 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3198 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3198;
					for (__81fgg2count3198 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3198 + __81fgg2step3198) / __81fgg2step3198)), _znpjgsef = __81fgg2dlsvn3198; __81fgg2count3198 != 0; __81fgg2count3198--, _znpjgsef += (__81fgg2step3198)) {

					{
						
						_72jumgs0 = *(_4b6rt45i+((_yi1ytrp0 + _znpjgsef) - (int)1 - 1));
						*(_z1ioc3c8+(_znpjgsef - 1)) = (*(_z1ioc3c8+(_znpjgsef - 1)) + *(_864fslqq+(*(_4b6rt45i+((_g0pbjgnr + _72jumgs0) - (int)1 - 1)) - 1)));
Mark20:;
						// continue
					}
										}				}
			}
			//* 
			// 
			
			if (_4h81eu1p)
			{
				//*           Refine computed eigenvalues so that they are relatively accurate 
				//*           with respect to the original matrix T. 
				
				_d0i9k0it = (int)1;
				_1mfobugm = (int)1;
				{
					System.Int32 __81fgg2dlsvn3199 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3199 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3199;
					for (__81fgg2count3199 = System.Math.Max(0, (System.Int32)(((System.Int32)(*(_4b6rt45i+((_yi1ytrp0 + _ev4xhht5) - (int)1 - 1))) - __81fgg2dlsvn3199 + __81fgg2step3199) / __81fgg2step3199)), _18fz0zf9 = __81fgg2dlsvn3199; __81fgg2count3199 != 0; __81fgg2count3199--, _18fz0zf9 += (__81fgg2step3199)) {

					{
						
						_9dbezfkf = *(_4b6rt45i+((_g0pbjgnr + _18fz0zf9) - (int)1 - 1));
						_oxr7eu3o = ((_9dbezfkf - _d0i9k0it) + (int)1);
						_xn5je7et = (_1mfobugm - (int)1);//*              check if any eigenvalues have to be refined in this block 
						
Mark36:;
						// continue
						if (_xn5je7et < _ev4xhht5)
						{
							
							if (*(_4b6rt45i+(_yi1ytrp0 + _xn5je7et - 1)) == _18fz0zf9)
							{
								
								_xn5je7et = (_xn5je7et + (int)1);goto Mark36;
							}
							
						}
						
						if (_xn5je7et < _1mfobugm)
						{
							
							_d0i9k0it = (_9dbezfkf + (int)1);goto Mark39;
						}
						// 
						
						_1l9k9q9k = (*(_4b6rt45i+((_xdn7zni0 + _1mfobugm) - (int)1 - 1)) - (int)1);
						_y7ic5zdn = *(_4b6rt45i+((_xdn7zni0 + _1mfobugm) - (int)1 - 1));
						_qitusv03 = *(_4b6rt45i+((_xdn7zni0 + _xn5je7et) - (int)1 - 1));
						_nmnmq6ye = (_ax5ijvbx * _p1iqarg6);
						_nk8t0jsw(ref _oxr7eu3o ,(_apig8meb+((_jh83swyf + _d0i9k0it) - (int)1 - 1)),(_apig8meb+((_nxemfc2w + _d0i9k0it) - (int)1 - 1)),ref _y7ic5zdn ,ref _qitusv03 ,ref _nmnmq6ye ,ref _1l9k9q9k ,(_z1ioc3c8+(_1mfobugm - 1)),(_apig8meb+((_1y4h88en + _1mfobugm) - (int)1 - 1)),(_apig8meb+(_cu6tg1rd - 1)),(_4b6rt45i+(_0u2khlex - 1)),ref _3aphllyg ,ref _gq29adzg ,ref _itfnbz60 );
						_d0i9k0it = (_9dbezfkf + (int)1);
						_1mfobugm = (_xn5je7et + (int)1);
Mark39:;
						// continue
					}
										}				}
			}
			//* 
			//*        If matrix was scaled, then rescale eigenvalues appropriately. 
			//* 
			
			if (_1m44vtuk != _kxg5drh2)
			{
				
				_ct5qqrv7(ref _ev4xhht5 ,ref Unsafe.AsRef(_kxg5drh2 / _1m44vtuk) ,_z1ioc3c8 ,ref Unsafe.AsRef((int)1) );
			}
			
		}
		//* 
		//*     If eigenvalues are not in increasing order, then sort them, 
		//*     possibly along with eigenvectors. 
		//* 
		
		if ((_naa7acm7 > (int)1) | (_dxpq0xkr == (int)2))
		{
			
			if (!(_189gzykk))
			{
				
				_ezdvkw03("I" ,ref _ev4xhht5 ,_z1ioc3c8 ,ref _itfnbz60 );
				if (_itfnbz60 != (int)0)
				{
					
					_gro5yvfo = (int)3;
					return;
				}
				
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn3200 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3200 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3200;
					for (__81fgg2count3200 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5 - (int)1) - __81fgg2dlsvn3200 + __81fgg2step3200) / __81fgg2step3200)), _znpjgsef = __81fgg2dlsvn3200; __81fgg2count3200 != 0; __81fgg2count3200--, _znpjgsef += (__81fgg2step3200)) {

					{
						
						_b5p6od9s = (int)0;
						_2qcyvkhx = *(_z1ioc3c8+(_znpjgsef - 1));
						{
							System.Int32 __81fgg2dlsvn3201 = (System.Int32)((_znpjgsef + (int)1));
							const System.Int32 __81fgg2step3201 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3201;
							for (__81fgg2count3201 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3201 + __81fgg2step3201) / __81fgg2step3201)), _j7zu8nju = __81fgg2dlsvn3201; __81fgg2count3201 != 0; __81fgg2count3201--, _j7zu8nju += (__81fgg2step3201)) {

							{
								
								if (*(_z1ioc3c8+(_j7zu8nju - 1)) < _2qcyvkhx)
								{
									
									_b5p6od9s = _j7zu8nju;
									_2qcyvkhx = *(_z1ioc3c8+(_j7zu8nju - 1));
								}
								
Mark50:;
								// continue
							}
														}						}
						if (_b5p6od9s != (int)0)
						{
							
							*(_z1ioc3c8+(_b5p6od9s - 1)) = *(_z1ioc3c8+(_znpjgsef - 1));
							*(_z1ioc3c8+(_znpjgsef - 1)) = _2qcyvkhx;
							if (_189gzykk)
							{
								
								_ahhuglvd(ref _dxpq0xkr ,(_7e60fcso+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_5l1tna8s)),ref Unsafe.AsRef((int)1) ,(_7e60fcso+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_5l1tna8s)),ref Unsafe.AsRef((int)1) );
								_72jumgs0 = *(_nr4g8ae2+(((int)2 * _b5p6od9s) - (int)1 - 1));
								*(_nr4g8ae2+(((int)2 * _b5p6od9s) - (int)1 - 1)) = *(_nr4g8ae2+(((int)2 * _znpjgsef) - (int)1 - 1));
								*(_nr4g8ae2+(((int)2 * _znpjgsef) - (int)1 - 1)) = _72jumgs0;
								_72jumgs0 = *(_nr4g8ae2+((int)2 * _b5p6od9s - 1));
								*(_nr4g8ae2+((int)2 * _b5p6od9s - 1)) = *(_nr4g8ae2+((int)2 * _znpjgsef - 1));
								*(_nr4g8ae2+((int)2 * _znpjgsef - 1)) = _72jumgs0;
							}
							
						}
						
Mark60:;
						// continue
					}
										}				}
			}
			
		}
		//* 
		//* 
		
		*(_apig8meb+((int)1 - 1)) = REAL(_jc37k0o9);
		*(_4b6rt45i+((int)1 - 1)) = _dgsb0lfe;
		return;//* 
		//*     End of SSTEMR 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
