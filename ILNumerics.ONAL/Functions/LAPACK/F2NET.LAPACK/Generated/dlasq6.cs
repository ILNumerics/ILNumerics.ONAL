
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
//*> \brief \b DLASQ6 computes one dqd transform in ping-pong form. Used by sbdsqr and sstegr. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLASQ6 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlasq6.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlasq6.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlasq6.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLASQ6( I0, N0, Z, PP, DMIN, DMIN1, DMIN2, DN, 
//*                          DNM1, DNM2 ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            I0, N0, PP 
//*       DOUBLE PRECISION   DMIN, DMIN1, DMIN2, DN, DNM1, DNM2 
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
//*> DLASQ6 computes one dqd (shift equal to zero) transform in 
//*> ping-pong form, with protection against underflow and overflow. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] I0 
//*> \verbatim 
//*>          I0 is INTEGER 
//*>        First index. 
//*> \endverbatim 
//*> 
//*> \param[in] N0 
//*> \verbatim 
//*>          N0 is INTEGER 
//*>        Last index. 
//*> \endverbatim 
//*> 
//*> \param[in] Z 
//*> \verbatim 
//*>          Z is DOUBLE PRECISION array, dimension ( 4*N ) 
//*>        Z holds the qd array. EMIN is stored in Z(4*N0) to avoid 
//*>        an extra argument. 
//*> \endverbatim 
//*> 
//*> \param[in] PP 
//*> \verbatim 
//*>          PP is INTEGER 
//*>        PP=0 for ping, PP=1 for pong. 
//*> \endverbatim 
//*> 
//*> \param[out] DMIN 
//*> \verbatim 
//*>          DMIN is DOUBLE PRECISION 
//*>        Minimum value of d. 
//*> \endverbatim 
//*> 
//*> \param[out] DMIN1 
//*> \verbatim 
//*>          DMIN1 is DOUBLE PRECISION 
//*>        Minimum value of d, excluding D( N0 ). 
//*> \endverbatim 
//*> 
//*> \param[out] DMIN2 
//*> \verbatim 
//*>          DMIN2 is DOUBLE PRECISION 
//*>        Minimum value of d, excluding D( N0 ) and D( N0-1 ). 
//*> \endverbatim 
//*> 
//*> \param[out] DN 
//*> \verbatim 
//*>          DN is DOUBLE PRECISION 
//*>        d(N0), the last value of d. 
//*> \endverbatim 
//*> 
//*> \param[out] DNM1 
//*> \verbatim 
//*>          DNM1 is DOUBLE PRECISION 
//*>        d(N0-1). 
//*> \endverbatim 
//*> 
//*> \param[out] DNM2 
//*> \verbatim 
//*>          DNM2 is DOUBLE PRECISION 
//*>        d(N0-2). 
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
//*  ===================================================================== 

	 
	public static void _8oz847ut(ref Int32 _kgliup4t, ref Int32 _psb09l5j, Double* _7e60fcso, ref Int32 _rk50assb, ref Double _tt3ji15i, ref Double _y61kuds7, ref Double _aaaeq9ec, ref Double _b10nc13b, ref Double _ozgqyi38, ref Double _9vhid3u5)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Int32 _h5f9ahvx =  default;
