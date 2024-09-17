namespace SlotMachine.Api.Endpoints.v1;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Abstractions;
using Contracts;

public sealed class PlayerEndpoints : IHasEndpoints
{
	public static void MapEndpoints ( RouteGroupBuilder routeGroupBuilder )
	{
		routeGroupBuilder
			.MapGroup ( "player" )
			.MapToApiVersion ( 1.0 )
			.MapPost (
				pattern: "update-balance" ,
				handler: UpdateBalance );
	}

	public static async Task<IResult> UpdateBalance (
 		[FromBody] UpdatePlayerBalanceRequestBody updatePlayerBalanceRequestBody ,
		[FromServices] IMediator mediator ,
		CancellationToken cancellationToken )
	{

		return Results.Ok (new());
	}
}