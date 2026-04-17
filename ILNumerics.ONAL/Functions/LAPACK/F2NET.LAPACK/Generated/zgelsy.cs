
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
//*> \brief <b> ZGELSY solves overdetermined or underdetermined systems for GE matrices</b> 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZGELSY + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zgelsy.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zgelsy.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zgelsy.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZGELSY( M, N, NRHS, A, LDA, B, LDB, JPVT, RCOND, RANK, 
//*                          WORK, LWORK, RWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, LDA, LDB, LWORK, M, N, NRHS, RANK 
//*       DOUBLE PRECISION   RCOND 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            JPVT( * ) 
//*       DOUBLE PRECISION   RWORK( * ) 
//*       COMPLEX*16         A( LDA, * ), B( LDB, * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZGELSY computes the minimum-norm solution to a complex linear least 
//*> squares problem: 
//*>     minimize || A * X - B || 
//*> using a complete orthogonal factorization of A.  A is an M-by-N 
//*> matrix which may be rank-deficient. 
//*> 
//*> Several right hand side vectors b and solution vectors x can be 
//*> handled in a single call; they are stored as the columns of the 
//*> M-by-NRHS right hand side matrix B and the N-by-NRHS solution 
//*> matrix X. 
//*> 
//*> The routine first computes a QR factorization with column pivoting: 
//*>     A * P = Q * [ R11 R12 ] 
//*>                 [  0  R22 ] 
//*> with R11 defined as the largest leading submatrix whose estimated 
//*> condition number is less than 1/RCOND.  The order of R11, RANK, 
//*> is the effective rank of A. 
//*> 
//*> Then, R22 is considered to be negligible, and R12 is annihilated 
//*> by unitary transformations from the right, arriving at the 
//*> complete orthogonal factorization: 
//*>    A * P = Q * [ T11 0 ] * Z 
//*>                [  0  0 ] 
//*> The minimum-norm solution is then 
//*>    X = P * Z**H [ inv(T11)*Q1**H*B ] 
//*>                 [        0         ] 
//*> where Q1 consists of the first RANK columns of Q. 
//*> 
//*> This routine is basically identical to the original xGELSX except 
//*> three differences: 
//*>   o The permutation of matrix B (the right hand side) is faster and 
//*>     more simple. 
//*>   o The call to the subroutine xGEQPF has been substituted by the 
//*>     the call to the subroutine xGEQP3. This subroutine is a Blas-3 
//*>     version of the QR factorization with column pivoting. 
//*>   o Matrix B (the right hand side) is updated with Blas-3. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of rows of the matrix A.  M >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of columns of the matrix A.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] NRHS 
//*> \verbatim 
//*>          NRHS is INTEGER 
//*>          The number of right hand sides, i.e., the number of 
//*>          columns of matrices B and X. NRHS >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension (LDA,N) 
//*>          On entry, the M-by-N matrix A. 
//*>          On exit, A has been overwritten by details of its 
//*>          complete orthogonal factorization. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,M). 
//*> \endverbatim 
//*> 
//*> \param[in,out] B 
//*> \verbatim 
//*>          B is COMPLEX*16 array, dimension (LDB,NRHS) 
//*>          On entry, the M-by-NRHS right hand side matrix B. 
//*>          On exit, the N-by-NRHS solution matrix X. 
//*> \endverbatim 
//*> 
//*> \param[in] LDB 
//*> \verbatim 
//*>          LDB is INTEGER 
//*>          The leading dimension of the array B. LDB >= max(1,M,N). 
//*> \endverbatim 
//*> 
//*> \param[in,out] JPVT 
//*> \verbatim 
//*>          JPVT is INTEGER array, dimension (N) 
//*>          On entry, if JPVT(i) .ne. 0, the i-th column of A is permuted 
//*>          to the front of AP, otherwise column i is a free column. 
//*>          On exit, if JPVT(i) = k, then the i-th column of A*P 
//*>          was the k-th column of A. 
//*> \endverbatim 
//*> 
//*> \param[in] RCOND 
//*> \verbatim 
//*>          RCOND is DOUBLE PRECISION 
//*>          RCOND is used to determine the effective rank of A, which 
//*>          is defined as the order of the largest leading triangular 
//*>          submatrix R11 in the QR factorization with pivoting of A, 
//*>          whose estimated condition number < 1/RCOND. 
//*> \endverbatim 
//*> 
//*> \param[out] RANK 
//*> \verbatim 
//*>          RANK is INTEGER 
//*>          The effective rank of A, i.e., the order of the submatrix 
//*>          R11.  This is the same as the order of the submatrix T11 
//*>          in the complete orthogonal factorization of A. 
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
//*>          The unblocked strategy requires that: 
//*>            LWORK >= MN + MAX( 2*MN, N+1, MN+NRHS ) 
//*>          where MN = min(M,N). 
//*>          The block algorithm requires that: 
//*>            LWORK >= MN + MAX( 2*MN, NB*(N+1), MN+MN*NB, MN+NB*NRHS ) 
//*>          where NB is an upper bound on the blocksize returned 
//*>          by ILAENV for the routines ZGEQP3, ZTZRZF, CTZRQF, ZUNMQR, 
//*>          and ZUNMRZ. 
//*> 
//*>          If LWORK = -1, then a workspace query is assumed; the routine 
//*>          only calculates the optimal size of the WORK array, returns 
//*>          this value as the first entry of the WORK array, and no error 
//*>          message related to LWORK is issued by XERBLA. 
//*> \endverbatim 
//*> 
//*> \param[out] RWORK 
//*> \verbatim 
//*>          RWORK is DOUBLE PRECISION array, dimension (2*N) 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0: successful exit 
//*>          < 0: if INFO = -i, the i-th argument had an illegal value 
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
//*> \ingroup complex16GEsolve 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>    A. Petitet, Computer Science Dept., Univ. of Tenn., Knoxville, USA \n 
//*>    E. Quintana-Orti, Depto. de Informatica, Universidad Jaime I, Spain \n 
//*>    G. Quintana-Orti, Depto. de Informatica, Universidad Jaime I, Spain \n 
//*> 
//*  ===================================================================== 

	 
	public static void _rsrplwoi(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _3nayvi7h, complex* _vxfgpup9, ref Int32 _ocv8fk5c, complex* _p9n405a5, ref Int32 _ly9opahg, Int32* _laipxa7w, ref Double _9zr5olpw, ref Int32 _uy2xc65y, complex* _apig8meb, ref Int32 _6fnxzlyp, Double* _dqanbbw3, ref Int32 _gro5yvfo)
	{
#region variable declarations
Int32 _gmv1yrg4 =  (int)1;
Int32 _o836wfk2 =  (int)2;
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
complex _gdjumcqt =   new fcomplex(0f,0f);
complex _40vhxf9f =   new fcomplex(1f,0f);
Boolean _lhlgm7z5 =  default;
Int32 _b5p6od9s =  default;
Int32 _ddt9utmg =  default;
Int32 _958q0men =  default;
Int32 _l9j66b7a =  default;
Int32 _cxl8k730 =  default;
Int32 _znpjgsef =  default;
Int32 _e4ueamrn =  default;
Int32 _0hik27x4 =  default;
Int32 _f7059815 =  default;
Int32 _5cqbzcf9 =  default;
Int32 _xitrebtf =  default;
Int32 _pvzafitq =  default;
Int32 _echfmoy1 =  default;
Double _j6vjow1g =  default;
Double _av7j8yda =  default;
Double _nk6sn86h =  default;
Double _t7m1e103 =  default;
Double _7yqs531w =  default;
Double _rhnpgpoi =  default;
Double _s97x59xi =  default;
Double _bogm0gwy =  default;
Double _u2m2azut =  default;
complex _o2zniltq =  default;
complex _h685tamv =  default;
complex _fmb4u5ka =  default;
complex _slkbnmvx =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK driver routine (version 3.7.0) -- 
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
		
		_0hik27x4 = ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_dxpq0xkr );
		_cxl8k730 = (_0hik27x4 + (int)1);
		_l9j66b7a = (((int)2 * _0hik27x4) + (int)1);//* 
		//*     Test the input arguments. 
		//* 
		
		_gro5yvfo = (int)0;
		_5cqbzcf9 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZGEQRF" ," " ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) );
		_xitrebtf = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZGERQF" ," " ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) );
		_pvzafitq = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZUNMQR" ," " ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _3nayvi7h ,ref Unsafe.AsRef((int)-1) );
		_echfmoy1 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZUNMRQ" ," " ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _3nayvi7h ,ref Unsafe.AsRef((int)-1) );
		_f7059815 = ILNumerics.F2NET.Intrinsics.MAX(_5cqbzcf9 ,_xitrebtf ,_pvzafitq ,_echfmoy1 );
		_e4ueamrn = ILNumerics.F2NET.Intrinsics.MAX((int)1 ,(_0hik27x4 + ((int)2 * _dxpq0xkr)) + (_f7059815 * (_dxpq0xkr + (int)1)) ,((int)2 * _0hik27x4) + (_f7059815 * _3nayvi7h) );
		*(_apig8meb+((int)1 - 1)) = ILNumerics.F2NET.Intrinsics.DCMPLX(_e4ueamrn );
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
		if (_3nayvi7h < (int)0)
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ))
		{
			
			_gro5yvfo = (int)-5;
		}
		else
		if (_ly9opahg < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)-7;
		}
		else
		if ((_6fnxzlyp < (_0hik27x4 + ILNumerics.F2NET.Intrinsics.MAX((int)2 * _0hik27x4 ,_dxpq0xkr + (int)1 ,_0hik27x4 + _3nayvi7h ))) & (!(_lhlgm7z5)))
		{
			
			_gro5yvfo = (int)-12;
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("ZGELSY" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
		
		if (ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_dxpq0xkr ,_3nayvi7h ) == (int)0)
		{
			
			_uy2xc65y = (int)0;
			return;
		}
		//* 
		//*     Get machine parameters 
		//* 
		
		_bogm0gwy = (_f43eg0w0("S" ) / _f43eg0w0("P" ));
		_av7j8yda = (_kxg5drh2 / _bogm0gwy);
		_to4dtyqc(ref _bogm0gwy ,ref _av7j8yda );//* 
		//*     Scale A, B if max entries outside range [SMLNUM,BIGNUM] 
		//* 
		
		_j6vjow1g = _o615qv2q("M" ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_dqanbbw3 );
		_ddt9utmg = (int)0;
		if ((_j6vjow1g > _d0547bi2) & (_j6vjow1g < _bogm0gwy))
		{
			//* 
			//*        Scale matrix norm up to SMLNUM 
			//* 
			
			_j6h8q4u5("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _j6vjow1g ,ref _bogm0gwy ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,ref _gro5yvfo );
			_ddt9utmg = (int)1;
		}
		else
		if (_j6vjow1g > _av7j8yda)
		{
			//* 
			//*        Scale matrix norm down to BIGNUM 
			//* 
			
			_j6h8q4u5("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _j6vjow1g ,ref _av7j8yda ,ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,ref _gro5yvfo );
			_ddt9utmg = (int)2;
		}
		else
		if (_j6vjow1g == _d0547bi2)
		{
			//* 
			//*        Matrix all zero. Return zero solution. 
			//* 
			
			_k14i9nd8("F" ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.MAX(_ev4xhht5 ,_dxpq0xkr )) ,ref _3nayvi7h ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_gdjumcqt) ,_p9n405a5 ,ref _ly9opahg );
			_uy2xc65y = (int)0;goto Mark70;
		}
		//* 
		
		_nk6sn86h = _o615qv2q("M" ,ref _ev4xhht5 ,ref _3nayvi7h ,_p9n405a5 ,ref _ly9opahg ,_dqanbbw3 );
		_958q0men = (int)0;
		if ((_nk6sn86h > _d0547bi2) & (_nk6sn86h < _bogm0gwy))
		{
			//* 
			//*        Scale matrix norm up to SMLNUM 
			//* 
			
			_j6h8q4u5("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _nk6sn86h ,ref _bogm0gwy ,ref _ev4xhht5 ,ref _3nayvi7h ,_p9n405a5 ,ref _ly9opahg ,ref _gro5yvfo );
			_958q0men = (int)1;
		}
		else
		if (_nk6sn86h > _av7j8yda)
		{
			//* 
			//*        Scale matrix norm down to BIGNUM 
			//* 
			
			_j6h8q4u5("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _nk6sn86h ,ref _av7j8yda ,ref _ev4xhht5 ,ref _3nayvi7h ,_p9n405a5 ,ref _ly9opahg ,ref _gro5yvfo );
			_958q0men = (int)2;
		}
		//* 
		//*     Compute QR factorization with column pivoting of A: 
		//*        A * P = Q * R 
		//* 
		
		_s006yt38(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_laipxa7w ,(_apig8meb+((int)1 - 1)),(_apig8meb+(_0hik27x4 + (int)1 - 1)),ref Unsafe.AsRef(_6fnxzlyp - _0hik27x4) ,_dqanbbw3 ,ref _gro5yvfo );
		_u2m2azut = (_0hik27x4 + ILNumerics.F2NET.Intrinsics.DBLE(*(_apig8meb+(_0hik27x4 + (int)1 - 1)) ));//* 
		//*     complex workspace: MN+NB*(N+1). real workspace 2*N. 
		//*     Details of Householder rotations stored in WORK(1:MN). 
		//* 
		//*     Determine RANK using incremental condition estimation 
		//* 
		
		*(_apig8meb+(_cxl8k730 - 1)) = _40vhxf9f;
		*(_apig8meb+(_l9j66b7a - 1)) = _40vhxf9f;
		_t7m1e103 = ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) );
		_rhnpgpoi = _t7m1e103;
		if (ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) ) == _d0547bi2)
		{
			
			_uy2xc65y = (int)0;
			_k14i9nd8("F" ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.MAX(_ev4xhht5 ,_dxpq0xkr )) ,ref _3nayvi7h ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_gdjumcqt) ,_p9n405a5 ,ref _ly9opahg );goto Mark70;
		}
		else
		{
			
			_uy2xc65y = (int)1;
		}
		//* 
		
