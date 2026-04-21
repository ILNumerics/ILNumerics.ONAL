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
//*> \brief \b SSTEIN 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SSTEIN + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/sstein.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/sstein.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/sstein.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SSTEIN( N, D, E, M, W, IBLOCK, ISPLIT, Z, LDZ, WORK, 
//*                          IWORK, IFAIL, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, LDZ, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IBLOCK( * ), IFAIL( * ), ISPLIT( * ), 
//*      $                   IWORK( * ) 
//*       REAL               D( * ), E( * ), W( * ), WORK( * ), Z( LDZ, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SSTEIN computes the eigenvectors of a real symmetric tridiagonal 
//*> matrix T corresponding to specified eigenvalues, using inverse 
//*> iteration. 
//*> 
//*> The maximum number of iterations allowed for each eigenvector is 
//*> specified by an internal parameter MAXITS (currently set to 5). 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] D 
//*> \verbatim 
//*>          D is REAL array, dimension (N) 
//*>          The n diagonal elements of the tridiagonal matrix T. 
//*> \endverbatim 
//*> 
//*> \param[in] E 
//*> \verbatim 
//*>          E is REAL array, dimension (N-1) 
//*>          The (n-1) subdiagonal elements of the tridiagonal matrix 
//*>          T, in elements 1 to N-1. 
//*> \endverbatim 
//*> 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of eigenvectors to be found.  0 <= M <= N. 
//*> \endverbatim 
//*> 
//*> \param[in] W 
//*> \verbatim 
//*>          W is REAL array, dimension (N) 
//*>          The first M elements of W contain the eigenvalues for 
//*>          which eigenvectors are to be computed.  The eigenvalues 
//*>          should be grouped by split-off block and ordered from 
//*>          smallest to largest within the block.  ( The output array 
//*>          W from SSTEBZ with ORDER = 'B' is expected here. ) 
//*> \endverbatim 
//*> 
//*> \param[in] IBLOCK 
//*> \verbatim 
//*>          IBLOCK is INTEGER array, dimension (N) 
//*>          The submatrix indices associated with the corresponding 
//*>          eigenvalues in W; IBLOCK(i)=1 if eigenvalue W(i) belongs to 
//*>          the first submatrix from the top, =2 if W(i) belongs to 
//*>          the second submatrix, etc.  ( The output array IBLOCK 
//*>          from SSTEBZ is expected here. ) 
//*> \endverbatim 
//*> 
//*> \param[in] ISPLIT 
//*> \verbatim 
//*>          ISPLIT is INTEGER array, dimension (N) 
//*>          The splitting points, at which T breaks up into submatrices. 
//*>          The first submatrix consists of rows/columns 1 to 
//*>          ISPLIT( 1 ), the second of rows/columns ISPLIT( 1 )+1 
//*>          through ISPLIT( 2 ), etc. 
//*>          ( The output array ISPLIT from SSTEBZ is expected here. ) 
//*> \endverbatim 
//*> 
//*> \param[out] Z 
//*> \verbatim 
//*>          Z is REAL array, dimension (LDZ, M) 
//*>          The computed eigenvectors.  The eigenvector associated 
//*>          with the eigenvalue W(i) is stored in the i-th column of 
//*>          Z.  Any vector which fails to converge is set to its current 
//*>          iterate after MAXITS iterations. 
//*> \endverbatim 
//*> 
//*> \param[in] LDZ 
//*> \verbatim 
//*>          LDZ is INTEGER 
//*>          The leading dimension of the array Z.  LDZ >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is REAL array, dimension (5*N) 
//*> \endverbatim 
//*> 
//*> \param[out] IWORK 
//*> \verbatim 
//*>          IWORK is INTEGER array, dimension (N) 
//*> \endverbatim 
//*> 
//*> \param[out] IFAIL 
//*> \verbatim 
//*>          IFAIL is INTEGER array, dimension (M) 
//*>          On normal exit, all elements of IFAIL are zero. 
//*>          If one or more eigenvectors fail to converge after 
//*>          MAXITS iterations, then their indices are stored in 
//*>          array IFAIL. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0: successful exit. 
//*>          < 0: if INFO = -i, the i-th argument had an illegal value 
//*>          > 0: if INFO = i, then i eigenvectors failed to converge 
//*>               in MAXITS iterations.  Their indices are stored in 
//*>               array IFAIL. 
//*> \endverbatim 
//* 
//*> \par Internal Parameters: 
//*  ========================= 
//*> 
//*> \verbatim 
//*>  MAXITS  INTEGER, default = 5 
//*>          The maximum number of iterations performed. 
//*> 
//*>  EXTRA   INTEGER, default = 2 
//*>          The number of iterations performed after norm growth 
//*>          criterion is satisfied, should be at least 1. 
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
//*> \ingroup realOTHERcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _rvcqshyo(ref Int32 _dxpq0xkr, Single* _plfm7z8g, Single* _864fslqq, ref Int32 _ev4xhht5, Single* _z1ioc3c8, Int32* _5zga5mk9, Int32* _nn033w1s, Single* _7e60fcso, ref Int32 _5l1tna8s, Single* _apig8meb, Int32* _4b6rt45i, Int32* _qngy48w5, ref Int32 _gro5yvfo)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)16 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
Single _76g572q1 =  10f;
Single _6kh3tpe5 =  0.001f;
Single _8x95b5ti =  0.1f;
Int32 _fwko83wa =  (int)5;
Int32 _pjank1ie =  (int)2;
Int32 _an5h6pnm =  default;
Int32 _i02zvjyx =  default;
Int32 _febv6765 =  default;
Int32 _kqoro1du =  default;
Int32 _b5p6od9s =  default;
Int32 _itfnbz60 =  default;
Int32 _48siyi7c =  default;
Int32 _xhnkp0uh =  default;
Int32 _o5xkd3w6 =  default;
Int32 _7wdrofvx =  default;
Int32 _fpsmiapa =  default;
Int32 _qrxtt59n =  default;
Int32 _znpjgsef =  default;
Int32 _dk3nh7xl =  default;
Int32 _18fz0zf9 =  default;
Int32 _2zco5oit =  default;
Int32 _6hcse2rb =  default;
Int32 _lwlbeqys =  default;
Single _kean55h2 =  default;
Single _p1iqarg6 =  default;
Single _pwsn1ias =  default;
Single _otuhdctn =  default;
Single _8rawzo5m =  default;
Single _bt3fkily =  default;
Single _jydskdjd =  default;
Single _ofbdxt08 =  default;
Single _k21k7dwo =  default;
Single _z99bk0ac =  default;
Single _txq1gp7u =  default;
Single _rtkm3mk9 =  default;
Single _v2xu5ros =  default;
Int32* _5c1snrj6 =  (Int32*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Int32) * ((int)4);
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
		//*     Test the input parameters. 
		//* 
		
		_gro5yvfo = (int)0;
		{
			System.Int32 __81fgg2dlsvn3416 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step3416 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3416;
			for (__81fgg2count3416 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3416 + __81fgg2step3416) / __81fgg2step3416)), _b5p6od9s = __81fgg2dlsvn3416; __81fgg2count3416 != 0; __81fgg2count3416--, _b5p6od9s += (__81fgg2step3416)) {

			{
				
				*(_qngy48w5+(_b5p6od9s - 1)) = (int)0;
Mark10:;
				// continue
			}
						}		}//* 
		
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if ((_ev4xhht5 < (int)0) | (_ev4xhht5 > _dxpq0xkr))
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if (_5l1tna8s < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)-9;
		}
		else
		{
			
			{
				System.Int32 __81fgg2dlsvn3417 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step3417 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3417;
				for (__81fgg2count3417 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3417 + __81fgg2step3417) / __81fgg2step3417)), _znpjgsef = __81fgg2dlsvn3417; __81fgg2count3417 != 0; __81fgg2count3417--, _znpjgsef += (__81fgg2step3417)) {

				{
					
					if (*(_5zga5mk9+(_znpjgsef - 1)) < *(_5zga5mk9+(_znpjgsef - (int)1 - 1)))
					{
						
						_gro5yvfo = (int)-6;goto Mark30;
					}
					
					if ((*(_5zga5mk9+(_znpjgsef - 1)) == *(_5zga5mk9+(_znpjgsef - (int)1 - 1))) & (*(_z1ioc3c8+(_znpjgsef - 1)) < *(_z1ioc3c8+(_znpjgsef - (int)1 - 1))))
					{
						
						_gro5yvfo = (int)-5;goto Mark30;
					}
					
Mark20:;
					// continue
				}
								}			}
