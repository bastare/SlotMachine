namespace SlotMachine.Infrastructure.loC.Injectors.Abstractions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public interface IInjector
{
	abstract static void Inject ( IServiceCollection serviceCollection , IConfiguration configuration );

	public static void RemoveInjection ( IServiceCollection _ ) { }
}