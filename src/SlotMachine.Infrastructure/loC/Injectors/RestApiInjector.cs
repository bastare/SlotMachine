namespace SlotMachine.Infrastructure.loC.Injectors;

using Microsoft.Extensions.DependencyInjection;
using Abstractions;
using Asp.Versioning;
using Microsoft.AspNetCore.Hosting;

public sealed class RestApiInjector : IInjector
{
	public static void Inject ( IServiceCollection serviceCollection , IWebHostEnvironment webHostEnvironment )
	{
		InjectVersionServices ();

		void InjectVersionServices ()
		{
			serviceCollection
				.AddApiVersioning ( setupAction =>
				{
					setupAction.DefaultApiVersion = new ( 1 , 0 );
					setupAction.ApiVersionReader = new UrlSegmentApiVersionReader ();
				} )
				.AddApiExplorer ( options =>
				{
					options.GroupNameFormat = "'v'V";
					options.SubstituteApiVersionInUrl = true;
				} );

			serviceCollection.AddEndpointsApiExplorer ();
		}
	}

	public static void RemoveInjection ( IServiceCollection serviceCollection )
	{
		throw new NotImplementedException ();
	}
}