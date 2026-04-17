
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
//*> \brief \b CLAHR2 reduces the specified number of first columns of a general rectangular matrix A so that elements below the specified subdiagonal are zero, and returns auxiliary matrices which are needed to apply the transformation to the unreduced part of A. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download CLAHR2 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/clahr2.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/clahr2.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/clahr2.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE CLAHR2( N, K, NB, A, LDA, TAU, T, LDT, Y, LDY ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            K, LDA, LDT, LDY, N, NB 
//*       .. 
//*       .. Array Arguments .. 
//*       COMPLEX            A( LDA, * ), T( LDT, NB ), TAU( NB ), 
//*      $                   Y( LDY, NB ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> CLAHR2 reduces the first NB columns of A complex general n-BY-(n-k+1) 
//*> matrix A so that elements below the k-th subdiagonal are zero. The 
//*> reduction is performed by an unitary similarity transformation 
//*> Q**H * A * Q. The routine returns the matrices V and T which determine 
//*> Q as a block reflector I - V*T*v**H, and also the matrix Y = A * V * T. 
//*> 
//*> This is an auxiliary routine called by CGEHRD. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix A. 
//*> \endverbatim 
//*> 
//*> \param[in] K 
//*> \verbatim 
//*>          K is INTEGER 
//*>          The offset for the reduction. Elements below the k-th 
//*>          subdiagonal in the first NB columns are reduced to zero. 
//*>          K < N. 
//*> \endverbatim 
//*> 
//*> \param[in] NB 
//*> \verbatim 
//*>          NB is INTEGER 
//*>          The number of columns to be reduced. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is COMPLEX array, dimension (LDA,N-K+1) 
//*>          On entry, the n-by-(n-k+1) general matrix A. 
//*>          On exit, the elements on and above the k-th subdiagonal in 
//*>          the first NB columns are overwritten with the corresponding 
//*>          elements of the reduced matrix; the elements below the k-th 
//*>          subdiagonal, with the array TAU, represent the matrix Q as a 
//*>          product of elementary reflectors. The other columns of A are 
//*>          unchanged. See Further Details. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[out] TAU 
//*> \verbatim 
//*>          TAU is COMPLEX array, dimension (NB) 
//*>          The scalar factors of the elementary reflectors. See Further 
//*>          Details. 
//*> \endverbatim 
//*> 
//*> \param[out] T 
//*> \verbatim 
//*>          T is COMPLEX array, dimension (LDT,NB) 
//*>          The upper triangular matrix T. 
//*> \endverbatim 
//*> 
//*> \param[in] LDT 
//*> \verbatim 
//*>          LDT is INTEGER 
//*>          The leading dimension of the array T.  LDT >= NB. 
//*> \endverbatim 
//*> 
//*> \param[out] Y 
//*> \verbatim 
//*>          Y is COMPLEX array, dimension (LDY,NB) 
//*>          The n-by-nb matrix Y. 
//*> \endverbatim 
//*> 
//*> \param[in] LDY 
//*> \verbatim 
//*>          LDY is INTEGER 
//*>          The leading dimension of the array Y. LDY >= N. 
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
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  The matrix Q is represented as a product of nb elementary reflectors 
//*> 
//*>     Q = H(1) H(2) . . . H(nb). 
//*> 
//*>  Each H(i) has the form 
//*> 
//*>     H(i) = I - tau * v * v**H 
//*> 
//*>  where tau is a complex scalar, and v is a complex vector with 
//*>  v(1:i+k-1) = 0, v(i+k) = 1; v(i+k+1:n) is stored on exit in 
//*>  A(i+k+1:n,i), and tau in TAU(i). 
//*> 
//*>  The elements of the vectors v together form the (n-k+1)-by-nb matrix 
//*>  V which is needed, with T and Y, to apply the transformation to the 
//*>  unreduced part of the matrix, using an update of the form: 
//*>  A := (I - V*T*V**H) * (A - Y*V**H). 
//*> 
//*>  The contents of A on exit are illustrated by the following example 
//*>  with n = 7, k = 3 and nb = 2: 
//*> 
//*>     ( a   a   a   a   a ) 
//*>     ( a   a   a   a   a ) 
//*>     ( a   a   a   a   a ) 
//*>     ( h   h   a   a   a ) 
//*>     ( v1  h   a   a   a ) 
//*>     ( v1  v2  a   a   a ) 
//*>     ( v1  v2  a   a   a ) 
//*> 
//*>  where a denotes an element of the original matrix A, h denotes a 
//*>  modified element of the upper Hessenberg matrix H, and vi denotes an 
//*>  element of the vector defining H(i). 
//*> 
//*>  This subroutine is a slight modification of LAPACK-3.0's DLAHRD 
//*>  incorporating improvements proposed by Quintana-Orti and Van de 
//*>  Gejin. Note that the entries of A(1:K,2:NB) differ from those 
//*>  returned by the original LAPACK-3.0's DLAHRD routine. (This 
//*>  subroutine is not backward compatible with LAPACK-3.0's DLAHRD.) 
//*> \endverbatim 
//* 
//*> \par References: 
//*  ================ 
//*> 
//*>  Gregorio Quintana-Orti and Robert van de Geijn, "Improving the 
//*>  performance of reduction to Hessenberg form," ACM Transactions on 
//*>  Mathematical Software, 32(2):180-194, June 2006. 
//*> 
//*  ===================================================================== 

	 
	public static void _t19jx8hp(ref Int32 _dxpq0xkr, ref Int32 _umlkckdg, ref Int32 _f7059815, fcomplex* _vxfgpup9, ref Int32 _ocv8fk5c, fcomplex* _0446f4de, fcomplex* _2ivtt43r, ref Int32 _w8yhbr2r, fcomplex* _f3z3edv0, ref Int32 _92z7u0w4)
	{
#region variable declarations
fcomplex _d0547bi2 =   new fcomplex(0f,0f);
fcomplex _kxg5drh2 =   new fcomplex(1f,0f);
Int32 _b5p6od9s =  default;
fcomplex _85pvcc5e =  default;
string fLanavab = default;
#endregion  variable declarations

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
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr <= (int)1)
		return;//* 
		
		{
			System.Int32 __81fgg2dlsvn2524 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2524 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2524;
			for (__81fgg2count2524 = System.Math.Max(0, (System.Int32)(((System.Int32)(_f7059815) - __81fgg2dlsvn2524 + __81fgg2step2524) / __81fgg2step2524)), _b5p6od9s = __81fgg2dlsvn2524; __81fgg2count2524 != 0; __81fgg2count2524--, _b5p6od9s += (__81fgg2step2524)) {

			{
				
				if (_b5p6od9s > (int)1)
				{
					//* 
					//*           Update A(K+1:N,I) 
					//* 
					//*           Update I-th column of A - Y * V**H 
					//* 
					
					_png2g84j(ref Unsafe.AsRef(_b5p6od9s - (int)1) ,(_vxfgpup9+((_umlkckdg + _b5p6od9s) - (int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );
					_f0oh3lvv("NO TRANSPOSE" ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_f3z3edv0+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_92z7u0w4)),ref _92z7u0w4 ,(_vxfgpup9+((_umlkckdg + _b5p6od9s) - (int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_umlkckdg + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
					_png2g84j(ref Unsafe.AsRef(_b5p6od9s - (int)1) ,(_vxfgpup9+((_umlkckdg + _b5p6od9s) - (int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c );//* 
					//*           Apply I - V * T**H * V**H to this column (call it b) from the 
					//*           left, using the last column of T as workspace 
					//* 
					//*           Let  V = ( V1 )   and   b = ( b1 )   (first I-1 rows) 
					//*                    ( V2 )             ( b2 ) 
					//* 
					//*           where V1 is unit lower triangular 
					//* 
					//*           w := V1**H * b1 
					//* 
					
					_33e0jk6i(ref Unsafe.AsRef(_b5p6od9s - (int)1) ,(_vxfgpup9+(_umlkckdg + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,(_2ivtt43r+((int)1 - 1) + (_f7059815 - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) );
					_09cah3zx("Lower" ,"Conjugate transpose" ,"UNIT" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,(_vxfgpup9+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_2ivtt43r+((int)1 - 1) + (_f7059815 - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) );//* 
					//*           w := w + V2**H * b2 
					//* 
					
					_f0oh3lvv("Conjugate transpose" ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_umlkckdg + _b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_umlkckdg + _b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_2ivtt43r+((int)1 - 1) + (_f7059815 - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) );//* 
					//*           w := T**H * w 
					//* 
					
					_09cah3zx("Upper" ,"Conjugate transpose" ,"NON-UNIT" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,_2ivtt43r ,ref _w8yhbr2r ,(_2ivtt43r+((int)1 - 1) + (_f7059815 - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) );//* 
					//*           b2 := b2 - V2*w 
					//* 
					
					_f0oh3lvv("NO TRANSPOSE" ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_vxfgpup9+(_umlkckdg + _b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_2ivtt43r+((int)1 - 1) + (_f7059815 - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_umlkckdg + _b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );//* 
					//*           b1 := b1 - V1*w 
					//* 
					
					_09cah3zx("Lower" ,"NO TRANSPOSE" ,"UNIT" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,(_vxfgpup9+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_2ivtt43r+((int)1 - 1) + (_f7059815 - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) );
					_0vz4nsob(ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_2ivtt43r+((int)1 - 1) + (_f7059815 - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_vxfgpup9+(_umlkckdg + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );//* 
					
					*(_vxfgpup9+((_umlkckdg + _b5p6od9s) - (int)1 - 1) + (_b5p6od9s - (int)1 - 1) * 1 * (_ocv8fk5c)) = _85pvcc5e;
				}
				//* 
				//*        Generate the elementary reflector H(I) to annihilate 
				//*        A(K+I+1:N,I) 
				//* 
				
				_ocp87dc1(ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(*(_vxfgpup9+(_umlkckdg + _b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c))) ,(_vxfgpup9+(ILNumerics.F2NET.Intrinsics.MIN((_umlkckdg + _b5p6od9s) + (int)1 ,_dxpq0xkr ) - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_0446f4de+(_b5p6od9s - 1))) );
				_85pvcc5e = *(_vxfgpup9+(_umlkckdg + _b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c));
				*(_vxfgpup9+(_umlkckdg + _b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)) = _kxg5drh2;//* 
				//*        Compute  Y(K+1:N,I) 
				//* 
				
				_f0oh3lvv("NO TRANSPOSE" ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_umlkckdg + (int)1 - 1) + (_b5p6od9s + (int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_umlkckdg + _b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_f3z3edv0+(_umlkckdg + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_92z7u0w4)),ref Unsafe.AsRef((int)1) );
				_f0oh3lvv("Conjugate transpose" ,ref Unsafe.AsRef(((_dxpq0xkr - _umlkckdg) - _b5p6od9s) + (int)1) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_umlkckdg + _b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+(_umlkckdg + _b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_d0547bi2) ,(_2ivtt43r+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) );
				_f0oh3lvv("NO TRANSPOSE" ,ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(-(_kxg5drh2)) ,(_f3z3edv0+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_92z7u0w4)),ref _92z7u0w4 ,(_2ivtt43r+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_f3z3edv0+(_umlkckdg + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_92z7u0w4)),ref Unsafe.AsRef((int)1) );
				_00l5hgpk(ref Unsafe.AsRef(_dxpq0xkr - _umlkckdg) ,ref Unsafe.AsRef(*(_0446f4de+(_b5p6od9s - 1))) ,(_f3z3edv0+(_umlkckdg + (int)1 - 1) + (_b5p6od9s - 1) * 1 * (_92z7u0w4)),ref Unsafe.AsRef((int)1) );//* 
				//*        Compute T(1:I,I) 
				//* 
				
				_00l5hgpk(ref Unsafe.AsRef(_b5p6od9s - (int)1) ,ref Unsafe.AsRef(-(*(_0446f4de+(_b5p6od9s - 1)))) ,(_2ivtt43r+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) );
				_09cah3zx("Upper" ,"No Transpose" ,"NON-UNIT" ,ref Unsafe.AsRef(_b5p6od9s - (int)1) ,_2ivtt43r ,ref _w8yhbr2r ,(_2ivtt43r+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) );
				*(_2ivtt43r+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * (_w8yhbr2r)) = *(_0446f4de+(_b5p6od9s - 1));//* 
				
Mark10:;
				// continue
			}
						}		}
		*(_vxfgpup9+(_umlkckdg + _f7059815 - 1) + (_f7059815 - 1) * 1 * (_ocv8fk5c)) = _85pvcc5e;//* 
		//*     Compute Y(1:K,1:NB) 
		//* 
		
		_szaic8qw("ALL" ,ref _umlkckdg ,ref _f7059815 ,(_vxfgpup9+((int)1 - 1) + ((int)2 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_f3z3edv0 ,ref _92z7u0w4 );
		_smeynpn3("RIGHT" ,"Lower" ,"NO TRANSPOSE" ,"UNIT" ,ref _umlkckdg ,ref _f7059815 ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+(_umlkckdg + (int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,_f3z3edv0 ,ref _92z7u0w4 );
		if (_dxpq0xkr > (_umlkckdg + _f7059815))
		_5p0w9905("NO TRANSPOSE" ,"NO TRANSPOSE" ,ref _umlkckdg ,ref _f7059815 ,ref Unsafe.AsRef((_dxpq0xkr - _umlkckdg) - _f7059815) ,ref Unsafe.AsRef(_kxg5drh2) ,(_vxfgpup9+((int)1 - 1) + ((int)2 + _f7059815 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,(_vxfgpup9+((_umlkckdg + (int)1) + _f7059815 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)),ref _ocv8fk5c ,ref Unsafe.AsRef(_kxg5drh2) ,_f3z3edv0 ,ref _92z7u0w4 );
		_smeynpn3("RIGHT" ,"Upper" ,"NO TRANSPOSE" ,"NON-UNIT" ,ref _umlkckdg ,ref _f7059815 ,ref Unsafe.AsRef(_kxg5drh2) ,_2ivtt43r ,ref _w8yhbr2r ,_f3z3edv0 ,ref _92z7u0w4 );//* 
		
		return;//* 
		//*     End of CLAHR2 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
