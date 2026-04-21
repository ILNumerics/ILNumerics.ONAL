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
//*> \brief \b ILAENV 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download ILAENV + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/ilaenv.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/ilaenv.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/ilaenv.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       INTEGER FUNCTION ILAENV( ISPEC, NAME, OPTS, N1, N2, N3, N4 ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER*( * )    NAME, OPTS 
//*       INTEGER            ISPEC, N1, N2, N3, N4 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> ILAENV is called from the LAPACK routines to choose problem-dependent 
//*> parameters for the local environment.  See ISPEC for a description of 
//*> the parameters. 
//*> 
//*> ILAENV returns an INTEGER 
//*> if ILAENV >= 0: ILAENV returns the value of the parameter specified by ISPEC 
//*> if ILAENV < 0:  if ILAENV = -k, the k-th argument had an illegal value. 
//*> 
//*> This version provides a set of parameters which should give good, 
//*> but not optimal, performance on many of the currently available 
//*> computers.  Users are encouraged to modify this subroutine to set 
//*> the tuning parameters for their particular machine using the option 
//*> and problem size information in the arguments. 
//*> 
//*> This routine will not function correctly if it is converted to all 
//*> lower case.  Converting it to all upper case is allowed. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] ISPEC 
//*> \verbatim 
//*>          ISPEC is INTEGER 
//*>          Specifies the parameter to be returned as the value of 
//*>          ILAENV. 
//*>          = 1: the optimal blocksize; if this value is 1, an unblocked 
//*>               algorithm will give the best performance. 
//*>          = 2: the minimum block size for which the block routine 
//*>               should be used; if the usable block size is less than 
//*>               this value, an unblocked routine should be used. 
//*>          = 3: the crossover point (in a block routine, for N less 
//*>               than this value, an unblocked routine should be used) 
//*>          = 4: the number of shifts, used in the nonsymmetric 
//*>               eigenvalue routines (DEPRECATED) 
//*>          = 5: the minimum column dimension for blocking to be used; 
//*>               rectangular blocks must have dimension at least k by m, 
//*>               where k is given by ILAENV(2,...) and m by ILAENV(5,...) 
//*>          = 6: the crossover point for the SVD (when reducing an m by n 
//*>               matrix to bidiagonal form, if max(m,n)/min(m,n) exceeds 
//*>               this value, a QR factorization is used first to reduce 
//*>               the matrix to a triangular form.) 
//*>          = 7: the number of processors 
//*>          = 8: the crossover point for the multishift QR method 
//*>               for nonsymmetric eigenvalue problems (DEPRECATED) 
//*>          = 9: maximum size of the subproblems at the bottom of the 
//*>               computation tree in the divide-and-conquer algorithm 
//*>               (used by xGELSD and xGESDD) 
//*>          =10: ieee NaN arithmetic can be trusted not to trap 
//*>          =11: infinity arithmetic can be trusted not to trap 
//*>          12 <= ISPEC <= 16: 
//*>               xHSEQR or related subroutines, 
//*>               see IPARMQ for detailed explanation 
//*> \endverbatim 
//*> 
//*> \param[in] NAME 
//*> \verbatim 
//*>          NAME is CHARACTER*(*) 
//*>          The name of the calling subroutine, in either upper case or 
//*>          lower case. 
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
//*> \param[in] N1 
//*> \verbatim 
//*>          N1 is INTEGER 
//*> \endverbatim 
//*> 
//*> \param[in] N2 
//*> \verbatim 
//*>          N2 is INTEGER 
//*> \endverbatim 
//*> 
//*> \param[in] N3 
//*> \verbatim 
//*>          N3 is INTEGER 
//*> \endverbatim 
//*> 
//*> \param[in] N4 
//*> \verbatim 
//*>          N4 is INTEGER 
//*>          Problem dimensions for the subroutine NAME; these may not all 
//*>          be required. 
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
//*> \date November 2019 
//* 
//*> \ingroup OTHERauxiliary 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  The following conventions have been used when calling ILAENV from the 
//*>  LAPACK routines: 
//*>  1)  OPTS is a concatenation of all of the character options to 
//*>      subroutine NAME, in the same order that they appear in the 
//*>      argument list for NAME, even if they are not used in determining 
//*>      the value of the parameter specified by ISPEC. 
//*>  2)  The problem dimensions N1, N2, N3, N4 are specified in the order 
//*>      that they appear in the argument list for NAME.  N1 is used 
//*>      first, N2 second, and so on, and unused problem dimensions are 
//*>      passed a value of -1. 
//*>  3)  The parameter value returned by ILAENV is checked for validity in 
//*>      the calling subroutine.  For example, ILAENV is used to retrieve 
//*>      the optimal blocksize for STRTRI as follows: 
//*> 
//*>      NB = ILAENV( 1, 'STRTRI', UPLO // DIAG, N, -1, -1, -1 ) 
//*>      IF( NB.LE.1 ) NB = MAX( 1, N ) 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static Int32 _4mvd6e4d(ref Int32 _r22uqjla, FString _hr3apt47, FString _c7e3jn3t, ref Int32 _4o1bt8b1, ref Int32 _tixk7d1h, ref Int32 _dnn52993, ref Int32 _kjv2e61d)
	{
#region variable declarations
Int32 _4mvd6e4d = default;
Int32 _b5p6od9s =  default;
Int32 _8jzcrkri =  default;
Int32 _qbqg6u98 =  default;
Int32 _f7059815 =  default;
Int32 _o80jnixx =  default;
Int32 _rtlyoyz3 =  default;
Boolean _6ep0s270 =  default;
Boolean _jxwhf5ea =  default;
Boolean _s4ytth5h =  default;
FString _o2zniltq =  new FString(1);
FString _h685tamv =  new FString(2);
FString _ba90vqrv =  new FString(2);
FString _plgtagjz =  new FString(3);
FString _jhl0dtr8 =  new FString(16);
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.9.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     November 2019 
		//* 
		//*     .. Scalar Arguments .. 
		//*     .. 
		//* 
		//*  ===================================================================== 
		//* 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		switch (_r22uqjla) {
						case 1:
			goto Mark10;
			case 2:
			goto Mark10;
			case 3:
			goto Mark10;
			case 4:
			goto Mark80;
			case 5:
			goto Mark90;
			case 6:
			goto Mark100;
			case 7:
			goto Mark110;
			case 8:
			goto Mark120;
			case 9:
			goto Mark130;
			case 10:
			goto Mark140;
			case 11:
			goto Mark150;
			case 12:
			goto Mark160;
			case 13:
			goto Mark160;
			case 14:
			goto Mark160;
			case 15:
			goto Mark160;
			case 16:
			goto Mark160;
			default:
			break;
		}
