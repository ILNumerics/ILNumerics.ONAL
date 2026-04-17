
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
//*> \brief \b ZLANSY returns the value of the 1-norm, or the Frobenius norm, or the infinity norm, or the element of largest absolute value of a complex symmetric matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZLANSY + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zlansy.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zlansy.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zlansy.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       DOUBLE PRECISION FUNCTION ZLANSY( NORM, UPLO, N, A, LDA, WORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          NORM, UPLO 
//*       INTEGER            LDA, N 
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
//*> ZLANSY  returns the value of the one norm,  or the Frobenius norm, or 
//*> the  infinity norm,  or the  element of  largest absolute value  of a 
//*> complex symmetric matrix A. 
//*> \endverbatim 
//*> 
//*> \return ZLANSY 
//*> \verbatim 
//*> 
//*>    ZLANSY = ( max(abs(A(i,j))), NORM = 'M' or 'm' 
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
//*>          Specifies the value to be returned in ZLANSY as described 
//*>          above. 
//*> \endverbatim 
//*> 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>          Specifies whether the upper or lower triangular part of the 
//*>          symmetric matrix A is to be referenced. 
//*>          = 'U':  Upper triangular part of A is referenced 
//*>          = 'L':  Lower triangular part of A is referenced 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix A.  N >= 0.  When N = 0, ZLANSY is 
//*>          set to zero. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension (LDA,N) 
//*>          The symmetric matrix A.  If UPLO = 'U', the leading n by n 
//*>          upper triangular part of A contains the upper triangular part 
//*>          of the matrix A, and the strictly lower triangular part of A 
//*>          is not referenced.  If UPLO = 'L', the leading n by n lower 
//*>          triangular part of A contains the lower triangular part of 
//*>          the matrix A, and the strictly upper triangular part of A is 
//*>          not referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(N,1). 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is DOUBLE PRECISION array, dimension (MAX(1,LWORK)), 
//*>          where LWORK >= N when NORM = 'I' or '1' or 'O'; otherwise, 
//*>          WORK is not referenced. 
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
//*> \ingroup complex16SYauxiliary 
//* 
//*  ===================================================================== 

	 
	public static Double _bkdqwg1x(FString _gq71rsgu, FString _9wyre9zc, ref Int32 _dxpq0xkr, complex* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _apig8meb)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)32 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Double _bkdqwg1x = default;
Double _kxg5drh2 =  1d;
Double _d0547bi2 =  0d;
Int32 _b5p6od9s =  default;
Int32 _znpjgsef =  default;
Double _8lucc4fb =  default;
Double _6j9l5fwy =  default;
Double _lwoxlbje =  default;
Double* _8l4yph2p =  (Double*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Double) * ((int)2);
Double* _70n56i0m =  (Double*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Double) * ((int)2);
string fLanavab = default;
#endregion  variable declarations
_gq71rsgu = _gq71rsgu.Convert(1);
_9wyre9zc = _9wyre9zc.Convert(1);

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
		
		if (_dxpq0xkr == (int)0)
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
			if (_w8y2rzgy(_9wyre9zc ,"U" ))
			{
				
				{
					System.Int32 __81fgg2dlsvn3608 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3608 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3608;
					for (__81fgg2count3608 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3608 + __81fgg2step3608) / __81fgg2step3608)), _znpjgsef = __81fgg2dlsvn3608; __81fgg2count3608 != 0; __81fgg2count3608--, _znpjgsef += (__81fgg2step3608)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn3609 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3609 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3609;
							for (__81fgg2count3609 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef) - __81fgg2dlsvn3609 + __81fgg2step3609) / __81fgg2step3609)), _b5p6od9s = __81fgg2dlsvn3609; __81fgg2count3609 != 0; __81fgg2count3609--, _b5p6od9s += (__81fgg2step3609)) {

							{
								
								_6j9l5fwy = ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) );
								if ((_lwoxlbje < _6j9l5fwy) | _fk98jwhi(ref _6j9l5fwy ))
								_lwoxlbje = _6j9l5fwy;
Mark10:;
								// continue
							}
														}						}
