
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
//*> \brief \b CLASR applies a sequence of plane rotations to a general rectangular matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CLASR + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/clasr.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/clasr.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/clasr.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CLASR( SIDE, PIVOT, DIRECT, M, N, C, S, A, LDA ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          DIRECT, PIVOT, SIDE 
//*       INTEGER            LDA, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               C( * ), S( * ) 
//*       COMPLEX            A( LDA, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CLASR applies a sequence of real plane rotations to a complex matrix 
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
//*>          C is REAL array, dimension 
//*>                  (M-1) if SIDE = 'L' 
//*>                  (N-1) if SIDE = 'R' 
//*>          The cosines c(k) of the plane rotations. 
//*> \endverbatim 
//*> 
//*> \param[in] S 
//*> \verbatim 
//*>          S is REAL array, dimension 
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
//*>          A is COMPLEX array, dimension (LDA,N) 
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
//*> \ingroup complexOTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _40qhbg49(FString _m2cn2gjg, FString _2836kgwz, FString _uw10mx43, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, Single* _3crf0qn3, Single* _irk8i6qr, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c)
	{
#region variable declarations
Single _kxg5drh2 =  1f;
Single _d0547bi2 =  0f;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _znpjgsef =  default;
Single _r0ocrtbh =  default;
Single _chiot9on =  default;
fcomplex _1ajfmh55 =  default;
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
			
			_ut9qalzx("CLASR " ,ref _gro5yvfo );
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
						System.Int32 __81fgg2dlsvn1385 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1385 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1385;
						for (__81fgg2count1385 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5 - (int)1) - __81fgg2dlsvn1385 + __81fgg2step1385) / __81fgg2step1385)), _znpjgsef = __81fgg2dlsvn1385; __81fgg2count1385 != 0; __81fgg2count1385--, _znpjgsef += (__81fgg2step1385)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn1386 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1386 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1386;
									for (__81fgg2count1386 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1386 + __81fgg2step1386) / __81fgg2step1386)), _b5p6od9s = __81fgg2dlsvn1386; __81fgg2count1386 != 0; __81fgg2count1386--, _b5p6od9s += (__81fgg2step1386)) {

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
						System.Int32 __81fgg2dlsvn1387 = (System.Int32)((_ev4xhht5 - (int)1));
						System.Int32 __81fgg2step1387 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1387;
						for (__81fgg2count1387 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1387 + __81fgg2step1387) / __81fgg2step1387)), _znpjgsef = __81fgg2dlsvn1387; __81fgg2count1387 != 0; __81fgg2count1387--, _znpjgsef += (__81fgg2step1387)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn1388 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1388 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1388;
									for (__81fgg2count1388 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1388 + __81fgg2step1388) / __81fgg2step1388)), _b5p6od9s = __81fgg2dlsvn1388; __81fgg2count1388 != 0; __81fgg2count1388--, _b5p6od9s += (__81fgg2step1388)) {

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
						System.Int32 __81fgg2dlsvn1389 = (System.Int32)((int)2);
						const System.Int32 __81fgg2step1389 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1389;
						for (__81fgg2count1389 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1389 + __81fgg2step1389) / __81fgg2step1389)), _znpjgsef = __81fgg2dlsvn1389; __81fgg2count1389 != 0; __81fgg2count1389--, _znpjgsef += (__81fgg2step1389)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - (int)1 - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - (int)1 - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn1390 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1390 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1390;
									for (__81fgg2count1390 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1390 + __81fgg2step1390) / __81fgg2step1390)), _b5p6od9s = __81fgg2dlsvn1390; __81fgg2count1390 != 0; __81fgg2count1390--, _b5p6od9s += (__81fgg2step1390)) {

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
						System.Int32 __81fgg2dlsvn1391 = (System.Int32)(_ev4xhht5);
						System.Int32 __81fgg2step1391 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1391;
						for (__81fgg2count1391 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)2) - __81fgg2dlsvn1391 + __81fgg2step1391) / __81fgg2step1391)), _znpjgsef = __81fgg2dlsvn1391; __81fgg2count1391 != 0; __81fgg2count1391--, _znpjgsef += (__81fgg2step1391)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - (int)1 - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - (int)1 - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn1392 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1392 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1392;
									for (__81fgg2count1392 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1392 + __81fgg2step1392) / __81fgg2step1392)), _b5p6od9s = __81fgg2dlsvn1392; __81fgg2count1392 != 0; __81fgg2count1392--, _b5p6od9s += (__81fgg2step1392)) {

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
						System.Int32 __81fgg2dlsvn1393 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1393 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1393;
						for (__81fgg2count1393 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5 - (int)1) - __81fgg2dlsvn1393 + __81fgg2step1393) / __81fgg2step1393)), _znpjgsef = __81fgg2dlsvn1393; __81fgg2count1393 != 0; __81fgg2count1393--, _znpjgsef += (__81fgg2step1393)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn1394 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1394 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1394;
									for (__81fgg2count1394 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1394 + __81fgg2step1394) / __81fgg2step1394)), _b5p6od9s = __81fgg2dlsvn1394; __81fgg2count1394 != 0; __81fgg2count1394--, _b5p6od9s += (__81fgg2step1394)) {

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
						System.Int32 __81fgg2dlsvn1395 = (System.Int32)((_ev4xhht5 - (int)1));
						System.Int32 __81fgg2step1395 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1395;
						for (__81fgg2count1395 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1395 + __81fgg2step1395) / __81fgg2step1395)), _znpjgsef = __81fgg2dlsvn1395; __81fgg2count1395 != 0; __81fgg2count1395--, _znpjgsef += (__81fgg2step1395)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn1396 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1396 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1396;
									for (__81fgg2count1396 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1396 + __81fgg2step1396) / __81fgg2step1396)), _b5p6od9s = __81fgg2dlsvn1396; __81fgg2count1396 != 0; __81fgg2count1396--, _b5p6od9s += (__81fgg2step1396)) {

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
						System.Int32 __81fgg2dlsvn1397 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1397 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1397;
						for (__81fgg2count1397 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn1397 + __81fgg2step1397) / __81fgg2step1397)), _znpjgsef = __81fgg2dlsvn1397; __81fgg2count1397 != 0; __81fgg2count1397--, _znpjgsef += (__81fgg2step1397)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn1398 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1398 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1398;
									for (__81fgg2count1398 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1398 + __81fgg2step1398) / __81fgg2step1398)), _b5p6od9s = __81fgg2dlsvn1398; __81fgg2count1398 != 0; __81fgg2count1398--, _b5p6od9s += (__81fgg2step1398)) {

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
						System.Int32 __81fgg2dlsvn1399 = (System.Int32)((_dxpq0xkr - (int)1));
						System.Int32 __81fgg2step1399 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1399;
						for (__81fgg2count1399 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1399 + __81fgg2step1399) / __81fgg2step1399)), _znpjgsef = __81fgg2dlsvn1399; __81fgg2count1399 != 0; __81fgg2count1399--, _znpjgsef += (__81fgg2step1399)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn1400 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1400 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1400;
									for (__81fgg2count1400 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1400 + __81fgg2step1400) / __81fgg2step1400)), _b5p6od9s = __81fgg2dlsvn1400; __81fgg2count1400 != 0; __81fgg2count1400--, _b5p6od9s += (__81fgg2step1400)) {

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
						System.Int32 __81fgg2dlsvn1401 = (System.Int32)((int)2);
						const System.Int32 __81fgg2step1401 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1401;
						for (__81fgg2count1401 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn1401 + __81fgg2step1401) / __81fgg2step1401)), _znpjgsef = __81fgg2dlsvn1401; __81fgg2count1401 != 0; __81fgg2count1401--, _znpjgsef += (__81fgg2step1401)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - (int)1 - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - (int)1 - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn1402 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1402 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1402;
									for (__81fgg2count1402 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1402 + __81fgg2step1402) / __81fgg2step1402)), _b5p6od9s = __81fgg2dlsvn1402; __81fgg2count1402 != 0; __81fgg2count1402--, _b5p6od9s += (__81fgg2step1402)) {

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
						System.Int32 __81fgg2dlsvn1403 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step1403 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1403;
						for (__81fgg2count1403 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)2) - __81fgg2dlsvn1403 + __81fgg2step1403) / __81fgg2step1403)), _znpjgsef = __81fgg2dlsvn1403; __81fgg2count1403 != 0; __81fgg2count1403--, _znpjgsef += (__81fgg2step1403)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - (int)1 - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - (int)1 - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn1404 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1404 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1404;
									for (__81fgg2count1404 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1404 + __81fgg2step1404) / __81fgg2step1404)), _b5p6od9s = __81fgg2dlsvn1404; __81fgg2count1404 != 0; __81fgg2count1404--, _b5p6od9s += (__81fgg2step1404)) {

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
						System.Int32 __81fgg2dlsvn1405 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1405 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1405;
						for (__81fgg2count1405 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn1405 + __81fgg2step1405) / __81fgg2step1405)), _znpjgsef = __81fgg2dlsvn1405; __81fgg2count1405 != 0; __81fgg2count1405--, _znpjgsef += (__81fgg2step1405)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn1406 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1406 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1406;
									for (__81fgg2count1406 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1406 + __81fgg2step1406) / __81fgg2step1406)), _b5p6od9s = __81fgg2dlsvn1406; __81fgg2count1406 != 0; __81fgg2count1406--, _b5p6od9s += (__81fgg2step1406)) {

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
						System.Int32 __81fgg2dlsvn1407 = (System.Int32)((_dxpq0xkr - (int)1));
						System.Int32 __81fgg2step1407 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count1407;
						for (__81fgg2count1407 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn1407 + __81fgg2step1407) / __81fgg2step1407)), _znpjgsef = __81fgg2dlsvn1407; __81fgg2count1407 != 0; __81fgg2count1407--, _znpjgsef += (__81fgg2step1407)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn1408 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step1408 = (System.Int32)((int)1);
									System.Int32 __81fgg2count1408;
									for (__81fgg2count1408 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn1408 + __81fgg2step1408) / __81fgg2step1408)), _b5p6od9s = __81fgg2dlsvn1408; __81fgg2count1408 != 0; __81fgg2count1408--, _b5p6od9s += (__81fgg2step1408)) {

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
		//*     End of CLASR 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
