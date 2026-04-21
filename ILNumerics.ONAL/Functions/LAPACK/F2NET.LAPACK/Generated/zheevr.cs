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
//*> \brief <b> ZHEEVR computes the eigenvalues and, optionally, the left and/or right eigenvectors for HE matrices</b> 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZHEEVR + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zheevr.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zheevr.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zheevr.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZHEEVR( JOBZ, RANGE, UPLO, N, A, LDA, VL, VU, IL, IU, 
//*                          ABSTOL, M, W, Z, LDZ, ISUPPZ, WORK, LWORK, 
//*                          RWORK, LRWORK, IWORK, LIWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          JOBZ, RANGE, UPLO 
//*       INTEGER            IL, INFO, IU, LDA, LDZ, LIWORK, LRWORK, LWORK, 
//*      $                   M, N 
//*       DOUBLE PRECISION   ABSTOL, VL, VU 
//*       .. 
//*       .. Array Arguments .. 
//*       INTEGER            ISUPPZ( * ), IWORK( * ) 
//*       DOUBLE PRECISION   RWORK( * ), W( * ) 
//*       COMPLEX*16         A( LDA, * ), WORK( * ), Z( LDZ, * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZHEEVR computes selected eigenvalues and, optionally, eigenvectors 
//*> of a complex Hermitian matrix A.  Eigenvalues and eigenvectors can 
//*> be selected by specifying either a range of values or a range of 
//*> indices for the desired eigenvalues. 
//*> 
//*> ZHEEVR first reduces the matrix A to tridiagonal form T with a call 
//*> to ZHETRD.  Then, whenever possible, ZHEEVR calls ZSTEMR to compute 
//*> eigenspectrum using Relatively Robust Representations.  ZSTEMR 
//*> computes eigenvalues by the dqds algorithm, while orthogonal 
//*> eigenvectors are computed from various "good" L D L^T representations 
//*> (also known as Relatively Robust Representations). Gram-Schmidt 
//*> orthogonalization is avoided as far as possible. More specifically, 
//*> the various steps of the algorithm are as follows. 
//*> 
//*> For each unreduced block (submatrix) of T, 
//*>    (a) Compute T - sigma I  = L D L^T, so that L and D 
//*>        define all the wanted eigenvalues to high relative accuracy. 
//*>        This means that small relative changes in the entries of D and L 
//*>        cause only small relative changes in the eigenvalues and 
//*>        eigenvectors. The standard (unfactored) representation of the 
//*>        tridiagonal matrix T does not have this property in general. 
//*>    (b) Compute the eigenvalues to suitable accuracy. 
//*>        If the eigenvectors are desired, the algorithm attains full 
//*>        accuracy of the computed eigenvalues only right before 
//*>        the corresponding vectors have to be computed, see steps c) and d). 
//*>    (c) For each cluster of close eigenvalues, select a new 
//*>        shift close to the cluster, find a new factorization, and refine 
//*>        the shifted eigenvalues to suitable accuracy. 
//*>    (d) For each eigenvalue with a large enough relative separation compute 
//*>        the corresponding eigenvector by forming a rank revealing twisted 
//*>        factorization. Go back to (c) for any clusters that remain. 
//*> 
//*> The desired accuracy of the output can be specified by the input 
//*> parameter ABSTOL. 
//*> 
//*> For more details, see DSTEMR's documentation and: 
//*> - Inderjit S. Dhillon and Beresford N. Parlett: "Multiple representations 
//*>   to compute orthogonal eigenvectors of symmetric tridiagonal matrices," 
//*>   Linear Algebra and its Applications, 387(1), pp. 1-28, August 2004. 
//*> - Inderjit Dhillon and Beresford Parlett: "Orthogonal Eigenvectors and 
//*>   Relative Gaps," SIAM Journal on Matrix Analysis and Applications, Vol. 25, 
//*>   2004.  Also LAPACK Working Note 154. 
//*> - Inderjit Dhillon: "A new O(n^2) algorithm for the symmetric 
//*>   tridiagonal eigenvalue/eigenvector problem", 
//*>   Computer Science Division Technical Report No. UCB/CSD-97-971, 
//*>   UC Berkeley, May 1997. 
//*> 
//*> 
//*> Note 1 : ZHEEVR calls ZSTEMR when the full spectrum is requested 
//*> on machines which conform to the ieee-754 floating point standard. 
//*> ZHEEVR calls DSTEBZ and ZSTEIN on non-ieee machines and 
//*> when partial spectrum requests are made. 
//*> 
//*> Normal execution of ZSTEMR may create NaNs and infinities and 
//*> hence may abort due to a floating point exception in environments 
//*> which do not handle NaNs and infinities in the ieee standard default 
//*> manner. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] JOBZ 
//*> \verbatim 
//*>          JOBZ is CHARACTER*1 
//*>          = 'N':  Compute eigenvalues only; 
//*>          = 'V':  Compute eigenvalues and eigenvectors. 
//*> \endverbatim 
//*> 
//*> \param[in] RANGE 
//*> \verbatim 
//*>          RANGE is CHARACTER*1 
//*>          = 'A': all eigenvalues will be found. 
//*>          = 'V': all eigenvalues in the half-open interval (VL,VU] 
//*>                 will be found. 
//*>          = 'I': the IL-th through IU-th eigenvalues will be found. 
//*>          For RANGE = 'V' or 'I' and IU - IL < N - 1, DSTEBZ and 
//*>          ZSTEIN are called 
//*> \endverbatim 
//*> 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>          = 'U':  Upper triangle of A is stored; 
//*>          = 'L':  Lower triangle of A is stored. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix A.  N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension (LDA, N) 
//*>          On entry, the Hermitian matrix A.  If UPLO = 'U', the 
//*>          leading N-by-N upper triangular part of A contains the 
//*>          upper triangular part of the matrix A.  If UPLO = 'L', 
//*>          the leading N-by-N lower triangular part of A contains 
//*>          the lower triangular part of the matrix A. 
//*>          On exit, the lower triangle (if UPLO='L') or the upper 
//*>          triangle (if UPLO='U') of A, including the diagonal, is 
//*>          destroyed. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[in] VL 
//*> \verbatim 
//*>          VL is DOUBLE PRECISION 
//*>          If RANGE='V', the lower bound of the interval to 
//*>          be searched for eigenvalues. VL < VU. 
//*>          Not referenced if RANGE = 'A' or 'I'. 
//*> \endverbatim 
//*> 
//*> \param[in] VU 
//*> \verbatim 
//*>          VU is DOUBLE PRECISION 
//*>          If RANGE='V', the upper bound of the interval to 
//*>          be searched for eigenvalues. VL < VU. 
//*>          Not referenced if RANGE = 'A' or 'I'. 
//*> \endverbatim 
//*> 
//*> \param[in] IL 
//*> \verbatim 
//*>          IL is INTEGER 
//*>          If RANGE='I', the index of the 
//*>          smallest eigenvalue to be returned. 
//*>          1 <= IL <= IU <= N, if N > 0; IL = 1 and IU = 0 if N = 0. 
//*>          Not referenced if RANGE = 'A' or 'V'. 
//*> \endverbatim 
//*> 
//*> \param[in] IU 
//*> \verbatim 
//*>          IU is INTEGER 
//*>          If RANGE='I', the index of the 
//*>          largest eigenvalue to be returned. 
//*>          1 <= IL <= IU <= N, if N > 0; IL = 1 and IU = 0 if N = 0. 
//*>          Not referenced if RANGE = 'A' or 'V'. 
//*> \endverbatim 
//*> 
//*> \param[in] ABSTOL 
//*> \verbatim 
//*>          ABSTOL is DOUBLE PRECISION 
//*>          The absolute error tolerance for the eigenvalues. 
//*>          An approximate eigenvalue is accepted as converged 
//*>          when it is determined to lie in an interval [a,b] 
//*>          of width less than or equal to 
//*> 
//*>                  ABSTOL + EPS *   max( |a|,|b| ) , 
//*> 
//*>          where EPS is the machine precision.  If ABSTOL is less than 
//*>          or equal to zero, then  EPS*|T|  will be used in its place, 
//*>          where |T| is the 1-norm of the tridiagonal matrix obtained 
//*>          by reducing A to tridiagonal form. 
//*> 
//*>          See "Computing Small Singular Values of Bidiagonal Matrices 
//*>          with Guaranteed High Relative Accuracy," by Demmel and 
//*>          Kahan, LAPACK Working Note #3. 
//*> 
//*>          If high relative accuracy is important, set ABSTOL to 
//*>          DLAMCH( 'Safe minimum' ).  Doing so will guarantee that 
//*>          eigenvalues are computed to high relative accuracy when 
//*>          possible in future releases.  The current code does not 
//*>          make any guarantees about high relative accuracy, but 
//*>          future releases will. See J. Barlow and J. Demmel, 
//*>          "Computing Accurate Eigensystems of Scaled Diagonally 
//*>          Dominant Matrices", LAPACK Working Note #7, for a discussion 
//*>          of which matrices define their eigenvalues to high relative 
//*>          accuracy. 
//*> \endverbatim 
//*> 
//*> \param[out] M 
//*> \verbatim 
//*>          M is INTEGER 
//*>          The total number of eigenvalues found.  0 <= M <= N. 
//*>          If RANGE = 'A', M = N, and if RANGE = 'I', M = IU-IL+1. 
//*> \endverbatim 
//*> 
//*> \param[out] W 
//*> \verbatim 
//*>          W is DOUBLE PRECISION array, dimension (N) 
//*>          The first M elements contain the selected eigenvalues in 
//*>          ascending order. 
//*> \endverbatim 
//*> 
//*> \param[out] Z 
//*> \verbatim 
//*>          Z is COMPLEX*16 array, dimension (LDZ, max(1,M)) 
//*>          If JOBZ = 'V', then if INFO = 0, the first M columns of Z 
//*>          contain the orthonormal eigenvectors of the matrix A 
//*>          corresponding to the selected eigenvalues, with the i-th 
//*>          column of Z holding the eigenvector associated with W(i). 
//*>          If JOBZ = 'N', then Z is not referenced. 
//*>          Note: the user must ensure that at least max(1,M) columns are 
//*>          supplied in the array Z; if RANGE = 'V', the exact value of M 
//*>          is not known in advance and an upper bound must be used. 
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
//*>          indicating the nonzero elements in Z. The i-th eigenvector 
//*>          is nonzero only in elements ISUPPZ( 2*i-1 ) through 
//*>          ISUPPZ( 2*i ). This is an output of ZSTEMR (tridiagonal 
//*>          matrix). The support of the eigenvectors of A is typically 
//*>          1:N because of the unitary transformations applied by ZUNMTR. 
//*>          Implemented only for RANGE = 'A' or 'I' and IU - IL = N - 1 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is COMPLEX*16 array, dimension (MAX(1,LWORK)) 
//*>          On exit, if INFO = 0, WORK(1) returns the optimal LWORK. 
//*> \endverbatim 
//*> 
//*> \param[in] LWORK 
//*> \verbatim 
//*>          LWORK is INTEGER 
//*>          The length of the array WORK.  LWORK >= max(1,2*N). 
//*>          For optimal efficiency, LWORK >= (NB+1)*N, 
//*>          where NB is the max of the blocksize for ZHETRD and for 
//*>          ZUNMTR as returned by ILAENV. 
//*> 
//*>          If LWORK = -1, then a workspace query is assumed; the routine 
//*>          only calculates the optimal sizes of the WORK, RWORK and 
//*>          IWORK arrays, returns these values as the first entries of 
//*>          the WORK, RWORK and IWORK arrays, and no error message 
//*>          related to LWORK or LRWORK or LIWORK is issued by XERBLA. 
//*> \endverbatim 
//*> 
//*> \param[out] RWORK 
//*> \verbatim 
//*>          RWORK is DOUBLE PRECISION array, dimension (MAX(1,LRWORK)) 
//*>          On exit, if INFO = 0, RWORK(1) returns the optimal 
//*>          (and minimal) LRWORK. 
//*> \endverbatim 
//*> 
//*> \param[in] LRWORK 
//*> \verbatim 
//*>          LRWORK is INTEGER 
//*>          The length of the array RWORK.  LRWORK >= max(1,24*N). 
//*> 
//*>          If LRWORK = -1, then a workspace query is assumed; the 
//*>          routine only calculates the optimal sizes of the WORK, RWORK 
//*>          and IWORK arrays, returns these values as the first entries 
//*>          of the WORK, RWORK and IWORK arrays, and no error message 
//*>          related to LWORK or LRWORK or LIWORK is issued by XERBLA. 
//*> \endverbatim 
//*> 
//*> \param[out] IWORK 
//*> \verbatim 
//*>          IWORK is INTEGER array, dimension (MAX(1,LIWORK)) 
//*>          On exit, if INFO = 0, IWORK(1) returns the optimal 
//*>          (and minimal) LIWORK. 
//*> \endverbatim 
//*> 
//*> \param[in] LIWORK 
//*> \verbatim 
//*>          LIWORK is INTEGER 
//*>          The dimension of the array IWORK.  LIWORK >= max(1,10*N). 
//*> 
//*>          If LIWORK = -1, then a workspace query is assumed; the 
//*>          routine only calculates the optimal sizes of the WORK, RWORK 
//*>          and IWORK arrays, returns these values as the first entries 
//*>          of the WORK, RWORK and IWORK arrays, and no error message 
//*>          related to LWORK or LRWORK or LIWORK is issued by XERBLA. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value 
//*>          > 0:  Internal error 
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
//*> \ingroup complex16HEeigen 
//* 
//*> \par Contributors: 
//*  ================== 
//*> 
//*>     Inderjit Dhillon, IBM Almaden, USA \n 
//*>     Osni Marques, LBNL/NERSC, USA \n 
//*>     Ken Stanley, Computer Science Division, University of 
//*>       California at Berkeley, USA \n 
//*>     Jason Riedy, Computer Science Division, University of 
//*>       California at Berkeley, USA \n 
//*> 
//*  ===================================================================== 

	 
	public static void _qdy0ve9d(FString _w6igfk2h, FString _wrqmi80z, FString _9wyre9zc, ref Int32 _dxpq0xkr, complex* _vxfgpup9, ref Int32 _ocv8fk5c, ref Double _ppzorcqs, ref Double _qqhwr930, ref Int32 _ic6kua09, ref Int32 _j4l29b9c, ref Double _rltspcxj, ref Int32 _ev4xhht5, Double* _z1ioc3c8, complex* _7e60fcso, ref Int32 _5l1tna8s, Int32* _nr4g8ae2, complex* _apig8meb, ref Int32 _6fnxzlyp, Double* _dqanbbw3, ref Int32 _1jkrnd6f, Int32* _4b6rt45i, ref Int32 _29mhiasb, ref Int32 _gro5yvfo)
	{
#region variable declarations
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Double _5m0mjfxm =  2d;
Boolean _w72qtubt =  default;
Boolean _neo0j9hw =  default;
Boolean _58l4so5d =  default;
Boolean _lhlgm7z5 =  default;
Boolean _lso0qkri =  default;
Boolean _hessdr7t =  default;
Boolean _189gzykk =  default;
Boolean _4h81eu1p =  default;
FString _loathnh7 =  new FString(1);
Int32 _b5p6od9s =  default;
Int32 _jaeky6an =  default;
Int32 _itfnbz60 =  default;
Int32 _gmv1yrg4 =  default;
Int32 _hvr7v77z =  default;
Int32 _z5t705l8 =  default;
Int32 _vk70rfov =  default;
Int32 _1e67nv2n =  default;
Int32 _z6n1f9c0 =  default;
Int32 _uzww6etc =  default;
Int32 _bq23b0ra =  default;
Int32 _vkry6jqy =  default;
Int32 _2ed3vbgv =  default;
Int32 _pyea3qip =  default;
Int32 _azfddsec =  default;
Int32 _ijsczdpc =  default;
Int32 _g5graale =  default;
Int32 _qq2166xp =  default;
Int32 _znpjgsef =  default;
Int32 _j7zu8nju =  default;
Int32 _dgsb0lfe =  default;
Int32 _cr029lri =  default;
Int32 _r9aaxbw7 =  default;
Int32 _a4yddzxl =  default;
Int32 _e7ps44f9 =  default;
Int32 _e4ueamrn =  default;
Int32 _jc37k0o9 =  default;
Int32 _f7059815 =  default;
Int32 _naa7acm7 =  default;
Double _enahnxc5 =  default;
Double _j6vjow1g =  default;
Double _av7j8yda =  default;
Double _p1iqarg6 =  default;
Double _o8rgmibn =  default;
Double _sg2xsi4l =  default;
Double _h75qnr7l =  default;
Double _91a1vq5f =  default;
Double _bogm0gwy =  default;
Double _c0o9kuh7 =  default;
Double _s19zzp7v =  default;
Double _a8smjixf =  default;
string fLanavab = default;
#endregion  variable declarations
_w6igfk2h = _w6igfk2h.Convert(1);
_wrqmi80z = _wrqmi80z.Convert(1);
_9wyre9zc = _9wyre9zc.Convert(1);

	{
		//* 
		//*  -- LAPACK driver routine (version 3.7.0) -- 
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
		//* 
		//*     Test the input parameters. 
		//* 
		
		_jaeky6an = _4mvd6e4d(ref Unsafe.AsRef((int)10) ,"ZHEEVR" ,"N" ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef((int)2) ,ref Unsafe.AsRef((int)3) ,ref Unsafe.AsRef((int)4) );//* 
		
		_58l4so5d = _w8y2rzgy(_9wyre9zc ,"L" );
		_189gzykk = _w8y2rzgy(_w6igfk2h ,"V" );
		_w72qtubt = _w8y2rzgy(_wrqmi80z ,"A" );
		_hessdr7t = _w8y2rzgy(_wrqmi80z ,"V" );
		_neo0j9hw = _w8y2rzgy(_wrqmi80z ,"I" );//* 
		
		_lhlgm7z5 = (((_6fnxzlyp == (int)-1) | (_1jkrnd6f == (int)-1)) | (_29mhiasb == (int)-1));//* 
		
		_e7ps44f9 = ILNumerics.F2NET.Intrinsics.MAX((int)1 ,(int)24 * _dxpq0xkr );
		_dgsb0lfe = ILNumerics.F2NET.Intrinsics.MAX((int)1 ,(int)10 * _dxpq0xkr );
		_jc37k0o9 = ILNumerics.F2NET.Intrinsics.MAX((int)1 ,(int)2 * _dxpq0xkr );//* 
		
		_gro5yvfo = (int)0;
		if (!((_189gzykk | _w8y2rzgy(_w6igfk2h ,"N" ))))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if (!(((_w72qtubt | _hessdr7t) | _neo0j9hw)))
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (!((_58l4so5d | _w8y2rzgy(_9wyre9zc ,"U" ))))
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)-6;
		}
		else
		{
			
			if (_hessdr7t)
			{
				
				if ((_dxpq0xkr > (int)0) & (_qqhwr930 <= _ppzorcqs))
				_gro5yvfo = (int)-8;
			}
			else
			if (_neo0j9hw)
			{
				
				if ((_ic6kua09 < (int)1) | (_ic6kua09 > ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr )))
				{
					
					_gro5yvfo = (int)-9;
				}
				else
				if ((_j4l29b9c < ILNumerics.F2NET.Intrinsics.MIN(_dxpq0xkr ,_ic6kua09 )) | (_j4l29b9c > _dxpq0xkr))
				{
					
					_gro5yvfo = (int)-10;
				}
				
			}
			
		}
		
		if (_gro5yvfo == (int)0)
		{
			
			if ((_5l1tna8s < (int)1) | (_189gzykk & (_5l1tna8s < _dxpq0xkr)))
			{
				
				_gro5yvfo = (int)-15;
			}
			
		}
		//* 
		
		if (_gro5yvfo == (int)0)
		{
			
			_f7059815 = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZHETRD" ,_9wyre9zc ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) );
			_f7059815 = ILNumerics.F2NET.Intrinsics.MAX(_f7059815 ,_4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZUNMTR" ,_9wyre9zc ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) ) );
			_e4ueamrn = ILNumerics.F2NET.Intrinsics.MAX((_f7059815 + (int)1) * _dxpq0xkr ,_jc37k0o9 );
			*(_apig8meb+((int)1 - 1)) = DCMPLX(_e4ueamrn);
			*(_dqanbbw3+((int)1 - 1)) = DBLE(_e7ps44f9);
			*(_4b6rt45i+((int)1 - 1)) = _dgsb0lfe;//* 
			
			if ((_6fnxzlyp < _jc37k0o9) & (!(_lhlgm7z5)))
			{
				
				_gro5yvfo = (int)-18;
			}
			else
			if ((_1jkrnd6f < _e7ps44f9) & (!(_lhlgm7z5)))
			{
				
				_gro5yvfo = (int)-20;
			}
			else
			if ((_29mhiasb < _dgsb0lfe) & (!(_lhlgm7z5)))
			{
				
				_gro5yvfo = (int)-22;
			}
			
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("ZHEEVR" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		else
		if (_lhlgm7z5)
		{
			
			return;
		}
		//* 
		//*     Quick return if possible 
		//* 
		
		_ev4xhht5 = (int)0;
		if (_dxpq0xkr == (int)0)
		{
			
			*(_apig8meb+((int)1 - 1)) = DCMPLX((int)1);
			return;
		}
		//* 
		
		if (_dxpq0xkr == (int)1)
		{
			
			*(_apig8meb+((int)1 - 1)) = DCMPLX((int)2);
			if (_w72qtubt | _neo0j9hw)
			{
				
				_ev4xhht5 = (int)1;
				*(_z1ioc3c8+((int)1 - 1)) = ILNumerics.F2NET.Intrinsics.DBLE(*(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) );
			}
			else
			{
				
				if ((_ppzorcqs < ILNumerics.F2NET.Intrinsics.DBLE(*(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) )) & (_qqhwr930 >= ILNumerics.F2NET.Intrinsics.DBLE(*(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) )))
				{
					
					_ev4xhht5 = (int)1;
					*(_z1ioc3c8+((int)1 - 1)) = ILNumerics.F2NET.Intrinsics.DBLE(*(_vxfgpup9+((int)1 - 1) + ((int)1 - 1) * 1 * (_ocv8fk5c)) );
				}
				
			}
			
			if (_189gzykk)
			{
				
				*(_7e60fcso+((int)1 - 1) + ((int)1 - 1) * 1 * (_5l1tna8s)) = DCMPLX(_kxg5drh2);
				*(_nr4g8ae2+((int)1 - 1)) = (int)1;
				*(_nr4g8ae2+((int)2 - 1)) = (int)1;
			}
			
			return;
		}
		//* 
		//*     Get machine constants. 
		//* 
		
		_h75qnr7l = _f43eg0w0("Safe minimum" );
		_p1iqarg6 = _f43eg0w0("Precision" );
		_bogm0gwy = (_h75qnr7l / _p1iqarg6);
		_av7j8yda = (_kxg5drh2 / _bogm0gwy);
		_sg2xsi4l = ILNumerics.F2NET.Intrinsics.SQRT(_bogm0gwy );
		_o8rgmibn = ILNumerics.F2NET.Intrinsics.MIN(ILNumerics.F2NET.Intrinsics.SQRT(_av7j8yda ) ,_kxg5drh2 / ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.SQRT(_h75qnr7l ) ) );//* 
		//*     Scale matrix to allowable range, if necessary. 
		//* 
		
		_g5graale = (int)0;
		_enahnxc5 = _rltspcxj;
		if (_hessdr7t)
		{
			
			_s19zzp7v = _ppzorcqs;
			_a8smjixf = _qqhwr930;
		}
		
		_j6vjow1g = _bkdqwg1x("M" ,_9wyre9zc ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_dqanbbw3 );
		if ((_j6vjow1g > _d0547bi2) & (_j6vjow1g < _sg2xsi4l))
		{
			
			_g5graale = (int)1;
			_91a1vq5f = (_sg2xsi4l / _j6vjow1g);
		}
		else
		if (_j6vjow1g > _o8rgmibn)
		{
			
			_g5graale = (int)1;
			_91a1vq5f = (_o8rgmibn / _j6vjow1g);
		}
		
		if (_g5graale == (int)1)
		{
			
			if (_58l4so5d)
			{
				
				{
					System.Int32 __81fgg2dlsvn3604 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3604 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3604;
					for (__81fgg2count3604 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3604 + __81fgg2step3604) / __81fgg2step3604)), _znpjgsef = __81fgg2dlsvn3604; __81fgg2count3604 != 0; __81fgg2count3604--, _znpjgsef += (__81fgg2step3604)) {

					{
						
						_z5tkm94d(ref Unsafe.AsRef((_dxpq0xkr - _znpjgsef) + (int)1) ,ref _91a1vq5f ,(_vxfgpup9+(_znpjgsef - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
Mark10:;
						// continue
					}
										}				}
			}
			else
			{
				
				{
					System.Int32 __81fgg2dlsvn3605 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step3605 = (System.Int32)((int)1);
					System.Int32 __81fgg2count3605;
					for (__81fgg2count3605 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn3605 + __81fgg2step3605) / __81fgg2step3605)), _znpjgsef = __81fgg2dlsvn3605; __81fgg2count3605 != 0; __81fgg2count3605--, _znpjgsef += (__81fgg2step3605)) {

					{
						
						_z5tkm94d(ref _znpjgsef ,ref _91a1vq5f ,(_vxfgpup9+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_ocv8fk5c)),ref Unsafe.AsRef((int)1) );
Mark20:;
						// continue
					}
										}				}
			}
			
			if (_rltspcxj > (int)0)
			_enahnxc5 = (_rltspcxj * _91a1vq5f);
			if (_hessdr7t)
			{
				
				_s19zzp7v = (_ppzorcqs * _91a1vq5f);
				_a8smjixf = (_qqhwr930 * _91a1vq5f);
			}
			
		}
		// 
		//*     Initialize indices into workspaces.  Note: The IWORK indices are 
		//*     used only if DSTERF or ZSTEMR fail. 
		// 
		//*     WORK(INDTAU:INDTAU+N-1) stores the complex scalar factors of the 
		//*     elementary reflectors used in ZHETRD. 
		
		_pyea3qip = (int)1;//*     INDWK is the starting offset of the remaining complex workspace, 
		//*     and LLWORK is the remaining complex workspace size. 
		
		_azfddsec = (_pyea3qip + _dxpq0xkr);
		_cr029lri = ((_6fnxzlyp - _azfddsec) + (int)1);// 
		//*     RWORK(INDRD:INDRD+N-1) stores the real tridiagonal's diagonal 
		//*     entries. 
		
		_z6n1f9c0 = (int)1;//*     RWORK(INDRE:INDRE+N-1) stores the off-diagonal entries of the 
		//*     tridiagonal matrix from ZHETRD. 
		
		_bq23b0ra = (_z6n1f9c0 + _dxpq0xkr);//*     RWORK(INDRDD:INDRDD+N-1) is a copy of the diagonal entries over 
		//*     -written by ZSTEMR (the DSTERF path copies the diagonal to W). 
		
		_uzww6etc = (_bq23b0ra + _dxpq0xkr);//*     RWORK(INDREE:INDREE+N-1) is a copy of the off-diagonal entries over 
		//*     -written while computing the eigenvalues in DSTERF and ZSTEMR. 
		
		_vkry6jqy = (_uzww6etc + _dxpq0xkr);//*     INDRWK is the starting offset of the left-over real workspace, and 
		//*     LLRWORK is the remaining workspace size. 
		
		_2ed3vbgv = (_vkry6jqy + _dxpq0xkr);
		_r9aaxbw7 = ((_1jkrnd6f - _2ed3vbgv) + (int)1);// 
		//*     IWORK(INDIBL:INDIBL+M-1) corresponds to IBLOCK in DSTEBZ and 
		//*     stores the block indices of each of the M<=N eigenvalues. 
		
		_hvr7v77z = (int)1;//*     IWORK(INDISP:INDISP+NSPLIT-1) corresponds to ISPLIT in DSTEBZ and 
		//*     stores the starting and finishing indices of each block. 
		
		_vk70rfov = (_hvr7v77z + _dxpq0xkr);//*     IWORK(INDIFL:INDIFL+N-1) stores the indices of eigenvectors 
		//*     that corresponding to eigenvectors that fail to converge in 
		//*     DSTEIN.  This information is discarded; if any fail, the driver 
		//*     returns INFO > 0. 
		
		_z5t705l8 = (_vk70rfov + _dxpq0xkr);//*     INDIWO is the offset of the remaining integer workspace. 
		
		_1e67nv2n = (_z5t705l8 + _dxpq0xkr);// 
		//* 
		//*     Call ZHETRD to reduce Hermitian matrix to tridiagonal form. 
		//* 
		
		_i7e52u8d(_9wyre9zc ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,(_dqanbbw3+(_z6n1f9c0 - 1)),(_dqanbbw3+(_bq23b0ra - 1)),(_apig8meb+(_pyea3qip - 1)),(_apig8meb+(_azfddsec - 1)),ref _cr029lri ,ref _itfnbz60 );//* 
		//*     If all eigenvalues are desired 
		//*     then call DSTERF or ZSTEMR and ZUNMTR. 
		//* 
		
		_lso0qkri = false;
		if (_neo0j9hw)
		{
			
			if ((_ic6kua09 == (int)1) & (_j4l29b9c == _dxpq0xkr))
			{
				
				_lso0qkri = true;
			}
			
		}
		
		if ((_w72qtubt | _lso0qkri) & (_jaeky6an == (int)1))
		{
			
			if (!(_189gzykk))
			{
				
				_gvjhlct0(ref _dxpq0xkr ,(_dqanbbw3+(_z6n1f9c0 - 1)),ref Unsafe.AsRef((int)1) ,_z1ioc3c8 ,ref Unsafe.AsRef((int)1) );
				_gvjhlct0(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,(_dqanbbw3+(_bq23b0ra - 1)),ref Unsafe.AsRef((int)1) ,(_dqanbbw3+(_vkry6jqy - 1)),ref Unsafe.AsRef((int)1) );
				_0tyujlyc(ref _dxpq0xkr ,_z1ioc3c8 ,(_dqanbbw3+(_vkry6jqy - 1)),ref _gro5yvfo );
			}
			else
			{
				
				_gvjhlct0(ref Unsafe.AsRef(_dxpq0xkr - (int)1) ,(_dqanbbw3+(_bq23b0ra - 1)),ref Unsafe.AsRef((int)1) ,(_dqanbbw3+(_vkry6jqy - 1)),ref Unsafe.AsRef((int)1) );
				_gvjhlct0(ref _dxpq0xkr ,(_dqanbbw3+(_z6n1f9c0 - 1)),ref Unsafe.AsRef((int)1) ,(_dqanbbw3+(_uzww6etc - 1)),ref Unsafe.AsRef((int)1) );//* 
				
				if (_rltspcxj <= ((_5m0mjfxm * _dxpq0xkr) * _p1iqarg6))
				{
					
					_4h81eu1p = true;
				}
				else
				{
					
					_4h81eu1p = false;
				}
				
				_322c5jks(_w6igfk2h ,"A" ,ref _dxpq0xkr ,(_dqanbbw3+(_uzww6etc - 1)),(_dqanbbw3+(_vkry6jqy - 1)),ref _ppzorcqs ,ref _qqhwr930 ,ref _ic6kua09 ,ref _j4l29b9c ,ref _ev4xhht5 ,_z1ioc3c8 ,_7e60fcso ,ref _5l1tna8s ,ref _dxpq0xkr ,_nr4g8ae2 ,ref _4h81eu1p ,(_dqanbbw3+(_2ed3vbgv - 1)),ref _r9aaxbw7 ,_4b6rt45i ,ref _29mhiasb ,ref _gro5yvfo );//* 
				//*           Apply unitary matrix used in reduction to tridiagonal 
				//*           form to eigenvectors returned by ZSTEMR. 
				//* 
				
				if (_189gzykk & (_gro5yvfo == (int)0))
				{
					
					_ijsczdpc = _azfddsec;
					_a4yddzxl = ((_6fnxzlyp - _ijsczdpc) + (int)1);
					_qwjrm8nd("L" ,_9wyre9zc ,"N" ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_pyea3qip - 1)),_7e60fcso ,ref _5l1tna8s ,(_apig8meb+(_ijsczdpc - 1)),ref _a4yddzxl ,ref _itfnbz60 );
				}
				
			}
			//* 
			//* 
			
			if (_gro5yvfo == (int)0)
			{
				
				_ev4xhht5 = _dxpq0xkr;goto Mark30;
			}
			
			_gro5yvfo = (int)0;
		}
		//* 
		//*     Otherwise, call DSTEBZ and, if eigenvectors are desired, ZSTEIN. 
		//*     Also call DSTEBZ and ZSTEIN if ZSTEMR fails. 
		//* 
		
		if (_189gzykk)
		{
			
			
			_loathnh7 = "B";
		}
		else
		{
			
			
			_loathnh7 = "E";
		}
		// 
		
		_mdgalkm7(_wrqmi80z ,_loathnh7 ,ref _dxpq0xkr ,ref _s19zzp7v ,ref _a8smjixf ,ref _ic6kua09 ,ref _j4l29b9c ,ref _enahnxc5 ,(_dqanbbw3+(_z6n1f9c0 - 1)),(_dqanbbw3+(_bq23b0ra - 1)),ref _ev4xhht5 ,ref _naa7acm7 ,_z1ioc3c8 ,(_4b6rt45i+(_hvr7v77z - 1)),(_4b6rt45i+(_vk70rfov - 1)),(_dqanbbw3+(_2ed3vbgv - 1)),(_4b6rt45i+(_1e67nv2n - 1)),ref _gro5yvfo );//* 
		
		if (_189gzykk)
		{
			
			_pxxbagjl(ref _dxpq0xkr ,(_dqanbbw3+(_z6n1f9c0 - 1)),(_dqanbbw3+(_bq23b0ra - 1)),ref _ev4xhht5 ,_z1ioc3c8 ,(_4b6rt45i+(_hvr7v77z - 1)),(_4b6rt45i+(_vk70rfov - 1)),_7e60fcso ,ref _5l1tna8s ,(_dqanbbw3+(_2ed3vbgv - 1)),(_4b6rt45i+(_1e67nv2n - 1)),(_4b6rt45i+(_z5t705l8 - 1)),ref _gro5yvfo );//* 
			//*        Apply unitary matrix used in reduction to tridiagonal 
			//*        form to eigenvectors returned by ZSTEIN. 
			//* 
			
			_ijsczdpc = _azfddsec;
			_a4yddzxl = ((_6fnxzlyp - _ijsczdpc) + (int)1);
			_qwjrm8nd("L" ,_9wyre9zc ,"N" ,ref _dxpq0xkr ,ref _ev4xhht5 ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_pyea3qip - 1)),_7e60fcso ,ref _5l1tna8s ,(_apig8meb+(_ijsczdpc - 1)),ref _a4yddzxl ,ref _itfnbz60 );
		}
		//* 
		//*     If matrix was scaled, then rescale eigenvalues appropriately. 
		//* 
		
