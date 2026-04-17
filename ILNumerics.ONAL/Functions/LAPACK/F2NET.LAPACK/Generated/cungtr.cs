
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
//*> \brief \b CUNGTR 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CUNGTR + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/cungtr.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/cungtr.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/cungtr.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CUNGTR( UPLO, N, A, LDA, TAU, WORK, LWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          UPLO 
//*       INTEGER            INFO, LDA, LWORK, N 
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
//*> CUNGTR generates a complex unitary matrix Q which is defined as the 
//*> product of n-1 elementary reflectors of order N, as returned by 
//*> CHETRD: 
//*> 
//*> if UPLO = 'U', Q = H(n-1) . . . H(2) H(1), 
//*> 
//*> if UPLO = 'L', Q = H(1) H(2) . . . H(n-1). 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>          = 'U': Upper triangle of A contains elementary reflectors 
//*>                 from CHETRD; 
//*>          = 'L': Lower triangle of A contains elementary reflectors 
//*>                 from CHETRD. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix Q. N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is COMPLEX array, dimension (LDA,N) 
//*>          On entry, the vectors which define the elementary reflectors, 
//*>          as returned by CHETRD. 
//*>          On exit, the N-by-N unitary matrix Q. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A. LDA >= N. 
//*> \endverbatim 
//*> 
//*> \param[in] TAU 
//*> \verbatim 
//*>          TAU is COMPLEX array, dimension (N-1) 
//*>          TAU(i) must contain the scalar factor of the elementary 
//*>          reflector H(i), as returned by CHETRD. 
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
//*>          The dimension of the array WORK. LWORK >= N-1. 
//*>          For optimum performance LWORK >= (N-1)*NB, where NB is 
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

	 
	public static void _p2ipbwy2(FString _9wyre9zc, ref Int32 _dxpq0xkr, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, fcomplex* _0446f4de, fcomplex* _apig8meb, ref Int32 _6fnxzlyp, ref Int32 _gro5yvfo)
	{
#region variable declarations
fcomplex _d0547bi2 =   new fcomplex(0f,0f);
fcomplex _kxg5drh2 =   new fcomplex(1f,0f);
Boolean _lhlgm7z5 =  default;
Boolean _l08igmvf =  default;
Int32 _b5p6od9s =  default;
Int32 _itfnbz60 =  default;
Int32 _znpjgsef =  default;
Int32 _e4ueamrn =  default;
Int32 _f7059815 =  default;
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
		//*     .. External Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input arguments 
		//* 
		
		_gro5yvfo = (int)0;
		_lhlgm7z5 = (_6fnxzlyp == (int)-1);
		_l08igmvf = _w8y2rzgy(_9wyre9zc ,"U" );
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
		if ((_6fnxzlyp < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr - (int)1 )) & (!(_lhlgm7z5)))
		{
			
			_gro5yvfo = (int)-7;
		}
		//* 
		
		if (_gro5yvfo == (int)0)
		{
			
			if (_l08igmvf)
			{
				
				_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"CUNGQL" ," " ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef((int)-1) );
			}
			else
			{
				
				_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"CUNGQR" ," " ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef((int)-1) );
			}
			
			_e4ueamrn = (ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr - (int)1 ) * _f7059815);
			*(_apig8meb+((int)1 - 1)) = CMPLX(_e4ueamrn);
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("CUNGTR" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
		
		if (_l08igmvf)
		{
			//* 
			//*        Q was determined by a call to CHETRD with UPLO = 'U' 
			//* 
			//*        Shift the vectors which define the elementary reflectors one 
			//*        column to the left, and set the last row and column of Q to 
			//*        those of the unit matrix 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn3865 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step3865 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3865;
				for (__81fgg2count3865 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn3865 + __81fgg2step3865) / __81fgg2step3865)), _znpjgsef = __81fgg2dlsvn3865; __81fgg2count3865 != 0; __81fgg2count3865--, _znpjgsef += (__81fgg2step3865)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn3866 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3866 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3866;
						for (__81fgg2count3866 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn3866 + __81fgg2step3866) / __81fgg2step3866)), _b5p6od9s = __81fgg2dlsvn3866; __81fgg2count3866 != 0; __81fgg2count3866--, _b5p6od9s += (__81fgg2step3866)) {

						{
							
							*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef + (int)1 - 1) * 1 * (_ocv8fk5c));
