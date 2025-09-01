
using UdemyNewMicroservice.Catalog.Api.Features.Categories.Dtos;

namespace UdemyNewMicroservice.Catalog.Api.Features.Categories
{
	public class CategoryMapping: Profile
	{
		public CategoryMapping()
		{
			CreateMap<Category, CategoryDto>().ReverseMap(); //#NOTES Reversemap metodu iki sınıfında birbirine dönüşümününü sağlamamıza olanak verir. Kullanmasaydık yalnızca Category => CategoryDto yapılabilirdi 
		}
	}
}
