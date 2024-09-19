namespace SlotMachine.Domain.Core.Entities.Validators;

using FluentValidation;
using Rules;

internal sealed class SlotMachineEntityValidator : AbstractValidator<SlotMachine>
{
	public SlotMachineEntityValidator ()
	{
		RuleFor ( slotMachine => slotMachine.WinLines )
			.NotEmpty ()
			.WithMessage ( "WinLines should not be empty" );

		RuleFor ( slotMachine => slotMachine.Height )
			.GreaterThanOrEqualTo ( SlotMachineRules.MatrixSizeRules.MinHeight )
			.WithMessage ( $"Height should be greater than {SlotMachineRules.MatrixSizeRules.MinHeight}" );

		RuleFor ( slotMachine => slotMachine.Width )
			.GreaterThan ( SlotMachineRules.MatrixSizeRules.MinWidth )
			.WithMessage ( $"Width should be greater than {SlotMachineRules.MatrixSizeRules.MinWidth}" );
	}
}