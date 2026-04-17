
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
//*> \brief \b DLAQTR solves a real quasi-triangular system of equations, or a complex quasi-triangular system of special form, in real arithmetic. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download DLAQTR + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/dlaqtr.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/dlaqtr.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/dlaqtr.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE DLAQTR( LTRAN, LREAL, N, T, LDT, B, W, SCALE, X, WORK, 
//*                          INFO ) 
//* 
//*       .. Scalar Arguments .. 
//*       LOGICAL            LREAL, LTRAN 
//*       INTEGER            INFO, LDT, N 
//*       DOUBLE PRECISION   SCALE, W 
//*       .. 
//*       .. Array Arguments .. 
//*       DOUBLE PRECISION   B( * ), T( LDT, * ), WORK( * ), X( * ) 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> DLAQTR solves the real quasi-triangular system 
//*> 
//*>              op(T)*p = scale*c,               if LREAL = .TRUE. 
//*> 
//*> or the complex quasi-triangular systems 
//*> 
//*>            op(T + iB)*(p+iq) = scale*(c+id),  if LREAL = .FALSE. 
//*> 
//*> in real arithmetic, where T is upper quasi-triangular. 
//*> If LREAL = .FALSE., then the first diagonal block of T must be 
//*> 1 by 1, B is the specially structured matrix 
//*> 
//*>                B = [ b(1) b(2) ... b(n) ] 
//*>                    [       w            ] 
//*>                    [           w        ] 
//*>                    [              .     ] 
//*>                    [                 w  ] 
//*> 
//*> op(A) = A or A**T, A**T denotes the transpose of 
//*> matrix A. 
//*> 
//*> On input, X = [ c ].  On output, X = [ p ]. 
//*>               [ d ]                  [ q ] 
//*> 
//*> This subroutine is designed for the condition number estimation 
//*> in routine DTRSNA. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in] LTRAN 
//*> \verbatim 
//*>          LTRAN is LOGICAL 
//*>          On entry, LTRAN specifies the option of conjugate transpose: 
//*>             = .FALSE.,    op(T+i*B) = T+i*B, 
//*>             = .TRUE.,     op(T+i*B) = (T+i*B)**T. 
//*> \endverbatim 
//*> 
//*> \param[in] LREAL 
//*> \verbatim 
//*>          LREAL is LOGICAL 
//*>          On entry, LREAL specifies the input matrix structure: 
//*>             = .FALSE.,    the input is complex 
//*>             = .TRUE.,     the input is real 
//*> \endverbatim 
//*> 
//*> \param[in] N 
//*> \verbatim 
//*>          N is INTEGER 
//*>          On entry, N specifies the order of T+i*B. N >= 0. 
//*> \endverbatim 
//*> 
//*> \param[in] T 
//*> \verbatim 
//*>          T is DOUBLE PRECISION array, dimension (LDT,N) 
//*>          On entry, T contains a matrix in Schur canonical form. 
//*>          If LREAL = .FALSE., then the first diagonal block of T mu 
//*>          be 1 by 1. 
//*> \endverbatim 
//*> 
//*> \param[in] LDT 
//*> \verbatim 
//*>          LDT is INTEGER 
//*>          The leading dimension of the matrix T. LDT >= max(1,N). 
//*> \endverbatim 
//*> 
//*> \param[in] B 
//*> \verbatim 
//*>          B is DOUBLE PRECISION array, dimension (N) 
//*>          On entry, B contains the elements to form the matrix 
//*>          B as described above. 
//*>          If LREAL = .TRUE., B is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[in] W 
//*> \verbatim 
//*>          W is DOUBLE PRECISION 
//*>          On entry, W is the diagonal element of the matrix B. 
//*>          If LREAL = .TRUE., W is not referenced. 
//*> \endverbatim 
//*> 
//*> \param[out] SCALE 
//*> \verbatim 
//*>          SCALE is DOUBLE PRECISION 
//*>          On exit, SCALE is the scale factor. 
//*> \endverbatim 
//*> 
//*> \param[in,out] X 
//*> \verbatim 
//*>          X is DOUBLE PRECISION array, dimension (2*N) 
//*>          On entry, X contains the right hand side of the system. 
//*>          On exit, X is overwritten by the solution. 
//*> \endverbatim 
//*> 
//*> \param[out] WORK 
//*> \verbatim 
//*>          WORK is DOUBLE PRECISION array, dimension (N) 
//*> \endverbatim 
//*> 
//*> \param[out] INFO 
//*> \verbatim 
//*>          INFO is INTEGER 
//*>          On exit, INFO is set to 
//*>             0: successful exit. 
//*>               1: the some diagonal 1 by 1 block has been perturbed by 
//*>                  a small number SMIN to keep nonsingularity. 
//*>               2: the some diagonal 2 by 2 block has been perturbed by 
//*>                  a small number in DLALN2 to keep nonsingularity. 
//*>          NOTE: In the interests of speed, this routine does not 
//*>                check the inputs for errors. 
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
//*> \date December 2016 
//* 
//*> \ingroup doubleOTHERauxiliary 
//* 
//*  ===================================================================== 

	 
	public static void _xznyevzr(ref Boolean _zxu29zrp, ref Boolean _jf4tq5xv, ref Int32 _dxpq0xkr, Double* _2ivtt43r, ref Int32 _w8yhbr2r, Double* _p9n405a5, ref Double _z1ioc3c8, ref Double _1m44vtuk, Double* _ta7zuy9k, Double* _apig8meb, ref Int32 _gro5yvfo)
	{
#region variable declarations
MemoryHandle lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = null;

try {
lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui = ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).New<byte>((ulong)(((int)64 + (int)0)));
byte* lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui = (byte*)lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui.Pointer;
Double _d0547bi2 =  0d;
Double _kxg5drh2 =  1d;
Boolean _2bzw4gjb =  default;
Int32 _b5p6od9s =  default;
Int32 _bhsiylw4 =  default;
Int32 _znpjgsef =  default;
Int32 _dk3nh7xl =  default;
Int32 _psodi8to =  default;
Int32 _iafuh0ex =  default;
Int32 _umlkckdg =  default;
Int32 _4o1bt8b1 =  default;
Int32 _tixk7d1h =  default;
Double _av7j8yda =  default;
Double _p1iqarg6 =  default;
Double _egqfb6nh =  default;
Double _wb3zr1em =  default;
Double _sgos5dql =  default;
Double _rhnpgpoi =  default;
Double _oosksyn4 =  default;
Double _bogm0gwy =  default;
Double _o2dp5e8w =  default;
Double _c8zglj2w =  default;
Double _2qcyvkhx =  default;
Double _rtkm3mk9 =  default;
Double _u2jk5kqa =  default;
Double _ziu6urj2 =  default;
Double _7e60fcso =  default;
Double* _plfm7z8g =  (Double*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Double) * ((int)2)*((int)2);
Double* _ycxba85s =  (Double*)lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui; lMemoryCounter4892347812399457sjkdflwerls9123lkuahleui += sizeof(Double) * ((int)2)*((int)2);
string fLanavab = default;
#endregion  variable declarations

	{
		//* 
		//*  -- LAPACK auxiliary routine (version 3.7.0) -- 
		//*  -- LAPACK is a software package provided by Univ. of Tennessee,    -- 
		//*  -- Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd..-- 
		//*     December 2016 
		//* 
		//*     .. Scalar Arguments .. 
		//*     .. 
		//*     .. Array Arguments .. 
		//*     .. 
		//* 
		//* ===================================================================== 
		//* 
		//*     .. Parameters .. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. Local Arrays .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. External Subroutines .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		//*     Do not test the input parameters for errors 
		//* 
		
		_2bzw4gjb = (!(_zxu29zrp));
		_gro5yvfo = (int)0;//* 
		//*     Quick return if possible 
		//* 
		
		if (_dxpq0xkr == (int)0)
		return;//* 
		//*     Set constants to control overflow 
		//* 
		
		_p1iqarg6 = _f43eg0w0("P" );
		_bogm0gwy = (_f43eg0w0("S" ) / _p1iqarg6);
		_av7j8yda = (_kxg5drh2 / _bogm0gwy);//* 
		
		_ziu6urj2 = _oui78ayq("M" ,ref _dxpq0xkr ,ref _dxpq0xkr ,_2ivtt43r ,ref _w8yhbr2r ,_plfm7z8g );
		if (!(_jf4tq5xv))
		_ziu6urj2 = ILNumerics.F2NET.Intrinsics.MAX(_ziu6urj2 ,ILNumerics.F2NET.Intrinsics.ABS(_z1ioc3c8 ) ,_oui78ayq("M" ,ref _dxpq0xkr ,ref Unsafe.AsRef((int)1) ,_p9n405a5 ,ref _dxpq0xkr ,_plfm7z8g ) );
		_rhnpgpoi = ILNumerics.F2NET.Intrinsics.MAX(_bogm0gwy ,_p1iqarg6 * _ziu6urj2 );//* 
		//*     Compute 1-norm of each column of strictly upper triangular 
		//*     part of T to control overflow in triangular solver. 
		//* 
		
		*(_apig8meb+((int)1 - 1)) = _d0547bi2;
		{
			System.Int32 __81fgg2dlsvn2331 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step2331 = (System.Int32)((int)1);
			System.Int32 __81fgg2count2331;
			for (__81fgg2count2331 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2331 + __81fgg2step2331) / __81fgg2step2331)), _znpjgsef = __81fgg2dlsvn2331; __81fgg2count2331 != 0; __81fgg2count2331--, _znpjgsef += (__81fgg2step2331)) {

			{
				
				*(_apig8meb+(_znpjgsef - 1)) = _seesn96r(ref Unsafe.AsRef(_znpjgsef - (int)1) ,(_2ivtt43r+((int)1 - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) );
Mark10:;
				// continue
			}
						}		}//* 
		
		if (!(_jf4tq5xv))
		{
			
			{
				System.Int32 __81fgg2dlsvn2332 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step2332 = (System.Int32)((int)1);
				System.Int32 __81fgg2count2332;
				for (__81fgg2count2332 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2332 + __81fgg2step2332) / __81fgg2step2332)), _b5p6od9s = __81fgg2dlsvn2332; __81fgg2count2332 != 0; __81fgg2count2332--, _b5p6od9s += (__81fgg2step2332)) {

				{
					
					*(_apig8meb+(_b5p6od9s - 1)) = (*(_apig8meb+(_b5p6od9s - 1)) + ILNumerics.F2NET.Intrinsics.ABS(*(_p9n405a5+(_b5p6od9s - 1)) ));
Mark20:;
					// continue
				}
								}			}
		}
		//* 
		
		_tixk7d1h = ((int)2 * _dxpq0xkr);
		_4o1bt8b1 = _dxpq0xkr;
		if (!(_jf4tq5xv))
		_4o1bt8b1 = _tixk7d1h;
		_umlkckdg = _ei7om7ok(ref _4o1bt8b1 ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
		_u2jk5kqa = ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_umlkckdg - 1)) );
		_1m44vtuk = _kxg5drh2;//* 
		
		if (_u2jk5kqa > _av7j8yda)
		{
			
			_1m44vtuk = (_av7j8yda / _u2jk5kqa);
			_f6jqcjk1(ref _4o1bt8b1 ,ref _1m44vtuk ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
			_u2jk5kqa = _av7j8yda;
		}
		//* 
		
		if (_jf4tq5xv)
		{
			//* 
			
			if (_2bzw4gjb)
			{
				//* 
				//*           Solve T*p = scale*c 
				//* 
				
				_iafuh0ex = _dxpq0xkr;
				{
					System.Int32 __81fgg2dlsvn2333 = (System.Int32)(_dxpq0xkr);
					System.Int32 __81fgg2step2333 = (System.Int32)((int)-1);
					System.Int32 __81fgg2count2333;
					for (__81fgg2count2333 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn2333 + __81fgg2step2333) / __81fgg2step2333)), _znpjgsef = __81fgg2dlsvn2333; __81fgg2count2333 != 0; __81fgg2count2333--, _znpjgsef += (__81fgg2step2333)) {

					{
						
						if (_znpjgsef > _iafuh0ex)goto Mark30;
						_dk3nh7xl = _znpjgsef;
						_psodi8to = _znpjgsef;
						_iafuh0ex = (_znpjgsef - (int)1);
						if (_znpjgsef > (int)1)
						{
							
							if (*(_2ivtt43r+(_znpjgsef - 1) + (_znpjgsef - (int)1 - 1) * 1 * (_w8yhbr2r)) != _d0547bi2)
							{
								
								_dk3nh7xl = (_znpjgsef - (int)1);
								_iafuh0ex = (_znpjgsef - (int)2);
							}
							
						}
						//* 
						
						if (_dk3nh7xl == _psodi8to)
						{
							//* 
							//*                 Meet 1 by 1 diagonal block 
							//* 
							//*                 Scale to avoid overflow when computing 
							//*                     x(j) = b(j)/T(j,j) 
							//* 
							
							_rtkm3mk9 = ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_dk3nh7xl - 1)) );
							_c8zglj2w = ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+(_dk3nh7xl - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)) );
							_2qcyvkhx = *(_2ivtt43r+(_dk3nh7xl - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r));
							if (_c8zglj2w < _rhnpgpoi)
							{
								
								_2qcyvkhx = _rhnpgpoi;
								_c8zglj2w = _rhnpgpoi;
								_gro5yvfo = (int)1;
							}
							//* 
							
							if (_rtkm3mk9 == _d0547bi2)goto Mark30;//* 
							
							if (_c8zglj2w < _kxg5drh2)
							{
								
								if (_rtkm3mk9 > (_av7j8yda * _c8zglj2w))
								{
									
									_egqfb6nh = (_kxg5drh2 / _rtkm3mk9);
									_f6jqcjk1(ref _dxpq0xkr ,ref _egqfb6nh ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
									_1m44vtuk = (_1m44vtuk * _egqfb6nh);
									_u2jk5kqa = (_u2jk5kqa * _egqfb6nh);
								}
								
							}
							
							*(_ta7zuy9k+(_dk3nh7xl - 1)) = (*(_ta7zuy9k+(_dk3nh7xl - 1)) / _2qcyvkhx);
							_rtkm3mk9 = ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_dk3nh7xl - 1)) );//* 
							//*                 Scale x if necessary to avoid overflow when adding a 
							//*                 multiple of column j1 of T. 
							//* 
							
							if (_rtkm3mk9 > _kxg5drh2)
							{
								
								_egqfb6nh = (_kxg5drh2 / _rtkm3mk9);
								if (*(_apig8meb+(_dk3nh7xl - 1)) > ((_av7j8yda - _u2jk5kqa) * _egqfb6nh))
								{
									
									_f6jqcjk1(ref _dxpq0xkr ,ref _egqfb6nh ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
									_1m44vtuk = (_1m44vtuk * _egqfb6nh);
								}
								
							}
							
							if (_dk3nh7xl > (int)1)
							{
								
								_3czdkijd(ref Unsafe.AsRef(_dk3nh7xl - (int)1) ,ref Unsafe.AsRef(-(*(_ta7zuy9k+(_dk3nh7xl - 1)))) ,(_2ivtt43r+((int)1 - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
								_umlkckdg = _ei7om7ok(ref Unsafe.AsRef(_dk3nh7xl - (int)1) ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
								_u2jk5kqa = ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_umlkckdg - 1)) );
							}
							//* 
							
						}
						else
						{
							//* 
							//*                 Meet 2 by 2 diagonal block 
							//* 
							//*                 Call 2 by 2 linear system solve, to take 
							//*                 care of possible overflow by scaling factor. 
							//* 
							
							*(_plfm7z8g+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2)) = *(_ta7zuy9k+(_dk3nh7xl - 1));
							*(_plfm7z8g+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2)) = *(_ta7zuy9k+(_psodi8to - 1));
							_6andobxo(ref Unsafe.AsRef(false) ,ref Unsafe.AsRef((int)2) ,ref Unsafe.AsRef((int)1) ,ref _rhnpgpoi ,ref Unsafe.AsRef(_kxg5drh2) ,(_2ivtt43r+(_dk3nh7xl - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,ref Unsafe.AsRef(_kxg5drh2) ,ref Unsafe.AsRef(_kxg5drh2) ,_plfm7z8g ,ref Unsafe.AsRef((int)2) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,_ycxba85s ,ref Unsafe.AsRef((int)2) ,ref _wb3zr1em ,ref _ziu6urj2 ,ref _bhsiylw4 );
							if (_bhsiylw4 != (int)0)
							_gro5yvfo = (int)2;//* 
							
							if (_wb3zr1em != _kxg5drh2)
							{
								
								_f6jqcjk1(ref _dxpq0xkr ,ref _wb3zr1em ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
								_1m44vtuk = (_1m44vtuk * _wb3zr1em);
							}
							
							*(_ta7zuy9k+(_dk3nh7xl - 1)) = *(_ycxba85s+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2));
							*(_ta7zuy9k+(_psodi8to - 1)) = *(_ycxba85s+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2));//* 
							//*                 Scale V(1,1) (= X(J1)) and/or V(2,1) (=X(J2)) 
							//*                 to avoid overflow in updating right-hand side. 
							//* 
							
							_rtkm3mk9 = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_ycxba85s+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_ycxba85s+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2)) ) );
							if (_rtkm3mk9 > _kxg5drh2)
							{
								
								_egqfb6nh = (_kxg5drh2 / _rtkm3mk9);
								if (ILNumerics.F2NET.Intrinsics.MAX(*(_apig8meb+(_dk3nh7xl - 1)) ,*(_apig8meb+(_psodi8to - 1)) ) > ((_av7j8yda - _u2jk5kqa) * _egqfb6nh))
								{
									
									_f6jqcjk1(ref _dxpq0xkr ,ref _egqfb6nh ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
									_1m44vtuk = (_1m44vtuk * _egqfb6nh);
								}
								
							}
							//* 
							//*                 Update right-hand side 
							//* 
							
							if (_dk3nh7xl > (int)1)
							{
								
								_3czdkijd(ref Unsafe.AsRef(_dk3nh7xl - (int)1) ,ref Unsafe.AsRef(-(*(_ta7zuy9k+(_dk3nh7xl - 1)))) ,(_2ivtt43r+((int)1 - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
								_3czdkijd(ref Unsafe.AsRef(_dk3nh7xl - (int)1) ,ref Unsafe.AsRef(-(*(_ta7zuy9k+(_psodi8to - 1)))) ,(_2ivtt43r+((int)1 - 1) + (_psodi8to - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
								_umlkckdg = _ei7om7ok(ref Unsafe.AsRef(_dk3nh7xl - (int)1) ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
								_u2jk5kqa = ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_umlkckdg - 1)) );
							}
							//* 
							
						}
						//* 
						
Mark30:;
						// continue
					}
										}				}//* 
				
			}
			else
			{
				//* 
				//*           Solve T**T*p = scale*c 
				//* 
				
				_iafuh0ex = (int)1;
				{
					System.Int32 __81fgg2dlsvn2334 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2334 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2334;
					for (__81fgg2count2334 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2334 + __81fgg2step2334) / __81fgg2step2334)), _znpjgsef = __81fgg2dlsvn2334; __81fgg2count2334 != 0; __81fgg2count2334--, _znpjgsef += (__81fgg2step2334)) {

					{
						
						if (_znpjgsef < _iafuh0ex)goto Mark40;
						_dk3nh7xl = _znpjgsef;
						_psodi8to = _znpjgsef;
						_iafuh0ex = (_znpjgsef + (int)1);
						if (_znpjgsef < _dxpq0xkr)
						{
							
							if (*(_2ivtt43r+(_znpjgsef + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)) != _d0547bi2)
							{
								
								_psodi8to = (_znpjgsef + (int)1);
								_iafuh0ex = (_znpjgsef + (int)2);
							}
							
						}
						//* 
						
						if (_dk3nh7xl == _psodi8to)
						{
							//* 
							//*                 1 by 1 diagonal block 
							//* 
							//*                 Scale if necessary to avoid overflow in forming the 
							//*                 right-hand side element by inner product. 
							//* 
							
							_rtkm3mk9 = ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_dk3nh7xl - 1)) );
							if (_u2jk5kqa > _kxg5drh2)
							{
								
								_egqfb6nh = (_kxg5drh2 / _u2jk5kqa);
								if (*(_apig8meb+(_dk3nh7xl - 1)) > ((_av7j8yda - _rtkm3mk9) * _egqfb6nh))
								{
									
									_f6jqcjk1(ref _dxpq0xkr ,ref _egqfb6nh ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
									_1m44vtuk = (_1m44vtuk * _egqfb6nh);
									_u2jk5kqa = (_u2jk5kqa * _egqfb6nh);
								}
								
							}
							//* 
							
							*(_ta7zuy9k+(_dk3nh7xl - 1)) = (*(_ta7zuy9k+(_dk3nh7xl - 1)) - _le984b8z(ref Unsafe.AsRef(_dk3nh7xl - (int)1) ,(_2ivtt43r+((int)1 - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) ));//* 
							
							_rtkm3mk9 = ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_dk3nh7xl - 1)) );
							_c8zglj2w = ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+(_dk3nh7xl - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)) );
							_2qcyvkhx = *(_2ivtt43r+(_dk3nh7xl - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r));
							if (_c8zglj2w < _rhnpgpoi)
							{
								
								_2qcyvkhx = _rhnpgpoi;
								_c8zglj2w = _rhnpgpoi;
								_gro5yvfo = (int)1;
							}
							//* 
							
							if (_c8zglj2w < _kxg5drh2)
							{
								
								if (_rtkm3mk9 > (_av7j8yda * _c8zglj2w))
								{
									
									_egqfb6nh = (_kxg5drh2 / _rtkm3mk9);
									_f6jqcjk1(ref _dxpq0xkr ,ref _egqfb6nh ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
									_1m44vtuk = (_1m44vtuk * _egqfb6nh);
									_u2jk5kqa = (_u2jk5kqa * _egqfb6nh);
								}
								
							}
							
							*(_ta7zuy9k+(_dk3nh7xl - 1)) = (*(_ta7zuy9k+(_dk3nh7xl - 1)) / _2qcyvkhx);
							_u2jk5kqa = ILNumerics.F2NET.Intrinsics.MAX(_u2jk5kqa ,ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_dk3nh7xl - 1)) ) );//* 
							
						}
						else
						{
							//* 
							//*                 2 by 2 diagonal block 
							//* 
							//*                 Scale if necessary to avoid overflow in forming the 
							//*                 right-hand side elements by inner product. 
							//* 
							
							_rtkm3mk9 = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_dk3nh7xl - 1)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_psodi8to - 1)) ) );
							if (_u2jk5kqa > _kxg5drh2)
							{
								
								_egqfb6nh = (_kxg5drh2 / _u2jk5kqa);
								if (ILNumerics.F2NET.Intrinsics.MAX(*(_apig8meb+(_psodi8to - 1)) ,*(_apig8meb+(_dk3nh7xl - 1)) ) > ((_av7j8yda - _rtkm3mk9) * _egqfb6nh))
								{
									
									_f6jqcjk1(ref _dxpq0xkr ,ref _egqfb6nh ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
									_1m44vtuk = (_1m44vtuk * _egqfb6nh);
									_u2jk5kqa = (_u2jk5kqa * _egqfb6nh);
								}
								
							}
							//* 
							
							*(_plfm7z8g+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2)) = (*(_ta7zuy9k+(_dk3nh7xl - 1)) - _le984b8z(ref Unsafe.AsRef(_dk3nh7xl - (int)1) ,(_2ivtt43r+((int)1 - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) ));
							*(_plfm7z8g+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2)) = (*(_ta7zuy9k+(_psodi8to - 1)) - _le984b8z(ref Unsafe.AsRef(_dk3nh7xl - (int)1) ,(_2ivtt43r+((int)1 - 1) + (_psodi8to - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) ));//* 
							
							_6andobxo(ref Unsafe.AsRef(true) ,ref Unsafe.AsRef((int)2) ,ref Unsafe.AsRef((int)1) ,ref _rhnpgpoi ,ref Unsafe.AsRef(_kxg5drh2) ,(_2ivtt43r+(_dk3nh7xl - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,ref Unsafe.AsRef(_kxg5drh2) ,ref Unsafe.AsRef(_kxg5drh2) ,_plfm7z8g ,ref Unsafe.AsRef((int)2) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(_d0547bi2) ,_ycxba85s ,ref Unsafe.AsRef((int)2) ,ref _wb3zr1em ,ref _ziu6urj2 ,ref _bhsiylw4 );
							if (_bhsiylw4 != (int)0)
							_gro5yvfo = (int)2;//* 
							
							if (_wb3zr1em != _kxg5drh2)
							{
								
								_f6jqcjk1(ref _dxpq0xkr ,ref _wb3zr1em ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
								_1m44vtuk = (_1m44vtuk * _wb3zr1em);
							}
							
							*(_ta7zuy9k+(_dk3nh7xl - 1)) = *(_ycxba85s+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2));
							*(_ta7zuy9k+(_psodi8to - 1)) = *(_ycxba85s+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2));
							_u2jk5kqa = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_dk3nh7xl - 1)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_psodi8to - 1)) ) ,_u2jk5kqa );//* 
							
						}
						
