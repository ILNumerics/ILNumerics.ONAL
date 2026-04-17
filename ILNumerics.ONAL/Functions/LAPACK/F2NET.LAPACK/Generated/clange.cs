
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
//*> \brief \b CLANGE returns the value of the 1-norm, Frobenius norm, infinity-norm, or the largest absolute value of any element of a general rectangular matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CLANGE + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/clange.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/clange.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/clange.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       REAL             FUNCTION CLANGE( NORM, M, N, A, LDA, WORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          NORM 
//*       INTEGER            LDA, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               WORK( * ) 
//*       COMPLEX            A( LDA, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CLANGE  returns the value of the one norm,  or the Frobenius norm, or 
//*> the  infinity norm,  or the  element of  largest absolute value  of a 
//*> complex matrix A. 
//*> \endverbatim 
//*> 
//*> \return CLANGE 
//*> \verbatim 
//*> 
//*>    CLANGE = ( max(abs(A(i,j))), NORM = 'M' or 'm' 
//*>             ( 
//*>             ( norm1(A),         NORM = '1', 'O' or 'o' 
//*>             ( 
//*>             ( normI(A),         NORM = 'I' or 'i' 
//*>             ( 
//*>             ( normF(A),         NORM = 'F', 'f', 'E' or 'e' 
//*> 
//*> where  norm1  denotes the  one norm of a matrix (maximum column sum), 
//*> normI  denotes the  infinity norm  of a matrix  (maximum row sum) and 
//*> normF  denotes the  Frobenius norm of a matrix (square root of sum of 
//*> squares).  Note that  max(abs(A(i,j)))  is not a consistent matrix norm. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] NORM 
//*> \verbatim 
//*>          NORM is CHARACTER*1 
//*>          Specifies the value to be returned in CLANGE as described 
//*>          above. 
//*> \endverbatim 
//*> 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of rows of the matrix A.  M >= 0.  When M = 0, 
//*>          CLANGE is set to zero. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of columns of the matrix A.  N >= 0.  When N = 0, 
//*>          CLANGE is set to zero. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is COMPLEX array, dimension (LDA,N) 
//*>          The m by n matrix A. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(M,1). 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is REAL array, dimension (MAX(1,LWORK)), 
//*>          where LWORK >= M when NORM = 'I'; otherwise, WORK is not 
//*>          referenced. 
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
//*> \ingroup complexGEauxiliary 
//* 
//*  ===================================================================== 

	 
	public static Single _t7qqtvjg(FString _gq71rsgu, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, Single* _apig8meb)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)16 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Single _t7qqtvjg = default;
Single _kxg5drh2 =  1f;
Single _d0547bi2 =  0f;
Int32 _b5p6od9s =  default;
Int32 _znpjgsef =  default;
Single _6j9l5fwy =  default;
Single _lwoxlbje =  default;
Single _1ajfmh55 =  default;
Single* _8l4yph2p =  (Single*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Single) * ((int)2);
Single* _70n56i0m =  (Single*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Single) * ((int)2);
string fLanavab = default;
#endregion  variable declarations
_gq71rsgu = _gq71rsgu.Convert(1);

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     December 2016 
		//* 
		//*     .. Scalar Arguments .. 
		//*     .. 
		//*     .. Array Arguments .. 
		//*     .. 
		//* 
		//* ===================================================================== 
		//* 
		//*     .. Parameters .. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Local Arrays .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		if (ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_dxpq0xkr ) == (int)0)
		{
			
			_lwoxlbje = _d0547bi2;
		}
		else
		if (_w8y2rzgy(_gq71rsgu ,"M" ))
		{
			//* 
			//*        Find max(abs(A(i,j))). 
			//* 
			
			_lwoxlbje = _d0547bi2;
			{
				System.Int32 __81fgg2dlsvn1119 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1119 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1119;
				for (__81fgg2count1119 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1119 + __81fgg2step1119) / __81fgg2step1119)), _znpjgsef = __81fgg2dlsvn1119; __81fgg2count1119 != 0; __81fgg2count1119--, _znpjgsef += (__81fgg2step1119)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn1120 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1120 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1120;
						for (__81fgg2count1120 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1120 + __81fgg2step1120) / __81fgg2step1120)), _b5p6od9s = __81fgg2dlsvn1120; __81fgg2count1120 != 0; __81fgg2count1120--, _b5p6od9s += (__81fgg2step1120)) {

						{
							
							_1ajfmh55 = ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) );
							if ((_lwoxlbje < _1ajfmh55) | _lilv8egi(ref _1ajfmh55 ))
							_lwoxlbje = _1ajfmh55;
Mark10:;
							// continue
						}
												}					}
