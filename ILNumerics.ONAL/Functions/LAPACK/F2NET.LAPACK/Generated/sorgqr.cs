
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
//*> \brief \b SORGQR 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SORGQR + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/sorgqr.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/sorgqr.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/sorgqr.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SORGQR( M, N, K, A, LDA, TAU, WORK, LWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, K, LDA, LWORK, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               A( LDA, * ), TAU( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SORGQR generates an M-by-N real matrix Q with orthonormal columns, 
//*> which is defined as the first N columns of a product of K elementary 
//*> reflectors of order M 
//*> 
//*>       Q  =  H(1) H(2) . . . H(k) 
//*> 
//*> as returned by SGEQRF. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of rows of the matrix Q. M >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of columns of the matrix Q. M >= N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] K 
//*> \verbatim 
//*>          K is INTEGER 
//*>          The number of elementary reflectors whose product defines the 
//*>          matrix Q. N >= K >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is REAL array, dimension (LDA,N) 
//*>          On entry, the i-th column must contain the vector which 
//*>          defines the elementary reflector H(i), for i = 1,2,...,k, as 
//*>          returned by SGEQRF in the first k columns of its array 
//*>          argument A. 
//*>          On exit, the M-by-N matrix Q. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The first dimension of the array A. LDA >= max(1,M). 
//*> \endverbatim 
//*> 
//*> \param[in] TAU 
//*> \verbatim 
//*>          TAU is REAL array, dimension (K) 
//*>          TAU(i) must contain the scalar factor of the elementary 
//*>          reflector H(i), as returned by SGEQRF. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is REAL array, dimension (MAX(1,LWORK)) 
//*>          On exit, if INFO = 0, WORK(1) returns the optimal LWORK. 
//*> \endverbatim 
//*> 
//*> \param[in] LWORK 
//*> \verbatim 
//*>          LWORK is INTEGER 
//*>          The dimension of the array WORK. LWORK >= max(1,N). 
//*>          For optimum performance LWORK >= N*NB, where NB is the 
//*>          optimal blocksize. 
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
//*>          < 0:  if INFO = -i, the i-th argument has an illegal value 
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
//*> \ingroup realOTHERcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _mwcfh21x(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, Single* _vxfgpup9, ref Int32 _ocv8fk5c, Single* _0446f4de, Single* _apig8meb, ref Int32 _6fnxzlyp, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Boolean _lhlgm7z5 =  default;
Int32 _b5p6od9s =  default;
Int32 _vyr1z1si =  default;
Int32 _itfnbz60 =  default;
Int32 _7tvbphxm =  default;
Int32 _znpjgsef =  default;
Int32 _1ub95eoc =  default;
Int32 _dulqqknh =  default;
Int32 _68ec3gbh =  default;
Int32 _iykhdriq =  default;
Int32 _e4ueamrn =  default;
Int32 _f7059815 =  default;
Int32 _o80jnixx =  default;
Int32 _rtlyoyz3 =  default;
string fLanavab = default;
#endregion  variable declarations

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
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input arguments 
		//* 
		
		_gro5yvfo = (int)0;
		_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"SORGQR" ," " ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef((int)-1) );
		_e4ueamrn = (ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ) * _f7059815);
		*(_apig8meb+((int)1 - 1)) = REAL(_e4ueamrn);
		_lhlgm7z5 = (_6fnxzlyp == (int)-1);
		if (_ev4xhht5 < (int)0)
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if ((_dxpq0xkr < (int)0) | (_dxpq0xkr > _ev4xhht5))
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if ((_umlkckdg < (int)0) | (_umlkckdg > _dxpq0xkr))
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ))
		{
			
			_gro5yvfo = (int)-5;
		}
		else
		if ((_6fnxzlyp < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr )) & (!(_lhlgm7z5)))
		{
			
			_gro5yvfo = (int)-8;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("SORGQR" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
		
		if (_dxpq0xkr <= (int)0)
		{
			
			*(_apig8meb+((int)1 - 1)) = REAL((int)1);
			return;
		}
		//* 
		
		_o80jnixx = (int)2;
		_rtlyoyz3 = (int)0;
		_7tvbphxm = _dxpq0xkr;
		if ((_f7059815 > (int)1) & (_f7059815 < _umlkckdg))
		{
			//* 
			//*        Determine when to cross over from blocked to unblocked code. 
			//* 
			
			_rtlyoyz3 = ILNumerics.F2NET.Intrinsics.MAX((int)0 ,_4mvd6e4d(ref Unsafe.AsRef((int)3) ,"SORGQR" ," " ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef((int)-1) ) );
			if (_rtlyoyz3 < _umlkckdg)
			{
				//* 
				//*           Determine if workspace is large enough for blocked code. 
				//* 
				
				_iykhdriq = _dxpq0xkr;
				_7tvbphxm = (_iykhdriq * _f7059815);
				if (_6fnxzlyp < _7tvbphxm)
				{
					//* 
					//*              Not enough workspace to use optimal NB:  reduce NB and 
					//*              determine the minimum value of NB. 
					//* 
					
					_f7059815 = (_6fnxzlyp / _iykhdriq);
					_o80jnixx = ILNumerics.F2NET.Intrinsics.MAX((int)2 ,_4mvd6e4d(ref Unsafe.AsRef((int)2) ,"SORGQR" ," " ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef((int)-1) ) );
				}
				
			}
			
		}
		//* 
		
		if (((_f7059815 >= _o80jnixx) & (_f7059815 < _umlkckdg)) & (_rtlyoyz3 < _umlkckdg))
		{
			//* 
			//*        Use blocked code after the last block. 
			//*        The first kk columns are handled by the block method. 
			//* 
			
			_1ub95eoc = ((((_umlkckdg - _rtlyoyz3) - (int)1) / _f7059815) * _f7059815);
			_dulqqknh = ILNumerics.F2NET.Intrinsics.MIN(_umlkckdg ,_1ub95eoc + _f7059815 );//* 
			//*        Set A(1:kk,kk+1:n) to zero. 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn890 = (System.Int32)((_dulqqknh + (int)1));
				const System.Int32 __81fgg2step890 = (System.Int32)((int)1);
				System.Int32 __81fgg2count890;
				for (__81fgg2count890 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn890 + __81fgg2step890) / __81fgg2step890)), _znpjgsef = __81fgg2dlsvn890; __81fgg2count890 != 0; __81fgg2count890--, _znpjgsef += (__81fgg2step890)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn891 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step891 = (System.Int32)((int)1);
						System.Int32 __81fgg2count891;
						for (__81fgg2count891 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dulqqknh) - __81fgg2dlsvn891 + __81fgg2step891) / __81fgg2step891)), _b5p6od9s = __81fgg2dlsvn891; __81fgg2count891 != 0; __81fgg2count891--, _b5p6od9s += (__81fgg2step891)) {

						{
							
							*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
Mark10:;
							// continue
						}
												}					}
