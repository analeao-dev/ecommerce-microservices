using ProductsMicroService.Core.Dto;
using ProductsMicroService.Core.Entities;

namespace ProductsMicroService.Core.RepositoryContracts;

public interface IProductRepository
{
    Task<Product?> Add(Product product);
    Task<int> Delete(Guid productId);
    Task<int> Update(Guid productId, UpdateProductRequest request);
    Task<bool> ExistsProductByName(string productName);
    Task<bool> ExistsProductById(Guid productId);
    Task<IEnumerable<ProductDto>> ListAll();
    Task<ProductDto?> GetProductById(Guid productId);
}