//* 
		//*     Invalid value for ISPEC 
		//* 
		
		_4mvd6e4d = (int)-1;
		return _4mvd6e4d;//* 
		
Mark10:;
		// continue//* 
		//*     Convert NAME to upper case if the first character is lower case. 
		//* 
		
		_4mvd6e4d = (int)1;
		
		_jhl0dtr8 = (_hr3apt47).AssignTo(16);
		_8jzcrkri = ILNumerics.F2NET.Intrinsics.ICHAR(_jhl0dtr8[(int)1,(int)1] );
		_qbqg6u98 = ILNumerics.F2NET.Intrinsics.ICHAR("Z" );
		if ((_qbqg6u98 == (int)90) | (_qbqg6u98 == (int)122))
		{
			//* 
			//*        ASCII character set 
			//* 
			
			if ((_8jzcrkri >= (int)97) & (_8jzcrkri <= (int)122))
			{
				
				
				_jhl0dtr8[(int)1,(int)1] = ILNumerics.F2NET.Intrinsics.CHAR(_8jzcrkri - (int)32 );
				{
					System.Int32 __81fgg2dlsvn44 = (System.Int32)((int)2);
					const System.Int32 __81fgg2step44 = (System.Int32)((int)1);
					System.Int32 __81fgg2count44;
					for (__81fgg2count44 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)6) - __81fgg2dlsvn44 + __81fgg2step44) / __81fgg2step44)), _b5p6od9s = __81fgg2dlsvn44; __81fgg2count44 != 0; __81fgg2count44--, _b5p6od9s += (__81fgg2step44)) {

					{
						
						_8jzcrkri = ILNumerics.F2NET.Intrinsics.ICHAR(_jhl0dtr8[_b5p6od9s,_b5p6od9s] );
						if ((_8jzcrkri >= (int)97) & (_8jzcrkri <= (int)122))
						
						_jhl0dtr8[_b5p6od9s,_b5p6od9s] = ILNumerics.F2NET.Intrinsics.CHAR(_8jzcrkri - (int)32 );
Mark20:;
						// continue
					}
										}				}
			}
			//* 
			
		}
		else
		if ((_qbqg6u98 == (int)233) | (_qbqg6u98 == (int)169))
		{
			//* 
			//*        EBCDIC character set 
			//* 
			
			if ((((_8jzcrkri >= (int)129) & (_8jzcrkri <= (int)137)) | ((_8jzcrkri >= (int)145) & (_8jzcrkri <= (int)153))) | ((_8jzcrkri >= (int)162) & (_8jzcrkri <= (int)169)))
			{
				
				
				_jhl0dtr8[(int)1,(int)1] = ILNumerics.F2NET.Intrinsics.CHAR(_8jzcrkri + (int)64 );
				{
					System.Int32 __81fgg2dlsvn45 = (System.Int32)((int)2);
					const System.Int32 __81fgg2step45 = (System.Int32)((int)1);
					System.Int32 __81fgg2count45;
					for (__81fgg2count45 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)6) - __81fgg2dlsvn45 + __81fgg2step45) / __81fgg2step45)), _b5p6od9s = __81fgg2dlsvn45; __81fgg2count45 != 0; __81fgg2count45--, _b5p6od9s += (__81fgg2step45)) {

					{
						
						_8jzcrkri = ILNumerics.F2NET.Intrinsics.ICHAR(_jhl0dtr8[_b5p6od9s,_b5p6od9s] );
						if ((((_8jzcrkri >= (int)129) & (_8jzcrkri <= (int)137)) | ((_8jzcrkri >= (int)145) & (_8jzcrkri <= (int)153))) | ((_8jzcrkri >= (int)162) & (_8jzcrkri <= (int)169)))
						
						_jhl0dtr8[_b5p6od9s,_b5p6od9s] = ILNumerics.F2NET.Intrinsics.CHAR(_8jzcrkri + (int)64 );
Mark30:;
						// continue
					}
										}				}
			}
			//* 
			
		}
		else
		if ((_qbqg6u98 == (int)218) | (_qbqg6u98 == (int)250))
		{
			//* 
			//*        Prime machines:  ASCII+128 
			//* 
			
			if ((_8jzcrkri >= (int)225) & (_8jzcrkri <= (int)250))
			{
				
				
				_jhl0dtr8[(int)1,(int)1] = ILNumerics.F2NET.Intrinsics.CHAR(_8jzcrkri - (int)32 );
				{
					System.Int32 __81fgg2dlsvn46 = (System.Int32)((int)2);
					const System.Int32 __81fgg2step46 = (System.Int32)((int)1);
					System.Int32 __81fgg2count46;
					for (__81fgg2count46 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)6) - __81fgg2dlsvn46 + __81fgg2step46) / __81fgg2step46)), _b5p6od9s = __81fgg2dlsvn46; __81fgg2count46 != 0; __81fgg2count46--, _b5p6od9s += (__81fgg2step46)) {

					{
						
						_8jzcrkri = ILNumerics.F2NET.Intrinsics.ICHAR(_jhl0dtr8[_b5p6od9s,_b5p6od9s] );
						if ((_8jzcrkri >= (int)225) & (_8jzcrkri <= (int)250))
						
						_jhl0dtr8[_b5p6od9s,_b5p6od9s] = ILNumerics.F2NET.Intrinsics.CHAR(_8jzcrkri - (int)32 );
Mark40:;
						// continue
					}
										}				}
			}
			
		}
		//* 
		
		
		_o2zniltq = (_jhl0dtr8[(int)1,(int)1]).AssignTo(1);
		_jxwhf5ea = ((_o2zniltq == "S") | (_o2zniltq == "D"));
		_6ep0s270 = ((_o2zniltq == "C") | (_o2zniltq == "Z"));
		if (!((_6ep0s270 | _jxwhf5ea)))
		return _4mvd6e4d;
		
		_h685tamv = (_jhl0dtr8[(int)2,(int)3]).AssignTo(2);
		
		_plgtagjz = (_jhl0dtr8[(int)4,(int)6]).AssignTo(3);
		
		_ba90vqrv = (_plgtagjz[(int)2,(int)3]).AssignTo(2);
		_s4ytth5h = ((ILNumerics.F2NET.Intrinsics.LEN(_jhl0dtr8 ) >= (int)11) & (_jhl0dtr8[(int)11,(int)11] == "2"));//* 
		
		switch (_r22uqjla) {
						case 1:
			goto Mark50;
			case 2:
			goto Mark60;
			case 3:
			goto Mark70;
			default:
			break;
		}
