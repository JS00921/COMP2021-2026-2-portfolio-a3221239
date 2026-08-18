namespace Task
{
    public abstract class Employee
    {
        public const decimal TaxRate = 0.2m;
        
        public string Name { get; set; }

        protected Employee(string name)
        {
            Name = name;
        }
        
        public abstract decimal CalculatePay();
        
        protected decimal CalculateTax(decimal grossPay) => grossPay * TaxRate;
    }
}