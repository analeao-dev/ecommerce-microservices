using ProductsMicroService.Communication.Responses;

namespace ProductsMicroService.Core.UseCases.Category.List;

public interface IListCategoriesUseCase
{
    Task<IReadOnlyList<CategoryResponse>> Execute();
}
