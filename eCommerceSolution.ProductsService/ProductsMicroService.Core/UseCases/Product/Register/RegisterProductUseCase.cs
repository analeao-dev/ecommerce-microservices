using FluentValidation;
using FluentValidation.Results;
using MapsterMapper;
using ProductsMicroService.Communication.Requests;
using ProductsMicroService.Communication.Responses;
using ProductsMicroService.Core.RepositoryContracts;
using ProductsMicroService.Exception;

namespace ProductsMicroService.Core.UseCases.Product.Register;

public class RegisterProductUseCase : IRegisterProductUseCase
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<RegisterProductRequest> _validator;

    public RegisterProductUseCase(IProductRepository productRepository, ICategoryRepository categoryRepository,
        IMapper mapper, IValidator<RegisterProductRequest> validator)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<RegisterProductResponse> Execute(RegisterProductRequest request)
    {
        await Validate(request);

        var product = _mapper.Map<Entities.Product>(request);

        var addedProduct = await _productRepository.Add(product)
                           ?? throw new InvalidOperationException("Product could not be registered.");

        return new RegisterProductResponse(addedProduct.ProductId, addedProduct.ProductName);
    }

    private async Task Validate(RegisterProductRequest request)
    {
        var result = await _validator.ValidateAsync(request);

        var productExists = await _productRepository.ExistsProductByName(request.ProductName);
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