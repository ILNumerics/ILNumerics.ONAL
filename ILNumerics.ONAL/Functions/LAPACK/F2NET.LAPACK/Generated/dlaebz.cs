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
//*> \brief \b DLAEBZ computes the number of eigenvalues of a real symmetric tridiagonal matrix which are less than or equal to a given value, and performs other tasks required by the routine sstebz. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLAEBZ + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlaebz.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlaebz.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlaebz.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLAEBZ( IJOB, NITMAX, N, MMAX, MINP, NBMIN, ABSTOL, 
//*                          RELTOL, PIVMIN, D, E, E2, NVAL, AB, C, MOUT, 
//*                          NAB, WORK, IWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            IJOB, INFO, MINP, MMAX, MOUT, N, NBMIN, NITMAX 
//*       DOUBLE PRECISION   ABSTOL, PIVMIN, RELTOL 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IWORK( * ), NAB( MMAX, * ), NVAL( * ) 
//*       DOUBLE PRECISION   AB( MMAX, * ), C( * ), D( * ), E( * ), E2( * ), 
//*      $                   WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLAEBZ contains the iteration loops which compute and use the 
//*> function N(w), which is the count of eigenvalues of a symmetric 
//*> tridiagonal matrix T less than or equal to its argument  w.  It 
//*> performs a choice of two types of loops: 
//*> 
//*> IJOB=1, followed by 
//*> IJOB=2: It takes as input a list of intervals and returns a list of 
//*>         sufficiently small intervals whose union contains the same 
//*>         eigenvalues as the union of the original intervals. 
//*>         The input intervals are (AB(j,1),AB(j,2)], j=1,...,MINP. 
//*>         The output interval (AB(j,1),AB(j,2)] will contain 
//*>         eigenvalues NAB(j,1)+1,...,NAB(j,2), where 1 <= j <= MOUT. 
//*> 
//*> IJOB=3: It performs a binary search in each input interval 
//*>         (AB(j,1),AB(j,2)] for a point  w(j)  such that 
//*>         N(w(j))=NVAL(j), and uses  C(j)  as the starting point of 
//*>         the search.  If such a w(j) is found, then on output 
//*>         AB(j,1)=AB(j,2)=w.  If no such w(j) is found, then on output 
//*>         (AB(j,1),AB(j,2)] will be a small interval containing the 
//*>         point where N(w) jumps through NVAL(j), unless that point 
//*>         lies outside the initial interval. 
//*> 
//*> Note that the intervals are in all cases half-open intervals, 
//*> i.e., of the form  (a,b] , which includes  b  but not  a . 
//*> 
//*> To avoid underflow, the matrix should be scaled so that its largest 
//*> element is no greater than  overflow**(1/2) * underflow**(1/4) 
//*> in absolute value.  To assure the most accurate computation 
//*> of small eigenvalues, the matrix should be scaled to be 
//*> not much smaller than that, either. 
//*> 
//*> See W. Kahan "Accurate Eigenvalues of a Symmetric Tridiagonal 
//*> Matrix", Report CS41, Computer Science Dept., Stanford 
//*> University, July 21, 1966 
//*> 
//*> Note: the arguments are, in general, *not* checked for unreasonable 
//*> values. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] IJOB 
//*> \verbatim 
//*>          IJOB is INTEGER 
//*>          Specifies what is to be done: 
//*>          = 1:  Compute NAB for the initial intervals. 
//*>          = 2:  Perform bisection iteration to find eigenvalues of T. 
//*>          = 3:  Perform bisection iteration to invert N(w), i.e., 
//*>                to find a point which has a specified number of 
//*>                eigenvalues of T to its left. 
//*>          Other values will cause DLAEBZ to return with INFO=-1. 
//*> \endverbatim 
//*> 
//*> \param[in] NITMAX 
//*> \verbatim 
//*>          NITMAX is INTEGER 
//*>          The maximum number of "levels" of bisection to be 
//*>          performed, i.e., an interval of width W will not be made 
//*>          smaller than 2^(-NITMAX) * W.  If not all intervals 
//*>          have converged after NITMAX iterations, then INFO is set 
//*>          to the number of non-converged intervals. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The dimension n of the tridiagonal matrix T.  It must be at 
//*>          least 1. 
//*> \endverbatim 
//*> 
//*> \param[in] MMAX 
//*> \verbatim 
//*>          MMAX is INTEGER 
//*>          The maximum number of intervals.  If more than MMAX intervals 
//*>          are generated, then DLAEBZ will quit with INFO=MMAX+1. 
//*> \endverbatim 
//*> 
//*> \param[in] MINP 
//*> \verbatim 
//*>          MINP is INTEGER 
//*>          The initial number of intervals.  It may not be greater than 
//*>          MMAX. 
//*> \endverbatim 
//*> 
//*> \param[in] NBMIN 
//*> \verbatim 
//*>          NBMIN is INTEGER 
//*>          The smallest number of intervals that should be processed 
//*>          using a vector loop.  If zero, then only the scalar loop 
//*>          will be used. 
//*> \endverbatim 
//*> 
//*> \param[in] ABSTOL 
//*> \verbatim 
//*>          ABSTOL is DOUBLE PRECISION 
//*>          The minimum (absolute) width of an interval.  When an 
//*>          interval is narrower than ABSTOL, or than RELTOL times the 
//*>          larger (in magnitude) endpoint, then it is considered to be 
//*>          sufficiently small, i.e., converged.  This must be at least 
//*>          zero. 
//*> \endverbatim 
//*> 
//*> \param[in] RELTOL 
//*> \verbatim 
//*>          RELTOL is DOUBLE PRECISION 
//*>          The minimum relative width of an interval.  When an interval 
//*>          is narrower than ABSTOL, or than RELTOL times the larger (in 
//*>          magnitude) endpoint, then it is considered to be 
//*>          sufficiently small, i.e., converged.  Note: this should 
//*>          always be at least radix*machine epsilon. 
//*> \endverbatim 
//*> 
//*> \param[in] PIVMIN 
//*> \verbatim 
//*>          PIVMIN is DOUBLE PRECISION 
//*>          The minimum absolute value of a "pivot" in the Sturm 
//*>          sequence loop. 
//*>          This must be at least  max |e(j)**2|*safe_min  and at 
//*>          least safe_min, where safe_min is at least 
//*>          the smallest number that can divide one without overflow. 
//*> \endverbatim 
//*> 
//*> \param[in] D 
//*> \verbatim 
//*>          D is DOUBLE PRECISION array, dimension (N) 
//*>          The diagonal elements of the tridiagonal matrix T. 
//*> \endverbatim 
//*> 
//*> \param[in] E 
//*> \verbatim 
//*>          E is DOUBLE PRECISION array, dimension (N) 
//*>          The offdiagonal elements of the tridiagonal matrix T in 
//*>          positions 1 through N-1.  E(N) is arbitrary. 
//*> \endverbatim 
//*> 
//*> \param[in] E2 
//*> \verbatim 
//*>          E2 is DOUBLE PRECISION array, dimension (N) 
//*>          The squares of the offdiagonal elements of the tridiagonal 
//*>          matrix T.  E2(N) is ignored. 
//*> \endverbatim 
//*> 
//*> \param[in,out] NVAL 
//*> \verbatim 
//*>          NVAL is INTEGER array, dimension (MINP) 
//*>          If IJOB=1 or 2, not referenced. 
//*>          If IJOB=3, the desired values of N(w).  The elements of NVAL 
//*>          will be reordered to correspond with the intervals in AB. 
//*>          Thus, NVAL(j) on output will not, in general be the same as 
//*>          NVAL(j) on input, but it will correspond with the interval 
//*>          (AB(j,1),AB(j,2)] on output. 
//*> \endverbatim 
//*> 
//*> \param[in,out] AB 
//*> \verbatim 
//*>          AB is DOUBLE PRECISION array, dimension (MMAX,2) 
//*>          The endpoints of the intervals.  AB(j,1) is  a(j), the left 
//*>          endpoint of the j-th interval, and AB(j,2) is b(j), the 
//*>          right endpoint of the j-th interval.  The input intervals 
//*>          will, in general, be modified, split, and reordered by the 
//*>          calculation. 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is DOUBLE PRECISION array, dimension (MMAX) 
//*>          If IJOB=1, ignored. 
//*>          If IJOB=2, workspace. 
//*>          If IJOB=3, then on input C(j) should be initialized to the 
//*>          first search point in the binary search. 
//*> \endverbatim 
//*> 
//*> \param[out] MOUT 
//*> \verbatim 
//*>          MOUT is INTEGER 
//*>          If IJOB=1, the number of eigenvalues in the intervals. 
//*>          If IJOB=2 or 3, the number of intervals output. 
//*>          If IJOB=3, MOUT will equal MINP. 
//*> \endverbatim 
//*> 
//*> \param[in,out] NAB 
//*> \verbatim 
//*>          NAB is INTEGER array, dimension (MMAX,2) 
//*>          If IJOB=1, then on output NAB(i,j) will be set to N(AB(i,j)). 
//*>          If IJOB=2, then on input, NAB(i,j) should be set.  It must 
//*>             satisfy the condition: 
//*>             N(AB(i,1)) <= NAB(i,1) <= NAB(i,2) <= N(AB(i,2)), 
//*>             which means that in interval i only eigenvalues 
//*>             NAB(i,1)+1,...,NAB(i,2) will be considered.  Usually, 
//*>             NAB(i,j)=N(AB(i,j)), from a previous call to DLAEBZ with 
//*>             IJOB=1. 
//*>             On output, NAB(i,j) will contain 
//*>             max(na(k),min(nb(k),N(AB(i,j)))), where k is the index of 
//*>             the input interval that the output interval 
//*>             (AB(j,1),AB(j,2)] came from, and na(k) and nb(k) are the 
//*>             the input values of NAB(k,1) and NAB(k,2). 
//*>          If IJOB=3, then on output, NAB(i,j) contains N(AB(i,j)), 
//*>             unless N(w) > NVAL(i) for all search points  w , in which 
//*>             case NAB(i,1) will not be modified, i.e., the output 
//*>             value will be the same as the input value (modulo 
//*>             reorderings -- see NVAL and AB), or unless N(w) < NVAL(i) 
//*>             for all search points  w , in which case NAB(i,2) will 
//*>             not be modified.  Normally, NAB should be set to some 
//*>             distinctive value(s) before DLAEBZ is called. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is DOUBLE PRECISION array, dimension (MMAX) 
//*>          Workspace. 
//*> \endverbatim 
//*> 
//*> \param[out] IWORK 
//*> \verbatim 
//*>          IWORK is INTEGER array, dimension (MMAX) 
//*>          Workspace. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:       All intervals converged. 
//*>          = 1--MMAX: The last INFO intervals did not converge. 
//*>          = MMAX+1:  More than MMAX intervals were generated. 
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
//*> \ingroup OTHERauxiliary 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>      This routine is intended to be called only by other LAPACK 
//*>  routines, thus the interface is less user-friendly.  It is intended 
//*>  for two purposes: 
//*> 
//*>  (a) finding eigenvalues.  In this case, DLAEBZ should have one or 
//*>      more initial intervals set up in AB, and DLAEBZ should be called 
//*>      with IJOB=1.  This sets up NAB, and also counts the eigenvalues. 
//*>      Intervals with no eigenvalues would usually be thrown out at 
//*>      this point.  Also, if not all the eigenvalues in an interval i 
//*>      are desired, NAB(i,1) can be increased or NAB(i,2) decreased. 
//*>      For example, set NAB(i,1)=NAB(i,2)-1 to get the largest 
//*>      eigenvalue.  DLAEBZ is then called with IJOB=2 and MMAX 
//*>      no smaller than the value of MOUT returned by the call with 
//*>      IJOB=1.  After this (IJOB=2) call, eigenvalues NAB(i,1)+1 
//*>      through NAB(i,2) are approximately AB(i,1) (or AB(i,2)) to the 
//*>      tolerance specified by ABSTOL and RELTOL. 
//*> 
//*>  (b) finding an interval (a',b'] containing eigenvalues w(f),...,w(l). 
//*>      In this case, start with a Gershgorin interval  (a,b).  Set up 
//*>      AB to contain 2 search intervals, both initially (a,b).  One 
//*>      NVAL element should contain  f-1  and the other should contain  l 
//*>      , while C should contain a and b, resp.  NAB(i,1) should be -1 
//*>      and NAB(i,2) should be N+1, to flag an error if the desired 
//*>      interval does not lie in (a,b).  DLAEBZ is then called with 
//*>      IJOB=3.  On exit, if w(f-1) < w(f), then one of the intervals -- 
//*>      j -- will have AB(j,1)=AB(j,2) and NAB(j,1)=NAB(j,2)=f-1, while 
//*>      if, to the specified tolerance, w(f-k)=...=w(f+r), k > 0 and r 
//*>      >= 0, then the interval will have  N(AB(j,1))=NAB(j,1)=f-k and 
//*>      N(AB(j,2))=NAB(j,2)=f+r.  The cases w(l) < w(l+1) and 
//*>      w(l-r)=...=w(l+k) are handled similarly. 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _8367l6gs(ref Int32 _5tze4r5c, ref Int32 _34uehjvc, ref Int32 _dxpq0xkr, ref Int32 _ur3aq1q4, ref Int32 _0mmsxtti, ref Int32 _o80jnixx, ref Double _rltspcxj, ref Double _brq3wv6n, ref Double _3aphllyg, Double* _plfm7z8g, Double* _864fslqq, Double* _0maek8rz, Int32* _i4n4cfbd, Double* _9tcc7f14, Double* _3crf0qn3, ref Int32 _c5orenoj, Int32* _kmcz21p8, Double* _apig8meb, Int32* _4b6rt45i, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Double _5m0mjfxm =  2d;
Double _gbf4169i =  1d / _5m0mjfxm;
Int32 _qq2166xp =  default;
Int32 _uqke0mt1 =  default;
Int32 _znpjgsef =  default;
Int32 _4wj3gzf5 =  default;
Int32 _pm01hoim =  default;
Int32 _c2zk3fjj =  default;
Int32 _7138527q =  default;
Int32 _oojv57m0 =  default;
Int32 _upl3k0xa =  default;
Int32 _lwwrdvcv =  default;
Double _c0o9kuh7 =  default;
Double _ww3bdyup =  default;
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
		//*  ===================================================================== 
		//* 
		//*     .. Parameters .. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Check for Errors 
		//* 
		
		_gro5yvfo = (int)0;
		if ((_5tze4r5c < (int)1) | (_5tze4r5c > (int)3))
		{
			
			_gro5yvfo = (int)-1;
			return;
		}
		//* 
		//*     Initialize NAB 
		//* 
		
		if (_5tze4r5c == (int)1)
		{
			//* 
			//*        Compute the number of eigenvalues in the initial intervals. 
			//* 
			
			_c5orenoj = (int)0;
			{
				System.Int32 __81fgg2dlsvn2849 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2849 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2849;
				for (__81fgg2count2849 = System.Math.Max(0, (System.Int32)(((System.Int32)(_0mmsxtti) - __81fgg2dlsvn2849 + __81fgg2step2849) / __81fgg2step2849)), _4wj3gzf5 = __81fgg2dlsvn2849; __81fgg2count2849 != 0; __81fgg2count2849--, _4wj3gzf5 += (__81fgg2step2849)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn2850 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2850 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2850;
						for (__81fgg2count2850 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)2) - __81fgg2dlsvn2850 + __81fgg2step2850) / __81fgg2step2850)), _c2zk3fjj = __81fgg2dlsvn2850; __81fgg2count2850 != 0; __81fgg2count2850--, _c2zk3fjj += (__81fgg2step2850)) {

						{
							
							_c0o9kuh7 = (*(_plfm7z8g+((int)1 - 1)) - *(_9tcc7f14+(_4wj3gzf5 - 1) + (_c2zk3fjj - 1) * 1 * (_ur3aq1q4)));
							if (ILNumerics.F2NET.Intrinsics.ABS(_c0o9kuh7 ) < _3aphllyg)
							_c0o9kuh7 = (-(_3aphllyg));
							*(_kmcz21p8+(_4wj3gzf5 - 1) + (_c2zk3fjj - 1) * 1 * (_ur3aq1q4)) = (int)0;
							if (_c0o9kuh7 <= _d0547bi2)
							*(_kmcz21p8+(_4wj3gzf5 - 1) + (_c2zk3fjj - 1) * 1 * (_ur3aq1q4)) = (int)1;//* 
							
							{
								System.Int32 __81fgg2dlsvn2851 = (System.Int32)((int)2);
								const System.Int32 __81fgg2step2851 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2851;
								for (__81fgg2count2851 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2851 + __81fgg2step2851) / __81fgg2step2851)), _znpjgsef = __81fgg2dlsvn2851; __81fgg2count2851 != 0; __81fgg2count2851--, _znpjgsef += (__81fgg2step2851)) {

								{
									
									_c0o9kuh7 = ((*(_plfm7z8g+(_znpjgsef - 1)) - (*(_0maek8rz+(_znpjgsef - (int)1 - 1)) / _c0o9kuh7)) - *(_9tcc7f14+(_4wj3gzf5 - 1) + (_c2zk3fjj - 1) * 1 * (_ur3aq1q4)));
									if (ILNumerics.F2NET.Intrinsics.ABS(_c0o9kuh7 ) < _3aphllyg)
									_c0o9kuh7 = (-(_3aphllyg));
									if (_c0o9kuh7 <= _d0547bi2)
									*(_kmcz21p8+(_4wj3gzf5 - 1) + (_c2zk3fjj - 1) * 1 * (_ur3aq1q4)) = (*(_kmcz21p8+(_4wj3gzf5 - 1) + (_c2zk3fjj - 1) * 1 * (_ur3aq1q4)) + (int)1);
Mark10:;
									// continue
								}
																}							}
