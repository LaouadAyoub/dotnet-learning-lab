using System;

namespace DotNetLearningLab.Mutability
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 5;
            int b = a;
            b = 10;

            Console.WriteLine(a);

            var p1 = new Person { Name = "Ayoub" };
            var p2 = p1;
            p2.Name = "Changed";

            Console.WriteLine(p1.Name);
        }
    }

    class Person
    {
        public string Name { get; set; } = string.Empty;
    }
}
