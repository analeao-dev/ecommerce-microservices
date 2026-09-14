using ProductsMicroService.Communication.Responses;

namespace ProductsMicroService.Core.UseCases.Category.Get;

public interface IGetCategoryByIdUseCase
{
    Task<CategoryResponse?> Execute(Guid categoryId);
}