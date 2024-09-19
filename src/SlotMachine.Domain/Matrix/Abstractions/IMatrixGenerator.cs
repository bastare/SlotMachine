namespace SlotMachine.Domain.Matrix.Abstractions;

public interface IMatrixGenerator
{
	int Width { get; }

	int Height { get; }

	List<int[][]> GenerateStraightRows ();

	int[][] GenerateVPattern ();

	int[][] GenerateWPattern ();
}