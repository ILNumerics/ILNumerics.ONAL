
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
//*> \brief \b DLASCL multiplies a general rectangular matrix by a real scalar defined as cto/cfrom. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLASCL + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlascl.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlascl.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlascl.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLASCL( TYPE, KL, KU, CFROM, CTO, M, N, A, LDA, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          TYPE 
//*       INTEGER            INFO, KL, KU, LDA, M, N 
//*       DOUBLE PRECISION   CFROM, CTO 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   A( LDA, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLASCL multiplies the M by N real matrix A by the real scalar 
//*> CTO/CFROM.  This is done without over/underflow as long as the final 
//*> result CTO*A(I,J)/CFROM does not over/underflow. TYPE specifies that 
//*> A may be full, upper triangular, lower triangular, upper Hessenberg, 
//*> or banded. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] TYPE 
//*> \verbatim 
//*>          TYPE is CHARACTER*1 
//*>          TYPE indices the storage type of the input matrix. 
//*>          = 'G':  A is a full matrix. 
//*>          = 'L':  A is a lower triangular matrix. 
//*>          = 'U':  A is an upper triangular matrix. 
//*>          = 'H':  A is an upper Hessenberg matrix. 
//*>          = 'B':  A is a symmetric band matrix with lower bandwidth KL 
//*>                  and upper bandwidth KU and with the only the lower 
//*>                  half stored. 
//*>          = 'Q':  A is a symmetric band matrix with lower bandwidth KL 
//*>                  and upper bandwidth KU and with the only the upper 
//*>                  half stored. 
//*>          = 'Z':  A is a band matrix with lower bandwidth KL and upper 
//*>                  bandwidth KU. See DGBTRF for storage details. 
//*> \endverbatim 
//*> 
//*> \param[in] KL 
//*> \verbatim 
//*>          KL is INTEGER 
//*>          The lower bandwidth of A.  Referenced only if TYPE = 'B', 
//*>          'Q' or 'Z'. 
//*> \endverbatim 
//*> 
//*> \param[in] KU 
//*> \verbatim 
//*>          KU is INTEGER 
//*>          The upper bandwidth of A.  Referenced only if TYPE = 'B', 
//*>          'Q' or 'Z'. 
//*> \endverbatim 
//*> 
//*> \param[in] CFROM 
//*> \verbatim 
//*>          CFROM is DOUBLE PRECISION 
//*> \endverbatim 
//*> 
//*> \param[in] CTO 
//*> \verbatim 
//*>          CTO is DOUBLE PRECISION 
//*> 
//*>          The matrix A is multiplied by CTO/CFROM. A(I,J) is computed 
//*>          without over/underflow if the final result CTO*A(I,J)/CFROM 
//*>          can be represented without over/underflow.  CFROM must be 
//*>          nonzero. 
//*> \endverbatim 
//*> 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of rows of the matrix A.  M >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of columns of the matrix A.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is DOUBLE PRECISION array, dimension (LDA,N) 
//*>          The matrix to be multiplied by CTO/CFROM.  See TYPE for the 
//*>          storage type. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A. 
//*>          If TYPE = 'G', 'L', 'U', 'H', LDA >= max(1,M); 
//*>             TYPE = 'B', LDA >= KL+1; 
//*>             TYPE = 'Q', LDA >= KU+1; 
//*>             TYPE = 'Z', LDA >= 2*KL+KU+1. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          0  - successful exit 
//*>          <0 - if INFO = -i, the i-th argument had an illegal value. 
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
//*> \date June 2016 
//* 
//*> \ingroup OTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _2xvktk5a(FString _i7fnuqu5, ref Int32 _upl3k0xa, ref Int32 _9okdzrrx, ref Double _yo0pxhoi, ref Double _zf6gc2d3, ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, Double* _vxfgpup9, ref Int32 _ocv8fk5c, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Boolean _xzeqhzgj =  default;
Int32 _b5p6od9s =  default;
Int32 _842z9590 =  default;
Int32 _znpjgsef =  default;
Int32 _psyg0fin =  default;
Int32 _bddf4r7h =  default;
Int32 _lqqambp8 =  default;
Int32 _hrr2pyi4 =  default;
Double _av7j8yda =  default;
Double _ff1ud6ds =  default;
Double _q1pevapq =  default;
Double _6untp8no =  default;
Double _cz8iyp7m =  default;
Double _5j9y0nep =  default;
Double _bogm0gwy =  default;
Int32 _n8tvvlnh =  default;
string fLanavab = default;
#endregion  variable declarations
_i7fnuqu5 = _i7fnuqu5.Convert(1);

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     June 2016 
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
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input arguments 
		//* 
		
		_gro5yvfo = (int)0;//* 
		
		if (_w8y2rzgy(_i7fnuqu5 ,"G" ))
		{
			
			_842z9590 = (int)0;
		}
		else
		if (_w8y2rzgy(_i7fnuqu5 ,"L" ))
		{
			
			_842z9590 = (int)1;
		}
		else
		if (_w8y2rzgy(_i7fnuqu5 ,"U" ))
		{
			
			_842z9590 = (int)2;
		}
		else
		if (_w8y2rzgy(_i7fnuqu5 ,"H" ))
		{
			
			_842z9590 = (int)3;
		}
		else
		if (_w8y2rzgy(_i7fnuqu5 ,"B" ))
		{
			
			_842z9590 = (int)4;
		}
		else
		if (_w8y2rzgy(_i7fnuqu5 ,"Q" ))
		{
			
			_842z9590 = (int)5;
		}
		else
		if (_w8y2rzgy(_i7fnuqu5 ,"Z" ))
		{
			
			_842z9590 = (int)6;
		}
		else
		{
			
			_842z9590 = (int)-1;
		}
		//* 
		
		if (_842z9590 == (int)-1)
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if ((_yo0pxhoi == _d0547bi2) | _fk98jwhi(ref _yo0pxhoi ))
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if (_fk98jwhi(ref _zf6gc2d3 ))
		{
			
			_gro5yvfo = (int)-5;
		}
		else
		if (_ev4xhht5 < (int)0)
		{
			
			_gro5yvfo = (int)-6;
		}
		else
		if (((_dxpq0xkr < (int)0) | ((_842z9590 == (int)4) & (_dxpq0xkr != _ev4xhht5))) | ((_842z9590 == (int)5) & (_dxpq0xkr != _ev4xhht5)))
		{
			
			_gro5yvfo = (int)-7;
		}
		else
		if ((_842z9590 <= (int)3) & (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 )))
		{
			
			_gro5yvfo = (int)-9;
		}
		else
		if (_842z9590 >= (int)4)
		{
			
			if ((_upl3k0xa < (int)0) | (_upl3k0xa > ILNumerics.F2NET.Intrinsics.MAX(_ev4xhht5 - (int)1 ,(int)0 )))
			{
				
				_gro5yvfo = (int)-2;
			}
			else
			if (((_9okdzrrx < (int)0) | (_9okdzrrx > ILNumerics.F2NET.Intrinsics.MAX(_dxpq0xkr - (int)1 ,(int)0 ))) | (((_842z9590 == (int)4) | (_842z9590 == (int)5)) & (_upl3k0xa != _9okdzrrx)))
			{
				
				_gro5yvfo = (int)-3;
			}
			else
			if ((((_842z9590 == (int)4) & (_ocv8fk5c < (_upl3k0xa + (int)1))) | ((_842z9590 == (int)5) & (_ocv8fk5c < (_9okdzrrx + (int)1)))) | ((_842z9590 == (int)6) & (_ocv8fk5c < ((((int)2 * _upl3k0xa) + _9okdzrrx) + (int)1))))
			{
				
				_gro5yvfo = (int)-9;
			}
			
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("DLASCL" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if ((_dxpq0xkr == (int)0) | (_ev4xhht5 == (int)0))
		return;//* 
		//*     Get machine parameters 
		//* 
		
		_bogm0gwy = _f43eg0w0("S" );
		_av7j8yda = (_kxg5drh2 / _bogm0gwy);//* 
		
		_q1pevapq = _yo0pxhoi;
		_cz8iyp7m = _zf6gc2d3;//* 
		
Mark10:;
		// continue
		_ff1ud6ds = (_q1pevapq * _bogm0gwy);
		if (_ff1ud6ds == _q1pevapq)
		{
			//!        CFROMC is an inf.  Multiply by a correctly signed zero for 
			//!        finite CTOC, or a NaN if CTOC is infinite. 
			
			_5j9y0nep = (_cz8iyp7m / _q1pevapq);
			_xzeqhzgj = true;
			_6untp8no = _cz8iyp7m;
		}
		else
		{
			
			_6untp8no = (_cz8iyp7m / _av7j8yda);
			if (_6untp8no == _cz8iyp7m)
			{
				//!           CTOC is either 0 or an inf.  In both cases, CTOC itself 
				//!           serves as the correct multiplication factor. 
				
				_5j9y0nep = _cz8iyp7m;
				_xzeqhzgj = true;
				_q1pevapq = _kxg5drh2;
			}
			else
			if ((ILNumerics.F2NET.Intrinsics.ABS(_ff1ud6ds ) > ILNumerics.F2NET.Intrinsics.ABS(_cz8iyp7m )) & (_cz8iyp7m != _d0547bi2))
			{
				
				_5j9y0nep = _bogm0gwy;
				_xzeqhzgj = false;
				_q1pevapq = _ff1ud6ds;
			}
			else
			if (ILNumerics.F2NET.Intrinsics.ABS(_6untp8no ) > ILNumerics.F2NET.Intrinsics.ABS(_q1pevapq ))
			{
				
				_5j9y0nep = _av7j8yda;
				_xzeqhzgj = false;
				_cz8iyp7m = _6untp8no;
			}
			else
			{
				
				_5j9y0nep = (_cz8iyp7m / _q1pevapq);
				_xzeqhzgj = true;
			}
			
		}
		//* 
		
		if (_842z9590 == (int)0)
		{
			//* 
			//*        Full matrix 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn184 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step184 = (System.Int32)((int)1);
				System.Int32 __81fgg2count184;
				for (__81fgg2count184 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn184 + __81fgg2step184) / __81fgg2step184)), _znpjgsef = __81fgg2dlsvn184; __81fgg2count184 != 0; __81fgg2count184--, _znpjgsef += (__81fgg2step184)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn185 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step185 = (System.Int32)((int)1);
						System.Int32 __81fgg2count185;
						for (__81fgg2count185 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn185 + __81fgg2step185) / __81fgg2step185)), _b5p6od9s = __81fgg2dlsvn185; __81fgg2count185 != 0; __81fgg2count185--, _b5p6od9s += (__81fgg2step185)) {

						{
							
							*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * _5j9y0nep);
Mark20:;
							// continue
						}
												}					}
