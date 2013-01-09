using System;
using ExtensionMethods;

namespace Yield
{
    class Program
    {
        static void Main()
        {
            var list=new PrependedList<int>(1, -1) {2, 3, 4, 5, 6, 7, 10};
            list.ForEach(Console.WriteLine);
            list.Clear();

            list.ForEach(Console.WriteLine);
            new []{2, 3, 4, 5, 6, 7,-1, 10}.ForEach(list.Add);
            list.ForEach(Console.WriteLine);
        }
    }
}
