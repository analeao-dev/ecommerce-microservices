using MapsterMapper;
using ProductsMicroService.Communication.Responses;
using ProductsMicroService.Core.RepositoryContracts;

namespace ProductsMicroService.Core.UseCases.Category.Get;


public class GetCategoryByIdUseCase : IGetCategoryByIdUseCase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoryByIdUseCase(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<CategoryResponse?> Execute(Guid categoryId)
    {
        var category = await _categoryRepository.GetCategoryById(categoryId);

        var response = _mapper.Map<CategoryResponse>(category);

        return response;
    }
}