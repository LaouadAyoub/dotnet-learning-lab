using System.Text;

// CONCEPT:
// StringBuilder is used when building text repeatedly.

// WHAT I LEARNED:
// StringBuilder avoids creating a new string on every append.
// It uses a mutable internal buffer and creates the final string at the end.

// WHEN TO USE:
// Use StringBuilder for loops, large text construction, logs, or repeated appends.

internal static class StringBuilderExperiments
{
    public static void Run()
    {
        Experiment1_BasicAppend();
        Console.WriteLine();

        Experiment2_LoopWithStringBuilder();
        Console.WriteLine();

        Experiment3_CompareMentalModel();
    }

    static void Experiment1_BasicAppend()
    {
        Console.WriteLine("Experiment 1 - Basic append");

        var sb = new StringBuilder();

        sb.Append("Ayoub");
        sb.Append(" ");
        sb.Append("Atlas");

        string result = sb.ToString();

        Console.WriteLine($"Result = {result}");
    }

    static void Experiment2_LoopWithStringBuilder()
    {
        Console.WriteLine("Experiment 2 - StringBuilder in a loop");

        var sb = new StringBuilder();

        for (int i = 0; i < 5; i++)
        {
            sb.Append(i);
            Console.WriteLine($"Step {i}: {sb.ToString()}");
        }

        string finalResult = sb.ToString();
        Console.WriteLine($"Final = {finalResult}");
    }

    static void Experiment3_CompareMentalModel()
    {
        Console.WriteLine("Experiment 3 - Compare mental model");

        string withString = "";
        withString += "A";
        withString += "B";
        withString += "C";

        var sb = new StringBuilder();
        sb.Append("A");
        sb.Append("B");
        sb.Append("C");

        Console.WriteLine($"string result        = {withString}");
        Console.WriteLine($"StringBuilder result = {sb.ToString()}");

        // Same visible result.
        // Different behavior internally:
        // string -> new object on each change
        // StringBuilder -> same builder grows, final string created at the end
    }
}
