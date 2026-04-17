//////////////////////////////////////////////////////////////////
//                                                              //
//  This is an auto - manipulated source file.                  //
//  Edits inside regions of HYCALPER AUTO GENERATED CODE        //
//  will be lost and overwritten on the next build!             //
//                                                              //
//////////////////////////////////////////////////////////////////
using System;
using ILNumerics;
using static ILNumerics.Globals;
using static ILNumerics.Core.Functions.Builtin.MathInternal;
using ILNumerics.Core.Functions.Builtin;
using ILNumerics.Core.Native;
using System.Security;

namespace ILNumerics {
    
    public partial class ILMath {

        static ILMath() {
            System.ComponentModel.LicenseManager.Validate(typeof(ILMath)); 

            // causes intialization + detects for MKL
            if (MathInternal.Lapack == null) {
                MathInternal.Lapack = new ILNumerics.F2NET.ManagedLAPACK();
            }
            if (MathInternal.FFTImplementation == null) {
                MathInternal.FFTImplementation = new ILNumerics.F2NET.ManagedFFTPACK5(); 
            }
        }

        /// <summary>
        /// Lapack implementation.
        /// </summary>
        public static ILapack Lapack {

            get { return MathInternal.Lapack; }
            set { MathInternal.Lapack = value; }
        }
   }
}
