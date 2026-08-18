using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Task.Tests;

public class PolymorphismTests : CurrencyCultureTests
{
    private static List<Employee> BuildPayroll() => new()
    {
        new FullTimeEmployee("Big Chungus", 6250m),
        new Contractor("Donald Trump", 25m, 100m),
        new FullTimeEmployee("Small Chungus", 85_000m),
        new Contractor("Tonald Drump", 95m, 120m)
    };

    [Fact]
    public void CalculatePay_UsesTheOverrideOfEachRuntimeType()
    {
        var payroll = BuildPayroll();

        Assert.Equal(new[] { 5000m, 2000m, 68_000m, 9120m },
                     payroll.Select(e => e.CalculatePay()));
    }

    [Fact]
    public void Payroll_TotalsMatchTheSumOfEachEmployeesPay()
    {
        var payroll = BuildPayroll();

        var totalPay = payroll.Sum(e => e.CalculatePay());

        Assert.Equal(84_120m, totalPay);
        Assert.Equal(16_824m, totalPay * Employee.TaxRate);
    }

    [Fact]
    public void EveryEmployee_CanBeTreatedAsIReportable()
    {
        var payroll = BuildPayroll();

        var reports = payroll.OfType<IReportable>().Select(r => r.GenerateReport()).ToList();

        Assert.Equal(4, reports.Count);
        Assert.Equal(2, reports.Count(r => r.StartsWith("Full-Time Employee:")));
        Assert.Equal(2, reports.Count(r => r.StartsWith("Contractor:")));
    }

    [Fact]
    public void GenerateReport_UsesTheOverrideOfEachRuntimeType()
    {
        var payroll = BuildPayroll();

        Assert.StartsWith("Full-Time Employee: Big Chungus", ((IReportable)payroll[0]).GenerateReport());
        Assert.StartsWith("Contractor: Donald Trump", ((IReportable)payroll[1]).GenerateReport());
    }
}
