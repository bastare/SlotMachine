namespace SlotMachine.Domain.Core.Entities.Validators;

using FluentValidation;

public sealed class PlayerEntityValidator : AbstractValidator<Player>
{
	public PlayerEntityValidator ()
	{
		RuleFor ( player => player.Amount )
			.GreaterThan ( 0 );
	}
}