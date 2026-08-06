using System;
using Xunit;
using PracBTask3;

public class PersonTests
{
    [Fact]
    public void FullName_ReturnsExpectedFormat()
    {
        // Arrange
        Person testPerson = new Person("Harry", "Elder", 20);

        // Act
        string result = testPerson.fullName();

        // Assert
        Assert.Equal("Elder, Harry", result);
    }

    [Fact]
    public void IsAdult_ReturnsTrue_WhenAge18OrMore()
    {
        // Arrange
        Person adultPerson = new Person("Jennifer", "Adams", 18);

        // Act
        bool result = adultPerson.isAdult();

        // Assert
        Assert.True(result);
    }
}