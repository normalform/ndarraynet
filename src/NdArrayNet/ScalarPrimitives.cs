// <copyright file="ScalarPrimitives.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//
// 1. Redistributions of source code must retain the above copyright notice, this
//    list of conditions and the following disclaimer.
// 2. Redistributions in binary form must reproduce the above copyright notice,
//    this list of conditions and the following disclaimer in the documentation
//    and/or other materials provided with the distribution.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED.IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
// The views and conclusions contained in the software and documentation are those
// of the authors and should not be interpreted as representing official policies,
// either expressed or implied, of the NdArrayNet project.
// </copyright>

namespace NdArrayNet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    internal class ScalarPrimitives<T, TC>
    {
        private static readonly ParameterExpression A = Expression.Parameter(typeof(T), "a");
        private static readonly ParameterExpression B = Expression.Parameter(typeof(T), "b");
        private static readonly ParameterExpression C = Expression.Parameter(typeof(TC), "c");
        private static readonly ParameterExpression Cond = Expression.Parameter(typeof(bool), "cond");

        private static readonly Func<T, T, T> AddFunc = TryBinary("+", new[] { Expression.Lambda<Func<T, T, T>>(Expression.Add(A, B), A, B) });
        private static readonly Func<T, T, T> SubtractFunc = TryBinary("-", new[] { Expression.Lambda<Func<T, T, T>>(Expression.Subtract(A, B), A, B) });
        private static readonly Func<T, T, T> MultiplyFunc = TryBinary("*", new[] { Expression.Lambda<Func<T, T, T>>(Expression.Multiply(A, B), A, B) });
        private static readonly Func<T, T, T> DivideFunc = TryBinary("/", new[] { Expression.Lambda<Func<T, T, T>>(Expression.Divide(A, B), A, B) });

        // NOTE: The .NET Modulo expression has different behavior from the Python in special cases (e.g. 4 % -3)
        private static readonly Func<T, T, T> ModuloFunc = TryBinary("%", new[] { Expression.Lambda<Func<T, T, T>>(Expression.Modulo(A, B), A, B) });

        private static readonly Func<T, T, T> PowerFunc = TryBinary("Power", new[] {
            Expression.Lambda<Func<T, T, T>>(Expression.Convert(Expression.Power(Expression.Convert(A, typeof(double)), Expression.Convert(B, typeof(double))), typeof(T)), A, B)
        });

        private static readonly Func<T, T> AbsFunc = Expression.Lambda<Func<T, T>>(Expression.Call(typeof(Math).GetMethod("Abs", new[] { typeof(T) }), A), A).Compile();
        private static readonly Func<T, T> SignFunc = Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod("Sign", new[] { typeof(T) }), A), typeof(T)), A).Compile();
        private static readonly Func<T, T> LogFunc = Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod("Log", new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile();
        private static readonly Func<T, T> Log10Func = Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod("Log10", new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile();
        private static readonly Func<T, T> ExpFunc = Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod("Exp", new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile();
        private static readonly Func<T, T> SinFunc = Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod("Sin", new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile();
        private static readonly Func<T, T> CosFunc = Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod("Cos", new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile();
        private static readonly Func<T, T> TanFunc = Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod("Tan", new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile();
        private static readonly Func<T, T> AsinFunc = Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod("Asin", new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile();
        private static readonly Func<T, T> AcosFunc = Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod("Acos", new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile();
        private static readonly Func<T, T> AtanFunc = Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod("Atan", new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile();
        private static readonly Func<T, T> SinhFunc = Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod("Sinh", new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile();
        private static readonly Func<T, T> CoshFunc = Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod("Cosh", new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile();
        private static readonly Func<T, T> TanhFunc = Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod("Tanh", new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile();
        private static readonly Func<T, T> SqrtFunc = Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod("Sqrt", new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile();
        private static readonly Func<T, T> CeilingFunc = Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod("Ceiling", new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile();
        private static readonly Func<T, T> FloorFunc = Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod("Floor", new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile();
        private static readonly Func<T, T> RoundFunc = Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod("Round", new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile();
        private static readonly Func<T, T> TruncateFunc = Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Call(typeof(Math).GetMethod("Truncate", new[] { typeof(T) }), Expression.Convert(A, typeof(double))), typeof(T)), A).Compile();

        private static readonly Func<TC, T> ConvertFunc = Expression.Lambda<Func<TC, T>>(Expression.Convert(C, typeof(T)), C).Compile();

        public ScalarPrimitives()
        {
        }

        internal static Func<T, T, T> CompileAny(Expression<Func<T, T, T>>[] fns)
        {
            foreach (var fn in fns)
            {
                try
                {
                    var func = fn.Compile();
                    if (func != null)
                    {
                        return func;
                    }
                }
                catch (InvalidOperationException)
                {
                    // TODO: Maybe I don't need this try anc catch block.
                    throw;
                }
            }

            var msg = string.Format("cannot compile scalar primitive for type %s", typeof(T).Name);
            throw new InvalidOperationException(msg);
        }

        internal static Func<T, T, T> TryBinary(string op, Expression<Func<T, T, T>>[] fns)
        {
            var msg = string.Format("The type {0} does not implemented {1}", typeof(T).Name, op);
            var thrw = Expression.Throw(Expression.Constant(new InvalidOperationException(msg)));
            var errExpr = Expression.Lambda<Func<T, T, T>>(Expression.Block(thrw, A), A, B);

            var fnsWithExceptionBlock = fns.Concat(new[] { errExpr });
            return CompileAny(fnsWithExceptionBlock.ToArray());
        }

        public T Add(T a, T b) => AddFunc.Invoke(a, b);

        public T Subtract(T a, T b) => SubtractFunc.Invoke(a, b);

        public T Multiply(T a, T b) => MultiplyFunc.Invoke(a, b);

        public T Divide(T a, T b) => DivideFunc.Invoke(a, b);

        public T Modulo(T a, T b) => ModuloFunc.Invoke(a, b);

        public T Power(T a, T b) => PowerFunc.Invoke(a, b);

        public T Abs(T a) => AbsFunc.Invoke(a);

        public T Sign(T a) => SignFunc.Invoke(a);

        public T Log(T a) => LogFunc.Invoke(a);

        public T Log10(T a) => Log10Func.Invoke(a);

        public T Exp(T a) => ExpFunc.Invoke(a);

        public T Sin(T a) => SinFunc.Invoke(a);

        public T Cos(T a) => CosFunc.Invoke(a);

        public T Tan(T a) => TanFunc.Invoke(a);

        public T Asin(T a) => AsinFunc.Invoke(a);

        public T Acos(T a) => AcosFunc.Invoke(a);

        public T Atan(T a) => AtanFunc.Invoke(a);

        public T Sinh(T a) => SinhFunc.Invoke(a);

        public T Cosh(T a) => CoshFunc.Invoke(a);

        public T Tanh(T a) => TanhFunc.Invoke(a);

        public T Sqrt(T a) => SqrtFunc.Invoke(a);

        public T Ceiling(T a) => CeilingFunc.Invoke(a);

        public T Floor(T a) => FloorFunc.Invoke(a);

        public T Round(T a) => RoundFunc.Invoke(a);

        public T Truncate(T a) => TruncateFunc.Invoke(a);

        public T Convert(TC c) => ConvertFunc.Invoke(c);
    }

    internal class ScalarPrimitives
    {
        private static Dictionary<Tuple<Type, Type>, object> instances = new Dictionary<Tuple<Type, Type>, object>();

        public static ScalarPrimitives<T, TC> For<T, TC>()
        {
            lock (instances)
            {
                var types = Tuple.Create(typeof(T), typeof(TC));
                if (instances.ContainsKey(types))
                {
                    return instances[types] as ScalarPrimitives<T, TC>;
                }

                var sp = new ScalarPrimitives<T, TC>();
                instances.Add(types, sp);
                return sp;
            }
        }
    }
}