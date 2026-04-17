
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
//*> \brief <b> DGESVD computes the singular value decomposition (SVD) for GE matrices</b> 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DGESVD + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dgesvd.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dgesvd.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dgesvd.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DGESVD( JOBU, JOBVT, M, N, A, LDA, S, U, LDU, VT, LDVT, 
//*                          WORK, LWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          JOBU, JOBVT 
//*       INTEGER            INFO, LDA, LDU, LDVT, LWORK, M, N 
//*       .. 
//*       .. Array Arguments .. 
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
//*> DGESVD computes the singular value decomposition (SVD) of a real 
//*> M-by-N matrix A, optionally computing the left and/or right singular 
//*> vectors. The SVD is written 
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
//*> Note that the routine returns V**T, not V. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] JOBU 
//*> \verbatim 
//*>          JOBU is CHARACTER*1 
//*>          Specifies options for computing all or part of the matrix U: 
//*>          = 'A':  all M columns of U are returned in array U: 
//*>          = 'S':  the first min(m,n) columns of U (the left singular 
//*>                  vectors) are returned in the array U; 
//*>          = 'O':  the first min(m,n) columns of U (the left singular 
//*>                  vectors) are overwritten on the array A; 
//*>          = 'N':  no columns of U (no left singular vectors) are 
//*>                  computed. 
//*> \endverbatim 
//*> 
//*> \param[in] JOBVT 
//*> \verbatim 
//*>          JOBVT is CHARACTER*1 
//*>          Specifies options for computing all or part of the matrix 
//*>          V**T: 
//*>          = 'A':  all N rows of V**T are returned in the array VT; 
//*>          = 'S':  the first min(m,n) rows of V**T (the right singular 
//*>                  vectors) are returned in the array VT; 
//*>          = 'O':  the first min(m,n) rows of V**T (the right singular 
//*>                  vectors) are overwritten on the array A; 
//*>          = 'N':  no rows of V**T (no right singular vectors) are 
//*>                  computed. 
//*> 
//*>          JOBVT and JOBU cannot both be 'O'. 
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
//*>          if JOBU = 'O',  A is overwritten with the first min(m,n) 
//*>                          columns of U (the left singular vectors, 
//*>                          stored columnwise); 
//*>          if JOBVT = 'O', A is overwritten with the first min(m,n) 
//*>                          rows of V**T (the right singular vectors, 
//*>                          stored rowwise); 
//*>          if JOBU .ne. 'O' and JOBVT .ne. 'O', the contents of A 
//*>                          are destroyed. 
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
//*>          (LDU,M) if JOBU = 'A' or (LDU,min(M,N)) if JOBU = 'S'. 
//*>          If JOBU = 'A', U contains the M-by-M orthogonal matrix U; 
//*>          if JOBU = 'S', U contains the first min(m,n) columns of U 
//*>          (the left singular vectors, stored columnwise); 
//*>          if JOBU = 'N' or 'O', U is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] LDU 
//*> \verbatim 
//*>          LDU is INTEGER 
//*>          The leading dimension of the array U.  LDU >= 1; if 
//*>          JOBU = 'S' or 'A', LDU >= M. 
//*> \endverbatim 
//*> 
//*> \param[out] VT 
//*> \verbatim 
//*>          VT is DOUBLE PRECISION array, dimension (LDVT,N) 
//*>          If JOBVT = 'A', VT contains the N-by-N orthogonal matrix 
//*>          V**T; 
//*>          if JOBVT = 'S', VT contains the first min(m,n) rows of 
//*>          V**T (the right singular vectors, stored rowwise); 
//*>          if JOBVT = 'N' or 'O', VT is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] LDVT 
//*> \verbatim 
//*>          LDVT is INTEGER 
//*>          The leading dimension of the array VT.  LDVT >= 1; if 
//*>          JOBVT = 'A', LDVT >= N; if JOBVT = 'S', LDVT >= min(M,N). 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is DOUBLE PRECISION array, dimension (MAX(1,LWORK)) 
//*>          On exit, if INFO = 0, WORK(1) returns the optimal LWORK; 
//*>          if INFO > 0, WORK(2:MIN(M,N)) contains the unconverged 
//*>          superdiagonal elements of an upper bidiagonal matrix B 
//*>          whose diagonal is in S (not necessarily sorted). B 
//*>          satisfies A = U * B * VT, so it has the same singular values 
//*>          as A, and singular vectors related by U and VT. 
//*> \endverbatim 
//*> 
//*> \param[in] LWORK 
//*> \verbatim 
//*>          LWORK is INTEGER 
//*>          The dimension of the array WORK. 
//*>          LWORK >= MAX(1,5*MIN(M,N)) for the paths (see comments inside code): 
//*>             - PATH 1  (M much larger than N, JOBU='N') 
//*>             - PATH 1t (N much larger than M, JOBVT='N') 
//*>          LWORK >= MAX(1,3*MIN(M,N) + MAX(M,N),5*MIN(M,N)) for the other paths 
//*>          For good performance, LWORK should generally be larger. 
//*> 
//*>          If LWORK = -1, then a workspace query is assumed; the routine 
//*>          only calculates the optimal size of the WORK array, returns 
//*>          this value as the first entry of the WORK array, and no error 
//*>          message related to LWORK is issued by XERBLA. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit. 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value. 
//*>          > 0:  if DBDSQR did not converge, INFO specifies how many 
//*>                superdiagonals of an intermediate bidiagonal form B 
//*>                did not converge to zero. See the description of WORK 
//*>                above for details. 
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
//*> \date April 2012 
//* 
//*> \ingroup doubleGEsing 
//* 
//*  ===================================================================== 

	 
	public static void _honk69hl(FString _14oeh29f, FString _au7flvr1, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, Double* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _irk8i6qr, Double* _7u55mqkq, ref Int32 _u6e6d39b, Double* _xdbczr8u, ref Int32 _h4ibbatv, Double* _apig8meb, ref Int32 _6fnxzlyp, ref Int32 _gro5yvfo)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)8 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Boolean _lhlgm7z5 =  default;
