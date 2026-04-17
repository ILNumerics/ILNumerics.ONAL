
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
//*> \brief \b CLANHE returns the value of the 1-norm, or the Frobenius norm, or the infinity norm, or the element of largest absolute value of a complex Hermitian matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CLANHE + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/clanhe.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/clanhe.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/clanhe.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       REAL             FUNCTION CLANHE( NORM, UPLO, N, A, LDA, WORK ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          NORM, UPLO 
//*       INTEGER            LDA, N 
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
//*> CLANHE  returns the value of the one norm,  or the Frobenius norm, or 
//*> the  infinity norm,  or the  element of  largest absolute value  of a 
//*> complex hermitian matrix A. 
//*> \endverbatim 
//*> 
//*> \return CLANHE 
//*> \verbatim 
//*> 
//*>    CLANHE = ( max(abs(A(i,j))), NORM = 'M' or 'm' 
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
//*>          Specifies the value to be returned in CLANHE as described 
//*>          above. 
//*> \endverbatim 
//*> 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>          Specifies whether the upper or lower triangular part of the 
//*>          hermitian matrix A is to be referenced. 
//*>          = 'U':  Upper triangular part of A is referenced 
//*>          = 'L':  Lower triangular part of A is referenced 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix A.  N >= 0.  When N = 0, CLANHE is 
//*>          set to zero. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is COMPLEX array, dimension (LDA,N) 
//*>          The hermitian matrix A.  If UPLO = 'U', the leading n by n 
//*>          upper triangular part of A contains the upper triangular part 
//*>          of the matrix A, and the strictly lower triangular part of A 
//*>          is not referenced.  If UPLO = 'L', the leading n by n lower 
//*>          triangular part of A contains the lower triangular part of 
//*>          the matrix A, and the strictly upper triangular part of A is 
//*>          not referenced. Note that the imaginary parts of the diagonal 
//*>          elements need not be set and are assumed to be zero. 
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
//*>          WORK is REAL array, dimension (MAX(1,LWORK)), 
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
//*> \ingroup complexHEauxiliary 
//* 
//*  ===================================================================== 

	 
	public static Single _9g7ym2dg(FString _gq71rsgu, FString _9wyre9zc, ref Int32 _dxpq0xkr, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, Single* _apig8meb)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)16 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Single _9g7ym2dg = default;
Single _kxg5drh2 =  1f;
Single _d0547bi2 =  0f;
Int32 _b5p6od9s =  default;
Int32 _znpjgsef =  default;
Single _8lucc4fb =  default;
Single _6j9l5fwy =  default;
Single _lwoxlbje =  default;
Single* _8l4yph2p =  (Single*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Single) * ((int)2);
Single* _70n56i0m =  (Single*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Single) * ((int)2);
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
					System.Int32 __81fgg2dlsvn3844 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3844 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3844;
					for (__81fgg2count3844 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3844 + __81fgg2step3844) / __81fgg2step3844)), _znpjgsef = __81fgg2dlsvn3844; __81fgg2count3844 != 0; __81fgg2count3844--, _znpjgsef += (__81fgg2step3844)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn3845 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3845 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3845;
							for (__81fgg2count3845 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn3845 + __81fgg2step3845) / __81fgg2step3845)), _b5p6od9s = __81fgg2dlsvn3845; __81fgg2count3845 != 0; __81fgg2count3845--, _b5p6od9s += (__81fgg2step3845)) {

							{
								
								_6j9l5fwy = ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) );
								if ((_lwoxlbje < _6j9l5fwy) | _lilv8egi(ref _6j9l5fwy ))
								_lwoxlbje = _6j9l5fwy;
Mark10:;
								// continue
							}
														}						}
						_6j9l5fwy = ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) );
						if ((_lwoxlbje < _6j9l5fwy) | _lilv8egi(ref _6j9l5fwy ))
						_lwoxlbje = _6j9l5fwy;
