
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
//*> \brief \b SLARRV computes the eigenvectors of the tridiagonal matrix T = L D LT given L, D and the eigenvalues of L D LT. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLARRV + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slarrv.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slarrv.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slarrv.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLARRV( N, VL, VU, D, L, PIVMIN, 
//*                          ISPLIT, M, DOL, DOU, MINRGP, 
//*                          RTOL1, RTOL2, W, WERR, WGAP, 
//*                          IBLOCK, INDEXW, GERS, Z, LDZ, ISUPPZ, 
//*                          WORK, IWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       INTEGER            DOL, DOU, INFO, LDZ, M, N 
//*       REAL               MINRGP, PIVMIN, RTOL1, RTOL2, VL, VU 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IBLOCK( * ), INDEXW( * ), ISPLIT( * ), 
//*      $                   ISUPPZ( * ), IWORK( * ) 
//*       REAL               D( * ), GERS( * ), L( * ), W( * ), WERR( * ), 
//*      $                   WGAP( * ), WORK( * ) 
//*       REAL              Z( LDZ, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLARRV computes the eigenvectors of the tridiagonal matrix 
//*> T = L D L**T given L, D and APPROXIMATIONS to the eigenvalues of L D L**T. 
//*> The input eigenvalues should have been computed by SLARRE. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] VL 
//*> \verbatim 
//*>          VL is REAL 
//*>          Lower bound of the interval that contains the desired 
//*>          eigenvalues. VL < VU. Needed to compute gaps on the left or right 
//*>          end of the extremal eigenvalues in the desired RANGE. 
//*> \endverbatim 
//*> 
//*> \param[in] VU 
//*> \verbatim 
//*>          VU is REAL 
//*>          Upper bound of the interval that contains the desired 
//*>          eigenvalues. VL < VU.  
//*>          Note: VU is currently not used by this implementation of SLARRV, VU is 
//*>          passed to SLARRV because it could be used compute gaps on the right end 
//*>          of the extremal eigenvalues. However, with not much initial accuracy in 
//*>          LAMBDA and VU, the formula can lead to an overestimation of the right gap 
//*>          and thus to inadequately early RQI 'convergence'. This is currently 
//*>          prevented this by forcing a small right gap. And so it turns out that VU 
//*>          is currently not used by this implementation of SLARRV. 
//*> \endverbatim 
//*> 
//*> \param[in,out] D 
//*> \verbatim 
//*>          D is REAL array, dimension (N) 
//*>          On entry, the N diagonal elements of the diagonal matrix D. 
//*>          On exit, D may be overwritten. 
//*> \endverbatim 
//*> 
//*> \param[in,out] L 
//*> \verbatim 
//*>          L is REAL array, dimension (N) 
//*>          On entry, the (N-1) subdiagonal elements of the unit 
//*>          bidiagonal matrix L are in elements 1 to N-1 of L 
//*>          (if the matrix is not split.) At the end of each block 
//*>          is stored the corresponding shift as given by SLARRE. 
//*>          On exit, L is overwritten. 
//*> \endverbatim 
//*> 
//*> \param[in] PIVMIN 
//*> \verbatim 
//*>          PIVMIN is REAL 
//*>          The minimum pivot allowed in the Sturm sequence. 
//*> \endverbatim 
//*> 
//*> \param[in] ISPLIT 
//*> \verbatim 
//*>          ISPLIT is INTEGER array, dimension (N) 
//*>          The splitting points, at which T breaks up into blocks. 
//*>          The first block consists of rows/columns 1 to 
//*>          ISPLIT( 1 ), the second of rows/columns ISPLIT( 1 )+1 
//*>          through ISPLIT( 2 ), etc. 
//*> \endverbatim 
//*> 
//*> \param[in] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The total number of input eigenvalues.  0 <= M <= N. 
//*> \endverbatim 
//*> 
//*> \param[in] DOL 
//*> \verbatim 
//*>          DOL is INTEGER 
//*> \endverbatim 
//*> 
//*> \param[in] DOU 
//*> \verbatim 
//*>          DOU is INTEGER 
//*>          If the user wants to compute only selected eigenvectors from all 
//*>          the eigenvalues supplied, he can specify an index range DOL:DOU. 
//*>          Or else the setting DOL=1, DOU=M should be applied. 
//*>          Note that DOL and DOU refer to the order in which the eigenvalues 
//*>          are stored in W. 
//*>          If the user wants to compute only selected eigenpairs, then 
//*>          the columns DOL-1 to DOU+1 of the eigenvector space Z contain the 
//*>          computed eigenvectors. All other columns of Z are set to zero. 
//*> \endverbatim 
//*> 
//*> \param[in] MINRGP 
//*> \verbatim 
//*>          MINRGP is REAL 
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
//*>           Parameters for bisection. 
//*>           An interval [LEFT,RIGHT] has converged if 
//*>           RIGHT-LEFT < MAX( RTOL1*GAP, RTOL2*MAX(|LEFT|,|RIGHT|) ) 
//*> \endverbatim 
//*> 
//*> \param[in,out] W 
//*> \verbatim 
//*>          W is REAL array, dimension (N) 
//*>          The first M elements of W contain the APPROXIMATE eigenvalues for 
//*>          which eigenvectors are to be computed.  The eigenvalues 
//*>          should be grouped by split-off block and ordered from 
//*>          smallest to largest within the block ( The output array 
//*>          W from SLARRE is expected here ). Furthermore, they are with 
//*>          respect to the shift of the corresponding root representation 
//*>          for their block. On exit, W holds the eigenvalues of the 
//*>          UNshifted matrix. 
//*> \endverbatim 
//*> 
//*> \param[in,out] WERR 
//*> \verbatim 
//*>          WERR is REAL array, dimension (N) 
//*>          The first M elements contain the semiwidth of the uncertainty 
//*>          interval of the corresponding eigenvalue in W 
//*> \endverbatim 
//*> 
//*> \param[in,out] WGAP 
//*> \verbatim 
//*>          WGAP is REAL array, dimension (N) 
//*>          The separation from the right neighbor eigenvalue in W. 
//*> \endverbatim 
//*> 
//*> \param[in] IBLOCK 
//*> \verbatim 
//*>          IBLOCK is INTEGER array, dimension (N) 
//*>          The indices of the blocks (submatrices) associated with the 
//*>          corresponding eigenvalues in W; IBLOCK(i)=1 if eigenvalue 
//*>          W(i) belongs to the first block from the top, =2 if W(i) 
//*>          belongs to the second block, etc. 
//*> \endverbatim 
//*> 
//*> \param[in] INDEXW 
//*> \verbatim 
//*>          INDEXW is INTEGER array, dimension (N) 
//*>          The indices of the eigenvalues within each block (submatrix); 
//*>          for example, INDEXW(i)= 10 and IBLOCK(i)=2 imply that the 
//*>          i-th eigenvalue W(i) is the 10-th eigenvalue in the second block. 
//*> \endverbatim 
//*> 
//*> \param[in] GERS 
//*> \verbatim 
//*>          GERS is REAL array, dimension (2*N) 
//*>          The N Gerschgorin intervals (the i-th Gerschgorin interval 
//*>          is (GERS(2*i-1), GERS(2*i)). The Gerschgorin intervals should 
//*>          be computed from the original UNshifted matrix. 
//*> \endverbatim 
//*> 
//*> \param[out] Z 
//*> \verbatim 
//*>          Z is REAL array, dimension (LDZ, max(1,M) ) 
//*>          If INFO = 0, the first M columns of Z contain the 
//*>          orthonormal eigenvectors of the matrix T 
//*>          corresponding to the input eigenvalues, with the i-th 
//*>          column of Z holding the eigenvector associated with W(i). 
//*>          Note: the user must ensure that at least max(1,M) columns are 
//*>          supplied in the array Z. 
//*> \endverbatim 
//*> 
//*> \param[in] LDZ 
//*> \verbatim 
//*>          LDZ is INTEGER 
//*>          The leading dimension of the array Z.  LDZ >= 1, and if 
//*>          JOBZ = 'V', LDZ >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[out] ISUPPZ 
//*> \verbatim 
//*>          ISUPPZ is INTEGER array, dimension ( 2*max(1,M) ) 
//*>          The support of the eigenvectors in Z, i.e., the indices 
//*>          indicating the nonzero elements in Z. The I-th eigenvector 
//*>          is nonzero only in elements ISUPPZ( 2*I-1 ) through 
//*>          ISUPPZ( 2*I ). 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is REAL array, dimension (12*N) 
//*> \endverbatim 
//*> 
//*> \param[out] IWORK 
//*> \verbatim 
//*>          IWORK is INTEGER array, dimension (7*N) 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit 
//*> 
//*>          > 0:  A problem occurred in SLARRV. 
//*>          < 0:  One of the called subroutines signaled an internal problem. 
//*>                Needs inspection of the corresponding parameter IINFO 
//*>                for further information. 
//*> 
//*>          =-1:  Problem in SLARRB when refining a child's eigenvalues. 
//*>          =-2:  Problem in SLARRF when computing the RRR of a child. 
//*>                When a child is inside a tight cluster, it can be difficult 
//*>                to find an RRR. A partial remedy from the user's point of 
//*>                view is to make the parameter MINRGP smaller and recompile. 
//*>                However, as the orthogonality of the computed vectors is 
//*>                proportional to 1/MINRGP, the user should be aware that 
//*>                he might be trading in precision when he decreases MINRGP. 
//*>          =-3:  Problem in SLARRB when refining a single eigenvalue 
//*>                after the Rayleigh correction was rejected. 
//*>          = 5:  The Rayleigh Quotient Iteration failed to converge to 
//*>                full accuracy in MAXITR steps. 
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
//*> \ingroup realOTHERauxiliary 
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

	 
	public static void _1mqx40id(ref Int32 _dxpq0xkr, ref Single _ppzorcqs, ref Single _qqhwr930, Single* _plfm7z8g, Single* _68ec3gbh, ref Single _3aphllyg, Int32* _nn033w1s, ref Int32 _ev4xhht5, ref Int32 _q7ds33jw, ref Int32 _9zqqyco7, ref Single _i7h9sv6c, ref Single _ndrkejw5, ref Single _nmnmq6ye, Single* _z1ioc3c8, Single* _sgbqptwj, Single* _fwsre11k, Int32* _5zga5mk9, Int32* _z5l332hf, Single* _yisb1mtu, Single* _7e60fcso, ref Int32 _5l1tna8s, Int32* _nr4g8ae2, Single* _apig8meb, Int32* _4b6rt45i, ref Int32 _gro5yvfo)
	{
#region variable declarations
Int32 _to3hwp46 =  (int)10;
Single _d0547bi2 =  0f;
Single _kxg5drh2 =  1f;
Single _5m0mjfxm =  2f;
Single _08e01ee2 =  3f;
Single _ax5ijvbx =  4f;
Single _gbf4169i =  0.5f;
Boolean _fhxbjlrd =  default;
Boolean _jd1w6veh =  default;
Boolean _h7je8uep =  default;
Boolean _xv6wja5h =  default;
Boolean _7wnbpdoe =  default;
Boolean _pai0cw0n =  default;
Int32 _xzeqhzgj =  default;
Int32 _b5p6od9s =  default;
Int32 _d0i9k0it =  default;
Int32 _vii02k9v =  default;
Int32 _9dbezfkf =  default;
Int32 _retbwjxi =  default;
Int32 _dhnyp54j =  default;
Int32 _huyjic1d =  default;
Int32 _sey7lonb =  default;
Int32 _0u2khlex =  default;
Int32 _itfnbz60 =  default;
Int32 _2vvers93 =  default;
Int32 _oxr7eu3o =  default;
Int32 _neo0j9hw =  default;
Int32 _4sba1eox =  default;
Int32 _x28iz0zu =  default;
Int32 _cu6tg1rd =  default;
Int32 _g9sr7rfs =  default;
Int32 _rfhyvbjy =  default;
Int32 _em7fbywm =  default;
Int32 _qq2166xp =  default;
Int32 _znpjgsef =  default;
Int32 _18fz0zf9 =  default;
Int32 _umlkckdg =  default;
Int32 _4m3633rc =  default;
Int32 _gbwpeto2 =  default;
Int32 _oe1m55dn =  default;
Int32 _mssw7zcv =  default;
Int32 _bfbhh57k =  default;
Int32 _tjon631m =  default;
Int32 _rql640o8 =  default;
Int32 _0pjrk628 =  default;
Int32 _iptlk5fq =  default;
Int32 _9a64eb5v =  default;
Int32 _1l9k9q9k =  default;
Int32 _no4puhsw =  default;
Int32 _du5rqvlw =  default;
Int32 _qqeqjpbw =  default;
Int32 _l5743u3z =  default;
Int32 _txd5cnna =  default;
Int32 _ejwydfmr =  default;
Int32 _76h09x9n =  default;
Int32 _atumjwo3 =  default;
Int32 _1mfobugm =  default;
Int32 _xn5je7et =  default;
Int32 _ykny7pl7 =  default;
Int32 _nieyky3t =  default;
Int32 _1g82rqud =  default;
Int32 _cdtnilwx =  default;
Int32 _5tfthe2e =  default;
Int32 _mgs3jz0i =  default;
Int32 _dy83zg6i =  default;
Int32 _zjw358yb =  default;
Single _vq21tjlx =  default;
Single _n3qk703m =  default;
Single _p1iqarg6 =  default;
Single _7fnb0l4r =  default;
Single _xhzaoxno =  default;
Single _nljum38y =  default;
Single _lr8ennxn =  default;
Single _jf74kq7y =  default;
Single _w637r8jo =  default;
Single _pvwxvshr =  default;
Single _r0uu2bo8 =  default;
Single _rqh268z3 =  default;
Single _mlp50vl9 =  default;
Single _pjqtyez1 =  default;
Single _j9a36i1z =  default;
Single _ruhusobv =  default;
Single _seewpnfp =  default;
Single _uc2rsbqo =  default;
Single _wphaod8a =  default;
Single _6c0ig98c =  default;
Single _91a1vq5f =  default;
Single _1v6i5d3q =  default;
Single _v4rhszzn =  default;
Single _0446f4de =  default;
Single _2qcyvkhx =  default;
Single _txq1gp7u =  default;
Single _1qk23co8 =  default;
Int32 _n8tvvlnh =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.8.0) -- 
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
		//*     .. 
		// 
		
		_gro5yvfo = (int)0;//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr <= (int)0)
		{
			
			return;
		}
		//* 
		//*     The first N entries of WORK are reserved for the eigenvalues 
		
		_4sba1eox = (_dxpq0xkr + (int)1);
		_x28iz0zu = (((int)2 * _dxpq0xkr) + (int)1);
		_cu6tg1rd = (((int)3 * _dxpq0xkr) + (int)1);
		_gbwpeto2 = ((int)12 * _dxpq0xkr);// 
		
		{
			System.Int32 __81fgg2dlsvn3392 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step3392 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3392;
			for (__81fgg2count3392 = System.Math.Max(0, (System.Int32)(((System.Int32)(_gbwpeto2) - __81fgg2dlsvn3392 + __81fgg2step3392) / __81fgg2step3392)), _b5p6od9s = __81fgg2dlsvn3392; __81fgg2count3392 != 0; __81fgg2count3392--, _b5p6od9s += (__81fgg2step3392)) {

			{
				
				*(_apig8meb+(_b5p6od9s - 1)) = _d0547bi2;
Mark5:;
				// continue
			}
						}		}// 
		//*     IWORK(IINDR+1:IINDR+N) hold the twist indices R for the 
		//*     factorization used to compute the FP vector 
		
		_sey7lonb = (int)0;//*     IWORK(IINDC1+1:IINC2+N) are used to store the clusters of the current 
		//*     layer and the one above. 
		
		_dhnyp54j = _dxpq0xkr;
		_huyjic1d = ((int)2 * _dxpq0xkr);
		_0u2khlex = (((int)3 * _dxpq0xkr) + (int)1);// 
		
		_4m3633rc = ((int)7 * _dxpq0xkr);
		{
			System.Int32 __81fgg2dlsvn3393 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step3393 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3393;
			for (__81fgg2count3393 = System.Math.Max(0, (System.Int32)(((System.Int32)(_4m3633rc) - __81fgg2dlsvn3393 + __81fgg2step3393) / __81fgg2step3393)), _b5p6od9s = __81fgg2dlsvn3393; __81fgg2count3393 != 0; __81fgg2count3393--, _b5p6od9s += (__81fgg2step3393)) {

			{
				
				*(_4b6rt45i+(_b5p6od9s - 1)) = (int)0;
Mark10:;
				// continue
			}
						}		}// 
		
		_mgs3jz0i = (int)1;
		if (_q7ds33jw > (int)1)
		{
			//*        Set lower bound for use of Z 
			
			_mgs3jz0i = (_q7ds33jw - (int)1);
		}
		
		_dy83zg6i = _ev4xhht5;
		if (_9zqqyco7 < _ev4xhht5)
		{
			//*        Set lower bound for use of Z 
			
			_dy83zg6i = (_9zqqyco7 + (int)1);
		}
		//*     The width of the part of Z that is used 
		
		_zjw358yb = ((_dy83zg6i - _mgs3jz0i) + (int)1);// 
		// 
		
		_t013e1c8("Full" ,ref _dxpq0xkr ,ref _zjw358yb ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_7e60fcso+((int)1 - 1) + (_mgs3jz0i - 1) * 1 * (_5l1tna8s)),ref _5l1tna8s );// 
		
		_p1iqarg6 = _d5tu038y("Precision" );
		_uc2rsbqo = (_5m0mjfxm * _p1iqarg6);//* 
		//*     Set expert flags for standard code. 
		
		_xv6wja5h = true;// 
		
		if ((_q7ds33jw == (int)1) & (_9zqqyco7 == _ev4xhht5))
		{
			
		}
		else
		{
			//*        Only selected eigenpairs are computed. Since the other evalues 
			//*        are not refined by RQ iteration, bisection has to compute to full 
			//*        accuracy. 
			
			_ndrkejw5 = (_ax5ijvbx * _p1iqarg6);
			_nmnmq6ye = (_ax5ijvbx * _p1iqarg6);
		}
		// 
		//*     The entries WBEGIN:WEND in W, WERR, WGAP correspond to the 
		//*     desired eigenvalues. The support of the nonzero eigenvector 
		//*     entries is contained in the interval IBEGIN:IEND. 
		//*     Remark that if k eigenpairs are desired, then the eigenvectors 
		//*     are stored in k contiguous columns of Z. 
		// 
		//*     DONE is the number of eigenvectors already computed 
		
		_xzeqhzgj = (int)0;
		_d0i9k0it = (int)1;
		_1mfobugm = (int)1;
		{
			System.Int32 __81fgg2dlsvn3394 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step3394 = (System.Int32)((int)1);
			System.Int32 __81fgg2count3394;
			for (__81fgg2count3394 = System.Math.Max(0, (System.Int32)(((System.Int32)(*(_5zga5mk9+(_ev4xhht5 - 1))) - __81fgg2dlsvn3394 + __81fgg2step3394) / __81fgg2step3394)), _18fz0zf9 = __81fgg2dlsvn3394; __81fgg2count3394 != 0; __81fgg2count3394--, _18fz0zf9 += (__81fgg2step3394)) {

			{
				
				_9dbezfkf = *(_nn033w1s+(_18fz0zf9 - 1));
				_91a1vq5f = *(_68ec3gbh+(_9dbezfkf - 1));//*        Find the eigenvectors of the submatrix indexed IBEGIN 
				//*        through IEND. 
				
				_xn5je7et = (_1mfobugm - (int)1);
Mark15:;
				// continue
				if (_xn5je7et < _ev4xhht5)
				{
					
					if (*(_5zga5mk9+(_xn5je7et + (int)1 - 1)) == _18fz0zf9)
					{
						
						_xn5je7et = (_xn5je7et + (int)1);goto Mark15;
					}
					
				}
				
				if (_xn5je7et < _1mfobugm)
				{
					
					_d0i9k0it = (_9dbezfkf + (int)1);goto Mark170;
				}
				else
				if ((_xn5je7et < _q7ds33jw) | (_1mfobugm > _9zqqyco7))
				{
					
					_d0i9k0it = (_9dbezfkf + (int)1);
					_1mfobugm = (_xn5je7et + (int)1);goto Mark170;
				}
				// 
				//*        Find local spectral diameter of the block 
				
				_lr8ennxn = *(_yisb1mtu+(((int)2 * _d0i9k0it) - (int)1 - 1));
				_jf74kq7y = *(_yisb1mtu+((int)2 * _d0i9k0it - 1));
				{
					System.Int32 __81fgg2dlsvn3395 = (System.Int32)((_d0i9k0it + (int)1));
					const System.Int32 __81fgg2step3395 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3395;
					for (__81fgg2count3395 = System.Math.Max(0, (System.Int32)(((System.Int32)(_9dbezfkf) - __81fgg2dlsvn3395 + __81fgg2step3395) / __81fgg2step3395)), _b5p6od9s = __81fgg2dlsvn3395; __81fgg2count3395 != 0; __81fgg2count3395--, _b5p6od9s += (__81fgg2step3395)) {

					{
						
						_lr8ennxn = ILNumerics.F2NET.Intrinsics.MIN(*(_yisb1mtu+(((int)2 * _b5p6od9s) - (int)1 - 1)) ,_lr8ennxn );
						_jf74kq7y = ILNumerics.F2NET.Intrinsics.MAX(*(_yisb1mtu+((int)2 * _b5p6od9s - 1)) ,_jf74kq7y );
Mark20:;
						// continue
					}
										}				}
				_1v6i5d3q = (_jf74kq7y - _lr8ennxn);// 
				//*        OLDIEN is the last index of the previous block 
				
				_qqeqjpbw = (_d0i9k0it - (int)1);//*        Calculate the size of the current block 
				
				_oxr7eu3o = ((_9dbezfkf - _d0i9k0it) + (int)1);//*        The number of eigenvalues in the current block 
				
				_2vvers93 = ((_xn5je7et - _1mfobugm) + (int)1);// 
				//*        This is for a 1x1 block 
				
				if (_d0i9k0it == _9dbezfkf)
				{
					
					_xzeqhzgj = (_xzeqhzgj + (int)1);
					*(_7e60fcso+(_d0i9k0it - 1) + (_1mfobugm - 1) * 1 * (_5l1tna8s)) = _kxg5drh2;
					*(_nr4g8ae2+(((int)2 * _1mfobugm) - (int)1 - 1)) = _d0i9k0it;
					*(_nr4g8ae2+((int)2 * _1mfobugm - 1)) = _d0i9k0it;
					*(_z1ioc3c8+(_1mfobugm - 1)) = (*(_z1ioc3c8+(_1mfobugm - 1)) + _91a1vq5f);
					*(_apig8meb+(_1mfobugm - 1)) = *(_z1ioc3c8+(_1mfobugm - 1));
					_d0i9k0it = (_9dbezfkf + (int)1);
					_1mfobugm = (_1mfobugm + (int)1);goto Mark170;
				}
				// 
				//*        The desired (shifted) eigenvalues are stored in W(WBEGIN:WEND) 
				//*        Note that these can be approximations, in this case, the corresp. 
				//*        entries of WERR give the size of the uncertainty interval. 
				//*        The eigenvalue approximations will be refined when necessary as 
				//*        high relative accuracy is required for the computation of the 
				//*        corresponding eigenvectors. 
				
				_wcs7ne88(ref _2vvers93 ,(_z1ioc3c8+(_1mfobugm - 1)),ref Unsafe.AsRef((int)1) ,(_apig8meb+(_1mfobugm - 1)),ref Unsafe.AsRef((int)1) );// 
				//*        We store in W the eigenvalue approximations w.r.t. the original 
				//*        matrix T. 
				
				{
					System.Int32 __81fgg2dlsvn3396 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3396 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3396;
					for (__81fgg2count3396 = System.Math.Max(0, (System.Int32)(((System.Int32)(_2vvers93) - __81fgg2dlsvn3396 + __81fgg2step3396) / __81fgg2step3396)), _b5p6od9s = __81fgg2dlsvn3396; __81fgg2count3396 != 0; __81fgg2count3396--, _b5p6od9s += (__81fgg2step3396)) {

					{
						
						*(_z1ioc3c8+((_1mfobugm + _b5p6od9s) - (int)1 - 1)) = (*(_z1ioc3c8+((_1mfobugm + _b5p6od9s) - (int)1 - 1)) + _91a1vq5f);
Mark30:;
						// continue
					}
										}				}// 
				// 
				//*        NDEPTH is the current depth of the representation tree 
				
				_mssw7zcv = (int)0;//*        PARITY is either 1 or 0 
				
				_76h09x9n = (int)1;//*        NCLUS is the number of clusters for the next level of the 
				//*        representation tree, we start with NCLUS = 1 for the root 
				
				_oe1m55dn = (int)1;
				*(_4b6rt45i+(_dhnyp54j + (int)1 - 1)) = (int)1;
				*(_4b6rt45i+(_dhnyp54j + (int)2 - 1)) = _2vvers93;// 
				//*        IDONE is the number of eigenvectors already computed in the current 
				//*        block 
				
				_vii02k9v = (int)0;//*        loop while( IDONE.LT.IM ) 
				//*        generate the representation tree for the current block and 
				//*        compute the eigenvectors 
				
Mark40:;
				// continue
				if (_vii02k9v < _2vvers93)
				{
					//*           This is a crude protection against infinitely deep trees 
					
					if (_mssw7zcv > _ev4xhht5)
					{
						
						_gro5yvfo = (int)-2;
						return;
					}
					//*           breadth first processing of the current level of the representation 
					//*           tree: OLDNCL = number of clusters on current level 
					
					_txd5cnna = _oe1m55dn;//*           reset NCLUS to count the number of child clusters 
					
					_oe1m55dn = (int)0;//* 
					
					_76h09x9n = ((int)1 - _76h09x9n);
					if (_76h09x9n == (int)0)
					{
						
						_no4puhsw = _dhnyp54j;
						_tjon631m = _huyjic1d;
					}
					else
					{
						
						_no4puhsw = _huyjic1d;
						_tjon631m = _dhnyp54j;
					}
					//*           Process the clusters on the current level 
					
					{
						System.Int32 __81fgg2dlsvn3397 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step3397 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3397;
						for (__81fgg2count3397 = System.Math.Max(0, (System.Int32)(((System.Int32)(_txd5cnna) - __81fgg2dlsvn3397 + __81fgg2step3397) / __81fgg2step3397)), _b5p6od9s = __81fgg2dlsvn3397; __81fgg2count3397 != 0; __81fgg2count3397--, _b5p6od9s += (__81fgg2step3397)) {

						{
							
							_znpjgsef = (_no4puhsw + ((int)2 * _b5p6od9s));//*              OLDFST, OLDLST = first, last index of current cluster. 
							//*                               cluster indices start with 1 and are relative 
							//*                               to WBEGIN when accessing W, WGAP, WERR, Z 
							
							_du5rqvlw = *(_4b6rt45i+(_znpjgsef - (int)1 - 1));
							_l5743u3z = *(_4b6rt45i+(_znpjgsef - 1));
							if (_mssw7zcv > (int)0)
							{
								//*                 Retrieve relatively robust representation (RRR) of cluster 
								//*                 that has been computed at the previous level 
								//*                 The RRR is stored in Z and overwritten once the eigenvectors 
								//*                 have been computed or when the cluster is refined 
								// 
								
								if ((_q7ds33jw == (int)1) & (_9zqqyco7 == _ev4xhht5))
								{
									//*                    Get representation from location of the leftmost evalue 
									//*                    of the cluster 
									
									_znpjgsef = ((_1mfobugm + _du5rqvlw) - (int)1);
								}
								else
								{
									
									if (((_1mfobugm + _du5rqvlw) - (int)1) < _q7ds33jw)
									{
										//*                       Get representation from the left end of Z array 
										
										_znpjgsef = (_q7ds33jw - (int)1);
									}
									else
									if (((_1mfobugm + _du5rqvlw) - (int)1) > _9zqqyco7)
									{
										//*                       Get representation from the right end of Z array 
										
										_znpjgsef = _9zqqyco7;
									}
									else
									{
										
										_znpjgsef = ((_1mfobugm + _du5rqvlw) - (int)1);
									}
									
								}
								
								_wcs7ne88(ref _oxr7eu3o ,(_7e60fcso+(_d0i9k0it - 1) + (_znpjgsef - 1) * 1 * (_5l1tna8s)),ref Unsafe.AsRef((int)1) ,(_plfm7z8g+(_d0i9k0it - 1)),ref Unsafe.AsRef((int)1) );
								_wcs7ne88(ref Unsafe.AsRef(_oxr7eu3o - (int)1) ,(_7e60fcso+(_d0i9k0it - 1) + (_znpjgsef + (int)1 - 1) * 1 * (_5l1tna8s)),ref Unsafe.AsRef((int)1) ,(_68ec3gbh+(_d0i9k0it - 1)),ref Unsafe.AsRef((int)1) );
								_91a1vq5f = *(_7e60fcso+(_9dbezfkf - 1) + (_znpjgsef + (int)1 - 1) * 1 * (_5l1tna8s));// 
								//*                 Set the corresponding entries in Z to zero 
								
								_t013e1c8("Full" ,ref _oxr7eu3o ,ref Unsafe.AsRef((int)2) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,(_7e60fcso+(_d0i9k0it - 1) + (_znpjgsef - 1) * 1 * (_5l1tna8s)),ref _5l1tna8s );
							}
							// 
							//*              Compute DL and DLL of current RRR 
							
							{
								System.Int32 __81fgg2dlsvn3398 = (System.Int32)(_d0i9k0it);
								const System.Int32 __81fgg2step3398 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3398;
								for (__81fgg2count3398 = System.Math.Max(0, (System.Int32)(((System.Int32)(_9dbezfkf - (int)1) - __81fgg2dlsvn3398 + __81fgg2step3398) / __81fgg2step3398)), _znpjgsef = __81fgg2dlsvn3398; __81fgg2count3398 != 0; __81fgg2count3398--, _znpjgsef += (__81fgg2step3398)) {

								{
									
									_2qcyvkhx = (*(_plfm7z8g+(_znpjgsef - 1)) * *(_68ec3gbh+(_znpjgsef - 1)));
									*(_apig8meb+((_4sba1eox - (int)1) + _znpjgsef - 1)) = _2qcyvkhx;
									*(_apig8meb+((_x28iz0zu - (int)1) + _znpjgsef - 1)) = (_2qcyvkhx * *(_68ec3gbh+(_znpjgsef - 1)));
Mark50:;
									// continue
								}
																}							}// 
							
							if (_mssw7zcv > (int)0)
							{
								//*                 P and Q are index of the first and last eigenvalue to compute 
								//*                 within the current block 
								
								_ejwydfmr = *(_z5l332hf+((_1mfobugm - (int)1) + _du5rqvlw - 1));
								_atumjwo3 = *(_z5l332hf+((_1mfobugm - (int)1) + _l5743u3z - 1));//*                 Offset for the arrays WORK, WGAP and WERR, i.e., the P-OFFSET 
								//*                 through the Q-OFFSET elements of these arrays are to be used. 
								//*                  OFFSET = P-OLDFST 
								
								_1l9k9q9k = (*(_z5l332hf+(_1mfobugm - 1)) - (int)1);//*                 perform limited bisection (if necessary) to get approximate 
								//*                 eigenvalues to the precision needed. 
								
								_2vkkjyq0(ref _oxr7eu3o ,(_plfm7z8g+(_d0i9k0it - 1)),(_apig8meb+((_x28iz0zu + _d0i9k0it) - (int)1 - 1)),ref _ejwydfmr ,ref _atumjwo3 ,ref _ndrkejw5 ,ref _nmnmq6ye ,ref _1l9k9q9k ,(_apig8meb+(_1mfobugm - 1)),(_fwsre11k+(_1mfobugm - 1)),(_sgbqptwj+(_1mfobugm - 1)),(_apig8meb+(_cu6tg1rd - 1)),(_4b6rt45i+(_0u2khlex - 1)),ref _3aphllyg ,ref _1v6i5d3q ,ref _oxr7eu3o ,ref _itfnbz60 );
								if (_itfnbz60 != (int)0)
								{
									
									_gro5yvfo = (int)-1;
									return;
								}
								//*                 We also recompute the extremal gaps. W holds all eigenvalues 
								//*                 of the unshifted matrix and must be used for computation 
								//*                 of WGAP, the entries of WORK might stem from RRRs with 
								//*                 different shifts. The gaps from WBEGIN-1+OLDFST to 
								//*                 WBEGIN-1+OLDLST are correctly computed in SLARRB. 
								//*                 However, we only allow the gaps to become greater since 
								//*                 this is what should happen when we decrease WERR 
								
								if (_du5rqvlw > (int)1)
								{
									
									*(_fwsre11k+((_1mfobugm + _du5rqvlw) - (int)2 - 1)) = ILNumerics.F2NET.Intrinsics.MAX(*(_fwsre11k+((_1mfobugm + _du5rqvlw) - (int)2 - 1)) ,((*(_z1ioc3c8+((_1mfobugm + _du5rqvlw) - (int)1 - 1)) - *(_sgbqptwj+((_1mfobugm + _du5rqvlw) - (int)1 - 1))) - *(_z1ioc3c8+((_1mfobugm + _du5rqvlw) - (int)2 - 1))) - *(_sgbqptwj+((_1mfobugm + _du5rqvlw) - (int)2 - 1)) );
								}
								
								if (((_1mfobugm + _l5743u3z) - (int)1) < _xn5je7et)
								{
									
									*(_fwsre11k+((_1mfobugm + _l5743u3z) - (int)1 - 1)) = ILNumerics.F2NET.Intrinsics.MAX(*(_fwsre11k+((_1mfobugm + _l5743u3z) - (int)1 - 1)) ,((*(_z1ioc3c8+(_1mfobugm + _l5743u3z - 1)) - *(_sgbqptwj+(_1mfobugm + _l5743u3z - 1))) - *(_z1ioc3c8+((_1mfobugm + _l5743u3z) - (int)1 - 1))) - *(_sgbqptwj+((_1mfobugm + _l5743u3z) - (int)1 - 1)) );
								}
								//*                 Each time the eigenvalues in WORK get refined, we store 
								//*                 the newly found approximation with all shifts applied in W 
								
								{
									System.Int32 __81fgg2dlsvn3399 = (System.Int32)(_du5rqvlw);
									const System.Int32 __81fgg2step3399 = (System.Int32)((int)1);
									System.Int32 __81fgg2count3399;
									for (__81fgg2count3399 = System.Math.Max(0, (System.Int32)(((System.Int32)(_l5743u3z) - __81fgg2dlsvn3399 + __81fgg2step3399) / __81fgg2step3399)), _znpjgsef = __81fgg2dlsvn3399; __81fgg2count3399 != 0; __81fgg2count3399--, _znpjgsef += (__81fgg2step3399)) {

									{
										
										*(_z1ioc3c8+((_1mfobugm + _znpjgsef) - (int)1 - 1)) = (*(_apig8meb+((_1mfobugm + _znpjgsef) - (int)1 - 1)) + _91a1vq5f);
Mark53:;
										// continue
									}
																		}								}
							}
							// 
							//*              Process the current node. 
							
							_rql640o8 = _du5rqvlw;
							{
								System.Int32 __81fgg2dlsvn3400 = (System.Int32)(_du5rqvlw);
								const System.Int32 __81fgg2step3400 = (System.Int32)((int)1);
								System.Int32 __81fgg2count3400;
								for (__81fgg2count3400 = System.Math.Max(0, (System.Int32)(((System.Int32)(_l5743u3z) - __81fgg2dlsvn3400 + __81fgg2step3400) / __81fgg2step3400)), _znpjgsef = __81fgg2dlsvn3400; __81fgg2count3400 != 0; __81fgg2count3400--, _znpjgsef += (__81fgg2step3400)) {

								{
									
									if (_znpjgsef == _l5743u3z)
									{
										//*                    we are at the right end of the cluster, this is also the 
										//*                    boundary of the child cluster 
										
										_iptlk5fq = _znpjgsef;
									}
									else
									if (*(_fwsre11k+((_1mfobugm + _znpjgsef) - (int)1 - 1)) >= (_i7h9sv6c * ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+((_1mfobugm + _znpjgsef) - (int)1 - 1)) )))
									{
										//*                    the right relative gap is big enough, the child cluster 
										//*                    (NEWFST,..,NEWLST) is well separated from the following 
										
										_iptlk5fq = _znpjgsef;
									}
									else
									{
										//*                    inside a child cluster, the relative gap is not 
										//*                    big enough. 
										goto Mark140;
									}
									// 
									//*                 Compute size of child cluster found 
									
									_9a64eb5v = ((_iptlk5fq - _rql640o8) + (int)1);// 
									//*                 NEWFTT is the place in Z where the new RRR or the computed 
									//*                 eigenvector is to be stored 
									
									if ((_q7ds33jw == (int)1) & (_9zqqyco7 == _ev4xhht5))
									{
										//*                    Store representation at location of the leftmost evalue 
										//*                    of the cluster 
										
										_0pjrk628 = ((_1mfobugm + _rql640o8) - (int)1);
									}
									else
									{
										
										if (((_1mfobugm + _rql640o8) - (int)1) < _q7ds33jw)
										{
											//*                       Store representation at the left end of Z array 
											
											_0pjrk628 = (_q7ds33jw - (int)1);
										}
										else
										if (((_1mfobugm + _rql640o8) - (int)1) > _9zqqyco7)
										{
											//*                       Store representation at the right end of Z array 
											
											_0pjrk628 = _9zqqyco7;
										}
										else
										{
											
											_0pjrk628 = ((_1mfobugm + _rql640o8) - (int)1);
										}
										
									}
									// 
									
									if (_9a64eb5v > (int)1)
									{
										//* 
										//*                    Current child is not a singleton but a cluster. 
										//*                    Compute and store new representation of child. 
										//* 
										//* 
										//*                    Compute left and right cluster gap. 
										//* 
										//*                    LGAP and RGAP are not computed from WORK because 
										//*                    the eigenvalue approximations may stem from RRRs 
										//*                    different shifts. However, W hold all eigenvalues 
										//*                    of the unshifted matrix. Still, the entries in WGAP 
										//*                    have to be computed from WORK since the entries 
										//*                    in W might be of the same order so that gaps are not 
										//*                    exhibited correctly for very close eigenvalues. 
										
										if (_rql640o8 == (int)1)
										{
											
											_r0uu2bo8 = ILNumerics.F2NET.Intrinsics.MAX(_d0547bi2 ,(*(_z1ioc3c8+(_1mfobugm - 1)) - *(_sgbqptwj+(_1mfobugm - 1))) - _ppzorcqs );
										}
										else
										{
											
											_r0uu2bo8 = *(_fwsre11k+((_1mfobugm + _rql640o8) - (int)2 - 1));
										}
										
										_j9a36i1z = *(_fwsre11k+((_1mfobugm + _iptlk5fq) - (int)1 - 1));//* 
										//*                    Compute left- and rightmost eigenvalue of child 
										//*                    to high precision in order to shift as close 
										//*                    as possible and obtain as large relative gaps 
										//*                    as possible 
										//* 
										
										{
											System.Int32 __81fgg2dlsvn3401 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step3401 = (System.Int32)((int)1);
											System.Int32 __81fgg2count3401;
											for (__81fgg2count3401 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)2) - __81fgg2dlsvn3401 + __81fgg2step3401) / __81fgg2step3401)), _umlkckdg = __81fgg2dlsvn3401; __81fgg2count3401 != 0; __81fgg2count3401--, _umlkckdg += (__81fgg2step3401)) {

											{
												
												if (_umlkckdg == (int)1)
												{
													
													_ejwydfmr = *(_z5l332hf+((_1mfobugm - (int)1) + _rql640o8 - 1));
												}
												else
												{
													
													_ejwydfmr = *(_z5l332hf+((_1mfobugm - (int)1) + _iptlk5fq - 1));
												}
												
												_1l9k9q9k = (*(_z5l332hf+(_1mfobugm - 1)) - (int)1);
												_2vkkjyq0(ref _oxr7eu3o ,(_plfm7z8g+(_d0i9k0it - 1)),(_apig8meb+((_x28iz0zu + _d0i9k0it) - (int)1 - 1)),ref _ejwydfmr ,ref _ejwydfmr ,ref _uc2rsbqo ,ref _uc2rsbqo ,ref _1l9k9q9k ,(_apig8meb+(_1mfobugm - 1)),(_fwsre11k+(_1mfobugm - 1)),(_sgbqptwj+(_1mfobugm - 1)),(_apig8meb+(_cu6tg1rd - 1)),(_4b6rt45i+(_0u2khlex - 1)),ref _3aphllyg ,ref _1v6i5d3q ,ref _oxr7eu3o ,ref _itfnbz60 );
Mark55:;
												// continue
											}
																						}										}//* 
										
										if ((((_1mfobugm + _iptlk5fq) - (int)1) < _q7ds33jw) | (((_1mfobugm + _rql640o8) - (int)1) > _9zqqyco7))
										{
											//*                       if the cluster contains no desired eigenvalues 
											//*                       skip the computation of that branch of the rep. tree 
											//* 
											//*                       We could skip before the refinement of the extremal 
											//*                       eigenvalues of the child, but then the representation 
											//*                       tree could be different from the one when nothing is 
											//*                       skipped. For this reason we skip at this place. 
											
											_vii02k9v = (((_vii02k9v + _iptlk5fq) - _rql640o8) + (int)1);goto Mark139;
										}
										//* 
										//*                    Compute RRR of child cluster. 
										//*                    Note that the new RRR is stored in Z 
										//* 
										//*                    SLARRF needs LWORK = 2*N 
										
										_aqviol3d(ref _oxr7eu3o ,(_plfm7z8g+(_d0i9k0it - 1)),(_68ec3gbh+(_d0i9k0it - 1)),(_apig8meb+((_4sba1eox + _d0i9k0it) - (int)1 - 1)),ref _rql640o8 ,ref _iptlk5fq ,(_apig8meb+(_1mfobugm - 1)),(_fwsre11k+(_1mfobugm - 1)),(_sgbqptwj+(_1mfobugm - 1)),ref _1v6i5d3q ,ref _r0uu2bo8 ,ref _j9a36i1z ,ref _3aphllyg ,ref _0446f4de ,(_7e60fcso+(_d0i9k0it - 1) + (_0pjrk628 - 1) * 1 * (_5l1tna8s)),(_7e60fcso+(_d0i9k0it - 1) + (_0pjrk628 + (int)1 - 1) * 1 * (_5l1tna8s)),(_apig8meb+(_cu6tg1rd - 1)),ref _itfnbz60 );
										if (_itfnbz60 == (int)0)
										{
											//*                       a new RRR for the cluster was found by SLARRF 
											//*                       update shift and store it 
											
											_v4rhszzn = (_91a1vq5f + _0446f4de);
											*(_7e60fcso+(_9dbezfkf - 1) + (_0pjrk628 + (int)1 - 1) * 1 * (_5l1tna8s)) = _v4rhszzn;//*                       WORK() are the midpoints and WERR() the semi-width 
											//*                       Note that the entries in W are unchanged. 
											
											{
												System.Int32 __81fgg2dlsvn3402 = (System.Int32)(_rql640o8);
												const System.Int32 __81fgg2step3402 = (System.Int32)((int)1);
												System.Int32 __81fgg2count3402;
												for (__81fgg2count3402 = System.Math.Max(0, (System.Int32)(((System.Int32)(_iptlk5fq) - __81fgg2dlsvn3402 + __81fgg2step3402) / __81fgg2step3402)), _umlkckdg = __81fgg2dlsvn3402; __81fgg2count3402 != 0; __81fgg2count3402--, _umlkckdg += (__81fgg2step3402)) {

												{
													
													_7fnb0l4r = ((_08e01ee2 * _p1iqarg6) * ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+((_1mfobugm + _umlkckdg) - (int)1 - 1)) ));
													*(_apig8meb+((_1mfobugm + _umlkckdg) - (int)1 - 1)) = (*(_apig8meb+((_1mfobugm + _umlkckdg) - (int)1 - 1)) - _0446f4de);
													_7fnb0l4r = (_7fnb0l4r + ((_ax5ijvbx * _p1iqarg6) * ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+((_1mfobugm + _umlkckdg) - (int)1 - 1)) )));//*                          Fudge errors 
													
													*(_sgbqptwj+((_1mfobugm + _umlkckdg) - (int)1 - 1)) = (*(_sgbqptwj+((_1mfobugm + _umlkckdg) - (int)1 - 1)) + _7fnb0l4r);//*                          Gaps are not fudged. Provided that WERR is small 
													//*                          when eigenvalues are close, a zero gap indicates 
													//*                          that a new representation is needed for resolving 
													//*                          the cluster. A fudge could lead to a wrong decision 
													//*                          of judging eigenvalues 'separated' which in 
													//*                          reality are not. This could have a negative impact 
													//*                          on the orthogonality of the computed eigenvectors. 
													
Mark116:;
													// continue
												}
																								}											}// 
											
											_oe1m55dn = (_oe1m55dn + (int)1);
											_umlkckdg = (_tjon631m + ((int)2 * _oe1m55dn));
											*(_4b6rt45i+(_umlkckdg - (int)1 - 1)) = _rql640o8;
											*(_4b6rt45i+(_umlkckdg - 1)) = _iptlk5fq;
										}
										else
										{
											
											_gro5yvfo = (int)-2;
											return;
										}
										
									}
									else
									{
										//* 
										//*                    Compute eigenvector of singleton 
										//* 
										
										_em7fbywm = (int)0;//* 
										
										_txq1gp7u = ((_ax5ijvbx * ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.REAL(_oxr7eu3o ) )) * _p1iqarg6);//* 
										
										_umlkckdg = _rql640o8;
										_ykny7pl7 = ((_1mfobugm + _umlkckdg) - (int)1);
										_nieyky3t = ILNumerics.F2NET.Intrinsics.MAX(_ykny7pl7 - (int)1 ,(int)1 );
										_1g82rqud = ILNumerics.F2NET.Intrinsics.MIN(_ykny7pl7 + (int)1 ,_ev4xhht5 );
										_w637r8jo = *(_apig8meb+(_ykny7pl7 - 1));
										_xzeqhzgj = (_xzeqhzgj + (int)1);//*                    Check if eigenvector computation is to be skipped 
										
										if ((_ykny7pl7 < _q7ds33jw) | (_ykny7pl7 > _9zqqyco7))
										{
											
											_fhxbjlrd = true;goto Mark125;
										}
										else
										{
											
											_fhxbjlrd = false;
										}
										
										_pvwxvshr = (*(_apig8meb+(_ykny7pl7 - 1)) - *(_sgbqptwj+(_ykny7pl7 - 1)));
										_ruhusobv = (*(_apig8meb+(_ykny7pl7 - 1)) + *(_sgbqptwj+(_ykny7pl7 - 1)));
										_neo0j9hw = *(_z5l332hf+(_ykny7pl7 - 1));//*                    Note that since we compute the eigenpairs for a child, 
										//*                    all eigenvalue approximations are w.r.t the same shift. 
										//*                    In this case, the entries in WORK should be used for 
										//*                    computing the gaps since they exhibit even very small 
										//*                    differences in the eigenvalues, as opposed to the 
										//*                    entries in W which might "look" the same. 
										// 
										
										if (_umlkckdg == (int)1)
										{
											//*                       In the case RANGE='I' and with not much initial 
											//*                       accuracy in LAMBDA and VL, the formula 
											//*                       LGAP = MAX( ZERO, (SIGMA - VL) + LAMBDA ) 
											//*                       can lead to an overestimation of the left gap and 
											//*                       thus to inadequately early RQI 'convergence'. 
											//*                       Prevent this by forcing a small left gap. 
											
											_r0uu2bo8 = (_p1iqarg6 * ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_pvwxvshr ) ,ILNumerics.F2NET.Intrinsics.ABS(_ruhusobv ) ));
										}
										else
										{
											
											_r0uu2bo8 = *(_fwsre11k+(_nieyky3t - 1));
										}
										
										if (_umlkckdg == _2vvers93)
										{
											//*                       In the case RANGE='I' and with not much initial 
											//*                       accuracy in LAMBDA and VU, the formula 
											//*                       can lead to an overestimation of the right gap and 
											//*                       thus to inadequately early RQI 'convergence'. 
											//*                       Prevent this by forcing a small right gap. 
											
											_j9a36i1z = (_p1iqarg6 * ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_pvwxvshr ) ,ILNumerics.F2NET.Intrinsics.ABS(_ruhusobv ) ));
										}
										else
										{
											
											_j9a36i1z = *(_fwsre11k+(_ykny7pl7 - 1));
										}
										
										_xhzaoxno = ILNumerics.F2NET.Intrinsics.MIN(_r0uu2bo8 ,_j9a36i1z );
										if ((_umlkckdg == (int)1) | (_umlkckdg == _2vvers93))
										{
											//*                       The eigenvector support can become wrong 
											//*                       because significant entries could be cut off due to a 
											//*                       large GAPTOL parameter in LAR1V. Prevent this. 
											
											_nljum38y = _d0547bi2;
										}
										else
										{
											
											_nljum38y = (_xhzaoxno * _p1iqarg6);
										}
										
										_g9sr7rfs = _oxr7eu3o;
										_rfhyvbjy = (int)1;//*                    Update WGAP so that it holds the minimum gap 
										//*                    to the left or the right. This is crucial in the 
										//*                    case where bisection is used to ensure that the 
										//*                    eigenvalue is refined up to the required precision. 
										//*                    The correct value is restored afterwards. 
										
										_wphaod8a = *(_fwsre11k+(_ykny7pl7 - 1));
										*(_fwsre11k+(_ykny7pl7 - 1)) = _xhzaoxno;//*                    We want to use the Rayleigh Quotient Correction 
										//*                    as often as possible since it converges quadratically 
										//*                    when we are close enough to the desired eigenvalue. 
										//*                    However, the Rayleigh Quotient can have the wrong sign 
										//*                    and lead us away from the desired eigenvalue. In this 
										//*                    case, the best we can do is to use bisection. 
										
										_7wnbpdoe = false;
										_pai0cw0n = false;//*                    Bisection is initially turned off unless it is forced 
										
										_jd1w6veh = (!(_xv6wja5h));
