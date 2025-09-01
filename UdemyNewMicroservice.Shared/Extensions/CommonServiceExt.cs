using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UdemyNewMicroservice.Shared.Extensions
{
	public static class CommonServiceExt
	{
		//Program.cs'de kodu kısaltmak için yazıldı.
		//assembly parametresi, MediatR'ın hangi derlemeden servisleri kaydedeceğini belirtir.
		public static IServiceCollection AddCommonServiceExt(this IServiceCollection services,Type assembly)
		{
			services.AddHttpContextAccessor();
			services.AddMediatR(p => p.RegisterServicesFromAssemblyContaining(assembly));
			services.AddFluentValidationAutoValidation();
			services.AddValidatorsFromAssemblyContaining(assembly);
			services.AddAutoMapper(assembly);
			return services;
		}
	}
}
