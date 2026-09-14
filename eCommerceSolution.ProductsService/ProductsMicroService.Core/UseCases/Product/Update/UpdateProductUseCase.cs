using FluentValidation;
using FluentValidation.Results;
using ProductsMicroService.Communication.Requests;
using ProductsMicroService.Core.Dto;
using ProductsMicroService.Core.RepositoryContracts;
using ProductsMicroService.Exception;

namespace ProductsMicroService.Core.UseCases.Product.Update;

public class UpdateProductUseCase : IUpdateProductUseCase
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IValidator<UpdateProductRequest> _validator;

    public UpdateProductUseCase(IProductRepository productRepository, ICategoryRepository categoryRepository, IValidator<UpdateProductRequest> validator)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _validator = validator;
    }

    public async Task Execute(Guid productId, UpdateProductRequest request)
    {
        await Validate(productId, request);
    }

    private async Task Validate(Guid productId, UpdateProductRequest request)
    {
        var result = await _validator.ValidateAsync(request);

        var productExists = await _productRepository.ExistsProductById(productId);
        var categoryExists = await _categoryRepository.ExistsCategoryById(request.CategoryId);

        if (productExists)
        {
            result.Errors.Add(new ValidationFailure(nameof(request.ProductName), "Product already exists."));
        }

        if (!categoryExists)
        {
            result.Errors.Add(new ValidationFailure(nameof(request.CategoryId), "Category does not exist."));
        }

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }

    }
}
