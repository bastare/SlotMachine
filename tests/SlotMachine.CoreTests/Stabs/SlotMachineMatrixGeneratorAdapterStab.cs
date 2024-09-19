namespace SlotMachine.CoreTests.Stabs;

using Rules;
using Domain.Aggregate.Abstractions;
using System.Collections.Generic;
using Domain.Core.Entities;

public sealed class SlotMachineMatrixGeneratorAdapterStab ( SlotMachine slotMachine , ISlotMachineMatrixGeneratorAdapter slotMachineMatrixGeneratorAdapter ) :
	ISlotMachineMatrixGeneratorAdapter
{
	public int Height => slotMachineMatrixGeneratorAdapter.Height;

	public int Width => slotMachineMatrixGeneratorAdapter.Width;

	public int[,] GenerateResultMatrix ()
		=> WinMatrixRules.WinMatrix;

	public List<int[][]> GenerateWinPaths ()
	{
		slotMachine.Height = slotMachineMatrixGeneratorAdapter.Height;
		slotMachine.Width = slotMachineMatrixGeneratorAdapter.Width;
		slotMachine.WinLines = slotMachineMatrixGeneratorAdapter.GenerateWinPaths ();
		return new ( slotMachine.WinLines );
	}
}