Mark30:;
			// continue
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("SSTEIN" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if ((_dxpq0xkr == (int)0) | (_ev4xhht5 == (int)0))
		{
			
			return;
		}
		else
		if (_dxpq0xkr == (int)1)
		{
			
			*(_7e60fcso+((int)1 - 1) + ((int)1 - 1) * 1 * (_5l1tna8s)) = _kxg5drh2;
			return;
		}
		//* 
		//*     Get machine constants. 
		//* 
		
		_p1iqarg6 = _d5tu038y("Precision" );//* 
		//*     Initialize seed for random number generator SLARNV. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn3418 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step3418 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3418;
			for (__81fgg2count3418 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)4) - __81fgg2dlsvn3418 + __81fgg2step3418) / __81fgg2step3418)), _b5p6od9s = __81fgg2dlsvn3418; __81fgg2count3418 != 0; __81fgg2count3418--, _b5p6od9s += (__81fgg2step3418)) {

			{
				
				*(_5c1snrj6+(_b5p6od9s - 1)) = (int)1;
Mark40:;
				// continue
			}
						}		}//* 
		//*     Initialize pointers. 
		//* 
		
		_48siyi7c = (int)0;
		_xhnkp0uh = (_48siyi7c + _dxpq0xkr);
		_o5xkd3w6 = (_xhnkp0uh + _dxpq0xkr);
		_7wdrofvx = (_o5xkd3w6 + _dxpq0xkr);
		_fpsmiapa = (_7wdrofvx + _dxpq0xkr);//* 
		//*     Compute eigenvectors of matrix blocks. 
		//* 
		
		_dk3nh7xl = (int)1;
		{
			System.Int32 __81fgg2dlsvn3419 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step3419 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3419;
			for (__81fgg2count3419 = System.Math.Max(0, (System.Int32)(((System.Int32)(*(_5zga5mk9+(_ev4xhht5 - 1))) - __81fgg2dlsvn3419 + __81fgg2step3419) / __81fgg2step3419)), _6hcse2rb = __81fgg2dlsvn3419; __81fgg2count3419 != 0; __81fgg2count3419--, _6hcse2rb += (__81fgg2step3419)) {

			{
				//* 
				//*        Find starting and ending indices of block nblk. 
				//* 
				
				if (_6hcse2rb == (int)1)
				{
					
					_an5h6pnm = (int)1;
				}
				else
				{
					
					_an5h6pnm = (*(_nn033w1s+(_6hcse2rb - (int)1 - 1)) + (int)1);
				}
				
				_febv6765 = *(_nn033w1s+(_6hcse2rb - 1));
				_i02zvjyx = ((_febv6765 - _an5h6pnm) + (int)1);
				if (_i02zvjyx == (int)1)goto Mark60;
				_kqoro1du = _dk3nh7xl;//* 
				//*        Compute reorthogonalization criterion and stopping criterion. 
				//* 
				
				_8rawzo5m = (ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_an5h6pnm - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_an5h6pnm - 1)) ));
				_8rawzo5m = ILNumerics.F2NET.Intrinsics.MAX(_8rawzo5m ,ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_febv6765 - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_febv6765 - (int)1 - 1)) ) );
				{
					System.Int32 __81fgg2dlsvn3420 = (System.Int32)((_an5h6pnm + (int)1));
					const System.Int32 __81fgg2step3420 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3420;
					for (__81fgg2count3420 = System.Math.Max(0, (System.Int32)(((System.Int32)(_febv6765 - (int)1) - __81fgg2dlsvn3420 + __81fgg2step3420) / __81fgg2step3420)), _b5p6od9s = __81fgg2dlsvn3420; __81fgg2count3420 != 0; __81fgg2count3420--, _b5p6od9s += (__81fgg2step3420)) {

					{
						
						_8rawzo5m = ILNumerics.F2NET.Intrinsics.MAX(_8rawzo5m ,(ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_b5p6od9s - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_b5p6od9s - (int)1 - 1)) )) + ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_b5p6od9s - 1)) ) );
