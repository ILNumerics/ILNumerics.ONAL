
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
//*> \brief \b SGETRI 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SGETRI + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/sgetri.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/sgetri.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/sgetri.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SGETRI( N, A, LDA, IPIV, WORK, LWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, LDA, LWORK, N 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IPIV( * ) 
//*       REAL               A( LDA, * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SGETRI computes the inverse of a matrix using the LU factorization 
//*> computed by SGETRF. 
//*> 
//*> This method inverts U and then computes inv(A) by solving the system 
//*> inv(A)*L = inv(U) for inv(A). 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix A.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is REAL array, dimension (LDA,N) 
//*>          On entry, the factors L and U from the factorization 
//*>          A = P*L*U as computed by SGETRF. 
//*>          On exit, if INFO = 0, the inverse of the original matrix A. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[in] IPIV 
//*> \verbatim 
//*>          IPIV is INTEGER array, dimension (N) 
//*>          The pivot indices from SGETRF; for 1<=i<=N, row i of the 
//*>          matrix was interchanged with row IPIV(i). 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is REAL array, dimension (MAX(1,LWORK)) 
//*>          On exit, if INFO=0, then WORK(1) returns the optimal LWORK. 
//*> \endverbatim 
//*> 
//*> \param[in] LWORK 
//*> \verbatim 
//*>          LWORK is INTEGER 
//*>          The dimension of the array WORK.  LWORK >= max(1,N). 
//*>          For optimal performance LWORK >= N*NB, where NB is 
//*>          the optimal blocksize returned by ILAENV. 
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
//*>          > 0:  if INFO = i, U(i,i) is exactly zero; the matrix is 
//*>                singular and its inverse could not be computed. 
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
//*> \ingroup realGEcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _s7pkoikw(ref Int32 _dxpq0xkr, Single* _vxfgpup9, ref Int32 _ocv8fk5c, Int32* _w1ilvusp, Single* _apig8meb, ref Int32 _6fnxzlyp, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
Boolean _lhlgm7z5 =  default;
Int32 _b5p6od9s =  default;
Int32 _7tvbphxm =  default;
Int32 _znpjgsef =  default;
Int32 _pscq8l5q =  default;
Int32 _j7zu8nju =  default;
Int32 _c2zk3fjj =  default;
Int32 _iykhdriq =  default;
Int32 _e4ueamrn =  default;
Int32 _f7059815 =  default;
Int32 _o80jnixx =  default;
Int32 _8dgyhtzt =  default;
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
		//*     .. External Functions .. 
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
		_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"SGETRI" ," " ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) );
		_e4ueamrn = (_dxpq0xkr * _f7059815);
		*(_apig8meb+((int)1 - 1)) = REAL(_e4ueamrn);
		_lhlgm7z5 = (_6fnxzlyp == (int)-1);
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if ((_6fnxzlyp < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr )) & (!(_lhlgm7z5)))
		{
			
			_gro5yvfo = (int)-6;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("SGETRI" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
		return;//* 
		//*     Form inv(U).  If INFO > 0 from STRTRI, then U is singular, 
		//*     and the inverse is not computed. 
		//* 
		
		_t6gv0wb2("Upper" ,"Non-unit" ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,ref _gro5yvfo );
		if (_gro5yvfo > (int)0)
		return;//* 
		
		_o80jnixx = (int)2;
		_iykhdriq = _dxpq0xkr;
		if ((_f7059815 > (int)1) & (_f7059815 < _dxpq0xkr))
		{
			
			_7tvbphxm = ILNumerics.F2NET.Intrinsics.MAX(_iykhdriq * _f7059815 ,(int)1 );
			if (_6fnxzlyp < _7tvbphxm)
			{
				
				_f7059815 = (_6fnxzlyp / _iykhdriq);
				_o80jnixx = ILNumerics.F2NET.Intrinsics.MAX((int)2 ,_4mvd6e4d(ref Unsafe.AsRef((int)2) ,"SGETRI" ," " ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ) );
			}
			
		}
		else
		{
			
			_7tvbphxm = _dxpq0xkr;
		}
		//* 
		//*     Solve the equation inv(A)*L = inv(U) for inv(A). 
		//* 
		
		if ((_f7059815 < _o80jnixx) | (_f7059815 >= _dxpq0xkr))
		{
			//* 
			//*        Use unblocked code. 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn1806 = (System.Int32)(_dxpq0xkr);
				System.Int32 __81fgg2step1806 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count1806;
				for (__81fgg2count1806 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1806 + __81fgg2step1806) / __81fgg2step1806)), _znpjgsef = __81fgg2dlsvn1806; __81fgg2count1806 != 0; __81fgg2count1806--, _znpjgsef += (__81fgg2step1806)) {

				{
					//* 
					//*           Copy current column of L to WORK and replace with zeros. 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn1807 = (System.Int32)((_znpjgsef + (int)1));
						const System.Int32 __81fgg2step1807 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1807;
						for (__81fgg2count1807 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1807 + __81fgg2step1807) / __81fgg2step1807)), _b5p6od9s = __81fgg2dlsvn1807; __81fgg2count1807 != 0; __81fgg2count1807--, _b5p6od9s += (__81fgg2step1807)) {

						{
							
							*(_apig8meb+(_b5p6od9s - 1)) = *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c));
							*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
