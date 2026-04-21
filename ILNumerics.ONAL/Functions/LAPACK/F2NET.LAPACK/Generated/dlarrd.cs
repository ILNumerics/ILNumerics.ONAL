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
//*> \brief \b DLARRD computes the eigenvalues of a symmetric tridiagonal matrix to suitable accuracy. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLARRD + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlarrd.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlarrd.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlarrd.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLARRD( RANGE, ORDER, N, VL, VU, IL, IU, GERS, 
//*                           RELTOL, D, E, E2, PIVMIN, NSPLIT, ISPLIT, 
//*                           M, W, WERR, WL, WU, IBLOCK, INDEXW, 
//*                           WORK, IWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          ORDER, RANGE 
//*       INTEGER            IL, INFO, IU, M, N, NSPLIT 
//*       DOUBLE PRECISION    PIVMIN, RELTOL, VL, VU, WL, WU 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IBLOCK( * ), INDEXW( * ), 
//*      $                   ISPLIT( * ), IWORK( * ) 
//*       DOUBLE PRECISION   D( * ), E( * ), E2( * ), 
//*      $                   GERS( * ), W( * ), WERR( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLARRD computes the eigenvalues of a symmetric tridiagonal 
//*> matrix T to suitable accuracy. This is an auxiliary code to be 
//*> called from DSTEMR. 
//*> The user may ask for all eigenvalues, all eigenvalues 
//*> in the half-open interval (VL, VU], or the IL-th through IU-th 
//*> eigenvalues. 
//*> 
//*> To avoid overflow, the matrix must be scaled so that its 
//*> largest element is no greater than overflow**(1/2) * underflow**(1/4) in absolute value, and for greatest 
//*> accuracy, it should not be much smaller than that. 
//*> 
//*> See W. Kahan "Accurate Eigenvalues of a Symmetric Tridiagonal 
//*> Matrix", Report CS41, Computer Science Dept., Stanford 
//*> University, July 21, 1966. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] RANGE 
//*> \verbatim 
//*>          RANGE is CHARACTER*1 
//*>          = 'A': ("All")   all eigenvalues will be found. 
//*>          = 'V': ("Value") all eigenvalues in the half-open interval 
//*>                           (VL, VU] will be found. 
//*>          = 'I': ("Index") the IL-th through IU-th eigenvalues (of the 
//*>                           entire matrix) will be found. 
//*> \endverbatim 
//*> 
//*> \param[in] ORDER 
//*> \verbatim 
//*>          ORDER is CHARACTER*1 
//*>          = 'B': ("By Block") the eigenvalues will be grouped by 
//*>                              split-off block (see IBLOCK, ISPLIT) and 
//*>                              ordered from smallest to largest within 
//*>                              the block. 
//*>          = 'E': ("Entire matrix") 
//*>                              the eigenvalues for the entire matrix 
//*>                              will be ordered from smallest to 
//*>                              largest. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the tridiagonal matrix T.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] VL 
//*> \verbatim 
//*>          VL is DOUBLE PRECISION 
//*>          If RANGE='V', the lower bound of the interval to 
//*>          be searched for eigenvalues.  Eigenvalues less than or equal 
//*>          to VL, or greater than VU, will not be returned.  VL < VU. 
//*>          Not referenced if RANGE = 'A' or 'I'. 
//*> \endverbatim 
//*> 
//*> \param[in] VU 
//*> \verbatim 
//*>          VU is DOUBLE PRECISION 
//*>          If RANGE='V', the upper bound of the interval to 
//*>          be searched for eigenvalues.  Eigenvalues less than or equal 
//*>          to VL, or greater than VU, will not be returned.  VL < VU. 
//*>          Not referenced if RANGE = 'A' or 'I'. 
//*> \endverbatim 
//*> 
//*> \param[in] IL 
//*> \verbatim 
//*>          IL is INTEGER 
//*>          If RANGE='I', the index of the 
//*>          smallest eigenvalue to be returned. 
//*>          1 <= IL <= IU <= N, if N > 0; IL = 1 and IU = 0 if N = 0. 
//*>          Not referenced if RANGE = 'A' or 'V'. 
//*> \endverbatim 
//*> 
//*> \param[in] IU 
//*> \verbatim 
//*>          IU is INTEGER 
//*>          If RANGE='I', the index of the 
//*>          largest eigenvalue to be returned. 
//*>          1 <= IL <= IU <= N, if N > 0; IL = 1 and IU = 0 if N = 0. 
//*>          Not referenced if RANGE = 'A' or 'V'. 
//*> \endverbatim 
//*> 
//*> \param[in] GERS 
//*> \verbatim 
//*>          GERS is DOUBLE PRECISION array, dimension (2*N) 
//*>          The N Gerschgorin intervals (the i-th Gerschgorin interval 
//*>          is (GERS(2*i-1), GERS(2*i)). 
//*> \endverbatim 
//*> 
//*> \param[in] RELTOL 
//*> \verbatim 
//*>          RELTOL is DOUBLE PRECISION 
//*>          The minimum relative width of an interval.  When an interval 
//*>          is narrower than RELTOL times the larger (in 
//*>          magnitude) endpoint, then it is considered to be 
//*>          sufficiently small, i.e., converged.  Note: this should 
//*>          always be at least radix*machine epsilon. 
//*> \endverbatim 
//*> 
//*> \param[in] D 
//*> \verbatim 
//*>          D is DOUBLE PRECISION array, dimension (N) 
//*>          The n diagonal elements of the tridiagonal matrix T. 
//*> \endverbatim 
//*> 
//*> \param[in] E 
//*> \verbatim 
//*>          E is DOUBLE PRECISION array, dimension (N-1) 
//*>          The (n-1) off-diagonal elements of the tridiagonal matrix T. 
//*> \endverbatim 
//*> 
//*> \param[in] E2 
//*> \verbatim 
//*>          E2 is DOUBLE PRECISION array, dimension (N-1) 
//*>          The (n-1) squared off-diagonal elements of the tridiagonal matrix T. 
//*> \endverbatim 
//*> 
//*> \param[in] PIVMIN 
//*> \verbatim 
//*>          PIVMIN is DOUBLE PRECISION 
//*>          The minimum pivot allowed in the Sturm sequence for T. 
//*> \endverbatim 
//*> 
//*> \param[in] NSPLIT 
//*> \verbatim 
//*>          NSPLIT is INTEGER 
//*>          The number of diagonal blocks in the matrix T. 
//*>          1 <= NSPLIT <= N. 
//*> \endverbatim 
//*> 
//*> \param[in] ISPLIT 
//*> \verbatim 
//*>          ISPLIT is INTEGER array, dimension (N) 
//*>          The splitting points, at which T breaks up into submatrices. 
//*>          The first submatrix consists of rows/columns 1 to ISPLIT(1), 
//*>          the second of rows/columns ISPLIT(1)+1 through ISPLIT(2), 
//*>          etc., and the NSPLIT-th consists of rows/columns 
//*>          ISPLIT(NSPLIT-1)+1 through ISPLIT(NSPLIT)=N. 
//*>          (Only the first NSPLIT elements will actually be used, but 
//*>          since the user cannot know a priori what value NSPLIT will 
//*>          have, N words must be reserved for ISPLIT.) 
//*> \endverbatim 
//*> 
//*> \param[out] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The actual number of eigenvalues found. 0 <= M <= N. 
//*>          (See also the description of INFO=2,3.) 
//*> \endverbatim 
//*> 
//*> \param[out] W 
//*> \verbatim 
//*>          W is DOUBLE PRECISION array, dimension (N) 
//*>          On exit, the first M elements of W will contain the 
//*>          eigenvalue approximations. DLARRD computes an interval 
//*>          I_j = (a_j, b_j] that includes eigenvalue j. The eigenvalue 
//*>          approximation is given as the interval midpoint 
//*>          W(j)= ( a_j + b_j)/2. The corresponding error is bounded by 
//*>          WERR(j) = abs( a_j - b_j)/2 
//*> \endverbatim 
//*> 
//*> \param[out] WERR 
//*> \verbatim 
//*>          WERR is DOUBLE PRECISION array, dimension (N) 
//*>          The error bound on the corresponding eigenvalue approximation 
//*>          in W. 
//*> \endverbatim 
//*> 
//*> \param[out] WL 
//*> \verbatim 
//*>          WL is DOUBLE PRECISION 
//*> \endverbatim 
//*> 
//*> \param[out] WU 
//*> \verbatim 
//*>          WU is DOUBLE PRECISION 
//*>          The interval (WL, WU] contains all the wanted eigenvalues. 
//*>          If RANGE='V', then WL=VL and WU=VU. 
//*>          If RANGE='A', then WL and WU are the global Gerschgorin bounds 
//*>                        on the spectrum. 
//*>          If RANGE='I', then WL and WU are computed by DLAEBZ from the 
//*>                        index range specified. 
//*> \endverbatim 
//*> 
//*> \param[out] IBLOCK 
//*> \verbatim 
//*>          IBLOCK is INTEGER array, dimension (N) 
//*>          At each row/column j where E(j) is zero or small, the 
//*>          matrix T is considered to split into a block diagonal 
//*>          matrix.  On exit, if INFO = 0, IBLOCK(i) specifies to which 
//*>          block (from 1 to the number of blocks) the eigenvalue W(i) 
//*>          belongs.  (DLARRD may use the remaining N-M elements as 
//*>          workspace.) 
//*> \endverbatim 
//*> 
//*> \param[out] INDEXW 
//*> \verbatim 
//*>          INDEXW is INTEGER array, dimension (N) 
//*>          The indices of the eigenvalues within each block (submatrix); 
//*>          for example, INDEXW(i)= j and IBLOCK(i)=k imply that the 
//*>          i-th eigenvalue W(i) is the j-th eigenvalue in block k. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is DOUBLE PRECISION array, dimension (4*N) 
//*> \endverbatim 
//*> 
//*> \param[out] IWORK 
//*> \verbatim 
//*>          IWORK is INTEGER array, dimension (3*N) 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value 
//*>          > 0:  some or all of the eigenvalues failed to converge or 
//*>                were not computed: 
//*>                =1 or 3: Bisection failed to converge for some 
//*>                        eigenvalues; these eigenvalues are flagged by a 
//*>                        negative block number.  The effect is that the 
//*>                        eigenvalues may not be as accurate as the 
//*>                        absolute and relative tolerances.  This is 
//*>                        generally caused by unexpectedly inaccurate 
//*>                        arithmetic. 
//*>                =2 or 3: RANGE='I' only: Not all of the eigenvalues 
//*>                        IL:IU were found. 
//*>                        Effect: M < IU+1-IL 
//*>                        Cause:  non-monotonic arithmetic, causing the 
//*>                                Sturm sequence to be non-monotonic. 
//*>                        Cure:   recalculate, using RANGE='A', and pick 
//*>                                out eigenvalues IL:IU.  In some cases, 
//*>                                increasing the PARAMETER "FUDGE" may 
//*>                                make things work. 
//*>                = 4:    RANGE='I', and the Gershgorin interval 
//*>                        initially used was too small.  No eigenvalues 
//*>                        were computed. 
//*>                        Probable cause: your machine has sloppy 
//*>                                        floating-point arithmetic. 
//*>                        Cure: Increase the PARAMETER "FUDGE", 
//*>                              recompile, and try again. 
//*> \endverbatim 
//* 
//*> \par Internal Parameters: 
//*  ========================= 
//*> 
//*> \verbatim 
//*>  FUDGE   DOUBLE PRECISION, default = 2 
//*>          A "fudge factor" to widen the Gershgorin intervals.  Ideally, 
//*>          a value of 1 should work, but on machines with sloppy 
//*>          arithmetic, this needs to be larger.  The default for 
//*>          publicly released versions should be large enough to handle 
//*>          the worst machine around.  Note that this has no effect 
//*>          on accuracy of the solution. 
//*> \endverbatim 
//*> 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>     W. Kahan, University of California, Berkeley, USA \n 
//*>     Beresford Parlett, University of California, Berkeley, USA \n 
//*>     Jim Demmel, University of California, Berkeley, USA \n 
//*>     Inderjit Dhillon, University of Texas, Austin, USA \n 
//*>     Osni Marques, LBNL/NERSC, USA \n 
//*>     Christof Voemel, University of California, Berkeley, USA \n 
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
//*> \ingroup OTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _havbzny4(FString _wrqmi80z, FString _loathnh7, ref Int32 _dxpq0xkr, ref Double _ppzorcqs, ref Double _qqhwr930, ref Int32 _ic6kua09, ref Int32 _j4l29b9c, Double* _yisb1mtu, ref Double _brq3wv6n, Double* _plfm7z8g, Double* _864fslqq, Double* _0maek8rz, ref Double _3aphllyg, ref Int32 _naa7acm7, Int32* _nn033w1s, ref Int32 _ev4xhht5, Double* _z1ioc3c8, Double* _sgbqptwj, ref Double _wkzzmhv7, ref Double _z2bllur5, Int32* _5zga5mk9, Int32* _z5l332hf, Double* _apig8meb, Int32* _4b6rt45i, ref Int32 _gro5yvfo)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)4 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Double _5m0mjfxm =  2d;
Double _gbf4169i =  _kxg5drh2 / _5m0mjfxm;
Double _7fnb0l4r =  _5m0mjfxm;
Int32 _ytsyu76w =  (int)1;
Int32 _55ursdfo =  (int)2;
Int32 _atfcdrg6 =  (int)3;
Boolean _uwgmxffk =  default;
Boolean _9vwzg4sj =  default;
Int32 _b5p6od9s =  default;
Int32 _vyr1z1si =  default;
Int32 _d0i9k0it =  default;
Int32 _4zavrvex =  default;
Int32 _5pjgpo42 =  default;
Int32 _smxeww0r =  default;
Int32 _9dbezfkf =  default;
Int32 _itfnbz60 =  default;
Int32 _2vvers93 =  default;
Int32 _oxr7eu3o =  default;
Int32 _kk45tx7l =  default;
Int32 _5q9hyd4z =  default;
Int32 _o42699gn =  default;
Int32 _7u74ue5o =  default;
Int32 _qq2166xp =  default;
Int32 _uqke0mt1 =  default;
Int32 _11qhqs00 =  default;
Int32 _kxxzluvq =  default;
Int32 _znpjgsef =  default;
Int32 _18fz0zf9 =  default;
Int32 _16jas2ek =  default;
Int32 _22sk16or =  default;
Int32 _gw26iq4i =  default;
Int32 _f7059815 =  default;
Int32 _2qketc28 =  default;
Int32 _2u7bqeo4 =  default;
Double _jmec0f4x =  default;
Double _p1iqarg6 =  default;
Double _lr8ennxn =  default;
Double _jf74kq7y =  default;
Double _eki85d4y =  default;
Double _c0o9kuh7 =  default;
Double _ww3bdyup =  default;
Double _6t1khtu3 =  default;
Double _ddd01k2r =  default;
Double _oq8p752h =  default;
Double _okixw5up =  default;
Double _ehalmmz0 =  default;
Int32* _4wo395cy =  (Int32*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Int32) * ((int)1);
string fLanavab = default;
#endregion  variable declarations
_wrqmi80z = _wrqmi80z.Convert(1);
_loathnh7 = _loathnh7.Convert(1);

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
		//*  ===================================================================== 
		//* 
		//*     .. Parameters .. 
		//*     .. 
		//*     .. Local Scalars .. 
		// 
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
		
		if (_dxpq0xkr <= (int)0)
		{
			
			return;
		}
		//* 
		//*     Decode RANGE 
		//* 
		
		if (_w8y2rzgy(_wrqmi80z ,"A" ))
		{
			
			_o42699gn = _ytsyu76w;
		}
		else
		if (_w8y2rzgy(_wrqmi80z ,"V" ))
		{
			
			_o42699gn = _55ursdfo;
		}
		else
		if (_w8y2rzgy(_wrqmi80z ,"I" ))
		{
			
			_o42699gn = _atfcdrg6;
		}
		else
		{
			
			_o42699gn = (int)0;
		}
		//* 
		//*     Check for Errors 
		//* 
		
		if (_o42699gn <= (int)0)
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (!((_w8y2rzgy(_loathnh7 ,"B" ) | _w8y2rzgy(_loathnh7 ,"E" ))))
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if (_o42699gn == _55ursdfo)
		{
			
			if (_ppzorcqs >= _qqhwr930)
			_gro5yvfo = (int)-5;
		}
		else
		if ((_o42699gn == _atfcdrg6) & ((_ic6kua09 < (int)1) | (_ic6kua09 > ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))))
		{
			
			_gro5yvfo = (int)-6;
		}
		else
		if ((_o42699gn == _atfcdrg6) & ((_j4l29b9c < ILNumerics.F2NET.Intrinsics.MIN(_dxpq0xkr ,_ic6kua09 )) | (_j4l29b9c > _dxpq0xkr)))
		{
			
			_gro5yvfo = (int)-7;
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			return;
		}
		// 
		//*     Initialize error flags 
		
		_gro5yvfo = (int)0;
		_uwgmxffk = false;
		_9vwzg4sj = false;// 
		//*     Quick return if possible 
		
		_ev4xhht5 = (int)0;
		if (_dxpq0xkr == (int)0)
		return;// 
		//*     Simplification: 
		
		if (((_o42699gn == _atfcdrg6) & (_ic6kua09 == (int)1)) & (_j4l29b9c == _dxpq0xkr))
		_o42699gn = (int)1;// 
		//*     Get machine constants 
		
		_p1iqarg6 = _f43eg0w0("P" );
		_ddd01k2r = _f43eg0w0("U" );// 
		// 
		//*     Special Case when N=1 
		//*     Treat case of 1x1 matrix for quick return 
		
		if (_dxpq0xkr == (int)1)
		{
			
			if (((_o42699gn == _ytsyu76w) | (((_o42699gn == _55ursdfo) & (*(_plfm7z8g+((int)1 - 1)) > _ppzorcqs)) & (*(_plfm7z8g+((int)1 - 1)) <= _qqhwr930))) | (((_o42699gn == _atfcdrg6) & (_ic6kua09 == (int)1)) & (_j4l29b9c == (int)1)))
			{
				
				_ev4xhht5 = (int)1;
				*(_z1ioc3c8+((int)1 - 1)) = *(_plfm7z8g+((int)1 - 1));//*           The computation error of the eigenvalue is zero 
				
				*(_sgbqptwj+((int)1 - 1)) = _d0547bi2;
				*(_5zga5mk9+((int)1 - 1)) = (int)1;
				*(_z5l332hf+((int)1 - 1)) = (int)1;
			}
			
			return;
		}
		// 
		//*     NB is the minimum vector length for vector bisection, or 0 
		//*     if only scalar is to be done. 
		
		_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"DSTEBZ" ," " ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) );
		if (_f7059815 <= (int)1)
		_f7059815 = (int)0;// 
		//*     Find global spectral radius 
		
		_lr8ennxn = *(_plfm7z8g+((int)1 - 1));
		_jf74kq7y = *(_plfm7z8g+((int)1 - 1));
		{
			System.Int32 __81fgg2dlsvn3024 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step3024 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3024;
			for (__81fgg2count3024 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3024 + __81fgg2step3024) / __81fgg2step3024)), _b5p6od9s = __81fgg2dlsvn3024; __81fgg2count3024 != 0; __81fgg2count3024--, _b5p6od9s += (__81fgg2step3024)) {

			{
				
				_lr8ennxn = ILNumerics.F2NET.Intrinsics.MIN(_lr8ennxn ,*(_yisb1mtu+(((int)2 * _b5p6od9s) - (int)1 - 1)) );
				_jf74kq7y = ILNumerics.F2NET.Intrinsics.MAX(_jf74kq7y ,*(_yisb1mtu+((int)2 * _b5p6od9s - 1)) );
Mark5:;
				// continue
			}
						}		}//*     Compute global Gerschgorin bounds and spectral diameter 
		
		_6t1khtu3 = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_lr8ennxn ) ,ILNumerics.F2NET.Intrinsics.ABS(_jf74kq7y ) );
		_lr8ennxn = ((_lr8ennxn - (((_7fnb0l4r * _6t1khtu3) * _p1iqarg6) * _dxpq0xkr)) - ((_7fnb0l4r * _5m0mjfxm) * _3aphllyg));
		_jf74kq7y = ((_jf74kq7y + (((_7fnb0l4r * _6t1khtu3) * _p1iqarg6) * _dxpq0xkr)) + ((_7fnb0l4r * _5m0mjfxm) * _3aphllyg));//*     [JAN/28/2009] remove the line below since SPDIAM variable not use 
		//*     SPDIAM = GU - GL 
		//*     Input arguments for DLAEBZ: 
		//*     The relative tolerance.  An interval (a,b] lies within 
		//*     "relative tolerance" if  b-a < RELTOL*max(|a|,|b|), 
		
		_eki85d4y = _brq3wv6n;//*     Set the absolute tolerance for interval convergence to zero to force 
		//*     interval convergence based on relative size of the interval. 
		//*     This is dangerous because intervals might not converge when RELTOL is 
		//*     small. But at least a very small number should be selected so that for 
		//*     strongly graded matrices, the code can get relatively accurate 
		//*     eigenvalues. 
		
		_jmec0f4x = (((_7fnb0l4r * _5m0mjfxm) * _ddd01k2r) + ((_7fnb0l4r * _5m0mjfxm) * _3aphllyg));// 
		
		if (_o42699gn == _atfcdrg6)
		{
			// 
			//*        RANGE='I': Compute an interval containing eigenvalues 
			//*        IL through IU. The initial interval [GL,GU] from the global 
			//*        Gerschgorin bounds GL and GU is refined by DLAEBZ. 
			
			_7u74ue5o = (ILNumerics.F2NET.Intrinsics.INT((ILNumerics.F2NET.Intrinsics.LOG(_6t1khtu3 + _3aphllyg ) - ILNumerics.F2NET.Intrinsics.LOG(_3aphllyg )) / ILNumerics.F2NET.Intrinsics.LOG(_5m0mjfxm ) ) + (int)2);
			*(_apig8meb+(_dxpq0xkr + (int)1 - 1)) = _lr8ennxn;
			*(_apig8meb+(_dxpq0xkr + (int)2 - 1)) = _lr8ennxn;
			*(_apig8meb+(_dxpq0xkr + (int)3 - 1)) = _jf74kq7y;
			*(_apig8meb+(_dxpq0xkr + (int)4 - 1)) = _jf74kq7y;
			*(_apig8meb+(_dxpq0xkr + (int)5 - 1)) = _lr8ennxn;
			*(_apig8meb+(_dxpq0xkr + (int)6 - 1)) = _jf74kq7y;
			*(_4b6rt45i+((int)1 - 1)) = (int)-1;
			*(_4b6rt45i+((int)2 - 1)) = (int)-1;
			*(_4b6rt45i+((int)3 - 1)) = (_dxpq0xkr + (int)1);
			*(_4b6rt45i+((int)4 - 1)) = (_dxpq0xkr + (int)1);
			*(_4b6rt45i+((int)5 - 1)) = (_ic6kua09 - (int)1);
			*(_4b6rt45i+((int)6 - 1)) = _j4l29b9c;//* 
			
			_8367l6gs(ref Unsafe.AsRef((int)3) ,ref _7u74ue5o ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)2) ,ref Unsafe.AsRef((int)2) ,ref _f7059815 ,ref _jmec0f4x ,ref _eki85d4y ,ref _3aphllyg ,_plfm7z8g ,_864fslqq ,_0maek8rz ,(_4b6rt45i+((int)5 - 1)),(_apig8meb+(_dxpq0xkr + (int)1 - 1)),(_apig8meb+(_dxpq0xkr + (int)5 - 1)),ref _5q9hyd4z ,_4b6rt45i ,_z1ioc3c8 ,_5zga5mk9 ,ref _itfnbz60 );
			if (_itfnbz60 != (int)0)
			{
				
				_gro5yvfo = _itfnbz60;
				return;
			}
			//*        On exit, output intervals may not be ordered by ascending negcount 
			
			if (*(_4b6rt45i+((int)6 - 1)) == _j4l29b9c)
			{
				
				_wkzzmhv7 = *(_apig8meb+(_dxpq0xkr + (int)1 - 1));
				_okixw5up = *(_apig8meb+(_dxpq0xkr + (int)3 - 1));
				_2qketc28 = *(_4b6rt45i+((int)1 - 1));
				_z2bllur5 = *(_apig8meb+(_dxpq0xkr + (int)4 - 1));
				_ehalmmz0 = *(_apig8meb+(_dxpq0xkr + (int)2 - 1));
				_2u7bqeo4 = *(_4b6rt45i+((int)4 - 1));
			}
			else
			{
				
				_wkzzmhv7 = *(_apig8meb+(_dxpq0xkr + (int)2 - 1));
				_okixw5up = *(_apig8meb+(_dxpq0xkr + (int)4 - 1));
				_2qketc28 = *(_4b6rt45i+((int)2 - 1));
				_z2bllur5 = *(_apig8meb+(_dxpq0xkr + (int)3 - 1));
				_ehalmmz0 = *(_apig8meb+(_dxpq0xkr + (int)1 - 1));
				_2u7bqeo4 = *(_4b6rt45i+((int)3 - 1));
			}
			//*        On exit, the interval [WL, WLU] contains a value with negcount NWL, 
			//*        and [WUL, WU] contains a value with negcount NWU. 
			
			if ((((_2qketc28 < (int)0) | (_2qketc28 >= _dxpq0xkr)) | (_2u7bqeo4 < (int)1)) | (_2u7bqeo4 > _dxpq0xkr))
			{
				
				_gro5yvfo = (int)4;
				return;
			}
			// 
			
		}
		else
		if (_o42699gn == _55ursdfo)
		{
			
			_wkzzmhv7 = _ppzorcqs;
			_z2bllur5 = _qqhwr930;// 
			
		}
		else
		if (_o42699gn == _ytsyu76w)
		{
			
			_wkzzmhv7 = _lr8ennxn;
			_z2bllur5 = _jf74kq7y;
		}
		// 
		// 
		// 
		//*     Find Eigenvalues -- Loop Over blocks and recompute NWL and NWU. 
		//*     NWL accumulates the number of eigenvalues .le. WL, 
		//*     NWU accumulates the number of eigenvalues .le. WU 
		
		_ev4xhht5 = (int)0;
		_9dbezfkf = (int)0;
		_gro5yvfo = (int)0;
		_2qketc28 = (int)0;
		_2u7bqeo4 = (int)0;//* 
		
		{
			System.Int32 __81fgg2dlsvn3025 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step3025 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3025;
			for (__81fgg2count3025 = System.Math.Max(0, (System.Int32)(((System.Int32)(_naa7acm7) - __81fgg2dlsvn3025 + __81fgg2step3025) / __81fgg2step3025)), _18fz0zf9 = __81fgg2dlsvn3025; __81fgg2count3025 != 0; __81fgg2count3025--, _18fz0zf9 += (__81fgg2step3025)) {

			{
				
				_kk45tx7l = _9dbezfkf;
				_d0i9k0it = (_kk45tx7l + (int)1);
				_9dbezfkf = *(_nn033w1s+(_18fz0zf9 - 1));
				_oxr7eu3o = (_9dbezfkf - _kk45tx7l);//* 
				
				if (_oxr7eu3o == (int)1)
				{
					//*           1x1 block 
					
					if (_wkzzmhv7 >= (*(_plfm7z8g+(_d0i9k0it - 1)) - _3aphllyg))
					_2qketc28 = (_2qketc28 + (int)1);
					if (_z2bllur5 >= (*(_plfm7z8g+(_d0i9k0it - 1)) - _3aphllyg))
					_2u7bqeo4 = (_2u7bqeo4 + (int)1);
					if ((_o42699gn == _ytsyu76w) | ((_wkzzmhv7 < (*(_plfm7z8g+(_d0i9k0it - 1)) - _3aphllyg)) & (_z2bllur5 >= (*(_plfm7z8g+(_d0i9k0it - 1)) - _3aphllyg))))
					{
						
						_ev4xhht5 = (_ev4xhht5 + (int)1);
						*(_z1ioc3c8+(_ev4xhht5 - 1)) = *(_plfm7z8g+(_d0i9k0it - 1));
						*(_sgbqptwj+(_ev4xhht5 - 1)) = _d0547bi2;//*              The gap for a single block doesn't matter for the later 
						//*              algorithm and is assigned an arbitrary large value 
						
						*(_5zga5mk9+(_ev4xhht5 - 1)) = _18fz0zf9;
						*(_z5l332hf+(_ev4xhht5 - 1)) = (int)1;
					}
					// 
					//*        Disabled 2x2 case because of a failure on the following matrix 
					//*        RANGE = 'I', IL = IU = 4 
					//*          Original Tridiagonal, d = [ 
					//*           -0.150102010615740E+00 
					//*           -0.849897989384260E+00 
					//*           -0.128208148052635E-15 
					//*            0.128257718286320E-15 
					//*          ]; 
					//*          e = [ 
					//*           -0.357171383266986E+00 
					//*           -0.180411241501588E-15 
					//*           -0.175152352710251E-15 
					//*          ]; 
					//* 
					//*         ELSE IF( IN.EQ.2 ) THEN 
					//**           2x2 block 
					//*            DISC = SQRT( (HALF*(D(IBEGIN)-D(IEND)))**2 + E(IBEGIN)**2 ) 
					//*            TMP1 = HALF*(D(IBEGIN)+D(IEND)) 
					//*            L1 = TMP1 - DISC 
					//*            IF( WL.GE. L1-PIVMIN ) 
					//*     $         NWL = NWL + 1 
					//*            IF( WU.GE. L1-PIVMIN ) 
					//*     $         NWU = NWU + 1 
					//*            IF( IRANGE.EQ.ALLRNG .OR. ( WL.LT.L1-PIVMIN .AND. WU.GE. 
					//*     $          L1-PIVMIN ) ) THEN 
					//*               M = M + 1 
					//*               W( M ) = L1 
					//**              The uncertainty of eigenvalues of a 2x2 matrix is very small 
					//*               WERR( M ) = EPS * ABS( W( M ) ) * TWO 
					//*               IBLOCK( M ) = JBLK 
					//*               INDEXW( M ) = 1 
					//*            ENDIF 
					//*            L2 = TMP1 + DISC 
					//*            IF( WL.GE. L2-PIVMIN ) 
					//*     $         NWL = NWL + 1 
					//*            IF( WU.GE. L2-PIVMIN ) 
					//*     $         NWU = NWU + 1 
					//*            IF( IRANGE.EQ.ALLRNG .OR. ( WL.LT.L2-PIVMIN .AND. WU.GE. 
					//*     $          L2-PIVMIN ) ) THEN 
					//*               M = M + 1 
					//*               W( M ) = L2 
					//**              The uncertainty of eigenvalues of a 2x2 matrix is very small 
					//*               WERR( M ) = EPS * ABS( W( M ) ) * TWO 
					//*               IBLOCK( M ) = JBLK 
					//*               INDEXW( M ) = 2 
					//*            ENDIF 
					
				}
				else
				{
					//*           General Case - block of size IN >= 2 
					//*           Compute local Gerschgorin interval and use it as the initial 
					//*           interval for DLAEBZ 
					
					_jf74kq7y = *(_plfm7z8g+(_d0i9k0it - 1));
					_lr8ennxn = *(_plfm7z8g+(_d0i9k0it - 1));
					_c0o9kuh7 = _d0547bi2;// 
					
					{
						System.Int32 __81fgg2dlsvn3026 = (System.Int32)(_d0i9k0it);
						const System.Int32 __81fgg2step3026 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3026;
						for (__81fgg2count3026 = System.Math.Max(0, (System.Int32)(((System.Int32)(_9dbezfkf) - __81fgg2dlsvn3026 + __81fgg2step3026) / __81fgg2step3026)), _znpjgsef = __81fgg2dlsvn3026; __81fgg2count3026 != 0; __81fgg2count3026--, _znpjgsef += (__81fgg2step3026)) {

						{
							
							_lr8ennxn = ILNumerics.F2NET.Intrinsics.MIN(_lr8ennxn ,*(_yisb1mtu+(((int)2 * _znpjgsef) - (int)1 - 1)) );
							_jf74kq7y = ILNumerics.F2NET.Intrinsics.MAX(_jf74kq7y ,*(_yisb1mtu+((int)2 * _znpjgsef - 1)) );
Mark40:;
							// continue
						}
												}					}//*           [JAN/28/2009] 
					//*           change SPDIAM by TNORM in lines 2 and 3 thereafter 
					//*           line 1: remove computation of SPDIAM (not useful anymore) 
					//*           SPDIAM = GU - GL 
					//*           GL = GL - FUDGE*SPDIAM*EPS*IN - FUDGE*PIVMIN 
					//*           GU = GU + FUDGE*SPDIAM*EPS*IN + FUDGE*PIVMIN 
					
					_lr8ennxn = ((_lr8ennxn - (((_7fnb0l4r * _6t1khtu3) * _p1iqarg6) * _oxr7eu3o)) - (_7fnb0l4r * _3aphllyg));
					_jf74kq7y = ((_jf74kq7y + (((_7fnb0l4r * _6t1khtu3) * _p1iqarg6) * _oxr7eu3o)) + (_7fnb0l4r * _3aphllyg));//* 
					
					if (_o42699gn > (int)1)
					{
						
						if (_jf74kq7y < _wkzzmhv7)
						{
							//*                 the local block contains none of the wanted eigenvalues 
							
							_2qketc28 = (_2qketc28 + _oxr7eu3o);
							_2u7bqeo4 = (_2u7bqeo4 + _oxr7eu3o);goto Mark70;
						}
						//*              refine search interval if possible, only range (WL,WU] matters 
						
						_lr8ennxn = ILNumerics.F2NET.Intrinsics.MAX(_lr8ennxn ,_wkzzmhv7 );
						_jf74kq7y = ILNumerics.F2NET.Intrinsics.MIN(_jf74kq7y ,_z2bllur5 );
						if (_lr8ennxn >= _jf74kq7y)goto Mark70;
					}
					// 
					//*           Find negcount of initial interval boundaries GL and GU 
					
					*(_apig8meb+(_dxpq0xkr + (int)1 - 1)) = _lr8ennxn;
					*(_apig8meb+((_dxpq0xkr + _oxr7eu3o) + (int)1 - 1)) = _jf74kq7y;
					_8367l6gs(ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef((int)0) ,ref _oxr7eu3o ,ref _oxr7eu3o ,ref Unsafe.AsRef((int)1) ,ref _f7059815 ,ref _jmec0f4x ,ref _eki85d4y ,ref _3aphllyg ,(_plfm7z8g+(_d0i9k0it - 1)),(_864fslqq+(_d0i9k0it - 1)),(_0maek8rz+(_d0i9k0it - 1)),_4wo395cy ,(_apig8meb+(_dxpq0xkr + (int)1 - 1)),(_apig8meb+((_dxpq0xkr + ((int)2 * _oxr7eu3o)) + (int)1 - 1)),ref _2vvers93 ,_4b6rt45i ,(_z1ioc3c8+(_ev4xhht5 + (int)1 - 1)),(_5zga5mk9+(_ev4xhht5 + (int)1 - 1)),ref _itfnbz60 );
					if (_itfnbz60 != (int)0)
					{
						
						_gro5yvfo = _itfnbz60;
						return;
					}
					//* 
					
					_2qketc28 = (_2qketc28 + *(_4b6rt45i+((int)1 - 1)));
					_2u7bqeo4 = (_2u7bqeo4 + *(_4b6rt45i+(_oxr7eu3o + (int)1 - 1)));
					_kxxzluvq = (_ev4xhht5 - *(_4b6rt45i+((int)1 - 1)));// 
					//*           Compute Eigenvalues 
					
					_7u74ue5o = (ILNumerics.F2NET.Intrinsics.INT((ILNumerics.F2NET.Intrinsics.LOG((_jf74kq7y - _lr8ennxn) + _3aphllyg ) - ILNumerics.F2NET.Intrinsics.LOG(_3aphllyg )) / ILNumerics.F2NET.Intrinsics.LOG(_5m0mjfxm ) ) + (int)2);
					_8367l6gs(ref Unsafe.AsRef((int)2) ,ref _7u74ue5o ,ref _oxr7eu3o ,ref _oxr7eu3o ,ref Unsafe.AsRef((int)1) ,ref _f7059815 ,ref _jmec0f4x ,ref _eki85d4y ,ref _3aphllyg ,(_plfm7z8g+(_d0i9k0it - 1)),(_864fslqq+(_d0i9k0it - 1)),(_0maek8rz+(_d0i9k0it - 1)),_4wo395cy ,(_apig8meb+(_dxpq0xkr + (int)1 - 1)),(_apig8meb+((_dxpq0xkr + ((int)2 * _oxr7eu3o)) + (int)1 - 1)),ref _5q9hyd4z ,_4b6rt45i ,(_z1ioc3c8+(_ev4xhht5 + (int)1 - 1)),(_5zga5mk9+(_ev4xhht5 + (int)1 - 1)),ref _itfnbz60 );
					if (_itfnbz60 != (int)0)
					{
						
						_gro5yvfo = _itfnbz60;
						return;
					}
					//* 
					//*           Copy eigenvalues into W and IBLOCK 
					//*           Use -JBLK for block number for unconverged eigenvalues. 
					//*           Loop over the number of output intervals from DLAEBZ 
					
					{
						System.Int32 __81fgg2dlsvn3027 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3027 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3027;
						for (__81fgg2count3027 = System.Math.Max(0, (System.Int32)(((System.Int32)(_5q9hyd4z) - __81fgg2dlsvn3027 + __81fgg2step3027) / __81fgg2step3027)), _znpjgsef = __81fgg2dlsvn3027; __81fgg2count3027 != 0; __81fgg2count3027--, _znpjgsef += (__81fgg2step3027)) {

						{
							//*              eigenvalue approximation is middle point of interval 
							
							_c0o9kuh7 = (_gbf4169i * (*(_apig8meb+(_znpjgsef + _dxpq0xkr - 1)) + *(_apig8meb+((_znpjgsef + _oxr7eu3o) + _dxpq0xkr - 1))));//*              semi length of error interval 
							
							_ww3bdyup = (_gbf4169i * ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+(_znpjgsef + _dxpq0xkr - 1)) - *(_apig8meb+((_znpjgsef + _oxr7eu3o) + _dxpq0xkr - 1)) ));
							if (_znpjgsef > (_5q9hyd4z - _itfnbz60))
							{
								//*                 Flag non-convergence. 
								
								_uwgmxffk = true;
								_vyr1z1si = (-(_18fz0zf9));
							}
							else
							{
								
								_vyr1z1si = _18fz0zf9;
							}
							
							{
								System.Int32 __81fgg2dlsvn3028 = (System.Int32)(((*(_4b6rt45i+(_znpjgsef - 1)) + (int)1) + _kxxzluvq));
								const System.Int32 __81fgg2step3028 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3028;
								for (__81fgg2count3028 = System.Math.Max(0, (System.Int32)(((System.Int32)(*(_4b6rt45i+(_znpjgsef + _oxr7eu3o - 1)) + _kxxzluvq) - __81fgg2dlsvn3028 + __81fgg2step3028) / __81fgg2step3028)), _22sk16or = __81fgg2dlsvn3028; __81fgg2count3028 != 0; __81fgg2count3028--, _22sk16or += (__81fgg2step3028)) {

								{
									
									*(_z1ioc3c8+(_22sk16or - 1)) = _c0o9kuh7;
									*(_sgbqptwj+(_22sk16or - 1)) = _ww3bdyup;
									*(_z5l332hf+(_22sk16or - 1)) = (_22sk16or - _kxxzluvq);
									*(_5zga5mk9+(_22sk16or - 1)) = _vyr1z1si;
Mark50:;
									// continue
								}
																}							}
Mark60:;
							// continue
						}
												}					}//* 
					
					_ev4xhht5 = (_ev4xhht5 + _2vvers93);
				}
				
Mark70:;
				// continue
			}
						}		}// 
		//*     If RANGE='I', then (WL,WU) contains eigenvalues NWL+1,...,NWU 
		//*     If NWL+1 < IL or NWU > IU, discard extra eigenvalues. 
		
		if (_o42699gn == _atfcdrg6)
		{
			
			_4zavrvex = ((_ic6kua09 - (int)1) - _2qketc28);
			_5pjgpo42 = (_2u7bqeo4 - _j4l29b9c);//* 
			
			if (_4zavrvex > (int)0)
			{
				
				_2vvers93 = (int)0;
				{
					System.Int32 __81fgg2dlsvn3029 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3029 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3029;
					for (__81fgg2count3029 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3029 + __81fgg2step3029) / __81fgg2step3029)), _22sk16or = __81fgg2dlsvn3029; __81fgg2count3029 != 0; __81fgg2count3029--, _22sk16or += (__81fgg2step3029)) {

					{
						//*              Remove some of the smallest eigenvalues from the left so that 
						//*              at the end IDISCL =0. Move all eigenvalues up to the left. 
						
						if ((*(_z1ioc3c8+(_22sk16or - 1)) <= _okixw5up) & (_4zavrvex > (int)0))
						{
							
							_4zavrvex = (_4zavrvex - (int)1);
						}
						else
						{
							
							_2vvers93 = (_2vvers93 + (int)1);
							*(_z1ioc3c8+(_2vvers93 - 1)) = *(_z1ioc3c8+(_22sk16or - 1));
							*(_sgbqptwj+(_2vvers93 - 1)) = *(_sgbqptwj+(_22sk16or - 1));
							*(_z5l332hf+(_2vvers93 - 1)) = *(_z5l332hf+(_22sk16or - 1));
							*(_5zga5mk9+(_2vvers93 - 1)) = *(_5zga5mk9+(_22sk16or - 1));
						}
						
Mark80:;
						// continue
					}
										}				}
				_ev4xhht5 = _2vvers93;
			}
			
			if (_5pjgpo42 > (int)0)
			{
				//*           Remove some of the largest eigenvalues from the right so that 
				//*           at the end IDISCU =0. Move all eigenvalues up to the left. 
				
				_2vvers93 = (_ev4xhht5 + (int)1);
				{
					System.Int32 __81fgg2dlsvn3030 = (System.Int32)(_ev4xhht5);
					System.Int32 __81fgg2step3030 = (System.Int32)((int)-1);
					System.Int32 __81fgg2count3030;
					for (__81fgg2count3030 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn3030 + __81fgg2step3030) / __81fgg2step3030)), _22sk16or = __81fgg2dlsvn3030; __81fgg2count3030 != 0; __81fgg2count3030--, _22sk16or += (__81fgg2step3030)) {

					{
						
						if ((*(_z1ioc3c8+(_22sk16or - 1)) >= _ehalmmz0) & (_5pjgpo42 > (int)0))
						{
							
							_5pjgpo42 = (_5pjgpo42 - (int)1);
						}
						else
						{
							
							_2vvers93 = (_2vvers93 - (int)1);
							*(_z1ioc3c8+(_2vvers93 - 1)) = *(_z1ioc3c8+(_22sk16or - 1));
							*(_sgbqptwj+(_2vvers93 - 1)) = *(_sgbqptwj+(_22sk16or - 1));
							*(_z5l332hf+(_2vvers93 - 1)) = *(_z5l332hf+(_22sk16or - 1));
							*(_5zga5mk9+(_2vvers93 - 1)) = *(_5zga5mk9+(_22sk16or - 1));
						}
						
Mark81:;
						// continue
					}
										}				}
				_gw26iq4i = (int)0;
				{
					System.Int32 __81fgg2dlsvn3031 = (System.Int32)(_2vvers93);
					const System.Int32 __81fgg2step3031 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3031;
					for (__81fgg2count3031 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3031 + __81fgg2step3031) / __81fgg2step3031)), _22sk16or = __81fgg2dlsvn3031; __81fgg2count3031 != 0; __81fgg2count3031--, _22sk16or += (__81fgg2step3031)) {

					{
						
						_gw26iq4i = (_gw26iq4i + (int)1);
						*(_z1ioc3c8+(_gw26iq4i - 1)) = *(_z1ioc3c8+(_22sk16or - 1));
						*(_sgbqptwj+(_gw26iq4i - 1)) = *(_sgbqptwj+(_22sk16or - 1));
						*(_z5l332hf+(_gw26iq4i - 1)) = *(_z5l332hf+(_22sk16or - 1));
						*(_5zga5mk9+(_gw26iq4i - 1)) = *(_5zga5mk9+(_22sk16or - 1));
Mark82:;
						// continue
					}
										}				}
				_ev4xhht5 = ((_ev4xhht5 - _2vvers93) + (int)1);
			}
			// 
			
			if ((_4zavrvex > (int)0) | (_5pjgpo42 > (int)0))
			{
				//*           Code to deal with effects of bad arithmetic. (If N(w) is 
				//*           monotone non-decreasing, this should never happen.) 
				//*           Some low eigenvalues to be discarded are not in (WL,WLU], 
				//*           or high eigenvalues to be discarded are not in (WUL,WU] 
				//*           so just kill off the smallest IDISCL/largest IDISCU 
				//*           eigenvalues, by marking the corresponding IBLOCK = 0 
				
				if (_4zavrvex > (int)0)
				{
					
					_oq8p752h = _z2bllur5;
					{
						System.Int32 __81fgg2dlsvn3032 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3032 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3032;
						for (__81fgg2count3032 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4zavrvex) - __81fgg2dlsvn3032 + __81fgg2step3032) / __81fgg2step3032)), _16jas2ek = __81fgg2dlsvn3032; __81fgg2count3032 != 0; __81fgg2count3032--, _16jas2ek += (__81fgg2step3032)) {

						{
							
							_11qhqs00 = (int)0;
							{
								System.Int32 __81fgg2dlsvn3033 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3033 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3033;
								for (__81fgg2count3033 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3033 + __81fgg2step3033) / __81fgg2step3033)), _22sk16or = __81fgg2dlsvn3033; __81fgg2count3033 != 0; __81fgg2count3033--, _22sk16or += (__81fgg2step3033)) {

								{
									
									if ((*(_5zga5mk9+(_22sk16or - 1)) != (int)0) & ((*(_z1ioc3c8+(_22sk16or - 1)) < _oq8p752h) | (_11qhqs00 == (int)0)))
									{
										
										_11qhqs00 = _22sk16or;
										_oq8p752h = *(_z1ioc3c8+(_22sk16or - 1));
									}
									
Mark90:;
									// continue
								}
																}							}
							*(_5zga5mk9+(_11qhqs00 - 1)) = (int)0;
