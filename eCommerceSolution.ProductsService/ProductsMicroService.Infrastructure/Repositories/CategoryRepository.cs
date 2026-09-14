using Dapper;
using ProductsMicroService.Core.Dto;
using ProductsMicroService.Core.Entities;
using ProductsMicroService.Core.RepositoryContracts;
using ProductsMicroService.Infrastructure.DbContext;

namespace ProductsMicroService.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly DapperDbContext _dbContext;

    public CategoryRepository(DapperDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Category?> Add(Category category)
    {
        category.CategoryId = Guid.NewGuid();

        const string query = "INSERT INTO Categories (category_id, category_name) VALUES (@CategoryId, @CategoryName)";

        var rowsAffected = await _dbContext.DbConnection.ExecuteAsync(query, category);

        return rowsAffected == 0 ? null : category;
    }

    public async Task<bool> ExistsCategoryByName(string categoryName)
    {
        const string query =
            @"SELECT EXISTS (SELECT 1 FROM categories WHERE LOWER(category_name) = LOWER(@categoryName))";

        var result = await _dbContext.DbConnection.ExecuteScalarAsync<bool>(query, new { categoryName = categoryName });

        return result;
    }

    public async Task<bool> ExistsCategoryById(Guid categoryId)
    {
        const string query = @"SELECT EXISTS (SELECT 1 FROM categories WHERE category_id = @categoryId )";

        var result = await _dbContext.DbConnection.ExecuteScalarAsync<bool>(query, new { categoryId = categoryId });

        return result;
    }

    public async Task<IEnumerable<CategoryDto>> ListAll()
    {
        const string query = """
                             SELECT
                                 c.category_id AS "CategoryId",
                                 c.category_name AS "CategoryName",
                                 c.is_active AS "IsActive"
                             FROM categories c
                             WHERE cc.is_active = true
                             """;

        var result = await _dbContext.DbConnection.QueryAsync<CategoryDto>(query);

        return result;
    }

    public async Task<CategoryDto?> GetCategoryById(Guid categoryId)
    {
        const string query = """
                             SELECT
                                 c.category_id AS "CategoryId",
                                 c.category_name AS "CategoryName",
                                 c.is_active AS "IsActive"
                             FROM categories c
                             WHERE cc.category_id = @categoryId
                             """;

        var result = await _dbContext.DbConnection.QuerySingleOrDefaultAsync<CategoryDto>(query, new { CategoryId = categoryId });

        return result;
    }

    public async Task<int> Delete(Guid categoryId, bool isActive)
    {
        const string query = """
                         UPDATE categories
                            SET is_active = @IsActive
                         WHERE category_id = @CategoryId
                         """;
        var rowsAffected = await _dbContext.DbConnection.ExecuteAsync(query, new { CategoryId = categoryId, IsActive = isActive });

        return rowsAffected;
    }
}
