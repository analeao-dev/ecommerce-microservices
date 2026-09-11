using Mapster;
using MapsterMapper;
using ProductsMicroService.Communication.Responses;
using ProductsMicroService.Core.Dto;
using ProductsMicroService.Core.Entities;
using ProductsMicroService.Core.RepositoryContracts;
using ProductsMicroService.Core.UseCases.Product.List;

public class ListProductsUseCase : IListProductsUseCase
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ListProductsUseCase(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<ProductResponse>> Execute()
    {
        var products = await _productRepository.ListAll();

        var response = _mapper.Map<List<ProductResponse>>(products);

        return response;
    }
}