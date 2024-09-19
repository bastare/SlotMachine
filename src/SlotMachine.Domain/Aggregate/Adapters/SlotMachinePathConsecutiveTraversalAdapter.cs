namespace SlotMachine.Domain.Aggregate.Adapters;

using Abstractions;
using Rules;
using SearchAlgorithms;
using System.Collections.Generic;

public sealed class SlotMachinePathConsecutiveTraversalAdapter : ICalculateWinAmount
{
	public long CalculateWinAmount ( int[,] winMatrix , List<int[][]> winLines , long bet )
		=> PathConsecutiveTraversal.Execute (
			matrix: winMatrix ,
			paths: winLines ,
			consecutiveCountLimit: SlotMachineRules.WinConditionRules.ConsecutiveSeries ,
			consecutiveNumberTransformer: ( consecutiveNumber ) =>
				consecutiveNumber.ConsecutiveValue * consecutiveNumber.ConsecutiveCount * bet );
}