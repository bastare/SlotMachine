namespace SlotMachine.Domain.Core.Entities.Validators;

using FluentValidation;
using Entities;

public sealed class PlayerEntityValidator : AbstractValidator<Player>
{
	public PlayerEntityValidator ()
	{
	}
}