Mark30:;
					// continue
				}
								}			}//* 
			
		}
		else
		if (_842z9590 == (int)1)
		{
			//* 
			//*        Lower triangular matrix 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn186 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step186 = (System.Int32)((int)1);
				System.Int32 __81fgg2count186;
				for (__81fgg2count186 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn186 + __81fgg2step186) / __81fgg2step186)), _znpjgsef = __81fgg2dlsvn186; __81fgg2count186 != 0; __81fgg2count186--, _znpjgsef += (__81fgg2step186)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn187 = (System.Int32)(_znpjgsef);
						const System.Int32 __81fgg2step187 = (System.Int32)((int)1);
						System.Int32 __81fgg2count187;
						for (__81fgg2count187 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn187 + __81fgg2step187) / __81fgg2step187)), _b5p6od9s = __81fgg2dlsvn187; __81fgg2count187 != 0; __81fgg2count187--, _b5p6od9s += (__81fgg2step187)) {

						{
							
							*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * _5j9y0nep);
Mark40:;
							// continue
						}
												}					}
Mark50:;
					// continue
				}
								}			}//* 
			
		}
		else
		if (_842z9590 == (int)2)
		{
			//* 
			//*        Upper triangular matrix 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn188 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step188 = (System.Int32)((int)1);
				System.Int32 __81fgg2count188;
				for (__81fgg2count188 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn188 + __81fgg2step188) / __81fgg2step188)), _znpjgsef = __81fgg2dlsvn188; __81fgg2count188 != 0; __81fgg2count188--, _znpjgsef += (__81fgg2step188)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn189 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step189 = (System.Int32)((int)1);
						System.Int32 __81fgg2count189;
						for (__81fgg2count189 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN(_znpjgsef ,_ev4xhht5 )) - __81fgg2dlsvn189 + __81fgg2step189) / __81fgg2step189)), _b5p6od9s = __81fgg2dlsvn189; __81fgg2count189 != 0; __81fgg2count189--, _b5p6od9s += (__81fgg2step189)) {

						{
							
							*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * _5j9y0nep);
Mark60:;
							// continue
						}
												}					}
