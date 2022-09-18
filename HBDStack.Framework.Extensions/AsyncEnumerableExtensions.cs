// ReSharper disable once CheckNamespace
namespace System.Collections.Generic;

public static class AsyncEnumerableExtensions
{
    public static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> enumerable)
    {
        var list = new List<T>();
        await foreach(var item in enumerable)
            list.Add(item);
        return list;
    }
}