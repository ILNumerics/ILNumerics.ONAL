
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
//*> \brief \b ZGEBAK 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZGEBAK + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zgebak.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zgebak.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zgebak.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZGEBAK( JOB, SIDE, N, ILO, IHI, SCALE, M, V, LDV, 
//*                          INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          JOB, SIDE 
//*       INTEGER            IHI, ILO, INFO, LDV, M, N 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   SCALE( * ) 
//*       COMPLEX*16         V( LDV, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZGEBAK forms the right or left eigenvectors of a complex general 
//*> matrix by backward transformation on the computed eigenvectors of the 
//*> balanced matrix output by ZGEBAL. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] JOB 
//*> \verbatim 
//*>          JOB is CHARACTER*1 
//*>          Specifies the type of backward transformation required: 
//*>          = 'N': do nothing, return immediately; 
//*>          = 'P': do backward transformation for permutation only; 
//*>          = 'S': do backward transformation for scaling only; 
//*>          = 'B': do backward transformations for both permutation and 
//*>                 scaling. 
//*>          JOB must be the same as the argument JOB supplied to ZGEBAL. 
//*> \endverbatim 
//*> 
//*> \param[in] SIDE 
//*> \verbatim 
//*>          SIDE is CHARACTER*1 
//*>          = 'R':  V contains right eigenvectors; 
//*>          = 'L':  V contains left eigenvectors. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The number of rows of the matrix V.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] ILO 
//*> \verbatim 
//*>          ILO is INTEGER 
//*> \endverbatim 
//*> 
//*> \param[in] IHI 
//*> \verbatim 
//*>          IHI is INTEGER 
//*>          The integers ILO and IHI determined by ZGEBAL. 
//*>          1 <= ILO <= IHI <= N, if N > 0; ILO=1 and IHI=0, if N=0. 
//*> \endverbatim 
//*> 
//*> \param[in] SCALE 
//*> \verbatim 
//*>          SCALE is DOUBLE PRECISION array, dimension (N) 
//*>          Details of the permutation and scaling factors, as returned 
//*>          by ZGEBAL. 
//*> \endverbatim 
//*> 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of columns of the matrix V.  M >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] V 
//*> \verbatim 
//*>          V is COMPLEX*16 array, dimension (LDV,M) 
//*>          On entry, the matrix of right or left eigenvectors to be 
//*>          transformed, as returned by ZHSEIN or ZTREVC. 
//*>          On exit, V is overwritten by the transformed eigenvectors. 
//*> \endverbatim 
//*> 
//*> \param[in] LDV 
//*> \verbatim 
//*>          LDV is INTEGER 
//*>          The leading dimension of the array V. LDV >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit 
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
//*> \ingroup complex16GEcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _ojkt64cy(FString _xcrv93xi, FString _m2cn2gjg, ref Int32 _dxpq0xkr, ref Int32 _pew3blan, ref Int32 _9c1csucx, Double* _1m44vtuk, ref Int32 _ev4xhht5, complex* _ycxba85s, ref Int32 _ys09rxze, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _kxg5drh2 =  1d;
Boolean _35js00fx =  default;
Boolean _yj0w80gr =  default;
Int32 _b5p6od9s =  default;
Int32 _retbwjxi =  default;
Int32 _umlkckdg =  default;
Double _irk8i6qr =  default;
string fLanavab = default;
#endregion  variable declarations
_xcrv93xi = _xcrv93xi.Convert(1);
_m2cn2gjg = _m2cn2gjg.Convert(1);

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
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Decode and Test the input parameters 
		//* 
		
		_yj0w80gr = _w8y2rzgy(_m2cn2gjg ,"R" );
		_35js00fx = _w8y2rzgy(_m2cn2gjg ,"L" );//* 
		
		_gro5yvfo = (int)0;
		if ((((!(_w8y2rzgy(_xcrv93xi ,"N" ))) & (!(_w8y2rzgy(_xcrv93xi ,"P" )))) & (!(_w8y2rzgy(_xcrv93xi ,"S" )))) & (!(_w8y2rzgy(_xcrv93xi ,"B" ))))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if ((!(_yj0w80gr)) & (!(_35js00fx)))
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if ((_pew3blan < (int)1) | (_pew3blan > ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr )))
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if ((_9c1csucx < ILNumerics.F2NET.Intrinsics.MIN(_pew3blan ,_dxpq0xkr )) | (_9c1csucx > _dxpq0xkr))
		{
			
			_gro5yvfo = (int)-5;
		}
		else
		if (_ev4xhht5 < (int)0)
		{
			
			_gro5yvfo = (int)-7;
		}
		else
		if (_ys09rxze < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)-9;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("ZGEBAK" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr == (int)0)
		return;
		if (_ev4xhht5 == (int)0)
		return;
		if (_w8y2rzgy(_xcrv93xi ,"N" ))
		return;//* 
		
		if (_pew3blan == _9c1csucx)goto Mark30;//* 
		//*     Backward balance 
		//* 
		
		if (_w8y2rzgy(_xcrv93xi ,"S" ) | _w8y2rzgy(_xcrv93xi ,"B" ))
		{
			//* 
			
			if (_yj0w80gr)
			{
				
				{
					System.Int32 __81fgg2dlsvn2659 = (System.Int32)(_pew3blan);
					const System.Int32 __81fgg2step2659 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2659;
					for (__81fgg2count2659 = System.Math.Max(0, (System.Int32)(((System.Int32)(_9c1csucx) - __81fgg2dlsvn2659 + __81fgg2step2659) / __81fgg2step2659)), _b5p6od9s = __81fgg2dlsvn2659; __81fgg2count2659 != 0; __81fgg2count2659--, _b5p6od9s += (__81fgg2step2659)) {

					{
						
						_irk8i6qr = *(_1m44vtuk+(_b5p6od9s - 1));
						_z5tkm94d(ref _ev4xhht5 ,ref _irk8i6qr ,(_ycxba85s+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze );
Mark10:;
						// continue
					}
										}				}
			}
			//* 
			
			if (_35js00fx)
			{
				
				{
					System.Int32 __81fgg2dlsvn2660 = (System.Int32)(_pew3blan);
					const System.Int32 __81fgg2step2660 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2660;
					for (__81fgg2count2660 = System.Math.Max(0, (System.Int32)(((System.Int32)(_9c1csucx) - __81fgg2dlsvn2660 + __81fgg2step2660) / __81fgg2step2660)), _b5p6od9s = __81fgg2dlsvn2660; __81fgg2count2660 != 0; __81fgg2count2660--, _b5p6od9s += (__81fgg2step2660)) {

					{
						
						_irk8i6qr = (_kxg5drh2 / *(_1m44vtuk+(_b5p6od9s - 1)));
						_z5tkm94d(ref _ev4xhht5 ,ref _irk8i6qr ,(_ycxba85s+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze );
Mark20:;
						// continue
					}
										}				}
			}
			//* 
			
		}
		//* 
		//*     Backward permutation 
		//* 
		//*     For  I = ILO-1 step -1 until 1, 
		//*              IHI+1 step 1 until N do -- 
		//* 
		
Mark30:;
		// continue
		if (_w8y2rzgy(_xcrv93xi ,"P" ) | _w8y2rzgy(_xcrv93xi ,"B" ))
		{
			
			if (_yj0w80gr)
			{
				
				{
					System.Int32 __81fgg2dlsvn2661 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2661 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2661;
					for (__81fgg2count2661 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2661 + __81fgg2step2661) / __81fgg2step2661)), _retbwjxi = __81fgg2dlsvn2661; __81fgg2count2661 != 0; __81fgg2count2661--, _retbwjxi += (__81fgg2step2661)) {

					{
						
						_b5p6od9s = _retbwjxi;
						if ((_b5p6od9s >= _pew3blan) & (_b5p6od9s <= _9c1csucx))goto Mark40;
						if (_b5p6od9s < _pew3blan)
						_b5p6od9s = (_pew3blan - _retbwjxi);
						_umlkckdg = INT(*(_1m44vtuk+(_b5p6od9s - 1)));
						if (_umlkckdg == _b5p6od9s)goto Mark40;
						_027ahmgj(ref _ev4xhht5 ,(_ycxba85s+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,(_ycxba85s+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze );
Mark40:;
						// continue
					}
										}				}
			}
			//* 
			
			if (_35js00fx)
			{
				
				{
					System.Int32 __81fgg2dlsvn2662 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2662 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2662;
					for (__81fgg2count2662 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2662 + __81fgg2step2662) / __81fgg2step2662)), _retbwjxi = __81fgg2dlsvn2662; __81fgg2count2662 != 0; __81fgg2count2662--, _retbwjxi += (__81fgg2step2662)) {

					{
						
						_b5p6od9s = _retbwjxi;
						if ((_b5p6od9s >= _pew3blan) & (_b5p6od9s <= _9c1csucx))goto Mark50;
						if (_b5p6od9s < _pew3blan)
						_b5p6od9s = (_pew3blan - _retbwjxi);
						_umlkckdg = INT(*(_1m44vtuk+(_b5p6od9s - 1)));
						if (_umlkckdg == _b5p6od9s)goto Mark50;
						_027ahmgj(ref _ev4xhht5 ,(_ycxba85s+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze ,(_ycxba85s+(_umlkckdg - 1) + ((int)1 - 1) * 1 * (_ys09rxze)),ref _ys09rxze );
Mark50:;
						// continue
					}
										}				}
			}
			
		}
		//* 
		
		return;//* 
		//*     End of ZGEBAK 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
