using NotificationBatchProcessor;

internal class Program
{
    static async Task Main()
    {
        await NotificationBatch.RunAsync();
    }
}
