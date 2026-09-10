using Microsoft.AspNetCore.Mvc;
using ProductsMicroService.Communication.Requests;
using ProductsMicroService.Core.UseCases.Product.Delete;
using ProductsMicroService.Core.UseCases.Product.List;
using ProductsMicroService.Core.UseCases.Product.Register;

namespace ProductsMicroService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IRegisterProductUseCase _registerProductUseCase;
    private readonly IDeleteProductUseCase _deleteProductUseCase;
    private readonly IListProductsUseCase _listProductsUseCase;

    public ProductsController(IRegisterProductUseCase registerProductUseCase, IDeleteProductUseCase deleteProductUseCase, IListProductsUseCase listProductsUseCase)
    {
        _registerProductUseCase = registerProductUseCase;
        _deleteProductUseCase = deleteProductUseCase;
        _listProductsUseCase = listProductsUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var response = await _listProductsUseCase.Execute();

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterProductRequest request)
    {
        var response = await _registerProductUseCase.Execute(request);

        return Ok(response);
    }

    [HttpDelete("{productId}:Guid")]
    public async Task<IActionResult> Delete([FromRoute] Guid productId)
    {
        await _deleteProductUseCase.Execute(productId);

        return NoContent();
    }
}