namespace SlotMachine.Api.Endpoints.v1;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Abstractions;

public sealed class SlotMachineEndpoints : IHasEndpoints
{
	public static void MapEndpoints ( RouteGroupBuilder routeGroupBuilder )
	{
		routeGroupBuilder
			.MapGroup ( "slot-machine" )
			.MapToApiVersion ( 1.0 )
			.MapPost (
				pattern: "spin" ,
				handler: Spin );
	}

	public static async Task<IResult> Spin (
		[FromServices] IMediator mediator ,
		CancellationToken cancellationToken )
	{

		return Results.Ok ();
	}
}