//* 
		
Mark50:;
		// continue//* 
		//*     ISPEC = 1:  block size 
		//* 
		//*     In these examples, separate code is provided for setting NB for 
		//*     real and complex.  We assume that NB will take the same value in 
		//*     single or double precision. 
		//* 
		
		_f7059815 = (int)1;//* 
		
		if (_jhl0dtr8[(int)2,(int)6] == "LAORH")
		{
			//* 
			//*        This is for *LAORHR_GETRFNP routine 
			//* 
			
			if (_jxwhf5ea)
			{
				
				_f7059815 = (int)32;
			}
			else
			{
				
				_f7059815 = (int)32;
			}
			
		}
		else
		if (_h685tamv == "GE")
		{
			
			if (_plgtagjz == "TRF")
			{
				
				if (_jxwhf5ea)
				{
					
					_f7059815 = (int)64;
				}
				else
				{
					
					_f7059815 = (int)64;
				}
				
			}
			else
			if ((((_plgtagjz == "QRF") | (_plgtagjz == "RQF")) | (_plgtagjz == "LQF")) | (_plgtagjz == "QLF"))
			{
				
				if (_jxwhf5ea)
				{
					
					_f7059815 = (int)32;
				}
				else
				{
					
					_f7059815 = (int)32;
				}
				
			}
			else
			if (_plgtagjz == "QR ")
			{
				
				if (_dnn52993 == (int)1)
				{
					
					if (_jxwhf5ea)
					{
						//*     M*N 
						
						if (((_4o1bt8b1 * _tixk7d1h) <= (int)131072) | (_4o1bt8b1 <= (int)8192))
						{
							
							_f7059815 = _4o1bt8b1;
						}
						else
						{
							
							_f7059815 = ((int)32768 / _tixk7d1h);
						}
						
					}
					else
					{
						
						if (((_4o1bt8b1 * _tixk7d1h) <= (int)131072) | (_4o1bt8b1 <= (int)8192))
						{
							
							_f7059815 = _4o1bt8b1;
						}
						else
						{
							
							_f7059815 = ((int)32768 / _tixk7d1h);
						}
						
					}
					
				}
				else
				{
					
					if (_jxwhf5ea)
					{
						
						_f7059815 = (int)1;
					}
					else
					{
						
						_f7059815 = (int)1;
					}
					
				}
				
			}
			else
			if (_plgtagjz == "LQ ")
			{
				
				if (_dnn52993 == (int)2)
				{
					
					if (_jxwhf5ea)
					{
						//*     M*N 
						
						if (((_4o1bt8b1 * _tixk7d1h) <= (int)131072) | (_4o1bt8b1 <= (int)8192))
						{
							
							_f7059815 = _4o1bt8b1;
						}
						else
						{
							
							_f7059815 = ((int)32768 / _tixk7d1h);
						}
						
					}
					else
					{
						
						if (((_4o1bt8b1 * _tixk7d1h) <= (int)131072) | (_4o1bt8b1 <= (int)8192))
						{
							
							_f7059815 = _4o1bt8b1;
						}
						else
						{
							
							_f7059815 = ((int)32768 / _tixk7d1h);
						}
						
					}
					
				}
				else
				{
					
					if (_jxwhf5ea)
					{
						
						_f7059815 = (int)1;
					}
					else
					{
						
						_f7059815 = (int)1;
					}
					
				}
				
			}
			else
			if (_plgtagjz == "HRD")
			{
				
				if (_jxwhf5ea)
				{
					
					_f7059815 = (int)32;
				}
				else
				{
					
					_f7059815 = (int)32;
				}
				
			}
			else
			if (_plgtagjz == "BRD")
			{
				
				if (_jxwhf5ea)
				{
					
					_f7059815 = (int)32;
				}
				else
				{
					
					_f7059815 = (int)32;
				}
				
			}
			else
			if (_plgtagjz == "TRI")
			{
				
				if (_jxwhf5ea)
				{
					
					_f7059815 = (int)64;
				}
				else
				{
					
					_f7059815 = (int)64;
				}
				
			}
			
		}
		else
		if (_h685tamv == "PO")
		{
			
			if (_plgtagjz == "TRF")
			{
				
				if (_jxwhf5ea)
				{
					
					_f7059815 = (int)64;
				}
				else
				{
					
					_f7059815 = (int)64;
				}
				
			}
			
		}
		else
		if (_h685tamv == "SY")
		{
			
			if (_plgtagjz == "TRF")
			{
				
				if (_jxwhf5ea)
				{
					
					if (_s4ytth5h)
					{
						
						_f7059815 = (int)192;
					}
					else
					{
						
						_f7059815 = (int)64;
					}
					
				}
				else
				{
					
					if (_s4ytth5h)
					{
						
						_f7059815 = (int)192;
					}
					else
					{
						
						_f7059815 = (int)64;
					}
					
				}
				
			}
			else
			if (_jxwhf5ea & (_plgtagjz == "TRD"))
			{
				
				_f7059815 = (int)32;
			}
			else
			if (_jxwhf5ea & (_plgtagjz == "GST"))
			{
				
				_f7059815 = (int)64;
			}
			
		}
		else
		if (_6ep0s270 & (_h685tamv == "HE"))
		{
			
			if (_plgtagjz == "TRF")
			{
				
				if (_s4ytth5h)
				{
					
					_f7059815 = (int)192;
				}
				else
				{
					
					_f7059815 = (int)64;
				}
				
			}
			else
			if (_plgtagjz == "TRD")
			{
				
				_f7059815 = (int)32;
			}
			else
			if (_plgtagjz == "GST")
			{
				
				_f7059815 = (int)64;
			}
			
		}
		else
		if (_jxwhf5ea & (_h685tamv == "OR"))
		{
			
			if (_plgtagjz[(int)1,(int)1] == "G")
			{
				
				if (((((((_ba90vqrv == "QR") | (_ba90vqrv == "RQ")) | (_ba90vqrv == "LQ")) | (_ba90vqrv == "QL")) | (_ba90vqrv == "HR")) | (_ba90vqrv == "TR")) | (_ba90vqrv == "BR"))
				{
					
					_f7059815 = (int)32;
				}
				
			}
			else
			if (_plgtagjz[(int)1,(int)1] == "M")
			{
				
				if (((((((_ba90vqrv == "QR") | (_ba90vqrv == "RQ")) | (_ba90vqrv == "LQ")) | (_ba90vqrv == "QL")) | (_ba90vqrv == "HR")) | (_ba90vqrv == "TR")) | (_ba90vqrv == "BR"))
				{
					
					_f7059815 = (int)32;
				}
				
			}
			
		}
		else
		if (_6ep0s270 & (_h685tamv == "UN"))
		{
			
			if (_plgtagjz[(int)1,(int)1] == "G")
			{
				
				if (((((((_ba90vqrv == "QR") | (_ba90vqrv == "RQ")) | (_ba90vqrv == "LQ")) | (_ba90vqrv == "QL")) | (_ba90vqrv == "HR")) | (_ba90vqrv == "TR")) | (_ba90vqrv == "BR"))
				{
					
					_f7059815 = (int)32;
				}
				
			}
			else
			if (_plgtagjz[(int)1,(int)1] == "M")
			{
				
				if (((((((_ba90vqrv == "QR") | (_ba90vqrv == "RQ")) | (_ba90vqrv == "LQ")) | (_ba90vqrv == "QL")) | (_ba90vqrv == "HR")) | (_ba90vqrv == "TR")) | (_ba90vqrv == "BR"))
				{
					
					_f7059815 = (int)32;
				}
				
			}
			
		}
		else
		if (_h685tamv == "GB")
		{
			
			if (_plgtagjz == "TRF")
			{
				
				if (_jxwhf5ea)
				{
					
					if (_kjv2e61d <= (int)64)
					{
						
						_f7059815 = (int)1;
					}
					else
					{
						
						_f7059815 = (int)32;
					}
					
				}
				else
				{
					
					if (_kjv2e61d <= (int)64)
					{
						
						_f7059815 = (int)1;
					}
					else
					{
						
						_f7059815 = (int)32;
					}
					
				}
				
			}
			
		}
		else
		if (_h685tamv == "PB")
		{
			
			if (_plgtagjz == "TRF")
			{
				
				if (_jxwhf5ea)
				{
					
					if (_tixk7d1h <= (int)64)
					{
						
						_f7059815 = (int)1;
					}
					else
					{
						
						_f7059815 = (int)32;
					}
					
				}
				else
				{
					
					if (_tixk7d1h <= (int)64)
					{
						
						_f7059815 = (int)1;
					}
					else
					{
						
						_f7059815 = (int)32;
					}
					
				}
				
			}
			
		}
		else
		if (_h685tamv == "TR")
		{
			
			if (_plgtagjz == "TRI")
			{
				
				if (_jxwhf5ea)
				{
					
					_f7059815 = (int)64;
				}
				else
				{
					
					_f7059815 = (int)64;
				}
				
			}
			else
			if (_plgtagjz == "EVC")
			{
				
				if (_jxwhf5ea)
				{
					
					_f7059815 = (int)64;
				}
				else
				{
					
					_f7059815 = (int)64;
				}
				
			}
			
		}
		else
		if (_h685tamv == "LA")
		{
			
			if (_plgtagjz == "UUM")
			{
				
				if (_jxwhf5ea)
				{
					
					_f7059815 = (int)64;
				}
				else
				{
					
					_f7059815 = (int)64;
				}
				
			}
			
		}
		else
		if (_jxwhf5ea & (_h685tamv == "ST"))
		{
			
			if (_plgtagjz == "EBZ")
			{
				
				_f7059815 = (int)1;
			}
			
		}
		else
		if (_h685tamv == "GG")
		{
			
			_f7059815 = (int)32;
			if (_plgtagjz == "HD3")
			{
				
				if (_jxwhf5ea)
				{
					
					_f7059815 = (int)32;
				}
				else
				{
					
					_f7059815 = (int)32;
				}
				
			}
			
		}
		
		_4mvd6e4d = _f7059815;
		return _4mvd6e4d;//* 
		
