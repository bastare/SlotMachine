namespace SlotMachine.Infrastructure.CrossCutting.loC;

using Microsoft.Extensions.DependencyInjection;
using Infrastructure.loC.Injectors;
using Microsoft.Extensions.Configuration;

public static class InjectBootstrapper
{
	public static void InjectLayersDependency ( this IServiceCollection serviceCollection , IConfiguration configuration )
	{
		AddHttpContextAccessorInjector.Inject ( serviceCollection , configuration );
		FluentValidatorInjector.Inject ( serviceCollection , configuration );
		MediatorInjector.Inject ( serviceCollection , configuration );
		MongoDbInjector.Inject ( serviceCollection , configuration );
		RestApiInjector.Inject ( serviceCollection , configuration );
	}
}
