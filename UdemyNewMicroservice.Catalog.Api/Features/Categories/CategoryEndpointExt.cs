using UdemyNewMicroservice.Catalog.Api.Features.Categories.Create;
using UdemyNewMicroservice.Catalog.Api.Features.Categories.GetAll;
using UdemyNewMicroservice.Shared.Filter;

namespace UdemyNewMicroservice.Catalog.Api.Features.Categories
{
	public static class CategoryEndpointExt
	{
		public static void AddCategoryGroupEndpointExt(this WebApplication app)
		{
			app.MapGroup("api/categories")
				.CreateCategoryItemEndpoint()
				.GetAllCategoryItemEndpoint();
		}
	}
}
