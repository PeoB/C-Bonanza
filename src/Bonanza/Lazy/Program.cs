using System;
using Autofac;

namespace Lazy
{
    class Program
    {
        static void Main()
        {
            LazyAutoFac();
            SimpleLazy();
        }

        private static void SimpleLazy()
        {
            var lazy = Foo();
            Console.WriteLine(lazy.Value);
            Console.WriteLine(lazy.Value);
        }

        static void LazyAutoFac()
        {
            var container = Bootstrap.DoIt();
            var theClass = container.Resolve<Lazy<TheClass>>();
            Console.WriteLine(theClass.Value.Result());
        }
        static Lazy<int> Foo()
        {
            return new Lazy<int>(()=>new Random().Next());
        }
    }
}
