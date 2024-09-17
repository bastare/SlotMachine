namespace SlotMachine.Infrastructure.loC.Injectors.Abstractions;

using Microsoft.Extensions.DependencyInjection;

public interface IInjector
{
   abstract static void Inject ( IServiceCollection serviceCollection );
}