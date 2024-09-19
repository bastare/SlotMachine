namespace SlotMachine.Domain.Matrix.Common.Exceptions;

public sealed class MatrixWrongSizeException : Exception
{
	public MatrixWrongSizeException () { }

	public MatrixWrongSizeException ( string? message ) : base ( message ) { }

	public MatrixWrongSizeException ( string? message , Exception? innerException ) : base ( message , innerException ) { }
}