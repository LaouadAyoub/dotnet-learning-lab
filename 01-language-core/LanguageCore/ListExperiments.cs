// CONCEPT:
// List<T> — dynamic, ordered, mutable reference-type collection of elements

// WHAT I LEARNED:
// - List stores multiple elements in insertion order
// - It grows automatically (no fixed size)
// - Elements are accessed by index (list[0], list[1], …)
// - It is mutable → elements can be changed without creating a new List
// - List is a reference type → assigning it to another variable shares the same object
// - Modifying the List through one variable affects all references to it (shared state)
// - Extracting a value (like int) gives a copy, not a reference to the List
// - Mutation = modifying the same List (shared changes)
// - Reassignment = pointing to a new List (isolated changes)
// - Key decision: mutate when you want to update the original data, create a new List when you want a separate version

// WHEN TO USE:
// - When you need to store and manage multiple items
// - When the number of elements can change
// - When order matters
// - When you need to modify elements dynamically (mutation)
// - When building or updating shared data (e.g., adding items to a list)
// - When transforming data without side effects → create a new List instead of mutating
// - Default choice for collections in backend code

internal static class ListExperiments
{
    public static void Run()
    {
        Experiment1_CreateListOfIntegers();
        Experiment2_AddElements();
        Experiment3_ModifyElements();
        Experiment4_SharedReference();
        Experiment5_MutationVsReassignment();
    }

    static void Experiment1_CreateListOfIntegers()
    {
        List<int> list = new List<int>();

        Console.WriteLine(list.Count);
    }

    static void Experiment2_AddElements()
    {
        List<int> list = new List<int>();

        list.Add(1);
        list.Add(2);
        list.Add(5);

        Console.WriteLine(list.Count);
        Console.WriteLine($"List[0] = {list[0]}");
        Console.WriteLine($"List[1] = {list[1]}");
        Console.WriteLine($"List[2] = {list[2]}");
    }

    static void Experiment3_ModifyElements()
    {
        List<int> list = new List<int>();

        list.Add(10);
        list.Add(20);
        list.Add(30);

        // Access a value
        int value = list[1];
        Console.WriteLine($"Before change → extracted value: {value}");

        // Modify the List
        list[1] = 99;

        Console.WriteLine($"After change → list[1]: {list[1]}");
        Console.WriteLine($"After change → extracted value: {value}");
    }

    static void Experiment4_SharedReference()
    {
        List<int> list1 = new List<int>();

        list1.Add(17);
        list1.Add(27);
        list1.Add(37);

        List<int> list2 = list1;

        list2.Add(77);

        Console.WriteLine($"list1 Count {list1.Count()}");

        Console.WriteLine($"list2 Count {list2.Count()}");
    }

    static void Experiment5_MutationVsReassignment()
    {
        var list1 = new List<int> { 1, 2, 3 };

        var list2 = list1;

        list2 = new List<int>();

        list2.Add(99);

        Console.WriteLine($"list1 Count {list1.Count()}");
        Console.WriteLine($"list2 Count {list2.Count()}");
    }
}
