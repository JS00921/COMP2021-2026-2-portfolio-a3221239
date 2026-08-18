using System;

namespace Task
{
    public static class Program
    {
        public static void Main()
        {
            // Both objects are held as Employee, yet each runs its own
            Employee[] staff =
            {
                new FullTimeEmployee("Big Chungus", 85_000m),
                new Contractor("Donald Trump", 95m, 120m)
            };
            foreach (Employee employee in staff)
            {
                Console.WriteLine($"{employee.Name} takes home {employee.CalculatePay():C}");
            }

            Console.WriteLine();
            Console.WriteLine(new string('-', 40));
            Console.WriteLine();

            // The interface lets us ask for a report without caring which
            foreach (Employee employee in staff)
            {
                if (employee is IReportable reportable)
                {
                    Console.WriteLine(reportable.GenerateReport());
                    Console.WriteLine();
                }
            }
        }
    }
}