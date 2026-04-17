
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
//*> \brief \b SLASY2 solves the Sylvester matrix equation where the matrices are of order 1 or 2. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLASY2 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slasy2.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slasy2.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slasy2.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLASY2( LTRANL, LTRANR, ISGN, N1, N2, TL, LDTL, TR, 
//*                          LDTR, B, LDB, SCALE, X, LDX, XNORM, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       LOGICAL            LTRANL, LTRANR 
//*       INTEGER            INFO, ISGN, LDB, LDTL, LDTR, LDX, N1, N2 
//*       REAL               SCALE, XNORM 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               B( LDB, * ), TL( LDTL, * ), TR( LDTR, * ), 
//*      $                   X( LDX, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLASY2 solves for the N1 by N2 matrix X, 1 <= N1,N2 <= 2, in 
//*> 
//*>        op(TL)*X + ISGN*X*op(TR) = SCALE*B, 
//*> 
//*> where TL is N1 by N1, TR is N2 by N2, B is N1 by N2, and ISGN = 1 or 
//*> -1.  op(T) = T or T**T, where T**T denotes the transpose of T. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] LTRANL 
//*> \verbatim 
//*>          LTRANL is LOGICAL 
//*>          On entry, LTRANL specifies the op(TL): 
//*>             = .FALSE., op(TL) = TL, 
//*>             = .TRUE., op(TL) = TL**T. 
//*> \endverbatim 
//*> 
//*> \param[in] LTRANR 
//*> \verbatim 
//*>          LTRANR is LOGICAL 
//*>          On entry, LTRANR specifies the op(TR): 
//*>            = .FALSE., op(TR) = TR, 
//*>            = .TRUE., op(TR) = TR**T. 
//*> \endverbatim 
//*> 
//*> \param[in] ISGN 
//*> \verbatim 
//*>          ISGN is INTEGER 
//*>          On entry, ISGN specifies the sign of the equation 
//*>          as described before. ISGN may only be 1 or -1. 
//*> \endverbatim 
//*> 
//*> \param[in] N1 
//*> \verbatim 
//*>          N1 is INTEGER 
//*>          On entry, N1 specifies the order of matrix TL. 
//*>          N1 may only be 0, 1 or 2. 
//*> \endverbatim 
//*> 
//*> \param[in] N2 
//*> \verbatim 
//*>          N2 is INTEGER 
//*>          On entry, N2 specifies the order of matrix TR. 
//*>          N2 may only be 0, 1 or 2. 
//*> \endverbatim 
//*> 
//*> \param[in] TL 
//*> \verbatim 
//*>          TL is REAL array, dimension (LDTL,2) 
//*>          On entry, TL contains an N1 by N1 matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] LDTL 
//*> \verbatim 
//*>          LDTL is INTEGER 
//*>          The leading dimension of the matrix TL. LDTL >= max(1,N1). 
//*> \endverbatim 
//*> 
//*> \param[in] TR 
//*> \verbatim 
//*>          TR is REAL array, dimension (LDTR,2) 
//*>          On entry, TR contains an N2 by N2 matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] LDTR 
//*> \verbatim 
//*>          LDTR is INTEGER 
//*>          The leading dimension of the matrix TR. LDTR >= max(1,N2). 
//*> \endverbatim 
//*> 
//*> \param[in] B 
//*> \verbatim 
//*>          B is REAL array, dimension (LDB,2) 
//*>          On entry, the N1 by N2 matrix B contains the right-hand 
//*>          side of the equation. 
//*> \endverbatim 
//*> 
//*> \param[in] LDB 
//*> \verbatim 
//*>          LDB is INTEGER 
//*>          The leading dimension of the matrix B. LDB >= max(1,N1). 
//*> \endverbatim 
//*> 
//*> \param[out] SCALE 
//*> \verbatim 
//*>          SCALE is REAL 
//*>          On exit, SCALE contains the scale factor. SCALE is chosen 
//*>          less than or equal to 1 to prevent the solution overflowing. 
//*> \endverbatim 
//*> 
//*> \param[out] X 
//*> \verbatim 
//*>          X is REAL array, dimension (LDX,2) 
//*>          On exit, X contains the N1 by N2 solution. 
//*> \endverbatim 
//*> 
//*> \param[in] LDX 
//*> \verbatim 
//*>          LDX is INTEGER 
//*>          The leading dimension of the matrix X. LDX >= max(1,N1). 
//*> \endverbatim 
//*> 
//*> \param[out] XNORM 
//*> \verbatim 
//*>          XNORM is REAL 
//*>          On exit, XNORM is the infinity-norm of the solution. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          On exit, INFO is set to 
//*>             0: successful exit. 
//*>             1: TL and TR have too close eigenvalues, so TL or 
//*>                TR is perturbed to get a nonsingular equation. 
//*>          NOTE: In the interests of speed, this routine does not 
//*>                check the inputs for errors. 
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
//*> \ingroup realSYauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _w3c2clvo(ref Boolean _h54ett6r, ref Boolean _vlyddzwx, ref Int32 _6aqem61u, ref Int32 _4o1bt8b1, ref Int32 _tixk7d1h, Single* _qcwsqci1, ref Int32 _kfrnohvo, Single* _fy8po3y0, ref Int32 _nbcrv69s, Single* _p9n405a5, ref Int32 _ly9opahg, ref Single _1m44vtuk, Single* _ta7zuy9k, ref Int32 _eeyyzhrs, ref Single _ziu6urj2, ref Int32 _gro5yvfo)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)120 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
Single _5m0mjfxm =  2f;
Single _gbf4169i =  0.5f;
Single _2j4711hv =  8f;
Boolean _2mdrghql =  default;
Boolean _dkxq7oat =  default;
Int32 _b5p6od9s =  default;
Int32 _8t9w2q8d =  default;
Int32 _w1ilvusp =  default;
Int32 _c727hijy =  default;
Int32 _znpjgsef =  default;
Int32 _c2zk3fjj =  default;
Int32 _pb1k3k8u =  default;
Int32 _umlkckdg =  default;
Single _cdze91vc =  default;
Single _p1iqarg6 =  default;
Single _7hdwv3au =  default;
Single _zgkeuer8 =  default;
Single _atoydcku =  default;
Single _rhnpgpoi =  default;
Single _bogm0gwy =  default;
Single _tvdu6mfs =  default;
Single _1ajfmh55 =  default;
Single _cpceqfr3 =  default;
Single _ldn3y6tl =  default;
Single _l0ib7eel =  default;
Single _u2jk5kqa =  default;
Int32* _9dmn4jw8 =  (Int32*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Int32) * ((int)4);
Single* _tek28dgt =  (Single*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Single) * ((int)4);
Single* _sdi888xs =  (Single*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Single) * ((int)4)*((int)4);
Single* _2qcyvkhx =  (Single*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Single) * ((int)4);
Single* _s13myzj9 =  (Single*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Single) * ((int)2);
string fLanavab = default;
#endregion  variable declarations

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
		//* ===================================================================== 
		//* 
		//*     .. Parameters .. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Local Arrays .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Data statements .. 
		
		{var vals = new Int32[] { (int)3,(int)4,(int)1,(int)2 };var valsIter = 0;

		{ for (int _i72810g = 0; _i72810g < 4;) { __slasy2._xnat4av4[_i72810g++] = vals[valsIter++]; }  };
		}
		{var vals = new Int32[] { (int)2,(int)1,(int)4,(int)3 };var valsIter = 0;

		{ for (int _i72810g = 0; _i72810g < 4;) { __slasy2._l1tx6g7a[_i72810g++] = vals[valsIter++]; }  };
		}
		{var vals = new Int32[] { (int)4,(int)3,(int)2,(int)1 };var valsIter = 0;

		{ for (int _i72810g = 0; _i72810g < 4;) { __slasy2._eymqmxu6[_i72810g++] = vals[valsIter++]; }  };
		}
		{var vals = new Boolean[] { false,false,true,true };var valsIter = 0;

		{ for (int _i72810g = 0; _i72810g < 4;) { __slasy2._zq2q0yje[_i72810g++] = vals[valsIter++]; }  };
		}
		{var vals = new Boolean[] { false,true,false,true };var valsIter = 0;

		{ for (int _i72810g = 0; _i72810g < 4;) { __slasy2._3fbtzrwt[_i72810g++] = vals[valsIter++]; }  };
		}//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Do not check the input parameters for errors 
		//* 
		
		_gro5yvfo = (int)0;//* 
		//*     Quick return if possible 
		//* 
		
		if ((_4o1bt8b1 == (int)0) | (_tixk7d1h == (int)0))
		return;//* 
		//*     Set constants to control overflow 
		//* 
		
		_p1iqarg6 = _d5tu038y("P" );
		_bogm0gwy = (_d5tu038y("S" ) / _p1iqarg6);
		_atoydcku = REAL(_6aqem61u);//* 
		
		_umlkckdg = (((_4o1bt8b1 + _4o1bt8b1) + _tixk7d1h) - (int)2);
		switch (_umlkckdg) {
						case 1:
			goto Mark10;
			case 2:
			goto Mark20;
			case 3:
			goto Mark30;
			case 4:
			goto Mark50;
			default:
			break;
		}
