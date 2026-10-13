using System;
using System.Collections.Generic;
using System.Globalization;

namespace Ovn4EncapsulationApp;

public sealed class PersonService : IPersonService
{
    public void Run()
    {
        if (!int.TryParse(Console.ReadLine(), out var count) || count < 1)
            return;

        var people = new List<Person>();

        for (var i = 0; i < count; i++)
        {
            var parts = Console.ReadLine()?
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts is not { Length: >= 4 })
                continue;

            try
            {
                var person = new Person(
                    parts[0],
                    parts[1],
                    int.Parse(parts[2], CultureInfo.InvariantCulture),
                    decimal.Parse(parts[3], CultureInfo.InvariantCulture));

                people.Add(person);
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid person data.");
            }
        }

        if (!decimal.TryParse(
                Console.ReadLine(),
                CultureInfo.InvariantCulture,
                out var bonus))
        {
            return;
        }

        foreach (var person in people)
            try
            {
                person.IncreaseSalary(bonus);
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            }

        foreach (var person in people)
            Console.WriteLine(person);
    }
}
