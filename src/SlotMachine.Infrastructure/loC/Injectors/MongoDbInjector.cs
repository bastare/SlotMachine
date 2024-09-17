namespace SlotMachine.Infrastructure.loC.Injectors;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Driver;
using Abstractions;

public sealed class Injector : IInjector
{
    public static void Inject(IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddSingleton<IMongoClient>(provider =>
        {
            var connectionString = "mongodb://localhost:27017"; // replace with your connection string
            return new MongoClient(connectionString);
        });
    }
}