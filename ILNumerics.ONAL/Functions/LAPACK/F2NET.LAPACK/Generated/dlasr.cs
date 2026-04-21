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
//*> \brief \b DLASR applies a sequence of plane rotations to a general rectangular matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLASR + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlasr.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlasr.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlasr.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLASR( SIDE, PIVOT, DIRECT, M, N, C, S, A, LDA ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          DIRECT, PIVOT, SIDE 
//*       INTEGER            LDA, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   A( LDA, * ), C( * ), S( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLASR applies a sequence of plane rotations to a real matrix A, 
//*> from either the left or the right. 
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
//*>          A is DOUBLE PRECISION array, dimension (LDA,N) 
//*>          The M-by-N matrix A.  On exit, A is overwritten by P*A if 
//*>          SIDE = 'L' or by A*P**T if SIDE = 'R'. 
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
//*> \ingroup OTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _sg7u7241(FString _m2cn2gjg, FString _2836kgwz, FString _uw10mx43, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, Double* _3crf0qn3, Double* _irk8i6qr, Double* _vxfgpup9, ref Int32 _ocv8fk5c)
	{
#region variable declarations
Double _kxg5drh2 =  1d;
Double _d0547bi2 =  0d;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _znpjgsef =  default;
Double _r0ocrtbh =  default;
Double _chiot9on =  default;
Double _1ajfmh55 =  default;
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
		//*     .. External Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
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
			
			_ut9qalzx("DLASR " ,ref _gro5yvfo );
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
						System.Int32 __81fgg2dlsvn330 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step330 = (System.Int32)((int)1);
						System.Int32 __81fgg2count330;
						for (__81fgg2count330 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5 - (int)1) - __81fgg2dlsvn330 + __81fgg2step330) / __81fgg2step330)), _znpjgsef = __81fgg2dlsvn330; __81fgg2count330 != 0; __81fgg2count330--, _znpjgsef += (__81fgg2step330)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn331 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step331 = (System.Int32)((int)1);
									System.Int32 __81fgg2count331;
									for (__81fgg2count331 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn331 + __81fgg2step331) / __81fgg2step331)), _b5p6od9s = __81fgg2dlsvn331; __81fgg2count331 != 0; __81fgg2count331--, _b5p6od9s += (__81fgg2step331)) {

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
						System.Int32 __81fgg2dlsvn332 = (System.Int32)((_ev4xhht5 - (int)1));
						System.Int32 __81fgg2step332 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count332;
						for (__81fgg2count332 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn332 + __81fgg2step332) / __81fgg2step332)), _znpjgsef = __81fgg2dlsvn332; __81fgg2count332 != 0; __81fgg2count332--, _znpjgsef += (__81fgg2step332)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn333 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step333 = (System.Int32)((int)1);
									System.Int32 __81fgg2count333;
									for (__81fgg2count333 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn333 + __81fgg2step333) / __81fgg2step333)), _b5p6od9s = __81fgg2dlsvn333; __81fgg2count333 != 0; __81fgg2count333--, _b5p6od9s += (__81fgg2step333)) {

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
						System.Int32 __81fgg2dlsvn334 = (System.Int32)((int)2);
						const System.Int32 __81fgg2step334 = (System.Int32)((int)1);
						System.Int32 __81fgg2count334;
						for (__81fgg2count334 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn334 + __81fgg2step334) / __81fgg2step334)), _znpjgsef = __81fgg2dlsvn334; __81fgg2count334 != 0; __81fgg2count334--, _znpjgsef += (__81fgg2step334)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - (int)1 - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - (int)1 - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn335 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step335 = (System.Int32)((int)1);
									System.Int32 __81fgg2count335;
									for (__81fgg2count335 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn335 + __81fgg2step335) / __81fgg2step335)), _b5p6od9s = __81fgg2dlsvn335; __81fgg2count335 != 0; __81fgg2count335--, _b5p6od9s += (__81fgg2step335)) {

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
						System.Int32 __81fgg2dlsvn336 = (System.Int32)(_ev4xhht5);
						System.Int32 __81fgg2step336 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count336;
						for (__81fgg2count336 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)2) - __81fgg2dlsvn336 + __81fgg2step336) / __81fgg2step336)), _znpjgsef = __81fgg2dlsvn336; __81fgg2count336 != 0; __81fgg2count336--, _znpjgsef += (__81fgg2step336)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - (int)1 - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - (int)1 - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn337 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step337 = (System.Int32)((int)1);
									System.Int32 __81fgg2count337;
									for (__81fgg2count337 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn337 + __81fgg2step337) / __81fgg2step337)), _b5p6od9s = __81fgg2dlsvn337; __81fgg2count337 != 0; __81fgg2count337--, _b5p6od9s += (__81fgg2step337)) {

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
						System.Int32 __81fgg2dlsvn338 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step338 = (System.Int32)((int)1);
						System.Int32 __81fgg2count338;
						for (__81fgg2count338 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5 - (int)1) - __81fgg2dlsvn338 + __81fgg2step338) / __81fgg2step338)), _znpjgsef = __81fgg2dlsvn338; __81fgg2count338 != 0; __81fgg2count338--, _znpjgsef += (__81fgg2step338)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn339 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step339 = (System.Int32)((int)1);
									System.Int32 __81fgg2count339;
									for (__81fgg2count339 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn339 + __81fgg2step339) / __81fgg2step339)), _b5p6od9s = __81fgg2dlsvn339; __81fgg2count339 != 0; __81fgg2count339--, _b5p6od9s += (__81fgg2step339)) {

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
						System.Int32 __81fgg2dlsvn340 = (System.Int32)((_ev4xhht5 - (int)1));
						System.Int32 __81fgg2step340 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count340;
						for (__81fgg2count340 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn340 + __81fgg2step340) / __81fgg2step340)), _znpjgsef = __81fgg2dlsvn340; __81fgg2count340 != 0; __81fgg2count340--, _znpjgsef += (__81fgg2step340)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn341 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step341 = (System.Int32)((int)1);
									System.Int32 __81fgg2count341;
									for (__81fgg2count341 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn341 + __81fgg2step341) / __81fgg2step341)), _b5p6od9s = __81fgg2dlsvn341; __81fgg2count341 != 0; __81fgg2count341--, _b5p6od9s += (__81fgg2step341)) {

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
						System.Int32 __81fgg2dlsvn342 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step342 = (System.Int32)((int)1);
						System.Int32 __81fgg2count342;
						for (__81fgg2count342 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn342 + __81fgg2step342) / __81fgg2step342)), _znpjgsef = __81fgg2dlsvn342; __81fgg2count342 != 0; __81fgg2count342--, _znpjgsef += (__81fgg2step342)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn343 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step343 = (System.Int32)((int)1);
									System.Int32 __81fgg2count343;
									for (__81fgg2count343 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn343 + __81fgg2step343) / __81fgg2step343)), _b5p6od9s = __81fgg2dlsvn343; __81fgg2count343 != 0; __81fgg2count343--, _b5p6od9s += (__81fgg2step343)) {

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
						System.Int32 __81fgg2dlsvn344 = (System.Int32)((_dxpq0xkr - (int)1));
						System.Int32 __81fgg2step344 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count344;
						for (__81fgg2count344 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn344 + __81fgg2step344) / __81fgg2step344)), _znpjgsef = __81fgg2dlsvn344; __81fgg2count344 != 0; __81fgg2count344--, _znpjgsef += (__81fgg2step344)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn345 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step345 = (System.Int32)((int)1);
									System.Int32 __81fgg2count345;
									for (__81fgg2count345 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn345 + __81fgg2step345) / __81fgg2step345)), _b5p6od9s = __81fgg2dlsvn345; __81fgg2count345 != 0; __81fgg2count345--, _b5p6od9s += (__81fgg2step345)) {

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
						System.Int32 __81fgg2dlsvn346 = (System.Int32)((int)2);
						const System.Int32 __81fgg2step346 = (System.Int32)((int)1);
						System.Int32 __81fgg2count346;
						for (__81fgg2count346 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn346 + __81fgg2step346) / __81fgg2step346)), _znpjgsef = __81fgg2dlsvn346; __81fgg2count346 != 0; __81fgg2count346--, _znpjgsef += (__81fgg2step346)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - (int)1 - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - (int)1 - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn347 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step347 = (System.Int32)((int)1);
									System.Int32 __81fgg2count347;
									for (__81fgg2count347 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn347 + __81fgg2step347) / __81fgg2step347)), _b5p6od9s = __81fgg2dlsvn347; __81fgg2count347 != 0; __81fgg2count347--, _b5p6od9s += (__81fgg2step347)) {

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
						System.Int32 __81fgg2dlsvn348 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step348 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count348;
						for (__81fgg2count348 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)2) - __81fgg2dlsvn348 + __81fgg2step348) / __81fgg2step348)), _znpjgsef = __81fgg2dlsvn348; __81fgg2count348 != 0; __81fgg2count348--, _znpjgsef += (__81fgg2step348)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - (int)1 - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - (int)1 - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn349 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step349 = (System.Int32)((int)1);
									System.Int32 __81fgg2count349;
									for (__81fgg2count349 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn349 + __81fgg2step349) / __81fgg2step349)), _b5p6od9s = __81fgg2dlsvn349; __81fgg2count349 != 0; __81fgg2count349--, _b5p6od9s += (__81fgg2step349)) {

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
						System.Int32 __81fgg2dlsvn350 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step350 = (System.Int32)((int)1);
						System.Int32 __81fgg2count350;
						for (__81fgg2count350 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn350 + __81fgg2step350) / __81fgg2step350)), _znpjgsef = __81fgg2dlsvn350; __81fgg2count350 != 0; __81fgg2count350--, _znpjgsef += (__81fgg2step350)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn351 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step351 = (System.Int32)((int)1);
									System.Int32 __81fgg2count351;
									for (__81fgg2count351 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn351 + __81fgg2step351) / __81fgg2step351)), _b5p6od9s = __81fgg2dlsvn351; __81fgg2count351 != 0; __81fgg2count351--, _b5p6od9s += (__81fgg2step351)) {

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
						System.Int32 __81fgg2dlsvn352 = (System.Int32)((_dxpq0xkr - (int)1));
						System.Int32 __81fgg2step352 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count352;
						for (__81fgg2count352 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn352 + __81fgg2step352) / __81fgg2step352)), _znpjgsef = __81fgg2dlsvn352; __81fgg2count352 != 0; __81fgg2count352--, _znpjgsef += (__81fgg2step352)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn353 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step353 = (System.Int32)((int)1);
									System.Int32 __81fgg2count353;
									for (__81fgg2count353 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn353 + __81fgg2step353) / __81fgg2step353)), _b5p6od9s = __81fgg2dlsvn353; __81fgg2count353 != 0; __81fgg2count353--, _b5p6od9s += (__81fgg2step353)) {

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
		//*     End of DLASR 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
