namespace SocialMediaBackend.BuildingBlocks.Infrastructure.Helpers;

public static class TaskHelpers
{
    public static async Task ProcessIndividually(
        this IEnumerable<Task> tasks,
        string? errorMessageContext = null)
    {
        await foreach (var task in Task.WhenEach(tasks))
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"{errorMessageContext ?? "Error handling task"}: {ex.Message}");
            }
        }
    }

    public static async Task ProcessIndividually<T>(
        this IEnumerable<Task<T>> tasks,
        Action<T> processResult,
        string? errorMessageContext = null)
    {
        await foreach (var task in Task.WhenEach(tasks))
        {
            try
            {
                var result = await task;
                processResult(result);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"{errorMessageContext ?? "Error handling task"}: {ex.Message}");
            }
        }
    }
}