Mark60:;
		// continue//* 
		//*     ISPEC = 2:  minimum block size 
		//* 
		
		_o80jnixx = (int)2;
		if (_h685tamv == "GE")
		{
			
			if ((((_plgtagjz == "QRF") | (_plgtagjz == "RQF")) | (_plgtagjz == "LQF")) | (_plgtagjz == "QLF"))
			{
				
				if (_jxwhf5ea)
				{
					
					_o80jnixx = (int)2;
				}
				else
				{
					
					_o80jnixx = (int)2;
				}
				
			}
			else
			if (_plgtagjz == "HRD")
			{
				
				if (_jxwhf5ea)
				{
					
					_o80jnixx = (int)2;
				}
				else
				{
					
					_o80jnixx = (int)2;
				}
				
			}
			else
			if (_plgtagjz == "BRD")
			{
				
				if (_jxwhf5ea)
				{
					
					_o80jnixx = (int)2;
				}
				else
				{
					
					_o80jnixx = (int)2;
				}
				
			}
			else
			if (_plgtagjz == "TRI")
			{
				
				if (_jxwhf5ea)
				{
					
					_o80jnixx = (int)2;
				}
				else
				{
					
					_o80jnixx = (int)2;
				}
				
			}
			
		}
		else
		if (_h685tamv == "SY")
		{
			
			if (_plgtagjz == "TRF")
			{
				
				if (_jxwhf5ea)
				{
					
					_o80jnixx = (int)8;
				}
				else
				{
					
					_o80jnixx = (int)8;
				}
				
			}
			else
			if (_jxwhf5ea & (_plgtagjz == "TRD"))
			{
				
				_o80jnixx = (int)2;
			}
			
		}
		else
		if (_6ep0s270 & (_h685tamv == "HE"))
		{
			
			if (_plgtagjz == "TRD")
			{
				
				_o80jnixx = (int)2;
			}
			
		}
		else
		if (_jxwhf5ea & (_h685tamv == "OR"))
		{
			
			if (_plgtagjz[(int)1,(int)1] == "G")
			{
				
				if (((((((_ba90vqrv == "QR") | (_ba90vqrv == "RQ")) | (_ba90vqrv == "LQ")) | (_ba90vqrv == "QL")) | (_ba90vqrv == "HR")) | (_ba90vqrv == "TR")) | (_ba90vqrv == "BR"))
				{
					
					_o80jnixx = (int)2;
				}
				
			}
			else
			if (_plgtagjz[(int)1,(int)1] == "M")
			{
				
				if (((((((_ba90vqrv == "QR") | (_ba90vqrv == "RQ")) | (_ba90vqrv == "LQ")) | (_ba90vqrv == "QL")) | (_ba90vqrv == "HR")) | (_ba90vqrv == "TR")) | (_ba90vqrv == "BR"))
				{
					
					_o80jnixx = (int)2;
				}
				
			}
			
		}
		else
		if (_6ep0s270 & (_h685tamv == "UN"))
		{
			
			if (_plgtagjz[(int)1,(int)1] == "G")
			{
				
				if (((((((_ba90vqrv == "QR") | (_ba90vqrv == "RQ")) | (_ba90vqrv == "LQ")) | (_ba90vqrv == "QL")) | (_ba90vqrv == "HR")) | (_ba90vqrv == "TR")) | (_ba90vqrv == "BR"))
				{
					
					_o80jnixx = (int)2;
				}
				
			}
			else
			if (_plgtagjz[(int)1,(int)1] == "M")
			{
				
				if (((((((_ba90vqrv == "QR") | (_ba90vqrv == "RQ")) | (_ba90vqrv == "LQ")) | (_ba90vqrv == "QL")) | (_ba90vqrv == "HR")) | (_ba90vqrv == "TR")) | (_ba90vqrv == "BR"))
				{
					
					_o80jnixx = (int)2;
				}
				
			}
			
		}
		else
		if (_h685tamv == "GG")
		{
			
			_o80jnixx = (int)2;
			if (_plgtagjz == "HD3")
			{
				
				_o80jnixx = (int)2;
			}
			
		}
		
		_4mvd6e4d = _o80jnixx;
		return _4mvd6e4d;//* 
		
