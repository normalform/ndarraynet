//Copyright(c) 2019, Jaeho Kim
//All rights reserved.

//Redistribution and use in source and binary forms, with or without
//modification, are permitted provided that the following conditions are met:

//1. Redistributions of source code must retain the above copyright notice, this
//   list of conditions and the following disclaimer.
//2. Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution.

//THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
//ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
//WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
//DISCLAIMED.IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
//ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
//(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
//LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
//ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
//(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

//The views and conclusions contained in the software and documentation are those
//of the authors and should not be interpreted as representing official policies,
//either expressed or implied, of the NdArrayNet project.

namespace NdArrayNet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    
    public class ScalarPrimitives<T, TC>
    {
        private static readonly ParameterExpression A = Expression.Parameter(typeof(T), "a");
        private static readonly ParameterExpression B = Expression.Parameter(typeof(T), "b");
        private static readonly ParameterExpression C = Expression.Parameter(typeof(TC), "c");
        private static readonly ParameterExpression Cond = Expression.Parameter(typeof(bool), "cond");

        public Func<T, T, T> AddFunc;
        public Func<T, T, T> SubtractFunc;
        public Func<T, T, T> MultiplyFunc;
        public Func<T, T, T> DivideFunc;
        public Func<TC, T> ConvertFunc;

        public T Add(T a, T b) => this.AddFunc.Invoke(a, b);

        public T Subtract(T a, T b) => this.SubtractFunc.Invoke(a, b);

        public T Multiply(T a, T b) => this.MultiplyFunc.Invoke(a, b);

        public T Divide(T a, T b) => this.DivideFunc.Invoke(a, b);

        public T Convert(TC c) => this.ConvertFunc.Invoke(c);

        public static Func<T, T, T> CompileAny(Expression<Func<T, T, T>>[] fns)
        {
            foreach (var fn in fns)
            {
                try
                {
                    var func = fn.Compile();
                    if(func != null)
                    {
                        return func;
                    }
                }
                catch(InvalidOperationException)
                {
                    throw;
                }
            }

            var msg = string.Format("cannot compile scalar primitive for type %s", typeof(T).Name);
            throw new InvalidOperationException(msg);
        }

        public static Func<T, T, T> TryBinary(string op, Expression<Func<T, T, T>>[] fns)
        {
            var msg = string.Format("The type {0} does not implemented {1}", typeof(T).Name, op);
            var thrw = Expression.Throw(Expression.Constant(new InvalidOperationException(msg)));
            var errExpr = Expression.Lambda<Func<T, T, T>>(Expression.Block(thrw, A), A, B);

            fns.Concat(new[] { errExpr });
            return CompileAny(fns);
        }

        public ScalarPrimitives()
        {
            this.AddFunc = TryBinary("+", new[] { Expression.Lambda<Func<T, T, T>>(Expression.Add(A, B), A, B) });
            this.SubtractFunc = TryBinary("-", new[] { Expression.Lambda<Func<T, T, T>>(Expression.Subtract(A, B), A, B) });
            this.MultiplyFunc = TryBinary("*", new[] { Expression.Lambda<Func<T, T, T>>(Expression.Multiply(A, B), A, B) });
            this.DivideFunc = TryBinary("/", new[] { Expression.Lambda<Func<T, T, T>>(Expression.Divide(A, B), A, B) });
            this.ConvertFunc = Expression.Lambda<Func<TC, T>>(Expression.Convert(C, typeof(T)), C).Compile();
        }
    }

    public class ScalarPrimitives
    {
        private static Dictionary<Tuple<Type, Type>, object> instances = new Dictionary<Tuple<Type, Type>, object>();

        public static ScalarPrimitives<T, TC> For<T, TC>()
        {
            lock(instances)
            {
                var types = Tuple.Create(typeof(T), typeof(TC));
                if(instances.ContainsKey(types))
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