Mark20:;
							// continue
						}
												}					}
					_c5orenoj = ((_c5orenoj + *(_kmcz21p8+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4))) - *(_kmcz21p8+(_4wj3gzf5 - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4)));
Mark30:;
					// continue
				}
								}			}
			return;
		}
		//* 
		//*     Initialize for loop 
		//* 
		//*     KF and KL have the following meaning: 
		//*        Intervals 1,...,KF-1 have converged. 
		//*        Intervals KF,...,KL  still need to be refined. 
		//* 
		
		_7138527q = (int)1;
		_upl3k0xa = _0mmsxtti;//* 
		//*     If IJOB=2, initialize C. 
		//*     If IJOB=3, use the user-supplied starting point. 
		//* 
		
		if (_5tze4r5c == (int)2)
		{
			
			{
				System.Int32 __81fgg2dlsvn2852 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2852 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2852;
				for (__81fgg2count2852 = System.Math.Max(0, (System.Int32)(((System.Int32)(_0mmsxtti) - __81fgg2dlsvn2852 + __81fgg2step2852) / __81fgg2step2852)), _4wj3gzf5 = __81fgg2dlsvn2852; __81fgg2count2852 != 0; __81fgg2count2852--, _4wj3gzf5 += (__81fgg2step2852)) {

				{
					
					*(_3crf0qn3+(_4wj3gzf5 - 1)) = (_gbf4169i * (*(_9tcc7f14+(_4wj3gzf5 - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4)) + *(_9tcc7f14+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4))));
Mark40:;
					// continue
				}
								}			}
		}
		//* 
		//*     Iteration loop 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn2853 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2853 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2853;
			for (__81fgg2count2853 = System.Math.Max(0, (System.Int32)(((System.Int32)(_34uehjvc) - __81fgg2dlsvn2853 + __81fgg2step2853) / __81fgg2step2853)), _pm01hoim = __81fgg2dlsvn2853; __81fgg2count2853 != 0; __81fgg2count2853--, _pm01hoim += (__81fgg2step2853)) {

			{
				//* 
				//*        Loop over intervals 
				//* 
				
				if ((((_upl3k0xa - _7138527q) + (int)1) >= _o80jnixx) & (_o80jnixx > (int)0))
				{
					//* 
					//*           Begin of Parallel Version of the loop 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn2854 = (System.Int32)(_7138527q);
						const System.Int32 __81fgg2step2854 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2854;
						for (__81fgg2count2854 = System.Math.Max(0, (System.Int32)(((System.Int32)(_upl3k0xa) - __81fgg2dlsvn2854 + __81fgg2step2854) / __81fgg2step2854)), _4wj3gzf5 = __81fgg2dlsvn2854; __81fgg2count2854 != 0; __81fgg2count2854--, _4wj3gzf5 += (__81fgg2step2854)) {

						{
							//* 
							//*              Compute N(c), the number of eigenvalues less than c 
							//* 
							
							*(_apig8meb+(_4wj3gzf5 - 1)) = (*(_plfm7z8g+((int)1 - 1)) - *(_3crf0qn3+(_4wj3gzf5 - 1)));
							*(_4b6rt45i+(_4wj3gzf5 - 1)) = (int)0;
							if (*(_apig8meb+(_4wj3gzf5 - 1)) <= _3aphllyg)
							{
								
								*(_4b6rt45i+(_4wj3gzf5 - 1)) = (int)1;
								*(_apig8meb+(_4wj3gzf5 - 1)) = ILNumerics.F2NET.Intrinsics.MIN(*(_apig8meb+(_4wj3gzf5 - 1)) ,-(_3aphllyg) );
							}
							//* 
							
							{
								System.Int32 __81fgg2dlsvn2855 = (System.Int32)((int)2);
								const System.Int32 __81fgg2step2855 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2855;
								for (__81fgg2count2855 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2855 + __81fgg2step2855) / __81fgg2step2855)), _znpjgsef = __81fgg2dlsvn2855; __81fgg2count2855 != 0; __81fgg2count2855--, _znpjgsef += (__81fgg2step2855)) {

								{
									
									*(_apig8meb+(_4wj3gzf5 - 1)) = ((*(_plfm7z8g+(_znpjgsef - 1)) - (*(_0maek8rz+(_znpjgsef - (int)1 - 1)) / *(_apig8meb+(_4wj3gzf5 - 1)))) - *(_3crf0qn3+(_4wj3gzf5 - 1)));
									if (*(_apig8meb+(_4wj3gzf5 - 1)) <= _3aphllyg)
									{
										
										*(_4b6rt45i+(_4wj3gzf5 - 1)) = (*(_4b6rt45i+(_4wj3gzf5 - 1)) + (int)1);
										*(_apig8meb+(_4wj3gzf5 - 1)) = ILNumerics.F2NET.Intrinsics.MIN(*(_apig8meb+(_4wj3gzf5 - 1)) ,-(_3aphllyg) );
									}
									
Mark50:;
									// continue
								}
																}							}
Mark60:;
							// continue
						}
												}					}//* 
					
					if (_5tze4r5c <= (int)2)
					{
						//* 
						//*              IJOB=2: Choose all intervals containing eigenvalues. 
						//* 
						
						_lwwrdvcv = _upl3k0xa;
						{
							System.Int32 __81fgg2dlsvn2856 = (System.Int32)(_7138527q);
							const System.Int32 __81fgg2step2856 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2856;
							for (__81fgg2count2856 = System.Math.Max(0, (System.Int32)(((System.Int32)(_upl3k0xa) - __81fgg2dlsvn2856 + __81fgg2step2856) / __81fgg2step2856)), _4wj3gzf5 = __81fgg2dlsvn2856; __81fgg2count2856 != 0; __81fgg2count2856--, _4wj3gzf5 += (__81fgg2step2856)) {

							{
								//* 
								//*                 Insure that N(w) is monotone 
								//* 
								
								*(_4b6rt45i+(_4wj3gzf5 - 1)) = ILNumerics.F2NET.Intrinsics.MIN(*(_kmcz21p8+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4)) ,ILNumerics.F2NET.Intrinsics.MAX(*(_kmcz21p8+(_4wj3gzf5 - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4)) ,*(_4b6rt45i+(_4wj3gzf5 - 1)) ) );//* 
								//*                 Update the Queue -- add intervals if both halves 
								//*                 contain eigenvalues. 
								//* 
								
								if (*(_4b6rt45i+(_4wj3gzf5 - 1)) == *(_kmcz21p8+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4)))
								{
									//* 
									//*                    No eigenvalue in the upper interval: 
									//*                    just use the lower interval. 
									//* 
									
									*(_9tcc7f14+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4)) = *(_3crf0qn3+(_4wj3gzf5 - 1));//* 
									
								}
								else
								if (*(_4b6rt45i+(_4wj3gzf5 - 1)) == *(_kmcz21p8+(_4wj3gzf5 - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4)))
								{
									//* 
									//*                    No eigenvalue in the lower interval: 
									//*                    just use the upper interval. 
									//* 
									
									*(_9tcc7f14+(_4wj3gzf5 - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4)) = *(_3crf0qn3+(_4wj3gzf5 - 1));
								}
								else
								{
									
									_lwwrdvcv = (_lwwrdvcv + (int)1);
									if (_lwwrdvcv <= _ur3aq1q4)
									{
										//* 
										//*                       Eigenvalue in both intervals -- add upper to 
										//*                       queue. 
										//* 
										
										*(_9tcc7f14+(_lwwrdvcv - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4)) = *(_9tcc7f14+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4));
										*(_kmcz21p8+(_lwwrdvcv - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4)) = *(_kmcz21p8+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4));
										*(_9tcc7f14+(_lwwrdvcv - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4)) = *(_3crf0qn3+(_4wj3gzf5 - 1));
										*(_kmcz21p8+(_lwwrdvcv - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4)) = *(_4b6rt45i+(_4wj3gzf5 - 1));
										*(_9tcc7f14+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4)) = *(_3crf0qn3+(_4wj3gzf5 - 1));
										*(_kmcz21p8+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4)) = *(_4b6rt45i+(_4wj3gzf5 - 1));
									}
									else
									{
										
										_gro5yvfo = (_ur3aq1q4 + (int)1);
									}
									
								}
								
