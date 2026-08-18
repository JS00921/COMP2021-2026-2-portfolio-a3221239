using Xunit;

namespace Task.Tests;

public class ContractorTests : CurrencyCultureTests
{
    [Fact]
    public void Constructor_SetsNameRateAndHours()
    {
        var contractor = new Contractor("Donald Trump", 25m, 100m);

        Assert.Equal("Donald Trump", contractor.Name);
        Assert.Equal(25m, contractor.Rate);
        Assert.Equal(100m, contractor.Hours);
    }

    [Theory]
    [InlineData(25, 100, 2000)]
    [InlineData(95, 120, 9120)]
    [InlineData(80, 37.5, 2400)]   // fractional hours
    [InlineData(50, 0, 0)]         // worked no hours
    [InlineData(0, 40, 0)]         // unpaid rate
    [InlineData(25, -8, -160)]     // unguarded negative hours (e.g. a correction)
    public void CalculatePay_ReturnsGrossLessTax(decimal rate, decimal hours, decimal expectedNet)
    {
        var contractor = new Contractor("Donald Trump", rate, hours);

        Assert.Equal(expectedNet, contractor.CalculatePay());
    }

    [Fact]
    public void CalculatePay_FollowsRateAndHoursWhenTheyChange()
    {
        var contractor = new Contractor("Donald Trump", 25m, 100m);
        Assert.Equal(2000m, contractor.CalculatePay());

        contractor.Rate = 95m;
        contractor.Hours = 120m;

        Assert.Equal(9120m, contractor.CalculatePay());
    }

    [Fact]
    public void GenerateReport_ContainsRateHoursGrossTaxAndNetPay()
    {
        var contractor = new Contractor("Donald Trump", 25m, 100m);

        var expected = Lines(
            "Contractor: Donald Trump",
            "  Rate          : $25.00 per hour",
            "  Hours Worked  : 100",
            "  Gross Pay     : $2,500.00",
            "  Tax (20%)    : $500.00",
            "  Net Pay       : $2,000.00");

        Assert.Equal(expected, contractor.GenerateReport());
    }

    [Fact]
    public void GenerateReport_ReflectsPropertyChanges()
    {
        var contractor = new Contractor("Donald Trump", 25m, 100m);

        contractor.Name = "Tonald Drump";
        contractor.Rate = 95m;
        contractor.Hours = 120m;

        var expected = Lines(
            "Contractor: Tonald Drump",
            "  Rate          : $95.00 per hour",
            "  Hours Worked  : 120",
            "  Gross Pay     : $11,400.00",
            "  Tax (20%)    : $2,280.00",
            "  Net Pay       : $9,120.00");

        Assert.Equal(expected, contractor.GenerateReport());
    }

    [Fact]
    public void GenerateReport_HandlesZeroHours()
    {
        var contractor = new Contractor("Donald Trump", 25m, 0m);

        var expected = Lines(
            "Contractor: Donald Trump",
            "  Rate          : $25.00 per hour",
            "  Hours Worked  : 0",
            "  Gross Pay     : $0.00",
            "  Tax (20%)    : $0.00",
            "  Net Pay       : $0.00");

        Assert.Equal(expected, contractor.GenerateReport());
    }

    [Fact]
    public void GenerateReport_IsReachableThroughTheIReportableInterface()
    {
        IReportable reportable = new Contractor("Donald Trump", 25m, 100m);

        Assert.Contains("Contractor: Donald Trump", reportable.GenerateReport());
    }

    [Fact]
    public void Contractor_IsAnEmployeeAndIsReportable()
    {
        var contractor = new Contractor("Donald Trump", 25m, 100m);

        Assert.IsAssignableFrom<Employee>(contractor);
        Assert.IsAssignableFrom<IReportable>(contractor);
    }
}
