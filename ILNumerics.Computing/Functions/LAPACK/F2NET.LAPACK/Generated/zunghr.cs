
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
//*> \brief \b ZUNGHR 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZUNGHR + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zunghr.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zunghr.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zunghr.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZUNGHR( N, ILO, IHI, A, LDA, TAU, WORK, LWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            IHI, ILO, INFO, LDA, LWORK, N 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX*16         A( LDA, * ), TAU( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZUNGHR generates a complex unitary matrix Q which is defined as the 
//*> product of IHI-ILO elementary reflectors of order N, as returned by 
//*> ZGEHRD: 
//*> 
//*> Q = H(ilo) H(ilo+1) . . . H(ihi-1). 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix Q. N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] ILO 
//*> \verbatim 
//*>          ILO is INTEGER 
//*> \endverbatim 
//*> 
//*> \param[in] IHI 
//*> \verbatim 
//*>          IHI is INTEGER 
//*> 
//*>          ILO and IHI must have the same values as in the previous call 
//*>          of ZGEHRD. Q is equal to the unit matrix except in the 
//*>          submatrix Q(ilo+1:ihi,ilo+1:ihi). 
//*>          1 <= ILO <= IHI <= N, if N > 0; ILO=1 and IHI=0, if N=0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension (LDA,N) 
//*>          On entry, the vectors which define the elementary reflectors, 
//*>          as returned by ZGEHRD. 
//*>          On exit, the N-by-N unitary matrix Q. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A. LDA >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[in] TAU 
//*> \verbatim 
//*>          TAU is COMPLEX*16 array, dimension (N-1) 
//*>          TAU(i) must contain the scalar factor of the elementary 
//*>          reflector H(i), as returned by ZGEHRD. 
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
//*>          The dimension of the array WORK. LWORK >= IHI-ILO. 
//*>          For optimum performance LWORK >= (IHI-ILO)*NB, where NB is 
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
//*> \ingroup complex16OTHERcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _3r1g0v7p(ref Int32 _dxpq0xkr, ref Int32 _pew3blan, ref Int32 _9c1csucx, complex* _vxfgpup9, ref Int32 _ocv8fk5c, complex* _0446f4de, complex* _apig8meb, ref Int32 _6fnxzlyp, ref Int32 _gro5yvfo)
	{
#region variable declarations
complex _d0547bi2 =   new fcomplex(0f,0f);
complex _kxg5drh2 =   new fcomplex(1f,0f);
Boolean _lhlgm7z5 =  default;
Int32 _b5p6od9s =  default;
Int32 _itfnbz60 =  default;
Int32 _znpjgsef =  default;
Int32 _e4ueamrn =  default;
Int32 _f7059815 =  default;
Int32 _aym8a085 =  default;
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
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input arguments 
		//* 
		
		_gro5yvfo = (int)0;
		_aym8a085 = (_9c1csucx - _pew3blan);
		_lhlgm7z5 = (_6fnxzlyp == (int)-1);
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if ((_pew3blan < (int)1) | (_pew3blan > ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr )))
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if ((_9c1csucx < ILNumerics.F2NET.Intrinsics.MIN(_pew3blan ,_dxpq0xkr )) | (_9c1csucx > _dxpq0xkr))
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)-5;
		}
		else
		if ((_6fnxzlyp < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_aym8a085 )) & (!(_lhlgm7z5)))
		{
			
			_gro5yvfo = (int)-8;
		}
		//* 
		
		if (_gro5yvfo == (int)0)
		{
			
			_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZUNGQR" ," " ,ref _aym8a085 ,ref _aym8a085 ,ref _aym8a085 ,ref Unsafe.AsRef((int)-1) );
			_e4ueamrn = (ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_aym8a085 ) * _f7059815);
			*(_apig8meb+((int)1 - 1)) = DCMPLX(_e4ueamrn);
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("ZUNGHR" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
			
			*(_apig8meb+((int)1 - 1)) = DCMPLX((int)1);
			return;
		}
		//* 
		//*     Shift the vectors which define the elementary reflectors one 
		//*     column to the right, and set the first ilo and the last n-ihi 
		//*     rows and columns to those of the unit matrix 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn2808 = (System.Int32)(_9c1csucx);
			System.Int32 __81fgg2step2808 = (System.Int32)((int)-1);
			System.Int32 __81fgg2count2808;
			for (__81fgg2count2808 = System.Math.Max(0, (System.Int32)(((System.Int32)(_pew3blan + (int)1) - __81fgg2dlsvn2808 + __81fgg2step2808) / __81fgg2step2808)), _znpjgsef = __81fgg2dlsvn2808; __81fgg2count2808 != 0; __81fgg2count2808--, _znpjgsef += (__81fgg2step2808)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn2809 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2809 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2809;
					for (__81fgg2count2809 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn2809 + __81fgg2step2809) / __81fgg2step2809)), _b5p6od9s = __81fgg2dlsvn2809; __81fgg2count2809 != 0; __81fgg2count2809--, _b5p6od9s += (__81fgg2step2809)) {

					{
						
						*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
Mark10:;
						// continue
					}
										}				}
				{
					System.Int32 __81fgg2dlsvn2810 = (System.Int32)((_znpjgsef + (int)1));
					const System.Int32 __81fgg2step2810 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2810;
					for (__81fgg2count2810 = System.Math.Max(0, (System.Int32)(((System.Int32)(_9c1csucx) - __81fgg2dlsvn2810 + __81fgg2step2810) / __81fgg2step2810)), _b5p6od9s = __81fgg2dlsvn2810; __81fgg2count2810 != 0; __81fgg2count2810--, _b5p6od9s += (__81fgg2step2810)) {

					{
						
						*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - (int)1 - 1) * 1 * (_ocv8fk5c));
Mark20:;
						// continue
					}
										}				}
				{
					System.Int32 __81fgg2dlsvn2811 = (System.Int32)((_9c1csucx + (int)1));
					const System.Int32 __81fgg2step2811 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2811;
					for (__81fgg2count2811 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2811 + __81fgg2step2811) / __81fgg2step2811)), _b5p6od9s = __81fgg2dlsvn2811; __81fgg2count2811 != 0; __81fgg2count2811--, _b5p6od9s += (__81fgg2step2811)) {

					{
						
						*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
Mark30:;
						// continue
					}
										}				}
