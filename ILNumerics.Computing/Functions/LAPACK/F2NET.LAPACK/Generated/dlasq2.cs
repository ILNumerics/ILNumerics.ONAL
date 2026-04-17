
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
//*> \brief \b DLASQ2 computes all the eigenvalues of the symmetric positive definite tridiagonal matrix associated with the qd Array Z to high relative accuracy. Used by sbdsqr and sstegr. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLASQ2 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlasq2.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlasq2.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlasq2.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLASQ2( N, Z, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   Z( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLASQ2 computes all the eigenvalues of the symmetric positive 
//*> definite tridiagonal matrix associated with the qd array Z to high 
//*> relative accuracy are computed to high relative accuracy, in the 
//*> absence of denormalization, underflow and overflow. 
//*> 
//*> To see the relation of Z to the tridiagonal matrix, let L be a 
//*> unit lower bidiagonal matrix with subdiagonals Z(2,4,6,,..) and 
//*> let U be an upper bidiagonal matrix with 1's above and diagonal 
//*> Z(1,3,5,,..). The tridiagonal is L*U or, if you prefer, the 
//*> symmetric tridiagonal to which it is similar. 
//*> 
//*> Note : DLASQ2 defines a logical variable, IEEE, which is true 
//*> on machines which follow ieee-754 floating-point standard in their 
//*> handling of infinities and NaNs, and false otherwise. This variable 
//*> is passed to DLASQ3. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>        The number of rows and columns in the matrix. N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] Z 
//*> \verbatim 
//*>          Z is DOUBLE PRECISION array, dimension ( 4*N ) 
//*>        On entry Z holds the qd array. On exit, entries 1 to N hold 
//*>        the eigenvalues in decreasing order, Z( 2*N+1 ) holds the 
//*>        trace, and Z( 2*N+2 ) holds the sum of the eigenvalues. If 
//*>        N > 2, then Z( 2*N+3 ) holds the iteration count, Z( 2*N+4 ) 
//*>        holds NDIVS/NIN^2, and Z( 2*N+5 ) holds the percentage of 
//*>        shifts that failed. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>        = 0: successful exit 
//*>        < 0: if the i-th argument is a scalar and had an illegal 
//*>             value, then INFO = -i, if the i-th argument is an 
//*>             array and the j-entry had an illegal value, then 
//*>             INFO = -(i*100+j) 
//*>        > 0: the algorithm failed 
//*>              = 1, a split was marked by a positive value in E 
//*>              = 2, current block of Z not diagonalized after 100*N 
//*>                   iterations (in inner while loop).  On exit Z holds 
//*>                   a qd array with the same eigenvalues as the given Z. 
//*>              = 3, termination criterion of outer while loop not met 
//*>                   (program created more than N unreduced blocks) 
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
//*> \ingroup auxOTHERcomputational 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  Local Variables: I0:N0 defines a current unreduced segment of Z. 
//*>  The shifts are accumulated in SIGMA. Iteration count is in ITER. 
//*>  Ping-pong is controlled by PP (alternates between 0 and 1). 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _i4v7i21v(ref Int32 _dxpq0xkr, Double* _7e60fcso, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _0tzcjs6r =  1.5d;
Double _d0547bi2 =  0d;
Double _gbf4169i =  0.5d;
Double _kxg5drh2 =  1d;
Double _5m0mjfxm =  2d;
Double _ax5ijvbx =  4d;
Double _2yce0i2m =  100d;
Boolean _id0vp1yu =  default;
Int32 _kgliup4t =  default;
Int32 _egqdmelt =  default;
Int32 _3xy4w22e =  default;
Int32 _itfnbz60 =  default;
Int32 _828n391q =  default;
Int32 _em7fbywm =  default;
Int32 _2twsa3rs =  default;
Int32 _k6khgoqa =  default;
Int32 _umlkckdg =  default;
Int32 _cidud698 =  default;
Int32 _psb09l5j =  default;
Int32 _4o1bt8b1 =  default;
Int32 _0veruty3 =  default;
Int32 _6vmhvjma =  default;
Int32 _gguzru7t =  default;
Int32 _rk50assb =  default;
Int32 _l7kv6hn5 =  default;
Int32 _tx1pza71 =  default;
Double _plfm7z8g =  default;
Double _irkucio3 =  default;
Double _5do7lkrf =  default;
Double _xvfwic6z =  default;
Double _tt3ji15i =  default;
Double _y61kuds7 =  default;
Double _aaaeq9ec =  default;
Double _b10nc13b =  default;
Double _iqx7r7kg =  default;
Double _i3q9kmqd =  default;
Double _864fslqq =  default;
Double _trz23puj =  default;
Double _48oov32t =  default;
Double _p1iqarg6 =  default;
Double _mu73se41 =  default;
Double _gv1d4lsf =  default;
Double _uvmwuql8 =  default;
Double _2hzs3s5k =  default;
Double _irk8i6qr =  default;
Double _h75qnr7l =  default;
Double _91a1vq5f =  default;
Double _2ivtt43r =  default;
Double _0446f4de =  default;
Double _1ajfmh55 =  default;
Double _txq1gp7u =  default;
Double _ecd0ne62 =  default;
Double _6o16fgl6 =  default;
Double _lkoza3sc =  default;
Double _g1av0bqy =  default;
Double _0q92h12q =  default;
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
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input arguments. 
		//*     (in case DLASQ2 is not called by DLASQ1) 
		//* 
		
		_gro5yvfo = (int)0;
		_p1iqarg6 = _f43eg0w0("Precision" );
		_h75qnr7l = _f43eg0w0("Safe minimum" );
		_txq1gp7u = (_p1iqarg6 * _2yce0i2m);
		_ecd0ne62 = __POW2(_txq1gp7u);//* 
		
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-1;
			_ut9qalzx("DLASQ2" ,ref Unsafe.AsRef((int)1) );
			return;
		}
		else
		if (_dxpq0xkr == (int)0)
		{
			
			return;
		}
		else
		if (_dxpq0xkr == (int)1)
		{
			//* 
			//*        1-by-1 case. 
			//* 
			
			if (*(_7e60fcso+((int)1 - 1)) < _d0547bi2)
			{
				
				_gro5yvfo = (int)-201;
				_ut9qalzx("DLASQ2" ,ref Unsafe.AsRef((int)2) );
			}
			
			return;
		}
		else
		if (_dxpq0xkr == (int)2)
		{
			//* 
			//*        2-by-2 case. 
			//* 
			
			if ((*(_7e60fcso+((int)2 - 1)) < _d0547bi2) | (*(_7e60fcso+((int)3 - 1)) < _d0547bi2))
			{
				
				_gro5yvfo = (int)-2;
				_ut9qalzx("DLASQ2" ,ref Unsafe.AsRef((int)2) );
				return;
			}
			else
			if (*(_7e60fcso+((int)3 - 1)) > *(_7e60fcso+((int)1 - 1)))
			{
				
				_plfm7z8g = *(_7e60fcso+((int)3 - 1));
				*(_7e60fcso+((int)3 - 1)) = *(_7e60fcso+((int)1 - 1));
				*(_7e60fcso+((int)1 - 1)) = _plfm7z8g;
			}
			
			*(_7e60fcso+((int)5 - 1)) = ((*(_7e60fcso+((int)1 - 1)) + *(_7e60fcso+((int)2 - 1))) + *(_7e60fcso+((int)3 - 1)));
			if (*(_7e60fcso+((int)2 - 1)) > (*(_7e60fcso+((int)3 - 1)) * _ecd0ne62))
			{
				
				_2ivtt43r = (_gbf4169i * ((*(_7e60fcso+((int)1 - 1)) - *(_7e60fcso+((int)3 - 1))) + *(_7e60fcso+((int)2 - 1))));
				_irk8i6qr = (*(_7e60fcso+((int)3 - 1)) * (*(_7e60fcso+((int)2 - 1)) / _2ivtt43r));
				if (_irk8i6qr <= _2ivtt43r)
				{
					
					_irk8i6qr = (*(_7e60fcso+((int)3 - 1)) * (*(_7e60fcso+((int)2 - 1)) / (_2ivtt43r * (_kxg5drh2 + ILNumerics.F2NET.Intrinsics.SQRT(_kxg5drh2 + (_irk8i6qr / _2ivtt43r) )))));
				}
				else
				{
					
					_irk8i6qr = (*(_7e60fcso+((int)3 - 1)) * (*(_7e60fcso+((int)2 - 1)) / (_2ivtt43r + (ILNumerics.F2NET.Intrinsics.SQRT(_2ivtt43r ) * ILNumerics.F2NET.Intrinsics.SQRT(_2ivtt43r + _irk8i6qr )))));
				}
				
				_2ivtt43r = (*(_7e60fcso+((int)1 - 1)) + (_irk8i6qr + *(_7e60fcso+((int)2 - 1))));
				*(_7e60fcso+((int)3 - 1)) = (*(_7e60fcso+((int)3 - 1)) * (*(_7e60fcso+((int)1 - 1)) / _2ivtt43r));
				*(_7e60fcso+((int)1 - 1)) = _2ivtt43r;
			}
			
			*(_7e60fcso+((int)2 - 1)) = *(_7e60fcso+((int)3 - 1));
			*(_7e60fcso+((int)6 - 1)) = (*(_7e60fcso+((int)2 - 1)) + *(_7e60fcso+((int)1 - 1)));
			return;
		}
		//* 
		//*     Check for negative data and compute sums of q's and e's. 
		//* 
		
		*(_7e60fcso+((int)2 * _dxpq0xkr - 1)) = _d0547bi2;
		_48oov32t = *(_7e60fcso+((int)2 - 1));
		_uvmwuql8 = _d0547bi2;
		_lkoza3sc = _d0547bi2;
		_plfm7z8g = _d0547bi2;
		_864fslqq = _d0547bi2;//* 
		
		{
			System.Int32 __81fgg2dlsvn301 = (System.Int32)((int)1);
			System.Int32 __81fgg2step301 = (System.Int32)((int)2);
			System.Int32 __81fgg2count301;
			for (__81fgg2count301 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)2 * (_dxpq0xkr - (int)1)) - __81fgg2dlsvn301 + __81fgg2step301) / __81fgg2step301)), _umlkckdg = __81fgg2dlsvn301; __81fgg2count301 != 0; __81fgg2count301--, _umlkckdg += (__81fgg2step301)) {

			{
				
				if (*(_7e60fcso+(_umlkckdg - 1)) < _d0547bi2)
				{
					
					_gro5yvfo = (-(((int)200 + _umlkckdg)));
					_ut9qalzx("DLASQ2" ,ref Unsafe.AsRef((int)2) );
					return;
				}
				else
				if (*(_7e60fcso+(_umlkckdg + (int)1 - 1)) < _d0547bi2)
				{
					
					_gro5yvfo = (-((((int)200 + _umlkckdg) + (int)1)));
					_ut9qalzx("DLASQ2" ,ref Unsafe.AsRef((int)2) );
					return;
				}
				
				_plfm7z8g = (_plfm7z8g + *(_7e60fcso+(_umlkckdg - 1)));
				_864fslqq = (_864fslqq + *(_7e60fcso+(_umlkckdg + (int)1 - 1)));
				_uvmwuql8 = ILNumerics.F2NET.Intrinsics.MAX(_uvmwuql8 ,*(_7e60fcso+(_umlkckdg - 1)) );
				_48oov32t = ILNumerics.F2NET.Intrinsics.MIN(_48oov32t ,*(_7e60fcso+(_umlkckdg + (int)1 - 1)) );
				_lkoza3sc = ILNumerics.F2NET.Intrinsics.MAX(_uvmwuql8 ,_lkoza3sc ,*(_7e60fcso+(_umlkckdg + (int)1 - 1)) );
Mark10:;
				// continue
			}
						}		}
		if (*(_7e60fcso+(((int)2 * _dxpq0xkr) - (int)1 - 1)) < _d0547bi2)
		{
			
			_gro5yvfo = (-((((int)200 + ((int)2 * _dxpq0xkr)) - (int)1)));
			_ut9qalzx("DLASQ2" ,ref Unsafe.AsRef((int)2) );
			return;
		}
		
		_plfm7z8g = (_plfm7z8g + *(_7e60fcso+(((int)2 * _dxpq0xkr) - (int)1 - 1)));
		_uvmwuql8 = ILNumerics.F2NET.Intrinsics.MAX(_uvmwuql8 ,*(_7e60fcso+(((int)2 * _dxpq0xkr) - (int)1 - 1)) );
		_lkoza3sc = ILNumerics.F2NET.Intrinsics.MAX(_uvmwuql8 ,_lkoza3sc );//* 
		//*     Check for diagonality. 
		//* 
		
		if (_864fslqq == _d0547bi2)
		{
			
			{
				System.Int32 __81fgg2dlsvn302 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step302 = (System.Int32)((int)1);
				System.Int32 __81fgg2count302;
				for (__81fgg2count302 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn302 + __81fgg2step302) / __81fgg2step302)), _umlkckdg = __81fgg2dlsvn302; __81fgg2count302 != 0; __81fgg2count302--, _umlkckdg += (__81fgg2step302)) {

				{
					
					*(_7e60fcso+(_umlkckdg - 1)) = *(_7e60fcso+(((int)2 * _umlkckdg) - (int)1 - 1));
Mark20:;
					// continue
				}
								}			}
			_agod5jth("D" ,ref _dxpq0xkr ,_7e60fcso ,ref _itfnbz60 );
			*(_7e60fcso+(((int)2 * _dxpq0xkr) - (int)1 - 1)) = _plfm7z8g;
			return;
		}
		//* 
		
		_6o16fgl6 = (_plfm7z8g + _864fslqq);//* 
		//*     Check for zero data. 
		//* 
		
		if (_6o16fgl6 == _d0547bi2)
		{
			
			*(_7e60fcso+(((int)2 * _dxpq0xkr) - (int)1 - 1)) = _d0547bi2;
			return;
		}
		//* 
		//*     Check whether the machine is IEEE conformable. 
		//* 
		
		_id0vp1yu = ((_4mvd6e4d(ref Unsafe.AsRef((int)10) ,"DLASQ2" ,"N" ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef((int)2) ,ref Unsafe.AsRef((int)3) ,ref Unsafe.AsRef((int)4) ) == (int)1) & (_4mvd6e4d(ref Unsafe.AsRef((int)11) ,"DLASQ2" ,"N" ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef((int)2) ,ref Unsafe.AsRef((int)3) ,ref Unsafe.AsRef((int)4) ) == (int)1));//* 
		//*     Rearrange data for locality: Z=(q1,qq1,e1,ee1,q2,qq2,e2,ee2,...). 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn303 = (System.Int32)(((int)2 * _dxpq0xkr));
			System.Int32 __81fgg2step303 = (System.Int32)((int)-2);
			System.Int32 __81fgg2count303;
			for (__81fgg2count303 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)2) - __81fgg2dlsvn303 + __81fgg2step303) / __81fgg2step303)), _umlkckdg = __81fgg2dlsvn303; __81fgg2count303 != 0; __81fgg2count303--, _umlkckdg += (__81fgg2step303)) {

			{
				
				*(_7e60fcso+((int)2 * _umlkckdg - 1)) = _d0547bi2;
				*(_7e60fcso+(((int)2 * _umlkckdg) - (int)1 - 1)) = *(_7e60fcso+(_umlkckdg - 1));
				*(_7e60fcso+(((int)2 * _umlkckdg) - (int)2 - 1)) = _d0547bi2;
				*(_7e60fcso+(((int)2 * _umlkckdg) - (int)3 - 1)) = *(_7e60fcso+(_umlkckdg - (int)1 - 1));
Mark30:;
				// continue
			}
						}		}//* 
		
		_kgliup4t = (int)1;
		_psb09l5j = _dxpq0xkr;//* 
		//*     Reverse the qd-array, if warranted. 
		//* 
		
		if ((_0tzcjs6r * *(_7e60fcso+(((int)4 * _kgliup4t) - (int)3 - 1))) < *(_7e60fcso+(((int)4 * _psb09l5j) - (int)3 - 1)))
		{
			
			_828n391q = ((int)4 * (_kgliup4t + _psb09l5j));
			{
				System.Int32 __81fgg2dlsvn304 = (System.Int32)(((int)4 * _kgliup4t));
				System.Int32 __81fgg2step304 = (System.Int32)((int)4);
				System.Int32 __81fgg2count304;
				for (__81fgg2count304 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)2 * ((_kgliup4t + _psb09l5j) - (int)1)) - __81fgg2dlsvn304 + __81fgg2step304) / __81fgg2step304)), _3xy4w22e = __81fgg2dlsvn304; __81fgg2count304 != 0; __81fgg2count304--, _3xy4w22e += (__81fgg2step304)) {

				{
					
					_1ajfmh55 = *(_7e60fcso+(_3xy4w22e - (int)3 - 1));
					*(_7e60fcso+(_3xy4w22e - (int)3 - 1)) = *(_7e60fcso+((_828n391q - _3xy4w22e) - (int)3 - 1));
					*(_7e60fcso+((_828n391q - _3xy4w22e) - (int)3 - 1)) = _1ajfmh55;
					_1ajfmh55 = *(_7e60fcso+(_3xy4w22e - (int)1 - 1));
					*(_7e60fcso+(_3xy4w22e - (int)1 - 1)) = *(_7e60fcso+((_828n391q - _3xy4w22e) - (int)5 - 1));
					*(_7e60fcso+((_828n391q - _3xy4w22e) - (int)5 - 1)) = _1ajfmh55;
Mark40:;
					// continue
				}
								}			}
		}
		//* 
		//*     Initial split checking via dqd and Li's test. 
		//* 
		
		_rk50assb = (int)0;//* 
		
		{
			System.Int32 __81fgg2dlsvn305 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step305 = (System.Int32)((int)1);
			System.Int32 __81fgg2count305;
			for (__81fgg2count305 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)2) - __81fgg2dlsvn305 + __81fgg2step305) / __81fgg2step305)), _umlkckdg = __81fgg2dlsvn305; __81fgg2count305 != 0; __81fgg2count305--, _umlkckdg += (__81fgg2step305)) {

			{
				//* 
				
				_plfm7z8g = *(_7e60fcso+((((int)4 * _psb09l5j) + _rk50assb) - (int)3 - 1));
				{
					System.Int32 __81fgg2dlsvn306 = (System.Int32)((((int)4 * (_psb09l5j - (int)1)) + _rk50assb));
					System.Int32 __81fgg2step306 = (System.Int32)((int)-4);
					System.Int32 __81fgg2count306;
					for (__81fgg2count306 = System.Math.Max(0, (System.Int32)(((System.Int32)(((int)4 * _kgliup4t) + _rk50assb) - __81fgg2dlsvn306 + __81fgg2step306) / __81fgg2step306)), _3xy4w22e = __81fgg2dlsvn306; __81fgg2count306 != 0; __81fgg2count306--, _3xy4w22e += (__81fgg2step306)) {

					{
						
						if (*(_7e60fcso+(_3xy4w22e - (int)1 - 1)) <= (_ecd0ne62 * _plfm7z8g))
						{
							
							*(_7e60fcso+(_3xy4w22e - (int)1 - 1)) = (-(_d0547bi2));
							_plfm7z8g = *(_7e60fcso+(_3xy4w22e - (int)3 - 1));
						}
						else
						{
							
							_plfm7z8g = (*(_7e60fcso+(_3xy4w22e - (int)3 - 1)) * (_plfm7z8g / (_plfm7z8g + *(_7e60fcso+(_3xy4w22e - (int)1 - 1)))));
						}
						
Mark50:;
						// continue
					}
										}				}//* 
				//*        dqd maps Z to ZZ plus Li's test. 
				//* 
				
				_48oov32t = *(_7e60fcso+((((int)4 * _kgliup4t) + _rk50assb) + (int)1 - 1));
				_plfm7z8g = *(_7e60fcso+((((int)4 * _kgliup4t) + _rk50assb) - (int)3 - 1));
				{
					System.Int32 __81fgg2dlsvn307 = (System.Int32)((((int)4 * _kgliup4t) + _rk50assb));
					System.Int32 __81fgg2step307 = (System.Int32)((int)4);
					System.Int32 __81fgg2count307;
					for (__81fgg2count307 = System.Math.Max(0, (System.Int32)(((System.Int32)(((int)4 * (_psb09l5j - (int)1)) + _rk50assb) - __81fgg2dlsvn307 + __81fgg2step307) / __81fgg2step307)), _3xy4w22e = __81fgg2dlsvn307; __81fgg2count307 != 0; __81fgg2count307--, _3xy4w22e += (__81fgg2step307)) {

					{
						
						*(_7e60fcso+((_3xy4w22e - ((int)2 * _rk50assb)) - (int)2 - 1)) = (_plfm7z8g + *(_7e60fcso+(_3xy4w22e - (int)1 - 1)));
						if (*(_7e60fcso+(_3xy4w22e - (int)1 - 1)) <= (_ecd0ne62 * _plfm7z8g))
						{
							
							*(_7e60fcso+(_3xy4w22e - (int)1 - 1)) = (-(_d0547bi2));
							*(_7e60fcso+((_3xy4w22e - ((int)2 * _rk50assb)) - (int)2 - 1)) = _plfm7z8g;
							*(_7e60fcso+(_3xy4w22e - ((int)2 * _rk50assb) - 1)) = _d0547bi2;
							_plfm7z8g = *(_7e60fcso+(_3xy4w22e + (int)1 - 1));
						}
						else
						if (((_h75qnr7l * *(_7e60fcso+(_3xy4w22e + (int)1 - 1))) < *(_7e60fcso+((_3xy4w22e - ((int)2 * _rk50assb)) - (int)2 - 1))) & ((_h75qnr7l * *(_7e60fcso+((_3xy4w22e - ((int)2 * _rk50assb)) - (int)2 - 1))) < *(_7e60fcso+(_3xy4w22e + (int)1 - 1))))
						{
							
							_1ajfmh55 = (*(_7e60fcso+(_3xy4w22e + (int)1 - 1)) / *(_7e60fcso+((_3xy4w22e - ((int)2 * _rk50assb)) - (int)2 - 1)));
							*(_7e60fcso+(_3xy4w22e - ((int)2 * _rk50assb) - 1)) = (*(_7e60fcso+(_3xy4w22e - (int)1 - 1)) * _1ajfmh55);
							_plfm7z8g = (_plfm7z8g * _1ajfmh55);
						}
						else
						{
							
							*(_7e60fcso+(_3xy4w22e - ((int)2 * _rk50assb) - 1)) = (*(_7e60fcso+(_3xy4w22e + (int)1 - 1)) * (*(_7e60fcso+(_3xy4w22e - (int)1 - 1)) / *(_7e60fcso+((_3xy4w22e - ((int)2 * _rk50assb)) - (int)2 - 1))));
							_plfm7z8g = (*(_7e60fcso+(_3xy4w22e + (int)1 - 1)) * (_plfm7z8g / *(_7e60fcso+((_3xy4w22e - ((int)2 * _rk50assb)) - (int)2 - 1))));
						}
						
						_48oov32t = ILNumerics.F2NET.Intrinsics.MIN(_48oov32t ,*(_7e60fcso+(_3xy4w22e - ((int)2 * _rk50assb) - 1)) );
Mark60:;
						// continue
					}
										}				}
				*(_7e60fcso+((((int)4 * _psb09l5j) - _rk50assb) - (int)2 - 1)) = _plfm7z8g;//* 
				//*        Now find qmax. 
				//* 
				
				_uvmwuql8 = *(_7e60fcso+((((int)4 * _kgliup4t) - _rk50assb) - (int)2 - 1));
				{
					System.Int32 __81fgg2dlsvn308 = (System.Int32)(((((int)4 * _kgliup4t) - _rk50assb) + (int)2));
					System.Int32 __81fgg2step308 = (System.Int32)((int)4);
					System.Int32 __81fgg2count308;
					for (__81fgg2count308 = System.Math.Max(0, (System.Int32)(((System.Int32)((((int)4 * _psb09l5j) - _rk50assb) - (int)2) - __81fgg2dlsvn308 + __81fgg2step308) / __81fgg2step308)), _3xy4w22e = __81fgg2dlsvn308; __81fgg2count308 != 0; __81fgg2count308--, _3xy4w22e += (__81fgg2step308)) {

					{
						
						_uvmwuql8 = ILNumerics.F2NET.Intrinsics.MAX(_uvmwuql8 ,*(_7e60fcso+(_3xy4w22e - 1)) );
Mark70:;
						// continue
					}
										}				}//* 
				//*        Prepare for the next iteration on K. 
				//* 
				
				_rk50assb = ((int)1 - _rk50assb);
Mark80:;
				// continue
			}
						}		}//* 
		//*     Initialise variables to pass to DLASQ3. 
		//* 
		
		_tx1pza71 = (int)0;
		_y61kuds7 = _d0547bi2;
		_aaaeq9ec = _d0547bi2;
		_b10nc13b = _d0547bi2;
		_iqx7r7kg = _d0547bi2;
		_i3q9kmqd = _d0547bi2;
		_mu73se41 = _d0547bi2;
		_0446f4de = _d0547bi2;//* 
		
		_em7fbywm = (int)2;
		_gguzru7t = (int)0;
		_6vmhvjma = ((int)2 * (_psb09l5j - _kgliup4t));//* 
		
		{
			System.Int32 __81fgg2dlsvn309 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step309 = (System.Int32)((int)1);
			System.Int32 __81fgg2count309;
			for (__81fgg2count309 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr + (int)1) - __81fgg2dlsvn309 + __81fgg2step309) / __81fgg2step309)), _2twsa3rs = __81fgg2dlsvn309; __81fgg2count309 != 0; __81fgg2count309--, _2twsa3rs += (__81fgg2step309)) {

			{
				
				if (_psb09l5j < (int)1)goto Mark170;//* 
				//*        While array unfinished do 
				//* 
				//*        E(N0) holds the value of SIGMA when submatrix in I0:N0 
				//*        splits from the rest of the array, but is negated. 
				//* 
				
				_xvfwic6z = _d0547bi2;
				if (_psb09l5j == _dxpq0xkr)
				{
					
					_91a1vq5f = _d0547bi2;
				}
				else
				{
					
					_91a1vq5f = (-(*(_7e60fcso+(((int)4 * _psb09l5j) - (int)1 - 1))));
				}
				
				if (_91a1vq5f < _d0547bi2)
				{
					
					_gro5yvfo = (int)1;
					return;
				}
				//* 
				//*        Find last unreduced submatrix's top index I0, find QMAX and 
				//*        EMIN. Find Gershgorin-type bound if Q's much greater than E's. 
				//* 
				
				_trz23puj = _d0547bi2;
				if (_psb09l5j > _kgliup4t)
				{
					
					_48oov32t = ILNumerics.F2NET.Intrinsics.ABS(*(_7e60fcso+(((int)4 * _psb09l5j) - (int)5 - 1)) );
				}
				else
				{
					
					_48oov32t = _d0547bi2;
				}
				
				_2hzs3s5k = *(_7e60fcso+(((int)4 * _psb09l5j) - (int)3 - 1));
				_uvmwuql8 = _2hzs3s5k;
				{
					System.Int32 __81fgg2dlsvn310 = (System.Int32)(((int)4 * _psb09l5j));
					System.Int32 __81fgg2step310 = (System.Int32)((int)-4);
					System.Int32 __81fgg2count310;
					for (__81fgg2count310 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)8) - __81fgg2dlsvn310 + __81fgg2step310) / __81fgg2step310)), _3xy4w22e = __81fgg2dlsvn310; __81fgg2count310 != 0; __81fgg2count310--, _3xy4w22e += (__81fgg2step310)) {

					{
						
						if (*(_7e60fcso+(_3xy4w22e - (int)5 - 1)) <= _d0547bi2)goto Mark100;
						if (_2hzs3s5k >= (_ax5ijvbx * _trz23puj))
						{
							
							_2hzs3s5k = ILNumerics.F2NET.Intrinsics.MIN(_2hzs3s5k ,*(_7e60fcso+(_3xy4w22e - (int)3 - 1)) );
							_trz23puj = ILNumerics.F2NET.Intrinsics.MAX(_trz23puj ,*(_7e60fcso+(_3xy4w22e - (int)5 - 1)) );
						}
						
						_uvmwuql8 = ILNumerics.F2NET.Intrinsics.MAX(_uvmwuql8 ,*(_7e60fcso+(_3xy4w22e - (int)7 - 1)) + *(_7e60fcso+(_3xy4w22e - (int)5 - 1)) );
						_48oov32t = ILNumerics.F2NET.Intrinsics.MIN(_48oov32t ,*(_7e60fcso+(_3xy4w22e - (int)5 - 1)) );
Mark90:;
						// continue
					}
										}				}
				_3xy4w22e = (int)4;//* 
				
