using System;
using System.Collections.Generic;

namespace ExtensionMethods
{
    public static class DictionaryExtensions
    {
         public static void SafeUpdate<T,T1>(this IDictionary<T,T1> dictionary,T key,Func<T1,T1> func,T1 defaultValue)
         {
             T1 value;
             if(!dictionary.TryGetValue(key, out value)) value = defaultValue;
             dictionary[key] = func(value);
         }

        public static T1 SafeGet<T,T1>(this IDictionary<T,T1> dictionary,T key,T1 defaultValue)
        {
            T1 value;
            return dictionary.TryGetValue(key, out value)
                       ? value
                       : defaultValue;
        }
    }
}