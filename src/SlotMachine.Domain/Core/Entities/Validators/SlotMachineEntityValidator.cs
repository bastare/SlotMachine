namespace SlotMachine.Domain.Core.Entities.Validators;

using FluentValidation;

public sealed class SlotMachineEntityValidator : AbstractValidator<SlotMachine>
{
	public SlotMachineEntityValidator ()
	{
		RuleFor ( slotMachine => slotMachine.WinLines )
			.NotEmpty ();

		RuleFor ( slotMachine => slotMachine.Height )
			.GreaterThan ( 2 )
			.NotEmpty ();

		RuleFor ( slotMachine => slotMachine.Width )
			.GreaterThan ( 2 )
			.NotEmpty ();
	}
}