Mark100:;
				// continue
				_kgliup4t = (_3xy4w22e / (int)4);
				_rk50assb = (int)0;//* 
				
				if ((_psb09l5j - _kgliup4t) > (int)1)
				{
					
					_irkucio3 = *(_7e60fcso+(((int)4 * _kgliup4t) - (int)3 - 1));
					_5do7lkrf = _irkucio3;
					_cidud698 = _kgliup4t;
					{
						System.Int32 __81fgg2dlsvn311 = (System.Int32)((((int)4 * _kgliup4t) + (int)1));
						System.Int32 __81fgg2step311 = (System.Int32)((int)4);
						System.Int32 __81fgg2count311;
						for (__81fgg2count311 = System.Math.Max(0, (System.Int32)(((System.Int32)(((int)4 * _psb09l5j) - (int)3) - __81fgg2dlsvn311 + __81fgg2step311) / __81fgg2step311)), _3xy4w22e = __81fgg2dlsvn311; __81fgg2count311 != 0; __81fgg2count311--, _3xy4w22e += (__81fgg2step311)) {

						{
							
							_irkucio3 = (*(_7e60fcso+(_3xy4w22e - 1)) * (_irkucio3 / (_irkucio3 + *(_7e60fcso+(_3xy4w22e - (int)2 - 1)))));
							if (_irkucio3 <= _5do7lkrf)
							{
								
								_5do7lkrf = _irkucio3;
								_cidud698 = ((_3xy4w22e + (int)3) / (int)4);
							}
							
Mark110:;
							// continue
						}
												}					}
					if ((((_cidud698 - _kgliup4t) * (int)2) < (_psb09l5j - _cidud698)) & (_5do7lkrf <= (_gbf4169i * *(_7e60fcso+(((int)4 * _psb09l5j) - (int)3 - 1)))))
					{
						
						_828n391q = ((int)4 * (_kgliup4t + _psb09l5j));
						_rk50assb = (int)2;
						{
							System.Int32 __81fgg2dlsvn312 = (System.Int32)(((int)4 * _kgliup4t));
							System.Int32 __81fgg2step312 = (System.Int32)((int)4);
							System.Int32 __81fgg2count312;
							for (__81fgg2count312 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)2 * ((_kgliup4t + _psb09l5j) - (int)1)) - __81fgg2dlsvn312 + __81fgg2step312) / __81fgg2step312)), _3xy4w22e = __81fgg2dlsvn312; __81fgg2count312 != 0; __81fgg2count312--, _3xy4w22e += (__81fgg2step312)) {

							{
								
								_1ajfmh55 = *(_7e60fcso+(_3xy4w22e - (int)3 - 1));
								*(_7e60fcso+(_3xy4w22e - (int)3 - 1)) = *(_7e60fcso+((_828n391q - _3xy4w22e) - (int)3 - 1));
								*(_7e60fcso+((_828n391q - _3xy4w22e) - (int)3 - 1)) = _1ajfmh55;
								_1ajfmh55 = *(_7e60fcso+(_3xy4w22e - (int)2 - 1));
								*(_7e60fcso+(_3xy4w22e - (int)2 - 1)) = *(_7e60fcso+((_828n391q - _3xy4w22e) - (int)2 - 1));
								*(_7e60fcso+((_828n391q - _3xy4w22e) - (int)2 - 1)) = _1ajfmh55;
								_1ajfmh55 = *(_7e60fcso+(_3xy4w22e - (int)1 - 1));
								*(_7e60fcso+(_3xy4w22e - (int)1 - 1)) = *(_7e60fcso+((_828n391q - _3xy4w22e) - (int)5 - 1));
								*(_7e60fcso+((_828n391q - _3xy4w22e) - (int)5 - 1)) = _1ajfmh55;
								_1ajfmh55 = *(_7e60fcso+(_3xy4w22e - 1));
								*(_7e60fcso+(_3xy4w22e - 1)) = *(_7e60fcso+((_828n391q - _3xy4w22e) - (int)4 - 1));
								*(_7e60fcso+((_828n391q - _3xy4w22e) - (int)4 - 1)) = _1ajfmh55;
Mark120:;
								// continue
							}
														}						}
					}
					
				}
				//* 
				//*        Put -(initial shift) into DMIN. 
				//* 
				
				_tt3ji15i = (-(ILNumerics.F2NET.Intrinsics.MAX(_d0547bi2 ,_2hzs3s5k - ((_5m0mjfxm * ILNumerics.F2NET.Intrinsics.SQRT(_2hzs3s5k )) * ILNumerics.F2NET.Intrinsics.SQRT(_trz23puj )) )));//* 
				//*        Now I0:N0 is unreduced. 
				//*        PP = 0 for ping, PP = 1 for pong. 
				//*        PP = 2 indicates that flipping was applied to the Z array and 
				//*               and that the tests for deflation upon entry in DLASQ3 
				//*               should not be performed. 
				//* 
				
				_0veruty3 = ((int)100 * ((_psb09l5j - _kgliup4t) + (int)1));
				{
					System.Int32 __81fgg2dlsvn313 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step313 = (System.Int32)((int)1);
					System.Int32 __81fgg2count313;
					for (__81fgg2count313 = System.Math.Max(0, (System.Int32)(((System.Int32)(_0veruty3) - __81fgg2dlsvn313 + __81fgg2step313) / __81fgg2step313)), _k6khgoqa = __81fgg2dlsvn313; __81fgg2count313 != 0; __81fgg2count313--, _k6khgoqa += (__81fgg2step313)) {

					{
						
						if (_kgliup4t > _psb09l5j)goto Mark150;//* 
						//*           While submatrix unfinished take a good dqds step. 
						//* 
						
						_yt0gzo2v(ref _kgliup4t ,ref _psb09l5j ,_7e60fcso ,ref _rk50assb ,ref _tt3ji15i ,ref _91a1vq5f ,ref _xvfwic6z ,ref _uvmwuql8 ,ref _gguzru7t ,ref _em7fbywm ,ref _6vmhvjma ,ref _id0vp1yu ,ref _tx1pza71 ,ref _y61kuds7 ,ref _aaaeq9ec ,ref _b10nc13b ,ref _iqx7r7kg ,ref _i3q9kmqd ,ref _mu73se41 ,ref _0446f4de );//* 
						
						_rk50assb = ((int)1 - _rk50assb);//* 
						//*           When EMIN is very small check for splits. 
						//* 
						
						if ((_rk50assb == (int)0) & ((_psb09l5j - _kgliup4t) >= (int)3))
						{
							
							if ((*(_7e60fcso+((int)4 * _psb09l5j - 1)) <= (_ecd0ne62 * _uvmwuql8)) | (*(_7e60fcso+(((int)4 * _psb09l5j) - (int)1 - 1)) <= (_ecd0ne62 * _91a1vq5f)))
							{
								
								_l7kv6hn5 = (_kgliup4t - (int)1);
								_uvmwuql8 = *(_7e60fcso+(((int)4 * _kgliup4t) - (int)3 - 1));
								_48oov32t = *(_7e60fcso+(((int)4 * _kgliup4t) - (int)1 - 1));
								_gv1d4lsf = *(_7e60fcso+((int)4 * _kgliup4t - 1));
								{
									System.Int32 __81fgg2dlsvn314 = (System.Int32)(((int)4 * _kgliup4t));
									System.Int32 __81fgg2step314 = (System.Int32)((int)4);
									System.Int32 __81fgg2count314;
									for (__81fgg2count314 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)4 * (_psb09l5j - (int)3)) - __81fgg2dlsvn314 + __81fgg2step314) / __81fgg2step314)), _3xy4w22e = __81fgg2dlsvn314; __81fgg2count314 != 0; __81fgg2count314--, _3xy4w22e += (__81fgg2step314)) {

									{
										
										if ((*(_7e60fcso+(_3xy4w22e - 1)) <= (_ecd0ne62 * *(_7e60fcso+(_3xy4w22e - (int)3 - 1)))) | (*(_7e60fcso+(_3xy4w22e - (int)1 - 1)) <= (_ecd0ne62 * _91a1vq5f)))
										{
											
											*(_7e60fcso+(_3xy4w22e - (int)1 - 1)) = (-(_91a1vq5f));
											_l7kv6hn5 = (_3xy4w22e / (int)4);
											_uvmwuql8 = _d0547bi2;
											_48oov32t = *(_7e60fcso+(_3xy4w22e + (int)3 - 1));
											_gv1d4lsf = *(_7e60fcso+(_3xy4w22e + (int)4 - 1));
										}
										else
										{
											
											_uvmwuql8 = ILNumerics.F2NET.Intrinsics.MAX(_uvmwuql8 ,*(_7e60fcso+(_3xy4w22e + (int)1 - 1)) );
											_48oov32t = ILNumerics.F2NET.Intrinsics.MIN(_48oov32t ,*(_7e60fcso+(_3xy4w22e - (int)1 - 1)) );
											_gv1d4lsf = ILNumerics.F2NET.Intrinsics.MIN(_gv1d4lsf ,*(_7e60fcso+(_3xy4w22e - 1)) );
										}
										
Mark130:;
										// continue
									}
																		}								}
								*(_7e60fcso+(((int)4 * _psb09l5j) - (int)1 - 1)) = _48oov32t;
								*(_7e60fcso+((int)4 * _psb09l5j - 1)) = _gv1d4lsf;
								_kgliup4t = (_l7kv6hn5 + (int)1);
							}
							
						}
						//* 
						
