namespace SlotMachine.Infrastructure.loC.Injectors;

using Microsoft.Extensions.DependencyInjection;
using Abstractions;

public sealed class AddHttpContextAccessorInjector : IInjector
{
    public static void Inject(IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpContextAccessor ();
    }
}