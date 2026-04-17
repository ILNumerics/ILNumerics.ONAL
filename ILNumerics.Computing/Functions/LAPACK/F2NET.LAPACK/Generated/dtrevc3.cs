
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
//*> \brief \b DTREVC3 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DTREVC3 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dtrevc3.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dtrevc3.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dtrevc3.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DTREVC3( SIDE, HOWMNY, SELECT, N, T, LDT, VL, LDVL, 
//*                           VR, LDVR, MM, M, WORK, LWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          HOWMNY, SIDE 
//*       INTEGER            INFO, LDT, LDVL, LDVR, LWORK, M, MM, N 
//*       .. 
//*       .. Array Arguments .. 
//*       LOGICAL            SELECT( * ) 
//*       DOUBLE PRECISION   T( LDT, * ), VL( LDVL, * ), VR( LDVR, * ), 
//*      $                   WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DTREVC3 computes some or all of the right and/or left eigenvectors of 
//*> a real upper quasi-triangular matrix T. 
//*> Matrices of this type are produced by the Schur factorization of 
//*> a real general matrix:  A = Q*T*Q**T, as computed by DHSEQR. 
//*> 
//*> The right eigenvector x and the left eigenvector y of T corresponding 
//*> to an eigenvalue w are defined by: 
//*> 
//*>    T*x = w*x,     (y**T)*T = w*(y**T) 
//*> 
//*> where y**T denotes the transpose of the vector y. 
//*> The eigenvalues are not input to this routine, but are read directly 
//*> from the diagonal blocks of T. 
//*> 
//*> This routine returns the matrices X and/or Y of right and left 
//*> eigenvectors of T, or the products Q*X and/or Q*Y, where Q is an 
//*> input matrix. If Q is the orthogonal factor that reduces a matrix 
//*> A to Schur form T, then Q*X and Q*Y are the matrices of right and 
//*> left eigenvectors of A. 
//*> 
//*> This uses a Level 3 BLAS version of the back transformation. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] SIDE 
//*> \verbatim 
//*>          SIDE is CHARACTER*1 
//*>          = 'R':  compute right eigenvectors only; 
//*>          = 'L':  compute left eigenvectors only; 
//*>          = 'B':  compute both right and left eigenvectors. 
//*> \endverbatim 
//*> 
//*> \param[in] HOWMNY 
//*> \verbatim 
//*>          HOWMNY is CHARACTER*1 
//*>          = 'A':  compute all right and/or left eigenvectors; 
//*>          = 'B':  compute all right and/or left eigenvectors, 
//*>                  backtransformed by the matrices in VR and/or VL; 
//*>          = 'S':  compute selected right and/or left eigenvectors, 
//*>                  as indicated by the logical array SELECT. 
//*> \endverbatim 
//*> 
//*> \param[in,out] SELECT 
//*> \verbatim 
//*>          SELECT is LOGICAL array, dimension (N) 
//*>          If HOWMNY = 'S', SELECT specifies the eigenvectors to be 
//*>          computed. 
//*>          If w(j) is a real eigenvalue, the corresponding real 
//*>          eigenvector is computed if SELECT(j) is .TRUE.. 
//*>          If w(j) and w(j+1) are the real and imaginary parts of a 
//*>          complex eigenvalue, the corresponding complex eigenvector is 
//*>          computed if either SELECT(j) or SELECT(j+1) is .TRUE., and 
//*>          on exit SELECT(j) is set to .TRUE. and SELECT(j+1) is set to 
//*>          .FALSE.. 
//*>          Not referenced if HOWMNY = 'A' or 'B'. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix T. N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] T 
//*> \verbatim 
//*>          T is DOUBLE PRECISION array, dimension (LDT,N) 
//*>          The upper quasi-triangular matrix T in Schur canonical form. 
//*> \endverbatim 
//*> 
//*> \param[in] LDT 
//*> \verbatim 
//*>          LDT is INTEGER 
//*>          The leading dimension of the array T. LDT >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[in,out] VL 
//*> \verbatim 
//*>          VL is DOUBLE PRECISION array, dimension (LDVL,MM) 
//*>          On entry, if SIDE = 'L' or 'B' and HOWMNY = 'B', VL must 
//*>          contain an N-by-N matrix Q (usually the orthogonal matrix Q 
//*>          of Schur vectors returned by DHSEQR). 
//*>          On exit, if SIDE = 'L' or 'B', VL contains: 
//*>          if HOWMNY = 'A', the matrix Y of left eigenvectors of T; 
//*>          if HOWMNY = 'B', the matrix Q*Y; 
//*>          if HOWMNY = 'S', the left eigenvectors of T specified by 
//*>                           SELECT, stored consecutively in the columns 
//*>                           of VL, in the same order as their 
//*>                           eigenvalues. 
//*>          A complex eigenvector corresponding to a complex eigenvalue 
//*>          is stored in two consecutive columns, the first holding the 
//*>          real part, and the second the imaginary part. 
//*>          Not referenced if SIDE = 'R'. 
//*> \endverbatim 
//*> 
//*> \param[in] LDVL 
//*> \verbatim 
//*>          LDVL is INTEGER 
//*>          The leading dimension of the array VL. 
//*>          LDVL >= 1, and if SIDE = 'L' or 'B', LDVL >= N. 
//*> \endverbatim 
//*> 
//*> \param[in,out] VR 
//*> \verbatim 
//*>          VR is DOUBLE PRECISION array, dimension (LDVR,MM) 
//*>          On entry, if SIDE = 'R' or 'B' and HOWMNY = 'B', VR must 
//*>          contain an N-by-N matrix Q (usually the orthogonal matrix Q 
//*>          of Schur vectors returned by DHSEQR). 
//*>          On exit, if SIDE = 'R' or 'B', VR contains: 
//*>          if HOWMNY = 'A', the matrix X of right eigenvectors of T; 
//*>          if HOWMNY = 'B', the matrix Q*X; 
//*>          if HOWMNY = 'S', the right eigenvectors of T specified by 
//*>                           SELECT, stored consecutively in the columns 
//*>                           of VR, in the same order as their 
//*>                           eigenvalues. 
//*>          A complex eigenvector corresponding to a complex eigenvalue 
//*>          is stored in two consecutive columns, the first holding the 
//*>          real part and the second the imaginary part. 
//*>          Not referenced if SIDE = 'L'. 
//*> \endverbatim 
//*> 
//*> \param[in] LDVR 
//*> \verbatim 
//*>          LDVR is INTEGER 
//*>          The leading dimension of the array VR. 
//*>          LDVR >= 1, and if SIDE = 'R' or 'B', LDVR >= N. 
//*> \endverbatim 
//*> 
//*> \param[in] MM 
//*> \verbatim 
//*>          MM is INTEGER 
//*>          The number of columns in the arrays VL and/or VR. MM >= M. 
//*> \endverbatim 
//*> 
//*> \param[out] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The number of columns in the arrays VL and/or VR actually 
//*>          used to store the eigenvectors. 
//*>          If HOWMNY = 'A' or 'B', M is set to N. 
//*>          Each selected real eigenvector occupies one column and each 
//*>          selected complex eigenvector occupies two columns. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is DOUBLE PRECISION array, dimension (MAX(1,LWORK)) 
//*> \endverbatim 
//*> 
//*> \param[in] LWORK 
//*> \verbatim 
//*>          LWORK is INTEGER 
//*>          The dimension of array WORK. LWORK >= max(1,3*N). 
//*>          For optimum performance, LWORK >= N + 2*N*NB, where NB is 
//*>          the optimal blocksize. 
//*> 
//*>          If LWORK = -1, then a workspace query is assumed; the routine 
//*>          only calculates the optimal size of the WORK array, returns 
//*>          this value as the first entry of the WORK array, and no error 
//*>          message related to LWORK is issued by XERBLA. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value 
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
//*> \date November 2017 
//* 
//*  @precisions fortran d -> s 
//* 
//*> \ingroup doubleOTHERcomputational 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  The algorithm used in this program is basically backward (forward) 
//*>  substitution, with scaling to make the the code robust against 
//*>  possible overflow. 
//*> 
//*>  Each eigenvector is normalized so that the element of largest 
//*>  magnitude has magnitude 1; here the magnitude of a complex number 
//*>  (x,y) is taken to be |x| + |y|. 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _gcmqo8xs(FString _m2cn2gjg, FString _beyjxzyr, Boolean* _2vi7x6ig, ref Int32 _dxpq0xkr, Double* _2ivtt43r, ref Int32 _w8yhbr2r, Double* _ppzorcqs, ref Int32 _uq25zlw0, Double* _b88wiuwq, ref Int32 _oxoory3e, ref Int32 _e9y2lltf, ref Int32 _ev4xhht5, Double* _apig8meb, ref Int32 _6fnxzlyp, ref Int32 _gro5yvfo)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)544 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Int32 _o80jnixx =  (int)8;
Int32 _blnc1nox =  (int)128;
Boolean _lqnhcvuj =  default;
Boolean _4oa6aiuq =  default;
Boolean _35js00fx =  default;
Boolean _lhlgm7z5 =  default;
Boolean _htf5ro8d =  default;
Boolean _94xnzecl =  default;
Boolean _yj0w80gr =  default;
Boolean _y66g69o8 =  default;
Int32 _b5p6od9s =  default;
Int32 _bhsiylw4 =  default;
Int32 _retbwjxi =  default;
Int32 _8t9w2q8d =  default;
Int32 _5kucxo3c =  default;
Int32 _znpjgsef =  default;
Int32 _dk3nh7xl =  default;
Int32 _psodi8to =  default;
Int32 _olmbz2a3 =  default;
Int32 _umlkckdg =  default;
Int32 _1ub95eoc =  default;
Int32 _uicet2a7 =  default;
Int32 _tafa1evd =  default;
Int32 _f7059815 =  default;
Int32 _sjhh7htz =  default;
Double _bafcbx97 =  default;
Double _av7j8yda =  default;
Double _trz23puj =  default;
Double _myhemoui =  default;
Double _egqfb6nh =  default;
Double _343rc715 =  default;
Double _1m44vtuk =  default;
Double _rhnpgpoi =  default;
Double _bogm0gwy =  default;
Double _0h4yb5wu =  default;
Double _zfw72syv =  default;
Double _69yw1gah =  default;
Double _2wzbu6qf =  default;
Double _nc0qphpn =  default;
Double _b5j6m2b7 =  default;
Double _ziu6urj2 =  default;
Double* _ta7zuy9k =  (Double*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Double) * ((int)2)*((int)2);
Int32* _x49mm45v =  (Int32*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Int32) * ((int)128);
string fLanavab = default;
#endregion  variable declarations
_m2cn2gjg = _m2cn2gjg.Convert(1);
_beyjxzyr = _beyjxzyr.Convert(1);

	{
		//* 
		//*  -- LAPACK computational routine (version 3.8.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     November 2017 
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
		//*     .. Local Arrays .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Decode and test the input parameters 
		//* 
		
		_4oa6aiuq = _w8y2rzgy(_m2cn2gjg ,"B" );
		_yj0w80gr = (_w8y2rzgy(_m2cn2gjg ,"R" ) | _4oa6aiuq);
		_35js00fx = (_w8y2rzgy(_m2cn2gjg ,"L" ) | _4oa6aiuq);//* 
		
		_lqnhcvuj = _w8y2rzgy(_beyjxzyr ,"A" );
		_htf5ro8d = _w8y2rzgy(_beyjxzyr ,"B" );
		_y66g69o8 = _w8y2rzgy(_beyjxzyr ,"S" );//* 
		
		_gro5yvfo = (int)0;
		_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"DTREVC" ,_m2cn2gjg + _beyjxzyr ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) );
		_tafa1evd = (_dxpq0xkr + (((int)2 * _dxpq0xkr) * _f7059815));
		*(_apig8meb+((int)1 - 1)) = DBLE(_tafa1evd);
		_lhlgm7z5 = (_6fnxzlyp == (int)-1);
		if ((!(_yj0w80gr)) & (!(_35js00fx)))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (((!(_lqnhcvuj)) & (!(_htf5ro8d))) & (!(_y66g69o8)))
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if (_w8yhbr2r < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)-6;
		}
		else
		if ((_uq25zlw0 < (int)1) | (_35js00fx & (_uq25zlw0 < _dxpq0xkr)))
		{
			
			_gro5yvfo = (int)-8;
		}
		else
		if ((_oxoory3e < (int)1) | (_yj0w80gr & (_oxoory3e < _dxpq0xkr)))
		{
			
			_gro5yvfo = (int)-10;
		}
		else
		if ((_6fnxzlyp < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,(int)3 * _dxpq0xkr )) & (!(_lhlgm7z5)))
		{
			
			_gro5yvfo = (int)-14;
		}
		else
		{
			//* 
			//*        Set M to the number of columns required to store the selected 
			//*        eigenvectors, standardize the array SELECT if necessary, and 
			//*        test MM. 
			//* 
			
			if (_y66g69o8)
			{
				
				_ev4xhht5 = (int)0;
				_94xnzecl = false;
				{
					System.Int32 __81fgg2dlsvn2286 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2286 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2286;
					for (__81fgg2count2286 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2286 + __81fgg2step2286) / __81fgg2step2286)), _znpjgsef = __81fgg2dlsvn2286; __81fgg2count2286 != 0; __81fgg2count2286--, _znpjgsef += (__81fgg2step2286)) {

					{
						
						if (_94xnzecl)
						{
							
							_94xnzecl = false;
							*(_2vi7x6ig+(_znpjgsef - 1)) = false;
						}
						else
						{
							
							if (_znpjgsef < _dxpq0xkr)
							{
								
								if (*(_2ivtt43r+(_znpjgsef + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)) == _d0547bi2)
								{
									
									if (*(_2vi7x6ig+(_znpjgsef - 1)))
									_ev4xhht5 = (_ev4xhht5 + (int)1);
								}
								else
								{
									
									_94xnzecl = true;
									if (*(_2vi7x6ig+(_znpjgsef - 1)) | *(_2vi7x6ig+(_znpjgsef + (int)1 - 1)))
									{
										
										*(_2vi7x6ig+(_znpjgsef - 1)) = true;
										_ev4xhht5 = (_ev4xhht5 + (int)2);
									}
									
								}
								
							}
							else
							{
								
								if (*(_2vi7x6ig+(_dxpq0xkr - 1)))
								_ev4xhht5 = (_ev4xhht5 + (int)1);
							}
							
						}
						
Mark10:;
						// continue
					}
										}				}
			}
			else
			{
				
				_ev4xhht5 = _dxpq0xkr;
			}
			//* 
			
			if (_e9y2lltf < _ev4xhht5)
			{
				
				_gro5yvfo = (int)-11;
			}
			
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("DTREVC3" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		else
		if (_lhlgm7z5)
		{
			
			return;
		}
		//* 
		//*     Quick return if possible. 
		//* 
		
		if (_dxpq0xkr == (int)0)
		return;//* 
		//*     Use blocked version of back-transformation if sufficient workspace. 
		//*     Zero-out the workspace to avoid potential NaN propagation. 
		//* 
		
		if (_htf5ro8d & (_6fnxzlyp >= (_dxpq0xkr + (((int)2 * _dxpq0xkr) * _o80jnixx))))
		{
			
			_f7059815 = ((_6fnxzlyp - _dxpq0xkr) / ((int)2 * _dxpq0xkr));
			_f7059815 = ILNumerics.F2NET.Intrinsics.MIN(_f7059815 ,_blnc1nox );
			_rta9tuwm("F" ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1 + ((int)2 * _f7059815)) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,_apig8meb ,ref _dxpq0xkr );
		}
		else
		{
			
			_f7059815 = (int)1;
		}
		//* 
		//*     Set the constants to control overflow. 
		//* 
		
		_zfw72syv = _f43eg0w0("Safe minimum" );
		_myhemoui = (_kxg5drh2 / _zfw72syv);
		_to4dtyqc(ref _zfw72syv ,ref _myhemoui );
		_0h4yb5wu = _f43eg0w0("Precision" );
		_bogm0gwy = (_zfw72syv * (_dxpq0xkr / _0h4yb5wu));
		_av7j8yda = ((_kxg5drh2 - _0h4yb5wu) / _bogm0gwy);//* 
		//*     Compute 1-norm of each column of strictly upper triangular 
		//*     part of T to control overflow in triangular solver. 
		//* 
		
		*(_apig8meb+((int)1 - 1)) = _d0547bi2;
		{
			System.Int32 __81fgg2dlsvn2287 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step2287 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2287;
			for (__81fgg2count2287 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2287 + __81fgg2step2287) / __81fgg2step2287)), _znpjgsef = __81fgg2dlsvn2287; __81fgg2count2287 != 0; __81fgg2count2287--, _znpjgsef += (__81fgg2step2287)) {

			{
				
				*(_apig8meb+(_znpjgsef - 1)) = _d0547bi2;
				{
					System.Int32 __81fgg2dlsvn2288 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2288 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2288;
					for (__81fgg2count2288 = System.Math.Max(0, (System.Int32)(((System.Int32)(_znpjgsef - (int)1) - __81fgg2dlsvn2288 + __81fgg2step2288) / __81fgg2step2288)), _b5p6od9s = __81fgg2dlsvn2288; __81fgg2count2288 != 0; __81fgg2count2288--, _b5p6od9s += (__81fgg2step2288)) {

					{
						
						*(_apig8meb+(_znpjgsef - 1)) = (*(_apig8meb+(_znpjgsef - 1)) + ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+(_b5p6od9s - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)) ));
Mark20:;
						// continue
					}
										}				}
