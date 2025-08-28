using MongoDB.Driver;
using UdemyNewMicroservice.Catalog.Api.Options;

namespace UdemyNewMicroservice.Catalog.Api.Repositories
{
	public static class RepositoryExt
	{
		public static IServiceCollection AddDatabaseServiceExt(this IServiceCollection services)
		{
			services.AddSingleton<IMongoClient, MongoClient>(s =>
			{
				var options = s.GetRequiredService<MongoOptions>();

				return new MongoClient(options.ConnectionString);
			});
			services.AddScoped(s =>
			{
				var mongoClient = s.GetRequiredService<IMongoClient>();
				var options = s.GetRequiredService<MongoOptions>();

				return AppDbContext.Create(mongoClient.GetDatabase(options.DatabaseName));
			});
			return services;
		}
	}
}
