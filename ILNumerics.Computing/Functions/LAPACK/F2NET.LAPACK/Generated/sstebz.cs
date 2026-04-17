
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
//*> \brief \b SSTEBZ 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SSTEBZ + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/sstebz.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/sstebz.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/sstebz.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SSTEBZ( RANGE, ORDER, N, VL, VU, IL, IU, ABSTOL, D, E, 
//*                          M, NSPLIT, W, IBLOCK, ISPLIT, WORK, IWORK, 
//*                          INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          ORDER, RANGE 
//*       INTEGER            IL, INFO, IU, M, N, NSPLIT 
//*       REAL               ABSTOL, VL, VU 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IBLOCK( * ), ISPLIT( * ), IWORK( * ) 
//*       REAL               D( * ), E( * ), W( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SSTEBZ computes the eigenvalues of a symmetric tridiagonal 
//*> matrix T.  The user may ask for all eigenvalues, all eigenvalues 
//*> in the half-open interval (VL, VU], or the IL-th through IU-th 
//*> eigenvalues. 
//*> 
//*> To avoid overflow, the matrix must be scaled so that its 
//*> largest element is no greater than overflow**(1/2) * underflow**(1/4) in absolute value, and for greatest 
//*> accuracy, it should not be much smaller than that. 
//*> 
//*> See W. Kahan "Accurate Eigenvalues of a Symmetric Tridiagonal 
//*> Matrix", Report CS41, Computer Science Dept., Stanford 
//*> University, July 21, 1966. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] RANGE 
//*> \verbatim 
//*>          RANGE is CHARACTER*1 
//*>          = 'A': ("All")   all eigenvalues will be found. 
//*>          = 'V': ("Value") all eigenvalues in the half-open interval 
//*>                           (VL, VU] will be found. 
//*>          = 'I': ("Index") the IL-th through IU-th eigenvalues (of the 
//*>                           entire matrix) will be found. 
//*> \endverbatim 
//*> 
//*> \param[in] ORDER 
//*> \verbatim 
//*>          ORDER is CHARACTER*1 
//*>          = 'B': ("By Block") the eigenvalues will be grouped by 
//*>                              split-off block (see IBLOCK, ISPLIT) and 
//*>                              ordered from smallest to largest within 
//*>                              the block. 
//*>          = 'E': ("Entire matrix") 
//*>                              the eigenvalues for the entire matrix 
//*>                              will be ordered from smallest to 
//*>                              largest. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the tridiagonal matrix T.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] VL 
//*> \verbatim 
//*>          VL is REAL 
//*> 
//*>          If RANGE='V', the lower bound of the interval to 
//*>          be searched for eigenvalues.  Eigenvalues less than or equal 
//*>          to VL, or greater than VU, will not be returned.  VL < VU. 
//*>          Not referenced if RANGE = 'A' or 'I'. 
//*> \endverbatim 
//*> 
//*> \param[in] VU 
//*> \verbatim 
//*>          VU is REAL 
//*> 
//*>          If RANGE='V', the upper bound of the interval to 
//*>          be searched for eigenvalues.  Eigenvalues less than or equal 
//*>          to VL, or greater than VU, will not be returned.  VL < VU. 
//*>          Not referenced if RANGE = 'A' or 'I'. 
//*> \endverbatim 
//*> 
//*> \param[in] IL 
//*> \verbatim 
//*>          IL is INTEGER 
//*> 
//*>          If RANGE='I', the index of the 
//*>          smallest eigenvalue to be returned. 
//*>          1 <= IL <= IU <= N, if N > 0; IL = 1 and IU = 0 if N = 0. 
//*>          Not referenced if RANGE = 'A' or 'V'. 
//*> \endverbatim 
//*> 
//*> \param[in] IU 
//*> \verbatim 
//*>          IU is INTEGER 
//*> 
//*>          If RANGE='I', the index of the 
//*>          largest eigenvalue to be returned. 
//*>          1 <= IL <= IU <= N, if N > 0; IL = 1 and IU = 0 if N = 0. 
//*>          Not referenced if RANGE = 'A' or 'V'. 
//*> \endverbatim 
//*> 
//*> \param[in] ABSTOL 
//*> \verbatim 
//*>          ABSTOL is REAL 
//*>          The absolute tolerance for the eigenvalues.  An eigenvalue 
//*>          (or cluster) is considered to be located if it has been 
//*>          determined to lie in an interval whose width is ABSTOL or 
//*>          less.  If ABSTOL is less than or equal to zero, then ULP*|T| 
//*>          will be used, where |T| means the 1-norm of T. 
//*> 
//*>          Eigenvalues will be computed most accurately when ABSTOL is 
//*>          set to twice the underflow threshold 2*SLAMCH('S'), not zero. 
//*> \endverbatim 
//*> 
//*> \param[in] D 
//*> \verbatim 
//*>          D is REAL array, dimension (N) 
//*>          The n diagonal elements of the tridiagonal matrix T. 
//*> \endverbatim 
//*> 
//*> \param[in] E 
//*> \verbatim 
//*>          E is REAL array, dimension (N-1) 
//*>          The (n-1) off-diagonal elements of the tridiagonal matrix T. 
//*> \endverbatim 
//*> 
//*> \param[out] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The actual number of eigenvalues found. 0 <= M <= N. 
//*>          (See also the description of INFO=2,3.) 
//*> \endverbatim 
//*> 
//*> \param[out] NSPLIT 
//*> \verbatim 
//*>          NSPLIT is INTEGER 
//*>          The number of diagonal blocks in the matrix T. 
//*>          1 <= NSPLIT <= N. 
//*> \endverbatim 
//*> 
//*> \param[out] W 
//*> \verbatim 
//*>          W is REAL array, dimension (N) 
//*>          On exit, the first M elements of W will contain the 
//*>          eigenvalues.  (SSTEBZ may use the remaining N-M elements as 
//*>          workspace.) 
//*> \endverbatim 
//*> 
//*> \param[out] IBLOCK 
//*> \verbatim 
//*>          IBLOCK is INTEGER array, dimension (N) 
//*>          At each row/column j where E(j) is zero or small, the 
//*>          matrix T is considered to split into a block diagonal 
//*>          matrix.  On exit, if INFO = 0, IBLOCK(i) specifies to which 
//*>          block (from 1 to the number of blocks) the eigenvalue W(i) 
//*>          belongs.  (SSTEBZ may use the remaining N-M elements as 
//*>          workspace.) 
//*> \endverbatim 
//*> 
//*> \param[out] ISPLIT 
//*> \verbatim 
//*>          ISPLIT is INTEGER array, dimension (N) 
//*>          The splitting points, at which T breaks up into submatrices. 
//*>          The first submatrix consists of rows/columns 1 to ISPLIT(1), 
//*>          the second of rows/columns ISPLIT(1)+1 through ISPLIT(2), 
//*>          etc., and the NSPLIT-th consists of rows/columns 
//*>          ISPLIT(NSPLIT-1)+1 through ISPLIT(NSPLIT)=N. 
//*>          (Only the first NSPLIT elements will actually be used, but 
//*>          since the user cannot know a priori what value NSPLIT will 
//*>          have, N words must be reserved for ISPLIT.) 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is REAL array, dimension (4*N) 
//*> \endverbatim 
//*> 
//*> \param[out] IWORK 
//*> \verbatim 
//*>          IWORK is INTEGER array, dimension (3*N) 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value 
//*>          > 0:  some or all of the eigenvalues failed to converge or 
//*>                were not computed: 
//*>                =1 or 3: Bisection failed to converge for some 
//*>                        eigenvalues; these eigenvalues are flagged by a 
//*>                        negative block number.  The effect is that the 
//*>                        eigenvalues may not be as accurate as the 
//*>                        absolute and relative tolerances.  This is 
//*>                        generally caused by unexpectedly inaccurate 
//*>                        arithmetic. 
//*>                =2 or 3: RANGE='I' only: Not all of the eigenvalues 
//*>                        IL:IU were found. 
//*>                        Effect: M < IU+1-IL 
//*>                        Cause:  non-monotonic arithmetic, causing the 
//*>                                Sturm sequence to be non-monotonic. 
//*>                        Cure:   recalculate, using RANGE='A', and pick 
//*>                                out eigenvalues IL:IU.  In some cases, 
//*>                                increasing the PARAMETER "FUDGE" may 
//*>                                make things work. 
//*>                = 4:    RANGE='I', and the Gershgorin interval 
//*>                        initially used was too small.  No eigenvalues 
//*>                        were computed. 
//*>                        Probable cause: your machine has sloppy 
//*>                                        floating-point arithmetic. 
//*>                        Cure: Increase the PARAMETER "FUDGE", 
//*>                              recompile, and try again. 
//*> \endverbatim 
//* 
//*> \par Internal Parameters: 
//*  ========================= 
//*> 
//*> \verbatim 
//*>  RELFAC  REAL, default = 2.0e0 
//*>          The relative tolerance.  An interval (a,b] lies within 
//*>          "relative tolerance" if  b-a < RELFAC*ulp*max(|a|,|b|), 
//*>          where "ulp" is the machine precision (distance from 1 to 
//*>          the next larger floating point number.) 
//*> 
//*>  FUDGE   REAL, default = 2 
//*>          A "fudge factor" to widen the Gershgorin intervals.  Ideally, 
//*>          a value of 1 should work, but on machines with sloppy 
//*>          arithmetic, this needs to be larger.  The default for 
//*>          publicly released versions should be large enough to handle 
//*>          the worst machine around.  Note that this has no effect 
//*>          on accuracy of the solution. 
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
//*> \ingroup auxOTHERcomputational 
//* 
//*  ===================================================================== 

	 
	public static void _g3zl7vt6(FString _wrqmi80z, FString _loathnh7, ref Int32 _dxpq0xkr, ref Single _ppzorcqs, ref Single _qqhwr930, ref Int32 _ic6kua09, ref Int32 _j4l29b9c, ref Single _rltspcxj, Single* _plfm7z8g, Single* _864fslqq, ref Int32 _ev4xhht5, ref Int32 _naa7acm7, Single* _z1ioc3c8, Int32* _5zga5mk9, Int32* _nn033w1s, Single* _apig8meb, Int32* _4b6rt45i, ref Int32 _gro5yvfo)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)4 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
