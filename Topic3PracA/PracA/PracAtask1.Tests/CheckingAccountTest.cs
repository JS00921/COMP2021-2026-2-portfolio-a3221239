using System;
using PracAtask1;
using Xunit;

namespace PracAtask1.Tests;

public class CheckingAccountTests
{
    [Fact]
    public void Constructor_SetsAllProperties()
    {
        var account = new CheckingAccount("Jordan", 500m, 2.50m);

        Assert.Equal("Jordan", account.Owner);
        Assert.Equal(500m, account.Balance);
        Assert.Equal(2.50m, account.TransactionFee);
    }

    [Fact]
    public void Constructor_ThrowsWhenFeeNegative()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new CheckingAccount("Jordan", 500m, -1m));
    }

    [Fact]
    public void Constructor_AllowsZeroFee()
    {
        Assert.Equal(0m, new CheckingAccount("Jordan", 500m, 0m).TransactionFee);
    }

    [Fact]
    public void TransactionFee_SetterThrowsWhenNegative()
    {
        var account = new CheckingAccount("Jordan", 500m, 2.50m);
        Assert.Throws<ArgumentOutOfRangeException>(() => account.TransactionFee = -0.01m);
    }

    // ---------- Withdraw override ----------

    [Fact]
    public void Withdraw_DeductsAmountPlusFee()
    {
        var account = new CheckingAccount("Jordan", 500m, 2.50m);

        account.Withdraw(100m);

        Assert.Equal(397.50m, account.Balance);   // 500 - 100 - 2.50
    }

    [Fact]
    public void Withdraw_ChargesFeeOnEveryCall()
    {
        var account = new CheckingAccount("Jordan", 500m, 2.50m);

        account.Withdraw(100m);
        account.Withdraw(100m);

        Assert.Equal(295m, account.Balance);      // 500 - 200 - 5.00
    }

    [Fact]
    public void Withdraw_AllowsAmountThatExactlyDrainsWithFee()
    {
        var account = new CheckingAccount("Jordan", 102.50m, 2.50m);

        account.Withdraw(100m);

        Assert.Equal(0m, account.Balance);
    }

    [Fact]
    public void Withdraw_ThrowsWhenBalanceCoversAmountButNotFee()
    {
        // THE critical test for this subclass. The base class would ALLOW
        // this withdrawal - 100 <= 100. Only the override rejects it,
        // because 100 + 2.50 > 100.
        var account = new CheckingAccount("Jordan", 100m, 2.50m);

        Assert.Throws<InvalidOperationException>(() => account.Withdraw(100m));
        Assert.Equal(100m, account.Balance);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-50)]
    public void Withdraw_ThrowsWhenNotPositive(int amount)
    {
        var account = new CheckingAccount("Jordan", 500m, 2.50m);
        Assert.Throws<ArgumentOutOfRangeException>(() => account.Withdraw((decimal)amount));
    }

    [Fact]
    public void Withdraw_WithZeroFeeMatchesBaseBehaviour()
    {
        var account = new CheckingAccount("Jordan", 500m, 0m);

        account.Withdraw(100m);

        Assert.Equal(400m, account.Balance);
    }

    // ---------- Polymorphism ----------

    [Fact]
    public void Withdraw_ChargesFeeThroughBaseTypedReference()
    {
        BankAccount account = new CheckingAccount("Jordan", 500m, 2.50m);

        account.Withdraw(100m);

        Assert.Equal(397.50m, account.Balance);
    }

    [Fact]
    public void Deposit_IsInheritedUnchanged()
    {
        var account = new CheckingAccount("Jordan", 500m, 2.50m);

        account.Deposit(100m);   // no fee on deposits

        Assert.Equal(600m, account.Balance);
    }
}