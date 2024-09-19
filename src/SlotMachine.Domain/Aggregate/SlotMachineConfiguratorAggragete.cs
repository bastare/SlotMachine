namespace SlotMachine.Domain.Aggregate;

using Core.Entities;
using Abstractions;

public sealed class SlotMachineConfiguratorAggregate ( SlotMachine slotMachine , ISlotMachineMatrixGeneratorAdapter slotMachineMatrixGenerator ) : ISlotMachineGenerateWinPathsAggregate
{
	public List<int[][]> GenerateWinLines ()
	{
		slotMachine.Height = slotMachineMatrixGenerator.Height;
		slotMachine.Width = slotMachineMatrixGenerator.Width;
		slotMachine.WinLines = slotMachineMatrixGenerator.GenerateWinPaths ();

		return slotMachine.WinLines;
	}
}