Single _5m0mjfxm =  2f;
Single _gbf4169i =  1f / _5m0mjfxm;
Single _7fnb0l4r =  2.1f;
Single _ak4z4zrk =  2f;
Boolean _uwgmxffk =  default;
Boolean _9vwzg4sj =  default;
Int32 _vyr1z1si =  default;
Int32 _d0i9k0it =  default;
Int32 _4zavrvex =  default;
Int32 _5pjgpo42 =  default;
Int32 _smxeww0r =  default;
Int32 _9dbezfkf =  default;
Int32 _itfnbz60 =  default;
Int32 _2vvers93 =  default;
Int32 _oxr7eu3o =  default;
Int32 _kk45tx7l =  default;
Int32 _r930ul4z =  default;
Int32 _5q9hyd4z =  default;
Int32 _o42699gn =  default;
Int32 _7u74ue5o =  default;
Int32 _qq2166xp =  default;
Int32 _11qhqs00 =  default;
Int32 _kxxzluvq =  default;
Int32 _znpjgsef =  default;
Int32 _pscq8l5q =  default;
Int32 _16jas2ek =  default;
Int32 _22sk16or =  default;
Int32 _f7059815 =  default;
Int32 _2qketc28 =  default;
Int32 _2u7bqeo4 =  default;
Single _jmec0f4x =  default;
Single _nbgcqppt =  default;
Single _lr8ennxn =  default;
Single _jf74kq7y =  default;
Single _3aphllyg =  default;
Single _eki85d4y =  default;
Single _oyap2l1x =  default;
Single _c0o9kuh7 =  default;
Single _ww3bdyup =  default;
Single _6t1khtu3 =  default;
Single _0h4yb5wu =  default;
Single _oq8p752h =  default;
Single _wkzzmhv7 =  default;
Single _okixw5up =  default;
Single _z2bllur5 =  default;
Single _ehalmmz0 =  default;
Int32* _4wo395cy =  (Int32*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Int32) * ((int)1);
string fLanavab = default;
#endregion  variable declarations
_wrqmi80z = _wrqmi80z.Convert(1);
_loathnh7 = _loathnh7.Convert(1);

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
		//*     .. Local Arrays .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		_gro5yvfo = (int)0;//* 
		//*     Decode RANGE 
		//* 
		
		if (_w8y2rzgy(_wrqmi80z ,"A" ))
		{
			
			_o42699gn = (int)1;
		}
		else
		if (_w8y2rzgy(_wrqmi80z ,"V" ))
		{
			
			_o42699gn = (int)2;
		}
		else
		if (_w8y2rzgy(_wrqmi80z ,"I" ))
		{
			
			_o42699gn = (int)3;
		}
		else
		{
			
			_o42699gn = (int)0;
		}
		//* 
		//*     Decode ORDER 
		//* 
		
		if (_w8y2rzgy(_loathnh7 ,"B" ))
		{
			
			_r930ul4z = (int)2;
		}
		else
		if (_w8y2rzgy(_loathnh7 ,"E" ))
		{
			
			_r930ul4z = (int)1;
		}
		else
		{
			
			_r930ul4z = (int)0;
		}
		//* 
		//*     Check for Errors 
		//* 
		
		if (_o42699gn <= (int)0)
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (_r930ul4z <= (int)0)
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if (_o42699gn == (int)2)
		{
			
			if (_ppzorcqs >= _qqhwr930)
			_gro5yvfo = (int)-5;
		}
		else
		if ((_o42699gn == (int)3) & ((_ic6kua09 < (int)1) | (_ic6kua09 > ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))))
		{
			
			_gro5yvfo = (int)-6;
		}
		else
		if ((_o42699gn == (int)3) & ((_j4l29b9c < ILNumerics.F2NET.Intrinsics.MIN(_dxpq0xkr ,_ic6kua09 )) | (_j4l29b9c > _dxpq0xkr)))
		{
			
			_gro5yvfo = (int)-7;
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("SSTEBZ" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		//* 
		//*     Initialize error flags 
		//* 
		
		_gro5yvfo = (int)0;
		_uwgmxffk = false;
		_9vwzg4sj = false;//* 
		//*     Quick return if possible 
		//* 
		
		_ev4xhht5 = (int)0;
		if (_dxpq0xkr == (int)0)
		return;//* 
		//*     Simplifications: 
		//* 
		
		if (((_o42699gn == (int)3) & (_ic6kua09 == (int)1)) & (_j4l29b9c == _dxpq0xkr))
		_o42699gn = (int)1;//* 
		//*     Get machine constants 
		//*     NB is the minimum vector length for vector bisection, or 0 
		//*     if only scalar is to be done. 
		//* 
		
		_oyap2l1x = _d5tu038y("S" );
		_0h4yb5wu = _d5tu038y("P" );
		_eki85d4y = (_0h4yb5wu * _ak4z4zrk);
		_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"SSTEBZ" ," " ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) );
		if (_f7059815 <= (int)1)
		_f7059815 = (int)0;//* 
		//*     Special Case when N=1 
		//* 
		
		if (_dxpq0xkr == (int)1)
		{
			
			_naa7acm7 = (int)1;
			*(_nn033w1s+((int)1 - 1)) = (int)1;
			if ((_o42699gn == (int)2) & ((_ppzorcqs >= *(_plfm7z8g+((int)1 - 1))) | (_qqhwr930 < *(_plfm7z8g+((int)1 - 1)))))
			{
				
				_ev4xhht5 = (int)0;
			}
			else
			{
				
				*(_z1ioc3c8+((int)1 - 1)) = *(_plfm7z8g+((int)1 - 1));
				*(_5zga5mk9+((int)1 - 1)) = (int)1;
				_ev4xhht5 = (int)1;
			}
			
			return;
		}
		//* 
		//*     Compute Splitting Points 
		//* 
		
		_naa7acm7 = (int)1;
		*(_apig8meb+(_dxpq0xkr - 1)) = _d0547bi2;
		_3aphllyg = _kxg5drh2;//* 
		
		{
			System.Int32 __81fgg2dlsvn3169 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step3169 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3169;
			for (__81fgg2count3169 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3169 + __81fgg2step3169) / __81fgg2step3169)), _znpjgsef = __81fgg2dlsvn3169; __81fgg2count3169 != 0; __81fgg2count3169--, _znpjgsef += (__81fgg2step3169)) {

			{
				
				_c0o9kuh7 = __POW2(*(_864fslqq+(_znpjgsef - (int)1 - 1)));
				if (((ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_znpjgsef - 1)) * *(_plfm7z8g+(_znpjgsef - (int)1 - 1)) ) * __POW2(_0h4yb5wu)) + _oyap2l1x) > _c0o9kuh7)
				{
					
					*(_nn033w1s+(_naa7acm7 - 1)) = (_znpjgsef - (int)1);
					_naa7acm7 = (_naa7acm7 + (int)1);
					*(_apig8meb+(_znpjgsef - (int)1 - 1)) = _d0547bi2;
				}
				else
				{
					
					*(_apig8meb+(_znpjgsef - (int)1 - 1)) = _c0o9kuh7;
					_3aphllyg = ILNumerics.F2NET.Intrinsics.MAX(_3aphllyg ,_c0o9kuh7 );
				}
				
