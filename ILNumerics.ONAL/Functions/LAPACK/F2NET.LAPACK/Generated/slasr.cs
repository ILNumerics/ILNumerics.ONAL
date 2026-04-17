
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
//*> \brief \b SLASR applies a sequence of plane rotations to a general rectangular matrix. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLASR + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slasr.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slasr.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slasr.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLASR( SIDE, PIVOT, DIRECT, M, N, C, S, A, LDA ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          DIRECT, PIVOT, SIDE 
//*       INTEGER            LDA, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               A( LDA, * ), C( * ), S( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLASR applies a sequence of plane rotations to a real matrix A, 
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
//*>          A is REAL array, dimension (LDA,N) 
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
//*> \ingroup OTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _2u3ycobg(FString _m2cn2gjg, FString _2836kgwz, FString _uw10mx43, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, Single* _3crf0qn3, Single* _irk8i6qr, Single* _vxfgpup9, ref Int32 _ocv8fk5c)
	{
#region variable declarations
Single _kxg5drh2 =  1f;
Single _d0547bi2 =  0f;
Int32 _b5p6od9s =  default;
Int32 _gro5yvfo =  default;
Int32 _znpjgsef =  default;
Single _r0ocrtbh =  default;
Single _chiot9on =  default;
Single _1ajfmh55 =  default;
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
			
			_ut9qalzx("SLASR " ,ref _gro5yvfo );
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
						System.Int32 __81fgg2dlsvn705 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step705 = (System.Int32)((int)1);
						System.Int32 __81fgg2count705;
						for (__81fgg2count705 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5 - (int)1) - __81fgg2dlsvn705 + __81fgg2step705) / __81fgg2step705)), _znpjgsef = __81fgg2dlsvn705; __81fgg2count705 != 0; __81fgg2count705--, _znpjgsef += (__81fgg2step705)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn706 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step706 = (System.Int32)((int)1);
									System.Int32 __81fgg2count706;
									for (__81fgg2count706 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn706 + __81fgg2step706) / __81fgg2step706)), _b5p6od9s = __81fgg2dlsvn706; __81fgg2count706 != 0; __81fgg2count706--, _b5p6od9s += (__81fgg2step706)) {

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
						System.Int32 __81fgg2dlsvn707 = (System.Int32)((_ev4xhht5 - (int)1));
						System.Int32 __81fgg2step707 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count707;
						for (__81fgg2count707 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn707 + __81fgg2step707) / __81fgg2step707)), _znpjgsef = __81fgg2dlsvn707; __81fgg2count707 != 0; __81fgg2count707--, _znpjgsef += (__81fgg2step707)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn708 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step708 = (System.Int32)((int)1);
									System.Int32 __81fgg2count708;
									for (__81fgg2count708 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn708 + __81fgg2step708) / __81fgg2step708)), _b5p6od9s = __81fgg2dlsvn708; __81fgg2count708 != 0; __81fgg2count708--, _b5p6od9s += (__81fgg2step708)) {

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
						System.Int32 __81fgg2dlsvn709 = (System.Int32)((int)2);
						const System.Int32 __81fgg2step709 = (System.Int32)((int)1);
						System.Int32 __81fgg2count709;
						for (__81fgg2count709 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn709 + __81fgg2step709) / __81fgg2step709)), _znpjgsef = __81fgg2dlsvn709; __81fgg2count709 != 0; __81fgg2count709--, _znpjgsef += (__81fgg2step709)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - (int)1 - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - (int)1 - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn710 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step710 = (System.Int32)((int)1);
									System.Int32 __81fgg2count710;
									for (__81fgg2count710 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn710 + __81fgg2step710) / __81fgg2step710)), _b5p6od9s = __81fgg2dlsvn710; __81fgg2count710 != 0; __81fgg2count710--, _b5p6od9s += (__81fgg2step710)) {

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
						System.Int32 __81fgg2dlsvn711 = (System.Int32)(_ev4xhht5);
						System.Int32 __81fgg2step711 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count711;
						for (__81fgg2count711 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)2) - __81fgg2dlsvn711 + __81fgg2step711) / __81fgg2step711)), _znpjgsef = __81fgg2dlsvn711; __81fgg2count711 != 0; __81fgg2count711--, _znpjgsef += (__81fgg2step711)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - (int)1 - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - (int)1 - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn712 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step712 = (System.Int32)((int)1);
									System.Int32 __81fgg2count712;
									for (__81fgg2count712 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn712 + __81fgg2step712) / __81fgg2step712)), _b5p6od9s = __81fgg2dlsvn712; __81fgg2count712 != 0; __81fgg2count712--, _b5p6od9s += (__81fgg2step712)) {

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
						System.Int32 __81fgg2dlsvn713 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step713 = (System.Int32)((int)1);
						System.Int32 __81fgg2count713;
						for (__81fgg2count713 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5 - (int)1) - __81fgg2dlsvn713 + __81fgg2step713) / __81fgg2step713)), _znpjgsef = __81fgg2dlsvn713; __81fgg2count713 != 0; __81fgg2count713--, _znpjgsef += (__81fgg2step713)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn714 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step714 = (System.Int32)((int)1);
									System.Int32 __81fgg2count714;
									for (__81fgg2count714 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn714 + __81fgg2step714) / __81fgg2step714)), _b5p6od9s = __81fgg2dlsvn714; __81fgg2count714 != 0; __81fgg2count714--, _b5p6od9s += (__81fgg2step714)) {

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
						System.Int32 __81fgg2dlsvn715 = (System.Int32)((_ev4xhht5 - (int)1));
						System.Int32 __81fgg2step715 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count715;
						for (__81fgg2count715 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn715 + __81fgg2step715) / __81fgg2step715)), _znpjgsef = __81fgg2dlsvn715; __81fgg2count715 != 0; __81fgg2count715--, _znpjgsef += (__81fgg2step715)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn716 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step716 = (System.Int32)((int)1);
									System.Int32 __81fgg2count716;
									for (__81fgg2count716 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn716 + __81fgg2step716) / __81fgg2step716)), _b5p6od9s = __81fgg2dlsvn716; __81fgg2count716 != 0; __81fgg2count716--, _b5p6od9s += (__81fgg2step716)) {

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
						System.Int32 __81fgg2dlsvn717 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step717 = (System.Int32)((int)1);
						System.Int32 __81fgg2count717;
						for (__81fgg2count717 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn717 + __81fgg2step717) / __81fgg2step717)), _znpjgsef = __81fgg2dlsvn717; __81fgg2count717 != 0; __81fgg2count717--, _znpjgsef += (__81fgg2step717)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn718 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step718 = (System.Int32)((int)1);
									System.Int32 __81fgg2count718;
									for (__81fgg2count718 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn718 + __81fgg2step718) / __81fgg2step718)), _b5p6od9s = __81fgg2dlsvn718; __81fgg2count718 != 0; __81fgg2count718--, _b5p6od9s += (__81fgg2step718)) {

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
						System.Int32 __81fgg2dlsvn719 = (System.Int32)((_dxpq0xkr - (int)1));
						System.Int32 __81fgg2step719 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count719;
						for (__81fgg2count719 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn719 + __81fgg2step719) / __81fgg2step719)), _znpjgsef = __81fgg2dlsvn719; __81fgg2count719 != 0; __81fgg2count719--, _znpjgsef += (__81fgg2step719)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn720 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step720 = (System.Int32)((int)1);
									System.Int32 __81fgg2count720;
									for (__81fgg2count720 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn720 + __81fgg2step720) / __81fgg2step720)), _b5p6od9s = __81fgg2dlsvn720; __81fgg2count720 != 0; __81fgg2count720--, _b5p6od9s += (__81fgg2step720)) {

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
						System.Int32 __81fgg2dlsvn721 = (System.Int32)((int)2);
						const System.Int32 __81fgg2step721 = (System.Int32)((int)1);
						System.Int32 __81fgg2count721;
						for (__81fgg2count721 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn721 + __81fgg2step721) / __81fgg2step721)), _znpjgsef = __81fgg2dlsvn721; __81fgg2count721 != 0; __81fgg2count721--, _znpjgsef += (__81fgg2step721)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - (int)1 - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - (int)1 - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn722 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step722 = (System.Int32)((int)1);
									System.Int32 __81fgg2count722;
									for (__81fgg2count722 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn722 + __81fgg2step722) / __81fgg2step722)), _b5p6od9s = __81fgg2dlsvn722; __81fgg2count722 != 0; __81fgg2count722--, _b5p6od9s += (__81fgg2step722)) {

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
						System.Int32 __81fgg2dlsvn723 = (System.Int32)(_dxpq0xkr);
						System.Int32 __81fgg2step723 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count723;
						for (__81fgg2count723 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)2) - __81fgg2dlsvn723 + __81fgg2step723) / __81fgg2step723)), _znpjgsef = __81fgg2dlsvn723; __81fgg2count723 != 0; __81fgg2count723--, _znpjgsef += (__81fgg2step723)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - (int)1 - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - (int)1 - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn724 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step724 = (System.Int32)((int)1);
									System.Int32 __81fgg2count724;
									for (__81fgg2count724 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn724 + __81fgg2step724) / __81fgg2step724)), _b5p6od9s = __81fgg2dlsvn724; __81fgg2count724 != 0; __81fgg2count724--, _b5p6od9s += (__81fgg2step724)) {

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
						System.Int32 __81fgg2dlsvn725 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step725 = (System.Int32)((int)1);
						System.Int32 __81fgg2count725;
						for (__81fgg2count725 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn725 + __81fgg2step725) / __81fgg2step725)), _znpjgsef = __81fgg2dlsvn725; __81fgg2count725 != 0; __81fgg2count725--, _znpjgsef += (__81fgg2step725)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn726 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step726 = (System.Int32)((int)1);
									System.Int32 __81fgg2count726;
									for (__81fgg2count726 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn726 + __81fgg2step726) / __81fgg2step726)), _b5p6od9s = __81fgg2dlsvn726; __81fgg2count726 != 0; __81fgg2count726--, _b5p6od9s += (__81fgg2step726)) {

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
						System.Int32 __81fgg2dlsvn727 = (System.Int32)((_dxpq0xkr - (int)1));
						System.Int32 __81fgg2step727 = (System.Int32)((int)-1);
						System.Int32 __81fgg2count727;
						for (__81fgg2count727 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn727 + __81fgg2step727) / __81fgg2step727)), _znpjgsef = __81fgg2dlsvn727; __81fgg2count727 != 0; __81fgg2count727--, _znpjgsef += (__81fgg2step727)) {

						{
							
							_r0ocrtbh = *(_3crf0qn3+(_znpjgsef - 1));
							_chiot9on = *(_irk8i6qr+(_znpjgsef - 1));
							if ((_r0ocrtbh != _kxg5drh2) | (_chiot9on != _d0547bi2))
							{
								
								{
									System.Int32 __81fgg2dlsvn728 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step728 = (System.Int32)((int)1);
									System.Int32 __81fgg2count728;
									for (__81fgg2count728 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn728 + __81fgg2step728) / __81fgg2step728)), _b5p6od9s = __81fgg2dlsvn728; __81fgg2count728 != 0; __81fgg2count728--, _b5p6od9s += (__81fgg2step728)) {

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
		//*     End of SLASR 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
