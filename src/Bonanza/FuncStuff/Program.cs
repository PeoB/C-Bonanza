using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncStuff
{
    class Program
    {
        static void Main()
        {
            ExamplePartial();
            ExampleCurry();
        }

        private static void ExampleCurry()
        {
            var curried = Functional.Curry<int, int, int>(Add);
            var addWith1 = curried(1);
            Console.WriteLine(addWith1(2));

            Func<int, int, int> add = Add;
            curried = add.Curry();
            Console.WriteLine(curried(2)(3));
        }

        static void ExamplePartial()
        {
            var addableFunc = Functional.Bind<int, int, int>(Add, 1);
            Console.WriteLine(addableFunc(1));

            Func<int, int, int> add = Add;
            addableFunc = add.Bind(2);
            Console.WriteLine(addableFunc(1));


            addableFunc = Without<int>.Bind(Add, 3);
            Console.WriteLine(addableFunc(1));
        }

        static int Add(int x, int y)
        {
            return x + y;
        }
    }
}