Mark20:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn3610 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3610 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3610;
					for (__81fgg2count3610 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3610 + __81fgg2step3610) / __81fgg2step3610)), _znpjgsef = __81fgg2dlsvn3610; __81fgg2count3610 != 0; __81fgg2count3610--, _znpjgsef += (__81fgg2step3610)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn3611 = (System.Int32)(_znpjgsef);
							const System.Int32 __81fgg2step3611 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3611;
							for (__81fgg2count3611 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3611 + __81fgg2step3611) / __81fgg2step3611)), _b5p6od9s = __81fgg2dlsvn3611; __81fgg2count3611 != 0; __81fgg2count3611--, _b5p6od9s += (__81fgg2step3611)) {

							{
								
								_6j9l5fwy = ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) );
								if ((_lwoxlbje < _6j9l5fwy) | _fk98jwhi(ref _6j9l5fwy ))
								_lwoxlbje = _6j9l5fwy;
Mark30:;
								// continue
							}
														}						}
Mark40:;
						// continue
					}
										}				}
			}
			
		}
		else
		if (((_w8y2rzgy(_gq71rsgu ,"I" )) | (_w8y2rzgy(_gq71rsgu ,"O" ))) | (_gq71rsgu == "1"))
		{
			//* 
			//*        Find normI(A) ( = norm1(A), since A is symmetric). 
			//* 
			
			_lwoxlbje = _d0547bi2;
			if (_w8y2rzgy(_9wyre9zc ,"U" ))
			{
				
				{
					System.Int32 __81fgg2dlsvn3612 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3612 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3612;
					for (__81fgg2count3612 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3612 + __81fgg2step3612) / __81fgg2step3612)), _znpjgsef = __81fgg2dlsvn3612; __81fgg2count3612 != 0; __81fgg2count3612--, _znpjgsef += (__81fgg2step3612)) {

					{
						
						_6j9l5fwy = _d0547bi2;
						{
							System.Int32 __81fgg2dlsvn3613 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3613 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3613;
							for (__81fgg2count3613 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn3613 + __81fgg2step3613) / __81fgg2step3613)), _b5p6od9s = __81fgg2dlsvn3613; __81fgg2count3613 != 0; __81fgg2count3613--, _b5p6od9s += (__81fgg2step3613)) {

							{
								
								_8lucc4fb = ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) );
								_6j9l5fwy = (_6j9l5fwy + _8lucc4fb);
								*(_apig8meb+(_b5p6od9s - 1)) = (*(_apig8meb+(_b5p6od9s - 1)) + _8lucc4fb);
Mark50:;
								// continue
							}
														}						}
						*(_apig8meb+(_znpjgsef - 1)) = (_6j9l5fwy + ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
Mark60:;
						// continue
					}
										}				}
				{
					System.Int32 __81fgg2dlsvn3614 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3614 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3614;
					for (__81fgg2count3614 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3614 + __81fgg2step3614) / __81fgg2step3614)), _b5p6od9s = __81fgg2dlsvn3614; __81fgg2count3614 != 0; __81fgg2count3614--, _b5p6od9s += (__81fgg2step3614)) {

					{
						
						_6j9l5fwy = *(_apig8meb+(_b5p6od9s - 1));
						if ((_lwoxlbje < _6j9l5fwy) | _fk98jwhi(ref _6j9l5fwy ))
						_lwoxlbje = _6j9l5fwy;
Mark70:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn3615 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3615 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3615;
					for (__81fgg2count3615 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3615 + __81fgg2step3615) / __81fgg2step3615)), _b5p6od9s = __81fgg2dlsvn3615; __81fgg2count3615 != 0; __81fgg2count3615--, _b5p6od9s += (__81fgg2step3615)) {

					{
						
						*(_apig8meb+(_b5p6od9s - 1)) = _d0547bi2;
Mark80:;
						// continue
					}
										}				}
				{
					System.Int32 __81fgg2dlsvn3616 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3616 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3616;
					for (__81fgg2count3616 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3616 + __81fgg2step3616) / __81fgg2step3616)), _znpjgsef = __81fgg2dlsvn3616; __81fgg2count3616 != 0; __81fgg2count3616--, _znpjgsef += (__81fgg2step3616)) {

					{
						
						_6j9l5fwy = (*(_apig8meb+(_znpjgsef - 1)) + ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ));
						{
							System.Int32 __81fgg2dlsvn3617 = (System.Int32)((_znpjgsef + (int)1));
							const System.Int32 __81fgg2step3617 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3617;
							for (__81fgg2count3617 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3617 + __81fgg2step3617) / __81fgg2step3617)), _b5p6od9s = __81fgg2dlsvn3617; __81fgg2count3617 != 0; __81fgg2count3617--, _b5p6od9s += (__81fgg2step3617)) {

							{
								
								_8lucc4fb = ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) );
								_6j9l5fwy = (_6j9l5fwy + _8lucc4fb);
								*(_apig8meb+(_b5p6od9s - 1)) = (*(_apig8meb+(_b5p6od9s - 1)) + _8lucc4fb);
