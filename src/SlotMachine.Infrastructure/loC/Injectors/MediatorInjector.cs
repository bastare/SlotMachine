namespace SlotMachine.Infrastructure.loC.Injectors;

using Microsoft.Extensions.DependencyInjection;
using Abstractions;
using Application;
using Domain;
using Microsoft.Extensions.Configuration;

public sealed class MediatorInjector : IInjector
{
	public static void Inject ( IServiceCollection serviceCollection , IConfiguration _ )
	{
		serviceCollection.AddMediatR ( mediatRServiceConfiguration =>
			mediatRServiceConfiguration
				.RegisterServicesFromAssemblies (
					typeof ( IApplicationMarker ).Assembly ,
					typeof ( IDomainMarker ).Assembly
				)
		);
	}
}