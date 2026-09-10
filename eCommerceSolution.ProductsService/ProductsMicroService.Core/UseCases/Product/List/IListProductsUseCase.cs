using ProductsMicroService.Communication.Responses;

namespace ProductsMicroService.Core.UseCases.Product.List;

public interface IListProductsUseCase
{
    Task<IReadOnlyList<ProductResponse>> Execute();
}
