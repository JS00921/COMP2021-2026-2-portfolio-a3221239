using System;

namespace PracAtask1;

public class SavingsAccount : BankAccount
{
    private decimal interestRate;
    
    public decimal InterestRate
    {
        get => interestRate;
        set
        {
            if (value < 0m || value > 1m)
                throw new ArgumentOutOfRangeException(nameof(value),
                    "Interest rate must be between 0 and 1 (0% to 100%).");

            interestRate = value;
        }
    }
    
    public SavingsAccount(string owner, decimal openingBalance, decimal interestRate)
        : base(owner, openingBalance)
    {
        InterestRate = interestRate;   // routed through the property, so it is validated
    }
    
    public decimal ApplyInterest()
    {
        decimal interest = decimal.Round(Balance * InterestRate, 2,
            MidpointRounding.AwayFromZero);
        
        if (interest <= 0m)
            return 0m;

        Deposit(interest);   // inherited - all deposit validation still applies
        return interest;
    }

    public override string ToString() =>
        $"{Owner} - Savings: {Balance:C} at {InterestRate:P2}";
    
    //Task 2
    public override void DisplayAccountInfo()
    {
        base.DisplayAccountInfo();
    
        Console.WriteLine($"  Interest rate:     {InterestRate:P2}");
        Console.WriteLine($"  Projected interest: {decimal.Round(Balance * InterestRate, 2, MidpointRounding.AwayFromZero):C}");
    }
}