Mark70:;
		// continue//* 
		//*     ISPEC = 3:  crossover point 
		//* 
		
		_rtlyoyz3 = (int)0;
		if (_h685tamv == "GE")
		{
			
			if ((((_plgtagjz == "QRF") | (_plgtagjz == "RQF")) | (_plgtagjz == "LQF")) | (_plgtagjz == "QLF"))
			{
				
				if (_jxwhf5ea)
				{
					
					_rtlyoyz3 = (int)128;
				}
				else
				{
					
					_rtlyoyz3 = (int)128;
				}
				
			}
			else
			if (_plgtagjz == "HRD")
			{
				
				if (_jxwhf5ea)
				{
					
					_rtlyoyz3 = (int)128;
				}
				else
				{
					
					_rtlyoyz3 = (int)128;
				}
				
			}
			else
			if (_plgtagjz == "BRD")
			{
				
				if (_jxwhf5ea)
				{
					
					_rtlyoyz3 = (int)128;
				}
				else
				{
					
					_rtlyoyz3 = (int)128;
				}
				
			}
			
		}
		else
		if (_h685tamv == "SY")
		{
			
			if (_jxwhf5ea & (_plgtagjz == "TRD"))
			{
				
				_rtlyoyz3 = (int)32;
			}
			
		}
		else
		if (_6ep0s270 & (_h685tamv == "HE"))
		{
			
			if (_plgtagjz == "TRD")
			{
				
				_rtlyoyz3 = (int)32;
			}
			
		}
		else
		if (_jxwhf5ea & (_h685tamv == "OR"))
		{
			
			if (_plgtagjz[(int)1,(int)1] == "G")
			{
				
				if (((((((_ba90vqrv == "QR") | (_ba90vqrv == "RQ")) | (_ba90vqrv == "LQ")) | (_ba90vqrv == "QL")) | (_ba90vqrv == "HR")) | (_ba90vqrv == "TR")) | (_ba90vqrv == "BR"))
				{
					
					_rtlyoyz3 = (int)128;
				}
				
			}
			
		}
		else
		if (_6ep0s270 & (_h685tamv == "UN"))
		{
			
			if (_plgtagjz[(int)1,(int)1] == "G")
			{
				
				if (((((((_ba90vqrv == "QR") | (_ba90vqrv == "RQ")) | (_ba90vqrv == "LQ")) | (_ba90vqrv == "QL")) | (_ba90vqrv == "HR")) | (_ba90vqrv == "TR")) | (_ba90vqrv == "BR"))
				{
					
					_rtlyoyz3 = (int)128;
				}
				
			}
			
		}
		else
		if (_h685tamv == "GG")
		{
			
			_rtlyoyz3 = (int)128;
			if (_plgtagjz == "HD3")
			{
				
				_rtlyoyz3 = (int)128;
			}
			
		}
		
		_4mvd6e4d = _rtlyoyz3;
		return _4mvd6e4d;//* 
		
