
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
//*> \brief \b CTREVC3 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CTREVC3 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/ctrevc3.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/ctrevc3.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/ctrevc3.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CTREVC3( SIDE, HOWMNY, SELECT, N, T, LDT, VL, LDVL, VR, 
//*                           LDVR, MM, M, WORK, LWORK, RWORK, LRWORK, INFO) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          HOWMNY, SIDE 
//*       INTEGER            INFO, LDT, LDVL, LDVR, LWORK, M, MM, N 
//*       .. 
//*       .. Array Arguments .. 
//*       LOGICAL            SELECT( * ) 
//*       REAL               RWORK( * ) 
//*       COMPLEX            T( LDT, * ), VL( LDVL, * ), VR( LDVR, * ), 
//*      $                   WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CTREVC3 computes some or all of the right and/or left eigenvectors of 
//*> a complex upper triangular matrix T. 
//*> Matrices of this type are produced by the Schur factorization of 
//*> a complex general matrix:  A = Q*T*Q**H, as computed by CHSEQR. 
//*> 
//*> The right eigenvector x and the left eigenvector y of T corresponding 
//*> to an eigenvalue w are defined by: 
//*> 
//*>              T*x = w*x,     (y**H)*T = w*(y**H) 
//*> 
//*> where y**H denotes the conjugate transpose of the vector y. 
//*> The eigenvalues are not input to this routine, but are read directly 
//*> from the diagonal of T. 
//*> 
//*> This routine returns the matrices X and/or Y of right and left 
//*> eigenvectors of T, or the products Q*X and/or Q*Y, where Q is an 
//*> input matrix. If Q is the unitary factor that reduces a matrix A to 
//*> Schur form T, then Q*X and Q*Y are the matrices of right and left 
//*> eigenvectors of A. 
//*> 
//*> This uses a Level 3 BLAS version of the back transformation. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] SIDE 
//*> \verbatim 
//*>          SIDE is CHARACTER*1 
//*>          = 'R':  compute right eigenvectors only; 
//*>          = 'L':  compute left eigenvectors only; 
//*>          = 'B':  compute both right and left eigenvectors. 
//*> \endverbatim 
//*> 
//*> \param[in] HOWMNY 
//*> \verbatim 
//*>          HOWMNY is CHARACTER*1 
//*>          = 'A':  compute all right and/or left eigenvectors; 
//*>          = 'B':  compute all right and/or left eigenvectors, 
//*>                  backtransformed using the matrices supplied in 
//*>                  VR and/or VL; 
//*>          = 'S':  compute selected right and/or left eigenvectors, 
//*>                  as indicated by the logical array SELECT. 
//*> \endverbatim 
//*> 
//*> \param[in] SELECT 
//*> \verbatim 
//*>          SELECT is LOGICAL array, dimension (N) 
//*>          If HOWMNY = 'S', SELECT specifies the eigenvectors to be 
//*>          computed. 
//*>          The eigenvector corresponding to the j-th eigenvalue is 
//*>          computed if SELECT(j) = .TRUE.. 
//*>          Not referenced if HOWMNY = 'A' or 'B'. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix T. N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] T 
//*> \verbatim 
//*>          T is COMPLEX array, dimension (LDT,N) 
//*>          The upper triangular matrix T.  T is modified, but restored 
//*>          on exit. 
//*> \endverbatim 
//*> 
//*> \param[in] LDT 
//*> \verbatim 
//*>          LDT is INTEGER 
//*>          The leading dimension of the array T. LDT >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[in,out] VL 
//*> \verbatim 
//*>          VL is COMPLEX array, dimension (LDVL,MM) 
//*>          On entry, if SIDE = 'L' or 'B' and HOWMNY = 'B', VL must 
//*>          contain an N-by-N matrix Q (usually the unitary matrix Q of 
//*>          Schur vectors returned by CHSEQR). 
//*>          On exit, if SIDE = 'L' or 'B', VL contains: 
//*>          if HOWMNY = 'A', the matrix Y of left eigenvectors of T; 
//*>          if HOWMNY = 'B', the matrix Q*Y; 
//*>          if HOWMNY = 'S', the left eigenvectors of T specified by 
//*>                           SELECT, stored consecutively in the columns 
//*>                           of VL, in the same order as their 
//*>                           eigenvalues. 
//*>          Not referenced if SIDE = 'R'. 
//*> \endverbatim 
//*> 
//*> \param[in] LDVL 
//*> \verbatim 
//*>          LDVL is INTEGER 
//*>          The leading dimension of the array VL. 
//*>          LDVL >= 1, and if SIDE = 'L' or 'B', LDVL >= N. 
//*> \endverbatim 
//*> 
//*> \param[in,out] VR 
//*> \verbatim 
//*>          VR is COMPLEX array, dimension (LDVR,MM) 
//*>          On entry, if SIDE = 'R' or 'B' and HOWMNY = 'B', VR must 
//*>          contain an N-by-N matrix Q (usually the unitary matrix Q of 
//*>          Schur vectors returned by CHSEQR). 
//*>          On exit, if SIDE = 'R' or 'B', VR contains: 
//*>          if HOWMNY = 'A', the matrix X of right eigenvectors of T; 
//*>          if HOWMNY = 'B', the matrix Q*X; 
//*>          if HOWMNY = 'S', the right eigenvectors of T specified by 
//*>                           SELECT, stored consecutively in the columns 
//*>                           of VR, in the same order as their 
//*>                           eigenvalues. 
//*>          Not referenced if SIDE = 'L'. 
//*> \endverbatim 
//*> 
//*> \param[in] LDVR 
//*> \verbatim 
//*>          LDVR is INTEGER 
//*>          The leading dimension of the array VR. 
//*>          LDVR >= 1, and if SIDE = 'R' or 'B', LDVR >= N. 
//*> \endverbatim 
//*> 
//*> \param[in] MM 
//*> \verbatim 
//*>          MM is INTEGER 
//*>          The number of columns in the arrays VL and/or VR. MM >= M. 
//*> \endverbatim 
//*> 
//*> \param[out] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of columns in the arrays VL and/or VR actually 
//*>          used to store the eigenvectors. 
//*>          If HOWMNY = 'A' or 'B', M is set to N. 
//*>          Each selected eigenvector occupies one column. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is COMPLEX array, dimension (MAX(1,LWORK)) 
//*> \endverbatim 
//*> 
//*> \param[in] LWORK 
//*> \verbatim 
//*>          LWORK is INTEGER 
//*>          The dimension of array WORK. LWORK >= max(1,2*N). 
//*>          For optimum performance, LWORK >= N + 2*N*NB, where NB is 
//*>          the optimal blocksize. 
//*> 
//*>          If LWORK = -1, then a workspace query is assumed; the routine 
//*>          only calculates the optimal size of the WORK array, returns 
//*>          this value as the first entry of the WORK array, and no error 
//*>          message related to LWORK is issued by XERBLA. 
//*> \endverbatim 
//*> 
//*> \param[out] RWORK 
//*> \verbatim 
//*>          RWORK is REAL array, dimension (LRWORK) 
//*> \endverbatim 
//*> 
//*> \param[in] LRWORK 
//*> \verbatim 
//*>          LRWORK is INTEGER 
//*>          The dimension of array RWORK. LRWORK >= max(1,N). 
//*> 
//*>          If LRWORK = -1, then a workspace query is assumed; the routine 
//*>          only calculates the optimal size of the RWORK array, returns 
//*>          this value as the first entry of the RWORK array, and no error 
//*>          message related to LRWORK is issued by XERBLA. 
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
//*> \date November 2017 
//* 
//*  @generated from ztrevc3.f, fortran z -> c, Tue Apr 19 01:47:44 2016 
//* 
//*> \ingroup complexOTHERcomputational 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  The algorithm used in this program is basically backward (forward) 
//*>  substitution, with scaling to make the the code robust against 
//*>  possible overflow. 
//*> 
//*>  Each eigenvector is normalized so that the element of largest 
//*>  magnitude has magnitude 1; here the magnitude of a complex number 
//*>  (x,y) is taken to be |x| + |y|. 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _uyafrjjq(FString _m2cn2gjg, FString _beyjxzyr, Boolean* _2vi7x6ig, ref Int32 _dxpq0xkr, fcomplex* _2ivtt43r, ref Int32 _w8yhbr2r, fcomplex* _ppzorcqs, ref Int32 _uq25zlw0, fcomplex* _b88wiuwq, ref Int32 _oxoory3e, ref Int32 _e9y2lltf, ref Int32 _ev4xhht5, fcomplex* _apig8meb, ref Int32 _6fnxzlyp, Single* _dqanbbw3, ref Int32 _1jkrnd6f, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
fcomplex _gdjumcqt =   new fcomplex(0f,0f);
fcomplex _40vhxf9f =   new fcomplex(1f,0f);
Int32 _o80jnixx =  (int)8;
Int32 _blnc1nox =  (int)128;
Boolean _lqnhcvuj =  default;
Boolean _4oa6aiuq =  default;
Boolean _35js00fx =  default;
Boolean _lhlgm7z5 =  default;
Boolean _htf5ro8d =  default;
Boolean _yj0w80gr =  default;
Boolean _y66g69o8 =  default;
Int32 _b5p6od9s =  default;
Int32 _retbwjxi =  default;
Int32 _5kucxo3c =  default;
Int32 _znpjgsef =  default;
Int32 _umlkckdg =  default;
Int32 _1ub95eoc =  default;
Int32 _uicet2a7 =  default;
Int32 _tafa1evd =  default;
Int32 _f7059815 =  default;
Single _myhemoui =  default;
Single _343rc715 =  default;
Single _1m44vtuk =  default;
Single _rhnpgpoi =  default;
Single _bogm0gwy =  default;
Single _0h4yb5wu =  default;
Single _zfw72syv =  default;
fcomplex _n7plx4io =  default;
string fLanavab = default;
#endregion  variable declarations
_m2cn2gjg = _m2cn2gjg.Convert(1);
_beyjxzyr = _beyjxzyr.Convert(1);

	{
		//* 
		//*  -- LAPACK computational routine (version 3.8.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     November 2017 
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
		//*     .. Statement Functions .. 
		//*     .. 
		//*     .. Statement Function definitions .. 
		
		
		Func<fcomplex,Single> _4jqx89by = (_a94616nn) => { return (ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.REAL(_a94616nn ) ) + ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.AIMAG(_a94616nn ) )); };;//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Decode and test the input parameters 
		//* 
		
		_4oa6aiuq = _w8y2rzgy(_m2cn2gjg ,"B" );
		_yj0w80gr = (_w8y2rzgy(_m2cn2gjg ,"R" ) | _4oa6aiuq);
		_35js00fx = (_w8y2rzgy(_m2cn2gjg ,"L" ) | _4oa6aiuq);//* 
		
		_lqnhcvuj = _w8y2rzgy(_beyjxzyr ,"A" );
		_htf5ro8d = _w8y2rzgy(_beyjxzyr ,"B" );
		_y66g69o8 = _w8y2rzgy(_beyjxzyr ,"S" );//* 
		//*     Set M to the number of columns required to store the selected 
		//*     eigenvectors. 
		//* 
		
		if (_y66g69o8)
		{
			
			_ev4xhht5 = (int)0;
			{
				System.Int32 __81fgg2dlsvn2577 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2577 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2577;
				for (__81fgg2count2577 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2577 + __81fgg2step2577) / __81fgg2step2577)), _znpjgsef = __81fgg2dlsvn2577; __81fgg2count2577 != 0; __81fgg2count2577--, _znpjgsef += (__81fgg2step2577)) {

				{
					
					if (*(_2vi7x6ig+(_znpjgsef - 1)))
					_ev4xhht5 = (_ev4xhht5 + (int)1);
Mark10:;
					// continue
				}
								}			}
		}
		else
		{
			
			_ev4xhht5 = _dxpq0xkr;
		}
		//* 
		
		_gro5yvfo = (int)0;
		_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"CTREVC" ,_m2cn2gjg + _beyjxzyr ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) );
		_tafa1evd = (_dxpq0xkr + (((int)2 * _dxpq0xkr) * _f7059815));
		*(_apig8meb+((int)1 - 1)) = CMPLX(_tafa1evd);
		*(_dqanbbw3+((int)1 - 1)) = REAL(_dxpq0xkr);
		_lhlgm7z5 = ((_6fnxzlyp == (int)-1) | (_1jkrnd6f == (int)-1));
		if ((!(_yj0w80gr)) & (!(_35js00fx)))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (((!(_lqnhcvuj)) & (!(_htf5ro8d))) & (!(_y66g69o8)))
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if (_w8yhbr2r < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)-6;
		}
		else
		if ((_uq25zlw0 < (int)1) | (_35js00fx & (_uq25zlw0 < _dxpq0xkr)))
		{
			
			_gro5yvfo = (int)-8;
		}
		else
		if ((_oxoory3e < (int)1) | (_yj0w80gr & (_oxoory3e < _dxpq0xkr)))
		{
			
			_gro5yvfo = (int)-10;
		}
		else
		if (_e9y2lltf < _ev4xhht5)
		{
			
			_gro5yvfo = (int)-11;
		}
		else
		if ((_6fnxzlyp < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,(int)2 * _dxpq0xkr )) & (!(_lhlgm7z5)))
		{
			
			_gro5yvfo = (int)-14;
		}
		else
		if ((_1jkrnd6f < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr )) & (!(_lhlgm7z5)))
		{
			
			_gro5yvfo = (int)-16;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("CTREVC3" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
		
		if (_dxpq0xkr == (int)0)
		return;//* 
		//*     Use blocked version of back-transformation if sufficient workspace. 
		//*     Zero-out the workspace to avoid potential NaN propagation. 
		//* 
		
		if (_htf5ro8d & (_6fnxzlyp >= (_dxpq0xkr + (((int)2 * _dxpq0xkr) * _o80jnixx))))
		{
			
			_f7059815 = ((_6fnxzlyp - _dxpq0xkr) / ((int)2 * _dxpq0xkr));
			_f7059815 = ILNumerics.F2NET.Intrinsics.MIN(_f7059815 ,_blnc1nox );
			_663dvznc("F" ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1 + ((int)2 * _f7059815)) ,ref Unsafe.AsRef(_gdjumcqt) ,ref Unsafe.AsRef(_gdjumcqt) ,_apig8meb ,ref _dxpq0xkr );
		}
		else
		{
			
			_f7059815 = (int)1;
		}
		//* 
		//*     Set the constants to control overflow. 
		//* 
		
		_zfw72syv = _d5tu038y("Safe minimum" );
		_myhemoui = (_kxg5drh2 / _zfw72syv);
		_6cljvt6b(ref _zfw72syv ,ref _myhemoui );
		_0h4yb5wu = _d5tu038y("Precision" );
		_bogm0gwy = (_zfw72syv * (_dxpq0xkr / _0h4yb5wu));//* 
		//*     Store the diagonal elements of T in working array WORK. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn2578 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2578 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2578;
			for (__81fgg2count2578 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2578 + __81fgg2step2578) / __81fgg2step2578)), _b5p6od9s = __81fgg2dlsvn2578; __81fgg2count2578 != 0; __81fgg2count2578--, _b5p6od9s += (__81fgg2step2578)) {

			{
				
				*(_apig8meb+(_b5p6od9s - 1)) = *(_2ivtt43r+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r));
Mark20:;
				// continue
			}
						}		}//* 
		//*     Compute 1-norm of each column of strictly upper triangular 
		//*     part of T to control overflow in triangular solver. 
		//* 
		
		*(_dqanbbw3+((int)1 - 1)) = _d0547bi2;
		{
			System.Int32 __81fgg2dlsvn2579 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step2579 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2579;
			for (__81fgg2count2579 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2579 + __81fgg2step2579) / __81fgg2step2579)), _znpjgsef = __81fgg2dlsvn2579; __81fgg2count2579 != 0; __81fgg2count2579--, _znpjgsef += (__81fgg2step2579)) {

			{
				
				*(_dqanbbw3+(_znpjgsef - 1)) = _ojoz4216(ref Unsafe.AsRef(_znpjgsef - (int)1) ,(_2ivtt43r+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) );
Mark30:;
				// continue
			}
						}		}//* 
		
		if (_yj0w80gr)
		{
			//* 
			//*        ============================================================ 
			//*        Compute right eigenvectors. 
			//* 
			//*        IV is index of column in current block. 
			//*        Non-blocked version always uses IV=NB=1; 
			//*        blocked     version starts with IV=NB, goes down to 1. 
			//*        (Note the "0-th" column is used to store the original diagonal.) 
			
			_uicet2a7 = _f7059815;
			_5kucxo3c = _ev4xhht5;
			{
				System.Int32 __81fgg2dlsvn2580 = (System.Int32)(_dxpq0xkr);
				System.Int32 __81fgg2step2580 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count2580;
				for (__81fgg2count2580 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn2580 + __81fgg2step2580) / __81fgg2step2580)), _1ub95eoc = __81fgg2dlsvn2580; __81fgg2count2580 != 0; __81fgg2count2580--, _1ub95eoc += (__81fgg2step2580)) {

				{
					
					if (_y66g69o8)
					{
						
						if (!(*(_2vi7x6ig+(_1ub95eoc - 1))))goto Mark80;
					}
					
					_rhnpgpoi = ILNumerics.F2NET.Intrinsics.MAX(_0h4yb5wu * (_4jqx89by(*(_2ivtt43r+(_1ub95eoc - 1) + (_1ub95eoc - 1) * 1 * (_w8yhbr2r)) )) ,_bogm0gwy );//* 
					//*           -------------------------------------------------------- 
					//*           Complex right eigenvector 
					//* 
					
					*(_apig8meb+(_1ub95eoc + (_uicet2a7 * _dxpq0xkr) - 1)) = _40vhxf9f;//* 
					//*           Form right-hand side. 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn2581 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2581 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2581;
						for (__81fgg2count2581 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1ub95eoc - (int)1) - __81fgg2dlsvn2581 + __81fgg2step2581) / __81fgg2step2581)), _umlkckdg = __81fgg2dlsvn2581; __81fgg2count2581 != 0; __81fgg2count2581--, _umlkckdg += (__81fgg2step2581)) {

						{
							
							*(_apig8meb+(_umlkckdg + (_uicet2a7 * _dxpq0xkr) - 1)) = (-(*(_2ivtt43r+(_umlkckdg - 1) + (_1ub95eoc - 1) * 1 * (_w8yhbr2r))));
Mark40:;
							// continue
						}
												}					}//* 
					//*           Solve upper triangular system: 
					//*           [ T(1:KI-1,1:KI-1) - T(KI,KI) ]*X = SCALE*WORK. 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn2582 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2582 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2582;
						for (__81fgg2count2582 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1ub95eoc - (int)1) - __81fgg2dlsvn2582 + __81fgg2step2582) / __81fgg2step2582)), _umlkckdg = __81fgg2dlsvn2582; __81fgg2count2582 != 0; __81fgg2count2582--, _umlkckdg += (__81fgg2step2582)) {

						{
							
							*(_2ivtt43r+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_w8yhbr2r)) = (*(_2ivtt43r+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_w8yhbr2r)) - *(_2ivtt43r+(_1ub95eoc - 1) + (_1ub95eoc - 1) * 1 * (_w8yhbr2r)));
							if (_4jqx89by(*(_2ivtt43r+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_w8yhbr2r)) ) < _rhnpgpoi)
							*(_2ivtt43r+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_w8yhbr2r)) = CMPLX(_rhnpgpoi);
