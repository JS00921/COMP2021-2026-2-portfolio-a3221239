using System;
using System.Collections.Generic;

namespace PracAtask1;

public static class Program
{
    public static void Main()
    {
        var basic    = new BankAccount("Johnny", 500m);
        var savings  = new SavingsAccount("Johnny", 1_000m, 0.035m);   // 3.5%
        var checking = new CheckingAccount("Johnny", 500m, 2.50m);
        var accounts = new List<BankAccount> { basic, savings, checking };

        Console.WriteLine("--- Opening ---");
        foreach (var account in accounts)
            Console.WriteLine(account);   // virtual ToString dispatches to each subclass

        Console.WriteLine("\n--- Withdraw 100 from each ---");
        foreach (var account in accounts)
        {
            account.Withdraw(100m);
            Console.WriteLine(account);
        }

        Console.WriteLine("\n--- Savings-only behaviour ---");
        decimal credited = savings.ApplyInterest();
        Console.WriteLine($"Interest credited: {credited:C}");
        Console.WriteLine(savings);
        
        if (accounts[1] is SavingsAccount typedSavings)
            Console.WriteLine($"Second interest run: {typedSavings.ApplyInterest():C}");

        Console.WriteLine("\n--- Exception paths ---");
        TryAction("checking.Withdraw(500m)", () => checking.Withdraw(500m));
        TryAction("savings.InterestRate = -1", () => savings.InterestRate = -1m);
        TryAction("new CheckingAccount fee -1",
                  () => new CheckingAccount("Johnny", 100m, -1m));
        
        // Task2
        foreach (var account in accounts)
        {
            account.DisplayAccountInfo();
            Console.WriteLine();
        }
    }
    
    private static void TryAction(string label, Action action)
    {
        try
        {
            action();
            Console.WriteLine($"{label,-28} succeeded");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{label,-28} threw {ex.GetType().Name}: {ex.Message}");
        }
    }
}

