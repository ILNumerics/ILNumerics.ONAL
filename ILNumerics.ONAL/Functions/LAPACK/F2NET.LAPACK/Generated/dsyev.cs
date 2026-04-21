// MIT License
// 
// Copyright (c) 2026 ILNumerics GmbH
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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
//*> \brief <b> DSYEV computes the eigenvalues and, optionally, the left and/or right eigenvectors for SY matrices</b> 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DSYEV + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dsyev.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dsyev.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dsyev.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DSYEV( JOBZ, UPLO, N, A, LDA, W, WORK, LWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          JOBZ, UPLO 
//*       INTEGER            INFO, LDA, LWORK, N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   A( LDA, * ), W( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DSYEV computes all eigenvalues and, optionally, eigenvectors of a 
//*> real symmetric matrix A. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] JOBZ 
//*> \verbatim 
//*>          JOBZ is CHARACTER*1 
//*>          = 'N':  Compute eigenvalues only; 
//*>          = 'V':  Compute eigenvalues and eigenvectors. 
//*> \endverbatim 
//*> 
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
//*>          A is DOUBLE PRECISION array, dimension (LDA, N) 
//*>          On entry, the symmetric matrix A.  If UPLO = 'U', the 
//*>          leading N-by-N upper triangular part of A contains the 
//*>          upper triangular part of the matrix A.  If UPLO = 'L', 
//*>          the leading N-by-N lower triangular part of A contains 
//*>          the lower triangular part of the matrix A. 
//*>          On exit, if JOBZ = 'V', then if INFO = 0, A contains the 
//*>          orthonormal eigenvectors of the matrix A. 
//*>          If JOBZ = 'N', then on exit the lower triangle (if UPLO='L') 
//*>          or the upper triangle (if UPLO='U') of A, including the 
//*>          diagonal, is destroyed. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[out] W 
//*> \verbatim 
//*>          W is DOUBLE PRECISION array, dimension (N) 
//*>          If INFO = 0, the eigenvalues in ascending order. 
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
//*>          The length of the array WORK.  LWORK >= max(1,3*N-1). 
//*>          For optimal efficiency, LWORK >= (NB+2)*N, 
//*>          where NB is the blocksize for DSYTRD returned by ILAENV. 
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
//*>          > 0:  if INFO = i, the algorithm failed to converge; i 
//*>                off-diagonal elements of an intermediate tridiagonal 
//*>                form did not converge to zero. 
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
//*> \ingroup doubleSYeigen 
//* 
//*  ===================================================================== 

	 
	public static void _dako2gco(FString _w6igfk2h, FString _9wyre9zc, ref Int32 _dxpq0xkr, Double* _vxfgpup9, ref Int32 _ocv8fk5c, Double* _z1ioc3c8, Double* _apig8meb, ref Int32 _6fnxzlyp, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Boolean _58l4so5d =  default;
Boolean _lhlgm7z5 =  default;
Boolean _189gzykk =  default;
Int32 _itfnbz60 =  default;
Int32 _gmv1yrg4 =  default;
Int32 _n8o48xp4 =  default;
Int32 _pyea3qip =  default;
Int32 _cu6tg1rd =  default;
Int32 _g5graale =  default;
Int32 _cr029lri =  default;
Int32 _e4ueamrn =  default;
Int32 _f7059815 =  default;
Double _j6vjow1g =  default;
Double _av7j8yda =  default;
Double _p1iqarg6 =  default;
Double _o8rgmibn =  default;
Double _sg2xsi4l =  default;
Double _h75qnr7l =  default;
Double _91a1vq5f =  default;
Double _bogm0gwy =  default;
string fLanavab = default;
#endregion  variable declarations
_w6igfk2h = _w6igfk2h.Convert(1);
_9wyre9zc = _9wyre9zc.Convert(1);

	{
		//* 
		//*  -- LAPACK driver routine (version 3.7.0) -- 
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
		
		_189gzykk = _w8y2rzgy(_w6igfk2h ,"V" );
		_58l4so5d = _w8y2rzgy(_9wyre9zc ,"L" );
		_lhlgm7z5 = (_6fnxzlyp == (int)-1);//* 
		
		_gro5yvfo = (int)0;
		if (!((_189gzykk | _w8y2rzgy(_w6igfk2h ,"N" ))))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (!((_58l4so5d | _w8y2rzgy(_9wyre9zc ,"U" ))))
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)-5;
		}
		//* 
		
		if (_gro5yvfo == (int)0)
		{
			
			_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"DSYTRD" ,_9wyre9zc ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) );
			_e4ueamrn = ILNumerics.F2NET.Intrinsics.MAX((int)1 ,(_f7059815 + (int)2) * _dxpq0xkr );
			*(_apig8meb+((int)1 - 1)) = DBLE(_e4ueamrn);//* 
			
			if ((_6fnxzlyp < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,((int)3 * _dxpq0xkr) - (int)1 )) & (!(_lhlgm7z5)))
			_gro5yvfo = (int)-8;
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("DSYEV " ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
			
			return;
		}
		//* 
		
		if (_dxpq0xkr == (int)1)
		{
			
			*(_z1ioc3c8+((int)1 - 1)) = *(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c));
			*(_apig8meb+((int)1 - 1)) = DBLE((int)2);
			if (_189gzykk)
			*(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;
			return;
		}
		//* 
		//*     Get machine constants. 
		//* 
		
		_h75qnr7l = _f43eg0w0("Safe minimum" );
		_p1iqarg6 = _f43eg0w0("Precision" );
		_bogm0gwy = (_h75qnr7l / _p1iqarg6);
		_av7j8yda = (_kxg5drh2 / _bogm0gwy);
		_sg2xsi4l = ILNumerics.F2NET.Intrinsics.SQRT(_bogm0gwy );
		_o8rgmibn = ILNumerics.F2NET.Intrinsics.SQRT(_av7j8yda );//* 
		//*     Scale matrix to allowable range, if necessary. 
		//* 
		
		_j6vjow1g = _8wo3jyo5("M" ,_9wyre9zc ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_apig8meb );
		_g5graale = (int)0;
		if ((_j6vjow1g > _d0547bi2) & (_j6vjow1g < _sg2xsi4l))
		{
			
			_g5graale = (int)1;
			_91a1vq5f = (_sg2xsi4l / _j6vjow1g);
		}
		else
		if (_j6vjow1g > _o8rgmibn)
		{
			
			_g5graale = (int)1;
			_91a1vq5f = (_o8rgmibn / _j6vjow1g);
		}
		
		if (_g5graale == (int)1)
		_2xvktk5a(_9wyre9zc ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef(_kxg5drh2) ,ref _91a1vq5f ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,ref _gro5yvfo );//* 
		//*     Call DSYTRD to reduce symmetric matrix to tridiagonal form. 
		//* 
		
		_n8o48xp4 = (int)1;
		_pyea3qip = (_n8o48xp4 + _dxpq0xkr);
		_cu6tg1rd = (_pyea3qip + _dxpq0xkr);
		_cr029lri = ((_6fnxzlyp - _cu6tg1rd) + (int)1);
		_tyexjdu4(_9wyre9zc ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_z1ioc3c8 ,(_apig8meb+(_n8o48xp4 - 1)),(_apig8meb+(_pyea3qip - 1)),(_apig8meb+(_cu6tg1rd - 1)),ref _cr029lri ,ref _itfnbz60 );//* 
		//*     For eigenvalues only, call DSTERF.  For eigenvectors, first call 
		//*     DORGTR to generate the orthogonal matrix, then call DSTEQR. 
		//* 
		
		if (!(_189gzykk))
		{
			
			_0tyujlyc(ref _dxpq0xkr ,_z1ioc3c8 ,(_apig8meb+(_n8o48xp4 - 1)),ref _gro5yvfo );
		}
		else
		{
			
			_mb6xv71r(_9wyre9zc ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_pyea3qip - 1)),(_apig8meb+(_cu6tg1rd - 1)),ref _cr029lri ,ref _itfnbz60 );
			_1j010kqo(_w6igfk2h ,ref _dxpq0xkr ,_z1ioc3c8 ,(_apig8meb+(_n8o48xp4 - 1)),_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_pyea3qip - 1)),ref _gro5yvfo );
		}
		//* 
		//*     If matrix was scaled, then rescale eigenvalues appropriately. 
		//* 
		
		if (_g5graale == (int)1)
		{
			
			if (_gro5yvfo == (int)0)
			{
				
				_gmv1yrg4 = _dxpq0xkr;
			}
			else
			{
				
				_gmv1yrg4 = (_gro5yvfo - (int)1);
			}
			
			_f6jqcjk1(ref _gmv1yrg4 ,ref Unsafe.AsRef(_kxg5drh2 / _91a1vq5f) ,_z1ioc3c8 ,ref Unsafe.AsRef((int)1) );
		}
		//* 
		//*     Set WORK(1) to optimal workspace size. 
		//* 
		
		*(_apig8meb+((int)1 - 1)) = DBLE(_e4ueamrn);//* 
		
		return;//* 
		//*     End of DSYEV 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
