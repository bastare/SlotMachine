namespace SlotMachine.Application.UseCases.SlotMachine.Commands;

using MediatR;
using FluentValidation;
using Domain.Rules;
using Domain.Aggregate;
using Domain.Projections.MongoDb;
using Domain.Core.Entities;
using Domain.Matrix;
using Domain.Aggregate.Adapters;
using Domain.Http.Exceptions;
using Domain.Extensions;
using MongoDB.Driver;

public static class SpinSlotMachine
{
	public sealed record SpinSlotMachineCommand ( string PlayerId , string SlotMachineId , long Bet ) : IRequest<SpinSlotMachineResponse>;

	public sealed record SpinSlotMachineResponse ( string PlayerId , string SlotMachineId , int[][] WinMatrix , long WinAmount );

	internal sealed class SpinSlotMachineCommandValidator : AbstractValidator<SpinSlotMachineCommand>
	{
		public SpinSlotMachineCommandValidator ()
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

	internal sealed class SpinSlotMachineResponseValidator : AbstractValidator<SpinSlotMachineResponse>
	{
		public SpinSlotMachineResponseValidator ()
		{
			RuleFor ( spinSlotMachineCommand => spinSlotMachineCommand.PlayerId )
				.NotEmpty ()
				.WithMessage ( "PlayerId should not be empty" );

			RuleFor ( spinSlotMachineCommand => spinSlotMachineCommand.SlotMachineId )
				.NotEmpty ()
				.WithMessage ( "SlotMachineId should not be empty" );

			RuleFor ( spinSlotMachineCommand => spinSlotMachineCommand.WinMatrix )
				.NotEmpty ()
				.WithMessage ( "WinMatrix should not be empty" );

			RuleFor ( spinSlotMachineCommand => spinSlotMachineCommand.WinAmount )
				.GreaterThanOrEqualTo ( 0 )
				.WithMessage ( "WinAmount should be positive" );
		}
	}

	internal sealed class SpinSlotMachineHandler ( IUnitOfWork unit ) :
		IRequestHandler<SpinSlotMachineCommand , SpinSlotMachineResponse>
	{
		public async Task<SpinSlotMachineResponse> Handle ( SpinSlotMachineCommand request , CancellationToken cancellationToken )
		{
			using var session_ = await unit.Client.StartSessionAsync ( cancellationToken: cancellationToken );

			try
			{
				var playerForSpin_ =
					await unit.GetCollection<Player> ()
						.Find ( player => player.Id == request.PlayerId )
						.FirstOrDefaultAsync ( cancellationToken ) ??
							throw new NotFoundException ( "The player does not exist" );

				var slotMachineForSpin_ =
					await unit.GetCollection<SlotMachine> ()
						.Find ( slotMachine => slotMachine.Id == request.SlotMachineId )
						.FirstOrDefaultAsync ( cancellationToken ) ??
							throw new NotFoundException ( "The slot machine does not exist" );

				var slotMachineAggregate_ =
					new SlotMachineAggregate (
						playerForSpin_ ,
						slotMachineForSpin_ ,
						winMatrixGenerator: new SlotMachineMatrixGeneratorAdapter (
							new MatrixGenerator ( slotMachineForSpin_.Width , slotMachineForSpin_.Height ) ) ,
						new SlotMachinePathConsecutiveTraversalAdapter () );

				var spinResult_ = slotMachineAggregate_.Spin ( request.Bet );

				await unit.GetCollection<Player> ()
					.ReplaceOneAsync (
						player => player.Id == playerForSpin_.Id ,
						playerForSpin_ ,
						cancellationToken: cancellationToken );

				await unit.GetCollection<SlotMachine> ()
					.ReplaceOneAsync (
						slotMachine => slotMachine.Id == slotMachineForSpin_.Id ,
						slotMachineForSpin_ ,
						cancellationToken: cancellationToken );

				return new SpinSlotMachineResponse (
					playerForSpin_.Id ,
					slotMachineForSpin_.Id ,
					spinResult_.WinMatrix.ToJaggedArray () ,
					spinResult_.WinAmount );

			}
			catch ( Exception )
			{
				await session_.AbortTransactionAsync ( cancellationToken );

				throw;
			}
			finally
			{
				session_.Dispose ();
			}
		}
	}
}