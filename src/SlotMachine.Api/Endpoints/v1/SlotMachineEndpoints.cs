namespace SlotMachine.Api.Endpoints.v1;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Abstractions;
using static Application.UseCases.SlotMachine.Commands.SpinSlotMachine;
using static Application.UseCases.SlotMachine.Commands.CreateSlotMachine;
using static Application.UseCases.SlotMachine.Commands.DeleteSlotMachine;
using static Application.UseCases.SlotMachine.Commands.ReconfigureSlotMachineMatrix;

public sealed class SlotMachineEndpoints : IHasEndpoints
{
	public sealed record CreateSlotMachineRequestBody ( int MatrixHeight , int MatrixWidth );

	public sealed record ReconfigureSlotMachineMatrixRequestBody ( string SlotMachineId , int Height , int Width );

	public sealed record SpinSlotMachineRequestBody ( string PlayerId , string SlotMachineId , long Bet );

	public static void MapEndpoints ( RouteGroupBuilder routeGroupBuilder )
	{
		var v1slotMachine =
			routeGroupBuilder
				.MapGroup ( "slot-machines" )
				.MapToApiVersion ( 1.0 );

		v1slotMachine.MapPost (
			pattern: "spin" ,
			handler: Spin );

		v1slotMachine.MapPost (
			pattern: string.Empty ,
			handler: Create );

		v1slotMachine.MapDelete (
			pattern: "{slotMachineIdForRemove}" ,
			handler: Delete );

		v1slotMachine.MapPost (
			pattern: "reconfigure-matrix-size" ,
			handler: ReconfigureMatrixSize );

	}

	public static async Task<IResult> ReconfigureMatrixSize (
		[FromBody] ReconfigureSlotMachineMatrixRequestBody reconfigureSlotMachineMatrixRequestBody ,
		[FromServices] IMediator mediator ,
		CancellationToken cancellationToken )
	{
		var response_ = await mediator.Send (
			new ReconfigureSlotMachineMatrixCommand (
				reconfigureSlotMachineMatrixRequestBody.SlotMachineId ,
				reconfigureSlotMachineMatrixRequestBody.Height ,
				reconfigureSlotMachineMatrixRequestBody.Width ) ,
			cancellationToken );

		return Results.Ok ( response_ );
	}

	public static async Task<IResult> Create (
		[FromBody] CreateSlotMachineRequestBody createSlotMachineRequestBody ,
		[FromServices] IMediator mediator ,
		CancellationToken cancellationToken )
	{
		var response_ = await mediator.Send (
			new CreateSlotMachineCommand (
				createSlotMachineRequestBody.MatrixHeight ,
				createSlotMachineRequestBody.MatrixWidth ) ,
			cancellationToken );

		return Results.Ok ( response_ );
	}

	public static async Task<IResult> Delete (
		[FromRoute] string slotMachineIdForRemove ,
		[FromServices] IMediator mediator ,
		CancellationToken cancellationToken )
	{
		var response_ = await mediator.Send (
			new DeleteSlotMachineCommand ( slotMachineIdForRemove ) ,
			cancellationToken );

		return Results.Ok ( response_ );
	}

	public static async Task<IResult> Spin (
		[FromBody] SpinSlotMachineRequestBody spinSlotMachineRequestBody ,
		[FromServices] IMediator mediator ,
		CancellationToken cancellationToken )
	{
		try
		{
			var response_ = await mediator.Send (
				new SpinSlotMachineCommand (
					spinSlotMachineRequestBody.PlayerId ,
					spinSlotMachineRequestBody.SlotMachineId ,
					spinSlotMachineRequestBody.Bet ) ,
				cancellationToken );

			return Results.Ok ( response_ );
		}
		catch ( Exception exception )
		{
			return Results.Problem ( exception.Message );
		}
	}
}