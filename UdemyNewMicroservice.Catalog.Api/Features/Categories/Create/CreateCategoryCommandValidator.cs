using FluentValidation;

namespace UdemyNewMicroservice.Catalog.Api.Features.Categories.Create
{
	public class CreateCategoryCommandValidator:AbstractValidator<CreateCategoryCommand>
	{
		public CreateCategoryCommandValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage(" {PropertyName} must be filled")
				.Length(4,50).WithMessage("Name must be between than 4 and 50 characters");
		}
	}
}
