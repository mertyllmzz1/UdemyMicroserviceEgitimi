using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using UdemyNewMicroservice.Catalog.Api.Features.Categories;
using UdemyNewMicroservice.Catalog.Api.Features.Courses;

namespace UdemyNewMicroservice.Catalog.Api.Repositories
{
	public class AppDbContext(DbContextOptions<AppDbContext> options):DbContext(options)
	{
		public DbSet<Course> Courses { get; set; }
		public DbSet<Category> Categories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			/*
			 * #NOTES  Collection => MongoDB'deki tablo karşılığıdır.
			 * document => MongoDB'deki kayıt karşılığıdır.
			 * field => MongoDB'deki kolon karşılığıdır.
			 */
			modelBuilder.Entity<Course>().ToCollection("courses");
			modelBuilder.Entity<Course>().HasKey(c => c.Id);
			/*
				* #NOTES Id otomatik generate edilmesin. Çünkü Guidleri biz kendimiz oluşturacağız.
			*/
			modelBuilder.Entity<Course>().Property(c => c.Id).ValueGeneratedNever();
			modelBuilder.Entity<Course>().Property(c => c.Name).HasElementName("name").HasMaxLength(100);
			modelBuilder.Entity<Course>().Property(c => c.Description).HasElementName("description").HasMaxLength(1000);
			modelBuilder.Entity<Course>().Property(c => c.Created).HasElementName("created");
			modelBuilder.Entity<Course>().Property(c => c.Price).HasElementName("price");
			modelBuilder.Entity<Course>().Property(c => c.UserId).HasElementName("userId");
			modelBuilder.Entity<Course>().Property(c => c.CategoryId).HasElementName("categoryId");
			modelBuilder.Entity<Course>().Property(c => c.Picture).HasElementName("picture");
			// Category navigation property si ignore ediliyor. Çünkü Course entity si Category entity sine ihtiyaç duymuyor.
			modelBuilder.Entity<Course>().Ignore(c => c.Category);


			modelBuilder.Entity<Category>().ToCollection("categories");
			modelBuilder.Entity<Category>().HasKey(c => c.Id);
			modelBuilder.Entity<Course>().Property(c => c.Id).ValueGeneratedNever();
			/*
				* #NOTES Feature Course içerisinde bir owned entity dir. Bunu belirtmeliyiz.
				* owned entity ler kendi başlarına var olamazlar. Sadece parent entity ile
				* Course ise 
			*/
			modelBuilder.Entity<Course>().OwnsOne(c => c.Feature, feature =>
			{
				feature.HasElementName("feature");
				feature.Property(f => f.Duration).HasElementName("duration");
				feature.Property(f => f.Rating).HasElementName("rating");
				feature.Property(f => f.EducatorFullName).HasElementName("educatorFullName").HasMaxLength(100);
			});
		


		}

	}
}
