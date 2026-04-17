
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
//*> \brief \b ZUNMRZ 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZUNMRZ + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zunmrz.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zunmrz.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zunmrz.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZUNMRZ( SIDE, TRANS, M, N, K, L, A, LDA, TAU, C, LDC, 
//*                          WORK, LWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          SIDE, TRANS 
//*       INTEGER            INFO, K, L, LDA, LDC, LWORK, M, N 
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
//*> ZUNMRZ overwrites the general complex M-by-N matrix C with 
//*> 
//*>                 SIDE = 'L'     SIDE = 'R' 
//*> TRANS = 'N':      Q * C          C * Q 
//*> TRANS = 'C':      Q**H * C       C * Q**H 
//*> 
//*> where Q is a complex unitary matrix defined as the product of k 
//*> elementary reflectors 
//*> 
//*>       Q = H(1) H(2) . . . H(k) 
//*> 
//*> as returned by ZTZRZF. Q is of order M if SIDE = 'L' and of order N 
//*> if SIDE = 'R'. 
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
//*>          = 'N':  No transpose, apply Q; 
//*>          = 'C':  Conjugate transpose, apply Q**H. 
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
//*>          The number of elementary reflectors whose product defines 
//*>          the matrix Q. 
//*>          If SIDE = 'L', M >= K >= 0; 
//*>          if SIDE = 'R', N >= K >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] L 
//*> \verbatim 
//*>          L is INTEGER 
//*>          The number of columns of the matrix A containing 
//*>          the meaningful part of the Householder reflectors. 
//*>          If SIDE = 'L', M >= L >= 0, if SIDE = 'R', N >= L >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension 
//*>                               (LDA,M) if SIDE = 'L', 
//*>                               (LDA,N) if SIDE = 'R' 
//*>          The i-th row must contain the vector which defines the 
//*>          elementary reflector H(i), for i = 1,2,...,k, as returned by 
//*>          ZTZRZF in the last k rows of its array argument A. 
//*>          A is modified by the routine but restored on exit. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A. LDA >= max(1,K). 
//*> \endverbatim 
//*> 
//*> \param[in] TAU 
//*> \verbatim 
//*>          TAU is COMPLEX*16 array, dimension (K) 
//*>          TAU(i) must contain the scalar factor of the elementary 
//*>          reflector H(i), as returned by ZTZRZF. 
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
//*> \par Contributors: 
//*  ================== 
//*> 
//*>    A. Petitet, Computer Science Dept., Univ. of Tenn., Knoxville, USA 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _x48lx74v(FString _m2cn2gjg, FString _scuo79v4, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, ref Int32 _68ec3gbh, complex* _vxfgpup9, ref Int32 _ocv8fk5c, complex* _0446f4de, complex* _3crf0qn3, ref Int32 _1s3eymp4, complex* _apig8meb, ref Int32 _6fnxzlyp, ref Int32 _gro5yvfo)
	{
#region variable declarations
Int32 _blnc1nox =  (int)64;
Int32 _w8yhbr2r =  _blnc1nox + (int)1;
Int32 _z68w9sjm =  _w8yhbr2r * _blnc1nox;
Boolean _pvwxvshr =  default;
Boolean _lhlgm7z5 =  default;
Boolean _2bzw4gjb =  default;
FString _l09pt3ga =  new FString(1);
Int32 _b5p6od9s =  default;
Int32 _egqdmelt =  default;
Int32 _8ur10vsh =  default;
Int32 _b3a707ow =  default;
Int32 _vyr1z1si =  default;
Int32 _8jzcrkri =  default;
Int32 _itfnbz60 =  default;
Int32 _y6qf7ne7 =  default;
Int32 _fpznay24 =  default;
Int32 _aynldcwj =  default;
Int32 _iykhdriq =  default;
Int32 _e4ueamrn =  default;
Int32 _31eu052u =  default;
Int32 _f7059815 =  default;
Int32 _o80jnixx =  default;
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
		_pvwxvshr = _w8y2rzgy(_m2cn2gjg ,"L" );
		_2bzw4gjb = _w8y2rzgy(_scuo79v4 ,"N" );
		_lhlgm7z5 = (_6fnxzlyp == (int)-1);//* 
		//*     NQ is the order of Q and NW is the minimum dimension of WORK 
		//* 
		
		if (_pvwxvshr)
		{
			
			_joervqa5 = _ev4xhht5;
			_w6pmxgch = ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr );
		}
		else
		{
			
			_joervqa5 = _dxpq0xkr;
			_w6pmxgch = ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 );
		}
		
		if ((!(_pvwxvshr)) & (!(_w8y2rzgy(_m2cn2gjg ,"R" ))))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if ((!(_2bzw4gjb)) & (!(_w8y2rzgy(_scuo79v4 ,"C" ))))
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
		if ((_umlkckdg < (int)0) | (_umlkckdg > _joervqa5))
		{
			
			_gro5yvfo = (int)-5;
		}
		else
		if (((_68ec3gbh < (int)0) | (_pvwxvshr & (_68ec3gbh > _ev4xhht5))) | ((!(_pvwxvshr)) & (_68ec3gbh > _dxpq0xkr)))
		{
			
			_gro5yvfo = (int)-6;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_umlkckdg ))
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
			//* 
			//*        Compute the workspace requirements 
			//* 
			
			if ((_ev4xhht5 == (int)0) | (_dxpq0xkr == (int)0))
			{
				
				_e4ueamrn = (int)1;
			}
			else
			{
				
				_f7059815 = ILNumerics.F2NET.Intrinsics.MIN(_blnc1nox ,_4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZUNMRQ" ,_m2cn2gjg + _scuo79v4 ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef((int)-1) ) );
				_e4ueamrn = ((_w6pmxgch * _f7059815) + _z68w9sjm);
			}
			
			*(_apig8meb+((int)1 - 1)) = DCMPLX(_e4ueamrn);
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("ZUNMRZ" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
		//*     Determine the block size.  NB may be at most NBMAX, where NBMAX 
		//*     is used to define the local array T. 
		//* 
		
		_f7059815 = ILNumerics.F2NET.Intrinsics.MIN(_blnc1nox ,_4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZUNMRQ" ,_m2cn2gjg + _scuo79v4 ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef((int)-1) ) );
		_o80jnixx = (int)2;
		_iykhdriq = _w6pmxgch;
		if ((_f7059815 > (int)1) & (_f7059815 < _umlkckdg))
		{
			
			if (_6fnxzlyp < ((_w6pmxgch * _f7059815) + _z68w9sjm))
			{
				
				_f7059815 = ((_6fnxzlyp - _z68w9sjm) / _iykhdriq);
				_o80jnixx = ILNumerics.F2NET.Intrinsics.MAX((int)2 ,_4mvd6e4d(ref Unsafe.AsRef((int)2) ,"ZUNMRQ" ,_m2cn2gjg + _scuo79v4 ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef((int)-1) ) );
			}
			
		}
		//* 
		
		if ((_f7059815 < _o80jnixx) | (_f7059815 >= _umlkckdg))
		{
			//* 
			//*        Use unblocked code 
			//* 
			
			_dfvzqlg2(_m2cn2gjg ,_scuo79v4 ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _umlkckdg ,ref _68ec3gbh ,_vxfgpup9 ,ref _ocv8fk5c ,_0446f4de ,_3crf0qn3 ,ref _1s3eymp4 ,_apig8meb ,ref _itfnbz60 );
		}
		else
		{
			//* 
			//*        Use blocked code 
			//* 
			
			_y6qf7ne7 = ((int)1 + (_w6pmxgch * _f7059815));
			if ((_pvwxvshr & (!(_2bzw4gjb))) | ((!(_pvwxvshr)) & _2bzw4gjb))
			{
				
				_egqdmelt = (int)1;
				_8ur10vsh = _umlkckdg;
				_b3a707ow = _f7059815;
			}
			else
			{
				
				_egqdmelt = ((((_umlkckdg - (int)1) / _f7059815) * _f7059815) + (int)1);
				_8ur10vsh = (int)1;
				_b3a707ow = (-(_f7059815));
			}
			//* 
			
			if (_pvwxvshr)
			{
				
				_q8n03esx = _dxpq0xkr;
				_aynldcwj = (int)1;
				_fpznay24 = ((_ev4xhht5 - _68ec3gbh) + (int)1);
			}
			else
			{
				
				_31eu052u = _ev4xhht5;
				_8jzcrkri = (int)1;
				_fpznay24 = ((_dxpq0xkr - _68ec3gbh) + (int)1);
			}
			//* 
			
			if (_2bzw4gjb)
			{
				
				
				_l09pt3ga = "C";
			}
			else
			{
				
				
				_l09pt3ga = "N";
			}
			//* 
			
			{
				System.Int32 __81fgg2dlsvn2172 = (System.Int32)(_egqdmelt);
				System.Int32 __81fgg2step2172 = (System.Int32)(_b3a707ow);
				System.Int32 __81fgg2count2172;
				for (__81fgg2count2172 = System.Math.Max(0, (System.Int32)(((System.Int32)(_8ur10vsh) - __81fgg2dlsvn2172 + __81fgg2step2172) / __81fgg2step2172)), _b5p6od9s = __81fgg2dlsvn2172; __81fgg2count2172 != 0; __81fgg2count2172--, _b5p6od9s += (__81fgg2step2172)) {

				{
					
					_vyr1z1si = ILNumerics.F2NET.Intrinsics.MIN(_f7059815 ,(_umlkckdg - _b5p6od9s) + (int)1 );//* 
					//*           Form the triangular factor of the block reflector 
					//*           H = H(i+ib-1) . . . H(i+1) H(i) 
					//* 
					
					_s87qy803("Backward" ,"Rowwise" ,ref _68ec3gbh ,ref _vyr1z1si ,(_vxfgpup9+(_b5p6od9s - 1) + (_fpznay24 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_0446f4de+(_b5p6od9s - 1)),(_apig8meb+(_y6qf7ne7 - 1)),ref Unsafe.AsRef(_w8yhbr2r) );//* 
					
					if (_pvwxvshr)
					{
						//* 
						//*              H or H**H is applied to C(i:m,1:n) 
						//* 
						
						_31eu052u = ((_ev4xhht5 - _b5p6od9s) + (int)1);
						_8jzcrkri = _b5p6od9s;
					}
					else
					{
						//* 
						//*              H or H**H is applied to C(1:m,i:n) 
						//* 
						
						_q8n03esx = ((_dxpq0xkr - _b5p6od9s) + (int)1);
						_aynldcwj = _b5p6od9s;
					}
					//* 
					//*           Apply H or H**H 
					//* 
					
					_z7rsp9uy(_m2cn2gjg ,_l09pt3ga ,"Backward" ,"Rowwise" ,ref _31eu052u ,ref _q8n03esx ,ref _vyr1z1si ,ref _68ec3gbh ,(_vxfgpup9+(_b5p6od9s - 1) + (_fpznay24 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_apig8meb+(_y6qf7ne7 - 1)),ref Unsafe.AsRef(_w8yhbr2r) ,(_3crf0qn3+(_8jzcrkri - 1) + (_aynldcwj - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,_apig8meb ,ref _iykhdriq );
Mark10:;
					// continue
				}
								}			}//* 
			
		}
		//* 
		
		*(_apig8meb+((int)1 - 1)) = DCMPLX(_e4ueamrn);//* 
		
		return;//* 
		//*     End of ZUNMRZ 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
