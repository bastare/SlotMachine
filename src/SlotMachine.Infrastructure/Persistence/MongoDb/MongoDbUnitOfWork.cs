namespace SlotMachine.Infrastructure.Persistence.MongoDb;

using MongoDB.Driver;
using Domain.Core.Entities.Abstractions;
using Domain.Projections.MongoDb;
using Microsoft.Extensions.Configuration;

public sealed class MongoDbUnitOfWork ( IMongoClient mongoClient , IConfiguration configuration ) : IUnitOfWork
{
	public IMongoClient Client { get; } = mongoClient;

	public IMongoDatabase Database { get; } = mongoClient.GetDatabase ( configuration.GetConnectionString ( "Database" ) );

	public IMongoCollection<TEntity> GetCollection<TEntity> ()
		where TEntity : IEntity
			=> Database.GetCollection<TEntity> ( typeof ( TEntity ).Name );
}