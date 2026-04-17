
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
//*> \brief <b> CHEEV computes the eigenvalues and, optionally, the left and/or right eigenvectors for HE matrices</b> 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CHEEV + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/cheev.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/cheev.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/cheev.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CHEEV( JOBZ, UPLO, N, A, LDA, W, WORK, LWORK, RWORK, 
//*                         INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          JOBZ, UPLO 
//*       INTEGER            INFO, LDA, LWORK, N 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               RWORK( * ), W( * ) 
//*       COMPLEX            A( LDA, * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CHEEV computes all eigenvalues and, optionally, eigenvectors of a 
//*> complex Hermitian matrix A. 
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
//*>          A is COMPLEX array, dimension (LDA, N) 
//*>          On entry, the Hermitian matrix A.  If UPLO = 'U', the 
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
//*>          W is REAL array, dimension (N) 
//*>          If INFO = 0, the eigenvalues in ascending order. 
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
//*>          The length of the array WORK.  LWORK >= max(1,2*N-1). 
//*>          For optimal efficiency, LWORK >= (NB+1)*N, 
//*>          where NB is the blocksize for CHETRD returned by ILAENV. 
//*> 
//*>          If LWORK = -1, then a workspace query is assumed; the routine 
//*>          only calculates the optimal size of the WORK array, returns 
//*>          this value as the first entry of the WORK array, and no error 
//*>          message related to LWORK is issued by XERBLA. 
//*> \endverbatim 
//*> 
//*> \param[out] RWORK 
//*> \verbatim 
//*>          RWORK is REAL array, dimension (max(1, 3*N-2)) 
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
//*> \ingroup complexHEeigen 
//* 
//*  ===================================================================== 

	 
	public static void _9w9zdxa8(FString _w6igfk2h, FString _9wyre9zc, ref Int32 _dxpq0xkr, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, Single* _z1ioc3c8, fcomplex* _apig8meb, ref Int32 _6fnxzlyp, Single* _dqanbbw3, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
fcomplex _40vhxf9f =   new fcomplex(1f,0f);
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
Single _j6vjow1g =  default;
Single _av7j8yda =  default;
Single _p1iqarg6 =  default;
Single _o8rgmibn =  default;
Single _sg2xsi4l =  default;
Single _h75qnr7l =  default;
Single _91a1vq5f =  default;
Single _bogm0gwy =  default;
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
			
			_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"CHETRD" ,_9wyre9zc ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) );
			_e4ueamrn = ILNumerics.F2NET.Intrinsics.MAX((int)1 ,(_f7059815 + (int)1) * _dxpq0xkr );
			*(_apig8meb+((int)1 - 1)) = CMPLX(_e4ueamrn);//* 
			
			if ((_6fnxzlyp < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,((int)2 * _dxpq0xkr) - (int)1 )) & (!(_lhlgm7z5)))
			_gro5yvfo = (int)-8;
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("CHEEV " ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
			
			*(_z1ioc3c8+((int)1 - 1)) = REAL(*(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)));
			*(_apig8meb+((int)1 - 1)) = CMPLX((int)1);
			if (_189gzykk)
			*(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) = _40vhxf9f;
			return;
		}
		//* 
		//*     Get machine constants. 
		//* 
		
		_h75qnr7l = _d5tu038y("Safe minimum" );
		_p1iqarg6 = _d5tu038y("Precision" );
		_bogm0gwy = (_h75qnr7l / _p1iqarg6);
		_av7j8yda = (_kxg5drh2 / _bogm0gwy);
		_sg2xsi4l = ILNumerics.F2NET.Intrinsics.SQRT(_bogm0gwy );
		_o8rgmibn = ILNumerics.F2NET.Intrinsics.SQRT(_av7j8yda );//* 
		//*     Scale matrix to allowable range, if necessary. 
		//* 
		
		_j6vjow1g = _9g7ym2dg("M" ,_9wyre9zc ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_dqanbbw3 );
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
		_0asigtd4(_9wyre9zc ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef(_kxg5drh2) ,ref _91a1vq5f ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,ref _gro5yvfo );//* 
		//*     Call CHETRD to reduce Hermitian matrix to tridiagonal form. 
		//* 
		
		_n8o48xp4 = (int)1;
		_pyea3qip = (int)1;
		_cu6tg1rd = (_pyea3qip + _dxpq0xkr);
		_cr029lri = ((_6fnxzlyp - _cu6tg1rd) + (int)1);
		_j742bzr9(_9wyre9zc ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_z1ioc3c8 ,(_dqanbbw3+(_n8o48xp4 - 1)),(_apig8meb+(_pyea3qip - 1)),(_apig8meb+(_cu6tg1rd - 1)),ref _cr029lri ,ref _itfnbz60 );//* 
		//*     For eigenvalues only, call SSTERF.  For eigenvectors, first call 
		//*     CUNGTR to generate the unitary matrix, then call CSTEQR. 
		//* 
		
		if (!(_189gzykk))
		{
			
			_5rm4mqbz(ref _dxpq0xkr ,_z1ioc3c8 ,(_dqanbbw3+(_n8o48xp4 - 1)),ref _gro5yvfo );
		}
		else
		{
			
			_p2ipbwy2(_9wyre9zc ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_pyea3qip - 1)),(_apig8meb+(_cu6tg1rd - 1)),ref _cr029lri ,ref _itfnbz60 );
			_cu6tg1rd = (_n8o48xp4 + _dxpq0xkr);
			_h4p1amig(_w6igfk2h ,ref _dxpq0xkr ,_z1ioc3c8 ,(_dqanbbw3+(_n8o48xp4 - 1)),_vxfgpup9 ,ref _ocv8fk5c ,(_dqanbbw3+(_cu6tg1rd - 1)),ref _gro5yvfo );
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
			
			_ct5qqrv7(ref _gmv1yrg4 ,ref Unsafe.AsRef(_kxg5drh2 / _91a1vq5f) ,_z1ioc3c8 ,ref Unsafe.AsRef((int)1) );
		}
		//* 
		//*     Set WORK(1) to optimal complex workspace size. 
		//* 
		
		*(_apig8meb+((int)1 - 1)) = CMPLX(_e4ueamrn);//* 
		
		return;//* 
		//*     End of CHEEV 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