//* 
		//*     1 by 1: TL11*X + SGN*X*TR11 = B11 
		//* 
		
Mark10:;
		// continue
		_tvdu6mfs = (*(_qcwsqci1+((int)1 - 1) + ((int)1 - 1) * 1 * (_kfrnohvo)) + (_atoydcku * *(_fy8po3y0+((int)1 - 1) + ((int)1 - 1) * 1 * (_nbcrv69s))));
		_cdze91vc = ILNumerics.F2NET.Intrinsics.ABS(_tvdu6mfs );
		if (_cdze91vc <= _bogm0gwy)
		{
			
			_tvdu6mfs = _bogm0gwy;
			_cdze91vc = _bogm0gwy;
			_gro5yvfo = (int)1;
		}
		//* 
		
		_1m44vtuk = _kxg5drh2;
		_7hdwv3au = ILNumerics.F2NET.Intrinsics.ABS(*(_p9n405a5+((int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)) );
		if ((_bogm0gwy * _7hdwv3au) > _cdze91vc)
		_1m44vtuk = (_kxg5drh2 / _7hdwv3au);//* 
		
		*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) = ((*(_p9n405a5+((int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)) * _1m44vtuk) / _tvdu6mfs);
		_ziu6urj2 = ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) );
		return;//* 
		//*     1 by 2: 
		//*     TL11*[X11 X12] + ISGN*[X11 X12]*op[TR11 TR12]  = [B11 B12] 
		//*                                       [TR21 TR22] 
		//* 
		
