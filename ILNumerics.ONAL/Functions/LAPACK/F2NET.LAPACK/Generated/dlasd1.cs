
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
//*> \brief \b DLASD1 computes the SVD of an upper bidiagonal matrix B of the specified size. Used by sbdsdc. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLASD1 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlasd1.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlasd1.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlasd1.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLASD1( NL, NR, SQRE, D, ALPHA, BETA, U, LDU, VT, LDVT, 
//*                          IDXQ, IWORK, WORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, LDU, LDVT, NL, NR, SQRE 
//*       DOUBLE PRECISION   ALPHA, BETA 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IDXQ( * ), IWORK( * ) 
//*       DOUBLE PRECISION   D( * ), U( LDU, * ), VT( LDVT, * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLASD1 computes the SVD of an upper bidiagonal N-by-M matrix B, 
//*> where N = NL + NR + 1 and M = N + SQRE. DLASD1 is called from DLASD0. 
//*> 
//*> A related subroutine DLASD7 handles the case in which the singular 
//*> values (and the singular vectors in factored form) are desired. 
//*> 
//*> DLASD1 computes the SVD as follows: 
//*> 
//*>               ( D1(in)    0    0       0 ) 
//*>   B = U(in) * (   Z1**T   a   Z2**T    b ) * VT(in) 
//*>               (   0       0   D2(in)   0 ) 
//*> 
//*>     = U(out) * ( D(out) 0) * VT(out) 
//*> 
//*> where Z**T = (Z1**T a Z2**T b) = u**T VT**T, and u is a vector of dimension M 
//*> with ALPHA and BETA in the NL+1 and NL+2 th entries and zeros 
//*> elsewhere; and the entry b is empty if SQRE = 0. 
//*> 
//*> The left singular vectors of the original matrix are stored in U, and 
//*> the transpose of the right singular vectors are stored in VT, and the 
//*> singular values are in D.  The algorithm consists of three stages: 
//*> 
//*>    The first stage consists of deflating the size of the problem 
//*>    when there are multiple singular values or when there are zeros in 
//*>    the Z vector.  For each such occurrence the dimension of the 
//*>    secular equation problem is reduced by one.  This stage is 
//*>    performed by the routine DLASD2. 
//*> 
//*>    The second stage consists of calculating the updated 
//*>    singular values. This is done by finding the square roots of the 
//*>    roots of the secular equation via the routine DLASD4 (as called 
//*>    by DLASD3). This routine also calculates the singular vectors of 
//*>    the current problem. 
//*> 
//*>    The final stage consists of computing the updated singular vectors 
//*>    directly using the updated singular values.  The singular vectors 
//*>    for the current problem are multiplied with the singular vectors 
//*>    from the overall problem. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] NL 
//*> \verbatim 
//*>          NL is INTEGER 
//*>         The row dimension of the upper block.  NL >= 1. 
//*> \endverbatim 
//*> 
//*> \param[in] NR 
//*> \verbatim 
//*>          NR is INTEGER 
//*>         The row dimension of the lower block.  NR >= 1. 
//*> \endverbatim 
//*> 
//*> \param[in] SQRE 
//*> \verbatim 
//*>          SQRE is INTEGER 
//*>         = 0: the lower block is an NR-by-NR square matrix. 
//*>         = 1: the lower block is an NR-by-(NR+1) rectangular matrix. 
//*> 
//*>         The bidiagonal matrix has row dimension N = NL + NR + 1, 
//*>         and column dimension M = N + SQRE. 
//*> \endverbatim 
//*> 
//*> \param[in,out] D 
//*> \verbatim 
//*>          D is DOUBLE PRECISION array, 
//*>                        dimension (N = NL+NR+1). 
//*>         On entry D(1:NL,1:NL) contains the singular values of the 
//*>         upper block; and D(NL+2:N) contains the singular values of 
//*>         the lower block. On exit D(1:N) contains the singular values 
//*>         of the modified matrix. 
//*> \endverbatim 
//*> 
//*> \param[in,out] ALPHA 
//*> \verbatim 
//*>          ALPHA is DOUBLE PRECISION 
//*>         Contains the diagonal element associated with the added row. 
//*> \endverbatim 
//*> 
//*> \param[in,out] BETA 
//*> \verbatim 
//*>          BETA is DOUBLE PRECISION 
//*>         Contains the off-diagonal element associated with the added 
//*>         row. 
//*> \endverbatim 
//*> 
//*> \param[in,out] U 
//*> \verbatim 
//*>          U is DOUBLE PRECISION array, dimension(LDU,N) 
//*>         On entry U(1:NL, 1:NL) contains the left singular vectors of 
//*>         the upper block; U(NL+2:N, NL+2:N) contains the left singular 
//*>         vectors of the lower block. On exit U contains the left 
//*>         singular vectors of the bidiagonal matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] LDU 
//*> \verbatim 
//*>          LDU is INTEGER 
//*>         The leading dimension of the array U.  LDU >= max( 1, N ). 
//*> \endverbatim 
//*> 
//*> \param[in,out] VT 
//*> \verbatim 
//*>          VT is DOUBLE PRECISION array, dimension(LDVT,M) 
//*>         where M = N + SQRE. 
//*>         On entry VT(1:NL+1, 1:NL+1)**T contains the right singular 
//*>         vectors of the upper block; VT(NL+2:M, NL+2:M)**T contains 
//*>         the right singular vectors of the lower block. On exit 
//*>         VT**T contains the right singular vectors of the 
//*>         bidiagonal matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] LDVT 
//*> \verbatim 
//*>          LDVT is INTEGER 
//*>         The leading dimension of the array VT.  LDVT >= max( 1, M ). 
//*> \endverbatim 
//*> 
//*> \param[in,out] IDXQ 
//*> \verbatim 
//*>          IDXQ is INTEGER array, dimension(N) 
//*>         This contains the permutation which will reintegrate the 
//*>         subproblem just solved back into sorted order, i.e. 
//*>         D( IDXQ( I = 1, N ) ) will be in ascending order. 
//*> \endverbatim 
//*> 
//*> \param[out] IWORK 
//*> \verbatim 
//*>          IWORK is INTEGER array, dimension( 4 * N ) 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is DOUBLE PRECISION array, dimension( 3*M**2 + 2*M ) 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit. 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value. 
//*>          > 0:  if INFO = 1, a singular value did not converge 
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
//*> \date June 2016 
//* 
//*> \ingroup OTHERauxiliary 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>     Ming Gu and Huan Ren, Computer Science Division, University of 
//*>     California at Berkeley, USA 
//*> 
//*  ===================================================================== 

	 
	public static void _mac9p8ny(ref Int32 _zx57w4aj, ref Int32 _oqpc3yjg, ref Int32 _9qyq7j3e, Double* _plfm7z8g, ref Double _r7cfteg3, ref Double _bafcbx97, Double* _7u55mqkq, ref Int32 _u6e6d39b, Double* _xdbczr8u, ref Int32 _h4ibbatv, Int32* _ca64lzpg, Int32* _4b6rt45i, Double* _apig8meb, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _kxg5drh2 =  1d;
Double _d0547bi2 =  0d;
Int32 _gj7zf775 =  default;
Int32 _b5p6od9s =  default;
Int32 _diodrai4 =  default;
Int32 _dzf4x6zd =  default;
Int32 _u0tzpo1z =  default;
Int32 _zicl1k1z =  default;
Int32 _mscxe0h8 =  default;
Int32 _8shzspll =  default;
Int32 _2i602taj =  default;
Int32 _qbqg6u98 =  default;
Int32 _umlkckdg =  default;
Int32 _u3fpniqy =  default;
Int32 _qz188u0m =  default;
Int32 _sh6ez9uf =  default;
Int32 _ev4xhht5 =  default;
Int32 _dxpq0xkr =  default;
Int32 _4o1bt8b1 =  default;
Int32 _tixk7d1h =  default;
Double _wby36o6h =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     June 2016 
		//* 
		//*     .. Scalar Arguments .. 
		//*     .. 
		//*     .. Array Arguments .. 
		//*     .. 
		//* 
		//*  ===================================================================== 
		//* 
		//*     .. Parameters .. 
		//* 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input parameters. 
		//* 
		
		_gro5yvfo = (int)0;//* 
		
		if (_zx57w4aj < (int)1)
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_oqpc3yjg < (int)1)
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if ((_9qyq7j3e < (int)0) | (_9qyq7j3e > (int)1))
		{
			
			_gro5yvfo = (int)-3;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("DLASD1" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		
		_dxpq0xkr = ((_zx57w4aj + _oqpc3yjg) + (int)1);
		_ev4xhht5 = (_dxpq0xkr + _9qyq7j3e);//* 
		//*     The following values are for bookkeeping purposes only.  They are 
		//*     integer pointers which indicate the portion of the workspace 
		//*     used by a particular array in DLASD2 and DLASD3. 
		//* 
		
		_qz188u0m = _dxpq0xkr;
		_sh6ez9uf = _ev4xhht5;//* 
		
		_qbqg6u98 = (int)1;
		_mscxe0h8 = (_qbqg6u98 + _ev4xhht5);
		_8shzspll = (_mscxe0h8 + _dxpq0xkr);
		_2i602taj = (_8shzspll + (_qz188u0m * _dxpq0xkr));
		_zicl1k1z = (_2i602taj + (_sh6ez9uf * _ev4xhht5));//* 
		
		_diodrai4 = (int)1;
		_dzf4x6zd = (_diodrai4 + _dxpq0xkr);
		_gj7zf775 = (_dzf4x6zd + _dxpq0xkr);
		_u0tzpo1z = (_gj7zf775 + _dxpq0xkr);//* 
		//*     Scale. 
		//* 
		
		_wby36o6h = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_r7cfteg3 ) ,ILNumerics.F2NET.Intrinsics.ABS(_bafcbx97 ) );
		*(_plfm7z8g+(_zx57w4aj + (int)1 - 1)) = _d0547bi2;
		{
			System.Int32 __81fgg2dlsvn203 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step203 = (System.Int32)((int)1);
			System.Int32 __81fgg2count203;
			for (__81fgg2count203 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn203 + __81fgg2step203) / __81fgg2step203)), _b5p6od9s = __81fgg2dlsvn203; __81fgg2count203 != 0; __81fgg2count203--, _b5p6od9s += (__81fgg2step203)) {

			{
				
				if (ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_b5p6od9s - 1)) ) > _wby36o6h)
				{
					
					_wby36o6h = ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_b5p6od9s - 1)) );
				}
				
