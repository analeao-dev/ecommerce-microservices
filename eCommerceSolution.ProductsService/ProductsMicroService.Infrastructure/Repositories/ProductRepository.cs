using Dapper;
using ProductsMicroService.Core.Entities;
using ProductsMicroService.Core.RepositoryContracts;
using ProductsMicroService.Infrastructure.DbContext;

namespace ProductsMicroService.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DapperDbContext _dbContext;

    public ProductRepository(DapperDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product?> Add(Product product)
    {
        product.ProductId = Guid.NewGuid();

        const string query = """
            INSERT INTO products (product_id, product_name, unit_price, quantity_in_stock, category_id)
            VALUES (@ProductId, @ProductName, @UnitPrice, @QuantityInStock, @CategoryId)
            """;

        var rowsAffected = await _dbContext.DbConnection.ExecuteAsync(query, product);

        return rowsAffected == 0 ? null : product;
    }
}
