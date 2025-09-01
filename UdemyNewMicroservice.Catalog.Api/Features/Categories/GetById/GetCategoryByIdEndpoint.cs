
using UdemyNewMicroservice.Catalog.Api.Features.Categories.Dtos;
using UdemyNewMicroservice.Catalog.Api.Repositories;

namespace UdemyNewMicroservice.Catalog.Api.Features.Categories.GetById
{
	public record GetCategoryByIdQuery(Guid Id): IRequestByServiceResult<CategoryDto>;


	public class GetCategoryByIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, ServiceResult<CategoryDto>>
	{
		public async Task<ServiceResult<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
		{
			var category = await context.Categories.FindAsync(request.Id , cancellationToken);
			if (category is null)
			{
				return ServiceResult<CategoryDto>.Error("Category not found",$"The category within Id {request.Id} is not found",System.Net.HttpStatusCode.NotFound);
			}
			var dto = mapper.Map<CategoryDto>(category);
			return ServiceResult<CategoryDto>.SuccessAsOk(dto);
		}
	}
	//public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
	//{
	//	public GetCategoryByIdQueryValidator()
	//	{
	//		RuleFor(x => x.Id).NotEmpty().WithMessage("{PropertyName} must be filled")
	//			.Must(id => id != Guid.Empty).WithMessage("{PropertyName} must be a valid GUID");
	//	}
	//}
	public static class GetCategoryByIdEndpoint
	{
		public static IEndpointRouteBuilder GetCategoryByIdItemEndpoint(this RouteGroupBuilder group)
		{
			group.MapGet("/{id:guid}", async (IMediator mediator, Guid id) =>
			 (await mediator.Send(new GetCategoryByIdQuery(id))).ToGenericResult()

			);
			return group;
		}
	}
}