Mark10:;
		// continue
		if (_uy2xc65y < _0hik27x4)
		{
			
			_b5p6od9s = (_uy2xc65y + (int)1);
			_663qlh4f(ref Unsafe.AsRef(_o836wfk2) ,ref _uy2xc65y ,(_apig8meb+(_cxl8k730 - 1)),ref _rhnpgpoi ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))) ,ref _s97x59xi ,ref _fmb4u5ka ,ref _o2zniltq );
			_663qlh4f(ref Unsafe.AsRef(_gmv1yrg4) ,ref _uy2xc65y ,(_apig8meb+(_l9j66b7a - 1)),ref _t7m1e103 ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))) ,ref _7yqs531w ,ref _slkbnmvx ,ref _h685tamv );//* 
			
			if ((_7yqs531w * _9zr5olpw) <= _s97x59xi)
			{
				
				{
					System.Int32 __81fgg2dlsvn2145 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2145 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2145;
					for (__81fgg2count2145 = System.Math.Max(0, (System.Int32)(((System.Int32)(_uy2xc65y) - __81fgg2dlsvn2145 + __81fgg2step2145) / __81fgg2step2145)), _b5p6od9s = __81fgg2dlsvn2145; __81fgg2count2145 != 0; __81fgg2count2145--, _b5p6od9s += (__81fgg2step2145)) {

					{
						
						*(_apig8meb+((_cxl8k730 + _b5p6od9s) - (int)1 - 1)) = (_fmb4u5ka * *(_apig8meb+((_cxl8k730 + _b5p6od9s) - (int)1 - 1)));
						*(_apig8meb+((_l9j66b7a + _b5p6od9s) - (int)1 - 1)) = (_slkbnmvx * *(_apig8meb+((_l9j66b7a + _b5p6od9s) - (int)1 - 1)));
Mark20:;
						// continue
					}
										}				}
				*(_apig8meb+(_cxl8k730 + _uy2xc65y - 1)) = _o2zniltq;
				*(_apig8meb+(_l9j66b7a + _uy2xc65y - 1)) = _h685tamv;
				_rhnpgpoi = _s97x59xi;
				_t7m1e103 = _7yqs531w;
				_uy2xc65y = (_uy2xc65y + (int)1);goto Mark10;
			}
			
		}
		//* 
		//*     complex workspace: 3*MN. 
		//* 
		//*     Logically partition R = [ R11 R12 ] 
		//*                             [  0  R22 ] 
		//*     where R11 = R(1:RANK,1:RANK) 
		//* 
		//*     [R11,R12] = [ T11, 0 ] * Y 
		//* 
		
		if (_uy2xc65y < _dxpq0xkr)
		_yzumtlkl(ref _uy2xc65y ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_0hik27x4 + (int)1 - 1)),(_apig8meb+(((int)2 * _0hik27x4) + (int)1 - 1)),ref Unsafe.AsRef(_6fnxzlyp - ((int)2 * _0hik27x4)) ,ref _gro5yvfo );//* 
		//*     complex workspace: 2*MN. 
		//*     Details of Householder rotations stored in WORK(MN+1:2*MN) 
		//* 
		//*     B(1:M,1:NRHS) := Q**H * B(1:M,1:NRHS) 
		//* 
		
		_1gd1avkg("Left" ,"Conjugate transpose" ,ref _ev4xhht5 ,ref _3nayvi7h ,ref _0hik27x4 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+((int)1 - 1)),_p9n405a5 ,ref _ly9opahg ,(_apig8meb+(((int)2 * _0hik27x4) + (int)1 - 1)),ref Unsafe.AsRef(_6fnxzlyp - ((int)2 * _0hik27x4)) ,ref _gro5yvfo );
		_u2m2azut = ILNumerics.F2NET.Intrinsics.MAX(_u2m2azut ,((int)2 * _0hik27x4) + ILNumerics.F2NET.Intrinsics.DBLE(*(_apig8meb+(((int)2 * _0hik27x4) + (int)1 - 1)) ) );//* 
		//*     complex workspace: 2*MN+NB*NRHS. 
		//* 
		//*     B(1:RANK,1:NRHS) := inv(T11) * B(1:RANK,1:NRHS) 
		//* 
		
		_qlsh8rhv("Left" ,"Upper" ,"No transpose" ,"Non-unit" ,ref _uy2xc65y ,ref _3nayvi7h ,ref Unsafe.AsRef(_40vhxf9f) ,_vxfgpup9 ,ref _ocv8fk5c ,_p9n405a5 ,ref _ly9opahg );//* 
		
		{
			System.Int32 __81fgg2dlsvn2146 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2146 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2146;
			for (__81fgg2count2146 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn2146 + __81fgg2step2146) / __81fgg2step2146)), _znpjgsef = __81fgg2dlsvn2146; __81fgg2count2146 != 0; __81fgg2count2146--, _znpjgsef += (__81fgg2step2146)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn2147 = (System.Int32)((_uy2xc65y + (int)1));
					const System.Int32 __81fgg2step2147 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2147;
					for (__81fgg2count2147 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2147 + __81fgg2step2147) / __81fgg2step2147)), _b5p6od9s = __81fgg2dlsvn2147; __81fgg2count2147 != 0; __81fgg2count2147--, _b5p6od9s += (__81fgg2step2147)) {

					{
						
						*(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)) = _gdjumcqt;
Mark30:;
						// continue
					}
										}				}