Mark140:;
						// continue
					}
										}				}//* 
				
				_gro5yvfo = (int)2;//* 
				//*        Maximum number of iterations exceeded, restore the shift 
				//*        SIGMA and place the new d's and e's in a qd array. 
				//*        This might need to be done for several blocks 
				//* 
				
				_egqdmelt = _kgliup4t;
				_4o1bt8b1 = _psb09l5j;
Mark145:;
				// continue
				_0q92h12q = *(_7e60fcso+(((int)4 * _kgliup4t) - (int)3 - 1));
				*(_7e60fcso+(((int)4 * _kgliup4t) - (int)3 - 1)) = (*(_7e60fcso+(((int)4 * _kgliup4t) - (int)3 - 1)) + _91a1vq5f);
				{
					System.Int32 __81fgg2dlsvn315 = (System.Int32)((_kgliup4t + (int)1));
					const System.Int32 __81fgg2step315 = (System.Int32)((int)1);
					System.Int32 __81fgg2count315;
					for (__81fgg2count315 = System.Math.Max(0, (System.Int32)(((System.Int32)(_psb09l5j) - __81fgg2dlsvn315 + __81fgg2step315) / __81fgg2step315)), _umlkckdg = __81fgg2dlsvn315; __81fgg2count315 != 0; __81fgg2count315--, _umlkckdg += (__81fgg2step315)) {

					{
						
						_g1av0bqy = *(_7e60fcso+(((int)4 * _umlkckdg) - (int)5 - 1));
						*(_7e60fcso+(((int)4 * _umlkckdg) - (int)5 - 1)) = (*(_7e60fcso+(((int)4 * _umlkckdg) - (int)5 - 1)) * (_0q92h12q / *(_7e60fcso+(((int)4 * _umlkckdg) - (int)7 - 1))));
						_0q92h12q = *(_7e60fcso+(((int)4 * _umlkckdg) - (int)3 - 1));
						*(_7e60fcso+(((int)4 * _umlkckdg) - (int)3 - 1)) = (((*(_7e60fcso+(((int)4 * _umlkckdg) - (int)3 - 1)) + _91a1vq5f) + _g1av0bqy) - *(_7e60fcso+(((int)4 * _umlkckdg) - (int)5 - 1)));
					}
										}				}//* 
				//*        Prepare to do this on the previous block if there is one 
				//* 
				
				if (_egqdmelt > (int)1)
				{
					
					_4o1bt8b1 = (_egqdmelt - (int)1);
					{
while (((_egqdmelt >= (int)2) & (*(_7e60fcso+(((int)4 * _egqdmelt) - (int)5 - 1)) >= _d0547bi2))) {
						{
							
							_egqdmelt = (_egqdmelt - (int)1);
						}
												}					}
					_91a1vq5f = (-(*(_7e60fcso+(((int)4 * _4o1bt8b1) - (int)1 - 1))));goto Mark145;
				}
				// 
				
				{
					System.Int32 __81fgg2dlsvn316 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step316 = (System.Int32)((int)1);
					System.Int32 __81fgg2count316;
					for (__81fgg2count316 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn316 + __81fgg2step316) / __81fgg2step316)), _umlkckdg = __81fgg2dlsvn316; __81fgg2count316 != 0; __81fgg2count316--, _umlkckdg += (__81fgg2step316)) {

					{
						
						*(_7e60fcso+(((int)2 * _umlkckdg) - (int)1 - 1)) = *(_7e60fcso+(((int)4 * _umlkckdg) - (int)3 - 1));//* 
						//*        Only the block 1..N0 is unfinished.  The rest of the e's 
						//*        must be essentially zero, although sometimes other data 
						//*        has been stored in them. 
						//* 
						
						if (_umlkckdg < _psb09l5j)
						{
							
							*(_7e60fcso+((int)2 * _umlkckdg - 1)) = *(_7e60fcso+(((int)4 * _umlkckdg) - (int)1 - 1));
						}
						else
						{
							
							*(_7e60fcso+((int)2 * _umlkckdg - 1)) = DBLE((int)0);
						}
						
					}
										}				}
				return;//* 
				//*        end IWHILB 
				//* 
				
