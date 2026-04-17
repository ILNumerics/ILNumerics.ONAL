
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
//*> \brief \b CUNMQL 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CUNMQL + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/cunmql.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/cunmql.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/cunmql.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CUNMQL( SIDE, TRANS, M, N, K, A, LDA, TAU, C, LDC, 
//*                          WORK, LWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          SIDE, TRANS 
//*       INTEGER            INFO, K, LDA, LDC, LWORK, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX            A( LDA, * ), C( LDC, * ), TAU( * ), 
//*      $                   WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CUNMQL overwrites the general complex M-by-N matrix C with 
//*> 
//*>                 SIDE = 'L'     SIDE = 'R' 
//*> TRANS = 'N':      Q * C          C * Q 
//*> TRANS = 'C':      Q**H * C       C * Q**H 
//*> 
//*> where Q is a complex unitary matrix defined as the product of k 
//*> elementary reflectors 
//*> 
//*>       Q = H(k) . . . H(2) H(1) 
//*> 
//*> as returned by CGEQLF. Q is of order M if SIDE = 'L' and of order N 
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
//*>          = 'C':  Transpose, apply Q**H. 
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
//*> \param[in] A 
//*> \verbatim 
//*>          A is COMPLEX array, dimension (LDA,K) 
//*>          The i-th column must contain the vector which defines the 
//*>          elementary reflector H(i), for i = 1,2,...,k, as returned by 
//*>          CGEQLF in the last k columns of its array argument A. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A. 
//*>          If SIDE = 'L', LDA >= max(1,M); 
//*>          if SIDE = 'R', LDA >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[in] TAU 
//*> \verbatim 
//*>          TAU is COMPLEX array, dimension (K) 
//*>          TAU(i) must contain the scalar factor of the elementary 
//*>          reflector H(i), as returned by CGEQLF. 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is COMPLEX array, dimension (LDC,N) 
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
//*>          WORK is COMPLEX array, dimension (MAX(1,LWORK)) 
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
//*> \ingroup complexOTHERcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _r7c38228(FString _m2cn2gjg, FString _scuo79v4, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, fcomplex* _0446f4de, fcomplex* _3crf0qn3, ref Int32 _1s3eymp4, fcomplex* _apig8meb, ref Int32 _6fnxzlyp, ref Int32 _gro5yvfo)
	{
#region variable declarations
Int32 _blnc1nox =  (int)64;
Int32 _w8yhbr2r =  _blnc1nox + (int)1;
Int32 _z68w9sjm =  _w8yhbr2r * _blnc1nox;
Boolean _pvwxvshr =  default;
Boolean _lhlgm7z5 =  default;
Boolean _2bzw4gjb =  default;
Int32 _b5p6od9s =  default;
Int32 _egqdmelt =  default;
Int32 _8ur10vsh =  default;
Int32 _b3a707ow =  default;
Int32 _vyr1z1si =  default;
Int32 _itfnbz60 =  default;
Int32 _y6qf7ne7 =  default;
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
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_joervqa5 ))
		{
			
			_gro5yvfo = (int)-7;
		}
		else
		if (_1s3eymp4 < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ))
		{
			
			_gro5yvfo = (int)-10;
		}
		else
		if ((_6fnxzlyp < _w6pmxgch) & (!(_lhlgm7z5)))
		{
			
			_gro5yvfo = (int)-12;
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
				
				_f7059815 = ILNumerics.F2NET.Intrinsics.MIN(_blnc1nox ,_4mvd6e4d(ref Unsafe.AsRef((int)1) ,"CUNMQL" ,_m2cn2gjg + _scuo79v4 ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef((int)-1) ) );
				_e4ueamrn = ((_w6pmxgch * _f7059815) + _z68w9sjm);
			}
			
			*(_apig8meb+((int)1 - 1)) = CMPLX(_e4ueamrn);
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("CUNMQL" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
		//*     Determine the block size 
		//* 
		
		_o80jnixx = (int)2;
		_iykhdriq = _w6pmxgch;
		if ((_f7059815 > (int)1) & (_f7059815 < _umlkckdg))
		{
			
			if (_6fnxzlyp < ((_w6pmxgch * _f7059815) + _z68w9sjm))
			{
				
				_f7059815 = ((_6fnxzlyp - _z68w9sjm) / _iykhdriq);
				_o80jnixx = ILNumerics.F2NET.Intrinsics.MAX((int)2 ,_4mvd6e4d(ref Unsafe.AsRef((int)2) ,"CUNMQL" ,_m2cn2gjg + _scuo79v4 ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _umlkckdg ,ref Unsafe.AsRef((int)-1) ) );
			}
			
		}
		//* 
		
		if ((_f7059815 < _o80jnixx) | (_f7059815 >= _umlkckdg))
		{
			//* 
			//*        Use unblocked code 
			//* 
			
			_9x5k8d5y(_m2cn2gjg ,_scuo79v4 ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _umlkckdg ,_vxfgpup9 ,ref _ocv8fk5c ,_0446f4de ,_3crf0qn3 ,ref _1s3eymp4 ,_apig8meb ,ref _itfnbz60 );
		}
		else
		{
			//* 
			//*        Use blocked code 
			//* 
			
			_y6qf7ne7 = ((int)1 + (_w6pmxgch * _f7059815));
			if ((_pvwxvshr & _2bzw4gjb) | ((!(_pvwxvshr)) & (!(_2bzw4gjb))))
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
			}
			else
			{
				
				_31eu052u = _ev4xhht5;
			}
			//* 
			
			{
				System.Int32 __81fgg2dlsvn3602 = (System.Int32)(_egqdmelt);
				System.Int32 __81fgg2step3602 = (System.Int32)(_b3a707ow);
				System.Int32 __81fgg2count3602;
				for (__81fgg2count3602 = System.Math.Max(0, (System.Int32)(((System.Int32)(_8ur10vsh) - __81fgg2dlsvn3602 + __81fgg2step3602) / __81fgg2step3602)), _b5p6od9s = __81fgg2dlsvn3602; __81fgg2count3602 != 0; __81fgg2count3602--, _b5p6od9s += (__81fgg2step3602)) {

				{
					
					_vyr1z1si = ILNumerics.F2NET.Intrinsics.MIN(_f7059815 ,(_umlkckdg - _b5p6od9s) + (int)1 );//* 
					//*           Form the triangular factor of the block reflector 
					//*           H = H(i+ib-1) . . . H(i+1) H(i) 
					//* 
					
					_uv02jzsj("Backward" ,"Columnwise" ,ref Unsafe.AsRef((((_joervqa5 - _umlkckdg) + _b5p6od9s) + _vyr1z1si) - (int)1) ,ref _vyr1z1si ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_0446f4de+(_b5p6od9s - 1)),(_apig8meb+(_y6qf7ne7 - 1)),ref Unsafe.AsRef(_w8yhbr2r) );
					if (_pvwxvshr)
					{
						//* 
						//*              H or H**H is applied to C(1:m-k+i+ib-1,1:n) 
						//* 
						
						_31eu052u = ((((_ev4xhht5 - _umlkckdg) + _b5p6od9s) + _vyr1z1si) - (int)1);
					}
					else
					{
						//* 
						//*              H or H**H is applied to C(1:m,1:n-k+i+ib-1) 
						//* 
						
						_q8n03esx = ((((_dxpq0xkr - _umlkckdg) + _b5p6od9s) + _vyr1z1si) - (int)1);
					}
					//* 
					//*           Apply H or H**H 
					//* 
					
					_h65laft9(_m2cn2gjg ,_scuo79v4 ,"Backward" ,"Columnwise" ,ref _31eu052u ,ref _q8n03esx ,ref _vyr1z1si ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_apig8meb+(_y6qf7ne7 - 1)),ref Unsafe.AsRef(_w8yhbr2r) ,_3crf0qn3 ,ref _1s3eymp4 ,_apig8meb ,ref _iykhdriq );
Mark10:;
					// continue
				}
								}			}
		}
		
		*(_apig8meb+((int)1 - 1)) = CMPLX(_e4ueamrn);
		return;//* 
		//*     End of CUNMQL 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
