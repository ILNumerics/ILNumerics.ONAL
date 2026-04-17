
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
//*> \brief <b> ZGEEVX computes the eigenvalues and, optionally, the left and/or right eigenvectors for GE matrices</b> 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ZGEEVX + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/zgeevx.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/zgeevx.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/zgeevx.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE ZGEEVX( BALANC, JOBVL, JOBVR, SENSE, N, A, LDA, W, VL, 
//*                          LDVL, VR, LDVR, ILO, IHI, SCALE, ABNRM, RCONDE, 
//*                          RCONDV, WORK, LWORK, RWORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          BALANC, JOBVL, JOBVR, SENSE 
//*       INTEGER            IHI, ILO, INFO, LDA, LDVL, LDVR, LWORK, N 
//*       DOUBLE PRECISION   ABNRM 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   RCONDE( * ), RCONDV( * ), RWORK( * ), 
//*      $                   SCALE( * ) 
//*       COMPLEX*16         A( LDA, * ), VL( LDVL, * ), VR( LDVR, * ), 
//*      $                   W( * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ZGEEVX computes for an N-by-N complex nonsymmetric matrix A, the 
//*> eigenvalues and, optionally, the left and/or right eigenvectors. 
//*> 
//*> Optionally also, it computes a balancing transformation to improve 
//*> the conditioning of the eigenvalues and eigenvectors (ILO, IHI, 
//*> SCALE, and ABNRM), reciprocal condition numbers for the eigenvalues 
//*> (RCONDE), and reciprocal condition numbers for the right 
//*> eigenvectors (RCONDV). 
//*> 
//*> The right eigenvector v(j) of A satisfies 
//*>                  A * v(j) = lambda(j) * v(j) 
//*> where lambda(j) is its eigenvalue. 
//*> The left eigenvector u(j) of A satisfies 
//*>               u(j)**H * A = lambda(j) * u(j)**H 
//*> where u(j)**H denotes the conjugate transpose of u(j). 
//*> 
//*> The computed eigenvectors are normalized to have Euclidean norm 
//*> equal to 1 and largest component real. 
//*> 
//*> Balancing a matrix means permuting the rows and columns to make it 
//*> more nearly upper triangular, and applying a diagonal similarity 
//*> transformation D * A * D**(-1), where D is a diagonal matrix, to 
//*> make its rows and columns closer in norm and the condition numbers 
//*> of its eigenvalues and eigenvectors smaller.  The computed 
//*> reciprocal condition numbers correspond to the balanced matrix. 
//*> Permuting rows and columns will not change the condition numbers 
//*> (in exact arithmetic) but diagonal scaling will.  For further 
//*> explanation of balancing, see section 4.10.2 of the LAPACK 
//*> Users' Guide. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] BALANC 
//*> \verbatim 
//*>          BALANC is CHARACTER*1 
//*>          Indicates how the input matrix should be diagonally scaled 
//*>          and/or permuted to improve the conditioning of its 
//*>          eigenvalues. 
//*>          = 'N': Do not diagonally scale or permute; 
//*>          = 'P': Perform permutations to make the matrix more nearly 
//*>                 upper triangular. Do not diagonally scale; 
//*>          = 'S': Diagonally scale the matrix, ie. replace A by 
//*>                 D*A*D**(-1), where D is a diagonal matrix chosen 
//*>                 to make the rows and columns of A more equal in 
//*>                 norm. Do not permute; 
//*>          = 'B': Both diagonally scale and permute A. 
//*> 
//*>          Computed reciprocal condition numbers will be for the matrix 
//*>          after balancing and/or permuting. Permuting does not change 
//*>          condition numbers (in exact arithmetic), but balancing does. 
//*> \endverbatim 
//*> 
//*> \param[in] JOBVL 
//*> \verbatim 
//*>          JOBVL is CHARACTER*1 
//*>          = 'N': left eigenvectors of A are not computed; 
//*>          = 'V': left eigenvectors of A are computed. 
//*>          If SENSE = 'E' or 'B', JOBVL must = 'V'. 
//*> \endverbatim 
//*> 
//*> \param[in] JOBVR 
//*> \verbatim 
//*>          JOBVR is CHARACTER*1 
//*>          = 'N': right eigenvectors of A are not computed; 
//*>          = 'V': right eigenvectors of A are computed. 
//*>          If SENSE = 'E' or 'B', JOBVR must = 'V'. 
//*> \endverbatim 
//*> 
//*> \param[in] SENSE 
//*> \verbatim 
//*>          SENSE is CHARACTER*1 
//*>          Determines which reciprocal condition numbers are computed. 
//*>          = 'N': None are computed; 
//*>          = 'E': Computed for eigenvalues only; 
//*>          = 'V': Computed for right eigenvectors only; 
//*>          = 'B': Computed for eigenvalues and right eigenvectors. 
//*> 
//*>          If SENSE = 'E' or 'B', both left and right eigenvectors 
//*>          must also be computed (JOBVL = 'V' and JOBVR = 'V'). 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          The order of the matrix A. N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is COMPLEX*16 array, dimension (LDA,N) 
//*>          On entry, the N-by-N matrix A. 
//*>          On exit, A has been overwritten.  If JOBVL = 'V' or 
//*>          JOBVR = 'V', A contains the Schur form of the balanced 
//*>          version of the matrix A. 
//*> \endverbatim 
//*> 
//*> \param[in] LDA 
//*> \verbatim 
//*>          LDA is INTEGER 
//*>          The leading dimension of the array A.  LDA >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[out] W 
//*> \verbatim 
//*>          W is COMPLEX*16 array, dimension (N) 
//*>          W contains the computed eigenvalues. 
//*> \endverbatim 
//*> 
//*> \param[out] VL 
//*> \verbatim 
//*>          VL is COMPLEX*16 array, dimension (LDVL,N) 
//*>          If JOBVL = 'V', the left eigenvectors u(j) are stored one 
//*>          after another in the columns of VL, in the same order 
//*>          as their eigenvalues. 
//*>          If JOBVL = 'N', VL is not referenced. 
//*>          u(j) = VL(:,j), the j-th column of VL. 
//*> \endverbatim 
//*> 
//*> \param[in] LDVL 
//*> \verbatim 
//*>          LDVL is INTEGER 
//*>          The leading dimension of the array VL.  LDVL >= 1; if 
//*>          JOBVL = 'V', LDVL >= N. 
//*> \endverbatim 
//*> 
//*> \param[out] VR 
//*> \verbatim 
//*>          VR is COMPLEX*16 array, dimension (LDVR,N) 
//*>          If JOBVR = 'V', the right eigenvectors v(j) are stored one 
//*>          after another in the columns of VR, in the same order 
//*>          as their eigenvalues. 
//*>          If JOBVR = 'N', VR is not referenced. 
//*>          v(j) = VR(:,j), the j-th column of VR. 
//*> \endverbatim 
//*> 
//*> \param[in] LDVR 
//*> \verbatim 
//*>          LDVR is INTEGER 
//*>          The leading dimension of the array VR.  LDVR >= 1; if 
//*>          JOBVR = 'V', LDVR >= N. 
//*> \endverbatim 
//*> 
//*> \param[out] ILO 
//*> \verbatim 
//*>          ILO is INTEGER 
//*> \endverbatim 
//*> 
//*> \param[out] IHI 
//*> \verbatim 
//*>          IHI is INTEGER 
//*>          ILO and IHI are integer values determined when A was 
//*>          balanced.  The balanced A(i,j) = 0 if I > J and 
//*>          J = 1,...,ILO-1 or I = IHI+1,...,N. 
//*> \endverbatim 
//*> 
//*> \param[out] SCALE 
//*> \verbatim 
//*>          SCALE is DOUBLE PRECISION array, dimension (N) 
//*>          Details of the permutations and scaling factors applied 
//*>          when balancing A.  If P(j) is the index of the row and column 
//*>          interchanged with row and column j, and D(j) is the scaling 
//*>          factor applied to row and column j, then 
//*>          SCALE(J) = P(J),    for J = 1,...,ILO-1 
//*>                   = D(J),    for J = ILO,...,IHI 
//*>                   = P(J)     for J = IHI+1,...,N. 
//*>          The order in which the interchanges are made is N to IHI+1, 
//*>          then 1 to ILO-1. 
//*> \endverbatim 
//*> 
//*> \param[out] ABNRM 
//*> \verbatim 
//*>          ABNRM is DOUBLE PRECISION 
//*>          The one-norm of the balanced matrix (the maximum 
//*>          of the sum of absolute values of elements of any column). 
//*> \endverbatim 
//*> 
//*> \param[out] RCONDE 
//*> \verbatim 
//*>          RCONDE is DOUBLE PRECISION array, dimension (N) 
//*>          RCONDE(j) is the reciprocal condition number of the j-th 
//*>          eigenvalue. 
//*> \endverbatim 
//*> 
//*> \param[out] RCONDV 
//*> \verbatim 
//*>          RCONDV is DOUBLE PRECISION array, dimension (N) 
//*>          RCONDV(j) is the reciprocal condition number of the j-th 
//*>          right eigenvector. 
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
//*>          The dimension of the array WORK.  If SENSE = 'N' or 'E', 
//*>          LWORK >= max(1,2*N), and if SENSE = 'V' or 'B', 
//*>          LWORK >= N*N+2*N. 
//*>          For good performance, LWORK must generally be larger. 
//*> 
//*>          If LWORK = -1, then a workspace query is assumed; the routine 
//*>          only calculates the optimal size of the WORK array, returns 
//*>          this value as the first entry of the WORK array, and no error 
//*>          message related to LWORK is issued by XERBLA. 
//*> \endverbatim 
//*> 
//*> \param[out] RWORK 
//*> \verbatim 
//*>          RWORK is DOUBLE PRECISION array, dimension (2*N) 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          = 0:  successful exit 
//*>          < 0:  if INFO = -i, the i-th argument had an illegal value. 
//*>          > 0:  if INFO = i, the QR algorithm failed to compute all the 
//*>                eigenvalues, and no eigenvectors or condition numbers 
//*>                have been computed; elements 1:ILO-1 and i+1:N of W 
//*>                contain eigenvalues which have converged. 
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
//*  @precisions fortran z -> c 
//* 
//*> \ingroup complex16GEeigen 
//* 
//*  ===================================================================== 

	 
	public static void _odkzrkpt(FString _0lhh48h1, FString _47oqk0vf, FString _7uv4f9ji, FString _jh0wfot0, ref Int32 _dxpq0xkr, complex* _vxfgpup9, ref Int32 _ocv8fk5c, complex* _z1ioc3c8, complex* _ppzorcqs, ref Int32 _uq25zlw0, complex* _b88wiuwq, ref Int32 _oxoory3e, ref Int32 _pew3blan, ref Int32 _9c1csucx, Double* _1m44vtuk, ref Double _tyqdj33c, Double* _2mczwya0, Double* _is5n4oz3, complex* _apig8meb, ref Int32 _6fnxzlyp, Double* _dqanbbw3, ref Int32 _gro5yvfo)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)9 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Boolean _lhlgm7z5 =  default;
