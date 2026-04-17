
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
//*> \brief \b ZUNMHR 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZUNMHR + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zunmhr.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zunmhr.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zunmhr.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZUNMHR( SIDE, TRANS, M, N, ILO, IHI, A, LDA, TAU, C, 
//*                          LDC, WORK, LWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          SIDE, TRANS 
//*       INTEGER            IHI, ILO, INFO, LDA, LDC, LWORK, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX*16         A( LDA, * ), C( LDC, * ), TAU( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZUNMHR overwrites the general complex M-by-N matrix C with 
//*> 
//*>                 SIDE = 'L'     SIDE = 'R' 
//*> TRANS = 'N':      Q * C          C * Q 
//*> TRANS = 'C':      Q**H * C       C * Q**H 
//*> 
//*> where Q is a complex unitary matrix of order nq, with nq = m if 
//*> SIDE = 'L' and nq = n if SIDE = 'R'. Q is defined as the product of 
//*> IHI-ILO elementary reflectors, as returned by ZGEHRD: 
//*> 
//*> Q = H(ilo) H(ilo+1) . . . H(ihi-1). 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] SIDE 
//*> \verbatim 
//*>          SIDE is CHARACTER*1 
//*>          = 'L': apply Q or Q**H from the Left; 
//*>          = 'R': apply Q or Q**H from the Right. 
//*> \endverbatim 
//*> 
//*> \param[in] TRANS 
//*> \verbatim 
//*>          TRANS is CHARACTER*1 
//*>          = 'N': apply Q  (No transpose) 
//*>          = 'C': apply Q**H (Conjugate transpose) 
//*> \endverbatim 
//*> 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of rows of the matrix C. M >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of columns of the matrix C. N >= 0. 
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
//*> 
//*>          ILO and IHI must have the same values as in the previous call 
//*>          of ZGEHRD. Q is equal to the unit matrix except in the 
//*>          submatrix Q(ilo+1:ihi,ilo+1:ihi). 
//*>          If SIDE = 'L', then 1 <= ILO <= IHI <= M, if M > 0, and 
//*>          ILO = 1 and IHI = 0, if M = 0; 
//*>          if SIDE = 'R', then 1 <= ILO <= IHI <= N, if N > 0, and 
//*>          ILO = 1 and IHI = 0, if N = 0. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension 
//*>                               (LDA,M) if SIDE = 'L' 
//*>                               (LDA,N) if SIDE = 'R' 
//*>          The vectors which define the elementary reflectors, as 
//*>          returned by ZGEHRD. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A. 
//*>          LDA >= max(1,M) if SIDE = 'L'; LDA >= max(1,N) if SIDE = 'R'. 
//*> \endverbatim 
//*> 
//*> \param[in] TAU 
//*> \verbatim 
//*>          TAU is COMPLEX*16 array, dimension 
//*>                               (M-1) if SIDE = 'L' 
//*>                               (N-1) if SIDE = 'R' 
//*>          TAU(i) must contain the scalar factor of the elementary 
//*>          reflector H(i), as returned by ZGEHRD. 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is COMPLEX*16 array, dimension (LDC,N) 
//*>          On entry, the M-by-N matrix C. 
//*>          On exit, C is overwritten by Q*C or Q**H*C or C*Q**H or C*Q. 
//*> \endverbatim 
//*> 
//*> \param[in] LDC 
//*> \verbatim 
//*>          LDC is INTEGER 
//*>          The leading dimension of the array C. LDC >= max(1,M). 
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
//*>          The dimension of the array WORK. 
//*>          If SIDE = 'L', LWORK >= max(1,N); 
//*>          if SIDE = 'R', LWORK >= max(1,M). 
//*>          For optimum performance LWORK >= N*NB if SIDE = 'L', and 
//*>          LWORK >= M*NB if SIDE = 'R', where NB is the optimal 
//*>          blocksize. 
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
//*>          = 0:  successful exit 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value 
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
//*> \ingroup complex16OTHERcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _ml2zcgc8(FString _m2cn2gjg, FString _scuo79v4, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _pew3blan, ref Int32 _9c1csucx, complex* _vxfgpup9, ref Int32 _ocv8fk5c, complex* _0446f4de, complex* _3crf0qn3, ref Int32 _1s3eymp4, complex* _apig8meb, ref Int32 _6fnxzlyp, ref Int32 _gro5yvfo)
	{
#region variable declarations
Boolean _pvwxvshr =  default;
Boolean _lhlgm7z5 =  default;
Int32 _egqdmelt =  default;
Int32 _8ur10vsh =  default;
Int32 _itfnbz60 =  default;
Int32 _e4ueamrn =  default;
Int32 _31eu052u =  default;
Int32 _f7059815 =  default;
Int32 _aym8a085 =  default;
Int32 _q8n03esx =  default;
Int32 _joervqa5 =  default;
Int32 _w6pmxgch =  default;
string fLanavab = default;
#endregion  variable declarations
_m2cn2gjg = _m2cn2gjg.Convert(1);
_scuo79v4 = _scuo79v4.Convert(1);

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
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input arguments 
		//* 
		
		_gro5yvfo = (int)0;
		_aym8a085 = (_9c1csucx - _pew3blan);
		_pvwxvshr = _w8y2rzgy(_m2cn2gjg ,"L" );
		_lhlgm7z5 = (_6fnxzlyp == (int)-1);//* 
		//*     NQ is the order of Q and NW is the minimum dimension of WORK 
		//* 
		
		if (_pvwxvshr)
		{
			
			_joervqa5 = _ev4xhht5;
			_w6pmxgch = _dxpq0xkr;
		}
		else
		{
			
			_joervqa5 = _dxpq0xkr;
			_w6pmxgch = _ev4xhht5;
		}
		
		if ((!(_pvwxvshr)) & (!(_w8y2rzgy(_m2cn2gjg ,"R" ))))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if ((!(_w8y2rzgy(_scuo79v4 ,"N" ))) & (!(_w8y2rzgy(_scuo79v4 ,"C" ))))
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
		if ((_pew3blan < (int)1) | (_pew3blan > ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_joervqa5 )))
		{
			
			_gro5yvfo = (int)-5;
		}
		else
		if ((_9c1csucx < ILNumerics.F2NET.Intrinsics.MIN(_pew3blan ,_joervqa5 )) | (_9c1csucx > _joervqa5))
		{
			
			_gro5yvfo = (int)-6;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_joervqa5 ))
		{
			
			_gro5yvfo = (int)-8;
		}
		else
		if (_1s3eymp4 < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ))
		{
			
			_gro5yvfo = (int)-11;
		}
		else
		if ((_6fnxzlyp < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_w6pmxgch )) & (!(_lhlgm7z5)))
		{
			
			_gro5yvfo = (int)-13;
		}
		//* 
		
		if (_gro5yvfo == (int)0)
		{
			
			if (_pvwxvshr)
			{
				
				_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZUNMQR" ,_m2cn2gjg + _scuo79v4 ,ref _aym8a085 ,ref _dxpq0xkr ,ref _aym8a085 ,ref Unsafe.AsRef((int)-1) );
			}
			else
			{
				
				_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZUNMQR" ,_m2cn2gjg + _scuo79v4 ,ref _ev4xhht5 ,ref _aym8a085 ,ref _aym8a085 ,ref Unsafe.AsRef((int)-1) );
			}
			
			_e4ueamrn = (ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_w6pmxgch ) * _f7059815);
			*(_apig8meb+((int)1 - 1)) = DCMPLX(_e4ueamrn);
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("ZUNMHR" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
		
		if (((_ev4xhht5 == (int)0) | (_dxpq0xkr == (int)0)) | (_aym8a085 == (int)0))
		{
			
			*(_apig8meb+((int)1 - 1)) = DCMPLX((int)1);
			return;
		}
		//* 
		
		if (_pvwxvshr)
		{
			
			_31eu052u = _aym8a085;
			_q8n03esx = _dxpq0xkr;
			_egqdmelt = (_pew3blan + (int)1);
			_8ur10vsh = (int)1;
		}
		else
		{
			
			_31eu052u = _ev4xhht5;
			_q8n03esx = _aym8a085;
			_egqdmelt = (int)1;
			_8ur10vsh = (_pew3blan + (int)1);
		}
		//* 
		
		_1gd1avkg(_m2cn2gjg ,_scuo79v4 ,ref _31eu052u ,ref _q8n03esx ,ref _aym8a085 ,(_vxfgpup9+(_pew3blan + (int)1 - 1) + (_pew3blan - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_0446f4de+(_pew3blan - 1)),(_3crf0qn3+(_egqdmelt - 1) + (_8ur10vsh - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,_apig8meb ,ref _6fnxzlyp ,ref _itfnbz60 );//* 
		
		*(_apig8meb+((int)1 - 1)) = DCMPLX(_e4ueamrn);
		return;//* 
		//*     End of ZUNMHR 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
