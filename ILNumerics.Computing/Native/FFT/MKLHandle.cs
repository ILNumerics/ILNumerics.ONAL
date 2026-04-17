using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Native {
    internal class MKLHandle : SafeHandleZeroOrMinusOneIsInvalid {
        private MKLHandle() : base(true) { }

        
        internal MKLHandle(IntPtr handle) : base(true) {
            base.handle = handle;
        }
        protected override bool ReleaseHandle() {
            if (!IsInvalid) {
                MKLImports.DftiFreeDescriptor(ref handle);
                handle = IntPtr.Zero;
                return true;
            }
            return false;
        }
    }
}
