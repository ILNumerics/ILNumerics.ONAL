
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
//*> \brief <b> ZGELSD computes the minimum-norm solution to a linear least squares problem for GE matrices</b> 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZGELSD + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zgelsd.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zgelsd.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zgelsd.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZGELSD( M, N, NRHS, A, LDA, B, LDB, S, RCOND, RANK, 
//*                          WORK, LWORK, RWORK, IWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, LDA, LDB, LWORK, M, N, NRHS, RANK 
//*       DOUBLE PRECISION   RCOND 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IWORK( * ) 
//*       DOUBLE PRECISION   RWORK( * ), S( * ) 
//*       COMPLEX*16         A( LDA, * ), B( LDB, * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZGELSD computes the minimum-norm solution to a real linear least 
//*> squares problem: 
//*>     minimize 2-norm(| b - A*x |) 
//*> using the singular value decomposition (SVD) of A. A is an M-by-N 
//*> matrix which may be rank-deficient. 
//*> 
//*> Several right hand side vectors b and solution vectors x can be 
//*> handled in a single call; they are stored as the columns of the 
//*> M-by-NRHS right hand side matrix B and the N-by-NRHS solution 
//*> matrix X. 
//*> 
//*> The problem is solved in three steps: 
//*> (1) Reduce the coefficient matrix A to bidiagonal form with 
//*>     Householder transformations, reducing the original problem 
//*>     into a "bidiagonal least squares problem" (BLS) 
//*> (2) Solve the BLS using a divide and conquer approach. 
//*> (3) Apply back all the Householder transformations to solve 
//*>     the original least squares problem. 
//*> 
//*> The effective rank of A is determined by treating as zero those 
//*> singular values which are less than RCOND times the largest singular 
//*> value. 
//*> 
//*> The divide and conquer algorithm makes very mild assumptions about 
//*> floating point arithmetic. It will work on machines with a guard 
//*> digit in add/subtract, or on those binary machines without guard 
//*> digits which subtract like the Cray X-MP, Cray Y-MP, Cray C-90, or 
//*> Cray-2. It could conceivably fail on hexadecimal or decimal machines 
//*> without guard digits, but we know of none. 
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
//*> \param[in] NRHS 
//*> \verbatim 
//*>          NRHS is INTEGER 
//*>          The number of right hand sides, i.e., the number of columns 
//*>          of the matrices B and X. NRHS >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension (LDA,N) 
//*>          On entry, the M-by-N matrix A. 
//*>          On exit, A has been destroyed. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A. LDA >= max(1,M). 
//*> \endverbatim 
//*> 
//*> \param[in,out] B 
//*> \verbatim 
//*>          B is COMPLEX*16 array, dimension (LDB,NRHS) 
//*>          On entry, the M-by-NRHS right hand side matrix B. 
//*>          On exit, B is overwritten by the N-by-NRHS solution matrix X. 
//*>          If m >= n and RANK = n, the residual sum-of-squares for 
//*>          the solution in the i-th column is given by the sum of 
//*>          squares of the modulus of elements n+1:m in that column. 
//*> \endverbatim 
//*> 
//*> \param[in] LDB 
//*> \verbatim 
//*>          LDB is INTEGER 
//*>          The leading dimension of the array B.  LDB >= max(1,M,N). 
//*> \endverbatim 
//*> 
//*> \param[out] S 
//*> \verbatim 
//*>          S is DOUBLE PRECISION array, dimension (min(M,N)) 
//*>          The singular values of A in decreasing order. 
//*>          The condition number of A in the 2-norm = S(1)/S(min(m,n)). 
//*> \endverbatim 
//*> 
//*> \param[in] RCOND 
//*> \verbatim 
//*>          RCOND is DOUBLE PRECISION 
//*>          RCOND is used to determine the effective rank of A. 
//*>          Singular values S(i) <= RCOND*S(1) are treated as zero. 
//*>          If RCOND < 0, machine precision is used instead. 
//*> \endverbatim 
//*> 
//*> \param[out] RANK 
//*> \verbatim 
//*>          RANK is INTEGER 
//*>          The effective rank of A, i.e., the number of singular values 
//*>          which are greater than RCOND*S(1). 
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
//*>          The dimension of the array WORK. LWORK must be at least 1. 
//*>          The exact minimum amount of workspace needed depends on M, 
//*>          N and NRHS. As long as LWORK is at least 
//*>              2*N + N*NRHS 
//*>          if M is greater than or equal to N or 
//*>              2*M + M*NRHS 
//*>          if M is less than N, the code will execute correctly. 
//*>          For good performance, LWORK should generally be larger. 
//*> 
//*>          If LWORK = -1, then a workspace query is assumed; the routine 
//*>          only calculates the optimal size of the array WORK and the 
//*>          minimum sizes of the arrays RWORK and IWORK, and returns 
//*>          these values as the first entries of the WORK, RWORK and 
//*>          IWORK arrays, and no error message related to LWORK is issued 
//*>          by XERBLA. 
//*> \endverbatim 
//*> 
//*> \param[out] RWORK 
//*> \verbatim 
//*>          RWORK is DOUBLE PRECISION array, dimension (MAX(1,LRWORK)) 
//*>          LRWORK >= 
//*>             10*N + 2*N*SMLSIZ + 8*N*NLVL + 3*SMLSIZ*NRHS + 
//*>             MAX( (SMLSIZ+1)**2, N*(1+NRHS) + 2*NRHS ) 
//*>          if M is greater than or equal to N or 
//*>             10*M + 2*M*SMLSIZ + 8*M*NLVL + 3*SMLSIZ*NRHS + 
//*>             MAX( (SMLSIZ+1)**2, N*(1+NRHS) + 2*NRHS ) 
//*>          if M is less than N, the code will execute correctly. 
//*>          SMLSIZ is returned by ILAENV and is equal to the maximum 
//*>          size of the subproblems at the bottom of the computation 
//*>          tree (usually about 25), and 
//*>             NLVL = MAX( 0, INT( LOG_2( MIN( M,N )/(SMLSIZ+1) ) ) + 1 ) 
//*>          On exit, if INFO = 0, RWORK(1) returns the minimum LRWORK. 
//*> \endverbatim 
//*> 
//*> \param[out] IWORK 
//*> \verbatim 
//*>          IWORK is INTEGER array, dimension (MAX(1,LIWORK)) 
//*>          LIWORK >= max(1, 3*MINMN*NLVL + 11*MINMN), 
//*>          where MINMN = MIN( M,N ). 
//*>          On exit, if INFO = 0, IWORK(1) returns the minimum LIWORK. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0: successful exit 
//*>          < 0: if INFO = -i, the i-th argument had an illegal value. 
//*>          > 0:  the algorithm for computing the SVD failed to converge; 
//*>                if INFO = i, i off-diagonal elements of an intermediate 
//*>                bidiagonal form did not converge to zero. 
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
//*> \date June 2017 
//* 
//*> \ingroup complex16GEsolve 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>     Ming Gu and Ren-Cang Li, Computer Science Division, University of 
//*>       California at Berkeley, USA \n 
//*>     Osni Marques, LBNL/NERSC, USA \n 
//* 
//*  ===================================================================== 

	 
	public static void _otp4ja5r(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref Int32 _3nayvi7h, complex* _vxfgpup9, ref Int32 _ocv8fk5c, complex* _p9n405a5, ref Int32 _ly9opahg, Double* _irk8i6qr, ref Double _9zr5olpw, ref Int32 _uy2xc65y, complex* _apig8meb, ref Int32 _6fnxzlyp, Double* _dqanbbw3, Int32* _4b6rt45i, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Double _5m0mjfxm =  2d;
complex _gdjumcqt =   new fcomplex(0f,0f);
Boolean _lhlgm7z5 =  default;
Int32 _ddt9utmg =  default;
Int32 _958q0men =  default;
Int32 _smxeww0r =  default;
Int32 _ic6kua09 =  default;
Int32 _q1w15vsx =  default;
Int32 _5ke1jwwr =  default;
Int32 _wx1x93f0 =  default;
Int32 _iykhdriq =  default;
Int32 _29mhiasb =  default;
Int32 _1jkrnd6f =  default;
Int32 _qzrhazkr =  default;
Int32 _tafa1evd =  default;
Int32 _qaseb1y7 =  default;
Int32 _gghrqcr1 =  default;
Int32 _e9y2lltf =  default;
Int32 _lhzduysr =  default;
Int32 _0n683y3x =  default;
Int32 _r49fp4o3 =  default;
Int32 _1myocm5q =  default;
Int32 _q1xpyios =  default;
Double _j6vjow1g =  default;
Double _av7j8yda =  default;
Double _nk6sn86h =  default;
Double _p1iqarg6 =  default;
Double _ptpa0vax =  default;
Double _bogm0gwy =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK driver routine (version 3.7.1) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     June 2017 
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
		//*     Test the input arguments. 
		//* 
		
		_gro5yvfo = (int)0;
		_qaseb1y7 = ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_dxpq0xkr );
		_qzrhazkr = ILNumerics.F2NET.Intrinsics.MAX(_ev4xhht5 ,_dxpq0xkr );
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
		if (_ly9opahg < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_qzrhazkr ))
		{
			
			_gro5yvfo = (int)-7;
		}
		//* 
		//*     Compute workspace. 
		//*     (Note: Comments in the code beginning "Workspace:" describe the 
		//*     minimal amount of workspace needed at that point in the code, 
		//*     as well as the preferred amount for good performance. 
		//*     NB refers to the optimal block size for the immediately 
		//*     following subroutine, as returned by ILAENV.) 
		//* 
		
		if (_gro5yvfo == (int)0)
		{
			
			_gghrqcr1 = (int)1;
			_tafa1evd = (int)1;
			_29mhiasb = (int)1;
			_1jkrnd6f = (int)1;
			if (_qaseb1y7 > (int)0)
			{
				
				_q1xpyios = _4mvd6e4d(ref Unsafe.AsRef((int)9) ,"ZGELSD" ," " ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) );
				_lhzduysr = _4mvd6e4d(ref Unsafe.AsRef((int)6) ,"ZGELSD" ," " ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref _3nayvi7h ,ref Unsafe.AsRef((int)-1) );
				_0n683y3x = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.DBLE(_qaseb1y7 ) / ILNumerics.F2NET.Intrinsics.DBLE(_q1xpyios + (int)1 ) ) / ILNumerics.F2NET.Intrinsics.LOG(_5m0mjfxm ) ) + (int)1 ,(int)0 );
				_29mhiasb = ((((int)3 * _qaseb1y7) * _0n683y3x) + ((int)11 * _qaseb1y7));
				_e9y2lltf = _ev4xhht5;
				if ((_ev4xhht5 >= _dxpq0xkr) & (_ev4xhht5 >= _lhzduysr))
				{
					//* 
					//*              Path 1a - overdetermined, with many more rows than 
					//*                        columns. 
					//* 
					
					_e9y2lltf = _dxpq0xkr;
					_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,_dxpq0xkr * _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZGEQRF" ," " ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ) );
					_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,_3nayvi7h * _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZUNMQR" ,"LC" ,ref _ev4xhht5 ,ref _3nayvi7h ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ) );
				}
				
				if (_ev4xhht5 >= _dxpq0xkr)
				{
					//* 
					//*              Path 1 - overdetermined or exactly determined. 
					//* 
					
					_1jkrnd6f = ((((((int)10 * _dxpq0xkr) + (((int)2 * _dxpq0xkr) * _q1xpyios)) + (((int)8 * _dxpq0xkr) * _0n683y3x)) + (((int)3 * _q1xpyios) * _3nayvi7h)) + ILNumerics.F2NET.Intrinsics.MAX(__POW2((_q1xpyios + (int)1)) ,(_dxpq0xkr * ((int)1 + _3nayvi7h)) + ((int)2 * _3nayvi7h) ));
					_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _dxpq0xkr) + ((_e9y2lltf + _dxpq0xkr) * _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZGEBRD" ," " ,ref _e9y2lltf ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) )) );
					_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _dxpq0xkr) + (_3nayvi7h * _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZUNMBR" ,"QLC" ,ref _e9y2lltf ,ref _3nayvi7h ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) )) );
					_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _dxpq0xkr) + ((_dxpq0xkr - (int)1) * _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZUNMBR" ,"PLN" ,ref _dxpq0xkr ,ref _3nayvi7h ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) )) );
					_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _dxpq0xkr) + (_dxpq0xkr * _3nayvi7h) );
					_gghrqcr1 = ILNumerics.F2NET.Intrinsics.MAX(((int)2 * _dxpq0xkr) + _e9y2lltf ,((int)2 * _dxpq0xkr) + (_dxpq0xkr * _3nayvi7h) );
				}
				
				if (_dxpq0xkr > _ev4xhht5)
				{
					
					_1jkrnd6f = ((((((int)10 * _ev4xhht5) + (((int)2 * _ev4xhht5) * _q1xpyios)) + (((int)8 * _ev4xhht5) * _0n683y3x)) + (((int)3 * _q1xpyios) * _3nayvi7h)) + ILNumerics.F2NET.Intrinsics.MAX(__POW2((_q1xpyios + (int)1)) ,(_dxpq0xkr * ((int)1 + _3nayvi7h)) + ((int)2 * _3nayvi7h) ));
					if (_dxpq0xkr >= _lhzduysr)
					{
						//* 
						//*                 Path 2a - underdetermined, with many more columns 
						//*                           than rows. 
						//* 
						
						_tafa1evd = (_ev4xhht5 + (_ev4xhht5 * _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZGELQF" ," " ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) )));
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((_ev4xhht5 * _ev4xhht5) + ((int)4 * _ev4xhht5)) + (((int)2 * _ev4xhht5) * _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZGEBRD" ," " ,ref _ev4xhht5 ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) )) );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((_ev4xhht5 * _ev4xhht5) + ((int)4 * _ev4xhht5)) + (_3nayvi7h * _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZUNMBR" ,"QLC" ,ref _ev4xhht5 ,ref _3nayvi7h ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)-1) )) );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((_ev4xhht5 * _ev4xhht5) + ((int)4 * _ev4xhht5)) + ((_ev4xhht5 - (int)1) * _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZUNMLQ" ,"LC" ,ref _dxpq0xkr ,ref _3nayvi7h ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)-1) )) );
						if (_3nayvi7h > (int)1)
						{
							
							_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((_ev4xhht5 * _ev4xhht5) + _ev4xhht5) + (_ev4xhht5 * _3nayvi7h) );
						}
						else
						{
							
							_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,(_ev4xhht5 * _ev4xhht5) + ((int)2 * _ev4xhht5) );
						}
						
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((_ev4xhht5 * _ev4xhht5) + ((int)4 * _ev4xhht5)) + (_ev4xhht5 * _3nayvi7h) );//!     XXX: Ensure the Path 2a case below is triggered.  The workspace 
						//!     calculation should use queries for all routines eventually. 
						
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,(((int)4 * _ev4xhht5) + (_ev4xhht5 * _ev4xhht5)) + ILNumerics.F2NET.Intrinsics.MAX(_ev4xhht5 ,((int)2 * _ev4xhht5) - (int)4 ,_3nayvi7h ,_dxpq0xkr - ((int)3 * _ev4xhht5) ) );
					}
					else
					{
						//* 
						//*                 Path 2 - underdetermined. 
						//* 
						
						_tafa1evd = (((int)2 * _ev4xhht5) + ((_dxpq0xkr + _ev4xhht5) * _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZGEBRD" ," " ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) )));
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _ev4xhht5) + (_3nayvi7h * _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZUNMBR" ,"QLC" ,ref _ev4xhht5 ,ref _3nayvi7h ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)-1) )) );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _ev4xhht5) + (_ev4xhht5 * _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZUNMBR" ,"PLN" ,ref _dxpq0xkr ,ref _3nayvi7h ,ref _ev4xhht5 ,ref Unsafe.AsRef((int)-1) )) );
						_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,((int)2 * _ev4xhht5) + (_ev4xhht5 * _3nayvi7h) );
					}
					
					_gghrqcr1 = ILNumerics.F2NET.Intrinsics.MAX(((int)2 * _ev4xhht5) + _dxpq0xkr ,((int)2 * _ev4xhht5) + (_ev4xhht5 * _3nayvi7h) );
				}
				
			}
			
			_gghrqcr1 = ILNumerics.F2NET.Intrinsics.MIN(_gghrqcr1 ,_tafa1evd );
			*(_apig8meb+((int)1 - 1)) = DCMPLX(_tafa1evd);
			*(_4b6rt45i+((int)1 - 1)) = _29mhiasb;
			*(_dqanbbw3+((int)1 - 1)) = DBLE(_1jkrnd6f);//* 
			
			if ((_6fnxzlyp < _gghrqcr1) & (!(_lhlgm7z5)))
			{
				
				_gro5yvfo = (int)-12;
			}
			
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("ZGELSD" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		else
		if (_lhlgm7z5)
		{
			
			return;
		}
		//* 
		//*     Quick return if possible. 
		//* 
		
		if ((_ev4xhht5 == (int)0) | (_dxpq0xkr == (int)0))
		{
			
			_uy2xc65y = (int)0;
			return;
		}
		//* 
		//*     Get machine parameters. 
		//* 
		
		_p1iqarg6 = _f43eg0w0("P" );
		_ptpa0vax = _f43eg0w0("S" );
		_bogm0gwy = (_ptpa0vax / _p1iqarg6);
		_av7j8yda = (_kxg5drh2 / _bogm0gwy);
		_to4dtyqc(ref _bogm0gwy ,ref _av7j8yda );//* 
		//*     Scale A if max entry outside range [SMLNUM,BIGNUM]. 
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
			//*        Scale matrix norm down to BIGNUM. 
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
			_rta9tuwm("F" ,ref _qaseb1y7 ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,_irk8i6qr ,ref Unsafe.AsRef((int)1) );
			_uy2xc65y = (int)0;goto Mark10;
		}
		//* 
		//*     Scale B if max entry outside range [SMLNUM,BIGNUM]. 
		//* 
		
		_nk6sn86h = _o615qv2q("M" ,ref _ev4xhht5 ,ref _3nayvi7h ,_p9n405a5 ,ref _ly9opahg ,_dqanbbw3 );
		_958q0men = (int)0;
		if ((_nk6sn86h > _d0547bi2) & (_nk6sn86h < _bogm0gwy))
		{
			//* 
			//*        Scale matrix norm up to SMLNUM. 
			//* 
			
			_j6h8q4u5("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _nk6sn86h ,ref _bogm0gwy ,ref _ev4xhht5 ,ref _3nayvi7h ,_p9n405a5 ,ref _ly9opahg ,ref _gro5yvfo );
			_958q0men = (int)1;
		}
		else
		if (_nk6sn86h > _av7j8yda)
		{
			//* 
			//*        Scale matrix norm down to BIGNUM. 
			//* 
			
			_j6h8q4u5("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _nk6sn86h ,ref _av7j8yda ,ref _ev4xhht5 ,ref _3nayvi7h ,_p9n405a5 ,ref _ly9opahg ,ref _gro5yvfo );
			_958q0men = (int)2;
		}
		//* 
		//*     If M < N make sure B(M+1:N,:) = 0 
		//* 
		
		if (_ev4xhht5 < _dxpq0xkr)
		_k14i9nd8("F" ,ref Unsafe.AsRef(_dxpq0xkr - _ev4xhht5) ,ref _3nayvi7h ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_gdjumcqt) ,(_p9n405a5+(_ev4xhht5 + (int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg );//* 
		//*     Overdetermined case. 
		//* 
		
		if (_ev4xhht5 >= _dxpq0xkr)
		{
			//* 
			//*        Path 1 - overdetermined or exactly determined. 
			//* 
			
			_e9y2lltf = _ev4xhht5;
			if (_ev4xhht5 >= _lhzduysr)
			{
				//* 
				//*           Path 1a - overdetermined, with many more rows than columns 
				//* 
				
				_e9y2lltf = _dxpq0xkr;
				_q1w15vsx = (int)1;
				_1myocm5q = (_q1w15vsx + _dxpq0xkr);//* 
				//*           Compute A=Q*R. 
				//*           (RWorkspace: need N) 
				//*           (CWorkspace: need N, prefer N*NB) 
				//* 
				
				_ljflx7wo(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _gro5yvfo );//* 
				//*           Multiply B by transpose(Q). 
				//*           (RWorkspace: need N) 
				//*           (CWorkspace: need NRHS, prefer NRHS*NB) 
				//* 
				
				_1gd1avkg("L" ,"C" ,ref _ev4xhht5 ,ref _3nayvi7h ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),_p9n405a5 ,ref _ly9opahg ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _gro5yvfo );//* 
				//*           Zero out below R. 
				//* 
				
				if (_dxpq0xkr > (int)1)
				{
					
					_k14i9nd8("L" ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_gdjumcqt) ,(_vxfgpup9+((int)2 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
				}
				
			}
			//* 
			
			_wx1x93f0 = (int)1;
			_5ke1jwwr = (_wx1x93f0 + _dxpq0xkr);
			_1myocm5q = (_5ke1jwwr + _dxpq0xkr);
			_smxeww0r = (int)1;
			_r49fp4o3 = (_smxeww0r + _dxpq0xkr);//* 
			//*        Bidiagonalize R in A. 
			//*        (RWorkspace: need N) 
			//*        (CWorkspace: need 2*N+MM, prefer 2*N+(MM+N)*NB) 
			//* 
			
			_w8dh0c16(ref _e9y2lltf ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _gro5yvfo );//* 
			//*        Multiply B by transpose of left bidiagonalizing vectors of R. 
			//*        (CWorkspace: need 2*N+NRHS, prefer 2*N+NRHS*NB) 
			//* 
			
			_2xvhm1xm("Q" ,"L" ,"C" ,ref _e9y2lltf ,ref _3nayvi7h ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),_p9n405a5 ,ref _ly9opahg ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _gro5yvfo );//* 
			//*        Solve the bidiagonal least squares problem. 
			//* 
			
			_fw6ai0ms("U" ,ref _q1xpyios ,ref _dxpq0xkr ,ref _3nayvi7h ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),_p9n405a5 ,ref _ly9opahg ,ref _9zr5olpw ,ref _uy2xc65y ,(_apig8meb+(_1myocm5q - 1)),(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );
			if (_gro5yvfo != (int)0)
			{
				goto Mark10;
			}
			//* 
			//*        Multiply B by right bidiagonalizing vectors of R. 
			//* 
			
			_2xvhm1xm("P" ,"L" ,"N" ,ref _dxpq0xkr ,ref _3nayvi7h ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),_p9n405a5 ,ref _ly9opahg ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _gro5yvfo );//* 
			
		}
		else
		if ((_dxpq0xkr >= _lhzduysr) & (_6fnxzlyp >= ((((int)4 * _ev4xhht5) + (_ev4xhht5 * _ev4xhht5)) + ILNumerics.F2NET.Intrinsics.MAX(_ev4xhht5 ,((int)2 * _ev4xhht5) - (int)4 ,_3nayvi7h ,_dxpq0xkr - ((int)3 * _ev4xhht5) ))))
		{
			//* 
			//*        Path 2a - underdetermined, with many more columns than rows 
			//*        and sufficient workspace for an efficient algorithm. 
			//* 
			
			_iykhdriq = _ev4xhht5;
			if (_6fnxzlyp >= ILNumerics.F2NET.Intrinsics.MAX((((int)4 * _ev4xhht5) + (_ev4xhht5 * _ocv8fk5c)) + ILNumerics.F2NET.Intrinsics.MAX(_ev4xhht5 ,((int)2 * _ev4xhht5) - (int)4 ,_3nayvi7h ,_dxpq0xkr - ((int)3 * _ev4xhht5) ) ,((_ev4xhht5 * _ocv8fk5c) + _ev4xhht5) + (_ev4xhht5 * _3nayvi7h) ))
			_iykhdriq = _ocv8fk5c;
			_q1w15vsx = (int)1;
			_1myocm5q = (_ev4xhht5 + (int)1);//* 
			//*        Compute A=L*Q. 
			//*        (CWorkspace: need 2*M, prefer M+M*NB) 
			//* 
			
			_nsco3b9r(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _gro5yvfo );
			_ic6kua09 = _1myocm5q;//* 
			//*        Copy L to WORK(IL), zeroing out above its diagonal. 
			//* 
			
			_nihu9ses("L" ,ref _ev4xhht5 ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_ic6kua09 - 1)),ref _iykhdriq );
			_k14i9nd8("U" ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_gdjumcqt) ,(_apig8meb+(_ic6kua09 + _iykhdriq - 1)),ref _iykhdriq );
			_wx1x93f0 = (_ic6kua09 + (_iykhdriq * _ev4xhht5));
			_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
			_1myocm5q = (_5ke1jwwr + _ev4xhht5);
			_smxeww0r = (int)1;
			_r49fp4o3 = (_smxeww0r + _ev4xhht5);//* 
			//*        Bidiagonalize L in WORK(IL). 
			//*        (RWorkspace: need M) 
			//*        (CWorkspace: need M*M+4*M, prefer M*M+4*M+2*M*NB) 
			//* 
			
			_w8dh0c16(ref _ev4xhht5 ,ref _ev4xhht5 ,(_apig8meb+(_ic6kua09 - 1)),ref _iykhdriq ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _gro5yvfo );//* 
			//*        Multiply B by transpose of left bidiagonalizing vectors of L. 
			//*        (CWorkspace: need M*M+4*M+NRHS, prefer M*M+4*M+NRHS*NB) 
			//* 
			
			_2xvhm1xm("Q" ,"L" ,"C" ,ref _ev4xhht5 ,ref _3nayvi7h ,ref _ev4xhht5 ,(_apig8meb+(_ic6kua09 - 1)),ref _iykhdriq ,(_apig8meb+(_wx1x93f0 - 1)),_p9n405a5 ,ref _ly9opahg ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _gro5yvfo );//* 
			//*        Solve the bidiagonal least squares problem. 
			//* 
			
			_fw6ai0ms("U" ,ref _q1xpyios ,ref _ev4xhht5 ,ref _3nayvi7h ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),_p9n405a5 ,ref _ly9opahg ,ref _9zr5olpw ,ref _uy2xc65y ,(_apig8meb+(_1myocm5q - 1)),(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );
			if (_gro5yvfo != (int)0)
			{
				goto Mark10;
			}
			//* 
			//*        Multiply B by right bidiagonalizing vectors of L. 
			//* 
			
			_2xvhm1xm("P" ,"L" ,"N" ,ref _ev4xhht5 ,ref _3nayvi7h ,ref _ev4xhht5 ,(_apig8meb+(_ic6kua09 - 1)),ref _iykhdriq ,(_apig8meb+(_5ke1jwwr - 1)),_p9n405a5 ,ref _ly9opahg ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _gro5yvfo );//* 
			//*        Zero out below first M rows of B. 
			//* 
			
			_k14i9nd8("F" ,ref Unsafe.AsRef(_dxpq0xkr - _ev4xhht5) ,ref _3nayvi7h ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_gdjumcqt) ,(_p9n405a5+(_ev4xhht5 + (int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)),ref _ly9opahg );
			_1myocm5q = (_q1w15vsx + _ev4xhht5);//* 
			//*        Multiply transpose(Q) by B. 
			//*        (CWorkspace: need NRHS, prefer NRHS*NB) 
			//* 
			
			_tzs01qev("L" ,"C" ,ref _dxpq0xkr ,ref _3nayvi7h ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),_p9n405a5 ,ref _ly9opahg ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _gro5yvfo );//* 
			
		}
		else
		{
			//* 
			//*        Path 2 - remaining underdetermined cases. 
			//* 
			
			_wx1x93f0 = (int)1;
			_5ke1jwwr = (_wx1x93f0 + _ev4xhht5);
			_1myocm5q = (_5ke1jwwr + _ev4xhht5);
			_smxeww0r = (int)1;
			_r49fp4o3 = (_smxeww0r + _ev4xhht5);//* 
			//*        Bidiagonalize A. 
			//*        (RWorkspace: need M) 
			//*        (CWorkspace: need 2*M+N, prefer 2*M+(M+N)*NB) 
			//* 
			
			_w8dh0c16(ref _ev4xhht5 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),(_apig8meb+(_wx1x93f0 - 1)),(_apig8meb+(_5ke1jwwr - 1)),(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _gro5yvfo );//* 
			//*        Multiply B by transpose of left bidiagonalizing vectors. 
			//*        (CWorkspace: need 2*M+NRHS, prefer 2*M+NRHS*NB) 
			//* 
			
			_2xvhm1xm("Q" ,"L" ,"C" ,ref _ev4xhht5 ,ref _3nayvi7h ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_wx1x93f0 - 1)),_p9n405a5 ,ref _ly9opahg ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _gro5yvfo );//* 
			//*        Solve the bidiagonal least squares problem. 
			//* 
			
			_fw6ai0ms("L" ,ref _q1xpyios ,ref _ev4xhht5 ,ref _3nayvi7h ,_irk8i6qr ,(_dqanbbw3+(_smxeww0r - 1)),_p9n405a5 ,ref _ly9opahg ,ref _9zr5olpw ,ref _uy2xc65y ,(_apig8meb+(_1myocm5q - 1)),(_dqanbbw3+(_r49fp4o3 - 1)),_4b6rt45i ,ref _gro5yvfo );
			if (_gro5yvfo != (int)0)
			{
				goto Mark10;
			}
			//* 
			//*        Multiply B by right bidiagonalizing vectors of A. 
			//* 
			
			_2xvhm1xm("P" ,"L" ,"N" ,ref _dxpq0xkr ,ref _3nayvi7h ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_5ke1jwwr - 1)),_p9n405a5 ,ref _ly9opahg ,(_apig8meb+(_1myocm5q - 1)),ref Unsafe.AsRef((_6fnxzlyp - _1myocm5q) + (int)1) ,ref _gro5yvfo );//* 
			
		}
		//* 
		//*     Undo scaling. 
		//* 
		
		if (_ddt9utmg == (int)1)
		{
			
			_j6h8q4u5("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _j6vjow1g ,ref _bogm0gwy ,ref _dxpq0xkr ,ref _3nayvi7h ,_p9n405a5 ,ref _ly9opahg ,ref _gro5yvfo );
			_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _bogm0gwy ,ref _j6vjow1g ,ref _qaseb1y7 ,ref Unsafe.AsRef((int)1) ,_irk8i6qr ,ref _qaseb1y7 ,ref _gro5yvfo );
		}
		else
		if (_ddt9utmg == (int)2)
		{
			
			_j6h8q4u5("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _j6vjow1g ,ref _av7j8yda ,ref _dxpq0xkr ,ref _3nayvi7h ,_p9n405a5 ,ref _ly9opahg ,ref _gro5yvfo );
			_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _av7j8yda ,ref _j6vjow1g ,ref _qaseb1y7 ,ref Unsafe.AsRef((int)1) ,_irk8i6qr ,ref _qaseb1y7 ,ref _gro5yvfo );
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
		
Mark10:;
		// continue
		*(_apig8meb+((int)1 - 1)) = DCMPLX(_tafa1evd);
		*(_4b6rt45i+((int)1 - 1)) = _29mhiasb;
		*(_dqanbbw3+((int)1 - 1)) = DBLE(_1jkrnd6f);
		return;//* 
		//*     End of ZGELSD 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