Mark40:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn2812 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2812 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2812;
			for (__81fgg2count2812 = System.Math.Max(0, (System.Int32)(((System.Int32)(_pew3blan) - __81fgg2dlsvn2812 + __81fgg2step2812) / __81fgg2step2812)), _znpjgsef = __81fgg2dlsvn2812; __81fgg2count2812 != 0; __81fgg2count2812--, _znpjgsef += (__81fgg2step2812)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn2813 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2813 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2813;
					for (__81fgg2count2813 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2813 + __81fgg2step2813) / __81fgg2step2813)), _b5p6od9s = __81fgg2dlsvn2813; __81fgg2count2813 != 0; __81fgg2count2813--, _b5p6od9s += (__81fgg2step2813)) {

					{
						
						*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
Mark50:;
						// continue
					}
										}				}
				*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;
Mark60:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn2814 = (System.Int32)((_9c1csucx + (int)1));
			const System.Int32 __81fgg2step2814 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2814;
			for (__81fgg2count2814 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2814 + __81fgg2step2814) / __81fgg2step2814)), _znpjgsef = __81fgg2dlsvn2814; __81fgg2count2814 != 0; __81fgg2count2814--, _znpjgsef += (__81fgg2step2814)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn2815 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2815 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2815;
					for (__81fgg2count2815 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2815 + __81fgg2step2815) / __81fgg2step2815)), _b5p6od9s = __81fgg2dlsvn2815; __81fgg2count2815 != 0; __81fgg2count2815--, _b5p6od9s += (__81fgg2step2815)) {

					{
						
						*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _d0547bi2;
Mark70:;
						// continue
					}
										}				}
				*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;
Mark80:;
				// continue
			}
						}		}//* 
		
		if (_aym8a085 > (int)0)
		{
			//* 
			//*        Generate Q(ilo+1:ihi,ilo+1:ihi) 
			//* 
			
			_13b6etkp(ref _aym8a085 ,ref _aym8a085 ,ref _aym8a085 ,(_vxfgpup9+(_pew3blan + (int)1 - 1) + (_pew3blan + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_0446f4de+(_pew3blan - 1)),_apig8meb ,ref _6fnxzlyp ,ref _itfnbz60 );
		}
		
		*(_apig8meb+((int)1 - 1)) = DCMPLX(_e4ueamrn);
		return;//* 
		//*     End of ZUNGHR 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
