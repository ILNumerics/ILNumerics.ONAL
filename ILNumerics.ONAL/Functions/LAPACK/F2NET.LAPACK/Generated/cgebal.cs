
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
//*> \brief \b CGEBAL 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CGEBAL + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/cgebal.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/cgebal.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/cgebal.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CGEBAL( JOB, N, A, LDA, ILO, IHI, SCALE, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          JOB 
//*       INTEGER            IHI, ILO, INFO, LDA, N 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               SCALE( * ) 
//*       COMPLEX            A( LDA, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CGEBAL balances a general complex matrix A.  This involves, first, 
//*> permuting A by a similarity transformation to isolate eigenvalues 
//*> in the first 1 to ILO-1 and last IHI+1 to N elements on the 
//*> diagonal; and second, applying a diagonal similarity transformation 
//*> to rows and columns ILO to IHI to make the rows and columns as 
//*> close in norm as possible.  Both steps are optional. 
//*> 
//*> Balancing may reduce the 1-norm of the matrix, and improve the 
//*> accuracy of the computed eigenvalues and/or eigenvectors. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] JOB 
//*> \verbatim 
//*>          JOB is CHARACTER*1 
//*>          Specifies the operations to be performed on A: 
//*>          = 'N':  none:  simply set ILO = 1, IHI = N, SCALE(I) = 1.0 
//*>                  for i = 1,...,N; 
//*>          = 'P':  permute only; 
//*>          = 'S':  scale only; 
//*>          = 'B':  both permute and scale. 
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
//*>          A is COMPLEX array, dimension (LDA,N) 
//*>          On entry, the input matrix A. 
//*>          On exit,  A is overwritten by the balanced matrix. 
//*>          If JOB = 'N', A is not referenced. 
//*>          See Further Details. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[out] ILO 
//*> \verbatim 
//*>          ILO is INTEGER 
//*> \endverbatim 
//*> \param[out] IHI 
//*> \verbatim 
//*>          IHI is INTEGER 
//*>          ILO and IHI are set to integers such that on exit 
//*>          A(i,j) = 0 if i > j and j = 1,...,ILO-1 or I = IHI+1,...,N. 
//*>          If JOB = 'N' or 'S', ILO = 1 and IHI = N. 
//*> \endverbatim 
//*> 
//*> \param[out] SCALE 
//*> \verbatim 
//*>          SCALE is REAL array, dimension (N) 
//*>          Details of the permutations and scaling factors applied to 
//*>          A.  If P(j) is the index of the row and column interchanged 
//*>          with row and column j and D(j) is the scaling factor 
//*>          applied to row and column j, then 
//*>          SCALE(j) = P(j)    for j = 1,...,ILO-1 
//*>                   = D(j)    for j = ILO,...,IHI 
//*>                   = P(j)    for j = IHI+1,...,N. 
//*>          The order in which the interchanges are made is N to IHI+1, 
//*>          then 1 to ILO-1. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit. 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value. 
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
//*> \ingroup complexGEcomputational 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  The permutations consist of row and column interchanges which put 
//*>  the matrix in the form 
//*> 
//*>             ( T1   X   Y  ) 
//*>     P A P = (  0   B   Z  ) 
//*>             (  0   0   T2 ) 
//*> 
//*>  where T1 and T2 are upper triangular matrices whose eigenvalues lie 
//*>  along the diagonal.  The column indices ILO and IHI mark the starting 
//*>  and ending columns of the submatrix B. Balancing consists of applying 
//*>  a diagonal similarity transformation inv(D) * B * D to make the 
//*>  1-norms of each row of B and its corresponding column nearly equal. 
//*>  The output matrix is 
//*> 
//*>     ( T1     X*D          Y    ) 
//*>     (  0  inv(D)*B*D  inv(D)*Z ). 
//*>     (  0      0           T2   ) 
//*> 
//*>  Information about the permutations P and the diagonal matrix D is 
//*>  returned in the vector SCALE. 
//*> 
//*>  This subroutine is based on the EISPACK routine CBAL. 
//*> 
//*>  Modified by Tzu-Yi Chen, Computer Science Division, University of 
//*>    California at Berkeley, USA 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _z3dunwfi(FString _xcrv93xi, ref Int32 _dxpq0xkr, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, ref Int32 _pew3blan, ref Int32 _9c1csucx, Single* _1m44vtuk, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
Single _9g2ab6cj =  2f;
Single _eph5s3zx =  0.95f;
Boolean _ld8702hs =  default;
Int32 _b5p6od9s =  default;
Int32 _04mc4jy1 =  default;
Int32 _550r38gi =  default;
Int32 _ia4q61w9 =  default;
Int32 _znpjgsef =  default;
Int32 _umlkckdg =  default;
Int32 _68ec3gbh =  default;
Int32 _ev4xhht5 =  default;
Single _3crf0qn3 =  default;
Single _han26hfi =  default;
Single _8plnuphw =  default;
Single _mu73se41 =  default;
Single _q2vwp05i =  default;
Single _fwhw1i65 =  default;
Single _irk8i6qr =  default;
Single _ci5dnex5 =  default;
Single _1qaxch3n =  default;
Single _6zy070v9 =  default;
Single _in7icb9x =  default;
string fLanavab = default;
#endregion  variable declarations
_xcrv93xi = _xcrv93xi.Convert(1);

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
		//*     .. External Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//* 
		//*     Test the input parameters 
		//* 
		
		_gro5yvfo = (int)0;
		if ((((!(_w8y2rzgy(_xcrv93xi ,"N" ))) & (!(_w8y2rzgy(_xcrv93xi ,"P" )))) & (!(_w8y2rzgy(_xcrv93xi ,"S" )))) & (!(_w8y2rzgy(_xcrv93xi ,"B" ))))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)-4;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("CGEBAL" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		
		_umlkckdg = (int)1;
		_68ec3gbh = _dxpq0xkr;//* 
		
		if (_dxpq0xkr == (int)0)goto Mark210;//* 
		
		if (_w8y2rzgy(_xcrv93xi ,"N" ))
		{
			
			{
				System.Int32 __81fgg2dlsvn2512 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2512 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2512;
				for (__81fgg2count2512 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2512 + __81fgg2step2512) / __81fgg2step2512)), _b5p6od9s = __81fgg2dlsvn2512; __81fgg2count2512 != 0; __81fgg2count2512--, _b5p6od9s += (__81fgg2step2512)) {

				{
					
					*(_1m44vtuk+(_b5p6od9s - 1)) = _kxg5drh2;
Mark10:;
					// continue
				}
								}			}goto Mark210;
		}
		//* 
		
		if (_w8y2rzgy(_xcrv93xi ,"S" ))goto Mark120;//* 
		//*     Permutation to isolate eigenvalues if possible 
		//* 
		goto Mark50;//* 
		//*     Row and column exchange. 
		//* 
		
