namespace SlotMachine.Domain.Core.Entities.Abstractions;

using FluentValidation.Results;

public interface IHasValidation
{
	ValidationResult Validate ();

	void ValidateAndThrow ();

	bool IsValid ();
}