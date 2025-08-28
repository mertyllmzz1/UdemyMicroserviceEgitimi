using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using System.Reflection.Emit;
using UdemyNewMicroservice.Catalog.Api.Features.Courses;

namespace UdemyNewMicroservice.Catalog.Api.Repositories
{
	public class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
	{
		public void Configure(EntityTypeBuilder<Course> builder)
		{
			/*
		 * #NOTES  Collection => MongoDB'deki tablo karşılığıdır.
		 * document => MongoDB'deki kayıt karşılığıdır.
		 * field => MongoDB'deki kolon karşılığıdır.
		 */
			builder.ToCollection("courses");
			builder.HasKey(c => c.Id);
			/*
				* #NOTES Id otomatik generate edilmesin. Çünkü Guidleri biz kendimiz oluşturacağız.
			*/
			builder.Property(c => c.Id).ValueGeneratedNever();
			builder.Property(c => c.Name).HasElementName("name").HasMaxLength(100);
			builder.Property(c => c.Description).HasElementName("description").HasMaxLength(1000);
			builder.Property(c => c.Created).HasElementName("created");
			builder.Property(c => c.Price).HasElementName("price");
			builder.Property(c => c.UserId).HasElementName("userId");
			builder.Property(c => c.CategoryId).HasElementName("categoryId");
			builder.Property(c => c.Picture).HasElementName("picture");
			// Category navigation property si ignore ediliyor. Çünkü Course entity si Category entity sine ihtiyaç duymuyor.
			builder.Ignore(c => c.Category);

			builder.OwnsOne(c => c.Feature, feature =>
			{
				feature.HasElementName("feature");
				feature.Property(f => f.Duration).HasElementName("duration");
				feature.Property(f => f.Rating).HasElementName("rating");
				feature.Property(f => f.EducatorFullName).HasElementName("educatorFullName").HasMaxLength(100);
			});

		}
	}
}
