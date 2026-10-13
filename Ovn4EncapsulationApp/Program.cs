using Microsoft.Extensions.DependencyInjection;

namespace Ovn4EncapsulationApp;


public static class Program
{
    public static void Main()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IPersonService, PersonService>();

        using var provider = services.BuildServiceProvider();
        provider.GetRequiredService<IPersonService>().Run();
    }
}