Mark70:;
					// continue
				}
								}			}//* 
			
		}
		else
		if (_842z9590 == (int)3)
		{
			//* 
			//*        Upper Hessenberg matrix 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn190 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step190 = (System.Int32)((int)1);
				System.Int32 __81fgg2count190;
				for (__81fgg2count190 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn190 + __81fgg2step190) / __81fgg2step190)), _znpjgsef = __81fgg2dlsvn190; __81fgg2count190 != 0; __81fgg2count190--, _znpjgsef += (__81fgg2step190)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn191 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step191 = (System.Int32)((int)1);
						System.Int32 __81fgg2count191;
						for (__81fgg2count191 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN(_znpjgsef + (int)1 ,_ev4xhht5 )) - __81fgg2dlsvn191 + __81fgg2step191) / __81fgg2step191)), _b5p6od9s = __81fgg2dlsvn191; __81fgg2count191 != 0; __81fgg2count191--, _b5p6od9s += (__81fgg2step191)) {

						{
							
							*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * _5j9y0nep);
Mark80:;
							// continue
						}
												}					}
Mark90:;
					// continue
				}
								}			}//* 
			
		}
		else
		if (_842z9590 == (int)4)
		{
			//* 
			//*        Lower half of a symmetric band matrix 
			//* 
			
			_lqqambp8 = (_upl3k0xa + (int)1);
			_hrr2pyi4 = (_dxpq0xkr + (int)1);
			{
				System.Int32 __81fgg2dlsvn192 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step192 = (System.Int32)((int)1);
				System.Int32 __81fgg2count192;
				for (__81fgg2count192 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn192 + __81fgg2step192) / __81fgg2step192)), _znpjgsef = __81fgg2dlsvn192; __81fgg2count192 != 0; __81fgg2count192--, _znpjgsef += (__81fgg2step192)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn193 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step193 = (System.Int32)((int)1);
						System.Int32 __81fgg2count193;
						for (__81fgg2count193 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN(_lqqambp8 ,_hrr2pyi4 - _znpjgsef )) - __81fgg2dlsvn193 + __81fgg2step193) / __81fgg2step193)), _b5p6od9s = __81fgg2dlsvn193; __81fgg2count193 != 0; __81fgg2count193--, _b5p6od9s += (__81fgg2step193)) {

						{
							
							*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * _5j9y0nep);
Mark100:;
							// continue
						}
												}					}
