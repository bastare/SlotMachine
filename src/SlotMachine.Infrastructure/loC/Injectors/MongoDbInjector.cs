namespace SlotMachine.Infrastructure.loC.Injectors;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Driver;
using Abstractions;
using Domain.Projections.MongoDb;
using Persistence.MongoDb;
using Microsoft.Extensions.Configuration;

public sealed class MongoDbInjector : IInjector
{
	public static void Inject ( IServiceCollection serviceCollection , IConfiguration configuration )
	{
		serviceCollection.TryAddSingleton<IMongoClient> (
			provider => new MongoClient ( configuration.GetConnectionString ( "MongoDB" ) ) );

		serviceCollection.TryAddSingleton<IUnitOfWork , MongoDbUnitOfWork> ();
	}

	public static void RemoveInjection ( IServiceCollection serviceCollection )
	{
		serviceCollection.RemoveAll<IMongoClient> ();
		serviceCollection.RemoveAll<IUnitOfWork> ();
	}
}