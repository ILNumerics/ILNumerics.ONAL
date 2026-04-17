
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
//*> \brief \b DGESDD 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DGESDD + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dgesdd.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dgesdd.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dgesdd.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DGESDD( JOBZ, M, N, A, LDA, S, U, LDU, VT, LDVT, 
//*                          WORK, LWORK, IWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          JOBZ 
//*       INTEGER            INFO, LDA, LDU, LDVT, LWORK, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IWORK( * ) 
//*       DOUBLE PRECISION   A( LDA, * ), S( * ), U( LDU, * ), 
//*      $                   VT( LDVT, * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DGESDD computes the singular value decomposition (SVD) of a real 
//*> M-by-N matrix A, optionally computing the left and right singular 
//*> vectors.  If singular vectors are desired, it uses a 
//*> divide-and-conquer algorithm. 
//*> 
//*> The SVD is written 
//*> 
//*>      A = U * SIGMA * transpose(V) 
//*> 
//*> where SIGMA is an M-by-N matrix which is zero except for its 
//*> min(m,n) diagonal elements, U is an M-by-M orthogonal matrix, and 
//*> V is an N-by-N orthogonal matrix.  The diagonal elements of SIGMA 
//*> are the singular values of A; they are real and non-negative, and 
//*> are returned in descending order.  The first min(m,n) columns of 
//*> U and V are the left and right singular vectors of A. 
//*> 
//*> Note that the routine returns VT = V**T, not V. 
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
//*>          = 'A':  all M columns of U and all N rows of V**T are 
//*>                  returned in the arrays U and VT; 
//*>          = 'S':  the first min(M,N) columns of U and the first 
//*>                  min(M,N) rows of V**T are returned in the arrays U 
//*>                  and VT; 
//*>          = 'O':  If M >= N, the first N columns of U are overwritten 
//*>                  on the array A and all rows of V**T are returned in 
//*>                  the array VT; 
//*>                  otherwise, all columns of U are returned in the 
//*>                  array U and the first M rows of V**T are overwritten 
//*>                  in the array A; 
//*>          = 'N':  no columns of U or rows of V**T are computed. 
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
//*>          A is DOUBLE PRECISION array, dimension (LDA,N) 
//*>          On entry, the M-by-N matrix A. 
//*>          On exit, 
//*>          if JOBZ = 'O',  A is overwritten with the first N columns 
//*>                          of U (the left singular vectors, stored 
//*>                          columnwise) if M >= N; 
//*>                          A is overwritten with the first M rows 
//*>                          of V**T (the right singular vectors, stored 
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
//*>          U is DOUBLE PRECISION array, dimension (LDU,UCOL) 
//*>          UCOL = M if JOBZ = 'A' or JOBZ = 'O' and M < N; 
//*>          UCOL = min(M,N) if JOBZ = 'S'. 
//*>          If JOBZ = 'A' or JOBZ = 'O' and M < N, U contains the M-by-M 
//*>          orthogonal matrix U; 
//*>          if JOBZ = 'S', U contains the first min(M,N) columns of U 
//*>          (the left singular vectors, stored columnwise); 
//*>          if JOBZ = 'O' and M >= N, or JOBZ = 'N', U is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] LDU 
//*> \verbatim 
//*>          LDU is INTEGER 
//*>          The leading dimension of the array U.  LDU >= 1; if 
//*>          JOBZ = 'S' or 'A' or JOBZ = 'O' and M < N, LDU >= M. 
//*> \endverbatim 
//*> 
//*> \param[out] VT 
//*> \verbatim 
//*>          VT is DOUBLE PRECISION array, dimension (LDVT,N) 
//*>          If JOBZ = 'A' or JOBZ = 'O' and M >= N, VT contains the 
//*>          N-by-N orthogonal matrix V**T; 
//*>          if JOBZ = 'S', VT contains the first min(M,N) rows of 
//*>          V**T (the right singular vectors, stored rowwise); 
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
//*>          WORK is DOUBLE PRECISION array, dimension (MAX(1,LWORK)) 
//*>          On exit, if INFO = 0, WORK(1) returns the optimal LWORK; 
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
//*>          If JOBZ = 'N', LWORK >= 3*mn + max( mx, 7*mn ). 
//*>          If JOBZ = 'O', LWORK >= 3*mn + max( mx, 5*mn*mn + 4*mn ). 
//*>          If JOBZ = 'S', LWORK >= 4*mn*mn + 7*mn. 
//*>          If JOBZ = 'A', LWORK >= 4*mn*mn + 6*mn + mx. 
//*>          These are not tight minimums in all cases; see comments inside code. 
//*>          For good performance, LWORK should generally be larger; 
//*>          a query is recommended. 
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
//*>          > 0:  DBDSDC did not converge, updating process failed. 
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
//*> \ingroup doubleGEsing 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>     Ming Gu and Huan Ren, Computer Science Division, University of 
//*>     California at Berkeley, USA 
//*> 
//*  ===================================================================== 

	 
	public static void _ybtak27u(FString _w6igfk2h, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, Double* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _irk8i6qr, Double* _7u55mqkq, ref Int32 _u6e6d39b, Double* _xdbczr8u, ref Int32 _h4ibbatv, Double* _apig8meb, ref Int32 _6fnxzlyp, Int32* _4b6rt45i, ref Int32 _gro5yvfo)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)12 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Boolean _lhlgm7z5 =  default;
