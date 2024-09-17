namespace SlotMachine.Domain.Core.Entities.Abstractions;

public interface IEntity
{
    object Id { get; set; }
}

public interface IEntity<TKey> : IEntity
{
    new TKey Id { get; set; }
}

public interface IAuditableEntity<TKey> : IEntity<TKey>, IAuditable<TKey>
    where TKey : IComparable<TKey>, IEquatable<TKey>
{ }