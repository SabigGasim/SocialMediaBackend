namespace SocialMediaBackend.Infrastructure.Helpers;

public static class ValueTaskHelper
{
    public static ValueTask WhenAll(IEnumerable<ValueTask> tasks)
    {
        ArgumentNullException.ThrowIfNull(tasks);

        if (tasks is ValueTask[] array)
        {
            if (array.Length == 0)
            {
                return ValueTask.CompletedTask;
            }

            return WhenAll(array);
        }

        if (tasks is ICollection<ValueTask> list)
        {
            if (list.Count == 0)
            {
                return ValueTask.CompletedTask;
            }

            return WhenAll(list);
        }

        array = tasks.ToArray();

        return WhenAll(array);
    }

    private static async ValueTask WhenAll(ICollection<ValueTask> tasks)
    {
        List<Exception>? exceptions = null;

        foreach (var task in tasks)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                exceptions ??= new(tasks.Count);
                exceptions.Add(ex);
            }
        }

        if (exceptions is not null)
            throw new AggregateException(exceptions);
    }

    private static async ValueTask WhenAll(ValueTask[] tasks)
    {
        List<Exception>? exceptions = null;

        for (var i = 0; i < tasks.Length; i++)
        {
            try
            {
                await tasks[i].ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                exceptions ??= new(tasks.Length);
                exceptions.Add(ex);
            }
        }

        if (exceptions is not null)
            throw new AggregateException(exceptions);
    }
}
