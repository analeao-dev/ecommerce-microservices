using ProductsMicroService.Core.Dto;
using ProductsMicroService.Core.Entities;

namespace ProductsMicroService.Core.RepositoryContracts;

public interface ICategoryRepository
{
    Task<Category?> Add(Category category);
    Task<IEnumerable<CategoryDto>> ListAll();
    Task<bool> ExistsCategoryByName(string categoryName);
    Task<bool> ExistsCategoryById(Guid categoryId);
    Task<CategoryDto?> GetCategoryById(Guid categoryId);
    Task<int> Delete(Guid categoryId, bool isActive);
}