Mark150:;
				// continue//* 
				
Mark160:;
				// continue
			}
						}		}//* 
		
		_gro5yvfo = (int)3;
		return;//* 
		//*     end IWHILA 
		//* 
		
Mark170:;
		// continue//* 
		//*     Move q's to the front. 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn317 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step317 = (System.Int32)((int)1);
			System.Int32 __81fgg2count317;
			for (__81fgg2count317 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn317 + __81fgg2step317) / __81fgg2step317)), _umlkckdg = __81fgg2dlsvn317; __81fgg2count317 != 0; __81fgg2count317--, _umlkckdg += (__81fgg2step317)) {

			{
				
				*(_7e60fcso+(_umlkckdg - 1)) = *(_7e60fcso+(((int)4 * _umlkckdg) - (int)3 - 1));
Mark180:;
				// continue
			}
						}		}//* 
		//*     Sort and compute sum of eigenvalues. 
		//* 
		
		_agod5jth("D" ,ref _dxpq0xkr ,_7e60fcso ,ref _itfnbz60 );//* 
		
		_864fslqq = _d0547bi2;
		{
			System.Int32 __81fgg2dlsvn318 = (System.Int32)(_dxpq0xkr);
			System.Int32 __81fgg2step318 = (System.Int32)((int)-1);
			System.Int32 __81fgg2count318;
			for (__81fgg2count318 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn318 + __81fgg2step318) / __81fgg2step318)), _umlkckdg = __81fgg2dlsvn318; __81fgg2count318 != 0; __81fgg2count318--, _umlkckdg += (__81fgg2step318)) {

			{
				
				_864fslqq = (_864fslqq + *(_7e60fcso+(_umlkckdg - 1)));
Mark190:;
				// continue
			}
						}		}//* 
		//*     Store trace, sum(eigenvalues) and information on performance. 
		//* 
		
		*(_7e60fcso+(((int)2 * _dxpq0xkr) + (int)1 - 1)) = _6o16fgl6;
		*(_7e60fcso+(((int)2 * _dxpq0xkr) + (int)2 - 1)) = _864fslqq;
		*(_7e60fcso+(((int)2 * _dxpq0xkr) + (int)3 - 1)) = ILNumerics.F2NET.Intrinsics.DBLE(_em7fbywm );
		*(_7e60fcso+(((int)2 * _dxpq0xkr) + (int)4 - 1)) = (ILNumerics.F2NET.Intrinsics.DBLE(_6vmhvjma ) / ILNumerics.F2NET.Intrinsics.DBLE(__POW2(_dxpq0xkr) ));
		*(_7e60fcso+(((int)2 * _dxpq0xkr) + (int)5 - 1)) = ((_2yce0i2m * _gguzru7t) / ILNumerics.F2NET.Intrinsics.DBLE(_em7fbywm ));
		return;//* 
		//*     End of DLASQ2 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
