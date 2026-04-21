// MIT License
// 
// Copyright (c) 2026 ILNumerics GmbH
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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
//*> \brief \b DLALN2 solves a 1-by-1 or 2-by-2 linear system of equations of the specified form. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLALN2 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlaln2.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlaln2.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlaln2.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLALN2( LTRANS, NA, NW, SMIN, CA, A, LDA, D1, D2, B, 
//*                          LDB, WR, WI, X, LDX, SCALE, XNORM, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       LOGICAL            LTRANS 
//*       INTEGER            INFO, LDA, LDB, LDX, NA, NW 
//*       DOUBLE PRECISION   CA, D1, D2, SCALE, SMIN, WI, WR, XNORM 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   A( LDA, * ), B( LDB, * ), X( LDX, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLALN2 solves a system of the form  (ca A - w D ) X = s B 
//*> or (ca A**T - w D) X = s B   with possible scaling ("s") and 
//*> perturbation of A.  (A**T means A-transpose.) 
//*> 
//*> A is an NA x NA real matrix, ca is a real scalar, D is an NA x NA 
//*> real diagonal matrix, w is a real or complex value, and X and B are 
//*> NA x 1 matrices -- real if w is real, complex if w is complex.  NA 
//*> may be 1 or 2. 
//*> 
//*> If w is complex, X and B are represented as NA x 2 matrices, 
//*> the first column of each being the real part and the second 
//*> being the imaginary part. 
//*> 
//*> "s" is a scaling factor (<= 1), computed by DLALN2, which is 
//*> so chosen that X can be computed without overflow.  X is further 
//*> scaled if necessary to assure that norm(ca A - w D)*norm(X) is less 
//*> than overflow. 
//*> 
//*> If both singular values of (ca A - w D) are less than SMIN, 
//*> SMIN*identity will be used instead of (ca A - w D).  If only one 
//*> singular value is less than SMIN, one element of (ca A - w D) will be 
//*> perturbed enough to make the smallest singular value roughly SMIN. 
//*> If both singular values are at least SMIN, (ca A - w D) will not be 
//*> perturbed.  In any case, the perturbation will be at most some small 
//*> multiple of max( SMIN, ulp*norm(ca A - w D) ).  The singular values 
//*> are computed by infinity-norm approximations, and thus will only be 
//*> correct to a factor of 2 or so. 
//*> 
//*> Note: all input quantities are assumed to be smaller than overflow 
//*> by a reasonable factor.  (See BIGNUM.) 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] LTRANS 
//*> \verbatim 
//*>          LTRANS is LOGICAL 
//*>          =.TRUE.:  A-transpose will be used. 
//*>          =.FALSE.: A will be used (not transposed.) 
//*> \endverbatim 
//*> 
//*> \param[in] NA 
//*> \verbatim 
//*>          NA is INTEGER 
//*>          The size of the matrix A.  It may (only) be 1 or 2. 
//*> \endverbatim 
//*> 
//*> \param[in] NW 
//*> \verbatim 
//*>          NW is INTEGER 
//*>          1 if "w" is real, 2 if "w" is complex.  It may only be 1 
//*>          or 2. 
//*> \endverbatim 
//*> 
//*> \param[in] SMIN 
//*> \verbatim 
//*>          SMIN is DOUBLE PRECISION 
//*>          The desired lower bound on the singular values of A.  This 
//*>          should be a safe distance away from underflow or overflow, 
//*>          say, between (underflow/machine precision) and  (machine 
//*>          precision * overflow ).  (See BIGNUM and ULP.) 
//*> \endverbatim 
//*> 
//*> \param[in] CA 
//*> \verbatim 
//*>          CA is DOUBLE PRECISION 
//*>          The coefficient c, which A is multiplied by. 
//*> \endverbatim 
//*> 
//*> \param[in] A 
//*> \verbatim 
//*>          A is DOUBLE PRECISION array, dimension (LDA,NA) 
//*>          The NA x NA matrix A. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of A.  It must be at least NA. 
//*> \endverbatim 
//*> 
//*> \param[in] D1 
//*> \verbatim 
//*>          D1 is DOUBLE PRECISION 
//*>          The 1,1 element in the diagonal matrix D. 
//*> \endverbatim 
//*> 
//*> \param[in] D2 
//*> \verbatim 
//*>          D2 is DOUBLE PRECISION 
//*>          The 2,2 element in the diagonal matrix D.  Not used if NA=1. 
//*> \endverbatim 
//*> 
//*> \param[in] B 
//*> \verbatim 
//*>          B is DOUBLE PRECISION array, dimension (LDB,NW) 
//*>          The NA x NW matrix B (right-hand side).  If NW=2 ("w" is 
//*>          complex), column 1 contains the real part of B and column 2 
//*>          contains the imaginary part. 
//*> \endverbatim 
//*> 
//*> \param[in] LDB 
//*> \verbatim 
//*>          LDB is INTEGER 
//*>          The leading dimension of B.  It must be at least NA. 
//*> \endverbatim 
//*> 
//*> \param[in] WR 
//*> \verbatim 
//*>          WR is DOUBLE PRECISION 
//*>          The real part of the scalar "w". 
//*> \endverbatim 
//*> 
//*> \param[in] WI 
//*> \verbatim 
//*>          WI is DOUBLE PRECISION 
//*>          The imaginary part of the scalar "w".  Not used if NW=1. 
//*> \endverbatim 
//*> 
//*> \param[out] X 
//*> \verbatim 
//*>          X is DOUBLE PRECISION array, dimension (LDX,NW) 
//*>          The NA x NW matrix X (unknowns), as computed by DLALN2. 
//*>          If NW=2 ("w" is complex), on exit, column 1 will contain 
//*>          the real part of X and column 2 will contain the imaginary 
//*>          part. 
//*> \endverbatim 
//*> 
//*> \param[in] LDX 
//*> \verbatim 
//*>          LDX is INTEGER 
//*>          The leading dimension of X.  It must be at least NA. 
//*> \endverbatim 
//*> 
//*> \param[out] SCALE 
//*> \verbatim 
//*>          SCALE is DOUBLE PRECISION 
//*>          The scale factor that B must be multiplied by to insure 
//*>          that overflow does not occur when computing X.  Thus, 
//*>          (ca A - w D) X  will be SCALE*B, not B (ignoring 
//*>          perturbations of A.)  It will be at most 1. 
//*> \endverbatim 
//*> 
//*> \param[out] XNORM 
//*> \verbatim 
//*>          XNORM is DOUBLE PRECISION 
//*>          The infinity-norm of X, when X is regarded as an NA x NW 
//*>          real matrix. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          An error flag.  It will be set to zero if no error occurs, 
//*>          a negative number if an argument is in error, or a positive 
//*>          number if  ca A - w D  had to be perturbed. 
//*>          The possible values are: 
//*>          = 0: No error occurred, and (ca A - w D) did not have to be 
//*>                 perturbed. 
//*>          = 1: (ca A - w D) had to be perturbed to make its smallest 
//*>               (or only) singular value greater than SMIN. 
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
//*> \date December 2016 
//* 
//*> \ingroup doubleOTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _6andobxo(ref Boolean _6min07r7, ref Int32 _fopltai1, ref Int32 _w6pmxgch, ref Double _rhnpgpoi, ref Double _han26hfi, Double* _vxfgpup9, ref Int32 _ocv8fk5c, ref Double _5v2i3gjn, ref Double _vt4856ip, Double* _p9n405a5, ref Int32 _ly9opahg, ref Double _b5j6m2b7, ref Double _nc0qphpn, Double* _ta7zuy9k, ref Int32 _eeyyzhrs, ref Double _1m44vtuk, ref Double _ziu6urj2, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Double _5m0mjfxm =  2d;
Int32 _gjnpgmw7 =  default;
Int32 _znpjgsef =  default;
Double _ox4i8otb =  default;
Double _dc012721 =  default;
Double _9leixcp0 =  default;
Double _av7j8yda =  default;
Double _nbgcqppt =  default;
Double _nqy1hops =  default;
Double _2gjyh0fi =  default;
Double _qwq5hojl =  default;
Double _id56iquy =  default;
Double _xgnmjbtu =  default;
Double _5wpr31sx =  default;
Double _68w48tl9 =  default;
Double _gnsdttkm =  default;
Double _vvbbm29o =  default;
Double _3x3t9ums =  default;
Double _c6av82sq =  default;
Double _viaj40zd =  default;
Double _4nf5sqam =  default;
Double _bogm0gwy =  default;
Double _1ajfmh55 =  default;
Double _md5izrkd =  default;
Double _9h1cs0bo =  default;
Double _5bslsar5 =  default;
Double _57h8wd6w =  default;
Double _6yj46j1e =  default;
Double _76kx8wue =  default;
Double _m0aq8y3l =  default;
Double _wam2klr5 =  default;
Double _kfg4qgsv =  default;
Double _knnw036i =  default;
Double _mdv5dpxh =  default;
Double _47i5794w =  default;
Double _pom2qzxg =  default;
Double _iogulgv2 =  default;
Double _ddphzv25 =  default;
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
		//*     .. Equivalences .. 
		//*     .. 
		//*     .. Data statements .. 
		
		{var vals = new Boolean[] { false,false,true,true };var valsIter = 0;

		{ for (int _i72810g = 0; _i72810g < 4;) { __dlaln2._027ahmgj[_i72810g++] = vals[valsIter++]; }  };
		}
		{var vals = new Boolean[] { false,true,false,true };var valsIter = 0;

		{ for (int _i72810g = 0; _i72810g < 4;) { __dlaln2._r9at8wxl[_i72810g++] = vals[valsIter++]; }  };
		}
		{var vals = new Int32[] { (int)1,(int)2,(int)3,(int)4,(int)2,(int)1,(int)4,(int)3,(int)3,(int)4,(int)1,(int)2,(int)4,(int)3,(int)2,(int)1 };var valsIter = 0;

		{ for (int _i72810g = 0; _i72810g < 16;) { __dlaln2._t1hlqe2w[_i72810g++] = vals[valsIter++]; }  };
		}//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Compute BIGNUM 
		//* 
		
		_bogm0gwy = (_5m0mjfxm * _f43eg0w0("Safe minimum" ));
		_av7j8yda = (_kxg5drh2 / _bogm0gwy);
		_4nf5sqam = ILNumerics.F2NET.Intrinsics.MAX(_rhnpgpoi ,_bogm0gwy );//* 
		//*     Don't check for input errors 
		//* 
		
		_gro5yvfo = (int)0;//* 
		//*     Standard Initializations 
		//* 
		
		_1m44vtuk = _kxg5drh2;//* 
		
		if (_fopltai1 == (int)1)
		{
			//* 
			//*        1 x 1  (i.e., scalar) system   C X = B 
			//* 
			
			if (_w6pmxgch == (int)1)
			{
				//* 
				//*           Real 1x1 system. 
				//* 
				//*           C = ca A - w D 
				//* 
				
				_3x3t9ums = ((_han26hfi * *(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c))) - (_b5j6m2b7 * _5v2i3gjn));
				_5wpr31sx = ILNumerics.F2NET.Intrinsics.ABS(_3x3t9ums );//* 
				//*           If | C | < SMINI, use C = SMINI 
				//* 
				
				if (_5wpr31sx < _4nf5sqam)
				{
					
					_3x3t9ums = _4nf5sqam;
					_5wpr31sx = _4nf5sqam;
					_gro5yvfo = (int)1;
				}
				//* 
				//*           Check scaling for  X = B / C 
				//* 
				
				_nbgcqppt = ILNumerics.F2NET.Intrinsics.ABS(*(_p9n405a5+((int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)) );
				if ((_5wpr31sx < _kxg5drh2) & (_nbgcqppt > _kxg5drh2))
				{
					
					if (_nbgcqppt > (_av7j8yda * _5wpr31sx))
					_1m44vtuk = (_kxg5drh2 / _nbgcqppt);
				}
				//* 
				//*           Compute X 
				//* 
				
				*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) = ((*(_p9n405a5+((int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)) * _1m44vtuk) / _3x3t9ums);
				_ziu6urj2 = ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) );
			}
			else
			{
				//* 
				//*           Complex 1x1 system (w is complex) 
				//* 
				//*           C = ca A - w D 
				//* 
				
				_3x3t9ums = ((_han26hfi * *(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c))) - (_b5j6m2b7 * _5v2i3gjn));
				_vvbbm29o = (-((_nc0qphpn * _5v2i3gjn)));
				_5wpr31sx = (ILNumerics.F2NET.Intrinsics.ABS(_3x3t9ums ) + ILNumerics.F2NET.Intrinsics.ABS(_vvbbm29o ));//* 
				//*           If | C | < SMINI, use C = SMINI 
				//* 
				
				if (_5wpr31sx < _4nf5sqam)
				{
					
					_3x3t9ums = _4nf5sqam;
					_vvbbm29o = _d0547bi2;
					_5wpr31sx = _4nf5sqam;
					_gro5yvfo = (int)1;
				}
				//* 
				//*           Check scaling for  X = B / C 
				//* 
				
				_nbgcqppt = (ILNumerics.F2NET.Intrinsics.ABS(*(_p9n405a5+((int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_p9n405a5+((int)1 - 1) + ((int)2 - 1) * 1 * (_ly9opahg)) ));
				if ((_5wpr31sx < _kxg5drh2) & (_nbgcqppt > _kxg5drh2))
				{
					
					if (_nbgcqppt > (_av7j8yda * _5wpr31sx))
					_1m44vtuk = (_kxg5drh2 / _nbgcqppt);
				}
				//* 
				//*           Compute X 
				//* 
				
				_x0fujx9g(ref Unsafe.AsRef(_1m44vtuk * *(_p9n405a5+((int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg))) ,ref Unsafe.AsRef(_1m44vtuk * *(_p9n405a5+((int)1 - 1) + ((int)2 - 1) * 1 * (_ly9opahg))) ,ref _3x3t9ums ,ref _vvbbm29o ,ref Unsafe.AsRef(*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs))) ,ref Unsafe.AsRef(*(_ta7zuy9k+((int)1 - 1) + ((int)2 - 1) * 1 * (_eeyyzhrs))) );
				_ziu6urj2 = (ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+((int)1 - 1) + ((int)2 - 1) * 1 * (_eeyyzhrs)) ));
			}
			//* 
			
		}
		else
		{
			//* 
			//*        2x2 System 
			//* 
			//*        Compute the real part of  C = ca A - w D  (or  ca A**T - w D ) 
			//* 
			
			*(__dlaln2._14at9km9+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2)) = ((_han26hfi * *(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c))) - (_b5j6m2b7 * _5v2i3gjn));
			*(__dlaln2._14at9km9+((int)2 - 1) + ((int)2 - 1) * 1 * ((int)2)) = ((_han26hfi * *(_vxfgpup9+((int)2 - 1) + ((int)2 - 1) * 1 * (_ocv8fk5c))) - (_b5j6m2b7 * _vt4856ip));
			if (_6min07r7)
			{
				
				*(__dlaln2._14at9km9+((int)1 - 1) + ((int)2 - 1) * 1 * ((int)2)) = (_han26hfi * *(_vxfgpup9+((int)2 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)));
				*(__dlaln2._14at9km9+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2)) = (_han26hfi * *(_vxfgpup9+((int)1 - 1) + ((int)2 - 1) * 1 * (_ocv8fk5c)));
			}
			else
			{
				
				*(__dlaln2._14at9km9+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2)) = (_han26hfi * *(_vxfgpup9+((int)2 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)));
				*(__dlaln2._14at9km9+((int)1 - 1) + ((int)2 - 1) * 1 * ((int)2)) = (_han26hfi * *(_vxfgpup9+((int)1 - 1) + ((int)2 - 1) * 1 * (_ocv8fk5c)));
			}
			//* 
			
			if (_w6pmxgch == (int)1)
			{
				//* 
				//*           Real 2x2 system  (w is real) 
				//* 
				//*           Find the largest element in C 
				//* 
				
				_xgnmjbtu = _d0547bi2;
				_gjnpgmw7 = (int)0;//* 
				
				{
					System.Int32 __81fgg2dlsvn2315 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2315 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2315;
					for (__81fgg2count2315 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)4) - __81fgg2dlsvn2315 + __81fgg2step2315) / __81fgg2step2315)), _znpjgsef = __81fgg2dlsvn2315; __81fgg2count2315 != 0; __81fgg2count2315--, _znpjgsef += (__81fgg2step2315)) {

					{
						
						if (ILNumerics.F2NET.Intrinsics.ABS(*(__dlaln2._wrlqzehw+(_znpjgsef - 1)) ) > _xgnmjbtu)
						{
							
							_xgnmjbtu = ILNumerics.F2NET.Intrinsics.ABS(*(__dlaln2._wrlqzehw+(_znpjgsef - 1)) );
							_gjnpgmw7 = _znpjgsef;
						}
						
Mark10:;
						// continue
					}
										}				}//* 
				//*           If norm(C) < SMINI, use SMINI*identity. 
				//* 
				
				if (_xgnmjbtu < _4nf5sqam)
				{
					
					_nbgcqppt = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_p9n405a5+((int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_p9n405a5+((int)2 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)) ) );
					if ((_4nf5sqam < _kxg5drh2) & (_nbgcqppt > _kxg5drh2))
					{
						
						if (_nbgcqppt > (_av7j8yda * _4nf5sqam))
						_1m44vtuk = (_kxg5drh2 / _nbgcqppt);
					}
					
					_1ajfmh55 = (_1m44vtuk / _4nf5sqam);
					*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) = (_1ajfmh55 * *(_p9n405a5+((int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)));
					*(_ta7zuy9k+((int)2 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) = (_1ajfmh55 * *(_p9n405a5+((int)2 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)));
					_ziu6urj2 = (_1ajfmh55 * _nbgcqppt);
					_gro5yvfo = (int)1;
					return;
				}
				//* 
				//*           Gaussian elimination with complete pivoting. 
				//* 
				
				_m0aq8y3l = *(__dlaln2._wrlqzehw+(_gjnpgmw7 - 1));
				_68w48tl9 = *(__dlaln2._wrlqzehw+(*(__dlaln2._t1hlqe2w+((int)2 - 1) + (_gjnpgmw7 - 1) * 1 * ((int)4)) - 1));
				_kfg4qgsv = *(__dlaln2._wrlqzehw+(*(__dlaln2._t1hlqe2w+((int)3 - 1) + (_gjnpgmw7 - 1) * 1 * ((int)4)) - 1));
				_gnsdttkm = *(__dlaln2._wrlqzehw+(*(__dlaln2._t1hlqe2w+((int)4 - 1) + (_gjnpgmw7 - 1) * 1 * ((int)4)) - 1));
				_wam2klr5 = (_kxg5drh2 / _m0aq8y3l);
				_viaj40zd = (_wam2klr5 * _68w48tl9);
				_mdv5dpxh = (_gnsdttkm - (_kfg4qgsv * _viaj40zd));//* 
				//*           If smaller pivot < SMINI, use SMINI 
				//* 
				
				if (ILNumerics.F2NET.Intrinsics.ABS(_mdv5dpxh ) < _4nf5sqam)
				{
					
					_mdv5dpxh = _4nf5sqam;
					_gro5yvfo = (int)1;
				}
				
				if (*(__dlaln2._r9at8wxl+(_gjnpgmw7 - 1)))
				{
					
					_nqy1hops = *(_p9n405a5+((int)2 - 1) + ((int)1 - 1) * 1 * (_ly9opahg));
					_2gjyh0fi = *(_p9n405a5+((int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg));
				}
				else
				{
					
					_nqy1hops = *(_p9n405a5+((int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg));
					_2gjyh0fi = *(_p9n405a5+((int)2 - 1) + ((int)1 - 1) * 1 * (_ly9opahg));
				}
				
				_2gjyh0fi = (_2gjyh0fi - (_viaj40zd * _nqy1hops));
				_ox4i8otb = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_nqy1hops * (_mdv5dpxh * _wam2klr5) ) ,ILNumerics.F2NET.Intrinsics.ABS(_2gjyh0fi ) );
				if ((_ox4i8otb > _kxg5drh2) & (ILNumerics.F2NET.Intrinsics.ABS(_mdv5dpxh ) < _kxg5drh2))
				{
					
					if (_ox4i8otb >= (_av7j8yda * ILNumerics.F2NET.Intrinsics.ABS(_mdv5dpxh )))
					_1m44vtuk = (_kxg5drh2 / _ox4i8otb);
				}
				//* 
				
				_ddphzv25 = ((_2gjyh0fi * _1m44vtuk) / _mdv5dpxh);
				_iogulgv2 = (((_1m44vtuk * _nqy1hops) * _wam2klr5) - (_ddphzv25 * (_wam2klr5 * _kfg4qgsv)));
				if (*(__dlaln2._027ahmgj+(_gjnpgmw7 - 1)))
				{
					
					*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) = _ddphzv25;
					*(_ta7zuy9k+((int)2 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) = _iogulgv2;
				}
				else
				{
					
					*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) = _iogulgv2;
					*(_ta7zuy9k+((int)2 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) = _ddphzv25;
				}
				
				_ziu6urj2 = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_iogulgv2 ) ,ILNumerics.F2NET.Intrinsics.ABS(_ddphzv25 ) );//* 
				//*           Further scaling if  norm(A) norm(X) > overflow 
				//* 
				
				if ((_ziu6urj2 > _kxg5drh2) & (_xgnmjbtu > _kxg5drh2))
				{
					
					if (_ziu6urj2 > (_av7j8yda / _xgnmjbtu))
					{
						
						_1ajfmh55 = (_xgnmjbtu / _av7j8yda);
						*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) = (_1ajfmh55 * *(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)));
						*(_ta7zuy9k+((int)2 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) = (_1ajfmh55 * *(_ta7zuy9k+((int)2 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)));
						_ziu6urj2 = (_1ajfmh55 * _ziu6urj2);
						_1m44vtuk = (_1ajfmh55 * _1m44vtuk);
					}
					
				}
				
			}
			else
			{
				//* 
				//*           Complex 2x2 system  (w is complex) 
				//* 
				//*           Find the largest element in C 
				//* 
				
				*(__dlaln2._mp24tx63+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2)) = (-((_nc0qphpn * _5v2i3gjn)));
				*(__dlaln2._mp24tx63+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2)) = _d0547bi2;
				*(__dlaln2._mp24tx63+((int)1 - 1) + ((int)2 - 1) * 1 * ((int)2)) = _d0547bi2;
				*(__dlaln2._mp24tx63+((int)2 - 1) + ((int)2 - 1) * 1 * ((int)2)) = (-((_nc0qphpn * _vt4856ip)));
				_xgnmjbtu = _d0547bi2;
				_gjnpgmw7 = (int)0;//* 
				
				{
					System.Int32 __81fgg2dlsvn2316 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2316 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2316;
					for (__81fgg2count2316 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)4) - __81fgg2dlsvn2316 + __81fgg2step2316) / __81fgg2step2316)), _znpjgsef = __81fgg2dlsvn2316; __81fgg2count2316 != 0; __81fgg2count2316--, _znpjgsef += (__81fgg2step2316)) {

					{
						
						if ((ILNumerics.F2NET.Intrinsics.ABS(*(__dlaln2._wrlqzehw+(_znpjgsef - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(__dlaln2._ppxyjd5x+(_znpjgsef - 1)) )) > _xgnmjbtu)
						{
							
							_xgnmjbtu = (ILNumerics.F2NET.Intrinsics.ABS(*(__dlaln2._wrlqzehw+(_znpjgsef - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(__dlaln2._ppxyjd5x+(_znpjgsef - 1)) ));
							_gjnpgmw7 = _znpjgsef;
						}
						
Mark20:;
						// continue
					}
										}				}//* 
				//*           If norm(C) < SMINI, use SMINI*identity. 
				//* 
				
				if (_xgnmjbtu < _4nf5sqam)
				{
					
					_nbgcqppt = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_p9n405a5+((int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_p9n405a5+((int)1 - 1) + ((int)2 - 1) * 1 * (_ly9opahg)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_p9n405a5+((int)2 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_p9n405a5+((int)2 - 1) + ((int)2 - 1) * 1 * (_ly9opahg)) ) );
					if ((_4nf5sqam < _kxg5drh2) & (_nbgcqppt > _kxg5drh2))
					{
						
						if (_nbgcqppt > (_av7j8yda * _4nf5sqam))
						_1m44vtuk = (_kxg5drh2 / _nbgcqppt);
					}
					
					_1ajfmh55 = (_1m44vtuk / _4nf5sqam);
					*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) = (_1ajfmh55 * *(_p9n405a5+((int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)));
					*(_ta7zuy9k+((int)2 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) = (_1ajfmh55 * *(_p9n405a5+((int)2 - 1) + ((int)1 - 1) * 1 * (_ly9opahg)));
					*(_ta7zuy9k+((int)1 - 1) + ((int)2 - 1) * 1 * (_eeyyzhrs)) = (_1ajfmh55 * *(_p9n405a5+((int)1 - 1) + ((int)2 - 1) * 1 * (_ly9opahg)));
					*(_ta7zuy9k+((int)2 - 1) + ((int)2 - 1) * 1 * (_eeyyzhrs)) = (_1ajfmh55 * *(_p9n405a5+((int)2 - 1) + ((int)2 - 1) * 1 * (_ly9opahg)));
					_ziu6urj2 = (_1ajfmh55 * _nbgcqppt);
					_gro5yvfo = (int)1;
					return;
				}
				//* 
				//*           Gaussian elimination with complete pivoting. 
				//* 
				
				_m0aq8y3l = *(__dlaln2._wrlqzehw+(_gjnpgmw7 - 1));
				_9h1cs0bo = *(__dlaln2._ppxyjd5x+(_gjnpgmw7 - 1));
				_68w48tl9 = *(__dlaln2._wrlqzehw+(*(__dlaln2._t1hlqe2w+((int)2 - 1) + (_gjnpgmw7 - 1) * 1 * ((int)4)) - 1));
				_qwq5hojl = *(__dlaln2._ppxyjd5x+(*(__dlaln2._t1hlqe2w+((int)2 - 1) + (_gjnpgmw7 - 1) * 1 * ((int)4)) - 1));
				_kfg4qgsv = *(__dlaln2._wrlqzehw+(*(__dlaln2._t1hlqe2w+((int)3 - 1) + (_gjnpgmw7 - 1) * 1 * ((int)4)) - 1));
				_57h8wd6w = *(__dlaln2._ppxyjd5x+(*(__dlaln2._t1hlqe2w+((int)3 - 1) + (_gjnpgmw7 - 1) * 1 * ((int)4)) - 1));
				_gnsdttkm = *(__dlaln2._wrlqzehw+(*(__dlaln2._t1hlqe2w+((int)4 - 1) + (_gjnpgmw7 - 1) * 1 * ((int)4)) - 1));
				_id56iquy = *(__dlaln2._ppxyjd5x+(*(__dlaln2._t1hlqe2w+((int)4 - 1) + (_gjnpgmw7 - 1) * 1 * ((int)4)) - 1));
				if ((_gjnpgmw7 == (int)1) | (_gjnpgmw7 == (int)4))
				{
					//* 
					//*              Code when off-diagonals of pivoted C are real 
					//* 
					
					if (ILNumerics.F2NET.Intrinsics.ABS(_m0aq8y3l ) > ILNumerics.F2NET.Intrinsics.ABS(_9h1cs0bo ))
					{
						
						_1ajfmh55 = (_9h1cs0bo / _m0aq8y3l);
						_wam2klr5 = (_kxg5drh2 / (_m0aq8y3l * (_kxg5drh2 + __POW2(_1ajfmh55))));
						_5bslsar5 = (-((_1ajfmh55 * _wam2klr5)));
					}
					else
					{
						
						_1ajfmh55 = (_m0aq8y3l / _9h1cs0bo);
						_5bslsar5 = (-((_kxg5drh2 / (_9h1cs0bo * (_kxg5drh2 + __POW2(_1ajfmh55))))));
						_wam2klr5 = (-((_1ajfmh55 * _5bslsar5)));
					}
					
					_viaj40zd = (_68w48tl9 * _wam2klr5);
					_c6av82sq = (_68w48tl9 * _5bslsar5);
					_knnw036i = (_kfg4qgsv * _wam2klr5);
					_6yj46j1e = (_kfg4qgsv * _5bslsar5);
					_mdv5dpxh = (_gnsdttkm - (_kfg4qgsv * _viaj40zd));
					_76kx8wue = (_id56iquy - (_kfg4qgsv * _c6av82sq));
				}
				else
				{
					//* 
					//*              Code when diagonals of pivoted C are real 
					//* 
					
					_wam2klr5 = (_kxg5drh2 / _m0aq8y3l);
					_5bslsar5 = _d0547bi2;
					_viaj40zd = (_68w48tl9 * _wam2klr5);
					_c6av82sq = (_qwq5hojl * _wam2klr5);
					_knnw036i = (_kfg4qgsv * _wam2klr5);
					_6yj46j1e = (_57h8wd6w * _wam2klr5);
					_mdv5dpxh = ((_gnsdttkm - (_kfg4qgsv * _viaj40zd)) + (_57h8wd6w * _c6av82sq));
					_76kx8wue = ((-((_kfg4qgsv * _c6av82sq))) - (_57h8wd6w * _viaj40zd));
				}
				
				_md5izrkd = (ILNumerics.F2NET.Intrinsics.ABS(_mdv5dpxh ) + ILNumerics.F2NET.Intrinsics.ABS(_76kx8wue ));//* 
				//*           If smaller pivot < SMINI, use SMINI 
				//* 
				
				if (_md5izrkd < _4nf5sqam)
				{
					
					_mdv5dpxh = _4nf5sqam;
					_76kx8wue = _d0547bi2;
					_gro5yvfo = (int)1;
				}
				
				if (*(__dlaln2._r9at8wxl+(_gjnpgmw7 - 1)))
				{
					
					_2gjyh0fi = *(_p9n405a5+((int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg));
					_nqy1hops = *(_p9n405a5+((int)2 - 1) + ((int)1 - 1) * 1 * (_ly9opahg));
					_9leixcp0 = *(_p9n405a5+((int)1 - 1) + ((int)2 - 1) * 1 * (_ly9opahg));
					_dc012721 = *(_p9n405a5+((int)2 - 1) + ((int)2 - 1) * 1 * (_ly9opahg));
				}
				else
				{
					
					_nqy1hops = *(_p9n405a5+((int)1 - 1) + ((int)1 - 1) * 1 * (_ly9opahg));
					_2gjyh0fi = *(_p9n405a5+((int)2 - 1) + ((int)1 - 1) * 1 * (_ly9opahg));
					_dc012721 = *(_p9n405a5+((int)1 - 1) + ((int)2 - 1) * 1 * (_ly9opahg));
					_9leixcp0 = *(_p9n405a5+((int)2 - 1) + ((int)2 - 1) * 1 * (_ly9opahg));
				}
				
				_2gjyh0fi = ((_2gjyh0fi - (_viaj40zd * _nqy1hops)) + (_c6av82sq * _dc012721));
				_9leixcp0 = ((_9leixcp0 - (_c6av82sq * _nqy1hops)) - (_viaj40zd * _dc012721));
				_ox4i8otb = ILNumerics.F2NET.Intrinsics.MAX((ILNumerics.F2NET.Intrinsics.ABS(_nqy1hops ) + ILNumerics.F2NET.Intrinsics.ABS(_dc012721 )) * (_md5izrkd * (ILNumerics.F2NET.Intrinsics.ABS(_wam2klr5 ) + ILNumerics.F2NET.Intrinsics.ABS(_5bslsar5 ))) ,ILNumerics.F2NET.Intrinsics.ABS(_2gjyh0fi ) + ILNumerics.F2NET.Intrinsics.ABS(_9leixcp0 ) );
				if ((_ox4i8otb > _kxg5drh2) & (_md5izrkd < _kxg5drh2))
				{
					
					if (_ox4i8otb >= (_av7j8yda * _md5izrkd))
					{
						
						_1m44vtuk = (_kxg5drh2 / _ox4i8otb);
						_nqy1hops = (_1m44vtuk * _nqy1hops);
						_dc012721 = (_1m44vtuk * _dc012721);
						_2gjyh0fi = (_1m44vtuk * _2gjyh0fi);
						_9leixcp0 = (_1m44vtuk * _9leixcp0);
					}
					
				}
				//* 
				
				_x0fujx9g(ref _2gjyh0fi ,ref _9leixcp0 ,ref _mdv5dpxh ,ref _76kx8wue ,ref _ddphzv25 ,ref _pom2qzxg );
				_iogulgv2 = ((((_wam2klr5 * _nqy1hops) - (_5bslsar5 * _dc012721)) - (_knnw036i * _ddphzv25)) + (_6yj46j1e * _pom2qzxg));
				_47i5794w = ((((_5bslsar5 * _nqy1hops) + (_wam2klr5 * _dc012721)) - (_6yj46j1e * _ddphzv25)) - (_knnw036i * _pom2qzxg));
				if (*(__dlaln2._027ahmgj+(_gjnpgmw7 - 1)))
				{
					
					*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) = _ddphzv25;
					*(_ta7zuy9k+((int)2 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) = _iogulgv2;
					*(_ta7zuy9k+((int)1 - 1) + ((int)2 - 1) * 1 * (_eeyyzhrs)) = _pom2qzxg;
					*(_ta7zuy9k+((int)2 - 1) + ((int)2 - 1) * 1 * (_eeyyzhrs)) = _47i5794w;
				}
				else
				{
					
					*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) = _iogulgv2;
					*(_ta7zuy9k+((int)2 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) = _ddphzv25;
					*(_ta7zuy9k+((int)1 - 1) + ((int)2 - 1) * 1 * (_eeyyzhrs)) = _47i5794w;
					*(_ta7zuy9k+((int)2 - 1) + ((int)2 - 1) * 1 * (_eeyyzhrs)) = _pom2qzxg;
				}
				
				_ziu6urj2 = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_iogulgv2 ) + ILNumerics.F2NET.Intrinsics.ABS(_47i5794w ) ,ILNumerics.F2NET.Intrinsics.ABS(_ddphzv25 ) + ILNumerics.F2NET.Intrinsics.ABS(_pom2qzxg ) );//* 
				//*           Further scaling if  norm(A) norm(X) > overflow 
				//* 
				
				if ((_ziu6urj2 > _kxg5drh2) & (_xgnmjbtu > _kxg5drh2))
				{
					
					if (_ziu6urj2 > (_av7j8yda / _xgnmjbtu))
					{
						
						_1ajfmh55 = (_xgnmjbtu / _av7j8yda);
						*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) = (_1ajfmh55 * *(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)));
						*(_ta7zuy9k+((int)2 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)) = (_1ajfmh55 * *(_ta7zuy9k+((int)2 - 1) + ((int)1 - 1) * 1 * (_eeyyzhrs)));
						*(_ta7zuy9k+((int)1 - 1) + ((int)2 - 1) * 1 * (_eeyyzhrs)) = (_1ajfmh55 * *(_ta7zuy9k+((int)1 - 1) + ((int)2 - 1) * 1 * (_eeyyzhrs)));
						*(_ta7zuy9k+((int)2 - 1) + ((int)2 - 1) * 1 * (_eeyyzhrs)) = (_1ajfmh55 * *(_ta7zuy9k+((int)2 - 1) + ((int)2 - 1) * 1 * (_eeyyzhrs)));
						_ziu6urj2 = (_1ajfmh55 * _ziu6urj2);
						_1m44vtuk = (_1ajfmh55 * _1m44vtuk);
					}
					
				}
				
			}
			
		}
		//* 
		
		return;//* 
		//*     End of DLALN2 
		//* 
		
	}
	
	} // 177


internal unsafe class __dlaln2 { 
internal static MemoryHandle _r9at8wxlH_ =  ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<Boolean>((ulong)((int)4));
internal static Boolean* _r9at8wxl = (Boolean*)_r9at8wxlH_.Pointer;
internal static MemoryHandle _027ahmgjH_ =  ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<Boolean>((ulong)((int)4));
internal static Boolean* _027ahmgj = (Boolean*)_027ahmgjH_.Pointer;
internal static MemoryHandle _t1hlqe2wH_ =  ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<Int32>((ulong)((int)4)*((int)4));
internal static Int32* _t1hlqe2w = (Int32*)_t1hlqe2wH_.Pointer;
internal static MemoryHandle _ppxyjd5xH_ =  ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<Double>((ulong)((int)4));
internal static Double* _ppxyjd5x = (Double*)_ppxyjd5xH_.Pointer;
internal static Double* _mp24tx63 =  _ppxyjd5x;
internal static MemoryHandle _wrlqzehwH_ =  ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<Double>((ulong)((int)4));
internal static Double* _wrlqzehw = (Double*)_wrlqzehwH_.Pointer;
internal static Double* _14at9km9 =  _wrlqzehw;

}

} // end class 
} // end namespace
#endif