Mark50:;
							// continue
						}
												}					}//* 
					
					if (_1ub95eoc > (int)1)
					{
						
						_i8j3yqqn("Upper" ,"No transpose" ,"Non-unit" ,"Y" ,ref Unsafe.AsRef(_1ub95eoc - (int)1) ,_2ivtt43r ,ref _w8yhbr2r ,(_apig8meb+((int)1 + (_uicet2a7 * _dxpq0xkr) - 1)),ref _1m44vtuk ,_dqanbbw3 ,ref _gro5yvfo );
						*(_apig8meb+(_1ub95eoc + (_uicet2a7 * _dxpq0xkr) - 1)) = CMPLX(_1m44vtuk);
					}
					//* 
					//*           Copy the vector x or Q*x to VR and normalize. 
					//* 
					
					if (!(_htf5ro8d))
					{
						//*              ------------------------------ 
						//*              no back-transform: copy x to VR and normalize. 
						
						_33e0jk6i(ref _1ub95eoc ,(_apig8meb+((int)1 + (_uicet2a7 * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) ,(_b88wiuwq+((int)1 - 1) + (_5kucxo3c - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );//* 
						
						_retbwjxi = _r3truie3(ref _1ub95eoc ,(_b88wiuwq+((int)1 - 1) + (_5kucxo3c - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );
						_343rc715 = (_kxg5drh2 / _4jqx89by(*(_b88wiuwq+(_retbwjxi - 1) + (_5kucxo3c - 1) * 1 * (_oxoory3e)) ));
						_2ylagitj(ref _1ub95eoc ,ref _343rc715 ,(_b88wiuwq+((int)1 - 1) + (_5kucxo3c - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );//* 
						
						{
							System.Int32 __81fgg2dlsvn2583 = (System.Int32)((_1ub95eoc + (int)1));
							const System.Int32 __81fgg2step2583 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2583;
							for (__81fgg2count2583 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2583 + __81fgg2step2583) / __81fgg2step2583)), _umlkckdg = __81fgg2dlsvn2583; __81fgg2count2583 != 0; __81fgg2count2583--, _umlkckdg += (__81fgg2step2583)) {

							{
								
								*(_b88wiuwq+(_umlkckdg - 1) + (_5kucxo3c - 1) * 1 * (_oxoory3e)) = _gdjumcqt;
Mark60:;
								// continue
							}
														}						}//* 
						
					}
					else
					if (_f7059815 == (int)1)
					{
						//*              ------------------------------ 
						//*              version 1: back-transform each vector with GEMV, Q*x. 
						
						if (_1ub95eoc > (int)1)
						_f0oh3lvv("N" ,ref _dxpq0xkr ,ref Unsafe.AsRef(_1ub95eoc - (int)1) ,ref Unsafe.AsRef(_40vhxf9f) ,_b88wiuwq ,ref _oxoory3e ,(_apig8meb+((int)1 + (_uicet2a7 * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.CMPLX(_1m44vtuk )) ,(_b88wiuwq+((int)1 - 1) + (_1ub95eoc - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );//* 
						
						_retbwjxi = _r3truie3(ref _dxpq0xkr ,(_b88wiuwq+((int)1 - 1) + (_1ub95eoc - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );
						_343rc715 = (_kxg5drh2 / _4jqx89by(*(_b88wiuwq+(_retbwjxi - 1) + (_1ub95eoc - 1) * 1 * (_oxoory3e)) ));
						_2ylagitj(ref _dxpq0xkr ,ref _343rc715 ,(_b88wiuwq+((int)1 - 1) + (_1ub95eoc - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );//* 
						
					}
					else
					{
						//*              ------------------------------ 
						//*              version 2: back-transform block of vectors with GEMM 
						//*              zero out below vector 
						
						{
							System.Int32 __81fgg2dlsvn2584 = (System.Int32)((_1ub95eoc + (int)1));
							const System.Int32 __81fgg2step2584 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2584;
							for (__81fgg2count2584 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2584 + __81fgg2step2584) / __81fgg2step2584)), _umlkckdg = __81fgg2dlsvn2584; __81fgg2count2584 != 0; __81fgg2count2584--, _umlkckdg += (__81fgg2step2584)) {

							{
								
								*(_apig8meb+(_umlkckdg + (_uicet2a7 * _dxpq0xkr) - 1)) = _gdjumcqt;
							}
														}						}//* 
						//*              Columns IV:NB of work are valid vectors. 
						//*              When the number of vectors stored reaches NB, 
						//*              or if this was last vector, do the GEMM 
						
						if ((_uicet2a7 == (int)1) | (_1ub95eoc == (int)1))
						{
							
							_5p0w9905("N" ,"N" ,ref _dxpq0xkr ,ref Unsafe.AsRef((_f7059815 - _uicet2a7) + (int)1) ,ref Unsafe.AsRef((_1ub95eoc + _f7059815) - _uicet2a7) ,ref Unsafe.AsRef(_40vhxf9f) ,_b88wiuwq ,ref _oxoory3e ,(_apig8meb+((int)1 + ((_uicet2a7) * _dxpq0xkr) - 1)),ref _dxpq0xkr ,ref Unsafe.AsRef(_gdjumcqt) ,(_apig8meb+((int)1 + ((_f7059815 + _uicet2a7) * _dxpq0xkr) - 1)),ref _dxpq0xkr );//*                 normalize vectors 
							
							{
								System.Int32 __81fgg2dlsvn2585 = (System.Int32)(_uicet2a7);
								const System.Int32 __81fgg2step2585 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2585;
								for (__81fgg2count2585 = System.Math.Max(0, (System.Int32)(((System.Int32)(_f7059815) - __81fgg2dlsvn2585 + __81fgg2step2585) / __81fgg2step2585)), _umlkckdg = __81fgg2dlsvn2585; __81fgg2count2585 != 0; __81fgg2count2585--, _umlkckdg += (__81fgg2step2585)) {

								{
									
									_retbwjxi = _r3truie3(ref _dxpq0xkr ,(_apig8meb+((int)1 + ((_f7059815 + _umlkckdg) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
									_343rc715 = (_kxg5drh2 / _4jqx89by(*(_apig8meb+(_retbwjxi + ((_f7059815 + _umlkckdg) * _dxpq0xkr) - 1)) ));
									_2ylagitj(ref _dxpq0xkr ,ref _343rc715 ,(_apig8meb+((int)1 + ((_f7059815 + _umlkckdg) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
								}
																}							}
							_szaic8qw("F" ,ref _dxpq0xkr ,ref Unsafe.AsRef((_f7059815 - _uicet2a7) + (int)1) ,(_apig8meb+((int)1 + ((_f7059815 + _uicet2a7) * _dxpq0xkr) - 1)),ref _dxpq0xkr ,(_b88wiuwq+((int)1 - 1) + (_1ub95eoc - 1) * 1 * (_oxoory3e)),ref _oxoory3e );
							_uicet2a7 = _f7059815;
						}
						else
						{
							
							_uicet2a7 = (_uicet2a7 - (int)1);
						}
						
					}
					//* 
					//*           Restore the original diagonal elements of T. 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn2586 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2586 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2586;
						for (__81fgg2count2586 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1ub95eoc - (int)1) - __81fgg2dlsvn2586 + __81fgg2step2586) / __81fgg2step2586)), _umlkckdg = __81fgg2dlsvn2586; __81fgg2count2586 != 0; __81fgg2count2586--, _umlkckdg += (__81fgg2step2586)) {

						{
							
							*(_2ivtt43r+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_w8yhbr2r)) = *(_apig8meb+(_umlkckdg - 1));
Mark70:;
							// continue
						}
												}					}//* 
					
					_5kucxo3c = (_5kucxo3c - (int)1);
Mark80:;
					// continue
				}
								}			}
		}
		//* 
		
		if (_35js00fx)
		{
			//* 
			//*        ============================================================ 
			//*        Compute left eigenvectors. 
			//* 
			//*        IV is index of column in current block. 
			//*        Non-blocked version always uses IV=1; 
			//*        blocked     version starts with IV=1, goes up to NB. 
			//*        (Note the "0-th" column is used to store the original diagonal.) 
			
			_uicet2a7 = (int)1;
			_5kucxo3c = (int)1;
			{
				System.Int32 __81fgg2dlsvn2587 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2587 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2587;
				for (__81fgg2count2587 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2587 + __81fgg2step2587) / __81fgg2step2587)), _1ub95eoc = __81fgg2dlsvn2587; __81fgg2count2587 != 0; __81fgg2count2587--, _1ub95eoc += (__81fgg2step2587)) {

				{
					//* 
					
					if (_y66g69o8)
					{
						
						if (!(*(_2vi7x6ig+(_1ub95eoc - 1))))goto Mark130;
					}
					
					_rhnpgpoi = ILNumerics.F2NET.Intrinsics.MAX(_0h4yb5wu * (_4jqx89by(*(_2ivtt43r+(_1ub95eoc - 1) + (_1ub95eoc - 1) * 1 * (_w8yhbr2r)) )) ,_bogm0gwy );//* 
					//*           -------------------------------------------------------- 
					//*           Complex left eigenvector 
					//* 
					
					*(_apig8meb+(_1ub95eoc + (_uicet2a7 * _dxpq0xkr) - 1)) = _40vhxf9f;//* 
					//*           Form right-hand side. 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn2588 = (System.Int32)((_1ub95eoc + (int)1));
						const System.Int32 __81fgg2step2588 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2588;
						for (__81fgg2count2588 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2588 + __81fgg2step2588) / __81fgg2step2588)), _umlkckdg = __81fgg2dlsvn2588; __81fgg2count2588 != 0; __81fgg2count2588--, _umlkckdg += (__81fgg2step2588)) {

						{
							
							*(_apig8meb+(_umlkckdg + (_uicet2a7 * _dxpq0xkr) - 1)) = (-(ILNumerics.F2NET.Intrinsics.CONJG(*(_2ivtt43r+(_1ub95eoc - 1) + (_umlkckdg - 1) * 1 * (_w8yhbr2r)) )));
Mark90:;
							// continue
						}
												}					}//* 
					//*           Solve conjugate-transposed triangular system: 
					//*           [ T(KI+1:N,KI+1:N) - T(KI,KI) ]**H * X = SCALE*WORK. 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn2589 = (System.Int32)((_1ub95eoc + (int)1));
						const System.Int32 __81fgg2step2589 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2589;
						for (__81fgg2count2589 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2589 + __81fgg2step2589) / __81fgg2step2589)), _umlkckdg = __81fgg2dlsvn2589; __81fgg2count2589 != 0; __81fgg2count2589--, _umlkckdg += (__81fgg2step2589)) {

						{
							
							*(_2ivtt43r+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_w8yhbr2r)) = (*(_2ivtt43r+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_w8yhbr2r)) - *(_2ivtt43r+(_1ub95eoc - 1) + (_1ub95eoc - 1) * 1 * (_w8yhbr2r)));
							if (_4jqx89by(*(_2ivtt43r+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_w8yhbr2r)) ) < _rhnpgpoi)
							*(_2ivtt43r+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_w8yhbr2r)) = CMPLX(_rhnpgpoi);
