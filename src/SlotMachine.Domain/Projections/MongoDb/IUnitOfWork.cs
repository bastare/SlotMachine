namespace SlotMachine.Domain.Projections.MongoDb;

using MongoDB.Driver;
using Core.Entities.Abstractions;

public interface IUnitOfWork
{
	IMongoCollection<TEntity> GetCollection<TEntity> () where TEntity : IEntity;

	IMongoClient Client { get; }

	IMongoDatabase Database { get; }
}