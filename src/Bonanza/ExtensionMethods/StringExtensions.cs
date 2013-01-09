using System.Collections.Generic;
using System.Linq;

namespace ExtensionMethods
{
    public static class StringExtensions
    {
        public static string Join(this IEnumerable<string> enumerable,string seperator="")
        {
            return string.Join(seperator, enumerable.ToArray());
        }

        public static string PascalToSnakeCase(this string s)
        {
            return s.ToCharArray().Select(SnakedCaseString).Join();
        }

        private static string SnakedCaseString(char c, int index)
        {
            var prefix = char.IsUpper(c) && index > 0 ? "_" : "";
            return prefix + char.ToLower(c);
        }
    }
}