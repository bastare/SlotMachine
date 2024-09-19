namespace SlotMachine.Application.UseCases.SlotMachine.Commands;

using MediatR;
using FluentValidation;
using Domain.Projections.MongoDb;
using Domain.Core.Entities;
using Domain.Rules;
using Domain.Aggregate;
using Domain.Matrix;
using Domain.Aggregate.Adapters;

public static class CreateSlotMachine
{
	public sealed record CreateSlotMachineCommand ( int Height , int Width ) : IRequest<CreatedSlotMachineResponse>;

	public sealed record CreatedSlotMachineResponse ( string SlotMachineId , int Width , int Height , List<int[][]> WinPaths );

	internal sealed class CreatePlayerAmountCommandValidator : AbstractValidator<CreateSlotMachineCommand>
	{
		public CreatePlayerAmountCommandValidator ()
		{
			RuleFor ( createSlotMachineCommand => createSlotMachineCommand.Height )
				.GreaterThanOrEqualTo ( SlotMachineRules.MatrixSizeRules.MinHeight )
				.WithMessage ( $"Height should be greater than {SlotMachineRules.MatrixSizeRules.MinHeight}" );

			RuleFor ( createSlotMachineCommand => createSlotMachineCommand.Width )
				.GreaterThan ( SlotMachineRules.MatrixSizeRules.MinWidth )
				.WithMessage ( $"Width should be greater than {SlotMachineRules.MatrixSizeRules.MinWidth}" );
		}
	}

	internal sealed class CreateSlotMachineHandler ( IUnitOfWork unit ) :
		IRequestHandler<CreateSlotMachineCommand , CreatedSlotMachineResponse>
	{
		public async Task<CreatedSlotMachineResponse> Handle ( CreateSlotMachineCommand request , CancellationToken cancellationToken )
		{
			using var session_ = await unit.Client.StartSessionAsync ( cancellationToken: cancellationToken );

			try
			{
				var slotMachineForCreation_ = new SlotMachine ();

				var slotMachineConfiguratorAggregate_ =
					new SlotMachineConfiguratorAggregate (
						slotMachineForCreation_ ,
						new SlotMachineMatrixGeneratorAdapter ( new MatrixGenerator ( request.Width , request.Height ) ) );

				slotMachineConfiguratorAggregate_.GenerateWinLines ();

				await unit.GetCollection<SlotMachine> ()
					.InsertOneAsync ( slotMachineForCreation_ , cancellationToken: cancellationToken );

				return new CreatedSlotMachineResponse (
					slotMachineForCreation_.Id ,
					slotMachineForCreation_.Width ,
					slotMachineForCreation_.Height ,
					slotMachineForCreation_.WinLines! );
			}
			catch ( Exception )
			{
				await session_.AbortTransactionAsync ( cancellationToken );

				throw;
			}
		}
	}
}