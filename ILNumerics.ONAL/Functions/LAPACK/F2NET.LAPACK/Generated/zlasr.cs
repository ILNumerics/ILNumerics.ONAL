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
//*> \brief \b ZLASR applies a sequence of plane rotations to a general rectangular matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZLASR + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zlasr.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zlasr.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zlasr.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZLASR( SIDE, PIVOT, DIRECT, M, N, C, S, A, LDA ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          DIRECT, PIVOT, SIDE 
//*       INTEGER            LDA, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   C( * ), S( * ) 
//*       COMPLEX*16         A( LDA, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZLASR applies a sequence of real plane rotations to a complex matrix 
//*> A, from either the left or the right. 
//*> 
//*> When SIDE = 'L', the transformation takes the form 
//*> 
//*>    A := P*A 
//*> 
//*> and when SIDE = 'R', the transformation takes the form 
//*> 
//*>    A := A*P**T 
//*> 
//*> where P is an orthogonal matrix consisting of a sequence of z plane 
//*> rotations, with z = M when SIDE = 'L' and z = N when SIDE = 'R', 
//*> and P**T is the transpose of P. 
//*> 
//*> When DIRECT = 'F' (Forward sequence), then 
//*> 
//*>    P = P(z-1) * ... * P(2) * P(1) 
//*> 
//*> and when DIRECT = 'B' (Backward sequence), then 
//*> 
//*>    P = P(1) * P(2) * ... * P(z-1) 
//*> 
//*> where P(k) is a plane rotation matrix defined by the 2-by-2 rotation 
//*> 
//*>    R(k) = (  c(k)  s(k) ) 
//*>         = ( -s(k)  c(k) ). 
//*> 
//*> When PIVOT = 'V' (Variable pivot), the rotation is performed 
//*> for the plane (k,k+1), i.e., P(k) has the form 
//*> 
//*>    P(k) = (  1                                            ) 
//*>           (       ...                                     ) 
//*>           (              1                                ) 
//*>           (                   c(k)  s(k)                  ) 
//*>           (                  -s(k)  c(k)                  ) 
//*>           (                                1              ) 
//*>           (                                     ...       ) 
//*>           (                                            1  ) 
//*> 
//*> where R(k) appears as a rank-2 modification to the identity matrix in 
//*> rows and columns k and k+1. 
//*> 
//*> When PIVOT = 'T' (Top pivot), the rotation is performed for the 
//*> plane (1,k+1), so P(k) has the form 
//*> 
//*>    P(k) = (  c(k)                    s(k)                 ) 
//*>           (         1                                     ) 
//*>           (              ...                              ) 
//*>           (                     1                         ) 
//*>           ( -s(k)                    c(k)                 ) 
//*>           (                                 1             ) 
//*>           (                                      ...      ) 
//*>           (                                             1 ) 
//*> 
//*> where R(k) appears in rows and columns 1 and k+1. 
//*> 
//*> Similarly, when PIVOT = 'B' (Bottom pivot), the rotation is 
//*> performed for the plane (k,z), giving P(k) the form 
//*> 
//*>    P(k) = ( 1                                             ) 
//*>           (      ...                                      ) 
//*>           (             1                                 ) 
//*>           (                  c(k)                    s(k) ) 
//*>           (                         1                     ) 
//*>           (                              ...              ) 
//*>           (                                     1         ) 
//*>           (                 -s(k)                    c(k) ) 
//*> 
//*> where R(k) appears in rows and columns k and z.  The rotations are 
//*> performed without ever forming P(k) explicitly. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] SIDE 
//*> \verbatim 
//*>          SIDE is CHARACTER*1 
//*>          Specifies whether the plane rotation matrix P is applied to 
//*>          A on the left or the right. 
//*>          = 'L':  Left, compute A := P*A 
//*>          = 'R':  Right, compute A:= A*P**T 
//*> \endverbatim 
//*> 
//*> \param[in] PIVOT 
//*> \verbatim 
//*>          PIVOT is CHARACTER*1 
//*>          Specifies the plane for which P(k) is a plane rotation 
//*>          matrix. 
//*>          = 'V':  Variable pivot, the plane (k,k+1) 
//*>          = 'T':  Top pivot, the plane (1,k+1) 
//*>          = 'B':  Bottom pivot, the plane (k,z) 
//*> \endverbatim 
//*> 
//*> \param[in] DIRECT 
//*> \verbatim 
//*>          DIRECT is CHARACTER*1 
//*>          Specifies whether P is a forward or backward sequence of 
//*>          plane rotations. 
//*>          = 'F':  Forward, P = P(z-1)*...*P(2)*P(1) 
//*>          = 'B':  Backward, P = P(1)*P(2)*...*P(z-1) 
//*> \endverbatim 
//*> 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of rows of the matrix A.  If m <= 1, an immediate 
//*>          return is effected. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of columns of the matrix A.  If n <= 1, an 
//*>          immediate return is effected. 
//*> \endverbatim 
//*> 
//*> \param[in] C 
//*> \verbatim 
//*>          C is DOUBLE PRECISION array, dimension 
//*>                  (M-1) if SIDE = 'L' 
//*>                  (N-1) if SIDE = 'R' 
//*>          The cosines c(k) of the plane rotations. 
//*> \endverbatim 
//*> 
//*> \param[in] S 
//*> \verbatim 
//*>          S is DOUBLE PRECISION array, dimension 
//*>                  (M-1) if SIDE = 'L' 
//*>                  (N-1) if SIDE = 'R' 
//*>          The sines s(k) of the plane rotations.  The 2-by-2 plane 
//*>          rotation part of the matrix P(k), R(k), has the form 
//*>          R(k) = (  c(k)  s(k) ) 
//*>                 ( -s(k)  c(k) ). 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension (LDA,N) 
//*>          The M-by-N matrix A.  On exit, A is overwritten by P*A if 
//*>          SIDE = 'R' or by A*P**T if SIDE = 'L'. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,M). 
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
//*> \ingroup complex16OTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _jp2bunpl(FString _m2cn2gjg, FString _2836kgwz, FString _uw10mx43, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, Double* _3crf0qn3, Double* _irk8i6qr, complex* _vxfgpup9, ref Int32 _ocv8fk5c)
	{
#region variable declarations
Double _kxg5drh2 =  1d;
Double _d0547bi2 =  0d;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _znpjgsef =  default;
Double _r0ocrtbh =  default;
Double _chiot9on =  default;
complex _1ajfmh55 =  default;
string fLanavab = default;
#endregion  variable declarations
_m2cn2gjg = _m2cn2gjg.Convert(1);
_2836kgwz = _2836kgwz.Convert(1);
_uw10mx43 = _uw10mx43.Convert(1);

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
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input parameters 
		//* 
		
		_gro5yvfo = (int)0;
		if (!((_w8y2rzgy(_m2cn2gjg ,"L" ) | _w8y2rzgy(_m2cn2gjg ,"R" ))))
		{
			
			_gro5yvfo = (int)1;
		}
		else
		if (!(((_w8y2rzgy(_2836kgwz ,"V" ) | _w8y2rzgy(_2836kgwz ,"T" )) | _w8y2rzgy(_2836kgwz ,"B" ))))
		{
			
			_gro5yvfo = (int)2;
		}
		else
		if (!((_w8y2rzgy(_uw10mx43 ,"F" ) | _w8y2rzgy(_uw10mx43 ,"B" ))))
		{
			
			_gro5yvfo = (int)3;
		}
		else
		if (_ev4xhht5 < (int)0)
		{
			
			_gro5yvfo = (int)4;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)5;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ))
		{
			
			_gro5yvfo = (int)9;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("ZLASR " ,ref _gro5yvfo );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if ((_ev4xhht5 == (int)0) | (_dxpq0xkr == (int)0))
		return;
		if (_w8y2rzgy(_m2cn2gjg ,"L" ))
		{
			//* 
			//*        Form  P * A 
			//* 
			
			if (_w8y2rzgy(_2836kgwz ,"V" ))
			{
				
				if (_w8y2rzgy(_uw10mx43 ,"F" ))
				{
					
					{
						System.Int32 __81fgg2dlsvn1434 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1434 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1434;
						for (__81fgg2count1434 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5 - (int)1) - __81fgg2dlsvn1434 + __81fgg2step1434) / __81fgg2step1434)), _znpjgsef = __81fgg2dlsvn1434; __81fgg2count1434 != 0; __81fgg2count1434--, _znpjgsef += (__81fgg2step1434)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn1435 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1435 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1435;
									for (__81fgg2count1435 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1435 + __81fgg2step1435) / __81fgg2step1435)), _b5p6od9s = __81fgg2dlsvn1435; __81fgg2count1435 != 0; __81fgg2count1435--, _b5p6od9s += (__81fgg2step1435)) {

									{
										
										_1ajfmh55 = *(_vxfgpup9+(_znpjgsef + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c));
										*(_vxfgpup9+(_znpjgsef + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = ((_r0ocrtbh * _1ajfmh55) - (_chiot9on * *(_vxfgpup9+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))));
										*(_vxfgpup9+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = ((_chiot9on * _1ajfmh55) + (_r0ocrtbh * *(_vxfgpup9+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))));
Mark10:;
										// continue
									}
																		}								}
							}
							
