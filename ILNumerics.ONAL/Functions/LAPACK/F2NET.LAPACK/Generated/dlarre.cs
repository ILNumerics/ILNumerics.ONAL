
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
//*> \brief \b DLARRE given the tridiagonal matrix T, sets small off-diagonal elements to zero and for each unreduced block Ti, finds base representations and eigenvalues. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLARRE + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlarre.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlarre.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlarre.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLARRE( RANGE, N, VL, VU, IL, IU, D, E, E2, 
//*                           RTOL1, RTOL2, SPLTOL, NSPLIT, ISPLIT, M, 
//*                           W, WERR, WGAP, IBLOCK, INDEXW, GERS, PIVMIN, 
//*                           WORK, IWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          RANGE 
//*       INTEGER            IL, INFO, IU, M, N, NSPLIT 
//*       DOUBLE PRECISION  PIVMIN, RTOL1, RTOL2, SPLTOL, VL, VU 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            IBLOCK( * ), ISPLIT( * ), IWORK( * ), 
//*      $                   INDEXW( * ) 
//*       DOUBLE PRECISION   D( * ), E( * ), E2( * ), GERS( * ), 
//*      $                   W( * ),WERR( * ), WGAP( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> To find the desired eigenvalues of a given real symmetric 
//*> tridiagonal matrix T, DLARRE sets any "small" off-diagonal 
//*> elements to zero, and for each unreduced block T_i, it finds 
//*> (a) a suitable shift at one end of the block's spectrum, 
//*> (b) the base representation, T_i - sigma_i I = L_i D_i L_i^T, and 
//*> (c) eigenvalues of each L_i D_i L_i^T. 
//*> The representations and eigenvalues found are then used by 
//*> DSTEMR to compute the eigenvectors of T. 
//*> The accuracy varies depending on whether bisection is used to 
//*> find a few eigenvalues or the dqds algorithm (subroutine DLASQ2) to 
//*> conpute all and then discard any unwanted one. 
//*> As an added benefit, DLARRE also outputs the n 
//*> Gerschgorin intervals for the matrices L_i D_i L_i^T. 
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
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix. N > 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] VL 
//*> \verbatim 
//*>          VL is DOUBLE PRECISION 
//*>          If RANGE='V', the lower bound for the eigenvalues. 
//*>          Eigenvalues less than or equal to VL, or greater than VU, 
//*>          will not be returned.  VL < VU. 
//*>          If RANGE='I' or ='A', DLARRE computes bounds on the desired 
//*>          part of the spectrum. 
//*> \endverbatim 
//*> 
//*> \param[in,out] VU 
//*> \verbatim 
//*>          VU is DOUBLE PRECISION 
//*>          If RANGE='V', the upper bound for the eigenvalues. 
//*>          Eigenvalues less than or equal to VL, or greater than VU, 
//*>          will not be returned.  VL < VU. 
//*>          If RANGE='I' or ='A', DLARRE computes bounds on the desired 
//*>          part of the spectrum. 
//*> \endverbatim 
//*> 
//*> \param[in] IL 
//*> \verbatim 
//*>          IL is INTEGER 
//*>          If RANGE='I', the index of the 
//*>          smallest eigenvalue to be returned. 
//*>          1 <= IL <= IU <= N. 
//*> \endverbatim 
//*> 
//*> \param[in] IU 
//*> \verbatim 
//*>          IU is INTEGER 
//*>          If RANGE='I', the index of the 
//*>          largest eigenvalue to be returned. 
//*>          1 <= IL <= IU <= N. 
//*> \endverbatim 
//*> 
//*> \param[in,out] D 
//*> \verbatim 
//*>          D is DOUBLE PRECISION array, dimension (N) 
//*>          On entry, the N diagonal elements of the tridiagonal 
//*>          matrix T. 
//*>          On exit, the N diagonal elements of the diagonal 
//*>          matrices D_i. 
//*> \endverbatim 
//*> 
//*> \param[in,out] E 
//*> \verbatim 
//*>          E is DOUBLE PRECISION array, dimension (N) 
//*>          On entry, the first (N-1) entries contain the subdiagonal 
//*>          elements of the tridiagonal matrix T; E(N) need not be set. 
//*>          On exit, E contains the subdiagonal elements of the unit 
//*>          bidiagonal matrices L_i. The entries E( ISPLIT( I ) ), 
//*>          1 <= I <= NSPLIT, contain the base points sigma_i on output. 
//*> \endverbatim 
//*> 
//*> \param[in,out] E2 
//*> \verbatim 
//*>          E2 is DOUBLE PRECISION array, dimension (N) 
//*>          On entry, the first (N-1) entries contain the SQUARES of the 
//*>          subdiagonal elements of the tridiagonal matrix T; 
//*>          E2(N) need not be set. 
//*>          On exit, the entries E2( ISPLIT( I ) ), 
//*>          1 <= I <= NSPLIT, have been set to zero 
//*> \endverbatim 
//*> 
//*> \param[in] RTOL1 
//*> \verbatim 
//*>          RTOL1 is DOUBLE PRECISION 
//*> \endverbatim 
//*> 
//*> \param[in] RTOL2 
//*> \verbatim 
//*>          RTOL2 is DOUBLE PRECISION 
//*>           Parameters for bisection. 
//*>           An interval [LEFT,RIGHT] has converged if 
//*>           RIGHT-LEFT < MAX( RTOL1*GAP, RTOL2*MAX(|LEFT|,|RIGHT|) ) 
//*> \endverbatim 
//*> 
//*> \param[in] SPLTOL 
//*> \verbatim 
//*>          SPLTOL is DOUBLE PRECISION 
//*>          The threshold for splitting. 
//*> \endverbatim 
//*> 
//*> \param[out] NSPLIT 
//*> \verbatim 
//*>          NSPLIT is INTEGER 
//*>          The number of blocks T splits into. 1 <= NSPLIT <= N. 
//*> \endverbatim 
//*> 
//*> \param[out] ISPLIT 
//*> \verbatim 
//*>          ISPLIT is INTEGER array, dimension (N) 
//*>          The splitting points, at which T breaks up into blocks. 
//*>          The first block consists of rows/columns 1 to ISPLIT(1), 
//*>          the second of rows/columns ISPLIT(1)+1 through ISPLIT(2), 
//*>          etc., and the NSPLIT-th consists of rows/columns 
//*>          ISPLIT(NSPLIT-1)+1 through ISPLIT(NSPLIT)=N. 
//*> \endverbatim 
//*> 
//*> \param[out] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The total number of eigenvalues (of all L_i D_i L_i^T) 
//*>          found. 
//*> \endverbatim 
//*> 
//*> \param[out] W 
//*> \verbatim 
//*>          W is DOUBLE PRECISION array, dimension (N) 
//*>          The first M elements contain the eigenvalues. The 
//*>          eigenvalues of each of the blocks, L_i D_i L_i^T, are 
//*>          sorted in ascending order ( DLARRE may use the 
//*>          remaining N-M elements as workspace). 
//*> \endverbatim 
//*> 
//*> \param[out] WERR 
//*> \verbatim 
//*>          WERR is DOUBLE PRECISION array, dimension (N) 
//*>          The error bound on the corresponding eigenvalue in W. 
//*> \endverbatim 
//*> 
//*> \param[out] WGAP 
//*> \verbatim 
//*>          WGAP is DOUBLE PRECISION array, dimension (N) 
//*>          The separation from the right neighbor eigenvalue in W. 
//*>          The gap is only with respect to the eigenvalues of the same block 
//*>          as each block has its own representation tree. 
//*>          Exception: at the right end of a block we store the left gap 
//*> \endverbatim 
//*> 
//*> \param[out] IBLOCK 
//*> \verbatim 
//*>          IBLOCK is INTEGER array, dimension (N) 
//*>          The indices of the blocks (submatrices) associated with the 
//*>          corresponding eigenvalues in W; IBLOCK(i)=1 if eigenvalue 
//*>          W(i) belongs to the first block from the top, =2 if W(i) 
//*>          belongs to the second block, etc. 
//*> \endverbatim 
//*> 
//*> \param[out] INDEXW 
//*> \verbatim 
//*>          INDEXW is INTEGER array, dimension (N) 
//*>          The indices of the eigenvalues within each block (submatrix); 
//*>          for example, INDEXW(i)= 10 and IBLOCK(i)=2 imply that the 
//*>          i-th eigenvalue W(i) is the 10-th eigenvalue in block 2 
//*> \endverbatim 
//*> 
//*> \param[out] GERS 
//*> \verbatim 
//*>          GERS is DOUBLE PRECISION array, dimension (2*N) 
//*>          The N Gerschgorin intervals (the i-th Gerschgorin interval 
//*>          is (GERS(2*i-1), GERS(2*i)). 
//*> \endverbatim 
//*> 
//*> \param[out] PIVMIN 
//*> \verbatim 
//*>          PIVMIN is DOUBLE PRECISION 
//*>          The minimum pivot in the Sturm sequence for T. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is DOUBLE PRECISION array, dimension (6*N) 
//*>          Workspace. 
//*> \endverbatim 
//*> 
//*> \param[out] IWORK 
//*> \verbatim 
//*>          IWORK is INTEGER array, dimension (5*N) 
//*>          Workspace. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit 
//*>          > 0:  A problem occurred in DLARRE. 
//*>          < 0:  One of the called subroutines signaled an internal problem. 
//*>                Needs inspection of the corresponding parameter IINFO 
//*>                for further information. 
//*> 
//*>          =-1:  Problem in DLARRD. 
//*>          = 2:  No base representation could be found in MAXTRY iterations. 
//*>                Increasing MAXTRY and recompilation might be a remedy. 
//*>          =-3:  Problem in DLARRB when computing the refined root 
//*>                representation for DLASQ2. 
//*>          =-4:  Problem in DLARRB when preforming bisection on the 
//*>                desired part of the spectrum. 
//*>          =-5:  Problem in DLASQ2. 
//*>          =-6:  Problem in DLASQ2. 
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
//*> \ingroup OTHERauxiliary 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  The base representations are required to suffer very little 
//*>  element growth and consequently define all their eigenvalues to 
//*>  high relative accuracy. 
//*> \endverbatim 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>     Beresford Parlett, University of California, Berkeley, USA \n 
//*>     Jim Demmel, University of California, Berkeley, USA \n 
//*>     Inderjit Dhillon, University of Texas, Austin, USA \n 
//*>     Osni Marques, LBNL/NERSC, USA \n 
//*>     Christof Voemel, University of California, Berkeley, USA \n 
//*> 
//*  ===================================================================== 

	 
	public static void _35k9fpss(FString _wrqmi80z, ref Int32 _dxpq0xkr, ref Double _ppzorcqs, ref Double _qqhwr930, ref Int32 _ic6kua09, ref Int32 _j4l29b9c, Double* _plfm7z8g, Double* _864fslqq, Double* _0maek8rz, ref Double _ndrkejw5, ref Double _nmnmq6ye, ref Double _6odkwg9v, ref Int32 _naa7acm7, Int32* _nn033w1s, ref Int32 _ev4xhht5, Double* _z1ioc3c8, Double* _sgbqptwj, Double* _fwsre11k, Int32* _5zga5mk9, Int32* _z5l332hf, Double* _yisb1mtu, ref Double _3aphllyg, Double* _apig8meb, Int32* _4b6rt45i, ref Int32 _gro5yvfo)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)16 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Double _5m0mjfxm =  2d;