Mark20:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn3846 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3846 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3846;
					for (__81fgg2count3846 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3846 + __81fgg2step3846) / __81fgg2step3846)), _znpjgsef = __81fgg2dlsvn3846; __81fgg2count3846 != 0; __81fgg2count3846--, _znpjgsef += (__81fgg2step3846)) {

					{
						
						_6j9l5fwy = ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) );
						if ((_lwoxlbje < _6j9l5fwy) | _lilv8egi(ref _6j9l5fwy ))
						_lwoxlbje = _6j9l5fwy;
						{
							System.Int32 __81fgg2dlsvn3847 = (System.Int32)((_znpjgsef + (int)1));
							const System.Int32 __81fgg2step3847 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3847;
							for (__81fgg2count3847 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3847 + __81fgg2step3847) / __81fgg2step3847)), _b5p6od9s = __81fgg2dlsvn3847; __81fgg2count3847 != 0; __81fgg2count3847--, _b5p6od9s += (__81fgg2step3847)) {

							{
								
								_6j9l5fwy = ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) );
								if ((_lwoxlbje < _6j9l5fwy) | _lilv8egi(ref _6j9l5fwy ))
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
			//*        Find normI(A) ( = norm1(A), since A is hermitian). 
			//* 
			
			_lwoxlbje = _d0547bi2;
			if (_w8y2rzgy(_9wyre9zc ,"U" ))
			{
				
				{
					System.Int32 __81fgg2dlsvn3848 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3848 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3848;
					for (__81fgg2count3848 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3848 + __81fgg2step3848) / __81fgg2step3848)), _znpjgsef = __81fgg2dlsvn3848; __81fgg2count3848 != 0; __81fgg2count3848--, _znpjgsef += (__81fgg2step3848)) {

					{
						
						_6j9l5fwy = _d0547bi2;
						{
							System.Int32 __81fgg2dlsvn3849 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3849 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3849;
							for (__81fgg2count3849 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn3849 + __81fgg2step3849) / __81fgg2step3849)), _b5p6od9s = __81fgg2dlsvn3849; __81fgg2count3849 != 0; __81fgg2count3849--, _b5p6od9s += (__81fgg2step3849)) {

							{
								
								_8lucc4fb = ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) );
								_6j9l5fwy = (_6j9l5fwy + _8lucc4fb);
								*(_apig8meb+(_b5p6od9s - 1)) = (*(_apig8meb+(_b5p6od9s - 1)) + _8lucc4fb);
Mark50:;
								// continue
							}
														}						}
						*(_apig8meb+(_znpjgsef - 1)) = (_6j9l5fwy + ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) ));
Mark60:;
						// continue
					}
										}				}
				{
					System.Int32 __81fgg2dlsvn3850 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3850 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3850;
					for (__81fgg2count3850 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3850 + __81fgg2step3850) / __81fgg2step3850)), _b5p6od9s = __81fgg2dlsvn3850; __81fgg2count3850 != 0; __81fgg2count3850--, _b5p6od9s += (__81fgg2step3850)) {

					{
						
						_6j9l5fwy = *(_apig8meb+(_b5p6od9s - 1));
						if ((_lwoxlbje < _6j9l5fwy) | _lilv8egi(ref _6j9l5fwy ))
						_lwoxlbje = _6j9l5fwy;
Mark70:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn3851 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3851 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3851;
					for (__81fgg2count3851 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3851 + __81fgg2step3851) / __81fgg2step3851)), _b5p6od9s = __81fgg2dlsvn3851; __81fgg2count3851 != 0; __81fgg2count3851--, _b5p6od9s += (__81fgg2step3851)) {

					{
						
						*(_apig8meb+(_b5p6od9s - 1)) = _d0547bi2;
Mark80:;
						// continue
					}
										}				}
				{
					System.Int32 __81fgg2dlsvn3852 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3852 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3852;
					for (__81fgg2count3852 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3852 + __81fgg2step3852) / __81fgg2step3852)), _znpjgsef = __81fgg2dlsvn3852; __81fgg2count3852 != 0; __81fgg2count3852--, _znpjgsef += (__81fgg2step3852)) {

					{
						
						_6j9l5fwy = (*(_apig8meb+(_znpjgsef - 1)) + ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) ));
						{
							System.Int32 __81fgg2dlsvn3853 = (System.Int32)((_znpjgsef + (int)1));
							const System.Int32 __81fgg2step3853 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3853;
							for (__81fgg2count3853 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3853 + __81fgg2step3853) / __81fgg2step3853)), _b5p6od9s = __81fgg2dlsvn3853; __81fgg2count3853 != 0; __81fgg2count3853--, _b5p6od9s += (__81fgg2step3853)) {

							{
								
								_8lucc4fb = ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) );
								_6j9l5fwy = (_6j9l5fwy + _8lucc4fb);
								*(_apig8meb+(_b5p6od9s - 1)) = (*(_apig8meb+(_b5p6od9s - 1)) + _8lucc4fb);
