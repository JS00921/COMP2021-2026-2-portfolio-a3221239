using Xunit;

namespace Task.Tests;

public class FullTimeEmployeeTests : CurrencyCultureTests
{
    [Fact]
    public void Constructor_SetsNameAndAnnualSalary()
    {
        var employee = new FullTimeEmployee("Big Chungus", 6250m);

        Assert.Equal("Big Chungus", employee.Name);
        Assert.Equal(6250m, employee.AnnualSalary);
    }

    [Theory]
    [InlineData(6250, 5000)]
    [InlineData(85000, 68000)]
    [InlineData(0, 0)]            // no salary means no pay and no tax
    [InlineData(-1000, -800)]     // unguarded negative salary still nets 80%
    [InlineData(0.05, 0.04)]
    public void CalculatePay_ReturnsSalaryLessTax(decimal salary, decimal expectedNet)
    {
        var employee = new FullTimeEmployee("Big Chungus", salary);

        Assert.Equal(expectedNet, employee.CalculatePay());
    }

    [Fact]
    public void CalculatePay_FollowsAnnualSalaryWhenItChanges()
    {
        var employee = new FullTimeEmployee("Big Chungus", 6250m);
        Assert.Equal(5000m, employee.CalculatePay());

        employee.AnnualSalary = 10_000m;

        Assert.Equal(8000m, employee.CalculatePay());
    }

    [Fact]
    public void GenerateReport_ContainsSalaryTaxAndNetPay()
    {
        var employee = new FullTimeEmployee("Big Chungus", 6250m);

        var expected = Lines(
            "Full-Time Employee: Big Chungus",
            "  Annual Salary : $6,250.00",
            "  Tax (20%)    : $1,250.00",
            "  Net Pay       : $5,000.00");

        Assert.Equal(expected, employee.GenerateReport());
    }

    [Fact]
    public void GenerateReport_ReflectsPropertyChanges()
    {
        var employee = new FullTimeEmployee("Big Chungus", 6250m);

        employee.Name = "Small Chungus";
        employee.AnnualSalary = 85_000m;

        var expected = Lines(
            "Full-Time Employee: Small Chungus",
            "  Annual Salary : $85,000.00",
            "  Tax (20%)    : $17,000.00",
            "  Net Pay       : $68,000.00");

        Assert.Equal(expected, employee.GenerateReport());
    }

    [Fact]
    public void GenerateReport_HandlesZeroSalary()
    {
        var employee = new FullTimeEmployee("Big Chungus", 0m);

        var expected = Lines(
            "Full-Time Employee: Big Chungus",
            "  Annual Salary : $0.00",
            "  Tax (20%)    : $0.00",
            "  Net Pay       : $0.00");

        Assert.Equal(expected, employee.GenerateReport());
    }

    [Fact]
    public void GenerateReport_IsReachableThroughTheIReportableInterface()
    {
        IReportable reportable = new FullTimeEmployee("Big Chungus", 6250m);

        Assert.Contains("Full-Time Employee: Big Chungus", reportable.GenerateReport());
    }

    [Fact]
    public void FullTimeEmployee_IsAnEmployeeAndIsReportable()
    {
        var employee = new FullTimeEmployee("Big Chungus", 6250m);

        Assert.IsAssignableFrom<Employee>(employee);
        Assert.IsAssignableFrom<IReportable>(employee);
    }
}
