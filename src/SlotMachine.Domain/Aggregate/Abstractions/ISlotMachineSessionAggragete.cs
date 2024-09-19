namespace SlotMachine.Domain.Aggregate.Abstractions;

public interface ISlotMachineSessionAggregate
{
	public sealed record SpinResponse ( long Amount , long WinAmount , int[,] WinMatrix );

	public SpinResponse Spin ( long bet );
}