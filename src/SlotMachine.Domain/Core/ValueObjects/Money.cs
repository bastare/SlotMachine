namespace SlotMachine.Domain.Core.ValueObjects;

public sealed record Money ( decimal Amount )
{
	public override string ToString ()
		=> Amount.ToString ( "0.00" );

	public static Money operator + ( Money a , Money b )
		=> new ( Amount: a.Amount + b.Amount );

	public static Money operator - ( Money a , Money b )
		=> new ( Amount: a.Amount - b.Amount );

	public static implicit operator decimal ( Money money )
		=> money.Amount;
}