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
//*> \brief \b DLARRJ performs refinement of the initial estimates of the eigenvalues of the matrix T. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLARRJ + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlarrj.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlarrj.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlarrj.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLARRJ( N, D, E2, IFIRST, ILAST, 
//*                          RTOL, OFFSET, W, WERR, WORK, IWORK, 
//*                          PIVMIN, SPDIAM, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            IFIRST, ILAST, INFO, N, OFFSET 
//*       DOUBLE PRECISION   PIVMIN, RTOL, SPDIAM 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IWORK( * ) 
//*       DOUBLE PRECISION   D( * ), E2( * ), W( * ), 
//*      $                   WERR( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> Given the initial eigenvalue approximations of T, DLARRJ 
//*> does  bisection to refine the eigenvalues of T, 
//*> W( IFIRST-OFFSET ) through W( ILAST-OFFSET ), to more accuracy. Initial 
//*> guesses for these eigenvalues are input in W, the corresponding estimate 
//*> of the error in these guesses in WERR. During bisection, intervals 
//*> [left, right] are maintained by storing their mid-points and 
//*> semi-widths in the arrays W and WERR respectively. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] D 
//*> \verbatim 
//*>          D is DOUBLE PRECISION array, dimension (N) 
//*>          The N diagonal elements of T. 
//*> \endverbatim 
//*> 
//*> \param[in] E2 
//*> \verbatim 
//*>          E2 is DOUBLE PRECISION array, dimension (N-1) 
//*>          The Squares of the (N-1) subdiagonal elements of T. 
//*> \endverbatim 
//*> 
//*> \param[in] IFIRST 
//*> \verbatim 
//*>          IFIRST is INTEGER 
//*>          The index of the first eigenvalue to be computed. 
//*> \endverbatim 
//*> 
//*> \param[in] ILAST 
//*> \verbatim 
//*>          ILAST is INTEGER 
//*>          The index of the last eigenvalue to be computed. 
//*> \endverbatim 
//*> 
//*> \param[in] RTOL 
//*> \verbatim 
//*>          RTOL is DOUBLE PRECISION 
//*>          Tolerance for the convergence of the bisection intervals. 
//*>          An interval [LEFT,RIGHT] has converged if 
//*>          RIGHT-LEFT < RTOL*MAX(|LEFT|,|RIGHT|). 
//*> \endverbatim 
//*> 
//*> \param[in] OFFSET 
//*> \verbatim 
//*>          OFFSET is INTEGER 
//*>          Offset for the arrays W and WERR, i.e., the IFIRST-OFFSET 
//*>          through ILAST-OFFSET elements of these arrays are to be used. 
//*> \endverbatim 
//*> 
//*> \param[in,out] W 
//*> \verbatim 
//*>          W is DOUBLE PRECISION array, dimension (N) 
//*>          On input, W( IFIRST-OFFSET ) through W( ILAST-OFFSET ) are 
//*>          estimates of the eigenvalues of L D L^T indexed IFIRST through 
//*>          ILAST. 
//*>          On output, these estimates are refined. 
//*> \endverbatim 
//*> 
//*> \param[in,out] WERR 
//*> \verbatim 
//*>          WERR is DOUBLE PRECISION array, dimension (N) 
//*>          On input, WERR( IFIRST-OFFSET ) through WERR( ILAST-OFFSET ) are 
//*>          the errors in the estimates of the corresponding elements in W. 
//*>          On output, these errors are refined. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is DOUBLE PRECISION array, dimension (2*N) 
//*>          Workspace. 
//*> \endverbatim 
//*> 
//*> \param[out] IWORK 
//*> \verbatim 
//*>          IWORK is INTEGER array, dimension (2*N) 
//*>          Workspace. 
//*> \endverbatim 
//*> 
//*> \param[in] PIVMIN 
//*> \verbatim 
//*>          PIVMIN is DOUBLE PRECISION 
//*>          The minimum pivot in the Sturm sequence for T. 
//*> \endverbatim 
//*> 
//*> \param[in] SPDIAM 
//*> \verbatim 
//*>          SPDIAM is DOUBLE PRECISION 
//*>          The spectral diameter of T. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          Error flag. 
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
//*> \date June 2017 
//* 
//*> \ingroup OTHERauxiliary 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*> Beresford Parlett, University of California, Berkeley, USA \n 
//*> Jim Demmel, University of California, Berkeley, USA \n 
//*> Inderjit Dhillon, University of Texas, Austin, USA \n 
//*> Osni Marques, LBNL/NERSC, USA \n 
//*> Christof Voemel, University of California, Berkeley, USA 
//* 
//*  ===================================================================== 

	 
	public static void _qhnidv8c(ref Int32 _dxpq0xkr, Double* _plfm7z8g, Double* _0maek8rz, ref Int32 _y7ic5zdn, ref Int32 _qitusv03, ref Double _9mzm56bw, ref Int32 _1l9k9q9k, Double* _z1ioc3c8, Double* _sgbqptwj, Double* _apig8meb, Int32* _4b6rt45i, ref Double _3aphllyg, ref Double _1v6i5d3q, ref Int32 _gro5yvfo)
	{
#region variable declarations
Int32 _lmysm07u =  default;
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Double _5m0mjfxm =  2d;
Double _gbf4169i =  0.5d;
Int32 _to3hwp46 =  default;
Int32 _mwbax5ov =  default;
Int32 _b5p6od9s =  default;
Int32 _egqdmelt =  default;
Int32 _8ur10vsh =  default;
Int32 _retbwjxi =  default;
Int32 _em7fbywm =  default;
Int32 _znpjgsef =  default;
Int32 _umlkckdg =  default;
Int32 _1ujdqhex =  default;
Int32 _in99hgs3 =  default;
Int32 _ejwydfmr =  default;
Int32 _shqj7mfx =  default;
Int32 _9hf67rg4 =  default;
Double _f8wr73cc =  default;
Double _g0jdw6pt =  default;
Double _pvwxvshr =  default;
Double _kdbjkqmm =  default;
Double _ruhusobv =  default;
Double _irk8i6qr =  default;
Double _2qcyvkhx =  default;
Double _21zlwdd8 =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.1) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     June 2017 
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
		//* 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		_gro5yvfo = (int)0;//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr <= (int)0)
		{
			
			return;
		}
		//* 
		
		_to3hwp46 = (ILNumerics.F2NET.Intrinsics.INT((ILNumerics.F2NET.Intrinsics.LOG(_1v6i5d3q + _3aphllyg ) - ILNumerics.F2NET.Intrinsics.LOG(_3aphllyg )) / ILNumerics.F2NET.Intrinsics.LOG(_5m0mjfxm ) ) + (int)2);//* 
		//*     Initialize unconverged intervals in [ WORK(2*I-1), WORK(2*I) ]. 
		//*     The Sturm Count, Count( WORK(2*I-1) ) is arranged to be I-1, while 
		//*     Count( WORK(2*I) ) is stored in IWORK( 2*I ). The integer IWORK( 2*I-1 ) 
		//*     for an unconverged interval is set to the index of the next unconverged 
		//*     interval, and is -1 or 0 for a converged interval. Thus a linked 
		//*     list of unconverged intervals is set up. 
		//* 
		// 
		
		_egqdmelt = _y7ic5zdn;
		_8ur10vsh = _qitusv03;//*     The number of unconverged intervals 
		
		_lmysm07u = (int)0;//*     The last unconverged interval found 
		
		_shqj7mfx = (int)0;
		{
			System.Int32 __81fgg2dlsvn3040 = (System.Int32)(_egqdmelt);
			const System.Int32 __81fgg2step3040 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3040;
			for (__81fgg2count3040 = System.Math.Max(0, (System.Int32)(((System.Int32)(_8ur10vsh) - __81fgg2dlsvn3040 + __81fgg2step3040) / __81fgg2step3040)), _b5p6od9s = __81fgg2dlsvn3040; __81fgg2count3040 != 0; __81fgg2count3040--, _b5p6od9s += (__81fgg2step3040)) {

			{
				
				_umlkckdg = ((int)2 * _b5p6od9s);
				_retbwjxi = (_b5p6od9s - _1l9k9q9k);
				_pvwxvshr = (*(_z1ioc3c8+(_retbwjxi - 1)) - *(_sgbqptwj+(_retbwjxi - 1)));
				_kdbjkqmm = *(_z1ioc3c8+(_retbwjxi - 1));
				_ruhusobv = (*(_z1ioc3c8+(_retbwjxi - 1)) + *(_sgbqptwj+(_retbwjxi - 1)));
				_21zlwdd8 = (_ruhusobv - _kdbjkqmm);
				_2qcyvkhx = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_pvwxvshr ) ,ILNumerics.F2NET.Intrinsics.ABS(_ruhusobv ) );// 
				//*        The following test prevents the test of converged intervals 
				
				if (_21zlwdd8 < (_9mzm56bw * _2qcyvkhx))
				{
					//*           This interval has already converged and does not need refinement. 
					//*           (Note that the gaps might change through refining the 
					//*            eigenvalues, however, they can only get bigger.) 
					//*           Remove it from the list. 
					
					*(_4b6rt45i+(_umlkckdg - (int)1 - 1)) = (int)-1;//*           Make sure that I1 always points to the first unconverged interval 
					
					if ((_b5p6od9s == _egqdmelt) & (_b5p6od9s < _8ur10vsh))
					_egqdmelt = (_b5p6od9s + (int)1);
					if ((_shqj7mfx >= _egqdmelt) & (_b5p6od9s <= _8ur10vsh))
					*(_4b6rt45i+(((int)2 * _shqj7mfx) - (int)1 - 1)) = (_b5p6od9s + (int)1);
				}
				else
				{
					//*           unconverged interval found 
					
					_shqj7mfx = _b5p6od9s;//*           Make sure that [LEFT,RIGHT] contains the desired eigenvalue 
					//* 
					//*           Do while( CNT(LEFT).GT.I-1 ) 
					//* 
					
					_g0jdw6pt = _kxg5drh2;
Mark20:;
					// continue
					_mwbax5ov = (int)0;
					_irk8i6qr = _pvwxvshr;
					_f8wr73cc = (*(_plfm7z8g+((int)1 - 1)) - _irk8i6qr);
					if (_f8wr73cc < _d0547bi2)
					_mwbax5ov = (_mwbax5ov + (int)1);
					{
						System.Int32 __81fgg2dlsvn3041 = (System.Int32)((int)2);
						const System.Int32 __81fgg2step3041 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3041;
						for (__81fgg2count3041 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3041 + __81fgg2step3041) / __81fgg2step3041)), _znpjgsef = __81fgg2dlsvn3041; __81fgg2count3041 != 0; __81fgg2count3041--, _znpjgsef += (__81fgg2step3041)) {

						{
							
							_f8wr73cc = ((*(_plfm7z8g+(_znpjgsef - 1)) - _irk8i6qr) - (*(_0maek8rz+(_znpjgsef - (int)1 - 1)) / _f8wr73cc));
							if (_f8wr73cc < _d0547bi2)
							_mwbax5ov = (_mwbax5ov + (int)1);
Mark30:;
							// continue
						}
												}					}
					if (_mwbax5ov > (_b5p6od9s - (int)1))
					{
						
						_pvwxvshr = (_pvwxvshr - (*(_sgbqptwj+(_retbwjxi - 1)) * _g0jdw6pt));
						_g0jdw6pt = (_5m0mjfxm * _g0jdw6pt);goto Mark20;
					}
					//* 
					//*           Do while( CNT(RIGHT).LT.I ) 
					//* 
					
					_g0jdw6pt = _kxg5drh2;
