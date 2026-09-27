// CONCEPT:
// HashSet<T>
// A collection that stores unique values only.
// Internally uses hashing → fast existence checks.
// No key/value → the value itself is what is tracked.


// WHAT I LEARNED:
// - Adding a duplicate does nothing → it’s ignored (no error, no second copy)
// - The value itself acts like a “key” internally (that’s why it’s fast)
// - Contains() is the main operation → check if something is already there
// - Remove() lets me update the state (what is “seen” vs “not seen”)
// - Order is irrelevant → I should not rely on it
// - Hashing narrows a lookup, then equality checks find a match; collisions can add work.


// WHEN TO USE:
// - When I need to ensure uniqueness automatically
// - When I want to answer: “Have I seen this before?”
// - When I want fast existence checks (better than List.Contains)
// - When I track processed items, visited nodes, or deduplicated data
// - When I don’t care about order, only presence

namespace DotNetLearningLab.LanguageCore
{
    internal class HashSets
    {
        public static void Run()
        {
            Experiment1_AddAndDuplicate();
            Experiment2_CheckContains();
            Experiment3_Remove();
        }

        static void Experiment1_AddAndDuplicate()
        {
            HashSet<string> set = new HashSet<string>();

            set.Add("A");
            set.Add("B");
            set.Add("A"); //Duplicate

            Console.WriteLine(set.Count);
        }

        static void Experiment2_CheckContains()
        {
            HashSet<string> set = new HashSet<string>();

            set.Add("A");
            set.Add("B");

            Console.WriteLine(set.Contains("A"));
            Console.WriteLine(set.Contains("C"));
        }

        static void Experiment3_Remove()
        {
            HashSet<string> set = new HashSet<string>();

            set.Add("A");
            set.Add("B");

            set.Remove("A");

            Console.WriteLine(set.Contains("A")); // false
        }
    }
}
