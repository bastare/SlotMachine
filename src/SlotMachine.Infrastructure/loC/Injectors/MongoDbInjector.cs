namespace SlotMachine.Infrastructure.loC.Injectors;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Driver;
using Abstractions;
using Microsoft.AspNetCore.Hosting;

public sealed class MongoDbInjector : IInjector
{
    public static void Inject(IServiceCollection serviceCollection, IWebHostEnvironment webHostEnvironment)
    {
        serviceCollection.TryAddSingleton<IMongoClient>(provider =>
        {
            var connectionString = "mongodb://localhost:27017"; // replace with your connection string
            return new MongoClient(connectionString);
        });
    }

	public static void RemoveInjection ( IServiceCollection serviceCollection )
	{
		serviceCollection.RemoveAll<IMongoClient>();
	}
}