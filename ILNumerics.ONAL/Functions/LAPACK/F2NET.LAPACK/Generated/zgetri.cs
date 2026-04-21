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
//*> \brief \b ZGETRI 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZGETRI + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zgetri.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zgetri.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zgetri.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZGETRI( N, A, LDA, IPIV, WORK, LWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, LDA, LWORK, N 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IPIV( * ) 
//*       COMPLEX*16         A( LDA, * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZGETRI computes the inverse of a matrix using the LU factorization 
//*> computed by ZGETRF. 
//*> 
//*> This method inverts U and then computes inv(A) by solving the system 
//*> inv(A)*L = inv(U) for inv(A). 
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
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension (LDA,N) 
//*>          On entry, the factors L and U from the factorization 
//*>          A = P*L*U as computed by ZGETRF. 
//*>          On exit, if INFO = 0, the inverse of the original matrix A. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[in] IPIV 
//*> \verbatim 
//*>          IPIV is INTEGER array, dimension (N) 
//*>          The pivot indices from ZGETRF; for 1<=i<=N, row i of the 
//*>          matrix was interchanged with row IPIV(i). 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is COMPLEX*16 array, dimension (MAX(1,LWORK)) 
//*>          On exit, if INFO=0, then WORK(1) returns the optimal LWORK. 
//*> \endverbatim 
//*> 
//*> \param[in] LWORK 
//*> \verbatim 
//*>          LWORK is INTEGER 
//*>          The dimension of the array WORK.  LWORK >= max(1,N). 
//*>          For optimal performance LWORK >= N*NB, where NB is 
//*>          the optimal blocksize returned by ILAENV. 
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
//*>          > 0:  if INFO = i, U(i,i) is exactly zero; the matrix is 
//*>                singular and its inverse could not be computed. 
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
//*> \ingroup complex16GEcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _3umidwpd(ref Int32 _dxpq0xkr, complex* _vxfgpup9, ref Int32 _ocv8fk5c, Int32* _w1ilvusp, complex* _apig8meb, ref Int32 _6fnxzlyp, ref Int32 _gro5yvfo)
	{
#region variable declarations
complex _d0547bi2 =   new fcomplex(0f,0f);
complex _kxg5drh2 =   new fcomplex(1f,0f);
Boolean _lhlgm7z5 =  default;
Int32 _b5p6od9s =  default;
Int32 _7tvbphxm =  default;
Int32 _znpjgsef =  default;
Int32 _pscq8l5q =  default;
Int32 _j7zu8nju =  default;
Int32 _c2zk3fjj =  default;
Int32 _iykhdriq =  default;
Int32 _e4ueamrn =  default;
Int32 _f7059815 =  default;
Int32 _o80jnixx =  default;
Int32 _8dgyhtzt =  default;
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
		//*     .. External Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input parameters. 
		//* 
		
		_gro5yvfo = (int)0;
		_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZGETRI" ," " ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) );
		_e4ueamrn = (_dxpq0xkr * _f7059815);
		*(_apig8meb+((int)1 - 1)) = DCMPLX(_e4ueamrn);
		_lhlgm7z5 = (_6fnxzlyp == (int)-1);
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if ((_6fnxzlyp < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr )) & (!(_lhlgm7z5)))
		{
			
			_gro5yvfo = (int)-6;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("ZGETRI" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
		
		if (_dxpq0xkr == (int)0)
		return;//* 
		//*     Form inv(U).  If INFO > 0 from ZTRTRI, then U is singular, 
		//*     and the inverse is not computed. 
		//* 
		
		_3gvzsp7u("Upper" ,"Non-unit" ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,ref _gro5yvfo );
		if (_gro5yvfo > (int)0)
		return;//* 
		
		_o80jnixx = (int)2;
		_iykhdriq = _dxpq0xkr;
		if ((_f7059815 > (int)1) & (_f7059815 < _dxpq0xkr))
		{
			
			_7tvbphxm = ILNumerics.F2NET.Intrinsics.MAX(_iykhdriq * _f7059815 ,(int)1 );
			if (_6fnxzlyp < _7tvbphxm)
			{
				
				_f7059815 = (_6fnxzlyp / _iykhdriq);
				_o80jnixx = ILNumerics.F2NET.Intrinsics.MAX((int)2 ,_4mvd6e4d(ref Unsafe.AsRef((int)2) ,"ZGETRI" ," " ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ) );
			}
			
		}
		else
		{
			
			_7tvbphxm = _dxpq0xkr;
		}
		//* 
		//*     Solve the equation inv(A)*L = inv(U) for inv(A). 
		//* 
		
		if ((_f7059815 < _o80jnixx) | (_f7059815 >= _dxpq0xkr))
		{
			//* 
			//*        Use unblocked code. 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn1818 = (System.Int32)(_dxpq0xkr);
				System.Int32 __81fgg2step1818 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count1818;
				for (__81fgg2count1818 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1818 + __81fgg2step1818) / __81fgg2step1818)), _znpjgsef = __81fgg2dlsvn1818; __81fgg2count1818 != 0; __81fgg2count1818--, _znpjgsef += (__81fgg2step1818)) {

				{
					//* 
					//*           Copy current column of L to WORK and replace with zeros. 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn1819 = (System.Int32)((_znpjgsef + (int)1));
						const System.Int32 __81fgg2step1819 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1819;
						for (__81fgg2count1819 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1819 + __81fgg2step1819) / __81fgg2step1819)), _b5p6od9s = __81fgg2dlsvn1819; __81fgg2count1819 != 0; __81fgg2count1819--, _b5p6od9s += (__81fgg2step1819)) {

						{
							
							*(_apig8meb+(_b5p6od9s - 1)) = *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c));
							*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
