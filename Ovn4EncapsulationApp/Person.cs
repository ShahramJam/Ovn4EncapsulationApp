using System;
using System.Globalization;

namespace Ovn4EncapsulationApp;

public sealed class Person
{

    public Person(
    string firstName,
    string lastName,
    int age,
    decimal salary)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Salary = salary;
    }
    public string FirstName
    {
        get;
        private set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
                throw new ArgumentException(
                    "First name cannot contain fewer than 3 symbols!");

            field = value;
        }
    }

    public string LastName
    {
        get;
        private set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
                throw new ArgumentException(
                    "Last name cannot contain fewer than 3 symbols!");

            field = value;
        }
    }

    public int Age
    {
        get;
        private set
        {
            if (value <= 0)
                throw new ArgumentException(
                    "Age cannot be zero or a negative integer!");

            field = value;
        }
    }

    public decimal Salary
    {
        get;
        private set
        {
            if (value < 460m)
                throw new ArgumentException(
                    "Salary cannot be less than 460 dollars!");

            field = value;
        }
    }

    public void IncreaseSalary(decimal percentage)
    {
        var effectivePercentage = Age < 30 ? percentage / 2m : percentage;
        Salary += Salary * effectivePercentage / 100m;
    }

    public override string ToString() =>
        string.Create(
            CultureInfo.InvariantCulture,
            $"{FirstName} {LastName} receives {Salary:F2} dollars.");
}
