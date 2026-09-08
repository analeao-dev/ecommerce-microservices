using ProductsMicroService.Communication.Requests;
using ProductsMicroService.Communication.Responses;
using ProductsMicroService.Core.RepositoryContracts;
using ProductsMicroService.Exception;

namespace ProductsMicroService.Core.UseCases.Category.Register;

public class RegisterCategoryUseCase : IRegisterCategoryUseCase
{
    private readonly ICategoryRepository _categoryRepository;

    public RegisterCategoryUseCase(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<RegisterCategoryResponse> Execute(RegisterCategoryRequest request)
    {
        await  Validate(request);
        
        var category = new Entities.Category
        {
            CategoryName = request.CategoryName
        };
        
        var addedCategory = await _categoryRepository.Add(category)
            ?? throw new InvalidOperationException("Category could not be registered.");;
        
        return new RegisterCategoryResponse(addedCategory.CategoryId, addedCategory.CategoryName);
    }

    private async Task Validate(RegisterCategoryRequest request)
    {
        var validator = new RegisterCategoryValidator();
        
        var result = await validator.ValidateAsync(request);
        
        var categoryExist = await _categoryRepository.ExistsCategoryByName(request.CategoryName);
        if (categoryExist)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("Category", "Category already exists."));
        }

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}