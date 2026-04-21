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
//*> \brief \b CUNGHR 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CUNGHR + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/cunghr.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/cunghr.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/cunghr.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CUNGHR( N, ILO, IHI, A, LDA, TAU, WORK, LWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            IHI, ILO, INFO, LDA, LWORK, N 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX            A( LDA, * ), TAU( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CUNGHR generates a complex unitary matrix Q which is defined as the 
//*> product of IHI-ILO elementary reflectors of order N, as returned by 
//*> CGEHRD: 
//*> 
//*> Q = H(ilo) H(ilo+1) . . . H(ihi-1). 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix Q. N >= 0. 
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
//*>          ILO and IHI must have the same values as in the previous call 
//*>          of CGEHRD. Q is equal to the unit matrix except in the 
//*>          submatrix Q(ilo+1:ihi,ilo+1:ihi). 
//*>          1 <= ILO <= IHI <= N, if N > 0; ILO=1 and IHI=0, if N=0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is COMPLEX array, dimension (LDA,N) 
//*>          On entry, the vectors which define the elementary reflectors, 
//*>          as returned by CGEHRD. 
//*>          On exit, the N-by-N unitary matrix Q. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A. LDA >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[in] TAU 
//*> \verbatim 
//*>          TAU is COMPLEX array, dimension (N-1) 
//*>          TAU(i) must contain the scalar factor of the elementary 
//*>          reflector H(i), as returned by CGEHRD. 
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
//*>          The dimension of the array WORK. LWORK >= IHI-ILO. 
//*>          For optimum performance LWORK >= (IHI-ILO)*NB, where NB is 
//*>          the optimal blocksize. 
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

	 
	public static void _1nlrbmyw(ref Int32 _dxpq0xkr, ref Int32 _pew3blan, ref Int32 _9c1csucx, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, fcomplex* _0446f4de, fcomplex* _apig8meb, ref Int32 _6fnxzlyp, ref Int32 _gro5yvfo)
	{
#region variable declarations
fcomplex _d0547bi2 =   new fcomplex(0f,0f);
fcomplex _kxg5drh2 =   new fcomplex(1f,0f);
Boolean _lhlgm7z5 =  default;
Int32 _b5p6od9s =  default;
Int32 _itfnbz60 =  default;
Int32 _znpjgsef =  default;
Int32 _e4ueamrn =  default;
Int32 _f7059815 =  default;
Int32 _aym8a085 =  default;
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
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input arguments 
		//* 
		
		_gro5yvfo = (int)0;
		_aym8a085 = (_9c1csucx - _pew3blan);
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
		if ((_6fnxzlyp < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_aym8a085 )) & (!(_lhlgm7z5)))
		{
			
			_gro5yvfo = (int)-8;
		}
		//* 
		
		if (_gro5yvfo == (int)0)
		{
			
			_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"CUNGQR" ," " ,ref _aym8a085 ,ref _aym8a085 ,ref _aym8a085 ,ref Unsafe.AsRef((int)-1) );
			_e4ueamrn = (ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_aym8a085 ) * _f7059815);
			*(_apig8meb+((int)1 - 1)) = CMPLX(_e4ueamrn);
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("CUNGHR" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
		{
			
			*(_apig8meb+((int)1 - 1)) = CMPLX((int)1);
			return;
		}
		//* 
		//*     Shift the vectors which define the elementary reflectors one 
		//*     column to the right, and set the first ilo and the last n-ihi 
		//*     rows and columns to those of the unit matrix 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn2647 = (System.Int32)(_9c1csucx);
			System.Int32 __81fgg2step2647 = (System.Int32)((int)-1);
			System.Int32 __81fgg2count2647;
			for (__81fgg2count2647 = System.Math.Max(0, (System.Int32)(((System.Int32)(_pew3blan + (int)1) - __81fgg2dlsvn2647 + __81fgg2step2647) / __81fgg2step2647)), _znpjgsef = __81fgg2dlsvn2647; __81fgg2count2647 != 0; __81fgg2count2647--, _znpjgsef += (__81fgg2step2647)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn2648 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2648 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2648;
					for (__81fgg2count2648 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn2648 + __81fgg2step2648) / __81fgg2step2648)), _b5p6od9s = __81fgg2dlsvn2648; __81fgg2count2648 != 0; __81fgg2count2648--, _b5p6od9s += (__81fgg2step2648)) {

					{
						
						*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
Mark10:;
						// continue
					}
										}				}
				{
					System.Int32 __81fgg2dlsvn2649 = (System.Int32)((_znpjgsef + (int)1));
					const System.Int32 __81fgg2step2649 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2649;
					for (__81fgg2count2649 = System.Math.Max(0, (System.Int32)(((System.Int32)(_9c1csucx) - __81fgg2dlsvn2649 + __81fgg2step2649) / __81fgg2step2649)), _b5p6od9s = __81fgg2dlsvn2649; __81fgg2count2649 != 0; __81fgg2count2649--, _b5p6od9s += (__81fgg2step2649)) {

					{
						
						*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - (int)1 - 1) * 1 * (_ocv8fk5c));
Mark20:;
						// continue
					}
										}				}
				{
					System.Int32 __81fgg2dlsvn2650 = (System.Int32)((_9c1csucx + (int)1));
					const System.Int32 __81fgg2step2650 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2650;
					for (__81fgg2count2650 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2650 + __81fgg2step2650) / __81fgg2step2650)), _b5p6od9s = __81fgg2dlsvn2650; __81fgg2count2650 != 0; __81fgg2count2650--, _b5p6od9s += (__81fgg2step2650)) {

					{
						
						*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
Mark30:;
						// continue
					}
										}				}
