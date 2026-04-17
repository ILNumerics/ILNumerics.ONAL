
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

	 
	public static void _wpv6n2rz(ref Int32 _ivvongrz, ref Int32 _1mqgnnli, Single* _g0tjc1wj, ref Int32 _411n0nn5, Single* _tyzpsh18, ref Int32 _a8qh3w7g, Single* _t99b7n0f, Single* _g45v2kzv, Single* _zt7ko2c4)
	{
#region variable declarations
Single _agfmp4lk =  default;
Int32 _1m894xin =  default;
Int32 _dmboubtu =  default;
Int32 _ld42zxsk =  default;
Int32 _knhh7y2m =  default;
string fLanavab = default;
#endregion  variable declarations

	{
		//C 
		
		_agfmp4lk = (ILNumerics.F2NET.Intrinsics.SQRT((float)2 ) / (float)2);
		{
			System.Int32 __81fgg2dlsvn229 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step229 = (System.Int32)((int)1);
			System.Int32 __81fgg2count229;
			for (__81fgg2count229 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn229 + __81fgg2step229) / __81fgg2step229)), _1m894xin = __81fgg2dlsvn229; __81fgg2count229 != 0; __81fgg2count229--, _1m894xin += (__81fgg2step229)) {

			{
				
				*(_tyzpsh18+((int)1 - 1) + ((int)1 - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)4)) = ((*(_g0tjc1wj+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) + *(_g0tjc1wj+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_g0tjc1wj+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) + *(_g0tjc1wj+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))));
				*(_tyzpsh18+((int)1 - 1) + (_ivvongrz - 1) * 1 * (_a8qh3w7g) + ((int)4 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)4)) = ((*(_g0tjc1wj+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) + *(_g0tjc1wj+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_g0tjc1wj+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) + *(_g0tjc1wj+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))));
				*(_tyzpsh18+((int)1 - 1) + (_ivvongrz - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)4)) = (*(_g0tjc1wj+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) - *(_g0tjc1wj+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)));
				*(_tyzpsh18+((int)1 - 1) + ((int)1 - 1) * 1 * (_a8qh3w7g) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)4)) = (*(_g0tjc1wj+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) - *(_g0tjc1wj+((int)1 - 1) + ((int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)));
Mark101:;
				// continue
			}
						}		}if ((_ivvongrz - (int)2) < 0) goto Mark107; else if ((_ivvongrz - (int)2) == 0) goto Mark105; else goto Mark102;
Mark102:;
		
		_dmboubtu = (_ivvongrz + (int)2);
		{
			System.Int32 __81fgg2dlsvn230 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step230 = (System.Int32)((int)1);
			System.Int32 __81fgg2count230;
			for (__81fgg2count230 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn230 + __81fgg2step230) / __81fgg2step230)), _1m894xin = __81fgg2dlsvn230; __81fgg2count230 != 0; __81fgg2count230--, _1m894xin += (__81fgg2step230)) {

			{
				
				{
					System.Int32 __81fgg2dlsvn231 = (System.Int32)((int)3);
					System.Int32 __81fgg2step231 = (System.Int32)((int)2);
					System.Int32 __81fgg2count231;
					for (__81fgg2count231 = System.Math.Max(0, (System.Int32)(((System.Int32)(_ivvongrz) - __81fgg2dlsvn231 + __81fgg2step231) / __81fgg2step231)), _ld42zxsk = __81fgg2dlsvn231; __81fgg2count231 != 0; __81fgg2count231--, _ld42zxsk += (__81fgg2step231)) {

					{
						
						_knhh7y2m = (_dmboubtu - _ld42zxsk);
						*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)4)) = ((((*(_t99b7n0f+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_t99b7n0f+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))) + ((*(_zt7ko2c4+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_zt7ko2c4+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))))) + (*(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) + ((*(_g45v2kzv+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_g45v2kzv+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))))));
						*(_tyzpsh18+((int)1 - 1) + (_knhh7y2m - (int)1 - 1) * 1 * (_a8qh3w7g) + ((int)4 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)4)) = ((*(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) + ((*(_g45v2kzv+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_g45v2kzv+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))))) - (((*(_t99b7n0f+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_t99b7n0f+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))) + ((*(_zt7ko2c4+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_zt7ko2c4+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))))));
						*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)4)) = ((((*(_t99b7n0f+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_t99b7n0f+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))) + ((*(_zt7ko2c4+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_zt7ko2c4+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))))) + (*(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) + ((*(_g45v2kzv+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_g45v2kzv+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))))));
						*(_tyzpsh18+((int)1 - 1) + (_knhh7y2m - 1) * 1 * (_a8qh3w7g) + ((int)4 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)4)) = ((((*(_t99b7n0f+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_t99b7n0f+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))) + ((*(_zt7ko2c4+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_zt7ko2c4+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))))) - (*(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) + ((*(_g45v2kzv+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_g45v2kzv+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))))));
						*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_a8qh3w7g) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)4)) = ((((*(_t99b7n0f+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_t99b7n0f+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))) - ((*(_zt7ko2c4+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_zt7ko2c4+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))))) + (*(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) - ((*(_g45v2kzv+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_g45v2kzv+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))))));
						*(_tyzpsh18+((int)1 - 1) + (_knhh7y2m - (int)1 - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)4)) = ((*(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) - ((*(_g45v2kzv+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_g45v2kzv+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))))) - (((*(_t99b7n0f+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_t99b7n0f+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))) - ((*(_zt7ko2c4+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_zt7ko2c4+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))))));
						*(_tyzpsh18+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_a8qh3w7g) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)4)) = ((((*(_zt7ko2c4+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_zt7ko2c4+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))) - ((*(_t99b7n0f+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_t99b7n0f+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))))) + (*(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) - ((*(_g45v2kzv+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_g45v2kzv+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))))));
						*(_tyzpsh18+((int)1 - 1) + (_knhh7y2m - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)4)) = ((((*(_zt7ko2c4+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_zt7ko2c4+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))) - ((*(_t99b7n0f+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) + (*(_t99b7n0f+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))))) - (*(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) - ((*(_g45v2kzv+(_ld42zxsk - (int)2 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))) - (*(_g45v2kzv+(_ld42zxsk - (int)1 - 1)) * *(_g0tjc1wj+((int)1 - 1) + (_ld42zxsk - (int)1 - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli))))));
