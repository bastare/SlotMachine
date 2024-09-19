namespace SlotMachine.Application.UseCases.Player.Commands;

using MediatR;
using FluentValidation;
using Domain.Projections.MongoDb;
using Domain.Core.Entities;

public static class CreatePlayer
{
	public sealed record CreatePlayerCommand ( long Amount ) : IRequest<CreatedPlayerResponse>;

	public sealed record CreatedPlayerResponse ( string PlayerId , long Amount );

	internal sealed class CreatePlayerAmountCommandValidator : AbstractValidator<CreatePlayerCommand>
	{
		public CreatePlayerAmountCommandValidator ()
		{
			RuleFor ( spinSlotMachineCommand => spinSlotMachineCommand.Amount )
				.GreaterThanOrEqualTo ( 0 )
				.WithMessage ( $"Amount should be positive" );
		}
	}

	internal sealed class PlayerAmountAfterTransactionValidator : AbstractValidator<CreatedPlayerResponse>
	{
		public PlayerAmountAfterTransactionValidator ()
		{
			RuleFor ( spinSlotMachineCommand => spinSlotMachineCommand.PlayerId )
				.NotEmpty ()
				.WithMessage ( "PlayerId should not be empty" );

			RuleFor ( spinSlotMachineCommand => spinSlotMachineCommand.Amount )
				.GreaterThanOrEqualTo ( 0 )
				.WithMessage ( $"Amount should be positive" );
		}
	}

	internal sealed class CreatePlayerHandler ( IUnitOfWork unit ) : IRequestHandler<CreatePlayerCommand , CreatedPlayerResponse>
	{
		public async Task<CreatedPlayerResponse> Handle ( CreatePlayerCommand request , CancellationToken cancellationToken )
		{
			using var session_ = await unit.Client.StartSessionAsync ( cancellationToken: cancellationToken );

			try
			{
				var newPlayer_ = new Player ();
				newPlayer_.AddPlayerAmount ( request.Amount );

				await unit.GetCollection<Player> ()
					.InsertOneAsync ( newPlayer_ , cancellationToken: cancellationToken );

				return new CreatedPlayerResponse (
					newPlayer_.Id ,
					newPlayer_.Amount );
			}
			catch ( Exception )
			{
				await session_.AbortTransactionAsync ( cancellationToken );

				throw;
			}
		}
	}
}