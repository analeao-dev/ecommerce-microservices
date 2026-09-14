using MapsterMapper;
using ProductsMicroService.Communication.Responses;
using ProductsMicroService.Core.Dto;
using ProductsMicroService.Core.RepositoryContracts;

namespace ProductsMicroService.Core.UseCases.Category.List;

public class ListCategoriesUseCase : IListCategoriesUseCase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public ListCategoriesUseCase(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<CategoryResponse>> Execute()
    {
        var categories = await _categoryRepository.ListAll();

        var response = _mapper.Map<List<CategoryResponse>>(categories);

        return response;
    }
}
