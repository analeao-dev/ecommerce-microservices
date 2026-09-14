using ProductsMicroService.Communication.Requests;
using ProductsMicroService.Core.RepositoryContracts;

namespace ProductsMicroService.Core.UseCases.Product.Delete;

public class DeleteProductUseCase : IDeleteProductUseCase
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteProductUseCase(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task Execute(Guid categoryId, bool isActive)
    {
        await _categoryRepository.Delete(categoryId, isActive);
    }
                    
    private async Task Validate(Guid categoryId, DeleteCategoryRequest request)
    {
        var validator = new DeleteProductValidator();
        var categoryExists = await _categoryRepository.ExistsCategoryById(categoryId);
    }
}