Double _ax5ijvbx =  4d;
Double _kxg5drh2 =  1d;
Double _7fnb0l4r =  2d;
Double _gbf4169i =  _kxg5drh2 / _5m0mjfxm;
Double _cu7spn6z =  100d;
Double _00l1i2h1 =  64d;
Double _lfikici9 =  _kxg5drh2 / _ax5ijvbx;
Double _5yjbn35p =  8d;
Double _g0jdw6pt =  _gbf4169i;
Double _d0547bi2 =  0d;
Int32 _5tt65im7 =  (int)6;
Int32 _ytsyu76w =  (int)1;
Int32 _atfcdrg6 =  (int)2;
Int32 _55ursdfo =  (int)3;
Boolean _zzozsldx =  default;
Boolean _lmn3lg5j =  default;
Boolean _4xdd1mqo =  default;
Int32 _mwbax5ov =  default;
Int32 _oe84shdy =  default;
Int32 _ps01envy =  default;
Int32 _b5p6od9s =  default;
Int32 _d0i9k0it =  default;
Int32 _xpllja47 =  default;
Int32 _9dbezfkf =  default;
Int32 _itfnbz60 =  default;
Int32 _oxr7eu3o =  default;
Int32 _9lx59bxm =  default;
Int32 _iam5kd9q =  default;
Int32 _o42699gn =  default;
Int32 _znpjgsef =  default;
Int32 _18fz0zf9 =  default;
Int32 _82ogha0x =  default;
Int32 _e9y2lltf =  default;
Int32 _1mfobugm =  default;
Int32 _xn5je7et =  default;
Double _ripqcbhm =  default;
Double _toyiwzj9 =  default;
Double _0dzv290j =  default;
Double _49t2npjg =  default;
Double _bnamisvm =  default;
Double _rwvs4hol =  default;
Double _trz23puj =  default;
Double _tj3m8oq8 =  default;
Double _p1iqarg6 =  default;
Double _lr8ennxn =  default;
Double _jf74kq7y =  default;
Double _v9lvno0t =  default;
Double _0m46cxip =  default;
Double _gczvoh2l =  default;
Double _9mzm56bw =  default;
Double _fmb4u5ka =  default;
Double _slkbnmvx =  default;
Double _h75qnr7l =  default;
Double _6c0ig98c =  default;
Double _91a1vq5f =  default;
Double _1v6i5d3q =  default;
Double _0446f4de =  default;
Double _2qcyvkhx =  default;
Double _c0o9kuh7 =  default;
Int32* _5c1snrj6 =  (Int32*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Int32) * ((int)4);
string fLanavab = default;
#endregion  variable declarations
_wrqmi80z = _wrqmi80z.Convert(1);

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
		// 
		// 
		//*     .. 
		//*     .. Local Arrays .. 
		//*     .. 
		//*     .. External Functions .. 
		// 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		// 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		// 
		
		_gro5yvfo = (int)0;//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr <= (int)0)
		{
			
			return;
		}
		//* 
		//*     Decode RANGE 
		//* 
		
		if (_w8y2rzgy(_wrqmi80z ,"A" ))
		{
			
			_o42699gn = _ytsyu76w;
		}
		else
		if (_w8y2rzgy(_wrqmi80z ,"V" ))
		{
			
			_o42699gn = _55ursdfo;
		}
		else
		if (_w8y2rzgy(_wrqmi80z ,"I" ))
		{
			
			_o42699gn = _atfcdrg6;
		}
		// 
		
		_ev4xhht5 = (int)0;// 
		//*     Get machine constants 
		
		_h75qnr7l = _f43eg0w0("S" );
		_p1iqarg6 = _f43eg0w0("P" );// 
		//*     Set parameters 
		
		_gczvoh2l = ILNumerics.F2NET.Intrinsics.SQRT(_p1iqarg6 );
		_toyiwzj9 = ILNumerics.F2NET.Intrinsics.SQRT(_p1iqarg6 );// 
		//*     Treat case of 1x1 matrix for quick return 
		
		if (_dxpq0xkr == (int)1)
		{
			
			if (((_o42699gn == _ytsyu76w) | (((_o42699gn == _55ursdfo) & (*(_plfm7z8g+((int)1 - 1)) > _ppzorcqs)) & (*(_plfm7z8g+((int)1 - 1)) <= _qqhwr930))) | (((_o42699gn == _atfcdrg6) & (_ic6kua09 == (int)1)) & (_j4l29b9c == (int)1)))
			{
				
				_ev4xhht5 = (int)1;
				*(_z1ioc3c8+((int)1 - 1)) = *(_plfm7z8g+((int)1 - 1));//*           The computation error of the eigenvalue is zero 
				
				*(_sgbqptwj+((int)1 - 1)) = _d0547bi2;
				*(_fwsre11k+((int)1 - 1)) = _d0547bi2;
				*(_5zga5mk9+((int)1 - 1)) = (int)1;
				*(_z5l332hf+((int)1 - 1)) = (int)1;
				*(_yisb1mtu+((int)1 - 1)) = *(_plfm7z8g+((int)1 - 1));
				*(_yisb1mtu+((int)2 - 1)) = *(_plfm7z8g+((int)1 - 1));
			}
			//*        store the shift for the initial RRR, which is zero in this case 
			
			*(_864fslqq+((int)1 - 1)) = _d0547bi2;
			return;
		}
		// 
		//*     General case: tridiagonal matrix of order > 1 
		//* 
		//*     Init WERR, WGAP. Compute Gerschgorin intervals and spectral diameter. 
		//*     Compute maximum off-diagonal entry and pivmin. 
		
		_lr8ennxn = *(_plfm7z8g+((int)1 - 1));
		_jf74kq7y = *(_plfm7z8g+((int)1 - 1));
		_tj3m8oq8 = _d0547bi2;
		_trz23puj = _d0547bi2;
		*(_864fslqq+(_dxpq0xkr - 1)) = _d0547bi2;
		{
			System.Int32 __81fgg2dlsvn2869 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2869 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2869;
			for (__81fgg2count2869 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2869 + __81fgg2step2869) / __81fgg2step2869)), _b5p6od9s = __81fgg2dlsvn2869; __81fgg2count2869 != 0; __81fgg2count2869--, _b5p6od9s += (__81fgg2step2869)) {

			{
				
				*(_sgbqptwj+(_b5p6od9s - 1)) = _d0547bi2;
				*(_fwsre11k+(_b5p6od9s - 1)) = _d0547bi2;
				_rwvs4hol = ILNumerics.F2NET.Intrinsics.ABS(*(_864fslqq+(_b5p6od9s - 1)) );
				if (_rwvs4hol >= _trz23puj)
				{
					
					_trz23puj = _rwvs4hol;
				}
				
				_c0o9kuh7 = (_rwvs4hol + _tj3m8oq8);
				*(_yisb1mtu+(((int)2 * _b5p6od9s) - (int)1 - 1)) = (*(_plfm7z8g+(_b5p6od9s - 1)) - _c0o9kuh7);
				_lr8ennxn = ILNumerics.F2NET.Intrinsics.MIN(_lr8ennxn ,*(_yisb1mtu+(((int)2 * _b5p6od9s) - (int)1 - 1)) );
				*(_yisb1mtu+((int)2 * _b5p6od9s - 1)) = (*(_plfm7z8g+(_b5p6od9s - 1)) + _c0o9kuh7);
				_jf74kq7y = ILNumerics.F2NET.Intrinsics.MAX(_jf74kq7y ,*(_yisb1mtu+((int)2 * _b5p6od9s - 1)) );
				_tj3m8oq8 = _rwvs4hol;
Mark5:;
				// continue
			}
						}		}//*     The minimum pivot allowed in the Sturm sequence for T 
		
		_3aphllyg = (_h75qnr7l * ILNumerics.F2NET.Intrinsics.MAX(_kxg5drh2 ,__POW2(_trz23puj) ));//*     Compute spectral diameter. The Gerschgorin bounds give an 
		//*     estimate that is wrong by at most a factor of SQRT(2) 
		
		_1v6i5d3q = (_jf74kq7y - _lr8ennxn);// 
		//*     Compute splitting points 
		
		_bnf6z4to(ref _dxpq0xkr ,_plfm7z8g ,_864fslqq ,_0maek8rz ,ref _6odkwg9v ,ref _1v6i5d3q ,ref _naa7acm7 ,_nn033w1s ,ref _itfnbz60 );// 
		//*     Can force use of bisection instead of faster DQDS. 
		//*     Option left in the code for future multisection work. 
		
		_zzozsldx = false;// 
		//*     Initialize USEDQD, DQDS should be used for ALLRNG unless someone 
		//*     explicitly wants bisection. 
		
		_4xdd1mqo = ((_o42699gn == _ytsyu76w) & (!(_zzozsldx)));// 
		
		if ((_o42699gn == _ytsyu76w) & (!(_zzozsldx)))
		{
			//*        Set interval [VL,VU] that contains all eigenvalues 
			
			_ppzorcqs = _lr8ennxn;
			_qqhwr930 = _jf74kq7y;
		}
		else
		{
			//*        We call DLARRD to find crude approximations to the eigenvalues 
			//*        in the desired range. In case IRANGE = INDRNG, we also obtain the 
			//*        interval (VL,VU] that contains all the wanted eigenvalues. 
			//*        An interval [LEFT,RIGHT] has converged if 
			//*        RIGHT-LEFT.LT.RTOL*MAX(ABS(LEFT),ABS(RIGHT)) 
			//*        DLARRD needs a WORK of size 4*N, IWORK of size 3*N 
			
			_havbzny4(_wrqmi80z ,"B" ,ref _dxpq0xkr ,ref _ppzorcqs ,ref _qqhwr930 ,ref _ic6kua09 ,ref _j4l29b9c ,_yisb1mtu ,ref _toyiwzj9 ,_plfm7z8g ,_864fslqq ,_0maek8rz ,ref _3aphllyg ,ref _naa7acm7 ,_nn033w1s ,ref _e9y2lltf ,_z1ioc3c8 ,_sgbqptwj ,ref _ppzorcqs ,ref _qqhwr930 ,_5zga5mk9 ,_z5l332hf ,_apig8meb ,_4b6rt45i ,ref _itfnbz60 );
			if (_itfnbz60 != (int)0)
			{
				
				_gro5yvfo = (int)-1;
				return;
			}
			//*        Make sure that the entries M+1 to N in W, WERR, IBLOCK, INDEXW are 0 
			
			{
				System.Int32 __81fgg2dlsvn2870 = (System.Int32)((_e9y2lltf + (int)1));
				const System.Int32 __81fgg2step2870 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2870;
				for (__81fgg2count2870 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2870 + __81fgg2step2870) / __81fgg2step2870)), _b5p6od9s = __81fgg2dlsvn2870; __81fgg2count2870 != 0; __81fgg2count2870--, _b5p6od9s += (__81fgg2step2870)) {

				{
					
					*(_z1ioc3c8+(_b5p6od9s - 1)) = _d0547bi2;
					*(_sgbqptwj+(_b5p6od9s - 1)) = _d0547bi2;
					*(_5zga5mk9+(_b5p6od9s - 1)) = (int)0;
					*(_z5l332hf+(_b5p6od9s - 1)) = (int)0;
Mark14:;
					// continue
				}
								}			}
		}
		// 
		// 
		//*** 
		//*     Loop over unreduced blocks 
		
		_d0i9k0it = (int)1;
		_1mfobugm = (int)1;
		{
			System.Int32 __81fgg2dlsvn2871 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step2871 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2871;
			for (__81fgg2count2871 = System.Math.Max(0, (System.Int32)(((System.Int32)(_naa7acm7) - __81fgg2dlsvn2871 + __81fgg2step2871) / __81fgg2step2871)), _18fz0zf9 = __81fgg2dlsvn2871; __81fgg2count2871 != 0; __81fgg2count2871--, _18fz0zf9 += (__81fgg2step2871)) {

			{
				
				_9dbezfkf = *(_nn033w1s+(_18fz0zf9 - 1));
				_oxr7eu3o = ((_9dbezfkf - _d0i9k0it) + (int)1);// 
				//*        1 X 1 block 
				
				if (_oxr7eu3o == (int)1)
				{
					
					if (((_o42699gn == _ytsyu76w) | (((_o42699gn == _55ursdfo) & (*(_plfm7z8g+(_d0i9k0it - 1)) > _ppzorcqs)) & (*(_plfm7z8g+(_d0i9k0it - 1)) <= _qqhwr930))) | ((_o42699gn == _atfcdrg6) & (*(_5zga5mk9+(_1mfobugm - 1)) == _18fz0zf9)))
					{
						
						_ev4xhht5 = (_ev4xhht5 + (int)1);
						*(_z1ioc3c8+(_ev4xhht5 - 1)) = *(_plfm7z8g+(_d0i9k0it - 1));
						*(_sgbqptwj+(_ev4xhht5 - 1)) = _d0547bi2;//*              The gap for a single block doesn't matter for the later 
						//*              algorithm and is assigned an arbitrary large value 
						
						*(_fwsre11k+(_ev4xhht5 - 1)) = _d0547bi2;
						*(_5zga5mk9+(_ev4xhht5 - 1)) = _18fz0zf9;
						*(_z5l332hf+(_ev4xhht5 - 1)) = (int)1;
						_1mfobugm = (_1mfobugm + (int)1);
					}
					//*           E( IEND ) holds the shift for the initial RRR 
					
					*(_864fslqq+(_9dbezfkf - 1)) = _d0547bi2;
					_d0i9k0it = (_9dbezfkf + (int)1);goto Mark170;
				}
				//* 
				//*        Blocks of size larger than 1x1 
				//* 
				//*        E( IEND ) will hold the shift for the initial RRR, for now set it =0 
				
				*(_864fslqq+(_9dbezfkf - 1)) = _d0547bi2;//* 
				//*        Find local outer bounds GL,GU for the block 
				
				_lr8ennxn = *(_plfm7z8g+(_d0i9k0it - 1));
				_jf74kq7y = *(_plfm7z8g+(_d0i9k0it - 1));
				{
					System.Int32 __81fgg2dlsvn2872 = (System.Int32)(_d0i9k0it);
					const System.Int32 __81fgg2step2872 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2872;
					for (__81fgg2count2872 = System.Math.Max(0, (System.Int32)(((System.Int32)(_9dbezfkf) - __81fgg2dlsvn2872 + __81fgg2step2872) / __81fgg2step2872)), _b5p6od9s = __81fgg2dlsvn2872; __81fgg2count2872 != 0; __81fgg2count2872--, _b5p6od9s += (__81fgg2step2872)) {

					{
						
						_lr8ennxn = ILNumerics.F2NET.Intrinsics.MIN(*(_yisb1mtu+(((int)2 * _b5p6od9s) - (int)1 - 1)) ,_lr8ennxn );
						_jf74kq7y = ILNumerics.F2NET.Intrinsics.MAX(*(_yisb1mtu+((int)2 * _b5p6od9s - 1)) ,_jf74kq7y );
Mark15:;
						// continue
					}
										}				}
				_1v6i5d3q = (_jf74kq7y - _lr8ennxn);// 
				
				if (!(((_o42699gn == _ytsyu76w) & (!(_zzozsldx)))))
				{
					//*           Count the number of eigenvalues in the current block. 
					
					_82ogha0x = (int)0;
					{
						System.Int32 __81fgg2dlsvn2873 = (System.Int32)(_1mfobugm);
						const System.Int32 __81fgg2step2873 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2873;
						for (__81fgg2count2873 = System.Math.Max(0, (System.Int32)(((System.Int32)(_e9y2lltf) - __81fgg2dlsvn2873 + __81fgg2step2873) / __81fgg2step2873)), _b5p6od9s = __81fgg2dlsvn2873; __81fgg2count2873 != 0; __81fgg2count2873--, _b5p6od9s += (__81fgg2step2873)) {

						{
							
							if (*(_5zga5mk9+(_b5p6od9s - 1)) == _18fz0zf9)
							{
								
								_82ogha0x = (_82ogha0x + (int)1);
							}
							else
							{
								goto Mark21;
							}
							
Mark20:;
							// continue
						}
												}					}
Mark21:;
					// continue// 
					
					if (_82ogha0x == (int)0)
					{
						//*              No eigenvalue in the current block lies in the desired range 
						//*              E( IEND ) holds the shift for the initial RRR 
						
						*(_864fslqq+(_9dbezfkf - 1)) = _d0547bi2;
						_d0i9k0it = (_9dbezfkf + (int)1);goto Mark170;
					}
					else
					{
						// 
						//*              Decide whether dqds or bisection is more efficient 
						
						_4xdd1mqo = ((_82ogha0x > (_g0jdw6pt * _oxr7eu3o)) & (!(_zzozsldx)));
						_xn5je7et = ((_1mfobugm + _82ogha0x) - (int)1);//*              Calculate gaps for the current block 
						//*              In later stages, when representations for individual 
						//*              eigenvalues are different, we use SIGMA = E( IEND ). 
						
						_91a1vq5f = _d0547bi2;
						{
							System.Int32 __81fgg2dlsvn2874 = (System.Int32)(_1mfobugm);
							const System.Int32 __81fgg2step2874 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2874;
							for (__81fgg2count2874 = System.Math.Max(0, (System.Int32)(((System.Int32)(_xn5je7et - (int)1) - __81fgg2dlsvn2874 + __81fgg2step2874) / __81fgg2step2874)), _b5p6od9s = __81fgg2dlsvn2874; __81fgg2count2874 != 0; __81fgg2count2874--, _b5p6od9s += (__81fgg2step2874)) {

							{
								
								*(_fwsre11k+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.MAX(_d0547bi2 ,(*(_z1ioc3c8+(_b5p6od9s + (int)1 - 1)) - *(_sgbqptwj+(_b5p6od9s + (int)1 - 1))) - (*(_z1ioc3c8+(_b5p6od9s - 1)) + *(_sgbqptwj+(_b5p6od9s - 1))) );
Mark30:;
								// continue
							}
														}						}
						*(_fwsre11k+(_xn5je7et - 1)) = ILNumerics.F2NET.Intrinsics.MAX(_d0547bi2 ,(_qqhwr930 - _91a1vq5f) - (*(_z1ioc3c8+(_xn5je7et - 1)) + *(_sgbqptwj+(_xn5je7et - 1))) );//*              Find local index of the first and last desired evalue. 
						
						_9lx59bxm = *(_z5l332hf+(_1mfobugm - 1));
						_iam5kd9q = *(_z5l332hf+(_xn5je7et - 1));
					}
					
				}
				
				if (((_o42699gn == _ytsyu76w) & (!(_zzozsldx))) | _4xdd1mqo)
				{
					//*           Case of DQDS 
					//*           Find approximations to the extremal eigenvalues of the block 
					
					_kiaa6aih(ref _oxr7eu3o ,ref Unsafe.AsRef((int)1) ,ref _lr8ennxn ,ref _jf74kq7y ,(_plfm7z8g+(_d0i9k0it - 1)),(_0maek8rz+(_d0i9k0it - 1)),ref _3aphllyg ,ref _gczvoh2l ,ref _2qcyvkhx ,ref _c0o9kuh7 ,ref _itfnbz60 );
					if (_itfnbz60 != (int)0)
					{
						
						_gro5yvfo = (int)-1;
						return;
					}
					
					_v9lvno0t = ILNumerics.F2NET.Intrinsics.MAX(_lr8ennxn ,(_2qcyvkhx - _c0o9kuh7) - ((_cu7spn6z * _p1iqarg6) * ILNumerics.F2NET.Intrinsics.ABS(_2qcyvkhx - _c0o9kuh7 )) );// 
					
					_kiaa6aih(ref _oxr7eu3o ,ref _oxr7eu3o ,ref _lr8ennxn ,ref _jf74kq7y ,(_plfm7z8g+(_d0i9k0it - 1)),(_0maek8rz+(_d0i9k0it - 1)),ref _3aphllyg ,ref _gczvoh2l ,ref _2qcyvkhx ,ref _c0o9kuh7 ,ref _itfnbz60 );
					if (_itfnbz60 != (int)0)
					{
						
						_gro5yvfo = (int)-1;
						return;
					}
					
					_0m46cxip = ILNumerics.F2NET.Intrinsics.MIN(_jf74kq7y ,(_2qcyvkhx + _c0o9kuh7) + ((_cu7spn6z * _p1iqarg6) * ILNumerics.F2NET.Intrinsics.ABS(_2qcyvkhx + _c0o9kuh7 )) );//*           Improve the estimate of the spectral diameter 
					
					_1v6i5d3q = (_0m46cxip - _v9lvno0t);
				}
				else
				{
					//*           Case of bisection 
					//*           Find approximations to the wanted extremal eigenvalues 
					
					_v9lvno0t = ILNumerics.F2NET.Intrinsics.MAX(_lr8ennxn ,(*(_z1ioc3c8+(_1mfobugm - 1)) - *(_sgbqptwj+(_1mfobugm - 1))) - ((_cu7spn6z * _p1iqarg6) * ILNumerics.F2NET.Intrinsics.ABS(*(_z1ioc3c8+(_1mfobugm - 1)) - *(_sgbqptwj+(_1mfobugm - 1)) )) );
					_0m46cxip = ILNumerics.F2NET.Intrinsics.MIN(_jf74kq7y ,(*(_z1ioc3c8+(_xn5je7et - 1)) + *(_sgbqptwj+(_xn5je7et - 1))) + ((_cu7spn6z * _p1iqarg6) * ILNumerics.F2NET.Intrinsics.ABS(*(_z1ioc3c8+(_xn5je7et - 1)) + *(_sgbqptwj+(_xn5je7et - 1)) )) );
				}
				// 
				// 
				//*        Decide whether the base representation for the current block 
				//*        L_JBLK D_JBLK L_JBLK^T = T_JBLK - sigma_JBLK I 
				//*        should be on the left or the right end of the current block. 
				//*        The strategy is to shift to the end which is "more populated" 
				//*        Furthermore, decide whether to use DQDS for the computation of 
				//*        the eigenvalue approximations at the end of DLARRE or bisection. 
				//*        dqds is chosen if all eigenvalues are desired or the number of 
				//*        eigenvalues to be computed is large compared to the blocksize. 
				
				if ((_o42699gn == _ytsyu76w) & (!(_zzozsldx)))
				{
					//*           If all the eigenvalues have to be computed, we use dqd 
					
					_4xdd1mqo = true;//*           INDL is the local index of the first eigenvalue to compute 
					
					_9lx59bxm = (int)1;
					_iam5kd9q = _oxr7eu3o;//*           MB =  number of eigenvalues to compute 
					
					_82ogha0x = _oxr7eu3o;
					_xn5je7et = ((_1mfobugm + _82ogha0x) - (int)1);//*           Define 1/4 and 3/4 points of the spectrum 
					
					_fmb4u5ka = (_v9lvno0t + (_lfikici9 * _1v6i5d3q));
					_slkbnmvx = (_0m46cxip - (_lfikici9 * _1v6i5d3q));
				}
				else
				{
					//*           DLARRD has computed IBLOCK and INDEXW for each eigenvalue 
					//*           approximation. 
					//*           choose sigma 
					
					if (_4xdd1mqo)
					{
						
						_fmb4u5ka = (_v9lvno0t + (_lfikici9 * _1v6i5d3q));
						_slkbnmvx = (_0m46cxip - (_lfikici9 * _1v6i5d3q));
					}
					else
					{
						
						_2qcyvkhx = (ILNumerics.F2NET.Intrinsics.MIN(_0m46cxip ,_qqhwr930 ) - ILNumerics.F2NET.Intrinsics.MAX(_v9lvno0t ,_ppzorcqs ));
						_fmb4u5ka = (ILNumerics.F2NET.Intrinsics.MAX(_v9lvno0t ,_ppzorcqs ) + (_lfikici9 * _2qcyvkhx));
						_slkbnmvx = (ILNumerics.F2NET.Intrinsics.MIN(_0m46cxip ,_qqhwr930 ) - (_lfikici9 * _2qcyvkhx));
					}
					
				}
				// 
				//*        Compute the negcount at the 1/4 and 3/4 points 
				
				if (_82ogha0x > (int)1)
				{
					
					_13cm454j("T" ,ref _oxr7eu3o ,ref _fmb4u5ka ,ref _slkbnmvx ,(_plfm7z8g+(_d0i9k0it - 1)),(_864fslqq+(_d0i9k0it - 1)),ref _3aphllyg ,ref _mwbax5ov ,ref _oe84shdy ,ref _ps01envy ,ref _itfnbz60 );
				}
				// 
				
				if (_82ogha0x == (int)1)
				{
					
					_91a1vq5f = _lr8ennxn;
					_6c0ig98c = _kxg5drh2;
				}
				else
				if ((_oe84shdy - _9lx59bxm) >= (_iam5kd9q - _ps01envy))
				{
					
					if ((_o42699gn == _ytsyu76w) & (!(_zzozsldx)))
					{
						
						_91a1vq5f = ILNumerics.F2NET.Intrinsics.MAX(_v9lvno0t ,_lr8ennxn );
					}
					else
					if (_4xdd1mqo)
					{
						//*              use Gerschgorin bound as shift to get pos def matrix 
						//*              for dqds 
						
						_91a1vq5f = _v9lvno0t;
					}
					else
					{
						//*              use approximation of the first desired eigenvalue of the 
						//*              block as shift 
						
						_91a1vq5f = ILNumerics.F2NET.Intrinsics.MAX(_v9lvno0t ,_ppzorcqs );
					}
					
					_6c0ig98c = _kxg5drh2;
				}
				else
				{
					
					if ((_o42699gn == _ytsyu76w) & (!(_zzozsldx)))
					{
						
						_91a1vq5f = ILNumerics.F2NET.Intrinsics.MIN(_0m46cxip ,_jf74kq7y );
					}
					else
					if (_4xdd1mqo)
					{
						//*              use Gerschgorin bound as shift to get neg def matrix 
						//*              for dqds 
						
						_91a1vq5f = _0m46cxip;
					}
					else
					{
						//*              use approximation of the first desired eigenvalue of the 
						//*              block as shift 
						
						_91a1vq5f = ILNumerics.F2NET.Intrinsics.MIN(_0m46cxip ,_qqhwr930 );
					}
					
					_6c0ig98c = (-(_kxg5drh2));
				}
				// 
				// 
				//*        An initial SIGMA has been chosen that will be used for computing 
				//*        T - SIGMA I = L D L^T 
				//*        Define the increment TAU of the shift in case the initial shift 
				//*        needs to be refined to obtain a factorization with not too much 
				//*        element growth. 
				
				if (_4xdd1mqo)
				{
					//*           The initial SIGMA was to the outer end of the spectrum 
					//*           the matrix is definite and we need not retreat. 
					
					_0446f4de = (((_1v6i5d3q * _p1iqarg6) * _dxpq0xkr) + (_5m0mjfxm * _3aphllyg));
					_0446f4de = ILNumerics.F2NET.Intrinsics.MAX(_0446f4de ,(_5m0mjfxm * _p1iqarg6) * ILNumerics.F2NET.Intrinsics.ABS(_91a1vq5f ) );
				}
				else
				{
					
					if (_82ogha0x > (int)1)
					{
						
						_0dzv290j = (((*(_z1ioc3c8+(_xn5je7et - 1)) + *(_sgbqptwj+(_xn5je7et - 1))) - *(_z1ioc3c8+(_1mfobugm - 1))) - *(_sgbqptwj+(_1mfobugm - 1)));
						_ripqcbhm = ILNumerics.F2NET.Intrinsics.ABS(_0dzv290j / ILNumerics.F2NET.Intrinsics.DBLE(_xn5je7et - _1mfobugm ) );
						if (_6c0ig98c == _kxg5drh2)
						{
							
							_0446f4de = (_gbf4169i * ILNumerics.F2NET.Intrinsics.MAX(*(_fwsre11k+(_1mfobugm - 1)) ,_ripqcbhm ));
							_0446f4de = ILNumerics.F2NET.Intrinsics.MAX(_0446f4de ,*(_sgbqptwj+(_1mfobugm - 1)) );
						}
						else
						{
							
							_0446f4de = (_gbf4169i * ILNumerics.F2NET.Intrinsics.MAX(*(_fwsre11k+(_xn5je7et - (int)1 - 1)) ,_ripqcbhm ));
							_0446f4de = ILNumerics.F2NET.Intrinsics.MAX(_0446f4de ,*(_sgbqptwj+(_xn5je7et - 1)) );
						}
						
					}
					else
					{
						
						_0446f4de = *(_sgbqptwj+(_1mfobugm - 1));
					}
					
				}
				//* 
				
				{
					System.Int32 __81fgg2dlsvn2875 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2875 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2875;
					for (__81fgg2count2875 = System.Math.Max(0, (System.Int32)(((System.Int32)(_5tt65im7) - __81fgg2dlsvn2875 + __81fgg2step2875) / __81fgg2step2875)), _xpllja47 = __81fgg2dlsvn2875; __81fgg2count2875 != 0; __81fgg2count2875--, _xpllja47 += (__81fgg2step2875)) {

					{
						//*           Compute L D L^T factorization of tridiagonal matrix T - sigma I. 
						//*           Store D in WORK(1:IN), L in WORK(IN+1:2*IN), and reciprocals of 
						//*           pivots in WORK(2*IN+1:3*IN) 
						
						_bnamisvm = (*(_plfm7z8g+(_d0i9k0it - 1)) - _91a1vq5f);
						*(_apig8meb+((int)1 - 1)) = _bnamisvm;
						_49t2npjg = ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+((int)1 - 1)) );
						_znpjgsef = _d0i9k0it;
						{
							System.Int32 __81fgg2dlsvn2876 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step2876 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2876;
							for (__81fgg2count2876 = System.Math.Max(0, (System.Int32)(((System.Int32)(_oxr7eu3o - (int)1) - __81fgg2dlsvn2876 + __81fgg2step2876) / __81fgg2step2876)), _b5p6od9s = __81fgg2dlsvn2876; __81fgg2count2876 != 0; __81fgg2count2876--, _b5p6od9s += (__81fgg2step2876)) {

							{
								
								*(_apig8meb+(((int)2 * _oxr7eu3o) + _b5p6od9s - 1)) = (_kxg5drh2 / *(_apig8meb+(_b5p6od9s - 1)));
								_2qcyvkhx = (*(_864fslqq+(_znpjgsef - 1)) * *(_apig8meb+(((int)2 * _oxr7eu3o) + _b5p6od9s - 1)));
								*(_apig8meb+(_oxr7eu3o + _b5p6od9s - 1)) = _2qcyvkhx;
								_bnamisvm = ((*(_plfm7z8g+(_znpjgsef + (int)1 - 1)) - _91a1vq5f) - (_2qcyvkhx * *(_864fslqq+(_znpjgsef - 1))));
								*(_apig8meb+(_b5p6od9s + (int)1 - 1)) = _bnamisvm;
								_49t2npjg = ILNumerics.F2NET.Intrinsics.MAX(_49t2npjg ,ILNumerics.F2NET.Intrinsics.ABS(_bnamisvm ) );
								_znpjgsef = (_znpjgsef + (int)1);
Mark70:;
								// continue
							}
														}						}//*           check for element growth 
						
						if (_49t2npjg > (_00l1i2h1 * _1v6i5d3q))
						{
							
							_lmn3lg5j = true;
						}
						else
						{
							
							_lmn3lg5j = false;
						}
						
						if (_4xdd1mqo & (!(_lmn3lg5j)))
						{
							//*              Ensure the definiteness of the representation 
							//*              All entries of D (of L D L^T) must have the same sign 
							
							{
								System.Int32 __81fgg2dlsvn2877 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step2877 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2877;
								for (__81fgg2count2877 = System.Math.Max(0, (System.Int32)(((System.Int32)(_oxr7eu3o) - __81fgg2dlsvn2877 + __81fgg2step2877) / __81fgg2step2877)), _b5p6od9s = __81fgg2dlsvn2877; __81fgg2count2877 != 0; __81fgg2count2877--, _b5p6od9s += (__81fgg2step2877)) {

								{
									
									_2qcyvkhx = (_6c0ig98c * *(_apig8meb+(_b5p6od9s - 1)));
									if (_2qcyvkhx < _d0547bi2)
									_lmn3lg5j = true;
Mark71:;
									// continue
								}
																}							}
						}
						
						if (_lmn3lg5j)
						{
							//*              Note that in the case of IRANGE=ALLRNG, we use the Gerschgorin 
							//*              shift which makes the matrix definite. So we should end up 
							//*              here really only in the case of IRANGE = VALRNG or INDRNG. 
							
							if (_xpllja47 == (_5tt65im7 - (int)1))
							{
								
								if (_6c0ig98c == _kxg5drh2)
								{
									//*                    The fudged Gerschgorin shift should succeed 
									
									_91a1vq5f = ((_lr8ennxn - (((_7fnb0l4r * _1v6i5d3q) * _p1iqarg6) * _dxpq0xkr)) - ((_7fnb0l4r * _5m0mjfxm) * _3aphllyg));
								}
								else
								{
									
									_91a1vq5f = ((_jf74kq7y + (((_7fnb0l4r * _1v6i5d3q) * _p1iqarg6) * _dxpq0xkr)) + ((_7fnb0l4r * _5m0mjfxm) * _3aphllyg));
								}
								
							}
							else
							{
								
								_91a1vq5f = (_91a1vq5f - (_6c0ig98c * _0446f4de));
								_0446f4de = (_5m0mjfxm * _0446f4de);
							}
							
						}
						else
						{
							//*              an initial RRR is found 
							goto Mark83;
						}
						
Mark80:;
						// continue
					}
										}				}//*        if the program reaches this point, no base representation could be 
				//*        found in MAXTRY iterations. 
				
				_gro5yvfo = (int)2;
				return;// 
				
Mark83:;
				// continue//*        At this point, we have found an initial base representation 
				//*        T - SIGMA I = L D L^T with not too much element growth. 
				//*        Store the shift. 
				
				*(_864fslqq+(_9dbezfkf - 1)) = _91a1vq5f;//*        Store D and L. 
				
				_gvjhlct0(ref _oxr7eu3o ,_apig8meb ,ref Unsafe.AsRef((int)1) ,(_plfm7z8g+(_d0i9k0it - 1)),ref Unsafe.AsRef((int)1) );
				_gvjhlct0(ref Unsafe.AsRef(_oxr7eu3o - (int)1) ,(_apig8meb+(_oxr7eu3o + (int)1 - 1)),ref Unsafe.AsRef((int)1) ,(_864fslqq+(_d0i9k0it - 1)),ref Unsafe.AsRef((int)1) );// 
				// 
				
				if (_82ogha0x > (int)1)
				{
					//* 
					//*           Perturb each entry of the base representation by a small 
					//*           (but random) relative amount to overcome difficulties with 
					//*           glued matrices. 
					//* 
					
					{
						System.Int32 __81fgg2dlsvn2878 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2878 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2878;
						for (__81fgg2count2878 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)4) - __81fgg2dlsvn2878 + __81fgg2step2878) / __81fgg2step2878)), _b5p6od9s = __81fgg2dlsvn2878; __81fgg2count2878 != 0; __81fgg2count2878--, _b5p6od9s += (__81fgg2step2878)) {

						{
							
							*(_5c1snrj6+(_b5p6od9s - 1)) = (int)1;
Mark122:;
							// continue
						}
												}					}// 
					
					_dx0mpk8s(ref Unsafe.AsRef((int)2) ,_5c1snrj6 ,ref Unsafe.AsRef(((int)2 * _oxr7eu3o) - (int)1) ,(_apig8meb+((int)1 - 1)));
					{
						System.Int32 __81fgg2dlsvn2879 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2879 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2879;
						for (__81fgg2count2879 = System.Math.Max(0, (System.Int32)(((System.Int32)(_oxr7eu3o - (int)1) - __81fgg2dlsvn2879 + __81fgg2step2879) / __81fgg2step2879)), _b5p6od9s = __81fgg2dlsvn2879; __81fgg2count2879 != 0; __81fgg2count2879--, _b5p6od9s += (__81fgg2step2879)) {

						{
							
							*(_plfm7z8g+((_d0i9k0it + _b5p6od9s) - (int)1 - 1)) = (*(_plfm7z8g+((_d0i9k0it + _b5p6od9s) - (int)1 - 1)) * (_kxg5drh2 + ((_p1iqarg6 * _5yjbn35p) * *(_apig8meb+(_b5p6od9s - 1)))));
							*(_864fslqq+((_d0i9k0it + _b5p6od9s) - (int)1 - 1)) = (*(_864fslqq+((_d0i9k0it + _b5p6od9s) - (int)1 - 1)) * (_kxg5drh2 + ((_p1iqarg6 * _5yjbn35p) * *(_apig8meb+(_oxr7eu3o + _b5p6od9s - 1)))));
Mark125:;
							// continue
						}
												}					}
					*(_plfm7z8g+(_9dbezfkf - 1)) = (*(_plfm7z8g+(_9dbezfkf - 1)) * (_kxg5drh2 + ((_p1iqarg6 * _ax5ijvbx) * *(_apig8meb+(_oxr7eu3o - 1)))));//* 
					
				}
				//* 
				//*        Don't update the Gerschgorin intervals because keeping track 
				//*        of the updates would be too much work in DLARRV. 
				//*        We update W instead and use it to locate the proper Gerschgorin 
				//*        intervals. 
				// 
				//*        Compute the required eigenvalues of L D L' by bisection or dqds 
				
				if (!(_4xdd1mqo))
				{
					//*           If DLARRD has been used, shift the eigenvalue approximations 
					//*           according to their representation. This is necessary for 
					//*           a uniform DLARRV since dqds computes eigenvalues of the 
					//*           shifted representation. In DLARRV, W will always hold the 
					//*           UNshifted eigenvalue approximation. 
					
					{
						System.Int32 __81fgg2dlsvn2880 = (System.Int32)(_1mfobugm);
						const System.Int32 __81fgg2step2880 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2880;
						for (__81fgg2count2880 = System.Math.Max(0, (System.Int32)(((System.Int32)(_xn5je7et) - __81fgg2dlsvn2880 + __81fgg2step2880) / __81fgg2step2880)), _znpjgsef = __81fgg2dlsvn2880; __81fgg2count2880 != 0; __81fgg2count2880--, _znpjgsef += (__81fgg2step2880)) {

						{
							
							*(_z1ioc3c8+(_znpjgsef - 1)) = (*(_z1ioc3c8+(_znpjgsef - 1)) - _91a1vq5f);
							*(_sgbqptwj+(_znpjgsef - 1)) = (*(_sgbqptwj+(_znpjgsef - 1)) + (ILNumerics.F2NET.Intrinsics.ABS(*(_z1ioc3c8+(_znpjgsef - 1)) ) * _p1iqarg6));
Mark134:;
							// continue
						}
												}					}//*           call DLARRB to reduce eigenvalue error of the approximations 
					//*           from DLARRD 
					
					{
						System.Int32 __81fgg2dlsvn2881 = (System.Int32)(_d0i9k0it);
						const System.Int32 __81fgg2step2881 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2881;
						for (__81fgg2count2881 = System.Math.Max(0, (System.Int32)(((System.Int32)(_9dbezfkf - (int)1) - __81fgg2dlsvn2881 + __81fgg2step2881) / __81fgg2step2881)), _b5p6od9s = __81fgg2dlsvn2881; __81fgg2count2881 != 0; __81fgg2count2881--, _b5p6od9s += (__81fgg2step2881)) {

						{
							
							*(_apig8meb+(_b5p6od9s - 1)) = (*(_plfm7z8g+(_b5p6od9s - 1)) * __POW2(*(_864fslqq+(_b5p6od9s - 1))));
Mark135:;
							// continue
						}
												}					}//*           use bisection to find EV from INDL to INDU 
					
					_0vn6dsl0(ref _oxr7eu3o ,(_plfm7z8g+(_d0i9k0it - 1)),(_apig8meb+(_d0i9k0it - 1)),ref _9lx59bxm ,ref _iam5kd9q ,ref _ndrkejw5 ,ref _nmnmq6ye ,ref Unsafe.AsRef(_9lx59bxm - (int)1) ,(_z1ioc3c8+(_1mfobugm - 1)),(_fwsre11k+(_1mfobugm - 1)),(_sgbqptwj+(_1mfobugm - 1)),(_apig8meb+(((int)2 * _dxpq0xkr) + (int)1 - 1)),_4b6rt45i ,ref _3aphllyg ,ref _1v6i5d3q ,ref _oxr7eu3o ,ref _itfnbz60 );
					if (_itfnbz60 != (int)0)
					{
						
						_gro5yvfo = (int)-4;
						return;
					}
					//*           DLARRB computes all gaps correctly except for the last one 
					//*           Record distance to VU/GU 
					
					*(_fwsre11k+(_xn5je7et - 1)) = ILNumerics.F2NET.Intrinsics.MAX(_d0547bi2 ,(_qqhwr930 - _91a1vq5f) - (*(_z1ioc3c8+(_xn5je7et - 1)) + *(_sgbqptwj+(_xn5je7et - 1))) );
					{
						System.Int32 __81fgg2dlsvn2882 = (System.Int32)(_9lx59bxm);
						const System.Int32 __81fgg2step2882 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2882;
						for (__81fgg2count2882 = System.Math.Max(0, (System.Int32)(((System.Int32)(_iam5kd9q) - __81fgg2dlsvn2882 + __81fgg2step2882) / __81fgg2step2882)), _b5p6od9s = __81fgg2dlsvn2882; __81fgg2count2882 != 0; __81fgg2count2882--, _b5p6od9s += (__81fgg2step2882)) {

						{
							
							_ev4xhht5 = (_ev4xhht5 + (int)1);
							*(_5zga5mk9+(_ev4xhht5 - 1)) = _18fz0zf9;
							*(_z5l332hf+(_ev4xhht5 - 1)) = _b5p6od9s;
Mark138:;
							// continue
						}
												}					}
				}
				else
				{
					//*           Call dqds to get all eigs (and then possibly delete unwanted 
					//*           eigenvalues). 
					//*           Note that dqds finds the eigenvalues of the L D L^T representation 
					//*           of T to high relative accuracy. High relative accuracy 
					//*           might be lost when the shift of the RRR is subtracted to obtain 
					//*           the eigenvalues of T. However, T is not guaranteed to define its 
					//*           eigenvalues to high relative accuracy anyway. 
					//*           Set RTOL to the order of the tolerance used in DLASQ2 
					//*           This is an ESTIMATED error, the worst case bound is 4*N*EPS 
					//*           which is usually too large and requires unnecessary work to be 
					//*           done by bisection when computing the eigenvectors 
					
					_9mzm56bw = ((ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.DBLE(_oxr7eu3o ) ) * _ax5ijvbx) * _p1iqarg6);
					_znpjgsef = _d0i9k0it;
					{
						System.Int32 __81fgg2dlsvn2883 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2883 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2883;
						for (__81fgg2count2883 = System.Math.Max(0, (System.Int32)(((System.Int32)(_oxr7eu3o - (int)1) - __81fgg2dlsvn2883 + __81fgg2step2883) / __81fgg2step2883)), _b5p6od9s = __81fgg2dlsvn2883; __81fgg2count2883 != 0; __81fgg2count2883--, _b5p6od9s += (__81fgg2step2883)) {

						{
							
							*(_apig8meb+(((int)2 * _b5p6od9s) - (int)1 - 1)) = ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_znpjgsef - 1)) );
							*(_apig8meb+((int)2 * _b5p6od9s - 1)) = ((*(_864fslqq+(_znpjgsef - 1)) * *(_864fslqq+(_znpjgsef - 1))) * *(_apig8meb+(((int)2 * _b5p6od9s) - (int)1 - 1)));
							_znpjgsef = (_znpjgsef + (int)1);
