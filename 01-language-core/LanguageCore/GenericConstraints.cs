
public interface IEntity
{
    int Id { get; set; }
}

public class Customer : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

// ❌ First version: intentionally broken
// Uncomment this to SEE the compiler failure.
// 'T' does not contain a definition for 'Id' and no accessible extension method 'Id'
// accepting a first argument of type 'T' could be found
// (are you missing a using directive or an assembly reference?)
//public class BrokenRepository<T>
//{
//    public void PrintEntityId(T entity)
//    {
//        Console.WriteLine(entity.Id);
//    }
//}

// ✅ Fixed version: constraint gives compiler proof
public class Repository<T> where T : IEntity
{
    public void PrintEntityId(T entity)
    {
        Console.WriteLine(entity.Id);
    }
}

// ❌ First version: intentionally broken
// Uncomment this to SEE the compiler failure.
// Cannot create an instance of the variable type 'T'
// because it does not have the new() constraint
//public class BrokenFactory<T>
//{
//    public T Create()
//    {
//        return new T();
//    }
//}


// ✅ Fixed version: new() constraint gives compiler proof
public class Factory<T> where T : new()
{
    public T Create()
    {
        return new T();
    }
}

public class Notification
{
    public string Message { get; set; } = string.Empty;
}

internal static class GenericConstraints
{
    public static void Run()
    {
        ObserveConstraintCreatingCompilerTrust();
        ObserveNewConstraintCreatingObjects();
    }


    static void ObserveConstraintCreatingCompilerTrust()
    {
        // WHAT I LEARNED:
        // Without a constraint, T could be anything.
        // The compiler cannot trust that T has an Id.
        // where T : IEntity reduces the possible types
        // and gives the compiler proof.

        // WHEN TO USE:
        // When building reusable backend infrastructure
        // that needs guaranteed capabilities from its type.

        Customer customer = new Customer()
        {
            Id = 10,
            Name = "Rick Sanchez"
        };

        Repository<Customer> repository = new Repository<Customer>();

        repository.PrintEntityId(customer);

    }


    //MIQ : Why can’t generic code do new T()
    // without where T : new ()?

    // Answer : The compiler cannot PROVE that T has an empty constructor.

    // Backend meaning : generic factories / mappers / serializers sometimes need to create objects automatically
    static void ObserveNewConstraintCreatingObjects()
    {
        // WHAT I LEARNED:
        // Generic code cannot create T by default.
        // The compiler does not know if T has an empty constructor.
        // where T : new() gives the compiler proof that new T() is safe.

        // WHEN TO USE:
        // When building reusable backend infrastructure
        // that needs to create objects automatically.
        // Examples:
        // factories
        // mappers
        // serializers
        // test data builders

        Factory<Notification> factory = new Factory<Notification>();

        Notification notification = factory.Create();


        notification.Message = "Payment received";

        Console.WriteLine(notification.Message);
    }



}