Mark50:;
						// continue
					}
										}				}
				_bt3fkily = (_6kh3tpe5 * _8rawzo5m);//* 
				
				_z99bk0ac = ILNumerics.F2NET.Intrinsics.SQRT(_8x95b5ti / _i02zvjyx );//* 
				//*        Loop through eigenvalues of block nblk. 
				//* 
				
Mark60:;
				// continue
				_18fz0zf9 = (int)0;
				{
					System.Int32 __81fgg2dlsvn3421 = (System.Int32)(_dk3nh7xl);
					const System.Int32 __81fgg2step3421 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3421;
					for (__81fgg2count3421 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3421 + __81fgg2step3421) / __81fgg2step3421)), _znpjgsef = __81fgg2dlsvn3421; __81fgg2count3421 != 0; __81fgg2count3421--, _znpjgsef += (__81fgg2step3421)) {

					{
						
						if (*(_5zga5mk9+(_znpjgsef - 1)) != _6hcse2rb)
						{
							
							_dk3nh7xl = _znpjgsef;goto Mark160;
						}
						
						_18fz0zf9 = (_18fz0zf9 + (int)1);
						_rtkm3mk9 = *(_z1ioc3c8+(_znpjgsef - 1));//* 
						//*           Skip all the work if the block size is one. 
						//* 
						
						if (_i02zvjyx == (int)1)
						{
							
							*(_apig8meb+(_48siyi7c + (int)1 - 1)) = _kxg5drh2;goto Mark120;
						}
						//* 
						//*           If eigenvalues j and j-1 are too close, add a relatively 
						//*           small perturbation. 
						//* 
						
						if (_18fz0zf9 > (int)1)
						{
							
							_pwsn1ias = ILNumerics.F2NET.Intrinsics.ABS(_p1iqarg6 * _rtkm3mk9 );
							_jydskdjd = (_76g572q1 * _pwsn1ias);
							_k21k7dwo = (_rtkm3mk9 - _v2xu5ros);
							if (_k21k7dwo < _jydskdjd)
							_rtkm3mk9 = (_v2xu5ros + _jydskdjd);
						}
						//* 
						
						_qrxtt59n = (int)0;
						_lwlbeqys = (int)0;//* 
						//*           Get random starting vector. 
						//* 
						
						_rcijgesd(ref Unsafe.AsRef((int)2) ,_5c1snrj6 ,ref _i02zvjyx ,(_apig8meb+(_48siyi7c + (int)1 - 1)));//* 
						//*           Copy the matrix T so it won't be destroyed in factorization. 
						//* 
						
						_wcs7ne88(ref _i02zvjyx ,(_plfm7z8g+(_an5h6pnm - 1)),ref Unsafe.AsRef((int)1) ,(_apig8meb+(_7wdrofvx + (int)1 - 1)),ref Unsafe.AsRef((int)1) );
						_wcs7ne88(ref Unsafe.AsRef(_i02zvjyx - (int)1) ,(_864fslqq+(_an5h6pnm - 1)),ref Unsafe.AsRef((int)1) ,(_apig8meb+(_xhnkp0uh + (int)2 - 1)),ref Unsafe.AsRef((int)1) );
						_wcs7ne88(ref Unsafe.AsRef(_i02zvjyx - (int)1) ,(_864fslqq+(_an5h6pnm - 1)),ref Unsafe.AsRef((int)1) ,(_apig8meb+(_o5xkd3w6 + (int)1 - 1)),ref Unsafe.AsRef((int)1) );//* 
						//*           Compute LU factors with partial pivoting  ( PT = LU ) 
						//* 
						
						_txq1gp7u = _d0547bi2;
						_8afyb8tq(ref _i02zvjyx ,(_apig8meb+(_7wdrofvx + (int)1 - 1)),ref _rtkm3mk9 ,(_apig8meb+(_xhnkp0uh + (int)2 - 1)),(_apig8meb+(_o5xkd3w6 + (int)1 - 1)),ref _txq1gp7u ,(_apig8meb+(_fpsmiapa + (int)1 - 1)),_4b6rt45i ,ref _itfnbz60 );//* 
						//*           Update iteration count. 
						//* 
						
Mark70:;
						// continue
						_qrxtt59n = (_qrxtt59n + (int)1);
						if (_qrxtt59n > _fwko83wa)goto Mark100;//* 
						//*           Normalize and scale the righthand side vector Pb. 
						//* 
						
						_2zco5oit = _z5b2nqbf(ref _i02zvjyx ,(_apig8meb+(_48siyi7c + (int)1 - 1)),ref Unsafe.AsRef((int)1) );
						_ofbdxt08 = (((_i02zvjyx * _8rawzo5m) * ILNumerics.F2NET.Intrinsics.MAX(_p1iqarg6 ,ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+(_7wdrofvx + _i02zvjyx - 1)) ) )) / ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+(_48siyi7c + _2zco5oit - 1)) ));
						_ct5qqrv7(ref _i02zvjyx ,ref _ofbdxt08 ,(_apig8meb+(_48siyi7c + (int)1 - 1)),ref Unsafe.AsRef((int)1) );//* 
						//*           Solve the system LU = Pb. 
						//* 
						
						_xke7eh7k(ref Unsafe.AsRef((int)-1) ,ref _i02zvjyx ,(_apig8meb+(_7wdrofvx + (int)1 - 1)),(_apig8meb+(_xhnkp0uh + (int)2 - 1)),(_apig8meb+(_o5xkd3w6 + (int)1 - 1)),(_apig8meb+(_fpsmiapa + (int)1 - 1)),_4b6rt45i ,(_apig8meb+(_48siyi7c + (int)1 - 1)),ref _txq1gp7u ,ref _itfnbz60 );//* 
						//*           Reorthogonalize by modified Gram-Schmidt if eigenvalues are 
						//*           close enough. 
						//* 
						
						if (_18fz0zf9 == (int)1)goto Mark90;
						if (ILNumerics.F2NET.Intrinsics.ABS(_rtkm3mk9 - _v2xu5ros ) > _bt3fkily)
						_kqoro1du = _znpjgsef;
						if (_kqoro1du != _znpjgsef)
						{
							
							{
								System.Int32 __81fgg2dlsvn3422 = (System.Int32)(_kqoro1du);
								const System.Int32 __81fgg2step3422 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3422;
								for (__81fgg2count3422 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn3422 + __81fgg2step3422) / __81fgg2step3422)), _b5p6od9s = __81fgg2dlsvn3422; __81fgg2count3422 != 0; __81fgg2count3422--, _b5p6od9s += (__81fgg2step3422)) {

								{
									
									_kean55h2 = (-(_j4n7j2pu(ref _i02zvjyx ,(_apig8meb+(_48siyi7c + (int)1 - 1)),ref Unsafe.AsRef((int)1) ,(_7e60fcso+(_an5h6pnm - 1) + (_b5p6od9s - 1) * 1 * (_5l1tna8s)),ref Unsafe.AsRef((int)1) )));
									_iceh2qqa(ref _i02zvjyx ,ref _kean55h2 ,(_7e60fcso+(_an5h6pnm - 1) + (_b5p6od9s - 1) * 1 * (_5l1tna8s)),ref Unsafe.AsRef((int)1) ,(_apig8meb+(_48siyi7c + (int)1 - 1)),ref Unsafe.AsRef((int)1) );
Mark80:;
									// continue
								}
																}							}
						}
						//* 
						//*           Check the infinity norm of the iterate. 
						//* 
						
