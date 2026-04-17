
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
//*> \brief \b ZGEMV 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZGEMV(TRANS,M,N,ALPHA,A,LDA,X,INCX,BETA,Y,INCY) 
//* 
//*       .. Scalar Arguments .. 
//*       COMPLEX*16 ALPHA,BETA 
//*       INTEGER INCX,INCY,LDA,M,N 
//*       CHARACTER TRANS 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX*16 A(LDA,*),X(*),Y(*) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZGEMV  performs one of the matrix-vector operations 
//*> 
//*>    y := alpha*A*x + beta*y,   or   y := alpha*A**T*x + beta*y,   or 
//*> 
//*>    y := alpha*A**H*x + beta*y, 
//*> 
//*> where alpha and beta are scalars, x and y are vectors and A is an 
//*> m by n matrix. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] TRANS 
//*> \verbatim 
//*>          TRANS is CHARACTER*1 
//*>           On entry, TRANS specifies the operation to be performed as 
//*>           follows: 
//*> 
//*>              TRANS = 'N' or 'n'   y := alpha*A*x + beta*y. 
//*> 
//*>              TRANS = 'T' or 't'   y := alpha*A**T*x + beta*y. 
//*> 
//*>              TRANS = 'C' or 'c'   y := alpha*A**H*x + beta*y. 
//*> \endverbatim 
//*> 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>           On entry, M specifies the number of rows of the matrix A. 
//*>           M must be at least zero. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>           On entry, N specifies the number of columns of the matrix A. 
//*>           N must be at least zero. 
//*> \endverbatim 
//*> 
//*> \param[in] ALPHA 
//*> \verbatim 
//*>          ALPHA is COMPLEX*16 
//*>           On entry, ALPHA specifies the scalar alpha. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension ( LDA, N ) 
//*>           Before entry, the leading m by n part of the array A must 
//*>           contain the matrix of coefficients. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>           On entry, LDA specifies the first dimension of A as declared 
//*>           in the calling (sub) program. LDA must be at least 
//*>           max( 1, m ). 
//*> \endverbatim 
//*> 
//*> \param[in] X 
//*> \verbatim 
//*>          X is COMPLEX*16 array, dimension at least 
//*>           ( 1 + ( n - 1 )*abs( INCX ) ) when TRANS = 'N' or 'n' 
//*>           and at least 
//*>           ( 1 + ( m - 1 )*abs( INCX ) ) otherwise. 
//*>           Before entry, the incremented array X must contain the 
//*>           vector x. 
//*> \endverbatim 
//*> 
//*> \param[in] INCX 
//*> \verbatim 
//*>          INCX is INTEGER 
//*>           On entry, INCX specifies the increment for the elements of 
//*>           X. INCX must not be zero. 
//*> \endverbatim 
//*> 
//*> \param[in] BETA 
//*> \verbatim 
//*>          BETA is COMPLEX*16 
//*>           On entry, BETA specifies the scalar beta. When BETA is 
//*>           supplied as zero then Y need not be set on input. 
//*> \endverbatim 
//*> 
//*> \param[in,out] Y 
//*> \verbatim 
//*>          Y is COMPLEX*16 array, dimension at least 
//*>           ( 1 + ( m - 1 )*abs( INCY ) ) when TRANS = 'N' or 'n' 
//*>           and at least 
//*>           ( 1 + ( n - 1 )*abs( INCY ) ) otherwise. 
//*>           Before entry with BETA non-zero, the incremented array Y 
//*>           must contain the vector y. On exit, Y is overwritten by the 
//*>           updated vector y. 
//*> \endverbatim 
//*> 
//*> \param[in] INCY 
//*> \verbatim 
//*>          INCY is INTEGER 
//*>           On entry, INCY specifies the increment for the elements of 
//*>           Y. INCY must not be zero. 
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
//*> \ingroup complex16_blas_level2 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  Level 2 Blas routine. 
//*>  The vector and matrix arguments are not referenced when N = 0, or M = 0 
//*> 
//*>  -- Written on 22-October-1986. 
//*>     Jack Dongarra, Argonne National Lab. 
//*>     Jeremy Du Croz, Nag Central Office. 
//*>     Sven Hammarling, Nag Central Office. 
//*>     Richard Hanson, Sandia National Labs. 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _xfaqgfxk(FString _scuo79v4, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, ref complex _r7cfteg3, complex* _vxfgpup9, ref Int32 _ocv8fk5c, complex* _ta7zuy9k, ref Int32 _1eqjusqc, ref complex _bafcbx97, complex* _f3z3edv0, ref Int32 _bbrxgmj7)
	{
#region variable declarations
complex _kxg5drh2 =   new fcomplex(1f,0f);
complex _d0547bi2 =   new fcomplex(0f,0f);
complex _1ajfmh55 =  default;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _b69ritwm =  default;
Int32 _821h5yui =  default;
Int32 _znpjgsef =  default;
Int32 _m3loivrh =  default;
Int32 _lwc63p7q =  default;
Int32 _ziknm33t =  default;
Int32 _ylb1uqzt =  default;
Int32 _k2902mav =  default;
Int32 _cf0mki7c =  default;
Boolean _moml4lap =  default;
string fLanavab = default;
#endregion  variable declarations
_scuo79v4 = _scuo79v4.Convert(1);

	{
		//* 
		//*  -- Reference BLAS level2 routine (version 3.7.0) -- 
		//*  -- Reference BLAS is a software package provided by Univ. of Tennessee,    -- 
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
		//* 
		//*     Test the input parameters. 
		//* 
		
		_gro5yvfo = (int)0;
		if (((!(_w8y2rzgy(_scuo79v4 ,"N" ))) & (!(_w8y2rzgy(_scuo79v4 ,"T" )))) & (!(_w8y2rzgy(_scuo79v4 ,"C" ))))
		{
			
			_gro5yvfo = (int)1;
		}
		else
		if (_ev4xhht5 < (int)0)
		{
			
			_gro5yvfo = (int)2;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)3;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ))
		{
			
			_gro5yvfo = (int)6;
		}
		else
		if (_1eqjusqc == (int)0)
		{
			
			_gro5yvfo = (int)8;
		}
		else
		if (_bbrxgmj7 == (int)0)
		{
			
			_gro5yvfo = (int)11;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("ZGEMV " ,ref _gro5yvfo );
			return;
		}
		//* 
		//*     Quick return if possible. 
		//* 
		
		if (((_ev4xhht5 == (int)0) | (_dxpq0xkr == (int)0)) | ((_r7cfteg3 == _d0547bi2) & (_bafcbx97 == _kxg5drh2)))
		return;//* 
		
		_moml4lap = _w8y2rzgy(_scuo79v4 ,"T" );//* 
		//*     Set  LENX  and  LENY, the lengths of the vectors x and y, and set 
		//*     up the start points in  X  and  Y. 
		//* 
		
		if (_w8y2rzgy(_scuo79v4 ,"N" ))
		{
			
			_k2902mav = _dxpq0xkr;
			_cf0mki7c = _ev4xhht5;
		}
		else
		{
			
			_k2902mav = _ev4xhht5;
			_cf0mki7c = _dxpq0xkr;
		}
		
		if (_1eqjusqc > (int)0)
		{
			
			_ziknm33t = (int)1;
		}
		else
		{
			
			_ziknm33t = ((int)1 - ((_k2902mav - (int)1) * _1eqjusqc));
		}
		
		if (_bbrxgmj7 > (int)0)
		{
			
			_ylb1uqzt = (int)1;
		}
		else
		{
			
			_ylb1uqzt = ((int)1 - ((_cf0mki7c - (int)1) * _bbrxgmj7));
		}
		//* 
		//*     Start the operations. In this version the elements of A are 
		//*     accessed sequentially with one pass through A. 
		//* 
		//*     First form  y := beta*y. 
		//* 
		
		if (_bafcbx97 != _kxg5drh2)
		{
			
			if (_bbrxgmj7 == (int)1)
			{
				
				if (_bafcbx97 == _d0547bi2)
				{
					
					{
						System.Int32 __81fgg2dlsvn1142 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1142 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1142;
						for (__81fgg2count1142 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cf0mki7c) - __81fgg2dlsvn1142 + __81fgg2step1142) / __81fgg2step1142)), _b5p6od9s = __81fgg2dlsvn1142; __81fgg2count1142 != 0; __81fgg2count1142--, _b5p6od9s += (__81fgg2step1142)) {

						{
							
							*(_f3z3edv0+(_b5p6od9s - 1)) = _d0547bi2;
Mark10:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn1143 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1143 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1143;
						for (__81fgg2count1143 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cf0mki7c) - __81fgg2dlsvn1143 + __81fgg2step1143) / __81fgg2step1143)), _b5p6od9s = __81fgg2dlsvn1143; __81fgg2count1143 != 0; __81fgg2count1143--, _b5p6od9s += (__81fgg2step1143)) {

						{
							
							*(_f3z3edv0+(_b5p6od9s - 1)) = (_bafcbx97 * *(_f3z3edv0+(_b5p6od9s - 1)));
Mark20:;
							// continue
						}
												}					}
				}
				
			}
			else
			{
				
				_821h5yui = _ylb1uqzt;
				if (_bafcbx97 == _d0547bi2)
				{
					
					{
						System.Int32 __81fgg2dlsvn1144 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1144 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1144;
						for (__81fgg2count1144 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cf0mki7c) - __81fgg2dlsvn1144 + __81fgg2step1144) / __81fgg2step1144)), _b5p6od9s = __81fgg2dlsvn1144; __81fgg2count1144 != 0; __81fgg2count1144--, _b5p6od9s += (__81fgg2step1144)) {

						{
							
							*(_f3z3edv0+(_821h5yui - 1)) = _d0547bi2;
							_821h5yui = (_821h5yui + _bbrxgmj7);
Mark30:;
							// continue
						}
												}					}
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn1145 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1145 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1145;
						for (__81fgg2count1145 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cf0mki7c) - __81fgg2dlsvn1145 + __81fgg2step1145) / __81fgg2step1145)), _b5p6od9s = __81fgg2dlsvn1145; __81fgg2count1145 != 0; __81fgg2count1145--, _b5p6od9s += (__81fgg2step1145)) {

						{
							
							*(_f3z3edv0+(_821h5yui - 1)) = (_bafcbx97 * *(_f3z3edv0+(_821h5yui - 1)));
							_821h5yui = (_821h5yui + _bbrxgmj7);
Mark40:;
							// continue
						}
												}					}
				}
				
			}
			
		}
		
		if (_r7cfteg3 == _d0547bi2)
		return;
		if (_w8y2rzgy(_scuo79v4 ,"N" ))
		{
			//* 
			//*        Form  y := alpha*A*x + y. 
			//* 
			
			_m3loivrh = _ziknm33t;
			if (_bbrxgmj7 == (int)1)
			{
				
				{
					System.Int32 __81fgg2dlsvn1146 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1146 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1146;
					for (__81fgg2count1146 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1146 + __81fgg2step1146) / __81fgg2step1146)), _znpjgsef = __81fgg2dlsvn1146; __81fgg2count1146 != 0; __81fgg2count1146--, _znpjgsef += (__81fgg2step1146)) {

					{
						
						_1ajfmh55 = (_r7cfteg3 * *(_ta7zuy9k+(_m3loivrh - 1)));
						{
							System.Int32 __81fgg2dlsvn1147 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1147 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1147;
							for (__81fgg2count1147 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1147 + __81fgg2step1147) / __81fgg2step1147)), _b5p6od9s = __81fgg2dlsvn1147; __81fgg2count1147 != 0; __81fgg2count1147--, _b5p6od9s += (__81fgg2step1147)) {

							{
								
								*(_f3z3edv0+(_b5p6od9s - 1)) = (*(_f3z3edv0+(_b5p6od9s - 1)) + (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
Mark50:;
								// continue
							}
														}						}
						_m3loivrh = (_m3loivrh + _1eqjusqc);
Mark60:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn1148 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1148 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1148;
					for (__81fgg2count1148 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1148 + __81fgg2step1148) / __81fgg2step1148)), _znpjgsef = __81fgg2dlsvn1148; __81fgg2count1148 != 0; __81fgg2count1148--, _znpjgsef += (__81fgg2step1148)) {

					{
						
						_1ajfmh55 = (_r7cfteg3 * *(_ta7zuy9k+(_m3loivrh - 1)));
						_821h5yui = _ylb1uqzt;
						{
							System.Int32 __81fgg2dlsvn1149 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step1149 = (System.Int32)((int)1);
							System.Int32 __81fgg2count1149;
							for (__81fgg2count1149 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1149 + __81fgg2step1149) / __81fgg2step1149)), _b5p6od9s = __81fgg2dlsvn1149; __81fgg2count1149 != 0; __81fgg2count1149--, _b5p6od9s += (__81fgg2step1149)) {

							{
								
								*(_f3z3edv0+(_821h5yui - 1)) = (*(_f3z3edv0+(_821h5yui - 1)) + (_1ajfmh55 * *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
								_821h5yui = (_821h5yui + _bbrxgmj7);
Mark70:;
								// continue
							}
														}						}
						_m3loivrh = (_m3loivrh + _1eqjusqc);
Mark80:;
						// continue
					}
										}				}
			}
			
		}
		else
		{
			//* 
			//*        Form  y := alpha*A**T*x + y  or  y := alpha*A**H*x + y. 
			//* 
			
			_lwc63p7q = _ylb1uqzt;
			if (_1eqjusqc == (int)1)
			{
				
				{
					System.Int32 __81fgg2dlsvn1150 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1150 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1150;
					for (__81fgg2count1150 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1150 + __81fgg2step1150) / __81fgg2step1150)), _znpjgsef = __81fgg2dlsvn1150; __81fgg2count1150 != 0; __81fgg2count1150--, _znpjgsef += (__81fgg2step1150)) {

					{
						
						_1ajfmh55 = _d0547bi2;
						if (_moml4lap)
						{
							
							{
								System.Int32 __81fgg2dlsvn1151 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1151 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1151;
								for (__81fgg2count1151 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1151 + __81fgg2step1151) / __81fgg2step1151)), _b5p6od9s = __81fgg2dlsvn1151; __81fgg2count1151 != 0; __81fgg2count1151--, _b5p6od9s += (__81fgg2step1151)) {

								{
									
									_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * *(_ta7zuy9k+(_b5p6od9s - 1))));
Mark90:;
									// continue
								}
																}							}
						}
						else
						{
							
							{
								System.Int32 __81fgg2dlsvn1152 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1152 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1152;
								for (__81fgg2count1152 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1152 + __81fgg2step1152) / __81fgg2step1152)), _b5p6od9s = __81fgg2dlsvn1152; __81fgg2count1152 != 0; __81fgg2count1152--, _b5p6od9s += (__81fgg2step1152)) {

								{
									
									_1ajfmh55 = (_1ajfmh55 + (ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * *(_ta7zuy9k+(_b5p6od9s - 1))));
Mark100:;
									// continue
								}
																}							}
						}
						
						*(_f3z3edv0+(_lwc63p7q - 1)) = (*(_f3z3edv0+(_lwc63p7q - 1)) + (_r7cfteg3 * _1ajfmh55));
						_lwc63p7q = (_lwc63p7q + _bbrxgmj7);
