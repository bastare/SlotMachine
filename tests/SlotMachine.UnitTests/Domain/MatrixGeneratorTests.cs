namespace SlotMachine.UnitTests.Domain;

using SlotMachine.Domain.Rules;
using SlotMachine.Domain.Matrix;
using SlotMachine.Domain.Matrix.Common.Exceptions;

public sealed class MatrixGeneratorTests
{
	[Fact]
	public void Constructor_DefaultDimensions_CreatesMatrixGenerator ()
	{
		// Arrange
		var generator = new MatrixGenerator ();

		// Assert
		Assert.Equal ( SlotMachineRules.MatrixSizeRules.MinWidth , generator.Width );
		Assert.Equal ( SlotMachineRules.MatrixSizeRules.MinHeight , generator.Height );
	}

	[Fact]
	public void Constructor_CustomDimensions_CreatesMatrixGenerator ()
	{
		// Arrange
		var width = 5;
		var height = 3;
		var generator = new MatrixGenerator ( width , height );

		// Assert
		Assert.Equal ( width , generator.Width );
		Assert.Equal ( height , generator.Height );
	}

	[Fact]
	public void Constructor_CustomDimensions_TooSmall_ThrowsException ()
	{
		// Arrange
		var wrongWidth = SlotMachineRules.MatrixSizeRules.MinWidth - 1;
		var wrongHeight = SlotMachineRules.MatrixSizeRules.MinHeight - 1;

		// Act and Assert
		Assert.Throws<MatrixWrongSizeException> ( () => new MatrixGenerator ( wrongWidth , wrongHeight ) );
	}

	[Fact]
	public void GenerateStraightRows_CreatesListOfWorkingRows ()
	{
		// Arrange
		var generator = new MatrixGenerator ();
		var straightRows = generator.GenerateStraightRows ();

		// Assert
		Assert.NotNull ( straightRows );
		Assert.True ( straightRows.Any () );
	}
}