Mark120:;
										// continue//*                    Check if bisection should be used to refine eigenvalue 
										
										if (_jd1w6veh)
										{
											//*                       Take the bisection as new iterate 
											
											_7wnbpdoe = true;
											_qq2166xp = *(_4b6rt45i+(_sey7lonb + _ykny7pl7 - 1));
											_1l9k9q9k = (*(_z5l332hf+(_1mfobugm - 1)) - (int)1);
											_2vkkjyq0(ref _oxr7eu3o ,(_plfm7z8g+(_d0i9k0it - 1)),(_apig8meb+((_x28iz0zu + _d0i9k0it) - (int)1 - 1)),ref _neo0j9hw ,ref _neo0j9hw ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_5m0mjfxm * _p1iqarg6) ,ref _1l9k9q9k ,(_apig8meb+(_1mfobugm - 1)),(_fwsre11k+(_1mfobugm - 1)),(_sgbqptwj+(_1mfobugm - 1)),(_apig8meb+(_cu6tg1rd - 1)),(_4b6rt45i+(_0u2khlex - 1)),ref _3aphllyg ,ref _1v6i5d3q ,ref _qq2166xp ,ref _itfnbz60 );
											if (_itfnbz60 != (int)0)
											{
												
												_gro5yvfo = (int)-3;
												return;
											}
											
											_w637r8jo = *(_apig8meb+(_ykny7pl7 - 1));//*                       Reset twist index from inaccurate LAMBDA to 
											//*                       force computation of true MINGMA 
											
											*(_4b6rt45i+(_sey7lonb + _ykny7pl7 - 1)) = (int)0;
										}
										//*                    Given LAMBDA, compute the eigenvector. 
										
										_fluj36w4(ref _oxr7eu3o ,ref Unsafe.AsRef((int)1) ,ref _oxr7eu3o ,ref _w637r8jo ,(_plfm7z8g+(_d0i9k0it - 1)),(_68ec3gbh+(_d0i9k0it - 1)),(_apig8meb+((_4sba1eox + _d0i9k0it) - (int)1 - 1)),(_apig8meb+((_x28iz0zu + _d0i9k0it) - (int)1 - 1)),ref _3aphllyg ,ref _nljum38y ,(_7e60fcso+(_d0i9k0it - 1) + (_ykny7pl7 - 1) * 1 * (_5l1tna8s)),ref Unsafe.AsRef(!(_7wnbpdoe)) ,ref _bfbhh57k ,ref _1qk23co8 ,ref _rqh268z3 ,ref Unsafe.AsRef(*(_4b6rt45i+(_sey7lonb + _ykny7pl7 - 1))) ,(_nr4g8ae2+(((int)2 * _ykny7pl7) - (int)1 - 1)),ref _mlp50vl9 ,ref _pjqtyez1 ,ref _seewpnfp ,(_apig8meb+(_cu6tg1rd - 1)));
										if (_em7fbywm == (int)0)
										{
											
											_vq21tjlx = _pjqtyez1;
											_n3qk703m = _w637r8jo;
										}
										else
										if (_pjqtyez1 < _vq21tjlx)
										{
											
											_vq21tjlx = _pjqtyez1;
											_n3qk703m = _w637r8jo;
										}
										
										_g9sr7rfs = ILNumerics.F2NET.Intrinsics.MIN(_g9sr7rfs ,*(_nr4g8ae2+(((int)2 * _ykny7pl7) - (int)1 - 1)) );
										_rfhyvbjy = ILNumerics.F2NET.Intrinsics.MAX(_rfhyvbjy ,*(_nr4g8ae2+((int)2 * _ykny7pl7 - 1)) );
										_em7fbywm = (_em7fbywm + (int)1);// 
										//*                    sin alpha <= |resid|/gap 
										//*                    Note that both the residual and the gap are 
										//*                    proportional to the matrix, so ||T|| doesn't play 
										//*                    a role in the quotient 
										// 
										//* 
										//*                    Convergence test for Rayleigh-Quotient iteration 
										//*                    (omitted when Bisection has been used) 
										//* 
										
										if (((_pjqtyez1 > (_txq1gp7u * _xhzaoxno)) & (ILNumerics.F2NET.Intrinsics.ABS(_seewpnfp ) > (_uc2rsbqo * ILNumerics.F2NET.Intrinsics.ABS(_w637r8jo )))) & (!(_7wnbpdoe)))
										{
											//*                       We need to check that the RQCORR update doesn't 
											//*                       move the eigenvalue away from the desired one and 
											//*                       towards a neighbor. -> protection with bisection 
											
											if (_neo0j9hw <= _bfbhh57k)
											{
												//*                          The wanted eigenvalue lies to the left 
												
												_6c0ig98c = (-(_kxg5drh2));
											}
											else
											{
												//*                          The wanted eigenvalue lies to the right 
												
												_6c0ig98c = _kxg5drh2;
											}
											//*                       We only use the RQCORR if it improves the 
											//*                       the iterate reasonably. 
											
											if ((((_seewpnfp * _6c0ig98c) >= _d0547bi2) & ((_w637r8jo + _seewpnfp) <= _ruhusobv)) & ((_w637r8jo + _seewpnfp) >= _pvwxvshr))
											{
												
												_pai0cw0n = true;//*                          Store new midpoint of bisection interval in WORK 
												
												if (_6c0ig98c == _kxg5drh2)
												{
													//*                             The current LAMBDA is on the left of the true 
													//*                             eigenvalue 
													
													_pvwxvshr = _w637r8jo;//*                             We prefer to assume that the error estimate 
													//*                             is correct. We could make the interval not 
													//*                             as a bracket but to be modified if the RQCORR 
													//*                             chooses to. In this case, the RIGHT side should 
													//*                             be modified as follows: 
													//*                              RIGHT = MAX(RIGHT, LAMBDA + RQCORR) 
													
												}
												else
												{
													//*                             The current LAMBDA is on the right of the true 
													//*                             eigenvalue 
													
													_ruhusobv = _w637r8jo;//*                             See comment about assuming the error estimate is 
													//*                             correct above. 
													//*                              LEFT = MIN(LEFT, LAMBDA + RQCORR) 
													
												}
												
												*(_apig8meb+(_ykny7pl7 - 1)) = (_gbf4169i * (_ruhusobv + _pvwxvshr));//*                          Take RQCORR since it has the correct sign and 
												//*                          improves the iterate reasonably 
												
												_w637r8jo = (_w637r8jo + _seewpnfp);//*                          Update width of error interval 
												
												*(_sgbqptwj+(_ykny7pl7 - 1)) = (_gbf4169i * (_ruhusobv - _pvwxvshr));
											}
											else
											{
												
												_jd1w6veh = true;
											}
											
											if ((_ruhusobv - _pvwxvshr) < (_uc2rsbqo * ILNumerics.F2NET.Intrinsics.ABS(_w637r8jo )))
											{
												//*                             The eigenvalue is computed to bisection accuracy 
												//*                             compute eigenvector and stop 
												
												_7wnbpdoe = true;goto Mark120;
											}
											else
											if (_em7fbywm < _to3hwp46)
											{
												goto Mark120;
											}
											else
											if (_em7fbywm == _to3hwp46)
											{
												
												_jd1w6veh = true;goto Mark120;
											}
											else
											{
												
												_gro5yvfo = (int)5;
												return;
											}
											
										}
										else
										{
											
											_h7je8uep = false;
											if ((_pai0cw0n & _7wnbpdoe) & (_vq21tjlx <= _pjqtyez1))
											{
												
												_w637r8jo = _n3qk703m;
												_h7je8uep = true;
											}
											
											if (_h7je8uep)
											{
												//*                          improve error angle by second step 
												
												_fluj36w4(ref _oxr7eu3o ,ref Unsafe.AsRef((int)1) ,ref _oxr7eu3o ,ref _w637r8jo ,(_plfm7z8g+(_d0i9k0it - 1)),(_68ec3gbh+(_d0i9k0it - 1)),(_apig8meb+((_4sba1eox + _d0i9k0it) - (int)1 - 1)),(_apig8meb+((_x28iz0zu + _d0i9k0it) - (int)1 - 1)),ref _3aphllyg ,ref _nljum38y ,(_7e60fcso+(_d0i9k0it - 1) + (_ykny7pl7 - 1) * 1 * (_5l1tna8s)),ref Unsafe.AsRef(!(_7wnbpdoe)) ,ref _bfbhh57k ,ref _1qk23co8 ,ref _rqh268z3 ,ref Unsafe.AsRef(*(_4b6rt45i+(_sey7lonb + _ykny7pl7 - 1))) ,(_nr4g8ae2+(((int)2 * _ykny7pl7) - (int)1 - 1)),ref _mlp50vl9 ,ref _pjqtyez1 ,ref _seewpnfp ,(_apig8meb+(_cu6tg1rd - 1)));
											}
											
											*(_apig8meb+(_ykny7pl7 - 1)) = _w637r8jo;
										}
										//* 
										//*                    Compute FP-vector support w.r.t. whole matrix 
										//* 
										
										*(_nr4g8ae2+(((int)2 * _ykny7pl7) - (int)1 - 1)) = (*(_nr4g8ae2+(((int)2 * _ykny7pl7) - (int)1 - 1)) + _qqeqjpbw);
										*(_nr4g8ae2+((int)2 * _ykny7pl7 - 1)) = (*(_nr4g8ae2+((int)2 * _ykny7pl7 - 1)) + _qqeqjpbw);
										_cdtnilwx = *(_nr4g8ae2+(((int)2 * _ykny7pl7) - (int)1 - 1));
										_5tfthe2e = *(_nr4g8ae2+((int)2 * _ykny7pl7 - 1));
										_g9sr7rfs = (_g9sr7rfs + _qqeqjpbw);
										_rfhyvbjy = (_rfhyvbjy + _qqeqjpbw);//*                    Ensure vector is ok if support in the RQI has changed 
										
										if (_g9sr7rfs < _cdtnilwx)
										{
											
											{
												System.Int32 __81fgg2dlsvn3403 = (System.Int32)(_g9sr7rfs);
												const System.Int32 __81fgg2step3403 = (System.Int32)((int)1);
												System.Int32 __81fgg2count3403;
												for (__81fgg2count3403 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cdtnilwx - (int)1) - __81fgg2dlsvn3403 + __81fgg2step3403) / __81fgg2step3403)), _retbwjxi = __81fgg2dlsvn3403; __81fgg2count3403 != 0; __81fgg2count3403--, _retbwjxi += (__81fgg2step3403)) {

												{
													
													*(_7e60fcso+(_retbwjxi - 1) + (_ykny7pl7 - 1) * 1 * (_5l1tna8s)) = _d0547bi2;
Mark122:;
													// continue
												}
																								}											}
										}
										
										if (_rfhyvbjy > _5tfthe2e)
										{
											
											{
												System.Int32 __81fgg2dlsvn3404 = (System.Int32)((_5tfthe2e + (int)1));
												const System.Int32 __81fgg2step3404 = (System.Int32)((int)1);
												System.Int32 __81fgg2count3404;
												for (__81fgg2count3404 = System.Math.Max(0, (System.Int32)(((System.Int32)(_rfhyvbjy) - __81fgg2dlsvn3404 + __81fgg2step3404) / __81fgg2step3404)), _retbwjxi = __81fgg2dlsvn3404; __81fgg2count3404 != 0; __81fgg2count3404--, _retbwjxi += (__81fgg2step3404)) {

												{
													
													*(_7e60fcso+(_retbwjxi - 1) + (_ykny7pl7 - 1) * 1 * (_5l1tna8s)) = _d0547bi2;
Mark123:;
													// continue
												}
																								}											}
										}
										
										_ct5qqrv7(ref Unsafe.AsRef((_5tfthe2e - _cdtnilwx) + (int)1) ,ref _mlp50vl9 ,(_7e60fcso+(_cdtnilwx - 1) + (_ykny7pl7 - 1) * 1 * (_5l1tna8s)),ref Unsafe.AsRef((int)1) );
