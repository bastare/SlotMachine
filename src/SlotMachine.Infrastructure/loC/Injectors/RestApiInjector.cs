namespace SlotMachine.Infrastructure.loC.Injectors;

using Microsoft.Extensions.DependencyInjection;
using Abstractions;
using Asp.Versioning;
using Microsoft.Extensions.Configuration;

public sealed class RestApiInjector : IInjector
{
	public static void Inject ( IServiceCollection serviceCollection , IConfiguration _ )
	{
		InjectVersionServices ();

		serviceCollection.AddSwaggerGen ();

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
}