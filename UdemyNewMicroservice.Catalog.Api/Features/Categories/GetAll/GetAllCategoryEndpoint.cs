using MediatR;
using Microsoft.EntityFrameworkCore;
using UdemyNewMicroservice.Catalog.Api.Features.Categories.Create;
using UdemyNewMicroservice.Catalog.Api.Features.Categories.Dtos;
using UdemyNewMicroservice.Catalog.Api.Repositories;
using UdemyNewMicroservice.Shared;
using UdemyNewMicroservice.Shared.Extensions;
using UdemyNewMicroservice.Shared.Filter;

namespace UdemyNewMicroservice.Catalog.Api.Features.Categories.GetAll
{
	public class GetAllCategoryQuery : IRequest<ServiceResult<List<CategoryDto>>>;
	public class GetAllCategoryHandler(AppDbContext context ) : IRequestHandler<GetAllCategoryQuery, ServiceResult<List<CategoryDto>>>
	{
		public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
		{
			var categories= await context.Categories
				.ToListAsync(cancellationToken);
			var categoriesAsDto = categories
				.Select(c => new CategoryDto(c.Id, c.Name))
				.ToList();

			return ServiceResult<List<CategoryDto>>.SuccessAsOk(categoriesAsDto);
		}
	}
	public static class GetAllCategoryEndpoint
	{
		public static RouteGroupBuilder GetAllCategoryItemEndpoint(this RouteGroupBuilder group)
		{
			group.MapGet("/", async (IMediator mediator) => (await mediator.Send(new GetAllCategoryQuery())).ToGenericResult());

			return group;
		}
	}
}
