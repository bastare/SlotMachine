namespace SlotMachine.Application.UseCases.SlotMachine.Commands;

using MediatR;
using FluentValidation;
using Domain.Projections.MongoDb;
using Domain.Core.Entities;
using MongoDB.Driver;

public static class DeleteSlotMachine
{
	public sealed record DeleteSlotMachineCommand ( string SlotMachineId ) : IRequest<string>;

	internal sealed class CreatePlayerAmountCommandValidator : AbstractValidator<DeleteSlotMachineCommand>
	{
		public CreatePlayerAmountCommandValidator ()
		{
			RuleFor ( spinSlotMachineCommand => spinSlotMachineCommand.SlotMachineId )
				.NotNull ();
		}
	}

	internal sealed class DeleteSlotMachineHandler ( IUnitOfWork unit ) : IRequestHandler<DeleteSlotMachineCommand , string>
	{
		public async Task<string> Handle ( DeleteSlotMachineCommand request , CancellationToken cancellationToken )
		{
			using var session_ = await unit.Client.StartSessionAsync ( cancellationToken: cancellationToken );

			try
			{
				await unit.GetCollection<SlotMachine> ()
					.DeleteOneAsync ( slotMachine => slotMachine.Id == request.SlotMachineId , cancellationToken: cancellationToken );

				return request.SlotMachineId;
			}
			catch ( Exception )
			{
				await session_.AbortTransactionAsync ( cancellationToken );

				throw;
			}
		}
	}
}