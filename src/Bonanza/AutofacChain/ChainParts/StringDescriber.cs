namespace AutofacChain.ChainParts
{
    public class StringDescriber:IDescibe
    {
        private readonly IDescibe _chainPart;

        public StringDescriber(IDescibe chainPart)
        {
            _chainPart = chainPart;
        }
        public string WhatIsIt(object obj)
        {
            if (!(obj is string))
                return _chainPart.WhatIsIt(obj);

            return "That would be the a string...";
        }
    }
}