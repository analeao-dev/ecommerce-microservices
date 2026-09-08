using ProductsMicroService.Core.Entities;

namespace ProductsMicroService.Core.RepositoryContracts;

public interface ICategoryRepository
{
    Task<Category?> Add(Category category);
    Task<bool> ExistsCategoryByName(string categoryName);
    Task<bool> ExistsCategoryById(Guid categoryId);
}