using System;

namespace Expressions
{
    class Program
    {
        static void Main()
        {
            Bootstrap.DoIt();
            var viewModel=new ViewModel();
            viewModel.MayMoveOn.Wait();
            Console.WriteLine(viewModel.Content.Length);

        }



    }
}
