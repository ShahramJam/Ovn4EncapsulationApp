using Ovn4EncapsulationApp;

namespace Ovn4EncapsulationAppTest;

public sealed class PersonServiceTests
{
    [Theory]
    [InlineData("0")]
    [InlineData("not a number")]
    public void Run_WithInvalidCount_ReturnsWithoutOutput(string input)
    {
        var output = RunWithConsoleInput(input);

        Assert.Equal(string.Empty, output);
    }

    [Fact]
    public void Run_AppliesFullBonusToOlderPeopleAndHalfBonusToYoungerPeople()
    {
        const string input =
            "2\n" +
            "John Smith 29 2000\n" +
            "Alice Jones 30 1000\n" +
            "10\n";

        var output = RunWithConsoleInput(input);

        Assert.Equal(
            $"John Smith receives 2100.00 dollars.{Environment.NewLine}" +
            $"Alice Jones receives 1100.00 dollars.{Environment.NewLine}",
            output);
    }

    [Fact]
    public void Run_ReportsInvalidRowsAndPrintsValidPeople()
    {
        const string input =
            "4\n" +
            "too few\n" +
            "John Smith nope 1000\n" +
            "Al Li 30 1000\n" +
            "Jane Doe 30 1000\n" +
            "0\n";

        var output = RunWithConsoleInput(input);

        Assert.Equal(
            $"Invalid person data.{Environment.NewLine}" +
            $"First name cannot contain fewer than 3 symbols!{Environment.NewLine}" +
            $"Jane Doe receives 1000.00 dollars.{Environment.NewLine}",
            output);
    }

    [Fact]
    public void Run_WithInvalidBonus_ReturnsWithoutPrintingPeople()
    {
        const string input = "1\nJane Doe 30 1000\nnot a bonus\n";

        var output = RunWithConsoleInput(input);

        Assert.Equal(string.Empty, output);
    }

    [Fact]
    public void Run_WhenBonusWouldLowerSalaryBelowMinimum_ReportsErrorAndPrintsOriginalSalary()
    {
        const string input = "1\nJane Doe 30 500\n-20\n";

        var output = RunWithConsoleInput(input);

        Assert.Equal(
            $"Salary cannot be less than 460 dollars!{Environment.NewLine}" +
            $"Jane Doe receives 500.00 dollars.{Environment.NewLine}",
            output);
    }

    private static string RunWithConsoleInput(string input)
    {
        var originalIn = Console.In;
        var originalOut = Console.Out;
        using var reader = new StringReader(input);
        using var writer = new StringWriter();

        try
        {
            Console.SetIn(reader);
            Console.SetOut(writer);

            new PersonService().Run();

            return writer.ToString();
        }
        finally
        {
            Console.SetIn(originalIn);
            Console.SetOut(originalOut);
        }
    }
}
