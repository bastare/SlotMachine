namespace SlotMachine.Domain.Aggregate.Abstractions;


public interface ISlotMachineGenerateWinPathsAggregate
{
	List<int[][]> GenerateWinLines ();
}