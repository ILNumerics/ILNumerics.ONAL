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
//*> \brief \b SLASDQ computes the SVD of a real bidiagonal matrix with diagonal d and off-diagonal e. Used by sbdsdc. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLASDQ + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slasdq.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slasdq.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slasdq.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLASDQ( UPLO, SQRE, N, NCVT, NRU, NCC, D, E, VT, LDVT, 
//*                          U, LDU, C, LDC, WORK, INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       CHARACTER          UPLO 
//*       INTEGER            INFO, LDC, LDU, LDVT, N, NCC, NCVT, NRU, SQRE 
//*       .. 
//*       .. Array Arguments .. 
//*       REAL               C( LDC, * ), D( * ), E( * ), U( LDU, * ), 
//*      $                   VT( LDVT, * ), WORK( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLASDQ computes the singular value decomposition (SVD) of a real 
//*> (upper or lower) bidiagonal matrix with diagonal D and offdiagonal 
//*> E, accumulating the transformations if desired. Letting B denote 
//*> the input bidiagonal matrix, the algorithm computes orthogonal 
//*> matrices Q and P such that B = Q * S * P**T (P**T denotes the transpose 
//*> of P). The singular values S are overwritten on D. 
//*> 
//*> The input matrix U  is changed to U  * Q  if desired. 
//*> The input matrix VT is changed to P**T * VT if desired. 
//*> The input matrix C  is changed to Q**T * C  if desired. 
//*> 
//*> See "Computing  Small Singular Values of Bidiagonal Matrices With 
//*> Guaranteed High Relative Accuracy," by J. Demmel and W. Kahan, 
//*> LAPACK Working Note #3, for a detailed description of the algorithm. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] UPLO 
//*> \verbatim 
//*>          UPLO is CHARACTER*1 
//*>        On entry, UPLO specifies whether the input bidiagonal matrix 
//*>        is upper or lower bidiagonal, and whether it is square are 
//*>        not. 
//*>           UPLO = 'U' or 'u'   B is upper bidiagonal. 
//*>           UPLO = 'L' or 'l'   B is lower bidiagonal. 
//*> \endverbatim 
//*> 
//*> \param[in] SQRE 
//*> \verbatim 
//*>          SQRE is INTEGER 
//*>        = 0: then the input matrix is N-by-N. 
//*>        = 1: then the input matrix is N-by-(N+1) if UPLU = 'U' and 
//*>             (N+1)-by-N if UPLU = 'L'. 
//*> 
//*>        The bidiagonal matrix has 
//*>        N = NL + NR + 1 rows and 
//*>        M = N + SQRE >= N columns. 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>        On entry, N specifies the number of rows and columns 
//*>        in the matrix. N must be at least 0. 
//*> \endverbatim 
//*> 
//*> \param[in] NCVT 
//*> \verbatim 
//*>          NCVT is INTEGER 
//*>        On entry, NCVT specifies the number of columns of 
//*>        the matrix VT. NCVT must be at least 0. 
//*> \endverbatim 
//*> 
//*> \param[in] NRU 
//*> \verbatim 
//*>          NRU is INTEGER 
//*>        On entry, NRU specifies the number of rows of 
//*>        the matrix U. NRU must be at least 0. 
//*> \endverbatim 
//*> 
//*> \param[in] NCC 
//*> \verbatim 
//*>          NCC is INTEGER 
//*>        On entry, NCC specifies the number of columns of 
//*>        the matrix C. NCC must be at least 0. 
//*> \endverbatim 
//*> 
//*> \param[in,out] D 
//*> \verbatim 
//*>          D is REAL array, dimension (N) 
//*>        On entry, D contains the diagonal entries of the 
//*>        bidiagonal matrix whose SVD is desired. On normal exit, 
//*>        D contains the singular values in ascending order. 
//*> \endverbatim 
//*> 
//*> \param[in,out] E 
//*> \verbatim 
//*>          E is REAL array. 
//*>        dimension is (N-1) if SQRE = 0 and N if SQRE = 1. 
//*>        On entry, the entries of E contain the offdiagonal entries 
//*>        of the bidiagonal matrix whose SVD is desired. On normal 
//*>        exit, E will contain 0. If the algorithm does not converge, 
//*>        D and E will contain the diagonal and superdiagonal entries 
//*>        of a bidiagonal matrix orthogonally equivalent to the one 
//*>        given as input. 
//*> \endverbatim 
//*> 
//*> \param[in,out] VT 
//*> \verbatim 
//*>          VT is REAL array, dimension (LDVT, NCVT) 
//*>        On entry, contains a matrix which on exit has been 
//*>        premultiplied by P**T, dimension N-by-NCVT if SQRE = 0 
//*>        and (N+1)-by-NCVT if SQRE = 1 (not referenced if NCVT=0). 
//*> \endverbatim 
//*> 
//*> \param[in] LDVT 
//*> \verbatim 
//*>          LDVT is INTEGER 
//*>        On entry, LDVT specifies the leading dimension of VT as 
//*>        declared in the calling (sub) program. LDVT must be at 
//*>        least 1. If NCVT is nonzero LDVT must also be at least N. 
//*> \endverbatim 
//*> 
//*> \param[in,out] U 
//*> \verbatim 
//*>          U is REAL array, dimension (LDU, N) 
//*>        On entry, contains a  matrix which on exit has been 
//*>        postmultiplied by Q, dimension NRU-by-N if SQRE = 0 
//*>        and NRU-by-(N+1) if SQRE = 1 (not referenced if NRU=0). 
//*> \endverbatim 
//*> 
//*> \param[in] LDU 
//*> \verbatim 
//*>          LDU is INTEGER 
//*>        On entry, LDU  specifies the leading dimension of U as 
//*>        declared in the calling (sub) program. LDU must be at 
//*>        least max( 1, NRU ) . 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is REAL array, dimension (LDC, NCC) 
//*>        On entry, contains an N-by-NCC matrix which on exit 
//*>        has been premultiplied by Q**T  dimension N-by-NCC if SQRE = 0 
//*>        and (N+1)-by-NCC if SQRE = 1 (not referenced if NCC=0). 
//*> \endverbatim 
//*> 
//*> \param[in] LDC 
//*> \verbatim 
//*>          LDC is INTEGER 
//*>        On entry, LDC  specifies the leading dimension of C as 
//*>        declared in the calling (sub) program. LDC must be at 
//*>        least 1. If NCC is nonzero, LDC must also be at least N. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is REAL array, dimension (4*N) 
//*>        Workspace. Only referenced if one of NCVT, NRU, or NCC is 
//*>        nonzero, and if N is at least 2. 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>        On exit, a value of 0 indicates a successful exit. 
//*>        If INFO < 0, argument number -INFO is illegal. 
//*>        If INFO > 0, the algorithm did not converge, and INFO 
//*>        specifies how many superdiagonals did not converge. 
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
//*> \par Contributors: 
//*  ================== 
//*> 
//*>     Ming Gu and Huan Ren, Computer Science Division, University of 
//*>     California at Berkeley, USA 
//*> 
//*  ===================================================================== 

	 
	public static void _zfmooian(FString _9wyre9zc, ref Int32 _9qyq7j3e, ref Int32 _dxpq0xkr, ref Int32 _0v6rtqiq, ref Int32 _tg62ial7, ref Int32 _bcsi4mx0, Single* _plfm7z8g, Single* _864fslqq, Single* _xdbczr8u, ref Int32 _h4ibbatv, Single* _7u55mqkq, ref Int32 _u6e6d39b, Single* _3crf0qn3, ref Int32 _1s3eymp4, Single* _apig8meb, ref Int32 _gro5yvfo)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Boolean _77utirhk =  default;
