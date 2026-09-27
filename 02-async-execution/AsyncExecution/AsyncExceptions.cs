using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetLearningLab.AsyncExecution
{
    internal class AsyncExceptions
    {

        public static async Task Run()
        {
            Task<string> userTask = GetUserAsync();

            Console.WriteLine("Task created");
            Console.WriteLine($"Status now: {userTask.Status}");

            await Task.Delay(2000);

            Console.WriteLine($"Status later: {userTask.Status}");
            Console.WriteLine($"Is faulted: {userTask.IsFaulted}");

            Console.WriteLine("Program continues.");
        }

        public static async Task RunWithAwait()
        {
            try
            {
                string user = await GetUserAsync();
                Console.WriteLine(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Caught: {ex.Message}");
            }

        }



        static async Task<string> GetUserAsync()
        {
            Console.WriteLine("Loading user...");

            await Task.Delay(1000);

            throw new Exception("User service failed");
        }

    }
}