Mark90:;
								// continue
							}
														}						}
						if ((_lwoxlbje < _6j9l5fwy) | _fk98jwhi(ref _6j9l5fwy ))
						_lwoxlbje = _6j9l5fwy;
Mark100:;
						// continue
					}
										}				}
			}
			
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
			*(_8l4yph2p+((int)2 - 1)) = _kxg5drh2;//* 
			//*        Sum off-diagonals 
			//* 
			
			if (_w8y2rzgy(_9wyre9zc ,"U" ))
			{
				
				{
					System.Int32 __81fgg2dlsvn3618 = (System.Int32)((int)2);
					const System.Int32 __81fgg2step3618 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3618;
					for (__81fgg2count3618 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3618 + __81fgg2step3618) / __81fgg2step3618)), _znpjgsef = __81fgg2dlsvn3618; __81fgg2count3618 != 0; __81fgg2count3618--, _znpjgsef += (__81fgg2step3618)) {

					{
						
						*(_70n56i0m+((int)1 - 1)) = _d0547bi2;
						*(_70n56i0m+((int)2 - 1)) = _kxg5drh2;
						_s6ao1et5(ref Unsafe.AsRef(_znpjgsef - (int)1) ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_70n56i0m+((int)1 - 1))) ,ref Unsafe.AsRef(*(_70n56i0m+((int)2 - 1))) );
						_2ah5b9jc(_8l4yph2p ,_70n56i0m );
Mark110:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn3619 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3619 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3619;
					for (__81fgg2count3619 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn3619 + __81fgg2step3619) / __81fgg2step3619)), _znpjgsef = __81fgg2dlsvn3619; __81fgg2count3619 != 0; __81fgg2count3619--, _znpjgsef += (__81fgg2step3619)) {

					{
						
						*(_70n56i0m+((int)1 - 1)) = _d0547bi2;
						*(_70n56i0m+((int)2 - 1)) = _kxg5drh2;
						_s6ao1et5(ref Unsafe.AsRef(_dxpq0xkr - _znpjgsef) ,(_vxfgpup9+(_znpjgsef + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_70n56i0m+((int)1 - 1))) ,ref Unsafe.AsRef(*(_70n56i0m+((int)2 - 1))) );
						_2ah5b9jc(_8l4yph2p ,_70n56i0m );
Mark120:;
						// continue
					}
										}				}
			}
			
			*(_8l4yph2p+((int)2 - 1)) = ((int)2 * *(_8l4yph2p+((int)2 - 1)));//* 
			//*        Sum diagonal 
			//* 
			
			*(_70n56i0m+((int)1 - 1)) = _d0547bi2;
			*(_70n56i0m+((int)2 - 1)) = _kxg5drh2;
			_s6ao1et5(ref _dxpq0xkr ,_vxfgpup9 ,ref Unsafe.AsRef(_ocv8fk5c + (int)1) ,ref Unsafe.AsRef(*(_70n56i0m+((int)1 - 1))) ,ref Unsafe.AsRef(*(_70n56i0m+((int)2 - 1))) );
			_2ah5b9jc(_8l4yph2p ,_70n56i0m );
			_lwoxlbje = (*(_8l4yph2p+((int)1 - 1)) * ILNumerics.F2NET.Intrinsics.SQRT(*(_8l4yph2p+((int)2 - 1)) ));
		}
		//* 
		
		_bkdqwg1x = _lwoxlbje;
		return _bkdqwg1x;//* 
		//*     End of ZLANSY 
		//* 
		
	}
	
	return _bkdqwg1x;
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
