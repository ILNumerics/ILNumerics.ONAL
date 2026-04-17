
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
//*> \brief \b DLANST returns the value of the 1-norm, or the Frobenius norm, or the infinity norm, or the element of largest absolute value of a real symmetric tridiagonal matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLANST + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlanst.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlanst.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlanst.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       DOUBLE PRECISION FUNCTION DLANST( NORM, N, D, E ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          NORM 
//*       INTEGER            N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   D( * ), E( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLANST  returns the value of the one norm,  or the Frobenius norm, or 
//*> the  infinity norm,  or the  element of  largest absolute value  of a 
//*> real symmetric tridiagonal matrix A. 
//*> \endverbatim 
//*> 
//*> \return DLANST 
//*> \verbatim 
//*> 
//*>    DLANST = ( max(abs(A(i,j))), NORM = 'M' or 'm' 
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
//*>          Specifies the value to be returned in DLANST as described 
//*>          above. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix A.  N >= 0.  When N = 0, DLANST is 
//*>          set to zero. 
//*> \endverbatim 
//*> 
//*> \param[in] D 
//*> \verbatim 
//*>          D is DOUBLE PRECISION array, dimension (N) 
//*>          The diagonal elements of A. 
//*> \endverbatim 
//*> 
//*> \param[in] E 
//*> \verbatim 
//*>          E is DOUBLE PRECISION array, dimension (N-1) 
//*>          The (n-1) sub-diagonal or super-diagonal elements of A. 
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
//*> \ingroup OTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static Double _j0e1628u(FString _gq71rsgu, ref Int32 _dxpq0xkr, Double* _plfm7z8g, Double* _864fslqq)
	{
#region variable declarations
Double _j0e1628u = default;
Double _kxg5drh2 =  1d;
Double _d0547bi2 =  0d;
Int32 _b5p6od9s =  default;
Double _b8rxgs6o =  default;
Double _1m44vtuk =  default;
Double _6j9l5fwy =  default;
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
		
		if (_dxpq0xkr <= (int)0)
		{
			
			_b8rxgs6o = _d0547bi2;
		}
		else
		if (_w8y2rzgy(_gq71rsgu ,"M" ))
		{
			//* 
			//*        Find max(abs(A(i,j))). 
			//* 
			
			_b8rxgs6o = ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_dxpq0xkr - 1)) );
			{
				System.Int32 __81fgg2dlsvn176 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step176 = (System.Int32)((int)1);
				System.Int32 __81fgg2count176;
				for (__81fgg2count176 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn176 + __81fgg2step176) / __81fgg2step176)), _b5p6od9s = __81fgg2dlsvn176; __81fgg2count176 != 0; __81fgg2count176--, _b5p6od9s += (__81fgg2step176)) {

				{
					
					_6j9l5fwy = ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_b5p6od9s - 1)) );
					if ((_b8rxgs6o < _6j9l5fwy) | _fk98jwhi(ref _6j9l5fwy ))
					_b8rxgs6o = _6j9l5fwy;
					_6j9l5fwy = ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_b5p6od9s - 1)) );
					if ((_b8rxgs6o < _6j9l5fwy) | _fk98jwhi(ref _6j9l5fwy ))
					_b8rxgs6o = _6j9l5fwy;
Mark10:;
					// continue
				}
								}			}
		}
		else
		if ((_w8y2rzgy(_gq71rsgu ,"O" ) | (_gq71rsgu == "1")) | _w8y2rzgy(_gq71rsgu ,"I" ))
		{
			//* 
			//*        Find norm1(A). 
			//* 
			
			if (_dxpq0xkr == (int)1)
			{
				
				_b8rxgs6o = ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+((int)1 - 1)) );
			}
			else
			{
				
				_b8rxgs6o = (ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+((int)1 - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+((int)1 - 1)) ));
				_6j9l5fwy = (ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_dxpq0xkr - (int)1 - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_dxpq0xkr - 1)) ));
				if ((_b8rxgs6o < _6j9l5fwy) | _fk98jwhi(ref _6j9l5fwy ))
				_b8rxgs6o = _6j9l5fwy;
				{
					System.Int32 __81fgg2dlsvn177 = (System.Int32)((int)2);
					const System.Int32 __81fgg2step177 = (System.Int32)((int)1);
					System.Int32 __81fgg2count177;
					for (__81fgg2count177 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn177 + __81fgg2step177) / __81fgg2step177)), _b5p6od9s = __81fgg2dlsvn177; __81fgg2count177 != 0; __81fgg2count177--, _b5p6od9s += (__81fgg2step177)) {

					{
						
						_6j9l5fwy = ((ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_b5p6od9s - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_b5p6od9s - 1)) )) + ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_b5p6od9s - (int)1 - 1)) ));
						if ((_b8rxgs6o < _6j9l5fwy) | _fk98jwhi(ref _6j9l5fwy ))
						_b8rxgs6o = _6j9l5fwy;
Mark20:;
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
			//* 
			
			_1m44vtuk = _d0547bi2;
			_6j9l5fwy = _kxg5drh2;
			if (_dxpq0xkr > (int)1)
			{
				
				_g54gbghr(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,_864fslqq ,ref Unsafe.AsRef((int)1) ,ref _1m44vtuk ,ref _6j9l5fwy );
				_6j9l5fwy = ((int)2 * _6j9l5fwy);
			}
			
			_g54gbghr(ref _dxpq0xkr ,_plfm7z8g ,ref Unsafe.AsRef((int)1) ,ref _1m44vtuk ,ref _6j9l5fwy );
			_b8rxgs6o = (_1m44vtuk * ILNumerics.F2NET.Intrinsics.SQRT(_6j9l5fwy ));
		}
		//* 
		
		_j0e1628u = _b8rxgs6o;
		return _j0e1628u;//* 
		//*     End of DLANST 
		//* 
		
	}
	
	return _j0e1628u;
	} // 177

} // end class 
} // end namespace
#endif