Boolean _pjb22a66 =  default;
Boolean _wylb4tri =  default;
Boolean _f545s740 =  default;
Boolean _xyg82qje =  default;
Boolean _9xy91aj1 =  default;
Boolean _qgc7m85z =  default;
Boolean _bxh9t3ks =  default;
FString _xcrv93xi =  new FString(1);
FString _m2cn2gjg =  new FString(1);
Int32 _plfe47df =  default;
Int32 _b5p6od9s =  default;
Int32 _ohcg4rre =  default;
Int32 _bhsiylw4 =  default;
Int32 _q1w15vsx =  default;
Int32 _wdpl46oy =  default;
Int32 _umlkckdg =  default;
Int32 _2r7fikug =  default;
Int32 _tafa1evd =  default;
Int32 _gghrqcr1 =  default;
Int32 _nfup5e6t =  default;
Double _j6vjow1g =  default;
Double _av7j8yda =  default;
Double _d6te6z4t =  default;
Double _p1iqarg6 =  default;
Double _ofbdxt08 =  default;
Double _bogm0gwy =  default;
complex _2qcyvkhx =  default;
Boolean* _2vi7x6ig =  (Boolean*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Boolean) * ((int)1);
Double* _g7qb61ha =  (Double*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Double) * ((int)1);
string fLanavab = default;
#endregion  variable declarations
_0lhh48h1 = _0lhh48h1.Convert(1);
_47oqk0vf = _47oqk0vf.Convert(1);
_7uv4f9ji = _7uv4f9ji.Convert(1);
_jh0wfot0 = _jh0wfot0.Convert(1);

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
		//*     .. Local Arrays .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input arguments 
		//* 
		
		_gro5yvfo = (int)0;
		_lhlgm7z5 = (_6fnxzlyp == (int)-1);
		_wylb4tri = _w8y2rzgy(_47oqk0vf ,"V" );
		_f545s740 = _w8y2rzgy(_7uv4f9ji ,"V" );
		_qgc7m85z = _w8y2rzgy(_jh0wfot0 ,"N" );
		_9xy91aj1 = _w8y2rzgy(_jh0wfot0 ,"E" );
		_bxh9t3ks = _w8y2rzgy(_jh0wfot0 ,"V" );
		_xyg82qje = _w8y2rzgy(_jh0wfot0 ,"B" );
		if (!((((_w8y2rzgy(_0lhh48h1 ,"N" ) | _w8y2rzgy(_0lhh48h1 ,"S" )) | _w8y2rzgy(_0lhh48h1 ,"P" )) | _w8y2rzgy(_0lhh48h1 ,"B" ))))
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if ((!(_wylb4tri)) & (!(_w8y2rzgy(_47oqk0vf ,"N" ))))
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if ((!(_f545s740)) & (!(_w8y2rzgy(_7uv4f9ji ,"N" ))))
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if ((!((((_qgc7m85z | _9xy91aj1) | _xyg82qje) | _bxh9t3ks))) | ((_9xy91aj1 | _xyg82qje) & (!((_wylb4tri & _f545s740)))))
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-5;
		}
		else
		if (_ocv8fk5c < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))
		{
			
			_gro5yvfo = (int)-7;
		}
		else
		if ((_uq25zlw0 < (int)1) | (_wylb4tri & (_uq25zlw0 < _dxpq0xkr)))
		{
			
			_gro5yvfo = (int)-10;
		}
		else
		if ((_oxoory3e < (int)1) | (_f545s740 & (_oxoory3e < _dxpq0xkr)))
		{
			
			_gro5yvfo = (int)-12;
		}
		//* 
		//*     Compute workspace 
		//*      (Note: Comments in the code beginning "Workspace:" describe the 
		//*       minimal amount of workspace needed at that point in the code, 
		//*       as well as the preferred amount for good performance. 
		//*       CWorkspace refers to complex workspace, and RWorkspace to real 
		//*       workspace. NB refers to the optimal block size for the 
		//*       immediately following subroutine, as returned by ILAENV. 
		//*       HSWORK refers to the workspace preferred by ZHSEQR, as 
		//*       calculated below. HSWORK is computed assuming ILO=1 and IHI=N, 
		//*       the worst case.) 
		//* 
		
		if (_gro5yvfo == (int)0)
		{
			
			if (_dxpq0xkr == (int)0)
			{
				
				_gghrqcr1 = (int)1;
				_tafa1evd = (int)1;
			}
			else
			{
				
				_tafa1evd = (_dxpq0xkr + (_dxpq0xkr * _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZGEHRD" ," " ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1) ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)0) )));//* 
				
				if (_wylb4tri)
				{
					
					_pceudq90("L" ,"B" ,_2vi7x6ig ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_ppzorcqs ,ref _uq25zlw0 ,_b88wiuwq ,ref _oxoory3e ,ref _dxpq0xkr ,ref _nfup5e6t ,_apig8meb ,ref Unsafe.AsRef((int)-1) ,_dqanbbw3 ,ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
					_2r7fikug = ILNumerics.F2NET.Intrinsics.INT(*(_apig8meb+((int)1 - 1)) );
					_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,_2r7fikug );
					_hp212wyz("S" ,"V" ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1) ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_z1ioc3c8 ,_ppzorcqs ,ref _uq25zlw0 ,_apig8meb ,ref Unsafe.AsRef((int)-1) ,ref _gro5yvfo );
				}
				else
				if (_f545s740)
				{
					
					_pceudq90("R" ,"B" ,_2vi7x6ig ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_ppzorcqs ,ref _uq25zlw0 ,_b88wiuwq ,ref _oxoory3e ,ref _dxpq0xkr ,ref _nfup5e6t ,_apig8meb ,ref Unsafe.AsRef((int)-1) ,_dqanbbw3 ,ref Unsafe.AsRef((int)-1) ,ref _bhsiylw4 );
					_2r7fikug = ILNumerics.F2NET.Intrinsics.INT(*(_apig8meb+((int)1 - 1)) );
					_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,_2r7fikug );
					_hp212wyz("S" ,"V" ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1) ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_z1ioc3c8 ,_b88wiuwq ,ref _oxoory3e ,_apig8meb ,ref Unsafe.AsRef((int)-1) ,ref _gro5yvfo );
				}
				else
				{
					
					if (_qgc7m85z)
					{
						
						_hp212wyz("E" ,"N" ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1) ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_z1ioc3c8 ,_b88wiuwq ,ref _oxoory3e ,_apig8meb ,ref Unsafe.AsRef((int)-1) ,ref _gro5yvfo );
					}
					else
					{
						
						_hp212wyz("S" ,"N" ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1) ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_z1ioc3c8 ,_b88wiuwq ,ref _oxoory3e ,_apig8meb ,ref Unsafe.AsRef((int)-1) ,ref _gro5yvfo );
					}
					
				}
				
				_plfe47df = ILNumerics.F2NET.Intrinsics.INT(*(_apig8meb+((int)1 - 1)) );//* 
				
				if ((!(_wylb4tri)) & (!(_f545s740)))
				{
					
					_gghrqcr1 = ((int)2 * _dxpq0xkr);
					if (!((_qgc7m85z | _9xy91aj1)))
					_gghrqcr1 = ILNumerics.F2NET.Intrinsics.MAX(_gghrqcr1 ,(_dxpq0xkr * _dxpq0xkr) + ((int)2 * _dxpq0xkr) );
					_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,_plfe47df );
					if (!((_qgc7m85z | _9xy91aj1)))
					_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,(_dxpq0xkr * _dxpq0xkr) + ((int)2 * _dxpq0xkr) );
				}
				else
				{
					
					_gghrqcr1 = ((int)2 * _dxpq0xkr);
					if (!((_qgc7m85z | _9xy91aj1)))
					_gghrqcr1 = ILNumerics.F2NET.Intrinsics.MAX(_gghrqcr1 ,(_dxpq0xkr * _dxpq0xkr) + ((int)2 * _dxpq0xkr) );
					_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,_plfe47df );
					_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,_dxpq0xkr + ((_dxpq0xkr - (int)1) * _4mvd6e4d(ref Unsafe.AsRef((int)1) ,"ZUNGHR" ," " ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1) ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)-1) )) );
					if (!((_qgc7m85z | _9xy91aj1)))
					_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,(_dxpq0xkr * _dxpq0xkr) + ((int)2 * _dxpq0xkr) );
					_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,(int)2 * _dxpq0xkr );
				}
				
				_tafa1evd = ILNumerics.F2NET.Intrinsics.MAX(_tafa1evd ,_gghrqcr1 );
			}
			
			*(_apig8meb+((int)1 - 1)) = DCMPLX(_tafa1evd);//* 
			
			if ((_6fnxzlyp < _gghrqcr1) & (!(_lhlgm7z5)))
			{
				
				_gro5yvfo = (int)-20;
			}
			
		}
		//* 
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("ZGEEVX" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
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
		
		if (_dxpq0xkr == (int)0)
		return;//* 
		//*     Get machine constants 
		//* 
		
		_p1iqarg6 = _f43eg0w0("P" );
		_bogm0gwy = _f43eg0w0("S" );
		_av7j8yda = (_kxg5drh2 / _bogm0gwy);
		_to4dtyqc(ref _bogm0gwy ,ref _av7j8yda );
		_bogm0gwy = (ILNumerics.F2NET.Intrinsics.SQRT(_bogm0gwy ) / _p1iqarg6);
		_av7j8yda = (_kxg5drh2 / _bogm0gwy);//* 
		//*     Scale A if max element outside range [SMLNUM,BIGNUM] 
		//* 
		
		_ohcg4rre = (int)0;
		_j6vjow1g = _o615qv2q("M" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_g7qb61ha );
		_pjb22a66 = false;
		if ((_j6vjow1g > _d0547bi2) & (_j6vjow1g < _bogm0gwy))
		{
			
			_pjb22a66 = true;
			_d6te6z4t = _bogm0gwy;
		}
		else
		if (_j6vjow1g > _av7j8yda)
		{
			
			_pjb22a66 = true;
			_d6te6z4t = _av7j8yda;
		}
		
		if (_pjb22a66)
		_j6h8q4u5("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _j6vjow1g ,ref _d6te6z4t ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,ref _bhsiylw4 );//* 
		//*     Balance the matrix and compute ABNRM 
		//* 
		
		_xcqo20w7(_0lhh48h1 ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,ref _pew3blan ,ref _9c1csucx ,_1m44vtuk ,ref _bhsiylw4 );
		_tyqdj33c = _o615qv2q("1" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_g7qb61ha );
		if (_pjb22a66)
		{
			
			*(_g7qb61ha+((int)1 - 1)) = _tyqdj33c;
			_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _d6te6z4t ,ref _j6vjow1g ,ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef((int)1) ,_g7qb61ha ,ref Unsafe.AsRef((int)1) ,ref _bhsiylw4 );
			_tyqdj33c = *(_g7qb61ha+((int)1 - 1));
		}
		//* 
		//*     Reduce to upper Hessenberg form 
		//*     (CWorkspace: need 2*N, prefer N+N*NB) 
		//*     (RWorkspace: none) 
		//* 
		
		_q1w15vsx = (int)1;
		_wdpl46oy = (_q1w15vsx + _dxpq0xkr);
		_j69fohs3(ref _dxpq0xkr ,ref _pew3blan ,ref _9c1csucx ,_vxfgpup9 ,ref _ocv8fk5c ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_wdpl46oy - 1)),ref Unsafe.AsRef((_6fnxzlyp - _wdpl46oy) + (int)1) ,ref _bhsiylw4 );//* 
		
		if (_wylb4tri)
		{
			//* 
			//*        Want left eigenvectors 
			//*        Copy Householder vectors to VL 
			//* 
			
			
			_m2cn2gjg = "L";
			_nihu9ses("L" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_ppzorcqs ,ref _uq25zlw0 );//* 
			//*        Generate unitary matrix in VL 
			//*        (CWorkspace: need 2*N-1, prefer N+(N-1)*NB) 
			//*        (RWorkspace: none) 
			//* 
			
			_3r1g0v7p(ref _dxpq0xkr ,ref _pew3blan ,ref _9c1csucx ,_ppzorcqs ,ref _uq25zlw0 ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_wdpl46oy - 1)),ref Unsafe.AsRef((_6fnxzlyp - _wdpl46oy) + (int)1) ,ref _bhsiylw4 );//* 
			//*        Perform QR iteration, accumulating Schur vectors in VL 
			//*        (CWorkspace: need 1, prefer HSWORK (see comments) ) 
			//*        (RWorkspace: none) 
			//* 
			
			_wdpl46oy = _q1w15vsx;
			_hp212wyz("S" ,"V" ,ref _dxpq0xkr ,ref _pew3blan ,ref _9c1csucx ,_vxfgpup9 ,ref _ocv8fk5c ,_z1ioc3c8 ,_ppzorcqs ,ref _uq25zlw0 ,(_apig8meb+(_wdpl46oy - 1)),ref Unsafe.AsRef((_6fnxzlyp - _wdpl46oy) + (int)1) ,ref _gro5yvfo );//* 
			
			if (_f545s740)
			{
				//* 
				//*           Want left and right eigenvectors 
				//*           Copy Schur vectors to VR 
				//* 
				
				
				_m2cn2gjg = "B";
				_nihu9ses("F" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_ppzorcqs ,ref _uq25zlw0 ,_b88wiuwq ,ref _oxoory3e );
			}
			//* 
			
		}
		else
		if (_f545s740)
		{
			//* 
			//*        Want right eigenvectors 
			//*        Copy Householder vectors to VR 
			//* 
			
			
			_m2cn2gjg = "R";
			_nihu9ses("L" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_b88wiuwq ,ref _oxoory3e );//* 
			//*        Generate unitary matrix in VR 
			//*        (CWorkspace: need 2*N-1, prefer N+(N-1)*NB) 
			//*        (RWorkspace: none) 
			//* 
			
			_3r1g0v7p(ref _dxpq0xkr ,ref _pew3blan ,ref _9c1csucx ,_b88wiuwq ,ref _oxoory3e ,(_apig8meb+(_q1w15vsx - 1)),(_apig8meb+(_wdpl46oy - 1)),ref Unsafe.AsRef((_6fnxzlyp - _wdpl46oy) + (int)1) ,ref _bhsiylw4 );//* 
			//*        Perform QR iteration, accumulating Schur vectors in VR 
			//*        (CWorkspace: need 1, prefer HSWORK (see comments) ) 
			//*        (RWorkspace: none) 
			//* 
			
			_wdpl46oy = _q1w15vsx;
			_hp212wyz("S" ,"V" ,ref _dxpq0xkr ,ref _pew3blan ,ref _9c1csucx ,_vxfgpup9 ,ref _ocv8fk5c ,_z1ioc3c8 ,_b88wiuwq ,ref _oxoory3e ,(_apig8meb+(_wdpl46oy - 1)),ref Unsafe.AsRef((_6fnxzlyp - _wdpl46oy) + (int)1) ,ref _gro5yvfo );//* 
			
		}
		else
		{
			//* 
			//*        Compute eigenvalues only 
			//*        If condition numbers desired, compute Schur form 
			//* 
			
			if (_qgc7m85z)
			{
				
				
				_xcrv93xi = "E";
			}
			else
			{
				
				
				_xcrv93xi = "S";
			}
			//* 
			//*        (CWorkspace: need 1, prefer HSWORK (see comments) ) 
			//*        (RWorkspace: none) 
			//* 
			
			_wdpl46oy = _q1w15vsx;
			_hp212wyz(_xcrv93xi ,"N" ,ref _dxpq0xkr ,ref _pew3blan ,ref _9c1csucx ,_vxfgpup9 ,ref _ocv8fk5c ,_z1ioc3c8 ,_b88wiuwq ,ref _oxoory3e ,(_apig8meb+(_wdpl46oy - 1)),ref Unsafe.AsRef((_6fnxzlyp - _wdpl46oy) + (int)1) ,ref _gro5yvfo );
		}
		//* 
		//*     If INFO .NE. 0 from ZHSEQR, then quit 
		//* 
		
		if (_gro5yvfo != (int)0)goto Mark50;//* 
		
		if (_wylb4tri | _f545s740)
		{
			//* 
			//*        Compute left and/or right eigenvectors 
			//*        (CWorkspace: need 2*N, prefer N + 2*N*NB) 
			//*        (RWorkspace: need N) 
			//* 
			
			_pceudq90(_m2cn2gjg ,"B" ,_2vi7x6ig ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_ppzorcqs ,ref _uq25zlw0 ,_b88wiuwq ,ref _oxoory3e ,ref _dxpq0xkr ,ref _nfup5e6t ,(_apig8meb+(_wdpl46oy - 1)),ref Unsafe.AsRef((_6fnxzlyp - _wdpl46oy) + (int)1) ,_dqanbbw3 ,ref _dxpq0xkr ,ref _bhsiylw4 );
		}
		//* 
		//*     Compute condition numbers if desired 
		//*     (CWorkspace: need N*N+2*N unless SENSE = 'E') 
		//*     (RWorkspace: need 2*N unless SENSE = 'E') 
		//* 
		
		if (!(_qgc7m85z))
		{
			
			_f0z2djqh(_jh0wfot0 ,"A" ,_2vi7x6ig ,ref _dxpq0xkr ,_vxfgpup9 ,ref _ocv8fk5c ,_ppzorcqs ,ref _uq25zlw0 ,_b88wiuwq ,ref _oxoory3e ,_2mczwya0 ,_is5n4oz3 ,ref _dxpq0xkr ,ref _nfup5e6t ,(_apig8meb+(_wdpl46oy - 1)),ref _dxpq0xkr ,_dqanbbw3 ,ref _ohcg4rre );
		}
		//* 
		
		if (_wylb4tri)
		{
			//* 
			//*        Undo balancing of left eigenvectors 
			//* 
			
			_ojkt64cy(_0lhh48h1 ,"L" ,ref _dxpq0xkr ,ref _pew3blan ,ref _9c1csucx ,_1m44vtuk ,ref _dxpq0xkr ,_ppzorcqs ,ref _uq25zlw0 ,ref _bhsiylw4 );//* 
			//*        Normalize left eigenvectors and make largest component real 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn2655 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2655 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2655;
				for (__81fgg2count2655 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2655 + __81fgg2step2655) / __81fgg2step2655)), _b5p6od9s = __81fgg2dlsvn2655; __81fgg2count2655 != 0; __81fgg2count2655--, _b5p6od9s += (__81fgg2step2655)) {

				{
					
					_ofbdxt08 = (_kxg5drh2 / _yzrhzz6l(ref _dxpq0xkr ,(_ppzorcqs+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) ));
					_z5tkm94d(ref _dxpq0xkr ,ref _ofbdxt08 ,(_ppzorcqs+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );
					{
						System.Int32 __81fgg2dlsvn2656 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2656 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2656;
						for (__81fgg2count2656 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2656 + __81fgg2step2656) / __81fgg2step2656)), _umlkckdg = __81fgg2dlsvn2656; __81fgg2count2656 != 0; __81fgg2count2656--, _umlkckdg += (__81fgg2step2656)) {

						{
							
							*(_dqanbbw3+(_umlkckdg - 1)) = (__POW2(ILNumerics.F2NET.Intrinsics.DBLE(*(_ppzorcqs+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_uq25zlw0)) )) + __POW2(ILNumerics.F2NET.Intrinsics.AIMAG(*(_ppzorcqs+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_uq25zlw0)) )));
Mark10:;
							// continue
						}
												}					}
					_umlkckdg = _ei7om7ok(ref _dxpq0xkr ,_dqanbbw3 ,ref Unsafe.AsRef((int)1) );
					_2qcyvkhx = (ILNumerics.F2NET.Intrinsics.CONJG(*(_ppzorcqs+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_uq25zlw0)) ) / ILNumerics.F2NET.Intrinsics.SQRT(*(_dqanbbw3+(_umlkckdg - 1)) ));
					_wv0on4xy(ref _dxpq0xkr ,ref _2qcyvkhx ,(_ppzorcqs+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_uq25zlw0)),ref Unsafe.AsRef((int)1) );
					*(_ppzorcqs+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_uq25zlw0)) = ILNumerics.F2NET.Intrinsics.DCMPLX(ILNumerics.F2NET.Intrinsics.DBLE(*(_ppzorcqs+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_uq25zlw0)) ) ,_d0547bi2 );
