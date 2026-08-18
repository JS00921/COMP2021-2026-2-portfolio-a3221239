namespace PracAtask1;

public class CheckingAccount : BankAccount
{
    private decimal transactionFee;
    
    public decimal TransactionFee
    {
        get => transactionFee;
        set
        {
            if (value < 0m)
                throw new ArgumentOutOfRangeException(nameof(value),
                    "Transaction fee cannot be negative.");

            transactionFee = value;
        }
    }

    public CheckingAccount(string owner, decimal openingBalance, decimal transactionFee)
        : base(owner, openingBalance)
    {
        TransactionFee = transactionFee;
    }
    
    public override void Withdraw(decimal amount)
    {
        if (amount <= 0m)
            throw new ArgumentOutOfRangeException(nameof(amount),
                "Withdrawal amount must be positive.");

        decimal totalDebit = amount + TransactionFee;

        if (totalDebit > Balance)
            throw new InvalidOperationException(
                $"Insufficient funds. Balance is {Balance:C}, but {amount:C} " +
                $"plus a {TransactionFee:C} fee requires {totalDebit:C}.");
        base.Withdraw(totalDebit);
    }

    public override string ToString() =>
        $"{Owner} - Checking: {Balance:C} ({TransactionFee:C} per withdrawal)";
    
    
    // //Task2
    // public override void DisplayAccountInfo()
    // {
    //     base.DisplayAccountInfo();
    //     Console.WriteLine($"Transaction fee: ${TransactionFee:0.00}");
    // }
}