Mark125:;
										// continue//*                    Update W 
										
										*(_z1ioc3c8+(_ykny7pl7 - 1)) = (_w637r8jo + _91a1vq5f);//*                    Recompute the gaps on the left and right 
										//*                    But only allow them to become larger and not 
										//*                    smaller (which can only happen through "bad" 
										//*                    cancellation and doesn't reflect the theory 
										//*                    where the initial gaps are underestimated due 
										//*                    to WERR being too crude.) 
										
										if (!(_fhxbjlrd))
										{
											
											if (_umlkckdg > (int)1)
											{
												
												*(_fwsre11k+(_nieyky3t - 1)) = ILNumerics.F2NET.Intrinsics.MAX(*(_fwsre11k+(_nieyky3t - 1)) ,((*(_z1ioc3c8+(_ykny7pl7 - 1)) - *(_sgbqptwj+(_ykny7pl7 - 1))) - *(_z1ioc3c8+(_nieyky3t - 1))) - *(_sgbqptwj+(_nieyky3t - 1)) );
											}
											
											if (_ykny7pl7 < _xn5je7et)
											{
												
												*(_fwsre11k+(_ykny7pl7 - 1)) = ILNumerics.F2NET.Intrinsics.MAX(_wphaod8a ,((*(_z1ioc3c8+(_1g82rqud - 1)) - *(_sgbqptwj+(_1g82rqud - 1))) - *(_z1ioc3c8+(_ykny7pl7 - 1))) - *(_sgbqptwj+(_ykny7pl7 - 1)) );
											}
											
										}
										
										_vii02k9v = (_vii02k9v + (int)1);
									}
									//*                 here ends the code for the current child 
									//* 
									
Mark139:;
									// continue//*                 Proceed to any remaining child nodes 
									
									_rql640o8 = (_znpjgsef + (int)1);
Mark140:;
									// continue
								}
																}							}
Mark150:;
							// continue
						}
												}					}
					_mssw7zcv = (_mssw7zcv + (int)1);goto Mark40;
				}
				
				_d0i9k0it = (_9dbezfkf + (int)1);
				_1mfobugm = (_xn5je7et + (int)1);
Mark170:;
				// continue
			}
						}		}//* 
		// 
		
		return;//* 
		//*     End of SLARRV 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
