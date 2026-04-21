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
//*> \brief \b IPARAM2STAGE 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at  
//*            http://www.netlib.org/lapack/explore-html/  
//* 
//*> \htmlonly 
//*> Download IPARAM2STAGE + dependencies  
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/iparam2stage.F">  
//*> [TGZ]</a>  
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/iparam2stage.F">  
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/iparam2stage.F">  
//*> [TXT]</a> 
//*> \endhtmlonly  
//* 
//*  Definition: 
//*  =========== 
//* 
//*       INTEGER FUNCTION IPARAM2STAGE( ISPEC, NAME, OPTS,  
//*                                    NI, NBI, IBI, NXI ) 
//*       #if defined(_OPENMP) 
//*           use omp_lib 
//*       #endif 
//*       IMPLICIT NONE 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER*( * )    NAME, OPTS 
//*       INTEGER            ISPEC, NI, NBI, IBI, NXI 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*>      This program sets problem and machine dependent parameters 
//*>      useful for xHETRD_2STAGE, xHETRD_HE2HB, xHETRD_HB2ST, 
//*>      xGEBRD_2STAGE, xGEBRD_GE2GB, xGEBRD_GB2BD  
//*>      and related subroutines for eigenvalue problems.  
//*>      It is called whenever ILAENV is called with 17 <= ISPEC <= 21. 
//*>      It is called whenever ILAENV2STAGE is called with 1 <= ISPEC <= 5 
//*>      with a direct conversion ISPEC + 16. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] ISPEC 
//*> \verbatim 
//*>          ISPEC is integer scalar 
//*>              ISPEC specifies which tunable parameter IPARAM2STAGE should 
//*>              return. 
//*> 
//*>              ISPEC=17: the optimal blocksize nb for the reduction to 
//*>                        BAND 
//*> 
//*>              ISPEC=18: the optimal blocksize ib for the eigenvectors 
//*>                        singular vectors update routine 
//*> 
//*>              ISPEC=19: The length of the array that store the Housholder  
//*>                        representation for the second stage  
//*>                        Band to Tridiagonal or Bidiagonal 
//*> 
//*>              ISPEC=20: The workspace needed for the routine in input. 
//*> 
//*>              ISPEC=21: For future release. 
//*> \endverbatim 
//*> 
//*> \param[in] NAME 
//*> \verbatim 
//*>          NAME is character string 
//*>               Name of the calling subroutine 
//*> \endverbatim 
//*> 
//*> \param[in] OPTS 
//*> \verbatim 
//*>          OPTS is CHARACTER*(*) 
//*>          The character options to the subroutine NAME, concatenated 
//*>          into a single character string.  For example, UPLO = 'U', 
//*>          TRANS = 'T', and DIAG = 'N' for a triangular routine would 
//*>          be specified as OPTS = 'UTN'. 
//*> \endverbatim 
//*> 
//*> \param[in] NI 
//*> \verbatim 
//*>          NI is INTEGER which is the size of the matrix 
//*> \endverbatim 
//*> 
//*> \param[in] NBI 
//*> \verbatim 
//*>          NBI is INTEGER which is the used in the reduciton,  
//*>          (e.g., the size of the band), needed to compute workspace 
//*>          and LHOUS2. 
//*> \endverbatim 
//*> 
//*> \param[in] IBI 
//*> \verbatim 
//*>          IBI is INTEGER which represent the IB of the reduciton, 
//*>          needed to compute workspace and LHOUS2. 
//*> \endverbatim 
//*> 
//*> \param[in] NXI 
//*> \verbatim 
//*>          NXI is INTEGER needed in the future release. 
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
//*> \ingroup auxOTHERauxiliary 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  Implemented by Azzam Haidar. 
//*> 
//*>  All detail are available on technical report, SC11, SC13 papers. 
//*> 
//*>  Azzam Haidar, Hatem Ltaief, and Jack Dongarra. 
//*>  Parallel reduction to condensed forms for symmetric eigenvalue problems 
//*>  using aggregated fine-grained and memory-aware kernels. In Proceedings 
//*>  of 2011 International Conference for High Performance Computing, 
//*>  Networking, Storage and Analysis (SC '11), New York, NY, USA, 
//*>  Article 8 , 11 pages. 
//*>  http://doi.acm.org/10.1145/2063384.2063394 
//*> 
//*>  A. Haidar, J. Kurzak, P. Luszczek, 2013. 
//*>  An improved parallel singular value algorithm and its implementation  
//*>  for multicore hardware, In Proceedings of 2013 International Conference 
//*>  for High Performance Computing, Networking, Storage and Analysis (SC '13). 
//*>  Denver, Colorado, USA, 2013. 
//*>  Article 90, 12 pages. 
//*>  http://doi.acm.org/10.1145/2503210.2503292 
//*> 
//*>  A. Haidar, R. Solca, S. Tomov, T. Schulthess and J. Dongarra. 
//*>  A novel hybrid CPU-GPU generalized eigensolver for electronic structure  
//*>  calculations based on fine-grained memory aware tasks. 
//*>  International Journal of High Performance Computing Applications. 
//*>  Volume 28 Issue 2, Pages 196-209, May 2014. 
//*>  http://hpc.sagepub.com/content/28/2/196  
//*> 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static Int32 _ly85qlbf(ref Int32 _r22uqjla, FString _hr3apt47, FString _c7e3jn3t, ref Int32 _q8n03esx, ref Int32 _lo6kpczm, ref Int32 _6w909j16, ref Int32 _mt3j5fxm)
	{
#region variable declarations
Int32 _ly85qlbf = default;
Int32 _b5p6od9s =  default;
Int32 _8jzcrkri =  default;
Int32 _qbqg6u98 =  default;
Int32 _biqfgse9 =  default;
Int32 _vyr1z1si =  default;
Int32 _9w93llgh =  default;
Int32 _6fnxzlyp =  default;
Int32 _hqnkli1f =  default;
Int32 _1ujhvtfn =  default;
Int32 _rael19hu =  default;
Int32 _bukjg3az =  default;
Boolean _cj80e3xm =  default;
Boolean _6maeskwq =  default;
FString _rvllg4ph =  new FString(1);
FString _j3j18r87 =  new FString(3);
FString _ttfmuojx =  new FString(5);
FString _jhl0dtr8 =  new FString(12);
FString _m2oo9cc0 =  new FString(1);
string fLanavab = default;
#endregion  variable declarations

	{
		
		#if (false)
		{
			//      use omp_lib
			// 
			
		}
		
		#endif//* 
		//*  -- LAPACK auxiliary routine (version 3.8.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     June 2016 
		//* 
		//*     .. Scalar Arguments .. 
		//* 
		//*  ================================================================ 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Invalid value for ISPEC 
		//* 
		
		if ((_r22uqjla < (int)17) | (_r22uqjla > (int)21))
		{
			
			_ly85qlbf = (int)-1;
			return _ly85qlbf;
		}
		//* 
		//*     Get the number of threads 
		//*       
		
		_hqnkli1f = (int)1;
		#if (false)
		{
			//!$OMP PARALLEL  
			//      NTHREADS = OMP_GET_NUM_THREADS() <-- commented, due to error: Unidentified symbol: OMP_GET_NUM_THREADS..
			//!$OMP END PARALLEL 
			
		}
		
		#endif//*      WRITE(*,*) 'IPARAM VOICI NTHREADS ISPEC ',NTHREADS, ISPEC 
		//* 
		
		if (_r22uqjla != (int)19)
		{
			//* 
			//*        Convert NAME to upper case if the first character is lower case. 
			//* 
			
			_ly85qlbf = (int)-1;
			
			_jhl0dtr8 = (_hr3apt47).AssignTo(12);
			_8jzcrkri = ILNumerics.F2NET.Intrinsics.ICHAR(_jhl0dtr8[(int)1,(int)1] );
			_qbqg6u98 = ILNumerics.F2NET.Intrinsics.ICHAR("Z" );
			if ((_qbqg6u98 == (int)90) | (_qbqg6u98 == (int)122))
			{
				//* 
				//*           ASCII character set 
				//* 
				
				if ((_8jzcrkri >= (int)97) & (_8jzcrkri <= (int)122))
				{
					
					
					_jhl0dtr8[(int)1,(int)1] = ILNumerics.F2NET.Intrinsics.CHAR(_8jzcrkri - (int)32 );
					{
						System.Int32 __81fgg2dlsvn50 = (System.Int32)((int)2);
						const System.Int32 __81fgg2step50 = (System.Int32)((int)1);
						System.Int32 __81fgg2count50;
						for (__81fgg2count50 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)12) - __81fgg2dlsvn50 + __81fgg2step50) / __81fgg2step50)), _b5p6od9s = __81fgg2dlsvn50; __81fgg2count50 != 0; __81fgg2count50--, _b5p6od9s += (__81fgg2step50)) {

						{
							
							_8jzcrkri = ILNumerics.F2NET.Intrinsics.ICHAR(_jhl0dtr8[_b5p6od9s,_b5p6od9s] );
							if ((_8jzcrkri >= (int)97) & (_8jzcrkri <= (int)122))
							
							_jhl0dtr8[_b5p6od9s,_b5p6od9s] = ILNumerics.F2NET.Intrinsics.CHAR(_8jzcrkri - (int)32 );
Mark100:;
							// continue
						}
												}					}
				}
				//* 
				
			}
			else
			if ((_qbqg6u98 == (int)233) | (_qbqg6u98 == (int)169))
			{
				//* 
				//*           EBCDIC character set 
				//* 
				
				if ((((_8jzcrkri >= (int)129) & (_8jzcrkri <= (int)137)) | ((_8jzcrkri >= (int)145) & (_8jzcrkri <= (int)153))) | ((_8jzcrkri >= (int)162) & (_8jzcrkri <= (int)169)))
				{
					
					
					_jhl0dtr8[(int)1,(int)1] = ILNumerics.F2NET.Intrinsics.CHAR(_8jzcrkri + (int)64 );
					{
						System.Int32 __81fgg2dlsvn51 = (System.Int32)((int)2);
						const System.Int32 __81fgg2step51 = (System.Int32)((int)1);
						System.Int32 __81fgg2count51;
						for (__81fgg2count51 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)12) - __81fgg2dlsvn51 + __81fgg2step51) / __81fgg2step51)), _b5p6od9s = __81fgg2dlsvn51; __81fgg2count51 != 0; __81fgg2count51--, _b5p6od9s += (__81fgg2step51)) {

						{
							
							_8jzcrkri = ILNumerics.F2NET.Intrinsics.ICHAR(_jhl0dtr8[_b5p6od9s,_b5p6od9s] );
							if ((((_8jzcrkri >= (int)129) & (_8jzcrkri <= (int)137)) | ((_8jzcrkri >= (int)145) & (_8jzcrkri <= (int)153))) | ((_8jzcrkri >= (int)162) & (_8jzcrkri <= (int)169)))
							
							_jhl0dtr8[_b5p6od9s,_b5p6od9s] = ILNumerics.F2NET.Intrinsics.CHAR(_8jzcrkri + (int)64 );
Mark110:;
							// continue
						}
												}					}
				}
				//* 
				
			}
			else
			if ((_qbqg6u98 == (int)218) | (_qbqg6u98 == (int)250))
			{
				//* 
				//*           Prime machines:  ASCII+128 
				//* 
				
				if ((_8jzcrkri >= (int)225) & (_8jzcrkri <= (int)250))
				{
					
					
					_jhl0dtr8[(int)1,(int)1] = ILNumerics.F2NET.Intrinsics.CHAR(_8jzcrkri - (int)32 );
					{
						System.Int32 __81fgg2dlsvn52 = (System.Int32)((int)2);
						const System.Int32 __81fgg2step52 = (System.Int32)((int)1);
						System.Int32 __81fgg2count52;
						for (__81fgg2count52 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)12) - __81fgg2dlsvn52 + __81fgg2step52) / __81fgg2step52)), _b5p6od9s = __81fgg2dlsvn52; __81fgg2count52 != 0; __81fgg2count52--, _b5p6od9s += (__81fgg2step52)) {

						{
							
							_8jzcrkri = ILNumerics.F2NET.Intrinsics.ICHAR(_jhl0dtr8[_b5p6od9s,_b5p6od9s] );
							if ((_8jzcrkri >= (int)225) & (_8jzcrkri <= (int)250))
							
							_jhl0dtr8[_b5p6od9s,_b5p6od9s] = ILNumerics.F2NET.Intrinsics.CHAR(_8jzcrkri - (int)32 );
Mark120:;
							// continue
						}
												}					}
				}
				
			}
			//* 
			
			
			_rvllg4ph = (_jhl0dtr8[(int)1,(int)1]).AssignTo(1);
			
			_j3j18r87 = (_jhl0dtr8[(int)4,(int)6]).AssignTo(3);
			
			_ttfmuojx = (_jhl0dtr8[(int)8,(int)12]).AssignTo(5);
			_cj80e3xm = ((_rvllg4ph == "S") | (_rvllg4ph == "D"));
			_6maeskwq = ((_rvllg4ph == "C") | (_rvllg4ph == "Z"));//* 
			//*        Invalid value for PRECISION 
			//*       
			
			if (!((_cj80e3xm | _6maeskwq)))
			{
				
				_ly85qlbf = (int)-1;
				return _ly85qlbf;
			}
			
		}
		//*      WRITE(*,*),'RPREC,CPREC ',RPREC,CPREC, 
		//*     $           '   ALGO ',ALGO,'    STAGE ',STAG 
		//*       
		//* 
		
		if ((_r22uqjla == (int)17) | (_r22uqjla == (int)18))
		{
			//* 
			//*     ISPEC = 17, 18:  block size KD, IB 
			//*     Could be also dependent from N but for now it 
			//*     depend only on sequential or parallel 
			//* 
			
			if (_hqnkli1f > (int)4)
			{
				
				if (_6maeskwq)
				{
					
					_biqfgse9 = (int)128;
					_vyr1z1si = (int)32;
				}
				else
				{
					
					_biqfgse9 = (int)160;
					_vyr1z1si = (int)40;
				}
				
			}
			else
			if (_hqnkli1f > (int)1)
			{
				
				if (_6maeskwq)
				{
					
					_biqfgse9 = (int)64;
					_vyr1z1si = (int)32;
				}
				else
				{
					
					_biqfgse9 = (int)64;
					_vyr1z1si = (int)32;
				}
				
			}
			else
			{
				
				if (_6maeskwq)
				{
					
					_biqfgse9 = (int)16;
					_vyr1z1si = (int)16;
				}
				else
				{
					
					_biqfgse9 = (int)32;
					_vyr1z1si = (int)16;
				}
				
			}
			
			if (_r22uqjla == (int)17)
			_ly85qlbf = _biqfgse9;
			if (_r22uqjla == (int)18)
			_ly85qlbf = _vyr1z1si;//* 
			
		}
		else
		if (_r22uqjla == (int)19)
		{
			//* 
			//*     ISPEC = 19:   
			//*     LHOUS length of the Houselholder representation 
			//*     matrix (V,T) of the second stage. should be >= 1. 
			//* 
			//*     Will add the VECT OPTION HERE next release 
			
			
			_m2oo9cc0 = (_c7e3jn3t[(int)1,(int)1]).AssignTo(1);
			if (_m2oo9cc0 == "N")
			{
				
				_9w93llgh = ILNumerics.F2NET.Intrinsics.MAX((int)1 ,(int)4 * _q8n03esx );
			}
			else
			{
				//*           This is not correct, it need to call the ALGO and the stage2 
				
				_9w93llgh = (ILNumerics.F2NET.Intrinsics.MAX((int)1 ,(int)4 * _q8n03esx ) + _6w909j16);
			}
			
			if (_9w93llgh >= (int)0)
			{
				
				_ly85qlbf = _9w93llgh;
			}
			else
			{
				
				_ly85qlbf = (int)-1;
			}
			//* 
			
		}
		else
		if (_r22uqjla == (int)20)
		{
			//* 
			//*     ISPEC = 20: (21 for future use)   
			//*     LWORK length of the workspace for  
			//*     either or both stages for TRD and BRD. should be >= 1. 
			//*     TRD: 
			//*     TRD_stage 1: = LT + LW + LS1 + LS2 
			//*                  = LDT*KD + N*KD + N*MAX(KD,FACTOPTNB) + LDS2*KD  
			//*                    where LDT=LDS2=KD 
			//*                  = N*KD + N*max(KD,FACTOPTNB) + 2*KD*KD 
			//*     TRD_stage 2: = (2NB+1)*N + KD*NTHREADS 
			//*     TRD_both   : = max(stage1,stage2) + AB ( AB=(KD+1)*N ) 
			//*                  = N*KD + N*max(KD+1,FACTOPTNB)  
			//*                    + max(2*KD*KD, KD*NTHREADS)  
			//*                    + (KD+1)*N 
			
			_6fnxzlyp = (int)-1;
			
			_jhl0dtr8[(int)1,(int)1] = _rvllg4ph;
			
			_jhl0dtr8[(int)2,(int)6] = "GEQRF";
			_rael19hu = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,_jhl0dtr8 ," " ,ref _q8n03esx ,ref _lo6kpczm ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) );
			
			_jhl0dtr8[(int)2,(int)6] = "GELQF";
			_bukjg3az = _4mvd6e4d(ref Unsafe.AsRef((int)1) ,_jhl0dtr8 ," " ,ref _lo6kpczm ,ref _q8n03esx ,ref Unsafe.AsRef((int)-1) ,ref Unsafe.AsRef((int)-1) );//*        Could be QR or LQ for TRD and the max for BRD 
			
			_1ujhvtfn = ILNumerics.F2NET.Intrinsics.MAX(_rael19hu ,_bukjg3az );
			if (_j3j18r87 == "TRD")
			{
				
				if (_ttfmuojx == "2STAG")
				{
					
					_6fnxzlyp = ((((_q8n03esx * _lo6kpczm) + (_q8n03esx * ILNumerics.F2NET.Intrinsics.MAX(_lo6kpczm + (int)1 ,_1ujhvtfn ))) + ILNumerics.F2NET.Intrinsics.MAX(((int)2 * _lo6kpczm) * _lo6kpczm ,_lo6kpczm * _hqnkli1f )) + ((_lo6kpczm + (int)1) * _q8n03esx));
				}
				else
				if ((_ttfmuojx == "HE2HB") | (_ttfmuojx == "SY2SB"))
				{
					
					_6fnxzlyp = (((_q8n03esx * _lo6kpczm) + (_q8n03esx * ILNumerics.F2NET.Intrinsics.MAX(_lo6kpczm ,_1ujhvtfn ))) + (((int)2 * _lo6kpczm) * _lo6kpczm));
				}
				else
				if ((_ttfmuojx == "HB2ST") | (_ttfmuojx == "SB2ST"))
				{
					
					_6fnxzlyp = (((((int)2 * _lo6kpczm) + (int)1) * _q8n03esx) + (_lo6kpczm * _hqnkli1f));
				}
				
			}
			else
			if (_j3j18r87 == "BRD")
			{
				
				if (_ttfmuojx == "2STAG")
				{
					
					_6fnxzlyp = ((((((int)2 * _q8n03esx) * _lo6kpczm) + (_q8n03esx * ILNumerics.F2NET.Intrinsics.MAX(_lo6kpczm + (int)1 ,_1ujhvtfn ))) + ILNumerics.F2NET.Intrinsics.MAX(((int)2 * _lo6kpczm) * _lo6kpczm ,_lo6kpczm * _hqnkli1f )) + ((_lo6kpczm + (int)1) * _q8n03esx));
				}
				else
				if (_ttfmuojx == "GE2GB")
				{
					
					_6fnxzlyp = (((_q8n03esx * _lo6kpczm) + (_q8n03esx * ILNumerics.F2NET.Intrinsics.MAX(_lo6kpczm ,_1ujhvtfn ))) + (((int)2 * _lo6kpczm) * _lo6kpczm));
				}
				else
				if (_ttfmuojx == "GB2BD")
				{
					
					_6fnxzlyp = (((((int)3 * _lo6kpczm) + (int)1) * _q8n03esx) + (_lo6kpczm * _hqnkli1f));
				}
				
			}
			
			_6fnxzlyp = ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_6fnxzlyp );// 
			
			if (_6fnxzlyp > (int)0)
			{
				
				_ly85qlbf = _6fnxzlyp;
			}
			else
			{
				
				_ly85qlbf = (int)-1;
			}
			//* 
			
		}
		else
		if (_r22uqjla == (int)21)
		{
			//* 
			//*     ISPEC = 21 for future use  
			
			_ly85qlbf = _mt3j5fxm;
		}
		//* 
		//*     ==== End of IPARAM2STAGE ==== 
		//* 
		
	}
	
	return _ly85qlbf;
	} // 177

} // end class 
} // end namespace
#endif
