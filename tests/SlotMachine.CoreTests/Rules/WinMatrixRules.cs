namespace SlotMachine.CoreTests.Rules;

public static class WinMatrixRules
{
	public const int PlayerAmount = 20;

	public readonly static int[,] WinMatrix = new int[,] {
		{ 3, 3, 3, 4, 5 },
		{ 2, 3, 2, 3, 3 },
		{ 1, 2, 3, 3, 3 }
	};

	public const int WinTotalWithoutMultiplier = 27;
}