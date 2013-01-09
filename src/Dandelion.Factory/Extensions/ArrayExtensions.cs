using System.Collections.Generic;

namespace Dandelion.Factory.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] Prepend<T>(this T[] items, T item)
        {
            var l = new List<T> { item };
            l.AddRange(items);
            return l.ToArray();
        }

        public static T[] Append<T>(this T[] items, T item)
        {
            var l = new List<T>(items) { item };
            return l.ToArray();
        }
    }
}