Mark20:;
		// continue//* 
		
		_rhnpgpoi = ILNumerics.F2NET.Intrinsics.MAX(_p1iqarg6 * ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_qcwsqci1+((int)1 - 1) + ((int)1 - 1) * 1 * (_kfrnohvo)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_fy8po3y0+((int)1 - 1) + ((int)1 - 1) * 1 * (_nbcrv69s)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_fy8po3y0+((int)1 - 1) + ((int)2 - 1) * 1 * (_nbcrv69s)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_fy8po3y0+((int)2 - 1) + ((int)1 - 1) * 1 * (_nbcrv69s)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_fy8po3y0+((int)2 - 1) + ((int)2 - 1) * 1 * (_nbcrv69s)) ) ) ,_bogm0gwy );
		*(_2qcyvkhx+((int)1 - 1)) = (*(_qcwsqci1+((int)1 - 1) + ((int)1 - 1) * 1 * (_kfrnohvo)) + (_atoydcku * *(_fy8po3y0+((int)1 - 1) + ((int)1 - 1) * 1 * (_nbcrv69s))));
		*(_2qcyvkhx+((int)4 - 1)) = (*(_qcwsqci1+((int)1 - 1) + ((int)1 - 1) * 1 * (_kfrnohvo)) + (_atoydcku * *(_fy8po3y0+((int)2 - 1) + ((int)2 - 1) * 1 * (_nbcrv69s))));
		if (_vlyddzwx)
		{
			
			*(_2qcyvkhx+((int)2 - 1)) = (_atoydcku * *(_fy8po3y0+((int)2 - 1) + ((int)1 - 1) * 1 * (_nbcrv69s)));
			*(_2qcyvkhx+((int)3 - 1)) = (_atoydcku * *(_fy8po3y0+((int)1 - 1) + ((int)2 - 1) * 1 * (_nbcrv69s)));
		}
		else
		{
			
			*(_2qcyvkhx+((int)2 - 1)) = (_atoydcku * *(_fy8po3y0+((int)1 - 1) + ((int)2 - 1) * 1 * (_nbcrv69s)));
			*(_2qcyvkhx+((int)3 - 1)) = (_atoydcku * *(_fy8po3y0+((int)2 - 1) + ((int)1 - 1) * 1 * (_nbcrv69s)));
		}
		
		*(_tek28dgt+((int)1 - 1)) = *(_p9n405a5+((int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg));
		*(_tek28dgt+((int)2 - 1)) = *(_p9n405a5+((int)1 - 1) + ((int)2 - 1) * 1 * (_ly9opahg));goto Mark40;//* 
		//*     2 by 1: 
		//*          op[TL11 TL12]*[X11] + ISGN* [X11]*TR11  = [B11] 
		//*            [TL21 TL22] [X21]         [X21]         [B21] 
		//* 
		
Mark30:;
		// continue
		_rhnpgpoi = ILNumerics.F2NET.Intrinsics.MAX(_p1iqarg6 * ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_fy8po3y0+((int)1 - 1) + ((int)1 - 1) * 1 * (_nbcrv69s)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_qcwsqci1+((int)1 - 1) + ((int)1 - 1) * 1 * (_kfrnohvo)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_qcwsqci1+((int)1 - 1) + ((int)2 - 1) * 1 * (_kfrnohvo)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_qcwsqci1+((int)2 - 1) + ((int)1 - 1) * 1 * (_kfrnohvo)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_qcwsqci1+((int)2 - 1) + ((int)2 - 1) * 1 * (_kfrnohvo)) ) ) ,_bogm0gwy );
		*(_2qcyvkhx+((int)1 - 1)) = (*(_qcwsqci1+((int)1 - 1) + ((int)1 - 1) * 1 * (_kfrnohvo)) + (_atoydcku * *(_fy8po3y0+((int)1 - 1) + ((int)1 - 1) * 1 * (_nbcrv69s))));
		*(_2qcyvkhx+((int)4 - 1)) = (*(_qcwsqci1+((int)2 - 1) + ((int)2 - 1) * 1 * (_kfrnohvo)) + (_atoydcku * *(_fy8po3y0+((int)1 - 1) + ((int)1 - 1) * 1 * (_nbcrv69s))));
		if (_h54ett6r)
		{
			
			*(_2qcyvkhx+((int)2 - 1)) = *(_qcwsqci1+((int)1 - 1) + ((int)2 - 1) * 1 * (_kfrnohvo));
			*(_2qcyvkhx+((int)3 - 1)) = *(_qcwsqci1+((int)2 - 1) + ((int)1 - 1) * 1 * (_kfrnohvo));
		}
		else
		{
			
			*(_2qcyvkhx+((int)2 - 1)) = *(_qcwsqci1+((int)2 - 1) + ((int)1 - 1) * 1 * (_kfrnohvo));
			*(_2qcyvkhx+((int)3 - 1)) = *(_qcwsqci1+((int)1 - 1) + ((int)2 - 1) * 1 * (_kfrnohvo));
		}
		
		*(_tek28dgt+((int)1 - 1)) = *(_p9n405a5+((int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg));
		*(_tek28dgt+((int)2 - 1)) = *(_p9n405a5+((int)2 - 1) + ((int)1 - 1) * 1 * (_ly9opahg));
Mark40:;
		// continue//* 
		//*     Solve 2 by 2 system using complete pivoting. 
		//*     Set pivots less than SMIN to SMIN. 
		//* 
		
		_w1ilvusp = _z5b2nqbf(ref Unsafe.AsRef((int)4) ,_2qcyvkhx ,ref Unsafe.AsRef((int)1) );
		_cpceqfr3 = *(_2qcyvkhx+(_w1ilvusp - 1));
		if (ILNumerics.F2NET.Intrinsics.ABS(_cpceqfr3 ) <= _rhnpgpoi)
		{
			
			_gro5yvfo = (int)1;
			_cpceqfr3 = _rhnpgpoi;
		}
		
		_ldn3y6tl = *(_2qcyvkhx+(*(__slasy2._xnat4av4+(_w1ilvusp - 1)) - 1));
		_zgkeuer8 = (*(_2qcyvkhx+(*(__slasy2._l1tx6g7a+(_w1ilvusp - 1)) - 1)) / _cpceqfr3);
		_l0ib7eel = (*(_2qcyvkhx+(*(__slasy2._eymqmxu6+(_w1ilvusp - 1)) - 1)) - (_ldn3y6tl * _zgkeuer8));
		_dkxq7oat = *(__slasy2._zq2q0yje+(_w1ilvusp - 1));
		_2mdrghql = *(__slasy2._3fbtzrwt+(_w1ilvusp - 1));
		if (ILNumerics.F2NET.Intrinsics.ABS(_l0ib7eel ) <= _rhnpgpoi)
		{
			
			_gro5yvfo = (int)1;
			_l0ib7eel = _rhnpgpoi;
		}
		
		if (_2mdrghql)
		{
			
			_1ajfmh55 = *(_tek28dgt+((int)2 - 1));
			*(_tek28dgt+((int)2 - 1)) = (*(_tek28dgt+((int)1 - 1)) - (_zgkeuer8 * _1ajfmh55));
			*(_tek28dgt+((int)1 - 1)) = _1ajfmh55;
		}
		else
		{
			
			*(_tek28dgt+((int)2 - 1)) = (*(_tek28dgt+((int)2 - 1)) - (_zgkeuer8 * *(_tek28dgt+((int)1 - 1))));
		}
		
		_1m44vtuk = _kxg5drh2;
		if ((((_5m0mjfxm * _bogm0gwy) * ILNumerics.F2NET.Intrinsics.ABS(*(_tek28dgt+((int)2 - 1)) )) > ILNumerics.F2NET.Intrinsics.ABS(_l0ib7eel )) | (((_5m0mjfxm * _bogm0gwy) * ILNumerics.F2NET.Intrinsics.ABS(*(_tek28dgt+((int)1 - 1)) )) > ILNumerics.F2NET.Intrinsics.ABS(_cpceqfr3 )))
		{
			
			_1m44vtuk = (_gbf4169i / ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_tek28dgt+((int)1 - 1)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_tek28dgt+((int)2 - 1)) ) ));
			*(_tek28dgt+((int)1 - 1)) = (*(_tek28dgt+((int)1 - 1)) * _1m44vtuk);
			*(_tek28dgt+((int)2 - 1)) = (*(_tek28dgt+((int)2 - 1)) * _1m44vtuk);
		}
		
		*(_s13myzj9+((int)2 - 1)) = (*(_tek28dgt+((int)2 - 1)) / _l0ib7eel);
		*(_s13myzj9+((int)1 - 1)) = ((*(_tek28dgt+((int)1 - 1)) / _cpceqfr3) - ((_ldn3y6tl / _cpceqfr3) * *(_s13myzj9+((int)2 - 1))));
		if (_dkxq7oat)
		{
			
			_1ajfmh55 = *(_s13myzj9+((int)2 - 1));
			*(_s13myzj9+((int)2 - 1)) = *(_s13myzj9+((int)1 - 1));
			*(_s13myzj9+((int)1 - 1)) = _1ajfmh55;
		}
		
		*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) = *(_s13myzj9+((int)1 - 1));
		if (_4o1bt8b1 == (int)1)
		{
			
			*(_ta7zuy9k+((int)1 - 1) + ((int)2 - 1) * 1 * (_eeyyzhrs)) = *(_s13myzj9+((int)2 - 1));
			_ziu6urj2 = (ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+((int)1 - 1) + ((int)2 - 1) * 1 * (_eeyyzhrs)) ));
		}
		else
		{
			
			*(_ta7zuy9k+((int)2 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) = *(_s13myzj9+((int)2 - 1));
			_ziu6urj2 = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+((int)2 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) ) );
		}
		
		return;//* 
		//*     2 by 2: 
		//*     op[TL11 TL12]*[X11 X12] +ISGN* [X11 X12]*op[TR11 TR12] = [B11 B12] 
		//*       [TL21 TL22] [X21 X22]        [X21 X22]   [TR21 TR22]   [B21 B22] 
		//* 
		//*     Solve equivalent 4 by 4 system using complete pivoting. 
		//*     Set pivots less than SMIN to SMIN. 
		//* 
		