Int32 _bwb7am8r =  default;
Double _plfm7z8g =  default;
Double _48oov32t =  default;
Double _h75qnr7l =  default;
Double _1ajfmh55 =  default;
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
		//*     .. Parameter .. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. External Function .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		if (((_psb09l5j - _kgliup4t) - (int)1) <= (int)0)
		return;//* 
		
		_h75qnr7l = _f43eg0w0("Safe minimum" );
		_h5f9ahvx = ((((int)4 * _kgliup4t) + _rk50assb) - (int)3);
		_48oov32t = *(_7e60fcso+(_h5f9ahvx + (int)4 - 1));
		_plfm7z8g = *(_7e60fcso+(_h5f9ahvx - 1));
		_tt3ji15i = _plfm7z8g;//* 
		
		if (_rk50assb == (int)0)
		{
			
			{
				System.Int32 __81fgg2dlsvn324 = (System.Int32)(((int)4 * _kgliup4t));
				System.Int32 __81fgg2step324 = (System.Int32)((int)4);
				System.Int32 __81fgg2count324;
				for (__81fgg2count324 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)4 * (_psb09l5j - (int)3)) - __81fgg2dlsvn324 + __81fgg2step324) / __81fgg2step324)), _h5f9ahvx = __81fgg2dlsvn324; __81fgg2count324 != 0; __81fgg2count324--, _h5f9ahvx += (__81fgg2step324)) {

				{
					
					*(_7e60fcso+(_h5f9ahvx - (int)2 - 1)) = (_plfm7z8g + *(_7e60fcso+(_h5f9ahvx - (int)1 - 1)));
					if (*(_7e60fcso+(_h5f9ahvx - (int)2 - 1)) == _d0547bi2)
					{
						
						*(_7e60fcso+(_h5f9ahvx - 1)) = _d0547bi2;
						_plfm7z8g = *(_7e60fcso+(_h5f9ahvx + (int)1 - 1));
						_tt3ji15i = _plfm7z8g;
						_48oov32t = _d0547bi2;
					}
					else
					if (((_h75qnr7l * *(_7e60fcso+(_h5f9ahvx + (int)1 - 1))) < *(_7e60fcso+(_h5f9ahvx - (int)2 - 1))) & ((_h75qnr7l * *(_7e60fcso+(_h5f9ahvx - (int)2 - 1))) < *(_7e60fcso+(_h5f9ahvx + (int)1 - 1))))
					{
						
						_1ajfmh55 = (*(_7e60fcso+(_h5f9ahvx + (int)1 - 1)) / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1)));
						*(_7e60fcso+(_h5f9ahvx - 1)) = (*(_7e60fcso+(_h5f9ahvx - (int)1 - 1)) * _1ajfmh55);
						_plfm7z8g = (_plfm7z8g * _1ajfmh55);
					}
					else
					{
						
						*(_7e60fcso+(_h5f9ahvx - 1)) = (*(_7e60fcso+(_h5f9ahvx + (int)1 - 1)) * (*(_7e60fcso+(_h5f9ahvx - (int)1 - 1)) / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1))));
						_plfm7z8g = (*(_7e60fcso+(_h5f9ahvx + (int)1 - 1)) * (_plfm7z8g / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1))));
					}
					
					_tt3ji15i = ILNumerics.F2NET.Intrinsics.MIN(_tt3ji15i ,_plfm7z8g );
					_48oov32t = ILNumerics.F2NET.Intrinsics.MIN(_48oov32t ,*(_7e60fcso+(_h5f9ahvx - 1)) );
