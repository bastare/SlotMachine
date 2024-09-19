namespace SlotMachine.Domain.SearchAlgorithms;

using System.Numerics;

public static class PathConsecutiveTraversal
{
	public static class Rules
	{
		public const int ConsecutiveCountLimitAllowed = 1;
	}

	public sealed record ConsecutiveNumber ( int ConsecutiveValue , int ConsecutiveCount );

	public static TResult Execute<TResult> ( int[,] matrix , List<int[][]> paths , int consecutiveCountLimit , Func<ConsecutiveNumber , TResult> consecutiveNumberTransformer )
		where TResult : struct, INumber<TResult>
	{
		if ( consecutiveCountLimit < Rules.ConsecutiveCountLimitAllowed )
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
				var (currentX_, currentY_) = (path[ index_ ][ 0 ], path[ index_ ][ 1 ]);
				var (previousX_, previousY_) = (path[ index_ - 1 ][ 0 ], path[ index_ - 1 ][ 1 ]);

				var currentValue_ = matrix[ currentX_ , currentY_ ];
				var previousValue_ = matrix[ previousX_ , previousY_ ];

				if ( currentValue_ == previousValue_ )
				{
					consecutiveCount_++;

					continue;
				}

				if ( consecutiveCountLimit <= consecutiveCount_ )
					pathResult_ +=
						consecutiveNumberTransformer (
							new ( ConsecutiveValue: previousValue_ , ConsecutiveCount: consecutiveCount_ ) );

				return pathResult_;
			}

			return pathResult_;
		}
	}
}