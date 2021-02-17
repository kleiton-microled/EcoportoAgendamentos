using System;
using System.Collections.Generic;

namespace Ecoporto.AgendamentoTRA.Extensions
{
    public static class ListExtensions
    {
        public static void AddRange<T>(this IList<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }
}