
#pragma warning disable CS0164, CS0219, CS0162, CS1718
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
//*> \brief \b IEEECK 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download IEEECK + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/ieeeck.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/ieeeck.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/ieeeck.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       INTEGER          FUNCTION IEEECK( ISPEC, ZERO, ONE ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            ISPEC 
//*       REAL               ONE, ZERO 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> IEEECK is called from the ILAENV to verify that Infinity and 
//*> possibly NaN arithmetic is safe (i.e. will not trap). 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] ISPEC 
//*> \verbatim 
//*>          ISPEC is INTEGER 
//*>          Specifies whether to test just for inifinity arithmetic 
//*>          or whether to test for infinity and NaN arithmetic. 
//*>          = 0: Verify infinity arithmetic only. 
//*>          = 1: Verify infinity and NaN arithmetic. 
//*> \endverbatim 
//*> 
//*> \param[in] ZERO 
//*> \verbatim 
//*>          ZERO is REAL 
//*>          Must contain the value 0.0 
//*>          This is passed to prevent the compiler from optimizing 
//*>          away this code. 
//*> \endverbatim 
//*> 
//*> \param[in] ONE 
//*> \verbatim 
//*>          ONE is REAL 
//*>          Must contain the value 1.0 
//*>          This is passed to prevent the compiler from optimizing 
//*>          away this code. 
//*> 
//*>  RETURN VALUE:  INTEGER 
//*>          = 0:  Arithmetic failed to produce the correct answers 
//*>          = 1:  Arithmetic produced the correct answers 
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

	 
	public static Int32 _df9j6irr(ref Int32 _r22uqjla, ref Single _d0547bi2, ref Single _kxg5drh2)
	{
#region variable declarations
Int32 _df9j6irr = default;
Single _le87faqb =  default;
Single _wjvlzwsr =  default;
Single _sbxpqiyg =  default;
Single _4yb7y0rf =  default;
Single _vmp8vu3c =  default;
Single _z8wxxf25 =  default;
Single _uabgqgsc =  default;
Single _p1cq8sm6 =  default;
Single _1p0mht57 =  default;
Single _iv0rwbsy =  default;
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
		//* 
		//*  ===================================================================== 
		//* 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Executable Statements .. 
		
		_df9j6irr = (int)1;//* 
		
		_iv0rwbsy = (_kxg5drh2 / _d0547bi2);
		if (_iv0rwbsy <= _kxg5drh2)
		{
			
			_df9j6irr = (int)0;
			return _df9j6irr;
		}
		//* 
		
		_uabgqgsc = (-((_kxg5drh2 / _d0547bi2)));
		if (_uabgqgsc >= _d0547bi2)
		{
			
			_df9j6irr = (int)0;
			return _df9j6irr;
		}
		//* 
		
		_p1cq8sm6 = (_kxg5drh2 / (_uabgqgsc + _kxg5drh2));
		if (_p1cq8sm6 != _d0547bi2)
		{
			
			_df9j6irr = (int)0;
			return _df9j6irr;
		}
		//* 
		
		_uabgqgsc = (_kxg5drh2 / _p1cq8sm6);
		if (_uabgqgsc >= _d0547bi2)
		{
			
			_df9j6irr = (int)0;
			return _df9j6irr;
		}
		//* 
		
		_1p0mht57 = (_p1cq8sm6 + _d0547bi2);
		if (_1p0mht57 != _d0547bi2)
		{
			
			_df9j6irr = (int)0;
			return _df9j6irr;
		}
		//* 
		
		_iv0rwbsy = (_kxg5drh2 / _1p0mht57);
		if (_iv0rwbsy <= _kxg5drh2)
		{
			
			_df9j6irr = (int)0;
			return _df9j6irr;
		}
		//* 
		
		_uabgqgsc = (_uabgqgsc * _iv0rwbsy);
		if (_uabgqgsc >= _d0547bi2)
		{
			
			_df9j6irr = (int)0;
			return _df9j6irr;
		}
		//* 
		
		_iv0rwbsy = (_iv0rwbsy * _iv0rwbsy);
		if (_iv0rwbsy <= _kxg5drh2)
		{
			
			_df9j6irr = (int)0;
			return _df9j6irr;
		}
		//* 
		//* 
		//* 
		//* 
		//*     Return if we were only asked to check infinity arithmetic 
		//* 
		
		if (_r22uqjla == (int)0)
		return _df9j6irr;//* 
		
		_le87faqb = (_iv0rwbsy + _uabgqgsc);//* 
		
		_wjvlzwsr = (_iv0rwbsy / _uabgqgsc);//* 
		
		_sbxpqiyg = (_iv0rwbsy / _iv0rwbsy);//* 
		
		_4yb7y0rf = (_iv0rwbsy * _d0547bi2);//* 
		
		_vmp8vu3c = (_uabgqgsc * _p1cq8sm6);//* 
		
		_z8wxxf25 = (_vmp8vu3c * _d0547bi2);//* 
		
		if (_le87faqb == _le87faqb)
		{
			
			_df9j6irr = (int)0;
			return _df9j6irr;
		}
		//* 
		
		if (_wjvlzwsr == _wjvlzwsr)
		{
			
			_df9j6irr = (int)0;
			return _df9j6irr;
		}
		//* 
		
		if (_sbxpqiyg == _sbxpqiyg)
		{
			
			_df9j6irr = (int)0;
			return _df9j6irr;
		}
		//* 
		
		if (_4yb7y0rf == _4yb7y0rf)
		{
			
			_df9j6irr = (int)0;
			return _df9j6irr;
		}
		//* 
		
		if (_vmp8vu3c == _vmp8vu3c)
		{
			
			_df9j6irr = (int)0;
			return _df9j6irr;
		}
		//* 
		
		if (_z8wxxf25 == _z8wxxf25)
		{
			
			_df9j6irr = (int)0;
			return _df9j6irr;
		}
		//* 
		
		return _df9j6irr;
	}
	
	return _df9j6irr;
	} // 177

} // end class 
} // end namespace
#endif
