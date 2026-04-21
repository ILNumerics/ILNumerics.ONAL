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
//*> \brief \b STREXC 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download STREXC + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/strexc.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/strexc.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/strexc.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE STREXC( COMPQ, N, T, LDT, Q, LDQ, IFST, ILST, WORK, 
//*                          INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          COMPQ 
//*       INTEGER            IFST, ILST, INFO, LDQ, LDT, N 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               Q( LDQ, * ), T( LDT, * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> STREXC reorders the real Schur factorization of a real matrix 
//*> A = Q*T*Q**T, so that the diagonal block of T with row index IFST is 
//*> moved to row ILST. 
//*> 
//*> The real Schur form T is reordered by an orthogonal similarity 
//*> transformation Z**T*T*Z, and optionally the matrix Q of Schur vectors 
//*> is updated by postmultiplying it with Z. 
//*> 
//*> T must be in Schur canonical form (as returned by SHSEQR), that is, 
//*> block upper triangular with 1-by-1 and 2-by-2 diagonal blocks; each 
//*> 2-by-2 diagonal block has its diagonal elements equal and its 
//*> off-diagonal elements of opposite sign. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] COMPQ 
//*> \verbatim 
//*>          COMPQ is CHARACTER*1 
//*>          = 'V':  update the matrix Q of Schur vectors; 
//*>          = 'N':  do not update Q. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix T. N >= 0. 
//*>          If N == 0 arguments ILST and IFST may be any value. 
//*> \endverbatim 
//*> 
//*> \param[in,out] T 
//*> \verbatim 
//*>          T is REAL array, dimension (LDT,N) 
//*>          On entry, the upper quasi-triangular matrix T, in Schur 
//*>          Schur canonical form. 
//*>          On exit, the reordered upper quasi-triangular matrix, again 
//*>          in Schur canonical form. 
//*> \endverbatim 
//*> 
//*> \param[in] LDT 
//*> \verbatim 
//*>          LDT is INTEGER 
//*>          The leading dimension of the array T. LDT >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[in,out] Q 
//*> \verbatim 
//*>          Q is REAL array, dimension (LDQ,N) 
//*>          On entry, if COMPQ = 'V', the matrix Q of Schur vectors. 
//*>          On exit, if COMPQ = 'V', Q has been postmultiplied by the 
//*>          orthogonal transformation matrix Z which reorders T. 
//*>          If COMPQ = 'N', Q is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] LDQ 
//*> \verbatim 
//*>          LDQ is INTEGER 
//*>          The leading dimension of the array Q.  LDQ >= 1, and if 
//*>          COMPQ = 'V', LDQ >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[in,out] IFST 
//*> \verbatim 
//*>          IFST is INTEGER 
//*> \endverbatim 
//*> 
//*> \param[in,out] ILST 
//*> \verbatim 
//*>          ILST is INTEGER 
//*> 
//*>          Specify the reordering of the diagonal blocks of T. 
//*>          The block with row index IFST is moved to row ILST, by a 
//*>          sequence of transpositions between adjacent blocks. 
//*>          On exit, if IFST pointed on entry to the second row of a 
//*>          2-by-2 block, it is changed to point to the first row; ILST 
//*>          always points to the first row of the block in its final 
//*>          position (which may differ from its input value by +1 or -1). 
//*>          1 <= IFST <= N; 1 <= ILST <= N. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is REAL array, dimension (N) 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value 
//*>          = 1:  two adjacent blocks were too close to swap (the problem 
//*>                is very ill-conditioned); T may have been partially 
//*>                reordered, and ILST points to the first row of the 
//*>                current position of the block being moved. 
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

	 
	public static void _ogu7zj4u(FString _bzlmbpq3, ref Int32 _dxpq0xkr, Single* _2ivtt43r, ref Int32 _w8yhbr2r, Single* _atumjwo3, ref Int32 _u3fpniqy, ref Int32 _la6t805m, ref Int32 _ab05c09e, Single* _apig8meb, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Boolean _gh3pzgwj =  default;
Int32 _ozt8116i =  default;
Int32 _3wy71t37 =  default;
Int32 _b12lybkb =  default;
Int32 _tyt7ypko =  default;
string fLanavab = default;
#endregion  variable declarations
_bzlmbpq3 = _bzlmbpq3.Convert(1);

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
		//*     Decode and test the input arguments. 
		//* 
		
		_gro5yvfo = (int)0;
		_gh3pzgwj = _w8y2rzgy(_bzlmbpq3 ,"V" );
		if ((!(_gh3pzgwj)) & (!(_w8y2rzgy(_bzlmbpq3 ,"N" ))))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_w8yhbr2r < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if ((_u3fpniqy < (int)1) | (_gh3pzgwj & (_u3fpniqy < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))))
		{
			
			_gro5yvfo = (int)-6;
		}
		else
		if (((_la6t805m < (int)1) | (_la6t805m > _dxpq0xkr)) & (_dxpq0xkr > (int)0))
		{
			
			_gro5yvfo = (int)-7;
		}
		else
		if (((_ab05c09e < (int)1) | (_ab05c09e > _dxpq0xkr)) & (_dxpq0xkr > (int)0))
		{
			
			_gro5yvfo = (int)-8;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("STREXC" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr <= (int)1)
		return;//* 
		//*     Determine the first row of specified block 
		//*     and find out it is 1 by 1 or 2 by 2. 
		//* 
		
		if (_la6t805m > (int)1)
		{
			
			if (*(_2ivtt43r+(_la6t805m - 1) + (_la6t805m - (int)1 - 1) * 1 * (_w8yhbr2r)) != _d0547bi2)
			_la6t805m = (_la6t805m - (int)1);
		}
		
		_3wy71t37 = (int)1;
		if (_la6t805m < _dxpq0xkr)
		{
			
			if (*(_2ivtt43r+(_la6t805m + (int)1 - 1) + (_la6t805m - 1) * 1 * (_w8yhbr2r)) != _d0547bi2)
			_3wy71t37 = (int)2;
		}
		//* 
		//*     Determine the first row of the final block 
		//*     and find out it is 1 by 1 or 2 by 2. 
		//* 
		
		if (_ab05c09e > (int)1)
		{
			
			if (*(_2ivtt43r+(_ab05c09e - 1) + (_ab05c09e - (int)1 - 1) * 1 * (_w8yhbr2r)) != _d0547bi2)
			_ab05c09e = (_ab05c09e - (int)1);
		}
		
		_b12lybkb = (int)1;
		if (_ab05c09e < _dxpq0xkr)
		{
			
			if (*(_2ivtt43r+(_ab05c09e + (int)1 - 1) + (_ab05c09e - 1) * 1 * (_w8yhbr2r)) != _d0547bi2)
			_b12lybkb = (int)2;
		}
		//* 
		
		if (_la6t805m == _ab05c09e)
		return;//* 
		
		if (_la6t805m < _ab05c09e)
		{
			//* 
			//*        Update ILST 
			//* 
			
			if ((_3wy71t37 == (int)2) & (_b12lybkb == (int)1))
			_ab05c09e = (_ab05c09e - (int)1);
			if ((_3wy71t37 == (int)1) & (_b12lybkb == (int)2))
			_ab05c09e = (_ab05c09e + (int)1);//* 
			
			_ozt8116i = _la6t805m;//* 
			
Mark10:;
			// continue//* 
			//*        Swap block with next one below 
			//* 
			
			if ((_3wy71t37 == (int)1) | (_3wy71t37 == (int)2))
			{
				//* 
				//*           Current block either 1 by 1 or 2 by 2 
				//* 
				
				_tyt7ypko = (int)1;
				if (((_ozt8116i + _3wy71t37) + (int)1) <= _dxpq0xkr)
				{
					
					if (*(_2ivtt43r+((_ozt8116i + _3wy71t37) + (int)1 - 1) + (_ozt8116i + _3wy71t37 - 1) * 1 * (_w8yhbr2r)) != _d0547bi2)
					_tyt7ypko = (int)2;
				}
				
				_qbfzslu9(ref _gh3pzgwj ,ref _dxpq0xkr ,_2ivtt43r ,ref _w8yhbr2r ,_atumjwo3 ,ref _u3fpniqy ,ref _ozt8116i ,ref _3wy71t37 ,ref _tyt7ypko ,_apig8meb ,ref _gro5yvfo );
				if (_gro5yvfo != (int)0)
				{
					
					_ab05c09e = _ozt8116i;
					return;
				}
				
				_ozt8116i = (_ozt8116i + _tyt7ypko);//* 
				//*           Test if 2 by 2 block breaks into two 1 by 1 blocks 
				//* 
				
				if (_3wy71t37 == (int)2)
				{
					
					if (*(_2ivtt43r+(_ozt8116i + (int)1 - 1) + (_ozt8116i - 1) * 1 * (_w8yhbr2r)) == _d0547bi2)
					_3wy71t37 = (int)3;
				}
				//* 
				
			}
			else
			{
				//* 
				//*           Current block consists of two 1 by 1 blocks each of which 
				//*           must be swapped individually 
				//* 
				
				_tyt7ypko = (int)1;
				if ((_ozt8116i + (int)3) <= _dxpq0xkr)
				{
					
					if (*(_2ivtt43r+(_ozt8116i + (int)3 - 1) + (_ozt8116i + (int)2 - 1) * 1 * (_w8yhbr2r)) != _d0547bi2)
					_tyt7ypko = (int)2;
				}
				
				_qbfzslu9(ref _gh3pzgwj ,ref _dxpq0xkr ,_2ivtt43r ,ref _w8yhbr2r ,_atumjwo3 ,ref _u3fpniqy ,ref Unsafe.AsRef(_ozt8116i + (int)1) ,ref Unsafe.AsRef((int)1) ,ref _tyt7ypko ,_apig8meb ,ref _gro5yvfo );
				if (_gro5yvfo != (int)0)
				{
					
					_ab05c09e = _ozt8116i;
					return;
				}
				
				if (_tyt7ypko == (int)1)
				{
					//* 
					//*              Swap two 1 by 1 blocks, no problems possible 
					//* 
					
					_qbfzslu9(ref _gh3pzgwj ,ref _dxpq0xkr ,_2ivtt43r ,ref _w8yhbr2r ,_atumjwo3 ,ref _u3fpniqy ,ref _ozt8116i ,ref Unsafe.AsRef((int)1) ,ref _tyt7ypko ,_apig8meb ,ref _gro5yvfo );
					_ozt8116i = (_ozt8116i + (int)1);
				}
				else
				{
					//* 
					//*              Recompute NBNEXT in case 2 by 2 split 
					//* 
					
					if (*(_2ivtt43r+(_ozt8116i + (int)2 - 1) + (_ozt8116i + (int)1 - 1) * 1 * (_w8yhbr2r)) == _d0547bi2)
					_tyt7ypko = (int)1;
					if (_tyt7ypko == (int)2)
					{
						//* 
						//*                 2 by 2 Block did not split 
						//* 
						
						_qbfzslu9(ref _gh3pzgwj ,ref _dxpq0xkr ,_2ivtt43r ,ref _w8yhbr2r ,_atumjwo3 ,ref _u3fpniqy ,ref _ozt8116i ,ref Unsafe.AsRef((int)1) ,ref _tyt7ypko ,_apig8meb ,ref _gro5yvfo );
						if (_gro5yvfo != (int)0)
						{
							
							_ab05c09e = _ozt8116i;
							return;
						}
						
						_ozt8116i = (_ozt8116i + (int)2);
					}
					else
					{
						//* 
						//*                 2 by 2 Block did split 
						//* 
						
						_qbfzslu9(ref _gh3pzgwj ,ref _dxpq0xkr ,_2ivtt43r ,ref _w8yhbr2r ,_atumjwo3 ,ref _u3fpniqy ,ref _ozt8116i ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef((int)1) ,_apig8meb ,ref _gro5yvfo );
						_qbfzslu9(ref _gh3pzgwj ,ref _dxpq0xkr ,_2ivtt43r ,ref _w8yhbr2r ,_atumjwo3 ,ref _u3fpniqy ,ref Unsafe.AsRef(_ozt8116i + (int)1) ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef((int)1) ,_apig8meb ,ref _gro5yvfo );
						_ozt8116i = (_ozt8116i + (int)2);
					}
					
				}
				
			}
			
			if (_ozt8116i < _ab05c09e)goto Mark10;//* 
			
		}
		else
		{
			//* 
			
			_ozt8116i = _la6t805m;
Mark20:;
			// continue//* 
			//*        Swap block with next one above 
			//* 
			
			if ((_3wy71t37 == (int)1) | (_3wy71t37 == (int)2))
			{
				//* 
				//*           Current block either 1 by 1 or 2 by 2 
				//* 
				
				_tyt7ypko = (int)1;
				if (_ozt8116i >= (int)3)
				{
					
					if (*(_2ivtt43r+(_ozt8116i - (int)1 - 1) + (_ozt8116i - (int)2 - 1) * 1 * (_w8yhbr2r)) != _d0547bi2)
					_tyt7ypko = (int)2;
				}
				
				_qbfzslu9(ref _gh3pzgwj ,ref _dxpq0xkr ,_2ivtt43r ,ref _w8yhbr2r ,_atumjwo3 ,ref _u3fpniqy ,ref Unsafe.AsRef(_ozt8116i - _tyt7ypko) ,ref _tyt7ypko ,ref _3wy71t37 ,_apig8meb ,ref _gro5yvfo );
				if (_gro5yvfo != (int)0)
				{
					
					_ab05c09e = _ozt8116i;
					return;
				}
				
				_ozt8116i = (_ozt8116i - _tyt7ypko);//* 
				//*           Test if 2 by 2 block breaks into two 1 by 1 blocks 
				//* 
				
				if (_3wy71t37 == (int)2)
				{
					
					if (*(_2ivtt43r+(_ozt8116i + (int)1 - 1) + (_ozt8116i - 1) * 1 * (_w8yhbr2r)) == _d0547bi2)
					_3wy71t37 = (int)3;
				}
				//* 
				
			}
			else
			{
				//* 
				//*           Current block consists of two 1 by 1 blocks each of which 
				//*           must be swapped individually 
				//* 
				
				_tyt7ypko = (int)1;
				if (_ozt8116i >= (int)3)
				{
					
					if (*(_2ivtt43r+(_ozt8116i - (int)1 - 1) + (_ozt8116i - (int)2 - 1) * 1 * (_w8yhbr2r)) != _d0547bi2)
					_tyt7ypko = (int)2;
				}
				
				_qbfzslu9(ref _gh3pzgwj ,ref _dxpq0xkr ,_2ivtt43r ,ref _w8yhbr2r ,_atumjwo3 ,ref _u3fpniqy ,ref Unsafe.AsRef(_ozt8116i - _tyt7ypko) ,ref _tyt7ypko ,ref Unsafe.AsRef((int)1) ,_apig8meb ,ref _gro5yvfo );
				if (_gro5yvfo != (int)0)
				{
					
					_ab05c09e = _ozt8116i;
					return;
				}
				
				if (_tyt7ypko == (int)1)
				{
					//* 
					//*              Swap two 1 by 1 blocks, no problems possible 
					//* 
					
					_qbfzslu9(ref _gh3pzgwj ,ref _dxpq0xkr ,_2ivtt43r ,ref _w8yhbr2r ,_atumjwo3 ,ref _u3fpniqy ,ref _ozt8116i ,ref _tyt7ypko ,ref Unsafe.AsRef((int)1) ,_apig8meb ,ref _gro5yvfo );
					_ozt8116i = (_ozt8116i - (int)1);
				}
				else
				{
					//* 
					//*              Recompute NBNEXT in case 2 by 2 split 
					//* 
					
					if (*(_2ivtt43r+(_ozt8116i - 1) + (_ozt8116i - (int)1 - 1) * 1 * (_w8yhbr2r)) == _d0547bi2)
					_tyt7ypko = (int)1;
					if (_tyt7ypko == (int)2)
					{
						//* 
						//*                 2 by 2 Block did not split 
						//* 
						
						_qbfzslu9(ref _gh3pzgwj ,ref _dxpq0xkr ,_2ivtt43r ,ref _w8yhbr2r ,_atumjwo3 ,ref _u3fpniqy ,ref Unsafe.AsRef(_ozt8116i - (int)1) ,ref Unsafe.AsRef((int)2) ,ref Unsafe.AsRef((int)1) ,_apig8meb ,ref _gro5yvfo );
						if (_gro5yvfo != (int)0)
						{
							
							_ab05c09e = _ozt8116i;
							return;
						}
						
						_ozt8116i = (_ozt8116i - (int)2);
					}
					else
					{
						//* 
						//*                 2 by 2 Block did split 
						//* 
						
						_qbfzslu9(ref _gh3pzgwj ,ref _dxpq0xkr ,_2ivtt43r ,ref _w8yhbr2r ,_atumjwo3 ,ref _u3fpniqy ,ref _ozt8116i ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef((int)1) ,_apig8meb ,ref _gro5yvfo );
						_qbfzslu9(ref _gh3pzgwj ,ref _dxpq0xkr ,_2ivtt43r ,ref _w8yhbr2r ,_atumjwo3 ,ref _u3fpniqy ,ref Unsafe.AsRef(_ozt8116i - (int)1) ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef((int)1) ,_apig8meb ,ref _gro5yvfo );
						_ozt8116i = (_ozt8116i - (int)2);
					}
					
				}
				
			}
			
			if (_ozt8116i > _ab05c09e)goto Mark20;
		}
		
		_ab05c09e = _ozt8116i;//* 
		
		return;//* 
		//*     End of STREXC 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
