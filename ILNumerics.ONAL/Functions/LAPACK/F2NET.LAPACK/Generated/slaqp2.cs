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
//*> \brief \b SLAQP2 computes a QR factorization with column pivoting of the matrix block. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLAQP2 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slaqp2.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slaqp2.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slaqp2.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLAQP2( M, N, OFFSET, A, LDA, JPVT, TAU, VN1, VN2, 
//*                          WORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            LDA, M, N, OFFSET 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            JPVT( * ) 
//*       REAL               A( LDA, * ), TAU( * ), VN1( * ), VN2( * ), 
//*      $                   WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLAQP2 computes a QR factorization with column pivoting of 
//*> the block A(OFFSET+1:M,1:N). 
//*> The block A(1:OFFSET,1:N) is accordingly pivoted, but not factorized. 
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
//*>          The number of columns of the matrix A. N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] OFFSET 
//*> \verbatim 
//*>          OFFSET is INTEGER 
//*>          The number of rows of the matrix A that must be pivoted 
//*>          but no factorized. OFFSET >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is REAL array, dimension (LDA,N) 
//*>          On entry, the M-by-N matrix A. 
//*>          On exit, the upper triangle of block A(OFFSET+1:M,1:N) is 
//*>          the triangular factor obtained; the elements in block 
//*>          A(OFFSET+1:M,1:N) below the diagonal, together with the 
//*>          array TAU, represent the orthogonal matrix Q as a product of 
//*>          elementary reflectors. Block A(1:OFFSET,1:N) has been 
//*>          accordingly pivoted, but no factorized. 
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
//*>          On entry, if JPVT(i) .ne. 0, the i-th column of A is permuted 
//*>          to the front of A*P (a leading column); if JPVT(i) = 0, 
//*>          the i-th column of A is a free column. 
//*>          On exit, if JPVT(i) = k, then the i-th column of A*P 
//*>          was the k-th column of A. 
//*> \endverbatim 
//*> 
//*> \param[out] TAU 
//*> \verbatim 
//*>          TAU is REAL array, dimension (min(M,N)) 
//*>          The scalar factors of the elementary reflectors. 
//*> \endverbatim 
//*> 
//*> \param[in,out] VN1 
//*> \verbatim 
//*>          VN1 is REAL array, dimension (N) 
//*>          The vector with the partial column norms. 
//*> \endverbatim 
//*> 
//*> \param[in,out] VN2 
//*> \verbatim 
//*>          VN2 is REAL array, dimension (N) 
//*>          The vector with the exact column norms. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is REAL array, dimension (N) 
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
//*> \ingroup realOTHERauxiliary 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>    G. Quintana-Orti, Depto. de Informatica, Universidad Jaime I, Spain 
//*>    X. Sun, Computer Science Dept., Duke University, USA 
//*> \n 
//*>  Partial column norm updating strategy modified on April 2011 
//*>    Z. Drmac and Z. Bujanovic, Dept. of Mathematics, 
//*>    University of Zagreb, Croatia. 
//* 
//*> \par References: 
//*  ================ 
//*> 
//*> LAPACK Working Note 176 
//* 
//*> \htmlonly 
//*> <a href="http://www.netlib.org/lapack/lawnspdf/lawn176.pdf">[PDF]</a> 
//*> \endhtmlonly 
//* 
//*  ===================================================================== 

	 
	public static void _o9a756wi(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _1l9k9q9k, Single* _vxfgpup9, ref Int32 _ocv8fk5c, Int32* _laipxa7w, Single* _0446f4de, Single* _noxmp3qo, Single* _m4m6epbx, Single* _apig8meb)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