Mark90:;
						// continue
						_2zco5oit = _z5b2nqbf(ref _i02zvjyx ,(_apig8meb+(_48siyi7c + (int)1 - 1)),ref Unsafe.AsRef((int)1) );
						_otuhdctn = ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+(_48siyi7c + _2zco5oit - 1)) );//* 
						//*           Continue for additional iterations after norm reaches 
						//*           stopping criterion. 
						//* 
						
						if (_otuhdctn < _z99bk0ac)goto Mark70;
						_lwlbeqys = (_lwlbeqys + (int)1);
						if (_lwlbeqys < (_pjank1ie + (int)1))goto Mark70;//* 
						goto Mark110;//* 
						//*           If stopping criterion was not satisfied, update info and 
						//*           store eigenvector number in array ifail. 
						//* 
						
Mark100:;
						// continue
						_gro5yvfo = (_gro5yvfo + (int)1);
						*(_qngy48w5+(_gro5yvfo - 1)) = _znpjgsef;//* 
						//*           Accept iterate as jth eigenvector. 
						//* 
						
Mark110:;
						// continue
						_ofbdxt08 = (_kxg5drh2 / _z20xbrro(ref _i02zvjyx ,(_apig8meb+(_48siyi7c + (int)1 - 1)),ref Unsafe.AsRef((int)1) ));
						_2zco5oit = _z5b2nqbf(ref _i02zvjyx ,(_apig8meb+(_48siyi7c + (int)1 - 1)),ref Unsafe.AsRef((int)1) );
						if (*(_apig8meb+(_48siyi7c + _2zco5oit - 1)) < _d0547bi2)
						_ofbdxt08 = (-(_ofbdxt08));
						_ct5qqrv7(ref _i02zvjyx ,ref _ofbdxt08 ,(_apig8meb+(_48siyi7c + (int)1 - 1)),ref Unsafe.AsRef((int)1) );
