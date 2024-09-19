namespace SlotMachine.Domain.Aggregate.Abstractions;

public interface ISlotMachineMatrixGeneratorAdapter : IGenerateWinMatrix, IGenerateWinPaths
{
	int Height { get; }

	int Width { get; }
}