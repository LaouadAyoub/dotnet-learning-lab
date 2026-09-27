namespace DotNetLearningLab.OrderImportPipeline
{

    //Imagine the backend receives raw order lines:

    //ORD-101,rick @example.com,150
    //ORD-102, morty @example.com,0
    //ORD-101, rick @example.com,150
    //INVALID-LINE
    //ORD-103, WALTER @EXAMPLE.COM  ,220

    //Your program should:
    //raw strings
    //→ parse
    //→ validate
    //→ remove duplicates
    //→ filter
    //→ transform
    //→ report results

    //This gives us one natural arena for:

    //strings
    //collections
    //IEnumerable<T>
    //List<T>
    //HashSet<T>
    //    LINQ
    //generics
    //exceptions
    //delegates
    //Func
    //Action

    //That is a proper Spiral 1 closing exercise.

    // Backend model
    public class Order
    {
        public string Id { get; set; } = string.Empty;

        public string CustomerEmail { get; set; } = String.Empty;

        public decimal Total { get; set; }
    }

    internal class OrderImportPipeline
    {
        public static void Run()
        {
            // The input
            string[] rawOrderLines =
            {
                "ORD-101,rick@example.com,150",
                "ORD-102,morty@example.com,0",
                "ORD-101,rick@example.com,150",
                "INVALID-LINE",
                "ORD-103,  WALTER@EXAMPLE.COM  ,220"
            };

            // Collect valid orders
            List<Order> validOrders = new List<Order>();

            foreach (string line in rawOrderLines)
            {
                try
                {
                    Order order = ParseOrder(line);

                    validOrders.Add(order);
                }
                catch (FormatException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            // Remove Duplicates with Hashset
            HashSet<string> processedOrderIds =
                new HashSet<string>();

            List<Order> uniqueOrders = new List<Order>();

            foreach (Order order in validOrders)
            {
                bool wasAdded = processedOrderIds.Add(order.Id);

                if (wasAdded)
                {
                    uniqueOrders.Add(order);
                }
            }

            // Pass the validation Rule

            IEnumerable<Order> processableOrdersSorted = FilterOrders(uniqueOrders, order => order.Total > 0).OrderByDescending(order => order.Total);


            // Use LINQ for projection
            IEnumerable<string> orderSummaries = processableOrdersSorted.Select(order =>
                                                                            $"{order.Id} | {order.CustomerEmail} | {order.Total}euros");

            // Report the transformed results
            foreach (string orderSummary in orderSummaries)
            {
                Console.WriteLine(orderSummary);
            }

            Console.WriteLine("------------------------");

            // Use Action
            ProcessOrders(
            processableOrdersSorted,
            order =>
            {
                Console.WriteLine(
                    $"Processing {order.Id} for {order.Total}euros");
            });


        }


        // Use Action
        static void ProcessOrders(
            IEnumerable<Order> orders,
            Action<Order> processingAction)
        {
            foreach (Order order in orders)
            {
                processingAction(order);
            }
        }

        // Pass the validation Rule, can be replaced by Where
        static IEnumerable<Order> FilterOrders(
            IEnumerable<Order> orders,
            Func<Order, bool> validationRule)
        {
            List<Order> filteredOrders = new List<Order>();

            foreach (Order order in orders)
            {
                if (validationRule(order))
                {
                    filteredOrders.Add(order);
                }
            }

            return filteredOrders;
        }

        // Parsing strings
        static Order ParseOrder(string rawOrderLine)
        {
            Order order = new Order();

            string[] lines = rawOrderLine.Split(',');

            if (lines.Length != 3)
            {
                throw new FormatException($"Invalid order line: {rawOrderLine}");
            }


            return new Order
            {
                Id = lines[0].Trim(),
                CustomerEmail = lines[1].Trim().ToLowerInvariant(),
                Total = decimal.Parse(lines[2].Trim(), System.Globalization.CultureInfo.InvariantCulture)
            };
        }

    }
}
