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
//*> \brief \b ZGESDD 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZGESDD + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zgesdd.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zgesdd.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zgesdd.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZGESDD( JOBZ, M, N, A, LDA, S, U, LDU, VT, LDVT, 
//*                          WORK, LWORK, RWORK, IWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          JOBZ 
//*       INTEGER            INFO, LDA, LDU, LDVT, LWORK, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IWORK( * ) 
//*       DOUBLE PRECISION   RWORK( * ), S( * ) 
//*       COMPLEX*16         A( LDA, * ), U( LDU, * ), VT( LDVT, * ), 
//*      $                   WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZGESDD computes the singular value decomposition (SVD) of a complex 
//*> M-by-N matrix A, optionally computing the left and/or right singular 
//*> vectors, by using divide-and-conquer method. The SVD is written 
//*> 
//*>      A = U * SIGMA * conjugate-transpose(V) 
//*> 
//*> where SIGMA is an M-by-N matrix which is zero except for its 
//*> min(m,n) diagonal elements, U is an M-by-M unitary matrix, and 
//*> V is an N-by-N unitary matrix.  The diagonal elements of SIGMA 
//*> are the singular values of A; they are real and non-negative, and 
//*> are returned in descending order.  The first min(m,n) columns of 
//*> U and V are the left and right singular vectors of A. 
//*> 
//*> Note that the routine returns VT = V**H, not V. 
//*> 
//*> The divide and conquer algorithm makes very mild assumptions about 
//*> floating point arithmetic. It will work on machines with a guard 
//*> digit in add/subtract, or on those binary machines without guard 
//*> digits which subtract like the Cray X-MP, Cray Y-MP, Cray C-90, or 
//*> Cray-2. It could conceivably fail on hexadecimal or decimal machines 
//*> without guard digits, but we know of none. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] JOBZ 
//*> \verbatim 
//*>          JOBZ is CHARACTER*1 
//*>          Specifies options for computing all or part of the matrix U: 
//*>          = 'A':  all M columns of U and all N rows of V**H are 
//*>                  returned in the arrays U and VT; 
//*>          = 'S':  the first min(M,N) columns of U and the first 
//*>                  min(M,N) rows of V**H are returned in the arrays U 
//*>                  and VT; 
//*>          = 'O':  If M >= N, the first N columns of U are overwritten 
//*>                  in the array A and all rows of V**H are returned in 
//*>                  the array VT; 
//*>                  otherwise, all columns of U are returned in the 
//*>                  array U and the first M rows of V**H are overwritten 
//*>                  in the array A; 
//*>          = 'N':  no columns of U or rows of V**H are computed. 
//*> \endverbatim 
//*> 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of rows of the input matrix A.  M >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of columns of the input matrix A.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension (LDA,N) 
//*>          On entry, the M-by-N matrix A. 
//*>          On exit, 
//*>          if JOBZ = 'O',  A is overwritten with the first N columns 
//*>                          of U (the left singular vectors, stored 
//*>                          columnwise) if M >= N; 
//*>                          A is overwritten with the first M rows 
//*>                          of V**H (the right singular vectors, stored 
//*>                          rowwise) otherwise. 
//*>          if JOBZ .ne. 'O', the contents of A are destroyed. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,M). 
//*> \endverbatim 
//*> 
//*> \param[out] S 
//*> \verbatim 
//*>          S is DOUBLE PRECISION array, dimension (min(M,N)) 
//*>          The singular values of A, sorted so that S(i) >= S(i+1). 
//*> \endverbatim 
//*> 
//*> \param[out] U 
//*> \verbatim 
//*>          U is COMPLEX*16 array, dimension (LDU,UCOL) 
//*>          UCOL = M if JOBZ = 'A' or JOBZ = 'O' and M < N; 
//*>          UCOL = min(M,N) if JOBZ = 'S'. 
//*>          If JOBZ = 'A' or JOBZ = 'O' and M < N, U contains the M-by-M 
//*>          unitary matrix U; 
//*>          if JOBZ = 'S', U contains the first min(M,N) columns of U 
//*>          (the left singular vectors, stored columnwise); 
//*>          if JOBZ = 'O' and M >= N, or JOBZ = 'N', U is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] LDU 
//*> \verbatim 
//*>          LDU is INTEGER 
//*>          The leading dimension of the array U.  LDU >= 1; 
//*>          if JOBZ = 'S' or 'A' or JOBZ = 'O' and M < N, LDU >= M. 
//*> \endverbatim 
//*> 
//*> \param[out] VT 
//*> \verbatim 
//*>          VT is COMPLEX*16 array, dimension (LDVT,N) 
//*>          If JOBZ = 'A' or JOBZ = 'O' and M >= N, VT contains the 
//*>          N-by-N unitary matrix V**H; 
//*>          if JOBZ = 'S', VT contains the first min(M,N) rows of 
//*>          V**H (the right singular vectors, stored rowwise); 
//*>          if JOBZ = 'O' and M < N, or JOBZ = 'N', VT is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] LDVT 
//*> \verbatim 
//*>          LDVT is INTEGER 
//*>          The leading dimension of the array VT.  LDVT >= 1; 
//*>          if JOBZ = 'A' or JOBZ = 'O' and M >= N, LDVT >= N; 
//*>          if JOBZ = 'S', LDVT >= min(M,N). 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is COMPLEX*16 array, dimension (MAX(1,LWORK)) 
//*>          On exit, if INFO = 0, WORK(1) returns the optimal LWORK. 
//*> \endverbatim 
//*> 
//*> \param[in] LWORK 
//*> \verbatim 
//*>          LWORK is INTEGER 
//*>          The dimension of the array WORK. LWORK >= 1. 
//*>          If LWORK = -1, a workspace query is assumed.  The optimal 
//*>          size for the WORK array is calculated and stored in WORK(1), 
//*>          and no other work except argument checking is performed. 
//*> 
//*>          Let mx = max(M,N) and mn = min(M,N). 
//*>          If JOBZ = 'N', LWORK >= 2*mn + mx. 
//*>          If JOBZ = 'O', LWORK >= 2*mn*mn + 2*mn + mx. 
//*>          If JOBZ = 'S', LWORK >=   mn*mn + 3*mn. 
//*>          If JOBZ = 'A', LWORK >=   mn*mn + 2*mn + mx. 
//*>          These are not tight minimums in all cases; see comments inside code. 
//*>          For good performance, LWORK should generally be larger; 
//*>          a query is recommended. 
//*> \endverbatim 
//*> 
//*> \param[out] RWORK 
//*> \verbatim 
//*>          RWORK is DOUBLE PRECISION array, dimension (MAX(1,LRWORK)) 
//*>          Let mx = max(M,N) and mn = min(M,N). 
//*>          If JOBZ = 'N',    LRWORK >= 5*mn (LAPACK <= 3.6 needs 7*mn); 
//*>          else if mx >> mn, LRWORK >= 5*mn*mn + 5*mn; 
//*>          else              LRWORK >= max( 5*mn*mn + 5*mn, 
//*>                                           2*mx*mn + 2*mn*mn + mn ). 
//*> \endverbatim 
//*> 
//*> \param[out] IWORK 
//*> \verbatim 
//*>          IWORK is INTEGER array, dimension (8*min(M,N)) 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit. 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value. 
//*>          > 0:  The updating process of DBDSDC did not converge. 
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
//*> \ingroup complex16GEsing 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>     Ming Gu and Huan Ren, Computer Science Division, University of 
//*>     California at Berkeley, USA 
//*> 
//*  ===================================================================== 

	 
	public static void _6sr3m9vr(FString _w6igfk2h, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, complex* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _irk8i6qr, complex* _7u55mqkq, ref Int32 _u6e6d39b, complex* _xdbczr8u, ref Int32 _h4ibbatv, complex* _apig8meb, ref Int32 _6fnxzlyp, Double* _dqanbbw3, Int32* _4b6rt45i, ref Int32 _gro5yvfo)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)28 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
