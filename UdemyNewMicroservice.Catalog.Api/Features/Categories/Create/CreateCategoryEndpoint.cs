using MediatR;
using Microsoft.AspNetCore.Mvc;
using UdemyNewMicroservice.Shared.Extensions;

namespace UdemyNewMicroservice.Catalog.Api.Features.Categories.Create
{
	public static class CreateCategoryEndpoint
	{
		public static RouteGroupBuilder CreateCategoryItemEndpoint(this RouteGroupBuilder group)
		{
			group.MapPost("/", async (CreateCategoryCommand command, IMediator mediator) =>(await mediator.Send(command)).ToGenericResult());

			return group;
		}
	}
}
