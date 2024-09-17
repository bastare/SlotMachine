namespace SlotMachine.Infrastructure.loC.Injectors;

using Microsoft.Extensions.DependencyInjection;
using Abstractions;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

public sealed class AddHttpContextAccessorInjector : IInjector
{
    public static void Inject(IServiceCollection serviceCollection, IWebHostEnvironment webHostEnvironment)
    {
        serviceCollection.AddHttpContextAccessor ();
    }

	public static void RemoveInjection ( IServiceCollection serviceCollection )
	{
		serviceCollection.RemoveAll<IHttpContextAccessor>();
	}
}