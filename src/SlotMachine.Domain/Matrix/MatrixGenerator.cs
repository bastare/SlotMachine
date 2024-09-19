namespace SlotMachine.Domain.Matrix;

using Common.Exceptions;
using Rules;
using Abstractions;

public sealed class MatrixGenerator : IMatrixGenerator
{
	public int Width { get; }

	public int Height { get; }

	public MatrixGenerator ()
	{
		Width = SlotMachineRules.MatrixSizeRules.MinWidth;
		Height = SlotMachineRules.MatrixSizeRules.MinHeight;
	}

	public MatrixGenerator ( int width , int height )
	{
		if ( width < SlotMachineRules.MatrixSizeRules.MinWidth || height < SlotMachineRules.MatrixSizeRules.MinHeight )
			throw new MatrixWrongSizeException ( message: $"Width and height should be greater than {SlotMachineRules.MatrixSizeRules.MinWidth} and {SlotMachineRules.MatrixSizeRules.MinHeight} respectively" );

		Width = width;
		Height = height;
	}

	public List<int[][]> GenerateStraightRows ()
	{
		var rowWinLines_ = new List<int[][]> ();

		for ( int rowIndex_ = 0; rowIndex_ < Height; rowIndex_++ )
		{
			var winLine_ = new int[ Width ][];

			for ( int colIndex_ = 0; colIndex_ < Width; colIndex_++ )
				winLine_[ colIndex_ ] = [ rowIndex_ , colIndex_ ];

			rowWinLines_.Add ( winLine_ );
		}

		return rowWinLines_;
	}

	public int[][] GenerateVPattern ()
	{
		var vPattern_ = new List<int[]> ();
		int mid_ = Width / 2;

		GoingDown ();
		GoingUp ();

		return [ .. vPattern_ ];

		void GoingDown ()
		{
			for ( var index_ = 0; index_ < Height && index_ <= mid_; index_++ )
				vPattern_.Add ( [ index_ , index_ ] );
		}

		void GoingUp ()
		{
			for ( int indexX_ = mid_ - 1; indexX_ >= 0; indexX_-- )
			{
				var indexY_ = Width - indexX_ - 1;

				vPattern_.Add ( [ indexX_ , indexY_ ] );
			}
		}
	}

	public int[][] GenerateWPattern ()
	{
		var wPattern_ = new List<int[]> ();

		var rowIndex_ = Height / 2;
		wPattern_.Add ( [ rowIndex_ , 0 ] );

		bool isGoingDown_ = true;

		for ( var colIndex_ = 1; colIndex_ < Width; colIndex_++ )
		{
			if ( isGoingDown_ )
			{
				rowIndex_ = Math.Min ( rowIndex_ + 1 , Height - 1 );
			}
			else
			{
				rowIndex_ = Math.Max ( rowIndex_ - 1 , 0 );
			}

			wPattern_.Add ( [ rowIndex_ , colIndex_ ] );

			isGoingDown_ = !isGoingDown_;
		}

		return [ .. wPattern_ ];
	}
}
