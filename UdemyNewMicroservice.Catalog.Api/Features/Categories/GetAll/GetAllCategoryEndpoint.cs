using AutoMapper;
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
	public class GetAllCategoryHandler(AppDbContext context, IMapper _mapper) : IRequestHandler<GetAllCategoryQuery, ServiceResult<List<CategoryDto>>>
	{
		public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
		{
			var categories= await context.Categories
				.ToListAsync(cancellationToken); //#NOTES : CancellationToken asenkron operasyonlarda işlemi iptal etmek için kullanılır. Bu, özellikle uzun süren işlemler sırasında uygulamanın yanıt vermesini sağlar ve gereksiz kaynak tüketimini önler.
			var categoriesAsDto = _mapper.Map<List<CategoryDto>>(categories);
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
