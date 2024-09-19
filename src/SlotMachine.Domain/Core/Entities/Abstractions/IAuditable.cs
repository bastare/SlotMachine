namespace SlotMachine.Domain.Core.Entities.Abstractions;

public interface IAuditable<TKey> : IAuditable
	where TKey : IComparable<TKey>, IEquatable<TKey>
{
	new TKey CreatedBy { get; set; }

	new TKey? LastModifiedBy { get; set; }
}

public interface IAuditable
{
	object CreatedBy { get; set; }

	DateTime Created { get; set; }

	object? LastModifiedBy { get; set; }

	DateTime? LastModified { get; set; }
}