complex _gdjumcqt =   new fcomplex(0f,0f);
complex _40vhxf9f =   new fcomplex(1f,0f);
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Boolean _lhlgm7z5 =  default;
Boolean _6ecs6pbo =  default;
Boolean _9hcbk2ie =  default;
Boolean _7y8lc0fu =  default;
Boolean _mtv5r2zz =  default;
Boolean _ngfvoqx1 =  default;
Int32 _9giy8o3g =  default;
Int32 _8rfn4f7g =  default;
Int32 _b5p6od9s =  default;
Int32 _smxeww0r =  default;
Int32 _bhsiylw4 =  default;
Int32 _ic6kua09 =  default;
Int32 _m9w6lk7x =  default;
Int32 _n6v1ono2 =  default;
Int32 _emo79hqt =  default;
Int32 _65mv5f5m =  default;
Int32 _q1w15vsx =  default;
Int32 _5ke1jwwr =  default;
Int32 _wx1x93f0 =  default;
Int32 _j4l29b9c =  default;
Int32 _gt43n8d1 =  default;
Int32 _r9baffok =  default;
Int32 _j49hoimx =  default;
Int32 _v6sbkzy4 =  default;
Int32 _lbvz22dx =  default;
Int32 _tafa1evd =  default;
Int32 _qaseb1y7 =  default;
Int32 _gghrqcr1 =  default;
Int32 _lmbwg5z2 =  default;
Int32 _0sgoq9v8 =  default;
Int32 _r49fp4o3 =  default;
Int32 _1myocm5q =  default;
Int32 _loujht2t =  default;
Int32 _d3z2y722 =  default;
Int32 _tbzu075b =  default;
Int32 _5mohk7qw =  default;
Int32 _rmiwyg72 =  default;
Int32 _y1ayqutj =  default;
Int32 _vht8spye =  default;
Int32 _3zm0ngzg =  default;
Int32 _ncllygen =  default;
Int32 _d6597gvk =  default;
Int32 _2ewcnsdb =  default;
Int32 _ye383gos =  default;
Int32 _c3lnjivp =  default;
Int32 _dg4wuygw =  default;
Int32 _eb7iuw24 =  default;
Int32 _9jxtngt6 =  default;
Int32 _sm7d4hbu =  default;
Int32 _th9hxrra =  default;
Int32 _8g32if8p =  default;
Int32 _aqo9vcb6 =  default;
Double _j6vjow1g =  default;
Double _av7j8yda =  default;
Double _p1iqarg6 =  default;
Double _bogm0gwy =  default;
Int32* _xpllja47 =  (Int32*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Int32) * ((int)1);
Double* _g7qb61ha =  (Double*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Double) * ((int)1);
complex* _n7plx4io =  (complex*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(complex) * ((int)1);
string fLanavab = default;
#endregion  variable declarations
_w6igfk2h = _w6igfk2h.Convert(1);

	{
		//* 
		//*  -- LAPACK driver routine (version 3.7.0) -- 
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
		//*     .. Local Arrays .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input arguments 
		//* 
		
		_gro5yvfo = (int)0;
		_qaseb1y7 = ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_dxpq0xkr );
		_lmbwg5z2 = ILNumerics.F2NET.Intrinsics.INT((_qaseb1y7 * 17d) / 9d );
		_0sgoq9v8 = ILNumerics.F2NET.Intrinsics.INT((_qaseb1y7 * 5d) / 3d );
		_6ecs6pbo = _w8y2rzgy(_w6igfk2h ,"A" );
		_ngfvoqx1 = _w8y2rzgy(_w6igfk2h ,"S" );
		_9hcbk2ie = (_6ecs6pbo | _ngfvoqx1);
		_mtv5r2zz = _w8y2rzgy(_w6igfk2h ,"O" );
		_7y8lc0fu = _w8y2rzgy(_w6igfk2h ,"N" );
		_lhlgm7z5 = (_6fnxzlyp == (int)-1);
		_gghrqcr1 = (int)1;
		_tafa1evd = (int)1;//* 
		
		if (!((((_6ecs6pbo | _ngfvoqx1) | _mtv5r2zz) | _7y8lc0fu)))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_ev4xhht5 < (int)0)
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ))
		{
			
			_gro5yvfo = (int)-5;
		}
		else
		if (((_u6e6d39b < (int)1) | (_9hcbk2ie & (_u6e6d39b < _ev4xhht5))) | ((_mtv5r2zz & (_ev4xhht5 < _dxpq0xkr)) & (_u6e6d39b < _ev4xhht5)))
		{
			
			_gro5yvfo = (int)-8;
		}
		else
		if ((((_h4ibbatv < (int)1) | (_6ecs6pbo & (_h4ibbatv < _dxpq0xkr))) | (_ngfvoqx1 & (_h4ibbatv < _qaseb1y7))) | ((_mtv5r2zz & (_ev4xhht5 >= _dxpq0xkr)) & (_h4ibbatv < _dxpq0xkr)))
		{
			
			_gro5yvfo = (int)-10;
		}
		//* 
		//*     Compute workspace 
		//*       Note: Comments in the code beginning "Workspace:" describe the 
		//*       minimal amount of workspace allocated at that point in the code, 
		//*       as well as the preferred amount for good performance. 
		//*       CWorkspace refers to complex workspace, and RWorkspace to 
		//*       real workspace. NB refers to the optimal block size for the 
		//*       immediately following subroutine, as returned by ILAENV.) 
		//* 
		
		if (_gro5yvfo == (int)0)
		{
			
			_gghrqcr1 = (int)1;
			_tafa1evd = (int)1;
			if ((_ev4xhht5 >= _dxpq0xkr) & (_qaseb1y7 > (int)0))
			{
				//* 
				//*           There is no complex work space needed for bidiagonal SVD 
				//*           The real work space needed for bidiagonal SVD (dbdsdc) is 
				//*           BDSPAC = 3*N*N + 4*N for singular values and vectors; 
				//*           BDSPAC = 4*N         for singular values only; 
				//*           not including e, RU, and RVT matrices. 
				//* 
				//*           Compute space preferred for each routine 
				
				_w8dh0c16(ref _ev4xhht5 ,ref _dxpq0xkr ,(_n7plx4io+((int)1 - 1)),ref _ev4xhht5 ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_d3z2y722 = ILNumerics.F2NET.Intrinsics.INT(*(_n7plx4io+((int)1 - 1)) );//* 
				
				_w8dh0c16(ref _dxpq0xkr ,ref _dxpq0xkr ,(_n7plx4io+((int)1 - 1)),ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_5mohk7qw = ILNumerics.F2NET.Intrinsics.INT(*(_n7plx4io+((int)1 - 1)) );//* 
				
				_ljflx7wo(ref _ev4xhht5 ,ref _dxpq0xkr ,(_n7plx4io+((int)1 - 1)),ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_y1ayqutj = ILNumerics.F2NET.Intrinsics.INT(*(_n7plx4io+((int)1 - 1)) );//* 
				
				_anznwuhr("P" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_n7plx4io+((int)1 - 1)),ref _dxpq0xkr ,(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_3zm0ngzg = ILNumerics.F2NET.Intrinsics.INT(*(_n7plx4io+((int)1 - 1)) );//* 
				
				_anznwuhr("Q" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,(_n7plx4io+((int)1 - 1)),ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_d6597gvk = ILNumerics.F2NET.Intrinsics.INT(*(_n7plx4io+((int)1 - 1)) );//* 
				
				_anznwuhr("Q" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_n7plx4io+((int)1 - 1)),ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_ncllygen = ILNumerics.F2NET.Intrinsics.INT(*(_n7plx4io+((int)1 - 1)) );//* 
				
				_13b6etkp(ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,(_n7plx4io+((int)1 - 1)),ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_c3lnjivp = ILNumerics.F2NET.Intrinsics.INT(*(_n7plx4io+((int)1 - 1)) );//* 
				
				_13b6etkp(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_n7plx4io+((int)1 - 1)),ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_dg4wuygw = ILNumerics.F2NET.Intrinsics.INT(*(_n7plx4io+((int)1 - 1)) );//* 
				
				_2xvhm1xm("P" ,"R" ,"C" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_n7plx4io+((int)1 - 1)),ref _dxpq0xkr ,(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),ref _dxpq0xkr ,(_n7plx4io+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_8g32if8p = ILNumerics.F2NET.Intrinsics.INT(*(_n7plx4io+((int)1 - 1)) );//* 
				
				_2xvhm1xm("Q" ,"L" ,"N" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,(_n7plx4io+((int)1 - 1)),ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_9jxtngt6 = ILNumerics.F2NET.Intrinsics.INT(*(_n7plx4io+((int)1 - 1)) );//* 
				
				_2xvhm1xm("Q" ,"L" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_n7plx4io+((int)1 - 1)),ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_th9hxrra = ILNumerics.F2NET.Intrinsics.INT(*(_n7plx4io+((int)1 - 1)) );//* 
				
				_2xvhm1xm("Q" ,"L" ,"N" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_n7plx4io+((int)1 - 1)),ref _dxpq0xkr ,(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),ref _dxpq0xkr ,(_n7plx4io+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_aqo9vcb6 = ILNumerics.F2NET.Intrinsics.INT(*(_n7plx4io+((int)1 - 1)) );//* 
				
				if (_ev4xhht5 >= _lmbwg5z2)
				{
					
					if (_7y8lc0fu)
					{
						//* 
						//*                 Path 1 (M >> N, JOBZ='N') 
						//* 
						
						_tafa1evd = (_dxpq0xkr + _y1ayqutj);
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _dxpq0xkr) + _5mohk7qw );
						_gghrqcr1 = ((int)3 * _dxpq0xkr);
					}
					else
					if (_mtv5r2zz)
					{
						//* 
						//*                 Path 2 (M >> N, JOBZ='O') 
						//* 
						
						_loujht2t = (_dxpq0xkr + _y1ayqutj);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_dxpq0xkr + _dg4wuygw );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)2 * _dxpq0xkr) + _5mohk7qw );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)2 * _dxpq0xkr) + _aqo9vcb6 );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)2 * _dxpq0xkr) + _8g32if8p );
						_tafa1evd = (((_ev4xhht5 * _dxpq0xkr) + (_dxpq0xkr * _dxpq0xkr)) + _loujht2t);
						_gghrqcr1 = ((((int)2 * _dxpq0xkr) * _dxpq0xkr) + ((int)3 * _dxpq0xkr));
					}
					else
					if (_ngfvoqx1)
					{
						//* 
						//*                 Path 3 (M >> N, JOBZ='S') 
						//* 
						
						_loujht2t = (_dxpq0xkr + _y1ayqutj);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_dxpq0xkr + _dg4wuygw );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)2 * _dxpq0xkr) + _5mohk7qw );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)2 * _dxpq0xkr) + _aqo9vcb6 );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)2 * _dxpq0xkr) + _8g32if8p );
						_tafa1evd = ((_dxpq0xkr * _dxpq0xkr) + _loujht2t);
						_gghrqcr1 = ((_dxpq0xkr * _dxpq0xkr) + ((int)3 * _dxpq0xkr));
					}
					else
					if (_6ecs6pbo)
					{
						//* 
						//*                 Path 4 (M >> N, JOBZ='A') 
						//* 
						
						_loujht2t = (_dxpq0xkr + _y1ayqutj);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_dxpq0xkr + _c3lnjivp );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)2 * _dxpq0xkr) + _5mohk7qw );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)2 * _dxpq0xkr) + _aqo9vcb6 );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)2 * _dxpq0xkr) + _8g32if8p );
						_tafa1evd = ((_dxpq0xkr * _dxpq0xkr) + _loujht2t);
						_gghrqcr1 = ((_dxpq0xkr * _dxpq0xkr) + ILNumerics.F2NET.Intrinsics.MAX((int)3 * _dxpq0xkr ,_dxpq0xkr + _ev4xhht5 ));
					}
					
				}
				else
				if (_ev4xhht5 >= _0sgoq9v8)
				{
					//* 
					//*              Path 5 (M >> N, but not as much as MNTHR1) 
					//* 
					
					_tafa1evd = (((int)2 * _dxpq0xkr) + _d3z2y722);
					_gghrqcr1 = (((int)2 * _dxpq0xkr) + _ev4xhht5);
					if (_mtv5r2zz)
					{
						//*                 Path 5o (M >> N, JOBZ='O') 
						
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _dxpq0xkr) + _3zm0ngzg );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _dxpq0xkr) + _ncllygen );
						_tafa1evd = (_tafa1evd + (_ev4xhht5 * _dxpq0xkr));
						_gghrqcr1 = (_gghrqcr1 + (_dxpq0xkr * _dxpq0xkr));
					}
					else
					if (_ngfvoqx1)
					{
						//*                 Path 5s (M >> N, JOBZ='S') 
						
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _dxpq0xkr) + _3zm0ngzg );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _dxpq0xkr) + _ncllygen );
					}
					else
					if (_6ecs6pbo)
					{
						//*                 Path 5a (M >> N, JOBZ='A') 
						
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _dxpq0xkr) + _3zm0ngzg );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _dxpq0xkr) + _d6597gvk );
					}
					
				}
				else
				{
					//* 
					//*              Path 6 (M >= N, but not much larger) 
					//* 
					
					_tafa1evd = (((int)2 * _dxpq0xkr) + _d3z2y722);
					_gghrqcr1 = (((int)2 * _dxpq0xkr) + _ev4xhht5);
					if (_mtv5r2zz)
					{
						//*                 Path 6o (M >= N, JOBZ='O') 
						
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _dxpq0xkr) + _8g32if8p );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _dxpq0xkr) + _th9hxrra );
						_tafa1evd = (_tafa1evd + (_ev4xhht5 * _dxpq0xkr));
						_gghrqcr1 = (_gghrqcr1 + (_dxpq0xkr * _dxpq0xkr));
					}
					else
					if (_ngfvoqx1)
					{
						//*                 Path 6s (M >= N, JOBZ='S') 
						
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _dxpq0xkr) + _th9hxrra );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _dxpq0xkr) + _8g32if8p );
					}
					else
					if (_6ecs6pbo)
					{
						//*                 Path 6a (M >= N, JOBZ='A') 
						
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _dxpq0xkr) + _9jxtngt6 );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _dxpq0xkr) + _8g32if8p );
					}
					
				}
				
			}
			else
			if (_qaseb1y7 > (int)0)
			{
				//* 
				//*           There is no complex work space needed for bidiagonal SVD 
				//*           The real work space needed for bidiagonal SVD (dbdsdc) is 
				//*           BDSPAC = 3*M*M + 4*M for singular values and vectors; 
				//*           BDSPAC = 4*M         for singular values only; 
				//*           not including e, RU, and RVT matrices. 
				//* 
				//*           Compute space preferred for each routine 
				
				_w8dh0c16(ref _ev4xhht5 ,ref _dxpq0xkr ,(_n7plx4io+((int)1 - 1)),ref _ev4xhht5 ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_d3z2y722 = ILNumerics.F2NET.Intrinsics.INT(*(_n7plx4io+((int)1 - 1)) );//* 
				
				_w8dh0c16(ref _ev4xhht5 ,ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),ref _ev4xhht5 ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_tbzu075b = ILNumerics.F2NET.Intrinsics.INT(*(_n7plx4io+((int)1 - 1)) );//* 
				
				_nsco3b9r(ref _ev4xhht5 ,ref _dxpq0xkr ,(_n7plx4io+((int)1 - 1)),ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_rmiwyg72 = ILNumerics.F2NET.Intrinsics.INT(*(_n7plx4io+((int)1 - 1)) );//* 
				
				_anznwuhr("P" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_vht8spye = ILNumerics.F2NET.Intrinsics.INT(*(_n7plx4io+((int)1 - 1)) );//* 
				
				_anznwuhr("P" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),ref _dxpq0xkr ,(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_3zm0ngzg = ILNumerics.F2NET.Intrinsics.INT(*(_n7plx4io+((int)1 - 1)) );//* 
				
				_anznwuhr("Q" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,(_n7plx4io+((int)1 - 1)),ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_d6597gvk = ILNumerics.F2NET.Intrinsics.INT(*(_n7plx4io+((int)1 - 1)) );//* 
				
				_fcuznjj5(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_2ewcnsdb = ILNumerics.F2NET.Intrinsics.INT(*(_n7plx4io+((int)1 - 1)) );//* 
				
				_fcuznjj5(ref _dxpq0xkr ,ref _dxpq0xkr ,ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),ref _dxpq0xkr ,(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_ye383gos = ILNumerics.F2NET.Intrinsics.INT(*(_n7plx4io+((int)1 - 1)) );//* 
				
				_2xvhm1xm("P" ,"R" ,"C" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_eb7iuw24 = ILNumerics.F2NET.Intrinsics.INT(*(_n7plx4io+((int)1 - 1)) );//* 
				
				_2xvhm1xm("P" ,"R" ,"C" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_sm7d4hbu = ILNumerics.F2NET.Intrinsics.INT(*(_n7plx4io+((int)1 - 1)) );//* 
				
				_2xvhm1xm("P" ,"R" ,"C" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),ref _dxpq0xkr ,(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),ref _dxpq0xkr ,(_n7plx4io+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_8g32if8p = ILNumerics.F2NET.Intrinsics.INT(*(_n7plx4io+((int)1 - 1)) );//* 
				
				_2xvhm1xm("Q" ,"L" ,"N" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),(_n7plx4io+((int)1 - 1)),ref _ev4xhht5 ,(_n7plx4io+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_9jxtngt6 = ILNumerics.F2NET.Intrinsics.INT(*(_n7plx4io+((int)1 - 1)) );//* 
				
				if (_dxpq0xkr >= _lmbwg5z2)
				{
					
					if (_7y8lc0fu)
					{
						//* 
						//*                 Path 1t (N >> M, JOBZ='N') 
						//* 
						
						_tafa1evd = (_ev4xhht5 + _rmiwyg72);
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _ev4xhht5) + _tbzu075b );
						_gghrqcr1 = ((int)3 * _ev4xhht5);
					}
					else
					if (_mtv5r2zz)
					{
						//* 
						//*                 Path 2t (N >> M, JOBZ='O') 
						//* 
						
						_loujht2t = (_ev4xhht5 + _rmiwyg72);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_ev4xhht5 + _2ewcnsdb );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)2 * _ev4xhht5) + _tbzu075b );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)2 * _ev4xhht5) + _9jxtngt6 );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)2 * _ev4xhht5) + _eb7iuw24 );
						_tafa1evd = (((_ev4xhht5 * _dxpq0xkr) + (_ev4xhht5 * _ev4xhht5)) + _loujht2t);
						_gghrqcr1 = ((((int)2 * _ev4xhht5) * _ev4xhht5) + ((int)3 * _ev4xhht5));
					}
					else
					if (_ngfvoqx1)
					{
						//* 
						//*                 Path 3t (N >> M, JOBZ='S') 
						//* 
						
						_loujht2t = (_ev4xhht5 + _rmiwyg72);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_ev4xhht5 + _2ewcnsdb );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)2 * _ev4xhht5) + _tbzu075b );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)2 * _ev4xhht5) + _9jxtngt6 );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)2 * _ev4xhht5) + _eb7iuw24 );
						_tafa1evd = ((_ev4xhht5 * _ev4xhht5) + _loujht2t);
						_gghrqcr1 = ((_ev4xhht5 * _ev4xhht5) + ((int)3 * _ev4xhht5));
					}
					else
					if (_6ecs6pbo)
					{
						//* 
						//*                 Path 4t (N >> M, JOBZ='A') 
						//* 
						
						_loujht2t = (_ev4xhht5 + _rmiwyg72);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_ev4xhht5 + _ye383gos );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)2 * _ev4xhht5) + _tbzu075b );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)2 * _ev4xhht5) + _9jxtngt6 );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)2 * _ev4xhht5) + _eb7iuw24 );
						_tafa1evd = ((_ev4xhht5 * _ev4xhht5) + _loujht2t);
						_gghrqcr1 = ((_ev4xhht5 * _ev4xhht5) + ILNumerics.F2NET.Intrinsics.MAX((int)3 * _ev4xhht5 ,_ev4xhht5 + _dxpq0xkr ));
					}
					
				}
				else
				if (_dxpq0xkr >= _0sgoq9v8)
				{
					//* 
					//*              Path 5t (N >> M, but not as much as MNTHR1) 
					//* 
					
					_tafa1evd = (((int)2 * _ev4xhht5) + _d3z2y722);
					_gghrqcr1 = (((int)2 * _ev4xhht5) + _dxpq0xkr);
					if (_mtv5r2zz)
					{
						//*                 Path 5to (N >> M, JOBZ='O') 
						
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _ev4xhht5) + _d6597gvk );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _ev4xhht5) + _vht8spye );
						_tafa1evd = (_tafa1evd + (_ev4xhht5 * _dxpq0xkr));
						_gghrqcr1 = (_gghrqcr1 + (_ev4xhht5 * _ev4xhht5));
					}
					else
					if (_ngfvoqx1)
					{
						//*                 Path 5ts (N >> M, JOBZ='S') 
						
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _ev4xhht5) + _d6597gvk );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _ev4xhht5) + _vht8spye );
					}
					else
					if (_6ecs6pbo)
					{
						//*                 Path 5ta (N >> M, JOBZ='A') 
						
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _ev4xhht5) + _d6597gvk );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _ev4xhht5) + _3zm0ngzg );
					}
					
				}
				else
				{
					//* 
					//*              Path 6t (N > M, but not much larger) 
					//* 
					
					_tafa1evd = (((int)2 * _ev4xhht5) + _d3z2y722);
					_gghrqcr1 = (((int)2 * _ev4xhht5) + _dxpq0xkr);
					if (_mtv5r2zz)
					{
						//*                 Path 6to (N > M, JOBZ='O') 
						
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _ev4xhht5) + _9jxtngt6 );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _ev4xhht5) + _sm7d4hbu );
						_tafa1evd = (_tafa1evd + (_ev4xhht5 * _dxpq0xkr));
						_gghrqcr1 = (_gghrqcr1 + (_ev4xhht5 * _ev4xhht5));
					}
					else
					if (_ngfvoqx1)
					{
						//*                 Path 6ts (N > M, JOBZ='S') 
						
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _ev4xhht5) + _9jxtngt6 );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _ev4xhht5) + _sm7d4hbu );
					}
					else
					if (_6ecs6pbo)
					{
						//*                 Path 6ta (N > M, JOBZ='A') 
						
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _ev4xhht5) + _9jxtngt6 );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _ev4xhht5) + _8g32if8p );
					}
					
				}
				
			}
			
			_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,_gghrqcr1 );
		}
		
		if (_gro5yvfo == (int)0)
		{
			
			*(_apig8meb+((int)1 - 1)) = DCMPLX(_tafa1evd);
			if ((_6fnxzlyp < _gghrqcr1) & (!(_lhlgm7z5)))
			{
				
				_gro5yvfo = (int)-12;
			}
			
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("ZGESDD" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		else
		if (_lhlgm7z5)
		{
			
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if ((_ev4xhht5 == (int)0) | (_dxpq0xkr == (int)0))
		{
			
			return;
		}
		//* 
		//*     Get machine constants 
		//* 
		
		_p1iqarg6 = _f43eg0w0("P" );
		_bogm0gwy = (ILNumerics.F2NET.Intrinsics.SQRT(_f43eg0w0("S" ) ) / _p1iqarg6);
		_av7j8yda = (_kxg5drh2 / _bogm0gwy);//* 
		//*     Scale A if max element outside range [SMLNUM,BIGNUM] 
		//* 
		
		_j6vjow1g = _o615qv2q("M" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_g7qb61ha );
		_65mv5f5m = (int)0;
		if ((_j6vjow1g > _d0547bi2) & (_j6vjow1g < _bogm0gwy))
		{
			
			_65mv5f5m = (int)1;
			_j6h8q4u5("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _j6vjow1g ,ref _bogm0gwy ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,ref _bhsiylw4 );
		}
		else
		if (_j6vjow1g > _av7j8yda)
		{
			
			_65mv5f5m = (int)1;
			_j6h8q4u5("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _j6vjow1g ,ref _av7j8yda ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,ref _bhsiylw4 );
		}
		//* 
		
		if (_ev4xhht5 >= _dxpq0xkr)
		{
			//* 
			//*        A has at least as many rows as columns. If A has sufficiently 
			//*        more rows than columns, first reduce using the QR 
			//*        decomposition (if sufficient workspace available) 
			//* 
			
			if (_ev4xhht5 >= _lmbwg5z2)
			{
				//* 
				
				if (_7y8lc0fu)
				{
					//* 
					//*              Path 1 (M >> N, JOBZ='N') 
					//*              No singular vectors to be computed 
					//* 
					
					_q1w15vsx = (int)1;
					_1myocm5q = (_q1w15vsx + _dxpq0xkr);//* 
					//*              Compute A=Q*R 
					//*              CWorkspace: need   N [tau] + N    [work] 
					//*              CWorkspace: prefer N [tau] + N*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_ljflx7wo(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Zero out below R 
					//* 
					
					_k14i9nd8("L" ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_gdjumcqt) ,(_vxfgpup9+((int)2 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
					_smxeww0r = (int)1;
					_wx1x93f0 = (int)1;
					_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
					_1myocm5q = (_5ke1jwwr + _dxpq0xkr);//* 
					//*              Bidiagonalize R in A 
					//*              CWorkspace: need   2*N [tauq, taup] + N      [work] 
					//*              CWorkspace: prefer 2*N [tauq, taup] + 2*N*NB [work] 
					//*              RWorkspace: need   N [e] 
					//* 
					
					_w8dh0c16(ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
					_r49fp4o3 = (_smxeww0r + _dxpq0xkr);//* 
					//*              Perform bidiagonal SVD, compute singular values only 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   N [e] + BDSPAC 
					//* 
					
					_c3rn9m7n("U" ,"N" ,ref _dxpq0xkr ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,_xpllja47 ,(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					
				}
				else
				if (_mtv5r2zz)
				{
					//* 
					//*              Path 2 (M >> N, JOBZ='O') 
					//*              N left singular vectors to be overwritten on A and 
					//*              N right singular vectors to be computed in VT 
					//* 
					
					_j4l29b9c = (int)1;//* 
					//*              WORK(IU) is N by N 
					//* 
					
					_lbvz22dx = _dxpq0xkr;
					_m9w6lk7x = (_j4l29b9c + (_lbvz22dx * _dxpq0xkr));
					if (_6fnxzlyp >= (((_ev4xhht5 * _dxpq0xkr) + (_dxpq0xkr * _dxpq0xkr)) + ((int)3 * _dxpq0xkr)))
					{
						//* 
						//*                 WORK(IR) is M by N 
						//* 
						
						_v6sbkzy4 = _ev4xhht5;
					}
					else
					{
						
						_v6sbkzy4 = (((_6fnxzlyp - (_dxpq0xkr * _dxpq0xkr)) - ((int)3 * _dxpq0xkr)) / _dxpq0xkr);
					}
					
					_q1w15vsx = (_m9w6lk7x + (_v6sbkzy4 * _dxpq0xkr));
					_1myocm5q = (_q1w15vsx + _dxpq0xkr);//* 
					//*              Compute A=Q*R 
					//*              CWorkspace: need   N*N [U] + N*N [R] + N [tau] + N    [work] 
					//*              CWorkspace: prefer N*N [U] + N*N [R] + N [tau] + N*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_ljflx7wo(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Copy R to WORK( IR ), zeroing out below it 
					//* 
					
					_nihu9ses("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 );
					_k14i9nd8("L" ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_gdjumcqt) ,(_apig8meb+(_m9w6lk7x + (int)1 - 1)),ref _v6sbkzy4 );//* 
					//*              Generate Q in A 
					//*              CWorkspace: need   N*N [U] + N*N [R] + N [tau] + N    [work] 
					//*              CWorkspace: prefer N*N [U] + N*N [R] + N [tau] + N*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_13b6etkp(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
					_smxeww0r = (int)1;
					_wx1x93f0 = _q1w15vsx;
					_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
					_1myocm5q = (_5ke1jwwr + _dxpq0xkr);//* 
					//*              Bidiagonalize R in WORK(IR) 
					//*              CWorkspace: need   N*N [U] + N*N [R] + 2*N [tauq, taup] + N      [work] 
					//*              CWorkspace: prefer N*N [U] + N*N [R] + 2*N [tauq, taup] + 2*N*NB [work] 
					//*              RWorkspace: need   N [e] 
					//* 
					
					_w8dh0c16(ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of R in WORK(IRU) and computing right singular vectors 
					//*              of R in WORK(IRVT) 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   N [e] + N*N [RU] + N*N [RVT] + BDSPAC 
					//* 
					
					_n6v1ono2 = (_smxeww0r + _dxpq0xkr);
					_emo79hqt = (_n6v1ono2 + (_dxpq0xkr * _dxpq0xkr));
					_r49fp4o3 = (_emo79hqt + (_dxpq0xkr * _dxpq0xkr));
					_c3rn9m7n("U" ,"I" ,ref _dxpq0xkr ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_dqanbbw3+(_n6v1ono2 - 1)),ref _dxpq0xkr ,(_dqanbbw3+(_emo79hqt - 1)),ref _dxpq0xkr ,_g7qb61ha ,_xpllja47 ,(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Copy real matrix RWORK(IRU) to complex matrix WORK(IU) 
					//*              Overwrite WORK(IU) by the left singular vectors of R 
					//*              CWorkspace: need   N*N [U] + N*N [R] + 2*N [tauq, taup] + N    [work] 
					//*              CWorkspace: prefer N*N [U] + N*N [R] + 2*N [tauq, taup] + N*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_nz2p9162("F" ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_dqanbbw3+(_n6v1ono2 - 1)),ref _dxpq0xkr ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx );
					_2xvhm1xm("Q" ,"L" ,"N" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Copy real matrix RWORK(IRVT) to complex matrix VT 
					//*              Overwrite VT by the right singular vectors of R 
					//*              CWorkspace: need   N*N [U] + N*N [R] + 2*N [tauq, taup] + N    [work] 
					//*              CWorkspace: prefer N*N [U] + N*N [R] + 2*N [tauq, taup] + N*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_nz2p9162("F" ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_dqanbbw3+(_emo79hqt - 1)),ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv );
					_2xvhm1xm("P" ,"R" ,"C" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_apig8meb+(_5ke1jwwr - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Multiply Q in A by left singular vectors of R in 
					//*              WORK(IU), storing result in WORK(IR) and copying to A 
					//*              CWorkspace: need   N*N [U] + N*N [R] 
					//*              CWorkspace: prefer N*N [U] + M*N [R] 
					//*              RWorkspace: need   0 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn1129 = (System.Int32)((int)1);
						System.Int32 __81fgg2step1129 = (System.Int32)(_v6sbkzy4);
						System.Int32 __81fgg2count1129;
						for (__81fgg2count1129 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1129 + __81fgg2step1129) / __81fgg2step1129)), _b5p6od9s = __81fgg2dlsvn1129; __81fgg2count1129 != 0; __81fgg2count1129--, _b5p6od9s += (__81fgg2step1129)) {

						{
							
							_8rfn4f7g = ILNumerics.F2NET.Intrinsics.MIN((_ev4xhht5 - _b5p6od9s) + (int)1 ,_v6sbkzy4 );
							_xos1d1er("N" ,"N" ,ref _8rfn4f7g ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_40vhxf9f) ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,ref Unsafe.AsRef(_gdjumcqt) ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 );
							_nihu9ses("F" ,ref _8rfn4f7g ,ref _dxpq0xkr ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
Mark10:;
							// continue
						}
												}					}//* 
					
				}
				else
				if (_ngfvoqx1)
				{
					//* 
					//*              Path 3 (M >> N, JOBZ='S') 
					//*              N left singular vectors to be computed in U and 
					//*              N right singular vectors to be computed in VT 
					//* 
					
					_m9w6lk7x = (int)1;//* 
					//*              WORK(IR) is N by N 
					//* 
					
					_v6sbkzy4 = _dxpq0xkr;
					_q1w15vsx = (_m9w6lk7x + (_v6sbkzy4 * _dxpq0xkr));
					_1myocm5q = (_q1w15vsx + _dxpq0xkr);//* 
					//*              Compute A=Q*R 
					//*              CWorkspace: need   N*N [R] + N [tau] + N    [work] 
					//*              CWorkspace: prefer N*N [R] + N [tau] + N*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_ljflx7wo(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Copy R to WORK(IR), zeroing out below it 
					//* 
					
					_nihu9ses("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 );
					_k14i9nd8("L" ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_gdjumcqt) ,(_apig8meb+(_m9w6lk7x + (int)1 - 1)),ref _v6sbkzy4 );//* 
					//*              Generate Q in A 
					//*              CWorkspace: need   N*N [R] + N [tau] + N    [work] 
					//*              CWorkspace: prefer N*N [R] + N [tau] + N*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_13b6etkp(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
					_smxeww0r = (int)1;
					_wx1x93f0 = _q1w15vsx;
					_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
					_1myocm5q = (_5ke1jwwr + _dxpq0xkr);//* 
					//*              Bidiagonalize R in WORK(IR) 
					//*              CWorkspace: need   N*N [R] + 2*N [tauq, taup] + N      [work] 
					//*              CWorkspace: prefer N*N [R] + 2*N [tauq, taup] + 2*N*NB [work] 
					//*              RWorkspace: need   N [e] 
					//* 
					
					_w8dh0c16(ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in RWORK(IRU) and computing right 
					//*              singular vectors of bidiagonal matrix in RWORK(IRVT) 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   N [e] + N*N [RU] + N*N [RVT] + BDSPAC 
					//* 
					
					_n6v1ono2 = (_smxeww0r + _dxpq0xkr);
					_emo79hqt = (_n6v1ono2 + (_dxpq0xkr * _dxpq0xkr));
					_r49fp4o3 = (_emo79hqt + (_dxpq0xkr * _dxpq0xkr));
					_c3rn9m7n("U" ,"I" ,ref _dxpq0xkr ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_dqanbbw3+(_n6v1ono2 - 1)),ref _dxpq0xkr ,(_dqanbbw3+(_emo79hqt - 1)),ref _dxpq0xkr ,_g7qb61ha ,_xpllja47 ,(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Copy real matrix RWORK(IRU) to complex matrix U 
					//*              Overwrite U by left singular vectors of R 
					//*              CWorkspace: need   N*N [R] + 2*N [tauq, taup] + N    [work] 
					//*              CWorkspace: prefer N*N [R] + 2*N [tauq, taup] + N*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_nz2p9162("F" ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_dqanbbw3+(_n6v1ono2 - 1)),ref _dxpq0xkr ,_7u55mqkq ,ref _u6e6d39b );
					_2xvhm1xm("Q" ,"L" ,"N" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_apig8meb+(_wx1x93f0 - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Copy real matrix RWORK(IRVT) to complex matrix VT 
					//*              Overwrite VT by right singular vectors of R 
					//*              CWorkspace: need   N*N [R] + 2*N [tauq, taup] + N    [work] 
					//*              CWorkspace: prefer N*N [R] + 2*N [tauq, taup] + N*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_nz2p9162("F" ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_dqanbbw3+(_emo79hqt - 1)),ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv );
					_2xvhm1xm("P" ,"R" ,"C" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_apig8meb+(_5ke1jwwr - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Multiply Q in A by left singular vectors of R in 
					//*              WORK(IR), storing result in U 
					//*              CWorkspace: need   N*N [R] 
					//*              RWorkspace: need   0 
					//* 
					
					_nihu9ses("F" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 );
					_xos1d1er("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_40vhxf9f) ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,ref Unsafe.AsRef(_gdjumcqt) ,_7u55mqkq ,ref _u6e6d39b );//* 
					
				}
				else
				if (_6ecs6pbo)
				{
					//* 
					//*              Path 4 (M >> N, JOBZ='A') 
					//*              M left singular vectors to be computed in U and 
					//*              N right singular vectors to be computed in VT 
					//* 
					
					_j4l29b9c = (int)1;//* 
					//*              WORK(IU) is N by N 
					//* 
					
					_lbvz22dx = _dxpq0xkr;
					_q1w15vsx = (_j4l29b9c + (_lbvz22dx * _dxpq0xkr));
					_1myocm5q = (_q1w15vsx + _dxpq0xkr);//* 
					//*              Compute A=Q*R, copying result to U 
					//*              CWorkspace: need   N*N [U] + N [tau] + N    [work] 
					//*              CWorkspace: prefer N*N [U] + N [tau] + N*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_ljflx7wo(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
					_nihu9ses("L" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );//* 
					//*              Generate Q in U 
					//*              CWorkspace: need   N*N [U] + N [tau] + M    [work] 
					//*              CWorkspace: prefer N*N [U] + N [tau] + M*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_13b6etkp(ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Produce R in A, zeroing out below it 
					//* 
					
					_k14i9nd8("L" ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_gdjumcqt) ,(_vxfgpup9+((int)2 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
					_smxeww0r = (int)1;
					_wx1x93f0 = _q1w15vsx;
					_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
					_1myocm5q = (_5ke1jwwr + _dxpq0xkr);//* 
					//*              Bidiagonalize R in A 
					//*              CWorkspace: need   N*N [U] + 2*N [tauq, taup] + N      [work] 
					//*              CWorkspace: prefer N*N [U] + 2*N [tauq, taup] + 2*N*NB [work] 
					//*              RWorkspace: need   N [e] 
					//* 
					
					_w8dh0c16(ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
					_n6v1ono2 = (_smxeww0r + _dxpq0xkr);
					_emo79hqt = (_n6v1ono2 + (_dxpq0xkr * _dxpq0xkr));
					_r49fp4o3 = (_emo79hqt + (_dxpq0xkr * _dxpq0xkr));//* 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in RWORK(IRU) and computing right 
					//*              singular vectors of bidiagonal matrix in RWORK(IRVT) 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   N [e] + N*N [RU] + N*N [RVT] + BDSPAC 
					//* 
					
					_c3rn9m7n("U" ,"I" ,ref _dxpq0xkr ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_dqanbbw3+(_n6v1ono2 - 1)),ref _dxpq0xkr ,(_dqanbbw3+(_emo79hqt - 1)),ref _dxpq0xkr ,_g7qb61ha ,_xpllja47 ,(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Copy real matrix RWORK(IRU) to complex matrix WORK(IU) 
					//*              Overwrite WORK(IU) by left singular vectors of R 
					//*              CWorkspace: need   N*N [U] + 2*N [tauq, taup] + N    [work] 
					//*              CWorkspace: prefer N*N [U] + 2*N [tauq, taup] + N*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_nz2p9162("F" ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_dqanbbw3+(_n6v1ono2 - 1)),ref _dxpq0xkr ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx );
					_2xvhm1xm("Q" ,"L" ,"N" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Copy real matrix RWORK(IRVT) to complex matrix VT 
					//*              Overwrite VT by right singular vectors of R 
					//*              CWorkspace: need   N*N [U] + 2*N [tauq, taup] + N    [work] 
					//*              CWorkspace: prefer N*N [U] + 2*N [tauq, taup] + N*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_nz2p9162("F" ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_dqanbbw3+(_emo79hqt - 1)),ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv );
					_2xvhm1xm("P" ,"R" ,"C" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Multiply Q in U by left singular vectors of R in 
					//*              WORK(IU), storing result in A 
					//*              CWorkspace: need   N*N [U] 
					//*              RWorkspace: need   0 
					//* 
					
					_xos1d1er("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_40vhxf9f) ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,ref Unsafe.AsRef(_gdjumcqt) ,_vxfgpup9 ,ref _ocv8fk5c );//* 
					//*              Copy left singular vectors of A from A to U 
					//* 
					
					_nihu9ses("F" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );//* 
					
				}
				//* 
				
			}
			else
			if (_ev4xhht5 >= _0sgoq9v8)
			{
				//* 
				//*           MNTHR2 <= M < MNTHR1 
				//* 
				//*           Path 5 (M >> N, but not as much as MNTHR1) 
				//*           Reduce to bidiagonal form without QR decomposition, use 
				//*           ZUNGBR and matrix multiplication to compute singular vectors 
				//* 
				
				_smxeww0r = (int)1;
				_r49fp4o3 = (_smxeww0r + _dxpq0xkr);
				_wx1x93f0 = (int)1;
				_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
				_1myocm5q = (_5ke1jwwr + _dxpq0xkr);//* 
				//*           Bidiagonalize A 
				//*           CWorkspace: need   2*N [tauq, taup] + M        [work] 
				//*           CWorkspace: prefer 2*N [tauq, taup] + (M+N)*NB [work] 
				//*           RWorkspace: need   N [e] 
				//* 
				
				_w8dh0c16(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
				if (_7y8lc0fu)
				{
					//* 
					//*              Path 5n (M >> N, JOBZ='N') 
					//*              Compute singular values only 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   N [e] + BDSPAC 
					//* 
					
					_c3rn9m7n("U" ,"N" ,ref _dxpq0xkr ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,_xpllja47 ,(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );
				}
				else
				if (_mtv5r2zz)
				{
					
					_j4l29b9c = _1myocm5q;
					_n6v1ono2 = _r49fp4o3;
					_emo79hqt = (_n6v1ono2 + (_dxpq0xkr * _dxpq0xkr));
					_r49fp4o3 = (_emo79hqt + (_dxpq0xkr * _dxpq0xkr));//* 
					//*              Path 5o (M >> N, JOBZ='O') 
					//*              Copy A to VT, generate P**H 
					//*              CWorkspace: need   2*N [tauq, taup] + N    [work] 
					//*              CWorkspace: prefer 2*N [tauq, taup] + N*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_nihu9ses("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );
					_anznwuhr("P" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Generate Q in A 
					//*              CWorkspace: need   2*N [tauq, taup] + N    [work] 
					//*              CWorkspace: prefer 2*N [tauq, taup] + N*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_anznwuhr("Q" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					
					if (_6fnxzlyp >= ((_ev4xhht5 * _dxpq0xkr) + ((int)3 * _dxpq0xkr)))
					{
						//* 
						//*                 WORK( IU ) is M by N 
						//* 
						
						_lbvz22dx = _ev4xhht5;
					}
					else
					{
						//* 
						//*                 WORK(IU) is LDWRKU by N 
						//* 
						
						_lbvz22dx = ((_6fnxzlyp - ((int)3 * _dxpq0xkr)) / _dxpq0xkr);
					}
					
					_1myocm5q = (_j4l29b9c + (_lbvz22dx * _dxpq0xkr));//* 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in RWORK(IRU) and computing right 
					//*              singular vectors of bidiagonal matrix in RWORK(IRVT) 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   N [e] + N*N [RU] + N*N [RVT] + BDSPAC 
					//* 
					
					_c3rn9m7n("U" ,"I" ,ref _dxpq0xkr ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_dqanbbw3+(_n6v1ono2 - 1)),ref _dxpq0xkr ,(_dqanbbw3+(_emo79hqt - 1)),ref _dxpq0xkr ,_g7qb61ha ,_xpllja47 ,(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Multiply real matrix RWORK(IRVT) by P**H in VT, 
					//*              storing the result in WORK(IU), copying to VT 
					//*              CWorkspace: need   2*N [tauq, taup] + N*N [U] 
					//*              RWorkspace: need   N [e] + N*N [RU] + N*N [RVT] + 2*N*N [rwork] 
					//* 
					
					_zwe74bak(ref _dxpq0xkr ,ref _dxpq0xkr ,(_dqanbbw3+(_emo79hqt - 1)),ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_dqanbbw3+(_r49fp4o3 - 1)));
					_nihu9ses("F" ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,_xdbczr8u ,ref _h4ibbatv );//* 
					//*              Multiply Q in A by real matrix RWORK(IRU), storing the 
					//*              result in WORK(IU), copying to A 
					//*              CWorkspace: need   2*N [tauq, taup] + N*N [U] 
					//*              CWorkspace: prefer 2*N [tauq, taup] + M*N [U] 
					//*              RWorkspace: need   N [e] + N*N [RU] + 2*N*N [rwork] 
					//*              RWorkspace: prefer N [e] + N*N [RU] + 2*M*N [rwork] < N + 5*N*N since M < 2*N here 
					//* 
					
					_r49fp4o3 = _emo79hqt;
					{
						System.Int32 __81fgg2dlsvn1130 = (System.Int32)((int)1);
						System.Int32 __81fgg2step1130 = (System.Int32)(_lbvz22dx);
						System.Int32 __81fgg2count1130;
						for (__81fgg2count1130 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1130 + __81fgg2step1130) / __81fgg2step1130)), _b5p6od9s = __81fgg2dlsvn1130; __81fgg2count1130 != 0; __81fgg2count1130--, _b5p6od9s += (__81fgg2step1130)) {

						{
							
							_8rfn4f7g = ILNumerics.F2NET.Intrinsics.MIN((_ev4xhht5 - _b5p6od9s) + (int)1 ,_lbvz22dx );
							_szowpa5a(ref _8rfn4f7g ,ref _dxpq0xkr ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_dqanbbw3+(_n6v1ono2 - 1)),ref _dxpq0xkr ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_dqanbbw3+(_r49fp4o3 - 1)));
							_nihu9ses("F" ,ref _8rfn4f7g ,ref _dxpq0xkr ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
Mark20:;
							// continue
						}
												}					}//* 
					
				}
				else
				if (_ngfvoqx1)
				{
					//* 
					//*              Path 5s (M >> N, JOBZ='S') 
					//*              Copy A to VT, generate P**H 
					//*              CWorkspace: need   2*N [tauq, taup] + N    [work] 
					//*              CWorkspace: prefer 2*N [tauq, taup] + N*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_nihu9ses("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );
					_anznwuhr("P" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Copy A to U, generate Q 
					//*              CWorkspace: need   2*N [tauq, taup] + N    [work] 
					//*              CWorkspace: prefer 2*N [tauq, taup] + N*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_nihu9ses("L" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );
					_anznwuhr("Q" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in RWORK(IRU) and computing right 
					//*              singular vectors of bidiagonal matrix in RWORK(IRVT) 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   N [e] + N*N [RU] + N*N [RVT] + BDSPAC 
					//* 
					
					_n6v1ono2 = _r49fp4o3;
					_emo79hqt = (_n6v1ono2 + (_dxpq0xkr * _dxpq0xkr));
					_r49fp4o3 = (_emo79hqt + (_dxpq0xkr * _dxpq0xkr));
					_c3rn9m7n("U" ,"I" ,ref _dxpq0xkr ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_dqanbbw3+(_n6v1ono2 - 1)),ref _dxpq0xkr ,(_dqanbbw3+(_emo79hqt - 1)),ref _dxpq0xkr ,_g7qb61ha ,_xpllja47 ,(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Multiply real matrix RWORK(IRVT) by P**H in VT, 
					//*              storing the result in A, copying to VT 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   N [e] + N*N [RU] + N*N [RVT] + 2*N*N [rwork] 
					//* 
					
					_zwe74bak(ref _dxpq0xkr ,ref _dxpq0xkr ,(_dqanbbw3+(_emo79hqt - 1)),ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv ,_vxfgpup9 ,ref _ocv8fk5c ,(_dqanbbw3+(_r49fp4o3 - 1)));
					_nihu9ses("F" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );//* 
					//*              Multiply Q in U by real matrix RWORK(IRU), storing the 
					//*              result in A, copying to U 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   N [e] + N*N [RU] + 2*M*N [rwork] < N + 5*N*N since M < 2*N here 
					//* 
					
					_r49fp4o3 = _emo79hqt;
					_szowpa5a(ref _ev4xhht5 ,ref _dxpq0xkr ,_7u55mqkq ,ref _u6e6d39b ,(_dqanbbw3+(_n6v1ono2 - 1)),ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_dqanbbw3+(_r49fp4o3 - 1)));
					_nihu9ses("F" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );
				}
				else
				{
					//* 
					//*              Path 5a (M >> N, JOBZ='A') 
					//*              Copy A to VT, generate P**H 
					//*              CWorkspace: need   2*N [tauq, taup] + N    [work] 
					//*              CWorkspace: prefer 2*N [tauq, taup] + N*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_nihu9ses("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );
					_anznwuhr("P" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Copy A to U, generate Q 
					//*              CWorkspace: need   2*N [tauq, taup] + M    [work] 
					//*              CWorkspace: prefer 2*N [tauq, taup] + M*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_nihu9ses("L" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );
					_anznwuhr("Q" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in RWORK(IRU) and computing right 
					//*              singular vectors of bidiagonal matrix in RWORK(IRVT) 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   N [e] + N*N [RU] + N*N [RVT] + BDSPAC 
					//* 
					
					_n6v1ono2 = _r49fp4o3;
					_emo79hqt = (_n6v1ono2 + (_dxpq0xkr * _dxpq0xkr));
					_r49fp4o3 = (_emo79hqt + (_dxpq0xkr * _dxpq0xkr));
					_c3rn9m7n("U" ,"I" ,ref _dxpq0xkr ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_dqanbbw3+(_n6v1ono2 - 1)),ref _dxpq0xkr ,(_dqanbbw3+(_emo79hqt - 1)),ref _dxpq0xkr ,_g7qb61ha ,_xpllja47 ,(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Multiply real matrix RWORK(IRVT) by P**H in VT, 
					//*              storing the result in A, copying to VT 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   N [e] + N*N [RU] + N*N [RVT] + 2*N*N [rwork] 
					//* 
					
					_zwe74bak(ref _dxpq0xkr ,ref _dxpq0xkr ,(_dqanbbw3+(_emo79hqt - 1)),ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv ,_vxfgpup9 ,ref _ocv8fk5c ,(_dqanbbw3+(_r49fp4o3 - 1)));
					_nihu9ses("F" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );//* 
					//*              Multiply Q in U by real matrix RWORK(IRU), storing the 
					//*              result in A, copying to U 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   N [e] + N*N [RU] + 2*M*N [rwork] < N + 5*N*N since M < 2*N here 
					//* 
					
					_r49fp4o3 = _emo79hqt;
					_szowpa5a(ref _ev4xhht5 ,ref _dxpq0xkr ,_7u55mqkq ,ref _u6e6d39b ,(_dqanbbw3+(_n6v1ono2 - 1)),ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_dqanbbw3+(_r49fp4o3 - 1)));
					_nihu9ses("F" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );
				}
				//* 
				
			}
			else
			{
				//* 
				//*           M .LT. MNTHR2 
				//* 
				//*           Path 6 (M >= N, but not much larger) 
				//*           Reduce to bidiagonal form without QR decomposition 
				//*           Use ZUNMBR to compute singular vectors 
				//* 
				
				_smxeww0r = (int)1;
				_r49fp4o3 = (_smxeww0r + _dxpq0xkr);
				_wx1x93f0 = (int)1;
				_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
				_1myocm5q = (_5ke1jwwr + _dxpq0xkr);//* 
				//*           Bidiagonalize A 
				//*           CWorkspace: need   2*N [tauq, taup] + M        [work] 
				//*           CWorkspace: prefer 2*N [tauq, taup] + (M+N)*NB [work] 
				//*           RWorkspace: need   N [e] 
				//* 
				
				_w8dh0c16(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
				if (_7y8lc0fu)
				{
					//* 
					//*              Path 6n (M >= N, JOBZ='N') 
					//*              Compute singular values only 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   N [e] + BDSPAC 
					//* 
					
					_c3rn9m7n("U" ,"N" ,ref _dxpq0xkr ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,_xpllja47 ,(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );
				}
				else
				if (_mtv5r2zz)
				{
					
					_j4l29b9c = _1myocm5q;
					_n6v1ono2 = _r49fp4o3;
					_emo79hqt = (_n6v1ono2 + (_dxpq0xkr * _dxpq0xkr));
					_r49fp4o3 = (_emo79hqt + (_dxpq0xkr * _dxpq0xkr));
					if (_6fnxzlyp >= ((_ev4xhht5 * _dxpq0xkr) + ((int)3 * _dxpq0xkr)))
					{
						//* 
						//*                 WORK( IU ) is M by N 
						//* 
						
						_lbvz22dx = _ev4xhht5;
					}
					else
					{
						//* 
						//*                 WORK( IU ) is LDWRKU by N 
						//* 
						
						_lbvz22dx = ((_6fnxzlyp - ((int)3 * _dxpq0xkr)) / _dxpq0xkr);
					}
					
					_1myocm5q = (_j4l29b9c + (_lbvz22dx * _dxpq0xkr));//* 
					//*              Path 6o (M >= N, JOBZ='O') 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in RWORK(IRU) and computing right 
					//*              singular vectors of bidiagonal matrix in RWORK(IRVT) 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   N [e] + N*N [RU] + N*N [RVT] + BDSPAC 
					//* 
					
					_c3rn9m7n("U" ,"I" ,ref _dxpq0xkr ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_dqanbbw3+(_n6v1ono2 - 1)),ref _dxpq0xkr ,(_dqanbbw3+(_emo79hqt - 1)),ref _dxpq0xkr ,_g7qb61ha ,_xpllja47 ,(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Copy real matrix RWORK(IRVT) to complex matrix VT 
					//*              Overwrite VT by right singular vectors of A 
					//*              CWorkspace: need   2*N [tauq, taup] + N*N [U] + N    [work] 
					//*              CWorkspace: prefer 2*N [tauq, taup] + N*N [U] + N*NB [work] 
					//*              RWorkspace: need   N [e] + N*N [RU] + N*N [RVT] 
					//* 
					
					_nz2p9162("F" ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_dqanbbw3+(_emo79hqt - 1)),ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv );
					_2xvhm1xm("P" ,"R" ,"C" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					
					if (_6fnxzlyp >= ((_ev4xhht5 * _dxpq0xkr) + ((int)3 * _dxpq0xkr)))
					{
						//* 
						//*                 Path 6o-fast 
						//*                 Copy real matrix RWORK(IRU) to complex matrix WORK(IU) 
						//*                 Overwrite WORK(IU) by left singular vectors of A, copying 
						//*                 to A 
						//*                 CWorkspace: need   2*N [tauq, taup] + M*N [U] + N    [work] 
						//*                 CWorkspace: prefer 2*N [tauq, taup] + M*N [U] + N*NB [work] 
						//*                 RWorkspace: need   N [e] + N*N [RU] 
						//* 
						
						_k14i9nd8("F" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_gdjumcqt) ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx );
						_nz2p9162("F" ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_dqanbbw3+(_n6v1ono2 - 1)),ref _dxpq0xkr ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx );
						_2xvhm1xm("Q" ,"L" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
						_nihu9ses("F" ,ref _ev4xhht5 ,ref _dxpq0xkr ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,_vxfgpup9 ,ref _ocv8fk5c );
					}
					else
					{
						//* 
						//*                 Path 6o-slow 
						//*                 Generate Q in A 
						//*                 CWorkspace: need   2*N [tauq, taup] + N*N [U] + N    [work] 
						//*                 CWorkspace: prefer 2*N [tauq, taup] + N*N [U] + N*NB [work] 
						//*                 RWorkspace: need   0 
						//* 
						
						_anznwuhr("Q" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
						//*                 Multiply Q in A by real matrix RWORK(IRU), storing the 
						//*                 result in WORK(IU), copying to A 
						//*                 CWorkspace: need   2*N [tauq, taup] + N*N [U] 
						//*                 CWorkspace: prefer 2*N [tauq, taup] + M*N [U] 
						//*                 RWorkspace: need   N [e] + N*N [RU] + 2*N*N [rwork] 
						//*                 RWorkspace: prefer N [e] + N*N [RU] + 2*M*N [rwork] < N + 5*N*N since M < 2*N here 
						//* 
						
						_r49fp4o3 = _emo79hqt;
						{
							System.Int32 __81fgg2dlsvn1131 = (System.Int32)((int)1);
							System.Int32 __81fgg2step1131 = (System.Int32)(_lbvz22dx);
							System.Int32 __81fgg2count1131;
							for (__81fgg2count1131 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1131 + __81fgg2step1131) / __81fgg2step1131)), _b5p6od9s = __81fgg2dlsvn1131; __81fgg2count1131 != 0; __81fgg2count1131--, _b5p6od9s += (__81fgg2step1131)) {

							{
								
								_8rfn4f7g = ILNumerics.F2NET.Intrinsics.MIN((_ev4xhht5 - _b5p6od9s) + (int)1 ,_lbvz22dx );
								_szowpa5a(ref _8rfn4f7g ,ref _dxpq0xkr ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_dqanbbw3+(_n6v1ono2 - 1)),ref _dxpq0xkr ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_dqanbbw3+(_r49fp4o3 - 1)));
								_nihu9ses("F" ,ref _8rfn4f7g ,ref _dxpq0xkr ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
Mark30:;
								// continue
							}
														}						}
					}
					//* 
					
				}
				else
				if (_ngfvoqx1)
				{
					//* 
					//*              Path 6s (M >= N, JOBZ='S') 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in RWORK(IRU) and computing right 
					//*              singular vectors of bidiagonal matrix in RWORK(IRVT) 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   N [e] + N*N [RU] + N*N [RVT] + BDSPAC 
					//* 
					
					_n6v1ono2 = _r49fp4o3;
					_emo79hqt = (_n6v1ono2 + (_dxpq0xkr * _dxpq0xkr));
					_r49fp4o3 = (_emo79hqt + (_dxpq0xkr * _dxpq0xkr));
					_c3rn9m7n("U" ,"I" ,ref _dxpq0xkr ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_dqanbbw3+(_n6v1ono2 - 1)),ref _dxpq0xkr ,(_dqanbbw3+(_emo79hqt - 1)),ref _dxpq0xkr ,_g7qb61ha ,_xpllja47 ,(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Copy real matrix RWORK(IRU) to complex matrix U 
					//*              Overwrite U by left singular vectors of A 
					//*              CWorkspace: need   2*N [tauq, taup] + N    [work] 
					//*              CWorkspace: prefer 2*N [tauq, taup] + N*NB [work] 
					//*              RWorkspace: need   N [e] + N*N [RU] + N*N [RVT] 
					//* 
					
					_k14i9nd8("F" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_gdjumcqt) ,_7u55mqkq ,ref _u6e6d39b );
					_nz2p9162("F" ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_dqanbbw3+(_n6v1ono2 - 1)),ref _dxpq0xkr ,_7u55mqkq ,ref _u6e6d39b );
					_2xvhm1xm("Q" ,"L" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Copy real matrix RWORK(IRVT) to complex matrix VT 
					//*              Overwrite VT by right singular vectors of A 
					//*              CWorkspace: need   2*N [tauq, taup] + N    [work] 
					//*              CWorkspace: prefer 2*N [tauq, taup] + N*NB [work] 
					//*              RWorkspace: need   N [e] + N*N [RU] + N*N [RVT] 
					//* 
					
					_nz2p9162("F" ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_dqanbbw3+(_emo79hqt - 1)),ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv );
					_2xvhm1xm("P" ,"R" ,"C" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
				}
				else
				{
					//* 
					//*              Path 6a (M >= N, JOBZ='A') 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in RWORK(IRU) and computing right 
					//*              singular vectors of bidiagonal matrix in RWORK(IRVT) 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   N [e] + N*N [RU] + N*N [RVT] + BDSPAC 
					//* 
					
					_n6v1ono2 = _r49fp4o3;
					_emo79hqt = (_n6v1ono2 + (_dxpq0xkr * _dxpq0xkr));
					_r49fp4o3 = (_emo79hqt + (_dxpq0xkr * _dxpq0xkr));
					_c3rn9m7n("U" ,"I" ,ref _dxpq0xkr ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_dqanbbw3+(_n6v1ono2 - 1)),ref _dxpq0xkr ,(_dqanbbw3+(_emo79hqt - 1)),ref _dxpq0xkr ,_g7qb61ha ,_xpllja47 ,(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Set the right corner of U to identity matrix 
					//* 
					
					_k14i9nd8("F" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_gdjumcqt) ,_7u55mqkq ,ref _u6e6d39b );
					if (_ev4xhht5 > _dxpq0xkr)
					{
						
						_k14i9nd8("F" ,ref Unsafe.AsRef(_ev4xhht5 - _dxpq0xkr) ,ref Unsafe.AsRef(_ev4xhht5 - _dxpq0xkr) ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_40vhxf9f) ,(_7u55mqkq+(_dxpq0xkr + (int)1 - 1) + (_dxpq0xkr + (int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b );
					}
					//* 
					//*              Copy real matrix RWORK(IRU) to complex matrix U 
					//*              Overwrite U by left singular vectors of A 
					//*              CWorkspace: need   2*N [tauq, taup] + M    [work] 
					//*              CWorkspace: prefer 2*N [tauq, taup] + M*NB [work] 
					//*              RWorkspace: need   N [e] + N*N [RU] + N*N [RVT] 
					//* 
					
					_nz2p9162("F" ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_dqanbbw3+(_n6v1ono2 - 1)),ref _dxpq0xkr ,_7u55mqkq ,ref _u6e6d39b );
					_2xvhm1xm("Q" ,"L" ,"N" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Copy real matrix RWORK(IRVT) to complex matrix VT 
					//*              Overwrite VT by right singular vectors of A 
					//*              CWorkspace: need   2*N [tauq, taup] + N    [work] 
					//*              CWorkspace: prefer 2*N [tauq, taup] + N*NB [work] 
					//*              RWorkspace: need   N [e] + N*N [RU] + N*N [RVT] 
					//* 
					
					_nz2p9162("F" ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_dqanbbw3+(_emo79hqt - 1)),ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv );
					_2xvhm1xm("P" ,"R" ,"C" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
				}
				//* 
				
			}
			//* 
			
		}
		else
		{
			//* 
			//*        A has more columns than rows. If A has sufficiently more 
			//*        columns than rows, first reduce using the LQ decomposition (if 
			//*        sufficient workspace available) 
			//* 
			
			if (_dxpq0xkr >= _lmbwg5z2)
			{
				//* 
				
				if (_7y8lc0fu)
				{
					//* 
					//*              Path 1t (N >> M, JOBZ='N') 
					//*              No singular vectors to be computed 
					//* 
					
					_q1w15vsx = (int)1;
					_1myocm5q = (_q1w15vsx + _ev4xhht5);//* 
					//*              Compute A=L*Q 
					//*              CWorkspace: need   M [tau] + M    [work] 
					//*              CWorkspace: prefer M [tau] + M*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_nsco3b9r(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Zero out above L 
					//* 
					
					_k14i9nd8("U" ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_gdjumcqt) ,(_vxfgpup9+((int)1 - 1) + ((int)2 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
					_smxeww0r = (int)1;
					_wx1x93f0 = (int)1;
					_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
					_1myocm5q = (_5ke1jwwr + _ev4xhht5);//* 
					//*              Bidiagonalize L in A 
					//*              CWorkspace: need   2*M [tauq, taup] + M      [work] 
					//*              CWorkspace: prefer 2*M [tauq, taup] + 2*M*NB [work] 
					//*              RWorkspace: need   M [e] 
					//* 
					
					_w8dh0c16(ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
					_r49fp4o3 = (_smxeww0r + _ev4xhht5);//* 
					//*              Perform bidiagonal SVD, compute singular values only 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   M [e] + BDSPAC 
					//* 
					
					_c3rn9m7n("U" ,"N" ,ref _ev4xhht5 ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,_xpllja47 ,(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					
				}
				else
				if (_mtv5r2zz)
				{
					//* 
					//*              Path 2t (N >> M, JOBZ='O') 
					//*              M right singular vectors to be overwritten on A and 
					//*              M left singular vectors to be computed in U 
					//* 
					
					_gt43n8d1 = (int)1;
					_r9baffok = _ev4xhht5;//* 
					//*              WORK(IVT) is M by M 
					//* 
					
					_ic6kua09 = (_gt43n8d1 + (_r9baffok * _ev4xhht5));
					if (_6fnxzlyp >= (((_ev4xhht5 * _dxpq0xkr) + (_ev4xhht5 * _ev4xhht5)) + ((int)3 * _ev4xhht5)))
					{
						//* 
						//*                 WORK(IL) M by N 
						//* 
						
						_j49hoimx = _ev4xhht5;
						_8rfn4f7g = _dxpq0xkr;
					}
					else
					{
						//* 
						//*                 WORK(IL) is M by CHUNK 
						//* 
						
						_j49hoimx = _ev4xhht5;
						_8rfn4f7g = (((_6fnxzlyp - (_ev4xhht5 * _ev4xhht5)) - ((int)3 * _ev4xhht5)) / _ev4xhht5);
					}
					
					_q1w15vsx = (_ic6kua09 + (_j49hoimx * _8rfn4f7g));
					_1myocm5q = (_q1w15vsx + _ev4xhht5);//* 
					//*              Compute A=L*Q 
					//*              CWorkspace: need   M*M [VT] + M*M [L] + M [tau] + M    [work] 
					//*              CWorkspace: prefer M*M [VT] + M*M [L] + M [tau] + M*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_nsco3b9r(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Copy L to WORK(IL), zeroing about above it 
					//* 
					
					_nihu9ses("L" ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_ic6kua09 - 1)),ref _j49hoimx );
					_k14i9nd8("U" ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_gdjumcqt) ,(_apig8meb+(_ic6kua09 + _j49hoimx - 1)),ref _j49hoimx );//* 
					//*              Generate Q in A 
					//*              CWorkspace: need   M*M [VT] + M*M [L] + M [tau] + M    [work] 
					//*              CWorkspace: prefer M*M [VT] + M*M [L] + M [tau] + M*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_fcuznjj5(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
					_smxeww0r = (int)1;
					_wx1x93f0 = _q1w15vsx;
					_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
					_1myocm5q = (_5ke1jwwr + _ev4xhht5);//* 
					//*              Bidiagonalize L in WORK(IL) 
					//*              CWorkspace: need   M*M [VT] + M*M [L] + 2*M [tauq, taup] + M      [work] 
					//*              CWorkspace: prefer M*M [VT] + M*M [L] + 2*M [tauq, taup] + 2*M*NB [work] 
					//*              RWorkspace: need   M [e] 
					//* 
					
					_w8dh0c16(ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_ic6kua09 - 1)),ref _j49hoimx ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in RWORK(IRU) and computing right 
					//*              singular vectors of bidiagonal matrix in RWORK(IRVT) 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   M [e] + M*M [RU] + M*M [RVT] + BDSPAC 
					//* 
					
					_n6v1ono2 = (_smxeww0r + _ev4xhht5);
					_emo79hqt = (_n6v1ono2 + (_ev4xhht5 * _ev4xhht5));
					_r49fp4o3 = (_emo79hqt + (_ev4xhht5 * _ev4xhht5));
					_c3rn9m7n("U" ,"I" ,ref _ev4xhht5 ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_dqanbbw3+(_n6v1ono2 - 1)),ref _ev4xhht5 ,(_dqanbbw3+(_emo79hqt - 1)),ref _ev4xhht5 ,_g7qb61ha ,_xpllja47 ,(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Copy real matrix RWORK(IRU) to complex matrix WORK(IU) 
					//*              Overwrite WORK(IU) by the left singular vectors of L 
					//*              CWorkspace: need   M*M [VT] + M*M [L] + 2*M [tauq, taup] + M    [work] 
					//*              CWorkspace: prefer M*M [VT] + M*M [L] + 2*M [tauq, taup] + M*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_nz2p9162("F" ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_dqanbbw3+(_n6v1ono2 - 1)),ref _ev4xhht5 ,_7u55mqkq ,ref _u6e6d39b );
					_2xvhm1xm("Q" ,"L" ,"N" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_ic6kua09 - 1)),ref _j49hoimx ,(_apig8meb+(_wx1x93f0 - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Copy real matrix RWORK(IRVT) to complex matrix WORK(IVT) 
					//*              Overwrite WORK(IVT) by the right singular vectors of L 
					//*              CWorkspace: need   M*M [VT] + M*M [L] + 2*M [tauq, taup] + M    [work] 
					//*              CWorkspace: prefer M*M [VT] + M*M [L] + 2*M [tauq, taup] + M*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_nz2p9162("F" ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_dqanbbw3+(_emo79hqt - 1)),ref _ev4xhht5 ,(_apig8meb+(_gt43n8d1 - 1)),ref _r9baffok );
					_2xvhm1xm("P" ,"R" ,"C" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_ic6kua09 - 1)),ref _j49hoimx ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_gt43n8d1 - 1)),ref _r9baffok ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Multiply right singular vectors of L in WORK(IL) by Q 
					//*              in A, storing result in WORK(IL) and copying to A 
					//*              CWorkspace: need   M*M [VT] + M*M [L] 
					//*              CWorkspace: prefer M*M [VT] + M*N [L] 
					//*              RWorkspace: need   0 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn1132 = (System.Int32)((int)1);
						System.Int32 __81fgg2step1132 = (System.Int32)(_8rfn4f7g);
						System.Int32 __81fgg2count1132;
						for (__81fgg2count1132 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1132 + __81fgg2step1132) / __81fgg2step1132)), _b5p6od9s = __81fgg2dlsvn1132; __81fgg2count1132 != 0; __81fgg2count1132--, _b5p6od9s += (__81fgg2step1132)) {

						{
							
							_9giy8o3g = ILNumerics.F2NET.Intrinsics.MIN((_dxpq0xkr - _b5p6od9s) + (int)1 ,_8rfn4f7g );
							_xos1d1er("N" ,"N" ,ref _ev4xhht5 ,ref _9giy8o3g ,ref _ev4xhht5 ,ref Unsafe.AsRef(_40vhxf9f) ,(_apig8meb+(_gt43n8d1 - 1)),ref _ev4xhht5 ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_gdjumcqt) ,(_apig8meb+(_ic6kua09 - 1)),ref _j49hoimx );
							_nihu9ses("F" ,ref _ev4xhht5 ,ref _9giy8o3g ,(_apig8meb+(_ic6kua09 - 1)),ref _j49hoimx ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
Mark40:;
							// continue
						}
												}					}//* 
					
				}
				else
				if (_ngfvoqx1)
				{
					//* 
					//*              Path 3t (N >> M, JOBZ='S') 
					//*              M right singular vectors to be computed in VT and 
					//*              M left singular vectors to be computed in U 
					//* 
					
					_ic6kua09 = (int)1;//* 
					//*              WORK(IL) is M by M 
					//* 
					
					_j49hoimx = _ev4xhht5;
					_q1w15vsx = (_ic6kua09 + (_j49hoimx * _ev4xhht5));
					_1myocm5q = (_q1w15vsx + _ev4xhht5);//* 
					//*              Compute A=L*Q 
					//*              CWorkspace: need   M*M [L] + M [tau] + M    [work] 
					//*              CWorkspace: prefer M*M [L] + M [tau] + M*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_nsco3b9r(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Copy L to WORK(IL), zeroing out above it 
					//* 
					
					_nihu9ses("L" ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_ic6kua09 - 1)),ref _j49hoimx );
					_k14i9nd8("U" ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_gdjumcqt) ,(_apig8meb+(_ic6kua09 + _j49hoimx - 1)),ref _j49hoimx );//* 
					//*              Generate Q in A 
					//*              CWorkspace: need   M*M [L] + M [tau] + M    [work] 
					//*              CWorkspace: prefer M*M [L] + M [tau] + M*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_fcuznjj5(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
					_smxeww0r = (int)1;
					_wx1x93f0 = _q1w15vsx;
					_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
					_1myocm5q = (_5ke1jwwr + _ev4xhht5);//* 
					//*              Bidiagonalize L in WORK(IL) 
					//*              CWorkspace: need   M*M [L] + 2*M [tauq, taup] + M      [work] 
					//*              CWorkspace: prefer M*M [L] + 2*M [tauq, taup] + 2*M*NB [work] 
					//*              RWorkspace: need   M [e] 
					//* 
					
					_w8dh0c16(ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_ic6kua09 - 1)),ref _j49hoimx ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in RWORK(IRU) and computing right 
					//*              singular vectors of bidiagonal matrix in RWORK(IRVT) 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   M [e] + M*M [RU] + M*M [RVT] + BDSPAC 
					//* 
					
					_n6v1ono2 = (_smxeww0r + _ev4xhht5);
					_emo79hqt = (_n6v1ono2 + (_ev4xhht5 * _ev4xhht5));
					_r49fp4o3 = (_emo79hqt + (_ev4xhht5 * _ev4xhht5));
					_c3rn9m7n("U" ,"I" ,ref _ev4xhht5 ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_dqanbbw3+(_n6v1ono2 - 1)),ref _ev4xhht5 ,(_dqanbbw3+(_emo79hqt - 1)),ref _ev4xhht5 ,_g7qb61ha ,_xpllja47 ,(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Copy real matrix RWORK(IRU) to complex matrix U 
					//*              Overwrite U by left singular vectors of L 
					//*              CWorkspace: need   M*M [L] + 2*M [tauq, taup] + M    [work] 
					//*              CWorkspace: prefer M*M [L] + 2*M [tauq, taup] + M*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_nz2p9162("F" ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_dqanbbw3+(_n6v1ono2 - 1)),ref _ev4xhht5 ,_7u55mqkq ,ref _u6e6d39b );
					_2xvhm1xm("Q" ,"L" ,"N" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_ic6kua09 - 1)),ref _j49hoimx ,(_apig8meb+(_wx1x93f0 - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Copy real matrix RWORK(IRVT) to complex matrix VT 
					//*              Overwrite VT by left singular vectors of L 
					//*              CWorkspace: need   M*M [L] + 2*M [tauq, taup] + M    [work] 
					//*              CWorkspace: prefer M*M [L] + 2*M [tauq, taup] + M*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_nz2p9162("F" ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_dqanbbw3+(_emo79hqt - 1)),ref _ev4xhht5 ,_xdbczr8u ,ref _h4ibbatv );
					_2xvhm1xm("P" ,"R" ,"C" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_ic6kua09 - 1)),ref _j49hoimx ,(_apig8meb+(_5ke1jwwr - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Copy VT to WORK(IL), multiply right singular vectors of L 
					//*              in WORK(IL) by Q in A, storing result in VT 
					//*              CWorkspace: need   M*M [L] 
					//*              RWorkspace: need   0 
					//* 
					
					_nihu9ses("F" ,ref _ev4xhht5 ,ref _ev4xhht5 ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_ic6kua09 - 1)),ref _j49hoimx );
					_xos1d1er("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,ref Unsafe.AsRef(_40vhxf9f) ,(_apig8meb+(_ic6kua09 - 1)),ref _j49hoimx ,_vxfgpup9 ,ref _ocv8fk5c ,ref Unsafe.AsRef(_gdjumcqt) ,_xdbczr8u ,ref _h4ibbatv );//* 
					
				}
				else
				if (_6ecs6pbo)
				{
					//* 
					//*              Path 4t (N >> M, JOBZ='A') 
					//*              N right singular vectors to be computed in VT and 
					//*              M left singular vectors to be computed in U 
					//* 
					
					_gt43n8d1 = (int)1;//* 
					//*              WORK(IVT) is M by M 
					//* 
					
					_r9baffok = _ev4xhht5;
					_q1w15vsx = (_gt43n8d1 + (_r9baffok * _ev4xhht5));
					_1myocm5q = (_q1w15vsx + _ev4xhht5);//* 
					//*              Compute A=L*Q, copying result to VT 
					//*              CWorkspace: need   M*M [VT] + M [tau] + M    [work] 
					//*              CWorkspace: prefer M*M [VT] + M [tau] + M*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_nsco3b9r(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
					_nihu9ses("U" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );//* 
					//*              Generate Q in VT 
					//*              CWorkspace: need   M*M [VT] + M [tau] + N    [work] 
					//*              CWorkspace: prefer M*M [VT] + M [tau] + N*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_fcuznjj5(ref _dxpq0xkr ,ref _dxpq0xkr ,ref _ev4xhht5 ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Produce L in A, zeroing out above it 
					//* 
					
					_k14i9nd8("U" ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_gdjumcqt) ,(_vxfgpup9+((int)1 - 1) + ((int)2 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
					_smxeww0r = (int)1;
					_wx1x93f0 = _q1w15vsx;
					_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
					_1myocm5q = (_5ke1jwwr + _ev4xhht5);//* 
					//*              Bidiagonalize L in A 
					//*              CWorkspace: need   M*M [VT] + 2*M [tauq, taup] + M      [work] 
					//*              CWorkspace: prefer M*M [VT] + 2*M [tauq, taup] + 2*M*NB [work] 
					//*              RWorkspace: need   M [e] 
					//* 
					
					_w8dh0c16(ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in RWORK(IRU) and computing right 
					//*              singular vectors of bidiagonal matrix in RWORK(IRVT) 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   M [e] + M*M [RU] + M*M [RVT] + BDSPAC 
					//* 
					
					_n6v1ono2 = (_smxeww0r + _ev4xhht5);
					_emo79hqt = (_n6v1ono2 + (_ev4xhht5 * _ev4xhht5));
					_r49fp4o3 = (_emo79hqt + (_ev4xhht5 * _ev4xhht5));
					_c3rn9m7n("U" ,"I" ,ref _ev4xhht5 ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_dqanbbw3+(_n6v1ono2 - 1)),ref _ev4xhht5 ,(_dqanbbw3+(_emo79hqt - 1)),ref _ev4xhht5 ,_g7qb61ha ,_xpllja47 ,(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Copy real matrix RWORK(IRU) to complex matrix U 
					//*              Overwrite U by left singular vectors of L 
					//*              CWorkspace: need   M*M [VT] + 2*M [tauq, taup] + M    [work] 
					//*              CWorkspace: prefer M*M [VT] + 2*M [tauq, taup] + M*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_nz2p9162("F" ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_dqanbbw3+(_n6v1ono2 - 1)),ref _ev4xhht5 ,_7u55mqkq ,ref _u6e6d39b );
					_2xvhm1xm("Q" ,"L" ,"N" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Copy real matrix RWORK(IRVT) to complex matrix WORK(IVT) 
					//*              Overwrite WORK(IVT) by right singular vectors of L 
					//*              CWorkspace: need   M*M [VT] + 2*M [tauq, taup] + M    [work] 
					//*              CWorkspace: prefer M*M [VT] + 2*M [tauq, taup] + M*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_nz2p9162("F" ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_dqanbbw3+(_emo79hqt - 1)),ref _ev4xhht5 ,(_apig8meb+(_gt43n8d1 - 1)),ref _r9baffok );
					_2xvhm1xm("P" ,"R" ,"C" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_gt43n8d1 - 1)),ref _r9baffok ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Multiply right singular vectors of L in WORK(IVT) by 
					//*              Q in VT, storing result in A 
					//*              CWorkspace: need   M*M [VT] 
					//*              RWorkspace: need   0 
					//* 
					
					_xos1d1er("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,ref Unsafe.AsRef(_40vhxf9f) ,(_apig8meb+(_gt43n8d1 - 1)),ref _r9baffok ,_xdbczr8u ,ref _h4ibbatv ,ref Unsafe.AsRef(_gdjumcqt) ,_vxfgpup9 ,ref _ocv8fk5c );//* 
					//*              Copy right singular vectors of A from A to VT 
					//* 
					
					_nihu9ses("F" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );//* 
					
				}
				//* 
				
			}
			else
			if (_dxpq0xkr >= _0sgoq9v8)
			{
				//* 
				//*           MNTHR2 <= N < MNTHR1 
				//* 
				//*           Path 5t (N >> M, but not as much as MNTHR1) 
				//*           Reduce to bidiagonal form without QR decomposition, use 
				//*           ZUNGBR and matrix multiplication to compute singular vectors 
				//* 
				
				_smxeww0r = (int)1;
				_r49fp4o3 = (_smxeww0r + _ev4xhht5);
				_wx1x93f0 = (int)1;
				_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
				_1myocm5q = (_5ke1jwwr + _ev4xhht5);//* 
				//*           Bidiagonalize A 
				//*           CWorkspace: need   2*M [tauq, taup] + N        [work] 
				//*           CWorkspace: prefer 2*M [tauq, taup] + (M+N)*NB [work] 
				//*           RWorkspace: need   M [e] 
				//* 
				
				_w8dh0c16(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
				
				if (_7y8lc0fu)
				{
					//* 
					//*              Path 5tn (N >> M, JOBZ='N') 
					//*              Compute singular values only 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   M [e] + BDSPAC 
					//* 
					
					_c3rn9m7n("L" ,"N" ,ref _ev4xhht5 ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,_xpllja47 ,(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );
				}
				else
				if (_mtv5r2zz)
				{
					
					_emo79hqt = _r49fp4o3;
					_n6v1ono2 = (_emo79hqt + (_ev4xhht5 * _ev4xhht5));
					_r49fp4o3 = (_n6v1ono2 + (_ev4xhht5 * _ev4xhht5));
					_gt43n8d1 = _1myocm5q;//* 
					//*              Path 5to (N >> M, JOBZ='O') 
					//*              Copy A to U, generate Q 
					//*              CWorkspace: need   2*M [tauq, taup] + M    [work] 
					//*              CWorkspace: prefer 2*M [tauq, taup] + M*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_nihu9ses("L" ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );
					_anznwuhr("Q" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Generate P**H in A 
					//*              CWorkspace: need   2*M [tauq, taup] + M    [work] 
					//*              CWorkspace: prefer 2*M [tauq, taup] + M*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_anznwuhr("P" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					
					_r9baffok = _ev4xhht5;
					if (_6fnxzlyp >= ((_ev4xhht5 * _dxpq0xkr) + ((int)3 * _ev4xhht5)))
					{
						//* 
						//*                 WORK( IVT ) is M by N 
						//* 
						
						_1myocm5q = (_gt43n8d1 + (_r9baffok * _dxpq0xkr));
						_8rfn4f7g = _dxpq0xkr;
					}
					else
					{
						//* 
						//*                 WORK( IVT ) is M by CHUNK 
						//* 
						
						_8rfn4f7g = ((_6fnxzlyp - ((int)3 * _ev4xhht5)) / _ev4xhht5);
						_1myocm5q = (_gt43n8d1 + (_r9baffok * _8rfn4f7g));
					}
					//* 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in RWORK(IRU) and computing right 
					//*              singular vectors of bidiagonal matrix in RWORK(IRVT) 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   M [e] + M*M [RVT] + M*M [RU] + BDSPAC 
					//* 
					
					_c3rn9m7n("L" ,"I" ,ref _ev4xhht5 ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_dqanbbw3+(_n6v1ono2 - 1)),ref _ev4xhht5 ,(_dqanbbw3+(_emo79hqt - 1)),ref _ev4xhht5 ,_g7qb61ha ,_xpllja47 ,(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Multiply Q in U by real matrix RWORK(IRVT) 
					//*              storing the result in WORK(IVT), copying to U 
					//*              CWorkspace: need   2*M [tauq, taup] + M*M [VT] 
					//*              RWorkspace: need   M [e] + M*M [RVT] + M*M [RU] + 2*M*M [rwork] 
					//* 
					
					_szowpa5a(ref _ev4xhht5 ,ref _ev4xhht5 ,_7u55mqkq ,ref _u6e6d39b ,(_dqanbbw3+(_n6v1ono2 - 1)),ref _ev4xhht5 ,(_apig8meb+(_gt43n8d1 - 1)),ref _r9baffok ,(_dqanbbw3+(_r49fp4o3 - 1)));
					_nihu9ses("F" ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_gt43n8d1 - 1)),ref _r9baffok ,_7u55mqkq ,ref _u6e6d39b );//* 
					//*              Multiply RWORK(IRVT) by P**H in A, storing the 
					//*              result in WORK(IVT), copying to A 
					//*              CWorkspace: need   2*M [tauq, taup] + M*M [VT] 
					//*              CWorkspace: prefer 2*M [tauq, taup] + M*N [VT] 
					//*              RWorkspace: need   M [e] + M*M [RVT] + 2*M*M [rwork] 
					//*              RWorkspace: prefer M [e] + M*M [RVT] + 2*M*N [rwork] < M + 5*M*M since N < 2*M here 
					//* 
					
					_r49fp4o3 = _n6v1ono2;
					{
						System.Int32 __81fgg2dlsvn1133 = (System.Int32)((int)1);
						System.Int32 __81fgg2step1133 = (System.Int32)(_8rfn4f7g);
						System.Int32 __81fgg2count1133;
						for (__81fgg2count1133 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1133 + __81fgg2step1133) / __81fgg2step1133)), _b5p6od9s = __81fgg2dlsvn1133; __81fgg2count1133 != 0; __81fgg2count1133--, _b5p6od9s += (__81fgg2step1133)) {

						{
							
							_9giy8o3g = ILNumerics.F2NET.Intrinsics.MIN((_dxpq0xkr - _b5p6od9s) + (int)1 ,_8rfn4f7g );
							_zwe74bak(ref _ev4xhht5 ,ref _9giy8o3g ,(_dqanbbw3+(_emo79hqt - 1)),ref _ev4xhht5 ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_apig8meb+(_gt43n8d1 - 1)),ref _r9baffok ,(_dqanbbw3+(_r49fp4o3 - 1)));
							_nihu9ses("F" ,ref _ev4xhht5 ,ref _9giy8o3g ,(_apig8meb+(_gt43n8d1 - 1)),ref _r9baffok ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
Mark50:;
							// continue
						}
												}					}
				}
				else
				if (_ngfvoqx1)
				{
					//* 
					//*              Path 5ts (N >> M, JOBZ='S') 
					//*              Copy A to U, generate Q 
					//*              CWorkspace: need   2*M [tauq, taup] + M    [work] 
					//*              CWorkspace: prefer 2*M [tauq, taup] + M*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_nihu9ses("L" ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );
					_anznwuhr("Q" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Copy A to VT, generate P**H 
					//*              CWorkspace: need   2*M [tauq, taup] + M    [work] 
					//*              CWorkspace: prefer 2*M [tauq, taup] + M*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_nihu9ses("U" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );
					_anznwuhr("P" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in RWORK(IRU) and computing right 
					//*              singular vectors of bidiagonal matrix in RWORK(IRVT) 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   M [e] + M*M [RVT] + M*M [RU] + BDSPAC 
					//* 
					
					_emo79hqt = _r49fp4o3;
					_n6v1ono2 = (_emo79hqt + (_ev4xhht5 * _ev4xhht5));
					_r49fp4o3 = (_n6v1ono2 + (_ev4xhht5 * _ev4xhht5));
					_c3rn9m7n("L" ,"I" ,ref _ev4xhht5 ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_dqanbbw3+(_n6v1ono2 - 1)),ref _ev4xhht5 ,(_dqanbbw3+(_emo79hqt - 1)),ref _ev4xhht5 ,_g7qb61ha ,_xpllja47 ,(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Multiply Q in U by real matrix RWORK(IRU), storing the 
					//*              result in A, copying to U 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   M [e] + M*M [RVT] + M*M [RU] + 2*M*M [rwork] 
					//* 
					
					_szowpa5a(ref _ev4xhht5 ,ref _ev4xhht5 ,_7u55mqkq ,ref _u6e6d39b ,(_dqanbbw3+(_n6v1ono2 - 1)),ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_dqanbbw3+(_r49fp4o3 - 1)));
					_nihu9ses("F" ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );//* 
					//*              Multiply real matrix RWORK(IRVT) by P**H in VT, 
					//*              storing the result in A, copying to VT 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   M [e] + M*M [RVT] + 2*M*N [rwork] < M + 5*M*M since N < 2*M here 
					//* 
					
					_r49fp4o3 = _n6v1ono2;
					_zwe74bak(ref _ev4xhht5 ,ref _dxpq0xkr ,(_dqanbbw3+(_emo79hqt - 1)),ref _ev4xhht5 ,_xdbczr8u ,ref _h4ibbatv ,_vxfgpup9 ,ref _ocv8fk5c ,(_dqanbbw3+(_r49fp4o3 - 1)));
					_nihu9ses("F" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );
				}
				else
				{
					//* 
					//*              Path 5ta (N >> M, JOBZ='A') 
					//*              Copy A to U, generate Q 
					//*              CWorkspace: need   2*M [tauq, taup] + M    [work] 
					//*              CWorkspace: prefer 2*M [tauq, taup] + M*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_nihu9ses("L" ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );
					_anznwuhr("Q" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Copy A to VT, generate P**H 
					//*              CWorkspace: need   2*M [tauq, taup] + N    [work] 
					//*              CWorkspace: prefer 2*M [tauq, taup] + N*NB [work] 
					//*              RWorkspace: need   0 
					//* 
					
					_nihu9ses("U" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );
					_anznwuhr("P" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _ev4xhht5 ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in RWORK(IRU) and computing right 
					//*              singular vectors of bidiagonal matrix in RWORK(IRVT) 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   M [e] + M*M [RVT] + M*M [RU] + BDSPAC 
					//* 
					
					_emo79hqt = _r49fp4o3;
					_n6v1ono2 = (_emo79hqt + (_ev4xhht5 * _ev4xhht5));
					_r49fp4o3 = (_n6v1ono2 + (_ev4xhht5 * _ev4xhht5));
					_c3rn9m7n("L" ,"I" ,ref _ev4xhht5 ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_dqanbbw3+(_n6v1ono2 - 1)),ref _ev4xhht5 ,(_dqanbbw3+(_emo79hqt - 1)),ref _ev4xhht5 ,_g7qb61ha ,_xpllja47 ,(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Multiply Q in U by real matrix RWORK(IRU), storing the 
					//*              result in A, copying to U 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   M [e] + M*M [RVT] + M*M [RU] + 2*M*M [rwork] 
					//* 
					
					_szowpa5a(ref _ev4xhht5 ,ref _ev4xhht5 ,_7u55mqkq ,ref _u6e6d39b ,(_dqanbbw3+(_n6v1ono2 - 1)),ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_dqanbbw3+(_r49fp4o3 - 1)));
					_nihu9ses("F" ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );//* 
					//*              Multiply real matrix RWORK(IRVT) by P**H in VT, 
					//*              storing the result in A, copying to VT 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   M [e] + M*M [RVT] + 2*M*N [rwork] < M + 5*M*M since N < 2*M here 
					//* 
					
					_r49fp4o3 = _n6v1ono2;
					_zwe74bak(ref _ev4xhht5 ,ref _dxpq0xkr ,(_dqanbbw3+(_emo79hqt - 1)),ref _ev4xhht5 ,_xdbczr8u ,ref _h4ibbatv ,_vxfgpup9 ,ref _ocv8fk5c ,(_dqanbbw3+(_r49fp4o3 - 1)));
					_nihu9ses("F" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );
				}
				//* 
				
			}
			else
			{
				//* 
				//*           N .LT. MNTHR2 
				//* 
				//*           Path 6t (N > M, but not much larger) 
				//*           Reduce to bidiagonal form without LQ decomposition 
				//*           Use ZUNMBR to compute singular vectors 
				//* 
				
				_smxeww0r = (int)1;
				_r49fp4o3 = (_smxeww0r + _ev4xhht5);
				_wx1x93f0 = (int)1;
				_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
				_1myocm5q = (_5ke1jwwr + _ev4xhht5);//* 
				//*           Bidiagonalize A 
				//*           CWorkspace: need   2*M [tauq, taup] + N        [work] 
				//*           CWorkspace: prefer 2*M [tauq, taup] + (M+N)*NB [work] 
				//*           RWorkspace: need   M [e] 
				//* 
				
				_w8dh0c16(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
				if (_7y8lc0fu)
				{
					//* 
					//*              Path 6tn (N > M, JOBZ='N') 
					//*              Compute singular values only 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   M [e] + BDSPAC 
					//* 
					
					_c3rn9m7n("L" ,"N" ,ref _ev4xhht5 ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,_xpllja47 ,(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );
				}
				else
				if (_mtv5r2zz)
				{
					//*              Path 6to (N > M, JOBZ='O') 
					
					_r9baffok = _ev4xhht5;
					_gt43n8d1 = _1myocm5q;
					if (_6fnxzlyp >= ((_ev4xhht5 * _dxpq0xkr) + ((int)3 * _ev4xhht5)))
					{
						//* 
						//*                 WORK( IVT ) is M by N 
						//* 
						
						_k14i9nd8("F" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_gdjumcqt) ,(_apig8meb+(_gt43n8d1 - 1)),ref _r9baffok );
						_1myocm5q = (_gt43n8d1 + (_r9baffok * _dxpq0xkr));
					}
					else
					{
						//* 
						//*                 WORK( IVT ) is M by CHUNK 
						//* 
						
						_8rfn4f7g = ((_6fnxzlyp - ((int)3 * _ev4xhht5)) / _ev4xhht5);
						_1myocm5q = (_gt43n8d1 + (_r9baffok * _8rfn4f7g));
					}
					//* 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in RWORK(IRU) and computing right 
					//*              singular vectors of bidiagonal matrix in RWORK(IRVT) 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   M [e] + M*M [RVT] + M*M [RU] + BDSPAC 
					//* 
					
					_emo79hqt = _r49fp4o3;
					_n6v1ono2 = (_emo79hqt + (_ev4xhht5 * _ev4xhht5));
					_r49fp4o3 = (_n6v1ono2 + (_ev4xhht5 * _ev4xhht5));
					_c3rn9m7n("L" ,"I" ,ref _ev4xhht5 ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_dqanbbw3+(_n6v1ono2 - 1)),ref _ev4xhht5 ,(_dqanbbw3+(_emo79hqt - 1)),ref _ev4xhht5 ,_g7qb61ha ,_xpllja47 ,(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Copy real matrix RWORK(IRU) to complex matrix U 
					//*              Overwrite U by left singular vectors of A 
					//*              CWorkspace: need   2*M [tauq, taup] + M*M [VT] + M    [work] 
					//*              CWorkspace: prefer 2*M [tauq, taup] + M*M [VT] + M*NB [work] 
					//*              RWorkspace: need   M [e] + M*M [RVT] + M*M [RU] 
					//* 
					
					_nz2p9162("F" ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_dqanbbw3+(_n6v1ono2 - 1)),ref _ev4xhht5 ,_7u55mqkq ,ref _u6e6d39b );
					_2xvhm1xm("Q" ,"L" ,"N" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					
					if (_6fnxzlyp >= ((_ev4xhht5 * _dxpq0xkr) + ((int)3 * _ev4xhht5)))
					{
						//* 
						//*                 Path 6to-fast 
						//*                 Copy real matrix RWORK(IRVT) to complex matrix WORK(IVT) 
						//*                 Overwrite WORK(IVT) by right singular vectors of A, 
						//*                 copying to A 
						//*                 CWorkspace: need   2*M [tauq, taup] + M*N [VT] + M    [work] 
						//*                 CWorkspace: prefer 2*M [tauq, taup] + M*N [VT] + M*NB [work] 
						//*                 RWorkspace: need   M [e] + M*M [RVT] 
						//* 
						
						_nz2p9162("F" ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_dqanbbw3+(_emo79hqt - 1)),ref _ev4xhht5 ,(_apig8meb+(_gt43n8d1 - 1)),ref _r9baffok );
						_2xvhm1xm("P" ,"R" ,"C" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_gt43n8d1 - 1)),ref _r9baffok ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
						_nihu9ses("F" ,ref _ev4xhht5 ,ref _dxpq0xkr ,(_apig8meb+(_gt43n8d1 - 1)),ref _r9baffok ,_vxfgpup9 ,ref _ocv8fk5c );
					}
					else
					{
						//* 
						//*                 Path 6to-slow 
						//*                 Generate P**H in A 
						//*                 CWorkspace: need   2*M [tauq, taup] + M*M [VT] + M    [work] 
						//*                 CWorkspace: prefer 2*M [tauq, taup] + M*M [VT] + M*NB [work] 
						//*                 RWorkspace: need   0 
						//* 
						
						_anznwuhr("P" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
						//*                 Multiply Q in A by real matrix RWORK(IRU), storing the 
						//*                 result in WORK(IU), copying to A 
						//*                 CWorkspace: need   2*M [tauq, taup] + M*M [VT] 
						//*                 CWorkspace: prefer 2*M [tauq, taup] + M*N [VT] 
						//*                 RWorkspace: need   M [e] + M*M [RVT] + 2*M*M [rwork] 
						//*                 RWorkspace: prefer M [e] + M*M [RVT] + 2*M*N [rwork] < M + 5*M*M since N < 2*M here 
						//* 
						
						_r49fp4o3 = _n6v1ono2;
						{
							System.Int32 __81fgg2dlsvn1134 = (System.Int32)((int)1);
							System.Int32 __81fgg2step1134 = (System.Int32)(_8rfn4f7g);
							System.Int32 __81fgg2count1134;
							for (__81fgg2count1134 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1134 + __81fgg2step1134) / __81fgg2step1134)), _b5p6od9s = __81fgg2dlsvn1134; __81fgg2count1134 != 0; __81fgg2count1134--, _b5p6od9s += (__81fgg2step1134)) {

							{
								
								_9giy8o3g = ILNumerics.F2NET.Intrinsics.MIN((_dxpq0xkr - _b5p6od9s) + (int)1 ,_8rfn4f7g );
								_zwe74bak(ref _ev4xhht5 ,ref _9giy8o3g ,(_dqanbbw3+(_emo79hqt - 1)),ref _ev4xhht5 ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_apig8meb+(_gt43n8d1 - 1)),ref _r9baffok ,(_dqanbbw3+(_r49fp4o3 - 1)));
								_nihu9ses("F" ,ref _ev4xhht5 ,ref _9giy8o3g ,(_apig8meb+(_gt43n8d1 - 1)),ref _r9baffok ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
Mark60:;
								// continue
							}
														}						}
					}
					
				}
				else
				if (_ngfvoqx1)
				{
					//* 
					//*              Path 6ts (N > M, JOBZ='S') 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in RWORK(IRU) and computing right 
					//*              singular vectors of bidiagonal matrix in RWORK(IRVT) 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   M [e] + M*M [RVT] + M*M [RU] + BDSPAC 
					//* 
					
					_emo79hqt = _r49fp4o3;
					_n6v1ono2 = (_emo79hqt + (_ev4xhht5 * _ev4xhht5));
					_r49fp4o3 = (_n6v1ono2 + (_ev4xhht5 * _ev4xhht5));
					_c3rn9m7n("L" ,"I" ,ref _ev4xhht5 ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_dqanbbw3+(_n6v1ono2 - 1)),ref _ev4xhht5 ,(_dqanbbw3+(_emo79hqt - 1)),ref _ev4xhht5 ,_g7qb61ha ,_xpllja47 ,(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Copy real matrix RWORK(IRU) to complex matrix U 
					//*              Overwrite U by left singular vectors of A 
					//*              CWorkspace: need   2*M [tauq, taup] + M    [work] 
					//*              CWorkspace: prefer 2*M [tauq, taup] + M*NB [work] 
					//*              RWorkspace: need   M [e] + M*M [RVT] + M*M [RU] 
					//* 
					
					_nz2p9162("F" ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_dqanbbw3+(_n6v1ono2 - 1)),ref _ev4xhht5 ,_7u55mqkq ,ref _u6e6d39b );
					_2xvhm1xm("Q" ,"L" ,"N" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Copy real matrix RWORK(IRVT) to complex matrix VT 
					//*              Overwrite VT by right singular vectors of A 
					//*              CWorkspace: need   2*M [tauq, taup] + M    [work] 
					//*              CWorkspace: prefer 2*M [tauq, taup] + M*NB [work] 
					//*              RWorkspace: need   M [e] + M*M [RVT] 
					//* 
					
					_k14i9nd8("F" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_gdjumcqt) ,_xdbczr8u ,ref _h4ibbatv );
					_nz2p9162("F" ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_dqanbbw3+(_emo79hqt - 1)),ref _ev4xhht5 ,_xdbczr8u ,ref _h4ibbatv );
					_2xvhm1xm("P" ,"R" ,"C" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
				}
				else
				{
					//* 
					//*              Path 6ta (N > M, JOBZ='A') 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in RWORK(IRU) and computing right 
					//*              singular vectors of bidiagonal matrix in RWORK(IRVT) 
					//*              CWorkspace: need   0 
					//*              RWorkspace: need   M [e] + M*M [RVT] + M*M [RU] + BDSPAC 
					//* 
					
					_emo79hqt = _r49fp4o3;
					_n6v1ono2 = (_emo79hqt + (_ev4xhht5 * _ev4xhht5));
					_r49fp4o3 = (_n6v1ono2 + (_ev4xhht5 * _ev4xhht5));//* 
					
					_c3rn9m7n("L" ,"I" ,ref _ev4xhht5 ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_dqanbbw3+(_n6v1ono2 - 1)),ref _ev4xhht5 ,(_dqanbbw3+(_emo79hqt - 1)),ref _ev4xhht5 ,_g7qb61ha ,_xpllja47 ,(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Copy real matrix RWORK(IRU) to complex matrix U 
					//*              Overwrite U by left singular vectors of A 
					//*              CWorkspace: need   2*M [tauq, taup] + M    [work] 
					//*              CWorkspace: prefer 2*M [tauq, taup] + M*NB [work] 
					//*              RWorkspace: need   M [e] + M*M [RVT] + M*M [RU] 
					//* 
					
					_nz2p9162("F" ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_dqanbbw3+(_n6v1ono2 - 1)),ref _ev4xhht5 ,_7u55mqkq ,ref _u6e6d39b );
					_2xvhm1xm("Q" ,"L" ,"N" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Set all of VT to identity matrix 
					//* 
					
					_k14i9nd8("F" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_40vhxf9f) ,_xdbczr8u ,ref _h4ibbatv );//* 
					//*              Copy real matrix RWORK(IRVT) to complex matrix VT 
					//*              Overwrite VT by right singular vectors of A 
					//*              CWorkspace: need   2*M [tauq, taup] + N    [work] 
					//*              CWorkspace: prefer 2*M [tauq, taup] + N*NB [work] 
					//*              RWorkspace: need   M [e] + M*M [RVT] 
					//* 
					
					_nz2p9162("F" ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_dqanbbw3+(_emo79hqt - 1)),ref _ev4xhht5 ,_xdbczr8u ,ref _h4ibbatv );
					_2xvhm1xm("P" ,"R" ,"C" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
				}
				//* 
				
			}
			//* 
			
		}
		//* 
		//*     Undo scaling if necessary 
		//* 
		
		if (_65mv5f5m == (int)1)
		{
			
			if (_j6vjow1g > _av7j8yda)
			_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _av7j8yda ,ref _j6vjow1g ,ref _qaseb1y7 ,ref Unsafe.AsRef((int)1) ,_irk8i6qr ,ref _qaseb1y7 ,ref _bhsiylw4 );
			if ((_gro5yvfo != (int)0) & (_j6vjow1g > _av7j8yda))
			_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _av7j8yda ,ref _j6vjow1g ,ref Unsafe.AsRef(_qaseb1y7 - (int)1) ,ref Unsafe.AsRef((int)1) ,(_dqanbbw3+(_smxeww0r - 1)),ref _qaseb1y7 ,ref _bhsiylw4 );
			if (_j6vjow1g < _bogm0gwy)
			_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _bogm0gwy ,ref _j6vjow1g ,ref _qaseb1y7 ,ref Unsafe.AsRef((int)1) ,_irk8i6qr ,ref _qaseb1y7 ,ref _bhsiylw4 );
			if ((_gro5yvfo != (int)0) & (_j6vjow1g < _bogm0gwy))
			_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _bogm0gwy ,ref _j6vjow1g ,ref Unsafe.AsRef(_qaseb1y7 - (int)1) ,ref Unsafe.AsRef((int)1) ,(_dqanbbw3+(_smxeww0r - 1)),ref _qaseb1y7 ,ref _bhsiylw4 );
		}
		//* 
		//*     Return optimal workspace in WORK(1) 
		//* 
		
		*(_apig8meb+((int)1 - 1)) = DCMPLX(_tafa1evd);//* 
		
		return;//* 
		//*     End of ZGESDD 
		//* 
		
	}
	
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
