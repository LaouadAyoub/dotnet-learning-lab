// CONCEPT:
// Strings are reference types but immutable.

// WHAT I LEARNED:
// Changing a string does not mutate the existing object.
// A new string is created and the variable can point to the new one.

// WHEN TO USE:
// Use string normally for simple text values and small concatenations.

internal static class Strings
{
    public static void Run()
    {
        Experiment1_AssignmentAndImmutability();
        Console.WriteLine();

        Experiment2_ConcatenationCreatesNewString();
        Console.WriteLine();

        Experiment3_LoopWithStringConcatenation();
    }

    static void Experiment1_AssignmentAndImmutability()
    {
        Console.WriteLine("Experiment 1 - Assignment and immutability");

        string a = "Ayoub";
        string b = a;

        b += " Atlas";

        Console.WriteLine($"a = {a}");
        Console.WriteLine($"b = {b}");

        // Expected:
        // a = Ayoub
        // b = Ayoub Atlas
    }

    static void Experiment2_ConcatenationCreatesNewString()
    {
        Console.WriteLine("Experiment 2 - Concatenation creates a new string");

        string name = "Ayoub";
        Console.WriteLine($"Before: {name}");

        name = name + " Atlas";
        Console.WriteLine($"After: {name}");

        // Idea:
        // "Ayoub" was not modified.
        // A new string "Ayoub Atlas" was created.
    }

    static void Experiment3_LoopWithStringConcatenation()
    {
        Console.WriteLine("Experiment 3 - String concatenation in a loop");

        string result = "";

        for (int i = 0; i < 5; i++)
        {
            result += i;
            Console.WriteLine($"Step {i}: {result}");
        }

        // Idea:
        // Each += creates a new string.
        // Fine for small cases, expensive in big loops.
    }
}
