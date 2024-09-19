namespace SlotMachine.Domain.SearchAlgorithms;

using System.Numerics;

public static class PathConsecutiveTraversal
{
	public sealed record ConsecutiveNumber ( int ConsecutiveValue , int ConsecutiveCount );

	public static TResult Execute<TResult> ( int[,] matrix , List<int[][]> paths , int consecutiveCountLimit , Func<ConsecutiveNumber , TResult> consecutiveNumberTransformer )
		where TResult : struct, INumber<TResult>
	{
		if ( consecutiveCountLimit < 1 )
			throw new ArgumentOutOfRangeException ( nameof ( consecutiveCountLimit ) , "ConsecutiveCountLimit should be greater than 1" );

		TResult totalResult_ = default;

		foreach ( var path_ in paths )
			totalResult_ += CalculateResultForPath ( matrix , path_ );

		return totalResult_;

		TResult CalculateResultForPath ( int[,] matrix , int[][] path )
		{
			TResult pathResult_ = default;
			var consecutiveCount_ = 1;

			for ( var index_ = 1; index_ < path.Length; index_++ )
			{
				var previous_ = path[ index_ - 1 ];
				var current_ = path[ index_ ];

				var currentValue_ = matrix[ current_[ 0 ] , current_[ 1 ] ];
				var previousValue_ = matrix[ previous_[ 0 ] , previous_[ 1 ] ];

				if ( currentValue_ == previousValue_ )
				{
					consecutiveCount_++;

					continue;
				}

				if ( consecutiveCount_ >= consecutiveCountLimit )
					pathResult_ +=
						consecutiveNumberTransformer (
							new ( ConsecutiveValue: previousValue_ , ConsecutiveCount: consecutiveCount_ ) );

				return pathResult_;
			}

			return pathResult_;
		}
	}
}