Mark10:;
				// continue
			}
						}		}
		*(_nn033w1s+(_naa7acm7 - 1)) = _dxpq0xkr;
		_3aphllyg = (_3aphllyg * _oyap2l1x);//* 
		//*     Compute Interval and ATOLI 
		//* 
		
		if (_o42699gn == (int)3)
		{
			//* 
			//*        RANGE='I': Compute the interval containing eigenvalues 
			//*                   IL through IU. 
			//* 
			//*        Compute Gershgorin interval for entire (split) matrix 
			//*        and use it as the initial interval 
			//* 
			
			_jf74kq7y = *(_plfm7z8g+((int)1 - 1));
			_lr8ennxn = *(_plfm7z8g+((int)1 - 1));
			_c0o9kuh7 = _d0547bi2;//* 
			
			{
				System.Int32 __81fgg2dlsvn3170 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step3170 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3170;
				for (__81fgg2count3170 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn3170 + __81fgg2step3170) / __81fgg2step3170)), _znpjgsef = __81fgg2dlsvn3170; __81fgg2count3170 != 0; __81fgg2count3170--, _znpjgsef += (__81fgg2step3170)) {

				{
					
					_ww3bdyup = ILNumerics.F2NET.Intrinsics.SQRT(*(_apig8meb+(_znpjgsef - 1)) );
					_jf74kq7y = ILNumerics.F2NET.Intrinsics.MAX(_jf74kq7y ,(*(_plfm7z8g+(_znpjgsef - 1)) + _c0o9kuh7) + _ww3bdyup );
					_lr8ennxn = ILNumerics.F2NET.Intrinsics.MIN(_lr8ennxn ,(*(_plfm7z8g+(_znpjgsef - 1)) - _c0o9kuh7) - _ww3bdyup );
					_c0o9kuh7 = _ww3bdyup;
Mark20:;
					// continue
				}
								}			}//* 
			
			_jf74kq7y = ILNumerics.F2NET.Intrinsics.MAX(_jf74kq7y ,*(_plfm7z8g+(_dxpq0xkr - 1)) + _c0o9kuh7 );
			_lr8ennxn = ILNumerics.F2NET.Intrinsics.MIN(_lr8ennxn ,*(_plfm7z8g+(_dxpq0xkr - 1)) - _c0o9kuh7 );
			_6t1khtu3 = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_lr8ennxn ) ,ILNumerics.F2NET.Intrinsics.ABS(_jf74kq7y ) );
			_lr8ennxn = ((_lr8ennxn - (((_7fnb0l4r * _6t1khtu3) * _0h4yb5wu) * _dxpq0xkr)) - ((_7fnb0l4r * _5m0mjfxm) * _3aphllyg));
			_jf74kq7y = ((_jf74kq7y + (((_7fnb0l4r * _6t1khtu3) * _0h4yb5wu) * _dxpq0xkr)) + (_7fnb0l4r * _3aphllyg));//* 
			//*        Compute Iteration parameters 
			//* 
			
			_7u74ue5o = (ILNumerics.F2NET.Intrinsics.INT((ILNumerics.F2NET.Intrinsics.LOG(_6t1khtu3 + _3aphllyg ) - ILNumerics.F2NET.Intrinsics.LOG(_3aphllyg )) / ILNumerics.F2NET.Intrinsics.LOG(_5m0mjfxm ) ) + (int)2);
			if (_rltspcxj <= _d0547bi2)
			{
				
				_jmec0f4x = (_0h4yb5wu * _6t1khtu3);
			}
			else
			{
				
				_jmec0f4x = _rltspcxj;
			}
			//* 
			
			*(_apig8meb+(_dxpq0xkr + (int)1 - 1)) = _lr8ennxn;
			*(_apig8meb+(_dxpq0xkr + (int)2 - 1)) = _lr8ennxn;
			*(_apig8meb+(_dxpq0xkr + (int)3 - 1)) = _jf74kq7y;
			*(_apig8meb+(_dxpq0xkr + (int)4 - 1)) = _jf74kq7y;
			*(_apig8meb+(_dxpq0xkr + (int)5 - 1)) = _lr8ennxn;
			*(_apig8meb+(_dxpq0xkr + (int)6 - 1)) = _jf74kq7y;
			*(_4b6rt45i+((int)1 - 1)) = (int)-1;
			*(_4b6rt45i+((int)2 - 1)) = (int)-1;
			*(_4b6rt45i+((int)3 - 1)) = (_dxpq0xkr + (int)1);
			*(_4b6rt45i+((int)4 - 1)) = (_dxpq0xkr + (int)1);
			*(_4b6rt45i+((int)5 - 1)) = (_ic6kua09 - (int)1);
			*(_4b6rt45i+((int)6 - 1)) = _j4l29b9c;//* 
			
			_k5yt0z1b(ref Unsafe.AsRef((int)3) ,ref _7u74ue5o ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)2) ,ref Unsafe.AsRef((int)2) ,ref _f7059815 ,ref _jmec0f4x ,ref _eki85d4y ,ref _3aphllyg ,_plfm7z8g ,_864fslqq ,_apig8meb ,(_4b6rt45i+((int)5 - 1)),(_apig8meb+(_dxpq0xkr + (int)1 - 1)),(_apig8meb+(_dxpq0xkr + (int)5 - 1)),ref _5q9hyd4z ,_4b6rt45i ,_z1ioc3c8 ,_5zga5mk9 ,ref _itfnbz60 );//* 
			
			if (*(_4b6rt45i+((int)6 - 1)) == _j4l29b9c)
			{
				
				_wkzzmhv7 = *(_apig8meb+(_dxpq0xkr + (int)1 - 1));
				_okixw5up = *(_apig8meb+(_dxpq0xkr + (int)3 - 1));
				_2qketc28 = *(_4b6rt45i+((int)1 - 1));
				_z2bllur5 = *(_apig8meb+(_dxpq0xkr + (int)4 - 1));
				_ehalmmz0 = *(_apig8meb+(_dxpq0xkr + (int)2 - 1));
				_2u7bqeo4 = *(_4b6rt45i+((int)4 - 1));
			}
			else
			{
				
				_wkzzmhv7 = *(_apig8meb+(_dxpq0xkr + (int)2 - 1));
				_okixw5up = *(_apig8meb+(_dxpq0xkr + (int)4 - 1));
				_2qketc28 = *(_4b6rt45i+((int)2 - 1));
				_z2bllur5 = *(_apig8meb+(_dxpq0xkr + (int)3 - 1));
				_ehalmmz0 = *(_apig8meb+(_dxpq0xkr + (int)1 - 1));
				_2u7bqeo4 = *(_4b6rt45i+((int)3 - 1));
			}
			//* 
			
			if ((((_2qketc28 < (int)0) | (_2qketc28 >= _dxpq0xkr)) | (_2u7bqeo4 < (int)1)) | (_2u7bqeo4 > _dxpq0xkr))
			{
				
				_gro5yvfo = (int)4;
				return;
			}
			
		}
		else
		{
			//* 
			//*        RANGE='A' or 'V' -- Set ATOLI 
			//* 
			
			_6t1khtu3 = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+((int)1 - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+((int)1 - 1)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_dxpq0xkr - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_dxpq0xkr - (int)1 - 1)) ) );//* 
			
			{
				System.Int32 __81fgg2dlsvn3171 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step3171 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3171;
				for (__81fgg2count3171 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn3171 + __81fgg2step3171) / __81fgg2step3171)), _znpjgsef = __81fgg2dlsvn3171; __81fgg2count3171 != 0; __81fgg2count3171--, _znpjgsef += (__81fgg2step3171)) {

				{
					
					_6t1khtu3 = ILNumerics.F2NET.Intrinsics.MAX(_6t1khtu3 ,(ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_znpjgsef - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_znpjgsef - (int)1 - 1)) )) + ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_znpjgsef - 1)) ) );
