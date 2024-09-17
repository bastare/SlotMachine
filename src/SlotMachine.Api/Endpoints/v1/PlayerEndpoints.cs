namespace SlotMachine.Api.Endpoints.v1;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Abstractions;

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
		[FromServices] IMediator mediator ,
		CancellationToken cancellationToken )
	{

		return Results.Ok ();
	}
}