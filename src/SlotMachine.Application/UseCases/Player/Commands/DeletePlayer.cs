namespace SlotMachine.Application.UseCases.Player.Commands;

using MediatR;
using FluentValidation;
using Domain.Projections.MongoDb;
using Domain.Core.Entities;
using MongoDB.Driver;

public static class DeletePlayer
{
	public sealed record DeletePlayerCommand ( string PlayerId ) : IRequest<string>;

	internal sealed class CreatePlayerAmountCommandValidator : AbstractValidator<DeletePlayerCommand>
	{
		public CreatePlayerAmountCommandValidator ()
		{
			RuleFor ( spinSlotMachineCommand => spinSlotMachineCommand.PlayerId )
				.NotNull ();
		}
	}

	internal sealed class DeletePlayerHandler ( IUnitOfWork unit ) : IRequestHandler<DeletePlayerCommand , string>
	{
		public async Task<string> Handle ( DeletePlayerCommand request , CancellationToken cancellationToken )
		{
			using var session_ = await unit.Client.StartSessionAsync ( cancellationToken: cancellationToken );

			try
			{
				await unit.GetCollection<Player> ()
					.DeleteOneAsync ( x => x.Id == request.PlayerId , cancellationToken: cancellationToken );

				return request.PlayerId;
			}
			catch ( Exception )
			{
				await session_.AbortTransactionAsync ( cancellationToken );

				throw;
			}
		}
	}
}