Mark90:;
								// continue
							}
														}						}
						if ((_lwoxlbje < _6j9l5fwy) | _lilv8egi(ref _6j9l5fwy ))
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
					System.Int32 __81fgg2dlsvn3854 = (System.Int32)((int)2);
					const System.Int32 __81fgg2step3854 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3854;
					for (__81fgg2count3854 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3854 + __81fgg2step3854) / __81fgg2step3854)), _znpjgsef = __81fgg2dlsvn3854; __81fgg2count3854 != 0; __81fgg2count3854--, _znpjgsef += (__81fgg2step3854)) {

					{
						
						*(_70n56i0m+((int)1 - 1)) = _d0547bi2;
						*(_70n56i0m+((int)2 - 1)) = _kxg5drh2;
						_bz1hd2n9(ref Unsafe.AsRef(_znpjgsef - (int)1) ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_70n56i0m+((int)1 - 1))) ,ref Unsafe.AsRef(*(_70n56i0m+((int)2 - 1))) );
						_4hcafcgj(_8l4yph2p ,_70n56i0m );
Mark110:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn3855 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3855 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3855;
					for (__81fgg2count3855 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn3855 + __81fgg2step3855) / __81fgg2step3855)), _znpjgsef = __81fgg2dlsvn3855; __81fgg2count3855 != 0; __81fgg2count3855--, _znpjgsef += (__81fgg2step3855)) {

					{
						
						*(_70n56i0m+((int)1 - 1)) = _d0547bi2;
						*(_70n56i0m+((int)2 - 1)) = _kxg5drh2;
						_bz1hd2n9(ref Unsafe.AsRef(_dxpq0xkr - _znpjgsef) ,(_vxfgpup9+(_znpjgsef + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_70n56i0m+((int)1 - 1))) ,ref Unsafe.AsRef(*(_70n56i0m+((int)2 - 1))) );
						_4hcafcgj(_8l4yph2p ,_70n56i0m );
Mark120:;
						// continue
					}
										}				}
			}
			
			*(_8l4yph2p+((int)2 - 1)) = ((int)2 * *(_8l4yph2p+((int)2 - 1)));//* 
			//*        Sum diagonal 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn3856 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step3856 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3856;
				for (__81fgg2count3856 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3856 + __81fgg2step3856) / __81fgg2step3856)), _b5p6od9s = __81fgg2dlsvn3856; __81fgg2count3856 != 0; __81fgg2count3856--, _b5p6od9s += (__81fgg2step3856)) {

				{
					
					if (ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) != _d0547bi2)
					{
						
						_8lucc4fb = ILNumerics.F2NET.Intrinsics.ABS(ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) );
						if (*(_8l4yph2p+((int)1 - 1)) < _8lucc4fb)
						{
							
							*(_8l4yph2p+((int)2 - 1)) = (_kxg5drh2 + (*(_8l4yph2p+((int)2 - 1)) * __POW2((*(_8l4yph2p+((int)1 - 1)) / _8lucc4fb))));
							*(_8l4yph2p+((int)1 - 1)) = _8lucc4fb;
						}
						else
						{
							
							*(_8l4yph2p+((int)2 - 1)) = (*(_8l4yph2p+((int)2 - 1)) + __POW2((_8lucc4fb / *(_8l4yph2p+((int)1 - 1)))));
						}
						
					}
					
Mark130:;
					// continue
				}
								}			}
			_lwoxlbje = (*(_8l4yph2p+((int)1 - 1)) * ILNumerics.F2NET.Intrinsics.SQRT(*(_8l4yph2p+((int)2 - 1)) ));
		}
		//* 
		
		_9g7ym2dg = _lwoxlbje;
		return _9g7ym2dg;//* 
		//*     End of CLANHE 
		//* 
		
	}
	
	return _9g7ym2dg;
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
