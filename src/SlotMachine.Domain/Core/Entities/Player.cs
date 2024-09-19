namespace SlotMachine.Domain.Core.Entities;

using FluentValidation;
using FluentValidation.Results;
using Abstractions;
using Validators;
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Rules;

public sealed class Player :
	IAuditableEntity<string>,
	IHasValidation
{
	[BsonId]
	[BsonElement ( "_id" )]
	[BsonRepresentation ( BsonType.ObjectId )]
	public string Id { get; set; }

	public long Amount { get; set; }

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

	public void Bet ( long bet )
	{
		if ( bet <= SlotMachineRules.BetRules.MinBet )
			throw new ArgumentOutOfRangeException ( nameof ( bet ) , $"Bet should be greater than {SlotMachineRules.BetRules.MinBet}" );

		if ( Amount < bet )
			throw new ArgumentOutOfRangeException ( nameof ( bet ) , "Player amount is not enough for the bet" );

		Amount -= bet;
	}

	public void AddPlayerAmount ( long amountToAdd )
	{
		if ( amountToAdd <= 0 )
			throw new ArgumentOutOfRangeException ( nameof ( amountToAdd ) , "Amount should be positive" );

		Amount += amountToAdd;
	}

	public bool IsValid ()
		=> Validate ()
			.IsValid;

	public void ValidateAndThrow ()
		=> new PlayerEntityValidator ()
			.ValidateAndThrow ( instance: this );

	public ValidationResult Validate ()
		=> new PlayerEntityValidator ()
			.Validate ( instance: this );
}