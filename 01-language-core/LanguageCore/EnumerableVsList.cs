// CONCEPT:
// IEnumerable vs List

// WHAT I LEARNED:
// Where() does not execute immediately.
// It prepares filtering logic.
// The filtering happens when I read the sequence.
// An IEnumerable query can execute multiple times.
// Each iteration can re-run the logic.
// IEnumerable<T> promises enumeration; it may represent a list or a deferred query.
// If I iterate the same IEnumerable multiple times, the logic can run multiple times.
// In a real backend app, if the source is a database query, this can mean multiple DB calls.
// ToList() forces execution once and stores the results in memory as a List.

// WHEN TO USE:
// Use this to understand delayed execution:
// build first, execute when consumed.
// Understand when queries are re-executed and why ToList() can prevent it.
// This Where query is deferred; IEnumerable<T> itself does not guarantee deferred execution.
// Use ToList() when I want to execute once and reuse the same results safely.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetLearningLab.LanguageCore
{
    internal class EnumerableVsList
    {

        public static void Run()
        {
            Experiment_WhenDoesExecutionHappen();
            Console.WriteLine();
            Experiment_QueryExecutesMultipleTimes();
            Console.WriteLine();
            Experiment_ToListExecutesOnceAndStoresResults();

        }

        static void Experiment_WhenDoesExecutionHappen()
        {
            List<Character> characters = new List<Character>
            {
                new Character("Neo", true),
                new Character("Trinity", true),
                new Character("Morpheus", false)
            };

            Console.WriteLine("Before Where");

            IEnumerable<Character> activeCharacters = characters.Where(character =>
            {
                Console.WriteLine($"Checking {character.Name}");
                return character.IsActive;
            });

            Console.WriteLine("After Where");
            Console.WriteLine("Before foreach");

            foreach (Character character in activeCharacters)
            {
                Console.WriteLine($"Active character: {character.Name}");
            }

            Console.WriteLine("After foreach");
        }

        static void Experiment_QueryExecutesMultipleTimes()
        {
            List<Character> characters = new List<Character>
            {
                new Character("Rick", true),
                new Character("Morty", true),
                new Character("Jerry", false)
            };

            Console.WriteLine("Creating query");

            IEnumerable<Character> activeCharacters = characters.Where(character =>
            {
                Console.WriteLine($"Filtering {character.Name}");
                return character.IsActive;
            });

            Console.WriteLine("First foreach");

            foreach (Character character in activeCharacters)
            {
                Console.WriteLine($"Active: {character.Name}");
            }

            Console.WriteLine("Second foreach");

            foreach (Character character in activeCharacters)
            {
                Console.WriteLine($"Active: {character.Name}");
            }
        }

        static void Experiment_ToListExecutesOnceAndStoresResults()
        {
            List<Character> characters = new List<Character>
            {
                new Character("Rick", true),
                new Character("Morty", true),
                new Character("Jerry", false)
            };

            Console.WriteLine("Creating query");

            IEnumerable<Character> activeCharactersQuery = characters.Where(character =>
            {
                Console.WriteLine($"Filtering {character.Name}");
                return character.IsActive;
            });

            Console.WriteLine("Converting query to List");

            List<Character> activeCharactersList = activeCharactersQuery.ToList();

            Console.WriteLine("First foreach on List");

            foreach (Character character in activeCharactersList)
            {
                Console.WriteLine($"Active: {character.Name}");
            }

            Console.WriteLine("Second foreach on List");

            foreach (Character character in activeCharactersList)
            {
                Console.WriteLine($"Active: {character.Name}");
            }
        }
    }

    internal class Character
    {
        public string Name { get; }
        public bool IsActive { get; }

        public Character(string name, bool isActive)
        {
            Name = name;
            IsActive = isActive;
        }
    }
}
