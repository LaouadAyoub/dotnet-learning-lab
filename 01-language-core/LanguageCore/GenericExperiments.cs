// CONCEPT:
// Generics — preserving type information through reusable code
// Generic methods — one reusable logic structure
// working with multiple concrete types


// WHAT I LEARNED:
// Using object removes exact type knowledge.
// Generics preserve the relationship between input type and output type.
// Generic methods allow the same logic
// to work with different types while
// preserving exact type information.

// WHEN TO USE:
// When building reusable backend systems that must remain strongly typed.
// Examples:
// repositories
// API responses
// pagination systems
// factories
// services




internal static class GenericExperiments
{
    public static void Run()
    {
        ObserveTypeLossWithObject();
        PreserveTypeWithGenerics();
        ObserveSameLogicWithDifferentTypes();
        ObserveReusableApiResponseStructure();
        ObserveReusablePaginationInfrastructure();
    }


    static void ObserveTypeLossWithObject()
    {
        // WHAT I LEARNED:
        // object removes exact type knowledge.
        // Compiler no longer knows the real type safely.

        // WHEN TO USE:
        // Understanding why excessive object usage creates
        // casting, runtime risks, and weaker backend code.

        object result = CreateObject("Rick");

        Console.WriteLine(result);

        // Compiler error:
        //string name = CreateObject("Rick");

        string name = (string)CreateObject("Rick");

        Console.WriteLine(name);

        // Dangerous runtime situation:
        // string invalidCast = (string)CreateObject(42);
    }

    static object CreateObject(object value)
    {
        return value;
    }

    static void PreserveTypeWithGenerics()
    {
        // WHAT I LEARNED:
        // Generics preserve the exact relationship
        // between input type and output type.

        // WHEN TO USE:
        // Reusable backend infrastructure that must remain
        // strongly typed without manual casting.

        string userName = CreateGeneric("Morty");

        int score = CreateGeneric(100);

        User user = CreateGeneric(new User
        {
            Name = "Walter White"
        });

        Console.WriteLine(userName);
        Console.WriteLine(score);
        Console.WriteLine(user.Name);
    }

    static T CreateGeneric<T>(T value)
    {
        return value;
    }


    static void ObserveSameLogicWithDifferentTypes()
    {
        // WHAT I LEARNED:
        // Same method.
        // Different types.
        // Compiler preserves exact type automatically.

        Console.WriteLine("------------ ObserveSameLogicWithDifferentTypes -------------");

        Print<string>("Rick");

        Print<int>(42);

        Print<User>(new User
        {
            Name = "Walter White"
        });

        string text = Echo<string>("Morty");

        int score = Echo<int>(100);

        User user = Echo<User>(new User
        {
            Name = "Jesse Pinkman"
        });

        Console.WriteLine(text);
        Console.WriteLine(score);
        Console.WriteLine(user.Name);
    }

    static void Print<T>(T value)
    {
        // WHAT I LEARNED:
        // Generic methods can receive
        // many different concrete types.

        Console.WriteLine(value);
    }

    static T Echo<T>(T value)
    {
        // WHAT I LEARNED:
        // T -> T preserves the relationship
        // between input type and output type.

        return value;
    }

    // CONCEPT:
    // Generic classes — reusable backend structures
    // preserving exact business payload types

    // WHAT I LEARNED:
    // Generic classes allow backend systems
    // to reuse the same infrastructure structure
    // for many different business entities.

    // WHEN TO USE:
    // API responses
    // pagination
    // repositories
    // wrappers
    // result systems
    // caching systems

    static void ObserveReusableApiResponseStructure()
    {
        // SAME structure
        // DIFFERENT business payloads

        ApiResponse<User> userResponse = new ApiResponse<User>
        {
            Success = true,
            Message = "User loaded successfully",

            Data = new User
            {
                Name = "Walter White"
            }
        };

        ApiResponse<Product> productResponse = new ApiResponse<Product>
        {
            Success = true,
            Message = "Product loaded successfully",

            Data = new Product
            {
                Title = "Gaming Keyboard"
            }
        };

        ApiResponse<Order> orderResponse = new ApiResponse<Order>
        {
            Success = true,
            Message = "Order loaded successfully",

            Data = new Order
            {
                Id = 404
            }
        };

        Console.WriteLine("-----------------------");

        Console.WriteLine(userResponse.Data.Name);

        Console.WriteLine(productResponse.Data.Title);

        Console.WriteLine(orderResponse.Data.Id);
    }

    // CONCEPT:
    // Generic infrastructure structures
    // used by many different backend business entities

    // WHAT I LEARNED:
    // Pagination is infrastructure behavior.
    // The pagination logic stays identical
    // while only the business payload type changes.

    // WHEN TO USE:
    // APIs
    // database queries
    // admin dashboards
    // search endpoints
    // infinite scrolling systems

    static void ObserveReusablePaginationInfrastructure()
    {
        // SAME pagination structure
        // DIFFERENT business payloads

        PaginatedResult<User> usersPage = new PaginatedResult<User>
        {
            TotalCount = 120,
            CurrentPage = 1,
            PageSize = 10,

            Items = new List<User>
            {
                new User { Name = "Rick Sanchez" },
                new User { Name = "Morty Smith" }
            }
        };

        PaginatedResult<Product> productsPage = new PaginatedResult<Product>
        {
            TotalCount = 80,
            CurrentPage = 2,
            PageSize = 20,

            Items = new List<Product>
            {
                new Product { Title = "Mechanical Keyboard" },
                new Product { Title = "Gaming Mouse" }
            }
        };

        PaginatedResult<Order> ordersPage = new PaginatedResult<Order>
        {
            TotalCount = 300,
            CurrentPage = 5,
            PageSize = 50,

            Items = new List<Order>
            {
                new Order { Id = 404 },
                new Order { Id = 505 }
            }
        };

        Console.WriteLine("-------------------------------");
        Console.WriteLine(usersPage.Items[0].Name);

        Console.WriteLine(productsPage.Items[1].Title);

        Console.WriteLine(ordersPage.Items[0].Id);
    }


}

public class User
{
    public string Name { get; set; } = string.Empty;

    public override string ToString()
    {
        return Name;
    }
}
public class Product
{
    public string Title { get; set; } = string.Empty;
}

public class Order
{
    public int Id { get; set; }
}

public class ApiResponse<T>
{
    // WHAT I LEARNED:
    // Generic structures preserve
    // the exact payload type.

    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public required T Data { get; set; }
}

public class PaginatedResult<T>
{
    // WHAT I LEARNED:
    // Generic infrastructure structures
    // preserve exact business payload types.

    public List<T> Items { get; set; } = new();

    public int TotalCount { get; set; }

    public int CurrentPage { get; set; }

    public int PageSize { get; set; }
}
