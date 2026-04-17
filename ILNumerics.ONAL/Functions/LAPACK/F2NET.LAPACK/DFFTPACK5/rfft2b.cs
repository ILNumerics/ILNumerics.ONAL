//////////////////////////////////////////////////////////////////
//  This is an auto - generated source file.                    //
//  Do not manually edit this file! Any changes made will be    //
//  lost on the next build! Edit the corresponding source file  //
//  (per default located in the template/ folder) instead!      //
//                                                              //
//////////////////////////////////////////////////////////////////

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
public static unsafe partial class DFFTPACK5 {
//C     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
//C     *                                                               * 
//C     *                  copyright (c) 2011 by UCAR                   * 
//C     *                                                               * 
//C     *       University Corporation for Atmospheric Research         * 
//C     *                                                               * 
//C     *                      all rights reserved                      * 
//C     *                                                               * 
//C     *                     FFTPACK  version 5.1                      * 
//C     *                                                               * 
//C     *                 A Fortran Package of Fast Fourier             * 
//C     *                                                               * 
//C     *                Subroutines and Example Programs               * 
//C     *                                                               * 
//C     *                             by                                * 
//C     *                                                               * 
//C     *               Paul Swarztrauber and Dick Valent               * 
//C     *                                                               * 
//C     *                             of                                * 
//C     *                                                               * 
//C     *         the National Center for Atmospheric Research          * 
//C     *                                                               * 
//C     *                Boulder, Colorado  (80307)  U.S.A.             * 
//C     *                                                               * 
//C     *                   which is sponsored by                       * 
//C     *                                                               * 
//C     *              the National Science Foundation                  * 
//C     *                                                               * 
//C     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
//C 

	 
	public static void _tu4bqeav(ref Int32 _14t4iqnj, ref Int32 _cjahrwwv, ref Int32 _cf4kqqk2, Double* _wkmvx6rb, Double* _uuvyil0u, ref Int32 _xjpd3das, Double* _y1uc90sp, ref Int32 _sybtf40y, ref Int32 _va7gplz4)
	{
#region variable declarations
Int32 _zc2wxh7q =  default;
Int32 _xyk50wc4 =  default;
Int32 _j5x18dti =  default;
Int32 _wmsh4kw4 =  default;
Int32 _9zasqhyg =  default;
Int32 _l8uxe3nf =  default;
Int32 _qb0uu4i2 =  default;
Int32 _fn23rpdf =  default;
Int32 _ca5zno6d =  default;
Int32 _jejunmnd =  default;
Int32 _ld42zxsk =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//C 
		//C 
		//C INITIALIZE IER 
		//C 
		
		_va7gplz4 = (int)0;//C 
		//C VERIFY LENSAV 
		//C 
		
		_xyk50wc4 = ((_cjahrwwv + ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.DBLE(_cjahrwwv ) ) / ILNumerics.F2NET.Intrinsics.LOG((double)2 ) )) + (int)4);
		_j5x18dti = ((((int)2 * _cf4kqqk2) + ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.DBLE(_cf4kqqk2 ) ) / ILNumerics.F2NET.Intrinsics.LOG((double)2 ) )) + (int)4);
		_wmsh4kw4 = ((_cf4kqqk2 + ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.DBLE(_cf4kqqk2 ) ) / ILNumerics.F2NET.Intrinsics.LOG((double)2 ) )) + (int)4);
		_9zasqhyg = ILNumerics.F2NET.Intrinsics.MOD(_cjahrwwv ,(int)2 );
		_l8uxe3nf = ILNumerics.F2NET.Intrinsics.MOD(_cf4kqqk2 ,(int)2 );//C 
		
		if (_xjpd3das < ((_xyk50wc4 + _j5x18dti) + _wmsh4kw4))
		{
			
			_va7gplz4 = (int)2;
			_yc1iu9qu("RFFT2F" ,ref Unsafe.AsRef((int)6) );goto Mark100;
		}
		//C 
		//C VERIFY LENWRK 
		//C 
		
		if (_sybtf40y < ((_cjahrwwv + (int)1) * _cf4kqqk2))
		{
			
			_va7gplz4 = (int)3;
			_yc1iu9qu("RFFT2F" ,ref Unsafe.AsRef((int)8) );goto Mark100;
		}
		//C 
		//C VERIFY LDIM IS AS BIG AS L 
		//C 
		
		if (_14t4iqnj < _cjahrwwv)
		{
			
			_va7gplz4 = (int)5;
			_yc1iu9qu("RFFT2F" ,ref Unsafe.AsRef((int)-6) );goto Mark100;
		}
		//C 
		//C TRANSFORM SECOND DIMENSION OF ARRAY 
		//C 
		
		{
			System.Int32 __81fgg2dlsvn517 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step517 = (System.Int32)((int)1);
			System.Int32 __81fgg2count517;
			for (__81fgg2count517 = System.Math.Max(0, (System.Int32)(((System.Int32)(((int)2 * ((_cf4kqqk2 + (int)1) / (int)2)) - (int)1) - __81fgg2dlsvn517 + __81fgg2step517) / __81fgg2step517)), _qb0uu4i2 = __81fgg2dlsvn517; __81fgg2count517 != 0; __81fgg2count517--, _qb0uu4i2 += (__81fgg2step517)) {

			{
				
				*(_wkmvx6rb+((int)1 - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj)) = (*(_wkmvx6rb+((int)1 - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj)) + *(_wkmvx6rb+((int)1 - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj)));
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn518 = (System.Int32)((int)3);
			System.Int32 __81fgg2step518 = (System.Int32)((int)2);
			System.Int32 __81fgg2count518;
			for (__81fgg2count518 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cf4kqqk2) - __81fgg2dlsvn518 + __81fgg2step518) / __81fgg2step518)), _qb0uu4i2 = __81fgg2dlsvn518; __81fgg2count518 != 0; __81fgg2count518--, _qb0uu4i2 += (__81fgg2step518)) {

			{
				
				*(_wkmvx6rb+((int)1 - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj)) = (-(*(_wkmvx6rb+((int)1 - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj))));
			}
						}		}
		_3gywwfj4(ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef((int)1) ,ref _cf4kqqk2 ,ref _14t4iqnj ,_wkmvx6rb ,ref Unsafe.AsRef(_cf4kqqk2 * _14t4iqnj) ,(_uuvyil0u+((_xyk50wc4 + _j5x18dti) + (int)1 - 1)),ref _wmsh4kw4 ,_y1uc90sp ,ref _sybtf40y ,ref _fn23rpdf );
		_ca5zno6d = ILNumerics.F2NET.Intrinsics.INT((_cjahrwwv + (int)1) / (int)2 );
		if (_ca5zno6d > (int)1)
		{
			
			_jejunmnd = (_ca5zno6d + _ca5zno6d);//C 
			//C     R AND WORK ARE SWITCHED BECAUSE THE THE FIRST DIMENSION 
			//C     OF THE INPUT TO COMPLEX CFFTMF MUST BE EVEN. 
			//C 
			
			_szbmqxuz(ref _14t4iqnj ,ref _jejunmnd ,ref _cjahrwwv ,ref _cf4kqqk2 ,_wkmvx6rb ,_y1uc90sp );
			_gw6i40vj(ref Unsafe.AsRef(_ca5zno6d - (int)1) ,ref Unsafe.AsRef((int)1) ,ref _cf4kqqk2 ,ref _ca5zno6d ,(ILNumerics.complex*)(_y1uc90sp+((int)2 - 1)) ,ref Unsafe.AsRef(_ca5zno6d * _cf4kqqk2) ,(_uuvyil0u+(_xyk50wc4 + (int)1 - 1)),ref _j5x18dti ,_wkmvx6rb ,ref Unsafe.AsRef(_cjahrwwv * _cf4kqqk2) ,ref _fn23rpdf );
			if (_fn23rpdf != (int)0)
			{
				
				_va7gplz4 = (int)20;
				_yc1iu9qu("RFFT2B" ,ref Unsafe.AsRef((int)-5) );goto Mark100;
			}
			
			_x1ghx7ba(ref _14t4iqnj ,ref _jejunmnd ,ref _cjahrwwv ,ref _cf4kqqk2 ,_wkmvx6rb ,_y1uc90sp );
		}
		//C 
		
		if (_9zasqhyg == (int)0)
		{
			
			{
				System.Int32 __81fgg2dlsvn519 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step519 = (System.Int32)((int)1);
				System.Int32 __81fgg2count519;
				for (__81fgg2count519 = System.Math.Max(0, (System.Int32)(((System.Int32)(((int)2 * ((_cf4kqqk2 + (int)1) / (int)2)) - (int)1) - __81fgg2dlsvn519 + __81fgg2step519) / __81fgg2step519)), _qb0uu4i2 = __81fgg2dlsvn519; __81fgg2count519 != 0; __81fgg2count519--, _qb0uu4i2 += (__81fgg2step519)) {

				{
					
					*(_wkmvx6rb+(_cjahrwwv - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj)) = (*(_wkmvx6rb+(_cjahrwwv - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj)) + *(_wkmvx6rb+(_cjahrwwv - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj)));
				}
								}			}
			{
				System.Int32 __81fgg2dlsvn520 = (System.Int32)((int)3);
				System.Int32 __81fgg2step520 = (System.Int32)((int)2);
				System.Int32 __81fgg2count520;
				for (__81fgg2count520 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cf4kqqk2) - __81fgg2dlsvn520 + __81fgg2step520) / __81fgg2step520)), _qb0uu4i2 = __81fgg2dlsvn520; __81fgg2count520 != 0; __81fgg2count520--, _qb0uu4i2 += (__81fgg2step520)) {

				{
					
					*(_wkmvx6rb+(_cjahrwwv - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj)) = (-(*(_wkmvx6rb+(_cjahrwwv - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj))));
				}
								}			}
			_3gywwfj4(ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef((int)1) ,ref _cf4kqqk2 ,ref _14t4iqnj ,(_wkmvx6rb+(_cjahrwwv - 1) + ((int)1 - 1) * 1 * (_14t4iqnj)),ref Unsafe.AsRef(_cf4kqqk2 * _14t4iqnj) ,(_uuvyil0u+((_xyk50wc4 + _j5x18dti) + (int)1 - 1)),ref _wmsh4kw4 ,_y1uc90sp ,ref _sybtf40y ,ref _fn23rpdf );
		}
		//C 
		//C     PRINT*, 'BACKWARD TRANSFORM IN THE J DIRECTION' 
		//C     DO I=1,L 
		//C       PRINT*, (R(I,J),J=1,M) 
		//C     END DO 
		//C 
		//C TRANSFORM FIRST DIMENSION OF ARRAY 
		//C 
		
		_zc2wxh7q = (((int)2 * ILNumerics.F2NET.Intrinsics.INT((_cjahrwwv + (int)1) / (int)2 )) - (int)1);
		{
			System.Int32 __81fgg2dlsvn521 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step521 = (System.Int32)((int)1);
			System.Int32 __81fgg2count521;
			for (__81fgg2count521 = System.Math.Max(0, (System.Int32)(((System.Int32)(_zc2wxh7q) - __81fgg2dlsvn521 + __81fgg2step521) / __81fgg2step521)), _ld42zxsk = __81fgg2dlsvn521; __81fgg2count521 != 0; __81fgg2count521--, _ld42zxsk += (__81fgg2step521)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn522 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step522 = (System.Int32)((int)1);
					System.Int32 __81fgg2count522;
					for (__81fgg2count522 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cf4kqqk2) - __81fgg2dlsvn522 + __81fgg2step522) / __81fgg2step522)), _qb0uu4i2 = __81fgg2dlsvn522; __81fgg2count522 != 0; __81fgg2count522--, _qb0uu4i2 += (__81fgg2step522)) {

					{
						
						*(_wkmvx6rb+(_ld42zxsk - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj)) = (*(_wkmvx6rb+(_ld42zxsk - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj)) + *(_wkmvx6rb+(_ld42zxsk - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj)));
					}
										}				}
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn523 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step523 = (System.Int32)((int)1);
			System.Int32 __81fgg2count523;
			for (__81fgg2count523 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cf4kqqk2) - __81fgg2dlsvn523 + __81fgg2step523) / __81fgg2step523)), _qb0uu4i2 = __81fgg2dlsvn523; __81fgg2count523 != 0; __81fgg2count523--, _qb0uu4i2 += (__81fgg2step523)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn524 = (System.Int32)((int)3);
					System.Int32 __81fgg2step524 = (System.Int32)((int)2);
					System.Int32 __81fgg2count524;
					for (__81fgg2count524 = System.Math.Max(0, (System.Int32)(((System.Int32)(_zc2wxh7q) - __81fgg2dlsvn524 + __81fgg2step524) / __81fgg2step524)), _ld42zxsk = __81fgg2dlsvn524; __81fgg2count524 != 0; __81fgg2count524--, _ld42zxsk += (__81fgg2step524)) {

					{
						
						*(_wkmvx6rb+(_ld42zxsk - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj)) = (-(*(_wkmvx6rb+(_ld42zxsk - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj))));
					}
										}				}
			}
						}		}
		_3gywwfj4(ref _cf4kqqk2 ,ref _14t4iqnj ,ref _cjahrwwv ,ref Unsafe.AsRef((int)1) ,_wkmvx6rb ,ref Unsafe.AsRef(_cf4kqqk2 * _14t4iqnj) ,(_uuvyil0u+((int)1 - 1)),ref Unsafe.AsRef((_cjahrwwv + ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.DBLE(_cjahrwwv ) ) / ILNumerics.F2NET.Intrinsics.LOG((double)2 ) )) + (int)4) ,_y1uc90sp ,ref _sybtf40y ,ref _fn23rpdf );//C 
		// 
		//C 
		//C     PRINT*, 'BACKWARD TRANSFORM IN THE I DIRECTION' 
		//C     DO I=1,L 
		//C       PRINT*, (R(I,J),J=1,M) 
		//C     END DO 
		//C 
		
		if (_fn23rpdf != (int)0)
		{
			
			_va7gplz4 = (int)20;
			_yc1iu9qu("RFFT2F" ,ref Unsafe.AsRef((int)-5) );goto Mark100;
		}
		//C 
		
		if (_fn23rpdf != (int)0)
		{
			
			_va7gplz4 = (int)20;
			_yc1iu9qu("RFFT2F" ,ref Unsafe.AsRef((int)-5) );goto Mark100;
		}
		//C 
		
Mark100:;
		// continue//C 
		
		return;
	}
	
	} // 177

} // end class 
} // end namespace
#endif