Mark20:;
		// continue
		*(_1m44vtuk+(_ev4xhht5 - 1)) = REAL(_znpjgsef);
		if (_znpjgsef == _ev4xhht5)goto Mark30;//* 
		
		_1frbwlh0(ref _68ec3gbh ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,(_vxfgpup9+((int)1 - 1) + (_ev4xhht5 - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
		_1frbwlh0(ref Unsafe.AsRef((_dxpq0xkr - _umlkckdg) + (int)1) ,(_vxfgpup9+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_ev4xhht5 - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );//* 
		
Mark30:;
		// continue
		switch (_550r38gi) {
						case 1:
			goto Mark40;
			case 2:
			goto Mark80;
			default:
			break;
		}
//* 
		//*     Search for rows isolating an eigenvalue and push them down. 
		//* 
		
Mark40:;
		// continue
		if (_68ec3gbh == (int)1)goto Mark210;
		_68ec3gbh = (_68ec3gbh - (int)1);//* 
		
Mark50:;
		// continue
		{
			System.Int32 __81fgg2dlsvn2513 = (System.Int32)(_68ec3gbh);
			System.Int32 __81fgg2step2513 = (System.Int32)((int)-1);
			System.Int32 __81fgg2count2513;
			for (__81fgg2count2513 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn2513 + __81fgg2step2513) / __81fgg2step2513)), _znpjgsef = __81fgg2dlsvn2513; __81fgg2count2513 != 0; __81fgg2count2513--, _znpjgsef += (__81fgg2step2513)) {

			{
				//* 
				
				{
					System.Int32 __81fgg2dlsvn2514 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2514 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2514;
					for (__81fgg2count2514 = System.Math.Max(0, (System.Int32)(((System.Int32)(_68ec3gbh) - __81fgg2dlsvn2514 + __81fgg2step2514) / __81fgg2step2514)), _b5p6od9s = __81fgg2dlsvn2514; __81fgg2count2514 != 0; __81fgg2count2514--, _b5p6od9s += (__81fgg2step2514)) {

					{
						
						if (_b5p6od9s == _znpjgsef)goto Mark60;
						if ((ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) != _d0547bi2) | (ILNumerics.F2NET.Intrinsics.AIMAG(*(_vxfgpup9+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) ) != _d0547bi2))goto Mark70;
Mark60:;
						// continue
					}
										}				}//* 
				
				_ev4xhht5 = _68ec3gbh;
				_550r38gi = (int)1;goto Mark20;
Mark70:;
				// continue
			}
						}		}//* 
		goto Mark90;//* 
		//*     Search for columns isolating an eigenvalue and push them left. 
		//* 
		
