using MapsterMapper;
using ProductsMicroService.Communication.Responses;
using ProductsMicroService.Core.Dto;
using ProductsMicroService.Core.RepositoryContracts;

namespace ProductsMicroService.Core.UseCases.Product.Get;

public class GetProductByIdUseCase : IGetProductByIdUseCase
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    
    public GetProductByIdUseCase(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductResponse?> Execute(Guid productId)
    {
        var product = await _productRepository.GetProductById(productId);
        
        var response = _mapper.Map<ProductResponse>(product);
        
        return response;
    }
}