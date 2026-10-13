using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace NotificationBatchProcessor
{
    // CONCEPT:
    // Filtering customers and sending asynchronous notifications.
    //
    // WHAT I LEARNED:
    // Fill in after observing the experiment.
    // Sequential sending allow to send every notification separately and wait for it to finish, while Concurrent send them all together and wait for all of them, so it's faster
    // But in backend work, faster doesn't always mean better :
    /*    | Situation                                            | Better choice           | Why                                                              |
    | ---------------------------------------------------- | ----------------------- | ---------------------------------------------------------------- |
    | Send three independent notifications                 | Concurrent              | Overlap the provider waiting time                                |
    | Create an order, then send confirmation using its ID | Sequential              | The confirmation depends on the created order                    |
    | Send 10,000 notifications                            | ** Limited concurrency** | Keep several sends in progress without overwhelming the provider |*/

    // WHEN TO USE:
    // Backend jobs that notify a collection of customers.


    internal static class NotificationBatch
    {
        public static async Task RunAsync()
        {
            Console.WriteLine("Notification batch started.");

            List<Customer> customers = new List<Customer>
            {
                new Customer(1, "rick@example.com", true),
                new Customer(2, "", true),
                new Customer(3, "morty@example.com", true),
                new Customer(4, "walter@example.com", false),
                new Customer(5, "rue@example.com", true)

            };

            List<Customer> eligibleCustomers = FilterEligibleCustomers(customers);

            Console.WriteLine($"Eligible customers: {eligibleCustomers.Count}/{customers.Count}");

            foreach( Customer customer in eligibleCustomers )
            {
                Console.WriteLine($"{customer.Id} | {customer.Email}");
            }

            Console.WriteLine($"Customers received: {customers.Count}");

            //string result = await SendNotificationAsync(eligibleCustomers[0]);

            Stopwatch sw = Stopwatch.StartNew();

            //List<string> results = await SendSequentiallyAsync(eligibleCustomers);

            List<string> results = await SendConcurrentlyAsync(eligibleCustomers);

            sw.Stop();

            foreach (string result in results)
            {
                Console.WriteLine(result);
            }

            Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds} ms");
        }

        private static List<Customer> FilterEligibleCustomers(IEnumerable<Customer> customers)
        {
            return customers.Where(customer => customer.IsActive && !string.IsNullOrWhiteSpace(customer.Email)).ToList();
        }

        private static async Task<string> SendNotificationAsync(Customer customer)
        {
            Console.WriteLine($"Starting: customer {customer.Id}");

            await Task.Delay(1000);

            if (customer.Id == 3)
            {
                throw new TimeoutException(
                    $"Provider timed out for customer {customer.Id}.");
            }

            Console.WriteLine($"Completed: customer {customer.Id}");

            return $"SENT | customer {customer.Id} | {customer.Email}";
        }

        private static async Task<string> SendSafelyAsync(
            Customer customer)
        {
            try
            {
                return await SendNotificationAsync(customer);
            }
            catch (TimeoutException exception)
            {
                return $"FAILED | customer {customer.Id} | {exception.Message}";
            }
        }

        private static async Task<List<string>> SendSequentiallyAsync(IEnumerable<Customer> customers)
        {
            List<string> results = new List<string>();

            foreach (Customer customer in customers)
            {
                string result = await SendSafelyAsync(customer);

                results.Add(result);
            }

            return results;
        }

        private static async Task<List<string>> SendConcurrentlyAsync(IEnumerable<Customer> customers)
        {
            List<Task<string>> sendTasks = new List<Task<string>>();

            foreach (Customer customer in customers)
            {
                Task<string> sendTask = SendSafelyAsync(customer);

                sendTasks.Add(sendTask);
            }

            string[] results = await Task.WhenAll(sendTasks);

            return results.ToList();
        }

        internal class Customer
        {
            public int Id { get; }
            public string Email { get; }
            public bool IsActive { get; }

            public Customer(int id, string email, bool isActive)
            {
                Id = id;
                Email = email;
                IsActive = isActive;
            }
        }
    }
}
