using System;

namespace ExtensionMethods
{
    public static class GenericExtensions
    { 
        public static T Tap<T>(this T self, Action<T> action)
        {
            action(self);
            return self;
        }

    }
}