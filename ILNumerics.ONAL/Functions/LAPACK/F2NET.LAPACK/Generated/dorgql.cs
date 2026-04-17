
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
//*> \brief \b DORGQL 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DORGQL + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dorgql.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dorgql.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dorgql.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DORGQL( M, N, K, A, LDA, TAU, WORK, LWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, K, LDA, LWORK, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   A( LDA, * ), TAU( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DORGQL generates an M-by-N real matrix Q with orthonormal columns, 
//*> which is defined as the last N columns of a product of K elementary 
//*> reflectors of order M 
//*> 
//*>       Q  =  H(k) . . . H(2) H(1) 
//*> 
//*> as returned by DGEQLF. 
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
//*>          A is DOUBLE PRECISION array, dimension (LDA,N) 
//*>          On entry, the (n-k+i)-th column must contain the vector which 
//*>          defines the elementary reflector H(i), for i = 1,2,...,k, as 
//*>          returned by DGEQLF in the last k columns of its array 
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
//*>          TAU is DOUBLE PRECISION array, dimension (K) 
//*>          TAU(i) must contain the scalar factor of the elementary 
//*>          reflector H(i), as returned by DGEQLF. 
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
//*> \ingroup doubleOTHERcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _reox2p5a(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, Double* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _0446f4de, Double* _apig8meb, ref Int32 _6fnxzlyp, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Boolean _lhlgm7z5 =  default;
Int32 _b5p6od9s =  default;
Int32 _vyr1z1si =  default;
Int32 _itfnbz60 =  default;
Int32 _7tvbphxm =  default;
Int32 _znpjgsef =  default;
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
		//* 
		
		if (_gro5yvfo == (int)0)
		{
			
			if (_dxpq0xkr == (int)0)
			{
				
				_e4ueamrn = (int)1;
			}
			else
			{
				
				_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"DORGQL" ," " ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef((int)-1) );
				_e4ueamrn = (_dxpq0xkr * _f7059815);
			}
			
			*(_apig8meb+((int)1 - 1)) = DBLE(_e4ueamrn);//* 
			
			if ((_6fnxzlyp < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr )) & (!(_lhlgm7z5)))
			{
				
				_gro5yvfo = (int)-8;
			}
			
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("DORGQL" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
			
			_rtlyoyz3 = ILNumerics.F2NET.Intrinsics.MAX((int)0 ,_4mvd6e4d(ref Unsafe.AsRef((int)3) ,"DORGQL" ," " ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef((int)-1) ) );
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
					_o80jnixx = ILNumerics.F2NET.Intrinsics.MAX((int)2 ,_4mvd6e4d(ref Unsafe.AsRef((int)2) ,"DORGQL" ," " ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef((int)-1) ) );
				}
				
			}
			
		}
		//* 
		
		if (((_f7059815 >= _o80jnixx) & (_f7059815 < _umlkckdg)) & (_rtlyoyz3 < _umlkckdg))
		{
			//* 
			//*        Use blocked code after the first block. 
			//*        The last kk columns are handled by the block method. 
			//* 
			
			_dulqqknh = ILNumerics.F2NET.Intrinsics.MIN(_umlkckdg ,((((_umlkckdg - _rtlyoyz3) + _f7059815) - (int)1) / _f7059815) * _f7059815 );//* 
			//*        Set A(m-kk+1:m,1:n-kk) to zero. 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn3722 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step3722 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3722;
				for (__81fgg2count3722 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - _dulqqknh) - __81fgg2dlsvn3722 + __81fgg2step3722) / __81fgg2step3722)), _znpjgsef = __81fgg2dlsvn3722; __81fgg2count3722 != 0; __81fgg2count3722--, _znpjgsef += (__81fgg2step3722)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn3723 = (System.Int32)(((_ev4xhht5 - _dulqqknh) + (int)1));
						const System.Int32 __81fgg2step3723 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3723;
						for (__81fgg2count3723 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3723 + __81fgg2step3723) / __81fgg2step3723)), _b5p6od9s = __81fgg2dlsvn3723; __81fgg2count3723 != 0; __81fgg2count3723--, _b5p6od9s += (__81fgg2step3723)) {

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
		//*     Use unblocked code for the first or only block. 
		//* 
		
		_nm04cz8s(ref Unsafe.AsRef(_ev4xhht5 - _dulqqknh) ,ref Unsafe.AsRef(_dxpq0xkr - _dulqqknh) ,ref Unsafe.AsRef(_umlkckdg - _dulqqknh) ,_vxfgpup9 ,ref _ocv8fk5c ,_0446f4de ,_apig8meb ,ref _itfnbz60 );//* 
		
		if (_dulqqknh > (int)0)
		{
			//* 
			//*        Use blocked code 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn3724 = (System.Int32)(((_umlkckdg - _dulqqknh) + (int)1));
				System.Int32 __81fgg2step3724 = (System.Int32)(_f7059815);
				System.Int32 __81fgg2count3724;
				for (__81fgg2count3724 = System.Math.Max(0, (System.Int32)(((System.Int32)(_umlkckdg) - __81fgg2dlsvn3724 + __81fgg2step3724) / __81fgg2step3724)), _b5p6od9s = __81fgg2dlsvn3724; __81fgg2count3724 != 0; __81fgg2count3724--, _b5p6od9s += (__81fgg2step3724)) {

				{
					
					_vyr1z1si = ILNumerics.F2NET.Intrinsics.MIN(_f7059815 ,(_umlkckdg - _b5p6od9s) + (int)1 );
					if (((_dxpq0xkr - _umlkckdg) + _b5p6od9s) > (int)1)
					{
						//* 
						//*              Form the triangular factor of the block reflector 
						//*              H = H(i+ib-1) . . . H(i+1) H(i) 
						//* 
						
						_kq0awdbm("Backward" ,"Columnwise" ,ref Unsafe.AsRef((((_ev4xhht5 - _umlkckdg) + _b5p6od9s) + _vyr1z1si) - (int)1) ,ref _vyr1z1si ,(_vxfgpup9+((int)1 - 1) + ((_dxpq0xkr - _umlkckdg) + _b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_0446f4de+(_b5p6od9s - 1)),_apig8meb ,ref _iykhdriq );//* 
						//*              Apply H to A(1:m-k+i+ib-1,1:n-k+i-1) from the left 
						//* 
						
						_s5p1x6x6("Left" ,"No transpose" ,"Backward" ,"Columnwise" ,ref Unsafe.AsRef((((_ev4xhht5 - _umlkckdg) + _b5p6od9s) + _vyr1z1si) - (int)1) ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) + _b5p6od9s) - (int)1) ,ref _vyr1z1si ,(_vxfgpup9+((int)1 - 1) + ((_dxpq0xkr - _umlkckdg) + _b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_apig8meb ,ref _iykhdriq ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_vyr1z1si + (int)1 - 1)),ref _iykhdriq );
					}
					//* 
					//*           Apply H to rows 1:m-k+i+ib-1 of current block 
					//* 
					
					_nm04cz8s(ref Unsafe.AsRef((((_ev4xhht5 - _umlkckdg) + _b5p6od9s) + _vyr1z1si) - (int)1) ,ref _vyr1z1si ,ref _vyr1z1si ,(_vxfgpup9+((int)1 - 1) + ((_dxpq0xkr - _umlkckdg) + _b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_0446f4de+(_b5p6od9s - 1)),_apig8meb ,ref _itfnbz60 );//* 
					//*           Set rows m-k+i+ib:m of current block to zero 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn3725 = (System.Int32)(((_dxpq0xkr - _umlkckdg) + _b5p6od9s));
						const System.Int32 __81fgg2step3725 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3725;
						for (__81fgg2count3725 = System.Math.Max(0, (System.Int32)(((System.Int32)((((_dxpq0xkr - _umlkckdg) + _b5p6od9s) + _vyr1z1si) - (int)1) - __81fgg2dlsvn3725 + __81fgg2step3725) / __81fgg2step3725)), _znpjgsef = __81fgg2dlsvn3725; __81fgg2count3725 != 0; __81fgg2count3725--, _znpjgsef += (__81fgg2step3725)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn3726 = (System.Int32)((((_ev4xhht5 - _umlkckdg) + _b5p6od9s) + _vyr1z1si));
								const System.Int32 __81fgg2step3726 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3726;
								for (__81fgg2count3726 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3726 + __81fgg2step3726) / __81fgg2step3726)), _68ec3gbh = __81fgg2dlsvn3726; __81fgg2count3726 != 0; __81fgg2count3726--, _68ec3gbh += (__81fgg2step3726)) {

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
		
		*(_apig8meb+((int)1 - 1)) = DBLE(_7tvbphxm);
		return;//* 
		//*     End of DORGQL 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
