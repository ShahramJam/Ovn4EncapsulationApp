using Ovn4EncapsulationApp;

namespace Ovn4EncapsulationAppTest;

public sealed class PersonTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("Al")]
    public void Constructor_RejectsInvalidFirstName(string? firstName)
    {
        var exception = Assert.Throws<ArgumentException>(
            () => new Person(firstName!, "Doe", 30, 460m));

        Assert.Equal(
            "First name cannot contain fewer than 3 symbols!",
            exception.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("Li")]
    public void Constructor_RejectsInvalidLastName(string? lastName)
    {
        var exception = Assert.Throws<ArgumentException>(
            () => new Person("Jane", lastName!, 30, 460m));

        Assert.Equal(
            "Last name cannot contain fewer than 3 symbols!",
            exception.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_RejectsNonPositiveAge(int age)
    {
        var exception = Assert.Throws<ArgumentException>(
            () => new Person("Jane", "Doe", age, 460m));

        Assert.Equal(
            "Age cannot be zero or a negative integer!",
            exception.Message);
    }

    [Theory]
    [InlineData(459.99)]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_RejectsSalaryBelowMinimum(decimal salary)
    {
        var exception = Assert.Throws<ArgumentException>(
            () => new Person("Jane", "Doe", 30, salary));

        Assert.Equal(
            "Salary cannot be less than 460 dollars!",
            exception.Message);
    }

    [Fact]
    public void Constructor_AcceptsMinimumSalary()
    {
        var person = new Person("Jane", "Doe", 30, 460m);

        Assert.Equal(460m, person.Salary);
    }

    [Fact]
    public void IncreaseSalary_AppliesHalfPercentageWhenAgeIsUnder30()
    {
        var person = new Person("Jane", "Doe", 29, 1000m);

        person.IncreaseSalary(10m);

        Assert.Equal(1050m, person.Salary);
    }

    [Fact]
    public void IncreaseSalary_AppliesFullPercentageWhenAgeIs30()
    {
        var person = new Person("Jane", "Doe", 30, 1000m);

        person.IncreaseSalary(10m);

        Assert.Equal(1100m, person.Salary);
    }

    [Fact]
    public void IncreaseSalary_RejectsResultBelowMinimumAndKeepsOriginalSalary()
    {
        var person = new Person("Jane", "Doe", 30, 460m);

        var exception = Assert.Throws<ArgumentException>(
            () => person.IncreaseSalary(-1m));

        Assert.Equal(
            "Salary cannot be less than 460 dollars!",
            exception.Message);
        Assert.Equal(460m, person.Salary);
    }

    [Fact]
    public void ToString_FormatsSalaryWithTwoDecimalPlaces()
    {
        var person = new Person("Jane", "Doe", 30, 1234.5m);

        Assert.Equal(
            "Jane Doe receives 1234.50 dollars.",
            person.ToString());
    }
}