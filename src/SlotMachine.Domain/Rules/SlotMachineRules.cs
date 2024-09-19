namespace SlotMachine.Domain.Rules;

public static class SlotMachineRules
{
	public static class MatrixSizeRules
	{
		public const int MinHeight = 3;

		public const int MinWidth = 3;

		public const int MinHightForCrossPatterns = 3;
	}

	public static class RandomNumberRanges
	{
		public const int Min = 0;

		public const int Max = 9;
	}

	public static class BetRules
	{
		public const int MinBet = 1;
	}

	public static class WinConditionRules
	{
		public const int ConsecutiveSeries = 3;
	}
}