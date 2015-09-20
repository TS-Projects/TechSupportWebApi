using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.Services.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
        {
            if (values == null) return;

            foreach (var item in values)
            {
                action(item);
            }
        }

        public static async Task ForEachAsync<T>(this IEnumerable<T> values, Action<T> action)
        {
            var task = values
                .Select(item => Task.Run(() => action(item)))
                .ToList();

            await Task.WhenAll(task);
        }

        public static async Task<IEnumerable<TResult>> ForEachAsync<T, TResult>(this IEnumerable<T> enumerable, Func<T, Task<TResult>> func)
        {
            var tasks = enumerable
                .Select(item => Task.Run(() => func(item)))
                .ToList();

            return await Task.WhenAll(tasks);
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> items)
        {
            return new HashSet<T>(items);
        } 
    }
}