Mark20:;
							// continue
						}
												}					}
				}
				else
				if (_w8y2rzgy(_uw10mx43 ,"B" ))
				{
					
					{
						System.Int32 __81fgg2dlsvn1436 = (System.Int32)((_ev4xhht5 - (int)1));
						System.Int32 __81fgg2step1436 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1436;
						for (__81fgg2count1436 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1436 + __81fgg2step1436) / __81fgg2step1436)), _znpjgsef = __81fgg2dlsvn1436; __81fgg2count1436 != 0; __81fgg2count1436--, _znpjgsef += (__81fgg2step1436)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn1437 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1437 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1437;
									for (__81fgg2count1437 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1437 + __81fgg2step1437) / __81fgg2step1437)), _b5p6od9s = __81fgg2dlsvn1437; __81fgg2count1437 != 0; __81fgg2count1437--, _b5p6od9s += (__81fgg2step1437)) {

									{
										
										_1ajfmh55 = *(_vxfgpup9+(_znpjgsef + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c));
										*(_vxfgpup9+(_znpjgsef + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = ((_r0ocrtbh * _1ajfmh55) - (_chiot9on * *(_vxfgpup9+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))));
										*(_vxfgpup9+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = ((_chiot9on * _1ajfmh55) + (_r0ocrtbh * *(_vxfgpup9+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))));
Mark30:;
										// continue
									}
																		}								}
							}
							
