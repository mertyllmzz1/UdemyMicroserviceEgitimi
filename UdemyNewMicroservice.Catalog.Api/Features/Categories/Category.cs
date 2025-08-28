using UdemyNewMicroservice.Catalog.Api.Features.Courses;
using UdemyNewMicroservice.Catalog.Api.Repositories;

namespace UdemyNewMicroservice.Catalog.Api.Features.Categories
{
	public class Category:BaseEntity
	{
		public string Name { get; set; } = default!;
		public List<Course> Courses { get; set; }
	}
}
