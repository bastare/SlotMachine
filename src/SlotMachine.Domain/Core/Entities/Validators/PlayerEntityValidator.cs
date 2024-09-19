namespace SlotMachine.Domain.Core.Entities.Validators;

using FluentValidation;

internal sealed class PlayerEntityValidator : AbstractValidator<Player>
{
	public PlayerEntityValidator ()
	{
		RuleFor ( player => player.Amount )
			.GreaterThanOrEqualTo ( 0 )
			.WithMessage ( "Amount should be positive" );
	}
}