Mark110:;
					// continue
				}
								}			}//* 
			
		}
		else
		if (_842z9590 == (int)5)
		{
			//* 
			//*        Upper half of a symmetric band matrix 
			//* 
			
			_psyg0fin = (_9okdzrrx + (int)2);
			_lqqambp8 = (_9okdzrrx + (int)1);
			{
				System.Int32 __81fgg2dlsvn194 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step194 = (System.Int32)((int)1);
				System.Int32 __81fgg2count194;
				for (__81fgg2count194 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn194 + __81fgg2step194) / __81fgg2step194)), _znpjgsef = __81fgg2dlsvn194; __81fgg2count194 != 0; __81fgg2count194--, _znpjgsef += (__81fgg2step194)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn195 = (System.Int32)(ILNumerics.F2NET.Intrinsics.MAX(_psyg0fin - _znpjgsef ,(int)1 ));
						const System.Int32 __81fgg2step195 = (System.Int32)((int)1);
						System.Int32 __81fgg2count195;
						for (__81fgg2count195 = System.Math.Max(0, (System.Int32)(((System.Int32)(_lqqambp8) - __81fgg2dlsvn195 + __81fgg2step195) / __81fgg2step195)), _b5p6od9s = __81fgg2dlsvn195; __81fgg2count195 != 0; __81fgg2count195--, _b5p6od9s += (__81fgg2step195)) {

						{
							
							*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * _5j9y0nep);
Mark120:;
							// continue
						}
												}					}
Mark130:;
					// continue
				}
								}			}//* 
			
		}
		else
		if (_842z9590 == (int)6)
		{
			//* 
			//*        Band matrix 
			//* 
			
			_psyg0fin = ((_upl3k0xa + _9okdzrrx) + (int)2);
			_bddf4r7h = (_upl3k0xa + (int)1);
			_lqqambp8 = ((((int)2 * _upl3k0xa) + _9okdzrrx) + (int)1);
			_hrr2pyi4 = (((_upl3k0xa + _9okdzrrx) + (int)1) + _ev4xhht5);
			{
				System.Int32 __81fgg2dlsvn196 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step196 = (System.Int32)((int)1);
				System.Int32 __81fgg2count196;
				for (__81fgg2count196 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn196 + __81fgg2step196) / __81fgg2step196)), _znpjgsef = __81fgg2dlsvn196; __81fgg2count196 != 0; __81fgg2count196--, _znpjgsef += (__81fgg2step196)) {

				{
					
					{
						System.Int32 __81fgg2dlsvn197 = (System.Int32)(ILNumerics.F2NET.Intrinsics.MAX(_psyg0fin - _znpjgsef ,_bddf4r7h ));
						const System.Int32 __81fgg2step197 = (System.Int32)((int)1);
						System.Int32 __81fgg2count197;
						for (__81fgg2count197 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN(_lqqambp8 ,_hrr2pyi4 - _znpjgsef )) - __81fgg2dlsvn197 + __81fgg2step197) / __81fgg2step197)), _b5p6od9s = __81fgg2dlsvn197; __81fgg2count197 != 0; __81fgg2count197--, _b5p6od9s += (__81fgg2step197)) {

						{
							
							*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) = (*(_vxfgpup9+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)) * _5j9y0nep);
Mark140:;
							// continue
						}
												}					}
Mark150:;
					// continue
				}
								}			}//* 
			
		}
		//* 
		
		if (!(_xzeqhzgj))goto Mark10;//* 
		
		return;//* 
		//*     End of DLASCL 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