Mark30:;
					// continue
				}
								}			}//* 
			
			if (_rltspcxj <= _d0547bi2)
			{
				
				_jmec0f4x = (_0h4yb5wu * _6t1khtu3);
			}
			else
			{
				
				_jmec0f4x = _rltspcxj;
			}
			//* 
			
			if (_o42699gn == (int)2)
			{
				
				_wkzzmhv7 = _ppzorcqs;
				_z2bllur5 = _qqhwr930;
			}
			else
			{
				
				_wkzzmhv7 = _d0547bi2;
				_z2bllur5 = _d0547bi2;
			}
			
		}
		//* 
		//*     Find Eigenvalues -- Loop Over Blocks and recompute NWL and NWU. 
		//*     NWL accumulates the number of eigenvalues .le. WL, 
		//*     NWU accumulates the number of eigenvalues .le. WU 
		//* 
		
		_ev4xhht5 = (int)0;
		_9dbezfkf = (int)0;
		_gro5yvfo = (int)0;
		_2qketc28 = (int)0;
		_2u7bqeo4 = (int)0;//* 
		
		{
			System.Int32 __81fgg2dlsvn3172 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step3172 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3172;
			for (__81fgg2count3172 = System.Math.Max(0, (System.Int32)(((System.Int32)(_naa7acm7) - __81fgg2dlsvn3172 + __81fgg2step3172) / __81fgg2step3172)), _pscq8l5q = __81fgg2dlsvn3172; __81fgg2count3172 != 0; __81fgg2count3172--, _pscq8l5q += (__81fgg2step3172)) {

			{
				
				_kk45tx7l = _9dbezfkf;
				_d0i9k0it = (_kk45tx7l + (int)1);
				_9dbezfkf = *(_nn033w1s+(_pscq8l5q - 1));
				_oxr7eu3o = (_9dbezfkf - _kk45tx7l);//* 
				
				if (_oxr7eu3o == (int)1)
				{
					//* 
					//*           Special Case -- IN=1 
					//* 
					
					if ((_o42699gn == (int)1) | (_wkzzmhv7 >= (*(_plfm7z8g+(_d0i9k0it - 1)) - _3aphllyg)))
					_2qketc28 = (_2qketc28 + (int)1);
					if ((_o42699gn == (int)1) | (_z2bllur5 >= (*(_plfm7z8g+(_d0i9k0it - 1)) - _3aphllyg)))
					_2u7bqeo4 = (_2u7bqeo4 + (int)1);
					if ((_o42699gn == (int)1) | ((_wkzzmhv7 < (*(_plfm7z8g+(_d0i9k0it - 1)) - _3aphllyg)) & (_z2bllur5 >= (*(_plfm7z8g+(_d0i9k0it - 1)) - _3aphllyg))))
					{
						
						_ev4xhht5 = (_ev4xhht5 + (int)1);
						*(_z1ioc3c8+(_ev4xhht5 - 1)) = *(_plfm7z8g+(_d0i9k0it - 1));
						*(_5zga5mk9+(_ev4xhht5 - 1)) = _pscq8l5q;
					}
					
				}
				else
				{
					//* 
					//*           General Case -- IN > 1 
					//* 
					//*           Compute Gershgorin Interval 
					//*           and use it as the initial interval 
					//* 
					
					_jf74kq7y = *(_plfm7z8g+(_d0i9k0it - 1));
					_lr8ennxn = *(_plfm7z8g+(_d0i9k0it - 1));
					_c0o9kuh7 = _d0547bi2;//* 
					
					{
						System.Int32 __81fgg2dlsvn3173 = (System.Int32)(_d0i9k0it);
						const System.Int32 __81fgg2step3173 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3173;
						for (__81fgg2count3173 = System.Math.Max(0, (System.Int32)(((System.Int32)(_9dbezfkf - (int)1) - __81fgg2dlsvn3173 + __81fgg2step3173) / __81fgg2step3173)), _znpjgsef = __81fgg2dlsvn3173; __81fgg2count3173 != 0; __81fgg2count3173--, _znpjgsef += (__81fgg2step3173)) {

						{
							
							_ww3bdyup = ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_znpjgsef - 1)) );
							_jf74kq7y = ILNumerics.F2NET.Intrinsics.MAX(_jf74kq7y ,(*(_plfm7z8g+(_znpjgsef - 1)) + _c0o9kuh7) + _ww3bdyup );
							_lr8ennxn = ILNumerics.F2NET.Intrinsics.MIN(_lr8ennxn ,(*(_plfm7z8g+(_znpjgsef - 1)) - _c0o9kuh7) - _ww3bdyup );
							_c0o9kuh7 = _ww3bdyup;
Mark40:;
							// continue
						}
												}					}//* 
					
					_jf74kq7y = ILNumerics.F2NET.Intrinsics.MAX(_jf74kq7y ,*(_plfm7z8g+(_9dbezfkf - 1)) + _c0o9kuh7 );
					_lr8ennxn = ILNumerics.F2NET.Intrinsics.MIN(_lr8ennxn ,*(_plfm7z8g+(_9dbezfkf - 1)) - _c0o9kuh7 );
					_nbgcqppt = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_lr8ennxn ) ,ILNumerics.F2NET.Intrinsics.ABS(_jf74kq7y ) );
					_lr8ennxn = ((_lr8ennxn - (((_7fnb0l4r * _nbgcqppt) * _0h4yb5wu) * _oxr7eu3o)) - (_7fnb0l4r * _3aphllyg));
					_jf74kq7y = ((_jf74kq7y + (((_7fnb0l4r * _nbgcqppt) * _0h4yb5wu) * _oxr7eu3o)) + (_7fnb0l4r * _3aphllyg));//* 
					//*           Compute ATOLI for the current submatrix 
					//* 
					
					if (_rltspcxj <= _d0547bi2)
					{
						
						_jmec0f4x = (_0h4yb5wu * ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_lr8ennxn ) ,ILNumerics.F2NET.Intrinsics.ABS(_jf74kq7y ) ));
					}
					else
					{
						
						_jmec0f4x = _rltspcxj;
					}
					//* 
					
					if (_o42699gn > (int)1)
					{
						
						if (_jf74kq7y < _wkzzmhv7)
						{
							
							_2qketc28 = (_2qketc28 + _oxr7eu3o);
							_2u7bqeo4 = (_2u7bqeo4 + _oxr7eu3o);goto Mark70;
						}
						
						_lr8ennxn = ILNumerics.F2NET.Intrinsics.MAX(_lr8ennxn ,_wkzzmhv7 );
						_jf74kq7y = ILNumerics.F2NET.Intrinsics.MIN(_jf74kq7y ,_z2bllur5 );
						if (_lr8ennxn >= _jf74kq7y)goto Mark70;
					}
					//* 
					//*           Set Up Initial Interval 
					//* 
					
					*(_apig8meb+(_dxpq0xkr + (int)1 - 1)) = _lr8ennxn;
					*(_apig8meb+((_dxpq0xkr + _oxr7eu3o) + (int)1 - 1)) = _jf74kq7y;
					_k5yt0z1b(ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef((int)0) ,ref _oxr7eu3o ,ref _oxr7eu3o ,ref Unsafe.AsRef((int)1) ,ref _f7059815 ,ref _jmec0f4x ,ref _eki85d4y ,ref _3aphllyg ,(_plfm7z8g+(_d0i9k0it - 1)),(_864fslqq+(_d0i9k0it - 1)),(_apig8meb+(_d0i9k0it - 1)),_4wo395cy ,(_apig8meb+(_dxpq0xkr + (int)1 - 1)),(_apig8meb+((_dxpq0xkr + ((int)2 * _oxr7eu3o)) + (int)1 - 1)),ref _2vvers93 ,_4b6rt45i ,(_z1ioc3c8+(_ev4xhht5 + (int)1 - 1)),(_5zga5mk9+(_ev4xhht5 + (int)1 - 1)),ref _itfnbz60 );//* 
					
					_2qketc28 = (_2qketc28 + *(_4b6rt45i+((int)1 - 1)));
					_2u7bqeo4 = (_2u7bqeo4 + *(_4b6rt45i+(_oxr7eu3o + (int)1 - 1)));
					_kxxzluvq = (_ev4xhht5 - *(_4b6rt45i+((int)1 - 1)));//* 
					//*           Compute Eigenvalues 
					//* 
					
					_7u74ue5o = (ILNumerics.F2NET.Intrinsics.INT((ILNumerics.F2NET.Intrinsics.LOG((_jf74kq7y - _lr8ennxn) + _3aphllyg ) - ILNumerics.F2NET.Intrinsics.LOG(_3aphllyg )) / ILNumerics.F2NET.Intrinsics.LOG(_5m0mjfxm ) ) + (int)2);
					_k5yt0z1b(ref Unsafe.AsRef((int)2) ,ref _7u74ue5o ,ref _oxr7eu3o ,ref _oxr7eu3o ,ref Unsafe.AsRef((int)1) ,ref _f7059815 ,ref _jmec0f4x ,ref _eki85d4y ,ref _3aphllyg ,(_plfm7z8g+(_d0i9k0it - 1)),(_864fslqq+(_d0i9k0it - 1)),(_apig8meb+(_d0i9k0it - 1)),_4wo395cy ,(_apig8meb+(_dxpq0xkr + (int)1 - 1)),(_apig8meb+((_dxpq0xkr + ((int)2 * _oxr7eu3o)) + (int)1 - 1)),ref _5q9hyd4z ,_4b6rt45i ,(_z1ioc3c8+(_ev4xhht5 + (int)1 - 1)),(_5zga5mk9+(_ev4xhht5 + (int)1 - 1)),ref _itfnbz60 );//* 
					//*           Copy Eigenvalues Into W and IBLOCK 
					//*           Use -JB for block number for unconverged eigenvalues. 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn3174 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3174 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3174;
						for (__81fgg2count3174 = System.Math.Max(0, (System.Int32)(((System.Int32)(_5q9hyd4z) - __81fgg2dlsvn3174 + __81fgg2step3174) / __81fgg2step3174)), _znpjgsef = __81fgg2dlsvn3174; __81fgg2count3174 != 0; __81fgg2count3174--, _znpjgsef += (__81fgg2step3174)) {

						{
							
							_c0o9kuh7 = (_gbf4169i * (*(_apig8meb+(_znpjgsef + _dxpq0xkr - 1)) + *(_apig8meb+((_znpjgsef + _oxr7eu3o) + _dxpq0xkr - 1))));//* 
							//*              Flag non-convergence. 
							//* 
							
							if (_znpjgsef > (_5q9hyd4z - _itfnbz60))
							{
								
								_uwgmxffk = true;
								_vyr1z1si = (-(_pscq8l5q));
							}
							else
							{
								
								_vyr1z1si = _pscq8l5q;
							}
							
							{
								System.Int32 __81fgg2dlsvn3175 = (System.Int32)(((*(_4b6rt45i+(_znpjgsef - 1)) + (int)1) + _kxxzluvq));
								const System.Int32 __81fgg2step3175 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3175;
								for (__81fgg2count3175 = System.Math.Max(0, (System.Int32)(((System.Int32)(*(_4b6rt45i+(_znpjgsef + _oxr7eu3o - 1)) + _kxxzluvq) - __81fgg2dlsvn3175 + __81fgg2step3175) / __81fgg2step3175)), _22sk16or = __81fgg2dlsvn3175; __81fgg2count3175 != 0; __81fgg2count3175--, _22sk16or += (__81fgg2step3175)) {

								{
									
									*(_z1ioc3c8+(_22sk16or - 1)) = _c0o9kuh7;
									*(_5zga5mk9+(_22sk16or - 1)) = _vyr1z1si;
Mark50:;
									// continue
								}
																}							}
Mark60:;
							// continue
						}
												}					}//* 
					
					_ev4xhht5 = (_ev4xhht5 + _2vvers93);
				}
				