Mark20:;
					// continue
				}
								}			}
		}
		else
		{
			
			_dulqqknh = (int)0;
		}
		//* 
		//*     Use unblocked code for the last or only block. 
		//* 
		
		if (_dulqqknh < _dxpq0xkr)
		_spjge3mz(ref Unsafe.AsRef(_ev4xhht5 - _dulqqknh) ,ref Unsafe.AsRef(_dxpq0xkr - _dulqqknh) ,ref Unsafe.AsRef(_umlkckdg - _dulqqknh) ,(_vxfgpup9+(_dulqqknh + (int)1 - 1) + (_dulqqknh + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_0446f4de+(_dulqqknh + (int)1 - 1)),_apig8meb ,ref _itfnbz60 );//* 
		
		if (_dulqqknh > (int)0)
		{
			//* 
			//*        Use blocked code 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn892 = (System.Int32)((_1ub95eoc + (int)1));
				System.Int32 __81fgg2step892 = (System.Int32)(-(_f7059815));
				System.Int32 __81fgg2count892;
				for (__81fgg2count892 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn892 + __81fgg2step892) / __81fgg2step892)), _b5p6od9s = __81fgg2dlsvn892; __81fgg2count892 != 0; __81fgg2count892--, _b5p6od9s += (__81fgg2step892)) {

				{
					
					_vyr1z1si = ILNumerics.F2NET.Intrinsics.MIN(_f7059815 ,(_umlkckdg - _b5p6od9s) + (int)1 );
					if ((_b5p6od9s + _vyr1z1si) <= _dxpq0xkr)
					{
						//* 
						//*              Form the triangular factor of the block reflector 
						//*              H = H(i) H(i+1) . . . H(i+ib-1) 
						//* 
						
						_cgg84bb4("Forward" ,"Columnwise" ,ref Unsafe.AsRef((_ev4xhht5 - _b5p6od9s) + (int)1) ,ref _vyr1z1si ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_0446f4de+(_b5p6od9s - 1)),_apig8meb ,ref _iykhdriq );//* 
						//*              Apply H to A(i:m,i+ib:n) from the left 
						//* 
						
						_8ewy8tu3("Left" ,"No transpose" ,"Forward" ,"Columnwise" ,ref Unsafe.AsRef((_ev4xhht5 - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(((_dxpq0xkr - _b5p6od9s) - _vyr1z1si) + (int)1) ,ref _vyr1z1si ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_apig8meb ,ref _iykhdriq ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s + _vyr1z1si - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_apig8meb+(_vyr1z1si + (int)1 - 1)),ref _iykhdriq );
					}
					//* 
					//*           Apply H to rows i:m of current block 
					//* 
					
					_spjge3mz(ref Unsafe.AsRef((_ev4xhht5 - _b5p6od9s) + (int)1) ,ref _vyr1z1si ,ref _vyr1z1si ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_0446f4de+(_b5p6od9s - 1)),_apig8meb ,ref _itfnbz60 );//* 
					//*           Set rows 1:i-1 of current block to zero 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn893 = (System.Int32)(_b5p6od9s);
						const System.Int32 __81fgg2step893 = (System.Int32)((int)1);
						System.Int32 __81fgg2count893;
						for (__81fgg2count893 = System.Math.Max(0, (System.Int32)(((System.Int32)((_b5p6od9s + _vyr1z1si) - (int)1) - __81fgg2dlsvn893 + __81fgg2step893) / __81fgg2step893)), _znpjgsef = __81fgg2dlsvn893; __81fgg2count893 != 0; __81fgg2count893--, _znpjgsef += (__81fgg2step893)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn894 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step894 = (System.Int32)((int)1);
								System.Int32 __81fgg2count894;
								for (__81fgg2count894 = System.Math.Max(0, (System.Int32)(((System.Int32)(_b5p6od9s - (int)1) - __81fgg2dlsvn894 + __81fgg2step894) / __81fgg2step894)), _68ec3gbh = __81fgg2dlsvn894; __81fgg2count894 != 0; __81fgg2count894--, _68ec3gbh += (__81fgg2step894)) {

								{
									
									*(_vxfgpup9+(_68ec3gbh - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
Mark30:;
									// continue
								}
																}							}
Mark40:;
							// continue
						}
												}					}
Mark50:;
					// continue
				}
								}			}
		}
		//* 
		
		*(_apig8meb+((int)1 - 1)) = REAL(_7tvbphxm);
		return;//* 
		//*     End of SORGQR 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
