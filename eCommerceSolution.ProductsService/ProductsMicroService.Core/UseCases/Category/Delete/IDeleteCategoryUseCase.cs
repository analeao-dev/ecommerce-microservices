namespace ProductsMicroService.Core.UseCases.Category.Delete;

public interface IDeleteCategoryUseCase
{
    Task Execute(Guid categoryId, bool isActive);
}