using System;

namespace PracAtask1;

public class BankAccount
{
    public string Owner { get; set; }
    public decimal Balance { get; private set; }

    public BankAccount(string owner, decimal openingBalance = 0m)
    {
        if (string.IsNullOrWhiteSpace(owner))
            throw new ArgumentException("Owner must be provided.", nameof(owner));
        if (openingBalance < 0m)
            throw new ArgumentOutOfRangeException(nameof(openingBalance),
                "Opening balance cannot be negative.");

        Owner = owner;
        Balance = openingBalance;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0m)
            throw new ArgumentOutOfRangeException(nameof(amount),
                "Deposit amount must be positive.");

        Balance += amount;
    }

    public void Deposit(int amount) => Deposit((decimal)amount);

    public void Deposit(double amount)
    {
        if (amount < (double)decimal.MinValue || amount > (double)decimal.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(amount),
                "Deposit amount is outside the range of decimal.");

        Deposit((decimal)amount);
    }
    
    public virtual void Withdraw(decimal amount)
    {
        if (amount <= 0m)
            throw new ArgumentOutOfRangeException(nameof(amount),
                "Withdrawal amount must be positive.");
        if (amount > Balance)
            throw new InvalidOperationException(
                $"Insufficient funds. Balance is {Balance:C}, requested {amount:C}.");

        Balance -= amount;
    }

    public override string ToString() => $"{Owner} - Basic: {Balance:C}";
    
    // //Task 2
    // public virtual void DisplayAccountInfo()
    // {
    //     Console.WriteLine("=== Account Information ===");
    //     Console.WriteLine($"  Type:    {GetType().Name}");
    //     Console.WriteLine($"  Owner:   {Owner}");
    //     Console.WriteLine($"  Balance: {Balance:C}");
    // }
}