Mark40:;
							// continue
						}
												}					}
				}
				
			}
			else
			if (_w8y2rzgy(_2836kgwz ,"T" ))
			{
				
				if (_w8y2rzgy(_uw10mx43 ,"F" ))
				{
					
					{
						System.Int32 __81fgg2dlsvn1438 = (System.Int32)((int)2);
						const System.Int32 __81fgg2step1438 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1438;
						for (__81fgg2count1438 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1438 + __81fgg2step1438) / __81fgg2step1438)), _znpjgsef = __81fgg2dlsvn1438; __81fgg2count1438 != 0; __81fgg2count1438--, _znpjgsef += (__81fgg2step1438)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - (int)1 - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - (int)1 - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn1439 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1439 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1439;
									for (__81fgg2count1439 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1439 + __81fgg2step1439) / __81fgg2step1439)), _b5p6od9s = __81fgg2dlsvn1439; __81fgg2count1439 != 0; __81fgg2count1439--, _b5p6od9s += (__81fgg2step1439)) {

									{
										
										_1ajfmh55 = *(_vxfgpup9+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c));
										*(_vxfgpup9+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = ((_r0ocrtbh * _1ajfmh55) - (_chiot9on * *(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))));
										*(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = ((_chiot9on * _1ajfmh55) + (_r0ocrtbh * *(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))));
Mark50:;
										// continue
									}
																		}								}
							}
							
