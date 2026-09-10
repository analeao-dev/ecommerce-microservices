using ProductsMicroService.Core.Dto;
using ProductsMicroService.Core.Entities;

namespace ProductsMicroService.Core.RepositoryContracts;

public interface IProductRepository
{
    Task<Product?> Add(Product product);
    Task<int> Delete(Guid productId);
    Task<bool> ExistsProductByName(string productName);
    Task<IEnumerable<ProductDto>> ListAll();
    Task<ProductDto?> GetProductById(Guid productId);
}