Mark20:;
					// continue
				}
								}			}
		}
		//* 
		
		if (_f545s740)
		{
			//* 
			//*        Undo balancing of right eigenvectors 
			//* 
			
			_ojkt64cy(_0lhh48h1 ,"R" ,ref _dxpq0xkr ,ref _pew3blan ,ref _9c1csucx ,_1m44vtuk ,ref _dxpq0xkr ,_b88wiuwq ,ref _oxoory3e ,ref _bhsiylw4 );//* 
			//*        Normalize right eigenvectors and make largest component real 
			//* 
			
			{
				System.Int32 __81fgg2dlsvn2657 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step2657 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2657;
				for (__81fgg2count2657 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2657 + __81fgg2step2657) / __81fgg2step2657)), _b5p6od9s = __81fgg2dlsvn2657; __81fgg2count2657 != 0; __81fgg2count2657--, _b5p6od9s += (__81fgg2step2657)) {

				{
					
					_ofbdxt08 = (_kxg5drh2 / _yzrhzz6l(ref _dxpq0xkr ,(_b88wiuwq+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) ));
					_z5tkm94d(ref _dxpq0xkr ,ref _ofbdxt08 ,(_b88wiuwq+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );
					{
						System.Int32 __81fgg2dlsvn2658 = (System.Int32)((int)1);
						const System.Int32 __81fgg2step2658 = (System.Int32)((int)1);
						System.Int32 __81fgg2count2658;
						for (__81fgg2count2658 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2658 + __81fgg2step2658) / __81fgg2step2658)), _umlkckdg = __81fgg2dlsvn2658; __81fgg2count2658 != 0; __81fgg2count2658--, _umlkckdg += (__81fgg2step2658)) {

						{
							
							*(_dqanbbw3+(_umlkckdg - 1)) = (__POW2(ILNumerics.F2NET.Intrinsics.DBLE(*(_b88wiuwq+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_oxoory3e)) )) + __POW2(ILNumerics.F2NET.Intrinsics.AIMAG(*(_b88wiuwq+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_oxoory3e)) )));
Mark30:;
							// continue
						}
												}					}
					_umlkckdg = _ei7om7ok(ref _dxpq0xkr ,_dqanbbw3 ,ref Unsafe.AsRef((int)1) );
					_2qcyvkhx = (ILNumerics.F2NET.Intrinsics.CONJG(*(_b88wiuwq+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_oxoory3e)) ) / ILNumerics.F2NET.Intrinsics.SQRT(*(_dqanbbw3+(_umlkckdg - 1)) ));
					_wv0on4xy(ref _dxpq0xkr ,ref _2qcyvkhx ,(_b88wiuwq+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_oxoory3e)),ref Unsafe.AsRef((int)1) );
					*(_b88wiuwq+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_oxoory3e)) = ILNumerics.F2NET.Intrinsics.DCMPLX(ILNumerics.F2NET.Intrinsics.DBLE(*(_b88wiuwq+(_umlkckdg - 1) + (_b5p6od9s - 1) * 1 * (_oxoory3e)) ) ,_d0547bi2 );
