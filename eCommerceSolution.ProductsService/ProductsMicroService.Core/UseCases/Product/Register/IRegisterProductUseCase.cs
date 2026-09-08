using ProductsMicroService.Communication.Requests;
using ProductsMicroService.Communication.Responses;

namespace ProductsMicroService.Core.UseCases.Product.Register;

public interface IRegisterProductUseCase
{
    Task<RegisterProductResponse> Execute(RegisterProductRequest request);
}