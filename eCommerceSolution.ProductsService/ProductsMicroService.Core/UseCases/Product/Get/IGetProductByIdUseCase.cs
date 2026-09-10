using ProductsMicroService.Communication.Responses;
using ProductsMicroService.Core.Dto;

namespace ProductsMicroService.Core.UseCases.Product.Get;

public interface IGetProductByIdUseCase
{
    Task<ProductResponse?> Execute(Guid productId);
}