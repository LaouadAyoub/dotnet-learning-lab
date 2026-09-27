// CONCEPT:
// Exceptions

// WHAT I LEARNED:
// Exceptions interrupt execution when something cannot continue safely.

// WHEN TO USE:
// To stop unsafe execution paths and handle failures clearly.

internal static class ExceptionExperiments
{
    public static void Run()
    {
        //ParseInvalidNumber();
        CatchInvalidParsing();
        //CompareCatchVsFinally();
        CompareExpectedAndUnexpectedFailure();
        //ThrowExceptionForInvalidPaymentAmount();
        ReadExceptionInformation();
    }

    static void ParseInvalidNumber()
    {
        // WHAT I LEARNED:
        // Invalid parsing throws an exception and stops execution immediately.

        // WHEN TO USE:
        // Backend systems must detect invalid input instead of continuing with corrupted data.

        Console.WriteLine("Before parsing");

        int number = int.Parse("hello");

        Console.WriteLine("After parsing");
    }

    static void CatchInvalidParsing()
    {
        // WHAT I LEARNED:
        // try/catch prevents the application from crashing completely.

        // WHEN TO USE:
        // Backend systems catch failures to respond safely instead of terminating execution.

        try
        {
            Console.WriteLine("Before parsing");

            int number = int.Parse("hello");

            Console.WriteLine("After parsing");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid number format detected");
        }

        Console.WriteLine("Application continues running");
    }

    static void CompareCatchVsFinally()
    {
        // WHAT I LEARNED:
        // finally executes as control leaves the try/catch, including when catch throws.

        // WHEN TO USE:
        // Backend systems use finally for cleanup during normal exception unwinding.
        // Abrupt process termination is not covered by this guarantee.
        // "Method finished" is not printed, and that's why we use Finally (It should be executed Without Debugging (Ctrl + F5))

        try
        {
            Console.WriteLine("Try started");

            int.Parse("hello");
        }
        catch (FormatException)
        {
            Console.WriteLine("Catch block started");

            throw new Exception("New exception inside catch");
        }
        finally
        {
            Console.WriteLine("Finally block executed");
        }

        Console.WriteLine("Method finished");
    }

    static void CompareExpectedAndUnexpectedFailure()
    {
        // WHAT I LEARNED:
        // Not all failures should be treated the same way.

        // WHEN TO USE:
        // Backend systems distinguish between normal business situations
        // and real system problems.

        Console.WriteLine("=== Experiment : CompareExpectedAndUnexpectedFailure ===");

        User? foundUser = FindUserById(99); // User not found, normal business reality, no need to throw and exception

        if (foundUser == null)
        {
            Console.WriteLine("Expected failure: user not found");
        }

        try
        {
            SimulateDatabaseFailure(); // Now the SYSTEM itself is unstable.
        }
        catch (Exception)
        {
            Console.WriteLine("Unexpected failure: database connection lost");
        }
    }

    static void ThrowExceptionForInvalidPaymentAmount()
    {
        // WHAT I LEARNED:
        // Developers can intentionally stop unsafe execution paths.

        // WHEN TO USE:
        // Backend systems protect business rules and data integrity.

        decimal paymentAmount = -150;

        Console.WriteLine("Starting payment processing");

        if (paymentAmount <= 0)
        {
            throw new Exception("Payment amount must be greater than zero");
        }

        Console.WriteLine("Payment processed successfully");
    }

    static void ReadExceptionInformation()
    {
        // What I learned:
        // Exceptions contain diagnostic information.

        // When To use:
        // Backend engineers inspect exceptions to understand failures clearly

        try
        {
            Console.WriteLine("\n === Experiment : ReadExceptionInformation ===");
            int.Parse("hello");
        }
        catch (FormatException exception)
        {
            Console.WriteLine($"Message: {exception.Message}");

            Console.WriteLine();

            Console.WriteLine($"Type: {exception.GetType().Name}");

            Console.WriteLine();

            Console.WriteLine("Stack trace");
            Console.WriteLine(exception.StackTrace);
        }
    }

    static User? FindUserById(int userId)
    {
        List<User> users =
        [
            new User(1, "Walter White"),
            new User(2, "Jesse Pinkman")
        ];

        return users.FirstOrDefault(user => user.Id == userId);
    }

    static void SimulateDatabaseFailure()
    {
        throw new Exception("Database unavailable");
    }

    record User(int Id, string Name);
}
