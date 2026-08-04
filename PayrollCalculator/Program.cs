// # payroll_calculator.py
// TAX_RATE = 0.2
//
// def calculate_pay(hours, rate):
//     if hours < 0 or rate < 0:
//         raise ValueError("Hours and rate must be positive.")
//     gross = hours * rate
//     tax = gross * TAX_RATE
//     net = gross - tax
//     return net
//
// def main():
//     name = input("Enter employee name: ")
//     hours = float(input("Hours worked: "))
//     rate = float(input("Hourly rate: "))
//     net_pay = calculate_pay(hours, rate)
//     print(f"{name} earned ${net_pay:.2f} after tax.")
//
// if __name__ == "__main__":
//     main()


using System;

namespace PayrollCalculator
{
    class Program
    {
        public static decimal TAX_RATE = 0.2m;
        public static decimal calculatePay(decimal hours, decimal rate)
        {
            if (hours < 0 || rate < 0)
            {
                throw new ArgumentException("Hours and rate must be positive.");
            }

            decimal gross = hours * rate;
            decimal tax = gross * TAX_RATE;
            decimal net = gross - tax;
            return net;
        }
        static void Main(string[] args)
        {
            //Task 2
            Console.Write("Enter employee name: ");
            string name = Console.ReadLine();
            
            Console.Write("Hours worked: ");
            decimal hours = decimal.Parse(Console.ReadLine());
            
            Console.Write("Hourly rate: ");
            decimal rate = decimal.Parse(Console.ReadLine());

            decimal netPay = calculatePay(hours, rate);
            
            Console.WriteLine($"{name} earned ${netPay:F2} after tax.");
            
            //Task 3
            Person p = new Person("Harry", "Elder", 20);
            
            Console.WriteLine($"First name: {p.firstName}");
            Console.WriteLine($"Last name:  {p.lastName}");
            Console.WriteLine($"Age:        {p.age}");
            
            Console.WriteLine($"Full name:  {p.fullName()}");
            Console.WriteLine($"Is adult:   {p.isAdult()}");
            
            Person child = new Person("Stacy", "Williams", 15);
            Console.WriteLine();
            Console.WriteLine($"{child.fullName()} is adult? {child.isAdult()}");
        }
    }
}