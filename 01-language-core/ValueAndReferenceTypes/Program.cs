internal class Program
{
    class User
    {
        public string Name { get; set; } = string.Empty;
    }
    class Person
    {
        public string Name = string.Empty;
    }

    struct Dose
    {
        public int value { get; set; }
    }
    private static void Main(string[] args)
    {
        // 🧪 Experiment 1 — Mutation of a Class
        //Console.WriteLine("Hello, World!");

        //var user1 = new User { Name = "Ayoub" };

        //var user2 = user1;

        //user2.Name = "Karim";

        //Console.WriteLine(user1.Name);

        //Console.WriteLine(user2.Name);

        //🧪 Experiment 2 — Reassignment of a Class

        //var user1 = new User { Name = "Ayoub" };
        //var user2 = user1;

        //user2 = new User { Name = "Karim" };

        //Console.WriteLine(user1.Name);
        //Console.WriteLine(user2.Name);

        // 🧪 Experiment 3 — Value Type Copy

        //int a = 5;
        //int b = a;

        //b = 10;

        //Console.WriteLine(a);
        //Console.WriteLine(b);


        //// copy a reference type

        //Person p1 = new Person();
        //p1.Name = "Ayoub";
        //Person p2 = p1;

        //p2.Name = "Atlas";

        //Console.WriteLine(p1.Name);
        //Console.WriteLine(p2.Name);

        //// compare struct

        //Dose d1 = new Dose();
        //d1.value = 5;


        //Dose d2 = new Dose();
        //d2.value = 5;

        //Console.WriteLine(d1.Equals(d2));


        string a = "Ayoub";
        string b = new string(new char[] { 'A', 'y', 'o', 'u', 'b' });

        Console.WriteLine(a == b);
        Console.WriteLine(object.ReferenceEquals(a, b));
        Console.WriteLine(a.Equals(b));


    }
}
