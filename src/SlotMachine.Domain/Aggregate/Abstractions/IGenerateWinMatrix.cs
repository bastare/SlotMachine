namespace SlotMachine.Domain.Aggregate.Abstractions;

public interface IGenerateWinMatrix
{
	int[,] GenerateResultMatrix ();
}