Mark20:;
					// continue
				}
								}			}
		}
		else
		if ((_w8y2rzgy(_gq71rsgu ,"O" )) | (_gq71rsgu == "1"))
		{
			//* 
			//*        Find norm1(A). 
			//* 
			
			_lwoxlbje = _d0547bi2;
			{
				System.Int32 __81fgg2dlsvn1121 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1121 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1121;
				for (__81fgg2count1121 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1121 + __81fgg2step1121) / __81fgg2step1121)), _znpjgsef = __81fgg2dlsvn1121; __81fgg2count1121 != 0; __81fgg2count1121--, _znpjgsef += (__81fgg2step1121)) {

				{
					
					_6j9l5fwy = _d0547bi2;
					{
						System.Int32 __81fgg2dlsvn1122 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1122 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1122;
						for (__81fgg2count1122 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1122 + __81fgg2step1122) / __81fgg2step1122)), _b5p6od9s = __81fgg2dlsvn1122; __81fgg2count1122 != 0; __81fgg2count1122--, _b5p6od9s += (__81fgg2step1122)) {

						{
							
							_6j9l5fwy = (_6j9l5fwy + ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
Mark30:;
							// continue
						}
												}					}
					if ((_lwoxlbje < _6j9l5fwy) | _lilv8egi(ref _6j9l5fwy ))
					_lwoxlbje = _6j9l5fwy;
Mark40:;
					// continue
				}
								}			}
		}
		else
		if (_w8y2rzgy(_gq71rsgu ,"I" ))
		{
			//* 
			//*        Find normI(A). 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn1123 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1123 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1123;
				for (__81fgg2count1123 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1123 + __81fgg2step1123) / __81fgg2step1123)), _b5p6od9s = __81fgg2dlsvn1123; __81fgg2count1123 != 0; __81fgg2count1123--, _b5p6od9s += (__81fgg2step1123)) {

				{
					
					*(_apig8meb+(_b5p6od9s - 1)) = _d0547bi2;
Mark50:;
					// continue
				}
								}			}
			{
				System.Int32 __81fgg2dlsvn1124 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1124 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1124;
				for (__81fgg2count1124 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1124 + __81fgg2step1124) / __81fgg2step1124)), _znpjgsef = __81fgg2dlsvn1124; __81fgg2count1124 != 0; __81fgg2count1124--, _znpjgsef += (__81fgg2step1124)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn1125 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1125 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1125;
						for (__81fgg2count1125 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1125 + __81fgg2step1125) / __81fgg2step1125)), _b5p6od9s = __81fgg2dlsvn1125; __81fgg2count1125 != 0; __81fgg2count1125--, _b5p6od9s += (__81fgg2step1125)) {

						{
							
							*(_apig8meb+(_b5p6od9s - 1)) = (*(_apig8meb+(_b5p6od9s - 1)) + ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
Mark60:;
							// continue
						}
												}					}
Mark70:;
					// continue
				}
								}			}
			_lwoxlbje = _d0547bi2;
			{
				System.Int32 __81fgg2dlsvn1126 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1126 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1126;
				for (__81fgg2count1126 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1126 + __81fgg2step1126) / __81fgg2step1126)), _b5p6od9s = __81fgg2dlsvn1126; __81fgg2count1126 != 0; __81fgg2count1126--, _b5p6od9s += (__81fgg2step1126)) {

				{
					
					_1ajfmh55 = *(_apig8meb+(_b5p6od9s - 1));
					if ((_lwoxlbje < _1ajfmh55) | _lilv8egi(ref _1ajfmh55 ))
					_lwoxlbje = _1ajfmh55;
Mark80:;
					// continue
				}
								}			}
		}
		else
		if ((_w8y2rzgy(_gq71rsgu ,"F" )) | (_w8y2rzgy(_gq71rsgu ,"E" )))
		{
			//* 
			//*        Find normF(A). 
			//*        SSQ(1) is scale 
			//*        SSQ(2) is sum-of-squares 
			//*        For better accuracy, sum each column separately. 
			//* 
			
			*(_8l4yph2p+((int)1 - 1)) = _d0547bi2;
			*(_8l4yph2p+((int)2 - 1)) = _kxg5drh2;
			{
				System.Int32 __81fgg2dlsvn1127 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1127 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1127;
				for (__81fgg2count1127 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1127 + __81fgg2step1127) / __81fgg2step1127)), _znpjgsef = __81fgg2dlsvn1127; __81fgg2count1127 != 0; __81fgg2count1127--, _znpjgsef += (__81fgg2step1127)) {

				{
					
					*(_70n56i0m+((int)1 - 1)) = _d0547bi2;
					*(_70n56i0m+((int)2 - 1)) = _kxg5drh2;
					_bz1hd2n9(ref _ev4xhht5 ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_70n56i0m+((int)1 - 1))) ,ref Unsafe.AsRef(*(_70n56i0m+((int)2 - 1))) );
					_4hcafcgj(_8l4yph2p ,_70n56i0m );
Mark90:;
					// continue
				}
								}			}
			_lwoxlbje = (*(_8l4yph2p+((int)1 - 1)) * ILNumerics.F2NET.Intrinsics.SQRT(*(_8l4yph2p+((int)2 - 1)) ));
		}
		//* 
		
		_t7qqtvjg = _lwoxlbje;
		return _t7qqtvjg;//* 
		//*     End of CLANGE 
		//* 
		
	}
	
	return _t7qqtvjg;
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