Mark120:;
						// continue
						{
							System.Int32 __81fgg2dlsvn3423 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3423 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3423;
							for (__81fgg2count3423 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3423 + __81fgg2step3423) / __81fgg2step3423)), _b5p6od9s = __81fgg2dlsvn3423; __81fgg2count3423 != 0; __81fgg2count3423--, _b5p6od9s += (__81fgg2step3423)) {

							{
								
								*(_7e60fcso+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_5l1tna8s)) = _d0547bi2;
Mark130:;
								// continue
							}
														}						}
						{
							System.Int32 __81fgg2dlsvn3424 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step3424 = (System.Int32)((int)1);
							System.Int32 __81fgg2count3424;
							for (__81fgg2count3424 = System.Math.Max(0, (System.Int32)(((System.Int32)(_i02zvjyx) - __81fgg2dlsvn3424 + __81fgg2step3424) / __81fgg2step3424)), _b5p6od9s = __81fgg2dlsvn3424; __81fgg2count3424 != 0; __81fgg2count3424--, _b5p6od9s += (__81fgg2step3424)) {

							{
								
								*(_7e60fcso+((_an5h6pnm + _b5p6od9s) - (int)1 - 1) + (_znpjgsef - 1) * 1 * (_5l1tna8s)) = *(_apig8meb+(_48siyi7c + _b5p6od9s - 1));
Mark140:;
								// continue
							}
														}						}//* 
						//*           Save the shift to check eigenvalue spacing at next 
						//*           iteration. 
						//* 
						
						_v2xu5ros = _rtkm3mk9;//* 
						
Mark150:;
						// continue
					}
										}				}
Mark160:;
				// continue
			}
						}		}//* 
		
		return;//* 
		//*     End of SSTEIN 
		//* 
		
	}
	
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