Mark50:;
		// continue
		_rhnpgpoi = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_fy8po3y0+((int)1 - 1) + ((int)1 - 1) * 1 * (_nbcrv69s)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_fy8po3y0+((int)1 - 1) + ((int)2 - 1) * 1 * (_nbcrv69s)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_fy8po3y0+((int)2 - 1) + ((int)1 - 1) * 1 * (_nbcrv69s)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_fy8po3y0+((int)2 - 1) + ((int)2 - 1) * 1 * (_nbcrv69s)) ) );
		_rhnpgpoi = ILNumerics.F2NET.Intrinsics.MAX(_rhnpgpoi ,ILNumerics.F2NET.Intrinsics.ABS(*(_qcwsqci1+((int)1 - 1) + ((int)1 - 1) * 1 * (_kfrnohvo)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_qcwsqci1+((int)1 - 1) + ((int)2 - 1) * 1 * (_kfrnohvo)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_qcwsqci1+((int)2 - 1) + ((int)1 - 1) * 1 * (_kfrnohvo)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_qcwsqci1+((int)2 - 1) + ((int)2 - 1) * 1 * (_kfrnohvo)) ) );
		_rhnpgpoi = ILNumerics.F2NET.Intrinsics.MAX(_p1iqarg6 * _rhnpgpoi ,_bogm0gwy );
		*(_tek28dgt+((int)1 - 1)) = _d0547bi2;
		_wcs7ne88(ref Unsafe.AsRef((int)16) ,_tek28dgt ,ref Unsafe.AsRef((int)0) ,_sdi888xs ,ref Unsafe.AsRef((int)1) );
		*(_sdi888xs+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)4)) = (*(_qcwsqci1+((int)1 - 1) + ((int)1 - 1) * 1 * (_kfrnohvo)) + (_atoydcku * *(_fy8po3y0+((int)1 - 1) + ((int)1 - 1) * 1 * (_nbcrv69s))));
		*(_sdi888xs+((int)2 - 1) + ((int)2 - 1) * 1 * ((int)4)) = (*(_qcwsqci1+((int)2 - 1) + ((int)2 - 1) * 1 * (_kfrnohvo)) + (_atoydcku * *(_fy8po3y0+((int)1 - 1) + ((int)1 - 1) * 1 * (_nbcrv69s))));
		*(_sdi888xs+((int)3 - 1) + ((int)3 - 1) * 1 * ((int)4)) = (*(_qcwsqci1+((int)1 - 1) + ((int)1 - 1) * 1 * (_kfrnohvo)) + (_atoydcku * *(_fy8po3y0+((int)2 - 1) + ((int)2 - 1) * 1 * (_nbcrv69s))));
		*(_sdi888xs+((int)4 - 1) + ((int)4 - 1) * 1 * ((int)4)) = (*(_qcwsqci1+((int)2 - 1) + ((int)2 - 1) * 1 * (_kfrnohvo)) + (_atoydcku * *(_fy8po3y0+((int)2 - 1) + ((int)2 - 1) * 1 * (_nbcrv69s))));
		if (_h54ett6r)
		{
			
			*(_sdi888xs+((int)1 - 1) + ((int)2 - 1) * 1 * ((int)4)) = *(_qcwsqci1+((int)2 - 1) + ((int)1 - 1) * 1 * (_kfrnohvo));
			*(_sdi888xs+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)4)) = *(_qcwsqci1+((int)1 - 1) + ((int)2 - 1) * 1 * (_kfrnohvo));
			*(_sdi888xs+((int)3 - 1) + ((int)4 - 1) * 1 * ((int)4)) = *(_qcwsqci1+((int)2 - 1) + ((int)1 - 1) * 1 * (_kfrnohvo));
			*(_sdi888xs+((int)4 - 1) + ((int)3 - 1) * 1 * ((int)4)) = *(_qcwsqci1+((int)1 - 1) + ((int)2 - 1) * 1 * (_kfrnohvo));
		}
		else
		{
			
			*(_sdi888xs+((int)1 - 1) + ((int)2 - 1) * 1 * ((int)4)) = *(_qcwsqci1+((int)1 - 1) + ((int)2 - 1) * 1 * (_kfrnohvo));
			*(_sdi888xs+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)4)) = *(_qcwsqci1+((int)2 - 1) + ((int)1 - 1) * 1 * (_kfrnohvo));
			*(_sdi888xs+((int)3 - 1) + ((int)4 - 1) * 1 * ((int)4)) = *(_qcwsqci1+((int)1 - 1) + ((int)2 - 1) * 1 * (_kfrnohvo));
			*(_sdi888xs+((int)4 - 1) + ((int)3 - 1) * 1 * ((int)4)) = *(_qcwsqci1+((int)2 - 1) + ((int)1 - 1) * 1 * (_kfrnohvo));
		}
		
		if (_vlyddzwx)
		{
			
			*(_sdi888xs+((int)1 - 1) + ((int)3 - 1) * 1 * ((int)4)) = (_atoydcku * *(_fy8po3y0+((int)1 - 1) + ((int)2 - 1) * 1 * (_nbcrv69s)));
			*(_sdi888xs+((int)2 - 1) + ((int)4 - 1) * 1 * ((int)4)) = (_atoydcku * *(_fy8po3y0+((int)1 - 1) + ((int)2 - 1) * 1 * (_nbcrv69s)));
			*(_sdi888xs+((int)3 - 1) + ((int)1 - 1) * 1 * ((int)4)) = (_atoydcku * *(_fy8po3y0+((int)2 - 1) + ((int)1 - 1) * 1 * (_nbcrv69s)));
			*(_sdi888xs+((int)4 - 1) + ((int)2 - 1) * 1 * ((int)4)) = (_atoydcku * *(_fy8po3y0+((int)2 - 1) + ((int)1 - 1) * 1 * (_nbcrv69s)));
		}
		else
		{
			
			*(_sdi888xs+((int)1 - 1) + ((int)3 - 1) * 1 * ((int)4)) = (_atoydcku * *(_fy8po3y0+((int)2 - 1) + ((int)1 - 1) * 1 * (_nbcrv69s)));
			*(_sdi888xs+((int)2 - 1) + ((int)4 - 1) * 1 * ((int)4)) = (_atoydcku * *(_fy8po3y0+((int)2 - 1) + ((int)1 - 1) * 1 * (_nbcrv69s)));
			*(_sdi888xs+((int)3 - 1) + ((int)1 - 1) * 1 * ((int)4)) = (_atoydcku * *(_fy8po3y0+((int)1 - 1) + ((int)2 - 1) * 1 * (_nbcrv69s)));
			*(_sdi888xs+((int)4 - 1) + ((int)2 - 1) * 1 * ((int)4)) = (_atoydcku * *(_fy8po3y0+((int)1 - 1) + ((int)2 - 1) * 1 * (_nbcrv69s)));
		}
		
		*(_tek28dgt+((int)1 - 1)) = *(_p9n405a5+((int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg));
		*(_tek28dgt+((int)2 - 1)) = *(_p9n405a5+((int)2 - 1) + ((int)1 - 1) * 1 * (_ly9opahg));
		*(_tek28dgt+((int)3 - 1)) = *(_p9n405a5+((int)1 - 1) + ((int)2 - 1) * 1 * (_ly9opahg));
		*(_tek28dgt+((int)4 - 1)) = *(_p9n405a5+((int)2 - 1) + ((int)2 - 1) * 1 * (_ly9opahg));//* 
		//*     Perform elimination 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn2413 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2413 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2413;
			for (__81fgg2count2413 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)3) - __81fgg2dlsvn2413 + __81fgg2step2413) / __81fgg2step2413)), _b5p6od9s = __81fgg2dlsvn2413; __81fgg2count2413 != 0; __81fgg2count2413--, _b5p6od9s += (__81fgg2step2413)) {

			{
				
				_u2jk5kqa = _d0547bi2;
				{
					System.Int32 __81fgg2dlsvn2414 = (System.Int32)(_b5p6od9s);
					const System.Int32 __81fgg2step2414 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2414;
					for (__81fgg2count2414 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)4) - __81fgg2dlsvn2414 + __81fgg2step2414) / __81fgg2step2414)), _8t9w2q8d = __81fgg2dlsvn2414; __81fgg2count2414 != 0; __81fgg2count2414--, _8t9w2q8d += (__81fgg2step2414)) {

					{
						
						{
							System.Int32 __81fgg2dlsvn2415 = (System.Int32)(_b5p6od9s);
							const System.Int32 __81fgg2step2415 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2415;
							for (__81fgg2count2415 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)4) - __81fgg2dlsvn2415 + __81fgg2step2415) / __81fgg2step2415)), _c2zk3fjj = __81fgg2dlsvn2415; __81fgg2count2415 != 0; __81fgg2count2415--, _c2zk3fjj += (__81fgg2step2415)) {

							{
								
								if (ILNumerics.F2NET.Intrinsics.ABS(*(_sdi888xs+(_8t9w2q8d - 1) + (_c2zk3fjj - 1) * 1 * ((int)4)) ) >= _u2jk5kqa)
								{
									
									_u2jk5kqa = ILNumerics.F2NET.Intrinsics.ABS(*(_sdi888xs+(_8t9w2q8d - 1) + (_c2zk3fjj - 1) * 1 * ((int)4)) );
									_c727hijy = _8t9w2q8d;
									_pb1k3k8u = _c2zk3fjj;
								}
								
Mark60:;
								// continue
							}
														}						}
