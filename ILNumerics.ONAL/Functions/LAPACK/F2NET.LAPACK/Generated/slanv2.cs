
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
//*> \brief \b SLANV2 computes the Schur factorization of a real 2-by-2 nonsymmetric matrix in standard form. 
//* 
//*  =========== DOCUMENTATION =========== 
//* 
//* Online html documentation available at 
//*            http://www.netlib.org/lapack/explore-html/ 
//* 
//*> \htmlonly 
//*> Download SLANV2 + dependencies 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.tgz?format=tgz&filename=/lapack/lapack_routine/slanv2.f"> 
//*> [TGZ]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.zip?format=zip&filename=/lapack/lapack_routine/slanv2.f"> 
//*> [ZIP]</a> 
//*> <a href="http://www.netlib.org/cgi-bin/netlibfiles.txt?format=txt&filename=/lapack/lapack_routine/slanv2.f"> 
//*> [TXT]</a> 
//*> \endhtmlonly 
//* 
//*  Definition: 
//*  =========== 
//* 
//*       SUBROUTINE SLANV2( A, B, C, D, RT1R, RT1I, RT2R, RT2I, CS, SN ) 
//* 
//*       .. Scalar Arguments .. 
//*       REAL               A, B, C, CS, D, RT1I, RT1R, RT2I, RT2R, SN 
//*       .. 
//* 
//* 
//*> \par Purpose: 
//*  ============= 
//*> 
//*> \verbatim 
//*> 
//*> SLANV2 computes the Schur factorization of a real 2-by-2 nonsymmetric 
//*> matrix in standard form: 
//*> 
//*>      [ A  B ] = [ CS -SN ] [ AA  BB ] [ CS  SN ] 
//*>      [ C  D ]   [ SN  CS ] [ CC  DD ] [-SN  CS ] 
//*> 
//*> where either 
//*> 1) CC = 0 so that AA and DD are real eigenvalues of the matrix, or 
//*> 2) AA = DD and BB*CC < 0, so that AA + or - sqrt(BB*CC) are complex 
//*> conjugate eigenvalues. 
//*> \endverbatim 
//* 
//*  Arguments: 
//*  ========== 
//* 
//*> \param[in,out] A 
//*> \verbatim 
//*>          A is REAL 
//*> \endverbatim 
//*> 
//*> \param[in,out] B 
//*> \verbatim 
//*>          B is REAL 
//*> \endverbatim 
//*> 
//*> \param[in,out] C 
//*> \verbatim 
//*>          C is REAL 
//*> \endverbatim 
//*> 
//*> \param[in,out] D 
//*> \verbatim 
//*>          D is REAL 
//*>          On entry, the elements of the input matrix. 
//*>          On exit, they are overwritten by the elements of the 
//*>          standardised Schur form. 
//*> \endverbatim 
//*> 
//*> \param[out] RT1R 
//*> \verbatim 
//*>          RT1R is REAL 
//*> \endverbatim 
//*> 
//*> \param[out] RT1I 
//*> \verbatim 
//*>          RT1I is REAL 
//*> \endverbatim 
//*> 
//*> \param[out] RT2R 
//*> \verbatim 
//*>          RT2R is REAL 
//*> \endverbatim 
//*> 
//*> \param[out] RT2I 
//*> \verbatim 
//*>          RT2I is REAL 
//*>          The real and imaginary parts of the eigenvalues. If the 
//*>          eigenvalues are a complex conjugate pair, RT1I > 0. 
//*> \endverbatim 
//*> 
//*> \param[out] CS 
//*> \verbatim 
//*>          CS is REAL 
//*> \endverbatim 
//*> 
//*> \param[out] SN 
//*> \verbatim 
//*>          SN is REAL 
//*>          Parameters of the rotation matrix. 
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
//*> \ingroup realOTHERauxiliary 
//* 
//*> \par Further Details: 
//*  ===================== 
//*> 
//*> \verbatim 
//*> 
//*>  Modified by V. Sima, Research Institute for Informatics, Bucharest, 
//*>  Romania, to reduce the risk of cancellation errors, 
//*>  when computing real eigenvalues, and to ensure, if possible, that 
//*>  abs(RT1R) >= abs(RT2R). 
//*> \endverbatim 
//*> 
//*  ===================================================================== 

	 
	public static void _ticz2j2b(ref Single _vxfgpup9, ref Single _p9n405a5, ref Single _3crf0qn3, ref Single _plfm7z8g, ref Single _iinphjl3, ref Single _xoempplp, ref Single _9l52vb05, ref Single _w3mf666f, ref Single _82tpdhyl, ref Single _8tmd0ner)
	{
#region variable declarations
Single _d0547bi2 =  0f;
Single _gbf4169i =  0.5f;
Single _kxg5drh2 =  1f;
Single _s6wyyqpq =  4f;
Single _zwm0s9sq =  default;
Single _uv0s0qmf =  default;
Single _ulx6ckrv =  default;
Single _rxi1jg48 =  default;
Single _985e9e9b =  default;
Single _oa6irzzu =  default;
Single _f4rvsg6o =  default;
Single _p1iqarg6 =  default;
Single _ejwydfmr =  default;
Single _v87rgacb =  default;
Single _t6uj67ei =  default;
Single _1m44vtuk =  default;
Single _91a1vq5f =  default;
Single _apthmm0m =  default;
Single _0446f4de =  default;
Single _1ajfmh55 =  default;
Single _7e60fcso =  default;
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
		//* 
		//*  ===================================================================== 
		//* 
		//*     .. Parameters .. 
		//*     .. 
		//*     .. Local Scalars .. 
		//*     .. 
		//*     .. External Functions .. 
		//*     .. 
		//*     .. Intrinsic Functions .. 
		//*     .. 
		//*     .. Executable Statements .. 
		//* 
		
		_p1iqarg6 = _d5tu038y("P" );
		if (_3crf0qn3 == _d0547bi2)
		{
			
			_82tpdhyl = _kxg5drh2;
			_8tmd0ner = _d0547bi2;//* 
			
		}
		else
		if (_p9n405a5 == _d0547bi2)
		{
			//* 
			//*        Swap rows and columns 
			//* 
			
			_82tpdhyl = _d0547bi2;
			_8tmd0ner = _kxg5drh2;
			_1ajfmh55 = _plfm7z8g;
			_plfm7z8g = _vxfgpup9;
			_vxfgpup9 = _1ajfmh55;
			_p9n405a5 = (-(_3crf0qn3));
			_3crf0qn3 = _d0547bi2;//* 
			
		}
		else
		if (((_vxfgpup9 - _plfm7z8g) == _d0547bi2) & (ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,_p9n405a5 ) != ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,_3crf0qn3 )))
		{
			
			_82tpdhyl = _kxg5drh2;
			_8tmd0ner = _d0547bi2;//* 
			
		}
		else
		{
			//* 
			
			_1ajfmh55 = (_vxfgpup9 - _plfm7z8g);
			_ejwydfmr = (_gbf4169i * _1ajfmh55);
			_ulx6ckrv = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_p9n405a5 ) ,ILNumerics.F2NET.Intrinsics.ABS(_3crf0qn3 ) );
			_rxi1jg48 = ((ILNumerics.F2NET.Intrinsics.MIN(ILNumerics.F2NET.Intrinsics.ABS(_p9n405a5 ) ,ILNumerics.F2NET.Intrinsics.ABS(_3crf0qn3 ) ) * ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,_p9n405a5 )) * ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,_3crf0qn3 ));
			_1m44vtuk = ILNumerics.F2NET.Intrinsics.MAX(ILNumerics.F2NET.Intrinsics.ABS(_ejwydfmr ) ,_ulx6ckrv );
			_7e60fcso = (((_ejwydfmr / _1m44vtuk) * _ejwydfmr) + ((_ulx6ckrv / _1m44vtuk) * _rxi1jg48));//* 
			//*        If Z is of the order of the machine accuracy, postpone the 
			//*        decision on the nature of eigenvalues 
			//* 
			
			if (_7e60fcso >= (_s6wyyqpq * _p1iqarg6))
			{
				//* 
				//*           Real eigenvalues. Compute A and D. 
				//* 
				
				_7e60fcso = (_ejwydfmr + ILNumerics.F2NET.Intrinsics.SIGN(ILNumerics.F2NET.Intrinsics.SQRT(_1m44vtuk ) * ILNumerics.F2NET.Intrinsics.SQRT(_7e60fcso ) ,_ejwydfmr ));
				_vxfgpup9 = (_plfm7z8g + _7e60fcso);
				_plfm7z8g = (_plfm7z8g - ((_ulx6ckrv / _7e60fcso) * _rxi1jg48));//* 
				//*           Compute B and the rotation matrix 
				//* 
				
				_0446f4de = _syk7170d(ref _3crf0qn3 ,ref _7e60fcso );
				_82tpdhyl = (_7e60fcso / _0446f4de);
				_8tmd0ner = (_3crf0qn3 / _0446f4de);
				_p9n405a5 = (_p9n405a5 - _3crf0qn3);
				_3crf0qn3 = _d0547bi2;//* 
				
			}
			else
			{
				//* 
				//*           Complex eigenvalues, or real (almost) equal eigenvalues. 
				//*           Make diagonal elements equal. 
				//* 
				
				_91a1vq5f = (_p9n405a5 + _3crf0qn3);
				_0446f4de = _syk7170d(ref _91a1vq5f ,ref _1ajfmh55 );
				_82tpdhyl = ILNumerics.F2NET.Intrinsics.SQRT(_gbf4169i * (_kxg5drh2 + (ILNumerics.F2NET.Intrinsics.ABS(_91a1vq5f ) / _0446f4de)) );
				_8tmd0ner = (-(((_ejwydfmr / (_0446f4de * _82tpdhyl)) * ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,_91a1vq5f ))));//* 
				//*           Compute [ AA  BB ] = [ A  B ] [ CS -SN ] 
				//*                   [ CC  DD ]   [ C  D ] [ SN  CS ] 
				//* 
				
				_zwm0s9sq = ((_vxfgpup9 * _82tpdhyl) + (_p9n405a5 * _8tmd0ner));
				_uv0s0qmf = ((-((_vxfgpup9 * _8tmd0ner))) + (_p9n405a5 * _82tpdhyl));
				_985e9e9b = ((_3crf0qn3 * _82tpdhyl) + (_plfm7z8g * _8tmd0ner));
				_f4rvsg6o = ((-((_3crf0qn3 * _8tmd0ner))) + (_plfm7z8g * _82tpdhyl));//* 
				//*           Compute [ A  B ] = [ CS  SN ] [ AA  BB ] 
				//*                   [ C  D ]   [-SN  CS ] [ CC  DD ] 
				//* 
				
				_vxfgpup9 = ((_zwm0s9sq * _82tpdhyl) + (_985e9e9b * _8tmd0ner));
				_p9n405a5 = ((_uv0s0qmf * _82tpdhyl) + (_f4rvsg6o * _8tmd0ner));
				_3crf0qn3 = ((-((_zwm0s9sq * _8tmd0ner))) + (_985e9e9b * _82tpdhyl));
				_plfm7z8g = ((-((_uv0s0qmf * _8tmd0ner))) + (_f4rvsg6o * _82tpdhyl));//* 
				
				_1ajfmh55 = (_gbf4169i * (_vxfgpup9 + _plfm7z8g));
				_vxfgpup9 = _1ajfmh55;
				_plfm7z8g = _1ajfmh55;//* 
				
				if (_3crf0qn3 != _d0547bi2)
				{
					
					if (_p9n405a5 != _d0547bi2)
					{
						
						if (ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,_p9n405a5 ) == ILNumerics.F2NET.Intrinsics.SIGN(_kxg5drh2 ,_3crf0qn3 ))
						{
							//* 
							//*                    Real eigenvalues: reduce to upper triangular form 
							//* 
							
							_v87rgacb = ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(_p9n405a5 ) );
							_t6uj67ei = ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(_3crf0qn3 ) );
							_ejwydfmr = ILNumerics.F2NET.Intrinsics.SIGN(_v87rgacb * _t6uj67ei ,_3crf0qn3 );
							_0446f4de = (_kxg5drh2 / ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(_p9n405a5 + _3crf0qn3 ) ));
							_vxfgpup9 = (_1ajfmh55 + _ejwydfmr);
							_plfm7z8g = (_1ajfmh55 - _ejwydfmr);
							_p9n405a5 = (_p9n405a5 - _3crf0qn3);
							_3crf0qn3 = _d0547bi2;
							_oa6irzzu = (_v87rgacb * _0446f4de);
							_apthmm0m = (_t6uj67ei * _0446f4de);
							_1ajfmh55 = ((_82tpdhyl * _oa6irzzu) - (_8tmd0ner * _apthmm0m));
							_8tmd0ner = ((_82tpdhyl * _apthmm0m) + (_8tmd0ner * _oa6irzzu));
							_82tpdhyl = _1ajfmh55;
						}
						
					}
					else
					{
						
						_p9n405a5 = (-(_3crf0qn3));
						_3crf0qn3 = _d0547bi2;
						_1ajfmh55 = _82tpdhyl;
						_82tpdhyl = (-(_8tmd0ner));
						_8tmd0ner = _1ajfmh55;
					}
					
				}
				
			}
			//* 
			
		}
		//* 
		//*     Store eigenvalues in (RT1R,RT1I) and (RT2R,RT2I). 
		//* 
		
		_iinphjl3 = _vxfgpup9;
		_9l52vb05 = _plfm7z8g;
		if (_3crf0qn3 == _d0547bi2)
		{
			
			_xoempplp = _d0547bi2;
			_w3mf666f = _d0547bi2;
		}
		else
		{
			
			_xoempplp = (ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(_p9n405a5 ) ) * ILNumerics.F2NET.Intrinsics.SQRT(ILNumerics.F2NET.Intrinsics.ABS(_3crf0qn3 ) ));
			_w3mf666f = (-(_xoempplp));
		}
		
		return;//* 
		//*     End of SLANV2 
		//* 
		
	}
	
	} // 177

} // end class 
} // end namespace
#endif
