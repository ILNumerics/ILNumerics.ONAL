
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
//*> \brief \b DLAEXC swaps adjacent diagonal blocks of a real upper quasi-triangular matrix in Schur canonical form, by an orthogonal similarity transformation. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLAEXC + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlaexc.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlaexc.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlaexc.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLAEXC( WANTQ, N, T, LDT, Q, LDQ, J1, N1, N2, WORK, 
//*                          INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       LOGICAL            WANTQ 
//*       INTEGER            INFO, J1, LDQ, LDT, N, N1, N2 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   Q( LDQ, * ), T( LDT, * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLAEXC swaps adjacent diagonal blocks T11 and T22 of order 1 or 2 in 
//*> an upper quasi-triangular matrix T by an orthogonal similarity 
//*> transformation. 
//*> 
//*> T must be in Schur canonical form, that is, block upper triangular 
//*> with 1-by-1 and 2-by-2 diagonal blocks; each 2-by-2 diagonal block 
//*> has its diagonal elemnts equal and its off-diagonal elements of 
//*> opposite sign. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] WANTQ 
//*> \verbatim 
//*>          WANTQ is LOGICAL 
//*>          = .TRUE. : accumulate the transformation in the matrix Q; 
//*>          = .FALSE.: do not accumulate the transformation. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix T. N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] T 
//*> \verbatim 
//*>          T is DOUBLE PRECISION array, dimension (LDT,N) 
//*>          On entry, the upper quasi-triangular matrix T, in Schur 
//*>          canonical form. 
//*>          On exit, the updated matrix T, again in Schur canonical form. 
//*> \endverbatim 
//*> 
//*> \param[in] LDT 
//*> \verbatim 
//*>          LDT is INTEGER 
//*>          The leading dimension of the array T. LDT >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[in,out] Q 
//*> \verbatim 
//*>          Q is DOUBLE PRECISION array, dimension (LDQ,N) 
//*>          On entry, if WANTQ is .TRUE., the orthogonal matrix Q. 
//*>          On exit, if WANTQ is .TRUE., the updated matrix Q. 
//*>          If WANTQ is .FALSE., Q is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] LDQ 
//*> \verbatim 
//*>          LDQ is INTEGER 
//*>          The leading dimension of the array Q. 
//*>          LDQ >= 1; and if WANTQ is .TRUE., LDQ >= N. 
//*> \endverbatim 
//*> 
//*> \param[in] J1 
//*> \verbatim 
//*>          J1 is INTEGER 
//*>          The index of the first row of the first block T11. 
//*> \endverbatim 
//*> 
//*> \param[in] N1 
//*> \verbatim 
//*>          N1 is INTEGER 
//*>          The order of the first block T11. N1 = 0, 1 or 2. 
//*> \endverbatim 
//*> 
//*> \param[in] N2 
//*> \verbatim 
//*>          N2 is INTEGER 
//*>          The order of the second block T22. N2 = 0, 1 or 2. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is DOUBLE PRECISION array, dimension (N) 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0: successful exit 
//*>          = 1: the transformed matrix T would be too far from Schur 
//*>               form; the blocks are not swapped and T and Q are 
//*>               unchanged. 
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
//*> \ingroup doubleOTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _bdf7n42q(ref Boolean _gh3pzgwj, ref Int32 _dxpq0xkr, Double* _2ivtt43r, ref Int32 _w8yhbr2r, Double* _atumjwo3, ref Int32 _u3fpniqy, ref Int32 _dk3nh7xl, ref Int32 _4o1bt8b1, ref Int32 _tixk7d1h, Double* _apig8meb, ref Int32 _gro5yvfo)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)232 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Double _76g572q1 =  10d;
Int32 _ud2c6e0n =  (int)4;
Int32 _eeyyzhrs =  (int)2;
Int32 _bhsiylw4 =  default;
Int32 _psodi8to =  default;
Int32 _mj5xo5tg =  default;
Int32 _h5f9ahvx =  default;
Int32 _umlkckdg =  default;
Int32 _rwm6akyl =  default;
Double _82tpdhyl =  default;
Double _9minh64v =  default;
Double _p1iqarg6 =  default;
Double _1m44vtuk =  default;
Double _bogm0gwy =  default;
Double _8tmd0ner =  default;
Double _bhnvwhs6 =  default;
Double _n2r5pyie =  default;
Double _0pa7b97y =  default;
Double _0446f4de =  default;
Double _tvdu6mfs =  default;
Double _xs3ggrxu =  default;
Double _1ajfmh55 =  default;
Double _j0ty6ytl =  default;
Double _y2cau71e =  default;
Double _19jvoy1k =  default;
Double _ddw256ub =  default;
Double _jzbad79b =  default;
Double _ziu6urj2 =  default;
Double* _plfm7z8g =  (Double*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Double) * ((int)4)*((int)4);
Double* _7u55mqkq =  (Double*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Double) * ((int)3);
Double* _x9gwwit3 =  (Double*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Double) * ((int)3);
Double* _s6mwvivs =  (Double*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Double) * ((int)3);
Double* _ta7zuy9k =  (Double*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Double) * ((int)2)*((int)2);
string fLanavab = default;
#endregion  variable declarations

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
		
		_gro5yvfo = (int)0;//* 
		//*     Quick return if possible 
		//* 
		
		if (((_dxpq0xkr == (int)0) | (_4o1bt8b1 == (int)0)) | (_tixk7d1h == (int)0))
		return;
		if ((_dk3nh7xl + _4o1bt8b1) > _dxpq0xkr)
		return;//* 
		
		_psodi8to = (_dk3nh7xl + (int)1);
		_mj5xo5tg = (_dk3nh7xl + (int)2);
		_h5f9ahvx = (_dk3nh7xl + (int)3);//* 
		
		if ((_4o1bt8b1 == (int)1) & (_tixk7d1h == (int)1))
		{
			//* 
			//*        Swap two 1-by-1 blocks. 
			//* 
			
			_bhnvwhs6 = *(_2ivtt43r+(_dk3nh7xl - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r));
			_n2r5pyie = *(_2ivtt43r+(_psodi8to - 1) + (_psodi8to - 1) * 1 * (_w8yhbr2r));//* 
			//*        Determine the transformation to perform the interchange. 
			//* 
			
			_uasfzoa5(ref Unsafe.AsRef(*(_2ivtt43r+(_dk3nh7xl - 1) + (_psodi8to - 1) * 1 * (_w8yhbr2r))) ,ref Unsafe.AsRef(_n2r5pyie - _bhnvwhs6) ,ref _82tpdhyl ,ref _8tmd0ner ,ref _1ajfmh55 );//* 
			//*        Apply transformation to the matrix T. 
			//* 
			
			if (_mj5xo5tg <= _dxpq0xkr)
			_2197fa5i(ref Unsafe.AsRef((_dxpq0xkr - _dk3nh7xl) - (int)1) ,(_2ivtt43r+(_dk3nh7xl - 1) + (_mj5xo5tg - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,(_2ivtt43r+(_psodi8to - 1) + (_mj5xo5tg - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,ref _82tpdhyl ,ref _8tmd0ner );
			_2197fa5i(ref Unsafe.AsRef(_dk3nh7xl - (int)1) ,(_2ivtt43r+((int)1 - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_2ivtt43r+((int)1 - 1) + (_psodi8to - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,ref _82tpdhyl ,ref _8tmd0ner );//* 
			
			*(_2ivtt43r+(_dk3nh7xl - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)) = _n2r5pyie;
			*(_2ivtt43r+(_psodi8to - 1) + (_psodi8to - 1) * 1 * (_w8yhbr2r)) = _bhnvwhs6;//* 
			
			if (_gh3pzgwj)
			{
				//* 
				//*           Accumulate transformation in the matrix Q. 
				//* 
				
				_2197fa5i(ref _dxpq0xkr ,(_atumjwo3+((int)1 - 1) + (_dk3nh7xl - 1) * 1 * (_u3fpniqy)),ref Unsafe.AsRef((int)1) ,(_atumjwo3+((int)1 - 1) + (_psodi8to - 1) * 1 * (_u3fpniqy)),ref Unsafe.AsRef((int)1) ,ref _82tpdhyl ,ref _8tmd0ner );
			}
			//* 
			
		}
		else
		{
			//* 
			//*        Swapping involves at least one 2-by-2 block. 
			//* 
			//*        Copy the diagonal block of order N1+N2 to the local array D 
			//*        and compute its norm. 
			//* 
			
			_rwm6akyl = (_4o1bt8b1 + _tixk7d1h);
			_hhtvj1kb("Full" ,ref _rwm6akyl ,ref _rwm6akyl ,(_2ivtt43r+(_dk3nh7xl - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,_plfm7z8g ,ref Unsafe.AsRef(_ud2c6e0n) );
			_9minh64v = _oui78ayq("Max" ,ref _rwm6akyl ,ref _rwm6akyl ,_plfm7z8g ,ref Unsafe.AsRef(_ud2c6e0n) ,_apig8meb );//* 
			//*        Compute machine-dependent threshold for test for accepting 
			//*        swap. 
			//* 
			
			_p1iqarg6 = _f43eg0w0("P" );
			_bogm0gwy = (_f43eg0w0("S" ) / _p1iqarg6);
			_j0ty6ytl = ILNumerics.F2NET.Intrinsics.MAX((_76g572q1 * _p1iqarg6) * _9minh64v ,_bogm0gwy );//* 
			//*        Solve T11*X - X*T22 = scale*T12 for X. 
			//* 
			
			_x5gczlj4(ref Unsafe.AsRef(false) ,ref Unsafe.AsRef(false) ,ref Unsafe.AsRef((int)-1) ,ref _4o1bt8b1 ,ref _tixk7d1h ,_plfm7z8g ,ref Unsafe.AsRef(_ud2c6e0n) ,(_plfm7z8g+(_4o1bt8b1 + (int)1 - 1) + (_4o1bt8b1 + (int)1 - 1) * 1 * ((int)4)),ref Unsafe.AsRef(_ud2c6e0n) ,(_plfm7z8g+((int)1 - 1) + (_4o1bt8b1 + (int)1 - 1) * 1 * ((int)4)),ref Unsafe.AsRef(_ud2c6e0n) ,ref _1m44vtuk ,_ta7zuy9k ,ref Unsafe.AsRef(_eeyyzhrs) ,ref _ziu6urj2 ,ref _bhsiylw4 );//* 
			//*        Swap the adjacent diagonal blocks. 
			//* 
			
			_umlkckdg = (((_4o1bt8b1 + _4o1bt8b1) + _tixk7d1h) - (int)3);
			switch (_umlkckdg) {
								case 1:
				goto Mark10;
				case 2:
				goto Mark20;
				case 3:
				goto Mark30;
				default:
				break;
			}
//* 
			
Mark10:;
			// continue//* 
			//*        N1 = 1, N2 = 2: generate elementary reflector H so that: 
			//* 
			//*        ( scale, X11, X12 ) H = ( 0, 0, * ) 
			//* 
			
			*(_7u55mqkq+((int)1 - 1)) = _1m44vtuk;
			*(_7u55mqkq+((int)2 - 1)) = *(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2));
			*(_7u55mqkq+((int)3 - 1)) = *(_ta7zuy9k+((int)1 - 1) + ((int)2 - 1) * 1 * ((int)2));
			_a51k3mk0(ref Unsafe.AsRef((int)3) ,ref Unsafe.AsRef(*(_7u55mqkq+((int)3 - 1))) ,_7u55mqkq ,ref Unsafe.AsRef((int)1) ,ref _0446f4de );
			*(_7u55mqkq+((int)3 - 1)) = _kxg5drh2;
			_bhnvwhs6 = *(_2ivtt43r+(_dk3nh7xl - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r));//* 
			//*        Perform swap provisionally on diagonal block in D. 
			//* 
			
			_42r3zmw1("L" ,ref Unsafe.AsRef((int)3) ,ref Unsafe.AsRef((int)3) ,_7u55mqkq ,ref _0446f4de ,_plfm7z8g ,ref Unsafe.AsRef(_ud2c6e0n) ,_apig8meb );
			_42r3zmw1("R" ,ref Unsafe.AsRef((int)3) ,ref Unsafe.AsRef((int)3) ,_7u55mqkq ,ref _0446f4de ,_plfm7z8g ,ref Unsafe.AsRef(_ud2c6e0n) ,_apig8meb );//* 
			//*        Test whether to reject swap. 
			//* 
			
			if (ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+((int)3 - 1) + ((int)1 - 1) * 1 * ((int)4)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+((int)3 - 1) + ((int)2 - 1) * 1 * ((int)4)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+((int)3 - 1) + ((int)3 - 1) * 1 * ((int)4)) - _bhnvwhs6 ) ) > _j0ty6ytl)goto Mark50;//* 
			//*        Accept swap: apply transformation to the entire matrix T. 
			//* 
			
			_42r3zmw1("L" ,ref Unsafe.AsRef((int)3) ,ref Unsafe.AsRef((_dxpq0xkr - _dk3nh7xl) + (int)1) ,_7u55mqkq ,ref _0446f4de ,(_2ivtt43r+(_dk3nh7xl - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,_apig8meb );
			_42r3zmw1("R" ,ref _psodi8to ,ref Unsafe.AsRef((int)3) ,_7u55mqkq ,ref _0446f4de ,(_2ivtt43r+((int)1 - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,_apig8meb );//* 
			
			*(_2ivtt43r+(_mj5xo5tg - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)) = _d0547bi2;
			*(_2ivtt43r+(_mj5xo5tg - 1) + (_psodi8to - 1) * 1 * (_w8yhbr2r)) = _d0547bi2;
			*(_2ivtt43r+(_mj5xo5tg - 1) + (_mj5xo5tg - 1) * 1 * (_w8yhbr2r)) = _bhnvwhs6;//* 
			
			if (_gh3pzgwj)
			{
				//* 
				//*           Accumulate transformation in the matrix Q. 
				//* 
				
				_42r3zmw1("R" ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)3) ,_7u55mqkq ,ref _0446f4de ,(_atumjwo3+((int)1 - 1) + (_dk3nh7xl - 1) * 1 * (_u3fpniqy)),ref _u3fpniqy ,_apig8meb );
			}
			goto Mark40;//* 
			
Mark20:;
			// continue//* 
			//*        N1 = 2, N2 = 1: generate elementary reflector H so that: 
			//* 
			//*        H (  -X11 ) = ( * ) 
			//*          (  -X21 ) = ( 0 ) 
			//*          ( scale ) = ( 0 ) 
			//* 
			
			*(_7u55mqkq+((int)1 - 1)) = (-(*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2))));
			*(_7u55mqkq+((int)2 - 1)) = (-(*(_ta7zuy9k+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2))));
			*(_7u55mqkq+((int)3 - 1)) = _1m44vtuk;
			_a51k3mk0(ref Unsafe.AsRef((int)3) ,ref Unsafe.AsRef(*(_7u55mqkq+((int)1 - 1))) ,(_7u55mqkq+((int)2 - 1)),ref Unsafe.AsRef((int)1) ,ref _0446f4de );
			*(_7u55mqkq+((int)1 - 1)) = _kxg5drh2;
			_0pa7b97y = *(_2ivtt43r+(_mj5xo5tg - 1) + (_mj5xo5tg - 1) * 1 * (_w8yhbr2r));//* 
			//*        Perform swap provisionally on diagonal block in D. 
			//* 
			
			_42r3zmw1("L" ,ref Unsafe.AsRef((int)3) ,ref Unsafe.AsRef((int)3) ,_7u55mqkq ,ref _0446f4de ,_plfm7z8g ,ref Unsafe.AsRef(_ud2c6e0n) ,_apig8meb );
			_42r3zmw1("R" ,ref Unsafe.AsRef((int)3) ,ref Unsafe.AsRef((int)3) ,_7u55mqkq ,ref _0446f4de ,_plfm7z8g ,ref Unsafe.AsRef(_ud2c6e0n) ,_apig8meb );//* 
			//*        Test whether to reject swap. 
			//* 
			
			if (ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)4)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+((int)3 - 1) + ((int)1 - 1) * 1 * ((int)4)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)4)) - _0pa7b97y ) ) > _j0ty6ytl)goto Mark50;//* 
			//*        Accept swap: apply transformation to the entire matrix T. 
			//* 
			
			_42r3zmw1("R" ,ref _mj5xo5tg ,ref Unsafe.AsRef((int)3) ,_7u55mqkq ,ref _0446f4de ,(_2ivtt43r+((int)1 - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,_apig8meb );
			_42r3zmw1("L" ,ref Unsafe.AsRef((int)3) ,ref Unsafe.AsRef(_dxpq0xkr - _dk3nh7xl) ,_7u55mqkq ,ref _0446f4de ,(_2ivtt43r+(_dk3nh7xl - 1) + (_psodi8to - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,_apig8meb );//* 
			
			*(_2ivtt43r+(_dk3nh7xl - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)) = _0pa7b97y;
			*(_2ivtt43r+(_psodi8to - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)) = _d0547bi2;
			*(_2ivtt43r+(_mj5xo5tg - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)) = _d0547bi2;//* 
			
			if (_gh3pzgwj)
			{
				//* 
				//*           Accumulate transformation in the matrix Q. 
				//* 
				
				_42r3zmw1("R" ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)3) ,_7u55mqkq ,ref _0446f4de ,(_atumjwo3+((int)1 - 1) + (_dk3nh7xl - 1) * 1 * (_u3fpniqy)),ref _u3fpniqy ,_apig8meb );
			}
			goto Mark40;//* 
			
Mark30:;
			// continue//* 
			//*        N1 = 2, N2 = 2: generate elementary reflectors H(1) and H(2) so 
			//*        that: 
			//* 
			//*        H(2) H(1) (  -X11  -X12 ) = (  *  * ) 
			//*                  (  -X21  -X22 )   (  0  * ) 
			//*                  ( scale    0  )   (  0  0 ) 
			//*                  (    0  scale )   (  0  0 ) 
			//* 
			
			*(_x9gwwit3+((int)1 - 1)) = (-(*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2))));
			*(_x9gwwit3+((int)2 - 1)) = (-(*(_ta7zuy9k+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2))));
			*(_x9gwwit3+((int)3 - 1)) = _1m44vtuk;
			_a51k3mk0(ref Unsafe.AsRef((int)3) ,ref Unsafe.AsRef(*(_x9gwwit3+((int)1 - 1))) ,(_x9gwwit3+((int)2 - 1)),ref Unsafe.AsRef((int)1) ,ref _tvdu6mfs );
			*(_x9gwwit3+((int)1 - 1)) = _kxg5drh2;//* 
			
			_1ajfmh55 = (-((_tvdu6mfs * (*(_ta7zuy9k+((int)1 - 1) + ((int)2 - 1) * 1 * ((int)2)) + (*(_x9gwwit3+((int)2 - 1)) * *(_ta7zuy9k+((int)2 - 1) + ((int)2 - 1) * 1 * ((int)2)))))));
			*(_s6mwvivs+((int)1 - 1)) = ((-((_1ajfmh55 * *(_x9gwwit3+((int)2 - 1))))) - *(_ta7zuy9k+((int)2 - 1) + ((int)2 - 1) * 1 * ((int)2)));
			*(_s6mwvivs+((int)2 - 1)) = (-((_1ajfmh55 * *(_x9gwwit3+((int)3 - 1)))));
			*(_s6mwvivs+((int)3 - 1)) = _1m44vtuk;
			_a51k3mk0(ref Unsafe.AsRef((int)3) ,ref Unsafe.AsRef(*(_s6mwvivs+((int)1 - 1))) ,(_s6mwvivs+((int)2 - 1)),ref Unsafe.AsRef((int)1) ,ref _xs3ggrxu );
			*(_s6mwvivs+((int)1 - 1)) = _kxg5drh2;//* 
			//*        Perform swap provisionally on diagonal block in D. 
			//* 
			
			_42r3zmw1("L" ,ref Unsafe.AsRef((int)3) ,ref Unsafe.AsRef((int)4) ,_x9gwwit3 ,ref _tvdu6mfs ,_plfm7z8g ,ref Unsafe.AsRef(_ud2c6e0n) ,_apig8meb );
			_42r3zmw1("R" ,ref Unsafe.AsRef((int)4) ,ref Unsafe.AsRef((int)3) ,_x9gwwit3 ,ref _tvdu6mfs ,_plfm7z8g ,ref Unsafe.AsRef(_ud2c6e0n) ,_apig8meb );
			_42r3zmw1("L" ,ref Unsafe.AsRef((int)3) ,ref Unsafe.AsRef((int)4) ,_s6mwvivs ,ref _xs3ggrxu ,(_plfm7z8g+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)4)),ref Unsafe.AsRef(_ud2c6e0n) ,_apig8meb );
			_42r3zmw1("R" ,ref Unsafe.AsRef((int)4) ,ref Unsafe.AsRef((int)3) ,_s6mwvivs ,ref _xs3ggrxu ,(_plfm7z8g+((int)1 - 1) + ((int)2 - 1) * 1 * ((int)4)),ref Unsafe.AsRef(_ud2c6e0n) ,_apig8meb );//* 
			//*        Test whether to reject swap. 
			//* 
			
			if (ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+((int)3 - 1) + ((int)1 - 1) * 1 * ((int)4)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+((int)3 - 1) + ((int)2 - 1) * 1 * ((int)4)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+((int)4 - 1) + ((int)1 - 1) * 1 * ((int)4)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+((int)4 - 1) + ((int)2 - 1) * 1 * ((int)4)) ) ) > _j0ty6ytl)goto Mark50;//* 
			//*        Accept swap: apply transformation to the entire matrix T. 
			//* 
			
			_42r3zmw1("L" ,ref Unsafe.AsRef((int)3) ,ref Unsafe.AsRef((_dxpq0xkr - _dk3nh7xl) + (int)1) ,_x9gwwit3 ,ref _tvdu6mfs ,(_2ivtt43r+(_dk3nh7xl - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,_apig8meb );
			_42r3zmw1("R" ,ref _h5f9ahvx ,ref Unsafe.AsRef((int)3) ,_x9gwwit3 ,ref _tvdu6mfs ,(_2ivtt43r+((int)1 - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,_apig8meb );
			_42r3zmw1("L" ,ref Unsafe.AsRef((int)3) ,ref Unsafe.AsRef((_dxpq0xkr - _dk3nh7xl) + (int)1) ,_s6mwvivs ,ref _xs3ggrxu ,(_2ivtt43r+(_psodi8to - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,_apig8meb );
			_42r3zmw1("R" ,ref _h5f9ahvx ,ref Unsafe.AsRef((int)3) ,_s6mwvivs ,ref _xs3ggrxu ,(_2ivtt43r+((int)1 - 1) + (_psodi8to - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,_apig8meb );//* 
			
			*(_2ivtt43r+(_mj5xo5tg - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)) = _d0547bi2;
			*(_2ivtt43r+(_mj5xo5tg - 1) + (_psodi8to - 1) * 1 * (_w8yhbr2r)) = _d0547bi2;
			*(_2ivtt43r+(_h5f9ahvx - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)) = _d0547bi2;
			*(_2ivtt43r+(_h5f9ahvx - 1) + (_psodi8to - 1) * 1 * (_w8yhbr2r)) = _d0547bi2;//* 
			
			if (_gh3pzgwj)
			{
				//* 
				//*           Accumulate transformation in the matrix Q. 
				//* 
				
				_42r3zmw1("R" ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)3) ,_x9gwwit3 ,ref _tvdu6mfs ,(_atumjwo3+((int)1 - 1) + (_dk3nh7xl - 1) * 1 * (_u3fpniqy)),ref _u3fpniqy ,_apig8meb );
				_42r3zmw1("R" ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)3) ,_s6mwvivs ,ref _xs3ggrxu ,(_atumjwo3+((int)1 - 1) + (_psodi8to - 1) * 1 * (_u3fpniqy)),ref _u3fpniqy ,_apig8meb );
			}
			//* 
			
Mark40:;
			// continue//* 
			
			if (_tixk7d1h == (int)2)
			{
				//* 
				//*           Standardize new 2-by-2 block T11 
				//* 
				
				_srab5ud9(ref Unsafe.AsRef(*(_2ivtt43r+(_dk3nh7xl - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r))) ,ref Unsafe.AsRef(*(_2ivtt43r+(_dk3nh7xl - 1) + (_psodi8to - 1) * 1 * (_w8yhbr2r))) ,ref Unsafe.AsRef(*(_2ivtt43r+(_psodi8to - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r))) ,ref Unsafe.AsRef(*(_2ivtt43r+(_psodi8to - 1) + (_psodi8to - 1) * 1 * (_w8yhbr2r))) ,ref _ddw256ub ,ref _y2cau71e ,ref _jzbad79b ,ref _19jvoy1k ,ref _82tpdhyl ,ref _8tmd0ner );
				_2197fa5i(ref Unsafe.AsRef((_dxpq0xkr - _dk3nh7xl) - (int)1) ,(_2ivtt43r+(_dk3nh7xl - 1) + (_dk3nh7xl + (int)2 - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,(_2ivtt43r+(_psodi8to - 1) + (_dk3nh7xl + (int)2 - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,ref _82tpdhyl ,ref _8tmd0ner );
				_2197fa5i(ref Unsafe.AsRef(_dk3nh7xl - (int)1) ,(_2ivtt43r+((int)1 - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_2ivtt43r+((int)1 - 1) + (_psodi8to - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,ref _82tpdhyl ,ref _8tmd0ner );
				if (_gh3pzgwj)
				_2197fa5i(ref _dxpq0xkr ,(_atumjwo3+((int)1 - 1) + (_dk3nh7xl - 1) * 1 * (_u3fpniqy)),ref Unsafe.AsRef((int)1) ,(_atumjwo3+((int)1 - 1) + (_psodi8to - 1) * 1 * (_u3fpniqy)),ref Unsafe.AsRef((int)1) ,ref _82tpdhyl ,ref _8tmd0ner );
			}
			//* 
			
			if (_4o1bt8b1 == (int)2)
			{
				//* 
				//*           Standardize new 2-by-2 block T22 
				//* 
				
				_mj5xo5tg = (_dk3nh7xl + _tixk7d1h);
				_h5f9ahvx = (_mj5xo5tg + (int)1);
				_srab5ud9(ref Unsafe.AsRef(*(_2ivtt43r+(_mj5xo5tg - 1) + (_mj5xo5tg - 1) * 1 * (_w8yhbr2r))) ,ref Unsafe.AsRef(*(_2ivtt43r+(_mj5xo5tg - 1) + (_h5f9ahvx - 1) * 1 * (_w8yhbr2r))) ,ref Unsafe.AsRef(*(_2ivtt43r+(_h5f9ahvx - 1) + (_mj5xo5tg - 1) * 1 * (_w8yhbr2r))) ,ref Unsafe.AsRef(*(_2ivtt43r+(_h5f9ahvx - 1) + (_h5f9ahvx - 1) * 1 * (_w8yhbr2r))) ,ref _ddw256ub ,ref _y2cau71e ,ref _jzbad79b ,ref _19jvoy1k ,ref _82tpdhyl ,ref _8tmd0ner );
				if ((_mj5xo5tg + (int)2) <= _dxpq0xkr)
				_2197fa5i(ref Unsafe.AsRef((_dxpq0xkr - _mj5xo5tg) - (int)1) ,(_2ivtt43r+(_mj5xo5tg - 1) + (_mj5xo5tg + (int)2 - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,(_2ivtt43r+(_h5f9ahvx - 1) + (_mj5xo5tg + (int)2 - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,ref _82tpdhyl ,ref _8tmd0ner );
				_2197fa5i(ref Unsafe.AsRef(_mj5xo5tg - (int)1) ,(_2ivtt43r+((int)1 - 1) + (_mj5xo5tg - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_2ivtt43r+((int)1 - 1) + (_h5f9ahvx - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,ref _82tpdhyl ,ref _8tmd0ner );
				if (_gh3pzgwj)
				_2197fa5i(ref _dxpq0xkr ,(_atumjwo3+((int)1 - 1) + (_mj5xo5tg - 1) * 1 * (_u3fpniqy)),ref Unsafe.AsRef((int)1) ,(_atumjwo3+((int)1 - 1) + (_h5f9ahvx - 1) * 1 * (_u3fpniqy)),ref Unsafe.AsRef((int)1) ,ref _82tpdhyl ,ref _8tmd0ner );
			}
			//* 
			
		}
		
		return;//* 
		//*     Exit with INFO = 1 if swap was rejected. 
		//* 
		
Mark50:;
		// continue
		_gro5yvfo = (int)1;
		return;//* 
		//*     End of DLAEXC 
		//* 
		
	}
	
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