Mark70:;
								// continue
							}
														}						}
						if (_gro5yvfo != (int)0)
						return;
						_upl3k0xa = _lwwrdvcv;
					}
					else
					{
						//* 
						//*              IJOB=3: Binary search.  Keep only the interval containing 
						//*                      w   s.t. N(w) = NVAL 
						//* 
						
						{
							System.Int32 __81fgg2dlsvn2857 = (System.Int32)(_7138527q);
							const System.Int32 __81fgg2step2857 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2857;
							for (__81fgg2count2857 = System.Math.Max(0, (System.Int32)(((System.Int32)(_upl3k0xa) - __81fgg2dlsvn2857 + __81fgg2step2857) / __81fgg2step2857)), _4wj3gzf5 = __81fgg2dlsvn2857; __81fgg2count2857 != 0; __81fgg2count2857--, _4wj3gzf5 += (__81fgg2step2857)) {

							{
								
								if (*(_4b6rt45i+(_4wj3gzf5 - 1)) <= *(_i4n4cfbd+(_4wj3gzf5 - 1)))
								{
									
									*(_9tcc7f14+(_4wj3gzf5 - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4)) = *(_3crf0qn3+(_4wj3gzf5 - 1));
									*(_kmcz21p8+(_4wj3gzf5 - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4)) = *(_4b6rt45i+(_4wj3gzf5 - 1));
								}
								
								if (*(_4b6rt45i+(_4wj3gzf5 - 1)) >= *(_i4n4cfbd+(_4wj3gzf5 - 1)))
								{
									
									*(_9tcc7f14+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4)) = *(_3crf0qn3+(_4wj3gzf5 - 1));
									*(_kmcz21p8+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4)) = *(_4b6rt45i+(_4wj3gzf5 - 1));
								}
								
Mark80:;
								// continue
							}
														}						}
					}
					//* 
					
				}
				else
				{
					//* 
					//*           End of Parallel Version of the loop 
					//* 
					//*           Begin of Serial Version of the loop 
					//* 
					
					_lwwrdvcv = _upl3k0xa;
					{
						System.Int32 __81fgg2dlsvn2858 = (System.Int32)(_7138527q);
						const System.Int32 __81fgg2step2858 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2858;
						for (__81fgg2count2858 = System.Math.Max(0, (System.Int32)(((System.Int32)(_upl3k0xa) - __81fgg2dlsvn2858 + __81fgg2step2858) / __81fgg2step2858)), _4wj3gzf5 = __81fgg2dlsvn2858; __81fgg2count2858 != 0; __81fgg2count2858--, _4wj3gzf5 += (__81fgg2step2858)) {

						{
							//* 
							//*              Compute N(w), the number of eigenvalues less than w 
							//* 
							
							_c0o9kuh7 = *(_3crf0qn3+(_4wj3gzf5 - 1));
							_ww3bdyup = (*(_plfm7z8g+((int)1 - 1)) - _c0o9kuh7);
							_qq2166xp = (int)0;
							if (_ww3bdyup <= _3aphllyg)
							{
								
								_qq2166xp = (int)1;
								_ww3bdyup = ILNumerics.F2NET.Intrinsics.MIN(_ww3bdyup ,-(_3aphllyg) );
							}
							//* 
							
							{
								System.Int32 __81fgg2dlsvn2859 = (System.Int32)((int)2);
								const System.Int32 __81fgg2step2859 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2859;
								for (__81fgg2count2859 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2859 + __81fgg2step2859) / __81fgg2step2859)), _znpjgsef = __81fgg2dlsvn2859; __81fgg2count2859 != 0; __81fgg2count2859--, _znpjgsef += (__81fgg2step2859)) {

								{
									
									_ww3bdyup = ((*(_plfm7z8g+(_znpjgsef - 1)) - (*(_0maek8rz+(_znpjgsef - (int)1 - 1)) / _ww3bdyup)) - _c0o9kuh7);
									if (_ww3bdyup <= _3aphllyg)
									{
										
										_qq2166xp = (_qq2166xp + (int)1);
										_ww3bdyup = ILNumerics.F2NET.Intrinsics.MIN(_ww3bdyup ,-(_3aphllyg) );
									}
									
Mark90:;
									// continue
								}
																}							}//* 
							
							if (_5tze4r5c <= (int)2)
							{
								//* 
								//*                 IJOB=2: Choose all intervals containing eigenvalues. 
								//* 
								//*                 Insure that N(w) is monotone 
								//* 
								
								_qq2166xp = ILNumerics.F2NET.Intrinsics.MIN(*(_kmcz21p8+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4)) ,ILNumerics.F2NET.Intrinsics.MAX(*(_kmcz21p8+(_4wj3gzf5 - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4)) ,_qq2166xp ) );//* 
								//*                 Update the Queue -- add intervals if both halves 
								//*                 contain eigenvalues. 
								//* 
								
								if (_qq2166xp == *(_kmcz21p8+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4)))
								{
									//* 
									//*                    No eigenvalue in the upper interval: 
									//*                    just use the lower interval. 
									//* 
									
									*(_9tcc7f14+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4)) = _c0o9kuh7;//* 
									
								}
								else
								if (_qq2166xp == *(_kmcz21p8+(_4wj3gzf5 - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4)))
								{
									//* 
									//*                    No eigenvalue in the lower interval: 
									//*                    just use the upper interval. 
									//* 
									
									*(_9tcc7f14+(_4wj3gzf5 - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4)) = _c0o9kuh7;
								}
								else
								if (_lwwrdvcv < _ur3aq1q4)
								{
									//* 
									//*                    Eigenvalue in both intervals -- add upper to queue. 
									//* 
									
									_lwwrdvcv = (_lwwrdvcv + (int)1);
									*(_9tcc7f14+(_lwwrdvcv - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4)) = *(_9tcc7f14+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4));
									*(_kmcz21p8+(_lwwrdvcv - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4)) = *(_kmcz21p8+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4));
									*(_9tcc7f14+(_lwwrdvcv - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4)) = _c0o9kuh7;
									*(_kmcz21p8+(_lwwrdvcv - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4)) = _qq2166xp;
									*(_9tcc7f14+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4)) = _c0o9kuh7;
									*(_kmcz21p8+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4)) = _qq2166xp;
								}
								else
								{
									
									_gro5yvfo = (_ur3aq1q4 + (int)1);
									return;
								}
								
							}
							else
							{
								//* 
								//*                 IJOB=3: Binary search.  Keep only the interval 
								//*                         containing  w  s.t. N(w) = NVAL 
								//* 
								
								if (_qq2166xp <= *(_i4n4cfbd+(_4wj3gzf5 - 1)))
								{
									
									*(_9tcc7f14+(_4wj3gzf5 - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4)) = _c0o9kuh7;
									*(_kmcz21p8+(_4wj3gzf5 - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4)) = _qq2166xp;
								}
								
								if (_qq2166xp >= *(_i4n4cfbd+(_4wj3gzf5 - 1)))
								{
									
									*(_9tcc7f14+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4)) = _c0o9kuh7;
									*(_kmcz21p8+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4)) = _qq2166xp;
								}
								
							}
							
