
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
//*> \brief \b CGETRF2 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       RECURSIVE SUBROUTINE CGETRF2( M, N, A, LDA, IPIV, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            INFO, LDA, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IPIV( * ) 
//*       COMPLEX            A( LDA, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CGETRF2 computes an LU factorization of a general M-by-N matrix A 
//*> using partial pivoting with row interchanges. 
//*> 
//*> The factorization has the form 
//*>    A = P * L * U 
//*> where P is a permutation matrix, L is lower triangular with unit 
//*> diagonal elements (lower trapezoidal if m > n), and U is upper 
//*> triangular (upper trapezoidal if m < n). 
//*> 
//*> This is the recursive version of the algorithm. It divides 
//*> the matrix into four submatrices: 
//*> 
//*>        [  A11 | A12  ]  where A11 is n1 by n1 and A22 is n2 by n2 
//*>    A = [ -----|----- ]  with n1 = min(m,n)/2 
//*>        [  A21 | A22  ]       n2 = n-n1 
//*> 
//*>                                       [ A11 ] 
//*> The subroutine calls itself to factor [ --- ], 
//*>                                       [ A12 ] 
//*>                 [ A12 ] 
//*> do the swaps on [ --- ], solve A12, update A22, 
//*>                 [ A22 ] 
//*> 
//*> then calls itself to factor A22 and do the swaps on A21. 
//*> 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
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
//*>          A is COMPLEX array, dimension (LDA,N) 
//*>          On entry, the M-by-N matrix to be factored. 
//*>          On exit, the factors L and U from the factorization 
//*>          A = P*L*U; the unit diagonal elements of L are not stored. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,M). 
//*> \endverbatim 
//*> 
//*> \param[out] IPIV 
//*> \verbatim 
//*>          IPIV is INTEGER array, dimension (min(M,N)) 
//*>          The pivot indices; for 1 <= i <= min(M,N), row i of the 
//*>          matrix was interchanged with row IPIV(i). 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value 
//*>          > 0:  if INFO = i, U(i,i) is exactly zero. The factorization 
//*>                has been completed, but the factor U is exactly 
//*>                singular, and division by zero will occur if it is used 
//*>                to solve a system of equations. 
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
//*> \ingroup complexGEcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _61ogh5t9(ref Int32 _ev4xhht5, ref Int32 _dxpq0xkr, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, Int32* _w1ilvusp, ref Int32 _gro5yvfo)
	{
#region variable declarations
fcomplex _kxg5drh2 =   new fcomplex(1f,0f);
fcomplex _d0547bi2 =   new fcomplex(0f,0f);
Single _ptpa0vax =  default;
fcomplex _1ajfmh55 =  default;
Int32 _b5p6od9s =  default;
Int32 _itfnbz60 =  default;
Int32 _4o1bt8b1 =  default;
Int32 _tixk7d1h =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK computational routine (version 3.7.0) -- 
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
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input parameters 
		//* 
		
		_gro5yvfo = (int)0;
		if (_ev4xhht5 < (int)0)
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_ev4xhht5 ))
		{
			
			_gro5yvfo = (int)-4;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("CGETRF2" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if ((_ev4xhht5 == (int)0) | (_dxpq0xkr == (int)0))
		return;// 
		
		if (_ev4xhht5 == (int)1)
		{
			//* 
			//*        Use unblocked code for one row case 
			//*        Just need to handle IPIV and INFO 
			//* 
			
			*(_w1ilvusp+((int)1 - 1)) = (int)1;
			if (*(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) == _d0547bi2)
			_gro5yvfo = (int)1;//* 
			
		}
		else
		if (_dxpq0xkr == (int)1)
		{
			//* 
			//*        Use unblocked code for one column case 
			//* 
			//* 
			//*        Compute machine safe minimum 
			//* 
			
			_ptpa0vax = _d5tu038y("S" );//* 
			//*        Find pivot and test for singularity 
			//* 
			
			_b5p6od9s = _r3truie3(ref _ev4xhht5 ,(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
			*(_w1ilvusp+((int)1 - 1)) = _b5p6od9s;
			if (*(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) != _d0547bi2)
			{
				//* 
				//*           Apply the interchange 
				//* 
				
				if (_b5p6od9s != (int)1)
				{
					
					_1ajfmh55 = *(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c));
					*(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) = *(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c));
					*(_vxfgpup9+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) = _1ajfmh55;
				}
				//* 
				//*           Compute elements 2:M of the column 
				//* 
				
				if (ILNumerics.F2NET.Intrinsics.ABS(*(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) ) >= _ptpa0vax)
				{
					
					_00l5hgpk(ref Unsafe.AsRef(_ev4xhht5 - (int)1) ,ref Unsafe.AsRef(_kxg5drh2 / *(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c))) ,(_vxfgpup9+((int)2 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
				}
				else
				{
					
					{
						System.Int32 __81fgg2dlsvn1780 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step1780 = (System.Int32)((int)1);
						System.Int32 __81fgg2count1780;
						for (__81fgg2count1780 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5 - (int)1) - __81fgg2dlsvn1780 + __81fgg2step1780) / __81fgg2step1780)), _b5p6od9s = __81fgg2dlsvn1780; __81fgg2count1780 != 0; __81fgg2count1780--, _b5p6od9s += (__81fgg2step1780)) {

						{
							
							*(_vxfgpup9+((int)1 + _b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) = (*(_vxfgpup9+((int)1 + _b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) / *(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)));
Mark10:;
							// continue
						}
												}					}
				}
				//* 
				
			}
			else
			{
				
				_gro5yvfo = (int)1;
			}
			//* 
			
		}
		else
		{
			//* 
			//*        Use recursive code 
			//* 
			
			_4o1bt8b1 = (ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_dxpq0xkr ) / (int)2);
			_tixk7d1h = (_dxpq0xkr - _4o1bt8b1);//* 
			//*               [ A11 ] 
			//*        Factor [ --- ] 
			//*               [ A21 ] 
			//* 
			
			_61ogh5t9(ref _ev4xhht5 ,ref _4o1bt8b1 ,_vxfgpup9 ,ref _ocv8fk5c ,_w1ilvusp ,ref _itfnbz60 );// 
			
			if ((_gro5yvfo == (int)0) & (_itfnbz60 > (int)0))
			_gro5yvfo = _itfnbz60;//* 
			//*                              [ A12 ] 
			//*        Apply interchanges to [ --- ] 
			//*                              [ A22 ] 
			//* 
			
			_iuxulk3d(ref _tixk7d1h ,(_vxfgpup9+((int)1 - 1) + (_4o1bt8b1 + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef((int)1) ,ref _4o1bt8b1 ,_w1ilvusp ,ref Unsafe.AsRef((int)1) );//* 
			//*        Solve A12 
			//* 
			
			_goj1gzmg("L" ,"L" ,"N" ,"U" ,ref _4o1bt8b1 ,ref _tixk7d1h ,ref Unsafe.AsRef(_kxg5drh2) ,_vxfgpup9 ,ref _ocv8fk5c ,(_vxfgpup9+((int)1 - 1) + (_4o1bt8b1 + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );//* 
			//*        Update A22 
			//* 
			
			_5p0w9905("N" ,"N" ,ref Unsafe.AsRef(_ev4xhht5 - _4o1bt8b1) ,ref _tixk7d1h ,ref _4o1bt8b1 ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+(_4o1bt8b1 + (int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+((int)1 - 1) + (_4o1bt8b1 + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_4o1bt8b1 + (int)1 - 1) + (_4o1bt8b1 + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );//* 
			//*        Factor A22 
			//* 
			
			_61ogh5t9(ref Unsafe.AsRef(_ev4xhht5 - _4o1bt8b1) ,ref _tixk7d1h ,(_vxfgpup9+(_4o1bt8b1 + (int)1 - 1) + (_4o1bt8b1 + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_w1ilvusp+(_4o1bt8b1 + (int)1 - 1)),ref _itfnbz60 );//* 
			//*        Adjust INFO and the pivot indices 
			//* 
			
			if ((_gro5yvfo == (int)0) & (_itfnbz60 > (int)0))
			_gro5yvfo = (_itfnbz60 + _4o1bt8b1);
			{
				System.Int32 __81fgg2dlsvn1781 = (System.Int32)((_4o1bt8b1 + (int)1));
				const System.Int32 __81fgg2step1781 = (System.Int32)((int)1);
				System.Int32 __81fgg2count1781;
				for (__81fgg2count1781 = System.Math.Max(0, (System.Int32)(((System.Int32)(ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_dxpq0xkr )) - __81fgg2dlsvn1781 + __81fgg2step1781) / __81fgg2step1781)), _b5p6od9s = __81fgg2dlsvn1781; __81fgg2count1781 != 0; __81fgg2count1781--, _b5p6od9s += (__81fgg2step1781)) {

				{
					
					*(_w1ilvusp+(_b5p6od9s - 1)) = (*(_w1ilvusp+(_b5p6od9s - 1)) + _4o1bt8b1);
Mark20:;
					// continue
				}
								}			}//* 
			//*        Apply interchanges to A21 
			//* 
			
			_iuxulk3d(ref _4o1bt8b1 ,(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_4o1bt8b1 + (int)1) ,ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.MIN(_ev4xhht5 ,_dxpq0xkr )) ,_w1ilvusp ,ref Unsafe.AsRef((int)1) );//* 
			
		}
		
		return;//* 
		//*     End of CGETRF2 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
