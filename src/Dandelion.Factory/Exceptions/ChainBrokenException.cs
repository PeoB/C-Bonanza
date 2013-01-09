using System;

namespace Dandelion.Factory.Exceptions
{
    public class ChainBrokenException : Exception
    {
        public ChainBrokenException() : base("No chain possible") { }
    }
}