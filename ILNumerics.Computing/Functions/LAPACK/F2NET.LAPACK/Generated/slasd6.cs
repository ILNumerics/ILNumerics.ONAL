
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
//*> \brief \b SLASD6 computes the SVD of an updated upper bidiagonal matrix obtained by merging two smaller ones by appending a row. Used by sbdsdc. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLASD6 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slasd6.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slasd6.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slasd6.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLASD6( ICOMPQ, NL, NR, SQRE, D, VF, VL, ALPHA, BETA, 
//*                          IDXQ, PERM, GIVPTR, GIVCOL, LDGCOL, GIVNUM, 
//*                          LDGNUM, POLES, DIFL, DIFR, Z, K, C, S, WORK, 
//*                          IWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            GIVPTR, ICOMPQ, INFO, K, LDGCOL, LDGNUM, NL, 
//*      $                   NR, SQRE 
//*       REAL               ALPHA, BETA, C, S 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            GIVCOL( LDGCOL, * ), IDXQ( * ), IWORK( * ), 
//*      $                   PERM( * ) 
//*       REAL               D( * ), DIFL( * ), DIFR( * ), 
//*      $                   GIVNUM( LDGNUM, * ), POLES( LDGNUM, * ), 
//*      $                   VF( * ), VL( * ), WORK( * ), Z( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLASD6 computes the SVD of an updated upper bidiagonal matrix B 
//*> obtained by merging two smaller ones by appending a row. This 
//*> routine is used only for the problem which requires all singular 
//*> values and optionally singular vector matrices in factored form. 
//*> B is an N-by-M matrix with N = NL + NR + 1 and M = N + SQRE. 
//*> A related subroutine, SLASD1, handles the case in which all singular 
//*> values and singular vectors of the bidiagonal matrix are desired. 
//*> 
//*> SLASD6 computes the SVD as follows: 
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
//*> The singular values of B can be computed using D1, D2, the first 
//*> components of all the right singular vectors of the lower block, and 
//*> the last components of all the right singular vectors of the upper 
//*> block. These components are stored and updated in VF and VL, 
//*> respectively, in SLASD6. Hence U and VT are not explicitly 
//*> referenced. 
//*> 
//*> The singular values are stored in D. The algorithm consists of two 
//*> stages: 
//*> 
//*>       The first stage consists of deflating the size of the problem 
//*>       when there are multiple singular values or if there is a zero 
//*>       in the Z vector. For each such occurrence the dimension of the 
//*>       secular equation problem is reduced by one. This stage is 
//*>       performed by the routine SLASD7. 
//*> 
//*>       The second stage consists of calculating the updated 
//*>       singular values. This is done by finding the roots of the 
//*>       secular equation via the routine SLASD4 (as called by SLASD8). 
//*>       This routine also updates VF and VL and computes the distances 
//*>       between the updated singular values and the old singular 
//*>       values. 
//*> 
//*> SLASD6 is called from SLASDA. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] ICOMPQ 
//*> \verbatim 
//*>          ICOMPQ is INTEGER 
//*>         Specifies whether singular vectors are to be computed in 
//*>         factored form: 
//*>         = 0: Compute singular values only. 
//*>         = 1: Compute singular vectors in factored form as well. 
//*> \endverbatim 
//*> 
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
//*>          D is REAL array, dimension (NL+NR+1). 
//*>         On entry D(1:NL,1:NL) contains the singular values of the 
//*>         upper block, and D(NL+2:N) contains the singular values 
//*>         of the lower block. On exit D(1:N) contains the singular 
//*>         values of the modified matrix. 
//*> \endverbatim 
//*> 
//*> \param[in,out] VF 
//*> \verbatim 
//*>          VF is REAL array, dimension (M) 
//*>         On entry, VF(1:NL+1) contains the first components of all 
//*>         right singular vectors of the upper block; and VF(NL+2:M) 
//*>         contains the first components of all right singular vectors 
//*>         of the lower block. On exit, VF contains the first components 
//*>         of all right singular vectors of the bidiagonal matrix. 
//*> \endverbatim 
//*> 
//*> \param[in,out] VL 
//*> \verbatim 
//*>          VL is REAL array, dimension (M) 
//*>         On entry, VL(1:NL+1) contains the  last components of all 
//*>         right singular vectors of the upper block; and VL(NL+2:M) 
//*>         contains the last components of all right singular vectors of 
//*>         the lower block. On exit, VL contains the last components of 
//*>         all right singular vectors of the bidiagonal matrix. 
//*> \endverbatim 
//*> 
//*> \param[in,out] ALPHA 
//*> \verbatim 
//*>          ALPHA is REAL 
//*>         Contains the diagonal element associated with the added row. 
//*> \endverbatim 
//*> 
//*> \param[in,out] BETA 
//*> \verbatim 
//*>          BETA is REAL 
//*>         Contains the off-diagonal element associated with the added 
//*>         row. 
//*> \endverbatim 
//*> 
//*> \param[in,out] IDXQ 
//*> \verbatim 
//*>          IDXQ is INTEGER array, dimension (N) 
//*>         This contains the permutation which will reintegrate the 
//*>         subproblem just solved back into sorted order, i.e. 
//*>         D( IDXQ( I = 1, N ) ) will be in ascending order. 
//*> \endverbatim 
//*> 
//*> \param[out] PERM 
//*> \verbatim 
//*>          PERM is INTEGER array, dimension ( N ) 
//*>         The permutations (from deflation and sorting) to be applied 
//*>         to each block. Not referenced if ICOMPQ = 0. 
//*> \endverbatim 
//*> 
//*> \param[out] GIVPTR 
//*> \verbatim 
//*>          GIVPTR is INTEGER 
//*>         The number of Givens rotations which took place in this 
//*>         subproblem. Not referenced if ICOMPQ = 0. 
//*> \endverbatim 
//*> 
//*> \param[out] GIVCOL 
//*> \verbatim 
//*>          GIVCOL is INTEGER array, dimension ( LDGCOL, 2 ) 
//*>         Each pair of numbers indicates a pair of columns to take place 
//*>         in a Givens rotation. Not referenced if ICOMPQ = 0. 
//*> \endverbatim 
//*> 
//*> \param[in] LDGCOL 
//*> \verbatim 
//*>          LDGCOL is INTEGER 
//*>         leading dimension of GIVCOL, must be at least N. 
//*> \endverbatim 
//*> 
//*> \param[out] GIVNUM 
//*> \verbatim 
//*>          GIVNUM is REAL array, dimension ( LDGNUM, 2 ) 
//*>         Each number indicates the C or S value to be used in the 
//*>         corresponding Givens rotation. Not referenced if ICOMPQ = 0. 
//*> \endverbatim 
//*> 
//*> \param[in] LDGNUM 
//*> \verbatim 
//*>          LDGNUM is INTEGER 
//*>         The leading dimension of GIVNUM and POLES, must be at least N. 
//*> \endverbatim 
//*> 
//*> \param[out] POLES 
//*> \verbatim 
//*>          POLES is REAL array, dimension ( LDGNUM, 2 ) 
//*>         On exit, POLES(1,*) is an array containing the new singular 
//*>         values obtained from solving the secular equation, and 
//*>         POLES(2,*) is an array containing the poles in the secular 
//*>         equation. Not referenced if ICOMPQ = 0. 
//*> \endverbatim 
//*> 
//*> \param[out] DIFL 
//*> \verbatim 
//*>          DIFL is REAL array, dimension ( N ) 
//*>         On exit, DIFL(I) is the distance between I-th updated 
//*>         (undeflated) singular value and the I-th (undeflated) old 
//*>         singular value. 
//*> \endverbatim 
//*> 
//*> \param[out] DIFR 
//*> \verbatim 
//*>          DIFR is REAL array, 
//*>                   dimension ( LDDIFR, 2 ) if ICOMPQ = 1 and 
//*>                   dimension ( K ) if ICOMPQ = 0. 
//*>          On exit, DIFR(I,1) = D(I) - DSIGMA(I+1), DIFR(K,1) is not 
//*>          defined and will not be referenced. 
//*> 
//*>          If ICOMPQ = 1, DIFR(1:K,2) is an array containing the 
//*>          normalizing factors for the right singular vector matrix. 
//*> 
//*>         See SLASD8 for details on DIFL and DIFR. 
//*> \endverbatim 
//*> 
//*> \param[out] Z 
//*> \verbatim 
//*>          Z is REAL array, dimension ( M ) 
//*>         The first elements of this array contain the components 
//*>         of the deflation-adjusted updating row vector. 
//*> \endverbatim 
//*> 
//*> \param[out] K 
//*> \verbatim 
//*>          K is INTEGER 
//*>         Contains the dimension of the non-deflated matrix, 
//*>         This is the order of the related secular equation. 1 <= K <=N. 
//*> \endverbatim 
//*> 
//*> \param[out] C 
//*> \verbatim 
//*>          C is REAL 
//*>         C contains garbage if SQRE =0 and the C-value of a Givens 
//*>         rotation related to the right null space if SQRE = 1. 
//*> \endverbatim 
//*> 
//*> \param[out] S 
//*> \verbatim 
//*>          S is REAL 
//*>         S contains garbage if SQRE =0 and the S-value of a Givens 
//*>         rotation related to the right null space if SQRE = 1. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is REAL array, dimension ( 4 * M ) 
//*> \endverbatim 
//*> 
//*> \param[out] IWORK 
//*> \verbatim 
//*>          IWORK is INTEGER array, dimension ( 3 * N ) 
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

	 
	public static void _4zx8qorc(ref Int32 _y1be9goe, ref Int32 _zx57w4aj, ref Int32 _oqpc3yjg, ref Int32 _9qyq7j3e, Single* _plfm7z8g, Single* _w7xcjdw0, Single* _ppzorcqs, ref Single _r7cfteg3, ref Single _bafcbx97, Int32* _ca64lzpg, Int32* _umao48xu, ref Int32 _8vecpt74, Int32* _0zwn6fsy, ref Int32 _uhi0ls8i, Single* _gh266ol1, ref Int32 _jlfchtn9, Single* _7nk40y8b, Single* _i8976ehd, Single* _doljbvm2, Single* _7e60fcso, ref Int32 _umlkckdg, ref Single _3crf0qn3, ref Single _irk8i6qr, Single* _apig8meb, Int32* _4b6rt45i, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _kxg5drh2 =  1f;
Single _d0547bi2 =  0f;
Int32 _b5p6od9s =  default;
Int32 _diodrai4 =  default;
Int32 _dzf4x6zd =  default;
Int32 _u0tzpo1z =  default;
Int32 _mscxe0h8 =  default;
Int32 _qt0ejazu =  default;
Int32 _re5sx3yi =  default;
Int32 _11qhqs00 =  default;
Int32 _ev4xhht5 =  default;
Int32 _dxpq0xkr =  default;
Int32 _4o1bt8b1 =  default;
Int32 _tixk7d1h =  default;
Single _wby36o6h =  default;
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
		
		_gro5yvfo = (int)0;
		_dxpq0xkr = ((_zx57w4aj + _oqpc3yjg) + (int)1);
		_ev4xhht5 = (_dxpq0xkr + _9qyq7j3e);//* 
		
		if ((_y1be9goe < (int)0) | (_y1be9goe > (int)1))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_zx57w4aj < (int)1)
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_oqpc3yjg < (int)1)
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if ((_9qyq7j3e < (int)0) | (_9qyq7j3e > (int)1))
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if (_uhi0ls8i < _dxpq0xkr)
		{
			
			_gro5yvfo = (int)-14;
		}
		else
		if (_jlfchtn9 < _dxpq0xkr)
		{
			
			_gro5yvfo = (int)-16;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("SLASD6" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     The following values are for bookkeeping purposes only.  They are 
		//*     integer pointers which indicate the portion of the workspace 
		//*     used by a particular array in SLASD7 and SLASD8. 
		//* 
		
		_mscxe0h8 = (int)1;
		_11qhqs00 = (_mscxe0h8 + _dxpq0xkr);
		_qt0ejazu = (_11qhqs00 + _ev4xhht5);
		_re5sx3yi = (_qt0ejazu + _ev4xhht5);//* 
		
		_diodrai4 = (int)1;
		_dzf4x6zd = (_diodrai4 + _dxpq0xkr);
		_u0tzpo1z = (_dzf4x6zd + _dxpq0xkr);//* 
		//*     Scale. 
		//* 
		
		_wby36o6h = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_r7cfteg3 ) ,ILNumerics.F2NET.Intrinsics.ABS(_bafcbx97 ) );
		*(_plfm7z8g+(_zx57w4aj + (int)1 - 1)) = _d0547bi2;
		{
			System.Int32 __81fgg2dlsvn742 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step742 = (System.Int32)((int)1);
			System.Int32 __81fgg2count742;
			for (__81fgg2count742 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn742 + __81fgg2step742) / __81fgg2step742)), _b5p6od9s = __81fgg2dlsvn742; __81fgg2count742 != 0; __81fgg2count742--, _b5p6od9s += (__81fgg2step742)) {

			{
				
				if (ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_b5p6od9s - 1)) ) > _wby36o6h)
				{
					
					_wby36o6h = ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_b5p6od9s - 1)) );
				}
				
