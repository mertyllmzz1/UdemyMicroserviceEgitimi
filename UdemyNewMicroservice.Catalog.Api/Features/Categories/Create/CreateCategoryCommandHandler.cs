
using UdemyNewMicroservice.Catalog.Api.Repositories;

namespace UdemyNewMicroservice.Catalog.Api.Features.Categories.Create
{
	public class CreateCategoryCommandHandler(AppDbContext context) : IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryResponse>>
	{
		public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
		{
			var existCategory = await context.Categories.AnyAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);

			if (existCategory)
			{
				return ServiceResult<CreateCategoryResponse>.Error("Category already exists", $"Category name '{request.Name}'", System.Net.HttpStatusCode.BadRequest);
			}

			var category = new Category
			{
				Name = request.Name,
				// id ataması MassTransit üzerinden NextSequentialGuid ile yapılıyor çünkü indexleme esnasında daha performanslı çalışıyor
				Id = NewId.NextSequentialGuid()
			};
			await context.AddAsync(category, cancellationToken);
			await context.SaveChangesAsync(cancellationToken);

			return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(category.Id),"<empty>");


		}
	}
}
