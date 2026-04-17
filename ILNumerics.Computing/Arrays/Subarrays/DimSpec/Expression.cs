// ToDo: DOKU Missing all over for ILExpression.cs
#pragma warning disable 1591


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using ILNumerics.Core.StorageLayer;
using System.Security;
using System.Security.Permissions;
using System.Collections.Concurrent;
using System.Threading;

namespace ILNumerics.Core.Misc {

    [Serializable]
    public class ILExpression {
        //    : BaseStorage<Expression, Array<Expression>, InArray<Expression>, ILOutArray<Expression>, RetArray<Expression>, ILStorage<Expression>> {

        public static IDictionary<string, Func<long, long>> s_cache;

        public static IDictionary<string, Func<long, long>> Cache {
            get {
                if (s_cache == null) {
                    s_cache = new ConcurrentDictionary<string, Func<long, long>>();
                }
                return s_cache;
            }
        }
        Expression m_expression;

        public ILExpression(Expression exp) {
            m_expression = exp;
        }

        public Expression Expression {
            get { return m_expression; }
        }

        #region operator overload
        public static ILExpression operator -(ILExpression a, ILExpression b) {
            return new ILExpression(Expression.Subtract(a.Expression, b.Expression));
        }
        public static ILExpression operator +(ILExpression a, ILExpression b) {
            return new ILExpression(Expression.Add(a.Expression, b.Expression));
        }
        public static ILExpression operator *(ILExpression a, ILExpression b) {
            return new ILExpression(Expression.Multiply(a.Expression, b.Expression));
        }
        public static ILExpression operator /(ILExpression a, ILExpression b) {
            return new ILExpression(Expression.Divide(a.Expression, b.Expression));
        }

        public static ILExpression operator +(ILExpression expr, BaseArray a) {
            return new ILExpression(Expression.Add(expr.Expression, BA2Expr(a)));
        }
        public static ILExpression operator +(BaseArray a, ILExpression expr) {
            return new ILExpression(Expression.Add(BA2Expr(a), expr.Expression));
        }
        public static ILExpression operator -(ILExpression expr, BaseArray a) {
            return new ILExpression(Expression.Subtract(expr.Expression, BA2Expr(a)));
        }
        public static ILExpression operator -(BaseArray a, ILExpression expr) {
            return new ILExpression(Expression.Subtract(BA2Expr(a), expr.Expression));
        }

        public static ILExpression operator /(ILExpression expr, BaseArray a) {
            return new ILExpression(Expression.Divide(expr.Expression, BA2Expr(a)));
        }
        public static ILExpression operator /(BaseArray a, ILExpression expr) {
            return new ILExpression(Expression.Divide(BA2Expr(a), expr.Expression));
        }
        public static ILExpression operator *(ILExpression expr, BaseArray a) {
            return new ILExpression(Expression.Multiply(expr.Expression, BA2Expr(a)));
        }
        public static ILExpression operator *(BaseArray a, ILExpression expr) {
            return new ILExpression(Expression.Multiply(BA2Expr(a), expr.Expression));
        }
        #endregion

        #region helper
        private static Expression BA2Expr(BaseArray a) {
            if (object.Equals(a, null)) {
                throw new ArgumentNullException("null is not a valid value in the context of subarray range expressions.");
            }
            long valInt;
            try {
                Array<long> A = ILNumerics.Core.Functions.Builtin.MathInternal.convert<long>(a); // <- releases any RetT 
                if (!A.IsScalar) throw new ArgumentException("Only scalar arrays are allowed in subarray range expressions!");
                valInt = A.GetValue(0);
            } catch (InvalidCastException) {
                throw new ArgumentException("Only numeric arrays with an element type convertable to uint are allowed to be used in subarray range expressions.");
            }
            ConstantExpression ret = Expression.Constant(valInt);
            return ret;
        }
        #endregion

        internal static long Evaluate(Expression expression, long lastIndexValue) {
            string key = expression.ToString();
            long ret;
            if (Cache.ContainsKey(key)) {
                ret = Cache[key](lastIndexValue);
            } else {
                ParameterExpression endParameter = (ParameterExpression)Globals.end.Expression;
                Func<long, long> func = Expression.Lambda<Func<long, long>>(expression, endParameter).Compile();
                Cache[key] = func;
                ret = func(lastIndexValue);
            }
            if (ret < 0) {
                throw new IndexOutOfRangeException($"Expression evaluation with 'end' specifier resulted in a negative value: {ret}");
            }
            return ret;
        }

    }
}