Mark40:;
				// continue
			}
						}		}//* 
		//*     B(1:N,1:NRHS) := Y**H * B(1:N,1:NRHS) 
		//* 
		
		if (_uy2xc65y < _dxpq0xkr)
		{
			
			_x48lx74v("Left" ,"Conjugate transpose" ,ref _dxpq0xkr ,ref _3nayvi7h ,ref _uy2xc65y ,ref Unsafe.AsRef(_dxpq0xkr - _uy2xc65y) ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_0hik27x4 + (int)1 - 1)),_p9n405a5 ,ref _ly9opahg ,(_apig8meb+(((int)2 * _0hik27x4) + (int)1 - 1)),ref Unsafe.AsRef(_6fnxzlyp - ((int)2 * _0hik27x4)) ,ref _gro5yvfo );
		}
		//* 
		//*     complex workspace: 2*MN+NRHS. 
		//* 
		//*     B(1:N,1:NRHS) := P * B(1:N,1:NRHS) 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn2148 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2148 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2148;
			for (__81fgg2count2148 = System.Math.Max(0, (System.Int32)(((System.Int32)(_3nayvi7h) - __81fgg2dlsvn2148 + __81fgg2step2148) / __81fgg2step2148)), _znpjgsef = __81fgg2dlsvn2148; __81fgg2count2148 != 0; __81fgg2count2148--, _znpjgsef += (__81fgg2step2148)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn2149 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2149 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2149;
					for (__81fgg2count2149 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2149 + __81fgg2step2149) / __81fgg2step2149)), _b5p6od9s = __81fgg2dlsvn2149; __81fgg2count2149 != 0; __81fgg2count2149--, _b5p6od9s += (__81fgg2step2149)) {

					{
						
						*(_apig8meb+(*(_laipxa7w+(_b5p6od9s - 1)) - 1)) = *(_p9n405a5+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg));
Mark50:;
						// continue
					}
										}				}
				_ly902k7t(ref _dxpq0xkr ,(_apig8meb+((int)1 - 1)),ref Unsafe.AsRef((int)1) ,(_p9n405a5+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ly9opahg)),ref Unsafe.AsRef((int)1) );