Mark80:;
		// continue//* 
		//*     ISPEC = 4:  number of shifts (used by xHSEQR) 
		//* 
		
		_4mvd6e4d = (int)6;
		return _4mvd6e4d;//* 
		
Mark90:;
		// continue//* 
		//*     ISPEC = 5:  minimum column dimension (not used) 
		//* 
		
		_4mvd6e4d = (int)2;
		return _4mvd6e4d;//* 
		
Mark100:;
		// continue//* 
		//*     ISPEC = 6:  crossover point for SVD (used by xGELSS and xGESVD) 
		//* 
		
		_4mvd6e4d = ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.REAL(ILNumerics.F2NET.Intrinsics.MIN(_4o1bt8b1 ,_tixk7d1h ) ) * 1.6f );
		return _4mvd6e4d;//* 
		
Mark110:;
		// continue//* 
		//*     ISPEC = 7:  number of processors (not used) 
		//* 
		
		_4mvd6e4d = (int)1;
		return _4mvd6e4d;//* 
		
Mark120:;
		// continue//* 
		//*     ISPEC = 8:  crossover point for multishift (used by xHSEQR) 
		//* 
		
		_4mvd6e4d = (int)50;
		return _4mvd6e4d;//* 
		
Mark130:;
		// continue//* 
		//*     ISPEC = 9:  maximum size of the subproblems at the bottom of the 
		//*                 computation tree in the divide-and-conquer algorithm 
		//*                 (used by xGELSD and xGESDD) 
		//* 
		
		_4mvd6e4d = (int)25;
		return _4mvd6e4d;//* 
		
