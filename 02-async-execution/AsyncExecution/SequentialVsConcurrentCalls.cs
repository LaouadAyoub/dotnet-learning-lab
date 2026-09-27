using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetLearningLab.AsyncExecution
{
    internal class SequentialVsConcurrentCalls
    {
        public static async Task Run()
        {
            //await RunSequentially();
            //await RunConcurrently();

            //await UseAwait();
            //UseResult();

            await UseWhenAll();
        }


        // CONCEPT:
        // Sequential vs concurrent asynchronous I/O
        //
        // WHAT I LEARNED:
        // Independent async operations can overlap if they are started
        // before I await their completion.
        //
        // WHEN TO USE:
        // Backend code that makes independent database, HTTP,
        // cache, or service calls.

        public static async Task RunSequentially()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            string user = await GetUserAsync();
            string orders = await GetOrdersAsync();

            stopwatch.Stop();

            Console.WriteLine($"Sequential: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"{user} | {orders}");
        }

        public static async Task RunConcurrently()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            Task<string> userTask = GetUserAsync();
            Task<string> ordersTask = GetOrdersAsync();

            string user = await userTask;
            string orders = await ordersTask;

            stopwatch.Stop();

            Console.WriteLine($"Concurrent: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"{user} | {orders}");
        }

        static async Task<string> GetUserAsync()
        {
            Console.WriteLine("Loading user...");

            await Task.Delay(1000);
            return "User loaded";
        }

        static async Task<string> GetOrdersAsync()
        {
            Console.WriteLine("Loading Order...");

            await Task.Delay(2000);
            return "Orders loaded";
        }

        // MIQ : What changes when I force a Task to give me its result synchronously instead of using await?

        // CONCEPT:
        // await vs .Result
        //
        // WHAT I LEARNED:
        // await can suspend the async method while the Task is incomplete.
        // .Result forces the current thread to wait for completion.
        //
        // WHEN TO USE:
        // In backend async code, prefer await instead of blocking
        // on asynchronous operations with .Result.
        public static async Task UseAwait()
        {
            Console.WriteLine("\n --- await ---");
            Console.WriteLine($"Before: Thread {Thread.CurrentThread.ManagedThreadId}");

            string user = await GetUserAsync();

            Console.WriteLine($"Result: {user}");
            Console.WriteLine($"After: Thread {Thread.CurrentThread.ManagedThreadId}");
        }

        public static void UseResult()
        {
            Console.WriteLine(" \n --- .Result -----");
            Console.WriteLine($"Before: Thread {Thread.CurrentThread.ManagedThreadId}");

            string user = GetUserAsync().Result;

            Console.WriteLine($"Result : {user}");
            Console.WriteLine($"After: Thread {Thread.CurrentThread.ManagedThreadId}");
        }

        //MIQ : If I start multiple independent async operations, how do I wait for all of them cleanly?
        // CONCEPT:
        // Task.WhenAll
        //
        // WHAT I LEARNED:
        // Independent async operations can start first,
        // then Task.WhenAll waits until all of them finish.
        //
        // WHEN TO USE:
        // Backend code that needs several independent
        // HTTP, database, cache, or service calls.

        static async Task UseWhenAll()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            Task<string> userTask = GetUserAsync();
            Task<string> orderTask = GetOrdersAsync();

            string[] results = await Task.WhenAll(userTask, orderTask);

            stopwatch.Stop();

            Console.WriteLine(results[0]);
            Console.WriteLine(results[1]);
            Console.WriteLine($"Total: {stopwatch.ElapsedMilliseconds}");
        }

    }
}
