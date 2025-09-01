using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyNewMicroservice.Shared.Filter
{
	public  class ValidationFilter<T> : IEndpointFilter
	{
		public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
		{
			//EndpointFilterInvocationContext: Endpoint'e gelen isteğin bağlamını temsil eder.

			var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
			if (validator is null)
			{
				return await next(context);
			}
			var requestModel = context.Arguments.OfType<T>().FirstOrDefault();
			if (requestModel is null)
			{
				return await next(context); //await next endpoint  metoduna devam etmeyi sağlar
			}

			var validationResult = await validator.ValidateAsync(requestModel);

			if (!validationResult.IsValid)
			{
				return Results.ValidationProblem(validationResult.ToDictionary());
			}

			return await next(context);
			//EndpointFilterDelegate: Bir sonraki filtreyi veya nihai işlemeyi temsil eder.
		}
	}
}