Mark80:;
		// continue
		_umlkckdg = (_umlkckdg + (int)1);//* 
		
Mark90:;
		// continue
		{
			System.Int32 __81fgg2dlsvn2515 = (System.Int32)(_umlkckdg);
			const System.Int32 __81fgg2step2515 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2515;
			for (__81fgg2count2515 = System.Math.Max(0, (System.Int32)(((System.Int32)(_68ec3gbh) - __81fgg2dlsvn2515 + __81fgg2step2515) / __81fgg2step2515)), _znpjgsef = __81fgg2dlsvn2515; __81fgg2count2515 != 0; __81fgg2count2515--, _znpjgsef += (__81fgg2step2515)) {

			{
				//* 
				
				{
					System.Int32 __81fgg2dlsvn2516 = (System.Int32)(_umlkckdg);
					const System.Int32 __81fgg2step2516 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2516;
					for (__81fgg2count2516 = System.Math.Max(0, (System.Int32)(((System.Int32)(_68ec3gbh) - __81fgg2dlsvn2516 + __81fgg2step2516) / __81fgg2step2516)), _b5p6od9s = __81fgg2dlsvn2516; __81fgg2count2516 != 0; __81fgg2count2516--, _b5p6od9s += (__81fgg2step2516)) {

					{
						
						if (_b5p6od9s == _znpjgsef)goto Mark100;
						if ((ILNumerics.F2NET.Intrinsics.REAL(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) != _d0547bi2) | (ILNumerics.F2NET.Intrinsics.AIMAG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) != _d0547bi2))goto Mark110;
Mark100:;
						// continue
					}
										}				}//* 
				
				_ev4xhht5 = _umlkckdg;
				_550r38gi = (int)2;goto Mark20;
Mark110:;
				// continue
			}
						}		}//* 
		
Mark120:;
		// continue
		{
			System.Int32 __81fgg2dlsvn2517 = (System.Int32)(_umlkckdg);
			const System.Int32 __81fgg2step2517 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2517;
			for (__81fgg2count2517 = System.Math.Max(0, (System.Int32)(((System.Int32)(_68ec3gbh) - __81fgg2dlsvn2517 + __81fgg2step2517) / __81fgg2step2517)), _b5p6od9s = __81fgg2dlsvn2517; __81fgg2count2517 != 0; __81fgg2count2517--, _b5p6od9s += (__81fgg2step2517)) {

			{
				
				*(_1m44vtuk+(_b5p6od9s - 1)) = _kxg5drh2;
Mark130:;
				// continue
			}
						}		}//* 
		
		if (_w8y2rzgy(_xcrv93xi ,"P" ))goto Mark210;//* 
		//*     Balance the submatrix in rows K to L. 
		//* 
		//*     Iterative loop for norm reduction 
		//* 
		
		_6zy070v9 = (_d5tu038y("S" ) / _d5tu038y("P" ));
		_ci5dnex5 = (_kxg5drh2 / _6zy070v9);
		_in7icb9x = (_6zy070v9 * _9g2ab6cj);
		_1qaxch3n = (_kxg5drh2 / _in7icb9x);
