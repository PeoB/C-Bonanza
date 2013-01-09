namespace AutofacChain.ChainParts
{
    public class IntDescriber:IDescibe
    {
        private readonly IDescibe _descibe;

        public IntDescriber(IDescibe descibe)
        {
            _descibe = descibe;
        }


        public string WhatIsIt(object obj)
        {
            if (!(obj is int))
                return _descibe.WhatIsIt(obj);

            return "That my friend is a basic Integer";
        }
    }
}