Mark70:;
						// continue
					}
										}				}
				if (_c727hijy != _b5p6od9s)
				{
					
					_ahhuglvd(ref Unsafe.AsRef((int)4) ,(_sdi888xs+(_c727hijy - 1) + ((int)1 - 1) * 1 * ((int)4)),ref Unsafe.AsRef((int)4) ,(_sdi888xs+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * ((int)4)),ref Unsafe.AsRef((int)4) );
					_1ajfmh55 = *(_tek28dgt+(_b5p6od9s - 1));
					*(_tek28dgt+(_b5p6od9s - 1)) = *(_tek28dgt+(_c727hijy - 1));
					*(_tek28dgt+(_c727hijy - 1)) = _1ajfmh55;
				}
				
				if (_pb1k3k8u != _b5p6od9s)
				_ahhuglvd(ref Unsafe.AsRef((int)4) ,(_sdi888xs+((int)1 - 1) + (_pb1k3k8u - 1) * 1 * ((int)4)),ref Unsafe.AsRef((int)1) ,(_sdi888xs+((int)1 - 1) + (_b5p6od9s - 1) * 1 * ((int)4)),ref Unsafe.AsRef((int)1) );
				*(_9dmn4jw8+(_b5p6od9s - 1)) = _pb1k3k8u;
				if (ILNumerics.F2NET.Intrinsics.ABS(*(_sdi888xs+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * ((int)4)) ) < _rhnpgpoi)
				{
					
					_gro5yvfo = (int)1;
					*(_sdi888xs+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * ((int)4)) = _rhnpgpoi;
				}
				
				{
					System.Int32 __81fgg2dlsvn2416 = (System.Int32)((_b5p6od9s + (int)1));
					const System.Int32 __81fgg2step2416 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2416;
					for (__81fgg2count2416 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)4) - __81fgg2dlsvn2416 + __81fgg2step2416) / __81fgg2step2416)), _znpjgsef = __81fgg2dlsvn2416; __81fgg2count2416 != 0; __81fgg2count2416--, _znpjgsef += (__81fgg2step2416)) {

					{
						
						*(_sdi888xs+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * ((int)4)) = (*(_sdi888xs+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * ((int)4)) / *(_sdi888xs+(_b5p6od9s - 1) + (_b5p6od9s - 1) * 1 * ((int)4)));
						*(_tek28dgt+(_znpjgsef - 1)) = (*(_tek28dgt+(_znpjgsef - 1)) - (*(_sdi888xs+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * ((int)4)) * *(_tek28dgt+(_b5p6od9s - 1))));
						{
							System.Int32 __81fgg2dlsvn2417 = (System.Int32)((_b5p6od9s + (int)1));
							const System.Int32 __81fgg2step2417 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2417;
							for (__81fgg2count2417 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)4) - __81fgg2dlsvn2417 + __81fgg2step2417) / __81fgg2step2417)), _umlkckdg = __81fgg2dlsvn2417; __81fgg2count2417 != 0; __81fgg2count2417--, _umlkckdg += (__81fgg2step2417)) {

							{
								
								*(_sdi888xs+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * ((int)4)) = (*(_sdi888xs+(_znpjgsef - 1) + (_umlkckdg - 1) * 1 * ((int)4)) - (*(_sdi888xs+(_znpjgsef - 1) + (_b5p6od9s - 1) * 1 * ((int)4)) * *(_sdi888xs+(_b5p6od9s - 1) + (_umlkckdg - 1) * 1 * ((int)4))));
Mark80:;
								// continue
							}
														}						}