Int32 _b5p6od9s =  default;
Int32 _ndl0tuhx =  default;
Int32 _ql1ymlhy =  default;
Int32 _znpjgsef =  default;
Int32 _ykgzqw5a =  default;
Int32 _v0c3oyw5 =  default;
Single _82tpdhyl =  default;
Single _q2vwp05i =  default;
Single _rhnpgpoi =  default;
Single _8tmd0ner =  default;
string fLanavab = default;
#endregion  variable declarations
_9wyre9zc = _9wyre9zc.Convert(1);

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.0) -- 
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
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Test the input parameters. 
		//* 
		
		_gro5yvfo = (int)0;
		_ql1ymlhy = (int)0;
		if (_w8y2rzgy(_9wyre9zc ,"U" ))
		_ql1ymlhy = (int)1;
		if (_w8y2rzgy(_9wyre9zc ,"L" ))
		_ql1ymlhy = (int)2;
		if (_ql1ymlhy == (int)0)
		{
			
			_gro5yvfo = (int)-1;
		}
		else
		if ((_9qyq7j3e < (int)0) | (_9qyq7j3e > (int)1))
		{
			
			_gro5yvfo = (int)-2;
		}
		else
		if (_dxpq0xkr < (int)0)
		{
			
			_gro5yvfo = (int)-3;
		}
		else
		if (_0v6rtqiq < (int)0)
		{
			
			_gro5yvfo = (int)-4;
		}
		else
		if (_tg62ial7 < (int)0)
		{
			
			_gro5yvfo = (int)-5;
		}
		else
		if (_bcsi4mx0 < (int)0)
		{
			
			_gro5yvfo = (int)-6;
		}
		else
		if (((_0v6rtqiq == (int)0) & (_h4ibbatv < (int)1)) | ((_0v6rtqiq > (int)0) & (_h4ibbatv < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))))
		{
			
			_gro5yvfo = (int)-10;
		}
		else
		if (_u6e6d39b < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_tg62ial7 ))
		{
			
			_gro5yvfo = (int)-12;
		}
		else
		if (((_bcsi4mx0 == (int)0) & (_1s3eymp4 < (int)1)) | ((_bcsi4mx0 > (int)0) & (_1s3eymp4 < ILNumerics.F2NET.Intrinsics.MAX((int)1 ,_dxpq0xkr ))))
		{
			
			_gro5yvfo = (int)-14;
		}
		
		if (_gro5yvfo != (int)0)
		{
			
			_ut9qalzx("SLASDQ" ,ref Unsafe.AsRef(-(_gro5yvfo)) );
			return;
		}
		
		if (_dxpq0xkr == (int)0)
		return;//* 
		//*     ROTATE is true if any singular vectors desired, false otherwise 
		//* 
		
		_77utirhk = (((_0v6rtqiq > (int)0) | (_tg62ial7 > (int)0)) | (_bcsi4mx0 > (int)0));
		_ykgzqw5a = (_dxpq0xkr + (int)1);
		_v0c3oyw5 = _9qyq7j3e;//* 
		//*     If matrix non-square upper bidiagonal, rotate to be lower 
		//*     bidiagonal.  The rotations are on the right. 
		//* 
		
		if ((_ql1ymlhy == (int)1) & (_v0c3oyw5 == (int)1))
		{
			
			{
				System.Int32 __81fgg2dlsvn644 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step644 = (System.Int32)((int)1);
				System.Int32 __81fgg2count644;
				for (__81fgg2count644 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn644 + __81fgg2step644) / __81fgg2step644)), _b5p6od9s = __81fgg2dlsvn644; __81fgg2count644 != 0; __81fgg2count644--, _b5p6od9s += (__81fgg2step644)) {

				{
					
					_uf57gsrz(ref Unsafe.AsRef(*(_plfm7z8g+(_b5p6od9s - 1))) ,ref Unsafe.AsRef(*(_864fslqq+(_b5p6od9s - 1))) ,ref _82tpdhyl ,ref _8tmd0ner ,ref _q2vwp05i );
					*(_plfm7z8g+(_b5p6od9s - 1)) = _q2vwp05i;
					*(_864fslqq+(_b5p6od9s - 1)) = (_8tmd0ner * *(_plfm7z8g+(_b5p6od9s + (int)1 - 1)));
					*(_plfm7z8g+(_b5p6od9s + (int)1 - 1)) = (_82tpdhyl * *(_plfm7z8g+(_b5p6od9s + (int)1 - 1)));
					if (_77utirhk)
					{
						
						*(_apig8meb+(_b5p6od9s - 1)) = _82tpdhyl;
						*(_apig8meb+(_dxpq0xkr + _b5p6od9s - 1)) = _8tmd0ner;
					}
					
Mark10:;
					// continue
				}
								}			}
			_uf57gsrz(ref Unsafe.AsRef(*(_plfm7z8g+(_dxpq0xkr - 1))) ,ref Unsafe.AsRef(*(_864fslqq+(_dxpq0xkr - 1))) ,ref _82tpdhyl ,ref _8tmd0ner ,ref _q2vwp05i );
			*(_plfm7z8g+(_dxpq0xkr - 1)) = _q2vwp05i;
			*(_864fslqq+(_dxpq0xkr - 1)) = _d0547bi2;
			if (_77utirhk)
			{
				
				*(_apig8meb+(_dxpq0xkr - 1)) = _82tpdhyl;
				*(_apig8meb+(_dxpq0xkr + _dxpq0xkr - 1)) = _8tmd0ner;
			}
			
			_ql1ymlhy = (int)2;
			_v0c3oyw5 = (int)0;//* 
			//*        Update singular vectors if desired. 
			//* 
			
			if (_0v6rtqiq > (int)0)
			_2u3ycobg("L" ,"V" ,"F" ,ref _ykgzqw5a ,ref _0v6rtqiq ,(_apig8meb+((int)1 - 1)),(_apig8meb+(_ykgzqw5a - 1)),_xdbczr8u ,ref _h4ibbatv );
		}
		//* 
		//*     If matrix lower bidiagonal, rotate to be upper bidiagonal 
		//*     by applying Givens rotations on the left. 
		//* 
		
		if (_ql1ymlhy == (int)2)
		{
			
			{
				System.Int32 __81fgg2dlsvn645 = (System.Int32)((int)1);
				const System.Int32 __81fgg2step645 = (System.Int32)((int)1);
				System.Int32 __81fgg2count645;
				for (__81fgg2count645 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr - (int)1) - __81fgg2dlsvn645 + __81fgg2step645) / __81fgg2step645)), _b5p6od9s = __81fgg2dlsvn645; __81fgg2count645 != 0; __81fgg2count645--, _b5p6od9s += (__81fgg2step645)) {

				{
					
					_uf57gsrz(ref Unsafe.AsRef(*(_plfm7z8g+(_b5p6od9s - 1))) ,ref Unsafe.AsRef(*(_864fslqq+(_b5p6od9s - 1))) ,ref _82tpdhyl ,ref _8tmd0ner ,ref _q2vwp05i );
					*(_plfm7z8g+(_b5p6od9s - 1)) = _q2vwp05i;
					*(_864fslqq+(_b5p6od9s - 1)) = (_8tmd0ner * *(_plfm7z8g+(_b5p6od9s + (int)1 - 1)));
					*(_plfm7z8g+(_b5p6od9s + (int)1 - 1)) = (_82tpdhyl * *(_plfm7z8g+(_b5p6od9s + (int)1 - 1)));
					if (_77utirhk)
					{
						
						*(_apig8meb+(_b5p6od9s - 1)) = _82tpdhyl;
						*(_apig8meb+(_dxpq0xkr + _b5p6od9s - 1)) = _8tmd0ner;
					}
					
Mark20:;
					// continue
				}
								}			}//* 
			//*        If matrix (N+1)-by-N lower bidiagonal, one additional 
			//*        rotation is needed. 
			//* 
			
			if (_v0c3oyw5 == (int)1)
			{
				
				_uf57gsrz(ref Unsafe.AsRef(*(_plfm7z8g+(_dxpq0xkr - 1))) ,ref Unsafe.AsRef(*(_864fslqq+(_dxpq0xkr - 1))) ,ref _82tpdhyl ,ref _8tmd0ner ,ref _q2vwp05i );
				*(_plfm7z8g+(_dxpq0xkr - 1)) = _q2vwp05i;
				if (_77utirhk)
				{
					
					*(_apig8meb+(_dxpq0xkr - 1)) = _82tpdhyl;
					*(_apig8meb+(_dxpq0xkr + _dxpq0xkr - 1)) = _8tmd0ner;
				}
				
			}
			//* 
			//*        Update singular vectors if desired. 
			//* 
			
			if (_tg62ial7 > (int)0)
			{
				
				if (_v0c3oyw5 == (int)0)
				{
					
					_2u3ycobg("R" ,"V" ,"F" ,ref _tg62ial7 ,ref _dxpq0xkr ,(_apig8meb+((int)1 - 1)),(_apig8meb+(_ykgzqw5a - 1)),_7u55mqkq ,ref _u6e6d39b );
				}
				else
				{
					
					_2u3ycobg("R" ,"V" ,"F" ,ref _tg62ial7 ,ref _ykgzqw5a ,(_apig8meb+((int)1 - 1)),(_apig8meb+(_ykgzqw5a - 1)),_7u55mqkq ,ref _u6e6d39b );
				}
				
			}
			
			if (_bcsi4mx0 > (int)0)
			{
				
				if (_v0c3oyw5 == (int)0)
				{
					
					_2u3ycobg("L" ,"V" ,"F" ,ref _dxpq0xkr ,ref _bcsi4mx0 ,(_apig8meb+((int)1 - 1)),(_apig8meb+(_ykgzqw5a - 1)),_3crf0qn3 ,ref _1s3eymp4 );
				}
				else
				{
					
					_2u3ycobg("L" ,"V" ,"F" ,ref _ykgzqw5a ,ref _bcsi4mx0 ,(_apig8meb+((int)1 - 1)),(_apig8meb+(_ykgzqw5a - 1)),_3crf0qn3 ,ref _1s3eymp4 );
				}
				
			}
			
		}
		//* 
		//*     Call SBDSQR to compute the SVD of the reduced real 
		//*     N-by-N upper bidiagonal matrix. 
		//* 
		
		_l1b220bb("U" ,ref _dxpq0xkr ,ref _0v6rtqiq ,ref _tg62ial7 ,ref _bcsi4mx0 ,_plfm7z8g ,_864fslqq ,_xdbczr8u ,ref _h4ibbatv ,_7u55mqkq ,ref _u6e6d39b ,_3crf0qn3 ,ref _1s3eymp4 ,_apig8meb ,ref _gro5yvfo );//* 
		//*     Sort the singular values into ascending order (insertion sort on 
		//*     singular values, but only one transposition per singular vector) 
		//* 
		
		{
			System.Int32 __81fgg2dlsvn646 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step646 = (System.Int32)((int)1);
			System.Int32 __81fgg2count646;
			for (__81fgg2count646 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn646 + __81fgg2step646) / __81fgg2step646)), _b5p6od9s = __81fgg2dlsvn646; __81fgg2count646 != 0; __81fgg2count646--, _b5p6od9s += (__81fgg2step646)) {

			{
				//* 
				//*        Scan for smallest D(I). 
				//* 
				
				_ndl0tuhx = _b5p6od9s;
				_rhnpgpoi = *(_plfm7z8g+(_b5p6od9s - 1));
				{
					System.Int32 __81fgg2dlsvn647 = (System.Int32)((_b5p6od9s + (int)1));
					const System.Int32 __81fgg2step647 = (System.Int32)((int)1);
					System.Int32 __81fgg2count647;
					for (__81fgg2count647 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn647 + __81fgg2step647) / __81fgg2step647)), _znpjgsef = __81fgg2dlsvn647; __81fgg2count647 != 0; __81fgg2count647--, _znpjgsef += (__81fgg2step647)) {

					{
						
						if (*(_plfm7z8g+(_znpjgsef - 1)) < _rhnpgpoi)
						{
							
							_ndl0tuhx = _znpjgsef;
							_rhnpgpoi = *(_plfm7z8g+(_znpjgsef - 1));
						}
						
Mark30:;
						// continue
					}
										}				}
				if (_ndl0tuhx != _b5p6od9s)
				{
					//* 
					//*           Swap singular values and vectors. 
					//* 
					
					*(_plfm7z8g+(_ndl0tuhx - 1)) = *(_plfm7z8g+(_b5p6od9s - 1));
					*(_plfm7z8g+(_b5p6od9s - 1)) = _rhnpgpoi;
					if (_0v6rtqiq > (int)0)
					_ahhuglvd(ref _0v6rtqiq ,(_xdbczr8u+(_ndl0tuhx - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv ,(_xdbczr8u+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_h4ibbatv)),ref _h4ibbatv );
					if (_tg62ial7 > (int)0)
					_ahhuglvd(ref _tg62ial7 ,(_7u55mqkq+((int)1 - 1) + (_ndl0tuhx - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef((int)1) ,(_7u55mqkq+((int)1 - 1) + (_b5p6od9s - 1) * 1 * (_u6e6d39b)),ref Unsafe.AsRef((int)1) );
					if (_bcsi4mx0 > (int)0)
					_ahhuglvd(ref _bcsi4mx0 ,(_3crf0qn3+(_ndl0tuhx - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 ,(_3crf0qn3+(_b5p6od9s - 1) + ((int)1 - 1) * 1 * (_1s3eymp4)),ref _1s3eymp4 );
				}
				
Mark40:;
				// continue
			}
						}		}//* 
		
		return;//* 
		//*     End of SLASDQ 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
