using Dapper;
using ProductsMicroService.Core.Dto;
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

    public async Task<int> Delete(Guid productId)
    {
        const string query = @"DELETE FROM products WHERE product_id = @productId";

        var rowsAffected = await _dbContext.DbConnection.ExecuteAsync(query, new { productId });

        return rowsAffected;
    }

    public async Task<bool> ExistsProductByName(string productName)
    {
        const string query = @"SELECT EXISTS (SELECT 1 FROM products WHERE LOWER(product_name) = LOWER(@productName))";

        var result = await _dbContext.DbConnection.ExecuteScalarAsync<bool>(query, new { productName = @productName });

        return result;
    }

    public async Task<IEnumerable<ProductDto>> ListAll()
    {
        const string query = """
                             SELECT
                                 p.product_id AS "ProductId",
                                 p.product_name AS "ProductName",
                                 p.unit_price AS "UnitPrice",
                                 p.quantity_in_stock AS "QuantityInStock",
                                 c.category_name AS "CategoryName"
                             FROM products p
                             LEFT JOIN categories c ON c.category_id = p.category_id
                             """;

        var result = await _dbContext.DbConnection.QueryAsync<ProductDto>(query);

        return result;
    }

    public async Task<ProductDto?> GetProductById(Guid productId)
    {
        const string query = """
                             SELECT
                                 p.product_id AS "ProductId",
                                 p.product_name AS "ProductName",
                                 p.unit_price AS "UnitPrice",
                                 p.quantity_in_stock AS "QuantityInStock",
                                 c.category_name AS "CategoryName"
                             FROM products p
                             LEFT JOIN categories c ON c.category_id = p.category_id
                             WHERE p.product_id = @productId
                             """;

        var result = await _dbContext.DbConnection.QuerySingleOrDefaultAsync<ProductDto>(query, new { ProductId = productId });;

        return result;
    }
}