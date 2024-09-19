namespace SlotMachine.Domain.Aggregate.Adapters;

using Aggregate.Abstractions;
using Matrix.Abstractions;
using Rules;
using System.Collections.Generic;

public sealed class SlotMachineMatrixGeneratorAdapter ( IMatrixGenerator matrixGenerator ) : ISlotMachineMatrixGeneratorAdapter
{
	public int Height => matrixGenerator.Height;

	public int Width => matrixGenerator.Width;

	public int[,] GenerateResultMatrix ()
	{
		var matrix_ = new int[ matrixGenerator.Height , matrixGenerator.Width ];
		var random_ = new Random ();

		for ( var row_ = 0; row_ < matrixGenerator.Height; row_++ )
			for ( var col_ = 0; col_ < matrixGenerator.Width; col_++ )
				matrix_[ row_ , col_ ] = random_.Next ( SlotMachineRules.RandomNumberRanges.Min , SlotMachineRules.RandomNumberRanges.Max );

		return matrix_;
	}

	public List<int[][]> GenerateWinPaths ()
	{
		var winPaths_ = new List<int[][]> ();

		winPaths_.AddRange ( matrixGenerator.GenerateStraightRows () );

		if ( SlotMachineRules.MatrixSizeRules.MinHightForCrossPatterns <= matrixGenerator.Height )
		{
			winPaths_.AddRange ( [
				matrixGenerator.GenerateVPattern () ,
				matrixGenerator.GenerateWPattern ()
			] );
		}

		return winPaths_;
	}
}