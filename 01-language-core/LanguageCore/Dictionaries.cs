// CONCEPT:
// Dictionary<TKey, TValue>
// Stores data as key → value
// Access is done by key (identity), not by position (like List)


// WHAT I LEARNED:
// - Dictionary = key → value mapping
// - Access is direct using the key (no loop needed)
// - Keys are unique
// - Indexer assignment replaces a value; Add throws for an existing key.
// - Use ContainsKey to safely check before accessing
// - Order is not guaranteed / not important


// WHEN TO USE:
// - When I have a unique identifier (id, name, email)
// - When I need fast access to a value
// - When position/order does not matter
// - Typical use: lookup tables (userId → User, key → config, etc.)


namespace DotNetLearningLab.LanguageCore
{
    internal class Dictionaries
    {
        public static void Run()
        {
            Experiment1_CreateAndAccess();
            Console.WriteLine();
            Experiment2_UpdateValue();
            Console.WriteLine();
            Experiment3_CheckKeyExists();
            Console.WriteLine();

        }

        static void Experiment1_CreateAndAccess()
        {
            var jobs = new Dictionary<string, string>();

            jobs["ayoub"] = "Backend Developer";
            jobs["sara"] = "Designer";
            jobs["leo"] = "DevOps Engineer";

            Console.WriteLine(jobs["ayoub"]);
        }

        static void Experiment2_UpdateValue()
        {
            var jobs = new Dictionary<string, string>();

            jobs["ayoub"] = "Backend Developer";
            jobs["ayoub"] = "DevOps Engineer";

            Console.WriteLine(jobs["ayoub"]);
        }

        static void Experiment3_CheckKeyExists()
        {
            var jobs = new Dictionary<string, string>();

            jobs["ayoub"] = "Backend Developer";

            if (jobs.ContainsKey("ayoub"))
            {
                Console.WriteLine("Key exists");
            }

            if (jobs.ContainsKey("unknown"))
            {
                Console.WriteLine("This will not print");
            }
        }
    }
}
