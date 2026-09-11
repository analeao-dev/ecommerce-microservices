using Microsoft.AspNetCore.Mvc;
using ProductsMicroService.Communication.Requests;
using ProductsMicroService.Communication.Responses;
using ProductsMicroService.Core.UseCases.Product.Delete;
using ProductsMicroService.Core.UseCases.Product.Get;
using ProductsMicroService.Core.UseCases.Product.List;
using ProductsMicroService.Core.UseCases.Product.Register;
using ProductsMicroService.Core.UseCases.Product.Update;

namespace ProductsMicroService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IRegisterProductUseCase _registerProductUseCase;
    private readonly IDeleteProductUseCase _deleteProductUseCase;
    private readonly IListProductsUseCase _listProductsUseCase;
    private readonly IGetProductByIdUseCase _getProductByIdUseCase;
    private readonly IUpdateProductUseCase _updateProductUseCase;

    public ProductsController(IRegisterProductUseCase registerProductUseCase,
        IDeleteProductUseCase deleteProductUseCase, IListProductsUseCase listProductsUseCase,
        IGetProductByIdUseCase getProductByIdUseCase, IUpdateProductUseCase updateProductUseCase)
    {
        _registerProductUseCase = registerProductUseCase;
        _deleteProductUseCase = deleteProductUseCase;
        _listProductsUseCase = listProductsUseCase;
        _getProductByIdUseCase = getProductByIdUseCase;
        _updateProductUseCase = updateProductUseCase;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ProductResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ProductResponse>>> List()
    {
        var response = await _listProductsUseCase.Execute();

        return Ok(response);
    }

    [HttpGet("{productId:guid}")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductResponse>> GetById([FromRoute] Guid productId)
    {
        var response = await _getProductByIdUseCase.Execute(productId);

        if (response == null)
            return NotFound();

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterProductRequest request)
    {
        var response = await _registerProductUseCase.Execute(request);

        return Created("", response);
    }

    [HttpDelete("{productId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid productId)
    {
        await _deleteProductUseCase.Execute(productId);

        return NoContent();
    }

    [HttpPut("{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid productId, [FromBody] UpdateProductRequest request)
    {
        var response = await _updateProductUseCase.Execute(productId, request);

        // if (!response)
        //     return NotFound();

        return NoContent();
    }

}