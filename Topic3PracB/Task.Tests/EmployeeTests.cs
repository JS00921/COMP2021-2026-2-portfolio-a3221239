using Xunit;

namespace Task.Tests;

public class EmployeeTests
{
    //Smallest possible concrete Employee, used to reach the base members.
    private sealed class StubEmployee : Employee
    {
        public StubEmployee(string name, decimal pay = 0m) : base(name) => Pay = pay;

        public decimal Pay { get; }

        public override decimal CalculatePay() => Pay;

        // Surfaces the protected helper so it can be asserted on directly.
        public decimal ExposeCalculateTax(decimal grossPay) => CalculateTax(grossPay);
    }

    [Fact]
    public void TaxRate_IsTwentyPercent()
    {
        Assert.Equal(0.2m, Employee.TaxRate);
    }

    [Fact]
    public void Constructor_SetsName()
    {
        var employee = new StubEmployee("Big Chungus");

        Assert.Equal("Big Chungus", employee.Name);
    }

    [Fact]
    public void Name_CanBeChangedAfterConstruction()
    {
        var employee = new StubEmployee("Big Chungus");

        employee.Name = "Small Chungus";

        Assert.Equal("Small Chungus", employee.Name);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(100, 20)]
    [InlineData(6250, 1250)]
    [InlineData(85000, 17000)]
    [InlineData(-500, -100)]      // no guard clause exists, so the sign carries through
    [InlineData(0.05, 0.01)]      // sub-cent gross still scales exactly (decimal, not double)
    public void CalculateTax_ReturnsTwentyPercentOfGross(decimal gross, decimal expectedTax)
    {
        var employee = new StubEmployee("Big Chungus");

        Assert.Equal(expectedTax, employee.ExposeCalculateTax(gross));
    }

    [Fact]
    public void CalculateTax_UsesTaxRateConstant()
    {
        var employee = new StubEmployee("Big Chungus");

        Assert.Equal(1000m * Employee.TaxRate, employee.ExposeCalculateTax(1000m));
    }

    [Fact]
    public void CalculatePay_IsDispatchedToTheDerivedOverride()
    {
        Employee employee = new StubEmployee("Big Chungus", 1234m);

        Assert.Equal(1234m, employee.CalculatePay());
    }

    [Fact]
    public void TaxRate_EmittedFieldMatchesTheInlinedConstant()
    {
        var field = typeof(Employee).GetField(nameof(Employee.TaxRate));

        Assert.NotNull(field);
        Assert.Equal(Employee.TaxRate, Assert.IsType<decimal>(field!.GetValue(null)));
    }

    [Fact]
    public void Employee_IsAbstract()
    {
        Assert.True(typeof(Employee).IsAbstract);
    }
}