Mark140:;
		// continue
		_ld8702hs = false;//* 
		
		{
			System.Int32 __81fgg2dlsvn2518 = (System.Int32)(_umlkckdg);
			const System.Int32 __81fgg2step2518 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2518;
			for (__81fgg2count2518 = System.Math.Max(0, (System.Int32)(((System.Int32)(_68ec3gbh) - __81fgg2dlsvn2518 + __81fgg2step2518) / __81fgg2step2518)), _b5p6od9s = __81fgg2dlsvn2518; __81fgg2count2518 != 0; __81fgg2count2518--, _b5p6od9s += (__81fgg2step2518)) {

			{
				//* 
				
				_3crf0qn3 = _igbqnt3f(ref Unsafe.AsRef((_68ec3gbh - _umlkckdg) + (int)1) ,(_vxfgpup9+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
				_q2vwp05i = _igbqnt3f(ref Unsafe.AsRef((_68ec3gbh - _umlkckdg) + (int)1) ,(_vxfgpup9+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
				_04mc4jy1 = _r3truie3(ref _68ec3gbh ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
				_han26hfi = ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_04mc4jy1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) );
				_ia4q61w9 = _r3truie3(ref Unsafe.AsRef((_dxpq0xkr - _umlkckdg) + (int)1) ,(_vxfgpup9+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
				_fwhw1i65 = ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+(_b5p6od9s - 1) + ((_ia4q61w9 + _umlkckdg) - (int)1 - 1) * 1 * (_ocv8fk5c)) );//* 
				//*        Guard against zero C or R due to underflow. 
				//* 
				
				if ((_3crf0qn3 == _d0547bi2) | (_q2vwp05i == _d0547bi2))goto Mark200;
				_mu73se41 = (_q2vwp05i / _9g2ab6cj);
				_8plnuphw = _kxg5drh2;
				_irk8i6qr = (_3crf0qn3 + _q2vwp05i);
Mark160:;
				// continue
				if (((_3crf0qn3 >= _mu73se41) | (ILNumerics.F2NET.Intrinsics.MAX(_8plnuphw ,_3crf0qn3 ,_han26hfi ) >= _1qaxch3n)) | (ILNumerics.F2NET.Intrinsics.MIN(_q2vwp05i ,_mu73se41 ,_fwhw1i65 ) <= _in7icb9x))goto Mark170;
				if (_lilv8egi(ref Unsafe.AsRef(((((_3crf0qn3 + _8plnuphw) + _han26hfi) + _q2vwp05i) + _mu73se41) + _fwhw1i65) ))
				{
					//* 
					//*           Exit if NaN to avoid infinite loop 
					//* 
					
					_gro5yvfo = (int)-3;
					_ut9qalzx("CGEBAL" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
					return;
				}
				
				_8plnuphw = (_8plnuphw * _9g2ab6cj);
				_3crf0qn3 = (_3crf0qn3 * _9g2ab6cj);
				_han26hfi = (_han26hfi * _9g2ab6cj);
				_q2vwp05i = (_q2vwp05i / _9g2ab6cj);
				_mu73se41 = (_mu73se41 / _9g2ab6cj);
				_fwhw1i65 = (_fwhw1i65 / _9g2ab6cj);goto Mark160;//* 
				
Mark170:;
				// continue
				_mu73se41 = (_3crf0qn3 / _9g2ab6cj);
Mark180:;
				// continue
				if (((_mu73se41 < _q2vwp05i) | (ILNumerics.F2NET.Intrinsics.MAX(_q2vwp05i ,_fwhw1i65 ) >= _1qaxch3n)) | (ILNumerics.F2NET.Intrinsics.MIN(_8plnuphw ,_3crf0qn3 ,_mu73se41 ,_han26hfi ) <= _in7icb9x))goto Mark190;
				_8plnuphw = (_8plnuphw / _9g2ab6cj);
				_3crf0qn3 = (_3crf0qn3 / _9g2ab6cj);
				_mu73se41 = (_mu73se41 / _9g2ab6cj);
				_han26hfi = (_han26hfi / _9g2ab6cj);
				_q2vwp05i = (_q2vwp05i * _9g2ab6cj);
				_fwhw1i65 = (_fwhw1i65 * _9g2ab6cj);goto Mark180;//* 
				//*        Now balance. 
				//* 
				
Mark190:;
				// continue
				if ((_3crf0qn3 + _q2vwp05i) >= (_eph5s3zx * _irk8i6qr))goto Mark200;
				if ((_8plnuphw < _kxg5drh2) & (*(_1m44vtuk+(_b5p6od9s - 1)) < _kxg5drh2))
				{
					
					if ((_8plnuphw * *(_1m44vtuk+(_b5p6od9s - 1))) <= _6zy070v9)goto Mark200;
				}
				
				if ((_8plnuphw > _kxg5drh2) & (*(_1m44vtuk+(_b5p6od9s - 1)) > _kxg5drh2))
				{
					
					if (*(_1m44vtuk+(_b5p6od9s - 1)) >= (_ci5dnex5 / _8plnuphw))goto Mark200;
				}
				
				_mu73se41 = (_kxg5drh2 / _8plnuphw);
				*(_1m44vtuk+(_b5p6od9s - 1)) = (*(_1m44vtuk+(_b5p6od9s - 1)) * _8plnuphw);
				_ld8702hs = true;//* 
				
				_2ylagitj(ref Unsafe.AsRef((_dxpq0xkr - _umlkckdg) + (int)1) ,ref _mu73se41 ,(_vxfgpup9+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
				_2ylagitj(ref _68ec3gbh ,ref _8plnuphw ,(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );//* 
				
Mark200:;
				// continue
			}
						}		}//* 
		
		if (_ld8702hs)goto Mark140;//* 
		
Mark210:;
		// continue
		_pew3blan = _umlkckdg;
		_9c1csucx = _68ec3gbh;//* 
		
		return;//* 
		//*     End of CGEBAL 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