Mark40:;
					// continue
				}
								}			}
		}
		//* 
		//*     Undo scaling if necessary 
		//* 
		
Mark50:;
		// continue
		if (_pjb22a66)
		{
			
			_j6h8q4u5("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _d6te6z4t ,ref _j6vjow1g ,ref Unsafe.AsRef(_dxpq0xkr - _gro5yvfo) ,ref Unsafe.AsRef((int)1) ,(_z1ioc3c8+(_gro5yvfo + (int)1 - 1)),ref Unsafe.AsRef(ILNumerics.F2NET.Intrinsics.MAX(_dxpq0xkr - _gro5yvfo ,(int)1 )) ,ref _bhsiylw4 );
			if (_gro5yvfo == (int)0)
			{
				
				if ((_bxh9t3ks | _xyg82qje) & (_ohcg4rre == (int)0))
				_2xvktk5a("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _d6te6z4t ,ref _j6vjow1g ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1) ,_is5n4oz3 ,ref _dxpq0xkr ,ref _bhsiylw4 );
			}
			else
			{
				
				_j6h8q4u5("G" ,ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef((int)0) ,ref _d6te6z4t ,ref _j6vjow1g ,ref Unsafe.AsRef(_pew3blan - (int)1) ,ref Unsafe.AsRef((int)1) ,_z1ioc3c8 ,ref _dxpq0xkr ,ref _bhsiylw4 );
			}
			
		}
		//* 
		
		*(_apig8meb+((int)1 - 1)) = DCMPLX(_tafa1evd);
		return;//* 
		//*     End of ZGEEVX 
		//* 
		
	}
	
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