Mark60:;
							// continue
						}
												}					}
				}
				else
				if (_w8y2rzgy(_uw10mx43 ,"B" ))
				{
					
					{
						System.Int32 __81fgg2dlsvn1440 = (System.Int32)(_ev4xhht5);
						System.Int32 __81fgg2step1440 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1440;
						for (__81fgg2count1440 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)2) - __81fgg2dlsvn1440 + __81fgg2step1440) / __81fgg2step1440)), _znpjgsef = __81fgg2dlsvn1440; __81fgg2count1440 != 0; __81fgg2count1440--, _znpjgsef += (__81fgg2step1440)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - (int)1 - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - (int)1 - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn1441 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1441 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1441;
									for (__81fgg2count1441 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1441 + __81fgg2step1441) / __81fgg2step1441)), _b5p6od9s = __81fgg2dlsvn1441; __81fgg2count1441 != 0; __81fgg2count1441--, _b5p6od9s += (__81fgg2step1441)) {

									{
										
										_1ajfmh55 = *(_vxfgpup9+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c));
										*(_vxfgpup9+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = ((_r0ocrtbh * _1ajfmh55) - (_chiot9on * *(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))));
										*(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = ((_chiot9on * _1ajfmh55) + (_r0ocrtbh * *(_vxfgpup9+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))));
Mark70:;
										// continue
									}
																		}								}
							}
							
Mark80:;
							// continue
						}
												}					}
				}
				
			}
			else
			if (_w8y2rzgy(_2836kgwz ,"B" ))
			{
				
				if (_w8y2rzgy(_uw10mx43 ,"F" ))
				{
					
					{
						System.Int32 __81fgg2dlsvn1442 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1442 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1442;
						for (__81fgg2count1442 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5 - (int)1) - __81fgg2dlsvn1442 + __81fgg2step1442) / __81fgg2step1442)), _znpjgsef = __81fgg2dlsvn1442; __81fgg2count1442 != 0; __81fgg2count1442--, _znpjgsef += (__81fgg2step1442)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn1443 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1443 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1443;
									for (__81fgg2count1443 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1443 + __81fgg2step1443) / __81fgg2step1443)), _b5p6od9s = __81fgg2dlsvn1443; __81fgg2count1443 != 0; __81fgg2count1443--, _b5p6od9s += (__81fgg2step1443)) {

									{
										
										_1ajfmh55 = *(_vxfgpup9+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c));
										*(_vxfgpup9+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = ((_chiot9on * *(_vxfgpup9+(_ev4xhht5 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))) + (_r0ocrtbh * _1ajfmh55));
										*(_vxfgpup9+(_ev4xhht5 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = ((_r0ocrtbh * *(_vxfgpup9+(_ev4xhht5 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))) - (_chiot9on * _1ajfmh55));
Mark90:;
										// continue
									}
																		}								}
							}
							
Mark100:;
							// continue
						}
												}					}
				}
				else
				if (_w8y2rzgy(_uw10mx43 ,"B" ))
				{
					
					{
						System.Int32 __81fgg2dlsvn1444 = (System.Int32)((_ev4xhht5 - (int)1));
						System.Int32 __81fgg2step1444 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1444;
						for (__81fgg2count1444 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1444 + __81fgg2step1444) / __81fgg2step1444)), _znpjgsef = __81fgg2dlsvn1444; __81fgg2count1444 != 0; __81fgg2count1444--, _znpjgsef += (__81fgg2step1444)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn1445 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1445 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1445;
									for (__81fgg2count1445 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1445 + __81fgg2step1445) / __81fgg2step1445)), _b5p6od9s = __81fgg2dlsvn1445; __81fgg2count1445 != 0; __81fgg2count1445--, _b5p6od9s += (__81fgg2step1445)) {

									{
										
										_1ajfmh55 = *(_vxfgpup9+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c));
										*(_vxfgpup9+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = ((_chiot9on * *(_vxfgpup9+(_ev4xhht5 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))) + (_r0ocrtbh * _1ajfmh55));
										*(_vxfgpup9+(_ev4xhht5 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = ((_r0ocrtbh * *(_vxfgpup9+(_ev4xhht5 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))) - (_chiot9on * _1ajfmh55));
Mark110:;
										// continue
									}
																		}								}
							}
							
