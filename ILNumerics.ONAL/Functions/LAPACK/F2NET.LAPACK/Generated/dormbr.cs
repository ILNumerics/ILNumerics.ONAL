
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
//*> \brief \b DORMBR 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DORMBR + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dormbr.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dormbr.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dormbr.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DORMBR( VECT, SIDE, TRANS, M, N, K, A, LDA, TAU, C, 
//*                          LDC, WORK, LWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          SIDE, TRANS, VECT 
//*       INTEGER            INFO, K, LDA, LDC, LWORK, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   A( LDA, * ), C( LDC, * ), TAU( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> If VECT = 'Q', DORMBR overwrites the general real M-by-N matrix C 
//*> with 
//*>                 SIDE = 'L'     SIDE = 'R' 
//*> TRANS = 'N':      Q * C          C * Q 
//*> TRANS = 'T':      Q**T * C       C * Q**T 
//*> 
//*> If VECT = 'P', DORMBR overwrites the general real M-by-N matrix C 
//*> with 
//*>                 SIDE = 'L'     SIDE = 'R' 
//*> TRANS = 'N':      P * C          C * P 
//*> TRANS = 'T':      P**T * C       C * P**T 
//*> 
//*> Here Q and P**T are the orthogonal matrices determined by DGEBRD when 
//*> reducing a real matrix A to bidiagonal form: A = Q * B * P**T. Q and 
//*> P**T are defined as products of elementary reflectors H(i) and G(i) 
//*> respectively. 
//*> 
//*> Let nq = m if SIDE = 'L' and nq = n if SIDE = 'R'. Thus nq is the 
//*> order of the orthogonal matrix Q or P**T that is applied. 
//*> 
//*> If VECT = 'Q', A is assumed to have been an NQ-by-K matrix: 
//*> if nq >= k, Q = H(1) H(2) . . . H(k); 
//*> if nq < k, Q = H(1) H(2) . . . H(nq-1). 
//*> 
//*> If VECT = 'P', A is assumed to have been a K-by-NQ matrix: 
//*> if k < nq, P = G(1) G(2) . . . G(k); 
//*> if k >= nq, P = G(1) G(2) . . . G(nq-1). 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] VECT 
//*> \verbatim 
//*>          VECT is CHARACTER*1 
//*>          = 'Q': apply Q or Q**T; 
//*>          = 'P': apply P or P**T. 
//*> \endverbatim 
//*> 
//*> \param[in] SIDE 
//*> \verbatim 
//*>          SIDE is CHARACTER*1 
//*>          = 'L': apply Q, Q**T, P or P**T from the Left; 
//*>          = 'R': apply Q, Q**T, P or P**T from the Right. 
//*> \endverbatim 
//*> 
//*> \param[in] TRANS 
//*> \verbatim 
//*>          TRANS is CHARACTER*1 
//*>          = 'N':  No transpose, apply Q  or P; 
//*>          = 'T':  Transpose, apply Q**T or P**T. 
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
//*> \param[in] K 
//*> \verbatim 
//*>          K is INTEGER 
//*>          If VECT = 'Q', the number of columns in the original 
//*>          matrix reduced by DGEBRD. 
//*>          If VECT = 'P', the number of rows in the original 
//*>          matrix reduced by DGEBRD. 
//*>          K >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is DOUBLE PRECISION array, dimension 
//*>                                (LDA,min(nq,K)) if VECT = 'Q' 
//*>                                (LDA,nq)        if VECT = 'P' 
//*>          The vectors which define the elementary reflectors H(i) and 
//*>          G(i), whose products determine the matrices Q and P, as 
//*>          returned by DGEBRD. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A. 
//*>          If VECT = 'Q', LDA >= max(1,nq); 
//*>          if VECT = 'P', LDA >= max(1,min(nq,K)). 
//*> \endverbatim 
//*> 
//*> \param[in] TAU 
//*> \verbatim 
//*>          TAU is DOUBLE PRECISION array, dimension (min(nq,K)) 
//*>          TAU(i) must contain the scalar factor of the elementary 
//*>          reflector H(i) or G(i) which determines Q or P, as returned 
//*>          by DGEBRD in the array argument TAUQ or TAUP. 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is DOUBLE PRECISION array, dimension (LDC,N) 
//*>          On entry, the M-by-N matrix C. 
//*>          On exit, C is overwritten by Q*C or Q**T*C or C*Q**T or C*Q 
//*>          or P*C or P**T*C or C*P or C*P**T. 
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
//*>          WORK is DOUBLE PRECISION array, dimension (MAX(1,LWORK)) 
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
//*> \ingroup doubleOTHERcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _pwi7fryj(FString _m2oo9cc0, FString _m2cn2gjg, FString _scuo79v4, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, Double* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _0446f4de, Double* _3crf0qn3, ref Int32 _1s3eymp4, Double* _apig8meb, ref Int32 _6fnxzlyp, ref Int32 _gro5yvfo)
	{
#region variable declarations
Boolean _tsriaea4 =  default;
Boolean _pvwxvshr =  default;
Boolean _lhlgm7z5 =  default;
Boolean _2bzw4gjb =  default;
FString _l09pt3ga =  new FString(1);
Int32 _egqdmelt =  default;
Int32 _8ur10vsh =  default;
Int32 _itfnbz60 =  default;
Int32 _e4ueamrn =  default;
Int32 _31eu052u =  default;
Int32 _f7059815 =  default;
Int32 _q8n03esx =  default;
Int32 _joervqa5 =  default;
Int32 _w6pmxgch =  default;
string fLanavab = default;
#endregion  variable declarations
_m2oo9cc0 = _m2oo9cc0.Convert(1);
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
		_tsriaea4 = _w8y2rzgy(_m2oo9cc0 ,"Q" );
		_pvwxvshr = _w8y2rzgy(_m2cn2gjg ,"L" );
		_2bzw4gjb = _w8y2rzgy(_scuo79v4 ,"N" );
		_lhlgm7z5 = (_6fnxzlyp == (int)-1);//* 
		//*     NQ is the order of Q or P and NW is the minimum dimension of WORK 
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
		
		if ((!(_tsriaea4)) & (!(_w8y2rzgy(_m2oo9cc0 ,"P" ))))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if ((!(_pvwxvshr)) & (!(_w8y2rzgy(_m2cn2gjg ,"R" ))))
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if ((!(_2bzw4gjb)) & (!(_w8y2rzgy(_scuo79v4 ,"T" ))))
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if (_ev4xhht5 < (int)0)
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-5;
		}
		else
		if (_umlkckdg < (int)0)
		{
			
			_gro5yvfo = (int)-6;
		}
		else
		if ((_tsriaea4 & (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_joervqa5 ))) | ((!(_tsriaea4)) & (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,ILNumerics.F2NET.Intrinsics.MIN(_joervqa5 ,_umlkckdg ) ))))
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
			
			if (_tsriaea4)
			{
				
				if (_pvwxvshr)
				{
					
					_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"DORMQR" ,_m2cn2gjg + _scuo79v4 ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref _dxpq0xkr ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef((int)-1) );
				}
				else
				{
					
					_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"DORMQR" ,_m2cn2gjg + _scuo79v4 ,ref _ev4xhht5 ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef((int)-1) );
				}
				
			}
			else
			{
				
				if (_pvwxvshr)
				{
					
					_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"DORMLQ" ,_m2cn2gjg + _scuo79v4 ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref _dxpq0xkr ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef((int)-1) );
				}
				else
				{
					
					_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"DORMLQ" ,_m2cn2gjg + _scuo79v4 ,ref _ev4xhht5 ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef((int)-1) );
				}
				
			}
			
			_e4ueamrn = (ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_w6pmxgch ) * _f7059815);
			*(_apig8meb+((int)1 - 1)) = DBLE(_e4ueamrn);
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("DORMBR" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
		
		*(_apig8meb+((int)1 - 1)) = DBLE((int)1);
		if ((_ev4xhht5 == (int)0) | (_dxpq0xkr == (int)0))
		return;//* 
		
		if (_tsriaea4)
		{
			//* 
			//*        Apply Q 
			//* 
			
			if (_joervqa5 >= _umlkckdg)
			{
				//* 
				//*           Q was determined by a call to DGEBRD with nq >= k 
				//* 
				
				_3a5e5srq(_m2cn2gjg ,_scuo79v4 ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _umlkckdg ,_vxfgpup9 ,ref _ocv8fk5c ,_0446f4de ,_3crf0qn3 ,ref _1s3eymp4 ,_apig8meb ,ref _6fnxzlyp ,ref _itfnbz60 );
			}
			else
			if (_joervqa5 > (int)1)
			{
				//* 
				//*           Q was determined by a call to DGEBRD with nq < k 
				//* 
				
				if (_pvwxvshr)
				{
					
					_31eu052u = (_ev4xhht5 - (int)1);
					_q8n03esx = _dxpq0xkr;
					_egqdmelt = (int)2;
					_8ur10vsh = (int)1;
				}
				else
				{
					
					_31eu052u = _ev4xhht5;
					_q8n03esx = (_dxpq0xkr - (int)1);
					_egqdmelt = (int)1;
					_8ur10vsh = (int)2;
				}
				
				_3a5e5srq(_m2cn2gjg ,_scuo79v4 ,ref _31eu052u ,ref _q8n03esx ,ref Unsafe.AsRef(_joervqa5 - (int)1) ,(_vxfgpup9+((int)2 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_0446f4de ,(_3crf0qn3+(_egqdmelt - 1) + (_8ur10vsh - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,_apig8meb ,ref _6fnxzlyp ,ref _itfnbz60 );
			}
			
		}
		else
		{
			//* 
			//*        Apply P 
			//* 
			
			if (_2bzw4gjb)
			{
				
				
				_l09pt3ga = "T";
			}
			else
			{
				
				
				_l09pt3ga = "N";
			}
			
			if (_joervqa5 > _umlkckdg)
			{
				//* 
				//*           P was determined by a call to DGEBRD with nq > k 
				//* 
				
				_ev8pql99(_m2cn2gjg ,_l09pt3ga ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _umlkckdg ,_vxfgpup9 ,ref _ocv8fk5c ,_0446f4de ,_3crf0qn3 ,ref _1s3eymp4 ,_apig8meb ,ref _6fnxzlyp ,ref _itfnbz60 );
			}
			else
			if (_joervqa5 > (int)1)
			{
				//* 
				//*           P was determined by a call to DGEBRD with nq <= k 
				//* 
				
				if (_pvwxvshr)
				{
					
					_31eu052u = (_ev4xhht5 - (int)1);
					_q8n03esx = _dxpq0xkr;
					_egqdmelt = (int)2;
					_8ur10vsh = (int)1;
				}
				else
				{
					
					_31eu052u = _ev4xhht5;
					_q8n03esx = (_dxpq0xkr - (int)1);
					_egqdmelt = (int)1;
					_8ur10vsh = (int)2;
				}
				
				_ev8pql99(_m2cn2gjg ,_l09pt3ga ,ref _31eu052u ,ref _q8n03esx ,ref Unsafe.AsRef(_joervqa5 - (int)1) ,(_vxfgpup9+((int)1 - 1) + ((int)2 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_0446f4de ,(_3crf0qn3+(_egqdmelt - 1) + (_8ur10vsh - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,_apig8meb ,ref _6fnxzlyp ,ref _itfnbz60 );
			}
			
		}
		
		*(_apig8meb+((int)1 - 1)) = DBLE(_e4ueamrn);
		return;//* 
		//*     End of DORMBR 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
