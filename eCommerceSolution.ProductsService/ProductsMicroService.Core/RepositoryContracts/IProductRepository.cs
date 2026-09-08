using ProductsMicroService.Core.Entities;

namespace ProductsMicroService.Core.RepositoryContracts;

public interface IProductRepository
{
    Task<Product?> Add(Product product);
}