Mark40:;
						// continue
					}
										}				}
			}
			//* 
			
		}
		else
		{
			//* 
			
			_oosksyn4 = ILNumerics.F2NET.Intrinsics.MAX(_p1iqarg6 * ILNumerics.F2NET.Intrinsics.ABS(_z1ioc3c8 ) ,_rhnpgpoi );
			if (_2bzw4gjb)
			{
				//* 
				//*           Solve (T + iB)*(p+iq) = c+id 
				//* 
				
				_iafuh0ex = _dxpq0xkr;
				{
					System.Int32 __81fgg2dlsvn2335 = (System.Int32)(_dxpq0xkr);
					System.Int32 __81fgg2step2335 = (System.Int32)((int)-1);
					System.Int32 __81fgg2count2335;
					for (__81fgg2count2335 = System.Math.Max(0, (System.Int32)(((System.Int32)((int)1) - __81fgg2dlsvn2335 + __81fgg2step2335) / __81fgg2step2335)), _znpjgsef = __81fgg2dlsvn2335; __81fgg2count2335 != 0; __81fgg2count2335--, _znpjgsef += (__81fgg2step2335)) {

					{
						
						if (_znpjgsef > _iafuh0ex)goto Mark70;
						_dk3nh7xl = _znpjgsef;
						_psodi8to = _znpjgsef;
						_iafuh0ex = (_znpjgsef - (int)1);
						if (_znpjgsef > (int)1)
						{
							
							if (*(_2ivtt43r+(_znpjgsef - 1) + (_znpjgsef - (int)1 - 1) * 1 * (_w8yhbr2r)) != _d0547bi2)
							{
								
								_dk3nh7xl = (_znpjgsef - (int)1);
								_iafuh0ex = (_znpjgsef - (int)2);
							}
							
						}
						//* 
						
						if (_dk3nh7xl == _psodi8to)
						{
							//* 
							//*                 1 by 1 diagonal block 
							//* 
							//*                 Scale if necessary to avoid overflow in division 
							//* 
							
							_7e60fcso = _z1ioc3c8;
							if (_dk3nh7xl == (int)1)
							_7e60fcso = *(_p9n405a5+((int)1 - 1));
							_rtkm3mk9 = (ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_dk3nh7xl - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_dxpq0xkr + _dk3nh7xl - 1)) ));
							_c8zglj2w = (ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+(_dk3nh7xl - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)) ) + ILNumerics.F2NET.Intrinsics.ABS(_7e60fcso ));
							_2qcyvkhx = *(_2ivtt43r+(_dk3nh7xl - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r));
							if (_c8zglj2w < _oosksyn4)
							{
								
								_2qcyvkhx = _oosksyn4;
								_c8zglj2w = _oosksyn4;
								_gro5yvfo = (int)1;
							}
							//* 
							
							if (_rtkm3mk9 == _d0547bi2)goto Mark70;//* 
							
							if (_c8zglj2w < _kxg5drh2)
							{
								
								if (_rtkm3mk9 > (_av7j8yda * _c8zglj2w))
								{
									
									_egqfb6nh = (_kxg5drh2 / _rtkm3mk9);
									_f6jqcjk1(ref _tixk7d1h ,ref _egqfb6nh ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
									_1m44vtuk = (_1m44vtuk * _egqfb6nh);
									_u2jk5kqa = (_u2jk5kqa * _egqfb6nh);
								}
								
							}
							
							_x0fujx9g(ref Unsafe.AsRef(*(_ta7zuy9k+(_dk3nh7xl - 1))) ,ref Unsafe.AsRef(*(_ta7zuy9k+(_dxpq0xkr + _dk3nh7xl - 1))) ,ref _2qcyvkhx ,ref _7e60fcso ,ref _o2dp5e8w ,ref _sgos5dql );
							*(_ta7zuy9k+(_dk3nh7xl - 1)) = _o2dp5e8w;
							*(_ta7zuy9k+(_dxpq0xkr + _dk3nh7xl - 1)) = _sgos5dql;
							_rtkm3mk9 = (ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_dk3nh7xl - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_dxpq0xkr + _dk3nh7xl - 1)) ));//* 
							//*                 Scale x if necessary to avoid overflow when adding a 
							//*                 multiple of column j1 of T. 
							//* 
							
							if (_rtkm3mk9 > _kxg5drh2)
							{
								
								_egqfb6nh = (_kxg5drh2 / _rtkm3mk9);
								if (*(_apig8meb+(_dk3nh7xl - 1)) > ((_av7j8yda - _u2jk5kqa) * _egqfb6nh))
								{
									
									_f6jqcjk1(ref _tixk7d1h ,ref _egqfb6nh ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
									_1m44vtuk = (_1m44vtuk * _egqfb6nh);
								}
								
							}
							//* 
							
							if (_dk3nh7xl > (int)1)
							{
								
								_3czdkijd(ref Unsafe.AsRef(_dk3nh7xl - (int)1) ,ref Unsafe.AsRef(-(*(_ta7zuy9k+(_dk3nh7xl - 1)))) ,(_2ivtt43r+((int)1 - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
								_3czdkijd(ref Unsafe.AsRef(_dk3nh7xl - (int)1) ,ref Unsafe.AsRef(-(*(_ta7zuy9k+(_dxpq0xkr + _dk3nh7xl - 1)))) ,(_2ivtt43r+((int)1 - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_ta7zuy9k+(_dxpq0xkr + (int)1 - 1)),ref Unsafe.AsRef((int)1) );//* 
								
								*(_ta7zuy9k+((int)1 - 1)) = (*(_ta7zuy9k+((int)1 - 1)) + (*(_p9n405a5+(_dk3nh7xl - 1)) * *(_ta7zuy9k+(_dxpq0xkr + _dk3nh7xl - 1))));
								*(_ta7zuy9k+(_dxpq0xkr + (int)1 - 1)) = (*(_ta7zuy9k+(_dxpq0xkr + (int)1 - 1)) - (*(_p9n405a5+(_dk3nh7xl - 1)) * *(_ta7zuy9k+(_dk3nh7xl - 1))));//* 
								
								_u2jk5kqa = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn2336 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step2336 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2336;
									for (__81fgg2count2336 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dk3nh7xl - (int)1) - __81fgg2dlsvn2336 + __81fgg2step2336) / __81fgg2step2336)), _umlkckdg = __81fgg2dlsvn2336; __81fgg2count2336 != 0; __81fgg2count2336--, _umlkckdg += (__81fgg2step2336)) {

									{
										
										_u2jk5kqa = ILNumerics.F2NET.Intrinsics.MAX(_u2jk5kqa ,ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_umlkckdg - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_umlkckdg + _dxpq0xkr - 1)) ) );
Mark50:;
										// continue
									}
																		}								}
							}
							//* 
							
						}
						else
						{
							//* 
							//*                 Meet 2 by 2 diagonal block 
							//* 
							
							*(_plfm7z8g+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2)) = *(_ta7zuy9k+(_dk3nh7xl - 1));
							*(_plfm7z8g+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2)) = *(_ta7zuy9k+(_psodi8to - 1));
							*(_plfm7z8g+((int)1 - 1) + ((int)2 - 1) * 1 * ((int)2)) = *(_ta7zuy9k+(_dxpq0xkr + _dk3nh7xl - 1));
							*(_plfm7z8g+((int)2 - 1) + ((int)2 - 1) * 1 * ((int)2)) = *(_ta7zuy9k+(_dxpq0xkr + _psodi8to - 1));
							_6andobxo(ref Unsafe.AsRef(false) ,ref Unsafe.AsRef((int)2) ,ref Unsafe.AsRef((int)2) ,ref _oosksyn4 ,ref Unsafe.AsRef(_kxg5drh2) ,(_2ivtt43r+(_dk3nh7xl - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,ref Unsafe.AsRef(_kxg5drh2) ,ref Unsafe.AsRef(_kxg5drh2) ,_plfm7z8g ,ref Unsafe.AsRef((int)2) ,ref Unsafe.AsRef(_d0547bi2) ,ref Unsafe.AsRef(-(_z1ioc3c8)) ,_ycxba85s ,ref Unsafe.AsRef((int)2) ,ref _wb3zr1em ,ref _ziu6urj2 ,ref _bhsiylw4 );
							if (_bhsiylw4 != (int)0)
							_gro5yvfo = (int)2;//* 
							
							if (_wb3zr1em != _kxg5drh2)
							{
								
								_f6jqcjk1(ref Unsafe.AsRef((int)2 * _dxpq0xkr) ,ref _wb3zr1em ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
								_1m44vtuk = (_wb3zr1em * _1m44vtuk);
							}
							
							*(_ta7zuy9k+(_dk3nh7xl - 1)) = *(_ycxba85s+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2));
							*(_ta7zuy9k+(_psodi8to - 1)) = *(_ycxba85s+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2));
							*(_ta7zuy9k+(_dxpq0xkr + _dk3nh7xl - 1)) = *(_ycxba85s+((int)1 - 1) + ((int)2 - 1) * 1 * ((int)2));
							*(_ta7zuy9k+(_dxpq0xkr + _psodi8to - 1)) = *(_ycxba85s+((int)2 - 1) + ((int)2 - 1) * 1 * ((int)2));//* 
							//*                 Scale X(J1), .... to avoid overflow in 
							//*                 updating right hand side. 
							//* 
							
							_rtkm3mk9 = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_ycxba85s+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_ycxba85s+((int)1 - 1) + ((int)2 - 1) * 1 * ((int)2)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_ycxba85s+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_ycxba85s+((int)2 - 1) + ((int)2 - 1) * 1 * ((int)2)) ) );
							if (_rtkm3mk9 > _kxg5drh2)
							{
								
								_egqfb6nh = (_kxg5drh2 / _rtkm3mk9);
								if (ILNumerics.F2NET.Intrinsics.MAX(*(_apig8meb+(_dk3nh7xl - 1)) ,*(_apig8meb+(_psodi8to - 1)) ) > ((_av7j8yda - _u2jk5kqa) * _egqfb6nh))
								{
									
									_f6jqcjk1(ref _tixk7d1h ,ref _egqfb6nh ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
									_1m44vtuk = (_1m44vtuk * _egqfb6nh);
								}
								
							}
							//* 
							//*                 Update the right-hand side. 
							//* 
							
							if (_dk3nh7xl > (int)1)
							{
								
								_3czdkijd(ref Unsafe.AsRef(_dk3nh7xl - (int)1) ,ref Unsafe.AsRef(-(*(_ta7zuy9k+(_dk3nh7xl - 1)))) ,(_2ivtt43r+((int)1 - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
								_3czdkijd(ref Unsafe.AsRef(_dk3nh7xl - (int)1) ,ref Unsafe.AsRef(-(*(_ta7zuy9k+(_psodi8to - 1)))) ,(_2ivtt43r+((int)1 - 1) + (_psodi8to - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );//* 
								
								_3czdkijd(ref Unsafe.AsRef(_dk3nh7xl - (int)1) ,ref Unsafe.AsRef(-(*(_ta7zuy9k+(_dxpq0xkr + _dk3nh7xl - 1)))) ,(_2ivtt43r+((int)1 - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_ta7zuy9k+(_dxpq0xkr + (int)1 - 1)),ref Unsafe.AsRef((int)1) );
								_3czdkijd(ref Unsafe.AsRef(_dk3nh7xl - (int)1) ,ref Unsafe.AsRef(-(*(_ta7zuy9k+(_dxpq0xkr + _psodi8to - 1)))) ,(_2ivtt43r+((int)1 - 1) + (_psodi8to - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_ta7zuy9k+(_dxpq0xkr + (int)1 - 1)),ref Unsafe.AsRef((int)1) );//* 
								
								*(_ta7zuy9k+((int)1 - 1)) = ((*(_ta7zuy9k+((int)1 - 1)) + (*(_p9n405a5+(_dk3nh7xl - 1)) * *(_ta7zuy9k+(_dxpq0xkr + _dk3nh7xl - 1)))) + (*(_p9n405a5+(_psodi8to - 1)) * *(_ta7zuy9k+(_dxpq0xkr + _psodi8to - 1))));
								*(_ta7zuy9k+(_dxpq0xkr + (int)1 - 1)) = ((*(_ta7zuy9k+(_dxpq0xkr + (int)1 - 1)) - (*(_p9n405a5+(_dk3nh7xl - 1)) * *(_ta7zuy9k+(_dk3nh7xl - 1)))) - (*(_p9n405a5+(_psodi8to - 1)) * *(_ta7zuy9k+(_psodi8to - 1))));//* 
								
								_u2jk5kqa = _d0547bi2;
								{
									System.Int32 __81fgg2dlsvn2337 = (System.Int32)((int)1);
									const System.Int32 __81fgg2step2337 = (System.Int32)((int)1);
									System.Int32 __81fgg2count2337;
									for (__81fgg2count2337 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dk3nh7xl - (int)1) - __81fgg2dlsvn2337 + __81fgg2step2337) / __81fgg2step2337)), _umlkckdg = __81fgg2dlsvn2337; __81fgg2count2337 != 0; __81fgg2count2337--, _umlkckdg += (__81fgg2step2337)) {

									{
										
										_u2jk5kqa = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_umlkckdg - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_umlkckdg + _dxpq0xkr - 1)) ) ,_u2jk5kqa );
Mark60:;
										// continue
									}
																		}								}
							}
							//* 
							
						}
						