Mark50:;
					// continue
					_mwbax5ov = (int)0;
					_irk8i6qr = _ruhusobv;
					_f8wr73cc = (*(_plfm7z8g+((int)1 - 1)) - _irk8i6qr);
					if (_f8wr73cc < _d0547bi2)
					_mwbax5ov = (_mwbax5ov + (int)1);
					{
						System.Int32 __81fgg2dlsvn3042 = (System.Int32)((int)2);
						const System.Int32 __81fgg2step3042 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3042;
						for (__81fgg2count3042 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3042 + __81fgg2step3042) / __81fgg2step3042)), _znpjgsef = __81fgg2dlsvn3042; __81fgg2count3042 != 0; __81fgg2count3042--, _znpjgsef += (__81fgg2step3042)) {

						{
							
							_f8wr73cc = ((*(_plfm7z8g+(_znpjgsef - 1)) - _irk8i6qr) - (*(_0maek8rz+(_znpjgsef - (int)1 - 1)) / _f8wr73cc));
							if (_f8wr73cc < _d0547bi2)
							_mwbax5ov = (_mwbax5ov + (int)1);
Mark60:;
							// continue
						}
												}					}
					if (_mwbax5ov < _b5p6od9s)
					{
						
						_ruhusobv = (_ruhusobv + (*(_sgbqptwj+(_retbwjxi - 1)) * _g0jdw6pt));
						_g0jdw6pt = (_5m0mjfxm * _g0jdw6pt);goto Mark50;
					}
					
					_lmysm07u = (_lmysm07u + (int)1);
					*(_4b6rt45i+(_umlkckdg - (int)1 - 1)) = (_b5p6od9s + (int)1);
					*(_4b6rt45i+(_umlkckdg - 1)) = _mwbax5ov;
				}
				
				*(_apig8meb+(_umlkckdg - (int)1 - 1)) = _pvwxvshr;
				*(_apig8meb+(_umlkckdg - 1)) = _ruhusobv;
