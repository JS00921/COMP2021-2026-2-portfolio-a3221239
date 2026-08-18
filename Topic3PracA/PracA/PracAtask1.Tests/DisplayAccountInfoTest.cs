using PracAtask1;
using Xunit;

namespace PracAtask1.Tests;

[Collection("Console")]   // serialised against other console-capturing classes
public class DisplayAccountInfoTests
{
    // These tests check the facts DisplayAccountInfo must report, not the exact
    // layout, so cosmetic changes to the banner or indentation do not break them.
    // Amounts and rates are formatted with the same specifiers the accounts use,
    // which keeps the assertions correct under any culture.

    [Fact]
    public void BankAccount_ReportsTypeOwnerAndBalance()
    {
        using var console = new ConsoleCapture();

        new BankAccount("Jordan", 500m).DisplayAccountInfo();

        Assert.Contains("BankAccount", console.Text);
        Assert.Contains("Jordan", console.Text);
        Assert.Contains($"{500m:C}", console.Text);
    }

    [Fact]
    public void SavingsAccount_AlsoReportsInterestRate()
    {
        using var console = new ConsoleCapture();

        new SavingsAccount("Jordan", 1_500m, 0.035m).DisplayAccountInfo();

        Assert.Contains("SavingsAccount", console.Text);
        Assert.Contains("Jordan", console.Text);
        Assert.Contains($"{1_500m:C}", console.Text);
        Assert.Contains($"{0.035m:P2}", console.Text);
    }

    [Fact]
    public void CheckingAccount_AlsoReportsTransactionFee()
    {
        using var console = new ConsoleCapture();

        new CheckingAccount("Jordan", 500m, 2.50m).DisplayAccountInfo();

        Assert.Contains("CheckingAccount", console.Text);
        Assert.Contains("Jordan", console.Text);
        Assert.Contains($"{500m:C}", console.Text);
        Assert.Contains($"${2.50m:0.00}", console.Text);
    }

    [Fact]
    public void DisplayAccountInfo_DispatchesOnRuntimeTypeNotDeclaredType()
    {
        using var console = new ConsoleCapture();

        // Declared as BankAccount, but the override must still run.
        BankAccount account = new SavingsAccount("Jordan", 1_500m, 0.035m);
        account.DisplayAccountInfo();

        Assert.Contains("SavingsAccount", console.Text);
        Assert.Contains($"{0.035m:P2}", console.Text);
        // The base implementation would have named the declared type instead.
        Assert.DoesNotContain("Type:    BankAccount", console.Text);
    }

    [Fact]
    public void DisplayAccountInfo_ReflectsUpdatedBalance()
    {
        using var console = new ConsoleCapture();

        var account = new CheckingAccount("Jordan", 500m, 2.50m);
        account.Withdraw(100m);   // 500 - 100 - 2.50 fee
        account.DisplayAccountInfo();

        Assert.Contains($"{397.50m:C}", console.Text);
        Assert.DoesNotContain($"{500m:C}", console.Text);
    }
}
