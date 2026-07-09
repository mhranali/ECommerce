using ECommerce.UseCases.Common;
using ECommerce.UseCases.Contracts;
using ECommerce.UseCases.DTOs.Products;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

public class ProductsController(IProductService productService) : ApiBaseController
{
    //Get all product
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAllProducts(CancellationToken ct = default)
    {
        var result = await productService.GetAllProductAsync(ct);
        return ToActionResult(result);
    }

    //Get product by id

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> GetProduct(int id, CancellationToken ct = default)
    { 
        var result = await productService.GetProductByIdAsync(id, ct);
        return ToActionResult(result);  
    }


    //Get all type
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<TypeDto>>> GetAllTypes(CancellationToken ct = default)
    {
        var result = await productService.GetAllTypeAsync(ct);
        return ToActionResult(result);
    }

    //Get all brand
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetAllBrands(CancellationToken ct = default)
    {
        var result = await productService.GetAllBrandAsync(ct);
        return ToActionResult(result);
    }
}
