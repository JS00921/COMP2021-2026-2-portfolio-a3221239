using System;
using PracAtask1;
using Xunit;

namespace PracAtask1.Tests;

public class SavingsAccountTests
{
    // ---------- Construction and inheritance ----------

    [Fact]
    public void Constructor_SetsAllProperties()
    {
        var account = new SavingsAccount("Jordan", 1_000m, 0.035m);

        Assert.Equal("Jordan", account.Owner);
        Assert.Equal(1_000m, account.Balance);
        Assert.Equal(0.035m, account.InterestRate);
    }

    [Fact]
    public void Constructor_RunsBaseValidationFirst()
    {
        // Owner validation lives in BankAccount, and ': base(...)' runs
        // before the derived constructor body - so this must still throw.
        Assert.Throws<ArgumentException>(() => new SavingsAccount("", 100m, 0.05m));
    }

    [Fact]
    public void SavingsAccount_IsABankAccount()
    {
        Assert.IsAssignableFrom<BankAccount>(new SavingsAccount("Jordan", 100m, 0.05m));
    }

    // ---------- InterestRate property guard ----------

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void InterestRate_AcceptsBoundaryValues(int rate)
    {
        var account = new SavingsAccount("Jordan", 100m, (decimal)rate);
        Assert.Equal((decimal)rate, account.InterestRate);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(2)]
    public void InterestRate_ThrowsWhenOutOfRange(int rate)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new SavingsAccount("Jordan", 100m, (decimal)rate));
    }

    [Fact]
    public void InterestRate_SetterThrowsAfterConstruction()
    {
        var account = new SavingsAccount("Jordan", 100m, 0.05m);
        Assert.Throws<ArgumentOutOfRangeException>(() => account.InterestRate = -0.01m);
    }

    // ---------- ApplyInterest ----------

    [Fact]
    public void ApplyInterest_CreditsBalanceAndReturnsAmount()
    {
        var account = new SavingsAccount("Jordan", 1_000m, 0.035m);

        decimal credited = account.ApplyInterest();

        Assert.Equal(35m, credited);
        Assert.Equal(1_035m, account.Balance);
    }

    [Fact]
    public void ApplyInterest_RoundsToTwoDecimalPlaces()
    {
        // 100.00 * 0.0333 = 3.33 exactly; use a value that needs rounding:
        // 101.55 * 0.0333 = 3.381615 -> 3.38
        var account = new SavingsAccount("Jordan", 101.55m, 0.0333m);

        decimal credited = account.ApplyInterest();

        Assert.Equal(3.38m, credited);
        Assert.Equal(104.93m, account.Balance);
    }

    [Fact]
    public void ApplyInterest_ReturnsZeroWhenBalanceIsZero()
    {
        // This is the guard clause path. Without it, Deposit(0m) would
        // throw ArgumentOutOfRangeException on a perfectly legal account.
        var account = new SavingsAccount("Jordan", 0m, 0.05m);

        decimal credited = account.ApplyInterest();

        Assert.Equal(0m, credited);
        Assert.Equal(0m, account.Balance);
    }

    [Fact]
    public void ApplyInterest_ReturnsZeroWhenRateIsZero()
    {
        var account = new SavingsAccount("Jordan", 1_000m, 0m);

        Assert.Equal(0m, account.ApplyInterest());
        Assert.Equal(1_000m, account.Balance);
    }

    [Fact]
    public void ApplyInterest_CompoundsAcrossCalls()
    {
        var account = new SavingsAccount("Jordan", 1_000m, 0.10m);

        account.ApplyInterest();   // +100.00 -> 1100.00
        account.ApplyInterest();   // +110.00 -> 1210.00

        Assert.Equal(1_210m, account.Balance);
    }

    // ---------- Inherited behaviour is unchanged ----------

    [Fact]
    public void Withdraw_UsesBaseBehaviourWithNoFee()
    {
        var account = new SavingsAccount("Jordan", 500m, 0.035m);

        account.Withdraw(100m);

        Assert.Equal(400m, account.Balance);
    }
}