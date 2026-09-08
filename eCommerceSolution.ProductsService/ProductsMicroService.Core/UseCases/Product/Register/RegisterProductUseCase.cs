using FluentValidation.Results;
using ProductsMicroService.Communication.Requests;
using ProductsMicroService.Communication.Responses;
using ProductsMicroService.Core.RepositoryContracts;
using ProductsMicroService.Exception;

namespace ProductsMicroService.Core.UseCases.Product.Register;

public class RegisterProductUseCase : IRegisterProductUseCase
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public RegisterProductUseCase(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<RegisterProductResponse> Execute(RegisterProductRequest request)
    {
        await Validate(request);

        var product = new Entities.Product
        {
            ProductName = request.ProductName,
            UnitPrice = request.Price ?? 0,
            QuantityInStock = request.Quantity ?? 0,
            CategoryId = request.CategoryId
        };

        var addedProduct = await _productRepository.Add(product)
                           ?? throw new InvalidOperationException("Product could not be registered.");

        return new RegisterProductResponse(addedProduct.ProductId, addedProduct.ProductName);
    }

    private async Task Validate(RegisterProductRequest request)
    {
        var validator = new RegisterProductValidator();
        var result = await validator.ValidateAsync(request);

        var categoryExists = await _categoryRepository.ExistsCategoryById(request.CategoryId);

        if (!categoryExists)
        {
            result.Errors.Add(new ValidationFailure("Category", "Category does not exist."));
        }

        if (result.IsValid == false)
        {
            var errosMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errosMessages);
        }
    }
}