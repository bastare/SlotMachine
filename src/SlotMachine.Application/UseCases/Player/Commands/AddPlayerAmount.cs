namespace SlotMachine.Application.UseCases.Player.Commands;

using MediatR;
using FluentValidation;
using Domain.Projections.MongoDb;
using Domain.Core.Entities;
using MongoDB.Driver;
using Domain.Http.Exceptions;

public static class AddPlayerAmount
{
	public sealed record AddPlayerAmountCommand ( string PlayerId , long Amount ) : IRequest<PlayerAmountAfterTransactionResponse>;

	public sealed record PlayerAmountAfterTransactionResponse ( string PlayerId , long Amount );

	internal sealed class AddPlayerAmountCommandValidator : AbstractValidator<AddPlayerAmountCommand>
	{
		public AddPlayerAmountCommandValidator ()
		{
			RuleFor ( spinSlotMachineCommand => spinSlotMachineCommand.PlayerId )
				.NotEmpty ()
				.WithMessage ( "PlayerId should not be empty" );

			RuleFor ( spinSlotMachineCommand => spinSlotMachineCommand.Amount )
				.GreaterThanOrEqualTo ( 0 )
				.WithMessage ( $"Amount should be positive" );
		}
	}

	internal sealed class PlayerAmountAfterTransactionValidator : AbstractValidator<PlayerAmountAfterTransactionResponse>
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

	internal sealed class AddPlayerAmountHandler ( IUnitOfWork unit ) :
		IRequestHandler<AddPlayerAmountCommand , PlayerAmountAfterTransactionResponse>
	{
		public async Task<PlayerAmountAfterTransactionResponse> Handle ( AddPlayerAmountCommand request , CancellationToken cancellationToken )
		{
			using var session_ = await unit.Client.StartSessionAsync ( cancellationToken: cancellationToken );

			try
			{
				var playerForTransaction_ =
					await unit.GetCollection<Player> ()
						.Find ( player => player.Id == request.PlayerId )
						.FirstOrDefaultAsync ( cancellationToken ) ??
							throw new NotFoundException ( "The player does not exist" );

				playerForTransaction_.AddPlayerAmount ( request.Amount );

				await unit.GetCollection<Player> ()
					.ReplaceOneAsync (
						player => player.Id == request.PlayerId ,
						playerForTransaction_ ,
						cancellationToken: cancellationToken );

				await session_.CommitTransactionAsync ( cancellationToken );

				return new PlayerAmountAfterTransactionResponse (
					playerForTransaction_.Id ,
					playerForTransaction_.Amount );
			}
			catch ( Exception )
			{
				await session_.AbortTransactionAsync ( cancellationToken );

				throw;
			}
		}
	}
}