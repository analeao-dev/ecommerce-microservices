using ProductsMicroService.Core.Dto;

namespace ProductsMicroService.Core.UseCases.Product.Update;

public interface IUpdateProductUseCase
{
    Task Execute(Guid productId, UpdateProductRequest request);
}