Mark10:;
							// continue
						}
												}					}//* 
					//*           Compute current column of inv(A). 
					//* 
					
					if (_znpjgsef < _dxpq0xkr)
					_xfaqgfxk("No transpose" ,ref _dxpq0xkr ,ref Unsafe.AsRef(_dxpq0xkr - _znpjgsef) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_apig8meb+(_znpjgsef + (int)1 - 1)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
Mark20:;
					// continue
				}
								}			}
		}
		else
		{
			//* 
			//*        Use blocked code. 
			//* 
			
			_8dgyhtzt = ((((_dxpq0xkr - (int)1) / _f7059815) * _f7059815) + (int)1);
			{
				System.Int32 __81fgg2dlsvn1820 = (System.Int32)(_8dgyhtzt);
				System.Int32 __81fgg2step1820 = (System.Int32)(-(_f7059815));
				System.Int32 __81fgg2count1820;
				for (__81fgg2count1820 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1820 + __81fgg2step1820) / __81fgg2step1820)), _znpjgsef = __81fgg2dlsvn1820; __81fgg2count1820 != 0; __81fgg2count1820--, _znpjgsef += (__81fgg2step1820)) {

				{
					
					_pscq8l5q = ILNumerics.F2NET.Intrinsics.MIN(_f7059815 ,(_dxpq0xkr - _znpjgsef) + (int)1 );//* 
					//*           Copy current block column of L to WORK and replace with 
					//*           zeros. 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn1821 = (System.Int32)(_znpjgsef);
						const System.Int32 __81fgg2step1821 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1821;
						for (__81fgg2count1821 = System.Math.Max(0, (System.Int32)(((System.Int32)((_znpjgsef + _pscq8l5q) - (int)1) - __81fgg2dlsvn1821 + __81fgg2step1821) / __81fgg2step1821)), _j7zu8nju = __81fgg2dlsvn1821; __81fgg2count1821 != 0; __81fgg2count1821--, _j7zu8nju += (__81fgg2step1821)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1822 = (System.Int32)((_j7zu8nju + (int)1));
								const System.Int32 __81fgg2step1822 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1822;
								for (__81fgg2count1822 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1822 + __81fgg2step1822) / __81fgg2step1822)), _b5p6od9s = __81fgg2dlsvn1822; __81fgg2count1822 != 0; __81fgg2count1822--, _b5p6od9s += (__81fgg2step1822)) {

								{
									
									*(_apig8meb+(_b5p6od9s + ((_j7zu8nju - _znpjgsef) * _iykhdriq) - 1)) = *(_vxfgpup9+(_b5p6od9s - 1) + (_j7zu8nju - 1) * 1 * (_ocv8fk5c));
									*(_vxfgpup9+(_b5p6od9s - 1) + (_j7zu8nju - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
Mark30:;
									// continue
								}
																}							}
Mark40:;
							// continue
						}
												}					}//* 
					//*           Compute current block column of inv(A). 
					//* 
					
					if ((_znpjgsef + _pscq8l5q) <= _dxpq0xkr)
					_xos1d1er("No transpose" ,"No transpose" ,ref _dxpq0xkr ,ref _pscq8l5q ,ref Unsafe.AsRef(((_dxpq0xkr - _znpjgsef) - _pscq8l5q) + (int)1) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef + _pscq8l5q - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_apig8meb+(_znpjgsef + _pscq8l5q - 1)),ref _iykhdriq ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
					_qlsh8rhv("Right" ,"Lower" ,"No transpose" ,"Unit" ,ref _dxpq0xkr ,ref _pscq8l5q ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+(_znpjgsef - 1)),ref _iykhdriq ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
Mark50:;
					// continue
				}
								}			}
		}
		//* 
		//*     Apply column interchanges. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn1823 = (System.Int32)((_dxpq0xkr - (int)1));
			System.Int32 __81fgg2step1823 = (System.Int32)((int)-1);
			System.Int32 __81fgg2count1823;
			for (__81fgg2count1823 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1823 + __81fgg2step1823) / __81fgg2step1823)), _znpjgsef = __81fgg2dlsvn1823; __81fgg2count1823 != 0; __81fgg2count1823--, _znpjgsef += (__81fgg2step1823)) {

			{
				
				_c2zk3fjj = *(_w1ilvusp+(_znpjgsef - 1));
				if (_c2zk3fjj != _znpjgsef)
				_027ahmgj(ref _dxpq0xkr ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,(_vxfgpup9+((int)1 - 1) + (_c2zk3fjj - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
Mark60:;
				// continue
			}
						}		}//* 
		
		*(_apig8meb+((int)1 - 1)) = DCMPLX(_7tvbphxm);
		return;//* 
		//*     End of ZGETRI 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