Mark60:;
				// continue
			}
						}		}//* 
		//*     complex workspace: N. 
		//* 
		//*     Undo scaling 
		//* 
		
		if (_ddt9utmg == (int)1)
		{
			
			_j6h8q4u5("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _j6vjow1g ,ref _bogm0gwy ,ref _dxpq0xkr ,ref _3nayvi7h ,_p9n405a5 ,ref _ly9opahg ,ref _gro5yvfo );
			_j6h8q4u5("U" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _bogm0gwy ,ref _j6vjow1g ,ref _uy2xc65y ,ref _uy2xc65y ,_vxfgpup9 ,ref _ocv8fk5c ,ref _gro5yvfo );
		}
		else
		if (_ddt9utmg == (int)2)
		{
			
			_j6h8q4u5("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _j6vjow1g ,ref _av7j8yda ,ref _dxpq0xkr ,ref _3nayvi7h ,_p9n405a5 ,ref _ly9opahg ,ref _gro5yvfo );
			_j6h8q4u5("U" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _av7j8yda ,ref _j6vjow1g ,ref _uy2xc65y ,ref _uy2xc65y ,_vxfgpup9 ,ref _ocv8fk5c ,ref _gro5yvfo );
		}
		
		if (_958q0men == (int)1)
		{
			
			_j6h8q4u5("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _bogm0gwy ,ref _nk6sn86h ,ref _dxpq0xkr ,ref _3nayvi7h ,_p9n405a5 ,ref _ly9opahg ,ref _gro5yvfo );
		}
		else
		if (_958q0men == (int)2)
		{
			
			_j6h8q4u5("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _av7j8yda ,ref _nk6sn86h ,ref _dxpq0xkr ,ref _3nayvi7h ,_p9n405a5 ,ref _ly9opahg ,ref _gro5yvfo );
		}
		//* 
		
Mark70:;
		// continue
		*(_apig8meb+((int)1 - 1)) = ILNumerics.F2NET.Intrinsics.DCMPLX(_e4ueamrn );//* 
		
		return;//* 
		//*     End of ZGELSY 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
