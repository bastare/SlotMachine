namespace SlotMachine.Application.UseCases.Commands;

using MediatR;
using FluentValidation;
using OneOf;

public sealed record SpinSlotMachineCommand ( long Bet ) : IRequest<OneOf<Unit, Exception>>;

public sealed class SpinSlotMachineCommandValidator : AbstractValidator<SpinSlotMachineCommand>
{
    public SpinSlotMachineCommandValidator()
    {
        RuleFor(spinSlotMachineCommand=> spinSlotMachineCommand.Bet)
			.GreaterThan(0)
			.WithMessage("Bet should be greater than 0");
    }
}

public sealed class SpinSlotMachineHandler : IRequestHandler<SpinSlotMachineCommand, OneOf<Unit, Exception>>
{
	public async Task<OneOf<Unit , Exception>> Handle ( SpinSlotMachineCommand request , CancellationToken cancellationToken )
	{
		return Unit.Value;
	}
}