Mark30:;
				// continue
			}
						}		}//* 
		//*     Index IP is used to specify the real or complex eigenvalue: 
		//*       IP = 0, real eigenvalue, 
		//*            1, first  of conjugate complex pair: (wr,wi) 
		//*           -1, second of conjugate complex pair: (wr,wi) 
		//*       ISCOMPLEX array stores IP for each column in current block. 
		//* 
		
		if (_yj0w80gr)
		{
			//* 
			//*        ============================================================ 
			//*        Compute right eigenvectors. 
			//* 
			//*        IV is index of column in current block. 
			//*        For complex right vector, uses IV-1 for real part and IV for complex part. 
			//*        Non-blocked version always uses IV=2; 
			//*        blocked     version starts with IV=NB, goes down to 1 or 2. 
			//*        (Note the "0-th" column is used for 1-norms computed above.) 
			
			_uicet2a7 = (int)2;
			if (_f7059815 > (int)2)
			{
				
				_uicet2a7 = _f7059815;
			}
			// 
			
			_8t9w2q8d = (int)0;
			_5kucxo3c = _ev4xhht5;
			{
				System.Int32 __81fgg2dlsvn2289 = (System.Int32)(_dxpq0xkr);
				System.Int32 __81fgg2step2289 = (System.Int32)((int)-1);
				System.Int32 __81fgg2count2289;
				for (__81fgg2count2289 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn2289 + __81fgg2step2289) / __81fgg2step2289)), _1ub95eoc = __81fgg2dlsvn2289; __81fgg2count2289 != 0; __81fgg2count2289--, _1ub95eoc += (__81fgg2step2289)) {

				{
					
					if (_8t9w2q8d == (int)-1)
					{
						//*              previous iteration (ki+1) was second of conjugate pair, 
						//*              so this ki is first of conjugate pair; skip to end of loop 
						
						_8t9w2q8d = (int)1;goto Mark140;
					}
					else
					if (_1ub95eoc == (int)1)
					{
						//*              last column, so this ki must be real eigenvalue 
						
						_8t9w2q8d = (int)0;
					}
					else
					if (*(_2ivtt43r+(_1ub95eoc - 1) + (_1ub95eoc - (int)1 - 1) * 1 * (_w8yhbr2r)) == _d0547bi2)
					{
						//*              zero on sub-diagonal, so this ki is real eigenvalue 
						
						_8t9w2q8d = (int)0;
					}
					else
					{
						//*              non-zero on sub-diagonal, so this ki is second of conjugate pair 
						
						_8t9w2q8d = (int)-1;
					}
					// 
					
					if (_y66g69o8)
					{
						
						if (_8t9w2q8d == (int)0)
						{
							
							if (!(*(_2vi7x6ig+(_1ub95eoc - 1))))goto Mark140;
						}
						else
						{
							
							if (!(*(_2vi7x6ig+(_1ub95eoc - (int)1 - 1))))goto Mark140;
						}
						
					}
					//* 
					//*           Compute the KI-th eigenvalue (WR,WI). 
					//* 
					
					_b5j6m2b7 = *(_2ivtt43r+(_1ub95eoc - 1) + (_1ub95eoc - 1) * 1 * (_w8yhbr2r));
					_nc0qphpn = _d0547bi2;
					if (_8t9w2q8d != (int)0)
					_nc0qphpn = (ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+(_1ub95eoc - 1) + (_1ub95eoc - (int)1 - 1) * 1 * (_w8yhbr2r)) ) ) * ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+(_1ub95eoc - (int)1 - 1) + (_1ub95eoc - 1) * 1 * (_w8yhbr2r)) ) ));
					_rhnpgpoi = ILNumerics.F2NET.Intrinsics.MAX(_0h4yb5wu * (ILNumerics.F2NET.Intrinsics.ABS(_b5j6m2b7 ) + ILNumerics.F2NET.Intrinsics.ABS(_nc0qphpn )) ,_bogm0gwy );//* 
					
					if (_8t9w2q8d == (int)0)
					{
						//* 
						//*              -------------------------------------------------------- 
						//*              Real right eigenvector 
						//* 
						
						*(_apig8meb+(_1ub95eoc + (_uicet2a7 * _dxpq0xkr) - 1)) = _kxg5drh2;//* 
						//*              Form right-hand side. 
						//* 
						
						{
							System.Int32 __81fgg2dlsvn2290 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step2290 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2290;
							for (__81fgg2count2290 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1ub95eoc - (int)1) - __81fgg2dlsvn2290 + __81fgg2step2290) / __81fgg2step2290)), _umlkckdg = __81fgg2dlsvn2290; __81fgg2count2290 != 0; __81fgg2count2290--, _umlkckdg += (__81fgg2step2290)) {

							{
								
								*(_apig8meb+(_umlkckdg + (_uicet2a7 * _dxpq0xkr) - 1)) = (-(*(_2ivtt43r+(_umlkckdg - 1) + (_1ub95eoc - 1) * 1 * (_w8yhbr2r))));
Mark50:;
								// continue
							}
														}						}//* 
						//*              Solve upper quasi-triangular system: 
						//*              [ T(1:KI-1,1:KI-1) - WR ]*X = SCALE*WORK. 
						//* 
						
						_olmbz2a3 = (_1ub95eoc - (int)1);
						{
							System.Int32 __81fgg2dlsvn2291 = (System.Int32)((_1ub95eoc - (int)1));
							System.Int32 __81fgg2step2291 = (System.Int32)((int)-1);
							System.Int32 __81fgg2count2291;
							for (__81fgg2count2291 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn2291 + __81fgg2step2291) / __81fgg2step2291)), _znpjgsef = __81fgg2dlsvn2291; __81fgg2count2291 != 0; __81fgg2count2291--, _znpjgsef += (__81fgg2step2291)) {

							{
								
								if (_znpjgsef > _olmbz2a3)goto Mark60;
								_dk3nh7xl = _znpjgsef;
								_psodi8to = _znpjgsef;
								_olmbz2a3 = (_znpjgsef - (int)1);
								if (_znpjgsef > (int)1)
								{
									
									if (*(_2ivtt43r+(_znpjgsef - 1) + (_znpjgsef - (int)1 - 1) * 1 * (_w8yhbr2r)) != _d0547bi2)
									{
										
										_dk3nh7xl = (_znpjgsef - (int)1);
										_olmbz2a3 = (_znpjgsef - (int)2);
									}
									
								}
								//* 
								
								if (_dk3nh7xl == _psodi8to)
								{
									//* 
									//*                    1-by-1 diagonal block 
									//* 
									
									_6andobxo(ref Unsafe.AsRef(false) ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef((int)1) ,ref _rhnpgpoi ,ref Unsafe.AsRef(_kxg5drh2) ,(_2ivtt43r+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,ref Unsafe.AsRef(_kxg5drh2) ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+(_znpjgsef + (_uicet2a7 * _dxpq0xkr) - 1)),ref _dxpq0xkr ,ref _b5j6m2b7 ,ref Unsafe.AsRef(_d0547bi2) ,_ta7zuy9k ,ref Unsafe.AsRef((int)2) ,ref _1m44vtuk ,ref _ziu6urj2 ,ref _bhsiylw4 );//* 
									//*                    Scale X(1,1) to avoid overflow when updating 
									//*                    the right-hand side. 
									//* 
									
									if (_ziu6urj2 > _kxg5drh2)
									{
										
										if (*(_apig8meb+(_znpjgsef - 1)) > (_av7j8yda / _ziu6urj2))
										{
											
											*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2)) = (*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2)) / _ziu6urj2);
											_1m44vtuk = (_1m44vtuk / _ziu6urj2);
										}
										
									}
									//* 
									//*                    Scale if necessary 
									//* 
									
									if (_1m44vtuk != _kxg5drh2)
									_f6jqcjk1(ref _1ub95eoc ,ref _1m44vtuk ,(_apig8meb+((int)1 + (_uicet2a7 * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
									*(_apig8meb+(_znpjgsef + (_uicet2a7 * _dxpq0xkr) - 1)) = *(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2));//* 
									//*                    Update right-hand side 
									//* 
									
									_3czdkijd(ref Unsafe.AsRef(_znpjgsef - (int)1) ,ref Unsafe.AsRef(-(*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2)))) ,(_2ivtt43r+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 + (_uicet2a7 * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );//* 
									
								}
								else
								{
									//* 
									//*                    2-by-2 diagonal block 
									//* 
									
									_6andobxo(ref Unsafe.AsRef(false) ,ref Unsafe.AsRef((int)2) ,ref Unsafe.AsRef((int)1) ,ref _rhnpgpoi ,ref Unsafe.AsRef(_kxg5drh2) ,(_2ivtt43r+(_znpjgsef - (int)1 - 1) + (_znpjgsef - (int)1 - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,ref Unsafe.AsRef(_kxg5drh2) ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+((_znpjgsef - (int)1) + (_uicet2a7 * _dxpq0xkr) - 1)),ref _dxpq0xkr ,ref _b5j6m2b7 ,ref Unsafe.AsRef(_d0547bi2) ,_ta7zuy9k ,ref Unsafe.AsRef((int)2) ,ref _1m44vtuk ,ref _ziu6urj2 ,ref _bhsiylw4 );//* 
									//*                    Scale X(1,1) and X(2,1) to avoid overflow when 
									//*                    updating the right-hand side. 
									//* 
									
									if (_ziu6urj2 > _kxg5drh2)
									{
										
										_bafcbx97 = ILNumerics.F2NET.Intrinsics.MAX(*(_apig8meb+(_znpjgsef - (int)1 - 1)) ,*(_apig8meb+(_znpjgsef - 1)) );
										if (_bafcbx97 > (_av7j8yda / _ziu6urj2))
										{
											
											*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2)) = (*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2)) / _ziu6urj2);
											*(_ta7zuy9k+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2)) = (*(_ta7zuy9k+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2)) / _ziu6urj2);
											_1m44vtuk = (_1m44vtuk / _ziu6urj2);
										}
										
									}
									//* 
									//*                    Scale if necessary 
									//* 
									
									if (_1m44vtuk != _kxg5drh2)
									_f6jqcjk1(ref _1ub95eoc ,ref _1m44vtuk ,(_apig8meb+((int)1 + (_uicet2a7 * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
									*(_apig8meb+((_znpjgsef - (int)1) + (_uicet2a7 * _dxpq0xkr) - 1)) = *(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2));
									*(_apig8meb+(_znpjgsef + (_uicet2a7 * _dxpq0xkr) - 1)) = *(_ta7zuy9k+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2));//* 
									//*                    Update right-hand side 
									//* 
									
									_3czdkijd(ref Unsafe.AsRef(_znpjgsef - (int)2) ,ref Unsafe.AsRef(-(*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2)))) ,(_2ivtt43r+((int)1 - 1) + (_znpjgsef - (int)1 - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 + (_uicet2a7 * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
									_3czdkijd(ref Unsafe.AsRef(_znpjgsef - (int)2) ,ref Unsafe.AsRef(-(*(_ta7zuy9k+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2)))) ,(_2ivtt43r+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 + (_uicet2a7 * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
								}
								
Mark60:;
								// continue
							}
														}						}//* 
						//*              Copy the vector x or Q*x to VR and normalize. 
						//* 
						
						if (!(_htf5ro8d))
						{
							//*                 ------------------------------ 
							//*                 no back-transform: copy x to VR and normalize. 
							
							_gvjhlct0(ref _1ub95eoc ,(_apig8meb+((int)1 + (_uicet2a7 * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) ,(_b88wiuwq+((int)1 - 1) + (_5kucxo3c - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );//* 
							
							_retbwjxi = _ei7om7ok(ref _1ub95eoc ,(_b88wiuwq+((int)1 - 1) + (_5kucxo3c - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );
							_343rc715 = (_kxg5drh2 / ILNumerics.F2NET.Intrinsics.ABS(*(_b88wiuwq+(_retbwjxi - 1) + (_5kucxo3c - 1) * 1 * (_oxoory3e)) ));
							_f6jqcjk1(ref _1ub95eoc ,ref _343rc715 ,(_b88wiuwq+((int)1 - 1) + (_5kucxo3c - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );//* 
							
							{
								System.Int32 __81fgg2dlsvn2292 = (System.Int32)((_1ub95eoc + (int)1));
								const System.Int32 __81fgg2step2292 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2292;
								for (__81fgg2count2292 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2292 + __81fgg2step2292) / __81fgg2step2292)), _umlkckdg = __81fgg2dlsvn2292; __81fgg2count2292 != 0; __81fgg2count2292--, _umlkckdg += (__81fgg2step2292)) {

								{
									
									*(_b88wiuwq+(_umlkckdg - 1) + (_5kucxo3c - 1) * 1 * (_oxoory3e)) = _d0547bi2;
Mark70:;
									// continue
								}
																}							}//* 
							
						}
						else
						if (_f7059815 == (int)1)
						{
							//*                 ------------------------------ 
							//*                 version 1: back-transform each vector with GEMV, Q*x. 
							
							if (_1ub95eoc > (int)1)
							_t5wmtd1j("N" ,ref _dxpq0xkr ,ref Unsafe.AsRef(_1ub95eoc - (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,_b88wiuwq ,ref _oxoory3e ,(_apig8meb+((int)1 + (_uicet2a7 * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_apig8meb+(_1ub95eoc + (_uicet2a7 * _dxpq0xkr) - 1))) ,(_b88wiuwq+((int)1 - 1) + (_1ub95eoc - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );//* 
							
							_retbwjxi = _ei7om7ok(ref _dxpq0xkr ,(_b88wiuwq+((int)1 - 1) + (_1ub95eoc - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );
							_343rc715 = (_kxg5drh2 / ILNumerics.F2NET.Intrinsics.ABS(*(_b88wiuwq+(_retbwjxi - 1) + (_1ub95eoc - 1) * 1 * (_oxoory3e)) ));
							_f6jqcjk1(ref _dxpq0xkr ,ref _343rc715 ,(_b88wiuwq+((int)1 - 1) + (_1ub95eoc - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );//* 
							
						}
						else
						{
							//*                 ------------------------------ 
							//*                 version 2: back-transform block of vectors with GEMM 
							//*                 zero out below vector 
							
							{
								System.Int32 __81fgg2dlsvn2293 = (System.Int32)((_1ub95eoc + (int)1));
								const System.Int32 __81fgg2step2293 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2293;
								for (__81fgg2count2293 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2293 + __81fgg2step2293) / __81fgg2step2293)), _umlkckdg = __81fgg2dlsvn2293; __81fgg2count2293 != 0; __81fgg2count2293--, _umlkckdg += (__81fgg2step2293)) {

								{
									
									*(_apig8meb+(_umlkckdg + (_uicet2a7 * _dxpq0xkr) - 1)) = _d0547bi2;
								}
																}							}
							*(_x49mm45v+(_uicet2a7 - 1)) = _8t9w2q8d;//*                 back-transform and normalization is done below 
							
						}
						
					}
					else
					{
						//* 
						//*              -------------------------------------------------------- 
						//*              Complex right eigenvector. 
						//* 
						//*              Initial solve 
						//*              [ ( T(KI-1,KI-1) T(KI-1,KI) ) - (WR + I*WI) ]*X = 0. 
						//*              [ ( T(KI,  KI-1) T(KI,  KI) )               ] 
						//* 
						
						if (ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+(_1ub95eoc - (int)1 - 1) + (_1ub95eoc - 1) * 1 * (_w8yhbr2r)) ) >= ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+(_1ub95eoc - 1) + (_1ub95eoc - (int)1 - 1) * 1 * (_w8yhbr2r)) ))
						{
							
							*(_apig8meb+((_1ub95eoc - (int)1) + ((_uicet2a7 - (int)1) * _dxpq0xkr) - 1)) = _kxg5drh2;
							*(_apig8meb+(_1ub95eoc + ((_uicet2a7) * _dxpq0xkr) - 1)) = (_nc0qphpn / *(_2ivtt43r+(_1ub95eoc - (int)1 - 1) + (_1ub95eoc - 1) * 1 * (_w8yhbr2r)));
						}
						else
						{
							
							*(_apig8meb+((_1ub95eoc - (int)1) + ((_uicet2a7 - (int)1) * _dxpq0xkr) - 1)) = (-((_nc0qphpn / *(_2ivtt43r+(_1ub95eoc - 1) + (_1ub95eoc - (int)1 - 1) * 1 * (_w8yhbr2r)))));
							*(_apig8meb+(_1ub95eoc + ((_uicet2a7) * _dxpq0xkr) - 1)) = _kxg5drh2;
						}
						
						*(_apig8meb+(_1ub95eoc + ((_uicet2a7 - (int)1) * _dxpq0xkr) - 1)) = _d0547bi2;
						*(_apig8meb+((_1ub95eoc - (int)1) + ((_uicet2a7) * _dxpq0xkr) - 1)) = _d0547bi2;//* 
						//*              Form right-hand side. 
						//* 
						
						{
							System.Int32 __81fgg2dlsvn2294 = (System.Int32)((int)1);
							const System.Int32 __81fgg2step2294 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2294;
							for (__81fgg2count2294 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1ub95eoc - (int)2) - __81fgg2dlsvn2294 + __81fgg2step2294) / __81fgg2step2294)), _umlkckdg = __81fgg2dlsvn2294; __81fgg2count2294 != 0; __81fgg2count2294--, _umlkckdg += (__81fgg2step2294)) {

							{
								
								*(_apig8meb+(_umlkckdg + ((_uicet2a7 - (int)1) * _dxpq0xkr) - 1)) = (-((*(_apig8meb+((_1ub95eoc - (int)1) + ((_uicet2a7 - (int)1) * _dxpq0xkr) - 1)) * *(_2ivtt43r+(_umlkckdg - 1) + (_1ub95eoc - (int)1 - 1) * 1 * (_w8yhbr2r)))));
								*(_apig8meb+(_umlkckdg + ((_uicet2a7) * _dxpq0xkr) - 1)) = (-((*(_apig8meb+(_1ub95eoc + ((_uicet2a7) * _dxpq0xkr) - 1)) * *(_2ivtt43r+(_umlkckdg - 1) + (_1ub95eoc - 1) * 1 * (_w8yhbr2r)))));
Mark80:;
								// continue
							}
														}						}//* 
						//*              Solve upper quasi-triangular system: 
						//*              [ T(1:KI-2,1:KI-2) - (WR+i*WI) ]*X = SCALE*(WORK+i*WORK2) 
						//* 
						
						_olmbz2a3 = (_1ub95eoc - (int)2);
						{
							System.Int32 __81fgg2dlsvn2295 = (System.Int32)((_1ub95eoc - (int)2));
							System.Int32 __81fgg2step2295 = (System.Int32)((int)-1);
							System.Int32 __81fgg2count2295;
							for (__81fgg2count2295 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn2295 + __81fgg2step2295) / __81fgg2step2295)), _znpjgsef = __81fgg2dlsvn2295; __81fgg2count2295 != 0; __81fgg2count2295--, _znpjgsef += (__81fgg2step2295)) {

							{
								
								if (_znpjgsef > _olmbz2a3)goto Mark90;
								_dk3nh7xl = _znpjgsef;
								_psodi8to = _znpjgsef;
								_olmbz2a3 = (_znpjgsef - (int)1);
								if (_znpjgsef > (int)1)
								{
									
									if (*(_2ivtt43r+(_znpjgsef - 1) + (_znpjgsef - (int)1 - 1) * 1 * (_w8yhbr2r)) != _d0547bi2)
									{
										
										_dk3nh7xl = (_znpjgsef - (int)1);
										_olmbz2a3 = (_znpjgsef - (int)2);
									}
									
								}
								//* 
								
								if (_dk3nh7xl == _psodi8to)
								{
									//* 
									//*                    1-by-1 diagonal block 
									//* 
									
									_6andobxo(ref Unsafe.AsRef(false) ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef((int)2) ,ref _rhnpgpoi ,ref Unsafe.AsRef(_kxg5drh2) ,(_2ivtt43r+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,ref Unsafe.AsRef(_kxg5drh2) ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+(_znpjgsef + ((_uicet2a7 - (int)1) * _dxpq0xkr) - 1)),ref _dxpq0xkr ,ref _b5j6m2b7 ,ref _nc0qphpn ,_ta7zuy9k ,ref Unsafe.AsRef((int)2) ,ref _1m44vtuk ,ref _ziu6urj2 ,ref _bhsiylw4 );//* 
									//*                    Scale X(1,1) and X(1,2) to avoid overflow when 
									//*                    updating the right-hand side. 
									//* 
									
									if (_ziu6urj2 > _kxg5drh2)
									{
										
										if (*(_apig8meb+(_znpjgsef - 1)) > (_av7j8yda / _ziu6urj2))
										{
											
											*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2)) = (*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2)) / _ziu6urj2);
											*(_ta7zuy9k+((int)1 - 1) + ((int)2 - 1) * 1 * ((int)2)) = (*(_ta7zuy9k+((int)1 - 1) + ((int)2 - 1) * 1 * ((int)2)) / _ziu6urj2);
											_1m44vtuk = (_1m44vtuk / _ziu6urj2);
										}
										
									}
									//* 
									//*                    Scale if necessary 
									//* 
									
									if (_1m44vtuk != _kxg5drh2)
									{
										
										_f6jqcjk1(ref _1ub95eoc ,ref _1m44vtuk ,(_apig8meb+((int)1 + ((_uicet2a7 - (int)1) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
										_f6jqcjk1(ref _1ub95eoc ,ref _1m44vtuk ,(_apig8meb+((int)1 + ((_uicet2a7) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
									}
									
									*(_apig8meb+(_znpjgsef + ((_uicet2a7 - (int)1) * _dxpq0xkr) - 1)) = *(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2));
									*(_apig8meb+(_znpjgsef + ((_uicet2a7) * _dxpq0xkr) - 1)) = *(_ta7zuy9k+((int)1 - 1) + ((int)2 - 1) * 1 * ((int)2));//* 
									//*                    Update the right-hand side 
									//* 
									
									_3czdkijd(ref Unsafe.AsRef(_znpjgsef - (int)1) ,ref Unsafe.AsRef(-(*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2)))) ,(_2ivtt43r+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 + ((_uicet2a7 - (int)1) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
									_3czdkijd(ref Unsafe.AsRef(_znpjgsef - (int)1) ,ref Unsafe.AsRef(-(*(_ta7zuy9k+((int)1 - 1) + ((int)2 - 1) * 1 * ((int)2)))) ,(_2ivtt43r+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 + ((_uicet2a7) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );//* 
									
								}
								else
								{
									//* 
									//*                    2-by-2 diagonal block 
									//* 
									
									_6andobxo(ref Unsafe.AsRef(false) ,ref Unsafe.AsRef((int)2) ,ref Unsafe.AsRef((int)2) ,ref _rhnpgpoi ,ref Unsafe.AsRef(_kxg5drh2) ,(_2ivtt43r+(_znpjgsef - (int)1 - 1) + (_znpjgsef - (int)1 - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,ref Unsafe.AsRef(_kxg5drh2) ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+((_znpjgsef - (int)1) + ((_uicet2a7 - (int)1) * _dxpq0xkr) - 1)),ref _dxpq0xkr ,ref _b5j6m2b7 ,ref _nc0qphpn ,_ta7zuy9k ,ref Unsafe.AsRef((int)2) ,ref _1m44vtuk ,ref _ziu6urj2 ,ref _bhsiylw4 );//* 
									//*                    Scale X to avoid overflow when updating 
									//*                    the right-hand side. 
									//* 
									
									if (_ziu6urj2 > _kxg5drh2)
									{
										
										_bafcbx97 = ILNumerics.F2NET.Intrinsics.MAX(*(_apig8meb+(_znpjgsef - (int)1 - 1)) ,*(_apig8meb+(_znpjgsef - 1)) );
										if (_bafcbx97 > (_av7j8yda / _ziu6urj2))
										{
											
											_egqfb6nh = (_kxg5drh2 / _ziu6urj2);
											*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2)) = (*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2)) * _egqfb6nh);
											*(_ta7zuy9k+((int)1 - 1) + ((int)2 - 1) * 1 * ((int)2)) = (*(_ta7zuy9k+((int)1 - 1) + ((int)2 - 1) * 1 * ((int)2)) * _egqfb6nh);
											*(_ta7zuy9k+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2)) = (*(_ta7zuy9k+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2)) * _egqfb6nh);
											*(_ta7zuy9k+((int)2 - 1) + ((int)2 - 1) * 1 * ((int)2)) = (*(_ta7zuy9k+((int)2 - 1) + ((int)2 - 1) * 1 * ((int)2)) * _egqfb6nh);
											_1m44vtuk = (_1m44vtuk * _egqfb6nh);
										}
										
									}
									//* 
									//*                    Scale if necessary 
									//* 
									
									if (_1m44vtuk != _kxg5drh2)
									{
										
										_f6jqcjk1(ref _1ub95eoc ,ref _1m44vtuk ,(_apig8meb+((int)1 + ((_uicet2a7 - (int)1) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
										_f6jqcjk1(ref _1ub95eoc ,ref _1m44vtuk ,(_apig8meb+((int)1 + ((_uicet2a7) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
									}
									
									*(_apig8meb+((_znpjgsef - (int)1) + ((_uicet2a7 - (int)1) * _dxpq0xkr) - 1)) = *(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2));
									*(_apig8meb+(_znpjgsef + ((_uicet2a7 - (int)1) * _dxpq0xkr) - 1)) = *(_ta7zuy9k+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2));
									*(_apig8meb+((_znpjgsef - (int)1) + ((_uicet2a7) * _dxpq0xkr) - 1)) = *(_ta7zuy9k+((int)1 - 1) + ((int)2 - 1) * 1 * ((int)2));
									*(_apig8meb+(_znpjgsef + ((_uicet2a7) * _dxpq0xkr) - 1)) = *(_ta7zuy9k+((int)2 - 1) + ((int)2 - 1) * 1 * ((int)2));//* 
									//*                    Update the right-hand side 
									//* 
									
									_3czdkijd(ref Unsafe.AsRef(_znpjgsef - (int)2) ,ref Unsafe.AsRef(-(*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2)))) ,(_2ivtt43r+((int)1 - 1) + (_znpjgsef - (int)1 - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 + ((_uicet2a7 - (int)1) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
									_3czdkijd(ref Unsafe.AsRef(_znpjgsef - (int)2) ,ref Unsafe.AsRef(-(*(_ta7zuy9k+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2)))) ,(_2ivtt43r+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 + ((_uicet2a7 - (int)1) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
									_3czdkijd(ref Unsafe.AsRef(_znpjgsef - (int)2) ,ref Unsafe.AsRef(-(*(_ta7zuy9k+((int)1 - 1) + ((int)2 - 1) * 1 * ((int)2)))) ,(_2ivtt43r+((int)1 - 1) + (_znpjgsef - (int)1 - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 + ((_uicet2a7) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
									_3czdkijd(ref Unsafe.AsRef(_znpjgsef - (int)2) ,ref Unsafe.AsRef(-(*(_ta7zuy9k+((int)2 - 1) + ((int)2 - 1) * 1 * ((int)2)))) ,(_2ivtt43r+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((int)1 + ((_uicet2a7) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
								}
								
Mark90:;
								// continue
							}
														}						}//* 
						//*              Copy the vector x or Q*x to VR and normalize. 
						//* 
						
						if (!(_htf5ro8d))
						{
							//*                 ------------------------------ 
							//*                 no back-transform: copy x to VR and normalize. 
							
							_gvjhlct0(ref _1ub95eoc ,(_apig8meb+((int)1 + ((_uicet2a7 - (int)1) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) ,(_b88wiuwq+((int)1 - 1) + (_5kucxo3c - (int)1 - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );
							_gvjhlct0(ref _1ub95eoc ,(_apig8meb+((int)1 + ((_uicet2a7) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) ,(_b88wiuwq+((int)1 - 1) + (_5kucxo3c - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );//* 
							
							_trz23puj = _d0547bi2;
							{
								System.Int32 __81fgg2dlsvn2296 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step2296 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2296;
								for (__81fgg2count2296 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1ub95eoc) - __81fgg2dlsvn2296 + __81fgg2step2296) / __81fgg2step2296)), _umlkckdg = __81fgg2dlsvn2296; __81fgg2count2296 != 0; __81fgg2count2296--, _umlkckdg += (__81fgg2step2296)) {

								{
									
									_trz23puj = ILNumerics.F2NET.Intrinsics.MAX(_trz23puj ,ILNumerics.F2NET.Intrinsics.ABS(*(_b88wiuwq+(_umlkckdg - 1) + (_5kucxo3c - (int)1 - 1) * 1 * (_oxoory3e)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_b88wiuwq+(_umlkckdg - 1) + (_5kucxo3c - 1) * 1 * (_oxoory3e)) ) );
Mark100:;
									// continue
								}
																}							}
							_343rc715 = (_kxg5drh2 / _trz23puj);
							_f6jqcjk1(ref _1ub95eoc ,ref _343rc715 ,(_b88wiuwq+((int)1 - 1) + (_5kucxo3c - (int)1 - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );
							_f6jqcjk1(ref _1ub95eoc ,ref _343rc715 ,(_b88wiuwq+((int)1 - 1) + (_5kucxo3c - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );//* 
							
							{
								System.Int32 __81fgg2dlsvn2297 = (System.Int32)((_1ub95eoc + (int)1));
								const System.Int32 __81fgg2step2297 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2297;
								for (__81fgg2count2297 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2297 + __81fgg2step2297) / __81fgg2step2297)), _umlkckdg = __81fgg2dlsvn2297; __81fgg2count2297 != 0; __81fgg2count2297--, _umlkckdg += (__81fgg2step2297)) {

								{
									
									*(_b88wiuwq+(_umlkckdg - 1) + (_5kucxo3c - (int)1 - 1) * 1 * (_oxoory3e)) = _d0547bi2;
									*(_b88wiuwq+(_umlkckdg - 1) + (_5kucxo3c - 1) * 1 * (_oxoory3e)) = _d0547bi2;
Mark110:;
									// continue
								}
																}							}//* 
							
						}
						else
						if (_f7059815 == (int)1)
						{
							//*                 ------------------------------ 
							//*                 version 1: back-transform each vector with GEMV, Q*x. 
							
							if (_1ub95eoc > (int)2)
							{
								
								_t5wmtd1j("N" ,ref _dxpq0xkr ,ref Unsafe.AsRef(_1ub95eoc - (int)2) ,ref Unsafe.AsRef(_kxg5drh2) ,_b88wiuwq ,ref _oxoory3e ,(_apig8meb+((int)1 + ((_uicet2a7 - (int)1) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_apig8meb+((_1ub95eoc - (int)1) + ((_uicet2a7 - (int)1) * _dxpq0xkr) - 1))) ,(_b88wiuwq+((int)1 - 1) + (_1ub95eoc - (int)1 - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );
								_t5wmtd1j("N" ,ref _dxpq0xkr ,ref Unsafe.AsRef(_1ub95eoc - (int)2) ,ref Unsafe.AsRef(_kxg5drh2) ,_b88wiuwq ,ref _oxoory3e ,(_apig8meb+((int)1 + ((_uicet2a7) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_apig8meb+(_1ub95eoc + ((_uicet2a7) * _dxpq0xkr) - 1))) ,(_b88wiuwq+((int)1 - 1) + (_1ub95eoc - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );
							}
							else
							{
								
								_f6jqcjk1(ref _dxpq0xkr ,ref Unsafe.AsRef(*(_apig8meb+((_1ub95eoc - (int)1) + ((_uicet2a7 - (int)1) * _dxpq0xkr) - 1))) ,(_b88wiuwq+((int)1 - 1) + (_1ub95eoc - (int)1 - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );
								_f6jqcjk1(ref _dxpq0xkr ,ref Unsafe.AsRef(*(_apig8meb+(_1ub95eoc + ((_uicet2a7) * _dxpq0xkr) - 1))) ,(_b88wiuwq+((int)1 - 1) + (_1ub95eoc - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );
							}
							//* 
							
							_trz23puj = _d0547bi2;
							{
								System.Int32 __81fgg2dlsvn2298 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step2298 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2298;
								for (__81fgg2count2298 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2298 + __81fgg2step2298) / __81fgg2step2298)), _umlkckdg = __81fgg2dlsvn2298; __81fgg2count2298 != 0; __81fgg2count2298--, _umlkckdg += (__81fgg2step2298)) {

								{
									
									_trz23puj = ILNumerics.F2NET.Intrinsics.MAX(_trz23puj ,ILNumerics.F2NET.Intrinsics.ABS(*(_b88wiuwq+(_umlkckdg - 1) + (_1ub95eoc - (int)1 - 1) * 1 * (_oxoory3e)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_b88wiuwq+(_umlkckdg - 1) + (_1ub95eoc - 1) * 1 * (_oxoory3e)) ) );
Mark120:;
									// continue
								}
																}							}
							_343rc715 = (_kxg5drh2 / _trz23puj);
							_f6jqcjk1(ref _dxpq0xkr ,ref _343rc715 ,(_b88wiuwq+((int)1 - 1) + (_1ub95eoc - (int)1 - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );
							_f6jqcjk1(ref _dxpq0xkr ,ref _343rc715 ,(_b88wiuwq+((int)1 - 1) + (_1ub95eoc - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );//* 
							
						}
						else
						{
							//*                 ------------------------------ 
							//*                 version 2: back-transform block of vectors with GEMM 
							//*                 zero out below vector 
							
							{
								System.Int32 __81fgg2dlsvn2299 = (System.Int32)((_1ub95eoc + (int)1));
								const System.Int32 __81fgg2step2299 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2299;
								for (__81fgg2count2299 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2299 + __81fgg2step2299) / __81fgg2step2299)), _umlkckdg = __81fgg2dlsvn2299; __81fgg2count2299 != 0; __81fgg2count2299--, _umlkckdg += (__81fgg2step2299)) {

								{
									
									*(_apig8meb+(_umlkckdg + ((_uicet2a7 - (int)1) * _dxpq0xkr) - 1)) = _d0547bi2;
									*(_apig8meb+(_umlkckdg + ((_uicet2a7) * _dxpq0xkr) - 1)) = _d0547bi2;
								}
																}							}
							*(_x49mm45v+(_uicet2a7 - (int)1 - 1)) = (-(_8t9w2q8d));
							*(_x49mm45v+(_uicet2a7 - 1)) = _8t9w2q8d;
							_uicet2a7 = (_uicet2a7 - (int)1);//*                 back-transform and normalization is done below 
							
						}
						
					}
					// 
					
					if (_f7059815 > (int)1)
					{
						//*              -------------------------------------------------------- 
						//*              Blocked version of back-transform 
						//*              For complex case, KI2 includes both vectors (KI-1 and KI) 
						
						if (_8t9w2q8d == (int)0)
						{
							
							_sjhh7htz = _1ub95eoc;
						}
						else
						{
							
							_sjhh7htz = (_1ub95eoc - (int)1);
						}
						// 
						//*              Columns IV:NB of work are valid vectors. 
						//*              When the number of vectors stored reaches NB-1 or NB, 
						//*              or if this was last vector, do the GEMM 
						
						if ((_uicet2a7 <= (int)2) | (_sjhh7htz == (int)1))
						{
							
							_5nsxi69c("N" ,"N" ,ref _dxpq0xkr ,ref Unsafe.AsRef((_f7059815 - _uicet2a7) + (int)1) ,ref Unsafe.AsRef((_sjhh7htz + _f7059815) - _uicet2a7) ,ref Unsafe.AsRef(_kxg5drh2) ,_b88wiuwq ,ref _oxoory3e ,(_apig8meb+((int)1 + ((_uicet2a7) * _dxpq0xkr) - 1)),ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+((int)1 + ((_f7059815 + _uicet2a7) * _dxpq0xkr) - 1)),ref _dxpq0xkr );//*                 normalize vectors 
							
							{
								System.Int32 __81fgg2dlsvn2300 = (System.Int32)(_uicet2a7);
								const System.Int32 __81fgg2step2300 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2300;
								for (__81fgg2count2300 = System.Math.Max(0, (System.Int32)(((System.Int32)(_f7059815) - __81fgg2dlsvn2300 + __81fgg2step2300) / __81fgg2step2300)), _umlkckdg = __81fgg2dlsvn2300; __81fgg2count2300 != 0; __81fgg2count2300--, _umlkckdg += (__81fgg2step2300)) {

								{
									
									if (*(_x49mm45v+(_umlkckdg - 1)) == (int)0)
									{
										//*                       real eigenvector 
										
										_retbwjxi = _ei7om7ok(ref _dxpq0xkr ,(_apig8meb+((int)1 + ((_f7059815 + _umlkckdg) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
										_343rc715 = (_kxg5drh2 / ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+(_retbwjxi + ((_f7059815 + _umlkckdg) * _dxpq0xkr) - 1)) ));
									}
									else
									if (*(_x49mm45v+(_umlkckdg - 1)) == (int)1)
									{
										//*                       first eigenvector of conjugate pair 
										
										_trz23puj = _d0547bi2;
										{
											System.Int32 __81fgg2dlsvn2301 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step2301 = (System.Int32)((int)1);
											System.Int32 __81fgg2count2301;
											for (__81fgg2count2301 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2301 + __81fgg2step2301) / __81fgg2step2301)), _retbwjxi = __81fgg2dlsvn2301; __81fgg2count2301 != 0; __81fgg2count2301--, _retbwjxi += (__81fgg2step2301)) {

											{
												
												_trz23puj = ILNumerics.F2NET.Intrinsics.MAX(_trz23puj ,ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+(_retbwjxi + ((_f7059815 + _umlkckdg) * _dxpq0xkr) - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+(_retbwjxi + (((_f7059815 + _umlkckdg) + (int)1) * _dxpq0xkr) - 1)) ) );
											}
																						}										}
										_343rc715 = (_kxg5drh2 / _trz23puj);//*                    else if ISCOMPLEX(K).EQ.-1 
										//*                       second eigenvector of conjugate pair 
										//*                       reuse same REMAX as previous K 
										
									}
									
									_f6jqcjk1(ref _dxpq0xkr ,ref _343rc715 ,(_apig8meb+((int)1 + ((_f7059815 + _umlkckdg) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
								}
																}							}
							_hhtvj1kb("F" ,ref _dxpq0xkr ,ref Unsafe.AsRef((_f7059815 - _uicet2a7) + (int)1) ,(_apig8meb+((int)1 + ((_f7059815 + _uicet2a7) * _dxpq0xkr) - 1)),ref _dxpq0xkr ,(_b88wiuwq+((int)1 - 1) + (_sjhh7htz - 1) * 1 * (_oxoory3e)),ref _oxoory3e );
							_uicet2a7 = _f7059815;
						}
						else
						{
							
							_uicet2a7 = (_uicet2a7 - (int)1);
						}
						
					}
					//* 
					
					_5kucxo3c = (_5kucxo3c - (int)1);
					if (_8t9w2q8d != (int)0)
					_5kucxo3c = (_5kucxo3c - (int)1);
Mark140:;
					// continue
				}
								}			}
		}
		// 
		
		if (_35js00fx)
		{
			//* 
			//*        ============================================================ 
			//*        Compute left eigenvectors. 
			//* 
			//*        IV is index of column in current block. 
			//*        For complex left vector, uses IV for real part and IV+1 for complex part. 
			//*        Non-blocked version always uses IV=1; 
			//*        blocked     version starts with IV=1, goes up to NB-1 or NB. 
			//*        (Note the "0-th" column is used for 1-norms computed above.) 
			
			_uicet2a7 = (int)1;
			_8t9w2q8d = (int)0;
			_5kucxo3c = (int)1;
			{
				System.Int32 __81fgg2dlsvn2302 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2302 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2302;
				for (__81fgg2count2302 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2302 + __81fgg2step2302) / __81fgg2step2302)), _1ub95eoc = __81fgg2dlsvn2302; __81fgg2count2302 != 0; __81fgg2count2302--, _1ub95eoc += (__81fgg2step2302)) {

				{
					
					if (_8t9w2q8d == (int)1)
					{
						//*              previous iteration (ki-1) was first of conjugate pair, 
						//*              so this ki is second of conjugate pair; skip to end of loop 
						
						_8t9w2q8d = (int)-1;goto Mark260;
					}
					else
					if (_1ub95eoc == _dxpq0xkr)
					{
						//*              last column, so this ki must be real eigenvalue 
						
						_8t9w2q8d = (int)0;
					}
					else
					if (*(_2ivtt43r+(_1ub95eoc + (int)1 - 1) + (_1ub95eoc - 1) * 1 * (_w8yhbr2r)) == _d0547bi2)
					{
						//*              zero on sub-diagonal, so this ki is real eigenvalue 
						
						_8t9w2q8d = (int)0;
					}
					else
					{
						//*              non-zero on sub-diagonal, so this ki is first of conjugate pair 
						
						_8t9w2q8d = (int)1;
					}
					//* 
					
					if (_y66g69o8)
					{
						
						if (!(*(_2vi7x6ig+(_1ub95eoc - 1))))goto Mark260;
					}
					//* 
					//*           Compute the KI-th eigenvalue (WR,WI). 
					//* 
					
					_b5j6m2b7 = *(_2ivtt43r+(_1ub95eoc - 1) + (_1ub95eoc - 1) * 1 * (_w8yhbr2r));
					_nc0qphpn = _d0547bi2;
					if (_8t9w2q8d != (int)0)
					_nc0qphpn = (ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+(_1ub95eoc - 1) + (_1ub95eoc + (int)1 - 1) * 1 * (_w8yhbr2r)) ) ) * ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+(_1ub95eoc + (int)1 - 1) + (_1ub95eoc - 1) * 1 * (_w8yhbr2r)) ) ));
					_rhnpgpoi = ILNumerics.F2NET.Intrinsics.MAX(_0h4yb5wu * (ILNumerics.F2NET.Intrinsics.ABS(_b5j6m2b7 ) + ILNumerics.F2NET.Intrinsics.ABS(_nc0qphpn )) ,_bogm0gwy );//* 
					
					if (_8t9w2q8d == (int)0)
					{
						//* 
						//*              -------------------------------------------------------- 
						//*              Real left eigenvector 
						//* 
						
						*(_apig8meb+(_1ub95eoc + (_uicet2a7 * _dxpq0xkr) - 1)) = _kxg5drh2;//* 
						//*              Form right-hand side. 
						//* 
						
						{
							System.Int32 __81fgg2dlsvn2303 = (System.Int32)((_1ub95eoc + (int)1));
							const System.Int32 __81fgg2step2303 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2303;
							for (__81fgg2count2303 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2303 + __81fgg2step2303) / __81fgg2step2303)), _umlkckdg = __81fgg2dlsvn2303; __81fgg2count2303 != 0; __81fgg2count2303--, _umlkckdg += (__81fgg2step2303)) {

							{
								
								*(_apig8meb+(_umlkckdg + (_uicet2a7 * _dxpq0xkr) - 1)) = (-(*(_2ivtt43r+(_1ub95eoc - 1) + (_umlkckdg - 1) * 1 * (_w8yhbr2r))));
Mark160:;
								// continue
							}
														}						}//* 
						//*              Solve transposed quasi-triangular system: 
						//*              [ T(KI+1:N,KI+1:N) - WR ]**T * X = SCALE*WORK 
						//* 
						
						_2wzbu6qf = _kxg5drh2;
						_69yw1gah = _av7j8yda;//* 
						
						_olmbz2a3 = (_1ub95eoc + (int)1);
						{
							System.Int32 __81fgg2dlsvn2304 = (System.Int32)((_1ub95eoc + (int)1));
							const System.Int32 __81fgg2step2304 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2304;
							for (__81fgg2count2304 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2304 + __81fgg2step2304) / __81fgg2step2304)), _znpjgsef = __81fgg2dlsvn2304; __81fgg2count2304 != 0; __81fgg2count2304--, _znpjgsef += (__81fgg2step2304)) {

							{
								
								if (_znpjgsef < _olmbz2a3)goto Mark170;
								_dk3nh7xl = _znpjgsef;
								_psodi8to = _znpjgsef;
								_olmbz2a3 = (_znpjgsef + (int)1);
								if (_znpjgsef < _dxpq0xkr)
								{
									
									if (*(_2ivtt43r+(_znpjgsef + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)) != _d0547bi2)
									{
										
										_psodi8to = (_znpjgsef + (int)1);
										_olmbz2a3 = (_znpjgsef + (int)2);
									}
									
								}
								//* 
								
								if (_dk3nh7xl == _psodi8to)
								{
									//* 
									//*                    1-by-1 diagonal block 
									//* 
									//*                    Scale if necessary to avoid overflow when forming 
									//*                    the right-hand side. 
									//* 
									
									if (*(_apig8meb+(_znpjgsef - 1)) > _69yw1gah)
									{
										
										_egqfb6nh = (_kxg5drh2 / _2wzbu6qf);
										_f6jqcjk1(ref Unsafe.AsRef((_dxpq0xkr - _1ub95eoc) + (int)1) ,ref _egqfb6nh ,(_apig8meb+(_1ub95eoc + (_uicet2a7 * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
										_2wzbu6qf = _kxg5drh2;
										_69yw1gah = _av7j8yda;
									}
									//* 
									
									*(_apig8meb+(_znpjgsef + (_uicet2a7 * _dxpq0xkr) - 1)) = (*(_apig8meb+(_znpjgsef + (_uicet2a7 * _dxpq0xkr) - 1)) - _le984b8z(ref Unsafe.AsRef((_znpjgsef - _1ub95eoc) - (int)1) ,(_2ivtt43r+(_1ub95eoc + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((_1ub95eoc + (int)1) + (_uicet2a7 * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) ));//* 
									//*                    Solve [ T(J,J) - WR ]**T * X = WORK 
									//* 
									
									_6andobxo(ref Unsafe.AsRef(false) ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef((int)1) ,ref _rhnpgpoi ,ref Unsafe.AsRef(_kxg5drh2) ,(_2ivtt43r+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,ref Unsafe.AsRef(_kxg5drh2) ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+(_znpjgsef + (_uicet2a7 * _dxpq0xkr) - 1)),ref _dxpq0xkr ,ref _b5j6m2b7 ,ref Unsafe.AsRef(_d0547bi2) ,_ta7zuy9k ,ref Unsafe.AsRef((int)2) ,ref _1m44vtuk ,ref _ziu6urj2 ,ref _bhsiylw4 );//* 
									//*                    Scale if necessary 
									//* 
									
									if (_1m44vtuk != _kxg5drh2)
									_f6jqcjk1(ref Unsafe.AsRef((_dxpq0xkr - _1ub95eoc) + (int)1) ,ref _1m44vtuk ,(_apig8meb+(_1ub95eoc + (_uicet2a7 * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
									*(_apig8meb+(_znpjgsef + (_uicet2a7 * _dxpq0xkr) - 1)) = *(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2));
									_2wzbu6qf = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+(_znpjgsef + (_uicet2a7 * _dxpq0xkr) - 1)) ) ,_2wzbu6qf );
									_69yw1gah = (_av7j8yda / _2wzbu6qf);//* 
									
								}
								else
								{
									//* 
									//*                    2-by-2 diagonal block 
									//* 
									//*                    Scale if necessary to avoid overflow when forming 
									//*                    the right-hand side. 
									//* 
									
									_bafcbx97 = ILNumerics.F2NET.Intrinsics.MAX(*(_apig8meb+(_znpjgsef - 1)) ,*(_apig8meb+(_znpjgsef + (int)1 - 1)) );
									if (_bafcbx97 > _69yw1gah)
									{
										
										_egqfb6nh = (_kxg5drh2 / _2wzbu6qf);
										_f6jqcjk1(ref Unsafe.AsRef((_dxpq0xkr - _1ub95eoc) + (int)1) ,ref _egqfb6nh ,(_apig8meb+(_1ub95eoc + (_uicet2a7 * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
										_2wzbu6qf = _kxg5drh2;
										_69yw1gah = _av7j8yda;
									}
									//* 
									
									*(_apig8meb+(_znpjgsef + (_uicet2a7 * _dxpq0xkr) - 1)) = (*(_apig8meb+(_znpjgsef + (_uicet2a7 * _dxpq0xkr) - 1)) - _le984b8z(ref Unsafe.AsRef((_znpjgsef - _1ub95eoc) - (int)1) ,(_2ivtt43r+(_1ub95eoc + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((_1ub95eoc + (int)1) + (_uicet2a7 * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) ));//* 
									
									*(_apig8meb+((_znpjgsef + (int)1) + (_uicet2a7 * _dxpq0xkr) - 1)) = (*(_apig8meb+((_znpjgsef + (int)1) + (_uicet2a7 * _dxpq0xkr) - 1)) - _le984b8z(ref Unsafe.AsRef((_znpjgsef - _1ub95eoc) - (int)1) ,(_2ivtt43r+(_1ub95eoc + (int)1 - 1) + (_znpjgsef + (int)1 - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((_1ub95eoc + (int)1) + (_uicet2a7 * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) ));//* 
									//*                    Solve 
									//*                    [ T(J,J)-WR   T(J,J+1)      ]**T * X = SCALE*( WORK1 ) 
									//*                    [ T(J+1,J)    T(J+1,J+1)-WR ]                ( WORK2 ) 
									//* 
									
									_6andobxo(ref Unsafe.AsRef(true) ,ref Unsafe.AsRef((int)2) ,ref Unsafe.AsRef((int)1) ,ref _rhnpgpoi ,ref Unsafe.AsRef(_kxg5drh2) ,(_2ivtt43r+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,ref Unsafe.AsRef(_kxg5drh2) ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+(_znpjgsef + (_uicet2a7 * _dxpq0xkr) - 1)),ref _dxpq0xkr ,ref _b5j6m2b7 ,ref Unsafe.AsRef(_d0547bi2) ,_ta7zuy9k ,ref Unsafe.AsRef((int)2) ,ref _1m44vtuk ,ref _ziu6urj2 ,ref _bhsiylw4 );//* 
									//*                    Scale if necessary 
									//* 
									
									if (_1m44vtuk != _kxg5drh2)
									_f6jqcjk1(ref Unsafe.AsRef((_dxpq0xkr - _1ub95eoc) + (int)1) ,ref _1m44vtuk ,(_apig8meb+(_1ub95eoc + (_uicet2a7 * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
									*(_apig8meb+(_znpjgsef + (_uicet2a7 * _dxpq0xkr) - 1)) = *(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2));
									*(_apig8meb+((_znpjgsef + (int)1) + (_uicet2a7 * _dxpq0xkr) - 1)) = *(_ta7zuy9k+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2));//* 
									
									_2wzbu6qf = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+(_znpjgsef + (_uicet2a7 * _dxpq0xkr) - 1)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+((_znpjgsef + (int)1) + (_uicet2a7 * _dxpq0xkr) - 1)) ) ,_2wzbu6qf );
									_69yw1gah = (_av7j8yda / _2wzbu6qf);//* 
									
								}
								
Mark170:;
								// continue
							}
														}						}//* 
						//*              Copy the vector x or Q*x to VL and normalize. 
						//* 
						
						if (!(_htf5ro8d))
						{
							//*                 ------------------------------ 
							//*                 no back-transform: copy x to VL and normalize. 
							
							_gvjhlct0(ref Unsafe.AsRef((_dxpq0xkr - _1ub95eoc) + (int)1) ,(_apig8meb+(_1ub95eoc + (_uicet2a7 * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) ,(_ppzorcqs+(_1ub95eoc - 1) + (_5kucxo3c - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );//* 
							
							_retbwjxi = ((_ei7om7ok(ref Unsafe.AsRef((_dxpq0xkr - _1ub95eoc) + (int)1) ,(_ppzorcqs+(_1ub95eoc - 1) + (_5kucxo3c - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) ) + _1ub95eoc) - (int)1);
							_343rc715 = (_kxg5drh2 / ILNumerics.F2NET.Intrinsics.ABS(*(_ppzorcqs+(_retbwjxi - 1) + (_5kucxo3c - 1) * 1 * (_uq25zlw0)) ));
							_f6jqcjk1(ref Unsafe.AsRef((_dxpq0xkr - _1ub95eoc) + (int)1) ,ref _343rc715 ,(_ppzorcqs+(_1ub95eoc - 1) + (_5kucxo3c - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );//* 
							
							{
								System.Int32 __81fgg2dlsvn2305 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step2305 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2305;
								for (__81fgg2count2305 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1ub95eoc - (int)1) - __81fgg2dlsvn2305 + __81fgg2step2305) / __81fgg2step2305)), _umlkckdg = __81fgg2dlsvn2305; __81fgg2count2305 != 0; __81fgg2count2305--, _umlkckdg += (__81fgg2step2305)) {

								{
									
									*(_ppzorcqs+(_umlkckdg - 1) + (_5kucxo3c - 1) * 1 * (_uq25zlw0)) = _d0547bi2;
Mark180:;
									// continue
								}
																}							}//* 
							
						}
						else
						if (_f7059815 == (int)1)
						{
							//*                 ------------------------------ 
							//*                 version 1: back-transform each vector with GEMV, Q*x. 
							
							if (_1ub95eoc < _dxpq0xkr)
							_t5wmtd1j("N" ,ref _dxpq0xkr ,ref Unsafe.AsRef(_dxpq0xkr - _1ub95eoc) ,ref Unsafe.AsRef(_kxg5drh2) ,(_ppzorcqs+((int)1 - 1) + (_1ub95eoc + (int)1 - 1) * 1 * (_uq25zlw0)),ref _uq25zlw0 ,(_apig8meb+((_1ub95eoc + (int)1) + (_uicet2a7 * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_apig8meb+(_1ub95eoc + (_uicet2a7 * _dxpq0xkr) - 1))) ,(_ppzorcqs+((int)1 - 1) + (_1ub95eoc - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );//* 
							
							_retbwjxi = _ei7om7ok(ref _dxpq0xkr ,(_ppzorcqs+((int)1 - 1) + (_1ub95eoc - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );
							_343rc715 = (_kxg5drh2 / ILNumerics.F2NET.Intrinsics.ABS(*(_ppzorcqs+(_retbwjxi - 1) + (_1ub95eoc - 1) * 1 * (_uq25zlw0)) ));
							_f6jqcjk1(ref _dxpq0xkr ,ref _343rc715 ,(_ppzorcqs+((int)1 - 1) + (_1ub95eoc - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );//* 
							
						}
						else
						{
							//*                 ------------------------------ 
							//*                 version 2: back-transform block of vectors with GEMM 
							//*                 zero out above vector 
							//*                 could go from KI-NV+1 to KI-1 
							
							{
								System.Int32 __81fgg2dlsvn2306 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step2306 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2306;
								for (__81fgg2count2306 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1ub95eoc - (int)1) - __81fgg2dlsvn2306 + __81fgg2step2306) / __81fgg2step2306)), _umlkckdg = __81fgg2dlsvn2306; __81fgg2count2306 != 0; __81fgg2count2306--, _umlkckdg += (__81fgg2step2306)) {

								{
									
									*(_apig8meb+(_umlkckdg + (_uicet2a7 * _dxpq0xkr) - 1)) = _d0547bi2;
								}
																}							}
							*(_x49mm45v+(_uicet2a7 - 1)) = _8t9w2q8d;//*                 back-transform and normalization is done below 
							
						}
						
					}
					else
					{
						//* 
						//*              -------------------------------------------------------- 
						//*              Complex left eigenvector. 
						//* 
						//*              Initial solve: 
						//*              [ ( T(KI,KI)    T(KI,KI+1)  )**T - (WR - I* WI) ]*X = 0. 
						//*              [ ( T(KI+1,KI) T(KI+1,KI+1) )                   ] 
						//* 
						
						if (ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+(_1ub95eoc - 1) + (_1ub95eoc + (int)1 - 1) * 1 * (_w8yhbr2r)) ) >= ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+(_1ub95eoc + (int)1 - 1) + (_1ub95eoc - 1) * 1 * (_w8yhbr2r)) ))
						{
							
							*(_apig8meb+(_1ub95eoc + ((_uicet2a7) * _dxpq0xkr) - 1)) = (_nc0qphpn / *(_2ivtt43r+(_1ub95eoc - 1) + (_1ub95eoc + (int)1 - 1) * 1 * (_w8yhbr2r)));
							*(_apig8meb+((_1ub95eoc + (int)1) + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1)) = _kxg5drh2;
						}
						else
						{
							
							*(_apig8meb+(_1ub95eoc + ((_uicet2a7) * _dxpq0xkr) - 1)) = _kxg5drh2;
							*(_apig8meb+((_1ub95eoc + (int)1) + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1)) = (-((_nc0qphpn / *(_2ivtt43r+(_1ub95eoc + (int)1 - 1) + (_1ub95eoc - 1) * 1 * (_w8yhbr2r)))));
						}
						
						*(_apig8meb+((_1ub95eoc + (int)1) + ((_uicet2a7) * _dxpq0xkr) - 1)) = _d0547bi2;
						*(_apig8meb+(_1ub95eoc + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1)) = _d0547bi2;//* 
						//*              Form right-hand side. 
						//* 
						
						{
							System.Int32 __81fgg2dlsvn2307 = (System.Int32)((_1ub95eoc + (int)2));
							const System.Int32 __81fgg2step2307 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2307;
							for (__81fgg2count2307 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2307 + __81fgg2step2307) / __81fgg2step2307)), _umlkckdg = __81fgg2dlsvn2307; __81fgg2count2307 != 0; __81fgg2count2307--, _umlkckdg += (__81fgg2step2307)) {

							{
								
								*(_apig8meb+(_umlkckdg + ((_uicet2a7) * _dxpq0xkr) - 1)) = (-((*(_apig8meb+(_1ub95eoc + ((_uicet2a7) * _dxpq0xkr) - 1)) * *(_2ivtt43r+(_1ub95eoc - 1) + (_umlkckdg - 1) * 1 * (_w8yhbr2r)))));
								*(_apig8meb+(_umlkckdg + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1)) = (-((*(_apig8meb+((_1ub95eoc + (int)1) + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1)) * *(_2ivtt43r+(_1ub95eoc + (int)1 - 1) + (_umlkckdg - 1) * 1 * (_w8yhbr2r)))));
Mark190:;
								// continue
							}
														}						}//* 
						//*              Solve transposed quasi-triangular system: 
						//*              [ T(KI+2:N,KI+2:N)**T - (WR-i*WI) ]*X = WORK1+i*WORK2 
						//* 
						
						_2wzbu6qf = _kxg5drh2;
						_69yw1gah = _av7j8yda;//* 
						
						_olmbz2a3 = (_1ub95eoc + (int)2);
						{
							System.Int32 __81fgg2dlsvn2308 = (System.Int32)((_1ub95eoc + (int)2));
							const System.Int32 __81fgg2step2308 = (System.Int32)((int)1);
							System.Int32 __81fgg2count2308;
							for (__81fgg2count2308 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2308 + __81fgg2step2308) / __81fgg2step2308)), _znpjgsef = __81fgg2dlsvn2308; __81fgg2count2308 != 0; __81fgg2count2308--, _znpjgsef += (__81fgg2step2308)) {

							{
								
								if (_znpjgsef < _olmbz2a3)goto Mark200;
								_dk3nh7xl = _znpjgsef;
								_psodi8to = _znpjgsef;
								_olmbz2a3 = (_znpjgsef + (int)1);
								if (_znpjgsef < _dxpq0xkr)
								{
									
									if (*(_2ivtt43r+(_znpjgsef + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)) != _d0547bi2)
									{
										
										_psodi8to = (_znpjgsef + (int)1);
										_olmbz2a3 = (_znpjgsef + (int)2);
									}
									
								}
								//* 
								
								if (_dk3nh7xl == _psodi8to)
								{
									//* 
									//*                    1-by-1 diagonal block 
									//* 
									//*                    Scale if necessary to avoid overflow when 
									//*                    forming the right-hand side elements. 
									//* 
									
									if (*(_apig8meb+(_znpjgsef - 1)) > _69yw1gah)
									{
										
										_egqfb6nh = (_kxg5drh2 / _2wzbu6qf);
										_f6jqcjk1(ref Unsafe.AsRef((_dxpq0xkr - _1ub95eoc) + (int)1) ,ref _egqfb6nh ,(_apig8meb+(_1ub95eoc + ((_uicet2a7) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
										_f6jqcjk1(ref Unsafe.AsRef((_dxpq0xkr - _1ub95eoc) + (int)1) ,ref _egqfb6nh ,(_apig8meb+(_1ub95eoc + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
										_2wzbu6qf = _kxg5drh2;
										_69yw1gah = _av7j8yda;
									}
									//* 
									
									*(_apig8meb+(_znpjgsef + ((_uicet2a7) * _dxpq0xkr) - 1)) = (*(_apig8meb+(_znpjgsef + ((_uicet2a7) * _dxpq0xkr) - 1)) - _le984b8z(ref Unsafe.AsRef((_znpjgsef - _1ub95eoc) - (int)2) ,(_2ivtt43r+(_1ub95eoc + (int)2 - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((_1ub95eoc + (int)2) + ((_uicet2a7) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) ));
									*(_apig8meb+(_znpjgsef + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1)) = (*(_apig8meb+(_znpjgsef + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1)) - _le984b8z(ref Unsafe.AsRef((_znpjgsef - _1ub95eoc) - (int)2) ,(_2ivtt43r+(_1ub95eoc + (int)2 - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((_1ub95eoc + (int)2) + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) ));//* 
									//*                    Solve [ T(J,J)-(WR-i*WI) ]*(X11+i*X12)= WK+I*WK2 
									//* 
									
									_6andobxo(ref Unsafe.AsRef(false) ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef((int)2) ,ref _rhnpgpoi ,ref Unsafe.AsRef(_kxg5drh2) ,(_2ivtt43r+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,ref Unsafe.AsRef(_kxg5drh2) ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+(_znpjgsef + (_uicet2a7 * _dxpq0xkr) - 1)),ref _dxpq0xkr ,ref _b5j6m2b7 ,ref Unsafe.AsRef(-(_nc0qphpn)) ,_ta7zuy9k ,ref Unsafe.AsRef((int)2) ,ref _1m44vtuk ,ref _ziu6urj2 ,ref _bhsiylw4 );//* 
									//*                    Scale if necessary 
									//* 
									
									if (_1m44vtuk != _kxg5drh2)
									{
										
										_f6jqcjk1(ref Unsafe.AsRef((_dxpq0xkr - _1ub95eoc) + (int)1) ,ref _1m44vtuk ,(_apig8meb+(_1ub95eoc + ((_uicet2a7) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
										_f6jqcjk1(ref Unsafe.AsRef((_dxpq0xkr - _1ub95eoc) + (int)1) ,ref _1m44vtuk ,(_apig8meb+(_1ub95eoc + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
									}
									
									*(_apig8meb+(_znpjgsef + ((_uicet2a7) * _dxpq0xkr) - 1)) = *(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2));
									*(_apig8meb+(_znpjgsef + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1)) = *(_ta7zuy9k+((int)1 - 1) + ((int)2 - 1) * 1 * ((int)2));
									_2wzbu6qf = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+(_znpjgsef + ((_uicet2a7) * _dxpq0xkr) - 1)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+(_znpjgsef + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1)) ) ,_2wzbu6qf );
									_69yw1gah = (_av7j8yda / _2wzbu6qf);//* 
									
								}
								else
								{
									//* 
									//*                    2-by-2 diagonal block 
									//* 
									//*                    Scale if necessary to avoid overflow when forming 
									//*                    the right-hand side elements. 
									//* 
									
									_bafcbx97 = ILNumerics.F2NET.Intrinsics.MAX(*(_apig8meb+(_znpjgsef - 1)) ,*(_apig8meb+(_znpjgsef + (int)1 - 1)) );
									if (_bafcbx97 > _69yw1gah)
									{
										
										_egqfb6nh = (_kxg5drh2 / _2wzbu6qf);
										_f6jqcjk1(ref Unsafe.AsRef((_dxpq0xkr - _1ub95eoc) + (int)1) ,ref _egqfb6nh ,(_apig8meb+(_1ub95eoc + ((_uicet2a7) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
										_f6jqcjk1(ref Unsafe.AsRef((_dxpq0xkr - _1ub95eoc) + (int)1) ,ref _egqfb6nh ,(_apig8meb+(_1ub95eoc + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
										_2wzbu6qf = _kxg5drh2;
										_69yw1gah = _av7j8yda;
									}
									//* 
									
									*(_apig8meb+(_znpjgsef + ((_uicet2a7) * _dxpq0xkr) - 1)) = (*(_apig8meb+(_znpjgsef + ((_uicet2a7) * _dxpq0xkr) - 1)) - _le984b8z(ref Unsafe.AsRef((_znpjgsef - _1ub95eoc) - (int)2) ,(_2ivtt43r+(_1ub95eoc + (int)2 - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((_1ub95eoc + (int)2) + ((_uicet2a7) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) ));//* 
									
									*(_apig8meb+(_znpjgsef + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1)) = (*(_apig8meb+(_znpjgsef + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1)) - _le984b8z(ref Unsafe.AsRef((_znpjgsef - _1ub95eoc) - (int)2) ,(_2ivtt43r+(_1ub95eoc + (int)2 - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((_1ub95eoc + (int)2) + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) ));//* 
									
									*(_apig8meb+((_znpjgsef + (int)1) + ((_uicet2a7) * _dxpq0xkr) - 1)) = (*(_apig8meb+((_znpjgsef + (int)1) + ((_uicet2a7) * _dxpq0xkr) - 1)) - _le984b8z(ref Unsafe.AsRef((_znpjgsef - _1ub95eoc) - (int)2) ,(_2ivtt43r+(_1ub95eoc + (int)2 - 1) + (_znpjgsef + (int)1 - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((_1ub95eoc + (int)2) + ((_uicet2a7) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) ));//* 
									
									*(_apig8meb+((_znpjgsef + (int)1) + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1)) = (*(_apig8meb+((_znpjgsef + (int)1) + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1)) - _le984b8z(ref Unsafe.AsRef((_znpjgsef - _1ub95eoc) - (int)2) ,(_2ivtt43r+(_1ub95eoc + (int)2 - 1) + (_znpjgsef + (int)1 - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_apig8meb+((_1ub95eoc + (int)2) + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) ));//* 
									//*                    Solve 2-by-2 complex linear equation 
									//*                    [ (T(j,j)   T(j,j+1)  )**T - (wr-i*wi)*I ]*X = SCALE*B 
									//*                    [ (T(j+1,j) T(j+1,j+1))                  ] 
									//* 
									
									_6andobxo(ref Unsafe.AsRef(true) ,ref Unsafe.AsRef((int)2) ,ref Unsafe.AsRef((int)2) ,ref _rhnpgpoi ,ref Unsafe.AsRef(_kxg5drh2) ,(_2ivtt43r+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,ref Unsafe.AsRef(_kxg5drh2) ,ref Unsafe.AsRef(_kxg5drh2) ,(_apig8meb+(_znpjgsef + (_uicet2a7 * _dxpq0xkr) - 1)),ref _dxpq0xkr ,ref _b5j6m2b7 ,ref Unsafe.AsRef(-(_nc0qphpn)) ,_ta7zuy9k ,ref Unsafe.AsRef((int)2) ,ref _1m44vtuk ,ref _ziu6urj2 ,ref _bhsiylw4 );//* 
									//*                    Scale if necessary 
									//* 
									
									if (_1m44vtuk != _kxg5drh2)
									{
										
										_f6jqcjk1(ref Unsafe.AsRef((_dxpq0xkr - _1ub95eoc) + (int)1) ,ref _1m44vtuk ,(_apig8meb+(_1ub95eoc + ((_uicet2a7) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
										_f6jqcjk1(ref Unsafe.AsRef((_dxpq0xkr - _1ub95eoc) + (int)1) ,ref _1m44vtuk ,(_apig8meb+(_1ub95eoc + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
									}
									
									*(_apig8meb+(_znpjgsef + ((_uicet2a7) * _dxpq0xkr) - 1)) = *(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2));
									*(_apig8meb+(_znpjgsef + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1)) = *(_ta7zuy9k+((int)1 - 1) + ((int)2 - 1) * 1 * ((int)2));
									*(_apig8meb+((_znpjgsef + (int)1) + ((_uicet2a7) * _dxpq0xkr) - 1)) = *(_ta7zuy9k+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2));
									*(_apig8meb+((_znpjgsef + (int)1) + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1)) = *(_ta7zuy9k+((int)2 - 1) + ((int)2 - 1) * 1 * ((int)2));
									_2wzbu6qf = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+((int)1 - 1) + ((int)2 - 1) * 1 * ((int)2)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+((int)2 - 1) + ((int)2 - 1) * 1 * ((int)2)) ) ,_2wzbu6qf );
									_69yw1gah = (_av7j8yda / _2wzbu6qf);//* 
									
								}
								
Mark200:;
								// continue
							}
														}						}//* 
						//*              Copy the vector x or Q*x to VL and normalize. 
						//* 
						
						if (!(_htf5ro8d))
						{
							//*                 ------------------------------ 
							//*                 no back-transform: copy x to VL and normalize. 
							
							_gvjhlct0(ref Unsafe.AsRef((_dxpq0xkr - _1ub95eoc) + (int)1) ,(_apig8meb+(_1ub95eoc + ((_uicet2a7) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) ,(_ppzorcqs+(_1ub95eoc - 1) + (_5kucxo3c - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );
							_gvjhlct0(ref Unsafe.AsRef((_dxpq0xkr - _1ub95eoc) + (int)1) ,(_apig8meb+(_1ub95eoc + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) ,(_ppzorcqs+(_1ub95eoc - 1) + (_5kucxo3c + (int)1 - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );//* 
							
							_trz23puj = _d0547bi2;
							{
								System.Int32 __81fgg2dlsvn2309 = (System.Int32)(_1ub95eoc);
								const System.Int32 __81fgg2step2309 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2309;
								for (__81fgg2count2309 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2309 + __81fgg2step2309) / __81fgg2step2309)), _umlkckdg = __81fgg2dlsvn2309; __81fgg2count2309 != 0; __81fgg2count2309--, _umlkckdg += (__81fgg2step2309)) {

								{
									
									_trz23puj = ILNumerics.F2NET.Intrinsics.MAX(_trz23puj ,ILNumerics.F2NET.Intrinsics.ABS(*(_ppzorcqs+(_umlkckdg - 1) + (_5kucxo3c - 1) * 1 * (_uq25zlw0)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_ppzorcqs+(_umlkckdg - 1) + (_5kucxo3c + (int)1 - 1) * 1 * (_uq25zlw0)) ) );
Mark220:;
									// continue
								}
																}							}
							_343rc715 = (_kxg5drh2 / _trz23puj);
							_f6jqcjk1(ref Unsafe.AsRef((_dxpq0xkr - _1ub95eoc) + (int)1) ,ref _343rc715 ,(_ppzorcqs+(_1ub95eoc - 1) + (_5kucxo3c - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );
							_f6jqcjk1(ref Unsafe.AsRef((_dxpq0xkr - _1ub95eoc) + (int)1) ,ref _343rc715 ,(_ppzorcqs+(_1ub95eoc - 1) + (_5kucxo3c + (int)1 - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );//* 
							
							{
								System.Int32 __81fgg2dlsvn2310 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step2310 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2310;
								for (__81fgg2count2310 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1ub95eoc - (int)1) - __81fgg2dlsvn2310 + __81fgg2step2310) / __81fgg2step2310)), _umlkckdg = __81fgg2dlsvn2310; __81fgg2count2310 != 0; __81fgg2count2310--, _umlkckdg += (__81fgg2step2310)) {

								{
									
									*(_ppzorcqs+(_umlkckdg - 1) + (_5kucxo3c - 1) * 1 * (_uq25zlw0)) = _d0547bi2;
									*(_ppzorcqs+(_umlkckdg - 1) + (_5kucxo3c + (int)1 - 1) * 1 * (_uq25zlw0)) = _d0547bi2;
Mark230:;
									// continue
								}
																}							}//* 
							
						}
						else
						if (_f7059815 == (int)1)
						{
							//*                 ------------------------------ 
							//*                 version 1: back-transform each vector with GEMV, Q*x. 
							
							if (_1ub95eoc < (_dxpq0xkr - (int)1))
							{
								
								_t5wmtd1j("N" ,ref _dxpq0xkr ,ref Unsafe.AsRef((_dxpq0xkr - _1ub95eoc) - (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_ppzorcqs+((int)1 - 1) + (_1ub95eoc + (int)2 - 1) * 1 * (_uq25zlw0)),ref _uq25zlw0 ,(_apig8meb+((_1ub95eoc + (int)2) + ((_uicet2a7) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_apig8meb+(_1ub95eoc + ((_uicet2a7) * _dxpq0xkr) - 1))) ,(_ppzorcqs+((int)1 - 1) + (_1ub95eoc - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );
								_t5wmtd1j("N" ,ref _dxpq0xkr ,ref Unsafe.AsRef((_dxpq0xkr - _1ub95eoc) - (int)1) ,ref Unsafe.AsRef(_kxg5drh2) ,(_ppzorcqs+((int)1 - 1) + (_1ub95eoc + (int)2 - 1) * 1 * (_uq25zlw0)),ref _uq25zlw0 ,(_apig8meb+((_1ub95eoc + (int)2) + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(*(_apig8meb+((_1ub95eoc + (int)1) + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1))) ,(_ppzorcqs+((int)1 - 1) + (_1ub95eoc + (int)1 - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );
							}
							else
							{
								
								_f6jqcjk1(ref _dxpq0xkr ,ref Unsafe.AsRef(*(_apig8meb+(_1ub95eoc + ((_uicet2a7) * _dxpq0xkr) - 1))) ,(_ppzorcqs+((int)1 - 1) + (_1ub95eoc - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );
								_f6jqcjk1(ref _dxpq0xkr ,ref Unsafe.AsRef(*(_apig8meb+((_1ub95eoc + (int)1) + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1))) ,(_ppzorcqs+((int)1 - 1) + (_1ub95eoc + (int)1 - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );
							}
							//* 
							
							_trz23puj = _d0547bi2;
							{
								System.Int32 __81fgg2dlsvn2311 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step2311 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2311;
								for (__81fgg2count2311 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2311 + __81fgg2step2311) / __81fgg2step2311)), _umlkckdg = __81fgg2dlsvn2311; __81fgg2count2311 != 0; __81fgg2count2311--, _umlkckdg += (__81fgg2step2311)) {

								{
									
									_trz23puj = ILNumerics.F2NET.Intrinsics.MAX(_trz23puj ,ILNumerics.F2NET.Intrinsics.ABS(*(_ppzorcqs+(_umlkckdg - 1) + (_1ub95eoc - 1) * 1 * (_uq25zlw0)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_ppzorcqs+(_umlkckdg - 1) + (_1ub95eoc + (int)1 - 1) * 1 * (_uq25zlw0)) ) );
Mark240:;
									// continue
								}
																}							}
							_343rc715 = (_kxg5drh2 / _trz23puj);
							_f6jqcjk1(ref _dxpq0xkr ,ref _343rc715 ,(_ppzorcqs+((int)1 - 1) + (_1ub95eoc - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );
							_f6jqcjk1(ref _dxpq0xkr ,ref _343rc715 ,(_ppzorcqs+((int)1 - 1) + (_1ub95eoc + (int)1 - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );//* 
							
						}
						else
						{
							//*                 ------------------------------ 
							//*                 version 2: back-transform block of vectors with GEMM 
							//*                 zero out above vector 
							//*                 could go from KI-NV+1 to KI-1 
							
							{
								System.Int32 __81fgg2dlsvn2312 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step2312 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2312;
								for (__81fgg2count2312 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1ub95eoc - (int)1) - __81fgg2dlsvn2312 + __81fgg2step2312) / __81fgg2step2312)), _umlkckdg = __81fgg2dlsvn2312; __81fgg2count2312 != 0; __81fgg2count2312--, _umlkckdg += (__81fgg2step2312)) {

								{
									
									*(_apig8meb+(_umlkckdg + ((_uicet2a7) * _dxpq0xkr) - 1)) = _d0547bi2;
									*(_apig8meb+(_umlkckdg + ((_uicet2a7 + (int)1) * _dxpq0xkr) - 1)) = _d0547bi2;
								}
																}							}
							*(_x49mm45v+(_uicet2a7 - 1)) = _8t9w2q8d;
							*(_x49mm45v+(_uicet2a7 + (int)1 - 1)) = (-(_8t9w2q8d));
							_uicet2a7 = (_uicet2a7 + (int)1);//*                 back-transform and normalization is done below 
							
						}
						
					}
					// 
					
					if (_f7059815 > (int)1)
					{
						//*              -------------------------------------------------------- 
						//*              Blocked version of back-transform 
						//*              For complex case, KI2 includes both vectors (KI and KI+1) 
						
						if (_8t9w2q8d == (int)0)
						{
							
							_sjhh7htz = _1ub95eoc;
						}
						else
						{
							
							_sjhh7htz = (_1ub95eoc + (int)1);
						}
						// 
						//*              Columns 1:IV of work are valid vectors. 
						//*              When the number of vectors stored reaches NB-1 or NB, 
						//*              or if this was last vector, do the GEMM 
						
						if ((_uicet2a7 >= (_f7059815 - (int)1)) | (_sjhh7htz == _dxpq0xkr))
						{
							
							_5nsxi69c("N" ,"N" ,ref _dxpq0xkr ,ref _uicet2a7 ,ref Unsafe.AsRef((_dxpq0xkr - _sjhh7htz) + _uicet2a7) ,ref Unsafe.AsRef(_kxg5drh2) ,(_ppzorcqs+((int)1 - 1) + ((_sjhh7htz - _uicet2a7) + (int)1 - 1) * 1 * (_uq25zlw0)),ref _uq25zlw0 ,(_apig8meb+(((_sjhh7htz - _uicet2a7) + (int)1) + (((int)1) * _dxpq0xkr) - 1)),ref _dxpq0xkr ,ref Unsafe.AsRef(_d0547bi2) ,(_apig8meb+((int)1 + ((_f7059815 + (int)1) * _dxpq0xkr) - 1)),ref _dxpq0xkr );//*                 normalize vectors 
							
							{
								System.Int32 __81fgg2dlsvn2313 = (System.Int32)((int)1);
								const System.Int32 __81fgg2step2313 = (System.Int32)((int)1);
								System.Int32 __81fgg2count2313;
								for (__81fgg2count2313 = System.Math.Max(0, (System.Int32)(((System.Int32)(_uicet2a7) - __81fgg2dlsvn2313 + __81fgg2step2313) / __81fgg2step2313)), _umlkckdg = __81fgg2dlsvn2313; __81fgg2count2313 != 0; __81fgg2count2313--, _umlkckdg += (__81fgg2step2313)) {

								{
									
									if (*(_x49mm45v+(_umlkckdg - 1)) == (int)0)
									{
										//*                       real eigenvector 
										
										_retbwjxi = _ei7om7ok(ref _dxpq0xkr ,(_apig8meb+((int)1 + ((_f7059815 + _umlkckdg) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
										_343rc715 = (_kxg5drh2 / ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+(_retbwjxi + ((_f7059815 + _umlkckdg) * _dxpq0xkr) - 1)) ));
									}
									else
									if (*(_x49mm45v+(_umlkckdg - 1)) == (int)1)
									{
										//*                       first eigenvector of conjugate pair 
										
										_trz23puj = _d0547bi2;
										{
											System.Int32 __81fgg2dlsvn2314 = (System.Int32)((int)1);
											const System.Int32 __81fgg2step2314 = (System.Int32)((int)1);
											System.Int32 __81fgg2count2314;
											for (__81fgg2count2314 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2314 + __81fgg2step2314) / __81fgg2step2314)), _retbwjxi = __81fgg2dlsvn2314; __81fgg2count2314 != 0; __81fgg2count2314--, _retbwjxi += (__81fgg2step2314)) {

											{
												
												_trz23puj = ILNumerics.F2NET.Intrinsics.MAX(_trz23puj ,ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+(_retbwjxi + ((_f7059815 + _umlkckdg) * _dxpq0xkr) - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_apig8meb+(_retbwjxi + (((_f7059815 + _umlkckdg) + (int)1) * _dxpq0xkr) - 1)) ) );
											}
																						}										}
										_343rc715 = (_kxg5drh2 / _trz23puj);//*                    else if ISCOMPLEX(K).EQ.-1 
										//*                       second eigenvector of conjugate pair 
										//*                       reuse same REMAX as previous K 
										
									}
									
									_f6jqcjk1(ref _dxpq0xkr ,ref _343rc715 ,(_apig8meb+((int)1 + ((_f7059815 + _umlkckdg) * _dxpq0xkr) - 1)),ref Unsafe.AsRef((int)1) );
								}
																}							}
							_hhtvj1kb("F" ,ref _dxpq0xkr ,ref _uicet2a7 ,(_apig8meb+((int)1 + ((_f7059815 + (int)1) * _dxpq0xkr) - 1)),ref _dxpq0xkr ,(_ppzorcqs+((int)1 - 1) + ((_sjhh7htz - _uicet2a7) + (int)1 - 1) * 1 * (_uq25zlw0)),ref _uq25zlw0 );
							_uicet2a7 = (int)1;
						}
						else
						{
							
							_uicet2a7 = (_uicet2a7 + (int)1);
						}
						
					}
					//* 
					
					_5kucxo3c = (_5kucxo3c + (int)1);
					if (_8t9w2q8d != (int)0)
					_5kucxo3c = (_5kucxo3c + (int)1);
Mark260:;
					// continue
				}
								}			}
		}
		//* 
		
		return;//* 
		//*     End of DTREVC3 
		//* 
		
	}
	
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
