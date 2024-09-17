namespace SlotMachine.Api.Endpoints.Abstractions;

public interface IHasEndpoints
{
	abstract static void MapEndpoints ( RouteGroupBuilder routeGroupBuilder );
}