namespace UdemyNewMicroservice.Catalog.Api.Features.Categories.Create
{ 
	// record Immutabele, .net08 ile gelen private constructer olarak tanımladık
	public record class CreateCategoryCommand(string Name):IRequest<ServiceResult<CreateCategoryResponse>>;


}