Mark70:;
				// continue
			}
						}		}//* 
		//*     If RANGE='I', then (WL,WU) contains eigenvalues NWL+1,...,NWU 
		//*     If NWL+1 < IL or NWU > IU, discard extra eigenvalues. 
		//* 
		
		if (_o42699gn == (int)3)
		{
			
			_2vvers93 = (int)0;
			_4zavrvex = ((_ic6kua09 - (int)1) - _2qketc28);
			_5pjgpo42 = (_2u7bqeo4 - _j4l29b9c);//* 
			
			if ((_4zavrvex > (int)0) | (_5pjgpo42 > (int)0))
			{
				
				{
					System.Int32 __81fgg2dlsvn3176 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3176 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3176;
					for (__81fgg2count3176 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3176 + __81fgg2step3176) / __81fgg2step3176)), _22sk16or = __81fgg2dlsvn3176; __81fgg2count3176 != 0; __81fgg2count3176--, _22sk16or += (__81fgg2step3176)) {

					{
						
						if ((*(_z1ioc3c8+(_22sk16or - 1)) <= _okixw5up) & (_4zavrvex > (int)0))
						{
							
							_4zavrvex = (_4zavrvex - (int)1);
						}
						else
						if ((*(_z1ioc3c8+(_22sk16or - 1)) >= _ehalmmz0) & (_5pjgpo42 > (int)0))
						{
							
							_5pjgpo42 = (_5pjgpo42 - (int)1);
						}
						else
						{
							
							_2vvers93 = (_2vvers93 + (int)1);
							*(_z1ioc3c8+(_2vvers93 - 1)) = *(_z1ioc3c8+(_22sk16or - 1));
							*(_5zga5mk9+(_2vvers93 - 1)) = *(_5zga5mk9+(_22sk16or - 1));
						}
						
Mark80:;
						// continue
					}
										}				}
				_ev4xhht5 = _2vvers93;
			}
			
			if ((_4zavrvex > (int)0) | (_5pjgpo42 > (int)0))
			{
				//* 
				//*           Code to deal with effects of bad arithmetic: 
				//*           Some low eigenvalues to be discarded are not in (WL,WLU], 
				//*           or high eigenvalues to be discarded are not in (WUL,WU] 
				//*           so just kill off the smallest IDISCL/largest IDISCU 
				//*           eigenvalues, by simply finding the smallest/largest 
				//*           eigenvalue(s). 
				//* 
				//*           (If N(w) is monotone non-decreasing, this should never 
				//*               happen.) 
				//* 
				
				if (_4zavrvex > (int)0)
				{
					
					_oq8p752h = _z2bllur5;
					{
						System.Int32 __81fgg2dlsvn3177 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3177 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3177;
						for (__81fgg2count3177 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4zavrvex) - __81fgg2dlsvn3177 + __81fgg2step3177) / __81fgg2step3177)), _16jas2ek = __81fgg2dlsvn3177; __81fgg2count3177 != 0; __81fgg2count3177--, _16jas2ek += (__81fgg2step3177)) {

						{
							
							_11qhqs00 = (int)0;
							{
								System.Int32 __81fgg2dlsvn3178 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3178 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3178;
								for (__81fgg2count3178 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3178 + __81fgg2step3178) / __81fgg2step3178)), _22sk16or = __81fgg2dlsvn3178; __81fgg2count3178 != 0; __81fgg2count3178--, _22sk16or += (__81fgg2step3178)) {

								{
									
									if ((*(_5zga5mk9+(_22sk16or - 1)) != (int)0) & ((*(_z1ioc3c8+(_22sk16or - 1)) < _oq8p752h) | (_11qhqs00 == (int)0)))
									{
										
										_11qhqs00 = _22sk16or;
										_oq8p752h = *(_z1ioc3c8+(_22sk16or - 1));
									}
									
Mark90:;
									// continue
								}
																}							}
							*(_5zga5mk9+(_11qhqs00 - 1)) = (int)0;