Mark10:;
							// continue
						}
												}					}
					*(_vxfgpup9+(_dxpq0xkr - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
Mark20:;
					// continue
				}
								}			}
			{
				System.Int32 __81fgg2dlsvn3867 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step3867 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3867;
				for (__81fgg2count3867 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn3867 + __81fgg2step3867) / __81fgg2step3867)), _b5p6od9s = __81fgg2dlsvn3867; __81fgg2count3867 != 0; __81fgg2count3867--, _b5p6od9s += (__81fgg2step3867)) {

				{
					
					*(_vxfgpup9+(_b5p6od9s - 1) + (_dxpq0xkr - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
Mark30:;
					// continue
				}
								}			}
			*(_vxfgpup9+(_dxpq0xkr - 1) + (_dxpq0xkr - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;//* 
			//*        Generate Q(1:n-1,1:n-1) 
			//* 
			
			_pqmj5hkr(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,_vxfgpup9 ,ref _ocv8fk5c ,_0446f4de ,_apig8meb ,ref _6fnxzlyp ,ref _itfnbz60 );//* 
			
		}
		else
		{
			//* 
			//*        Q was determined by a call to CHETRD with UPLO = 'L'. 
			//* 
			//*        Shift the vectors which define the elementary reflectors one 
			//*        column to the right, and set the first row and column of Q to 
			//*        those of the unit matrix 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn3868 = (System.Int32)(_dxpq0xkr);
				System.Int32 __81fgg2step3868 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count3868;
				for (__81fgg2count3868 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)2) - __81fgg2dlsvn3868 + __81fgg2step3868) / __81fgg2step3868)), _znpjgsef = __81fgg2dlsvn3868; __81fgg2count3868 != 0; __81fgg2count3868--, _znpjgsef += (__81fgg2step3868)) {

				{
					
					*(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
					{
						System.Int32 __81fgg2dlsvn3869 = (System.Int32)((_znpjgsef + (int)1));
						const System.Int32 __81fgg2step3869 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3869;
						for (__81fgg2count3869 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3869 + __81fgg2step3869) / __81fgg2step3869)), _b5p6od9s = __81fgg2dlsvn3869; __81fgg2count3869 != 0; __81fgg2count3869--, _b5p6od9s += (__81fgg2step3869)) {

						{
							
							*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - (int)1 - 1) * 1 * (_ocv8fk5c));
Mark40:;
							// continue
						}
												}					}
Mark50:;
					// continue
				}
								}			}
			*(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;
			{
				System.Int32 __81fgg2dlsvn3870 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step3870 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3870;
				for (__81fgg2count3870 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3870 + __81fgg2step3870) / __81fgg2step3870)), _b5p6od9s = __81fgg2dlsvn3870; __81fgg2count3870 != 0; __81fgg2count3870--, _b5p6od9s += (__81fgg2step3870)) {

				{
					
					*(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
Mark60:;
					// continue
				}
								}			}
			if (_dxpq0xkr > (int)1)
			{
				//* 
				//*           Generate Q(2:n,2:n) 
				//* 
				
				_hfwn2zbk(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,(_vxfgpup9+((int)2 - 1) + ((int)2 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_0446f4de ,_apig8meb ,ref _6fnxzlyp ,ref _itfnbz60 );
			}
			
		}
		
		*(_apig8meb+((int)1 - 1)) = CMPLX(_e4ueamrn);
		return;//* 
		//*     End of CUNGTR 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