Mark90:;
						// continue
					}
										}				}
Mark100:;
				// continue
			}
						}		}
		if (ILNumerics.F2NET.Intrinsics.ABS(*(_sdi888xs+((int)4 - 1) + ((int)4 - 1) * 1 * ((int)4)) ) < _rhnpgpoi)
		{
			
			_gro5yvfo = (int)1;
			*(_sdi888xs+((int)4 - 1) + ((int)4 - 1) * 1 * ((int)4)) = _rhnpgpoi;
		}
		
		_1m44vtuk = _kxg5drh2;
		if ((((((_2j4711hv * _bogm0gwy) * ILNumerics.F2NET.Intrinsics.ABS(*(_tek28dgt+((int)1 - 1)) )) > ILNumerics.F2NET.Intrinsics.ABS(*(_sdi888xs+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)4)) )) | (((_2j4711hv * _bogm0gwy) * ILNumerics.F2NET.Intrinsics.ABS(*(_tek28dgt+((int)2 - 1)) )) > ILNumerics.F2NET.Intrinsics.ABS(*(_sdi888xs+((int)2 - 1) + ((int)2 - 1) * 1 * ((int)4)) ))) | (((_2j4711hv * _bogm0gwy) * ILNumerics.F2NET.Intrinsics.ABS(*(_tek28dgt+((int)3 - 1)) )) > ILNumerics.F2NET.Intrinsics.ABS(*(_sdi888xs+((int)3 - 1) + ((int)3 - 1) * 1 * ((int)4)) ))) | (((_2j4711hv * _bogm0gwy) * ILNumerics.F2NET.Intrinsics.ABS(*(_tek28dgt+((int)4 - 1)) )) > ILNumerics.F2NET.Intrinsics.ABS(*(_sdi888xs+((int)4 - 1) + ((int)4 - 1) * 1 * ((int)4)) )))
		{
			
			_1m44vtuk = ((_kxg5drh2 / _2j4711hv) / ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_tek28dgt+((int)1 - 1)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_tek28dgt+((int)2 - 1)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_tek28dgt+((int)3 - 1)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_tek28dgt+((int)4 - 1)) ) ));
			*(_tek28dgt+((int)1 - 1)) = (*(_tek28dgt+((int)1 - 1)) * _1m44vtuk);
			*(_tek28dgt+((int)2 - 1)) = (*(_tek28dgt+((int)2 - 1)) * _1m44vtuk);
			*(_tek28dgt+((int)3 - 1)) = (*(_tek28dgt+((int)3 - 1)) * _1m44vtuk);
			*(_tek28dgt+((int)4 - 1)) = (*(_tek28dgt+((int)4 - 1)) * _1m44vtuk);
		}
		
		{
			System.Int32 __81fgg2dlsvn2418 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2418 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2418;
			for (__81fgg2count2418 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)4) - __81fgg2dlsvn2418 + __81fgg2step2418) / __81fgg2step2418)), _b5p6od9s = __81fgg2dlsvn2418; __81fgg2count2418 != 0; __81fgg2count2418--, _b5p6od9s += (__81fgg2step2418)) {

			{
				
				_umlkckdg = ((int)5 - _b5p6od9s);
				_1ajfmh55 = (_kxg5drh2 / *(_sdi888xs+(_umlkckdg - 1) + (_umlkckdg - 1) * 1 * ((int)4)));
				*(_2qcyvkhx+(_umlkckdg - 1)) = (*(_tek28dgt+(_umlkckdg - 1)) * _1ajfmh55);
				{
					System.Int32 __81fgg2dlsvn2419 = (System.Int32)((_umlkckdg + (int)1));
					const System.Int32 __81fgg2step2419 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2419;
					for (__81fgg2count2419 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)4) - __81fgg2dlsvn2419 + __81fgg2step2419) / __81fgg2step2419)), _znpjgsef = __81fgg2dlsvn2419; __81fgg2count2419 != 0; __81fgg2count2419--, _znpjgsef += (__81fgg2step2419)) {

					{
						
						*(_2qcyvkhx+(_umlkckdg - 1)) = (*(_2qcyvkhx+(_umlkckdg - 1)) - ((_1ajfmh55 * *(_sdi888xs+(_umlkckdg - 1) + (_znpjgsef - 1) * 1 * ((int)4))) * *(_2qcyvkhx+(_znpjgsef - 1))));
Mark110:;
						// continue
					}
										}				}
