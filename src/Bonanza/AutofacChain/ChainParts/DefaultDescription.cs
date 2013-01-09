namespace AutofacChain.ChainParts
{
    public class DefaultDescription:IDescibe
    {
        public string WhatIsIt(object obj)
        {
            return "Its an object with the name: " + obj.GetType().Name;
        }
    }
}