Mark100:;
							// continue
						}
												}					}//* 
					
					if (_1ub95eoc < _dxpq0xkr)
					{
						
						_i8j3yqqn("Upper" ,"Conjugate transpose" ,"Non-unit" ,"Y" ,ref Unsafe.AsRef(_dxpq0xkr - _1ub95eoc) ,(_2ivtt43r+(_1ub95eoc + (int)1 - 1) + (_1ub95eoc + (int)1 - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,(_apig8meb+((_1ub95eoc + (int)1) + (_uicet2a7 * _dxpq0xkr) - 1)),ref _1m44vtuk ,_dqanbbw3 ,ref _gro5yvfo );
						*(_apig8meb+(_1ub95eoc + (_uicet2a7 * _dxpq0xkr) - 1)) = CMPLX(_1m44vtuk);
					}
					//* 
					//*           Copy the vector x or Q*x to VL and normalize. 
					//* 
					
					if (!(_htf5ro8d))
					{
						//*              ------------------------------ 
						//*              no back-transform: copy x to VL and normalize. 
						
						_33e0jk6i(ref Unsafe.AsRef((_dxpq0xkr - _1ub95eoc) + (int)1) ,(_apig8meb+(_1ub95eoc + (_uicet2a7 * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) ,(_ppzorcqs+(_1ub95eoc - 1) + (_5kucxo3c - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );//* 
						
						_retbwjxi = ((_r3truie3(ref Unsafe.AsRef((_dxpq0xkr - _1ub95eoc) + (int)1) ,(_ppzorcqs+(_1ub95eoc - 1) + (_5kucxo3c - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) ) + _1ub95eoc) - (int)1);
						_343rc715 = (_kxg5drh2 / _4jqx89by(*(_ppzorcqs+(_retbwjxi - 1) + (_5kucxo3c - 1) * 1 * (_uq25zlw0)) ));
						_2ylagitj(ref Unsafe.AsRef((_dxpq0xkr - _1ub95eoc) + (int)1) ,ref _343rc715 ,(_ppzorcqs+(_1ub95eoc - 1) + (_5kucxo3c - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );//* 
						
						{
							System.Int32 __81fgg2dlsvn2590 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step2590 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2590;
							for (__81fgg2count2590 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1ub95eoc - (int)1) - __81fgg2dlsvn2590 + __81fgg2step2590) / __81fgg2step2590)), _umlkckdg = __81fgg2dlsvn2590; __81fgg2count2590 != 0; __81fgg2count2590--, _umlkckdg += (__81fgg2step2590)) {

							{
								
								*(_ppzorcqs+(_umlkckdg - 1) + (_5kucxo3c - 1) * 1 * (_uq25zlw0)) = _gdjumcqt;
Mark110:;
								// continue
							}
														}						}//* 
						
					}
					else
					if (_f7059815 == (int)1)
					{
						//*              ------------------------------ 
						//*              version 1: back-transform each vector with GEMV, Q*x. 
						
						if (_1ub95eoc < _dxpq0xkr)
						_f0oh3lvv("N" ,ref _dxpq0xkr ,ref Unsafe.AsRef(_dxpq0xkr - _1ub95eoc) ,ref Unsafe.AsRef(_40vhxf9f) ,(_ppzorcqs+((int)1 - 1) + (_1ub95eoc + (int)1 - 1) * 1 * (_uq25zlw0)),ref _uq25zlw0 ,(_apig8meb+((_1ub95eoc + (int)1) + (_uicet2a7 * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.CMPLX(_1m44vtuk )) ,(_ppzorcqs+((int)1 - 1) + (_1ub95eoc - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );//* 
						
						_retbwjxi = _r3truie3(ref _dxpq0xkr ,(_ppzorcqs+((int)1 - 1) + (_1ub95eoc - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );
						_343rc715 = (_kxg5drh2 / _4jqx89by(*(_ppzorcqs+(_retbwjxi - 1) + (_1ub95eoc - 1) * 1 * (_uq25zlw0)) ));
						_2ylagitj(ref _dxpq0xkr ,ref _343rc715 ,(_ppzorcqs+((int)1 - 1) + (_1ub95eoc - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );//* 
						
					}
					else
					{
						//*              ------------------------------ 
						//*              version 2: back-transform block of vectors with GEMM 
						//*              zero out above vector 
						//*              could go from KI-NV+1 to KI-1 
						
						{
							System.Int32 __81fgg2dlsvn2591 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step2591 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2591;
							for (__81fgg2count2591 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1ub95eoc - (int)1) - __81fgg2dlsvn2591 + __81fgg2step2591) / __81fgg2step2591)), _umlkckdg = __81fgg2dlsvn2591; __81fgg2count2591 != 0; __81fgg2count2591--, _umlkckdg += (__81fgg2step2591)) {

							{
								
								*(_apig8meb+(_umlkckdg + (_uicet2a7 * _dxpq0xkr) - 1)) = _gdjumcqt;
							}
														}						}//* 
						//*              Columns 1:IV of work are valid vectors. 
						//*              When the number of vectors stored reaches NB, 
						//*              or if this was last vector, do the GEMM 
						
						if ((_uicet2a7 == _f7059815) | (_1ub95eoc == _dxpq0xkr))
						{
							
							_5p0w9905("N" ,"N" ,ref _dxpq0xkr ,ref _uicet2a7 ,ref Unsafe.AsRef((_dxpq0xkr - _1ub95eoc) + _uicet2a7) ,ref Unsafe.AsRef(_40vhxf9f) ,(_ppzorcqs+((int)1 - 1) + ((_1ub95eoc - _uicet2a7) + (int)1 - 1) * 1 * (_uq25zlw0)),ref _uq25zlw0 ,(_apig8meb+(((_1ub95eoc - _uicet2a7) + (int)1) + (((int)1) * _dxpq0xkr) - 1)),ref _dxpq0xkr ,ref Unsafe.AsRef(_gdjumcqt) ,(_apig8meb+((int)1 + ((_f7059815 + (int)1) * _dxpq0xkr) - 1)),ref _dxpq0xkr );//*                 normalize vectors 
							
							{
								System.Int32 __81fgg2dlsvn2592 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step2592 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2592;
								for (__81fgg2count2592 = System.Math.Max(0, (System.Int32)(((System.Int32)(_uicet2a7) - __81fgg2dlsvn2592 + __81fgg2step2592) / __81fgg2step2592)), _umlkckdg = __81fgg2dlsvn2592; __81fgg2count2592 != 0; __81fgg2count2592--, _umlkckdg += (__81fgg2step2592)) {

								{
									
									_retbwjxi = _r3truie3(ref _dxpq0xkr ,(_apig8meb+((int)1 + ((_f7059815 + _umlkckdg) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
									_343rc715 = (_kxg5drh2 / _4jqx89by(*(_apig8meb+(_retbwjxi + ((_f7059815 + _umlkckdg) * _dxpq0xkr) - 1)) ));
									_2ylagitj(ref _dxpq0xkr ,ref _343rc715 ,(_apig8meb+((int)1 + ((_f7059815 + _umlkckdg) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
								}
																}							}
							_szaic8qw("F" ,ref _dxpq0xkr ,ref _uicet2a7 ,(_apig8meb+((int)1 + ((_f7059815 + (int)1) * _dxpq0xkr) - 1)),ref _dxpq0xkr ,(_ppzorcqs+((int)1 - 1) + ((_1ub95eoc - _uicet2a7) + (int)1 - 1) * 1 * (_uq25zlw0)),ref _uq25zlw0 );
							_uicet2a7 = (int)1;
						}
						else
						{
							
							_uicet2a7 = (_uicet2a7 + (int)1);
						}
						
					}
					//* 
					//*           Restore the original diagonal elements of T. 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn2593 = (System.Int32)((_1ub95eoc + (int)1));
						const System.Int32 __81fgg2step2593 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2593;
						for (__81fgg2count2593 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2593 + __81fgg2step2593) / __81fgg2step2593)), _umlkckdg = __81fgg2dlsvn2593; __81fgg2count2593 != 0; __81fgg2count2593--, _umlkckdg += (__81fgg2step2593)) {

						{
							
							*(_2ivtt43r+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * (_w8yhbr2r)) = *(_apig8meb+(_umlkckdg - 1));
Mark120:;
							// continue
						}
												}					}//* 
					
					_5kucxo3c = (_5kucxo3c + (int)1);
Mark130:;
					// continue
				}
								}			}
		}
		//* 
		
		return;//* 
		//*     End of CTREVC3 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
