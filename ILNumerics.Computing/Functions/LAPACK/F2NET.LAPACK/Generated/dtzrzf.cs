
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
//*> \brief \b DTZRZF 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DTZRZF + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dtzrzf.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dtzrzf.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dtzrzf.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DTZRZF( M, N, A, LDA, TAU, WORK, LWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, LDA, LWORK, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   A( LDA, * ), TAU( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DTZRZF reduces the M-by-N ( M<=N ) real upper trapezoidal matrix A 
//*> to upper triangular form by means of orthogonal transformations. 
//*> 
//*> The upper trapezoidal matrix A is factored as 
//*> 
//*>    A = ( R  0 ) * Z, 
//*> 
//*> where Z is an N-by-N orthogonal matrix and R is an M-by-M upper 
//*> triangular matrix. 
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
//*>          The number of columns of the matrix A.  N >= M. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is DOUBLE PRECISION array, dimension (LDA,N) 
//*>          On entry, the leading M-by-N upper trapezoidal part of the 
//*>          array A must contain the matrix to be factorized. 
//*>          On exit, the leading M-by-M upper triangular part of A 
//*>          contains the upper triangular matrix R, and elements M+1 to 
//*>          N of the first M rows of A, with the array TAU, represent the 
//*>          orthogonal matrix Z as a product of M elementary reflectors. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,M). 
//*> \endverbatim 
//*> 
//*> \param[out] TAU 
//*> \verbatim 
//*>          TAU is DOUBLE PRECISION array, dimension (M) 
//*>          The scalar factors of the elementary reflectors. 
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
//*>          The dimension of the array WORK.  LWORK >= max(1,M). 
//*>          For optimum performance LWORK >= M*NB, where NB is 
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
//*> \date April 2012 
//* 
//*> \ingroup doubleOTHERcomputational 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>    A. Petitet, Computer Science Dept., Univ. of Tenn., Knoxville, USA 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  The N-by-N matrix Z can be computed by 
//*> 
//*>     Z =  Z(1)*Z(2)* ... *Z(M) 
//*> 
//*>  where each N-by-N Z(k) is given by 
//*> 
//*>     Z(k) = I - tau(k)*v(k)*v(k)**T 
//*> 
//*>  with v(k) is the kth row vector of the M-by-N matrix 
//*> 
//*>     V = ( I   A(:,M+1:N) ) 
//*> 
//*>  I is the M-by-M identity matrix, A(:,M+1:N) 
//*>  is the output stored in A on exit from DTZRZF, 
//*>  and tau(k) is the kth element of the array TAU. 
//*> 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _qtix7f5m(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, Double* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _0446f4de, Double* _apig8meb, ref Int32 _6fnxzlyp, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Boolean _lhlgm7z5 =  default;
Int32 _b5p6od9s =  default;
Int32 _vyr1z1si =  default;
Int32 _7tvbphxm =  default;
Int32 _1ub95eoc =  default;
Int32 _dulqqknh =  default;
Int32 _iykhdriq =  default;
Int32 _yq36eu37 =  default;
Int32 _e4ueamrn =  default;
Int32 _kwmm2c1l =  default;
Int32 _4s3y5e1x =  default;
Int32 _f7059815 =  default;
Int32 _o80jnixx =  default;
Int32 _rtlyoyz3 =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK computational routine (version 3.7.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     April 2012 
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
		//*     Test the input arguments 
		//* 
		
		_gro5yvfo = (int)0;
		_lhlgm7z5 = (_6fnxzlyp == (int)-1);
		if (_ev4xhht5 < (int)0)
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_dxpq0xkr < _ev4xhht5)
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ))
		{
			
			_gro5yvfo = (int)-4;
		}
		//* 
		
		if (_gro5yvfo == (int)0)
		{
			
			if ((_ev4xhht5 == (int)0) | (_ev4xhht5 == _dxpq0xkr))
			{
				
				_e4ueamrn = (int)1;
				_yq36eu37 = (int)1;
			}
			else
			{
				//* 
				//*           Determine the block size. 
				//* 
				
				_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"DGERQF" ," " ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) );
				_e4ueamrn = (_ev4xhht5 * _f7059815);
				_yq36eu37 = ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 );
			}
			
			*(_apig8meb+((int)1 - 1)) = DBLE(_e4ueamrn);//* 
			
			if ((_6fnxzlyp < _yq36eu37) & (!(_lhlgm7z5)))
			{
				
				_gro5yvfo = (int)-7;
			}
			
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("DTZRZF" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
		
		if (_ev4xhht5 == (int)0)
		{
			
			return;
		}
		else
		if (_ev4xhht5 == _dxpq0xkr)
		{
			
			{
				System.Int32 __81fgg2dlsvn2090 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2090 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2090;
				for (__81fgg2count2090 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2090 + __81fgg2step2090) / __81fgg2step2090)), _b5p6od9s = __81fgg2dlsvn2090; __81fgg2count2090 != 0; __81fgg2count2090--, _b5p6od9s += (__81fgg2step2090)) {

				{
					
					*(_0446f4de+(_b5p6od9s - 1)) = _d0547bi2;
Mark10:;
					// continue
				}
								}			}
			return;
		}
		//* 
		
		_o80jnixx = (int)2;
		_rtlyoyz3 = (int)1;
		_7tvbphxm = _ev4xhht5;
		if ((_f7059815 > (int)1) & (_f7059815 < _ev4xhht5))
		{
			//* 
			//*        Determine when to cross over from blocked to unblocked code. 
			//* 
			
			_rtlyoyz3 = ILNumerics.F2NET.Intrinsics.MAX((int)0 ,_4mvd6e4d(ref Unsafe.AsRef((int)3) ,"DGERQF" ," " ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ) );
			if (_rtlyoyz3 < _ev4xhht5)
			{
				//* 
				//*           Determine if workspace is large enough for blocked code. 
				//* 
				
				_iykhdriq = _ev4xhht5;
				_7tvbphxm = (_iykhdriq * _f7059815);
				if (_6fnxzlyp < _7tvbphxm)
				{
					//* 
					//*              Not enough workspace to use optimal NB:  reduce NB and 
					//*              determine the minimum value of NB. 
					//* 
					
					_f7059815 = (_6fnxzlyp / _iykhdriq);
					_o80jnixx = ILNumerics.F2NET.Intrinsics.MAX((int)2 ,_4mvd6e4d(ref Unsafe.AsRef((int)2) ,"DGERQF" ," " ,ref _ev4xhht5 ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ) );
				}
				
			}
			
		}
		//* 
		
		if (((_f7059815 >= _o80jnixx) & (_f7059815 < _ev4xhht5)) & (_rtlyoyz3 < _ev4xhht5))
		{
			//* 
			//*        Use blocked code initially. 
			//*        The last kk rows are handled by the block method. 
			//* 
			
			_kwmm2c1l = ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 + (int)1 ,_dxpq0xkr );
			_1ub95eoc = ((((_ev4xhht5 - _rtlyoyz3) - (int)1) / _f7059815) * _f7059815);
			_dulqqknh = ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_1ub95eoc + _f7059815 );//* 
			
			{
				System.Int32 __81fgg2dlsvn2091 = (System.Int32)((((_ev4xhht5 - _dulqqknh) + _1ub95eoc) + (int)1));
				System.Int32 __81fgg2step2091 = (System.Int32)(-(_f7059815));
				System.Int32 __81fgg2count2091;
				for (__81fgg2count2091 = System.Math.Max(0, (System.Int32)(((System.Int32)((_ev4xhht5 - _dulqqknh) + (int)1) - __81fgg2dlsvn2091 + __81fgg2step2091) / __81fgg2step2091)), _b5p6od9s = __81fgg2dlsvn2091; __81fgg2count2091 != 0; __81fgg2count2091--, _b5p6od9s += (__81fgg2step2091)) {

				{
					
					_vyr1z1si = ILNumerics.F2NET.Intrinsics.MIN((_ev4xhht5 - _b5p6od9s) + (int)1 ,_f7059815 );//* 
					//*           Compute the TZ factorization of the current block 
					//*           A(i:i+ib-1,i:n) 
					//* 
					
					_wfabsqdk(ref _vyr1z1si ,ref Unsafe.AsRef((_dxpq0xkr - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - _ev4xhht5) ,(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_0446f4de+(_b5p6od9s - 1)),_apig8meb );
					if (_b5p6od9s > (int)1)
					{
						//* 
						//*              Form the triangular factor of the block reflector 
						//*              H = H(i+ib-1) . . . H(i+1) H(i) 
						//* 
						
						_vpa9e32t("Backward" ,"Rowwise" ,ref Unsafe.AsRef(_dxpq0xkr - _ev4xhht5) ,ref _vyr1z1si ,(_vxfgpup9+(_b5p6od9s - 1) + (_kwmm2c1l - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_0446f4de+(_b5p6od9s - 1)),_apig8meb ,ref _iykhdriq );//* 
						//*              Apply H to A(1:i-1,i:n) from the right 
						//* 
						
						_n8gl0bxs("Right" ,"No transpose" ,"Backward" ,"Rowwise" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef((_dxpq0xkr - _b5p6od9s) + (int)1) ,ref _vyr1z1si ,ref Unsafe.AsRef(_dxpq0xkr - _ev4xhht5) ,(_vxfgpup9+(_b5p6od9s - 1) + (_kwmm2c1l - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_apig8meb ,ref _iykhdriq ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_apig8meb+(_vyr1z1si + (int)1 - 1)),ref _iykhdriq );
					}
					
Mark20:;
					// continue
				}
								}			}
			_4s3y5e1x = ((_b5p6od9s + _f7059815) - (int)1);
		}
		else
		{
			
			_4s3y5e1x = _ev4xhht5;
		}
		//* 
		//*     Use unblocked code to factor the last or only block 
		//* 
		
		if (_4s3y5e1x > (int)0)
		_wfabsqdk(ref _4s3y5e1x ,ref _dxpq0xkr ,ref Unsafe.AsRef(_dxpq0xkr - _ev4xhht5) ,_vxfgpup9 ,ref _ocv8fk5c ,_0446f4de ,_apig8meb );//* 
		
		*(_apig8meb+((int)1 - 1)) = DBLE(_e4ueamrn);//* 
		
		return;//* 
		//*     End of DTZRZF 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
