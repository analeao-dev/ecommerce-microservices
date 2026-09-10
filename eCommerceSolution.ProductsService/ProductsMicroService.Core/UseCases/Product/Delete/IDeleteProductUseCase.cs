namespace ProductsMicroService.Core.UseCases.Product.Delete;

public interface IDeleteProductUseCase 
{
    Task Execute(Guid productId);
}