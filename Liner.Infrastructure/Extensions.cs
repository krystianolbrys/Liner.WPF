using System;
using System.Collections.Generic;

namespace Liner.Infrastructure
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerableSource, Action<T> action)
        {
            foreach (var item in enumerableSource)
            {
                action.Invoke(item);
            }
        }
    }
}
