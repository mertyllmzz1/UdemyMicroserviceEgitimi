using UdemyNewMicroservice.Catalog.Api.Features.Categories;
using UdemyNewMicroservice.Catalog.Api.Repositories;

namespace UdemyNewMicroservice.Catalog.Api.Features.Courses
{
	public class Course:BaseEntity
	{
		/* 
		 * default! -> null gelmeyecek demek.
	  */
		public string Name { get; set; }= default!;
		public string Description { get; set; }
		public decimal Price { get; set; }
		public Guid UserId{ get; set; }
		public string Picture { get; set; }
		public DateTime Created{ get; set; }
		public Guid CategoryId { get; set; }
		public Category Category { get; set; } = default!; 
		public Feature Feature { get; set; } = default!;
	}
}
