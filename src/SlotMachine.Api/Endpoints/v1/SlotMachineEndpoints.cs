namespace SlotMachine.Api.Endpoints.v1;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Abstractions;
using Contracts;

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
		[FromBody] SpinSlotMachineRequestBody spinSlotMachineRequestBody ,
		[FromServices] IMediator mediator ,
		CancellationToken cancellationToken )
	{

		return Results.Ok ();
	}
}