Mark140:;
							// continue
						}
												}					}
					*(_apig8meb+(((int)2 * _oxr7eu3o) - (int)1 - 1)) = ILNumerics.F2NET.Intrinsics.ABS(*(_plfm7z8g+(_9dbezfkf - 1)) );
					*(_apig8meb+((int)2 * _oxr7eu3o - 1)) = _d0547bi2;
					_i4v7i21v(ref _oxr7eu3o ,_apig8meb ,ref _itfnbz60 );
					if (_itfnbz60 != (int)0)
					{
						//*              If IINFO = -5 then an index is part of a tight cluster 
						//*              and should be changed. The index is in IWORK(1) and the 
						//*              gap is in WORK(N+1) 
						
						_gro5yvfo = (int)-5;
						return;
					}
					else
					{
						//*              Test that all eigenvalues are positive as expected 
						
						{
							System.Int32 __81fgg2dlsvn2884 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step2884 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2884;
							for (__81fgg2count2884 = System.Math.Max(0, (System.Int32)(((System.Int32)(_oxr7eu3o) - __81fgg2dlsvn2884 + __81fgg2step2884) / __81fgg2step2884)), _b5p6od9s = __81fgg2dlsvn2884; __81fgg2count2884 != 0; __81fgg2count2884--, _b5p6od9s += (__81fgg2step2884)) {

							{
								
								if (*(_apig8meb+(_b5p6od9s - 1)) < _d0547bi2)
								{
									
									_gro5yvfo = (int)-6;
									return;
								}
								
Mark149:;
								// continue
							}
														}						}
					}
					
					if (_6c0ig98c > _d0547bi2)
					{
						
						{
							System.Int32 __81fgg2dlsvn2885 = (System.Int32)(_9lx59bxm);
							const System.Int32 __81fgg2step2885 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2885;
							for (__81fgg2count2885 = System.Math.Max(0, (System.Int32)(((System.Int32)(_iam5kd9q) - __81fgg2dlsvn2885 + __81fgg2step2885) / __81fgg2step2885)), _b5p6od9s = __81fgg2dlsvn2885; __81fgg2count2885 != 0; __81fgg2count2885--, _b5p6od9s += (__81fgg2step2885)) {

							{
								
								_ev4xhht5 = (_ev4xhht5 + (int)1);
								*(_z1ioc3c8+(_ev4xhht5 - 1)) = *(_apig8meb+((_oxr7eu3o - _b5p6od9s) + (int)1 - 1));
								*(_5zga5mk9+(_ev4xhht5 - 1)) = _18fz0zf9;
								*(_z5l332hf+(_ev4xhht5 - 1)) = _b5p6od9s;
Mark150:;
								// continue
							}
														}						}
					}
					else
					{
						
						{
							System.Int32 __81fgg2dlsvn2886 = (System.Int32)(_9lx59bxm);
							const System.Int32 __81fgg2step2886 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2886;
							for (__81fgg2count2886 = System.Math.Max(0, (System.Int32)(((System.Int32)(_iam5kd9q) - __81fgg2dlsvn2886 + __81fgg2step2886) / __81fgg2step2886)), _b5p6od9s = __81fgg2dlsvn2886; __81fgg2count2886 != 0; __81fgg2count2886--, _b5p6od9s += (__81fgg2step2886)) {

							{
								
								_ev4xhht5 = (_ev4xhht5 + (int)1);
								*(_z1ioc3c8+(_ev4xhht5 - 1)) = (-(*(_apig8meb+(_b5p6od9s - 1))));
								*(_5zga5mk9+(_ev4xhht5 - 1)) = _18fz0zf9;
								*(_z5l332hf+(_ev4xhht5 - 1)) = _b5p6od9s;
Mark160:;
								// continue
							}
														}						}
					}
					// 
					
					{
						System.Int32 __81fgg2dlsvn2887 = (System.Int32)(((_ev4xhht5 - _82ogha0x) + (int)1));
						const System.Int32 __81fgg2step2887 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2887;
						for (__81fgg2count2887 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn2887 + __81fgg2step2887) / __81fgg2step2887)), _b5p6od9s = __81fgg2dlsvn2887; __81fgg2count2887 != 0; __81fgg2count2887--, _b5p6od9s += (__81fgg2step2887)) {

						{
							//*              the value of RTOL below should be the tolerance in DLASQ2 
							
							*(_sgbqptwj+(_b5p6od9s - 1)) = (_9mzm56bw * ILNumerics.F2NET.Intrinsics.ABS(*(_z1ioc3c8+(_b5p6od9s - 1)) ));
Mark165:;
							// continue
						}
												}					}
					{
						System.Int32 __81fgg2dlsvn2888 = (System.Int32)(((_ev4xhht5 - _82ogha0x) + (int)1));
						const System.Int32 __81fgg2step2888 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2888;
						for (__81fgg2count2888 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5 - (int)1) - __81fgg2dlsvn2888 + __81fgg2step2888) / __81fgg2step2888)), _b5p6od9s = __81fgg2dlsvn2888; __81fgg2count2888 != 0; __81fgg2count2888--, _b5p6od9s += (__81fgg2step2888)) {

						{
							//*              compute the right gap between the intervals 
							
							*(_fwsre11k+(_b5p6od9s - 1)) = ILNumerics.F2NET.Intrinsics.MAX(_d0547bi2 ,(*(_z1ioc3c8+(_b5p6od9s + (int)1 - 1)) - *(_sgbqptwj+(_b5p6od9s + (int)1 - 1))) - (*(_z1ioc3c8+(_b5p6od9s - 1)) + *(_sgbqptwj+(_b5p6od9s - 1))) );
Mark166:;
							// continue
						}
												}					}
					*(_fwsre11k+(_ev4xhht5 - 1)) = ILNumerics.F2NET.Intrinsics.MAX(_d0547bi2 ,(_qqhwr930 - _91a1vq5f) - (*(_z1ioc3c8+(_ev4xhht5 - 1)) + *(_sgbqptwj+(_ev4xhht5 - 1))) );
				}
				//*        proceed with next block 
				
				_d0i9k0it = (_9dbezfkf + (int)1);
				_1mfobugm = (_xn5je7et + (int)1);
Mark170:;
				// continue
			}
						}		}//* 
		// 
		
		return;//* 
		//*     end of DLARRE 
		//* 
		
	}
	
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
