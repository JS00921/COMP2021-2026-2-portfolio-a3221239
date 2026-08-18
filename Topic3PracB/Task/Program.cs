using System;
using System.Collections.Generic;

namespace Task
{
    public static class Program
    {
        public static void Main()
        {
            // A List<Employee> can hold any type that derives from Employee.
            List<Employee> employees = new List<Employee>
            {
                new FullTimeEmployee("Big Chungus", 6250m),
                new Contractor("Donald Trump", 25m, 100m),
                new FullTimeEmployee("Small Chungus", 85_000m),
                new Contractor("Tonald Drump", 95m, 120m)
            };

            decimal totalPay = 0m;
            decimal totalTax = 0m;

            foreach (Employee employee in employees)
            {
                // Virtual dispatch: the runtime picks FullTimeEmployee.
                decimal pay = employee.CalculatePay();
                decimal tax = pay * Employee.TaxRate;

                totalPay += pay;
                totalTax += tax;

                Console.WriteLine($"{employee.Name}: Pay {pay:C0}. Tax {tax:C0}.");
            }

            Console.WriteLine();
            Console.WriteLine($"{employees.Count} employees. " +
                              $"Total pay {totalPay:C0}. Total tax {totalTax:C0}.");
        }
    }
}