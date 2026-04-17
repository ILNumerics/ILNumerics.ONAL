
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
//*> \brief \b ZUNGBR 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZUNGBR + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zungbr.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zungbr.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zungbr.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZUNGBR( VECT, M, N, K, A, LDA, TAU, WORK, LWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          VECT 
//*       INTEGER            INFO, K, LDA, LWORK, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX*16         A( LDA, * ), TAU( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZUNGBR generates one of the complex unitary matrices Q or P**H 
//*> determined by ZGEBRD when reducing a complex matrix A to bidiagonal 
//*> form: A = Q * B * P**H.  Q and P**H are defined as products of 
//*> elementary reflectors H(i) or G(i) respectively. 
//*> 
//*> If VECT = 'Q', A is assumed to have been an M-by-K matrix, and Q 
//*> is of order M: 
//*> if m >= k, Q = H(1) H(2) . . . H(k) and ZUNGBR returns the first n 
//*> columns of Q, where m >= n >= k; 
//*> if m < k, Q = H(1) H(2) . . . H(m-1) and ZUNGBR returns Q as an 
//*> M-by-M matrix. 
//*> 
//*> If VECT = 'P', A is assumed to have been a K-by-N matrix, and P**H 
//*> is of order N: 
//*> if k < n, P**H = G(k) . . . G(2) G(1) and ZUNGBR returns the first m 
//*> rows of P**H, where n >= m >= k; 
//*> if k >= n, P**H = G(n-1) . . . G(2) G(1) and ZUNGBR returns P**H as 
//*> an N-by-N matrix. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] VECT 
//*> \verbatim 
//*>          VECT is CHARACTER*1 
//*>          Specifies whether the matrix Q or the matrix P**H is 
//*>          required, as defined in the transformation applied by ZGEBRD: 
//*>          = 'Q':  generate Q; 
//*>          = 'P':  generate P**H. 
//*> \endverbatim 
//*> 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of rows of the matrix Q or P**H to be returned. 
//*>          M >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of columns of the matrix Q or P**H to be returned. 
//*>          N >= 0. 
//*>          If VECT = 'Q', M >= N >= min(M,K); 
//*>          if VECT = 'P', N >= M >= min(N,K). 
//*> \endverbatim 
//*> 
//*> \param[in] K 
//*> \verbatim 
//*>          K is INTEGER 
//*>          If VECT = 'Q', the number of columns in the original M-by-K 
//*>          matrix reduced by ZGEBRD. 
//*>          If VECT = 'P', the number of rows in the original K-by-N 
//*>          matrix reduced by ZGEBRD. 
//*>          K >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension (LDA,N) 
//*>          On entry, the vectors which define the elementary reflectors, 
//*>          as returned by ZGEBRD. 
//*>          On exit, the M-by-N matrix Q or P**H. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A. LDA >= M. 
//*> \endverbatim 
//*> 
//*> \param[in] TAU 
//*> \verbatim 
//*>          TAU is COMPLEX*16 array, dimension 
//*>                                (min(M,K)) if VECT = 'Q' 
//*>                                (min(N,K)) if VECT = 'P' 
//*>          TAU(i) must contain the scalar factor of the elementary 
//*>          reflector H(i) or G(i), which determines Q or P**H, as 
//*>          returned by ZGEBRD in its array argument TAUQ or TAUP. 
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
//*>          The dimension of the array WORK. LWORK >= max(1,min(M,N)). 
//*>          For optimum performance LWORK >= min(M,N)*NB, where NB 
//*>          is the optimal blocksize. 
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
//*> \date April 2012 
//* 
//*> \ingroup complex16GBcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _anznwuhr(FString _m2oo9cc0, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, complex* _vxfgpup9, ref Int32 _ocv8fk5c, complex* _0446f4de, complex* _apig8meb, ref Int32 _6fnxzlyp, ref Int32 _gro5yvfo)
	{
#region variable declarations
complex _d0547bi2 =   new fcomplex(0f,0f);
complex _kxg5drh2 =   new fcomplex(1f,0f);
Boolean _lhlgm7z5 =  default;
Boolean _gh3pzgwj =  default;
Int32 _b5p6od9s =  default;
Int32 _itfnbz60 =  default;
Int32 _znpjgsef =  default;
Int32 _e4ueamrn =  default;
Int32 _0hik27x4 =  default;
string fLanavab = default;
#endregion  variable declarations
_m2oo9cc0 = _m2oo9cc0.Convert(1);

	{
		//* 
		//*  -- LAPACK computational routine (version 3.7.0) -- 
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
		_gh3pzgwj = _w8y2rzgy(_m2oo9cc0 ,"Q" );
		_0hik27x4 = ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_dxpq0xkr );
		_lhlgm7z5 = (_6fnxzlyp == (int)-1);
		if ((!(_gh3pzgwj)) & (!(_w8y2rzgy(_m2oo9cc0 ,"P" ))))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_ev4xhht5 < (int)0)
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (((_dxpq0xkr < (int)0) | (_gh3pzgwj & ((_dxpq0xkr > _ev4xhht5) | (_dxpq0xkr < ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_umlkckdg ))))) | ((!(_gh3pzgwj)) & ((_ev4xhht5 > _dxpq0xkr) | (_ev4xhht5 < ILNumerics.F2NET.Intrinsics.MIN(_dxpq0xkr ,_umlkckdg )))))
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if (_umlkckdg < (int)0)
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ))
		{
			
			_gro5yvfo = (int)-6;
		}
		else
		if ((_6fnxzlyp < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_0hik27x4 )) & (!(_lhlgm7z5)))
		{
			
			_gro5yvfo = (int)-9;
		}
		//* 
		
		if (_gro5yvfo == (int)0)
		{
			
			*(_apig8meb+((int)1 - 1)) = DCMPLX((int)1);
			if (_gh3pzgwj)
			{
				
				if (_ev4xhht5 >= _umlkckdg)
				{
					
					_13b6etkp(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _umlkckdg ,_vxfgpup9 ,ref _ocv8fk5c ,_0446f4de ,_apig8meb ,ref Unsafe.AsRef((int)-1) ,ref _itfnbz60 );
				}
				else
				{
					
					if (_ev4xhht5 > (int)1)
					{
						
						_13b6etkp(ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,(_vxfgpup9+((int)2 - 1) + ((int)2 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_0446f4de ,_apig8meb ,ref Unsafe.AsRef((int)-1) ,ref _itfnbz60 );
					}
					
				}
				
			}
			else
			{
				
				if (_umlkckdg < _dxpq0xkr)
				{
					
					_fcuznjj5(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _umlkckdg ,_vxfgpup9 ,ref _ocv8fk5c ,_0446f4de ,_apig8meb ,ref Unsafe.AsRef((int)-1) ,ref _itfnbz60 );
				}
				else
				{
					
					if (_dxpq0xkr > (int)1)
					{
						
						_fcuznjj5(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,(_vxfgpup9+((int)2 - 1) + ((int)2 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_0446f4de ,_apig8meb ,ref Unsafe.AsRef((int)-1) ,ref _itfnbz60 );
					}
					
				}
				
			}
			
			_e4ueamrn = INT(*(_apig8meb+((int)1 - 1)));
			_e4ueamrn = ILNumerics.F2NET.Intrinsics.MAX(_e4ueamrn ,_0hik27x4 );
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("ZUNGBR" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		else
		if (_lhlgm7z5)
		{
			
			*(_apig8meb+((int)1 - 1)) = DCMPLX(_e4ueamrn);
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if ((_ev4xhht5 == (int)0) | (_dxpq0xkr == (int)0))
		{
			
			*(_apig8meb+((int)1 - 1)) = DCMPLX((int)1);
			return;
		}
		//* 
		
		if (_gh3pzgwj)
		{
			//* 
			//*        Form Q, determined by a call to ZGEBRD to reduce an m-by-k 
			//*        matrix 
			//* 
			
			if (_ev4xhht5 >= _umlkckdg)
			{
				//* 
				//*           If m >= k, assume m >= n >= k 
				//* 
				
				_13b6etkp(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _umlkckdg ,_vxfgpup9 ,ref _ocv8fk5c ,_0446f4de ,_apig8meb ,ref _6fnxzlyp ,ref _itfnbz60 );//* 
				
			}
			else
			{
				//* 
				//*           If m < k, assume m = n 
				//* 
				//*           Shift the vectors which define the elementary reflectors one 
				//*           column to the right, and set the first row and column of Q 
				//*           to those of the unit matrix 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn1316 = (System.Int32)(_ev4xhht5);
					System.Int32 __81fgg2step1316 = (System.Int32)((int)-1);
					System.Int32 __81fgg2count1316;
					for (__81fgg2count1316 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)2) - __81fgg2dlsvn1316 + __81fgg2step1316) / __81fgg2step1316)), _znpjgsef = __81fgg2dlsvn1316; __81fgg2count1316 != 0; __81fgg2count1316--, _znpjgsef += (__81fgg2step1316)) {

					{
						
						*(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
						{
							System.Int32 __81fgg2dlsvn1317 = (System.Int32)((_znpjgsef + (int)1));
							const System.Int32 __81fgg2step1317 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1317;
							for (__81fgg2count1317 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1317 + __81fgg2step1317) / __81fgg2step1317)), _b5p6od9s = __81fgg2dlsvn1317; __81fgg2count1317 != 0; __81fgg2count1317--, _b5p6od9s += (__81fgg2step1317)) {

							{
								
								*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - (int)1 - 1) * 1 * (_ocv8fk5c));
Mark10:;
								// continue
							}
														}						}
Mark20:;
						// continue
					}
										}				}
				*(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;
				{
					System.Int32 __81fgg2dlsvn1318 = (System.Int32)((int)2);
					const System.Int32 __81fgg2step1318 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1318;
					for (__81fgg2count1318 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1318 + __81fgg2step1318) / __81fgg2step1318)), _b5p6od9s = __81fgg2dlsvn1318; __81fgg2count1318 != 0; __81fgg2count1318--, _b5p6od9s += (__81fgg2step1318)) {

					{
						
						*(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
Mark30:;
						// continue
					}
										}				}
				if (_ev4xhht5 > (int)1)
				{
					//* 
					//*              Form Q(2:m,2:m) 
					//* 
					
					_13b6etkp(ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,(_vxfgpup9+((int)2 - 1) + ((int)2 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_0446f4de ,_apig8meb ,ref _6fnxzlyp ,ref _itfnbz60 );
				}
				
			}
			
		}
		else
		{
			//* 
			//*        Form P**H, determined by a call to ZGEBRD to reduce a k-by-n 
			//*        matrix 
			//* 
			
			if (_umlkckdg < _dxpq0xkr)
			{
				//* 
				//*           If k < n, assume k <= m <= n 
				//* 
				
				_fcuznjj5(ref _ev4xhht5 ,ref _dxpq0xkr ,ref _umlkckdg ,_vxfgpup9 ,ref _ocv8fk5c ,_0446f4de ,_apig8meb ,ref _6fnxzlyp ,ref _itfnbz60 );//* 
				
			}
			else
			{
				//* 
				//*           If k >= n, assume m = n 
				//* 
				//*           Shift the vectors which define the elementary reflectors one 
				//*           row downward, and set the first row and column of P**H to 
				//*           those of the unit matrix 
				//* 
				
				*(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;
				{
					System.Int32 __81fgg2dlsvn1319 = (System.Int32)((int)2);
					const System.Int32 __81fgg2step1319 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1319;
					for (__81fgg2count1319 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1319 + __81fgg2step1319) / __81fgg2step1319)), _b5p6od9s = __81fgg2dlsvn1319; __81fgg2count1319 != 0; __81fgg2count1319--, _b5p6od9s += (__81fgg2step1319)) {

					{
						
						*(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
Mark40:;
						// continue
					}
										}				}
				{
					System.Int32 __81fgg2dlsvn1320 = (System.Int32)((int)2);
					const System.Int32 __81fgg2step1320 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1320;
					for (__81fgg2count1320 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1320 + __81fgg2step1320) / __81fgg2step1320)), _znpjgsef = __81fgg2dlsvn1320; __81fgg2count1320 != 0; __81fgg2count1320--, _znpjgsef += (__81fgg2step1320)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn1321 = (System.Int32)((_znpjgsef - (int)1));
							System.Int32 __81fgg2step1321 = (System.Int32)((int)-1);
							System.Int32 __81fgg2count1321;
							for (__81fgg2count1321 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)2) - __81fgg2dlsvn1321 + __81fgg2step1321) / __81fgg2step1321)), _b5p6od9s = __81fgg2dlsvn1321; __81fgg2count1321 != 0; __81fgg2count1321--, _b5p6od9s += (__81fgg2step1321)) {

							{
								
								*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = *(_vxfgpup9+(_b5p6od9s - (int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c));
Mark50:;
								// continue
							}
														}						}
						*(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
Mark60:;
						// continue
					}
										}				}
				if (_dxpq0xkr > (int)1)
				{
					//* 
					//*              Form P**H(2:n,2:n) 
					//* 
					
					_fcuznjj5(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,(_vxfgpup9+((int)2 - 1) + ((int)2 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_0446f4de ,_apig8meb ,ref _6fnxzlyp ,ref _itfnbz60 );
				}
				
			}
			
		}
		
		*(_apig8meb+((int)1 - 1)) = DCMPLX(_e4ueamrn);
		return;//* 
		//*     End of ZUNGBR 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
