using System;
namespace Lazy
{
    public class TheClass
    {
        private readonly Lazy<TheOtherClass> _otherClass;

        public TheClass(Lazy<TheOtherClass> otherClass)
        {
            _otherClass = otherClass;
        }

        public string Result()
        {
            return _otherClass.Value.SomeValue;
        }
    }
}