Mark75:;
				// continue
			}
						}		}// 
		// 
		
		_9hf67rg4 = _egqdmelt;//* 
		//*     Do while( NINT.GT.0 ), i.e. there are still unconverged intervals 
		//*     and while (ITER.LT.MAXITR) 
		//* 
		
		_em7fbywm = (int)0;
Mark80:;
		// continue
		_shqj7mfx = (_egqdmelt - (int)1);
		_b5p6od9s = _egqdmelt;
		_in99hgs3 = _lmysm07u;// 
		
		{
			System.Int32 __81fgg2dlsvn3043 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step3043 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3043;
			for (__81fgg2count3043 = System.Math.Max(0, (System.Int32)(((System.Int32)(_in99hgs3) - __81fgg2dlsvn3043 + __81fgg2step3043) / __81fgg2step3043)), _ejwydfmr = __81fgg2dlsvn3043; __81fgg2count3043 != 0; __81fgg2count3043--, _ejwydfmr += (__81fgg2step3043)) {

			{
				
				_umlkckdg = ((int)2 * _b5p6od9s);
				_retbwjxi = (_b5p6od9s - _1l9k9q9k);
				_1ujdqhex = *(_4b6rt45i+(_umlkckdg - (int)1 - 1));
				_pvwxvshr = *(_apig8meb+(_umlkckdg - (int)1 - 1));
				_ruhusobv = *(_apig8meb+(_umlkckdg - 1));
				_kdbjkqmm = (_gbf4169i * (_pvwxvshr + _ruhusobv));// 
				//*        semiwidth of interval 
				
				_21zlwdd8 = (_ruhusobv - _kdbjkqmm);
				_2qcyvkhx = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_pvwxvshr ) ,ILNumerics.F2NET.Intrinsics.ABS(_ruhusobv ) );// 
				
				if ((_21zlwdd8 < (_9mzm56bw * _2qcyvkhx)) | (_em7fbywm == _to3hwp46))
				{
					//*           reduce number of unconverged intervals 
					
					_lmysm07u = (_lmysm07u - (int)1);//*           Mark interval as converged. 
					
					*(_4b6rt45i+(_umlkckdg - (int)1 - 1)) = (int)0;
					if (_egqdmelt == _b5p6od9s)
					{
						
						_egqdmelt = _1ujdqhex;
					}
					else
					{
						//*              Prev holds the last unconverged interval previously examined 
						
						if (_shqj7mfx >= _egqdmelt)
						*(_4b6rt45i+(((int)2 * _shqj7mfx) - (int)1 - 1)) = _1ujdqhex;
					}
					
					_b5p6od9s = _1ujdqhex;goto Mark100;
				}
				
				_shqj7mfx = _b5p6od9s;//* 
				//*        Perform one bisection step 
				//* 
				
				_mwbax5ov = (int)0;
				_irk8i6qr = _kdbjkqmm;
				_f8wr73cc = (*(_plfm7z8g+((int)1 - 1)) - _irk8i6qr);
				if (_f8wr73cc < _d0547bi2)
				_mwbax5ov = (_mwbax5ov + (int)1);
				{
					System.Int32 __81fgg2dlsvn3044 = (System.Int32)((int)2);
					const System.Int32 __81fgg2step3044 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3044;
					for (__81fgg2count3044 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3044 + __81fgg2step3044) / __81fgg2step3044)), _znpjgsef = __81fgg2dlsvn3044; __81fgg2count3044 != 0; __81fgg2count3044--, _znpjgsef += (__81fgg2step3044)) {

					{
						
						_f8wr73cc = ((*(_plfm7z8g+(_znpjgsef - 1)) - _irk8i6qr) - (*(_0maek8rz+(_znpjgsef - (int)1 - 1)) / _f8wr73cc));
						if (_f8wr73cc < _d0547bi2)
						_mwbax5ov = (_mwbax5ov + (int)1);
Mark90:;
						// continue
					}
										}				}
				if (_mwbax5ov <= (_b5p6od9s - (int)1))
				{
					
					*(_apig8meb+(_umlkckdg - (int)1 - 1)) = _kdbjkqmm;
				}
				else
				{
					
					*(_apig8meb+(_umlkckdg - 1)) = _kdbjkqmm;
				}
				
				_b5p6od9s = _1ujdqhex;// 
				