Boolean _6ecs6pbo =  default;
Boolean _9hcbk2ie =  default;
Boolean _7y8lc0fu =  default;
Boolean _mtv5r2zz =  default;
Boolean _ngfvoqx1 =  default;
Int32 _24c48sks =  default;
Int32 _9giy8o3g =  default;
Int32 _8rfn4f7g =  default;
Int32 _b5p6od9s =  default;
Int32 _smxeww0r =  default;
Int32 _bhsiylw4 =  default;
Int32 _ic6kua09 =  default;
Int32 _m9w6lk7x =  default;
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
Int32 _lhzduysr =  default;
Int32 _1myocm5q =  default;
Int32 _loujht2t =  default;
Int32 _nxtvvvgq =  default;
Int32 _sozuprs2 =  default;
Int32 _sbnzdk54 =  default;
Int32 _k1gcshu1 =  default;
Int32 _og07f66f =  default;
Int32 _fh5yxlb3 =  default;
Int32 _521fgdpc =  default;
Int32 _1zl3pe8l =  default;
Int32 _38fj0vxv =  default;
Int32 _fit6phg9 =  default;
Int32 _zmg0poul =  default;
Int32 _0xfpvc0b =  default;
Int32 _jt4zf2dk =  default;
Int32 _ega8ttxu =  default;
Int32 _smdc8i6p =  default;
Int32 _n4scph9d =  default;
Int32 _rsx5mr37 =  default;
Double _j6vjow1g =  default;
Double _av7j8yda =  default;
Double _p1iqarg6 =  default;
Double _bogm0gwy =  default;
Int32* _xpllja47 =  (Int32*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Int32) * ((int)1);
Double* _g7qb61ha =  (Double*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Double) * ((int)1);
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
		_6ecs6pbo = _w8y2rzgy(_w6igfk2h ,"A" );
		_ngfvoqx1 = _w8y2rzgy(_w6igfk2h ,"S" );
		_9hcbk2ie = (_6ecs6pbo | _ngfvoqx1);
		_mtv5r2zz = _w8y2rzgy(_w6igfk2h ,"O" );
		_7y8lc0fu = _w8y2rzgy(_w6igfk2h ,"N" );
		_lhlgm7z5 = (_6fnxzlyp == (int)-1);//* 
		
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
		//*       NB refers to the optimal block size for the immediately 
		//*       following subroutine, as returned by ILAENV. 
		//* 
		
		if (_gro5yvfo == (int)0)
		{
			
			_gghrqcr1 = (int)1;
			_tafa1evd = (int)1;
			_24c48sks = (int)0;
			_lhzduysr = ILNumerics.F2NET.Intrinsics.INT((_qaseb1y7 * 11d) / 6d );
			if ((_ev4xhht5 >= _dxpq0xkr) & (_qaseb1y7 > (int)0))
			{
				//* 
				//*           Compute space needed for DBDSDC 
				//* 
				
				if (_7y8lc0fu)
				{
					//*              dbdsdc needs only 4*N (or 6*N for uplo=L for LAPACK <= 3.6) 
					//*              keep 7*N for backwards compatibility. 
					
					_24c48sks = ((int)7 * _dxpq0xkr);
				}
				else
				{
					
					_24c48sks = ((((int)3 * _dxpq0xkr) * _dxpq0xkr) + ((int)4 * _dxpq0xkr));
				}
				//* 
				//*           Compute space preferred for each routine 
				
				_sf2bwwb1(ref _ev4xhht5 ,ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),ref _ev4xhht5 ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_nxtvvvgq = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//* 
				
				_sf2bwwb1(ref _dxpq0xkr ,ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_sbnzdk54 = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//* 
				
				_ac2l6xc0(ref _ev4xhht5 ,ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),ref _ev4xhht5 ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_og07f66f = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//* 
				
				_cmc4j0e3("Q" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_521fgdpc = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//* 
				
				_hxix712m(ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),ref _ev4xhht5 ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_fit6phg9 = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//* 
				
				_hxix712m(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),ref _ev4xhht5 ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_zmg0poul = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//* 
				
				_pwi7fryj("P" ,"R" ,"T" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_n4scph9d = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//* 
				
				_pwi7fryj("Q" ,"L" ,"N" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_rsx5mr37 = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//* 
				
				_pwi7fryj("Q" ,"L" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),ref _ev4xhht5 ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref _ev4xhht5 ,(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_smdc8i6p = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//* 
				
				_pwi7fryj("Q" ,"L" ,"N" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),ref _ev4xhht5 ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref _ev4xhht5 ,(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_jt4zf2dk = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//* 
				
				if (_ev4xhht5 >= _lhzduysr)
				{
					
					if (_7y8lc0fu)
					{
						//* 
						//*                 Path 1 (M >> N, JOBZ='N') 
						//* 
						
						_loujht2t = (_dxpq0xkr + _og07f66f);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _sbnzdk54 );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_24c48sks + _dxpq0xkr );
						_gghrqcr1 = (_24c48sks + _dxpq0xkr);
					}
					else
					if (_mtv5r2zz)
					{
						//* 
						//*                 Path 2 (M >> N, JOBZ='O') 
						//* 
						
						_loujht2t = (_dxpq0xkr + _og07f66f);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_dxpq0xkr + _zmg0poul );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _sbnzdk54 );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _rsx5mr37 );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _n4scph9d );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _24c48sks );
						_tafa1evd = (_loujht2t + (((int)2 * _dxpq0xkr) * _dxpq0xkr));
						_gghrqcr1 = ((_24c48sks + (((int)2 * _dxpq0xkr) * _dxpq0xkr)) + ((int)3 * _dxpq0xkr));
					}
					else
					if (_ngfvoqx1)
					{
						//* 
						//*                 Path 3 (M >> N, JOBZ='S') 
						//* 
						
						_loujht2t = (_dxpq0xkr + _og07f66f);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_dxpq0xkr + _zmg0poul );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _sbnzdk54 );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _rsx5mr37 );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _n4scph9d );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _24c48sks );
						_tafa1evd = (_loujht2t + (_dxpq0xkr * _dxpq0xkr));
						_gghrqcr1 = ((_24c48sks + (_dxpq0xkr * _dxpq0xkr)) + ((int)3 * _dxpq0xkr));
					}
					else
					if (_6ecs6pbo)
					{
						//* 
						//*                 Path 4 (M >> N, JOBZ='A') 
						//* 
						
						_loujht2t = (_dxpq0xkr + _og07f66f);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_dxpq0xkr + _fit6phg9 );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _sbnzdk54 );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _rsx5mr37 );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _n4scph9d );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _24c48sks );
						_tafa1evd = (_loujht2t + (_dxpq0xkr * _dxpq0xkr));
						_gghrqcr1 = ((_dxpq0xkr * _dxpq0xkr) + ILNumerics.F2NET.Intrinsics.MAX(((int)3 * _dxpq0xkr) + _24c48sks ,_dxpq0xkr + _ev4xhht5 ));
					}
					
				}
				else
				{
					//* 
					//*              Path 5 (M >= N, but not much larger) 
					//* 
					
					_loujht2t = (((int)3 * _dxpq0xkr) + _nxtvvvgq);
					if (_7y8lc0fu)
					{
						//*                 Path 5n (M >= N, jobz='N') 
						
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _24c48sks );
						_gghrqcr1 = (((int)3 * _dxpq0xkr) + ILNumerics.F2NET.Intrinsics.MAX(_ev4xhht5 ,_24c48sks ));
					}
					else
					if (_mtv5r2zz)
					{
						//*                 Path 5o (M >= N, jobz='O') 
						
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _n4scph9d );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _smdc8i6p );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _24c48sks );
						_tafa1evd = (_loujht2t + (_ev4xhht5 * _dxpq0xkr));
						_gghrqcr1 = (((int)3 * _dxpq0xkr) + ILNumerics.F2NET.Intrinsics.MAX(_ev4xhht5 ,(_dxpq0xkr * _dxpq0xkr) + _24c48sks ));
					}
					else
					if (_ngfvoqx1)
					{
						//*                 Path 5s (M >= N, jobz='S') 
						
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _smdc8i6p );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _n4scph9d );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _24c48sks );
						_gghrqcr1 = (((int)3 * _dxpq0xkr) + ILNumerics.F2NET.Intrinsics.MAX(_ev4xhht5 ,_24c48sks ));
					}
					else
					if (_6ecs6pbo)
					{
						//*                 Path 5a (M >= N, jobz='A') 
						
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _jt4zf2dk );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _n4scph9d );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _24c48sks );
						_gghrqcr1 = (((int)3 * _dxpq0xkr) + ILNumerics.F2NET.Intrinsics.MAX(_ev4xhht5 ,_24c48sks ));
					}
					
				}
				
			}
			else
			if (_qaseb1y7 > (int)0)
			{
				//* 
				//*           Compute space needed for DBDSDC 
				//* 
				
				if (_7y8lc0fu)
				{
					//*              dbdsdc needs only 4*N (or 6*N for uplo=L for LAPACK <= 3.6) 
					//*              keep 7*N for backwards compatibility. 
					
					_24c48sks = ((int)7 * _ev4xhht5);
				}
				else
				{
					
					_24c48sks = ((((int)3 * _ev4xhht5) * _ev4xhht5) + ((int)4 * _ev4xhht5));
				}
				//* 
				//*           Compute space preferred for each routine 
				
				_sf2bwwb1(ref _ev4xhht5 ,ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),ref _ev4xhht5 ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_nxtvvvgq = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//* 
				
				_sf2bwwb1(ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ev4xhht5 ,_irk8i6qr ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_sozuprs2 = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//* 
				
				_bxcqf0ji(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ev4xhht5 ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_k1gcshu1 = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//* 
				
				_n6025boa(ref _dxpq0xkr ,ref _dxpq0xkr ,ref _ev4xhht5 ,(_g7qb61ha+((int)1 - 1)),ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_38fj0vxv = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//* 
				
				_n6025boa(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ev4xhht5 ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_1zl3pe8l = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//* 
				
				_cmc4j0e3("P" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_fh5yxlb3 = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//* 
				
				_pwi7fryj("P" ,"R" ,"T" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_g7qb61ha+((int)1 - 1)),ref _ev4xhht5 ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref _ev4xhht5 ,(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_0xfpvc0b = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//* 
				
				_pwi7fryj("P" ,"R" ,"T" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,(_g7qb61ha+((int)1 - 1)),ref _ev4xhht5 ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref _ev4xhht5 ,(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_ega8ttxu = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//* 
				
				_pwi7fryj("P" ,"R" ,"T" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _ev4xhht5 ,(_g7qb61ha+((int)1 - 1)),ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_n4scph9d = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//* 
				
				_pwi7fryj("Q" ,"L" ,"N" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_g7qb61ha+((int)1 - 1)),ref _ev4xhht5 ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref _ev4xhht5 ,(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_jt4zf2dk = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//* 
				
				if (_dxpq0xkr >= _lhzduysr)
				{
					
					if (_7y8lc0fu)
					{
						//* 
						//*                 Path 1t (N >> M, JOBZ='N') 
						//* 
						
						_loujht2t = (_ev4xhht5 + _k1gcshu1);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _sozuprs2 );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_24c48sks + _ev4xhht5 );
						_gghrqcr1 = (_24c48sks + _ev4xhht5);
					}
					else
					if (_mtv5r2zz)
					{
						//* 
						//*                 Path 2t (N >> M, JOBZ='O') 
						//* 
						
						_loujht2t = (_ev4xhht5 + _k1gcshu1);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_ev4xhht5 + _1zl3pe8l );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _sozuprs2 );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _jt4zf2dk );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _0xfpvc0b );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _24c48sks );
						_tafa1evd = (_loujht2t + (((int)2 * _ev4xhht5) * _ev4xhht5));
						_gghrqcr1 = ((_24c48sks + (((int)2 * _ev4xhht5) * _ev4xhht5)) + ((int)3 * _ev4xhht5));
					}
					else
					if (_ngfvoqx1)
					{
						//* 
						//*                 Path 3t (N >> M, JOBZ='S') 
						//* 
						
						_loujht2t = (_ev4xhht5 + _k1gcshu1);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_ev4xhht5 + _1zl3pe8l );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _sozuprs2 );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _jt4zf2dk );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _0xfpvc0b );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _24c48sks );
						_tafa1evd = (_loujht2t + (_ev4xhht5 * _ev4xhht5));
						_gghrqcr1 = ((_24c48sks + (_ev4xhht5 * _ev4xhht5)) + ((int)3 * _ev4xhht5));
					}
					else
					if (_6ecs6pbo)
					{
						//* 
						//*                 Path 4t (N >> M, JOBZ='A') 
						//* 
						
						_loujht2t = (_ev4xhht5 + _k1gcshu1);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_ev4xhht5 + _38fj0vxv );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _sozuprs2 );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _jt4zf2dk );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _0xfpvc0b );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _24c48sks );
						_tafa1evd = (_loujht2t + (_ev4xhht5 * _ev4xhht5));
						_gghrqcr1 = ((_ev4xhht5 * _ev4xhht5) + ILNumerics.F2NET.Intrinsics.MAX(((int)3 * _ev4xhht5) + _24c48sks ,_ev4xhht5 + _dxpq0xkr ));
					}
					
				}
				else
				{
					//* 
					//*              Path 5t (N > M, but not much larger) 
					//* 
					
					_loujht2t = (((int)3 * _ev4xhht5) + _nxtvvvgq);
					if (_7y8lc0fu)
					{
						//*                 Path 5tn (N > M, jobz='N') 
						
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _24c48sks );
						_gghrqcr1 = (((int)3 * _ev4xhht5) + ILNumerics.F2NET.Intrinsics.MAX(_dxpq0xkr ,_24c48sks ));
					}
					else
					if (_mtv5r2zz)
					{
						//*                 Path 5to (N > M, jobz='O') 
						
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _jt4zf2dk );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _ega8ttxu );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _24c48sks );
						_tafa1evd = (_loujht2t + (_ev4xhht5 * _dxpq0xkr));
						_gghrqcr1 = (((int)3 * _ev4xhht5) + ILNumerics.F2NET.Intrinsics.MAX(_dxpq0xkr ,(_ev4xhht5 * _ev4xhht5) + _24c48sks ));
					}
					else
					if (_ngfvoqx1)
					{
						//*                 Path 5ts (N > M, jobz='S') 
						
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _jt4zf2dk );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _ega8ttxu );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _24c48sks );
						_gghrqcr1 = (((int)3 * _ev4xhht5) + ILNumerics.F2NET.Intrinsics.MAX(_dxpq0xkr ,_24c48sks ));
					}
					else
					if (_6ecs6pbo)
					{
						//*                 Path 5ta (N > M, jobz='A') 
						
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _jt4zf2dk );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _n4scph9d );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _24c48sks );
						_gghrqcr1 = (((int)3 * _ev4xhht5) + ILNumerics.F2NET.Intrinsics.MAX(_dxpq0xkr ,_24c48sks ));
					}
					
				}
				
			}
			// 
			
			_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,_gghrqcr1 );
			*(_apig8meb+((int)1 - 1)) = DBLE(_tafa1evd);//* 
			
			if ((_6fnxzlyp < _gghrqcr1) & (!(_lhlgm7z5)))
			{
				
				_gro5yvfo = (int)-12;
			}
			
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("DGESDD" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
		
		_j6vjow1g = _oui78ayq("M" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_g7qb61ha );
		_65mv5f5m = (int)0;
		if ((_j6vjow1g > _d0547bi2) & (_j6vjow1g < _bogm0gwy))
		{
			
			_65mv5f5m = (int)1;
			_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _j6vjow1g ,ref _bogm0gwy ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,ref _bhsiylw4 );
		}
		else
		if (_j6vjow1g > _av7j8yda)
		{
			
			_65mv5f5m = (int)1;
			_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _j6vjow1g ,ref _av7j8yda ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,ref _bhsiylw4 );
		}
		//* 
		
		if (_ev4xhht5 >= _dxpq0xkr)
		{
			//* 
			//*        A has at least as many rows as columns. If A has sufficiently 
			//*        more rows than columns, first reduce using the QR 
			//*        decomposition (if sufficient workspace available) 
			//* 
			
			if (_ev4xhht5 >= _lhzduysr)
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
					//*              Workspace: need   N [tau] + N    [work] 
					//*              Workspace: prefer N [tau] + N*NB [work] 
					//* 
					
					_ac2l6xc0(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Zero out below R 
					//* 
					
					_rta9tuwm("L" ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_vxfgpup9+((int)2 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
					_smxeww0r = (int)1;
					_wx1x93f0 = (_smxeww0r + _dxpq0xkr);
					_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
					_1myocm5q = (_5ke1jwwr + _dxpq0xkr);//* 
					//*              Bidiagonalize R in A 
					//*              Workspace: need   3*N [e, tauq, taup] + N      [work] 
					//*              Workspace: prefer 3*N [e, tauq, taup] + 2*N*NB [work] 
					//* 
					
					_sf2bwwb1(ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
					_1myocm5q = (_smxeww0r + _dxpq0xkr);//* 
					//*              Perform bidiagonal SVD, computing singular values only 
					//*              Workspace: need   N [e] + BDSPAC 
					//* 
					
					_c3rn9m7n("U" ,"N" ,ref _dxpq0xkr ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,_xpllja47 ,(_apig8meb+(_1myocm5q - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					
				}
				else
				if (_mtv5r2zz)
				{
					//* 
					//*              Path 2 (M >> N, JOBZ = 'O') 
					//*              N left singular vectors to be overwritten on A and 
					//*              N right singular vectors to be computed in VT 
					//* 
					
					_m9w6lk7x = (int)1;//* 
					//*              WORK(IR) is LDWRKR by N 
					//* 
					
					if (_6fnxzlyp >= ((((_ocv8fk5c * _dxpq0xkr) + (_dxpq0xkr * _dxpq0xkr)) + ((int)3 * _dxpq0xkr)) + _24c48sks))
					{
						
						_v6sbkzy4 = _ocv8fk5c;
					}
					else
					{
						
						_v6sbkzy4 = ((((_6fnxzlyp - (_dxpq0xkr * _dxpq0xkr)) - ((int)3 * _dxpq0xkr)) - _24c48sks) / _dxpq0xkr);
					}
					
					_q1w15vsx = (_m9w6lk7x + (_v6sbkzy4 * _dxpq0xkr));
					_1myocm5q = (_q1w15vsx + _dxpq0xkr);//* 
					//*              Compute A=Q*R 
					//*              Workspace: need   N*N [R] + N [tau] + N    [work] 
					//*              Workspace: prefer N*N [R] + N [tau] + N*NB [work] 
					//* 
					
					_ac2l6xc0(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Copy R to WORK(IR), zeroing out below it 
					//* 
					
					_hhtvj1kb("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 );
					_rta9tuwm("L" ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_m9w6lk7x + (int)1 - 1)),ref _v6sbkzy4 );//* 
					//*              Generate Q in A 
					//*              Workspace: need   N*N [R] + N [tau] + N    [work] 
					//*              Workspace: prefer N*N [R] + N [tau] + N*NB [work] 
					//* 
					
					_hxix712m(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
					_smxeww0r = _q1w15vsx;
					_wx1x93f0 = (_smxeww0r + _dxpq0xkr);
					_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
					_1myocm5q = (_5ke1jwwr + _dxpq0xkr);//* 
					//*              Bidiagonalize R in WORK(IR) 
					//*              Workspace: need   N*N [R] + 3*N [e, tauq, taup] + N      [work] 
					//*              Workspace: prefer N*N [R] + 3*N [e, tauq, taup] + 2*N*NB [work] 
					//* 
					
					_sf2bwwb1(ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              WORK(IU) is N by N 
					//* 
					
					_j4l29b9c = _1myocm5q;
					_1myocm5q = (_j4l29b9c + (_dxpq0xkr * _dxpq0xkr));//* 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in WORK(IU) and computing right 
					//*              singular vectors of bidiagonal matrix in VT 
					//*              Workspace: need   N*N [R] + 3*N [e, tauq, taup] + N*N [U] + BDSPAC 
					//* 
					
					_c3rn9m7n("U" ,"I" ,ref _dxpq0xkr ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_j4l29b9c - 1)),ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv ,_g7qb61ha ,_xpllja47 ,(_apig8meb+(_1myocm5q - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Overwrite WORK(IU) by left singular vectors of R 
					//*              and VT by right singular vectors of R 
					//*              Workspace: need   N*N [R] + 3*N [e, tauq, taup] + N*N [U] + N    [work] 
					//*              Workspace: prefer N*N [R] + 3*N [e, tauq, taup] + N*N [U] + N*NB [work] 
					//* 
					
					_pwi7fryj("Q" ,"L" ,"N" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_j4l29b9c - 1)),ref _dxpq0xkr ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
					_pwi7fryj("P" ,"R" ,"T" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_apig8meb+(_5ke1jwwr - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Multiply Q in A by left singular vectors of R in 
					//*              WORK(IU), storing result in WORK(IR) and copying to A 
					//*              Workspace: need   N*N [R] + 3*N [e, tauq, taup] + N*N [U] 
					//*              Workspace: prefer M*N [R] + 3*N [e, tauq, taup] + N*N [U] 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn167 = (System.Int32)((int)1);
						System.Int32 __81fgg2step167 = (System.Int32)(_v6sbkzy4);
						System.Int32 __81fgg2count167;
						for (__81fgg2count167 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn167 + __81fgg2step167) / __81fgg2step167)), _b5p6od9s = __81fgg2dlsvn167; __81fgg2count167 != 0; __81fgg2count167--, _b5p6od9s += (__81fgg2step167)) {

						{
							
							_8rfn4f7g = ILNumerics.F2NET.Intrinsics.MIN((_ev4xhht5 - _b5p6od9s) + (int)1 ,_v6sbkzy4 );
							_5nsxi69c("N" ,"N" ,ref _8rfn4f7g ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_apig8meb+(_j4l29b9c - 1)),ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 );
							_hhtvj1kb("F" ,ref _8rfn4f7g ,ref _dxpq0xkr ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
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
					//*              Workspace: need   N*N [R] + N [tau] + N    [work] 
					//*              Workspace: prefer N*N [R] + N [tau] + N*NB [work] 
					//* 
					
					_ac2l6xc0(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Copy R to WORK(IR), zeroing out below it 
					//* 
					
					_hhtvj1kb("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 );
					_rta9tuwm("L" ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_m9w6lk7x + (int)1 - 1)),ref _v6sbkzy4 );//* 
					//*              Generate Q in A 
					//*              Workspace: need   N*N [R] + N [tau] + N    [work] 
					//*              Workspace: prefer N*N [R] + N [tau] + N*NB [work] 
					//* 
					
					_hxix712m(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
					_smxeww0r = _q1w15vsx;
					_wx1x93f0 = (_smxeww0r + _dxpq0xkr);
					_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
					_1myocm5q = (_5ke1jwwr + _dxpq0xkr);//* 
					//*              Bidiagonalize R in WORK(IR) 
					//*              Workspace: need   N*N [R] + 3*N [e, tauq, taup] + N      [work] 
					//*              Workspace: prefer N*N [R] + 3*N [e, tauq, taup] + 2*N*NB [work] 
					//* 
					
					_sf2bwwb1(ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagoal matrix in U and computing right singular 
					//*              vectors of bidiagonal matrix in VT 
					//*              Workspace: need   N*N [R] + 3*N [e, tauq, taup] + BDSPAC 
					//* 
					
					_c3rn9m7n("U" ,"I" ,ref _dxpq0xkr ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_7u55mqkq ,ref _u6e6d39b ,_xdbczr8u ,ref _h4ibbatv ,_g7qb61ha ,_xpllja47 ,(_apig8meb+(_1myocm5q - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Overwrite U by left singular vectors of R and VT 
					//*              by right singular vectors of R 
					//*              Workspace: need   N*N [R] + 3*N [e, tauq, taup] + N    [work] 
					//*              Workspace: prefer N*N [R] + 3*N [e, tauq, taup] + N*NB [work] 
					//* 
					
					_pwi7fryj("Q" ,"L" ,"N" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_apig8meb+(_wx1x93f0 - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					
					_pwi7fryj("P" ,"R" ,"T" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_apig8meb+(_5ke1jwwr - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Multiply Q in A by left singular vectors of R in 
					//*              WORK(IR), storing result in U 
					//*              Workspace: need   N*N [R] 
					//* 
					
					_hhtvj1kb("F" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 );
					_5nsxi69c("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_kxg5drh2) ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,ref Unsafe.AsRef(_d0547bi2) ,_7u55mqkq ,ref _u6e6d39b );//* 
					
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
					//*              Workspace: need   N*N [U] + N [tau] + N    [work] 
					//*              Workspace: prefer N*N [U] + N [tau] + N*NB [work] 
					//* 
					
					_ac2l6xc0(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
					_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );//* 
					//*              Generate Q in U 
					//*              Workspace: need   N*N [U] + N [tau] + M    [work] 
					//*              Workspace: prefer N*N [U] + N [tau] + M*NB [work] 
					
					_hxix712m(ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Produce R in A, zeroing out other entries 
					//* 
					
					_rta9tuwm("L" ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_vxfgpup9+((int)2 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
					_smxeww0r = _q1w15vsx;
					_wx1x93f0 = (_smxeww0r + _dxpq0xkr);
					_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
					_1myocm5q = (_5ke1jwwr + _dxpq0xkr);//* 
					//*              Bidiagonalize R in A 
					//*              Workspace: need   N*N [U] + 3*N [e, tauq, taup] + N      [work] 
					//*              Workspace: prefer N*N [U] + 3*N [e, tauq, taup] + 2*N*NB [work] 
					//* 
					
					_sf2bwwb1(ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in WORK(IU) and computing right 
					//*              singular vectors of bidiagonal matrix in VT 
					//*              Workspace: need   N*N [U] + 3*N [e, tauq, taup] + BDSPAC 
					//* 
					
					_c3rn9m7n("U" ,"I" ,ref _dxpq0xkr ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_j4l29b9c - 1)),ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv ,_g7qb61ha ,_xpllja47 ,(_apig8meb+(_1myocm5q - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Overwrite WORK(IU) by left singular vectors of R and VT 
					//*              by right singular vectors of R 
					//*              Workspace: need   N*N [U] + 3*N [e, tauq, taup] + N    [work] 
					//*              Workspace: prefer N*N [U] + 3*N [e, tauq, taup] + N*NB [work] 
					//* 
					
					_pwi7fryj("Q" ,"L" ,"N" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
					_pwi7fryj("P" ,"R" ,"T" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Multiply Q in U by left singular vectors of R in 
					//*              WORK(IU), storing result in A 
					//*              Workspace: need   N*N [U] 
					//* 
					
					_5nsxi69c("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_kxg5drh2) ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,ref Unsafe.AsRef(_d0547bi2) ,_vxfgpup9 ,ref _ocv8fk5c );//* 
					//*              Copy left singular vectors of A from A to U 
					//* 
					
					_hhtvj1kb("F" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );//* 
					
				}
				//* 
				
			}
			else
			{
				//* 
				//*           M .LT. MNTHR 
				//* 
				//*           Path 5 (M >= N, but not much larger) 
				//*           Reduce to bidiagonal form without QR decomposition 
				//* 
				
				_smxeww0r = (int)1;
				_wx1x93f0 = (_smxeww0r + _dxpq0xkr);
				_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
				_1myocm5q = (_5ke1jwwr + _dxpq0xkr);//* 
				//*           Bidiagonalize A 
				//*           Workspace: need   3*N [e, tauq, taup] + M        [work] 
				//*           Workspace: prefer 3*N [e, tauq, taup] + (M+N)*NB [work] 
				//* 
				
				_sf2bwwb1(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
				if (_7y8lc0fu)
				{
					//* 
					//*              Path 5n (M >= N, JOBZ='N') 
					//*              Perform bidiagonal SVD, only computing singular values 
					//*              Workspace: need   3*N [e, tauq, taup] + BDSPAC 
					//* 
					
					_c3rn9m7n("U" ,"N" ,ref _dxpq0xkr ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,_xpllja47 ,(_apig8meb+(_1myocm5q - 1)),_4b6rt45i ,ref _gro5yvfo );
				}
				else
				if (_mtv5r2zz)
				{
					//*              Path 5o (M >= N, JOBZ='O') 
					
					_j4l29b9c = _1myocm5q;
					if (_6fnxzlyp >= (((_ev4xhht5 * _dxpq0xkr) + ((int)3 * _dxpq0xkr)) + _24c48sks))
					{
						//* 
						//*                 WORK( IU ) is M by N 
						//* 
						
						_lbvz22dx = _ev4xhht5;
						_1myocm5q = (_j4l29b9c + (_lbvz22dx * _dxpq0xkr));
						_rta9tuwm("F" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx );//*                 IR is unused; silence compile warnings 
						
						_m9w6lk7x = (int)-1;
					}
					else
					{
						//* 
						//*                 WORK( IU ) is N by N 
						//* 
						
						_lbvz22dx = _dxpq0xkr;
						_1myocm5q = (_j4l29b9c + (_lbvz22dx * _dxpq0xkr));//* 
						//*                 WORK(IR) is LDWRKR by N 
						//* 
						
						_m9w6lk7x = _1myocm5q;
						_v6sbkzy4 = (((_6fnxzlyp - (_dxpq0xkr * _dxpq0xkr)) - ((int)3 * _dxpq0xkr)) / _dxpq0xkr);
					}
					
					_1myocm5q = (_j4l29b9c + (_lbvz22dx * _dxpq0xkr));//* 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in WORK(IU) and computing right 
					//*              singular vectors of bidiagonal matrix in VT 
					//*              Workspace: need   3*N [e, tauq, taup] + N*N [U] + BDSPAC 
					//* 
					
					_c3rn9m7n("U" ,"I" ,ref _dxpq0xkr ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,_xdbczr8u ,ref _h4ibbatv ,_g7qb61ha ,_xpllja47 ,(_apig8meb+(_1myocm5q - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Overwrite VT by right singular vectors of A 
					//*              Workspace: need   3*N [e, tauq, taup] + N*N [U] + N    [work] 
					//*              Workspace: prefer 3*N [e, tauq, taup] + N*N [U] + N*NB [work] 
					//* 
					
					_pwi7fryj("P" ,"R" ,"T" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					
					if (_6fnxzlyp >= (((_ev4xhht5 * _dxpq0xkr) + ((int)3 * _dxpq0xkr)) + _24c48sks))
					{
						//* 
						//*                 Path 5o-fast 
						//*                 Overwrite WORK(IU) by left singular vectors of A 
						//*                 Workspace: need   3*N [e, tauq, taup] + M*N [U] + N    [work] 
						//*                 Workspace: prefer 3*N [e, tauq, taup] + M*N [U] + N*NB [work] 
						//* 
						
						_pwi7fryj("Q" ,"L" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
						//*                 Copy left singular vectors of A from WORK(IU) to A 
						//* 
						
						_hhtvj1kb("F" ,ref _ev4xhht5 ,ref _dxpq0xkr ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,_vxfgpup9 ,ref _ocv8fk5c );
					}
					else
					{
						//* 
						//*                 Path 5o-slow 
						//*                 Generate Q in A 
						//*                 Workspace: need   3*N [e, tauq, taup] + N*N [U] + N    [work] 
						//*                 Workspace: prefer 3*N [e, tauq, taup] + N*N [U] + N*NB [work] 
						//* 
						
						_cmc4j0e3("Q" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
						//*                 Multiply Q in A by left singular vectors of 
						//*                 bidiagonal matrix in WORK(IU), storing result in 
						//*                 WORK(IR) and copying to A 
						//*                 Workspace: need   3*N [e, tauq, taup] + N*N [U] + NB*N [R] 
						//*                 Workspace: prefer 3*N [e, tauq, taup] + N*N [U] + M*N  [R] 
						//* 
						
						{
							System.Int32 __81fgg2dlsvn168 = (System.Int32)((int)1);
							System.Int32 __81fgg2step168 = (System.Int32)(_v6sbkzy4);
							System.Int32 __81fgg2count168;
							for (__81fgg2count168 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn168 + __81fgg2step168) / __81fgg2step168)), _b5p6od9s = __81fgg2dlsvn168; __81fgg2count168 != 0; __81fgg2count168--, _b5p6od9s += (__81fgg2step168)) {

							{
								
								_8rfn4f7g = ILNumerics.F2NET.Intrinsics.MIN((_ev4xhht5 - _b5p6od9s) + (int)1 ,_v6sbkzy4 );
								_5nsxi69c("N" ,"N" ,ref _8rfn4f7g ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 );
								_hhtvj1kb("F" ,ref _8rfn4f7g ,ref _dxpq0xkr ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
Mark20:;
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
					//*              Path 5s (M >= N, JOBZ='S') 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in U and computing right singular 
					//*              vectors of bidiagonal matrix in VT 
					//*              Workspace: need   3*N [e, tauq, taup] + BDSPAC 
					//* 
					
					_rta9tuwm("F" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,_7u55mqkq ,ref _u6e6d39b );
					_c3rn9m7n("U" ,"I" ,ref _dxpq0xkr ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_7u55mqkq ,ref _u6e6d39b ,_xdbczr8u ,ref _h4ibbatv ,_g7qb61ha ,_xpllja47 ,(_apig8meb+(_1myocm5q - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Overwrite U by left singular vectors of A and VT 
					//*              by right singular vectors of A 
					//*              Workspace: need   3*N [e, tauq, taup] + N    [work] 
					//*              Workspace: prefer 3*N [e, tauq, taup] + N*NB [work] 
					//* 
					
					_pwi7fryj("Q" ,"L" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
					_pwi7fryj("P" ,"R" ,"T" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
				}
				else
				if (_6ecs6pbo)
				{
					//* 
					//*              Path 5a (M >= N, JOBZ='A') 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in U and computing right singular 
					//*              vectors of bidiagonal matrix in VT 
					//*              Workspace: need   3*N [e, tauq, taup] + BDSPAC 
					//* 
					
					_rta9tuwm("F" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,_7u55mqkq ,ref _u6e6d39b );
					_c3rn9m7n("U" ,"I" ,ref _dxpq0xkr ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_7u55mqkq ,ref _u6e6d39b ,_xdbczr8u ,ref _h4ibbatv ,_g7qb61ha ,_xpllja47 ,(_apig8meb+(_1myocm5q - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Set the right corner of U to identity matrix 
					//* 
					
					if (_ev4xhht5 > _dxpq0xkr)
					{
						
						_rta9tuwm("F" ,ref Unsafe.AsRef(_ev4xhht5 - _dxpq0xkr) ,ref Unsafe.AsRef(_ev4xhht5 - _dxpq0xkr) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,(_7u55mqkq+(_dxpq0xkr + (int)1 - 1) + (_dxpq0xkr + (int)1 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b );
					}
					//* 
					//*              Overwrite U by left singular vectors of A and VT 
					//*              by right singular vectors of A 
					//*              Workspace: need   3*N [e, tauq, taup] + M    [work] 
					//*              Workspace: prefer 3*N [e, tauq, taup] + M*NB [work] 
					//* 
					
					_pwi7fryj("Q" ,"L" ,"N" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
					_pwi7fryj("P" ,"R" ,"T" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
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
			
			if (_dxpq0xkr >= _lhzduysr)
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
					//*              Workspace: need   M [tau] + M [work] 
					//*              Workspace: prefer M [tau] + M*NB [work] 
					//* 
					
					_bxcqf0ji(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Zero out above L 
					//* 
					
					_rta9tuwm("U" ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_vxfgpup9+((int)1 - 1) + ((int)2 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
					_smxeww0r = (int)1;
					_wx1x93f0 = (_smxeww0r + _ev4xhht5);
					_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
					_1myocm5q = (_5ke1jwwr + _ev4xhht5);//* 
					//*              Bidiagonalize L in A 
					//*              Workspace: need   3*M [e, tauq, taup] + M      [work] 
					//*              Workspace: prefer 3*M [e, tauq, taup] + 2*M*NB [work] 
					//* 
					
					_sf2bwwb1(ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
					_1myocm5q = (_smxeww0r + _ev4xhht5);//* 
					//*              Perform bidiagonal SVD, computing singular values only 
					//*              Workspace: need   M [e] + BDSPAC 
					//* 
					
					_c3rn9m7n("U" ,"N" ,ref _ev4xhht5 ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,_xpllja47 ,(_apig8meb+(_1myocm5q - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					
				}
				else
				if (_mtv5r2zz)
				{
					//* 
					//*              Path 2t (N >> M, JOBZ='O') 
					//*              M right singular vectors to be overwritten on A and 
					//*              M left singular vectors to be computed in U 
					//* 
					
					_gt43n8d1 = (int)1;//* 
					//*              WORK(IVT) is M by M 
					//*              WORK(IL)  is M by M; it is later resized to M by chunk for gemm 
					//* 
					
					_ic6kua09 = (_gt43n8d1 + (_ev4xhht5 * _ev4xhht5));
					if (_6fnxzlyp >= ((((_ev4xhht5 * _dxpq0xkr) + (_ev4xhht5 * _ev4xhht5)) + ((int)3 * _ev4xhht5)) + _24c48sks))
					{
						
						_j49hoimx = _ev4xhht5;
						_8rfn4f7g = _dxpq0xkr;
					}
					else
					{
						
						_j49hoimx = _ev4xhht5;
						_8rfn4f7g = ((_6fnxzlyp - (_ev4xhht5 * _ev4xhht5)) / _ev4xhht5);
					}
					
					_q1w15vsx = (_ic6kua09 + (_j49hoimx * _ev4xhht5));
					_1myocm5q = (_q1w15vsx + _ev4xhht5);//* 
					//*              Compute A=L*Q 
					//*              Workspace: need   M*M [VT] + M*M [L] + M [tau] + M    [work] 
					//*              Workspace: prefer M*M [VT] + M*M [L] + M [tau] + M*NB [work] 
					//* 
					
					_bxcqf0ji(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Copy L to WORK(IL), zeroing about above it 
					//* 
					
					_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_ic6kua09 - 1)),ref _j49hoimx );
					_rta9tuwm("U" ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_ic6kua09 + _j49hoimx - 1)),ref _j49hoimx );//* 
					//*              Generate Q in A 
					//*              Workspace: need   M*M [VT] + M*M [L] + M [tau] + M    [work] 
					//*              Workspace: prefer M*M [VT] + M*M [L] + M [tau] + M*NB [work] 
					//* 
					
					_n6025boa(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
					_smxeww0r = _q1w15vsx;
					_wx1x93f0 = (_smxeww0r + _ev4xhht5);
					_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
					_1myocm5q = (_5ke1jwwr + _ev4xhht5);//* 
					//*              Bidiagonalize L in WORK(IL) 
					//*              Workspace: need   M*M [VT] + M*M [L] + 3*M [e, tauq, taup] + M      [work] 
					//*              Workspace: prefer M*M [VT] + M*M [L] + 3*M [e, tauq, taup] + 2*M*NB [work] 
					//* 
					
					_sf2bwwb1(ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_ic6kua09 - 1)),ref _j49hoimx ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in U, and computing right singular 
					//*              vectors of bidiagonal matrix in WORK(IVT) 
					//*              Workspace: need   M*M [VT] + M*M [L] + 3*M [e, tauq, taup] + BDSPAC 
					//* 
					
					_c3rn9m7n("U" ,"I" ,ref _ev4xhht5 ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_gt43n8d1 - 1)),ref _ev4xhht5 ,_g7qb61ha ,_xpllja47 ,(_apig8meb+(_1myocm5q - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Overwrite U by left singular vectors of L and WORK(IVT) 
					//*              by right singular vectors of L 
					//*              Workspace: need   M*M [VT] + M*M [L] + 3*M [e, tauq, taup] + M    [work] 
					//*              Workspace: prefer M*M [VT] + M*M [L] + 3*M [e, tauq, taup] + M*NB [work] 
					//* 
					
					_pwi7fryj("Q" ,"L" ,"N" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_ic6kua09 - 1)),ref _j49hoimx ,(_apig8meb+(_wx1x93f0 - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
					_pwi7fryj("P" ,"R" ,"T" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_ic6kua09 - 1)),ref _j49hoimx ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_gt43n8d1 - 1)),ref _ev4xhht5 ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Multiply right singular vectors of L in WORK(IVT) by Q 
					//*              in A, storing result in WORK(IL) and copying to A 
					//*              Workspace: need   M*M [VT] + M*M [L] 
					//*              Workspace: prefer M*M [VT] + M*N [L] 
					//*              At this point, L is resized as M by chunk. 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn169 = (System.Int32)((int)1);
						System.Int32 __81fgg2step169 = (System.Int32)(_8rfn4f7g);
						System.Int32 __81fgg2count169;
						for (__81fgg2count169 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn169 + __81fgg2step169) / __81fgg2step169)), _b5p6od9s = __81fgg2dlsvn169; __81fgg2count169 != 0; __81fgg2count169--, _b5p6od9s += (__81fgg2step169)) {

						{
							
							_9giy8o3g = ILNumerics.F2NET.Intrinsics.MIN((_dxpq0xkr - _b5p6od9s) + (int)1 ,_8rfn4f7g );
							_5nsxi69c("N" ,"N" ,ref _ev4xhht5 ,ref _9giy8o3g ,ref _ev4xhht5 ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+(_gt43n8d1 - 1)),ref _ev4xhht5 ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_ic6kua09 - 1)),ref _j49hoimx );
							_hhtvj1kb("F" ,ref _ev4xhht5 ,ref _9giy8o3g ,(_apig8meb+(_ic6kua09 - 1)),ref _j49hoimx ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
Mark30:;
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
					//*              Workspace: need   M*M [L] + M [tau] + M    [work] 
					//*              Workspace: prefer M*M [L] + M [tau] + M*NB [work] 
					//* 
					
					_bxcqf0ji(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Copy L to WORK(IL), zeroing out above it 
					//* 
					
					_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_ic6kua09 - 1)),ref _j49hoimx );
					_rta9tuwm("U" ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_ic6kua09 + _j49hoimx - 1)),ref _j49hoimx );//* 
					//*              Generate Q in A 
					//*              Workspace: need   M*M [L] + M [tau] + M    [work] 
					//*              Workspace: prefer M*M [L] + M [tau] + M*NB [work] 
					//* 
					
					_n6025boa(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
					_smxeww0r = _q1w15vsx;
					_wx1x93f0 = (_smxeww0r + _ev4xhht5);
					_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
					_1myocm5q = (_5ke1jwwr + _ev4xhht5);//* 
					//*              Bidiagonalize L in WORK(IU). 
					//*              Workspace: need   M*M [L] + 3*M [e, tauq, taup] + M      [work] 
					//*              Workspace: prefer M*M [L] + 3*M [e, tauq, taup] + 2*M*NB [work] 
					//* 
					
					_sf2bwwb1(ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_ic6kua09 - 1)),ref _j49hoimx ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in U and computing right singular 
					//*              vectors of bidiagonal matrix in VT 
					//*              Workspace: need   M*M [L] + 3*M [e, tauq, taup] + BDSPAC 
					//* 
					
					_c3rn9m7n("U" ,"I" ,ref _ev4xhht5 ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_7u55mqkq ,ref _u6e6d39b ,_xdbczr8u ,ref _h4ibbatv ,_g7qb61ha ,_xpllja47 ,(_apig8meb+(_1myocm5q - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Overwrite U by left singular vectors of L and VT 
					//*              by right singular vectors of L 
					//*              Workspace: need   M*M [L] + 3*M [e, tauq, taup] + M    [work] 
					//*              Workspace: prefer M*M [L] + 3*M [e, tauq, taup] + M*NB [work] 
					//* 
					
					_pwi7fryj("Q" ,"L" ,"N" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_ic6kua09 - 1)),ref _j49hoimx ,(_apig8meb+(_wx1x93f0 - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
					_pwi7fryj("P" ,"R" ,"T" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_ic6kua09 - 1)),ref _j49hoimx ,(_apig8meb+(_5ke1jwwr - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Multiply right singular vectors of L in WORK(IL) by 
					//*              Q in A, storing result in VT 
					//*              Workspace: need   M*M [L] 
					//* 
					
					_hhtvj1kb("F" ,ref _ev4xhht5 ,ref _ev4xhht5 ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_ic6kua09 - 1)),ref _j49hoimx );
					_5nsxi69c("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+(_ic6kua09 - 1)),ref _j49hoimx ,_vxfgpup9 ,ref _ocv8fk5c ,ref Unsafe.AsRef(_d0547bi2) ,_xdbczr8u ,ref _h4ibbatv );//* 
					
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
					//*              Workspace: need   M*M [VT] + M [tau] + M    [work] 
					//*              Workspace: prefer M*M [VT] + M [tau] + M*NB [work] 
					//* 
					
					_bxcqf0ji(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
					_hhtvj1kb("U" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );//* 
					//*              Generate Q in VT 
					//*              Workspace: need   M*M [VT] + M [tau] + N    [work] 
					//*              Workspace: prefer M*M [VT] + M [tau] + N*NB [work] 
					//* 
					
					_n6025boa(ref _dxpq0xkr ,ref _dxpq0xkr ,ref _ev4xhht5 ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Produce L in A, zeroing out other entries 
					//* 
					
					_rta9tuwm("U" ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_vxfgpup9+((int)1 - 1) + ((int)2 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
					_smxeww0r = _q1w15vsx;
					_wx1x93f0 = (_smxeww0r + _ev4xhht5);
					_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
					_1myocm5q = (_5ke1jwwr + _ev4xhht5);//* 
					//*              Bidiagonalize L in A 
					//*              Workspace: need   M*M [VT] + 3*M [e, tauq, taup] + M      [work] 
					//*              Workspace: prefer M*M [VT] + 3*M [e, tauq, taup] + 2*M*NB [work] 
					//* 
					
					_sf2bwwb1(ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in U and computing right singular 
					//*              vectors of bidiagonal matrix in WORK(IVT) 
					//*              Workspace: need   M*M [VT] + 3*M [e, tauq, taup] + BDSPAC 
					//* 
					
					_c3rn9m7n("U" ,"I" ,ref _ev4xhht5 ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_gt43n8d1 - 1)),ref _r9baffok ,_g7qb61ha ,_xpllja47 ,(_apig8meb+(_1myocm5q - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Overwrite U by left singular vectors of L and WORK(IVT) 
					//*              by right singular vectors of L 
					//*              Workspace: need   M*M [VT] + 3*M [e, tauq, taup]+ M    [work] 
					//*              Workspace: prefer M*M [VT] + 3*M [e, tauq, taup]+ M*NB [work] 
					//* 
					
					_pwi7fryj("Q" ,"L" ,"N" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
					_pwi7fryj("P" ,"R" ,"T" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_gt43n8d1 - 1)),ref _r9baffok ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Multiply right singular vectors of L in WORK(IVT) by 
					//*              Q in VT, storing result in A 
					//*              Workspace: need   M*M [VT] 
					//* 
					
					_5nsxi69c("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+(_gt43n8d1 - 1)),ref _r9baffok ,_xdbczr8u ,ref _h4ibbatv ,ref Unsafe.AsRef(_d0547bi2) ,_vxfgpup9 ,ref _ocv8fk5c );//* 
					//*              Copy right singular vectors of A from A to VT 
					//* 
					
					_hhtvj1kb("F" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );//* 
					
				}
				//* 
				
			}
			else
			{
				//* 
				//*           N .LT. MNTHR 
				//* 
				//*           Path 5t (N > M, but not much larger) 
				//*           Reduce to bidiagonal form without LQ decomposition 
				//* 
				
				_smxeww0r = (int)1;
				_wx1x93f0 = (_smxeww0r + _ev4xhht5);
				_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
				_1myocm5q = (_5ke1jwwr + _ev4xhht5);//* 
				//*           Bidiagonalize A 
				//*           Workspace: need   3*M [e, tauq, taup] + N        [work] 
				//*           Workspace: prefer 3*M [e, tauq, taup] + (M+N)*NB [work] 
				//* 
				
				_sf2bwwb1(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
				if (_7y8lc0fu)
				{
					//* 
					//*              Path 5tn (N > M, JOBZ='N') 
					//*              Perform bidiagonal SVD, only computing singular values 
					//*              Workspace: need   3*M [e, tauq, taup] + BDSPAC 
					//* 
					
					_c3rn9m7n("L" ,"N" ,ref _ev4xhht5 ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,_xpllja47 ,(_apig8meb+(_1myocm5q - 1)),_4b6rt45i ,ref _gro5yvfo );
				}
				else
				if (_mtv5r2zz)
				{
					//*              Path 5to (N > M, JOBZ='O') 
					
					_r9baffok = _ev4xhht5;
					_gt43n8d1 = _1myocm5q;
					if (_6fnxzlyp >= (((_ev4xhht5 * _dxpq0xkr) + ((int)3 * _ev4xhht5)) + _24c48sks))
					{
						//* 
						//*                 WORK( IVT ) is M by N 
						//* 
						
						_rta9tuwm("F" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_gt43n8d1 - 1)),ref _r9baffok );
						_1myocm5q = (_gt43n8d1 + (_r9baffok * _dxpq0xkr));//*                 IL is unused; silence compile warnings 
						
						_ic6kua09 = (int)-1;
					}
					else
					{
						//* 
						//*                 WORK( IVT ) is M by M 
						//* 
						
						_1myocm5q = (_gt43n8d1 + (_r9baffok * _ev4xhht5));
						_ic6kua09 = _1myocm5q;//* 
						//*                 WORK(IL) is M by CHUNK 
						//* 
						
						_8rfn4f7g = (((_6fnxzlyp - (_ev4xhht5 * _ev4xhht5)) - ((int)3 * _ev4xhht5)) / _ev4xhht5);
					}
					//* 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in U and computing right singular 
					//*              vectors of bidiagonal matrix in WORK(IVT) 
					//*              Workspace: need   3*M [e, tauq, taup] + M*M [VT] + BDSPAC 
					//* 
					
					_c3rn9m7n("L" ,"I" ,ref _ev4xhht5 ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_gt43n8d1 - 1)),ref _r9baffok ,_g7qb61ha ,_xpllja47 ,(_apig8meb+(_1myocm5q - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Overwrite U by left singular vectors of A 
					//*              Workspace: need   3*M [e, tauq, taup] + M*M [VT] + M    [work] 
					//*              Workspace: prefer 3*M [e, tauq, taup] + M*M [VT] + M*NB [work] 
					//* 
					
					_pwi7fryj("Q" ,"L" ,"N" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
					
					if (_6fnxzlyp >= (((_ev4xhht5 * _dxpq0xkr) + ((int)3 * _ev4xhht5)) + _24c48sks))
					{
						//* 
						//*                 Path 5to-fast 
						//*                 Overwrite WORK(IVT) by left singular vectors of A 
						//*                 Workspace: need   3*M [e, tauq, taup] + M*N [VT] + M    [work] 
						//*                 Workspace: prefer 3*M [e, tauq, taup] + M*N [VT] + M*NB [work] 
						//* 
						
						_pwi7fryj("P" ,"R" ,"T" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_gt43n8d1 - 1)),ref _r9baffok ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
						//*                 Copy right singular vectors of A from WORK(IVT) to A 
						//* 
						
						_hhtvj1kb("F" ,ref _ev4xhht5 ,ref _dxpq0xkr ,(_apig8meb+(_gt43n8d1 - 1)),ref _r9baffok ,_vxfgpup9 ,ref _ocv8fk5c );
					}
					else
					{
						//* 
						//*                 Path 5to-slow 
						//*                 Generate P**T in A 
						//*                 Workspace: need   3*M [e, tauq, taup] + M*M [VT] + M    [work] 
						//*                 Workspace: prefer 3*M [e, tauq, taup] + M*M [VT] + M*NB [work] 
						//* 
						
						_cmc4j0e3("P" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );//* 
						//*                 Multiply Q in A by right singular vectors of 
						//*                 bidiagonal matrix in WORK(IVT), storing result in 
						//*                 WORK(IL) and copying to A 
						//*                 Workspace: need   3*M [e, tauq, taup] + M*M [VT] + M*NB [L] 
						//*                 Workspace: prefer 3*M [e, tauq, taup] + M*M [VT] + M*N  [L] 
						//* 
						
						{
							System.Int32 __81fgg2dlsvn170 = (System.Int32)((int)1);
							System.Int32 __81fgg2step170 = (System.Int32)(_8rfn4f7g);
							System.Int32 __81fgg2count170;
							for (__81fgg2count170 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn170 + __81fgg2step170) / __81fgg2step170)), _b5p6od9s = __81fgg2dlsvn170; __81fgg2count170 != 0; __81fgg2count170--, _b5p6od9s += (__81fgg2step170)) {

							{
								
								_9giy8o3g = ILNumerics.F2NET.Intrinsics.MIN((_dxpq0xkr - _b5p6od9s) + (int)1 ,_8rfn4f7g );
								_5nsxi69c("N" ,"N" ,ref _ev4xhht5 ,ref _9giy8o3g ,ref _ev4xhht5 ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+(_gt43n8d1 - 1)),ref _r9baffok ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_ic6kua09 - 1)),ref _ev4xhht5 );
								_hhtvj1kb("F" ,ref _ev4xhht5 ,ref _9giy8o3g ,(_apig8meb+(_ic6kua09 - 1)),ref _ev4xhht5 ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
Mark40:;
								// continue
							}
														}						}
					}
					
				}
				else
				if (_ngfvoqx1)
				{
					//* 
					//*              Path 5ts (N > M, JOBZ='S') 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in U and computing right singular 
					//*              vectors of bidiagonal matrix in VT 
					//*              Workspace: need   3*M [e, tauq, taup] + BDSPAC 
					//* 
					
					_rta9tuwm("F" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,_xdbczr8u ,ref _h4ibbatv );
					_c3rn9m7n("L" ,"I" ,ref _ev4xhht5 ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_7u55mqkq ,ref _u6e6d39b ,_xdbczr8u ,ref _h4ibbatv ,_g7qb61ha ,_xpllja47 ,(_apig8meb+(_1myocm5q - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Overwrite U by left singular vectors of A and VT 
					//*              by right singular vectors of A 
					//*              Workspace: need   3*M [e, tauq, taup] + M    [work] 
					//*              Workspace: prefer 3*M [e, tauq, taup] + M*NB [work] 
					//* 
					
					_pwi7fryj("Q" ,"L" ,"N" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
					_pwi7fryj("P" ,"R" ,"T" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
				}
				else
				if (_6ecs6pbo)
				{
					//* 
					//*              Path 5ta (N > M, JOBZ='A') 
					//*              Perform bidiagonal SVD, computing left singular vectors 
					//*              of bidiagonal matrix in U and computing right singular 
					//*              vectors of bidiagonal matrix in VT 
					//*              Workspace: need   3*M [e, tauq, taup] + BDSPAC 
					//* 
					
					_rta9tuwm("F" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,_xdbczr8u ,ref _h4ibbatv );
					_c3rn9m7n("L" ,"I" ,ref _ev4xhht5 ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_7u55mqkq ,ref _u6e6d39b ,_xdbczr8u ,ref _h4ibbatv ,_g7qb61ha ,_xpllja47 ,(_apig8meb+(_1myocm5q - 1)),_4b6rt45i ,ref _gro5yvfo );//* 
					//*              Set the right corner of VT to identity matrix 
					//* 
					
					if (_dxpq0xkr > _ev4xhht5)
					{
						
						_rta9tuwm("F" ,ref Unsafe.AsRef(_dxpq0xkr - _ev4xhht5) ,ref Unsafe.AsRef(_dxpq0xkr - _ev4xhht5) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_kxg5drh2) ,(_xdbczr8u+(_ev4xhht5 + (int)1 - 1) + (_ev4xhht5 + (int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv );
					}
					//* 
					//*              Overwrite U by left singular vectors of A and VT 
					//*              by right singular vectors of A 
					//*              Workspace: need   3*M [e, tauq, taup] + N    [work] 
					//*              Workspace: prefer 3*M [e, tauq, taup] + N*NB [work] 
					//* 
					
					_pwi7fryj("Q" ,"L" ,"N" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
					_pwi7fryj("P" ,"R" ,"T" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _bhsiylw4 );
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
			if (_j6vjow1g < _bogm0gwy)
			_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _bogm0gwy ,ref _j6vjow1g ,ref _qaseb1y7 ,ref Unsafe.AsRef((int)1) ,_irk8i6qr ,ref _qaseb1y7 ,ref _bhsiylw4 );
		}
		//* 
		//*     Return optimal workspace in WORK(1) 
		//* 
		
		*(_apig8meb+((int)1 - 1)) = DBLE(_tafa1evd);//* 
		
		return;//* 
		//*     End of DGESDD 
		//* 
		
	}
	
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
