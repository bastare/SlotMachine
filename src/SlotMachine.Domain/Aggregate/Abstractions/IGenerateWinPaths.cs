namespace SlotMachine.Domain.Aggregate.Abstractions;

public interface IGenerateWinPaths
{
	List<int[][]> GenerateWinPaths ();
}