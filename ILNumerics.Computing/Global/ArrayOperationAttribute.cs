using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILNumerics.Core.Global {

    /// <summary>
    /// Attribute used to identify acceleratable operations on ILNumerics arrays. Used by ILNumerics.Accelerator.
    /// </summary>
    public class ArrayOperationAttribute : Attribute {

        string m_proxyTypeName; 

        public string ProxyTypeName {
            get {
                return m_proxyTypeName; 
            }
        }
        /// <summary>
        /// Create a new <see cref="ArrayOperationAttribute"/>.
        /// </summary>
        /// <param name="proxyTypeName">The name of a type providing extensive information about the operation.</param>
        public ArrayOperationAttribute(string proxyTypeName) {
            m_proxyTypeName = proxyTypeName; 
        }
    }
}
