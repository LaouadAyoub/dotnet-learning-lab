using DotNetLearningLab.LanguageCore;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("=== Strings Experiments ===");
        Strings.Run();

        Console.WriteLine();
        Console.WriteLine("=== StringBuilder Experiments ===");
        StringBuilderExperiments.Run();

        Console.WriteLine();
        Console.WriteLine("=== Collections List Experiments ===");
        ListExperiments.Run();

        Console.WriteLine();
        Console.WriteLine("=== Dictionaries Experiments ===");
        Dictionaries.Run();

        Console.WriteLine();
        Console.WriteLine("=== Hashsets Experiments ===");
        HashSets.Run();

        Console.WriteLine();
        Console.WriteLine("=== EnumerableVsList Experiments ===");
        EnumerableVsList.Run();

        Console.WriteLine();
        Console.WriteLine("=== Linq Experiments ===");
        Linq.Run();

        Console.WriteLine();
        Console.WriteLine("=== Exceptions Experiments ===");
        ExceptionExperiments.Run();


        Console.WriteLine();
        Console.WriteLine("=== Generics Experiments ===");
        GenericExperiments.Run();


        Console.WriteLine();
        Console.WriteLine("=== Generics constraints Experiments ===");
        GenericConstraints.Run();

        Console.WriteLine();
        Console.WriteLine("=== Delegates Experiments ===");
        DelegateExperiments.Run();

    }
}