Mark100:;
				// continue
			}
						}		}
		_em7fbywm = (_em7fbywm + (int)1);//*     do another loop if there are still unconverged intervals 
		//*     However, in the last iteration, all intervals are accepted 
		//*     since this is the best we can do. 
		
		if ((_lmysm07u > (int)0) & (_em7fbywm <= _to3hwp46))goto Mark80;//* 
		//* 
		//*     At this point, all the intervals have converged 
		
		{
			System.Int32 __81fgg2dlsvn3045 = (System.Int32)(_9hf67rg4);
			const System.Int32 __81fgg2step3045 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3045;
			for (__81fgg2count3045 = System.Math.Max(0, (System.Int32)(((System.Int32)(_qitusv03) - __81fgg2dlsvn3045 + __81fgg2step3045) / __81fgg2step3045)), _b5p6od9s = __81fgg2dlsvn3045; __81fgg2count3045 != 0; __81fgg2count3045--, _b5p6od9s += (__81fgg2step3045)) {

			{
				
				_umlkckdg = ((int)2 * _b5p6od9s);
				_retbwjxi = (_b5p6od9s - _1l9k9q9k);//*        All intervals marked by '0' have been refined. 
				
				if (*(_4b6rt45i+(_umlkckdg - (int)1 - 1)) == (int)0)
				{
					
					*(_z1ioc3c8+(_retbwjxi - 1)) = (_gbf4169i * (*(_apig8meb+(_umlkckdg - (int)1 - 1)) + *(_apig8meb+(_umlkckdg - 1))));
					*(_sgbqptwj+(_retbwjxi - 1)) = (*(_apig8meb+(_umlkckdg - 1)) - *(_z1ioc3c8+(_retbwjxi - 1)));
				}
				
Mark110:;
				// continue
			}
						}		}//* 
		// 
		
		return;//* 
		//*     End of DLARRJ 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
