
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
//*> \brief \b DSYTRD 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DSYTRD + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dsytrd.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dsytrd.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dsytrd.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DSYTRD( UPLO, N, A, LDA, D, E, TAU, WORK, LWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          UPLO 
//*       INTEGER            INFO, LDA, LWORK, N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   A( LDA, * ), D( * ), E( * ), TAU( * ), 
//*      $                   WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DSYTRD reduces a real symmetric matrix A to real symmetric 
//*> tridiagonal form T by an orthogonal similarity transformation: 
//*> Q**T * A * Q = T. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>          = 'U':  Upper triangle of A is stored; 
//*>          = 'L':  Lower triangle of A is stored. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix A.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is DOUBLE PRECISION array, dimension (LDA,N) 
//*>          On entry, the symmetric matrix A.  If UPLO = 'U', the leading 
//*>          N-by-N upper triangular part of A contains the upper 
//*>          triangular part of the matrix A, and the strictly lower 
//*>          triangular part of A is not referenced.  If UPLO = 'L', the 
//*>          leading N-by-N lower triangular part of A contains the lower 
//*>          triangular part of the matrix A, and the strictly upper 
//*>          triangular part of A is not referenced. 
//*>          On exit, if UPLO = 'U', the diagonal and first superdiagonal 
//*>          of A are overwritten by the corresponding elements of the 
//*>          tridiagonal matrix T, and the elements above the first 
//*>          superdiagonal, with the array TAU, represent the orthogonal 
//*>          matrix Q as a product of elementary reflectors; if UPLO 
//*>          = 'L', the diagonal and first subdiagonal of A are over- 
//*>          written by the corresponding elements of the tridiagonal 
//*>          matrix T, and the elements below the first subdiagonal, with 
//*>          the array TAU, represent the orthogonal matrix Q as a product 
//*>          of elementary reflectors. See Further Details. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[out] D 
//*> \verbatim 
//*>          D is DOUBLE PRECISION array, dimension (N) 
//*>          The diagonal elements of the tridiagonal matrix T: 
//*>          D(i) = A(i,i). 
//*> \endverbatim 
//*> 
//*> \param[out] E 
//*> \verbatim 
//*>          E is DOUBLE PRECISION array, dimension (N-1) 
//*>          The off-diagonal elements of the tridiagonal matrix T: 
//*>          E(i) = A(i,i+1) if UPLO = 'U', E(i) = A(i+1,i) if UPLO = 'L'. 
//*> \endverbatim 
//*> 
//*> \param[out] TAU 
//*> \verbatim 
//*>          TAU is DOUBLE PRECISION array, dimension (N-1) 
//*>          The scalar factors of the elementary reflectors (see Further 
//*>          Details). 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is DOUBLE PRECISION array, dimension (MAX(1,LWORK)) 
//*>          On exit, if INFO = 0, WORK(1) returns the optimal LWORK. 
//*> \endverbatim 
//*> 
//*> \param[in] LWORK 
//*> \verbatim 
//*>          LWORK is INTEGER 
//*>          The dimension of the array WORK.  LWORK >= 1. 
//*>          For optimum performance LWORK >= N*NB, where NB is the 
//*>          optimal blocksize. 
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
//*> \ingroup doubleSYcomputational 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  If UPLO = 'U', the matrix Q is represented as a product of elementary 
//*>  reflectors 
//*> 
//*>     Q = H(n-1) . . . H(2) H(1). 
//*> 
//*>  Each H(i) has the form 
//*> 
//*>     H(i) = I - tau * v * v**T 
//*> 
//*>  where tau is a real scalar, and v is a real vector with 
//*>  v(i+1:n) = 0 and v(i) = 1; v(1:i-1) is stored on exit in 
//*>  A(1:i-1,i+1), and tau in TAU(i). 
//*> 
//*>  If UPLO = 'L', the matrix Q is represented as a product of elementary 
//*>  reflectors 
//*> 
//*>     Q = H(1) H(2) . . . H(n-1). 
//*> 
//*>  Each H(i) has the form 
//*> 
//*>     H(i) = I - tau * v * v**T 
//*> 
//*>  where tau is a real scalar, and v is a real vector with 
//*>  v(1:i) = 0 and v(i+1) = 1; v(i+2:n) is stored on exit in A(i+2:n,i), 
//*>  and tau in TAU(i). 
//*> 
//*>  The contents of A on exit are illustrated by the following examples 
//*>  with n = 5: 
//*> 
//*>  if UPLO = 'U':                       if UPLO = 'L': 
//*> 
//*>    (  d   e   v2  v3  v4 )              (  d                  ) 
//*>    (      d   e   v3  v4 )              (  e   d              ) 
//*>    (          d   e   v4 )              (  v1  e   d          ) 
//*>    (              d   e  )              (  v1  v2  e   d      ) 
//*>    (                  d  )              (  v1  v2  v3  e   d  ) 
//*> 
//*>  where d and e denote diagonal and off-diagonal elements of T, and vi 
//*>  denotes an element of the vector defining H(i). 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _tyexjdu4(FString _9wyre9zc, ref Int32 _dxpq0xkr, Double* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _plfm7z8g, Double* _864fslqq, Double* _0446f4de, Double* _apig8meb, ref Int32 _6fnxzlyp, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _kxg5drh2 =  1d;
Boolean _lhlgm7z5 =  default;
Boolean _l08igmvf =  default;
Int32 _b5p6od9s =  default;
Int32 _itfnbz60 =  default;
Int32 _7tvbphxm =  default;
Int32 _znpjgsef =  default;
Int32 _dulqqknh =  default;
Int32 _iykhdriq =  default;
Int32 _e4ueamrn =  default;
Int32 _f7059815 =  default;
Int32 _o80jnixx =  default;
Int32 _rtlyoyz3 =  default;
string fLanavab = default;
#endregion  variable declarations
_9wyre9zc = _9wyre9zc.Convert(1);

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
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input parameters 
		//* 
		
		_gro5yvfo = (int)0;
		_l08igmvf = _w8y2rzgy(_9wyre9zc ,"U" );
		_lhlgm7z5 = (_6fnxzlyp == (int)-1);
		if ((!(_l08igmvf)) & (!(_w8y2rzgy(_9wyre9zc ,"L" ))))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if ((_6fnxzlyp < (int)1) & (!(_lhlgm7z5)))
		{
			
			_gro5yvfo = (int)-9;
		}
		//* 
		
		if (_gro5yvfo == (int)0)
		{
			//* 
			//*        Determine the block size. 
			//* 
			
			_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"DSYTRD" ,_9wyre9zc ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) );
			_e4ueamrn = (_dxpq0xkr * _f7059815);
			*(_apig8meb+((int)1 - 1)) = DBLE(_e4ueamrn);
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("DSYTRD" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
			
			*(_apig8meb+((int)1 - 1)) = DBLE((int)1);
			return;
		}
		//* 
		
		_rtlyoyz3 = _dxpq0xkr;
		_7tvbphxm = (int)1;
		if ((_f7059815 > (int)1) & (_f7059815 < _dxpq0xkr))
		{
			//* 
			//*        Determine when to cross over from blocked to unblocked code 
			//*        (last block is always handled by unblocked code). 
			//* 
			
			_rtlyoyz3 = ILNumerics.F2NET.Intrinsics.MAX(_f7059815 ,_4mvd6e4d(ref Unsafe.AsRef((int)3) ,"DSYTRD" ,_9wyre9zc ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ) );
			if (_rtlyoyz3 < _dxpq0xkr)
			{
				//* 
				//*           Determine if workspace is large enough for blocked code. 
				//* 
				
				_iykhdriq = _dxpq0xkr;
				_7tvbphxm = (_iykhdriq * _f7059815);
				if (_6fnxzlyp < _7tvbphxm)
				{
					//* 
					//*              Not enough workspace to use optimal NB:  determine the 
					//*              minimum value of NB, and reduce NB or force use of 
					//*              unblocked code by setting NX = N. 
					//* 
					
					_f7059815 = ILNumerics.F2NET.Intrinsics.MAX(_6fnxzlyp / _iykhdriq ,(int)1 );
					_o80jnixx = _4mvd6e4d(ref Unsafe.AsRef((int)2) ,"DSYTRD" ,_9wyre9zc ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) );
					if (_f7059815 < _o80jnixx)
					_rtlyoyz3 = _dxpq0xkr;
				}
				
			}
			else
			{
				
				_rtlyoyz3 = _dxpq0xkr;
			}
			
		}
		else
		{
			
			_f7059815 = (int)1;
		}
		//* 
		
		if (_l08igmvf)
		{
			//* 
			//*        Reduce the upper triangle of A. 
			//*        Columns 1:kk are handled by the unblocked method. 
			//* 
			
			_dulqqknh = (_dxpq0xkr - (((((_dxpq0xkr - _rtlyoyz3) + _f7059815) - (int)1) / _f7059815) * _f7059815));
			{
				System.Int32 __81fgg2dlsvn3099 = (System.Int32)(((_dxpq0xkr - _f7059815) + (int)1));
				System.Int32 __81fgg2step3099 = (System.Int32)(-(_f7059815));
				System.Int32 __81fgg2count3099;
				for (__81fgg2count3099 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dulqqknh + (int)1) - __81fgg2dlsvn3099 + __81fgg2step3099) / __81fgg2step3099)), _b5p6od9s = __81fgg2dlsvn3099; __81fgg2count3099 != 0; __81fgg2count3099--, _b5p6od9s += (__81fgg2step3099)) {

				{
					//* 
					//*           Reduce columns i:i+nb-1 to tridiagonal form and form the 
					//*           matrix W which is needed to update the unreduced part of 
					//*           the matrix 
					//* 
					
					_lfgv9yd2(_9wyre9zc ,ref Unsafe.AsRef((_b5p6od9s + _f7059815) - (int)1) ,ref _f7059815 ,_vxfgpup9 ,ref _ocv8fk5c ,_864fslqq ,_0446f4de ,_apig8meb ,ref _iykhdriq );//* 
					//*           Update the unreduced submatrix A(1:i-1,1:i-1), using an 
					//*           update of the form:  A := A - V*W**T - W*V**T 
					//* 
					
					_wpcl5drw(_9wyre9zc ,"No transpose" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref _f7059815 ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_apig8meb ,ref _iykhdriq ,ref Unsafe.AsRef(_kxg5drh2) ,_vxfgpup9 ,ref _ocv8fk5c );//* 
					//*           Copy superdiagonal elements back into A, and diagonal 
					//*           elements into D 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn3100 = (System.Int32)(_b5p6od9s);
						const System.Int32 __81fgg2step3100 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3100;
						for (__81fgg2count3100 = System.Math.Max(0, (System.Int32)(((System.Int32)((_b5p6od9s + _f7059815) - (int)1) - __81fgg2dlsvn3100 + __81fgg2step3100) / __81fgg2step3100)), _znpjgsef = __81fgg2dlsvn3100; __81fgg2count3100 != 0; __81fgg2count3100--, _znpjgsef += (__81fgg2step3100)) {

						{
							
							*(_vxfgpup9+(_znpjgsef - (int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = *(_864fslqq+(_znpjgsef - (int)1 - 1));
							*(_plfm7z8g+(_znpjgsef - 1)) = *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c));
Mark10:;
							// continue
						}
												}					}
Mark20:;
					// continue
				}
								}			}//* 
			//*        Use unblocked code to reduce the last or only block 
			//* 
			
			_jh7znm36(_9wyre9zc ,ref _dulqqknh ,_vxfgpup9 ,ref _ocv8fk5c ,_plfm7z8g ,_864fslqq ,_0446f4de ,ref _itfnbz60 );
		}
		else
		{
			//* 
			//*        Reduce the lower triangle of A 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn3101 = (System.Int32)((int)1);
				System.Int32 __81fgg2step3101 = (System.Int32)(_f7059815);
				System.Int32 __81fgg2count3101;
				for (__81fgg2count3101 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - _rtlyoyz3) - __81fgg2dlsvn3101 + __81fgg2step3101) / __81fgg2step3101)), _b5p6od9s = __81fgg2dlsvn3101; __81fgg2count3101 != 0; __81fgg2count3101--, _b5p6od9s += (__81fgg2step3101)) {

				{
					//* 
					//*           Reduce columns i:i+nb-1 to tridiagonal form and form the 
					//*           matrix W which is needed to update the unreduced part of 
					//*           the matrix 
					//* 
					
					_lfgv9yd2(_9wyre9zc ,ref Unsafe.AsRef((_dxpq0xkr - _b5p6od9s) + (int)1) ,ref _f7059815 ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_864fslqq+(_b5p6od9s - 1)),(_0446f4de+(_b5p6od9s - 1)),_apig8meb ,ref _iykhdriq );//* 
					//*           Update the unreduced submatrix A(i+ib:n,i+ib:n), using 
					//*           an update of the form:  A := A - V*W**T - W*V**T 
					//* 
					
					_wpcl5drw(_9wyre9zc ,"No transpose" ,ref Unsafe.AsRef(((_dxpq0xkr - _b5p6od9s) - _f7059815) + (int)1) ,ref _f7059815 ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+(_b5p6od9s + _f7059815 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_apig8meb+(_f7059815 + (int)1 - 1)),ref _iykhdriq ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_b5p6od9s + _f7059815 - 1) + (_b5p6od9s + _f7059815 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );//* 
					//*           Copy subdiagonal elements back into A, and diagonal 
					//*           elements into D 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn3102 = (System.Int32)(_b5p6od9s);
						const System.Int32 __81fgg2step3102 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3102;
						for (__81fgg2count3102 = System.Math.Max(0, (System.Int32)(((System.Int32)((_b5p6od9s + _f7059815) - (int)1) - __81fgg2dlsvn3102 + __81fgg2step3102) / __81fgg2step3102)), _znpjgsef = __81fgg2dlsvn3102; __81fgg2count3102 != 0; __81fgg2count3102--, _znpjgsef += (__81fgg2step3102)) {

						{
							
							*(_vxfgpup9+(_znpjgsef + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = *(_864fslqq+(_znpjgsef - 1));
							*(_plfm7z8g+(_znpjgsef - 1)) = *(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c));
Mark30:;
							// continue
						}
												}					}
Mark40:;
					// continue
				}
								}			}//* 
			//*        Use unblocked code to reduce the last or only block 
			//* 
			
			_jh7znm36(_9wyre9zc ,ref Unsafe.AsRef((_dxpq0xkr - _b5p6od9s) + (int)1) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_plfm7z8g+(_b5p6od9s - 1)),(_864fslqq+(_b5p6od9s - 1)),(_0446f4de+(_b5p6od9s - 1)),ref _itfnbz60 );
		}
		//* 
		
		*(_apig8meb+((int)1 - 1)) = DBLE(_e4ueamrn);
		return;//* 
		//*     End of DSYTRD 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
