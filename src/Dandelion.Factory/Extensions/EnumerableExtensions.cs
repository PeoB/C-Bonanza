using System;
using System.Collections.Generic;
using System.Linq;

namespace Dandelion.Factory.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T, int> action)
        {
            var i = 0;
            foreach (var item in enumerable)
            {
                action(item, i);
                ++i;
            }
        }
        public static string Join(this IEnumerable<string> enumerable, string seperator)
        {
            return string.Join(seperator, enumerable.ToArray());
        }

        public static int IndexOf<T>(this IEnumerable<T> enumerable, Predicate<T> predicate)
        {
            var i = 0;
            foreach (var item in enumerable)
            {
                if (predicate(item))
                {
                    return i;
                }
                ++i;
            }
            return -1;
        }

        public static bool All<T>(this IEnumerable<T> enumerable, Func<T, int, bool> predicate)
        {
            return enumerable.Select((o, i) => new { o, i }).All(item => predicate(item.o, item.i));
        }
    }
}