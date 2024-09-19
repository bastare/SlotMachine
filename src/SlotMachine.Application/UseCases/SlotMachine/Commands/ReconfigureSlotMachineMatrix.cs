namespace SlotMachine.Application.UseCases.SlotMachine.Commands;

using MediatR;
using FluentValidation;
using Domain.Projections.MongoDb;
using Domain.Core.Entities;
using Domain.Rules;
using Domain.Aggregate;
using Domain.Matrix;
using Domain.Aggregate.Adapters;
using MongoDB.Driver;
using global::SlotMachine.Domain.Http.Exceptions;

public static class ReconfigureSlotMachineMatrix
{
	public sealed record ReconfigureSlotMachineMatrixCommand ( string SlotMachineId , int Height , int Width ) : IRequest<ReconfigureSlotMachineResponse>;

	public sealed record ReconfigureSlotMachineResponse ( string SlotMachineId , int Width , int Height , List<int[][]> WinPaths );

	internal sealed class ReconfigureSlotMachineMatrixCommandValidator : AbstractValidator<ReconfigureSlotMachineMatrixCommand>
	{
		public ReconfigureSlotMachineMatrixCommandValidator ()
		{
			RuleFor ( reconfigureSlotsMachineMatrixCommand => reconfigureSlotsMachineMatrixCommand.Height )
				.GreaterThanOrEqualTo ( SlotMachineRules.MatrixSizeRules.MinHeight )
				.WithMessage ( $"Height should be greater than {SlotMachineRules.MatrixSizeRules.MinHeight}" );

			RuleFor ( reconfigureSlotsMachineMatrixCommand => reconfigureSlotsMachineMatrixCommand.Width )
				.GreaterThan ( SlotMachineRules.MatrixSizeRules.MinWidth )
				.WithMessage ( $"Width should be greater than {SlotMachineRules.MatrixSizeRules.MinWidth}" );
		}
	}

	internal sealed class ReconfigureSlotMachineMatrixHandler ( IUnitOfWork unit ) :
		IRequestHandler<ReconfigureSlotMachineMatrixCommand , ReconfigureSlotMachineResponse>
	{
		public async Task<ReconfigureSlotMachineResponse> Handle ( ReconfigureSlotMachineMatrixCommand request , CancellationToken cancellationToken )
		{
			using var session_ = await unit.Client.StartSessionAsync ( cancellationToken: cancellationToken );

			try
			{
				var slotMachineForReconfiguration_ =
					await unit.GetCollection<SlotMachine> ()
						.Find ( slotMachine => slotMachine.Id == request.SlotMachineId )
						.FirstOrDefaultAsync ( cancellationToken ) ??
							throw new NotFoundException ( "The slot machine does not exist" ); ;

				var slotMachineConfiguratorAggregate_ =
					new SlotMachineConfiguratorAggregate (
						slotMachineForReconfiguration_ ,
						new SlotMachineMatrixGeneratorAdapter ( new MatrixGenerator ( request.Width , request.Height ) ) );

				slotMachineConfiguratorAggregate_.GenerateWinLines ();

				await unit.GetCollection<SlotMachine> ()
					.ReplaceOneAsync (
						slotMachine => slotMachine.Id == slotMachineForReconfiguration_.Id ,
						slotMachineForReconfiguration_ ,
						cancellationToken: cancellationToken );

				return new ReconfigureSlotMachineResponse (
					slotMachineForReconfiguration_.Id ,
					slotMachineForReconfiguration_.Width ,
					slotMachineForReconfiguration_.Height ,
					slotMachineForReconfiguration_.WinLines! );
			}
			catch ( Exception )
			{
				await session_.AbortTransactionAsync ( cancellationToken );

				throw;
			}
		}
	}
}