Mark100:;
							// continue
						}
												}					}
				}
				
				if (_5pjgpo42 > (int)0)
				{
					
					_oq8p752h = _wkzzmhv7;
					{
						System.Int32 __81fgg2dlsvn3034 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3034 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3034;
						for (__81fgg2count3034 = System.Math.Max(0, (System.Int32)(((System.Int32)(_5pjgpo42) - __81fgg2dlsvn3034 + __81fgg2step3034) / __81fgg2step3034)), _16jas2ek = __81fgg2dlsvn3034; __81fgg2count3034 != 0; __81fgg2count3034--, _16jas2ek += (__81fgg2step3034)) {

						{
							
							_11qhqs00 = (int)0;
							{
								System.Int32 __81fgg2dlsvn3035 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3035 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3035;
								for (__81fgg2count3035 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3035 + __81fgg2step3035) / __81fgg2step3035)), _22sk16or = __81fgg2dlsvn3035; __81fgg2count3035 != 0; __81fgg2count3035--, _22sk16or += (__81fgg2step3035)) {

								{
									
									if ((*(_5zga5mk9+(_22sk16or - 1)) != (int)0) & ((*(_z1ioc3c8+(_22sk16or - 1)) >= _oq8p752h) | (_11qhqs00 == (int)0)))
									{
										
										_11qhqs00 = _22sk16or;
										_oq8p752h = *(_z1ioc3c8+(_22sk16or - 1));
									}
									
Mark110:;
									// continue
								}
																}							}
							*(_5zga5mk9+(_11qhqs00 - 1)) = (int)0;
Mark120:;
							// continue
						}
												}					}
				}
				//*           Now erase all eigenvalues with IBLOCK set to zero 
				
				_2vvers93 = (int)0;
				{
					System.Int32 __81fgg2dlsvn3036 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3036 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3036;
					for (__81fgg2count3036 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3036 + __81fgg2step3036) / __81fgg2step3036)), _22sk16or = __81fgg2dlsvn3036; __81fgg2count3036 != 0; __81fgg2count3036--, _22sk16or += (__81fgg2step3036)) {

					{
						
						if (*(_5zga5mk9+(_22sk16or - 1)) != (int)0)
						{
							
							_2vvers93 = (_2vvers93 + (int)1);
							*(_z1ioc3c8+(_2vvers93 - 1)) = *(_z1ioc3c8+(_22sk16or - 1));
							*(_sgbqptwj+(_2vvers93 - 1)) = *(_sgbqptwj+(_22sk16or - 1));
							*(_z5l332hf+(_2vvers93 - 1)) = *(_z5l332hf+(_22sk16or - 1));
							*(_5zga5mk9+(_2vvers93 - 1)) = *(_5zga5mk9+(_22sk16or - 1));
						}
						
Mark130:;
						// continue
					}
										}				}
				_ev4xhht5 = _2vvers93;
			}
			
			if ((_4zavrvex < (int)0) | (_5pjgpo42 < (int)0))
			{
				
				_9vwzg4sj = true;
			}
			
		}
		//* 
		
		if (((_o42699gn == _ytsyu76w) & (_ev4xhht5 != _dxpq0xkr)) | ((_o42699gn == _atfcdrg6) & (_ev4xhht5 != ((_j4l29b9c - _ic6kua09) + (int)1))))
		{
			
			_9vwzg4sj = true;
		}
		// 
		//*     If ORDER='B', do nothing the eigenvalues are already sorted by 
		//*        block. 
		//*     If ORDER='E', sort the eigenvalues from smallest to largest 
		// 
		
		if (_w8y2rzgy(_loathnh7 ,"E" ) & (_naa7acm7 > (int)1))
		{
			
			{
				System.Int32 __81fgg2dlsvn3037 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step3037 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3037;
				for (__81fgg2count3037 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5 - (int)1) - __81fgg2dlsvn3037 + __81fgg2step3037) / __81fgg2step3037)), _22sk16or = __81fgg2dlsvn3037; __81fgg2count3037 != 0; __81fgg2count3037--, _22sk16or += (__81fgg2step3037)) {

				{
					
					_smxeww0r = (int)0;
					_c0o9kuh7 = *(_z1ioc3c8+(_22sk16or - 1));
					{
						System.Int32 __81fgg2dlsvn3038 = (System.Int32)((_22sk16or + (int)1));
						const System.Int32 __81fgg2step3038 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3038;
						for (__81fgg2count3038 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3038 + __81fgg2step3038) / __81fgg2step3038)), _znpjgsef = __81fgg2dlsvn3038; __81fgg2count3038 != 0; __81fgg2count3038--, _znpjgsef += (__81fgg2step3038)) {

						{
							
							if (*(_z1ioc3c8+(_znpjgsef - 1)) < _c0o9kuh7)
							{
								
								_smxeww0r = _znpjgsef;
								_c0o9kuh7 = *(_z1ioc3c8+(_znpjgsef - 1));
							}
							
Mark140:;
							// continue
						}
												}					}
					if (_smxeww0r != (int)0)
					{
						
						_ww3bdyup = *(_sgbqptwj+(_smxeww0r - 1));
						_qq2166xp = *(_5zga5mk9+(_smxeww0r - 1));
						_uqke0mt1 = *(_z5l332hf+(_smxeww0r - 1));
						*(_z1ioc3c8+(_smxeww0r - 1)) = *(_z1ioc3c8+(_22sk16or - 1));
						*(_sgbqptwj+(_smxeww0r - 1)) = *(_sgbqptwj+(_22sk16or - 1));
						*(_5zga5mk9+(_smxeww0r - 1)) = *(_5zga5mk9+(_22sk16or - 1));
						*(_z5l332hf+(_smxeww0r - 1)) = *(_z5l332hf+(_22sk16or - 1));
						*(_z1ioc3c8+(_22sk16or - 1)) = _c0o9kuh7;
						*(_sgbqptwj+(_22sk16or - 1)) = _ww3bdyup;
						*(_5zga5mk9+(_22sk16or - 1)) = _qq2166xp;
						*(_z5l332hf+(_22sk16or - 1)) = _uqke0mt1;
					}
					
Mark150:;
					// continue
				}
								}			}
		}
		//* 
		
		_gro5yvfo = (int)0;
		if (_uwgmxffk)
		_gro5yvfo = (_gro5yvfo + (int)1);
		if (_9vwzg4sj)
		_gro5yvfo = (_gro5yvfo + (int)2);
		return;//* 
		//*     End of DLARRD 
		//* 
		
	}
	
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
