// CONCEPT:
// LINQ core backend patterns: filtering, projection, chaining, and execution.
//
// WHAT I LEARNED:
// LINQ lets me describe data flow clearly. The important part is knowing
// what runs, when it runs, and what shape of data I want.
//
// WHEN TO USE:
// Use LINQ when reading, filtering, transforming, or preparing data,
// especially when building backend responses from collections or queries.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetLearningLab.LanguageCore
{
    internal class Linq
    {

        public static void Run()
        {
            FilterCharactersStartingWithM();
            Console.WriteLine();
            ProjectCharactersToLabels();
            Console.WriteLine();
            SelectUserNamesFromUsers();
            Console.WriteLine();
            ProjectUsersToResponseObjects();
            Console.WriteLine();
            CompareToListBeforeAndAfterFiltering();
            Console.WriteLine();
            CheckMultipleEnumerationBehavior();
            Console.WriteLine();
            CheckIfAnyActiveUsersExist();
            Console.WriteLine();
            CountActiveUsers();
            Console.WriteLine();
            FindUserByNameOrDefault();
            Console.WriteLine();
            FindUserWithFirstAndObserveException();

        }

        // CONCEPT:
        // LINQ filtering using Where (basic pipeline understanding)
        //
        // WHAT I LEARNED:
        // Where filters elements using a condition function (lambda).
        // The result is not data, but a pipeline that runs when iterated.
        //
        // WHEN TO USE:
        // When I want to select only elements that match a condition
        // from a collection (very common in backend filtering).
        static void FilterCharactersStartingWithM()
        {
            List<string> characters = new List<string>
            {
                "Rick",
                "Morty",
                "Walter",
                "Jesse"
            };

            IEnumerable<string> filteredCharacters = characters.Where(character => character.StartsWith("M"));

            foreach (string character in filteredCharacters)
            {
                Console.WriteLine(character);
            }
        }

        static void ProjectCharactersToLabels()
        {
            // WHAT I LEARNED:
            // Select transforms each element into something new.
            // It does not remove items, it changes their shape.

            // WHEN TO USE:
            // When I want to convert data into another form
            // (formatting, extracting, reshaping).

            List<string> characters = new List<string>
            {
                "Rick",
                "Morty",
                "Walter",
                "Jesse"
            };

            IEnumerable<string> transformedCharacters = characters
                .Select(character => "Character: " + character);

            foreach (string character in transformedCharacters)
            {
                Console.WriteLine(character);
            }
        }

        static void SelectUserNamesFromUsers()
        {
            // WHAT I LEARNED:
            // Select can extract specific fields from objects.
            // I don’t need the full object, only the data I want.

            // WHEN TO USE:
            // When building backend responses (DTOs),
            // to avoid returning unnecessary data.

            List<User> users = new List<User>
            {
                new User { Id = 1, Name = "Rick", IsActive = true },
                new User { Id = 2, Name = "Morty", IsActive = false },
                new User { Id = 3, Name = "Walter", IsActive = true }
            };

            IEnumerable<string> userNames = users
                .Select(user => user.Name);

            foreach (string name in userNames)
            {
                Console.WriteLine(name);
            }
        }

        static void ProjectUsersToResponseObjects()
        {
            // WHAT I LEARNED:
            // Select can create new objects (DTOs) from existing data.
            // I control exactly what fields I expose.

            // WHEN TO USE:
            // When building API responses to return only the necessary data
            // instead of full database entities.

            List<User> users = new List<User>
            {
                new User { Id = 1, Name = "Rick", IsActive = true },
                new User { Id = 2, Name = "Morty", IsActive = false },
                new User { Id = 3, Name = "Walter", IsActive = true }
            };

            IEnumerable<UserResponse> responses = users
                .Where(user => user.IsActive)
                .Select(user => new UserResponse
                {
                    Id = user.Id,
                    Name = user.Name
                });

            foreach (UserResponse response in responses)
            {
                Console.WriteLine($"Id: {response.Id}, Name: {response.Name}");
            }
        }

        static void CompareToListBeforeAndAfterFiltering()
        {
            // WHAT I LEARNED:
            // ToList() executes the query immediately.
            // If I call it too early, I load everything before filtering.

            // WHEN TO USE:
            // Filter before materializing when only the filtered results are needed
            // to avoid unnecessary work (especially important with databases).

            List<User> users = new List<User>
            {
                new User { Id = 1, Name = "Rick", IsActive = true },
                new User { Id = 2, Name = "Morty", IsActive = false },
                new User { Id = 3, Name = "Walter", IsActive = true }
            };

            Console.WriteLine("=== Case 1: ToList AFTER filtering (Good!) ===");

            List<User> filteredUsersCorrect = users
                .Where(user => user.IsActive)
                .ToList();

            foreach (User user in filteredUsersCorrect)
            {
                Console.WriteLine(user.Name);
            }

            Console.WriteLine();
            Console.WriteLine("=== Case 2: ToList BEFORE filtering ===");

            List<User> allUsersMaterialized = users.ToList();

            IEnumerable<User> filteredUsersWrong = allUsersMaterialized
                .Where(user => user.IsActive);

            foreach (User user in filteredUsersWrong)
            {
                Console.WriteLine(user.Name);
            }
        }

        static void CheckMultipleEnumerationBehavior()
        {
            // WHAT I LEARNED:
            // A LINQ query (IEnumerable) executes each time it is iterated.
            // If I use it multiple times, the logic runs multiple times.

            // WHEN TO USE:
            // When I reuse a query multiple times, I may need ToList()
            // to avoid repeated execution (especially important with databases).

            List<User> users = new List<User>
            {
                new User { Id = 1, Name = "Rick", IsActive = true },
                new User { Id = 2, Name = "Morty", IsActive = false},
                new User { Id = 3, Name = "Walter", IsActive = true }
            };

            IEnumerable<User> activeUsersQuery = users.Where(user =>
            {
                Console.WriteLine($"Filtering user: {user.Name}");
                return user.IsActive;
            });

            Console.WriteLine($"=== First use : Count ===");

            int activeCount = activeUsersQuery.Count(); // Enumerates the in-memory query.

            Console.WriteLine($"Active count: {activeCount}");

            Console.WriteLine();
            Console.WriteLine("=== Second use: foreach ===");

            foreach (User user in activeUsersQuery)   // Enumerates the in-memory query again.
            {
                Console.WriteLine(user.Name);
            }

            // 🧠 The fix pattern

            // List<User> activeUsers = activeUsersQuery.ToList();
            // Then
            // activeUsers.Count();
            // foreach (...)
        }

        static void CheckIfAnyActiveUsersExist()
        {
            // WHAT I LEARNED:
            // Any() checks if at least one element matches a condition.
            // It returns true or false.

            // WHEN TO USE:
            // When I only need to know if something exists,
            // not how many items exist.

            List<User> users = new List<User>
            {
                new User {Id = 1, Name = "Rick", IsActive = true},
                new User {Id = 1, Name = "Morty", IsActive = false},
                new User {Id = 1, Name = "Walter", IsActive = true},
            };

            bool hasActiveUsers = users.Any(user => user.IsActive);

            Console.WriteLine(hasActiveUsers);

            // In databases, Any() can stop early when it finds one result ⚡
        }

        static void CountActiveUsers()
        {
            // WHAT I LEARNED:
            // Count() returns the number of elements
            // that match a condition.

            // WHEN TO USE:
            // When I need the exact quantity of matching items,
            // not just existence.

            List<User> users = new List<User>
            {
                new User { Id = 1, Name = "Rick", IsActive = true },
                new User { Id = 2, Name = "Morty", IsActive = false },
                new User { Id = 3, Name = "Walter", IsActive = true }
            };

            int activeUsersCount = users
                .Count(user => user.IsActive);

            Console.WriteLine(activeUsersCount);
        }


        static void FindUserByNameOrDefault()
        {
            // WHAT I LEARNED:
            // FirstOrDefault() safely retrieves one item.
            // For this reference type, no match returns null. Value types return default(T).

            // WHEN TO USE:
            // When searching for one item that may or may not exist
            // (very common in backend APIs and validation flows).

            List<User> users = new List<User>
            {
                new User { Id = 1, Name = "Rick", IsActive = true },
                new User { Id = 2, Name = "Morty", IsActive = false },
                new User { Id = 3, Name = "Walter", IsActive = true }
            };

            User? foundUser = users.FirstOrDefault(user => user.Name == "Walter");

            if (foundUser != null)
            {
                Console.WriteLine($"User found: {foundUser.Name}");
            }
            else
            {
                Console.WriteLine("User not found");
            }
        }


        static void FindUserWithFirstAndObserveException()
        {
            // WHAT I LEARNED:
            // First() expects a matching element to exist.
            // If nothing is found, it throws an exception.

            // WHEN TO USE:
            // When I am absolutely sure that data exists.
            // Otherwise, prefer FirstOrDefault().
            // In many real backend situations: FirstOrDefault() is safer and often preferred.
            // First() is often used deliberately to raise and exception if something required is missing or something is broken
            // like : required configuration, authenticated user context, startup state, invariant assumptions.

            List<User> users = new List<User>
            {
                new User { Id = 1, Name = "Rick", IsActive = true },
                new User { Id = 2, Name = "Morty", IsActive = false },
                new User { Id = 3, Name = "Walter", IsActive = true }
            };

            try
            {
                User foundUser = users.First(user => user.Name == "Unknown");
                Console.WriteLine(foundUser.Name);
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine($"No matching user: {exception.Message}");
            }
        }


        class User
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public bool IsActive { get; set; }
        }

        class UserResponse
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }




    }
}
