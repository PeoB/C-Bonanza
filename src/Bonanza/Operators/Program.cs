using System;

namespace Operators
{
    class Program
    {
        static void Main()
        {
            var foo = new Foo(7);
            var foo2 = new Foo(2);
            if (!foo) return;
            if (foo && foo2)
            {
                Console.WriteLine("All is well");
            }
            Console.WriteLine((foo & foo2).Value);
            Console.WriteLine((foo2 & TimeSpan.FromHours(1)).TotalSeconds);
        }
    }
}
