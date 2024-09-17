namespace SlotMachine.Domain.Core.Entities;

using FluentValidation;
using FluentValidation.Results;
using Abstractions;
using Validators;
using System;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;

public sealed class Player :
	IAuditableEntity<ObjectId>,
	IHasValidationAsync
{
	public ObjectId Id { get; set; }

	public long Amount { get; set; }

	public ObjectId CreatedBy { get; set; }

	public ObjectId LastModifiedBy { get; set; }

	public DateTime Created { get; set; }

	public DateTime? LastModified { get; set; }

	object IEntity.Id
	{
		get => Id;
		set => Id = ( ObjectId ) value;
	}

	object IAuditable.CreatedBy
	{
		get => CreatedBy;
		set => CreatedBy = ( ObjectId ) value;
	}

	object? IAuditable.LastModifiedBy
	{
		get => LastModifiedBy;
		set => LastModifiedBy = ( ObjectId ) value!;
	}

	public async Task<bool> IsValidAsync ( CancellationToken cancellationToken = default )
	{
		var result_ = await ValidateAsync ( cancellationToken );

		return result_.IsValid;
	}

	public async Task ValidateAndThrowAsync ( CancellationToken cancellationToken = default )
		=> await new PlayerEntityValidator ()
			.ValidateAndThrowAsync ( instance: this , cancellationToken );

	public async Task<ValidationResult> ValidateAsync ( CancellationToken cancellationToken = default )
		=> await new PlayerEntityValidator ()
			.ValidateAsync ( instance: this , cancellationToken );
}