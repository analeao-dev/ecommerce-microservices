using Microsoft.AspNetCore.Mvc;
using ProductsMicroService.Communication.Requests;
using ProductsMicroService.Core.UseCases.Category.Register;

namespace ProductsMicroService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IRegisterCategoryUseCase _registerCategoryUseCase;

    public CategoriesController(IRegisterCategoryUseCase  registerCategoryUseCase)
    {
        _registerCategoryUseCase = registerCategoryUseCase;
    }
    
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterCategoryRequest request)
    {
        var response = await _registerCategoryUseCase.Execute(request);
        return Created("", response);
    }
}