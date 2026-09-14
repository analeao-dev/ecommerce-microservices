using Microsoft.AspNetCore.Mvc;
using ProductsMicroService.Communication.Requests;
using ProductsMicroService.Communication.Responses;
using ProductsMicroService.Core.UseCases.Category.Delete;
using ProductsMicroService.Core.UseCases.Category.Get;
using ProductsMicroService.Core.UseCases.Category.List;
using ProductsMicroService.Core.UseCases.Category.Register;

namespace ProductsMicroService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IRegisterCategoryUseCase _registerCategoryUseCase;
    private readonly IListCategoriesUseCase _listCategoriesUseCase;
    private readonly IGetCategoryByIdUseCase _getCategoryByIdUseCase;
    private readonly IDeleteCategoryUseCase _deleteCategoryUseCase;


    public CategoriesController(
        IRegisterCategoryUseCase registerCategoryUseCase, 
        IListCategoriesUseCase listCategoriesUseCase, 
        IGetCategoryByIdUseCase getCategoryByIdUseCase,
        IDeleteCategoryUseCase deleteCategoryUseCase)
    {
        _registerCategoryUseCase = registerCategoryUseCase;
        _listCategoriesUseCase = listCategoriesUseCase;
        _getCategoryByIdUseCase = getCategoryByIdUseCase;
        _deleteCategoryUseCase = deleteCategoryUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterCategoryRequest request)
    {
        var response = await _registerCategoryUseCase.Execute(request);
        return Created("", response);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CategoryResponse>>> List()
    {
        var response = await _listCategoriesUseCase.Execute();

        return Ok(response);
    }

    [HttpGet("{categoryId:guid}")]
    public async Task<ActionResult<CategoryResponse>> GetById([FromRoute] Guid categoryId)
    {
        var response = await _getCategoryByIdUseCase.Execute(categoryId);

        if (response == null)
            return NotFound();


        return Ok(response);
    }

    [HttpDelete("{categoryId:guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid categoryId, bool isActive)
    {
        await _deleteCategoryUseCase.Execute(categoryId, isActive);

        return NoContent();
    }
}