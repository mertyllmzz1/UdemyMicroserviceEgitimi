using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using UdemyNewMicroservice.Catalog.Api.Features.Categories;

namespace UdemyNewMicroservice.Catalog.Api.Repositories
{
	public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.ToCollection("categories");
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Id).ValueGeneratedNever();
			/*
				* #NOTES Feature Course içerisinde bir owned entity dir. Bunu belirtmeliyiz.
				* owned entity ler kendi başlarına var olamazlar. Sadece parent entity ile
				* Course ise 
			*/
		}
	}
}