Mark10:;
					// continue
				}
								}			}
		}
		else
		{
			
			{
				System.Int32 __81fgg2dlsvn325 = (System.Int32)(((int)4 * _kgliup4t));
				System.Int32 __81fgg2step325 = (System.Int32)((int)4);
				System.Int32 __81fgg2count325;
				for (__81fgg2count325 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)4 * (_psb09l5j - (int)3)) - __81fgg2dlsvn325 + __81fgg2step325) / __81fgg2step325)), _h5f9ahvx = __81fgg2dlsvn325; __81fgg2count325 != 0; __81fgg2count325--, _h5f9ahvx += (__81fgg2step325)) {

				{
					
					*(_7e60fcso+(_h5f9ahvx - (int)3 - 1)) = (_plfm7z8g + *(_7e60fcso+(_h5f9ahvx - 1)));
					if (*(_7e60fcso+(_h5f9ahvx - (int)3 - 1)) == _d0547bi2)
					{
						
						*(_7e60fcso+(_h5f9ahvx - (int)1 - 1)) = _d0547bi2;
						_plfm7z8g = *(_7e60fcso+(_h5f9ahvx + (int)2 - 1));
						_tt3ji15i = _plfm7z8g;
						_48oov32t = _d0547bi2;
					}
					else
					if (((_h75qnr7l * *(_7e60fcso+(_h5f9ahvx + (int)2 - 1))) < *(_7e60fcso+(_h5f9ahvx - (int)3 - 1))) & ((_h75qnr7l * *(_7e60fcso+(_h5f9ahvx - (int)3 - 1))) < *(_7e60fcso+(_h5f9ahvx + (int)2 - 1))))
					{
						
						_1ajfmh55 = (*(_7e60fcso+(_h5f9ahvx + (int)2 - 1)) / *(_7e60fcso+(_h5f9ahvx - (int)3 - 1)));
						*(_7e60fcso+(_h5f9ahvx - (int)1 - 1)) = (*(_7e60fcso+(_h5f9ahvx - 1)) * _1ajfmh55);
						_plfm7z8g = (_plfm7z8g * _1ajfmh55);
					}
					else
					{
						
						*(_7e60fcso+(_h5f9ahvx - (int)1 - 1)) = (*(_7e60fcso+(_h5f9ahvx + (int)2 - 1)) * (*(_7e60fcso+(_h5f9ahvx - 1)) / *(_7e60fcso+(_h5f9ahvx - (int)3 - 1))));
						_plfm7z8g = (*(_7e60fcso+(_h5f9ahvx + (int)2 - 1)) * (_plfm7z8g / *(_7e60fcso+(_h5f9ahvx - (int)3 - 1))));
					}
					
					_tt3ji15i = ILNumerics.F2NET.Intrinsics.MIN(_tt3ji15i ,_plfm7z8g );
					_48oov32t = ILNumerics.F2NET.Intrinsics.MIN(_48oov32t ,*(_7e60fcso+(_h5f9ahvx - (int)1 - 1)) );
Mark20:;
					// continue
				}
								}			}
		}
		//* 
		//*     Unroll last two steps. 
		//* 
		
		_9vhid3u5 = _plfm7z8g;
		_aaaeq9ec = _tt3ji15i;
		_h5f9ahvx = (((int)4 * (_psb09l5j - (int)2)) - _rk50assb);
		_bwb7am8r = ((_h5f9ahvx + ((int)2 * _rk50assb)) - (int)1);
		*(_7e60fcso+(_h5f9ahvx - (int)2 - 1)) = (_9vhid3u5 + *(_7e60fcso+(_bwb7am8r - 1)));
		if (*(_7e60fcso+(_h5f9ahvx - (int)2 - 1)) == _d0547bi2)
		{
			
			*(_7e60fcso+(_h5f9ahvx - 1)) = _d0547bi2;
			_ozgqyi38 = *(_7e60fcso+(_bwb7am8r + (int)2 - 1));
			_tt3ji15i = _ozgqyi38;
			_48oov32t = _d0547bi2;
		}
		else
		if (((_h75qnr7l * *(_7e60fcso+(_bwb7am8r + (int)2 - 1))) < *(_7e60fcso+(_h5f9ahvx - (int)2 - 1))) & ((_h75qnr7l * *(_7e60fcso+(_h5f9ahvx - (int)2 - 1))) < *(_7e60fcso+(_bwb7am8r + (int)2 - 1))))
		{
			
			_1ajfmh55 = (*(_7e60fcso+(_bwb7am8r + (int)2 - 1)) / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1)));
			*(_7e60fcso+(_h5f9ahvx - 1)) = (*(_7e60fcso+(_bwb7am8r - 1)) * _1ajfmh55);
			_ozgqyi38 = (_9vhid3u5 * _1ajfmh55);
		}
		else
		{
			
			*(_7e60fcso+(_h5f9ahvx - 1)) = (*(_7e60fcso+(_bwb7am8r + (int)2 - 1)) * (*(_7e60fcso+(_bwb7am8r - 1)) / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1))));
			_ozgqyi38 = (*(_7e60fcso+(_bwb7am8r + (int)2 - 1)) * (_9vhid3u5 / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1))));
		}
		
		_tt3ji15i = ILNumerics.F2NET.Intrinsics.MIN(_tt3ji15i ,_ozgqyi38 );//* 
		
		_y61kuds7 = _tt3ji15i;
		_h5f9ahvx = (_h5f9ahvx + (int)4);
		_bwb7am8r = ((_h5f9ahvx + ((int)2 * _rk50assb)) - (int)1);
		*(_7e60fcso+(_h5f9ahvx - (int)2 - 1)) = (_ozgqyi38 + *(_7e60fcso+(_bwb7am8r - 1)));
		if (*(_7e60fcso+(_h5f9ahvx - (int)2 - 1)) == _d0547bi2)
		{
			
			*(_7e60fcso+(_h5f9ahvx - 1)) = _d0547bi2;
			_b10nc13b = *(_7e60fcso+(_bwb7am8r + (int)2 - 1));
			_tt3ji15i = _b10nc13b;
			_48oov32t = _d0547bi2;
		}
		else
		if (((_h75qnr7l * *(_7e60fcso+(_bwb7am8r + (int)2 - 1))) < *(_7e60fcso+(_h5f9ahvx - (int)2 - 1))) & ((_h75qnr7l * *(_7e60fcso+(_h5f9ahvx - (int)2 - 1))) < *(_7e60fcso+(_bwb7am8r + (int)2 - 1))))
		{
			
			_1ajfmh55 = (*(_7e60fcso+(_bwb7am8r + (int)2 - 1)) / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1)));
			*(_7e60fcso+(_h5f9ahvx - 1)) = (*(_7e60fcso+(_bwb7am8r - 1)) * _1ajfmh55);
			_b10nc13b = (_ozgqyi38 * _1ajfmh55);
		}
		else
		{
			
			*(_7e60fcso+(_h5f9ahvx - 1)) = (*(_7e60fcso+(_bwb7am8r + (int)2 - 1)) * (*(_7e60fcso+(_bwb7am8r - 1)) / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1))));
			_b10nc13b = (*(_7e60fcso+(_bwb7am8r + (int)2 - 1)) * (_ozgqyi38 / *(_7e60fcso+(_h5f9ahvx - (int)2 - 1))));
		}
		
		_tt3ji15i = ILNumerics.F2NET.Intrinsics.MIN(_tt3ji15i ,_b10nc13b );//* 
		
		*(_7e60fcso+(_h5f9ahvx + (int)2 - 1)) = _b10nc13b;
		*(_7e60fcso+(((int)4 * _psb09l5j) - _rk50assb - 1)) = _48oov32t;
		return;//* 
		//*     End of DLASQ6 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
