using SlotMachine.Api.Endpoints.v1;
using SlotMachine.Infrastructure.CrossCutting.loC;

var builder = WebApplication.CreateBuilder ( args );

builder.Configuration.AddEnvironmentVariables ();

builder.Services.InjectLayersDependency ( builder.Configuration );

var app = builder.Build ();

if ( app.Environment.IsDevelopment () )
{
	app.UseSwagger ();
	app.UseSwaggerUI ();
}

app.UseHttpsRedirection ();

MapEndpoints ( app );

await app.RunAsync ();

static void MapEndpoints ( WebApplication webApplication )
{
	var apiVersionSet_ =
		webApplication
			.NewApiVersionSet ()
			.HasApiVersion ( apiVersion: new ( 1.0 ) )
			.ReportApiVersions ()
			.Build ();

	var routeGroupBuilder_ =
		webApplication
			.MapGroup ( "api/v{apiVersion:apiVersion}" )
			.WithApiVersionSet ( apiVersionSet_ )
			.WithOpenApi ();

	SlotMachineEndpoints.MapEndpoints ( routeGroupBuilder_ );
	PlayerEndpoints.MapEndpoints ( routeGroupBuilder_ );
}

public partial class Program { }