using System;
using System.Collections.Generic;

namespace Features.Linq
{
    public static class MyLinq
    {
        public static double Count<T>(this IEnumerable<T> collection)
        {
            var count = 0;
            foreach (var item in collection)
            {
                count++;
            }
            return count;
        }
    }
}