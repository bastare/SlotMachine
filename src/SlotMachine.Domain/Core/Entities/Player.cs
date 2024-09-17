namespace SlotMachine.Domain.Core.Entities;

using FluentValidation;
using FluentValidation.Results;
using Abstractions;
using Validators;
using System;
using System.Threading;
using System.Threading.Tasks;

public sealed class Player :
	IAuditableEntity<string>,
	IHasValidationAsync
{
	public required string Id { get; set; }

	public long Amount { get; init; }

	public required string CreatedBy { get; set; }

	public string? LastModifiedBy { get; set; }

	public DateTime Created { get; set; }

	public DateTime? LastModified { get; set; }

	object IEntity.Id
	{
		get => Id;
		set => Id = ( string ) value;
	}

	object IAuditable.CreatedBy
	{
		get => CreatedBy;
		set => CreatedBy = ( string ) value;
	}

	object? IAuditable.LastModifiedBy { get; set; }

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