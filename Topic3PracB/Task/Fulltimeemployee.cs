namespace Task
{
    public class FullTimeEmployee : Employee, IReportable
    {
        public decimal AnnualSalary { get; set; }

        public FullTimeEmployee(string name, decimal annualSalary)
            : base(name)
        {
            AnnualSalary = annualSalary;
        }

        public override decimal CalculatePay()
        {
            return AnnualSalary - CalculateTax(AnnualSalary);
        }

        public string GenerateReport()
        {
            decimal gross = AnnualSalary;
            decimal tax = CalculateTax(gross);

            return $"Full-Time Employee: {Name}{Environment.NewLine}" +
                   $"  Annual Salary : {gross:C}{Environment.NewLine}" +
                   $"  Tax ({TaxRate:P0})    : {tax:C}{Environment.NewLine}" +
                   $"  Net Pay       : {CalculatePay():C}";
        }
    }
}