Mark110:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn1153 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step1153 = (System.Int32)((int)1);
					System.Int32 __81fgg2count1153;
					for (__81fgg2count1153 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1153 + __81fgg2step1153) / __81fgg2step1153)), _znpjgsef = __81fgg2dlsvn1153; __81fgg2count1153 != 0; __81fgg2count1153--, _znpjgsef += (__81fgg2step1153)) {

					{
						
						_1ajfmh55 = _d0547bi2;
						_b69ritwm = _ziknm33t;
						if (_moml4lap)
						{
							
							{
								System.Int32 __81fgg2dlsvn1154 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1154 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1154;
								for (__81fgg2count1154 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1154 + __81fgg2step1154) / __81fgg2step1154)), _b5p6od9s = __81fgg2dlsvn1154; __81fgg2count1154 != 0; __81fgg2count1154--, _b5p6od9s += (__81fgg2step1154)) {

								{
									
									_1ajfmh55 = (_1ajfmh55 + (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * *(_ta7zuy9k+(_b69ritwm - 1))));
									_b69ritwm = (_b69ritwm + _1eqjusqc);
Mark120:;
									// continue
								}
																}							}
						}
						else
						{
							
							{
								System.Int32 __81fgg2dlsvn1155 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step1155 = (System.Int32)((int)1);
								System.Int32 __81fgg2count1155;
								for (__81fgg2count1155 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1155 + __81fgg2step1155) / __81fgg2step1155)), _b5p6od9s = __81fgg2dlsvn1155; __81fgg2count1155 != 0; __81fgg2count1155--, _b5p6od9s += (__81fgg2step1155)) {

								{
									
									_1ajfmh55 = (_1ajfmh55 + (ILNumerics.F2NET.Intrinsics.DCONJG(*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) ) * *(_ta7zuy9k+(_b69ritwm - 1))));
									_b69ritwm = (_b69ritwm + _1eqjusqc);
Mark130:;
									// continue
								}
																}							}
						}
						
						*(_f3z3edv0+(_lwc63p7q - 1)) = (*(_f3z3edv0+(_lwc63p7q - 1)) + (_r7cfteg3 * _1ajfmh55));
						_lwc63p7q = (_lwc63p7q + _bbrxgmj7);
Mark140:;
						// continue
					}
										}				}
			}
			
		}
		//* 
		
		return;//* 
		//*     End of ZGEMV . 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
