using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AutofacChain.ChainParts;

namespace AutofacChain
{
    class Program
    {
        static void Main(string[] args)
        {
            var container=Bootstrap.BuildIt();
            var describes=container.Resolve<IDescibe>();

            Console.WriteLine(describes.WhatIsIt(1));
            Console.WriteLine(describes.WhatIsIt(""));
            Console.WriteLine(describes.WhatIsIt(new Program()));
        }
    }
}
