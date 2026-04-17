
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
public static unsafe partial class FFTPACK5 {
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

	 
	public static void _fyjd968h(ref Int32 _14t4iqnj, ref Int32 _cjahrwwv, ref Int32 _cf4kqqk2, Single* _wkmvx6rb, Single* _uuvyil0u, ref Int32 _xjpd3das, Single* _y1uc90sp, ref Int32 _sybtf40y, ref Int32 _va7gplz4)
	{
#region variable declarations
Int32 _76ks9ceq =  default;
Int32 _9zasqhyg =  default;
Int32 _l8uxe3nf =  default;
Int32 _n5zvov1l =  default;
Int32 _cixqc4ez =  default;
Int32 _xyk50wc4 =  default;
Int32 _j5x18dti =  default;
Int32 _wmsh4kw4 =  default;
Int32 _fn23rpdf =  default;
Int32 _zc2wxh7q =  default;
Int32 _ld42zxsk =  default;
Int32 _qb0uu4i2 =  default;
Int32 _ca5zno6d =  default;
Int32 _jejunmnd =  default;
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
		
		_xyk50wc4 = ((_cjahrwwv + ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.REAL(_cjahrwwv ) ) / ILNumerics.F2NET.Intrinsics.LOG((float)2 ) )) + (int)4);
		_j5x18dti = ((((int)2 * _cf4kqqk2) + ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.REAL(_cf4kqqk2 ) ) / ILNumerics.F2NET.Intrinsics.LOG((float)2 ) )) + (int)4);
		_wmsh4kw4 = ((_cf4kqqk2 + ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.REAL(_cf4kqqk2 ) ) / ILNumerics.F2NET.Intrinsics.LOG((float)2 ) )) + (int)4);//C 
		
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
		//C TRANSFORM FIRST DIMENSION OF ARRAY 
		//C 
		
		_xlz6l0rm(ref _cf4kqqk2 ,ref _14t4iqnj ,ref _cjahrwwv ,ref Unsafe.AsRef((int)1) ,_wkmvx6rb ,ref Unsafe.AsRef(_cf4kqqk2 * _14t4iqnj) ,(_uuvyil0u+((int)1 - 1)),ref Unsafe.AsRef((_cjahrwwv + ILNumerics.F2NET.Intrinsics.INT(ILNumerics.F2NET.Intrinsics.LOG(ILNumerics.F2NET.Intrinsics.REAL(_cjahrwwv ) ) / ILNumerics.F2NET.Intrinsics.LOG((float)2 ) )) + (int)4) ,_y1uc90sp ,ref _sybtf40y ,ref _fn23rpdf );//C 
		
		if (_fn23rpdf != (int)0)
		{
			
			_va7gplz4 = (int)20;
			_yc1iu9qu("RFFT2F" ,ref Unsafe.AsRef((int)-5) );goto Mark100;
		}
		//C 
		
		_zc2wxh7q = (((int)2 * ILNumerics.F2NET.Intrinsics.INT((_cjahrwwv + (int)1) / (int)2 )) - (int)1);
		{
			System.Int32 __81fgg2dlsvn428 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step428 = (System.Int32)((int)1);
			System.Int32 __81fgg2count428;
			for (__81fgg2count428 = System.Math.Max(0, (System.Int32)(((System.Int32)(_zc2wxh7q) - __81fgg2dlsvn428 + __81fgg2step428) / __81fgg2step428)), _ld42zxsk = __81fgg2dlsvn428; __81fgg2count428 != 0; __81fgg2count428--, _ld42zxsk += (__81fgg2step428)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn429 = (System.Int32)((int)1);
					const System.Int32 __81fgg2step429 = (System.Int32)((int)1);
					System.Int32 __81fgg2count429;
					for (__81fgg2count429 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cf4kqqk2) - __81fgg2dlsvn429 + __81fgg2step429) / __81fgg2step429)), _qb0uu4i2 = __81fgg2dlsvn429; __81fgg2count429 != 0; __81fgg2count429--, _qb0uu4i2 += (__81fgg2step429)) {

					{
						
						*(_wkmvx6rb+(_ld42zxsk - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj)) = ((float)0.5 * *(_wkmvx6rb+(_ld42zxsk - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj)));
					}
										}				}
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn430 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step430 = (System.Int32)((int)1);
			System.Int32 __81fgg2count430;
			for (__81fgg2count430 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cf4kqqk2) - __81fgg2dlsvn430 + __81fgg2step430) / __81fgg2step430)), _qb0uu4i2 = __81fgg2dlsvn430; __81fgg2count430 != 0; __81fgg2count430--, _qb0uu4i2 += (__81fgg2step430)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn431 = (System.Int32)((int)3);
					System.Int32 __81fgg2step431 = (System.Int32)((int)2);
					System.Int32 __81fgg2count431;
					for (__81fgg2count431 = System.Math.Max(0, (System.Int32)(((System.Int32)(_zc2wxh7q) - __81fgg2dlsvn431 + __81fgg2step431) / __81fgg2step431)), _ld42zxsk = __81fgg2dlsvn431; __81fgg2count431 != 0; __81fgg2count431--, _ld42zxsk += (__81fgg2step431)) {

					{
						
						*(_wkmvx6rb+(_ld42zxsk - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj)) = (-(*(_wkmvx6rb+(_ld42zxsk - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj))));
					}
										}				}
			}
						}		}//C 
		//C     PRINT*, 'FORWARD TRANSFORM IN THE I DIRECTION' 
		//C     DO I=1,L 
		//C       PRINT*, (R(I,J),J=1,M) 
		//C     END DO 
		//C 
		//C RESHUFFLE TO ADD IN NYQUIST IMAGINARY COMPONENTS 
		//C 
		
		_9zasqhyg = ILNumerics.F2NET.Intrinsics.MOD(_cjahrwwv ,(int)2 );
		_l8uxe3nf = ILNumerics.F2NET.Intrinsics.MOD(_cf4kqqk2 ,(int)2 );//C 
		//C TRANSFORM SECOND DIMENSION OF ARRAY 
		//C 
		
		_xlz6l0rm(ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef((int)1) ,ref _cf4kqqk2 ,ref _14t4iqnj ,_wkmvx6rb ,ref Unsafe.AsRef(_cf4kqqk2 * _14t4iqnj) ,(_uuvyil0u+((_xyk50wc4 + _j5x18dti) + (int)1 - 1)),ref _wmsh4kw4 ,_y1uc90sp ,ref _sybtf40y ,ref _fn23rpdf );
		{
			System.Int32 __81fgg2dlsvn432 = (System.Int32)((int)2);
			const System.Int32 __81fgg2step432 = (System.Int32)((int)1);
			System.Int32 __81fgg2count432;
			for (__81fgg2count432 = System.Math.Max(0, (System.Int32)(((System.Int32)(((int)2 * ((_cf4kqqk2 + (int)1) / (int)2)) - (int)1) - __81fgg2dlsvn432 + __81fgg2step432) / __81fgg2step432)), _qb0uu4i2 = __81fgg2dlsvn432; __81fgg2count432 != 0; __81fgg2count432--, _qb0uu4i2 += (__81fgg2step432)) {

			{
				
				*(_wkmvx6rb+((int)1 - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj)) = ((float)0.5 * *(_wkmvx6rb+((int)1 - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj)));
			}
						}		}
		{
			System.Int32 __81fgg2dlsvn433 = (System.Int32)((int)3);
			System.Int32 __81fgg2step433 = (System.Int32)((int)2);
			System.Int32 __81fgg2count433;
			for (__81fgg2count433 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cf4kqqk2) - __81fgg2dlsvn433 + __81fgg2step433) / __81fgg2step433)), _qb0uu4i2 = __81fgg2dlsvn433; __81fgg2count433 != 0; __81fgg2count433--, _qb0uu4i2 += (__81fgg2step433)) {

			{
				
				*(_wkmvx6rb+((int)1 - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj)) = (-(*(_wkmvx6rb+((int)1 - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj))));
			}
						}		}
		_ca5zno6d = ILNumerics.F2NET.Intrinsics.INT((_cjahrwwv + (int)1) / (int)2 );
		if (_ca5zno6d > (int)1)
		{
			
			_jejunmnd = (_ca5zno6d + _ca5zno6d);//C 
			//C     R AND WORK ARE SWITCHED BECAUSE THE THE FIRST DIMENSION 
			//C     OF THE INPUT TO COMPLEX CFFTMF MUST BE EVEN. 
			//C 
			
			_szbmqxuz(ref _14t4iqnj ,ref _jejunmnd ,ref _cjahrwwv ,ref _cf4kqqk2 ,_wkmvx6rb ,_y1uc90sp );
			_l6sfv99g(ref Unsafe.AsRef(_ca5zno6d - (int)1) ,ref Unsafe.AsRef((int)1) ,ref _cf4kqqk2 ,ref _ca5zno6d ,(ILNumerics.fcomplex*)(_y1uc90sp+((int)2 - 1)) ,ref Unsafe.AsRef(_ca5zno6d * _cf4kqqk2) ,(_uuvyil0u+(_xyk50wc4 + (int)1 - 1)),ref _j5x18dti ,_wkmvx6rb ,ref Unsafe.AsRef(_cjahrwwv * _cf4kqqk2) ,ref _fn23rpdf );
			if (_fn23rpdf != (int)0)
			{
				
				_va7gplz4 = (int)20;
				_yc1iu9qu("RFFT2F" ,ref Unsafe.AsRef((int)-5) );goto Mark100;
			}
			
			_x1ghx7ba(ref _14t4iqnj ,ref _jejunmnd ,ref _cjahrwwv ,ref _cf4kqqk2 ,_wkmvx6rb ,_y1uc90sp );
		}
		//C 
		
		if (_9zasqhyg == (int)0)
		{
			
			_xlz6l0rm(ref Unsafe.AsRef((int)1) ,ref Unsafe.AsRef((int)1) ,ref _cf4kqqk2 ,ref _14t4iqnj ,(_wkmvx6rb+(_cjahrwwv - 1) + ((int)1 - 1) * 1 * (_14t4iqnj)),ref Unsafe.AsRef(_cf4kqqk2 * _14t4iqnj) ,(_uuvyil0u+((_xyk50wc4 + _j5x18dti) + (int)1 - 1)),ref _wmsh4kw4 ,_y1uc90sp ,ref _sybtf40y ,ref _fn23rpdf );
			{
				System.Int32 __81fgg2dlsvn434 = (System.Int32)((int)2);
				const System.Int32 __81fgg2step434 = (System.Int32)((int)1);
				System.Int32 __81fgg2count434;
				for (__81fgg2count434 = System.Math.Max(0, (System.Int32)(((System.Int32)(((int)2 * ((_cf4kqqk2 + (int)1) / (int)2)) - (int)1) - __81fgg2dlsvn434 + __81fgg2step434) / __81fgg2step434)), _qb0uu4i2 = __81fgg2dlsvn434; __81fgg2count434 != 0; __81fgg2count434--, _qb0uu4i2 += (__81fgg2step434)) {

				{
					
					*(_wkmvx6rb+(_cjahrwwv - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj)) = ((float)0.5 * *(_wkmvx6rb+(_cjahrwwv - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj)));
				}
								}			}
			{
				System.Int32 __81fgg2dlsvn435 = (System.Int32)((int)3);
				System.Int32 __81fgg2step435 = (System.Int32)((int)2);
				System.Int32 __81fgg2count435;
				for (__81fgg2count435 = System.Math.Max(0, (System.Int32)(((System.Int32)(_cf4kqqk2) - __81fgg2dlsvn435 + __81fgg2step435) / __81fgg2step435)), _qb0uu4i2 = __81fgg2dlsvn435; __81fgg2count435 != 0; __81fgg2count435--, _qb0uu4i2 += (__81fgg2step435)) {

				{
					
					*(_wkmvx6rb+(_cjahrwwv - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj)) = (-(*(_wkmvx6rb+(_cjahrwwv - 1) + (_qb0uu4i2 - 1) * 1 * (_14t4iqnj))));
				}
								}			}
		}
		//C 
		//C     PRINT*, 'FORWARD TRANSFORM IN THE J DIRECTION' 
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
		//C 
		
Mark100:;
		// continue
		return;
	}
	
	} // 177

} // end class 
} // end namespace
#endif