Mark120:;
				// continue
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn2420 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2420 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2420;
			for (__81fgg2count2420 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)3) - __81fgg2dlsvn2420 + __81fgg2step2420) / __81fgg2step2420)), _b5p6od9s = __81fgg2dlsvn2420; __81fgg2count2420 != 0; __81fgg2count2420--, _b5p6od9s += (__81fgg2step2420)) {

			{
				
				if (*(_9dmn4jw8+((int)4 - _b5p6od9s - 1)) != ((int)4 - _b5p6od9s))
				{
					
					_1ajfmh55 = *(_2qcyvkhx+((int)4 - _b5p6od9s - 1));
					*(_2qcyvkhx+((int)4 - _b5p6od9s - 1)) = *(_2qcyvkhx+(*(_9dmn4jw8+((int)4 - _b5p6od9s - 1)) - 1));
					*(_2qcyvkhx+(*(_9dmn4jw8+((int)4 - _b5p6od9s - 1)) - 1)) = _1ajfmh55;
				}
				
Mark130:;
				// continue
			}
						}		}
		*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) = *(_2qcyvkhx+((int)1 - 1));
		*(_ta7zuy9k+((int)2 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) = *(_2qcyvkhx+((int)2 - 1));
		*(_ta7zuy9k+((int)1 - 1) + ((int)2 - 1) * 1 * (_eeyyzhrs)) = *(_2qcyvkhx+((int)3 - 1));
		*(_ta7zuy9k+((int)2 - 1) + ((int)2 - 1) * 1 * (_eeyyzhrs)) = *(_2qcyvkhx+((int)4 - 1));
		_ziu6urj2 = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_2qcyvkhx+((int)1 - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_2qcyvkhx+((int)3 - 1)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_2qcyvkhx+((int)2 - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_2qcyvkhx+((int)4 - 1)) ) );
		return;//* 
		//*     End of SLASY2 
		//* 
		
	}
	
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177


internal unsafe class __slasy2 { 
internal static MemoryHandle _3fbtzrwtH_ =  ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<Boolean>((ulong)((int)4));
internal static Boolean* _3fbtzrwt = (Boolean*)_3fbtzrwtH_.Pointer;
internal static MemoryHandle _zq2q0yjeH_ =  ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<Boolean>((ulong)((int)4));
internal static Boolean* _zq2q0yje = (Boolean*)_zq2q0yjeH_.Pointer;
internal static MemoryHandle _l1tx6g7aH_ =  ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<Int32>((ulong)((int)4));
internal static Int32* _l1tx6g7a = (Int32*)_l1tx6g7aH_.Pointer;
internal static MemoryHandle _xnat4av4H_ =  ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<Int32>((ulong)((int)4));
internal static Int32* _xnat4av4 = (Int32*)_xnat4av4H_.Pointer;
internal static MemoryHandle _eymqmxu6H_ =  ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<Int32>((ulong)((int)4));
internal static Int32* _eymqmxu6 = (Int32*)_eymqmxu6H_.Pointer;

}

} // end class 
} // end namespace
#endif
