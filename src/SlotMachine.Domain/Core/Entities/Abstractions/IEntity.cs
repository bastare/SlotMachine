namespace SlotMachine.Domain.Core.Entities.Abstractions;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public interface IEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    object Id { get; set; }
}

public interface IEntity<TKey> : IEntity
{
    new TKey Id { get; set; }
}

public interface IAuditableEntity<TKey> : IEntity<TKey>, IAuditable<TKey>
    where TKey : IComparable, IComparable<TKey>, IEquatable<TKey>
{ }