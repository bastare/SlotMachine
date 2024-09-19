namespace SlotMachine.Domain.Core.Entities.Abstractions;

using FluentValidation.Results;

public interface IHasValidationAsync
{
	Task<ValidationResult> ValidateAsync ( CancellationToken cancellationToken = default );

	Task ValidateAndThrowAsync ( CancellationToken cancellationToken = default );

	Task<bool> IsValidAsync ( CancellationToken cancellationToken = default );
}