Mark140:;
		// continue//* 
		//*     ISPEC = 10: ieee NaN arithmetic can be trusted not to trap 
		//* 
		//*     ILAENV = 0 
		
		_4mvd6e4d = (int)1;
		if (_4mvd6e4d == (int)1)
		{
			
			_4mvd6e4d = _df9j6irr(ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef(0f) ,ref Unsafe.AsRef(1f) );
		}
		
		return _4mvd6e4d;//* 
		
Mark150:;
		// continue//* 
		//*     ISPEC = 11: infinity arithmetic can be trusted not to trap 
		//* 
		//*     ILAENV = 0 
		
		_4mvd6e4d = (int)1;
		if (_4mvd6e4d == (int)1)
		{
			
			_4mvd6e4d = _df9j6irr(ref Unsafe.AsRef((int)0) ,ref Unsafe.AsRef(0f) ,ref Unsafe.AsRef(1f) );
		}
		
		return _4mvd6e4d;//* 
		
Mark160:;
		// continue//* 
		//*     12 <= ISPEC <= 16: xHSEQR or related subroutines. 
		//* 
		
		_4mvd6e4d = _scrthew8(ref _r22uqjla ,_hr3apt47 ,_c7e3jn3t ,ref _4o1bt8b1 ,ref _tixk7d1h ,ref _dnn52993 ,ref _kjv2e61d );
		return _4mvd6e4d;//* 
		//*     End of ILAENV 
		//* 
		
	}
	
	return _4mvd6e4d;
	} // 177

} // end class 
} // end namespace
#endif