Mark40:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn2651 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2651 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2651;
			for (__81fgg2count2651 = System.Math.Max(0, (System.Int32)(((System.Int32)(_pew3blan) - __81fgg2dlsvn2651 + __81fgg2step2651) / __81fgg2step2651)), _znpjgsef = __81fgg2dlsvn2651; __81fgg2count2651 != 0; __81fgg2count2651--, _znpjgsef += (__81fgg2step2651)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn2652 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2652 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2652;
					for (__81fgg2count2652 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2652 + __81fgg2step2652) / __81fgg2step2652)), _b5p6od9s = __81fgg2dlsvn2652; __81fgg2count2652 != 0; __81fgg2count2652--, _b5p6od9s += (__81fgg2step2652)) {

					{
						
						*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
Mark50:;
						// continue
					}
										}				}
				*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;
Mark60:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn2653 = (System.Int32)((_9c1csucx + (int)1));
			const System.Int32 __81fgg2step2653 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2653;
			for (__81fgg2count2653 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2653 + __81fgg2step2653) / __81fgg2step2653)), _znpjgsef = __81fgg2dlsvn2653; __81fgg2count2653 != 0; __81fgg2count2653--, _znpjgsef += (__81fgg2step2653)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn2654 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2654 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2654;
					for (__81fgg2count2654 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2654 + __81fgg2step2654) / __81fgg2step2654)), _b5p6od9s = __81fgg2dlsvn2654; __81fgg2count2654 != 0; __81fgg2count2654--, _b5p6od9s += (__81fgg2step2654)) {

					{
						
						*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
Mark70:;
						// continue
					}
										}				}
				*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;
Mark80:;
				// continue
			}
						}		}//* 
		
		if (_aym8a085 > (int)0)
		{
			//* 
			//*        Generate Q(ilo+1:ihi,ilo+1:ihi) 
			//* 
			
			_hfwn2zbk(ref _aym8a085 ,ref _aym8a085 ,ref _aym8a085 ,(_vxfgpup9+(_pew3blan + (int)1 - 1) + (_pew3blan + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_0446f4de+(_pew3blan - 1)),_apig8meb ,ref _6fnxzlyp ,ref _itfnbz60 );
		}
		
		*(_apig8meb+((int)1 - 1)) = CMPLX(_e4ueamrn);
		return;//* 
		//*     End of CUNGHR 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
