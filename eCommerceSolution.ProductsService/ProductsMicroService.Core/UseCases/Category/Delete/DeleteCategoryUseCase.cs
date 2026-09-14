using ProductsMicroService.Core.RepositoryContracts;
using ProductsMicroService.Core.UseCases.Category.Delete;

namespace ProductsMicroService.Core.UseCases.Category.Delete;

public class DeleteCategoryUseCase : IDeleteCategoryUseCase
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryUseCase(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task Execute(Guid categoryId, bool isActive)
    {
        await _categoryRepository.Delete(categoryId, isActive);
    }

    private async Task Validate()
    {
        var validator = new DeleteCategoryValidator();

        
    }
}