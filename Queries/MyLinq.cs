using System;
using System.Collections.Generic;

namespace Queries
{
    public static class MyLinq
    {
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    // yield return will build a new data structure
                    // and immediatelly give it back to the
                    // calling code
                    yield return item;
                }
            }
        }
    }
}