Mark120:;
							// continue
						}
												}					}
				}
				
			}
			
		}
		else
		if (_w8y2rzgy(_m2cn2gjg ,"R" ))
		{
			//* 
			//*        Form A * P**T 
			//* 
			
			if (_w8y2rzgy(_2836kgwz ,"V" ))
			{
				
				if (_w8y2rzgy(_uw10mx43 ,"F" ))
				{
					
					{
						System.Int32 __81fgg2dlsvn1446 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1446 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1446;
						for (__81fgg2count1446 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn1446 + __81fgg2step1446) / __81fgg2step1446)), _znpjgsef = __81fgg2dlsvn1446; __81fgg2count1446 != 0; __81fgg2count1446--, _znpjgsef += (__81fgg2step1446)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn1447 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1447 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1447;
									for (__81fgg2count1447 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1447 + __81fgg2step1447) / __81fgg2step1447)), _b5p6od9s = __81fgg2dlsvn1447; __81fgg2count1447 != 0; __81fgg2count1447--, _b5p6od9s += (__81fgg2step1447)) {

									{
										
										_1ajfmh55 = *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef + (int)1 - 1) * 1 * (_ocv8fk5c));
										*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef + (int)1 - 1) * 1 * (_ocv8fk5c)) = ((_r0ocrtbh * _1ajfmh55) - (_chiot9on * *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
										*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = ((_chiot9on * _1ajfmh55) + (_r0ocrtbh * *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
Mark130:;
										// continue
									}
																		}								}
							}
							
Mark140:;
							// continue
						}
												}					}
				}
				else
				if (_w8y2rzgy(_uw10mx43 ,"B" ))
				{
					
					{
						System.Int32 __81fgg2dlsvn1448 = (System.Int32)((_dxpq0xkr - (int)1));
						System.Int32 __81fgg2step1448 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1448;
						for (__81fgg2count1448 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1448 + __81fgg2step1448) / __81fgg2step1448)), _znpjgsef = __81fgg2dlsvn1448; __81fgg2count1448 != 0; __81fgg2count1448--, _znpjgsef += (__81fgg2step1448)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn1449 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1449 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1449;
									for (__81fgg2count1449 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1449 + __81fgg2step1449) / __81fgg2step1449)), _b5p6od9s = __81fgg2dlsvn1449; __81fgg2count1449 != 0; __81fgg2count1449--, _b5p6od9s += (__81fgg2step1449)) {

									{
										
										_1ajfmh55 = *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef + (int)1 - 1) * 1 * (_ocv8fk5c));
										*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef + (int)1 - 1) * 1 * (_ocv8fk5c)) = ((_r0ocrtbh * _1ajfmh55) - (_chiot9on * *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
										*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = ((_chiot9on * _1ajfmh55) + (_r0ocrtbh * *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c))));
Mark150:;
										// continue
									}
																		}								}
							}
							
Mark160:;
							// continue
						}
												}					}
				}
				
			}
			else
			if (_w8y2rzgy(_2836kgwz ,"T" ))
			{
				
				if (_w8y2rzgy(_uw10mx43 ,"F" ))
				{
					
					{
						System.Int32 __81fgg2dlsvn1450 = (System.Int32)((int)2);
						const System.Int32 __81fgg2step1450 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1450;
						for (__81fgg2count1450 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1450 + __81fgg2step1450) / __81fgg2step1450)), _znpjgsef = __81fgg2dlsvn1450; __81fgg2count1450 != 0; __81fgg2count1450--, _znpjgsef += (__81fgg2step1450)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - (int)1 - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - (int)1 - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn1451 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1451 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1451;
									for (__81fgg2count1451 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1451 + __81fgg2step1451) / __81fgg2step1451)), _b5p6od9s = __81fgg2dlsvn1451; __81fgg2count1451 != 0; __81fgg2count1451--, _b5p6od9s += (__81fgg2step1451)) {

									{
										
										_1ajfmh55 = *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c));
										*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = ((_r0ocrtbh * _1ajfmh55) - (_chiot9on * *(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c))));
										*(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) = ((_chiot9on * _1ajfmh55) + (_r0ocrtbh * *(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c))));
Mark170:;
										// continue
									}
																		}								}
							}
							
