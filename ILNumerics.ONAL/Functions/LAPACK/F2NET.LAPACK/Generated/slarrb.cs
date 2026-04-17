
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
//*> \brief \b SLARRB provides limited bisection to locate eigenvalues for more accuracy. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLARRB + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slarrb.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slarrb.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slarrb.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLARRB( N, D, LLD, IFIRST, ILAST, RTOL1, 
//*                          RTOL2, OFFSET, W, WGAP, WERR, WORK, IWORK, 
//*                          PIVMIN, SPDIAM, TWIST, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            IFIRST, ILAST, INFO, N, OFFSET, TWIST 
//*       REAL               PIVMIN, RTOL1, RTOL2, SPDIAM 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IWORK( * ) 
//*       REAL               D( * ), LLD( * ), W( * ), 
//*      $                   WERR( * ), WGAP( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> Given the relatively robust representation(RRR) L D L^T, SLARRB 
//*> does "limited" bisection to refine the eigenvalues of L D L^T, 
//*> W( IFIRST-OFFSET ) through W( ILAST-OFFSET ), to more accuracy. Initial 
//*> guesses for these eigenvalues are input in W, the corresponding estimate 
//*> of the error in these guesses and their gaps are input in WERR 
//*> and WGAP, respectively. During bisection, intervals 
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
//*>          D is REAL array, dimension (N) 
//*>          The N diagonal elements of the diagonal matrix D. 
//*> \endverbatim 
//*> 
//*> \param[in] LLD 
//*> \verbatim 
//*>          LLD is REAL array, dimension (N-1) 
//*>          The (N-1) elements L(i)*L(i)*D(i). 
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
//*> \param[in] RTOL1 
//*> \verbatim 
//*>          RTOL1 is REAL 
//*> \endverbatim 
//*> 
//*> \param[in] RTOL2 
//*> \verbatim 
//*>          RTOL2 is REAL 
//*>          Tolerance for the convergence of the bisection intervals. 
//*>          An interval [LEFT,RIGHT] has converged if 
//*>          RIGHT-LEFT < MAX( RTOL1*GAP, RTOL2*MAX(|LEFT|,|RIGHT|) ) 
//*>          where GAP is the (estimated) distance to the nearest 
//*>          eigenvalue. 
//*> \endverbatim 
//*> 
//*> \param[in] OFFSET 
//*> \verbatim 
//*>          OFFSET is INTEGER 
//*>          Offset for the arrays W, WGAP and WERR, i.e., the IFIRST-OFFSET 
//*>          through ILAST-OFFSET elements of these arrays are to be used. 
//*> \endverbatim 
//*> 
//*> \param[in,out] W 
//*> \verbatim 
//*>          W is REAL array, dimension (N) 
//*>          On input, W( IFIRST-OFFSET ) through W( ILAST-OFFSET ) are 
//*>          estimates of the eigenvalues of L D L^T indexed IFIRST through 
//*>          ILAST. 
//*>          On output, these estimates are refined. 
//*> \endverbatim 
//*> 
//*> \param[in,out] WGAP 
//*> \verbatim 
//*>          WGAP is REAL array, dimension (N-1) 
//*>          On input, the (estimated) gaps between consecutive 
//*>          eigenvalues of L D L^T, i.e., WGAP(I-OFFSET) is the gap between 
//*>          eigenvalues I and I+1. Note that if IFIRST = ILAST 
//*>          then WGAP(IFIRST-OFFSET) must be set to ZERO. 
//*>          On output, these gaps are refined. 
//*> \endverbatim 
//*> 
//*> \param[in,out] WERR 
//*> \verbatim 
//*>          WERR is REAL array, dimension (N) 
//*>          On input, WERR( IFIRST-OFFSET ) through WERR( ILAST-OFFSET ) are 
//*>          the errors in the estimates of the corresponding elements in W. 
//*>          On output, these errors are refined. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is REAL array, dimension (2*N) 
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
//*>          PIVMIN is REAL 
//*>          The minimum pivot in the Sturm sequence. 
//*> \endverbatim 
//*> 
//*> \param[in] SPDIAM 
//*> \verbatim 
//*>          SPDIAM is REAL 
//*>          The spectral diameter of the matrix. 
//*> \endverbatim 
//*> 
//*> \param[in] TWIST 
//*> \verbatim 
//*>          TWIST is INTEGER 
//*>          The twist index for the twisted factorization that is used 
//*>          for the negcount. 
//*>          TWIST = N: Compute negcount from L D L^T - LAMBDA I = L+ D+ L+^T 
//*>          TWIST = 1: Compute negcount from L D L^T - LAMBDA I = U- D- U-^T 
//*>          TWIST = R: Compute negcount from L D L^T - LAMBDA I = N(r) D(r) N(r) 
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

	 
	public static void _2vkkjyq0(ref Int32 _dxpq0xkr, Single* _plfm7z8g, Single* _4sixt94s, ref Int32 _y7ic5zdn, ref Int32 _qitusv03, ref Single _ndrkejw5, ref Single _nmnmq6ye, ref Int32 _1l9k9q9k, Single* _z1ioc3c8, Single* _fwsre11k, Single* _sgbqptwj, Single* _apig8meb, Int32* _4b6rt45i, ref Single _3aphllyg, ref Single _1v6i5d3q, ref Int32 _2ojprtnp, ref Int32 _gro5yvfo)
	{
#region variable declarations
Int32 _lmysm07u =  default;
Single _d0547bi2 =  0f;
Single _5m0mjfxm =  2f;
Single _gbf4169i =  0.5f;
Int32 _to3hwp46 =  default;
Int32 _b5p6od9s =  default;
Int32 _egqdmelt =  default;
Int32 _retbwjxi =  default;
Int32 _8t9w2q8d =  default;
Int32 _em7fbywm =  default;
Int32 _umlkckdg =  default;
Int32 _bfbhh57k =  default;
Int32 _1ujdqhex =  default;
Int32 _in99hgs3 =  default;
Int32 _shqj7mfx =  default;
Int32 _q2vwp05i =  default;
Single _ownqot6n =  default;
Single _iycyld94 =  default;
Single _xhzaoxno =  default;
Single _pvwxvshr =  default;
Single _r0uu2bo8 =  default;
Single _kdbjkqmm =  default;
Single _319ia531 =  default;
Single _j9a36i1z =  default;
Single _ruhusobv =  default;
Single _2qcyvkhx =  default;
Single _21zlwdd8 =  default;
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
		//*     .. 
		//*     .. External Functions .. 
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
		
		_to3hwp46 = (ILNumerics.F2NET.Intrinsics.INT((ILNumerics.F2NET.Intrinsics.LOG(_1v6i5d3q + _3aphllyg ) - ILNumerics.F2NET.Intrinsics.LOG(_3aphllyg )) / ILNumerics.F2NET.Intrinsics.LOG(_5m0mjfxm ) ) + (int)2);
		_319ia531 = (_5m0mjfxm * _3aphllyg);//* 
		
		_q2vwp05i = _2ojprtnp;
		if ((_q2vwp05i < (int)1) | (_q2vwp05i > _dxpq0xkr))
		_q2vwp05i = _dxpq0xkr;//* 
		//*     Initialize unconverged intervals in [ WORK(2*I-1), WORK(2*I) ]. 
		//*     The Sturm Count, Count( WORK(2*I-1) ) is arranged to be I-1, while 
		//*     Count( WORK(2*I) ) is stored in IWORK( 2*I ). The integer IWORK( 2*I-1 ) 
		//*     for an unconverged interval is set to the index of the next unconverged 
		//*     interval, and is -1 or 0 for a converged interval. Thus a linked 
		//*     list of unconverged intervals is set up. 
		//* 
		
		_egqdmelt = _y7ic5zdn;//*     The number of unconverged intervals 
		
		_lmysm07u = (int)0;//*     The last unconverged interval found 
		
		_shqj7mfx = (int)0;// 
		
		_j9a36i1z = *(_fwsre11k+(_egqdmelt - _1l9k9q9k - 1));
		{
			System.Int32 __81fgg2dlsvn3359 = (System.Int32)(_egqdmelt);
			const System.Int32 __81fgg2step3359 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3359;
			for (__81fgg2count3359 = System.Math.Max(0, (System.Int32)(((System.Int32)(_qitusv03) - __81fgg2dlsvn3359 + __81fgg2step3359) / __81fgg2step3359)), _b5p6od9s = __81fgg2dlsvn3359; __81fgg2count3359 != 0; __81fgg2count3359--, _b5p6od9s += (__81fgg2step3359)) {

			{
				
				_umlkckdg = ((int)2 * _b5p6od9s);
				_retbwjxi = (_b5p6od9s - _1l9k9q9k);
				_pvwxvshr = (*(_z1ioc3c8+(_retbwjxi - 1)) - *(_sgbqptwj+(_retbwjxi - 1)));
				_ruhusobv = (*(_z1ioc3c8+(_retbwjxi - 1)) + *(_sgbqptwj+(_retbwjxi - 1)));
				_r0uu2bo8 = _j9a36i1z;
				_j9a36i1z = *(_fwsre11k+(_retbwjxi - 1));
				_xhzaoxno = ILNumerics.F2NET.Intrinsics.MIN(_r0uu2bo8 ,_j9a36i1z );// 
				//*        Make sure that [LEFT,RIGHT] contains the desired eigenvalue 
				//*        Compute negcount from dstqds facto L+D+L+^T = L D L^T - LEFT 
				//* 
				//*        Do while( NEGCNT(LEFT).GT.I-1 ) 
				//* 
				
				_ownqot6n = *(_sgbqptwj+(_retbwjxi - 1));
Mark20:;
				// continue
				_bfbhh57k = _escttm7a(ref _dxpq0xkr ,_plfm7z8g ,_4sixt94s ,ref _pvwxvshr ,ref _3aphllyg ,ref _q2vwp05i );
				if (_bfbhh57k > (_b5p6od9s - (int)1))
				{
					
					_pvwxvshr = (_pvwxvshr - _ownqot6n);
					_ownqot6n = (_5m0mjfxm * _ownqot6n);goto Mark20;
				}
				//* 
				//*        Do while( NEGCNT(RIGHT).LT.I ) 
				//*        Compute negcount from dstqds facto L+D+L+^T = L D L^T - RIGHT 
				//* 
				
				_ownqot6n = *(_sgbqptwj+(_retbwjxi - 1));
Mark50:;
				// continue// 
				
				_bfbhh57k = _escttm7a(ref _dxpq0xkr ,_plfm7z8g ,_4sixt94s ,ref _ruhusobv ,ref _3aphllyg ,ref _q2vwp05i );
				if (_bfbhh57k < _b5p6od9s)
				{
					
					_ruhusobv = (_ruhusobv + _ownqot6n);
					_ownqot6n = (_5m0mjfxm * _ownqot6n);goto Mark50;
				}
				
				_21zlwdd8 = (_gbf4169i * ILNumerics.F2NET.Intrinsics.ABS(_pvwxvshr - _ruhusobv ));
				_2qcyvkhx = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_pvwxvshr ) ,ILNumerics.F2NET.Intrinsics.ABS(_ruhusobv ) );
				_iycyld94 = ILNumerics.F2NET.Intrinsics.MAX(_ndrkejw5 * _xhzaoxno ,_nmnmq6ye * _2qcyvkhx );
				if ((_21zlwdd8 <= _iycyld94) | (_21zlwdd8 <= _319ia531))
				{
					//*           This interval has already converged and does not need refinement. 
					//*           (Note that the gaps might change through refining the 
					//*            eigenvalues, however, they can only get bigger.) 
					//*           Remove it from the list. 
					
					*(_4b6rt45i+(_umlkckdg - (int)1 - 1)) = (int)-1;//*           Make sure that I1 always points to the first unconverged interval 
					
					if ((_b5p6od9s == _egqdmelt) & (_b5p6od9s < _qitusv03))
					_egqdmelt = (_b5p6od9s + (int)1);
					if ((_shqj7mfx >= _egqdmelt) & (_b5p6od9s <= _qitusv03))
					*(_4b6rt45i+(((int)2 * _shqj7mfx) - (int)1 - 1)) = (_b5p6od9s + (int)1);
				}
				else
				{
					//*           unconverged interval found 
					
					_shqj7mfx = _b5p6od9s;
					_lmysm07u = (_lmysm07u + (int)1);
					*(_4b6rt45i+(_umlkckdg - (int)1 - 1)) = (_b5p6od9s + (int)1);
					*(_4b6rt45i+(_umlkckdg - 1)) = _bfbhh57k;
				}
				
				*(_apig8meb+(_umlkckdg - (int)1 - 1)) = _pvwxvshr;
				*(_apig8meb+(_umlkckdg - 1)) = _ruhusobv;
Mark75:;
				// continue
			}
						}		}// 
		//* 
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
			System.Int32 __81fgg2dlsvn3360 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step3360 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3360;
			for (__81fgg2count3360 = System.Math.Max(0, (System.Int32)(((System.Int32)(_in99hgs3) - __81fgg2dlsvn3360 + __81fgg2step3360) / __81fgg2step3360)), _8t9w2q8d = __81fgg2dlsvn3360; __81fgg2count3360 != 0; __81fgg2count3360--, _8t9w2q8d += (__81fgg2step3360)) {

			{
				
				_umlkckdg = ((int)2 * _b5p6od9s);
				_retbwjxi = (_b5p6od9s - _1l9k9q9k);
				_j9a36i1z = *(_fwsre11k+(_retbwjxi - 1));
				_r0uu2bo8 = _j9a36i1z;
				if (_retbwjxi > (int)1)
				_r0uu2bo8 = *(_fwsre11k+(_retbwjxi - (int)1 - 1));
				_xhzaoxno = ILNumerics.F2NET.Intrinsics.MIN(_r0uu2bo8 ,_j9a36i1z );
				_1ujdqhex = *(_4b6rt45i+(_umlkckdg - (int)1 - 1));
				_pvwxvshr = *(_apig8meb+(_umlkckdg - (int)1 - 1));
				_ruhusobv = *(_apig8meb+(_umlkckdg - 1));
				_kdbjkqmm = (_gbf4169i * (_pvwxvshr + _ruhusobv));// 
				//*        semiwidth of interval 
				
				_21zlwdd8 = (_ruhusobv - _kdbjkqmm);
				_2qcyvkhx = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_pvwxvshr ) ,ILNumerics.F2NET.Intrinsics.ABS(_ruhusobv ) );
				_iycyld94 = ILNumerics.F2NET.Intrinsics.MAX(_ndrkejw5 * _xhzaoxno ,_nmnmq6ye * _2qcyvkhx );
				if (((_21zlwdd8 <= _iycyld94) | (_21zlwdd8 <= _319ia531)) | (_em7fbywm == _to3hwp46))
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
				
				_bfbhh57k = _escttm7a(ref _dxpq0xkr ,_plfm7z8g ,_4sixt94s ,ref _kdbjkqmm ,ref _3aphllyg ,ref _q2vwp05i );
				if (_bfbhh57k <= (_b5p6od9s - (int)1))
				{
					
					*(_apig8meb+(_umlkckdg - (int)1 - 1)) = _kdbjkqmm;
				}
				else
				{
					
					*(_apig8meb+(_umlkckdg - 1)) = _kdbjkqmm;
				}
				
				_b5p6od9s = _1ujdqhex;
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
			System.Int32 __81fgg2dlsvn3361 = (System.Int32)(_y7ic5zdn);
			const System.Int32 __81fgg2step3361 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3361;
			for (__81fgg2count3361 = System.Math.Max(0, (System.Int32)(((System.Int32)(_qitusv03) - __81fgg2dlsvn3361 + __81fgg2step3361) / __81fgg2step3361)), _b5p6od9s = __81fgg2dlsvn3361; __81fgg2count3361 != 0; __81fgg2count3361--, _b5p6od9s += (__81fgg2step3361)) {

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
		
		{
			System.Int32 __81fgg2dlsvn3362 = (System.Int32)((_y7ic5zdn + (int)1));
			const System.Int32 __81fgg2step3362 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3362;
			for (__81fgg2count3362 = System.Math.Max(0, (System.Int32)(((System.Int32)(_qitusv03) - __81fgg2dlsvn3362 + __81fgg2step3362) / __81fgg2step3362)), _b5p6od9s = __81fgg2dlsvn3362; __81fgg2count3362 != 0; __81fgg2count3362--, _b5p6od9s += (__81fgg2step3362)) {

			{
				
				_umlkckdg = ((int)2 * _b5p6od9s);
				_retbwjxi = (_b5p6od9s - _1l9k9q9k);
				*(_fwsre11k+(_retbwjxi - (int)1 - 1)) = ILNumerics.F2NET.Intrinsics.MAX(_d0547bi2 ,((*(_z1ioc3c8+(_retbwjxi - 1)) - *(_sgbqptwj+(_retbwjxi - 1))) - *(_z1ioc3c8+(_retbwjxi - (int)1 - 1))) - *(_sgbqptwj+(_retbwjxi - (int)1 - 1)) );
Mark111:;
				// continue
			}
						}		}// 
		
		return;//* 
		//*     End of SLARRB 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
