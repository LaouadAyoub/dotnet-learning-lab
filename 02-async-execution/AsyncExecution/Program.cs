namespace DotNetLearningLab.AsyncExecution
{
    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            string experiment = args.Length == 0 ? "status" : args[0];
            switch (experiment)
            {
                case "status":
                    await AsyncExceptions.Run();
                    break;
                case "exceptions":
                    await AsyncExceptions.RunWithAwait();
                    break;
                case "sequential":
                    await SequentialVsConcurrentCalls.RunSequentially();
                    break;
                case "concurrent":
                    await SequentialVsConcurrentCalls.RunConcurrently();
                    break;
                case "when-all":
                    await SequentialVsConcurrentCalls.Run();
                    break;
                case "await":
                    await SequentialVsConcurrentCalls.UseAwait();
                    break;
                case "result":
                    SequentialVsConcurrentCalls.UseResult();
                    break;
                default:
                    Console.Error.WriteLine("Choose: status | exceptions | sequential | concurrent | when-all | await | result");
                    return 1;
            }
            return 0;
        }
    }
}
