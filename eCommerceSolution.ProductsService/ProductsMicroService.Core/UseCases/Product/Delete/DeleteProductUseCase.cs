using ProductsMicroService.Core.RepositoryContracts;

namespace ProductsMicroService.Core.UseCases.Product.Delete;

public class DeleteProductUseCase : IDeleteProductUseCase
{
    private readonly IProductRepository _productRepository;

    public DeleteProductUseCase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Execute(Guid productId)
    {
        await _productRepository.Delete(productId);
    }

    private async Task Validate()
    {
        var validator = new DeleteProductValidator();
    }
}