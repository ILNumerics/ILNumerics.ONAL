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
//*> \brief \b DGEHRD 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DGEHRD + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dgehrd.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dgehrd.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dgehrd.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DGEHRD( N, ILO, IHI, A, LDA, TAU, WORK, LWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            IHI, ILO, INFO, LDA, LWORK, N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION  A( LDA, * ), TAU( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DGEHRD reduces a real general matrix A to upper Hessenberg form H by 
//*> an orthogonal similarity transformation:  Q**T * A * Q = H . 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix A.  N >= 0. 
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
//*>          It is assumed that A is already upper triangular in rows 
//*>          and columns 1:ILO-1 and IHI+1:N. ILO and IHI are normally 
//*>          set by a previous call to DGEBAL; otherwise they should be 
//*>          set to 1 and N respectively. See Further Details. 
//*>          1 <= ILO <= IHI <= N, if N > 0; ILO=1 and IHI=0, if N=0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is DOUBLE PRECISION array, dimension (LDA,N) 
//*>          On entry, the N-by-N general matrix to be reduced. 
//*>          On exit, the upper triangle and the first subdiagonal of A 
//*>          are overwritten with the upper Hessenberg matrix H, and the 
//*>          elements below the first subdiagonal, with the array TAU, 
//*>          represent the orthogonal matrix Q as a product of elementary 
//*>          reflectors. See Further Details. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[out] TAU 
//*> \verbatim 
//*>          TAU is DOUBLE PRECISION array, dimension (N-1) 
//*>          The scalar factors of the elementary reflectors (see Further 
//*>          Details). Elements 1:ILO-1 and IHI:N-1 of TAU are set to 
//*>          zero. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is DOUBLE PRECISION array, dimension (LWORK) 
//*>          On exit, if INFO = 0, WORK(1) returns the optimal LWORK. 
//*> \endverbatim 
//*> 
//*> \param[in] LWORK 
//*> \verbatim 
//*>          LWORK is INTEGER 
//*>          The length of the array WORK.  LWORK >= max(1,N). 
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
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value. 
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
//*> \ingroup doubleGEcomputational 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  The matrix Q is represented as a product of (ihi-ilo) elementary 
//*>  reflectors 
//*> 
//*>     Q = H(ilo) H(ilo+1) . . . H(ihi-1). 
//*> 
//*>  Each H(i) has the form 
//*> 
//*>     H(i) = I - tau * v * v**T 
//*> 
//*>  where tau is a real scalar, and v is a real vector with 
//*>  v(1:i) = 0, v(i+1) = 1 and v(ihi+1:n) = 0; v(i+2:ihi) is stored on 
//*>  exit in A(i+2:ihi,i), and tau in TAU(i). 
//*> 
//*>  The contents of A are illustrated by the following example, with 
//*>  n = 7, ilo = 2 and ihi = 6: 
//*> 
//*>  on entry,                        on exit, 
//*> 
//*>  ( a   a   a   a   a   a   a )    (  a   a   h   h   h   h   a ) 
//*>  (     a   a   a   a   a   a )    (      a   h   h   h   h   a ) 
//*>  (     a   a   a   a   a   a )    (      h   h   h   h   h   h ) 
//*>  (     a   a   a   a   a   a )    (      v2  h   h   h   h   h ) 
//*>  (     a   a   a   a   a   a )    (      v2  v3  h   h   h   h ) 
//*>  (     a   a   a   a   a   a )    (      v2  v3  v4  h   h   h ) 
//*>  (                         a )    (                          a ) 
//*> 
//*>  where a denotes an element of the original matrix A, h denotes a 
//*>  modified element of the upper Hessenberg matrix H, and vi denotes an 
//*>  element of the vector defining H(i). 
//*> 
//*>  This file is a slight modification of LAPACK-3.0's DGEHRD 
//*>  subroutine incorporating improvements proposed by Quintana-Orti and 
//*>  Van de Geijn (2006). (See DLAHR2.) 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _g5i7nj2c(ref Int32 _dxpq0xkr, ref Int32 _pew3blan, ref Int32 _9c1csucx, Double* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _0446f4de, Double* _apig8meb, ref Int32 _6fnxzlyp, ref Int32 _gro5yvfo)
	{
#region variable declarations
Int32 _blnc1nox =  (int)64;
Int32 _w8yhbr2r =  _blnc1nox + (int)1;
Int32 _z68w9sjm =  _w8yhbr2r * _blnc1nox;
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Boolean _lhlgm7z5 =  default;
Int32 _b5p6od9s =  default;
Int32 _vyr1z1si =  default;
Int32 _itfnbz60 =  default;
Int32 _y6qf7ne7 =  default;
Int32 _znpjgsef =  default;
Int32 _iykhdriq =  default;
Int32 _e4ueamrn =  default;
Int32 _f7059815 =  default;
Int32 _o80jnixx =  default;
Int32 _aym8a085 =  default;
Int32 _rtlyoyz3 =  default;
Double _85pvcc5e =  default;
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
		//*     Test the input parameters 
		//* 
		
		_gro5yvfo = (int)0;
		_lhlgm7z5 = (_6fnxzlyp == (int)-1);
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if ((_pew3blan < (int)1) | (_pew3blan > ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr )))
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if ((_9c1csucx < ILNumerics.F2NET.Intrinsics.MIN(_pew3blan ,_dxpq0xkr )) | (_9c1csucx > _dxpq0xkr))
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)-5;
		}
		else
		if ((_6fnxzlyp < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr )) & (!(_lhlgm7z5)))
		{
			
			_gro5yvfo = (int)-8;
		}
		//* 
		
		if (_gro5yvfo == (int)0)
		{
			//* 
			//*        Compute the workspace requirements 
			//* 
			
			_f7059815 = ILNumerics.F2NET.Intrinsics.MIN(_blnc1nox ,_4mvd6e4d(ref Unsafe.AsRef((int)1) ,"DGEHRD" ," " ,ref _dxpq0xkr ,ref _pew3blan ,ref _9c1csucx ,ref Unsafe.AsRef((int)-1) ) );
			_e4ueamrn = ((_dxpq0xkr * _f7059815) + _z68w9sjm);
			*(_apig8meb+((int)1 - 1)) = DBLE(_e4ueamrn);
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("DGEHRD" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		else
		if (_lhlgm7z5)
		{
			
			return;
		}
		//* 
		//*     Set elements 1:ILO-1 and IHI:N-1 of TAU to zero 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn2189 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2189 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2189;
			for (__81fgg2count2189 = System.Math.Max(0, (System.Int32)(((System.Int32)(_pew3blan - (int)1) - __81fgg2dlsvn2189 + __81fgg2step2189) / __81fgg2step2189)), _b5p6od9s = __81fgg2dlsvn2189; __81fgg2count2189 != 0; __81fgg2count2189--, _b5p6od9s += (__81fgg2step2189)) {

			{
				
				*(_0446f4de+(_b5p6od9s - 1)) = _d0547bi2;
Mark10:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn2190 = (System.Int32)(ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_9c1csucx ));
			const System.Int32 __81fgg2step2190 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2190;
			for (__81fgg2count2190 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn2190 + __81fgg2step2190) / __81fgg2step2190)), _b5p6od9s = __81fgg2dlsvn2190; __81fgg2count2190 != 0; __81fgg2count2190--, _b5p6od9s += (__81fgg2step2190)) {

			{
				
				*(_0446f4de+(_b5p6od9s - 1)) = _d0547bi2;
Mark20:;
				// continue
			}
						}		}//* 
		//*     Quick return if possible 
		//* 
		
		_aym8a085 = ((_9c1csucx - _pew3blan) + (int)1);
		if (_aym8a085 <= (int)1)
		{
			
			*(_apig8meb+((int)1 - 1)) = DBLE((int)1);
			return;
		}
		//* 
		//*     Determine the block size 
		//* 
		
		_f7059815 = ILNumerics.F2NET.Intrinsics.MIN(_blnc1nox ,_4mvd6e4d(ref Unsafe.AsRef((int)1) ,"DGEHRD" ," " ,ref _dxpq0xkr ,ref _pew3blan ,ref _9c1csucx ,ref Unsafe.AsRef((int)-1) ) );
		_o80jnixx = (int)2;
		if ((_f7059815 > (int)1) & (_f7059815 < _aym8a085))
		{
			//* 
			//*        Determine when to cross over from blocked to unblocked code 
			//*        (last block is always handled by unblocked code) 
			//* 
			
			_rtlyoyz3 = ILNumerics.F2NET.Intrinsics.MAX(_f7059815 ,_4mvd6e4d(ref Unsafe.AsRef((int)3) ,"DGEHRD" ," " ,ref _dxpq0xkr ,ref _pew3blan ,ref _9c1csucx ,ref Unsafe.AsRef((int)-1) ) );
			if (_rtlyoyz3 < _aym8a085)
			{
				//* 
				//*           Determine if workspace is large enough for blocked code 
				//* 
				
				if (_6fnxzlyp < ((_dxpq0xkr * _f7059815) + _z68w9sjm))
				{
					//* 
					//*              Not enough workspace to use optimal NB:  determine the 
					//*              minimum value of NB, and reduce NB or force use of 
					//*              unblocked code 
					//* 
					
					_o80jnixx = ILNumerics.F2NET.Intrinsics.MAX((int)2 ,_4mvd6e4d(ref Unsafe.AsRef((int)2) ,"DGEHRD" ," " ,ref _dxpq0xkr ,ref _pew3blan ,ref _9c1csucx ,ref Unsafe.AsRef((int)-1) ) );
					if (_6fnxzlyp >= ((_dxpq0xkr * _o80jnixx) + _z68w9sjm))
					{
						
						_f7059815 = ((_6fnxzlyp - _z68w9sjm) / _dxpq0xkr);
					}
					else
					{
						
						_f7059815 = (int)1;
					}
					
				}
				
			}
			
		}
		
		_iykhdriq = _dxpq0xkr;//* 
		
		if ((_f7059815 < _o80jnixx) | (_f7059815 >= _aym8a085))
		{
			//* 
			//*        Use unblocked code below 
			//* 
			
			_b5p6od9s = _pew3blan;//* 
			
		}
		else
		{
			//* 
			//*        Use blocked code 
			//* 
			
			_y6qf7ne7 = ((int)1 + (_dxpq0xkr * _f7059815));
			{
				System.Int32 __81fgg2dlsvn2191 = (System.Int32)(_pew3blan);
				System.Int32 __81fgg2step2191 = (System.Int32)(_f7059815);
				System.Int32 __81fgg2count2191;
				for (__81fgg2count2191 = System.Math.Max(0, (System.Int32)(((System.Int32)((_9c1csucx - (int)1) - _rtlyoyz3) - __81fgg2dlsvn2191 + __81fgg2step2191) / __81fgg2step2191)), _b5p6od9s = __81fgg2dlsvn2191; __81fgg2count2191 != 0; __81fgg2count2191--, _b5p6od9s += (__81fgg2step2191)) {

				{
					
					_vyr1z1si = ILNumerics.F2NET.Intrinsics.MIN(_f7059815 ,_9c1csucx - _b5p6od9s );//* 
					//*           Reduce columns i:i+ib-1 to Hessenberg form, returning the 
					//*           matrices V and T of the block reflector H = I - V*T*V**T 
					//*           which performs the reduction, and also the matrix Y = A*V*T 
					//* 
					
					_n9zq1867(ref _9c1csucx ,ref _b5p6od9s ,ref _vyr1z1si ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_0446f4de+(_b5p6od9s - 1)),(_apig8meb+(_y6qf7ne7 - 1)),ref Unsafe.AsRef(_w8yhbr2r) ,_apig8meb ,ref _iykhdriq );//* 
					//*           Apply the block reflector H to A(1:ihi,i+ib:ihi) from the 
					//*           right, computing  A := A - Y * V**T. V(i+ib,ib-1) must be set 
					//*           to 1 
					//* 
					
					_85pvcc5e = *(_vxfgpup9+(_b5p6od9s + _vyr1z1si - 1) + ((_b5p6od9s + _vyr1z1si) - (int)1 - 1) * 1 * (_ocv8fk5c));
					*(_vxfgpup9+(_b5p6od9s + _vyr1z1si - 1) + ((_b5p6od9s + _vyr1z1si) - (int)1 - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;
					_5nsxi69c("No transpose" ,"Transpose" ,ref _9c1csucx ,ref Unsafe.AsRef(((_9c1csucx - _b5p6od9s) - _vyr1z1si) + (int)1) ,ref _vyr1z1si ,ref Unsafe.AsRef(-(_kxg5drh2)) ,_apig8meb ,ref _iykhdriq ,(_vxfgpup9+(_b5p6od9s + _vyr1z1si - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s + _vyr1z1si - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
					*(_vxfgpup9+(_b5p6od9s + _vyr1z1si - 1) + ((_b5p6od9s + _vyr1z1si) - (int)1 - 1) * 1 * (_ocv8fk5c)) = _85pvcc5e;//* 
					//*           Apply the block reflector H to A(1:i,i+1:i+ib-1) from the 
					//*           right 
					//* 
					
					_birntqim("Right" ,"Lower" ,"Transpose" ,"Unit" ,ref _b5p6od9s ,ref Unsafe.AsRef(_vyr1z1si - (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_apig8meb ,ref _iykhdriq );
					{
						System.Int32 __81fgg2dlsvn2192 = (System.Int32)((int)0);
						const System.Int32 __81fgg2step2192 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2192;
						for (__81fgg2count2192 = System.Math.Max(0, (System.Int32)(((System.Int32)(_vyr1z1si - (int)2) - __81fgg2dlsvn2192 + __81fgg2step2192) / __81fgg2step2192)), _znpjgsef = __81fgg2dlsvn2192; __81fgg2count2192 != 0; __81fgg2count2192--, _znpjgsef += (__81fgg2step2192)) {

						{
							
							_3czdkijd(ref _b5p6od9s ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_apig8meb+((_iykhdriq * _znpjgsef) + (int)1 - 1)),ref Unsafe.AsRef((int)1) ,(_vxfgpup9+((int)1 - 1) + ((_b5p6od9s + _znpjgsef) + (int)1 - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
Mark30:;
							// continue
						}
												}					}//* 
					//*           Apply the block reflector H to A(i+1:ihi,i+ib:n) from the 
					//*           left 
					//* 
					
					_s5p1x6x6("Left" ,"Transpose" ,"Forward" ,"Columnwise" ,ref Unsafe.AsRef(_9c1csucx - _b5p6od9s) ,ref Unsafe.AsRef(((_dxpq0xkr - _b5p6od9s) - _vyr1z1si) + (int)1) ,ref _vyr1z1si ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_apig8meb+(_y6qf7ne7 - 1)),ref Unsafe.AsRef(_w8yhbr2r) ,(_vxfgpup9+(_b5p6od9s + (int)1 - 1) + (_b5p6od9s + _vyr1z1si - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_apig8meb ,ref _iykhdriq );
Mark40:;
					// continue
				}
								}			}
		}
		//* 
		//*     Use unblocked code to reduce the rest of the matrix 
		//* 
		
		_34zctg7l(ref _dxpq0xkr ,ref _b5p6od9s ,ref _9c1csucx ,_vxfgpup9 ,ref _ocv8fk5c ,_0446f4de ,_apig8meb ,ref _itfnbz60 );
		*(_apig8meb+((int)1 - 1)) = DBLE(_e4ueamrn);//* 
		
		return;//* 
		//*     End of DGEHRD 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