Boolean _hfkr5gre =  default;
Boolean _96h5p04b =  default;
Boolean _s7btv50y =  default;
Boolean _utderuk7 =  default;
Boolean _oigv4c9v =  default;
Boolean _njekud0j =  default;
Boolean _05xnu7gm =  default;
Boolean _ov7mcmwp =  default;
Boolean _wkqhtihq =  default;
Boolean _8pgl0qdq =  default;
Int32 _24c48sks =  default;
Int32 _9giy8o3g =  default;
Int32 _8rfn4f7g =  default;
Int32 _b5p6od9s =  default;
Int32 _smxeww0r =  default;
Int32 _bhsiylw4 =  default;
Int32 _m9w6lk7x =  default;
Int32 _65mv5f5m =  default;
Int32 _q1w15vsx =  default;
Int32 _5ke1jwwr =  default;
Int32 _wx1x93f0 =  default;
Int32 _j4l29b9c =  default;
Int32 _4b6rt45i =  default;
Int32 _v6sbkzy4 =  default;
Int32 _lbvz22dx =  default;
Int32 _tafa1evd =  default;
Int32 _qaseb1y7 =  default;
Int32 _gghrqcr1 =  default;
Int32 _lhzduysr =  default;
Int32 _34ov76nq =  default;
Int32 _0v6rtqiq =  default;
Int32 _tg62ial7 =  default;
Int32 _c4gifwtb =  default;
Int32 _loujht2t =  default;
Int32 _xzykxs0s =  default;
Int32 _yhpe15v8 =  default;
Int32 _rqsi8arz =  default;
Int32 _69fyb9kl =  default;
Int32 _6fyb132k =  default;
Int32 _e570rjzg =  default;
Int32 _ji0ydldw =  default;
Int32 _h9oxnbrz =  default;
Int32 _6dhwgtgy =  default;
Double _j6vjow1g =  default;
Double _av7j8yda =  default;
Double _p1iqarg6 =  default;
Double _bogm0gwy =  default;
Double* _g7qb61ha =  (Double*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Double) * ((int)1);
string fLanavab = default;
#endregion  variable declarations
_14oeh29f = _14oeh29f.Convert(1);
_au7flvr1 = _au7flvr1.Convert(1);

	{
		//* 
		//*  -- LAPACK driver routine (version 3.7.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     April 2012 
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
		_hfkr5gre = _w8y2rzgy(_14oeh29f ,"A" );
		_oigv4c9v = _w8y2rzgy(_14oeh29f ,"S" );
		_96h5p04b = (_hfkr5gre | _oigv4c9v);
		_utderuk7 = _w8y2rzgy(_14oeh29f ,"O" );
		_s7btv50y = _w8y2rzgy(_14oeh29f ,"N" );
		_njekud0j = _w8y2rzgy(_au7flvr1 ,"A" );
		_8pgl0qdq = _w8y2rzgy(_au7flvr1 ,"S" );
		_05xnu7gm = (_njekud0j | _8pgl0qdq);
		_wkqhtihq = _w8y2rzgy(_au7flvr1 ,"O" );
		_ov7mcmwp = _w8y2rzgy(_au7flvr1 ,"N" );
		_lhlgm7z5 = (_6fnxzlyp == (int)-1);//* 
		
		if (!((((_hfkr5gre | _oigv4c9v) | _utderuk7) | _s7btv50y)))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if ((!((((_njekud0j | _8pgl0qdq) | _wkqhtihq) | _ov7mcmwp))) | (_wkqhtihq & _utderuk7))
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_ev4xhht5 < (int)0)
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ))
		{
			
			_gro5yvfo = (int)-6;
		}
		else
		if ((_u6e6d39b < (int)1) | (_96h5p04b & (_u6e6d39b < _ev4xhht5)))
		{
			
			_gro5yvfo = (int)-9;
		}
		else
		if (((_h4ibbatv < (int)1) | (_njekud0j & (_h4ibbatv < _dxpq0xkr))) | (_8pgl0qdq & (_h4ibbatv < _qaseb1y7)))
		{
			
			_gro5yvfo = (int)-11;
		}
		//* 
		//*     Compute workspace 
		//*      (Note: Comments in the code beginning "Workspace:" describe the 
		//*       minimal amount of workspace needed at that point in the code, 
		//*       as well as the preferred amount for good performance. 
		//*       NB refers to the optimal block size for the immediately 
		//*       following subroutine, as returned by ILAENV.) 
		//* 
		
		if (_gro5yvfo == (int)0)
		{
			
			_gghrqcr1 = (int)1;
			_tafa1evd = (int)1;
			if ((_ev4xhht5 >= _dxpq0xkr) & (_qaseb1y7 > (int)0))
			{
				//* 
				//*           Compute space needed for DBDSQR 
				//* 
				
				_lhzduysr = _4mvd6e4d(ref Unsafe.AsRef((int)6) ,"DGESVD" ,_14oeh29f + _au7flvr1 ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) );
				_24c48sks = ((int)5 * _dxpq0xkr);//*           Compute space needed for DGEQRF 
				
				_ac2l6xc0(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_xzykxs0s = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//*           Compute space needed for DORGQR 
				
				_hxix712m(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_yhpe15v8 = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );
				_hxix712m(ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_rqsi8arz = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//*           Compute space needed for DGEBRD 
				
				_sf2bwwb1(ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_69fyb9kl = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//*           Compute space needed for DORGBR P 
				
				_cmc4j0e3("P" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_6fyb132k = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//*           Compute space needed for DORGBR Q 
				
				_cmc4j0e3("Q" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_e570rjzg = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//* 
				
				if (_ev4xhht5 >= _lhzduysr)
				{
					
					if (_s7btv50y)
					{
						//* 
						//*                 Path 1 (M much larger than N, JOBU='N') 
						//* 
						
						_tafa1evd = (_dxpq0xkr + _xzykxs0s);
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)3 * _dxpq0xkr) + _69fyb9kl );
						if (_wkqhtihq | _05xnu7gm)
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)3 * _dxpq0xkr) + _6fyb132k );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,_24c48sks );
						_gghrqcr1 = ILNumerics.F2NET.Intrinsics.MAX((int)4 * _dxpq0xkr ,_24c48sks );
					}
					else
					if (_utderuk7 & _ov7mcmwp)
					{
						//* 
						//*                 Path 2 (M much larger than N, JOBU='O', JOBVT='N') 
						//* 
						
						_loujht2t = (_dxpq0xkr + _xzykxs0s);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_dxpq0xkr + _yhpe15v8 );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _69fyb9kl );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _e570rjzg );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_24c48sks );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX((_dxpq0xkr * _dxpq0xkr) + _loujht2t ,((_dxpq0xkr * _dxpq0xkr) + (_ev4xhht5 * _dxpq0xkr)) + _dxpq0xkr );
						_gghrqcr1 = ILNumerics.F2NET.Intrinsics.MAX(((int)3 * _dxpq0xkr) + _ev4xhht5 ,_24c48sks );
					}
					else
					if (_utderuk7 & _05xnu7gm)
					{
						//* 
						//*                 Path 3 (M much larger than N, JOBU='O', JOBVT='S' or 
						//*                 'A') 
						//* 
						
						_loujht2t = (_dxpq0xkr + _xzykxs0s);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_dxpq0xkr + _yhpe15v8 );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _69fyb9kl );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _e570rjzg );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _6fyb132k );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_24c48sks );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX((_dxpq0xkr * _dxpq0xkr) + _loujht2t ,((_dxpq0xkr * _dxpq0xkr) + (_ev4xhht5 * _dxpq0xkr)) + _dxpq0xkr );
						_gghrqcr1 = ILNumerics.F2NET.Intrinsics.MAX(((int)3 * _dxpq0xkr) + _ev4xhht5 ,_24c48sks );
					}
					else
					if (_oigv4c9v & _ov7mcmwp)
					{
						//* 
						//*                 Path 4 (M much larger than N, JOBU='S', JOBVT='N') 
						//* 
						
						_loujht2t = (_dxpq0xkr + _xzykxs0s);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_dxpq0xkr + _yhpe15v8 );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _69fyb9kl );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _e570rjzg );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_24c48sks );
						_tafa1evd = ((_dxpq0xkr * _dxpq0xkr) + _loujht2t);
						_gghrqcr1 = ILNumerics.F2NET.Intrinsics.MAX(((int)3 * _dxpq0xkr) + _ev4xhht5 ,_24c48sks );
					}
					else
					if (_oigv4c9v & _wkqhtihq)
					{
						//* 
						//*                 Path 5 (M much larger than N, JOBU='S', JOBVT='O') 
						//* 
						
						_loujht2t = (_dxpq0xkr + _xzykxs0s);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_dxpq0xkr + _yhpe15v8 );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _69fyb9kl );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _e570rjzg );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _6fyb132k );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_24c48sks );
						_tafa1evd = ((((int)2 * _dxpq0xkr) * _dxpq0xkr) + _loujht2t);
						_gghrqcr1 = ILNumerics.F2NET.Intrinsics.MAX(((int)3 * _dxpq0xkr) + _ev4xhht5 ,_24c48sks );
					}
					else
					if (_oigv4c9v & _05xnu7gm)
					{
						//* 
						//*                 Path 6 (M much larger than N, JOBU='S', JOBVT='S' or 
						//*                 'A') 
						//* 
						
						_loujht2t = (_dxpq0xkr + _xzykxs0s);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_dxpq0xkr + _yhpe15v8 );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _69fyb9kl );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _e570rjzg );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _6fyb132k );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_24c48sks );
						_tafa1evd = ((_dxpq0xkr * _dxpq0xkr) + _loujht2t);
						_gghrqcr1 = ILNumerics.F2NET.Intrinsics.MAX(((int)3 * _dxpq0xkr) + _ev4xhht5 ,_24c48sks );
					}
					else
					if (_hfkr5gre & _ov7mcmwp)
					{
						//* 
						//*                 Path 7 (M much larger than N, JOBU='A', JOBVT='N') 
						//* 
						
						_loujht2t = (_dxpq0xkr + _xzykxs0s);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_dxpq0xkr + _rqsi8arz );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _69fyb9kl );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _e570rjzg );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_24c48sks );
						_tafa1evd = ((_dxpq0xkr * _dxpq0xkr) + _loujht2t);
						_gghrqcr1 = ILNumerics.F2NET.Intrinsics.MAX(((int)3 * _dxpq0xkr) + _ev4xhht5 ,_24c48sks );
					}
					else
					if (_hfkr5gre & _wkqhtihq)
					{
						//* 
						//*                 Path 8 (M much larger than N, JOBU='A', JOBVT='O') 
						//* 
						
						_loujht2t = (_dxpq0xkr + _xzykxs0s);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_dxpq0xkr + _rqsi8arz );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _69fyb9kl );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _e570rjzg );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _6fyb132k );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_24c48sks );
						_tafa1evd = ((((int)2 * _dxpq0xkr) * _dxpq0xkr) + _loujht2t);
						_gghrqcr1 = ILNumerics.F2NET.Intrinsics.MAX(((int)3 * _dxpq0xkr) + _ev4xhht5 ,_24c48sks );
					}
					else
					if (_hfkr5gre & _05xnu7gm)
					{
						//* 
						//*                 Path 9 (M much larger than N, JOBU='A', JOBVT='S' or 
						//*                 'A') 
						//* 
						
						_loujht2t = (_dxpq0xkr + _xzykxs0s);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_dxpq0xkr + _rqsi8arz );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _69fyb9kl );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _e570rjzg );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _dxpq0xkr) + _6fyb132k );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_24c48sks );
						_tafa1evd = ((_dxpq0xkr * _dxpq0xkr) + _loujht2t);
						_gghrqcr1 = ILNumerics.F2NET.Intrinsics.MAX(((int)3 * _dxpq0xkr) + _ev4xhht5 ,_24c48sks );
					}
					
				}
				else
				{
					//* 
					//*              Path 10 (M at least N, but not much larger) 
					//* 
					
					_sf2bwwb1(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
					_69fyb9kl = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );
					_tafa1evd = (((int)3 * _dxpq0xkr) + _69fyb9kl);
					if (_oigv4c9v | _utderuk7)
					{
						
						_cmc4j0e3("Q" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
						_e570rjzg = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)3 * _dxpq0xkr) + _e570rjzg );
					}
					
					if (_hfkr5gre)
					{
						
						_cmc4j0e3("Q" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
						_e570rjzg = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)3 * _dxpq0xkr) + _e570rjzg );
					}
					
					if (!(_ov7mcmwp))
					{
						
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)3 * _dxpq0xkr) + _6fyb132k );
					}
					
					_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,_24c48sks );
					_gghrqcr1 = ILNumerics.F2NET.Intrinsics.MAX(((int)3 * _dxpq0xkr) + _ev4xhht5 ,_24c48sks );
				}
				
			}
			else
			if (_qaseb1y7 > (int)0)
			{
				//* 
				//*           Compute space needed for DBDSQR 
				//* 
				
				_lhzduysr = _4mvd6e4d(ref Unsafe.AsRef((int)6) ,"DGESVD" ,_14oeh29f + _au7flvr1 ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) );
				_24c48sks = ((int)5 * _ev4xhht5);//*           Compute space needed for DGELQF 
				
				_bxcqf0ji(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_ji0ydldw = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//*           Compute space needed for DORGLQ 
				
				_n6025boa(ref _dxpq0xkr ,ref _dxpq0xkr ,ref _ev4xhht5 ,(_g7qb61ha+((int)1 - 1)),ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_h9oxnbrz = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );
				_n6025boa(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_6dhwgtgy = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//*           Compute space needed for DGEBRD 
				
				_sf2bwwb1(ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_69fyb9kl = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//*            Compute space needed for DORGBR P 
				
				_cmc4j0e3("P" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_6fyb132k = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );//*           Compute space needed for DORGBR Q 
				
				_cmc4j0e3("Q" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
				_e570rjzg = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );
				if (_dxpq0xkr >= _lhzduysr)
				{
					
					if (_ov7mcmwp)
					{
						//* 
						//*                 Path 1t(N much larger than M, JOBVT='N') 
						//* 
						
						_tafa1evd = (_ev4xhht5 + _ji0ydldw);
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)3 * _ev4xhht5) + _69fyb9kl );
						if (_utderuk7 | _96h5p04b)
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)3 * _ev4xhht5) + _e570rjzg );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,_24c48sks );
						_gghrqcr1 = ILNumerics.F2NET.Intrinsics.MAX((int)4 * _ev4xhht5 ,_24c48sks );
					}
					else
					if (_wkqhtihq & _s7btv50y)
					{
						//* 
						//*                 Path 2t(N much larger than M, JOBU='N', JOBVT='O') 
						//* 
						
						_loujht2t = (_ev4xhht5 + _ji0ydldw);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_ev4xhht5 + _6dhwgtgy );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _69fyb9kl );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _6fyb132k );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_24c48sks );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX((_ev4xhht5 * _ev4xhht5) + _loujht2t ,((_ev4xhht5 * _ev4xhht5) + (_ev4xhht5 * _dxpq0xkr)) + _ev4xhht5 );
						_gghrqcr1 = ILNumerics.F2NET.Intrinsics.MAX(((int)3 * _ev4xhht5) + _dxpq0xkr ,_24c48sks );
					}
					else
					if (_wkqhtihq & _96h5p04b)
					{
						//* 
						//*                 Path 3t(N much larger than M, JOBU='S' or 'A', 
						//*                 JOBVT='O') 
						//* 
						
						_loujht2t = (_ev4xhht5 + _ji0ydldw);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_ev4xhht5 + _6dhwgtgy );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _69fyb9kl );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _6fyb132k );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _e570rjzg );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_24c48sks );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX((_ev4xhht5 * _ev4xhht5) + _loujht2t ,((_ev4xhht5 * _ev4xhht5) + (_ev4xhht5 * _dxpq0xkr)) + _ev4xhht5 );
						_gghrqcr1 = ILNumerics.F2NET.Intrinsics.MAX(((int)3 * _ev4xhht5) + _dxpq0xkr ,_24c48sks );
					}
					else
					if (_8pgl0qdq & _s7btv50y)
					{
						//* 
						//*                 Path 4t(N much larger than M, JOBU='N', JOBVT='S') 
						//* 
						
						_loujht2t = (_ev4xhht5 + _ji0ydldw);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_ev4xhht5 + _6dhwgtgy );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _69fyb9kl );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _6fyb132k );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_24c48sks );
						_tafa1evd = ((_ev4xhht5 * _ev4xhht5) + _loujht2t);
						_gghrqcr1 = ILNumerics.F2NET.Intrinsics.MAX(((int)3 * _ev4xhht5) + _dxpq0xkr ,_24c48sks );
					}
					else
					if (_8pgl0qdq & _utderuk7)
					{
						//* 
						//*                 Path 5t(N much larger than M, JOBU='O', JOBVT='S') 
						//* 
						
						_loujht2t = (_ev4xhht5 + _ji0ydldw);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_ev4xhht5 + _6dhwgtgy );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _69fyb9kl );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _6fyb132k );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _e570rjzg );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_24c48sks );
						_tafa1evd = ((((int)2 * _ev4xhht5) * _ev4xhht5) + _loujht2t);
						_gghrqcr1 = ILNumerics.F2NET.Intrinsics.MAX(((int)3 * _ev4xhht5) + _dxpq0xkr ,_24c48sks );
					}
					else
					if (_8pgl0qdq & _96h5p04b)
					{
						//* 
						//*                 Path 6t(N much larger than M, JOBU='S' or 'A', 
						//*                 JOBVT='S') 
						//* 
						
						_loujht2t = (_ev4xhht5 + _ji0ydldw);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_ev4xhht5 + _6dhwgtgy );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _69fyb9kl );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _6fyb132k );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _e570rjzg );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_24c48sks );
						_tafa1evd = ((_ev4xhht5 * _ev4xhht5) + _loujht2t);
						_gghrqcr1 = ILNumerics.F2NET.Intrinsics.MAX(((int)3 * _ev4xhht5) + _dxpq0xkr ,_24c48sks );
					}
					else
					if (_njekud0j & _s7btv50y)
					{
						//* 
						//*                 Path 7t(N much larger than M, JOBU='N', JOBVT='A') 
						//* 
						
						_loujht2t = (_ev4xhht5 + _ji0ydldw);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_ev4xhht5 + _h9oxnbrz );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _69fyb9kl );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _6fyb132k );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_24c48sks );
						_tafa1evd = ((_ev4xhht5 * _ev4xhht5) + _loujht2t);
						_gghrqcr1 = ILNumerics.F2NET.Intrinsics.MAX(((int)3 * _ev4xhht5) + _dxpq0xkr ,_24c48sks );
					}
					else
					if (_njekud0j & _utderuk7)
					{
						//* 
						//*                 Path 8t(N much larger than M, JOBU='O', JOBVT='A') 
						//* 
						
						_loujht2t = (_ev4xhht5 + _ji0ydldw);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_ev4xhht5 + _h9oxnbrz );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _69fyb9kl );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _6fyb132k );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _e570rjzg );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_24c48sks );
						_tafa1evd = ((((int)2 * _ev4xhht5) * _ev4xhht5) + _loujht2t);
						_gghrqcr1 = ILNumerics.F2NET.Intrinsics.MAX(((int)3 * _ev4xhht5) + _dxpq0xkr ,_24c48sks );
					}
					else
					if (_njekud0j & _96h5p04b)
					{
						//* 
						//*                 Path 9t(N much larger than M, JOBU='S' or 'A', 
						//*                 JOBVT='A') 
						//* 
						
						_loujht2t = (_ev4xhht5 + _ji0ydldw);
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_ev4xhht5 + _h9oxnbrz );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _69fyb9kl );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _6fyb132k );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,((int)3 * _ev4xhht5) + _e570rjzg );
						_loujht2t = ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,_24c48sks );
						_tafa1evd = ((_ev4xhht5 * _ev4xhht5) + _loujht2t);
						_gghrqcr1 = ILNumerics.F2NET.Intrinsics.MAX(((int)3 * _ev4xhht5) + _dxpq0xkr ,_24c48sks );
					}
					
				}
				else
				{
					//* 
					//*              Path 10t(N greater than M, but not much larger) 
					//* 
					
					_sf2bwwb1(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
					_69fyb9kl = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );
					_tafa1evd = (((int)3 * _ev4xhht5) + _69fyb9kl);
					if (_8pgl0qdq | _wkqhtihq)
					{
						//*                Compute space needed for DORGBR P 
						
						_cmc4j0e3("P" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
						_6fyb132k = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)3 * _ev4xhht5) + _6fyb132k );
					}
					
					if (_njekud0j)
					{
						
						_cmc4j0e3("P" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _dxpq0xkr ,(_g7qb61ha+((int)1 - 1)),(_g7qb61ha+((int)1 - 1)),ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
						_6fyb132k = ILNumerics.F2NET.Intrinsics.INT(*(_g7qb61ha+((int)1 - 1)) );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)3 * _ev4xhht5) + _6fyb132k );
					}
					
					if (!(_s7btv50y))
					{
						
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)3 * _ev4xhht5) + _e570rjzg );
					}
					
					_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,_24c48sks );
					_gghrqcr1 = ILNumerics.F2NET.Intrinsics.MAX(((int)3 * _ev4xhht5) + _dxpq0xkr ,_24c48sks );
				}
				
			}
			
			_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,_gghrqcr1 );
			*(_apig8meb+((int)1 - 1)) = DBLE(_tafa1evd);//* 
			
			if ((_6fnxzlyp < _gghrqcr1) & (!(_lhlgm7z5)))
			{
				
				_gro5yvfo = (int)-13;
			}
			
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("DGESVD" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
				
				if (_s7btv50y)
				{
					//* 
					//*              Path 1 (M much larger than N, JOBU='N') 
					//*              No left singular vectors to be computed 
					//* 
					
					_q1w15vsx = (int)1;
					_4b6rt45i = (_q1w15vsx + _dxpq0xkr);//* 
					//*              Compute A=Q*R 
					//*              (Workspace: need 2*N, prefer N + N*NB) 
					//* 
					
					_ac2l6xc0(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Zero out below R 
					//* 
					
					if (_dxpq0xkr > (int)1)
					{
						
						_rta9tuwm("L" ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_vxfgpup9+((int)2 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
					}
					
					_smxeww0r = (int)1;
					_wx1x93f0 = (_smxeww0r + _dxpq0xkr);
					_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
					_4b6rt45i = (_5ke1jwwr + _dxpq0xkr);//* 
					//*              Bidiagonalize R in A 
					//*              (Workspace: need 4*N, prefer 3*N + 2*N*NB) 
					//* 
					
					_sf2bwwb1(ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
					_0v6rtqiq = (int)0;
					if (_wkqhtihq | _05xnu7gm)
					{
						//* 
						//*                 If right singular vectors desired, generate P'. 
						//*                 (Workspace: need 4*N-1, prefer 3*N + (N-1)*NB) 
						//* 
						
						_cmc4j0e3("P" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
						_0v6rtqiq = _dxpq0xkr;
					}
					
					_4b6rt45i = (_smxeww0r + _dxpq0xkr);//* 
					//*              Perform bidiagonal QR iteration, computing right 
					//*              singular vectors of A in A if desired 
					//*              (Workspace: need BDSPAC) 
					//* 
					
					_nvdqer79("U" ,ref _dxpq0xkr ,ref _0v6rtqiq ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_vxfgpup9 ,ref _ocv8fk5c ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
					//*              If right singular vectors desired in VT, copy them there 
					//* 
					
					if (_05xnu7gm)
					_hhtvj1kb("F" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );//* 
					
				}
				else
				if (_utderuk7 & _ov7mcmwp)
				{
					//* 
					//*              Path 2 (M much larger than N, JOBU='O', JOBVT='N') 
					//*              N left singular vectors to be overwritten on A and 
					//*              no right singular vectors to be computed 
					//* 
					
					if (_6fnxzlyp >= ((_dxpq0xkr * _dxpq0xkr) + ILNumerics.F2NET.Intrinsics.MAX((int)4 * _dxpq0xkr ,_24c48sks )))
					{
						//* 
						//*                 Sufficient workspace for a fast algorithm 
						//* 
						
						_m9w6lk7x = (int)1;
						if (_6fnxzlyp >= (ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,(_ocv8fk5c * _dxpq0xkr) + _dxpq0xkr ) + (_ocv8fk5c * _dxpq0xkr)))
						{
							//* 
							//*                    WORK(IU) is LDA by N, WORK(IR) is LDA by N 
							//* 
							
							_lbvz22dx = _ocv8fk5c;
							_v6sbkzy4 = _ocv8fk5c;
						}
						else
						if (_6fnxzlyp >= (ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,(_ocv8fk5c * _dxpq0xkr) + _dxpq0xkr ) + (_dxpq0xkr * _dxpq0xkr)))
						{
							//* 
							//*                    WORK(IU) is LDA by N, WORK(IR) is N by N 
							//* 
							
							_lbvz22dx = _ocv8fk5c;
							_v6sbkzy4 = _dxpq0xkr;
						}
						else
						{
							//* 
							//*                    WORK(IU) is LDWRKU by N, WORK(IR) is N by N 
							//* 
							
							_lbvz22dx = (((_6fnxzlyp - (_dxpq0xkr * _dxpq0xkr)) - _dxpq0xkr) / _dxpq0xkr);
							_v6sbkzy4 = _dxpq0xkr;
						}
						
						_q1w15vsx = (_m9w6lk7x + (_v6sbkzy4 * _dxpq0xkr));
						_4b6rt45i = (_q1w15vsx + _dxpq0xkr);//* 
						//*                 Compute A=Q*R 
						//*                 (Workspace: need N*N + 2*N, prefer N*N + N + N*NB) 
						//* 
						
						_ac2l6xc0(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
						//*                 Copy R to WORK(IR) and zero out below it 
						//* 
						
						_hhtvj1kb("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 );
						_rta9tuwm("L" ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_m9w6lk7x + (int)1 - 1)),ref _v6sbkzy4 );//* 
						//*                 Generate Q in A 
						//*                 (Workspace: need N*N + 2*N, prefer N*N + N + N*NB) 
						//* 
						
						_hxix712m(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
						_smxeww0r = _q1w15vsx;
						_wx1x93f0 = (_smxeww0r + _dxpq0xkr);
						_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
						_4b6rt45i = (_5ke1jwwr + _dxpq0xkr);//* 
						//*                 Bidiagonalize R in WORK(IR) 
						//*                 (Workspace: need N*N + 4*N, prefer N*N + 3*N + 2*N*NB) 
						//* 
						
						_sf2bwwb1(ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
						//*                 Generate left vectors bidiagonalizing R 
						//*                 (Workspace: need N*N + 4*N, prefer N*N + 3*N + N*NB) 
						//* 
						
						_cmc4j0e3("Q" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
						_4b6rt45i = (_smxeww0r + _dxpq0xkr);//* 
						//*                 Perform bidiagonal QR iteration, computing left 
						//*                 singular vectors of R in WORK(IR) 
						//*                 (Workspace: need N*N + BDSPAC) 
						//* 
						
						_nvdqer79("U" ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );
						_j4l29b9c = (_smxeww0r + _dxpq0xkr);//* 
						//*                 Multiply Q in A by left singular vectors of R in 
						//*                 WORK(IR), storing result in WORK(IU) and copying to A 
						//*                 (Workspace: need N*N + 2*N, prefer N*N + M*N + N) 
						//* 
						
						{
							System.Int32 __81fgg2dlsvn1354 = (System.Int32)((int)1);
							System.Int32 __81fgg2step1354 = (System.Int32)(_lbvz22dx);
							System.Int32 __81fgg2count1354;
							for (__81fgg2count1354 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1354 + __81fgg2step1354) / __81fgg2step1354)), _b5p6od9s = __81fgg2dlsvn1354; __81fgg2count1354 != 0; __81fgg2count1354--, _b5p6od9s += (__81fgg2step1354)) {

							{
								
								_8rfn4f7g = ILNumerics.F2NET.Intrinsics.MIN((_ev4xhht5 - _b5p6od9s) + (int)1 ,_lbvz22dx );
								_5nsxi69c("N" ,"N" ,ref _8rfn4f7g ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx );
								_hhtvj1kb("F" ,ref _8rfn4f7g ,ref _dxpq0xkr ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
Mark10:;
								// continue
							}
														}						}//* 
						
					}
					else
					{
						//* 
						//*                 Insufficient workspace for a fast algorithm 
						//* 
						
						_smxeww0r = (int)1;
						_wx1x93f0 = (_smxeww0r + _dxpq0xkr);
						_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
						_4b6rt45i = (_5ke1jwwr + _dxpq0xkr);//* 
						//*                 Bidiagonalize A 
						//*                 (Workspace: need 3*N + M, prefer 3*N + (M + N)*NB) 
						//* 
						
						_sf2bwwb1(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
						//*                 Generate left vectors bidiagonalizing A 
						//*                 (Workspace: need 4*N, prefer 3*N + N*NB) 
						//* 
						
						_cmc4j0e3("Q" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
						_4b6rt45i = (_smxeww0r + _dxpq0xkr);//* 
						//*                 Perform bidiagonal QR iteration, computing left 
						//*                 singular vectors of A in A 
						//*                 (Workspace: need BDSPAC) 
						//* 
						
						_nvdqer79("U" ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_vxfgpup9 ,ref _ocv8fk5c ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
						
					}
					//* 
					
				}
				else
				if (_utderuk7 & _05xnu7gm)
				{
					//* 
					//*              Path 3 (M much larger than N, JOBU='O', JOBVT='S' or 'A') 
					//*              N left singular vectors to be overwritten on A and 
					//*              N right singular vectors to be computed in VT 
					//* 
					
					if (_6fnxzlyp >= ((_dxpq0xkr * _dxpq0xkr) + ILNumerics.F2NET.Intrinsics.MAX((int)4 * _dxpq0xkr ,_24c48sks )))
					{
						//* 
						//*                 Sufficient workspace for a fast algorithm 
						//* 
						
						_m9w6lk7x = (int)1;
						if (_6fnxzlyp >= (ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,(_ocv8fk5c * _dxpq0xkr) + _dxpq0xkr ) + (_ocv8fk5c * _dxpq0xkr)))
						{
							//* 
							//*                    WORK(IU) is LDA by N and WORK(IR) is LDA by N 
							//* 
							
							_lbvz22dx = _ocv8fk5c;
							_v6sbkzy4 = _ocv8fk5c;
						}
						else
						if (_6fnxzlyp >= (ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,(_ocv8fk5c * _dxpq0xkr) + _dxpq0xkr ) + (_dxpq0xkr * _dxpq0xkr)))
						{
							//* 
							//*                    WORK(IU) is LDA by N and WORK(IR) is N by N 
							//* 
							
							_lbvz22dx = _ocv8fk5c;
							_v6sbkzy4 = _dxpq0xkr;
						}
						else
						{
							//* 
							//*                    WORK(IU) is LDWRKU by N and WORK(IR) is N by N 
							//* 
							
							_lbvz22dx = (((_6fnxzlyp - (_dxpq0xkr * _dxpq0xkr)) - _dxpq0xkr) / _dxpq0xkr);
							_v6sbkzy4 = _dxpq0xkr;
						}
						
						_q1w15vsx = (_m9w6lk7x + (_v6sbkzy4 * _dxpq0xkr));
						_4b6rt45i = (_q1w15vsx + _dxpq0xkr);//* 
						//*                 Compute A=Q*R 
						//*                 (Workspace: need N*N + 2*N, prefer N*N + N + N*NB) 
						//* 
						
						_ac2l6xc0(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
						//*                 Copy R to VT, zeroing out below it 
						//* 
						
						_hhtvj1kb("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );
						if (_dxpq0xkr > (int)1)
						_rta9tuwm("L" ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_xdbczr8u+((int)2 - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv );//* 
						//*                 Generate Q in A 
						//*                 (Workspace: need N*N + 2*N, prefer N*N + N + N*NB) 
						//* 
						
						_hxix712m(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
						_smxeww0r = _q1w15vsx;
						_wx1x93f0 = (_smxeww0r + _dxpq0xkr);
						_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
						_4b6rt45i = (_5ke1jwwr + _dxpq0xkr);//* 
						//*                 Bidiagonalize R in VT, copying result to WORK(IR) 
						//*                 (Workspace: need N*N + 4*N, prefer N*N + 3*N + 2*N*NB) 
						//* 
						
						_sf2bwwb1(ref _dxpq0xkr ,ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
						_hhtvj1kb("L" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 );//* 
						//*                 Generate left vectors bidiagonalizing R in WORK(IR) 
						//*                 (Workspace: need N*N + 4*N, prefer N*N + 3*N + N*NB) 
						//* 
						
						_cmc4j0e3("Q" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
						//*                 Generate right vectors bidiagonalizing R in VT 
						//*                 (Workspace: need N*N + 4*N-1, prefer N*N + 3*N + (N-1)*NB) 
						//* 
						
						_cmc4j0e3("P" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
						_4b6rt45i = (_smxeww0r + _dxpq0xkr);//* 
						//*                 Perform bidiagonal QR iteration, computing left 
						//*                 singular vectors of R in WORK(IR) and computing right 
						//*                 singular vectors of R in VT 
						//*                 (Workspace: need N*N + BDSPAC) 
						//* 
						
						_nvdqer79("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );
						_j4l29b9c = (_smxeww0r + _dxpq0xkr);//* 
						//*                 Multiply Q in A by left singular vectors of R in 
						//*                 WORK(IR), storing result in WORK(IU) and copying to A 
						//*                 (Workspace: need N*N + 2*N, prefer N*N + M*N + N) 
						//* 
						
						{
							System.Int32 __81fgg2dlsvn1355 = (System.Int32)((int)1);
							System.Int32 __81fgg2step1355 = (System.Int32)(_lbvz22dx);
							System.Int32 __81fgg2count1355;
							for (__81fgg2count1355 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1355 + __81fgg2step1355) / __81fgg2step1355)), _b5p6od9s = __81fgg2dlsvn1355; __81fgg2count1355 != 0; __81fgg2count1355--, _b5p6od9s += (__81fgg2step1355)) {

							{
								
								_8rfn4f7g = ILNumerics.F2NET.Intrinsics.MIN((_ev4xhht5 - _b5p6od9s) + (int)1 ,_lbvz22dx );
								_5nsxi69c("N" ,"N" ,ref _8rfn4f7g ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx );
								_hhtvj1kb("F" ,ref _8rfn4f7g ,ref _dxpq0xkr ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
Mark20:;
								// continue
							}
														}						}//* 
						
					}
					else
					{
						//* 
						//*                 Insufficient workspace for a fast algorithm 
						//* 
						
						_q1w15vsx = (int)1;
						_4b6rt45i = (_q1w15vsx + _dxpq0xkr);//* 
						//*                 Compute A=Q*R 
						//*                 (Workspace: need 2*N, prefer N + N*NB) 
						//* 
						
						_ac2l6xc0(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
						//*                 Copy R to VT, zeroing out below it 
						//* 
						
						_hhtvj1kb("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );
						if (_dxpq0xkr > (int)1)
						_rta9tuwm("L" ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_xdbczr8u+((int)2 - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv );//* 
						//*                 Generate Q in A 
						//*                 (Workspace: need 2*N, prefer N + N*NB) 
						//* 
						
						_hxix712m(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
						_smxeww0r = _q1w15vsx;
						_wx1x93f0 = (_smxeww0r + _dxpq0xkr);
						_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
						_4b6rt45i = (_5ke1jwwr + _dxpq0xkr);//* 
						//*                 Bidiagonalize R in VT 
						//*                 (Workspace: need 4*N, prefer 3*N + 2*N*NB) 
						//* 
						
						_sf2bwwb1(ref _dxpq0xkr ,ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
						//*                 Multiply Q in A by left vectors bidiagonalizing R 
						//*                 (Workspace: need 3*N + M, prefer 3*N + M*NB) 
						//* 
						
						_pwi7fryj("Q" ,"R" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_wx1x93f0 - 1)),_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
						//*                 Generate right vectors bidiagonalizing R in VT 
						//*                 (Workspace: need 4*N-1, prefer 3*N + (N-1)*NB) 
						//* 
						
						_cmc4j0e3("P" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
						_4b6rt45i = (_smxeww0r + _dxpq0xkr);//* 
						//*                 Perform bidiagonal QR iteration, computing left 
						//*                 singular vectors of A in A and computing right 
						//*                 singular vectors of A in VT 
						//*                 (Workspace: need BDSPAC) 
						//* 
						
						_nvdqer79("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_xdbczr8u ,ref _h4ibbatv ,_vxfgpup9 ,ref _ocv8fk5c ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
						
					}
					//* 
					
				}
				else
				if (_oigv4c9v)
				{
					//* 
					
					if (_ov7mcmwp)
					{
						//* 
						//*                 Path 4 (M much larger than N, JOBU='S', JOBVT='N') 
						//*                 N left singular vectors to be computed in U and 
						//*                 no right singular vectors to be computed 
						//* 
						
						if (_6fnxzlyp >= ((_dxpq0xkr * _dxpq0xkr) + ILNumerics.F2NET.Intrinsics.MAX((int)4 * _dxpq0xkr ,_24c48sks )))
						{
							//* 
							//*                    Sufficient workspace for a fast algorithm 
							//* 
							
							_m9w6lk7x = (int)1;
							if (_6fnxzlyp >= (_loujht2t + (_ocv8fk5c * _dxpq0xkr)))
							{
								//* 
								//*                       WORK(IR) is LDA by N 
								//* 
								
								_v6sbkzy4 = _ocv8fk5c;
							}
							else
							{
								//* 
								//*                       WORK(IR) is N by N 
								//* 
								
								_v6sbkzy4 = _dxpq0xkr;
							}
							
							_q1w15vsx = (_m9w6lk7x + (_v6sbkzy4 * _dxpq0xkr));
							_4b6rt45i = (_q1w15vsx + _dxpq0xkr);//* 
							//*                    Compute A=Q*R 
							//*                    (Workspace: need N*N + 2*N, prefer N*N + N + N*NB) 
							//* 
							
							_ac2l6xc0(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Copy R to WORK(IR), zeroing out below it 
							//* 
							
							_hhtvj1kb("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 );
							_rta9tuwm("L" ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_m9w6lk7x + (int)1 - 1)),ref _v6sbkzy4 );//* 
							//*                    Generate Q in A 
							//*                    (Workspace: need N*N + 2*N, prefer N*N + N + N*NB) 
							//* 
							
							_hxix712m(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_smxeww0r = _q1w15vsx;
							_wx1x93f0 = (_smxeww0r + _dxpq0xkr);
							_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
							_4b6rt45i = (_5ke1jwwr + _dxpq0xkr);//* 
							//*                    Bidiagonalize R in WORK(IR) 
							//*                    (Workspace: need N*N + 4*N, prefer N*N + 3*N + 2*N*NB) 
							//* 
							
							_sf2bwwb1(ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Generate left vectors bidiagonalizing R in WORK(IR) 
							//*                    (Workspace: need N*N + 4*N, prefer N*N + 3*N + N*NB) 
							//* 
							
							_cmc4j0e3("Q" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_4b6rt45i = (_smxeww0r + _dxpq0xkr);//* 
							//*                    Perform bidiagonal QR iteration, computing left 
							//*                    singular vectors of R in WORK(IR) 
							//*                    (Workspace: need N*N + BDSPAC) 
							//* 
							
							_nvdqer79("U" ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
							//*                    Multiply Q in A by left singular vectors of R in 
							//*                    WORK(IR), storing result in U 
							//*                    (Workspace: need N*N) 
							//* 
							
							_5nsxi69c("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_kxg5drh2) ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,ref Unsafe.AsRef(_d0547bi2) ,_7u55mqkq ,ref _u6e6d39b );//* 
							
						}
						else
						{
							//* 
							//*                    Insufficient workspace for a fast algorithm 
							//* 
							
							_q1w15vsx = (int)1;
							_4b6rt45i = (_q1w15vsx + _dxpq0xkr);//* 
							//*                    Compute A=Q*R, copying result to U 
							//*                    (Workspace: need 2*N, prefer N + N*NB) 
							//* 
							
							_ac2l6xc0(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );//* 
							//*                    Generate Q in U 
							//*                    (Workspace: need 2*N, prefer N + N*NB) 
							//* 
							
							_hxix712m(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_smxeww0r = _q1w15vsx;
							_wx1x93f0 = (_smxeww0r + _dxpq0xkr);
							_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
							_4b6rt45i = (_5ke1jwwr + _dxpq0xkr);//* 
							//*                    Zero out below R in A 
							//* 
							
							if (_dxpq0xkr > (int)1)
							{
								
								_rta9tuwm("L" ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_vxfgpup9+((int)2 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							}
							//* 
							//*                    Bidiagonalize R in A 
							//*                    (Workspace: need 4*N, prefer 3*N + 2*N*NB) 
							//* 
							
							_sf2bwwb1(ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Multiply Q in U by left vectors bidiagonalizing R 
							//*                    (Workspace: need 3*N + M, prefer 3*N + M*NB) 
							//* 
							
							_pwi7fryj("Q" ,"R" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_4b6rt45i = (_smxeww0r + _dxpq0xkr);//* 
							//*                    Perform bidiagonal QR iteration, computing left 
							//*                    singular vectors of A in U 
							//*                    (Workspace: need BDSPAC) 
							//* 
							
							_nvdqer79("U" ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_7u55mqkq ,ref _u6e6d39b ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
							
						}
						//* 
						
					}
					else
					if (_wkqhtihq)
					{
						//* 
						//*                 Path 5 (M much larger than N, JOBU='S', JOBVT='O') 
						//*                 N left singular vectors to be computed in U and 
						//*                 N right singular vectors to be overwritten on A 
						//* 
						
						if (_6fnxzlyp >= ((((int)2 * _dxpq0xkr) * _dxpq0xkr) + ILNumerics.F2NET.Intrinsics.MAX((int)4 * _dxpq0xkr ,_24c48sks )))
						{
							//* 
							//*                    Sufficient workspace for a fast algorithm 
							//* 
							
							_j4l29b9c = (int)1;
							if (_6fnxzlyp >= (_loujht2t + (((int)2 * _ocv8fk5c) * _dxpq0xkr)))
							{
								//* 
								//*                       WORK(IU) is LDA by N and WORK(IR) is LDA by N 
								//* 
								
								_lbvz22dx = _ocv8fk5c;
								_m9w6lk7x = (_j4l29b9c + (_lbvz22dx * _dxpq0xkr));
								_v6sbkzy4 = _ocv8fk5c;
							}
							else
							if (_6fnxzlyp >= (_loujht2t + ((_ocv8fk5c + _dxpq0xkr) * _dxpq0xkr)))
							{
								//* 
								//*                       WORK(IU) is LDA by N and WORK(IR) is N by N 
								//* 
								
								_lbvz22dx = _ocv8fk5c;
								_m9w6lk7x = (_j4l29b9c + (_lbvz22dx * _dxpq0xkr));
								_v6sbkzy4 = _dxpq0xkr;
							}
							else
							{
								//* 
								//*                       WORK(IU) is N by N and WORK(IR) is N by N 
								//* 
								
								_lbvz22dx = _dxpq0xkr;
								_m9w6lk7x = (_j4l29b9c + (_lbvz22dx * _dxpq0xkr));
								_v6sbkzy4 = _dxpq0xkr;
							}
							
							_q1w15vsx = (_m9w6lk7x + (_v6sbkzy4 * _dxpq0xkr));
							_4b6rt45i = (_q1w15vsx + _dxpq0xkr);//* 
							//*                    Compute A=Q*R 
							//*                    (Workspace: need 2*N*N + 2*N, prefer 2*N*N + N + N*NB) 
							//* 
							
							_ac2l6xc0(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Copy R to WORK(IU), zeroing out below it 
							//* 
							
							_hhtvj1kb("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx );
							_rta9tuwm("L" ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_j4l29b9c + (int)1 - 1)),ref _lbvz22dx );//* 
							//*                    Generate Q in A 
							//*                    (Workspace: need 2*N*N + 2*N, prefer 2*N*N + N + N*NB) 
							//* 
							
							_hxix712m(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_smxeww0r = _q1w15vsx;
							_wx1x93f0 = (_smxeww0r + _dxpq0xkr);
							_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
							_4b6rt45i = (_5ke1jwwr + _dxpq0xkr);//* 
							//*                    Bidiagonalize R in WORK(IU), copying result to 
							//*                    WORK(IR) 
							//*                    (Workspace: need 2*N*N + 4*N, 
							//*                                prefer 2*N*N+3*N+2*N*NB) 
							//* 
							
							_sf2bwwb1(ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_hhtvj1kb("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 );//* 
							//*                    Generate left bidiagonalizing vectors in WORK(IU) 
							//*                    (Workspace: need 2*N*N + 4*N, prefer 2*N*N + 3*N + N*NB) 
							//* 
							
							_cmc4j0e3("Q" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Generate right bidiagonalizing vectors in WORK(IR) 
							//*                    (Workspace: need 2*N*N + 4*N-1, 
							//*                                prefer 2*N*N+3*N+(N-1)*NB) 
							//* 
							
							_cmc4j0e3("P" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_4b6rt45i = (_smxeww0r + _dxpq0xkr);//* 
							//*                    Perform bidiagonal QR iteration, computing left 
							//*                    singular vectors of R in WORK(IU) and computing 
							//*                    right singular vectors of R in WORK(IR) 
							//*                    (Workspace: need 2*N*N + BDSPAC) 
							//* 
							
							_nvdqer79("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
							//*                    Multiply Q in A by left singular vectors of R in 
							//*                    WORK(IU), storing result in U 
							//*                    (Workspace: need N*N) 
							//* 
							
							_5nsxi69c("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_kxg5drh2) ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,ref Unsafe.AsRef(_d0547bi2) ,_7u55mqkq ,ref _u6e6d39b );//* 
							//*                    Copy right singular vectors of R to A 
							//*                    (Workspace: need N*N) 
							//* 
							
							_hhtvj1kb("F" ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,_vxfgpup9 ,ref _ocv8fk5c );//* 
							
						}
						else
						{
							//* 
							//*                    Insufficient workspace for a fast algorithm 
							//* 
							
							_q1w15vsx = (int)1;
							_4b6rt45i = (_q1w15vsx + _dxpq0xkr);//* 
							//*                    Compute A=Q*R, copying result to U 
							//*                    (Workspace: need 2*N, prefer N + N*NB) 
							//* 
							
							_ac2l6xc0(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );//* 
							//*                    Generate Q in U 
							//*                    (Workspace: need 2*N, prefer N + N*NB) 
							//* 
							
							_hxix712m(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_smxeww0r = _q1w15vsx;
							_wx1x93f0 = (_smxeww0r + _dxpq0xkr);
							_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
							_4b6rt45i = (_5ke1jwwr + _dxpq0xkr);//* 
							//*                    Zero out below R in A 
							//* 
							
							if (_dxpq0xkr > (int)1)
							{
								
								_rta9tuwm("L" ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_vxfgpup9+((int)2 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							}
							//* 
							//*                    Bidiagonalize R in A 
							//*                    (Workspace: need 4*N, prefer 3*N + 2*N*NB) 
							//* 
							
							_sf2bwwb1(ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Multiply Q in U by left vectors bidiagonalizing R 
							//*                    (Workspace: need 3*N + M, prefer 3*N + M*NB) 
							//* 
							
							_pwi7fryj("Q" ,"R" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Generate right vectors bidiagonalizing R in A 
							//*                    (Workspace: need 4*N-1, prefer 3*N + (N-1)*NB) 
							//* 
							
							_cmc4j0e3("P" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_4b6rt45i = (_smxeww0r + _dxpq0xkr);//* 
							//*                    Perform bidiagonal QR iteration, computing left 
							//*                    singular vectors of A in U and computing right 
							//*                    singular vectors of A in A 
							//*                    (Workspace: need BDSPAC) 
							//* 
							
							_nvdqer79("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
							
						}
						//* 
						
					}
					else
					if (_05xnu7gm)
					{
						//* 
						//*                 Path 6 (M much larger than N, JOBU='S', JOBVT='S' 
						//*                         or 'A') 
						//*                 N left singular vectors to be computed in U and 
						//*                 N right singular vectors to be computed in VT 
						//* 
						
						if (_6fnxzlyp >= ((_dxpq0xkr * _dxpq0xkr) + ILNumerics.F2NET.Intrinsics.MAX((int)4 * _dxpq0xkr ,_24c48sks )))
						{
							//* 
							//*                    Sufficient workspace for a fast algorithm 
							//* 
							
							_j4l29b9c = (int)1;
							if (_6fnxzlyp >= (_loujht2t + (_ocv8fk5c * _dxpq0xkr)))
							{
								//* 
								//*                       WORK(IU) is LDA by N 
								//* 
								
								_lbvz22dx = _ocv8fk5c;
							}
							else
							{
								//* 
								//*                       WORK(IU) is N by N 
								//* 
								
								_lbvz22dx = _dxpq0xkr;
							}
							
							_q1w15vsx = (_j4l29b9c + (_lbvz22dx * _dxpq0xkr));
							_4b6rt45i = (_q1w15vsx + _dxpq0xkr);//* 
							//*                    Compute A=Q*R 
							//*                    (Workspace: need N*N + 2*N, prefer N*N + N + N*NB) 
							//* 
							
							_ac2l6xc0(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Copy R to WORK(IU), zeroing out below it 
							//* 
							
							_hhtvj1kb("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx );
							_rta9tuwm("L" ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_j4l29b9c + (int)1 - 1)),ref _lbvz22dx );//* 
							//*                    Generate Q in A 
							//*                    (Workspace: need N*N + 2*N, prefer N*N + N + N*NB) 
							//* 
							
							_hxix712m(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_smxeww0r = _q1w15vsx;
							_wx1x93f0 = (_smxeww0r + _dxpq0xkr);
							_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
							_4b6rt45i = (_5ke1jwwr + _dxpq0xkr);//* 
							//*                    Bidiagonalize R in WORK(IU), copying result to VT 
							//*                    (Workspace: need N*N + 4*N, prefer N*N + 3*N + 2*N*NB) 
							//* 
							
							_sf2bwwb1(ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_hhtvj1kb("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,_xdbczr8u ,ref _h4ibbatv );//* 
							//*                    Generate left bidiagonalizing vectors in WORK(IU) 
							//*                    (Workspace: need N*N + 4*N, prefer N*N + 3*N + N*NB) 
							//* 
							
							_cmc4j0e3("Q" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Generate right bidiagonalizing vectors in VT 
							//*                    (Workspace: need N*N + 4*N-1, 
							//*                                prefer N*N+3*N+(N-1)*NB) 
							//* 
							
							_cmc4j0e3("P" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_4b6rt45i = (_smxeww0r + _dxpq0xkr);//* 
							//*                    Perform bidiagonal QR iteration, computing left 
							//*                    singular vectors of R in WORK(IU) and computing 
							//*                    right singular vectors of R in VT 
							//*                    (Workspace: need N*N + BDSPAC) 
							//* 
							
							_nvdqer79("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
							//*                    Multiply Q in A by left singular vectors of R in 
							//*                    WORK(IU), storing result in U 
							//*                    (Workspace: need N*N) 
							//* 
							
							_5nsxi69c("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_kxg5drh2) ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,ref Unsafe.AsRef(_d0547bi2) ,_7u55mqkq ,ref _u6e6d39b );//* 
							
						}
						else
						{
							//* 
							//*                    Insufficient workspace for a fast algorithm 
							//* 
							
							_q1w15vsx = (int)1;
							_4b6rt45i = (_q1w15vsx + _dxpq0xkr);//* 
							//*                    Compute A=Q*R, copying result to U 
							//*                    (Workspace: need 2*N, prefer N + N*NB) 
							//* 
							
							_ac2l6xc0(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );//* 
							//*                    Generate Q in U 
							//*                    (Workspace: need 2*N, prefer N + N*NB) 
							//* 
							
							_hxix712m(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Copy R to VT, zeroing out below it 
							//* 
							
							_hhtvj1kb("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );
							if (_dxpq0xkr > (int)1)
							_rta9tuwm("L" ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_xdbczr8u+((int)2 - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv );
							_smxeww0r = _q1w15vsx;
							_wx1x93f0 = (_smxeww0r + _dxpq0xkr);
							_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
							_4b6rt45i = (_5ke1jwwr + _dxpq0xkr);//* 
							//*                    Bidiagonalize R in VT 
							//*                    (Workspace: need 4*N, prefer 3*N + 2*N*NB) 
							//* 
							
							_sf2bwwb1(ref _dxpq0xkr ,ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Multiply Q in U by left bidiagonalizing vectors 
							//*                    in VT 
							//*                    (Workspace: need 3*N + M, prefer 3*N + M*NB) 
							//* 
							
							_pwi7fryj("Q" ,"R" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_wx1x93f0 - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Generate right bidiagonalizing vectors in VT 
							//*                    (Workspace: need 4*N-1, prefer 3*N + (N-1)*NB) 
							//* 
							
							_cmc4j0e3("P" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_4b6rt45i = (_smxeww0r + _dxpq0xkr);//* 
							//*                    Perform bidiagonal QR iteration, computing left 
							//*                    singular vectors of A in U and computing right 
							//*                    singular vectors of A in VT 
							//*                    (Workspace: need BDSPAC) 
							//* 
							
							_nvdqer79("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_xdbczr8u ,ref _h4ibbatv ,_7u55mqkq ,ref _u6e6d39b ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
							
						}
						//* 
						
					}
					//* 
					
				}
				else
				if (_hfkr5gre)
				{
					//* 
					
					if (_ov7mcmwp)
					{
						//* 
						//*                 Path 7 (M much larger than N, JOBU='A', JOBVT='N') 
						//*                 M left singular vectors to be computed in U and 
						//*                 no right singular vectors to be computed 
						//* 
						
						if (_6fnxzlyp >= ((_dxpq0xkr * _dxpq0xkr) + ILNumerics.F2NET.Intrinsics.MAX(_dxpq0xkr + _ev4xhht5 ,(int)4 * _dxpq0xkr ,_24c48sks )))
						{
							//* 
							//*                    Sufficient workspace for a fast algorithm 
							//* 
							
							_m9w6lk7x = (int)1;
							if (_6fnxzlyp >= (_loujht2t + (_ocv8fk5c * _dxpq0xkr)))
							{
								//* 
								//*                       WORK(IR) is LDA by N 
								//* 
								
								_v6sbkzy4 = _ocv8fk5c;
							}
							else
							{
								//* 
								//*                       WORK(IR) is N by N 
								//* 
								
								_v6sbkzy4 = _dxpq0xkr;
							}
							
							_q1w15vsx = (_m9w6lk7x + (_v6sbkzy4 * _dxpq0xkr));
							_4b6rt45i = (_q1w15vsx + _dxpq0xkr);//* 
							//*                    Compute A=Q*R, copying result to U 
							//*                    (Workspace: need N*N + 2*N, prefer N*N + N + N*NB) 
							//* 
							
							_ac2l6xc0(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );//* 
							//*                    Copy R to WORK(IR), zeroing out below it 
							//* 
							
							_hhtvj1kb("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 );
							_rta9tuwm("L" ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_m9w6lk7x + (int)1 - 1)),ref _v6sbkzy4 );//* 
							//*                    Generate Q in U 
							//*                    (Workspace: need N*N + N + M, prefer N*N + N + M*NB) 
							//* 
							
							_hxix712m(ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_smxeww0r = _q1w15vsx;
							_wx1x93f0 = (_smxeww0r + _dxpq0xkr);
							_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
							_4b6rt45i = (_5ke1jwwr + _dxpq0xkr);//* 
							//*                    Bidiagonalize R in WORK(IR) 
							//*                    (Workspace: need N*N + 4*N, prefer N*N + 3*N + 2*N*NB) 
							//* 
							
							_sf2bwwb1(ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Generate left bidiagonalizing vectors in WORK(IR) 
							//*                    (Workspace: need N*N + 4*N, prefer N*N + 3*N + N*NB) 
							//* 
							
							_cmc4j0e3("Q" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_4b6rt45i = (_smxeww0r + _dxpq0xkr);//* 
							//*                    Perform bidiagonal QR iteration, computing left 
							//*                    singular vectors of R in WORK(IR) 
							//*                    (Workspace: need N*N + BDSPAC) 
							//* 
							
							_nvdqer79("U" ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
							//*                    Multiply Q in U by left singular vectors of R in 
							//*                    WORK(IR), storing result in A 
							//*                    (Workspace: need N*N) 
							//* 
							
							_5nsxi69c("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_kxg5drh2) ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,ref Unsafe.AsRef(_d0547bi2) ,_vxfgpup9 ,ref _ocv8fk5c );//* 
							//*                    Copy left singular vectors of A from A to U 
							//* 
							
							_hhtvj1kb("F" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );//* 
							
						}
						else
						{
							//* 
							//*                    Insufficient workspace for a fast algorithm 
							//* 
							
							_q1w15vsx = (int)1;
							_4b6rt45i = (_q1w15vsx + _dxpq0xkr);//* 
							//*                    Compute A=Q*R, copying result to U 
							//*                    (Workspace: need 2*N, prefer N + N*NB) 
							//* 
							
							_ac2l6xc0(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );//* 
							//*                    Generate Q in U 
							//*                    (Workspace: need N + M, prefer N + M*NB) 
							//* 
							
							_hxix712m(ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_smxeww0r = _q1w15vsx;
							_wx1x93f0 = (_smxeww0r + _dxpq0xkr);
							_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
							_4b6rt45i = (_5ke1jwwr + _dxpq0xkr);//* 
							//*                    Zero out below R in A 
							//* 
							
							if (_dxpq0xkr > (int)1)
							{
								
								_rta9tuwm("L" ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_vxfgpup9+((int)2 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							}
							//* 
							//*                    Bidiagonalize R in A 
							//*                    (Workspace: need 4*N, prefer 3*N + 2*N*NB) 
							//* 
							
							_sf2bwwb1(ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Multiply Q in U by left bidiagonalizing vectors 
							//*                    in A 
							//*                    (Workspace: need 3*N + M, prefer 3*N + M*NB) 
							//* 
							
							_pwi7fryj("Q" ,"R" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_4b6rt45i = (_smxeww0r + _dxpq0xkr);//* 
							//*                    Perform bidiagonal QR iteration, computing left 
							//*                    singular vectors of A in U 
							//*                    (Workspace: need BDSPAC) 
							//* 
							
							_nvdqer79("U" ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_7u55mqkq ,ref _u6e6d39b ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
							
						}
						//* 
						
					}
					else
					if (_wkqhtihq)
					{
						//* 
						//*                 Path 8 (M much larger than N, JOBU='A', JOBVT='O') 
						//*                 M left singular vectors to be computed in U and 
						//*                 N right singular vectors to be overwritten on A 
						//* 
						
						if (_6fnxzlyp >= ((((int)2 * _dxpq0xkr) * _dxpq0xkr) + ILNumerics.F2NET.Intrinsics.MAX(_dxpq0xkr + _ev4xhht5 ,(int)4 * _dxpq0xkr ,_24c48sks )))
						{
							//* 
							//*                    Sufficient workspace for a fast algorithm 
							//* 
							
							_j4l29b9c = (int)1;
							if (_6fnxzlyp >= (_loujht2t + (((int)2 * _ocv8fk5c) * _dxpq0xkr)))
							{
								//* 
								//*                       WORK(IU) is LDA by N and WORK(IR) is LDA by N 
								//* 
								
								_lbvz22dx = _ocv8fk5c;
								_m9w6lk7x = (_j4l29b9c + (_lbvz22dx * _dxpq0xkr));
								_v6sbkzy4 = _ocv8fk5c;
							}
							else
							if (_6fnxzlyp >= (_loujht2t + ((_ocv8fk5c + _dxpq0xkr) * _dxpq0xkr)))
							{
								//* 
								//*                       WORK(IU) is LDA by N and WORK(IR) is N by N 
								//* 
								
								_lbvz22dx = _ocv8fk5c;
								_m9w6lk7x = (_j4l29b9c + (_lbvz22dx * _dxpq0xkr));
								_v6sbkzy4 = _dxpq0xkr;
							}
							else
							{
								//* 
								//*                       WORK(IU) is N by N and WORK(IR) is N by N 
								//* 
								
								_lbvz22dx = _dxpq0xkr;
								_m9w6lk7x = (_j4l29b9c + (_lbvz22dx * _dxpq0xkr));
								_v6sbkzy4 = _dxpq0xkr;
							}
							
							_q1w15vsx = (_m9w6lk7x + (_v6sbkzy4 * _dxpq0xkr));
							_4b6rt45i = (_q1w15vsx + _dxpq0xkr);//* 
							//*                    Compute A=Q*R, copying result to U 
							//*                    (Workspace: need 2*N*N + 2*N, prefer 2*N*N + N + N*NB) 
							//* 
							
							_ac2l6xc0(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );//* 
							//*                    Generate Q in U 
							//*                    (Workspace: need 2*N*N + N + M, prefer 2*N*N + N + M*NB) 
							//* 
							
							_hxix712m(ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Copy R to WORK(IU), zeroing out below it 
							//* 
							
							_hhtvj1kb("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx );
							_rta9tuwm("L" ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_j4l29b9c + (int)1 - 1)),ref _lbvz22dx );
							_smxeww0r = _q1w15vsx;
							_wx1x93f0 = (_smxeww0r + _dxpq0xkr);
							_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
							_4b6rt45i = (_5ke1jwwr + _dxpq0xkr);//* 
							//*                    Bidiagonalize R in WORK(IU), copying result to 
							//*                    WORK(IR) 
							//*                    (Workspace: need 2*N*N + 4*N, 
							//*                                prefer 2*N*N+3*N+2*N*NB) 
							//* 
							
							_sf2bwwb1(ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_hhtvj1kb("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 );//* 
							//*                    Generate left bidiagonalizing vectors in WORK(IU) 
							//*                    (Workspace: need 2*N*N + 4*N, prefer 2*N*N + 3*N + N*NB) 
							//* 
							
							_cmc4j0e3("Q" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Generate right bidiagonalizing vectors in WORK(IR) 
							//*                    (Workspace: need 2*N*N + 4*N-1, 
							//*                                prefer 2*N*N+3*N+(N-1)*NB) 
							//* 
							
							_cmc4j0e3("P" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_4b6rt45i = (_smxeww0r + _dxpq0xkr);//* 
							//*                    Perform bidiagonal QR iteration, computing left 
							//*                    singular vectors of R in WORK(IU) and computing 
							//*                    right singular vectors of R in WORK(IR) 
							//*                    (Workspace: need 2*N*N + BDSPAC) 
							//* 
							
							_nvdqer79("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
							//*                    Multiply Q in U by left singular vectors of R in 
							//*                    WORK(IU), storing result in A 
							//*                    (Workspace: need N*N) 
							//* 
							
							_5nsxi69c("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_kxg5drh2) ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,ref Unsafe.AsRef(_d0547bi2) ,_vxfgpup9 ,ref _ocv8fk5c );//* 
							//*                    Copy left singular vectors of A from A to U 
							//* 
							
							_hhtvj1kb("F" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );//* 
							//*                    Copy right singular vectors of R from WORK(IR) to A 
							//* 
							
							_hhtvj1kb("F" ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,_vxfgpup9 ,ref _ocv8fk5c );//* 
							
						}
						else
						{
							//* 
							//*                    Insufficient workspace for a fast algorithm 
							//* 
							
							_q1w15vsx = (int)1;
							_4b6rt45i = (_q1w15vsx + _dxpq0xkr);//* 
							//*                    Compute A=Q*R, copying result to U 
							//*                    (Workspace: need 2*N, prefer N + N*NB) 
							//* 
							
							_ac2l6xc0(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );//* 
							//*                    Generate Q in U 
							//*                    (Workspace: need N + M, prefer N + M*NB) 
							//* 
							
							_hxix712m(ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_smxeww0r = _q1w15vsx;
							_wx1x93f0 = (_smxeww0r + _dxpq0xkr);
							_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
							_4b6rt45i = (_5ke1jwwr + _dxpq0xkr);//* 
							//*                    Zero out below R in A 
							//* 
							
							if (_dxpq0xkr > (int)1)
							{
								
								_rta9tuwm("L" ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_vxfgpup9+((int)2 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
							}
							//* 
							//*                    Bidiagonalize R in A 
							//*                    (Workspace: need 4*N, prefer 3*N + 2*N*NB) 
							//* 
							
							_sf2bwwb1(ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Multiply Q in U by left bidiagonalizing vectors 
							//*                    in A 
							//*                    (Workspace: need 3*N + M, prefer 3*N + M*NB) 
							//* 
							
							_pwi7fryj("Q" ,"R" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Generate right bidiagonalizing vectors in A 
							//*                    (Workspace: need 4*N-1, prefer 3*N + (N-1)*NB) 
							//* 
							
							_cmc4j0e3("P" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_4b6rt45i = (_smxeww0r + _dxpq0xkr);//* 
							//*                    Perform bidiagonal QR iteration, computing left 
							//*                    singular vectors of A in U and computing right 
							//*                    singular vectors of A in A 
							//*                    (Workspace: need BDSPAC) 
							//* 
							
							_nvdqer79("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
							
						}
						//* 
						
					}
					else
					if (_05xnu7gm)
					{
						//* 
						//*                 Path 9 (M much larger than N, JOBU='A', JOBVT='S' 
						//*                         or 'A') 
						//*                 M left singular vectors to be computed in U and 
						//*                 N right singular vectors to be computed in VT 
						//* 
						
						if (_6fnxzlyp >= ((_dxpq0xkr * _dxpq0xkr) + ILNumerics.F2NET.Intrinsics.MAX(_dxpq0xkr + _ev4xhht5 ,(int)4 * _dxpq0xkr ,_24c48sks )))
						{
							//* 
							//*                    Sufficient workspace for a fast algorithm 
							//* 
							
							_j4l29b9c = (int)1;
							if (_6fnxzlyp >= (_loujht2t + (_ocv8fk5c * _dxpq0xkr)))
							{
								//* 
								//*                       WORK(IU) is LDA by N 
								//* 
								
								_lbvz22dx = _ocv8fk5c;
							}
							else
							{
								//* 
								//*                       WORK(IU) is N by N 
								//* 
								
								_lbvz22dx = _dxpq0xkr;
							}
							
							_q1w15vsx = (_j4l29b9c + (_lbvz22dx * _dxpq0xkr));
							_4b6rt45i = (_q1w15vsx + _dxpq0xkr);//* 
							//*                    Compute A=Q*R, copying result to U 
							//*                    (Workspace: need N*N + 2*N, prefer N*N + N + N*NB) 
							//* 
							
							_ac2l6xc0(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );//* 
							//*                    Generate Q in U 
							//*                    (Workspace: need N*N + N + M, prefer N*N + N + M*NB) 
							//* 
							
							_hxix712m(ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Copy R to WORK(IU), zeroing out below it 
							//* 
							
							_hhtvj1kb("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx );
							_rta9tuwm("L" ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_j4l29b9c + (int)1 - 1)),ref _lbvz22dx );
							_smxeww0r = _q1w15vsx;
							_wx1x93f0 = (_smxeww0r + _dxpq0xkr);
							_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
							_4b6rt45i = (_5ke1jwwr + _dxpq0xkr);//* 
							//*                    Bidiagonalize R in WORK(IU), copying result to VT 
							//*                    (Workspace: need N*N + 4*N, prefer N*N + 3*N + 2*N*NB) 
							//* 
							
							_sf2bwwb1(ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_hhtvj1kb("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,_xdbczr8u ,ref _h4ibbatv );//* 
							//*                    Generate left bidiagonalizing vectors in WORK(IU) 
							//*                    (Workspace: need N*N + 4*N, prefer N*N + 3*N + N*NB) 
							//* 
							
							_cmc4j0e3("Q" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Generate right bidiagonalizing vectors in VT 
							//*                    (Workspace: need N*N + 4*N-1, 
							//*                                prefer N*N+3*N+(N-1)*NB) 
							//* 
							
							_cmc4j0e3("P" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_4b6rt45i = (_smxeww0r + _dxpq0xkr);//* 
							//*                    Perform bidiagonal QR iteration, computing left 
							//*                    singular vectors of R in WORK(IU) and computing 
							//*                    right singular vectors of R in VT 
							//*                    (Workspace: need N*N + BDSPAC) 
							//* 
							
							_nvdqer79("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
							//*                    Multiply Q in U by left singular vectors of R in 
							//*                    WORK(IU), storing result in A 
							//*                    (Workspace: need N*N) 
							//* 
							
							_5nsxi69c("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref Unsafe.AsRef(_kxg5drh2) ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,ref Unsafe.AsRef(_d0547bi2) ,_vxfgpup9 ,ref _ocv8fk5c );//* 
							//*                    Copy left singular vectors of A from A to U 
							//* 
							
							_hhtvj1kb("F" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );//* 
							
						}
						else
						{
							//* 
							//*                    Insufficient workspace for a fast algorithm 
							//* 
							
							_q1w15vsx = (int)1;
							_4b6rt45i = (_q1w15vsx + _dxpq0xkr);//* 
							//*                    Compute A=Q*R, copying result to U 
							//*                    (Workspace: need 2*N, prefer N + N*NB) 
							//* 
							
							_ac2l6xc0(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );//* 
							//*                    Generate Q in U 
							//*                    (Workspace: need N + M, prefer N + M*NB) 
							//* 
							
							_hxix712m(ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Copy R from A to VT, zeroing out below it 
							//* 
							
							_hhtvj1kb("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );
							if (_dxpq0xkr > (int)1)
							_rta9tuwm("L" ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_xdbczr8u+((int)2 - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv );
							_smxeww0r = _q1w15vsx;
							_wx1x93f0 = (_smxeww0r + _dxpq0xkr);
							_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
							_4b6rt45i = (_5ke1jwwr + _dxpq0xkr);//* 
							//*                    Bidiagonalize R in VT 
							//*                    (Workspace: need 4*N, prefer 3*N + 2*N*NB) 
							//* 
							
							_sf2bwwb1(ref _dxpq0xkr ,ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Multiply Q in U by left bidiagonalizing vectors 
							//*                    in VT 
							//*                    (Workspace: need 3*N + M, prefer 3*N + M*NB) 
							//* 
							
							_pwi7fryj("Q" ,"R" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_wx1x93f0 - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Generate right bidiagonalizing vectors in VT 
							//*                    (Workspace: need 4*N-1, prefer 3*N + (N-1)*NB) 
							//* 
							
							_cmc4j0e3("P" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_4b6rt45i = (_smxeww0r + _dxpq0xkr);//* 
							//*                    Perform bidiagonal QR iteration, computing left 
							//*                    singular vectors of A in U and computing right 
							//*                    singular vectors of A in VT 
							//*                    (Workspace: need BDSPAC) 
							//* 
							
							_nvdqer79("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_xdbczr8u ,ref _h4ibbatv ,_7u55mqkq ,ref _u6e6d39b ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
							
						}
						//* 
						
					}
					//* 
					
				}
				//* 
				
			}
			else
			{
				//* 
				//*           M .LT. MNTHR 
				//* 
				//*           Path 10 (M at least N, but not much larger) 
				//*           Reduce to bidiagonal form without QR decomposition 
				//* 
				
				_smxeww0r = (int)1;
				_wx1x93f0 = (_smxeww0r + _dxpq0xkr);
				_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
				_4b6rt45i = (_5ke1jwwr + _dxpq0xkr);//* 
				//*           Bidiagonalize A 
				//*           (Workspace: need 3*N + M, prefer 3*N + (M + N)*NB) 
				//* 
				
				_sf2bwwb1(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
				if (_96h5p04b)
				{
					//* 
					//*              If left singular vectors desired in U, copy result to U 
					//*              and generate left bidiagonalizing vectors in U 
					//*              (Workspace: need 3*N + NCU, prefer 3*N + NCU*NB) 
					//* 
					
					_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );
					if (_oigv4c9v)
					_34ov76nq = _dxpq0xkr;
					if (_hfkr5gre)
					_34ov76nq = _ev4xhht5;
					_cmc4j0e3("Q" ,ref _ev4xhht5 ,ref _34ov76nq ,ref _dxpq0xkr ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
				}
				
				if (_05xnu7gm)
				{
					//* 
					//*              If right singular vectors desired in VT, copy result to 
					//*              VT and generate right bidiagonalizing vectors in VT 
					//*              (Workspace: need 4*N-1, prefer 3*N + (N-1)*NB) 
					//* 
					
					_hhtvj1kb("U" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );
					_cmc4j0e3("P" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
				}
				
				if (_utderuk7)
				{
					//* 
					//*              If left singular vectors desired in A, generate left 
					//*              bidiagonalizing vectors in A 
					//*              (Workspace: need 4*N, prefer 3*N + N*NB) 
					//* 
					
					_cmc4j0e3("Q" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
				}
				
				if (_wkqhtihq)
				{
					//* 
					//*              If right singular vectors desired in A, generate right 
					//*              bidiagonalizing vectors in A 
					//*              (Workspace: need 4*N-1, prefer 3*N + (N-1)*NB) 
					//* 
					
					_cmc4j0e3("P" ,ref _dxpq0xkr ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
				}
				
				_4b6rt45i = (_smxeww0r + _dxpq0xkr);
				if (_96h5p04b | _utderuk7)
				_tg62ial7 = _ev4xhht5;
				if (_s7btv50y)
				_tg62ial7 = (int)0;
				if (_05xnu7gm | _wkqhtihq)
				_0v6rtqiq = _dxpq0xkr;
				if (_ov7mcmwp)
				_0v6rtqiq = (int)0;
				if ((!(_utderuk7)) & (!(_wkqhtihq)))
				{
					//* 
					//*              Perform bidiagonal QR iteration, if desired, computing 
					//*              left singular vectors in U and computing right singular 
					//*              vectors in VT 
					//*              (Workspace: need BDSPAC) 
					//* 
					
					_nvdqer79("U" ,ref _dxpq0xkr ,ref _0v6rtqiq ,ref _tg62ial7 ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_xdbczr8u ,ref _h4ibbatv ,_7u55mqkq ,ref _u6e6d39b ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );
				}
				else
				if ((!(_utderuk7)) & _wkqhtihq)
				{
					//* 
					//*              Perform bidiagonal QR iteration, if desired, computing 
					//*              left singular vectors in U and computing right singular 
					//*              vectors in A 
					//*              (Workspace: need BDSPAC) 
					//* 
					
					_nvdqer79("U" ,ref _dxpq0xkr ,ref _0v6rtqiq ,ref _tg62ial7 ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );
				}
				else
				{
					//* 
					//*              Perform bidiagonal QR iteration, if desired, computing 
					//*              left singular vectors in A and computing right singular 
					//*              vectors in VT 
					//*              (Workspace: need BDSPAC) 
					//* 
					
					_nvdqer79("U" ,ref _dxpq0xkr ,ref _0v6rtqiq ,ref _tg62ial7 ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_xdbczr8u ,ref _h4ibbatv ,_vxfgpup9 ,ref _ocv8fk5c ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );
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
				
				if (_ov7mcmwp)
				{
					//* 
					//*              Path 1t(N much larger than M, JOBVT='N') 
					//*              No right singular vectors to be computed 
					//* 
					
					_q1w15vsx = (int)1;
					_4b6rt45i = (_q1w15vsx + _ev4xhht5);//* 
					//*              Compute A=L*Q 
					//*              (Workspace: need 2*M, prefer M + M*NB) 
					//* 
					
					_bxcqf0ji(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
					//*              Zero out above L 
					//* 
					
					_rta9tuwm("U" ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_vxfgpup9+((int)1 - 1) + ((int)2 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
					_smxeww0r = (int)1;
					_wx1x93f0 = (_smxeww0r + _ev4xhht5);
					_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
					_4b6rt45i = (_5ke1jwwr + _ev4xhht5);//* 
					//*              Bidiagonalize L in A 
					//*              (Workspace: need 4*M, prefer 3*M + 2*M*NB) 
					//* 
					
					_sf2bwwb1(ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
					if (_utderuk7 | _96h5p04b)
					{
						//* 
						//*                 If left singular vectors desired, generate Q 
						//*                 (Workspace: need 4*M, prefer 3*M + M*NB) 
						//* 
						
						_cmc4j0e3("Q" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
					}
					
					_4b6rt45i = (_smxeww0r + _ev4xhht5);
					_tg62ial7 = (int)0;
					if (_utderuk7 | _96h5p04b)
					_tg62ial7 = _ev4xhht5;//* 
					//*              Perform bidiagonal QR iteration, computing left singular 
					//*              vectors of A in A if desired 
					//*              (Workspace: need BDSPAC) 
					//* 
					
					_nvdqer79("U" ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)0) ,ref _tg62ial7 ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_vxfgpup9 ,ref _ocv8fk5c ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
					//*              If left singular vectors desired in U, copy them there 
					//* 
					
					if (_96h5p04b)
					_hhtvj1kb("F" ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );//* 
					
				}
				else
				if (_wkqhtihq & _s7btv50y)
				{
					//* 
					//*              Path 2t(N much larger than M, JOBU='N', JOBVT='O') 
					//*              M right singular vectors to be overwritten on A and 
					//*              no left singular vectors to be computed 
					//* 
					
					if (_6fnxzlyp >= ((_ev4xhht5 * _ev4xhht5) + ILNumerics.F2NET.Intrinsics.MAX((int)4 * _ev4xhht5 ,_24c48sks )))
					{
						//* 
						//*                 Sufficient workspace for a fast algorithm 
						//* 
						
						_m9w6lk7x = (int)1;
						if (_6fnxzlyp >= (ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,(_ocv8fk5c * _dxpq0xkr) + _ev4xhht5 ) + (_ocv8fk5c * _ev4xhht5)))
						{
							//* 
							//*                    WORK(IU) is LDA by N and WORK(IR) is LDA by M 
							//* 
							
							_lbvz22dx = _ocv8fk5c;
							_8rfn4f7g = _dxpq0xkr;
							_v6sbkzy4 = _ocv8fk5c;
						}
						else
						if (_6fnxzlyp >= (ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,(_ocv8fk5c * _dxpq0xkr) + _ev4xhht5 ) + (_ev4xhht5 * _ev4xhht5)))
						{
							//* 
							//*                    WORK(IU) is LDA by N and WORK(IR) is M by M 
							//* 
							
							_lbvz22dx = _ocv8fk5c;
							_8rfn4f7g = _dxpq0xkr;
							_v6sbkzy4 = _ev4xhht5;
						}
						else
						{
							//* 
							//*                    WORK(IU) is M by CHUNK and WORK(IR) is M by M 
							//* 
							
							_lbvz22dx = _ev4xhht5;
							_8rfn4f7g = (((_6fnxzlyp - (_ev4xhht5 * _ev4xhht5)) - _ev4xhht5) / _ev4xhht5);
							_v6sbkzy4 = _ev4xhht5;
						}
						
						_q1w15vsx = (_m9w6lk7x + (_v6sbkzy4 * _ev4xhht5));
						_4b6rt45i = (_q1w15vsx + _ev4xhht5);//* 
						//*                 Compute A=L*Q 
						//*                 (Workspace: need M*M + 2*M, prefer M*M + M + M*NB) 
						//* 
						
						_bxcqf0ji(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
						//*                 Copy L to WORK(IR) and zero out above it 
						//* 
						
						_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 );
						_rta9tuwm("U" ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_m9w6lk7x + _v6sbkzy4 - 1)),ref _v6sbkzy4 );//* 
						//*                 Generate Q in A 
						//*                 (Workspace: need M*M + 2*M, prefer M*M + M + M*NB) 
						//* 
						
						_n6025boa(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
						_smxeww0r = _q1w15vsx;
						_wx1x93f0 = (_smxeww0r + _ev4xhht5);
						_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
						_4b6rt45i = (_5ke1jwwr + _ev4xhht5);//* 
						//*                 Bidiagonalize L in WORK(IR) 
						//*                 (Workspace: need M*M + 4*M, prefer M*M + 3*M + 2*M*NB) 
						//* 
						
						_sf2bwwb1(ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
						//*                 Generate right vectors bidiagonalizing L 
						//*                 (Workspace: need M*M + 4*M-1, prefer M*M + 3*M + (M-1)*NB) 
						//* 
						
						_cmc4j0e3("P" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
						_4b6rt45i = (_smxeww0r + _ev4xhht5);//* 
						//*                 Perform bidiagonal QR iteration, computing right 
						//*                 singular vectors of L in WORK(IR) 
						//*                 (Workspace: need M*M + BDSPAC) 
						//* 
						
						_nvdqer79("U" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );
						_j4l29b9c = (_smxeww0r + _ev4xhht5);//* 
						//*                 Multiply right singular vectors of L in WORK(IR) by Q 
						//*                 in A, storing result in WORK(IU) and copying to A 
						//*                 (Workspace: need M*M + 2*M, prefer M*M + M*N + M) 
						//* 
						
						{
							System.Int32 __81fgg2dlsvn1356 = (System.Int32)((int)1);
							System.Int32 __81fgg2step1356 = (System.Int32)(_8rfn4f7g);
							System.Int32 __81fgg2count1356;
							for (__81fgg2count1356 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1356 + __81fgg2step1356) / __81fgg2step1356)), _b5p6od9s = __81fgg2dlsvn1356; __81fgg2count1356 != 0; __81fgg2count1356--, _b5p6od9s += (__81fgg2step1356)) {

							{
								
								_9giy8o3g = ILNumerics.F2NET.Intrinsics.MIN((_dxpq0xkr - _b5p6od9s) + (int)1 ,_8rfn4f7g );
								_5nsxi69c("N" ,"N" ,ref _ev4xhht5 ,ref _9giy8o3g ,ref _ev4xhht5 ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx );
								_hhtvj1kb("F" ,ref _ev4xhht5 ,ref _9giy8o3g ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
Mark30:;
								// continue
							}
														}						}//* 
						
					}
					else
					{
						//* 
						//*                 Insufficient workspace for a fast algorithm 
						//* 
						
						_smxeww0r = (int)1;
						_wx1x93f0 = (_smxeww0r + _ev4xhht5);
						_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
						_4b6rt45i = (_5ke1jwwr + _ev4xhht5);//* 
						//*                 Bidiagonalize A 
						//*                 (Workspace: need 3*M + N, prefer 3*M + (M + N)*NB) 
						//* 
						
						_sf2bwwb1(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
						//*                 Generate right vectors bidiagonalizing A 
						//*                 (Workspace: need 4*M, prefer 3*M + M*NB) 
						//* 
						
						_cmc4j0e3("P" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
						_4b6rt45i = (_smxeww0r + _ev4xhht5);//* 
						//*                 Perform bidiagonal QR iteration, computing right 
						//*                 singular vectors of A in A 
						//*                 (Workspace: need BDSPAC) 
						//* 
						
						_nvdqer79("L" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_vxfgpup9 ,ref _ocv8fk5c ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
						
					}
					//* 
					
				}
				else
				if (_wkqhtihq & _96h5p04b)
				{
					//* 
					//*              Path 3t(N much larger than M, JOBU='S' or 'A', JOBVT='O') 
					//*              M right singular vectors to be overwritten on A and 
					//*              M left singular vectors to be computed in U 
					//* 
					
					if (_6fnxzlyp >= ((_ev4xhht5 * _ev4xhht5) + ILNumerics.F2NET.Intrinsics.MAX((int)4 * _ev4xhht5 ,_24c48sks )))
					{
						//* 
						//*                 Sufficient workspace for a fast algorithm 
						//* 
						
						_m9w6lk7x = (int)1;
						if (_6fnxzlyp >= (ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,(_ocv8fk5c * _dxpq0xkr) + _ev4xhht5 ) + (_ocv8fk5c * _ev4xhht5)))
						{
							//* 
							//*                    WORK(IU) is LDA by N and WORK(IR) is LDA by M 
							//* 
							
							_lbvz22dx = _ocv8fk5c;
							_8rfn4f7g = _dxpq0xkr;
							_v6sbkzy4 = _ocv8fk5c;
						}
						else
						if (_6fnxzlyp >= (ILNumerics.F2NET.Intrinsics.MAX(_loujht2t ,(_ocv8fk5c * _dxpq0xkr) + _ev4xhht5 ) + (_ev4xhht5 * _ev4xhht5)))
						{
							//* 
							//*                    WORK(IU) is LDA by N and WORK(IR) is M by M 
							//* 
							
							_lbvz22dx = _ocv8fk5c;
							_8rfn4f7g = _dxpq0xkr;
							_v6sbkzy4 = _ev4xhht5;
						}
						else
						{
							//* 
							//*                    WORK(IU) is M by CHUNK and WORK(IR) is M by M 
							//* 
							
							_lbvz22dx = _ev4xhht5;
							_8rfn4f7g = (((_6fnxzlyp - (_ev4xhht5 * _ev4xhht5)) - _ev4xhht5) / _ev4xhht5);
							_v6sbkzy4 = _ev4xhht5;
						}
						
						_q1w15vsx = (_m9w6lk7x + (_v6sbkzy4 * _ev4xhht5));
						_4b6rt45i = (_q1w15vsx + _ev4xhht5);//* 
						//*                 Compute A=L*Q 
						//*                 (Workspace: need M*M + 2*M, prefer M*M + M + M*NB) 
						//* 
						
						_bxcqf0ji(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
						//*                 Copy L to U, zeroing about above it 
						//* 
						
						_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );
						_rta9tuwm("U" ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_7u55mqkq+((int)1 - 1) + ((int)2 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b );//* 
						//*                 Generate Q in A 
						//*                 (Workspace: need M*M + 2*M, prefer M*M + M + M*NB) 
						//* 
						
						_n6025boa(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
						_smxeww0r = _q1w15vsx;
						_wx1x93f0 = (_smxeww0r + _ev4xhht5);
						_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
						_4b6rt45i = (_5ke1jwwr + _ev4xhht5);//* 
						//*                 Bidiagonalize L in U, copying result to WORK(IR) 
						//*                 (Workspace: need M*M + 4*M, prefer M*M + 3*M + 2*M*NB) 
						//* 
						
						_sf2bwwb1(ref _ev4xhht5 ,ref _ev4xhht5 ,_7u55mqkq ,ref _u6e6d39b ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
						_hhtvj1kb("U" ,ref _ev4xhht5 ,ref _ev4xhht5 ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 );//* 
						//*                 Generate right vectors bidiagonalizing L in WORK(IR) 
						//*                 (Workspace: need M*M + 4*M-1, prefer M*M + 3*M + (M-1)*NB) 
						//* 
						
						_cmc4j0e3("P" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
						//*                 Generate left vectors bidiagonalizing L in U 
						//*                 (Workspace: need M*M + 4*M, prefer M*M + 3*M + M*NB) 
						//* 
						
						_cmc4j0e3("Q" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
						_4b6rt45i = (_smxeww0r + _ev4xhht5);//* 
						//*                 Perform bidiagonal QR iteration, computing left 
						//*                 singular vectors of L in U, and computing right 
						//*                 singular vectors of L in WORK(IR) 
						//*                 (Workspace: need M*M + BDSPAC) 
						//* 
						
						_nvdqer79("U" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,_7u55mqkq ,ref _u6e6d39b ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );
						_j4l29b9c = (_smxeww0r + _ev4xhht5);//* 
						//*                 Multiply right singular vectors of L in WORK(IR) by Q 
						//*                 in A, storing result in WORK(IU) and copying to A 
						//*                 (Workspace: need M*M + 2*M, prefer M*M + M*N + M)) 
						//* 
						
						{
							System.Int32 __81fgg2dlsvn1357 = (System.Int32)((int)1);
							System.Int32 __81fgg2step1357 = (System.Int32)(_8rfn4f7g);
							System.Int32 __81fgg2count1357;
							for (__81fgg2count1357 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1357 + __81fgg2step1357) / __81fgg2step1357)), _b5p6od9s = __81fgg2dlsvn1357; __81fgg2count1357 != 0; __81fgg2count1357--, _b5p6od9s += (__81fgg2step1357)) {

							{
								
								_9giy8o3g = ILNumerics.F2NET.Intrinsics.MIN((_dxpq0xkr - _b5p6od9s) + (int)1 ,_8rfn4f7g );
								_5nsxi69c("N" ,"N" ,ref _ev4xhht5 ,ref _9giy8o3g ,ref _ev4xhht5 ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx );
								_hhtvj1kb("F" ,ref _ev4xhht5 ,ref _9giy8o3g ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
Mark40:;
								// continue
							}
														}						}//* 
						
					}
					else
					{
						//* 
						//*                 Insufficient workspace for a fast algorithm 
						//* 
						
						_q1w15vsx = (int)1;
						_4b6rt45i = (_q1w15vsx + _ev4xhht5);//* 
						//*                 Compute A=L*Q 
						//*                 (Workspace: need 2*M, prefer M + M*NB) 
						//* 
						
						_bxcqf0ji(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
						//*                 Copy L to U, zeroing out above it 
						//* 
						
						_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );
						_rta9tuwm("U" ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_7u55mqkq+((int)1 - 1) + ((int)2 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b );//* 
						//*                 Generate Q in A 
						//*                 (Workspace: need 2*M, prefer M + M*NB) 
						//* 
						
						_n6025boa(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
						_smxeww0r = _q1w15vsx;
						_wx1x93f0 = (_smxeww0r + _ev4xhht5);
						_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
						_4b6rt45i = (_5ke1jwwr + _ev4xhht5);//* 
						//*                 Bidiagonalize L in U 
						//*                 (Workspace: need 4*M, prefer 3*M + 2*M*NB) 
						//* 
						
						_sf2bwwb1(ref _ev4xhht5 ,ref _ev4xhht5 ,_7u55mqkq ,ref _u6e6d39b ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
						//*                 Multiply right vectors bidiagonalizing L by Q in A 
						//*                 (Workspace: need 3*M + N, prefer 3*M + N*NB) 
						//* 
						
						_pwi7fryj("P" ,"L" ,"T" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_5ke1jwwr - 1)),_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
						//*                 Generate left vectors bidiagonalizing L in U 
						//*                 (Workspace: need 4*M, prefer 3*M + M*NB) 
						//* 
						
						_cmc4j0e3("Q" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
						_4b6rt45i = (_smxeww0r + _ev4xhht5);//* 
						//*                 Perform bidiagonal QR iteration, computing left 
						//*                 singular vectors of A in U and computing right 
						//*                 singular vectors of A in A 
						//*                 (Workspace: need BDSPAC) 
						//* 
						
						_nvdqer79("U" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
						
					}
					//* 
					
				}
				else
				if (_8pgl0qdq)
				{
					//* 
					
					if (_s7btv50y)
					{
						//* 
						//*                 Path 4t(N much larger than M, JOBU='N', JOBVT='S') 
						//*                 M right singular vectors to be computed in VT and 
						//*                 no left singular vectors to be computed 
						//* 
						
						if (_6fnxzlyp >= ((_ev4xhht5 * _ev4xhht5) + ILNumerics.F2NET.Intrinsics.MAX((int)4 * _ev4xhht5 ,_24c48sks )))
						{
							//* 
							//*                    Sufficient workspace for a fast algorithm 
							//* 
							
							_m9w6lk7x = (int)1;
							if (_6fnxzlyp >= (_loujht2t + (_ocv8fk5c * _ev4xhht5)))
							{
								//* 
								//*                       WORK(IR) is LDA by M 
								//* 
								
								_v6sbkzy4 = _ocv8fk5c;
							}
							else
							{
								//* 
								//*                       WORK(IR) is M by M 
								//* 
								
								_v6sbkzy4 = _ev4xhht5;
							}
							
							_q1w15vsx = (_m9w6lk7x + (_v6sbkzy4 * _ev4xhht5));
							_4b6rt45i = (_q1w15vsx + _ev4xhht5);//* 
							//*                    Compute A=L*Q 
							//*                    (Workspace: need M*M + 2*M, prefer M*M + M + M*NB) 
							//* 
							
							_bxcqf0ji(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Copy L to WORK(IR), zeroing out above it 
							//* 
							
							_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 );
							_rta9tuwm("U" ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_m9w6lk7x + _v6sbkzy4 - 1)),ref _v6sbkzy4 );//* 
							//*                    Generate Q in A 
							//*                    (Workspace: need M*M + 2*M, prefer M*M + M + M*NB) 
							//* 
							
							_n6025boa(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_smxeww0r = _q1w15vsx;
							_wx1x93f0 = (_smxeww0r + _ev4xhht5);
							_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
							_4b6rt45i = (_5ke1jwwr + _ev4xhht5);//* 
							//*                    Bidiagonalize L in WORK(IR) 
							//*                    (Workspace: need M*M + 4*M, prefer M*M + 3*M + 2*M*NB) 
							//* 
							
							_sf2bwwb1(ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Generate right vectors bidiagonalizing L in 
							//*                    WORK(IR) 
							//*                    (Workspace: need M*M + 4*M, prefer M*M + 3*M + (M-1)*NB) 
							//* 
							
							_cmc4j0e3("P" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_4b6rt45i = (_smxeww0r + _ev4xhht5);//* 
							//*                    Perform bidiagonal QR iteration, computing right 
							//*                    singular vectors of L in WORK(IR) 
							//*                    (Workspace: need M*M + BDSPAC) 
							//* 
							
							_nvdqer79("U" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
							//*                    Multiply right singular vectors of L in WORK(IR) by 
							//*                    Q in A, storing result in VT 
							//*                    (Workspace: need M*M) 
							//* 
							
							_5nsxi69c("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,_vxfgpup9 ,ref _ocv8fk5c ,ref Unsafe.AsRef(_d0547bi2) ,_xdbczr8u ,ref _h4ibbatv );//* 
							
						}
						else
						{
							//* 
							//*                    Insufficient workspace for a fast algorithm 
							//* 
							
							_q1w15vsx = (int)1;
							_4b6rt45i = (_q1w15vsx + _ev4xhht5);//* 
							//*                    Compute A=L*Q 
							//*                    (Workspace: need 2*M, prefer M + M*NB) 
							//* 
							
							_bxcqf0ji(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Copy result to VT 
							//* 
							
							_hhtvj1kb("U" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );//* 
							//*                    Generate Q in VT 
							//*                    (Workspace: need 2*M, prefer M + M*NB) 
							//* 
							
							_n6025boa(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_smxeww0r = _q1w15vsx;
							_wx1x93f0 = (_smxeww0r + _ev4xhht5);
							_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
							_4b6rt45i = (_5ke1jwwr + _ev4xhht5);//* 
							//*                    Zero out above L in A 
							//* 
							
							_rta9tuwm("U" ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_vxfgpup9+((int)1 - 1) + ((int)2 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );//* 
							//*                    Bidiagonalize L in A 
							//*                    (Workspace: need 4*M, prefer 3*M + 2*M*NB) 
							//* 
							
							_sf2bwwb1(ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Multiply right vectors bidiagonalizing L by Q in VT 
							//*                    (Workspace: need 3*M + N, prefer 3*M + N*NB) 
							//* 
							
							_pwi7fryj("P" ,"L" ,"T" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_4b6rt45i = (_smxeww0r + _ev4xhht5);//* 
							//*                    Perform bidiagonal QR iteration, computing right 
							//*                    singular vectors of A in VT 
							//*                    (Workspace: need BDSPAC) 
							//* 
							
							_nvdqer79("U" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_xdbczr8u ,ref _h4ibbatv ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
							
						}
						//* 
						
					}
					else
					if (_utderuk7)
					{
						//* 
						//*                 Path 5t(N much larger than M, JOBU='O', JOBVT='S') 
						//*                 M right singular vectors to be computed in VT and 
						//*                 M left singular vectors to be overwritten on A 
						//* 
						
						if (_6fnxzlyp >= ((((int)2 * _ev4xhht5) * _ev4xhht5) + ILNumerics.F2NET.Intrinsics.MAX((int)4 * _ev4xhht5 ,_24c48sks )))
						{
							//* 
							//*                    Sufficient workspace for a fast algorithm 
							//* 
							
							_j4l29b9c = (int)1;
							if (_6fnxzlyp >= (_loujht2t + (((int)2 * _ocv8fk5c) * _ev4xhht5)))
							{
								//* 
								//*                       WORK(IU) is LDA by M and WORK(IR) is LDA by M 
								//* 
								
								_lbvz22dx = _ocv8fk5c;
								_m9w6lk7x = (_j4l29b9c + (_lbvz22dx * _ev4xhht5));
								_v6sbkzy4 = _ocv8fk5c;
							}
							else
							if (_6fnxzlyp >= (_loujht2t + ((_ocv8fk5c + _ev4xhht5) * _ev4xhht5)))
							{
								//* 
								//*                       WORK(IU) is LDA by M and WORK(IR) is M by M 
								//* 
								
								_lbvz22dx = _ocv8fk5c;
								_m9w6lk7x = (_j4l29b9c + (_lbvz22dx * _ev4xhht5));
								_v6sbkzy4 = _ev4xhht5;
							}
							else
							{
								//* 
								//*                       WORK(IU) is M by M and WORK(IR) is M by M 
								//* 
								
								_lbvz22dx = _ev4xhht5;
								_m9w6lk7x = (_j4l29b9c + (_lbvz22dx * _ev4xhht5));
								_v6sbkzy4 = _ev4xhht5;
							}
							
							_q1w15vsx = (_m9w6lk7x + (_v6sbkzy4 * _ev4xhht5));
							_4b6rt45i = (_q1w15vsx + _ev4xhht5);//* 
							//*                    Compute A=L*Q 
							//*                    (Workspace: need 2*M*M + 2*M, prefer 2*M*M + M + M*NB) 
							//* 
							
							_bxcqf0ji(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Copy L to WORK(IU), zeroing out below it 
							//* 
							
							_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx );
							_rta9tuwm("U" ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_j4l29b9c + _lbvz22dx - 1)),ref _lbvz22dx );//* 
							//*                    Generate Q in A 
							//*                    (Workspace: need 2*M*M + 2*M, prefer 2*M*M + M + M*NB) 
							//* 
							
							_n6025boa(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_smxeww0r = _q1w15vsx;
							_wx1x93f0 = (_smxeww0r + _ev4xhht5);
							_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
							_4b6rt45i = (_5ke1jwwr + _ev4xhht5);//* 
							//*                    Bidiagonalize L in WORK(IU), copying result to 
							//*                    WORK(IR) 
							//*                    (Workspace: need 2*M*M + 4*M, 
							//*                                prefer 2*M*M+3*M+2*M*NB) 
							//* 
							
							_sf2bwwb1(ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 );//* 
							//*                    Generate right bidiagonalizing vectors in WORK(IU) 
							//*                    (Workspace: need 2*M*M + 4*M-1, 
							//*                                prefer 2*M*M+3*M+(M-1)*NB) 
							//* 
							
							_cmc4j0e3("P" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Generate left bidiagonalizing vectors in WORK(IR) 
							//*                    (Workspace: need 2*M*M + 4*M, prefer 2*M*M + 3*M + M*NB) 
							//* 
							
							_cmc4j0e3("Q" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_4b6rt45i = (_smxeww0r + _ev4xhht5);//* 
							//*                    Perform bidiagonal QR iteration, computing left 
							//*                    singular vectors of L in WORK(IR) and computing 
							//*                    right singular vectors of L in WORK(IU) 
							//*                    (Workspace: need 2*M*M + BDSPAC) 
							//* 
							
							_nvdqer79("U" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
							//*                    Multiply right singular vectors of L in WORK(IU) by 
							//*                    Q in A, storing result in VT 
							//*                    (Workspace: need M*M) 
							//* 
							
							_5nsxi69c("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,_vxfgpup9 ,ref _ocv8fk5c ,ref Unsafe.AsRef(_d0547bi2) ,_xdbczr8u ,ref _h4ibbatv );//* 
							//*                    Copy left singular vectors of L to A 
							//*                    (Workspace: need M*M) 
							//* 
							
							_hhtvj1kb("F" ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,_vxfgpup9 ,ref _ocv8fk5c );//* 
							
						}
						else
						{
							//* 
							//*                    Insufficient workspace for a fast algorithm 
							//* 
							
							_q1w15vsx = (int)1;
							_4b6rt45i = (_q1w15vsx + _ev4xhht5);//* 
							//*                    Compute A=L*Q, copying result to VT 
							//*                    (Workspace: need 2*M, prefer M + M*NB) 
							//* 
							
							_bxcqf0ji(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_hhtvj1kb("U" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );//* 
							//*                    Generate Q in VT 
							//*                    (Workspace: need 2*M, prefer M + M*NB) 
							//* 
							
							_n6025boa(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_smxeww0r = _q1w15vsx;
							_wx1x93f0 = (_smxeww0r + _ev4xhht5);
							_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
							_4b6rt45i = (_5ke1jwwr + _ev4xhht5);//* 
							//*                    Zero out above L in A 
							//* 
							
							_rta9tuwm("U" ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_vxfgpup9+((int)1 - 1) + ((int)2 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );//* 
							//*                    Bidiagonalize L in A 
							//*                    (Workspace: need 4*M, prefer 3*M + 2*M*NB) 
							//* 
							
							_sf2bwwb1(ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Multiply right vectors bidiagonalizing L by Q in VT 
							//*                    (Workspace: need 3*M + N, prefer 3*M + N*NB) 
							//* 
							
							_pwi7fryj("P" ,"L" ,"T" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Generate left bidiagonalizing vectors of L in A 
							//*                    (Workspace: need 4*M, prefer 3*M + M*NB) 
							//* 
							
							_cmc4j0e3("Q" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_4b6rt45i = (_smxeww0r + _ev4xhht5);//* 
							//*                    Perform bidiagonal QR iteration, compute left 
							//*                    singular vectors of A in A and compute right 
							//*                    singular vectors of A in VT 
							//*                    (Workspace: need BDSPAC) 
							//* 
							
							_nvdqer79("U" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_xdbczr8u ,ref _h4ibbatv ,_vxfgpup9 ,ref _ocv8fk5c ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
							
						}
						//* 
						
					}
					else
					if (_96h5p04b)
					{
						//* 
						//*                 Path 6t(N much larger than M, JOBU='S' or 'A', 
						//*                         JOBVT='S') 
						//*                 M right singular vectors to be computed in VT and 
						//*                 M left singular vectors to be computed in U 
						//* 
						
						if (_6fnxzlyp >= ((_ev4xhht5 * _ev4xhht5) + ILNumerics.F2NET.Intrinsics.MAX((int)4 * _ev4xhht5 ,_24c48sks )))
						{
							//* 
							//*                    Sufficient workspace for a fast algorithm 
							//* 
							
							_j4l29b9c = (int)1;
							if (_6fnxzlyp >= (_loujht2t + (_ocv8fk5c * _ev4xhht5)))
							{
								//* 
								//*                       WORK(IU) is LDA by N 
								//* 
								
								_lbvz22dx = _ocv8fk5c;
							}
							else
							{
								//* 
								//*                       WORK(IU) is LDA by M 
								//* 
								
								_lbvz22dx = _ev4xhht5;
							}
							
							_q1w15vsx = (_j4l29b9c + (_lbvz22dx * _ev4xhht5));
							_4b6rt45i = (_q1w15vsx + _ev4xhht5);//* 
							//*                    Compute A=L*Q 
							//*                    (Workspace: need M*M + 2*M, prefer M*M + M + M*NB) 
							//* 
							
							_bxcqf0ji(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Copy L to WORK(IU), zeroing out above it 
							//* 
							
							_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx );
							_rta9tuwm("U" ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_j4l29b9c + _lbvz22dx - 1)),ref _lbvz22dx );//* 
							//*                    Generate Q in A 
							//*                    (Workspace: need M*M + 2*M, prefer M*M + M + M*NB) 
							//* 
							
							_n6025boa(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_smxeww0r = _q1w15vsx;
							_wx1x93f0 = (_smxeww0r + _ev4xhht5);
							_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
							_4b6rt45i = (_5ke1jwwr + _ev4xhht5);//* 
							//*                    Bidiagonalize L in WORK(IU), copying result to U 
							//*                    (Workspace: need M*M + 4*M, prefer M*M + 3*M + 2*M*NB) 
							//* 
							
							_sf2bwwb1(ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,_7u55mqkq ,ref _u6e6d39b );//* 
							//*                    Generate right bidiagonalizing vectors in WORK(IU) 
							//*                    (Workspace: need M*M + 4*M-1, 
							//*                                prefer M*M+3*M+(M-1)*NB) 
							//* 
							
							_cmc4j0e3("P" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Generate left bidiagonalizing vectors in U 
							//*                    (Workspace: need M*M + 4*M, prefer M*M + 3*M + M*NB) 
							//* 
							
							_cmc4j0e3("Q" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_4b6rt45i = (_smxeww0r + _ev4xhht5);//* 
							//*                    Perform bidiagonal QR iteration, computing left 
							//*                    singular vectors of L in U and computing right 
							//*                    singular vectors of L in WORK(IU) 
							//*                    (Workspace: need M*M + BDSPAC) 
							//* 
							
							_nvdqer79("U" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,_7u55mqkq ,ref _u6e6d39b ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
							//*                    Multiply right singular vectors of L in WORK(IU) by 
							//*                    Q in A, storing result in VT 
							//*                    (Workspace: need M*M) 
							//* 
							
							_5nsxi69c("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,_vxfgpup9 ,ref _ocv8fk5c ,ref Unsafe.AsRef(_d0547bi2) ,_xdbczr8u ,ref _h4ibbatv );//* 
							
						}
						else
						{
							//* 
							//*                    Insufficient workspace for a fast algorithm 
							//* 
							
							_q1w15vsx = (int)1;
							_4b6rt45i = (_q1w15vsx + _ev4xhht5);//* 
							//*                    Compute A=L*Q, copying result to VT 
							//*                    (Workspace: need 2*M, prefer M + M*NB) 
							//* 
							
							_bxcqf0ji(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_hhtvj1kb("U" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );//* 
							//*                    Generate Q in VT 
							//*                    (Workspace: need 2*M, prefer M + M*NB) 
							//* 
							
							_n6025boa(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Copy L to U, zeroing out above it 
							//* 
							
							_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );
							_rta9tuwm("U" ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_7u55mqkq+((int)1 - 1) + ((int)2 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b );
							_smxeww0r = _q1w15vsx;
							_wx1x93f0 = (_smxeww0r + _ev4xhht5);
							_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
							_4b6rt45i = (_5ke1jwwr + _ev4xhht5);//* 
							//*                    Bidiagonalize L in U 
							//*                    (Workspace: need 4*M, prefer 3*M + 2*M*NB) 
							//* 
							
							_sf2bwwb1(ref _ev4xhht5 ,ref _ev4xhht5 ,_7u55mqkq ,ref _u6e6d39b ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Multiply right bidiagonalizing vectors in U by Q 
							//*                    in VT 
							//*                    (Workspace: need 3*M + N, prefer 3*M + N*NB) 
							//* 
							
							_pwi7fryj("P" ,"L" ,"T" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_5ke1jwwr - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Generate left bidiagonalizing vectors in U 
							//*                    (Workspace: need 4*M, prefer 3*M + M*NB) 
							//* 
							
							_cmc4j0e3("Q" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_4b6rt45i = (_smxeww0r + _ev4xhht5);//* 
							//*                    Perform bidiagonal QR iteration, computing left 
							//*                    singular vectors of A in U and computing right 
							//*                    singular vectors of A in VT 
							//*                    (Workspace: need BDSPAC) 
							//* 
							
							_nvdqer79("U" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_xdbczr8u ,ref _h4ibbatv ,_7u55mqkq ,ref _u6e6d39b ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
							
						}
						//* 
						
					}
					//* 
					
				}
				else
				if (_njekud0j)
				{
					//* 
					
					if (_s7btv50y)
					{
						//* 
						//*                 Path 7t(N much larger than M, JOBU='N', JOBVT='A') 
						//*                 N right singular vectors to be computed in VT and 
						//*                 no left singular vectors to be computed 
						//* 
						
						if (_6fnxzlyp >= ((_ev4xhht5 * _ev4xhht5) + ILNumerics.F2NET.Intrinsics.MAX(_dxpq0xkr + _ev4xhht5 ,(int)4 * _ev4xhht5 ,_24c48sks )))
						{
							//* 
							//*                    Sufficient workspace for a fast algorithm 
							//* 
							
							_m9w6lk7x = (int)1;
							if (_6fnxzlyp >= (_loujht2t + (_ocv8fk5c * _ev4xhht5)))
							{
								//* 
								//*                       WORK(IR) is LDA by M 
								//* 
								
								_v6sbkzy4 = _ocv8fk5c;
							}
							else
							{
								//* 
								//*                       WORK(IR) is M by M 
								//* 
								
								_v6sbkzy4 = _ev4xhht5;
							}
							
							_q1w15vsx = (_m9w6lk7x + (_v6sbkzy4 * _ev4xhht5));
							_4b6rt45i = (_q1w15vsx + _ev4xhht5);//* 
							//*                    Compute A=L*Q, copying result to VT 
							//*                    (Workspace: need M*M + 2*M, prefer M*M + M + M*NB) 
							//* 
							
							_bxcqf0ji(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_hhtvj1kb("U" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );//* 
							//*                    Copy L to WORK(IR), zeroing out above it 
							//* 
							
							_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 );
							_rta9tuwm("U" ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_m9w6lk7x + _v6sbkzy4 - 1)),ref _v6sbkzy4 );//* 
							//*                    Generate Q in VT 
							//*                    (Workspace: need M*M + M + N, prefer M*M + M + N*NB) 
							//* 
							
							_n6025boa(ref _dxpq0xkr ,ref _dxpq0xkr ,ref _ev4xhht5 ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_smxeww0r = _q1w15vsx;
							_wx1x93f0 = (_smxeww0r + _ev4xhht5);
							_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
							_4b6rt45i = (_5ke1jwwr + _ev4xhht5);//* 
							//*                    Bidiagonalize L in WORK(IR) 
							//*                    (Workspace: need M*M + 4*M, prefer M*M + 3*M + 2*M*NB) 
							//* 
							
							_sf2bwwb1(ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Generate right bidiagonalizing vectors in WORK(IR) 
							//*                    (Workspace: need M*M + 4*M-1, 
							//*                                prefer M*M+3*M+(M-1)*NB) 
							//* 
							
							_cmc4j0e3("P" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_4b6rt45i = (_smxeww0r + _ev4xhht5);//* 
							//*                    Perform bidiagonal QR iteration, computing right 
							//*                    singular vectors of L in WORK(IR) 
							//*                    (Workspace: need M*M + BDSPAC) 
							//* 
							
							_nvdqer79("U" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
							//*                    Multiply right singular vectors of L in WORK(IR) by 
							//*                    Q in VT, storing result in A 
							//*                    (Workspace: need M*M) 
							//* 
							
							_5nsxi69c("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,_xdbczr8u ,ref _h4ibbatv ,ref Unsafe.AsRef(_d0547bi2) ,_vxfgpup9 ,ref _ocv8fk5c );//* 
							//*                    Copy right singular vectors of A from A to VT 
							//* 
							
							_hhtvj1kb("F" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );//* 
							
						}
						else
						{
							//* 
							//*                    Insufficient workspace for a fast algorithm 
							//* 
							
							_q1w15vsx = (int)1;
							_4b6rt45i = (_q1w15vsx + _ev4xhht5);//* 
							//*                    Compute A=L*Q, copying result to VT 
							//*                    (Workspace: need 2*M, prefer M + M*NB) 
							//* 
							
							_bxcqf0ji(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_hhtvj1kb("U" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );//* 
							//*                    Generate Q in VT 
							//*                    (Workspace: need M + N, prefer M + N*NB) 
							//* 
							
							_n6025boa(ref _dxpq0xkr ,ref _dxpq0xkr ,ref _ev4xhht5 ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_smxeww0r = _q1w15vsx;
							_wx1x93f0 = (_smxeww0r + _ev4xhht5);
							_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
							_4b6rt45i = (_5ke1jwwr + _ev4xhht5);//* 
							//*                    Zero out above L in A 
							//* 
							
							_rta9tuwm("U" ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_vxfgpup9+((int)1 - 1) + ((int)2 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );//* 
							//*                    Bidiagonalize L in A 
							//*                    (Workspace: need 4*M, prefer 3*M + 2*M*NB) 
							//* 
							
							_sf2bwwb1(ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Multiply right bidiagonalizing vectors in A by Q 
							//*                    in VT 
							//*                    (Workspace: need 3*M + N, prefer 3*M + N*NB) 
							//* 
							
							_pwi7fryj("P" ,"L" ,"T" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_4b6rt45i = (_smxeww0r + _ev4xhht5);//* 
							//*                    Perform bidiagonal QR iteration, computing right 
							//*                    singular vectors of A in VT 
							//*                    (Workspace: need BDSPAC) 
							//* 
							
							_nvdqer79("U" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_xdbczr8u ,ref _h4ibbatv ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
							
						}
						//* 
						
					}
					else
					if (_utderuk7)
					{
						//* 
						//*                 Path 8t(N much larger than M, JOBU='O', JOBVT='A') 
						//*                 N right singular vectors to be computed in VT and 
						//*                 M left singular vectors to be overwritten on A 
						//* 
						
						if (_6fnxzlyp >= ((((int)2 * _ev4xhht5) * _ev4xhht5) + ILNumerics.F2NET.Intrinsics.MAX(_dxpq0xkr + _ev4xhht5 ,(int)4 * _ev4xhht5 ,_24c48sks )))
						{
							//* 
							//*                    Sufficient workspace for a fast algorithm 
							//* 
							
							_j4l29b9c = (int)1;
							if (_6fnxzlyp >= (_loujht2t + (((int)2 * _ocv8fk5c) * _ev4xhht5)))
							{
								//* 
								//*                       WORK(IU) is LDA by M and WORK(IR) is LDA by M 
								//* 
								
								_lbvz22dx = _ocv8fk5c;
								_m9w6lk7x = (_j4l29b9c + (_lbvz22dx * _ev4xhht5));
								_v6sbkzy4 = _ocv8fk5c;
							}
							else
							if (_6fnxzlyp >= (_loujht2t + ((_ocv8fk5c + _ev4xhht5) * _ev4xhht5)))
							{
								//* 
								//*                       WORK(IU) is LDA by M and WORK(IR) is M by M 
								//* 
								
								_lbvz22dx = _ocv8fk5c;
								_m9w6lk7x = (_j4l29b9c + (_lbvz22dx * _ev4xhht5));
								_v6sbkzy4 = _ev4xhht5;
							}
							else
							{
								//* 
								//*                       WORK(IU) is M by M and WORK(IR) is M by M 
								//* 
								
								_lbvz22dx = _ev4xhht5;
								_m9w6lk7x = (_j4l29b9c + (_lbvz22dx * _ev4xhht5));
								_v6sbkzy4 = _ev4xhht5;
							}
							
							_q1w15vsx = (_m9w6lk7x + (_v6sbkzy4 * _ev4xhht5));
							_4b6rt45i = (_q1w15vsx + _ev4xhht5);//* 
							//*                    Compute A=L*Q, copying result to VT 
							//*                    (Workspace: need 2*M*M + 2*M, prefer 2*M*M + M + M*NB) 
							//* 
							
							_bxcqf0ji(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_hhtvj1kb("U" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );//* 
							//*                    Generate Q in VT 
							//*                    (Workspace: need 2*M*M + M + N, prefer 2*M*M + M + N*NB) 
							//* 
							
							_n6025boa(ref _dxpq0xkr ,ref _dxpq0xkr ,ref _ev4xhht5 ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Copy L to WORK(IU), zeroing out above it 
							//* 
							
							_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx );
							_rta9tuwm("U" ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_j4l29b9c + _lbvz22dx - 1)),ref _lbvz22dx );
							_smxeww0r = _q1w15vsx;
							_wx1x93f0 = (_smxeww0r + _ev4xhht5);
							_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
							_4b6rt45i = (_5ke1jwwr + _ev4xhht5);//* 
							//*                    Bidiagonalize L in WORK(IU), copying result to 
							//*                    WORK(IR) 
							//*                    (Workspace: need 2*M*M + 4*M, 
							//*                                prefer 2*M*M+3*M+2*M*NB) 
							//* 
							
							_sf2bwwb1(ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 );//* 
							//*                    Generate right bidiagonalizing vectors in WORK(IU) 
							//*                    (Workspace: need 2*M*M + 4*M-1, 
							//*                                prefer 2*M*M+3*M+(M-1)*NB) 
							//* 
							
							_cmc4j0e3("P" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Generate left bidiagonalizing vectors in WORK(IR) 
							//*                    (Workspace: need 2*M*M + 4*M, prefer 2*M*M + 3*M + M*NB) 
							//* 
							
							_cmc4j0e3("Q" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_4b6rt45i = (_smxeww0r + _ev4xhht5);//* 
							//*                    Perform bidiagonal QR iteration, computing left 
							//*                    singular vectors of L in WORK(IR) and computing 
							//*                    right singular vectors of L in WORK(IU) 
							//*                    (Workspace: need 2*M*M + BDSPAC) 
							//* 
							
							_nvdqer79("U" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
							//*                    Multiply right singular vectors of L in WORK(IU) by 
							//*                    Q in VT, storing result in A 
							//*                    (Workspace: need M*M) 
							//* 
							
							_5nsxi69c("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,_xdbczr8u ,ref _h4ibbatv ,ref Unsafe.AsRef(_d0547bi2) ,_vxfgpup9 ,ref _ocv8fk5c );//* 
							//*                    Copy right singular vectors of A from A to VT 
							//* 
							
							_hhtvj1kb("F" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );//* 
							//*                    Copy left singular vectors of A from WORK(IR) to A 
							//* 
							
							_hhtvj1kb("F" ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_m9w6lk7x - 1)),ref _v6sbkzy4 ,_vxfgpup9 ,ref _ocv8fk5c );//* 
							
						}
						else
						{
							//* 
							//*                    Insufficient workspace for a fast algorithm 
							//* 
							
							_q1w15vsx = (int)1;
							_4b6rt45i = (_q1w15vsx + _ev4xhht5);//* 
							//*                    Compute A=L*Q, copying result to VT 
							//*                    (Workspace: need 2*M, prefer M + M*NB) 
							//* 
							
							_bxcqf0ji(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_hhtvj1kb("U" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );//* 
							//*                    Generate Q in VT 
							//*                    (Workspace: need M + N, prefer M + N*NB) 
							//* 
							
							_n6025boa(ref _dxpq0xkr ,ref _dxpq0xkr ,ref _ev4xhht5 ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_smxeww0r = _q1w15vsx;
							_wx1x93f0 = (_smxeww0r + _ev4xhht5);
							_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
							_4b6rt45i = (_5ke1jwwr + _ev4xhht5);//* 
							//*                    Zero out above L in A 
							//* 
							
							_rta9tuwm("U" ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_vxfgpup9+((int)1 - 1) + ((int)2 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );//* 
							//*                    Bidiagonalize L in A 
							//*                    (Workspace: need 4*M, prefer 3*M + 2*M*NB) 
							//* 
							
							_sf2bwwb1(ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Multiply right bidiagonalizing vectors in A by Q 
							//*                    in VT 
							//*                    (Workspace: need 3*M + N, prefer 3*M + N*NB) 
							//* 
							
							_pwi7fryj("P" ,"L" ,"T" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Generate left bidiagonalizing vectors in A 
							//*                    (Workspace: need 4*M, prefer 3*M + M*NB) 
							//* 
							
							_cmc4j0e3("Q" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_4b6rt45i = (_smxeww0r + _ev4xhht5);//* 
							//*                    Perform bidiagonal QR iteration, computing left 
							//*                    singular vectors of A in A and computing right 
							//*                    singular vectors of A in VT 
							//*                    (Workspace: need BDSPAC) 
							//* 
							
							_nvdqer79("U" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_xdbczr8u ,ref _h4ibbatv ,_vxfgpup9 ,ref _ocv8fk5c ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
							
						}
						//* 
						
					}
					else
					if (_96h5p04b)
					{
						//* 
						//*                 Path 9t(N much larger than M, JOBU='S' or 'A', 
						//*                         JOBVT='A') 
						//*                 N right singular vectors to be computed in VT and 
						//*                 M left singular vectors to be computed in U 
						//* 
						
						if (_6fnxzlyp >= ((_ev4xhht5 * _ev4xhht5) + ILNumerics.F2NET.Intrinsics.MAX(_dxpq0xkr + _ev4xhht5 ,(int)4 * _ev4xhht5 ,_24c48sks )))
						{
							//* 
							//*                    Sufficient workspace for a fast algorithm 
							//* 
							
							_j4l29b9c = (int)1;
							if (_6fnxzlyp >= (_loujht2t + (_ocv8fk5c * _ev4xhht5)))
							{
								//* 
								//*                       WORK(IU) is LDA by M 
								//* 
								
								_lbvz22dx = _ocv8fk5c;
							}
							else
							{
								//* 
								//*                       WORK(IU) is M by M 
								//* 
								
								_lbvz22dx = _ev4xhht5;
							}
							
							_q1w15vsx = (_j4l29b9c + (_lbvz22dx * _ev4xhht5));
							_4b6rt45i = (_q1w15vsx + _ev4xhht5);//* 
							//*                    Compute A=L*Q, copying result to VT 
							//*                    (Workspace: need M*M + 2*M, prefer M*M + M + M*NB) 
							//* 
							
							_bxcqf0ji(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_hhtvj1kb("U" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );//* 
							//*                    Generate Q in VT 
							//*                    (Workspace: need M*M + M + N, prefer M*M + M + N*NB) 
							//* 
							
							_n6025boa(ref _dxpq0xkr ,ref _dxpq0xkr ,ref _ev4xhht5 ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Copy L to WORK(IU), zeroing out above it 
							//* 
							
							_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx );
							_rta9tuwm("U" ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+(_j4l29b9c + _lbvz22dx - 1)),ref _lbvz22dx );
							_smxeww0r = _q1w15vsx;
							_wx1x93f0 = (_smxeww0r + _ev4xhht5);
							_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
							_4b6rt45i = (_5ke1jwwr + _ev4xhht5);//* 
							//*                    Bidiagonalize L in WORK(IU), copying result to U 
							//*                    (Workspace: need M*M + 4*M, prefer M*M + 3*M + 2*M*NB) 
							//* 
							
							_sf2bwwb1(ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,_7u55mqkq ,ref _u6e6d39b );//* 
							//*                    Generate right bidiagonalizing vectors in WORK(IU) 
							//*                    (Workspace: need M*M + 4*M, prefer M*M + 3*M + (M-1)*NB) 
							//* 
							
							_cmc4j0e3("P" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Generate left bidiagonalizing vectors in U 
							//*                    (Workspace: need M*M + 4*M, prefer M*M + 3*M + M*NB) 
							//* 
							
							_cmc4j0e3("Q" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_4b6rt45i = (_smxeww0r + _ev4xhht5);//* 
							//*                    Perform bidiagonal QR iteration, computing left 
							//*                    singular vectors of L in U and computing right 
							//*                    singular vectors of L in WORK(IU) 
							//*                    (Workspace: need M*M + BDSPAC) 
							//* 
							
							_nvdqer79("U" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,_7u55mqkq ,ref _u6e6d39b ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
							//*                    Multiply right singular vectors of L in WORK(IU) by 
							//*                    Q in VT, storing result in A 
							//*                    (Workspace: need M*M) 
							//* 
							
							_5nsxi69c("N" ,"N" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+(_j4l29b9c - 1)),ref _lbvz22dx ,_xdbczr8u ,ref _h4ibbatv ,ref Unsafe.AsRef(_d0547bi2) ,_vxfgpup9 ,ref _ocv8fk5c );//* 
							//*                    Copy right singular vectors of A from A to VT 
							//* 
							
							_hhtvj1kb("F" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );//* 
							
						}
						else
						{
							//* 
							//*                    Insufficient workspace for a fast algorithm 
							//* 
							
							_q1w15vsx = (int)1;
							_4b6rt45i = (_q1w15vsx + _ev4xhht5);//* 
							//*                    Compute A=L*Q, copying result to VT 
							//*                    (Workspace: need 2*M, prefer M + M*NB) 
							//* 
							
							_bxcqf0ji(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_hhtvj1kb("U" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );//* 
							//*                    Generate Q in VT 
							//*                    (Workspace: need M + N, prefer M + N*NB) 
							//* 
							
							_n6025boa(ref _dxpq0xkr ,ref _dxpq0xkr ,ref _ev4xhht5 ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Copy L to U, zeroing out above it 
							//* 
							
							_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );
							_rta9tuwm("U" ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_7u55mqkq+((int)1 - 1) + ((int)2 - 1) * 1 * (_u6e6d39b)),ref _u6e6d39b );
							_smxeww0r = _q1w15vsx;
							_wx1x93f0 = (_smxeww0r + _ev4xhht5);
							_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
							_4b6rt45i = (_5ke1jwwr + _ev4xhht5);//* 
							//*                    Bidiagonalize L in U 
							//*                    (Workspace: need 4*M, prefer 3*M + 2*M*NB) 
							//* 
							
							_sf2bwwb1(ref _ev4xhht5 ,ref _ev4xhht5 ,_7u55mqkq ,ref _u6e6d39b ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Multiply right bidiagonalizing vectors in U by Q 
							//*                    in VT 
							//*                    (Workspace: need 3*M + N, prefer 3*M + N*NB) 
							//* 
							
							_pwi7fryj("P" ,"L" ,"T" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_5ke1jwwr - 1)),_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );//* 
							//*                    Generate left bidiagonalizing vectors in U 
							//*                    (Workspace: need 4*M, prefer 3*M + M*NB) 
							//* 
							
							_cmc4j0e3("Q" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _ev4xhht5 ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
							_4b6rt45i = (_smxeww0r + _ev4xhht5);//* 
							//*                    Perform bidiagonal QR iteration, computing left 
							//*                    singular vectors of A in U and computing right 
							//*                    singular vectors of A in VT 
							//*                    (Workspace: need BDSPAC) 
							//* 
							
							_nvdqer79("U" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_xdbczr8u ,ref _h4ibbatv ,_7u55mqkq ,ref _u6e6d39b ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );//* 
							
						}
						//* 
						
					}
					//* 
					
				}
				//* 
				
			}
			else
			{
				//* 
				//*           N .LT. MNTHR 
				//* 
				//*           Path 10t(N greater than M, but not much larger) 
				//*           Reduce to bidiagonal form without LQ decomposition 
				//* 
				
				_smxeww0r = (int)1;
				_wx1x93f0 = (_smxeww0r + _ev4xhht5);
				_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
				_4b6rt45i = (_5ke1jwwr + _ev4xhht5);//* 
				//*           Bidiagonalize A 
				//*           (Workspace: need 3*M + N, prefer 3*M + (M + N)*NB) 
				//* 
				
				_sf2bwwb1(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
				if (_96h5p04b)
				{
					//* 
					//*              If left singular vectors desired in U, copy result to U 
					//*              and generate left bidiagonalizing vectors in U 
					//*              (Workspace: need 4*M-1, prefer 3*M + (M-1)*NB) 
					//* 
					
					_hhtvj1kb("L" ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b );
					_cmc4j0e3("Q" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
				}
				
				if (_05xnu7gm)
				{
					//* 
					//*              If right singular vectors desired in VT, copy result to 
					//*              VT and generate right bidiagonalizing vectors in VT 
					//*              (Workspace: need 3*M + NRVT, prefer 3*M + NRVT*NB) 
					//* 
					
					_hhtvj1kb("U" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_xdbczr8u ,ref _h4ibbatv );
					if (_njekud0j)
					_c4gifwtb = _dxpq0xkr;
					if (_8pgl0qdq)
					_c4gifwtb = _ev4xhht5;
					_cmc4j0e3("P" ,ref _c4gifwtb ,ref _dxpq0xkr ,ref _ev4xhht5 ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
				}
				
				if (_utderuk7)
				{
					//* 
					//*              If left singular vectors desired in A, generate left 
					//*              bidiagonalizing vectors in A 
					//*              (Workspace: need 4*M-1, prefer 3*M + (M-1)*NB) 
					//* 
					
					_cmc4j0e3("Q" ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
				}
				
				if (_wkqhtihq)
				{
					//* 
					//*              If right singular vectors desired in A, generate right 
					//*              bidiagonalizing vectors in A 
					//*              (Workspace: need 4*M, prefer 3*M + M*NB) 
					//* 
					
					_cmc4j0e3("P" ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_4b6rt45i - 1)),ref Unsafe.AsRef((_6fnxzlyp - _4b6rt45i) + (int)1) ,ref _bhsiylw4 );
				}
				
				_4b6rt45i = (_smxeww0r + _ev4xhht5);
				if (_96h5p04b | _utderuk7)
				_tg62ial7 = _ev4xhht5;
				if (_s7btv50y)
				_tg62ial7 = (int)0;
				if (_05xnu7gm | _wkqhtihq)
				_0v6rtqiq = _dxpq0xkr;
				if (_ov7mcmwp)
				_0v6rtqiq = (int)0;
				if ((!(_utderuk7)) & (!(_wkqhtihq)))
				{
					//* 
					//*              Perform bidiagonal QR iteration, if desired, computing 
					//*              left singular vectors in U and computing right singular 
					//*              vectors in VT 
					//*              (Workspace: need BDSPAC) 
					//* 
					
					_nvdqer79("L" ,ref _ev4xhht5 ,ref _0v6rtqiq ,ref _tg62ial7 ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_xdbczr8u ,ref _h4ibbatv ,_7u55mqkq ,ref _u6e6d39b ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );
				}
				else
				if ((!(_utderuk7)) & _wkqhtihq)
				{
					//* 
					//*              Perform bidiagonal QR iteration, if desired, computing 
					//*              left singular vectors in U and computing right singular 
					//*              vectors in A 
					//*              (Workspace: need BDSPAC) 
					//* 
					
					_nvdqer79("L" ,ref _ev4xhht5 ,ref _0v6rtqiq ,ref _tg62ial7 ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_vxfgpup9 ,ref _ocv8fk5c ,_7u55mqkq ,ref _u6e6d39b ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );
				}
				else
				{
					//* 
					//*              Perform bidiagonal QR iteration, if desired, computing 
					//*              left singular vectors in A and computing right singular 
					//*              vectors in VT 
					//*              (Workspace: need BDSPAC) 
					//* 
					
					_nvdqer79("L" ,ref _ev4xhht5 ,ref _0v6rtqiq ,ref _tg62ial7 ,ref Unsafe.AsRef((int)0) ,_irk8i6qr ,(_apig8meb+(_smxeww0r - 1)),_xdbczr8u ,ref _h4ibbatv ,_vxfgpup9 ,ref _ocv8fk5c ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,(_apig8meb+(_4b6rt45i - 1)),ref _gro5yvfo );
				}
				//* 
				
			}
			//* 
			
		}
		//* 
		//*     If DBDSQR failed to converge, copy unconverged superdiagonals 
		//*     to WORK( 2:MINMN ) 
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			if (_smxeww0r > (int)2)
			{
				
				{
					System.Int32 __81fgg2dlsvn1358 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1358 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1358;
					for (__81fgg2count1358 = System.Math.Max(0, (System.Int32)(((System.Int32)(_qaseb1y7 - (int)1) - __81fgg2dlsvn1358 + __81fgg2step1358) / __81fgg2step1358)), _b5p6od9s = __81fgg2dlsvn1358; __81fgg2count1358 != 0; __81fgg2count1358--, _b5p6od9s += (__81fgg2step1358)) {

					{
						
						*(_apig8meb+(_b5p6od9s + (int)1 - 1)) = *(_apig8meb+((_b5p6od9s + _smxeww0r) - (int)1 - 1));
Mark50:;
						// continue
					}
										}				}
			}
			
			if (_smxeww0r < (int)2)
			{
				
				{
					System.Int32 __81fgg2dlsvn1359 = (System.Int32)((_qaseb1y7 - (int)1));
					System.Int32 __81fgg2step1359 = (System.Int32)((int)-1);
					System.Int32 __81fgg2count1359;
					for (__81fgg2count1359 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1359 + __81fgg2step1359) / __81fgg2step1359)), _b5p6od9s = __81fgg2dlsvn1359; __81fgg2count1359 != 0; __81fgg2count1359--, _b5p6od9s += (__81fgg2step1359)) {

					{
						
						*(_apig8meb+(_b5p6od9s + (int)1 - 1)) = *(_apig8meb+((_b5p6od9s + _smxeww0r) - (int)1 - 1));
Mark60:;
						// continue
					}
										}				}
			}
			
		}
		//* 
		//*     Undo scaling if necessary 
		//* 
		
		if (_65mv5f5m == (int)1)
		{
			
			if (_j6vjow1g > _av7j8yda)
			_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _av7j8yda ,ref _j6vjow1g ,ref _qaseb1y7 ,ref Unsafe.AsRef((int)1) ,_irk8i6qr ,ref _qaseb1y7 ,ref _bhsiylw4 );
			if ((_gro5yvfo != (int)0) & (_j6vjow1g > _av7j8yda))
			_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _av7j8yda ,ref _j6vjow1g ,ref Unsafe.AsRef(_qaseb1y7 - (int)1) ,ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)2 - 1)),ref _qaseb1y7 ,ref _bhsiylw4 );
			if (_j6vjow1g < _bogm0gwy)
			_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _bogm0gwy ,ref _j6vjow1g ,ref _qaseb1y7 ,ref Unsafe.AsRef((int)1) ,_irk8i6qr ,ref _qaseb1y7 ,ref _bhsiylw4 );
			if ((_gro5yvfo != (int)0) & (_j6vjow1g < _bogm0gwy))
			_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _bogm0gwy ,ref _j6vjow1g ,ref Unsafe.AsRef(_qaseb1y7 - (int)1) ,ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)2 - 1)),ref _qaseb1y7 ,ref _bhsiylw4 );
		}
		//* 
		//*     Return optimal workspace in WORK(1) 
		//* 
		
		*(_apig8meb+((int)1 - 1)) = DBLE(_tafa1evd);//* 
		
		return;//* 
		//*     End of DGESVD 
		//* 
		
	}
	
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
