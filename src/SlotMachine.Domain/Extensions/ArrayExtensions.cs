namespace SlotMachine.Domain.Extensions;

public static class ArrayExtensions
{
	public static int[][] ToJaggedArray ( this int[,] multiArray )
	{
		int rows = multiArray.GetLength ( 0 );
		int cols = multiArray.GetLength ( 1 );
		int[][] jaggedArray = new int[ rows ][];

		for ( int i = 0; i < rows; i++ )
		{
			jaggedArray[ i ] = new int[ cols ];
			for ( int j = 0; j < cols; j++ )
			{
				jaggedArray[ i ][ j ] = multiArray[ i , j ];
			}
		}
		return jaggedArray;
	}
}