Mark30:;
		// continue
		if (_g5graale == (int)1)
		{
			
			if (_gro5yvfo == (int)0)
			{
				
				_gmv1yrg4 = _ev4xhht5;
			}
			else
			{
				
				_gmv1yrg4 = (_gro5yvfo - (int)1);
			}
			
			_f6jqcjk1(ref _gmv1yrg4 ,ref Unsafe.AsRef(_kxg5drh2 / _91a1vq5f) ,_z1ioc3c8 ,ref Unsafe.AsRef((int)1) );
		}
		//* 
		//*     If eigenvalues are not in order, then sort them, along with 
		//*     eigenvectors. 
		//* 
		
		if (_189gzykk)
		{
			
			{
				System.Int32 __81fgg2dlsvn3606 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step3606 = (System.Int32)((int)1);
				System.Int32 __81fgg2count3606;
				for (__81fgg2count3606 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5 - (int)1) - __81fgg2dlsvn3606 + __81fgg2step3606) / __81fgg2step3606)), _znpjgsef = __81fgg2dlsvn3606; __81fgg2count3606 != 0; __81fgg2count3606--, _znpjgsef += (__81fgg2step3606)) {

				{
					
					_b5p6od9s = (int)0;
					_c0o9kuh7 = *(_z1ioc3c8+(_znpjgsef - 1));
					{
						System.Int32 __81fgg2dlsvn3607 = (System.Int32)((_znpjgsef + (int)1));
						const System.Int32 __81fgg2step3607 = (System.Int32)((int)1);
						System.Int32 __81fgg2count3607;
						for (__81fgg2count3607 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ev4xhht5) - __81fgg2dlsvn3607 + __81fgg2step3607) / __81fgg2step3607)), _j7zu8nju = __81fgg2dlsvn3607; __81fgg2count3607 != 0; __81fgg2count3607--, _j7zu8nju += (__81fgg2step3607)) {

						{
							
							if (*(_z1ioc3c8+(_j7zu8nju - 1)) < _c0o9kuh7)
							{
								
								_b5p6od9s = _j7zu8nju;
								_c0o9kuh7 = *(_z1ioc3c8+(_j7zu8nju - 1));
							}
							
Mark40:;
							// continue
						}
												}					}//* 
					
					if (_b5p6od9s != (int)0)
					{
						
						_qq2166xp = *(_4b6rt45i+((_hvr7v77z + _b5p6od9s) - (int)1 - 1));
						*(_z1ioc3c8+(_b5p6od9s - 1)) = *(_z1ioc3c8+(_znpjgsef - 1));
						*(_4b6rt45i+((_hvr7v77z + _b5p6od9s) - (int)1 - 1)) = *(_4b6rt45i+((_hvr7v77z + _znpjgsef) - (int)1 - 1));
						*(_z1ioc3c8+(_znpjgsef - 1)) = _c0o9kuh7;
						*(_4b6rt45i+((_hvr7v77z + _znpjgsef) - (int)1 - 1)) = _qq2166xp;
						_027ahmgj(ref _dxpq0xkr ,(_7e60fcso+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_5l1tna8s)),ref Unsafe.AsRef((int)1) ,(_7e60fcso+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_5l1tna8s)),ref Unsafe.AsRef((int)1) );
					}
					
Mark50:;
					// continue
				}
								}			}
		}
		//* 
		//*     Set WORK(1) to optimal workspace size. 
		//* 
		
		*(_apig8meb+((int)1 - 1)) = DCMPLX(_e4ueamrn);
		*(_dqanbbw3+((int)1 - 1)) = DBLE(_e7ps44f9);
		*(_4b6rt45i+((int)1 - 1)) = _dgsb0lfe;//* 
		
		return;//* 
		//*     End of ZHEEVR 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
