using System;

namespace FuncStuff
{
    public static class Functional
    {
        public static Func<T2, T3, TOut> Bind<T, T2, T3, TOut>(this Func<T, T2, T3, TOut> func, T arg)
        {
            return (arg2, arg3) => func(arg, arg2, arg3);
        }

        public static Func<T2, TOut> Bind<T, T2, TOut>(this Func<T, T2, TOut> func, T arg)
        {
            return arg2 => func(arg, arg2);
        }

        public static Func<TOut> Bind<T, TOut>(this Func<T, TOut> func, T arg)
        {
            return () => func(arg);
        }


        public static Func<T, Func<T2, TOut>> Curry<T, T2, TOut>(this Func<T, T2, TOut> func)
        {
            return func.Bind;
        }

        public static Func<T, Func<T2, Func<T3,TOut>>> Curry<T, T2, T3, TOut>(this Func<T, T2, T3, TOut> func)
        {
            return arg => func.Bind(arg).Curry();
        }
    }

    public static class Without<T2>
    {
        public static Func<T2, TOut> Bind<T, TOut>(Func<T, T2, TOut> func, T arg)
        {
            return func.Bind(arg);
        }
    }
}