Mark100:;
							// continue
						}
												}					}
					_upl3k0xa = _lwwrdvcv;//* 
					
				}
				//* 
				//*        Check for convergence 
				//* 
				
				_oojv57m0 = _7138527q;
				{
					System.Int32 __81fgg2dlsvn2860 = (System.Int32)(_7138527q);
					const System.Int32 __81fgg2step2860 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2860;
					for (__81fgg2count2860 = System.Math.Max(0, (System.Int32)(((System.Int32)(_upl3k0xa) - __81fgg2dlsvn2860 + __81fgg2step2860) / __81fgg2step2860)), _4wj3gzf5 = __81fgg2dlsvn2860; __81fgg2count2860 != 0; __81fgg2count2860--, _4wj3gzf5 += (__81fgg2step2860)) {

					{
						
						_c0o9kuh7 = ILNumerics.F2NET.Intrinsics.ABS(*(_9tcc7f14+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4)) - *(_9tcc7f14+(_4wj3gzf5 - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4)) );
						_ww3bdyup = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_9tcc7f14+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_9tcc7f14+(_4wj3gzf5 - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4)) ) );
						if ((_c0o9kuh7 < ILNumerics.F2NET.Intrinsics.MAX(_rltspcxj ,_3aphllyg ,_brq3wv6n * _ww3bdyup )) | (*(_kmcz21p8+(_4wj3gzf5 - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4)) >= *(_kmcz21p8+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4))))
						{
							//* 
							//*              Converged -- Swap with position KFNEW, 
							//*                           then increment KFNEW 
							//* 
							
							if (_4wj3gzf5 > _oojv57m0)
							{
								
								_c0o9kuh7 = *(_9tcc7f14+(_4wj3gzf5 - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4));
								_ww3bdyup = *(_9tcc7f14+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4));
								_qq2166xp = *(_kmcz21p8+(_4wj3gzf5 - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4));
								_uqke0mt1 = *(_kmcz21p8+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4));
								*(_9tcc7f14+(_4wj3gzf5 - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4)) = *(_9tcc7f14+(_oojv57m0 - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4));
								*(_9tcc7f14+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4)) = *(_9tcc7f14+(_oojv57m0 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4));
								*(_kmcz21p8+(_4wj3gzf5 - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4)) = *(_kmcz21p8+(_oojv57m0 - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4));
								*(_kmcz21p8+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4)) = *(_kmcz21p8+(_oojv57m0 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4));
								*(_9tcc7f14+(_oojv57m0 - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4)) = _c0o9kuh7;
								*(_9tcc7f14+(_oojv57m0 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4)) = _ww3bdyup;
								*(_kmcz21p8+(_oojv57m0 - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4)) = _qq2166xp;
								*(_kmcz21p8+(_oojv57m0 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4)) = _uqke0mt1;
								if (_5tze4r5c == (int)3)
								{
									
									_qq2166xp = *(_i4n4cfbd+(_4wj3gzf5 - 1));
									*(_i4n4cfbd+(_4wj3gzf5 - 1)) = *(_i4n4cfbd+(_oojv57m0 - 1));
									*(_i4n4cfbd+(_oojv57m0 - 1)) = _qq2166xp;
								}
								
							}
							
							_oojv57m0 = (_oojv57m0 + (int)1);
						}
						
