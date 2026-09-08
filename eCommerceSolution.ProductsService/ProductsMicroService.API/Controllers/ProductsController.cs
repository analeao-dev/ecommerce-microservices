using Microsoft.AspNetCore.Mvc;
using ProductsMicroService.Communication.Requests;
using ProductsMicroService.Core.UseCases.Product.Register;

namespace ProductsMicroService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IRegisterProductUseCase _registerProductUseCase;

    public ProductsController(IRegisterProductUseCase registerProductUseCase)
    {
        _registerProductUseCase = registerProductUseCase;
    }
    
    [HttpPost]
    public async Task<IActionResult> Register([FromBody]RegisterProductRequest request)
    {
        var response = await _registerProductUseCase.Execute(request);
        
        return Ok(response);
    }
}