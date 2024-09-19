namespace SlotMachine.Domain.Aggregate.Abstractions;

public interface ICalculateWinAmount
{
	long CalculateWinAmount ( int[,] winMatrix , List<int[][]> winLines , long bet );
}