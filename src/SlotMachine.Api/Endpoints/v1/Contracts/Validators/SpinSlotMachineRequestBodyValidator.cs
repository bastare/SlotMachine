namespace SlotMachine.Api.Endpoints.v1.Contracts.Validators;

using FluentValidation;

public sealed class SpinSlotMachineRequestBodyValidator : AbstractValidator<SpinSlotMachineRequestBody>
{
	public SpinSlotMachineRequestBodyValidator ()
	{
		RuleFor ( spinSlotMachineRequestBody => spinSlotMachineRequestBody.Bet )
			.GreaterThan ( 0 );
	}
}