Mark10:;
				// continue
			}
						}		}
		_cq2kgmi4("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _wby36o6h ,ref Unsafe.AsRef(_kxg5drh2) ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1) ,_plfm7z8g ,ref _dxpq0xkr ,ref _gro5yvfo );
		_r7cfteg3 = (_r7cfteg3 / _wby36o6h);
		_bafcbx97 = (_bafcbx97 / _wby36o6h);//* 
		//*     Sort and Deflate singular values. 
		//* 
		
		_evcsl3f1(ref _y1be9goe ,ref _zx57w4aj ,ref _oqpc3yjg ,ref _9qyq7j3e ,ref _umlkckdg ,_plfm7z8g ,_7e60fcso ,(_apig8meb+(_11qhqs00 - 1)),_w7xcjdw0 ,(_apig8meb+(_qt0ejazu - 1)),_ppzorcqs ,(_apig8meb+(_re5sx3yi - 1)),ref _r7cfteg3 ,ref _bafcbx97 ,(_apig8meb+(_mscxe0h8 - 1)),(_4b6rt45i+(_diodrai4 - 1)),(_4b6rt45i+(_u0tzpo1z - 1)),_ca64lzpg ,_umao48xu ,ref _8vecpt74 ,_0zwn6fsy ,ref _uhi0ls8i ,_gh266ol1 ,ref _jlfchtn9 ,ref _3crf0qn3 ,ref _irk8i6qr ,ref _gro5yvfo );//* 
		//*     Solve Secular Equation, compute DIFL, DIFR, and update VF, VL. 
		//* 
		
		_1al7xefr(ref _y1be9goe ,ref _umlkckdg ,_plfm7z8g ,_7e60fcso ,_w7xcjdw0 ,_ppzorcqs ,_i8976ehd ,_doljbvm2 ,ref _jlfchtn9 ,(_apig8meb+(_mscxe0h8 - 1)),(_apig8meb+(_11qhqs00 - 1)),ref _gro5yvfo );//* 
		//*     Report the possible convergence failure. 
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			return;
		}
		//* 
		//*     Save the poles if ICOMPQ = 1. 
		//* 
		
		if (_y1be9goe == (int)1)
		{
			
			_wcs7ne88(ref _umlkckdg ,_plfm7z8g ,ref Unsafe.AsRef((int)1) ,(_7nk40y8b+((int)1 - 1) + ((int)1 - 1) * 1 * (_jlfchtn9)),ref Unsafe.AsRef((int)1) );
			_wcs7ne88(ref _umlkckdg ,(_apig8meb+(_mscxe0h8 - 1)),ref Unsafe.AsRef((int)1) ,(_7nk40y8b+((int)1 - 1) + ((int)2 - 1) * 1 * (_jlfchtn9)),ref Unsafe.AsRef((int)1) );
		}
		//* 
		//*     Unscale. 
		//* 
		
		_cq2kgmi4("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef(_kxg5drh2) ,ref _wby36o6h ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1) ,_plfm7z8g ,ref _dxpq0xkr ,ref _gro5yvfo );//* 
		//*     Prepare the IDXQ sorting permutation. 
		//* 
		
		_4o1bt8b1 = _umlkckdg;
		_tixk7d1h = (_dxpq0xkr - _umlkckdg);
		_5cjk91e1(ref _4o1bt8b1 ,ref _tixk7d1h ,_plfm7z8g ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef((int)-1) ,_ca64lzpg );//* 
		
		return;//* 
		//*     End of SLASD6 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