Mark180:;
							// continue
						}
												}					}
				}
				else
				if (_w8y2rzgy(_uw10mx43 ,"B" ))
				{
					
					{
						System.Int32 __81fgg2dlsvn1452 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step1452 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1452;
						for (__81fgg2count1452 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)2) - __81fgg2dlsvn1452 + __81fgg2step1452) / __81fgg2step1452)), _znpjgsef = __81fgg2dlsvn1452; __81fgg2count1452 != 0; __81fgg2count1452--, _znpjgsef += (__81fgg2step1452)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - (int)1 - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - (int)1 - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn1453 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1453 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1453;
									for (__81fgg2count1453 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1453 + __81fgg2step1453) / __81fgg2step1453)), _b5p6od9s = __81fgg2dlsvn1453; __81fgg2count1453 != 0; __81fgg2count1453--, _b5p6od9s += (__81fgg2step1453)) {

									{
										
										_1ajfmh55 = *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c));
										*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = ((_r0ocrtbh * _1ajfmh55) - (_chiot9on * *(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c))));
										*(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) = ((_chiot9on * _1ajfmh55) + (_r0ocrtbh * *(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c))));
Mark190:;
										// continue
									}
																		}								}
							}
							
Mark200:;
							// continue
						}
												}					}
				}
				
			}
			else
			if (_w8y2rzgy(_2836kgwz ,"B" ))
			{
				
				if (_w8y2rzgy(_uw10mx43 ,"F" ))
				{
					
					{
						System.Int32 __81fgg2dlsvn1454 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1454 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1454;
						for (__81fgg2count1454 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn1454 + __81fgg2step1454) / __81fgg2step1454)), _znpjgsef = __81fgg2dlsvn1454; __81fgg2count1454 != 0; __81fgg2count1454--, _znpjgsef += (__81fgg2step1454)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn1455 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1455 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1455;
									for (__81fgg2count1455 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1455 + __81fgg2step1455) / __81fgg2step1455)), _b5p6od9s = __81fgg2dlsvn1455; __81fgg2count1455 != 0; __81fgg2count1455--, _b5p6od9s += (__81fgg2step1455)) {

									{
										
										_1ajfmh55 = *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c));
										*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = ((_chiot9on * *(_vxfgpup9+(_b5p6od9s - 1) + (_dxpq0xkr - 1) * 1 * (_ocv8fk5c))) + (_r0ocrtbh * _1ajfmh55));
										*(_vxfgpup9+(_b5p6od9s - 1) + (_dxpq0xkr - 1) * 1 * (_ocv8fk5c)) = ((_r0ocrtbh * *(_vxfgpup9+(_b5p6od9s - 1) + (_dxpq0xkr - 1) * 1 * (_ocv8fk5c))) - (_chiot9on * _1ajfmh55));
Mark210:;
										// continue
									}
																		}								}
							}
							
Mark220:;
							// continue
						}
												}					}
				}
				else
				if (_w8y2rzgy(_uw10mx43 ,"B" ))
				{
					
					{
						System.Int32 __81fgg2dlsvn1456 = (System.Int32)((_dxpq0xkr - (int)1));
						System.Int32 __81fgg2step1456 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1456;
						for (__81fgg2count1456 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1456 + __81fgg2step1456) / __81fgg2step1456)), _znpjgsef = __81fgg2dlsvn1456; __81fgg2count1456 != 0; __81fgg2count1456--, _znpjgsef += (__81fgg2step1456)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn1457 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1457 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1457;
									for (__81fgg2count1457 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1457 + __81fgg2step1457) / __81fgg2step1457)), _b5p6od9s = __81fgg2dlsvn1457; __81fgg2count1457 != 0; __81fgg2count1457--, _b5p6od9s += (__81fgg2step1457)) {

									{
										
										_1ajfmh55 = *(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c));
										*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = ((_chiot9on * *(_vxfgpup9+(_b5p6od9s - 1) + (_dxpq0xkr - 1) * 1 * (_ocv8fk5c))) + (_r0ocrtbh * _1ajfmh55));
										*(_vxfgpup9+(_b5p6od9s - 1) + (_dxpq0xkr - 1) * 1 * (_ocv8fk5c)) = ((_r0ocrtbh * *(_vxfgpup9+(_b5p6od9s - 1) + (_dxpq0xkr - 1) * 1 * (_ocv8fk5c))) - (_chiot9on * _1ajfmh55));
Mark230:;
										// continue
									}
																		}								}
							}
							
Mark240:;
							// continue
						}
												}					}
				}
				
			}
			
		}
		//* 
		
		return;//* 
		//*     End of ZLASR 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
