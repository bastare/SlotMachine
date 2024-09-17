namespace SlotMachine.Api.Endpoints.v1.Contracts.Validators;

using FluentValidation;

public sealed class UpdatePlayerBalanceRequestBodyValidator : AbstractValidator<UpdatePlayerBalanceRequestBody>
{
	public UpdatePlayerBalanceRequestBodyValidator ()
	{
		RuleFor ( updatePlayerBalanceRequestBody => updatePlayerBalanceRequestBody.Amount )
			.GreaterThan ( 0 );
	}
}