Mark100:;
							// continue
						}
												}					}
				}
				
				if (_5pjgpo42 > (int)0)
				{
					//* 
					
					_oq8p752h = _wkzzmhv7;
					{
						System.Int32 __81fgg2dlsvn3179 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3179 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3179;
						for (__81fgg2count3179 = System.Math.Max(0, (System.Int32)(((System.Int32)(_5pjgpo42) - __81fgg2dlsvn3179 + __81fgg2step3179) / __81fgg2step3179)), _16jas2ek = __81fgg2dlsvn3179; __81fgg2count3179 != 0; __81fgg2count3179--, _16jas2ek += (__81fgg2step3179)) {

						{
							
							_11qhqs00 = (int)0;
							{
								System.Int32 __81fgg2dlsvn3180 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step3180 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3180;
								for (__81fgg2count3180 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3180 + __81fgg2step3180) / __81fgg2step3180)), _22sk16or = __81fgg2dlsvn3180; __81fgg2count3180 != 0; __81fgg2count3180--, _22sk16or += (__81fgg2step3180)) {

								{
									
									if ((*(_5zga5mk9+(_22sk16or - 1)) != (int)0) & ((*(_z1ioc3c8+(_22sk16or - 1)) > _oq8p752h) | (_11qhqs00 == (int)0)))
									{
										
										_11qhqs00 = _22sk16or;
										_oq8p752h = *(_z1ioc3c8+(_22sk16or - 1));
									}
									
Mark110:;
									// continue
								}
																}							}
							*(_5zga5mk9+(_11qhqs00 - 1)) = (int)0;
Mark120:;
							// continue
						}
												}					}
				}
				
				_2vvers93 = (int)0;
				{
					System.Int32 __81fgg2dlsvn3181 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3181 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3181;
					for (__81fgg2count3181 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3181 + __81fgg2step3181) / __81fgg2step3181)), _22sk16or = __81fgg2dlsvn3181; __81fgg2count3181 != 0; __81fgg2count3181--, _22sk16or += (__81fgg2step3181)) {

					{
						
						if (*(_5zga5mk9+(_22sk16or - 1)) != (int)0)
						{
							
							_2vvers93 = (_2vvers93 + (int)1);
							*(_z1ioc3c8+(_2vvers93 - 1)) = *(_z1ioc3c8+(_22sk16or - 1));
							*(_5zga5mk9+(_2vvers93 - 1)) = *(_5zga5mk9+(_22sk16or - 1));
						}
						
Mark130:;
						// continue
					}
										}				}
				_ev4xhht5 = _2vvers93;
			}
			
			if ((_4zavrvex < (int)0) | (_5pjgpo42 < (int)0))
			{
				
				_9vwzg4sj = true;
			}
			
		}
		//* 
		//*     If ORDER='B', do nothing -- the eigenvalues are already sorted 
		//*        by block. 
		//*     If ORDER='E', sort the eigenvalues from smallest to largest 
		//* 
		
		if ((_r930ul4z == (int)1) & (_naa7acm7 > (int)1))
		{
			
			{
				System.Int32 __81fgg2dlsvn3182 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step3182 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3182;
				for (__81fgg2count3182 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5 - (int)1) - __81fgg2dlsvn3182 + __81fgg2step3182) / __81fgg2step3182)), _22sk16or = __81fgg2dlsvn3182; __81fgg2count3182 != 0; __81fgg2count3182--, _22sk16or += (__81fgg2step3182)) {

				{
					
					_smxeww0r = (int)0;
					_c0o9kuh7 = *(_z1ioc3c8+(_22sk16or - 1));
					{
						System.Int32 __81fgg2dlsvn3183 = (System.Int32)((_22sk16or + (int)1));
						const System.Int32 __81fgg2step3183 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3183;
						for (__81fgg2count3183 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3183 + __81fgg2step3183) / __81fgg2step3183)), _znpjgsef = __81fgg2dlsvn3183; __81fgg2count3183 != 0; __81fgg2count3183--, _znpjgsef += (__81fgg2step3183)) {

						{
							
							if (*(_z1ioc3c8+(_znpjgsef - 1)) < _c0o9kuh7)
							{
								
								_smxeww0r = _znpjgsef;
								_c0o9kuh7 = *(_z1ioc3c8+(_znpjgsef - 1));
							}
							
Mark140:;
							// continue
						}
												}					}//* 
					
					if (_smxeww0r != (int)0)
					{
						
						_qq2166xp = *(_5zga5mk9+(_smxeww0r - 1));
						*(_z1ioc3c8+(_smxeww0r - 1)) = *(_z1ioc3c8+(_22sk16or - 1));
						*(_5zga5mk9+(_smxeww0r - 1)) = *(_5zga5mk9+(_22sk16or - 1));
						*(_z1ioc3c8+(_22sk16or - 1)) = _c0o9kuh7;
						*(_5zga5mk9+(_22sk16or - 1)) = _qq2166xp;
					}
					
Mark150:;
					// continue
				}
								}			}
		}
		//* 
		
		_gro5yvfo = (int)0;
		if (_uwgmxffk)
		_gro5yvfo = (_gro5yvfo + (int)1);
		if (_9vwzg4sj)
		_gro5yvfo = (_gro5yvfo + (int)2);
		return;//* 
		//*     End of SSTEBZ 
		//* 
		
	}
	
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