Int32 _b5p6od9s =  default;
Int32 _m1gysdbg =  default;
Int32 _znpjgsef =  default;
Int32 _0hik27x4 =  default;
Int32 _620cwrex =  default;
Int32 _153xco97 =  default;
Single _qmqa6kps =  default;
Single _1ajfmh55 =  default;
Single _q3ig7mub =  default;
Single _krshq2hy =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.0) -- 
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
		
		_0hik27x4 = ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 - _1l9k9q9k ,_dxpq0xkr );
		_krshq2hy = ILNumerics.F2NET.Intrinsics.SQRT(_d5tu038y("Epsilon" ) );//* 
		//*     Compute factorization. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn1832 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step1832 = (System.Int32)((int)1);
			System.Int32 __81fgg2count1832;
			for (__81fgg2count1832 = System.Math.Max(0, (System.Int32)(((System.Int32)(_0hik27x4) - __81fgg2dlsvn1832 + __81fgg2step1832) / __81fgg2step1832)), _b5p6od9s = __81fgg2dlsvn1832; __81fgg2count1832 != 0; __81fgg2count1832--, _b5p6od9s += (__81fgg2step1832)) {

			{
				//* 
				
				_620cwrex = (_1l9k9q9k + _b5p6od9s);//* 
				//*        Determine ith pivot column and swap if necessary. 
				//* 
				
				_153xco97 = ((_b5p6od9s - (int)1) + _z5b2nqbf(ref Unsafe.AsRef((_dxpq0xkr - _b5p6od9s) + (int)1) ,(_noxmp3qo+(_b5p6od9s - 1)),ref Unsafe.AsRef((int)1) ));//* 
				
				if (_153xco97 != _b5p6od9s)
				{
					
					_ahhuglvd(ref _ev4xhht5 ,(_vxfgpup9+((int)1 - 1) + (_153xco97 - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
					_m1gysdbg = *(_laipxa7w+(_153xco97 - 1));
					*(_laipxa7w+(_153xco97 - 1)) = *(_laipxa7w+(_b5p6od9s - 1));
					*(_laipxa7w+(_b5p6od9s - 1)) = _m1gysdbg;
					*(_noxmp3qo+(_153xco97 - 1)) = *(_noxmp3qo+(_b5p6od9s - 1));
					*(_m4m6epbx+(_153xco97 - 1)) = *(_m4m6epbx+(_b5p6od9s - 1));
				}
				//* 
				//*        Generate elementary reflector H(i). 
				//* 
				
				if (_620cwrex < _ev4xhht5)
				{
					
					_mbabw0s0(ref Unsafe.AsRef((_ev4xhht5 - _620cwrex) + (int)1) ,ref Unsafe.AsRef(*(_vxfgpup9+(_620cwrex - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))) ,(_vxfgpup9+(_620cwrex + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_0446f4de+(_b5p6od9s - 1))) );
				}
				else
				{
					
					_mbabw0s0(ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_vxfgpup9+(_ev4xhht5 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))) ,(_vxfgpup9+(_ev4xhht5 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_0446f4de+(_b5p6od9s - 1))) );
				}
				//* 
				
				if (_b5p6od9s < _dxpq0xkr)
				{
					//* 
					//*           Apply H(i)**T to A(offset+i:m,i+1:n) from the left. 
					//* 
					
					_qmqa6kps = *(_vxfgpup9+(_620cwrex - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c));
					*(_vxfgpup9+(_620cwrex - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;
					_tfywat2m("Left" ,ref Unsafe.AsRef((_ev4xhht5 - _620cwrex) + (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - _b5p6od9s) ,(_vxfgpup9+(_620cwrex - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_0446f4de+(_b5p6od9s - 1))) ,(_vxfgpup9+(_620cwrex - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_apig8meb+((int)1 - 1)));
					*(_vxfgpup9+(_620cwrex - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _qmqa6kps;
				}
				//* 
				//*        Update partial column norms. 
				//* 
				
				{
					System.Int32 __81fgg2dlsvn1833 = (System.Int32)((_b5p6od9s + (int)1));
					const System.Int32 __81fgg2step1833 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1833;
					for (__81fgg2count1833 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1833 + __81fgg2step1833) / __81fgg2step1833)), _znpjgsef = __81fgg2dlsvn1833; __81fgg2count1833 != 0; __81fgg2count1833--, _znpjgsef += (__81fgg2step1833)) {

					{
						
						if (*(_noxmp3qo+(_znpjgsef - 1)) != _d0547bi2)
						{
							//* 
							//*              NOTE: The following 4 lines follow from the analysis in 
							//*              Lapack Working Note 176. 
							//* 
							
							_1ajfmh55 = (_kxg5drh2 - __POW2((ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_620cwrex - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) / *(_noxmp3qo+(_znpjgsef - 1)))));
							_1ajfmh55 = ILNumerics.F2NET.Intrinsics.MAX(_1ajfmh55 ,_d0547bi2 );
							_q3ig7mub = (_1ajfmh55 * __POW2((*(_noxmp3qo+(_znpjgsef - 1)) / *(_m4m6epbx+(_znpjgsef - 1)))));
							if (_q3ig7mub <= _krshq2hy)
							{
								
								if (_620cwrex < _ev4xhht5)
								{
									
									*(_noxmp3qo+(_znpjgsef - 1)) = _z20xbrro(ref Unsafe.AsRef(_ev4xhht5 - _620cwrex) ,(_vxfgpup9+(_620cwrex + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
									*(_m4m6epbx+(_znpjgsef - 1)) = *(_noxmp3qo+(_znpjgsef - 1));
								}
								else
								{
									
									*(_noxmp3qo+(_znpjgsef - 1)) = _d0547bi2;
									*(_m4m6epbx+(_znpjgsef - 1)) = _d0547bi2;
								}
								
							}
							else
							{
								
								*(_noxmp3qo+(_znpjgsef - 1)) = (*(_noxmp3qo+(_znpjgsef - 1)) * ILNumerics.F2NET.Intrinsics.SQRT(_1ajfmh55 ));
							}
							
						}
						
Mark10:;
						// continue
					}
										}				}//* 
				
Mark20:;
				// continue
			}
						}		}//* 
		
		return;//* 
		//*     End of SLAQP2 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