Mark103:;
						// continue
					}
										}				}
Mark104:;
				// continue
			}
						}		}
		if (ILNumerics.F2NET.Intrinsics.MOD(_ivvongrz ,(int)2 ) == (int)1)
		return;
Mark105:;
		// continue
		{
			System.Int32 __81fgg2dlsvn232 = (System.Int32)((int)1);
			const System.Int32 __81fgg2step232 = (System.Int32)((int)1);
			System.Int32 __81fgg2count232;
			for (__81fgg2count232 = System.Math.Max(0, (System.Int32)(((System.Int32)(_1mqgnnli) - __81fgg2dlsvn232 + __81fgg2step232) / __81fgg2step232)), _1m894xin = __81fgg2dlsvn232; __81fgg2count232 != 0; __81fgg2count232--, _1m894xin += (__81fgg2step232)) {

			{
				
				*(_tyzpsh18+((int)1 - 1) + (_ivvongrz - 1) * 1 * (_a8qh3w7g) + ((int)1 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)4)) = ((_agfmp4lk * (*(_g0tjc1wj+((int)1 - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) - *(_g0tjc1wj+((int)1 - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))) + *(_g0tjc1wj+((int)1 - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)));
				*(_tyzpsh18+((int)1 - 1) + (_ivvongrz - 1) * 1 * (_a8qh3w7g) + ((int)3 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)4)) = (*(_g0tjc1wj+((int)1 - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)1 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) - (_agfmp4lk * (*(_g0tjc1wj+((int)1 - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) - *(_g0tjc1wj+((int)1 - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))));
				*(_tyzpsh18+((int)1 - 1) + ((int)1 - 1) * 1 * (_a8qh3w7g) + ((int)2 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)4)) = ((-((_agfmp4lk * (*(_g0tjc1wj+((int)1 - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) + *(_g0tjc1wj+((int)1 - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))))) - *(_g0tjc1wj+((int)1 - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)));
				*(_tyzpsh18+((int)1 - 1) + ((int)1 - 1) * 1 * (_a8qh3w7g) + ((int)4 - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) + (_1m894xin - 1) * 1 * (_a8qh3w7g) * (_ivvongrz) * ((int)4)) = ((-((_agfmp4lk * (*(_g0tjc1wj+((int)1 - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)2 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)) + *(_g0tjc1wj+((int)1 - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)4 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)))))) + *(_g0tjc1wj+((int)1 - 1) + (_ivvongrz - 1) * 1 * (_411n0nn5) + (_1m894xin - 1) * 1 * (_411n0nn5) * (_ivvongrz) + ((int)3 - 1) * 1 * (_411n0nn5) * (_ivvongrz) * (_1mqgnnli)));
Mark106:;
				// continue
			}
						}		}
Mark107:;
		
		return;
	}
	
	} // 177

} // end class 
} // end namespace
#endif
