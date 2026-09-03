using ProductsMicroService.Communication.Requests;
using ProductsMicroService.Communication.Responses;

namespace ProductsMicroService.Core.UseCases.Category.Register;

public interface IRegisterCategoryUseCase
{
    Task<RegisterCategoryResponse> Execute(RegisterCategoryRequest request);
}