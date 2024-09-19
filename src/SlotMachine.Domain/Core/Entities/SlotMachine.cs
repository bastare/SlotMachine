namespace SlotMachine.Domain.Core.Entities;

using FluentValidation;
using FluentValidation.Results;
using Abstractions;
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Validators;

public sealed class SlotMachine :
	IAuditableEntity<string>,
	IHasValidation
{
	[BsonId]
	[BsonElement ( "_id" )]
	[BsonRepresentation ( BsonType.ObjectId )]
	public string Id { get; set; }

	public int Height { get; set; }

	public int Width { get; set; }

	public List<int[][]>? WinLines { get; set; } = [];

	public string CreatedBy { get; set; }

	public string LastModifiedBy { get; set; }

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

	object? IAuditable.LastModifiedBy
	{
		get => LastModifiedBy;
		set => LastModifiedBy = ( string ) value!;
	}

	public bool IsValid ()
		=> Validate ().IsValid;

	public void ValidateAndThrow ()
		=> new SlotMachineEntityValidator ()
			.ValidateAndThrow ( instance: this );

	public ValidationResult Validate ()
		=> new SlotMachineEntityValidator ()
			.Validate ( instance: this );
}