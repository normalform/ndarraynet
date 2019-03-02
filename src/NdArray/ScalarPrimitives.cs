// <copyright file="ScalarPrimitives.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    internal class ScalarPrimitives<T, TC>
    {
        private static readonly ParameterExpression A = Expression.Parameter(typeof(T), "a");
        private static readonly ParameterExpression B = Expression.Parameter(typeof(T), "b");
        private static readonly ParameterExpression C = Expression.Parameter(typeof(TC), "c");

        private static readonly Lazy<Func<T, T, T>> AddFunc = new Lazy<Func<T, T, T>>(() => TryBinary("+", new[] { Expression.Lambda<Func<T, T, T>>(Expression.Add(A, B), A, B) }));
        private static readonly Lazy<Func<T, T, T>> SubtractFunc = new Lazy<Func<T, T, T>>(() => TryBinary("-", new[] { Expression.Lambda<Func<T, T, T>>(Expression.Subtract(A, B), A, B) }));
        private static readonly Lazy<Func<T, T, T>> MultiplyFunc = new Lazy<Func<T, T, T>>(() => TryBinary("*", new[] { Expression.Lambda<Func<T, T, T>>(Expression.Multiply(A, B), A, B) }));
        private static readonly Lazy<Func<T, T, T>> DivideFunc = new Lazy<Func<T, T, T>>(() => TryBinary("/", new[] { Expression.Lambda<Func<T, T, T>>(Expression.Divide(A, B), A, B) }));

        // NOTE: The .NET Modulo expression has different behavior from the Python in special cases (e.g. 4 % -3)
        private static readonly Lazy<Func<T, T, T>> ModuloFunc = new Lazy<Func<T, T, T>>(() => TryBinary("%", new[] { Expression.Lambda<Func<T, T, T>>(Expression.Modulo(A, B), A, B) }));

        private static readonly Lazy<Func<T, T>> UnaryPlusFunc = new Lazy<Func<T, T>>(() => TryUnary("~+", new[] { Expression.Lambda<Func<T, T>>(Expression.UnaryPlus(A), A) }));
        private static readonly Lazy<Func<T, T>> UnaryMinusFunc = new Lazy<Func<T, T>>(() => TryUnary("~-", new[] { Expression.Lambda<Func<T, T>>(Expression.Negate(A), A) }));

        private static readonly Lazy<Func<T, T, bool>> EqualFunc = new Lazy<Func<T, T, bool>>(() => TryCompare("==", new[] { Expression.Lambda<Func<T, T, bool>>(Expression.Equal(A, B), A, B), Expression.Lambda<Func<T, T, bool>>(Expression.Call(A, typeof(IEquatable<T>).GetMethod(nameof(IEquatable<T>.Equals), new[] { typeof(T) }), B), A, B) }));
        private static readonly Lazy<Func<T, T, bool>> NotEqualFunc = new Lazy<Func<T, T, bool>>(() => TryCompare("!=", new[] { Expression.Lambda<Func<T, T, bool>>(Expression.NotEqual(A, B), A, B), Expression.Lambda<Func<T, T, bool>>(Expression.IsFalse(Expression.Call(A, typeof(IEquatable<T>).GetMethod(nameof(IEquatable<T>.Equals), new[] { typeof(T) }), B)), A, B) }));
        private static readonly Lazy<Func<T, T, bool>> LessFunc = new Lazy<Func<T, T, bool>>(() => TryCompare("<", new[] { Expression.Lambda<Func<T, T, bool>>(Expression.LessThan(A, B), A, B), Expression.Lambda<Func<T, T, bool>>(Expression.LessThan(Expression.Call(A, typeof(IComparable<T>).GetMethod(nameof(IComparable<T>.CompareTo), new[] { typeof(T) }), B), Expression.Constant(0)), A, B) }));
        private static readonly Lazy<Func<T, T, bool>> LessOrEqualFunc = new Lazy<Func<T, T, bool>>(() => TryCompare("<=", new[] { Expression.Lambda<Func<T, T, bool>>(Expression.LessThanOrEqual(A, B), A, B), Expression.Lambda<Func<T, T, bool>>(Expression.LessThanOrEqual(Expression.Call(A, typeof(IComparable<T>).GetMethod(nameof(IComparable<T>.CompareTo), new[] { typeof(T) }), B), Expression.Constant(0)), A, B) }));
        private static readonly Lazy<Func<T, T, bool>> GreaterFunc = new Lazy<Func<T, T, bool>>(() => TryCompare(">", new[] { Expression.Lambda<Func<T, T, bool>>(Expression.GreaterThan(A, B), A, B), Expression.Lambda<Func<T, T, bool>>(Expression.GreaterThan(Expression.Call(A, typeof(IComparable<T>).GetMethod(nameof(IComparable<T>.CompareTo), new[] { typeof(T) }), B), Expression.Constant(0)), A, B) }));
        private static readonly Lazy<Func<T, T, bool>> GreaterOrEqualFunc = new Lazy<Func<T, T, bool>>(() => TryCompare(">=", new[] { Expression.Lambda<Func<T, T, bool>>(Expression.GreaterThanOrEqual(A, B), A, B), Expression.Lambda<Func<T, T, bool>>(Expression.GreaterThanOrEqual(Expression.Call(A, typeof(IComparable<T>).GetMethod(nameof(IComparable<T>.CompareTo), new[] { typeof(T) }), B), Expression.Constant(0)), A, B) }));

        private static readonly Lazy<Func<T, T, T>> PowerFunc = new Lazy<Func<T, T, T>>(() => TryBinary("Power", new[] { Expression.Lambda<Func<T, T, T>>(Expression.Convert(Expression.Power(Expression.Convert(A, typeof(double)), Expression.Convert(B, typeof(double))), typeof(T)), A, B) }));

        private static readonly Lazy<Func<T, T, T>> MaximumFunc = new Lazy<Func<T, T, T>>(() => TryBinary("Maximum", new[] { Expression.Lambda<Func<T, T, T>>(Expression.Call(typeof(Math).GetMethod(nameof(Math.Max), new[] { typeof(T), typeof(T) }), A, B), A, B) }));
        private static readonly Lazy<Func<T, T, T>> MinimumFunc = new Lazy<Func<T, T, T>>(() => TryBinary("Minimum", new[] { Expression.Lambda<Func<T, T, T>>(Expression.Call(typeof(Math).GetMethod(nameof(Math.Min), new[] { typeof(T), typeof(T) }), A, B), A, B) }));

        private static readonly Lazy<Func<T, T>> AbsFunc = new Lazy<Func<T, T>>(() => Expression.Lambda<Func<T, T>>(Expression.Call(typeof(Math).GetMethod(nameof(Math.Abs), new[] { typeof(T) }), A), A).Compile());
        private static readonly Lazy<Func<T, T>> SignFunc = new Lazy<Func<T, T>>(() => Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod(nameof(Math.Sign), new[] { typeof(T) }), A), typeof(T)), A).Compile());
        private static readonly Lazy<Func<T, T>> LogFunc = new Lazy<Func<T, T>>(() => Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod(nameof(Math.Log), new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile());
        private static readonly Lazy<Func<T, T>> Log10Func = new Lazy<Func<T, T>>(() => Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod(nameof(Math.Log10), new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile());
        private static readonly Lazy<Func<T, T>> ExpFunc = new Lazy<Func<T, T>>(() => Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod(nameof(Math.Exp), new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile());
        private static readonly Lazy<Func<T, T>> SinFunc = new Lazy<Func<T, T>>(() => Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod(nameof(Math.Sin), new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile());
        private static readonly Lazy<Func<T, T>> CosFunc = new Lazy<Func<T, T>>(() => Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod(nameof(Math.Cos), new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile());
        private static readonly Lazy<Func<T, T>> TanFunc = new Lazy<Func<T, T>>(() => Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod(nameof(Math.Tan), new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile());
        private static readonly Lazy<Func<T, T>> AsinFunc = new Lazy<Func<T, T>>(() => Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod(nameof(Math.Asin), new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile());
        private static readonly Lazy<Func<T, T>> AcosFunc = new Lazy<Func<T, T>>(() => Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod(nameof(Math.Acos), new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile());
        private static readonly Lazy<Func<T, T>> AtanFunc = new Lazy<Func<T, T>>(() => Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod(nameof(Math.Atan), new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile());
        private static readonly Lazy<Func<T, T>> SinhFunc = new Lazy<Func<T, T>>(() => Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod(nameof(Math.Sinh), new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile());
        private static readonly Lazy<Func<T, T>> CoshFunc = new Lazy<Func<T, T>>(() => Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod(nameof(Math.Cosh), new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile());
        private static readonly Lazy<Func<T, T>> TanhFunc = new Lazy<Func<T, T>>(() => Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod(nameof(Math.Tanh), new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile());
        private static readonly Lazy<Func<T, T>> SqrtFunc = new Lazy<Func<T, T>>(() => Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod(nameof(Math.Sqrt), new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile());
        private static readonly Lazy<Func<T, T>> CeilingFunc = new Lazy<Func<T, T>>(() => Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod(nameof(Math.Ceiling), new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile());
        private static readonly Lazy<Func<T, T>> FloorFunc = new Lazy<Func<T, T>>(() => Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod(nameof(Math.Floor), new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile());
        private static readonly Lazy<Func<T, T>> RoundFunc = new Lazy<Func<T, T>>(() => Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod(nameof(Math.Round), new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile());
        private static readonly Lazy<Func<T, T>> TruncateFunc = new Lazy<Func<T, T>>(() => Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod(nameof(Math.Truncate), new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile());

        private static readonly Lazy<Func<TC, T>> ConvertFunc = new Lazy<Func<TC, T>>(() => Expression.Lambda<Func<TC, T>>(Expression.Convert(C, typeof(T)), C).Compile());

        public ScalarPrimitives()
        {
        }

        public T Add(T a, T b) => AddFunc.Value(a, b);

        public T Subtract(T a, T b) => SubtractFunc.Value(a, b);

        public T Multiply(T a, T b) => MultiplyFunc.Value(a, b);

        public T Divide(T a, T b) => DivideFunc.Value(a, b);

        public T Modulo(T a, T b) => ModuloFunc.Value(a, b);

        public T Power(T a, T b) => PowerFunc.Value(a, b);

        public T Abs(T a) => AbsFunc.Value(a);

        public T Sign(T a) => SignFunc.Value(a);

        public T Log(T a) => LogFunc.Value(a);

        public T Log10(T a) => Log10Func.Value(a);

        public T Exp(T a) => ExpFunc.Value(a);

        public T Sin(T a) => SinFunc.Value(a);

        public T Cos(T a) => CosFunc.Value(a);

        public T Tan(T a) => TanFunc.Value(a);

        public T Asin(T a) => AsinFunc.Value(a);

        public T Acos(T a) => AcosFunc.Value(a);

        public T Atan(T a) => AtanFunc.Value(a);

        public T Sinh(T a) => SinhFunc.Value(a);

        public T Cosh(T a) => CoshFunc.Value(a);

        public T Tanh(T a) => TanhFunc.Value(a);

        public T Sqrt(T a) => SqrtFunc.Value(a);

        public T Ceiling(T a) => CeilingFunc.Value(a);

        public T Floor(T a) => FloorFunc.Value(a);

        public T Maximum(T a, T b) => MaximumFunc.Value(a, b);

        public T Minimum(T a, T b) => MinimumFunc.Value(a, b);

        public T Round(T a) => RoundFunc.Value(a);

        public T Truncate(T a) => TruncateFunc.Value(a);

        public T Convert(TC c) => ConvertFunc.Value(c);

        public T UnaryPlus(T a) => UnaryPlusFunc.Value(a);

        public T UnaryMinus(T a) => UnaryMinusFunc.Value(a);

        public bool Equal(T a, T b) => EqualFunc.Value(a, b);

        public bool NotEqual(T a, T b) => NotEqualFunc.Value(a, b);

        public bool Less(T a, T b) => LessFunc.Value(a, b);

        public bool LessOrEqual(T a, T b) => LessOrEqualFunc.Value(a, b);

        public bool Greater(T a, T b) => GreaterFunc.Value(a, b);

        public bool GreaterOrEqual(T a, T b) => GreaterOrEqualFunc.Value(a, b);

        public bool IsFinite(T v) => IsFiniteFunc(v);

        internal static Func<T, T> CompileAny(Expression<Func<T, T>>[] fns)
        {
            foreach (var fn in fns)
            {
                var func = fn.Compile();
                if (func != null)
                {
                    return func;
                }
            }

            var errorMessage = string.Format("Cannot compile scalar primitive for type {0}", typeof(T).Name);
            throw new InvalidOperationException(errorMessage);
        }

        internal static Func<T, T, T> CompileAny(Expression<Func<T, T, T>>[] fns)
        {
            foreach (var fn in fns)
            {
                var func = fn.Compile();
                if (func != null)
                {
                    return func;
                }
            }

            var errorMessage = string.Format("Cannot compile scalar primitive for type {0}", typeof(T).Name);
            throw new InvalidOperationException(errorMessage);
        }

        internal static Func<T, T, bool> CompileAny(Expression<Func<T, T, bool>>[] fns)
        {
            foreach (var fn in fns)
            {
                var func = fn.Compile();
                if (func != null)
                {
                    return func;
                }
            }

            var errorMessage = string.Format("cannot compile scalar primitive for type {0}", typeof(T).Name);
            throw new InvalidOperationException(errorMessage);
        }

        internal static Func<T, T> TryUnary(string op, Expression<Func<T, T>>[] fns)
        {
            var msg = string.Format("The type {0} does not implemented {1}", typeof(T).Name, op);
            var thrw = Expression.Throw(Expression.Constant(new InvalidOperationException(msg)));
            var errExpr = Expression.Lambda<Func<T, T>>(Expression.Block(thrw, A), A);

            var fnsWithExceptionBlock = fns.Concat(new[] { errExpr });
            return CompileAny(fnsWithExceptionBlock.ToArray());
        }

        internal static Func<T, T, T> TryBinary(string op, Expression<Func<T, T, T>>[] fns)
        {
            var msg = string.Format("The type {0} does not implemented {1}", typeof(T).Name, op);
            var thrw = Expression.Throw(Expression.Constant(new InvalidOperationException(msg)));
            var errExpr = Expression.Lambda<Func<T, T, T>>(Expression.Block(thrw, A), A, B);

            var fnsWithExceptionBlock = fns.Concat(new[] { errExpr });
            return CompileAny(fnsWithExceptionBlock.ToArray());
        }

        internal static Func<T, T, bool> TryCompare(string op, Expression<Func<T, T, bool>>[] fns)
        {
            var msg = string.Format("The type {0} does not implemented {1}", typeof(T).Name, op);
            var thrw = Expression.Throw(Expression.Constant(new InvalidOperationException(msg)));
            var errExpr = Expression.Lambda<Func<T, T, bool>>(Expression.Block(thrw, Expression.Constant(false)), A, B);

            var fnsWithExceptionBlock = fns.Concat(new[] { errExpr });
            return CompileAny(fnsWithExceptionBlock.ToArray());
        }

        internal static bool IsFiniteFunc(T v)
        {
            var type = typeof(T);
            if (type == typeof(float))
            {
                var val = System.Convert.ToSingle(v);
                return !(float.IsInfinity(val) || float.IsNaN(val));
            }
            else if (type == typeof(double))
            {
                var val = System.Convert.ToDouble(v);
                return !(double.IsInfinity(val) || double.IsNaN(val));
            }

            return true;
        }
    }
}