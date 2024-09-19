namespace SlotMachine.Domain.Aggregate.Abstractions;

public interface ISlotMachineSessionAggregate
{
	public sealed record SpinResponse ( long Amount , long WinAmount , int[,] WinMatrix );

	SpinResponse Spin ( long bet );
}