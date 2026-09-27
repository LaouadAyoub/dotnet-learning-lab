using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetLearningLab.LanguageCore
{
    internal class DelegateExperiments
    {
        public static void Run()
        {
            ValidateOrderUsingDelegate();
            ValidationOrderUsingDifferentRules();
            ValidateOrderUsingLambda();
            ValidateOrderUsingFunc();
            NotifyOrderUsingAction();
        }

        static void ValidateOrderUsingDelegate()
        {
            // WHAT I LEARNED:
            // A delegate variable can store any method
            // that matches its signature.
            //
            // WHEN TO USE:
            // Pass different business rules into the same backend process.

            Order order = new Order
            {
                Total = 0,
                IsPaid = true,
            };

            OrderValidationRule validationRule = IsPaidOrder;

            bool isValid = validationRule(order);

            Console.WriteLine($"Order valid: {isValid}");
        }

        static void ValidationOrderUsingDifferentRules()
        {
            // WHAT I LEARNED:
            // A method can receive behavior through a delegate parameter.
            //
            // WHEN TO USE:
            // When the workflow stays the same,
            // but the business rule can change.


            Order order = new Order
            {

                Total = 150,
                IsPaid = false,
            };

            ProcessOrder(order, HasPositiveTotal);
            ProcessOrder(order, IsPaidOrder);
        }

        static void ValidateOrderUsingLambda()
        {

            Order order = new Order
            {

                Total = 150,
                IsPaid = false,

            };

            ProcessOrder(order, orderToValidate => orderToValidate.Total > 0);
            ProcessOrder(order, orderToValidate => orderToValidate.IsPaid);

        }

        static void ValidateOrderUsingFunc()
        {
            Order order = new Order
            {
                Total = 150,
                IsPaid = false,
            };

            ProcessOrderUsingFunc(order, orderToValidate => orderToValidate.Total > 0);
            ProcessOrderUsingFunc(order, orderToValidate => orderToValidate.IsPaid);

        }

        //  🧪 Experiment 5: Action
        //MIQ
        //When I only want something to happen, why would I return a value?

        static void NotifyOrderUsingAction()
        {

            Order order = new Order
            {
                Total = 150,
                IsPaid = true,
            };

            ProcessOrderNotification(
                order,
                orderToNotify =>
                {
                    Console.WriteLine($"Notification sent for order ({orderToNotify.Total}€).");
                });
        }

        static void ProcessOrderNotification(Order order, Action<Order> notificationAction)
        {
            Console.WriteLine("Processing order...");

            notificationAction(order);

            Console.WriteLine("Process finished.");
        }

        static void ProcessOrderUsingFunc(Order order, Func<Order, bool> ValidationRule)
        {
            bool isValid = ValidationRule(order);

            if (!isValid)
            {
                Console.WriteLine("Order rejected.");
                return;
            }

            Console.WriteLine("Order processed.");
        }



        static void ProcessOrder(Order order, OrderValidationRule validationRule)
        {
            bool isValid = validationRule(order);

            if (!isValid)
            {
                Console.WriteLine("Order rejected.");
                return;
            }

            Console.WriteLine("Order processed.");
        }
        static bool HasPositiveTotal(Order order)
        {
            return order.Total > 0;
        }
        static bool IsPaidOrder(Order order)
        {
            return order.IsPaid;
        }
    }


    public delegate bool OrderValidationRule(Order order);

    public class Order
    {
        public decimal Total { get; set; }

        public bool IsPaid { get; set; }
    }
}
