using System;
using PracAtask1;
using Xunit;

namespace PracAtask1.Tests;

public class BankAccountTests
{

    [Fact]
    public void Constructor_SetsOwnerAndOpeningBalance()
    {
        var account = new BankAccount("Johnny", 100m);

        Assert.Equal("Johnny", account.Owner);
        Assert.Equal(100m, account.Balance);
    }

    [Fact]
    public void Constructor_DefaultsBalanceToZero()
    {
        Assert.Equal(0m, new BankAccount("Johnny").Balance);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_ThrowsWhenOwnerMissing(string? owner)
    {
        Assert.Throws<ArgumentException>(() => new BankAccount(owner!));
    }

    [Fact]
    public void Constructor_ThrowsWhenOpeningBalanceNegative()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new BankAccount("Johnny", -1m));
    }

    [Fact]
    public void DepositDecimal_IncreasesBalance()
    {
        var account = new BankAccount("Johnny", 100m);
        account.Deposit(50.25m);
        Assert.Equal(150.25m, account.Balance);
    }

    [Fact]
    public void DepositInt_IncreasesBalance()
    {
        var account = new BankAccount("Johnny", 100m);
        account.Deposit(25);
        Assert.Equal(125m, account.Balance);
    }

    [Fact]
    public void DepositDouble_IncreasesBalance()
    {
        var account = new BankAccount("Johnny", 100m);
        account.Deposit(10.75);
        Assert.Equal(110.75m, account.Balance);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void DepositDecimal_ThrowsWhenNotPositive(int amount)
    {
        var account = new BankAccount("Johnny", 100m);
        Assert.Throws<ArgumentOutOfRangeException>(() => account.Deposit((decimal)amount));
    }

    [Fact]
    public void DepositInt_ThrowsWhenNotPositive()
    {
        var account = new BankAccount("Johnny", 100m);
        Assert.Throws<ArgumentOutOfRangeException>(() => account.Deposit(0));
    }

    [Fact]
    public void DepositDouble_ThrowsWhenNotPositive()
    {
        var account = new BankAccount("Johnny", 100m);
        Assert.Throws<ArgumentOutOfRangeException>(() => account.Deposit(-0.01));
    }

    [Fact]
    public void DepositDouble_ThrowsWhenOutsideDecimalRange()
    {
        var account = new BankAccount("Johnny", 100m);
        Assert.Throws<ArgumentOutOfRangeException>(() => account.Deposit(double.MaxValue));
    }
    

    [Fact]
    public void Withdraw_ReducesBalance()
    {
        var account = new BankAccount("Johnny", 100m);
        account.Withdraw(40m);
        Assert.Equal(60m, account.Balance);
    }

    [Fact]
    public void Withdraw_AllowsExactBalance()
    {
        var account = new BankAccount("Johnny", 100m);
        account.Withdraw(100m);
        Assert.Equal(0m, account.Balance);
    }

    [Fact]
    public void Withdraw_ThrowsWhenAmountExceedsBalanceByOneCent()
    {
        var account = new BankAccount("Johnny", 100m);
        Assert.Throws<InvalidOperationException>(() => account.Withdraw(100.01m));
    }

    [Fact]
    public void Withdraw_LeavesBalanceUnchangedWhenItThrows()
    {
        var account = new BankAccount("Johnny", 100m);

        Assert.Throws<InvalidOperationException>(() => account.Withdraw(500m));
        Assert.Equal(100m, account.Balance);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-50)]
    public void Withdraw_ThrowsWhenNotPositive(int amount)
    {
        var account = new BankAccount("Johnny", 100m);
        Assert.Throws<ArgumentOutOfRangeException>(() => account.Withdraw((decimal)amount));
    }
    

    [Fact]
    public void Owner_CanBeReassigned()
    {
        var account = new BankAccount("Johnny", 100m) { Owner = "J. Zhang" };
        Assert.Equal("J. Zhang", account.Owner);
    }
}