Mark110:;
						// continue
					}
										}				}
				_7138527q = _oojv57m0;//* 
				//*        Choose Midpoints 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn2861 = (System.Int32)(_7138527q);
					const System.Int32 __81fgg2step2861 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2861;
					for (__81fgg2count2861 = System.Math.Max(0, (System.Int32)(((System.Int32)(_upl3k0xa) - __81fgg2dlsvn2861 + __81fgg2step2861) / __81fgg2step2861)), _4wj3gzf5 = __81fgg2dlsvn2861; __81fgg2count2861 != 0; __81fgg2count2861--, _4wj3gzf5 += (__81fgg2step2861)) {

					{
						
						*(_3crf0qn3+(_4wj3gzf5 - 1)) = (_gbf4169i * (*(_9tcc7f14+(_4wj3gzf5 - 1) + ((int)1 - 1) * 1 * (_ur3aq1q4)) + *(_9tcc7f14+(_4wj3gzf5 - 1) + ((int)2 - 1) * 1 * (_ur3aq1q4))));
Mark120:;
						// continue
					}
										}				}//* 
				//*        If no more intervals to refine, quit. 
				//* 
				
				if (_7138527q > _upl3k0xa)goto Mark140;
Mark130:;
				// continue
			}
						}		}//* 
		//*     Converged 
		//* 
		
Mark140:;
		// continue
		_gro5yvfo = ILNumerics.F2NET.Intrinsics.MAX((_upl3k0xa + (int)1) - _7138527q ,(int)0 );
		_c5orenoj = _upl3k0xa;//* 
		
		return;//* 
		//*     End of DLAEBZ 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
