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
//*> \brief \b SGEQP3 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SGEQP3 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/sgeqp3.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/sgeqp3.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/sgeqp3.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SGEQP3( M, N, A, LDA, JPVT, TAU, WORK, LWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, LDA, LWORK, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            JPVT( * ) 
//*       REAL               A( LDA, * ), TAU( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SGEQP3 computes a QR factorization with column pivoting of a 
//*> matrix A:  A*P = Q*R  using Level 3 BLAS. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of rows of the matrix A. M >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of columns of the matrix A.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is REAL array, dimension (LDA,N) 
//*>          On entry, the M-by-N matrix A. 
//*>          On exit, the upper triangle of the array contains the 
//*>          min(M,N)-by-N upper trapezoidal matrix R; the elements below 
//*>          the diagonal, together with the array TAU, represent the 
//*>          orthogonal matrix Q as a product of min(M,N) elementary 
//*>          reflectors. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A. LDA >= max(1,M). 
//*> \endverbatim 
//*> 
//*> \param[in,out] JPVT 
//*> \verbatim 
//*>          JPVT is INTEGER array, dimension (N) 
//*>          On entry, if JPVT(J).ne.0, the J-th column of A is permuted 
//*>          to the front of A*P (a leading column); if JPVT(J)=0, 
//*>          the J-th column of A is a free column. 
//*>          On exit, if JPVT(J)=K, then the J-th column of A*P was the 
//*>          the K-th column of A. 
//*> \endverbatim 
//*> 
//*> \param[out] TAU 
//*> \verbatim 
//*>          TAU is REAL array, dimension (min(M,N)) 
//*>          The scalar factors of the elementary reflectors. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is REAL array, dimension (MAX(1,LWORK)) 
//*>          On exit, if INFO=0, WORK(1) returns the optimal LWORK. 
//*> \endverbatim 
//*> 
//*> \param[in] LWORK 
//*> \verbatim 
//*>          LWORK is INTEGER 
//*>          The dimension of the array WORK. LWORK >= 3*N+1. 
//*>          For optimal performance LWORK >= 2*N+( N+1 )*NB, where NB 
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
//*>          = 0: successful exit. 
//*>          < 0: if INFO = -i, the i-th argument had an illegal value. 
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
//*> \ingroup realGEcomputational 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  The matrix Q is represented as a product of elementary reflectors 
//*> 
//*>     Q = H(1) H(2) . . . H(k), where k = min(m,n). 
//*> 
//*>  Each H(i) has the form 
//*> 
//*>     H(i) = I - tau * v * v**T 
//*> 
//*>  where tau is a real scalar, and v is a real/complex vector 
//*>  with v(1:i-1) = 0 and v(i) = 1; v(i+1:m) is stored on exit in 
//*>  A(i+1:m,i), and tau in TAU(i). 
//*> \endverbatim 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>    G. Quintana-Orti, Depto. de Informatica, Universidad Jaime I, Spain 
//*>    X. Sun, Computer Science Dept., Duke University, USA 
//*> 
//*  ===================================================================== 

	 
	public static void _dpoj3pi6(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, Single* _vxfgpup9, ref Int32 _ocv8fk5c, Int32* _laipxa7w, Single* _0446f4de, Single* _apig8meb, ref Int32 _6fnxzlyp, ref Int32 _gro5yvfo)
	{
#region variable declarations
Int32 _yi0d901l =  (int)1;
Int32 _fx3jmukq =  (int)2;
Int32 _hx39kbqy =  (int)3;
Boolean _lhlgm7z5 =  default;
Int32 _y0i7lfaw =  default;
Int32 _7tvbphxm =  default;
Int32 _znpjgsef =  default;
Int32 _pscq8l5q =  default;
Int32 _e4ueamrn =  default;
Int32 _qaseb1y7 =  default;
Int32 _x7jab39s =  default;
Int32 _fopltai1 =  default;
Int32 _f7059815 =  default;
Int32 _o80jnixx =  default;
Int32 _z1iah8lx =  default;
Int32 _rtlyoyz3 =  default;
Int32 _2bpjs4aa =  default;
Int32 _xhvokkpn =  default;
Int32 _8tmd0ner =  default;
Int32 _yk36veex =  default;
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
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     Test input arguments 
		//*  ==================== 
		//* 
		
		_gro5yvfo = (int)0;
		_lhlgm7z5 = (_6fnxzlyp == (int)-1);
		if (_ev4xhht5 < (int)0)
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ))
		{
			
			_gro5yvfo = (int)-4;
		}
		//* 
		
		if (_gro5yvfo == (int)0)
		{
			
			_qaseb1y7 = ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_dxpq0xkr );
			if (_qaseb1y7 == (int)0)
			{
				
				_7tvbphxm = (int)1;
				_e4ueamrn = (int)1;
			}
			else
			{
				
				_7tvbphxm = (((int)3 * _dxpq0xkr) + (int)1);
				_f7059815 = _4mvd6e4d(ref Unsafe.AsRef(_yi0d901l) ,"SGEQRF" ," " ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) );
				_e4ueamrn = (((int)2 * _dxpq0xkr) + ((_dxpq0xkr + (int)1) * _f7059815));
			}
			
			*(_apig8meb+((int)1 - 1)) = REAL(_e4ueamrn);//* 
			
			if ((_6fnxzlyp < _7tvbphxm) & (!(_lhlgm7z5)))
			{
				
				_gro5yvfo = (int)-8;
			}
			
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("SGEQP3" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		else
		if (_lhlgm7z5)
		{
			
			return;
		}
		//* 
		//*     Move initial columns up front. 
		//* 
		
		_z1iah8lx = (int)1;
		{
			System.Int32 __81fgg2dlsvn1830 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1830 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1830;
			for (__81fgg2count1830 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1830 + __81fgg2step1830) / __81fgg2step1830)), _znpjgsef = __81fgg2dlsvn1830; __81fgg2count1830 != 0; __81fgg2count1830--, _znpjgsef += (__81fgg2step1830)) {

			{
				
				if (*(_laipxa7w+(_znpjgsef - 1)) != (int)0)
				{
					
					if (_znpjgsef != _z1iah8lx)
					{
						
						_ahhuglvd(ref _ev4xhht5 ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,(_vxfgpup9+((int)1 - 1) + (_z1iah8lx - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
						*(_laipxa7w+(_znpjgsef - 1)) = *(_laipxa7w+(_z1iah8lx - 1));
						*(_laipxa7w+(_z1iah8lx - 1)) = _znpjgsef;
					}
					else
					{
						
						*(_laipxa7w+(_znpjgsef - 1)) = _znpjgsef;
					}
					
					_z1iah8lx = (_z1iah8lx + (int)1);
				}
				else
				{
					
					*(_laipxa7w+(_znpjgsef - 1)) = _znpjgsef;
				}
				
Mark10:;
				// continue
			}
						}		}
		_z1iah8lx = (_z1iah8lx - (int)1);//* 
		//*     Factorize fixed columns 
		//*  ======================= 
		//* 
		//*     Compute the QR factorization of fixed columns and update 
		//*     remaining columns. 
		//* 
		
		if (_z1iah8lx > (int)0)
		{
			
			_fopltai1 = ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_z1iah8lx );//*CC      CALL SGEQR2( M, NA, A, LDA, TAU, WORK, INFO ) 
			
			_egefx4n9(ref _ev4xhht5 ,ref _fopltai1 ,_vxfgpup9 ,ref _ocv8fk5c ,_0446f4de ,_apig8meb ,ref _6fnxzlyp ,ref _gro5yvfo );
			_7tvbphxm = ILNumerics.F2NET.Intrinsics.MAX(_7tvbphxm ,ILNumerics.F2NET.Intrinsics.INT(*(_apig8meb+((int)1 - 1)) ) );
			if (_fopltai1 < _dxpq0xkr)
			{
				//*CC         CALL SORM2R( 'Left', 'Transpose', M, N-NA, NA, A, LDA, 
				//*CC  $                   TAU, A( 1, NA+1 ), LDA, WORK, INFO ) 
				
				_yzo7hbs3("Left" ,"Transpose" ,ref _ev4xhht5 ,ref Unsafe.AsRef(_dxpq0xkr - _fopltai1) ,ref _fopltai1 ,_vxfgpup9 ,ref _ocv8fk5c ,_0446f4de ,(_vxfgpup9+((int)1 - 1) + (_fopltai1 + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_apig8meb ,ref _6fnxzlyp ,ref _gro5yvfo );
				_7tvbphxm = ILNumerics.F2NET.Intrinsics.MAX(_7tvbphxm ,ILNumerics.F2NET.Intrinsics.INT(*(_apig8meb+((int)1 - 1)) ) );
			}
			
		}
		//* 
		//*     Factorize free columns 
		//*  ====================== 
		//* 
		
		if (_z1iah8lx < _qaseb1y7)
		{
			//* 
			
			_2bpjs4aa = (_ev4xhht5 - _z1iah8lx);
			_8tmd0ner = (_dxpq0xkr - _z1iah8lx);
			_xhvokkpn = (_qaseb1y7 - _z1iah8lx);//* 
			//*        Determine the block size. 
			//* 
			
			_f7059815 = _4mvd6e4d(ref Unsafe.AsRef(_yi0d901l) ,"SGEQRF" ," " ,ref _2bpjs4aa ,ref _8tmd0ner ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) );
			_o80jnixx = (int)2;
			_rtlyoyz3 = (int)0;//* 
			
			if ((_f7059815 > (int)1) & (_f7059815 < _xhvokkpn))
			{
				//* 
				//*           Determine when to cross over from blocked to unblocked code. 
				//* 
				
				_rtlyoyz3 = ILNumerics.F2NET.Intrinsics.MAX((int)0 ,_4mvd6e4d(ref Unsafe.AsRef(_hx39kbqy) ,"SGEQRF" ," " ,ref _2bpjs4aa ,ref _8tmd0ner ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ) );//* 
				//* 
				
				if (_rtlyoyz3 < _xhvokkpn)
				{
					//* 
					//*              Determine if workspace is large enough for blocked code. 
					//* 
					
					_x7jab39s = (((int)2 * _8tmd0ner) + ((_8tmd0ner + (int)1) * _f7059815));
					_7tvbphxm = ILNumerics.F2NET.Intrinsics.MAX(_7tvbphxm ,_x7jab39s );
					if (_6fnxzlyp < _x7jab39s)
					{
						//* 
						//*                 Not enough workspace to use optimal NB: Reduce NB and 
						//*                 determine the minimum value of NB. 
						//* 
						
						_f7059815 = ((_6fnxzlyp - ((int)2 * _8tmd0ner)) / (_8tmd0ner + (int)1));
						_o80jnixx = ILNumerics.F2NET.Intrinsics.MAX((int)2 ,_4mvd6e4d(ref Unsafe.AsRef(_fx3jmukq) ,"SGEQRF" ," " ,ref _2bpjs4aa ,ref _8tmd0ner ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ) );//* 
						//* 
						
					}
					
				}
				
			}
			//* 
			//*        Initialize partial column norms. The first N elements of work 
			//*        store the exact column norms. 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn1831 = (System.Int32)((_z1iah8lx + (int)1));
				const System.Int32 __81fgg2step1831 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1831;
				for (__81fgg2count1831 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1831 + __81fgg2step1831) / __81fgg2step1831)), _znpjgsef = __81fgg2dlsvn1831; __81fgg2count1831 != 0; __81fgg2count1831--, _znpjgsef += (__81fgg2step1831)) {

				{
					
					*(_apig8meb+(_znpjgsef - 1)) = _z20xbrro(ref _2bpjs4aa ,(_vxfgpup9+(_z1iah8lx + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
					*(_apig8meb+(_dxpq0xkr + _znpjgsef - 1)) = *(_apig8meb+(_znpjgsef - 1));
Mark20:;
					// continue
				}
								}			}//* 
			
			if (((_f7059815 >= _o80jnixx) & (_f7059815 < _xhvokkpn)) & (_rtlyoyz3 < _xhvokkpn))
			{
				//* 
				//*           Use blocked code initially. 
				//* 
				
				_znpjgsef = (_z1iah8lx + (int)1);//* 
				//*           Compute factorization: while loop. 
				//* 
				//* 
				
				_yk36veex = (_qaseb1y7 - _rtlyoyz3);
Mark30:;
				// continue
				if (_znpjgsef <= _yk36veex)
				{
					
					_pscq8l5q = ILNumerics.F2NET.Intrinsics.MIN(_f7059815 ,(_yk36veex - _znpjgsef) + (int)1 );//* 
					//*              Factorize JB columns among columns J:N. 
					//* 
					
					_rigi67i2(ref _ev4xhht5 ,ref Unsafe.AsRef((_dxpq0xkr - _znpjgsef) + (int)1) ,ref Unsafe.AsRef(_znpjgsef - (int)1) ,ref _pscq8l5q ,ref _y0i7lfaw ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_laipxa7w+(_znpjgsef - 1)),(_0446f4de+(_znpjgsef - 1)),(_apig8meb+(_znpjgsef - 1)),(_apig8meb+(_dxpq0xkr + _znpjgsef - 1)),(_apig8meb+(((int)2 * _dxpq0xkr) + (int)1 - 1)),(_apig8meb+((((int)2 * _dxpq0xkr) + _pscq8l5q) + (int)1 - 1)),ref Unsafe.AsRef((_dxpq0xkr - _znpjgsef) + (int)1) );//* 
					
					_znpjgsef = (_znpjgsef + _y0i7lfaw);goto Mark30;
				}
				
			}
			else
			{
				
				_znpjgsef = (_z1iah8lx + (int)1);
			}
			//* 
			//*        Use unblocked code to factor the last or only block. 
			//* 
			//* 
			
			if (_znpjgsef <= _qaseb1y7)
			_o9a756wi(ref _ev4xhht5 ,ref Unsafe.AsRef((_dxpq0xkr - _znpjgsef) + (int)1) ,ref Unsafe.AsRef(_znpjgsef - (int)1) ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_laipxa7w+(_znpjgsef - 1)),(_0446f4de+(_znpjgsef - 1)),(_apig8meb+(_znpjgsef - 1)),(_apig8meb+(_dxpq0xkr + _znpjgsef - 1)),(_apig8meb+(((int)2 * _dxpq0xkr) + (int)1 - 1)));//* 
			
		}
		//* 
		
		*(_apig8meb+((int)1 - 1)) = REAL(_7tvbphxm);
		return;//* 
		//*     End of SGEQP3 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
