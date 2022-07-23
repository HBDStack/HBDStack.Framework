namespace HBDStack.Framework.Extensions;

/// <summary>
/// A helper class to ensure the method is run once only
/// </summary>
public static class Runner
{
    private static readonly HashSet<string> Cache = new();

    public static void Once(Action action)
    {
        var key = $"{action.Method.Name}-{action.Method.GetHashCode()}";
        if (Cache.Contains(key)) return;
     
        action.Invoke();
        Cache.Add(key);
    }

    public static async Task Once(Func<Task> action)
    {
        var key = $"{action.Method.Name}-{action.Method.GetHashCode()}";
        if (Cache.Contains(key)) return;

        await action.Invoke();
        Cache.Add(key);
    }
}