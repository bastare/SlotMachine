namespace SlotMachine.Api.Endpoints.v1;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Abstractions;
using static Application.UseCases.Player.Commands.AddPlayerAmount;
using static Application.UseCases.Player.Commands.CreatePlayer;
using static Application.UseCases.Player.Commands.DeletePlayer;

public sealed class PlayerEndpoints : IHasEndpoints
{
	public sealed record CreatePlayerRequestBody ( long Amount );

	public sealed record AddPlayerBalanceRequestBody ( string PlayerId , long Amount );

	public static void MapEndpoints ( RouteGroupBuilder routeGroupBuilder )
	{
		var v1Player =
			routeGroupBuilder
				.MapGroup ( "players" )
				.MapToApiVersion ( 1.0 );

		v1Player.MapPost (
			pattern: string.Empty ,
			handler: Create );

		v1Player.MapDelete (
			pattern: "{playerIdForRemove}" ,
			handler: Delete );

		v1Player.MapPost (
			pattern: "add-player-balance" ,
			handler: AddPlayerBalance );
	}

	public static async Task<IResult> Create (
		[FromBody] CreatePlayerRequestBody createPlayerRequestBody ,
		[FromServices] IMediator mediator ,
		CancellationToken cancellationToken )
	{
		var response_ = await mediator.Send (
			new CreatePlayerCommand ( createPlayerRequestBody.Amount ) ,
			cancellationToken );

		return Results.Ok ( response_ );
	}

	public static async Task<IResult> Delete (
		[FromRoute] string playerIdForRemove ,
		[FromServices] IMediator mediator ,
		CancellationToken cancellationToken )
	{
		var response_ = await mediator.Send (
			new DeletePlayerCommand ( playerIdForRemove ) ,
			cancellationToken );

		return Results.Ok ( response_ );
	}

	public static async Task<IResult> AddPlayerBalance (
		[FromBody] AddPlayerBalanceRequestBody addPlayerBalanceRequestBody ,
		[FromServices] IMediator mediator ,
		CancellationToken cancellationToken )
	{
		try
		{
			var response_ = await mediator.Send (
				new AddPlayerAmountCommand (
					addPlayerBalanceRequestBody.PlayerId ,
					addPlayerBalanceRequestBody.Amount ) ,
				cancellationToken );

			return Results.Ok ( response_ );
		}
		catch ( Exception exception )
		{
			return Results.Problem ( exception.Message );
		}
	}
}