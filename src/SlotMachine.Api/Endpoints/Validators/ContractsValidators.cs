namespace SlotMachine.Api.Endpoints.Validators;

using FluentValidation;
using Domain.Rules;
using static v1.SlotMachineEndpoints;
using static v1.PlayerEndpoints;

internal static class ContractsValidators
{
	internal sealed class AddPlayerBalanceRequestBodyValidator : AbstractValidator<AddPlayerBalanceRequestBody>
	{
		public AddPlayerBalanceRequestBodyValidator ()
		{
			RuleFor ( updatePlayerBalanceRequestBody => updatePlayerBalanceRequestBody.PlayerId )
				.NotEmpty ()
				.WithMessage ( "PlayerId should not be empty" );

			RuleFor ( updatePlayerBalanceRequestBody => updatePlayerBalanceRequestBody.Amount )
				.GreaterThan ( 0 );
		}
	}

	internal sealed class SpinSlotMachineRequestBodyValidator : AbstractValidator<SpinSlotMachineRequestBody>
	{
		public SpinSlotMachineRequestBodyValidator ()
		{
			RuleFor ( spinSlotMachineCommand => spinSlotMachineCommand.PlayerId )
				.NotEmpty ()
				.WithMessage ( "PlayerId should not be empty" );

			RuleFor ( spinSlotMachineCommand => spinSlotMachineCommand.SlotMachineId )
				.NotEmpty ()
				.WithMessage ( "SlotMachineId should not be empty" );

			RuleFor ( spinSlotMachineCommand => spinSlotMachineCommand.Bet )
				.GreaterThanOrEqualTo ( SlotMachineRules.BetRules.MinBet )
				.WithMessage ( $"Bet should be greater than {SlotMachineRules.BetRules.MinBet}" );
		}
	}

	internal sealed class CreateSlotMachineRequestBodyValidator : AbstractValidator<CreateSlotMachineRequestBody>
	{
		public CreateSlotMachineRequestBodyValidator ()
		{
			RuleFor ( slotMachine => slotMachine.MatrixHeight )
				.GreaterThanOrEqualTo ( SlotMachineRules.MatrixSizeRules.MinHeight )
				.WithMessage ( $"Height should be greater than {SlotMachineRules.MatrixSizeRules.MinHeight}" );

			RuleFor ( slotMachine => slotMachine.MatrixWidth )
				.GreaterThan ( SlotMachineRules.MatrixSizeRules.MinWidth )
				.WithMessage ( $"Width should be greater than {SlotMachineRules.MatrixSizeRules.MinWidth}" );
		}
	}
}