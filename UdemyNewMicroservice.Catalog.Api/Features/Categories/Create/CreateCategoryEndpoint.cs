using UdemyNewMicroservice.Shared.Filter;

namespace UdemyNewMicroservice.Catalog.Api.Features.Categories.Create
{
	public static class CreateCategoryEndpoint
	{
		public static RouteGroupBuilder CreateCategoryItemEndpoint(this RouteGroupBuilder group)
		{
			group.MapPost("/", async (CreateCategoryCommand command, IMediator mediator) =>(await mediator.Send(command)).ToGenericResult()).AddEndpointFilter<ValidationFilter<CreateCategoryCommand>>();

			return group;
		}
	}
}
