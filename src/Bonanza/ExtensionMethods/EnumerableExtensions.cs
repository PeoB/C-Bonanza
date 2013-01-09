using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtensionMethods
{
    public static class EnumerableExtensions
    {
         public static void ForEach<T>(this IEnumerable<T> self,Action<T> action )
         {
             foreach (var item in self)
             {
                 action(item);
             }
         }
         public static void ForEach<T>(this IEnumerable<T> self, Action<T,int> action)
         {
             var i=0;
             foreach (var item in self)
             {
                 action(item,i++);
             }
         }

        public static IEnumerable<T> Append<T>(this IEnumerable<T> enumerable,params T[] value)
        {
            return enumerable.Concat(value);
        }

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> enumerable, params T[] value)
        {
            return value.Concat(enumerable);
        } 
 
    }
}