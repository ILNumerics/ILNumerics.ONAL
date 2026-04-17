
using ILNumerics.Core.Misc;
using System;
using System.Linq.Expressions;

namespace ILNumerics {

    /// <summary>
    /// Static class defining useful constants, shortcuts, aliases and functions for working with ILNumerics arrays. 
    /// </summary>
    [Serializable]
    public class EndExpression : ILExpression {

        internal EndExpression() : base(Expression.Parameter(typeof(long), "end")) { } 
    }
}
