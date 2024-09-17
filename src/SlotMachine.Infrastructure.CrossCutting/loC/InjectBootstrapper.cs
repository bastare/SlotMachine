namespace SlotMachine.Infrastructure.CrossCutting.loC;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.loC.Injectors;

public static class InjectBootstrapper
{
	public static void InjectLayersDependency ( this IServiceCollection serviceCollection , IWebHostEnvironment webHostEnvironment )
	{
		AddHttpContextAccessorInjector.Inject ( serviceCollection , webHostEnvironment );
		MongoDbInjector.Inject ( serviceCollection , webHostEnvironment );
		RestApiInjector.Inject ( serviceCollection , webHostEnvironment );
	}
}
