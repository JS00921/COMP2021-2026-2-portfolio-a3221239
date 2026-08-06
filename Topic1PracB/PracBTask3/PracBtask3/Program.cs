using System;

namespace PracBTask3;

class Program
{
    static void Main()
    {
        Person testPerson = new Person("Harry", "Elder", 20);
        Console.WriteLine($"Full name: {testPerson.fullName()}");
        Console.WriteLine($"Is adult:  {testPerson.isAdult()}");

        Person youngPerson = new Person("Stacy", "Williams", 15);
        Console.WriteLine($"Full name: {youngPerson.fullName()}");
        Console.WriteLine($"Is adult:  {youngPerson.isAdult()}");
    }
}