Mark70:;
						// continue
					}
										}				}//* 
				
			}
			else
			{
				//* 
				//*           Solve (T + iB)**T*(p+iq) = c+id 
				//* 
				
				_iafuh0ex = (int)1;
				{
					System.Int32 __81fgg2dlsvn2338 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step2338 = (System.Int32)((int)1);
					System.Int32 __81fgg2count2338;
					for (__81fgg2count2338 = System.Math.Max(0, (System.Int32)(((System.Int32)(_dxpq0xkr) - __81fgg2dlsvn2338 + __81fgg2step2338) / __81fgg2step2338)), _znpjgsef = __81fgg2dlsvn2338; __81fgg2count2338 != 0; __81fgg2count2338--, _znpjgsef += (__81fgg2step2338)) {

					{
						
						if (_znpjgsef < _iafuh0ex)goto Mark80;
						_dk3nh7xl = _znpjgsef;
						_psodi8to = _znpjgsef;
						_iafuh0ex = (_znpjgsef + (int)1);
						if (_znpjgsef < _dxpq0xkr)
						{
							
							if (*(_2ivtt43r+(_znpjgsef + (int)1 - 1) + (_znpjgsef - 1) * 1 * (_w8yhbr2r)) != _d0547bi2)
							{
								
								_psodi8to = (_znpjgsef + (int)1);
								_iafuh0ex = (_znpjgsef + (int)2);
							}
							
						}
						//* 
						
						if (_dk3nh7xl == _psodi8to)
						{
							//* 
							//*                 1 by 1 diagonal block 
							//* 
							//*                 Scale if necessary to avoid overflow in forming the 
							//*                 right-hand side element by inner product. 
							//* 
							
							_rtkm3mk9 = (ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_dk3nh7xl - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_dk3nh7xl + _dxpq0xkr - 1)) ));
							if (_u2jk5kqa > _kxg5drh2)
							{
								
								_egqfb6nh = (_kxg5drh2 / _u2jk5kqa);
								if (*(_apig8meb+(_dk3nh7xl - 1)) > ((_av7j8yda - _rtkm3mk9) * _egqfb6nh))
								{
									
									_f6jqcjk1(ref _tixk7d1h ,ref _egqfb6nh ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
									_1m44vtuk = (_1m44vtuk * _egqfb6nh);
									_u2jk5kqa = (_u2jk5kqa * _egqfb6nh);
								}
								
							}
							//* 
							
							*(_ta7zuy9k+(_dk3nh7xl - 1)) = (*(_ta7zuy9k+(_dk3nh7xl - 1)) - _le984b8z(ref Unsafe.AsRef(_dk3nh7xl - (int)1) ,(_2ivtt43r+((int)1 - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) ));
							*(_ta7zuy9k+(_dxpq0xkr + _dk3nh7xl - 1)) = (*(_ta7zuy9k+(_dxpq0xkr + _dk3nh7xl - 1)) - _le984b8z(ref Unsafe.AsRef(_dk3nh7xl - (int)1) ,(_2ivtt43r+((int)1 - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_ta7zuy9k+(_dxpq0xkr + (int)1 - 1)),ref Unsafe.AsRef((int)1) ));
							if (_dk3nh7xl > (int)1)
							{
								
								*(_ta7zuy9k+(_dk3nh7xl - 1)) = (*(_ta7zuy9k+(_dk3nh7xl - 1)) - (*(_p9n405a5+(_dk3nh7xl - 1)) * *(_ta7zuy9k+(_dxpq0xkr + (int)1 - 1))));
								*(_ta7zuy9k+(_dxpq0xkr + _dk3nh7xl - 1)) = (*(_ta7zuy9k+(_dxpq0xkr + _dk3nh7xl - 1)) + (*(_p9n405a5+(_dk3nh7xl - 1)) * *(_ta7zuy9k+((int)1 - 1))));
							}
							
							_rtkm3mk9 = (ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_dk3nh7xl - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_dk3nh7xl + _dxpq0xkr - 1)) ));//* 
							
							_7e60fcso = _z1ioc3c8;
							if (_dk3nh7xl == (int)1)
							_7e60fcso = *(_p9n405a5+((int)1 - 1));//* 
							//*                 Scale if necessary to avoid overflow in 
							//*                 complex division 
							//* 
							
							_c8zglj2w = (ILNumerics.F2NET.Intrinsics.ABS(*(_2ivtt43r+(_dk3nh7xl - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)) ) + ILNumerics.F2NET.Intrinsics.ABS(_7e60fcso ));
							_2qcyvkhx = *(_2ivtt43r+(_dk3nh7xl - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r));
							if (_c8zglj2w < _oosksyn4)
							{
								
								_2qcyvkhx = _oosksyn4;
								_c8zglj2w = _oosksyn4;
								_gro5yvfo = (int)1;
							}
							//* 
							
							if (_c8zglj2w < _kxg5drh2)
							{
								
								if (_rtkm3mk9 > (_av7j8yda * _c8zglj2w))
								{
									
									_egqfb6nh = (_kxg5drh2 / _rtkm3mk9);
									_f6jqcjk1(ref _tixk7d1h ,ref _egqfb6nh ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
									_1m44vtuk = (_1m44vtuk * _egqfb6nh);
									_u2jk5kqa = (_u2jk5kqa * _egqfb6nh);
								}
								
							}
							
							_x0fujx9g(ref Unsafe.AsRef(*(_ta7zuy9k+(_dk3nh7xl - 1))) ,ref Unsafe.AsRef(*(_ta7zuy9k+(_dxpq0xkr + _dk3nh7xl - 1))) ,ref _2qcyvkhx ,ref Unsafe.AsRef(-(_7e60fcso)) ,ref _o2dp5e8w ,ref _sgos5dql );
							*(_ta7zuy9k+(_dk3nh7xl - 1)) = _o2dp5e8w;
							*(_ta7zuy9k+(_dk3nh7xl + _dxpq0xkr - 1)) = _sgos5dql;
							_u2jk5kqa = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_dk3nh7xl - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_dk3nh7xl + _dxpq0xkr - 1)) ) ,_u2jk5kqa );//* 
							
						}
						else
						{
							//* 
							//*                 2 by 2 diagonal block 
							//* 
							//*                 Scale if necessary to avoid overflow in forming the 
							//*                 right-hand side element by inner product. 
							//* 
							
							_rtkm3mk9 = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_dk3nh7xl - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_dxpq0xkr + _dk3nh7xl - 1)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_psodi8to - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_dxpq0xkr + _psodi8to - 1)) ) );
							if (_u2jk5kqa > _kxg5drh2)
							{
								
								_egqfb6nh = (_kxg5drh2 / _u2jk5kqa);
								if (ILNumerics.F2NET.Intrinsics.MAX(*(_apig8meb+(_dk3nh7xl - 1)) ,*(_apig8meb+(_psodi8to - 1)) ) > ((_av7j8yda - _rtkm3mk9) / _u2jk5kqa))
								{
									
									_f6jqcjk1(ref _tixk7d1h ,ref _egqfb6nh ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
									_1m44vtuk = (_1m44vtuk * _egqfb6nh);
									_u2jk5kqa = (_u2jk5kqa * _egqfb6nh);
								}
								
							}
							//* 
							
							*(_plfm7z8g+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2)) = (*(_ta7zuy9k+(_dk3nh7xl - 1)) - _le984b8z(ref Unsafe.AsRef(_dk3nh7xl - (int)1) ,(_2ivtt43r+((int)1 - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) ));
							*(_plfm7z8g+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2)) = (*(_ta7zuy9k+(_psodi8to - 1)) - _le984b8z(ref Unsafe.AsRef(_dk3nh7xl - (int)1) ,(_2ivtt43r+((int)1 - 1) + (_psodi8to - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) ));
							*(_plfm7z8g+((int)1 - 1) + ((int)2 - 1) * 1 * ((int)2)) = (*(_ta7zuy9k+(_dxpq0xkr + _dk3nh7xl - 1)) - _le984b8z(ref Unsafe.AsRef(_dk3nh7xl - (int)1) ,(_2ivtt43r+((int)1 - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_ta7zuy9k+(_dxpq0xkr + (int)1 - 1)),ref Unsafe.AsRef((int)1) ));
							*(_plfm7z8g+((int)2 - 1) + ((int)2 - 1) * 1 * ((int)2)) = (*(_ta7zuy9k+(_dxpq0xkr + _psodi8to - 1)) - _le984b8z(ref Unsafe.AsRef(_dk3nh7xl - (int)1) ,(_2ivtt43r+((int)1 - 1) + (_psodi8to - 1) * 1 * (_w8yhbr2r)),ref Unsafe.AsRef((int)1) ,(_ta7zuy9k+(_dxpq0xkr + (int)1 - 1)),ref Unsafe.AsRef((int)1) ));
							*(_plfm7z8g+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2)) = (*(_plfm7z8g+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2)) - (*(_p9n405a5+(_dk3nh7xl - 1)) * *(_ta7zuy9k+(_dxpq0xkr + (int)1 - 1))));
							*(_plfm7z8g+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2)) = (*(_plfm7z8g+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2)) - (*(_p9n405a5+(_psodi8to - 1)) * *(_ta7zuy9k+(_dxpq0xkr + (int)1 - 1))));
							*(_plfm7z8g+((int)1 - 1) + ((int)2 - 1) * 1 * ((int)2)) = (*(_plfm7z8g+((int)1 - 1) + ((int)2 - 1) * 1 * ((int)2)) + (*(_p9n405a5+(_dk3nh7xl - 1)) * *(_ta7zuy9k+((int)1 - 1))));
							*(_plfm7z8g+((int)2 - 1) + ((int)2 - 1) * 1 * ((int)2)) = (*(_plfm7z8g+((int)2 - 1) + ((int)2 - 1) * 1 * ((int)2)) + (*(_p9n405a5+(_psodi8to - 1)) * *(_ta7zuy9k+((int)1 - 1))));//* 
							
							_6andobxo(ref Unsafe.AsRef(true) ,ref Unsafe.AsRef((int)2) ,ref Unsafe.AsRef((int)2) ,ref _oosksyn4 ,ref Unsafe.AsRef(_kxg5drh2) ,(_2ivtt43r+(_dk3nh7xl - 1) + (_dk3nh7xl - 1) * 1 * (_w8yhbr2r)),ref _w8yhbr2r ,ref Unsafe.AsRef(_kxg5drh2) ,ref Unsafe.AsRef(_kxg5drh2) ,_plfm7z8g ,ref Unsafe.AsRef((int)2) ,ref Unsafe.AsRef(_d0547bi2) ,ref _z1ioc3c8 ,_ycxba85s ,ref Unsafe.AsRef((int)2) ,ref _wb3zr1em ,ref _ziu6urj2 ,ref _bhsiylw4 );
							if (_bhsiylw4 != (int)0)
							_gro5yvfo = (int)2;//* 
							
							if (_wb3zr1em != _kxg5drh2)
							{
								
								_f6jqcjk1(ref _tixk7d1h ,ref _wb3zr1em ,_ta7zuy9k ,ref Unsafe.AsRef((int)1) );
								_1m44vtuk = (_wb3zr1em * _1m44vtuk);
							}
							
							*(_ta7zuy9k+(_dk3nh7xl - 1)) = *(_ycxba85s+((int)1 - 1) + ((int)1 - 1) * 1 * ((int)2));
							*(_ta7zuy9k+(_psodi8to - 1)) = *(_ycxba85s+((int)2 - 1) + ((int)1 - 1) * 1 * ((int)2));
							*(_ta7zuy9k+(_dxpq0xkr + _dk3nh7xl - 1)) = *(_ycxba85s+((int)1 - 1) + ((int)2 - 1) * 1 * ((int)2));
							*(_ta7zuy9k+(_dxpq0xkr + _psodi8to - 1)) = *(_ycxba85s+((int)2 - 1) + ((int)2 - 1) * 1 * ((int)2));
							_u2jk5kqa = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_dk3nh7xl - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_dxpq0xkr + _dk3nh7xl - 1)) ) ,ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_psodi8to - 1)) ) + ILNumerics.F2NET.Intrinsics.ABS(*(_ta7zuy9k+(_dxpq0xkr + _psodi8to - 1)) ) ,_u2jk5kqa );//* 
							
						}
						//* 
						
Mark80:;
						// continue
					}
										}				}//* 
				
			}
			//* 
			
		}
		//* 
		
		return;//* 
		//*     End of DLAQTR 
		//* 
		
	}
	
	} finally { 
if (lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui != null) ILNumerics.Core.DeviceManagement.DeviceManager.GetDevice(0).MemoryPool.Free(lMemoryHandle4892347812399457sjkdflwerls9123lkuahleui); 
}
	} // 177

} // end class 
} // end namespace
#endif
