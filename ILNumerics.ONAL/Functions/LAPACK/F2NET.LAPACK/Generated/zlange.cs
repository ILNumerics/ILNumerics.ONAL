
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
//*> \brief \b ZLANGE returns the value of the 1-norm, Frobenius norm, infinity-norm, or the largest absolute value of any element of a general rectangular matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZLANGE + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zlange.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zlange.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zlange.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       DOUBLE PRECISION FUNCTION ZLANGE( NORM, M, N, A, LDA, WORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          NORM 
//*       INTEGER            LDA, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   WORK( * ) 
//*       COMPLEX*16         A( LDA, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZLANGE  returns the value of the one norm,  or the Frobenius norm, or 
//*> the  infinity norm,  or the  element of  largest absolute value  of a 
//*> complex matrix A. 
//*> \endverbatim 
//*> 
//*> \return ZLANGE 
//*> \verbatim 
//*> 
//*>    ZLANGE = ( max(abs(A(i,j))), NORM = 'M' or 'm' 
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
//*>          Specifies the value to be returned in ZLANGE as described 
//*>          above. 
//*> \endverbatim 
//*> 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of rows of the matrix A.  M >= 0.  When M = 0, 
//*>          ZLANGE is set to zero. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of columns of the matrix A.  N >= 0.  When N = 0, 
//*>          ZLANGE is set to zero. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension (LDA,N) 
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
//*>          WORK is DOUBLE PRECISION array, dimension (MAX(1,LWORK)), 
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
//*> \ingroup complex16GEauxiliary 
//* 
//*  ===================================================================== 

	 
	public static Double _o615qv2q(FString _gq71rsgu, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, complex* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _apig8meb)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)32 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Double _o615qv2q = default;
Double _kxg5drh2 =  1d;
Double _d0547bi2 =  0d;
Int32 _b5p6od9s =  default;
Int32 _znpjgsef =  default;
Double _6j9l5fwy =  default;
Double _lwoxlbje =  default;
Double _1ajfmh55 =  default;
Double* _8l4yph2p =  (Double*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Double) * ((int)2);
Double* _70n56i0m =  (Double*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Double) * ((int)2);
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
				System.Int32 __81fgg2dlsvn1344 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1344 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1344;
				for (__81fgg2count1344 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1344 + __81fgg2step1344) / __81fgg2step1344)), _znpjgsef = __81fgg2dlsvn1344; __81fgg2count1344 != 0; __81fgg2count1344--, _znpjgsef += (__81fgg2step1344)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn1345 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1345 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1345;
						for (__81fgg2count1345 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1345 + __81fgg2step1345) / __81fgg2step1345)), _b5p6od9s = __81fgg2dlsvn1345; __81fgg2count1345 != 0; __81fgg2count1345--, _b5p6od9s += (__81fgg2step1345)) {

						{
							
							_1ajfmh55 = ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) );
							if ((_lwoxlbje < _1ajfmh55) | _fk98jwhi(ref _1ajfmh55 ))
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
				System.Int32 __81fgg2dlsvn1346 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1346 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1346;
				for (__81fgg2count1346 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1346 + __81fgg2step1346) / __81fgg2step1346)), _znpjgsef = __81fgg2dlsvn1346; __81fgg2count1346 != 0; __81fgg2count1346--, _znpjgsef += (__81fgg2step1346)) {

				{
					
					_6j9l5fwy = _d0547bi2;
					{
						System.Int32 __81fgg2dlsvn1347 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1347 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1347;
						for (__81fgg2count1347 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1347 + __81fgg2step1347) / __81fgg2step1347)), _b5p6od9s = __81fgg2dlsvn1347; __81fgg2count1347 != 0; __81fgg2count1347--, _b5p6od9s += (__81fgg2step1347)) {

						{
							
							_6j9l5fwy = (_6j9l5fwy + ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
Mark30:;
							// continue
						}
												}					}
					if ((_lwoxlbje < _6j9l5fwy) | _fk98jwhi(ref _6j9l5fwy ))
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
				System.Int32 __81fgg2dlsvn1348 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1348 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1348;
				for (__81fgg2count1348 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1348 + __81fgg2step1348) / __81fgg2step1348)), _b5p6od9s = __81fgg2dlsvn1348; __81fgg2count1348 != 0; __81fgg2count1348--, _b5p6od9s += (__81fgg2step1348)) {

				{
					
					*(_apig8meb+(_b5p6od9s - 1)) = _d0547bi2;
Mark50:;
					// continue
				}
								}			}
			{
				System.Int32 __81fgg2dlsvn1349 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1349 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1349;
				for (__81fgg2count1349 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1349 + __81fgg2step1349) / __81fgg2step1349)), _znpjgsef = __81fgg2dlsvn1349; __81fgg2count1349 != 0; __81fgg2count1349--, _znpjgsef += (__81fgg2step1349)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn1350 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1350 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1350;
						for (__81fgg2count1350 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1350 + __81fgg2step1350) / __81fgg2step1350)), _b5p6od9s = __81fgg2dlsvn1350; __81fgg2count1350 != 0; __81fgg2count1350--, _b5p6od9s += (__81fgg2step1350)) {

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
				System.Int32 __81fgg2dlsvn1351 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1351 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1351;
				for (__81fgg2count1351 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1351 + __81fgg2step1351) / __81fgg2step1351)), _b5p6od9s = __81fgg2dlsvn1351; __81fgg2count1351 != 0; __81fgg2count1351--, _b5p6od9s += (__81fgg2step1351)) {

				{
					
					_1ajfmh55 = *(_apig8meb+(_b5p6od9s - 1));
					if ((_lwoxlbje < _1ajfmh55) | _fk98jwhi(ref _1ajfmh55 ))
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
				System.Int32 __81fgg2dlsvn1352 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step1352 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1352;
				for (__81fgg2count1352 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1352 + __81fgg2step1352) / __81fgg2step1352)), _znpjgsef = __81fgg2dlsvn1352; __81fgg2count1352 != 0; __81fgg2count1352--, _znpjgsef += (__81fgg2step1352)) {

				{
					
					*(_70n56i0m+((int)1 - 1)) = _d0547bi2;
					*(_70n56i0m+((int)2 - 1)) = _kxg5drh2;
					_s6ao1et5(ref _ev4xhht5 ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_70n56i0m+((int)1 - 1))) ,ref Unsafe.AsRef(*(_70n56i0m+((int)2 - 1))) );
					_2ah5b9jc(_8l4yph2p ,_70n56i0m );
Mark90:;
					// continue
				}
								}			}
			_lwoxlbje = (*(_8l4yph2p+((int)1 - 1)) * ILNumerics.F2NET.Intrinsics.SQRT(*(_8l4yph2p+((int)2 - 1)) ));
		}
		//* 
		
		_o615qv2q = _lwoxlbje;
		return _o615qv2q;//* 
		//*     End of ZLANGE 
		//* 
		
	}
	
	return _o615qv2q;
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