Mark10:;
							// continue
						}
												}					}//* 
					//*           Compute current column of inv(A). 
					//* 
					
					if (_znpjgsef < _dxpq0xkr)
					_9mvi1n8m("No transpose" ,ref _dxpq0xkr ,ref Unsafe.AsRef(_dxpq0xkr - _znpjgsef) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_apig8meb+(_znpjgsef + (int)1 - 1)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
Mark20:;
					// continue
				}
								}			}
		}
		else
		{
			//* 
			//*        Use blocked code. 
			//* 
			
			_8dgyhtzt = ((((_dxpq0xkr - (int)1) / _f7059815) * _f7059815) + (int)1);
			{
				System.Int32 __81fgg2dlsvn1808 = (System.Int32)(_8dgyhtzt);
				System.Int32 __81fgg2step1808 = (System.Int32)(-(_f7059815));
				System.Int32 __81fgg2count1808;
				for (__81fgg2count1808 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1808 + __81fgg2step1808) / __81fgg2step1808)), _znpjgsef = __81fgg2dlsvn1808; __81fgg2count1808 != 0; __81fgg2count1808--, _znpjgsef += (__81fgg2step1808)) {

				{
					
					_pscq8l5q = ILNumerics.F2NET.Intrinsics.MIN(_f7059815 ,(_dxpq0xkr - _znpjgsef) + (int)1 );//* 
					//*           Copy current block column of L to WORK and replace with 
					//*           zeros. 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn1809 = (System.Int32)(_znpjgsef);
						const System.Int32 __81fgg2step1809 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1809;
						for (__81fgg2count1809 = System.Math.Max(0, (System.Int32)(((System.Int32)((_znpjgsef + _pscq8l5q) - (int)1) - __81fgg2dlsvn1809 + __81fgg2step1809) / __81fgg2step1809)), _j7zu8nju = __81fgg2dlsvn1809; __81fgg2count1809 != 0; __81fgg2count1809--, _j7zu8nju += (__81fgg2step1809)) {

						{
							
							{
								System.Int32 __81fgg2dlsvn1810 = (System.Int32)((_j7zu8nju + (int)1));
								const System.Int32 __81fgg2step1810 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1810;
								for (__81fgg2count1810 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1810 + __81fgg2step1810) / __81fgg2step1810)), _b5p6od9s = __81fgg2dlsvn1810; __81fgg2count1810 != 0; __81fgg2count1810--, _b5p6od9s += (__81fgg2step1810)) {

								{
									
									*(_apig8meb+(_b5p6od9s + ((_j7zu8nju - _znpjgsef) * _iykhdriq) - 1)) = *(_vxfgpup9+(_b5p6od9s - 1) + (_j7zu8nju - 1) * 1 * (_ocv8fk5c));
									*(_vxfgpup9+(_b5p6od9s - 1) + (_j7zu8nju - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
Mark30:;
									// continue
								}
																}							}
Mark40:;
							// continue
						}
												}					}//* 
					//*           Compute current block column of inv(A). 
					//* 
					
					if ((_znpjgsef + _pscq8l5q) <= _dxpq0xkr)
					_b8wa9454("No transpose" ,"No transpose" ,ref _dxpq0xkr ,ref _pscq8l5q ,ref Unsafe.AsRef(((_dxpq0xkr - _znpjgsef) - _pscq8l5q) + (int)1) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef + _pscq8l5q - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_apig8meb+(_znpjgsef + _pscq8l5q - 1)),ref _iykhdriq ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
					_ieiywhin("Right" ,"Lower" ,"No transpose" ,"Unit" ,ref _dxpq0xkr ,ref _pscq8l5q ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+(_znpjgsef - 1)),ref _iykhdriq ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
Mark50:;
					// continue
				}
								}			}
		}
		//* 
		//*     Apply column interchanges. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn1811 = (System.Int32)((_dxpq0xkr - (int)1));
			System.Int32 __81fgg2step1811 = (System.Int32)((int)-1);
			System.Int32 __81fgg2count1811;
			for (__81fgg2count1811 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1811 + __81fgg2step1811) / __81fgg2step1811)), _znpjgsef = __81fgg2dlsvn1811; __81fgg2count1811 != 0; __81fgg2count1811--, _znpjgsef += (__81fgg2step1811)) {

			{
				
				_c2zk3fjj = *(_w1ilvusp+(_znpjgsef - 1));
				if (_c2zk3fjj != _znpjgsef)
				_ahhuglvd(ref _dxpq0xkr ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,(_vxfgpup9+((int)1 - 1) + (_c2zk3fjj - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
Mark60:;
				// continue
			}
						}		}//* 
		
		*(_apig8meb+((int)1 - 1)) = REAL(_7tvbphxm);
		return;//* 
		//*     End of SGETRI 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