Mark10:;
				// continue
			}
						}		}
		_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _wby36o6h ,ref Unsafe.AsRef(_kxg5drh2) ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1) ,_plfm7z8g ,ref _dxpq0xkr ,ref _gro5yvfo );
		_r7cfteg3 = (_r7cfteg3 / _wby36o6h);
		_bafcbx97 = (_bafcbx97 / _wby36o6h);//* 
		//*     Deflate singular values. 
		//* 
		
		_ph6grb63(ref _zx57w4aj ,ref _oqpc3yjg ,ref _9qyq7j3e ,ref _umlkckdg ,_plfm7z8g ,(_apig8meb+(_qbqg6u98 - 1)),ref _r7cfteg3 ,ref _bafcbx97 ,_7u55mqkq ,ref _u6e6d39b ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_mscxe0h8 - 1)),(_apig8meb+(_8shzspll - 1)),ref _qz188u0m ,(_apig8meb+(_2i602taj - 1)),ref _sh6ez9uf ,(_4b6rt45i+(_u0tzpo1z - 1)),(_4b6rt45i+(_diodrai4 - 1)),(_4b6rt45i+(_dzf4x6zd - 1)),_ca64lzpg ,(_4b6rt45i+(_gj7zf775 - 1)),ref _gro5yvfo );//* 
		//*     Solve Secular Equation and update singular vectors. 
		//* 
		
		_u3fpniqy = _umlkckdg;
		_s3dkv1uz(ref _zx57w4aj ,ref _oqpc3yjg ,ref _9qyq7j3e ,ref _umlkckdg ,_plfm7z8g ,(_apig8meb+(_zicl1k1z - 1)),ref _u3fpniqy ,(_apig8meb+(_mscxe0h8 - 1)),_7u55mqkq ,ref _u6e6d39b ,(_apig8meb+(_8shzspll - 1)),ref _qz188u0m ,_xdbczr8u ,ref _h4ibbatv ,(_apig8meb+(_2i602taj - 1)),ref _sh6ez9uf ,(_4b6rt45i+(_dzf4x6zd - 1)),(_4b6rt45i+(_gj7zf775 - 1)),(_apig8meb+(_qbqg6u98 - 1)),ref _gro5yvfo );//* 
		//*     Report the convergence failure. 
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			return;
		}
		//* 
		//*     Unscale. 
		//* 
		
		_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef(_kxg5drh2) ,ref _wby36o6h ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1) ,_plfm7z8g ,ref _dxpq0xkr ,ref _gro5yvfo );//* 
		//*     Prepare the IDXQ sorting permutation. 
		//* 
		
		_4o1bt8b1 = _umlkckdg;
		_tixk7d1h = (_dxpq0xkr - _umlkckdg);
		_csi3ymnh(ref _4o1bt8b1 ,ref _tixk7d1h ,_plfm7z8g ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef((int)-1) ,_ca64lzpg );//* 
		
		return;//* 
		//*     End of DLASD1 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
