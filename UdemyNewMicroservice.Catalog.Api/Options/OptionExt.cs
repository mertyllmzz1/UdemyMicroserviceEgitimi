using Microsoft.Extensions.Options;

namespace UdemyNewMicroservice.Catalog.Api.Options
{
	public static class OptionExt
	{
		public static IServiceCollection AddOptionsExt(this IServiceCollection services)
		{
			services.AddOptions<MongoOptions>().BindConfiguration(nameof(MongoOptions))
				.ValidateDataAnnotations()
				.ValidateOnStart();
			services.AddSingleton(s => s.GetRequiredService<IOptions<MongoOptions>>().Value);
			return services;
		}
	}
}
