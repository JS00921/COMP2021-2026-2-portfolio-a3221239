using System;

namespace Task
{
    public class Contractor : Employee, IReportable
    {
        public decimal Rate { get; set; }
        public decimal Hours { get; set; }

        public Contractor(string name, decimal rate, decimal hours)
            : base(name)
        {
            Rate = rate;
            Hours = hours;
        }

        public override decimal CalculatePay()
        {
            decimal gross = Rate * Hours;
            return gross - CalculateTax(gross);
        }

        public string GenerateReport()
        {
            decimal gross = Rate * Hours;
            decimal tax = CalculateTax(gross);

            return $"Contractor: {Name}{Environment.NewLine}" +
                   $"  Rate          : {Rate:C} per hour{Environment.NewLine}" +
                   $"  Hours Worked  : {Hours}{Environment.NewLine}" +
                   $"  Gross Pay     : {gross:C}{Environment.NewLine}" +
                   $"  Tax ({TaxRate:P0})    : {tax:C}{Environment.NewLine}" +
                   $"  Net Pay       : {CalculatePay():C}";
        }
    }
}