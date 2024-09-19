namespace SlotMachine.Domain.Aggregate;

using Core.Entities;
using Rules;
using Abstractions;
using static Abstractions.ISlotMachineSessionAggregate;

public sealed class SlotMachineAggregate ( Player player , SlotMachine slotMachine , IGenerateWinMatrix winMatrixGenerator , ICalculateWinAmount calculateWinAmount ) :
	ISlotMachineSessionAggregate
{
	public SpinResponse Spin ( long bet )
	{
		if ( bet <= SlotMachineRules.BetRules.MinBet )
			throw new ArgumentOutOfRangeException ( nameof ( bet ) , $"Bet should be greater or equal than {SlotMachineRules.BetRules.MinBet}" );

		player.Bet ( bet );

		var winMatrix_ = winMatrixGenerator.GenerateResultMatrix ();
		var winAmount_ = calculateWinAmount.CalculateWinAmount ( winMatrix_ , slotMachine.WinLines! , bet );

		player.AddPlayerAmount ( winAmount_ );

		return new ( player.Amount , winAmount_ , winMatrix_ );
	}
}