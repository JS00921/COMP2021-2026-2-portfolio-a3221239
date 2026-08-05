namespace PayrollCalculator;

public class Person
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public decimal age { get; set; }

    public Person(string firstName, string lastName, decimal age)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required.", nameof(firstName));
        
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required.", nameof(lastName));
        
        if (age < 0 || age > 130)
            throw new ArgumentOutOfRangeException( nameof(age), "Age must be between 0 and 130.");
        
        this.firstName = firstName;
        this.lastName = lastName;
        this.age = age;
    }
    
    public string fullName() => $"{lastName}, {firstName}";

    public bool isAdult() => age >= 18;
}