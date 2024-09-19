namespace SlotMachine.Infrastructure.loC.Injectors;

using Microsoft.Extensions.DependencyInjection;
using Abstractions;
using FluentValidation.AspNetCore;
using FluentValidation;
using Application;
using Domain;
using Microsoft.Extensions.Configuration;
using System.Reflection;

public sealed class FluentValidatorInjector : IInjector
{
	public static void Inject ( IServiceCollection serviceCollection , IConfiguration configuration )
	{
		serviceCollection.AddFluentValidationAutoValidation ();
		serviceCollection.AddFluentValidationClientsideAdapters ();
		serviceCollection.AddValidatorsFromAssemblies ( [
			Assembly.GetEntryAssembly () ,
			typeof(IDomainMarker).Assembly,
			typeof(IApplicationMarker).Assembly,
		] );
	}
}