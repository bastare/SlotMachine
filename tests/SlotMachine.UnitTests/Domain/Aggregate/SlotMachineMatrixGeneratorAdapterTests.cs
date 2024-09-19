namespace SlotMachine.UnitTests.Domain.Aggregate;

using SlotMachine.Domain.Rules;
using SlotMachine.Domain.Matrix;
using SlotMachine.Domain.Aggregate.Adapters;

public sealed class SlotMachineMatrixGeneratorAdapterTests
{
	[Fact]
	public void GenerateResultMatrix_CreatesMatrixWithRandomValues ()
	{
		// Arrange
		var generator = new SlotMachineMatrixGeneratorAdapter ( new MatrixGenerator () );
		var matrix = generator.GenerateResultMatrix ();

		// Assert
		Assert.NotNull ( matrix );
		Assert.Equal ( generator.Height , matrix.GetLength ( 0 ) );
		Assert.Equal ( generator.Width , matrix.GetLength ( 1 ) );
		Assert.True ( HasValidNumbers ( matrix ) );

		static bool HasValidNumbers ( int[,] matrix )
			=> matrix.Cast<int> ()
				.All ( matrixNumber =>
					matrixNumber >= SlotMachineRules.RandomNumberRanges.Min
						&& matrixNumber <= SlotMachineRules.RandomNumberRanges.Max );
	}

	[Fact]
	public void GenerateWinPaths_CreatesListOfWorkingPaths ()
	{
		// Arrange
		var generator = new SlotMachineMatrixGeneratorAdapter ( new MatrixGenerator () );
		var winPaths = generator.GenerateWinPaths ();

		// Assert
		Assert.NotNull ( winPaths );
		Assert.True ( winPaths.Any () );
	}
}