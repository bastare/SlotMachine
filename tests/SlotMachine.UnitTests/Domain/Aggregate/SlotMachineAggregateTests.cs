namespace SlotMachine.UnitTests.Domain.Aggregate;

using SlotMachine.Domain.Rules;
using SlotMachine.Domain.Matrix;
using SlotMachine.Domain.Aggregate.Adapters;
using SlotMachine.Domain.Aggregate;
using SlotMachine.Domain.Core.Entities;
using SlotMachine.CoreTests.Stabs;
using SlotMachine.CoreTests.Rules;

public sealed class SlotMachineAggregateTests
{
	[Fact]
	public void Spin_ReturnWin27MultipliedByBet ()
	{
		// Arrange
		const long Bet = 10;

		var player = new Player () { Amount = WinMatrixRules.PlayerAmount };
		var slotMachine = new SlotMachine ();

		var slotMachineMatrixGeneratorAdapter =
			new SlotMachineMatrixGeneratorAdapter (
				new MatrixGenerator ( WinMatrixRules.WinMatrix.GetLength ( 1 ) , WinMatrixRules.WinMatrix.GetLength ( 0 ) ) );

		var slotMachinePathConsecutiveTraversalAdapter = new SlotMachinePathConsecutiveTraversalAdapter ();

		var slotMachineMatrixGeneratorAdapterStab =
			new SlotMachineMatrixGeneratorAdapterStab (
				slotMachine ,
				slotMachineMatrixGeneratorAdapter );

		var slotMachineAggregate =
			new SlotMachineAggregate (
				player ,
				slotMachine ,
				slotMachineMatrixGeneratorAdapterStab ,
				slotMachinePathConsecutiveTraversalAdapter );

		// Act
		slotMachineMatrixGeneratorAdapterStab.GenerateWinPaths ();
		var response = slotMachineAggregate.Spin ( Bet );

		// Assert
		Assert.Equal ( WinMatrixRules.WinTotalWithoutMultiplier * Bet , response.WinAmount );
	}
}