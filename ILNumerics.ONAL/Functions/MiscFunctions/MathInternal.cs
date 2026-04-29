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
using ILNumerics.Core.Native;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Functions.Builtin {
    internal static partial class MathInternal {

        #region attributes / properties
        internal static ILapack Lapack { get; set; } = null;
        internal static IFFT FFTImplementation { get; set; } = null;
        #endregion

        #region constructors / init
        static MathInternal() {
            InitInternal();
        }
        #endregion

        #region Helpers
         
        internal static void InitInternal() {

            #region find bitrate
            try {
                string path = Environment.GetEnvironmentVariable("PATH");
                // credit goes to SO: http://stackoverflow.com/questions/864484/getting-the-path-of-the-current-assembly
                // this is better than using .Location! See the comments in the answer at SO.
                // Basically, it ensures the location of the assembly to the one installed to. This is 
                // important for ASP.NET scenarios, where the assembly may be shadow copied to a temp folder.
#pragma warning disable SYSLIB0012
                string myPath = (new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
#pragma warning restore
                myPath = System.Uri.UnescapeDataString(myPath); // removes %20 for spaces a.t.l (DOES NOT WORK WITH # in path!!)
                myPath = Path.GetDirectoryName(myPath); // turns forward slashes into backslashes (DOES NOT WORK WITH SPACES IN PATH!!)

                if (myPath.StartsWith("file:\\")) {
                    myPath = myPath.Substring(6);    // TODO: how to remove that hack ?? 
                }

                if (!String.IsNullOrWhiteSpace(Settings.NativeDependenciesAbsolutePath)) {
                    myPath = Path.GetDirectoryName(Settings.NativeDependenciesAbsolutePath);
                    System.Diagnostics.Trace.WriteLine("Dependency directory set by configuration: " + myPath);
                } else if (Environment.Is64BitProcess) {
                    myPath = Path.Combine(myPath, "bin64");
                    System.Diagnostics.Trace.WriteLine("64 bit process detected.");
                } else {
                    myPath = Path.Combine(myPath, "bin32");
                    System.Diagnostics.Trace.WriteLine("32 bit process detected.");
                }
                if (!path.Contains(myPath)) {
                    System.Diagnostics.Trace.WriteLine("Adding dependency directory to PATH environment variable: ");
                    System.Diagnostics.Trace.WriteLine(myPath);
                    Environment.SetEnvironmentVariable("PATH", myPath + ";" + path);
                } else {
                    System.Diagnostics.Trace.WriteLine("Dependency directory '" + myPath + "' already contained in environment variable PATH. Skipping PATH modification.");
                }
            } catch (System.Security.SecurityException exc) {
                System.Diagnostics.Trace.Write("(Permission) error while determining native dependency location /modifying PATH variable: " + exc.ToString());
            }
            #endregion

            #region initialize proc specific interfaces
            // for now we check for the existance of the Intel MKL
            // make sure that Globals is init first
            double dummy = Globals.eps + 1; 
            try {
                using (Scope.Enter()) {
                    if (!MKLFFT.TryCreate(out var mkl)) {
                        System.Diagnostics.Trace.WriteLine($"ILNumerics.ONAL: MKL was not found."); 
                        return; 
                    }
                    mkl.FFTForward1D( vector<float>(1.0f, 0f, 0f, 0f), 0).Dispose(); // fails if no MKL present
                    LapackMKL10_0 lapack = new LapackMKL10_0();
                    // set Lapack and FFTImplementation only now! Otherwise other threads may already try to use it without failsafety
                    FFTImplementation = mkl;
                    Lapack = lapack;
                }
                System.Diagnostics.Trace.WriteLine($"ILNumerics.ONAL is initialized.");
                return; 
            } catch (System.Security.SecurityException exc) {
                System.Diagnostics.Trace.WriteLine("Error initializing MKL: " + exc.ToString());
                // MKL missing ... :| 
                // well, our matmult works blocked and is not a bad substitute, at least 
            } catch (DllNotFoundException) {
                System.Diagnostics.Trace.WriteLine("No native MKL was found. Using managed LAPACK implementation. In order to use optimized LAPACK, add ILNumerics.Core.Native package from nuget!");
                // MKL missing ... :| 
                // well, our matmult works blocked and is not a bad substitute, at least 
            } catch (BadImageFormatException exc) {
                if (!Environment.Is64BitProcess) {
                    System.Diagnostics.Trace.WriteLine("MKL was found to be incompatible. Consider targeting the 64 bit platform!?\r\n" + exc.ToString());
                } else {
                    System.Diagnostics.Trace.WriteLine("MKL was found to be incompatible.\r\n" + exc.ToString());
                }
            }
            // failure initializing... 
            FFTImplementation = null;
            Lapack = null; 
            #endregion

            // machine parameters have moved to ILNumerics.Globals.
        }
        #endregion


    }
}
